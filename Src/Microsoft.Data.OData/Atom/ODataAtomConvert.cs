using System;
using System.Globalization;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000285 RID: 645
	internal static class ODataAtomConvert
	{
		// Token: 0x0600156D RID: 5485 RVA: 0x0004EAAB File Offset: 0x0004CCAB
		internal static string ToString(bool b)
		{
			if (!b)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0004EABB File Offset: 0x0004CCBB
		internal static string ToString(byte b)
		{
			return XmlConvert.ToString(b);
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0004EAC3 File Offset: 0x0004CCC3
		internal static string ToString(decimal d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0004EACB File Offset: 0x0004CCCB
		internal static string ToString(this DateTime dt)
		{
			return PlatformHelper.ConvertDateTimeToString(dt);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0004EAD3 File Offset: 0x0004CCD3
		internal static string ToString(DateTimeOffset dateTime)
		{
			return XmlConvert.ToString(dateTime);
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0004EADC File Offset: 0x0004CCDC
		internal static string ToAtomString(DateTimeOffset dateTime)
		{
			if (dateTime.Offset == ODataAtomConvert.zeroOffset)
			{
				return dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
			}
			return dateTime.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0004EB27 File Offset: 0x0004CD27
		internal static string ToString(this TimeSpan ts)
		{
			return XmlConvert.ToString(ts);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0004EB2F File Offset: 0x0004CD2F
		internal static string ToString(this double d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0004EB37 File Offset: 0x0004CD37
		internal static string ToString(this short i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0004EB3F File Offset: 0x0004CD3F
		internal static string ToString(this int i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0004EB47 File Offset: 0x0004CD47
		internal static string ToString(this long i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0004EB4F File Offset: 0x0004CD4F
		internal static string ToString(this sbyte sb)
		{
			return XmlConvert.ToString(sb);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0004EB57 File Offset: 0x0004CD57
		internal static string ToString(this byte[] bytes)
		{
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0004EB5F File Offset: 0x0004CD5F
		internal static string ToString(this float s)
		{
			return XmlConvert.ToString(s);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0004EB67 File Offset: 0x0004CD67
		internal static string ToString(this Guid guid)
		{
			return XmlConvert.ToString(guid);
		}

		// Token: 0x040007D3 RID: 2003
		private static readonly TimeSpan zeroOffset = new TimeSpan(0, 0, 0);
	}
}
