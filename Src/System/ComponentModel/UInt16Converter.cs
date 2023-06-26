using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x020005B7 RID: 1463
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class UInt16Converter : BaseNumberConverter
	{
		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x060036DE RID: 14046 RVA: 0x000EEE83 File Offset: 0x000ED083
		internal override Type TargetType
		{
			get
			{
				return typeof(ushort);
			}
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000EEE8F File Offset: 0x000ED08F
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt16(value, radix);
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000EEE9D File Offset: 0x000ED09D
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ushort.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x000EEEAC File Offset: 0x000ED0AC
		internal override object FromString(string value, CultureInfo culture)
		{
			return ushort.Parse(value, culture);
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x000EEEBC File Offset: 0x000ED0BC
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ushort)value).ToString("G", formatInfo);
		}
	}
}
