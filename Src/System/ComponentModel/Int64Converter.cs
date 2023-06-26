using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 64-bit signed integer objects to and from various other representations.</summary>
	// Token: 0x0200056F RID: 1391
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class Int64Converter : BaseNumberConverter
	{
		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060033BC RID: 13244 RVA: 0x000E3975 File Offset: 0x000E1B75
		internal override Type TargetType
		{
			get
			{
				return typeof(long);
			}
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000E3981 File Offset: 0x000E1B81
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt64(value, radix);
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000E398F File Offset: 0x000E1B8F
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return long.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000E399E File Offset: 0x000E1B9E
		internal override object FromString(string value, CultureInfo culture)
		{
			return long.Parse(value, culture);
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000E39AC File Offset: 0x000E1BAC
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((long)value).ToString("G", formatInfo);
		}
	}
}
