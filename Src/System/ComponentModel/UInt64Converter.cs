using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 64-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x020005B9 RID: 1465
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class UInt64Converter : BaseNumberConverter
	{
		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x060036EA RID: 14058 RVA: 0x000EEF45 File Offset: 0x000ED145
		internal override Type TargetType
		{
			get
			{
				return typeof(ulong);
			}
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000EEF51 File Offset: 0x000ED151
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt64(value, radix);
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000EEF5F File Offset: 0x000ED15F
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ulong.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000EEF6E File Offset: 0x000ED16E
		internal override object FromString(string value, CultureInfo culture)
		{
			return ulong.Parse(value, culture);
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000EEF7C File Offset: 0x000ED17C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ulong)value).ToString("G", formatInfo);
		}
	}
}
