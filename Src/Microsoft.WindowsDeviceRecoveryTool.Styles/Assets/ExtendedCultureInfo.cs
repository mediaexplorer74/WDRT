using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.WindowsDeviceRecoveryTool.Localization;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Assets
{
	// Token: 0x02000019 RID: 25
	public class ExtendedCultureInfo : CultureInfo, INotifyPropertyChanged
	{
		// Token: 0x06000080 RID: 128 RVA: 0x0000365F File Offset: 0x0000185F
		public ExtendedCultureInfo(CultureInfo cultureInfo)
			: base(cultureInfo.LCID)
		{
			ExtendedCultureInfo.AllInstance.Add(this);
			LocalizationManager.Instance().LanguageChanged += this.OnLanguageChanged;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000081 RID: 129 RVA: 0x00003694 File Offset: 0x00001894
		// (remove) Token: 0x06000082 RID: 130 RVA: 0x000036CC File Offset: 0x000018CC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003704 File Offset: 0x00001904
		public string ExtendedDisplayName
		{
			get
			{
				CultureInfo cultureInfo = (this.IsCountryCreated(this) ? this : this.Parent);
				string text = cultureInfo.DisplayName.Replace(" (", ", ").Replace(")", string.Empty);
				string text2 = cultureInfo.NativeName.Replace("(", ", ").Replace(")", string.Empty);
				bool flag = string.CompareOrdinal(cultureInfo.DisplayName, cultureInfo.NativeName) == 0;
				string text3;
				if (flag)
				{
					text3 = text;
				}
				else
				{
					bool flag2 = LocalizationManager.Instance().CurrentLanguage.TextInfo.IsRightToLeft && !this.TextInfo.IsRightToLeft;
					if (flag2)
					{
						text3 = string.Format("({0} ({1}", text2, text);
					}
					else
					{
						text3 = string.Format("{0} ({1})", text2, text);
					}
				}
				return text3;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000037E0 File Offset: 0x000019E0
		private bool IsCountryCreated(CultureInfo language)
		{
			return ExtendedCultureInfo.AllInstance.Any((ExtendedCultureInfo i) => i != language && string.CompareOrdinal(i.TwoLetterISOLanguageName, language.TwoLetterISOLanguageName) == 0);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003818 File Offset: 0x00001A18
		public static Collection<ExtendedCultureInfo> CreateLanguagesList(IEnumerable<CultureInfo> baseList)
		{
			List<ExtendedCultureInfo> list = baseList.Select((CultureInfo language) => new ExtendedCultureInfo(language)).ToList<ExtendedCultureInfo>();
			return new Collection<ExtendedCultureInfo>(list);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000385B File Offset: 0x00001A5B
		private void OnLanguageChanged(object sender, EventArgs eventArgs)
		{
			this.OnPropertyChanged("ExtendedDisplayName");
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000386C File Offset: 0x00001A6C
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			bool flag = propertyChanged != null;
			if (flag)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400001E RID: 30
		private static readonly List<ExtendedCultureInfo> AllInstance = new List<ExtendedCultureInfo>();
	}
}
