using System;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x0200032B RID: 811
	internal static class AutomationMessages
	{
		// Token: 0x06003510 RID: 13584 RVA: 0x000F11E0 File Offset: 0x000EF3E0
		public static IntPtr WriteAutomationText(string text)
		{
			IntPtr intPtr = IntPtr.Zero;
			string text2 = AutomationMessages.GenerateLogFileName(ref intPtr);
			if (text2 != null)
			{
				try
				{
					FileStream fileStream = new FileStream(text2, FileMode.Create, FileAccess.Write);
					StreamWriter streamWriter = new StreamWriter(fileStream);
					streamWriter.WriteLine(text);
					streamWriter.Dispose();
					fileStream.Dispose();
				}
				catch
				{
					intPtr = IntPtr.Zero;
				}
			}
			return intPtr;
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x000F1240 File Offset: 0x000EF440
		public static string ReadAutomationText(IntPtr fileId)
		{
			string text = null;
			if (fileId != IntPtr.Zero)
			{
				string text2 = AutomationMessages.GenerateLogFileName(ref fileId);
				if (File.Exists(text2))
				{
					try
					{
						FileStream fileStream = new FileStream(text2, FileMode.Open, FileAccess.Read);
						StreamReader streamReader = new StreamReader(fileStream);
						text = streamReader.ReadToEnd();
						streamReader.Dispose();
						fileStream.Dispose();
					}
					catch
					{
						text = null;
					}
				}
			}
			return text;
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x000F12A8 File Offset: 0x000EF4A8
		private static string GenerateLogFileName(ref IntPtr fileId)
		{
			string text = null;
			string environmentVariable = Environment.GetEnvironmentVariable("TEMP");
			if (environmentVariable != null)
			{
				if (fileId == IntPtr.Zero)
				{
					Random random = new Random(DateTime.Now.Millisecond);
					fileId = new IntPtr(random.Next());
				}
				text = environmentVariable + "\\Maui" + fileId.ToString() + ".log";
			}
			return text;
		}

		// Token: 0x04001F2A RID: 7978
		private const int WM_USER = 1024;

		// Token: 0x04001F2B RID: 7979
		internal const int PGM_GETBUTTONCOUNT = 1104;

		// Token: 0x04001F2C RID: 7980
		internal const int PGM_GETBUTTONSTATE = 1106;

		// Token: 0x04001F2D RID: 7981
		internal const int PGM_SETBUTTONSTATE = 1105;

		// Token: 0x04001F2E RID: 7982
		internal const int PGM_GETBUTTONTEXT = 1107;

		// Token: 0x04001F2F RID: 7983
		internal const int PGM_GETBUTTONTOOLTIPTEXT = 1108;

		// Token: 0x04001F30 RID: 7984
		internal const int PGM_GETROWCOORDS = 1109;

		// Token: 0x04001F31 RID: 7985
		internal const int PGM_GETVISIBLEROWCOUNT = 1110;

		// Token: 0x04001F32 RID: 7986
		internal const int PGM_GETSELECTEDROW = 1111;

		// Token: 0x04001F33 RID: 7987
		internal const int PGM_SETSELECTEDTAB = 1112;

		// Token: 0x04001F34 RID: 7988
		internal const int PGM_GETTESTINGINFO = 1113;
	}
}
