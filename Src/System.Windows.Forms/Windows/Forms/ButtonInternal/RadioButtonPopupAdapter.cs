using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004C3 RID: 1219
	internal class RadioButtonPopupAdapter : RadioButtonFlatAdapter
	{
		// Token: 0x06005007 RID: 20487 RVA: 0x0014C781 File Offset: 0x0014A981
		internal RadioButtonPopupAdapter(ButtonBase control)
			: base(control)
		{
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x0014C78C File Offset: 0x0014A98C
		internal override void PaintUp(PaintEventArgs e, CheckState state)
		{
			Graphics graphics = e.Graphics;
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonPopupAdapter buttonPopupAdapter = new ButtonPopupAdapter(base.Control);
				buttonPopupAdapter.PaintUp(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.Layout(e).Layout();
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			base.DrawCheckBackgroundFlat(e, layoutData.checkBounds, colorData.buttonShadow, colorData.options.highContrast ? colorData.buttonFace : colorData.highlight);
			base.DrawCheckOnly(e, layoutData, colorData.windowText, colorData.highlight, true);
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x0014C868 File Offset: 0x0014AA68
		internal override void PaintOver(PaintEventArgs e, CheckState state)
		{
			Graphics graphics = e.Graphics;
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonPopupAdapter buttonPopupAdapter = new ButtonPopupAdapter(base.Control);
				buttonPopupAdapter.PaintOver(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.Layout(e).Layout();
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			Color color = ((colorData.options.highContrast && AccessibilityImprovements.Level1) ? colorData.buttonFace : colorData.highlight);
			base.DrawCheckBackground3DLite(e, layoutData.checkBounds, colorData.windowText, color, colorData, true);
			base.DrawCheckOnly(e, layoutData, colorData.windowText, colorData.highlight, true);
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x0014C950 File Offset: 0x0014AB50
		internal override void PaintDown(PaintEventArgs e, CheckState state)
		{
			Graphics graphics = e.Graphics;
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonPopupAdapter buttonPopupAdapter = new ButtonPopupAdapter(base.Control);
				buttonPopupAdapter.PaintDown(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.Layout(e).Layout();
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			base.DrawCheckBackground3DLite(e, layoutData.checkBounds, colorData.windowText, colorData.highlight, colorData, true);
			base.DrawCheckOnly(e, layoutData, colorData.buttonShadow, colorData.highlight, true);
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x0014CA18 File Offset: 0x0014AC18
		protected override ButtonBaseAdapter CreateButtonAdapter()
		{
			return new ButtonPopupAdapter(base.Control);
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x0014CA28 File Offset: 0x0014AC28
		protected override ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = base.Layout(e);
			if (!base.Control.MouseIsDown && !base.Control.MouseIsOver)
			{
				layoutOptions.shadowedText = true;
			}
			return layoutOptions;
		}
	}
}
