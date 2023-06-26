using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Assets
{
	// Token: 0x0200001B RID: 27
	public static class StyleLogic
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000395C File Offset: 0x00001B5C
		public static ReadOnlyCollection<DictionaryStyle> Styles
		{
			get
			{
				return StyleLogic.MetroStyles;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003974 File Offset: 0x00001B74
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000039A8 File Offset: 0x00001BA8
		public static DictionaryStyle CurrentStyle
		{
			get
			{
				bool flag = StyleLogic.currentStyle == null;
				DictionaryStyle dictionaryStyle;
				if (flag)
				{
					dictionaryStyle = StyleLogic.MetroStyles.FirstOrDefault<DictionaryStyle>();
				}
				else
				{
					dictionaryStyle = StyleLogic.currentStyle;
				}
				return dictionaryStyle;
			}
			set
			{
				bool flag = value == null || !StyleLogic.MetroStyles.Contains(value);
				if (flag)
				{
					StyleLogic.currentStyle = StyleLogic.MetroStyles.FirstOrDefault<DictionaryStyle>();
				}
				else
				{
					StyleLogic.currentStyle = value;
				}
				bool flag2 = StyleLogic.currentStyle != null;
				if (flag2)
				{
					Uri uri = new Uri("Microsoft.WindowsDeviceRecoveryTool.Styles;component/Colors/" + StyleLogic.currentStyle.FileName, UriKind.Relative);
					ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
					Application.Current.Resources.MergedDictionaries.Insert(0, resourceDictionary);
					Application.Current.Resources.MergedDictionaries.Remove(Application.Current.Resources.MergedDictionaries[1]);
				}
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003A60 File Offset: 0x00001C60
		public static DictionaryStyle GetStyle(string styleName)
		{
			string value = styleName.ToLower();
			return StyleLogic.MetroStyles.First((DictionaryStyle style) => style.Name.ToLower().Equals(value));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003A9C File Offset: 0x00001C9C
		public static void RestoreStyle(string styleName)
		{
			DictionaryStyle dictionaryStyle = StyleLogic.FindStyleByName(styleName);
			StyleLogic.CurrentStyle = dictionaryStyle;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public static DictionaryStyle FindStyleByName(string name)
		{
			foreach (DictionaryStyle dictionaryStyle in StyleLogic.Styles)
			{
				bool flag = string.CompareOrdinal(dictionaryStyle.Name.ToLower(), name.ToLower()) == 0;
				if (flag)
				{
					return dictionaryStyle;
				}
			}
			return StyleLogic.CurrentStyle;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003B30 File Offset: 0x00001D30
		public static void LoadTheme(string name)
		{
			Uri uri = new Uri("Microsoft.WindowsDeviceRecoveryTool.Styles;component/" + name, UriKind.Relative);
			ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
			Application.Current.Resources.MergedDictionaries.Insert(1, resourceDictionary);
			Application.Current.Resources.MergedDictionaries.Remove(Application.Current.Resources.MergedDictionaries[2]);
			StyleLogic.ResetStyles();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003BA4 File Offset: 0x00001DA4
		private static void ResetStyles()
		{
			ResourceDictionary resourceDictionary = Application.LoadComponent(new Uri("/Microsoft.WindowsDeviceRecoveryTool.Styles;component/SystemStyles.xaml", UriKind.Relative)) as ResourceDictionary;
			Application.Current.Resources.MergedDictionaries.Insert(2, resourceDictionary);
			Application.Current.Resources.MergedDictionaries.Remove(Application.Current.Resources.MergedDictionaries[3]);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003C0C File Offset: 0x00001E0C
		public static bool IfStyleExists(string name)
		{
			return StyleLogic.MetroStyles.Any((DictionaryStyle s) => string.Compare(s.Name, name, StringComparison.Ordinal) == 0);
		}

		// Token: 0x04000024 RID: 36
		private static readonly ReadOnlyCollection<DictionaryStyle> MetroStyles = new ReadOnlyCollection<DictionaryStyle>(new List<DictionaryStyle>
		{
			new DictionaryStyle("AccentColorEmerald", "EmeraldDictionary.xaml", Color.FromArgb(0, 138, 0)),
			new DictionaryStyle("AccentColorCobalt", "CobaltDictionary.xaml", Color.FromArgb(0, 80, 239)),
			new DictionaryStyle("AccentColorCrimson", "CrimsonDictionary.xaml", Color.FromArgb(162, 0, 37)),
			new DictionaryStyle("AccentColorMauve", "MauveDictionary.xaml", Color.FromArgb(118, 96, 138)),
			new DictionaryStyle("AccentColorSienna", "SiennaDictionary.xaml", Color.FromArgb(160, 82, 45)),
			new DictionaryStyle("AccentColorIndigo", "IndigoDictionary.xaml", Color.FromArgb(106, 0, 255)),
			new DictionaryStyle("AccentColorBlue", "BlueDictionary.xaml", Color.FromArgb(255, 0, 66, 127)),
			new DictionaryStyle("AccentColorBlack", "BlackDictionary.xaml", Color.Black)
		});

		// Token: 0x04000025 RID: 37
		private static DictionaryStyle currentStyle;
	}
}
