﻿using System;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	/// <summary>Represents a single-precision floating-point number.</summary>
	// Token: 0x0200013F RID: 319
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Single : IComparable, IFormattable, IConvertible, IComparable<float>, IEquatable<float>
	{
		/// <summary>Returns a value indicating whether the specified number evaluates to negative or positive infinity.</summary>
		/// <param name="f">A single-precision floating-point number.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="f" /> evaluates to <see cref="F:System.Single.PositiveInfinity" /> or <see cref="F:System.Single.NegativeInfinity" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001314 RID: 4884 RVA: 0x00038464 File Offset: 0x00036664
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsInfinity(float f)
		{
			return (*(int*)(&f) & int.MaxValue) == 2139095040;
		}

		/// <summary>Returns a value indicating whether the specified number evaluates to positive infinity.</summary>
		/// <param name="f">A single-precision floating-point number.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="f" /> evaluates to <see cref="F:System.Single.PositiveInfinity" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001315 RID: 4885 RVA: 0x00038477 File Offset: 0x00036677
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsPositiveInfinity(float f)
		{
			return *(int*)(&f) == 2139095040;
		}

		/// <summary>Returns a value indicating whether the specified number evaluates to negative infinity.</summary>
		/// <param name="f">A single-precision floating-point number.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="f" /> evaluates to <see cref="F:System.Single.NegativeInfinity" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001316 RID: 4886 RVA: 0x00038484 File Offset: 0x00036684
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsNegativeInfinity(float f)
		{
			return *(int*)(&f) == -8388608;
		}

		/// <summary>Returns a value that indicates whether the specified value is not a number (<see cref="F:System.Single.NaN" />).</summary>
		/// <param name="f">A single-precision floating-point number.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="f" /> evaluates to not a number (<see cref="F:System.Single.NaN" />); otherwise, <see langword="false" />.</returns>
		// Token: 0x06001317 RID: 4887 RVA: 0x00038491 File Offset: 0x00036691
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsNaN(float f)
		{
			return (*(int*)(&f) & int.MaxValue) > 2139095040;
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified object.</summary>
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
		///  -or-  
		///
		///  This instance is not a number (<see cref="F:System.Single.NaN" />) and <paramref name="value" /> is a number.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///  -or-  
		///
		///  This instance and value are both not a number (<see cref="F:System.Single.NaN" />), <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />.  
		///
		///   Greater than zero  
		///
		///   This instance is greater than <paramref name="value" />.  
		///
		///  -or-  
		///
		///  This instance is a number and <paramref name="value" /> is not a number (<see cref="F:System.Single.NaN" />).  
		///
		///  -or-  
		///
		///  <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Single" />.</exception>
		// Token: 0x06001318 RID: 4888 RVA: 0x000384A4 File Offset: 0x000366A4
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is float))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeSingle"));
			}
			float num = (float)value;
			if (this < num)
			{
				return -1;
			}
			if (this > num)
			{
				return 1;
			}
			if (this == num)
			{
				return 0;
			}
			if (!float.IsNaN(this))
			{
				return 1;
			}
			if (!float.IsNaN(num))
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified single-precision floating-point number and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified single-precision floating-point number.</summary>
		/// <param name="value">A single-precision floating-point number to compare.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.  
		///   Return Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   This instance is less than <paramref name="value" />.  
		///
		///  -or-  
		///
		///  This instance is not a number (<see cref="F:System.Single.NaN" />) and <paramref name="value" /> is a number.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///  -or-  
		///
		///  Both this instance and <paramref name="value" /> are not a number (<see cref="F:System.Single.NaN" />), <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />.  
		///
		///   Greater than zero  
		///
		///   This instance is greater than <paramref name="value" />.  
		///
		///  -or-  
		///
		///  This instance is a number and <paramref name="value" /> is not a number (<see cref="F:System.Single.NaN" />).</returns>
		// Token: 0x06001319 RID: 4889 RVA: 0x00038500 File Offset: 0x00036700
		[__DynamicallyInvokable]
		public int CompareTo(float value)
		{
			if (this < value)
			{
				return -1;
			}
			if (this > value)
			{
				return 1;
			}
			if (this == value)
			{
				return 0;
			}
			if (!float.IsNaN(this))
			{
				return 1;
			}
			if (!float.IsNaN(value))
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Returns a value that indicates whether two specified <see cref="T:System.Single" /> values are equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600131A RID: 4890 RVA: 0x0003852D File Offset: 0x0003672D
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator ==(float left, float right)
		{
			return left == right;
		}

		/// <summary>Returns a value that indicates whether two specified <see cref="T:System.Single" /> values are not equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600131B RID: 4891 RVA: 0x00038533 File Offset: 0x00036733
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator !=(float left, float right)
		{
			return left != right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Single" /> value is less than another specified <see cref="T:System.Single" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600131C RID: 4892 RVA: 0x0003853C File Offset: 0x0003673C
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator <(float left, float right)
		{
			return left < right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Single" /> value is greater than another specified <see cref="T:System.Single" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600131D RID: 4893 RVA: 0x00038542 File Offset: 0x00036742
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator >(float left, float right)
		{
			return left > right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Single" /> value is less than or equal to another specified <see cref="T:System.Single" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600131E RID: 4894 RVA: 0x00038548 File Offset: 0x00036748
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator <=(float left, float right)
		{
			return left <= right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Single" /> value is greater than or equal to another specified <see cref="T:System.Single" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is greater than or equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600131F RID: 4895 RVA: 0x00038551 File Offset: 0x00036751
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator >=(float left, float right)
		{
			return left >= right;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see cref="T:System.Single" /> and equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001320 RID: 4896 RVA: 0x0003855C File Offset: 0x0003675C
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (!(obj is float))
			{
				return false;
			}
			float num = (float)obj;
			return num == this || (float.IsNaN(num) && float.IsNaN(this));
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Single" /> object represent the same value.</summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001321 RID: 4897 RVA: 0x00038592 File Offset: 0x00036792
		[__DynamicallyInvokable]
		public bool Equals(float obj)
		{
			return obj == this || (float.IsNaN(obj) && float.IsNaN(this));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001322 RID: 4898 RVA: 0x000385AC File Offset: 0x000367AC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetHashCode()
		{
			float num = this;
			if (num == 0f)
			{
				return 0;
			}
			return *(int*)(&num);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		// Token: 0x06001323 RID: 4899 RVA: 0x000385CC File Offset: 0x000367CC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatSingle(this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		// Token: 0x06001324 RID: 4900 RVA: 0x000385DB File Offset: 0x000367DB
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatSingle(this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
		/// <param name="format">A numeric format string.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.</exception>
		// Token: 0x06001325 RID: 4901 RVA: 0x000385EB File Offset: 0x000367EB
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatSingle(this, format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <param name="format">A numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		// Token: 0x06001326 RID: 4902 RVA: 0x000385FA File Offset: 0x000367FA
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatSingle(this, format, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its single-precision floating-point number equivalent.</summary>
		/// <param name="s">A string that contains a number to convert.</param>
		/// <returns>A single-precision floating-point number equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />.</exception>
		// Token: 0x06001327 RID: 4903 RVA: 0x0003860A File Offset: 0x0003680A
		[__DynamicallyInvokable]
		public static float Parse(string s)
		{
			return float.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its single-precision floating-point number equivalent.</summary>
		/// <param name="s">A string that contains a number to convert.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <returns>A single-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> includes the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		// Token: 0x06001328 RID: 4904 RVA: 0x0003861C File Offset: 0x0003681C
		[__DynamicallyInvokable]
		public static float Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return float.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its single-precision floating-point number equivalent.</summary>
		/// <param name="s">A string that contains a number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <returns>A single-precision floating-point number equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />.</exception>
		// Token: 0x06001329 RID: 4905 RVA: 0x00038630 File Offset: 0x00036830
		[__DynamicallyInvokable]
		public static float Parse(string s, IFormatProvider provider)
		{
			return float.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its single-precision floating-point number equivalent.</summary>
		/// <param name="s">A string that contains a number to convert.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <returns>A single-precision floating-point number equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a numeric value.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />.</exception>
		// Token: 0x0600132A RID: 4906 RVA: 0x00038643 File Offset: 0x00036843
		[__DynamicallyInvokable]
		public static float Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return float.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00038658 File Offset: 0x00036858
		private static float Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			return Number.ParseSingle(s, style, info);
		}

		/// <summary>Converts the string representation of a number to its single-precision floating-point number equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="s">A string representing a number to convert.</param>
		/// <param name="result">When this method returns, contains single-precision floating-point number equivalent to the numeric value or symbol contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, is not a number in a valid format, or represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. This parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600132C RID: 4908 RVA: 0x00038662 File Offset: 0x00036862
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out float result)
		{
			return float.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its single-precision floating-point number equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="s">A string representing a number to convert.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <param name="result">When this method returns, contains the single-precision floating-point number equivalent to the numeric value or symbol contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, is not in a format compliant with <paramref name="style" />, represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />, or if <paramref name="style" /> is not a valid combination of <see cref="T:System.Globalization.NumberStyles" /> enumerated constants. This parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		// Token: 0x0600132D RID: 4909 RVA: 0x00038675 File Offset: 0x00036875
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out float result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return float.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0003868C File Offset: 0x0003688C
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out float result)
		{
			if (s == null)
			{
				result = 0f;
				return false;
			}
			if (!Number.TryParseSingle(s, style, info, out result))
			{
				string text = s.Trim();
				if (text.Equals(info.PositiveInfinitySymbol))
				{
					result = float.PositiveInfinity;
				}
				else if (text.Equals(info.NegativeInfinitySymbol))
				{
					result = float.NegativeInfinity;
				}
				else
				{
					if (!text.Equals(info.NaNSymbol))
					{
						return false;
					}
					result = float.NaN;
				}
			}
			return true;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Single" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Single" />.</returns>
		// Token: 0x0600132F RID: 4911 RVA: 0x00038701 File Offset: 0x00036901
		public TypeCode GetTypeCode()
		{
			return TypeCode.Single;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the current instance is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001330 RID: 4912 RVA: 0x00038705 File Offset: 0x00036905
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06001331 RID: 4913 RVA: 0x0003870E File Offset: 0x0003690E
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Single", "Char" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.SByte" />.</returns>
		// Token: 0x06001332 RID: 4914 RVA: 0x00038735 File Offset: 0x00036935
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		// Token: 0x06001333 RID: 4915 RVA: 0x0003873E File Offset: 0x0003693E
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int16" />.</returns>
		// Token: 0x06001334 RID: 4916 RVA: 0x00038747 File Offset: 0x00036947
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		// Token: 0x06001335 RID: 4917 RVA: 0x00038750 File Offset: 0x00036950
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int32" />.</returns>
		// Token: 0x06001336 RID: 4918 RVA: 0x00038759 File Offset: 0x00036959
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		// Token: 0x06001337 RID: 4919 RVA: 0x00038762 File Offset: 0x00036962
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int64" />.</returns>
		// Token: 0x06001338 RID: 4920 RVA: 0x0003876B File Offset: 0x0003696B
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		// Token: 0x06001339 RID: 4921 RVA: 0x00038774 File Offset: 0x00036974
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, unchanged.</returns>
		// Token: 0x0600133A RID: 4922 RVA: 0x0003877D File Offset: 0x0003697D
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		// Token: 0x0600133B RID: 4923 RVA: 0x00038781 File Offset: 0x00036981
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		// Token: 0x0600133C RID: 4924 RVA: 0x0003878A File Offset: 0x0003698A
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x0600133D RID: 4925 RVA: 0x00038793 File Offset: 0x00036993
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Single", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <param name="type">The type to which to convert this <see cref="T:System.Single" /> value.</param>
		/// <param name="provider">An object that supplies information about the format of the returned value.</param>
		/// <returns>The value of the current instance, converted to <paramref name="type" />.</returns>
		// Token: 0x0600133E RID: 4926 RVA: 0x000387BA File Offset: 0x000369BA
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04000688 RID: 1672
		internal float m_value;

		/// <summary>Represents the smallest possible value of <see cref="T:System.Single" />. This field is constant.</summary>
		// Token: 0x04000689 RID: 1673
		[__DynamicallyInvokable]
		public const float MinValue = -3.40282347E+38f;

		/// <summary>Represents the smallest positive <see cref="T:System.Single" /> value that is greater than zero. This field is constant.</summary>
		// Token: 0x0400068A RID: 1674
		[__DynamicallyInvokable]
		public const float Epsilon = 1.401298E-45f;

		/// <summary>Represents the largest possible value of <see cref="T:System.Single" />. This field is constant.</summary>
		// Token: 0x0400068B RID: 1675
		[__DynamicallyInvokable]
		public const float MaxValue = 3.40282347E+38f;

		/// <summary>Represents positive infinity. This field is constant.</summary>
		// Token: 0x0400068C RID: 1676
		[__DynamicallyInvokable]
		public const float PositiveInfinity = float.PositiveInfinity;

		/// <summary>Represents negative infinity. This field is constant.</summary>
		// Token: 0x0400068D RID: 1677
		[__DynamicallyInvokable]
		public const float NegativeInfinity = float.NegativeInfinity;

		/// <summary>Represents not a number (<see langword="NaN" />). This field is constant.</summary>
		// Token: 0x0400068E RID: 1678
		[__DynamicallyInvokable]
		public const float NaN = float.NaN;
	}
}
