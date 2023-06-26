using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents the table drawn by the <see cref="T:System.Windows.Forms.DataGrid" /> control at run time.</summary>
	// Token: 0x02000188 RID: 392
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	public class DataGridTableStyle : Component, IDataGridEditingService
	{
		/// <summary>Indicates whether sorting is allowed on the grid table when this <see cref="T:System.Windows.Forms.DataGridTableStyle" /> is used.</summary>
		/// <returns>
		///   <see langword="true" /> if sorting is allowed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x00054D40 File Offset: 0x00052F40
		// (set) Token: 0x0600176E RID: 5998 RVA: 0x00054D48 File Offset: 0x00052F48
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("DataGridAllowSortingDescr")]
		public bool AllowSorting
		{
			get
			{
				return this.allowSorting;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException(SR.GetString("DataGridDefaultTableSet", new object[] { "AllowSorting" }));
				}
				if (this.allowSorting != value)
				{
					this.allowSorting = value;
					this.OnAllowSortingChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.AllowSorting" /> property value changes.</summary>
		// Token: 0x140000F0 RID: 240
		// (add) Token: 0x0600176F RID: 5999 RVA: 0x00054D96 File Offset: 0x00052F96
		// (remove) Token: 0x06001770 RID: 6000 RVA: 0x00054DA9 File Offset: 0x00052FA9
		public event EventHandler AllowSortingChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventAllowSorting, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventAllowSorting, value);
			}
		}

		/// <summary>Gets or sets the background color of odd-numbered rows of the grid.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of odd-numbered rows. The default is <see cref="P:System.Drawing.SystemBrushes.Window" /></returns>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x00054DBC File Offset: 0x00052FBC
		// (set) Token: 0x06001772 RID: 6002 RVA: 0x00054DCC File Offset: 0x00052FCC
		[SRCategory("CatColors")]
		[SRDescription("DataGridAlternatingBackColorDescr")]
		public Color AlternatingBackColor
		{
			get
			{
				return this.alternatingBackBrush.Color;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException(SR.GetString("DataGridDefaultTableSet", new object[] { "AlternatingBackColor" }));
				}
				if (DataGrid.IsTransparentColor(value))
				{
					throw new ArgumentException(SR.GetString("DataGridTableStyleTransparentAlternatingBackColorNotAllowed"));
				}
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "AlternatingBackColor" }));
				}
				if (!this.alternatingBackBrush.Color.Equals(value))
				{
					this.alternatingBackBrush = new SolidBrush(value);
					this.InvalidateInside();
					this.OnAlternatingBackColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.AlternatingBackColor" /> value changes.</summary>
		// Token: 0x140000F1 RID: 241
		// (add) Token: 0x06001773 RID: 6003 RVA: 0x00054E7C File Offset: 0x0005307C
		// (remove) Token: 0x06001774 RID: 6004 RVA: 0x00054E8F File Offset: 0x0005308F
		public event EventHandler AlternatingBackColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventAlternatingBackColor, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventAlternatingBackColor, value);
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.AlternatingBackColor" /> property to its default value.</summary>
		// Token: 0x06001775 RID: 6005 RVA: 0x00054EA2 File Offset: 0x000530A2
		public void ResetAlternatingBackColor()
		{
			if (this.ShouldSerializeAlternatingBackColor())
			{
				this.AlternatingBackColor = DataGridTableStyle.DefaultAlternatingBackBrush.Color;
				this.InvalidateInside();
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.AlternatingBackColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001776 RID: 6006 RVA: 0x00054EC2 File Offset: 0x000530C2
		protected virtual bool ShouldSerializeAlternatingBackColor()
		{
			return !this.AlternatingBackBrush.Equals(DataGridTableStyle.DefaultAlternatingBackBrush);
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x00054ED7 File Offset: 0x000530D7
		internal SolidBrush AlternatingBackBrush
		{
			get
			{
				return this.alternatingBackBrush;
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.BackColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001778 RID: 6008 RVA: 0x00054EDF File Offset: 0x000530DF
		protected bool ShouldSerializeBackColor()
		{
			return !DataGridTableStyle.DefaultBackBrush.Equals(this.backBrush);
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.ForeColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001779 RID: 6009 RVA: 0x00054EF4 File Offset: 0x000530F4
		protected bool ShouldSerializeForeColor()
		{
			return !DataGridTableStyle.DefaultForeBrush.Equals(this.foreBrush);
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x00054F09 File Offset: 0x00053109
		internal SolidBrush BackBrush
		{
			get
			{
				return this.backBrush;
			}
		}

		/// <summary>Gets or sets the background color of even-numbered rows of the grid.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of odd-numbered rows.</returns>
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x00054F11 File Offset: 0x00053111
		// (set) Token: 0x0600177C RID: 6012 RVA: 0x00054F20 File Offset: 0x00053120
		[SRCategory("CatColors")]
		[SRDescription("ControlBackColorDescr")]
		public Color BackColor
		{
			get
			{
				return this.backBrush.Color;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException(SR.GetString("DataGridDefaultTableSet", new object[] { "BackColor" }));
				}
				if (DataGrid.IsTransparentColor(value))
				{
					throw new ArgumentException(SR.GetString("DataGridTableStyleTransparentBackColorNotAllowed"));
				}
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "BackColor" }));
				}
				if (!this.backBrush.Color.Equals(value))
				{
					this.backBrush = new SolidBrush(value);
					this.InvalidateInside();
					this.OnBackColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.BackColor" /> value changes.</summary>
		// Token: 0x140000F2 RID: 242
		// (add) Token: 0x0600177D RID: 6013 RVA: 0x00054FD0 File Offset: 0x000531D0
		// (remove) Token: 0x0600177E RID: 6014 RVA: 0x00054FE3 File Offset: 0x000531E3
		public event EventHandler BackColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventBackColor, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventBackColor, value);
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.BackColor" /> property to its default value.</summary>
		// Token: 0x0600177F RID: 6015 RVA: 0x00054FF6 File Offset: 0x000531F6
		public void ResetBackColor()
		{
			if (!this.backBrush.Equals(DataGridTableStyle.DefaultBackBrush))
			{
				this.BackColor = DataGridTableStyle.DefaultBackBrush.Color;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0005501C File Offset: 0x0005321C
		internal int BorderWidth
		{
			get
			{
				if (this.DataGrid == null)
				{
					return 0;
				}
				DataGridLineStyle dataGridLineStyle;
				int num;
				if (this.IsDefault)
				{
					dataGridLineStyle = this.DataGrid.GridLineStyle;
					num = this.DataGrid.GridLineWidth;
				}
				else
				{
					dataGridLineStyle = this.GridLineStyle;
					num = this.GridLineWidth;
				}
				if (dataGridLineStyle == DataGridLineStyle.None)
				{
					return 0;
				}
				return num;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x000445EA File Offset: 0x000427EA
		internal static SolidBrush DefaultAlternatingBackBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.Window;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x000445EA File Offset: 0x000427EA
		internal static SolidBrush DefaultBackBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.Window;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x000445F6 File Offset: 0x000427F6
		internal static SolidBrush DefaultForeBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.WindowText;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x0004460E File Offset: 0x0004280E
		private static SolidBrush DefaultGridLineBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.Control;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0004460E File Offset: 0x0004280E
		private static SolidBrush DefaultHeaderBackBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.Control;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x0004461A File Offset: 0x0004281A
		private static SolidBrush DefaultHeaderForeBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.ControlText;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x00044626 File Offset: 0x00042826
		private static Pen DefaultHeaderForePen
		{
			get
			{
				return new Pen(SystemColors.ControlText);
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x00044632 File Offset: 0x00042832
		private static SolidBrush DefaultLinkBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.HotTrack;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x000445D2 File Offset: 0x000427D2
		private static SolidBrush DefaultSelectionBackBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.ActiveCaption;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x000445DE File Offset: 0x000427DE
		private static SolidBrush DefaultSelectionForeBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.ActiveCaptionText;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0005506B File Offset: 0x0005326B
		// (set) Token: 0x0600178C RID: 6028 RVA: 0x00055074 File Offset: 0x00053274
		internal int FocusedRelation
		{
			get
			{
				return this.focusedRelation;
			}
			set
			{
				if (this.focusedRelation != value)
				{
					this.focusedRelation = value;
					if (this.focusedRelation == -1)
					{
						this.focusedTextWidth = 0;
						return;
					}
					Graphics graphics = this.DataGrid.CreateGraphicsInternal();
					this.focusedTextWidth = (int)Math.Ceiling((double)graphics.MeasureString((string)this.RelationsList[this.focusedRelation], this.DataGrid.LinkFont).Width);
					graphics.Dispose();
				}
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x000550F0 File Offset: 0x000532F0
		internal int FocusedTextWidth
		{
			get
			{
				return this.focusedTextWidth;
			}
		}

		/// <summary>Gets or sets the foreground color of the grid table.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the grid table.</returns>
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x000550F8 File Offset: 0x000532F8
		// (set) Token: 0x0600178F RID: 6031 RVA: 0x00055108 File Offset: 0x00053308
		[SRCategory("CatColors")]
		[SRDescription("ControlForeColorDescr")]
		public Color ForeColor
		{
			get
			{
				return this.foreBrush.Color;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException(SR.GetString("DataGridDefaultTableSet", new object[] { "ForeColor" }));
				}
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "BackColor" }));
				}
				if (!this.foreBrush.Color.Equals(value))
				{
					this.foreBrush = new SolidBrush(value);
					this.InvalidateInside();
					this.OnForeColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.ForeColor" /> value changes.</summary>
		// Token: 0x140000F3 RID: 243
		// (add) Token: 0x06001790 RID: 6032 RVA: 0x000551A0 File Offset: 0x000533A0
		// (remove) Token: 0x06001791 RID: 6033 RVA: 0x000551B3 File Offset: 0x000533B3
		public event EventHandler ForeColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventForeColor, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventForeColor, value);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x000551C6 File Offset: 0x000533C6
		internal SolidBrush ForeBrush
		{
			get
			{
				return this.foreBrush;
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.ForeColor" /> property to its default value.</summary>
		// Token: 0x06001793 RID: 6035 RVA: 0x000551CE File Offset: 0x000533CE
		public void ResetForeColor()
		{
			if (!this.foreBrush.Equals(DataGridTableStyle.DefaultForeBrush))
			{
				this.ForeColor = DataGridTableStyle.DefaultForeBrush.Color;
			}
		}

		/// <summary>Gets or sets the color of grid lines.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the grid line color.</returns>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x000551F2 File Offset: 0x000533F2
		// (set) Token: 0x06001795 RID: 6037 RVA: 0x00055200 File Offset: 0x00053400
		[SRCategory("CatColors")]
		[SRDescription("DataGridGridLineColorDescr")]
		public Color GridLineColor
		{
			get
			{
				return this.gridLineBrush.Color;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException(SR.GetString("DataGridDefaultTableSet", new object[] { "GridLineColor" }));
				}
				if (this.gridLineBrush.Color != value)
				{
					if (value.IsEmpty)
					{
						throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "GridLineColor" }));
					}
					this.gridLineBrush = new SolidBrush(value);
					this.OnGridLineColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.GridLineColor" /> value changes.</summary>
		// Token: 0x140000F4 RID: 244
		// (add) Token: 0x06001796 RID: 6038 RVA: 0x00055284 File Offset: 0x00053484
		// (remove) Token: 0x06001797 RID: 6039 RVA: 0x00055297 File Offset: 0x00053497
		public event EventHandler GridLineColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventGridLineColor, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventGridLineColor, value);
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.GridLineColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001798 RID: 6040 RVA: 0x000552AA File Offset: 0x000534AA
		protected virtual bool ShouldSerializeGridLineColor()
		{
			return !this.GridLineBrush.Equals(DataGridTableStyle.DefaultGridLineBrush);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.GridLineColor" /> property to its default value.</summary>
		// Token: 0x06001799 RID: 6041 RVA: 0x000552BF File Offset: 0x000534BF
		public void ResetGridLineColor()
		{
			if (this.ShouldSerializeGridLineColor())
			{
				this.GridLineColor = DataGridTableStyle.DefaultGridLineBrush.Color;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x000552D9 File Offset: 0x000534D9
		internal SolidBrush GridLineBrush
		{
			get
			{
				return this.gridLineBrush;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x000552E1 File Offset: 0x000534E1
		internal int GridLineWidth
		{
			get
			{
				if (this.GridLineStyle != DataGridLineStyle.Solid)
				{
					return 0;
				}
				return 1;
			}
		}

		/// <summary>Gets or sets the style of grid lines.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridLineStyle" /> values. The default is <see langword="DataGridLineStyle.Solid" />.</returns>
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x000552EF File Offset: 0x000534EF
		// (set) Token: 0x0600179D RID: 6045 RVA: 0x000552F8 File Offset: 0x000534F8
		[SRCategory("CatAppearance")]
		[DefaultValue(DataGridLineStyle.Solid)]
		[SRDescription("DataGridGridLineStyleDescr")]
		public DataGridLineStyle GridLineStyle
		{
			get
			{
				return this.gridLineStyle;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException(SR.GetString("DataGridDefaultTableSet", new object[] { "GridLineStyle" }));
				}
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridLineStyle));
				}
				if (this.gridLineStyle != value)
				{
					this.gridLineStyle = value;
					this.OnGridLineStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.GridLineStyle" /> value changes.</summary>
		// Token: 0x140000F5 RID: 245
		// (add) Token: 0x0600179E RID: 6046 RVA: 0x0005536C File Offset: 0x0005356C
		// (remove) Token: 0x0600179F RID: 6047 RVA: 0x0005537F File Offset: 0x0005357F
		public event EventHandler GridLineStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventGridLineStyle, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventGridLineStyle, value);
			}
		}

		/// <summary>Gets or sets the background color of headers.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of headers.</returns>
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x00055392 File Offset: 0x00053592
		// (set) Token: 0x060017A1 RID: 6049 RVA: 0x000553A0 File Offset: 0x000535A0
		[SRCategory("CatColors")]
		[SRDescription("DataGridHeaderBackColorDescr")]
		public Color HeaderBackColor
		{
			get
			{
				return this.headerBackBrush.Color;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException(SR.GetString("DataGridDefaultTableSet", new object[] { "HeaderBackColor" }));
				}
				if (DataGrid.IsTransparentColor(value))
				{
					throw new ArgumentException(SR.GetString("DataGridTableStyleTransparentHeaderBackColorNotAllowed"));
				}
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "HeaderBackColor" }));
				}
				if (!value.Equals(this.headerBackBrush.Color))
				{
					this.headerBackBrush = new SolidBrush(value);
					this.OnHeaderBackColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderBackColor" /> value changes.</summary>
		// Token: 0x140000F6 RID: 246
		// (add) Token: 0x060017A2 RID: 6050 RVA: 0x00055448 File Offset: 0x00053648
		// (remove) Token: 0x060017A3 RID: 6051 RVA: 0x0005545B File Offset: 0x0005365B
		public event EventHandler HeaderBackColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventHeaderBackColor, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventHeaderBackColor, value);
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0005546E File Offset: 0x0005366E
		internal SolidBrush HeaderBackBrush
		{
			get
			{
				return this.headerBackBrush;
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderBackColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060017A5 RID: 6053 RVA: 0x00055476 File Offset: 0x00053676
		protected virtual bool ShouldSerializeHeaderBackColor()
		{
			return !this.HeaderBackBrush.Equals(DataGridTableStyle.DefaultHeaderBackBrush);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderBackColor" /> property to its default value.</summary>
		// Token: 0x060017A6 RID: 6054 RVA: 0x0005548B File Offset: 0x0005368B
		public void ResetHeaderBackColor()
		{
			if (this.ShouldSerializeHeaderBackColor())
			{
				this.HeaderBackColor = DataGridTableStyle.DefaultHeaderBackBrush.Color;
			}
		}

		/// <summary>Gets or sets the font used for header captions.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> used for captions.</returns>
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x000554A5 File Offset: 0x000536A5
		// (set) Token: 0x060017A8 RID: 6056 RVA: 0x000554D0 File Offset: 0x000536D0
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[AmbientValue(null)]
		[SRDescription("DataGridHeaderFontDescr")]
		public Font HeaderFont
		{
			get
			{
				if (this.headerFont != null)
				{
					return this.headerFont;
				}
				if (this.DataGrid != null)
				{
					return this.DataGrid.Font;
				}
				return Control.DefaultFont;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException(SR.GetString("DataGridDefaultTableSet", new object[] { "HeaderFont" }));
				}
				if ((value == null && this.headerFont != null) || (value != null && !value.Equals(this.headerFont)))
				{
					this.headerFont = value;
					this.OnHeaderFontChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderFont" /> value changes.</summary>
		// Token: 0x140000F7 RID: 247
		// (add) Token: 0x060017A9 RID: 6057 RVA: 0x00055531 File Offset: 0x00053731
		// (remove) Token: 0x060017AA RID: 6058 RVA: 0x00055544 File Offset: 0x00053744
		public event EventHandler HeaderFontChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventHeaderFont, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventHeaderFont, value);
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00055557 File Offset: 0x00053757
		private bool ShouldSerializeHeaderFont()
		{
			return this.headerFont != null;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderFont" /> property to its default value.</summary>
		// Token: 0x060017AC RID: 6060 RVA: 0x00055562 File Offset: 0x00053762
		public void ResetHeaderFont()
		{
			if (this.headerFont != null)
			{
				this.headerFont = null;
				this.OnHeaderFontChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the foreground color of headers.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of headers.</returns>
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x0005557E File Offset: 0x0005377E
		// (set) Token: 0x060017AE RID: 6062 RVA: 0x0005558C File Offset: 0x0005378C
		[SRCategory("CatColors")]
		[SRDescription("DataGridHeaderForeColorDescr")]
		public Color HeaderForeColor
		{
			get
			{
				return this.headerForePen.Color;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException(SR.GetString("DataGridDefaultTableSet", new object[] { "HeaderForeColor" }));
				}
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "HeaderForeColor" }));
				}
				if (!value.Equals(this.headerForePen.Color))
				{
					this.headerForePen = new Pen(value);
					this.headerForeBrush = new SolidBrush(value);
					this.OnHeaderForeColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderForeColor" /> value changes.</summary>
		// Token: 0x140000F8 RID: 248
		// (add) Token: 0x060017AF RID: 6063 RVA: 0x00055628 File Offset: 0x00053828
		// (remove) Token: 0x060017B0 RID: 6064 RVA: 0x0005563B File Offset: 0x0005383B
		public event EventHandler HeaderForeColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventHeaderForeColor, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventHeaderForeColor, value);
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderForeColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060017B1 RID: 6065 RVA: 0x0005564E File Offset: 0x0005384E
		protected virtual bool ShouldSerializeHeaderForeColor()
		{
			return !this.HeaderForePen.Equals(DataGridTableStyle.DefaultHeaderForePen);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderForeColor" /> property to its default value.</summary>
		// Token: 0x060017B2 RID: 6066 RVA: 0x00055663 File Offset: 0x00053863
		public void ResetHeaderForeColor()
		{
			if (this.ShouldSerializeHeaderForeColor())
			{
				this.HeaderForeColor = DataGridTableStyle.DefaultHeaderForeBrush.Color;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0005567D File Offset: 0x0005387D
		internal SolidBrush HeaderForeBrush
		{
			get
			{
				return this.headerForeBrush;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x00055685 File Offset: 0x00053885
		internal Pen HeaderForePen
		{
			get
			{
				return this.headerForePen;
			}
		}

		/// <summary>Gets or sets the color of link text.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> of link text.</returns>
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0005568D File Offset: 0x0005388D
		// (set) Token: 0x060017B6 RID: 6070 RVA: 0x0005569C File Offset: 0x0005389C
		[SRCategory("CatColors")]
		[SRDescription("DataGridLinkColorDescr")]
		public Color LinkColor
		{
			get
			{
				return this.linkBrush.Color;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException(SR.GetString("DataGridDefaultTableSet", new object[] { "LinkColor" }));
				}
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "LinkColor" }));
				}
				if (!this.linkBrush.Color.Equals(value))
				{
					this.linkBrush = new SolidBrush(value);
					this.OnLinkColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkColor" /> value changes.</summary>
		// Token: 0x140000F9 RID: 249
		// (add) Token: 0x060017B7 RID: 6071 RVA: 0x0005572E File Offset: 0x0005392E
		// (remove) Token: 0x060017B8 RID: 6072 RVA: 0x00055741 File Offset: 0x00053941
		public event EventHandler LinkColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventLinkColor, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventLinkColor, value);
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060017B9 RID: 6073 RVA: 0x00055754 File Offset: 0x00053954
		protected virtual bool ShouldSerializeLinkColor()
		{
			return !this.LinkBrush.Equals(DataGridTableStyle.DefaultLinkBrush);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkColor" /> property to its default value.</summary>
		// Token: 0x060017BA RID: 6074 RVA: 0x00055769 File Offset: 0x00053969
		public void ResetLinkColor()
		{
			if (this.ShouldSerializeLinkColor())
			{
				this.LinkColor = DataGridTableStyle.DefaultLinkBrush.Color;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x00055783 File Offset: 0x00053983
		internal Brush LinkBrush
		{
			get
			{
				return this.linkBrush;
			}
		}

		/// <summary>Gets or sets the color displayed when hovering over link text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the hover color.</returns>
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0005578B File Offset: 0x0005398B
		// (set) Token: 0x060017BD RID: 6077 RVA: 0x000070A6 File Offset: 0x000052A6
		[SRDescription("DataGridLinkHoverColorDescr")]
		[SRCategory("CatColors")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Color LinkHoverColor
		{
			get
			{
				return this.LinkColor;
			}
			set
			{
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkHoverColor" /> value changes.</summary>
		// Token: 0x140000FA RID: 250
		// (add) Token: 0x060017BE RID: 6078 RVA: 0x00055793 File Offset: 0x00053993
		// (remove) Token: 0x060017BF RID: 6079 RVA: 0x000557A6 File Offset: 0x000539A6
		public event EventHandler LinkHoverColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventLinkHoverColor, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventLinkHoverColor, value);
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkHoverColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060017C0 RID: 6080 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool ShouldSerializeLinkHoverColor()
		{
			return false;
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x000557B9 File Offset: 0x000539B9
		internal Rectangle RelationshipRect
		{
			get
			{
				if (this.relationshipRect.IsEmpty)
				{
					this.ComputeRelationshipRect();
				}
				return this.relationshipRect;
			}
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x000557D8 File Offset: 0x000539D8
		private Rectangle ComputeRelationshipRect()
		{
			if (this.relationshipRect.IsEmpty && this.DataGrid.AllowNavigation)
			{
				Graphics graphics = this.DataGrid.CreateGraphicsInternal();
				this.relationshipRect = default(Rectangle);
				this.relationshipRect.X = 0;
				int num = 0;
				for (int i = 0; i < this.RelationsList.Count; i++)
				{
					int num2 = (int)Math.Ceiling((double)graphics.MeasureString((string)this.RelationsList[i], this.DataGrid.LinkFont).Width);
					if (num2 > num)
					{
						num = num2;
					}
				}
				graphics.Dispose();
				this.relationshipRect.Width = num + 5;
				this.relationshipRect.Width = this.relationshipRect.Width + 2;
				this.relationshipRect.Height = this.BorderWidth + this.relationshipHeight * this.RelationsList.Count;
				this.relationshipRect.Height = this.relationshipRect.Height + 2;
				if (this.RelationsList.Count > 0)
				{
					this.relationshipRect.Height = this.relationshipRect.Height + 2;
				}
			}
			return this.relationshipRect;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x000558FF File Offset: 0x00053AFF
		internal void ResetRelationsUI()
		{
			this.relationshipRect = Rectangle.Empty;
			this.focusedRelation = -1;
			this.relationshipHeight = this.dataGrid.LinkFontHeight + 1;
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x00055926 File Offset: 0x00053B26
		internal int RelationshipHeight
		{
			get
			{
				return this.relationshipHeight;
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkHoverColor" /> property to its default value.</summary>
		// Token: 0x060017C5 RID: 6085 RVA: 0x000070A6 File Offset: 0x000052A6
		public void ResetLinkHoverColor()
		{
		}

		/// <summary>Gets or sets the width used to create columns when a new grid is displayed.</summary>
		/// <returns>The width used to create columns when a new grid is displayed.</returns>
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x0005592E File Offset: 0x00053B2E
		// (set) Token: 0x060017C7 RID: 6087 RVA: 0x00055938 File Offset: 0x00053B38
		[DefaultValue(75)]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("DataGridPreferredColumnWidthDescr")]
		[TypeConverter(typeof(DataGridPreferredColumnWidthTypeConverter))]
		public int PreferredColumnWidth
		{
			get
			{
				return this.preferredColumnWidth;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException(SR.GetString("DataGridDefaultTableSet", new object[] { "PreferredColumnWidth" }));
				}
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("DataGridColumnWidth"), "PreferredColumnWidth");
				}
				if (this.preferredColumnWidth != value)
				{
					this.preferredColumnWidth = value;
					this.OnPreferredColumnWidthChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.PreferredColumnWidth" /> property value changes.</summary>
		// Token: 0x140000FB RID: 251
		// (add) Token: 0x060017C8 RID: 6088 RVA: 0x0005599F File Offset: 0x00053B9F
		// (remove) Token: 0x060017C9 RID: 6089 RVA: 0x000559B2 File Offset: 0x00053BB2
		public event EventHandler PreferredColumnWidthChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventPreferredColumnWidth, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventPreferredColumnWidth, value);
			}
		}

		/// <summary>Gets or sets the height used to create a row when a new grid is displayed.</summary>
		/// <returns>The height of a row, in pixels.</returns>
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x000559C5 File Offset: 0x00053BC5
		// (set) Token: 0x060017CB RID: 6091 RVA: 0x000559D0 File Offset: 0x00053BD0
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("DataGridPreferredRowHeightDescr")]
		public int PreferredRowHeight
		{
			get
			{
				return this.prefferedRowHeight;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException(SR.GetString("DataGridDefaultTableSet", new object[] { "PrefferedRowHeight" }));
				}
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("DataGridRowRowHeight"));
				}
				this.prefferedRowHeight = value;
				this.OnPreferredRowHeightChanged(EventArgs.Empty);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.PreferredRowHeight" /> value changes.</summary>
		// Token: 0x140000FC RID: 252
		// (add) Token: 0x060017CC RID: 6092 RVA: 0x00055A29 File Offset: 0x00053C29
		// (remove) Token: 0x060017CD RID: 6093 RVA: 0x00055A3C File Offset: 0x00053C3C
		public event EventHandler PreferredRowHeightChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventPreferredRowHeight, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventPreferredRowHeight, value);
			}
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00055A4F File Offset: 0x00053C4F
		private void ResetPreferredRowHeight()
		{
			this.PreferredRowHeight = DataGridTableStyle.defaultFontHeight + 3;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.PreferredRowHeight" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060017CF RID: 6095 RVA: 0x00055A5E File Offset: 0x00053C5E
		protected bool ShouldSerializePreferredRowHeight()
		{
			return this.prefferedRowHeight != DataGridTableStyle.defaultFontHeight + 3;
		}

		/// <summary>Gets or sets a value indicating whether column headers are visible.</summary>
		/// <returns>
		///   <see langword="true" /> if column headers are visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x00055A72 File Offset: 0x00053C72
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x00055A7A File Offset: 0x00053C7A
		[SRCategory("CatDisplay")]
		[DefaultValue(true)]
		[SRDescription("DataGridColumnHeadersVisibleDescr")]
		public bool ColumnHeadersVisible
		{
			get
			{
				return this.columnHeadersVisible;
			}
			set
			{
				if (this.columnHeadersVisible != value)
				{
					this.columnHeadersVisible = value;
					this.OnColumnHeadersVisibleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.ColumnHeadersVisible" /> value changes.</summary>
		// Token: 0x140000FD RID: 253
		// (add) Token: 0x060017D2 RID: 6098 RVA: 0x00055A97 File Offset: 0x00053C97
		// (remove) Token: 0x060017D3 RID: 6099 RVA: 0x00055AAA File Offset: 0x00053CAA
		public event EventHandler ColumnHeadersVisibleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventColumnHeadersVisible, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventColumnHeadersVisible, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether row headers are visible.</summary>
		/// <returns>
		///   <see langword="true" /> if row headers are visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x00055ABD File Offset: 0x00053CBD
		// (set) Token: 0x060017D5 RID: 6101 RVA: 0x00055AC5 File Offset: 0x00053CC5
		[SRCategory("CatDisplay")]
		[DefaultValue(true)]
		[SRDescription("DataGridRowHeadersVisibleDescr")]
		public bool RowHeadersVisible
		{
			get
			{
				return this.rowHeadersVisible;
			}
			set
			{
				if (this.rowHeadersVisible != value)
				{
					this.rowHeadersVisible = value;
					this.OnRowHeadersVisibleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.RowHeadersVisible" /> value changes.</summary>
		// Token: 0x140000FE RID: 254
		// (add) Token: 0x060017D6 RID: 6102 RVA: 0x00055AE2 File Offset: 0x00053CE2
		// (remove) Token: 0x060017D7 RID: 6103 RVA: 0x00055AF5 File Offset: 0x00053CF5
		public event EventHandler RowHeadersVisibleChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventRowHeadersVisible, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventRowHeadersVisible, value);
			}
		}

		/// <summary>Gets or sets the width of row headers.</summary>
		/// <returns>The width of row headers, in pixels.</returns>
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x00055B08 File Offset: 0x00053D08
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x00055B10 File Offset: 0x00053D10
		[SRCategory("CatLayout")]
		[DefaultValue(35)]
		[Localizable(true)]
		[SRDescription("DataGridRowHeaderWidthDescr")]
		public int RowHeaderWidth
		{
			get
			{
				return this.rowHeaderWidth;
			}
			set
			{
				if (this.DataGrid != null)
				{
					value = Math.Max(this.DataGrid.MinimumRowHeaderWidth(), value);
				}
				if (this.rowHeaderWidth != value)
				{
					this.rowHeaderWidth = value;
					this.OnRowHeaderWidthChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.RowHeaderWidth" /> value changes.</summary>
		// Token: 0x140000FF RID: 255
		// (add) Token: 0x060017DA RID: 6106 RVA: 0x00055B48 File Offset: 0x00053D48
		// (remove) Token: 0x060017DB RID: 6107 RVA: 0x00055B5B File Offset: 0x00053D5B
		public event EventHandler RowHeaderWidthChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventRowHeaderWidth, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventRowHeaderWidth, value);
			}
		}

		/// <summary>Gets or sets the background color of selected cells.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that represents the background color of selected cells.</returns>
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x00055B6E File Offset: 0x00053D6E
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x00055B7C File Offset: 0x00053D7C
		[SRCategory("CatColors")]
		[SRDescription("DataGridSelectionBackColorDescr")]
		public Color SelectionBackColor
		{
			get
			{
				return this.selectionBackBrush.Color;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException(SR.GetString("DataGridDefaultTableSet", new object[] { "SelectionBackColor" }));
				}
				if (DataGrid.IsTransparentColor(value))
				{
					throw new ArgumentException(SR.GetString("DataGridTableStyleTransparentSelectionBackColorNotAllowed"));
				}
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "SelectionBackColor" }));
				}
				if (!value.Equals(this.selectionBackBrush.Color))
				{
					this.selectionBackBrush = new SolidBrush(value);
					this.InvalidateInside();
					this.OnSelectionBackColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionBackColor" /> value changes.</summary>
		// Token: 0x14000100 RID: 256
		// (add) Token: 0x060017DE RID: 6110 RVA: 0x00055C2A File Offset: 0x00053E2A
		// (remove) Token: 0x060017DF RID: 6111 RVA: 0x00055C3D File Offset: 0x00053E3D
		public event EventHandler SelectionBackColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventSelectionBackColor, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventSelectionBackColor, value);
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x00055C50 File Offset: 0x00053E50
		internal SolidBrush SelectionBackBrush
		{
			get
			{
				return this.selectionBackBrush;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x00055C58 File Offset: 0x00053E58
		internal SolidBrush SelectionForeBrush
		{
			get
			{
				return this.selectionForeBrush;
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionBackColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060017E2 RID: 6114 RVA: 0x00055C60 File Offset: 0x00053E60
		protected bool ShouldSerializeSelectionBackColor()
		{
			return !DataGridTableStyle.DefaultSelectionBackBrush.Equals(this.selectionBackBrush);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionBackColor" /> property to its default value.</summary>
		// Token: 0x060017E3 RID: 6115 RVA: 0x00055C75 File Offset: 0x00053E75
		public void ResetSelectionBackColor()
		{
			if (this.ShouldSerializeSelectionBackColor())
			{
				this.SelectionBackColor = DataGridTableStyle.DefaultSelectionBackBrush.Color;
			}
		}

		/// <summary>Gets or sets the foreground color of selected cells.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that represents the foreground color of selected cells.</returns>
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x00055C8F File Offset: 0x00053E8F
		// (set) Token: 0x060017E5 RID: 6117 RVA: 0x00055C9C File Offset: 0x00053E9C
		[Description("The foreground color for the current data grid row")]
		[SRCategory("CatColors")]
		[SRDescription("DataGridSelectionForeColorDescr")]
		public Color SelectionForeColor
		{
			get
			{
				return this.selectionForeBrush.Color;
			}
			set
			{
				if (this.isDefaultTableStyle)
				{
					throw new ArgumentException(SR.GetString("DataGridDefaultTableSet", new object[] { "SelectionForeColor" }));
				}
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "SelectionForeColor" }));
				}
				if (!value.Equals(this.selectionForeBrush.Color))
				{
					this.selectionForeBrush = new SolidBrush(value);
					this.InvalidateInside();
					this.OnSelectionForeColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionForeColor" /> value changes.</summary>
		// Token: 0x14000101 RID: 257
		// (add) Token: 0x060017E6 RID: 6118 RVA: 0x00055D32 File Offset: 0x00053F32
		// (remove) Token: 0x060017E7 RID: 6119 RVA: 0x00055D45 File Offset: 0x00053F45
		public event EventHandler SelectionForeColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventSelectionForeColor, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventSelectionForeColor, value);
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionForeColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060017E8 RID: 6120 RVA: 0x00055D58 File Offset: 0x00053F58
		protected virtual bool ShouldSerializeSelectionForeColor()
		{
			return !this.SelectionForeBrush.Equals(DataGridTableStyle.DefaultSelectionForeBrush);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionForeColor" /> property to its default value.</summary>
		// Token: 0x060017E9 RID: 6121 RVA: 0x00055D6D File Offset: 0x00053F6D
		public void ResetSelectionForeColor()
		{
			if (this.ShouldSerializeSelectionForeColor())
			{
				this.SelectionForeColor = DataGridTableStyle.DefaultSelectionForeBrush.Color;
			}
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00055D87 File Offset: 0x00053F87
		private void InvalidateInside()
		{
			if (this.DataGrid != null)
			{
				this.DataGrid.InvalidateInside();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> class using the specified value to determine whether the grid table is the default style.</summary>
		/// <param name="isDefaultTableStyle">
		///   <see langword="true" /> to specify the table as the default; otherwise, <see langword="false" />.</param>
		// Token: 0x060017EB RID: 6123 RVA: 0x00055D9C File Offset: 0x00053F9C
		public DataGridTableStyle(bool isDefaultTableStyle)
		{
			this.gridColumns = new GridColumnStylesCollection(this, isDefaultTableStyle);
			this.gridColumns.CollectionChanged += this.OnColumnCollectionChanged;
			this.isDefaultTableStyle = isDefaultTableStyle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> class.</summary>
		// Token: 0x060017EC RID: 6124 RVA: 0x00055EAA File Offset: 0x000540AA
		public DataGridTableStyle()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> class with the specified <see cref="T:System.Windows.Forms.CurrencyManager" />.</summary>
		/// <param name="listManager">The <see cref="T:System.Windows.Forms.CurrencyManager" /> to use.</param>
		// Token: 0x060017ED RID: 6125 RVA: 0x00055EB3 File Offset: 0x000540B3
		public DataGridTableStyle(CurrencyManager listManager)
			: this()
		{
			this.mappingName = listManager.GetListName();
			this.SetGridColumnStylesCollection(listManager);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00055ED0 File Offset: 0x000540D0
		internal void SetRelationsList(CurrencyManager listManager)
		{
			PropertyDescriptorCollection itemProperties = listManager.GetItemProperties();
			int count = itemProperties.Count;
			if (this.relationsList.Count > 0)
			{
				this.relationsList.Clear();
			}
			for (int i = 0; i < count; i++)
			{
				PropertyDescriptor propertyDescriptor = itemProperties[i];
				if (DataGridTableStyle.PropertyDescriptorIsARelation(propertyDescriptor))
				{
					this.relationsList.Add(propertyDescriptor.Name);
				}
			}
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00055F34 File Offset: 0x00054134
		internal void SetGridColumnStylesCollection(CurrencyManager listManager)
		{
			this.gridColumns.CollectionChanged -= this.OnColumnCollectionChanged;
			PropertyDescriptorCollection itemProperties = listManager.GetItemProperties();
			if (this.relationsList.Count > 0)
			{
				this.relationsList.Clear();
			}
			int count = itemProperties.Count;
			for (int i = 0; i < count; i++)
			{
				PropertyDescriptor propertyDescriptor = itemProperties[i];
				if (propertyDescriptor.IsBrowsable)
				{
					if (DataGridTableStyle.PropertyDescriptorIsARelation(propertyDescriptor))
					{
						this.relationsList.Add(propertyDescriptor.Name);
					}
					else
					{
						DataGridColumnStyle dataGridColumnStyle = this.CreateGridColumn(propertyDescriptor, this.isDefaultTableStyle);
						if (this.isDefaultTableStyle)
						{
							this.gridColumns.AddDefaultColumn(dataGridColumnStyle);
						}
						else
						{
							dataGridColumnStyle.MappingName = propertyDescriptor.Name;
							dataGridColumnStyle.HeaderText = propertyDescriptor.Name;
							this.gridColumns.Add(dataGridColumnStyle);
						}
					}
				}
			}
			this.gridColumns.CollectionChanged += this.OnColumnCollectionChanged;
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0005601F File Offset: 0x0005421F
		private static bool PropertyDescriptorIsARelation(PropertyDescriptor prop)
		{
			return typeof(IList).IsAssignableFrom(prop.PropertyType) && !typeof(Array).IsAssignableFrom(prop.PropertyType);
		}

		/// <summary>Creates a <see cref="T:System.Windows.Forms.DataGridColumnStyle" />, using the specified property descriptor.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> used to create the column style object.</param>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		// Token: 0x060017F1 RID: 6129 RVA: 0x00056052 File Offset: 0x00054252
		protected internal virtual DataGridColumnStyle CreateGridColumn(PropertyDescriptor prop)
		{
			return this.CreateGridColumn(prop, false);
		}

		/// <summary>Creates a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> using the specified property descriptor. Specifies whether the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> is a default column style.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> used to create the column style object.</param>
		/// <param name="isDefault">Specifies whether the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> is a default column style. This parameter is read-only.</param>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		// Token: 0x060017F2 RID: 6130 RVA: 0x0005605C File Offset: 0x0005425C
		protected internal virtual DataGridColumnStyle CreateGridColumn(PropertyDescriptor prop, bool isDefault)
		{
			Type propertyType = prop.PropertyType;
			DataGridColumnStyle dataGridColumnStyle;
			if (propertyType.Equals(typeof(bool)))
			{
				dataGridColumnStyle = new DataGridBoolColumn(prop, isDefault);
			}
			else if (propertyType.Equals(typeof(string)))
			{
				dataGridColumnStyle = new DataGridTextBoxColumn(prop, isDefault);
			}
			else if (propertyType.Equals(typeof(DateTime)))
			{
				dataGridColumnStyle = new DataGridTextBoxColumn(prop, "d", isDefault);
			}
			else if (propertyType.Equals(typeof(short)) || propertyType.Equals(typeof(int)) || propertyType.Equals(typeof(long)) || propertyType.Equals(typeof(ushort)) || propertyType.Equals(typeof(uint)) || propertyType.Equals(typeof(ulong)) || propertyType.Equals(typeof(decimal)) || propertyType.Equals(typeof(double)) || propertyType.Equals(typeof(float)) || propertyType.Equals(typeof(byte)) || propertyType.Equals(typeof(sbyte)))
			{
				dataGridColumnStyle = new DataGridTextBoxColumn(prop, "G", isDefault);
			}
			else
			{
				dataGridColumnStyle = new DataGridTextBoxColumn(prop, isDefault);
			}
			return dataGridColumnStyle;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x000561BB File Offset: 0x000543BB
		internal void ResetRelationsList()
		{
			if (this.isDefaultTableStyle)
			{
				this.relationsList.Clear();
			}
		}

		/// <summary>Gets or sets the name used to map this table to a specific data source.</summary>
		/// <returns>The name used to map this grid to a specific data source.</returns>
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x000561D0 File Offset: 0x000543D0
		// (set) Token: 0x060017F5 RID: 6133 RVA: 0x000561D8 File Offset: 0x000543D8
		[Editor("System.Windows.Forms.Design.DataGridTableStyleMappingNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string MappingName
		{
			get
			{
				return this.mappingName;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value.Equals(this.mappingName))
				{
					return;
				}
				string text = this.MappingName;
				this.mappingName = value;
				try
				{
					if (this.DataGrid != null)
					{
						this.DataGrid.TableStyles.CheckForMappingNameDuplicates(this);
					}
				}
				catch
				{
					this.mappingName = text;
					throw;
				}
				this.OnMappingNameChanged(EventArgs.Empty);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" /> value changes.</summary>
		// Token: 0x14000102 RID: 258
		// (add) Token: 0x060017F6 RID: 6134 RVA: 0x0005624C File Offset: 0x0005444C
		// (remove) Token: 0x060017F7 RID: 6135 RVA: 0x0005625F File Offset: 0x0005445F
		public event EventHandler MappingNameChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventMappingName, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventMappingName, value);
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x00056272 File Offset: 0x00054472
		internal ArrayList RelationsList
		{
			get
			{
				return this.relationsList;
			}
		}

		/// <summary>Gets the collection of columns drawn for this table.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> that contains all <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects for the table.</returns>
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x0005627A File Offset: 0x0005447A
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual GridColumnStylesCollection GridColumnStyles
		{
			get
			{
				return this.gridColumns;
			}
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00056284 File Offset: 0x00054484
		internal void SetInternalDataGrid(DataGrid dG, bool force)
		{
			if (this.dataGrid != null && this.dataGrid.Equals(dG) && !force)
			{
				return;
			}
			this.dataGrid = dG;
			if (dG != null && dG.Initializing)
			{
				return;
			}
			int count = this.gridColumns.Count;
			for (int i = 0; i < count; i++)
			{
				this.gridColumns[i].SetDataGridInternalInColumn(dG);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGrid" /> control for the drawn table.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGrid" /> control that displays the table.</returns>
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x000562E8 File Offset: 0x000544E8
		// (set) Token: 0x060017FC RID: 6140 RVA: 0x000562F0 File Offset: 0x000544F0
		[Browsable(false)]
		public virtual DataGrid DataGrid
		{
			get
			{
				return this.dataGrid;
			}
			set
			{
				this.SetInternalDataGrid(value, true);
			}
		}

		/// <summary>Gets or sets a value indicating whether columns can be edited.</summary>
		/// <returns>
		///   <see langword="true" />, if columns cannot be edited; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x000562FA File Offset: 0x000544FA
		// (set) Token: 0x060017FE RID: 6142 RVA: 0x00056302 File Offset: 0x00054502
		[DefaultValue(false)]
		public virtual bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				if (this.readOnly != value)
				{
					this.readOnly = value;
					this.OnReadOnlyChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.ReadOnly" /> value changes.</summary>
		// Token: 0x14000103 RID: 259
		// (add) Token: 0x060017FF RID: 6143 RVA: 0x0005631F File Offset: 0x0005451F
		// (remove) Token: 0x06001800 RID: 6144 RVA: 0x00056332 File Offset: 0x00054532
		public event EventHandler ReadOnlyChanged
		{
			add
			{
				base.Events.AddHandler(DataGridTableStyle.EventReadOnly, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridTableStyle.EventReadOnly, value);
			}
		}

		/// <summary>Requests an edit operation.</summary>
		/// <param name="gridColumn">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to edit.</param>
		/// <param name="rowNumber">The number of the edited row.</param>
		/// <returns>
		///   <see langword="true" />, if the operation succeeds; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001801 RID: 6145 RVA: 0x00056348 File Offset: 0x00054548
		public bool BeginEdit(DataGridColumnStyle gridColumn, int rowNumber)
		{
			DataGrid dataGrid = this.DataGrid;
			return dataGrid != null && dataGrid.BeginEdit(gridColumn, rowNumber);
		}

		/// <summary>Requests an end to an edit operation.</summary>
		/// <param name="gridColumn">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to edit.</param>
		/// <param name="rowNumber">The number of the edited row.</param>
		/// <param name="shouldAbort">A value indicating whether the operation should be stopped; <see langword="true" /> if it should stop; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the edit operation ends successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001802 RID: 6146 RVA: 0x0005636C File Offset: 0x0005456C
		public bool EndEdit(DataGridColumnStyle gridColumn, int rowNumber, bool shouldAbort)
		{
			DataGrid dataGrid = this.DataGrid;
			return dataGrid != null && dataGrid.EndEdit(gridColumn, rowNumber, shouldAbort);
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00056390 File Offset: 0x00054590
		internal void InvalidateColumn(DataGridColumnStyle column)
		{
			int num = this.GridColumnStyles.IndexOf(column);
			if (num >= 0 && this.DataGrid != null)
			{
				this.DataGrid.InvalidateColumn(num);
			}
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x000563C4 File Offset: 0x000545C4
		private void OnColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			this.gridColumns.CollectionChanged -= this.OnColumnCollectionChanged;
			try
			{
				DataGrid dataGrid = this.DataGrid;
				DataGridColumnStyle dataGridColumnStyle = e.Element as DataGridColumnStyle;
				if (e.Action == CollectionChangeAction.Add)
				{
					if (dataGridColumnStyle != null)
					{
						dataGridColumnStyle.SetDataGridInternalInColumn(dataGrid);
					}
				}
				else if (e.Action == CollectionChangeAction.Remove)
				{
					if (dataGridColumnStyle != null)
					{
						dataGridColumnStyle.SetDataGridInternalInColumn(null);
					}
				}
				else if (e.Element != null)
				{
					for (int i = 0; i < this.gridColumns.Count; i++)
					{
						this.gridColumns[i].SetDataGridInternalInColumn(null);
					}
				}
				if (dataGrid != null)
				{
					dataGrid.OnColumnCollectionChanged(this, e);
				}
			}
			finally
			{
				this.gridColumns.CollectionChanged += this.OnColumnCollectionChanged;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.ReadOnlyChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001805 RID: 6149 RVA: 0x0005648C File Offset: 0x0005468C
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventReadOnly] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.MappingNameChanged" /> event</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001806 RID: 6150 RVA: 0x000564BC File Offset: 0x000546BC
		protected virtual void OnMappingNameChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventMappingName] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.AlternatingBackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001807 RID: 6151 RVA: 0x000564EC File Offset: 0x000546EC
		protected virtual void OnAlternatingBackColorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventAlternatingBackColor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001808 RID: 6152 RVA: 0x0005651C File Offset: 0x0005471C
		protected virtual void OnForeColorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventBackColor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001809 RID: 6153 RVA: 0x0005654C File Offset: 0x0005474C
		protected virtual void OnBackColorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventForeColor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.AllowSortingChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600180A RID: 6154 RVA: 0x0005657C File Offset: 0x0005477C
		protected virtual void OnAllowSortingChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventAllowSorting] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.GridLineColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600180B RID: 6155 RVA: 0x000565AC File Offset: 0x000547AC
		protected virtual void OnGridLineColorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventGridLineColor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.GridLineStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600180C RID: 6156 RVA: 0x000565DC File Offset: 0x000547DC
		protected virtual void OnGridLineStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventGridLineStyle] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.HeaderBackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600180D RID: 6157 RVA: 0x0005660C File Offset: 0x0005480C
		protected virtual void OnHeaderBackColorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventHeaderBackColor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.HeaderFontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600180E RID: 6158 RVA: 0x0005663C File Offset: 0x0005483C
		protected virtual void OnHeaderFontChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventHeaderFont] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.HeaderForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600180F RID: 6159 RVA: 0x0005666C File Offset: 0x0005486C
		protected virtual void OnHeaderForeColorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventHeaderForeColor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.LinkColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001810 RID: 6160 RVA: 0x0005669C File Offset: 0x0005489C
		protected virtual void OnLinkColorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventLinkColor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see langword="LinkHoverColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001811 RID: 6161 RVA: 0x000566CC File Offset: 0x000548CC
		protected virtual void OnLinkHoverColorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventLinkHoverColor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.PreferredRowHeightChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001812 RID: 6162 RVA: 0x000566FC File Offset: 0x000548FC
		protected virtual void OnPreferredRowHeightChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventPreferredRowHeight] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.PreferredColumnWidthChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001813 RID: 6163 RVA: 0x0005672C File Offset: 0x0005492C
		protected virtual void OnPreferredColumnWidthChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventPreferredColumnWidth] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.ColumnHeadersVisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001814 RID: 6164 RVA: 0x0005675C File Offset: 0x0005495C
		protected virtual void OnColumnHeadersVisibleChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventColumnHeadersVisible] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.RowHeadersVisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001815 RID: 6165 RVA: 0x0005678C File Offset: 0x0005498C
		protected virtual void OnRowHeadersVisibleChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventRowHeadersVisible] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.RowHeaderWidthChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001816 RID: 6166 RVA: 0x000567BC File Offset: 0x000549BC
		protected virtual void OnRowHeaderWidthChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventRowHeaderWidth] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.SelectionForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001817 RID: 6167 RVA: 0x000567EC File Offset: 0x000549EC
		protected virtual void OnSelectionForeColorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventSelectionForeColor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.SelectionBackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001818 RID: 6168 RVA: 0x0005681C File Offset: 0x00054A1C
		protected virtual void OnSelectionBackColorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridTableStyle.EventSelectionBackColor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001819 RID: 6169 RVA: 0x0005684C File Offset: 0x00054A4C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				GridColumnStylesCollection gridColumnStyles = this.GridColumnStyles;
				if (gridColumnStyles != null)
				{
					for (int i = 0; i < gridColumnStyles.Count; i++)
					{
						gridColumnStyles[i].Dispose();
					}
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x0005688A File Offset: 0x00054A8A
		internal bool IsDefault
		{
			get
			{
				return this.isDefaultTableStyle;
			}
		}

		// Token: 0x04000A88 RID: 2696
		internal DataGrid dataGrid;

		// Token: 0x04000A89 RID: 2697
		private int relationshipHeight;

		// Token: 0x04000A8A RID: 2698
		internal const int relationshipSpacing = 1;

		// Token: 0x04000A8B RID: 2699
		private Rectangle relationshipRect = Rectangle.Empty;

		// Token: 0x04000A8C RID: 2700
		private int focusedRelation = -1;

		// Token: 0x04000A8D RID: 2701
		private int focusedTextWidth;

		// Token: 0x04000A8E RID: 2702
		private ArrayList relationsList = new ArrayList(2);

		// Token: 0x04000A8F RID: 2703
		private string mappingName = "";

		// Token: 0x04000A90 RID: 2704
		private GridColumnStylesCollection gridColumns;

		// Token: 0x04000A91 RID: 2705
		private bool readOnly;

		// Token: 0x04000A92 RID: 2706
		private bool isDefaultTableStyle;

		// Token: 0x04000A93 RID: 2707
		private static readonly object EventAllowSorting = new object();

		// Token: 0x04000A94 RID: 2708
		private static readonly object EventGridLineColor = new object();

		// Token: 0x04000A95 RID: 2709
		private static readonly object EventGridLineStyle = new object();

		// Token: 0x04000A96 RID: 2710
		private static readonly object EventHeaderBackColor = new object();

		// Token: 0x04000A97 RID: 2711
		private static readonly object EventHeaderForeColor = new object();

		// Token: 0x04000A98 RID: 2712
		private static readonly object EventHeaderFont = new object();

		// Token: 0x04000A99 RID: 2713
		private static readonly object EventLinkColor = new object();

		// Token: 0x04000A9A RID: 2714
		private static readonly object EventLinkHoverColor = new object();

		// Token: 0x04000A9B RID: 2715
		private static readonly object EventPreferredColumnWidth = new object();

		// Token: 0x04000A9C RID: 2716
		private static readonly object EventPreferredRowHeight = new object();

		// Token: 0x04000A9D RID: 2717
		private static readonly object EventColumnHeadersVisible = new object();

		// Token: 0x04000A9E RID: 2718
		private static readonly object EventRowHeaderWidth = new object();

		// Token: 0x04000A9F RID: 2719
		private static readonly object EventSelectionBackColor = new object();

		// Token: 0x04000AA0 RID: 2720
		private static readonly object EventSelectionForeColor = new object();

		// Token: 0x04000AA1 RID: 2721
		private static readonly object EventMappingName = new object();

		// Token: 0x04000AA2 RID: 2722
		private static readonly object EventAlternatingBackColor = new object();

		// Token: 0x04000AA3 RID: 2723
		private static readonly object EventBackColor = new object();

		// Token: 0x04000AA4 RID: 2724
		private static readonly object EventForeColor = new object();

		// Token: 0x04000AA5 RID: 2725
		private static readonly object EventReadOnly = new object();

		// Token: 0x04000AA6 RID: 2726
		private static readonly object EventRowHeadersVisible = new object();

		// Token: 0x04000AA7 RID: 2727
		private const bool defaultAllowSorting = true;

		// Token: 0x04000AA8 RID: 2728
		private const DataGridLineStyle defaultGridLineStyle = DataGridLineStyle.Solid;

		// Token: 0x04000AA9 RID: 2729
		private const int defaultPreferredColumnWidth = 75;

		// Token: 0x04000AAA RID: 2730
		private const int defaultRowHeaderWidth = 35;

		// Token: 0x04000AAB RID: 2731
		internal static readonly Font defaultFont = Control.DefaultFont;

		// Token: 0x04000AAC RID: 2732
		internal static readonly int defaultFontHeight = DataGridTableStyle.defaultFont.Height;

		// Token: 0x04000AAD RID: 2733
		private bool allowSorting = true;

		// Token: 0x04000AAE RID: 2734
		private SolidBrush alternatingBackBrush = DataGridTableStyle.DefaultAlternatingBackBrush;

		// Token: 0x04000AAF RID: 2735
		private SolidBrush backBrush = DataGridTableStyle.DefaultBackBrush;

		// Token: 0x04000AB0 RID: 2736
		private SolidBrush foreBrush = DataGridTableStyle.DefaultForeBrush;

		// Token: 0x04000AB1 RID: 2737
		private SolidBrush gridLineBrush = DataGridTableStyle.DefaultGridLineBrush;

		// Token: 0x04000AB2 RID: 2738
		private DataGridLineStyle gridLineStyle = DataGridLineStyle.Solid;

		// Token: 0x04000AB3 RID: 2739
		internal SolidBrush headerBackBrush = DataGridTableStyle.DefaultHeaderBackBrush;

		// Token: 0x04000AB4 RID: 2740
		internal Font headerFont;

		// Token: 0x04000AB5 RID: 2741
		internal SolidBrush headerForeBrush = DataGridTableStyle.DefaultHeaderForeBrush;

		// Token: 0x04000AB6 RID: 2742
		internal Pen headerForePen = DataGridTableStyle.DefaultHeaderForePen;

		// Token: 0x04000AB7 RID: 2743
		private SolidBrush linkBrush = DataGridTableStyle.DefaultLinkBrush;

		// Token: 0x04000AB8 RID: 2744
		internal int preferredColumnWidth = 75;

		// Token: 0x04000AB9 RID: 2745
		private int prefferedRowHeight = DataGridTableStyle.defaultFontHeight + 3;

		// Token: 0x04000ABA RID: 2746
		private SolidBrush selectionBackBrush = DataGridTableStyle.DefaultSelectionBackBrush;

		// Token: 0x04000ABB RID: 2747
		private SolidBrush selectionForeBrush = DataGridTableStyle.DefaultSelectionForeBrush;

		// Token: 0x04000ABC RID: 2748
		private int rowHeaderWidth = 35;

		// Token: 0x04000ABD RID: 2749
		private bool rowHeadersVisible = true;

		// Token: 0x04000ABE RID: 2750
		private bool columnHeadersVisible = true;

		/// <summary>Gets the default table style.</summary>
		// Token: 0x04000ABF RID: 2751
		public static readonly DataGridTableStyle DefaultTableStyle = new DataGridTableStyle(true);
	}
}
