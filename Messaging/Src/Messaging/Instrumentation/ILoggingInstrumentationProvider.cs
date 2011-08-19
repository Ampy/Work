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

namespace RTSafe.RTDP.Messaging.Instrumentation
{
    /// <summary/>
    public interface ILoggingInstrumentationProvider
    {
        /// <summary/>
        void FireLockAcquisitionError(string message);

        /// <summary/>
        void FireConfigurationFailureEvent(Exception configurationException);

        /// <summary/>
        void FireFailureLoggingErrorEvent(string message, Exception exception);

        /// <summary/>
        void FireLogEventRaised();

        /// <summary/>
        void FireTraceListenerEntryWrittenEvent();

        /// <summary/>
        void FireReconfigurationErrorEvent(Exception exception);
    }
}
