using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 32-bit signed integer objects to and from other representations.</summary>
	// Token: 0x0200056E RID: 1390
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class Int32Converter : BaseNumberConverter
	{
		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060033B6 RID: 13238 RVA: 0x000E3915 File Offset: 0x000E1B15
		internal override Type TargetType
		{
			get
			{
				return typeof(int);
			}
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x000E3921 File Offset: 0x000E1B21
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt32(value, radix);
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000E392F File Offset: 0x000E1B2F
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return int.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000E393E File Offset: 0x000E1B3E
		internal override object FromString(string value, CultureInfo culture)
		{
			return int.Parse(value, culture);
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x000E394C File Offset: 0x000E1B4C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((int)value).ToString("G", formatInfo);
		}
	}
}
