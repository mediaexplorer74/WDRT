using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Microsoft.WindowsDeviceRecoveryTool.Localization
{
	// Token: 0x02000003 RID: 3
	public class LocalizationManager
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000010 RID: 16 RVA: 0x00002478 File Offset: 0x00000678
		// (remove) Token: 0x06000011 RID: 17 RVA: 0x000024B0 File Offset: 0x000006B0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler LanguageChanged;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000024E8 File Offset: 0x000006E8
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002510 File Offset: 0x00000710
		public CultureInfo CurrentLanguage
		{
			get
			{
				return Application.Current.Dispatcher.Thread.CurrentUICulture;
			}
			set
			{
				bool flag = value != Application.Current.Dispatcher.Thread.CurrentUICulture;
				if (flag)
				{
					Application.Current.Dispatcher.Thread.CurrentUICulture = value;
					this.OnLanguageChanged();
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000255B File Offset: 0x0000075B
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002563 File Offset: 0x00000763
		private ResourceLocalizationProvider TranslationProvider { get; set; }

		// Token: 0x06000016 RID: 22 RVA: 0x0000256C File Offset: 0x0000076C
		public static string GetTranslation(string key)
		{
			return LocalizationManager.Instance().Translate(key).ToString();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002590 File Offset: 0x00000790
		public static LocalizationManager Instance()
		{
			bool flag = LocalizationManager.localizationManager == null;
			if (flag)
			{
				LocalizationManager.localizationManager = new LocalizationManager
				{
					TranslationProvider = new ResourceLocalizationProvider("Microsoft.WindowsDeviceRecoveryTool.Localization.Resources.Resources", Assembly.GetExecutingAssembly())
				};
			}
			return LocalizationManager.localizationManager;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025D8 File Offset: 0x000007D8
		public IEnumerable<CultureInfo> Languages()
		{
			bool flag = this.TranslationProvider != null;
			IEnumerable<CultureInfo> enumerable;
			if (flag)
			{
				enumerable = this.TranslationProvider.Languages();
			}
			else
			{
				enumerable = Enumerable.Empty<CultureInfo>();
			}
			return enumerable;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000260C File Offset: 0x0000080C
		public object Translate(string key)
		{
			bool flag = this.TranslationProvider != null;
			if (flag)
			{
				object obj = this.TranslationProvider.Translate(key, this.CurrentLanguage);
				bool flag2 = obj != null;
				if (flag2)
				{
					return obj.ToString();
				}
				string text = key.Split(new char[] { '_' }).FirstOrDefault<string>();
				obj = this.TranslationProvider.Translate(string.Format("{0}_UnknownError", text), this.CurrentLanguage);
				bool flag3 = obj != null;
				if (flag3)
				{
					return obj.ToString();
				}
			}
			return "KEY: " + key + " NOT FOUND";
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026B0 File Offset: 0x000008B0
		public object EnglishResource(string key)
		{
			bool flag = this.TranslationProvider != null;
			if (flag)
			{
				object obj = this.TranslationProvider.Translate(key, new CultureInfo("en-US"));
				bool flag2 = obj != null;
				if (flag2)
				{
					return obj.ToString();
				}
			}
			return "KEY: " + key + " NOT FOUND";
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000270C File Offset: 0x0000090C
		private void OnLanguageChanged()
		{
			EventHandler languageChanged = this.LanguageChanged;
			bool flag = languageChanged != null;
			if (flag)
			{
				languageChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000008 RID: 8
		private static LocalizationManager localizationManager;
	}
}
