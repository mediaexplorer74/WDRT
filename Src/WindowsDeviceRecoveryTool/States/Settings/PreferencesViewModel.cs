using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Properties;
using Microsoft.WindowsDeviceRecoveryTool.Styles.Assets;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Settings
{
	// Token: 0x02000032 RID: 50
	[Export]
	public class PreferencesViewModel : BaseViewModel, ICanHandle<ThemeColorChangedMessage>, ICanHandle, ICanHandle<LanguageChangedMessage>
	{
		// Token: 0x0600021B RID: 539 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
		[ImportingConstructor]
		public PreferencesViewModel()
		{
			try
			{
				Tracer<PreferencesViewModel>.LogEntry(".ctor");
				this.styles = StyleLogic.Styles;
				base.RaisePropertyChanged<ReadOnlyCollection<DictionaryStyle>>(() => this.Styles);
				this.FillStyles();
				IEnumerable<CultureInfo> enumerable = LocalizationManager.Instance().Languages();
				Collection<ExtendedCultureInfo> collection = ExtendedCultureInfo.CreateLanguagesList(enumerable);
				this.Languages = CollectionViewSource.GetDefaultView(collection);
				this.Languages.SortDescriptions.Add(new SortDescription("ExtendedDisplayName", ListSortDirection.Ascending));
				this.Languages.MoveCurrentTo(LocalizationManager.Instance().CurrentLanguage);
			}
			catch (Exception ex)
			{
				Tracer<PreferencesViewModel>.WriteError(ex);
				throw;
			}
			finally
			{
				Tracer<PreferencesViewModel>.LogExit(".ctor");
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000CD24 File Offset: 0x0000AF24
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000CD48 File Offset: 0x0000AF48
		public CultureInfo SelectedLanguage
		{
			get
			{
				return this.Languages.CurrentItem as CultureInfo;
			}
			set
			{
				this.Languages.MoveCurrentTo(value);
				bool flag = !LocalizationManager.Instance().CurrentLanguage.Equals(value);
				if (flag)
				{
					LocalizationManager.Instance().CurrentLanguage = value;
					ApplicationInfo.CurrentLanguageInRegistry = LocalizationManager.Instance().CurrentLanguage;
					this.StylesView.Refresh();
				}
				this.UpdatePageHeaders();
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000CDD0 File Offset: 0x0000AFD0
		public ThemeStyle SelectedTheme
		{
			get
			{
				return this.GetTheme(Settings.Default.Theme);
			}
			set
			{
				bool flag = value.Name != Settings.Default.Theme;
				if (flag)
				{
					Settings.Default.Theme = value.Name;
					base.RaisePropertyChanged<ThemeStyle>(() => this.SelectedTheme);
					bool flag2 = this.reloadTheme;
					if (flag2)
					{
						StyleLogic.LoadTheme(Settings.GetSelectedThemeFileName());
					}
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000CE58 File Offset: 0x0000B058
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000CE74 File Offset: 0x0000B074
		public string SelectedColor
		{
			get
			{
				return Settings.Default.Style;
			}
			set
			{
				bool flag = value != Settings.Default.Style;
				if (flag)
				{
					Settings.Default.Style = value;
					base.RaisePropertyChanged<string>(() => this.SelectedColor);
					bool flag2 = this.reloadTheme;
					if (flag2)
					{
						StyleLogic.LoadTheme(Settings.GetSelectedThemeFileName());
					}
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000CF0C File Offset: 0x0000B10C
		public List<ThemeStyle> ThemeList
		{
			get
			{
				return this.themeList;
			}
			set
			{
				base.SetValue<List<ThemeStyle>>(() => this.ThemeList, ref this.themeList, value);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000CF4C File Offset: 0x0000B14C
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000CF64 File Offset: 0x0000B164
		public ICollectionView Languages
		{
			get
			{
				return this.languages;
			}
			set
			{
				base.SetValue<ICollectionView>(() => this.Languages, ref this.languages, value);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000CFBC File Offset: 0x0000B1BC
		public ICollectionView StylesView
		{
			get
			{
				return this.stylesView;
			}
			set
			{
				base.SetValue<ICollectionView>(() => this.StylesView, ref this.stylesView, value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000CFFC File Offset: 0x0000B1FC
		public ReadOnlyCollection<DictionaryStyle> Styles
		{
			get
			{
				return new ReadOnlyCollection<DictionaryStyle>(this.styles);
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000D01C File Offset: 0x0000B21C
		private void FillStyles()
		{
			this.StylesView = CollectionViewSource.GetDefaultView(this.Styles);
			this.StylesView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
			this.StylesView.MoveCurrentTo(StyleLogic.CurrentStyle);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000D069 File Offset: 0x0000B269
		public override void OnStarted()
		{
			this.UpdatePageHeaders();
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000D073 File Offset: 0x0000B273
		public override void OnStopped()
		{
			Settings.Default.Save();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000D084 File Offset: 0x0000B284
		public void Handle(ThemeColorChangedMessage message)
		{
			bool flag = false;
			try
			{
				this.reloadTheme = false;
				bool flag2 = !string.IsNullOrEmpty(message.Color) && message.Color != Settings.Default.Style;
				if (flag2)
				{
					this.SelectedColor = message.Color;
					flag = true;
				}
				bool flag3 = !string.IsNullOrEmpty(message.Theme) && message.Theme != Settings.Default.Theme;
				if (flag3)
				{
					this.SelectedTheme = this.GetTheme(message.Theme);
					flag = true;
				}
			}
			finally
			{
				this.reloadTheme = true;
				bool flag4 = flag;
				if (flag4)
				{
					StyleLogic.LoadTheme(Settings.GetSelectedThemeFileName());
				}
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000D148 File Offset: 0x0000B348
		public void Handle(LanguageChangedMessage message)
		{
			bool flag = message.Language != null && !object.Equals(message.Language, LocalizationManager.Instance().CurrentLanguage);
			if (flag)
			{
				this.SelectedLanguage = message.Language;
			}
			this.UpdatePageHeaders();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D194 File Offset: 0x0000B394
		private ThemeStyle GetTheme(string name)
		{
			return this.themeList.FirstOrDefault((ThemeStyle t) => t.Name.Equals(name));
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000D1CA File Offset: 0x0000B3CA
		private void UpdatePageHeaders()
		{
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("Settings"), LocalizationManager.GetTranslation("Preferences")));
		}

		// Token: 0x04000107 RID: 263
		private readonly IList<DictionaryStyle> styles;

		// Token: 0x04000108 RID: 264
		private ICollectionView languages;

		// Token: 0x04000109 RID: 265
		private ICollectionView stylesView;

		// Token: 0x0400010A RID: 266
		private List<ThemeStyle> themeList = new List<ThemeStyle>
		{
			new ThemeStyle("ThemeDark"),
			new ThemeStyle("ThemeLight"),
			new ThemeStyle("ThemeHighContrast")
		};

		// Token: 0x0400010B RID: 267
		private bool reloadTheme = true;
	}
}
