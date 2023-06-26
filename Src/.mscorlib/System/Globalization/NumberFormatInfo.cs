using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Globalization
{
	/// <summary>Provides culture-specific information for formatting and parsing numeric values.</summary>
	// Token: 0x020003D7 RID: 983
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class NumberFormatInfo : ICloneable, IFormatProvider
	{
		/// <summary>Initializes a new writable instance of the <see cref="T:System.Globalization.NumberFormatInfo" /> class that is culture-independent (invariant).</summary>
		// Token: 0x06003222 RID: 12834 RVA: 0x000C2424 File Offset: 0x000C0624
		[__DynamicallyInvokable]
		public NumberFormatInfo()
			: this(null)
		{
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000C2430 File Offset: 0x000C0630
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if (this.numberDecimalSeparator != this.numberGroupSeparator)
			{
				this.validForParseAsNumber = true;
			}
			else
			{
				this.validForParseAsNumber = false;
			}
			if (this.numberDecimalSeparator != this.numberGroupSeparator && this.numberDecimalSeparator != this.currencyGroupSeparator && this.currencyDecimalSeparator != this.numberGroupSeparator && this.currencyDecimalSeparator != this.currencyGroupSeparator)
			{
				this.validForParseAsCurrency = true;
				return;
			}
			this.validForParseAsCurrency = false;
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x000C24BB File Offset: 0x000C06BB
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000C24BD File Offset: 0x000C06BD
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x000C24BF File Offset: 0x000C06BF
		private static void VerifyDecimalSeparator(string decSep, string propertyName)
		{
			if (decSep == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_String"));
			}
			if (decSep.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyDecString"));
			}
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x000C24ED File Offset: 0x000C06ED
		private static void VerifyGroupSeparator(string groupSep, string propertyName)
		{
			if (groupSep == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_String"));
			}
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000C2504 File Offset: 0x000C0704
		private static void VerifyNativeDigits(string[] nativeDig, string propertyName)
		{
			if (nativeDig == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (nativeDig.Length != 10)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitCount"), propertyName);
			}
			for (int i = 0; i < nativeDig.Length; i++)
			{
				if (nativeDig[i] == null)
				{
					throw new ArgumentNullException(propertyName, Environment.GetResourceString("ArgumentNull_ArrayValue"));
				}
				if (nativeDig[i].Length != 1)
				{
					if (nativeDig[i].Length != 2)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitValue"), propertyName);
					}
					if (!char.IsSurrogatePair(nativeDig[i][0], nativeDig[i][1]))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitValue"), propertyName);
					}
				}
				if (CharUnicodeInfo.GetDecimalDigitValue(nativeDig[i], 0) != i && CharUnicodeInfo.GetUnicodeCategory(nativeDig[i], 0) != UnicodeCategory.PrivateUse)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNativeDigitValue"), propertyName);
				}
			}
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000C25E2 File Offset: 0x000C07E2
		private static void VerifyDigitSubstitution(DigitShapes digitSub, string propertyName)
		{
			if (digitSub > DigitShapes.NativeNational)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDigitSubstitution"), propertyName);
			}
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000C25FC File Offset: 0x000C07FC
		[SecuritySafeCritical]
		internal NumberFormatInfo(CultureData cultureData)
		{
			if (cultureData != null)
			{
				cultureData.GetNFIValues(this);
				if (cultureData.IsInvariantCulture)
				{
					this.m_isInvariant = true;
				}
			}
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000C2781 File Offset: 0x000C0981
		private void VerifyWritable()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
			}
		}

		/// <summary>Gets a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> object that is culture-independent (invariant).</summary>
		/// <returns>A read-only  object that is culture-independent (invariant).</returns>
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600322C RID: 12844 RVA: 0x000C279C File Offset: 0x000C099C
		[__DynamicallyInvokable]
		public static NumberFormatInfo InvariantInfo
		{
			[__DynamicallyInvokable]
			get
			{
				if (NumberFormatInfo.invariantInfo == null)
				{
					NumberFormatInfo.invariantInfo = NumberFormatInfo.ReadOnly(new NumberFormatInfo
					{
						m_isInvariant = true
					});
				}
				return NumberFormatInfo.invariantInfo;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.NumberFormatInfo" /> associated with the specified <see cref="T:System.IFormatProvider" />.</summary>
		/// <param name="formatProvider">The <see cref="T:System.IFormatProvider" /> used to get the <see cref="T:System.Globalization.NumberFormatInfo" />.  
		///  -or-  
		///  <see langword="null" /> to get <see cref="P:System.Globalization.NumberFormatInfo.CurrentInfo" />.</param>
		/// <returns>The <see cref="T:System.Globalization.NumberFormatInfo" /> associated with the specified <see cref="T:System.IFormatProvider" />.</returns>
		// Token: 0x0600322D RID: 12845 RVA: 0x000C27D4 File Offset: 0x000C09D4
		[__DynamicallyInvokable]
		public static NumberFormatInfo GetInstance(IFormatProvider formatProvider)
		{
			CultureInfo cultureInfo = formatProvider as CultureInfo;
			if (cultureInfo != null && !cultureInfo.m_isInherited)
			{
				NumberFormatInfo numberFormatInfo = cultureInfo.numInfo;
				if (numberFormatInfo != null)
				{
					return numberFormatInfo;
				}
				return cultureInfo.NumberFormat;
			}
			else
			{
				NumberFormatInfo numberFormatInfo = formatProvider as NumberFormatInfo;
				if (numberFormatInfo != null)
				{
					return numberFormatInfo;
				}
				if (formatProvider != null)
				{
					numberFormatInfo = formatProvider.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo;
					if (numberFormatInfo != null)
					{
						return numberFormatInfo;
					}
				}
				return NumberFormatInfo.CurrentInfo;
			}
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Globalization.NumberFormatInfo" /> object.</summary>
		/// <returns>A new object copied from the original <see cref="T:System.Globalization.NumberFormatInfo" /> object.</returns>
		// Token: 0x0600322E RID: 12846 RVA: 0x000C2838 File Offset: 0x000C0A38
		[__DynamicallyInvokable]
		public object Clone()
		{
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)base.MemberwiseClone();
			numberFormatInfo.isReadOnly = false;
			return numberFormatInfo;
		}

		/// <summary>Gets or sets the number of decimal places to use in currency values.</summary>
		/// <returns>The number of decimal places to use in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x000C2859 File Offset: 0x000C0A59
		// (set) Token: 0x06003230 RID: 12848 RVA: 0x000C2864 File Offset: 0x000C0A64
		[__DynamicallyInvokable]
		public int CurrencyDecimalDigits
		{
			[__DynamicallyInvokable]
			get
			{
				return this.currencyDecimalDigits;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("CurrencyDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 99));
				}
				this.VerifyWritable();
				this.currencyDecimalDigits = value;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in currency values.</summary>
		/// <returns>The string to use as the decimal separator in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x000C28B3 File Offset: 0x000C0AB3
		// (set) Token: 0x06003232 RID: 12850 RVA: 0x000C28BB File Offset: 0x000C0ABB
		[__DynamicallyInvokable]
		public string CurrencyDecimalSeparator
		{
			[__DynamicallyInvokable]
			get
			{
				return this.currencyDecimalSeparator;
			}
			[__DynamicallyInvokable]
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "CurrencyDecimalSeparator");
				this.currencyDecimalSeparator = value;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000C28D5 File Offset: 0x000C0AD5
		[__DynamicallyInvokable]
		public bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000C28E0 File Offset: 0x000C0AE0
		internal static void CheckGroupSize(string propName, int[] groupSize)
		{
			int i = 0;
			while (i < groupSize.Length)
			{
				if (groupSize[i] < 1)
				{
					if (i == groupSize.Length - 1 && groupSize[i] == 0)
					{
						return;
					}
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGroupSize"), propName);
				}
				else
				{
					if (groupSize[i] > 9)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGroupSize"), propName);
					}
					i++;
				}
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in currency values.</summary>
		/// <returns>The number of digits in each group to the left of the decimal in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.  
		///  -or-  
		///  The property is being set and the array contains an entry, other than the last entry, that is set to 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x000C2938 File Offset: 0x000C0B38
		// (set) Token: 0x06003236 RID: 12854 RVA: 0x000C294C File Offset: 0x000C0B4C
		[__DynamicallyInvokable]
		public int[] CurrencyGroupSizes
		{
			[__DynamicallyInvokable]
			get
			{
				return (int[])this.currencyGroupSizes.Clone();
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CurrencyGroupSizes", Environment.GetResourceString("ArgumentNull_Obj"));
				}
				this.VerifyWritable();
				int[] array = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("CurrencyGroupSizes", array);
				this.currencyGroupSizes = array;
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in numeric values.</summary>
		/// <returns>The number of digits in each group to the left of the decimal in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.  
		///  -or-  
		///  The property is being set and the array contains an entry, other than the last entry, that is set to 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x000C2995 File Offset: 0x000C0B95
		// (set) Token: 0x06003238 RID: 12856 RVA: 0x000C29A8 File Offset: 0x000C0BA8
		[__DynamicallyInvokable]
		public int[] NumberGroupSizes
		{
			[__DynamicallyInvokable]
			get
			{
				return (int[])this.numberGroupSizes.Clone();
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NumberGroupSizes", Environment.GetResourceString("ArgumentNull_Obj"));
				}
				this.VerifyWritable();
				int[] array = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("NumberGroupSizes", array);
				this.numberGroupSizes = array;
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in percent values.</summary>
		/// <returns>The number of digits in each group to the left of the decimal in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.  
		///  -or-  
		///  The property is being set and the array contains an entry, other than the last entry, that is set to 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06003239 RID: 12857 RVA: 0x000C29F1 File Offset: 0x000C0BF1
		// (set) Token: 0x0600323A RID: 12858 RVA: 0x000C2A04 File Offset: 0x000C0C04
		[__DynamicallyInvokable]
		public int[] PercentGroupSizes
		{
			[__DynamicallyInvokable]
			get
			{
				return (int[])this.percentGroupSizes.Clone();
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PercentGroupSizes", Environment.GetResourceString("ArgumentNull_Obj"));
				}
				this.VerifyWritable();
				int[] array = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("PercentGroupSizes", array);
				this.percentGroupSizes = array;
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in currency values.</summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x000C2A4D File Offset: 0x000C0C4D
		// (set) Token: 0x0600323C RID: 12860 RVA: 0x000C2A55 File Offset: 0x000C0C55
		[__DynamicallyInvokable]
		public string CurrencyGroupSeparator
		{
			[__DynamicallyInvokable]
			get
			{
				return this.currencyGroupSeparator;
			}
			[__DynamicallyInvokable]
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "CurrencyGroupSeparator");
				this.currencyGroupSeparator = value;
			}
		}

		/// <summary>Gets or sets the string to use as the currency symbol.</summary>
		/// <returns>The string to use as the currency symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "¤".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x000C2A6F File Offset: 0x000C0C6F
		// (set) Token: 0x0600323E RID: 12862 RVA: 0x000C2A77 File Offset: 0x000C0C77
		[__DynamicallyInvokable]
		public string CurrencySymbol
		{
			[__DynamicallyInvokable]
			get
			{
				return this.currencySymbol;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CurrencySymbol", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.currencySymbol = value;
			}
		}

		/// <summary>Gets a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> that formats values based on the current culture.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.NumberFormatInfo" /> based on the culture of the current thread.</returns>
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x000C2AA0 File Offset: 0x000C0CA0
		[__DynamicallyInvokable]
		public static NumberFormatInfo CurrentInfo
		{
			[__DynamicallyInvokable]
			get
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				if (!currentCulture.m_isInherited)
				{
					NumberFormatInfo numInfo = currentCulture.numInfo;
					if (numInfo != null)
					{
						return numInfo;
					}
				}
				return (NumberFormatInfo)currentCulture.GetFormat(typeof(NumberFormatInfo));
			}
		}

		/// <summary>Gets or sets the string that represents the IEEE NaN (not a number) value.</summary>
		/// <returns>The string that represents the IEEE NaN (not a number) value. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "NaN".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x000C2AE1 File Offset: 0x000C0CE1
		// (set) Token: 0x06003241 RID: 12865 RVA: 0x000C2AE9 File Offset: 0x000C0CE9
		[__DynamicallyInvokable]
		public string NaNSymbol
		{
			[__DynamicallyInvokable]
			get
			{
				return this.nanSymbol;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NaNSymbol", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.nanSymbol = value;
			}
		}

		/// <summary>Gets or sets the format pattern for negative currency values.</summary>
		/// <returns>The format pattern for negative currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "($n)", where "$" is the <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 15.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06003242 RID: 12866 RVA: 0x000C2B10 File Offset: 0x000C0D10
		// (set) Token: 0x06003243 RID: 12867 RVA: 0x000C2B18 File Offset: 0x000C0D18
		[__DynamicallyInvokable]
		public int CurrencyNegativePattern
		{
			[__DynamicallyInvokable]
			get
			{
				return this.currencyNegativePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 15)
				{
					throw new ArgumentOutOfRangeException("CurrencyNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 15));
				}
				this.VerifyWritable();
				this.currencyNegativePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for negative numeric values.</summary>
		/// <returns>The format pattern for negative numeric values.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 4.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06003244 RID: 12868 RVA: 0x000C2B67 File Offset: 0x000C0D67
		// (set) Token: 0x06003245 RID: 12869 RVA: 0x000C2B70 File Offset: 0x000C0D70
		[__DynamicallyInvokable]
		public int NumberNegativePattern
		{
			[__DynamicallyInvokable]
			get
			{
				return this.numberNegativePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 4)
				{
					throw new ArgumentOutOfRangeException("NumberNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 4));
				}
				this.VerifyWritable();
				this.numberNegativePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for positive percent values.</summary>
		/// <returns>The format pattern for positive percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "n %", where "%" is the <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 3.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06003246 RID: 12870 RVA: 0x000C2BBD File Offset: 0x000C0DBD
		// (set) Token: 0x06003247 RID: 12871 RVA: 0x000C2BC8 File Offset: 0x000C0DC8
		[__DynamicallyInvokable]
		public int PercentPositivePattern
		{
			[__DynamicallyInvokable]
			get
			{
				return this.percentPositivePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("PercentPositivePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 3));
				}
				this.VerifyWritable();
				this.percentPositivePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for negative percent values.</summary>
		/// <returns>The format pattern for negative percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "-n %", where "%" is the <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 11.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06003248 RID: 12872 RVA: 0x000C2C15 File Offset: 0x000C0E15
		// (set) Token: 0x06003249 RID: 12873 RVA: 0x000C2C20 File Offset: 0x000C0E20
		[__DynamicallyInvokable]
		public int PercentNegativePattern
		{
			[__DynamicallyInvokable]
			get
			{
				return this.percentNegativePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 11)
				{
					throw new ArgumentOutOfRangeException("PercentNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 11));
				}
				this.VerifyWritable();
				this.percentNegativePattern = value;
			}
		}

		/// <summary>Gets or sets the string that represents negative infinity.</summary>
		/// <returns>The string that represents negative infinity. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "-Infinity".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600324A RID: 12874 RVA: 0x000C2C6F File Offset: 0x000C0E6F
		// (set) Token: 0x0600324B RID: 12875 RVA: 0x000C2C77 File Offset: 0x000C0E77
		[__DynamicallyInvokable]
		public string NegativeInfinitySymbol
		{
			[__DynamicallyInvokable]
			get
			{
				return this.negativeInfinitySymbol;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NegativeInfinitySymbol", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.negativeInfinitySymbol = value;
			}
		}

		/// <summary>Gets or sets the string that denotes that the associated number is negative.</summary>
		/// <returns>The string that denotes that the associated number is negative. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "-".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x000C2C9E File Offset: 0x000C0E9E
		// (set) Token: 0x0600324D RID: 12877 RVA: 0x000C2CA6 File Offset: 0x000C0EA6
		[__DynamicallyInvokable]
		public string NegativeSign
		{
			[__DynamicallyInvokable]
			get
			{
				return this.negativeSign;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NegativeSign", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.negativeSign = value;
			}
		}

		/// <summary>Gets or sets the number of decimal places to use in numeric values.</summary>
		/// <returns>The number of decimal places to use in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x0600324E RID: 12878 RVA: 0x000C2CCD File Offset: 0x000C0ECD
		// (set) Token: 0x0600324F RID: 12879 RVA: 0x000C2CD8 File Offset: 0x000C0ED8
		[__DynamicallyInvokable]
		public int NumberDecimalDigits
		{
			[__DynamicallyInvokable]
			get
			{
				return this.numberDecimalDigits;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("NumberDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 99));
				}
				this.VerifyWritable();
				this.numberDecimalDigits = value;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in numeric values.</summary>
		/// <returns>The string to use as the decimal separator in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06003250 RID: 12880 RVA: 0x000C2D27 File Offset: 0x000C0F27
		// (set) Token: 0x06003251 RID: 12881 RVA: 0x000C2D2F File Offset: 0x000C0F2F
		[__DynamicallyInvokable]
		public string NumberDecimalSeparator
		{
			[__DynamicallyInvokable]
			get
			{
				return this.numberDecimalSeparator;
			}
			[__DynamicallyInvokable]
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "NumberDecimalSeparator");
				this.numberDecimalSeparator = value;
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in numeric values.</summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x000C2D49 File Offset: 0x000C0F49
		// (set) Token: 0x06003253 RID: 12883 RVA: 0x000C2D51 File Offset: 0x000C0F51
		[__DynamicallyInvokable]
		public string NumberGroupSeparator
		{
			[__DynamicallyInvokable]
			get
			{
				return this.numberGroupSeparator;
			}
			[__DynamicallyInvokable]
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "NumberGroupSeparator");
				this.numberGroupSeparator = value;
			}
		}

		/// <summary>Gets or sets the format pattern for positive currency values.</summary>
		/// <returns>The format pattern for positive currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "$n", where "$" is the <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 3.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06003254 RID: 12884 RVA: 0x000C2D6B File Offset: 0x000C0F6B
		// (set) Token: 0x06003255 RID: 12885 RVA: 0x000C2D74 File Offset: 0x000C0F74
		[__DynamicallyInvokable]
		public int CurrencyPositivePattern
		{
			[__DynamicallyInvokable]
			get
			{
				return this.currencyPositivePattern;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("CurrencyPositivePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 3));
				}
				this.VerifyWritable();
				this.currencyPositivePattern = value;
			}
		}

		/// <summary>Gets or sets the string that represents positive infinity.</summary>
		/// <returns>The string that represents positive infinity. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "Infinity".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06003256 RID: 12886 RVA: 0x000C2DC1 File Offset: 0x000C0FC1
		// (set) Token: 0x06003257 RID: 12887 RVA: 0x000C2DC9 File Offset: 0x000C0FC9
		[__DynamicallyInvokable]
		public string PositiveInfinitySymbol
		{
			[__DynamicallyInvokable]
			get
			{
				return this.positiveInfinitySymbol;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PositiveInfinitySymbol", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.positiveInfinitySymbol = value;
			}
		}

		/// <summary>Gets or sets the string that denotes that the associated number is positive.</summary>
		/// <returns>The string that denotes that the associated number is positive. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "+".</returns>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the value to be assigned is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06003258 RID: 12888 RVA: 0x000C2DF0 File Offset: 0x000C0FF0
		// (set) Token: 0x06003259 RID: 12889 RVA: 0x000C2DF8 File Offset: 0x000C0FF8
		[__DynamicallyInvokable]
		public string PositiveSign
		{
			[__DynamicallyInvokable]
			get
			{
				return this.positiveSign;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PositiveSign", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.positiveSign = value;
			}
		}

		/// <summary>Gets or sets the number of decimal places to use in percent values.</summary>
		/// <returns>The number of decimal places to use in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x000C2E1F File Offset: 0x000C101F
		// (set) Token: 0x0600325B RID: 12891 RVA: 0x000C2E28 File Offset: 0x000C1028
		[__DynamicallyInvokable]
		public int PercentDecimalDigits
		{
			[__DynamicallyInvokable]
			get
			{
				return this.percentDecimalDigits;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("PercentDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 99));
				}
				this.VerifyWritable();
				this.percentDecimalDigits = value;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in percent values.</summary>
		/// <returns>The string to use as the decimal separator in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600325C RID: 12892 RVA: 0x000C2E77 File Offset: 0x000C1077
		// (set) Token: 0x0600325D RID: 12893 RVA: 0x000C2E7F File Offset: 0x000C107F
		[__DynamicallyInvokable]
		public string PercentDecimalSeparator
		{
			[__DynamicallyInvokable]
			get
			{
				return this.percentDecimalSeparator;
			}
			[__DynamicallyInvokable]
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "PercentDecimalSeparator");
				this.percentDecimalSeparator = value;
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in percent values.</summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x000C2E99 File Offset: 0x000C1099
		// (set) Token: 0x0600325F RID: 12895 RVA: 0x000C2EA1 File Offset: 0x000C10A1
		[__DynamicallyInvokable]
		public string PercentGroupSeparator
		{
			[__DynamicallyInvokable]
			get
			{
				return this.percentGroupSeparator;
			}
			[__DynamicallyInvokable]
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "PercentGroupSeparator");
				this.percentGroupSeparator = value;
			}
		}

		/// <summary>Gets or sets the string to use as the percent symbol.</summary>
		/// <returns>The string to use as the percent symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "%".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x000C2EBB File Offset: 0x000C10BB
		// (set) Token: 0x06003261 RID: 12897 RVA: 0x000C2EC3 File Offset: 0x000C10C3
		[__DynamicallyInvokable]
		public string PercentSymbol
		{
			[__DynamicallyInvokable]
			get
			{
				return this.percentSymbol;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PercentSymbol", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.percentSymbol = value;
			}
		}

		/// <summary>Gets or sets the string to use as the per mille symbol.</summary>
		/// <returns>The string to use as the per mille symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "‰", which is the Unicode character U+2030.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x000C2EEA File Offset: 0x000C10EA
		// (set) Token: 0x06003263 RID: 12899 RVA: 0x000C2EF2 File Offset: 0x000C10F2
		[__DynamicallyInvokable]
		public string PerMilleSymbol
		{
			[__DynamicallyInvokable]
			get
			{
				return this.perMilleSymbol;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PerMilleSymbol", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.perMilleSymbol = value;
			}
		}

		/// <summary>Gets or sets a string array of native digits equivalent to the Western digits 0 through 9.</summary>
		/// <returns>A string array that contains the native equivalent of the Western digits 0 through 9. The default is an array having the elements "0", "1", "2", "3", "4", "5", "6", "7", "8", and "9".</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the value is <see langword="null" />.  
		///  -or-  
		///  In a set operation, an element of the value array is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">In a set operation, the value array does not contain 10 elements.  
		///  -or-  
		///  In a set operation, an element of the value array does not contain either a single <see cref="T:System.Char" /> object or a pair of <see cref="T:System.Char" /> objects that comprise a surrogate pair.  
		///  -or-  
		///  In a set operation, an element of the value array is not a number digit as defined by the Unicode Standard. That is, the digit in the array element does not have the Unicode <see langword="Number, Decimal Digit" /> (Nd) General Category value.  
		///  -or-  
		///  In a set operation, the numeric value of an element in the value array does not correspond to the element's position in the array. That is, the element at index 0, which is the first element of the array, does not have a numeric value of 0, or the element at index 1 does not have a numeric value of 1.</exception>
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x000C2F19 File Offset: 0x000C1119
		// (set) Token: 0x06003265 RID: 12901 RVA: 0x000C2F2B File Offset: 0x000C112B
		[ComVisible(false)]
		public string[] NativeDigits
		{
			get
			{
				return (string[])this.nativeDigits.Clone();
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyNativeDigits(value, "NativeDigits");
				this.nativeDigits = value;
			}
		}

		/// <summary>Gets or sets a value that specifies how the graphical user interface displays the shape of a digit.</summary>
		/// <returns>One of the enumeration values that specifies the culture-specific digit shape.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">The value in a set operation is not a valid <see cref="T:System.Globalization.DigitShapes" /> value.</exception>
		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x000C2F45 File Offset: 0x000C1145
		// (set) Token: 0x06003267 RID: 12903 RVA: 0x000C2F4D File Offset: 0x000C114D
		[ComVisible(false)]
		public DigitShapes DigitSubstitution
		{
			get
			{
				return (DigitShapes)this.digitSubstitution;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDigitSubstitution(value, "DigitSubstitution");
				this.digitSubstitution = (int)value;
			}
		}

		/// <summary>Gets an object of the specified type that provides a number formatting service.</summary>
		/// <param name="formatType">The <see cref="T:System.Type" /> of the required formatting service.</param>
		/// <returns>The current <see cref="T:System.Globalization.NumberFormatInfo" />, if <paramref name="formatType" /> is the same as the type of the current <see cref="T:System.Globalization.NumberFormatInfo" />; otherwise, <see langword="null" />.</returns>
		// Token: 0x06003268 RID: 12904 RVA: 0x000C2F67 File Offset: 0x000C1167
		[__DynamicallyInvokable]
		public object GetFormat(Type formatType)
		{
			if (!(formatType == typeof(NumberFormatInfo)))
			{
				return null;
			}
			return this;
		}

		/// <summary>Returns a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> wrapper.</summary>
		/// <param name="nfi">The <see cref="T:System.Globalization.NumberFormatInfo" /> to wrap.</param>
		/// <returns>A read-only <see cref="T:System.Globalization.NumberFormatInfo" /> wrapper around <paramref name="nfi" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="nfi" /> is <see langword="null" />.</exception>
		// Token: 0x06003269 RID: 12905 RVA: 0x000C2F80 File Offset: 0x000C1180
		[__DynamicallyInvokable]
		public static NumberFormatInfo ReadOnly(NumberFormatInfo nfi)
		{
			if (nfi == null)
			{
				throw new ArgumentNullException("nfi");
			}
			if (nfi.IsReadOnly)
			{
				return nfi;
			}
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)nfi.MemberwiseClone();
			numberFormatInfo.isReadOnly = true;
			return numberFormatInfo;
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000C2FBC File Offset: 0x000C11BC
		internal static void ValidateParseStyleInteger(NumberStyles style)
		{
			if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNumberStyles"), "style");
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None && (style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidHexStyle"));
			}
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000C3009 File Offset: 0x000C1209
		internal static void ValidateParseStyleFloatingPoint(NumberStyles style)
		{
			if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNumberStyles"), "style");
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_HexStyleNotSupported"));
			}
		}

		// Token: 0x04001555 RID: 5461
		private static volatile NumberFormatInfo invariantInfo;

		// Token: 0x04001556 RID: 5462
		internal int[] numberGroupSizes = new int[] { 3 };

		// Token: 0x04001557 RID: 5463
		internal int[] currencyGroupSizes = new int[] { 3 };

		// Token: 0x04001558 RID: 5464
		internal int[] percentGroupSizes = new int[] { 3 };

		// Token: 0x04001559 RID: 5465
		internal string positiveSign = "+";

		// Token: 0x0400155A RID: 5466
		internal string negativeSign = "-";

		// Token: 0x0400155B RID: 5467
		internal string numberDecimalSeparator = ".";

		// Token: 0x0400155C RID: 5468
		internal string numberGroupSeparator = ",";

		// Token: 0x0400155D RID: 5469
		internal string currencyGroupSeparator = ",";

		// Token: 0x0400155E RID: 5470
		internal string currencyDecimalSeparator = ".";

		// Token: 0x0400155F RID: 5471
		internal string currencySymbol = "¤";

		// Token: 0x04001560 RID: 5472
		internal string ansiCurrencySymbol;

		// Token: 0x04001561 RID: 5473
		internal string nanSymbol = "NaN";

		// Token: 0x04001562 RID: 5474
		internal string positiveInfinitySymbol = "Infinity";

		// Token: 0x04001563 RID: 5475
		internal string negativeInfinitySymbol = "-Infinity";

		// Token: 0x04001564 RID: 5476
		internal string percentDecimalSeparator = ".";

		// Token: 0x04001565 RID: 5477
		internal string percentGroupSeparator = ",";

		// Token: 0x04001566 RID: 5478
		internal string percentSymbol = "%";

		// Token: 0x04001567 RID: 5479
		internal string perMilleSymbol = "‰";

		// Token: 0x04001568 RID: 5480
		[OptionalField(VersionAdded = 2)]
		internal string[] nativeDigits = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

		// Token: 0x04001569 RID: 5481
		[OptionalField(VersionAdded = 1)]
		internal int m_dataItem;

		// Token: 0x0400156A RID: 5482
		internal int numberDecimalDigits = 2;

		// Token: 0x0400156B RID: 5483
		internal int currencyDecimalDigits = 2;

		// Token: 0x0400156C RID: 5484
		internal int currencyPositivePattern;

		// Token: 0x0400156D RID: 5485
		internal int currencyNegativePattern;

		// Token: 0x0400156E RID: 5486
		internal int numberNegativePattern = 1;

		// Token: 0x0400156F RID: 5487
		internal int percentPositivePattern;

		// Token: 0x04001570 RID: 5488
		internal int percentNegativePattern;

		// Token: 0x04001571 RID: 5489
		internal int percentDecimalDigits = 2;

		// Token: 0x04001572 RID: 5490
		[OptionalField(VersionAdded = 2)]
		internal int digitSubstitution = 1;

		// Token: 0x04001573 RID: 5491
		internal bool isReadOnly;

		// Token: 0x04001574 RID: 5492
		[OptionalField(VersionAdded = 1)]
		internal bool m_useUserOverride;

		// Token: 0x04001575 RID: 5493
		[OptionalField(VersionAdded = 2)]
		internal bool m_isInvariant;

		// Token: 0x04001576 RID: 5494
		[OptionalField(VersionAdded = 1)]
		internal bool validForParseAsNumber = true;

		// Token: 0x04001577 RID: 5495
		[OptionalField(VersionAdded = 1)]
		internal bool validForParseAsCurrency = true;

		// Token: 0x04001578 RID: 5496
		private const NumberStyles InvalidNumberStyles = ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier);
	}
}
