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
using System.Net;
using System.Net.Mail;
using System.Text;

using RTSafe.RTDP.Messaging.Formatters;
using RTSafe.RTDP.Messaging.TraceListeners;

namespace RTSafe.RTDP.Messaging.Configuration
{
	/// <summary>
	/// Represents a <see cref="EmailMessage"/>.
	/// Encapsulates a System.Net.MailMessage with functions to accept a MsgEntry, Formatting, and sending of emails
	/// </summary>
	public class EmailMessage
	{
		private EmailTraceListenerData configurationData;
		private IMsgFormatter formatter;
		private MsgEntry MsgEntry;

		/// <summary>
		/// Initializes a <see cref="EmailMessage"/> with email configuration data, MsgEntry, and formatter 
		/// </summary>
		/// <param name="configurationData">The configuration data <see cref="EmailTraceListenerData"/> 
		/// that represents how to create the email message</param>
		/// <param name="MsgEntry">The MsgEntry <see cref="MsgEntry"/> to send via email.</param>
		/// <param name="formatter">The Formatter <see cref="IMsgFormatter"/> which determines how the 
		/// email message should be formatted</param>
		public EmailMessage(EmailTraceListenerData configurationData, MsgEntry MsgEntry, IMsgFormatter formatter)
		{
			this.configurationData = configurationData;
			this.MsgEntry = MsgEntry;
			this.formatter = formatter;
		}

		/// <summary>
		/// Initializes a <see cref="EmailMessage"/> with the raw data to create and email, the MsgEntry, and the formatter 
		/// </summary>
		/// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
		/// <param name="fromAddress">Represents from whom the email is sent.</param>
		/// <param name="subjectLineStarter">Starting text for the subject line.</param>
		/// <param name="subjectLineEnder">Ending text for the subject line.</param>
		/// <param name="smtpServer">The name of the SMTP server.</param>
		/// <param name="smtpPort">The port on the SMTP server to use for sending the email.</param>
		/// <param name="MsgEntry">The MsgEntry <see cref="MsgEntry"/> to send via email.</param>
		/// <param name="formatter">The Formatter <see cref="IMsgFormatter"/> which determines how the 
		/// email message should be formatted</param>
		public EmailMessage(string toAddress, string fromAddress, string subjectLineStarter, string subjectLineEnder, string smtpServer, int smtpPort, MsgEntry MsgEntry, IMsgFormatter formatter)
		{
			this.configurationData = new EmailTraceListenerData(toAddress, fromAddress, subjectLineStarter, subjectLineEnder, smtpServer, smtpPort, string.Empty);
			this.MsgEntry = MsgEntry;
			this.formatter = formatter;
		}

        /// <summary>
        /// Initializes a <see cref="EmailMessage"/> with the raw data to create and email, the MsgEntry, and the formatter 
        /// </summary>
        /// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        /// <param name="fromAddress">Represents from whom the email is sent.</param>
        /// <param name="subjectLineStarter">Starting text for the subject line.</param>
        /// <param name="subjectLineEnder">Ending text for the subject line.</param>
        /// <param name="smtpServer">The name of the SMTP server.</param>
        /// <param name="smtpPort">The port on the SMTP server to use for sending the email.</param>
        /// <param name="MsgEntry">The MsgEntry <see cref="MsgEntry"/> to send via email.</param>
        /// <param name="formatter">The Formatter <see cref="IMsgFormatter"/> which determines how the 
        /// email message should be formatted</param>
        /// <param name="authenticationMode">Authenticate mode to use when connecting to SMTP server.</param>
        /// <param name="userName">User name to send to SMTP server if using username/password authentication.</param>
        /// <param name="password">Password to send to SMTP server if using username/password authentication.</param>
        /// <param name="useSSL">Use SSL to connect to STMP server - if true, yes, if false, no.</param>
        public EmailMessage(string toAddress, string fromAddress, string subjectLineStarter, string subjectLineEnder, string smtpServer, int smtpPort, MsgEntry MsgEntry, IMsgFormatter formatter,
            EmailAuthenticationMode authenticationMode, string userName, string password, bool useSSL)
        {
            this.configurationData = new EmailTraceListenerData(toAddress, fromAddress, subjectLineStarter,
                                                                subjectLineEnder, smtpServer, smtpPort, string.Empty)
                                         {
                                             AuthenticationMode = authenticationMode,
                                             UserName = userName,
                                             Password = password,
                                             UseSSL = useSSL
                                         };
            this.MsgEntry = MsgEntry;
            this.formatter = formatter;
        }

		/// <summary>
		/// Initializes a <see cref="EmailMessage"/> with the raw data to create and email, a message, and the formatter 
		/// </summary>
		/// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
		/// <param name="fromAddress">Represents from whom the email is sent.</param>
		/// <param name="subjectLineStarter">Starting text for the subject line.</param>
		/// <param name="subjectLineEnder">Ending text for the subject line.</param>
		/// <param name="smtpServer">The name of the SMTP server.</param>
		/// <param name="smtpPort">The port on the SMTP server to use for sending the email.</param>
		/// <param name="message">Represents the message to send via email.</param>
		/// <param name="formatter">The Formatter <see cref="IMsgFormatter"/> which determines how the 
		/// email message should be formatted</param>
		public EmailMessage(string toAddress, string fromAddress, string subjectLineStarter, string subjectLineEnder, string smtpServer, int smtpPort, string message, IMsgFormatter formatter)
		{
			this.configurationData = new EmailTraceListenerData(toAddress, fromAddress, subjectLineStarter, subjectLineEnder, smtpServer, smtpPort, string.Empty);
			this.MsgEntry = new MsgEntry();
			MsgEntry.Message = message;
			this.formatter = formatter;
		}

        /// <summary>
        /// Initializes a <see cref="EmailMessage"/> with the raw data to create and email, a message, and the formatter 
        /// </summary>
        /// <param name="toAddress">A semicolon delimited string the represents to whom the email should be sent.</param>
        /// <param name="fromAddress">Represents from whom the email is sent.</param>
        /// <param name="subjectLineStarter">Starting text for the subject line.</param>
        /// <param name="subjectLineEnder">Ending text for the subject line.</param>
        /// <param name="smtpServer">The name of the SMTP server.</param>
        /// <param name="smtpPort">The port on the SMTP server to use for sending the email.</param>
        /// <param name="message">Represents the message to send via email.</param>
        /// <param name="formatter">The Formatter <see cref="IMsgFormatter"/> which determines how the 
        /// email message should be formatted</param>
        /// <param name="authenticationMode">Authenticate mode to use when connecting to SMTP server.</param>
        /// <param name="userName">User name to send to SMTP server if using username/password authentication.</param>
        /// <param name="password">Password to send to SMTP server if using username/password authentication.</param>
        /// <param name="useSSL">Use SSL to connect to STMP server - if true, yes, if false, no.</param>
        public EmailMessage(string toAddress, string fromAddress, string subjectLineStarter, string subjectLineEnder, string smtpServer, int smtpPort, string message, IMsgFormatter formatter,
            EmailAuthenticationMode authenticationMode, string userName, string password, bool useSSL)
        {
            this.configurationData = new EmailTraceListenerData(toAddress, fromAddress, subjectLineStarter,
                                                                subjectLineEnder, smtpServer, smtpPort, string.Empty)
                                         {
                                             AuthenticationMode = authenticationMode,
                                             UserName = userName,
                                             Password = password,
                                             UseSSL = useSSL
                                         };
            this.MsgEntry = new MsgEntry();
            MsgEntry.Message = message;
            this.formatter = formatter;
        }

		/// <summary>
		/// Creates the prefix for the subject line
		/// </summary>
		/// <param name="subjectLineField">string to add as the subject line prefix (plus whitespace) if it is not empty.</param>
		/// <returns>modified string to use as subject line prefix</returns>
		private static string GenerateSubjectPrefix(string subjectLineField)
		{
			return string.IsNullOrEmpty(subjectLineField)
				? ""
				: subjectLineField + " ";
		}

		/// <summary>
		/// Creates the suffix for the subject line.
		/// </summary>
		/// <param name="subjectLineField">string to add as the subject line suffix (plus whitespace) if it is not empty.</param>
		/// <returns>modified string to use as subject line suffix</returns>
		private static string GenerateSubjectSuffix(string subjectLineField)
		{
			return string.IsNullOrEmpty(subjectLineField)
				? ""
				: " " + subjectLineField;
		}

		/// <summary>
		/// Creates a <see cref="MailMessage"/> from the configuration data which was used to create the instance of this object.
		/// </summary>
		/// <returns>A new <see cref="MailMessage"/>.</returns>
		protected MailMessage CreateMailMessage()
		{
			string header = GenerateSubjectPrefix(configurationData.SubjectLineStarter);
			string footer = GenerateSubjectSuffix(configurationData.SubjectLineEnder);

			string sendToSmtpSubject = header + MsgEntry.Severity.ToString() + footer;

			MailMessage message = new MailMessage();
			string[] toAddresses = configurationData.ToAddress.Split(';');
			foreach (string toAddress in toAddresses)
			{
				message.To.Add(new MailAddress(toAddress));
			}

			message.From = new MailAddress(configurationData.FromAddress);

			message.Body = (formatter != null) ? formatter.Format(MsgEntry) : MsgEntry.Message;
			message.Subject = sendToSmtpSubject;
			message.BodyEncoding = Encoding.UTF8;

			return message;
		}

		/// <summary>
		/// Uses the settings for the SMTP server and SMTP port to send the new mail message
		/// </summary>
		public virtual void Send()
		{
			using (MailMessage message = CreateMailMessage())
			{
				SendMessage(message);
			}
		}

		/// <summary>
		/// Uses the settings for the SMTP server and SMTP port to send the MailMessage that it is passed
		/// </summary>
		/// <param name="message">MailMessage to send via SMTP</param>
		public virtual void SendMessage(MailMessage message)
		{
			SmtpClient smtpClient = new SmtpClient(configurationData.SmtpServer, configurationData.SmtpPort);
            SetCredentials(smtpClient);
		    smtpClient.EnableSsl = configurationData.UseSSL;
			smtpClient.Send(message);
		}

        private void SetCredentials(SmtpClient smtpClient)
        {
            switch (configurationData.AuthenticationMode)
            {
                case EmailAuthenticationMode.WindowsCredentials:
                    smtpClient.UseDefaultCredentials = true;
                    break;

                case EmailAuthenticationMode.UserNameAndPassword:
                    smtpClient.Credentials = new NetworkCredential(configurationData.UserName, configurationData.Password);
                    break;
            }
        }
	}
}
