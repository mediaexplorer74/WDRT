using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	/// <summary>Provides constants and static methods for trigonometric, logarithmic, and other common mathematical functions.</summary>
	// Token: 0x0200010B RID: 267
	[__DynamicallyInvokable]
	public static class Math
	{
		/// <summary>Returns the angle whose cosine is the specified number.</summary>
		/// <param name="d">A number representing a cosine, where <paramref name="d" /> must be greater than or equal to -1, but less than or equal to 1.</param>
		/// <returns>An angle, θ, measured in radians, such that 0 ≤θ≤π  
		///  -or-  
		///  <see cref="F:System.Double.NaN" /> if <paramref name="d" /> &lt; -1 or <paramref name="d" /> &gt; 1 or <paramref name="d" /> equals <see cref="F:System.Double.NaN" />.</returns>
		// Token: 0x06000FFD RID: 4093
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Acos(double d);

		/// <summary>Returns the angle whose sine is the specified number.</summary>
		/// <param name="d">A number representing a sine, where <paramref name="d" /> must be greater than or equal to -1, but less than or equal to 1.</param>
		/// <returns>An angle, θ, measured in radians, such that -π/2 ≤θ≤π/2  
		///  -or-  
		///  <see cref="F:System.Double.NaN" /> if <paramref name="d" /> &lt; -1 or <paramref name="d" /> &gt; 1 or <paramref name="d" /> equals <see cref="F:System.Double.NaN" />.</returns>
		// Token: 0x06000FFE RID: 4094
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Asin(double d);

		/// <summary>Returns the angle whose tangent is the specified number.</summary>
		/// <param name="d">A number representing a tangent.</param>
		/// <returns>An angle, θ, measured in radians, such that -π/2 ≤θ≤π/2.  
		///  -or-  
		///  <see cref="F:System.Double.NaN" /> if <paramref name="d" /> equals <see cref="F:System.Double.NaN" />, -π/2 rounded to double precision (-1.5707963267949) if <paramref name="d" /> equals <see cref="F:System.Double.NegativeInfinity" />, or π/2 rounded to double precision (1.5707963267949) if <paramref name="d" /> equals <see cref="F:System.Double.PositiveInfinity" />.</returns>
		// Token: 0x06000FFF RID: 4095
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Atan(double d);

		/// <summary>Returns the angle whose tangent is the quotient of two specified numbers.</summary>
		/// <param name="y">The y coordinate of a point.</param>
		/// <param name="x">The x coordinate of a point.</param>
		/// <returns>An angle, θ, measured in radians, such that -π≤θ≤π, and tan(θ) = <paramref name="y" /> / <paramref name="x" />, where (<paramref name="x" />, <paramref name="y" />) is a point in the Cartesian plane. Observe the following:  
		///
		/// For (<paramref name="x" />, <paramref name="y" />) in quadrant 1, 0 &lt; θ &lt; π/2.  
		///
		/// For (<paramref name="x" />, <paramref name="y" />) in quadrant 2, π/2 &lt; θ≤π.  
		///
		/// For (<paramref name="x" />, <paramref name="y" />) in quadrant 3, -π &lt; θ &lt; -π/2.  
		///
		/// For (<paramref name="x" />, <paramref name="y" />) in quadrant 4, -π/2 &lt; θ &lt; 0.  
		///
		///
		///  For points on the boundaries of the quadrants, the return value is the following:  
		///
		/// If y is 0 and x is not negative, θ = 0.  
		///
		/// If y is 0 and x is negative, θ = π.  
		///
		/// If y is positive and x is 0, θ = π/2.  
		///
		/// If y is negative and x is 0, θ = -π/2.  
		///
		/// If y is 0 and x is 0, θ = 0.  
		///
		///
		///  If <paramref name="x" /> or <paramref name="y" /> is <see cref="F:System.Double.NaN" />, or if <paramref name="x" /> and <paramref name="y" /> are either <see cref="F:System.Double.PositiveInfinity" /> or <see cref="F:System.Double.NegativeInfinity" />, the method returns <see cref="F:System.Double.NaN" />.</returns>
		// Token: 0x06001000 RID: 4096
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Atan2(double y, double x);

		/// <summary>Returns the smallest integral value that is greater than or equal to the specified decimal number.</summary>
		/// <param name="d">A decimal number.</param>
		/// <returns>The smallest integral value that is greater than or equal to <paramref name="d" />. Note that this method returns a <see cref="T:System.Decimal" /> instead of an integral type.</returns>
		// Token: 0x06001001 RID: 4097 RVA: 0x00030B79 File Offset: 0x0002ED79
		[__DynamicallyInvokable]
		public static decimal Ceiling(decimal d)
		{
			return decimal.Ceiling(d);
		}

		/// <summary>Returns the smallest integral value that is greater than or equal to the specified double-precision floating-point number.</summary>
		/// <param name="a">A double-precision floating-point number.</param>
		/// <returns>The smallest integral value that is greater than or equal to <paramref name="a" />. If <paramref name="a" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, that value is returned. Note that this method returns a <see cref="T:System.Double" /> instead of an integral type.</returns>
		// Token: 0x06001002 RID: 4098
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Ceiling(double a);

		/// <summary>Returns the cosine of the specified angle.</summary>
		/// <param name="d">An angle, measured in radians.</param>
		/// <returns>The cosine of <paramref name="d" />. If <paramref name="d" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, this method returns <see cref="F:System.Double.NaN" />.</returns>
		// Token: 0x06001003 RID: 4099
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Cos(double d);

		/// <summary>Returns the hyperbolic cosine of the specified angle.</summary>
		/// <param name="value">An angle, measured in radians.</param>
		/// <returns>The hyperbolic cosine of <paramref name="value" />. If <paramref name="value" /> is equal to <see cref="F:System.Double.NegativeInfinity" /> or <see cref="F:System.Double.PositiveInfinity" />, <see cref="F:System.Double.PositiveInfinity" /> is returned. If <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NaN" /> is returned.</returns>
		// Token: 0x06001004 RID: 4100
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Cosh(double value);

		/// <summary>Returns the largest integral value less than or equal to the specified decimal number.</summary>
		/// <param name="d">A decimal number.</param>
		/// <returns>The largest integral value less than or equal to <paramref name="d" />.  Note that the method returns an integral value of type <see cref="T:System.Decimal" />.</returns>
		// Token: 0x06001005 RID: 4101 RVA: 0x00030B81 File Offset: 0x0002ED81
		[__DynamicallyInvokable]
		public static decimal Floor(decimal d)
		{
			return decimal.Floor(d);
		}

		/// <summary>Returns the largest integral value less than or equal to the specified double-precision floating-point number.</summary>
		/// <param name="d">A double-precision floating-point number.</param>
		/// <returns>The largest integral value less than or equal to <paramref name="d" />. If <paramref name="d" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, that value is returned.</returns>
		// Token: 0x06001006 RID: 4102
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Floor(double d);

		// Token: 0x06001007 RID: 4103 RVA: 0x00030B8C File Offset: 0x0002ED8C
		[SecuritySafeCritical]
		private unsafe static double InternalRound(double value, int digits, MidpointRounding mode)
		{
			if (Math.Abs(value) < Math.doubleRoundLimit)
			{
				double num = Math.roundPower10Double[digits];
				value *= num;
				if (mode == MidpointRounding.AwayFromZero)
				{
					double num2 = Math.SplitFractionDouble(&value);
					if (Math.Abs(num2) >= 0.5)
					{
						value += (double)Math.Sign(num2);
					}
				}
				else
				{
					value = Math.Round(value);
				}
				value /= num;
			}
			return value;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00030BEC File Offset: 0x0002EDEC
		[SecuritySafeCritical]
		private unsafe static double InternalTruncate(double d)
		{
			Math.SplitFractionDouble(&d);
			return d;
		}

		/// <summary>Returns the sine of the specified angle.</summary>
		/// <param name="a">An angle, measured in radians.</param>
		/// <returns>The sine of <paramref name="a" />. If <paramref name="a" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, this method returns <see cref="F:System.Double.NaN" />.</returns>
		// Token: 0x06001009 RID: 4105
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sin(double a);

		/// <summary>Returns the tangent of the specified angle.</summary>
		/// <param name="a">An angle, measured in radians.</param>
		/// <returns>The tangent of <paramref name="a" />. If <paramref name="a" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, this method returns <see cref="F:System.Double.NaN" />.</returns>
		// Token: 0x0600100A RID: 4106
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Tan(double a);

		/// <summary>Returns the hyperbolic sine of the specified angle.</summary>
		/// <param name="value">An angle, measured in radians.</param>
		/// <returns>The hyperbolic sine of <paramref name="value" />. If <paramref name="value" /> is equal to <see cref="F:System.Double.NegativeInfinity" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NaN" />, this method returns a <see cref="T:System.Double" /> equal to <paramref name="value" />.</returns>
		// Token: 0x0600100B RID: 4107
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sinh(double value);

		/// <summary>Returns the hyperbolic tangent of the specified angle.</summary>
		/// <param name="value">An angle, measured in radians.</param>
		/// <returns>The hyperbolic tangent of <paramref name="value" />. If <paramref name="value" /> is equal to <see cref="F:System.Double.NegativeInfinity" />, this method returns -1. If value is equal to <see cref="F:System.Double.PositiveInfinity" />, this method returns 1. If <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />, this method returns <see cref="F:System.Double.NaN" />.</returns>
		// Token: 0x0600100C RID: 4108
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Tanh(double value);

		/// <summary>Rounds a double-precision floating-point value to the nearest integral value, and rounds midpoint values to the nearest even number.</summary>
		/// <param name="a">A double-precision floating-point number to be rounded.</param>
		/// <returns>The integer nearest <paramref name="a" />. If the fractional component of <paramref name="a" /> is halfway between two integers, one of which is even and the other odd, then the even number is returned. Note that this method returns a <see cref="T:System.Double" /> instead of an integral type.</returns>
		// Token: 0x0600100D RID: 4109
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Round(double a);

		/// <summary>Rounds a double-precision floating-point value to a specified number of fractional digits, and rounds midpoint values to the nearest even number.</summary>
		/// <param name="value">A double-precision floating-point number to be rounded.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <returns>The number nearest to <paramref name="value" /> that contains a number of fractional digits equal to <paramref name="digits" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="digits" /> is less than 0 or greater than 15.</exception>
		// Token: 0x0600100E RID: 4110 RVA: 0x00030BF8 File Offset: 0x0002EDF8
		[__DynamicallyInvokable]
		public static double Round(double value, int digits)
		{
			if (digits < 0 || digits > 15)
			{
				throw new ArgumentOutOfRangeException("digits", Environment.GetResourceString("ArgumentOutOfRange_RoundingDigits"));
			}
			return Math.InternalRound(value, digits, MidpointRounding.ToEven);
		}

		/// <summary>Rounds a double-precision floating-point value to the nearest integer, and uses the specified rounding convention for midpoint values.</summary>
		/// <param name="value">A double-precision floating-point number to be rounded.</param>
		/// <param name="mode">Specification for how to round <paramref name="value" /> if it is midway between two other numbers.</param>
		/// <returns>The integer nearest <paramref name="value" />. If <paramref name="value" /> is halfway between two integers, one of which is even and the other odd, then <paramref name="mode" /> determines which of the two is returned. Note that this method returns a <see cref="T:System.Double" /> instead of an integral type.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid value of <see cref="T:System.MidpointRounding" />.</exception>
		// Token: 0x0600100F RID: 4111 RVA: 0x00030C20 File Offset: 0x0002EE20
		[__DynamicallyInvokable]
		public static double Round(double value, MidpointRounding mode)
		{
			return Math.Round(value, 0, mode);
		}

		/// <summary>Rounds a double-precision floating-point value to a specified number of fractional digits, and uses the specified rounding convention for midpoint values.</summary>
		/// <param name="value">A double-precision floating-point number to be rounded.</param>
		/// <param name="digits">The number of fractional digits in the return value.</param>
		/// <param name="mode">Specification for how to round <paramref name="value" /> if it is midway between two other numbers.</param>
		/// <returns>The number nearest to <paramref name="value" /> that has a number of fractional digits equal to <paramref name="digits" />. If <paramref name="value" /> has fewer fractional digits than <paramref name="digits" />, <paramref name="value" /> is returned unchanged.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="digits" /> is less than 0 or greater than 15.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid value of <see cref="T:System.MidpointRounding" />.</exception>
		// Token: 0x06001010 RID: 4112 RVA: 0x00030C2C File Offset: 0x0002EE2C
		[__DynamicallyInvokable]
		public static double Round(double value, int digits, MidpointRounding mode)
		{
			if (digits < 0 || digits > 15)
			{
				throw new ArgumentOutOfRangeException("digits", Environment.GetResourceString("ArgumentOutOfRange_RoundingDigits"));
			}
			if (mode < MidpointRounding.ToEven || mode > MidpointRounding.AwayFromZero)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidEnumValue", new object[] { mode, "MidpointRounding" }), "mode");
			}
			return Math.InternalRound(value, digits, mode);
		}

		/// <summary>Rounds a decimal value to the nearest integral value, and rounds midpoint values to the nearest even number.</summary>
		/// <param name="d">A decimal number to be rounded.</param>
		/// <returns>The integer nearest the <paramref name="d" /> parameter. If the fractional component of <paramref name="d" /> is halfway between two integers, one of which is even and the other odd, the even number is returned. Note that this method returns a <see cref="T:System.Decimal" /> instead of an integral type.</returns>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" />.</exception>
		// Token: 0x06001011 RID: 4113 RVA: 0x00030C93 File Offset: 0x0002EE93
		[__DynamicallyInvokable]
		public static decimal Round(decimal d)
		{
			return decimal.Round(d, 0);
		}

		/// <summary>Rounds a decimal value to a specified number of fractional digits, and rounds midpoint values to the nearest even number.</summary>
		/// <param name="d">A decimal number to be rounded.</param>
		/// <param name="decimals">The number of decimal places in the return value.</param>
		/// <returns>The number nearest to <paramref name="d" /> that contains a number of fractional digits equal to <paramref name="decimals" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="decimals" /> is less than 0 or greater than 28.</exception>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" />.</exception>
		// Token: 0x06001012 RID: 4114 RVA: 0x00030C9C File Offset: 0x0002EE9C
		[__DynamicallyInvokable]
		public static decimal Round(decimal d, int decimals)
		{
			return decimal.Round(d, decimals);
		}

		/// <summary>Rounds a decimal value to the nearest integer, and uses the specified rounding convention for midpoint values.</summary>
		/// <param name="d">A decimal number to be rounded.</param>
		/// <param name="mode">Specification for how to round <paramref name="d" /> if it is midway between two other numbers.</param>
		/// <returns>The integer nearest <paramref name="d" />. If <paramref name="d" /> is halfway between two numbers, one of which is even and the other odd, then <paramref name="mode" /> determines which of the two is returned. Note that this method returns a <see cref="T:System.Decimal" /> instead of an integral type.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid value of <see cref="T:System.MidpointRounding" />.</exception>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" />.</exception>
		// Token: 0x06001013 RID: 4115 RVA: 0x00030CA5 File Offset: 0x0002EEA5
		[__DynamicallyInvokable]
		public static decimal Round(decimal d, MidpointRounding mode)
		{
			return decimal.Round(d, 0, mode);
		}

		/// <summary>Rounds a decimal value to a specified number of fractional digits, and uses the specified rounding convention for midpoint values.</summary>
		/// <param name="d">A decimal number to be rounded.</param>
		/// <param name="decimals">The number of decimal places in the return value.</param>
		/// <param name="mode">Specification for how to round <paramref name="d" /> if it is midway between two other numbers.</param>
		/// <returns>The number nearest to <paramref name="d" /> that contains a number of fractional digits equal to <paramref name="decimals" />. If <paramref name="d" /> has fewer fractional digits than <paramref name="decimals" />, <paramref name="d" /> is returned unchanged.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="decimals" /> is less than 0 or greater than 28.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid value of <see cref="T:System.MidpointRounding" />.</exception>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" />.</exception>
		// Token: 0x06001014 RID: 4116 RVA: 0x00030CAF File Offset: 0x0002EEAF
		[__DynamicallyInvokable]
		public static decimal Round(decimal d, int decimals, MidpointRounding mode)
		{
			return decimal.Round(d, decimals, mode);
		}

		// Token: 0x06001015 RID: 4117
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern double SplitFractionDouble(double* value);

		/// <summary>Calculates the integral part of a specified decimal number.</summary>
		/// <param name="d">A number to truncate.</param>
		/// <returns>The integral part of <paramref name="d" />; that is, the number that remains after any fractional digits have been discarded.</returns>
		// Token: 0x06001016 RID: 4118 RVA: 0x00030CB9 File Offset: 0x0002EEB9
		[__DynamicallyInvokable]
		public static decimal Truncate(decimal d)
		{
			return decimal.Truncate(d);
		}

		/// <summary>Calculates the integral part of a specified double-precision floating-point number.</summary>
		/// <param name="d">A number to truncate.</param>
		/// <returns>The integral part of <paramref name="d" />; that is, the number that remains after any fractional digits have been discarded, or one of the values listed in the following table.  
		///  <paramref name="d" /> Return value  
		///
		///  <see cref="F:System.Double.NaN" /><see cref="F:System.Double.NaN" /><see cref="F:System.Double.NegativeInfinity" /><see cref="F:System.Double.NegativeInfinity" /><see cref="F:System.Double.PositiveInfinity" /><see cref="F:System.Double.PositiveInfinity" /></returns>
		// Token: 0x06001017 RID: 4119 RVA: 0x00030CC1 File Offset: 0x0002EEC1
		[__DynamicallyInvokable]
		public static double Truncate(double d)
		{
			return Math.InternalTruncate(d);
		}

		/// <summary>Returns the square root of a specified number.</summary>
		/// <param name="d">The number whose square root is to be found.</param>
		/// <returns>One of the values in the following table.  
		///  <paramref name="d" /> parameter  
		///
		///   Return value  
		///
		///   Zero or positive  
		///
		///   The positive square root of <paramref name="d" />.  
		///
		///   Negative  
		///
		///  <see cref="F:System.Double.NaN" /> Equals <see cref="F:System.Double.NaN" /><see cref="F:System.Double.NaN" /> Equals <see cref="F:System.Double.PositiveInfinity" /><see cref="F:System.Double.PositiveInfinity" /></returns>
		// Token: 0x06001018 RID: 4120
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sqrt(double d);

		/// <summary>Returns the natural (base <see langword="e" />) logarithm of a specified number.</summary>
		/// <param name="d">The number whose logarithm is to be found.</param>
		/// <returns>One of the values in the following table.  
		///  <paramref name="d" /> parameter  
		///
		///   Return value  
		///
		///   Positive  
		///
		///   The natural logarithm of <paramref name="d" />; that is, ln <paramref name="d" />, or log e <paramref name="d" /> Zero  
		///
		///  <see cref="F:System.Double.NegativeInfinity" /> Negative  
		///
		///  <see cref="F:System.Double.NaN" /> Equal to <see cref="F:System.Double.NaN" /><see cref="F:System.Double.NaN" /> Equal to <see cref="F:System.Double.PositiveInfinity" /><see cref="F:System.Double.PositiveInfinity" /></returns>
		// Token: 0x06001019 RID: 4121
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Log(double d);

		/// <summary>Returns the base 10 logarithm of a specified number.</summary>
		/// <param name="d">A number whose logarithm is to be found.</param>
		/// <returns>One of the values in the following table.  
		///  <paramref name="d" /> parameter  
		///
		///   Return value  
		///
		///   Positive  
		///
		///   The base 10 log of <paramref name="d" />; that is, log 10<paramref name="d" />.  
		///
		///   Zero  
		///
		///  <see cref="F:System.Double.NegativeInfinity" /> Negative  
		///
		///  <see cref="F:System.Double.NaN" /> Equal to <see cref="F:System.Double.NaN" /><see cref="F:System.Double.NaN" /> Equal to <see cref="F:System.Double.PositiveInfinity" /><see cref="F:System.Double.PositiveInfinity" /></returns>
		// Token: 0x0600101A RID: 4122
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Log10(double d);

		/// <summary>Returns <see langword="e" /> raised to the specified power.</summary>
		/// <param name="d">A number specifying a power.</param>
		/// <returns>The number <see langword="e" /> raised to the power <paramref name="d" />. If <paramref name="d" /> equals <see cref="F:System.Double.NaN" /> or <see cref="F:System.Double.PositiveInfinity" />, that value is returned. If <paramref name="d" /> equals <see cref="F:System.Double.NegativeInfinity" />, 0 is returned.</returns>
		// Token: 0x0600101B RID: 4123
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Exp(double d);

		/// <summary>Returns a specified number raised to the specified power.</summary>
		/// <param name="x">A double-precision floating-point number to be raised to a power.</param>
		/// <param name="y">A double-precision floating-point number that specifies a power.</param>
		/// <returns>The number <paramref name="x" /> raised to the power <paramref name="y" />.</returns>
		// Token: 0x0600101C RID: 4124
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Pow(double x, double y);

		/// <summary>Returns the remainder resulting from the division of a specified number by another specified number.</summary>
		/// <param name="x">A dividend.</param>
		/// <param name="y">A divisor.</param>
		/// <returns>A number equal to <paramref name="x" /> - (<paramref name="y" /> Q), where Q is the quotient of <paramref name="x" /> / <paramref name="y" /> rounded to the nearest integer (if <paramref name="x" /> / <paramref name="y" /> falls halfway between two integers, the even integer is returned).  
		///  If <paramref name="x" /> - (<paramref name="y" /> Q) is zero, the value +0 is returned if <paramref name="x" /> is positive, or -0 if <paramref name="x" /> is negative.  
		///  If <paramref name="y" /> = 0, <see cref="F:System.Double.NaN" /> is returned.</returns>
		// Token: 0x0600101D RID: 4125 RVA: 0x00030CCC File Offset: 0x0002EECC
		[__DynamicallyInvokable]
		public static double IEEERemainder(double x, double y)
		{
			if (double.IsNaN(x))
			{
				return x;
			}
			if (double.IsNaN(y))
			{
				return y;
			}
			double num = x % y;
			if (double.IsNaN(num))
			{
				return double.NaN;
			}
			if (num == 0.0 && double.IsNegative(x))
			{
				return double.NegativeZero;
			}
			double num2 = num - Math.Abs(y) * (double)Math.Sign(x);
			if (Math.Abs(num2) == Math.Abs(num))
			{
				double num3 = x / y;
				double num4 = Math.Round(num3);
				if (Math.Abs(num4) > Math.Abs(num3))
				{
					return num2;
				}
				return num;
			}
			else
			{
				if (Math.Abs(num2) < Math.Abs(num))
				{
					return num2;
				}
				return num;
			}
		}

		/// <summary>Returns the absolute value of an 8-bit signed integer.</summary>
		/// <param name="value">A number that is greater than <see cref="F:System.SByte.MinValue" />, but less than or equal to <see cref="F:System.SByte.MaxValue" />.</param>
		/// <returns>An 8-bit signed integer, x, such that 0 ≤ x ≤<see cref="F:System.SByte.MaxValue" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> equals <see cref="F:System.SByte.MinValue" />.</exception>
		// Token: 0x0600101E RID: 4126 RVA: 0x00030D6A File Offset: 0x0002EF6A
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte Abs(sbyte value)
		{
			if (value >= 0)
			{
				return value;
			}
			return Math.AbsHelper(value);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00030D78 File Offset: 0x0002EF78
		private static sbyte AbsHelper(sbyte value)
		{
			if (value == -128)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
			}
			return -value;
		}

		/// <summary>Returns the absolute value of a 16-bit signed integer.</summary>
		/// <param name="value">A number that is greater than <see cref="F:System.Int16.MinValue" />, but less than or equal to <see cref="F:System.Int16.MaxValue" />.</param>
		/// <returns>A 16-bit signed integer, x, such that 0 ≤ x ≤<see cref="F:System.Int16.MaxValue" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> equals <see cref="F:System.Int16.MinValue" />.</exception>
		// Token: 0x06001020 RID: 4128 RVA: 0x00030D92 File Offset: 0x0002EF92
		[__DynamicallyInvokable]
		public static short Abs(short value)
		{
			if (value >= 0)
			{
				return value;
			}
			return Math.AbsHelper(value);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00030DA0 File Offset: 0x0002EFA0
		private static short AbsHelper(short value)
		{
			if (value == -32768)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
			}
			return -value;
		}

		/// <summary>Returns the absolute value of a 32-bit signed integer.</summary>
		/// <param name="value">A number that is greater than <see cref="F:System.Int32.MinValue" />, but less than or equal to <see cref="F:System.Int32.MaxValue" />.</param>
		/// <returns>A 32-bit signed integer, x, such that 0 ≤ x ≤<see cref="F:System.Int32.MaxValue" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> equals <see cref="F:System.Int32.MinValue" />.</exception>
		// Token: 0x06001022 RID: 4130 RVA: 0x00030DBD File Offset: 0x0002EFBD
		[__DynamicallyInvokable]
		public static int Abs(int value)
		{
			if (value >= 0)
			{
				return value;
			}
			return Math.AbsHelper(value);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00030DCB File Offset: 0x0002EFCB
		private static int AbsHelper(int value)
		{
			if (value == -2147483648)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
			}
			return -value;
		}

		/// <summary>Returns the absolute value of a 64-bit signed integer.</summary>
		/// <param name="value">A number that is greater than <see cref="F:System.Int64.MinValue" />, but less than or equal to <see cref="F:System.Int64.MaxValue" />.</param>
		/// <returns>A 64-bit signed integer, x, such that 0 ≤ x ≤<see cref="F:System.Int64.MaxValue" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> equals <see cref="F:System.Int64.MinValue" />.</exception>
		// Token: 0x06001024 RID: 4132 RVA: 0x00030DE7 File Offset: 0x0002EFE7
		[__DynamicallyInvokable]
		public static long Abs(long value)
		{
			if (value >= 0L)
			{
				return value;
			}
			return Math.AbsHelper(value);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00030DF6 File Offset: 0x0002EFF6
		private static long AbsHelper(long value)
		{
			if (value == -9223372036854775808L)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
			}
			return -value;
		}

		/// <summary>Returns the absolute value of a single-precision floating-point number.</summary>
		/// <param name="value">A number that is greater than or equal to <see cref="F:System.Single.MinValue" />, but less than or equal to <see cref="F:System.Single.MaxValue" />.</param>
		/// <returns>A single-precision floating-point number, x, such that 0 ≤ x ≤<see cref="F:System.Single.MaxValue" />.</returns>
		// Token: 0x06001026 RID: 4134
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Abs(float value);

		/// <summary>Returns the absolute value of a double-precision floating-point number.</summary>
		/// <param name="value">A number that is greater than or equal to <see cref="F:System.Double.MinValue" />, but less than or equal to <see cref="F:System.Double.MaxValue" />.</param>
		/// <returns>A double-precision floating-point number, x, such that 0 ≤ x ≤<see cref="F:System.Double.MaxValue" />.</returns>
		// Token: 0x06001027 RID: 4135
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Abs(double value);

		/// <summary>Returns the absolute value of a <see cref="T:System.Decimal" /> number.</summary>
		/// <param name="value">A number that is greater than or equal to <see cref="F:System.Decimal.MinValue" />, but less than or equal to <see cref="F:System.Decimal.MaxValue" />.</param>
		/// <returns>A decimal number, x, such that 0 ≤ x ≤<see cref="F:System.Decimal.MaxValue" />.</returns>
		// Token: 0x06001028 RID: 4136 RVA: 0x00030E16 File Offset: 0x0002F016
		[__DynamicallyInvokable]
		public static decimal Abs(decimal value)
		{
			return decimal.Abs(value);
		}

		/// <summary>Returns the larger of two 8-bit signed integers.</summary>
		/// <param name="val1">The first of two 8-bit signed integers to compare.</param>
		/// <param name="val2">The second of two 8-bit signed integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		// Token: 0x06001029 RID: 4137 RVA: 0x00030E1E File Offset: 0x0002F01E
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static sbyte Max(sbyte val1, sbyte val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two 8-bit unsigned integers.</summary>
		/// <param name="val1">The first of two 8-bit unsigned integers to compare.</param>
		/// <param name="val2">The second of two 8-bit unsigned integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		// Token: 0x0600102A RID: 4138 RVA: 0x00030E27 File Offset: 0x0002F027
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static byte Max(byte val1, byte val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two 16-bit signed integers.</summary>
		/// <param name="val1">The first of two 16-bit signed integers to compare.</param>
		/// <param name="val2">The second of two 16-bit signed integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		// Token: 0x0600102B RID: 4139 RVA: 0x00030E30 File Offset: 0x0002F030
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static short Max(short val1, short val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two 16-bit unsigned integers.</summary>
		/// <param name="val1">The first of two 16-bit unsigned integers to compare.</param>
		/// <param name="val2">The second of two 16-bit unsigned integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		// Token: 0x0600102C RID: 4140 RVA: 0x00030E39 File Offset: 0x0002F039
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static ushort Max(ushort val1, ushort val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two 32-bit signed integers.</summary>
		/// <param name="val1">The first of two 32-bit signed integers to compare.</param>
		/// <param name="val2">The second of two 32-bit signed integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		// Token: 0x0600102D RID: 4141 RVA: 0x00030E42 File Offset: 0x0002F042
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static int Max(int val1, int val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two 32-bit unsigned integers.</summary>
		/// <param name="val1">The first of two 32-bit unsigned integers to compare.</param>
		/// <param name="val2">The second of two 32-bit unsigned integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		// Token: 0x0600102E RID: 4142 RVA: 0x00030E4B File Offset: 0x0002F04B
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static uint Max(uint val1, uint val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two 64-bit signed integers.</summary>
		/// <param name="val1">The first of two 64-bit signed integers to compare.</param>
		/// <param name="val2">The second of two 64-bit signed integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		// Token: 0x0600102F RID: 4143 RVA: 0x00030E54 File Offset: 0x0002F054
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static long Max(long val1, long val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two 64-bit unsigned integers.</summary>
		/// <param name="val1">The first of two 64-bit unsigned integers to compare.</param>
		/// <param name="val2">The second of two 64-bit unsigned integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		// Token: 0x06001030 RID: 4144 RVA: 0x00030E5D File Offset: 0x0002F05D
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static ulong Max(ulong val1, ulong val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the larger of two single-precision floating-point numbers.</summary>
		/// <param name="val1">The first of two single-precision floating-point numbers to compare.</param>
		/// <param name="val2">The second of two single-precision floating-point numbers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger. If <paramref name="val1" />, or <paramref name="val2" />, or both <paramref name="val1" /> and <paramref name="val2" /> are equal to <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.NaN" /> is returned.</returns>
		// Token: 0x06001031 RID: 4145 RVA: 0x00030E66 File Offset: 0x0002F066
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static float Max(float val1, float val2)
		{
			if (val1 > val2)
			{
				return val1;
			}
			if (float.IsNaN(val1))
			{
				return val1;
			}
			return val2;
		}

		/// <summary>Returns the larger of two double-precision floating-point numbers.</summary>
		/// <param name="val1">The first of two double-precision floating-point numbers to compare.</param>
		/// <param name="val2">The second of two double-precision floating-point numbers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger. If <paramref name="val1" />, <paramref name="val2" />, or both <paramref name="val1" /> and <paramref name="val2" /> are equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NaN" /> is returned.</returns>
		// Token: 0x06001032 RID: 4146 RVA: 0x00030E79 File Offset: 0x0002F079
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static double Max(double val1, double val2)
		{
			if (val1 > val2)
			{
				return val1;
			}
			if (double.IsNaN(val1))
			{
				return val1;
			}
			return val2;
		}

		/// <summary>Returns the larger of two decimal numbers.</summary>
		/// <param name="val1">The first of two decimal numbers to compare.</param>
		/// <param name="val2">The second of two decimal numbers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		// Token: 0x06001033 RID: 4147 RVA: 0x00030E8C File Offset: 0x0002F08C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static decimal Max(decimal val1, decimal val2)
		{
			return decimal.Max(val1, val2);
		}

		/// <summary>Returns the smaller of two 8-bit signed integers.</summary>
		/// <param name="val1">The first of two 8-bit signed integers to compare.</param>
		/// <param name="val2">The second of two 8-bit signed integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		// Token: 0x06001034 RID: 4148 RVA: 0x00030E95 File Offset: 0x0002F095
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static sbyte Min(sbyte val1, sbyte val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two 8-bit unsigned integers.</summary>
		/// <param name="val1">The first of two 8-bit unsigned integers to compare.</param>
		/// <param name="val2">The second of two 8-bit unsigned integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		// Token: 0x06001035 RID: 4149 RVA: 0x00030E9E File Offset: 0x0002F09E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static byte Min(byte val1, byte val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two 16-bit signed integers.</summary>
		/// <param name="val1">The first of two 16-bit signed integers to compare.</param>
		/// <param name="val2">The second of two 16-bit signed integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		// Token: 0x06001036 RID: 4150 RVA: 0x00030EA7 File Offset: 0x0002F0A7
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static short Min(short val1, short val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two 16-bit unsigned integers.</summary>
		/// <param name="val1">The first of two 16-bit unsigned integers to compare.</param>
		/// <param name="val2">The second of two 16-bit unsigned integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		// Token: 0x06001037 RID: 4151 RVA: 0x00030EB0 File Offset: 0x0002F0B0
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static ushort Min(ushort val1, ushort val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two 32-bit signed integers.</summary>
		/// <param name="val1">The first of two 32-bit signed integers to compare.</param>
		/// <param name="val2">The second of two 32-bit signed integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		// Token: 0x06001038 RID: 4152 RVA: 0x00030EB9 File Offset: 0x0002F0B9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static int Min(int val1, int val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two 32-bit unsigned integers.</summary>
		/// <param name="val1">The first of two 32-bit unsigned integers to compare.</param>
		/// <param name="val2">The second of two 32-bit unsigned integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		// Token: 0x06001039 RID: 4153 RVA: 0x00030EC2 File Offset: 0x0002F0C2
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static uint Min(uint val1, uint val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two 64-bit signed integers.</summary>
		/// <param name="val1">The first of two 64-bit signed integers to compare.</param>
		/// <param name="val2">The second of two 64-bit signed integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		// Token: 0x0600103A RID: 4154 RVA: 0x00030ECB File Offset: 0x0002F0CB
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static long Min(long val1, long val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two 64-bit unsigned integers.</summary>
		/// <param name="val1">The first of two 64-bit unsigned integers to compare.</param>
		/// <param name="val2">The second of two 64-bit unsigned integers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		// Token: 0x0600103B RID: 4155 RVA: 0x00030ED4 File Offset: 0x0002F0D4
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[NonVersionable]
		[__DynamicallyInvokable]
		public static ulong Min(ulong val1, ulong val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		/// <summary>Returns the smaller of two single-precision floating-point numbers.</summary>
		/// <param name="val1">The first of two single-precision floating-point numbers to compare.</param>
		/// <param name="val2">The second of two single-precision floating-point numbers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller. If <paramref name="val1" />, <paramref name="val2" />, or both <paramref name="val1" /> and <paramref name="val2" /> are equal to <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.NaN" /> is returned.</returns>
		// Token: 0x0600103C RID: 4156 RVA: 0x00030EDD File Offset: 0x0002F0DD
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static float Min(float val1, float val2)
		{
			if (val1 < val2)
			{
				return val1;
			}
			if (float.IsNaN(val1))
			{
				return val1;
			}
			return val2;
		}

		/// <summary>Returns the smaller of two double-precision floating-point numbers.</summary>
		/// <param name="val1">The first of two double-precision floating-point numbers to compare.</param>
		/// <param name="val2">The second of two double-precision floating-point numbers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller. If <paramref name="val1" />, <paramref name="val2" />, or both <paramref name="val1" /> and <paramref name="val2" /> are equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NaN" /> is returned.</returns>
		// Token: 0x0600103D RID: 4157 RVA: 0x00030EF0 File Offset: 0x0002F0F0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static double Min(double val1, double val2)
		{
			if (val1 < val2)
			{
				return val1;
			}
			if (double.IsNaN(val1))
			{
				return val1;
			}
			return val2;
		}

		/// <summary>Returns the smaller of two decimal numbers.</summary>
		/// <param name="val1">The first of two decimal numbers to compare.</param>
		/// <param name="val2">The second of two decimal numbers to compare.</param>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		// Token: 0x0600103E RID: 4158 RVA: 0x00030F03 File Offset: 0x0002F103
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static decimal Min(decimal val1, decimal val2)
		{
			return decimal.Min(val1, val2);
		}

		/// <summary>Returns the logarithm of a specified number in a specified base.</summary>
		/// <param name="a">The number whose logarithm is to be found.</param>
		/// <param name="newBase">The base of the logarithm.</param>
		/// <returns>One of the values in the following table. (+Infinity denotes <see cref="F:System.Double.PositiveInfinity" />, -Infinity denotes <see cref="F:System.Double.NegativeInfinity" />, and NaN denotes <see cref="F:System.Double.NaN" />.)  
		///  <paramref name="a" /><paramref name="newBase" /> Return value  
		///
		///  <paramref name="a" />&gt; 0  
		///
		///   (0 &lt;<paramref name="newBase" />&lt; 1) -or-(<paramref name="newBase" />&gt; 1)  
		///
		///   lognewBase(a)  
		///
		///  <paramref name="a" />&lt; 0  
		///
		///   (any value)  
		///
		///   NaN  
		///
		///   (any value)  
		///
		///  <paramref name="newBase" />&lt; 0  
		///
		///   NaN  
		///
		///  <paramref name="a" /> != 1  
		///
		///  <paramref name="newBase" /> = 0  
		///
		///   NaN  
		///
		///  <paramref name="a" /> != 1  
		///
		///  <paramref name="newBase" /> = +Infinity  
		///
		///   NaN  
		///
		///  <paramref name="a" /> = NaN  
		///
		///   (any value)  
		///
		///   NaN  
		///
		///   (any value)  
		///
		///  <paramref name="newBase" /> = NaN  
		///
		///   NaN  
		///
		///   (any value)  
		///
		///  <paramref name="newBase" /> = 1  
		///
		///   NaN  
		///
		///  <paramref name="a" /> = 0  
		///
		///   0 &lt;<paramref name="newBase" />&lt; 1  
		///
		///   +Infinity  
		///
		///  <paramref name="a" /> = 0  
		///
		///  <paramref name="newBase" />&gt; 1  
		///
		///   -Infinity  
		///
		///  <paramref name="a" /> =  +Infinity  
		///
		///   0 &lt;<paramref name="newBase" />&lt; 1  
		///
		///   -Infinity  
		///
		///  <paramref name="a" /> =  +Infinity  
		///
		///  <paramref name="newBase" />&gt; 1  
		///
		///   +Infinity  
		///
		///  <paramref name="a" /> = 1  
		///
		///  <paramref name="newBase" /> = 0  
		///
		///   0  
		///
		///  <paramref name="a" /> = 1  
		///
		///  <paramref name="newBase" /> = +Infinity  
		///
		///   0</returns>
		// Token: 0x0600103F RID: 4159 RVA: 0x00030F0C File Offset: 0x0002F10C
		[__DynamicallyInvokable]
		public static double Log(double a, double newBase)
		{
			if (double.IsNaN(a))
			{
				return a;
			}
			if (double.IsNaN(newBase))
			{
				return newBase;
			}
			if (newBase == 1.0)
			{
				return double.NaN;
			}
			if (a != 1.0 && (newBase == 0.0 || double.IsPositiveInfinity(newBase)))
			{
				return double.NaN;
			}
			return Math.Log(a) / Math.Log(newBase);
		}

		/// <summary>Returns an integer that indicates the sign of an 8-bit signed integer.</summary>
		/// <param name="value">A signed number.</param>
		/// <returns>A number that indicates the sign of <paramref name="value" />, as shown in the following table.  
		///   Return value  
		///
		///   Meaning  
		///
		///   -1  
		///
		///  <paramref name="value" /> is less than zero.  
		///
		///   0  
		///
		///  <paramref name="value" /> is equal to zero.  
		///
		///   1  
		///
		///  <paramref name="value" /> is greater than zero.</returns>
		// Token: 0x06001040 RID: 4160 RVA: 0x00030F7A File Offset: 0x0002F17A
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static int Sign(sbyte value)
		{
			if (value < 0)
			{
				return -1;
			}
			if (value > 0)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>Returns an integer that indicates the sign of a 16-bit signed integer.</summary>
		/// <param name="value">A signed number.</param>
		/// <returns>A number that indicates the sign of <paramref name="value" />, as shown in the following table.  
		///   Return value  
		///
		///   Meaning  
		///
		///   -1  
		///
		///  <paramref name="value" /> is less than zero.  
		///
		///   0  
		///
		///  <paramref name="value" /> is equal to zero.  
		///
		///   1  
		///
		///  <paramref name="value" /> is greater than zero.</returns>
		// Token: 0x06001041 RID: 4161 RVA: 0x00030F89 File Offset: 0x0002F189
		[__DynamicallyInvokable]
		public static int Sign(short value)
		{
			if (value < 0)
			{
				return -1;
			}
			if (value > 0)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>Returns an integer that indicates the sign of a 32-bit signed integer.</summary>
		/// <param name="value">A signed number.</param>
		/// <returns>A number that indicates the sign of <paramref name="value" />, as shown in the following table.  
		///   Return value  
		///
		///   Meaning  
		///
		///   -1  
		///
		///  <paramref name="value" /> is less than zero.  
		///
		///   0  
		///
		///  <paramref name="value" /> is equal to zero.  
		///
		///   1  
		///
		///  <paramref name="value" /> is greater than zero.</returns>
		// Token: 0x06001042 RID: 4162 RVA: 0x00030F98 File Offset: 0x0002F198
		[__DynamicallyInvokable]
		public static int Sign(int value)
		{
			if (value < 0)
			{
				return -1;
			}
			if (value > 0)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>Returns an integer that indicates the sign of a 64-bit signed integer.</summary>
		/// <param name="value">A signed number.</param>
		/// <returns>A number that indicates the sign of <paramref name="value" />, as shown in the following table.  
		///   Return value  
		///
		///   Meaning  
		///
		///   -1  
		///
		///  <paramref name="value" /> is less than zero.  
		///
		///   0  
		///
		///  <paramref name="value" /> is equal to zero.  
		///
		///   1  
		///
		///  <paramref name="value" /> is greater than zero.</returns>
		// Token: 0x06001043 RID: 4163 RVA: 0x00030FA7 File Offset: 0x0002F1A7
		[__DynamicallyInvokable]
		public static int Sign(long value)
		{
			if (value < 0L)
			{
				return -1;
			}
			if (value > 0L)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>Returns an integer that indicates the sign of a single-precision floating-point number.</summary>
		/// <param name="value">A signed number.</param>
		/// <returns>A number that indicates the sign of <paramref name="value" />, as shown in the following table.  
		///   Return value  
		///
		///   Meaning  
		///
		///   -1  
		///
		///  <paramref name="value" /> is less than zero.  
		///
		///   0  
		///
		///  <paramref name="value" /> is equal to zero.  
		///
		///   1  
		///
		///  <paramref name="value" /> is greater than zero.</returns>
		/// <exception cref="T:System.ArithmeticException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Single.NaN" />.</exception>
		// Token: 0x06001044 RID: 4164 RVA: 0x00030FB8 File Offset: 0x0002F1B8
		[__DynamicallyInvokable]
		public static int Sign(float value)
		{
			if (value < 0f)
			{
				return -1;
			}
			if (value > 0f)
			{
				return 1;
			}
			if (value == 0f)
			{
				return 0;
			}
			throw new ArithmeticException(Environment.GetResourceString("Arithmetic_NaN"));
		}

		/// <summary>Returns an integer that indicates the sign of a double-precision floating-point number.</summary>
		/// <param name="value">A signed number.</param>
		/// <returns>A number that indicates the sign of <paramref name="value" />, as shown in the following table.  
		///   Return value  
		///
		///   Meaning  
		///
		///   -1  
		///
		///  <paramref name="value" /> is less than zero.  
		///
		///   0  
		///
		///  <paramref name="value" /> is equal to zero.  
		///
		///   1  
		///
		///  <paramref name="value" /> is greater than zero.</returns>
		/// <exception cref="T:System.ArithmeticException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x06001045 RID: 4165 RVA: 0x00030FE7 File Offset: 0x0002F1E7
		[__DynamicallyInvokable]
		public static int Sign(double value)
		{
			if (value < 0.0)
			{
				return -1;
			}
			if (value > 0.0)
			{
				return 1;
			}
			if (value == 0.0)
			{
				return 0;
			}
			throw new ArithmeticException(Environment.GetResourceString("Arithmetic_NaN"));
		}

		/// <summary>Returns an integer that indicates the sign of a decimal number.</summary>
		/// <param name="value">A signed decimal number.</param>
		/// <returns>A number that indicates the sign of <paramref name="value" />, as shown in the following table.  
		///   Return value  
		///
		///   Meaning  
		///
		///   -1  
		///
		///  <paramref name="value" /> is less than zero.  
		///
		///   0  
		///
		///  <paramref name="value" /> is equal to zero.  
		///
		///   1  
		///
		///  <paramref name="value" /> is greater than zero.</returns>
		// Token: 0x06001046 RID: 4166 RVA: 0x00031022 File Offset: 0x0002F222
		[__DynamicallyInvokable]
		public static int Sign(decimal value)
		{
			if (value < 0m)
			{
				return -1;
			}
			if (value > 0m)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>Produces the full product of two 32-bit numbers.</summary>
		/// <param name="a">The first number to multiply.</param>
		/// <param name="b">The second number to multiply.</param>
		/// <returns>The number containing the product of the specified numbers.</returns>
		// Token: 0x06001047 RID: 4167 RVA: 0x00031043 File Offset: 0x0002F243
		public static long BigMul(int a, int b)
		{
			return (long)a * (long)b;
		}

		/// <summary>Calculates the quotient of two 32-bit signed integers and also returns the remainder in an output parameter.</summary>
		/// <param name="a">The dividend.</param>
		/// <param name="b">The divisor.</param>
		/// <param name="result">The remainder.</param>
		/// <returns>The quotient of the specified numbers.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="b" /> is zero.</exception>
		// Token: 0x06001048 RID: 4168 RVA: 0x0003104A File Offset: 0x0002F24A
		public static int DivRem(int a, int b, out int result)
		{
			result = a % b;
			return a / b;
		}

		/// <summary>Calculates the quotient of two 64-bit signed integers and also returns the remainder in an output parameter.</summary>
		/// <param name="a">The dividend.</param>
		/// <param name="b">The divisor.</param>
		/// <param name="result">The remainder.</param>
		/// <returns>The quotient of the specified numbers.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="b" /> is zero.</exception>
		// Token: 0x06001049 RID: 4169 RVA: 0x00031054 File Offset: 0x0002F254
		public static long DivRem(long a, long b, out long result)
		{
			result = a % b;
			return a / b;
		}

		// Token: 0x040005BB RID: 1467
		private static double doubleRoundLimit = 1E+16;

		// Token: 0x040005BC RID: 1468
		private const int maxRoundingDigits = 15;

		// Token: 0x040005BD RID: 1469
		private static double[] roundPower10Double = new double[]
		{
			1.0, 10.0, 100.0, 1000.0, 10000.0, 100000.0, 1000000.0, 10000000.0, 100000000.0, 1000000000.0,
			10000000000.0, 100000000000.0, 1000000000000.0, 10000000000000.0, 100000000000000.0, 1E+15
		};

		/// <summary>Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.</summary>
		// Token: 0x040005BE RID: 1470
		[__DynamicallyInvokable]
		public const double PI = 3.1415926535897931;

		/// <summary>Represents the natural logarithmic base, specified by the constant, <see langword="e" />.</summary>
		// Token: 0x040005BF RID: 1471
		[__DynamicallyInvokable]
		public const double E = 2.7182818284590451;
	}
}
