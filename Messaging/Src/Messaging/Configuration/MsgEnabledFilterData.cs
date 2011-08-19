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

using System.Collections.Generic;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using RTSafe.RTDP.Messaging.Filters;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;

namespace RTSafe.RTDP.Messaging.Configuration
{
    /// <summary>
    /// Represents the configuration settings that describe a <see cref="MsgEnabledFilter"/>.
    /// </summary>
    [ResourceDescription(typeof(DesignResources), "LogEnabledFilterDataDescription")]
    [ResourceDisplayName(typeof(DesignResources), "LogEnabledFilterDataDisplayName")]
    public class MsgEnabledFilterData : MsgFilterData
    {
        private const string enabledProperty = "enabled";

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="MsgEnabledFilterData"/> class.</para>
        /// </summary>
        public MsgEnabledFilterData()
        {
            Type = typeof(MsgEnabledFilter);
        }

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="MsgEnabledFilterData"/> class.</para>
        /// </summary>
        /// <param name="enabled">True if logging should be enabled.</param>
        public MsgEnabledFilterData(bool enabled)
            : this("enabled", enabled)
        {
        }

        /// <summary>
        /// <para>Initialize a new named instance of the <see cref="MsgEnabledFilterData"/> class.</para>
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="enabled">True if logging should be enabled.</param>
        public MsgEnabledFilterData(string name, bool enabled)
            : base(name, typeof(MsgEnabledFilter))
        {
            this.Enabled = enabled;
        }


        /// <summary>
        /// Gets or sets the enabled value.
        /// </summary>
        [ConfigurationProperty(enabledProperty, IsRequired = true, DefaultValue=false)]
        [ResourceDescription(typeof(DesignResources), "LogEnabledFilterDataEnabledDescription")]
        [ResourceDisplayName(typeof(DesignResources), "LogEnabledFilterDataEnabledDisplayName")]
        public bool Enabled
        {
            get { return (bool)base[enabledProperty]; }
            set { base[enabledProperty] = value; }
        }

        /// <summary>
        /// Creates an enumeration of <see cref="TypeRegistration"/> instances describing the filter represented by 
        /// this configuration object.
        /// </summary>
        /// <returns>A an enumeration of <see cref="TypeRegistration"/> instance describing a filter.</returns>
        public override IEnumerable<TypeRegistration> GetRegistrations()
        {
            yield return
                new TypeRegistration<IMsgFilter>(() => new MsgEnabledFilter(this.Name, this.Enabled))
                {
                    Name = this.Name,
                    Lifetime = TypeRegistrationLifetime.Transient
                };
        }
    }
}
