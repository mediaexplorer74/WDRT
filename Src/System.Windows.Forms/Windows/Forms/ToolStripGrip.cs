using System;
using System.Drawing;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003C4 RID: 964
	internal class ToolStripGrip : ToolStripButton
	{
		// Token: 0x06004177 RID: 16759 RVA: 0x001183C8 File Offset: 0x001165C8
		internal ToolStripGrip()
		{
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledDefaultPadding = DpiHelper.LogicalToDeviceUnits(ToolStripGrip.defaultPadding, 0);
				this.scaledGripThickness = DpiHelper.LogicalToDeviceUnitsX(ToolStripGrip.gripThicknessDefault);
				this.scaledGripThicknessVisualStylesEnabled = DpiHelper.LogicalToDeviceUnitsX(ToolStripGrip.gripThicknessVisualStylesEnabled);
			}
			this.gripThickness = (ToolStripManager.VisualStylesEnabled ? this.scaledGripThicknessVisualStylesEnabled : this.scaledGripThickness);
			base.SupportsItemClick = false;
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06004178 RID: 16760 RVA: 0x0011846C File Offset: 0x0011666C
		protected internal override Padding DefaultMargin
		{
			get
			{
				return this.scaledDefaultPadding;
			}
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06004179 RID: 16761 RVA: 0x0001180C File Offset: 0x0000FA0C
		public override bool CanSelect
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x0600417A RID: 16762 RVA: 0x00118474 File Offset: 0x00116674
		internal int GripThickness
		{
			get
			{
				return this.gripThickness;
			}
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x0600417B RID: 16763 RVA: 0x0011847C File Offset: 0x0011667C
		// (set) Token: 0x0600417C RID: 16764 RVA: 0x00118490 File Offset: 0x00116690
		internal bool MovingToolStrip
		{
			get
			{
				return this.ToolStripPanelRow != null && this.movingToolStrip;
			}
			set
			{
				if (this.movingToolStrip != value && base.ParentInternal != null)
				{
					if (value && base.ParentInternal.ToolStripPanelRow == null)
					{
						return;
					}
					this.movingToolStrip = value;
					this.lastEndLocation = ToolStrip.InvalidMouseEnter;
					if (this.movingToolStrip)
					{
						((ISupportToolStripPanel)base.ParentInternal).BeginDrag();
						return;
					}
					((ISupportToolStripPanel)base.ParentInternal).EndDrag();
				}
			}
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x0600417D RID: 16765 RVA: 0x001184F0 File Offset: 0x001166F0
		private ToolStripPanelRow ToolStripPanelRow
		{
			get
			{
				if (base.ParentInternal != null)
				{
					return ((ISupportToolStripPanel)base.ParentInternal).ToolStripPanelRow;
				}
				return null;
			}
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x00118507 File Offset: 0x00116707
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripGrip.ToolStripGripAccessibleObject(this);
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x00118510 File Offset: 0x00116710
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size empty = Size.Empty;
			if (base.ParentInternal != null)
			{
				if (base.ParentInternal.LayoutStyle == ToolStripLayoutStyle.VerticalStackWithOverflow)
				{
					empty = new Size(base.ParentInternal.Width, this.gripThickness);
				}
				else
				{
					empty = new Size(this.gripThickness, base.ParentInternal.Height);
				}
			}
			if (empty.Width > constrainingSize.Width)
			{
				empty.Width = constrainingSize.Width;
			}
			if (empty.Height > constrainingSize.Height)
			{
				empty.Height = constrainingSize.Height;
			}
			return empty;
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x001185A8 File Offset: 0x001167A8
		private bool LeftMouseButtonIsDown()
		{
			return Control.MouseButtons == MouseButtons.Left && Control.ModifierKeys == Keys.None;
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x001185C0 File Offset: 0x001167C0
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.ParentInternal != null)
			{
				base.ParentInternal.OnPaintGrip(e);
			}
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x001185D6 File Offset: 0x001167D6
		protected override void OnMouseDown(MouseEventArgs mea)
		{
			this.startLocation = base.TranslatePoint(new Point(mea.X, mea.Y), ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ScreenCoords);
			base.OnMouseDown(mea);
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x00118600 File Offset: 0x00116800
		protected override void OnMouseMove(MouseEventArgs mea)
		{
			bool flag = this.LeftMouseButtonIsDown();
			if (!this.MovingToolStrip && flag)
			{
				Point point = base.TranslatePoint(mea.Location, ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ScreenCoords);
				int num = point.X - this.startLocation.X;
				num = ((num < 0) ? (num * -1) : num);
				if (ToolStripGrip.DragSize == LayoutUtils.MaxSize)
				{
					ToolStripGrip.DragSize = SystemInformation.DragSize;
				}
				if (num >= ToolStripGrip.DragSize.Width)
				{
					this.MovingToolStrip = true;
				}
				else
				{
					int num2 = point.Y - this.startLocation.Y;
					num2 = ((num2 < 0) ? (num2 * -1) : num2);
					if (num2 >= ToolStripGrip.DragSize.Height)
					{
						this.MovingToolStrip = true;
					}
				}
			}
			if (this.MovingToolStrip)
			{
				if (flag)
				{
					Point point2 = base.TranslatePoint(new Point(mea.X, mea.Y), ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ScreenCoords);
					if (point2 != this.lastEndLocation)
					{
						this.ToolStripPanelRow.ToolStripPanel.MoveControl(base.ParentInternal, point2);
						this.lastEndLocation = point2;
					}
					this.startLocation = point2;
				}
				else
				{
					this.MovingToolStrip = false;
				}
			}
			base.OnMouseMove(mea);
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x00118724 File Offset: 0x00116924
		protected override void OnMouseEnter(EventArgs e)
		{
			if (base.ParentInternal != null && this.ToolStripPanelRow != null && !base.ParentInternal.IsInDesignMode)
			{
				this.oldCursor = base.ParentInternal.Cursor;
				ToolStripGrip.SetCursor(base.ParentInternal, Cursors.SizeAll);
			}
			else
			{
				this.oldCursor = null;
			}
			base.OnMouseEnter(e);
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x00118780 File Offset: 0x00116980
		protected override void OnMouseLeave(EventArgs e)
		{
			if (this.oldCursor != null && !base.ParentInternal.IsInDesignMode)
			{
				ToolStripGrip.SetCursor(base.ParentInternal, this.oldCursor);
			}
			if (!this.MovingToolStrip && this.LeftMouseButtonIsDown())
			{
				this.MovingToolStrip = true;
			}
			base.OnMouseLeave(e);
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x001187D8 File Offset: 0x001169D8
		protected override void OnMouseUp(MouseEventArgs mea)
		{
			if (this.MovingToolStrip)
			{
				Point point = base.TranslatePoint(new Point(mea.X, mea.Y), ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ScreenCoords);
				this.ToolStripPanelRow.ToolStripPanel.MoveControl(base.ParentInternal, point);
			}
			if (!base.ParentInternal.IsInDesignMode)
			{
				ToolStripGrip.SetCursor(base.ParentInternal, this.oldCursor);
			}
			ToolStripPanel.ClearDragFeedback();
			this.MovingToolStrip = false;
			base.OnMouseUp(mea);
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x00118850 File Offset: 0x00116A50
		internal override void ToolStrip_RescaleConstants(int oldDpi, int newDpi)
		{
			base.RescaleConstantsInternal(newDpi);
			this.scaledDefaultPadding = DpiHelper.LogicalToDeviceUnits(ToolStripGrip.defaultPadding, newDpi);
			this.scaledGripThickness = DpiHelper.LogicalToDeviceUnits(ToolStripGrip.gripThicknessDefault, newDpi);
			this.scaledGripThicknessVisualStylesEnabled = DpiHelper.LogicalToDeviceUnits(ToolStripGrip.gripThicknessVisualStylesEnabled, newDpi);
			base.Margin = this.DefaultMargin;
			this.gripThickness = (ToolStripManager.VisualStylesEnabled ? this.scaledGripThicknessVisualStylesEnabled : this.scaledGripThickness);
			this.OnFontChanged(EventArgs.Empty);
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x001188C9 File Offset: 0x00116AC9
		private static void SetCursor(Control control, Cursor cursor)
		{
			IntSecurity.ModifyCursor.Assert();
			control.Cursor = cursor;
		}

		// Token: 0x0400250F RID: 9487
		private Cursor oldCursor;

		// Token: 0x04002510 RID: 9488
		private int gripThickness;

		// Token: 0x04002511 RID: 9489
		private Point startLocation = Point.Empty;

		// Token: 0x04002512 RID: 9490
		private bool movingToolStrip;

		// Token: 0x04002513 RID: 9491
		private Point lastEndLocation = ToolStrip.InvalidMouseEnter;

		// Token: 0x04002514 RID: 9492
		private static Size DragSize = LayoutUtils.MaxSize;

		// Token: 0x04002515 RID: 9493
		private static readonly Padding defaultPadding = new Padding(2);

		// Token: 0x04002516 RID: 9494
		private static readonly int gripThicknessDefault = 3;

		// Token: 0x04002517 RID: 9495
		private static readonly int gripThicknessVisualStylesEnabled = 5;

		// Token: 0x04002518 RID: 9496
		private Padding scaledDefaultPadding = ToolStripGrip.defaultPadding;

		// Token: 0x04002519 RID: 9497
		private int scaledGripThickness = ToolStripGrip.gripThicknessDefault;

		// Token: 0x0400251A RID: 9498
		private int scaledGripThicknessVisualStylesEnabled = ToolStripGrip.gripThicknessVisualStylesEnabled;

		// Token: 0x02000802 RID: 2050
		internal class ToolStripGripAccessibleObject : ToolStripButton.ToolStripButtonAccessibleObject
		{
			// Token: 0x06006EBF RID: 28351 RVA: 0x00195821 File Offset: 0x00193A21
			public ToolStripGripAccessibleObject(ToolStripGrip owner)
				: base(owner)
			{
			}

			// Token: 0x17001837 RID: 6199
			// (get) Token: 0x06006EC0 RID: 28352 RVA: 0x0019582C File Offset: 0x00193A2C
			// (set) Token: 0x06006EC1 RID: 28353 RVA: 0x0019586D File Offset: 0x00193A6D
			public override string Name
			{
				get
				{
					string accessibleName = base.Owner.AccessibleName;
					if (accessibleName != null)
					{
						return accessibleName;
					}
					if (string.IsNullOrEmpty(this.stockName))
					{
						this.stockName = SR.GetString("ToolStripGripAccessibleName");
					}
					return this.stockName;
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x17001838 RID: 6200
			// (get) Token: 0x06006EC2 RID: 28354 RVA: 0x00195878 File Offset: 0x00193A78
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.Grip;
				}
			}

			// Token: 0x06006EC3 RID: 28355 RVA: 0x00195898 File Offset: 0x00193A98
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3)
				{
					if (propertyID == 30003)
					{
						return 50027;
					}
					if (propertyID == 30022)
					{
						return false;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x040042F7 RID: 17143
			private string stockName;
		}
	}
}
