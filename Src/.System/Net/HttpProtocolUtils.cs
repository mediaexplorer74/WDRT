using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x02000143 RID: 323
	internal class HttpProtocolUtils
	{
		// Token: 0x06000B5C RID: 2908 RVA: 0x0003E0BD File Offset: 0x0003C2BD
		private HttpProtocolUtils()
		{
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0003E0C8 File Offset: 0x0003C2C8
		internal static DateTime string2date(string S)
		{
			DateTime dateTime;
			if (HttpDateParse.ParseHttpDate(S, out dateTime))
			{
				return dateTime;
			}
			throw new ProtocolViolationException(SR.GetString("net_baddate"));
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0003E0F0 File Offset: 0x0003C2F0
		internal static string date2string(DateTime D)
		{
			DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
			return D.ToUniversalTime().ToString("R", dateTimeFormatInfo);
		}
	}
}
