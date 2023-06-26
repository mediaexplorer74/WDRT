using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Provides an exception that is thrown when an invalid type is used with a <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
	// Token: 0x020000AF RID: 175
	[Serializable]
	public class SettingsPropertyWrongTypeException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameter.</summary>
		/// <param name="message">A string containing an exception message.</param>
		// Token: 0x06000604 RID: 1540 RVA: 0x00023C4A File Offset: 0x00021E4A
		public SettingsPropertyWrongTypeException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameters.</summary>
		/// <param name="message">A string containing an exception message.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06000605 RID: 1541 RVA: 0x00023C53 File Offset: 0x00021E53
		public SettingsPropertyWrongTypeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameters.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination of the serialized stream.</param>
		// Token: 0x06000606 RID: 1542 RVA: 0x00023C5D File Offset: 0x00021E5D
		protected SettingsPropertyWrongTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class.</summary>
		// Token: 0x06000607 RID: 1543 RVA: 0x00023C67 File Offset: 0x00021E67
		public SettingsPropertyWrongTypeException()
		{
		}
	}
}
