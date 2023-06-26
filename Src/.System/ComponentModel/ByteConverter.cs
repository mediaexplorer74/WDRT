using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 8-bit unsigned integer objects to and from various other representations.</summary>
	// Token: 0x0200051F RID: 1311
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ByteConverter : BaseNumberConverter
	{
		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x060031BC RID: 12732 RVA: 0x000DFBA9 File Offset: 0x000DDDA9
		internal override Type TargetType
		{
			get
			{
				return typeof(byte);
			}
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000DFBB5 File Offset: 0x000DDDB5
		internal override object FromString(string value, int radix)
		{
			return Convert.ToByte(value, radix);
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000DFBC3 File Offset: 0x000DDDC3
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return byte.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000DFBD2 File Offset: 0x000DDDD2
		internal override object FromString(string value, CultureInfo culture)
		{
			return byte.Parse(value, culture);
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000DFBE0 File Offset: 0x000DDDE0
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((byte)value).ToString("G", formatInfo);
		}
	}
}
