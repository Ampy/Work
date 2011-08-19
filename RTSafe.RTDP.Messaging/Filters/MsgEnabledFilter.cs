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
using RTSafe.RTDP.Messaging.Configuration;

namespace RTSafe.RTDP.Messaging.Filters
{
	/// <summary>
	/// Represents a Boolean on/off filter.
	/// </summary>
	[ConfigurationElementType(typeof(MsgEnabledFilterData))]
	public class MsgEnabledFilter : MsgFilter
	{
		private bool enabled = false;

		/// <summary>
		/// Initializes an instance of <see cref="MsgEnabledFilter"/>.
		/// </summary>
		/// <param name="name">The name of the filter.</param>
		/// <param name="enabled">True if the filter allows messages, false otherwise.</param>
		public MsgEnabledFilter(string name, bool enabled)
			: base(name)
		{
			this.enabled = enabled;
		}

		/// <summary>
		/// Tests to see if a message meets the criteria to be processed. 
		/// </summary>
		/// <param name="log">Log entry to test.</param>
		/// <returns><b>true</b> if the message passes through the filter and should be logged, <b>false</b> otherwise.</returns>
		public override bool Filter(MsgEntry log)
		{
			return enabled;
		}

		/// <summary>
		/// Gets or set the Boolean flag for the filter.
		/// </summary>
		public bool Enabled
		{
			get { return this.enabled; }
			set { this.enabled = value; }
		}
	}
}
