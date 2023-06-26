using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.IO
{
	// Token: 0x020001AB RID: 427
	[ComVisible(false)]
	internal static class LongPath
	{
		// Token: 0x06001ADB RID: 6875 RVA: 0x0005A556 File Offset: 0x00058756
		[SecurityCritical]
		internal static string NormalizePath(string path)
		{
			return LongPath.NormalizePath(path, true);
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0005A55F File Offset: 0x0005875F
		[SecurityCritical]
		internal static string NormalizePath(string path, bool fullCheck)
		{
			return Path.NormalizePath(path, fullCheck, 32767);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0005A570 File Offset: 0x00058770
		internal static string InternalCombine(string path1, string path2)
		{
			bool flag;
			string text = LongPath.TryRemoveLongPathPrefix(path1, out flag);
			string text2 = Path.InternalCombine(text, path2);
			if (flag)
			{
				text2 = Path.AddLongPathPrefix(text2);
			}
			return text2;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0005A59C File Offset: 0x0005879C
		internal static int GetRootLength(string path)
		{
			bool flag;
			string text = LongPath.TryRemoveLongPathPrefix(path, out flag);
			int num = Path.GetRootLength(text);
			if (flag)
			{
				num += 4;
			}
			return num;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0005A5C4 File Offset: 0x000587C4
		internal static bool IsPathRooted(string path)
		{
			string text = Path.RemoveLongPathPrefix(path);
			return Path.IsPathRooted(text);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0005A5E0 File Offset: 0x000587E0
		[SecurityCritical]
		internal static string GetPathRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			bool flag;
			string text = LongPath.TryRemoveLongPathPrefix(path, out flag);
			text = LongPath.NormalizePath(text, false);
			string text2 = path.Substring(0, LongPath.GetRootLength(text));
			if (flag)
			{
				text2 = Path.AddLongPathPrefix(text2);
			}
			return text2;
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0005A61C File Offset: 0x0005881C
		[SecurityCritical]
		internal static string GetDirectoryName(string path)
		{
			if (path != null)
			{
				bool flag;
				string text = LongPath.TryRemoveLongPathPrefix(path, out flag);
				Path.CheckInvalidPathChars(text, false);
				path = LongPath.NormalizePath(text, false);
				int rootLength = LongPath.GetRootLength(text);
				int num = text.Length;
				if (num > rootLength)
				{
					num = text.Length;
					if (num == rootLength)
					{
						return null;
					}
					while (num > rootLength && text[--num] != Path.DirectorySeparatorChar && text[num] != Path.AltDirectorySeparatorChar)
					{
					}
					string text2 = text.Substring(0, num);
					if (flag)
					{
						text2 = Path.AddLongPathPrefix(text2);
					}
					return text2;
				}
			}
			return null;
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0005A6A2 File Offset: 0x000588A2
		internal static string TryRemoveLongPathPrefix(string path, out bool removed)
		{
			removed = Path.HasLongPathPrefix(path);
			if (!removed)
			{
				return path;
			}
			return Path.RemoveLongPathPrefix(path);
		}
	}
}
