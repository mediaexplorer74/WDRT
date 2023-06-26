using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003ED RID: 1005
	internal class ToolStripPanelCell : ArrangedElement
	{
		// Token: 0x06004526 RID: 17702 RVA: 0x00122871 File Offset: 0x00120A71
		public ToolStripPanelCell(Control control)
			: this(null, control)
		{
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x0012287C File Offset: 0x00120A7C
		public ToolStripPanelCell(ToolStripPanelRow parent, Control control)
		{
			this.ToolStripPanelRow = parent;
			this._wrappedToolStrip = control as ToolStrip;
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (this._wrappedToolStrip == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("TypedControlCollectionShouldBeOfType", new object[] { typeof(ToolStrip).Name }), new object[0]), control.GetType().Name);
			}
			CommonProperties.SetAutoSize(this, true);
			this._wrappedToolStrip.LocationChanging += this.OnToolStripLocationChanging;
			this._wrappedToolStrip.VisibleChanged += this.OnToolStripVisibleChanged;
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x06004528 RID: 17704 RVA: 0x00122946 File Offset: 0x00120B46
		// (set) Token: 0x06004529 RID: 17705 RVA: 0x0012294E File Offset: 0x00120B4E
		public Rectangle CachedBounds
		{
			get
			{
				return this.cachedBounds;
			}
			set
			{
				this.cachedBounds = value;
			}
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x0600452A RID: 17706 RVA: 0x00122957 File Offset: 0x00120B57
		public Control Control
		{
			get
			{
				return this._wrappedToolStrip;
			}
		}

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x0600452B RID: 17707 RVA: 0x0012295F File Offset: 0x00120B5F
		public bool ControlInDesignMode
		{
			get
			{
				return this._wrappedToolStrip != null && this._wrappedToolStrip.IsInDesignMode;
			}
		}

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x0600452C RID: 17708 RVA: 0x00122957 File Offset: 0x00120B57
		public IArrangedElement InnerElement
		{
			get
			{
				return this._wrappedToolStrip;
			}
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x0600452D RID: 17709 RVA: 0x00122957 File Offset: 0x00120B57
		public ISupportToolStripPanel DraggedControl
		{
			get
			{
				return this._wrappedToolStrip;
			}
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x0600452E RID: 17710 RVA: 0x00122976 File Offset: 0x00120B76
		// (set) Token: 0x0600452F RID: 17711 RVA: 0x0012297E File Offset: 0x00120B7E
		public ToolStripPanelRow ToolStripPanelRow
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (this.parent != value)
				{
					if (this.parent != null)
					{
						((IList)this.parent.Cells).Remove(this);
					}
					this.parent = value;
					base.Margin = Padding.Empty;
				}
			}
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x06004530 RID: 17712 RVA: 0x001229B4 File Offset: 0x00120BB4
		// (set) Token: 0x06004531 RID: 17713 RVA: 0x001229E3 File Offset: 0x00120BE3
		public override bool Visible
		{
			get
			{
				return this.Control != null && this.Control.ParentInternal == this.ToolStripPanelRow.ToolStripPanel && this.InnerElement.ParticipatesInLayout;
			}
			set
			{
				this.Control.Visible = value;
			}
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x06004532 RID: 17714 RVA: 0x001229F1 File Offset: 0x00120BF1
		public Size MaximumSize
		{
			get
			{
				return this.maxSize;
			}
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06004533 RID: 17715 RVA: 0x0002F715 File Offset: 0x0002D915
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return DefaultLayout.Instance;
			}
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x00122976 File Offset: 0x00120B76
		protected override IArrangedElement GetContainer()
		{
			return this.parent;
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x001229F9 File Offset: 0x00120BF9
		public int Grow(int growBy)
		{
			if (this.ToolStripPanelRow.Orientation == Orientation.Vertical)
			{
				return this.GrowVertical(growBy);
			}
			return this.GrowHorizontal(growBy);
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x00122A18 File Offset: 0x00120C18
		private int GrowVertical(int growBy)
		{
			if (this.MaximumSize.Height >= this.Control.PreferredSize.Height)
			{
				return 0;
			}
			if (this.MaximumSize.Height + growBy >= this.Control.PreferredSize.Height)
			{
				int num = this.Control.PreferredSize.Height - this.MaximumSize.Height;
				this.maxSize = LayoutUtils.MaxSize;
				return num;
			}
			if (this.MaximumSize.Height + growBy < this.Control.PreferredSize.Height)
			{
				this.maxSize.Height = this.maxSize.Height + growBy;
				return growBy;
			}
			return 0;
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x00122ADC File Offset: 0x00120CDC
		private int GrowHorizontal(int growBy)
		{
			if (this.MaximumSize.Width >= this.Control.PreferredSize.Width)
			{
				return 0;
			}
			if (this.MaximumSize.Width + growBy >= this.Control.PreferredSize.Width)
			{
				int num = this.Control.PreferredSize.Width - this.MaximumSize.Width;
				this.maxSize = LayoutUtils.MaxSize;
				return num;
			}
			if (this.MaximumSize.Width + growBy < this.Control.PreferredSize.Width)
			{
				this.maxSize.Width = this.maxSize.Width + growBy;
				return growBy;
			}
			return 0;
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x00122BA0 File Offset: 0x00120DA0
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this._wrappedToolStrip != null)
					{
						this._wrappedToolStrip.LocationChanging -= this.OnToolStripLocationChanging;
						this._wrappedToolStrip.VisibleChanged -= this.OnToolStripVisibleChanged;
					}
					this._wrappedToolStrip = null;
					if (this.parent != null)
					{
						((IList)this.parent.Cells).Remove(this);
					}
					this.parent = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x00122C28 File Offset: 0x00120E28
		protected override ArrangedElementCollection GetChildren()
		{
			return ArrangedElementCollection.Empty;
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x00122C30 File Offset: 0x00120E30
		public override Size GetPreferredSize(Size constrainingSize)
		{
			ISupportToolStripPanel draggedControl = this.DraggedControl;
			Size size = Size.Empty;
			if (draggedControl.Stretch)
			{
				if (this.ToolStripPanelRow.Orientation == Orientation.Horizontal)
				{
					constrainingSize.Width = this.ToolStripPanelRow.Bounds.Width;
					size = this._wrappedToolStrip.GetPreferredSize(constrainingSize);
					size.Width = constrainingSize.Width;
				}
				else
				{
					constrainingSize.Height = this.ToolStripPanelRow.Bounds.Height;
					size = this._wrappedToolStrip.GetPreferredSize(constrainingSize);
					size.Height = constrainingSize.Height;
				}
			}
			else
			{
				size = ((!this._wrappedToolStrip.AutoSize) ? this._wrappedToolStrip.Size : this._wrappedToolStrip.GetPreferredSize(constrainingSize));
			}
			return size;
		}

		// Token: 0x0600453B RID: 17723 RVA: 0x00122CF8 File Offset: 0x00120EF8
		protected override void SetBoundsCore(Rectangle bounds, BoundsSpecified specified)
		{
			this.currentlySizing = true;
			this.CachedBounds = bounds;
			try
			{
				if (this.DraggedControl.IsCurrentlyDragging)
				{
					if (this.ToolStripPanelRow.Cells[this.ToolStripPanelRow.Cells.Count - 1] == this)
					{
						Rectangle displayRectangle = this.ToolStripPanelRow.DisplayRectangle;
						if (this.ToolStripPanelRow.Orientation == Orientation.Horizontal)
						{
							int num = bounds.Right - displayRectangle.Right;
							if (num > 0 && bounds.Width > num)
							{
								bounds.Width -= num;
							}
						}
						else
						{
							int num2 = bounds.Bottom - displayRectangle.Bottom;
							if (num2 > 0 && bounds.Height > num2)
							{
								bounds.Height -= num2;
							}
						}
					}
					base.SetBoundsCore(bounds, specified);
					this.InnerElement.SetBounds(bounds, specified);
				}
				else if (!this.ToolStripPanelRow.CachedBoundsMode)
				{
					base.SetBoundsCore(bounds, specified);
					this.InnerElement.SetBounds(bounds, specified);
				}
			}
			finally
			{
				this.currentlySizing = false;
			}
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x00122E10 File Offset: 0x00121010
		public int Shrink(int shrinkBy)
		{
			if (this.ToolStripPanelRow.Orientation == Orientation.Vertical)
			{
				return this.ShrinkVertical(shrinkBy);
			}
			return this.ShrinkHorizontal(shrinkBy);
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x0001180C File Offset: 0x0000FA0C
		private int ShrinkHorizontal(int shrinkBy)
		{
			return 0;
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x0001180C File Offset: 0x0000FA0C
		private int ShrinkVertical(int shrinkBy)
		{
			return 0;
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x00122E30 File Offset: 0x00121030
		private void OnToolStripLocationChanging(object sender, ToolStripLocationCancelEventArgs e)
		{
			if (this.ToolStripPanelRow == null)
			{
				return;
			}
			if (!this.currentlySizing && !this.currentlyDragging)
			{
				try
				{
					this.currentlyDragging = true;
					Point newLocation = e.NewLocation;
					if (this.ToolStripPanelRow != null && this.ToolStripPanelRow.Bounds == Rectangle.Empty)
					{
						this.ToolStripPanelRow.ToolStripPanel.PerformUpdate(true);
					}
					if (this._wrappedToolStrip != null)
					{
						this.ToolStripPanelRow.ToolStripPanel.Join(this._wrappedToolStrip, newLocation);
					}
				}
				finally
				{
					this.currentlyDragging = false;
					e.Cancel = true;
				}
			}
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x00122ED4 File Offset: 0x001210D4
		private void OnToolStripVisibleChanged(object sender, EventArgs e)
		{
			if (this._wrappedToolStrip != null && !this._wrappedToolStrip.IsInDesignMode && !this._wrappedToolStrip.IsCurrentlyDragging && !this._wrappedToolStrip.IsDisposed && !this._wrappedToolStrip.Disposing)
			{
				if (!this.Control.Visible)
				{
					this.restoreOnVisibleChanged = this.ToolStripPanelRow != null && ((IList)this.ToolStripPanelRow.Cells).Contains(this);
					return;
				}
				if (this.restoreOnVisibleChanged)
				{
					try
					{
						if (this.ToolStripPanelRow != null && ((IList)this.ToolStripPanelRow.Cells).Contains(this))
						{
							this.ToolStripPanelRow.ToolStripPanel.Join(this._wrappedToolStrip, this._wrappedToolStrip.Location);
						}
					}
					finally
					{
						this.restoreOnVisibleChanged = false;
					}
				}
			}
		}

		// Token: 0x0400263A RID: 9786
		private ToolStrip _wrappedToolStrip;

		// Token: 0x0400263B RID: 9787
		private ToolStripPanelRow parent;

		// Token: 0x0400263C RID: 9788
		private Size maxSize = LayoutUtils.MaxSize;

		// Token: 0x0400263D RID: 9789
		private bool currentlySizing;

		// Token: 0x0400263E RID: 9790
		private bool currentlyDragging;

		// Token: 0x0400263F RID: 9791
		private bool restoreOnVisibleChanged;

		// Token: 0x04002640 RID: 9792
		private Rectangle cachedBounds = Rectangle.Empty;
	}
}
