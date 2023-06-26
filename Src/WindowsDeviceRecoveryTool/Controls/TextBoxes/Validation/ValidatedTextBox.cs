using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls.TextBoxes.Validation
{
	// Token: 0x020000E1 RID: 225
	public class ValidatedTextBox : TextBox, IDisposable
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x0002038A File Offset: 0x0001E58A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0002039C File Offset: 0x0001E59C
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				if (disposing)
				{
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000203C8 File Offset: 0x0001E5C8
		protected override void OnGotFocus(RoutedEventArgs e)
		{
			base.OnGotFocus(e);
			bool flag = this.ValidationSucceeded();
			if (flag)
			{
				this.validatedText = base.Text;
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000203F8 File Offset: 0x0001E5F8
		protected override void OnLostFocus(RoutedEventArgs e)
		{
			base.OnLostFocus(e);
			bool flag = !this.ValidationSucceeded();
			if (flag)
			{
				base.Text = this.validatedText;
			}
			this.ClearValidationErrors();
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00020434 File Offset: 0x0001E634
		private bool ValidationSucceeded()
		{
			return !Validation.GetHasError(this);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00020450 File Offset: 0x0001E650
		private void ClearValidationErrors()
		{
			BindingExpression bindingExpression = base.GetBindingExpression(TextBox.TextProperty);
			bool flag = bindingExpression != null;
			if (flag)
			{
				Validation.ClearInvalid(bindingExpression);
			}
		}

		// Token: 0x04000329 RID: 809
		private bool disposed;

		// Token: 0x0400032A RID: 810
		private string validatedText = string.Empty;
	}
}
