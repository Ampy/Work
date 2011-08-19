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
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;

namespace RTSafe.RTDP.Messaging.Configuration
{
    /// <summary>
    /// A configuration element that handles the entries for the &lt;categories&gt; element
    /// for the Log Call handler.
    /// </summary>
    [ResourceDescription(typeof(DesignResources), "LogCallHandlerCategoryEntryDescription")]
    [ResourceDisplayName(typeof(DesignResources), "LogCallHandlerCategoryEntryDisplayName")]
    [AddSateliteProviderCommand(MessagingSettings.SectionName, typeof(MessagingSettings), "DefaultCategory", "Name")]
    public class MsgCallHandlerCategoryEntry : NamedConfigurationElement
    {
        /// <summary>
        /// Construct an empty <see cref="MsgCallHandlerCategoryEntry"/>.
        /// </summary>
        public MsgCallHandlerCategoryEntry()
        {
        }

        /// <summary>
        /// Construct a <see cref="MsgCallHandlerCategoryEntry"/> with the given
        /// category string.
        /// </summary>
        /// <param name="name">Category string.</param>
        public MsgCallHandlerCategoryEntry(string name) : base(name)
        {
        }

        /// <summary>
        /// Gets or sets the name of the element.
        /// </summary>
        /// <value>
        /// The name of the element.
        /// </value>
        /// <remarks>
        /// Overriden in order to annotate with designtime attribute.
        /// </remarks>
        [ResourceDescription(typeof(DesignResources), "LogCallHandlerCategoryEntryNameDescription")]
        [ResourceDisplayName(typeof(DesignResources), "LogCallHandlerCategoryEntryNameDisplayName")]
        [Reference(typeof(NamedElementCollection<TraceSourceData>), typeof(TraceSourceData))]
        [ViewModel(CommonDesignTime.ViewModelTypeNames.CollectionEditorContainedElementReferenceProperty)]
        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
    }
}
