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

namespace RTSafe.RTDP.Messaging.Formatters
{
	/// <summary>
	/// Abstract implememtation of the <see cref="IMsgFormatter"/> interface.
	/// </summary>
	public abstract class MsgFormatter : IMsgFormatter
	{
		/// <summary>
		/// Formats a log entry and return a string to be outputted.
		/// </summary>
		/// <param name="log">Log entry to format.</param>
		/// <returns>A string representing the log entry.</returns>
		public abstract string Format(MsgEntry log);
	}
}
