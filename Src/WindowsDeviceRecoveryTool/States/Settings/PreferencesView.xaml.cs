using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Styles.Assets;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Settings
{
	// Token: 0x02000031 RID: 49
	[Export]
	[Region(new string[] { "SettingsMainArea" })]
	public partial class PreferencesView : StackPanel
	{
		// Token: 0x06000211 RID: 529 RVA: 0x0000C933 File Offset: 0x0000AB33
		public PreferencesView()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000C944 File Offset: 0x0000AB44
		private static IEnumerable<T> FindVisualChildrenWithName<T>(DependencyObject depObj, string name) where T : DependencyObject
		{
			bool flag = depObj != null;
			if (flag)
			{
				int num;
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i = num + 1)
				{
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					FrameworkElement frameworkElement = child as FrameworkElement;
					bool flag2 = frameworkElement != null && child is T && frameworkElement.Name == name;
					if (flag2)
					{
						yield return (T)((object)child);
					}
					foreach (T childOfChild in PreferencesView.FindVisualChildrenWithName<T>(child, name))
					{
						yield return childOfChild;
						childOfChild = default(T);
					}
					IEnumerator<T> enumerator = null;
					child = null;
					frameworkElement = null;
					num = i;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000C95B File Offset: 0x0000AB5B
		private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
		{
			bool flag = depObj != null;
			if (flag)
			{
				int num;
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i = num + 1)
				{
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					bool flag2 = child is T;
					if (flag2)
					{
						yield return (T)((object)child);
					}
					foreach (T childOfChild in PreferencesView.FindVisualChildren<T>(child))
					{
						yield return childOfChild;
						childOfChild = default(T);
					}
					IEnumerator<T> enumerator = null;
					child = null;
					num = i;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private void OnLanguagesComboBoxPreviewKeyDown(object sender, KeyEventArgs e)
		{
			bool isRepeat = e.IsRepeat;
			if (isRepeat)
			{
				e.Handled = true;
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000C990 File Offset: 0x0000AB90
		private void OnLanguagesComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ThemeStyle themeStyle = this.ThemesComboBox.SelectedItem as ThemeStyle;
			bool flag = themeStyle != null;
			if (flag)
			{
				this.UpdateComboBoxText(this.ThemesComboBox, themeStyle.LocalizedName);
			}
			DictionaryStyle dictionaryStyle = this.ColorsComboBox.SelectedItem as DictionaryStyle;
			bool flag2 = dictionaryStyle != null;
			if (flag2)
			{
				this.UpdateComboBoxText(this.ColorsComboBox, dictionaryStyle.LocalizedName);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000C9FC File Offset: 0x0000ABFC
		private void UpdateComboBoxText(ComboBox comboBox, string text)
		{
			IEnumerable<ContentPresenter> enumerable = PreferencesView.FindVisualChildrenWithName<ContentPresenter>(comboBox, "ContentPresenter");
			IList<ContentPresenter> list = (enumerable as IList<ContentPresenter>) ?? enumerable.ToList<ContentPresenter>();
			bool flag = list.FirstOrDefault<ContentPresenter>() != null;
			if (flag)
			{
				IEnumerable<TextBlock> enumerable2 = PreferencesView.FindVisualChildren<TextBlock>(list.FirstOrDefault<ContentPresenter>());
				TextBlock[] array = (enumerable2 as TextBlock[]) ?? enumerable2.ToArray<TextBlock>();
				bool flag2 = array.FirstOrDefault<TextBlock>() != null;
				if (flag2)
				{
					comboBox.Text = text;
					array.First<TextBlock>().Text = text;
				}
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		private void OnStyleComboBoxSelectionChanged(object sender, SelectionChangedEventArgs args)
		{
			this.lastFocusedElement = Keyboard.FocusedElement;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000CA8C File Offset: 0x0000AC8C
		private void OnPreferencesViewLoaded(object sender, RoutedEventArgs args)
		{
			bool flag = this.lastFocusedElement == null;
			if (!flag)
			{
				Keyboard.Focus(this.lastFocusedElement);
				this.lastFocusedElement = null;
			}
		}

		// Token: 0x04000101 RID: 257
		private IInputElement lastFocusedElement;
	}
}
