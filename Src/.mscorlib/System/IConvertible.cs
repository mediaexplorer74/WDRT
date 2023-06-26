using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Defines methods that convert the value of the implementing reference or value type to a common language runtime type that has an equivalent value.</summary>
	// Token: 0x02000103 RID: 259
	[CLSCompliant(false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IConvertible
	{
		/// <summary>Returns the <see cref="T:System.TypeCode" /> for this instance.</summary>
		/// <returns>The enumerated constant that is the <see cref="T:System.TypeCode" /> of the class or value type that implements this interface.</returns>
		// Token: 0x06000FBE RID: 4030
		[__DynamicallyInvokable]
		TypeCode GetTypeCode();

		/// <summary>Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>A Boolean value equivalent to the value of this instance.</returns>
		// Token: 0x06000FBF RID: 4031
		[__DynamicallyInvokable]
		bool ToBoolean(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>A Unicode character equivalent to the value of this instance.</returns>
		// Token: 0x06000FC0 RID: 4032
		[__DynamicallyInvokable]
		char ToChar(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 8-bit signed integer equivalent to the value of this instance.</returns>
		// Token: 0x06000FC1 RID: 4033
		[__DynamicallyInvokable]
		sbyte ToSByte(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 8-bit unsigned integer equivalent to the value of this instance.</returns>
		// Token: 0x06000FC2 RID: 4034
		[__DynamicallyInvokable]
		byte ToByte(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 16-bit signed integer equivalent to the value of this instance.</returns>
		// Token: 0x06000FC3 RID: 4035
		[__DynamicallyInvokable]
		short ToInt16(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 16-bit unsigned integer equivalent to the value of this instance.</returns>
		// Token: 0x06000FC4 RID: 4036
		[__DynamicallyInvokable]
		ushort ToUInt16(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 32-bit signed integer equivalent to the value of this instance.</returns>
		// Token: 0x06000FC5 RID: 4037
		[__DynamicallyInvokable]
		int ToInt32(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 32-bit unsigned integer equivalent to the value of this instance.</returns>
		// Token: 0x06000FC6 RID: 4038
		[__DynamicallyInvokable]
		uint ToUInt32(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 64-bit signed integer equivalent to the value of this instance.</returns>
		// Token: 0x06000FC7 RID: 4039
		[__DynamicallyInvokable]
		long ToInt64(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An 64-bit unsigned integer equivalent to the value of this instance.</returns>
		// Token: 0x06000FC8 RID: 4040
		[__DynamicallyInvokable]
		ulong ToUInt64(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>A single-precision floating-point number equivalent to the value of this instance.</returns>
		// Token: 0x06000FC9 RID: 4041
		[__DynamicallyInvokable]
		float ToSingle(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent double-precision floating-point number using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>A double-precision floating-point number equivalent to the value of this instance.</returns>
		// Token: 0x06000FCA RID: 4042
		[__DynamicallyInvokable]
		double ToDouble(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent <see cref="T:System.Decimal" /> number using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>A <see cref="T:System.Decimal" /> number equivalent to the value of this instance.</returns>
		// Token: 0x06000FCB RID: 4043
		[__DynamicallyInvokable]
		decimal ToDecimal(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent <see cref="T:System.DateTime" /> using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>A <see cref="T:System.DateTime" /> instance equivalent to the value of this instance.</returns>
		// Token: 0x06000FCC RID: 4044
		[__DynamicallyInvokable]
		DateTime ToDateTime(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an equivalent <see cref="T:System.String" /> using the specified culture-specific formatting information.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>A <see cref="T:System.String" /> instance equivalent to the value of this instance.</returns>
		// Token: 0x06000FCD RID: 4045
		[__DynamicallyInvokable]
		string ToString(IFormatProvider provider);

		/// <summary>Converts the value of this instance to an <see cref="T:System.Object" /> of the specified <see cref="T:System.Type" /> that has an equivalent value, using the specified culture-specific formatting information.</summary>
		/// <param name="conversionType">The <see cref="T:System.Type" /> to which the value of this instance is converted.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>An <see cref="T:System.Object" /> instance of type <paramref name="conversionType" /> whose value is equivalent to the value of this instance.</returns>
		// Token: 0x06000FCE RID: 4046
		[__DynamicallyInvokable]
		object ToType(Type conversionType, IFormatProvider provider);
	}
}
