using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit signed integer objects to and from other representations.</summary>
	// Token: 0x0200056D RID: 1389
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class Int16Converter : BaseNumberConverter
	{
		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x060033B0 RID: 13232 RVA: 0x000E38B4 File Offset: 0x000E1AB4
		internal override Type TargetType
		{
			get
			{
				return typeof(short);
			}
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000E38C0 File Offset: 0x000E1AC0
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt16(value, radix);
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x000E38CE File Offset: 0x000E1ACE
		internal override object FromString(string value, CultureInfo culture)
		{
			return short.Parse(value, culture);
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x000E38DC File Offset: 0x000E1ADC
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return short.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x000E38EC File Offset: 0x000E1AEC
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((short)value).ToString("G", formatInfo);
		}
	}
}
