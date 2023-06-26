using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004BF RID: 1215
	internal class CheckBoxPopupAdapter : CheckBoxBaseAdapter
	{
		// Token: 0x06004FE4 RID: 20452 RVA: 0x0014B34B File Offset: 0x0014954B
		internal CheckBoxPopupAdapter(ButtonBase control)
			: base(control)
		{
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x0014B5B8 File Offset: 0x001497B8
		internal override void PaintUp(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonPopupAdapter buttonPopupAdapter = new ButtonPopupAdapter(base.Control);
				buttonPopupAdapter.PaintUp(e, base.Control.CheckState);
				return;
			}
			Graphics graphics = e.Graphics;
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.PaintPopupLayout(e, false).Layout();
			Region clip = e.Graphics.Clip;
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			base.DrawCheckBackground(e, layoutData.checkBounds, colorData.windowText, colorData.options.highContrast ? colorData.buttonFace : colorData.highlight, true, colorData);
			ButtonBaseAdapter.DrawFlatBorder(e.Graphics, layoutData.checkBounds, (colorData.options.highContrast && !base.Control.Enabled && AccessibilityImprovements.Level1) ? colorData.windowFrame : colorData.buttonShadow);
			base.DrawCheckOnly(e, layoutData, colorData, colorData.windowText, colorData.highlight);
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x0014B6E0 File Offset: 0x001498E0
		internal override void PaintOver(PaintEventArgs e, CheckState state)
		{
			Graphics graphics = e.Graphics;
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonPopupAdapter buttonPopupAdapter = new ButtonPopupAdapter(base.Control);
				buttonPopupAdapter.PaintOver(e, base.Control.CheckState);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.PaintPopupLayout(e, true).Layout();
			Region clip = e.Graphics.Clip;
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			base.DrawCheckBackground(e, layoutData.checkBounds, colorData.windowText, colorData.options.highContrast ? colorData.buttonFace : colorData.highlight, true, colorData);
			CheckBoxBaseAdapter.DrawPopupBorder(graphics, layoutData.checkBounds, colorData);
			base.DrawCheckOnly(e, layoutData, colorData, colorData.windowText, colorData.highlight);
			if (!AccessibilityImprovements.Level2 || !string.IsNullOrEmpty(base.Control.Text))
			{
				e.Graphics.Clip = clip;
				e.Graphics.ExcludeClip(layoutData.checkArea);
			}
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x0014B80C File Offset: 0x00149A0C
		internal override void PaintDown(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonPopupAdapter buttonPopupAdapter = new ButtonPopupAdapter(base.Control);
				buttonPopupAdapter.PaintDown(e, base.Control.CheckState);
				return;
			}
			Graphics graphics = e.Graphics;
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.PaintPopupLayout(e, true).Layout();
			Region clip = e.Graphics.Clip;
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			base.DrawCheckBackground(e, layoutData.checkBounds, colorData.windowText, colorData.buttonFace, true, colorData);
			CheckBoxBaseAdapter.DrawPopupBorder(graphics, layoutData.checkBounds, colorData);
			base.DrawCheckOnly(e, layoutData, colorData, colorData.windowText, colorData.buttonFace);
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x0014B8EA File Offset: 0x00149AEA
		protected override ButtonBaseAdapter CreateButtonAdapter()
		{
			return new ButtonPopupAdapter(base.Control);
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x0014B8F8 File Offset: 0x00149AF8
		protected override ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e)
		{
			return this.PaintPopupLayout(e, true);
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x0014B910 File Offset: 0x00149B10
		internal static ButtonBaseAdapter.LayoutOptions PaintPopupLayout(Graphics g, bool show3D, int checkSize, Rectangle clientRectangle, Padding padding, bool isDefault, Font font, string text, bool enabled, ContentAlignment textAlign, RightToLeft rtl, Control control = null)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = ButtonBaseAdapter.CommonLayout(clientRectangle, padding, isDefault, font, text, enabled, textAlign, rtl);
			layoutOptions.shadowedText = false;
			if (show3D)
			{
				layoutOptions.checkSize = (int)((double)checkSize * CheckableControlBaseAdapter.GetDpiScaleRatio(g, control) + 1.0);
			}
			else
			{
				layoutOptions.checkSize = (int)((double)checkSize * CheckableControlBaseAdapter.GetDpiScaleRatio(g, control));
				layoutOptions.checkPaddingSize = 1;
			}
			return layoutOptions;
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x0014B974 File Offset: 0x00149B74
		private ButtonBaseAdapter.LayoutOptions PaintPopupLayout(PaintEventArgs e, bool show3D)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = this.CommonLayout();
			layoutOptions.shadowedText = false;
			if (show3D)
			{
				layoutOptions.checkSize = (int)(11.0 * base.GetDpiScaleRatio(e.Graphics) + 1.0);
			}
			else
			{
				layoutOptions.checkSize = (int)(11.0 * base.GetDpiScaleRatio(e.Graphics));
				layoutOptions.checkPaddingSize = 1;
			}
			return layoutOptions;
		}
	}
}
