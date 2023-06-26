using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 8-bit unsigned integer objects to and from a string.</summary>
	// Token: 0x020005A9 RID: 1449
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class SByteConverter : BaseNumberConverter
	{
		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06003604 RID: 13828 RVA: 0x000EBCD4 File Offset: 0x000E9ED4
		internal override Type TargetType
		{
			get
			{
				return typeof(sbyte);
			}
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000EBCE0 File Offset: 0x000E9EE0
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSByte(value, radix);
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000EBCEE File Offset: 0x000E9EEE
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return sbyte.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000EBCFD File Offset: 0x000E9EFD
		internal override object FromString(string value, CultureInfo culture)
		{
			return sbyte.Parse(value, culture);
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000EBD0C File Offset: 0x000E9F0C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((sbyte)value).ToString("G", formatInfo);
		}
	}
}
