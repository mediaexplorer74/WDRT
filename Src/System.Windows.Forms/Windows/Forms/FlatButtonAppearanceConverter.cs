using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000254 RID: 596
	internal class FlatButtonAppearanceConverter : ExpandableObjectConverter
	{
		// Token: 0x060025B3 RID: 9651 RVA: 0x000AF8F4 File Offset: 0x000ADAF4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return "";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000AF91C File Offset: 0x000ADB1C
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (context != null && context.Instance is Button)
			{
				Attribute[] array = new Attribute[attributes.Length + 1];
				attributes.CopyTo(array, 0);
				array[attributes.Length] = new ApplicableToButtonAttribute();
				attributes = array;
			}
			return TypeDescriptor.GetProperties(value, attributes);
		}
	}
}
