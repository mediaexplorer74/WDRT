using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
	/// <summary>The exception that is thrown when a method attempts to construct a culture that is not available.</summary>
	// Token: 0x020003A8 RID: 936
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class CultureNotFoundException : ArgumentException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with its message string set to a system-supplied message.</summary>
		// Token: 0x06002EBD RID: 11965 RVA: 0x000B3B88 File Offset: 0x000B1D88
		[__DynamicallyInvokable]
		public CultureNotFoundException()
			: base(CultureNotFoundException.DefaultMessage)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with the specified error message.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x06002EBE RID: 11966 RVA: 0x000B3B95 File Offset: 0x000B1D95
		[__DynamicallyInvokable]
		public CultureNotFoundException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message and the name of the parameter that is the cause this exception.</summary>
		/// <param name="paramName">The name of the parameter that is the cause of the current exception.</param>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x06002EBF RID: 11967 RVA: 0x000B3B9E File Offset: 0x000B1D9E
		[__DynamicallyInvokable]
		public CultureNotFoundException(string paramName, string message)
			: base(message, paramName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06002EC0 RID: 11968 RVA: 0x000B3BA8 File Offset: 0x000B1DA8
		[__DynamicallyInvokable]
		public CultureNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture ID, and the name of the parameter that is the cause this exception.</summary>
		/// <param name="paramName">The name of the parameter that is the cause the current exception.</param>
		/// <param name="invalidCultureId">The Culture ID that cannot be found.</param>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x06002EC1 RID: 11969 RVA: 0x000B3BB2 File Offset: 0x000B1DB2
		public CultureNotFoundException(string paramName, int invalidCultureId, string message)
			: base(message, paramName)
		{
			this.m_invalidCultureId = new int?(invalidCultureId);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture ID, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		/// <param name="invalidCultureId">The Culture ID that cannot be found.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06002EC2 RID: 11970 RVA: 0x000B3BC8 File Offset: 0x000B1DC8
		public CultureNotFoundException(string message, int invalidCultureId, Exception innerException)
			: base(message, innerException)
		{
			this.m_invalidCultureId = new int?(invalidCultureId);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture Name, and the name of the parameter that is the cause this exception.</summary>
		/// <param name="paramName">The name of the parameter that is the cause the current exception.</param>
		/// <param name="invalidCultureName">The Culture Name that cannot be found.</param>
		/// <param name="message">The error message to display with this exception.</param>
		// Token: 0x06002EC3 RID: 11971 RVA: 0x000B3BDE File Offset: 0x000B1DDE
		[__DynamicallyInvokable]
		public CultureNotFoundException(string paramName, string invalidCultureName, string message)
			: base(message, paramName)
		{
			this.m_invalidCultureName = invalidCultureName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class with a specified error message, the invalid Culture Name, and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message to display with this exception.</param>
		/// <param name="invalidCultureName">The Culture Name that cannot be found.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06002EC4 RID: 11972 RVA: 0x000B3BEF File Offset: 0x000B1DEF
		[__DynamicallyInvokable]
		public CultureNotFoundException(string message, string invalidCultureName, Exception innerException)
			: base(message, innerException)
		{
			this.m_invalidCultureName = invalidCultureName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureNotFoundException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06002EC5 RID: 11973 RVA: 0x000B3C00 File Offset: 0x000B1E00
		protected CultureNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_invalidCultureId = (int?)info.GetValue("InvalidCultureId", typeof(int?));
			this.m_invalidCultureName = (string)info.GetValue("InvalidCultureName", typeof(string));
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the parameter name and additional exception information.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06002EC6 RID: 11974 RVA: 0x000B3C58 File Offset: 0x000B1E58
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			int? num = null;
			num = this.m_invalidCultureId;
			info.AddValue("InvalidCultureId", num, typeof(int?));
			info.AddValue("InvalidCultureName", this.m_invalidCultureName, typeof(string));
		}

		/// <summary>Gets the culture identifier that cannot be found.</summary>
		/// <returns>The invalid culture identifier.</returns>
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06002EC7 RID: 11975 RVA: 0x000B3CC0 File Offset: 0x000B1EC0
		public virtual int? InvalidCultureId
		{
			get
			{
				return this.m_invalidCultureId;
			}
		}

		/// <summary>Gets the culture name that cannot be found.</summary>
		/// <returns>The invalid culture name.</returns>
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06002EC8 RID: 11976 RVA: 0x000B3CC8 File Offset: 0x000B1EC8
		[__DynamicallyInvokable]
		public virtual string InvalidCultureName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_invalidCultureName;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x000B3CD0 File Offset: 0x000B1ED0
		private static string DefaultMessage
		{
			get
			{
				return Environment.GetResourceString("Argument_CultureNotSupported");
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06002ECA RID: 11978 RVA: 0x000B3CDC File Offset: 0x000B1EDC
		private string FormatedInvalidCultureId
		{
			get
			{
				if (this.InvalidCultureId != null)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0} (0x{0:x4})", this.InvalidCultureId.Value);
				}
				return this.InvalidCultureName;
			}
		}

		/// <summary>Gets the error message that explains the reason for the exception.</summary>
		/// <returns>A text string describing the details of the exception.</returns>
		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x000B3D24 File Offset: 0x000B1F24
		[__DynamicallyInvokable]
		public override string Message
		{
			[__DynamicallyInvokable]
			get
			{
				string message = base.Message;
				if (this.m_invalidCultureId == null && this.m_invalidCultureName == null)
				{
					return message;
				}
				string resourceString = Environment.GetResourceString("Argument_CultureInvalidIdentifier", new object[] { this.FormatedInvalidCultureId });
				if (message == null)
				{
					return resourceString;
				}
				return message + Environment.NewLine + resourceString;
			}
		}

		// Token: 0x0400135E RID: 4958
		private string m_invalidCultureName;

		// Token: 0x0400135F RID: 4959
		private int? m_invalidCultureId;
	}
}
