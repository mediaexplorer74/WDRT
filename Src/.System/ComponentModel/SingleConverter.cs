using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert single-precision, floating point number objects to and from various other representations.</summary>
	// Token: 0x020005AB RID: 1451
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class SingleConverter : BaseNumberConverter
	{
		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x000EBD99 File Offset: 0x000E9F99
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06003610 RID: 13840 RVA: 0x000EBD9C File Offset: 0x000E9F9C
		internal override Type TargetType
		{
			get
			{
				return typeof(float);
			}
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x000EBDA8 File Offset: 0x000E9FA8
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSingle(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x000EBDBA File Offset: 0x000E9FBA
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return float.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x000EBDCD File Offset: 0x000E9FCD
		internal override object FromString(string value, CultureInfo culture)
		{
			return float.Parse(value, culture);
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x000EBDDC File Offset: 0x000E9FDC
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((float)value).ToString("R", formatInfo);
		}
	}
}
