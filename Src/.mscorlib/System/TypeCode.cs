using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the type of an object.</summary>
	// Token: 0x0200014C RID: 332
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum TypeCode
	{
		/// <summary>A null reference.</summary>
		// Token: 0x040006D8 RID: 1752
		[__DynamicallyInvokable]
		Empty,
		/// <summary>A general type representing any reference or value type not explicitly represented by another <see langword="TypeCode" />.</summary>
		// Token: 0x040006D9 RID: 1753
		[__DynamicallyInvokable]
		Object,
		/// <summary>A database null (column) value.</summary>
		// Token: 0x040006DA RID: 1754
		DBNull,
		/// <summary>A simple type representing Boolean values of <see langword="true" /> or <see langword="false" />.</summary>
		// Token: 0x040006DB RID: 1755
		[__DynamicallyInvokable]
		Boolean,
		/// <summary>An integral type representing unsigned 16-bit integers with values between 0 and 65535. The set of possible values for the <see cref="F:System.TypeCode.Char" /> type corresponds to the Unicode character set.</summary>
		// Token: 0x040006DC RID: 1756
		[__DynamicallyInvokable]
		Char,
		/// <summary>An integral type representing signed 8-bit integers with values between -128 and 127.</summary>
		// Token: 0x040006DD RID: 1757
		[__DynamicallyInvokable]
		SByte,
		/// <summary>An integral type representing unsigned 8-bit integers with values between 0 and 255.</summary>
		// Token: 0x040006DE RID: 1758
		[__DynamicallyInvokable]
		Byte,
		/// <summary>An integral type representing signed 16-bit integers with values between -32768 and 32767.</summary>
		// Token: 0x040006DF RID: 1759
		[__DynamicallyInvokable]
		Int16,
		/// <summary>An integral type representing unsigned 16-bit integers with values between 0 and 65535.</summary>
		// Token: 0x040006E0 RID: 1760
		[__DynamicallyInvokable]
		UInt16,
		/// <summary>An integral type representing signed 32-bit integers with values between -2147483648 and 2147483647.</summary>
		// Token: 0x040006E1 RID: 1761
		[__DynamicallyInvokable]
		Int32,
		/// <summary>An integral type representing unsigned 32-bit integers with values between 0 and 4294967295.</summary>
		// Token: 0x040006E2 RID: 1762
		[__DynamicallyInvokable]
		UInt32,
		/// <summary>An integral type representing signed 64-bit integers with values between -9223372036854775808 and 9223372036854775807.</summary>
		// Token: 0x040006E3 RID: 1763
		[__DynamicallyInvokable]
		Int64,
		/// <summary>An integral type representing unsigned 64-bit integers with values between 0 and 18446744073709551615.</summary>
		// Token: 0x040006E4 RID: 1764
		[__DynamicallyInvokable]
		UInt64,
		/// <summary>A floating point type representing values ranging from approximately 1.5 x 10 -45 to 3.4 x 10 38 with a precision of 7 digits.</summary>
		// Token: 0x040006E5 RID: 1765
		[__DynamicallyInvokable]
		Single,
		/// <summary>A floating point type representing values ranging from approximately 5.0 x 10 -324 to 1.7 x 10 308 with a precision of 15-16 digits.</summary>
		// Token: 0x040006E6 RID: 1766
		[__DynamicallyInvokable]
		Double,
		/// <summary>A simple type representing values ranging from 1.0 x 10 -28 to approximately 7.9 x 10 28 with 28-29 significant digits.</summary>
		// Token: 0x040006E7 RID: 1767
		[__DynamicallyInvokable]
		Decimal,
		/// <summary>A type representing a date and time value.</summary>
		// Token: 0x040006E8 RID: 1768
		[__DynamicallyInvokable]
		DateTime,
		/// <summary>A sealed class type representing Unicode character strings.</summary>
		// Token: 0x040006E9 RID: 1769
		[__DynamicallyInvokable]
		String = 18
	}
}
