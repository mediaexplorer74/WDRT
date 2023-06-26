using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a row of a <see cref="T:System.Windows.Forms.ToolStripPanel" /> that can contain controls.</summary>
	// Token: 0x020003F2 RID: 1010
	[ToolboxItem(false)]
	public class ToolStripPanelRow : Component, IArrangedElement, IComponent, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> class, specifying the containing <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <param name="parent">The containing <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
		// Token: 0x06004553 RID: 17747 RVA: 0x00123026 File Offset: 0x00121226
		public ToolStripPanelRow(ToolStripPanel parent)
			: this(parent, true)
		{
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x00123030 File Offset: 0x00121230
		internal ToolStripPanelRow(ToolStripPanel parent, bool visible)
		{
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.minAllowedWidth = DpiHelper.LogicalToDeviceUnitsX(50);
			}
			this.parent = parent;
			this.state[ToolStripPanelRow.stateVisible] = visible;
			this.state[ToolStripPanelRow.stateDisposing | ToolStripPanelRow.stateLocked | ToolStripPanelRow.stateInitialized] = false;
			using (new LayoutTransaction(parent, this, null))
			{
				this.Margin = this.DefaultMargin;
				CommonProperties.SetAutoSize(this, true);
			}
		}

		/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />, including its nonclient elements, in pixels, relative to the parent control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the size and location.</returns>
		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06004555 RID: 17749 RVA: 0x001230E4 File Offset: 0x001212E4
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the controls in the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</summary>
		/// <returns>An array of controls.</returns>
		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06004556 RID: 17750 RVA: 0x001230EC File Offset: 0x001212EC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlControlsDescr")]
		public Control[] Controls
		{
			get
			{
				Control[] array = new Control[this.ControlsInternal.Count];
				this.ControlsInternal.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06004557 RID: 17751 RVA: 0x00123118 File Offset: 0x00121318
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlControlsDescr")]
		internal ToolStripPanelRow.ToolStripPanelRowControlCollection ControlsInternal
		{
			get
			{
				ToolStripPanelRow.ToolStripPanelRowControlCollection toolStripPanelRowControlCollection = (ToolStripPanelRow.ToolStripPanelRowControlCollection)this.Properties.GetObject(ToolStripPanelRow.PropControlsCollection);
				if (toolStripPanelRowControlCollection == null)
				{
					toolStripPanelRowControlCollection = this.CreateControlsInstance();
					this.Properties.SetObject(ToolStripPanelRow.PropControlsCollection, toolStripPanelRowControlCollection);
				}
				return toolStripPanelRowControlCollection;
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06004558 RID: 17752 RVA: 0x00123157 File Offset: 0x00121357
		internal ArrangedElementCollection Cells
		{
			get
			{
				return this.ControlsInternal.Cells;
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x00123164 File Offset: 0x00121364
		// (set) Token: 0x0600455A RID: 17754 RVA: 0x00123176 File Offset: 0x00121376
		internal bool CachedBoundsMode
		{
			get
			{
				return this.state[ToolStripPanelRow.stateCachedBoundsMode];
			}
			set
			{
				this.state[ToolStripPanelRow.stateCachedBoundsMode] = value;
			}
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x00123189 File Offset: 0x00121389
		private ToolStripPanelRow.ToolStripPanelRowManager RowManager
		{
			get
			{
				if (this.rowManager == null)
				{
					this.rowManager = ((this.Orientation == Orientation.Horizontal) ? new ToolStripPanelRow.HorizontalRowManager(this) : new ToolStripPanelRow.VerticalRowManager(this));
					this.Initialized = true;
				}
				return this.rowManager;
			}
		}

		/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the default space between controls.</returns>
		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x0600455C RID: 17756 RVA: 0x001231BC File Offset: 0x001213BC
		protected virtual Padding DefaultMargin
		{
			get
			{
				ToolStripPanelCell nextVisibleCell = this.RowManager.GetNextVisibleCell(0, true);
				if (nextVisibleCell != null && nextVisibleCell.DraggedControl != null && nextVisibleCell.DraggedControl.Stretch)
				{
					Padding rowMargin = this.ToolStripPanel.RowMargin;
					if (this.Orientation == Orientation.Horizontal)
					{
						rowMargin.Left = 0;
						rowMargin.Right = 0;
					}
					else
					{
						rowMargin.Top = 0;
						rowMargin.Bottom = 0;
					}
					return rowMargin;
				}
				return this.ToolStripPanel.RowMargin;
			}
		}

		/// <summary>Gets the internal spacing, in pixels, of the contents of a control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the internal spacing of the contents of a control.</returns>
		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x0600455D RID: 17757 RVA: 0x00019A61 File Offset: 0x00017C61
		protected virtual Padding DefaultPadding
		{
			get
			{
				return Padding.Empty;
			}
		}

		/// <summary>Gets the display area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the size and location.</returns>
		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x0600455E RID: 17758 RVA: 0x00123232 File Offset: 0x00121432
		public Rectangle DisplayRectangle
		{
			get
			{
				return this.RowManager.DisplayRectangle;
			}
		}

		/// <summary>Gets an instance of the control's layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> for the control's contents.</returns>
		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x0600455F RID: 17759 RVA: 0x000AF974 File Offset: 0x000ADB74
		public LayoutEngine LayoutEngine
		{
			get
			{
				return FlowLayout.Instance;
			}
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06004560 RID: 17760 RVA: 0x0012323F File Offset: 0x0012143F
		internal bool Locked
		{
			get
			{
				return this.state[ToolStripPanelRow.stateLocked];
			}
		}

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06004561 RID: 17761 RVA: 0x00123251 File Offset: 0x00121451
		// (set) Token: 0x06004562 RID: 17762 RVA: 0x00123263 File Offset: 0x00121463
		private bool Initialized
		{
			get
			{
				return this.state[ToolStripPanelRow.stateInitialized];
			}
			set
			{
				this.state[ToolStripPanelRow.stateInitialized] = value;
			}
		}

		/// <summary>Gets or sets the space between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the space between controls.</returns>
		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x00019A7D File Offset: 0x00017C7D
		// (set) Token: 0x06004564 RID: 17764 RVA: 0x00123276 File Offset: 0x00121476
		public Padding Margin
		{
			get
			{
				return CommonProperties.GetMargin(this);
			}
			set
			{
				if (this.Margin != value)
				{
					CommonProperties.SetMargin(this, value);
				}
			}
		}

		/// <summary>Gets or sets padding within the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the control's internal spacing characteristics.</returns>
		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06004565 RID: 17765 RVA: 0x0012328D File Offset: 0x0012148D
		// (set) Token: 0x06004566 RID: 17766 RVA: 0x0012329B File Offset: 0x0012149B
		public virtual Padding Padding
		{
			get
			{
				return CommonProperties.GetPadding(this, this.DefaultPadding);
			}
			set
			{
				if (this.Padding != value)
				{
					CommonProperties.SetPadding(this, value);
				}
			}
		}

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x001232B2 File Offset: 0x001214B2
		internal Control ParentInternal
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06004568 RID: 17768 RVA: 0x001232BA File Offset: 0x001214BA
		internal PropertyStore Properties
		{
			get
			{
				return this.propertyStore;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripPanel" /> that contains the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanel" /> that contains the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</returns>
		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x001232B2 File Offset: 0x001214B2
		public ToolStripPanel ToolStripPanel
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x0600456A RID: 17770 RVA: 0x001232C2 File Offset: 0x001214C2
		internal bool Visible
		{
			get
			{
				return this.state[ToolStripPanelRow.stateVisible];
			}
		}

		/// <summary>Gets the layout direction of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> relative to its containing <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values.</returns>
		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x0600456B RID: 17771 RVA: 0x001232D4 File Offset: 0x001214D4
		public Orientation Orientation
		{
			get
			{
				return this.ToolStripPanel.Orientation;
			}
		}

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Windows.Forms.ToolStrip" /> can be dragged and dropped into a <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</summary>
		/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to be dragged and dropped into the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
		/// <returns>
		///   <see langword="true" /> if there is enough space in the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to receive the <see cref="T:System.Windows.Forms.ToolStrip" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600456C RID: 17772 RVA: 0x001232E1 File Offset: 0x001214E1
		public bool CanMove(ToolStrip toolStripToDrag)
		{
			return !this.ToolStripPanel.Locked && !this.Locked && this.RowManager.CanMove(toolStripToDrag);
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x00123306 File Offset: 0x00121506
		private ToolStripPanelRow.ToolStripPanelRowControlCollection CreateControlsInstance()
		{
			return new ToolStripPanelRow.ToolStripPanelRowControlCollection(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600456E RID: 17774 RVA: 0x00123310 File Offset: 0x00121510
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.state[ToolStripPanelRow.stateDisposing] = true;
					this.ControlsInternal.Clear();
				}
			}
			finally
			{
				this.state[ToolStripPanelRow.stateDisposing] = false;
				base.Dispose(disposing);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ControlAdded" /> event.</summary>
		/// <param name="control">The control that was added to the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
		/// <param name="index">The zero-based index representing the position of the added control.</param>
		// Token: 0x0600456F RID: 17775 RVA: 0x00123368 File Offset: 0x00121568
		protected internal virtual void OnControlAdded(Control control, int index)
		{
			ISupportToolStripPanel supportToolStripPanel = control as ISupportToolStripPanel;
			if (supportToolStripPanel != null)
			{
				supportToolStripPanel.ToolStripPanelRow = this;
			}
			this.RowManager.OnControlAdded(control, index);
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripPanelRow.Orientation" /> property changes.</summary>
		// Token: 0x06004570 RID: 17776 RVA: 0x00123393 File Offset: 0x00121593
		protected internal virtual void OnOrientationChanged()
		{
			this.rowManager = null;
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripPanelRow.Bounds" /> property changes.</summary>
		/// <param name="oldBounds">The original value of the <see cref="P:System.Windows.Forms.ToolStripPanelRow.Bounds" /> property.</param>
		/// <param name="newBounds">The new value of the <see cref="P:System.Windows.Forms.ToolStripPanelRow.Bounds" /> property.</param>
		// Token: 0x06004571 RID: 17777 RVA: 0x0012339C File Offset: 0x0012159C
		protected void OnBoundsChanged(Rectangle oldBounds, Rectangle newBounds)
		{
			((IArrangedElement)this).PerformLayout(this, PropertyNames.Size);
			this.RowManager.OnBoundsChanged(oldBounds, newBounds);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ControlRemoved" /> event.</summary>
		/// <param name="control">The control that was removed from the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
		/// <param name="index">The zero-based index representing the position of the removed control.</param>
		// Token: 0x06004572 RID: 17778 RVA: 0x001233B8 File Offset: 0x001215B8
		protected internal virtual void OnControlRemoved(Control control, int index)
		{
			if (!this.state[ToolStripPanelRow.stateDisposing])
			{
				this.SuspendLayout();
				this.RowManager.OnControlRemoved(control, index);
				ISupportToolStripPanel supportToolStripPanel = control as ISupportToolStripPanel;
				if (supportToolStripPanel != null && supportToolStripPanel.ToolStripPanelRow == this)
				{
					supportToolStripPanel.ToolStripPanelRow = null;
				}
				this.ResumeLayout(true);
				if (this.ControlsInternal.Count <= 0)
				{
					this.ToolStripPanel.RowsInternal.Remove(this);
					base.Dispose();
				}
			}
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x00123430 File Offset: 0x00121630
		internal Size GetMinimumSize(ToolStrip toolStrip)
		{
			if (toolStrip.MinimumSize == Size.Empty)
			{
				return new Size(this.minAllowedWidth, this.minAllowedWidth);
			}
			return toolStrip.MinimumSize;
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x0012345C File Offset: 0x0012165C
		private void ApplyCachedBounds()
		{
			for (int i = 0; i < this.Cells.Count; i++)
			{
				IArrangedElement arrangedElement = this.Cells[i];
				if (arrangedElement.ParticipatesInLayout)
				{
					ToolStripPanelCell toolStripPanelCell = arrangedElement as ToolStripPanelCell;
					arrangedElement.SetBounds(toolStripPanelCell.CachedBounds, BoundsSpecified.None);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06004575 RID: 17781 RVA: 0x001234A8 File Offset: 0x001216A8
		protected virtual void OnLayout(LayoutEventArgs e)
		{
			if (this.Initialized && !this.state[ToolStripPanelRow.stateInLayout])
			{
				this.state[ToolStripPanelRow.stateInLayout] = true;
				try
				{
					this.Margin = this.DefaultMargin;
					this.CachedBoundsMode = true;
					try
					{
						bool flag = this.LayoutEngine.Layout(this, e);
					}
					finally
					{
						this.CachedBoundsMode = false;
					}
					if (this.RowManager.GetNextVisibleCell(this.Cells.Count - 1, false) == null)
					{
						this.ApplyCachedBounds();
					}
					else if (this.Orientation == Orientation.Horizontal)
					{
						this.OnLayoutHorizontalPostFix();
					}
					else
					{
						this.OnLayoutVerticalPostFix();
					}
				}
				finally
				{
					this.state[ToolStripPanelRow.stateInLayout] = false;
				}
			}
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x0012357C File Offset: 0x0012177C
		private void OnLayoutHorizontalPostFix()
		{
			ToolStripPanelCell nextVisibleCell = this.RowManager.GetNextVisibleCell(this.Cells.Count - 1, false);
			if (nextVisibleCell == null)
			{
				this.ApplyCachedBounds();
				return;
			}
			int num = nextVisibleCell.CachedBounds.Right - this.RowManager.DisplayRectangle.Right;
			if (num <= 0)
			{
				this.ApplyCachedBounds();
				return;
			}
			int[] array = new int[this.Cells.Count];
			for (int i = 0; i < this.Cells.Count; i++)
			{
				ToolStripPanelCell toolStripPanelCell = this.Cells[i] as ToolStripPanelCell;
				array[i] = toolStripPanelCell.Margin.Left;
			}
			num -= this.RowManager.FreeSpaceFromRow(num);
			for (int j = 0; j < this.Cells.Count; j++)
			{
				ToolStripPanelCell toolStripPanelCell2 = this.Cells[j] as ToolStripPanelCell;
				Rectangle cachedBounds = toolStripPanelCell2.CachedBounds;
				cachedBounds.X -= Math.Max(0, array[j] - toolStripPanelCell2.Margin.Left);
				toolStripPanelCell2.CachedBounds = cachedBounds;
			}
			if (num <= 0)
			{
				this.ApplyCachedBounds();
				return;
			}
			int[] array2 = null;
			for (int k = this.Cells.Count - 1; k >= 0; k--)
			{
				ToolStripPanelCell toolStripPanelCell3 = this.Cells[k] as ToolStripPanelCell;
				if (toolStripPanelCell3.Visible)
				{
					Size minimumSize = this.GetMinimumSize(toolStripPanelCell3.Control as ToolStrip);
					Rectangle cachedBounds2 = toolStripPanelCell3.CachedBounds;
					if (cachedBounds2.Width > minimumSize.Width)
					{
						num -= cachedBounds2.Width - minimumSize.Width;
						cachedBounds2.Width = ((num < 0) ? (minimumSize.Width + -num) : minimumSize.Width);
						for (int l = k + 1; l < this.Cells.Count; l++)
						{
							if (array2 == null)
							{
								array2 = new int[this.Cells.Count];
							}
							array2[l] += Math.Max(0, toolStripPanelCell3.CachedBounds.Width - cachedBounds2.Width);
						}
						toolStripPanelCell3.CachedBounds = cachedBounds2;
					}
				}
				if (num <= 0)
				{
					break;
				}
			}
			if (array2 != null)
			{
				for (int m = 0; m < this.Cells.Count; m++)
				{
					ToolStripPanelCell toolStripPanelCell4 = this.Cells[m] as ToolStripPanelCell;
					Rectangle cachedBounds3 = toolStripPanelCell4.CachedBounds;
					cachedBounds3.X -= array2[m];
					toolStripPanelCell4.CachedBounds = cachedBounds3;
				}
			}
			this.ApplyCachedBounds();
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x0012381C File Offset: 0x00121A1C
		private void OnLayoutVerticalPostFix()
		{
			ToolStripPanelCell nextVisibleCell = this.RowManager.GetNextVisibleCell(this.Cells.Count - 1, false);
			int num = nextVisibleCell.CachedBounds.Bottom - this.RowManager.DisplayRectangle.Bottom;
			if (num <= 0)
			{
				this.ApplyCachedBounds();
				return;
			}
			int[] array = new int[this.Cells.Count];
			for (int i = 0; i < this.Cells.Count; i++)
			{
				ToolStripPanelCell toolStripPanelCell = this.Cells[i] as ToolStripPanelCell;
				array[i] = toolStripPanelCell.Margin.Top;
			}
			num -= this.RowManager.FreeSpaceFromRow(num);
			for (int j = 0; j < this.Cells.Count; j++)
			{
				ToolStripPanelCell toolStripPanelCell2 = this.Cells[j] as ToolStripPanelCell;
				Rectangle cachedBounds = toolStripPanelCell2.CachedBounds;
				cachedBounds.X = Math.Max(0, cachedBounds.X - array[j] - toolStripPanelCell2.Margin.Top);
				toolStripPanelCell2.CachedBounds = cachedBounds;
			}
			if (num <= 0)
			{
				this.ApplyCachedBounds();
				return;
			}
			int[] array2 = null;
			for (int k = this.Cells.Count - 1; k >= 0; k--)
			{
				ToolStripPanelCell toolStripPanelCell3 = this.Cells[k] as ToolStripPanelCell;
				if (toolStripPanelCell3.Visible)
				{
					Size minimumSize = this.GetMinimumSize(toolStripPanelCell3.Control as ToolStrip);
					Rectangle cachedBounds2 = toolStripPanelCell3.CachedBounds;
					if (cachedBounds2.Height > minimumSize.Height)
					{
						num -= cachedBounds2.Height - minimumSize.Height;
						cachedBounds2.Height = ((num < 0) ? (minimumSize.Height + -num) : minimumSize.Height);
						for (int l = k + 1; l < this.Cells.Count; l++)
						{
							if (array2 == null)
							{
								array2 = new int[this.Cells.Count];
							}
							array2[l] += Math.Max(0, toolStripPanelCell3.CachedBounds.Height - cachedBounds2.Height);
						}
						toolStripPanelCell3.CachedBounds = cachedBounds2;
					}
				}
				if (num <= 0)
				{
					break;
				}
			}
			if (array2 != null)
			{
				for (int m = 0; m < this.Cells.Count; m++)
				{
					ToolStripPanelCell toolStripPanelCell4 = this.Cells[m] as ToolStripPanelCell;
					Rectangle cachedBounds3 = toolStripPanelCell4.CachedBounds;
					cachedBounds3.Y -= array2[m];
					toolStripPanelCell4.CachedBounds = cachedBounds3;
				}
			}
			this.ApplyCachedBounds();
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x00123AB4 File Offset: 0x00121CB4
		private void SetBounds(Rectangle bounds)
		{
			if (bounds != this.bounds)
			{
				Rectangle rectangle = this.bounds;
				this.bounds = bounds;
				this.OnBoundsChanged(rectangle, bounds);
			}
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x00123AE5 File Offset: 0x00121CE5
		private void SuspendLayout()
		{
			this.suspendCount++;
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x00123AF5 File Offset: 0x00121CF5
		private void ResumeLayout(bool performLayout)
		{
			this.suspendCount--;
			if (performLayout)
			{
				((IArrangedElement)this).PerformLayout(this, null);
			}
		}

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x0600457B RID: 17787 RVA: 0x00123B10 File Offset: 0x00121D10
		ArrangedElementCollection IArrangedElement.Children
		{
			get
			{
				return this.Cells;
			}
		}

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x0600457C RID: 17788 RVA: 0x00123B18 File Offset: 0x00121D18
		IArrangedElement IArrangedElement.Container
		{
			get
			{
				return this.ToolStripPanel;
			}
		}

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x0600457D RID: 17789 RVA: 0x00123B20 File Offset: 0x00121D20
		Rectangle IArrangedElement.DisplayRectangle
		{
			get
			{
				return this.Bounds;
			}
		}

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x0600457E RID: 17790 RVA: 0x00123B35 File Offset: 0x00121D35
		bool IArrangedElement.ParticipatesInLayout
		{
			get
			{
				return this.Visible;
			}
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x0600457F RID: 17791 RVA: 0x00123B3D File Offset: 0x00121D3D
		PropertyStore IArrangedElement.Properties
		{
			get
			{
				return this.Properties;
			}
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x00123B48 File Offset: 0x00121D48
		Size IArrangedElement.GetPreferredSize(Size constrainingSize)
		{
			Size size = this.LayoutEngine.GetPreferredSize(this, constrainingSize - this.Padding.Size) + this.Padding.Size;
			if (this.Orientation == Orientation.Horizontal && this.ParentInternal != null)
			{
				size.Width = this.DisplayRectangle.Width;
			}
			else
			{
				size.Height = this.DisplayRectangle.Height;
			}
			return size;
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x00123BC6 File Offset: 0x00121DC6
		void IArrangedElement.SetBounds(Rectangle bounds, BoundsSpecified specified)
		{
			this.SetBounds(bounds);
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x00123BCF File Offset: 0x00121DCF
		void IArrangedElement.PerformLayout(IArrangedElement container, string propertyName)
		{
			if (this.suspendCount <= 0)
			{
				this.OnLayout(new LayoutEventArgs(container, propertyName));
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06004583 RID: 17795 RVA: 0x00123BE7 File Offset: 0x00121DE7
		internal Rectangle DragBounds
		{
			get
			{
				return this.RowManager.DragBounds;
			}
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x00123BF4 File Offset: 0x00121DF4
		internal void MoveControl(ToolStrip movingControl, Point startClientLocation, Point endClientLocation)
		{
			this.RowManager.MoveControl(movingControl, startClientLocation, endClientLocation);
		}

		// Token: 0x06004585 RID: 17797 RVA: 0x00123C04 File Offset: 0x00121E04
		internal void JoinRow(ToolStrip toolStripToDrag, Point locationToDrag)
		{
			this.RowManager.JoinRow(toolStripToDrag, locationToDrag);
		}

		// Token: 0x06004586 RID: 17798 RVA: 0x00123C13 File Offset: 0x00121E13
		internal void LeaveRow(ToolStrip toolStripToDrag)
		{
			this.RowManager.LeaveRow(toolStripToDrag);
			if (this.ControlsInternal.Count == 0)
			{
				this.ToolStripPanel.RowsInternal.Remove(this);
				base.Dispose();
			}
		}

		// Token: 0x06004587 RID: 17799 RVA: 0x000070A6 File Offset: 0x000052A6
		[Conditional("DEBUG")]
		private void PrintPlacements(int index)
		{
		}

		// Token: 0x04002647 RID: 9799
		private Rectangle bounds = Rectangle.Empty;

		// Token: 0x04002648 RID: 9800
		private ToolStripPanel parent;

		// Token: 0x04002649 RID: 9801
		private BitVector32 state;

		// Token: 0x0400264A RID: 9802
		private PropertyStore propertyStore = new PropertyStore();

		// Token: 0x0400264B RID: 9803
		private int suspendCount;

		// Token: 0x0400264C RID: 9804
		private ToolStripPanelRow.ToolStripPanelRowManager rowManager;

		// Token: 0x0400264D RID: 9805
		private const int MINALLOWEDWIDTH = 50;

		// Token: 0x0400264E RID: 9806
		private int minAllowedWidth = 50;

		// Token: 0x0400264F RID: 9807
		private static readonly int stateVisible = BitVector32.CreateMask();

		// Token: 0x04002650 RID: 9808
		private static readonly int stateDisposing = BitVector32.CreateMask(ToolStripPanelRow.stateVisible);

		// Token: 0x04002651 RID: 9809
		private static readonly int stateLocked = BitVector32.CreateMask(ToolStripPanelRow.stateDisposing);

		// Token: 0x04002652 RID: 9810
		private static readonly int stateInitialized = BitVector32.CreateMask(ToolStripPanelRow.stateLocked);

		// Token: 0x04002653 RID: 9811
		private static readonly int stateCachedBoundsMode = BitVector32.CreateMask(ToolStripPanelRow.stateInitialized);

		// Token: 0x04002654 RID: 9812
		private static readonly int stateInLayout = BitVector32.CreateMask(ToolStripPanelRow.stateCachedBoundsMode);

		// Token: 0x04002655 RID: 9813
		private static readonly int PropControlsCollection = PropertyStore.CreateKey();

		// Token: 0x04002656 RID: 9814
		internal static TraceSwitch ToolStripPanelRowCreationDebug;

		// Token: 0x04002657 RID: 9815
		internal static readonly TraceSwitch ToolStripPanelMouseDebug;

		// Token: 0x02000810 RID: 2064
		private abstract class ToolStripPanelRowManager
		{
			// Token: 0x06006F40 RID: 28480 RVA: 0x00197438 File Offset: 0x00195638
			public ToolStripPanelRowManager(ToolStripPanelRow owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006F41 RID: 28481 RVA: 0x00197448 File Offset: 0x00195648
			public virtual bool CanMove(ToolStrip toolStripToDrag)
			{
				if (toolStripToDrag != null && ((ISupportToolStripPanel)toolStripToDrag).Stretch)
				{
					return false;
				}
				foreach (object obj in this.Row.ControlsInternal)
				{
					Control control = (Control)obj;
					ISupportToolStripPanel supportToolStripPanel = control as ISupportToolStripPanel;
					if (supportToolStripPanel != null && supportToolStripPanel.Stretch)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x17001856 RID: 6230
			// (get) Token: 0x06006F42 RID: 28482 RVA: 0x00054155 File Offset: 0x00052355
			public virtual Rectangle DragBounds
			{
				get
				{
					return Rectangle.Empty;
				}
			}

			// Token: 0x17001857 RID: 6231
			// (get) Token: 0x06006F43 RID: 28483 RVA: 0x00054155 File Offset: 0x00052355
			public virtual Rectangle DisplayRectangle
			{
				get
				{
					return Rectangle.Empty;
				}
			}

			// Token: 0x17001858 RID: 6232
			// (get) Token: 0x06006F44 RID: 28484 RVA: 0x001974CC File Offset: 0x001956CC
			public ToolStripPanel ToolStripPanel
			{
				get
				{
					return this.owner.ToolStripPanel;
				}
			}

			// Token: 0x17001859 RID: 6233
			// (get) Token: 0x06006F45 RID: 28485 RVA: 0x001974D9 File Offset: 0x001956D9
			public ToolStripPanelRow Row
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x1700185A RID: 6234
			// (get) Token: 0x06006F46 RID: 28486 RVA: 0x001974E1 File Offset: 0x001956E1
			public FlowLayoutSettings FlowLayoutSettings
			{
				get
				{
					if (this.flowLayoutSettings == null)
					{
						this.flowLayoutSettings = new FlowLayoutSettings(this.owner);
					}
					return this.flowLayoutSettings;
				}
			}

			// Token: 0x06006F47 RID: 28487 RVA: 0x0001180C File Offset: 0x0000FA0C
			protected internal virtual int FreeSpaceFromRow(int spaceToFree)
			{
				return 0;
			}

			// Token: 0x06006F48 RID: 28488 RVA: 0x00197504 File Offset: 0x00195704
			protected virtual int Grow(int index, int growBy)
			{
				int num = 0;
				if (index >= 0 && index < this.Row.ControlsInternal.Count - 1)
				{
					ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)this.Row.Cells[index];
					if (toolStripPanelCell.Visible)
					{
						num = toolStripPanelCell.Grow(growBy);
					}
				}
				return num;
			}

			// Token: 0x06006F49 RID: 28489 RVA: 0x00197554 File Offset: 0x00195754
			public ToolStripPanelCell GetNextVisibleCell(int index, bool forward)
			{
				if (forward)
				{
					for (int i = index; i < this.Row.Cells.Count; i++)
					{
						ToolStripPanelCell toolStripPanelCell = this.Row.Cells[i] as ToolStripPanelCell;
						if ((toolStripPanelCell.Visible || (this.owner.parent.Visible && toolStripPanelCell.ControlInDesignMode)) && toolStripPanelCell.ToolStripPanelRow == this.owner)
						{
							return toolStripPanelCell;
						}
					}
				}
				else
				{
					for (int j = index; j >= 0; j--)
					{
						ToolStripPanelCell toolStripPanelCell2 = this.Row.Cells[j] as ToolStripPanelCell;
						if ((toolStripPanelCell2.Visible || (this.owner.parent.Visible && toolStripPanelCell2.ControlInDesignMode)) && toolStripPanelCell2.ToolStripPanelRow == this.owner)
						{
							return toolStripPanelCell2;
						}
					}
				}
				return null;
			}

			// Token: 0x06006F4A RID: 28490 RVA: 0x00197620 File Offset: 0x00195820
			protected virtual int GrowControlsAfter(int index, int growBy)
			{
				if (growBy < 0)
				{
					return 0;
				}
				int num = growBy;
				for (int i = index + 1; i < this.Row.ControlsInternal.Count; i++)
				{
					int num2 = this.Grow(i, num);
					if (num2 >= 0)
					{
						num -= num2;
						if (num <= 0)
						{
							return growBy;
						}
					}
				}
				return growBy - num;
			}

			// Token: 0x06006F4B RID: 28491 RVA: 0x0019766C File Offset: 0x0019586C
			protected virtual int GrowControlsBefore(int index, int growBy)
			{
				if (growBy < 0)
				{
					return 0;
				}
				int num = growBy;
				for (int i = index - 1; i >= 0; i--)
				{
					num -= this.Grow(i, num);
					if (num <= 0)
					{
						return growBy;
					}
				}
				return growBy - num;
			}

			// Token: 0x06006F4C RID: 28492 RVA: 0x000070A6 File Offset: 0x000052A6
			public virtual void MoveControl(ToolStrip movingControl, Point startClientLocation, Point endClientLocation)
			{
			}

			// Token: 0x06006F4D RID: 28493 RVA: 0x000070A6 File Offset: 0x000052A6
			public virtual void LeaveRow(ToolStrip toolStripToDrag)
			{
			}

			// Token: 0x06006F4E RID: 28494 RVA: 0x000070A6 File Offset: 0x000052A6
			public virtual void JoinRow(ToolStrip toolStripToDrag, Point locationToDrag)
			{
			}

			// Token: 0x06006F4F RID: 28495 RVA: 0x000070A6 File Offset: 0x000052A6
			protected internal virtual void OnControlAdded(Control c, int index)
			{
			}

			// Token: 0x06006F50 RID: 28496 RVA: 0x000070A6 File Offset: 0x000052A6
			protected internal virtual void OnControlRemoved(Control c, int index)
			{
			}

			// Token: 0x06006F51 RID: 28497 RVA: 0x000070A6 File Offset: 0x000052A6
			protected internal virtual void OnBoundsChanged(Rectangle oldBounds, Rectangle newBounds)
			{
			}

			// Token: 0x04004318 RID: 17176
			private FlowLayoutSettings flowLayoutSettings;

			// Token: 0x04004319 RID: 17177
			private ToolStripPanelRow owner;
		}

		// Token: 0x02000811 RID: 2065
		private class HorizontalRowManager : ToolStripPanelRow.ToolStripPanelRowManager
		{
			// Token: 0x06006F52 RID: 28498 RVA: 0x001976A3 File Offset: 0x001958A3
			public HorizontalRowManager(ToolStripPanelRow owner)
				: base(owner)
			{
				owner.SuspendLayout();
				base.FlowLayoutSettings.WrapContents = false;
				base.FlowLayoutSettings.FlowDirection = FlowDirection.LeftToRight;
				owner.ResumeLayout(false);
			}

			// Token: 0x1700185B RID: 6235
			// (get) Token: 0x06006F53 RID: 28499 RVA: 0x001976D4 File Offset: 0x001958D4
			public override Rectangle DisplayRectangle
			{
				get
				{
					Rectangle displayRectangle = ((IArrangedElement)base.Row).DisplayRectangle;
					if (base.ToolStripPanel != null)
					{
						Rectangle displayRectangle2 = base.ToolStripPanel.DisplayRectangle;
						if ((!base.ToolStripPanel.Visible || LayoutUtils.IsZeroWidthOrHeight(displayRectangle2)) && base.ToolStripPanel.ParentInternal != null)
						{
							displayRectangle.Width = base.ToolStripPanel.ParentInternal.DisplayRectangle.Width - (base.ToolStripPanel.Margin.Horizontal + base.ToolStripPanel.Padding.Horizontal) - base.Row.Margin.Horizontal;
						}
						else
						{
							displayRectangle.Width = displayRectangle2.Width - base.Row.Margin.Horizontal;
						}
					}
					return displayRectangle;
				}
			}

			// Token: 0x1700185C RID: 6236
			// (get) Token: 0x06006F54 RID: 28500 RVA: 0x001977A8 File Offset: 0x001959A8
			public override Rectangle DragBounds
			{
				get
				{
					Rectangle bounds = base.Row.Bounds;
					int num = base.ToolStripPanel.RowsInternal.IndexOf(base.Row);
					if (num > 0)
					{
						Rectangle bounds2 = base.ToolStripPanel.RowsInternal[num - 1].Bounds;
						int num2 = bounds2.Y + bounds2.Height - (bounds2.Height >> 2);
						bounds.Height += bounds.Y - num2;
						bounds.Y = num2;
					}
					if (num < base.ToolStripPanel.RowsInternal.Count - 1)
					{
						Rectangle bounds3 = base.ToolStripPanel.RowsInternal[num + 1].Bounds;
						bounds.Height += (bounds3.Height >> 2) + base.Row.Margin.Bottom + base.ToolStripPanel.RowsInternal[num + 1].Margin.Top;
					}
					bounds.Width += base.Row.Margin.Horizontal + base.ToolStripPanel.Padding.Horizontal + 5;
					bounds.X -= base.Row.Margin.Left + base.ToolStripPanel.Padding.Left + 4;
					return bounds;
				}
			}

			// Token: 0x06006F55 RID: 28501 RVA: 0x00197920 File Offset: 0x00195B20
			public override bool CanMove(ToolStrip toolStripToDrag)
			{
				if (base.CanMove(toolStripToDrag))
				{
					Size size = Size.Empty;
					for (int i = 0; i < base.Row.ControlsInternal.Count; i++)
					{
						size += base.Row.GetMinimumSize(base.Row.ControlsInternal[i] as ToolStrip);
					}
					return (size + base.Row.GetMinimumSize(toolStripToDrag)).Width < this.DisplayRectangle.Width;
				}
				return false;
			}

			// Token: 0x06006F56 RID: 28502 RVA: 0x001979AC File Offset: 0x00195BAC
			protected internal override int FreeSpaceFromRow(int spaceToFree)
			{
				int num = spaceToFree;
				if (spaceToFree > 0)
				{
					ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
					if (nextVisibleCell == null)
					{
						return 0;
					}
					Padding margin = nextVisibleCell.Margin;
					if (margin.Left >= spaceToFree)
					{
						margin.Left -= spaceToFree;
						margin.Right = 0;
						spaceToFree = 0;
					}
					else
					{
						spaceToFree -= nextVisibleCell.Margin.Left;
						margin.Left = 0;
						margin.Right = 0;
					}
					nextVisibleCell.Margin = margin;
					spaceToFree -= this.MoveLeft(base.Row.Cells.Count - 1, spaceToFree);
					if (spaceToFree > 0)
					{
						spaceToFree -= nextVisibleCell.Shrink(spaceToFree);
					}
				}
				return num - Math.Max(0, spaceToFree);
			}

			// Token: 0x06006F57 RID: 28503 RVA: 0x00197A6C File Offset: 0x00195C6C
			public override void MoveControl(ToolStrip movingControl, Point clientStartLocation, Point clientEndLocation)
			{
				if (base.Row.Locked)
				{
					return;
				}
				if (!this.DragBounds.Contains(clientEndLocation))
				{
					base.MoveControl(movingControl, clientStartLocation, clientEndLocation);
					return;
				}
				int num = base.Row.ControlsInternal.IndexOf(movingControl);
				int num2 = clientEndLocation.X - clientStartLocation.X;
				if (num2 < 0)
				{
					this.MoveLeft(num, num2 * -1);
					return;
				}
				this.MoveRight(num, num2);
			}

			// Token: 0x06006F58 RID: 28504 RVA: 0x00197AE0 File Offset: 0x00195CE0
			private int MoveLeft(int index, int spaceToFree)
			{
				int num = 0;
				base.Row.SuspendLayout();
				try
				{
					if (spaceToFree == 0 || index < 0)
					{
						return 0;
					}
					for (int i = index; i >= 0; i--)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[i];
						if (toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode)
						{
							int num2 = spaceToFree - num;
							Padding padding = toolStripPanelCell.Margin;
							if (padding.Horizontal >= num2)
							{
								num += num2;
								padding.Left -= num2;
								padding.Right = 0;
								toolStripPanelCell.Margin = padding;
							}
							else
							{
								num += toolStripPanelCell.Margin.Horizontal;
								padding.Left = 0;
								padding.Right = 0;
								toolStripPanelCell.Margin = padding;
							}
							if (num >= spaceToFree)
							{
								if (index + 1 < base.Row.Cells.Count)
								{
									toolStripPanelCell = base.GetNextVisibleCell(index + 1, true);
									if (toolStripPanelCell != null)
									{
										padding = toolStripPanelCell.Margin;
										padding.Left += spaceToFree;
										toolStripPanelCell.Margin = padding;
									}
								}
								return spaceToFree;
							}
						}
					}
				}
				finally
				{
					base.Row.ResumeLayout(true);
				}
				return num;
			}

			// Token: 0x06006F59 RID: 28505 RVA: 0x00197C1C File Offset: 0x00195E1C
			private int MoveRight(int index, int spaceToFree)
			{
				int num = 0;
				base.Row.SuspendLayout();
				try
				{
					if (spaceToFree == 0 || index < 0 || index >= base.Row.ControlsInternal.Count)
					{
						return 0;
					}
					int i = index + 1;
					while (i < base.Row.Cells.Count)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[i];
						if (toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode)
						{
							int num2 = spaceToFree - num;
							Padding padding = toolStripPanelCell.Margin;
							if (padding.Horizontal >= num2)
							{
								num += num2;
								padding.Left -= num2;
								padding.Right = 0;
								toolStripPanelCell.Margin = padding;
								break;
							}
							num += toolStripPanelCell.Margin.Horizontal;
							padding.Left = 0;
							padding.Right = 0;
							toolStripPanelCell.Margin = padding;
							break;
						}
						else
						{
							i++;
						}
					}
					if (base.Row.Cells.Count > 0 && spaceToFree > num)
					{
						ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
						if (nextVisibleCell != null)
						{
							num += this.DisplayRectangle.Right - nextVisibleCell.Bounds.Right;
						}
						else
						{
							num += this.DisplayRectangle.Width;
						}
					}
					if (spaceToFree <= num)
					{
						ToolStripPanelCell toolStripPanelCell = base.GetNextVisibleCell(index, true);
						if (toolStripPanelCell == null)
						{
							toolStripPanelCell = base.Row.Cells[index] as ToolStripPanelCell;
						}
						if (toolStripPanelCell != null)
						{
							Padding padding = toolStripPanelCell.Margin;
							padding.Left += spaceToFree;
							toolStripPanelCell.Margin = padding;
						}
						return spaceToFree;
					}
					for (int j = index + 1; j < base.Row.Cells.Count; j++)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[j];
						if (toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode)
						{
							int num3 = spaceToFree - num;
							num += toolStripPanelCell.Shrink(num3);
							if (spaceToFree >= num)
							{
								base.Row.ResumeLayout(true);
								return spaceToFree;
							}
						}
					}
					if (base.Row.Cells.Count == 1)
					{
						ToolStripPanelCell toolStripPanelCell = base.GetNextVisibleCell(index, true);
						if (toolStripPanelCell != null)
						{
							Padding padding = toolStripPanelCell.Margin;
							padding.Left += num;
							toolStripPanelCell.Margin = padding;
						}
					}
				}
				finally
				{
					base.Row.ResumeLayout(true);
				}
				return num;
			}

			// Token: 0x06006F5A RID: 28506 RVA: 0x00197EA0 File Offset: 0x001960A0
			public override void LeaveRow(ToolStrip toolStripToDrag)
			{
				base.Row.SuspendLayout();
				int num = base.Row.ControlsInternal.IndexOf(toolStripToDrag);
				if (num >= 0)
				{
					if (num < base.Row.ControlsInternal.Count - 1)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[num];
						if (toolStripPanelCell.Visible)
						{
							int num2 = toolStripPanelCell.Margin.Horizontal + toolStripPanelCell.Bounds.Width;
							ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(num + 1, true);
							if (nextVisibleCell != null)
							{
								Padding margin = nextVisibleCell.Margin;
								margin.Left += num2;
								nextVisibleCell.Margin = margin;
							}
						}
					}
					((IList)base.Row.Cells).RemoveAt(num);
				}
				base.Row.ResumeLayout(true);
			}

			// Token: 0x06006F5B RID: 28507 RVA: 0x000070A6 File Offset: 0x000052A6
			protected internal override void OnControlAdded(Control control, int index)
			{
			}

			// Token: 0x06006F5C RID: 28508 RVA: 0x000070A6 File Offset: 0x000052A6
			protected internal override void OnControlRemoved(Control control, int index)
			{
			}

			// Token: 0x06006F5D RID: 28509 RVA: 0x00197F70 File Offset: 0x00196170
			public override void JoinRow(ToolStrip toolStripToDrag, Point locationToDrag)
			{
				if (!base.Row.ControlsInternal.Contains(toolStripToDrag))
				{
					base.Row.SuspendLayout();
					try
					{
						if (base.Row.ControlsInternal.Count > 0)
						{
							int i;
							for (i = 0; i < base.Row.Cells.Count; i++)
							{
								ToolStripPanelCell toolStripPanelCell = base.Row.Cells[i] as ToolStripPanelCell;
								if ((toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode) && (base.Row.Cells[i].Bounds.Contains(locationToDrag) || base.Row.Cells[i].Bounds.X >= locationToDrag.X))
								{
									break;
								}
							}
							Control control = base.Row.ControlsInternal[i];
							if (i < base.Row.ControlsInternal.Count)
							{
								base.Row.ControlsInternal.Insert(i, toolStripToDrag);
							}
							else
							{
								base.Row.ControlsInternal.Add(toolStripToDrag);
							}
							int num = (toolStripToDrag.AutoSize ? toolStripToDrag.PreferredSize.Width : toolStripToDrag.Width);
							int num2 = num;
							if (i == 0)
							{
								num2 += locationToDrag.X;
							}
							int num3 = 0;
							if (i < base.Row.ControlsInternal.Count - 1)
							{
								ToolStripPanelCell toolStripPanelCell2 = (ToolStripPanelCell)base.Row.Cells[i + 1];
								Padding padding = toolStripPanelCell2.Margin;
								if (padding.Left > num2)
								{
									padding.Left -= num2;
									toolStripPanelCell2.Margin = padding;
									num3 = num2;
								}
								else
								{
									num3 = this.MoveRight(i + 1, num2 - num3);
									if (num3 > 0)
									{
										padding = toolStripPanelCell2.Margin;
										padding.Left = Math.Max(0, padding.Left - num3);
										toolStripPanelCell2.Margin = padding;
									}
								}
							}
							else
							{
								ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(base.Row.Cells.Count - 2, false);
								ToolStripPanelCell nextVisibleCell2 = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
								if (nextVisibleCell != null && nextVisibleCell2 != null)
								{
									Padding margin = nextVisibleCell2.Margin;
									margin.Left = Math.Max(0, locationToDrag.X - nextVisibleCell.Bounds.Right);
									nextVisibleCell2.Margin = margin;
									num3 = num2;
								}
							}
							if (num3 < num2 && i > 0)
							{
								num3 = this.MoveLeft(i - 1, num2 - num3);
							}
							if (i == 0 && num3 - num > 0)
							{
								ToolStripPanelCell toolStripPanelCell3 = base.Row.Cells[i] as ToolStripPanelCell;
								Padding margin2 = toolStripPanelCell3.Margin;
								margin2.Left = num3 - num;
								toolStripPanelCell3.Margin = margin2;
							}
						}
						else
						{
							base.Row.ControlsInternal.Add(toolStripToDrag);
							if (base.Row.Cells.Count > 0 || toolStripToDrag.IsInDesignMode)
							{
								ToolStripPanelCell toolStripPanelCell4 = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
								if (toolStripPanelCell4 == null && toolStripToDrag.IsInDesignMode)
								{
									toolStripPanelCell4 = (ToolStripPanelCell)base.Row.Cells[base.Row.Cells.Count - 1];
								}
								if (toolStripPanelCell4 != null)
								{
									Padding margin3 = toolStripPanelCell4.Margin;
									margin3.Left = Math.Max(0, locationToDrag.X - base.Row.Margin.Left);
									toolStripPanelCell4.Margin = margin3;
								}
							}
						}
					}
					finally
					{
						base.Row.ResumeLayout(true);
					}
				}
			}

			// Token: 0x06006F5E RID: 28510 RVA: 0x00198330 File Offset: 0x00196530
			protected internal override void OnBoundsChanged(Rectangle oldBounds, Rectangle newBounds)
			{
				base.OnBoundsChanged(oldBounds, newBounds);
			}

			// Token: 0x0400431A RID: 17178
			private const int DRAG_BOUNDS_INFLATE = 4;
		}

		// Token: 0x02000812 RID: 2066
		private class VerticalRowManager : ToolStripPanelRow.ToolStripPanelRowManager
		{
			// Token: 0x06006F5F RID: 28511 RVA: 0x0019833A File Offset: 0x0019653A
			public VerticalRowManager(ToolStripPanelRow owner)
				: base(owner)
			{
				owner.SuspendLayout();
				base.FlowLayoutSettings.WrapContents = false;
				base.FlowLayoutSettings.FlowDirection = FlowDirection.TopDown;
				owner.ResumeLayout(false);
			}

			// Token: 0x1700185D RID: 6237
			// (get) Token: 0x06006F60 RID: 28512 RVA: 0x00198368 File Offset: 0x00196568
			public override Rectangle DisplayRectangle
			{
				get
				{
					Rectangle displayRectangle = ((IArrangedElement)base.Row).DisplayRectangle;
					if (base.ToolStripPanel != null)
					{
						Rectangle displayRectangle2 = base.ToolStripPanel.DisplayRectangle;
						if ((!base.ToolStripPanel.Visible || LayoutUtils.IsZeroWidthOrHeight(displayRectangle2)) && base.ToolStripPanel.ParentInternal != null)
						{
							displayRectangle.Height = base.ToolStripPanel.ParentInternal.DisplayRectangle.Height - (base.ToolStripPanel.Margin.Vertical + base.ToolStripPanel.Padding.Vertical) - base.Row.Margin.Vertical;
						}
						else
						{
							displayRectangle.Height = displayRectangle2.Height - base.Row.Margin.Vertical;
						}
					}
					return displayRectangle;
				}
			}

			// Token: 0x1700185E RID: 6238
			// (get) Token: 0x06006F61 RID: 28513 RVA: 0x0019843C File Offset: 0x0019663C
			public override Rectangle DragBounds
			{
				get
				{
					Rectangle bounds = base.Row.Bounds;
					int num = base.ToolStripPanel.RowsInternal.IndexOf(base.Row);
					if (num > 0)
					{
						Rectangle bounds2 = base.ToolStripPanel.RowsInternal[num - 1].Bounds;
						int num2 = bounds2.X + bounds2.Width - (bounds2.Width >> 2);
						bounds.Width += bounds.X - num2;
						bounds.X = num2;
					}
					if (num < base.ToolStripPanel.RowsInternal.Count - 1)
					{
						Rectangle bounds3 = base.ToolStripPanel.RowsInternal[num + 1].Bounds;
						bounds.Width += (bounds3.Width >> 2) + base.Row.Margin.Right + base.ToolStripPanel.RowsInternal[num + 1].Margin.Left;
					}
					bounds.Height += base.Row.Margin.Vertical + base.ToolStripPanel.Padding.Vertical + 5;
					bounds.Y -= base.Row.Margin.Top + base.ToolStripPanel.Padding.Top + 4;
					return bounds;
				}
			}

			// Token: 0x06006F62 RID: 28514 RVA: 0x001985B4 File Offset: 0x001967B4
			public override bool CanMove(ToolStrip toolStripToDrag)
			{
				if (base.CanMove(toolStripToDrag))
				{
					Size size = Size.Empty;
					for (int i = 0; i < base.Row.ControlsInternal.Count; i++)
					{
						size += base.Row.GetMinimumSize(base.Row.ControlsInternal[i] as ToolStrip);
					}
					return (size + base.Row.GetMinimumSize(toolStripToDrag)).Height < this.DisplayRectangle.Height;
				}
				return false;
			}

			// Token: 0x06006F63 RID: 28515 RVA: 0x00198640 File Offset: 0x00196840
			protected internal override int FreeSpaceFromRow(int spaceToFree)
			{
				int num = spaceToFree;
				if (spaceToFree > 0)
				{
					ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
					if (nextVisibleCell == null)
					{
						return 0;
					}
					Padding margin = nextVisibleCell.Margin;
					if (margin.Top >= spaceToFree)
					{
						margin.Top -= spaceToFree;
						margin.Bottom = 0;
						spaceToFree = 0;
					}
					else
					{
						spaceToFree -= nextVisibleCell.Margin.Top;
						margin.Top = 0;
						margin.Bottom = 0;
					}
					nextVisibleCell.Margin = margin;
					spaceToFree -= this.MoveUp(base.Row.Cells.Count - 1, spaceToFree);
					if (spaceToFree > 0)
					{
						spaceToFree -= nextVisibleCell.Shrink(spaceToFree);
					}
				}
				return num - Math.Max(0, spaceToFree);
			}

			// Token: 0x06006F64 RID: 28516 RVA: 0x00198700 File Offset: 0x00196900
			public override void MoveControl(ToolStrip movingControl, Point clientStartLocation, Point clientEndLocation)
			{
				if (base.Row.Locked)
				{
					return;
				}
				if (!this.DragBounds.Contains(clientEndLocation))
				{
					base.MoveControl(movingControl, clientStartLocation, clientEndLocation);
					return;
				}
				int num = base.Row.ControlsInternal.IndexOf(movingControl);
				int num2 = clientEndLocation.Y - clientStartLocation.Y;
				if (num2 < 0)
				{
					this.MoveUp(num, num2 * -1);
					return;
				}
				this.MoveDown(num, num2);
			}

			// Token: 0x06006F65 RID: 28517 RVA: 0x00198774 File Offset: 0x00196974
			private int MoveUp(int index, int spaceToFree)
			{
				int num = 0;
				base.Row.SuspendLayout();
				try
				{
					if (spaceToFree == 0 || index < 0)
					{
						return 0;
					}
					for (int i = index; i >= 0; i--)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[i];
						if (toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode)
						{
							int num2 = spaceToFree - num;
							Padding padding = toolStripPanelCell.Margin;
							if (padding.Vertical >= num2)
							{
								num += num2;
								padding.Top -= num2;
								padding.Bottom = 0;
								toolStripPanelCell.Margin = padding;
							}
							else
							{
								num += toolStripPanelCell.Margin.Vertical;
								padding.Top = 0;
								padding.Bottom = 0;
								toolStripPanelCell.Margin = padding;
							}
							if (num >= spaceToFree)
							{
								if (index + 1 < base.Row.Cells.Count)
								{
									toolStripPanelCell = base.GetNextVisibleCell(index + 1, true);
									if (toolStripPanelCell != null)
									{
										padding = toolStripPanelCell.Margin;
										padding.Top += spaceToFree;
										toolStripPanelCell.Margin = padding;
									}
								}
								return spaceToFree;
							}
						}
					}
				}
				finally
				{
					base.Row.ResumeLayout(true);
				}
				return num;
			}

			// Token: 0x06006F66 RID: 28518 RVA: 0x001988B0 File Offset: 0x00196AB0
			private int MoveDown(int index, int spaceToFree)
			{
				int num = 0;
				base.Row.SuspendLayout();
				try
				{
					if (spaceToFree == 0 || index < 0 || index >= base.Row.ControlsInternal.Count)
					{
						return 0;
					}
					int i = index + 1;
					while (i < base.Row.Cells.Count)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[i];
						if (toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode)
						{
							int num2 = spaceToFree - num;
							Padding padding = toolStripPanelCell.Margin;
							if (padding.Vertical >= num2)
							{
								num += num2;
								padding.Top -= num2;
								padding.Bottom = 0;
								toolStripPanelCell.Margin = padding;
								break;
							}
							num += toolStripPanelCell.Margin.Vertical;
							padding.Top = 0;
							padding.Bottom = 0;
							toolStripPanelCell.Margin = padding;
							break;
						}
						else
						{
							i++;
						}
					}
					if (base.Row.Cells.Count > 0 && spaceToFree > num)
					{
						ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
						if (nextVisibleCell != null)
						{
							num += this.DisplayRectangle.Bottom - nextVisibleCell.Bounds.Bottom;
						}
						else
						{
							num += this.DisplayRectangle.Height;
						}
					}
					if (spaceToFree <= num)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[index];
						Padding padding = toolStripPanelCell.Margin;
						padding.Top += spaceToFree;
						toolStripPanelCell.Margin = padding;
						return spaceToFree;
					}
					for (int j = index + 1; j < base.Row.Cells.Count; j++)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[j];
						if (toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode)
						{
							int num3 = spaceToFree - num;
							num += toolStripPanelCell.Shrink(num3);
							if (spaceToFree >= num)
							{
								base.Row.ResumeLayout(true);
								return spaceToFree;
							}
						}
					}
					if (base.Row.Cells.Count == 1)
					{
						ToolStripPanelCell toolStripPanelCell = base.GetNextVisibleCell(index, true);
						if (toolStripPanelCell != null)
						{
							Padding padding = toolStripPanelCell.Margin;
							padding.Top += num;
							toolStripPanelCell.Margin = padding;
						}
					}
				}
				finally
				{
					base.Row.ResumeLayout(true);
				}
				return spaceToFree - num;
			}

			// Token: 0x06006F67 RID: 28519 RVA: 0x00198B30 File Offset: 0x00196D30
			protected internal override void OnBoundsChanged(Rectangle oldBounds, Rectangle newBounds)
			{
				base.OnBoundsChanged(oldBounds, newBounds);
				if (base.Row.Cells.Count > 0)
				{
					ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
					int num = ((nextVisibleCell != null) ? (nextVisibleCell.Bounds.Bottom - newBounds.Height) : 0);
					if (num > 0)
					{
						ToolStripPanelCell nextVisibleCell2 = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
						Padding margin = nextVisibleCell2.Margin;
						if (margin.Top >= num)
						{
							margin.Top -= num;
							margin.Bottom = 0;
							nextVisibleCell2.Margin = margin;
							num = 0;
						}
						else
						{
							num -= nextVisibleCell2.Margin.Top;
							margin.Top = 0;
							margin.Bottom = 0;
							nextVisibleCell2.Margin = margin;
						}
						num -= nextVisibleCell2.Shrink(num);
						this.MoveUp(base.Row.Cells.Count - 1, num);
					}
				}
			}

			// Token: 0x06006F68 RID: 28520 RVA: 0x000070A6 File Offset: 0x000052A6
			protected internal override void OnControlRemoved(Control c, int index)
			{
			}

			// Token: 0x06006F69 RID: 28521 RVA: 0x000070A6 File Offset: 0x000052A6
			protected internal override void OnControlAdded(Control control, int index)
			{
			}

			// Token: 0x06006F6A RID: 28522 RVA: 0x00198C38 File Offset: 0x00196E38
			public override void JoinRow(ToolStrip toolStripToDrag, Point locationToDrag)
			{
				if (!base.Row.ControlsInternal.Contains(toolStripToDrag))
				{
					base.Row.SuspendLayout();
					try
					{
						if (base.Row.ControlsInternal.Count > 0)
						{
							int i;
							for (i = 0; i < base.Row.Cells.Count; i++)
							{
								ToolStripPanelCell toolStripPanelCell = base.Row.Cells[i] as ToolStripPanelCell;
								if ((toolStripPanelCell.Visible || toolStripPanelCell.ControlInDesignMode) && (toolStripPanelCell.Bounds.Contains(locationToDrag) || toolStripPanelCell.Bounds.Y >= locationToDrag.Y))
								{
									break;
								}
							}
							Control control = base.Row.ControlsInternal[i];
							if (i < base.Row.ControlsInternal.Count)
							{
								base.Row.ControlsInternal.Insert(i, toolStripToDrag);
							}
							else
							{
								base.Row.ControlsInternal.Add(toolStripToDrag);
							}
							int num = (toolStripToDrag.AutoSize ? toolStripToDrag.PreferredSize.Height : toolStripToDrag.Height);
							int num2 = num;
							if (i == 0)
							{
								num2 += locationToDrag.Y;
							}
							int num3 = 0;
							if (i < base.Row.ControlsInternal.Count - 1)
							{
								ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(i + 1, true);
								if (nextVisibleCell != null)
								{
									Padding padding = nextVisibleCell.Margin;
									if (padding.Top > num2)
									{
										padding.Top -= num2;
										nextVisibleCell.Margin = padding;
										num3 = num2;
									}
									else
									{
										num3 = this.MoveDown(i + 1, num2 - num3);
										if (num3 > 0)
										{
											padding = nextVisibleCell.Margin;
											padding.Top -= num3;
											nextVisibleCell.Margin = padding;
										}
									}
								}
							}
							else
							{
								ToolStripPanelCell nextVisibleCell2 = base.GetNextVisibleCell(base.Row.Cells.Count - 2, false);
								ToolStripPanelCell nextVisibleCell3 = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
								if (nextVisibleCell2 != null && nextVisibleCell3 != null)
								{
									Padding margin = nextVisibleCell3.Margin;
									margin.Top = Math.Max(0, locationToDrag.Y - nextVisibleCell2.Bounds.Bottom);
									nextVisibleCell3.Margin = margin;
									num3 = num2;
								}
							}
							if (num3 < num2 && i > 0)
							{
								num3 = this.MoveUp(i - 1, num2 - num3);
							}
							if (i == 0 && num3 - num > 0)
							{
								ToolStripPanelCell toolStripPanelCell2 = base.Row.Cells[i] as ToolStripPanelCell;
								Padding margin2 = toolStripPanelCell2.Margin;
								margin2.Top = num3 - num;
								toolStripPanelCell2.Margin = margin2;
							}
						}
						else
						{
							base.Row.ControlsInternal.Add(toolStripToDrag);
							if (base.Row.Cells.Count > 0)
							{
								ToolStripPanelCell nextVisibleCell4 = base.GetNextVisibleCell(base.Row.Cells.Count - 1, false);
								if (nextVisibleCell4 != null)
								{
									Padding margin3 = nextVisibleCell4.Margin;
									margin3.Top = Math.Max(0, locationToDrag.Y - base.Row.Margin.Top);
									nextVisibleCell4.Margin = margin3;
								}
							}
						}
					}
					finally
					{
						base.Row.ResumeLayout(true);
					}
				}
			}

			// Token: 0x06006F6B RID: 28523 RVA: 0x00198F88 File Offset: 0x00197188
			public override void LeaveRow(ToolStrip toolStripToDrag)
			{
				base.Row.SuspendLayout();
				int num = base.Row.ControlsInternal.IndexOf(toolStripToDrag);
				if (num >= 0)
				{
					if (num < base.Row.ControlsInternal.Count - 1)
					{
						ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.Row.Cells[num];
						if (toolStripPanelCell.Visible)
						{
							int num2 = toolStripPanelCell.Margin.Vertical + toolStripPanelCell.Bounds.Height;
							ToolStripPanelCell nextVisibleCell = base.GetNextVisibleCell(num + 1, true);
							if (nextVisibleCell != null)
							{
								Padding margin = nextVisibleCell.Margin;
								margin.Top += num2;
								nextVisibleCell.Margin = margin;
							}
						}
					}
					((IList)base.Row.Cells).RemoveAt(num);
				}
				base.Row.ResumeLayout(true);
			}

			// Token: 0x0400431B RID: 17179
			private const int DRAG_BOUNDS_INFLATE = 4;
		}

		// Token: 0x02000813 RID: 2067
		internal class ToolStripPanelRowControlCollection : ArrangedElementCollection, IList, ICollection, IEnumerable
		{
			// Token: 0x06006F6C RID: 28524 RVA: 0x00199057 File Offset: 0x00197257
			public ToolStripPanelRowControlCollection(ToolStripPanelRow owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006F6D RID: 28525 RVA: 0x00199066 File Offset: 0x00197266
			public ToolStripPanelRowControlCollection(ToolStripPanelRow owner, Control[] value)
			{
				this.owner = owner;
				this.AddRange(value);
			}

			// Token: 0x1700185F RID: 6239
			public virtual Control this[int index]
			{
				get
				{
					return this.GetControl(index);
				}
			}

			// Token: 0x17001860 RID: 6240
			// (get) Token: 0x06006F6F RID: 28527 RVA: 0x00199085 File Offset: 0x00197285
			public ArrangedElementCollection Cells
			{
				get
				{
					if (this.cellCollection == null)
					{
						this.cellCollection = new ArrangedElementCollection(base.InnerList);
					}
					return this.cellCollection;
				}
			}

			// Token: 0x17001861 RID: 6241
			// (get) Token: 0x06006F70 RID: 28528 RVA: 0x001990A6 File Offset: 0x001972A6
			public ToolStripPanel ToolStripPanel
			{
				get
				{
					return this.owner.ToolStripPanel;
				}
			}

			// Token: 0x06006F71 RID: 28529 RVA: 0x001990B4 File Offset: 0x001972B4
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int Add(Control value)
			{
				ISupportToolStripPanel supportToolStripPanel = value as ISupportToolStripPanel;
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (supportToolStripPanel == null)
				{
					throw new NotSupportedException(SR.GetString("TypedControlCollectionShouldBeOfType", new object[] { typeof(ToolStrip).Name }));
				}
				int num = base.InnerList.Add(supportToolStripPanel.ToolStripPanelCell);
				this.OnAdd(supportToolStripPanel, num);
				return num;
			}

			// Token: 0x06006F72 RID: 28530 RVA: 0x0019911C File Offset: 0x0019731C
			[EditorBrowsable(EditorBrowsableState.Never)]
			public void AddRange(Control[] value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ToolStripPanel toolStripPanel = this.ToolStripPanel;
				if (toolStripPanel != null)
				{
					toolStripPanel.SuspendLayout();
				}
				try
				{
					for (int i = 0; i < value.Length; i++)
					{
						this.Add(value[i]);
					}
				}
				finally
				{
					if (toolStripPanel != null)
					{
						toolStripPanel.ResumeLayout();
					}
				}
			}

			// Token: 0x06006F73 RID: 28531 RVA: 0x0019917C File Offset: 0x0019737C
			public bool Contains(Control value)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this.GetControl(i) == value)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06006F74 RID: 28532 RVA: 0x001991A8 File Offset: 0x001973A8
			public virtual void Clear()
			{
				if (this.owner != null)
				{
					this.ToolStripPanel.SuspendLayout();
				}
				try
				{
					while (this.Count != 0)
					{
						this.RemoveAt(this.Count - 1);
					}
				}
				finally
				{
					if (this.owner != null)
					{
						this.ToolStripPanel.ResumeLayout();
					}
				}
			}

			// Token: 0x06006F75 RID: 28533 RVA: 0x00199208 File Offset: 0x00197408
			public override IEnumerator GetEnumerator()
			{
				return new ToolStripPanelRow.ToolStripPanelRowControlCollection.ToolStripPanelCellToControlEnumerator(base.InnerList);
			}

			// Token: 0x06006F76 RID: 28534 RVA: 0x00199218 File Offset: 0x00197418
			private Control GetControl(int index)
			{
				Control control = null;
				if (index < this.Count && index >= 0)
				{
					ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.InnerList[index];
					control = ((toolStripPanelCell != null) ? toolStripPanelCell.Control : null);
				}
				return control;
			}

			// Token: 0x06006F77 RID: 28535 RVA: 0x00199258 File Offset: 0x00197458
			private int IndexOfControl(Control c)
			{
				for (int i = 0; i < this.Count; i++)
				{
					ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)base.InnerList[i];
					if (toolStripPanelCell.Control == c)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006F78 RID: 28536 RVA: 0x00199294 File Offset: 0x00197494
			void IList.Clear()
			{
				this.Clear();
			}

			// Token: 0x17001862 RID: 6242
			// (get) Token: 0x06006F79 RID: 28537 RVA: 0x0011C9DC File Offset: 0x0011ABDC
			bool IList.IsFixedSize
			{
				get
				{
					return base.InnerList.IsFixedSize;
				}
			}

			// Token: 0x06006F7A RID: 28538 RVA: 0x0011C768 File Offset: 0x0011A968
			bool IList.Contains(object value)
			{
				return base.InnerList.Contains(value);
			}

			// Token: 0x17001863 RID: 6243
			// (get) Token: 0x06006F7B RID: 28539 RVA: 0x0014D3E3 File Offset: 0x0014B5E3
			bool IList.IsReadOnly
			{
				get
				{
					return base.InnerList.IsReadOnly;
				}
			}

			// Token: 0x06006F7C RID: 28540 RVA: 0x0019929C File Offset: 0x0019749C
			void IList.RemoveAt(int index)
			{
				this.RemoveAt(index);
			}

			// Token: 0x06006F7D RID: 28541 RVA: 0x001992A5 File Offset: 0x001974A5
			void IList.Remove(object value)
			{
				this.Remove(value as Control);
			}

			// Token: 0x06006F7E RID: 28542 RVA: 0x001992B3 File Offset: 0x001974B3
			int IList.Add(object value)
			{
				return this.Add(value as Control);
			}

			// Token: 0x06006F7F RID: 28543 RVA: 0x001992C1 File Offset: 0x001974C1
			int IList.IndexOf(object value)
			{
				return this.IndexOf(value as Control);
			}

			// Token: 0x06006F80 RID: 28544 RVA: 0x001992CF File Offset: 0x001974CF
			void IList.Insert(int index, object value)
			{
				this.Insert(index, value as Control);
			}

			// Token: 0x06006F81 RID: 28545 RVA: 0x001992E0 File Offset: 0x001974E0
			public int IndexOf(Control value)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this.GetControl(i) == value)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006F82 RID: 28546 RVA: 0x0019930C File Offset: 0x0019750C
			[EditorBrowsable(EditorBrowsableState.Never)]
			public void Insert(int index, Control value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ISupportToolStripPanel supportToolStripPanel = value as ISupportToolStripPanel;
				if (supportToolStripPanel == null)
				{
					throw new NotSupportedException(SR.GetString("TypedControlCollectionShouldBeOfType", new object[] { typeof(ToolStrip).Name }));
				}
				base.InnerList.Insert(index, supportToolStripPanel.ToolStripPanelCell);
				this.OnAdd(supportToolStripPanel, index);
			}

			// Token: 0x06006F83 RID: 28547 RVA: 0x00199374 File Offset: 0x00197574
			private void OnAfterRemove(Control control, int index)
			{
				if (this.owner != null)
				{
					using (new LayoutTransaction(this.ToolStripPanel, control, PropertyNames.Parent))
					{
						this.owner.ToolStripPanel.Controls.Remove(control);
						this.owner.OnControlRemoved(control, index);
					}
				}
			}

			// Token: 0x06006F84 RID: 28548 RVA: 0x001993DC File Offset: 0x001975DC
			private void OnAdd(ISupportToolStripPanel controlToBeDragged, int index)
			{
				if (this.owner != null)
				{
					LayoutTransaction layoutTransaction = null;
					if (this.ToolStripPanel != null && this.ToolStripPanel.ParentInternal != null)
					{
						layoutTransaction = new LayoutTransaction(this.ToolStripPanel, this.ToolStripPanel.ParentInternal, PropertyNames.Parent);
					}
					try
					{
						if (controlToBeDragged != null)
						{
							controlToBeDragged.ToolStripPanelRow = this.owner;
							Control control = controlToBeDragged as Control;
							if (control != null)
							{
								control.ParentInternal = this.owner.ToolStripPanel;
								this.owner.OnControlAdded(control, index);
							}
						}
					}
					finally
					{
						if (layoutTransaction != null)
						{
							layoutTransaction.Dispose();
						}
					}
				}
			}

			// Token: 0x06006F85 RID: 28549 RVA: 0x00199478 File Offset: 0x00197678
			[EditorBrowsable(EditorBrowsableState.Never)]
			public void Remove(Control value)
			{
				int num = this.IndexOfControl(value);
				this.RemoveAt(num);
			}

			// Token: 0x06006F86 RID: 28550 RVA: 0x00199494 File Offset: 0x00197694
			[EditorBrowsable(EditorBrowsableState.Never)]
			public void RemoveAt(int index)
			{
				if (index >= 0 && index < this.Count)
				{
					Control control = this.GetControl(index);
					ToolStripPanelCell toolStripPanelCell = base.InnerList[index] as ToolStripPanelCell;
					base.InnerList.RemoveAt(index);
					this.OnAfterRemove(control, index);
				}
			}

			// Token: 0x06006F87 RID: 28551 RVA: 0x001994DC File Offset: 0x001976DC
			[EditorBrowsable(EditorBrowsableState.Never)]
			public void CopyTo(Control[] array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (index >= array.Length || base.InnerList.Count > array.Length - index)
				{
					throw new ArgumentException(SR.GetString("ToolStripPanelRowControlCollectionIncorrectIndexLength"));
				}
				for (int i = 0; i < base.InnerList.Count; i++)
				{
					array[index++] = this.GetControl(i);
				}
			}

			// Token: 0x0400431C RID: 17180
			private ToolStripPanelRow owner;

			// Token: 0x0400431D RID: 17181
			private ArrangedElementCollection cellCollection;

			// Token: 0x020008CD RID: 2253
			private class ToolStripPanelCellToControlEnumerator : IEnumerator, ICloneable
			{
				// Token: 0x060072E3 RID: 29411 RVA: 0x001A38E0 File Offset: 0x001A1AE0
				internal ToolStripPanelCellToControlEnumerator(ArrayList list)
				{
					this.arrayListEnumerator = ((IEnumerable)list).GetEnumerator();
				}

				// Token: 0x1700193C RID: 6460
				// (get) Token: 0x060072E4 RID: 29412 RVA: 0x001A38F4 File Offset: 0x001A1AF4
				public virtual object Current
				{
					get
					{
						ToolStripPanelCell toolStripPanelCell = this.arrayListEnumerator.Current as ToolStripPanelCell;
						if (toolStripPanelCell != null)
						{
							return toolStripPanelCell.Control;
						}
						return null;
					}
				}

				// Token: 0x060072E5 RID: 29413 RVA: 0x001A391D File Offset: 0x001A1B1D
				public object Clone()
				{
					return base.MemberwiseClone();
				}

				// Token: 0x060072E6 RID: 29414 RVA: 0x001A3925 File Offset: 0x001A1B25
				public virtual bool MoveNext()
				{
					return this.arrayListEnumerator.MoveNext();
				}

				// Token: 0x060072E7 RID: 29415 RVA: 0x001A3932 File Offset: 0x001A1B32
				public virtual void Reset()
				{
					this.arrayListEnumerator.Reset();
				}

				// Token: 0x04004557 RID: 17751
				private IEnumerator arrayListEnumerator;
			}
		}
	}
}
