namespace System.Windows.Forms
{
	// Token: 0x020002ED RID: 749
	[global::System.Security.Permissions.SecurityPermission(global::System.Security.Permissions.SecurityAction.LinkDemand, Flags = global::System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
	internal sealed partial class MdiWindowDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x06002F90 RID: 12176 RVA: 0x000D6934 File Offset: 0x000D4B34
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::System.Windows.Forms.MdiWindowDialog));
			this.itemList = new global::System.Windows.Forms.ListBox();
			this.okButton = new global::System.Windows.Forms.Button();
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.okCancelTableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.okCancelTableLayoutPanel.SuspendLayout();
			this.itemList.DoubleClick += new global::System.EventHandler(this.ItemList_doubleClick);
			this.itemList.SelectedIndexChanged += new global::System.EventHandler(this.ItemList_selectedIndexChanged);
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.itemList, "itemList");
			this.itemList.FormattingEnabled = true;
			this.itemList.Name = "itemList";
			componentResourceManager.ApplyResources(this.okButton, "okButton");
			this.okButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.okButton.Margin = new global::System.Windows.Forms.Padding(0, 0, 3, 0);
			this.okButton.Name = "okButton";
			componentResourceManager.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Margin = new global::System.Windows.Forms.Padding(3, 0, 0, 0);
			this.cancelButton.Name = "cancelButton";
			componentResourceManager.ApplyResources(this.okCancelTableLayoutPanel, "okCancelTableLayoutPanel");
			this.okCancelTableLayoutPanel.ColumnCount = 2;
			this.okCancelTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.okCancelTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.okCancelTableLayoutPanel.Controls.Add(this.okButton, 0, 0);
			this.okCancelTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
			this.okCancelTableLayoutPanel.Name = "okCancelTableLayoutPanel";
			this.okCancelTableLayoutPanel.RowCount = 1;
			this.okCancelTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.okCancelTableLayoutPanel);
			base.Controls.Add(this.itemList);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MdiWindowDialog";
			base.ShowIcon = false;
			this.okCancelTableLayoutPanel.ResumeLayout(false);
			this.okCancelTableLayoutPanel.PerformLayout();
			base.AcceptButton = this.okButton;
			base.CancelButton = this.cancelButton;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04001399 RID: 5017
		private global::System.Windows.Forms.ListBox itemList;

		// Token: 0x0400139A RID: 5018
		private global::System.Windows.Forms.Button okButton;

		// Token: 0x0400139B RID: 5019
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x0400139C RID: 5020
		private global::System.Windows.Forms.TableLayoutPanel okCancelTableLayoutPanel;
	}
}
