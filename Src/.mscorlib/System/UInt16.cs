using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	/// <summary>Represents a 16-bit unsigned integer.</summary>
	// Token: 0x02000150 RID: 336
	[CLSCompliant(false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct UInt16 : IComparable, IFormattable, IConvertible, IComparable<ushort>, IEquatable<ushort>
	{
		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <param name="value">An object to compare, or <see langword="null" />.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.  
		///   Return Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   This instance is less than <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///   Greater than zero  
		///
		///   This instance is greater than <paramref name="value" />.  
		///
		///  -or-  
		///
		///  <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.UInt16" />.</exception>
		// Token: 0x060014E1 RID: 5345 RVA: 0x0003DE95 File Offset: 0x0003C095
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is ushort)
			{
				return (int)(this - (ushort)value);
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_MustBeUInt16"));
		}

		/// <summary>Compares this instance to a specified 16-bit unsigned integer and returns an indication of their relative values.</summary>
		/// <param name="value">An unsigned integer to compare.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.  
		///   Return Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   This instance is less than <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///   Greater than zero  
		///
		///   This instance is greater than <paramref name="value" />.</returns>
		// Token: 0x060014E2 RID: 5346 RVA: 0x0003DEBD File Offset: 0x0003C0BD
		[__DynamicallyInvokable]
		public int CompareTo(ushort value)
		{
			return (int)(this - value);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see cref="T:System.UInt16" /> and equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014E3 RID: 5347 RVA: 0x0003DEC3 File Offset: 0x0003C0C3
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is ushort && this == (ushort)obj;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.UInt16" /> value.</summary>
		/// <param name="obj">A 16-bit unsigned integer to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> has the same value as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014E4 RID: 5348 RVA: 0x0003DED9 File Offset: 0x0003C0D9
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(ushort obj)
		{
			return this == obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060014E5 RID: 5349 RVA: 0x0003DEE0 File Offset: 0x0003C0E0
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this;
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance, which consists of a sequence of digits ranging from 0 to 9, without a sign or leading zeros.</returns>
		// Token: 0x060014E6 RID: 5350 RVA: 0x0003DEE4 File Offset: 0x0003C0E4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatUInt32((uint)this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance, which consists of a sequence of digits ranging from 0 to 9, without a sign or leading zeros.</returns>
		// Token: 0x060014E7 RID: 5351 RVA: 0x0003DEF3 File Offset: 0x0003C0F3
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatUInt32((uint)this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format.</summary>
		/// <param name="format">A numeric format string.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter is invalid.</exception>
		// Token: 0x060014E8 RID: 5352 RVA: 0x0003DF03 File Offset: 0x0003C103
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatUInt32((uint)this, format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <param name="format">A numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance, as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.</exception>
		// Token: 0x060014E9 RID: 5353 RVA: 0x0003DF12 File Offset: 0x0003C112
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt32((uint)this, format, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its 16-bit unsigned integer equivalent.</summary>
		/// <param name="s">A string that represents the number to convert.</param>
		/// <returns>A 16-bit unsigned integer equivalent to the number contained in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x060014EA RID: 5354 RVA: 0x0003DF22 File Offset: 0x0003C122
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort Parse(string s)
		{
			return ushort.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its 16-bit unsigned integer equivalent.  
		///  This method is not CLS-compliant. The CLS-compliant alternative is <see cref="M:System.Int32.Parse(System.String,System.Globalization.NumberStyles)" />.</summary>
		/// <param name="s">A string that represents the number to convert. The string is interpreted by using the style specified by the <paramref name="style" /> parameter.</param>
		/// <param name="style">A bitwise combination of the enumeration values that specify the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <returns>A 16-bit unsigned integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format compliant with <paramref name="style" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-  
		/// <paramref name="s" /> includes non-zero, fractional digits.</exception>
		// Token: 0x060014EB RID: 5355 RVA: 0x0003DF30 File Offset: 0x0003C130
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return ushort.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its 16-bit unsigned integer equivalent.</summary>
		/// <param name="s">A string that represents the number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <returns>A 16-bit unsigned integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x060014EC RID: 5356 RVA: 0x0003DF44 File Offset: 0x0003C144
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort Parse(string s, IFormatProvider provider)
		{
			return ushort.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its 16-bit unsigned integer equivalent.</summary>
		/// <param name="s">A string that represents the number to convert. The string is interpreted by using the style specified by the <paramref name="style" /> parameter.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicate the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <returns>A 16-bit unsigned integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format compliant with <paramref name="style" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.  
		/// -or-  
		/// <paramref name="s" /> includes non-zero, fractional digits.</exception>
		// Token: 0x060014ED RID: 5357 RVA: 0x0003DF53 File Offset: 0x0003C153
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return ushort.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0003DF68 File Offset: 0x0003C168
		private static ushort Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			uint num = 0U;
			try
			{
				num = Number.ParseUInt32(s, style, info);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"), ex);
			}
			if (num > 65535U)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)num;
		}

		/// <summary>Tries to convert the string representation of a number to its 16-bit unsigned integer equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="s">A string that represents the number to convert.</param>
		/// <param name="result">When this method returns, contains the 16-bit unsigned integer value that is equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, is not in the correct format. , or represents a number less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. This parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014EF RID: 5359 RVA: 0x0003DFC0 File Offset: 0x0003C1C0
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out ushort result)
		{
			return ushort.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its 16-bit unsigned integer equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="s">A string that represents the number to convert. The string is interpreted by using the style specified by the <paramref name="style" /> parameter.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <param name="result">When this method returns, contains the 16-bit unsigned integer value equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, is not in a format compliant with <paramref name="style" />, or represents a number less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. This parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		// Token: 0x060014F0 RID: 5360 RVA: 0x0003DFCF File Offset: 0x0003C1CF
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ushort result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return ushort.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0003DFE8 File Offset: 0x0003C1E8
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out ushort result)
		{
			result = 0;
			uint num;
			if (!Number.TryParseUInt32(s, style, info, out num))
			{
				return false;
			}
			if (num > 65535U)
			{
				return false;
			}
			result = (ushort)num;
			return true;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.UInt16" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.UInt16" />.</returns>
		// Token: 0x060014F2 RID: 5362 RVA: 0x0003E015 File Offset: 0x0003C215
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt16;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the current instance is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014F3 RID: 5363 RVA: 0x0003E018 File Offset: 0x0003C218
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Char" />.</returns>
		// Token: 0x060014F4 RID: 5364 RVA: 0x0003E021 File Offset: 0x0003C221
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The current value of this instance, converted to an <see cref="T:System.SByte" />.</returns>
		// Token: 0x060014F5 RID: 5365 RVA: 0x0003E02A File Offset: 0x0003C22A
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		// Token: 0x060014F6 RID: 5366 RVA: 0x0003E033 File Offset: 0x0003C233
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The current value of this instance, converted to an <see cref="T:System.Int16" />.</returns>
		// Token: 0x060014F7 RID: 5367 RVA: 0x0003E03C File Offset: 0x0003C23C
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The current value of this instance, unchanged.</returns>
		// Token: 0x060014F8 RID: 5368 RVA: 0x0003E045 File Offset: 0x0003C245
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of this instance, converted to an <see cref="T:System.Int32" />.</returns>
		// Token: 0x060014F9 RID: 5369 RVA: 0x0003E049 File Offset: 0x0003C249
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The current value of this instance, converted to a <see cref="T:System.UInt32" />.</returns>
		// Token: 0x060014FA RID: 5370 RVA: 0x0003E052 File Offset: 0x0003C252
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The current value of this instance, converted to an <see cref="T:System.Int64" />.</returns>
		// Token: 0x060014FB RID: 5371 RVA: 0x0003E05B File Offset: 0x0003C25B
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The current value of this instance, converted to a <see cref="T:System.UInt64" />.</returns>
		// Token: 0x060014FC RID: 5372 RVA: 0x0003E064 File Offset: 0x0003C264
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The current value pf this instance, converted to a <see cref="T:System.Single" />.</returns>
		// Token: 0x060014FD RID: 5373 RVA: 0x0003E06D File Offset: 0x0003C26D
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The current value of this instance, converted to a <see cref="T:System.Double" />.</returns>
		// Token: 0x060014FE RID: 5374 RVA: 0x0003E076 File Offset: 0x0003C276
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The current value of this instance, converted to a <see cref="T:System.Decimal" />.</returns>
		// Token: 0x060014FF RID: 5375 RVA: 0x0003E07F File Offset: 0x0003C27F
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06001500 RID: 5376 RVA: 0x0003E088 File Offset: 0x0003C288
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "UInt16", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <param name="type">The type to which to convert this <see cref="T:System.UInt16" /> value.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies information about the format of the returned value.</param>
		/// <returns>The current value of this instance, converted to <paramref name="type" />.</returns>
		// Token: 0x06001501 RID: 5377 RVA: 0x0003E0AF File Offset: 0x0003C2AF
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040006F1 RID: 1777
		private ushort m_value;

		/// <summary>Represents the largest possible value of <see cref="T:System.UInt16" />. This field is constant.</summary>
		// Token: 0x040006F2 RID: 1778
		[__DynamicallyInvokable]
		public const ushort MaxValue = 65535;

		/// <summary>Represents the smallest possible value of <see cref="T:System.UInt16" />. This field is constant.</summary>
		// Token: 0x040006F3 RID: 1779
		[__DynamicallyInvokable]
		public const ushort MinValue = 0;
	}
}
