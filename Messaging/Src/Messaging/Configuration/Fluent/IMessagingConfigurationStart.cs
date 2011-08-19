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
    /// Fluent interface that allows global logging settings to be configured.
    /// </summary>
    public interface ILoggingConfigurationStart : ILoggingConfigurationContd, IFluentInterface
    {
        /// <summary>
        /// Returns an fluent interface that can be used to further configure logging settings.
        /// </summary>
        ILoggingConfigurationOptions WithOptions { get; }

    }
}
