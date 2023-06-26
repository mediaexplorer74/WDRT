using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents a cell that contains a link.</summary>
	// Token: 0x02000203 RID: 515
	public class DataGridViewLinkCell : DataGridViewCell
	{
		/// <summary>Gets or sets the color used to display an active link.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display a link that is being selected. The default value is the user's Internet Explorer setting for the color of links in the hover state.</returns>
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x0009DEF4 File Offset: 0x0009C0F4
		// (set) Token: 0x06002181 RID: 8577 RVA: 0x0009DF60 File Offset: 0x0009C160
		public Color ActiveLinkColor
		{
			get
			{
				if (base.Properties.ContainsObject(DataGridViewLinkCell.PropLinkCellActiveLinkColor))
				{
					return (Color)base.Properties.GetObject(DataGridViewLinkCell.PropLinkCellActiveLinkColor);
				}
				if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
				{
					return this.HighContrastLinkColor;
				}
				if (!AccessibilityImprovements.Level5)
				{
					return LinkUtilities.IEActiveLinkColor;
				}
				if (!this.Selected)
				{
					return LinkUtilities.IEActiveLinkColor;
				}
				return SystemColors.HighlightText;
			}
			set
			{
				if (!value.Equals(this.ActiveLinkColor))
				{
					base.Properties.SetObject(DataGridViewLinkCell.PropLinkCellActiveLinkColor, value);
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x17000789 RID: 1929
		// (set) Token: 0x06002182 RID: 8578 RVA: 0x0009DFCC File Offset: 0x0009C1CC
		internal Color ActiveLinkColorInternal
		{
			set
			{
				if (!value.Equals(this.ActiveLinkColor))
				{
					base.Properties.SetObject(DataGridViewLinkCell.PropLinkCellActiveLinkColor, value);
				}
			}
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x0009E000 File Offset: 0x0009C200
		private bool ShouldSerializeActiveLinkColor()
		{
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
			{
				return !this.ActiveLinkColor.Equals(SystemColors.HotTrack);
			}
			return !this.ActiveLinkColor.Equals(LinkUtilities.IEActiveLinkColor);
		}

		/// <summary>Gets the type of the cell's hosted editing control.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x00015C90 File Offset: 0x00013E90
		public override Type EditType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the display <see cref="T:System.Type" /> of the cell value.</summary>
		/// <returns>The display <see cref="T:System.Type" /> of the cell value.</returns>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x0009E05E File Offset: 0x0009C25E
		public override Type FormattedValueType
		{
			get
			{
				return DataGridViewLinkCell.defaultFormattedValueType;
			}
		}

		/// <summary>Gets or sets a value that represents the behavior of a link.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.LinkBehavior" /> values. The default is <see cref="F:System.Windows.Forms.LinkBehavior.SystemDefault" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.LinkBehavior" /> value.</exception>
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x0009E068 File Offset: 0x0009C268
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x0009E090 File Offset: 0x0009C290
		[DefaultValue(LinkBehavior.SystemDefault)]
		public LinkBehavior LinkBehavior
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewLinkCell.PropLinkCellLinkBehavior, out flag);
				if (flag)
				{
					return (LinkBehavior)integer;
				}
				return LinkBehavior.SystemDefault;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(LinkBehavior));
				}
				if (value != this.LinkBehavior)
				{
					base.Properties.SetInteger(DataGridViewLinkCell.PropLinkCellLinkBehavior, (int)value);
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x1700078D RID: 1933
		// (set) Token: 0x06002188 RID: 8584 RVA: 0x0009E10C File Offset: 0x0009C30C
		internal LinkBehavior LinkBehaviorInternal
		{
			set
			{
				if (value != this.LinkBehavior)
				{
					base.Properties.SetInteger(DataGridViewLinkCell.PropLinkCellLinkBehavior, (int)value);
				}
			}
		}

		/// <summary>Gets or sets the color used to display an inactive and unvisited link.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to initially display a link. The default value is the user's Internet Explorer setting for the link color.</returns>
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x0009E128 File Offset: 0x0009C328
		// (set) Token: 0x0600218A RID: 8586 RVA: 0x0009E194 File Offset: 0x0009C394
		public Color LinkColor
		{
			get
			{
				if (base.Properties.ContainsObject(DataGridViewLinkCell.PropLinkCellLinkColor))
				{
					return (Color)base.Properties.GetObject(DataGridViewLinkCell.PropLinkCellLinkColor);
				}
				if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
				{
					return this.HighContrastLinkColor;
				}
				if (!AccessibilityImprovements.Level5)
				{
					return LinkUtilities.IELinkColor;
				}
				if (!this.Selected)
				{
					return LinkUtilities.IELinkColor;
				}
				return SystemColors.HighlightText;
			}
			set
			{
				if (!value.Equals(this.LinkColor))
				{
					base.Properties.SetObject(DataGridViewLinkCell.PropLinkCellLinkColor, value);
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x1700078F RID: 1935
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x0009E200 File Offset: 0x0009C400
		internal Color LinkColorInternal
		{
			set
			{
				if (!value.Equals(this.LinkColor))
				{
					base.Properties.SetObject(DataGridViewLinkCell.PropLinkCellLinkColor, value);
				}
			}
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x0009E234 File Offset: 0x0009C434
		private bool ShouldSerializeLinkColor()
		{
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
			{
				return !this.LinkColor.Equals(SystemColors.HotTrack);
			}
			return !this.LinkColor.Equals(LinkUtilities.IELinkColor);
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x0009E294 File Offset: 0x0009C494
		// (set) Token: 0x0600218E RID: 8590 RVA: 0x0009E2BA File Offset: 0x0009C4BA
		private LinkState LinkState
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewLinkCell.PropLinkCellLinkState, out flag);
				if (flag)
				{
					return (LinkState)integer;
				}
				return LinkState.Normal;
			}
			set
			{
				if (this.LinkState != value)
				{
					base.Properties.SetInteger(DataGridViewLinkCell.PropLinkCellLinkState, (int)value);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the link was visited.</summary>
		/// <returns>
		///   <see langword="true" /> if the link has been visited; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x0009E2D6 File Offset: 0x0009C4D6
		// (set) Token: 0x06002190 RID: 8592 RVA: 0x0009E2E8 File Offset: 0x0009C4E8
		public bool LinkVisited
		{
			get
			{
				return this.linkVisitedSet && this.linkVisited;
			}
			set
			{
				this.linkVisitedSet = true;
				if (value != this.LinkVisited)
				{
					this.linkVisited = value;
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x0009E33C File Offset: 0x0009C53C
		private bool ShouldSerializeLinkVisited()
		{
			return this.linkVisitedSet = true;
		}

		/// <summary>Gets or sets a value indicating whether the link changes color when it is visited.</summary>
		/// <returns>
		///   <see langword="true" /> if the link changes color when it is selected; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x0009E354 File Offset: 0x0009C554
		// (set) Token: 0x06002193 RID: 8595 RVA: 0x0009E380 File Offset: 0x0009C580
		[DefaultValue(true)]
		public bool TrackVisitedState
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewLinkCell.PropLinkCellTrackVisitedState, out flag);
				return !flag || integer != 0;
			}
			set
			{
				if (value != this.TrackVisitedState)
				{
					base.Properties.SetInteger(DataGridViewLinkCell.PropLinkCellTrackVisitedState, value ? 1 : 0);
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x17000793 RID: 1939
		// (set) Token: 0x06002194 RID: 8596 RVA: 0x0009E3DC File Offset: 0x0009C5DC
		internal bool TrackVisitedStateInternal
		{
			set
			{
				if (value != this.TrackVisitedState)
				{
					base.Properties.SetInteger(DataGridViewLinkCell.PropLinkCellTrackVisitedState, value ? 1 : 0);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the column <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.Text" /> property value is displayed as the link text.</summary>
		/// <returns>
		///   <see langword="true" /> if the column <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.Text" /> property value is displayed as the link text; <see langword="false" /> if the cell <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValue" /> property value is displayed as the link text. The default is <see langword="false" />.</returns>
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x0009E400 File Offset: 0x0009C600
		// (set) Token: 0x06002196 RID: 8598 RVA: 0x0009E42B File Offset: 0x0009C62B
		[DefaultValue(false)]
		public bool UseColumnTextForLinkValue
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(DataGridViewLinkCell.PropLinkCellUseColumnTextForLinkValue, out flag);
				return flag && integer != 0;
			}
			set
			{
				if (value != this.UseColumnTextForLinkValue)
				{
					base.Properties.SetInteger(DataGridViewLinkCell.PropLinkCellUseColumnTextForLinkValue, value ? 1 : 0);
					base.OnCommonChange();
				}
			}
		}

		// Token: 0x17000795 RID: 1941
		// (set) Token: 0x06002197 RID: 8599 RVA: 0x0009E453 File Offset: 0x0009C653
		internal bool UseColumnTextForLinkValueInternal
		{
			set
			{
				if (value != this.UseColumnTextForLinkValue)
				{
					base.Properties.SetInteger(DataGridViewLinkCell.PropLinkCellUseColumnTextForLinkValue, value ? 1 : 0);
				}
			}
		}

		/// <summary>Gets or sets the color used to display a link that has been previously visited.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display a link that has been visited. The default value is the user's Internet Explorer setting for the visited link color.</returns>
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x0009E478 File Offset: 0x0009C678
		// (set) Token: 0x06002199 RID: 8601 RVA: 0x0009E4F0 File Offset: 0x0009C6F0
		public Color VisitedLinkColor
		{
			get
			{
				if (base.Properties.ContainsObject(DataGridViewLinkCell.PropLinkCellVisitedLinkColor))
				{
					return (Color)base.Properties.GetObject(DataGridViewLinkCell.PropLinkCellVisitedLinkColor);
				}
				if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
				{
					if (!this.Selected)
					{
						return LinkUtilities.GetVisitedLinkColor();
					}
					return SystemColors.HighlightText;
				}
				else
				{
					if (!AccessibilityImprovements.Level5)
					{
						return LinkUtilities.IEVisitedLinkColor;
					}
					if (!this.Selected)
					{
						return LinkUtilities.IEVisitedLinkColor;
					}
					return SystemColors.HighlightText;
				}
			}
			set
			{
				if (!value.Equals(this.VisitedLinkColor))
				{
					base.Properties.SetObject(DataGridViewLinkCell.PropLinkCellVisitedLinkColor, value);
					if (base.DataGridView != null)
					{
						if (base.RowIndex != -1)
						{
							base.DataGridView.InvalidateCell(this);
							return;
						}
						base.DataGridView.InvalidateColumnInternal(base.ColumnIndex);
					}
				}
			}
		}

		// Token: 0x17000797 RID: 1943
		// (set) Token: 0x0600219A RID: 8602 RVA: 0x0009E55C File Offset: 0x0009C75C
		internal Color VisitedLinkColorInternal
		{
			set
			{
				if (!value.Equals(this.VisitedLinkColor))
				{
					base.Properties.SetObject(DataGridViewLinkCell.PropLinkCellVisitedLinkColor, value);
				}
			}
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x0009E590 File Offset: 0x0009C790
		private bool ShouldSerializeVisitedLinkColor()
		{
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
			{
				return !this.VisitedLinkColor.Equals(SystemColors.HotTrack);
			}
			return !this.VisitedLinkColor.Equals(LinkUtilities.IEVisitedLinkColor);
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x0009E5EE File Offset: 0x0009C7EE
		private Color HighContrastLinkColor
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (!this.Selected)
				{
					return SystemColors.HotTrack;
				}
				return SystemColors.HighlightText;
			}
		}

		/// <summary>Gets or sets the data type of the values in the cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x0009E604 File Offset: 0x0009C804
		public override Type ValueType
		{
			get
			{
				Type valueType = base.ValueType;
				if (valueType != null)
				{
					return valueType;
				}
				return DataGridViewLinkCell.defaultValueType;
			}
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewLinkCell" />.</returns>
		// Token: 0x0600219E RID: 8606 RVA: 0x0009E628 File Offset: 0x0009C828
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewLinkCell dataGridViewLinkCell;
			if (type == DataGridViewLinkCell.cellType)
			{
				dataGridViewLinkCell = new DataGridViewLinkCell();
			}
			else
			{
				dataGridViewLinkCell = (DataGridViewLinkCell)Activator.CreateInstance(type);
			}
			base.CloneInternal(dataGridViewLinkCell);
			if (base.Properties.ContainsObject(DataGridViewLinkCell.PropLinkCellActiveLinkColor))
			{
				dataGridViewLinkCell.ActiveLinkColorInternal = this.ActiveLinkColor;
			}
			if (base.Properties.ContainsInteger(DataGridViewLinkCell.PropLinkCellUseColumnTextForLinkValue))
			{
				dataGridViewLinkCell.UseColumnTextForLinkValueInternal = this.UseColumnTextForLinkValue;
			}
			if (base.Properties.ContainsInteger(DataGridViewLinkCell.PropLinkCellLinkBehavior))
			{
				dataGridViewLinkCell.LinkBehaviorInternal = this.LinkBehavior;
			}
			if (base.Properties.ContainsObject(DataGridViewLinkCell.PropLinkCellLinkColor))
			{
				dataGridViewLinkCell.LinkColorInternal = this.LinkColor;
			}
			if (base.Properties.ContainsInteger(DataGridViewLinkCell.PropLinkCellTrackVisitedState))
			{
				dataGridViewLinkCell.TrackVisitedStateInternal = this.TrackVisitedState;
			}
			if (base.Properties.ContainsObject(DataGridViewLinkCell.PropLinkCellVisitedLinkColor))
			{
				dataGridViewLinkCell.VisitedLinkColorInternal = this.VisitedLinkColor;
			}
			if (this.linkVisitedSet)
			{
				dataGridViewLinkCell.LinkVisited = this.LinkVisited;
			}
			return dataGridViewLinkCell;
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0009E730 File Offset: 0x0009C930
		private bool LinkBoundsContainPoint(int x, int y, int rowIndex)
		{
			return base.GetContentBounds(rowIndex).Contains(x, y);
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewLinkCell" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewLinkCell" />.</returns>
		// Token: 0x060021A0 RID: 8608 RVA: 0x0009E74E File Offset: 0x0009C94E
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject(this);
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics" /> and cell style.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		// Token: 0x060021A1 RID: 8609 RVA: 0x0009E758 File Offset: 0x0009C958
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (base.DataGridView == null || rowIndex < 0 || base.OwningColumn == null)
			{
				return Rectangle.Empty;
			}
			object value = this.GetValue(rowIndex);
			object formattedValue = this.GetFormattedValue(value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle;
			DataGridViewElementStates dataGridViewElementStates;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dataGridViewAdvancedBorderStyle, out dataGridViewElementStates, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementStates, formattedValue, null, cellStyle, dataGridViewAdvancedBorderStyle, DataGridViewPaintParts.ContentForeground, true, false, false);
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's error icon, if one is displayed.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		// Token: 0x060021A2 RID: 8610 RVA: 0x0009E7CC File Offset: 0x0009C9CC
		protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (base.DataGridView == null || rowIndex < 0 || base.OwningColumn == null || !base.DataGridView.ShowCellErrors || string.IsNullOrEmpty(this.GetErrorText(rowIndex)))
			{
				return Rectangle.Empty;
			}
			object value = this.GetValue(rowIndex);
			object formattedValue = this.GetFormattedValue(value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle;
			DataGridViewElementStates dataGridViewElementStates;
			Rectangle rectangle;
			base.ComputeBorderStyleCellStateAndCellBounds(rowIndex, out dataGridViewAdvancedBorderStyle, out dataGridViewElementStates, out rectangle);
			return this.PaintPrivate(graphics, rectangle, rectangle, rowIndex, dataGridViewElementStates, formattedValue, this.GetErrorText(rowIndex), cellStyle, dataGridViewAdvancedBorderStyle, DataGridViewPaintParts.ContentForeground, false, true, false);
		}

		/// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		// Token: 0x060021A3 RID: 8611 RVA: 0x0009E860 File Offset: 0x0009CA60
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			if (base.DataGridView == null)
			{
				return new Size(-1, -1);
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			Rectangle stdBorderWidths = base.StdBorderWidths;
			int num = stdBorderWidths.Left + stdBorderWidths.Width + cellStyle.Padding.Horizontal;
			int num2 = stdBorderWidths.Top + stdBorderWidths.Height + cellStyle.Padding.Vertical;
			DataGridViewFreeDimension freeDimensionFromConstraint = DataGridViewCell.GetFreeDimensionFromConstraint(constraintSize);
			object formattedValue = base.GetFormattedValue(rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.PreferredSize);
			string text = formattedValue as string;
			if (string.IsNullOrEmpty(text))
			{
				text = " ";
			}
			TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
			Size size;
			if (cellStyle.WrapMode == DataGridViewTriState.True && text.Length > 1)
			{
				if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
				{
					if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
					{
						int num3 = constraintSize.Height - num2 - 1 - 1;
						if ((cellStyle.Alignment & DataGridViewLinkCell.anyBottom) != DataGridViewContentAlignment.NotSet)
						{
							num3--;
						}
						size = new Size(DataGridViewCell.MeasureTextWidth(graphics, text, cellStyle.Font, Math.Max(1, num3), textFormatFlags), 0);
					}
					else
					{
						size = DataGridViewCell.MeasureTextPreferredSize(graphics, text, cellStyle.Font, 5f, textFormatFlags);
					}
				}
				else
				{
					size = new Size(0, DataGridViewCell.MeasureTextHeight(graphics, text, cellStyle.Font, Math.Max(1, constraintSize.Width - num - 1 - 2), textFormatFlags));
				}
			}
			else if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				if (freeDimensionFromConstraint == DataGridViewFreeDimension.Width)
				{
					size = new Size(DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, textFormatFlags).Width, 0);
				}
				else
				{
					size = DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, textFormatFlags);
				}
			}
			else
			{
				size = new Size(0, DataGridViewCell.MeasureTextSize(graphics, text, cellStyle.Font, textFormatFlags).Height);
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Height)
			{
				size.Width += 3 + num;
				if (base.DataGridView.ShowCellErrors)
				{
					size.Width = Math.Max(size.Width, num + 8 + (int)DataGridViewCell.iconsWidth);
				}
			}
			if (freeDimensionFromConstraint != DataGridViewFreeDimension.Width)
			{
				size.Height += 2 + num2;
				if ((cellStyle.Alignment & DataGridViewLinkCell.anyBottom) != DataGridViewContentAlignment.NotSet)
				{
					size.Height++;
				}
				if (base.DataGridView.ShowCellErrors)
				{
					size.Height = Math.Max(size.Height, num2 + 8 + (int)DataGridViewCell.iconsHeight);
				}
			}
			return size;
		}

		/// <summary>Gets the value of the cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The value contained in the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x060021A4 RID: 8612 RVA: 0x0009EACC File Offset: 0x0009CCCC
		protected override object GetValue(int rowIndex)
		{
			if (this.UseColumnTextForLinkValue && base.DataGridView != null && base.DataGridView.NewRowIndex != rowIndex && base.OwningColumn != null && base.OwningColumn is DataGridViewLinkColumn)
			{
				return ((DataGridViewLinkColumn)base.OwningColumn).Text;
			}
			return base.GetValue(rowIndex);
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when a key is released and the cell has focus.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains data about the key press.</param>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		/// <returns>
		///   <see langword="true" /> if the SPACE key was released, the <see cref="P:System.Windows.Forms.DataGridViewLinkCell.TrackVisitedState" /> property is <see langword="true" />, the <see cref="P:System.Windows.Forms.DataGridViewLinkCell.LinkVisited" /> property is <see langword="false" />, and the CTRL, ALT, and SHIFT keys are not pressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060021A5 RID: 8613 RVA: 0x0009EB24 File Offset: 0x0009CD24
		protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return e.KeyCode != Keys.Space || e.Alt || e.Control || e.Shift || (this.TrackVisitedState && !this.LinkVisited);
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is pressed while the pointer is over the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
		/// <returns>
		///   <see langword="true" /> if the mouse pointer is over the link; otherwise, <see langword="false" />.</returns>
		// Token: 0x060021A6 RID: 8614 RVA: 0x0009EB5D File Offset: 0x0009CD5D
		protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex);
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer leaves the cell.</summary>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		/// <returns>
		///   <see langword="true" /> if the link displayed by the cell is not in the normal state; otherwise, <see langword="false" />.</returns>
		// Token: 0x060021A7 RID: 8615 RVA: 0x0009EB77 File Offset: 0x0009CD77
		protected override bool MouseLeaveUnsharesRow(int rowIndex)
		{
			return this.LinkState > LinkState.Normal;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer moves over the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
		/// <returns>
		///   <see langword="true" /> if the mouse pointer is over the link and the link is has not yet changed color to reflect the hover state; otherwise, <see langword="false" />.</returns>
		// Token: 0x060021A8 RID: 8616 RVA: 0x0009EB82 File Offset: 0x0009CD82
		protected override bool MouseMoveUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			if (this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex))
			{
				if ((this.LinkState & LinkState.Hover) == LinkState.Normal)
				{
					return true;
				}
			}
			else if ((this.LinkState & LinkState.Hover) != LinkState.Normal)
			{
				return true;
			}
			return false;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is released while the pointer is over the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
		/// <returns>
		///   <see langword="true" /> if the mouse pointer is over the link; otherwise, <see langword="false" />.</returns>
		// Token: 0x060021A9 RID: 8617 RVA: 0x0009EBB7 File Offset: 0x0009CDB7
		protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return this.TrackVisitedState && this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex);
		}

		/// <summary>Called when a character key is released while the focus is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x060021AA RID: 8618 RVA: 0x0009EBDC File Offset: 0x0009CDDC
		protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (e.KeyCode == Keys.Space && !e.Alt && !e.Control && !e.Shift)
			{
				base.RaiseCellClick(new DataGridViewCellEventArgs(base.ColumnIndex, rowIndex));
				if (base.DataGridView != null && base.ColumnIndex < base.DataGridView.Columns.Count && rowIndex < base.DataGridView.Rows.Count)
				{
					base.RaiseCellContentClick(new DataGridViewCellEventArgs(base.ColumnIndex, rowIndex));
					if (this.TrackVisitedState)
					{
						this.LinkVisited = true;
					}
				}
				e.Handled = true;
			}
		}

		/// <summary>Called when the user holds down a mouse button while the pointer is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060021AB RID: 8619 RVA: 0x0009EC84 File Offset: 0x0009CE84
		protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex))
			{
				this.LinkState |= LinkState.Active;
				base.DataGridView.InvalidateCell(base.ColumnIndex, e.RowIndex);
			}
			base.OnMouseDown(e);
		}

		/// <summary>Called when the mouse pointer leaves the cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x060021AC RID: 8620 RVA: 0x0009ECE0 File Offset: 0x0009CEE0
		protected override void OnMouseLeave(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (DataGridViewLinkCell.dataGridViewCursor != null)
			{
				base.DataGridView.Cursor = DataGridViewLinkCell.dataGridViewCursor;
				DataGridViewLinkCell.dataGridViewCursor = null;
			}
			if (this.LinkState != LinkState.Normal)
			{
				this.LinkState = LinkState.Normal;
				base.DataGridView.InvalidateCell(base.ColumnIndex, rowIndex);
			}
			base.OnMouseLeave(rowIndex);
		}

		/// <summary>Called when the mouse pointer moves within a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060021AD RID: 8621 RVA: 0x0009ED44 File Offset: 0x0009CF44
		protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex))
			{
				if ((this.LinkState & LinkState.Hover) == LinkState.Normal)
				{
					this.LinkState |= LinkState.Hover;
					base.DataGridView.InvalidateCell(base.ColumnIndex, e.RowIndex);
				}
				if (DataGridViewLinkCell.dataGridViewCursor == null)
				{
					DataGridViewLinkCell.dataGridViewCursor = base.DataGridView.UserSetCursor;
				}
				if (base.DataGridView.Cursor != Cursors.Hand)
				{
					base.DataGridView.Cursor = Cursors.Hand;
				}
			}
			else if ((this.LinkState & LinkState.Hover) != LinkState.Normal)
			{
				this.LinkState &= (LinkState)(-2);
				base.DataGridView.Cursor = DataGridViewLinkCell.dataGridViewCursor;
				base.DataGridView.InvalidateCell(base.ColumnIndex, e.RowIndex);
			}
			base.OnMouseMove(e);
		}

		/// <summary>Called when the user releases a mouse button while the pointer is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060021AE RID: 8622 RVA: 0x0009EE30 File Offset: 0x0009D030
		protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
			if (base.DataGridView == null)
			{
				return;
			}
			if (this.LinkBoundsContainPoint(e.X, e.Y, e.RowIndex) && this.TrackVisitedState)
			{
				this.LinkVisited = true;
			}
		}

		/// <summary>Paints the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="cellState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</param>
		/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="errorText">An error message that is associated with the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
		/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
		// Token: 0x060021AF RID: 8623 RVA: 0x0009EE64 File Offset: 0x0009D064
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			this.PaintPrivate(graphics, clipBounds, cellBounds, rowIndex, cellState, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts, false, false, true);
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0009EE9C File Offset: 0x0009D09C
		private Rectangle PaintPrivate(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts, bool computeContentBounds, bool computeErrorIconBounds, bool paint)
		{
			if (paint && DataGridViewCell.PaintBorder(paintParts))
			{
				this.PaintBorder(g, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
			}
			Rectangle rectangle = Rectangle.Empty;
			Rectangle rectangle2 = this.BorderWidths(advancedBorderStyle);
			Rectangle rectangle3 = cellBounds;
			rectangle3.Offset(rectangle2.X, rectangle2.Y);
			rectangle3.Width -= rectangle2.Right;
			rectangle3.Height -= rectangle2.Bottom;
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			bool flag = currentCellAddress.X == base.ColumnIndex && currentCellAddress.Y == rowIndex;
			bool flag2 = (cellState & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			SolidBrush cachedBrush = base.DataGridView.GetCachedBrush((DataGridViewCell.PaintSelectionBackground(paintParts) && flag2) ? cellStyle.SelectionBackColor : cellStyle.BackColor);
			if (paint && DataGridViewCell.PaintBackground(paintParts) && cachedBrush.Color.A == 255)
			{
				g.FillRectangle(cachedBrush, rectangle3);
			}
			if (cellStyle.Padding != Padding.Empty)
			{
				if (base.DataGridView.RightToLeftInternal)
				{
					rectangle3.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
				}
				else
				{
					rectangle3.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
				}
				rectangle3.Width -= cellStyle.Padding.Horizontal;
				rectangle3.Height -= cellStyle.Padding.Vertical;
			}
			Rectangle rectangle4 = rectangle3;
			string text = formattedValue as string;
			if (text != null && (paint || computeContentBounds))
			{
				rectangle3.Offset(1, 1);
				rectangle3.Width -= 3;
				rectangle3.Height -= 2;
				if ((cellStyle.Alignment & DataGridViewLinkCell.anyBottom) != DataGridViewContentAlignment.NotSet)
				{
					rectangle3.Height--;
				}
				Font font = null;
				Font font2 = null;
				bool flag3 = (this.LinkState & LinkState.Active) == LinkState.Active;
				LinkUtilities.EnsureLinkFontsInternal(cellStyle.Font, this.LinkBehavior, ref font, ref font2, flag3);
				TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(base.DataGridView.RightToLeftInternal, cellStyle.Alignment, cellStyle.WrapMode);
				if (paint)
				{
					if (rectangle3.Width > 0 && rectangle3.Height > 0)
					{
						if (flag && base.DataGridView.ShowFocusCues && base.DataGridView.Focused && DataGridViewCell.PaintFocus(paintParts))
						{
							Rectangle textBounds = DataGridViewUtilities.GetTextBounds(rectangle3, text, textFormatFlags, cellStyle, (this.LinkState == LinkState.Hover) ? font2 : font);
							if ((cellStyle.Alignment & DataGridViewLinkCell.anyLeft) != DataGridViewContentAlignment.NotSet)
							{
								int num = textBounds.X;
								textBounds.X = num - 1;
								num = textBounds.Width;
								textBounds.Width = num + 1;
							}
							else if ((cellStyle.Alignment & DataGridViewLinkCell.anyRight) != DataGridViewContentAlignment.NotSet)
							{
								int num = textBounds.X;
								textBounds.X = num + 1;
								num = textBounds.Width;
								textBounds.Width = num + 1;
							}
							textBounds.Height += 2;
							ControlPaint.DrawFocusRectangle(g, textBounds, Color.Empty, cachedBrush.Color);
						}
						Color color;
						if ((this.LinkState & LinkState.Active) == LinkState.Active)
						{
							color = this.ActiveLinkColor;
						}
						else if (this.LinkVisited)
						{
							color = this.VisitedLinkColor;
						}
						else
						{
							color = this.LinkColor;
						}
						if (DataGridViewCell.PaintContentForeground(paintParts))
						{
							if ((textFormatFlags & TextFormatFlags.SingleLine) != TextFormatFlags.Default)
							{
								textFormatFlags |= TextFormatFlags.EndEllipsis;
							}
							TextRenderer.DrawText(g, text, (this.LinkState == LinkState.Hover) ? font2 : font, rectangle3, color, textFormatFlags);
						}
					}
					else if (flag && base.DataGridView.ShowFocusCues && base.DataGridView.Focused && DataGridViewCell.PaintFocus(paintParts) && rectangle4.Width > 0 && rectangle4.Height > 0)
					{
						ControlPaint.DrawFocusRectangle(g, rectangle4, Color.Empty, cachedBrush.Color);
					}
				}
				else
				{
					rectangle = DataGridViewUtilities.GetTextBounds(rectangle3, text, textFormatFlags, cellStyle, (this.LinkState == LinkState.Hover) ? font2 : font);
				}
				font.Dispose();
				font2.Dispose();
			}
			else if (paint || computeContentBounds)
			{
				if (flag && base.DataGridView.ShowFocusCues && base.DataGridView.Focused && DataGridViewCell.PaintFocus(paintParts) && paint && rectangle3.Width > 0 && rectangle3.Height > 0)
				{
					ControlPaint.DrawFocusRectangle(g, rectangle3, Color.Empty, cachedBrush.Color);
				}
			}
			else if (computeErrorIconBounds && !string.IsNullOrEmpty(errorText))
			{
				rectangle = base.ComputeErrorIconBounds(rectangle4);
			}
			if (base.DataGridView.ShowCellErrors && paint && DataGridViewCell.PaintErrorIcon(paintParts))
			{
				base.PaintErrorIcon(g, cellStyle, rowIndex, cellBounds, rectangle4, errorText);
			}
			return rectangle;
		}

		/// <summary>Returns a string that describes the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x060021B1 RID: 8625 RVA: 0x0009F380 File Offset: 0x0009D580
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewLinkCell { ColumnIndex=",
				base.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				base.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x04000DF5 RID: 3573
		private static readonly DataGridViewContentAlignment anyLeft = (DataGridViewContentAlignment)273;

		// Token: 0x04000DF6 RID: 3574
		private static readonly DataGridViewContentAlignment anyRight = (DataGridViewContentAlignment)1092;

		// Token: 0x04000DF7 RID: 3575
		private static readonly DataGridViewContentAlignment anyBottom = (DataGridViewContentAlignment)1792;

		// Token: 0x04000DF8 RID: 3576
		private static Type defaultFormattedValueType = typeof(string);

		// Token: 0x04000DF9 RID: 3577
		private static Type defaultValueType = typeof(object);

		// Token: 0x04000DFA RID: 3578
		private static Type cellType = typeof(DataGridViewLinkCell);

		// Token: 0x04000DFB RID: 3579
		private static readonly int PropLinkCellActiveLinkColor = PropertyStore.CreateKey();

		// Token: 0x04000DFC RID: 3580
		private static readonly int PropLinkCellLinkBehavior = PropertyStore.CreateKey();

		// Token: 0x04000DFD RID: 3581
		private static readonly int PropLinkCellLinkColor = PropertyStore.CreateKey();

		// Token: 0x04000DFE RID: 3582
		private static readonly int PropLinkCellLinkState = PropertyStore.CreateKey();

		// Token: 0x04000DFF RID: 3583
		private static readonly int PropLinkCellTrackVisitedState = PropertyStore.CreateKey();

		// Token: 0x04000E00 RID: 3584
		private static readonly int PropLinkCellUseColumnTextForLinkValue = PropertyStore.CreateKey();

		// Token: 0x04000E01 RID: 3585
		private static readonly int PropLinkCellVisitedLinkColor = PropertyStore.CreateKey();

		// Token: 0x04000E02 RID: 3586
		private const byte DATAGRIDVIEWLINKCELL_horizontalTextMarginLeft = 1;

		// Token: 0x04000E03 RID: 3587
		private const byte DATAGRIDVIEWLINKCELL_horizontalTextMarginRight = 2;

		// Token: 0x04000E04 RID: 3588
		private const byte DATAGRIDVIEWLINKCELL_verticalTextMarginTop = 1;

		// Token: 0x04000E05 RID: 3589
		private const byte DATAGRIDVIEWLINKCELL_verticalTextMarginBottom = 1;

		// Token: 0x04000E06 RID: 3590
		private bool linkVisited;

		// Token: 0x04000E07 RID: 3591
		private bool linkVisitedSet;

		// Token: 0x04000E08 RID: 3592
		private static Cursor dataGridViewCursor = null;

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewLinkCell" /> control to accessibility client applications.</summary>
		// Token: 0x02000672 RID: 1650
		protected class DataGridViewLinkCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" />.</param>
			// Token: 0x06006664 RID: 26212 RVA: 0x0017BC46 File Offset: 0x00179E46
			public DataGridViewLinkCellAccessibleObject(DataGridViewCell owner)
				: base(owner)
			{
			}

			/// <summary>Gets a string that represents the default action of the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" />.</summary>
			/// <returns>The string "Click".</returns>
			// Token: 0x17001648 RID: 5704
			// (get) Token: 0x06006665 RID: 26213 RVA: 0x0017E27A File Offset: 0x0017C47A
			public override string DefaultAction
			{
				get
				{
					return SR.GetString("DataGridView_AccLinkCellDefaultAction");
				}
			}

			/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" />.</summary>
			/// <exception cref="T:System.InvalidOperationException">The cell returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property has a <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property value that is not <see langword="null" /> and a <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> property value of -1, indicating that the cell is in a shared row.</exception>
			// Token: 0x06006666 RID: 26214 RVA: 0x0017E288 File Offset: 0x0017C488
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				DataGridViewLinkCell dataGridViewLinkCell = (DataGridViewLinkCell)base.Owner;
				DataGridView dataGridView = dataGridViewLinkCell.DataGridView;
				if (dataGridView != null && dataGridViewLinkCell.RowIndex == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedCell"));
				}
				if (dataGridViewLinkCell.OwningColumn != null && dataGridViewLinkCell.OwningRow != null)
				{
					dataGridView.OnCellContentClickInternal(new DataGridViewCellEventArgs(dataGridViewLinkCell.ColumnIndex, dataGridViewLinkCell.RowIndex));
				}
			}

			/// <summary>Gets the number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" />.</summary>
			/// <returns>The value -1.</returns>
			// Token: 0x06006667 RID: 26215 RVA: 0x0001180C File Offset: 0x0000FA0C
			public override int GetChildCount()
			{
				return 0;
			}

			// Token: 0x06006668 RID: 26216 RVA: 0x0017BCD6 File Offset: 0x00179ED6
			internal override bool IsIAccessibleExSupported()
			{
				return AccessibilityImprovements.Level2 || base.IsIAccessibleExSupported();
			}

			// Token: 0x06006669 RID: 26217 RVA: 0x0017E2EB File Offset: 0x0017C4EB
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50005;
				}
				return base.GetPropertyValue(propertyID);
			}
		}
	}
}
