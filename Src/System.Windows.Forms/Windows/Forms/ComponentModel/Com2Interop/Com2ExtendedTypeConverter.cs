using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000497 RID: 1175
	internal class Com2ExtendedTypeConverter : TypeConverter
	{
		// Token: 0x06004E79 RID: 20089 RVA: 0x00142FD3 File Offset: 0x001411D3
		public Com2ExtendedTypeConverter(TypeConverter innerConverter)
		{
			this.innerConverter = innerConverter;
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x00142FE2 File Offset: 0x001411E2
		public Com2ExtendedTypeConverter(Type baseType)
		{
			this.innerConverter = TypeDescriptor.GetConverter(baseType);
		}

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06004E7B RID: 20091 RVA: 0x00142FF6 File Offset: 0x001411F6
		public TypeConverter InnerConverter
		{
			get
			{
				return this.innerConverter;
			}
		}

		// Token: 0x06004E7C RID: 20092 RVA: 0x00143000 File Offset: 0x00141200
		public TypeConverter GetWrappedConverter(Type t)
		{
			for (TypeConverter typeConverter = this.innerConverter; typeConverter != null; typeConverter = ((Com2ExtendedTypeConverter)typeConverter).InnerConverter)
			{
				if (t.IsInstanceOfType(typeConverter))
				{
					return typeConverter;
				}
				if (!(typeConverter is Com2ExtendedTypeConverter))
				{
					break;
				}
			}
			return null;
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x00143039 File Offset: 0x00141239
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.CanConvertFrom(context, sourceType);
			}
			return base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x00143059 File Offset: 0x00141259
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.CanConvertTo(context, destinationType);
			}
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x00143079 File Offset: 0x00141279
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.ConvertFrom(context, culture, value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x0014309B File Offset: 0x0014129B
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.ConvertTo(context, culture, value, destinationType);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x001430C1 File Offset: 0x001412C1
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.CreateInstance(context, propertyValues);
			}
			return base.CreateInstance(context, propertyValues);
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x001430E1 File Offset: 0x001412E1
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.GetCreateInstanceSupported(context);
			}
			return base.GetCreateInstanceSupported(context);
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x001430FF File Offset: 0x001412FF
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.GetProperties(context, value, attributes);
			}
			return base.GetProperties(context, value, attributes);
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x00143121 File Offset: 0x00141321
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.GetPropertiesSupported(context);
			}
			return base.GetPropertiesSupported(context);
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x0014313F File Offset: 0x0014133F
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.GetStandardValues(context);
			}
			return base.GetStandardValues(context);
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x0014315D File Offset: 0x0014135D
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.GetStandardValuesExclusive(context);
			}
			return base.GetStandardValuesExclusive(context);
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x0014317B File Offset: 0x0014137B
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.GetStandardValuesSupported(context);
			}
			return base.GetStandardValuesSupported(context);
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x00143199 File Offset: 0x00141399
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (this.innerConverter != null)
			{
				return this.innerConverter.IsValid(context, value);
			}
			return base.IsValid(context, value);
		}

		// Token: 0x04003403 RID: 13315
		private TypeConverter innerConverter;
	}
}
