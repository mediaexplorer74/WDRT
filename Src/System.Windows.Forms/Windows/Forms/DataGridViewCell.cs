using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Represents an individual cell in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001A1 RID: 417
	[TypeConverter(typeof(DataGridViewCellConverter))]
	public abstract class DataGridViewCell : DataGridViewElement, ICloneable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> class.</summary>
		// Token: 0x06001D2D RID: 7469 RVA: 0x00089634 File Offset: 0x00087834
		protected DataGridViewCell()
		{
			if (!DataGridViewCell.isScalingInitialized)
			{
				if (DpiHelper.IsScalingRequired)
				{
					DataGridViewCell.iconsWidth = (byte)DpiHelper.LogicalToDeviceUnitsX(12);
					DataGridViewCell.iconsHeight = (byte)DpiHelper.LogicalToDeviceUnitsY(11);
				}
				DataGridViewCell.isScalingInitialized = true;
			}
			this.propertyStore = new PropertyStore();
			base.StateInternal = DataGridViewElementStates.None;
		}

		/// <summary>Releases the unmanaged resources and performs other cleanup operations before the <see cref="T:System.Windows.Forms.DataGridViewCell" /> is reclaimed by garbage collection.</summary>
		// Token: 0x06001D2E RID: 7470 RVA: 0x00089688 File Offset: 0x00087888
		~DataGridViewCell()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> assigned to the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> assigned to the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x000896B8 File Offset: 0x000878B8
		[Browsable(false)]
		public AccessibleObject AccessibilityObject
		{
			get
			{
				AccessibleObject accessibleObject = (AccessibleObject)this.Properties.GetObject(DataGridViewCell.PropCellAccessibilityObject);
				if (accessibleObject == null)
				{
					accessibleObject = this.CreateAccessibilityInstance();
					this.Properties.SetObject(DataGridViewCell.PropCellAccessibilityObject, accessibleObject);
				}
				return accessibleObject;
			}
		}

		/// <summary>Gets the column index for this cell.</summary>
		/// <returns>The index of the column that contains the cell; -1 if the cell is not contained within a column.</returns>
		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x000896F7 File Offset: 0x000878F7
		public int ColumnIndex
		{
			get
			{
				if (this.owningColumn == null)
				{
					return -1;
				}
				return this.owningColumn.Index;
			}
		}

		/// <summary>Gets the bounding rectangle that encloses the cell's content area.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.  
		///  -or-  
		///  The cell is a column header cell.</exception>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> property is less than 0, indicating that the cell is a row header cell.</exception>
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x0008970E File Offset: 0x0008790E
		[Browsable(false)]
		public Rectangle ContentBounds
		{
			get
			{
				return this.GetContentBounds(this.RowIndex);
			}
		}

		/// <summary>Gets or sets the shortcut menu associated with the cell.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the cell.</returns>
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x0008971C File Offset: 0x0008791C
		// (set) Token: 0x06001D33 RID: 7475 RVA: 0x0008972A File Offset: 0x0008792A
		[DefaultValue(null)]
		public virtual ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.GetContextMenuStrip(this.RowIndex);
			}
			set
			{
				this.ContextMenuStripInternal = value;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x00089733 File Offset: 0x00087933
		// (set) Token: 0x06001D35 RID: 7477 RVA: 0x0008974C File Offset: 0x0008794C
		private ContextMenuStrip ContextMenuStripInternal
		{
			get
			{
				return (ContextMenuStrip)this.Properties.GetObject(DataGridViewCell.PropCellContextMenuStrip);
			}
			set
			{
				ContextMenuStrip contextMenuStrip = (ContextMenuStrip)this.Properties.GetObject(DataGridViewCell.PropCellContextMenuStrip);
				if (contextMenuStrip != value)
				{
					EventHandler eventHandler = new EventHandler(this.DetachContextMenuStrip);
					if (contextMenuStrip != null)
					{
						contextMenuStrip.Disposed -= eventHandler;
					}
					this.Properties.SetObject(DataGridViewCell.PropCellContextMenuStrip, value);
					if (value != null)
					{
						value.Disposed += eventHandler;
					}
					if (base.DataGridView != null)
					{
						base.DataGridView.OnCellContextMenuStripChanged(this);
					}
				}
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x000897B9 File Offset: 0x000879B9
		// (set) Token: 0x06001D37 RID: 7479 RVA: 0x000897C4 File Offset: 0x000879C4
		private byte CurrentMouseLocation
		{
			get
			{
				return this.flags & 3;
			}
			set
			{
				this.flags = (byte)((int)this.flags & -4);
				this.flags |= value;
			}
		}

		/// <summary>Gets the default value for a cell in the row for new records.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the default value.</returns>
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001D38 RID: 7480 RVA: 0x00015C90 File Offset: 0x00013E90
		[Browsable(false)]
		public virtual object DefaultNewRowValue
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value that indicates whether the cell is currently displayed on-screen.</summary>
		/// <returns>
		///   <see langword="true" /> if the cell is on-screen or partially on-screen; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x000897E8 File Offset: 0x000879E8
		[Browsable(false)]
		public virtual bool Displayed
		{
			get
			{
				return base.DataGridView != null && (base.DataGridView != null && this.RowIndex >= 0 && this.ColumnIndex >= 0) && this.owningColumn.Displayed && this.owningRow.Displayed;
			}
		}

		/// <summary>Gets the current, formatted value of the cell, regardless of whether the cell is in edit mode and the value has not been committed.</summary>
		/// <returns>The current, formatted value of the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.  
		///  -or-  
		///  The cell is a column header cell.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to <see langword="true" />. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x00089838 File Offset: 0x00087A38
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object EditedFormattedValue
		{
			get
			{
				if (base.DataGridView == null)
				{
					return null;
				}
				DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, this.RowIndex, false);
				return this.GetEditedFormattedValue(this.GetValue(this.RowIndex), this.RowIndex, ref inheritedStyle, DataGridViewDataErrorContexts.Formatting);
			}
		}

		/// <summary>Gets the type of the cell's hosted editing control.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the <see cref="T:System.Windows.Forms.DataGridViewTextBoxEditingControl" /> type.</returns>
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x00089879 File Offset: 0x00087A79
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual Type EditType
		{
			get
			{
				return typeof(DataGridViewTextBoxEditingControl);
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x00089885 File Offset: 0x00087A85
		private static Bitmap ErrorBitmap
		{
			get
			{
				if (DataGridViewCell.errorBmp == null)
				{
					DataGridViewCell.errorBmp = DataGridViewCell.GetBitmap("DataGridViewRow.error.bmp");
				}
				return DataGridViewCell.errorBmp;
			}
		}

		/// <summary>Gets the bounds of the error icon for the cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the error icon for the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.  
		///  -or-  
		///  The cell is a column header cell.</exception>
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x000898A2 File Offset: 0x00087AA2
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Rectangle ErrorIconBounds
		{
			get
			{
				return this.GetErrorIconBounds(this.RowIndex);
			}
		}

		/// <summary>Gets or sets the text describing an error condition associated with the cell.</summary>
		/// <returns>The text that describes an error condition associated with the cell.</returns>
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x000898B0 File Offset: 0x00087AB0
		// (set) Token: 0x06001D3F RID: 7487 RVA: 0x000898BE File Offset: 0x00087ABE
		[Browsable(false)]
		public string ErrorText
		{
			get
			{
				return this.GetErrorText(this.RowIndex);
			}
			set
			{
				this.ErrorTextInternal = value;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x000898C8 File Offset: 0x00087AC8
		// (set) Token: 0x06001D41 RID: 7489 RVA: 0x000898F8 File Offset: 0x00087AF8
		private string ErrorTextInternal
		{
			get
			{
				object @object = this.Properties.GetObject(DataGridViewCell.PropCellErrorText);
				if (@object != null)
				{
					return (string)@object;
				}
				return string.Empty;
			}
			set
			{
				string errorTextInternal = this.ErrorTextInternal;
				if (!string.IsNullOrEmpty(value) || this.Properties.ContainsObject(DataGridViewCell.PropCellErrorText))
				{
					this.Properties.SetObject(DataGridViewCell.PropCellErrorText, value);
				}
				if (base.DataGridView != null && !errorTextInternal.Equals(this.ErrorTextInternal))
				{
					base.DataGridView.OnCellErrorTextChanged(this);
				}
			}
		}

		/// <summary>Gets the value of the cell as formatted for display.</summary>
		/// <returns>The formatted value of the cell or <see langword="null" /> if the cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.  
		///  -or-  
		///  The cell is a column header cell.</exception>
		/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to <see langword="true" />. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x0008995C File Offset: 0x00087B5C
		[Browsable(false)]
		public object FormattedValue
		{
			get
			{
				if (base.DataGridView == null)
				{
					return null;
				}
				DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, this.RowIndex, false);
				return this.GetFormattedValue(this.RowIndex, ref inheritedStyle, DataGridViewDataErrorContexts.Formatting);
			}
		}

		/// <summary>Gets the type of the formatted value associated with the cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the type of the cell's formatted value.</returns>
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x00089991 File Offset: 0x00087B91
		[Browsable(false)]
		public virtual Type FormattedValueType
		{
			get
			{
				return this.ValueType;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0008999C File Offset: 0x00087B9C
		private TypeConverter FormattedValueTypeConverter
		{
			get
			{
				TypeConverter typeConverter = null;
				if (this.FormattedValueType != null)
				{
					if (base.DataGridView != null)
					{
						typeConverter = base.DataGridView.GetCachedTypeConverter(this.FormattedValueType);
					}
					else
					{
						typeConverter = TypeDescriptor.GetConverter(this.FormattedValueType);
					}
				}
				return typeConverter;
			}
		}

		/// <summary>Gets a value indicating whether the cell is frozen.</summary>
		/// <returns>
		///   <see langword="true" /> if the cell is frozen; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x000899E4 File Offset: 0x00087BE4
		[Browsable(false)]
		public virtual bool Frozen
		{
			get
			{
				if (base.DataGridView != null && this.RowIndex >= 0 && this.ColumnIndex >= 0)
				{
					return this.owningColumn.Frozen && this.owningRow.Frozen;
				}
				return this.owningRow != null && (this.owningRow.DataGridView == null || this.RowIndex >= 0) && this.owningRow.Frozen;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001D46 RID: 7494 RVA: 0x00089A51 File Offset: 0x00087C51
		internal bool HasErrorText
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewCell.PropCellErrorText) && this.Properties.GetObject(DataGridViewCell.PropCellErrorText) != null;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewCell.Style" /> property has been set.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.DataGridViewCell.Style" /> property has been set; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x00089A7A File Offset: 0x00087C7A
		[Browsable(false)]
		public bool HasStyle
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewCell.PropCellStyle) && this.Properties.GetObject(DataGridViewCell.PropCellStyle) != null;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x00089AA3 File Offset: 0x00087CA3
		internal bool HasToolTipText
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewCell.PropCellToolTipText) && this.Properties.GetObject(DataGridViewCell.PropCellToolTipText) != null;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x00089ACC File Offset: 0x00087CCC
		internal bool HasValue
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewCell.PropCellValue) && this.Properties.GetObject(DataGridViewCell.PropCellValue) != null;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x00089AF5 File Offset: 0x00087CF5
		internal virtual bool HasValueType
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewCell.PropCellValueType) && this.Properties.GetObject(DataGridViewCell.PropCellValueType) != null;
			}
		}

		/// <summary>Gets the current state of the cell as inherited from the state of its row and column.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values representing the current state of the cell.</returns>
		/// <exception cref="T:System.ArgumentException">The cell is not contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control and the value of its <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> property is not -1.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The cell is contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control and the value of its <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> property is -1.</exception>
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x00089B1E File Offset: 0x00087D1E
		[Browsable(false)]
		public DataGridViewElementStates InheritedState
		{
			get
			{
				return this.GetInheritedState(this.RowIndex);
			}
		}

		/// <summary>Gets the style currently applied to the cell.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> currently applied to the cell.</returns>
		/// <exception cref="T:System.InvalidOperationException">The cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.  
		///  -or-  
		///  The cell is a column header cell.</exception>
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x00089B2C File Offset: 0x00087D2C
		[Browsable(false)]
		public DataGridViewCellStyle InheritedStyle
		{
			get
			{
				return this.GetInheritedStyleInternal(this.RowIndex);
			}
		}

		/// <summary>Gets a value indicating whether this cell is currently being edited.</summary>
		/// <returns>
		///   <see langword="true" /> if the cell is in edit mode; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row containing the cell is a shared row.</exception>
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x00089B3C File Offset: 0x00087D3C
		[Browsable(false)]
		public bool IsInEditMode
		{
			get
			{
				if (base.DataGridView == null)
				{
					return false;
				}
				if (this.RowIndex == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedCell"));
				}
				Point currentCellAddress = base.DataGridView.CurrentCellAddress;
				return currentCellAddress.X != -1 && currentCellAddress.X == this.ColumnIndex && currentCellAddress.Y == this.RowIndex && base.DataGridView.IsCurrentCellInEditMode;
			}
		}

		/// <summary>Gets the column that contains this cell.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> that contains the cell, or <see langword="null" /> if the cell is not in a column.</returns>
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x00089BAD File Offset: 0x00087DAD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataGridViewColumn OwningColumn
		{
			get
			{
				return this.owningColumn;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (set) Token: 0x06001D4F RID: 7503 RVA: 0x00089BB5 File Offset: 0x00087DB5
		internal DataGridViewColumn OwningColumnInternal
		{
			set
			{
				this.owningColumn = value;
			}
		}

		/// <summary>Gets the row that contains this cell.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that contains the cell, or <see langword="null" /> if the cell is not in a row.</returns>
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x00089BBE File Offset: 0x00087DBE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataGridViewRow OwningRow
		{
			get
			{
				return this.owningRow;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (set) Token: 0x06001D51 RID: 7505 RVA: 0x00089BC6 File Offset: 0x00087DC6
		internal DataGridViewRow OwningRowInternal
		{
			set
			{
				this.owningRow = value;
			}
		}

		/// <summary>Gets the size, in pixels, of a rectangular area into which the cell can fit.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> containing the height and width, in pixels.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.  
		///  -or-  
		///  The cell is a column header cell.</exception>
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x00089BCF File Offset: 0x00087DCF
		[Browsable(false)]
		public Size PreferredSize
		{
			get
			{
				return this.GetPreferredSize(this.RowIndex);
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x00089BDD File Offset: 0x00087DDD
		internal PropertyStore Properties
		{
			get
			{
				return this.propertyStore;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cell's data can be edited.</summary>
		/// <returns>
		///   <see langword="true" /> if the cell's data cannot be edited; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no owning row when setting this property.  
		///  -or-  
		///  The owning row is shared when setting this property.</exception>
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x00089BE8 File Offset: 0x00087DE8
		// (set) Token: 0x06001D55 RID: 7509 RVA: 0x00089C58 File Offset: 0x00087E58
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool ReadOnly
		{
			get
			{
				return (this.State & DataGridViewElementStates.ReadOnly) != DataGridViewElementStates.None || (this.owningRow != null && (this.owningRow.DataGridView == null || this.RowIndex >= 0) && this.owningRow.ReadOnly) || (base.DataGridView != null && this.RowIndex >= 0 && this.ColumnIndex >= 0 && this.owningColumn.ReadOnly);
			}
			set
			{
				if (base.DataGridView != null)
				{
					if (this.RowIndex == -1)
					{
						throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedCell"));
					}
					if (value != this.ReadOnly && !base.DataGridView.ReadOnly)
					{
						base.DataGridView.OnDataGridViewElementStateChanging(this, -1, DataGridViewElementStates.ReadOnly);
						base.DataGridView.SetReadOnlyCellCore(this.ColumnIndex, this.RowIndex, value);
						return;
					}
				}
				else if (this.owningRow == null)
				{
					if (value != this.ReadOnly)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewCell_CannotSetReadOnlyState"));
					}
				}
				else
				{
					this.owningRow.SetReadOnlyCellCore(this, value);
				}
			}
		}

		// Token: 0x17000671 RID: 1649
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x00089CF1 File Offset: 0x00087EF1
		internal bool ReadOnlyInternal
		{
			set
			{
				if (value)
				{
					base.StateInternal = this.State | DataGridViewElementStates.ReadOnly;
				}
				else
				{
					base.StateInternal = this.State & ~DataGridViewElementStates.ReadOnly;
				}
				if (base.DataGridView != null)
				{
					base.DataGridView.OnDataGridViewElementStateChanged(this, -1, DataGridViewElementStates.ReadOnly);
				}
			}
		}

		/// <summary>Gets a value indicating whether the cell can be resized.</summary>
		/// <returns>
		///   <see langword="true" /> if the cell can be resized; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x00089D2C File Offset: 0x00087F2C
		[Browsable(false)]
		public virtual bool Resizable
		{
			get
			{
				return (this.owningRow != null && (this.owningRow.DataGridView == null || this.RowIndex >= 0) && this.owningRow.Resizable == DataGridViewTriState.True) || (base.DataGridView != null && this.RowIndex >= 0 && this.ColumnIndex >= 0 && this.owningColumn.Resizable == DataGridViewTriState.True);
			}
		}

		/// <summary>Gets the index of the cell's parent row.</summary>
		/// <returns>The index of the row that contains the cell; -1 if there is no owning row.</returns>
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x00089D91 File Offset: 0x00087F91
		[Browsable(false)]
		public int RowIndex
		{
			get
			{
				if (this.owningRow == null)
				{
					return -1;
				}
				return this.owningRow.Index;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cell has been selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the cell has been selected; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no associated <see cref="T:System.Windows.Forms.DataGridView" /> when setting this property.  
		///  -or-  
		///  The owning row is shared when setting this property.</exception>
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00089DA8 File Offset: 0x00087FA8
		// (set) Token: 0x06001D5A RID: 7514 RVA: 0x00089E18 File Offset: 0x00088018
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Selected
		{
			get
			{
				return (this.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.None || (this.owningRow != null && (this.owningRow.DataGridView == null || this.RowIndex >= 0) && this.owningRow.Selected) || (base.DataGridView != null && this.RowIndex >= 0 && this.ColumnIndex >= 0 && this.owningColumn.Selected);
			}
			set
			{
				if (base.DataGridView != null)
				{
					if (this.RowIndex == -1)
					{
						throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedCell"));
					}
					base.DataGridView.SetSelectedCellCoreInternal(this.ColumnIndex, this.RowIndex, value);
					return;
				}
				else
				{
					if (value)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewCell_CannotSetSelectedState"));
					}
					return;
				}
			}
		}

		// Token: 0x17000675 RID: 1653
		// (set) Token: 0x06001D5B RID: 7515 RVA: 0x00089E72 File Offset: 0x00088072
		internal bool SelectedInternal
		{
			set
			{
				if (value)
				{
					base.StateInternal = this.State | DataGridViewElementStates.Selected;
				}
				else
				{
					base.StateInternal = this.State & ~DataGridViewElementStates.Selected;
				}
				if (base.DataGridView != null)
				{
					base.DataGridView.OnDataGridViewElementStateChanged(this, -1, DataGridViewElementStates.Selected);
				}
			}
		}

		/// <summary>Gets the size of the cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> set to the owning row's height and the owning column's width.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row containing the cell is a shared row.  
		///  -or-  
		///  The cell is a column header cell.</exception>
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x00089EAE File Offset: 0x000880AE
		[Browsable(false)]
		public Size Size
		{
			get
			{
				return this.GetSize(this.RowIndex);
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x00089EBC File Offset: 0x000880BC
		internal Rectangle StdBorderWidths
		{
			get
			{
				if (base.DataGridView != null)
				{
					DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = new DataGridViewAdvancedBorderStyle();
					DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle2 = this.AdjustCellBorderStyle(base.DataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStyle, false, false, false, false);
					return this.BorderWidths(dataGridViewAdvancedBorderStyle2);
				}
				return Rectangle.Empty;
			}
		}

		/// <summary>Gets or sets the style for the cell.</summary>
		/// <returns>The style associated with the cell.</returns>
		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x00089EFC File Offset: 0x000880FC
		// (set) Token: 0x06001D5F RID: 7519 RVA: 0x00089F48 File Offset: 0x00088148
		[Browsable(true)]
		public DataGridViewCellStyle Style
		{
			get
			{
				DataGridViewCellStyle dataGridViewCellStyle = (DataGridViewCellStyle)this.Properties.GetObject(DataGridViewCell.PropCellStyle);
				if (dataGridViewCellStyle == null)
				{
					dataGridViewCellStyle = new DataGridViewCellStyle();
					dataGridViewCellStyle.AddScope(base.DataGridView, DataGridViewCellStyleScopes.Cell);
					this.Properties.SetObject(DataGridViewCell.PropCellStyle, dataGridViewCellStyle);
				}
				return dataGridViewCellStyle;
			}
			set
			{
				DataGridViewCellStyle dataGridViewCellStyle = null;
				if (this.HasStyle)
				{
					dataGridViewCellStyle = this.Style;
					dataGridViewCellStyle.RemoveScope(DataGridViewCellStyleScopes.Cell);
				}
				if (value != null || this.Properties.ContainsObject(DataGridViewCell.PropCellStyle))
				{
					if (value != null)
					{
						value.AddScope(base.DataGridView, DataGridViewCellStyleScopes.Cell);
					}
					this.Properties.SetObject(DataGridViewCell.PropCellStyle, value);
				}
				if (((dataGridViewCellStyle != null && value == null) || (dataGridViewCellStyle == null && value != null) || (dataGridViewCellStyle != null && value != null && !dataGridViewCellStyle.Equals(this.Style))) && base.DataGridView != null)
				{
					base.DataGridView.OnCellStyleChanged(this);
				}
			}
		}

		/// <summary>Gets or sets the object that contains supplemental data about the cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the cell. The default is <see langword="null" />.</returns>
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x00089FD7 File Offset: 0x000881D7
		// (set) Token: 0x06001D61 RID: 7521 RVA: 0x00089FE9 File Offset: 0x000881E9
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.Properties.GetObject(DataGridViewCell.PropCellTag);
			}
			set
			{
				if (value != null || this.Properties.ContainsObject(DataGridViewCell.PropCellTag))
				{
					this.Properties.SetObject(DataGridViewCell.PropCellTag, value);
				}
			}
		}

		/// <summary>Gets or sets the ToolTip text associated with this cell.</summary>
		/// <returns>The ToolTip text associated with the cell. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x0008A011 File Offset: 0x00088211
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x0008A01F File Offset: 0x0008821F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ToolTipText
		{
			get
			{
				return this.GetToolTipText(this.RowIndex);
			}
			set
			{
				this.ToolTipTextInternal = value;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x0008A028 File Offset: 0x00088228
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x0008A058 File Offset: 0x00088258
		private string ToolTipTextInternal
		{
			get
			{
				object @object = this.Properties.GetObject(DataGridViewCell.PropCellToolTipText);
				if (@object != null)
				{
					return (string)@object;
				}
				return string.Empty;
			}
			set
			{
				string toolTipTextInternal = this.ToolTipTextInternal;
				if (!string.IsNullOrEmpty(value) || this.Properties.ContainsObject(DataGridViewCell.PropCellToolTipText))
				{
					this.Properties.SetObject(DataGridViewCell.PropCellToolTipText, value);
				}
				if (base.DataGridView != null && !toolTipTextInternal.Equals(this.ToolTipTextInternal))
				{
					base.DataGridView.OnCellToolTipTextChanged(this);
				}
			}
		}

		/// <summary>Gets or sets the value associated with this cell.</summary>
		/// <returns>Gets or sets the data to be displayed by the cell. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> is outside the valid range of 0 to the number of rows in the control minus 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x0008A0B9 File Offset: 0x000882B9
		// (set) Token: 0x06001D67 RID: 7527 RVA: 0x0008A0C7 File Offset: 0x000882C7
		[Browsable(false)]
		public object Value
		{
			get
			{
				return this.GetValue(this.RowIndex);
			}
			set
			{
				this.SetValue(this.RowIndex, value);
			}
		}

		/// <summary>Gets or sets the data type of the values in the cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0008A0D8 File Offset: 0x000882D8
		// (set) Token: 0x06001D69 RID: 7529 RVA: 0x0008A119 File Offset: 0x00088319
		[Browsable(false)]
		public virtual Type ValueType
		{
			get
			{
				Type type = (Type)this.Properties.GetObject(DataGridViewCell.PropCellValueType);
				if (type == null && this.OwningColumn != null)
				{
					type = this.OwningColumn.ValueType;
				}
				return type;
			}
			set
			{
				if (value != null || this.Properties.ContainsObject(DataGridViewCell.PropCellValueType))
				{
					this.Properties.SetObject(DataGridViewCell.PropCellValueType, value);
				}
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x0008A148 File Offset: 0x00088348
		private TypeConverter ValueTypeConverter
		{
			get
			{
				TypeConverter typeConverter = null;
				if (this.OwningColumn != null)
				{
					typeConverter = this.OwningColumn.BoundColumnConverter;
				}
				if (typeConverter == null && this.ValueType != null)
				{
					if (base.DataGridView != null)
					{
						typeConverter = base.DataGridView.GetCachedTypeConverter(this.ValueType);
					}
					else
					{
						typeConverter = TypeDescriptor.GetConverter(this.ValueType);
					}
				}
				return typeConverter;
			}
		}

		/// <summary>Gets a value indicating whether the cell is in a row or column that has been hidden.</summary>
		/// <returns>
		///   <see langword="true" /> if the cell is visible; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0008A1A8 File Offset: 0x000883A8
		[Browsable(false)]
		public virtual bool Visible
		{
			get
			{
				if (base.DataGridView != null && this.RowIndex >= 0 && this.ColumnIndex >= 0)
				{
					return this.owningColumn.Visible && this.owningRow.Visible;
				}
				return this.owningRow != null && (this.owningRow.DataGridView == null || this.RowIndex >= 0) && this.owningRow.Visible;
			}
		}

		/// <summary>Modifies the input cell border style according to the specified criteria.</summary>
		/// <param name="dataGridViewAdvancedBorderStyleInput">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the cell border style to modify.</param>
		/// <param name="dataGridViewAdvancedBorderStylePlaceholder">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that is used to store intermediate changes to the cell border style.</param>
		/// <param name="singleVerticalBorderAdded">
		///   <see langword="true" /> to add a vertical border to the cell; otherwise, <see langword="false" />.</param>
		/// <param name="singleHorizontalBorderAdded">
		///   <see langword="true" /> to add a horizontal border to the cell; otherwise, <see langword="false" />.</param>
		/// <param name="isFirstDisplayedColumn">
		///   <see langword="true" /> if the hosting cell is in the first visible column; otherwise, <see langword="false" />.</param>
		/// <param name="isFirstDisplayedRow">
		///   <see langword="true" /> if the hosting cell is in the first visible row; otherwise, <see langword="false" />.</param>
		/// <returns>The modified <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />.</returns>
		// Token: 0x06001D6C RID: 7532 RVA: 0x0008A218 File Offset: 0x00088418
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual DataGridViewAdvancedBorderStyle AdjustCellBorderStyle(DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			DataGridViewAdvancedCellBorderStyle all = dataGridViewAdvancedBorderStyleInput.All;
			if (all != DataGridViewAdvancedCellBorderStyle.NotSet)
			{
				if (all == DataGridViewAdvancedCellBorderStyle.Single)
				{
					if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Single;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = ((isFirstDisplayedColumn && singleVerticalBorderAdded) ? DataGridViewAdvancedCellBorderStyle.Single : DataGridViewAdvancedCellBorderStyle.None);
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = ((isFirstDisplayedColumn && singleVerticalBorderAdded) ? DataGridViewAdvancedCellBorderStyle.Single : DataGridViewAdvancedCellBorderStyle.None);
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Single;
					}
					dataGridViewAdvancedBorderStylePlaceholder.TopInternal = ((isFirstDisplayedRow && singleHorizontalBorderAdded) ? DataGridViewAdvancedCellBorderStyle.Single : DataGridViewAdvancedCellBorderStyle.None);
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.Single;
					return dataGridViewAdvancedBorderStylePlaceholder;
				}
				if (all != DataGridViewAdvancedCellBorderStyle.OutsetPartial)
				{
				}
			}
			else if (base.DataGridView != null && base.DataGridView.AdvancedCellBorderStyle == dataGridViewAdvancedBorderStyleInput)
			{
				DataGridViewCellBorderStyle cellBorderStyle = base.DataGridView.CellBorderStyle;
				if (cellBorderStyle == DataGridViewCellBorderStyle.SingleVertical)
				{
					if (base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Single;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = ((isFirstDisplayedColumn && singleVerticalBorderAdded) ? DataGridViewAdvancedCellBorderStyle.Single : DataGridViewAdvancedCellBorderStyle.None);
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = ((isFirstDisplayedColumn && singleVerticalBorderAdded) ? DataGridViewAdvancedCellBorderStyle.Single : DataGridViewAdvancedCellBorderStyle.None);
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Single;
					}
					dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.None;
					return dataGridViewAdvancedBorderStylePlaceholder;
				}
				if (cellBorderStyle == DataGridViewCellBorderStyle.SingleHorizontal)
				{
					dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.None;
					dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.None;
					dataGridViewAdvancedBorderStylePlaceholder.TopInternal = ((isFirstDisplayedRow && singleHorizontalBorderAdded) ? DataGridViewAdvancedCellBorderStyle.Single : DataGridViewAdvancedCellBorderStyle.None);
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.Single;
					return dataGridViewAdvancedBorderStylePlaceholder;
				}
			}
			return dataGridViewAdvancedBorderStyleInput;
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Rectangle" /> that represents the widths of all the cell margins.</summary>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that the margins are to be calculated for.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the widths of all the cell margins.</returns>
		// Token: 0x06001D6D RID: 7533 RVA: 0x0008A348 File Offset: 0x00088548
		protected virtual Rectangle BorderWidths(DataGridViewAdvancedBorderStyle advancedBorderStyle)
		{
			Rectangle rectangle = default(Rectangle);
			rectangle.X = ((advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.None) ? 0 : 1);
			if (advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.InsetDouble)
			{
				int num = rectangle.X;
				rectangle.X = num + 1;
			}
			rectangle.Y = ((advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.None) ? 0 : 1);
			if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.InsetDouble)
			{
				int num = rectangle.Y;
				rectangle.Y = num + 1;
			}
			rectangle.Width = ((advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.None) ? 0 : 1);
			if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.InsetDouble)
			{
				int num = rectangle.Width;
				rectangle.Width = num + 1;
			}
			rectangle.Height = ((advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.None) ? 0 : 1);
			if (advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.InsetDouble)
			{
				int num = rectangle.Height;
				rectangle.Height = num + 1;
			}
			if (this.owningColumn != null)
			{
				if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
				{
					rectangle.X += this.owningColumn.DividerWidth;
				}
				else
				{
					rectangle.Width += this.owningColumn.DividerWidth;
				}
			}
			if (this.owningRow != null)
			{
				rectangle.Height += this.owningRow.DividerHeight;
			}
			return rectangle;
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void CacheEditingControl()
		{
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0008A4AC File Offset: 0x000886AC
		internal DataGridViewElementStates CellStateFromColumnRowStates(DataGridViewElementStates rowState)
		{
			DataGridViewElementStates dataGridViewElementStates = DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected;
			DataGridViewElementStates dataGridViewElementStates2 = DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible;
			DataGridViewElementStates dataGridViewElementStates3 = this.owningColumn.State & dataGridViewElementStates;
			dataGridViewElementStates3 |= rowState & dataGridViewElementStates;
			return dataGridViewElementStates3 | (this.owningColumn.State & dataGridViewElementStates2 & (rowState & dataGridViewElementStates2));
		}

		/// <summary>Indicates whether the cell's row will be unshared when the cell is clicked.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnClick(System.Windows.Forms.DataGridViewCellEventArgs)" /> method.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001D70 RID: 7536 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool ClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return false;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0008A4E8 File Offset: 0x000886E8
		internal bool ClickUnsharesRowInternal(DataGridViewCellEventArgs e)
		{
			return this.ClickUnsharesRow(e);
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0008A4F4 File Offset: 0x000886F4
		internal void CloneInternal(DataGridViewCell dataGridViewCell)
		{
			if (this.HasValueType)
			{
				dataGridViewCell.ValueType = this.ValueType;
			}
			if (this.HasStyle)
			{
				dataGridViewCell.Style = new DataGridViewCellStyle(this.Style);
			}
			if (this.HasErrorText)
			{
				dataGridViewCell.ErrorText = this.ErrorTextInternal;
			}
			if (this.HasToolTipText)
			{
				dataGridViewCell.ToolTipText = this.ToolTipTextInternal;
			}
			if (this.ContextMenuStripInternal != null)
			{
				dataGridViewCell.ContextMenuStrip = this.ContextMenuStripInternal.Clone();
			}
			dataGridViewCell.StateInternal = this.State & ~DataGridViewElementStates.Selected;
			dataGridViewCell.Tag = this.Tag;
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x06001D73 RID: 7539 RVA: 0x0008A58C File Offset: 0x0008878C
		public virtual object Clone()
		{
			DataGridViewCell dataGridViewCell = (DataGridViewCell)Activator.CreateInstance(base.GetType());
			this.CloneInternal(dataGridViewCell);
			return dataGridViewCell;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0008A5B4 File Offset: 0x000887B4
		internal static int ColorDistance(Color color1, Color color2)
		{
			int num = (int)(color1.R - color2.R);
			int num2 = (int)(color1.G - color2.G);
			int num3 = (int)(color1.B - color2.B);
			return num * num + num2 * num2 + num3 * num3;
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0008A5FC File Offset: 0x000887FC
		internal void ComputeBorderStyleCellStateAndCellBounds(int rowIndex, out DataGridViewAdvancedBorderStyle dgvabsEffective, out DataGridViewElementStates cellState, out Rectangle cellBounds)
		{
			bool flag = !base.DataGridView.RowHeadersVisible && base.DataGridView.AdvancedCellBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Single;
			bool flag2 = !base.DataGridView.ColumnHeadersVisible && base.DataGridView.AdvancedCellBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Single;
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = new DataGridViewAdvancedBorderStyle();
			if (rowIndex > -1 && this.OwningColumn != null)
			{
				dgvabsEffective = this.AdjustCellBorderStyle(base.DataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStyle, flag, flag2, this.ColumnIndex == base.DataGridView.FirstDisplayedColumnIndex, rowIndex == base.DataGridView.FirstDisplayedRowIndex);
				DataGridViewElementStates rowState = base.DataGridView.Rows.GetRowState(rowIndex);
				cellState = this.CellStateFromColumnRowStates(rowState);
				cellState |= this.State;
			}
			else if (this.OwningColumn != null)
			{
				DataGridViewColumn lastColumn = base.DataGridView.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None);
				bool flag3 = lastColumn != null && lastColumn.Index == this.ColumnIndex;
				dgvabsEffective = base.DataGridView.AdjustColumnHeaderBorderStyle(base.DataGridView.AdvancedColumnHeadersBorderStyle, dataGridViewAdvancedBorderStyle, this.ColumnIndex == base.DataGridView.FirstDisplayedColumnIndex, flag3);
				cellState = this.OwningColumn.State | this.State;
			}
			else if (this.OwningRow != null)
			{
				dgvabsEffective = this.OwningRow.AdjustRowHeaderBorderStyle(base.DataGridView.AdvancedRowHeadersBorderStyle, dataGridViewAdvancedBorderStyle, flag, flag2, rowIndex == base.DataGridView.FirstDisplayedRowIndex, rowIndex == base.DataGridView.Rows.GetLastRow(DataGridViewElementStates.Visible));
				cellState = this.OwningRow.GetState(rowIndex) | this.State;
			}
			else
			{
				dgvabsEffective = base.DataGridView.AdjustedTopLeftHeaderBorderStyle;
				cellState = this.State;
			}
			cellBounds = new Rectangle(new Point(0, 0), this.GetSize(rowIndex));
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0008A7CC File Offset: 0x000889CC
		internal Rectangle ComputeErrorIconBounds(Rectangle cellValueBounds)
		{
			if (cellValueBounds.Width >= (int)(8 + DataGridViewCell.iconsWidth) && cellValueBounds.Height >= (int)(8 + DataGridViewCell.iconsHeight))
			{
				Rectangle rectangle = new Rectangle(base.DataGridView.RightToLeftInternal ? (cellValueBounds.Left + 4) : (cellValueBounds.Right - 4 - (int)DataGridViewCell.iconsWidth), cellValueBounds.Y + (cellValueBounds.Height - (int)DataGridViewCell.iconsHeight) / 2, (int)DataGridViewCell.iconsWidth, (int)DataGridViewCell.iconsHeight);
				return rectangle;
			}
			return Rectangle.Empty;
		}

		/// <summary>Indicates whether the cell's row will be unshared when the cell's content is clicked.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnContentClick(System.Windows.Forms.DataGridViewCellEventArgs)" /> method.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001D77 RID: 7543 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool ContentClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return false;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0008A84F File Offset: 0x00088A4F
		internal bool ContentClickUnsharesRowInternal(DataGridViewCellEventArgs e)
		{
			return this.ContentClickUnsharesRow(e);
		}

		/// <summary>Indicates whether the cell's row will be unshared when the cell's content is double-clicked.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs)" /> method.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001D79 RID: 7545 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool ContentDoubleClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return false;
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0008A858 File Offset: 0x00088A58
		internal bool ContentDoubleClickUnsharesRowInternal(DataGridViewCellEventArgs e)
		{
			return this.ContentDoubleClickUnsharesRow(e);
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x06001D7B RID: 7547 RVA: 0x0008A861 File Offset: 0x00088A61
		protected virtual AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewCell.DataGridViewCellAccessibleObject(this);
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0008A869 File Offset: 0x00088A69
		private void DetachContextMenuStrip(object sender, EventArgs e)
		{
			this.ContextMenuStripInternal = null;
		}

		/// <summary>Removes the cell's editing control from the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">This cell is not associated with a <see cref="T:System.Windows.Forms.DataGridView" />.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridView.EditingControl" /> property of the associated <see cref="T:System.Windows.Forms.DataGridView" /> has a value of <see langword="null" />. This is the case, for example, when the control is not in edit mode.</exception>
		// Token: 0x06001D7D RID: 7549 RVA: 0x0008A874 File Offset: 0x00088A74
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void DetachEditingControl()
		{
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView == null || dataGridView.EditingControl == null)
			{
				throw new InvalidOperationException();
			}
			if (dataGridView.EditingControl.ParentInternal != null)
			{
				if (dataGridView.EditingControl.ContainsFocus)
				{
					ContainerControl containerControl = dataGridView.GetContainerControlInternal() as ContainerControl;
					if (containerControl != null && (dataGridView.EditingControl == containerControl.ActiveControl || dataGridView.EditingControl.Contains(containerControl.ActiveControl)))
					{
						dataGridView.FocusInternal();
					}
					else
					{
						UnsafeNativeMethods.SetFocus(new HandleRef(null, IntPtr.Zero));
					}
				}
				dataGridView.EditingPanel.Controls.Remove(dataGridView.EditingControl);
				if (AccessibilityImprovements.Level3 && this.AccessibleRestructuringNeeded)
				{
					dataGridView.EditingControlAccessibleObject.SetParent(null);
					this.AccessibilityObject.SetDetachableChild(null);
					this.AccessibilityObject.RaiseStructureChangedEvent(UnsafeNativeMethods.StructureChangeType.ChildRemoved, dataGridView.EditingControlAccessibleObject.RuntimeId);
				}
			}
			if (dataGridView.EditingPanel.ParentInternal != null)
			{
				((DataGridView.DataGridViewControlCollection)dataGridView.Controls).RemoveInternal(dataGridView.EditingPanel);
			}
			this.CurrentMouseLocation = 0;
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x0008A980 File Offset: 0x00088B80
		private bool AccessibleRestructuringNeeded
		{
			get
			{
				Type type = base.DataGridView.EditingControl.GetType();
				return (type == typeof(DataGridViewComboBoxEditingControl) && !type.IsSubclassOf(typeof(DataGridViewComboBoxEditingControl))) || (type == typeof(DataGridViewTextBoxEditingControl) && !type.IsSubclassOf(typeof(DataGridViewTextBoxEditingControl)));
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		// Token: 0x06001D7F RID: 7551 RVA: 0x0008A9EB File Offset: 0x00088BEB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.DataGridViewCell" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001D80 RID: 7552 RVA: 0x0008A9FC File Offset: 0x00088BFC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				ContextMenuStrip contextMenuStripInternal = this.ContextMenuStripInternal;
				if (contextMenuStripInternal != null)
				{
					contextMenuStripInternal.Disposed -= this.DetachContextMenuStrip;
				}
			}
		}

		/// <summary>Indicates whether the cell's row will be unshared when the cell is double-clicked.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs)" /> method.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001D81 RID: 7553 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool DoubleClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return false;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0008AA28 File Offset: 0x00088C28
		internal bool DoubleClickUnsharesRowInternal(DataGridViewCellEventArgs e)
		{
			return this.DoubleClickUnsharesRow(e);
		}

		/// <summary>Indicates whether the parent row will be unshared when the focus moves to the cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="throughMouseClick">
		///   <see langword="true" /> if a user action moved focus to the cell; <see langword="false" /> if a programmatic operation moved focus to the cell.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared; otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001D83 RID: 7555 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool EnterUnsharesRow(int rowIndex, bool throughMouseClick)
		{
			return false;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0008AA31 File Offset: 0x00088C31
		internal bool EnterUnsharesRowInternal(int rowIndex, bool throughMouseClick)
		{
			return this.EnterUnsharesRow(rowIndex, throughMouseClick);
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0008AA3C File Offset: 0x00088C3C
		internal static void FormatPlainText(string s, bool csv, TextWriter output, ref bool escapeApplied)
		{
			if (s == null)
			{
				return;
			}
			int length = s.Length;
			for (int i = 0; i < length; i++)
			{
				char c = s[i];
				if (c != '\t')
				{
					if (c != '"')
					{
						if (c != ',')
						{
							output.Write(c);
						}
						else
						{
							if (csv)
							{
								escapeApplied = true;
							}
							output.Write(',');
						}
					}
					else if (csv)
					{
						output.Write("\"\"");
						escapeApplied = true;
					}
					else
					{
						output.Write('"');
					}
				}
				else if (!csv)
				{
					output.Write(' ');
				}
				else
				{
					output.Write('\t');
				}
			}
			if (escapeApplied)
			{
				output.Write('"');
			}
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0008AAD0 File Offset: 0x00088CD0
		internal static void FormatPlainTextAsHtml(string s, TextWriter output)
		{
			if (s == null)
			{
				return;
			}
			int length = s.Length;
			char c = '\0';
			int i = 0;
			while (i < length)
			{
				char c2 = s[i];
				if (c2 <= ' ')
				{
					if (c2 != '\n')
					{
						if (c2 != '\r')
						{
							if (c2 != ' ')
							{
								goto IL_B7;
							}
							if (c == ' ')
							{
								output.Write("&nbsp;");
							}
							else
							{
								output.Write(c2);
							}
						}
					}
					else
					{
						output.Write("<br>");
					}
				}
				else if (c2 <= '&')
				{
					if (c2 != '"')
					{
						if (c2 != '&')
						{
							goto IL_B7;
						}
						output.Write("&amp;");
					}
					else
					{
						output.Write("&quot;");
					}
				}
				else if (c2 != '<')
				{
					if (c2 != '>')
					{
						goto IL_B7;
					}
					output.Write("&gt;");
				}
				else
				{
					output.Write("&lt;");
				}
				IL_F8:
				c = c2;
				i++;
				continue;
				IL_B7:
				if (c2 >= '\u00a0' && c2 < 'Ā')
				{
					output.Write("&#");
					int num = (int)c2;
					output.Write(num.ToString(NumberFormatInfo.InvariantInfo));
					output.Write(';');
					goto IL_F8;
				}
				output.Write(c2);
				goto IL_F8;
			}
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0008ABE4 File Offset: 0x00088DE4
		private static Bitmap GetBitmap(string bitmapName)
		{
			Bitmap bitmap = new Bitmap(typeof(DataGridViewCell), bitmapName);
			bitmap.MakeTransparent();
			if (DpiHelper.IsScalingRequired)
			{
				Bitmap bitmap2 = DpiHelper.CreateResizedBitmap(bitmap, new Size((int)DataGridViewCell.iconsWidth, (int)DataGridViewCell.iconsHeight));
				if (bitmap2 != null)
				{
					bitmap.Dispose();
					bitmap = bitmap2;
				}
			}
			return bitmap;
		}

		/// <summary>Retrieves the formatted value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard" />.</summary>
		/// <param name="rowIndex">The zero-based index of the row containing the cell.</param>
		/// <param name="firstCell">
		///   <see langword="true" /> to indicate that the cell is in the first column of the region defined by the selected cells; otherwise, <see langword="false" />.</param>
		/// <param name="lastCell">
		///   <see langword="true" /> to indicate that the cell is the last column of the region defined by the selected cells; otherwise, <see langword="false" />.</param>
		/// <param name="inFirstRow">
		///   <see langword="true" /> to indicate that the cell is in the first row of the region defined by the selected cells; otherwise, <see langword="false" />.</param>
		/// <param name="inLastRow">
		///   <see langword="true" /> to indicate that the cell is in the last row of the region defined by the selected cells; otherwise, <see langword="false" />.</param>
		/// <param name="format">The current format string of the cell.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than 0 or greater than or equal to the number of rows in the control.</exception>
		/// <exception cref="T:System.InvalidOperationException">The value of the cell's <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property is <see langword="null" />.  
		///  -or-  
		///  <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to <see langword="true" />. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		// Token: 0x06001D88 RID: 7560 RVA: 0x0008AC34 File Offset: 0x00088E34
		protected virtual object GetClipboardContent(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			object obj = null;
			if (base.DataGridView.IsSharedCellSelected(this, rowIndex))
			{
				obj = this.GetEditedFormattedValue(this.GetValue(rowIndex), rowIndex, ref inheritedStyle, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.ClipboardContent);
			}
			StringBuilder stringBuilder = new StringBuilder(64);
			if (string.Equals(format, DataFormats.Html, StringComparison.OrdinalIgnoreCase))
			{
				if (firstCell)
				{
					if (inFirstRow)
					{
						stringBuilder.Append("<TABLE>");
					}
					stringBuilder.Append("<TR>");
				}
				stringBuilder.Append("<TD>");
				if (obj != null)
				{
					DataGridViewCell.FormatPlainTextAsHtml(obj.ToString(), new StringWriter(stringBuilder, CultureInfo.CurrentCulture));
				}
				else
				{
					stringBuilder.Append("&nbsp;");
				}
				stringBuilder.Append("</TD>");
				if (lastCell)
				{
					stringBuilder.Append("</TR>");
					if (inLastRow)
					{
						stringBuilder.Append("</TABLE>");
					}
				}
				return stringBuilder.ToString();
			}
			bool flag = string.Equals(format, DataFormats.CommaSeparatedValue, StringComparison.OrdinalIgnoreCase);
			if (flag || string.Equals(format, DataFormats.Text, StringComparison.OrdinalIgnoreCase) || string.Equals(format, DataFormats.UnicodeText, StringComparison.OrdinalIgnoreCase))
			{
				if (obj != null)
				{
					if (firstCell && lastCell && inFirstRow && inLastRow)
					{
						stringBuilder.Append(obj.ToString());
					}
					else
					{
						bool flag2 = false;
						int length = stringBuilder.Length;
						DataGridViewCell.FormatPlainText(obj.ToString(), flag, new StringWriter(stringBuilder, CultureInfo.CurrentCulture), ref flag2);
						if (flag2)
						{
							stringBuilder.Insert(length, '"');
						}
					}
				}
				if (lastCell)
				{
					if (!inLastRow)
					{
						stringBuilder.Append('\r');
						stringBuilder.Append('\n');
					}
				}
				else
				{
					stringBuilder.Append(flag ? ',' : '\t');
				}
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x0008ADF1 File Offset: 0x00088FF1
		internal object GetClipboardContentInternal(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
		{
			return this.GetClipboardContent(rowIndex, firstCell, lastCell, inFirstRow, inLastRow, format);
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x0008AE04 File Offset: 0x00089004
		internal ContextMenuStrip GetContextMenuStrip(int rowIndex)
		{
			ContextMenuStrip contextMenuStrip = this.ContextMenuStripInternal;
			if (base.DataGridView != null && (base.DataGridView.VirtualMode || base.DataGridView.DataSource != null))
			{
				contextMenuStrip = base.DataGridView.OnCellContextMenuStripNeeded(this.ColumnIndex, rowIndex, contextMenuStrip);
			}
			return contextMenuStrip;
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0008AE50 File Offset: 0x00089050
		internal void GetContrastedPens(Color baseline, ref Pen darkPen, ref Pen lightPen)
		{
			int num = DataGridViewCell.ColorDistance(baseline, SystemColors.ControlDark);
			int num2 = DataGridViewCell.ColorDistance(baseline, SystemColors.ControlLightLight);
			if (SystemInformation.HighContrast)
			{
				if (num < 2000)
				{
					darkPen = base.DataGridView.GetCachedPen(ControlPaint.DarkDark(baseline));
				}
				else
				{
					darkPen = base.DataGridView.GetCachedPen(SystemColors.ControlDark);
				}
				if (num2 < 2000)
				{
					lightPen = base.DataGridView.GetCachedPen(ControlPaint.LightLight(baseline));
					return;
				}
				lightPen = base.DataGridView.GetCachedPen(SystemColors.ControlLightLight);
				return;
			}
			else
			{
				if (num < 1000)
				{
					darkPen = base.DataGridView.GetCachedPen(ControlPaint.Dark(baseline));
				}
				else
				{
					darkPen = base.DataGridView.GetCachedPen(SystemColors.ControlDark);
				}
				if (num2 < 1000)
				{
					lightPen = base.DataGridView.GetCachedPen(ControlPaint.Light(baseline));
					return;
				}
				lightPen = base.DataGridView.GetCachedPen(SystemColors.ControlLightLight);
				return;
			}
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's content area using a default <see cref="T:System.Drawing.Graphics" /> and cell style currently in effect for the cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="rowIndex" /> is less than 0 or greater than the number of rows in the control minus 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		// Token: 0x06001D8C RID: 7564 RVA: 0x0008AF38 File Offset: 0x00089138
		public Rectangle GetContentBounds(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			Rectangle contentBounds;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				contentBounds = this.GetContentBounds(graphics, inheritedStyle, rowIndex);
			}
			return contentBounds;
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics" /> and cell style.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		// Token: 0x06001D8D RID: 7565 RVA: 0x00054155 File Offset: 0x00052355
		protected virtual Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			return Rectangle.Empty;
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0008AF8C File Offset: 0x0008918C
		internal object GetEditedFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle dataGridViewCellStyle, DataGridViewDataErrorContexts context)
		{
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			if (this.ColumnIndex != currentCellAddress.X || rowIndex != currentCellAddress.Y)
			{
				return this.GetFormattedValue(value, rowIndex, ref dataGridViewCellStyle, null, null, context);
			}
			IDataGridViewEditingControl dataGridViewEditingControl = (IDataGridViewEditingControl)base.DataGridView.EditingControl;
			if (dataGridViewEditingControl != null)
			{
				return dataGridViewEditingControl.GetEditingControlFormattedValue(context);
			}
			IDataGridViewEditingCell dataGridViewEditingCell = this as IDataGridViewEditingCell;
			if (dataGridViewEditingCell != null && base.DataGridView.IsCurrentCellInEditMode)
			{
				return dataGridViewEditingCell.GetEditingCellFormattedValue(context);
			}
			return this.GetFormattedValue(value, rowIndex, ref dataGridViewCellStyle, null, null, context);
		}

		/// <summary>Returns the current, formatted value of the cell, regardless of whether the cell is in edit mode and the value has not been committed.</summary>
		/// <param name="rowIndex">The row index of the cell.</param>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the data error context.</param>
		/// <returns>The current, formatted value of the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="rowIndex" /> is less than 0 or greater than the number of rows in the control minus 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to <see langword="true" />. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		// Token: 0x06001D8F RID: 7567 RVA: 0x0008B018 File Offset: 0x00089218
		public object GetEditedFormattedValue(int rowIndex, DataGridViewDataErrorContexts context)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			return this.GetEditedFormattedValue(this.GetValue(rowIndex), rowIndex, ref inheritedStyle, context);
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0008B04C File Offset: 0x0008924C
		internal Rectangle GetErrorIconBounds(int rowIndex)
		{
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			Rectangle errorIconBounds;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				errorIconBounds = this.GetErrorIconBounds(graphics, inheritedStyle, rowIndex);
			}
			return errorIconBounds;
		}

		/// <summary>Returns the bounding rectangle that encloses the cell's error icon, if one is displayed.</summary>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		// Token: 0x06001D91 RID: 7569 RVA: 0x00054155 File Offset: 0x00052355
		protected virtual Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			return Rectangle.Empty;
		}

		/// <summary>Returns a string that represents the error for the cell.</summary>
		/// <param name="rowIndex">The row index of the cell.</param>
		/// <returns>A string that describes the error for the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x06001D92 RID: 7570 RVA: 0x0008B090 File Offset: 0x00089290
		protected internal virtual string GetErrorText(int rowIndex)
		{
			string text = string.Empty;
			object @object = this.Properties.GetObject(DataGridViewCell.PropCellErrorText);
			if (@object != null)
			{
				text = (string)@object;
			}
			else if (base.DataGridView != null && rowIndex != -1 && rowIndex != base.DataGridView.NewRowIndex && this.OwningColumn != null && this.OwningColumn.IsDataBound && base.DataGridView.DataConnection != null)
			{
				text = base.DataGridView.DataConnection.GetError(this.OwningColumn.BoundColumnIndex, this.ColumnIndex, rowIndex);
			}
			if (base.DataGridView != null && (base.DataGridView.VirtualMode || base.DataGridView.DataSource != null) && this.ColumnIndex >= 0 && rowIndex >= 0)
			{
				text = base.DataGridView.OnCellErrorTextNeeded(this.ColumnIndex, rowIndex, text);
			}
			return text;
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0008B163 File Offset: 0x00089363
		internal object GetFormattedValue(int rowIndex, ref DataGridViewCellStyle cellStyle, DataGridViewDataErrorContexts context)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			return this.GetFormattedValue(this.GetValue(rowIndex), rowIndex, ref cellStyle, null, null, context);
		}

		/// <summary>Gets the value of the cell as formatted for display.</summary>
		/// <param name="value">The value to be formatted.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
		/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the value type that provides custom conversion to the formatted value type, or <see langword="null" /> if no such custom conversion is needed.</param>
		/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the formatted value type that provides custom conversion from the value type, or <see langword="null" /> if no such custom conversion is needed.</param>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values describing the context in which the formatted value is needed.</param>
		/// <returns>The formatted value of the cell or <see langword="null" /> if the cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
		/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to <see langword="true" />. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
		// Token: 0x06001D94 RID: 7572 RVA: 0x0008B184 File Offset: 0x00089384
		protected virtual object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			DataGridViewCellFormattingEventArgs dataGridViewCellFormattingEventArgs = base.DataGridView.OnCellFormatting(this.ColumnIndex, rowIndex, value, this.FormattedValueType, cellStyle);
			cellStyle = dataGridViewCellFormattingEventArgs.CellStyle;
			bool formattingApplied = dataGridViewCellFormattingEventArgs.FormattingApplied;
			object obj = dataGridViewCellFormattingEventArgs.Value;
			bool flag = true;
			if (!formattingApplied && this.FormattedValueType != null && (obj == null || !this.FormattedValueType.IsAssignableFrom(obj.GetType())))
			{
				try
				{
					obj = Formatter.FormatObject(obj, this.FormattedValueType, (valueTypeConverter == null) ? this.ValueTypeConverter : valueTypeConverter, (formattedValueTypeConverter == null) ? this.FormattedValueTypeConverter : formattedValueTypeConverter, cellStyle.Format, cellStyle.FormatProvider, cellStyle.NullValue, cellStyle.DataSourceNullValue);
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
					DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs = new DataGridViewDataErrorEventArgs(ex, this.ColumnIndex, rowIndex, context);
					base.RaiseDataError(dataGridViewDataErrorEventArgs);
					if (dataGridViewDataErrorEventArgs.ThrowException)
					{
						throw dataGridViewDataErrorEventArgs.Exception;
					}
					flag = false;
				}
			}
			if (flag && (obj == null || this.FormattedValueType == null || !this.FormattedValueType.IsAssignableFrom(obj.GetType())))
			{
				if (obj == null && cellStyle.NullValue == null && this.FormattedValueType != null && !typeof(ValueType).IsAssignableFrom(this.FormattedValueType))
				{
					return null;
				}
				Exception ex2;
				if (this.FormattedValueType == null)
				{
					ex2 = new FormatException(SR.GetString("DataGridViewCell_FormattedValueTypeNull"));
				}
				else
				{
					ex2 = new FormatException(SR.GetString("DataGridViewCell_FormattedValueHasWrongType"));
				}
				DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs2 = new DataGridViewDataErrorEventArgs(ex2, this.ColumnIndex, rowIndex, context);
				base.RaiseDataError(dataGridViewDataErrorEventArgs2);
				if (dataGridViewDataErrorEventArgs2.ThrowException)
				{
					throw dataGridViewDataErrorEventArgs2.Exception;
				}
			}
			return obj;
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x0008B354 File Offset: 0x00089554
		internal static DataGridViewFreeDimension GetFreeDimensionFromConstraint(Size constraintSize)
		{
			if (constraintSize.Width < 0 || constraintSize.Height < 0)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"constraintSize",
					constraintSize.ToString()
				}));
			}
			if (constraintSize.Width == 0)
			{
				if (constraintSize.Height == 0)
				{
					return DataGridViewFreeDimension.Both;
				}
				return DataGridViewFreeDimension.Width;
			}
			else
			{
				if (constraintSize.Height == 0)
				{
					return DataGridViewFreeDimension.Height;
				}
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"constraintSize",
					constraintSize.ToString()
				}));
			}
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x0008B3F1 File Offset: 0x000895F1
		internal int GetHeight(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return -1;
			}
			return this.owningRow.GetHeight(rowIndex);
		}

		/// <summary>Gets the inherited shortcut menu for the current cell.</summary>
		/// <param name="rowIndex">The row index of the current cell.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip" /> if the parent <see cref="T:System.Windows.Forms.DataGridView" />, <see cref="T:System.Windows.Forms.DataGridViewRow" />, or <see cref="T:System.Windows.Forms.DataGridViewColumn" /> has a <see cref="T:System.Windows.Forms.ContextMenuStrip" /> assigned; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell is not <see langword="null" /> and the specified <paramref name="rowIndex" /> is less than 0 or greater than the number of rows in the control minus 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		// Token: 0x06001D97 RID: 7575 RVA: 0x0008B40C File Offset: 0x0008960C
		public virtual ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
		{
			if (base.DataGridView != null)
			{
				if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				if (this.ColumnIndex < 0)
				{
					throw new InvalidOperationException();
				}
			}
			ContextMenuStrip contextMenuStrip = this.GetContextMenuStrip(rowIndex);
			if (contextMenuStrip != null)
			{
				return contextMenuStrip;
			}
			if (this.owningRow != null)
			{
				contextMenuStrip = this.owningRow.GetContextMenuStrip(rowIndex);
				if (contextMenuStrip != null)
				{
					return contextMenuStrip;
				}
			}
			if (this.owningColumn != null)
			{
				contextMenuStrip = this.owningColumn.ContextMenuStrip;
				if (contextMenuStrip != null)
				{
					return contextMenuStrip;
				}
			}
			if (base.DataGridView != null)
			{
				return base.DataGridView.ContextMenuStrip;
			}
			return null;
		}

		/// <summary>Returns a value indicating the current state of the cell as inherited from the state of its row and column.</summary>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values representing the current state of the cell.</returns>
		/// <exception cref="T:System.ArgumentException">The cell is not contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control and <paramref name="rowIndex" /> is not -1.  
		///  -or-  
		///  <paramref name="rowIndex" /> is not the index of the row containing this cell.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The cell is contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control and <paramref name="rowIndex" /> is outside the valid range of 0 to the number of rows in the control minus 1.</exception>
		// Token: 0x06001D98 RID: 7576 RVA: 0x0008B4A8 File Offset: 0x000896A8
		public virtual DataGridViewElementStates GetInheritedState(int rowIndex)
		{
			DataGridViewElementStates dataGridViewElementStates = this.State | DataGridViewElementStates.ResizableSet;
			if (base.DataGridView == null)
			{
				if (rowIndex != -1)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"rowIndex",
						rowIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owningRow != null)
				{
					dataGridViewElementStates |= this.owningRow.GetState(-1) & (DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible);
					if (this.owningRow.GetResizable(rowIndex) == DataGridViewTriState.True)
					{
						dataGridViewElementStates |= DataGridViewElementStates.Resizable;
					}
				}
				return dataGridViewElementStates;
			}
			else
			{
				if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				if (base.DataGridView.Rows.SharedRow(rowIndex) != this.owningRow)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"rowIndex",
						rowIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
				DataGridViewElementStates rowState = base.DataGridView.Rows.GetRowState(rowIndex);
				dataGridViewElementStates |= rowState & (DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Selected);
				dataGridViewElementStates |= this.owningColumn.State & (DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Selected);
				if (this.owningRow.GetResizable(rowIndex) == DataGridViewTriState.True || this.owningColumn.Resizable == DataGridViewTriState.True)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Resizable;
				}
				if (this.owningColumn.Visible && this.owningRow.GetVisible(rowIndex))
				{
					dataGridViewElementStates |= DataGridViewElementStates.Visible;
					if (this.owningColumn.Displayed && this.owningRow.GetDisplayed(rowIndex))
					{
						dataGridViewElementStates |= DataGridViewElementStates.Displayed;
					}
				}
				if (this.owningColumn.Frozen && this.owningRow.GetFrozen(rowIndex))
				{
					dataGridViewElementStates |= DataGridViewElementStates.Frozen;
				}
				return dataGridViewElementStates;
			}
		}

		/// <summary>Gets the style applied to the cell.</summary>
		/// <param name="inheritedCellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be populated with the inherited cell style.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="includeColors">
		///   <see langword="true" /> to include inherited colors in the returned cell style; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that includes the style settings of the cell inherited from the cell's parent row, column, and <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The cell has no associated <see cref="T:System.Windows.Forms.DataGridView" />.  
		///  -or-  
		///  <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than 0, or greater than or equal to the number of rows in the parent <see cref="T:System.Windows.Forms.DataGridView" />.</exception>
		// Token: 0x06001D99 RID: 7577 RVA: 0x0008B63C File Offset: 0x0008983C
		public virtual DataGridViewCellStyle GetInheritedStyle(DataGridViewCellStyle inheritedCellStyle, int rowIndex, bool includeColors)
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_CellNeedsDataGridViewForInheritedStyle"));
			}
			if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (this.ColumnIndex < 0)
			{
				throw new InvalidOperationException();
			}
			DataGridViewCellStyle dataGridViewCellStyle;
			if (inheritedCellStyle == null)
			{
				dataGridViewCellStyle = base.DataGridView.PlaceholderCellStyle;
				if (!includeColors)
				{
					dataGridViewCellStyle.BackColor = Color.Empty;
					dataGridViewCellStyle.ForeColor = Color.Empty;
					dataGridViewCellStyle.SelectionBackColor = Color.Empty;
					dataGridViewCellStyle.SelectionForeColor = Color.Empty;
				}
			}
			else
			{
				dataGridViewCellStyle = inheritedCellStyle;
			}
			DataGridViewCellStyle dataGridViewCellStyle2 = null;
			if (this.HasStyle)
			{
				dataGridViewCellStyle2 = this.Style;
			}
			DataGridViewCellStyle dataGridViewCellStyle3 = null;
			if (base.DataGridView.Rows.SharedRow(rowIndex).HasDefaultCellStyle)
			{
				dataGridViewCellStyle3 = base.DataGridView.Rows.SharedRow(rowIndex).DefaultCellStyle;
			}
			DataGridViewCellStyle dataGridViewCellStyle4 = null;
			if (this.owningColumn.HasDefaultCellStyle)
			{
				dataGridViewCellStyle4 = this.owningColumn.DefaultCellStyle;
			}
			DataGridViewCellStyle defaultCellStyle = base.DataGridView.DefaultCellStyle;
			if (includeColors)
			{
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.BackColor.IsEmpty)
				{
					dataGridViewCellStyle.BackColor = dataGridViewCellStyle2.BackColor;
				}
				else if (dataGridViewCellStyle3 != null && !dataGridViewCellStyle3.BackColor.IsEmpty)
				{
					dataGridViewCellStyle.BackColor = dataGridViewCellStyle3.BackColor;
				}
				else if (!base.DataGridView.RowsDefaultCellStyle.BackColor.IsEmpty && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.BackColor.IsEmpty))
				{
					dataGridViewCellStyle.BackColor = base.DataGridView.RowsDefaultCellStyle.BackColor;
				}
				else if (rowIndex % 2 == 1 && !base.DataGridView.AlternatingRowsDefaultCellStyle.BackColor.IsEmpty)
				{
					dataGridViewCellStyle.BackColor = base.DataGridView.AlternatingRowsDefaultCellStyle.BackColor;
				}
				else if (dataGridViewCellStyle4 != null && !dataGridViewCellStyle4.BackColor.IsEmpty)
				{
					dataGridViewCellStyle.BackColor = dataGridViewCellStyle4.BackColor;
				}
				else
				{
					dataGridViewCellStyle.BackColor = defaultCellStyle.BackColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle.ForeColor = dataGridViewCellStyle2.ForeColor;
				}
				else if (dataGridViewCellStyle3 != null && !dataGridViewCellStyle3.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle.ForeColor = dataGridViewCellStyle3.ForeColor;
				}
				else if (!base.DataGridView.RowsDefaultCellStyle.ForeColor.IsEmpty && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.ForeColor.IsEmpty))
				{
					dataGridViewCellStyle.ForeColor = base.DataGridView.RowsDefaultCellStyle.ForeColor;
				}
				else if (rowIndex % 2 == 1 && !base.DataGridView.AlternatingRowsDefaultCellStyle.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle.ForeColor = base.DataGridView.AlternatingRowsDefaultCellStyle.ForeColor;
				}
				else if (dataGridViewCellStyle4 != null && !dataGridViewCellStyle4.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle.ForeColor = dataGridViewCellStyle4.ForeColor;
				}
				else
				{
					dataGridViewCellStyle.ForeColor = defaultCellStyle.ForeColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionBackColor = dataGridViewCellStyle2.SelectionBackColor;
				}
				else if (dataGridViewCellStyle3 != null && !dataGridViewCellStyle3.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionBackColor = dataGridViewCellStyle3.SelectionBackColor;
				}
				else if (!base.DataGridView.RowsDefaultCellStyle.SelectionBackColor.IsEmpty && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor.IsEmpty))
				{
					dataGridViewCellStyle.SelectionBackColor = base.DataGridView.RowsDefaultCellStyle.SelectionBackColor;
				}
				else if (rowIndex % 2 == 1 && !base.DataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionBackColor = base.DataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor;
				}
				else if (dataGridViewCellStyle4 != null && !dataGridViewCellStyle4.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionBackColor = dataGridViewCellStyle4.SelectionBackColor;
				}
				else
				{
					dataGridViewCellStyle.SelectionBackColor = defaultCellStyle.SelectionBackColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionForeColor = dataGridViewCellStyle2.SelectionForeColor;
				}
				else if (dataGridViewCellStyle3 != null && !dataGridViewCellStyle3.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionForeColor = dataGridViewCellStyle3.SelectionForeColor;
				}
				else if (!base.DataGridView.RowsDefaultCellStyle.SelectionForeColor.IsEmpty && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor.IsEmpty))
				{
					dataGridViewCellStyle.SelectionForeColor = base.DataGridView.RowsDefaultCellStyle.SelectionForeColor;
				}
				else if (rowIndex % 2 == 1 && !base.DataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionForeColor = base.DataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor;
				}
				else if (dataGridViewCellStyle4 != null && !dataGridViewCellStyle4.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionForeColor = dataGridViewCellStyle4.SelectionForeColor;
				}
				else
				{
					dataGridViewCellStyle.SelectionForeColor = defaultCellStyle.SelectionForeColor;
				}
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Font != null)
			{
				dataGridViewCellStyle.Font = dataGridViewCellStyle2.Font;
			}
			else if (dataGridViewCellStyle3 != null && dataGridViewCellStyle3.Font != null)
			{
				dataGridViewCellStyle.Font = dataGridViewCellStyle3.Font;
			}
			else if (base.DataGridView.RowsDefaultCellStyle.Font != null && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.Font == null))
			{
				dataGridViewCellStyle.Font = base.DataGridView.RowsDefaultCellStyle.Font;
			}
			else if (rowIndex % 2 == 1 && base.DataGridView.AlternatingRowsDefaultCellStyle.Font != null)
			{
				dataGridViewCellStyle.Font = base.DataGridView.AlternatingRowsDefaultCellStyle.Font;
			}
			else if (dataGridViewCellStyle4 != null && dataGridViewCellStyle4.Font != null)
			{
				dataGridViewCellStyle.Font = dataGridViewCellStyle4.Font;
			}
			else
			{
				dataGridViewCellStyle.Font = defaultCellStyle.Font;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsNullValueDefault)
			{
				dataGridViewCellStyle.NullValue = dataGridViewCellStyle2.NullValue;
			}
			else if (dataGridViewCellStyle3 != null && !dataGridViewCellStyle3.IsNullValueDefault)
			{
				dataGridViewCellStyle.NullValue = dataGridViewCellStyle3.NullValue;
			}
			else if (!base.DataGridView.RowsDefaultCellStyle.IsNullValueDefault && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.IsNullValueDefault))
			{
				dataGridViewCellStyle.NullValue = base.DataGridView.RowsDefaultCellStyle.NullValue;
			}
			else if (rowIndex % 2 == 1 && !base.DataGridView.AlternatingRowsDefaultCellStyle.IsNullValueDefault)
			{
				dataGridViewCellStyle.NullValue = base.DataGridView.AlternatingRowsDefaultCellStyle.NullValue;
			}
			else if (dataGridViewCellStyle4 != null && !dataGridViewCellStyle4.IsNullValueDefault)
			{
				dataGridViewCellStyle.NullValue = dataGridViewCellStyle4.NullValue;
			}
			else
			{
				dataGridViewCellStyle.NullValue = defaultCellStyle.NullValue;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsDataSourceNullValueDefault)
			{
				dataGridViewCellStyle.DataSourceNullValue = dataGridViewCellStyle2.DataSourceNullValue;
			}
			else if (dataGridViewCellStyle3 != null && !dataGridViewCellStyle3.IsDataSourceNullValueDefault)
			{
				dataGridViewCellStyle.DataSourceNullValue = dataGridViewCellStyle3.DataSourceNullValue;
			}
			else if (!base.DataGridView.RowsDefaultCellStyle.IsDataSourceNullValueDefault && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.IsDataSourceNullValueDefault))
			{
				dataGridViewCellStyle.DataSourceNullValue = base.DataGridView.RowsDefaultCellStyle.DataSourceNullValue;
			}
			else if (rowIndex % 2 == 1 && !base.DataGridView.AlternatingRowsDefaultCellStyle.IsDataSourceNullValueDefault)
			{
				dataGridViewCellStyle.DataSourceNullValue = base.DataGridView.AlternatingRowsDefaultCellStyle.DataSourceNullValue;
			}
			else if (dataGridViewCellStyle4 != null && !dataGridViewCellStyle4.IsDataSourceNullValueDefault)
			{
				dataGridViewCellStyle.DataSourceNullValue = dataGridViewCellStyle4.DataSourceNullValue;
			}
			else
			{
				dataGridViewCellStyle.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Format.Length != 0)
			{
				dataGridViewCellStyle.Format = dataGridViewCellStyle2.Format;
			}
			else if (dataGridViewCellStyle3 != null && dataGridViewCellStyle3.Format.Length != 0)
			{
				dataGridViewCellStyle.Format = dataGridViewCellStyle3.Format;
			}
			else if (base.DataGridView.RowsDefaultCellStyle.Format.Length != 0 && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.Format.Length == 0))
			{
				dataGridViewCellStyle.Format = base.DataGridView.RowsDefaultCellStyle.Format;
			}
			else if (rowIndex % 2 == 1 && base.DataGridView.AlternatingRowsDefaultCellStyle.Format.Length != 0)
			{
				dataGridViewCellStyle.Format = base.DataGridView.AlternatingRowsDefaultCellStyle.Format;
			}
			else if (dataGridViewCellStyle4 != null && dataGridViewCellStyle4.Format.Length != 0)
			{
				dataGridViewCellStyle.Format = dataGridViewCellStyle4.Format;
			}
			else
			{
				dataGridViewCellStyle.Format = defaultCellStyle.Format;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsFormatProviderDefault)
			{
				dataGridViewCellStyle.FormatProvider = dataGridViewCellStyle2.FormatProvider;
			}
			else if (dataGridViewCellStyle3 != null && !dataGridViewCellStyle3.IsFormatProviderDefault)
			{
				dataGridViewCellStyle.FormatProvider = dataGridViewCellStyle3.FormatProvider;
			}
			else if (!base.DataGridView.RowsDefaultCellStyle.IsFormatProviderDefault && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.IsFormatProviderDefault))
			{
				dataGridViewCellStyle.FormatProvider = base.DataGridView.RowsDefaultCellStyle.FormatProvider;
			}
			else if (rowIndex % 2 == 1 && !base.DataGridView.AlternatingRowsDefaultCellStyle.IsFormatProviderDefault)
			{
				dataGridViewCellStyle.FormatProvider = base.DataGridView.AlternatingRowsDefaultCellStyle.FormatProvider;
			}
			else if (dataGridViewCellStyle4 != null && !dataGridViewCellStyle4.IsFormatProviderDefault)
			{
				dataGridViewCellStyle.FormatProvider = dataGridViewCellStyle4.FormatProvider;
			}
			else
			{
				dataGridViewCellStyle.FormatProvider = defaultCellStyle.FormatProvider;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Alignment != DataGridViewContentAlignment.NotSet)
			{
				dataGridViewCellStyle.AlignmentInternal = dataGridViewCellStyle2.Alignment;
			}
			else if (dataGridViewCellStyle3 != null && dataGridViewCellStyle3.Alignment != DataGridViewContentAlignment.NotSet)
			{
				dataGridViewCellStyle.AlignmentInternal = dataGridViewCellStyle3.Alignment;
			}
			else if (base.DataGridView.RowsDefaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.Alignment == DataGridViewContentAlignment.NotSet))
			{
				dataGridViewCellStyle.AlignmentInternal = base.DataGridView.RowsDefaultCellStyle.Alignment;
			}
			else if (rowIndex % 2 == 1 && base.DataGridView.AlternatingRowsDefaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				dataGridViewCellStyle.AlignmentInternal = base.DataGridView.AlternatingRowsDefaultCellStyle.Alignment;
			}
			else if (dataGridViewCellStyle4 != null && dataGridViewCellStyle4.Alignment != DataGridViewContentAlignment.NotSet)
			{
				dataGridViewCellStyle.AlignmentInternal = dataGridViewCellStyle4.Alignment;
			}
			else
			{
				dataGridViewCellStyle.AlignmentInternal = defaultCellStyle.Alignment;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.WrapMode != DataGridViewTriState.NotSet)
			{
				dataGridViewCellStyle.WrapModeInternal = dataGridViewCellStyle2.WrapMode;
			}
			else if (dataGridViewCellStyle3 != null && dataGridViewCellStyle3.WrapMode != DataGridViewTriState.NotSet)
			{
				dataGridViewCellStyle.WrapModeInternal = dataGridViewCellStyle3.WrapMode;
			}
			else if (base.DataGridView.RowsDefaultCellStyle.WrapMode != DataGridViewTriState.NotSet && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.WrapMode == DataGridViewTriState.NotSet))
			{
				dataGridViewCellStyle.WrapModeInternal = base.DataGridView.RowsDefaultCellStyle.WrapMode;
			}
			else if (rowIndex % 2 == 1 && base.DataGridView.AlternatingRowsDefaultCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				dataGridViewCellStyle.WrapModeInternal = base.DataGridView.AlternatingRowsDefaultCellStyle.WrapMode;
			}
			else if (dataGridViewCellStyle4 != null && dataGridViewCellStyle4.WrapMode != DataGridViewTriState.NotSet)
			{
				dataGridViewCellStyle.WrapModeInternal = dataGridViewCellStyle4.WrapMode;
			}
			else
			{
				dataGridViewCellStyle.WrapModeInternal = defaultCellStyle.WrapMode;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Tag != null)
			{
				dataGridViewCellStyle.Tag = dataGridViewCellStyle2.Tag;
			}
			else if (dataGridViewCellStyle3 != null && dataGridViewCellStyle3.Tag != null)
			{
				dataGridViewCellStyle.Tag = dataGridViewCellStyle3.Tag;
			}
			else if (base.DataGridView.RowsDefaultCellStyle.Tag != null && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.Tag == null))
			{
				dataGridViewCellStyle.Tag = base.DataGridView.RowsDefaultCellStyle.Tag;
			}
			else if (rowIndex % 2 == 1 && base.DataGridView.AlternatingRowsDefaultCellStyle.Tag != null)
			{
				dataGridViewCellStyle.Tag = base.DataGridView.AlternatingRowsDefaultCellStyle.Tag;
			}
			else if (dataGridViewCellStyle4 != null && dataGridViewCellStyle4.Tag != null)
			{
				dataGridViewCellStyle.Tag = dataGridViewCellStyle4.Tag;
			}
			else
			{
				dataGridViewCellStyle.Tag = defaultCellStyle.Tag;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Padding != Padding.Empty)
			{
				dataGridViewCellStyle.PaddingInternal = dataGridViewCellStyle2.Padding;
			}
			else if (dataGridViewCellStyle3 != null && dataGridViewCellStyle3.Padding != Padding.Empty)
			{
				dataGridViewCellStyle.PaddingInternal = dataGridViewCellStyle3.Padding;
			}
			else if (base.DataGridView.RowsDefaultCellStyle.Padding != Padding.Empty && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.Padding == Padding.Empty))
			{
				dataGridViewCellStyle.PaddingInternal = base.DataGridView.RowsDefaultCellStyle.Padding;
			}
			else if (rowIndex % 2 == 1 && base.DataGridView.AlternatingRowsDefaultCellStyle.Padding != Padding.Empty)
			{
				dataGridViewCellStyle.PaddingInternal = base.DataGridView.AlternatingRowsDefaultCellStyle.Padding;
			}
			else if (dataGridViewCellStyle4 != null && dataGridViewCellStyle4.Padding != Padding.Empty)
			{
				dataGridViewCellStyle.PaddingInternal = dataGridViewCellStyle4.Padding;
			}
			else
			{
				dataGridViewCellStyle.PaddingInternal = defaultCellStyle.Padding;
			}
			return dataGridViewCellStyle;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x0008C2FA File Offset: 0x0008A4FA
		internal DataGridViewCellStyle GetInheritedStyleInternal(int rowIndex)
		{
			return this.GetInheritedStyle(null, rowIndex, true);
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0008C308 File Offset: 0x0008A508
		internal int GetPreferredHeight(int rowIndex, int width)
		{
			if (base.DataGridView == null)
			{
				return -1;
			}
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			int height;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				height = this.GetPreferredSize(graphics, inheritedStyle, rowIndex, new Size(width, 0)).Height;
			}
			return height;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0008C368 File Offset: 0x0008A568
		internal Size GetPreferredSize(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return new Size(-1, -1);
			}
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			Size preferredSize;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				preferredSize = this.GetPreferredSize(graphics, inheritedStyle, rowIndex, Size.Empty);
			}
			return preferredSize;
		}

		/// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		// Token: 0x06001D9D RID: 7581 RVA: 0x0008C3C4 File Offset: 0x0008A5C4
		protected virtual Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			return new Size(-1, -1);
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x0008C3D0 File Offset: 0x0008A5D0
		internal static int GetPreferredTextHeight(Graphics g, bool rightToLeft, string text, DataGridViewCellStyle cellStyle, int maxWidth, out bool widthTruncated)
		{
			TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(rightToLeft, cellStyle.Alignment, cellStyle.WrapMode);
			if (cellStyle.WrapMode == DataGridViewTriState.True)
			{
				return DataGridViewCell.MeasureTextHeight(g, text, cellStyle.Font, maxWidth, textFormatFlags, out widthTruncated);
			}
			Size size = DataGridViewCell.MeasureTextSize(g, text, cellStyle.Font, textFormatFlags);
			widthTruncated = size.Width > maxWidth;
			return size.Height;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0008C430 File Offset: 0x0008A630
		internal int GetPreferredWidth(int rowIndex, int height)
		{
			if (base.DataGridView == null)
			{
				return -1;
			}
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			int width;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				width = this.GetPreferredSize(graphics, inheritedStyle, rowIndex, new Size(0, height)).Width;
			}
			return width;
		}

		/// <summary>Gets the size of the cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the cell's dimensions.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="rowIndex" /> is -1</exception>
		// Token: 0x06001DA0 RID: 7584 RVA: 0x0008C490 File Offset: 0x0008A690
		protected virtual Size GetSize(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return new Size(-1, -1);
			}
			if (rowIndex == -1)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedCell", new object[] { "Size" }));
			}
			return new Size(this.owningColumn.Thickness, this.owningRow.GetHeight(rowIndex));
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0008C4EC File Offset: 0x0008A6EC
		private string GetToolTipText(int rowIndex)
		{
			string text = this.ToolTipTextInternal;
			if (base.DataGridView != null && (base.DataGridView.VirtualMode || base.DataGridView.DataSource != null))
			{
				text = base.DataGridView.OnCellToolTipTextNeeded(this.ColumnIndex, rowIndex, text);
			}
			return text;
		}

		/// <summary>Gets the value of the cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>The value contained in the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell is not <see langword="null" /> and <paramref name="rowIndex" /> is less than 0 or greater than or equal to the number of rows in the parent <see cref="T:System.Windows.Forms.DataGridView" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell is not <see langword="null" /> and the value of the <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> property is less than 0, indicating that the cell is a row header cell.</exception>
		// Token: 0x06001DA2 RID: 7586 RVA: 0x0008C538 File Offset: 0x0008A738
		protected virtual object GetValue(int rowIndex)
		{
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView != null)
			{
				if (rowIndex < 0 || rowIndex >= dataGridView.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				if (this.ColumnIndex < 0)
				{
					throw new InvalidOperationException();
				}
			}
			if (dataGridView == null || (dataGridView.AllowUserToAddRowsInternal && rowIndex > -1 && rowIndex == dataGridView.NewRowIndex && rowIndex != dataGridView.CurrentCellAddress.Y) || (!dataGridView.VirtualMode && this.OwningColumn != null && !this.OwningColumn.IsDataBound) || rowIndex == -1 || this.ColumnIndex == -1)
			{
				return this.Properties.GetObject(DataGridViewCell.PropCellValue);
			}
			if (this.OwningColumn == null || !this.OwningColumn.IsDataBound)
			{
				return dataGridView.OnCellValueNeeded(this.ColumnIndex, rowIndex);
			}
			DataGridView.DataGridViewDataConnection dataConnection = dataGridView.DataConnection;
			if (dataConnection == null)
			{
				return null;
			}
			if (dataConnection.CurrencyManager.Count <= rowIndex)
			{
				return this.Properties.GetObject(DataGridViewCell.PropCellValue);
			}
			return dataConnection.GetValue(this.OwningColumn.BoundColumnIndex, this.ColumnIndex, rowIndex);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x0008C645 File Offset: 0x0008A845
		internal object GetValueInternal(int rowIndex)
		{
			return this.GetValue(rowIndex);
		}

		/// <summary>Initializes the control used to edit the cell.</summary>
		/// <param name="rowIndex">The zero-based row index of the cell's location.</param>
		/// <param name="initialFormattedValue">An <see cref="T:System.Object" /> that represents the value displayed by the cell when editing is started.</param>
		/// <param name="dataGridViewCellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <exception cref="T:System.InvalidOperationException">There is no associated <see cref="T:System.Windows.Forms.DataGridView" /> or if one is present, it does not have an associated editing control.</exception>
		// Token: 0x06001DA4 RID: 7588 RVA: 0x0008C650 File Offset: 0x0008A850
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView == null || dataGridView.EditingControl == null)
			{
				throw new InvalidOperationException();
			}
			if (dataGridView.EditingControl.ParentInternal == null)
			{
				dataGridView.EditingControl.CausesValidation = dataGridView.CausesValidation;
				dataGridView.EditingPanel.CausesValidation = dataGridView.CausesValidation;
				dataGridView.EditingControl.Visible = true;
				dataGridView.EditingPanel.Visible = false;
				dataGridView.Controls.Add(dataGridView.EditingPanel);
				dataGridView.EditingPanel.Controls.Add(dataGridView.EditingControl);
			}
			if (AccessibilityImprovements.Level3 && this.AccessibleRestructuringNeeded)
			{
				dataGridView.EditingControlAccessibleObject.SetParent(this.AccessibilityObject);
				this.AccessibilityObject.SetDetachableChild(dataGridView.EditingControl.AccessibilityObject);
				this.AccessibilityObject.RaiseStructureChangedEvent(UnsafeNativeMethods.StructureChangeType.ChildAdded, dataGridView.EditingControlAccessibleObject.RuntimeId);
			}
		}

		/// <summary>Indicates whether the parent row is unshared if the user presses a key while the focus is on the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001DA5 RID: 7589 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return false;
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x0008C731 File Offset: 0x0008A931
		internal bool KeyDownUnsharesRowInternal(KeyEventArgs e, int rowIndex)
		{
			return this.KeyDownUnsharesRow(e, rowIndex);
		}

		/// <summary>Determines if edit mode should be started based on the given key.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that represents the key that was pressed.</param>
		/// <returns>
		///   <see langword="true" /> if edit mode should be started; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x06001DA7 RID: 7591 RVA: 0x0001180C File Offset: 0x0000FA0C
		public virtual bool KeyEntersEditMode(KeyEventArgs e)
		{
			return false;
		}

		/// <summary>Indicates whether a row will be unshared if a key is pressed while a cell in the row has focus.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001DA8 RID: 7592 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool KeyPressUnsharesRow(KeyPressEventArgs e, int rowIndex)
		{
			return false;
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x0008C73B File Offset: 0x0008A93B
		internal bool KeyPressUnsharesRowInternal(KeyPressEventArgs e, int rowIndex)
		{
			return this.KeyPressUnsharesRow(e, rowIndex);
		}

		/// <summary>Indicates whether the parent row is unshared when the user releases a key while the focus is on the cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001DAA RID: 7594 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return false;
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x0008C745 File Offset: 0x0008A945
		internal bool KeyUpUnsharesRowInternal(KeyEventArgs e, int rowIndex)
		{
			return this.KeyUpUnsharesRow(e, rowIndex);
		}

		/// <summary>Indicates whether a row will be unshared when the focus leaves a cell in the row.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="throughMouseClick">
		///   <see langword="true" /> if a user action moved focus to the cell; <see langword="false" /> if a programmatic operation moved focus to the cell.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001DAC RID: 7596 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool LeaveUnsharesRow(int rowIndex, bool throughMouseClick)
		{
			return false;
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0008C74F File Offset: 0x0008A94F
		internal bool LeaveUnsharesRowInternal(int rowIndex, bool throughMouseClick)
		{
			return this.LeaveUnsharesRow(rowIndex, throughMouseClick);
		}

		/// <summary>Gets the height, in pixels, of the specified text, given the specified characteristics.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
		/// <param name="maxWidth">The maximum width of the text.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values to apply to the text.</param>
		/// <returns>The height, in pixels, of the text.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="font" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxWidth" /> is less than 1.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</exception>
		// Token: 0x06001DAE RID: 7598 RVA: 0x0008C75C File Offset: 0x0008A95C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static int MeasureTextHeight(Graphics graphics, string text, Font font, int maxWidth, TextFormatFlags flags)
		{
			bool flag;
			return DataGridViewCell.MeasureTextHeight(graphics, text, font, maxWidth, flags, out flag);
		}

		/// <summary>Gets the height, in pixels, of the specified text, given the specified characteristics. Also indicates whether the required width is greater than the specified maximum width.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
		/// <param name="maxWidth">The maximum width of the text.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values to apply to the text.</param>
		/// <param name="widthTruncated">Set to <see langword="true" /> if the required width of the text is greater than <paramref name="maxWidth" />.</param>
		/// <returns>The height, in pixels, of the text.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="font" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxWidth" /> is less than 1.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</exception>
		// Token: 0x06001DAF RID: 7599 RVA: 0x0008C778 File Offset: 0x0008A978
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static int MeasureTextHeight(Graphics graphics, string text, Font font, int maxWidth, TextFormatFlags flags, out bool widthTruncated)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			if (maxWidth <= 0)
			{
				throw new ArgumentOutOfRangeException("maxWidth", SR.GetString("InvalidLowBoundArgument", new object[]
				{
					"maxWidth",
					maxWidth.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (!DataGridViewUtilities.ValidTextFormatFlags(flags))
			{
				throw new InvalidEnumArgumentException("flags", (int)flags, typeof(TextFormatFlags));
			}
			flags &= TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak;
			Size size = TextRenderer.MeasureText(text, font, new Size(maxWidth, int.MaxValue), flags);
			widthTruncated = size.Width > maxWidth;
			return size.Height;
		}

		/// <summary>Gets the ideal height and width of the specified text given the specified characteristics.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
		/// <param name="maxRatio">The maximum width-to-height ratio of the block of text.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values to apply to the text.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the preferred height and width of the text.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="font" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxRatio" /> is less than or equal to 0.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</exception>
		// Token: 0x06001DB0 RID: 7600 RVA: 0x0008C83C File Offset: 0x0008AA3C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Size MeasureTextPreferredSize(Graphics graphics, string text, Font font, float maxRatio, TextFormatFlags flags)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			if (maxRatio <= 0f)
			{
				throw new ArgumentOutOfRangeException("maxRatio", SR.GetString("InvalidLowBoundArgument", new object[]
				{
					"maxRatio",
					maxRatio.ToString(CultureInfo.CurrentCulture),
					"0.0"
				}));
			}
			if (!DataGridViewUtilities.ValidTextFormatFlags(flags))
			{
				throw new InvalidEnumArgumentException("flags", (int)flags, typeof(TextFormatFlags));
			}
			if (string.IsNullOrEmpty(text))
			{
				return new Size(0, 0);
			}
			Size size = DataGridViewCell.MeasureTextSize(graphics, text, font, flags);
			if ((float)(size.Width / size.Height) <= maxRatio)
			{
				return size;
			}
			flags &= TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak;
			float num = (float)(size.Width * size.Width) / (float)size.Height / maxRatio * 1.1f;
			Size size2;
			for (;;)
			{
				size2 = TextRenderer.MeasureText(text, font, new Size((int)num, int.MaxValue), flags);
				if ((float)(size2.Width / size2.Height) <= maxRatio || size2.Width > (int)num)
				{
					break;
				}
				num = (float)size2.Width * 0.9f;
				if (num <= 1f)
				{
					return size2;
				}
			}
			return size2;
		}

		/// <summary>Gets the height and width of the specified text given the specified characteristics.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values to apply to the text.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the height and width of the text.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="font" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</exception>
		// Token: 0x06001DB1 RID: 7601 RVA: 0x0008C974 File Offset: 0x0008AB74
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Size MeasureTextSize(Graphics graphics, string text, Font font, TextFormatFlags flags)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			if (!DataGridViewUtilities.ValidTextFormatFlags(flags))
			{
				throw new InvalidEnumArgumentException("flags", (int)flags, typeof(TextFormatFlags));
			}
			flags &= TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak;
			return TextRenderer.MeasureText(text, font, new Size(int.MaxValue, int.MaxValue), flags);
		}

		/// <summary>Gets the width, in pixels, of the specified text given the specified characteristics.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
		/// <param name="maxHeight">The maximum height of the text.</param>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values to apply to the text.</param>
		/// <returns>The width, in pixels, of the text.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="font" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxHeight" /> is less than 1.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</exception>
		// Token: 0x06001DB2 RID: 7602 RVA: 0x0008C9DC File Offset: 0x0008ABDC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static int MeasureTextWidth(Graphics graphics, string text, Font font, int maxHeight, TextFormatFlags flags)
		{
			if (maxHeight <= 0)
			{
				throw new ArgumentOutOfRangeException("maxHeight", SR.GetString("InvalidLowBoundArgument", new object[]
				{
					"maxHeight",
					maxHeight.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			Size size = DataGridViewCell.MeasureTextSize(graphics, text, font, flags);
			if (size.Height >= maxHeight || (flags & TextFormatFlags.SingleLine) != TextFormatFlags.Default)
			{
				return size.Width;
			}
			flags &= TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak;
			int num = size.Width;
			float num2 = (float)num * 0.9f;
			for (;;)
			{
				Size size2 = TextRenderer.MeasureText(text, font, new Size((int)num2, maxHeight), flags);
				if (size2.Height > maxHeight || size2.Width > (int)num2)
				{
					break;
				}
				num = (int)num2;
				num2 = (float)size2.Width * 0.9f;
				if (num2 <= 1f)
				{
					return num;
				}
			}
			return num;
		}

		/// <summary>Indicates whether a row will be unshared if the user clicks a mouse button while the pointer is on a cell in the row.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001DB3 RID: 7603 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool MouseClickUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0008CAB4 File Offset: 0x0008ACB4
		internal bool MouseClickUnsharesRowInternal(DataGridViewCellMouseEventArgs e)
		{
			return this.MouseClickUnsharesRow(e);
		}

		/// <summary>Indicates whether a row will be unshared if the user double-clicks a cell in the row.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001DB5 RID: 7605 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool MouseDoubleClickUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x0008CABD File Offset: 0x0008ACBD
		internal bool MouseDoubleClickUnsharesRowInternal(DataGridViewCellMouseEventArgs e)
		{
			return this.MouseDoubleClickUnsharesRow(e);
		}

		/// <summary>Indicates whether a row will be unshared when the user holds down a mouse button while the pointer is on a cell in the row.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001DB7 RID: 7607 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0008CAC6 File Offset: 0x0008ACC6
		internal bool MouseDownUnsharesRowInternal(DataGridViewCellMouseEventArgs e)
		{
			return this.MouseDownUnsharesRow(e);
		}

		/// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001DB9 RID: 7609 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool MouseEnterUnsharesRow(int rowIndex)
		{
			return false;
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0008CACF File Offset: 0x0008ACCF
		internal bool MouseEnterUnsharesRowInternal(int rowIndex)
		{
			return this.MouseEnterUnsharesRow(rowIndex);
		}

		/// <summary>Indicates whether a row will be unshared when the mouse pointer leaves the row.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001DBB RID: 7611 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool MouseLeaveUnsharesRow(int rowIndex)
		{
			return false;
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0008CAD8 File Offset: 0x0008ACD8
		internal bool MouseLeaveUnsharesRowInternal(int rowIndex)
		{
			return this.MouseLeaveUnsharesRow(rowIndex);
		}

		/// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001DBD RID: 7613 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool MouseMoveUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0008CAE1 File Offset: 0x0008ACE1
		internal bool MouseMoveUnsharesRowInternal(DataGridViewCellMouseEventArgs e)
		{
			return this.MouseMoveUnsharesRow(e);
		}

		/// <summary>Indicates whether a row will be unshared when the user releases a mouse button while the pointer is on a cell in the row.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		/// <returns>
		///   <see langword="true" /> if the row will be unshared, otherwise, <see langword="false" />. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns <see langword="false" />.</returns>
		// Token: 0x06001DBF RID: 7615 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x0008CAEA File Offset: 0x0008ACEA
		internal bool MouseUpUnsharesRowInternal(DataGridViewCellMouseEventArgs e)
		{
			return this.MouseUpUnsharesRow(e);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0008CAF4 File Offset: 0x0008ACF4
		private void OnCellDataAreaMouseEnterInternal(int rowIndex)
		{
			if (!base.DataGridView.ShowCellToolTips)
			{
				return;
			}
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			if (currentCellAddress.X != -1 && currentCellAddress.X == this.ColumnIndex && currentCellAddress.Y == rowIndex && base.DataGridView.EditingControl != null)
			{
				return;
			}
			string text = this.GetToolTipText(rowIndex);
			if (string.IsNullOrEmpty(text))
			{
				if (!(this.FormattedValueType == DataGridViewCell.stringType))
				{
					goto IL_1E5;
				}
				if (rowIndex != -1 && this.OwningColumn != null)
				{
					int preferredWidth = this.GetPreferredWidth(rowIndex, this.OwningRow.Height);
					int preferredHeight = this.GetPreferredHeight(rowIndex, this.OwningColumn.Width);
					if (this.OwningColumn.Width >= preferredWidth && this.OwningRow.Height >= preferredHeight)
					{
						goto IL_1E5;
					}
					DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
					string text2 = this.GetEditedFormattedValue(this.GetValue(rowIndex), rowIndex, ref inheritedStyle, DataGridViewDataErrorContexts.Display) as string;
					if (!string.IsNullOrEmpty(text2))
					{
						text = DataGridViewCell.TruncateToolTipText(text2);
						goto IL_1E5;
					}
					goto IL_1E5;
				}
				else
				{
					if ((rowIndex == -1 || this.OwningRow == null || !base.DataGridView.RowHeadersVisible || base.DataGridView.RowHeadersWidth <= 0 || this.OwningColumn != null) && rowIndex != -1)
					{
						goto IL_1E5;
					}
					string text3 = this.GetValue(rowIndex) as string;
					if (string.IsNullOrEmpty(text3))
					{
						goto IL_1E5;
					}
					DataGridViewCellStyle inheritedStyle2 = this.GetInheritedStyle(null, rowIndex, false);
					using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
					{
						Rectangle contentBounds = this.GetContentBounds(graphics, inheritedStyle2, rowIndex);
						bool flag = false;
						int num = 0;
						if (contentBounds.Width > 0)
						{
							num = DataGridViewCell.GetPreferredTextHeight(graphics, base.DataGridView.RightToLeftInternal, text3, inheritedStyle2, contentBounds.Width, out flag);
						}
						else
						{
							flag = true;
						}
						if (num > contentBounds.Height || flag)
						{
							text = DataGridViewCell.TruncateToolTipText(text3);
						}
						goto IL_1E5;
					}
				}
			}
			if (base.DataGridView.IsRestricted)
			{
				text = DataGridViewCell.TruncateToolTipText(text);
			}
			IL_1E5:
			if (!string.IsNullOrEmpty(text))
			{
				base.DataGridView.ActivateToolTip(true, text, this.ColumnIndex, rowIndex);
			}
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0008CD14 File Offset: 0x0008AF14
		private void OnCellDataAreaMouseLeaveInternal()
		{
			if (base.DataGridView.IsDisposed)
			{
				return;
			}
			base.DataGridView.ActivateToolTip(false, string.Empty, -1, -1);
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0008CD38 File Offset: 0x0008AF38
		private void OnCellErrorAreaMouseEnterInternal(int rowIndex)
		{
			string errorText = this.GetErrorText(rowIndex);
			base.DataGridView.ActivateToolTip(true, errorText, this.ColumnIndex, rowIndex);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x0008CD61 File Offset: 0x0008AF61
		private void OnCellErrorAreaMouseLeaveInternal()
		{
			base.DataGridView.ActivateToolTip(false, string.Empty, -1, -1);
		}

		/// <summary>Called when the cell is clicked.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data.</param>
		// Token: 0x06001DC5 RID: 7621 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnClick(DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x0008CD76 File Offset: 0x0008AF76
		internal void OnClickInternal(DataGridViewCellEventArgs e)
		{
			this.OnClick(e);
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x0008CD80 File Offset: 0x0008AF80
		internal void OnCommonChange()
		{
			if (base.DataGridView != null && !base.DataGridView.IsDisposed && !base.DataGridView.Disposing)
			{
				if (this.RowIndex == -1)
				{
					base.DataGridView.OnColumnCommonChange(this.ColumnIndex);
					return;
				}
				base.DataGridView.OnCellCommonChange(this.ColumnIndex, this.RowIndex);
			}
		}

		/// <summary>Called when the cell's contents are clicked.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data.</param>
		// Token: 0x06001DC8 RID: 7624 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnContentClick(DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x0008CDE1 File Offset: 0x0008AFE1
		internal void OnContentClickInternal(DataGridViewCellEventArgs e)
		{
			this.OnContentClick(e);
		}

		/// <summary>Called when the cell's contents are double-clicked.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data.</param>
		// Token: 0x06001DCA RID: 7626 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnContentDoubleClick(DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x0008CDEA File Offset: 0x0008AFEA
		internal void OnContentDoubleClickInternal(DataGridViewCellEventArgs e)
		{
			this.OnContentDoubleClick(e);
		}

		/// <summary>Called when the cell is double-clicked.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data.</param>
		// Token: 0x06001DCC RID: 7628 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnDoubleClick(DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x0008CDF3 File Offset: 0x0008AFF3
		internal void OnDoubleClickInternal(DataGridViewCellEventArgs e)
		{
			this.OnDoubleClick(e);
		}

		/// <summary>Called when the focus moves to a cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="throughMouseClick">
		///   <see langword="true" /> if a user action moved focus to the cell; <see langword="false" /> if a programmatic operation moved focus to the cell.</param>
		// Token: 0x06001DCE RID: 7630 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnEnter(int rowIndex, bool throughMouseClick)
		{
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x0008CDFC File Offset: 0x0008AFFC
		internal void OnEnterInternal(int rowIndex, bool throughMouseClick)
		{
			this.OnEnter(rowIndex, throughMouseClick);
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0008CE06 File Offset: 0x0008B006
		internal void OnKeyDownInternal(KeyEventArgs e, int rowIndex)
		{
			this.OnKeyDown(e, rowIndex);
		}

		/// <summary>Called when a character key is pressed while the focus is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x06001DD1 RID: 7633 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnKeyDown(KeyEventArgs e, int rowIndex)
		{
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0008CE10 File Offset: 0x0008B010
		internal void OnKeyPressInternal(KeyPressEventArgs e, int rowIndex)
		{
			this.OnKeyPress(e, rowIndex);
		}

		/// <summary>Called when a key is pressed while the focus is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x06001DD3 RID: 7635 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnKeyPress(KeyPressEventArgs e, int rowIndex)
		{
		}

		/// <summary>Called when a character key is released while the focus is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x06001DD4 RID: 7636 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnKeyUp(KeyEventArgs e, int rowIndex)
		{
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x0008CE1A File Offset: 0x0008B01A
		internal void OnKeyUpInternal(KeyEventArgs e, int rowIndex)
		{
			this.OnKeyUp(e, rowIndex);
		}

		/// <summary>Called when the focus moves from a cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="throughMouseClick">
		///   <see langword="true" /> if a user action moved focus from the cell; <see langword="false" /> if a programmatic operation moved focus from the cell.</param>
		// Token: 0x06001DD6 RID: 7638 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnLeave(int rowIndex, bool throughMouseClick)
		{
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0008CE24 File Offset: 0x0008B024
		internal void OnLeaveInternal(int rowIndex, bool throughMouseClick)
		{
			this.OnLeave(rowIndex, throughMouseClick);
		}

		/// <summary>Called when the user clicks a mouse button while the pointer is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001DD8 RID: 7640 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnMouseClick(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x0008CE2E File Offset: 0x0008B02E
		internal void OnMouseClickInternal(DataGridViewCellMouseEventArgs e)
		{
			this.OnMouseClick(e);
		}

		/// <summary>Called when the user double-clicks a mouse button while the pointer is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001DDA RID: 7642 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnMouseDoubleClick(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x0008CE37 File Offset: 0x0008B037
		internal void OnMouseDoubleClickInternal(DataGridViewCellMouseEventArgs e)
		{
			this.OnMouseDoubleClick(e);
		}

		/// <summary>Called when the user holds down a mouse button while the pointer is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001DDC RID: 7644 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x0008CE40 File Offset: 0x0008B040
		internal void OnMouseDownInternal(DataGridViewCellMouseEventArgs e)
		{
			base.DataGridView.CellMouseDownInContentBounds = this.GetContentBounds(e.RowIndex).Contains(e.X, e.Y);
			if (((this.ColumnIndex < 0 || e.RowIndex < 0) && base.DataGridView.ApplyVisualStylesToHeaderCells) || (this.ColumnIndex >= 0 && e.RowIndex >= 0 && base.DataGridView.ApplyVisualStylesToInnerCells))
			{
				base.DataGridView.InvalidateCell(this.ColumnIndex, e.RowIndex);
			}
			this.OnMouseDown(e);
		}

		/// <summary>Called when the mouse pointer moves over a cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x06001DDE RID: 7646 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnMouseEnter(int rowIndex)
		{
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x0008CED4 File Offset: 0x0008B0D4
		internal void OnMouseEnterInternal(int rowIndex)
		{
			this.OnMouseEnter(rowIndex);
		}

		/// <summary>Called when the mouse pointer leaves the cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x06001DE0 RID: 7648 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnMouseLeave(int rowIndex)
		{
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x0008CEE0 File Offset: 0x0008B0E0
		internal void OnMouseLeaveInternal(int rowIndex)
		{
			switch (this.CurrentMouseLocation)
			{
			case 1:
				this.OnCellDataAreaMouseLeaveInternal();
				break;
			case 2:
				this.OnCellErrorAreaMouseLeaveInternal();
				break;
			}
			this.CurrentMouseLocation = 0;
			this.OnMouseLeave(rowIndex);
		}

		/// <summary>Called when the mouse pointer moves within a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001DE2 RID: 7650 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x0008CF24 File Offset: 0x0008B124
		internal void OnMouseMoveInternal(DataGridViewCellMouseEventArgs e)
		{
			byte currentMouseLocation = this.CurrentMouseLocation;
			this.UpdateCurrentMouseLocation(e);
			switch (currentMouseLocation)
			{
			case 0:
				if (this.CurrentMouseLocation == 1)
				{
					this.OnCellDataAreaMouseEnterInternal(e.RowIndex);
				}
				else
				{
					this.OnCellErrorAreaMouseEnterInternal(e.RowIndex);
				}
				break;
			case 1:
				if (this.CurrentMouseLocation == 2)
				{
					this.OnCellDataAreaMouseLeaveInternal();
					this.OnCellErrorAreaMouseEnterInternal(e.RowIndex);
				}
				break;
			case 2:
				if (this.CurrentMouseLocation == 1)
				{
					this.OnCellErrorAreaMouseLeaveInternal();
					this.OnCellDataAreaMouseEnterInternal(e.RowIndex);
				}
				break;
			}
			this.OnMouseMove(e);
		}

		/// <summary>Called when the user releases a mouse button while the pointer is on a cell.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001DE4 RID: 7652 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x0008CFB8 File Offset: 0x0008B1B8
		internal void OnMouseUpInternal(DataGridViewCellMouseEventArgs e)
		{
			int x = e.X;
			int y = e.Y;
			if (((this.ColumnIndex < 0 || e.RowIndex < 0) && base.DataGridView.ApplyVisualStylesToHeaderCells) || (this.ColumnIndex >= 0 && e.RowIndex >= 0 && base.DataGridView.ApplyVisualStylesToInnerCells))
			{
				base.DataGridView.InvalidateCell(this.ColumnIndex, e.RowIndex);
			}
			if (e.Button == MouseButtons.Left && this.GetContentBounds(e.RowIndex).Contains(x, y))
			{
				base.DataGridView.OnCommonCellContentClick(e.ColumnIndex, e.RowIndex, e.Clicks > 1);
			}
			if (base.DataGridView != null && e.ColumnIndex < base.DataGridView.Columns.Count && e.RowIndex < base.DataGridView.Rows.Count)
			{
				this.OnMouseUp(e);
			}
		}

		/// <summary>Called when the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell changes.</summary>
		// Token: 0x06001DE6 RID: 7654 RVA: 0x0008D0AC File Offset: 0x0008B2AC
		protected override void OnDataGridViewChanged()
		{
			if (this.HasStyle)
			{
				if (base.DataGridView == null)
				{
					this.Style.RemoveScope(DataGridViewCellStyleScopes.Cell);
				}
				else
				{
					this.Style.AddScope(base.DataGridView, DataGridViewCellStyleScopes.Cell);
				}
			}
			base.OnDataGridViewChanged();
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
		// Token: 0x06001DE7 RID: 7655 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x0008D0E4 File Offset: 0x0008B2E4
		internal void PaintInternal(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			this.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x0008D10A File Offset: 0x0008B30A
		internal static bool PaintBackground(DataGridViewPaintParts paintParts)
		{
			return (paintParts & DataGridViewPaintParts.Background) > DataGridViewPaintParts.None;
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0008D112 File Offset: 0x0008B312
		internal static bool PaintBorder(DataGridViewPaintParts paintParts)
		{
			return (paintParts & DataGridViewPaintParts.Border) > DataGridViewPaintParts.None;
		}

		/// <summary>Paints the border of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the border.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the area of the border that is being painted.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the current cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles of the border that is being painted.</param>
		// Token: 0x06001DEB RID: 7659 RVA: 0x0008D11C File Offset: 0x0008B31C
		protected virtual void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle bounds, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (base.DataGridView == null)
			{
				return;
			}
			Pen pen = null;
			Pen pen2 = null;
			Pen cachedPen = base.DataGridView.GetCachedPen(cellStyle.BackColor);
			Pen gridPen = base.DataGridView.GridPen;
			this.GetContrastedPens(cellStyle.BackColor, ref pen, ref pen2);
			int num = ((this.owningColumn == null) ? 0 : this.owningColumn.DividerWidth);
			if (num != 0)
			{
				if (num > bounds.Width)
				{
					num = bounds.Width;
				}
				DataGridViewAdvancedCellBorderStyle right = advancedBorderStyle.Right;
				Color color;
				if (right != DataGridViewAdvancedCellBorderStyle.Single)
				{
					if (right != DataGridViewAdvancedCellBorderStyle.Inset)
					{
						color = SystemColors.ControlDark;
					}
					else
					{
						color = SystemColors.ControlLightLight;
					}
				}
				else
				{
					color = base.DataGridView.GridPen.Color;
				}
				graphics.FillRectangle(base.DataGridView.GetCachedBrush(color), base.DataGridView.RightToLeftInternal ? bounds.X : (bounds.Right - num), bounds.Y, num, bounds.Height);
				if (base.DataGridView.RightToLeftInternal)
				{
					bounds.X += num;
				}
				bounds.Width -= num;
				if (bounds.Width <= 0)
				{
					return;
				}
			}
			num = ((this.owningRow == null) ? 0 : this.owningRow.DividerHeight);
			if (num != 0)
			{
				if (num > bounds.Height)
				{
					num = bounds.Height;
				}
				DataGridViewAdvancedCellBorderStyle bottom = advancedBorderStyle.Bottom;
				Color color2;
				if (bottom != DataGridViewAdvancedCellBorderStyle.Single)
				{
					if (bottom != DataGridViewAdvancedCellBorderStyle.Inset)
					{
						color2 = SystemColors.ControlDark;
					}
					else
					{
						color2 = SystemColors.ControlLightLight;
					}
				}
				else
				{
					color2 = base.DataGridView.GridPen.Color;
				}
				graphics.FillRectangle(base.DataGridView.GetCachedBrush(color2), bounds.X, bounds.Bottom - num, bounds.Width, num);
				bounds.Height -= num;
				if (bounds.Height <= 0)
				{
					return;
				}
			}
			if (advancedBorderStyle.All == DataGridViewAdvancedCellBorderStyle.None)
			{
				return;
			}
			switch (advancedBorderStyle.Left)
			{
			case DataGridViewAdvancedCellBorderStyle.Single:
				graphics.DrawLine(gridPen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
				break;
			case DataGridViewAdvancedCellBorderStyle.Inset:
				graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
				break;
			case DataGridViewAdvancedCellBorderStyle.InsetDouble:
			{
				int num2 = bounds.Y + 1;
				int num3 = bounds.Bottom - 1;
				if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.OutsetPartial || advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.None)
				{
					num2--;
				}
				if (advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.OutsetPartial)
				{
					num3++;
				}
				graphics.DrawLine(pen2, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
				graphics.DrawLine(pen, bounds.X + 1, num2, bounds.X + 1, num3);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.Outset:
				graphics.DrawLine(pen2, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
				break;
			case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
			{
				int num2 = bounds.Y + 1;
				int num3 = bounds.Bottom - 1;
				if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.OutsetPartial || advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.None)
				{
					num2--;
				}
				if (advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.OutsetPartial)
				{
					num3++;
				}
				graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
				graphics.DrawLine(pen2, bounds.X + 1, num2, bounds.X + 1, num3);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.OutsetPartial:
			{
				int num2 = bounds.Y + 2;
				int num3 = bounds.Bottom - 3;
				if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.InsetDouble)
				{
					num2++;
				}
				else if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.None)
				{
					num2--;
				}
				graphics.DrawLine(cachedPen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
				graphics.DrawLine(pen2, bounds.X, num2, bounds.X, num3);
				break;
			}
			}
			switch (advancedBorderStyle.Right)
			{
			case DataGridViewAdvancedCellBorderStyle.Single:
				graphics.DrawLine(gridPen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 1);
				break;
			case DataGridViewAdvancedCellBorderStyle.Inset:
				graphics.DrawLine(pen2, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 1);
				break;
			case DataGridViewAdvancedCellBorderStyle.InsetDouble:
			{
				int num2 = bounds.Y + 1;
				int num3 = bounds.Bottom - 1;
				if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.OutsetPartial || advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.None)
				{
					num2--;
				}
				if (advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.OutsetPartial || advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.Inset)
				{
					num3++;
				}
				graphics.DrawLine(pen2, bounds.Right - 2, bounds.Y, bounds.Right - 2, bounds.Bottom - 1);
				graphics.DrawLine(pen, bounds.Right - 1, num2, bounds.Right - 1, num3);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.Outset:
				graphics.DrawLine(pen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 1);
				break;
			case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
			{
				int num2 = bounds.Y + 1;
				int num3 = bounds.Bottom - 1;
				if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.OutsetPartial || advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.None)
				{
					num2--;
				}
				if (advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.OutsetPartial)
				{
					num3++;
				}
				graphics.DrawLine(pen, bounds.Right - 2, bounds.Y, bounds.Right - 2, bounds.Bottom - 1);
				graphics.DrawLine(pen2, bounds.Right - 1, num2, bounds.Right - 1, num3);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.OutsetPartial:
			{
				int num2 = bounds.Y + 2;
				int num3 = bounds.Bottom - 3;
				if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.InsetDouble)
				{
					num2++;
				}
				else if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.None)
				{
					num2--;
				}
				graphics.DrawLine(cachedPen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 1);
				graphics.DrawLine(pen, bounds.Right - 1, num2, bounds.Right - 1, num3);
				break;
			}
			}
			switch (advancedBorderStyle.Top)
			{
			case DataGridViewAdvancedCellBorderStyle.Single:
				graphics.DrawLine(gridPen, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
				break;
			case DataGridViewAdvancedCellBorderStyle.Inset:
			{
				int num4 = bounds.X;
				int num5 = bounds.Right - 1;
				if (advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.InsetDouble)
				{
					num4++;
				}
				if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.Inset || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.Outset)
				{
					num5--;
				}
				graphics.DrawLine(pen, num4, bounds.Y, num5, bounds.Y);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.InsetDouble:
			{
				int num4 = bounds.X;
				if (advancedBorderStyle.Left != DataGridViewAdvancedCellBorderStyle.OutsetPartial && advancedBorderStyle.Left != DataGridViewAdvancedCellBorderStyle.None)
				{
					num4++;
				}
				int num5 = bounds.Right - 2;
				if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.OutsetPartial || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.None)
				{
					num5++;
				}
				graphics.DrawLine(pen2, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
				graphics.DrawLine(pen, num4, bounds.Y + 1, num5, bounds.Y + 1);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.Outset:
			{
				int num4 = bounds.X;
				int num5 = bounds.Right - 1;
				if (advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.InsetDouble)
				{
					num4++;
				}
				if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.Inset || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.Outset)
				{
					num5--;
				}
				graphics.DrawLine(pen2, num4, bounds.Y, num5, bounds.Y);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
			{
				int num4 = bounds.X;
				if (advancedBorderStyle.Left != DataGridViewAdvancedCellBorderStyle.OutsetPartial && advancedBorderStyle.Left != DataGridViewAdvancedCellBorderStyle.None)
				{
					num4++;
				}
				int num5 = bounds.Right - 2;
				if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.OutsetPartial || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.None)
				{
					num5++;
				}
				graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
				graphics.DrawLine(pen2, num4, bounds.Y + 1, num5, bounds.Y + 1);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.OutsetPartial:
			{
				int num4 = bounds.X;
				int num5 = bounds.Right - 1;
				if (advancedBorderStyle.Left != DataGridViewAdvancedCellBorderStyle.None)
				{
					num4++;
					if (advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.InsetDouble)
					{
						num4++;
					}
				}
				if (advancedBorderStyle.Right != DataGridViewAdvancedCellBorderStyle.None)
				{
					num5--;
					if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.InsetDouble)
					{
						num5--;
					}
				}
				graphics.DrawLine(cachedPen, num4, bounds.Y, num5, bounds.Y);
				graphics.DrawLine(pen2, num4 + 1, bounds.Y, num5 - 1, bounds.Y);
				break;
			}
			}
			switch (advancedBorderStyle.Bottom)
			{
			case DataGridViewAdvancedCellBorderStyle.Single:
				graphics.DrawLine(gridPen, bounds.X, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
				return;
			case DataGridViewAdvancedCellBorderStyle.Inset:
			{
				int num5 = bounds.Right - 1;
				if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.InsetDouble)
				{
					num5--;
				}
				graphics.DrawLine(pen2, bounds.X, bounds.Bottom - 1, num5, bounds.Bottom - 1);
				return;
			}
			case DataGridViewAdvancedCellBorderStyle.InsetDouble:
			case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
				break;
			case DataGridViewAdvancedCellBorderStyle.Outset:
			{
				int num4 = bounds.X;
				int num5 = bounds.Right - 1;
				if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.InsetDouble || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.OutsetDouble)
				{
					num5--;
				}
				graphics.DrawLine(pen, num4, bounds.Bottom - 1, num5, bounds.Bottom - 1);
				return;
			}
			case DataGridViewAdvancedCellBorderStyle.OutsetPartial:
			{
				int num4 = bounds.X;
				int num5 = bounds.Right - 1;
				if (advancedBorderStyle.Left != DataGridViewAdvancedCellBorderStyle.None)
				{
					num4++;
					if (advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.InsetDouble)
					{
						num4++;
					}
				}
				if (advancedBorderStyle.Right != DataGridViewAdvancedCellBorderStyle.None)
				{
					num5--;
					if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.InsetDouble)
					{
						num5--;
					}
				}
				graphics.DrawLine(cachedPen, num4, bounds.Bottom - 1, num5, bounds.Bottom - 1);
				graphics.DrawLine(pen, num4 + 1, bounds.Bottom - 1, num5 - 1, bounds.Bottom - 1);
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x0008DC02 File Offset: 0x0008BE02
		internal static bool PaintContentBackground(DataGridViewPaintParts paintParts)
		{
			return (paintParts & DataGridViewPaintParts.ContentBackground) > DataGridViewPaintParts.None;
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x0008DC0A File Offset: 0x0008BE0A
		internal static bool PaintContentForeground(DataGridViewPaintParts paintParts)
		{
			return (paintParts & DataGridViewPaintParts.ContentForeground) > DataGridViewPaintParts.None;
		}

		/// <summary>Paints the error icon of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the border.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="cellValueBounds">The bounding <see cref="T:System.Drawing.Rectangle" /> that encloses the cell's content area.</param>
		/// <param name="errorText">An error message that is associated with the cell.</param>
		// Token: 0x06001DEE RID: 7662 RVA: 0x0008DC12 File Offset: 0x0008BE12
		protected virtual void PaintErrorIcon(Graphics graphics, Rectangle clipBounds, Rectangle cellValueBounds, string errorText)
		{
			if (!string.IsNullOrEmpty(errorText) && cellValueBounds.Width >= (int)(8 + DataGridViewCell.iconsWidth) && cellValueBounds.Height >= (int)(8 + DataGridViewCell.iconsHeight))
			{
				DataGridViewCell.PaintErrorIcon(graphics, this.ComputeErrorIconBounds(cellValueBounds));
			}
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x0008DC4C File Offset: 0x0008BE4C
		private static void PaintErrorIcon(Graphics graphics, Rectangle iconBounds)
		{
			Bitmap errorBitmap = DataGridViewCell.ErrorBitmap;
			if (errorBitmap != null)
			{
				Bitmap bitmap = errorBitmap;
				lock (bitmap)
				{
					graphics.DrawImage(errorBitmap, iconBounds, 0, 0, (int)DataGridViewCell.iconsWidth, (int)DataGridViewCell.iconsHeight, GraphicsUnit.Pixel);
				}
			}
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x0008DCA0 File Offset: 0x0008BEA0
		internal void PaintErrorIcon(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Rectangle cellBounds, Rectangle cellValueBounds, string errorText)
		{
			if (!string.IsNullOrEmpty(errorText) && cellValueBounds.Width >= (int)(8 + DataGridViewCell.iconsWidth) && cellValueBounds.Height >= (int)(8 + DataGridViewCell.iconsHeight))
			{
				Rectangle errorIconBounds = this.GetErrorIconBounds(graphics, cellStyle, rowIndex);
				if (errorIconBounds.Width >= 4 && errorIconBounds.Height >= (int)DataGridViewCell.iconsHeight)
				{
					errorIconBounds.X += cellBounds.X;
					errorIconBounds.Y += cellBounds.Y;
					DataGridViewCell.PaintErrorIcon(graphics, errorIconBounds);
				}
			}
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x0008DD29 File Offset: 0x0008BF29
		internal static bool PaintErrorIcon(DataGridViewPaintParts paintParts)
		{
			return (paintParts & DataGridViewPaintParts.ErrorIcon) > DataGridViewPaintParts.None;
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x0008DD32 File Offset: 0x0008BF32
		internal static bool PaintFocus(DataGridViewPaintParts paintParts)
		{
			return (paintParts & DataGridViewPaintParts.Focus) > DataGridViewPaintParts.None;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x0008DD3C File Offset: 0x0008BF3C
		internal static void PaintPadding(Graphics graphics, Rectangle bounds, DataGridViewCellStyle cellStyle, Brush br, bool rightToLeft)
		{
			Rectangle rectangle;
			if (rightToLeft)
			{
				rectangle = new Rectangle(bounds.X, bounds.Y, cellStyle.Padding.Right, bounds.Height);
				graphics.FillRectangle(br, rectangle);
				rectangle.X = bounds.Right - cellStyle.Padding.Left;
				rectangle.Width = cellStyle.Padding.Left;
				graphics.FillRectangle(br, rectangle);
				rectangle.X = bounds.Left + cellStyle.Padding.Right;
			}
			else
			{
				rectangle = new Rectangle(bounds.X, bounds.Y, cellStyle.Padding.Left, bounds.Height);
				graphics.FillRectangle(br, rectangle);
				rectangle.X = bounds.Right - cellStyle.Padding.Right;
				rectangle.Width = cellStyle.Padding.Right;
				graphics.FillRectangle(br, rectangle);
				rectangle.X = bounds.Left + cellStyle.Padding.Left;
			}
			rectangle.Y = bounds.Y;
			rectangle.Width = bounds.Width - cellStyle.Padding.Horizontal;
			rectangle.Height = cellStyle.Padding.Top;
			graphics.FillRectangle(br, rectangle);
			rectangle.Y = bounds.Bottom - cellStyle.Padding.Bottom;
			rectangle.Height = cellStyle.Padding.Bottom;
			graphics.FillRectangle(br, rectangle);
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x0008DEE9 File Offset: 0x0008C0E9
		internal static bool PaintSelectionBackground(DataGridViewPaintParts paintParts)
		{
			return (paintParts & DataGridViewPaintParts.SelectionBackground) > DataGridViewPaintParts.None;
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x0008DEF4 File Offset: 0x0008C0F4
		internal void PaintWork(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			DataGridView dataGridView = base.DataGridView;
			int columnIndex = this.ColumnIndex;
			object value = this.GetValue(rowIndex);
			string errorText = this.GetErrorText(rowIndex);
			object obj;
			if (columnIndex > -1 && rowIndex > -1)
			{
				obj = this.GetEditedFormattedValue(value, rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.Display);
			}
			else
			{
				obj = value;
			}
			DataGridViewCellPaintingEventArgs cellPaintingEventArgs = dataGridView.CellPaintingEventArgs;
			cellPaintingEventArgs.SetProperties(graphics, clipBounds, cellBounds, rowIndex, columnIndex, cellState, value, obj, errorText, cellStyle, advancedBorderStyle, paintParts);
			dataGridView.OnCellPainting(cellPaintingEventArgs);
			if (cellPaintingEventArgs.Handled)
			{
				return;
			}
			this.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, obj, errorText, cellStyle, advancedBorderStyle, paintParts);
		}

		/// <summary>Converts a value formatted for display to an actual cell value.</summary>
		/// <param name="formattedValue">The display value of the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
		/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the display value type, or <see langword="null" /> to use the default converter.</param>
		/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the cell value type, or <see langword="null" /> to use the default converter.</param>
		/// <returns>The cell value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="cellStyle" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValueType" /> property value is <see langword="null" />.  
		///  -or-  
		///  The <see cref="P:System.Windows.Forms.DataGridViewCell.ValueType" /> property value is <see langword="null" />.  
		///  -or-  
		///  <paramref name="formattedValue" /> cannot be converted.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="formattedValue" /> is <see langword="null" />.  
		/// -or-  
		/// The type of <paramref name="formattedValue" /> does not match the type indicated by the <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValueType" /> property.</exception>
		// Token: 0x06001DF6 RID: 7670 RVA: 0x0008DF86 File Offset: 0x0008C186
		public virtual object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
		{
			return this.ParseFormattedValueInternal(this.ValueType, formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0008DF9C File Offset: 0x0008C19C
		internal object ParseFormattedValueInternal(Type valueType, object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (this.FormattedValueType == null)
			{
				throw new FormatException(SR.GetString("DataGridViewCell_FormattedValueTypeNull"));
			}
			if (valueType == null)
			{
				throw new FormatException(SR.GetString("DataGridViewCell_ValueTypeNull"));
			}
			if (formattedValue == null || !this.FormattedValueType.IsAssignableFrom(formattedValue.GetType()))
			{
				throw new ArgumentException(SR.GetString("DataGridViewCell_FormattedValueHasWrongType"), "formattedValue");
			}
			return Formatter.ParseObject(formattedValue, valueType, this.FormattedValueType, (valueTypeConverter == null) ? this.ValueTypeConverter : valueTypeConverter, (formattedValueTypeConverter == null) ? this.FormattedValueTypeConverter : formattedValueTypeConverter, cellStyle.FormatProvider, cellStyle.NullValue, cellStyle.IsDataSourceNullValueDefault ? Formatter.GetDefaultDataSourceNullValue(valueType) : cellStyle.DataSourceNullValue);
		}

		/// <summary>Sets the location and size of the editing control hosted by a cell in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
		/// <param name="setLocation">
		///   <see langword="true" /> to have the control placed as specified by the other arguments; <see langword="false" /> to allow the control to place itself.</param>
		/// <param name="setSize">
		///   <see langword="true" /> to specify the size; <see langword="false" /> to allow the control to size itself.</param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that defines the cell bounds.</param>
		/// <param name="cellClip">The area that will be used to paint the editing control.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell being edited.</param>
		/// <param name="singleVerticalBorderAdded">
		///   <see langword="true" /> to add a vertical border to the cell; otherwise, <see langword="false" />.</param>
		/// <param name="singleHorizontalBorderAdded">
		///   <see langword="true" /> to add a horizontal border to the cell; otherwise, <see langword="false" />.</param>
		/// <param name="isFirstDisplayedColumn">
		///   <see langword="true" /> if the hosting cell is in the first visible column; otherwise, <see langword="false" />.</param>
		/// <param name="isFirstDisplayedRow">
		///   <see langword="true" /> if the hosting cell is in the first visible row; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The cell is not contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001DF8 RID: 7672 RVA: 0x0008E064 File Offset: 0x0008C264
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			Rectangle rectangle = this.PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
			if (setLocation)
			{
				base.DataGridView.EditingControl.Location = new Point(rectangle.X, rectangle.Y);
			}
			if (setSize)
			{
				base.DataGridView.EditingControl.Size = new Size(rectangle.Width, rectangle.Height);
			}
		}

		/// <summary>Sets the location and size of the editing panel hosted by the cell, and returns the normal bounds of the editing control within the editing panel.</summary>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that defines the cell bounds.</param>
		/// <param name="cellClip">The area that will be used to paint the editing panel.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell being edited.</param>
		/// <param name="singleVerticalBorderAdded">
		///   <see langword="true" /> to add a vertical border to the cell; otherwise, <see langword="false" />.</param>
		/// <param name="singleHorizontalBorderAdded">
		///   <see langword="true" /> to add a horizontal border to the cell; otherwise, <see langword="false" />.</param>
		/// <param name="isFirstDisplayedColumn">
		///   <see langword="true" /> if the cell is in the first column currently displayed in the control; otherwise, <see langword="false" />.</param>
		/// <param name="isFirstDisplayedRow">
		///   <see langword="true" /> if the cell is in the first row currently displayed in the control; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the normal bounds of the editing control within the editing panel.</returns>
		/// <exception cref="T:System.InvalidOperationException">The cell has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001DF9 RID: 7673 RVA: 0x0008E0D4 File Offset: 0x0008C2D4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual Rectangle PositionEditingPanel(Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException();
			}
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = new DataGridViewAdvancedBorderStyle();
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle2 = this.AdjustCellBorderStyle(base.DataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
			Rectangle rectangle = this.BorderWidths(dataGridViewAdvancedBorderStyle2);
			rectangle.X += cellStyle.Padding.Left;
			rectangle.Y += cellStyle.Padding.Top;
			rectangle.Width += cellStyle.Padding.Right;
			rectangle.Height += cellStyle.Padding.Bottom;
			int num = cellBounds.Width;
			int num2 = cellBounds.Height;
			int num3;
			if (cellClip.X - cellBounds.X >= rectangle.X)
			{
				num3 = cellClip.X;
				num -= cellClip.X - cellBounds.X;
			}
			else
			{
				num3 = cellBounds.X + rectangle.X;
				num -= rectangle.X;
			}
			if (cellClip.Right <= cellBounds.Right - rectangle.Width)
			{
				num -= cellBounds.Right - cellClip.Right;
			}
			else
			{
				num -= rectangle.Width;
			}
			int num4 = cellBounds.X - cellClip.X;
			int num5 = cellBounds.Width - rectangle.X - rectangle.Width;
			int num6;
			if (cellClip.Y - cellBounds.Y >= rectangle.Y)
			{
				num6 = cellClip.Y;
				num2 -= cellClip.Y - cellBounds.Y;
			}
			else
			{
				num6 = cellBounds.Y + rectangle.Y;
				num2 -= rectangle.Y;
			}
			if (cellClip.Bottom <= cellBounds.Bottom - rectangle.Height)
			{
				num2 -= cellBounds.Bottom - cellClip.Bottom;
			}
			else
			{
				num2 -= rectangle.Height;
			}
			int num7 = cellBounds.Y - cellClip.Y;
			int num8 = cellBounds.Height - rectangle.Y - rectangle.Height;
			base.DataGridView.EditingPanel.Location = new Point(num3, num6);
			base.DataGridView.EditingPanel.Size = new Size(num, num2);
			return new Rectangle(num4, num7, num5, num8);
		}

		/// <summary>Sets the value of the cell.</summary>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		/// <param name="value">The cell value to set.</param>
		/// <returns>
		///   <see langword="true" /> if the value has been set; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than 0 or greater than or equal to the number of rows in the parent <see cref="T:System.Windows.Forms.DataGridView" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0.</exception>
		// Token: 0x06001DFA RID: 7674 RVA: 0x0008E34C File Offset: 0x0008C54C
		protected virtual bool SetValue(int rowIndex, object value)
		{
			object obj = null;
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView != null && !dataGridView.InSortOperation)
			{
				obj = this.GetValue(rowIndex);
			}
			if (dataGridView != null && this.OwningColumn != null && this.OwningColumn.IsDataBound)
			{
				DataGridView.DataGridViewDataConnection dataConnection = dataGridView.DataConnection;
				if (dataConnection == null)
				{
					return false;
				}
				if (dataConnection.CurrencyManager.Count <= rowIndex)
				{
					if (value != null || this.Properties.ContainsObject(DataGridViewCell.PropCellValue))
					{
						this.Properties.SetObject(DataGridViewCell.PropCellValue, value);
					}
				}
				else
				{
					if (!dataConnection.PushValue(this.OwningColumn.BoundColumnIndex, this.ColumnIndex, rowIndex, value))
					{
						return false;
					}
					if (base.DataGridView == null || this.OwningRow == null || this.OwningRow.DataGridView == null)
					{
						return true;
					}
					if (this.OwningRow.Index == base.DataGridView.CurrentCellAddress.Y)
					{
						base.DataGridView.IsCurrentRowDirtyInternal = true;
					}
				}
			}
			else if (dataGridView == null || !dataGridView.VirtualMode || rowIndex == -1 || this.ColumnIndex == -1)
			{
				if (value != null || this.Properties.ContainsObject(DataGridViewCell.PropCellValue))
				{
					this.Properties.SetObject(DataGridViewCell.PropCellValue, value);
				}
			}
			else
			{
				dataGridView.OnCellValuePushed(this.ColumnIndex, rowIndex, value);
			}
			if (dataGridView != null && !dataGridView.InSortOperation && ((obj == null && value != null) || (obj != null && value == null) || (obj != null && !value.Equals(obj))))
			{
				base.RaiseCellValueChanged(new DataGridViewCellEventArgs(this.ColumnIndex, rowIndex));
			}
			return true;
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x0008E4CC File Offset: 0x0008C6CC
		internal bool SetValueInternal(int rowIndex, object value)
		{
			return this.SetValue(rowIndex, value);
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x0008E4D8 File Offset: 0x0008C6D8
		internal static bool TextFitsInBounds(Graphics graphics, string text, Font font, Size maxBounds, TextFormatFlags flags)
		{
			bool flag;
			int num = DataGridViewCell.MeasureTextHeight(graphics, text, font, maxBounds.Width, flags, out flag);
			return num <= maxBounds.Height && !flag;
		}

		/// <summary>Returns a string that describes the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x06001DFD RID: 7677 RVA: 0x0008E50C File Offset: 0x0008C70C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewCell { ColumnIndex=",
				this.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				this.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x0008E568 File Offset: 0x0008C768
		private static string TruncateToolTipText(string toolTipText)
		{
			if (toolTipText.Length > 288)
			{
				StringBuilder stringBuilder = new StringBuilder(toolTipText.Substring(0, 256), 259);
				stringBuilder.Append("...");
				return stringBuilder.ToString();
			}
			return toolTipText;
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x0008E5B0 File Offset: 0x0008C7B0
		private void UpdateCurrentMouseLocation(DataGridViewCellMouseEventArgs e)
		{
			if (this.GetErrorIconBounds(e.RowIndex).Contains(e.X, e.Y))
			{
				this.CurrentMouseLocation = 2;
				return;
			}
			this.CurrentMouseLocation = 1;
		}

		// Token: 0x04000C84 RID: 3204
		private const TextFormatFlags textFormatSupportedFlags = TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak;

		// Token: 0x04000C85 RID: 3205
		private const int DATAGRIDVIEWCELL_constrastThreshold = 1000;

		// Token: 0x04000C86 RID: 3206
		private const int DATAGRIDVIEWCELL_highConstrastThreshold = 2000;

		// Token: 0x04000C87 RID: 3207
		private const int DATAGRIDVIEWCELL_maxToolTipLength = 288;

		// Token: 0x04000C88 RID: 3208
		private const int DATAGRIDVIEWCELL_maxToolTipCutOff = 256;

		// Token: 0x04000C89 RID: 3209
		private const int DATAGRIDVIEWCELL_toolTipEllipsisLength = 3;

		// Token: 0x04000C8A RID: 3210
		private const string DATAGRIDVIEWCELL_toolTipEllipsis = "...";

		// Token: 0x04000C8B RID: 3211
		private const byte DATAGRIDVIEWCELL_flagAreaNotSet = 0;

		// Token: 0x04000C8C RID: 3212
		private const byte DATAGRIDVIEWCELL_flagDataArea = 1;

		// Token: 0x04000C8D RID: 3213
		private const byte DATAGRIDVIEWCELL_flagErrorArea = 2;

		// Token: 0x04000C8E RID: 3214
		internal const byte DATAGRIDVIEWCELL_iconMarginWidth = 4;

		// Token: 0x04000C8F RID: 3215
		internal const byte DATAGRIDVIEWCELL_iconMarginHeight = 4;

		// Token: 0x04000C90 RID: 3216
		private const byte DATAGRIDVIEWCELL_iconsWidth = 12;

		// Token: 0x04000C91 RID: 3217
		private const byte DATAGRIDVIEWCELL_iconsHeight = 11;

		// Token: 0x04000C92 RID: 3218
		private static bool isScalingInitialized = false;

		// Token: 0x04000C93 RID: 3219
		internal static byte iconsWidth = 12;

		// Token: 0x04000C94 RID: 3220
		internal static byte iconsHeight = 11;

		// Token: 0x04000C95 RID: 3221
		internal static readonly int PropCellValue = PropertyStore.CreateKey();

		// Token: 0x04000C96 RID: 3222
		private static readonly int PropCellContextMenuStrip = PropertyStore.CreateKey();

		// Token: 0x04000C97 RID: 3223
		private static readonly int PropCellErrorText = PropertyStore.CreateKey();

		// Token: 0x04000C98 RID: 3224
		private static readonly int PropCellStyle = PropertyStore.CreateKey();

		// Token: 0x04000C99 RID: 3225
		private static readonly int PropCellValueType = PropertyStore.CreateKey();

		// Token: 0x04000C9A RID: 3226
		private static readonly int PropCellTag = PropertyStore.CreateKey();

		// Token: 0x04000C9B RID: 3227
		private static readonly int PropCellToolTipText = PropertyStore.CreateKey();

		// Token: 0x04000C9C RID: 3228
		private static readonly int PropCellAccessibilityObject = PropertyStore.CreateKey();

		// Token: 0x04000C9D RID: 3229
		private static Bitmap errorBmp = null;

		// Token: 0x04000C9E RID: 3230
		private PropertyStore propertyStore;

		// Token: 0x04000C9F RID: 3231
		private DataGridViewRow owningRow;

		// Token: 0x04000CA0 RID: 3232
		private DataGridViewColumn owningColumn;

		// Token: 0x04000CA1 RID: 3233
		private static Type stringType = typeof(string);

		// Token: 0x04000CA2 RID: 3234
		private byte flags;

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewCell" /> to accessibility client applications.</summary>
		// Token: 0x02000665 RID: 1637
		protected class DataGridViewCellAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> class without initializing the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property.</summary>
			// Token: 0x060065E1 RID: 26081 RVA: 0x001772AE File Offset: 0x001754AE
			public DataGridViewCellAccessibleObject()
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> class, setting the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property to the specified <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</param>
			// Token: 0x060065E2 RID: 26082 RVA: 0x0017BD03 File Offset: 0x00179F03
			public DataGridViewCellAccessibleObject(DataGridViewCell owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the location and size of the accessible object.</summary>
			/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the accessible object.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x17001619 RID: 5657
			// (get) Token: 0x060065E3 RID: 26083 RVA: 0x0017BD12 File Offset: 0x00179F12
			public override Rectangle Bounds
			{
				get
				{
					return this.GetAccessibleObjectBounds(this.GetAccessibleObjectParent());
				}
			}

			/// <summary>Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
			/// <returns>The string "Edit".</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x1700161A RID: 5658
			// (get) Token: 0x060065E4 RID: 26084 RVA: 0x0017BD20 File Offset: 0x00179F20
			public override string DefaultAction
			{
				get
				{
					if (this.Owner == null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
					}
					if (!this.Owner.ReadOnly)
					{
						return SR.GetString("DataGridView_AccCellDefaultAction");
					}
					return string.Empty;
				}
			}

			/// <summary>Gets the names of the owning cell's type and base type.</summary>
			/// <returns>The names of the owning cell's type and base type.</returns>
			// Token: 0x1700161B RID: 5659
			// (get) Token: 0x060065E5 RID: 26085 RVA: 0x0017BD57 File Offset: 0x00179F57
			public override string Help
			{
				get
				{
					if (AccessibilityImprovements.Level2)
					{
						return null;
					}
					return this.owner.GetType().Name + "(" + this.owner.GetType().BaseType.Name + ")";
				}
			}

			/// <summary>Gets the name of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <returns>The name of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x1700161C RID: 5660
			// (get) Token: 0x060065E6 RID: 26086 RVA: 0x0017BD98 File Offset: 0x00179F98
			public override string Name
			{
				get
				{
					if (this.owner == null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
					}
					if (this.owner.OwningColumn == null || (AccessibilityImprovements.Level5 && this.owner.OwningRow == null))
					{
						return string.Empty;
					}
					int num = (AccessibilityImprovements.Level5 ? ((this.owner.DataGridView == null) ? (-1) : this.owner.DataGridView.Rows.GetVisibleIndex(this.owner.OwningRow)) : this.owner.OwningRow.Index);
					string text = SR.GetString("DataGridView_AccDataGridViewCellName", new object[]
					{
						this.owner.OwningColumn.HeaderText,
						num
					});
					if (AccessibilityImprovements.Level3 && this.owner.OwningColumn.SortMode != DataGridViewColumnSortMode.NotSortable)
					{
						DataGridViewCell dataGridViewCell = this.Owner;
						DataGridView dataGridView = dataGridViewCell.DataGridView;
						if (dataGridViewCell.OwningColumn != null && dataGridViewCell.OwningColumn == dataGridView.SortedColumn)
						{
							text = text + ", " + ((dataGridView.SortOrder == SortOrder.Ascending) ? SR.GetString("SortedAscendingAccessibleStatus") : SR.GetString("SortedDescendingAccessibleStatus"));
						}
						else
						{
							text = text + ", " + SR.GetString("NotSortedAccessibleStatus");
						}
					}
					return text;
				}
			}

			/// <summary>Gets or sets the cell that owns the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">When setting this property, the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property has already been set.</exception>
			// Token: 0x1700161D RID: 5661
			// (get) Token: 0x060065E7 RID: 26087 RVA: 0x0017BEDC File Offset: 0x0017A0DC
			// (set) Token: 0x060065E8 RID: 26088 RVA: 0x0017BEE4 File Offset: 0x0017A0E4
			public DataGridViewCell Owner
			{
				get
				{
					return this.owner;
				}
				set
				{
					if (this.owner != null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerAlreadySet"));
					}
					this.owner = value;
				}
			}

			/// <summary>Gets the parent of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <returns>The parent of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x1700161E RID: 5662
			// (get) Token: 0x060065E9 RID: 26089 RVA: 0x0017BF05 File Offset: 0x0017A105
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.ParentPrivate;
				}
			}

			// Token: 0x1700161F RID: 5663
			// (get) Token: 0x060065EA RID: 26090 RVA: 0x0017BF0D File Offset: 0x0017A10D
			private AccessibleObject ParentPrivate
			{
				get
				{
					if (this.owner == null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
					}
					if (this.owner.OwningRow == null)
					{
						return null;
					}
					return this.owner.OwningRow.AccessibilityObject;
				}
			}

			/// <summary>Gets the role of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.Cell" /> value.</returns>
			// Token: 0x17001620 RID: 5664
			// (get) Token: 0x060065EB RID: 26091 RVA: 0x00178038 File Offset: 0x00176238
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Cell;
				}
			}

			/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates" /> values.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x17001621 RID: 5665
			// (get) Token: 0x060065EC RID: 26092 RVA: 0x0017BF48 File Offset: 0x0017A148
			public override AccessibleStates State
			{
				get
				{
					if (this.owner == null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
					}
					AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
					if (this.owner == this.owner.DataGridView.CurrentCell)
					{
						accessibleStates |= AccessibleStates.Focused;
					}
					if (this.owner.Selected)
					{
						accessibleStates |= AccessibleStates.Selected;
					}
					if (AccessibilityImprovements.Level1 && this.owner.ReadOnly)
					{
						accessibleStates |= AccessibleStates.ReadOnly;
					}
					Rectangle rectangle;
					if (this.owner.OwningColumn != null && this.owner.OwningRow != null)
					{
						rectangle = this.owner.DataGridView.GetCellDisplayRectangle(this.owner.OwningColumn.Index, this.owner.OwningRow.Index, false);
					}
					else if (this.owner.OwningRow != null)
					{
						rectangle = this.owner.DataGridView.GetCellDisplayRectangle(-1, this.owner.OwningRow.Index, false);
					}
					else if (this.owner.OwningColumn != null)
					{
						rectangle = this.owner.DataGridView.GetCellDisplayRectangle(this.owner.OwningColumn.Index, -1, false);
					}
					else
					{
						rectangle = this.owner.DataGridView.GetCellDisplayRectangle(-1, -1, false);
					}
					if (!rectangle.IntersectsWith(this.owner.DataGridView.ClientRectangle))
					{
						accessibleStates |= AccessibleStates.Offscreen;
					}
					return accessibleStates;
				}
			}

			/// <summary>Gets or sets a string representing the formatted value of the owning cell.</summary>
			/// <returns>A <see cref="T:System.String" /> representation of the cell value.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x17001622 RID: 5666
			// (get) Token: 0x060065ED RID: 26093 RVA: 0x0017C0A4 File Offset: 0x0017A2A4
			// (set) Token: 0x060065EE RID: 26094 RVA: 0x0017C13C File Offset: 0x0017A33C
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (this.owner == null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
					}
					object formattedValue = this.owner.FormattedValue;
					string text = formattedValue as string;
					if (formattedValue == null || (text != null && string.IsNullOrEmpty(text)))
					{
						return SR.GetString("DataGridView_AccNullValue");
					}
					if (text != null)
					{
						return text;
					}
					if (this.owner.OwningColumn == null)
					{
						return string.Empty;
					}
					TypeConverter formattedValueTypeConverter = this.owner.FormattedValueTypeConverter;
					if (formattedValueTypeConverter != null && formattedValueTypeConverter.CanConvertTo(typeof(string)))
					{
						return formattedValueTypeConverter.ConvertToString(formattedValue);
					}
					return formattedValue.ToString();
				}
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				set
				{
					if (this.owner is DataGridViewHeaderCell)
					{
						return;
					}
					if (this.owner.ReadOnly)
					{
						return;
					}
					if (this.owner.OwningRow == null)
					{
						return;
					}
					if (this.owner.DataGridView.IsCurrentCellInEditMode)
					{
						this.owner.DataGridView.EndEdit();
					}
					DataGridViewCellStyle inheritedStyle = this.owner.InheritedStyle;
					object formattedValue = this.owner.GetFormattedValue(value, this.owner.OwningRow.Index, ref inheritedStyle, null, null, DataGridViewDataErrorContexts.Formatting);
					this.owner.Value = this.owner.ParseFormattedValue(formattedValue, inheritedStyle, null, null);
				}
			}

			/// <summary>Performs the default action associated with the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is <see langword="null" />.  
			///  -or-  
			///  The value of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> property is not <see langword="null" /> and the <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> property of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is equal to -1.</exception>
			// Token: 0x060065EF RID: 26095 RVA: 0x0017C1E0 File Offset: 0x0017A3E0
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				DataGridViewCell dataGridViewCell = this.Owner;
				DataGridView dataGridView = dataGridViewCell.DataGridView;
				if (dataGridViewCell is DataGridViewHeaderCell)
				{
					return;
				}
				if (dataGridView != null && dataGridViewCell.RowIndex == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedCell"));
				}
				this.Select(AccessibleSelection.TakeFocus | AccessibleSelection.TakeSelection);
				if (dataGridViewCell.ReadOnly)
				{
					return;
				}
				if (dataGridViewCell.EditType != null)
				{
					if (dataGridView.InBeginEdit || dataGridView.InEndEdit)
					{
						return;
					}
					if (dataGridView.IsCurrentCellInEditMode)
					{
						dataGridView.EndEdit();
						return;
					}
					if (dataGridView.EditMode != DataGridViewEditMode.EditProgrammatically)
					{
						dataGridView.BeginEdit(true);
					}
				}
			}

			// Token: 0x060065F0 RID: 26096 RVA: 0x0017C288 File Offset: 0x0017A488
			internal Rectangle GetAccessibleObjectBounds(AccessibleObject parentAccObject)
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				if (this.owner.OwningColumn == null)
				{
					return Rectangle.Empty;
				}
				Rectangle bounds = parentAccObject.Bounds;
				int num = this.owner.DataGridView.Columns.ColumnIndexToActualDisplayIndex(this.owner.DataGridView.FirstDisplayedScrollingColumnIndex, DataGridViewElementStates.Visible);
				int num2 = this.owner.DataGridView.Columns.ColumnIndexToActualDisplayIndex(this.owner.ColumnIndex, DataGridViewElementStates.Visible);
				bool rowHeadersVisible = this.owner.DataGridView.RowHeadersVisible;
				Rectangle rectangle;
				if (num2 < num)
				{
					rectangle = parentAccObject.GetChild(num2 + 1 + (rowHeadersVisible ? 1 : 0)).Bounds;
					if (this.Owner.DataGridView.RightToLeft == RightToLeft.No)
					{
						rectangle.X -= this.owner.OwningColumn.Width;
					}
					else
					{
						rectangle.X = rectangle.Right;
					}
					rectangle.Width = this.owner.OwningColumn.Width;
				}
				else if (num2 == num)
				{
					rectangle = this.owner.DataGridView.GetColumnDisplayRectangle(this.owner.ColumnIndex, false);
					int firstDisplayedScrollingColumnHiddenWidth = this.owner.DataGridView.FirstDisplayedScrollingColumnHiddenWidth;
					if (firstDisplayedScrollingColumnHiddenWidth != 0)
					{
						if (this.owner.DataGridView.RightToLeft == RightToLeft.No)
						{
							rectangle.X -= firstDisplayedScrollingColumnHiddenWidth;
						}
						rectangle.Width += firstDisplayedScrollingColumnHiddenWidth;
					}
					rectangle = this.owner.DataGridView.RectangleToScreen(rectangle);
				}
				else
				{
					rectangle = parentAccObject.GetChild(num2 - 1 + (rowHeadersVisible ? 1 : 0)).Bounds;
					if (this.owner.DataGridView.RightToLeft == RightToLeft.No)
					{
						rectangle.X = rectangle.Right;
					}
					else
					{
						rectangle.X -= this.owner.OwningColumn.Width;
					}
					rectangle.Width = this.owner.OwningColumn.Width;
				}
				bounds.X = rectangle.X;
				bounds.Width = rectangle.Width;
				return bounds;
			}

			// Token: 0x060065F1 RID: 26097 RVA: 0x0017C4A8 File Offset: 0x0017A6A8
			private AccessibleObject GetAccessibleObjectParent()
			{
				if (this.owner is DataGridViewButtonCell || this.owner is DataGridViewCheckBoxCell || this.owner is DataGridViewComboBoxCell || this.owner is DataGridViewImageCell || this.owner is DataGridViewLinkCell || this.owner is DataGridViewTextBoxCell)
				{
					return this.ParentPrivate;
				}
				return this.Parent;
			}

			/// <summary>Returns the accessible object corresponding to the specified index.</summary>
			/// <param name="index">The zero-based index of the child accessible object.</param>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x060065F2 RID: 26098 RVA: 0x0017C510 File Offset: 0x0017A710
			public override AccessibleObject GetChild(int index)
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				if (this.owner.DataGridView.EditingControl != null && this.owner.DataGridView.IsCurrentCellInEditMode && this.owner.DataGridView.CurrentCell == this.owner && index == 0)
				{
					return this.owner.DataGridView.EditingControl.AccessibilityObject;
				}
				return null;
			}

			/// <summary>Returns the number of children that belong to the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
			/// <returns>The value 1 if the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> is being edited; otherwise, -1.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x060065F3 RID: 26099 RVA: 0x0017C58C File Offset: 0x0017A78C
			public override int GetChildCount()
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				if (this.owner.DataGridView.EditingControl != null && this.owner.DataGridView.IsCurrentCellInEditMode && this.owner.DataGridView.CurrentCell == this.owner)
				{
					return 1;
				}
				return 0;
			}

			/// <summary>Returns the child accessible object that has keyboard focus.</summary>
			/// <returns>
			///   <see langword="null" /> in all cases.</returns>
			// Token: 0x060065F4 RID: 26100 RVA: 0x00015C90 File Offset: 0x00013E90
			public override AccessibleObject GetFocused()
			{
				return null;
			}

			/// <summary>Returns the child accessible object that is currently selected.</summary>
			/// <returns>
			///   <see langword="null" /> in all cases.</returns>
			// Token: 0x060065F5 RID: 26101 RVA: 0x00015C90 File Offset: 0x00013E90
			public override AccessibleObject GetSelected()
			{
				return null;
			}

			/// <summary>Navigates to another accessible object.</summary>
			/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> that represents the <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified <see cref="T:System.Windows.Forms.AccessibleNavigation" /> value.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x060065F6 RID: 26102 RVA: 0x0017C5F0 File Offset: 0x0017A7F0
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				if (this.owner.OwningColumn == null || this.owner.OwningRow == null)
				{
					return null;
				}
				switch (navigationDirection)
				{
				case AccessibleNavigation.Up:
					if (this.owner.OwningRow.Index != this.owner.DataGridView.Rows.GetFirstRow(DataGridViewElementStates.Visible))
					{
						int previousRow = this.owner.DataGridView.Rows.GetPreviousRow(this.owner.OwningRow.Index, DataGridViewElementStates.Visible);
						return this.owner.DataGridView.Rows[previousRow].Cells[this.owner.OwningColumn.Index].AccessibilityObject;
					}
					if (this.owner.DataGridView.ColumnHeadersVisible)
					{
						return this.owner.OwningColumn.HeaderCell.AccessibilityObject;
					}
					return null;
				case AccessibleNavigation.Down:
				{
					if (this.owner.OwningRow.Index == this.owner.DataGridView.Rows.GetLastRow(DataGridViewElementStates.Visible))
					{
						return null;
					}
					int nextRow = this.owner.DataGridView.Rows.GetNextRow(this.owner.OwningRow.Index, DataGridViewElementStates.Visible);
					return this.owner.DataGridView.Rows[nextRow].Cells[this.owner.OwningColumn.Index].AccessibilityObject;
				}
				case AccessibleNavigation.Left:
					if (this.owner.DataGridView.RightToLeft == RightToLeft.No)
					{
						return this.NavigateBackward(true);
					}
					return this.NavigateForward(true);
				case AccessibleNavigation.Right:
					if (this.owner.DataGridView.RightToLeft == RightToLeft.No)
					{
						return this.NavigateForward(true);
					}
					return this.NavigateBackward(true);
				case AccessibleNavigation.Next:
					return this.NavigateForward(false);
				case AccessibleNavigation.Previous:
					return this.NavigateBackward(false);
				default:
					return null;
				}
			}

			// Token: 0x060065F7 RID: 26103 RVA: 0x0017C7E8 File Offset: 0x0017A9E8
			private AccessibleObject NavigateBackward(bool wrapAround)
			{
				if (this.owner.OwningColumn != this.owner.DataGridView.Columns.GetFirstColumn(DataGridViewElementStates.Visible))
				{
					int index = this.owner.DataGridView.Columns.GetPreviousColumn(this.owner.OwningColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
					return this.owner.OwningRow.Cells[index].AccessibilityObject;
				}
				if (wrapAround)
				{
					AccessibleObject accessibleObject = this.Owner.OwningRow.AccessibilityObject.Navigate(AccessibleNavigation.Previous);
					if (accessibleObject != null && accessibleObject.GetChildCount() > 0)
					{
						return accessibleObject.GetChild(accessibleObject.GetChildCount() - 1);
					}
					return null;
				}
				else
				{
					if (this.owner.DataGridView.RowHeadersVisible)
					{
						return this.owner.OwningRow.AccessibilityObject.GetChild(0);
					}
					return null;
				}
			}

			// Token: 0x060065F8 RID: 26104 RVA: 0x0017C8C0 File Offset: 0x0017AAC0
			private AccessibleObject NavigateForward(bool wrapAround)
			{
				if (this.owner.OwningColumn != this.owner.DataGridView.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None))
				{
					int index = this.owner.DataGridView.Columns.GetNextColumn(this.owner.OwningColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
					return this.owner.OwningRow.Cells[index].AccessibilityObject;
				}
				if (!wrapAround)
				{
					return null;
				}
				AccessibleObject accessibleObject = this.Owner.OwningRow.AccessibilityObject.Navigate(AccessibleNavigation.Next);
				if (accessibleObject == null || accessibleObject.GetChildCount() <= 0)
				{
					return null;
				}
				if (this.Owner.DataGridView.RowHeadersVisible)
				{
					return accessibleObject.GetChild(1);
				}
				return accessibleObject.GetChild(0);
			}

			/// <summary>Modifies the selection or moves the keyboard focus of the accessible object.</summary>
			/// <param name="flags">One of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values.</param>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x060065F9 RID: 26105 RVA: 0x0017C984 File Offset: 0x0017AB84
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
				{
					this.owner.DataGridView.FocusInternal();
				}
				if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection)
				{
					this.owner.Selected = true;
					this.owner.DataGridView.CurrentCell = this.owner;
				}
				if ((flags & AccessibleSelection.AddSelection) == AccessibleSelection.AddSelection)
				{
					this.owner.Selected = true;
				}
				if ((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection && (flags & (AccessibleSelection.TakeSelection | AccessibleSelection.AddSelection)) == AccessibleSelection.None)
				{
					this.owner.Selected = false;
				}
			}

			// Token: 0x060065FA RID: 26106 RVA: 0x0017CA14 File Offset: 0x0017AC14
			internal override void SetDetachableChild(AccessibleObject child)
			{
				this._child = child;
			}

			// Token: 0x060065FB RID: 26107 RVA: 0x0016FF79 File Offset: 0x0016E179
			internal override void SetFocus()
			{
				base.SetFocus();
				base.RaiseAutomationEvent(20005);
			}

			// Token: 0x17001623 RID: 5667
			// (get) Token: 0x060065FC RID: 26108 RVA: 0x0017CA1D File Offset: 0x0017AC1D
			internal override int[] RuntimeId
			{
				get
				{
					if (this.runtimeId == null)
					{
						this.runtimeId = new int[2];
						this.runtimeId[0] = 42;
						this.runtimeId[1] = this.GetHashCode();
					}
					return this.runtimeId;
				}
			}

			// Token: 0x17001624 RID: 5668
			// (get) Token: 0x060065FD RID: 26109 RVA: 0x0017CA54 File Offset: 0x0017AC54
			private string AutomationId
			{
				get
				{
					string text = string.Empty;
					foreach (int num in this.RuntimeId)
					{
						text += num.ToString();
					}
					return text;
				}
			}

			// Token: 0x060065FE RID: 26110 RVA: 0x0017CA8F File Offset: 0x0017AC8F
			internal override bool IsIAccessibleExSupported()
			{
				return AccessibilityImprovements.Level2 || base.IsIAccessibleExSupported();
			}

			// Token: 0x17001625 RID: 5669
			// (get) Token: 0x060065FF RID: 26111 RVA: 0x00016039 File Offset: 0x00014239
			internal override Rectangle BoundingRectangle
			{
				get
				{
					return this.Bounds;
				}
			}

			// Token: 0x17001626 RID: 5670
			// (get) Token: 0x06006600 RID: 26112 RVA: 0x0017CAA0 File Offset: 0x0017ACA0
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					return this.owner.DataGridView.AccessibilityObject;
				}
			}

			// Token: 0x06006601 RID: 26113 RVA: 0x0017CAB4 File Offset: 0x0017ACB4
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				if (this.owner.OwningColumn == null || this.owner.OwningRow == null)
				{
					return null;
				}
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return this.owner.OwningRow.AccessibilityObject;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
					return this.NavigateForward(false);
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
					return this.NavigateBackward(false);
				case UnsafeNativeMethods.NavigateDirection.FirstChild:
				case UnsafeNativeMethods.NavigateDirection.LastChild:
					if (this.owner.DataGridView.CurrentCell == this.owner && this.owner.DataGridView.IsCurrentCellInEditMode && this.owner.DataGridView.EditingControl != null)
					{
						return this._child;
					}
					return null;
				default:
					return null;
				}
			}

			// Token: 0x06006602 RID: 26114 RVA: 0x0017CB78 File Offset: 0x0017AD78
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level5 && propertyID == 30003)
				{
					return 50029;
				}
				if (AccessibilityImprovements.Level3)
				{
					switch (propertyID)
					{
					case 30005:
						return this.Name;
					case 30006:
					case 30012:
					case 30014:
					case 30015:
					case 30016:
					case 30017:
					case 30018:
						break;
					case 30007:
						return string.Empty;
					case 30008:
						return (this.State & AccessibleStates.Focused) == AccessibleStates.Focused;
					case 30009:
						return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
					case 30010:
						return this.owner.DataGridView.Enabled;
					case 30011:
						return this.AutomationId;
					case 30013:
						return this.Help ?? string.Empty;
					case 30019:
						return false;
					default:
						if (propertyID == 30022)
						{
							return (this.State & AccessibleStates.Offscreen) == AccessibleStates.Offscreen;
						}
						if (propertyID == 30068)
						{
							return this.Owner.DataGridView.AccessibilityObject;
						}
						break;
					}
				}
				if (propertyID == 30039)
				{
					return this.IsPatternSupported(10013);
				}
				if (propertyID == 30029)
				{
					return this.IsPatternSupported(10007);
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006603 RID: 26115 RVA: 0x0017CCD8 File Offset: 0x0017AED8
			internal override bool IsPatternSupported(int patternId)
			{
				return (AccessibilityImprovements.Level3 && (patternId.Equals(10018) || patternId.Equals(10000) || patternId.Equals(10002))) || ((patternId == 10013 || patternId == 10007) && this.owner.ColumnIndex != -1 && this.owner.RowIndex != -1) || base.IsPatternSupported(patternId);
			}

			// Token: 0x06006604 RID: 26116 RVA: 0x0017CD50 File Offset: 0x0017AF50
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetRowHeaderItems()
			{
				if (this.owner.DataGridView.RowHeadersVisible && this.owner.OwningRow.HasHeaderCell)
				{
					return new UnsafeNativeMethods.IRawElementProviderSimple[] { this.owner.OwningRow.HeaderCell.AccessibilityObject };
				}
				return null;
			}

			// Token: 0x06006605 RID: 26117 RVA: 0x0017CDA4 File Offset: 0x0017AFA4
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetColumnHeaderItems()
			{
				if (this.owner.DataGridView.ColumnHeadersVisible && this.owner.OwningColumn.HasHeaderCell)
				{
					return new UnsafeNativeMethods.IRawElementProviderSimple[] { this.owner.OwningColumn.HeaderCell.AccessibilityObject };
				}
				return null;
			}

			// Token: 0x17001627 RID: 5671
			// (get) Token: 0x06006606 RID: 26118 RVA: 0x0017CDF8 File Offset: 0x0017AFF8
			internal override int Row
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (!AccessibilityImprovements.Level5)
					{
						if (this.owner.OwningRow == null)
						{
							return -1;
						}
						return this.owner.OwningRow.Index;
					}
					else
					{
						DataGridViewCell dataGridViewCell = this.owner;
						bool? flag;
						if (dataGridViewCell == null)
						{
							flag = null;
						}
						else
						{
							DataGridViewRow owningRow = dataGridViewCell.OwningRow;
							flag = ((owningRow != null) ? new bool?(owningRow.Visible) : null);
						}
						if (!(flag ?? false) || this.owner.DataGridView == null)
						{
							return -1;
						}
						return this.owner.DataGridView.Rows.GetVisibleIndex(this.owner.OwningRow);
					}
				}
			}

			// Token: 0x17001628 RID: 5672
			// (get) Token: 0x06006607 RID: 26119 RVA: 0x0017CEA4 File Offset: 0x0017B0A4
			internal override int Column
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (!AccessibilityImprovements.Level5)
					{
						if (this.owner.OwningColumn == null)
						{
							return -1;
						}
						return this.owner.OwningColumn.Index;
					}
					else
					{
						DataGridViewCell dataGridViewCell = this.owner;
						bool? flag;
						if (dataGridViewCell == null)
						{
							flag = null;
						}
						else
						{
							DataGridViewColumn owningColumn = dataGridViewCell.OwningColumn;
							flag = ((owningColumn != null) ? new bool?(owningColumn.Visible) : null);
						}
						if (!(flag ?? false) || this.owner.DataGridView == null)
						{
							return -1;
						}
						return this.owner.DataGridView.Columns.GetVisibleIndex(this.owner.OwningColumn);
					}
				}
			}

			// Token: 0x17001629 RID: 5673
			// (get) Token: 0x06006608 RID: 26120 RVA: 0x0017CAA0 File Offset: 0x0017ACA0
			internal override UnsafeNativeMethods.IRawElementProviderSimple ContainingGrid
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.owner.DataGridView.AccessibilityObject;
				}
			}

			// Token: 0x1700162A RID: 5674
			// (get) Token: 0x06006609 RID: 26121 RVA: 0x0017CF4D File Offset: 0x0017B14D
			internal override bool IsReadOnly
			{
				get
				{
					return this.owner.ReadOnly;
				}
			}

			// Token: 0x04003A53 RID: 14931
			private int[] runtimeId;

			// Token: 0x04003A54 RID: 14932
			private AccessibleObject _child;

			// Token: 0x04003A55 RID: 14933
			private DataGridViewCell owner;
		}
	}
}
