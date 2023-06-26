using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.WindowsDeviceRecoveryTool.Styles.Assets;

namespace Microsoft.WindowsDeviceRecoveryTool.Properties
{
	// Token: 0x0200007B RID: 123
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.4.0.0")]
	public sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x0001619A File Offset: 0x0001439A
		public Settings()
		{
			base.SettingChanging += this.SettingChangingEventHandler;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000161B8 File Offset: 0x000143B8
		private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
		{
			bool flag = e.SettingName.Equals("Style");
			if (flag)
			{
				StyleLogic.CurrentStyle = StyleLogic.GetStyle((string)e.NewValue);
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000161F4 File Offset: 0x000143F4
		public static string GetThemeFileName(string name)
		{
			string text;
			if (!(name == "ThemeDark"))
			{
				if (!(name == "ThemeLight"))
				{
					if (!(name == "ThemeHighContrast"))
					{
						text = "LightTheme.xaml";
					}
					else
					{
						text = "HighContrastTheme.xaml";
					}
				}
				else
				{
					text = "LightTheme.xaml";
				}
			}
			else
			{
				text = "DarkTheme.xaml";
			}
			return text;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00016250 File Offset: 0x00014450
		public static string GetSelectedThemeFileName()
		{
			return Settings.GetThemeFileName(Settings.Default.Theme);
		}
	}
}
