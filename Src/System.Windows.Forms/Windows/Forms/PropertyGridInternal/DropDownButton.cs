using System;
using System.Drawing;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020004FD RID: 1277
	internal sealed class DropDownButton : Button
	{
		// Token: 0x060053B1 RID: 21425 RVA: 0x0015E945 File Offset: 0x0015CB45
		public DropDownButton()
		{
			base.SetStyle(ControlStyles.Selectable, true);
			this.SetAccessibleName();
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x060053B2 RID: 21426 RVA: 0x0015E95F File Offset: 0x0015CB5F
		// (set) Token: 0x060053B3 RID: 21427 RVA: 0x0015E967 File Offset: 0x0015CB67
		public bool IgnoreMouse
		{
			get
			{
				return this.ignoreMouse;
			}
			set
			{
				this.ignoreMouse = value;
			}
		}

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x060053B4 RID: 21428 RVA: 0x000A83A1 File Offset: 0x000A65A1
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3;
			}
		}

		// Token: 0x17001402 RID: 5122
		// (set) Token: 0x060053B5 RID: 21429 RVA: 0x0015E970 File Offset: 0x0015CB70
		public bool UseComboBoxTheme
		{
			set
			{
				if (this.useComboBoxTheme != value)
				{
					this.useComboBoxTheme = value;
					if (AccessibilityImprovements.Level1)
					{
						this.SetAccessibleName();
					}
					base.Invalidate();
				}
			}
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x0015E995 File Offset: 0x0015CB95
		protected override void OnClick(EventArgs e)
		{
			if (!this.IgnoreMouse)
			{
				base.OnClick(e);
			}
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x0015E9A6 File Offset: 0x0015CBA6
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (!this.IgnoreMouse)
			{
				base.OnMouseUp(e);
			}
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x0015E9B7 File Offset: 0x0015CBB7
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (!this.IgnoreMouse)
			{
				base.OnMouseDown(e);
			}
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x0015E9C8 File Offset: 0x0015CBC8
		protected override void OnPaint(PaintEventArgs pevent)
		{
			base.OnPaint(pevent);
			if (Application.RenderWithVisualStyles & this.useComboBoxTheme)
			{
				ComboBoxState comboBoxState = ComboBoxState.Normal;
				if (base.MouseIsDown)
				{
					comboBoxState = ComboBoxState.Pressed;
				}
				else if (base.MouseIsOver)
				{
					comboBoxState = ComboBoxState.Hot;
				}
				Rectangle rectangle = new Rectangle(0, 0, base.Width, base.Height);
				if (comboBoxState == ComboBoxState.Normal)
				{
					pevent.Graphics.FillRectangle(SystemBrushes.Window, rectangle);
				}
				if (!DpiHelper.EnableDpiChangedHighDpiImprovements)
				{
					ComboBoxRenderer.DrawDropDownButton(pevent.Graphics, rectangle, comboBoxState);
				}
				else
				{
					ComboBoxRenderer.DrawDropDownButtonForHandle(pevent.Graphics, rectangle, comboBoxState, base.HandleInternal);
				}
				if (AccessibilityImprovements.Level1 && this.Focused)
				{
					rectangle.Inflate(-1, -1);
					ControlPaint.DrawFocusRectangle(pevent.Graphics, rectangle, this.ForeColor, this.BackColor);
				}
			}
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x0015EA88 File Offset: 0x0015CC88
		internal void PerformButtonClick()
		{
			if (base.Visible && base.Enabled)
			{
				this.OnClick(EventArgs.Empty);
			}
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x0015EAA5 File Offset: 0x0015CCA5
		private void SetAccessibleName()
		{
			if (AccessibilityImprovements.Level1 && this.useComboBoxTheme)
			{
				base.AccessibleName = SR.GetString("PropertyGridDropDownButtonComboBoxAccessibleName");
				return;
			}
			base.AccessibleName = SR.GetString("PropertyGridDropDownButtonAccessibleName");
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x0015EAD7 File Offset: 0x0015CCD7
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new DropDownButtonAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x0015EAED File Offset: 0x0015CCED
		internal override ButtonBaseAdapter CreateStandardAdapter()
		{
			return new DropDownButtonAdapter(this);
		}

		// Token: 0x040036C1 RID: 14017
		private bool useComboBoxTheme;

		// Token: 0x040036C2 RID: 14018
		private bool ignoreMouse;
	}
}
