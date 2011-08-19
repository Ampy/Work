﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Logging Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Security.Principal;
using RTSafe.RTDP.Messaging.Filters;
using RTSafe.RTDP.Messaging.Formatters;
using RTSafe.RTDP.Messaging.Instrumentation;
using RTSafe.RTDP.Messaging.Properties;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.Utility;

namespace RTSafe.RTDP.Messaging
{
    /// <summary>
    /// Instance based class to write log messages based on a given configuration.
    /// Messages are routed based on category.
    /// </summary>
    /// <remarks>
    /// <para>
    /// To write log messages to the default configuration, use the <see cref="Messager"/> facade.  
    /// Only create an instance of a LogWriter if you need to write log messages using a custom configuration.
    /// </para>
    /// <para>
    /// The LogWriter works as an entry point to the <see cref="System.Diagnostics"/> trace listeners. 
    /// It will trace the <see cref="MsgEntry"/> through the <see cref="TraceListeners"/>s associated with the <see cref="MsgSource"/>s 
    /// for all the matching categories in the elements of the <see cref="MsgEntry.Categories"/> property of the log entry. 
    /// If the "all events" special log source is configured, the log entry will be traced through the log source regardles of other categories 
    /// that might have matched.
    /// If the "all events" special log source is not configured and the "unprocessed categories" special log source is configured,
    /// and the category specified in the MsgEntry being logged is not defined, then the MsgEntry will be logged to the "unprocessed categories"
    /// special log source.
    /// If both the "all events" and "unprocessed categories" special log sources are not configured and the property LogWarningsWhenNoCategoriesMatch
    /// is set to true, then the MsgEntry is logged to the "logging errors and warnings" special log source.
    /// </para>
    /// </remarks>
    public abstract class MsgWriter : IDisposable
    {
        /// <summary>
        /// EventID used on LogEntries that occur when internal LogWriter mechanisms fail.
        /// </summary>
        public const int LogWriterFailureEventID = 6352;

        private const int DefaultPriority = -1;
        private const TraceEventType DefaultSeverity = TraceEventType.Information;
        private const int DefaultEventId = 1;
        private const string DefaultTitle = "";
        private static readonly ICollection<string> emptyCategoriesList = new string[0];

        /// <summary>
        /// Gets the <see cref="MsgSource"/> mappings available for the <see cref="MsgWriter"/>.
        /// </summary>
        public abstract IDictionary<string, MsgSource> TraceSources
        {
            get;
        }

        /// <summary>
        /// Releases the resources used by the <see cref="MsgWriter"/>.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Releases the resources used by the <see cref="MsgWriter"/>.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> when disposing, <see langword="false"/> otherwise.</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Empties the context items dictionary.
        /// </summary>
        public void FlushContextItems()
        {
            ContextItems items = new ContextItems();
            items.FlushContextItems();
        }

        /// <summary>
        /// Returns the filter of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of filter requiered.</typeparam>
        /// <returns>The instance of <typeparamref name="T"/> in the filters collection, or <see langword="null"/> 
        /// if there is no such instance.</returns>
        public abstract T GetFilter<T>()
            where T : class, IMsgFilter;

        /// <summary>
        /// Returns the filter of type <typeparamref name="T"/> named <paramref name="name"/>.
        /// </summary>
        /// <typeparam name="T">The type of filter required.</typeparam>
        /// <param name="name">The name of the filter required.</param>
        /// <returns>The instance of <typeparamref name="T"/> named <paramref name="name"/> in 
        /// the filters collection, or <see langword="null"/> if there is no such instance.</returns>
        public abstract T GetFilter<T>(string name)
            where T : class, IMsgFilter;

        /// <summary>
        /// Returns the filter named <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the filter required.</param>
        /// <returns>The filter named <paramref name="name"/> in 
        /// the filters collection, or <see langword="null"/> if there is no such filter.</returns>
        public abstract IMsgFilter GetFilter(string name);

        /// <summary>
        /// Gets a list of <see cref="MsgSource"/> objects for the log entry.
        /// </summary>
        /// <param name="MsgEntry">The <see cref="MsgEntry"/> to get the matching trace sources.</param>
        /// <returns>A collection of <see cref="MsgSource"/> objects.</returns>
        public abstract IEnumerable<MsgSource> GetMatchingTraceSources(MsgEntry MsgEntry);

        /// <summary>
        /// Queries whether logging is enabled.
        /// </summary>
        /// <returns><b>true</b> if logging is enabled.</returns>
        public abstract bool IsLoggingEnabled();

        /// <summary>
        /// Queries whether tracing is enabled.
        /// </summary>
        /// <returns><b>true</b> if tracing is enabled.</returns>
        public abstract bool IsTracingEnabled();

        /// <summary>
        /// Reset lock timeouts to thier original values.
        /// </summary>
        public static void ResetLockTimeouts()
        {
        }

        /// <summary>
        /// Adds a key/value pair to the <see cref="System.Runtime.Remoting.Messaging.CallContext"/> dictionary.  
        /// Context items will be recorded with every log entry.
        /// </summary>
        /// <param name="key">Hashtable key</param>
        /// <param name="value">Value.  Objects will be serialized.</param>
        /// <example>The following example demonstrates use of the AddContextItem method.
        /// <code>Logger.SetContextItem("SessionID", myComponent.SessionId);</code></example>
        public void SetContextItem(object key,
                                   object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            ContextItems items = new ContextItems();
            items.SetContextItem(key, value);
        }

        /// <summary>
        /// Queries whether a <see cref="MsgEntry"/> shold be logged.
        /// </summary>
        /// <param name="log">The log entry to check.</param>
        /// <returns><b>true</b> if the entry should be logged.</returns>
        public abstract bool ShouldLog(MsgEntry log);

        /// <overloads>
        /// Write a new log entry to the default category.
        /// </overloads>
        /// <summary>
        /// Write a new log entry to the default category.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        public void Write(object message)
        {
            this.Write(
                message,
                emptyCategoriesList,
                DefaultPriority,
                DefaultEventId,
                DefaultSeverity,
                DefaultTitle,
                null);
        }

        /// <summary>
        /// Write a new log entry to a specific category.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        public void Write(object message, string category)
        {
            this.Write(message, category, DefaultPriority, DefaultEventId, DefaultSeverity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific category and priority.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        public void Write(object message, string category, int priority)
        {
            this.Write(message, category, priority, DefaultEventId, DefaultSeverity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific category, priority and event id.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        public void Write(object message, string category, int priority, int eventId)
        {
            this.Write(message, category, priority, eventId, DefaultSeverity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific category, priority, event id and severity.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        /// <param name="severity">Log entry severity as a <see cref="TraceEventType"/> enumeration. (Unspecified, Information, Warning or Error).</param>
        public void Write(object message, string category, int priority, int eventId, TraceEventType severity)
        {
            this.Write(message, category, priority, eventId, severity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific category, priority, event id, severity
        /// and title.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        /// <param name="severity">Log message severity as a <see cref="TraceEventType"/> enumeration. (Unspecified, Information, Warning or Error).</param>
        /// <param name="title">Additional description of the log entry message</param>
        public void Write(
            object message,
            string category,
            int priority,
            int eventId,
            TraceEventType severity,
            string title)
        {
            this.Write(message, category, priority, eventId, severity, title, null);
        }

        /// <summary>
        /// Write a new log entry and a dictionary of extended properties.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="properties">Dictionary of key/value pairs to log.</param>
        public void Write(object message, IDictionary<string, object> properties)
        {
            this.Write(
                message,
                emptyCategoriesList,
                DefaultPriority,
                DefaultEventId,
                DefaultSeverity,
                DefaultTitle,
                properties);
        }

        /// <summary>
        /// Write a new log entry to a specific category with a dictionary 
        /// of extended properties.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="properties">Dictionary of key/value pairs to log.</param>
        public void Write(object message, string category, IDictionary<string, object> properties)
        {
            this.Write(
                message,
                category,
                DefaultPriority,
                DefaultEventId,
                DefaultSeverity,
                DefaultTitle,
                properties);
        }

        /// <summary>
        /// Write a new log entry to with a specific category, priority and a dictionary 
        /// of extended properties.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="properties">Dictionary of key/value pairs to log.</param>
        public void Write(object message, string category, int priority, IDictionary<string, object> properties)
        {
            this.Write(message, category, priority, DefaultEventId, DefaultSeverity, DefaultTitle, properties);
        }

        /// <summary>
        /// Write a new log entry with a specific category, priority, event Id, severity
        /// title and dictionary of extended properties.
        /// </summary>
        /// <example>The following example demonstrates use of the Write method with
        /// a full set of parameters.
        /// <code></code></example>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        /// <param name="severity">Log message severity as a <see cref="TraceEventType"/> enumeration. (Unspecified, Information, Warning or Error).</param>
        /// <param name="title">Additional description of the log entry message.</param>
        /// <param name="properties">Dictionary of key/value pairs to log.</param>
        public void Write(
            object message,
            string category,
            int priority,
            int eventId,
            TraceEventType severity,
            string title,
            IDictionary<string, object> properties)
        {
            this.Write(message, new string[] { category }, priority, eventId, severity, title, properties);
        }

        /// <summary>
        /// Write a new log entry to a specific collection of categories.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        public void Write(object message, IEnumerable<string> categories)
        {
            this.Write(message, categories, DefaultPriority, DefaultEventId, DefaultSeverity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific collection of categories and priority.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        public void Write(object message, IEnumerable<string> categories, int priority)
        {
            this.Write(message, categories, priority, DefaultEventId, DefaultSeverity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific collection of categories, priority and event id.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        public void Write(object message, IEnumerable<string> categories, int priority, int eventId)
        {
            this.Write(message, categories, priority, eventId, DefaultSeverity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific collection of categories, priority, event id and severity.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        /// <param name="severity">Log entry severity as a <see cref="TraceEventType"/> enumeration. 
        /// (Unspecified, Information, Warning or Error).</param>
        public void Write(
            object message,
            IEnumerable<string> categories,
            int priority,
            int eventId,
            TraceEventType severity)
        {
            this.Write(message, categories, priority, eventId, severity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific collection of categories, priority, event id, severity
        /// and title.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        /// <param name="severity">Log message severity as a <see cref="TraceEventType"/> enumeration. (Unspecified, Information, Warning or Error).</param>
        /// <param name="title">Additional description of the log entry message</param>
        public void Write(
            object message,
            IEnumerable<string> categories,
            int priority,
            int eventId,
            TraceEventType severity,
            string title)
        {
            this.Write(message, categories, priority, eventId, severity, title, null);
        }

        /// <summary>
        /// Write a new log entry to a specific collection of categories with a dictionary of extended properties.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        /// <param name="properties">Dictionary of key/value pairs to log.</param>
        public void Write(object message, IEnumerable<string> categories, IDictionary<string, object> properties)
        {
            this.Write(message, categories, DefaultPriority, DefaultEventId, DefaultSeverity, DefaultTitle, properties);
        }

        /// <summary>
        /// Write a new log entry to with a specific collection of categories, priority and a dictionary 
        /// of extended properties.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="properties">Dictionary of key/value pairs to log.</param>
        public void Write(
            object message,
            IEnumerable<string> categories,
            int priority,
            IDictionary<string,
            object> properties)
        {
            this.Write(message, categories, priority, DefaultEventId, DefaultSeverity, DefaultTitle, properties);
        }

        /// <summary>
        /// Write a new log entry with a specific category, priority, event Id, severity
        /// title and dictionary of extended properties.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        /// <param name="severity">Log message severity as a <see cref="TraceEventType"/> enumeration. (Unspecified, Information, Warning or Error).</param>
        /// <param name="title">Additional description of the log entry message.</param>
        /// <param name="properties">Dictionary of key/value pairs to log.</param>
        public void Write(
            object message,
            IEnumerable<string> categories,
            int priority,
            int eventId,
            TraceEventType severity,
            string title,
            IDictionary<string, object> properties)
        {
            MsgEntry log = new MsgEntry();
            log.Message = message.ToString();
            log.Categories = categories.ToArray();
            log.Priority = priority;
            //log.EventId = eventId;
            log.Severity = severity;
            //log.Title = title;
            log.ExtendedProperties = properties;

            this.Write(log);
        }

        /// <summary>
        /// Writes a new log entry as defined in the <see cref="MsgEntry"/> parameter.
        /// </summary>
        /// <param name="log">Log entry object to write.</param>
        public abstract void Write(MsgEntry log);
    }
}
