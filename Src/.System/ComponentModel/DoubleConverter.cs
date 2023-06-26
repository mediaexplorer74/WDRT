using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert double-precision, floating point number objects to and from various other representations.</summary>
	// Token: 0x02000547 RID: 1351
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DoubleConverter : BaseNumberConverter
	{
		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x000E23B2 File Offset: 0x000E05B2
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x060032CB RID: 13003 RVA: 0x000E23B5 File Offset: 0x000E05B5
		internal override Type TargetType
		{
			get
			{
				return typeof(double);
			}
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000E23C1 File Offset: 0x000E05C1
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDouble(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000E23D3 File Offset: 0x000E05D3
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return double.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000E23E6 File Offset: 0x000E05E6
		internal override object FromString(string value, CultureInfo culture)
		{
			return double.Parse(value, culture);
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000E23F4 File Offset: 0x000E05F4
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((double)value).ToString("R", formatInfo);
		}
	}
}
