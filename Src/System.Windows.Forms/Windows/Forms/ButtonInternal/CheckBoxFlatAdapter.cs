using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004BE RID: 1214
	internal class CheckBoxFlatAdapter : CheckBoxBaseAdapter
	{
		// Token: 0x06004FDC RID: 20444 RVA: 0x0014B34B File Offset: 0x0014954B
		internal CheckBoxFlatAdapter(ButtonBase control)
			: base(control)
		{
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x0014B354 File Offset: 0x00149554
		internal override void PaintDown(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintDown(e, base.Control.CheckState);
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

		// Token: 0x06004FDE RID: 20446 RVA: 0x0014B3DC File Offset: 0x001495DC
		internal override void PaintOver(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintOver(e, base.Control.CheckState);
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

		// Token: 0x06004FDF RID: 20447 RVA: 0x0014B464 File Offset: 0x00149664
		internal override void PaintUp(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintUp(e, base.Control.CheckState);
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

		// Token: 0x06004FE0 RID: 20448 RVA: 0x0014B4EC File Offset: 0x001496EC
		private void PaintFlatWorker(PaintEventArgs e, Color checkColor, Color checkBackground, Color checkBorder, ButtonBaseAdapter.ColorData colors)
		{
			Graphics graphics = e.Graphics;
			ButtonBaseAdapter.LayoutData layoutData = this.Layout(e).Layout();
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			base.DrawCheckFlat(e, layoutData, checkColor, colors.options.highContrast ? colors.buttonFace : checkBackground, checkBorder, colors);
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colors, checkColor, true);
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x06004FE1 RID: 20449 RVA: 0x0014B560 File Offset: 0x00149760
		private new ButtonFlatAdapter ButtonAdapter
		{
			get
			{
				return (ButtonFlatAdapter)base.ButtonAdapter;
			}
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x0014B56D File Offset: 0x0014976D
		protected override ButtonBaseAdapter CreateButtonAdapter()
		{
			return new ButtonFlatAdapter(base.Control);
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x0014B57C File Offset: 0x0014977C
		protected override ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = this.CommonLayout();
			layoutOptions.checkSize = (int)(11.0 * base.GetDpiScaleRatio(e.Graphics));
			layoutOptions.shadowedText = false;
			return layoutOptions;
		}
	}
}
