using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Represents a column of cells that contain links in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x02000204 RID: 516
	[ToolboxBitmap(typeof(DataGridViewLinkColumn), "DataGridViewLinkColumn.bmp")]
	public class DataGridViewLinkColumn : DataGridViewColumn
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewLinkColumn" /> class.</summary>
		// Token: 0x060021B3 RID: 8627 RVA: 0x0009F480 File Offset: 0x0009D680
		public DataGridViewLinkColumn()
			: base(new DataGridViewLinkCell())
		{
		}

		/// <summary>Gets or sets the color used to display an active link within cells in the column.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display a link that is being selected. The default value is the user's Internet Explorer setting for the color of links in the hover state.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x0009F48D File Offset: 0x0009D68D
		// (set) Token: 0x060021B5 RID: 8629 RVA: 0x0009F4B8 File Offset: 0x0009D6B8
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_LinkColumnActiveLinkColorDescr")]
		public Color ActiveLinkColor
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewLinkCell)this.CellTemplate).ActiveLinkColor;
			}
			set
			{
				if (!this.ActiveLinkColor.Equals(value))
				{
					((DataGridViewLinkCell)this.CellTemplate).ActiveLinkColorInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewLinkCell dataGridViewLinkCell = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
							if (dataGridViewLinkCell != null)
							{
								dataGridViewLinkCell.ActiveLinkColorInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x0009F558 File Offset: 0x0009D758
		private bool ShouldSerializeActiveLinkColor()
		{
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
			{
				return !this.ActiveLinkColor.Equals(SystemColors.HotTrack);
			}
			return !this.ActiveLinkColor.Equals(LinkUtilities.IEActiveLinkColor);
		}

		/// <summary>Gets or sets the template used to create new cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after. The default value is a new <see cref="T:System.Windows.Forms.DataGridViewLinkCell" /> instance.</returns>
		/// <exception cref="T:System.InvalidCastException">When setting this property to a value that is not of type <see cref="T:System.Windows.Forms.DataGridViewLinkCell" />.</exception>
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x00089219 File Offset: 0x00087419
		// (set) Token: 0x060021B8 RID: 8632 RVA: 0x0009F5B6 File Offset: 0x0009D7B6
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override DataGridViewCell CellTemplate
		{
			get
			{
				return base.CellTemplate;
			}
			set
			{
				if (value != null && !(value is DataGridViewLinkCell))
				{
					throw new InvalidCastException(SR.GetString("DataGridViewTypeColumn_WrongCellTemplateType", new object[] { "System.Windows.Forms.DataGridViewLinkCell" }));
				}
				base.CellTemplate = value;
			}
		}

		/// <summary>Gets or sets a value that represents the behavior of links within cells in the column.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.LinkBehavior" /> value indicating the link behavior. The default is <see cref="F:System.Windows.Forms.LinkBehavior.SystemDefault" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x0009F5E8 File Offset: 0x0009D7E8
		// (set) Token: 0x060021BA RID: 8634 RVA: 0x0009F614 File Offset: 0x0009D814
		[DefaultValue(LinkBehavior.SystemDefault)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_LinkColumnLinkBehaviorDescr")]
		public LinkBehavior LinkBehavior
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewLinkCell)this.CellTemplate).LinkBehavior;
			}
			set
			{
				if (!this.LinkBehavior.Equals(value))
				{
					((DataGridViewLinkCell)this.CellTemplate).LinkBehavior = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewLinkCell dataGridViewLinkCell = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
							if (dataGridViewLinkCell != null)
							{
								dataGridViewLinkCell.LinkBehaviorInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		/// <summary>Gets or sets the color used to display an unselected link within cells in the column.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to initially display a link. The default value is the user's Internet Explorer setting for the link color.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x0009F6B4 File Offset: 0x0009D8B4
		// (set) Token: 0x060021BC RID: 8636 RVA: 0x0009F6E0 File Offset: 0x0009D8E0
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_LinkColumnLinkColorDescr")]
		public Color LinkColor
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewLinkCell)this.CellTemplate).LinkColor;
			}
			set
			{
				if (!this.LinkColor.Equals(value))
				{
					((DataGridViewLinkCell)this.CellTemplate).LinkColorInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewLinkCell dataGridViewLinkCell = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
							if (dataGridViewLinkCell != null)
							{
								dataGridViewLinkCell.LinkColorInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x0009F780 File Offset: 0x0009D980
		private bool ShouldSerializeLinkColor()
		{
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
			{
				return !this.LinkColor.Equals(SystemColors.HotTrack);
			}
			return !this.LinkColor.Equals(LinkUtilities.IELinkColor);
		}

		/// <summary>Gets or sets the link text displayed in a column's cells if <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.UseColumnTextForLinkValue" /> is <see langword="true" />.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the link text.</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x0009F7DE File Offset: 0x0009D9DE
		// (set) Token: 0x060021BF RID: 8639 RVA: 0x0009F7E8 File Offset: 0x0009D9E8
		[DefaultValue(null)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_LinkColumnTextDescr")]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (!string.Equals(value, this.text, StringComparison.Ordinal))
				{
					this.text = value;
					if (base.DataGridView != null)
					{
						if (this.UseColumnTextForLinkValue)
						{
							base.DataGridView.OnColumnCommonChange(base.Index);
							return;
						}
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewLinkCell dataGridViewLinkCell = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
							if (dataGridViewLinkCell != null && dataGridViewLinkCell.UseColumnTextForLinkValue)
							{
								base.DataGridView.OnColumnCommonChange(base.Index);
								return;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the link changes color if it has been visited.</summary>
		/// <returns>
		///   <see langword="true" /> if the link changes color when it is selected; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x0009F8A2 File Offset: 0x0009DAA2
		// (set) Token: 0x060021C1 RID: 8641 RVA: 0x0009F8CC File Offset: 0x0009DACC
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_LinkColumnTrackVisitedStateDescr")]
		public bool TrackVisitedState
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewLinkCell)this.CellTemplate).TrackVisitedState;
			}
			set
			{
				if (this.TrackVisitedState != value)
				{
					((DataGridViewLinkCell)this.CellTemplate).TrackVisitedStateInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewLinkCell dataGridViewLinkCell = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
							if (dataGridViewLinkCell != null)
							{
								dataGridViewLinkCell.TrackVisitedStateInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.Text" /> property value is displayed as the link text.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.Text" /> property value is displayed as the link text; <see langword="false" /> if the cell <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValue" /> property value is displayed as the link text. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x0009F957 File Offset: 0x0009DB57
		// (set) Token: 0x060021C3 RID: 8643 RVA: 0x0009F984 File Offset: 0x0009DB84
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_LinkColumnUseColumnTextForLinkValueDescr")]
		public bool UseColumnTextForLinkValue
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewLinkCell)this.CellTemplate).UseColumnTextForLinkValue;
			}
			set
			{
				if (this.UseColumnTextForLinkValue != value)
				{
					((DataGridViewLinkCell)this.CellTemplate).UseColumnTextForLinkValueInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewLinkCell dataGridViewLinkCell = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
							if (dataGridViewLinkCell != null)
							{
								dataGridViewLinkCell.UseColumnTextForLinkValueInternal = value;
							}
						}
						base.DataGridView.OnColumnCommonChange(base.Index);
					}
				}
			}
		}

		/// <summary>Gets or sets the color used to display a link that has been previously visited.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display a link that has been visited. The default value is the user's Internet Explorer setting for the visited link color.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x0009FA0F File Offset: 0x0009DC0F
		// (set) Token: 0x060021C5 RID: 8645 RVA: 0x0009FA3C File Offset: 0x0009DC3C
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_LinkColumnVisitedLinkColorDescr")]
		public Color VisitedLinkColor
		{
			get
			{
				if (this.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewColumn_CellTemplateRequired"));
				}
				return ((DataGridViewLinkCell)this.CellTemplate).VisitedLinkColor;
			}
			set
			{
				if (!this.VisitedLinkColor.Equals(value))
				{
					((DataGridViewLinkCell)this.CellTemplate).VisitedLinkColorInternal = value;
					if (base.DataGridView != null)
					{
						DataGridViewRowCollection rows = base.DataGridView.Rows;
						int count = rows.Count;
						for (int i = 0; i < count; i++)
						{
							DataGridViewRow dataGridViewRow = rows.SharedRow(i);
							DataGridViewLinkCell dataGridViewLinkCell = dataGridViewRow.Cells[base.Index] as DataGridViewLinkCell;
							if (dataGridViewLinkCell != null)
							{
								dataGridViewLinkCell.VisitedLinkColorInternal = value;
							}
						}
						base.DataGridView.InvalidateColumn(base.Index);
					}
				}
			}
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0009FADC File Offset: 0x0009DCDC
		private bool ShouldSerializeVisitedLinkColor()
		{
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level2)
			{
				return !this.VisitedLinkColor.Equals(SystemColors.HotTrack);
			}
			return !this.VisitedLinkColor.Equals(LinkUtilities.IEVisitedLinkColor);
		}

		/// <summary>Creates an exact copy of this column.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewLinkColumn" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.CellTemplate" /> property is <see langword="null" />.</exception>
		// Token: 0x060021C7 RID: 8647 RVA: 0x0009FB3C File Offset: 0x0009DD3C
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewLinkColumn dataGridViewLinkColumn;
			if (type == DataGridViewLinkColumn.columnType)
			{
				dataGridViewLinkColumn = new DataGridViewLinkColumn();
			}
			else
			{
				dataGridViewLinkColumn = (DataGridViewLinkColumn)Activator.CreateInstance(type);
			}
			if (dataGridViewLinkColumn != null)
			{
				base.CloneInternal(dataGridViewLinkColumn);
				dataGridViewLinkColumn.Text = this.text;
			}
			return dataGridViewLinkColumn;
		}

		/// <summary>Gets a string that describes the column.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
		// Token: 0x060021C8 RID: 8648 RVA: 0x0009FB88 File Offset: 0x0009DD88
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("DataGridViewLinkColumn { Name=");
			stringBuilder.Append(base.Name);
			stringBuilder.Append(", Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000E09 RID: 3593
		private static Type columnType = typeof(DataGridViewLinkColumn);

		// Token: 0x04000E0A RID: 3594
		private string text;
	}
}
