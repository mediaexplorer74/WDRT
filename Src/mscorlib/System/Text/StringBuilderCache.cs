using System;

namespace System.Text
{
	// Token: 0x02000A57 RID: 2647
	internal static class StringBuilderCache
	{
		// Token: 0x0600674D RID: 26445 RVA: 0x0015D944 File Offset: 0x0015BB44
		public static StringBuilder Acquire(int capacity = 16)
		{
			if (capacity <= 360)
			{
				StringBuilder cachedInstance = StringBuilderCache.CachedInstance;
				if (cachedInstance != null && capacity <= cachedInstance.Capacity)
				{
					StringBuilderCache.CachedInstance = null;
					cachedInstance.Clear();
					return cachedInstance;
				}
			}
			return new StringBuilder(capacity);
		}

		// Token: 0x0600674E RID: 26446 RVA: 0x0015D980 File Offset: 0x0015BB80
		public static void Release(StringBuilder sb)
		{
			if (sb.Capacity <= 360)
			{
				StringBuilderCache.CachedInstance = sb;
			}
		}

		// Token: 0x0600674F RID: 26447 RVA: 0x0015D998 File Offset: 0x0015BB98
		public static string GetStringAndRelease(StringBuilder sb)
		{
			string text = sb.ToString();
			StringBuilderCache.Release(sb);
			return text;
		}

		// Token: 0x04002E2F RID: 11823
		internal const int MAX_BUILDER_SIZE = 360;

		// Token: 0x04002E30 RID: 11824
		[ThreadStatic]
		private static StringBuilder CachedInstance;
	}
}
