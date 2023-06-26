using System;
using System.Globalization;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x0200007F RID: 127
	public class DialogManager
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x00016287 File Offset: 0x00014487
		private DialogManager()
		{
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00016624 File Offset: 0x00014824
		public static DialogManager Instance
		{
			get
			{
				return DialogManager.Nested.NestedInstance;
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001663B File Offset: 0x0001483B
		public void SetShellWindow(Window shell)
		{
			this.shellWindow = shell;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00016648 File Offset: 0x00014848
		public string OpenFileDialog(string extension, string initialDirectory = null)
		{
			Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
			{
				DefaultExt = "." + extension.ToLower(CultureInfo.CurrentCulture),
				Filter = string.Format("{0} files (*.{1})|*.{1}", extension.ToUpper(CultureInfo.CurrentCulture), extension.ToLower(CultureInfo.CurrentCulture))
			};
			bool flag = initialDirectory != null;
			if (flag)
			{
				openFileDialog.InitialDirectory = initialDirectory;
			}
			bool? flag2 = openFileDialog.ShowDialog();
			bool? flag3 = flag2;
			bool flag4 = true;
			bool flag5 = (flag3.GetValueOrDefault() == flag4) & (flag3 != null);
			string text;
			if (flag5)
			{
				text = openFileDialog.FileName;
			}
			else
			{
				text = null;
			}
			return text;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000166E8 File Offset: 0x000148E8
		public string OpenDirectoryDialog()
		{
			return this.OpenDirectoryDialog(Environment.ExpandEnvironmentVariables(Settings.Default.ZipFilePath));
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00016710 File Offset: 0x00014910
		public string OpenDirectoryDialog(string initPath)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
			{
				SelectedPath = initPath,
				Description = LocalizationManager.GetTranslation("PleaseSelectAFolder")
			};
			bool flag = folderBrowserDialog.ShowDialog() == DialogResult.OK;
			string text;
			if (flag)
			{
				text = folderBrowserDialog.SelectedPath;
			}
			else
			{
				text = null;
			}
			return text;
		}

		// Token: 0x0400021A RID: 538
		private Window shellWindow;

		// Token: 0x0200012E RID: 302
		private class Nested
		{
			// Token: 0x040003E6 RID: 998
			internal static readonly DialogManager NestedInstance = new DialogManager();
		}
	}
}
