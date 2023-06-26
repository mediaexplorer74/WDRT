using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 32-bit unsigned integer objects to and from various other representations.</summary>
	// Token: 0x020005B8 RID: 1464
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class UInt32Converter : BaseNumberConverter
	{
		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x060036E4 RID: 14052 RVA: 0x000EEEE5 File Offset: 0x000ED0E5
		internal override Type TargetType
		{
			get
			{
				return typeof(uint);
			}
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000EEEF1 File Offset: 0x000ED0F1
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt32(value, radix);
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000EEEFF File Offset: 0x000ED0FF
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return uint.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000EEF0E File Offset: 0x000ED10E
		internal override object FromString(string value, CultureInfo culture)
		{
			return uint.Parse(value, culture);
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000EEF1C File Offset: 0x000ED11C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((uint)value).ToString("G", formatInfo);
		}
	}
}
