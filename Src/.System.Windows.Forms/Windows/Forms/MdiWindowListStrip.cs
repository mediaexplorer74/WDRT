using System;
using System.Globalization;
using System.Security;

namespace System.Windows.Forms
{
	// Token: 0x020002EB RID: 747
	internal class MdiWindowListStrip : MenuStrip
	{
		// Token: 0x06002F84 RID: 12164 RVA: 0x000D64BF File Offset: 0x000D46BF
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.mdiParent = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06002F85 RID: 12165 RVA: 0x000D64D4 File Offset: 0x000D46D4
		internal ToolStripMenuItem MergeItem
		{
			get
			{
				if (this.mergeItem == null)
				{
					this.mergeItem = new ToolStripMenuItem();
					this.mergeItem.MergeAction = MergeAction.MatchOnly;
				}
				if (this.mergeItem.Owner == null)
				{
					this.Items.Add(this.mergeItem);
				}
				return this.mergeItem;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06002F86 RID: 12166 RVA: 0x000D6525 File Offset: 0x000D4725
		// (set) Token: 0x06002F87 RID: 12167 RVA: 0x000D652D File Offset: 0x000D472D
		internal MenuStrip MergedMenu
		{
			get
			{
				return this.mergedMenu;
			}
			set
			{
				this.mergedMenu = value;
			}
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000D6538 File Offset: 0x000D4738
		public void PopulateItems(Form mdiParent, ToolStripMenuItem mdiMergeItem, bool includeSeparator)
		{
			this.mdiParent = mdiParent;
			base.SuspendLayout();
			this.MergeItem.DropDown.SuspendLayout();
			try
			{
				ToolStripMenuItem toolStripMenuItem = this.MergeItem;
				toolStripMenuItem.DropDownItems.Clear();
				toolStripMenuItem.Text = mdiMergeItem.Text;
				Form[] mdiChildren = mdiParent.MdiChildren;
				if (mdiChildren != null && mdiChildren.Length != 0)
				{
					if (includeSeparator)
					{
						ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
						toolStripSeparator.MergeAction = MergeAction.Append;
						toolStripSeparator.MergeIndex = -1;
						toolStripMenuItem.DropDownItems.Add(toolStripSeparator);
					}
					Form activeMdiChild = mdiParent.ActiveMdiChild;
					int num = 0;
					int num2 = 1;
					int num3 = 0;
					bool flag = false;
					for (int i = 0; i < mdiChildren.Length; i++)
					{
						if (mdiChildren[i].Visible && mdiChildren[i].CloseReason == CloseReason.None)
						{
							num++;
							if ((flag && num3 < 9) || (!flag && num3 < 8) || mdiChildren[i].Equals(activeMdiChild))
							{
								string text = WindowsFormsUtils.EscapeTextWithAmpersands(mdiParent.MdiChildren[i].Text);
								text = ((text == null) ? string.Empty : text);
								ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(mdiParent.MdiChildren[i]);
								toolStripMenuItem2.Text = string.Format(CultureInfo.CurrentCulture, "&{0} {1}", new object[] { num2, text });
								toolStripMenuItem2.MergeAction = MergeAction.Append;
								toolStripMenuItem2.MergeIndex = num2;
								toolStripMenuItem2.Click += this.OnWindowListItemClick;
								if (mdiChildren[i].Equals(activeMdiChild))
								{
									toolStripMenuItem2.Checked = true;
									flag = true;
								}
								num2++;
								num3++;
								toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
							}
						}
					}
					if (num > 9)
					{
						ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
						toolStripMenuItem3.Text = SR.GetString("MDIMenuMoreWindows");
						toolStripMenuItem3.Click += this.OnMoreWindowsMenuItemClick;
						toolStripMenuItem3.MergeAction = MergeAction.Append;
						toolStripMenuItem.DropDownItems.Add(toolStripMenuItem3);
					}
				}
			}
			finally
			{
				base.ResumeLayout(false);
				this.MergeItem.DropDown.ResumeLayout(false);
			}
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000D675C File Offset: 0x000D495C
		private void OnMoreWindowsMenuItemClick(object sender, EventArgs e)
		{
			Form[] mdiChildren = this.mdiParent.MdiChildren;
			if (mdiChildren != null)
			{
				IntSecurity.AllWindows.Assert();
				try
				{
					using (MdiWindowDialog mdiWindowDialog = new MdiWindowDialog())
					{
						mdiWindowDialog.SetItems(this.mdiParent.ActiveMdiChild, mdiChildren);
						DialogResult dialogResult = mdiWindowDialog.ShowDialog();
						if (dialogResult == DialogResult.OK)
						{
							mdiWindowDialog.ActiveChildForm.Activate();
							if (mdiWindowDialog.ActiveChildForm.ActiveControl != null && !mdiWindowDialog.ActiveChildForm.ActiveControl.Focused)
							{
								mdiWindowDialog.ActiveChildForm.ActiveControl.Focus();
							}
						}
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000D6810 File Offset: 0x000D4A10
		private void OnWindowListItemClick(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				Form mdiForm = toolStripMenuItem.MdiForm;
				if (mdiForm != null)
				{
					IntSecurity.ModifyFocus.Assert();
					try
					{
						mdiForm.Activate();
						if (mdiForm.ActiveControl != null && !mdiForm.ActiveControl.Focused)
						{
							mdiForm.ActiveControl.Focus();
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
		}

		// Token: 0x04001391 RID: 5009
		private Form mdiParent;

		// Token: 0x04001392 RID: 5010
		private ToolStripMenuItem mergeItem;

		// Token: 0x04001393 RID: 5011
		private MenuStrip mergedMenu;
	}
}
