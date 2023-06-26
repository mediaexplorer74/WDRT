using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004C4 RID: 1220
	internal class RadioButtonStandardAdapter : RadioButtonBaseAdapter
	{
		// Token: 0x0600500D RID: 20493 RVA: 0x0014C502 File Offset: 0x0014A702
		internal RadioButtonStandardAdapter(ButtonBase control)
			: base(control)
		{
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x0014CA60 File Offset: 0x0014AC60
		internal override void PaintUp(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintUp(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			ButtonBaseAdapter.ColorData colorData = base.PaintRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.Layout(e).Layout();
			base.PaintButtonBackground(e, base.Control.ClientRectangle, null);
			base.PaintImage(e, layoutData);
			base.DrawCheckBox(e, layoutData);
			base.AdjustFocusRectangle(layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x0014CAF2 File Offset: 0x0014ACF2
		internal override void PaintDown(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintDown(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			this.PaintUp(e, state);
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x0014CB28 File Offset: 0x0014AD28
		internal override void PaintOver(PaintEventArgs e, CheckState state)
		{
			if (base.Control.Appearance == Appearance.Button)
			{
				this.ButtonAdapter.PaintOver(e, base.Control.Checked ? CheckState.Checked : CheckState.Unchecked);
				return;
			}
			this.PaintUp(e, state);
		}

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06005011 RID: 20497 RVA: 0x0014BC40 File Offset: 0x00149E40
		private new ButtonStandardAdapter ButtonAdapter
		{
			get
			{
				return (ButtonStandardAdapter)base.ButtonAdapter;
			}
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x0014CB5E File Offset: 0x0014AD5E
		protected override ButtonBaseAdapter CreateButtonAdapter()
		{
			return new ButtonStandardAdapter(base.Control);
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x0014CB6C File Offset: 0x0014AD6C
		protected override ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = this.CommonLayout();
			layoutOptions.hintTextUp = false;
			layoutOptions.everettButtonCompat = !Application.RenderWithVisualStyles;
			if (Application.RenderWithVisualStyles)
			{
				ButtonBase control = base.Control;
				using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
				{
					layoutOptions.checkSize = RadioButtonRenderer.GetGlyphSize(graphics, RadioButtonRenderer.ConvertFromButtonState(base.GetState(), control.MouseIsOver), control.HandleInternal).Width;
					return layoutOptions;
				}
			}
			layoutOptions.checkSize = (int)((double)layoutOptions.checkSize * base.GetDpiScaleRatio(e.Graphics));
			return layoutOptions;
		}
	}
}
