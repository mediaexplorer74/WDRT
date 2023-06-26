using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020001CB RID: 459
	internal class NetRes
	{
		// Token: 0x06001221 RID: 4641 RVA: 0x00060C96 File Offset: 0x0005EE96
		private NetRes()
		{
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00060CA0 File Offset: 0x0005EEA0
		public static string GetWebStatusString(string Res, WebExceptionStatus Status)
		{
			string @string = SR.GetString(WebExceptionMapping.GetWebStatusString(Status));
			string string2 = SR.GetString(Res);
			return string.Format(CultureInfo.CurrentCulture, string2, new object[] { @string });
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00060CD5 File Offset: 0x0005EED5
		public static string GetWebStatusString(WebExceptionStatus Status)
		{
			return SR.GetString(WebExceptionMapping.GetWebStatusString(Status));
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00060CE4 File Offset: 0x0005EEE4
		public static string GetWebStatusCodeString(HttpStatusCode statusCode, string statusDescription)
		{
			string text = "(";
			int num = (int)statusCode;
			string text2 = text + num.ToString(NumberFormatInfo.InvariantInfo) + ")";
			string text3 = null;
			try
			{
				text3 = SR.GetString("net_httpstatuscode_" + statusCode.ToString(), null);
			}
			catch
			{
			}
			if (text3 != null && text3.Length > 0)
			{
				text2 = text2 + " " + text3;
			}
			else if (statusDescription != null && statusDescription.Length > 0)
			{
				text2 = text2 + " " + statusDescription;
			}
			return text2;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00060D7C File Offset: 0x0005EF7C
		public static string GetWebStatusCodeString(FtpStatusCode statusCode, string statusDescription)
		{
			string text = "(";
			int num = (int)statusCode;
			string text2 = text + num.ToString(NumberFormatInfo.InvariantInfo) + ")";
			string text3 = null;
			try
			{
				text3 = SR.GetString("net_ftpstatuscode_" + statusCode.ToString(), null);
			}
			catch
			{
			}
			if (text3 != null && text3.Length > 0)
			{
				text2 = text2 + " " + text3;
			}
			else if (statusDescription != null && statusDescription.Length > 0)
			{
				text2 = text2 + " " + statusDescription;
			}
			return text2;
		}
	}
}
