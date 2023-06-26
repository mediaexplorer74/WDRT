using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Represents a row in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x02000206 RID: 518
	[TypeConverter(typeof(DataGridViewRowConverter))]
	public class DataGridViewRow : DataGridViewBand
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> class without using a template.</summary>
		// Token: 0x060021CA RID: 8650 RVA: 0x0009FBFF File Offset: 0x0009DDFF
		public DataGridViewRow()
		{
			this.bandIsRow = true;
			base.MinimumThickness = 3;
			base.Thickness = Control.DefaultFont.Height + 9;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> assigned to the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> assigned to the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x0009FC28 File Offset: 0x0009DE28
		[Browsable(false)]
		public AccessibleObject AccessibilityObject
		{
			get
			{
				AccessibleObject accessibleObject = (AccessibleObject)base.Properties.GetObject(DataGridViewRow.PropRowAccessibilityObject);
				if (accessibleObject == null)
				{
					accessibleObject = this.CreateAccessibilityInstance();
					base.Properties.SetObject(DataGridViewRow.PropRowAccessibilityObject, accessibleObject);
				}
				return accessibleObject;
			}
		}

		/// <summary>Gets the collection of cells that populate the row.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> that contains all of the cells in the row.</returns>
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x0009FC67 File Offset: 0x0009DE67
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataGridViewCellCollection Cells
		{
			get
			{
				if (this.rowCells == null)
				{
					this.rowCells = this.CreateCellsInstance();
				}
				return this.rowCells;
			}
		}

		/// <summary>Gets or sets the shortcut menu for the row.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the current <see cref="T:System.Windows.Forms.DataGridViewRow" />. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">When getting the value of this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060021CD RID: 8653 RVA: 0x000934B6 File Offset: 0x000916B6
		// (set) Token: 0x060021CE RID: 8654 RVA: 0x000934BE File Offset: 0x000916BE
		[DefaultValue(null)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_RowContextMenuStripDescr")]
		public override ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return base.ContextMenuStrip;
			}
			set
			{
				base.ContextMenuStrip = value;
			}
		}

		/// <summary>Gets the data-bound object that populated the row.</summary>
		/// <returns>The data-bound <see cref="T:System.Object" />.</returns>
		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x0009FC84 File Offset: 0x0009DE84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object DataBoundItem
		{
			get
			{
				if (base.DataGridView != null && base.DataGridView.DataConnection != null && base.Index > -1 && base.Index != base.DataGridView.NewRowIndex)
				{
					return base.DataGridView.DataConnection.CurrencyManager[base.Index];
				}
				return null;
			}
		}

		/// <summary>Gets or sets the default styles for the row, which are used to render cells in the row unless the styles are overridden.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied as the default style.</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x00093504 File Offset: 0x00091704
		// (set) Token: 0x060021D1 RID: 8657 RVA: 0x0009FCDF File Offset: 0x0009DEDF
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_RowDefaultCellStyleDescr")]
		public override DataGridViewCellStyle DefaultCellStyle
		{
			get
			{
				return base.DefaultCellStyle;
			}
			set
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[] { "DefaultCellStyle" }));
				}
				base.DefaultCellStyle = value;
			}
		}

		/// <summary>Gets a value indicating whether this row is displayed on the screen.</summary>
		/// <returns>
		///   <see langword="true" /> if the row is currently displayed on the screen; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x0009FD17 File Offset: 0x0009DF17
		[Browsable(false)]
		public override bool Displayed
		{
			get
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[] { "Displayed" }));
				}
				return this.GetDisplayed(base.Index);
			}
		}

		/// <summary>Gets or sets the height, in pixels, of the row divider.</summary>
		/// <returns>The height, in pixels, of the divider (the row's bottom margin).</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x000937C3 File Offset: 0x000919C3
		// (set) Token: 0x060021D4 RID: 8660 RVA: 0x0009FD54 File Offset: 0x0009DF54
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_RowDividerHeightDescr")]
		public int DividerHeight
		{
			get
			{
				return base.DividerThickness;
			}
			set
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[] { "DividerHeight" }));
				}
				base.DividerThickness = value;
			}
		}

		/// <summary>Gets or sets the error message text for row-level errors.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the error message.</returns>
		/// <exception cref="T:System.InvalidOperationException">When getting the value of this property, the row is a shared row in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x0009FD8C File Offset: 0x0009DF8C
		// (set) Token: 0x060021D6 RID: 8662 RVA: 0x0009FD9A File Offset: 0x0009DF9A
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_RowErrorTextDescr")]
		public string ErrorText
		{
			get
			{
				return this.GetErrorText(base.Index);
			}
			set
			{
				this.ErrorTextInternal = value;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x0009FDA4 File Offset: 0x0009DFA4
		// (set) Token: 0x060021D8 RID: 8664 RVA: 0x0009FDD4 File Offset: 0x0009DFD4
		private string ErrorTextInternal
		{
			get
			{
				object @object = base.Properties.GetObject(DataGridViewRow.PropRowErrorText);
				if (@object != null)
				{
					return (string)@object;
				}
				return string.Empty;
			}
			set
			{
				string errorTextInternal = this.ErrorTextInternal;
				if (!string.IsNullOrEmpty(value) || base.Properties.ContainsObject(DataGridViewRow.PropRowErrorText))
				{
					base.Properties.SetObject(DataGridViewRow.PropRowErrorText, value);
				}
				if (base.DataGridView != null && !errorTextInternal.Equals(this.ErrorTextInternal))
				{
					base.DataGridView.OnRowErrorTextChanged(this);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the row is frozen.</summary>
		/// <returns>
		///   <see langword="true" /> if the row is frozen; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x0009FE35 File Offset: 0x0009E035
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x0009FE72 File Offset: 0x0009E072
		[Browsable(false)]
		public override bool Frozen
		{
			get
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[] { "Frozen" }));
				}
				return this.GetFrozen(base.Index);
			}
			set
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[] { "Frozen" }));
				}
				base.Frozen = value;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x0009FEAA File Offset: 0x0009E0AA
		internal bool HasErrorText
		{
			get
			{
				return base.Properties.ContainsObject(DataGridViewRow.PropRowErrorText) && base.Properties.GetObject(DataGridViewRow.PropRowErrorText) != null;
			}
		}

		/// <summary>Gets or sets the row's header cell.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" /> that represents the header cell of row.</returns>
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x0009FED3 File Offset: 0x0009E0D3
		// (set) Token: 0x060021DD RID: 8669 RVA: 0x000938DA File Offset: 0x00091ADA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataGridViewRowHeaderCell HeaderCell
		{
			get
			{
				return (DataGridViewRowHeaderCell)base.HeaderCellCore;
			}
			set
			{
				base.HeaderCellCore = value;
			}
		}

		/// <summary>Gets or sets the current height of the row.</summary>
		/// <returns>The height, in pixels, of the row. The default is the height of the default font plus 9 pixels.</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x00093F2F File Offset: 0x0009212F
		// (set) Token: 0x060021DF RID: 8671 RVA: 0x0009FEE0 File Offset: 0x0009E0E0
		[DefaultValue(22)]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_RowHeightDescr")]
		public int Height
		{
			get
			{
				return base.Thickness;
			}
			set
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[] { "Height" }));
				}
				base.Thickness = value;
			}
		}

		/// <summary>Gets the cell style in effect for the row.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that specifies the formatting and style information for the cells in the row.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x0009FF18 File Offset: 0x0009E118
		public override DataGridViewCellStyle InheritedStyle
		{
			get
			{
				if (base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[] { "InheritedStyle" }));
				}
				DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
				this.BuildInheritedRowStyle(base.Index, dataGridViewCellStyle);
				return dataGridViewCellStyle;
			}
		}

		/// <summary>Gets a value indicating whether the row is the row for new records.</summary>
		/// <returns>
		///   <see langword="true" /> if the row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" />, which is used for the entry of a new row of data; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x0009FF60 File Offset: 0x0009E160
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsNewRow
		{
			get
			{
				return base.DataGridView != null && base.DataGridView.NewRowIndex == base.Index;
			}
		}

		/// <summary>Gets or sets the minimum height of the row.</summary>
		/// <returns>The minimum row height in pixels, ranging from 2 to <see cref="F:System.Int32.MaxValue" />. The default is 3.</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 2.</exception>
		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x00093C4B File Offset: 0x00091E4B
		// (set) Token: 0x060021E3 RID: 8675 RVA: 0x0009FF7F File Offset: 0x0009E17F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int MinimumHeight
		{
			get
			{
				return base.MinimumThickness;
			}
			set
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[] { "MinimumHeight" }));
				}
				base.MinimumThickness = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the row is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the row is read-only; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x0009FFB7 File Offset: 0x0009E1B7
		// (set) Token: 0x060021E5 RID: 8677 RVA: 0x0009FFF4 File Offset: 0x0009E1F4
		[Browsable(true)]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_RowReadOnlyDescr")]
		public override bool ReadOnly
		{
			get
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[] { "ReadOnly" }));
				}
				return this.GetReadOnly(base.Index);
			}
			set
			{
				base.ReadOnly = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether users can resize the row or indicating that the behavior is inherited from the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToResizeRows" /> property.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewTriState" /> value that indicates whether the row can be resized or whether it can be resized only when the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToResizeRows" /> property is set to <see langword="true" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x0009FFFD File Offset: 0x0009E1FD
		// (set) Token: 0x060021E7 RID: 8679 RVA: 0x00093D59 File Offset: 0x00091F59
		[NotifyParentProperty(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_RowResizableDescr")]
		public override DataGridViewTriState Resizable
		{
			get
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[] { "Resizable" }));
				}
				return this.GetResizable(base.Index);
			}
			set
			{
				base.Resizable = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the row is selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the row is selected; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x000A003A File Offset: 0x0009E23A
		// (set) Token: 0x060021E9 RID: 8681 RVA: 0x000A0077 File Offset: 0x0009E277
		public override bool Selected
		{
			get
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[] { "Selected" }));
				}
				return this.GetSelected(base.Index);
			}
			set
			{
				base.Selected = value;
			}
		}

		/// <summary>Gets the current state of the row.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the row state.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x000A0080 File Offset: 0x0009E280
		public override DataGridViewElementStates State
		{
			get
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[] { "State" }));
				}
				return this.GetState(base.Index);
			}
		}

		/// <summary>Gets or sets a value indicating whether the row is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the row is visible; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x000A00BD File Offset: 0x0009E2BD
		// (set) Token: 0x060021EC RID: 8684 RVA: 0x000A00FA File Offset: 0x0009E2FA
		[Browsable(false)]
		public override bool Visible
		{
			get
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[] { "Visible" }));
				}
				return this.GetVisible(base.Index);
			}
			set
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[] { "Visible" }));
				}
				base.Visible = value;
			}
		}

		/// <summary>Modifies an input row header border style according to the specified criteria.</summary>
		/// <param name="dataGridViewAdvancedBorderStyleInput">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the row header border style to modify.</param>
		/// <param name="dataGridViewAdvancedBorderStylePlaceholder">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that is used to store intermediate changes to the row header border style.</param>
		/// <param name="singleVerticalBorderAdded">
		///   <see langword="true" /> to add a single vertical border to the result; otherwise, <see langword="false" />.</param>
		/// <param name="singleHorizontalBorderAdded">
		///   <see langword="true" /> to add a single horizontal border to the result; otherwise, <see langword="false" />.</param>
		/// <param name="isFirstDisplayedRow">
		///   <see langword="true" /> if the row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, <see langword="false" />.</param>
		/// <param name="isLastVisibleRow">
		///   <see langword="true" /> if the row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has its <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to <see langword="true" />; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the new border style used.</returns>
		// Token: 0x060021ED RID: 8685 RVA: 0x000A0134 File Offset: 0x0009E334
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual DataGridViewAdvancedBorderStyle AdjustRowHeaderBorderStyle(DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedRow, bool isLastVisibleRow)
		{
			if (base.DataGridView != null && base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				switch (dataGridViewAdvancedBorderStyleInput.All)
				{
				case DataGridViewAdvancedCellBorderStyle.Single:
					if (isFirstDisplayedRow && !base.DataGridView.ColumnHeadersVisible)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.Single;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
					}
					dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Single;
					dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Single;
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.None;
					return dataGridViewAdvancedBorderStylePlaceholder;
				case DataGridViewAdvancedCellBorderStyle.Inset:
					if (isFirstDisplayedRow && !base.DataGridView.ColumnHeadersVisible)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
					}
					dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.None;
					return dataGridViewAdvancedBorderStylePlaceholder;
				case DataGridViewAdvancedCellBorderStyle.InsetDouble:
					if (isFirstDisplayedRow && !base.DataGridView.ColumnHeadersVisible)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.InsetDouble;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
					}
					if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.InsetDouble;
					}
					dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.None;
					return dataGridViewAdvancedBorderStylePlaceholder;
				case DataGridViewAdvancedCellBorderStyle.Outset:
					if (isFirstDisplayedRow && !base.DataGridView.ColumnHeadersVisible)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
					}
					dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.None;
					return dataGridViewAdvancedBorderStylePlaceholder;
				case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
					if (isFirstDisplayedRow && !base.DataGridView.ColumnHeadersVisible)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
					}
					if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
					}
					dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.None;
					return dataGridViewAdvancedBorderStylePlaceholder;
				case DataGridViewAdvancedCellBorderStyle.OutsetPartial:
					if (isFirstDisplayedRow && !base.DataGridView.ColumnHeadersVisible)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
					}
					if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
					}
					dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.None;
					return dataGridViewAdvancedBorderStylePlaceholder;
				}
			}
			else
			{
				switch (dataGridViewAdvancedBorderStyleInput.All)
				{
				case DataGridViewAdvancedCellBorderStyle.Single:
					if (!isFirstDisplayedRow || base.DataGridView.ColumnHeadersVisible)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Single;
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
						dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.Single;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Single;
						return dataGridViewAdvancedBorderStylePlaceholder;
					}
					break;
				case DataGridViewAdvancedCellBorderStyle.Inset:
					if (isFirstDisplayedRow && singleHorizontalBorderAdded)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Inset;
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.InsetDouble;
						dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.Inset;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Inset;
						return dataGridViewAdvancedBorderStylePlaceholder;
					}
					break;
				case DataGridViewAdvancedCellBorderStyle.InsetDouble:
					if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Inset;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.InsetDouble;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.InsetDouble;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					}
					if (isFirstDisplayedRow)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = (base.DataGridView.ColumnHeadersVisible ? DataGridViewAdvancedCellBorderStyle.Inset : DataGridViewAdvancedCellBorderStyle.InsetDouble);
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					}
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					return dataGridViewAdvancedBorderStylePlaceholder;
				case DataGridViewAdvancedCellBorderStyle.Outset:
					if (isFirstDisplayedRow && singleHorizontalBorderAdded)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Outset;
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
						dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.Outset;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Outset;
						return dataGridViewAdvancedBorderStylePlaceholder;
					}
					break;
				case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
					if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Outset;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					if (isFirstDisplayedRow)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = (base.DataGridView.ColumnHeadersVisible ? DataGridViewAdvancedCellBorderStyle.Outset : DataGridViewAdvancedCellBorderStyle.OutsetDouble);
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					return dataGridViewAdvancedBorderStylePlaceholder;
				case DataGridViewAdvancedCellBorderStyle.OutsetPartial:
					if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Outset;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					if (isFirstDisplayedRow)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = (base.DataGridView.ColumnHeadersVisible ? DataGridViewAdvancedCellBorderStyle.Outset : DataGridViewAdvancedCellBorderStyle.OutsetDouble);
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.OutsetPartial;
					}
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = (isLastVisibleRow ? DataGridViewAdvancedCellBorderStyle.Outset : DataGridViewAdvancedCellBorderStyle.OutsetPartial);
					return dataGridViewAdvancedBorderStylePlaceholder;
				}
			}
			return dataGridViewAdvancedBorderStyleInput;
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x000A0508 File Offset: 0x0009E708
		private void BuildInheritedRowHeaderCellStyle(DataGridViewCellStyle inheritedCellStyle)
		{
			DataGridViewCellStyle dataGridViewCellStyle = null;
			if (this.HeaderCell.HasStyle)
			{
				dataGridViewCellStyle = this.HeaderCell.Style;
			}
			DataGridViewCellStyle rowHeadersDefaultCellStyle = base.DataGridView.RowHeadersDefaultCellStyle;
			DataGridViewCellStyle defaultCellStyle = base.DataGridView.DefaultCellStyle;
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.BackColor.IsEmpty)
			{
				inheritedCellStyle.BackColor = dataGridViewCellStyle.BackColor;
			}
			else if (!rowHeadersDefaultCellStyle.BackColor.IsEmpty)
			{
				inheritedCellStyle.BackColor = rowHeadersDefaultCellStyle.BackColor;
			}
			else
			{
				inheritedCellStyle.BackColor = defaultCellStyle.BackColor;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.ForeColor.IsEmpty)
			{
				inheritedCellStyle.ForeColor = dataGridViewCellStyle.ForeColor;
			}
			else if (!rowHeadersDefaultCellStyle.ForeColor.IsEmpty)
			{
				inheritedCellStyle.ForeColor = rowHeadersDefaultCellStyle.ForeColor;
			}
			else
			{
				inheritedCellStyle.ForeColor = defaultCellStyle.ForeColor;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.SelectionBackColor.IsEmpty)
			{
				inheritedCellStyle.SelectionBackColor = dataGridViewCellStyle.SelectionBackColor;
			}
			else if (!rowHeadersDefaultCellStyle.SelectionBackColor.IsEmpty)
			{
				inheritedCellStyle.SelectionBackColor = rowHeadersDefaultCellStyle.SelectionBackColor;
			}
			else
			{
				inheritedCellStyle.SelectionBackColor = defaultCellStyle.SelectionBackColor;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.SelectionForeColor.IsEmpty)
			{
				inheritedCellStyle.SelectionForeColor = dataGridViewCellStyle.SelectionForeColor;
			}
			else if (!rowHeadersDefaultCellStyle.SelectionForeColor.IsEmpty)
			{
				inheritedCellStyle.SelectionForeColor = rowHeadersDefaultCellStyle.SelectionForeColor;
			}
			else
			{
				inheritedCellStyle.SelectionForeColor = defaultCellStyle.SelectionForeColor;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Font != null)
			{
				inheritedCellStyle.Font = dataGridViewCellStyle.Font;
			}
			else if (rowHeadersDefaultCellStyle.Font != null)
			{
				inheritedCellStyle.Font = rowHeadersDefaultCellStyle.Font;
			}
			else
			{
				inheritedCellStyle.Font = defaultCellStyle.Font;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsNullValueDefault)
			{
				inheritedCellStyle.NullValue = dataGridViewCellStyle.NullValue;
			}
			else if (!rowHeadersDefaultCellStyle.IsNullValueDefault)
			{
				inheritedCellStyle.NullValue = rowHeadersDefaultCellStyle.NullValue;
			}
			else
			{
				inheritedCellStyle.NullValue = defaultCellStyle.NullValue;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsDataSourceNullValueDefault)
			{
				inheritedCellStyle.DataSourceNullValue = dataGridViewCellStyle.DataSourceNullValue;
			}
			else if (!rowHeadersDefaultCellStyle.IsDataSourceNullValueDefault)
			{
				inheritedCellStyle.DataSourceNullValue = rowHeadersDefaultCellStyle.DataSourceNullValue;
			}
			else
			{
				inheritedCellStyle.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Format.Length != 0)
			{
				inheritedCellStyle.Format = dataGridViewCellStyle.Format;
			}
			else if (rowHeadersDefaultCellStyle.Format.Length != 0)
			{
				inheritedCellStyle.Format = rowHeadersDefaultCellStyle.Format;
			}
			else
			{
				inheritedCellStyle.Format = defaultCellStyle.Format;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsFormatProviderDefault)
			{
				inheritedCellStyle.FormatProvider = dataGridViewCellStyle.FormatProvider;
			}
			else if (!rowHeadersDefaultCellStyle.IsFormatProviderDefault)
			{
				inheritedCellStyle.FormatProvider = rowHeadersDefaultCellStyle.FormatProvider;
			}
			else
			{
				inheritedCellStyle.FormatProvider = defaultCellStyle.FormatProvider;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				inheritedCellStyle.AlignmentInternal = dataGridViewCellStyle.Alignment;
			}
			else if (rowHeadersDefaultCellStyle != null && rowHeadersDefaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				inheritedCellStyle.AlignmentInternal = rowHeadersDefaultCellStyle.Alignment;
			}
			else
			{
				inheritedCellStyle.AlignmentInternal = defaultCellStyle.Alignment;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				inheritedCellStyle.WrapModeInternal = dataGridViewCellStyle.WrapMode;
			}
			else if (rowHeadersDefaultCellStyle != null && rowHeadersDefaultCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				inheritedCellStyle.WrapModeInternal = rowHeadersDefaultCellStyle.WrapMode;
			}
			else
			{
				inheritedCellStyle.WrapModeInternal = defaultCellStyle.WrapMode;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Tag != null)
			{
				inheritedCellStyle.Tag = dataGridViewCellStyle.Tag;
			}
			else if (rowHeadersDefaultCellStyle.Tag != null)
			{
				inheritedCellStyle.Tag = rowHeadersDefaultCellStyle.Tag;
			}
			else
			{
				inheritedCellStyle.Tag = defaultCellStyle.Tag;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Padding != Padding.Empty)
			{
				inheritedCellStyle.PaddingInternal = dataGridViewCellStyle.Padding;
				return;
			}
			if (rowHeadersDefaultCellStyle.Padding != Padding.Empty)
			{
				inheritedCellStyle.PaddingInternal = rowHeadersDefaultCellStyle.Padding;
				return;
			}
			inheritedCellStyle.PaddingInternal = defaultCellStyle.Padding;
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x000A08AC File Offset: 0x0009EAAC
		private void BuildInheritedRowStyle(int rowIndex, DataGridViewCellStyle inheritedRowStyle)
		{
			DataGridViewCellStyle dataGridViewCellStyle = null;
			if (base.HasDefaultCellStyle)
			{
				dataGridViewCellStyle = this.DefaultCellStyle;
			}
			DataGridViewCellStyle defaultCellStyle = base.DataGridView.DefaultCellStyle;
			DataGridViewCellStyle rowsDefaultCellStyle = base.DataGridView.RowsDefaultCellStyle;
			DataGridViewCellStyle alternatingRowsDefaultCellStyle = base.DataGridView.AlternatingRowsDefaultCellStyle;
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.BackColor.IsEmpty)
			{
				inheritedRowStyle.BackColor = dataGridViewCellStyle.BackColor;
			}
			else if (!rowsDefaultCellStyle.BackColor.IsEmpty && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.BackColor.IsEmpty))
			{
				inheritedRowStyle.BackColor = rowsDefaultCellStyle.BackColor;
			}
			else if (rowIndex % 2 == 1 && !alternatingRowsDefaultCellStyle.BackColor.IsEmpty)
			{
				inheritedRowStyle.BackColor = alternatingRowsDefaultCellStyle.BackColor;
			}
			else
			{
				inheritedRowStyle.BackColor = defaultCellStyle.BackColor;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.ForeColor.IsEmpty)
			{
				inheritedRowStyle.ForeColor = dataGridViewCellStyle.ForeColor;
			}
			else if (!rowsDefaultCellStyle.ForeColor.IsEmpty && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.ForeColor.IsEmpty))
			{
				inheritedRowStyle.ForeColor = rowsDefaultCellStyle.ForeColor;
			}
			else if (rowIndex % 2 == 1 && !alternatingRowsDefaultCellStyle.ForeColor.IsEmpty)
			{
				inheritedRowStyle.ForeColor = alternatingRowsDefaultCellStyle.ForeColor;
			}
			else
			{
				inheritedRowStyle.ForeColor = defaultCellStyle.ForeColor;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.SelectionBackColor.IsEmpty)
			{
				inheritedRowStyle.SelectionBackColor = dataGridViewCellStyle.SelectionBackColor;
			}
			else if (!rowsDefaultCellStyle.SelectionBackColor.IsEmpty && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.SelectionBackColor.IsEmpty))
			{
				inheritedRowStyle.SelectionBackColor = rowsDefaultCellStyle.SelectionBackColor;
			}
			else if (rowIndex % 2 == 1 && !alternatingRowsDefaultCellStyle.SelectionBackColor.IsEmpty)
			{
				inheritedRowStyle.SelectionBackColor = alternatingRowsDefaultCellStyle.SelectionBackColor;
			}
			else
			{
				inheritedRowStyle.SelectionBackColor = defaultCellStyle.SelectionBackColor;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.SelectionForeColor.IsEmpty)
			{
				inheritedRowStyle.SelectionForeColor = dataGridViewCellStyle.SelectionForeColor;
			}
			else if (!rowsDefaultCellStyle.SelectionForeColor.IsEmpty && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.SelectionForeColor.IsEmpty))
			{
				inheritedRowStyle.SelectionForeColor = rowsDefaultCellStyle.SelectionForeColor;
			}
			else if (rowIndex % 2 == 1 && !alternatingRowsDefaultCellStyle.SelectionForeColor.IsEmpty)
			{
				inheritedRowStyle.SelectionForeColor = alternatingRowsDefaultCellStyle.SelectionForeColor;
			}
			else
			{
				inheritedRowStyle.SelectionForeColor = defaultCellStyle.SelectionForeColor;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Font != null)
			{
				inheritedRowStyle.Font = dataGridViewCellStyle.Font;
			}
			else if (rowsDefaultCellStyle.Font != null && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.Font == null))
			{
				inheritedRowStyle.Font = rowsDefaultCellStyle.Font;
			}
			else if (rowIndex % 2 == 1 && alternatingRowsDefaultCellStyle.Font != null)
			{
				inheritedRowStyle.Font = alternatingRowsDefaultCellStyle.Font;
			}
			else
			{
				inheritedRowStyle.Font = defaultCellStyle.Font;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsNullValueDefault)
			{
				inheritedRowStyle.NullValue = dataGridViewCellStyle.NullValue;
			}
			else if (!rowsDefaultCellStyle.IsNullValueDefault && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.IsNullValueDefault))
			{
				inheritedRowStyle.NullValue = rowsDefaultCellStyle.NullValue;
			}
			else if (rowIndex % 2 == 1 && !alternatingRowsDefaultCellStyle.IsNullValueDefault)
			{
				inheritedRowStyle.NullValue = alternatingRowsDefaultCellStyle.NullValue;
			}
			else
			{
				inheritedRowStyle.NullValue = defaultCellStyle.NullValue;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsDataSourceNullValueDefault)
			{
				inheritedRowStyle.DataSourceNullValue = dataGridViewCellStyle.DataSourceNullValue;
			}
			else if (!rowsDefaultCellStyle.IsDataSourceNullValueDefault && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.IsDataSourceNullValueDefault))
			{
				inheritedRowStyle.DataSourceNullValue = rowsDefaultCellStyle.DataSourceNullValue;
			}
			else if (rowIndex % 2 == 1 && !alternatingRowsDefaultCellStyle.IsDataSourceNullValueDefault)
			{
				inheritedRowStyle.DataSourceNullValue = alternatingRowsDefaultCellStyle.DataSourceNullValue;
			}
			else
			{
				inheritedRowStyle.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Format.Length != 0)
			{
				inheritedRowStyle.Format = dataGridViewCellStyle.Format;
			}
			else if (rowsDefaultCellStyle.Format.Length != 0 && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.Format.Length == 0))
			{
				inheritedRowStyle.Format = rowsDefaultCellStyle.Format;
			}
			else if (rowIndex % 2 == 1 && alternatingRowsDefaultCellStyle.Format.Length != 0)
			{
				inheritedRowStyle.Format = alternatingRowsDefaultCellStyle.Format;
			}
			else
			{
				inheritedRowStyle.Format = defaultCellStyle.Format;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsFormatProviderDefault)
			{
				inheritedRowStyle.FormatProvider = dataGridViewCellStyle.FormatProvider;
			}
			else if (!rowsDefaultCellStyle.IsFormatProviderDefault && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.IsFormatProviderDefault))
			{
				inheritedRowStyle.FormatProvider = rowsDefaultCellStyle.FormatProvider;
			}
			else if (rowIndex % 2 == 1 && !alternatingRowsDefaultCellStyle.IsFormatProviderDefault)
			{
				inheritedRowStyle.FormatProvider = alternatingRowsDefaultCellStyle.FormatProvider;
			}
			else
			{
				inheritedRowStyle.FormatProvider = defaultCellStyle.FormatProvider;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				inheritedRowStyle.AlignmentInternal = dataGridViewCellStyle.Alignment;
			}
			else if (rowsDefaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.Alignment == DataGridViewContentAlignment.NotSet))
			{
				inheritedRowStyle.AlignmentInternal = rowsDefaultCellStyle.Alignment;
			}
			else if (rowIndex % 2 == 1 && alternatingRowsDefaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				inheritedRowStyle.AlignmentInternal = alternatingRowsDefaultCellStyle.Alignment;
			}
			else
			{
				inheritedRowStyle.AlignmentInternal = defaultCellStyle.Alignment;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				inheritedRowStyle.WrapModeInternal = dataGridViewCellStyle.WrapMode;
			}
			else if (rowsDefaultCellStyle.WrapMode != DataGridViewTriState.NotSet && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.WrapMode == DataGridViewTriState.NotSet))
			{
				inheritedRowStyle.WrapModeInternal = rowsDefaultCellStyle.WrapMode;
			}
			else if (rowIndex % 2 == 1 && alternatingRowsDefaultCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				inheritedRowStyle.WrapModeInternal = alternatingRowsDefaultCellStyle.WrapMode;
			}
			else
			{
				inheritedRowStyle.WrapModeInternal = defaultCellStyle.WrapMode;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Tag != null)
			{
				inheritedRowStyle.Tag = dataGridViewCellStyle.Tag;
			}
			else if (rowsDefaultCellStyle.Tag != null && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.Tag == null))
			{
				inheritedRowStyle.Tag = rowsDefaultCellStyle.Tag;
			}
			else if (rowIndex % 2 == 1 && alternatingRowsDefaultCellStyle.Tag != null)
			{
				inheritedRowStyle.Tag = alternatingRowsDefaultCellStyle.Tag;
			}
			else
			{
				inheritedRowStyle.Tag = defaultCellStyle.Tag;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Padding != Padding.Empty)
			{
				inheritedRowStyle.PaddingInternal = dataGridViewCellStyle.Padding;
				return;
			}
			if (rowsDefaultCellStyle.Padding != Padding.Empty && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.Padding == Padding.Empty))
			{
				inheritedRowStyle.PaddingInternal = rowsDefaultCellStyle.Padding;
				return;
			}
			if (rowIndex % 2 == 1 && alternatingRowsDefaultCellStyle.Padding != Padding.Empty)
			{
				inheritedRowStyle.PaddingInternal = alternatingRowsDefaultCellStyle.Padding;
				return;
			}
			inheritedRowStyle.PaddingInternal = defaultCellStyle.Padding;
		}

		/// <summary>Creates an exact copy of this row.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		// Token: 0x060021F0 RID: 8688 RVA: 0x000A0ECC File Offset: 0x0009F0CC
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewRow dataGridViewRow;
			if (type == DataGridViewRow.rowType)
			{
				dataGridViewRow = new DataGridViewRow();
			}
			else
			{
				dataGridViewRow = (DataGridViewRow)Activator.CreateInstance(type);
			}
			if (dataGridViewRow != null)
			{
				base.CloneInternal(dataGridViewRow);
				if (this.HasErrorText)
				{
					dataGridViewRow.ErrorText = this.ErrorTextInternal;
				}
				if (base.HasHeaderCell)
				{
					dataGridViewRow.HeaderCell = (DataGridViewRowHeaderCell)this.HeaderCell.Clone();
				}
				dataGridViewRow.CloneCells(this);
			}
			return dataGridViewRow;
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x000A0F48 File Offset: 0x0009F148
		private void CloneCells(DataGridViewRow rowTemplate)
		{
			int count = rowTemplate.Cells.Count;
			if (count > 0)
			{
				DataGridViewCell[] array = new DataGridViewCell[count];
				for (int i = 0; i < count; i++)
				{
					DataGridViewCell dataGridViewCell = rowTemplate.Cells[i];
					DataGridViewCell dataGridViewCell2 = (DataGridViewCell)dataGridViewCell.Clone();
					array[i] = dataGridViewCell2;
				}
				this.Cells.AddRange(array);
			}
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		// Token: 0x060021F2 RID: 8690 RVA: 0x000A0FA3 File Offset: 0x0009F1A3
		protected virtual AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewRow.DataGridViewRowAccessibleObject(this);
		}

		/// <summary>Clears the existing cells and sets their template according to the supplied <see cref="T:System.Windows.Forms.DataGridView" /> template.</summary>
		/// <param name="dataGridView">A <see cref="T:System.Windows.Forms.DataGridView" /> that acts as a template for cell styles.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridView" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A row that already belongs to the <see cref="T:System.Windows.Forms.DataGridView" /> was added.  
		///  -or-  
		///  A column that has no cell template was added.</exception>
		// Token: 0x060021F3 RID: 8691 RVA: 0x000A0FAC File Offset: 0x0009F1AC
		public void CreateCells(DataGridView dataGridView)
		{
			if (dataGridView == null)
			{
				throw new ArgumentNullException("dataGridView");
			}
			if (base.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_RowAlreadyBelongsToDataGridView"));
			}
			DataGridViewCellCollection cells = this.Cells;
			cells.Clear();
			DataGridViewColumnCollection columns = dataGridView.Columns;
			foreach (object obj in columns)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
				if (dataGridViewColumn.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_AColumnHasNoCellTemplate"));
				}
				DataGridViewCell dataGridViewCell = (DataGridViewCell)dataGridViewColumn.CellTemplate.Clone();
				cells.Add(dataGridViewCell);
			}
		}

		/// <summary>Clears the existing cells and sets their template and values.</summary>
		/// <param name="dataGridView">A <see cref="T:System.Windows.Forms.DataGridView" /> that acts as a template for cell styles.</param>
		/// <param name="values">An array of objects that initialize the reset cells.</param>
		/// <exception cref="T:System.ArgumentNullException">Either of the parameters is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">A row that already belongs to the <see cref="T:System.Windows.Forms.DataGridView" /> was added.  
		///  -or-  
		///  A column that has no cell template was added.</exception>
		// Token: 0x060021F4 RID: 8692 RVA: 0x000A106C File Offset: 0x0009F26C
		public void CreateCells(DataGridView dataGridView, params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this.CreateCells(dataGridView);
			this.SetValuesInternal(values);
		}

		/// <summary>Constructs a new collection of cells based on this row.</summary>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />.</returns>
		// Token: 0x060021F5 RID: 8693 RVA: 0x000A108B File Offset: 0x0009F28B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual DataGridViewCellCollection CreateCellsInstance()
		{
			return new DataGridViewCellCollection(this);
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x000A1094 File Offset: 0x0009F294
		internal void DetachFromDataGridView()
		{
			if (base.DataGridView != null)
			{
				base.DataGridViewInternal = null;
				base.IndexInternal = -1;
				if (base.HasHeaderCell)
				{
					this.HeaderCell.DataGridViewInternal = null;
				}
				foreach (object obj in this.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					dataGridViewCell.DataGridViewInternal = null;
					if (dataGridViewCell.Selected)
					{
						dataGridViewCell.SelectedInternal = false;
					}
				}
				if (this.Selected)
				{
					base.SelectedInternal = false;
				}
			}
		}

		/// <summary>Draws a focus rectangle around the specified bounds.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> used to paint the focus rectangle.</param>
		/// <param name="cellsPaintSelectionBackground">
		///   <see langword="true" /> to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of <paramref name="cellStyle" /> as the color of the focus rectangle; <see langword="false" /> to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> property of <paramref name="cellStyle" /> as the color of the focus rectangle.</param>
		/// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="cellStyle" /> is <see langword="null" />.</exception>
		// Token: 0x060021F7 RID: 8695 RVA: 0x000A1138 File Offset: 0x0009F338
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void DrawFocus(Graphics graphics, Rectangle clipBounds, Rectangle bounds, int rowIndex, DataGridViewElementStates rowState, DataGridViewCellStyle cellStyle, bool cellsPaintSelectionBackground)
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_RowDoesNotYetBelongToDataGridView"));
			}
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			Color color;
			if (cellsPaintSelectionBackground && (rowState & DataGridViewElementStates.Selected) != DataGridViewElementStates.None)
			{
				color = cellStyle.SelectionBackColor;
			}
			else
			{
				color = cellStyle.BackColor;
			}
			ControlPaint.DrawFocusRectangle(graphics, bounds, Color.Empty, color);
		}

		/// <summary>Gets the shortcut menu for the row.</summary>
		/// <param name="rowIndex">The index of the current row.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip" /> that belongs to the <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="rowIndex" /> is -1.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than zero or greater than or equal to the number of rows in the control minus one.</exception>
		// Token: 0x060021F8 RID: 8696 RVA: 0x000A11A4 File Offset: 0x0009F3A4
		public ContextMenuStrip GetContextMenuStrip(int rowIndex)
		{
			ContextMenuStrip contextMenuStrip = base.ContextMenuStripInternal;
			if (base.DataGridView != null)
			{
				if (rowIndex == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedRow"));
				}
				if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				if (base.DataGridView.VirtualMode || base.DataGridView.DataSource != null)
				{
					contextMenuStrip = base.DataGridView.OnRowContextMenuStripNeeded(rowIndex, contextMenuStrip);
				}
			}
			return contextMenuStrip;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000A121F File Offset: 0x0009F41F
		internal bool GetDisplayed(int rowIndex)
		{
			return (this.GetState(rowIndex) & DataGridViewElementStates.Displayed) > DataGridViewElementStates.None;
		}

		/// <summary>Gets the error text for the row at the specified index.</summary>
		/// <param name="rowIndex">The index of the row that contains the error.</param>
		/// <returns>A string that describes the error of the row at the specified index.</returns>
		/// <exception cref="T:System.InvalidOperationException">The row belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control and <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the control minus one.</exception>
		// Token: 0x060021FA RID: 8698 RVA: 0x000A1230 File Offset: 0x0009F430
		public string GetErrorText(int rowIndex)
		{
			string text = this.ErrorTextInternal;
			if (base.DataGridView != null)
			{
				if (rowIndex == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedRow"));
				}
				if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				if (string.IsNullOrEmpty(text) && base.DataGridView.DataSource != null && rowIndex != base.DataGridView.NewRowIndex)
				{
					text = base.DataGridView.DataConnection.GetError(rowIndex);
				}
				if (base.DataGridView.DataSource != null || base.DataGridView.VirtualMode)
				{
					text = base.DataGridView.OnRowErrorTextNeeded(rowIndex, text);
				}
			}
			return text;
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000A12E3 File Offset: 0x0009F4E3
		internal bool GetFrozen(int rowIndex)
		{
			return (this.GetState(rowIndex) & DataGridViewElementStates.Frozen) > DataGridViewElementStates.None;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000A12F4 File Offset: 0x0009F4F4
		internal int GetHeight(int rowIndex)
		{
			int num;
			int num2;
			base.GetHeightInfo(rowIndex, out num, out num2);
			return num;
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000A1310 File Offset: 0x0009F510
		internal int GetMinimumHeight(int rowIndex)
		{
			int num;
			int num2;
			base.GetHeightInfo(rowIndex, out num, out num2);
			return num2;
		}

		/// <summary>Calculates the ideal height of the specified row based on the specified criteria.</summary>
		/// <param name="rowIndex">The index of the row whose preferred height is calculated.</param>
		/// <param name="autoSizeRowMode">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode" /> that specifies an automatic sizing mode.</param>
		/// <param name="fixedWidth">
		///   <see langword="true" /> to calculate the preferred height for a fixed cell width; otherwise, <see langword="false" />.</param>
		/// <returns>The ideal height of the row, in pixels.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="autoSizeRowMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode" /> value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="rowIndex" /> is not in the valid range of 0 to the number of rows in the control minus 1.</exception>
		// Token: 0x060021FE RID: 8702 RVA: 0x000A132C File Offset: 0x0009F52C
		public virtual int GetPreferredHeight(int rowIndex, DataGridViewAutoSizeRowMode autoSizeRowMode, bool fixedWidth)
		{
			if ((autoSizeRowMode & (DataGridViewAutoSizeRowMode)(-4)) != (DataGridViewAutoSizeRowMode)0)
			{
				throw new InvalidEnumArgumentException("autoSizeRowMode", (int)autoSizeRowMode, typeof(DataGridViewAutoSizeRowMode));
			}
			if (base.DataGridView != null && (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count))
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (base.DataGridView == null)
			{
				return -1;
			}
			int num = 0;
			if (base.DataGridView.RowHeadersVisible && (autoSizeRowMode & DataGridViewAutoSizeRowMode.RowHeader) != (DataGridViewAutoSizeRowMode)0)
			{
				if (fixedWidth || base.DataGridView.RowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.EnableResizing || base.DataGridView.RowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.DisableResizing)
				{
					num = Math.Max(num, this.HeaderCell.GetPreferredHeight(rowIndex, base.DataGridView.RowHeadersWidth));
				}
				else
				{
					num = Math.Max(num, this.HeaderCell.GetPreferredSize(rowIndex).Height);
				}
			}
			if ((autoSizeRowMode & DataGridViewAutoSizeRowMode.AllCellsExceptHeader) != (DataGridViewAutoSizeRowMode)0)
			{
				foreach (object obj in this.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					DataGridViewColumn dataGridViewColumn = base.DataGridView.Columns[dataGridViewCell.ColumnIndex];
					if (dataGridViewColumn.Visible)
					{
						int num2;
						if (fixedWidth || (dataGridViewColumn.InheritedAutoSizeMode & (DataGridViewAutoSizeColumnMode)12) == DataGridViewAutoSizeColumnMode.NotSet)
						{
							num2 = dataGridViewCell.GetPreferredHeight(rowIndex, dataGridViewColumn.Width);
						}
						else
						{
							num2 = dataGridViewCell.GetPreferredSize(rowIndex).Height;
						}
						if (num < num2)
						{
							num = num2;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x000A14A4 File Offset: 0x0009F6A4
		internal bool GetReadOnly(int rowIndex)
		{
			return (this.GetState(rowIndex) & DataGridViewElementStates.ReadOnly) != DataGridViewElementStates.None || (base.DataGridView != null && base.DataGridView.ReadOnly);
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x000A14C8 File Offset: 0x0009F6C8
		internal DataGridViewTriState GetResizable(int rowIndex)
		{
			if ((this.GetState(rowIndex) & DataGridViewElementStates.ResizableSet) != DataGridViewElementStates.None)
			{
				if ((this.GetState(rowIndex) & DataGridViewElementStates.Resizable) == DataGridViewElementStates.None)
				{
					return DataGridViewTriState.False;
				}
				return DataGridViewTriState.True;
			}
			else
			{
				if (base.DataGridView == null)
				{
					return DataGridViewTriState.NotSet;
				}
				if (!base.DataGridView.AllowUserToResizeRows)
				{
					return DataGridViewTriState.False;
				}
				return DataGridViewTriState.True;
			}
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x000A14FF File Offset: 0x0009F6FF
		internal bool GetSelected(int rowIndex)
		{
			return (this.GetState(rowIndex) & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
		}

		/// <summary>Returns a value indicating the current state of the row.</summary>
		/// <param name="rowIndex">The index of the row.</param>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the row state.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row has been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control, but the <paramref name="rowIndex" /> value is not in the valid range of 0 to the number of rows in the control minus 1.</exception>
		/// <exception cref="T:System.ArgumentException">The row is not a shared row, but the <paramref name="rowIndex" /> value does not match the row's <see cref="P:System.Windows.Forms.DataGridViewBand.Index" /> property value.  
		///  -or-  
		///  The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control, but the <paramref name="rowIndex" /> value does not match the row's <see cref="P:System.Windows.Forms.DataGridViewBand.Index" /> property value.</exception>
		// Token: 0x06002202 RID: 8706 RVA: 0x000A1510 File Offset: 0x0009F710
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual DataGridViewElementStates GetState(int rowIndex)
		{
			if (base.DataGridView != null && (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count))
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (base.DataGridView != null && base.DataGridView.Rows.SharedRow(rowIndex).Index == -1)
			{
				return base.DataGridView.Rows.GetRowState(rowIndex);
			}
			if (rowIndex != base.Index)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"rowIndex",
					rowIndex.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return base.State;
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000A15B6 File Offset: 0x0009F7B6
		internal bool GetVisible(int rowIndex)
		{
			return (this.GetState(rowIndex) & DataGridViewElementStates.Visible) > DataGridViewElementStates.None;
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000A15C5 File Offset: 0x0009F7C5
		internal void OnSharedStateChanged(int sharedRowIndex, DataGridViewElementStates elementState)
		{
			base.DataGridView.Rows.InvalidateCachedRowCount(elementState);
			base.DataGridView.Rows.InvalidateCachedRowsHeight(elementState);
			base.DataGridView.OnDataGridViewElementStateChanged(this, sharedRowIndex, elementState);
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000A15F7 File Offset: 0x0009F7F7
		internal void OnSharedStateChanging(int sharedRowIndex, DataGridViewElementStates elementState)
		{
			base.DataGridView.OnDataGridViewElementStateChanging(this, sharedRowIndex, elementState);
		}

		/// <summary>Paints the current row.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
		/// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
		/// <param name="isFirstDisplayedRow">
		///   <see langword="true" /> to indicate whether the current row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, <see langword="false" />.</param>
		/// <param name="isLastVisibleRow">
		///   <see langword="true" /> to indicate whether the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to <see langword="true" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the control minus one.</exception>
		// Token: 0x06002206 RID: 8710 RVA: 0x000A1608 File Offset: 0x0009F808
		protected internal virtual void Paint(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow)
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_RowDoesNotYetBelongToDataGridView"));
			}
			DataGridView dataGridView = base.DataGridView;
			DataGridViewRow dataGridViewRow = dataGridView.Rows.SharedRow(rowIndex);
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			this.BuildInheritedRowStyle(rowIndex, dataGridViewCellStyle);
			DataGridViewRowPrePaintEventArgs rowPrePaintEventArgs = dataGridView.RowPrePaintEventArgs;
			rowPrePaintEventArgs.SetProperties(graphics, clipBounds, rowBounds, rowIndex, rowState, dataGridViewRow.GetErrorText(rowIndex), dataGridViewCellStyle, isFirstDisplayedRow, isLastVisibleRow);
			dataGridView.OnRowPrePaint(rowPrePaintEventArgs);
			if (rowPrePaintEventArgs.Handled)
			{
				return;
			}
			DataGridViewPaintParts paintParts = rowPrePaintEventArgs.PaintParts;
			Rectangle clipBounds2 = rowPrePaintEventArgs.ClipBounds;
			this.PaintHeader(graphics, clipBounds2, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, paintParts);
			this.PaintCells(graphics, clipBounds2, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, paintParts);
			dataGridViewRow = dataGridView.Rows.SharedRow(rowIndex);
			this.BuildInheritedRowStyle(rowIndex, dataGridViewCellStyle);
			DataGridViewRowPostPaintEventArgs rowPostPaintEventArgs = dataGridView.RowPostPaintEventArgs;
			rowPostPaintEventArgs.SetProperties(graphics, clipBounds2, rowBounds, rowIndex, rowState, dataGridViewRow.GetErrorText(rowIndex), dataGridViewCellStyle, isFirstDisplayedRow, isLastVisibleRow);
			dataGridView.OnRowPostPaint(rowPostPaintEventArgs);
		}

		/// <summary>Paints the cells in the current row.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
		/// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
		/// <param name="isFirstDisplayedRow">
		///   <see langword="true" /> to indicate whether the current row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, <see langword="false" />.</param>
		/// <param name="isLastVisibleRow">
		///   <see langword="true" /> to indicate whether the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to <see langword="true" />; otherwise, <see langword="false" />.</param>
		/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values indicating the parts of the cells to paint.</param>
		/// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="paintParts" /> in not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values.</exception>
		// Token: 0x06002207 RID: 8711 RVA: 0x000A1704 File Offset: 0x0009F904
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void PaintCells(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, DataGridViewPaintParts paintParts)
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_RowDoesNotYetBelongToDataGridView"));
			}
			if (paintParts < DataGridViewPaintParts.None || paintParts > DataGridViewPaintParts.All)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewPaintPartsCombination", new object[] { "paintParts" }));
			}
			DataGridView dataGridView = base.DataGridView;
			Rectangle rectangle = rowBounds;
			int num = (dataGridView.RowHeadersVisible ? dataGridView.RowHeadersWidth : 0);
			bool flag = true;
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = new DataGridViewAdvancedBorderStyle();
			DataGridViewColumn dataGridViewColumn = dataGridView.Columns.GetFirstColumn(DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible);
			while (dataGridViewColumn != null)
			{
				DataGridViewCell dataGridViewCell = this.Cells[dataGridViewColumn.Index];
				rectangle.Width = dataGridViewColumn.Thickness;
				if (dataGridView.SingleVerticalBorderAdded && flag)
				{
					int num2 = rectangle.Width;
					rectangle.Width = num2 + 1;
				}
				if (dataGridView.RightToLeftInternal)
				{
					rectangle.X = rowBounds.Right - num - rectangle.Width;
				}
				else
				{
					rectangle.X = rowBounds.X + num;
				}
				DataGridViewColumn dataGridViewColumn2 = dataGridView.Columns.GetNextColumn(dataGridViewColumn, DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible, DataGridViewElementStates.None);
				if (clipBounds.IntersectsWith(rectangle))
				{
					DataGridViewElementStates dataGridViewElementStates = dataGridViewCell.CellStateFromColumnRowStates(rowState);
					if (base.Index != -1)
					{
						dataGridViewElementStates |= dataGridViewCell.State;
					}
					dataGridViewCell.GetInheritedStyle(dataGridViewCellStyle, rowIndex, true);
					DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle2 = dataGridViewCell.AdjustCellBorderStyle(dataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStyle, dataGridView.SingleVerticalBorderAdded, dataGridView.SingleHorizontalBorderAdded, flag, isFirstDisplayedRow);
					dataGridViewCell.PaintWork(graphics, clipBounds, rectangle, rowIndex, dataGridViewElementStates, dataGridViewCellStyle, dataGridViewAdvancedBorderStyle2, paintParts);
				}
				num += rectangle.Width;
				if (num >= rowBounds.Width)
				{
					break;
				}
				dataGridViewColumn = dataGridViewColumn2;
				flag = false;
			}
			Rectangle rectangle2 = rowBounds;
			if (num < rectangle2.Width && dataGridView.FirstDisplayedScrollingColumnIndex >= 0)
			{
				if (!dataGridView.RightToLeftInternal)
				{
					rectangle2.X -= dataGridView.FirstDisplayedScrollingColumnHiddenWidth;
				}
				rectangle2.Width += dataGridView.FirstDisplayedScrollingColumnHiddenWidth;
				Region region = null;
				if (dataGridView.FirstDisplayedScrollingColumnHiddenWidth > 0)
				{
					region = graphics.Clip;
					Rectangle rectangle3 = rowBounds;
					if (!dataGridView.RightToLeftInternal)
					{
						rectangle3.X += num;
					}
					rectangle3.Width -= num;
					graphics.SetClip(rectangle3);
				}
				dataGridViewColumn = dataGridView.Columns[dataGridView.FirstDisplayedScrollingColumnIndex];
				while (dataGridViewColumn != null)
				{
					DataGridViewCell dataGridViewCell = this.Cells[dataGridViewColumn.Index];
					rectangle.Width = dataGridViewColumn.Thickness;
					if (dataGridView.SingleVerticalBorderAdded && flag)
					{
						int num2 = rectangle.Width;
						rectangle.Width = num2 + 1;
					}
					if (dataGridView.RightToLeftInternal)
					{
						rectangle.X = rectangle2.Right - num - rectangle.Width;
					}
					else
					{
						rectangle.X = rectangle2.X + num;
					}
					DataGridViewColumn dataGridViewColumn2 = dataGridView.Columns.GetNextColumn(dataGridViewColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.None);
					if (clipBounds.IntersectsWith(rectangle))
					{
						DataGridViewElementStates dataGridViewElementStates = dataGridViewCell.CellStateFromColumnRowStates(rowState);
						if (base.Index != -1)
						{
							dataGridViewElementStates |= dataGridViewCell.State;
						}
						dataGridViewCell.GetInheritedStyle(dataGridViewCellStyle, rowIndex, true);
						DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle2 = dataGridViewCell.AdjustCellBorderStyle(dataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStyle, dataGridView.SingleVerticalBorderAdded, dataGridView.SingleHorizontalBorderAdded, flag, isFirstDisplayedRow);
						dataGridViewCell.PaintWork(graphics, clipBounds, rectangle, rowIndex, dataGridViewElementStates, dataGridViewCellStyle, dataGridViewAdvancedBorderStyle2, paintParts);
					}
					num += rectangle.Width;
					if (num >= rectangle2.Width)
					{
						break;
					}
					dataGridViewColumn = dataGridViewColumn2;
					flag = false;
				}
				if (region != null)
				{
					graphics.Clip = region;
					region.Dispose();
				}
			}
		}

		/// <summary>Paints the header cell of the current row.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
		/// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
		/// <param name="isFirstDisplayedRow">
		///   <see langword="true" /> to indicate that the current row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, <see langword="false" />.</param>
		/// <param name="isLastVisibleRow">
		///   <see langword="true" /> to indicate that the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to <see langword="true" />; otherwise, <see langword="false" />.</param>
		/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values indicating the parts of the cells to paint.</param>
		/// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="paintParts" /> in not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values.</exception>
		// Token: 0x06002208 RID: 8712 RVA: 0x000A1A74 File Offset: 0x0009FC74
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void PaintHeader(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, DataGridViewPaintParts paintParts)
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_RowDoesNotYetBelongToDataGridView"));
			}
			if (paintParts < DataGridViewPaintParts.None || paintParts > DataGridViewPaintParts.All)
			{
				throw new InvalidEnumArgumentException("paintParts", (int)paintParts, typeof(DataGridViewPaintParts));
			}
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView.RowHeadersVisible)
			{
				Rectangle rectangle = rowBounds;
				rectangle.Width = dataGridView.RowHeadersWidth;
				if (dataGridView.RightToLeftInternal)
				{
					rectangle.X = rowBounds.Right - rectangle.Width;
				}
				if (clipBounds.IntersectsWith(rectangle))
				{
					DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
					DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle = new DataGridViewAdvancedBorderStyle();
					this.BuildInheritedRowHeaderCellStyle(dataGridViewCellStyle);
					DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyle2 = this.AdjustRowHeaderBorderStyle(dataGridView.AdvancedRowHeadersBorderStyle, dataGridViewAdvancedBorderStyle, dataGridView.SingleVerticalBorderAdded, dataGridView.SingleHorizontalBorderAdded, isFirstDisplayedRow, isLastVisibleRow);
					this.HeaderCell.PaintWork(graphics, clipBounds, rectangle, rowIndex, rowState, dataGridViewCellStyle, dataGridViewAdvancedBorderStyle2, paintParts);
				}
			}
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000A1B50 File Offset: 0x0009FD50
		internal void SetReadOnlyCellCore(DataGridViewCell dataGridViewCell, bool readOnly)
		{
			if (this.ReadOnly && !readOnly)
			{
				foreach (object obj in this.Cells)
				{
					DataGridViewCell dataGridViewCell2 = (DataGridViewCell)obj;
					dataGridViewCell2.ReadOnlyInternal = true;
				}
				dataGridViewCell.ReadOnlyInternal = false;
				this.ReadOnly = false;
				return;
			}
			if (!this.ReadOnly && readOnly)
			{
				dataGridViewCell.ReadOnlyInternal = true;
			}
		}

		/// <summary>Sets the values of the row's cells.</summary>
		/// <param name="values">One or more objects that represent the cell values in the row.  
		///  -or-  
		///  An <see cref="T:System.Array" /> of <see cref="T:System.Object" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if all values have been set; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This method is called when the associated <see cref="T:System.Windows.Forms.DataGridView" /> is operating in virtual mode.  
		///  -or-  
		///  This row is a shared row.</exception>
		// Token: 0x0600220A RID: 8714 RVA: 0x000A1BD8 File Offset: 0x0009FDD8
		public bool SetValues(params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (base.DataGridView != null)
			{
				if (base.DataGridView.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationInVirtualMode"));
				}
				if (base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedRow"));
				}
			}
			return this.SetValuesInternal(values);
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x000A1C38 File Offset: 0x0009FE38
		internal bool SetValuesInternal(params object[] values)
		{
			bool flag = true;
			DataGridViewCellCollection cells = this.Cells;
			int count = cells.Count;
			int num = 0;
			while (num < cells.Count && num != values.Length)
			{
				if (!cells[num].SetValueInternal(base.Index, values[num]))
				{
					flag = false;
				}
				num++;
			}
			return flag && values.Length <= count;
		}

		/// <summary>Gets a human-readable string that describes the row.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes this row.</returns>
		// Token: 0x0600220C RID: 8716 RVA: 0x000A1C94 File Offset: 0x0009FE94
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(36);
			stringBuilder.Append("DataGridViewRow { Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000E15 RID: 3605
		private static Type rowType = typeof(DataGridViewRow);

		// Token: 0x04000E16 RID: 3606
		private static readonly int PropRowErrorText = PropertyStore.CreateKey();

		// Token: 0x04000E17 RID: 3607
		private static readonly int PropRowAccessibilityObject = PropertyStore.CreateKey();

		// Token: 0x04000E18 RID: 3608
		private const DataGridViewAutoSizeRowCriteriaInternal invalidDataGridViewAutoSizeRowCriteriaInternalMask = ~(DataGridViewAutoSizeRowCriteriaInternal.Header | DataGridViewAutoSizeRowCriteriaInternal.AllColumns);

		// Token: 0x04000E19 RID: 3609
		internal const int defaultMinRowThickness = 3;

		// Token: 0x04000E1A RID: 3610
		private DataGridViewCellCollection rowCells;

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewRow" /> to accessibility client applications.</summary>
		// Token: 0x02000673 RID: 1651
		[ComVisible(true)]
		protected class DataGridViewRowAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> class without setting the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property.</summary>
			// Token: 0x0600666A RID: 26218 RVA: 0x001772AE File Offset: 0x001754AE
			public DataGridViewRowAccessibleObject()
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> class, setting the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property to the specified <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /></param>
			// Token: 0x0600666B RID: 26219 RVA: 0x0017E307 File Offset: 0x0017C507
			public DataGridViewRowAccessibleObject(DataGridViewRow owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the location and size of the accessible object.</summary>
			/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the accessible object.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x17001649 RID: 5705
			// (get) Token: 0x0600666C RID: 26220 RVA: 0x0017E318 File Offset: 0x0017C518
			public override Rectangle Bounds
			{
				get
				{
					if (this.owner == null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
					}
					Rectangle rectangle;
					if (this.owner.Index < this.owner.DataGridView.FirstDisplayedScrollingRowIndex)
					{
						int rowCount = this.owner.DataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible, 0, this.owner.Index);
						rectangle = this.ParentPrivate.GetChild(rowCount + 1 + 1).Bounds;
						rectangle.Y -= this.owner.Height;
						rectangle.Height = this.owner.Height;
					}
					else if (this.owner.Index >= this.owner.DataGridView.FirstDisplayedScrollingRowIndex && this.owner.Index < this.owner.DataGridView.FirstDisplayedScrollingRowIndex + this.owner.DataGridView.DisplayedRowCount(true))
					{
						rectangle = this.owner.DataGridView.GetRowDisplayRectangle(this.owner.Index, false);
						rectangle = this.owner.DataGridView.RectangleToScreen(rectangle);
					}
					else
					{
						int num = this.owner.DataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible, 0, this.owner.Index);
						if (!this.owner.DataGridView.Rows[0].Visible)
						{
							num--;
						}
						if (!this.owner.DataGridView.ColumnHeadersVisible)
						{
							num--;
						}
						rectangle = this.ParentPrivate.GetChild(num).Bounds;
						rectangle.Y += rectangle.Height;
						rectangle.Height = this.owner.Height;
					}
					return rectangle;
				}
			}

			/// <summary>Gets the name of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
			/// <returns>The name of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x1700164A RID: 5706
			// (get) Token: 0x0600666D RID: 26221 RVA: 0x0017E4DC File Offset: 0x0017C6DC
			public override string Name
			{
				get
				{
					if (this.owner == null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
					}
					return SR.GetString("DataGridView_AccRowName", new object[] { (AccessibilityImprovements.Level5 ? ((this.owner.DataGridView != null && this.owner.Visible) ? this.owner.DataGridView.Rows.GetVisibleIndex(this.owner) : (-1)) : this.owner.Index).ToString(CultureInfo.CurrentCulture) });
				}
			}

			/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridViewRow" /> to which this <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> applies.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that owns this <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">When setting this property, the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property has already been set.</exception>
			// Token: 0x1700164B RID: 5707
			// (get) Token: 0x0600666E RID: 26222 RVA: 0x0017E56D File Offset: 0x0017C76D
			// (set) Token: 0x0600666F RID: 26223 RVA: 0x0017E575 File Offset: 0x0017C775
			public DataGridViewRow Owner
			{
				get
				{
					return this.owner;
				}
				set
				{
					if (this.owner != null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerAlreadySet"));
					}
					this.owner = value;
				}
			}

			/// <summary>Gets the parent of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.DataGridView.DataGridViewAccessibleObject" /> that belongs to the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x1700164C RID: 5708
			// (get) Token: 0x06006670 RID: 26224 RVA: 0x0017E596 File Offset: 0x0017C796
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.ParentPrivate;
				}
			}

			// Token: 0x1700164D RID: 5709
			// (get) Token: 0x06006671 RID: 26225 RVA: 0x0017E59E File Offset: 0x0017C79E
			private AccessibleObject ParentPrivate
			{
				get
				{
					if (this.owner == null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
					}
					return this.owner.DataGridView.AccessibilityObject;
				}
			}

			/// <summary>Gets the role of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.Row" /> value.</returns>
			// Token: 0x1700164E RID: 5710
			// (get) Token: 0x06006672 RID: 26226 RVA: 0x00177DCE File Offset: 0x00175FCE
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Row;
				}
			}

			// Token: 0x1700164F RID: 5711
			// (get) Token: 0x06006673 RID: 26227 RVA: 0x0017E5C8 File Offset: 0x0017C7C8
			internal override int[] RuntimeId
			{
				get
				{
					if (AccessibilityImprovements.Level3 && this.runtimeId == null)
					{
						this.runtimeId = new int[3];
						this.runtimeId[0] = 42;
						this.runtimeId[1] = this.Parent.GetHashCode();
						this.runtimeId[2] = this.GetHashCode();
					}
					return this.runtimeId;
				}
			}

			// Token: 0x17001650 RID: 5712
			// (get) Token: 0x06006674 RID: 26228 RVA: 0x0017E621 File Offset: 0x0017C821
			private AccessibleObject SelectedCellsAccessibilityObject
			{
				get
				{
					if (this.owner == null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
					}
					if (this.selectedCellsAccessibilityObject == null)
					{
						this.selectedCellsAccessibilityObject = new DataGridViewRow.DataGridViewSelectedRowCellsAccessibleObject(this.owner);
					}
					return this.selectedCellsAccessibilityObject;
				}
			}

			/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
			/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates" /> values. The default is the bitwise combination of the <see cref="F:System.Windows.Forms.AccessibleStates.Selectable" /> and <see cref="F:System.Windows.Forms.AccessibleStates.Focusable" /> values.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x17001651 RID: 5713
			// (get) Token: 0x06006675 RID: 26229 RVA: 0x0017E65C File Offset: 0x0017C85C
			public override AccessibleStates State
			{
				get
				{
					if (this.owner == null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
					}
					AccessibleStates accessibleStates = AccessibleStates.Selectable;
					bool flag = true;
					if (this.owner.Selected)
					{
						flag = true;
					}
					else
					{
						for (int i = 0; i < this.owner.Cells.Count; i++)
						{
							if (!this.owner.Cells[i].Selected)
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						accessibleStates |= AccessibleStates.Selected;
					}
					if (!this.owner.DataGridView.GetRowDisplayRectangle(this.owner.Index, true).IntersectsWith(this.owner.DataGridView.ClientRectangle))
					{
						accessibleStates |= AccessibleStates.Offscreen;
					}
					return accessibleStates;
				}
			}

			/// <summary>Gets the value of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
			/// <returns>The value of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x17001652 RID: 5714
			// (get) Token: 0x06006676 RID: 26230 RVA: 0x0017E718 File Offset: 0x0017C918
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (this.owner == null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
					}
					if (this.owner.DataGridView.AllowUserToAddRows && this.owner.Index == this.owner.DataGridView.NewRowIndex)
					{
						return SR.GetString("DataGridView_AccRowCreateNew");
					}
					StringBuilder stringBuilder = new StringBuilder(1024);
					int childCount = this.GetChildCount();
					int num = (this.owner.DataGridView.RowHeadersVisible ? 1 : 0);
					for (int i = num; i < childCount; i++)
					{
						AccessibleObject child = this.GetChild(i);
						if (child != null)
						{
							stringBuilder.Append(child.Value);
						}
						if (i != childCount - 1)
						{
							stringBuilder.Append(";");
						}
					}
					return stringBuilder.ToString();
				}
			}

			// Token: 0x17001653 RID: 5715
			// (get) Token: 0x06006677 RID: 26231 RVA: 0x0017E7E0 File Offset: 0x0017C9E0
			private int VisibleIndex
			{
				get
				{
					DataGridViewRow dataGridViewRow = this.owner;
					if (((dataGridViewRow != null) ? dataGridViewRow.DataGridView : null) == null)
					{
						return -1;
					}
					if (!this.owner.DataGridView.ColumnHeadersVisible)
					{
						return this.owner.DataGridView.Rows.GetVisibleIndex(this.owner);
					}
					return this.owner.DataGridView.Rows.GetVisibleIndex(this.owner) + 1;
				}
			}

			/// <summary>Returns the accessible child corresponding to the specified index.</summary>
			/// <param name="index">The zero-based index of the accessible child.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> that represents the <see cref="T:System.Windows.Forms.DataGridViewCell" /> corresponding to the specified index.</returns>
			/// <exception cref="T:System.InvalidOperationException">
			///   <paramref name="index" /> is less than 0.  
			/// -or-  
			/// The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x06006678 RID: 26232 RVA: 0x0017E850 File Offset: 0x0017CA50
			public override AccessibleObject GetChild(int index)
			{
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
				if (AccessibilityImprovements.Level5 && (this.owner.DataGridView == null || index > this.GetChildCount() - 1))
				{
					return null;
				}
				if (index == 0 && this.owner.DataGridView.RowHeadersVisible)
				{
					return this.owner.HeaderCell.AccessibilityObject;
				}
				if (this.owner.DataGridView.RowHeadersVisible)
				{
					index--;
				}
				int num = this.owner.DataGridView.Columns.ActualDisplayIndexToColumnIndex(index, DataGridViewElementStates.Visible);
				return this.owner.Cells[num].AccessibilityObject;
			}

			/// <summary>Returns the number of children belonging to the accessible object.</summary>
			/// <returns>The number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> corresponds to the number of visible columns in the <see cref="T:System.Windows.Forms.DataGridView" />. If the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersVisible" /> property is <see langword="true" />, the <see cref="M:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.GetChildCount" /> method includes the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" /> in the count of child accessible objects.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x06006679 RID: 26233 RVA: 0x0017E914 File Offset: 0x0017CB14
			public override int GetChildCount()
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
				int num = this.owner.DataGridView.Columns.GetColumnCount(DataGridViewElementStates.Visible);
				if (this.owner.DataGridView.RowHeadersVisible)
				{
					num++;
				}
				return num;
			}

			/// <summary>Gets an accessible object that represents the currently selected <see cref="T:System.Windows.Forms.DataGridViewCell" /> objects.</summary>
			/// <returns>An accessible object that represents the currently selected <see cref="T:System.Windows.Forms.DataGridViewCell" /> objects in the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x0600667A RID: 26234 RVA: 0x0017E968 File Offset: 0x0017CB68
			public override AccessibleObject GetSelected()
			{
				return this.SelectedCellsAccessibilityObject;
			}

			/// <summary>Returns the accessible object that has keyboard focus.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> if the cell indicated by the <see cref="P:System.Windows.Forms.DataGridView.CurrentCell" /> property has keyboard focus and is in the current <see cref="T:System.Windows.Forms.DataGridViewRow" />; otherwise, <see langword="null" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x0600667B RID: 26235 RVA: 0x0017E970 File Offset: 0x0017CB70
			public override AccessibleObject GetFocused()
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
				if (this.owner.DataGridView.Focused && this.owner.DataGridView.CurrentCell != null && this.owner.DataGridView.CurrentCell.RowIndex == this.owner.Index)
				{
					return this.owner.DataGridView.CurrentCell.AccessibilityObject;
				}
				return null;
			}

			/// <summary>Navigates to another accessible object.</summary>
			/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents an object in the specified direction.</returns>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x0600667C RID: 26236 RVA: 0x0017E9F4 File Offset: 0x0017CBF4
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
				switch (navigationDirection)
				{
				case AccessibleNavigation.Up:
				case AccessibleNavigation.Previous:
					if (this.owner.Index != this.owner.DataGridView.Rows.GetFirstRow(DataGridViewElementStates.Visible))
					{
						if (AccessibilityImprovements.Level5)
						{
							return this.owner.DataGridView.AccessibilityObject.GetChild(this.VisibleIndex - 1);
						}
						int previousRow = this.owner.DataGridView.Rows.GetPreviousRow(this.owner.Index, DataGridViewElementStates.Visible);
						int rowCount = this.owner.DataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible, 0, previousRow);
						if (this.owner.DataGridView.ColumnHeadersVisible)
						{
							return this.owner.DataGridView.AccessibilityObject.GetChild(rowCount + 1);
						}
						return this.owner.DataGridView.AccessibilityObject.GetChild(rowCount);
					}
					else
					{
						if (this.owner.DataGridView.ColumnHeadersVisible)
						{
							return this.ParentPrivate.GetChild(0);
						}
						return null;
					}
					break;
				case AccessibleNavigation.Down:
				case AccessibleNavigation.Next:
					if (this.owner.Index == this.owner.DataGridView.Rows.GetLastRow(DataGridViewElementStates.Visible))
					{
						return null;
					}
					if (AccessibilityImprovements.Level5)
					{
						int visibleIndex = this.VisibleIndex;
						if (visibleIndex >= 0)
						{
							return this.owner.DataGridView.AccessibilityObject.GetChild(visibleIndex + 1);
						}
						return null;
					}
					else
					{
						int nextRow = this.owner.DataGridView.Rows.GetNextRow(this.owner.Index, DataGridViewElementStates.Visible);
						int rowCount2 = this.owner.DataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible, 0, nextRow);
						if (this.owner.DataGridView.ColumnHeadersVisible)
						{
							return this.owner.DataGridView.AccessibilityObject.GetChild(rowCount2 + 1);
						}
						return this.owner.DataGridView.AccessibilityObject.GetChild(rowCount2);
					}
					break;
				case AccessibleNavigation.FirstChild:
					if (this.GetChildCount() == 0)
					{
						return null;
					}
					return this.GetChild(0);
				case AccessibleNavigation.LastChild:
				{
					int childCount = this.GetChildCount();
					if (childCount == 0)
					{
						return null;
					}
					return this.GetChild(childCount - 1);
				}
				}
				return null;
			}

			/// <summary>Modifies the selection or moves the keyboard focus of the accessible object.</summary>
			/// <param name="flags">One of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values.</param>
			/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is <see langword="null" />.</exception>
			// Token: 0x0600667D RID: 26237 RVA: 0x0017EC38 File Offset: 0x0017CE38
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
				DataGridView dataGridView = this.owner.DataGridView;
				if (dataGridView == null)
				{
					return;
				}
				if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
				{
					dataGridView.FocusInternal();
				}
				if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection && this.owner.Cells.Count > 0)
				{
					if (dataGridView.CurrentCell != null && dataGridView.CurrentCell.OwningColumn != null)
					{
						dataGridView.CurrentCell = this.owner.Cells[dataGridView.CurrentCell.OwningColumn.Index];
					}
					else
					{
						int index = dataGridView.Columns.GetFirstColumn(DataGridViewElementStates.Visible).Index;
						if (index > -1)
						{
							dataGridView.CurrentCell = this.owner.Cells[index];
						}
					}
				}
				if ((flags & AccessibleSelection.AddSelection) == AccessibleSelection.AddSelection && (flags & AccessibleSelection.TakeSelection) == AccessibleSelection.None && (dataGridView.SelectionMode == DataGridViewSelectionMode.FullRowSelect || dataGridView.SelectionMode == DataGridViewSelectionMode.RowHeaderSelect))
				{
					this.owner.Selected = true;
				}
				if ((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection && (flags & (AccessibleSelection.TakeSelection | AccessibleSelection.AddSelection)) == AccessibleSelection.None)
				{
					this.owner.Selected = false;
				}
			}

			// Token: 0x0600667E RID: 26238 RVA: 0x0017ED44 File Offset: 0x0017CF44
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (this.Owner == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
				DataGridView dataGridView = this.Owner.DataGridView;
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return this.Parent;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
					return this.Navigate(AccessibleNavigation.Next);
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
					return this.Navigate(AccessibleNavigation.Previous);
				case UnsafeNativeMethods.NavigateDirection.FirstChild:
					return this.Navigate(AccessibleNavigation.FirstChild);
				case UnsafeNativeMethods.NavigateDirection.LastChild:
					return this.Navigate(AccessibleNavigation.LastChild);
				default:
					return null;
				}
			}

			// Token: 0x17001654 RID: 5716
			// (get) Token: 0x0600667F RID: 26239 RVA: 0x0017EDB9 File Offset: 0x0017CFB9
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					return this.Owner.DataGridView.AccessibilityObject;
				}
			}

			// Token: 0x06006680 RID: 26240 RVA: 0x00178E57 File Offset: 0x00177057
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId.Equals(10018);
			}

			// Token: 0x06006681 RID: 26241 RVA: 0x0017EDCC File Offset: 0x0017CFCC
			internal override object GetPropertyValue(int propertyId)
			{
				if (AccessibilityImprovements.Level3)
				{
					switch (propertyId)
					{
					case 30005:
						return this.Name;
					case 30006:
					case 30011:
					case 30012:
						goto IL_83;
					case 30007:
						return string.Empty;
					case 30008:
					case 30009:
						break;
					case 30010:
						return this.Owner.DataGridView.Enabled;
					case 30013:
						return this.Help ?? string.Empty;
					default:
						if (propertyId != 30019 && propertyId != 30022)
						{
							goto IL_83;
						}
						break;
					}
					return false;
				}
				IL_83:
				return base.GetPropertyValue(propertyId);
			}

			// Token: 0x04003A68 RID: 14952
			private int[] runtimeId;

			// Token: 0x04003A69 RID: 14953
			private DataGridViewRow owner;

			// Token: 0x04003A6A RID: 14954
			private DataGridViewRow.DataGridViewSelectedRowCellsAccessibleObject selectedCellsAccessibilityObject;
		}

		// Token: 0x02000674 RID: 1652
		private class DataGridViewSelectedRowCellsAccessibleObject : AccessibleObject
		{
			// Token: 0x06006682 RID: 26242 RVA: 0x0017EE63 File Offset: 0x0017D063
			internal DataGridViewSelectedRowCellsAccessibleObject(DataGridViewRow owner)
			{
				this.owner = owner;
			}

			// Token: 0x17001655 RID: 5717
			// (get) Token: 0x06006683 RID: 26243 RVA: 0x0017EE72 File Offset: 0x0017D072
			public override string Name
			{
				get
				{
					return SR.GetString("DataGridView_AccSelectedRowCellsName");
				}
			}

			// Token: 0x17001656 RID: 5718
			// (get) Token: 0x06006684 RID: 26244 RVA: 0x0017EE7E File Offset: 0x0017D07E
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.owner.AccessibilityObject;
				}
			}

			// Token: 0x17001657 RID: 5719
			// (get) Token: 0x06006685 RID: 26245 RVA: 0x0017B57C File Offset: 0x0017977C
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Grouping;
				}
			}

			// Token: 0x17001658 RID: 5720
			// (get) Token: 0x06006686 RID: 26246 RVA: 0x0017B580 File Offset: 0x00179780
			public override AccessibleStates State
			{
				get
				{
					return AccessibleStates.Selected | AccessibleStates.Selectable;
				}
			}

			// Token: 0x17001659 RID: 5721
			// (get) Token: 0x06006687 RID: 26247 RVA: 0x00016178 File Offset: 0x00014378
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.Name;
				}
			}

			// Token: 0x06006688 RID: 26248 RVA: 0x0017EE8C File Offset: 0x0017D08C
			public override AccessibleObject GetChild(int index)
			{
				if (index < this.GetChildCount())
				{
					int num = -1;
					for (int i = 1; i < this.owner.AccessibilityObject.GetChildCount(); i++)
					{
						if ((this.owner.AccessibilityObject.GetChild(i).State & AccessibleStates.Selected) == AccessibleStates.Selected)
						{
							num++;
						}
						if (num == index)
						{
							return this.owner.AccessibilityObject.GetChild(i);
						}
					}
					return null;
				}
				return null;
			}

			// Token: 0x06006689 RID: 26249 RVA: 0x0017EEF8 File Offset: 0x0017D0F8
			public override int GetChildCount()
			{
				int num = 0;
				for (int i = 1; i < this.owner.AccessibilityObject.GetChildCount(); i++)
				{
					if ((this.owner.AccessibilityObject.GetChild(i).State & AccessibleStates.Selected) == AccessibleStates.Selected)
					{
						num++;
					}
				}
				return num;
			}

			// Token: 0x0600668A RID: 26250 RVA: 0x00006A49 File Offset: 0x00004C49
			public override AccessibleObject GetSelected()
			{
				return this;
			}

			// Token: 0x0600668B RID: 26251 RVA: 0x0017EF44 File Offset: 0x0017D144
			public override AccessibleObject GetFocused()
			{
				if (this.owner.DataGridView.CurrentCell != null && this.owner.DataGridView.CurrentCell.Selected)
				{
					return this.owner.DataGridView.CurrentCell.AccessibilityObject;
				}
				return null;
			}

			// Token: 0x0600668C RID: 26252 RVA: 0x0017EF91 File Offset: 0x0017D191
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				if (navigationDirection != AccessibleNavigation.FirstChild)
				{
					if (navigationDirection != AccessibleNavigation.LastChild)
					{
						return null;
					}
					if (this.GetChildCount() > 0)
					{
						return this.GetChild(this.GetChildCount() - 1);
					}
					return null;
				}
				else
				{
					if (this.GetChildCount() > 0)
					{
						return this.GetChild(0);
					}
					return null;
				}
			}

			// Token: 0x04003A6B RID: 14955
			private DataGridViewRow owner;
		}
	}
}
