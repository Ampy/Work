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
using RTSafe.RTDP.Messaging;
using RTSafe.RTDP.Messaging.Formatters;
using RTSafe.RTDP.Messaging.Configuration;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent
{
    /// <summary>
    /// Interface for builder classes used to configure <see cref="IMsgFormatter"/> instances.
    /// </summary>
    public interface IFormatterBuilder
    {
        /// <summary>
        /// Returns the <see cref="FormatterData"/> instance that contains the configuration for an <see cref="IMsgFormatter"/> instance.
        /// </summary>
        FormatterData GetFormatterData();
    }
}
