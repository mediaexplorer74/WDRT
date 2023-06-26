using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000505 RID: 1285
	internal partial class GridErrorDlg : Form
	{
		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x06005483 RID: 21635 RVA: 0x001619E8 File Offset: 0x0015FBE8
		public bool DetailsButtonExpanded
		{
			get
			{
				return this.detailsButtonExpanded;
			}
		}

		// Token: 0x17001443 RID: 5187
		// (set) Token: 0x06005484 RID: 21636 RVA: 0x001619F0 File Offset: 0x0015FBF0
		public string Details
		{
			set
			{
				this.details.Text = value;
			}
		}

		// Token: 0x17001444 RID: 5188
		// (set) Token: 0x06005485 RID: 21637 RVA: 0x001619FE File Offset: 0x0015FBFE
		public string Message
		{
			set
			{
				this.lblMessage.Text = value;
			}
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x00161A0C File Offset: 0x0015FC0C
		public GridErrorDlg(PropertyGrid owner)
		{
			this.ownerGrid = owner;
			this.expandImage = new Bitmap(typeof(ThreadExceptionDialog), "down.bmp");
			this.expandImage.MakeTransparent();
			if (DpiHelper.IsScalingRequired)
			{
				DpiHelper.ScaleBitmapLogicalToDevice(ref this.expandImage, 0);
			}
			this.collapseImage = new Bitmap(typeof(ThreadExceptionDialog), "up.bmp");
			this.collapseImage.MakeTransparent();
			if (DpiHelper.IsScalingRequired)
			{
				DpiHelper.ScaleBitmapLogicalToDevice(ref this.collapseImage, 0);
			}
			this.InitializeComponent();
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				if (control.SupportsUseCompatibleTextRendering)
				{
					control.UseCompatibleTextRenderingInt = this.ownerGrid.UseCompatibleTextRendering;
				}
			}
			this.pictureBox.Image = SystemIcons.Warning.ToBitmap();
			this.detailsBtn.Text = " " + SR.GetString("ExDlgShowDetails");
			this.details.AccessibleName = SR.GetString("ExDlgDetailsText");
			this.okBtn.Text = SR.GetString("ExDlgOk");
			this.cancelBtn.Text = SR.GetString("ExDlgCancel");
			this.detailsBtn.Image = this.expandImage;
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x00161B80 File Offset: 0x0015FD80
		private void DetailsClick(object sender, EventArgs devent)
		{
			int num = this.details.Height + 8;
			if (this.details.Visible)
			{
				this.detailsBtn.Image = this.expandImage;
				this.detailsButtonExpanded = false;
				base.Height -= num;
			}
			else
			{
				this.detailsBtn.Image = this.collapseImage;
				this.detailsButtonExpanded = true;
				this.details.Width = this.overarchingTableLayoutPanel.Width - this.details.Margin.Horizontal;
				base.Height += num;
			}
			this.details.Visible = !this.details.Visible;
			if (AccessibilityImprovements.Level1)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
				base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
				this.details.TabStop = !this.details.TabStop;
				if (this.details.Visible)
				{
					this.details.Focus();
				}
			}
		}

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x06005488 RID: 21640 RVA: 0x000F750D File Offset: 0x000F570D
		private static bool IsRTLResources
		{
			get
			{
				return SR.GetString("RTL") != "RTL_False";
			}
		}

		// Token: 0x0600548A RID: 21642 RVA: 0x0016242B File Offset: 0x0016062B
		private void OnButtonClick(object s, EventArgs e)
		{
			base.DialogResult = ((Button)s).DialogResult;
			base.Close();
		}

		// Token: 0x0600548B RID: 21643 RVA: 0x00162444 File Offset: 0x00160644
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (base.Visible)
			{
				using (Graphics graphics = base.CreateGraphics())
				{
					int num = (int)Math.Ceiling((double)PropertyGrid.MeasureTextHelper.MeasureText(this.ownerGrid, graphics, this.detailsBtn.Text, this.detailsBtn.Font).Width);
					num += this.detailsBtn.Image.Width;
					this.detailsBtn.Width = (int)Math.Ceiling((double)((float)num * (this.ownerGrid.UseCompatibleTextRendering ? 1.15f : 1.4f)));
					this.detailsBtn.Height = this.okBtn.Height;
				}
				int x = this.details.Location.X;
				int num2 = this.detailsBtn.Location.Y + this.detailsBtn.Height + this.detailsBtn.Margin.Bottom;
				Control control = this.detailsBtn.Parent;
				while (control != null && !(control is Form))
				{
					num2 += control.Location.Y;
					control = control.Parent;
				}
				this.details.Location = new Point(x, num2);
				if (this.details.Visible)
				{
					this.DetailsClick(this.details, EventArgs.Empty);
				}
			}
			this.okBtn.Focus();
		}

		// Token: 0x04003708 RID: 14088
		private Bitmap expandImage;

		// Token: 0x04003709 RID: 14089
		private Bitmap collapseImage;

		// Token: 0x0400370A RID: 14090
		private PropertyGrid ownerGrid;

		// Token: 0x0400370B RID: 14091
		private bool detailsButtonExpanded;
	}
}
