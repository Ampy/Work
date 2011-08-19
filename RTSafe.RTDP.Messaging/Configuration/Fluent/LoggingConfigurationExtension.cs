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
using RTSafe.RTDP.Messaging.Properties;
using System.Globalization;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent
{
    /// <summary/>
    public abstract class LoggingConfigurationExtension : ILoggingConfigurationOptions, ILoggingConfigurationExtension
    {
        ILoggingConfigurationExtension contextExtension;
        
        /// <summary/>
        protected LoggingConfigurationExtension(ILoggingConfigurationContd context)
        {
            contextExtension = context as ILoggingConfigurationExtension;

            if (contextExtension == null) throw new ArgumentException(
                string.Format(CultureInfo.CurrentCulture, Resources.ParameterMustImplementType, typeof(ILoggingConfigurationExtension)),
                "context");
        }

        /// <summary/>
        protected MessagingSettings LoggingSettings
        {
            get { return contextExtension.LoggingSettings; }
        }

        /// <summary/>
        protected ILoggingConfigurationOptions LoggingOptions
        {
            get { return contextExtension.LoggingOptions; }
        }


        ILoggingConfigurationOptions ILoggingConfigurationOptions.DisableTracing()
        {
            return LoggingOptions.DisableTracing();
        }

        ILoggingConfigurationOptions ILoggingConfigurationOptions.DoNotRevertImpersonation()
        {
            return LoggingOptions.DoNotRevertImpersonation();
        }

        ILoggingConfigurationOptions ILoggingConfigurationOptions.DoNotLogWarningsWhenNoCategoryExists()
        {
            return LoggingOptions.DoNotLogWarningsWhenNoCategoryExists();
        }

        ILoggingConfigurationCustomCategoryStart ILoggingConfigurationContd.LogToCategoryNamed(string categoryName)
        {
            return LoggingOptions.LogToCategoryNamed(categoryName);
        }

        ILoggingConfigurationSpecialSources ILoggingConfigurationContd.SpecialSources
        {
            get { return LoggingOptions.SpecialSources; }
        }


        ILoggingConfigurationOptions ILoggingConfigurationExtension.LoggingOptions
        {
            get { return contextExtension.LoggingOptions; }
        }

        MessagingSettings ILoggingConfigurationExtension.LoggingSettings
        {
            get { return contextExtension.LoggingSettings ; }
        }
    }
}
