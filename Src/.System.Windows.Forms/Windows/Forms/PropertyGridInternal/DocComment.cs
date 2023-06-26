using System;
using System.Drawing;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020004FB RID: 1275
	internal class DocComment : PropertyGrid.SnappableControl
	{
		// Token: 0x0600539D RID: 21405 RVA: 0x0015E2CC File Offset: 0x0015C4CC
		internal DocComment(PropertyGrid owner)
			: base(owner)
		{
			base.SuspendLayout();
			this.m_labelTitle = new Label();
			this.m_labelTitle.UseMnemonic = false;
			this.m_labelTitle.Cursor = Cursors.Default;
			this.m_labelDesc = new Label();
			this.m_labelDesc.AutoEllipsis = true;
			this.m_labelDesc.Cursor = Cursors.Default;
			this.UpdateTextRenderingEngine();
			base.Controls.Add(this.m_labelTitle);
			base.Controls.Add(this.m_labelDesc);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.cBorder = base.LogicalToDeviceUnits(3);
				this.cydef = base.LogicalToDeviceUnits(59);
			}
			base.Size = new Size(0, this.cydef);
			this.Text = SR.GetString("PBRSDocCommentPaneTitle");
			base.SetStyle(ControlStyles.Selectable, false);
			base.ResumeLayout(false);
		}

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x0600539E RID: 21406 RVA: 0x0015E3D4 File Offset: 0x0015C5D4
		// (set) Token: 0x0600539F RID: 21407 RVA: 0x0015E3E9 File Offset: 0x0015C5E9
		public virtual int Lines
		{
			get
			{
				this.UpdateUIWithFont();
				return base.Height / this.lineHeight;
			}
			set
			{
				this.UpdateUIWithFont();
				base.Size = new Size(base.Width, 1 + value * this.lineHeight);
			}
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x0015E40C File Offset: 0x0015C60C
		public override int GetOptimalHeight(int width)
		{
			this.UpdateUIWithFont();
			int num = this.m_labelTitle.Size.Height;
			if (this.ownerGrid.IsHandleCreated && !base.IsHandleCreated)
			{
				base.CreateControl();
			}
			Graphics graphics = this.m_labelDesc.CreateGraphicsInternal();
			SizeF sizeF = PropertyGrid.MeasureTextHelper.MeasureText(this.ownerGrid, graphics, this.m_labelTitle.Text, this.Font, width);
			Size size = Size.Ceiling(sizeF);
			graphics.Dispose();
			int num2 = (DpiHelper.EnableDpiChangedHighDpiImprovements ? base.LogicalToDeviceUnits(2) : 2);
			num += size.Height * 2 + num2;
			return Math.Max(num + 2 * num2, DpiHelper.EnableDpiChangedHighDpiImprovements ? base.LogicalToDeviceUnits(59) : 59);
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void LayoutWindow()
		{
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x0015E4C7 File Offset: 0x0015C6C7
		protected override void OnFontChanged(EventArgs e)
		{
			this.needUpdateUIWithFont = true;
			base.PerformLayout();
			base.OnFontChanged(e);
		}

		// Token: 0x060053A3 RID: 21411 RVA: 0x0015E4DD File Offset: 0x0015C6DD
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.UpdateUIWithFont();
			this.SetChildLabelsBounds();
			this.m_labelDesc.Text = this.fullDesc;
			this.m_labelDesc.AccessibleName = this.fullDesc;
			base.OnLayout(e);
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x0015E514 File Offset: 0x0015C714
		protected override void OnResize(EventArgs e)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			if (!this.rect.IsEmpty && clientRectangle.Width > this.rect.Width)
			{
				Rectangle rectangle = new Rectangle(this.rect.Width - 1, 0, clientRectangle.Width - this.rect.Width + 1, this.rect.Height);
				base.Invalidate(rectangle);
			}
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.lineHeight = this.Font.Height + base.LogicalToDeviceUnits(2);
				if (base.ClientRectangle.Width != this.rect.Width || base.ClientRectangle.Height != this.rect.Height)
				{
					this.m_labelTitle.Location = new Point(this.cBorder, this.cBorder);
					this.m_labelDesc.Location = new Point(this.cBorder, this.cBorder + this.lineHeight);
					this.SetChildLabelsBounds();
				}
			}
			this.rect = clientRectangle;
			base.OnResize(e);
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x0015E634 File Offset: 0x0015C834
		private void SetChildLabelsBounds()
		{
			Size clientSize = base.ClientSize;
			clientSize.Width = Math.Max(0, clientSize.Width - 2 * this.cBorder);
			clientSize.Height = Math.Max(0, clientSize.Height - 2 * this.cBorder);
			this.m_labelTitle.SetBounds(this.m_labelTitle.Top, this.m_labelTitle.Left, clientSize.Width, Math.Min(this.lineHeight, clientSize.Height), BoundsSpecified.Size);
			this.m_labelDesc.SetBounds(this.m_labelDesc.Top, this.m_labelDesc.Left, clientSize.Width, Math.Max(0, clientSize.Height - this.lineHeight - (DpiHelper.EnableDpiChangedHighDpiImprovements ? base.LogicalToDeviceUnits(1) : 1)), BoundsSpecified.Size);
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x0015E70E File Offset: 0x0015C90E
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.UpdateUIWithFont();
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x0015E71D File Offset: 0x0015C91D
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.cBorder = base.LogicalToDeviceUnits(3);
				this.cydef = base.LogicalToDeviceUnits(59);
			}
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x0015E74C File Offset: 0x0015C94C
		public virtual void SetComment(string title, string desc)
		{
			if (this.m_labelDesc.Text != title)
			{
				this.m_labelTitle.Text = title;
			}
			if (desc != this.fullDesc)
			{
				this.fullDesc = desc;
				this.m_labelDesc.Text = this.fullDesc;
				this.m_labelDesc.AccessibleName = this.fullDesc;
			}
		}

		// Token: 0x060053A9 RID: 21417 RVA: 0x0015E7B0 File Offset: 0x0015C9B0
		public override int SnapHeightRequest(int cyNew)
		{
			this.UpdateUIWithFont();
			int num = Math.Max(2, cyNew / this.lineHeight);
			return 1 + num * this.lineHeight;
		}

		// Token: 0x060053AA RID: 21418 RVA: 0x0015E7DC File Offset: 0x0015C9DC
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new DocCommentAccessibleObject(this, this.ownerGrid);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x060053AB RID: 21419 RVA: 0x000A83A1 File Offset: 0x000A65A1
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3;
			}
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x0015E7F8 File Offset: 0x0015C9F8
		internal void UpdateTextRenderingEngine()
		{
			this.m_labelTitle.UseCompatibleTextRendering = this.ownerGrid.UseCompatibleTextRendering;
			this.m_labelDesc.UseCompatibleTextRendering = this.ownerGrid.UseCompatibleTextRendering;
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x0015E828 File Offset: 0x0015CA28
		private void UpdateUIWithFont()
		{
			if (base.IsHandleCreated && this.needUpdateUIWithFont)
			{
				try
				{
					this.m_labelTitle.Font = new Font(this.Font, FontStyle.Bold);
				}
				catch
				{
				}
				this.lineHeight = this.Font.Height + 2;
				this.m_labelTitle.Location = new Point(this.cBorder, this.cBorder);
				this.m_labelDesc.Location = new Point(this.cBorder, this.cBorder + this.lineHeight);
				this.needUpdateUIWithFont = false;
				base.PerformLayout();
			}
		}

		// Token: 0x040036B4 RID: 14004
		private Label m_labelTitle;

		// Token: 0x040036B5 RID: 14005
		private Label m_labelDesc;

		// Token: 0x040036B6 RID: 14006
		private string fullDesc;

		// Token: 0x040036B7 RID: 14007
		protected int lineHeight;

		// Token: 0x040036B8 RID: 14008
		private bool needUpdateUIWithFont = true;

		// Token: 0x040036B9 RID: 14009
		protected const int CBORDER = 3;

		// Token: 0x040036BA RID: 14010
		protected const int CXDEF = 0;

		// Token: 0x040036BB RID: 14011
		protected const int CYDEF = 59;

		// Token: 0x040036BC RID: 14012
		protected const int MIN_LINES = 2;

		// Token: 0x040036BD RID: 14013
		private int cydef = 59;

		// Token: 0x040036BE RID: 14014
		private int cBorder = 3;

		// Token: 0x040036BF RID: 14015
		internal Rectangle rect = Rectangle.Empty;
	}
}
