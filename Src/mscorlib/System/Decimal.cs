using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	/// <summary>Represents a decimal floating-point number.</summary>
	// Token: 0x020000D3 RID: 211
	[ComVisible(true)]
	[NonVersionable]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Decimal : IFormattable, IComparable, IConvertible, IDeserializationCallback, IComparable<decimal>, IEquatable<decimal>
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 32-bit signed integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />.</param>
		// Token: 0x06000D10 RID: 3344 RVA: 0x00027C00 File Offset: 0x00025E00
		[__DynamicallyInvokable]
		public Decimal(int value)
		{
			int num = value;
			if (num >= 0)
			{
				this.flags = 0;
			}
			else
			{
				this.flags = int.MinValue;
				num = -num;
			}
			this.lo = num;
			this.mid = 0;
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 32-bit unsigned integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />.</param>
		// Token: 0x06000D11 RID: 3345 RVA: 0x00027C3F File Offset: 0x00025E3F
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public Decimal(uint value)
		{
			this.flags = 0;
			this.lo = (int)value;
			this.mid = 0;
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 64-bit signed integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />.</param>
		// Token: 0x06000D12 RID: 3346 RVA: 0x00027C60 File Offset: 0x00025E60
		[__DynamicallyInvokable]
		public Decimal(long value)
		{
			long num = value;
			if (num >= 0L)
			{
				this.flags = 0;
			}
			else
			{
				this.flags = int.MinValue;
				num = -num;
			}
			this.lo = (int)num;
			this.mid = (int)(num >> 32);
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 64-bit unsigned integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />.</param>
		// Token: 0x06000D13 RID: 3347 RVA: 0x00027CA5 File Offset: 0x00025EA5
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public Decimal(ulong value)
		{
			this.flags = 0;
			this.lo = (int)value;
			this.mid = (int)(value >> 32);
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified single-precision floating-point number.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />.</exception>
		// Token: 0x06000D14 RID: 3348
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Decimal(float value);

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified double-precision floating-point number.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />.</exception>
		// Token: 0x06000D15 RID: 3349
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Decimal(double value);

		// Token: 0x06000D16 RID: 3350 RVA: 0x00027CC8 File Offset: 0x00025EC8
		internal Decimal(Currency value)
		{
			decimal num = Currency.ToDecimal(value);
			this.lo = num.lo;
			this.mid = num.mid;
			this.hi = num.hi;
			this.flags = num.flags;
		}

		/// <summary>Converts the specified <see cref="T:System.Decimal" /> value to the equivalent OLE Automation Currency value, which is contained in a 64-bit signed integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>A 64-bit signed integer that contains the OLE Automation equivalent of <paramref name="value" />.</returns>
		// Token: 0x06000D17 RID: 3351 RVA: 0x00027D0C File Offset: 0x00025F0C
		[__DynamicallyInvokable]
		public static long ToOACurrency(decimal value)
		{
			return new Currency(value).ToOACurrency();
		}

		/// <summary>Converts the specified 64-bit signed integer, which contains an OLE Automation Currency value, to the equivalent <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="cy">An OLE Automation Currency value.</param>
		/// <returns>A <see cref="T:System.Decimal" /> that contains the equivalent of <paramref name="cy" />.</returns>
		// Token: 0x06000D18 RID: 3352 RVA: 0x00027D27 File Offset: 0x00025F27
		[__DynamicallyInvokable]
		public static decimal FromOACurrency(long cy)
		{
			return Currency.ToDecimal(Currency.FromOACurrency(cy));
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to a decimal value represented in binary and contained in a specified array.</summary>
		/// <param name="bits">An array of 32-bit signed integers containing a representation of a decimal value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bits" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of the <paramref name="bits" /> is not 4.  
		///  -or-  
		///  The representation of the decimal value in <paramref name="bits" /> is not valid.</exception>
		// Token: 0x06000D19 RID: 3353 RVA: 0x00027D34 File Offset: 0x00025F34
		[__DynamicallyInvokable]
		public Decimal(int[] bits)
		{
			this.lo = 0;
			this.mid = 0;
			this.hi = 0;
			this.flags = 0;
			this.SetBits(bits);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00027D5C File Offset: 0x00025F5C
		private void SetBits(int[] bits)
		{
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			if (bits.Length == 4)
			{
				int num = bits[3];
				if ((num & 2130771967) == 0 && (num & 16711680) <= 1835008)
				{
					this.lo = bits[0];
					this.mid = bits[1];
					this.hi = bits[2];
					this.flags = num;
					return;
				}
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_DecBitCtor"));
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> from parameters specifying the instance's constituent parts.</summary>
		/// <param name="lo">The low 32 bits of a 96-bit integer.</param>
		/// <param name="mid">The middle 32 bits of a 96-bit integer.</param>
		/// <param name="hi">The high 32 bits of a 96-bit integer.</param>
		/// <param name="isNegative">
		///   <see langword="true" /> to indicate a negative number; <see langword="false" /> to indicate a positive number.</param>
		/// <param name="scale">A power of 10 ranging from 0 to 28.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scale" /> is greater than 28.</exception>
		// Token: 0x06000D1B RID: 3355 RVA: 0x00027DCC File Offset: 0x00025FCC
		[__DynamicallyInvokable]
		public Decimal(int lo, int mid, int hi, bool isNegative, byte scale)
		{
			if (scale > 28)
			{
				throw new ArgumentOutOfRangeException("scale", Environment.GetResourceString("ArgumentOutOfRange_DecimalScale"));
			}
			this.lo = lo;
			this.mid = mid;
			this.hi = hi;
			this.flags = (int)scale << 16;
			if (isNegative)
			{
				this.flags |= int.MinValue;
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00027E2C File Offset: 0x0002602C
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			try
			{
				this.SetBits(decimal.GetBits(this));
			}
			catch (ArgumentException ex)
			{
				throw new SerializationException(Environment.GetResourceString("Overflow_Decimal"), ex);
			}
		}

		/// <summary>Runs when the deserialization of an object has been completed.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Decimal" /> object contains invalid or corrupted data.</exception>
		// Token: 0x06000D1D RID: 3357 RVA: 0x00027E70 File Offset: 0x00026070
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			try
			{
				this.SetBits(decimal.GetBits(this));
			}
			catch (ArgumentException ex)
			{
				throw new SerializationException(Environment.GetResourceString("Overflow_Decimal"), ex);
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00027EB4 File Offset: 0x000260B4
		private Decimal(int lo, int mid, int hi, int flags)
		{
			if ((flags & 2130771967) == 0 && (flags & 16711680) <= 1835008)
			{
				this.lo = lo;
				this.mid = mid;
				this.hi = hi;
				this.flags = flags;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_DecBitCtor"));
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00027F07 File Offset: 0x00026107
		internal static decimal Abs(decimal d)
		{
			return new decimal(d.lo, d.mid, d.hi, d.flags & int.MaxValue);
		}

		/// <summary>Adds two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The first value to add.</param>
		/// <param name="d2">The second value to add.</param>
		/// <returns>The sum of <paramref name="d1" /> and <paramref name="d2" />.</returns>
		/// <exception cref="T:System.OverflowException">The sum of <paramref name="d1" /> and <paramref name="d2" /> is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000D20 RID: 3360 RVA: 0x00027F2C File Offset: 0x0002612C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static decimal Add(decimal d1, decimal d2)
		{
			decimal.FCallAddSub(ref d1, ref d2, 0);
			return d1;
		}

		// Token: 0x06000D21 RID: 3361
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallAddSub(ref decimal d1, ref decimal d2, byte bSign);

		// Token: 0x06000D22 RID: 3362
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallAddSubOverflowed(ref decimal d1, ref decimal d2, byte bSign, ref bool overflowed);

		/// <summary>Returns the smallest integral value that is greater than or equal to the specified decimal number.</summary>
		/// <param name="d">A decimal number.</param>
		/// <returns>The smallest integral value that is greater than or equal to the <paramref name="d" /> parameter. Note that this method returns a <see cref="T:System.Decimal" /> instead of an integral type.</returns>
		// Token: 0x06000D23 RID: 3363 RVA: 0x00027F39 File Offset: 0x00026139
		[__DynamicallyInvokable]
		public static decimal Ceiling(decimal d)
		{
			return -decimal.Floor(-d);
		}

		/// <summary>Compares two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>A signed number indicating the relative values of <paramref name="d1" /> and <paramref name="d2" />.  
		///   Return value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///  <paramref name="d1" /> is less than <paramref name="d2" />.  
		///
		///   Zero  
		///
		///  <paramref name="d1" /> and <paramref name="d2" /> are equal.  
		///
		///   Greater than zero  
		///
		///  <paramref name="d1" /> is greater than <paramref name="d2" />.</returns>
		// Token: 0x06000D24 RID: 3364 RVA: 0x00027F4B File Offset: 0x0002614B
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static int Compare(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2);
		}

		// Token: 0x06000D25 RID: 3365
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int FCallCompare(ref decimal d1, ref decimal d2);

		/// <summary>Compares this instance to a specified object and returns a comparison of their relative values.</summary>
		/// <param name="value">The object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.  
		///   Return value  
		///
		///   Meaning  
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
		///   <paramref name="value" /> is not a <see cref="T:System.Decimal" />.</exception>
		// Token: 0x06000D26 RID: 3366 RVA: 0x00027F58 File Offset: 0x00026158
		[SecuritySafeCritical]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is decimal))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDecimal"));
			}
			decimal num = (decimal)value;
			return decimal.FCallCompare(ref this, ref num);
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Decimal" /> object and returns a comparison of their relative values.</summary>
		/// <param name="value">The object to compare with this instance.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.  
		///   Return value  
		///
		///   Meaning  
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
		// Token: 0x06000D27 RID: 3367 RVA: 0x00027F91 File Offset: 0x00026191
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public int CompareTo(decimal value)
		{
			return decimal.FCallCompare(ref this, ref value);
		}

		/// <summary>Divides two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The dividend.</param>
		/// <param name="d2">The divisor.</param>
		/// <returns>The result of dividing <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="d2" /> is zero.</exception>
		/// <exception cref="T:System.OverflowException">The return value (that is, the quotient) is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000D28 RID: 3368 RVA: 0x00027F9B File Offset: 0x0002619B
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static decimal Divide(decimal d1, decimal d2)
		{
			decimal.FCallDivide(ref d1, ref d2);
			return d1;
		}

		// Token: 0x06000D29 RID: 3369
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallDivide(ref decimal d1, ref decimal d2);

		// Token: 0x06000D2A RID: 3370
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallDivideOverflowed(ref decimal d1, ref decimal d2, ref bool overflowed);

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Object" /> represent the same type and value.</summary>
		/// <param name="value">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is a <see cref="T:System.Decimal" /> and equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D2B RID: 3371 RVA: 0x00027FA8 File Offset: 0x000261A8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			if (value is decimal)
			{
				decimal num = (decimal)value;
				return decimal.FCallCompare(ref this, ref num) == 0;
			}
			return false;
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Decimal" /> object represent the same value.</summary>
		/// <param name="value">An object to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D2C RID: 3372 RVA: 0x00027FD1 File Offset: 0x000261D1
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool Equals(decimal value)
		{
			return decimal.FCallCompare(ref this, ref value) == 0;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000D2D RID: 3373
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern int GetHashCode();

		/// <summary>Returns a value indicating whether two specified instances of <see cref="T:System.Decimal" /> represent the same value.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> and <paramref name="d2" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D2E RID: 3374 RVA: 0x00027FDE File Offset: 0x000261DE
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static bool Equals(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2) == 0;
		}

		/// <summary>Rounds a specified <see cref="T:System.Decimal" /> number to the closest integer toward negative infinity.</summary>
		/// <param name="d">The value to round.</param>
		/// <returns>If <paramref name="d" /> has a fractional part, the next whole <see cref="T:System.Decimal" /> number toward negative infinity that is less than <paramref name="d" />.  
		///  -or-  
		///  If <paramref name="d" /> doesn't have a fractional part, <paramref name="d" /> is returned unchanged. Note that the method returns an integral value of type <see cref="T:System.Decimal" />.</returns>
		// Token: 0x06000D2F RID: 3375 RVA: 0x00027FEC File Offset: 0x000261EC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static decimal Floor(decimal d)
		{
			decimal.FCallFloor(ref d);
			return d;
		}

		// Token: 0x06000D30 RID: 3376
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallFloor(ref decimal d);

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>A string that represents the value of this instance.</returns>
		// Token: 0x06000D31 RID: 3377 RVA: 0x00027FF6 File Offset: 0x000261F6
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatDecimal(this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.</exception>
		// Token: 0x06000D32 RID: 3378 RVA: 0x00028009 File Offset: 0x00026209
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return Number.FormatDecimal(this, format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		// Token: 0x06000D33 RID: 3379 RVA: 0x0002801C File Offset: 0x0002621C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatDecimal(this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <param name="format">A numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.</exception>
		// Token: 0x06000D34 RID: 3380 RVA: 0x00028030 File Offset: 0x00026230
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatDecimal(this, format, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent.</summary>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <returns>The equivalent to the number contained in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000D35 RID: 3381 RVA: 0x00028044 File Offset: 0x00026244
		[__DynamicallyInvokable]
		public static decimal Parse(string s)
		{
			return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its <see cref="T:System.Decimal" /> equivalent.</summary>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" /> values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Number" />.</param>
		/// <returns>The <see cref="T:System.Decimal" /> number equivalent to the number contained in <paramref name="s" /> as specified by <paramref name="style" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" /></exception>
		// Token: 0x06000D36 RID: 3382 RVA: 0x00028053 File Offset: 0x00026253
		[__DynamicallyInvokable]
		public static decimal Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.ParseDecimal(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent using the specified culture-specific format information.</summary>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific parsing information about <paramref name="s" />.</param>
		/// <returns>The <see cref="T:System.Decimal" /> number equivalent to the number contained in <paramref name="s" /> as specified by <paramref name="provider" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not of the correct format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000D37 RID: 3383 RVA: 0x00028067 File Offset: 0x00026267
		[__DynamicallyInvokable]
		public static decimal Parse(string s, IFormatProvider provider)
		{
			return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent using the specified style and culture-specific format.</summary>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" /> values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Number" />.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that supplies culture-specific information about the format of <paramref name="s" />.</param>
		/// <returns>The <see cref="T:System.Decimal" /> number equivalent to the number contained in <paramref name="s" /> as specified by <paramref name="style" /> and <paramref name="provider" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		// Token: 0x06000D38 RID: 3384 RVA: 0x00028077 File Offset: 0x00026277
		[__DynamicallyInvokable]
		public static decimal Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.ParseDecimal(s, style, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.Decimal" /> number that is equivalent to the numeric value contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, is not a number in a valid format, or represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. This parameter is passed uininitialized; any value originally supplied in <paramref name="result" /> is overwritten.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D39 RID: 3385 RVA: 0x0002808C File Offset: 0x0002628C
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out decimal result)
		{
			return Number.TryParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent using the specified style and culture-specific format. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Number" />.</param>
		/// <param name="provider">An object that supplies culture-specific parsing information about <paramref name="s" />.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.Decimal" /> number that is equivalent to the numeric value contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, is not a number in a format compliant with <paramref name="style" />, or represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. This parameter is passed uininitialized; any value originally supplied in <paramref name="result" /> is overwritten.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		// Token: 0x06000D3A RID: 3386 RVA: 0x0002809C File Offset: 0x0002629C
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out decimal result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.TryParseDecimal(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		/// <summary>Converts the value of a specified instance of <see cref="T:System.Decimal" /> to its equivalent binary representation.</summary>
		/// <param name="d">The value to convert.</param>
		/// <returns>A 32-bit signed integer array with four elements that contain the binary representation of <paramref name="d" />.</returns>
		// Token: 0x06000D3B RID: 3387 RVA: 0x000280B2 File Offset: 0x000262B2
		[__DynamicallyInvokable]
		public static int[] GetBits(decimal d)
		{
			return new int[] { d.lo, d.mid, d.hi, d.flags };
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000280E0 File Offset: 0x000262E0
		internal static void GetBytes(decimal d, byte[] buffer)
		{
			buffer[0] = (byte)d.lo;
			buffer[1] = (byte)(d.lo >> 8);
			buffer[2] = (byte)(d.lo >> 16);
			buffer[3] = (byte)(d.lo >> 24);
			buffer[4] = (byte)d.mid;
			buffer[5] = (byte)(d.mid >> 8);
			buffer[6] = (byte)(d.mid >> 16);
			buffer[7] = (byte)(d.mid >> 24);
			buffer[8] = (byte)d.hi;
			buffer[9] = (byte)(d.hi >> 8);
			buffer[10] = (byte)(d.hi >> 16);
			buffer[11] = (byte)(d.hi >> 24);
			buffer[12] = (byte)d.flags;
			buffer[13] = (byte)(d.flags >> 8);
			buffer[14] = (byte)(d.flags >> 16);
			buffer[15] = (byte)(d.flags >> 24);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x000281B4 File Offset: 0x000263B4
		internal static decimal ToDecimal(byte[] buffer)
		{
			int num = (int)buffer[0] | ((int)buffer[1] << 8) | ((int)buffer[2] << 16) | ((int)buffer[3] << 24);
			int num2 = (int)buffer[4] | ((int)buffer[5] << 8) | ((int)buffer[6] << 16) | ((int)buffer[7] << 24);
			int num3 = (int)buffer[8] | ((int)buffer[9] << 8) | ((int)buffer[10] << 16) | ((int)buffer[11] << 24);
			int num4 = (int)buffer[12] | ((int)buffer[13] << 8) | ((int)buffer[14] << 16) | ((int)buffer[15] << 24);
			return new decimal(num, num2, num3, num4);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00028234 File Offset: 0x00026434
		private static void InternalAddUInt32RawUnchecked(ref decimal value, uint i)
		{
			uint num = (uint)value.lo;
			uint num2 = num + i;
			value.lo = (int)num2;
			if (num2 < num || num2 < i)
			{
				num = (uint)value.mid;
				num2 = num + 1U;
				value.mid = (int)num2;
				if (num2 < num || num2 < 1U)
				{
					value.hi++;
				}
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00028284 File Offset: 0x00026484
		private static uint InternalDivRemUInt32(ref decimal value, uint divisor)
		{
			uint num = 0U;
			if (value.hi != 0)
			{
				ulong num2 = (ulong)value.hi;
				value.hi = (int)((uint)(num2 / (ulong)divisor));
				num = (uint)(num2 % (ulong)divisor);
			}
			if (value.mid != 0 || num != 0U)
			{
				ulong num2 = ((ulong)num << 32) | (ulong)value.mid;
				value.mid = (int)((uint)(num2 / (ulong)divisor));
				num = (uint)(num2 % (ulong)divisor);
			}
			if (value.lo != 0 || num != 0U)
			{
				ulong num2 = ((ulong)num << 32) | (ulong)value.lo;
				value.lo = (int)((uint)(num2 / (ulong)divisor));
				num = (uint)(num2 % (ulong)divisor);
			}
			return num;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0002830C File Offset: 0x0002650C
		private static void InternalRoundFromZero(ref decimal d, int decimalCount)
		{
			int num = (d.flags & 16711680) >> 16;
			int num2 = num - decimalCount;
			if (num2 <= 0)
			{
				return;
			}
			uint num4;
			uint num5;
			do
			{
				int num3 = ((num2 > 9) ? 9 : num2);
				num4 = decimal.Powers10[num3];
				num5 = decimal.InternalDivRemUInt32(ref d, num4);
				num2 -= num3;
			}
			while (num2 > 0);
			if (num5 >= num4 >> 1)
			{
				decimal.InternalAddUInt32RawUnchecked(ref d, 1U);
			}
			d.flags = ((decimalCount << 16) & 16711680) | (d.flags & int.MinValue);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00028382 File Offset: 0x00026582
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static decimal Max(decimal d1, decimal d2)
		{
			if (decimal.FCallCompare(ref d1, ref d2) < 0)
			{
				return d2;
			}
			return d1;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00028393 File Offset: 0x00026593
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static decimal Min(decimal d1, decimal d2)
		{
			if (decimal.FCallCompare(ref d1, ref d2) >= 0)
			{
				return d2;
			}
			return d1;
		}

		/// <summary>Computes the remainder after dividing two <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The dividend.</param>
		/// <param name="d2">The divisor.</param>
		/// <returns>The remainder after dividing <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="d2" /> is zero.</exception>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000D43 RID: 3395 RVA: 0x000283A4 File Offset: 0x000265A4
		[__DynamicallyInvokable]
		public static decimal Remainder(decimal d1, decimal d2)
		{
			d2.flags = (d2.flags & int.MaxValue) | (d1.flags & int.MinValue);
			if (decimal.Abs(d1) < decimal.Abs(d2))
			{
				return d1;
			}
			d1 -= d2;
			if (d1 == 0m)
			{
				d1.flags = (d1.flags & int.MaxValue) | (d2.flags & int.MinValue);
			}
			decimal num = decimal.Truncate(d1 / d2);
			decimal num2 = num * d2;
			decimal num3 = d1 - num2;
			if ((d1.flags & -2147483648) != (num3.flags & -2147483648))
			{
				if (-0.000000000000000000000000001m <= num3 && num3 <= 0.000000000000000000000000001m)
				{
					num3.flags = (num3.flags & int.MaxValue) | (d1.flags & int.MinValue);
				}
				else
				{
					num3 += d2;
				}
			}
			return num3;
		}

		/// <summary>Multiplies two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The multiplicand.</param>
		/// <param name="d2">The multiplier.</param>
		/// <returns>The result of multiplying <paramref name="d1" /> and <paramref name="d2" />.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000D44 RID: 3396 RVA: 0x000284A4 File Offset: 0x000266A4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static decimal Multiply(decimal d1, decimal d2)
		{
			decimal.FCallMultiply(ref d1, ref d2);
			return d1;
		}

		// Token: 0x06000D45 RID: 3397
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallMultiply(ref decimal d1, ref decimal d2);

		// Token: 0x06000D46 RID: 3398
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallMultiplyOverflowed(ref decimal d1, ref decimal d2, ref bool overflowed);

		/// <summary>Returns the result of multiplying the specified <see cref="T:System.Decimal" /> value by negative one.</summary>
		/// <param name="d">The value to negate.</param>
		/// <returns>A decimal number with the value of <paramref name="d" />, but the opposite sign.  
		///  -or-  
		///  Zero, if <paramref name="d" /> is zero.</returns>
		// Token: 0x06000D47 RID: 3399 RVA: 0x000284B0 File Offset: 0x000266B0
		[__DynamicallyInvokable]
		public static decimal Negate(decimal d)
		{
			return new decimal(d.lo, d.mid, d.hi, d.flags ^ int.MinValue);
		}

		/// <summary>Rounds a decimal value to the nearest integer.</summary>
		/// <param name="d">A decimal number to round.</param>
		/// <returns>The integer that is nearest to the <paramref name="d" /> parameter. If <paramref name="d" /> is halfway between two integers, one of which is even and the other odd, the even number is returned.</returns>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" /> value.</exception>
		// Token: 0x06000D48 RID: 3400 RVA: 0x000284D5 File Offset: 0x000266D5
		public static decimal Round(decimal d)
		{
			return decimal.Round(d, 0);
		}

		/// <summary>Rounds a <see cref="T:System.Decimal" /> value to a specified number of decimal places.</summary>
		/// <param name="d">A decimal number to round.</param>
		/// <param name="decimals">A value from 0 to 28 that specifies the number of decimal places to round to.</param>
		/// <returns>The decimal number equivalent to <paramref name="d" /> rounded to <paramref name="decimals" /> number of decimal places.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="decimals" /> is not a value from 0 to 28.</exception>
		// Token: 0x06000D49 RID: 3401 RVA: 0x000284DE File Offset: 0x000266DE
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static decimal Round(decimal d, int decimals)
		{
			decimal.FCallRound(ref d, decimals);
			return d;
		}

		/// <summary>Rounds a decimal value to the nearest integer. A parameter specifies how to round the value if it is midway between two other numbers.</summary>
		/// <param name="d">A decimal number to round.</param>
		/// <param name="mode">A value that specifies how to round <paramref name="d" /> if it is midway between two other numbers.</param>
		/// <returns>The integer that is nearest to the <paramref name="d" /> parameter. If <paramref name="d" /> is halfway between two numbers, one of which is even and the other odd, the <paramref name="mode" /> parameter determines which of the two numbers is returned.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a <see cref="T:System.MidpointRounding" /> value.</exception>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" /> object.</exception>
		// Token: 0x06000D4A RID: 3402 RVA: 0x000284E9 File Offset: 0x000266E9
		public static decimal Round(decimal d, MidpointRounding mode)
		{
			return decimal.Round(d, 0, mode);
		}

		/// <summary>Rounds a decimal value to a specified precision. A parameter specifies how to round the value if it is midway between two other numbers.</summary>
		/// <param name="d">A decimal number to round.</param>
		/// <param name="decimals">The number of significant decimal places (precision) in the return value.</param>
		/// <param name="mode">A value that specifies how to round <paramref name="d" /> if it is midway between two other numbers.</param>
		/// <returns>The number that is nearest to the <paramref name="d" /> parameter with a precision equal to the <paramref name="decimals" /> parameter. If <paramref name="d" /> is halfway between two numbers, one of which is even and the other odd, the <paramref name="mode" /> parameter determines which of the two numbers is returned. If the precision of <paramref name="d" /> is less than <paramref name="decimals" />, <paramref name="d" /> is returned unchanged.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="decimals" /> is less than 0 or greater than 28.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a <see cref="T:System.MidpointRounding" /> value.</exception>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" /> object.</exception>
		// Token: 0x06000D4B RID: 3403 RVA: 0x000284F4 File Offset: 0x000266F4
		[SecuritySafeCritical]
		public static decimal Round(decimal d, int decimals, MidpointRounding mode)
		{
			if (decimals < 0 || decimals > 28)
			{
				throw new ArgumentOutOfRangeException("decimals", Environment.GetResourceString("ArgumentOutOfRange_DecimalRound"));
			}
			if (mode < MidpointRounding.ToEven || mode > MidpointRounding.AwayFromZero)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidEnumValue", new object[] { mode, "MidpointRounding" }), "mode");
			}
			if (mode == MidpointRounding.ToEven)
			{
				decimal.FCallRound(ref d, decimals);
			}
			else
			{
				decimal.InternalRoundFromZero(ref d, decimals);
			}
			return d;
		}

		// Token: 0x06000D4C RID: 3404
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallRound(ref decimal d, int decimals);

		/// <summary>Subtracts one specified <see cref="T:System.Decimal" /> value from another.</summary>
		/// <param name="d1">The minuend.</param>
		/// <param name="d2">The subtrahend.</param>
		/// <returns>The result of subtracting <paramref name="d2" /> from <paramref name="d1" />.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000D4D RID: 3405 RVA: 0x00028569 File Offset: 0x00026769
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static decimal Subtract(decimal d1, decimal d2)
		{
			decimal.FCallAddSub(ref d1, ref d2, 128);
			return d1;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>An 8-bit unsigned integer equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x06000D4E RID: 3406 RVA: 0x0002857C File Offset: 0x0002677C
		[__DynamicallyInvokable]
		public static byte ToByte(decimal value)
		{
			uint num;
			try
			{
				num = decimal.ToUInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"), ex);
			}
			if (num < 0U || num > 255U)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Byte"));
			}
			return (byte)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 8-bit signed integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>An 8-bit signed integer equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x06000D4F RID: 3407 RVA: 0x000285D4 File Offset: 0x000267D4
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte ToSByte(decimal value)
		{
			int num;
			try
			{
				num = decimal.ToInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"), ex);
			}
			if (num < -128 || num > 127)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
			}
			return (sbyte)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 16-bit signed integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>A 16-bit signed integer equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x06000D50 RID: 3408 RVA: 0x00028628 File Offset: 0x00026828
		[__DynamicallyInvokable]
		public static short ToInt16(decimal value)
		{
			int num;
			try
			{
				num = decimal.ToInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"), ex);
			}
			if (num < -32768 || num > 32767)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Int16"));
			}
			return (short)num;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00028684 File Offset: 0x00026884
		[SecuritySafeCritical]
		internal static Currency ToCurrency(decimal d)
		{
			Currency currency = default(Currency);
			decimal.FCallToCurrency(ref currency, d);
			return currency;
		}

		// Token: 0x06000D52 RID: 3410
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallToCurrency(ref Currency result, decimal d);

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent double-precision floating-point number.</summary>
		/// <param name="d">The decimal number to convert.</param>
		/// <returns>A double-precision floating-point number equivalent to <paramref name="d" />.</returns>
		// Token: 0x06000D53 RID: 3411
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double ToDouble(decimal d);

		// Token: 0x06000D54 RID: 3412
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int FCallToInt32(decimal d);

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 32-bit signed integer.</summary>
		/// <param name="d">The decimal number to convert.</param>
		/// <returns>A 32-bit signed integer equivalent to the value of <paramref name="d" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000D55 RID: 3413 RVA: 0x000286A4 File Offset: 0x000268A4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static int ToInt32(decimal d)
		{
			if ((d.flags & 16711680) != 0)
			{
				decimal.FCallTruncate(ref d);
			}
			if (d.hi == 0 && d.mid == 0)
			{
				int num = d.lo;
				if (d.flags >= 0)
				{
					if (num >= 0)
					{
						return num;
					}
				}
				else
				{
					num = -num;
					if (num <= 0)
					{
						return num;
					}
				}
			}
			throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 64-bit signed integer.</summary>
		/// <param name="d">The decimal number to convert.</param>
		/// <returns>A 64-bit signed integer equivalent to the value of <paramref name="d" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06000D56 RID: 3414 RVA: 0x00028704 File Offset: 0x00026904
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static long ToInt64(decimal d)
		{
			if ((d.flags & 16711680) != 0)
			{
				decimal.FCallTruncate(ref d);
			}
			if (d.hi == 0)
			{
				long num = ((long)d.lo & (long)((ulong)(-1))) | ((long)d.mid << 32);
				if (d.flags >= 0)
				{
					if (num >= 0L)
					{
						return num;
					}
				}
				else
				{
					num = -num;
					if (num <= 0L)
					{
						return num;
					}
				}
			}
			throw new OverflowException(Environment.GetResourceString("Overflow_Int64"));
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>A 16-bit unsigned integer equivalent to the value of <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.UInt16.MaxValue" /> or less than <see cref="F:System.UInt16.MinValue" />.</exception>
		// Token: 0x06000D57 RID: 3415 RVA: 0x00028770 File Offset: 0x00026970
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort ToUInt16(decimal value)
		{
			uint num;
			try
			{
				num = decimal.ToUInt32(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"), ex);
			}
			if (num < 0U || num > 65535U)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_UInt16"));
			}
			return (ushort)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 32-bit unsigned integer.</summary>
		/// <param name="d">The decimal number to convert.</param>
		/// <returns>A 32-bit unsigned integer equivalent to the value of <paramref name="d" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is negative or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x06000D58 RID: 3416 RVA: 0x000287C8 File Offset: 0x000269C8
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint ToUInt32(decimal d)
		{
			if ((d.flags & 16711680) != 0)
			{
				decimal.FCallTruncate(ref d);
			}
			if (d.hi == 0 && d.mid == 0)
			{
				uint num = (uint)d.lo;
				if (d.flags >= 0 || num == 0U)
				{
					return num;
				}
			}
			throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 64-bit unsigned integer.</summary>
		/// <param name="d">The decimal number to convert.</param>
		/// <returns>A 64-bit unsigned integer equivalent to the value of <paramref name="d" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is negative or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x06000D59 RID: 3417 RVA: 0x00028820 File Offset: 0x00026A20
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ulong ToUInt64(decimal d)
		{
			if ((d.flags & 16711680) != 0)
			{
				decimal.FCallTruncate(ref d);
			}
			if (d.hi == 0)
			{
				ulong num = (ulong)d.lo | ((ulong)d.mid << 32);
				if (d.flags >= 0 || num == 0UL)
				{
					return num;
				}
			}
			throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent single-precision floating-point number.</summary>
		/// <param name="d">The decimal number to convert.</param>
		/// <returns>A single-precision floating-point number equivalent to the value of <paramref name="d" />.</returns>
		// Token: 0x06000D5A RID: 3418
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float ToSingle(decimal d);

		/// <summary>Returns the integral digits of the specified <see cref="T:System.Decimal" />; any fractional digits are discarded.</summary>
		/// <param name="d">The decimal number to truncate.</param>
		/// <returns>The result of <paramref name="d" /> rounded toward zero, to the nearest whole number.</returns>
		// Token: 0x06000D5B RID: 3419 RVA: 0x0002887A File Offset: 0x00026A7A
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static decimal Truncate(decimal d)
		{
			decimal.FCallTruncate(ref d);
			return d;
		}

		// Token: 0x06000D5C RID: 3420
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallTruncate(ref decimal d);

		/// <summary>Defines an implicit conversion of an 8-bit unsigned integer to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>The converted 8-bit unsigned integer.</returns>
		// Token: 0x06000D5D RID: 3421 RVA: 0x00028884 File Offset: 0x00026A84
		[__DynamicallyInvokable]
		public static implicit operator decimal(byte value)
		{
			return new decimal((int)value);
		}

		/// <summary>Defines an implicit conversion of an 8-bit signed integer to a <see cref="T:System.Decimal" />.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>The converted 8-bit signed integer.</returns>
		// Token: 0x06000D5E RID: 3422 RVA: 0x0002888C File Offset: 0x00026A8C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static implicit operator decimal(sbyte value)
		{
			return new decimal((int)value);
		}

		/// <summary>Defines an implicit conversion of a 16-bit signed integer to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>The converted 16-bit signed integer.</returns>
		// Token: 0x06000D5F RID: 3423 RVA: 0x00028894 File Offset: 0x00026A94
		[__DynamicallyInvokable]
		public static implicit operator decimal(short value)
		{
			return new decimal((int)value);
		}

		/// <summary>Defines an implicit conversion of a 16-bit unsigned integer to a <see cref="T:System.Decimal" />.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>The converted 16-bit unsigned integer.</returns>
		// Token: 0x06000D60 RID: 3424 RVA: 0x0002889C File Offset: 0x00026A9C
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static implicit operator decimal(ushort value)
		{
			return new decimal((int)value);
		}

		/// <summary>Defines an implicit conversion of a Unicode character to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>The converted Unicode character.</returns>
		// Token: 0x06000D61 RID: 3425 RVA: 0x000288A4 File Offset: 0x00026AA4
		[__DynamicallyInvokable]
		public static implicit operator decimal(char value)
		{
			return new decimal((int)value);
		}

		/// <summary>Defines an implicit conversion of a 32-bit signed integer to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>The converted 32-bit signed integer.</returns>
		// Token: 0x06000D62 RID: 3426 RVA: 0x000288AC File Offset: 0x00026AAC
		[__DynamicallyInvokable]
		public static implicit operator decimal(int value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an implicit conversion of a 32-bit unsigned integer to a <see cref="T:System.Decimal" />.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>The converted 32-bit unsigned integer.</returns>
		// Token: 0x06000D63 RID: 3427 RVA: 0x000288B4 File Offset: 0x00026AB4
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static implicit operator decimal(uint value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an implicit conversion of a 64-bit signed integer to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>The converted 64-bit signed integer.</returns>
		// Token: 0x06000D64 RID: 3428 RVA: 0x000288BC File Offset: 0x00026ABC
		[__DynamicallyInvokable]
		public static implicit operator decimal(long value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an implicit conversion of a 64-bit unsigned integer to a <see cref="T:System.Decimal" />.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>The converted 64-bit unsigned integer.</returns>
		// Token: 0x06000D65 RID: 3429 RVA: 0x000288C4 File Offset: 0x00026AC4
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static implicit operator decimal(ulong value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a single-precision floating-point number to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The single-precision floating-point number to convert.</param>
		/// <returns>The converted single-precision floating point number.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />.</exception>
		// Token: 0x06000D66 RID: 3430 RVA: 0x000288CC File Offset: 0x00026ACC
		[__DynamicallyInvokable]
		public static explicit operator decimal(float value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a double-precision floating-point number to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>The converted double-precision floating point number.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />.</exception>
		// Token: 0x06000D67 RID: 3431 RVA: 0x000288D4 File Offset: 0x00026AD4
		[__DynamicallyInvokable]
		public static explicit operator decimal(double value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to an 8-bit unsigned integer.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>An 8-bit unsigned integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x06000D68 RID: 3432 RVA: 0x000288DC File Offset: 0x00026ADC
		[__DynamicallyInvokable]
		public static explicit operator byte(decimal value)
		{
			return decimal.ToByte(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to an 8-bit signed integer.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>An 8-bit signed integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x06000D69 RID: 3433 RVA: 0x000288E4 File Offset: 0x00026AE4
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator sbyte(decimal value)
		{
			return decimal.ToSByte(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a Unicode character.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A Unicode character that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />.</exception>
		// Token: 0x06000D6A RID: 3434 RVA: 0x000288EC File Offset: 0x00026AEC
		[__DynamicallyInvokable]
		public static explicit operator char(decimal value)
		{
			ushort num;
			try
			{
				num = decimal.ToUInt16(value);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Char"), ex);
			}
			return (char)num;
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 16-bit signed integer.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A 16-bit signed integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x06000D6B RID: 3435 RVA: 0x00028928 File Offset: 0x00026B28
		[__DynamicallyInvokable]
		public static explicit operator short(decimal value)
		{
			return decimal.ToInt16(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 16-bit unsigned integer.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A 16-bit unsigned integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x06000D6C RID: 3436 RVA: 0x00028930 File Offset: 0x00026B30
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator ushort(decimal value)
		{
			return decimal.ToUInt16(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 32-bit signed integer.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A 32-bit signed integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000D6D RID: 3437 RVA: 0x00028938 File Offset: 0x00026B38
		[__DynamicallyInvokable]
		public static explicit operator int(decimal value)
		{
			return decimal.ToInt32(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 32-bit unsigned integer.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A 32-bit unsigned integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x06000D6E RID: 3438 RVA: 0x00028940 File Offset: 0x00026B40
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator uint(decimal value)
		{
			return decimal.ToUInt32(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 64-bit signed integer.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A 64-bit signed integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06000D6F RID: 3439 RVA: 0x00028948 File Offset: 0x00026B48
		[__DynamicallyInvokable]
		public static explicit operator long(decimal value)
		{
			return decimal.ToInt64(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 64-bit unsigned integer.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A 64-bit unsigned integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is negative or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x06000D70 RID: 3440 RVA: 0x00028950 File Offset: 0x00026B50
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static explicit operator ulong(decimal value)
		{
			return decimal.ToUInt64(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a single-precision floating-point number.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A single-precision floating-point number that represents the converted <see cref="T:System.Decimal" />.</returns>
		// Token: 0x06000D71 RID: 3441 RVA: 0x00028958 File Offset: 0x00026B58
		[__DynamicallyInvokable]
		public static explicit operator float(decimal value)
		{
			return decimal.ToSingle(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a double-precision floating-point number.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A double-precision floating-point number that represents the converted <see cref="T:System.Decimal" />.</returns>
		// Token: 0x06000D72 RID: 3442 RVA: 0x00028960 File Offset: 0x00026B60
		[__DynamicallyInvokable]
		public static explicit operator double(decimal value)
		{
			return decimal.ToDouble(value);
		}

		/// <summary>Returns the value of the <see cref="T:System.Decimal" /> operand (the sign of the operand is unchanged).</summary>
		/// <param name="d">The operand to return.</param>
		/// <returns>The value of the operand, <paramref name="d" />.</returns>
		// Token: 0x06000D73 RID: 3443 RVA: 0x00028968 File Offset: 0x00026B68
		[__DynamicallyInvokable]
		public static decimal operator +(decimal d)
		{
			return d;
		}

		/// <summary>Negates the value of the specified <see cref="T:System.Decimal" /> operand.</summary>
		/// <param name="d">The value to negate.</param>
		/// <returns>The result of <paramref name="d" /> multiplied by negative one (-1).</returns>
		// Token: 0x06000D74 RID: 3444 RVA: 0x0002896B File Offset: 0x00026B6B
		[__DynamicallyInvokable]
		public static decimal operator -(decimal d)
		{
			return decimal.Negate(d);
		}

		/// <summary>Increments the <see cref="T:System.Decimal" /> operand by 1.</summary>
		/// <param name="d">The value to increment.</param>
		/// <returns>The value of <paramref name="d" /> incremented by 1.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000D75 RID: 3445 RVA: 0x00028973 File Offset: 0x00026B73
		[__DynamicallyInvokable]
		public static decimal operator ++(decimal d)
		{
			return decimal.Add(d, 1m);
		}

		/// <summary>Decrements the <see cref="T:System.Decimal" /> operand by one.</summary>
		/// <param name="d">The value to decrement.</param>
		/// <returns>The value of <paramref name="d" /> decremented by 1.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000D76 RID: 3446 RVA: 0x00028980 File Offset: 0x00026B80
		[__DynamicallyInvokable]
		public static decimal operator --(decimal d)
		{
			return decimal.Subtract(d, 1m);
		}

		/// <summary>Adds two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The first value to add.</param>
		/// <param name="d2">The second value to add.</param>
		/// <returns>The result of adding <paramref name="d1" /> and <paramref name="d2" />.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000D77 RID: 3447 RVA: 0x0002898D File Offset: 0x00026B8D
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static decimal operator +(decimal d1, decimal d2)
		{
			decimal.FCallAddSub(ref d1, ref d2, 0);
			return d1;
		}

		/// <summary>Subtracts two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The minuend.</param>
		/// <param name="d2">The subtrahend.</param>
		/// <returns>The result of subtracting <paramref name="d2" /> from <paramref name="d1" />.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000D78 RID: 3448 RVA: 0x0002899A File Offset: 0x00026B9A
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static decimal operator -(decimal d1, decimal d2)
		{
			decimal.FCallAddSub(ref d1, ref d2, 128);
			return d1;
		}

		/// <summary>Multiplies two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The first value to multiply.</param>
		/// <param name="d2">The second value to multiply.</param>
		/// <returns>The result of multiplying <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000D79 RID: 3449 RVA: 0x000289AB File Offset: 0x00026BAB
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static decimal operator *(decimal d1, decimal d2)
		{
			decimal.FCallMultiply(ref d1, ref d2);
			return d1;
		}

		/// <summary>Divides two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The dividend.</param>
		/// <param name="d2">The divisor.</param>
		/// <returns>The result of dividing <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="d2" /> is zero.</exception>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000D7A RID: 3450 RVA: 0x000289B7 File Offset: 0x00026BB7
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static decimal operator /(decimal d1, decimal d2)
		{
			decimal.FCallDivide(ref d1, ref d2);
			return d1;
		}

		/// <summary>Returns the remainder resulting from dividing two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The dividend.</param>
		/// <param name="d2">The divisor.</param>
		/// <returns>The remainder resulting from dividing <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="d2" /> is <see langword="zero" />.</exception>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000D7B RID: 3451 RVA: 0x000289C3 File Offset: 0x00026BC3
		[__DynamicallyInvokable]
		public static decimal operator %(decimal d1, decimal d2)
		{
			return decimal.Remainder(d1, d2);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Decimal" /> values are equal.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> and <paramref name="d2" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D7C RID: 3452 RVA: 0x000289CC File Offset: 0x00026BCC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static bool operator ==(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2) == 0;
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Decimal" /> objects have different values.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> and <paramref name="d2" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D7D RID: 3453 RVA: 0x000289DA File Offset: 0x00026BDA
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static bool operator !=(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2) != 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is less than another specified <see cref="T:System.Decimal" />.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> is less than <paramref name="d2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D7E RID: 3454 RVA: 0x000289E8 File Offset: 0x00026BE8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static bool operator <(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2) < 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is less than or equal to another specified <see cref="T:System.Decimal" />.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> is less than or equal to <paramref name="d2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D7F RID: 3455 RVA: 0x000289F6 File Offset: 0x00026BF6
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static bool operator <=(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2) <= 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is greater than another specified <see cref="T:System.Decimal" />.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> is greater than <paramref name="d2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D80 RID: 3456 RVA: 0x00028A07 File Offset: 0x00026C07
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static bool operator >(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2) > 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is greater than or equal to another specified <see cref="T:System.Decimal" />.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> is greater than or equal to <paramref name="d2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D81 RID: 3457 RVA: 0x00028A15 File Offset: 0x00026C15
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static bool operator >=(decimal d1, decimal d2)
		{
			return decimal.FCallCompare(ref d1, ref d2) >= 0;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Decimal" />.</summary>
		/// <returns>The enumerated constant <see cref="F:System.TypeCode.Decimal" />.</returns>
		// Token: 0x06000D82 RID: 3458 RVA: 0x00028A26 File Offset: 0x00026C26
		public TypeCode GetTypeCode()
		{
			return TypeCode.Decimal;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the current instance is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D83 RID: 3459 RVA: 0x00028A2A File Offset: 0x00026C2A
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>None. This conversion is not supported.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000D84 RID: 3460 RVA: 0x00028A37 File Offset: 0x00026C37
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Decimal", "Char" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.SByte" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x06000D85 RID: 3461 RVA: 0x00028A5E File Offset: 0x00026C5E
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x06000D86 RID: 3462 RVA: 0x00028A6B File Offset: 0x00026C6B
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Int16" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x06000D87 RID: 3463 RVA: 0x00028A78 File Offset: 0x00026C78
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x06000D88 RID: 3464 RVA: 0x00028A85 File Offset: 0x00026C85
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">The parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Int32" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000D89 RID: 3465 RVA: 0x00028A92 File Offset: 0x00026C92
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x06000D8A RID: 3466 RVA: 0x00028A9F File Offset: 0x00026C9F
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Int64" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06000D8B RID: 3467 RVA: 0x00028AAC File Offset: 0x00026CAC
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x06000D8C RID: 3468 RVA: 0x00028AB9 File Offset: 0x00026CB9
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		// Token: 0x06000D8D RID: 3469 RVA: 0x00028AC6 File Offset: 0x00026CC6
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		// Token: 0x06000D8E RID: 3470 RVA: 0x00028AD3 File Offset: 0x00026CD3
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, unchanged.</returns>
		// Token: 0x06000D8F RID: 3471 RVA: 0x00028AE0 File Offset: 0x00026CE0
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>None. This conversion is not supported.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000D90 RID: 3472 RVA: 0x00028AE8 File Offset: 0x00026CE8
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Decimal", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <param name="type">The type to which to convert the value of this <see cref="T:System.Decimal" /> instance.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies culture-specific information about the format of the returned value.</param>
		/// <returns>The value of the current instance, converted to a <paramref name="type" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The requested type conversion is not supported.</exception>
		// Token: 0x06000D91 RID: 3473 RVA: 0x00028B0F File Offset: 0x00026D0F
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x0400054B RID: 1355
		private const int SignMask = -2147483648;

		// Token: 0x0400054C RID: 1356
		private const byte DECIMAL_NEG = 128;

		// Token: 0x0400054D RID: 1357
		private const byte DECIMAL_ADD = 0;

		// Token: 0x0400054E RID: 1358
		private const int ScaleMask = 16711680;

		// Token: 0x0400054F RID: 1359
		private const int ScaleShift = 16;

		// Token: 0x04000550 RID: 1360
		private const int MaxInt32Scale = 9;

		// Token: 0x04000551 RID: 1361
		private static uint[] Powers10 = new uint[] { 1U, 10U, 100U, 1000U, 10000U, 100000U, 1000000U, 10000000U, 100000000U, 1000000000U };

		/// <summary>Represents the number zero (0).</summary>
		// Token: 0x04000552 RID: 1362
		[__DynamicallyInvokable]
		public const decimal Zero = 0m;

		/// <summary>Represents the number one (1).</summary>
		// Token: 0x04000553 RID: 1363
		[__DynamicallyInvokable]
		public const decimal One = 1m;

		/// <summary>Represents the number negative one (-1).</summary>
		// Token: 0x04000554 RID: 1364
		[__DynamicallyInvokable]
		public const decimal MinusOne = -1m;

		/// <summary>Represents the largest possible value of <see cref="T:System.Decimal" />. This field is constant and read-only.</summary>
		// Token: 0x04000555 RID: 1365
		[__DynamicallyInvokable]
		public const decimal MaxValue = 79228162514264337593543950335m;

		/// <summary>Represents the smallest possible value of <see cref="T:System.Decimal" />. This field is constant and read-only.</summary>
		// Token: 0x04000556 RID: 1366
		[__DynamicallyInvokable]
		public const decimal MinValue = -79228162514264337593543950335m;

		// Token: 0x04000557 RID: 1367
		private const decimal NearNegativeZero = -0.000000000000000000000000001m;

		// Token: 0x04000558 RID: 1368
		private const decimal NearPositiveZero = 0.000000000000000000000000001m;

		// Token: 0x04000559 RID: 1369
		private int flags;

		// Token: 0x0400055A RID: 1370
		private int hi;

		// Token: 0x0400055B RID: 1371
		private int lo;

		// Token: 0x0400055C RID: 1372
		private int mid;
	}
}
