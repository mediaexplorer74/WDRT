using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x0200049A RID: 1178
	internal class Com2IDispatchConverter : Com2ExtendedTypeConverter
	{
		// Token: 0x06004E93 RID: 20115 RVA: 0x001434B8 File Offset: 0x001416B8
		public Com2IDispatchConverter(Com2PropertyDescriptor propDesc, bool allowExpand, TypeConverter baseConverter)
			: base(baseConverter)
		{
			this.propDesc = propDesc;
			this.allowExpand = allowExpand;
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x001434CF File Offset: 0x001416CF
		public Com2IDispatchConverter(Com2PropertyDescriptor propDesc, bool allowExpand)
			: base(propDesc.PropertyType)
		{
			this.propDesc = propDesc;
			this.allowExpand = allowExpand;
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x0001180C File Offset: 0x0000FA0C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return false;
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x001434EB File Offset: 0x001416EB
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x00143500 File Offset: 0x00141700
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value == null)
			{
				return Com2IDispatchConverter.none;
			}
			string text = ComNativeDescriptor.Instance.GetName(value);
			if (text == null || text.Length == 0)
			{
				text = ComNativeDescriptor.Instance.GetClassName(value);
			}
			if (text == null)
			{
				return "(Object)";
			}
			return text;
		}

		// Token: 0x06004E98 RID: 20120 RVA: 0x00143562 File Offset: 0x00141762
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(value, attributes);
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x0014356B File Offset: 0x0014176B
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return this.allowExpand;
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x0001180C File Offset: 0x0000FA0C
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x04003406 RID: 13318
		private Com2PropertyDescriptor propDesc;

		// Token: 0x04003407 RID: 13319
		protected static readonly string none = SR.GetString("toStringNone");

		// Token: 0x04003408 RID: 13320
		private bool allowExpand;
	}
}
