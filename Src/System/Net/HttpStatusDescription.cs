using System;
using System.Runtime.CompilerServices;

namespace System.Net
{
	// Token: 0x02000105 RID: 261
	internal static class HttpStatusDescription
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x00035D6D File Offset: 0x00033F6D
		[FriendAccessAllowed]
		internal static string Get(HttpStatusCode code)
		{
			return HttpStatusDescription.Get((int)code);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00035D78 File Offset: 0x00033F78
		internal static string Get(int code)
		{
			if (code >= 100 && code < 600)
			{
				int num = code / 100;
				int num2 = code % 100;
				if (num2 < HttpStatusDescription.httpStatusDescriptions[num].Length)
				{
					return HttpStatusDescription.httpStatusDescriptions[num][num2];
				}
			}
			return null;
		}

		// Token: 0x04000EC0 RID: 3776
		private static readonly string[][] httpStatusDescriptions = new string[][]
		{
			null,
			new string[] { "Continue", "Switching Protocols", "Processing" },
			new string[] { "OK", "Created", "Accepted", "Non-Authoritative Information", "No Content", "Reset Content", "Partial Content", "Multi-Status" },
			new string[] { "Multiple Choices", "Moved Permanently", "Found", "See Other", "Not Modified", "Use Proxy", null, "Temporary Redirect" },
			new string[]
			{
				"Bad Request", "Unauthorized", "Payment Required", "Forbidden", "Not Found", "Method Not Allowed", "Not Acceptable", "Proxy Authentication Required", "Request Timeout", "Conflict",
				"Gone", "Length Required", "Precondition Failed", "Request Entity Too Large", "Request-Uri Too Long", "Unsupported Media Type", "Requested Range Not Satisfiable", "Expectation Failed", null, null,
				null, null, "Unprocessable Entity", "Locked", "Failed Dependency", null, "Upgrade Required"
			},
			new string[] { "Internal Server Error", "Not Implemented", "Bad Gateway", "Service Unavailable", "Gateway Timeout", "Http Version Not Supported", null, "Insufficient Storage" }
		};
	}
}
