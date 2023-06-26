using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Behaviors
{
	// Token: 0x02000011 RID: 17
	public static class ComboBoxBehavior
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000028DC File Offset: 0x00000ADC
		public static bool GetOpenDropDownOnEnter(ComboBox comboBox)
		{
			return (bool)comboBox.GetValue(ComboBoxBehavior.OpenDropDownOnEnterProperty);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000028FE File Offset: 0x00000AFE
		public static void SetOpenDropDownOnEnter(ComboBox comboBox, bool value)
		{
			comboBox.SetValue(ComboBoxBehavior.OpenDropDownOnEnterProperty, value);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002914 File Offset: 0x00000B14
		public static bool GetMoveFocusOnTab(ComboBox comboBox)
		{
			return (bool)comboBox.GetValue(ComboBoxBehavior.MoveFocusOnTabProperty);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002936 File Offset: 0x00000B36
		public static void SetMoveFocusOnTab(ComboBox comboBox, bool value)
		{
			comboBox.SetValue(ComboBoxBehavior.MoveFocusOnTabProperty, value);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000294C File Offset: 0x00000B4C
		private static void OpenDropDownOnEnter_PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			ComboBox comboBox = obj as ComboBox;
			bool flag = comboBox == null;
			if (!flag)
			{
				bool flag2 = (bool)args.NewValue;
				bool flag3 = flag2;
				if (flag3)
				{
					comboBox.PreviewKeyDown += ComboBoxBehavior.OpenDropDownOnEnter_ComboBox_PreviewKeyDown;
				}
				else
				{
					comboBox.PreviewKeyDown -= ComboBoxBehavior.OpenDropDownOnEnter_ComboBox_PreviewKeyDown;
				}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029AC File Offset: 0x00000BAC
		private static void MoveFocusOnTab_PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			ComboBox comboBox = obj as ComboBox;
			bool flag = comboBox == null;
			if (!flag)
			{
				bool flag2 = (bool)args.NewValue;
				bool flag3 = flag2;
				if (flag3)
				{
					comboBox.KeyDown += ComboBoxBehavior.MoveFocusOnTab_ComboBox_KeyDown;
				}
				else
				{
					comboBox.KeyDown -= ComboBoxBehavior.MoveFocusOnTab_ComboBox_KeyDown;
				}
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A0C File Offset: 0x00000C0C
		private static void OpenDropDownOnEnter_ComboBox_PreviewKeyDown(object sender, KeyEventArgs args)
		{
			bool flag = args.Key != Key.Return;
			if (!flag)
			{
				ComboBox comboBox = (ComboBox)sender;
				bool flag2 = !comboBox.IsKeyboardFocused;
				if (!flag2)
				{
					comboBox.IsDropDownOpen = true;
					args.Handled = true;
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A54 File Offset: 0x00000C54
		private static void MoveFocusOnTab_ComboBox_KeyDown(object sender, KeyEventArgs args)
		{
			bool flag = args.Key != Key.Tab;
			if (!flag)
			{
				ComboBox comboBox = (ComboBox)sender;
				bool flag2 = !comboBox.IsDropDownOpen || comboBox.IsEditable;
				if (!flag2)
				{
					ComboBoxItem comboBoxItem = args.OriginalSource as ComboBoxItem;
					bool flag3 = comboBoxItem == null;
					if (!flag3)
					{
						ItemContainerGenerator itemContainerGenerator = comboBox.ItemContainerGenerator;
						int num = itemContainerGenerator.IndexFromContainer(comboBoxItem);
						KeyboardDevice keyboardDevice = args.KeyboardDevice;
						ModifierKeys modifiers = keyboardDevice.Modifiers;
						FocusNavigationDirection focusNavigationDirection = ((!modifiers.HasFlag(ModifierKeys.Shift)) ? FocusNavigationDirection.Next : FocusNavigationDirection.Previous);
						TraversalRequest traversalRequest = new TraversalRequest(focusNavigationDirection);
						comboBox.MoveFocus(traversalRequest);
						comboBox.SelectedIndex = num;
						comboBox.IsDropDownOpen = false;
						args.Handled = true;
					}
				}
			}
		}

		// Token: 0x0400000A RID: 10
		public static readonly DependencyProperty OpenDropDownOnEnterProperty = DependencyProperty.RegisterAttached("OpenDropDownOnEnter", typeof(bool), typeof(ComboBoxBehavior), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ComboBoxBehavior.OpenDropDownOnEnter_PropertyChanged)));

		// Token: 0x0400000B RID: 11
		public static readonly DependencyProperty MoveFocusOnTabProperty = DependencyProperty.RegisterAttached("MoveFocusOnTab", typeof(bool), typeof(ComboBoxBehavior), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ComboBoxBehavior.MoveFocusOnTab_PropertyChanged)));
	}
}
