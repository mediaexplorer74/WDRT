using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.CSharp
{
	// Token: 0x02000010 RID: 16
	internal abstract class CSharpModifierAttributeConverter : TypeConverter
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600014A RID: 330
		protected abstract object[] Values { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600014B RID: 331
		protected abstract string[] Names { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600014C RID: 332
		protected abstract object DefaultValue { get; }

		// Token: 0x0600014D RID: 333 RVA: 0x0000D29F File Offset: 0x0000B49F
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = (string)value;
				string[] names = this.Names;
				for (int i = 0; i < names.Length; i++)
				{
					if (names[i].Equals(text))
					{
						return this.Values[i];
					}
				}
			}
			return this.DefaultValue;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000D30C File Offset: 0x0000B50C
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

		// Token: 0x06000150 RID: 336 RVA: 0x0000D380 File Offset: 0x0000B580
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000D383 File Offset: 0x0000B583
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000D386 File Offset: 0x0000B586
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(this.Values);
		}
	}
}
