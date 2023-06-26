using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Represents a linear collection of elements in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x0200019D RID: 413
	public class DataGridViewBand : DataGridViewElement, ICloneable, IDisposable
	{
		// Token: 0x06001CBC RID: 7356 RVA: 0x00086968 File Offset: 0x00084B68
		internal DataGridViewBand()
		{
			this.propertyStore = new PropertyStore();
			this.bandIndex = -1;
		}

		/// <summary>Releases the resources associated with the band.</summary>
		// Token: 0x06001CBD RID: 7357 RVA: 0x00086984 File Offset: 0x00084B84
		~DataGridViewBand()
		{
			this.Dispose(false);
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x000869B4 File Offset: 0x00084BB4
		// (set) Token: 0x06001CBF RID: 7359 RVA: 0x000869BC File Offset: 0x00084BBC
		internal int CachedThickness
		{
			get
			{
				return this.cachedThickness;
			}
			set
			{
				this.cachedThickness = value;
			}
		}

		/// <summary>Gets or sets the shortcut menu for the band.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the current <see cref="T:System.Windows.Forms.DataGridViewBand" />. The default is <see langword="null" />.</returns>
		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x000869C5 File Offset: 0x00084BC5
		// (set) Token: 0x06001CC1 RID: 7361 RVA: 0x000869E7 File Offset: 0x00084BE7
		[DefaultValue(null)]
		public virtual ContextMenuStrip ContextMenuStrip
		{
			get
			{
				if (this.bandIsRow)
				{
					return ((DataGridViewRow)this).GetContextMenuStrip(this.Index);
				}
				return this.ContextMenuStripInternal;
			}
			set
			{
				this.ContextMenuStripInternal = value;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x000869F0 File Offset: 0x00084BF0
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x00086A08 File Offset: 0x00084C08
		internal ContextMenuStrip ContextMenuStripInternal
		{
			get
			{
				return (ContextMenuStrip)this.Properties.GetObject(DataGridViewBand.PropContextMenuStrip);
			}
			set
			{
				ContextMenuStrip contextMenuStrip = (ContextMenuStrip)this.Properties.GetObject(DataGridViewBand.PropContextMenuStrip);
				if (contextMenuStrip != value)
				{
					EventHandler eventHandler = new EventHandler(this.DetachContextMenuStrip);
					if (contextMenuStrip != null)
					{
						contextMenuStrip.Disposed -= eventHandler;
					}
					this.Properties.SetObject(DataGridViewBand.PropContextMenuStrip, value);
					if (value != null)
					{
						value.Disposed += eventHandler;
					}
					if (base.DataGridView != null)
					{
						base.DataGridView.OnBandContextMenuStripChanged(this);
					}
				}
			}
		}

		/// <summary>Gets or sets the default cell style of the band.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> associated with the <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x00086A78 File Offset: 0x00084C78
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x00086AD0 File Offset: 0x00084CD0
		[Browsable(false)]
		public virtual DataGridViewCellStyle DefaultCellStyle
		{
			get
			{
				DataGridViewCellStyle dataGridViewCellStyle = (DataGridViewCellStyle)this.Properties.GetObject(DataGridViewBand.PropDefaultCellStyle);
				if (dataGridViewCellStyle == null)
				{
					dataGridViewCellStyle = new DataGridViewCellStyle();
					dataGridViewCellStyle.AddScope(base.DataGridView, this.bandIsRow ? DataGridViewCellStyleScopes.Row : DataGridViewCellStyleScopes.Column);
					this.Properties.SetObject(DataGridViewBand.PropDefaultCellStyle, dataGridViewCellStyle);
				}
				return dataGridViewCellStyle;
			}
			set
			{
				DataGridViewCellStyle dataGridViewCellStyle = null;
				if (this.HasDefaultCellStyle)
				{
					dataGridViewCellStyle = this.DefaultCellStyle;
					dataGridViewCellStyle.RemoveScope(this.bandIsRow ? DataGridViewCellStyleScopes.Row : DataGridViewCellStyleScopes.Column);
				}
				if (value != null || this.Properties.ContainsObject(DataGridViewBand.PropDefaultCellStyle))
				{
					if (value != null)
					{
						value.AddScope(base.DataGridView, this.bandIsRow ? DataGridViewCellStyleScopes.Row : DataGridViewCellStyleScopes.Column);
					}
					this.Properties.SetObject(DataGridViewBand.PropDefaultCellStyle, value);
				}
				if (((dataGridViewCellStyle != null && value == null) || (dataGridViewCellStyle == null && value != null) || (dataGridViewCellStyle != null && value != null && !dataGridViewCellStyle.Equals(this.DefaultCellStyle))) && base.DataGridView != null)
				{
					base.DataGridView.OnBandDefaultCellStyleChanged(this);
				}
			}
		}

		/// <summary>Gets or sets the run-time type of the default header cell.</summary>
		/// <returns>A <see cref="T:System.Type" /> that describes the run-time class of the object used as the default header cell.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is not a <see cref="T:System.Type" /> representing <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" /> or a derived type.</exception>
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00086B78 File Offset: 0x00084D78
		// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x00086BC8 File Offset: 0x00084DC8
		[Browsable(false)]
		public Type DefaultHeaderCellType
		{
			get
			{
				Type type = (Type)this.Properties.GetObject(DataGridViewBand.PropDefaultHeaderCellType);
				if (type == null)
				{
					if (this.bandIsRow)
					{
						type = typeof(DataGridViewRowHeaderCell);
					}
					else
					{
						type = typeof(DataGridViewColumnHeaderCell);
					}
				}
				return type;
			}
			set
			{
				if (!(value != null) && !this.Properties.ContainsObject(DataGridViewBand.PropDefaultHeaderCellType))
				{
					return;
				}
				if (Type.GetType("System.Windows.Forms.DataGridViewHeaderCell").IsAssignableFrom(value))
				{
					this.Properties.SetObject(DataGridViewBand.PropDefaultHeaderCellType, value);
					return;
				}
				throw new ArgumentException(SR.GetString("DataGridView_WrongType", new object[] { "DefaultHeaderCellType", "System.Windows.Forms.DataGridViewHeaderCell" }));
			}
		}

		/// <summary>Gets a value indicating whether the band is currently displayed onscreen.</summary>
		/// <returns>
		///   <see langword="true" /> if the band is currently onscreen; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x00086C3C File Offset: 0x00084E3C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Displayed
		{
			get
			{
				return (this.State & DataGridViewElementStates.Displayed) > DataGridViewElementStates.None;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x00086C56 File Offset: 0x00084E56
		internal bool DisplayedInternal
		{
			set
			{
				if (value)
				{
					base.StateInternal = this.State | DataGridViewElementStates.Displayed;
				}
				else
				{
					base.StateInternal = this.State & ~DataGridViewElementStates.Displayed;
				}
				if (base.DataGridView != null)
				{
					this.OnStateChanged(DataGridViewElementStates.Displayed);
				}
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x00086C8C File Offset: 0x00084E8C
		// (set) Token: 0x06001CCB RID: 7371 RVA: 0x00086CB4 File Offset: 0x00084EB4
		internal int DividerThickness
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(DataGridViewBand.PropDividerThickness, out flag);
				if (!flag)
				{
					return 0;
				}
				return integer;
			}
			set
			{
				if (value < 0)
				{
					if (this.bandIsRow)
					{
						throw new ArgumentOutOfRangeException("DividerHeight", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"DividerHeight",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					throw new ArgumentOutOfRangeException("DividerWidth", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"DividerWidth",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				else
				{
					if (value <= 65536)
					{
						if (value != this.DividerThickness)
						{
							this.Properties.SetInteger(DataGridViewBand.PropDividerThickness, value);
							if (base.DataGridView != null)
							{
								base.DataGridView.OnBandDividerThicknessChanged(this);
							}
						}
						return;
					}
					if (this.bandIsRow)
					{
						throw new ArgumentOutOfRangeException("DividerHeight", SR.GetString("InvalidHighBoundArgumentEx", new object[]
						{
							"DividerHeight",
							value.ToString(CultureInfo.CurrentCulture),
							65536.ToString(CultureInfo.CurrentCulture)
						}));
					}
					throw new ArgumentOutOfRangeException("DividerWidth", SR.GetString("InvalidHighBoundArgumentEx", new object[]
					{
						"DividerWidth",
						value.ToString(CultureInfo.CurrentCulture),
						65536.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the band will move when a user scrolls through the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the band cannot be scrolled from view; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x00086E25 File Offset: 0x00085025
		// (set) Token: 0x06001CCD RID: 7373 RVA: 0x00086E32 File Offset: 0x00085032
		[DefaultValue(false)]
		public virtual bool Frozen
		{
			get
			{
				return (this.State & DataGridViewElementStates.Frozen) > DataGridViewElementStates.None;
			}
			set
			{
				if ((this.State & DataGridViewElementStates.Frozen) > DataGridViewElementStates.None != value)
				{
					this.OnStateChanging(DataGridViewElementStates.Frozen);
					if (value)
					{
						base.StateInternal = this.State | DataGridViewElementStates.Frozen;
					}
					else
					{
						base.StateInternal = this.State & ~DataGridViewElementStates.Frozen;
					}
					this.OnStateChanged(DataGridViewElementStates.Frozen);
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewBand.DefaultCellStyle" /> property has been set.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.DataGridViewBand.DefaultCellStyle" /> property has been set; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x00086E72 File Offset: 0x00085072
		[Browsable(false)]
		public bool HasDefaultCellStyle
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewBand.PropDefaultCellStyle) && this.Properties.GetObject(DataGridViewBand.PropDefaultCellStyle) != null;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x00086E9B File Offset: 0x0008509B
		internal bool HasDefaultHeaderCellType
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewBand.PropDefaultHeaderCellType) && this.Properties.GetObject(DataGridViewBand.PropDefaultHeaderCellType) != null;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x00086EC4 File Offset: 0x000850C4
		internal bool HasHeaderCell
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewBand.PropHeaderCell) && this.Properties.GetObject(DataGridViewBand.PropHeaderCell) != null;
			}
		}

		/// <summary>Gets or sets the header cell of the <see cref="T:System.Windows.Forms.DataGridViewBand" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" /> representing the header cell of the <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is not a <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" /> and this <see cref="T:System.Windows.Forms.DataGridViewBand" /> instance is of type <see cref="T:System.Windows.Forms.DataGridViewRow" />.  
		///  -or-  
		///  The specified value when setting this property is not a <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> and this <see cref="T:System.Windows.Forms.DataGridViewBand" /> instance is of type <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</exception>
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x00086EF0 File Offset: 0x000850F0
		// (set) Token: 0x06001CD2 RID: 7378 RVA: 0x00086FB0 File Offset: 0x000851B0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected DataGridViewHeaderCell HeaderCellCore
		{
			get
			{
				DataGridViewHeaderCell dataGridViewHeaderCell = (DataGridViewHeaderCell)this.Properties.GetObject(DataGridViewBand.PropHeaderCell);
				if (dataGridViewHeaderCell == null)
				{
					Type defaultHeaderCellType = this.DefaultHeaderCellType;
					dataGridViewHeaderCell = (DataGridViewHeaderCell)SecurityUtils.SecureCreateInstance(defaultHeaderCellType);
					dataGridViewHeaderCell.DataGridViewInternal = base.DataGridView;
					if (this.bandIsRow)
					{
						dataGridViewHeaderCell.OwningRowInternal = (DataGridViewRow)this;
						this.Properties.SetObject(DataGridViewBand.PropHeaderCell, dataGridViewHeaderCell);
					}
					else
					{
						DataGridViewColumn dataGridViewColumn = this as DataGridViewColumn;
						dataGridViewHeaderCell.OwningColumnInternal = dataGridViewColumn;
						this.Properties.SetObject(DataGridViewBand.PropHeaderCell, dataGridViewHeaderCell);
						if (base.DataGridView != null && base.DataGridView.SortedColumn == dataGridViewColumn)
						{
							DataGridViewColumnHeaderCell dataGridViewColumnHeaderCell = dataGridViewHeaderCell as DataGridViewColumnHeaderCell;
							dataGridViewColumnHeaderCell.SortGlyphDirection = base.DataGridView.SortOrder;
						}
					}
				}
				return dataGridViewHeaderCell;
			}
			set
			{
				DataGridViewHeaderCell dataGridViewHeaderCell = (DataGridViewHeaderCell)this.Properties.GetObject(DataGridViewBand.PropHeaderCell);
				if (value != null || this.Properties.ContainsObject(DataGridViewBand.PropHeaderCell))
				{
					if (dataGridViewHeaderCell != null)
					{
						dataGridViewHeaderCell.DataGridViewInternal = null;
						if (this.bandIsRow)
						{
							dataGridViewHeaderCell.OwningRowInternal = null;
						}
						else
						{
							dataGridViewHeaderCell.OwningColumnInternal = null;
							((DataGridViewColumnHeaderCell)dataGridViewHeaderCell).SortGlyphDirectionInternal = SortOrder.None;
						}
					}
					if (value != null)
					{
						if (this.bandIsRow)
						{
							if (!(value is DataGridViewRowHeaderCell))
							{
								throw new ArgumentException(SR.GetString("DataGridView_WrongType", new object[] { "HeaderCell", "System.Windows.Forms.DataGridViewRowHeaderCell" }));
							}
							if (value.OwningRow != null)
							{
								value.OwningRow.HeaderCell = null;
							}
							value.OwningRowInternal = (DataGridViewRow)this;
						}
						else
						{
							if (!(value is DataGridViewColumnHeaderCell))
							{
								throw new ArgumentException(SR.GetString("DataGridView_WrongType", new object[] { "HeaderCell", "System.Windows.Forms.DataGridViewColumnHeaderCell" }));
							}
							if (value.OwningColumn != null)
							{
								value.OwningColumn.HeaderCell = null;
							}
							value.OwningColumnInternal = (DataGridViewColumn)this;
						}
						value.DataGridViewInternal = base.DataGridView;
					}
					this.Properties.SetObject(DataGridViewBand.PropHeaderCell, value);
				}
				if (((value == null && dataGridViewHeaderCell != null) || (value != null && dataGridViewHeaderCell == null) || (value != null && dataGridViewHeaderCell != null && !dataGridViewHeaderCell.Equals(value))) && base.DataGridView != null)
				{
					base.DataGridView.OnBandHeaderCellChanged(this);
				}
			}
		}

		/// <summary>Gets the relative position of the band within the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
		/// <returns>The zero-based position of the band in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> or <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" /> that it is contained within. The default is -1, indicating that there is no associated <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00087113 File Offset: 0x00085313
		[Browsable(false)]
		public int Index
		{
			get
			{
				return this.bandIndex;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (set) Token: 0x06001CD4 RID: 7380 RVA: 0x0008711B File Offset: 0x0008531B
		internal int IndexInternal
		{
			set
			{
				this.bandIndex = value;
			}
		}

		/// <summary>Gets the cell style in effect for the current band, taking into account style inheritance.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> associated with the <see cref="T:System.Windows.Forms.DataGridViewBand" />. The default is <see langword="null" />.</returns>
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x00015C90 File Offset: 0x00013E90
		[Browsable(false)]
		public virtual DataGridViewCellStyle InheritedStyle
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value indicating whether the band represents a row.</summary>
		/// <returns>
		///   <see langword="true" /> if the band represents a <see cref="T:System.Windows.Forms.DataGridViewRow" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x00087124 File Offset: 0x00085324
		protected bool IsRow
		{
			get
			{
				return this.bandIsRow;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x0008712C File Offset: 0x0008532C
		// (set) Token: 0x06001CD8 RID: 7384 RVA: 0x00087164 File Offset: 0x00085364
		internal int MinimumThickness
		{
			get
			{
				if (this.bandIsRow && this.bandIndex > -1)
				{
					int num;
					int num2;
					this.GetHeightInfo(this.bandIndex, out num, out num2);
					return num2;
				}
				return this.minimumThickness;
			}
			set
			{
				if (this.minimumThickness != value)
				{
					if (value < 2)
					{
						if (this.bandIsRow)
						{
							throw new ArgumentOutOfRangeException("MinimumHeight", value, SR.GetString("DataGridViewBand_MinimumHeightSmallerThanOne", new object[] { 2.ToString(CultureInfo.CurrentCulture) }));
						}
						throw new ArgumentOutOfRangeException("MinimumWidth", value, SR.GetString("DataGridViewBand_MinimumWidthSmallerThanOne", new object[] { 2.ToString(CultureInfo.CurrentCulture) }));
					}
					else
					{
						if (this.Thickness < value)
						{
							if (base.DataGridView != null && !this.bandIsRow)
							{
								base.DataGridView.OnColumnMinimumWidthChanging((DataGridViewColumn)this, value);
							}
							this.Thickness = value;
						}
						this.minimumThickness = value;
						if (base.DataGridView != null)
						{
							base.DataGridView.OnBandMinimumThicknessChanged(this);
						}
					}
				}
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001CD9 RID: 7385 RVA: 0x0008723A File Offset: 0x0008543A
		internal PropertyStore Properties
		{
			get
			{
				return this.propertyStore;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can edit the band's cells.</summary>
		/// <returns>
		///   <see langword="true" /> if the user cannot edit the band's cells; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, this <see cref="T:System.Windows.Forms.DataGridViewBand" /> instance is a shared <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x00087242 File Offset: 0x00085442
		// (set) Token: 0x06001CDB RID: 7387 RVA: 0x00087268 File Offset: 0x00085468
		[DefaultValue(false)]
		public virtual bool ReadOnly
		{
			get
			{
				return (this.State & DataGridViewElementStates.ReadOnly) != DataGridViewElementStates.None || (base.DataGridView != null && base.DataGridView.ReadOnly);
			}
			set
			{
				if (base.DataGridView == null)
				{
					if ((this.State & DataGridViewElementStates.ReadOnly) > DataGridViewElementStates.None != value)
					{
						if (value)
						{
							if (this.bandIsRow)
							{
								foreach (object obj in ((DataGridViewRow)this).Cells)
								{
									DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
									if (dataGridViewCell.ReadOnly)
									{
										dataGridViewCell.ReadOnlyInternal = false;
									}
								}
							}
							base.StateInternal = this.State | DataGridViewElementStates.ReadOnly;
							return;
						}
						base.StateInternal = this.State & ~DataGridViewElementStates.ReadOnly;
					}
					return;
				}
				if (base.DataGridView.ReadOnly)
				{
					return;
				}
				if (!this.bandIsRow)
				{
					this.OnStateChanging(DataGridViewElementStates.ReadOnly);
					base.DataGridView.SetReadOnlyColumnCore(this.bandIndex, value);
					return;
				}
				if (this.bandIndex == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[] { "ReadOnly" }));
				}
				this.OnStateChanging(DataGridViewElementStates.ReadOnly);
				base.DataGridView.SetReadOnlyRowCore(this.bandIndex, value);
			}
		}

		// Token: 0x1700063A RID: 1594
		// (set) Token: 0x06001CDC RID: 7388 RVA: 0x00087380 File Offset: 0x00085580
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
				this.OnStateChanged(DataGridViewElementStates.ReadOnly);
			}
		}

		/// <summary>Gets or sets a value indicating whether the band can be resized in the user interface (UI).</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewTriState" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewTriState.True" />.</returns>
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x000873AB File Offset: 0x000855AB
		// (set) Token: 0x06001CDE RID: 7390 RVA: 0x000873E0 File Offset: 0x000855E0
		[Browsable(true)]
		public virtual DataGridViewTriState Resizable
		{
			get
			{
				if ((this.State & DataGridViewElementStates.ResizableSet) != DataGridViewElementStates.None)
				{
					if ((this.State & DataGridViewElementStates.Resizable) == DataGridViewElementStates.None)
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
					if (!base.DataGridView.AllowUserToResizeColumns)
					{
						return DataGridViewTriState.False;
					}
					return DataGridViewTriState.True;
				}
			}
			set
			{
				DataGridViewTriState resizable = this.Resizable;
				if (value == DataGridViewTriState.NotSet)
				{
					base.StateInternal = this.State & ~DataGridViewElementStates.ResizableSet;
				}
				else
				{
					base.StateInternal = this.State | DataGridViewElementStates.ResizableSet;
					if ((this.State & DataGridViewElementStates.Resizable) > DataGridViewElementStates.None != (value == DataGridViewTriState.True))
					{
						if (value == DataGridViewTriState.True)
						{
							base.StateInternal = this.State | DataGridViewElementStates.Resizable;
						}
						else
						{
							base.StateInternal = this.State & ~DataGridViewElementStates.Resizable;
						}
					}
				}
				if (resizable != this.Resizable)
				{
					this.OnStateChanged(DataGridViewElementStates.Resizable);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the band is in a selected user interface (UI) state.</summary>
		/// <returns>
		///   <see langword="true" /> if the band is selected; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is <see langword="true" />, but the band has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		///  -or-  
		///  This property is being set on a shared <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x0008745B File Offset: 0x0008565B
		// (set) Token: 0x06001CE0 RID: 7392 RVA: 0x0008746C File Offset: 0x0008566C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Selected
		{
			get
			{
				return (this.State & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			}
			set
			{
				if (base.DataGridView != null)
				{
					if (this.bandIsRow)
					{
						if (this.bandIndex == -1)
						{
							throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[] { "Selected" }));
						}
						if (base.DataGridView.SelectionMode == DataGridViewSelectionMode.FullRowSelect || base.DataGridView.SelectionMode == DataGridViewSelectionMode.RowHeaderSelect)
						{
							base.DataGridView.SetSelectedRowCoreInternal(this.bandIndex, value);
							return;
						}
					}
					else if (base.DataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || base.DataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect)
					{
						base.DataGridView.SetSelectedColumnCoreInternal(this.bandIndex, value);
						return;
					}
				}
				else if (value)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewBand_CannotSelect"));
				}
			}
		}

		// Token: 0x1700063D RID: 1597
		// (set) Token: 0x06001CE1 RID: 7393 RVA: 0x00087524 File Offset: 0x00085724
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
					this.OnStateChanged(DataGridViewElementStates.Selected);
				}
			}
		}

		/// <summary>Gets or sets the object that contains data to associate with the band.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains information associated with the band. The default is <see langword="null" />.</returns>
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x00087559 File Offset: 0x00085759
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x0008756B File Offset: 0x0008576B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object Tag
		{
			get
			{
				return this.Properties.GetObject(DataGridViewBand.PropUserData);
			}
			set
			{
				if (value != null || this.Properties.ContainsObject(DataGridViewBand.PropUserData))
				{
					this.Properties.SetObject(DataGridViewBand.PropUserData, value);
				}
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x00087594 File Offset: 0x00085794
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x000875CC File Offset: 0x000857CC
		internal int Thickness
		{
			get
			{
				if (this.bandIsRow && this.bandIndex > -1)
				{
					int num;
					int num2;
					this.GetHeightInfo(this.bandIndex, out num, out num2);
					return num;
				}
				return this.thickness;
			}
			set
			{
				int num = this.MinimumThickness;
				if (value < num)
				{
					value = num;
				}
				if (value <= 65536)
				{
					bool flag = true;
					if (this.bandIsRow)
					{
						if (base.DataGridView != null && base.DataGridView.AutoSizeRowsMode != DataGridViewAutoSizeRowsMode.None)
						{
							this.cachedThickness = value;
							flag = false;
						}
					}
					else
					{
						DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this;
						DataGridViewAutoSizeColumnMode inheritedAutoSizeMode = dataGridViewColumn.InheritedAutoSizeMode;
						if (inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.Fill && inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.None && inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.NotSet)
						{
							this.cachedThickness = value;
							flag = false;
						}
						else if (inheritedAutoSizeMode == DataGridViewAutoSizeColumnMode.Fill && base.DataGridView != null && dataGridViewColumn.Visible)
						{
							IntPtr handle = base.DataGridView.Handle;
							base.DataGridView.AdjustFillingColumn(dataGridViewColumn, value);
							flag = false;
						}
					}
					if (flag && this.thickness != value)
					{
						if (base.DataGridView != null)
						{
							base.DataGridView.OnBandThicknessChanging();
						}
						this.ThicknessInternal = value;
					}
					return;
				}
				if (this.bandIsRow)
				{
					throw new ArgumentOutOfRangeException("Height", SR.GetString("InvalidHighBoundArgumentEx", new object[]
					{
						"Height",
						value.ToString(CultureInfo.CurrentCulture),
						65536.ToString(CultureInfo.CurrentCulture)
					}));
				}
				throw new ArgumentOutOfRangeException("Width", SR.GetString("InvalidHighBoundArgumentEx", new object[]
				{
					"Width",
					value.ToString(CultureInfo.CurrentCulture),
					65536.ToString(CultureInfo.CurrentCulture)
				}));
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x00087733 File Offset: 0x00085933
		// (set) Token: 0x06001CE7 RID: 7399 RVA: 0x0008773B File Offset: 0x0008593B
		internal int ThicknessInternal
		{
			get
			{
				return this.thickness;
			}
			set
			{
				this.thickness = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnBandThicknessChanged(this);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the band is visible to the user.</summary>
		/// <returns>
		///   <see langword="true" /> if the band is visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is <see langword="false" /> and the band is the row for new records.</exception>
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x00087758 File Offset: 0x00085958
		// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x00087768 File Offset: 0x00085968
		[DefaultValue(true)]
		public virtual bool Visible
		{
			get
			{
				return (this.State & DataGridViewElementStates.Visible) > DataGridViewElementStates.None;
			}
			set
			{
				if ((this.State & DataGridViewElementStates.Visible) > DataGridViewElementStates.None != value)
				{
					if (base.DataGridView != null && this.bandIsRow && base.DataGridView.NewRowIndex != -1 && base.DataGridView.NewRowIndex == this.bandIndex && !value)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewBand_NewRowCannotBeInvisible"));
					}
					this.OnStateChanging(DataGridViewElementStates.Visible);
					if (value)
					{
						base.StateInternal = this.State | DataGridViewElementStates.Visible;
					}
					else
					{
						base.StateInternal = this.State & ~DataGridViewElementStates.Visible;
					}
					this.OnStateChanged(DataGridViewElementStates.Visible);
				}
			}
		}

		/// <summary>Creates an exact copy of this band.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
		// Token: 0x06001CEA RID: 7402 RVA: 0x000877FC File Offset: 0x000859FC
		public virtual object Clone()
		{
			DataGridViewBand dataGridViewBand = (DataGridViewBand)Activator.CreateInstance(base.GetType());
			if (dataGridViewBand != null)
			{
				this.CloneInternal(dataGridViewBand);
			}
			return dataGridViewBand;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x00087828 File Offset: 0x00085A28
		internal void CloneInternal(DataGridViewBand dataGridViewBand)
		{
			dataGridViewBand.propertyStore = new PropertyStore();
			dataGridViewBand.bandIndex = -1;
			dataGridViewBand.bandIsRow = this.bandIsRow;
			if (!this.bandIsRow || this.bandIndex >= 0 || base.DataGridView == null)
			{
				dataGridViewBand.StateInternal = this.State & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Selected);
			}
			dataGridViewBand.thickness = this.Thickness;
			dataGridViewBand.MinimumThickness = this.MinimumThickness;
			dataGridViewBand.cachedThickness = this.CachedThickness;
			dataGridViewBand.DividerThickness = this.DividerThickness;
			dataGridViewBand.Tag = this.Tag;
			if (this.HasDefaultCellStyle)
			{
				dataGridViewBand.DefaultCellStyle = new DataGridViewCellStyle(this.DefaultCellStyle);
			}
			if (this.HasDefaultHeaderCellType)
			{
				dataGridViewBand.DefaultHeaderCellType = this.DefaultHeaderCellType;
			}
			if (this.ContextMenuStripInternal != null)
			{
				dataGridViewBand.ContextMenuStrip = this.ContextMenuStripInternal.Clone();
			}
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x000878FD File Offset: 0x00085AFD
		private void DetachContextMenuStrip(object sender, EventArgs e)
		{
			this.ContextMenuStripInternal = null;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.DataGridViewBand" />.</summary>
		// Token: 0x06001CED RID: 7405 RVA: 0x00087906 File Offset: 0x00085B06
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.DataGridViewBand" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001CEE RID: 7406 RVA: 0x00087918 File Offset: 0x00085B18
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

		// Token: 0x06001CEF RID: 7407 RVA: 0x00087944 File Offset: 0x00085B44
		internal void GetHeightInfo(int rowIndex, out int height, out int minimumHeight)
		{
			if (base.DataGridView != null && (base.DataGridView.VirtualMode || base.DataGridView.DataSource != null) && base.DataGridView.AutoSizeRowsMode == DataGridViewAutoSizeRowsMode.None)
			{
				DataGridViewRowHeightInfoNeededEventArgs dataGridViewRowHeightInfoNeededEventArgs = base.DataGridView.OnRowHeightInfoNeeded(rowIndex, this.thickness, this.minimumThickness);
				height = dataGridViewRowHeightInfoNeededEventArgs.Height;
				minimumHeight = dataGridViewRowHeightInfoNeededEventArgs.MinimumHeight;
				return;
			}
			height = this.thickness;
			minimumHeight = this.minimumThickness;
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x000879BC File Offset: 0x00085BBC
		internal void OnStateChanged(DataGridViewElementStates elementState)
		{
			if (base.DataGridView != null)
			{
				if (this.bandIsRow)
				{
					base.DataGridView.Rows.InvalidateCachedRowCount(elementState);
					base.DataGridView.Rows.InvalidateCachedRowsHeight(elementState);
					if (this.bandIndex != -1)
					{
						base.DataGridView.OnDataGridViewElementStateChanged(this, -1, elementState);
						return;
					}
				}
				else
				{
					base.DataGridView.Columns.InvalidateCachedColumnCount(elementState);
					base.DataGridView.Columns.InvalidateCachedColumnsWidth(elementState);
					base.DataGridView.OnDataGridViewElementStateChanged(this, -1, elementState);
				}
			}
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x00087A43 File Offset: 0x00085C43
		private void OnStateChanging(DataGridViewElementStates elementState)
		{
			if (base.DataGridView != null)
			{
				if (this.bandIsRow)
				{
					if (this.bandIndex != -1)
					{
						base.DataGridView.OnDataGridViewElementStateChanging(this, -1, elementState);
						return;
					}
				}
				else
				{
					base.DataGridView.OnDataGridViewElementStateChanging(this, -1, elementState);
				}
			}
		}

		/// <summary>Called when the band is associated with a different <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x06001CF2 RID: 7410 RVA: 0x00087A7C File Offset: 0x00085C7C
		protected override void OnDataGridViewChanged()
		{
			if (this.HasDefaultCellStyle)
			{
				if (base.DataGridView == null)
				{
					this.DefaultCellStyle.RemoveScope(this.bandIsRow ? DataGridViewCellStyleScopes.Row : DataGridViewCellStyleScopes.Column);
				}
				else
				{
					this.DefaultCellStyle.AddScope(base.DataGridView, this.bandIsRow ? DataGridViewCellStyleScopes.Row : DataGridViewCellStyleScopes.Column);
				}
			}
			base.OnDataGridViewChanged();
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x00087AD8 File Offset: 0x00085CD8
		private bool ShouldSerializeDefaultHeaderCellType()
		{
			Type type = (Type)this.Properties.GetObject(DataGridViewBand.PropDefaultHeaderCellType);
			return type != null;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x00087B02 File Offset: 0x00085D02
		internal bool ShouldSerializeResizable()
		{
			return (this.State & DataGridViewElementStates.ResizableSet) > DataGridViewElementStates.None;
		}

		/// <summary>Returns a string that represents the current band.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
		// Token: 0x06001CF5 RID: 7413 RVA: 0x00087B10 File Offset: 0x00085D10
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(36);
			stringBuilder.Append("DataGridViewBand { Index=");
			stringBuilder.Append(this.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000C66 RID: 3174
		private static readonly int PropContextMenuStrip = PropertyStore.CreateKey();

		// Token: 0x04000C67 RID: 3175
		private static readonly int PropDefaultCellStyle = PropertyStore.CreateKey();

		// Token: 0x04000C68 RID: 3176
		private static readonly int PropDefaultHeaderCellType = PropertyStore.CreateKey();

		// Token: 0x04000C69 RID: 3177
		private static readonly int PropDividerThickness = PropertyStore.CreateKey();

		// Token: 0x04000C6A RID: 3178
		private static readonly int PropHeaderCell = PropertyStore.CreateKey();

		// Token: 0x04000C6B RID: 3179
		private static readonly int PropUserData = PropertyStore.CreateKey();

		// Token: 0x04000C6C RID: 3180
		internal const int minBandThickness = 2;

		// Token: 0x04000C6D RID: 3181
		internal const int maxBandThickness = 65536;

		// Token: 0x04000C6E RID: 3182
		private PropertyStore propertyStore;

		// Token: 0x04000C6F RID: 3183
		private int thickness;

		// Token: 0x04000C70 RID: 3184
		private int cachedThickness;

		// Token: 0x04000C71 RID: 3185
		private int minimumThickness;

		// Token: 0x04000C72 RID: 3186
		private int bandIndex;

		// Token: 0x04000C73 RID: 3187
		internal bool bandIsRow;
	}
}
