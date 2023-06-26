using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System
{
	/// <summary>Provides the base class for enumerations.</summary>
	// Token: 0x020000DA RID: 218
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Enum : ValueType, IComparable, IFormattable, IConvertible
	{
		// Token: 0x06000DEA RID: 3562 RVA: 0x0002A970 File Offset: 0x00028B70
		[SecuritySafeCritical]
		private static Enum.ValuesAndNames GetCachedValuesAndNames(RuntimeType enumType, bool getNames)
		{
			Enum.ValuesAndNames valuesAndNames = enumType.GenericCache as Enum.ValuesAndNames;
			if (valuesAndNames == null || (getNames && valuesAndNames.Names == null))
			{
				ulong[] array = null;
				string[] array2 = null;
				Enum.GetEnumValuesAndNames(enumType.GetTypeHandleInternal(), JitHelpers.GetObjectHandleOnStack<ulong[]>(ref array), JitHelpers.GetObjectHandleOnStack<string[]>(ref array2), getNames);
				valuesAndNames = new Enum.ValuesAndNames(array, array2);
				enumType.GenericCache = valuesAndNames;
			}
			return valuesAndNames;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0002A9C8 File Offset: 0x00028BC8
		private static string InternalFormattedHexString(object value)
		{
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.Boolean:
				return Convert.ToByte((bool)value).ToString("X2", null);
			case TypeCode.Char:
				return ((ushort)((char)value)).ToString("X4", null);
			case TypeCode.SByte:
				return ((byte)((sbyte)value)).ToString("X2", null);
			case TypeCode.Byte:
				return ((byte)value).ToString("X2", null);
			case TypeCode.Int16:
				return ((ushort)((short)value)).ToString("X4", null);
			case TypeCode.UInt16:
				return ((ushort)value).ToString("X4", null);
			case TypeCode.Int32:
				return ((uint)((int)value)).ToString("X8", null);
			case TypeCode.UInt32:
				return ((uint)value).ToString("X8", null);
			case TypeCode.Int64:
				return ((ulong)((long)value)).ToString("X16", null);
			case TypeCode.UInt64:
				return ((ulong)value).ToString("X16", null);
			default:
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnknownEnumType"));
			}
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0002AB00 File Offset: 0x00028D00
		private static string InternalFormat(RuntimeType eT, object value)
		{
			if (eT.IsDefined(typeof(FlagsAttribute), false))
			{
				return Enum.InternalFlagsFormat(eT, value);
			}
			string name = Enum.GetName(eT, value);
			if (name == null)
			{
				return value.ToString();
			}
			return name;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0002AB3C File Offset: 0x00028D3C
		private static string InternalFlagsFormat(RuntimeType eT, object value)
		{
			ulong num = Enum.ToUInt64(value);
			Enum.ValuesAndNames cachedValuesAndNames = Enum.GetCachedValuesAndNames(eT, true);
			string[] names = cachedValuesAndNames.Names;
			ulong[] values = cachedValuesAndNames.Values;
			int num2 = values.Length - 1;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			ulong num3 = num;
			while (num2 >= 0 && (num2 != 0 || values[num2] != 0UL))
			{
				if ((num & values[num2]) == values[num2])
				{
					num -= values[num2];
					if (!flag)
					{
						stringBuilder.Insert(0, ", ");
					}
					stringBuilder.Insert(0, names[num2]);
					flag = false;
				}
				num2--;
			}
			if (num != 0UL)
			{
				return value.ToString();
			}
			if (num3 != 0UL)
			{
				return stringBuilder.ToString();
			}
			if (values.Length != 0 && values[0] == 0UL)
			{
				return names[0];
			}
			return "0";
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0002ABF0 File Offset: 0x00028DF0
		internal static ulong ToUInt64(object value)
		{
			ulong num;
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				num = Convert.ToUInt64(value, CultureInfo.InvariantCulture);
				break;
			case TypeCode.SByte:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
				num = (ulong)Convert.ToInt64(value, CultureInfo.InvariantCulture);
				break;
			default:
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnknownEnumType"));
			}
			return num;
		}

		// Token: 0x06000DEF RID: 3567
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalCompareTo(object o1, object o2);

		// Token: 0x06000DF0 RID: 3568
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType InternalGetUnderlyingType(RuntimeType enumType);

		// Token: 0x06000DF1 RID: 3569
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetEnumValuesAndNames(RuntimeTypeHandle enumType, ObjectHandleOnStack values, ObjectHandleOnStack names, bool getNames);

		// Token: 0x06000DF2 RID: 3570
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object InternalBoxEnum(RuntimeType enumType, long value);

		/// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object. The return value indicates whether the conversion succeeded.</summary>
		/// <param name="value">The case-sensitive string representation of the enumeration name or underlying value to convert.</param>
		/// <param name="result">When this method returns, <paramref name="result" /> contains an object of type TEnum whose value is represented by <paramref name="value" /> if the parse operation succeeds. If the parse operation fails, <paramref name="result" /> contains the default value of the underlying type of TEnum. Note that this value need not be a member of the TEnum enumeration. This parameter is passed uninitialized.</param>
		/// <typeparam name="TEnum">The enumeration type to which to convert <paramref name="value" />.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="TEnum" /> is not an enumeration type.</exception>
		// Token: 0x06000DF3 RID: 3571 RVA: 0x0002AC63 File Offset: 0x00028E63
		[__DynamicallyInvokable]
		public static bool TryParse<TEnum>(string value, out TEnum result) where TEnum : struct
		{
			return Enum.TryParse<TEnum>(value, false, out result);
		}

		/// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object. A parameter specifies whether the operation is case-sensitive. The return value indicates whether the conversion succeeded.</summary>
		/// <param name="value">The string representation of the enumeration name or underlying value to convert.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to ignore case; <see langword="false" /> to consider case.</param>
		/// <param name="result">When this method returns, <paramref name="result" /> contains an object of type TEnum whose value is represented by <paramref name="value" /> if the parse operation succeeds. If the parse operation fails, <paramref name="result" /> contains the default value of the underlying type of TEnum. Note that this value need not be a member of the TEnum enumeration. This parameter is passed uninitialized.</param>
		/// <typeparam name="TEnum">The enumeration type to which to convert <paramref name="value" />.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="TEnum" /> is not an enumeration type.</exception>
		// Token: 0x06000DF4 RID: 3572 RVA: 0x0002AC70 File Offset: 0x00028E70
		[__DynamicallyInvokable]
		public static bool TryParse<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct
		{
			result = default(TEnum);
			Enum.EnumResult enumResult = default(Enum.EnumResult);
			enumResult.Init(false);
			bool flag;
			if (flag = Enum.TryParseEnum(typeof(TEnum), value, ignoreCase, ref enumResult))
			{
				result = (TEnum)((object)enumResult.parsedEnum);
			}
			return flag;
		}

		/// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.</summary>
		/// <param name="enumType">An enumeration type.</param>
		/// <param name="value">A string containing the name or value to convert.</param>
		/// <returns>An object of type <paramref name="enumType" /> whose value is represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.  
		/// -or-  
		/// <paramref name="value" /> is either an empty string or only contains white space.  
		/// -or-  
		/// <paramref name="value" /> is a name, but not one of the named constants defined for the enumeration.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is outside the range of the underlying type of <paramref name="enumType" />.</exception>
		// Token: 0x06000DF5 RID: 3573 RVA: 0x0002ACBD File Offset: 0x00028EBD
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static object Parse(Type enumType, string value)
		{
			return Enum.Parse(enumType, value, false);
		}

		/// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object. A parameter specifies whether the operation is case-insensitive.</summary>
		/// <param name="enumType">An enumeration type.</param>
		/// <param name="value">A string containing the name or value to convert.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to ignore case; <see langword="false" /> to regard case.</param>
		/// <returns>An object of type <paramref name="enumType" /> whose value is represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.  
		/// -or-  
		/// <paramref name="value" /> is either an empty string ("") or only contains white space.  
		/// -or-  
		/// <paramref name="value" /> is a name, but not one of the named constants defined for the enumeration.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is outside the range of the underlying type of <paramref name="enumType" />.</exception>
		// Token: 0x06000DF6 RID: 3574 RVA: 0x0002ACC8 File Offset: 0x00028EC8
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static object Parse(Type enumType, string value, bool ignoreCase)
		{
			Enum.EnumResult enumResult = default(Enum.EnumResult);
			enumResult.Init(true);
			if (Enum.TryParseEnum(enumType, value, ignoreCase, ref enumResult))
			{
				return enumResult.parsedEnum;
			}
			throw enumResult.GetEnumParseException();
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0002AD00 File Offset: 0x00028F00
		private static bool TryParseEnum(Type enumType, string value, bool ignoreCase, ref Enum.EnumResult parseResult)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
			}
			if (value == null)
			{
				parseResult.SetFailure(Enum.ParseFailureKind.ArgumentNull, "value");
				return false;
			}
			value = value.Trim();
			if (value.Length == 0)
			{
				parseResult.SetFailure(Enum.ParseFailureKind.Argument, "Arg_MustContainEnumInfo", null);
				return false;
			}
			ulong num = 0UL;
			if (char.IsDigit(value[0]) || value[0] == '-' || value[0] == '+')
			{
				Type underlyingType = Enum.GetUnderlyingType(enumType);
				try
				{
					object obj = Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
					parseResult.parsedEnum = Enum.ToObject(enumType, obj);
					return true;
				}
				catch (FormatException)
				{
				}
				catch (Exception ex)
				{
					if (parseResult.canThrow)
					{
						throw;
					}
					parseResult.SetFailure(ex);
					return false;
				}
			}
			string[] array = value.Split(Enum.enumSeperatorCharArray);
			Enum.ValuesAndNames cachedValuesAndNames = Enum.GetCachedValuesAndNames(runtimeType, true);
			string[] names = cachedValuesAndNames.Names;
			ulong[] values = cachedValuesAndNames.Values;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
				bool flag = false;
				int j = 0;
				while (j < names.Length)
				{
					if (ignoreCase)
					{
						if (string.Compare(names[j], array[i], StringComparison.OrdinalIgnoreCase) == 0)
						{
							goto IL_15D;
						}
					}
					else if (names[j].Equals(array[i]))
					{
						goto IL_15D;
					}
					j++;
					continue;
					IL_15D:
					ulong num2 = values[j];
					num |= num2;
					flag = true;
					break;
				}
				if (!flag)
				{
					parseResult.SetFailure(Enum.ParseFailureKind.ArgumentWithParameter, "Arg_EnumValueNotFound", value);
					return false;
				}
			}
			bool flag2;
			try
			{
				parseResult.parsedEnum = Enum.ToObject(enumType, num);
				flag2 = true;
			}
			catch (Exception ex2)
			{
				if (parseResult.canThrow)
				{
					throw;
				}
				parseResult.SetFailure(ex2);
				flag2 = false;
			}
			return flag2;
		}

		/// <summary>Returns the underlying type of the specified enumeration.</summary>
		/// <param name="enumType">The enumeration whose underlying type will be retrieved.</param>
		/// <returns>The underlying type of <paramref name="enumType" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x06000DF8 RID: 3576 RVA: 0x0002AF00 File Offset: 0x00029100
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static Type GetUnderlyingType(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumUnderlyingType();
		}

		/// <summary>Retrieves an array of the values of the constants in a specified enumeration.</summary>
		/// <param name="enumType">An enumeration type.</param>
		/// <returns>An array that contains the values of the constants in <paramref name="enumType" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is invoked by reflection in a reflection-only context,  
		///  -or-  
		///  <paramref name="enumType" /> is a type from an assembly loaded in a reflection-only context.</exception>
		// Token: 0x06000DF9 RID: 3577 RVA: 0x0002AF1C File Offset: 0x0002911C
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static Array GetValues(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumValues();
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0002AF38 File Offset: 0x00029138
		internal static ulong[] InternalGetValues(RuntimeType enumType)
		{
			return Enum.GetCachedValuesAndNames(enumType, false).Values;
		}

		/// <summary>Retrieves the name of the constant in the specified enumeration that has the specified value.</summary>
		/// <param name="enumType">An enumeration type.</param>
		/// <param name="value">The value of a particular enumerated constant in terms of its underlying type.</param>
		/// <returns>A string containing the name of the enumerated constant in <paramref name="enumType" /> whose value is <paramref name="value" />; or <see langword="null" /> if no such constant is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.  
		/// -or-  
		/// <paramref name="value" /> is neither of type <paramref name="enumType" /> nor does it have the same underlying type as <paramref name="enumType" />.</exception>
		// Token: 0x06000DFB RID: 3579 RVA: 0x0002AF46 File Offset: 0x00029146
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static string GetName(Type enumType, object value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumName(value);
		}

		/// <summary>Retrieves an array of the names of the constants in a specified enumeration.</summary>
		/// <param name="enumType">An enumeration type.</param>
		/// <returns>A string array of the names of the constants in <paramref name="enumType" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> parameter is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x06000DFC RID: 3580 RVA: 0x0002AF63 File Offset: 0x00029163
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static string[] GetNames(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumNames();
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0002AF7F File Offset: 0x0002917F
		internal static string[] InternalGetNames(RuntimeType enumType)
		{
			return Enum.GetCachedValuesAndNames(enumType, true).Names;
		}

		/// <summary>Converts the specified object with an integer value to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value convert to an enumeration member.</param>
		/// <returns>An enumeration object whose value is <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.  
		/// -or-  
		/// <paramref name="value" /> is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />.</exception>
		// Token: 0x06000DFE RID: 3582 RVA: 0x0002AF90 File Offset: 0x00029190
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static object ToObject(Type enumType, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			TypeCode typeCode = Convert.GetTypeCode(value);
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && (typeCode == TypeCode.Boolean || typeCode == TypeCode.Char))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnumBaseTypeOrEnum"), "value");
			}
			switch (typeCode)
			{
			case TypeCode.Boolean:
				return Enum.ToObject(enumType, (bool)value);
			case TypeCode.Char:
				return Enum.ToObject(enumType, (char)value);
			case TypeCode.SByte:
				return Enum.ToObject(enumType, (sbyte)value);
			case TypeCode.Byte:
				return Enum.ToObject(enumType, (byte)value);
			case TypeCode.Int16:
				return Enum.ToObject(enumType, (short)value);
			case TypeCode.UInt16:
				return Enum.ToObject(enumType, (ushort)value);
			case TypeCode.Int32:
				return Enum.ToObject(enumType, (int)value);
			case TypeCode.UInt32:
				return Enum.ToObject(enumType, (uint)value);
			case TypeCode.Int64:
				return Enum.ToObject(enumType, (long)value);
			case TypeCode.UInt64:
				return Enum.ToObject(enumType, (ulong)value);
			default:
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnumBaseTypeOrEnum"), "value");
			}
		}

		/// <summary>Returns a Boolean telling whether a given integral value, or its name as a string, exists in a specified enumeration.</summary>
		/// <param name="enumType">An enumeration type.</param>
		/// <param name="value">The value or name of a constant in <paramref name="enumType" />.</param>
		/// <returns>
		///   <see langword="true" /> if a constant in <paramref name="enumType" /> has a value equal to <paramref name="value" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see langword="Enum" />.  
		/// -or-  
		/// The type of <paramref name="value" /> is an enumeration, but it is not an enumeration of type <paramref name="enumType" />.  
		/// -or-  
		/// The type of <paramref name="value" /> is not an underlying type of <paramref name="enumType" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />, or <see cref="T:System.String" />.</exception>
		// Token: 0x06000DFF RID: 3583 RVA: 0x0002B0A1 File Offset: 0x000292A1
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static bool IsDefined(Type enumType, object value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.IsEnumDefined(value);
		}

		/// <summary>Converts the specified value of a specified enumerated type to its equivalent string representation according to the specified format.</summary>
		/// <param name="enumType">The enumeration type of the value to convert.</param>
		/// <param name="value">The value to convert.</param>
		/// <param name="format">The output format to use.</param>
		/// <returns>A string representation of <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="enumType" />, <paramref name="value" />, or <paramref name="format" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="enumType" /> parameter is not an <see cref="T:System.Enum" /> type.  
		///  -or-  
		///  The <paramref name="value" /> is from an enumeration that differs in type from <paramref name="enumType" />.  
		///  -or-  
		///  The type of <paramref name="value" /> is not an underlying type of <paramref name="enumType" />.</exception>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
		// Token: 0x06000E00 RID: 3584 RVA: 0x0002B0C0 File Offset: 0x000292C0
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static string Format(Type enumType, object value, string format)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
			}
			Type type = value.GetType();
			Type underlyingType = Enum.GetUnderlyingType(enumType);
			if (type.IsEnum)
			{
				Type underlyingType2 = Enum.GetUnderlyingType(type);
				if (!type.IsEquivalentTo(enumType))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_EnumAndObjectMustBeSameType", new object[]
					{
						type.ToString(),
						enumType.ToString()
					}));
				}
				value = ((Enum)value).GetValue();
			}
			else if (type != underlyingType)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumFormatUnderlyingTypeAndObjectMustBeSameType", new object[]
				{
					type.ToString(),
					underlyingType.ToString()
				}));
			}
			if (format.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("Format_InvalidEnumFormatSpecification"));
			}
			char c = format[0];
			if (c == 'D' || c == 'd')
			{
				return value.ToString();
			}
			if (c == 'X' || c == 'x')
			{
				return Enum.InternalFormattedHexString(value);
			}
			if (c == 'G' || c == 'g')
			{
				return Enum.InternalFormat(runtimeType, value);
			}
			if (c == 'F' || c == 'f')
			{
				return Enum.InternalFlagsFormat(runtimeType, value);
			}
			throw new FormatException(Environment.GetResourceString("Format_InvalidEnumFormatSpecification"));
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0002B248 File Offset: 0x00029448
		[SecuritySafeCritical]
		internal unsafe object GetValue()
		{
			fixed (byte* ptr = &JitHelpers.GetPinningHelper(this).m_data)
			{
				void* ptr2 = (void*)ptr;
				switch (this.InternalGetCorElementType())
				{
				case CorElementType.Boolean:
					return *(byte*)ptr2 != 0;
				case CorElementType.Char:
					return (char)(*(ushort*)ptr2);
				case CorElementType.I1:
					return *(sbyte*)ptr2;
				case CorElementType.U1:
					return *(byte*)ptr2;
				case CorElementType.I2:
					return *(short*)ptr2;
				case CorElementType.U2:
					return *(ushort*)ptr2;
				case CorElementType.I4:
					return *(int*)ptr2;
				case CorElementType.U4:
					return *(uint*)ptr2;
				case CorElementType.I8:
					return *(long*)ptr2;
				case CorElementType.U8:
					return (ulong)(*(long*)ptr2);
				case CorElementType.R4:
					return *(float*)ptr2;
				case CorElementType.R8:
					return *(double*)ptr2;
				case CorElementType.I:
					return *(IntPtr*)ptr2;
				case CorElementType.U:
					return (UIntPtr)(*(IntPtr*)ptr2);
				}
				return null;
			}
		}

		// Token: 0x06000E02 RID: 3586
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool InternalHasFlag(Enum flags);

		// Token: 0x06000E03 RID: 3587
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern CorElementType InternalGetCorElementType();

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an enumeration value of the same type and with the same underlying value as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000E04 RID: 3588
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern bool Equals(object obj);

		/// <summary>Returns the hash code for the value of this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000E05 RID: 3589 RVA: 0x0002B348 File Offset: 0x00029548
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetHashCode()
		{
			fixed (byte* ptr = &JitHelpers.GetPinningHelper(this).m_data)
			{
				void* ptr2 = (void*)ptr;
				switch (this.InternalGetCorElementType())
				{
				case CorElementType.Boolean:
					return ((bool*)ptr2)->GetHashCode();
				case CorElementType.Char:
					return ((char*)ptr2)->GetHashCode();
				case CorElementType.I1:
					return ((sbyte*)ptr2)->GetHashCode();
				case CorElementType.U1:
					return ((byte*)ptr2)->GetHashCode();
				case CorElementType.I2:
					return ((short*)ptr2)->GetHashCode();
				case CorElementType.U2:
					return ((ushort*)ptr2)->GetHashCode();
				case CorElementType.I4:
					return ((int*)ptr2)->GetHashCode();
				case CorElementType.U4:
					return ((uint*)ptr2)->GetHashCode();
				case CorElementType.I8:
					return ((long*)ptr2)->GetHashCode();
				case CorElementType.U8:
					return ((ulong*)ptr2)->GetHashCode();
				case CorElementType.R4:
					return ((float*)ptr2)->GetHashCode();
				case CorElementType.R8:
					return ((double*)ptr2)->GetHashCode();
				case CorElementType.I:
					return ((IntPtr*)ptr2)->GetHashCode();
				case CorElementType.U:
					return ((UIntPtr*)ptr2)->GetHashCode();
				}
				return 0;
			}
		}

		/// <summary>Converts the value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		// Token: 0x06000E06 RID: 3590 RVA: 0x0002B438 File Offset: 0x00029638
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return Enum.InternalFormat((RuntimeType)base.GetType(), this.GetValue());
		}

		/// <summary>This method overload is obsolete; use <see cref="M:System.Enum.ToString(System.String)" />.</summary>
		/// <param name="format">A format specification.</param>
		/// <param name="provider">(Obsolete.)</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> does not contain a valid format specification.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
		// Token: 0x06000E07 RID: 3591 RVA: 0x0002B450 File Offset: 0x00029650
		[Obsolete("The provider argument is not used. Please use ToString(String).")]
		public string ToString(string format, IFormatProvider provider)
		{
			return this.ToString(format);
		}

		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <param name="target">An object to compare, or <see langword="null" />.</param>
		/// <returns>A signed number that indicates the relative values of this instance and <paramref name="target" />.  
		///   Value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///   The value of this instance is less than the value of <paramref name="target" />.  
		///
		///   Zero  
		///
		///   The value of this instance is equal to the value of <paramref name="target" />.  
		///
		///   Greater than zero  
		///
		///   The value of this instance is greater than the value of <paramref name="target" />.  
		///
		///  -or-  
		///
		///  <paramref name="target" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> and this instance are not the same type.</exception>
		/// <exception cref="T:System.InvalidOperationException">This instance is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />.</exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		// Token: 0x06000E08 RID: 3592 RVA: 0x0002B45C File Offset: 0x0002965C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public int CompareTo(object target)
		{
			if (this == null)
			{
				throw new NullReferenceException();
			}
			int num = Enum.InternalCompareTo(this, target);
			if (num < 2)
			{
				return num;
			}
			if (num == 2)
			{
				Type type = base.GetType();
				Type type2 = target.GetType();
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumAndObjectMustBeSameType", new object[]
				{
					type2.ToString(),
					type.ToString()
				}));
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnknownEnumType"));
		}

		/// <summary>Converts the value of this instance to its equivalent string representation using the specified format.</summary>
		/// <param name="format">A format string.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> contains an invalid specification.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
		// Token: 0x06000E09 RID: 3593 RVA: 0x0002B4CC File Offset: 0x000296CC
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			if (format == null || format.Length == 0)
			{
				format = "G";
			}
			if (string.Compare(format, "G", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return this.ToString();
			}
			if (string.Compare(format, "D", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return this.GetValue().ToString();
			}
			if (string.Compare(format, "X", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return Enum.InternalFormattedHexString(this.GetValue());
			}
			if (string.Compare(format, "F", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return Enum.InternalFlagsFormat((RuntimeType)base.GetType(), this.GetValue());
			}
			throw new FormatException(Environment.GetResourceString("Format_InvalidEnumFormatSpecification"));
		}

		/// <summary>This method overload is obsolete; use <see cref="M:System.Enum.ToString" />.</summary>
		/// <param name="provider">(obsolete)</param>
		/// <returns>The string representation of the value of this instance.</returns>
		// Token: 0x06000E0A RID: 3594 RVA: 0x0002B568 File Offset: 0x00029768
		[Obsolete("The provider argument is not used. Please use ToString().")]
		public string ToString(IFormatProvider provider)
		{
			return this.ToString();
		}

		/// <summary>Determines whether one or more bit fields are set in the current instance.</summary>
		/// <param name="flag">An enumeration value.</param>
		/// <returns>
		///   <see langword="true" /> if the bit field or bit fields that are set in <paramref name="flag" /> are also set in the current instance; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flag" /> is a different type than the current instance.</exception>
		// Token: 0x06000E0B RID: 3595 RVA: 0x0002B570 File Offset: 0x00029770
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool HasFlag(Enum flag)
		{
			if (flag == null)
			{
				throw new ArgumentNullException("flag");
			}
			if (!base.GetType().IsEquivalentTo(flag.GetType()))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EnumTypeDoesNotMatch", new object[]
				{
					flag.GetType(),
					base.GetType()
				}));
			}
			return this.InternalHasFlag(flag);
		}

		/// <summary>Returns the type code of the underlying type of this enumeration member.</summary>
		/// <returns>The type code of the underlying type of this instance.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumeration type is unknown.</exception>
		// Token: 0x06000E0C RID: 3596 RVA: 0x0002B5D0 File Offset: 0x000297D0
		public TypeCode GetTypeCode()
		{
			Type type = base.GetType();
			Type underlyingType = Enum.GetUnderlyingType(type);
			if (underlyingType == typeof(int))
			{
				return TypeCode.Int32;
			}
			if (underlyingType == typeof(sbyte))
			{
				return TypeCode.SByte;
			}
			if (underlyingType == typeof(short))
			{
				return TypeCode.Int16;
			}
			if (underlyingType == typeof(long))
			{
				return TypeCode.Int64;
			}
			if (underlyingType == typeof(uint))
			{
				return TypeCode.UInt32;
			}
			if (underlyingType == typeof(byte))
			{
				return TypeCode.Byte;
			}
			if (underlyingType == typeof(ushort))
			{
				return TypeCode.UInt16;
			}
			if (underlyingType == typeof(ulong))
			{
				return TypeCode.UInt64;
			}
			if (underlyingType == typeof(bool))
			{
				return TypeCode.Boolean;
			}
			if (underlyingType == typeof(char))
			{
				return TypeCode.Char;
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnknownEnumType"));
		}

		/// <summary>Converts the current value to a Boolean value based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>This member always throws an exception.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000E0D RID: 3597 RVA: 0x0002B6C6 File Offset: 0x000298C6
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a Unicode character based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>This member always throws an exception.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000E0E RID: 3598 RVA: 0x0002B6D8 File Offset: 0x000298D8
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to an 8-bit signed integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x06000E0F RID: 3599 RVA: 0x0002B6EA File Offset: 0x000298EA
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to an 8-bit unsigned integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x06000E10 RID: 3600 RVA: 0x0002B6FC File Offset: 0x000298FC
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 16-bit signed integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x06000E11 RID: 3601 RVA: 0x0002B70E File Offset: 0x0002990E
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 16-bit unsigned integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x06000E12 RID: 3602 RVA: 0x0002B720 File Offset: 0x00029920
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 32-bit signed integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x06000E13 RID: 3603 RVA: 0x0002B732 File Offset: 0x00029932
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 32-bit unsigned integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x06000E14 RID: 3604 RVA: 0x0002B744 File Offset: 0x00029944
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 64-bit signed integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x06000E15 RID: 3605 RVA: 0x0002B756 File Offset: 0x00029956
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 64-bit unsigned integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x06000E16 RID: 3606 RVA: 0x0002B768 File Offset: 0x00029968
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a single-precision floating-point number based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>This member always throws an exception.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000E17 RID: 3607 RVA: 0x0002B77A File Offset: 0x0002997A
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a double-precision floating point number based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>This member always throws an exception.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000E18 RID: 3608 RVA: 0x0002B78C File Offset: 0x0002998C
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a <see cref="T:System.Decimal" /> based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>This member always throws an exception.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000E19 RID: 3609 RVA: 0x0002B79E File Offset: 0x0002999E
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a <see cref="T:System.DateTime" /> based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>This member always throws an exception.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000E1A RID: 3610 RVA: 0x0002B7B0 File Offset: 0x000299B0
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Enum", "DateTime" }));
		}

		/// <summary>Converts the current value to a specified type based on the underlying type.</summary>
		/// <param name="type">The type to convert to.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x06000E1B RID: 3611 RVA: 0x0002B7D7 File Offset: 0x000299D7
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		/// <summary>Converts the specified 8-bit signed integer value to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x06000E1C RID: 3612 RVA: 0x0002B7E4 File Offset: 0x000299E4
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[ComVisible(true)]
		public static object ToObject(Type enumType, sbyte value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		/// <summary>Converts the specified 16-bit signed integer to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x06000E1D RID: 3613 RVA: 0x0002B850 File Offset: 0x00029A50
		[SecuritySafeCritical]
		[ComVisible(true)]
		public static object ToObject(Type enumType, short value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		/// <summary>Converts the specified 32-bit signed integer to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x06000E1E RID: 3614 RVA: 0x0002B8BC File Offset: 0x00029ABC
		[SecuritySafeCritical]
		[ComVisible(true)]
		public static object ToObject(Type enumType, int value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		/// <summary>Converts the specified 8-bit unsigned integer to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x06000E1F RID: 3615 RVA: 0x0002B928 File Offset: 0x00029B28
		[SecuritySafeCritical]
		[ComVisible(true)]
		public static object ToObject(Type enumType, byte value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		/// <summary>Converts the specified 16-bit unsigned integer value to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x06000E20 RID: 3616 RVA: 0x0002B994 File Offset: 0x00029B94
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[ComVisible(true)]
		public static object ToObject(Type enumType, ushort value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		/// <summary>Converts the specified 32-bit unsigned integer value to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x06000E21 RID: 3617 RVA: 0x0002BA00 File Offset: 0x00029C00
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[ComVisible(true)]
		public static object ToObject(Type enumType, uint value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		/// <summary>Converts the specified 64-bit signed integer to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x06000E22 RID: 3618 RVA: 0x0002BA6C File Offset: 0x00029C6C
		[SecuritySafeCritical]
		[ComVisible(true)]
		public static object ToObject(Type enumType, long value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, value);
		}

		/// <summary>Converts the specified 64-bit unsigned integer value to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x06000E23 RID: 3619 RVA: 0x0002BAD8 File Offset: 0x00029CD8
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[ComVisible(true)]
		public static object ToObject(Type enumType, ulong value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0002BB44 File Offset: 0x00029D44
		[SecuritySafeCritical]
		private static object ToObject(Type enumType, char value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0002BBB0 File Offset: 0x00029DB0
		[SecuritySafeCritical]
		private static object ToObject(Type enumType, bool value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, value ? 1L : 0L);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Enum" /> class.</summary>
		// Token: 0x06000E26 RID: 3622 RVA: 0x0002BC21 File Offset: 0x00029E21
		[__DynamicallyInvokable]
		protected Enum()
		{
		}

		// Token: 0x04000569 RID: 1385
		private static readonly char[] enumSeperatorCharArray = new char[] { ',' };

		// Token: 0x0400056A RID: 1386
		private const string enumSeperator = ", ";

		// Token: 0x02000AE0 RID: 2784
		private enum ParseFailureKind
		{
			// Token: 0x0400313F RID: 12607
			None,
			// Token: 0x04003140 RID: 12608
			Argument,
			// Token: 0x04003141 RID: 12609
			ArgumentNull,
			// Token: 0x04003142 RID: 12610
			ArgumentWithParameter,
			// Token: 0x04003143 RID: 12611
			UnhandledException
		}

		// Token: 0x02000AE1 RID: 2785
		private struct EnumResult
		{
			// Token: 0x06006A12 RID: 27154 RVA: 0x0016E6BB File Offset: 0x0016C8BB
			internal void Init(bool canMethodThrow)
			{
				this.parsedEnum = 0;
				this.canThrow = canMethodThrow;
			}

			// Token: 0x06006A13 RID: 27155 RVA: 0x0016E6D0 File Offset: 0x0016C8D0
			internal void SetFailure(Exception unhandledException)
			{
				this.m_failure = Enum.ParseFailureKind.UnhandledException;
				this.m_innerException = unhandledException;
			}

			// Token: 0x06006A14 RID: 27156 RVA: 0x0016E6E0 File Offset: 0x0016C8E0
			internal void SetFailure(Enum.ParseFailureKind failure, string failureParameter)
			{
				this.m_failure = failure;
				this.m_failureParameter = failureParameter;
				if (this.canThrow)
				{
					throw this.GetEnumParseException();
				}
			}

			// Token: 0x06006A15 RID: 27157 RVA: 0x0016E6FF File Offset: 0x0016C8FF
			internal void SetFailure(Enum.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
			{
				this.m_failure = failure;
				this.m_failureMessageID = failureMessageID;
				this.m_failureMessageFormatArgument = failureMessageFormatArgument;
				if (this.canThrow)
				{
					throw this.GetEnumParseException();
				}
			}

			// Token: 0x06006A16 RID: 27158 RVA: 0x0016E728 File Offset: 0x0016C928
			internal Exception GetEnumParseException()
			{
				switch (this.m_failure)
				{
				case Enum.ParseFailureKind.Argument:
					return new ArgumentException(Environment.GetResourceString(this.m_failureMessageID));
				case Enum.ParseFailureKind.ArgumentNull:
					return new ArgumentNullException(this.m_failureParameter);
				case Enum.ParseFailureKind.ArgumentWithParameter:
					return new ArgumentException(Environment.GetResourceString(this.m_failureMessageID, new object[] { this.m_failureMessageFormatArgument }));
				case Enum.ParseFailureKind.UnhandledException:
					return this.m_innerException;
				default:
					return new ArgumentException(Environment.GetResourceString("Arg_EnumValueNotFound"));
				}
			}

			// Token: 0x04003144 RID: 12612
			internal object parsedEnum;

			// Token: 0x04003145 RID: 12613
			internal bool canThrow;

			// Token: 0x04003146 RID: 12614
			internal Enum.ParseFailureKind m_failure;

			// Token: 0x04003147 RID: 12615
			internal string m_failureMessageID;

			// Token: 0x04003148 RID: 12616
			internal string m_failureParameter;

			// Token: 0x04003149 RID: 12617
			internal object m_failureMessageFormatArgument;

			// Token: 0x0400314A RID: 12618
			internal Exception m_innerException;
		}

		// Token: 0x02000AE2 RID: 2786
		private class ValuesAndNames
		{
			// Token: 0x06006A17 RID: 27159 RVA: 0x0016E7A9 File Offset: 0x0016C9A9
			public ValuesAndNames(ulong[] values, string[] names)
			{
				this.Values = values;
				this.Names = names;
			}

			// Token: 0x0400314B RID: 12619
			public ulong[] Values;

			// Token: 0x0400314C RID: 12620
			public string[] Names;
		}
	}
}
