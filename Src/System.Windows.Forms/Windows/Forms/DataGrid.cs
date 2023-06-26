using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Displays ADO.NET data in a scrollable grid. The <see cref="T:System.Windows.Forms.DataGridView" /> control replaces and adds functionality to the <see cref="T:System.Windows.Forms.DataGrid" /> control; however, the <see cref="T:System.Windows.Forms.DataGrid" /> control is retained for both backward compatibility and future use, if you choose.</summary>
	// Token: 0x02000179 RID: 377
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.DataGridDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("DataSource")]
	[DefaultEvent("Navigate")]
	[ComplexBindingProperties("DataSource", "DataMember")]
	public class DataGrid : Control, ISupportInitialize, IDataGridEditingService
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGrid" /> class.</summary>
		// Token: 0x0600142E RID: 5166 RVA: 0x00043FA8 File Offset: 0x000421A8
		public DataGrid()
		{
			base.SetStyle(ControlStyles.UserPaint, true);
			base.SetStyle(ControlStyles.Opaque, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
			base.SetStyle(ControlStyles.UserMouse, true);
			this.gridState = new BitVector32(272423);
			this.dataGridTables = new GridTableStylesCollection(this);
			this.layout = this.CreateInitialLayoutState();
			this.parentRows = new DataGridParentRows(this);
			this.horizScrollBar.Top = base.ClientRectangle.Height - this.horizScrollBar.Height;
			this.horizScrollBar.Left = 0;
			this.horizScrollBar.Visible = false;
			this.horizScrollBar.Scroll += this.GridHScrolled;
			base.Controls.Add(this.horizScrollBar);
			this.vertScrollBar.Top = 0;
			this.vertScrollBar.Left = base.ClientRectangle.Width - this.vertScrollBar.Width;
			this.vertScrollBar.Visible = false;
			this.vertScrollBar.Scroll += this.GridVScrolled;
			base.Controls.Add(this.vertScrollBar);
			this.BackColor = DataGrid.DefaultBackBrush.Color;
			this.ForeColor = DataGrid.DefaultForeBrush.Color;
			this.borderStyle = BorderStyle.Fixed3D;
			this.currentChangedHandler = new EventHandler(this.DataSource_RowChanged);
			this.positionChangedHandler = new EventHandler(this.DataSource_PositionChanged);
			this.itemChangedHandler = new ItemChangedEventHandler(this.DataSource_ItemChanged);
			this.metaDataChangedHandler = new EventHandler(this.DataSource_MetaDataChanged);
			this.dataGridTableStylesCollectionChanged = new CollectionChangeEventHandler(this.TableStylesCollectionChanged);
			this.dataGridTables.CollectionChanged += this.dataGridTableStylesCollectionChanged;
			this.SetDataGridTable(this.defaultTableStyle, true);
			this.backButtonHandler = new EventHandler(this.OnBackButtonClicked);
			this.downButtonHandler = new EventHandler(this.OnShowParentDetailsButtonClicked);
			this.caption = new DataGridCaption(this);
			this.caption.BackwardClicked += this.backButtonHandler;
			this.caption.DownClicked += this.downButtonHandler;
			this.RecalculateFonts();
			base.Size = new Size(130, 80);
			base.Invalidate();
			base.PerformLayout();
		}

		/// <summary>Gets or sets a value indicating whether the grid can be resorted by clicking on a column header.</summary>
		/// <returns>
		///   <see langword="true" /> if columns can be sorted; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0004432A File Offset: 0x0004252A
		// (set) Token: 0x06001430 RID: 5168 RVA: 0x00044338 File Offset: 0x00042538
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("DataGridAllowSortingDescr")]
		public bool AllowSorting
		{
			get
			{
				return this.gridState[1];
			}
			set
			{
				if (this.AllowSorting != value)
				{
					this.gridState[1] = value;
					if (!value && this.listManager != null)
					{
						IList list = this.listManager.List;
						if (list is IBindingList)
						{
							((IBindingList)list).RemoveSort();
						}
					}
				}
			}
		}

		/// <summary>Gets or sets the background color of odd-numbered rows of the grid.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the alternating background color. The default is the system color for windows (<see cref="P:System.Drawing.SystemColors.Window" />).</returns>
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x00044385 File Offset: 0x00042585
		// (set) Token: 0x06001432 RID: 5170 RVA: 0x00044394 File Offset: 0x00042594
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
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "AlternatingBackColor" }));
				}
				if (DataGrid.IsTransparentColor(value))
				{
					throw new ArgumentException(SR.GetString("DataGridTransparentAlternatingBackColorNotAllowed"));
				}
				if (!this.alternatingBackBrush.Color.Equals(value))
				{
					this.alternatingBackBrush = new SolidBrush(value);
					this.InvalidateInside();
				}
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.AlternatingBackColor" /> property to its default color.</summary>
		// Token: 0x06001433 RID: 5171 RVA: 0x00044413 File Offset: 0x00042613
		public void ResetAlternatingBackColor()
		{
			if (this.ShouldSerializeAlternatingBackColor())
			{
				this.AlternatingBackColor = DataGrid.DefaultAlternatingBackBrush.Color;
				this.InvalidateInside();
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.AlternatingBackColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001434 RID: 5172 RVA: 0x00044433 File Offset: 0x00042633
		protected virtual bool ShouldSerializeAlternatingBackColor()
		{
			return !this.AlternatingBackBrush.Equals(DataGrid.DefaultAlternatingBackBrush);
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x00044448 File Offset: 0x00042648
		internal Brush AlternatingBackBrush
		{
			get
			{
				return this.alternatingBackBrush;
			}
		}

		/// <summary>Gets or sets the background color of even-numbered rows of the grid.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of rows in the grid. The default is the system color for windows (<see cref="P:System.Drawing.SystemColors.Window" />).</returns>
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0001A049 File Offset: 0x00018249
		// (set) Token: 0x06001437 RID: 5175 RVA: 0x00044450 File Offset: 0x00042650
		[SRCategory("CatColors")]
		[SRDescription("ControlBackColorDescr")]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				if (DataGrid.IsTransparentColor(value))
				{
					throw new ArgumentException(SR.GetString("DataGridTransparentBackColorNotAllowed"));
				}
				base.BackColor = value;
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.BackColor" /> property to its default value.</summary>
		// Token: 0x06001438 RID: 5176 RVA: 0x00044474 File Offset: 0x00042674
		public override void ResetBackColor()
		{
			if (!this.BackColor.Equals(DataGrid.DefaultBackBrush.Color))
			{
				this.BackColor = DataGrid.DefaultBackBrush.Color;
			}
		}

		/// <summary>Gets or sets the foreground color (typically the color of the text) property of the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color. The default is <see cref="P:System.Drawing.SystemBrushes.WindowText" /> color.</returns>
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x0001A0E7 File Offset: 0x000182E7
		// (set) Token: 0x0600143A RID: 5178 RVA: 0x00013024 File Offset: 0x00011224
		[SRCategory("CatColors")]
		[SRDescription("ControlForeColorDescr")]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.ForeColor" /> property to its default value.</summary>
		// Token: 0x0600143B RID: 5179 RVA: 0x000444B8 File Offset: 0x000426B8
		public override void ResetForeColor()
		{
			if (!this.ForeColor.Equals(DataGrid.DefaultForeBrush.Color))
			{
				this.ForeColor = DataGrid.DefaultForeBrush.Color;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x000444FA File Offset: 0x000426FA
		internal SolidBrush BackBrush
		{
			get
			{
				return this.backBrush;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00044502 File Offset: 0x00042702
		internal SolidBrush ForeBrush
		{
			get
			{
				return this.foreBrush;
			}
		}

		/// <summary>Gets or sets the grid's border style.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> enumeration values. The default is <see langword="FixedSingle" />.</returns>
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0004450A File Offset: 0x0004270A
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x00044514 File Offset: 0x00042714
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		[SRDescription("DataGridBorderStyleDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
				}
				if (this.borderStyle != value)
				{
					this.borderStyle = value;
					base.PerformLayout();
					base.Invalidate();
					this.OnBorderStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.BorderStyle" /> has changed.</summary>
		// Token: 0x140000CD RID: 205
		// (add) Token: 0x06001440 RID: 5184 RVA: 0x0004456E File Offset: 0x0004276E
		// (remove) Token: 0x06001441 RID: 5185 RVA: 0x00044581 File Offset: 0x00042781
		[SRCategory("CatPropertyChanged")]
		[SRDescription("DataGridOnBorderStyleChangedDescr")]
		public event EventHandler BorderStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.EVENT_BORDERSTYLECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EVENT_BORDERSTYLECHANGED, value);
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00044594 File Offset: 0x00042794
		private int BorderWidth
		{
			get
			{
				if (this.BorderStyle == BorderStyle.Fixed3D)
				{
					return SystemInformation.Border3DSize.Width;
				}
				if (this.BorderStyle == BorderStyle.FixedSingle)
				{
					return 2;
				}
				return 0;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default size of the control.</returns>
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x000445C4 File Offset: 0x000427C4
		protected override Size DefaultSize
		{
			get
			{
				return new Size(130, 80);
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x000445D2 File Offset: 0x000427D2
		private static SolidBrush DefaultSelectionBackBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.ActiveCaption;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x000445DE File Offset: 0x000427DE
		private static SolidBrush DefaultSelectionForeBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.ActiveCaptionText;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x000445EA File Offset: 0x000427EA
		internal static SolidBrush DefaultBackBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.Window;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x000445F6 File Offset: 0x000427F6
		internal static SolidBrush DefaultForeBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.WindowText;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x00044602 File Offset: 0x00042802
		private static SolidBrush DefaultBackgroundBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.AppWorkspace;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x000445F6 File Offset: 0x000427F6
		internal static SolidBrush DefaultParentRowsForeBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.WindowText;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0004460E File Offset: 0x0004280E
		internal static SolidBrush DefaultParentRowsBackBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.Control;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x000445EA File Offset: 0x000427EA
		internal static SolidBrush DefaultAlternatingBackBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.Window;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0004460E File Offset: 0x0004280E
		private static SolidBrush DefaultGridLineBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.Control;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0004460E File Offset: 0x0004280E
		private static SolidBrush DefaultHeaderBackBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.Control;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0004461A File Offset: 0x0004281A
		private static SolidBrush DefaultHeaderForeBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.ControlText;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x00044626 File Offset: 0x00042826
		private static Pen DefaultHeaderForePen
		{
			get
			{
				return new Pen(SystemColors.ControlText);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x00044632 File Offset: 0x00042832
		private static SolidBrush DefaultLinkBrush
		{
			get
			{
				return (SolidBrush)SystemBrushes.HotTrack;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0004463E File Offset: 0x0004283E
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x00044650 File Offset: 0x00042850
		private bool ListHasErrors
		{
			get
			{
				return this.gridState[128];
			}
			set
			{
				if (this.ListHasErrors != value)
				{
					this.gridState[128] = value;
					this.ComputeMinimumRowHeaderWidth();
					if (!this.layout.RowHeadersVisible)
					{
						return;
					}
					if (value)
					{
						if (this.myGridTable.IsDefault)
						{
							this.RowHeaderWidth += 15;
							return;
						}
						this.myGridTable.RowHeaderWidth += 15;
						return;
					}
					else
					{
						if (this.myGridTable.IsDefault)
						{
							this.RowHeaderWidth -= 15;
							return;
						}
						this.myGridTable.RowHeaderWidth -= 15;
					}
				}
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x000446F4 File Offset: 0x000428F4
		private bool Bound
		{
			get
			{
				return this.listManager != null && this.myGridTable != null;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00044709 File Offset: 0x00042909
		internal DataGridCaption Caption
		{
			get
			{
				return this.caption;
			}
		}

		/// <summary>Gets or sets the background color of the caption area.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the caption's background color. The default is <see cref="P:System.Drawing.SystemColors.ActiveCaption" /> color.</returns>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x00044711 File Offset: 0x00042911
		// (set) Token: 0x06001456 RID: 5206 RVA: 0x0004471E File Offset: 0x0004291E
		[SRCategory("CatColors")]
		[SRDescription("DataGridCaptionBackColorDescr")]
		public Color CaptionBackColor
		{
			get
			{
				return this.Caption.BackColor;
			}
			set
			{
				if (DataGrid.IsTransparentColor(value))
				{
					throw new ArgumentException(SR.GetString("DataGridTransparentCaptionBackColorNotAllowed"));
				}
				this.Caption.BackColor = value;
			}
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00044744 File Offset: 0x00042944
		private void ResetCaptionBackColor()
		{
			this.Caption.ResetBackColor();
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGrid.CaptionBackColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has been changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001458 RID: 5208 RVA: 0x00044751 File Offset: 0x00042951
		protected virtual bool ShouldSerializeCaptionBackColor()
		{
			return this.Caption.ShouldSerializeBackColor();
		}

		/// <summary>Gets or sets the foreground color of the caption area.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the caption area. The default is <see cref="P:System.Drawing.SystemColors.ActiveCaptionText" />.</returns>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0004475E File Offset: 0x0004295E
		// (set) Token: 0x0600145A RID: 5210 RVA: 0x0004476B File Offset: 0x0004296B
		[SRCategory("CatColors")]
		[SRDescription("DataGridCaptionForeColorDescr")]
		public Color CaptionForeColor
		{
			get
			{
				return this.Caption.ForeColor;
			}
			set
			{
				this.Caption.ForeColor = value;
			}
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00044779 File Offset: 0x00042979
		private void ResetCaptionForeColor()
		{
			this.Caption.ResetForeColor();
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGrid.CaptionForeColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has been changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600145C RID: 5212 RVA: 0x00044786 File Offset: 0x00042986
		protected virtual bool ShouldSerializeCaptionForeColor()
		{
			return this.Caption.ShouldSerializeForeColor();
		}

		/// <summary>Gets or sets the font of the grid's caption.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that represents the caption's font.</returns>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00044793 File Offset: 0x00042993
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x000447A0 File Offset: 0x000429A0
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[AmbientValue(null)]
		[SRDescription("DataGridCaptionFontDescr")]
		public Font CaptionFont
		{
			get
			{
				return this.Caption.Font;
			}
			set
			{
				this.Caption.Font = value;
			}
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x000447AE File Offset: 0x000429AE
		private bool ShouldSerializeCaptionFont()
		{
			return this.Caption.ShouldSerializeFont();
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x000447BB File Offset: 0x000429BB
		private void ResetCaptionFont()
		{
			this.Caption.ResetFont();
		}

		/// <summary>Gets or sets the text of the grid's window caption.</summary>
		/// <returns>A string to be displayed as the window caption of the grid. The default is an empty string ("").</returns>
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x000447C8 File Offset: 0x000429C8
		// (set) Token: 0x06001462 RID: 5218 RVA: 0x000447D5 File Offset: 0x000429D5
		[SRCategory("CatAppearance")]
		[DefaultValue("")]
		[Localizable(true)]
		[SRDescription("DataGridCaptionTextDescr")]
		public string CaptionText
		{
			get
			{
				return this.Caption.Text;
			}
			set
			{
				this.Caption.Text = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the grid's caption is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the caption is visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x000447E3 File Offset: 0x000429E3
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x000447F0 File Offset: 0x000429F0
		[DefaultValue(true)]
		[SRCategory("CatDisplay")]
		[SRDescription("DataGridCaptionVisibleDescr")]
		public bool CaptionVisible
		{
			get
			{
				return this.layout.CaptionVisible;
			}
			set
			{
				if (this.layout.CaptionVisible != value)
				{
					this.layout.CaptionVisible = value;
					base.PerformLayout();
					base.Invalidate();
					this.OnCaptionVisibleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.CaptionVisible" /> property has changed.</summary>
		// Token: 0x140000CE RID: 206
		// (add) Token: 0x06001465 RID: 5221 RVA: 0x00044823 File Offset: 0x00042A23
		// (remove) Token: 0x06001466 RID: 5222 RVA: 0x00044836 File Offset: 0x00042A36
		[SRCategory("CatPropertyChanged")]
		[SRDescription("DataGridOnCaptionVisibleChangedDescr")]
		public event EventHandler CaptionVisibleChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.EVENT_CAPTIONVISIBLECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EVENT_CAPTIONVISIBLECHANGED, value);
			}
		}

		/// <summary>Gets or sets which cell has the focus. Not available at design time.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridCell" /> with the focus.</returns>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x00044849 File Offset: 0x00042A49
		// (set) Token: 0x06001468 RID: 5224 RVA: 0x0004485C File Offset: 0x00042A5C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("DataGridCurrentCellDescr")]
		public DataGridCell CurrentCell
		{
			get
			{
				return new DataGridCell(this.currentRow, this.currentCol);
			}
			set
			{
				if (this.layout.dirty)
				{
					throw new ArgumentException(SR.GetString("DataGridSettingCurrentCellNotGood"));
				}
				if (value.RowNumber == this.currentRow && value.ColumnNumber == this.currentCol)
				{
					return;
				}
				if (this.DataGridRowsLength == 0 || this.myGridTable.GridColumnStyles == null || this.myGridTable.GridColumnStyles.Count == 0)
				{
					return;
				}
				this.EnsureBound();
				int num = this.currentRow;
				int num2 = this.currentCol;
				bool flag = this.gridState[32768];
				bool flag2 = false;
				bool flag3 = false;
				int num3 = value.ColumnNumber;
				int num4 = value.RowNumber;
				string text = null;
				try
				{
					int count = this.myGridTable.GridColumnStyles.Count;
					if (num3 < 0)
					{
						num3 = 0;
					}
					if (num3 >= count)
					{
						num3 = count - 1;
					}
					int num5 = this.DataGridRowsLength;
					DataGridRow[] array = this.DataGridRows;
					if (num4 < 0)
					{
						num4 = 0;
					}
					if (num4 >= num5)
					{
						num4 = num5 - 1;
					}
					if (this.currentCol != num3)
					{
						flag2 = true;
						int position = this.ListManager.Position;
						int count2 = this.ListManager.List.Count;
						this.EndEdit();
						if (this.ListManager.Position != position || count2 != this.ListManager.List.Count)
						{
							this.RecreateDataGridRows();
							if (this.ListManager.List.Count > 0)
							{
								this.currentRow = this.ListManager.Position;
								this.Edit();
							}
							else
							{
								this.currentRow = -1;
							}
							return;
						}
						this.currentCol = num3;
						this.InvalidateRow(this.currentRow);
					}
					if (this.currentRow != num4)
					{
						flag2 = true;
						int position2 = this.ListManager.Position;
						int count3 = this.ListManager.List.Count;
						this.EndEdit();
						if (this.ListManager.Position != position2 || count3 != this.ListManager.List.Count)
						{
							this.RecreateDataGridRows();
							if (this.ListManager.List.Count > 0)
							{
								this.currentRow = this.ListManager.Position;
								this.Edit();
							}
							else
							{
								this.currentRow = -1;
							}
							return;
						}
						if (this.currentRow < num5)
						{
							array[this.currentRow].OnRowLeave();
						}
						array[num4].OnRowEnter();
						this.currentRow = num4;
						if (num < num5)
						{
							this.InvalidateRow(num);
						}
						this.InvalidateRow(this.currentRow);
						if (num != this.listManager.Position)
						{
							flag3 = true;
							if (this.gridState[32768])
							{
								this.AbortEdit();
							}
						}
						else if (this.gridState[1048576])
						{
							this.ListManager.PositionChanged -= this.positionChangedHandler;
							this.ListManager.CancelCurrentEdit();
							this.ListManager.Position = this.currentRow;
							this.ListManager.PositionChanged += this.positionChangedHandler;
							array[this.DataGridRowsLength - 1] = new DataGridAddNewRow(this, this.myGridTable, this.DataGridRowsLength - 1);
							this.SetDataGridRows(array, this.DataGridRowsLength);
							this.gridState[1048576] = false;
						}
						else
						{
							this.ListManager.EndCurrentEdit();
							if (num5 != this.DataGridRowsLength)
							{
								this.currentRow = ((this.currentRow == num5 - 1) ? (this.DataGridRowsLength - 1) : this.currentRow);
							}
							if (this.currentRow == this.dataGridRowsLength - 1 && this.policy.AllowAdd)
							{
								this.AddNewRow();
							}
							else
							{
								this.ListManager.Position = this.currentRow;
							}
						}
					}
				}
				catch (Exception ex)
				{
					text = ex.Message;
				}
				if (text != null)
				{
					DialogResult dialogResult = RTLAwareMessageBox.Show(null, SR.GetString("DataGridPushedIncorrectValueIntoColumn", new object[] { text }), SR.GetString("DataGridErrorMessageBoxCaption"), MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
					if (dialogResult == DialogResult.Yes)
					{
						this.currentRow = num;
						this.currentCol = num2;
						this.InvalidateRowHeader(num4);
						this.InvalidateRowHeader(this.currentRow);
						if (flag)
						{
							this.Edit();
						}
					}
					else
					{
						if (this.currentRow == this.DataGridRowsLength - 1 && num == this.DataGridRowsLength - 2 && this.DataGridRows[this.currentRow] is DataGridAddNewRow)
						{
							num4 = num;
						}
						this.currentRow = num4;
						this.listManager.PositionChanged -= this.positionChangedHandler;
						this.listManager.CancelCurrentEdit();
						this.listManager.Position = num4;
						this.listManager.PositionChanged += this.positionChangedHandler;
						this.currentRow = num4;
						this.currentCol = num3;
						if (flag)
						{
							this.Edit();
						}
					}
				}
				if (flag2)
				{
					this.EnsureVisible(this.currentRow, this.currentCol);
					this.OnCurrentCellChanged(EventArgs.Empty);
					if (!flag3)
					{
						this.Edit();
					}
					base.AccessibilityNotifyClients(AccessibleEvents.Focus, this.CurrentCellAccIndex);
					base.AccessibilityNotifyClients(AccessibleEvents.Selection, this.CurrentCellAccIndex);
				}
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x00044D74 File Offset: 0x00042F74
		internal int CurrentCellAccIndex
		{
			get
			{
				int num = 0;
				num++;
				num += this.myGridTable.GridColumnStyles.Count;
				num += this.DataGridRows.Length;
				if (this.horizScrollBar.Visible)
				{
					num++;
				}
				if (this.vertScrollBar.Visible)
				{
					num++;
				}
				return num + (this.currentRow * this.myGridTable.GridColumnStyles.Count + this.currentCol);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.CurrentCell" /> property has changed.</summary>
		// Token: 0x140000CF RID: 207
		// (add) Token: 0x0600146A RID: 5226 RVA: 0x00044DE9 File Offset: 0x00042FE9
		// (remove) Token: 0x0600146B RID: 5227 RVA: 0x00044DFC File Offset: 0x00042FFC
		[SRCategory("CatPropertyChanged")]
		[SRDescription("DataGridOnCurrentCellChangedDescr")]
		public event EventHandler CurrentCellChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.EVENT_CURRENTCELLCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EVENT_CURRENTCELLCHANGED, value);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x00044E10 File Offset: 0x00043010
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x00044E2B File Offset: 0x0004302B
		private int CurrentColumn
		{
			get
			{
				return this.CurrentCell.ColumnNumber;
			}
			set
			{
				this.CurrentCell = new DataGridCell(this.currentRow, value);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x00044E40 File Offset: 0x00043040
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x00044E5B File Offset: 0x0004305B
		private int CurrentRow
		{
			get
			{
				return this.CurrentCell.RowNumber;
			}
			set
			{
				this.CurrentCell = new DataGridCell(value, this.currentCol);
			}
		}

		/// <summary>Gets or sets the background color of selected rows.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of selected rows. The default is the <see cref="P:System.Drawing.SystemBrushes.ActiveCaption" /> color.</returns>
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00044E6F File Offset: 0x0004306F
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x00044E7C File Offset: 0x0004307C
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
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "SelectionBackColor" }));
				}
				if (DataGrid.IsTransparentColor(value))
				{
					throw new ArgumentException(SR.GetString("DataGridTransparentSelectionBackColorNotAllowed"));
				}
				if (!value.Equals(this.selectionBackBrush.Color))
				{
					this.selectionBackBrush = new SolidBrush(value);
					this.InvalidateInside();
				}
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00044EF9 File Offset: 0x000430F9
		internal SolidBrush SelectionBackBrush
		{
			get
			{
				return this.selectionBackBrush;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x00044F01 File Offset: 0x00043101
		internal SolidBrush SelectionForeBrush
		{
			get
			{
				return this.selectionForeBrush;
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.SelectionBackColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001474 RID: 5236 RVA: 0x00044F09 File Offset: 0x00043109
		protected bool ShouldSerializeSelectionBackColor()
		{
			return !DataGrid.DefaultSelectionBackBrush.Equals(this.selectionBackBrush);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.SelectionBackColor" /> property to its default value.</summary>
		// Token: 0x06001475 RID: 5237 RVA: 0x00044F1E File Offset: 0x0004311E
		public void ResetSelectionBackColor()
		{
			if (this.ShouldSerializeSelectionBackColor())
			{
				this.SelectionBackColor = DataGrid.DefaultSelectionBackBrush.Color;
			}
		}

		/// <summary>Gets or set the foreground color of selected rows.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing the foreground color of selected rows. The default is the <see cref="P:System.Drawing.SystemBrushes.ActiveCaptionText" /> color.</returns>
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00044F38 File Offset: 0x00043138
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x00044F48 File Offset: 0x00043148
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
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "SelectionForeColor" }));
				}
				if (!value.Equals(this.selectionForeBrush.Color))
				{
					this.selectionForeBrush = new SolidBrush(value);
					this.InvalidateInside();
				}
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.SelectionForeColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001478 RID: 5240 RVA: 0x00044FAD File Offset: 0x000431AD
		protected virtual bool ShouldSerializeSelectionForeColor()
		{
			return !this.SelectionForeBrush.Equals(DataGrid.DefaultSelectionForeBrush);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.SelectionForeColor" /> property to its default value.</summary>
		// Token: 0x06001479 RID: 5241 RVA: 0x00044FC2 File Offset: 0x000431C2
		public void ResetSelectionForeColor()
		{
			if (this.ShouldSerializeSelectionForeColor())
			{
				this.SelectionForeColor = DataGrid.DefaultSelectionForeBrush.Color;
			}
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00044FDC File Offset: 0x000431DC
		internal override bool ShouldSerializeForeColor()
		{
			return !DataGrid.DefaultForeBrush.Color.Equals(this.ForeColor);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00045010 File Offset: 0x00043210
		internal override bool ShouldSerializeBackColor()
		{
			return !DataGrid.DefaultBackBrush.Color.Equals(this.BackColor);
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x00045043 File Offset: 0x00043243
		internal DataGridRow[] DataGridRows
		{
			get
			{
				if (this.dataGridRows == null)
				{
					this.CreateDataGridRows();
				}
				return this.dataGridRows;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00045059 File Offset: 0x00043259
		internal DataGridToolTip ToolTipProvider
		{
			get
			{
				return this.toolTipProvider;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x00045061 File Offset: 0x00043261
		// (set) Token: 0x0600147F RID: 5247 RVA: 0x00045069 File Offset: 0x00043269
		internal int ToolTipId
		{
			get
			{
				return this.toolTipId;
			}
			set
			{
				this.toolTipId = value;
			}
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00045074 File Offset: 0x00043274
		private void ResetToolTip()
		{
			for (int i = 0; i < this.ToolTipId; i++)
			{
				this.ToolTipProvider.RemoveToolTip(new IntPtr(i));
			}
			if (!this.parentRows.IsEmpty())
			{
				bool flag = this.isRightToLeft();
				int detailsButtonWidth = this.Caption.GetDetailsButtonWidth();
				Rectangle backButtonRect = this.Caption.GetBackButtonRect(this.layout.Caption, flag, detailsButtonWidth);
				Rectangle detailsButtonRect = this.Caption.GetDetailsButtonRect(this.layout.Caption, flag);
				backButtonRect.X = this.MirrorRectangle(backButtonRect, this.layout.Inside, this.isRightToLeft());
				detailsButtonRect.X = this.MirrorRectangle(detailsButtonRect, this.layout.Inside, this.isRightToLeft());
				this.ToolTipProvider.AddToolTip(SR.GetString("DataGridCaptionBackButtonToolTip"), new IntPtr(0), backButtonRect);
				this.ToolTipProvider.AddToolTip(SR.GetString("DataGridCaptionDetailsButtonToolTip"), new IntPtr(1), detailsButtonRect);
				this.ToolTipId = 2;
				return;
			}
			this.ToolTipId = 0;
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00045180 File Offset: 0x00043380
		private void CreateDataGridRows()
		{
			CurrencyManager currencyManager = this.ListManager;
			DataGridTableStyle dataGridTableStyle = this.myGridTable;
			this.InitializeColumnWidths();
			if (currencyManager == null)
			{
				this.SetDataGridRows(new DataGridRow[0], 0);
				return;
			}
			int num = currencyManager.Count;
			if (this.policy.AllowAdd)
			{
				num++;
			}
			DataGridRow[] array = new DataGridRow[num];
			for (int i = 0; i < currencyManager.Count; i++)
			{
				array[i] = new DataGridRelationshipRow(this, dataGridTableStyle, i);
			}
			if (this.policy.AllowAdd)
			{
				this.addNewRow = new DataGridAddNewRow(this, dataGridTableStyle, num - 1);
				array[num - 1] = this.addNewRow;
			}
			else
			{
				this.addNewRow = null;
			}
			this.SetDataGridRows(array, num);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0004522C File Offset: 0x0004342C
		private void RecreateDataGridRows()
		{
			int num = 0;
			CurrencyManager currencyManager = this.ListManager;
			if (currencyManager != null)
			{
				num = currencyManager.Count;
				if (this.policy.AllowAdd)
				{
					num++;
				}
			}
			this.SetDataGridRows(null, num);
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00045268 File Offset: 0x00043468
		internal void SetDataGridRows(DataGridRow[] newRows, int newRowsLength)
		{
			this.dataGridRows = newRows;
			this.dataGridRowsLength = newRowsLength;
			this.vertScrollBar.Maximum = Math.Max(0, this.DataGridRowsLength - 1);
			if (this.firstVisibleRow > newRowsLength)
			{
				this.vertScrollBar.Value = 0;
				this.firstVisibleRow = 0;
			}
			this.ResetUIState();
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x000452BE File Offset: 0x000434BE
		internal int DataGridRowsLength
		{
			get
			{
				return this.dataGridRowsLength;
			}
		}

		/// <summary>Gets or sets the data source that the grid is displaying data for.</summary>
		/// <returns>An object that functions as a data source.</returns>
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x000452C6 File Offset: 0x000434C6
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x000452D0 File Offset: 0x000434D0
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[AttributeProvider(typeof(IListSource))]
		[SRDescription("DataGridDataSourceDescr")]
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (value != null && !(value is IList) && !(value is IListSource))
				{
					throw new ArgumentException(SR.GetString("BadDataSourceForComplexBinding"));
				}
				if (this.dataSource != null && this.dataSource.Equals(value))
				{
					return;
				}
				if ((value == null || value == Convert.DBNull) && this.DataMember != null && this.DataMember.Length != 0)
				{
					this.dataSource = null;
					this.DataMember = "";
					return;
				}
				if (value != null)
				{
					this.EnforceValidDataMember(value);
				}
				this.ResetParentRows();
				this.Set_ListManager(value, this.DataMember, false);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.DataSource" /> property value has changed.</summary>
		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x06001487 RID: 5255 RVA: 0x00045368 File Offset: 0x00043568
		// (remove) Token: 0x06001488 RID: 5256 RVA: 0x0004537B File Offset: 0x0004357B
		[SRCategory("CatPropertyChanged")]
		[SRDescription("DataGridOnDataSourceChangedDescr")]
		public event EventHandler DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.EVENT_DATASOURCECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EVENT_DATASOURCECHANGED, value);
			}
		}

		/// <summary>Gets or sets the specific list in a <see cref="P:System.Windows.Forms.DataGrid.DataSource" /> for which the <see cref="T:System.Windows.Forms.DataGrid" /> control displays a grid.</summary>
		/// <returns>A list in a <see cref="P:System.Windows.Forms.DataGrid.DataSource" />. The default is an empty string ("").</returns>
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0004538E File Offset: 0x0004358E
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x00045396 File Offset: 0x00043596
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("DataGridDataMemberDescr")]
		public string DataMember
		{
			get
			{
				return this.dataMember;
			}
			set
			{
				if (this.dataMember != null && this.dataMember.Equals(value))
				{
					return;
				}
				this.ResetParentRows();
				this.Set_ListManager(this.DataSource, value, false);
			}
		}

		/// <summary>Sets the <see cref="P:System.Windows.Forms.DataGrid.DataSource" /> and <see cref="P:System.Windows.Forms.DataGrid.DataMember" /> properties at run time.</summary>
		/// <param name="dataSource">The data source for the <see cref="T:System.Windows.Forms.DataGrid" /> control.</param>
		/// <param name="dataMember">The <see cref="P:System.Windows.Forms.DataGrid.DataMember" /> string that specifies the table to bind to within the object returned by the <see cref="P:System.Windows.Forms.DataGrid.DataSource" /> property.</param>
		/// <exception cref="T:System.ArgumentException">One or more of the arguments are invalid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSource" /> argument is <see langword="null" />.</exception>
		// Token: 0x0600148B RID: 5259 RVA: 0x000453C4 File Offset: 0x000435C4
		public void SetDataBinding(object dataSource, string dataMember)
		{
			this.parentRows.Clear();
			this.originalState = null;
			this.caption.BackButtonActive = (this.caption.DownButtonActive = (this.caption.BackButtonVisible = false));
			this.caption.SetDownButtonDirection(!this.layout.ParentRowsVisible);
			this.Set_ListManager(dataSource, dataMember, false);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.CurrencyManager" /> for this <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CurrencyManager" /> for this <see cref="T:System.Windows.Forms.DataGrid" /> control.</returns>
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x0004542D File Offset: 0x0004362D
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x0004546A File Offset: 0x0004366A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("DataGridListManagerDescr")]
		protected internal CurrencyManager ListManager
		{
			get
			{
				if (this.listManager == null && this.BindingContext != null && this.DataSource != null)
				{
					return (CurrencyManager)this.BindingContext[this.DataSource, this.DataMember];
				}
				return this.listManager;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("DataGridSetListManager"));
			}
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0004547B File Offset: 0x0004367B
		internal void Set_ListManager(object newDataSource, string newDataMember, bool force)
		{
			this.Set_ListManager(newDataSource, newDataMember, force, true);
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00045488 File Offset: 0x00043688
		internal void Set_ListManager(object newDataSource, string newDataMember, bool force, bool forceColumnCreation)
		{
			bool flag = this.DataSource != newDataSource;
			bool flag2 = this.DataMember != newDataMember;
			if (!force && !flag && !flag2 && this.gridState[2097152])
			{
				return;
			}
			this.gridState[2097152] = true;
			if (this.toBeDisposedEditingControl != null)
			{
				base.Controls.Remove(this.toBeDisposedEditingControl);
				this.toBeDisposedEditingControl = null;
			}
			bool flag3 = true;
			try
			{
				this.UpdateListManager();
				if (this.listManager != null)
				{
					this.UnWireDataSource();
				}
				CurrencyManager currencyManager = this.listManager;
				if (newDataSource != null && this.BindingContext != null && newDataSource != Convert.DBNull)
				{
					this.listManager = (CurrencyManager)this.BindingContext[newDataSource, newDataMember];
				}
				else
				{
					this.listManager = null;
				}
				this.dataSource = newDataSource;
				this.dataMember = ((newDataMember == null) ? "" : newDataMember);
				bool flag4 = this.listManager != currencyManager;
				if (this.listManager != null)
				{
					this.WireDataSource();
					this.policy.UpdatePolicy(this.listManager, this.ReadOnly);
				}
				if (!this.Initializing && this.listManager == null)
				{
					if (base.ContainsFocus && this.ParentInternal == null)
					{
						for (int i = 0; i < base.Controls.Count; i++)
						{
							if (base.Controls[i].Focused)
							{
								this.toBeDisposedEditingControl = base.Controls[i];
								break;
							}
						}
						if (this.toBeDisposedEditingControl == this.horizScrollBar || this.toBeDisposedEditingControl == this.vertScrollBar)
						{
							this.toBeDisposedEditingControl = null;
						}
					}
					this.SetDataGridRows(null, 0);
					this.defaultTableStyle.GridColumnStyles.Clear();
					this.SetDataGridTable(this.defaultTableStyle, forceColumnCreation);
					if (this.toBeDisposedEditingControl != null)
					{
						base.Controls.Add(this.toBeDisposedEditingControl);
					}
				}
				if (flag4 || this.gridState[4194304])
				{
					if (base.Visible)
					{
						base.BeginUpdateInternal();
					}
					if (this.listManager != null)
					{
						this.defaultTableStyle.GridColumnStyles.ResetDefaultColumnCollection();
						DataGridTableStyle dataGridTableStyle = this.dataGridTables[this.listManager.GetListName()];
						if (dataGridTableStyle == null)
						{
							this.SetDataGridTable(this.defaultTableStyle, forceColumnCreation);
						}
						else
						{
							this.SetDataGridTable(dataGridTableStyle, forceColumnCreation);
						}
						this.currentRow = ((this.listManager.Position == -1) ? 0 : this.listManager.Position);
					}
					this.RecreateDataGridRows();
					if (base.Visible)
					{
						base.EndUpdateInternal();
					}
					flag3 = false;
					this.ComputeMinimumRowHeaderWidth();
					if (this.myGridTable.IsDefault)
					{
						this.RowHeaderWidth = Math.Max(this.minRowHeaderWidth, this.RowHeaderWidth);
					}
					else
					{
						this.myGridTable.RowHeaderWidth = Math.Max(this.minRowHeaderWidth, this.RowHeaderWidth);
					}
					this.ListHasErrors = this.DataGridSourceHasErrors();
					this.ResetUIState();
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
			finally
			{
				this.gridState[2097152] = false;
				if (flag3 && base.Visible)
				{
					base.EndUpdateInternal();
				}
			}
		}

		/// <summary>Gets or sets index of the row that currently has focus.</summary>
		/// <returns>The zero-based index of the current row.</returns>
		/// <exception cref="T:System.Exception">There is no <see cref="T:System.Windows.Forms.CurrencyManager" />.</exception>
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x000457BC File Offset: 0x000439BC
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x00045820 File Offset: 0x00043A20
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[SRDescription("DataGridSelectedIndexDescr")]
		public int CurrentRowIndex
		{
			get
			{
				if (this.originalState == null)
				{
					if (this.listManager != null)
					{
						return this.listManager.Position;
					}
					return -1;
				}
				else
				{
					if (this.BindingContext == null)
					{
						return -1;
					}
					CurrencyManager currencyManager = (CurrencyManager)this.BindingContext[this.originalState.DataSource, this.originalState.DataMember];
					return currencyManager.Position;
				}
			}
			set
			{
				if (this.listManager == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridSetSelectIndex"));
				}
				if (this.originalState == null)
				{
					this.listManager.Position = value;
					this.currentRow = value;
					return;
				}
				CurrencyManager currencyManager = (CurrencyManager)this.BindingContext[this.originalState.DataSource, this.originalState.DataMember];
				currencyManager.Position = value;
				this.originalState.LinkingRow = this.originalState.DataGridRows[value];
				base.Invalidate();
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects for the grid.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> that represents the collection of <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects.</returns>
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x000458AD File Offset: 0x00043AAD
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("DataGridGridTablesDescr")]
		public GridTableStylesCollection TableStyles
		{
			get
			{
				return this.dataGridTables;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x000458B5 File Offset: 0x00043AB5
		internal new int FontHeight
		{
			get
			{
				return this.fontHeight;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x000458BD File Offset: 0x00043ABD
		internal AccessibleObject ParentRowsAccessibleObject
		{
			get
			{
				return this.parentRows.AccessibleObject;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x000458CA File Offset: 0x00043ACA
		internal Rectangle ParentRowsBounds
		{
			get
			{
				return this.layout.ParentRows;
			}
		}

		/// <summary>Gets or sets the color of the grid lines.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the grid lines. The default is the system color for controls (<see cref="P:System.Drawing.SystemColors.Control" />).</returns>
		/// <exception cref="T:System.ArgumentException">The value is not set.</exception>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x000458D7 File Offset: 0x00043AD7
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x000458E4 File Offset: 0x00043AE4
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
				if (this.gridLineBrush.Color != value)
				{
					if (value.IsEmpty)
					{
						throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "GridLineColor" }));
					}
					this.gridLineBrush = new SolidBrush(value);
					base.Invalidate(this.layout.Data);
				}
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.GridLineColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001498 RID: 5272 RVA: 0x00045948 File Offset: 0x00043B48
		protected virtual bool ShouldSerializeGridLineColor()
		{
			return !this.GridLineBrush.Equals(DataGrid.DefaultGridLineBrush);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.GridLineColor" /> property to its default value.</summary>
		// Token: 0x06001499 RID: 5273 RVA: 0x0004595D File Offset: 0x00043B5D
		public void ResetGridLineColor()
		{
			if (this.ShouldSerializeGridLineColor())
			{
				this.GridLineColor = DataGrid.DefaultGridLineBrush.Color;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00045977 File Offset: 0x00043B77
		internal SolidBrush GridLineBrush
		{
			get
			{
				return this.gridLineBrush;
			}
		}

		/// <summary>Gets or sets the line style of the grid.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridLineStyle" /> values. The default is <see langword="Solid" />.</returns>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0004597F File Offset: 0x00043B7F
		// (set) Token: 0x0600149C RID: 5276 RVA: 0x00045988 File Offset: 0x00043B88
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
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridLineStyle));
				}
				if (this.gridLineStyle != value)
				{
					this.gridLineStyle = value;
					this.myGridTable.ResetRelationsUI();
					base.Invalidate(this.layout.Data);
				}
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x000459E7 File Offset: 0x00043BE7
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

		/// <summary>Gets or sets the way parent row labels are displayed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridParentRowsLabelStyle" /> values. The default is <see langword="Both" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The enumerator was not valid.</exception>
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x000459F5 File Offset: 0x00043BF5
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x00045A00 File Offset: 0x00043C00
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(DataGridParentRowsLabelStyle.Both)]
		[SRCategory("CatDisplay")]
		[SRDescription("DataGridParentRowsLabelStyleDescr")]
		public DataGridParentRowsLabelStyle ParentRowsLabelStyle
		{
			get
			{
				return this.parentRowsLabels;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridParentRowsLabelStyle));
				}
				if (this.parentRowsLabels != value)
				{
					this.parentRowsLabels = value;
					base.Invalidate(this.layout.ParentRows);
					this.OnParentRowsLabelStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the label style of the parent row is changed.</summary>
		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x060014A0 RID: 5280 RVA: 0x00045A5F File Offset: 0x00043C5F
		// (remove) Token: 0x060014A1 RID: 5281 RVA: 0x00045A72 File Offset: 0x00043C72
		[SRCategory("CatPropertyChanged")]
		[SRDescription("DataGridOnParentRowsLabelStyleChangedDescr")]
		public event EventHandler ParentRowsLabelStyleChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.EVENT_PARENTROWSLABELSTYLECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EVENT_PARENTROWSLABELSTYLECHANGED, value);
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00045A85 File Offset: 0x00043C85
		internal bool Initializing
		{
			get
			{
				return this.inInit;
			}
		}

		/// <summary>Gets the index of the first visible column in a grid.</summary>
		/// <returns>The index of a <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00045A8D File Offset: 0x00043C8D
		[Browsable(false)]
		[SRDescription("DataGridFirstVisibleColumnDescr")]
		public int FirstVisibleColumn
		{
			get
			{
				return this.firstVisibleCol;
			}
		}

		/// <summary>Gets or sets a value indicating whether the grid displays in flat mode.</summary>
		/// <returns>
		///   <see langword="true" /> if the grid is displayed flat; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x00045A95 File Offset: 0x00043C95
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x00045AA4 File Offset: 0x00043CA4
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridFlatModeDescr")]
		public bool FlatMode
		{
			get
			{
				return this.gridState[64];
			}
			set
			{
				if (value != this.FlatMode)
				{
					this.gridState[64] = value;
					base.Invalidate(this.layout.Inside);
					this.OnFlatModeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.FlatMode" /> has changed.</summary>
		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x060014A6 RID: 5286 RVA: 0x00045AD9 File Offset: 0x00043CD9
		// (remove) Token: 0x060014A7 RID: 5287 RVA: 0x00045AEC File Offset: 0x00043CEC
		[SRCategory("CatPropertyChanged")]
		[SRDescription("DataGridOnFlatModeChangedDescr")]
		public event EventHandler FlatModeChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.EVENT_FLATMODECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EVENT_FLATMODECHANGED, value);
			}
		}

		/// <summary>Gets or sets the background color of all row and column headers.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of row and column headers. The default is the system color for controls, <see cref="P:System.Drawing.SystemColors.Control" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">While trying to set the property, a <see langword="Color.Empty" /> was passed.</exception>
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x00045AFF File Offset: 0x00043CFF
		// (set) Token: 0x060014A9 RID: 5289 RVA: 0x00045B0C File Offset: 0x00043D0C
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
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "HeaderBackColor" }));
				}
				if (DataGrid.IsTransparentColor(value))
				{
					throw new ArgumentException(SR.GetString("DataGridTransparentHeaderBackColorNotAllowed"));
				}
				if (!value.Equals(this.headerBackBrush.Color))
				{
					this.headerBackBrush = new SolidBrush(value);
					if (this.layout.RowHeadersVisible)
					{
						base.Invalidate(this.layout.RowHeaders);
					}
					if (this.layout.ColumnHeadersVisible)
					{
						base.Invalidate(this.layout.ColumnHeaders);
					}
					base.Invalidate(this.layout.TopLeftHeader);
				}
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x00045BD0 File Offset: 0x00043DD0
		internal SolidBrush HeaderBackBrush
		{
			get
			{
				return this.headerBackBrush;
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.HeaderBackColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014AB RID: 5291 RVA: 0x00045BD8 File Offset: 0x00043DD8
		protected virtual bool ShouldSerializeHeaderBackColor()
		{
			return !this.HeaderBackBrush.Equals(DataGrid.DefaultHeaderBackBrush);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.HeaderBackColor" /> property to its default value.</summary>
		// Token: 0x060014AC RID: 5292 RVA: 0x00045BED File Offset: 0x00043DED
		public void ResetHeaderBackColor()
		{
			if (this.ShouldSerializeHeaderBackColor())
			{
				this.HeaderBackColor = DataGrid.DefaultHeaderBackBrush.Color;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x00045C07 File Offset: 0x00043E07
		internal SolidBrush BackgroundBrush
		{
			get
			{
				return this.backgroundBrush;
			}
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00045C0F File Offset: 0x00043E0F
		private void ResetBackgroundColor()
		{
			if (this.backgroundBrush != null && this.BackgroundBrush != DataGrid.DefaultBackgroundBrush)
			{
				this.backgroundBrush.Dispose();
				this.backgroundBrush = null;
			}
			this.backgroundBrush = DataGrid.DefaultBackgroundBrush;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.BackgroundColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014AF RID: 5295 RVA: 0x00045C43 File Offset: 0x00043E43
		protected virtual bool ShouldSerializeBackgroundColor()
		{
			return !this.BackgroundBrush.Equals(DataGrid.DefaultBackgroundBrush);
		}

		/// <summary>Gets or sets the color of the non-row area of the grid.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the grid's background. The default is the <see cref="P:System.Drawing.SystemColors.AppWorkspace" /> color.</returns>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00045C58 File Offset: 0x00043E58
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x00045C68 File Offset: 0x00043E68
		[SRCategory("CatColors")]
		[SRDescription("DataGridBackgroundColorDescr")]
		public Color BackgroundColor
		{
			get
			{
				return this.backgroundBrush.Color;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "BackgroundColor" }));
				}
				if (!value.Equals(this.backgroundBrush.Color))
				{
					if (this.backgroundBrush != null && this.BackgroundBrush != DataGrid.DefaultBackgroundBrush)
					{
						this.backgroundBrush.Dispose();
						this.backgroundBrush = null;
					}
					this.backgroundBrush = new SolidBrush(value);
					base.Invalidate(this.layout.Inside);
					this.OnBackgroundColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.BackgroundColor" /> has changed.</summary>
		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x060014B2 RID: 5298 RVA: 0x00045D0A File Offset: 0x00043F0A
		// (remove) Token: 0x060014B3 RID: 5299 RVA: 0x00045D1D File Offset: 0x00043F1D
		[SRCategory("CatPropertyChanged")]
		[SRDescription("DataGridOnBackgroundColorChangedDescr")]
		public event EventHandler BackgroundColorChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.EVENT_BACKGROUNDCOLORCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EVENT_BACKGROUNDCOLORCHANGED, value);
			}
		}

		/// <summary>Gets or sets the font used for column headers.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> that represents the header text.</returns>
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00045D30 File Offset: 0x00043F30
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x00045D48 File Offset: 0x00043F48
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridHeaderFontDescr")]
		public Font HeaderFont
		{
			get
			{
				if (this.headerFont != null)
				{
					return this.headerFont;
				}
				return this.Font;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("HeaderFont");
				}
				if (!value.Equals(this.headerFont))
				{
					this.headerFont = value;
					this.RecalculateFonts();
					base.PerformLayout();
					base.Invalidate(this.layout.Inside);
				}
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.HeaderFont" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014B6 RID: 5302 RVA: 0x00045D95 File Offset: 0x00043F95
		protected bool ShouldSerializeHeaderFont()
		{
			return this.headerFont != null;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.HeaderFont" /> property to its default value.</summary>
		// Token: 0x060014B7 RID: 5303 RVA: 0x00045DA0 File Offset: 0x00043FA0
		public void ResetHeaderFont()
		{
			if (this.headerFont != null)
			{
				this.headerFont = null;
				this.RecalculateFonts();
				base.PerformLayout();
				base.Invalidate(this.layout.Inside);
			}
		}

		/// <summary>Gets or sets the foreground color of headers.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the grid's column headers, including the column header text and the plus/minus glyphs. The default is <see cref="P:System.Drawing.SystemColors.ControlText" /> color.</returns>
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x00045DCE File Offset: 0x00043FCE
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x00045DDC File Offset: 0x00043FDC
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
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "HeaderForeColor" }));
				}
				if (!value.Equals(this.headerForePen.Color))
				{
					this.headerForePen = new Pen(value);
					this.headerForeBrush = new SolidBrush(value);
					if (this.layout.RowHeadersVisible)
					{
						base.Invalidate(this.layout.RowHeaders);
					}
					if (this.layout.ColumnHeadersVisible)
					{
						base.Invalidate(this.layout.ColumnHeaders);
					}
					base.Invalidate(this.layout.TopLeftHeader);
				}
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.HeaderForeColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014BA RID: 5306 RVA: 0x00045E94 File Offset: 0x00044094
		protected virtual bool ShouldSerializeHeaderForeColor()
		{
			return !this.HeaderForePen.Equals(DataGrid.DefaultHeaderForePen);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.HeaderForeColor" /> property to its default value.</summary>
		// Token: 0x060014BB RID: 5307 RVA: 0x00045EA9 File Offset: 0x000440A9
		public void ResetHeaderForeColor()
		{
			if (this.ShouldSerializeHeaderForeColor())
			{
				this.HeaderForeColor = DataGrid.DefaultHeaderForeBrush.Color;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x00045EC3 File Offset: 0x000440C3
		internal SolidBrush HeaderForeBrush
		{
			get
			{
				return this.headerForeBrush;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00045ECB File Offset: 0x000440CB
		internal Pen HeaderForePen
		{
			get
			{
				return this.headerForePen;
			}
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00045ED3 File Offset: 0x000440D3
		private void ResetHorizontalOffset()
		{
			this.horizontalOffset = 0;
			this.negOffset = 0;
			this.firstVisibleCol = 0;
			this.numVisibleCols = 0;
			this.lastTotallyVisibleCol = -1;
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x00045EF8 File Offset: 0x000440F8
		// (set) Token: 0x060014C0 RID: 5312 RVA: 0x00045F00 File Offset: 0x00044100
		internal int HorizontalOffset
		{
			get
			{
				return this.horizontalOffset;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				int columnWidthSum = this.GetColumnWidthSum();
				int num = columnWidthSum - this.layout.Data.Width;
				if (value > num && num > 0)
				{
					value = num;
				}
				if (value == this.horizontalOffset)
				{
					return;
				}
				int num2 = this.horizontalOffset - value;
				this.horizScrollBar.Value = value;
				Rectangle rectangle = this.layout.Data;
				if (this.layout.ColumnHeadersVisible)
				{
					rectangle = Rectangle.Union(rectangle, this.layout.ColumnHeaders);
				}
				this.horizontalOffset = value;
				this.firstVisibleCol = this.ComputeFirstVisibleColumn();
				this.ComputeVisibleColumns();
				if (this.gridState[131072])
				{
					if (this.currentCol >= this.firstVisibleCol && this.currentCol < this.firstVisibleCol + this.numVisibleCols - 1 && (this.gridState[32768] || this.gridState[16384]))
					{
						this.Edit();
					}
					else
					{
						this.EndEdit();
					}
					this.gridState[131072] = false;
				}
				else
				{
					this.EndEdit();
				}
				NativeMethods.RECT[] array = this.CreateScrollableRegion(rectangle);
				this.ScrollRectangles(array, num2);
				this.OnScroll(EventArgs.Empty);
			}
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0004603C File Offset: 0x0004423C
		private void ScrollRectangles(NativeMethods.RECT[] rects, int change)
		{
			if (rects != null)
			{
				if (this.isRightToLeft())
				{
					change = -change;
				}
				foreach (NativeMethods.RECT rect in rects)
				{
					SafeNativeMethods.ScrollWindow(new HandleRef(this, base.Handle), change, 0, ref rect, ref rect);
				}
			}
		}

		/// <summary>Gets the horizontal scroll bar for the grid.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ScrollBar" /> for the grid.</returns>
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00046086 File Offset: 0x00044286
		[SRDescription("DataGridHorizScrollBarDescr")]
		protected ScrollBar HorizScrollBar
		{
			get
			{
				return this.horizScrollBar;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x0004608E File Offset: 0x0004428E
		internal bool LedgerStyle
		{
			get
			{
				return this.gridState[32];
			}
		}

		/// <summary>Gets or sets the color of the text that you can click to navigate to a child table.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of text that is clicked to navigate to a child table. The default is <see cref="P:System.Drawing.SystemColors.HotTrack" />.</returns>
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0004609D File Offset: 0x0004429D
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x000460AC File Offset: 0x000442AC
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
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("DataGridEmptyColor", new object[] { "LinkColor" }));
				}
				if (!this.linkBrush.Color.Equals(value))
				{
					this.linkBrush = new SolidBrush(value);
					base.Invalidate(this.layout.Data);
				}
			}
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0004611E File Offset: 0x0004431E
		internal virtual bool ShouldSerializeLinkColor()
		{
			return !this.LinkBrush.Equals(DataGrid.DefaultLinkBrush);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.LinkColor" /> property to its default value.</summary>
		// Token: 0x060014C7 RID: 5319 RVA: 0x00046133 File Offset: 0x00044333
		public void ResetLinkColor()
		{
			if (this.ShouldSerializeLinkColor())
			{
				this.LinkColor = DataGrid.DefaultLinkBrush.Color;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0004614D File Offset: 0x0004434D
		internal Brush LinkBrush
		{
			get
			{
				return this.linkBrush;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The color displayed when hovering over link text.</returns>
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00046155 File Offset: 0x00044355
		// (set) Token: 0x060014CA RID: 5322 RVA: 0x000070A6 File Offset: 0x000052A6
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

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.LinkHoverColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014CB RID: 5323 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool ShouldSerializeLinkHoverColor()
		{
			return false;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGrid.LinkHoverColor" /> property to its default value.</summary>
		// Token: 0x060014CC RID: 5324 RVA: 0x000070A6 File Offset: 0x000052A6
		public void ResetLinkHoverColor()
		{
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x0004615D File Offset: 0x0004435D
		internal Font LinkFont
		{
			get
			{
				return this.linkFont;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x00046165 File Offset: 0x00044365
		internal int LinkFontHeight
		{
			get
			{
				return this.linkFontHeight;
			}
		}

		/// <summary>Gets or sets a value indicating whether navigation is allowed.</summary>
		/// <returns>
		///   <see langword="true" /> if navigation is allowed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x0004616D File Offset: 0x0004436D
		// (set) Token: 0x060014D0 RID: 5328 RVA: 0x00046180 File Offset: 0x00044380
		[DefaultValue(true)]
		[SRDescription("DataGridNavigationModeDescr")]
		[SRCategory("CatBehavior")]
		public bool AllowNavigation
		{
			get
			{
				return this.gridState[8192];
			}
			set
			{
				if (this.AllowNavigation != value)
				{
					this.gridState[8192] = value;
					this.Caption.BackButtonActive = !this.parentRows.IsEmpty() && value;
					this.Caption.BackButtonVisible = this.Caption.BackButtonActive;
					this.RecreateDataGridRows();
					this.OnAllowNavigationChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.AllowNavigation" /> property has changed.</summary>
		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x060014D1 RID: 5329 RVA: 0x000461E9 File Offset: 0x000443E9
		// (remove) Token: 0x060014D2 RID: 5330 RVA: 0x000461FC File Offset: 0x000443FC
		[SRCategory("CatPropertyChanged")]
		[SRDescription("DataGridOnNavigationModeChangedDescr")]
		public event EventHandler AllowNavigationChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.EVENT_ALLOWNAVIGATIONCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EVENT_ALLOWNAVIGATIONCHANGED, value);
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The type of cursor to display as the mouse pointer moves over the object.</returns>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x0001A0A0 File Offset: 0x000182A0
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x0001A0A8 File Offset: 0x000182A8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGrid.Cursor" /> property changes.</summary>
		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x060014D5 RID: 5333 RVA: 0x0004620F File Offset: 0x0004440F
		// (remove) Token: 0x060014D6 RID: 5334 RVA: 0x00046218 File Offset: 0x00044418
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CursorChanged
		{
			add
			{
				base.CursorChanged += value;
			}
			remove
			{
				base.CursorChanged -= value;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The background image associated with the control.</returns>
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x060014D8 RID: 5336 RVA: 0x00011884 File Offset: 0x0000FA84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x000118A7 File Offset: 0x0000FAA7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGrid.BackgroundImage" /> property changes.</summary>
		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x060014DB RID: 5339 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x060014DC RID: 5340 RVA: 0x00011896 File Offset: 0x0000FA96
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGrid.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x060014DD RID: 5341 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x060014DE RID: 5342 RVA: 0x000118B9 File Offset: 0x0000FAB9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		/// <summary>Gets or sets the background color of parent rows.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of parent rows. The default is the <see cref="P:System.Drawing.SystemColors.Control" /> color.</returns>
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x00046221 File Offset: 0x00044421
		// (set) Token: 0x060014E0 RID: 5344 RVA: 0x0004622E File Offset: 0x0004442E
		[SRCategory("CatColors")]
		[SRDescription("DataGridParentRowsBackColorDescr")]
		public Color ParentRowsBackColor
		{
			get
			{
				return this.parentRows.BackColor;
			}
			set
			{
				if (DataGrid.IsTransparentColor(value))
				{
					throw new ArgumentException(SR.GetString("DataGridTransparentParentRowsBackColorNotAllowed"));
				}
				this.parentRows.BackColor = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00046254 File Offset: 0x00044454
		internal SolidBrush ParentRowsBackBrush
		{
			get
			{
				return this.parentRows.BackBrush;
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.ParentRowsBackColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has been changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014E2 RID: 5346 RVA: 0x00046261 File Offset: 0x00044461
		protected virtual bool ShouldSerializeParentRowsBackColor()
		{
			return !this.ParentRowsBackBrush.Equals(DataGrid.DefaultParentRowsBackBrush);
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00046276 File Offset: 0x00044476
		private void ResetParentRowsBackColor()
		{
			if (this.ShouldSerializeParentRowsBackColor())
			{
				this.parentRows.BackBrush = DataGrid.DefaultParentRowsBackBrush;
			}
		}

		/// <summary>Gets or sets the foreground color of parent rows.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of parent rows. The default is the <see cref="P:System.Drawing.SystemColors.WindowText" /> color.</returns>
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00046290 File Offset: 0x00044490
		// (set) Token: 0x060014E5 RID: 5349 RVA: 0x0004629D File Offset: 0x0004449D
		[SRCategory("CatColors")]
		[SRDescription("DataGridParentRowsForeColorDescr")]
		public Color ParentRowsForeColor
		{
			get
			{
				return this.parentRows.ForeColor;
			}
			set
			{
				this.parentRows.ForeColor = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x000462AB File Offset: 0x000444AB
		internal SolidBrush ParentRowsForeBrush
		{
			get
			{
				return this.parentRows.ForeBrush;
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.ParentRowsForeColor" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has been changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014E7 RID: 5351 RVA: 0x000462B8 File Offset: 0x000444B8
		protected virtual bool ShouldSerializeParentRowsForeColor()
		{
			return !this.ParentRowsForeBrush.Equals(DataGrid.DefaultParentRowsForeBrush);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x000462CD File Offset: 0x000444CD
		private void ResetParentRowsForeColor()
		{
			if (this.ShouldSerializeParentRowsForeColor())
			{
				this.parentRows.ForeBrush = DataGrid.DefaultParentRowsForeBrush;
			}
		}

		/// <summary>Gets or sets the default width of the grid columns in pixels.</summary>
		/// <returns>The default width (in pixels) of columns in the grid.</returns>
		/// <exception cref="T:System.ArgumentException">The property value is less than 0.</exception>
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x000462E7 File Offset: 0x000444E7
		// (set) Token: 0x060014EA RID: 5354 RVA: 0x000462EF File Offset: 0x000444EF
		[DefaultValue(75)]
		[SRCategory("CatLayout")]
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
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("DataGridColumnWidth"), "PreferredColumnWidth");
				}
				if (this.preferredColumnWidth != value)
				{
					this.preferredColumnWidth = value;
				}
			}
		}

		/// <summary>Gets or sets the preferred row height for the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <returns>The height of a row.</returns>
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x0004631A File Offset: 0x0004451A
		// (set) Token: 0x060014EC RID: 5356 RVA: 0x00046322 File Offset: 0x00044522
		[SRCategory("CatLayout")]
		[SRDescription("DataGridPreferredRowHeightDescr")]
		public int PreferredRowHeight
		{
			get
			{
				return this.prefferedRowHeight;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("DataGridRowRowHeight"));
				}
				this.prefferedRowHeight = value;
			}
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0004633F File Offset: 0x0004453F
		private void ResetPreferredRowHeight()
		{
			this.prefferedRowHeight = DataGrid.defaultFontHeight + 3;
		}

		/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGrid.PreferredRowHeight" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014EE RID: 5358 RVA: 0x0004634E File Offset: 0x0004454E
		protected bool ShouldSerializePreferredRowHeight()
		{
			return this.prefferedRowHeight != DataGrid.defaultFontHeight + 3;
		}

		/// <summary>Gets or sets a value indicating whether the grid is in read-only mode.</summary>
		/// <returns>
		///   <see langword="true" /> if the grid is in read-only mode; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x00046362 File Offset: 0x00044562
		// (set) Token: 0x060014F0 RID: 5360 RVA: 0x00046374 File Offset: 0x00044574
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridReadOnlyDescr")]
		public bool ReadOnly
		{
			get
			{
				return this.gridState[4096];
			}
			set
			{
				if (this.ReadOnly != value)
				{
					bool flag = false;
					if (value)
					{
						flag = this.policy.AllowAdd;
						this.policy.AllowRemove = false;
						this.policy.AllowEdit = false;
						this.policy.AllowAdd = false;
					}
					else
					{
						flag |= this.policy.UpdatePolicy(this.listManager, value);
					}
					this.gridState[4096] = value;
					DataGridRow[] array = this.DataGridRows;
					if (flag)
					{
						this.RecreateDataGridRows();
						DataGridRow[] array2 = this.DataGridRows;
						int num = Math.Min(array2.Length, array.Length);
						for (int i = 0; i < num; i++)
						{
							if (array[i].Selected)
							{
								array2[i].Selected = true;
							}
						}
					}
					base.PerformLayout();
					this.InvalidateInside();
					this.OnReadOnlyChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.ReadOnly" /> property value changes.</summary>
		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x060014F1 RID: 5361 RVA: 0x00046449 File Offset: 0x00044649
		// (remove) Token: 0x060014F2 RID: 5362 RVA: 0x0004645C File Offset: 0x0004465C
		[SRCategory("CatPropertyChanged")]
		[SRDescription("DataGridOnReadOnlyChangedDescr")]
		public event EventHandler ReadOnlyChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.EVENT_READONLYCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EVENT_READONLYCHANGED, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the column headers of a table are visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the column headers are visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x0004646F File Offset: 0x0004466F
		// (set) Token: 0x060014F4 RID: 5364 RVA: 0x0004647D File Offset: 0x0004467D
		[SRCategory("CatDisplay")]
		[DefaultValue(true)]
		[SRDescription("DataGridColumnHeadersVisibleDescr")]
		public bool ColumnHeadersVisible
		{
			get
			{
				return this.gridState[2];
			}
			set
			{
				if (this.ColumnHeadersVisible != value)
				{
					this.gridState[2] = value;
					this.layout.ColumnHeadersVisible = value;
					base.PerformLayout();
					this.InvalidateInside();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the parent rows of a table are visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the parent rows are visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x000464AD File Offset: 0x000446AD
		// (set) Token: 0x060014F6 RID: 5366 RVA: 0x000464BA File Offset: 0x000446BA
		[SRCategory("CatDisplay")]
		[DefaultValue(true)]
		[SRDescription("DataGridParentRowsVisibleDescr")]
		public bool ParentRowsVisible
		{
			get
			{
				return this.layout.ParentRowsVisible;
			}
			set
			{
				if (this.layout.ParentRowsVisible != value)
				{
					this.SetParentRowsVisibility(value);
					this.caption.SetDownButtonDirection(!value);
					this.OnParentRowsVisibleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGrid.ParentRowsVisible" /> property value changes.</summary>
		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x060014F7 RID: 5367 RVA: 0x000464EB File Offset: 0x000446EB
		// (remove) Token: 0x060014F8 RID: 5368 RVA: 0x000464FE File Offset: 0x000446FE
		[SRCategory("CatPropertyChanged")]
		[SRDescription("DataGridOnParentRowsVisibleChangedDescr")]
		public event EventHandler ParentRowsVisibleChanged
		{
			add
			{
				base.Events.AddHandler(DataGrid.EVENT_PARENTROWSVISIBLECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EVENT_PARENTROWSVISIBLECHANGED, value);
			}
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00046511 File Offset: 0x00044711
		internal bool ParentRowsIsEmpty()
		{
			return this.parentRows.IsEmpty();
		}

		/// <summary>Gets or sets a value that specifies whether row headers are visible.</summary>
		/// <returns>
		///   <see langword="true" /> if row headers are visible; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0004651E File Offset: 0x0004471E
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x0004652C File Offset: 0x0004472C
		[SRCategory("CatDisplay")]
		[DefaultValue(true)]
		[SRDescription("DataGridRowHeadersVisibleDescr")]
		public bool RowHeadersVisible
		{
			get
			{
				return this.gridState[4];
			}
			set
			{
				if (this.RowHeadersVisible != value)
				{
					this.gridState[4] = value;
					base.PerformLayout();
					this.InvalidateInside();
				}
			}
		}

		/// <summary>Gets or sets the width of row headers.</summary>
		/// <returns>The width of row headers in the <see cref="T:System.Windows.Forms.DataGrid" />. The default is 35.</returns>
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x00046550 File Offset: 0x00044750
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x00046558 File Offset: 0x00044758
		[SRCategory("CatLayout")]
		[DefaultValue(35)]
		[SRDescription("DataGridRowHeaderWidthDescr")]
		public int RowHeaderWidth
		{
			get
			{
				return this.rowHeaderWidth;
			}
			set
			{
				value = Math.Max(this.minRowHeaderWidth, value);
				if (this.rowHeaderWidth != value)
				{
					this.rowHeaderWidth = value;
					if (this.layout.RowHeadersVisible)
					{
						base.PerformLayout();
						this.InvalidateInside();
					}
				}
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x060014FF RID: 5375 RVA: 0x00023FE9 File Offset: 0x000221E9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DataGrid.Text" /> property changes.</summary>
		// Token: 0x140000DA RID: 218
		// (add) Token: 0x06001500 RID: 5376 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06001501 RID: 5377 RVA: 0x0004659A File Offset: 0x0004479A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>Gets the vertical scroll bar of the control.</summary>
		/// <returns>The vertical <see cref="T:System.Windows.Forms.ScrollBar" /> of the grid.</returns>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x000465A3 File Offset: 0x000447A3
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("DataGridVertScrollBarDescr")]
		protected ScrollBar VertScrollBar
		{
			get
			{
				return this.vertScrollBar;
			}
		}

		/// <summary>Gets the number of visible columns.</summary>
		/// <returns>The number of columns visible in the viewport. The viewport is the rectangular area through which the grid is visible. The size of the viewport depends on the size of the <see cref="T:System.Windows.Forms.DataGrid" /> control; if you allow users to resize the control, the viewport will also be affected.</returns>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x000465AB File Offset: 0x000447AB
		[Browsable(false)]
		[SRDescription("DataGridVisibleColumnCountDescr")]
		public int VisibleColumnCount
		{
			get
			{
				return Math.Min(this.numVisibleCols, (this.myGridTable == null) ? 0 : this.myGridTable.GridColumnStyles.Count);
			}
		}

		/// <summary>Gets the number of rows visible.</summary>
		/// <returns>The number of rows visible in the viewport. The viewport is the rectangular area through which the grid is visible. The size of the viewport depends on the size of the <see cref="T:System.Windows.Forms.DataGrid" /> control; if you allow users to resize the control, the viewport will also be affected.</returns>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x000465D3 File Offset: 0x000447D3
		[Browsable(false)]
		[SRDescription("DataGridVisibleRowCountDescr")]
		public int VisibleRowCount
		{
			get
			{
				return this.numVisibleRows;
			}
		}

		/// <summary>Gets or sets the value of the cell at the specified the row and column.</summary>
		/// <param name="rowIndex">The zero-based index of the row containing the value.</param>
		/// <param name="columnIndex">The zero-based index of the column containing the value.</param>
		/// <returns>The value, typed as <see cref="T:System.Object" />, of the cell.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">While getting or setting, the <paramref name="rowIndex" /> is out of range.  
		///  While getting or setting, the <paramref name="columnIndex" /> is out of range.</exception>
		// Token: 0x170004FB RID: 1275
		public object this[int rowIndex, int columnIndex]
		{
			get
			{
				this.EnsureBound();
				if (rowIndex < 0 || rowIndex >= this.DataGridRowsLength)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				if (columnIndex < 0 || columnIndex >= this.myGridTable.GridColumnStyles.Count)
				{
					throw new ArgumentOutOfRangeException("columnIndex");
				}
				CurrencyManager currencyManager = this.listManager;
				DataGridColumnStyle dataGridColumnStyle = this.myGridTable.GridColumnStyles[columnIndex];
				return dataGridColumnStyle.GetColumnValueAtRow(currencyManager, rowIndex);
			}
			set
			{
				this.EnsureBound();
				if (rowIndex < 0 || rowIndex >= this.DataGridRowsLength)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				if (columnIndex < 0 || columnIndex >= this.myGridTable.GridColumnStyles.Count)
				{
					throw new ArgumentOutOfRangeException("columnIndex");
				}
				CurrencyManager currencyManager = this.listManager;
				if (currencyManager.Position != rowIndex)
				{
					currencyManager.Position = rowIndex;
				}
				DataGridColumnStyle dataGridColumnStyle = this.myGridTable.GridColumnStyles[columnIndex];
				dataGridColumnStyle.SetColumnValueAtRow(currencyManager, rowIndex, value);
				if (columnIndex >= this.firstVisibleCol && columnIndex <= this.firstVisibleCol + this.numVisibleCols - 1 && rowIndex >= this.firstVisibleRow && rowIndex <= this.firstVisibleRow + this.numVisibleRows)
				{
					Rectangle cellBounds = this.GetCellBounds(rowIndex, columnIndex);
					base.Invalidate(cellBounds);
				}
			}
		}

		/// <summary>Gets or sets the value of a specified <see cref="T:System.Windows.Forms.DataGridCell" />.</summary>
		/// <param name="cell">A <see cref="T:System.Windows.Forms.DataGridCell" /> that represents a cell in the grid.</param>
		/// <returns>The value, typed as <see cref="T:System.Object" />, of the cell.</returns>
		// Token: 0x170004FC RID: 1276
		public object this[DataGridCell cell]
		{
			get
			{
				return this[cell.RowNumber, cell.ColumnNumber];
			}
			set
			{
				this[cell.RowNumber, cell.ColumnNumber] = value;
			}
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0004673C File Offset: 0x0004493C
		private void WireTableStylePropChanged(DataGridTableStyle gridTable)
		{
			gridTable.GridLineColorChanged += this.GridLineColorChanged;
			gridTable.GridLineStyleChanged += this.GridLineStyleChanged;
			gridTable.HeaderBackColorChanged += this.HeaderBackColorChanged;
			gridTable.HeaderFontChanged += this.HeaderFontChanged;
			gridTable.HeaderForeColorChanged += this.HeaderForeColorChanged;
			gridTable.LinkColorChanged += this.LinkColorChanged;
			gridTable.LinkHoverColorChanged += this.LinkHoverColorChanged;
			gridTable.PreferredColumnWidthChanged += this.PreferredColumnWidthChanged;
			gridTable.RowHeadersVisibleChanged += this.RowHeadersVisibleChanged;
			gridTable.ColumnHeadersVisibleChanged += this.ColumnHeadersVisibleChanged;
			gridTable.RowHeaderWidthChanged += this.RowHeaderWidthChanged;
			gridTable.AllowSortingChanged += this.AllowSortingChanged;
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x00046824 File Offset: 0x00044A24
		private void UnWireTableStylePropChanged(DataGridTableStyle gridTable)
		{
			gridTable.GridLineColorChanged -= this.GridLineColorChanged;
			gridTable.GridLineStyleChanged -= this.GridLineStyleChanged;
			gridTable.HeaderBackColorChanged -= this.HeaderBackColorChanged;
			gridTable.HeaderFontChanged -= this.HeaderFontChanged;
			gridTable.HeaderForeColorChanged -= this.HeaderForeColorChanged;
			gridTable.LinkColorChanged -= this.LinkColorChanged;
			gridTable.LinkHoverColorChanged -= this.LinkHoverColorChanged;
			gridTable.PreferredColumnWidthChanged -= this.PreferredColumnWidthChanged;
			gridTable.RowHeadersVisibleChanged -= this.RowHeadersVisibleChanged;
			gridTable.ColumnHeadersVisibleChanged -= this.ColumnHeadersVisibleChanged;
			gridTable.RowHeaderWidthChanged -= this.RowHeaderWidthChanged;
			gridTable.AllowSortingChanged -= this.AllowSortingChanged;
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0004690C File Offset: 0x00044B0C
		private void WireDataSource()
		{
			this.listManager.CurrentChanged += this.currentChangedHandler;
			this.listManager.PositionChanged += this.positionChangedHandler;
			this.listManager.ItemChanged += this.itemChangedHandler;
			this.listManager.MetaDataChanged += this.metaDataChangedHandler;
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00046960 File Offset: 0x00044B60
		private void UnWireDataSource()
		{
			this.listManager.CurrentChanged -= this.currentChangedHandler;
			this.listManager.PositionChanged -= this.positionChangedHandler;
			this.listManager.ItemChanged -= this.itemChangedHandler;
			this.listManager.MetaDataChanged -= this.metaDataChangedHandler;
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x000469B4 File Offset: 0x00044BB4
		private void DataSource_Changed(object sender, EventArgs ea)
		{
			this.policy.UpdatePolicy(this.ListManager, this.ReadOnly);
			if (this.gridState[512])
			{
				DataGridRow[] array = this.DataGridRows;
				int num = this.DataGridRowsLength;
				array[num - 1] = new DataGridRelationshipRow(this, this.myGridTable, num - 1);
				this.SetDataGridRows(array, num);
			}
			else if (this.gridState[1048576] && !this.gridState[1024])
			{
				this.listManager.CancelCurrentEdit();
				this.gridState[1048576] = false;
				this.RecreateDataGridRows();
			}
			else if (!this.gridState[1024])
			{
				this.RecreateDataGridRows();
				this.currentRow = Math.Min(this.currentRow, this.listManager.Count);
			}
			bool listHasErrors = this.ListHasErrors;
			this.ListHasErrors = this.DataGridSourceHasErrors();
			if (listHasErrors == this.ListHasErrors)
			{
				this.InvalidateInside();
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00046AB5 File Offset: 0x00044CB5
		private void GridLineColorChanged(object sender, EventArgs e)
		{
			base.Invalidate(this.layout.Data);
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00046AC8 File Offset: 0x00044CC8
		private void GridLineStyleChanged(object sender, EventArgs e)
		{
			this.myGridTable.ResetRelationsUI();
			base.Invalidate(this.layout.Data);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00046AE8 File Offset: 0x00044CE8
		private void HeaderBackColorChanged(object sender, EventArgs e)
		{
			if (this.layout.RowHeadersVisible)
			{
				base.Invalidate(this.layout.RowHeaders);
			}
			if (this.layout.ColumnHeadersVisible)
			{
				base.Invalidate(this.layout.ColumnHeaders);
			}
			base.Invalidate(this.layout.TopLeftHeader);
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x00046B42 File Offset: 0x00044D42
		private void HeaderFontChanged(object sender, EventArgs e)
		{
			this.RecalculateFonts();
			base.PerformLayout();
			base.Invalidate(this.layout.Inside);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00046B64 File Offset: 0x00044D64
		private void HeaderForeColorChanged(object sender, EventArgs e)
		{
			if (this.layout.RowHeadersVisible)
			{
				base.Invalidate(this.layout.RowHeaders);
			}
			if (this.layout.ColumnHeadersVisible)
			{
				base.Invalidate(this.layout.ColumnHeaders);
			}
			base.Invalidate(this.layout.TopLeftHeader);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00046AB5 File Offset: 0x00044CB5
		private void LinkColorChanged(object sender, EventArgs e)
		{
			base.Invalidate(this.layout.Data);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00046AB5 File Offset: 0x00044CB5
		private void LinkHoverColorChanged(object sender, EventArgs e)
		{
			base.Invalidate(this.layout.Data);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00046BBE File Offset: 0x00044DBE
		private void PreferredColumnWidthChanged(object sender, EventArgs e)
		{
			this.SetDataGridRows(null, this.DataGridRowsLength);
			base.PerformLayout();
			base.Invalidate();
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x00046BD9 File Offset: 0x00044DD9
		private void RowHeadersVisibleChanged(object sender, EventArgs e)
		{
			this.layout.RowHeadersVisible = this.myGridTable != null && this.myGridTable.RowHeadersVisible;
			base.PerformLayout();
			this.InvalidateInside();
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x00046C08 File Offset: 0x00044E08
		private void ColumnHeadersVisibleChanged(object sender, EventArgs e)
		{
			this.layout.ColumnHeadersVisible = this.myGridTable != null && this.myGridTable.ColumnHeadersVisible;
			base.PerformLayout();
			this.InvalidateInside();
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00046C37 File Offset: 0x00044E37
		private void RowHeaderWidthChanged(object sender, EventArgs e)
		{
			if (this.layout.RowHeadersVisible)
			{
				base.PerformLayout();
				this.InvalidateInside();
			}
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x00046C54 File Offset: 0x00044E54
		private void AllowSortingChanged(object sender, EventArgs e)
		{
			if (!this.myGridTable.AllowSorting && this.listManager != null)
			{
				IList list = this.listManager.List;
				if (list is IBindingList)
				{
					((IBindingList)list).RemoveSort();
				}
			}
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x00046C98 File Offset: 0x00044E98
		private void DataSource_RowChanged(object sender, EventArgs ea)
		{
			DataGridRow[] array = this.DataGridRows;
			if (this.currentRow < this.DataGridRowsLength)
			{
				this.InvalidateRow(this.currentRow);
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00046CC8 File Offset: 0x00044EC8
		private void DataSource_PositionChanged(object sender, EventArgs ea)
		{
			if (this.DataGridRowsLength > this.listManager.Count + (this.policy.AllowAdd ? 1 : 0) && !this.gridState[1024])
			{
				this.RecreateDataGridRows();
			}
			if (this.ListManager.Position != this.currentRow)
			{
				this.CurrentCell = new DataGridCell(this.listManager.Position, this.currentCol);
			}
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00046D41 File Offset: 0x00044F41
		internal void DataSource_MetaDataChanged(object sender, EventArgs e)
		{
			this.MetaDataChanged();
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00046D4C File Offset: 0x00044F4C
		private bool DataGridSourceHasErrors()
		{
			if (this.listManager == null)
			{
				return false;
			}
			for (int i = 0; i < this.listManager.Count; i++)
			{
				object obj = this.listManager[i];
				if (obj is IDataErrorInfo)
				{
					string error = ((IDataErrorInfo)obj).Error;
					if (error != null && error.Length != 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00046DA8 File Offset: 0x00044FA8
		private void TableStylesCollectionChanged(object sender, CollectionChangeEventArgs ccea)
		{
			if (sender != this.dataGridTables)
			{
				return;
			}
			if (this.listManager == null)
			{
				return;
			}
			if (ccea.Action == CollectionChangeAction.Add)
			{
				DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)ccea.Element;
				if (this.listManager.GetListName().Equals(dataGridTableStyle.MappingName))
				{
					this.SetDataGridTable(dataGridTableStyle, true);
					this.SetDataGridRows(null, 0);
					return;
				}
			}
			else if (ccea.Action == CollectionChangeAction.Remove)
			{
				DataGridTableStyle dataGridTableStyle2 = (DataGridTableStyle)ccea.Element;
				if (this.myGridTable.MappingName.Equals(dataGridTableStyle2.MappingName))
				{
					this.defaultTableStyle.GridColumnStyles.ResetDefaultColumnCollection();
					this.SetDataGridTable(this.defaultTableStyle, true);
					this.SetDataGridRows(null, 0);
					return;
				}
			}
			else
			{
				DataGridTableStyle dataGridTableStyle3 = this.dataGridTables[this.listManager.GetListName()];
				if (dataGridTableStyle3 == null)
				{
					if (!this.myGridTable.IsDefault)
					{
						this.defaultTableStyle.GridColumnStyles.ResetDefaultColumnCollection();
						this.SetDataGridTable(this.defaultTableStyle, true);
						this.SetDataGridRows(null, 0);
						return;
					}
				}
				else
				{
					this.SetDataGridTable(dataGridTableStyle3, true);
					this.SetDataGridRows(null, 0);
				}
			}
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00046EBC File Offset: 0x000450BC
		private void DataSource_ItemChanged(object sender, ItemChangedEventArgs ea)
		{
			if (ea.Index == -1)
			{
				this.DataSource_Changed(sender, EventArgs.Empty);
				return;
			}
			object obj = this.listManager[ea.Index];
			bool listHasErrors = this.ListHasErrors;
			if (obj is IDataErrorInfo)
			{
				if (((IDataErrorInfo)obj).Error.Length != 0)
				{
					this.ListHasErrors = true;
				}
				else if (this.ListHasErrors)
				{
					this.ListHasErrors = this.DataGridSourceHasErrors();
				}
			}
			if (listHasErrors == this.ListHasErrors)
			{
				this.InvalidateRow(ea.Index);
			}
			if (this.editColumn != null && ea.Index == this.currentRow)
			{
				this.editColumn.UpdateUI(this.ListManager, ea.Index, null);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.BorderStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001520 RID: 5408 RVA: 0x00046F74 File Offset: 0x00045174
		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGrid.EVENT_BORDERSTYLECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.CaptionVisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001521 RID: 5409 RVA: 0x00046FA4 File Offset: 0x000451A4
		protected virtual void OnCaptionVisibleChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGrid.EVENT_CAPTIONVISIBLECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.CurrentCellChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001522 RID: 5410 RVA: 0x00046FD4 File Offset: 0x000451D4
		protected virtual void OnCurrentCellChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGrid.EVENT_CURRENTCELLCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.FlatModeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001523 RID: 5411 RVA: 0x00047004 File Offset: 0x00045204
		protected virtual void OnFlatModeChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGrid.EVENT_FLATMODECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.BackgroundColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001524 RID: 5412 RVA: 0x00047034 File Offset: 0x00045234
		protected virtual void OnBackgroundColorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGrid.EVENT_BACKGROUNDCOLORCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.AllowNavigationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001525 RID: 5413 RVA: 0x00047064 File Offset: 0x00045264
		protected virtual void OnAllowNavigationChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGrid.EVENT_ALLOWNAVIGATIONCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.ParentRowsVisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001526 RID: 5414 RVA: 0x00047094 File Offset: 0x00045294
		protected virtual void OnParentRowsVisibleChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGrid.EVENT_PARENTROWSVISIBLECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.ParentRowsLabelStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001527 RID: 5415 RVA: 0x000470C4 File Offset: 0x000452C4
		protected virtual void OnParentRowsLabelStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGrid.EVENT_PARENTROWSLABELSTYLECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.ReadOnlyChanged" /> event</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001528 RID: 5416 RVA: 0x000470F4 File Offset: 0x000452F4
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGrid.EVENT_READONLYCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.Navigate" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.NavigateEventArgs" /> that contains the event data.</param>
		// Token: 0x06001529 RID: 5417 RVA: 0x00047122 File Offset: 0x00045322
		protected void OnNavigate(NavigateEventArgs e)
		{
			if (this.onNavigate != null)
			{
				this.onNavigate(this, e);
			}
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0004713C File Offset: 0x0004533C
		internal void OnNodeClick(EventArgs e)
		{
			base.PerformLayout();
			GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
			if (this.firstVisibleCol > -1 && this.firstVisibleCol < gridColumnStyles.Count && gridColumnStyles[this.firstVisibleCol] == this.editColumn)
			{
				this.Edit();
			}
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.EVENT_NODECLICKED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.RowHeaderClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600152B RID: 5419 RVA: 0x000471AD File Offset: 0x000453AD
		protected void OnRowHeaderClick(EventArgs e)
		{
			if (this.onRowHeaderClick != null)
			{
				this.onRowHeaderClick(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.Scroll" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600152C RID: 5420 RVA: 0x000471C4 File Offset: 0x000453C4
		protected void OnScroll(EventArgs e)
		{
			if (this.ToolTipProvider != null)
			{
				this.ResetToolTip();
			}
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.EVENT_SCROLL];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Listens for the scroll event of the horizontal scroll bar.</summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that contains data about the control.</param>
		/// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data.</param>
		// Token: 0x0600152D RID: 5421 RVA: 0x00047200 File Offset: 0x00045400
		protected virtual void GridHScrolled(object sender, ScrollEventArgs se)
		{
			if (!base.Enabled)
			{
				return;
			}
			if (this.DataSource == null)
			{
				return;
			}
			this.gridState[131072] = true;
			if (se.Type == ScrollEventType.SmallIncrement || se.Type == ScrollEventType.SmallDecrement)
			{
				int num = ((se.Type == ScrollEventType.SmallIncrement) ? 1 : (-1));
				if (se.Type == ScrollEventType.SmallDecrement && this.negOffset == 0)
				{
					GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
					int num2 = this.firstVisibleCol - 1;
					while (num2 >= 0 && gridColumnStyles[num2].Width == 0)
					{
						num--;
						num2--;
					}
				}
				if (se.Type == ScrollEventType.SmallIncrement && this.negOffset == 0)
				{
					GridColumnStylesCollection gridColumnStyles2 = this.myGridTable.GridColumnStyles;
					int num3 = this.firstVisibleCol;
					while (num3 > -1 && num3 < gridColumnStyles2.Count && gridColumnStyles2[num3].Width == 0)
					{
						num++;
						num3++;
					}
				}
				this.ScrollRight(num);
				se.NewValue = this.HorizontalOffset;
			}
			else if (se.Type != ScrollEventType.EndScroll)
			{
				this.HorizontalOffset = se.NewValue;
			}
			this.gridState[131072] = false;
		}

		/// <summary>Listens for the scroll event of the vertical scroll bar.</summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that contains data about the control.</param>
		/// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data.</param>
		// Token: 0x0600152E RID: 5422 RVA: 0x00047320 File Offset: 0x00045520
		protected virtual void GridVScrolled(object sender, ScrollEventArgs se)
		{
			if (!base.Enabled)
			{
				return;
			}
			if (this.DataSource == null)
			{
				return;
			}
			this.gridState[131072] = true;
			try
			{
				se.NewValue = Math.Min(se.NewValue, this.DataGridRowsLength - this.numTotallyVisibleRows);
				int num = se.NewValue - this.firstVisibleRow;
				this.ScrollDown(num);
			}
			finally
			{
				this.gridState[131072] = false;
			}
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x000473A8 File Offset: 0x000455A8
		private void HandleEndCurrentEdit()
		{
			int num = this.currentRow;
			int num2 = this.currentCol;
			string text = null;
			try
			{
				this.listManager.EndCurrentEdit();
			}
			catch (Exception ex)
			{
				text = ex.Message;
			}
			if (text != null)
			{
				DialogResult dialogResult = RTLAwareMessageBox.Show(null, SR.GetString("DataGridPushedIncorrectValueIntoColumn", new object[] { text }), SR.GetString("DataGridErrorMessageBoxCaption"), MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
				if (dialogResult == DialogResult.Yes)
				{
					this.currentRow = num;
					this.currentCol = num2;
					this.InvalidateRowHeader(this.currentRow);
					this.Edit();
					return;
				}
				this.listManager.PositionChanged -= this.positionChangedHandler;
				this.listManager.CancelCurrentEdit();
				this.listManager.Position = this.currentRow;
				this.listManager.PositionChanged += this.positionChangedHandler;
			}
		}

		/// <summary>Listens for the caption's back button clicked event.</summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that contains data about the control.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains data about the event.</param>
		// Token: 0x06001530 RID: 5424 RVA: 0x00047484 File Offset: 0x00045684
		protected void OnBackButtonClicked(object sender, EventArgs e)
		{
			this.NavigateBack();
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.EVENT_BACKBUTTONCLICK];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001531 RID: 5425 RVA: 0x000474B8 File Offset: 0x000456B8
		protected override void OnBackColorChanged(EventArgs e)
		{
			this.backBrush = new SolidBrush(this.BackColor);
			base.Invalidate();
			base.OnBackColorChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BindingContextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001532 RID: 5426 RVA: 0x000474D8 File Offset: 0x000456D8
		protected override void OnBindingContextChanged(EventArgs e)
		{
			if (this.DataSource != null && !this.gridState[2097152])
			{
				try
				{
					this.Set_ListManager(this.DataSource, this.DataMember, true, false);
				}
				catch
				{
					if (this.Site == null || !this.Site.DesignMode)
					{
						throw;
					}
					RTLAwareMessageBox.Show(null, SR.GetString("DataGridExceptionInPaint"), null, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
					if (base.Visible)
					{
						base.BeginUpdateInternal();
					}
					this.ResetParentRows();
					this.Set_ListManager(null, string.Empty, true);
					if (base.Visible)
					{
						base.EndUpdateInternal();
					}
				}
			}
			base.OnBindingContextChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.DataSourceChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001533 RID: 5427 RVA: 0x00047590 File Offset: 0x00045790
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGrid.EVENT_DATASOURCECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGrid.ShowParentDetailsButtonClick" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001534 RID: 5428 RVA: 0x000475C0 File Offset: 0x000457C0
		protected void OnShowParentDetailsButtonClicked(object sender, EventArgs e)
		{
			this.ParentRowsVisible = !this.caption.ToggleDownButtonDirection();
			EventHandler eventHandler = (EventHandler)base.Events[DataGrid.EVENT_DOWNBUTTONCLICK];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001535 RID: 5429 RVA: 0x00047602 File Offset: 0x00045802
		protected override void OnForeColorChanged(EventArgs e)
		{
			this.foreBrush = new SolidBrush(this.ForeColor);
			base.Invalidate();
			base.OnForeColorChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001536 RID: 5430 RVA: 0x00047624 File Offset: 0x00045824
		protected override void OnFontChanged(EventArgs e)
		{
			this.Caption.OnGridFontChanged();
			this.RecalculateFonts();
			this.RecreateDataGridRows();
			if (this.originalState != null)
			{
				Stack stack = new Stack();
				while (!this.parentRows.IsEmpty())
				{
					DataGridState dataGridState = this.parentRows.PopTop();
					int num = dataGridState.DataGridRowsLength;
					for (int i = 0; i < num; i++)
					{
						dataGridState.DataGridRows[i].Height = dataGridState.DataGridRows[i].MinimumRowHeight(dataGridState.GridColumnStyles);
					}
					stack.Push(dataGridState);
				}
				while (stack.Count != 0)
				{
					this.parentRows.AddParent((DataGridState)stack.Pop());
				}
			}
			base.OnFontChanged(e);
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.OnPaintBackground(System.Windows.Forms.PaintEventArgs)" /> to prevent painting the background of the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <param name="ebe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint.</param>
		// Token: 0x06001537 RID: 5431 RVA: 0x000070A6 File Offset: 0x000052A6
		protected override void OnPaintBackground(PaintEventArgs ebe)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event, which repositions controls and updates scroll bars.</summary>
		/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06001538 RID: 5432 RVA: 0x000476D4 File Offset: 0x000458D4
		protected override void OnLayout(LayoutEventArgs levent)
		{
			if (this.gridState[65536])
			{
				return;
			}
			base.OnLayout(levent);
			if (this.gridState[16777216])
			{
				return;
			}
			this.gridState[2048] = false;
			try
			{
				if (base.IsHandleCreated)
				{
					if (this.layout.ParentRowsVisible)
					{
						this.parentRows.OnLayout();
					}
					if (this.ToolTipProvider != null)
					{
						this.ResetToolTip();
					}
					this.ComputeLayout();
				}
			}
			finally
			{
				this.gridState[2048] = true;
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.CreateHandle" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001539 RID: 5433 RVA: 0x00047778 File Offset: 0x00045978
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.toolTipProvider = new DataGridToolTip(this);
			this.toolTipProvider.CreateToolTipHandle();
			this.toolTipId = 0;
			base.PerformLayout();
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.DestroyHandle" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> containing the event data.</param>
		// Token: 0x0600153A RID: 5434 RVA: 0x000477A5 File Offset: 0x000459A5
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			if (this.toolTipProvider != null)
			{
				this.toolTipProvider.Destroy();
				this.toolTipProvider = null;
			}
			this.toolTipId = 0;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600153B RID: 5435 RVA: 0x000477CF File Offset: 0x000459CF
		protected override void OnEnter(EventArgs e)
		{
			if (this.gridState[2048] && !this.gridState[65536])
			{
				if (this.Bound)
				{
					this.Edit();
				}
				base.OnEnter(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600153C RID: 5436 RVA: 0x0004780A File Offset: 0x00045A0A
		protected override void OnLeave(EventArgs e)
		{
			this.OnLeave_Grid();
			base.OnLeave(e);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0004781C File Offset: 0x00045A1C
		private void OnLeave_Grid()
		{
			this.gridState[2048] = false;
			try
			{
				this.EndEdit();
				if (this.listManager != null && !this.gridState[65536])
				{
					if (this.gridState[1048576])
					{
						this.listManager.CancelCurrentEdit();
						DataGridRow[] array = this.DataGridRows;
						array[this.DataGridRowsLength - 1] = new DataGridAddNewRow(this, this.myGridTable, this.DataGridRowsLength - 1);
						this.SetDataGridRows(array, this.DataGridRowsLength);
					}
					else
					{
						this.HandleEndCurrentEdit();
					}
				}
			}
			finally
			{
				this.gridState[2048] = true;
				if (!this.gridState[65536])
				{
					this.gridState[1048576] = false;
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="ke">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that provides data about the <see cref="M:System.Windows.Forms.Control.OnKeyDown(System.Windows.Forms.KeyEventArgs)" /> event.</param>
		// Token: 0x0600153E RID: 5438 RVA: 0x000478F8 File Offset: 0x00045AF8
		protected override void OnKeyDown(KeyEventArgs ke)
		{
			base.OnKeyDown(ke);
			this.ProcessGridKey(ke);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="kpe">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains data about the <see cref="M:System.Windows.Forms.Control.OnKeyPress(System.Windows.Forms.KeyPressEventArgs)" /> event</param>
		// Token: 0x0600153F RID: 5439 RVA: 0x0004790C File Offset: 0x00045B0C
		protected override void OnKeyPress(KeyPressEventArgs kpe)
		{
			base.OnKeyPress(kpe);
			GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
			if (gridColumnStyles != null && this.currentCol > 0 && this.currentCol < gridColumnStyles.Count && !gridColumnStyles[this.currentCol].ReadOnly && kpe.KeyChar > ' ')
			{
				this.Edit(new string(new char[] { kpe.KeyChar }));
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains data about the <see cref="M:System.Windows.Forms.Control.OnMouseDown(System.Windows.Forms.MouseEventArgs)" /> event.</param>
		// Token: 0x06001540 RID: 5440 RVA: 0x00047980 File Offset: 0x00045B80
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.gridState[524288] = false;
			this.gridState[256] = false;
			if (this.listManager == null)
			{
				return;
			}
			DataGrid.HitTestInfo hitTestInfo = this.HitTest(e.X, e.Y);
			Keys modifierKeys = Control.ModifierKeys;
			bool flag = (modifierKeys & Keys.Control) == Keys.Control && (modifierKeys & Keys.Alt) == Keys.None;
			bool flag2 = (modifierKeys & Keys.Shift) == Keys.Shift;
			if (e.Button != MouseButtons.Left)
			{
				return;
			}
			if (hitTestInfo.type == DataGrid.HitTestType.ColumnResize)
			{
				if (e.Clicks > 1)
				{
					this.ColAutoResize(hitTestInfo.col);
					return;
				}
				this.ColResizeBegin(e, hitTestInfo.col);
				return;
			}
			else if (hitTestInfo.type == DataGrid.HitTestType.RowResize)
			{
				if (e.Clicks > 1)
				{
					this.RowAutoResize(hitTestInfo.row);
					return;
				}
				this.RowResizeBegin(e, hitTestInfo.row);
				return;
			}
			else
			{
				if (hitTestInfo.type == DataGrid.HitTestType.ColumnHeader)
				{
					this.trackColumnHeader = this.myGridTable.GridColumnStyles[hitTestInfo.col].PropertyDescriptor;
					return;
				}
				if (hitTestInfo.type == DataGrid.HitTestType.Caption)
				{
					Rectangle rectangle = this.layout.Caption;
					this.caption.MouseDown(e.X - rectangle.X, e.Y - rectangle.Y);
					return;
				}
				if (this.layout.Data.Contains(e.X, e.Y) || this.layout.RowHeaders.Contains(e.X, e.Y))
				{
					int rowFromY = this.GetRowFromY(e.Y);
					if (rowFromY > -1)
					{
						Point point = this.NormalizeToRow(e.X, e.Y, rowFromY);
						DataGridRow[] array = this.DataGridRows;
						if (array[rowFromY].OnMouseDown(point.X, point.Y, this.layout.RowHeaders, this.isRightToLeft()))
						{
							this.CommitEdit();
							array = this.DataGridRows;
							if (rowFromY < this.DataGridRowsLength && array[rowFromY] is DataGridRelationshipRow && ((DataGridRelationshipRow)array[rowFromY]).Expanded)
							{
								this.EnsureVisible(rowFromY, 0);
							}
							this.Edit();
							return;
						}
					}
				}
				if (hitTestInfo.type == DataGrid.HitTestType.RowHeader)
				{
					this.EndEdit();
					if (!(this.DataGridRows[hitTestInfo.row] is DataGridAddNewRow))
					{
						int num = this.currentRow;
						this.CurrentCell = new DataGridCell(hitTestInfo.row, this.currentCol);
						if (hitTestInfo.row != num && this.currentRow != hitTestInfo.row && this.currentRow == num)
						{
							return;
						}
					}
					if (flag)
					{
						if (this.IsSelected(hitTestInfo.row))
						{
							this.UnSelect(hitTestInfo.row);
						}
						else
						{
							this.Select(hitTestInfo.row);
						}
					}
					else
					{
						if (this.lastRowSelected != -1 && flag2)
						{
							int num2 = Math.Min(this.lastRowSelected, hitTestInfo.row);
							int num3 = Math.Max(this.lastRowSelected, hitTestInfo.row);
							int num4 = this.lastRowSelected;
							this.ResetSelection();
							this.lastRowSelected = num4;
							DataGridRow[] array2 = this.DataGridRows;
							for (int i = num2; i <= num3; i++)
							{
								array2[i].Selected = true;
								this.numSelectedRows++;
							}
							this.EndEdit();
							return;
						}
						this.ResetSelection();
						this.Select(hitTestInfo.row);
					}
					this.lastRowSelected = hitTestInfo.row;
					return;
				}
				if (hitTestInfo.type == DataGrid.HitTestType.ParentRows)
				{
					this.EndEdit();
					this.parentRows.OnMouseDown(e.X, e.Y, this.isRightToLeft());
				}
				if (hitTestInfo.type == DataGrid.HitTestType.Cell)
				{
					if (this.myGridTable.GridColumnStyles[hitTestInfo.col].MouseDown(hitTestInfo.row, e.X, e.Y))
					{
						return;
					}
					DataGridCell dataGridCell = new DataGridCell(hitTestInfo.row, hitTestInfo.col);
					if (this.policy.AllowEdit && this.CurrentCell.Equals(dataGridCell))
					{
						this.ResetSelection();
						this.EnsureVisible(this.currentRow, this.currentCol);
						this.Edit();
						return;
					}
					this.ResetSelection();
					this.CurrentCell = dataGridCell;
				}
				return;
			}
		}

		/// <summary>Creates the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains data about the <see cref="M:System.Windows.Forms.Control.OnMouseLeave(System.EventArgs)" /> event.</param>
		// Token: 0x06001541 RID: 5441 RVA: 0x00047DCC File Offset: 0x00045FCC
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (this.oldRow != -1)
			{
				DataGridRow[] array = this.DataGridRows;
				array[this.oldRow].OnMouseLeft(this.layout.RowHeaders, this.isRightToLeft());
			}
			if (this.gridState[262144])
			{
				this.caption.MouseLeft();
			}
			this.Cursor = null;
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00047E32 File Offset: 0x00046032
		internal void TextBoxOnMouseWheel(MouseEventArgs e)
		{
			this.OnMouseWheel(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains data about the <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" /> event.</param>
		// Token: 0x06001543 RID: 5443 RVA: 0x00047E3C File Offset: 0x0004603C
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this.listManager == null)
			{
				return;
			}
			DataGrid.HitTestInfo hitTestInfo = this.HitTest(e.X, e.Y);
			bool flag = this.isRightToLeft();
			if (this.gridState[8])
			{
				this.ColResizeMove(e);
			}
			if (this.gridState[16])
			{
				this.RowResizeMove(e);
			}
			if (this.gridState[8] || hitTestInfo.type == DataGrid.HitTestType.ColumnResize)
			{
				this.Cursor = Cursors.SizeWE;
				return;
			}
			if (this.gridState[16] || hitTestInfo.type == DataGrid.HitTestType.RowResize)
			{
				this.Cursor = Cursors.SizeNS;
				return;
			}
			this.Cursor = null;
			if (this.layout.Data.Contains(e.X, e.Y) || (this.layout.RowHeadersVisible && this.layout.RowHeaders.Contains(e.X, e.Y)))
			{
				DataGridRow[] array = this.DataGridRows;
				int rowFromY = this.GetRowFromY(e.Y);
				if (this.lastRowSelected != -1 && !this.gridState[256])
				{
					int rowTop = this.GetRowTop(this.lastRowSelected);
					int num = rowTop + array[this.lastRowSelected].Height;
					int height = SystemInformation.DragSize.Height;
					this.gridState[256] = (e.Y - rowTop < height && rowTop - e.Y < height) || (e.Y - num < height && num - e.Y < height);
				}
				if (rowFromY > -1)
				{
					Point point = this.NormalizeToRow(e.X, e.Y, rowFromY);
					if (!array[rowFromY].OnMouseMove(point.X, point.Y, this.layout.RowHeaders, flag) && this.gridState[256])
					{
						MouseButtons mouseButtons = Control.MouseButtons;
						if (this.lastRowSelected != -1 && (mouseButtons & MouseButtons.Left) == MouseButtons.Left && ((Control.ModifierKeys & Keys.Control) != Keys.Control || (Control.ModifierKeys & Keys.Alt) != Keys.None))
						{
							int num2 = this.lastRowSelected;
							this.ResetSelection();
							this.lastRowSelected = num2;
							int num3 = Math.Min(this.lastRowSelected, rowFromY);
							int num4 = Math.Max(this.lastRowSelected, rowFromY);
							DataGridRow[] array2 = this.DataGridRows;
							for (int i = num3; i <= num4; i++)
							{
								array2[i].Selected = true;
								this.numSelectedRows++;
							}
						}
					}
				}
				if (this.oldRow != rowFromY && this.oldRow != -1)
				{
					array[this.oldRow].OnMouseLeft(this.layout.RowHeaders, flag);
				}
				this.oldRow = rowFromY;
			}
			if (hitTestInfo.type == DataGrid.HitTestType.ParentRows && this.parentRows != null)
			{
				this.parentRows.OnMouseMove(e.X, e.Y);
			}
			if (hitTestInfo.type == DataGrid.HitTestType.Caption)
			{
				this.gridState[262144] = true;
				Rectangle rectangle = this.layout.Caption;
				this.caption.MouseOver(e.X - rectangle.X, e.Y - rectangle.Y);
				return;
			}
			if (this.gridState[262144])
			{
				this.gridState[262144] = false;
				this.caption.MouseLeft();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains data about the <see cref="M:System.Windows.Forms.Control.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event.</param>
		// Token: 0x06001544 RID: 5444 RVA: 0x000481C4 File Offset: 0x000463C4
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.gridState[256] = false;
			if (this.listManager == null || this.myGridTable == null)
			{
				return;
			}
			if (this.gridState[8])
			{
				this.ColResizeEnd(e);
			}
			if (this.gridState[16])
			{
				this.RowResizeEnd(e);
			}
			this.gridState[8] = false;
			this.gridState[16] = false;
			DataGrid.HitTestInfo hitTestInfo = this.HitTest(e.X, e.Y);
			if ((hitTestInfo.type & DataGrid.HitTestType.Caption) == DataGrid.HitTestType.Caption)
			{
				this.caption.MouseUp(e.X, e.Y);
			}
			if (hitTestInfo.type == DataGrid.HitTestType.ColumnHeader)
			{
				PropertyDescriptor propertyDescriptor = this.myGridTable.GridColumnStyles[hitTestInfo.col].PropertyDescriptor;
				if (propertyDescriptor == this.trackColumnHeader)
				{
					this.ColumnHeaderClicked(this.trackColumnHeader);
				}
			}
			this.trackColumnHeader = null;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains data about the <see cref="M:System.Windows.Forms.Control.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event.</param>
		// Token: 0x06001545 RID: 5445 RVA: 0x000482B8 File Offset: 0x000464B8
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			if (e is HandledMouseEventArgs)
			{
				if (((HandledMouseEventArgs)e).Handled)
				{
					return;
				}
				((HandledMouseEventArgs)e).Handled = true;
			}
			bool flag = true;
			if ((Control.ModifierKeys & Keys.Control) != Keys.None)
			{
				flag = false;
			}
			if (this.listManager == null || this.myGridTable == null)
			{
				return;
			}
			ScrollBar scrollBar = (flag ? this.vertScrollBar : this.horizScrollBar);
			if (!scrollBar.Visible)
			{
				return;
			}
			this.gridState[131072] = true;
			this.wheelDelta += e.Delta;
			float num = (float)this.wheelDelta / 120f;
			int num2 = (int)((float)SystemInformation.MouseWheelScrollLines * num);
			if (num2 != 0)
			{
				this.wheelDelta = 0;
				if (flag)
				{
					int num3 = this.firstVisibleRow - num2;
					num3 = Math.Max(0, Math.Min(num3, this.DataGridRowsLength - this.numTotallyVisibleRows));
					this.ScrollDown(num3 - this.firstVisibleRow);
				}
				else
				{
					int num4 = this.horizScrollBar.Value + ((num2 < 0) ? 1 : (-1)) * this.horizScrollBar.LargeChange;
					this.HorizontalOffset = num4;
				}
			}
			this.gridState[131072] = false;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="pe">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> which contains data about the event.</param>
		// Token: 0x06001546 RID: 5446 RVA: 0x000483E8 File Offset: 0x000465E8
		protected override void OnPaint(PaintEventArgs pe)
		{
			try
			{
				this.CheckHierarchyState();
				if (this.layout.dirty)
				{
					this.ComputeLayout();
				}
				Graphics graphics = pe.Graphics;
				Region clip = graphics.Clip;
				if (this.layout.CaptionVisible)
				{
					this.caption.Paint(graphics, this.layout.Caption, this.isRightToLeft());
				}
				if (this.layout.ParentRowsVisible)
				{
					graphics.FillRectangle(SystemBrushes.AppWorkspace, this.layout.ParentRows);
					this.parentRows.Paint(graphics, this.layout.ParentRows, this.isRightToLeft());
				}
				Rectangle rectangle = this.layout.Data;
				if (this.layout.RowHeadersVisible)
				{
					rectangle = Rectangle.Union(rectangle, this.layout.RowHeaders);
				}
				if (this.layout.ColumnHeadersVisible)
				{
					rectangle = Rectangle.Union(rectangle, this.layout.ColumnHeaders);
				}
				graphics.SetClip(rectangle);
				this.PaintGrid(graphics, rectangle);
				graphics.Clip = clip;
				clip.Dispose();
				this.PaintBorder(graphics, this.layout.ClientRectangle);
				graphics.FillRectangle(DataGrid.DefaultHeaderBackBrush, this.layout.ResizeBoxRect);
				base.OnPaint(pe);
			}
			catch
			{
				if (this.Site == null || !this.Site.DesignMode)
				{
					throw;
				}
				this.gridState[8388608] = true;
				try
				{
					RTLAwareMessageBox.Show(null, SR.GetString("DataGridExceptionInPaint"), null, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
					if (base.Visible)
					{
						base.BeginUpdateInternal();
					}
					this.ResetParentRows();
					this.Set_ListManager(null, string.Empty, true);
				}
				finally
				{
					this.gridState[8388608] = false;
					if (base.Visible)
					{
						base.EndUpdateInternal();
					}
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001547 RID: 5447 RVA: 0x000485DC File Offset: 0x000467DC
		protected override void OnResize(EventArgs e)
		{
			if (this.layout.CaptionVisible)
			{
				base.Invalidate(this.layout.Caption);
			}
			if (this.layout.ParentRowsVisible)
			{
				this.parentRows.OnResize(this.layout.ParentRows);
			}
			int borderWidth = this.BorderWidth;
			Rectangle clientRectangle = this.layout.ClientRectangle;
			Rectangle rectangle = new Rectangle(clientRectangle.X + clientRectangle.Width - borderWidth, clientRectangle.Y, borderWidth, clientRectangle.Height);
			Rectangle rectangle2 = new Rectangle(clientRectangle.X, clientRectangle.Y + clientRectangle.Height - borderWidth, clientRectangle.Width, borderWidth);
			Rectangle clientRectangle2 = base.ClientRectangle;
			if (clientRectangle2.Width != clientRectangle.Width)
			{
				base.Invalidate(rectangle);
				rectangle = new Rectangle(clientRectangle2.X + clientRectangle2.Width - borderWidth, clientRectangle2.Y, borderWidth, clientRectangle2.Height);
				base.Invalidate(rectangle);
			}
			if (clientRectangle2.Height != clientRectangle.Height)
			{
				base.Invalidate(rectangle2);
				rectangle2 = new Rectangle(clientRectangle2.X, clientRectangle2.Y + clientRectangle2.Height - borderWidth, clientRectangle2.Width, borderWidth);
				base.Invalidate(rectangle2);
			}
			if (!this.layout.ResizeBoxRect.IsEmpty)
			{
				base.Invalidate(this.layout.ResizeBoxRect);
			}
			this.layout.ClientRectangle = clientRectangle2;
			int num = this.firstVisibleRow;
			base.OnResize(e);
			if (this.isRightToLeft() || num != this.firstVisibleRow)
			{
				base.Invalidate();
			}
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00048774 File Offset: 0x00046974
		internal void OnRowHeightChanged(DataGridRow row)
		{
			this.ClearRegionCache();
			int rowTop = this.GetRowTop(row.RowNumber);
			if (rowTop > 0)
			{
				base.Invalidate(new Rectangle
				{
					Y = rowTop,
					X = this.layout.Inside.X,
					Width = this.layout.Inside.Width,
					Height = this.layout.Inside.Bottom - rowTop
				});
			}
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x000487F8 File Offset: 0x000469F8
		internal void ParentRowsDataChanged()
		{
			this.parentRows.Clear();
			this.caption.BackButtonActive = (this.caption.DownButtonActive = (this.caption.BackButtonVisible = false));
			this.caption.SetDownButtonDirection(!this.layout.ParentRowsVisible);
			object obj = this.originalState.DataSource;
			string text = this.originalState.DataMember;
			this.originalState = null;
			this.Set_ListManager(obj, text, true);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0004887C File Offset: 0x00046A7C
		private void AbortEdit()
		{
			this.gridState[65536] = true;
			this.editColumn.Abort(this.editRow.RowNumber);
			this.gridState[65536] = false;
			this.gridState[32768] = false;
			this.editRow = null;
			this.editColumn = null;
		}

		/// <summary>Occurs when the user navigates to a new table.</summary>
		// Token: 0x140000DB RID: 219
		// (add) Token: 0x0600154B RID: 5451 RVA: 0x000488E0 File Offset: 0x00046AE0
		// (remove) Token: 0x0600154C RID: 5452 RVA: 0x000488F9 File Offset: 0x00046AF9
		[SRCategory("CatAction")]
		[SRDescription("DataGridNavigateEventDescr")]
		public event NavigateEventHandler Navigate
		{
			add
			{
				this.onNavigate = (NavigateEventHandler)Delegate.Combine(this.onNavigate, value);
			}
			remove
			{
				this.onNavigate = (NavigateEventHandler)Delegate.Remove(this.onNavigate, value);
			}
		}

		/// <summary>Occurs when a row header is clicked.</summary>
		// Token: 0x140000DC RID: 220
		// (add) Token: 0x0600154D RID: 5453 RVA: 0x00048912 File Offset: 0x00046B12
		// (remove) Token: 0x0600154E RID: 5454 RVA: 0x0004892B File Offset: 0x00046B2B
		protected event EventHandler RowHeaderClick
		{
			add
			{
				this.onRowHeaderClick = (EventHandler)Delegate.Combine(this.onRowHeaderClick, value);
			}
			remove
			{
				this.onRowHeaderClick = (EventHandler)Delegate.Remove(this.onRowHeaderClick, value);
			}
		}

		// Token: 0x140000DD RID: 221
		// (add) Token: 0x0600154F RID: 5455 RVA: 0x00048944 File Offset: 0x00046B44
		// (remove) Token: 0x06001550 RID: 5456 RVA: 0x00048957 File Offset: 0x00046B57
		[SRCategory("CatAction")]
		[SRDescription("DataGridNodeClickEventDescr")]
		internal event EventHandler NodeClick
		{
			add
			{
				base.Events.AddHandler(DataGrid.EVENT_NODECLICKED, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EVENT_NODECLICKED, value);
			}
		}

		/// <summary>Occurs when the user scrolls the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		// Token: 0x140000DE RID: 222
		// (add) Token: 0x06001551 RID: 5457 RVA: 0x0004896A File Offset: 0x00046B6A
		// (remove) Token: 0x06001552 RID: 5458 RVA: 0x0004897D File Offset: 0x00046B7D
		[SRCategory("CatAction")]
		[SRDescription("DataGridScrollEventDescr")]
		public event EventHandler Scroll
		{
			add
			{
				base.Events.AddHandler(DataGrid.EVENT_SCROLL, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EVENT_SCROLL, value);
			}
		}

		/// <summary>Gets or sets the site of the control.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the Control, if any.</returns>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x00048990 File Offset: 0x00046B90
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x00048998 File Offset: 0x00046B98
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				ISite site = this.Site;
				base.Site = value;
				if (value != site && !base.Disposing)
				{
					this.SubObjectsSiteChange(false);
					this.SubObjectsSiteChange(true);
				}
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x000489D0 File Offset: 0x00046BD0
		internal void AddNewRow()
		{
			this.EnsureBound();
			this.ResetSelection();
			this.UpdateListManager();
			this.gridState[512] = true;
			this.gridState[1048576] = true;
			try
			{
				this.ListManager.AddNew();
			}
			catch
			{
				this.gridState[512] = false;
				this.gridState[1048576] = false;
				base.PerformLayout();
				this.InvalidateInside();
				throw;
			}
			this.gridState[512] = false;
		}

		/// <summary>Attempts to put the grid into a state where editing is allowed.</summary>
		/// <param name="gridColumn">A <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to edit.</param>
		/// <param name="rowNumber">The number of the row to edit.</param>
		/// <returns>
		///   <see langword="true" /> if the method is successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001556 RID: 5462 RVA: 0x00048A70 File Offset: 0x00046C70
		public bool BeginEdit(DataGridColumnStyle gridColumn, int rowNumber)
		{
			if (this.DataSource == null || this.myGridTable == null)
			{
				return false;
			}
			if (this.gridState[32768])
			{
				return false;
			}
			int num;
			if ((num = this.myGridTable.GridColumnStyles.IndexOf(gridColumn)) < 0)
			{
				return false;
			}
			this.CurrentCell = new DataGridCell(rowNumber, num);
			this.ResetSelection();
			this.Edit();
			return true;
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Windows.Forms.DataGrid" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		// Token: 0x06001557 RID: 5463 RVA: 0x00048AD7 File Offset: 0x00046CD7
		public void BeginInit()
		{
			if (this.inInit)
			{
				throw new InvalidOperationException(SR.GetString("DataGridBeginInit"));
			}
			this.inInit = true;
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00048AF8 File Offset: 0x00046CF8
		private Rectangle CalcRowResizeFeedbackRect(MouseEventArgs e)
		{
			Rectangle data = this.layout.Data;
			Rectangle rectangle = new Rectangle(data.X, e.Y, data.Width, 3);
			rectangle.Y = Math.Min(data.Bottom - 3, rectangle.Y);
			rectangle.Y = Math.Max(rectangle.Y, 0);
			return rectangle;
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00048B60 File Offset: 0x00046D60
		private Rectangle CalcColResizeFeedbackRect(MouseEventArgs e)
		{
			Rectangle data = this.layout.Data;
			Rectangle rectangle = new Rectangle(e.X, data.Y, 3, data.Height);
			rectangle.X = Math.Min(data.Right - 3, rectangle.X);
			rectangle.X = Math.Max(rectangle.X, 0);
			return rectangle;
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00048BC6 File Offset: 0x00046DC6
		private void CancelCursorUpdate()
		{
			if (this.listManager != null)
			{
				this.EndEdit();
				this.listManager.CancelCurrentEdit();
			}
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00048BE4 File Offset: 0x00046DE4
		private void CheckHierarchyState()
		{
			if (this.checkHierarchy && this.listManager != null && this.myGridTable != null)
			{
				if (this.myGridTable == null)
				{
					return;
				}
				for (int i = 0; i < this.myGridTable.GridColumnStyles.Count; i++)
				{
					DataGridColumnStyle dataGridColumnStyle = this.myGridTable.GridColumnStyles[i];
				}
				this.checkHierarchy = false;
			}
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00048C46 File Offset: 0x00046E46
		private void ClearRegionCache()
		{
			this.cachedScrollableRegion = null;
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00048C50 File Offset: 0x00046E50
		private void ColAutoResize(int col)
		{
			this.EndEdit();
			CurrencyManager currencyManager = this.listManager;
			if (currencyManager == null)
			{
				return;
			}
			Graphics graphics = base.CreateGraphicsInternal();
			try
			{
				DataGridColumnStyle dataGridColumnStyle = this.myGridTable.GridColumnStyles[col];
				string headerText = dataGridColumnStyle.HeaderText;
				Font font;
				if (this.myGridTable.IsDefault)
				{
					font = this.HeaderFont;
				}
				else
				{
					font = this.myGridTable.HeaderFont;
				}
				int num = (int)graphics.MeasureString(headerText, font).Width + this.layout.ColumnHeaders.Height + 1;
				int count = currencyManager.Count;
				for (int i = 0; i < count; i++)
				{
					object columnValueAtRow = dataGridColumnStyle.GetColumnValueAtRow(currencyManager, i);
					int width = dataGridColumnStyle.GetPreferredSize(graphics, columnValueAtRow).Width;
					if (width > num)
					{
						num = width;
					}
				}
				if (dataGridColumnStyle.Width != num)
				{
					dataGridColumnStyle.width = num;
					this.ComputeVisibleColumns();
					bool flag = true;
					if (this.lastTotallyVisibleCol != -1)
					{
						for (int j = this.lastTotallyVisibleCol + 1; j < this.myGridTable.GridColumnStyles.Count; j++)
						{
							if (this.myGridTable.GridColumnStyles[j].PropertyDescriptor != null)
							{
								flag = false;
								break;
							}
						}
					}
					else
					{
						flag = false;
					}
					if (flag && (this.negOffset != 0 || this.horizontalOffset != 0))
					{
						dataGridColumnStyle.width = num;
						int num2 = 0;
						int count2 = this.myGridTable.GridColumnStyles.Count;
						int width2 = this.layout.Data.Width;
						GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
						this.negOffset = 0;
						this.horizontalOffset = 0;
						this.firstVisibleCol = 0;
						for (int k = count2 - 1; k >= 0; k--)
						{
							if (gridColumnStyles[k].PropertyDescriptor != null)
							{
								num2 += gridColumnStyles[k].Width;
								if (num2 > width2)
								{
									if (this.negOffset == 0)
									{
										this.firstVisibleCol = k;
										this.negOffset = num2 - width2;
										this.horizontalOffset = this.negOffset;
										this.numVisibleCols++;
									}
									else
									{
										this.horizontalOffset += gridColumnStyles[k].Width;
									}
								}
								else
								{
									this.numVisibleCols++;
								}
							}
						}
						base.PerformLayout();
						base.Invalidate(Rectangle.Union(this.layout.Data, this.layout.ColumnHeaders));
					}
					else
					{
						base.PerformLayout();
						Rectangle rectangle = this.layout.Data;
						if (this.layout.ColumnHeadersVisible)
						{
							rectangle = Rectangle.Union(rectangle, this.layout.ColumnHeaders);
						}
						int colBeg = this.GetColBeg(col);
						if (!this.isRightToLeft())
						{
							rectangle.Width -= colBeg - rectangle.X;
							rectangle.X = colBeg;
						}
						else
						{
							rectangle.Width -= colBeg;
						}
						base.Invalidate(rectangle);
					}
				}
			}
			finally
			{
				graphics.Dispose();
			}
			if (this.horizScrollBar.Visible)
			{
				this.horizScrollBar.Value = this.HorizontalOffset;
			}
		}

		/// <summary>Collapses child relations, if any exist for all rows, or for a specified row.</summary>
		/// <param name="row">The number of the row to collapse. If set to -1, all rows are collapsed.</param>
		// Token: 0x0600155E RID: 5470 RVA: 0x00048F88 File Offset: 0x00047188
		public void Collapse(int row)
		{
			this.SetRowExpansionState(row, false);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00048F94 File Offset: 0x00047194
		private void ColResizeBegin(MouseEventArgs e, int col)
		{
			int x = e.X;
			this.EndEdit();
			Rectangle rectangle = Rectangle.Union(this.layout.ColumnHeaders, this.layout.Data);
			if (this.isRightToLeft())
			{
				rectangle.Width = this.GetColBeg(col) - this.layout.Data.X - 2;
			}
			else
			{
				int colBeg = this.GetColBeg(col);
				rectangle.X = colBeg + 3;
				rectangle.Width = this.layout.Data.X + this.layout.Data.Width - colBeg - 2;
			}
			base.CaptureInternal = true;
			Cursor.ClipInternal = base.RectangleToScreen(rectangle);
			this.gridState[8] = true;
			this.trackColAnchor = x;
			this.trackColumn = col;
			this.DrawColSplitBar(e);
			this.lastSplitBar = e;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0004906F File Offset: 0x0004726F
		private void ColResizeMove(MouseEventArgs e)
		{
			if (this.lastSplitBar != null)
			{
				this.DrawColSplitBar(this.lastSplitBar);
				this.lastSplitBar = e;
			}
			this.DrawColSplitBar(e);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00049094 File Offset: 0x00047294
		private void ColResizeEnd(MouseEventArgs e)
		{
			this.gridState[16777216] = true;
			try
			{
				if (this.lastSplitBar != null)
				{
					this.DrawColSplitBar(this.lastSplitBar);
					this.lastSplitBar = null;
				}
				bool flag = this.isRightToLeft();
				int num = (flag ? Math.Max(e.X, this.layout.Data.X) : Math.Min(e.X, this.layout.Data.Right + 1));
				int num2 = num - this.GetColEnd(this.trackColumn);
				if (flag)
				{
					num2 = -num2;
				}
				if (this.trackColAnchor != num && num2 != 0)
				{
					DataGridColumnStyle dataGridColumnStyle = this.myGridTable.GridColumnStyles[this.trackColumn];
					int num3 = dataGridColumnStyle.Width + num2;
					num3 = Math.Max(num3, 3);
					dataGridColumnStyle.Width = num3;
					this.ComputeVisibleColumns();
					bool flag2 = true;
					for (int i = this.lastTotallyVisibleCol + 1; i < this.myGridTable.GridColumnStyles.Count; i++)
					{
						if (this.myGridTable.GridColumnStyles[i].PropertyDescriptor != null)
						{
							flag2 = false;
							break;
						}
					}
					if (flag2 && (this.negOffset != 0 || this.horizontalOffset != 0))
					{
						int num4 = 0;
						int count = this.myGridTable.GridColumnStyles.Count;
						int width = this.layout.Data.Width;
						GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
						this.negOffset = 0;
						this.horizontalOffset = 0;
						this.firstVisibleCol = 0;
						for (int j = count - 1; j > -1; j--)
						{
							if (gridColumnStyles[j].PropertyDescriptor != null)
							{
								num4 += gridColumnStyles[j].Width;
								if (num4 > width)
								{
									if (this.negOffset == 0)
									{
										this.negOffset = num4 - width;
										this.firstVisibleCol = j;
										this.horizontalOffset = this.negOffset;
										this.numVisibleCols++;
									}
									else
									{
										this.horizontalOffset += gridColumnStyles[j].Width;
									}
								}
								else
								{
									this.numVisibleCols++;
								}
							}
						}
						base.Invalidate(Rectangle.Union(this.layout.Data, this.layout.ColumnHeaders));
					}
					else
					{
						Rectangle rectangle = Rectangle.Union(this.layout.ColumnHeaders, this.layout.Data);
						int colBeg = this.GetColBeg(this.trackColumn);
						rectangle.Width -= (flag ? (rectangle.Right - colBeg) : (colBeg - rectangle.X));
						rectangle.X = (flag ? this.layout.Data.X : colBeg);
						base.Invalidate(rectangle);
					}
				}
			}
			finally
			{
				Cursor.ClipInternal = Rectangle.Empty;
				base.CaptureInternal = false;
				this.gridState[16777216] = false;
			}
			base.PerformLayout();
			if (this.horizScrollBar.Visible)
			{
				this.horizScrollBar.Value = this.HorizontalOffset;
			}
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x000493C4 File Offset: 0x000475C4
		private void MetaDataChanged()
		{
			this.parentRows.Clear();
			this.caption.BackButtonActive = (this.caption.DownButtonActive = (this.caption.BackButtonVisible = false));
			this.caption.SetDownButtonDirection(!this.layout.ParentRowsVisible);
			this.gridState[4194304] = true;
			try
			{
				if (this.originalState != null)
				{
					this.Set_ListManager(this.originalState.DataSource, this.originalState.DataMember, true);
					this.originalState = null;
				}
				else
				{
					this.Set_ListManager(this.DataSource, this.DataMember, true);
				}
			}
			finally
			{
				this.gridState[4194304] = false;
			}
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00049494 File Offset: 0x00047694
		private void RowAutoResize(int row)
		{
			this.EndEdit();
			CurrencyManager currencyManager = this.ListManager;
			if (currencyManager == null)
			{
				return;
			}
			Graphics graphics = base.CreateGraphicsInternal();
			try
			{
				GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
				DataGridRow dataGridRow = this.DataGridRows[row];
				int count = currencyManager.Count;
				int num = 0;
				int count2 = gridColumnStyles.Count;
				for (int i = 0; i < count2; i++)
				{
					object columnValueAtRow = gridColumnStyles[i].GetColumnValueAtRow(currencyManager, row);
					num = Math.Max(num, gridColumnStyles[i].GetPreferredHeight(graphics, columnValueAtRow));
				}
				if (dataGridRow.Height != num)
				{
					dataGridRow.Height = num;
					base.PerformLayout();
					Rectangle rectangle = this.layout.Data;
					if (this.layout.RowHeadersVisible)
					{
						rectangle = Rectangle.Union(rectangle, this.layout.RowHeaders);
					}
					int rowTop = this.GetRowTop(row);
					rectangle.Height -= rectangle.Y - rowTop;
					rectangle.Y = rowTop;
					base.Invalidate(rectangle);
				}
			}
			finally
			{
				graphics.Dispose();
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x000495B0 File Offset: 0x000477B0
		private void RowResizeBegin(MouseEventArgs e, int row)
		{
			int y = e.Y;
			this.EndEdit();
			Rectangle rectangle = Rectangle.Union(this.layout.RowHeaders, this.layout.Data);
			int rowTop = this.GetRowTop(row);
			rectangle.Y = rowTop + 3;
			rectangle.Height = this.layout.Data.Y + this.layout.Data.Height - rowTop - 2;
			base.CaptureInternal = true;
			Cursor.ClipInternal = base.RectangleToScreen(rectangle);
			this.gridState[16] = true;
			this.trackRowAnchor = y;
			this.trackRow = row;
			this.DrawRowSplitBar(e);
			this.lastSplitBar = e;
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00049661 File Offset: 0x00047861
		private void RowResizeMove(MouseEventArgs e)
		{
			if (this.lastSplitBar != null)
			{
				this.DrawRowSplitBar(this.lastSplitBar);
				this.lastSplitBar = e;
			}
			this.DrawRowSplitBar(e);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00049688 File Offset: 0x00047888
		private void RowResizeEnd(MouseEventArgs e)
		{
			try
			{
				if (this.lastSplitBar != null)
				{
					this.DrawRowSplitBar(this.lastSplitBar);
					this.lastSplitBar = null;
				}
				int num = Math.Min(e.Y, this.layout.Data.Y + this.layout.Data.Height + 1);
				int num2 = num - this.GetRowBottom(this.trackRow);
				if (this.trackRowAnchor != num && num2 != 0)
				{
					DataGridRow dataGridRow = this.DataGridRows[this.trackRow];
					int num3 = dataGridRow.Height + num2;
					num3 = Math.Max(num3, 3);
					dataGridRow.Height = num3;
					base.PerformLayout();
					Rectangle rectangle = Rectangle.Union(this.layout.RowHeaders, this.layout.Data);
					int rowTop = this.GetRowTop(this.trackRow);
					rectangle.Height -= rectangle.Y - rowTop;
					rectangle.Y = rowTop;
					base.Invalidate(rectangle);
				}
			}
			finally
			{
				Cursor.ClipInternal = Rectangle.Empty;
				base.CaptureInternal = false;
			}
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x000497A4 File Offset: 0x000479A4
		private void ColumnHeaderClicked(PropertyDescriptor prop)
		{
			if (!this.CommitEdit())
			{
				return;
			}
			bool flag;
			if (this.myGridTable.IsDefault)
			{
				flag = this.AllowSorting;
			}
			else
			{
				flag = this.myGridTable.AllowSorting;
			}
			if (!flag)
			{
				return;
			}
			ListSortDirection listSortDirection = this.ListManager.GetSortDirection();
			PropertyDescriptor sortProperty = this.ListManager.GetSortProperty();
			if (sortProperty != null && sortProperty.Equals(prop))
			{
				listSortDirection = ((listSortDirection == ListSortDirection.Ascending) ? ListSortDirection.Descending : ListSortDirection.Ascending);
			}
			else
			{
				listSortDirection = ListSortDirection.Ascending;
			}
			if (this.listManager.Count == 0)
			{
				return;
			}
			this.ListManager.SetSort(prop, listSortDirection);
			this.ResetSelection();
			this.InvalidateInside();
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00049838 File Offset: 0x00047A38
		private bool CommitEdit()
		{
			if ((!this.gridState[32768] && !this.gridState[16384]) || (this.gridState[65536] && !this.gridState[131072]))
			{
				return true;
			}
			this.gridState[65536] = true;
			if (this.editColumn.ReadOnly || this.gridState[1048576])
			{
				bool flag = false;
				if (base.ContainsFocus)
				{
					flag = true;
				}
				if (flag && this.gridState[2048])
				{
					this.FocusInternal();
				}
				this.editColumn.ConcedeFocus();
				if (flag && this.gridState[2048] && base.CanFocus && !this.Focused)
				{
					this.FocusInternal();
				}
				this.gridState[65536] = false;
				return true;
			}
			bool flag2 = this.editColumn.Commit(this.ListManager, this.currentRow);
			this.gridState[65536] = false;
			if (flag2)
			{
				this.gridState[32768] = false;
			}
			return flag2;
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00049970 File Offset: 0x00047B70
		private int ComputeDeltaRows(int targetRow)
		{
			if (this.firstVisibleRow == targetRow)
			{
				return 0;
			}
			int num = -1;
			int num2 = -1;
			int num3 = this.DataGridRowsLength;
			int num4 = 0;
			DataGridRow[] array = this.DataGridRows;
			for (int i = 0; i < num3; i++)
			{
				if (i == this.firstVisibleRow)
				{
					num = num4;
				}
				if (i == targetRow)
				{
					num2 = num4;
				}
				if (num2 != -1 && num != -1)
				{
					break;
				}
				num4 += array[i].Height;
			}
			int num5 = num2 + array[targetRow].Height;
			int num6 = this.layout.Data.Height + num;
			if (num5 > num6)
			{
				int num7 = num5 - num6;
				num += num7;
			}
			else
			{
				if (num < num2)
				{
					return 0;
				}
				int num8 = num - num2;
				num -= num8;
			}
			int num9 = this.ComputeFirstVisibleRow(num);
			return num9 - this.firstVisibleRow;
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00049A38 File Offset: 0x00047C38
		private int ComputeFirstVisibleRow(int firstVisibleRowLogicalTop)
		{
			int num = this.DataGridRowsLength;
			int num2 = 0;
			DataGridRow[] array = this.DataGridRows;
			int num3 = 0;
			while (num3 < num && num2 < firstVisibleRowLogicalTop)
			{
				num2 += array[num3].Height;
				num3++;
			}
			return num3;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00049A74 File Offset: 0x00047C74
		private void ComputeLayout()
		{
			bool flag = !this.isRightToLeft();
			Rectangle resizeBoxRect = this.layout.ResizeBoxRect;
			this.EndEdit();
			this.ClearRegionCache();
			DataGrid.LayoutData layoutData = new DataGrid.LayoutData(this.layout);
			layoutData.Inside = base.ClientRectangle;
			Rectangle inside = layoutData.Inside;
			int borderWidth = this.BorderWidth;
			inside.Inflate(-borderWidth, -borderWidth);
			Rectangle rectangle = inside;
			if (this.layout.CaptionVisible)
			{
				int num = this.captionFontHeight + 6;
				Rectangle rectangle2 = layoutData.Caption;
				rectangle2 = rectangle;
				rectangle2.Height = num;
				rectangle.Y += num;
				rectangle.Height -= num;
				layoutData.Caption = rectangle2;
			}
			else
			{
				layoutData.Caption = Rectangle.Empty;
			}
			if (this.layout.ParentRowsVisible)
			{
				Rectangle rectangle3 = layoutData.ParentRows;
				int height = this.parentRows.Height;
				rectangle3 = rectangle;
				rectangle3.Height = height;
				rectangle.Y += height;
				rectangle.Height -= height;
				layoutData.ParentRows = rectangle3;
			}
			else
			{
				layoutData.ParentRows = Rectangle.Empty;
			}
			int num2 = this.headerFontHeight + 6;
			if (this.layout.ColumnHeadersVisible)
			{
				Rectangle rectangle4 = layoutData.ColumnHeaders;
				rectangle4 = rectangle;
				rectangle4.Height = num2;
				rectangle.Y += num2;
				rectangle.Height -= num2;
				layoutData.ColumnHeaders = rectangle4;
			}
			else
			{
				layoutData.ColumnHeaders = Rectangle.Empty;
			}
			bool flag2 = (this.myGridTable.IsDefault ? this.RowHeadersVisible : this.myGridTable.RowHeadersVisible);
			int num3 = (this.myGridTable.IsDefault ? this.RowHeaderWidth : this.myGridTable.RowHeaderWidth);
			layoutData.RowHeadersVisible = flag2;
			if (this.myGridTable != null && flag2)
			{
				Rectangle rectangle5 = layoutData.RowHeaders;
				if (flag)
				{
					rectangle5 = rectangle;
					rectangle5.Width = num3;
					rectangle.X += num3;
					rectangle.Width -= num3;
				}
				else
				{
					rectangle5 = rectangle;
					rectangle5.Width = num3;
					rectangle5.X = rectangle.Right - num3;
					rectangle.Width -= num3;
				}
				layoutData.RowHeaders = rectangle5;
				if (this.layout.ColumnHeadersVisible)
				{
					Rectangle rectangle6 = layoutData.TopLeftHeader;
					Rectangle columnHeaders = layoutData.ColumnHeaders;
					if (flag)
					{
						rectangle6 = columnHeaders;
						rectangle6.Width = num3;
						columnHeaders.Width -= num3;
						columnHeaders.X += num3;
					}
					else
					{
						rectangle6 = columnHeaders;
						rectangle6.Width = num3;
						rectangle6.X = columnHeaders.Right - num3;
						columnHeaders.Width -= num3;
					}
					layoutData.TopLeftHeader = rectangle6;
					layoutData.ColumnHeaders = columnHeaders;
				}
				else
				{
					layoutData.TopLeftHeader = Rectangle.Empty;
				}
			}
			else
			{
				layoutData.RowHeaders = Rectangle.Empty;
				layoutData.TopLeftHeader = Rectangle.Empty;
			}
			layoutData.Data = rectangle;
			layoutData.Inside = inside;
			this.layout = layoutData;
			this.LayoutScrollBars();
			if (!resizeBoxRect.Equals(this.layout.ResizeBoxRect) && !this.layout.ResizeBoxRect.IsEmpty)
			{
				base.Invalidate(this.layout.ResizeBoxRect);
			}
			this.layout.dirty = false;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00049DEC File Offset: 0x00047FEC
		private int ComputeRowDelta(int from, int to)
		{
			int num = from;
			int num2 = to;
			int num3 = -1;
			if (num > num2)
			{
				num = to;
				num2 = from;
				num3 = 1;
			}
			DataGridRow[] array = this.DataGridRows;
			int num4 = 0;
			for (int i = num; i < num2; i++)
			{
				num4 += array[i].Height;
			}
			return num3 * num4;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00049E35 File Offset: 0x00048035
		internal int MinimumRowHeaderWidth()
		{
			return this.minRowHeaderWidth;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00049E40 File Offset: 0x00048040
		internal void ComputeMinimumRowHeaderWidth()
		{
			this.minRowHeaderWidth = 15;
			if (this.ListHasErrors)
			{
				this.minRowHeaderWidth += 15;
			}
			if (this.myGridTable != null && this.myGridTable.RelationsList.Count != 0)
			{
				this.minRowHeaderWidth += 15;
			}
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00049E98 File Offset: 0x00048098
		private void ComputeVisibleColumns()
		{
			this.EnsureBound();
			GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
			int count = gridColumnStyles.Count;
			int num = -this.negOffset;
			int num2 = 0;
			int width = this.layout.Data.Width;
			int num3 = this.firstVisibleCol;
			if (width < 0 || gridColumnStyles.Count == 0)
			{
				this.numVisibleCols = (this.firstVisibleCol = 0);
				this.lastTotallyVisibleCol = -1;
				return;
			}
			while (num < width && num3 < count)
			{
				if (gridColumnStyles[num3].PropertyDescriptor != null)
				{
					num += gridColumnStyles[num3].Width;
				}
				num3++;
				num2++;
			}
			this.numVisibleCols = num2;
			if (num < width)
			{
				int num4 = this.firstVisibleCol - 1;
				while (num4 > 0 && num + gridColumnStyles[num4].Width <= width)
				{
					if (gridColumnStyles[num4].PropertyDescriptor != null)
					{
						num += gridColumnStyles[num4].Width;
					}
					num2++;
					this.firstVisibleCol--;
					num4--;
				}
				if (this.numVisibleCols != num2)
				{
					base.Invalidate(this.layout.Data);
					base.Invalidate(this.layout.ColumnHeaders);
					this.numVisibleCols = num2;
				}
			}
			this.lastTotallyVisibleCol = this.firstVisibleCol + this.numVisibleCols - 1;
			if (num > width)
			{
				if (this.numVisibleCols <= 1 || (this.numVisibleCols == 2 && this.negOffset != 0))
				{
					this.lastTotallyVisibleCol = -1;
					return;
				}
				this.lastTotallyVisibleCol--;
			}
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0004A024 File Offset: 0x00048224
		private int ComputeFirstVisibleColumn()
		{
			int i = 0;
			if (this.horizontalOffset == 0)
			{
				this.negOffset = 0;
				return 0;
			}
			if (this.myGridTable != null && this.myGridTable.GridColumnStyles != null && this.myGridTable.GridColumnStyles.Count != 0)
			{
				GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
				int num = 0;
				int count = gridColumnStyles.Count;
				if (gridColumnStyles[0].Width == -1)
				{
					this.negOffset = 0;
					return 0;
				}
				for (i = 0; i < count; i++)
				{
					if (gridColumnStyles[i].PropertyDescriptor != null)
					{
						num += gridColumnStyles[i].Width;
					}
					if (num > this.horizontalOffset)
					{
						break;
					}
				}
				if (i == count)
				{
					this.negOffset = 0;
					return 0;
				}
				this.negOffset = gridColumnStyles[i].Width - (num - this.horizontalOffset);
			}
			return i;
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0004A0FC File Offset: 0x000482FC
		private void ComputeVisibleRows()
		{
			this.EnsureBound();
			Rectangle data = this.layout.Data;
			int height = data.Height;
			int num = 0;
			int num2 = 0;
			DataGridRow[] array = this.DataGridRows;
			int num3 = this.DataGridRowsLength;
			if (height < 0)
			{
				this.numVisibleRows = (this.numTotallyVisibleRows = 0);
				return;
			}
			int num4 = this.firstVisibleRow;
			while (num4 < num3 && num <= height)
			{
				num += array[num4].Height;
				num2++;
				num4++;
			}
			if (num < height)
			{
				for (int i = this.firstVisibleRow - 1; i >= 0; i--)
				{
					int height2 = array[i].Height;
					if (num + height2 > height)
					{
						break;
					}
					num += height2;
					this.firstVisibleRow--;
					num2++;
				}
			}
			this.numVisibleRows = (this.numTotallyVisibleRows = num2);
			if (num > height)
			{
				this.numTotallyVisibleRows--;
			}
		}

		/// <summary>Constructs a new instance of the accessibility object for this control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" /> for this control.</returns>
		// Token: 0x06001572 RID: 5490 RVA: 0x0004A1E2 File Offset: 0x000483E2
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGrid.DataGridAccessibleObject(this);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0004A1EC File Offset: 0x000483EC
		private DataGridState CreateChildState(string relationName, DataGridRow source)
		{
			DataGridState dataGridState = new DataGridState();
			string text;
			if (string.IsNullOrEmpty(this.DataMember))
			{
				text = relationName;
			}
			else
			{
				text = this.DataMember + "." + relationName;
			}
			CurrencyManager currencyManager = (CurrencyManager)this.BindingContext[this.DataSource, text];
			dataGridState.DataSource = this.DataSource;
			dataGridState.DataMember = text;
			dataGridState.ListManager = currencyManager;
			dataGridState.DataGridRows = null;
			dataGridState.DataGridRowsLength = currencyManager.Count + (this.policy.AllowAdd ? 1 : 0);
			return dataGridState;
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0004A27C File Offset: 0x0004847C
		private DataGrid.LayoutData CreateInitialLayoutState()
		{
			return new DataGrid.LayoutData
			{
				Inside = default(Rectangle),
				TopLeftHeader = default(Rectangle),
				ColumnHeaders = default(Rectangle),
				RowHeaders = default(Rectangle),
				Data = default(Rectangle),
				Caption = default(Rectangle),
				ParentRows = default(Rectangle),
				ResizeBoxRect = default(Rectangle),
				ColumnHeadersVisible = true,
				RowHeadersVisible = true,
				CaptionVisible = true,
				ParentRowsVisible = true,
				ClientRectangle = base.ClientRectangle
			};
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0004A318 File Offset: 0x00048518
		private NativeMethods.RECT[] CreateScrollableRegion(Rectangle scroll)
		{
			if (this.cachedScrollableRegion != null)
			{
				return this.cachedScrollableRegion;
			}
			bool flag = this.isRightToLeft();
			using (Region region = new Region(scroll))
			{
				int num = this.numVisibleRows;
				int num2 = this.layout.Data.Y;
				int x = this.layout.Data.X;
				DataGridRow[] array = this.DataGridRows;
				for (int i = this.firstVisibleRow; i < num; i++)
				{
					int height = array[i].Height;
					Rectangle nonScrollableArea = array[i].GetNonScrollableArea();
					nonScrollableArea.X += x;
					nonScrollableArea.X = this.MirrorRectangle(nonScrollableArea, this.layout.Data, flag);
					if (!nonScrollableArea.IsEmpty)
					{
						region.Exclude(new Rectangle(nonScrollableArea.X, nonScrollableArea.Y + num2, nonScrollableArea.Width, nonScrollableArea.Height));
					}
					num2 += height;
				}
				using (Graphics graphics = base.CreateGraphicsInternal())
				{
					IntPtr hrgn = region.GetHrgn(graphics);
					if (hrgn != IntPtr.Zero)
					{
						this.cachedScrollableRegion = UnsafeNativeMethods.GetRectsFromRegion(hrgn);
						IntSecurity.ObjectFromWin32Handle.Assert();
						try
						{
							region.ReleaseHrgn(hrgn);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
				}
			}
			return this.cachedScrollableRegion;
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.DataGrid" />.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001576 RID: 5494 RVA: 0x0004A4BC File Offset: 0x000486BC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.vertScrollBar != null)
				{
					this.vertScrollBar.Dispose();
				}
				if (this.horizScrollBar != null)
				{
					this.horizScrollBar.Dispose();
				}
				if (this.toBeDisposedEditingControl != null)
				{
					this.toBeDisposedEditingControl.Dispose();
					this.toBeDisposedEditingControl = null;
				}
				GridTableStylesCollection tableStyles = this.TableStyles;
				if (tableStyles != null)
				{
					for (int i = 0; i < tableStyles.Count; i++)
					{
						tableStyles[i].Dispose();
					}
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0004A53C File Offset: 0x0004873C
		private void DrawColSplitBar(MouseEventArgs e)
		{
			Rectangle rectangle = this.CalcColResizeFeedbackRect(e);
			this.DrawSplitBar(rectangle);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0004A558 File Offset: 0x00048758
		private void DrawRowSplitBar(MouseEventArgs e)
		{
			Rectangle rectangle = this.CalcRowResizeFeedbackRect(e);
			this.DrawSplitBar(rectangle);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0004A574 File Offset: 0x00048774
		private void DrawSplitBar(Rectangle r)
		{
			IntPtr handle = base.Handle;
			IntPtr dcex = UnsafeNativeMethods.GetDCEx(new HandleRef(this, handle), NativeMethods.NullHandleRef, 1026);
			IntPtr intPtr = ControlPaint.CreateHalftoneHBRUSH();
			IntPtr intPtr2 = SafeNativeMethods.SelectObject(new HandleRef(this, dcex), new HandleRef(null, intPtr));
			SafeNativeMethods.PatBlt(new HandleRef(this, dcex), r.X, r.Y, r.Width, r.Height, 5898313);
			SafeNativeMethods.SelectObject(new HandleRef(this, dcex), new HandleRef(null, intPtr2));
			SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
			UnsafeNativeMethods.ReleaseDC(new HandleRef(this, handle), new HandleRef(this, dcex));
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0004A61C File Offset: 0x0004881C
		private void Edit()
		{
			this.Edit(null);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0004A628 File Offset: 0x00048828
		private void Edit(string displayText)
		{
			this.EnsureBound();
			bool flag = true;
			this.EndEdit();
			DataGridRow[] array = this.DataGridRows;
			if (this.DataGridRowsLength == 0)
			{
				return;
			}
			array[this.currentRow].OnEdit();
			this.editRow = array[this.currentRow];
			if (this.myGridTable.GridColumnStyles.Count == 0)
			{
				return;
			}
			this.editColumn = this.myGridTable.GridColumnStyles[this.currentCol];
			if (this.editColumn.PropertyDescriptor == null)
			{
				return;
			}
			Rectangle rectangle = Rectangle.Empty;
			if (this.currentRow < this.firstVisibleRow || this.currentRow > this.firstVisibleRow + this.numVisibleRows || this.currentCol < this.firstVisibleCol || this.currentCol > this.firstVisibleCol + this.numVisibleCols - 1 || (this.currentCol == this.firstVisibleCol && this.negOffset != 0))
			{
				flag = false;
			}
			else
			{
				rectangle = this.GetCellBounds(this.currentRow, this.currentCol);
			}
			this.gridState[16384] = true;
			this.gridState[32768] = false;
			this.gridState[65536] = true;
			this.editColumn.Edit(this.ListManager, this.currentRow, rectangle, this.myGridTable.ReadOnly || this.ReadOnly || !this.policy.AllowEdit, displayText, flag);
			this.gridState[65536] = false;
		}

		/// <summary>Requests an end to an edit operation taking place on the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <param name="gridColumn">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to cease editing.</param>
		/// <param name="rowNumber">The number of the row to cease editing.</param>
		/// <param name="shouldAbort">Set to <see langword="true" /> if the current operation should be stopped.</param>
		/// <returns>
		///   <see langword="true" /> if the editing operation ceases; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600157C RID: 5500 RVA: 0x0004A7AC File Offset: 0x000489AC
		public bool EndEdit(DataGridColumnStyle gridColumn, int rowNumber, bool shouldAbort)
		{
			bool flag = false;
			if (this.gridState[32768])
			{
				DataGridColumnStyle dataGridColumnStyle = this.editColumn;
				int rowNumber2 = this.editRow.RowNumber;
				if (shouldAbort)
				{
					this.AbortEdit();
					flag = true;
				}
				else
				{
					flag = this.CommitEdit();
				}
			}
			return flag;
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0004A7F9 File Offset: 0x000489F9
		private void EndEdit()
		{
			if (!this.gridState[32768] && !this.gridState[16384])
			{
				return;
			}
			if (!this.CommitEdit())
			{
				this.AbortEdit();
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0004A830 File Offset: 0x00048A30
		private void EnforceValidDataMember(object value)
		{
			if (this.DataMember == null || this.DataMember.Length == 0)
			{
				return;
			}
			if (this.BindingContext == null)
			{
				return;
			}
			try
			{
				BindingManagerBase bindingManagerBase = this.BindingContext[value, this.dataMember];
			}
			catch
			{
				this.dataMember = "";
			}
		}

		/// <summary>Informs the <see cref="T:System.Windows.Forms.DataGrid" /> control when the user begins to edit the column at the specified location.</summary>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that defines the location of the edited column.</param>
		// Token: 0x0600157F RID: 5503 RVA: 0x0004A890 File Offset: 0x00048A90
		protected internal virtual void ColumnStartedEditing(Rectangle bounds)
		{
			DataGridRow[] array = this.DataGridRows;
			if (bounds.IsEmpty && this.editColumn is DataGridTextBoxColumn && this.currentRow != -1 && this.currentCol != -1)
			{
				DataGridTextBoxColumn dataGridTextBoxColumn = this.editColumn as DataGridTextBoxColumn;
				Rectangle cellBounds = this.GetCellBounds(this.currentRow, this.currentCol);
				this.gridState[65536] = true;
				try
				{
					dataGridTextBoxColumn.TextBox.Bounds = cellBounds;
				}
				finally
				{
					this.gridState[65536] = false;
				}
			}
			if (this.gridState[1048576])
			{
				int num = this.DataGridRowsLength;
				DataGridRow[] array2 = new DataGridRow[num + 1];
				for (int i = 0; i < num; i++)
				{
					array2[i] = array[i];
				}
				array2[num] = new DataGridAddNewRow(this, this.myGridTable, num);
				this.SetDataGridRows(array2, num + 1);
				this.Edit();
				this.gridState[1048576] = false;
				this.gridState[32768] = true;
				this.gridState[16384] = false;
				return;
			}
			this.gridState[32768] = true;
			this.gridState[16384] = false;
			this.InvalidateRowHeader(this.currentRow);
			array[this.currentRow].LoseChildFocus(this.layout.RowHeaders, this.isRightToLeft());
		}

		/// <summary>Informs the <see cref="T:System.Windows.Forms.DataGrid" /> control when the user begins to edit a column using the specified control.</summary>
		/// <param name="editingControl">The <see cref="T:System.Windows.Forms.Control" /> used to edit the column.</param>
		// Token: 0x06001580 RID: 5504 RVA: 0x0004AA10 File Offset: 0x00048C10
		protected internal virtual void ColumnStartedEditing(Control editingControl)
		{
			this.ColumnStartedEditing(editingControl.Bounds);
		}

		/// <summary>Displays child relations, if any exist, for all rows or a specific row.</summary>
		/// <param name="row">The number of the row to expand. If set to -1, all rows are expanded.</param>
		// Token: 0x06001581 RID: 5505 RVA: 0x0004AA1E File Offset: 0x00048C1E
		public void Expand(int row)
		{
			this.SetRowExpansionState(row, true);
		}

		/// <summary>Creates a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> using the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to use for creating the grid column style.</param>
		/// <param name="isDefault">
		///   <see langword="true" /> to set the column style as the default; otherwise, <see langword="false" />.</param>
		/// <returns>The new <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		// Token: 0x06001582 RID: 5506 RVA: 0x0004AA28 File Offset: 0x00048C28
		protected virtual DataGridColumnStyle CreateGridColumn(PropertyDescriptor prop, bool isDefault)
		{
			if (this.myGridTable != null)
			{
				return this.myGridTable.CreateGridColumn(prop, isDefault);
			}
			return null;
		}

		/// <summary>Creates a new <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to use for creating the grid column style.</param>
		/// <returns>The new <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
		// Token: 0x06001583 RID: 5507 RVA: 0x0004AA41 File Offset: 0x00048C41
		protected virtual DataGridColumnStyle CreateGridColumn(PropertyDescriptor prop)
		{
			if (this.myGridTable != null)
			{
				return this.myGridTable.CreateGridColumn(prop);
			}
			return null;
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Windows.Forms.DataGrid" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		// Token: 0x06001584 RID: 5508 RVA: 0x0004AA5C File Offset: 0x00048C5C
		public void EndInit()
		{
			this.inInit = false;
			if (this.myGridTable == null && this.ListManager != null)
			{
				this.SetDataGridTable(this.TableStyles[this.ListManager.GetListName()], true);
			}
			if (this.myGridTable != null)
			{
				this.myGridTable.DataGrid = this;
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0004AAB4 File Offset: 0x00048CB4
		private int GetColFromX(int x)
		{
			if (this.myGridTable == null)
			{
				return -1;
			}
			Rectangle data = this.layout.Data;
			x = this.MirrorPoint(x, data, this.isRightToLeft());
			GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
			int count = gridColumnStyles.Count;
			int num = data.X - this.negOffset;
			int num2 = this.firstVisibleCol;
			while (num < data.Width + data.X && num2 < count)
			{
				if (gridColumnStyles[num2].PropertyDescriptor != null)
				{
					num += gridColumnStyles[num2].Width;
				}
				if (num > x)
				{
					return num2;
				}
				num2++;
			}
			return -1;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0004AB58 File Offset: 0x00048D58
		internal int GetColBeg(int col)
		{
			int num = this.layout.Data.X - this.negOffset;
			GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
			int num2 = Math.Min(col, gridColumnStyles.Count);
			for (int i = this.firstVisibleCol; i < num2; i++)
			{
				if (gridColumnStyles[i].PropertyDescriptor != null)
				{
					num += gridColumnStyles[i].Width;
				}
			}
			return this.MirrorPoint(num, this.layout.Data, this.isRightToLeft());
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0004ABDC File Offset: 0x00048DDC
		internal int GetColEnd(int col)
		{
			int colBeg = this.GetColBeg(col);
			int width = this.myGridTable.GridColumnStyles[col].Width;
			if (!this.isRightToLeft())
			{
				return colBeg + width;
			}
			return colBeg - width;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0004AC18 File Offset: 0x00048E18
		private int GetColumnWidthSum()
		{
			int num = 0;
			if (this.myGridTable != null && this.myGridTable.GridColumnStyles != null)
			{
				GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
				int count = gridColumnStyles.Count;
				for (int i = 0; i < count; i++)
				{
					if (gridColumnStyles[i].PropertyDescriptor != null)
					{
						num += gridColumnStyles[i].Width;
					}
				}
			}
			return num;
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0004AC7C File Offset: 0x00048E7C
		private DataGridRelationshipRow[] GetExpandableRows()
		{
			int num = this.DataGridRowsLength;
			DataGridRow[] array = this.DataGridRows;
			if (this.policy.AllowAdd)
			{
				num = Math.Max(num - 1, 0);
			}
			DataGridRelationshipRow[] array2 = new DataGridRelationshipRow[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = (DataGridRelationshipRow)array[i];
			}
			return array2;
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0004ACD0 File Offset: 0x00048ED0
		private int GetRowFromY(int y)
		{
			Rectangle data = this.layout.Data;
			int num = data.Y;
			int num2 = this.firstVisibleRow;
			int num3 = this.DataGridRowsLength;
			DataGridRow[] array = this.DataGridRows;
			int bottom = data.Bottom;
			while (num < bottom && num2 < num3)
			{
				num += array[num2].Height;
				if (num > y)
				{
					return num2;
				}
				num2++;
			}
			return -1;
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0004AD32 File Offset: 0x00048F32
		internal Rectangle GetRowHeaderRect()
		{
			return this.layout.RowHeaders;
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0004AD3F File Offset: 0x00048F3F
		internal Rectangle GetColumnHeadersRect()
		{
			return this.layout.ColumnHeaders;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0004AD4C File Offset: 0x00048F4C
		private Rectangle GetRowRect(int rowNumber)
		{
			Rectangle data = this.layout.Data;
			int num = data.Y;
			DataGridRow[] array = this.DataGridRows;
			int num2 = this.firstVisibleRow;
			while (num2 <= rowNumber && num <= data.Bottom)
			{
				if (num2 == rowNumber)
				{
					Rectangle rectangle = new Rectangle(data.X, num, data.Width, array[num2].Height);
					if (this.layout.RowHeadersVisible)
					{
						rectangle.Width += this.layout.RowHeaders.Width;
						rectangle.X -= (this.isRightToLeft() ? 0 : this.layout.RowHeaders.Width);
					}
					return rectangle;
				}
				num += array[num2].Height;
				num2++;
			}
			return Rectangle.Empty;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0004AE24 File Offset: 0x00049024
		private int GetRowTop(int row)
		{
			DataGridRow[] array = this.DataGridRows;
			int num = this.layout.Data.Y;
			int num2 = Math.Min(row, this.DataGridRowsLength);
			for (int i = this.firstVisibleRow; i < num2; i++)
			{
				num += array[i].Height;
			}
			for (int j = this.firstVisibleRow; j > num2; j--)
			{
				num -= array[j].Height;
			}
			return num;
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x0004AE94 File Offset: 0x00049094
		private int GetRowBottom(int row)
		{
			DataGridRow[] array = this.DataGridRows;
			return this.GetRowTop(row) + array[row].Height;
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x0004AEB8 File Offset: 0x000490B8
		private void EnsureBound()
		{
			if (!this.Bound)
			{
				throw new InvalidOperationException(SR.GetString("DataGridUnbound"));
			}
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x0004AED4 File Offset: 0x000490D4
		private void EnsureVisible(int row, int col)
		{
			if (row < this.firstVisibleRow || row >= this.firstVisibleRow + this.numTotallyVisibleRows)
			{
				int num = this.ComputeDeltaRows(row);
				this.ScrollDown(num);
			}
			if (this.firstVisibleCol == 0 && this.numVisibleCols == 0 && this.lastTotallyVisibleCol == -1)
			{
				return;
			}
			int num2 = this.firstVisibleCol;
			int num3 = this.negOffset;
			int num4 = this.lastTotallyVisibleCol;
			while (col < this.firstVisibleCol || (col == this.firstVisibleCol && this.negOffset != 0) || (this.lastTotallyVisibleCol == -1 && col > this.firstVisibleCol) || (this.lastTotallyVisibleCol > -1 && col > this.lastTotallyVisibleCol))
			{
				this.ScrollToColumn(col);
				if (num2 == this.firstVisibleCol && num3 == this.negOffset && num4 == this.lastTotallyVisibleCol)
				{
					break;
				}
				num2 = this.firstVisibleCol;
				num3 = this.negOffset;
				num4 = this.lastTotallyVisibleCol;
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Rectangle" /> that specifies the four corners of the selected cell.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that defines the current cell's corners.</returns>
		// Token: 0x06001592 RID: 5522 RVA: 0x0004AFB0 File Offset: 0x000491B0
		public Rectangle GetCurrentCellBounds()
		{
			DataGridCell currentCell = this.CurrentCell;
			return this.GetCellBounds(currentCell.RowNumber, currentCell.ColumnNumber);
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> of the cell specified by row and column number.</summary>
		/// <param name="row">The number of the cell's row.</param>
		/// <param name="col">The number of the cell's column.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that defines the current cell's corners.</returns>
		// Token: 0x06001593 RID: 5523 RVA: 0x0004AFD8 File Offset: 0x000491D8
		public Rectangle GetCellBounds(int row, int col)
		{
			DataGridRow[] array = this.DataGridRows;
			Rectangle cellBounds = array[row].GetCellBounds(col);
			cellBounds.Y += this.GetRowTop(row);
			cellBounds.X += this.layout.Data.X - this.negOffset;
			cellBounds.X = this.MirrorRectangle(cellBounds, this.layout.Data, this.isRightToLeft());
			return cellBounds;
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Rectangle" /> of the cell specified by <see cref="T:System.Windows.Forms.DataGridCell" />.</summary>
		/// <param name="dgc">The <see cref="T:System.Windows.Forms.DataGridCell" /> to look up.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that defines the current cell's corners.</returns>
		// Token: 0x06001594 RID: 5524 RVA: 0x0004B050 File Offset: 0x00049250
		public Rectangle GetCellBounds(DataGridCell dgc)
		{
			return this.GetCellBounds(dgc.RowNumber, dgc.ColumnNumber);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0004B068 File Offset: 0x00049268
		internal Rectangle GetRowBounds(DataGridRow row)
		{
			return new Rectangle
			{
				Y = this.GetRowTop(row.RowNumber),
				X = this.layout.Data.X,
				Height = row.Height,
				Width = this.layout.Data.Width
			};
		}

		/// <summary>Gets information, such as row and column number of a clicked point on the grid, using the x and y coordinate passed to the method.</summary>
		/// <param name="x">The horizontal position of the coordinate.</param>
		/// <param name="y">The vertical position of the coordinate.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGrid.HitTestInfo" /> that contains information about the clicked part of the grid.</returns>
		// Token: 0x06001596 RID: 5526 RVA: 0x0004B0CC File Offset: 0x000492CC
		public DataGrid.HitTestInfo HitTest(int x, int y)
		{
			int y2 = this.layout.Data.Y;
			DataGrid.HitTestInfo hitTestInfo = new DataGrid.HitTestInfo();
			if (this.layout.CaptionVisible && this.layout.Caption.Contains(x, y))
			{
				hitTestInfo.type = DataGrid.HitTestType.Caption;
				return hitTestInfo;
			}
			if (this.layout.ParentRowsVisible && this.layout.ParentRows.Contains(x, y))
			{
				hitTestInfo.type = DataGrid.HitTestType.ParentRows;
				return hitTestInfo;
			}
			if (!this.layout.Inside.Contains(x, y))
			{
				return hitTestInfo;
			}
			if (this.layout.TopLeftHeader.Contains(x, y))
			{
				return hitTestInfo;
			}
			if (this.layout.ColumnHeaders.Contains(x, y))
			{
				hitTestInfo.type = DataGrid.HitTestType.ColumnHeader;
				hitTestInfo.col = this.GetColFromX(x);
				if (hitTestInfo.col < 0)
				{
					return DataGrid.HitTestInfo.Nowhere;
				}
				int colBeg = this.GetColBeg(hitTestInfo.col + 1);
				bool flag = this.isRightToLeft();
				if ((flag && x - colBeg < 8) || (!flag && colBeg - x < 8))
				{
					hitTestInfo.type = DataGrid.HitTestType.ColumnResize;
				}
				if (!this.allowColumnResize)
				{
					return DataGrid.HitTestInfo.Nowhere;
				}
				return hitTestInfo;
			}
			else if (this.layout.RowHeaders.Contains(x, y))
			{
				hitTestInfo.type = DataGrid.HitTestType.RowHeader;
				hitTestInfo.row = this.GetRowFromY(y);
				if (hitTestInfo.row < 0)
				{
					return DataGrid.HitTestInfo.Nowhere;
				}
				DataGridRow[] array = this.DataGridRows;
				int num = this.GetRowTop(hitTestInfo.row) + array[hitTestInfo.row].Height;
				if (num - y - this.BorderWidth < 2 && !(array[hitTestInfo.row] is DataGridAddNewRow))
				{
					hitTestInfo.type = DataGrid.HitTestType.RowResize;
				}
				if (!this.allowRowResize)
				{
					return DataGrid.HitTestInfo.Nowhere;
				}
				return hitTestInfo;
			}
			else
			{
				if (!this.layout.Data.Contains(x, y))
				{
					return hitTestInfo;
				}
				hitTestInfo.type = DataGrid.HitTestType.Cell;
				hitTestInfo.col = this.GetColFromX(x);
				hitTestInfo.row = this.GetRowFromY(y);
				if (hitTestInfo.col < 0 || hitTestInfo.row < 0)
				{
					return DataGrid.HitTestInfo.Nowhere;
				}
				return hitTestInfo;
			}
		}

		/// <summary>Gets information, such as row and column number of a clicked point on the grid, about the grid using a specific <see cref="T:System.Drawing.Point" />.</summary>
		/// <param name="position">A <see cref="T:System.Drawing.Point" /> that represents single x,y coordinate.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGrid.HitTestInfo" /> that contains specific information about the grid.</returns>
		// Token: 0x06001597 RID: 5527 RVA: 0x0004B2CA File Offset: 0x000494CA
		public DataGrid.HitTestInfo HitTest(Point position)
		{
			return this.HitTest(position.X, position.Y);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0004B2E0 File Offset: 0x000494E0
		private void InitializeColumnWidths()
		{
			if (this.myGridTable == null)
			{
				return;
			}
			GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
			int count = gridColumnStyles.Count;
			int num = (this.myGridTable.IsDefault ? this.PreferredColumnWidth : this.myGridTable.PreferredColumnWidth);
			for (int i = 0; i < count; i++)
			{
				if (gridColumnStyles[i].width == -1)
				{
					gridColumnStyles[i].width = num;
				}
			}
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0004B352 File Offset: 0x00049552
		internal void InvalidateInside()
		{
			base.Invalidate(this.layout.Inside);
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x0004B365 File Offset: 0x00049565
		internal void InvalidateCaption()
		{
			if (this.layout.CaptionVisible)
			{
				base.Invalidate(this.layout.Caption);
			}
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0004B385 File Offset: 0x00049585
		internal void InvalidateCaptionRect(Rectangle r)
		{
			if (this.layout.CaptionVisible)
			{
				base.Invalidate(r);
			}
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0004B39C File Offset: 0x0004959C
		internal void InvalidateColumn(int column)
		{
			GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
			if (column < 0 || gridColumnStyles == null || gridColumnStyles.Count <= column)
			{
				return;
			}
			if (column < this.firstVisibleCol || column > this.firstVisibleCol + this.numVisibleCols - 1)
			{
				return;
			}
			Rectangle rectangle = default(Rectangle);
			rectangle.Height = this.layout.Data.Height;
			rectangle.Width = gridColumnStyles[column].Width;
			rectangle.Y = this.layout.Data.Y;
			int num = this.layout.Data.X - this.negOffset;
			int count = gridColumnStyles.Count;
			int num2 = this.firstVisibleCol;
			while (num2 < count && num2 != column)
			{
				num += gridColumnStyles[num2].Width;
				num2++;
			}
			rectangle.X = num;
			rectangle.X = this.MirrorRectangle(rectangle, this.layout.Data, this.isRightToLeft());
			base.Invalidate(rectangle);
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0004B4A2 File Offset: 0x000496A2
		internal void InvalidateParentRows()
		{
			if (this.layout.ParentRowsVisible)
			{
				base.Invalidate(this.layout.ParentRows);
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0004B4C4 File Offset: 0x000496C4
		internal void InvalidateParentRowsRect(Rectangle r)
		{
			Rectangle rectangle = this.layout.ParentRows;
			base.Invalidate(r);
			bool isEmpty = rectangle.IsEmpty;
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0004B4EC File Offset: 0x000496EC
		internal void InvalidateRow(int rowNumber)
		{
			Rectangle rowRect = this.GetRowRect(rowNumber);
			if (!rowRect.IsEmpty)
			{
				base.Invalidate(rowRect);
			}
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0004B514 File Offset: 0x00049714
		private void InvalidateRowHeader(int rowNumber)
		{
			if (rowNumber >= this.firstVisibleRow && rowNumber < this.firstVisibleRow + this.numVisibleRows)
			{
				if (!this.layout.RowHeadersVisible)
				{
					return;
				}
				base.Invalidate(new Rectangle
				{
					Y = this.GetRowTop(rowNumber),
					X = this.layout.RowHeaders.X,
					Width = this.layout.RowHeaders.Width,
					Height = this.DataGridRows[rowNumber].Height
				});
			}
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0004B5A8 File Offset: 0x000497A8
		internal void InvalidateRowRect(int rowNumber, Rectangle r)
		{
			Rectangle rowRect = this.GetRowRect(rowNumber);
			if (!rowRect.IsEmpty)
			{
				Rectangle rectangle = new Rectangle(rowRect.X + r.X, rowRect.Y + r.Y, r.Width, r.Height);
				if (this.vertScrollBar.Visible && this.isRightToLeft())
				{
					rectangle.X -= this.vertScrollBar.Width;
				}
				base.Invalidate(rectangle);
			}
		}

		/// <summary>Gets a value that indicates whether the node of a specified row is expanded or collapsed.</summary>
		/// <param name="rowNumber">The number of the row in question.</param>
		/// <returns>
		///   <see langword="true" /> if the node is expanded; otherwise, <see langword="false" />.</returns>
		// Token: 0x060015A2 RID: 5538 RVA: 0x0004B630 File Offset: 0x00049830
		public bool IsExpanded(int rowNumber)
		{
			if (rowNumber < 0 || rowNumber > this.DataGridRowsLength)
			{
				throw new ArgumentOutOfRangeException("rowNumber");
			}
			DataGridRow[] array = this.DataGridRows;
			DataGridRow dataGridRow = array[rowNumber];
			if (dataGridRow is DataGridRelationshipRow)
			{
				DataGridRelationshipRow dataGridRelationshipRow = (DataGridRelationshipRow)dataGridRow;
				return dataGridRelationshipRow.Expanded;
			}
			return false;
		}

		/// <summary>Gets a value indicating whether a specified row is selected.</summary>
		/// <param name="row">The number of the row you are interested in.</param>
		/// <returns>
		///   <see langword="true" /> if the row is selected; otherwise, <see langword="false" />.</returns>
		// Token: 0x060015A3 RID: 5539 RVA: 0x0004B678 File Offset: 0x00049878
		public bool IsSelected(int row)
		{
			DataGridRow[] array = this.DataGridRows;
			return array[row].Selected;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0004B694 File Offset: 0x00049894
		internal static bool IsTransparentColor(Color color)
		{
			return color.A < byte.MaxValue;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0004B6A4 File Offset: 0x000498A4
		private void LayoutScrollBars()
		{
			if (this.listManager == null || this.myGridTable == null)
			{
				this.horizScrollBar.Visible = false;
				this.vertScrollBar.Visible = false;
				return;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = this.isRightToLeft();
			int count = this.myGridTable.GridColumnStyles.Count;
			DataGridRow[] array = this.DataGridRows;
			int num = Math.Max(0, this.GetColumnWidthSum());
			if (num > this.layout.Data.Width && !flag)
			{
				int height = this.horizScrollBar.Height;
				DataGrid.LayoutData layoutData = this.layout;
				layoutData.Data.Height = layoutData.Data.Height - height;
				if (this.layout.RowHeadersVisible)
				{
					DataGrid.LayoutData layoutData2 = this.layout;
					layoutData2.RowHeaders.Height = layoutData2.RowHeaders.Height - height;
				}
				flag = true;
			}
			int num2 = this.firstVisibleRow;
			this.ComputeVisibleRows();
			if (this.numTotallyVisibleRows != this.DataGridRowsLength && !flag2)
			{
				int width = this.vertScrollBar.Width;
				DataGrid.LayoutData layoutData3 = this.layout;
				layoutData3.Data.Width = layoutData3.Data.Width - width;
				if (this.layout.ColumnHeadersVisible)
				{
					if (flag4)
					{
						DataGrid.LayoutData layoutData4 = this.layout;
						layoutData4.ColumnHeaders.X = layoutData4.ColumnHeaders.X + width;
					}
					DataGrid.LayoutData layoutData5 = this.layout;
					layoutData5.ColumnHeaders.Width = layoutData5.ColumnHeaders.Width - width;
				}
				flag2 = true;
			}
			this.firstVisibleCol = this.ComputeFirstVisibleColumn();
			this.ComputeVisibleColumns();
			if (flag2 && num > this.layout.Data.Width && !flag)
			{
				this.firstVisibleRow = num2;
				int height2 = this.horizScrollBar.Height;
				DataGrid.LayoutData layoutData6 = this.layout;
				layoutData6.Data.Height = layoutData6.Data.Height - height2;
				if (this.layout.RowHeadersVisible)
				{
					DataGrid.LayoutData layoutData7 = this.layout;
					layoutData7.RowHeaders.Height = layoutData7.RowHeaders.Height - height2;
				}
				flag = true;
				flag3 = true;
			}
			if (flag3)
			{
				this.ComputeVisibleRows();
				if (this.numTotallyVisibleRows != this.DataGridRowsLength && !flag2)
				{
					int width2 = this.vertScrollBar.Width;
					DataGrid.LayoutData layoutData8 = this.layout;
					layoutData8.Data.Width = layoutData8.Data.Width - width2;
					if (this.layout.ColumnHeadersVisible)
					{
						if (flag4)
						{
							DataGrid.LayoutData layoutData9 = this.layout;
							layoutData9.ColumnHeaders.X = layoutData9.ColumnHeaders.X + width2;
						}
						DataGrid.LayoutData layoutData10 = this.layout;
						layoutData10.ColumnHeaders.Width = layoutData10.ColumnHeaders.Width - width2;
					}
					flag2 = true;
				}
			}
			this.layout.ResizeBoxRect = default(Rectangle);
			if (flag2 && flag)
			{
				Rectangle data = this.layout.Data;
				this.layout.ResizeBoxRect = new Rectangle(flag4 ? data.X : data.Right, data.Bottom, this.vertScrollBar.Width, this.horizScrollBar.Height);
			}
			if (flag && count > 0)
			{
				int num3 = num - this.layout.Data.Width;
				this.horizScrollBar.Minimum = 0;
				this.horizScrollBar.Maximum = num;
				this.horizScrollBar.SmallChange = 1;
				this.horizScrollBar.LargeChange = Math.Max(num - num3, 0);
				this.horizScrollBar.Enabled = base.Enabled;
				this.horizScrollBar.RightToLeft = this.RightToLeft;
				this.horizScrollBar.Bounds = new Rectangle(flag4 ? (this.layout.Inside.X + this.layout.ResizeBoxRect.Width) : this.layout.Inside.X, this.layout.Data.Bottom, this.layout.Inside.Width - this.layout.ResizeBoxRect.Width, this.horizScrollBar.Height);
				this.horizScrollBar.Visible = true;
			}
			else
			{
				this.HorizontalOffset = 0;
				this.horizScrollBar.Visible = false;
			}
			if (flag2)
			{
				int num4 = this.layout.Data.Y;
				if (this.layout.ColumnHeadersVisible)
				{
					num4 = this.layout.ColumnHeaders.Y;
				}
				this.vertScrollBar.LargeChange = ((this.numTotallyVisibleRows != 0) ? this.numTotallyVisibleRows : 1);
				this.vertScrollBar.Bounds = new Rectangle(flag4 ? this.layout.Data.X : this.layout.Data.Right, num4, this.vertScrollBar.Width, this.layout.Data.Height + this.layout.ColumnHeaders.Height);
				this.vertScrollBar.Enabled = base.Enabled;
				this.vertScrollBar.Visible = true;
				if (flag4)
				{
					DataGrid.LayoutData layoutData11 = this.layout;
					layoutData11.Data.X = layoutData11.Data.X + this.vertScrollBar.Width;
					return;
				}
			}
			else
			{
				this.vertScrollBar.Visible = false;
			}
		}

		/// <summary>Navigates back to the table previously displayed in the grid.</summary>
		// Token: 0x060015A6 RID: 5542 RVA: 0x0004BB94 File Offset: 0x00049D94
		public void NavigateBack()
		{
			if (!this.CommitEdit() || this.parentRows.IsEmpty())
			{
				return;
			}
			if (this.gridState[1048576])
			{
				this.gridState[1048576] = false;
				try
				{
					this.listManager.CancelCurrentEdit();
					goto IL_4F;
				}
				catch
				{
					goto IL_4F;
				}
			}
			this.UpdateListManager();
			IL_4F:
			DataGridState dataGridState = this.parentRows.PopTop();
			this.ResetMouseState();
			dataGridState.PullState(this, false);
			if (this.parentRows.GetTopParent() == null)
			{
				this.originalState = null;
			}
			DataGridRow[] array = this.DataGridRows;
			if ((this.ReadOnly || !this.policy.AllowAdd) == array[this.DataGridRowsLength - 1] is DataGridAddNewRow)
			{
				int num = ((this.ReadOnly || !this.policy.AllowAdd) ? (this.DataGridRowsLength - 1) : (this.DataGridRowsLength + 1));
				DataGridRow[] array2 = new DataGridRow[num];
				for (int i = 0; i < Math.Min(num, this.DataGridRowsLength); i++)
				{
					array2[i] = this.DataGridRows[i];
				}
				if (!this.ReadOnly && this.policy.AllowAdd)
				{
					array2[num - 1] = new DataGridAddNewRow(this, this.myGridTable, num - 1);
				}
				this.SetDataGridRows(array2, num);
			}
			array = this.DataGridRows;
			if (array != null && array.Length != 0)
			{
				DataGridTableStyle dataGridTableStyle = array[0].DataGridTableStyle;
				if (dataGridTableStyle != this.myGridTable)
				{
					for (int j = 0; j < array.Length; j++)
					{
						array[j].DataGridTableStyle = this.myGridTable;
					}
				}
			}
			if (this.myGridTable.GridColumnStyles.Count > 0 && this.myGridTable.GridColumnStyles[0].Width == -1)
			{
				this.InitializeColumnWidths();
			}
			this.currentRow = ((this.ListManager.Position == -1) ? 0 : this.ListManager.Position);
			if (!this.AllowNavigation)
			{
				this.RecreateDataGridRows();
			}
			this.caption.BackButtonActive = this.parentRows.GetTopParent() != null && this.AllowNavigation;
			this.caption.BackButtonVisible = this.caption.BackButtonActive;
			this.caption.DownButtonActive = this.parentRows.GetTopParent() != null;
			base.PerformLayout();
			base.Invalidate();
			if (this.vertScrollBar.Visible)
			{
				this.vertScrollBar.Value = this.firstVisibleRow;
			}
			if (this.horizScrollBar.Visible)
			{
				this.horizScrollBar.Value = this.HorizontalOffset + this.negOffset;
			}
			this.Edit();
			this.OnNavigate(new NavigateEventArgs(false));
		}

		/// <summary>Navigates to the table specified by row and relation name.</summary>
		/// <param name="rowNumber">The number of the row to navigate to.</param>
		/// <param name="relationName">The name of the child relation to navigate to.</param>
		// Token: 0x060015A7 RID: 5543 RVA: 0x0004BE44 File Offset: 0x0004A044
		public void NavigateTo(int rowNumber, string relationName)
		{
			if (!this.AllowNavigation)
			{
				return;
			}
			DataGridRow[] array = this.DataGridRows;
			if (rowNumber < 0 || rowNumber > this.DataGridRowsLength - (this.policy.AllowAdd ? 2 : 1))
			{
				throw new ArgumentOutOfRangeException("rowNumber");
			}
			this.EnsureBound();
			DataGridRow dataGridRow = array[rowNumber];
			this.NavigateTo(relationName, dataGridRow, false);
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0004BEA0 File Offset: 0x0004A0A0
		internal void NavigateTo(string relationName, DataGridRow source, bool fromRow)
		{
			if (!this.AllowNavigation)
			{
				return;
			}
			if (!this.CommitEdit())
			{
				return;
			}
			DataGridState dataGridState;
			try
			{
				dataGridState = this.CreateChildState(relationName, source);
			}
			catch
			{
				this.NavigateBack();
				return;
			}
			try
			{
				this.listManager.EndCurrentEdit();
			}
			catch
			{
				return;
			}
			DataGridState dataGridState2 = new DataGridState(this);
			dataGridState2.LinkingRow = source;
			if (source.RowNumber != this.CurrentRow)
			{
				this.listManager.Position = source.RowNumber;
			}
			if (this.parentRows.GetTopParent() == null)
			{
				this.originalState = dataGridState2;
			}
			this.parentRows.AddParent(dataGridState2);
			this.NavigateTo(dataGridState);
			this.OnNavigate(new NavigateEventArgs(true));
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0004BF64 File Offset: 0x0004A164
		private void NavigateTo(DataGridState childState)
		{
			this.EndEdit();
			this.gridState[16384] = false;
			this.ResetMouseState();
			childState.PullState(this, true);
			if (this.listManager.Position != this.currentRow)
			{
				this.currentRow = ((this.listManager.Position == -1) ? 0 : this.listManager.Position);
			}
			if (this.parentRows.GetTopParent() != null)
			{
				this.caption.BackButtonActive = this.AllowNavigation;
				this.caption.BackButtonVisible = this.caption.BackButtonActive;
				this.caption.DownButtonActive = true;
			}
			this.HorizontalOffset = 0;
			base.PerformLayout();
			base.Invalidate();
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0004C020 File Offset: 0x0004A220
		private Point NormalizeToRow(int x, int y, int row)
		{
			Point point = new Point(0, this.layout.Data.Y);
			DataGridRow[] array = this.DataGridRows;
			for (int i = this.firstVisibleRow; i < row; i++)
			{
				point.Y += array[i].Height;
			}
			return new Point(x, y - point.Y);
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0004C084 File Offset: 0x0004A284
		internal void OnColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)sender;
			if (dataGridTableStyle.Equals(this.myGridTable))
			{
				if (!this.myGridTable.IsDefault && (e.Action != CollectionChangeAction.Refresh || e.Element == null))
				{
					this.PairTableStylesAndGridColumns(this.listManager, this.myGridTable, false);
				}
				base.Invalidate();
				base.PerformLayout();
			}
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0004C0E4 File Offset: 0x0004A2E4
		private void PaintColumnHeaders(Graphics g)
		{
			bool flag = this.isRightToLeft();
			Rectangle columnHeaders = this.layout.ColumnHeaders;
			if (!flag)
			{
				columnHeaders.X -= this.negOffset;
			}
			columnHeaders.Width += this.negOffset;
			int num = this.PaintColumnHeaderText(g, columnHeaders);
			if (flag)
			{
				columnHeaders.X = columnHeaders.Right - num;
			}
			columnHeaders.Width = num;
			if (!this.FlatMode)
			{
				ControlPaint.DrawBorder3D(g, columnHeaders, Border3DStyle.RaisedInner);
				columnHeaders.Inflate(-1, -1);
				int num2 = columnHeaders.Width;
				columnHeaders.Width = num2 - 1;
				num2 = columnHeaders.Height;
				columnHeaders.Height = num2 - 1;
				g.DrawRectangle(SystemPens.Control, columnHeaders);
			}
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0004C19C File Offset: 0x0004A39C
		private int PaintColumnHeaderText(Graphics g, Rectangle boundingRect)
		{
			int num = 0;
			Rectangle rectangle = boundingRect;
			GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
			bool flag = this.isRightToLeft();
			int count = gridColumnStyles.Count;
			PropertyDescriptor sortProperty = this.ListManager.GetSortProperty();
			for (int i = this.firstVisibleCol; i < count; i++)
			{
				if (gridColumnStyles[i].PropertyDescriptor != null)
				{
					if (num > boundingRect.Width)
					{
						break;
					}
					bool flag2 = sortProperty != null && sortProperty.Equals(gridColumnStyles[i].PropertyDescriptor);
					TriangleDirection triangleDirection = TriangleDirection.Up;
					if (flag2)
					{
						ListSortDirection sortDirection = this.ListManager.GetSortDirection();
						if (sortDirection == ListSortDirection.Descending)
						{
							triangleDirection = TriangleDirection.Down;
						}
					}
					if (flag)
					{
						rectangle.Width = gridColumnStyles[i].Width - (flag2 ? rectangle.Height : 0);
						rectangle.X = boundingRect.Right - num - rectangle.Width;
					}
					else
					{
						rectangle.X = boundingRect.X + num;
						rectangle.Width = gridColumnStyles[i].Width - (flag2 ? rectangle.Height : 0);
					}
					Brush brush;
					if (this.myGridTable.IsDefault)
					{
						brush = this.HeaderBackBrush;
					}
					else
					{
						brush = this.myGridTable.HeaderBackBrush;
					}
					g.FillRectangle(brush, rectangle);
					if (flag)
					{
						rectangle.X -= 2;
						rectangle.Y += 2;
					}
					else
					{
						rectangle.X += 2;
						rectangle.Y += 2;
					}
					StringFormat stringFormat = new StringFormat();
					HorizontalAlignment alignment = gridColumnStyles[i].Alignment;
					stringFormat.Alignment = ((alignment == HorizontalAlignment.Right) ? StringAlignment.Far : ((alignment == HorizontalAlignment.Center) ? StringAlignment.Center : StringAlignment.Near));
					stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
					if (flag)
					{
						stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
						stringFormat.Alignment = StringAlignment.Near;
					}
					g.DrawString(gridColumnStyles[i].HeaderText, this.myGridTable.IsDefault ? this.HeaderFont : this.myGridTable.HeaderFont, this.myGridTable.IsDefault ? this.HeaderForeBrush : this.myGridTable.HeaderForeBrush, rectangle, stringFormat);
					stringFormat.Dispose();
					if (flag)
					{
						rectangle.X += 2;
						rectangle.Y -= 2;
					}
					else
					{
						rectangle.X -= 2;
						rectangle.Y -= 2;
					}
					if (flag2)
					{
						Rectangle rectangle2 = new Rectangle(flag ? (rectangle.X - rectangle.Height) : rectangle.Right, rectangle.Y, rectangle.Height, rectangle.Height);
						g.FillRectangle(brush, rectangle2);
						int num2 = Math.Max(0, (rectangle.Height - 5) / 2);
						rectangle2.Inflate(-num2, -num2);
						Pen pen = new Pen(this.BackgroundBrush);
						Pen pen2 = new Pen(this.myGridTable.BackBrush);
						Triangle.Paint(g, rectangle2, triangleDirection, brush, pen, pen2, pen, true);
						pen.Dispose();
						pen2.Dispose();
					}
					int num3 = rectangle.Width + (flag2 ? rectangle.Height : 0);
					if (!this.FlatMode)
					{
						if (flag && flag2)
						{
							rectangle.X -= rectangle.Height;
						}
						rectangle.Width = num3;
						ControlPaint.DrawBorder3D(g, rectangle, Border3DStyle.RaisedInner);
					}
					num += num3;
				}
			}
			if (num < boundingRect.Width)
			{
				rectangle = boundingRect;
				if (!flag)
				{
					rectangle.X += num;
				}
				rectangle.Width -= num;
				g.FillRectangle(this.backgroundBrush, rectangle);
			}
			return num;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0004C560 File Offset: 0x0004A760
		private void PaintBorder(Graphics g, Rectangle bounds)
		{
			if (this.BorderStyle == BorderStyle.None)
			{
				return;
			}
			if (this.BorderStyle == BorderStyle.Fixed3D)
			{
				Border3DStyle border3DStyle = Border3DStyle.Sunken;
				ControlPaint.DrawBorder3D(g, bounds, border3DStyle);
				return;
			}
			if (this.BorderStyle == BorderStyle.FixedSingle)
			{
				Brush brush;
				if (this.myGridTable.IsDefault)
				{
					brush = this.HeaderForeBrush;
				}
				else
				{
					brush = this.myGridTable.HeaderForeBrush;
				}
				g.FillRectangle(brush, bounds.X, bounds.Y, bounds.Width + 2, 2);
				g.FillRectangle(brush, bounds.Right - 2, bounds.Y, 2, bounds.Height + 2);
				g.FillRectangle(brush, bounds.X, bounds.Bottom - 2, bounds.Width + 2, 2);
				g.FillRectangle(brush, bounds.X, bounds.Y, 2, bounds.Height + 2);
				return;
			}
			Pen windowFrame = SystemPens.WindowFrame;
			int num = bounds.Width;
			bounds.Width = num - 1;
			num = bounds.Height;
			bounds.Height = num - 1;
			g.DrawRectangle(windowFrame, bounds);
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0004C66C File Offset: 0x0004A86C
		private void PaintGrid(Graphics g, Rectangle gridBounds)
		{
			Rectangle rectangle = gridBounds;
			if (this.listManager != null)
			{
				if (this.layout.ColumnHeadersVisible)
				{
					Region clip = g.Clip;
					g.SetClip(this.layout.ColumnHeaders);
					this.PaintColumnHeaders(g);
					g.Clip = clip;
					clip.Dispose();
					int height = this.layout.ColumnHeaders.Height;
					rectangle.Y += height;
					rectangle.Height -= height;
				}
				if (this.layout.TopLeftHeader.Width > 0)
				{
					if (this.myGridTable.IsDefault)
					{
						g.FillRectangle(this.HeaderBackBrush, this.layout.TopLeftHeader);
					}
					else
					{
						g.FillRectangle(this.myGridTable.HeaderBackBrush, this.layout.TopLeftHeader);
					}
					if (!this.FlatMode)
					{
						ControlPaint.DrawBorder3D(g, this.layout.TopLeftHeader, Border3DStyle.RaisedInner);
					}
				}
				this.PaintRows(g, ref rectangle);
			}
			if (rectangle.Height > 0)
			{
				g.FillRectangle(this.backgroundBrush, rectangle);
			}
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0004C780 File Offset: 0x0004A980
		private void DeleteDataGridRows(int deletedRows)
		{
			if (deletedRows == 0)
			{
				return;
			}
			int num = this.DataGridRowsLength;
			int num2 = num - deletedRows + (this.gridState[1048576] ? 1 : 0);
			DataGridRow[] array = new DataGridRow[num2];
			DataGridRow[] array2 = this.DataGridRows;
			int num3 = 0;
			for (int i = 0; i < num; i++)
			{
				if (array2[i].Selected)
				{
					num3++;
				}
				else
				{
					array[i - num3] = array2[i];
					array[i - num3].number = i - num3;
				}
			}
			if (this.gridState[1048576])
			{
				array[num - num3] = new DataGridAddNewRow(this, this.myGridTable, num - num3);
				this.gridState[1048576] = false;
			}
			this.SetDataGridRows(array, num2);
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0004C844 File Offset: 0x0004AA44
		private void PaintRows(Graphics g, ref Rectangle boundingRect)
		{
			int num = 0;
			bool flag = this.isRightToLeft();
			Rectangle rectangle = boundingRect;
			Rectangle rectangle2 = Rectangle.Empty;
			bool rowHeadersVisible = this.layout.RowHeadersVisible;
			Rectangle rectangle3 = Rectangle.Empty;
			int num2 = this.DataGridRowsLength;
			DataGridRow[] array = this.DataGridRows;
			int num3 = this.myGridTable.GridColumnStyles.Count - this.firstVisibleCol;
			int num4 = this.firstVisibleRow;
			while (num4 < num2 && num <= boundingRect.Height)
			{
				rectangle = boundingRect;
				rectangle.Height = array[num4].Height;
				rectangle.Y = boundingRect.Y + num;
				if (rowHeadersVisible)
				{
					rectangle3 = rectangle;
					rectangle3.Width = this.layout.RowHeaders.Width;
					if (flag)
					{
						rectangle3.X = rectangle.Right - rectangle3.Width;
					}
					if (g.IsVisible(rectangle3))
					{
						array[num4].PaintHeader(g, rectangle3, flag, this.gridState[32768]);
						g.ExcludeClip(rectangle3);
					}
					if (!flag)
					{
						rectangle.X += rectangle3.Width;
					}
					rectangle.Width -= rectangle3.Width;
				}
				if (g.IsVisible(rectangle))
				{
					rectangle2 = rectangle;
					if (!flag)
					{
						rectangle2.X -= this.negOffset;
					}
					rectangle2.Width += this.negOffset;
					array[num4].Paint(g, rectangle2, rectangle, this.firstVisibleCol, num3, flag);
				}
				num += rectangle.Height;
				num4++;
			}
			boundingRect.Y += num;
			boundingRect.Height -= num;
		}

		/// <summary>Gets or sets a value that indicates whether a key should be processed further.</summary>
		/// <param name="keyData">A <see cref="T:System.Windows.Forms.Keys" /> that contains data about the pressed key.</param>
		/// <returns>
		///   <see langword="true" />, the key should be processed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060015B2 RID: 5554 RVA: 0x0004CA00 File Offset: 0x0004AC00
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			DataGridRow[] array = this.DataGridRows;
			if (this.listManager != null && this.DataGridRowsLength > 0 && array[this.currentRow].OnKeyPress(keyData))
			{
				return true;
			}
			Keys keys = keyData & Keys.KeyCode;
			if (keys <= Keys.A)
			{
				if (keys <= Keys.Return)
				{
					if (keys != Keys.Tab && keys != Keys.Return)
					{
						goto IL_243;
					}
				}
				else
				{
					switch (keys)
					{
					case Keys.Escape:
					case Keys.Space:
					case Keys.Prior:
					case Keys.Next:
					case Keys.Left:
					case Keys.Up:
					case Keys.Right:
					case Keys.Down:
						break;
					case Keys.IMEConvert:
					case Keys.IMENonconvert:
					case Keys.IMEAccept:
					case Keys.IMEModeChange:
					case Keys.End:
					case Keys.Home:
						goto IL_243;
					default:
						if (keys != Keys.Delete && keys != Keys.A)
						{
							goto IL_243;
						}
						break;
					}
				}
			}
			else if (keys <= Keys.Add)
			{
				if (keys != Keys.C)
				{
					if (keys != Keys.Add)
					{
						goto IL_243;
					}
				}
				else
				{
					if ((keyData & Keys.Control) == Keys.None || (keyData & Keys.Alt) != Keys.None || !this.Bound)
					{
						goto IL_243;
					}
					if (this.numSelectedRows != 0)
					{
						int num = 0;
						string text = "";
						for (int i = 0; i < this.DataGridRowsLength; i++)
						{
							if (array[i].Selected)
							{
								GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
								int count = gridColumnStyles.Count;
								for (int j = 0; j < count; j++)
								{
									DataGridColumnStyle dataGridColumnStyle = gridColumnStyles[j];
									text += dataGridColumnStyle.GetDisplayText(dataGridColumnStyle.GetColumnValueAtRow(this.ListManager, i));
									if (j < count - 1)
									{
										text += this.GetOutputTextDelimiter();
									}
								}
								if (num < this.numSelectedRows - 1)
								{
									text += "\r\n";
								}
								num++;
							}
						}
						Clipboard.SetDataObject(text);
						return true;
					}
					if (this.currentRow >= this.ListManager.Count)
					{
						goto IL_243;
					}
					GridColumnStylesCollection gridColumnStyles2 = this.myGridTable.GridColumnStyles;
					if (this.currentCol >= 0 && this.currentCol < gridColumnStyles2.Count)
					{
						DataGridColumnStyle dataGridColumnStyle2 = gridColumnStyles2[this.currentCol];
						string displayText = dataGridColumnStyle2.GetDisplayText(dataGridColumnStyle2.GetColumnValueAtRow(this.ListManager, this.currentRow));
						Clipboard.SetDataObject(displayText);
						return true;
					}
					goto IL_243;
				}
			}
			else if (keys != Keys.Subtract && keys != Keys.Oemplus && keys != Keys.OemMinus)
			{
				goto IL_243;
			}
			KeyEventArgs keyEventArgs = new KeyEventArgs(keyData);
			if (this.ProcessGridKey(keyEventArgs))
			{
				return true;
			}
			IL_243:
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0004CC58 File Offset: 0x0004AE58
		private void DeleteRows(DataGridRow[] localGridRows)
		{
			int num = 0;
			int num2 = ((this.listManager == null) ? 0 : this.listManager.Count);
			if (base.Visible)
			{
				base.BeginUpdateInternal();
			}
			try
			{
				if (this.ListManager != null)
				{
					for (int i = 0; i < this.DataGridRowsLength; i++)
					{
						if (localGridRows[i].Selected)
						{
							if (localGridRows[i] is DataGridAddNewRow)
							{
								localGridRows[i].Selected = false;
							}
							else
							{
								this.ListManager.RemoveAt(i - num);
								num++;
							}
						}
					}
				}
			}
			catch
			{
				this.RecreateDataGridRows();
				this.gridState[1024] = false;
				if (base.Visible)
				{
					base.EndUpdateInternal();
				}
				throw;
			}
			if (this.listManager != null && num2 == this.listManager.Count + num)
			{
				this.DeleteDataGridRows(num);
			}
			else
			{
				this.RecreateDataGridRows();
			}
			this.gridState[1024] = false;
			if (base.Visible)
			{
				base.EndUpdateInternal();
			}
			if (this.listManager != null && num2 != this.listManager.Count + num)
			{
				base.Invalidate();
			}
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0004CD78 File Offset: 0x0004AF78
		private int MoveLeftRight(GridColumnStylesCollection cols, int startCol, bool goRight)
		{
			int i;
			if (goRight)
			{
				for (i = startCol + 1; i < cols.Count; i++)
				{
					if (cols[i].PropertyDescriptor != null)
					{
						return i;
					}
				}
				return i;
			}
			for (i = startCol - 1; i >= 0; i--)
			{
				if (cols[i].PropertyDescriptor != null)
				{
					return i;
				}
			}
			return i;
		}

		/// <summary>Processes keys for grid navigation.</summary>
		/// <param name="ke">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains data about the key up or key down event.</param>
		/// <returns>
		///   <see langword="true" />, if the key was processed; otherwise <see langword="false" />.</returns>
		// Token: 0x060015B5 RID: 5557 RVA: 0x0004CDCC File Offset: 0x0004AFCC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected bool ProcessGridKey(KeyEventArgs ke)
		{
			if (this.listManager == null || this.myGridTable == null)
			{
				return false;
			}
			DataGridRow[] array = this.DataGridRows;
			KeyEventArgs keyEventArgs = ke;
			if (this.isRightToLeft())
			{
				Keys keyCode = ke.KeyCode;
				if (keyCode != Keys.Left)
				{
					if (keyCode == Keys.Right)
					{
						keyEventArgs = new KeyEventArgs(Keys.Left | ke.Modifiers);
					}
				}
				else
				{
					keyEventArgs = new KeyEventArgs(Keys.Right | ke.Modifiers);
				}
			}
			GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
			int num = 0;
			int num2 = gridColumnStyles.Count;
			for (int i = 0; i < gridColumnStyles.Count; i++)
			{
				if (gridColumnStyles[i].PropertyDescriptor != null)
				{
					num = i;
					break;
				}
			}
			for (int j = gridColumnStyles.Count - 1; j >= 0; j--)
			{
				if (gridColumnStyles[j].PropertyDescriptor != null)
				{
					num2 = j;
					break;
				}
			}
			Keys keyCode2 = keyEventArgs.KeyCode;
			if (keyCode2 <= Keys.A)
			{
				if (keyCode2 <= Keys.Return)
				{
					if (keyCode2 == Keys.Tab)
					{
						return this.ProcessTabKey(keyEventArgs.KeyData);
					}
					if (keyCode2 == Keys.Return)
					{
						this.gridState[524288] = false;
						this.ResetSelection();
						if (!this.gridState[32768])
						{
							return false;
						}
						if ((keyEventArgs.Modifiers & Keys.Control) != Keys.None && !keyEventArgs.Alt)
						{
							this.EndEdit();
							this.HandleEndCurrentEdit();
							this.Edit();
						}
						else
						{
							this.CurrentRow = this.currentRow + 1;
						}
					}
				}
				else
				{
					switch (keyCode2)
					{
					case Keys.Escape:
						this.gridState[524288] = false;
						this.ResetSelection();
						if (!this.gridState[32768])
						{
							this.CancelEditing();
							this.Edit();
							return false;
						}
						this.AbortEdit();
						if (this.layout.RowHeadersVisible && this.currentRow > -1)
						{
							Rectangle rowRect = this.GetRowRect(this.currentRow);
							rowRect.Width = this.layout.RowHeaders.Width;
							base.Invalidate(rowRect);
						}
						this.Edit();
						break;
					case Keys.IMEConvert:
					case Keys.IMENonconvert:
					case Keys.IMEAccept:
					case Keys.IMEModeChange:
					case Keys.Select:
					case Keys.Print:
					case Keys.Execute:
					case Keys.Snapshot:
					case Keys.Insert:
						break;
					case Keys.Space:
						this.gridState[524288] = false;
						if (this.dataGridRowsLength == 0)
						{
							return true;
						}
						if (keyEventArgs.Shift)
						{
							this.ResetSelection();
							this.EndEdit();
							DataGridRow[] array2 = this.DataGridRows;
							array2[this.currentRow].Selected = true;
							this.numSelectedRows = 1;
							return true;
						}
						return false;
					case Keys.Prior:
						if (this.dataGridRowsLength == 0)
						{
							return true;
						}
						this.gridState[524288] = false;
						if (keyEventArgs.Shift)
						{
							int num3 = this.currentRow;
							this.CurrentRow = Math.Max(0, this.CurrentRow - this.numTotallyVisibleRows);
							DataGridRow[] array3 = this.DataGridRows;
							for (int k = num3; k >= this.currentRow; k--)
							{
								if (!array3[k].Selected)
								{
									array3[k].Selected = true;
									this.numSelectedRows++;
								}
							}
							this.EndEdit();
						}
						else if (keyEventArgs.Control && !keyEventArgs.Alt)
						{
							this.ParentRowsVisible = false;
						}
						else
						{
							this.ResetSelection();
							this.CurrentRow = Math.Max(0, this.CurrentRow - this.numTotallyVisibleRows);
						}
						break;
					case Keys.Next:
						this.gridState[524288] = false;
						if (this.dataGridRowsLength == 0)
						{
							return true;
						}
						if (keyEventArgs.Shift)
						{
							int num4 = this.currentRow;
							this.CurrentRow = Math.Min(this.DataGridRowsLength - (this.policy.AllowAdd ? 2 : 1), this.currentRow + this.numTotallyVisibleRows);
							DataGridRow[] array4 = this.DataGridRows;
							for (int l = num4; l <= this.currentRow; l++)
							{
								if (!array4[l].Selected)
								{
									array4[l].Selected = true;
									this.numSelectedRows++;
								}
							}
							this.EndEdit();
						}
						else if (keyEventArgs.Control && !keyEventArgs.Alt)
						{
							this.ParentRowsVisible = true;
						}
						else
						{
							this.ResetSelection();
							this.CurrentRow = Math.Min(this.DataGridRowsLength - (this.policy.AllowAdd ? 2 : 1), this.CurrentRow + this.numTotallyVisibleRows);
						}
						break;
					case Keys.End:
						this.gridState[524288] = false;
						if (this.dataGridRowsLength == 0)
						{
							return true;
						}
						this.ResetSelection();
						this.CurrentColumn = num2;
						if (keyEventArgs.Control && !keyEventArgs.Alt)
						{
							int num5 = this.currentRow;
							this.CurrentRow = Math.Max(0, this.DataGridRowsLength - (this.policy.AllowAdd ? 2 : 1));
							if (keyEventArgs.Shift)
							{
								DataGridRow[] array5 = this.DataGridRows;
								for (int m = num5; m <= this.currentRow; m++)
								{
									array5[m].Selected = true;
								}
								this.numSelectedRows = this.currentRow - num5 + 1;
								this.EndEdit();
							}
							return true;
						}
						break;
					case Keys.Home:
						this.gridState[524288] = false;
						if (this.dataGridRowsLength == 0)
						{
							return true;
						}
						this.ResetSelection();
						this.CurrentColumn = 0;
						if (keyEventArgs.Control && !keyEventArgs.Alt)
						{
							int num6 = this.currentRow;
							this.CurrentRow = 0;
							if (keyEventArgs.Shift)
							{
								DataGridRow[] array6 = this.DataGridRows;
								for (int n = 0; n <= num6; n++)
								{
									array6[n].Selected = true;
									this.numSelectedRows++;
								}
								this.EndEdit();
							}
							return true;
						}
						break;
					case Keys.Left:
						this.gridState[524288] = false;
						this.ResetSelection();
						if ((keyEventArgs.Modifiers & Keys.Modifiers) == Keys.Alt)
						{
							if (this.Caption.BackButtonVisible)
							{
								this.NavigateBack();
							}
							return true;
						}
						if ((keyEventArgs.Modifiers & Keys.Control) == Keys.Control)
						{
							this.CurrentColumn = num;
						}
						else if (this.currentCol == num && this.currentRow != 0)
						{
							this.CurrentRow--;
							int num7 = this.MoveLeftRight(this.myGridTable.GridColumnStyles, this.myGridTable.GridColumnStyles.Count, false);
							this.CurrentColumn = num7;
						}
						else
						{
							int num8 = this.MoveLeftRight(this.myGridTable.GridColumnStyles, this.currentCol, false);
							if (num8 == -1)
							{
								if (this.currentRow == 0)
								{
									return true;
								}
								this.CurrentRow--;
								this.CurrentColumn = num2;
							}
							else
							{
								this.CurrentColumn = num8;
							}
						}
						break;
					case Keys.Up:
						this.gridState[524288] = false;
						if (this.dataGridRowsLength == 0)
						{
							return true;
						}
						if (keyEventArgs.Control && !keyEventArgs.Alt)
						{
							if (keyEventArgs.Shift)
							{
								DataGridRow[] array7 = this.DataGridRows;
								int num9 = this.currentRow;
								this.CurrentRow = 0;
								this.ResetSelection();
								for (int num10 = 0; num10 <= num9; num10++)
								{
									array7[num10].Selected = true;
								}
								this.numSelectedRows = num9 + 1;
								this.EndEdit();
								return true;
							}
							this.ResetSelection();
							this.CurrentRow = 0;
							return true;
						}
						else
						{
							if (keyEventArgs.Shift)
							{
								DataGridRow[] array8 = this.DataGridRows;
								if (array8[this.currentRow].Selected)
								{
									if (this.currentRow >= 1)
									{
										if (array8[this.currentRow - 1].Selected)
										{
											if (this.currentRow >= this.DataGridRowsLength - 1 || !array8[this.currentRow + 1].Selected)
											{
												this.numSelectedRows--;
												array8[this.currentRow].Selected = false;
											}
										}
										else
										{
											this.numSelectedRows += (array8[this.currentRow - 1].Selected ? 0 : 1);
											array8[this.currentRow - 1].Selected = true;
										}
										int num11 = this.CurrentRow;
										this.CurrentRow = num11 - 1;
									}
								}
								else
								{
									this.numSelectedRows++;
									array8[this.currentRow].Selected = true;
									if (this.currentRow >= 1)
									{
										this.numSelectedRows += (array8[this.currentRow - 1].Selected ? 0 : 1);
										array8[this.currentRow - 1].Selected = true;
										int num11 = this.CurrentRow;
										this.CurrentRow = num11 - 1;
									}
								}
								this.EndEdit();
								return true;
							}
							if (keyEventArgs.Alt)
							{
								this.SetRowExpansionState(-1, false);
								return true;
							}
							this.ResetSelection();
							this.CurrentRow--;
							this.Edit();
						}
						break;
					case Keys.Right:
						this.gridState[524288] = false;
						this.ResetSelection();
						if ((keyEventArgs.Modifiers & Keys.Control) == Keys.Control && !keyEventArgs.Alt)
						{
							this.CurrentColumn = num2;
						}
						else if (this.currentCol == num2 && this.currentRow != this.DataGridRowsLength - 1)
						{
							this.CurrentRow++;
							this.CurrentColumn = num;
						}
						else
						{
							int num12 = this.MoveLeftRight(this.myGridTable.GridColumnStyles, this.currentCol, true);
							if (num12 == gridColumnStyles.Count + 1)
							{
								this.CurrentColumn = num;
								int num11 = this.CurrentRow;
								this.CurrentRow = num11 + 1;
							}
							else
							{
								this.CurrentColumn = num12;
							}
						}
						break;
					case Keys.Down:
						this.gridState[524288] = false;
						if (this.dataGridRowsLength == 0)
						{
							return true;
						}
						if (keyEventArgs.Control && !keyEventArgs.Alt)
						{
							if (keyEventArgs.Shift)
							{
								int num13 = this.currentRow;
								this.CurrentRow = Math.Max(0, this.DataGridRowsLength - (this.policy.AllowAdd ? 2 : 1));
								DataGridRow[] array9 = this.DataGridRows;
								this.ResetSelection();
								for (int num14 = num13; num14 <= this.currentRow; num14++)
								{
									array9[num14].Selected = true;
								}
								this.numSelectedRows = this.currentRow - num13 + 1;
								this.EndEdit();
								return true;
							}
							this.ResetSelection();
							this.CurrentRow = Math.Max(0, this.DataGridRowsLength - (this.policy.AllowAdd ? 2 : 1));
							return true;
						}
						else
						{
							if (keyEventArgs.Shift)
							{
								DataGridRow[] array10 = this.DataGridRows;
								if (array10[this.currentRow].Selected)
								{
									if (this.currentRow < this.DataGridRowsLength - (this.policy.AllowAdd ? 1 : 0) - 1)
									{
										if (array10[this.currentRow + 1].Selected)
										{
											if (this.currentRow == 0 || !array10[this.currentRow - 1].Selected)
											{
												this.numSelectedRows--;
												array10[this.currentRow].Selected = false;
											}
										}
										else
										{
											this.numSelectedRows += (array10[this.currentRow + 1].Selected ? 0 : 1);
											array10[this.currentRow + 1].Selected = true;
										}
										int num11 = this.CurrentRow;
										this.CurrentRow = num11 + 1;
									}
								}
								else
								{
									this.numSelectedRows++;
									array10[this.currentRow].Selected = true;
									if (this.currentRow < this.DataGridRowsLength - (this.policy.AllowAdd ? 1 : 0) - 1)
									{
										int num11 = this.CurrentRow;
										this.CurrentRow = num11 + 1;
										this.numSelectedRows += (array10[this.currentRow].Selected ? 0 : 1);
										array10[this.currentRow].Selected = true;
									}
								}
								this.EndEdit();
								return true;
							}
							if (keyEventArgs.Alt)
							{
								this.SetRowExpansionState(-1, true);
								return true;
							}
							this.ResetSelection();
							this.Edit();
							this.CurrentRow++;
						}
						break;
					case Keys.Delete:
						this.gridState[524288] = false;
						if (!this.policy.AllowRemove || this.numSelectedRows <= 0)
						{
							return false;
						}
						this.gridState[1024] = true;
						this.DeleteRows(array);
						this.currentRow = ((this.listManager.Count == 0) ? 0 : this.listManager.Position);
						this.numSelectedRows = 0;
						break;
					default:
						if (keyCode2 == Keys.A)
						{
							this.gridState[524288] = false;
							if (keyEventArgs.Control && !keyEventArgs.Alt)
							{
								DataGridRow[] array11 = this.DataGridRows;
								for (int num15 = 0; num15 < this.DataGridRowsLength; num15++)
								{
									if (array11[num15] is DataGridRelationshipRow)
									{
										array11[num15].Selected = true;
									}
								}
								this.numSelectedRows = this.DataGridRowsLength - (this.policy.AllowAdd ? 1 : 0);
								this.EndEdit();
								return true;
							}
							return false;
						}
						break;
					}
				}
			}
			else
			{
				if (keyCode2 <= Keys.Subtract)
				{
					if (keyCode2 == Keys.Add)
					{
						goto IL_64A;
					}
					if (keyCode2 != Keys.Subtract)
					{
						return true;
					}
				}
				else
				{
					if (keyCode2 == Keys.F2)
					{
						this.gridState[524288] = false;
						this.ResetSelection();
						this.Edit();
						return true;
					}
					if (keyCode2 == Keys.Oemplus)
					{
						goto IL_64A;
					}
					if (keyCode2 != Keys.OemMinus)
					{
						return true;
					}
				}
				this.gridState[524288] = false;
				if (keyEventArgs.Control && !keyEventArgs.Alt)
				{
					this.SetRowExpansionState(-1, false);
					return true;
				}
				return false;
				IL_64A:
				this.gridState[524288] = false;
				if (keyEventArgs.Control)
				{
					this.SetRowExpansionState(-1, true);
					this.EndEdit();
					return true;
				}
				return false;
			}
			return true;
		}

		/// <summary>Previews a keyboard message and returns a value indicating if the key was consumed.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> that contains data about the event. The parameter is passed by reference.</param>
		/// <returns>
		///   <see langword="true" />, if the key was consumed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060015B6 RID: 5558 RVA: 0x0004DB7C File Offset: 0x0004BD7C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessKeyPreview(ref Message m)
		{
			if (m.Msg == 256)
			{
				KeyEventArgs keyEventArgs = new KeyEventArgs((Keys)(long)m.WParam | Control.ModifierKeys);
				Keys keyCode = keyEventArgs.KeyCode;
				if (keyCode <= Keys.A)
				{
					if (keyCode <= Keys.Return)
					{
						if (keyCode != Keys.Tab && keyCode != Keys.Return)
						{
							goto IL_113;
						}
					}
					else
					{
						switch (keyCode)
						{
						case Keys.Escape:
						case Keys.Space:
						case Keys.Prior:
						case Keys.Next:
						case Keys.End:
						case Keys.Home:
						case Keys.Left:
						case Keys.Up:
						case Keys.Right:
						case Keys.Down:
						case Keys.Delete:
							break;
						case Keys.IMEConvert:
						case Keys.IMENonconvert:
						case Keys.IMEAccept:
						case Keys.IMEModeChange:
						case Keys.Select:
						case Keys.Print:
						case Keys.Execute:
						case Keys.Snapshot:
						case Keys.Insert:
							goto IL_113;
						default:
							if (keyCode != Keys.A)
							{
								goto IL_113;
							}
							break;
						}
					}
				}
				else if (keyCode <= Keys.Subtract)
				{
					if (keyCode != Keys.Add && keyCode != Keys.Subtract)
					{
						goto IL_113;
					}
				}
				else if (keyCode != Keys.F2 && keyCode != Keys.Oemplus && keyCode != Keys.OemMinus)
				{
					goto IL_113;
				}
				return this.ProcessGridKey(keyEventArgs);
			}
			if (m.Msg == 257)
			{
				KeyEventArgs keyEventArgs2 = new KeyEventArgs((Keys)(long)m.WParam | Control.ModifierKeys);
				if (keyEventArgs2.KeyCode == Keys.Tab)
				{
					return this.ProcessGridKey(keyEventArgs2);
				}
			}
			IL_113:
			return base.ProcessKeyPreview(ref m);
		}

		/// <summary>Gets a value indicating whether the Tab key should be processed.</summary>
		/// <param name="keyData">A <see cref="T:System.Windows.Forms.Keys" /> that contains data about which the pressed key.</param>
		/// <returns>
		///   <see langword="true" /> if the TAB key should be processed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060015B7 RID: 5559 RVA: 0x0004DCA4 File Offset: 0x0004BEA4
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected bool ProcessTabKey(Keys keyData)
		{
			if (this.listManager == null || this.myGridTable == null)
			{
				return false;
			}
			bool flag = false;
			int count = this.myGridTable.GridColumnStyles.Count;
			bool flag2 = this.isRightToLeft();
			this.ResetSelection();
			if (this.gridState[32768])
			{
				flag = true;
				if (!this.CommitEdit())
				{
					this.Edit();
					return true;
				}
			}
			if ((keyData & Keys.Control) == Keys.Control)
			{
				if ((keyData & Keys.Alt) == Keys.Alt)
				{
					return true;
				}
				Keys keys = keyData & ~Keys.Control;
				this.EndEdit();
				this.gridState[65536] = true;
				try
				{
					this.FocusInternal();
				}
				finally
				{
					this.gridState[65536] = false;
				}
				bool flag3 = false;
				IntSecurity.ModifyFocus.Assert();
				try
				{
					flag3 = base.ProcessDialogKey(keys);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				return flag3;
			}
			else
			{
				DataGridRow[] array = this.DataGridRows;
				GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
				int num = 0;
				int num2 = gridColumnStyles.Count - 1;
				if (array.Length == 0)
				{
					this.EndEdit();
					bool flag4 = false;
					IntSecurity.ModifyFocus.Assert();
					try
					{
						flag4 = base.ProcessDialogKey(keyData);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					return flag4;
				}
				for (int i = 0; i < gridColumnStyles.Count; i++)
				{
					if (gridColumnStyles[i].PropertyDescriptor != null)
					{
						num2 = i;
						break;
					}
				}
				for (int j = gridColumnStyles.Count - 1; j >= 0; j--)
				{
					if (gridColumnStyles[j].PropertyDescriptor != null)
					{
						num = j;
						break;
					}
				}
				if (this.CurrentColumn == num)
				{
					if ((this.gridState[524288] || (!this.gridState[524288] && (keyData & Keys.Shift) != Keys.Shift)) && array[this.CurrentRow].ProcessTabKey(keyData, this.layout.RowHeaders, this.isRightToLeft()))
					{
						if (gridColumnStyles.Count > 0)
						{
							gridColumnStyles[this.CurrentColumn].ConcedeFocus();
						}
						this.gridState[524288] = true;
						if (this.gridState[2048] && base.CanFocus && !this.Focused)
						{
							this.FocusInternal();
						}
						return true;
					}
					if (this.currentRow == this.DataGridRowsLength - 1 && (keyData & Keys.Shift) == Keys.None)
					{
						this.EndEdit();
						bool flag5 = false;
						IntSecurity.ModifyFocus.Assert();
						try
						{
							flag5 = base.ProcessDialogKey(keyData);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
						return flag5;
					}
				}
				if (this.CurrentColumn == num2)
				{
					if (!this.gridState[524288])
					{
						if (this.CurrentRow != 0 && (keyData & Keys.Shift) == Keys.Shift && array[this.CurrentRow - 1].ProcessTabKey(keyData, this.layout.RowHeaders, this.isRightToLeft()))
						{
							int num3 = this.CurrentRow;
							this.CurrentRow = num3 - 1;
							if (gridColumnStyles.Count > 0)
							{
								gridColumnStyles[this.CurrentColumn].ConcedeFocus();
							}
							this.gridState[524288] = true;
							if (this.gridState[2048] && base.CanFocus && !this.Focused)
							{
								this.FocusInternal();
							}
							return true;
						}
						if (this.currentRow == 0 && (keyData & Keys.Shift) == Keys.Shift)
						{
							this.EndEdit();
							bool flag6 = false;
							IntSecurity.ModifyFocus.Assert();
							try
							{
								flag6 = base.ProcessDialogKey(keyData);
							}
							finally
							{
								CodeAccessPermission.RevertAssert();
							}
							return flag6;
						}
					}
					else
					{
						if (array[this.CurrentRow].ProcessTabKey(keyData, this.layout.RowHeaders, this.isRightToLeft()))
						{
							return true;
						}
						this.gridState[524288] = false;
						this.CurrentColumn = num;
						return true;
					}
				}
				if ((keyData & Keys.Shift) != Keys.Shift)
				{
					if (this.CurrentColumn == num)
					{
						if (this.CurrentRow != this.DataGridRowsLength - 1)
						{
							this.CurrentColumn = num2;
						}
						this.CurrentRow++;
					}
					else
					{
						int num4 = this.MoveLeftRight(gridColumnStyles, this.currentCol, true);
						this.CurrentColumn = num4;
					}
				}
				else if (this.CurrentColumn == num2)
				{
					if (this.CurrentRow != 0)
					{
						this.CurrentColumn = num;
					}
					if (!this.gridState[524288])
					{
						int num3 = this.CurrentRow;
						this.CurrentRow = num3 - 1;
					}
				}
				else if (this.gridState[524288] && this.CurrentColumn == num)
				{
					this.InvalidateRow(this.currentRow);
					this.Edit();
				}
				else
				{
					int num5 = this.MoveLeftRight(gridColumnStyles, this.currentCol, false);
					this.CurrentColumn = num5;
				}
				this.gridState[524288] = false;
				if (flag)
				{
					this.ResetSelection();
					this.Edit();
				}
				return true;
			}
		}

		/// <summary>Cancels the current edit operation and rolls back all changes.</summary>
		// Token: 0x060015B8 RID: 5560 RVA: 0x0004E1B8 File Offset: 0x0004C3B8
		protected virtual void CancelEditing()
		{
			this.CancelCursorUpdate();
			if (this.gridState[1048576])
			{
				this.gridState[1048576] = false;
				DataGridRow[] array = this.DataGridRows;
				array[this.DataGridRowsLength - 1] = new DataGridAddNewRow(this, this.myGridTable, this.DataGridRowsLength - 1);
				this.SetDataGridRows(array, this.DataGridRowsLength);
			}
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0004E220 File Offset: 0x0004C420
		internal void RecalculateFonts()
		{
			try
			{
				this.linkFont = new Font(this.Font, FontStyle.Underline);
			}
			catch
			{
			}
			this.fontHeight = this.Font.Height;
			this.linkFontHeight = this.LinkFont.Height;
			this.captionFontHeight = this.CaptionFont.Height;
			if (this.myGridTable == null || this.myGridTable.IsDefault)
			{
				this.headerFontHeight = this.HeaderFont.Height;
				return;
			}
			this.headerFontHeight = this.myGridTable.HeaderFont.Height;
		}

		/// <summary>Occurs when the <see langword="Back" /> button on a child table is clicked.</summary>
		// Token: 0x140000DF RID: 223
		// (add) Token: 0x060015BA RID: 5562 RVA: 0x0004E2C4 File Offset: 0x0004C4C4
		// (remove) Token: 0x060015BB RID: 5563 RVA: 0x0004E2D7 File Offset: 0x0004C4D7
		[SRCategory("CatAction")]
		[SRDescription("DataGridBackButtonClickDescr")]
		public event EventHandler BackButtonClick
		{
			add
			{
				base.Events.AddHandler(DataGrid.EVENT_BACKBUTTONCLICK, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EVENT_BACKBUTTONCLICK, value);
			}
		}

		/// <summary>Occurs when the <see langword="ShowParentDetails" /> button is clicked.</summary>
		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x060015BC RID: 5564 RVA: 0x0004E2EA File Offset: 0x0004C4EA
		// (remove) Token: 0x060015BD RID: 5565 RVA: 0x0004E2FD File Offset: 0x0004C4FD
		[SRCategory("CatAction")]
		[SRDescription("DataGridDownButtonClickDescr")]
		public event EventHandler ShowParentDetailsButtonClick
		{
			add
			{
				base.Events.AddHandler(DataGrid.EVENT_DOWNBUTTONCLICK, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGrid.EVENT_DOWNBUTTONCLICK, value);
			}
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x0004E310 File Offset: 0x0004C510
		private void ResetMouseState()
		{
			this.oldRow = -1;
			this.gridState[262144] = true;
		}

		/// <summary>Turns off selection for all rows that are selected.</summary>
		// Token: 0x060015BF RID: 5567 RVA: 0x0004E32C File Offset: 0x0004C52C
		protected void ResetSelection()
		{
			if (this.numSelectedRows > 0)
			{
				DataGridRow[] array = this.DataGridRows;
				for (int i = 0; i < this.DataGridRowsLength; i++)
				{
					if (array[i].Selected)
					{
						array[i].Selected = false;
					}
				}
			}
			this.numSelectedRows = 0;
			this.lastRowSelected = -1;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0004E37C File Offset: 0x0004C57C
		private void ResetParentRows()
		{
			this.parentRows.Clear();
			this.originalState = null;
			this.caption.BackButtonActive = (this.caption.DownButtonActive = (this.caption.BackButtonVisible = false));
			this.caption.SetDownButtonDirection(!this.layout.ParentRowsVisible);
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0004E3DC File Offset: 0x0004C5DC
		private void ResetUIState()
		{
			this.gridState[524288] = false;
			this.ResetSelection();
			this.ResetMouseState();
			base.PerformLayout();
			base.Invalidate();
			if (this.horizScrollBar.Visible)
			{
				this.horizScrollBar.Invalidate();
			}
			if (this.vertScrollBar.Visible)
			{
				this.vertScrollBar.Invalidate();
			}
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0004E444 File Offset: 0x0004C644
		private void ScrollDown(int rows)
		{
			if (rows != 0)
			{
				this.ClearRegionCache();
				int num = Math.Max(0, Math.Min(this.firstVisibleRow + rows, this.DataGridRowsLength - 1));
				int num2 = this.firstVisibleRow;
				this.firstVisibleRow = num;
				this.vertScrollBar.Value = num;
				bool flag = this.gridState[32768];
				this.ComputeVisibleRows();
				if (this.gridState[131072])
				{
					this.Edit();
					this.gridState[131072] = false;
				}
				else
				{
					this.EndEdit();
				}
				int num3 = this.ComputeRowDelta(num2, num);
				Rectangle rectangle = this.layout.Data;
				if (this.layout.RowHeadersVisible)
				{
					rectangle = Rectangle.Union(rectangle, this.layout.RowHeaders);
				}
				NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
				SafeNativeMethods.ScrollWindow(new HandleRef(this, base.Handle), 0, num3, ref rect, ref rect);
				this.OnScroll(EventArgs.Empty);
				if (flag)
				{
					this.InvalidateRowHeader(this.currentRow);
				}
			}
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0004E568 File Offset: 0x0004C768
		private void ScrollRight(int columns)
		{
			int num = this.firstVisibleCol + columns;
			GridColumnStylesCollection gridColumnStyles = this.myGridTable.GridColumnStyles;
			int num2 = 0;
			int count = gridColumnStyles.Count;
			int num3 = 0;
			if (this.myGridTable.IsDefault)
			{
				num3 = count;
			}
			else
			{
				for (int i = 0; i < count; i++)
				{
					if (gridColumnStyles[i].PropertyDescriptor != null)
					{
						num3++;
					}
				}
			}
			if ((this.lastTotallyVisibleCol == num3 - 1 && columns > 0) || (this.firstVisibleCol == 0 && columns < 0 && this.negOffset == 0))
			{
				return;
			}
			num = Math.Min(num, count - 1);
			for (int j = 0; j < num; j++)
			{
				if (gridColumnStyles[j].PropertyDescriptor != null)
				{
					num2 += gridColumnStyles[j].Width;
				}
			}
			this.HorizontalOffset = num2;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0004E634 File Offset: 0x0004C834
		private void ScrollToColumn(int targetCol)
		{
			int num = targetCol - this.firstVisibleCol;
			if (targetCol > this.lastTotallyVisibleCol && this.lastTotallyVisibleCol != -1)
			{
				num = targetCol - this.lastTotallyVisibleCol;
			}
			if (num != 0 || this.negOffset != 0)
			{
				this.ScrollRight(num);
			}
		}

		/// <summary>Selects a specified row.</summary>
		/// <param name="row">The index of the row to select.</param>
		// Token: 0x060015C5 RID: 5573 RVA: 0x0004E678 File Offset: 0x0004C878
		public void Select(int row)
		{
			DataGridRow[] array = this.DataGridRows;
			if (!array[row].Selected)
			{
				array[row].Selected = true;
				this.numSelectedRows++;
			}
			this.EndEdit();
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0004E6B4 File Offset: 0x0004C8B4
		private void PairTableStylesAndGridColumns(CurrencyManager lm, DataGridTableStyle gridTable, bool forceColumnCreation)
		{
			PropertyDescriptorCollection itemProperties = lm.GetItemProperties();
			GridColumnStylesCollection gridColumnStyles = gridTable.GridColumnStyles;
			if (gridTable.IsDefault || string.Compare(lm.GetListName(), gridTable.MappingName, true, CultureInfo.InvariantCulture) != 0)
			{
				gridTable.SetGridColumnStylesCollection(lm);
				if (gridTable.GridColumnStyles.Count > 0 && gridTable.GridColumnStyles[0].Width == -1)
				{
					this.InitializeColumnWidths();
				}
				return;
			}
			if (gridTable.GridColumnStyles.Count != 0 || base.DesignMode)
			{
				for (int i = 0; i < gridColumnStyles.Count; i++)
				{
					gridColumnStyles[i].PropertyDescriptor = null;
				}
				for (int j = 0; j < itemProperties.Count; j++)
				{
					DataGridColumnStyle dataGridColumnStyle = gridColumnStyles.MapColumnStyleToPropertyName(itemProperties[j].Name);
					if (dataGridColumnStyle != null)
					{
						dataGridColumnStyle.PropertyDescriptor = itemProperties[j];
					}
				}
				gridTable.SetRelationsList(lm);
				return;
			}
			if (forceColumnCreation)
			{
				gridTable.SetGridColumnStylesCollection(lm);
				return;
			}
			gridTable.SetRelationsList(lm);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x0004E7AC File Offset: 0x0004C9AC
		internal void SetDataGridTable(DataGridTableStyle newTable, bool forceColumnCreation)
		{
			if (this.myGridTable != null)
			{
				this.UnWireTableStylePropChanged(this.myGridTable);
				if (this.myGridTable.IsDefault)
				{
					this.myGridTable.GridColumnStyles.ResetPropertyDescriptors();
					this.myGridTable.ResetRelationsList();
				}
			}
			this.myGridTable = newTable;
			this.WireTableStylePropChanged(this.myGridTable);
			this.layout.RowHeadersVisible = (newTable.IsDefault ? this.RowHeadersVisible : newTable.RowHeadersVisible);
			if (newTable != null)
			{
				newTable.DataGrid = this;
			}
			if (this.listManager != null)
			{
				this.PairTableStylesAndGridColumns(this.listManager, this.myGridTable, forceColumnCreation);
			}
			if (newTable != null)
			{
				newTable.ResetRelationsUI();
			}
			this.gridState[16384] = false;
			this.horizScrollBar.Value = 0;
			this.firstVisibleRow = 0;
			this.currentCol = 0;
			if (this.listManager == null)
			{
				this.currentRow = 0;
			}
			else
			{
				this.currentRow = ((this.listManager.Position == -1) ? 0 : this.listManager.Position);
			}
			this.ResetHorizontalOffset();
			this.negOffset = 0;
			this.ResetUIState();
			this.checkHierarchy = true;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0004E8D0 File Offset: 0x0004CAD0
		internal void SetParentRowsVisibility(bool visible)
		{
			Rectangle rectangle = this.layout.ParentRows;
			Rectangle data = this.layout.Data;
			if (this.layout.RowHeadersVisible)
			{
				data.X -= (this.isRightToLeft() ? 0 : this.layout.RowHeaders.Width);
				data.Width += this.layout.RowHeaders.Width;
			}
			if (this.layout.ColumnHeadersVisible)
			{
				data.Y -= this.layout.ColumnHeaders.Height;
				data.Height += this.layout.ColumnHeaders.Height;
			}
			this.EndEdit();
			if (visible)
			{
				this.layout.ParentRowsVisible = true;
				base.PerformLayout();
				base.Invalidate();
				return;
			}
			NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(data.X, data.Y - this.layout.ParentRows.Height, data.Width, data.Height + this.layout.ParentRows.Height);
			SafeNativeMethods.ScrollWindow(new HandleRef(this, base.Handle), 0, -rectangle.Height, ref rect, ref rect);
			if (this.vertScrollBar.Visible)
			{
				Rectangle bounds = this.vertScrollBar.Bounds;
				bounds.Y -= rectangle.Height;
				bounds.Height += rectangle.Height;
				base.Invalidate(bounds);
			}
			this.layout.ParentRowsVisible = false;
			base.PerformLayout();
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0004EA74 File Offset: 0x0004CC74
		private void SetRowExpansionState(int row, bool expanded)
		{
			if (row < -1 || row > this.DataGridRowsLength - (this.policy.AllowAdd ? 2 : 1))
			{
				throw new ArgumentOutOfRangeException("row");
			}
			DataGridRow[] array = this.DataGridRows;
			if (row == -1)
			{
				DataGridRelationshipRow[] expandableRows = this.GetExpandableRows();
				bool flag = false;
				for (int i = 0; i < expandableRows.Length; i++)
				{
					if (expandableRows[i].Expanded != expanded)
					{
						expandableRows[i].Expanded = expanded;
						flag = true;
					}
				}
				if (flag && (this.gridState[16384] || this.gridState[32768]))
				{
					this.ResetSelection();
					this.Edit();
					return;
				}
			}
			else if (array[row] is DataGridRelationshipRow)
			{
				DataGridRelationshipRow dataGridRelationshipRow = (DataGridRelationshipRow)array[row];
				if (dataGridRelationshipRow.Expanded != expanded)
				{
					if (this.gridState[16384] || this.gridState[32768])
					{
						this.ResetSelection();
						this.Edit();
					}
					dataGridRelationshipRow.Expanded = expanded;
				}
			}
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x0004EB70 File Offset: 0x0004CD70
		private void ObjectSiteChange(IContainer container, IComponent component, bool site)
		{
			if (site)
			{
				if (component.Site == null)
				{
					container.Add(component);
					return;
				}
			}
			else if (component.Site != null && component.Site.Container == container)
			{
				container.Remove(component);
			}
		}

		/// <summary>Adds or removes the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects from the container that is associated with the <see cref="T:System.Windows.Forms.DataGrid" />.</summary>
		/// <param name="site">
		///   <see langword="true" /> to add the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects to a container; <see langword="false" /> to remove them.</param>
		// Token: 0x060015CB RID: 5579 RVA: 0x0004EBA4 File Offset: 0x0004CDA4
		public void SubObjectsSiteChange(bool site)
		{
			if (this.DesignMode && this.Site != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.Site.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					DesignerTransaction designerTransaction = designerHost.CreateTransaction();
					try
					{
						IContainer container = this.Site.Container;
						DataGridTableStyle[] array = new DataGridTableStyle[this.TableStyles.Count];
						this.TableStyles.CopyTo(array, 0);
						foreach (DataGridTableStyle dataGridTableStyle in array)
						{
							this.ObjectSiteChange(container, dataGridTableStyle, site);
							DataGridColumnStyle[] array2 = new DataGridColumnStyle[dataGridTableStyle.GridColumnStyles.Count];
							dataGridTableStyle.GridColumnStyles.CopyTo(array2, 0);
							foreach (DataGridColumnStyle dataGridColumnStyle in array2)
							{
								this.ObjectSiteChange(container, dataGridColumnStyle, site);
							}
						}
					}
					finally
					{
						designerTransaction.Commit();
					}
				}
			}
		}

		/// <summary>Unselects a specified row.</summary>
		/// <param name="row">The index of the row to deselect.</param>
		// Token: 0x060015CC RID: 5580 RVA: 0x0004ECA0 File Offset: 0x0004CEA0
		public void UnSelect(int row)
		{
			DataGridRow[] array = this.DataGridRows;
			if (array[row].Selected)
			{
				array[row].Selected = false;
				this.numSelectedRows--;
			}
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0004ECD8 File Offset: 0x0004CED8
		private void UpdateListManager()
		{
			try
			{
				if (this.listManager != null)
				{
					this.EndEdit();
					this.listManager.EndCurrentEdit();
				}
			}
			catch
			{
			}
		}

		/// <summary>Gets the string that is the delimiter between columns when row contents are copied to the Clipboard.</summary>
		/// <returns>The string value "\t", which represents a tab used to separate columns in a row.</returns>
		// Token: 0x060015CE RID: 5582 RVA: 0x0004ED14 File Offset: 0x0004CF14
		protected virtual string GetOutputTextDelimiter()
		{
			return "\t";
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0004ED1B File Offset: 0x0004CF1B
		private int MirrorRectangle(Rectangle R1, Rectangle rect, bool rightToLeft)
		{
			if (rightToLeft)
			{
				return rect.Right + rect.X - R1.Right;
			}
			return R1.X;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0004ED3F File Offset: 0x0004CF3F
		private int MirrorPoint(int x, Rectangle rect, bool rightToLeft)
		{
			if (rightToLeft)
			{
				return rect.Right + rect.X - x;
			}
			return x;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0004ED57 File Offset: 0x0004CF57
		private bool isRightToLeft()
		{
			return this.RightToLeft == RightToLeft.Yes;
		}

		// Token: 0x04000981 RID: 2433
		internal TraceSwitch DataGridAcc;

		// Token: 0x04000982 RID: 2434
		private const int GRIDSTATE_allowSorting = 1;

		// Token: 0x04000983 RID: 2435
		private const int GRIDSTATE_columnHeadersVisible = 2;

		// Token: 0x04000984 RID: 2436
		private const int GRIDSTATE_rowHeadersVisible = 4;

		// Token: 0x04000985 RID: 2437
		private const int GRIDSTATE_trackColResize = 8;

		// Token: 0x04000986 RID: 2438
		private const int GRIDSTATE_trackRowResize = 16;

		// Token: 0x04000987 RID: 2439
		private const int GRIDSTATE_isLedgerStyle = 32;

		// Token: 0x04000988 RID: 2440
		private const int GRIDSTATE_isFlatMode = 64;

		// Token: 0x04000989 RID: 2441
		private const int GRIDSTATE_listHasErrors = 128;

		// Token: 0x0400098A RID: 2442
		private const int GRIDSTATE_dragging = 256;

		// Token: 0x0400098B RID: 2443
		private const int GRIDSTATE_inListAddNew = 512;

		// Token: 0x0400098C RID: 2444
		private const int GRIDSTATE_inDeleteRow = 1024;

		// Token: 0x0400098D RID: 2445
		private const int GRIDSTATE_canFocus = 2048;

		// Token: 0x0400098E RID: 2446
		private const int GRIDSTATE_readOnlyMode = 4096;

		// Token: 0x0400098F RID: 2447
		private const int GRIDSTATE_allowNavigation = 8192;

		// Token: 0x04000990 RID: 2448
		private const int GRIDSTATE_isNavigating = 16384;

		// Token: 0x04000991 RID: 2449
		private const int GRIDSTATE_isEditing = 32768;

		// Token: 0x04000992 RID: 2450
		private const int GRIDSTATE_editControlChanging = 65536;

		// Token: 0x04000993 RID: 2451
		private const int GRIDSTATE_isScrolling = 131072;

		// Token: 0x04000994 RID: 2452
		private const int GRIDSTATE_overCaption = 262144;

		// Token: 0x04000995 RID: 2453
		private const int GRIDSTATE_childLinkFocused = 524288;

		// Token: 0x04000996 RID: 2454
		private const int GRIDSTATE_inAddNewRow = 1048576;

		// Token: 0x04000997 RID: 2455
		private const int GRIDSTATE_inSetListManager = 2097152;

		// Token: 0x04000998 RID: 2456
		private const int GRIDSTATE_metaDataChanged = 4194304;

		// Token: 0x04000999 RID: 2457
		private const int GRIDSTATE_exceptionInPaint = 8388608;

		// Token: 0x0400099A RID: 2458
		private const int GRIDSTATE_layoutSuspended = 16777216;

		// Token: 0x0400099B RID: 2459
		private BitVector32 gridState;

		// Token: 0x0400099C RID: 2460
		private const int NumRowsForAutoResize = 10;

		// Token: 0x0400099D RID: 2461
		private const int errorRowBitmapWidth = 15;

		// Token: 0x0400099E RID: 2462
		private const DataGridParentRowsLabelStyle defaultParentRowsLabelStyle = DataGridParentRowsLabelStyle.Both;

		// Token: 0x0400099F RID: 2463
		private const BorderStyle defaultBorderStyle = BorderStyle.Fixed3D;

		// Token: 0x040009A0 RID: 2464
		private const bool defaultCaptionVisible = true;

		// Token: 0x040009A1 RID: 2465
		private const bool defaultParentRowsVisible = true;

		// Token: 0x040009A2 RID: 2466
		private DataGridTableStyle defaultTableStyle = new DataGridTableStyle(true);

		// Token: 0x040009A3 RID: 2467
		private SolidBrush alternatingBackBrush = DataGrid.DefaultAlternatingBackBrush;

		// Token: 0x040009A4 RID: 2468
		private SolidBrush gridLineBrush = DataGrid.DefaultGridLineBrush;

		// Token: 0x040009A5 RID: 2469
		private const DataGridLineStyle defaultGridLineStyle = DataGridLineStyle.Solid;

		// Token: 0x040009A6 RID: 2470
		private DataGridLineStyle gridLineStyle = DataGridLineStyle.Solid;

		// Token: 0x040009A7 RID: 2471
		private SolidBrush headerBackBrush = DataGrid.DefaultHeaderBackBrush;

		// Token: 0x040009A8 RID: 2472
		private Font headerFont;

		// Token: 0x040009A9 RID: 2473
		private SolidBrush headerForeBrush = DataGrid.DefaultHeaderForeBrush;

		// Token: 0x040009AA RID: 2474
		private Pen headerForePen = DataGrid.DefaultHeaderForePen;

		// Token: 0x040009AB RID: 2475
		private SolidBrush linkBrush = DataGrid.DefaultLinkBrush;

		// Token: 0x040009AC RID: 2476
		private const int defaultPreferredColumnWidth = 75;

		// Token: 0x040009AD RID: 2477
		private int preferredColumnWidth = 75;

		// Token: 0x040009AE RID: 2478
		private static int defaultFontHeight = Control.DefaultFont.Height;

		// Token: 0x040009AF RID: 2479
		private int prefferedRowHeight = DataGrid.defaultFontHeight + 3;

		// Token: 0x040009B0 RID: 2480
		private const int defaultRowHeaderWidth = 35;

		// Token: 0x040009B1 RID: 2481
		private int rowHeaderWidth = 35;

		// Token: 0x040009B2 RID: 2482
		private int minRowHeaderWidth;

		// Token: 0x040009B3 RID: 2483
		private SolidBrush selectionBackBrush = DataGrid.DefaultSelectionBackBrush;

		// Token: 0x040009B4 RID: 2484
		private SolidBrush selectionForeBrush = DataGrid.DefaultSelectionForeBrush;

		// Token: 0x040009B5 RID: 2485
		private DataGridParentRows parentRows;

		// Token: 0x040009B6 RID: 2486
		private DataGridState originalState;

		// Token: 0x040009B7 RID: 2487
		private DataGridRow[] dataGridRows = new DataGridRow[0];

		// Token: 0x040009B8 RID: 2488
		private int dataGridRowsLength;

		// Token: 0x040009B9 RID: 2489
		private int toolTipId;

		// Token: 0x040009BA RID: 2490
		private DataGridToolTip toolTipProvider;

		// Token: 0x040009BB RID: 2491
		private DataGridAddNewRow addNewRow;

		// Token: 0x040009BC RID: 2492
		private DataGrid.LayoutData layout = new DataGrid.LayoutData();

		// Token: 0x040009BD RID: 2493
		private NativeMethods.RECT[] cachedScrollableRegion;

		// Token: 0x040009BE RID: 2494
		internal bool allowColumnResize = true;

		// Token: 0x040009BF RID: 2495
		internal bool allowRowResize = true;

		// Token: 0x040009C0 RID: 2496
		internal DataGridParentRowsLabelStyle parentRowsLabels = DataGridParentRowsLabelStyle.Both;

		// Token: 0x040009C1 RID: 2497
		private int trackColAnchor;

		// Token: 0x040009C2 RID: 2498
		private int trackColumn;

		// Token: 0x040009C3 RID: 2499
		private int trackRowAnchor;

		// Token: 0x040009C4 RID: 2500
		private int trackRow;

		// Token: 0x040009C5 RID: 2501
		private PropertyDescriptor trackColumnHeader;

		// Token: 0x040009C6 RID: 2502
		private MouseEventArgs lastSplitBar;

		// Token: 0x040009C7 RID: 2503
		private Font linkFont;

		// Token: 0x040009C8 RID: 2504
		private SolidBrush backBrush = DataGrid.DefaultBackBrush;

		// Token: 0x040009C9 RID: 2505
		private SolidBrush foreBrush = DataGrid.DefaultForeBrush;

		// Token: 0x040009CA RID: 2506
		private SolidBrush backgroundBrush = DataGrid.DefaultBackgroundBrush;

		// Token: 0x040009CB RID: 2507
		private int fontHeight = -1;

		// Token: 0x040009CC RID: 2508
		private int linkFontHeight = -1;

		// Token: 0x040009CD RID: 2509
		private int captionFontHeight = -1;

		// Token: 0x040009CE RID: 2510
		private int headerFontHeight = -1;

		// Token: 0x040009CF RID: 2511
		private DataGridCaption caption;

		// Token: 0x040009D0 RID: 2512
		private BorderStyle borderStyle;

		// Token: 0x040009D1 RID: 2513
		private object dataSource;

		// Token: 0x040009D2 RID: 2514
		private string dataMember = "";

		// Token: 0x040009D3 RID: 2515
		private CurrencyManager listManager;

		// Token: 0x040009D4 RID: 2516
		private Control toBeDisposedEditingControl;

		// Token: 0x040009D5 RID: 2517
		internal GridTableStylesCollection dataGridTables;

		// Token: 0x040009D6 RID: 2518
		internal DataGridTableStyle myGridTable;

		// Token: 0x040009D7 RID: 2519
		internal bool checkHierarchy = true;

		// Token: 0x040009D8 RID: 2520
		internal bool inInit;

		// Token: 0x040009D9 RID: 2521
		internal int currentRow;

		// Token: 0x040009DA RID: 2522
		internal int currentCol;

		// Token: 0x040009DB RID: 2523
		private int numSelectedRows;

		// Token: 0x040009DC RID: 2524
		private int lastRowSelected = -1;

		// Token: 0x040009DD RID: 2525
		private DataGrid.Policy policy = new DataGrid.Policy();

		// Token: 0x040009DE RID: 2526
		private DataGridColumnStyle editColumn;

		// Token: 0x040009DF RID: 2527
		private DataGridRow editRow;

		// Token: 0x040009E0 RID: 2528
		private ScrollBar horizScrollBar = new HScrollBar();

		// Token: 0x040009E1 RID: 2529
		private ScrollBar vertScrollBar = new VScrollBar();

		// Token: 0x040009E2 RID: 2530
		private int horizontalOffset;

		// Token: 0x040009E3 RID: 2531
		private int negOffset;

		// Token: 0x040009E4 RID: 2532
		private int wheelDelta;

		// Token: 0x040009E5 RID: 2533
		internal int firstVisibleRow;

		// Token: 0x040009E6 RID: 2534
		internal int firstVisibleCol;

		// Token: 0x040009E7 RID: 2535
		private int numVisibleRows;

		// Token: 0x040009E8 RID: 2536
		private int numVisibleCols;

		// Token: 0x040009E9 RID: 2537
		private int numTotallyVisibleRows;

		// Token: 0x040009EA RID: 2538
		private int lastTotallyVisibleCol;

		// Token: 0x040009EB RID: 2539
		private int oldRow = -1;

		// Token: 0x040009EC RID: 2540
		private static readonly object EVENT_CURRENTCELLCHANGED = new object();

		// Token: 0x040009ED RID: 2541
		private static readonly object EVENT_NODECLICKED = new object();

		// Token: 0x040009EE RID: 2542
		private static readonly object EVENT_SCROLL = new object();

		// Token: 0x040009EF RID: 2543
		private static readonly object EVENT_BACKBUTTONCLICK = new object();

		// Token: 0x040009F0 RID: 2544
		private static readonly object EVENT_DOWNBUTTONCLICK = new object();

		// Token: 0x040009F1 RID: 2545
		private ItemChangedEventHandler itemChangedHandler;

		// Token: 0x040009F2 RID: 2546
		private EventHandler positionChangedHandler;

		// Token: 0x040009F3 RID: 2547
		private EventHandler currentChangedHandler;

		// Token: 0x040009F4 RID: 2548
		private EventHandler metaDataChangedHandler;

		// Token: 0x040009F5 RID: 2549
		private CollectionChangeEventHandler dataGridTableStylesCollectionChanged;

		// Token: 0x040009F6 RID: 2550
		private EventHandler backButtonHandler;

		// Token: 0x040009F7 RID: 2551
		private EventHandler downButtonHandler;

		// Token: 0x040009F8 RID: 2552
		private NavigateEventHandler onNavigate;

		// Token: 0x040009F9 RID: 2553
		private EventHandler onRowHeaderClick;

		// Token: 0x040009FA RID: 2554
		private static readonly object EVENT_BORDERSTYLECHANGED = new object();

		// Token: 0x040009FB RID: 2555
		private static readonly object EVENT_CAPTIONVISIBLECHANGED = new object();

		// Token: 0x040009FC RID: 2556
		private static readonly object EVENT_DATASOURCECHANGED = new object();

		// Token: 0x040009FD RID: 2557
		private static readonly object EVENT_PARENTROWSLABELSTYLECHANGED = new object();

		// Token: 0x040009FE RID: 2558
		private static readonly object EVENT_FLATMODECHANGED = new object();

		// Token: 0x040009FF RID: 2559
		private static readonly object EVENT_BACKGROUNDCOLORCHANGED = new object();

		// Token: 0x04000A00 RID: 2560
		private static readonly object EVENT_ALLOWNAVIGATIONCHANGED = new object();

		// Token: 0x04000A01 RID: 2561
		private static readonly object EVENT_READONLYCHANGED = new object();

		// Token: 0x04000A02 RID: 2562
		private static readonly object EVENT_PARENTROWSVISIBLECHANGED = new object();

		// Token: 0x02000644 RID: 1604
		[ComVisible(true)]
		internal class DataGridAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x060064A5 RID: 25765 RVA: 0x0009B733 File Offset: 0x00099933
			public DataGridAccessibleObject(DataGrid owner)
				: base(owner)
			{
			}

			// Token: 0x17001595 RID: 5525
			// (get) Token: 0x060064A6 RID: 25766 RVA: 0x00176824 File Offset: 0x00174A24
			internal DataGrid DataGrid
			{
				get
				{
					return (DataGrid)base.Owner;
				}
			}

			// Token: 0x17001596 RID: 5526
			// (get) Token: 0x060064A7 RID: 25767 RVA: 0x00176831 File Offset: 0x00174A31
			private int ColumnCountPrivate
			{
				get
				{
					return ((DataGrid)base.Owner).myGridTable.GridColumnStyles.Count;
				}
			}

			// Token: 0x17001597 RID: 5527
			// (get) Token: 0x060064A8 RID: 25768 RVA: 0x0017684D File Offset: 0x00174A4D
			private int RowCountPrivate
			{
				get
				{
					return ((DataGrid)base.Owner).dataGridRows.Length;
				}
			}

			// Token: 0x17001598 RID: 5528
			// (get) Token: 0x060064A9 RID: 25769 RVA: 0x00176864 File Offset: 0x00174A64
			// (set) Token: 0x060064AA RID: 25770 RVA: 0x00176887 File Offset: 0x00174A87
			public override string Name
			{
				get
				{
					string accessibleName = base.Owner.AccessibleName;
					if (accessibleName != null)
					{
						return accessibleName;
					}
					return "DataGrid";
				}
				set
				{
					base.Owner.AccessibleName = value;
				}
			}

			// Token: 0x17001599 RID: 5529
			// (get) Token: 0x060064AB RID: 25771 RVA: 0x00176898 File Offset: 0x00174A98
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.Table;
				}
			}

			// Token: 0x060064AC RID: 25772 RVA: 0x001768BC File Offset: 0x00174ABC
			public override AccessibleObject GetChild(int index)
			{
				DataGrid dataGrid = (DataGrid)base.Owner;
				int columnCountPrivate = this.ColumnCountPrivate;
				int rowCountPrivate = this.RowCountPrivate;
				if (dataGrid.dataGridRows == null)
				{
					dataGrid.CreateDataGridRows();
				}
				if (index < 1)
				{
					return dataGrid.ParentRowsAccessibleObject;
				}
				index--;
				if (index < columnCountPrivate)
				{
					return dataGrid.myGridTable.GridColumnStyles[index].HeaderAccessibleObject;
				}
				index -= columnCountPrivate;
				if (index < rowCountPrivate)
				{
					return dataGrid.dataGridRows[index].AccessibleObject;
				}
				index -= rowCountPrivate;
				if (dataGrid.horizScrollBar.Visible)
				{
					if (index == 0)
					{
						return dataGrid.horizScrollBar.AccessibilityObject;
					}
					index--;
				}
				if (dataGrid.vertScrollBar.Visible)
				{
					if (index == 0)
					{
						return dataGrid.vertScrollBar.AccessibilityObject;
					}
					index--;
				}
				int count = dataGrid.myGridTable.GridColumnStyles.Count;
				int num = dataGrid.dataGridRows.Length;
				int num2 = index / count;
				int num3 = index % count;
				if (num2 < dataGrid.dataGridRows.Length && num3 < dataGrid.myGridTable.GridColumnStyles.Count)
				{
					return dataGrid.dataGridRows[num2].AccessibleObject.GetChild(num3);
				}
				return null;
			}

			// Token: 0x060064AD RID: 25773 RVA: 0x001769D8 File Offset: 0x00174BD8
			public override int GetChildCount()
			{
				int num = 1 + this.ColumnCountPrivate + ((DataGrid)base.Owner).DataGridRowsLength;
				if (this.DataGrid.horizScrollBar.Visible)
				{
					num++;
				}
				if (this.DataGrid.vertScrollBar.Visible)
				{
					num++;
				}
				return num + this.DataGrid.DataGridRows.Length * this.DataGrid.myGridTable.GridColumnStyles.Count;
			}

			// Token: 0x060064AE RID: 25774 RVA: 0x00176A52 File Offset: 0x00174C52
			public override AccessibleObject GetFocused()
			{
				if (this.DataGrid.Focused)
				{
					return this.GetSelected();
				}
				return null;
			}

			// Token: 0x060064AF RID: 25775 RVA: 0x00176A6C File Offset: 0x00174C6C
			public override AccessibleObject GetSelected()
			{
				if (this.DataGrid.DataGridRows.Length == 0 || this.DataGrid.myGridTable.GridColumnStyles.Count == 0)
				{
					return null;
				}
				DataGridCell currentCell = this.DataGrid.CurrentCell;
				return this.GetChild(1 + this.ColumnCountPrivate + currentCell.RowNumber).GetChild(currentCell.ColumnNumber);
			}

			// Token: 0x060064B0 RID: 25776 RVA: 0x00176AD0 File Offset: 0x00174CD0
			public override AccessibleObject HitTest(int x, int y)
			{
				Point point = this.DataGrid.PointToClient(new Point(x, y));
				DataGrid.HitTestInfo hitTestInfo = this.DataGrid.HitTest(point.X, point.Y);
				DataGrid.HitTestType type = hitTestInfo.Type;
				if (type <= DataGrid.HitTestType.RowResize)
				{
					switch (type)
					{
					case DataGrid.HitTestType.None:
					case DataGrid.HitTestType.Cell | DataGrid.HitTestType.ColumnHeader:
					case DataGrid.HitTestType.Cell | DataGrid.HitTestType.RowHeader:
					case DataGrid.HitTestType.ColumnHeader | DataGrid.HitTestType.RowHeader:
					case DataGrid.HitTestType.Cell | DataGrid.HitTestType.ColumnHeader | DataGrid.HitTestType.RowHeader:
					case DataGrid.HitTestType.ColumnResize:
						break;
					case DataGrid.HitTestType.Cell:
						return this.GetChild(1 + this.ColumnCountPrivate + hitTestInfo.Row).GetChild(hitTestInfo.Column);
					case DataGrid.HitTestType.ColumnHeader:
						return this.GetChild(1 + hitTestInfo.Column);
					case DataGrid.HitTestType.RowHeader:
						return this.GetChild(1 + this.ColumnCountPrivate + hitTestInfo.Row);
					default:
						if (type != DataGrid.HitTestType.RowResize)
						{
						}
						break;
					}
				}
				else if (type != DataGrid.HitTestType.Caption)
				{
					if (type == DataGrid.HitTestType.ParentRows)
					{
						return this.DataGrid.ParentRowsAccessibleObject;
					}
				}
				return null;
			}

			// Token: 0x060064B1 RID: 25777 RVA: 0x00176BA6 File Offset: 0x00174DA6
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				if (this.GetChildCount() > 0)
				{
					if (navdir == AccessibleNavigation.FirstChild)
					{
						return this.GetChild(0);
					}
					if (navdir == AccessibleNavigation.LastChild)
					{
						return this.GetChild(this.GetChildCount() - 1);
					}
				}
				return null;
			}
		}

		// Token: 0x02000645 RID: 1605
		internal class LayoutData
		{
			// Token: 0x060064B2 RID: 25778 RVA: 0x00176BD4 File Offset: 0x00174DD4
			public LayoutData()
			{
			}

			// Token: 0x060064B3 RID: 25779 RVA: 0x00176C54 File Offset: 0x00174E54
			public LayoutData(DataGrid.LayoutData src)
			{
				this.GrabLayout(src);
			}

			// Token: 0x060064B4 RID: 25780 RVA: 0x00176CD8 File Offset: 0x00174ED8
			private void GrabLayout(DataGrid.LayoutData src)
			{
				this.Inside = src.Inside;
				this.TopLeftHeader = src.TopLeftHeader;
				this.ColumnHeaders = src.ColumnHeaders;
				this.RowHeaders = src.RowHeaders;
				this.Data = src.Data;
				this.Caption = src.Caption;
				this.ParentRows = src.ParentRows;
				this.ResizeBoxRect = src.ResizeBoxRect;
				this.ColumnHeadersVisible = src.ColumnHeadersVisible;
				this.RowHeadersVisible = src.RowHeadersVisible;
				this.CaptionVisible = src.CaptionVisible;
				this.ParentRowsVisible = src.ParentRowsVisible;
				this.ClientRectangle = src.ClientRectangle;
			}

			// Token: 0x060064B5 RID: 25781 RVA: 0x00176D84 File Offset: 0x00174F84
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(200);
				stringBuilder.Append(base.ToString());
				stringBuilder.Append(" { \n");
				stringBuilder.Append("Inside = ");
				stringBuilder.Append(this.Inside.ToString());
				stringBuilder.Append('\n');
				stringBuilder.Append("TopLeftHeader = ");
				stringBuilder.Append(this.TopLeftHeader.ToString());
				stringBuilder.Append('\n');
				stringBuilder.Append("ColumnHeaders = ");
				stringBuilder.Append(this.ColumnHeaders.ToString());
				stringBuilder.Append('\n');
				stringBuilder.Append("RowHeaders = ");
				stringBuilder.Append(this.RowHeaders.ToString());
				stringBuilder.Append('\n');
				stringBuilder.Append("Data = ");
				stringBuilder.Append(this.Data.ToString());
				stringBuilder.Append('\n');
				stringBuilder.Append("Caption = ");
				stringBuilder.Append(this.Caption.ToString());
				stringBuilder.Append('\n');
				stringBuilder.Append("ParentRows = ");
				stringBuilder.Append(this.ParentRows.ToString());
				stringBuilder.Append('\n');
				stringBuilder.Append("ResizeBoxRect = ");
				stringBuilder.Append(this.ResizeBoxRect.ToString());
				stringBuilder.Append('\n');
				stringBuilder.Append("ColumnHeadersVisible = ");
				stringBuilder.Append(this.ColumnHeadersVisible.ToString());
				stringBuilder.Append('\n');
				stringBuilder.Append("RowHeadersVisible = ");
				stringBuilder.Append(this.RowHeadersVisible.ToString());
				stringBuilder.Append('\n');
				stringBuilder.Append("CaptionVisible = ");
				stringBuilder.Append(this.CaptionVisible.ToString());
				stringBuilder.Append('\n');
				stringBuilder.Append("ParentRowsVisible = ");
				stringBuilder.Append(this.ParentRowsVisible.ToString());
				stringBuilder.Append('\n');
				stringBuilder.Append("ClientRectangle = ");
				stringBuilder.Append(this.ClientRectangle.ToString());
				stringBuilder.Append(" } ");
				return stringBuilder.ToString();
			}

			// Token: 0x040039A7 RID: 14759
			internal bool dirty = true;

			// Token: 0x040039A8 RID: 14760
			public Rectangle Inside = Rectangle.Empty;

			// Token: 0x040039A9 RID: 14761
			public Rectangle RowHeaders = Rectangle.Empty;

			// Token: 0x040039AA RID: 14762
			public Rectangle TopLeftHeader = Rectangle.Empty;

			// Token: 0x040039AB RID: 14763
			public Rectangle ColumnHeaders = Rectangle.Empty;

			// Token: 0x040039AC RID: 14764
			public Rectangle Data = Rectangle.Empty;

			// Token: 0x040039AD RID: 14765
			public Rectangle Caption = Rectangle.Empty;

			// Token: 0x040039AE RID: 14766
			public Rectangle ParentRows = Rectangle.Empty;

			// Token: 0x040039AF RID: 14767
			public Rectangle ResizeBoxRect = Rectangle.Empty;

			// Token: 0x040039B0 RID: 14768
			public bool ColumnHeadersVisible;

			// Token: 0x040039B1 RID: 14769
			public bool RowHeadersVisible;

			// Token: 0x040039B2 RID: 14770
			public bool CaptionVisible;

			// Token: 0x040039B3 RID: 14771
			public bool ParentRowsVisible;

			// Token: 0x040039B4 RID: 14772
			public Rectangle ClientRectangle = Rectangle.Empty;
		}

		/// <summary>Contains information about a part of the <see cref="T:System.Windows.Forms.DataGrid" /> at a specified coordinate. This class cannot be inherited.</summary>
		// Token: 0x02000646 RID: 1606
		public sealed class HitTestInfo
		{
			// Token: 0x060064B6 RID: 25782 RVA: 0x00176FF0 File Offset: 0x001751F0
			internal HitTestInfo()
			{
				this.type = DataGrid.HitTestType.None;
				this.row = (this.col = -1);
			}

			// Token: 0x060064B7 RID: 25783 RVA: 0x0017701C File Offset: 0x0017521C
			internal HitTestInfo(DataGrid.HitTestType type)
			{
				this.type = type;
				this.row = (this.col = -1);
			}

			/// <summary>Gets the number of the column the user has clicked.</summary>
			/// <returns>The number of the column.</returns>
			// Token: 0x1700159A RID: 5530
			// (get) Token: 0x060064B8 RID: 25784 RVA: 0x00177046 File Offset: 0x00175246
			public int Column
			{
				get
				{
					return this.col;
				}
			}

			/// <summary>Gets the number of the row the user has clicked.</summary>
			/// <returns>The number of the clicked row.</returns>
			// Token: 0x1700159B RID: 5531
			// (get) Token: 0x060064B9 RID: 25785 RVA: 0x0017704E File Offset: 0x0017524E
			public int Row
			{
				get
				{
					return this.row;
				}
			}

			/// <summary>Gets the part of the <see cref="T:System.Windows.Forms.DataGrid" /> control, other than the row or column, that was clicked.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.DataGrid.HitTestType" /> enumerations.</returns>
			// Token: 0x1700159C RID: 5532
			// (get) Token: 0x060064BA RID: 25786 RVA: 0x00177056 File Offset: 0x00175256
			public DataGrid.HitTestType Type
			{
				get
				{
					return this.type;
				}
			}

			/// <summary>Indicates whether two objects are identical.</summary>
			/// <param name="value">The second object to compare, typed as <see cref="T:System.Object" />.</param>
			/// <returns>
			///   <see langword="true" /> if the objects are equal; otherwise, <see langword="false" />.</returns>
			// Token: 0x060064BB RID: 25787 RVA: 0x00177060 File Offset: 0x00175260
			public override bool Equals(object value)
			{
				if (value is DataGrid.HitTestInfo)
				{
					DataGrid.HitTestInfo hitTestInfo = (DataGrid.HitTestInfo)value;
					return this.type == hitTestInfo.type && this.row == hitTestInfo.row && this.col == hitTestInfo.col;
				}
				return false;
			}

			/// <summary>Gets the hash code for the <see cref="T:System.Windows.Forms.DataGrid.HitTestInfo" /> instance.</summary>
			/// <returns>The hash code for this instance.</returns>
			// Token: 0x060064BC RID: 25788 RVA: 0x001770AA File Offset: 0x001752AA
			public override int GetHashCode()
			{
				return (int)(this.type + (this.row << 8) + (this.col << 16));
			}

			/// <summary>Gets the type, row number, and column number.</summary>
			/// <returns>The type, row number, and column number.</returns>
			// Token: 0x060064BD RID: 25789 RVA: 0x001770C8 File Offset: 0x001752C8
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"{ ",
					this.type.ToString(),
					",",
					this.row.ToString(CultureInfo.InvariantCulture),
					",",
					this.col.ToString(CultureInfo.InvariantCulture),
					"}"
				});
			}

			// Token: 0x040039B5 RID: 14773
			internal DataGrid.HitTestType type;

			// Token: 0x040039B6 RID: 14774
			internal int row;

			// Token: 0x040039B7 RID: 14775
			internal int col;

			/// <summary>Indicates that a coordinate corresponds to part of the <see cref="T:System.Windows.Forms.DataGrid" /> control that is not functioning.</summary>
			// Token: 0x040039B8 RID: 14776
			public static readonly DataGrid.HitTestInfo Nowhere = new DataGrid.HitTestInfo();
		}

		/// <summary>Specifies the part of the <see cref="T:System.Windows.Forms.DataGrid" /> control the user has clicked.</summary>
		// Token: 0x02000647 RID: 1607
		[Flags]
		public enum HitTestType
		{
			/// <summary>The background area, visible when the control contains no table, few rows, or when a table is scrolled to its bottom.</summary>
			// Token: 0x040039BA RID: 14778
			None = 0,
			/// <summary>A cell in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
			// Token: 0x040039BB RID: 14779
			Cell = 1,
			/// <summary>A column header in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
			// Token: 0x040039BC RID: 14780
			ColumnHeader = 2,
			/// <summary>A row header in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
			// Token: 0x040039BD RID: 14781
			RowHeader = 4,
			/// <summary>The column border, which is the line between column headers. It can be dragged to resize a column's width.</summary>
			// Token: 0x040039BE RID: 14782
			ColumnResize = 8,
			/// <summary>The row border, which is the line between grid row headers. It can be dragged to resize a row's height.</summary>
			// Token: 0x040039BF RID: 14783
			RowResize = 16,
			/// <summary>The caption of the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
			// Token: 0x040039C0 RID: 14784
			Caption = 32,
			/// <summary>The parent row section of the <see cref="T:System.Windows.Forms.DataGrid" /> control. The parent row displays information from or about the parent table of the currently displayed child table, such as the name of the parent table, column names and values of the parent record.</summary>
			// Token: 0x040039C1 RID: 14785
			ParentRows = 64
		}

		// Token: 0x02000648 RID: 1608
		private class Policy
		{
			// Token: 0x1700159D RID: 5533
			// (get) Token: 0x060064C0 RID: 25792 RVA: 0x00177163 File Offset: 0x00175363
			// (set) Token: 0x060064C1 RID: 25793 RVA: 0x0017716B File Offset: 0x0017536B
			public bool AllowAdd
			{
				get
				{
					return this.allowAdd;
				}
				set
				{
					if (this.allowAdd != value)
					{
						this.allowAdd = value;
					}
				}
			}

			// Token: 0x1700159E RID: 5534
			// (get) Token: 0x060064C2 RID: 25794 RVA: 0x0017717D File Offset: 0x0017537D
			// (set) Token: 0x060064C3 RID: 25795 RVA: 0x00177185 File Offset: 0x00175385
			public bool AllowEdit
			{
				get
				{
					return this.allowEdit;
				}
				set
				{
					if (this.allowEdit != value)
					{
						this.allowEdit = value;
					}
				}
			}

			// Token: 0x1700159F RID: 5535
			// (get) Token: 0x060064C4 RID: 25796 RVA: 0x00177197 File Offset: 0x00175397
			// (set) Token: 0x060064C5 RID: 25797 RVA: 0x0017719F File Offset: 0x0017539F
			public bool AllowRemove
			{
				get
				{
					return this.allowRemove;
				}
				set
				{
					if (this.allowRemove != value)
					{
						this.allowRemove = value;
					}
				}
			}

			// Token: 0x060064C6 RID: 25798 RVA: 0x001771B4 File Offset: 0x001753B4
			public bool UpdatePolicy(CurrencyManager listManager, bool gridReadOnly)
			{
				bool flag = false;
				IBindingList bindingList = ((listManager == null) ? null : (listManager.List as IBindingList));
				if (listManager == null)
				{
					if (!this.allowAdd)
					{
						flag = true;
					}
					this.allowAdd = (this.allowEdit = (this.allowRemove = true));
				}
				else
				{
					if (this.AllowAdd != listManager.AllowAdd && !gridReadOnly)
					{
						flag = true;
					}
					this.AllowAdd = listManager.AllowAdd && !gridReadOnly && bindingList != null && bindingList.SupportsChangeNotification;
					this.AllowEdit = listManager.AllowEdit && !gridReadOnly;
					this.AllowRemove = listManager.AllowRemove && !gridReadOnly && bindingList != null && bindingList.SupportsChangeNotification;
				}
				return flag;
			}

			// Token: 0x040039C2 RID: 14786
			private bool allowAdd = true;

			// Token: 0x040039C3 RID: 14787
			private bool allowEdit = true;

			// Token: 0x040039C4 RID: 14788
			private bool allowRemove = true;
		}
	}
}
