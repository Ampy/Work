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

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.ServiceLocation;

namespace RTSafe.RTDP.Messaging
{
    /// <summary>
    /// Factory to create <see cref="MsgWriter"/> instances.
    /// </summary>
    public class MsgWriterFactory : ContainerBasedInstanceFactory<MsgWriter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsgWriter"/> class with the default <see cref="IConfigurationSource"/> instance.
        /// </summary>
        public MsgWriterFactory()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsgWriter"/> class with a <see cref="IConfigurationSource"/> instance.
        /// </summary>
        /// <param name="configurationSource">The source for configuration information.</param>
        public MsgWriterFactory(IConfigurationSource configurationSource)
            : base(configurationSource)
        {
        }

        /// <summary>
        /// Create an instance of <see cref="MsgWriterFactory"/> that resolves objects
        /// using the supplied <paramref name="container"/>.
        /// </summary>
        /// <param name="container"><see cref="IServiceLocator"/> to use to resolve objects.</param>
        public MsgWriterFactory(IServiceLocator container)
            : base(container)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="MsgWriter"/> based on the configuration in the <see cref="IConfigurationSource"/> 
        /// instance of the factory.
        /// </summary>
        /// <returns>The created <see cref="MsgWriter"/> object.</returns>
        public MsgWriter Create()
        {
            return CreateDefault();
        }
    }
}
