using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004C2 RID: 1218
	internal class RadioButtonFlatAdapter : RadioButtonBaseAdapter
	{
		// Token: 0x06005000 RID: 20480 RVA: 0x0014C502 File Offset: 0x0014A702
		internal RadioButtonFlatAdapter(ButtonBase control)
			: base(control)
		{
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x0014C50C File Offset: 0x0014A70C
		internal override void PaintDown(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonFlatAdapter buttonFlatAdapter = new ButtonFlatAdapter(base.Control);
				buttonFlatAdapter.PaintDown(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintFlatRender(e.Graphics).Calculate();
			if (base.Control.Enabled)
			{
				this.PaintFlatWorker(e, colorData.windowText, colorData.highlight, colorData.windowFrame, colorData);
				return;
			}
			this.PaintFlatWorker(e, colorData.buttonShadow, colorData.buttonFace, colorData.buttonShadow, colorData);
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x0014C5A0 File Offset: 0x0014A7A0
		internal override void PaintOver(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonFlatAdapter buttonFlatAdapter = new ButtonFlatAdapter(base.Control);
				buttonFlatAdapter.PaintOver(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintFlatRender(e.Graphics).Calculate();
			if (base.Control.Enabled)
			{
				this.PaintFlatWorker(e, colorData.windowText, colorData.lowHighlight, colorData.windowFrame, colorData);
				return;
			}
			this.PaintFlatWorker(e, colorData.buttonShadow, colorData.buttonFace, colorData.buttonShadow, colorData);
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x0014C634 File Offset: 0x0014A834
		internal override void PaintUp(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonFlatAdapter buttonFlatAdapter = new ButtonFlatAdapter(base.Control);
				buttonFlatAdapter.PaintUp(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintFlatRender(e.Graphics).Calculate();
			if (base.Control.Enabled)
			{
				this.PaintFlatWorker(e, colorData.windowText, colorData.highlight, colorData.windowFrame, colorData);
				return;
			}
			this.PaintFlatWorker(e, colorData.buttonShadow, colorData.buttonFace, colorData.buttonShadow, colorData);
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x0014C6C8 File Offset: 0x0014A8C8
		private void PaintFlatWorker(PaintEventArgs e, Color checkColor, Color checkBackground, Color checkBorder, ButtonBaseAdapter.ColorData colors)
		{
			Graphics graphics = e.Graphics;
			ButtonBaseAdapter.LayoutData layoutData = this.Layout(e).Layout();
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			base.DrawCheckFlat(e, layoutData, checkColor, colors.options.highContrast ? colors.buttonFace : checkBackground, checkBorder);
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colors, checkColor, true);
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x0014C73A File Offset: 0x0014A93A
		protected override ButtonBaseAdapter CreateButtonAdapter()
		{
			return new ButtonFlatAdapter(base.Control);
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x0014C748 File Offset: 0x0014A948
		protected override ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = this.CommonLayout();
			layoutOptions.checkSize = (int)(12.0 * base.GetDpiScaleRatio(e.Graphics));
			layoutOptions.shadowedText = false;
			return layoutOptions;
		}

		// Token: 0x0400346D RID: 13421
		protected const int flatCheckSize = 12;
	}
}
