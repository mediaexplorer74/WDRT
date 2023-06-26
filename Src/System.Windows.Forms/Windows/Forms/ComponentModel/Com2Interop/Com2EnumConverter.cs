using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000495 RID: 1173
	internal class Com2EnumConverter : TypeConverter
	{
		// Token: 0x06004E6B RID: 20075 RVA: 0x00142E94 File Offset: 0x00141094
		public Com2EnumConverter(Com2Enum enumObj)
		{
			this.com2Enum = enumObj;
		}

		// Token: 0x06004E6C RID: 20076 RVA: 0x000C223C File Offset: 0x000C043C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x00142EA3 File Offset: 0x001410A3
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
		{
			return base.CanConvertTo(context, destType) || destType.IsEnum;
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x00142EB7 File Offset: 0x001410B7
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return this.com2Enum.FromString((string)value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x00142EDC File Offset: 0x001410DC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null)
			{
				string text = this.com2Enum.ToString(value);
				if (text != null)
				{
					return text;
				}
				return "";
			}
			else
			{
				if (destinationType.IsEnum)
				{
					return Enum.ToObject(destinationType, value);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x00142F4C File Offset: 0x0014114C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				object[] array = this.com2Enum.Values;
				if (array != null)
				{
					this.values = new TypeConverter.StandardValuesCollection(array);
				}
			}
			return this.values;
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x00142F82 File Offset: 0x00141182
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return this.com2Enum.IsStrictEnum;
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x00012E4E File Offset: 0x0001104E
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x00142F90 File Offset: 0x00141190
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			string text = this.com2Enum.ToString(value);
			return text != null && text.Length > 0;
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x00142FB8 File Offset: 0x001411B8
		public void RefreshValues()
		{
			this.values = null;
		}

		// Token: 0x04003401 RID: 13313
		internal readonly Com2Enum com2Enum;

		// Token: 0x04003402 RID: 13314
		private TypeConverter.StandardValuesCollection values;
	}
}
