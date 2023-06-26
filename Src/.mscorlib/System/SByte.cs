using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
	/// <summary>Represents an 8-bit signed integer.</summary>
	// Token: 0x0200013C RID: 316
	[CLSCompliant(false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct SByte : IComparable, IFormattable, IConvertible, IComparable<sbyte>, IEquatable<sbyte>
	{
		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <param name="obj">An object to compare, or <see langword="null" />.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="obj" />.  
		///   Return Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   This instance is less than <paramref name="obj" />.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="obj" />.  
		///
		///   Greater than zero  
		///
		///   This instance is greater than <paramref name="obj" />.  
		///
		///  -or-  
		///
		///  <paramref name="obj" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not an <see cref="T:System.SByte" />.</exception>
		// Token: 0x060012E8 RID: 4840 RVA: 0x00037FE7 File Offset: 0x000361E7
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is sbyte))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeSByte"));
			}
			return (int)(this - (sbyte)obj);
		}

		/// <summary>Compares this instance to a specified 8-bit signed integer and returns an indication of their relative values.</summary>
		/// <param name="value">An 8-bit signed integer to compare.</param>
		/// <returns>A signed integer that indicates the relative order of this instance and <paramref name="value" />.  
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
		// Token: 0x060012E9 RID: 4841 RVA: 0x0003800F File Offset: 0x0003620F
		[__DynamicallyInvokable]
		public int CompareTo(sbyte value)
		{
			return (int)(this - value);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see cref="T:System.SByte" /> and equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060012EA RID: 4842 RVA: 0x00038015 File Offset: 0x00036215
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is sbyte && this == (sbyte)obj;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.SByte" /> value.</summary>
		/// <param name="obj">An <see cref="T:System.SByte" /> value to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> has the same value as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060012EB RID: 4843 RVA: 0x0003802B File Offset: 0x0003622B
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(sbyte obj)
		{
			return this == obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060012EC RID: 4844 RVA: 0x00038032 File Offset: 0x00036232
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this ^ ((int)this << 8);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance, consisting of a negative sign if the value is negative, and a sequence of digits ranging from 0 to 9 with no leading zeroes.</returns>
		// Token: 0x060012ED RID: 4845 RVA: 0x0003803B File Offset: 0x0003623B
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Number.FormatInt32((int)this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance, as specified by <paramref name="provider" />.</returns>
		// Token: 0x060012EE RID: 4846 RVA: 0x0003804A File Offset: 0x0003624A
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.</exception>
		// Token: 0x060012EF RID: 4847 RVA: 0x0003805A File Offset: 0x0003625A
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return this.ToString(format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.</exception>
		// Token: 0x060012F0 RID: 4848 RVA: 0x00038068 File Offset: 0x00036268
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return this.ToString(format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00038078 File Offset: 0x00036278
		[SecuritySafeCritical]
		private string ToString(string format, NumberFormatInfo info)
		{
			if (this < 0 && format != null && format.Length > 0 && (format[0] == 'X' || format[0] == 'x'))
			{
				uint num = (uint)this & 255U;
				return Number.FormatUInt32(num, format, info);
			}
			return Number.FormatInt32((int)this, format, info);
		}

		/// <summary>Converts the string representation of a number to its 8-bit signed integer equivalent.</summary>
		/// <param name="s">A string that represents a number to convert. The string is interpreted using the <see cref="F:System.Globalization.NumberStyles.Integer" /> style.</param>
		/// <returns>An 8-bit signed integer that is equivalent to the number contained in the <paramref name="s" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not consist of an optional sign followed by a sequence of digits (zero through nine).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x060012F2 RID: 4850 RVA: 0x000380C7 File Offset: 0x000362C7
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte Parse(string s)
		{
			return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its 8-bit signed integer equivalent.</summary>
		/// <param name="s">A string that contains a number to convert. The string is interpreted using the style specified by <paramref name="style" />.</param>
		/// <param name="style">A bitwise combination of the enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <returns>An 8-bit signed integer that is equivalent to the number specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format that is compliant with <paramref name="style" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.  
		/// -or-  
		/// <paramref name="s" /> includes non-zero, fractional digits.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		// Token: 0x060012F3 RID: 4851 RVA: 0x000380D5 File Offset: 0x000362D5
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return sbyte.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified culture-specific format to its 8-bit signed integer equivalent.</summary>
		/// <param name="s">A string that represents a number to convert. The string is interpreted using the <see cref="F:System.Globalization.NumberStyles.Integer" /> style.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. If <paramref name="provider" /> is <see langword="null" />, the thread current culture is used.</param>
		/// <returns>An 8-bit signed integer that is equivalent to the number specified in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x060012F4 RID: 4852 RVA: 0x000380E9 File Offset: 0x000362E9
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte Parse(string s, IFormatProvider provider)
		{
			return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number that is in a specified style and culture-specific format to its 8-bit signed equivalent.</summary>
		/// <param name="s">A string that contains the number to convert. The string is interpreted by using the style specified by <paramref name="style" />.</param>
		/// <param name="style">A bitwise combination of the enumeration values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />. If <paramref name="provider" /> is <see langword="null" />, the thread current culture is used.</param>
		/// <returns>An 8-bit signed byte value that is equivalent to the number specified in the <paramref name="s" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in a format that is compliant with <paramref name="style" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.  
		/// -or-  
		/// <paramref name="s" /> includes non-zero, fractional digits.</exception>
		// Token: 0x060012F5 RID: 4853 RVA: 0x000380F8 File Offset: 0x000362F8
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return sbyte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00038110 File Offset: 0x00036310
		private static sbyte Parse(string s, NumberStyles style, NumberFormatInfo info)
		{
			int num = 0;
			try
			{
				num = Number.ParseInt32(s, style, info);
			}
			catch (OverflowException ex)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_SByte"), ex);
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (num < 0 || num > 255)
				{
					throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
				}
				return (sbyte)num;
			}
			else
			{
				if (num < -128 || num > 127)
				{
					throw new OverflowException(Environment.GetResourceString("Overflow_SByte"));
				}
				return (sbyte)num;
			}
		}

		/// <summary>Tries to convert the string representation of a number to its <see cref="T:System.SByte" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="s">A string that contains a number to convert.</param>
		/// <param name="result">When this method returns, contains the 8-bit signed integer value that is equivalent to the number contained in <paramref name="s" /> if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, is not in the correct format, or represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. This parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x060012F7 RID: 4855 RVA: 0x00038190 File Offset: 0x00036390
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out sbyte result)
		{
			return sbyte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Tries to convert the string representation of a number in a specified style and culture-specific format to its <see cref="T:System.SByte" /> equivalent, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="s">A string representing a number to convert.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Integer" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <param name="result">When this method returns, contains the 8-bit signed integer value equivalent to the number contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, is not in a format compliant with <paramref name="style" />, or represents a number less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. This parameter is passed uninitialized; any value originally supplied in <paramref name="result" /> will be overwritten.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is not a combination of <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> and <see cref="F:System.Globalization.NumberStyles.HexNumber" /> values.</exception>
		// Token: 0x060012F8 RID: 4856 RVA: 0x0003819F File Offset: 0x0003639F
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out sbyte result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return sbyte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000381B8 File Offset: 0x000363B8
		private static bool TryParse(string s, NumberStyles style, NumberFormatInfo info, out sbyte result)
		{
			result = 0;
			int num;
			if (!Number.TryParseInt32(s, style, info, out num))
			{
				return false;
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				if (num < 0 || num > 255)
				{
					return false;
				}
				result = (sbyte)num;
				return true;
			}
			else
			{
				if (num < -128 || num > 127)
				{
					return false;
				}
				result = (sbyte)num;
				return true;
			}
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.SByte" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.SByte" />.</returns>
		// Token: 0x060012FA RID: 4858 RVA: 0x00038204 File Offset: 0x00036404
		public TypeCode GetTypeCode()
		{
			return TypeCode.SByte;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is unused.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the current instance is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x060012FB RID: 4859 RVA: 0x00038207 File Offset: 0x00036407
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Char" />.</returns>
		// Token: 0x060012FC RID: 4860 RVA: 0x00038210 File Offset: 0x00036410
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, unchanged.</returns>
		// Token: 0x060012FD RID: 4861 RVA: 0x00038219 File Offset: 0x00036419
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is unused.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		// Token: 0x060012FE RID: 4862 RVA: 0x0003821D File Offset: 0x0003641D
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int16" />.</returns>
		// Token: 0x060012FF RID: 4863 RVA: 0x00038226 File Offset: 0x00036426
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		// Token: 0x06001300 RID: 4864 RVA: 0x0003822F File Offset: 0x0003642F
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int32" />.</returns>
		// Token: 0x06001301 RID: 4865 RVA: 0x00038238 File Offset: 0x00036438
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return (int)this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		// Token: 0x06001302 RID: 4866 RVA: 0x0003823C File Offset: 0x0003643C
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to an <see cref="T:System.Int64" />.</returns>
		// Token: 0x06001303 RID: 4867 RVA: 0x00038245 File Offset: 0x00036445
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		// Token: 0x06001304 RID: 4868 RVA: 0x0003824E File Offset: 0x0003644E
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		// Token: 0x06001305 RID: 4869 RVA: 0x00038257 File Offset: 0x00036457
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		// Token: 0x06001306 RID: 4870 RVA: 0x00038260 File Offset: 0x00036460
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is unused.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Decimal" />.</returns>
		// Token: 0x06001307 RID: 4871 RVA: 0x00038269 File Offset: 0x00036469
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>None. This conversion is not supported.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06001308 RID: 4872 RVA: 0x00038272 File Offset: 0x00036472
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "SByte", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to which to convert this <see cref="T:System.SByte" /> value.</param>
		/// <param name="provider">A <see cref="T:System.IFormatProvider" /> implementation that provides information about the format of the returned value.</param>
		/// <returns>The value of the current instance, converted to an object of type <paramref name="type" />.</returns>
		// Token: 0x06001309 RID: 4873 RVA: 0x00038299 File Offset: 0x00036499
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x04000680 RID: 1664
		private sbyte m_value;

		/// <summary>Represents the largest possible value of <see cref="T:System.SByte" />. This field is constant.</summary>
		// Token: 0x04000681 RID: 1665
		[__DynamicallyInvokable]
		public const sbyte MaxValue = 127;

		/// <summary>Represents the smallest possible value of <see cref="T:System.SByte" />. This field is constant.</summary>
		// Token: 0x04000682 RID: 1666
		[__DynamicallyInvokable]
		public const sbyte MinValue = -128;
	}
}
