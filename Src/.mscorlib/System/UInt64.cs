using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	/// <summary>Represents a 64-bit unsigned integer.</summary>
	// Token: 0x02000152 RID: 338
	[CLSCompliant(false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct UInt64 : IComparable, IFormattable, IConvertible, IComparable<ulong>, IEquatable<ulong>
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
		///   <paramref name="value" /> is not a <see cref="T:System.UInt64" />.</exception>
		// Token: 0x06001521 RID: 5409 RVA: 0x0003E288 File Offset: 0x0003C488
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is ulong))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeUInt64"));
			}
			ulong num = (ulong)value;
			if (this < num)
			{
				return -1;
			}
			if (this > num)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified 64-bit unsigned integer and returns an indication of their relative values.</summary>
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
		// Token: 0x06001522 RID: 5410 RVA: 0x0003E2C8 File Offset: 0x0003C4C8
		[__DynamicallyInvokable]
		public int CompareTo(ulong value)
		{
			if (this < value)
			{
				return -1;
			}
			if (this > value)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see cref="T:System.UInt64" /> and equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001523 RID: 5411 RVA: 0x0003E2D9 File Offset: 0x0003C4D9
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is ulong && this == (ulong)obj;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.UInt64" /> value.</summary>
		/// <param name="obj">A <see cref="T:System.UInt64" /> value to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> has the same value as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001524 RID: 5412 RVA: 0x0003E2EF File Offset: 0x0003C4EF
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(ulong obj)
		{
			return this == obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001525 RID: 5413 RVA: 0x0003E2F6 File Offset: 0x0003C4F6
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this ^ (int)(this >> 32);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance, consisting of a sequence of digits ranging from 0 to 9, without a sign or leading zeroes.</returns>
		// Token: 0x06001526 RID: 5414 RVA: 0x0003E302 File Offset: 0x0003C502
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatUInt64(this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance, consisting of a sequence of digits ranging from 0 to 9, without a sign or leading zeros.</returns>
		// Token: 0x06001527 RID: 5415 RVA: 0x0003E311 File Offset: 0x0003C511
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatUInt64(this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format.</summary>
		/// <param name="format">A numeric format string.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter is invalid.</exception>
		// Token: 0x06001528 RID: 5416 RVA: 0x0003E321 File Offset: 0x0003C521
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatUInt64(this, format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <param name="format">A numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about this instance.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter is invalid.</exception>
		// Token: 0x06001529 RID: 5417 RVA: 0x0003E330 File Offset: 0x0003C530
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt64(this, format, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its 64-bit unsigned integer equivalent.</summary>
		/// <param name="s">A string that represents the number to convert.</param>
		/// <returns>A 64-bit unsigned integer equivalent to the number contained in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The <paramref name="s" /> parameter is not in the correct format.</exception>
		/// <exception cref="T:System.OverflowException">The <paramref name="s" /> parameter represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x0600152A RID: 5418 RVA: 0x0003E340 File Offset: 0x0003C540
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong Parse(string s)
		{
			return Number.ParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its 64-bit unsigned integer equivalent.</summary>
		/// <param name="s">A string that represents the number to convert. The string is interpreted by using the style specified by the <paramref name="style" /> parameter.</param>
		/// <param name="style">A bitwise combination of the enumeration values that specifies the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <returns>A 64-bit unsigned integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">The <paramref name="s" /> parameter is not in a format compliant with <paramref name="style" />.</exception>
		/// <exception cref="T:System.OverflowException">The <paramref name="s" /> parameter represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.  
		///  -or-  
		///  <paramref name="s" /> includes non-zero, fractional digits.</exception>
		// Token: 0x0600152B RID: 5419 RVA: 0x0003E34E File Offset: 0x0003C54E
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseUInt64(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its 64-bit unsigned integer equivalent.</summary>
		/// <param name="s">A string that represents the number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <returns>A 64-bit unsigned integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The <paramref name="s" /> parameter is not in the correct style.</exception>
		/// <exception cref="T:System.OverflowException">The <paramref name="s" /> parameter represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x0600152C RID: 5420 RVA: 0x0003E362 File Offset: 0x0003C562
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong Parse(string s, IFormatProvider provider)
		{
			return Number.ParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its 64-bit unsigned integer equivalent.</summary>
		/// <param name="s">A string that represents the number to convert. The string is interpreted by using the style specified by the <paramref name="style" /> parameter.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <returns>A 64-bit unsigned integer equivalent to the number specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		/// <exception cref="T:System.FormatException">The <paramref name="s" /> parameter is not in a format compliant with <paramref name="style" />.</exception>
		/// <exception cref="T:System.OverflowException">The <paramref name="s" /> parameter represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.  
		///  -or-  
		///  <paramref name="s" /> includes non-zero, fractional digits.</exception>
		// Token: 0x0600152D RID: 5421 RVA: 0x0003E371 File Offset: 0x0003C571
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseUInt64(s, style, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Tries to convert the string representation of a number to its 64-bit unsigned integer equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="s">A string that represents the number to convert.</param>
		/// <param name="result">When this method returns, contains the 64-bit unsigned integer value that is equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, is not of the correct format, or represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. This parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600152E RID: 5422 RVA: 0x0003E386 File Offset: 0x0003C586
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out ulong result)
		{
			return Number.TryParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its 64-bit unsigned integer equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="s">A string that represents the number to convert. The string is interpreted by using the style specified by the <paramref name="style" /> parameter.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <param name="result">When this method returns, contains the 64-bit unsigned integer value equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, is not in a format compliant with <paramref name="style" />, or represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. This parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		// Token: 0x0600152F RID: 5423 RVA: 0x0003E395 File Offset: 0x0003C595
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out ulong result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseUInt64(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.UInt64" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.UInt64" />.</returns>
		// Token: 0x06001530 RID: 5424 RVA: 0x0003E3AB File Offset: 0x0003C5AB
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt64;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the current instance is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001531 RID: 5425 RVA: 0x0003E3AF File Offset: 0x0003C5AF
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Char" />.</returns>
		// Token: 0x06001532 RID: 5426 RVA: 0x0003E3B8 File Offset: 0x0003C5B8
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.SByte" />.</returns>
		// Token: 0x06001533 RID: 5427 RVA: 0x0003E3C1 File Offset: 0x0003C5C1
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		// Token: 0x06001534 RID: 5428 RVA: 0x0003E3CA File Offset: 0x0003C5CA
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int16" />.</returns>
		// Token: 0x06001535 RID: 5429 RVA: 0x0003E3D3 File Offset: 0x0003C5D3
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		// Token: 0x06001536 RID: 5430 RVA: 0x0003E3DC File Offset: 0x0003C5DC
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int32" />.</returns>
		// Token: 0x06001537 RID: 5431 RVA: 0x0003E3E5 File Offset: 0x0003C5E5
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		// Token: 0x06001538 RID: 5432 RVA: 0x0003E3EE File Offset: 0x0003C5EE
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int64" />.</returns>
		// Token: 0x06001539 RID: 5433 RVA: 0x0003E3F7 File Offset: 0x0003C5F7
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, unchanged.</returns>
		// Token: 0x0600153A RID: 5434 RVA: 0x0003E400 File Offset: 0x0003C600
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		// Token: 0x0600153B RID: 5435 RVA: 0x0003E404 File Offset: 0x0003C604
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		// Token: 0x0600153C RID: 5436 RVA: 0x0003E40D File Offset: 0x0003C60D
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		// Token: 0x0600153D RID: 5437 RVA: 0x0003E416 File Offset: 0x0003C616
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x0600153E RID: 5438 RVA: 0x0003E41F File Offset: 0x0003C61F
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "UInt64", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <param name="type">The type to which to convert this <see cref="T:System.UInt64" /> value.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies information about the format of the returned value.</param>
		/// <returns>The value of the current instance, converted to <paramref name="type" />.</returns>
		// Token: 0x0600153F RID: 5439 RVA: 0x0003E446 File Offset: 0x0003C646
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040006F7 RID: 1783
		private ulong m_value;

		/// <summary>Represents the largest possible value of <see cref="T:System.UInt64" />. This field is constant.</summary>
		// Token: 0x040006F8 RID: 1784
		[__DynamicallyInvokable]
		public const ulong MaxValue = 18446744073709551615UL;

		/// <summary>Represents the smallest possible value of <see cref="T:System.UInt64" />. This field is constant.</summary>
		// Token: 0x040006F9 RID: 1785
		[__DynamicallyInvokable]
		public const ulong MinValue = 0UL;
	}
}
