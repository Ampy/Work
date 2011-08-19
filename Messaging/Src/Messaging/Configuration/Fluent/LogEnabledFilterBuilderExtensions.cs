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
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent;
using Microsoft.Practices.EnterpriseLibrary.Common.Properties;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    /// <summary/>
    public static class LogEnabledFilterBuilderExtensions
    {
        /// <summary/>
        public static ILoggingConfigurationFilterLogEnabled FilterEnableOrDisable(this ILoggingConfigurationOptions context, string logEnabledFilterName)
        {
            if (string.IsNullOrEmpty(logEnabledFilterName)) 
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "logEnabledFilterName");

            return new FilterLogEnabledBuilder(context, logEnabledFilterName);
        }

        private class FilterLogEnabledBuilder : LoggingConfigurationExtension, ILoggingConfigurationFilterLogEnabled
        {
            MsgEnabledFilterData logEnabledFilterData;

            public FilterLogEnabledBuilder(ILoggingConfigurationOptions context, string logEnabledFilterName)
                :base(context)
            {
                logEnabledFilterData = new MsgEnabledFilterData()
                {
                    Name = logEnabledFilterName
                };

                base.LoggingSettings.LogFilters.Add(logEnabledFilterData);
            }

            public ILoggingConfigurationOptions Enable()
            {
                logEnabledFilterData.Enabled = true;

                return base.LoggingOptions;
            }
        }
    }
}
