//===============================================================================
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
using System.Diagnostics;
using System.IO;
using RTSafe.RTDP.Messaging.Formatters;
using RTSafe.RTDP.Messaging.Instrumentation;

namespace RTSafe.RTDP.Messaging.TraceListeners
{
    /// <summary>
    /// Extends <see cref="TextWriterTraceListener"/> to add formatting capabilities.
    /// </summary>
    public class FormattedTextWriterTraceListener : TextWriterTraceListener
    {
        private IMsgFormatter formatter;

        /// <summary>
        /// Initializes a new instance of <see cref="FormattedTextWriterTraceListener"/>.
        /// </summary>
        public FormattedTextWriterTraceListener()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="FormattedTextWriterTraceListener"/> with a <see cref="IMsgFormatter"/>.
        /// </summary>
        /// <param name="formatter">The formatter to format the messages.</param>
        public FormattedTextWriterTraceListener(IMsgFormatter formatter)
        {
            this.Formatter = formatter;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="FormattedTextWriterTraceListener"/> with a 
        /// <see cref="IMsgFormatter"/> and a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="formatter">The formatter to format the messages.</param>
        public FormattedTextWriterTraceListener(Stream stream, IMsgFormatter formatter)
            : this(stream)
        {
            this.Formatter = formatter;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="FormattedTextWriterTraceListener"/> with a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        public FormattedTextWriterTraceListener(Stream stream)
            : base(stream)
        { }

        /// <summary>
        /// Initializes a new instance of <see cref="FormattedTextWriterTraceListener"/> with a 
        /// <see cref="IMsgFormatter"/> and a <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="formatter">The formatter to format the messages.</param>
        public FormattedTextWriterTraceListener(TextWriter writer, IMsgFormatter formatter)
            : this(writer)
        {
            this.Formatter = formatter;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="FormattedTextWriterTraceListener"/> with a <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        public FormattedTextWriterTraceListener(TextWriter writer)
            : base(writer)
        { }

        /// <summary>
        /// Initializes a new instance of <see cref="FormattedTextWriterTraceListener"/> with a 
        /// <see cref="IMsgFormatter"/> and a file name.
        /// </summary>
        /// <param name="fileName">The file name to write to.</param>
        /// <param name="formatter">The formatter to format the messages.</param>
        public FormattedTextWriterTraceListener(string fileName, IMsgFormatter formatter)
            : this(fileName)
        {
            this.Formatter = formatter;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="FormattedTextWriterTraceListener"/> with a file name.
        /// </summary>
        /// <param name="fileName">The file name to write to.</param>
        public FormattedTextWriterTraceListener(string fileName)
            : base(RootFileNameAndEnsureTargetFolderExists(fileName))
        {
        }

        /// <summary>
        /// Initializes a new named instance of <see cref="FormattedTextWriterTraceListener"/> with a 
        /// <see cref="IMsgFormatter"/> and a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="name">The name.</param>
        /// <param name="formatter">The formatter to format the messages.</param>
        public FormattedTextWriterTraceListener(Stream stream, string name, IMsgFormatter formatter)
            : this(stream, name)
        {
            this.Formatter = formatter;
        }

        /// <summary>
        /// Initializes a new named instance of <see cref="FormattedTextWriterTraceListener"/> with a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="name">The name.</param>
        public FormattedTextWriterTraceListener(Stream stream, string name)
            : base(stream, name)
        { }

        /// <summary>
        /// Initializes a new named instance of <see cref="FormattedTextWriterTraceListener"/> with a 
        /// <see cref="IMsgFormatter"/> and a <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="name">The name.</param>
        /// <param name="formatter">The formatter to format the messages.</param>
        public FormattedTextWriterTraceListener(TextWriter writer, string name, IMsgFormatter formatter)
            : this(writer, name)
        {
            this.Formatter = formatter;
        }

        /// <summary>
        /// Initializes a new named instance of <see cref="FormattedTextWriterTraceListener"/> with a 
        /// <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="name">The name.</param>
        public FormattedTextWriterTraceListener(TextWriter writer, string name)
            : base(writer, name)
        { }

        /// <summary>
        /// Initializes a new named instance of <see cref="FormattedTextWriterTraceListener"/> with a 
        /// <see cref="IMsgFormatter"/> and a file name.
        /// </summary>
        /// <param name="fileName">The file name to write to.</param>
        /// <param name="name">The name.</param>
        /// <param name="formatter">The formatter to format the messages.</param>
        public FormattedTextWriterTraceListener(string fileName, string name, IMsgFormatter formatter)
            : this(fileName, name)
        {
            this.Formatter = formatter;
        }

        /// <summary>
        /// Initializes a new named instance of <see cref="FormattedTextWriterTraceListener"/> with a 
        /// <see cref="IMsgFormatter"/> and a file name.
        /// </summary>
        /// <param name="fileName">The file name to write to.</param>
        /// <param name="name">The name.</param>
        public FormattedTextWriterTraceListener(string fileName, string name)
            : base(RootFileNameAndEnsureTargetFolderExists(fileName), name)
        { }

        /// <summary>
        /// Intercepts the tracing request to format the object to trace.
        /// </summary>
        /// <remarks>
        /// Formatting is only performed if the object to trace is a <see cref="MsgEntry"/> and the formatter is set.
        /// </remarks>
        /// <param name="eventCache">The context information.</param>
        /// <param name="source">The trace source.</param>
        /// <param name="eventType">The severity.</param>
        /// <param name="id">The event id.</param>
        /// <param name="data">The object to trace.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                if (data is MsgEntry)
                {
                    if (this.Formatter != null)
                    {
                        base.Write(this.Formatter.Format(data as MsgEntry));
                    }
                    else
                    {
                        base.TraceData(eventCache, source, eventType, id, data);
                    }
                }
                else
                {
                    base.TraceData(eventCache, source, eventType, id, data);
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="IMsgFormatter"/> used to format the trace messages.
        /// </summary>
        public IMsgFormatter Formatter
        {
            get
            {
                return this.formatter;
            }

            set
            {
                this.formatter = value;
            }
        }

        /// <summary>
        /// Declares "formatter" as a supported attribute name.
        /// </summary>
        /// <returns></returns>
        protected override string[] GetSupportedAttributes()
        {
            return new string[1] { "formatter" };
        }

        private static string RootFileNameAndEnsureTargetFolderExists(string fileName)
        {
            string rootedFileName = fileName;
            if (!Path.IsPathRooted(rootedFileName))
            {
                rootedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rootedFileName);
            }

            string directory = Path.GetDirectoryName(rootedFileName);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return rootedFileName;
        }
    }
}
