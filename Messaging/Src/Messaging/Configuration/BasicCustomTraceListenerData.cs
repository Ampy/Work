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
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design.Validation;
using RTSafe.RTDP.Messaging.TraceListeners;

namespace RTSafe.RTDP.Messaging.Configuration
{
    /// <summary>
    /// Base configuration object for custom <see cref="System.Diagnostics.TraceListener"/>s.
    /// </summary>
    public abstract class BasicCustomTraceListenerData
        : TraceListenerData, IHelperAssistedCustomConfigurationData<BasicCustomTraceListenerData>
    {
        internal const string initDataProperty = "initializeData";
        private const string AttributeWrappedPrefix = "\u200Cwrapped";

        private readonly CustomProviderDataHelper<BasicCustomTraceListenerData> helper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCustomTraceListenerData"/> with default values.
        /// </summary>
        public BasicCustomTraceListenerData()
        {
            helper = CreateHelper();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCustomTraceListenerData"/> with name, type and initial information.
        /// </summary>
        /// <param name="name">The name for the represented <see cref="System.Diagnostics.TraceListener"/>.</param>
        /// <param name="type">The <see cref="Type"/> of the represented <see cref="System.Diagnostics.TraceListener"/>.</param>
        /// <param name="initData">The initialization information for the represented <see cref="System.Diagnostics.TraceListener"/>.</param>
        public BasicCustomTraceListenerData(string name, Type type, string initData)
            : this(name, type, initData, TraceOptions.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCustomTraceListenerData"/> with name, fully qualified type name and initial information.
        /// </summary>
        /// <param name="name">The name for the represented <see cref="System.Diagnostics.TraceListener"/>.</param>
        /// <param name="typeName">The fully qualified type name of the represented <see cref="System.Diagnostics.TraceListener"/>.</param>
        /// <param name="initData">The initialization information for the represented <see cref="System.Diagnostics.TraceListener"/>.</param>
        public BasicCustomTraceListenerData(string name, string typeName, string initData)
            : this(name, typeName, initData, TraceOptions.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCustomTraceListenerData"/> with name, type, initial information, and <see cref="TraceOptions"/> output options.
        /// </summary>
        /// <param name="name">The name for the represented <see cref="System.Diagnostics.TraceListener"/>.</param>
        /// <param name="type">The <see cref="Type"/> of the represented <see cref="System.Diagnostics.TraceListener"/>.</param>
        /// <param name="initData">The initialization information for the represented <see cref="System.Diagnostics.TraceListener"/>.</param>
        /// <param name="traceOutputOptions">The <see cref="TraceOptions"/> output options.</param>
        public BasicCustomTraceListenerData(string name, Type type, string initData, TraceOptions traceOutputOptions)
            : this(name, new AssemblyQualifiedTypeNameConverter().ConvertToString(type), initData, traceOutputOptions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicCustomTraceListenerData"/> with name, fully qualified type name, initial information, and <see cref="TraceOptions"/> output options.
        /// </summary>
        /// <param name="name">The name for the represented <see cref="System.Diagnostics.TraceListener"/>.</param>
        /// <param name="typeName">The fully qualified type name of the represented <see cref="System.Diagnostics.TraceListener"/>.</param>
        /// <param name="initData">The initialization information for the represented <see cref="System.Diagnostics.TraceListener"/>.</param>
        /// <param name="traceOutputOptions">The <see cref="TraceOptions"/> output options.</param>
        public BasicCustomTraceListenerData(string name, string typeName, string initData, TraceOptions traceOutputOptions)
        {
            helper = CreateHelper();
            ListenerDataType = GetType();
            Name = name;
            TypeName = typeName;
            TraceOutputOptions = traceOutputOptions;
            InitData = initData;
        }

        /// <summary>
        /// Sets the attribute value for a key.
        /// </summary>
        /// <param name="key">The attribute name.</param>
        /// <param name="value">The attribute value.</param>
        public void SetAttributeValue(string key, string value)
        {
            helper.HandleSetAttributeValue(key, value);
        }

        /// <summary>
        /// Gets the custom configuration attributes.
        /// </summary>
        [Validation(MessagingDesignTime.ValidatorTypes.NameValueCollectionValidator)]
        public NameValueCollection Attributes
        {
            get { return helper.Attributes; }
        }

        /// <summary>
        /// Gets or sets the initialization data.
        /// </summary>
        public string InitData
        {
            get { return (string)base[initDataProperty]; }
            set { base[initDataProperty] = value; }
        }

        /// <summary>
        /// Creates the helper that enapsulates the configuration properties management.
        /// </summary>
        /// <returns></returns>
        protected virtual CustomProviderDataHelper<BasicCustomTraceListenerData> CreateHelper()
        {
            return new BasicCustomTraceListenerDataHelper(this);
        }

        /// <summary>
        /// Gets a <see cref="ConfigurationPropertyCollection"/> of the properties that are defined for 
        /// this configuration element when implemented in a derived class. 
        /// </summary>
        /// <value>
        /// A <see cref="ConfigurationPropertyCollection"/> of the properties that are defined for this
        /// configuration element when implemented in a derived class. 
        /// </value>
        protected override ConfigurationPropertyCollection Properties
        {
            get { return helper.Properties; }
        }

        /// <summary>
        /// Modifies the <see cref="SystemDiagnosticsTraceListenerData"/> object to remove all values that should not be saved. 
        /// </summary>
        /// <param name="sourceElement">A <see cref="ConfigurationElement"/> object at the current level containing a merged view of the properties.</param>
        /// <param name="parentElement">A parent <see cref="ConfigurationElement"/> object or <see langword="null"/> if this is the top level.</param>		
        /// <param name="saveMode">One of the <see cref="ConfigurationSaveMode"/> values.</param>
        protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
        {
            helper.HandleUnmerge(sourceElement, parentElement, saveMode);
        }

        /// <summary>
        /// Resets the internal state of the <see cref="SystemDiagnosticsTraceListenerData"/> object, 
        /// including the locks and the properties collection.
        /// </summary>
        /// <param name="parentElement">The parent element.</param>
        protected override void Reset(ConfigurationElement parentElement)
        {
            helper.HandleReset(parentElement);
        }

        /// <summary>
        /// Indicates whether this configuration element has been modified since it was last 
        /// saved or loaded when implemented in a derived class.
        /// </summary>
        /// <returns><see langword="true"/> if the element has been modified; otherwise, <see langword="false"/>. </returns>
        protected override bool IsModified()
        {
            return helper.HandleIsModified();
        }

        /// <summary>
        /// Called when an unknown attribute is encountered while deserializing the <see cref="SystemDiagnosticsTraceListenerData"/> object.
        /// </summary>
        /// <param name="name">The name of the unrecognized attribute.</param>
        /// <param name="value">The value of the unrecognized attribute.</param>
        /// <returns><see langword="true"/> if the processing of the element should continue; otherwise, <see langword="false"/>.</returns>
        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            return helper.HandleOnDeserializeUnrecognizedAttribute(name, value);
        }

        /// <summary>
        /// Gets the helper.
        /// </summary>
        CustomProviderDataHelper<BasicCustomTraceListenerData> IHelperAssistedCustomConfigurationData<BasicCustomTraceListenerData>.Helper
        {
            get { return helper; }
        }

        /// <summary>Invokes the inherited behavior.</summary>
        object IHelperAssistedCustomConfigurationData<BasicCustomTraceListenerData>.BaseGetPropertyValue(ConfigurationProperty property)
        {
            return base[property];
        }

        /// <summary>Invokes the inherited behavior.</summary>
        void IHelperAssistedCustomConfigurationData<BasicCustomTraceListenerData>.BaseSetPropertyValue(ConfigurationProperty property, object value)
        {
            base[property] = value;
        }

        /// <summary>Invokes the inherited behavior.</summary>
        void IHelperAssistedCustomConfigurationData<BasicCustomTraceListenerData>.BaseUnmerge(ConfigurationElement sourceElement,
                    ConfigurationElement parentElement,
                    ConfigurationSaveMode saveMode)
        {
            base.Unmerge(sourceElement, parentElement, saveMode);
        }

        /// <summary>Invokes the inherited behavior.</summary>
        void IHelperAssistedCustomConfigurationData<BasicCustomTraceListenerData>.BaseReset(ConfigurationElement parentElement)
        {
            base.Reset(parentElement);
        }

        /// <summary>Invokes the inherited behavior.</summary>
        bool IHelperAssistedCustomConfigurationData<BasicCustomTraceListenerData>.BaseIsModified()
        {
            return base.IsModified();
        }

        ///<summary>
        /// Returns the type <see cref="TypeRegistration"/> entries for the specified configuration data.
        ///</summary>
        /// <remarks>
        /// This will return multiple <see cref="TypeRegistration"/> entries only if the <see cref="BasicCustomTraceListenerData.Attributes"/>
        /// property is populated.  The additional registration returned is a <see cref="AttributeSettingTraceListenerWrapper"/> to handle
        /// supplying the attribute data to the underlying <see cref="TraceListener"/>.
        /// </remarks>
        ///<returns>A set of registry entries.</returns>        
        public override IEnumerable<TypeRegistration> GetRegistrations()
        {
            var mainListenerRegistration = GetTraceListenerTypeRegistration();
            if (this.Attributes.Count > 0)
            {
                mainListenerRegistration.Name = WrappedTraceListenerName + AttributeWrappedPrefix;

                yield return GetWrappingRegistration(mainListenerRegistration.Name);
            }

            yield return mainListenerRegistration;
            yield return GetTraceListenerWrapperTypeRegistration();
        }

        private TypeRegistration GetWrappingRegistration(string mainListenerRegistrationName)
        {
            return new TypeRegistration<TraceListener>(
                  () => new AttributeSettingTraceListenerWrapper(
                            Container.Resolved<TraceListener>(mainListenerRegistrationName),
                            this.Attributes
                            )
                  )
            {
                Name = this.WrappedTraceListenerName,
                Lifetime = TypeRegistrationLifetime.Transient
            };
        }


        /// <summary>
        /// Gets the creation expression used to produce a <see cref="TypeRegistration"/> during
        /// <see cref="TraceListenerData.GetRegistrations"/>.
        /// </summary>
        /// <remarks>
        /// This must be overridden by a subclass, but is not marked as abstract due to configuration serialization needs.
        /// </remarks>
        /// <returns>A <see cref="Expression"/> that creates a <see cref="TraceListener"/></returns>
        /// <exception cref="ArgumentException">Throws an argument exception if it cannot locate an appropriate constructor for the <see cref="NameTypeConfigurationElement.Type"/>.</exception>
        protected override Expression<Func<TraceListener>> GetCreationExpression()
        {
            Expression<Func<TraceListener>> expression;
            Type[] constructorParameters = new Type[0];
            Expression[] expressionArguments = new Expression[0];

            if (!string.IsNullOrEmpty(InitData))
            {
                constructorParameters = new Type[] { InitData.GetType() };
                expressionArguments = new Expression[] { Expression.Constant(InitData) };
            }

            ConstructorInfo constructor = GetConstructor(constructorParameters);

            expression =
                Expression.Lambda<Func<TraceListener>>(
                    Expression.New(
                        constructor,
                        expressionArguments)
                );

            return expression;
        }

        private ConstructorInfo GetConstructor(Type[] constructorParameters)
        {
            ConstructorInfo constructor = Type.GetConstructor(constructorParameters);

            if (constructor == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                                                          Messaging.Properties.Resources.ExceptionCannotFindAppropriateConstructor,
                                                          Type.Name,
                                                          constructorParameters.Length));
            }
            return constructor;
        }
    }
}
