using System;
using System.Resources;
using Microsoft.Reflection;

namespace System.Diagnostics.Tracing.Internal
{
	// Token: 0x02000487 RID: 1159
	internal static class Environment
	{
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06003781 RID: 14209 RVA: 0x000D6C28 File Offset: 0x000D4E28
		public static int TickCount
		{
			get
			{
				return Environment.TickCount;
			}
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000D6C30 File Offset: 0x000D4E30
		public static string GetResourceString(string key, params object[] args)
		{
			string @string = Environment.rm.GetString(key);
			if (@string != null)
			{
				return string.Format(@string, args);
			}
			string text = string.Empty;
			foreach (object obj in args)
			{
				if (text != string.Empty)
				{
					text += ", ";
				}
				text += obj.ToString();
			}
			return key + " (" + text + ")";
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000D6CA7 File Offset: 0x000D4EA7
		public static string GetRuntimeResourceString(string key, params object[] args)
		{
			return Environment.GetResourceString(key, args);
		}

		// Token: 0x040018BA RID: 6330
		public static readonly string NewLine = Environment.NewLine;

		// Token: 0x040018BB RID: 6331
		private static ResourceManager rm = new ResourceManager("Microsoft.Diagnostics.Tracing.Messages", typeof(Environment).Assembly());
	}
}
