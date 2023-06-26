using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Represents the exception that is thrown when the <see cref="T:System.Net.Mail.SmtpClient" /> is not able to complete a <see cref="Overload:System.Net.Mail.SmtpClient.Send" /> or <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> operation.</summary>
	// Token: 0x0200028A RID: 650
	[Serializable]
	public class SmtpException : Exception, ISerializable
	{
		// Token: 0x0600183F RID: 6207 RVA: 0x0007BA3D File Offset: 0x00079C3D
		private static string GetMessageForStatus(SmtpStatusCode statusCode, string serverResponse)
		{
			return SmtpException.GetMessageForStatus(statusCode) + " " + SR.GetString("MailServerResponse", new object[] { serverResponse });
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0007BA64 File Offset: 0x00079C64
		private static string GetMessageForStatus(SmtpStatusCode statusCode)
		{
			if (statusCode <= SmtpStatusCode.UserNotLocalWillForward)
			{
				if (statusCode <= SmtpStatusCode.ServiceReady)
				{
					if (statusCode == SmtpStatusCode.SystemStatus)
					{
						return SR.GetString("SmtpSystemStatus");
					}
					if (statusCode == SmtpStatusCode.HelpMessage)
					{
						return SR.GetString("SmtpHelpMessage");
					}
					if (statusCode == SmtpStatusCode.ServiceReady)
					{
						return SR.GetString("SmtpServiceReady");
					}
				}
				else
				{
					if (statusCode == SmtpStatusCode.ServiceClosingTransmissionChannel)
					{
						return SR.GetString("SmtpServiceClosingTransmissionChannel");
					}
					if (statusCode == SmtpStatusCode.Ok)
					{
						return SR.GetString("SmtpOK");
					}
					if (statusCode == SmtpStatusCode.UserNotLocalWillForward)
					{
						return SR.GetString("SmtpUserNotLocalWillForward");
					}
				}
			}
			else if (statusCode <= SmtpStatusCode.ClientNotPermitted)
			{
				if (statusCode == SmtpStatusCode.StartMailInput)
				{
					return SR.GetString("SmtpStartMailInput");
				}
				if (statusCode == SmtpStatusCode.ServiceNotAvailable)
				{
					return SR.GetString("SmtpServiceNotAvailable");
				}
				switch (statusCode)
				{
				case SmtpStatusCode.MailboxBusy:
					return SR.GetString("SmtpMailboxBusy");
				case SmtpStatusCode.LocalErrorInProcessing:
					return SR.GetString("SmtpLocalErrorInProcessing");
				case SmtpStatusCode.InsufficientStorage:
					return SR.GetString("SmtpInsufficientStorage");
				case SmtpStatusCode.ClientNotPermitted:
					return SR.GetString("SmtpClientNotPermitted");
				}
			}
			else
			{
				switch (statusCode)
				{
				case SmtpStatusCode.CommandUnrecognized:
					break;
				case SmtpStatusCode.SyntaxError:
					return SR.GetString("SmtpSyntaxError");
				case SmtpStatusCode.CommandNotImplemented:
					return SR.GetString("SmtpCommandNotImplemented");
				case SmtpStatusCode.BadCommandSequence:
					return SR.GetString("SmtpBadCommandSequence");
				case SmtpStatusCode.CommandParameterNotImplemented:
					return SR.GetString("SmtpCommandParameterNotImplemented");
				default:
					if (statusCode == SmtpStatusCode.MustIssueStartTlsFirst)
					{
						return SR.GetString("SmtpMustIssueStartTlsFirst");
					}
					switch (statusCode)
					{
					case SmtpStatusCode.MailboxUnavailable:
						return SR.GetString("SmtpMailboxUnavailable");
					case SmtpStatusCode.UserNotLocalTryAlternatePath:
						return SR.GetString("SmtpUserNotLocalTryAlternatePath");
					case SmtpStatusCode.ExceededStorageAllocation:
						return SR.GetString("SmtpExceededStorageAllocation");
					case SmtpStatusCode.MailboxNameNotAllowed:
						return SR.GetString("SmtpMailboxNameNotAllowed");
					case SmtpStatusCode.TransactionFailed:
						return SR.GetString("SmtpTransactionFailed");
					}
					break;
				}
			}
			return SR.GetString("SmtpCommandUnrecognized");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified status code.</summary>
		/// <param name="statusCode">An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value.</param>
		// Token: 0x06001841 RID: 6209 RVA: 0x0007BC54 File Offset: 0x00079E54
		public SmtpException(SmtpStatusCode statusCode)
			: base(SmtpException.GetMessageForStatus(statusCode))
		{
			this.statusCode = statusCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified status code and error message.</summary>
		/// <param name="statusCode">An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value.</param>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		// Token: 0x06001842 RID: 6210 RVA: 0x0007BC70 File Offset: 0x00079E70
		public SmtpException(SmtpStatusCode statusCode, string message)
			: base(message)
		{
			this.statusCode = statusCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class.</summary>
		// Token: 0x06001843 RID: 6211 RVA: 0x0007BC87 File Offset: 0x00079E87
		public SmtpException()
			: this(SmtpStatusCode.GeneralFailure)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		// Token: 0x06001844 RID: 6212 RVA: 0x0007BC90 File Offset: 0x00079E90
		public SmtpException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified error message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06001845 RID: 6213 RVA: 0x0007BCA0 File Offset: 0x00079EA0
		public SmtpException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpException" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream associated with the new instance.</param>
		// Token: 0x06001846 RID: 6214 RVA: 0x0007BCB1 File Offset: 0x00079EB1
		protected SmtpException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
			this.statusCode = (SmtpStatusCode)serializationInfo.GetInt32("Status");
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x0007BCD3 File Offset: 0x00079ED3
		internal SmtpException(SmtpStatusCode statusCode, string serverMessage, bool serverResponse)
			: base(SmtpException.GetMessageForStatus(statusCode, serverMessage))
		{
			this.statusCode = statusCode;
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0007BCF0 File Offset: 0x00079EF0
		internal SmtpException(string message, string serverResponse)
			: base(message + " " + SR.GetString("MailServerResponse", new object[] { serverResponse }))
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.Mail.SmtpException" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" />, which holds the serialized data for the <see cref="T:System.Net.Mail.SmtpException" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream associated with the new <see cref="T:System.Net.Mail.SmtpException" />.</param>
		// Token: 0x06001849 RID: 6217 RVA: 0x0007BD1E File Offset: 0x00079F1E
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.Mail.SmtpException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x0600184A RID: 6218 RVA: 0x0007BD28 File Offset: 0x00079F28
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("Status", (int)this.statusCode, typeof(int));
		}

		/// <summary>Gets the status code returned by an SMTP server when an email message is transmitted.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value that indicates the error that occurred.</returns>
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0007BD52 File Offset: 0x00079F52
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x0007BD5A File Offset: 0x00079F5A
		public SmtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				this.statusCode = value;
			}
		}

		// Token: 0x04001842 RID: 6210
		private SmtpStatusCode statusCode = SmtpStatusCode.GeneralFailure;
	}
}
