using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.Cursor" /> objects to and from various other representations.</summary>
	// Token: 0x02000176 RID: 374
	public class CursorConverter : TypeConverter
	{
		/// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
		/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
		/// <param name="sourceType">The type you wish to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if this object can perform the conversion.</returns>
		// Token: 0x06001401 RID: 5121 RVA: 0x000434A7 File Offset: 0x000416A7
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(byte[]) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001402 RID: 5122 RVA: 0x000434D7 File Offset: 0x000416D7
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(byte[]) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		// Token: 0x06001403 RID: 5123 RVA: 0x00043508 File Offset: 0x00041708
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				foreach (PropertyInfo propertyInfo in this.GetProperties())
				{
					if (string.Equals(propertyInfo.Name, text, StringComparison.OrdinalIgnoreCase))
					{
						object[] array = null;
						return propertyInfo.GetValue(null, array);
					}
				}
			}
			if (value is byte[])
			{
				MemoryStream memoryStream = new MemoryStream((byte[])value);
				return new Cursor(memoryStream);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If <see langword="null" /> is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		// Token: 0x06001404 RID: 5124 RVA: 0x00043584 File Offset: 0x00041784
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null)
			{
				PropertyInfo[] properties = this.GetProperties();
				int num = -1;
				for (int i = 0; i < properties.Length; i++)
				{
					PropertyInfo propertyInfo = properties[i];
					object[] array = null;
					Cursor cursor = (Cursor)propertyInfo.GetValue(null, array);
					if (cursor == (Cursor)value)
					{
						if (cursor == value)
						{
							return propertyInfo.Name;
						}
						num = i;
					}
				}
				if (num != -1)
				{
					return properties[num].Name;
				}
				throw new FormatException(SR.GetString("CursorCannotCovertToString"));
			}
			else
			{
				if (destinationType == typeof(InstanceDescriptor) && value is Cursor)
				{
					PropertyInfo[] properties2 = this.GetProperties();
					foreach (PropertyInfo propertyInfo2 in properties2)
					{
						if (propertyInfo2.GetValue(null, null) == value)
						{
							return new InstanceDescriptor(propertyInfo2, null);
						}
					}
				}
				if (!(destinationType == typeof(byte[])))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				if (value != null)
				{
					MemoryStream memoryStream = new MemoryStream();
					Cursor cursor2 = (Cursor)value;
					cursor2.SavePicture(memoryStream);
					memoryStream.Close();
					return memoryStream.ToArray();
				}
				return new byte[0];
			}
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x000436C9 File Offset: 0x000418C9
		private PropertyInfo[] GetProperties()
		{
			return typeof(Cursors).GetProperties(BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Retrieves a collection containing a set of standard values for the data type this validator is designed for. This will return <see langword="null" /> if the data type does not support a standard set of values.</summary>
		/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
		/// <returns>A collection containing a standard set of valid values, or <see langword="null" />. The default implementation always returns <see langword="null" />.</returns>
		// Token: 0x06001406 RID: 5126 RVA: 0x000436DC File Offset: 0x000418DC
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				ArrayList arrayList = new ArrayList();
				foreach (PropertyInfo propertyInfo in this.GetProperties())
				{
					object[] array = null;
					arrayList.Add(propertyInfo.GetValue(null, array));
				}
				this.values = new TypeConverter.StandardValuesCollection(arrayList.ToArray());
			}
			return this.values;
		}

		/// <summary>Determines if this object supports a standard set of values that can be picked from a list.</summary>
		/// <param name="context">A type descriptor through which additional context may be provided.</param>
		/// <returns>Returns <see langword="true" /> if <see langword="GetStandardValues" /> should be called to find a common set of values the object supports.</returns>
		// Token: 0x06001407 RID: 5127 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0400094C RID: 2380
		private TypeConverter.StandardValuesCollection values;
	}
}
