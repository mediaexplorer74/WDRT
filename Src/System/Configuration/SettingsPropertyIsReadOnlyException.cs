using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Provides an exception for read-only <see cref="T:System.Configuration.SettingsProperty" /> objects.</summary>
	// Token: 0x020000AB RID: 171
	[Serializable]
	public class SettingsPropertyIsReadOnlyException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class based on a supplied parameter.</summary>
		/// <param name="message">A string containing an exception message.</param>
		// Token: 0x060005DE RID: 1502 RVA: 0x000232CF File Offset: 0x000214CF
		public SettingsPropertyIsReadOnlyException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class based on supplied parameters.</summary>
		/// <param name="message">A string containing an exception message.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x060005DF RID: 1503 RVA: 0x000232D8 File Offset: 0x000214D8
		public SettingsPropertyIsReadOnlyException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class based on the supplied parameters.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination of the serialized stream.</param>
		// Token: 0x060005E0 RID: 1504 RVA: 0x000232E2 File Offset: 0x000214E2
		protected SettingsPropertyIsReadOnlyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class.</summary>
		// Token: 0x060005E1 RID: 1505 RVA: 0x000232EC File Offset: 0x000214EC
		public SettingsPropertyIsReadOnlyException()
		{
		}
	}
}
