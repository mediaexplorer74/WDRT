using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>The exception that is thrown when email is sent using an <see cref="T:System.Net.Mail.SmtpClient" /> and cannot be delivered to all recipients.</summary>
	// Token: 0x0200028C RID: 652
	[Serializable]
	public class SmtpFailedRecipientsException : SmtpFailedRecipientException, ISerializable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class.</summary>
		// Token: 0x06001857 RID: 6231 RVA: 0x0007BE03 File Offset: 0x0007A003
		public SmtpFailedRecipientsException()
		{
			this.innerExceptions = new SmtpFailedRecipientException[0];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" />.</summary>
		/// <param name="message">The exception message.</param>
		// Token: 0x06001858 RID: 6232 RVA: 0x0007BE17 File Offset: 0x0007A017
		public SmtpFailedRecipientsException(string message)
			: base(message)
		{
			this.innerExceptions = new SmtpFailedRecipientException[0];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" /> and inner <see cref="T:System.Exception" />.</summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerException">The inner exception.</param>
		// Token: 0x06001859 RID: 6233 RVA: 0x0007BE2C File Offset: 0x0007A02C
		public SmtpFailedRecipientsException(string message, Exception innerException)
			: base(message, innerException)
		{
			SmtpFailedRecipientException ex = innerException as SmtpFailedRecipientException;
			SmtpFailedRecipientException[] array;
			if (ex != null)
			{
				(array = new SmtpFailedRecipientException[1])[0] = ex;
			}
			else
			{
				array = new SmtpFailedRecipientException[0];
			}
			this.innerExceptions = array;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> instance.</param>
		// Token: 0x0600185A RID: 6234 RVA: 0x0007BE63 File Offset: 0x0007A063
		protected SmtpFailedRecipientsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.innerExceptions = (SmtpFailedRecipientException[])info.GetValue("innerExceptions", typeof(SmtpFailedRecipientException[]));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" /> and array of type <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerExceptions">The array of recipients with delivery errors.</param>
		// Token: 0x0600185B RID: 6235 RVA: 0x0007BE90 File Offset: 0x0007A090
		public SmtpFailedRecipientsException(string message, SmtpFailedRecipientException[] innerExceptions)
			: base(message, (innerExceptions != null && innerExceptions.Length != 0) ? innerExceptions[0].FailedRecipient : null, (innerExceptions != null && innerExceptions.Length != 0) ? innerExceptions[0] : null)
		{
			if (innerExceptions == null)
			{
				throw new ArgumentNullException("innerExceptions");
			}
			this.innerExceptions = ((innerExceptions == null) ? new SmtpFailedRecipientException[0] : innerExceptions);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0007BEE4 File Offset: 0x0007A0E4
		internal SmtpFailedRecipientsException(ArrayList innerExceptions, bool allFailed)
			: base(allFailed ? SR.GetString("SmtpAllRecipientsFailed") : SR.GetString("SmtpRecipientFailed"), (innerExceptions != null && innerExceptions.Count > 0) ? ((SmtpFailedRecipientException)innerExceptions[0]).FailedRecipient : null, (innerExceptions != null && innerExceptions.Count > 0) ? ((SmtpFailedRecipientException)innerExceptions[0]) : null)
		{
			if (innerExceptions == null)
			{
				throw new ArgumentNullException("innerExceptions");
			}
			this.innerExceptions = new SmtpFailedRecipientException[innerExceptions.Count];
			int num = 0;
			foreach (object obj in innerExceptions)
			{
				SmtpFailedRecipientException ex = (SmtpFailedRecipientException)obj;
				this.innerExceptions[num++] = ex;
			}
		}

		/// <summary>Gets one or more <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />s that indicate the email recipients with SMTP delivery errors.</summary>
		/// <returns>An array of type <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> that lists the recipients with delivery errors.</returns>
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x0007BFBC File Offset: 0x0007A1BC
		public SmtpFailedRecipientException[] InnerExceptions
		{
			get
			{
				return this.innerExceptions;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />.</param>
		// Token: 0x0600185E RID: 6238 RVA: 0x0007BFC4 File Offset: 0x0007A1C4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used.</param>
		// Token: 0x0600185F RID: 6239 RVA: 0x0007BFCE File Offset: 0x0007A1CE
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("innerExceptions", this.innerExceptions, typeof(SmtpFailedRecipientException[]));
		}

		// Token: 0x04001845 RID: 6213
		private SmtpFailedRecipientException[] innerExceptions;
	}
}
