using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>The exception thrown when using invalid arguments that are enumerators.</summary>
	// Token: 0x02000572 RID: 1394
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class InvalidEnumArgumentException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class without a message.</summary>
		// Token: 0x060033C8 RID: 13256 RVA: 0x000E3A5A File Offset: 0x000E1C5A
		public InvalidEnumArgumentException()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class with the specified message.</summary>
		/// <param name="message">The message to display with this exception.</param>
		// Token: 0x060033C9 RID: 13257 RVA: 0x000E3A63 File Offset: 0x000E1C63
		public InvalidEnumArgumentException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class with the specified detailed description and the specified exception.</summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x060033CA RID: 13258 RVA: 0x000E3A6C File Offset: 0x000E1C6C
		public InvalidEnumArgumentException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class with a message generated from the argument, the invalid value, and an enumeration class.</summary>
		/// <param name="argumentName">The name of the argument that caused the exception.</param>
		/// <param name="invalidValue">The value of the argument that failed.</param>
		/// <param name="enumClass">A <see cref="T:System.Type" /> that represents the enumeration class with the valid values.</param>
		// Token: 0x060033CB RID: 13259 RVA: 0x000E3A76 File Offset: 0x000E1C76
		public InvalidEnumArgumentException(string argumentName, int invalidValue, Type enumClass)
			: base(SR.GetString("InvalidEnumArgument", new object[]
			{
				argumentName,
				invalidValue.ToString(CultureInfo.CurrentCulture),
				enumClass.Name
			}), argumentName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidEnumArgumentException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x060033CC RID: 13260 RVA: 0x000E3AAB File Offset: 0x000E1CAB
		protected InvalidEnumArgumentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
