using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Provides a <see cref="T:System.ComponentModel.TypeConverter" /> to convert <see cref="T:System.Windows.Forms.Keys" /> objects to and from other representations.</summary>
	// Token: 0x020002B6 RID: 694
	public class KeysConverter : TypeConverter, IComparer
	{
		// Token: 0x06002A90 RID: 10896 RVA: 0x000C013E File Offset: 0x000BE33E
		private void AddKey(string key, Keys value)
		{
			this.keyNames[key] = value;
			this.displayOrder.Add(key);
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x000C0160 File Offset: 0x000BE360
		private void Initialize()
		{
			this.keyNames = new Hashtable(34);
			this.displayOrder = new List<string>(34);
			this.AddKey(SR.GetString("toStringEnter"), Keys.Return);
			this.AddKey("F12", Keys.F12);
			this.AddKey("F11", Keys.F11);
			this.AddKey("F10", Keys.F10);
			this.AddKey(SR.GetString("toStringEnd"), Keys.End);
			this.AddKey(SR.GetString("toStringControl"), Keys.Control);
			this.AddKey("F8", Keys.F8);
			this.AddKey("F9", Keys.F9);
			this.AddKey(SR.GetString("toStringAlt"), Keys.Alt);
			this.AddKey("F4", Keys.F4);
			this.AddKey("F5", Keys.F5);
			this.AddKey("F6", Keys.F6);
			this.AddKey("F7", Keys.F7);
			this.AddKey("F1", Keys.F1);
			this.AddKey("F2", Keys.F2);
			this.AddKey("F3", Keys.F3);
			this.AddKey(SR.GetString("toStringPageDown"), Keys.Next);
			this.AddKey(SR.GetString("toStringInsert"), Keys.Insert);
			this.AddKey(SR.GetString("toStringHome"), Keys.Home);
			this.AddKey(SR.GetString("toStringDelete"), Keys.Delete);
			this.AddKey(SR.GetString("toStringShift"), Keys.Shift);
			this.AddKey(SR.GetString("toStringPageUp"), Keys.Prior);
			this.AddKey(SR.GetString("toStringBack"), Keys.Back);
			this.AddKey("0", Keys.D0);
			this.AddKey("1", Keys.D1);
			this.AddKey("2", Keys.D2);
			this.AddKey("3", Keys.D3);
			this.AddKey("4", Keys.D4);
			this.AddKey("5", Keys.D5);
			this.AddKey("6", Keys.D6);
			this.AddKey("7", Keys.D7);
			this.AddKey("8", Keys.D8);
			this.AddKey("9", Keys.D9);
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x000C0373 File Offset: 0x000BE573
		private IDictionary KeyNames
		{
			get
			{
				if (this.keyNames == null)
				{
					this.Initialize();
				}
				return this.keyNames;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002A93 RID: 10899 RVA: 0x000C0389 File Offset: 0x000BE589
		private List<string> DisplayOrder
		{
			get
			{
				if (this.displayOrder == null)
				{
					this.Initialize();
				}
				return this.displayOrder;
			}
		}

		/// <summary>Returns a value indicating whether this converter can convert an object in the specified source type to the native type of the converter using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this converter is being invoked from. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <param name="sourceType">The <see cref="T:System.Type" /> to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if the conversion can be performed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002A94 RID: 10900 RVA: 0x000C039F File Offset: 0x000BE59F
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(Enum[]) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Returns a value indicating whether this converter can convert an object in the specified source type to the native type of the converter using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this converter is being invoked from. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if the conversion can be performed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002A95 RID: 10901 RVA: 0x000C03CF File Offset: 0x000BE5CF
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(Enum[]) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Compares two key values for equivalence.</summary>
		/// <param name="a">An <see cref="T:System.Object" /> that represents the first key to compare.</param>
		/// <param name="b">An <see cref="T:System.Object" /> that represents the second key to compare.</param>
		/// <returns>An integer indicating the relationship between the two parameters.  
		///   Value Type  
		///
		///   Condition  
		///
		///   A negative integer.  
		///
		///  <paramref name="a" /> is less than <paramref name="b" />.  
		///
		///   zero  
		///
		///  <paramref name="a" /> equals <paramref name="b" />.  
		///
		///   A positive integer.  
		///
		///  <paramref name="a" /> is greater than <paramref name="b" />.</returns>
		// Token: 0x06002A96 RID: 10902 RVA: 0x000C03ED File Offset: 0x000BE5ED
		public int Compare(object a, object b)
		{
			return string.Compare(base.ConvertToString(a), base.ConvertToString(b), false, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the specified object to the converter's native type.</summary>
		/// <param name="context">An <see langword="ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this converter is being invoked from. This parameter or properties of this parameter can be null.</param>
		/// <param name="culture">A <see langword="CultureInfo" /> object to provide locale information.</param>
		/// <param name="value">The object to convert.</param>
		/// <returns>An object that represents the converted <paramref name="value" />.</returns>
		/// <exception cref="T:System.FormatException">An invalid key combination was supplied.  
		///  -or-  
		///  An invalid key name was supplied.</exception>
		// Token: 0x06002A97 RID: 10903 RVA: 0x000C0408 File Offset: 0x000BE608
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (text.Length == 0)
				{
					return null;
				}
				string[] array = text.Split(new char[] { '+' });
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = array[i].Trim();
				}
				Keys keys = Keys.None;
				bool flag = false;
				for (int j = 0; j < array.Length; j++)
				{
					object obj = this.KeyNames[array[j]];
					if (obj == null)
					{
						obj = Enum.Parse(typeof(Keys), array[j]);
					}
					if (obj == null)
					{
						throw new FormatException(SR.GetString("KeysConverterInvalidKeyName", new object[] { array[j] }));
					}
					Keys keys2 = (Keys)obj;
					if ((keys2 & Keys.KeyCode) != Keys.None)
					{
						if (flag)
						{
							throw new FormatException(SR.GetString("KeysConverterInvalidKeyCombination"));
						}
						flag = true;
					}
					keys |= keys2;
				}
				return keys;
			}
			else
			{
				if (value is Enum[])
				{
					long num = 0L;
					foreach (Enum @enum in (Enum[])value)
					{
						num |= Convert.ToInt64(@enum, CultureInfo.InvariantCulture);
					}
					return Enum.ToObject(typeof(Keys), num);
				}
				return base.ConvertFrom(context, culture, value);
			}
		}

		/// <summary>Converts the specified object to the specified destination type.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this converter is being invoked from. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> to provide locale information.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the object to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
		// Token: 0x06002A98 RID: 10904 RVA: 0x000C0560 File Offset: 0x000BE760
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value is Keys || value is int)
			{
				bool flag = destinationType == typeof(string);
				bool flag2 = false;
				if (!flag)
				{
					flag2 = destinationType == typeof(Enum[]);
				}
				if (flag || flag2)
				{
					Keys keys = (Keys)value;
					bool flag3 = false;
					ArrayList arrayList = new ArrayList();
					Keys keys2 = keys & Keys.Modifiers;
					for (int i = 0; i < this.DisplayOrder.Count; i++)
					{
						string text = this.DisplayOrder[i];
						Keys keys3 = (Keys)this.keyNames[text];
						if ((keys3 & keys2) != Keys.None)
						{
							if (flag)
							{
								if (flag3)
								{
									arrayList.Add("+");
								}
								arrayList.Add(text);
							}
							else
							{
								arrayList.Add(keys3);
							}
							flag3 = true;
						}
					}
					Keys keys4 = keys & Keys.KeyCode;
					bool flag4 = false;
					if (flag3 && flag)
					{
						arrayList.Add("+");
					}
					for (int j = 0; j < this.DisplayOrder.Count; j++)
					{
						string text2 = this.DisplayOrder[j];
						Keys keys5 = (Keys)this.keyNames[text2];
						if (keys5.Equals(keys4))
						{
							if (flag)
							{
								arrayList.Add(text2);
							}
							else
							{
								arrayList.Add(keys5);
							}
							flag4 = true;
							break;
						}
					}
					if (!flag4 && Enum.IsDefined(typeof(Keys), (int)keys4))
					{
						if (flag)
						{
							arrayList.Add(keys4.ToString());
						}
						else
						{
							arrayList.Add(keys4);
						}
					}
					if (flag)
					{
						StringBuilder stringBuilder = new StringBuilder(32);
						foreach (object obj in arrayList)
						{
							string text3 = (string)obj;
							stringBuilder.Append(text3);
						}
						return stringBuilder.ToString();
					}
					return (Enum[])arrayList.ToArray(typeof(Enum));
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Returns a collection of standard values for the data type that this type converter is designed for when provided with a format context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this converter is being invoked from. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, which can be empty if the data type does not support a standard set of values.</returns>
		// Token: 0x06002A99 RID: 10905 RVA: 0x000C07B8 File Offset: 0x000BE9B8
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				ArrayList arrayList = new ArrayList();
				ICollection collection = this.KeyNames.Values;
				foreach (object obj in collection)
				{
					arrayList.Add(obj);
				}
				arrayList.Sort(this);
				this.values = new TypeConverter.StandardValuesCollection(arrayList.ToArray());
			}
			return this.values;
		}

		/// <summary>Determines if the list of standard values returned from <see langword="GetStandardValues" /> is an exclusive list using the specified <see cref="T:System.ComponentModel.ITypeDescriptorContext" />.</summary>
		/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if the collection returned from <see cref="Overload:System.Windows.Forms.KeysConverter.GetStandardValues" /> is an exhaustive list of possible values; otherwise, <see langword="false" /> if other values are possible. The default implementation for this method always returns <see langword="false" />.</returns>
		// Token: 0x06002A9A RID: 10906 RVA: 0x0001180C File Offset: 0x0000FA0C
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Gets a value indicating whether this object supports a standard set of values that can be picked from a list.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this converter is being invoked from. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <returns>Always returns <see langword="true" />.</returns>
		// Token: 0x06002A9B RID: 10907 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x040011F6 RID: 4598
		private IDictionary keyNames;

		// Token: 0x040011F7 RID: 4599
		private List<string> displayOrder;

		// Token: 0x040011F8 RID: 4600
		private TypeConverter.StandardValuesCollection values;

		// Token: 0x040011F9 RID: 4601
		private const Keys FirstDigit = Keys.D0;

		// Token: 0x040011FA RID: 4602
		private const Keys LastDigit = Keys.D9;

		// Token: 0x040011FB RID: 4603
		private const Keys FirstAscii = Keys.A;

		// Token: 0x040011FC RID: 4604
		private const Keys LastAscii = Keys.Z;

		// Token: 0x040011FD RID: 4605
		private const Keys FirstNumpadDigit = Keys.NumPad0;

		// Token: 0x040011FE RID: 4606
		private const Keys LastNumpadDigit = Keys.NumPad9;
	}
}
