using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Specifies an exception that is handled as a warning instead of an error.</summary>
	// Token: 0x020005BA RID: 1466
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class WarningException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class.</summary>
		// Token: 0x060036F0 RID: 14064 RVA: 0x000EEFA5 File Offset: 0x000ED1A5
		public WarningException()
			: this(null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message and no Help file.</summary>
		/// <param name="message">The message to display to the end user.</param>
		// Token: 0x060036F1 RID: 14065 RVA: 0x000EEFB0 File Offset: 0x000ED1B0
		public WarningException(string message)
			: this(message, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message, and with access to the specified Help file.</summary>
		/// <param name="message">The message to display to the end user.</param>
		/// <param name="helpUrl">The Help file to display if the user requests help.</param>
		// Token: 0x060036F2 RID: 14066 RVA: 0x000EEFBB File Offset: 0x000ED1BB
		public WarningException(string message, string helpUrl)
			: this(message, helpUrl, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified detailed description and the specified exception.</summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x060036F3 RID: 14067 RVA: 0x000EEFC6 File Offset: 0x000ED1C6
		public WarningException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message, and with access to the specified Help file and topic.</summary>
		/// <param name="message">The message to display to the end user.</param>
		/// <param name="helpUrl">The Help file to display if the user requests help.</param>
		/// <param name="helpTopic">The Help topic to display if the user requests help.</param>
		// Token: 0x060036F4 RID: 14068 RVA: 0x000EEFD0 File Offset: 0x000ED1D0
		public WarningException(string message, string helpUrl, string helpTopic)
			: base(message)
		{
			this.helpUrl = helpUrl;
			this.helpTopic = helpTopic;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x060036F5 RID: 14069 RVA: 0x000EEFE8 File Offset: 0x000ED1E8
		protected WarningException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.helpUrl = (string)info.GetValue("helpUrl", typeof(string));
			this.helpTopic = (string)info.GetValue("helpTopic", typeof(string));
		}

		/// <summary>Gets the Help file associated with the warning.</summary>
		/// <returns>The Help file associated with the warning.</returns>
		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x060036F6 RID: 14070 RVA: 0x000EF03D File Offset: 0x000ED23D
		public string HelpUrl
		{
			get
			{
				return this.helpUrl;
			}
		}

		/// <summary>Gets the Help topic associated with the warning.</summary>
		/// <returns>The Help topic associated with the warning.</returns>
		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x060036F7 RID: 14071 RVA: 0x000EF045 File Offset: 0x000ED245
		public string HelpTopic
		{
			get
			{
				return this.helpTopic;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the parameter name and additional exception information.</summary>
		/// <param name="info">Stores the data that was being used to serialize or deserialize the object that the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> was serializing or deserializing.</param>
		/// <param name="context">Describes the source and destination of the stream that generated the exception, as well as a means for serialization to retain that context and an additional caller-defined context.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060036F8 RID: 14072 RVA: 0x000EF04D File Offset: 0x000ED24D
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("helpUrl", this.helpUrl);
			info.AddValue("helpTopic", this.helpTopic);
			base.GetObjectData(info, context);
		}

		// Token: 0x04002AA5 RID: 10917
		private readonly string helpUrl;

		// Token: 0x04002AA6 RID: 10918
		private readonly string helpTopic;
	}
}
