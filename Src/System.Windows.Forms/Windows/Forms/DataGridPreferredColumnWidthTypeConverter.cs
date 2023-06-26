using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Converts the value of an object to a different data type.</summary>
	// Token: 0x02000181 RID: 385
	public class DataGridPreferredColumnWidthTypeConverter : TypeConverter
	{
		/// <summary>Gets a value that specifies whether the converter can convert an object in the given source type to the native type of the converter.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
		/// <returns>
		///   <see langword="true" />, if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x060016D0 RID: 5840 RVA: 0x00051593 File Offset: 0x0004F793
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(int);
		}

		/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If <see langword="null" /> is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		// Token: 0x060016D1 RID: 5841 RVA: 0x000515BC File Offset: 0x0004F7BC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (!(value.GetType() == typeof(int)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			int num = (int)value;
			if (num == -1)
			{
				return "AutoColumnResize (-1)";
			}
			return num.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		// Token: 0x060016D2 RID: 5842 RVA: 0x00051628 File Offset: 0x0004F828
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value.GetType() == typeof(string))
			{
				string text = value.ToString();
				if (text.Equals("AutoColumnResize (-1)"))
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.CurrentCulture);
			}
			else
			{
				if (value.GetType() == typeof(int))
				{
					return (int)value;
				}
				throw base.GetConvertFromException(value);
			}
		}
	}
}
