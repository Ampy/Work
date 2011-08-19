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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTSafe.RTDP.Messaging.Configuration;
using System.Diagnostics;
using System.Messaging;
using RTSafe.RTDP.Messaging.TraceListeners;
using System.Collections.Specialized;
using RTSafe.RTDP.Messaging.Formatters;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent
{
    /// <summary>
    /// Allows access to the internal configuration classes used to configure the logging application block.
    /// </summary>
    public interface ILoggingConfigurationExtension : IFluentInterface
    {
        /// <summary>
        /// Returns a fluent interface that can be used to configure global logging application block settings.
        /// </summary>
        ILoggingConfigurationOptions LoggingOptions { get; }

        /// <summary>
        /// Returns the <see cref="LoggingSettings"/> that are being build up.
        /// </summary>
        MessagingSettings LoggingSettings { get; }
    }
}
