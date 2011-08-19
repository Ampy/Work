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
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation.Configuration;
using RTSafe.RTDP.Messaging.Filters;
using RTSafe.RTDP.Messaging.Instrumentation;
using Container = Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel.Container;

namespace RTSafe.RTDP.Messaging.Configuration
{
    /// <summary>
    /// Configuration settings for client-side logging applications.
    /// </summary>
    [ViewModel(MessagingDesignTime.ViewModelTypeNames.LogggingSectionViewModel)]
    [ResourceDescription(typeof(DesignResources), "LoggingSettingsDescription")]
    [ResourceDisplayName(typeof(DesignResources), "LoggingSettingsDisplayName")]
    public class MessagingSettings : SerializableConfigurationSection, ITypeRegistrationsProvider
    {
        private const string ErrorsTraceSourceKey = "___ERRORS";
        private const string AllTraceSourceKey = "___ALL";
        private const string NoMatchesTraceSourceKey = "___NO_MATCHES";

        private const string tracingEnabledProperty = "tracingEnabled";
        private const string nameProperty = "name";
        private const string traceListenerDataCollectionProperty = "listeners";
        private const string formatterDataCollectionProperty = "formatters";
        private const string logFiltersProperty = "msgFilters";
        private const string traceSourcesProrperty = "categorySources";
        private const string defaultCategoryProperty = "defaultCategory";
        private const string logWarningsWhenNoCategoriesMatchProperty = "msgWarningsWhenNoCategoriesMatch";
        private const string specialTraceSourcesProperty = "specialSources";
        private const string revertImpersonationProperty = "revertImpersonation";

        /// <summary>
        /// Configuration section name for logging client settings.
        /// </summary>
        public const string SectionName = "messagingConfiguration";//BlockSectionNames.Logging;

        /// <summary>
        /// Initialize a new instance of the <see cref="MessagingSettings"/> with default values.
        /// </summary>
        public MessagingSettings()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="MessagingSettings"/> using the given name.
        /// </summary>
        /// <param name="name">The name to use for this instance</param>
        public MessagingSettings(string name)
            : this(name, true, string.Empty)
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="MessagingSettings"/> using the given values.
        /// </summary>
        /// <param name="name">The name to use for this instance</param>
        /// <param name="tracingEnabled">Should tracing be enabled?</param>
        /// <param name="defaultCategory">The default category to use.</param>
        public MessagingSettings(string name, bool tracingEnabled, string defaultCategory)
        {
            Name = name;
            TracingEnabled = tracingEnabled;
            DefaultCategory = defaultCategory;
        }

        /// <summary>
        /// Retrieves the <see cref="MessagingSettings"/> section from the configuration source.
        /// </summary>
        /// <param name="configurationSource">The <see cref="IConfigurationSource"/> to get the section from.</param>
        /// <returns>The logging section.</returns>
        public static MessagingSettings GetLoggingSettings(IConfigurationSource configurationSource)
        {
            if(configurationSource == null) throw new ArgumentNullException("configurationSource");
            return (MessagingSettings)configurationSource.GetSection(SectionName);
        }

        /// <summary>
        /// Enable or disable trace logging.
        /// </summary>
        [ConfigurationProperty(tracingEnabledProperty, DefaultValue = true)]
        [ResourceDescription(typeof(DesignResources), "LoggingSettingsTracingEnabledDescription")]
        [ResourceDisplayName(typeof(DesignResources), "LoggingSettingsTracingEnabledDisplayName")]
        public bool TracingEnabled
        {
            get
            {
                return (bool)this[tracingEnabledProperty];
            }
            set
            {
                this[tracingEnabledProperty] = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the configuration node.
        /// </summary>
        [ConfigurationProperty(nameProperty)]
        [Browsable(false)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public string Name
        {
            get
            {
                return (string)this[nameProperty];
            }
            set
            {
                this[nameProperty] = value;
            }
        }

        /// <summary>
        /// Gets or sets the default logging category.
        /// </summary>
        [ConfigurationProperty(defaultCategoryProperty, IsRequired = true)]
        [Reference(typeof(NamedElementCollection<TraceSourceData>), typeof(TraceSourceData))]
        [ResourceDescription(typeof(DesignResources), "LoggingSettingsDefaultCategoryDescription")]
        [ResourceDisplayName(typeof(DesignResources), "LoggingSettingsDefaultCategoryDisplayName")]
        public string DefaultCategory
        {
            get
            {
                return (string)this[defaultCategoryProperty];
            }
            set
            {
                this[defaultCategoryProperty] = value;
            }
        }

        /// <summary>
        /// Gets the collection of <see cref="TraceListenerData"/> configuration elements that define 
        /// the available <see cref="System.Diagnostics.TraceListener"/>s.
        /// </summary>
        [ConfigurationProperty(traceListenerDataCollectionProperty)]
        [ViewModel(MessagingDesignTime.ViewModelTypeNames.TraceListenerElementCollectionViewModel)]
        [ConfigurationCollection(typeof(TraceListenerData))]
        [ResourceDescription(typeof(DesignResources), "LoggingSettingsTraceListenersDescription")]
        [ResourceDisplayName(typeof(DesignResources), "LoggingSettingsTraceListenersDisplayName")]
        public TraceListenerDataCollection TraceListeners
        {
            get
            {
                return (TraceListenerDataCollection)base[traceListenerDataCollectionProperty];
            }
        }

        /// <summary>
        /// Gets the collection of <see cref="FormatterData"/> configuration elements that define 
        /// the available <see cref="RTSafe.RTDP.Messaging.Formatters.IMsgFormatter"/>s.
        /// </summary>
        [ConfigurationProperty(formatterDataCollectionProperty)]
        [ConfigurationCollection(typeof(FormatterData))]
        [ResourceDescription(typeof(DesignResources), "LoggingSettingsFormattersDescription")]
        [ResourceDisplayName(typeof(DesignResources), "LoggingSettingsFormattersDisplayName")]
        public NameTypeConfigurationElementCollection<FormatterData, CustomFormatterData> Formatters
        {
            get
            {
                return (NameTypeConfigurationElementCollection<FormatterData, CustomFormatterData>)base[formatterDataCollectionProperty];
            }
        }

        /// <summary>
        /// Gets the collection of <see cref="MsgFilterData"/> configuration elements that define 
        /// the available <see cref="RTSafe.RTDP.Messaging.Filters.IMsgFilter"/>s.
        /// </summary>
        [ConfigurationProperty(logFiltersProperty)]
        [ConfigurationCollection(typeof(MsgFilterData))]
        [ResourceDescription(typeof(DesignResources), "LoggingSettingsLogFiltersDescription")]
        [ResourceDisplayName(typeof(DesignResources), "LoggingSettingsLogFiltersDisplayName")]
        public NameTypeConfigurationElementCollection<MsgFilterData, CustomLogFilterData> LogFilters
        {
            get
            {
                return (NameTypeConfigurationElementCollection<MsgFilterData, CustomLogFilterData>)base[logFiltersProperty];
            }
        }

        /// <summary>
        /// Gets the collection of <see cref="TraceSourceData"/> configuration elements that define 
        /// the available <see cref="MsgSource"/>s.
        /// </summary>
        [ConfigurationProperty(traceSourcesProrperty)]
        [ConfigurationCollection(typeof(TraceSourceData))]
        [ResourceDescription(typeof(DesignResources), "LoggingSettingsTraceSourcesDescription")]
        [ResourceDisplayName(typeof(DesignResources), "LoggingSettingsTraceSourcesDisplayName")]
        public NamedElementCollection<TraceSourceData> TraceSources
        {
            get
            {
                return (NamedElementCollection<TraceSourceData>)base[traceSourcesProrperty];
            }
        }

        /// <summary>
        /// Gets or sets the configuration elements that define the distinguished <see cref="MsgSource"/>s: 
        /// for all events. for missing categories, and for errors and warnings.
        /// </summary>
        [ConfigurationProperty(specialTraceSourcesProperty, IsRequired = true)]
        [ResourceDescription(typeof(DesignResources), "LoggingSettingsSpecialTraceSourcesDescription")]
        [ResourceDisplayName(typeof(DesignResources), "LoggingSettingsSpecialTraceSourcesDisplayName")]
        public SpecialTraceSourcesData SpecialTraceSources
        {
            get
            {
                return (SpecialTraceSourcesData)base[specialTraceSourcesProperty];
            }
            set
            {
                base[specialTraceSourcesProperty] = value;
            }
        }

        /// <summary>
        /// Gets or sets the indication that a warning should be logged when a category is not found while 
        /// dispatching a log entry.
        /// </summary>
        [ConfigurationProperty(logWarningsWhenNoCategoriesMatchProperty, DefaultValue = true)]
        [ResourceDescription(typeof(DesignResources), "LoggingSettingsLogWarningWhenNoCategoriesMatchDescription")]
        [ResourceDisplayName(typeof(DesignResources), "LoggingSettingsLogWarningWhenNoCategoriesMatchDisplayName")]
        public bool LogWarningWhenNoCategoriesMatch
        {
            get
            {
                return (bool)this[logWarningsWhenNoCategoriesMatchProperty];
            }
            set
            {
                this[logWarningsWhenNoCategoriesMatchProperty] = value;
            }
        }

        /// <summary>
        /// Gets or sets the indication that impersonation should be reverted temporarily while logging, if enabled.
        /// </summary>
        [ConfigurationProperty(revertImpersonationProperty, DefaultValue = true, IsRequired = false)]
        [ResourceDescription(typeof(DesignResources), "LoggingSettingsRevertImpersonationDescription")]
        [ResourceDisplayName(typeof(DesignResources), "LoggingSettingsRevertImpersonationDisplayName")]
        public bool RevertImpersonation
        {
            get
            {
                return (bool)this[revertImpersonationProperty];
            }
            set
            {
                this[revertImpersonationProperty] = value;
            }
        }

        /// <summary>
        /// Return the <see cref="TypeRegistration"/> objects needed to configure
        /// the container.
        /// </summary>
        /// <returns>The sequence of <see cref="TypeRegistration"/> objects.</returns>
        public IEnumerable<TypeRegistration> GetRegistrations(IConfigurationSource configurationSource)
        {
            return GetRegistrationsCore(configurationSource);
        }

        /// <summary>
        /// Return the <see cref="TypeRegistration"/> objects needed to reconfigure
        /// the container after a configuration source has changed.
        /// </summary>
        /// <remarks>If there are no reregistrations, return an empty sequence.</remarks>
        /// <param name="configurationSource">The <see cref="IConfigurationSource"/> containing
        /// the configuration information.</param>
        /// <returns>The sequence of <see cref="TypeRegistration"/> objects.</returns>
        public IEnumerable<TypeRegistration> GetUpdatedRegistrations(IConfigurationSource configurationSource)
        {
            return GetRegistrationsCore(configurationSource);
        }

        private IEnumerable<TypeRegistration> GetRegistrationsCore(IConfigurationSource configurationSource)
        {
            var registrations = new List<TypeRegistration>();

            registrations.Add(CreateLoggingInstrumentationProviderRegistration(configurationSource));
            registrations.Add(CreateLoggingUpdateCoordinatorRegistration());
            registrations.Add(CreateLogWriterRegistration());
            registrations.Add(CreateDefaultLoggingEventLoggerRegistration(configurationSource));
            registrations.AddRange(TraceListeners.SelectMany(tld => tld.GetRegistrations()));
            registrations.AddRange(LogFilters.SelectMany(lfd => lfd.GetRegistrations()));
            registrations.AddRange(Formatters.SelectMany(fd => fd.GetRegistrations()));
            registrations.AddRange(TraceSources.Select(tsd => tsd.GetRegistrations()));
            registrations.Add(
                CreateLogSourceRegistration(SpecialTraceSources.AllEventsTraceSource, AllTraceSourceKey));
            registrations.Add(
                CreateLogSourceRegistration(SpecialTraceSources.NotProcessedTraceSource, NoMatchesTraceSourceKey));
            registrations.Add(
                CreateLogSourceRegistration(SpecialTraceSources.ErrorsTraceSource, ErrorsTraceSourceKey));
            registrations.Add(CreateLogWriterStructureHolderRegistration());
            registrations.Add(CreateTraceManagerRegistration());
            registrations.Add(CreateTracerInstrumentationProviderRegistration(configurationSource));

            return registrations;
        }

        private static TypeRegistration CreateLogSourceRegistration(TraceSourceData traceSourceData, string name)
        {
            var registration = traceSourceData.GetRegistrations();
            registration.Name = name;
            return registration;
        }

        private TypeRegistration CreateLogWriterRegistration()
        {
            return
                new TypeRegistration<MsgWriter>(() =>
                   new MsgWriterImpl(
                       Container.Resolved<MsgWriterStructureHolder>(),
                       Container.Resolved<ILoggingInstrumentationProvider>(),
                       Container.Resolved<IMessagingUpdateCoordinator>()))
               {
                   Lifetime = TypeRegistrationLifetime.Singleton,
                   IsDefault = true,
                   IsPublicName = true
               };
        }

        private TypeRegistration CreateLoggingUpdateCoordinatorRegistration()
        {
            return
                new TypeRegistration<IMessagingUpdateCoordinator>(() =>
                   new MessagingUpdateCoordinator(
                       Container.Resolved<ConfigurationChangeEventSource>(),
                       Container.Resolved<ILoggingInstrumentationProvider>()))
               {
                   Lifetime = TypeRegistrationLifetime.Singleton,
                   IsDefault = true
               };
        }

        private static TypeRegistration CreateLoggingInstrumentationProviderRegistration(IConfigurationSource configurationSource)
        {
            var instrumentationSection = InstrumentationConfigurationSection.GetSection(configurationSource);

            return
                new TypeRegistration<ILoggingInstrumentationProvider>(() =>
                    new MessagingInstrumentationProvider(
                        instrumentationSection.PerformanceCountersEnabled,
                        instrumentationSection.EventLoggingEnabled,
                        instrumentationSection.ApplicationInstanceName))
                {
                    Lifetime = TypeRegistrationLifetime.Singleton,
                    IsDefault = true
                };
        }

        private TypeRegistration CreateLogWriterStructureHolderRegistration()
        {
            return
                new TypeRegistration<MsgWriterStructureHolder>(() =>
                    new MsgWriterStructureHolder(
                        Container.ResolvedEnumerable<IMsgFilter>(LogFilters.Select(lfd => lfd.Name)),
                        TraceSources.Select(tsd => tsd.Name).ToArray(),
                        Container.ResolvedEnumerable<MsgSource>(TraceSources.Select(tsd => tsd.Name)),
                        Container.Resolved<MsgSource>(AllTraceSourceKey),
                        Container.Resolved<MsgSource>(NoMatchesTraceSourceKey),
                        Container.Resolved<MsgSource>(ErrorsTraceSourceKey),
                        DefaultCategory,
                        TracingEnabled,
                        LogWarningWhenNoCategoriesMatch,
                        RevertImpersonation))
                {
                    Lifetime = TypeRegistrationLifetime.Transient,
                    IsDefault = true
                };
        }

        private static TypeRegistration CreateTraceManagerRegistration()
        {
            return
                new TypeRegistration<TraceManager>(() =>
                    new TraceManager(
                        Container.Resolved<MsgWriter>(),
                        Container.Resolved<ITracerInstrumentationProvider>()))
                {
                    Lifetime = TypeRegistrationLifetime.Transient,
                    IsDefault = true
                };
        }

        private static TypeRegistration CreateTracerInstrumentationProviderRegistration(IConfigurationSource configurationSource)
        {
            var instrumentationSection = InstrumentationConfigurationSection.GetSection(configurationSource);

            return
                new TypeRegistration<ITracerInstrumentationProvider>(() =>
                    new TracerInstrumentationProvider(
                        instrumentationSection.PerformanceCountersEnabled,
                        instrumentationSection.EventLoggingEnabled,
                        instrumentationSection.ApplicationInstanceName))
                {
                    Lifetime = TypeRegistrationLifetime.Transient,
                    IsDefault = true
                };
        }

        private static TypeRegistration CreateDefaultLoggingEventLoggerRegistration(IConfigurationSource configurationSource)
        {
            var instrumentationSettings = InstrumentationConfigurationSection.GetSection(configurationSource);
            return
                new TypeRegistration<DefaultLoggingEventLogger>(() =>
                    new DefaultLoggingEventLogger(instrumentationSettings.EventLoggingEnabled))
                {
                    Lifetime = TypeRegistrationLifetime.Transient,
                    IsDefault = true
                };
        }
    }
}
