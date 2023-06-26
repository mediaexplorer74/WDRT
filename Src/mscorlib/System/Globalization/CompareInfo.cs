using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Win32;

namespace System.Globalization
{
	/// <summary>Implements a set of methods for culture-sensitive string comparisons.</summary>
	// Token: 0x020003A6 RID: 934
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class CompareInfo : IDeserializationCallback
	{
		// Token: 0x06002E1B RID: 11803 RVA: 0x000B178C File Offset: 0x000AF98C
		internal CompareInfo(CultureInfo culture)
		{
			this.m_name = culture.m_name;
			this.m_sortName = culture.SortName;
			IntPtr intPtr;
			this.m_dataHandle = CompareInfo.InternalInitSortHandle(this.m_sortName, out intPtr);
			this.m_handleOrigin = intPtr;
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the specified culture and that uses string comparison methods in the specified <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <param name="culture">An integer representing the culture identifier.</param>
		/// <param name="assembly">An <see cref="T:System.Reflection.Assembly" /> that contains the string comparison methods to use.</param>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" /> is of an invalid type.</exception>
		// Token: 0x06002E1C RID: 11804 RVA: 0x000B17D4 File Offset: 0x000AF9D4
		public static CompareInfo GetCompareInfo(int culture, Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (assembly != typeof(object).Module.Assembly)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_OnlyMscorlib"));
			}
			return CompareInfo.GetCompareInfo(culture);
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the specified culture and that uses string comparison methods in the specified <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <param name="name">A string representing the culture name.</param>
		/// <param name="assembly">An <see cref="T:System.Reflection.Assembly" /> that contains the string comparison methods to use.</param>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="assembly" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an invalid culture name.  
		/// -or-  
		/// <paramref name="assembly" /> is of an invalid type.</exception>
		// Token: 0x06002E1D RID: 11805 RVA: 0x000B1828 File Offset: 0x000AFA28
		public static CompareInfo GetCompareInfo(string name, Assembly assembly)
		{
			if (name == null || assembly == null)
			{
				throw new ArgumentNullException((name == null) ? "name" : "assembly");
			}
			if (assembly != typeof(object).Module.Assembly)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_OnlyMscorlib"));
			}
			return CompareInfo.GetCompareInfo(name);
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the culture with the specified identifier.</summary>
		/// <param name="culture">An integer representing the culture identifier.</param>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		// Token: 0x06002E1E RID: 11806 RVA: 0x000B1888 File Offset: 0x000AFA88
		public static CompareInfo GetCompareInfo(int culture)
		{
			if (CultureData.IsCustomCultureId(culture))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_CustomCultureCannotBePassedByNumber", new object[] { "culture" }));
			}
			return CultureInfo.GetCultureInfo(culture).CompareInfo;
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the culture with the specified name.</summary>
		/// <param name="name">A string representing the culture name.</param>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an invalid culture name.</exception>
		// Token: 0x06002E1F RID: 11807 RVA: 0x000B18BB File Offset: 0x000AFABB
		[__DynamicallyInvokable]
		public static CompareInfo GetCompareInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return CultureInfo.GetCultureInfo(name).CompareInfo;
		}

		/// <summary>Indicates whether a specified Unicode character is sortable.</summary>
		/// <param name="ch">A Unicode character.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="ch" /> parameter is sortable; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E20 RID: 11808 RVA: 0x000B18D6 File Offset: 0x000AFAD6
		[ComVisible(false)]
		public static bool IsSortable(char ch)
		{
			return CompareInfo.IsSortable(ch.ToString());
		}

		/// <summary>Indicates whether a specified Unicode string is sortable.</summary>
		/// <param name="text">A string of zero or more Unicode characters.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="str" /> parameter is not an empty string ("") and all the Unicode characters in <paramref name="str" /> are sortable; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is <see langword="null" />.</exception>
		// Token: 0x06002E21 RID: 11809 RVA: 0x000B18E4 File Offset: 0x000AFAE4
		[SecuritySafeCritical]
		[ComVisible(false)]
		public static bool IsSortable(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (text.Length == 0)
			{
				return false;
			}
			CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
			return CompareInfo.InternalIsSortable(compareInfo.m_dataHandle, compareInfo.m_handleOrigin, compareInfo.m_sortName, text, text.Length);
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000B1932 File Offset: 0x000AFB32
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_name = null;
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000B193C File Offset: 0x000AFB3C
		private void OnDeserialized()
		{
			CultureInfo cultureInfo;
			if (this.m_name == null)
			{
				cultureInfo = CultureInfo.GetCultureInfo(this.culture);
				this.m_name = cultureInfo.m_name;
			}
			else
			{
				cultureInfo = CultureInfo.GetCultureInfo(this.m_name);
			}
			this.m_sortName = cultureInfo.SortName;
			IntPtr intPtr;
			this.m_dataHandle = CompareInfo.InternalInitSortHandle(this.m_sortName, out intPtr);
			this.m_handleOrigin = intPtr;
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000B199D File Offset: 0x000AFB9D
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000B19A5 File Offset: 0x000AFBA5
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.culture = CultureInfo.GetCultureInfo(this.Name).LCID;
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback.</param>
		// Token: 0x06002E26 RID: 11814 RVA: 0x000B19BD File Offset: 0x000AFBBD
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialized();
		}

		/// <summary>Gets the name of the culture used for sorting operations by this <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <returns>The name of a culture.</returns>
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06002E27 RID: 11815 RVA: 0x000B19C5 File Offset: 0x000AFBC5
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual string Name
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_name == "zh-CHT" || this.m_name == "zh-CHS")
				{
					return this.m_name;
				}
				return this.m_sortName;
			}
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000B19F8 File Offset: 0x000AFBF8
		internal static int GetNativeCompareFlags(CompareOptions options)
		{
			int num = 134217728;
			if ((options & CompareOptions.IgnoreCase) != CompareOptions.None)
			{
				num |= 1;
			}
			if ((options & CompareOptions.IgnoreKanaType) != CompareOptions.None)
			{
				num |= 65536;
			}
			if ((options & CompareOptions.IgnoreNonSpace) != CompareOptions.None)
			{
				num |= 2;
			}
			if ((options & CompareOptions.IgnoreSymbols) != CompareOptions.None)
			{
				num |= 4;
			}
			if ((options & CompareOptions.IgnoreWidth) != CompareOptions.None)
			{
				num |= 131072;
			}
			if ((options & CompareOptions.StringSort) != CompareOptions.None)
			{
				num |= 4096;
			}
			if (options == CompareOptions.Ordinal)
			{
				num = 1073741824;
			}
			return num;
		}

		/// <summary>Compares two strings.</summary>
		/// <param name="string1">The first string to compare.</param>
		/// <param name="string2">The second string to compare.</param>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.  
		///   Value  
		///
		///   Condition  
		///
		///   zero  
		///
		///   The two strings are equal.  
		///
		///   less than zero  
		///
		///  <paramref name="string1" /> is less than <paramref name="string2" />.  
		///
		///   greater than zero  
		///
		///  <paramref name="string1" /> is greater than <paramref name="string2" />.</returns>
		// Token: 0x06002E29 RID: 11817 RVA: 0x000B1A61 File Offset: 0x000AFC61
		[__DynamicallyInvokable]
		public virtual int Compare(string string1, string string2)
		{
			return this.Compare(string1, string2, CompareOptions.None);
		}

		/// <summary>Compares two strings using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="string1">The first string to compare.</param>
		/// <param name="string2">The second string to compare.</param>
		/// <param name="options">A value that defines how <paramref name="string1" /> and <paramref name="string2" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.  
		///   Value  
		///
		///   Condition  
		///
		///   zero  
		///
		///   The two strings are equal.  
		///
		///   less than zero  
		///
		///  <paramref name="string1" /> is less than <paramref name="string2" />.  
		///
		///   greater than zero  
		///
		///  <paramref name="string1" /> is greater than <paramref name="string2" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E2A RID: 11818 RVA: 0x000B1A6C File Offset: 0x000AFC6C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual int Compare(string string1, string string2, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return string.Compare(string1, string2, StringComparison.OrdinalIgnoreCase);
			}
			if ((options & CompareOptions.Ordinal) != CompareOptions.None)
			{
				if (options != CompareOptions.Ordinal)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_CompareOptionOrdinal"), "options");
				}
				return string.CompareOrdinal(string1, string2);
			}
			else
			{
				if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
				}
				if (string1 == null)
				{
					if (string2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (string2 == null)
					{
						return 1;
					}
					return CompareInfo.InternalCompareString(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, string1, 0, string1.Length, string2, 0, string2.Length, CompareInfo.GetNativeCompareFlags(options));
				}
			}
		}

		/// <summary>Compares a section of one string with a section of another string.</summary>
		/// <param name="string1">The first string to compare.</param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing.</param>
		/// <param name="length1">The number of consecutive characters in <paramref name="string1" /> to compare.</param>
		/// <param name="string2">The second string to compare.</param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing.</param>
		/// <param name="length2">The number of consecutive characters in <paramref name="string2" /> to compare.</param>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.  
		///   Value  
		///
		///   Condition  
		///
		///   zero  
		///
		///   The two strings are equal.  
		///
		///   less than zero  
		///
		///   The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />.  
		///
		///   greater than zero  
		///
		///   The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="length1" /> or <paramref name="offset2" /> or <paramref name="length2" /> is less than zero.  
		/// -or-  
		/// <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.  
		/// -or-  
		/// <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />.  
		/// -or-  
		/// <paramref name="length1" /> is greater than the number of characters from <paramref name="offset1" /> to the end of <paramref name="string1" />.  
		/// -or-  
		/// <paramref name="length2" /> is greater than the number of characters from <paramref name="offset2" /> to the end of <paramref name="string2" />.</exception>
		// Token: 0x06002E2B RID: 11819 RVA: 0x000B1B12 File Offset: 0x000AFD12
		[__DynamicallyInvokable]
		public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2)
		{
			return this.Compare(string1, offset1, length1, string2, offset2, length2, CompareOptions.None);
		}

		/// <summary>Compares the end section of a string with the end section of another string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="string1">The first string to compare.</param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing.</param>
		/// <param name="string2">The second string to compare.</param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing.</param>
		/// <param name="options">A value that defines how <paramref name="string1" /> and <paramref name="string2" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.  
		///   Value  
		///
		///   Condition  
		///
		///   zero  
		///
		///   The two strings are equal.  
		///
		///   less than zero  
		///
		///   The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />.  
		///
		///   greater than zero  
		///
		///   The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="offset2" /> is less than zero.  
		/// -or-  
		/// <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.  
		/// -or-  
		/// <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E2C RID: 11820 RVA: 0x000B1B24 File Offset: 0x000AFD24
		[__DynamicallyInvokable]
		public virtual int Compare(string string1, int offset1, string string2, int offset2, CompareOptions options)
		{
			return this.Compare(string1, offset1, (string1 == null) ? 0 : (string1.Length - offset1), string2, offset2, (string2 == null) ? 0 : (string2.Length - offset2), options);
		}

		/// <summary>Compares the end section of a string with the end section of another string.</summary>
		/// <param name="string1">The first string to compare.</param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing.</param>
		/// <param name="string2">The second string to compare.</param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing.</param>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.  
		///   Value  
		///
		///   Condition  
		///
		///   zero  
		///
		///   The two strings are equal.  
		///
		///   less than zero  
		///
		///   The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />.  
		///
		///   greater than zero  
		///
		///   The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="offset2" /> is less than zero.  
		/// -or-  
		/// <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.  
		/// -or-  
		/// <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />.</exception>
		// Token: 0x06002E2D RID: 11821 RVA: 0x000B1B50 File Offset: 0x000AFD50
		[__DynamicallyInvokable]
		public virtual int Compare(string string1, int offset1, string string2, int offset2)
		{
			return this.Compare(string1, offset1, string2, offset2, CompareOptions.None);
		}

		/// <summary>Compares a section of one string with a section of another string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="string1">The first string to compare.</param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing.</param>
		/// <param name="length1">The number of consecutive characters in <paramref name="string1" /> to compare.</param>
		/// <param name="string2">The second string to compare.</param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing.</param>
		/// <param name="length2">The number of consecutive characters in <paramref name="string2" /> to compare.</param>
		/// <param name="options">A value that defines how <paramref name="string1" /> and <paramref name="string2" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.  
		///   Value  
		///
		///   Condition  
		///
		///   zero  
		///
		///   The two strings are equal.  
		///
		///   less than zero  
		///
		///   The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />.  
		///
		///   greater than zero  
		///
		///   The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="length1" /> or <paramref name="offset2" /> or <paramref name="length2" /> is less than zero.  
		/// -or-  
		/// <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.  
		/// -or-  
		/// <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />.  
		/// -or-  
		/// <paramref name="length1" /> is greater than the number of characters from <paramref name="offset1" /> to the end of <paramref name="string1" />.  
		/// -or-  
		/// <paramref name="length2" /> is greater than the number of characters from <paramref name="offset2" /> to the end of <paramref name="string2" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E2E RID: 11822 RVA: 0x000B1B60 File Offset: 0x000AFD60
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				int num = string.Compare(string1, offset1, string2, offset2, (length1 < length2) ? length1 : length2, StringComparison.OrdinalIgnoreCase);
				if (length1 == length2 || num != 0)
				{
					return num;
				}
				if (length1 <= length2)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				if (length1 < 0 || length2 < 0)
				{
					throw new ArgumentOutOfRangeException((length1 < 0) ? "length1" : "length2", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
				}
				if (offset1 < 0 || offset2 < 0)
				{
					throw new ArgumentOutOfRangeException((offset1 < 0) ? "offset1" : "offset2", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
				}
				if (offset1 > ((string1 == null) ? 0 : string1.Length) - length1)
				{
					throw new ArgumentOutOfRangeException("string1", Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
				}
				if (offset2 > ((string2 == null) ? 0 : string2.Length) - length2)
				{
					throw new ArgumentOutOfRangeException("string2", Environment.GetResourceString("ArgumentOutOfRange_OffsetLength"));
				}
				if ((options & CompareOptions.Ordinal) != CompareOptions.None)
				{
					if (options != CompareOptions.Ordinal)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_CompareOptionOrdinal"), "options");
					}
				}
				else if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
				}
				if (string1 == null)
				{
					if (string2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (string2 == null)
					{
						return 1;
					}
					if (options == CompareOptions.Ordinal)
					{
						return CompareInfo.CompareOrdinal(string1, offset1, length1, string2, offset2, length2);
					}
					return CompareInfo.InternalCompareString(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, string1, offset1, length1, string2, offset2, length2, CompareInfo.GetNativeCompareFlags(options));
				}
			}
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000B1CDC File Offset: 0x000AFEDC
		[SecurityCritical]
		private static int CompareOrdinal(string string1, int offset1, int length1, string string2, int offset2, int length2)
		{
			int num = string.nativeCompareOrdinalEx(string1, offset1, string2, offset2, (length1 < length2) ? length1 : length2);
			if (length1 == length2 || num != 0)
			{
				return num;
			}
			if (length1 <= length2)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Determines whether the specified source string starts with the specified prefix using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search in.</param>
		/// <param name="prefix">The string to compare with the beginning of <paramref name="source" />.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="prefix" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>
		///   <see langword="true" /> if the length of <paramref name="prefix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> starts with <paramref name="prefix" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="prefix" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E30 RID: 11824 RVA: 0x000B1D10 File Offset: 0x000AFF10
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual bool IsPrefix(string source, string prefix, CompareOptions options)
		{
			if (source == null || prefix == null)
			{
				throw new ArgumentNullException((source == null) ? "source" : "prefix", Environment.GetResourceString("ArgumentNull_String"));
			}
			if (prefix.Length == 0)
			{
				return true;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.StartsWith(prefix, StringComparison.Ordinal);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
			}
			return CompareInfo.InternalFindNLSStringEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, CompareInfo.GetNativeCompareFlags(options) | 1048576 | ((source.IsAscii() && prefix.IsAscii()) ? 536870912 : 0), source, source.Length, 0, prefix, prefix.Length) > -1;
		}

		/// <summary>Determines whether the specified source string starts with the specified prefix.</summary>
		/// <param name="source">The string to search in.</param>
		/// <param name="prefix">The string to compare with the beginning of <paramref name="source" />.</param>
		/// <returns>
		///   <see langword="true" /> if the length of <paramref name="prefix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> starts with <paramref name="prefix" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="prefix" /> is <see langword="null" />.</exception>
		// Token: 0x06002E31 RID: 11825 RVA: 0x000B1DD9 File Offset: 0x000AFFD9
		[__DynamicallyInvokable]
		public virtual bool IsPrefix(string source, string prefix)
		{
			return this.IsPrefix(source, prefix, CompareOptions.None);
		}

		/// <summary>Determines whether the specified source string ends with the specified suffix using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search in.</param>
		/// <param name="suffix">The string to compare with the end of <paramref name="source" />.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="suffix" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>
		///   <see langword="true" /> if the length of <paramref name="suffix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> ends with <paramref name="suffix" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="suffix" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E32 RID: 11826 RVA: 0x000B1DE4 File Offset: 0x000AFFE4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual bool IsSuffix(string source, string suffix, CompareOptions options)
		{
			if (source == null || suffix == null)
			{
				throw new ArgumentNullException((source == null) ? "source" : "suffix", Environment.GetResourceString("ArgumentNull_String"));
			}
			if (suffix.Length == 0)
			{
				return true;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.EndsWith(suffix, StringComparison.Ordinal);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
			}
			return CompareInfo.InternalFindNLSStringEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, CompareInfo.GetNativeCompareFlags(options) | 2097152 | ((source.IsAscii() && suffix.IsAscii()) ? 536870912 : 0), source, source.Length, source.Length - 1, suffix, suffix.Length) >= 0;
		}

		/// <summary>Determines whether the specified source string ends with the specified suffix.</summary>
		/// <param name="source">The string to search in.</param>
		/// <param name="suffix">The string to compare with the end of <paramref name="source" />.</param>
		/// <returns>
		///   <see langword="true" /> if the length of <paramref name="suffix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> ends with <paramref name="suffix" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="suffix" /> is <see langword="null" />.</exception>
		// Token: 0x06002E33 RID: 11827 RVA: 0x000B1EB7 File Offset: 0x000B00B7
		[__DynamicallyInvokable]
		public virtual bool IsSuffix(string source, string suffix)
		{
			return this.IsSuffix(source, suffix, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the entire source string.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within <paramref name="source" />; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06002E34 RID: 11828 RVA: 0x000B1EC2 File Offset: 0x000B00C2
		[__DynamicallyInvokable]
		public virtual int IndexOf(string source, char value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the entire source string.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within <paramref name="source" />; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06002E35 RID: 11829 RVA: 0x000B1EE2 File Offset: 0x000B00E2
		[__DynamicallyInvokable]
		public virtual int IndexOf(string source, string value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="options">A value that defines how the strings should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E36 RID: 11830 RVA: 0x000B1F02 File Offset: 0x000B0102
		[__DynamicallyInvokable]
		public virtual int IndexOf(string source, char value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E37 RID: 11831 RVA: 0x000B1F22 File Offset: 0x000B0122
		[__DynamicallyInvokable]
		public virtual int IndexOf(string source, string value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		// Token: 0x06002E38 RID: 11832 RVA: 0x000B1F42 File Offset: 0x000B0142
		public virtual int IndexOf(string source, char value, int startIndex)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		// Token: 0x06002E39 RID: 11833 RVA: 0x000B1F64 File Offset: 0x000B0164
		public virtual int IndexOf(string source, string value, int startIndex)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E3A RID: 11834 RVA: 0x000B1F86 File Offset: 0x000B0186
		[__DynamicallyInvokable]
		public virtual int IndexOf(string source, char value, int startIndex, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E3B RID: 11835 RVA: 0x000B1FA9 File Offset: 0x000B01A9
		[__DynamicallyInvokable]
		public virtual int IndexOf(string source, string value, int startIndex, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		// Token: 0x06002E3C RID: 11836 RVA: 0x000B1FCC File Offset: 0x000B01CC
		[__DynamicallyInvokable]
		public virtual int IndexOf(string source, char value, int startIndex, int count)
		{
			return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		// Token: 0x06002E3D RID: 11837 RVA: 0x000B1FDA File Offset: 0x000B01DA
		[__DynamicallyInvokable]
		public virtual int IndexOf(string source, string value, int startIndex, int count)
		{
			return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E3E RID: 11838 RVA: 0x000B1FE8 File Offset: 0x000B01E8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual int IndexOf(string source, char value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (startIndex < 0 || startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0 || startIndex > source.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.IndexOf(value.ToString(), startIndex, count, StringComparison.OrdinalIgnoreCase);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
			}
			return CompareInfo.InternalFindNLSStringEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, CompareInfo.GetNativeCompareFlags(options) | 4194304 | ((source.IsAscii() && value <= '\u007f') ? 536870912 : 0), source, count, startIndex, new string(value, 1), 1);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E3F RID: 11839 RVA: 0x000B20D4 File Offset: 0x000B02D4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual int IndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (source.Length == 0)
			{
				if (value.Length == 0)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (count < 0 || startIndex > source.Length - count)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return source.IndexOf(value, startIndex, count, StringComparison.OrdinalIgnoreCase);
				}
				if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
				}
				return CompareInfo.InternalFindNLSStringEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, CompareInfo.GetNativeCompareFlags(options) | 4194304 | ((source.IsAscii() && value.IsAscii()) ? 536870912 : 0), source, count, startIndex, value, value.Length);
			}
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the entire source string.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within <paramref name="source" />; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06002E40 RID: 11840 RVA: 0x000B21F0 File Offset: 0x000B03F0
		[__DynamicallyInvokable]
		public virtual int LastIndexOf(string source, char value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the entire source string.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within <paramref name="source" />; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06002E41 RID: 11841 RVA: 0x000B2217 File Offset: 0x000B0417
		[__DynamicallyInvokable]
		public virtual int LastIndexOf(string source, string value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within <paramref name="source" />, using the specified comparison options; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E42 RID: 11842 RVA: 0x000B223E File Offset: 0x000B043E
		[__DynamicallyInvokable]
		public virtual int LastIndexOf(string source, char value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within <paramref name="source" />, using the specified comparison options; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E43 RID: 11843 RVA: 0x000B2265 File Offset: 0x000B0465
		[__DynamicallyInvokable]
		public virtual int LastIndexOf(string source, string value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		// Token: 0x06002E44 RID: 11844 RVA: 0x000B228C File Offset: 0x000B048C
		public virtual int LastIndexOf(string source, char value, int startIndex)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		// Token: 0x06002E45 RID: 11845 RVA: 0x000B229B File Offset: 0x000B049B
		public virtual int LastIndexOf(string source, string value, int startIndex)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E46 RID: 11846 RVA: 0x000B22AA File Offset: 0x000B04AA
		[__DynamicallyInvokable]
		public virtual int LastIndexOf(string source, char value, int startIndex, CompareOptions options)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E47 RID: 11847 RVA: 0x000B22BA File Offset: 0x000B04BA
		[__DynamicallyInvokable]
		public virtual int LastIndexOf(string source, string value, int startIndex, CompareOptions options)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and that ends at <paramref name="startIndex" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		// Token: 0x06002E48 RID: 11848 RVA: 0x000B22CA File Offset: 0x000B04CA
		[__DynamicallyInvokable]
		public virtual int LastIndexOf(string source, char value, int startIndex, int count)
		{
			return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and that ends at <paramref name="startIndex" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		// Token: 0x06002E49 RID: 11849 RVA: 0x000B22D8 File Offset: 0x000B04D8
		[__DynamicallyInvokable]
		public virtual int LastIndexOf(string source, string value, int startIndex, int count)
		{
			return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and that ends at <paramref name="startIndex" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E4A RID: 11850 RVA: 0x000B22E8 File Offset: 0x000B04E8
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual int LastIndexOf(string source, char value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal && options != CompareOptions.OrdinalIgnoreCase)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
			}
			if (source.Length == 0 && (startIndex == -1 || startIndex == 0))
			{
				return -1;
			}
			if (startIndex < 0 || startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (startIndex == source.Length)
			{
				startIndex--;
				if (count > 0)
				{
					count--;
				}
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.LastIndexOf(value.ToString(), startIndex, count, StringComparison.OrdinalIgnoreCase);
			}
			return CompareInfo.InternalFindNLSStringEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, CompareInfo.GetNativeCompareFlags(options) | 8388608 | ((source.IsAscii() && value <= '\u007f') ? 536870912 : 0), source, count, startIndex, new string(value, 1), 1);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and that ends at <paramref name="startIndex" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E4B RID: 11851 RVA: 0x000B2404 File Offset: 0x000B0604
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual int LastIndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal && options != CompareOptions.OrdinalIgnoreCase)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
			}
			if (source.Length == 0 && (startIndex == -1 || startIndex == 0))
			{
				if (value.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (startIndex < 0 || startIndex > source.Length)
				{
					throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
				}
				if (startIndex == source.Length)
				{
					startIndex--;
					if (count > 0)
					{
						count--;
					}
					if (value.Length == 0 && count >= 0 && startIndex - count + 1 >= 0)
					{
						return startIndex;
					}
				}
				if (count < 0 || startIndex - count + 1 < 0)
				{
					throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return source.LastIndexOf(value, startIndex, count, StringComparison.OrdinalIgnoreCase);
				}
				return CompareInfo.InternalFindNLSStringEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, CompareInfo.GetNativeCompareFlags(options) | 8388608 | ((source.IsAscii() && value.IsAscii()) ? 536870912 : 0), source, count, startIndex, value, value.Length);
			}
		}

		/// <summary>Gets a <see cref="T:System.Globalization.SortKey" /> object for the specified string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string for which a <see cref="T:System.Globalization.SortKey" /> object is obtained.</param>
		/// <param name="options">A bitwise combination of one or more of the following enumeration values that define how the sort key is calculated: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <returns>The <see cref="T:System.Globalization.SortKey" /> object that contains the sort key for the specified string.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06002E4C RID: 11852 RVA: 0x000B2549 File Offset: 0x000B0749
		public virtual SortKey GetSortKey(string source, CompareOptions options)
		{
			return this.CreateSortKey(source, options);
		}

		/// <summary>Gets the sort key for the specified string.</summary>
		/// <param name="source">The string for which a <see cref="T:System.Globalization.SortKey" /> object is obtained.</param>
		/// <returns>The <see cref="T:System.Globalization.SortKey" /> object that contains the sort key for the specified string.</returns>
		// Token: 0x06002E4D RID: 11853 RVA: 0x000B2553 File Offset: 0x000B0753
		public virtual SortKey GetSortKey(string source)
		{
			return this.CreateSortKey(source, CompareOptions.None);
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000B2560 File Offset: 0x000B0760
		[SecuritySafeCritical]
		private SortKey CreateSortKey(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
			}
			byte[] array = null;
			if (string.IsNullOrEmpty(source))
			{
				array = EmptyArray<byte>.Value;
				source = "\0";
			}
			int nativeCompareFlags = CompareInfo.GetNativeCompareFlags(options);
			int num = CompareInfo.InternalGetSortKey(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, nativeCompareFlags, source, source.Length, null, 0);
			if (num == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "source");
			}
			if (array == null)
			{
				array = new byte[num];
				num = CompareInfo.InternalGetSortKey(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, nativeCompareFlags, source, source.Length, array, array.Length);
			}
			else
			{
				source = string.Empty;
			}
			return new SortKey(this.Name, source, options, array);
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.CompareInfo" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equal to the current <see cref="T:System.Globalization.CompareInfo" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E4F RID: 11855 RVA: 0x000B2638 File Offset: 0x000B0838
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			CompareInfo compareInfo = value as CompareInfo;
			return compareInfo != null && this.Name == compareInfo.Name;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.CompareInfo" /> for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.CompareInfo" />.</returns>
		// Token: 0x06002E50 RID: 11856 RVA: 0x000B2662 File Offset: 0x000B0862
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>Gets the hash code for a string based on specified comparison options.</summary>
		/// <param name="source">The string whose hash code is to be returned.</param>
		/// <param name="options">A value that determines how strings are compared.</param>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06002E51 RID: 11857 RVA: 0x000B266F File Offset: 0x000B086F
		[__DynamicallyInvokable]
		public virtual int GetHashCode(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.GetHashCode();
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return TextInfo.GetHashCodeOrdinalIgnoreCase(source);
			}
			return this.GetHashCodeOfString(source, options, false, 0L);
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000B26A8 File Offset: 0x000B08A8
		internal int GetHashCodeOfString(string source, CompareOptions options)
		{
			return this.GetHashCodeOfString(source, options, false, 0L);
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x000B26B8 File Offset: 0x000B08B8
		[SecuritySafeCritical]
		internal int GetHashCodeOfString(string source, CompareOptions options, bool forceRandomizedHashing, long additionalEntropy)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "options");
			}
			if (source.Length == 0)
			{
				return 0;
			}
			return CompareInfo.InternalGetGlobalizedHashCode(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, source, source.Length, CompareInfo.GetNativeCompareFlags(options), forceRandomizedHashing, additionalEntropy);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Globalization.CompareInfo" /> object.</returns>
		// Token: 0x06002E54 RID: 11860 RVA: 0x000B271F File Offset: 0x000B091F
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return "CompareInfo - " + this.Name;
		}

		/// <summary>Gets the properly formed culture identifier for the current <see cref="T:System.Globalization.CompareInfo" />.</summary>
		/// <returns>The properly formed culture identifier for the current <see cref="T:System.Globalization.CompareInfo" />.</returns>
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x000B2731 File Offset: 0x000B0931
		public int LCID
		{
			get
			{
				return CultureInfo.GetCultureInfo(this.Name).LCID;
			}
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x000B2743 File Offset: 0x000B0943
		[SecuritySafeCritical]
		internal static IntPtr InternalInitSortHandle(string localeName, out IntPtr handleOrigin)
		{
			return CompareInfo.NativeInternalInitSortHandle(localeName, out handleOrigin);
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x000B274C File Offset: 0x000B094C
		internal static bool IsLegacy20SortingBehaviorRequested
		{
			get
			{
				return CompareInfo.InternalSortVersion == 4096U;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000B275A File Offset: 0x000B095A
		private static uint InternalSortVersion
		{
			[SecuritySafeCritical]
			get
			{
				return CompareInfo.InternalGetSortVersion();
			}
		}

		/// <summary>Gets information about the version of Unicode used for comparing and sorting strings.</summary>
		/// <returns>An object that contains information about the Unicode version used for comparing and sorting strings.</returns>
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x000B2764 File Offset: 0x000B0964
		public SortVersion Version
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_SortVersion == null)
				{
					Win32Native.NlsVersionInfoEx nlsVersionInfoEx = default(Win32Native.NlsVersionInfoEx);
					nlsVersionInfoEx.dwNLSVersionInfoSize = Marshal.SizeOf(typeof(Win32Native.NlsVersionInfoEx));
					CompareInfo.InternalGetNlsVersionEx(this.m_dataHandle, this.m_handleOrigin, this.m_sortName, ref nlsVersionInfoEx);
					this.m_SortVersion = new SortVersion(nlsVersionInfoEx.dwNLSVersion, (nlsVersionInfoEx.dwEffectiveId != 0) ? nlsVersionInfoEx.dwEffectiveId : this.LCID, nlsVersionInfoEx.guidCustomVersion);
				}
				return this.m_SortVersion;
			}
		}

		// Token: 0x06002E5A RID: 11866
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool InternalGetNlsVersionEx(IntPtr handle, IntPtr handleOrigin, string localeName, ref Win32Native.NlsVersionInfoEx lpNlsVersionInformation);

		// Token: 0x06002E5B RID: 11867
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern uint InternalGetSortVersion();

		// Token: 0x06002E5C RID: 11868
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr NativeInternalInitSortHandle(string localeName, out IntPtr handleOrigin);

		// Token: 0x06002E5D RID: 11869
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int InternalGetGlobalizedHashCode(IntPtr handle, IntPtr handleOrigin, string localeName, string source, int length, int dwFlags, bool forceRandomizedHashing, long additionalEntropy);

		// Token: 0x06002E5E RID: 11870
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool InternalIsSortable(IntPtr handle, IntPtr handleOrigin, string localeName, string source, int length);

		// Token: 0x06002E5F RID: 11871
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int InternalCompareString(IntPtr handle, IntPtr handleOrigin, string localeName, string string1, int offset1, int length1, string string2, int offset2, int length2, int flags);

		// Token: 0x06002E60 RID: 11872
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int InternalFindNLSStringEx(IntPtr handle, IntPtr handleOrigin, string localeName, int flags, string source, int sourceCount, int startIndex, string target, int targetCount);

		// Token: 0x06002E61 RID: 11873
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int InternalGetSortKey(IntPtr handle, IntPtr handleOrigin, string localeName, int flags, string source, int sourceCount, byte[] target, int targetCount);

		// Token: 0x0400131F RID: 4895
		private const CompareOptions ValidIndexMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);

		// Token: 0x04001320 RID: 4896
		private const CompareOptions ValidCompareMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort);

		// Token: 0x04001321 RID: 4897
		private const CompareOptions ValidHashCodeOfStringMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);

		// Token: 0x04001322 RID: 4898
		[OptionalField(VersionAdded = 2)]
		private string m_name;

		// Token: 0x04001323 RID: 4899
		[NonSerialized]
		private string m_sortName;

		// Token: 0x04001324 RID: 4900
		[NonSerialized]
		private IntPtr m_dataHandle;

		// Token: 0x04001325 RID: 4901
		[NonSerialized]
		private IntPtr m_handleOrigin;

		// Token: 0x04001326 RID: 4902
		[OptionalField(VersionAdded = 1)]
		private int win32LCID;

		// Token: 0x04001327 RID: 4903
		private int culture;

		// Token: 0x04001328 RID: 4904
		private const int LINGUISTIC_IGNORECASE = 16;

		// Token: 0x04001329 RID: 4905
		private const int NORM_IGNORECASE = 1;

		// Token: 0x0400132A RID: 4906
		private const int NORM_IGNOREKANATYPE = 65536;

		// Token: 0x0400132B RID: 4907
		private const int LINGUISTIC_IGNOREDIACRITIC = 32;

		// Token: 0x0400132C RID: 4908
		private const int NORM_IGNORENONSPACE = 2;

		// Token: 0x0400132D RID: 4909
		private const int NORM_IGNORESYMBOLS = 4;

		// Token: 0x0400132E RID: 4910
		private const int NORM_IGNOREWIDTH = 131072;

		// Token: 0x0400132F RID: 4911
		private const int SORT_STRINGSORT = 4096;

		// Token: 0x04001330 RID: 4912
		private const int COMPARE_OPTIONS_ORDINAL = 1073741824;

		// Token: 0x04001331 RID: 4913
		internal const int NORM_LINGUISTIC_CASING = 134217728;

		// Token: 0x04001332 RID: 4914
		private const int RESERVED_FIND_ASCII_STRING = 536870912;

		// Token: 0x04001333 RID: 4915
		private const int SORT_VERSION_WHIDBEY = 4096;

		// Token: 0x04001334 RID: 4916
		private const int SORT_VERSION_V4 = 393473;

		// Token: 0x04001335 RID: 4917
		[OptionalField(VersionAdded = 3)]
		private SortVersion m_SortVersion;
	}
}
