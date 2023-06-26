using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004BC RID: 1212
	internal abstract class CheckableControlBaseAdapter : ButtonBaseAdapter
	{
		// Token: 0x06004FC6 RID: 20422 RVA: 0x0014946C File Offset: 0x0014766C
		internal CheckableControlBaseAdapter(ButtonBase control)
			: base(control)
		{
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06004FC7 RID: 20423 RVA: 0x0014A8E5 File Offset: 0x00148AE5
		protected ButtonBaseAdapter ButtonAdapter
		{
			get
			{
				if (this.buttonAdapter == null)
				{
					this.buttonAdapter = this.CreateButtonAdapter();
				}
				return this.buttonAdapter;
			}
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x0014A904 File Offset: 0x00148B04
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (this.Appearance == Appearance.Button)
			{
				return this.ButtonAdapter.GetPreferredSizeCore(proposedSize);
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

		// Token: 0x06004FC9 RID: 20425
		protected abstract ButtonBaseAdapter CreateButtonAdapter();

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06004FCA RID: 20426 RVA: 0x0014A984 File Offset: 0x00148B84
		private Appearance Appearance
		{
			get
			{
				CheckBox checkBox = base.Control as CheckBox;
				if (checkBox != null)
				{
					return checkBox.Appearance;
				}
				RadioButton radioButton = base.Control as RadioButton;
				if (radioButton != null)
				{
					return radioButton.Appearance;
				}
				return Appearance.Normal;
			}
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x0014A9C0 File Offset: 0x00148BC0
		internal override ButtonBaseAdapter.LayoutOptions CommonLayout()
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = base.CommonLayout();
			layoutOptions.growBorderBy1PxWhenDefault = false;
			layoutOptions.borderSize = 0;
			layoutOptions.paddingSize = 0;
			layoutOptions.maxFocus = false;
			layoutOptions.focusOddEvenFixup = true;
			layoutOptions.checkSize = 13;
			return layoutOptions;
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x0014AA00 File Offset: 0x00148C00
		internal double GetDpiScaleRatio(Graphics g)
		{
			return CheckableControlBaseAdapter.GetDpiScaleRatio(g, base.Control);
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x0014AA0E File Offset: 0x00148C0E
		internal static double GetDpiScaleRatio(Graphics g, Control control)
		{
			if (DpiHelper.EnableDpiChangedMessageHandling && control != null && control.IsHandleCreated)
			{
				return (double)control.deviceDpi / 96.0;
			}
			if (g == null)
			{
				return 1.0;
			}
			return (double)(g.DpiX / 96f);
		}

		// Token: 0x04003466 RID: 13414
		private const int standardCheckSize = 13;

		// Token: 0x04003467 RID: 13415
		private ButtonBaseAdapter buttonAdapter;
	}
}
