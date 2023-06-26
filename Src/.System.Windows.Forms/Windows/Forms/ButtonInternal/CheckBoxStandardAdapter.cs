using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004C0 RID: 1216
	internal sealed class CheckBoxStandardAdapter : CheckBoxBaseAdapter
	{
		// Token: 0x06004FEC RID: 20460 RVA: 0x0014B34B File Offset: 0x0014954B
		internal CheckBoxStandardAdapter(ButtonBase control)
			: base(control)
		{
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x0014B9E0 File Offset: 0x00149BE0
		internal override void PaintUp(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintUp(e, base.Control.CheckState);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.Layout(e).Layout();
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			if (!layoutData.options.everettButtonCompat)
			{
				layoutData.textBounds.Offset(-1, -1);
			}
			layoutData.imageBounds.Offset(-1, -1);
			base.AdjustFocusRectangle(layoutData);
			if (!AccessibilityImprovements.Level2 || !string.IsNullOrEmpty(base.Control.Text))
			{
				int num = layoutData.focus.X & 1;
				if (!Application.RenderWithVisualStyles)
				{
					num = 1 - num;
				}
				layoutData.focus.Offset(-(num + 1), -2);
				layoutData.focus.Width = layoutData.textBounds.Width + layoutData.imageBounds.Width - 1;
				layoutData.focus.Intersect(layoutData.textBounds);
				if (layoutData.options.textAlign != (ContentAlignment)273 && layoutData.options.useCompatibleTextRendering && layoutData.options.font.Italic)
				{
					ButtonBaseAdapter.LayoutData layoutData2 = layoutData;
					layoutData2.focus.Width = layoutData2.focus.Width + 2;
				}
			}
			base.PaintImage(e, layoutData);
			base.DrawCheckBox(e, layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x06004FEE RID: 20462 RVA: 0x0014BB52 File Offset: 0x00149D52
		internal override void PaintDown(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintDown(e, base.Control.CheckState);
				return;
			}
			this.PaintUp(e, state);
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x0014BB82 File Offset: 0x00149D82
		internal override void PaintOver(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintOver(e, base.Control.CheckState);
				return;
			}
			this.PaintUp(e, state);
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x0014BBB4 File Offset: 0x00149DB4
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				ButtonStandardAdapter buttonStandardAdapter = new ButtonStandardAdapter(base.Control);
				return buttonStandardAdapter.GetPreferredSizeCore(proposedSize);
			}
			Size preferredSizeCore;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				using (PaintEventArgs paintEventArgs = new PaintEventArgs(graphics, default(Rectangle)))
				{
					ButtonBaseAdapter.LayoutOptions layoutOptions = this.Layout(paintEventArgs);
					preferredSizeCore = layoutOptions.GetPreferredSizeCore(proposedSize);
				}
			}
			return preferredSizeCore;
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x06004FF1 RID: 20465 RVA: 0x0014BC40 File Offset: 0x00149E40
		private new ButtonStandardAdapter ButtonAdapter
		{
			get
			{
				return (ButtonStandardAdapter)base.ButtonAdapter;
			}
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x0014BC4D File Offset: 0x00149E4D
		protected override ButtonBaseAdapter CreateButtonAdapter()
		{
			return new ButtonStandardAdapter(base.Control);
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x0014BC5C File Offset: 0x00149E5C
		protected override ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = this.CommonLayout();
			layoutOptions.checkPaddingSize = 1;
			layoutOptions.everettButtonCompat = !Application.RenderWithVisualStyles;
			if (Application.RenderWithVisualStyles)
			{
				using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
				{
					layoutOptions.checkSize = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxRenderer.ConvertFromButtonState(base.GetState(), true, base.Control.MouseIsOver), base.Control.HandleInternal).Width;
					return layoutOptions;
				}
			}
			if (DpiHelper.EnableDpiChangedMessageHandling)
			{
				layoutOptions.checkSize = base.Control.LogicalToDeviceUnits(layoutOptions.checkSize);
			}
			else
			{
				layoutOptions.checkSize = (int)((double)layoutOptions.checkSize * base.GetDpiScaleRatio(e.Graphics));
			}
			return layoutOptions;
		}
	}
}
