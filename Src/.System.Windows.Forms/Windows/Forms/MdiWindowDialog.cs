using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020002ED RID: 749
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed partial class MdiWindowDialog : Form
	{
		// Token: 0x06002F8B RID: 12171 RVA: 0x000D687C File Offset: 0x000D4A7C
		public MdiWindowDialog()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06002F8C RID: 12172 RVA: 0x000D688A File Offset: 0x000D4A8A
		public Form ActiveChildForm
		{
			get
			{
				return this.active;
			}
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000D6894 File Offset: 0x000D4A94
		public void SetItems(Form active, Form[] all)
		{
			int num = 0;
			for (int i = 0; i < all.Length; i++)
			{
				if (all[i].Visible)
				{
					int num2 = this.itemList.Items.Add(new MdiWindowDialog.ListItem(all[i]));
					if (all[i].Equals(active))
					{
						num = num2;
					}
				}
			}
			this.active = active;
			this.itemList.SelectedIndex = num;
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x000D68F4 File Offset: 0x000D4AF4
		private void ItemList_doubleClick(object source, EventArgs e)
		{
			this.okButton.PerformClick();
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000D6904 File Offset: 0x000D4B04
		private void ItemList_selectedIndexChanged(object source, EventArgs e)
		{
			MdiWindowDialog.ListItem listItem = (MdiWindowDialog.ListItem)this.itemList.SelectedItem;
			if (listItem != null)
			{
				this.active = listItem.form;
			}
		}

		// Token: 0x0400139D RID: 5021
		private Form active;

		// Token: 0x020006D3 RID: 1747
		private class ListItem
		{
			// Token: 0x06006AB4 RID: 27316 RVA: 0x0018AECE File Offset: 0x001890CE
			public ListItem(Form f)
			{
				this.form = f;
			}

			// Token: 0x06006AB5 RID: 27317 RVA: 0x0018AEDD File Offset: 0x001890DD
			public override string ToString()
			{
				return this.form.Text;
			}

			// Token: 0x04003B44 RID: 15172
			public Form form;
		}
	}
}
