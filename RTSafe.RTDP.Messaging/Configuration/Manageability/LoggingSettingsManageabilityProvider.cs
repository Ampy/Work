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
using System.Globalization;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;
using RTSafe.RTDP.Messaging.Configuration.Manageability.Properties;

namespace RTSafe.RTDP.Messaging.Configuration.Manageability
{
    /// <summary>
    /// Represents the behavior required to provide Group Policy updates for a <see cref="MessagingSettings"/> instance
    /// and its internal configuration elements.
    /// </summary>
    public class LoggingSettingsManageabilityProvider
        : ConfigurationSectionManageabilityProviderBase<MessagingSettings>
    {
        /// <summary>
        /// The name of the default category property.
        /// </summary>
        public const String DefaultCategoryPropertyName = "defaultCategory";

        /// <summary>
        /// The name of the tracing enabled property.
        /// </summary>
        public const String TracingEnabledPropertyName = "tracingEnabled";

        /// <summary>
        /// The name of the log warning on no match property.
        /// </summary>
        public const String LogWarningOnNoMatchPropertyName = "logWarningsWhenNoCategoriesMatch";

        /// <summary>
        /// The name of the revert impersonation property.
        /// </summary>
        public const String RevertImpersonationPropertyName = "revertImpersonation";

        /// <summary>
        /// The name of the category sources property.
        /// </summary>
        public const String CategorySourcesKeyName = "categorySources";

        /// <summary>
        /// The name of the special sources property.
        /// </summary>
        public const String SpecialSourcesKeyName = "specialSources";

        /// <summary>
        /// The name of the all events property.
        /// </summary>
        public const String SpecialSourcesAllEventsKeyName = "allEvents";

        /// <summary>
        /// The name of the sources errors property.
        /// </summary>
        public const String SpecialSourcesErrorsKeyName = "errors";

        /// <summary>
        /// The name of the special sources not processed key property.
        /// </summary>
        public const String SpecialSourcesNotProcessedKeyName = "notProcessed";

        /// <summary>
        /// The name of the source default level property.
        /// </summary>
        public const String SourceDefaultLevelPropertyName = "switchValue";

        /// <summary>
        /// The name of the auto flush level property.
        /// </summary>
        public const String SourceAutoFlushPropertyName = "autoFlush";

        /// <summary>
        /// The name of the source trace listerners property.
        /// </summary>
        public const String SourceTraceListenersPropertyName = "listeners";

        /// <summary>
        /// The name of the source kind category property.
        /// </summary>
        public const String SourceKindCategory = "Category";

        /// <summary>
        /// The name of the source kind all events property.
        /// </summary>
        public const String SourceKindAllEvents = "All events";

        /// <summary>
        /// The name of the source kind errors property.
        /// </summary>
        public const String SourceKindErrors = "Errors";

        /// <summary>
        /// The name of the source kind not processed property.
        /// </summary>
        public const String SourceKindNotProcessed = "Not processed";

        /// <summary>
        /// The name of the log filters property.
        /// </summary>
        public const String LogFiltersKeyName = "logFilters";

        /// <summary>
        /// The name of the log formatters property.
        /// </summary>
        public const String LogFormattersKeyName = "formatters";

        /// <summary>
        /// The name of the trace listeners property.
        /// </summary>
        public const String TraceListenersKeyName = "listeners";

        /// <summary>
        /// <para>This method supports the Enterprise Library Manageability Extensions infrastructure and is not intended to 
        /// be used directly from your code.</para>
        /// Initializes a new instance of the <see cref="LoggingSettingsManageabilityProvider"/> class with a 
        /// given set of manageability providers to use when dealing with the configuration for filters, formatters and trace listeners.
        /// </summary>
        /// <param name="subProviders">The mapping from configuration element type to
        /// <see cref="ConfigurationElementManageabilityProvider"/>.</param>
        public LoggingSettingsManageabilityProvider(IDictionary<Type, ConfigurationElementManageabilityProvider> subProviders)
            : base(subProviders)
        { }

        #region ADM generation

        /// <summary>
        /// Adds the ADM instructions that describe the policies that can be used to override the configuration
        /// information represented by a configuration section.
        /// </summary>
        /// <param name="contentBuilder">The <see cref="AdmContentBuilder"/> to which the Adm instructions are to be appended.</param>
        /// <param name="configurationSection">The configuration section instance.</param>
        /// <param name="configurationSource">The configuration source from where to get additional configuration
        /// information, if necessary.</param>
        /// <param name="sectionKey">The root key for the section's policies.</param>
        protected override void AddAdministrativeTemplateDirectives(
            AdmContentBuilder contentBuilder,
            MessagingSettings configurationSection,
            IConfigurationSource configurationSource,
            String sectionKey)
        {
            AddBlockSettingsPolicy(contentBuilder, sectionKey, configurationSection);
            AddCategorySourcesPolicies(contentBuilder, sectionKey, configurationSection);
            AddSpecialSourcesPolicies(contentBuilder, sectionKey, configurationSection);
            AddElementsPolicies<MsgFilterData>(contentBuilder,
                configurationSection.LogFilters,
                configurationSource,
                sectionKey + @"\" + LogFiltersKeyName,
                Resources.LogFiltersCategoryName);
            AddElementsPolicies<FormatterData>(contentBuilder,
                configurationSection.Formatters,
                configurationSource,
                sectionKey + @"\" + LogFormattersKeyName,
                Resources.LogFormattersCategoryName);
            AddElementsPolicies<TraceListenerData>(contentBuilder,
                configurationSection.TraceListeners,
                configurationSource,
                sectionKey + @"\" + TraceListenersKeyName,
                Resources.TraceListenersCategoryName);
        }

        /// <summary>
        /// Gets the name of the category that represents the whole configuration section.
        /// </summary>
        protected override string SectionCategoryName
        {
            get { return Resources.LoggingSectionCategoryName; }
        }

        /// <summary>
        /// Gets the name of the managed configuration section.
        /// </summary>
        protected override string SectionName
        {
            get { return MessagingSettings.SectionName; }
        }

        private static void AddBlockSettingsPolicy(
            AdmContentBuilder contentBuilder,
            String sectionKey,
            MessagingSettings configurationSection)
        {
            contentBuilder.StartPolicy(Resources.LoggingSettingsPolicyName, sectionKey);
            {
                contentBuilder.AddDropDownListPartForNamedElementCollection<TraceSourceData>(
                    Resources.LoggingSettingsDefaultCategoryPartName,
                    DefaultCategoryPropertyName,
                    configurationSection.TraceSources,
                    configurationSection.DefaultCategory,
                    false);

                contentBuilder.AddCheckboxPart(Resources.LoggingSettingsLogWarningPartName,
                    LogWarningOnNoMatchPropertyName,
                    configurationSection.LogWarningWhenNoCategoriesMatch);

                contentBuilder.AddCheckboxPart(Resources.LoggingSettingsEnableTracingPartName,
                    TracingEnabledPropertyName,
                    configurationSection.TracingEnabled);

                contentBuilder.AddCheckboxPart(Resources.LoggingSettingsRevertImpersonationPartName,
                    RevertImpersonationPropertyName,
                    configurationSection.RevertImpersonation);
            }
            contentBuilder.EndPolicy();
        }

        private static void AddCategorySourcesPolicies(
            AdmContentBuilder contentBuilder,
            String sectionKey,
            MessagingSettings configurationSection)
        {
            String traceSourcesKey = sectionKey + @"\" + CategorySourcesKeyName;

            contentBuilder.StartCategory(Resources.CategorySourcesCategoryName);

            foreach (TraceSourceData traceSourceData in configurationSection.TraceSources)
            {
                AddTraceSourcePolicy(traceSourceData,
                    traceSourceData.Name,
                    traceSourcesKey,
                    contentBuilder,
                    configurationSection);
            }

            contentBuilder.EndCategory();
        }

        private static void AddSpecialSourcesPolicies(
            AdmContentBuilder contentBuilder,
            String sectionKey,
            MessagingSettings configurationSection)
        {
            String specialTraceSourcesKey = sectionKey + @"\" + SpecialSourcesKeyName;

            contentBuilder.StartCategory(Resources.SpecialSourcesCategoryName);

            AddTraceSourcePolicy(configurationSection.SpecialTraceSources.AllEventsTraceSource,
                SpecialSourcesAllEventsKeyName,
                specialTraceSourcesKey,
                contentBuilder,
                configurationSection);

            AddTraceSourcePolicy(configurationSection.SpecialTraceSources.NotProcessedTraceSource,
                SpecialSourcesNotProcessedKeyName,
                specialTraceSourcesKey,
                contentBuilder,
                configurationSection);

            AddTraceSourcePolicy(configurationSection.SpecialTraceSources.ErrorsTraceSource,
                SpecialSourcesErrorsKeyName,
                specialTraceSourcesKey,
                contentBuilder,
                configurationSection);

            contentBuilder.EndCategory();
        }

        private static void AddTraceSourcePolicy(
            TraceSourceData traceSourceData,
            String traceSourceName,
            String parentKey,
            AdmContentBuilder contentBuilder,
            MessagingSettings configurationSection)
        {
            String traceSourceKey = parentKey + @"\" + traceSourceName;

            contentBuilder.StartPolicy(String.Format(CultureInfo.InvariantCulture,
                                                    Resources.TraceSourcePolicyNameTemplate,
                                                    traceSourceName),
                traceSourceKey);
            {
                contentBuilder.AddDropDownListPartForEnumeration<SourceLevels>(Resources.TraceSourceDefaultLevelPartName,
                    SourceDefaultLevelPropertyName,
                    traceSourceData.DefaultLevel);

                contentBuilder.AddCheckboxPart(Resources.TraceSourceAutoFlushPartName,
                    SourceAutoFlushPropertyName,
                    traceSourceData.AutoFlush);

                contentBuilder.AddTextPart(Resources.TraceSourceListenersPartName);

                String traceSourceListenersKey = traceSourceKey + @"\" + SourceTraceListenersPropertyName;
                foreach (TraceListenerData traceListener in configurationSection.TraceListeners)
                {
                    contentBuilder.AddCheckboxPart(traceListener.Name,
                        traceSourceListenersKey,
                        traceListener.Name,
                        traceSourceData.TraceListeners.Contains(traceListener.Name),
                        true,
                        false);
                }
            }
            contentBuilder.EndPolicy();
        }

        #endregion

        #region Manageability support

        /// <summary>
        /// Overrides the <paramref name="configurationSection"/>'s properties with the Group Policy values from 
        /// the registry.
        /// </summary>
        /// <param name="configurationSection">The configuration section that must be managed.</param>
        /// <param name="policyKey">The <see cref="IRegistryKey"/> which holds the Group Policy overrides.</param>
        protected override void OverrideWithGroupPoliciesForConfigurationSection(
            MessagingSettings configurationSection,
            IRegistryKey policyKey)
        {
            String defaultCategoryOverride = policyKey.GetStringValue(DefaultCategoryPropertyName);
            bool? tracingEnabledEnabledOverride = policyKey.GetBoolValue(TracingEnabledPropertyName);
            bool? logWarningWhenNoCategoriesMatchOverride = policyKey.GetBoolValue(LogWarningOnNoMatchPropertyName);
            bool? revertImpersonationOverride = policyKey.GetBoolValue(RevertImpersonationPropertyName);

            configurationSection.DefaultCategory = defaultCategoryOverride;
            configurationSection.TracingEnabled = tracingEnabledEnabledOverride.Value;
            configurationSection.LogWarningWhenNoCategoriesMatch = logWarningWhenNoCategoriesMatchOverride.Value;
            configurationSection.RevertImpersonation = revertImpersonationOverride.Value;
        }

        /// <summary>
        /// Overrides the <paramref name="configurationSection"/>'s configuration elements' properties 
        /// with the Group Policy values from the registry, if any.
        /// </summary>
        /// <param name="configurationSection">The configuration section that must be managed.</param>
        /// <param name="readGroupPolicies"><see langword="true"/> if Group Policy overrides must be applied; otherwise, 
        /// <see langword="false"/>.</param>
        /// <param name="machineKey">The <see cref="IRegistryKey"/> which holds the Group Policy overrides for the 
        /// configuration section at the machine level, or <see langword="null"/> 
        /// if there is no such registry key.</param>
        /// <param name="userKey">The <see cref="IRegistryKey"/> which holds the Group Policy overrides for the 
        /// configuration section at the user level, or <see langword="null"/> 
        /// if there is no such registry key.</param>
        protected override void OverrideWithGroupPoliciesForConfigurationElements(
            MessagingSettings configurationSection,
            bool readGroupPolicies,
            IRegistryKey machineKey,
            IRegistryKey userKey)
        {
            OverrideWithGroupPoliciesForCategoryTraceSources(configurationSection,
                readGroupPolicies, machineKey, userKey);
            OverrideWithGroupPoliciesForSpecialTraceSources(configurationSection,
                readGroupPolicies, machineKey, userKey);
            OverrideWithGroupPoliciesForElementCollection(configurationSection.LogFilters,
                LogFiltersKeyName,
                readGroupPolicies, machineKey, userKey);
            OverrideWithGroupPoliciesForElementCollection(configurationSection.Formatters,
                LogFormattersKeyName,
                readGroupPolicies, machineKey, userKey);
            OverrideWithGroupPoliciesForElementCollection(configurationSection.TraceListeners,
                TraceListenersKeyName,
                readGroupPolicies, machineKey, userKey);
        }

        private void OverrideWithGroupPoliciesForCategoryTraceSources(
            MessagingSettings configurationSection,
            bool readGroupPolicies,
            IRegistryKey machineKey,
            IRegistryKey userKey)
        {
            List<TraceSourceData> elementsToRemove = new List<TraceSourceData>();

            IRegistryKey machineSourcesKey = null;
            IRegistryKey userSourcesKey = null;

            try
            {
                LoadRegistrySubKeys(CategorySourcesKeyName,
                    machineKey, userKey,
                    out machineSourcesKey, out userSourcesKey);

                foreach (TraceSourceData traceSourceData in configurationSection.TraceSources)
                {
                    IRegistryKey machineSourceKey = null;
                    IRegistryKey userSourceKey = null;

                    try
                    {
                        LoadRegistrySubKeys(traceSourceData.Name,
                            machineSourcesKey, userSourcesKey,
                            out machineSourceKey, out userSourceKey);

                        if (!OverrideWithGroupPoliciesForTraceSource(traceSourceData,
                                readGroupPolicies, machineSourceKey, userSourceKey,
                                SourceKindCategory))
                        {
                            elementsToRemove.Add(traceSourceData);
                        }
                    }
                    finally
                    {
                        ReleaseRegistryKeys(machineSourceKey, userSourceKey);
                    }
                }
            }
            finally
            {
                ReleaseRegistryKeys(machineSourcesKey, userSourcesKey);
            }

            // remove disabled elements
            foreach (TraceSourceData element in elementsToRemove)
            {
                configurationSection.TraceSources.Remove(element.Name);
            }
        }

        private void OverrideWithGroupPoliciesForSpecialTraceSources(
            MessagingSettings configurationSection,
            bool readGroupPolicies,
            IRegistryKey machineKey,
            IRegistryKey userKey)
        {
            IRegistryKey machineSpecialSourcesKey = null;
            IRegistryKey userSpecialSourcesKey = null;

            try
            {
                LoadRegistrySubKeys(SpecialSourcesKeyName,
                    machineKey, userKey,
                    out machineSpecialSourcesKey, out userSpecialSourcesKey);

                if (configurationSection.SpecialTraceSources.AllEventsTraceSource != null)
                {
                    OverrideWithGroupPoliciesForSpecialTraceSource(SpecialSourcesAllEventsKeyName,
                        configurationSection.SpecialTraceSources.AllEventsTraceSource,
                        readGroupPolicies, machineSpecialSourcesKey, userSpecialSourcesKey,
                        SourceKindAllEvents);
                }
                if (configurationSection.SpecialTraceSources.NotProcessedTraceSource != null)
                {
                    OverrideWithGroupPoliciesForSpecialTraceSource(SpecialSourcesNotProcessedKeyName,
                        configurationSection.SpecialTraceSources.NotProcessedTraceSource,
                        readGroupPolicies, machineSpecialSourcesKey, userSpecialSourcesKey,
                        SourceKindNotProcessed);
                }
                if (configurationSection.SpecialTraceSources.ErrorsTraceSource != null)
                {
                    OverrideWithGroupPoliciesForSpecialTraceSource(SpecialSourcesErrorsKeyName,
                        configurationSection.SpecialTraceSources.ErrorsTraceSource,
                        readGroupPolicies, machineSpecialSourcesKey, userSpecialSourcesKey,
                        SourceKindErrors);
                }
            }
            finally
            {
                ReleaseRegistryKeys(machineSpecialSourcesKey, userSpecialSourcesKey);
            }
        }

        private void OverrideWithGroupPoliciesForSpecialTraceSource(
            String sourceName,
            TraceSourceData traceSourceData,
            bool readGroupPolicies,
            IRegistryKey machineParentKey,
            IRegistryKey userParentKey,
            String sourceKind)
        {
            if (readGroupPolicies)
            {
                IRegistryKey machineSourceKey = null;
                IRegistryKey userSourceKey = null;

                try
                {
                    LoadRegistrySubKeys(sourceName,
                        machineParentKey, userParentKey,
                        out machineSourceKey, out userSourceKey);

                    if (!OverrideWithGroupPoliciesForTraceSource(traceSourceData,
                            readGroupPolicies, machineSourceKey, userSourceKey,
                            sourceKind))
                    {
                        // "special" trace sources cannot be removed because they aren't elements in collections
                        // but a trace source is considered disabled if it has no trace listeners
                        traceSourceData.TraceListeners.Clear();
                    }
                }
                finally
                {
                    ReleaseRegistryKeys(machineSourceKey, userSourceKey);
                }
            }
        }

        private bool OverrideWithGroupPoliciesForTraceSource(
            TraceSourceData traceSourceData,
            bool readGroupPolicies,
            IRegistryKey machineKey,
            IRegistryKey userKey,
            String sourceKind)
        {
            if (readGroupPolicies)
            {
                IRegistryKey policyKey = machineKey != null ? machineKey : userKey;
                if (policyKey != null)
                {
                    if (policyKey.IsPolicyKey && !policyKey.GetBoolValue(PolicyValueName).Value)
                    {
                        return false;
                    }
                    try
                    {
                        SourceLevels? defaultLevelOverride = policyKey.GetEnumValue<SourceLevels>(SourceDefaultLevelPropertyName);
                        bool? autoFlushOverride = policyKey.GetBoolValue(SourceAutoFlushPropertyName);

                        // the key where the values for the source listeners are stored
                        // might not exist if no listener is selected
                        traceSourceData.TraceListeners.Clear();
                        using (IRegistryKey listenersOverrideKey = policyKey.OpenSubKey(SourceTraceListenersPropertyName))
                        {
                            if (listenersOverrideKey != null)
                            {
                                foreach (String valueName in listenersOverrideKey.GetValueNames())
                                {
                                    traceSourceData.TraceListeners.Add(new TraceListenerReferenceData(valueName));
                                }
                            }
                        }
                        traceSourceData.DefaultLevel = defaultLevelOverride.Value;
                        traceSourceData.AutoFlush = autoFlushOverride.Value;
                    }
                    catch (RegistryAccessException ex)
                    {
                        LogExceptionWhileOverriding(ex);
                    }
                }
            }

            return true;
        }

        #endregion
    }
}
