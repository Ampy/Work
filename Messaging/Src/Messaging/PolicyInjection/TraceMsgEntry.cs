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
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Instrumentation;

namespace RTSafe.RTDP.Messaging.PolicyInjection
{
    /// <summary>
    /// A <see cref="MsgEntry"/> class that contains the extra information logged
    /// by the <see cref="MsgCallHandler"/>.
    /// </summary>
    [Serializable]
    public class TraceMsgEntry : MsgEntry
    {
        private string typeName;
        private string methodName;
        private string returnValue;
        private string callStack;
        private string exception;
        private TimeSpan? callTime;

        /// <summary>
        /// Create an empty <see cref="TraceMsgEntry"/>.
        /// </summary>
        public TraceMsgEntry()
        {
        }

        /// <summary>
        /// Create a new <see cref="TraceMsgEntry"/>.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="category">Log category.</param>
        /// <param name="priority">Log priority.</param>
        /// <param name="eventId">Log event id.</param>
        /// <param name="severity">Log severity.</param>
        /// <param name="title">Log title.</param>
        /// <param name="properties">Extra properties. This contains the parameters to the call.</param>
        /// <param name="typeName">Name of type implementing the method being called.</param>
        /// <param name="methodName">Method name being called.</param>
        public TraceMsgEntry(object message, string category, int priority, int eventId, TraceEventType severity, string title, IDictionary<string, object> properties, string typeName, string methodName) 
            : base(message, category, priority, eventId, severity, title, properties)
        {
            this.typeName = typeName;
            this.methodName = methodName;
        }

        /// <summary>
        /// Create a new <see cref="TraceMsgEntry"/>.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="categories">Categories of the log entry.</param>
        /// <param name="priority">Log priority.</param>
        /// <param name="eventId">Log event id.</param>
        /// <param name="severity">Log severity.</param>
        /// <param name="title">Log title.</param>
        /// <param name="properties">Extra properties. This contains the parameters to the call.</param>
        /// <param name="typeName">Name of type implementing the method being called.</param>
        /// <param name="methodName">Method name being called.</param>
        public TraceMsgEntry(object message, ICollection<string> categories, int priority, int eventId, TraceEventType severity, string title, IDictionary<string, object> properties, string typeName, string methodName)
            : base(message, categories, priority, eventId, severity, title, properties)
        {
            this.typeName = typeName;
            this.methodName = methodName;
        }

        /// <summary>
        /// Type name
        /// </summary>
        /// <value>type name</value>
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        /// <summary>
        /// Method name
        /// </summary>
        /// <value>method name</value>
        public string MethodName
        {
            get { return methodName; }
            set { methodName = value; }
        }

        /// <summary>
        /// Return value from the call.
        /// </summary>
        /// <value>return value</value>
        public string ReturnValue
        {
            get { return returnValue; }
            set { returnValue = value; }
        }

        /// <summary>
        /// The call stack from the current call.
        /// </summary>
        /// <value>call stack string.</value>
        public string CallStack
        {
            get { return callStack; }
            set { callStack = value; }
        }

        /// <summary>
        /// Exception thrown from the target.
        /// </summary>
        /// <value>If exception was thrown, this is the exception object. Null if no exception thrown.</value>
        public string Exception
        {
            get { return exception; }
            set { exception = value; }
        }

        /// <summary>
        /// Total time to call the target.
        /// </summary>
        /// <value>null if not logged, else the elapsed time.</value>
        [IgnoreMember]
        public TimeSpan? CallTime
        {
            get { return callTime; }
            set { callTime = value; }
        }


        /// <summary>
        /// This is to support WMI instrumentation by returning
        /// the actual <see cref="CallTime"/> 
        /// </summary>
        [ManagedName("CallTime")]
        public TimeSpan ElapsedTime
        {
            get { return callTime.HasValue ? callTime.Value : TimeSpan.Zero; }
        }
    }
}
