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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using RTSafe.RTDP.Messaging.Configuration;

namespace RTSafe.RTDP.Messaging.Formatters
{
	/// <summary>
	/// Log formatter that will format a <see cref="MsgEntry"/> in a way suitable for wire transmission.
	/// </summary>
	[ConfigurationElementType(typeof(BinaryLogFormatterData))]
	public class BinaryLogFormatter : MsgFormatter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryLogFormatter"/> class.
		/// </summary>
		public BinaryLogFormatter()
		{ }

		/// <summary>
		/// Formats a log entry as a serialized representation.
		/// </summary>
		/// <remarks>
		/// Will use a BinaryFormatter for doing the actual serialization.
		/// </remarks>
		/// <param name="log">The <see cref="MsgEntry"/> to format.</param>
		/// <returns>A string version of the <see cref="MsgEntry"/> that can be deserialized back to a <see cref="MsgEntry"/> instance.</returns>
		public override string Format(MsgEntry log)
		{
			using (MemoryStream binaryStream = new MemoryStream())
			{
				GetFormatter().Serialize(binaryStream, log);
				return Convert.ToBase64String(binaryStream.ToArray());
			}
		}

		/// <summary>
		/// Deserializes the string representation of a <see cref="MsgEntry"/> into a <see cref="MsgEntry"/> instance.
		/// </summary>
		/// <param name="serializedLogEntry">The serialized <see cref="MsgEntry"/> representation.</param>
		/// <returns>The <see cref="MsgEntry"/>.</returns>
		public static MsgEntry Deserialize(string serializedLogEntry)
		{
			using (MemoryStream binaryStream = new MemoryStream(Convert.FromBase64String(serializedLogEntry)))
			{
				return (MsgEntry)GetFormatter().Deserialize(binaryStream);
			}
		}

		private static BinaryFormatter GetFormatter()
		{
			return new BinaryFormatter();
		}
	}
}
