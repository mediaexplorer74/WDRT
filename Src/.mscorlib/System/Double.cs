using System;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	/// <summary>Represents a double-precision floating-point number.</summary>
	// Token: 0x020000D7 RID: 215
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Double : IComparable, IFormattable, IConvertible, IComparable<double>, IEquatable<double>
	{
		/// <summary>Returns a value indicating whether the specified number evaluates to negative or positive infinity</summary>
		/// <param name="d">A double-precision floating-point number.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d" /> evaluates to <see cref="F:System.Double.PositiveInfinity" /> or <see cref="F:System.Double.NegativeInfinity" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DB3 RID: 3507 RVA: 0x0002A4E2 File Offset: 0x000286E2
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsInfinity(double d)
		{
			return (*(long*)(&d) & long.MaxValue) == 9218868437227405312L;
		}

		/// <summary>Returns a value indicating whether the specified number evaluates to positive infinity.</summary>
		/// <param name="d">A double-precision floating-point number.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d" /> evaluates to <see cref="F:System.Double.PositiveInfinity" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DB4 RID: 3508 RVA: 0x0002A4FD File Offset: 0x000286FD
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool IsPositiveInfinity(double d)
		{
			return d == double.PositiveInfinity;
		}

		/// <summary>Returns a value indicating whether the specified number evaluates to negative infinity.</summary>
		/// <param name="d">A double-precision floating-point number.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d" /> evaluates to <see cref="F:System.Double.NegativeInfinity" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DB5 RID: 3509 RVA: 0x0002A50E File Offset: 0x0002870E
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool IsNegativeInfinity(double d)
		{
			return d == double.NegativeInfinity;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0002A51F File Offset: 0x0002871F
		[SecuritySafeCritical]
		internal unsafe static bool IsNegative(double d)
		{
			return (*(long*)(&d) & long.MinValue) == long.MinValue;
		}

		/// <summary>Returns a value that indicates whether the specified value is not a number (<see cref="F:System.Double.NaN" />).</summary>
		/// <param name="d">A double-precision floating-point number.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d" /> evaluates to <see cref="F:System.Double.NaN" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DB7 RID: 3511 RVA: 0x0002A53A File Offset: 0x0002873A
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[NonVersionable]
		[__DynamicallyInvokable]
		public unsafe static bool IsNaN(double d)
		{
			return (*(long*)(&d) & long.MaxValue) > 9218868437227405312L;
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified object.</summary>
		/// <param name="value">An object to compare, or <see langword="null" />.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.  
		///   Value  
		///
		///   Description  
		///
		///   A negative integer  
		///
		///   This instance is less than <paramref name="value" />.  
		///
		///  -or-  
		///
		///  This instance is not a number (<see cref="F:System.Double.NaN" />) and <paramref name="value" /> is a number.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///  -or-  
		///
		///  This instance and <paramref name="value" /> are both <see langword="Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" /> A positive integer  
		///
		///   This instance is greater than <paramref name="value" />.  
		///
		///  -or-  
		///
		///  This instance is a number and <paramref name="value" /> is not a number (<see cref="F:System.Double.NaN" />).  
		///
		///  -or-  
		///
		///  <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Double" />.</exception>
		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002A558 File Offset: 0x00028758
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is double))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDouble"));
			}
			double num = (double)value;
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
			if (!double.IsNaN(this))
			{
				return 1;
			}
			if (!double.IsNaN(num))
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified double-precision floating-point number and returns an integer that indicates whether the value of this instance is less than, equal to, or greater than the value of the specified double-precision floating-point number.</summary>
		/// <param name="value">A double-precision floating-point number to compare.</param>
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
		///  This instance is not a number (<see cref="F:System.Double.NaN" />) and <paramref name="value" /> is a number.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///  -or-  
		///
		///  Both this instance and <paramref name="value" /> are not a number (<see cref="F:System.Double.NaN" />), <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />.  
		///
		///   Greater than zero  
		///
		///   This instance is greater than <paramref name="value" />.  
		///
		///  -or-  
		///
		///  This instance is a number and <paramref name="value" /> is not a number (<see cref="F:System.Double.NaN" />).</returns>
		// Token: 0x06000DB9 RID: 3513 RVA: 0x0002A5B4 File Offset: 0x000287B4
		[__DynamicallyInvokable]
		public int CompareTo(double value)
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
			if (!double.IsNaN(this))
			{
				return 1;
			}
			if (!double.IsNaN(value))
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see cref="T:System.Double" /> and equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DBA RID: 3514 RVA: 0x0002A5E4 File Offset: 0x000287E4
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (!(obj is double))
			{
				return false;
			}
			double num = (double)obj;
			return num == this || (double.IsNaN(num) && double.IsNaN(this));
		}

		/// <summary>Returns a value that indicates whether two specified <see cref="T:System.Double" /> values are equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DBB RID: 3515 RVA: 0x0002A61A File Offset: 0x0002881A
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator ==(double left, double right)
		{
			return left == right;
		}

		/// <summary>Returns a value that indicates whether two specified <see cref="T:System.Double" /> values are not equal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DBC RID: 3516 RVA: 0x0002A620 File Offset: 0x00028820
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator !=(double left, double right)
		{
			return left != right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Double" /> value is less than another specified <see cref="T:System.Double" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DBD RID: 3517 RVA: 0x0002A629 File Offset: 0x00028829
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator <(double left, double right)
		{
			return left < right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Double" /> value is greater than another specified <see cref="T:System.Double" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DBE RID: 3518 RVA: 0x0002A62F File Offset: 0x0002882F
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator >(double left, double right)
		{
			return left > right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Double" /> value is less than or equal to another specified <see cref="T:System.Double" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DBF RID: 3519 RVA: 0x0002A635 File Offset: 0x00028835
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator <=(double left, double right)
		{
			return left <= right;
		}

		/// <summary>Returns a value that indicates whether a specified <see cref="T:System.Double" /> value is greater than or equal to another specified <see cref="T:System.Double" /> value.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is greater than or equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DC0 RID: 3520 RVA: 0x0002A63E File Offset: 0x0002883E
		[NonVersionable]
		[__DynamicallyInvokable]
		public static bool operator >=(double left, double right)
		{
			return left >= right;
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Double" /> object represent the same value.</summary>
		/// <param name="obj">A <see cref="T:System.Double" /> object to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DC1 RID: 3521 RVA: 0x0002A647 File Offset: 0x00028847
		[__DynamicallyInvokable]
		public bool Equals(double obj)
		{
			return obj == this || (double.IsNaN(obj) && double.IsNaN(this));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000DC2 RID: 3522 RVA: 0x0002A664 File Offset: 0x00028864
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetHashCode()
		{
			double num = this;
			if (num == 0.0)
			{
				return 0;
			}
			long num2 = *(long*)(&num);
			return (int)num2 ^ (int)(num2 >> 32);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		// Token: 0x06000DC3 RID: 3523 RVA: 0x0002A68F File Offset: 0x0002888F
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatDouble(this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
		/// <param name="format">A numeric format string.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.</exception>
		// Token: 0x06000DC4 RID: 3524 RVA: 0x0002A69E File Offset: 0x0002889E
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatDouble(this, format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		// Token: 0x06000DC5 RID: 3525 RVA: 0x0002A6AD File Offset: 0x000288AD
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatDouble(this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <param name="format">A numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		// Token: 0x06000DC6 RID: 3526 RVA: 0x0002A6BD File Offset: 0x000288BD
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatDouble(this, format, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its double-precision floating-point number equivalent.</summary>
		/// <param name="s">A string that contains a number to convert.</param>
		/// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />.</exception>
		// Token: 0x06000DC7 RID: 3527 RVA: 0x0002A6CD File Offset: 0x000288CD
		[__DynamicallyInvokable]
		public static double Parse(string s)
		{
			return double.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its double-precision floating-point number equivalent.</summary>
		/// <param name="s">A string that contains a number to convert.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicate the style elements that can be present in <paramref name="s" />. A typical value to specify is a combination of <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> includes the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		// Token: 0x06000DC8 RID: 3528 RVA: 0x0002A6DF File Offset: 0x000288DF
		[__DynamicallyInvokable]
		public static double Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return double.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its double-precision floating-point number equivalent.</summary>
		/// <param name="s">A string that contains a number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />.</exception>
		// Token: 0x06000DC9 RID: 3529 RVA: 0x0002A6F3 File Offset: 0x000288F3
		[__DynamicallyInvokable]
		public static double Parse(string s, IFormatProvider provider)
		{
			return double.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its double-precision floating-point number equivalent.</summary>
		/// <param name="s">A string that contains a number to convert.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicate the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <returns>A double-precision floating-point number that is equivalent to the numeric value or symbol specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a numeric value.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />.</exception>
		// Token: 0x06000DCA RID: 3530 RVA: 0x0002A706 File Offset: 0x00028906
		[__DynamicallyInvokable]
		public static double Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return double.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0002A71B File Offset: 0x0002891B
		private static double Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			return Number.ParseDouble(s, style, info);
		}

		/// <summary>Converts the string representation of a number to its double-precision floating-point number equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="s">A string containing a number to convert.</param>
		/// <param name="result">When this method returns, contains the double-precision floating-point number equivalent of the <paramref name="s" /> parameter, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, is not a number in a valid format, or represents a number less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. This parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DCC RID: 3532 RVA: 0x0002A725 File Offset: 0x00028925
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out double result)
		{
			return double.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Converts the string representation of a number in a specified style and culture-specific format to its double-precision floating-point number equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="s">A string containing a number to convert.</param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" /> values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Float" /> combined with <see cref="F:System.Globalization.NumberStyles.AllowThousands" />.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <param name="result">When this method returns, contains a double-precision floating-point number equivalent of the numeric value or symbol contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, is not in a format compliant with <paramref name="style" />, represents a number less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />, or if <paramref name="style" /> is not a valid combination of <see cref="T:System.Globalization.NumberStyles" /> enumerated constants. This parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> includes the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		// Token: 0x06000DCD RID: 3533 RVA: 0x0002A738 File Offset: 0x00028938
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out double result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return double.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0002A750 File Offset: 0x00028950
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out double result)
		{
			if (s == null)
			{
				result = 0.0;
				return false;
			}
			if (!Number.TryParseDouble(s, style, info, out result))
			{
				string text = s.Trim();
				if (text.Equals(info.PositiveInfinitySymbol))
				{
					result = double.PositiveInfinity;
				}
				else if (text.Equals(info.NegativeInfinitySymbol))
				{
					result = double.NegativeInfinity;
				}
				else
				{
					if (!text.Equals(info.NaNSymbol))
					{
						return false;
					}
					result = double.NaN;
				}
			}
			return true;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Double" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Double" />.</returns>
		// Token: 0x06000DCF RID: 3535 RVA: 0x0002A7D5 File Offset: 0x000289D5
		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the current instance is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000DD0 RID: 3536 RVA: 0x0002A7D9 File Offset: 0x000289D9
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000DD1 RID: 3537 RVA: 0x0002A7E2 File Offset: 0x000289E2
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Double", "Char" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.SByte" />.</returns>
		// Token: 0x06000DD2 RID: 3538 RVA: 0x0002A809 File Offset: 0x00028A09
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		// Token: 0x06000DD3 RID: 3539 RVA: 0x0002A812 File Offset: 0x00028A12
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int16" />.</returns>
		// Token: 0x06000DD4 RID: 3540 RVA: 0x0002A81B File Offset: 0x00028A1B
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		// Token: 0x06000DD5 RID: 3541 RVA: 0x0002A824 File Offset: 0x00028A24
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int32" />.</returns>
		// Token: 0x06000DD6 RID: 3542 RVA: 0x0002A82D File Offset: 0x00028A2D
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		// Token: 0x06000DD7 RID: 3543 RVA: 0x0002A836 File Offset: 0x00028A36
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int64" />.</returns>
		// Token: 0x06000DD8 RID: 3544 RVA: 0x0002A83F File Offset: 0x00028A3F
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		// Token: 0x06000DD9 RID: 3545 RVA: 0x0002A848 File Offset: 0x00028A48
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		// Token: 0x06000DDA RID: 3546 RVA: 0x0002A851 File Offset: 0x00028A51
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, unchanged.</returns>
		// Token: 0x06000DDB RID: 3547 RVA: 0x0002A85A File Offset: 0x00028A5A
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		// Token: 0x06000DDC RID: 3548 RVA: 0x0002A85E File Offset: 0x00028A5E
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" /></summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000DDD RID: 3549 RVA: 0x0002A867 File Offset: 0x00028A67
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Double", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <param name="type">The type to which to convert this <see cref="T:System.Double" /> value.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies culture-specific information about the format of the returned value.</param>
		/// <returns>The value of the current instance, converted to <paramref name="type" />.</returns>
		// Token: 0x06000DDE RID: 3550 RVA: 0x0002A88E File Offset: 0x00028A8E
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x0400055F RID: 1375
		internal double m_value;

		/// <summary>Represents the smallest possible value of a <see cref="T:System.Double" />. This field is constant.</summary>
		// Token: 0x04000560 RID: 1376
		[__DynamicallyInvokable]
		public const double MinValue = -1.7976931348623157E+308;

		/// <summary>Represents the largest possible value of a <see cref="T:System.Double" />. This field is constant.</summary>
		// Token: 0x04000561 RID: 1377
		[__DynamicallyInvokable]
		public const double MaxValue = 1.7976931348623157E+308;

		/// <summary>Represents the smallest positive <see cref="T:System.Double" /> value that is greater than zero. This field is constant.</summary>
		// Token: 0x04000562 RID: 1378
		[__DynamicallyInvokable]
		public const double Epsilon = 4.94065645841247E-324;

		/// <summary>Represents negative infinity. This field is constant.</summary>
		// Token: 0x04000563 RID: 1379
		[__DynamicallyInvokable]
		public const double NegativeInfinity = double.NegativeInfinity;

		/// <summary>Represents positive infinity. This field is constant.</summary>
		// Token: 0x04000564 RID: 1380
		[__DynamicallyInvokable]
		public const double PositiveInfinity = double.PositiveInfinity;

		/// <summary>Represents a value that is not a number (<see langword="NaN" />). This field is constant.</summary>
		// Token: 0x04000565 RID: 1381
		[__DynamicallyInvokable]
		public const double NaN = double.NaN;

		// Token: 0x04000566 RID: 1382
		internal static double NegativeZero = BitConverter.Int64BitsToDouble(long.MinValue);
	}
}
