using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.WindowsDeviceRecoveryTool.Controls.TextBoxes.Validation;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls.TextBoxes
{
	// Token: 0x020000DC RID: 220
	public class NumericTextBox : ValidatedTextBox
	{
		// Token: 0x06000702 RID: 1794 RVA: 0x0001FCAE File Offset: 0x0001DEAE
		public NumericTextBox()
		{
			this.MinValue = int.MinValue;
			this.MaxValue = int.MaxValue;
			DataObject.AddPastingHandler(this, new DataObjectPastingEventHandler(this.OnPaste));
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001FCE3 File Offset: 0x0001DEE3
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x0001FCEB File Offset: 0x0001DEEB
		public int MinValue { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001FCF4 File Offset: 0x0001DEF4
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x0001FCFC File Offset: 0x0001DEFC
		public int MaxValue { get; set; }

		// Token: 0x06000707 RID: 1799 RVA: 0x0001FD08 File Offset: 0x0001DF08
		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			bool flag = e.Key == Key.Space;
			if (flag)
			{
				e.Handled = true;
			}
			else
			{
				base.OnPreviewKeyDown(e);
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001FD38 File Offset: 0x0001DF38
		protected override void OnTextChanged(TextChangedEventArgs e)
		{
			base.OnTextChanged(e);
			base.Text = NumericTextBox.RemoveTrailingZeros(base.Text);
			bool flag = !this.IsTextAllowed(base.Text);
			if (flag)
			{
				base.Text = string.Empty;
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001FD84 File Offset: 0x0001DF84
		protected override void OnPreviewTextInput(TextCompositionEventArgs e)
		{
			bool flag = e == null;
			if (!flag)
			{
				string text = new string(base.Text.ToCharArray());
				text = text.Remove(text.IndexOf(base.SelectedText, StringComparison.Ordinal), base.SelectedText.Length);
				e.Handled = !this.IsTextAllowed(text.Insert(base.CaretIndex, e.Text));
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001FDF0 File Offset: 0x0001DFF0
		private static string RemoveTrailingZeros(string text)
		{
			string text2 = new string(text.ToCharArray());
			while (text2.StartsWith("0", StringComparison.Ordinal) && text2.Length > 1)
			{
				text2 = text2.Remove(0, 1);
			}
			return text2;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001FE3C File Offset: 0x0001E03C
		private void OnPaste(object sender, DataObjectPastingEventArgs e)
		{
			bool flag = e == null;
			if (!flag)
			{
				bool dataPresent = e.DataObject.GetDataPresent(typeof(string));
				if (dataPresent)
				{
					string text = new string(base.Text.ToCharArray());
					text = new string(text.ToCharArray());
					text = text.Remove(text.IndexOf(base.SelectedText, StringComparison.Ordinal), base.SelectedText.Length);
					text = text.Insert(base.CaretIndex, (string)e.DataObject.GetData(typeof(string)));
					text = NumericTextBox.RemoveTrailingZeros(text);
					bool flag2 = !this.IsTextAllowed(text);
					if (flag2)
					{
						e.CancelCommand();
					}
				}
				else
				{
					e.CancelCommand();
				}
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001FF04 File Offset: 0x0001E104
		private bool IsTextAllowed(string text)
		{
			bool flag = text.StartsWith("0", StringComparison.Ordinal) && text.Length > 1;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				int num;
				bool flag3 = !int.TryParse(text, out num);
				flag2 = !flag3 && this.MinValue <= num && num <= this.MaxValue;
			}
			return flag2;
		}
	}
}
