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

namespace RTSafe.RTDP.Messaging.Filters
{
	/// <summary>
	/// Abstract implementation of the <see cref="IMsgFilter"/> interface.
	/// </summary>
	public abstract class MsgFilter : IMsgFilter
	{
		private string name;

		/// <summary>
		/// Initializes a new instance of <see cref="MsgFilter"/>.
		/// </summary>
		/// <param name="name">The name for the log filter.</param>
		public MsgFilter(string name)
		{
			this.name = name;
		}

		/// <summary>
		/// Test to see if a message meets the criteria to be processed. 
		/// </summary>
		/// <param name="log">Log entry to test.</param>
		/// <returns><b>true</b> if the message passes through the filter and should be logged, <b>false</b> otherwise.</returns>
		public abstract bool Filter(MsgEntry log);

		/// <summary>
		/// Gets the name of the log filter
		/// </summary>
		public string Name
		{
			get { return name; }
		}
	}
}
