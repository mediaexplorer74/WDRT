using System;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls.TextBoxes
{
	// Token: 0x020000DD RID: 221
	public static class PasswordHelper
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x0001FF65 File Offset: 0x0001E165
		public static void SetAttach(DependencyObject dp, bool value)
		{
			dp.SetValue(PasswordHelper.AttachProperty, value);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001FF7C File Offset: 0x0001E17C
		public static bool GetAttach(DependencyObject dp)
		{
			return (bool)dp.GetValue(PasswordHelper.AttachProperty);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
		public static string GetPassword(DependencyObject dp)
		{
			return (string)dp.GetValue(PasswordHelper.PasswordProperty);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001FFC2 File Offset: 0x0001E1C2
		public static void SetPassword(DependencyObject dp, string value)
		{
			dp.SetValue(PasswordHelper.PasswordProperty, value);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001FFD4 File Offset: 0x0001E1D4
		private static bool GetIsUpdating(DependencyObject dp)
		{
			return (bool)dp.GetValue(PasswordHelper.IsUpdatingProperty);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001FFF6 File Offset: 0x0001E1F6
		private static void SetIsUpdating(DependencyObject dp, bool value)
		{
			dp.SetValue(PasswordHelper.IsUpdatingProperty, value);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0002000C File Offset: 0x0001E20C
		private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			PasswordBox passwordBox = (PasswordBox)sender;
			bool flag = sender == null || !PasswordHelper.GetAttach(sender);
			if (!flag)
			{
				passwordBox.PasswordChanged -= PasswordHelper.PasswordChanged;
				bool flag2 = !PasswordHelper.GetIsUpdating(passwordBox);
				if (flag2)
				{
					passwordBox.Password = (string)e.NewValue;
				}
				passwordBox.PasswordChanged += PasswordHelper.PasswordChanged;
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00020080 File Offset: 0x0001E280
		private static void Attach(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			PasswordBox passwordBox = (PasswordBox)sender;
			bool flag = passwordBox == null;
			if (!flag)
			{
				bool flag2 = (bool)e.OldValue;
				if (flag2)
				{
					passwordBox.PasswordChanged -= PasswordHelper.PasswordChanged;
				}
				bool flag3 = (bool)e.NewValue;
				if (flag3)
				{
					passwordBox.PasswordChanged += PasswordHelper.PasswordChanged;
				}
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000200EC File Offset: 0x0001E2EC
		private static void PasswordChanged(object sender, RoutedEventArgs e)
		{
			PasswordBox passwordBox = (PasswordBox)sender;
			PasswordHelper.SetIsUpdating(passwordBox, true);
			PasswordHelper.SetPassword(passwordBox, passwordBox.Password);
			PasswordHelper.SetIsUpdating(passwordBox, false);
		}

		// Token: 0x04000326 RID: 806
		public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordHelper), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(PasswordHelper.OnPasswordPropertyChanged)));

		// Token: 0x04000327 RID: 807
		public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordHelper), new PropertyMetadata(false, new PropertyChangedCallback(PasswordHelper.Attach)));

		// Token: 0x04000328 RID: 808
		private static readonly DependencyProperty IsUpdatingProperty = DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(PasswordHelper), new PropertyMetadata(false));
	}
}
