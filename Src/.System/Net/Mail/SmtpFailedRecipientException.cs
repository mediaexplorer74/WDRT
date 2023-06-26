using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Represents the exception that is thrown when the <see cref="T:System.Net.Mail.SmtpClient" /> is not able to complete a <see cref="Overload:System.Net.Mail.SmtpClient.Send" /> or <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> operation to a particular recipient.</summary>
	// Token: 0x0200028B RID: 651
	[Serializable]
	public class SmtpFailedRecipientException : SmtpException, ISerializable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class.</summary>
		// Token: 0x0600184D RID: 6221 RVA: 0x0007BD63 File Offset: 0x00079F63
		public SmtpFailedRecipientException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class with the specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains the error message.</param>
		// Token: 0x0600184E RID: 6222 RVA: 0x0007BD6B File Offset: 0x00079F6B
		public SmtpFailedRecipientException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified error message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x0600184F RID: 6223 RVA: 0x0007BD74 File Offset: 0x00079F74
		public SmtpFailedRecipientException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream that is associated with the new instance.</param>
		// Token: 0x06001850 RID: 6224 RVA: 0x0007BD7E File Offset: 0x00079F7E
		protected SmtpFailedRecipientException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.failedRecipient = info.GetString("failedRecipient");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class with the specified status code and email address.</summary>
		/// <param name="statusCode">An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value.</param>
		/// <param name="failedRecipient">A <see cref="T:System.String" /> that contains the email address.</param>
		// Token: 0x06001851 RID: 6225 RVA: 0x0007BD99 File Offset: 0x00079F99
		public SmtpFailedRecipientException(SmtpStatusCode statusCode, string failedRecipient)
			: base(statusCode)
		{
			this.failedRecipient = failedRecipient;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class with the specified status code, email address, and server response.</summary>
		/// <param name="statusCode">An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value.</param>
		/// <param name="failedRecipient">A <see cref="T:System.String" /> that contains the email address.</param>
		/// <param name="serverResponse">A <see cref="T:System.String" /> that contains the server response.</param>
		// Token: 0x06001852 RID: 6226 RVA: 0x0007BDA9 File Offset: 0x00079FA9
		public SmtpFailedRecipientException(SmtpStatusCode statusCode, string failedRecipient, string serverResponse)
			: base(statusCode, serverResponse, true)
		{
			this.failedRecipient = failedRecipient;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified error message, email address, and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		/// <param name="failedRecipient">A <see cref="T:System.String" /> that contains the email address.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06001853 RID: 6227 RVA: 0x0007BDBB File Offset: 0x00079FBB
		public SmtpFailedRecipientException(string message, string failedRecipient, Exception innerException)
			: base(message, innerException)
		{
			this.failedRecipient = failedRecipient;
		}

		/// <summary>Indicates the email address with delivery difficulties.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the email address.</returns>
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x0007BDCC File Offset: 0x00079FCC
		public string FailedRecipient
		{
			get
			{
				return this.failedRecipient;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance, which holds the serialized data for the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> instance that contains the destination of the serialized stream that is associated with the new <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</param>
		// Token: 0x06001855 RID: 6229 RVA: 0x0007BDD4 File Offset: 0x00079FD4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06001856 RID: 6230 RVA: 0x0007BDDE File Offset: 0x00079FDE
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("failedRecipient", this.failedRecipient, typeof(string));
		}

		// Token: 0x04001843 RID: 6211
		private string failedRecipient;

		// Token: 0x04001844 RID: 6212
		internal bool fatal;
	}
}
