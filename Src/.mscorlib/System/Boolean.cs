using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System
{
	/// <summary>Represents a Boolean (<see langword="true" /> or <see langword="false" />) value.</summary>
	// Token: 0x020000B2 RID: 178
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Boolean : IComparable, IConvertible, IComparable<bool>, IEquatable<bool>
	{
		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Boolean" />.</returns>
		// Token: 0x06000A34 RID: 2612 RVA: 0x00020FD1 File Offset: 0x0001F1D1
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (!this)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Converts the value of this instance to its equivalent string representation (either "True" or "False").</summary>
		/// <returns>"True" (the value of the <see cref="F:System.Boolean.TrueString" /> property) if the value of this instance is <see langword="true" />, or "False" (the value of the <see cref="F:System.Boolean.FalseString" /> property) if the value of this instance is <see langword="false" />.</returns>
		// Token: 0x06000A35 RID: 2613 RVA: 0x00020FDA File Offset: 0x0001F1DA
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (!this)
			{
				return "False";
			}
			return "True";
		}

		/// <summary>Converts the value of this instance to its equivalent string representation (either "True" or "False").</summary>
		/// <param name="provider">(Reserved) An <see cref="T:System.IFormatProvider" /> object.</param>
		/// <returns>
		///   <see cref="F:System.Boolean.TrueString" /> if the value of this instance is <see langword="true" />, or <see cref="F:System.Boolean.FalseString" /> if the value of this instance is <see langword="false" />.</returns>
		// Token: 0x06000A36 RID: 2614 RVA: 0x00020FEB File Offset: 0x0001F1EB
		public string ToString(IFormatProvider provider)
		{
			if (!this)
			{
				return "False";
			}
			return "True";
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Boolean" /> and has the same value as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A37 RID: 2615 RVA: 0x00020FFC File Offset: 0x0001F1FC
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is bool && this == (bool)obj;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.Boolean" /> object.</summary>
		/// <param name="obj">A <see cref="T:System.Boolean" /> value to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> has the same value as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A38 RID: 2616 RVA: 0x00021012 File Offset: 0x0001F212
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(bool obj)
		{
			return this == obj;
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates their relationship to one another.</summary>
		/// <param name="obj">An object to compare to this instance, or <see langword="null" />.</param>
		/// <returns>A signed integer that indicates the relative order of this instance and <paramref name="obj" />.  
		///   Return Value  
		///
		///   Condition  
		///
		///   Less than zero  
		///
		///   This instance is <see langword="false" /> and <paramref name="obj" /> is <see langword="true" />.  
		///
		///   Zero  
		///
		///   This instance and <paramref name="obj" /> are equal (either both are <see langword="true" /> or both are <see langword="false" />).  
		///
		///   Greater than zero  
		///
		///   This instance is <see langword="true" /> and <paramref name="obj" /> is <see langword="false" />.  
		///
		///  -or-  
		///
		///  <paramref name="obj" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a <see cref="T:System.Boolean" />.</exception>
		// Token: 0x06000A39 RID: 2617 RVA: 0x00021019 File Offset: 0x0001F219
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is bool))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeBoolean"));
			}
			if (this == (bool)obj)
			{
				return 0;
			}
			if (!this)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Boolean" /> object and returns an integer that indicates their relationship to one another.</summary>
		/// <param name="value">A <see cref="T:System.Boolean" /> object to compare to this instance.</param>
		/// <returns>A signed integer that indicates the relative values of this instance and <paramref name="value" />.  
		///   Return Value  
		///
		///   Condition  
		///
		///   Less than zero  
		///
		///   This instance is <see langword="false" /> and <paramref name="value" /> is <see langword="true" />.  
		///
		///   Zero  
		///
		///   This instance and <paramref name="value" /> are equal (either both are <see langword="true" /> or both are <see langword="false" />).  
		///
		///   Greater than zero  
		///
		///   This instance is <see langword="true" /> and <paramref name="value" /> is <see langword="false" />.</returns>
		// Token: 0x06000A3A RID: 2618 RVA: 0x0002104B File Offset: 0x0001F24B
		[__DynamicallyInvokable]
		public int CompareTo(bool value)
		{
			if (this == value)
			{
				return 0;
			}
			if (!this)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Converts the specified string representation of a logical value to its <see cref="T:System.Boolean" /> equivalent.</summary>
		/// <param name="value">A string containing the value to convert.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is equivalent to <see cref="F:System.Boolean.TrueString" />; <see langword="false" /> if <paramref name="value" /> is equivalent to <see cref="F:System.Boolean.FalseString" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not equivalent to <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</exception>
		// Token: 0x06000A3B RID: 2619 RVA: 0x0002105C File Offset: 0x0001F25C
		[__DynamicallyInvokable]
		public static bool Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			bool flag = false;
			if (!bool.TryParse(value, out flag))
			{
				throw new FormatException(Environment.GetResourceString("Format_BadBoolean"));
			}
			return flag;
		}

		/// <summary>Tries to convert the specified string representation of a logical value to its <see cref="T:System.Boolean" /> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="value">A string containing the value to convert.</param>
		/// <param name="result">When this method returns, if the conversion succeeded, contains <see langword="true" /> if <paramref name="value" /> is equal to <see cref="F:System.Boolean.TrueString" /> or <see langword="false" /> if <paramref name="value" /> is equal to <see cref="F:System.Boolean.FalseString" />. If the conversion failed, contains <see langword="false" />. The conversion fails if <paramref name="value" /> is <see langword="null" /> or is not equal to the value of either the <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" /> field.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A3C RID: 2620 RVA: 0x00021094 File Offset: 0x0001F294
		[__DynamicallyInvokable]
		public static bool TryParse(string value, out bool result)
		{
			result = false;
			if (value == null)
			{
				return false;
			}
			if ("True".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				result = true;
				return true;
			}
			if ("False".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				result = false;
				return true;
			}
			value = bool.TrimWhiteSpaceAndNull(value);
			if ("True".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				result = true;
				return true;
			}
			if ("False".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				result = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00021100 File Offset: 0x0001F300
		private static string TrimWhiteSpaceAndNull(string value)
		{
			int i = 0;
			int num = value.Length - 1;
			char c = '\0';
			while (i < value.Length)
			{
				if (!char.IsWhiteSpace(value[i]) && value[i] != c)
				{
					IL_52:
					while (num >= i && (char.IsWhiteSpace(value[num]) || value[num] == c))
					{
						num--;
					}
					return value.Substring(i, num - i + 1);
				}
				i++;
			}
			goto IL_52;
		}

		/// <summary>Returns the type code for the <see cref="T:System.Boolean" /> value type.</summary>
		/// <returns>The enumerated constant <see cref="F:System.TypeCode.Boolean" />.</returns>
		// Token: 0x06000A3E RID: 2622 RVA: 0x0002116F File Offset: 0x0001F36F
		public TypeCode GetTypeCode()
		{
			return TypeCode.Boolean;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>
		///   <see langword="true" /> or <see langword="false" />.</returns>
		// Token: 0x06000A3F RID: 2623 RVA: 0x00021172 File Offset: 0x0001F372
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">You attempt to convert a <see cref="T:System.Boolean" /> value to a <see cref="T:System.Char" /> value. This conversion is not supported.</exception>
		// Token: 0x06000A40 RID: 2624 RVA: 0x00021176 File Offset: 0x0001F376
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Boolean", "Char" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x06000A41 RID: 2625 RVA: 0x0002119D File Offset: 0x0001F39D
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if the value of this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x06000A42 RID: 2626 RVA: 0x000211A6 File Offset: 0x0001F3A6
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x06000A43 RID: 2627 RVA: 0x000211AF File Offset: 0x0001F3AF
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x06000A44 RID: 2628 RVA: 0x000211B8 File Offset: 0x0001F3B8
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x06000A45 RID: 2629 RVA: 0x000211C1 File Offset: 0x0001F3C1
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x06000A46 RID: 2630 RVA: 0x000211CA File Offset: 0x0001F3CA
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x06000A47 RID: 2631 RVA: 0x000211D3 File Offset: 0x0001F3D3
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x06000A48 RID: 2632 RVA: 0x000211DC File Offset: 0x0001F3DC
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x06000A49 RID: 2633 RVA: 0x000211E5 File Offset: 0x0001F3E5
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x06000A4A RID: 2634 RVA: 0x000211EE File Offset: 0x0001F3EE
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>1 if this instance is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x06000A4B RID: 2635 RVA: 0x000211F7 File Offset: 0x0001F3F7
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">You attempt to convert a <see cref="T:System.Boolean" /> value to a <see cref="T:System.DateTime" /> value. This conversion is not supported.</exception>
		// Token: 0x06000A4C RID: 2636 RVA: 0x00021200 File Offset: 0x0001F400
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Boolean", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <param name="type">The desired type.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies culture-specific information about the format of the returned value.</param>
		/// <returns>An object of the specified type, with a value that is equivalent to the value of this <see langword="Boolean" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The requested type conversion is not supported.</exception>
		// Token: 0x06000A4D RID: 2637 RVA: 0x00021227 File Offset: 0x0001F427
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040003ED RID: 1005
		private bool m_value;

		// Token: 0x040003EE RID: 1006
		internal const int True = 1;

		// Token: 0x040003EF RID: 1007
		internal const int False = 0;

		// Token: 0x040003F0 RID: 1008
		internal const string TrueLiteral = "True";

		// Token: 0x040003F1 RID: 1009
		internal const string FalseLiteral = "False";

		/// <summary>Represents the Boolean value <see langword="true" /> as a string. This field is read-only.</summary>
		// Token: 0x040003F2 RID: 1010
		[__DynamicallyInvokable]
		public static readonly string TrueString = "True";

		/// <summary>Represents the Boolean value <see langword="false" /> as a string. This field is read-only.</summary>
		// Token: 0x040003F3 RID: 1011
		[__DynamicallyInvokable]
		public static readonly string FalseString = "False";
	}
}
