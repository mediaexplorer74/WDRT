using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.VisualBasic
{
	// Token: 0x0200000B RID: 11
	internal abstract class VBModifierAttributeConverter : TypeConverter
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600008A RID: 138
		protected abstract object[] Values { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600008B RID: 139
		protected abstract string[] Names { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600008C RID: 140
		protected abstract object DefaultValue { get; }

		// Token: 0x0600008D RID: 141 RVA: 0x00006C85 File Offset: 0x00004E85
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00006CA4 File Offset: 0x00004EA4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = (string)value;
				string[] names = this.Names;
				for (int i = 0; i < names.Length; i++)
				{
					if (names[i].Equals(text, StringComparison.OrdinalIgnoreCase))
					{
						return this.Values[i];
					}
				}
			}
			return this.DefaultValue;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006CF0 File Offset: 0x00004EF0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				object[] values = this.Values;
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i].Equals(value))
					{
						return this.Names[i];
					}
				}
				return SR.GetString("toStringUnknown");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006D67 File Offset: 0x00004F67
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00006D6A File Offset: 0x00004F6A
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(this.Values);
		}
	}
}
