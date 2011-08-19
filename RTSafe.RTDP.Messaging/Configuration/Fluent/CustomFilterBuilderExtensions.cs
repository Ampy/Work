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
using RTSafe.RTDP.Messaging.Filters;
using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
using System.Globalization;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    /// <summary>
    /// <see cref="ILoggingConfigurationOptions"/> extensions to configure custom <see cref="IMsgFilter"/> instances.
    /// </summary>
    /// <seealso cref="IMsgFilter"/>
    /// <seealso cref="CustomLogFilterData"/>
    public static class CustomFilterBuilderExtensions
    {
        /// <summary>
        /// Adds an custom <see cref="IMsgFilter"/> instance of type <typeparamref name="TCustomFilter"/> to the logging configuration.
        /// </summary>
        /// <typeparam name="TCustomFilter">Concrete type of the custom <see cref="IMsgFilter"/> instance.</typeparam>
        /// <param name="context">Fluent interface extension point.</param>
        /// <param name="customFilterName">Name of the <see cref="IMsgFilter"/> instance added to configuration.</param>
        /// <seealso cref="IMsgFilter"/>
        /// <seealso cref="CustomLogFilterData"/>
        public static ILoggingConfigurationOptions FilterCustom<TCustomFilter>(this ILoggingConfigurationOptions context, string customFilterName)
            where TCustomFilter : IMsgFilter
        {
            return FilterCustom(context, customFilterName, typeof(TCustomFilter), new NameValueCollection());
        }

        /// <summary>
        /// Adds an custom <see cref="IMsgFilter"/> instance of type <paramref name="customFilterType"/> to the logging configuration.
        /// </summary>
        /// <param name="context">Fluent interface extension point.</param>
        /// <param name="customFilterName">Name of the <see cref="IMsgFilter"/> instance added to configuration.</param>
        /// <param name="customFilterType">Concrete type of the custom <see cref="IMsgFilter"/> instance.</param>
        /// <seealso cref="IMsgFilter"/>
        /// <seealso cref="CustomLogFilterData"/>
        public static ILoggingConfigurationOptions FilterCustom(this ILoggingConfigurationOptions context, string customFilterName, Type customFilterType)
        {
            return FilterCustom(context, customFilterName, customFilterType, new NameValueCollection());
        }

        /// <summary>
        /// Adds an custom <see cref="IMsgFilter"/> instance of type <typeparamref name="TCustomFilter"/> to the logging configuration.
        /// </summary>
        /// <typeparam name="TCustomFilter">Concrete type of the custom <see cref="IMsgFilter"/> instance.</typeparam>
        /// <param name="context">Fluent interface extension point.</param>
        /// <param name="customFilterName">Name of the <see cref="IMsgFilter"/> instance added to configuration.</param>
        /// <param name="attributes">Attributes that should be passed to <typeparamref name="TCustomFilter"/> when creating an instance.</param>
        /// <seealso cref="IMsgFilter"/>
        /// <seealso cref="CustomLogFilterData"/>
        public static ILoggingConfigurationOptions FilterCustom<TCustomFilter>(this ILoggingConfigurationOptions context, string customFilterName, NameValueCollection attributes)
            where TCustomFilter : IMsgFilter
        {
            return FilterCustom(context, customFilterName, typeof(TCustomFilter), attributes);
        }

        /// <summary>
        /// Adds an custom <see cref="IMsgFilter"/> instance of type <paramref name="customFilterType"/> to the logging configuration.
        /// </summary>
        /// <param name="context">Fluent interface extension point.</param>
        /// <param name="customFilterName">Name of the <see cref="IMsgFilter"/> instance added to configuration.</param>
        /// <param name="customFilterType">Concrete type of the custom <see cref="IMsgFilter"/> instance.</param>
        /// <param name="attributes">Attributes that should be passed to <paramref name="customFilterType"/> when creating an instance.</param>
        /// <seealso cref="IMsgFilter"/>
        /// <seealso cref="CustomLogFilterData"/>
        public static ILoggingConfigurationOptions FilterCustom(this ILoggingConfigurationOptions context, string customFilterName, Type customFilterType, NameValueCollection attributes)
        {
            if (string.IsNullOrEmpty(customFilterName))
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "customFilterName");

            if (customFilterType == null)
                throw new ArgumentNullException("customFilterType");

            if (attributes == null)
                throw new ArgumentNullException("attributes");

            if (!typeof(IMsgFilter).IsAssignableFrom(customFilterType))
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                    Resources.ExceptionTypeMustImplementInterface, typeof(IMsgFilter)), "customFilterType");

            var builder = new FilterCustomBuilder(context, customFilterName, customFilterType, attributes);
            return context;
        }

        private class FilterCustomBuilder : LoggingConfigurationExtension
        {
            public FilterCustomBuilder(ILoggingConfigurationOptions context, string logFilterName, Type customFilterType, NameValueCollection attributes)
                : base(context)
            {
                CustomLogFilterData customFilter = new CustomLogFilterData
                {
                    Name = logFilterName,
                    Type = customFilterType
                };
                customFilter.Attributes.Add(attributes);

                base.LoggingSettings.LogFilters.Add(customFilter);
            }
        }
    }
}
