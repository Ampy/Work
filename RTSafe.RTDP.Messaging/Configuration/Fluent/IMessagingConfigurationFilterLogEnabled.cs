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

using RTSafe.RTDP.Messaging;
using RTSafe.RTDP.Messaging.Filters;
using RTSafe.RTDP.Messaging.Configuration;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent
{
    /// <summary>
    /// Fluent interface used to configure a <see cref="MsgEnabledFilter"/> instance.
    /// </summary>
    /// <see cref="MsgEnabledFilter"/>
    /// <see cref="MsgEnabledFilterData"/>
    public interface ILoggingConfigurationFilterLogEnabled : ILoggingConfigurationOptions, IFluentInterface
    {
        /// <summary>
        /// Specifies that all logging should be enabled. <br/>
        /// The default for the <see cref="MsgEnabledFilter"/> is that all logging is disabled.
        /// </summary>
        /// <returns>Fluent interface used to further configure the logging application block.</returns>
        /// <see cref="MsgEnabledFilter"/>
        /// <see cref="MsgEnabledFilterData"/>
        ILoggingConfigurationOptions Enable();
    }
}
