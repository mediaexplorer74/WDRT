using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;
using Accessibility;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows control to display a list of items.</summary>
	// Token: 0x020002CB RID: 715
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ListBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("SelectedIndexChanged")]
	[DefaultProperty("Items")]
	[DefaultBindingProperty("SelectedValue")]
	[SRDescription("DescriptionListBox")]
	public class ListBox : ListControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListBox" /> class.</summary>
		// Token: 0x06002BD6 RID: 11222 RVA: 0x000C575C File Offset: 0x000C395C
		public ListBox()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, false);
			base.SetState2(2048, true);
			base.SetBounds(0, 0, 120, 96);
			this.requestedHeight = base.Height;
			this.PrepareForDrawing();
		}

		/// <summary>Provides constants for rescaling the control when a DPI change occurs.</summary>
		/// <param name="deviceDpiOld">The DPI value prior to the change.</param>
		/// <param name="deviceDpiNew">The DPI value after the change.</param>
		// Token: 0x06002BD7 RID: 11223 RVA: 0x000C57EC File Offset: 0x000C39EC
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			this.PrepareForDrawing();
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000C57FC File Offset: 0x000C39FC
		private void PrepareForDrawing()
		{
			if (DpiHelper.EnableCheckedListBoxHighDpiImprovements)
			{
				this.scaledListItemStartPosition = base.LogicalToDeviceUnits(1);
				this.scaledListItemBordersHeight = 2 * base.LogicalToDeviceUnits(1);
				this.scaledListItemPaddingBuffer = base.LogicalToDeviceUnits(3);
			}
		}

		/// <summary>Gets or sets the background color for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06002BD9 RID: 11225 RVA: 0x00027DA7 File Offset: 0x00025FA7
		// (set) Token: 0x06002BDA RID: 11226 RVA: 0x00012D84 File Offset: 0x00010F84
		public override Color BackColor
		{
			get
			{
				if (this.ShouldSerializeBackColor())
				{
					return base.BackColor;
				}
				return SystemColors.Window;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The background image of the form.</returns>
		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x06002BDC RID: 11228 RVA: 0x00011884 File Offset: 0x0000FA84
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListBox.BackgroundImage" /> property of the label changes.</summary>
		// Token: 0x140001F6 RID: 502
		// (add) Token: 0x06002BDD RID: 11229 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x06002BDE RID: 11230 RVA: 0x00011896 File Offset: 0x0000FA96
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

		/// <summary>Gets or sets the background image layout for a <see cref="T:System.Windows.Forms.ListBox" /> as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" />. The values are <see langword="Center" />, <see langword="None" />, <see langword="Stretch" />, <see langword="Tile" />, or <see langword="Zoom" />. <see langword="Center" /> is the default value.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified enumeration value does not exist.</exception>
		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x06002BE0 RID: 11232 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListBox.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x140001F7 RID: 503
		// (add) Token: 0x06002BE1 RID: 11233 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x06002BE2 RID: 11234 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>Gets or sets the type of border that is drawn around the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values.</exception>
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06002BE3 RID: 11235 RVA: 0x000C582E File Offset: 0x000C3A2E
		// (set) Token: 0x06002BE4 RID: 11236 RVA: 0x000C5838 File Offset: 0x000C3A38
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		[SRDescription("ListBoxBorderDescr")]
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
				if (value != this.borderStyle)
				{
					this.borderStyle = value;
					base.RecreateHandle();
					this.integralHeightAdjust = true;
					try
					{
						base.Height = this.requestedHeight;
					}
					finally
					{
						this.integralHeightAdjust = false;
					}
				}
			}
		}

		/// <summary>Gets or sets the width of columns in a multicolumn <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>The width, in pixels, of each column in the control. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentException">A value less than zero is assigned to the property.</exception>
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06002BE5 RID: 11237 RVA: 0x000C58B0 File Offset: 0x000C3AB0
		// (set) Token: 0x06002BE6 RID: 11238 RVA: 0x000C58B8 File Offset: 0x000C3AB8
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(0)]
		[SRDescription("ListBoxColumnWidthDescr")]
		public int ColumnWidth
		{
			get
			{
				return this.columnWidth;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"value",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.columnWidth != value)
				{
					this.columnWidth = value;
					if (this.columnWidth == 0)
					{
						base.RecreateHandle();
						return;
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(405, this.columnWidth, 0);
					}
				}
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x000C5944 File Offset: 0x000C3B44
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "LISTBOX";
				createParams.Style |= 2097217;
				if (this.scrollAlwaysVisible)
				{
					createParams.Style |= 4096;
				}
				if (!this.integralHeight)
				{
					createParams.Style |= 256;
				}
				if (this.useTabStops)
				{
					createParams.Style |= 128;
				}
				BorderStyle borderStyle = this.borderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.ExStyle |= 512;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				if (this.multiColumn)
				{
					createParams.Style |= 1049088;
				}
				else if (this.horizontalScrollbar)
				{
					createParams.Style |= 1048576;
				}
				switch (this.selectionMode)
				{
				case SelectionMode.None:
					createParams.Style |= 16384;
					break;
				case SelectionMode.MultiSimple:
					createParams.Style |= 8;
					break;
				case SelectionMode.MultiExtended:
					createParams.Style |= 2048;
					break;
				}
				switch (this.drawMode)
				{
				case DrawMode.OwnerDrawFixed:
					createParams.Style |= 16;
					break;
				case DrawMode.OwnerDrawVariable:
					createParams.Style |= 32;
					break;
				}
				return createParams;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListBox" /> recognizes and expands tab characters when it draws its strings by using the <see cref="P:System.Windows.Forms.ListBox.CustomTabOffsets" /> integer array.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ListBox" /> recognizes and expands tab characters; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x000C5ABF File Offset: 0x000C3CBF
		// (set) Token: 0x06002BE9 RID: 11241 RVA: 0x000C5AC7 File Offset: 0x000C3CC7
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Browsable(false)]
		public bool UseCustomTabOffsets
		{
			get
			{
				return this.useCustomTabOffsets;
			}
			set
			{
				if (this.useCustomTabOffsets != value)
				{
					this.useCustomTabOffsets = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000C5ADF File Offset: 0x000C3CDF
		protected override Size DefaultSize
		{
			get
			{
				return new Size(120, 96);
			}
		}

		/// <summary>Gets or sets the drawing mode for the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DrawMode" /> values representing the mode for drawing the items of the control. The default is <see langword="DrawMode.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a member of the <see cref="T:System.Windows.Forms.DrawMode" /> enumeration.</exception>
		/// <exception cref="T:System.ArgumentException">A multicolumn <see cref="T:System.Windows.Forms.ListBox" /> cannot have a variable-sized height.</exception>
		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06002BEB RID: 11243 RVA: 0x000C5AEA File Offset: 0x000C3CEA
		// (set) Token: 0x06002BEC RID: 11244 RVA: 0x000C5AF4 File Offset: 0x000C3CF4
		[SRCategory("CatBehavior")]
		[DefaultValue(DrawMode.Normal)]
		[SRDescription("ListBoxDrawModeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public virtual DrawMode DrawMode
		{
			get
			{
				return this.drawMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DrawMode));
				}
				if (this.drawMode != value)
				{
					if (this.MultiColumn && value == DrawMode.OwnerDrawVariable)
					{
						throw new ArgumentException(SR.GetString("ListBoxVarHeightMultiCol"), "value");
					}
					this.drawMode = value;
					base.RecreateHandle();
					if (this.drawMode == DrawMode.OwnerDrawVariable)
					{
						LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.DrawMode);
					}
				}
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06002BED RID: 11245 RVA: 0x000C5B7E File Offset: 0x000C3D7E
		internal int FocusedIndex
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (int)(long)base.SendMessage(415, 0, 0);
				}
				return -1;
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x0001A0D6 File Offset: 0x000182D6
		// (set) Token: 0x06002BEF RID: 11247 RVA: 0x000C5B9D File Offset: 0x000C3D9D
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
				if (!this.integralHeight)
				{
					this.RefreshItems();
				}
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x0001300E File Offset: 0x0001120E
		// (set) Token: 0x06002BF1 RID: 11249 RVA: 0x00013024 File Offset: 0x00011224
		public override Color ForeColor
		{
			get
			{
				if (this.ShouldSerializeForeColor())
				{
					return base.ForeColor;
				}
				return SystemColors.WindowText;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Gets or sets the width by which the horizontal scroll bar of a <see cref="T:System.Windows.Forms.ListBox" /> can scroll.</summary>
		/// <returns>The width, in pixels, that the horizontal scroll bar can scroll the control. The default is zero.</returns>
		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x000C5BB4 File Offset: 0x000C3DB4
		// (set) Token: 0x06002BF3 RID: 11251 RVA: 0x000C5BBC File Offset: 0x000C3DBC
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Localizable(true)]
		[SRDescription("ListBoxHorizontalExtentDescr")]
		public int HorizontalExtent
		{
			get
			{
				return this.horizontalExtent;
			}
			set
			{
				if (value != this.horizontalExtent)
				{
					this.horizontalExtent = value;
					this.UpdateHorizontalExtent();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a horizontal scroll bar is displayed in the control.</summary>
		/// <returns>
		///   <see langword="true" /> to display a horizontal scroll bar in the control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x000C5BD4 File Offset: 0x000C3DD4
		// (set) Token: 0x06002BF5 RID: 11253 RVA: 0x000C5BDC File Offset: 0x000C3DDC
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("ListBoxHorizontalScrollbarDescr")]
		public bool HorizontalScrollbar
		{
			get
			{
				return this.horizontalScrollbar;
			}
			set
			{
				if (value != this.horizontalScrollbar)
				{
					this.horizontalScrollbar = value;
					this.RefreshItems();
					if (!this.MultiColumn)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should resize to avoid showing partial items.</summary>
		/// <returns>
		///   <see langword="true" /> if the control resizes so that it does not display partial items; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x000C5C02 File Offset: 0x000C3E02
		// (set) Token: 0x06002BF7 RID: 11255 RVA: 0x000C5C0C File Offset: 0x000C3E0C
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ListBoxIntegralHeightDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public bool IntegralHeight
		{
			get
			{
				return this.integralHeight;
			}
			set
			{
				if (this.integralHeight != value)
				{
					this.integralHeight = value;
					base.RecreateHandle();
					this.integralHeightAdjust = true;
					try
					{
						base.Height = this.requestedHeight;
					}
					finally
					{
						this.integralHeightAdjust = false;
					}
				}
			}
		}

		/// <summary>Gets or sets the height of an item in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>The height, in pixels, of an item in the control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Windows.Forms.ListBox.ItemHeight" /> property was set to less than 0 or more than 255 pixels.</exception>
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x000C5C5C File Offset: 0x000C3E5C
		// (set) Token: 0x06002BF9 RID: 11257 RVA: 0x000C5C80 File Offset: 0x000C3E80
		[SRCategory("CatBehavior")]
		[DefaultValue(13)]
		[Localizable(true)]
		[SRDescription("ListBoxItemHeightDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public virtual int ItemHeight
		{
			get
			{
				if (this.drawMode == DrawMode.OwnerDrawFixed || this.drawMode == DrawMode.OwnerDrawVariable)
				{
					return this.itemHeight;
				}
				return this.GetItemHeight(0);
			}
			set
			{
				if (value < 1 || value > 255)
				{
					throw new ArgumentOutOfRangeException("ItemHeight", SR.GetString("InvalidExBoundArgument", new object[]
					{
						"ItemHeight",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture),
						"256"
					}));
				}
				if (this.itemHeight != value)
				{
					this.itemHeight = value;
					if (this.drawMode == DrawMode.OwnerDrawFixed && base.IsHandleCreated)
					{
						this.BeginUpdate();
						base.SendMessage(416, 0, value);
						if (this.IntegralHeight)
						{
							Size size = base.Size;
							base.Size = new Size(size.Width + 1, size.Height);
							base.Size = size;
						}
						this.EndUpdate();
					}
				}
			}
		}

		/// <summary>Gets the items of the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> representing the items in the <see cref="T:System.Windows.Forms.ListBox" />.</returns>
		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x000C5D50 File Offset: 0x000C3F50
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("ListBoxItemsDescr")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		public ListBox.ObjectCollection Items
		{
			get
			{
				if (this.itemsCollection == null)
				{
					this.itemsCollection = this.CreateItemCollection();
				}
				return this.itemsCollection;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x000C5D6C File Offset: 0x000C3F6C
		internal virtual int MaxItemWidth
		{
			get
			{
				if (this.horizontalExtent > 0)
				{
					return this.horizontalExtent;
				}
				if (this.DrawMode != DrawMode.Normal)
				{
					return -1;
				}
				if (this.maxWidth > -1)
				{
					return this.maxWidth;
				}
				this.maxWidth = this.ComputeMaxItemWidth(this.maxWidth);
				return this.maxWidth;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListBox" /> supports multiple columns.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ListBox" /> supports multiple columns; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">A multicolumn <see cref="T:System.Windows.Forms.ListBox" /> cannot have a variable-sized height.</exception>
		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002BFC RID: 11260 RVA: 0x000C5DBB File Offset: 0x000C3FBB
		// (set) Token: 0x06002BFD RID: 11261 RVA: 0x000C5DC3 File Offset: 0x000C3FC3
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListBoxMultiColumnDescr")]
		public bool MultiColumn
		{
			get
			{
				return this.multiColumn;
			}
			set
			{
				if (this.multiColumn != value)
				{
					if (value && this.drawMode == DrawMode.OwnerDrawVariable)
					{
						throw new ArgumentException(SR.GetString("ListBoxVarHeightMultiCol"), "value");
					}
					this.multiColumn = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets the combined height of all items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>The combined height, in pixels, of all items in the control.</returns>
		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000C5DFC File Offset: 0x000C3FFC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListBoxPreferredHeightDescr")]
		public int PreferredHeight
		{
			get
			{
				int num = 0;
				if (this.drawMode == DrawMode.OwnerDrawVariable)
				{
					if (base.RecreatingHandle || base.GetState(262144))
					{
						num = base.Height;
					}
					else if (this.itemsCollection != null)
					{
						int count = this.itemsCollection.Count;
						for (int i = 0; i < count; i++)
						{
							num += this.GetItemHeight(i);
						}
					}
				}
				else
				{
					int num2 = ((this.itemsCollection == null || this.itemsCollection.Count == 0) ? 1 : this.itemsCollection.Count);
					num = this.GetItemHeight(0) * num2;
				}
				if (this.borderStyle != BorderStyle.None)
				{
					num += SystemInformation.BorderSize.Height * 4 + 3;
				}
				return num;
			}
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000C5EAC File Offset: 0x000C40AC
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			int preferredHeight = this.PreferredHeight;
			if (base.IsHandleCreated)
			{
				int num = this.SizeFromClientSize(new Size(this.MaxItemWidth, preferredHeight)).Width;
				num += SystemInformation.VerticalScrollBarWidth + 4;
				return new Size(num, preferredHeight) + this.Padding.Size;
			}
			return this.DefaultSize;
		}

		/// <summary>Gets or sets a value indicating whether text displayed by the control is displayed from right to left.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values.</returns>
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x000C5F10 File Offset: 0x000C4110
		// (set) Token: 0x06002C01 RID: 11265 RVA: 0x000C5F21 File Offset: 0x000C4121
		public override RightToLeft RightToLeft
		{
			get
			{
				if (!ListBox.RunningOnWin2K)
				{
					return RightToLeft.No;
				}
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x000C5F2A File Offset: 0x000C412A
		private static bool RunningOnWin2K
		{
			get
			{
				if (!ListBox.checkedOS && (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 5))
				{
					ListBox.runningOnWin2K = false;
					ListBox.checkedOS = true;
				}
				return ListBox.runningOnWin2K;
			}
		}

		/// <summary>Gets or sets a value indicating whether the vertical scroll bar is shown at all times.</summary>
		/// <returns>
		///   <see langword="true" /> if the vertical scroll bar should always be displayed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x000C5F63 File Offset: 0x000C4163
		// (set) Token: 0x06002C04 RID: 11268 RVA: 0x000C5F6B File Offset: 0x000C416B
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("ListBoxScrollIsVisibleDescr")]
		public bool ScrollAlwaysVisible
		{
			get
			{
				return this.scrollAlwaysVisible;
			}
			set
			{
				if (this.scrollAlwaysVisible != value)
				{
					this.scrollAlwaysVisible = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ListBox" /> currently enables selection of list items.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="T:System.Windows.Forms.SelectionMode" /> is not <see cref="F:System.Windows.Forms.SelectionMode.None" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x000C5F83 File Offset: 0x000C4183
		protected override bool AllowSelection
		{
			get
			{
				return this.selectionMode > SelectionMode.None;
			}
		}

		/// <summary>Gets or sets the zero-based index of the currently selected item in a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than -1 or greater than or equal to the item count.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.ListBox.SelectionMode" /> property is set to <see langword="None" />.</exception>
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06002C06 RID: 11270 RVA: 0x000C5F90 File Offset: 0x000C4190
		// (set) Token: 0x06002C07 RID: 11271 RVA: 0x000C6008 File Offset: 0x000C4208
		[Browsable(false)]
		[Bindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListBoxSelectedIndexDescr")]
		public override int SelectedIndex
		{
			get
			{
				SelectionMode selectionMode = (this.selectionModeChanging ? this.cachedSelectionMode : this.selectionMode);
				if (selectionMode == SelectionMode.None)
				{
					return -1;
				}
				if (selectionMode == SelectionMode.One && base.IsHandleCreated)
				{
					return (int)(long)base.SendMessage(392, 0, 0);
				}
				if (this.itemsCollection != null && this.SelectedItems.Count > 0)
				{
					return this.Items.IndexOfIdentifier(this.SelectedItems.GetObjectAt(0));
				}
				return -1;
			}
			set
			{
				int num = ((this.itemsCollection == null) ? 0 : this.itemsCollection.Count);
				if (value < -1 || value >= num)
				{
					throw new ArgumentOutOfRangeException("SelectedIndex", SR.GetString("InvalidArgument", new object[]
					{
						"SelectedIndex",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.selectionMode == SelectionMode.None)
				{
					throw new ArgumentException(SR.GetString("ListBoxInvalidSelectionMode"), "SelectedIndex");
				}
				if (this.selectionMode == SelectionMode.One && value != -1)
				{
					int selectedIndex = this.SelectedIndex;
					if (selectedIndex != value)
					{
						if (selectedIndex != -1)
						{
							this.SelectedItems.SetSelected(selectedIndex, false);
						}
						this.SelectedItems.SetSelected(value, true);
						if (base.IsHandleCreated)
						{
							this.NativeSetSelected(value, true);
						}
						this.OnSelectedIndexChanged(EventArgs.Empty);
						return;
					}
				}
				else if (value == -1)
				{
					if (this.SelectedIndex != -1)
					{
						this.ClearSelected();
						return;
					}
				}
				else if (!this.SelectedItems.GetSelected(value))
				{
					this.SelectedItems.SetSelected(value, true);
					if (base.IsHandleCreated)
					{
						this.NativeSetSelected(value, true);
					}
					this.OnSelectedIndexChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets a collection that contains the zero-based indexes of all currently selected items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" /> containing the indexes of the currently selected items in the control. If no items are currently selected, an empty <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" /> is returned.</returns>
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002C08 RID: 11272 RVA: 0x000C6122 File Offset: 0x000C4322
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListBoxSelectedIndicesDescr")]
		public ListBox.SelectedIndexCollection SelectedIndices
		{
			get
			{
				if (this.selectedIndices == null)
				{
					this.selectedIndices = new ListBox.SelectedIndexCollection(this);
				}
				return this.selectedIndices;
			}
		}

		/// <summary>Gets or sets the currently selected item in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>An object that represents the current selection in the control.</returns>
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000C613E File Offset: 0x000C433E
		// (set) Token: 0x06002C0A RID: 11274 RVA: 0x000C615C File Offset: 0x000C435C
		[Browsable(false)]
		[Bindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListBoxSelectedItemDescr")]
		public object SelectedItem
		{
			get
			{
				if (this.SelectedItems.Count > 0)
				{
					return this.SelectedItems[0];
				}
				return null;
			}
			set
			{
				if (this.itemsCollection != null)
				{
					if (value != null)
					{
						int num = this.itemsCollection.IndexOf(value);
						if (num != -1)
						{
							this.SelectedIndex = num;
							return;
						}
					}
					else
					{
						this.SelectedIndex = -1;
					}
				}
			}
		}

		/// <summary>Gets a collection containing the currently selected items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListBox.SelectedObjectCollection" /> containing the currently selected items in the control.</returns>
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06002C0B RID: 11275 RVA: 0x000C6194 File Offset: 0x000C4394
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListBoxSelectedItemsDescr")]
		public ListBox.SelectedObjectCollection SelectedItems
		{
			get
			{
				if (this.selectedItems == null)
				{
					this.selectedItems = new ListBox.SelectedObjectCollection(this);
				}
				return this.selectedItems;
			}
		}

		/// <summary>Gets or sets the method in which items are selected in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.SelectionMode" /> values. The default is <see langword="SelectionMode.One" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.SelectionMode" /> values.</exception>
		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x000C61B0 File Offset: 0x000C43B0
		// (set) Token: 0x06002C0D RID: 11277 RVA: 0x000C61B8 File Offset: 0x000C43B8
		[SRCategory("CatBehavior")]
		[DefaultValue(SelectionMode.One)]
		[SRDescription("ListBoxSelectionModeDescr")]
		public virtual SelectionMode SelectionMode
		{
			get
			{
				return this.selectionMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(SelectionMode));
				}
				if (this.selectionMode != value)
				{
					this.SelectedItems.EnsureUpToDate();
					this.selectionMode = value;
					try
					{
						this.selectionModeChanging = true;
						base.RecreateHandle();
					}
					finally
					{
						this.selectionModeChanging = false;
						this.cachedSelectionMode = this.selectionMode;
						if (base.IsHandleCreated)
						{
							this.NativeUpdateSelection();
						}
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the items in the <see cref="T:System.Windows.Forms.ListBox" /> are sorted alphabetically.</summary>
		/// <returns>
		///   <see langword="true" /> if items in the control are sorted; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x000C6248 File Offset: 0x000C4448
		// (set) Token: 0x06002C0F RID: 11279 RVA: 0x000C6250 File Offset: 0x000C4450
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListBoxSortedDescr")]
		public bool Sorted
		{
			get
			{
				return this.sorted;
			}
			set
			{
				if (this.sorted != value)
				{
					this.sorted = value;
					if (this.sorted && this.itemsCollection != null && this.itemsCollection.Count >= 1)
					{
						this.Sort();
					}
				}
			}
		}

		/// <summary>Gets or searches for the text of the currently selected item in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>The text of the currently selected item in the control.</returns>
		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000C6286 File Offset: 0x000C4486
		// (set) Token: 0x06002C11 RID: 11281 RVA: 0x000C62C8 File Offset: 0x000C44C8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				if (this.SelectionMode == SelectionMode.None || this.SelectedItem == null)
				{
					return base.Text;
				}
				if (base.FormattingEnabled)
				{
					return base.GetItemText(this.SelectedItem);
				}
				return base.FilterItemOnProperty(this.SelectedItem).ToString();
			}
			set
			{
				base.Text = value;
				if (this.SelectionMode != SelectionMode.None && value != null && (this.SelectedItem == null || !value.Equals(base.GetItemText(this.SelectedItem))))
				{
					int count = this.Items.Count;
					for (int i = 0; i < count; i++)
					{
						if (string.Compare(value, base.GetItemText(this.Items[i]), true, CultureInfo.CurrentCulture) == 0)
						{
							this.SelectedIndex = i;
							return;
						}
					}
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListBox.Text" /> property is changed.</summary>
		// Token: 0x140001F8 RID: 504
		// (add) Token: 0x06002C12 RID: 11282 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06002C13 RID: 11283 RVA: 0x0004659A File Offset: 0x0004479A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
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

		/// <summary>Gets or sets the index of the first visible item in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>The zero-based index of the first visible item in the control.</returns>
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000C6343 File Offset: 0x000C4543
		// (set) Token: 0x06002C15 RID: 11285 RVA: 0x000C6367 File Offset: 0x000C4567
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListBoxTopIndexDescr")]
		public int TopIndex
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (int)(long)base.SendMessage(398, 0, 0);
				}
				return this.topIndex;
			}
			set
			{
				if (base.IsHandleCreated)
				{
					base.SendMessage(407, value, 0);
					return;
				}
				this.topIndex = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListBox" /> can recognize and expand tab characters when drawing its strings.</summary>
		/// <returns>
		///   <see langword="true" /> if the control can expand tab characters; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x000C6387 File Offset: 0x000C4587
		// (set) Token: 0x06002C17 RID: 11287 RVA: 0x000C638F File Offset: 0x000C458F
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("ListBoxUseTabStopsDescr")]
		public bool UseTabStops
		{
			get
			{
				return this.useTabStops;
			}
			set
			{
				if (this.useTabStops != value)
				{
					this.useTabStops = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets the width of the tabs between the items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>A collection of integers representing the tab widths.</returns>
		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002C18 RID: 11288 RVA: 0x000C63A7 File Offset: 0x000C45A7
		[SRCategory("CatBehavior")]
		[SRDescription("ListBoxCustomTabOffsetsDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public ListBox.IntegerCollection CustomTabOffsets
		{
			get
			{
				if (this.customTabOffsets == null)
				{
					this.customTabOffsets = new ListBox.IntegerCollection(this);
				}
				return this.customTabOffsets;
			}
		}

		/// <summary>This member is obsolete, and there is no replacement.</summary>
		/// <param name="value">An array of objects.</param>
		// Token: 0x06002C19 RID: 11289 RVA: 0x000C63C4 File Offset: 0x000C45C4
		[Obsolete("This method has been deprecated.  There is no replacement.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void AddItemsCore(object[] value)
		{
			if (value == null || value.Length == 0)
			{
				return;
			}
			this.Items.AddRangeInternal(value);
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ListBox" /> control is clicked.</summary>
		// Token: 0x140001F9 RID: 505
		// (add) Token: 0x06002C1A RID: 11290 RVA: 0x00012FD4 File Offset: 0x000111D4
		// (remove) Token: 0x06002C1B RID: 11291 RVA: 0x00012FDD File Offset: 0x000111DD
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.ListBox" /> control with the mouse pointer.</summary>
		// Token: 0x140001FA RID: 506
		// (add) Token: 0x06002C1C RID: 11292 RVA: 0x00012FE6 File Offset: 0x000111E6
		// (remove) Token: 0x06002C1D RID: 11293 RVA: 0x00012FEF File Offset: 0x000111EF
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event MouseEventHandler MouseClick
		{
			add
			{
				base.MouseClick += value;
			}
			remove
			{
				base.MouseClick -= value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x06002C1F RID: 11295 RVA: 0x0001344A File Offset: 0x0001164A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ListBox.Padding" /> property changes.</summary>
		// Token: 0x140001FB RID: 507
		// (add) Token: 0x06002C20 RID: 11296 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x06002C21 RID: 11297 RVA: 0x0001345C File Offset: 0x0001165C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ListBox" /> control is painted.</summary>
		// Token: 0x140001FC RID: 508
		// (add) Token: 0x06002C22 RID: 11298 RVA: 0x00013D73 File Offset: 0x00011F73
		// (remove) Token: 0x06002C23 RID: 11299 RVA: 0x00013D7C File Offset: 0x00011F7C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}

		/// <summary>Occurs when a visual aspect of an owner-drawn <see cref="T:System.Windows.Forms.ListBox" /> changes.</summary>
		// Token: 0x140001FD RID: 509
		// (add) Token: 0x06002C24 RID: 11300 RVA: 0x000C63EB File Offset: 0x000C45EB
		// (remove) Token: 0x06002C25 RID: 11301 RVA: 0x000C63FE File Offset: 0x000C45FE
		[SRCategory("CatBehavior")]
		[SRDescription("drawItemEventDescr")]
		public event DrawItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(ListBox.EVENT_DRAWITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListBox.EVENT_DRAWITEM, value);
			}
		}

		/// <summary>Occurs when an owner-drawn <see cref="T:System.Windows.Forms.ListBox" /> is created and the sizes of the list items are determined.</summary>
		// Token: 0x140001FE RID: 510
		// (add) Token: 0x06002C26 RID: 11302 RVA: 0x000C6411 File Offset: 0x000C4611
		// (remove) Token: 0x06002C27 RID: 11303 RVA: 0x000C6424 File Offset: 0x000C4624
		[SRCategory("CatBehavior")]
		[SRDescription("measureItemEventDescr")]
		public event MeasureItemEventHandler MeasureItem
		{
			add
			{
				base.Events.AddHandler(ListBox.EVENT_MEASUREITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListBox.EVENT_MEASUREITEM, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListBox.SelectedIndex" /> property or the <see cref="P:System.Windows.Forms.ListBox.SelectedIndices" /> collection has changed.</summary>
		// Token: 0x140001FF RID: 511
		// (add) Token: 0x06002C28 RID: 11304 RVA: 0x000C6437 File Offset: 0x000C4637
		// (remove) Token: 0x06002C29 RID: 11305 RVA: 0x000C644A File Offset: 0x000C464A
		[SRCategory("CatBehavior")]
		[SRDescription("selectedIndexChangedEventDescr")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ListBox.EVENT_SELECTEDINDEXCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListBox.EVENT_SELECTEDINDEXCHANGED, value);
			}
		}

		/// <summary>Maintains performance while items are added to the <see cref="T:System.Windows.Forms.ListBox" /> one at a time by preventing the control from drawing until the <see cref="M:System.Windows.Forms.ListBox.EndUpdate" /> method is called.</summary>
		// Token: 0x06002C2A RID: 11306 RVA: 0x000C645D File Offset: 0x000C465D
		public void BeginUpdate()
		{
			base.BeginUpdateInternal();
			this.updateCount++;
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000C6473 File Offset: 0x000C4673
		private void CheckIndex(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("IndexOutOfRange", new object[] { index.ToString(CultureInfo.CurrentCulture) }));
			}
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000C64B1 File Offset: 0x000C46B1
		private void CheckNoDataSource()
		{
			if (base.DataSource != null)
			{
				throw new ArgumentException(SR.GetString("DataSourceLocksItems"));
			}
		}

		/// <summary>Creates a new instance of the item collection.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> that represents the new item collection.</returns>
		// Token: 0x06002C2D RID: 11309 RVA: 0x000C64CB File Offset: 0x000C46CB
		protected virtual ListBox.ObjectCollection CreateItemCollection()
		{
			return new ListBox.ObjectCollection(this);
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000C64D4 File Offset: 0x000C46D4
		internal virtual int ComputeMaxItemWidth(int oldMax)
		{
			string[] array = new string[this.Items.Count];
			for (int i = 0; i < this.Items.Count; i++)
			{
				array[i] = base.GetItemText(this.Items[i]);
			}
			return Math.Max(oldMax, LayoutUtils.OldGetLargestStringSizeInCollection(this.Font, array).Width);
		}

		/// <summary>Unselects all items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		// Token: 0x06002C2F RID: 11311 RVA: 0x000C6538 File Offset: 0x000C4738
		public void ClearSelected()
		{
			bool flag = false;
			int num = ((this.itemsCollection == null) ? 0 : this.itemsCollection.Count);
			for (int i = 0; i < num; i++)
			{
				if (this.SelectedItems.GetSelected(i))
				{
					flag = true;
					this.SelectedItems.SetSelected(i, false);
					if (base.IsHandleCreated)
					{
						this.NativeSetSelected(i, false);
					}
				}
			}
			if (flag)
			{
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
		}

		/// <summary>Resumes painting the <see cref="T:System.Windows.Forms.ListBox" /> control after painting is suspended by the <see cref="M:System.Windows.Forms.ListBox.BeginUpdate" /> method.</summary>
		// Token: 0x06002C30 RID: 11312 RVA: 0x000C65A5 File Offset: 0x000C47A5
		public void EndUpdate()
		{
			base.EndUpdateInternal();
			this.updateCount--;
		}

		/// <summary>Finds the first item in the <see cref="T:System.Windows.Forms.ListBox" /> that starts with the specified string.</summary>
		/// <param name="s">The text to search for.</param>
		/// <returns>The zero-based index of the first item found; returns <see langword="ListBox.NoMatches" /> if no match is found.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="s" /> parameter is less than -1 or greater than or equal to the item count.</exception>
		// Token: 0x06002C31 RID: 11313 RVA: 0x000C65BC File Offset: 0x000C47BC
		public int FindString(string s)
		{
			return this.FindString(s, -1);
		}

		/// <summary>Finds the first item in the <see cref="T:System.Windows.Forms.ListBox" /> that starts with the specified string. The search starts at a specific starting index.</summary>
		/// <param name="s">The text to search for.</param>
		/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to negative one (-1) to search from the beginning of the control.</param>
		/// <returns>The zero-based index of the first item found; returns <see langword="ListBox.NoMatches" /> if no match is found.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="startIndex" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> class.</exception>
		// Token: 0x06002C32 RID: 11314 RVA: 0x000C65C8 File Offset: 0x000C47C8
		public int FindString(string s, int startIndex)
		{
			if (s == null)
			{
				return -1;
			}
			int num = ((this.itemsCollection == null) ? 0 : this.itemsCollection.Count);
			if (num == 0)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= num)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			return base.FindStringInternal(s, this.Items, startIndex, false);
		}

		/// <summary>Finds the first item in the <see cref="T:System.Windows.Forms.ListBox" /> that exactly matches the specified string.</summary>
		/// <param name="s">The text to search for.</param>
		/// <returns>The zero-based index of the first item found; returns <see langword="ListBox.NoMatches" /> if no match is found.</returns>
		// Token: 0x06002C33 RID: 11315 RVA: 0x000C6618 File Offset: 0x000C4818
		public int FindStringExact(string s)
		{
			return this.FindStringExact(s, -1);
		}

		/// <summary>Finds the first item in the <see cref="T:System.Windows.Forms.ListBox" /> that exactly matches the specified string. The search starts at a specific starting index.</summary>
		/// <param name="s">The text to search for.</param>
		/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to negative one (-1) to search from the beginning of the control.</param>
		/// <returns>The zero-based index of the first item found; returns <see langword="ListBox.NoMatches" /> if no match is found.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="startIndex" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> class.</exception>
		// Token: 0x06002C34 RID: 11316 RVA: 0x000C6624 File Offset: 0x000C4824
		public int FindStringExact(string s, int startIndex)
		{
			if (s == null)
			{
				return -1;
			}
			int num = ((this.itemsCollection == null) ? 0 : this.itemsCollection.Count);
			if (num == 0)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= num)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			return base.FindStringInternal(s, this.Items, startIndex, true);
		}

		/// <summary>Returns the height of an item in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <param name="index">The zero-based index of the item to return the height for.</param>
		/// <returns>The height, in pixels, of the specified item.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value of the <paramref name="index" /> parameter is less than zero or greater than the item count.</exception>
		// Token: 0x06002C35 RID: 11317 RVA: 0x000C6674 File Offset: 0x000C4874
		public int GetItemHeight(int index)
		{
			int num = ((this.itemsCollection == null) ? 0 : this.itemsCollection.Count);
			if (index < 0 || (index > 0 && index >= num))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (this.drawMode != DrawMode.OwnerDrawVariable)
			{
				index = 0;
			}
			if (!base.IsHandleCreated)
			{
				return this.itemHeight;
			}
			int num2 = (int)(long)base.SendMessage(417, index, 0);
			if (num2 == -1)
			{
				throw new Win32Exception();
			}
			return num2;
		}

		/// <summary>Returns the bounding rectangle for an item in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <param name="index">The zero-based index of item whose bounding rectangle you want to return.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle for the specified item.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> class.</exception>
		// Token: 0x06002C36 RID: 11318 RVA: 0x000C6710 File Offset: 0x000C4910
		public Rectangle GetItemRectangle(int index)
		{
			this.CheckIndex(index);
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			base.SendMessage(408, index, ref rect);
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		/// <summary>Retrieves the bounds within which the <see cref="T:System.Windows.Forms.ListBox" /> is scaled.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display bounds.</param>
		/// <param name="factor">The height and width of the control's bounds.</param>
		/// <param name="specified">One of the values of <see cref="T:System.Windows.Forms.BoundsSpecified" /> that specifies the bounds of the control to use when defining its size and position.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the bounds within which the control is scaled.</returns>
		// Token: 0x06002C37 RID: 11319 RVA: 0x000C6758 File Offset: 0x000C4958
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			bounds.Height = this.requestedHeight;
			return base.GetScaledBounds(bounds, factor, specified);
		}

		/// <summary>Returns a value indicating whether the specified item is selected.</summary>
		/// <param name="index">The zero-based index of the item that determines whether it is selected.</param>
		/// <returns>
		///   <see langword="true" /> if the specified item is currently selected in the <see cref="T:System.Windows.Forms.ListBox" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> class.</exception>
		// Token: 0x06002C38 RID: 11320 RVA: 0x000C6770 File Offset: 0x000C4970
		public bool GetSelected(int index)
		{
			this.CheckIndex(index);
			return this.GetSelectedInternal(index);
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000C6780 File Offset: 0x000C4980
		private bool GetSelectedInternal(int index)
		{
			if (!base.IsHandleCreated)
			{
				return this.itemsCollection != null && this.SelectedItems.GetSelected(index);
			}
			int num = (int)(long)base.SendMessage(391, index, 0);
			if (num == -1)
			{
				throw new Win32Exception();
			}
			return num > 0;
		}

		/// <summary>Returns the zero-based index of the item at the specified coordinates.</summary>
		/// <param name="p">A <see cref="T:System.Drawing.Point" /> object containing the coordinates used to obtain the item index.</param>
		/// <returns>The zero-based index of the item found at the specified coordinates; returns <see langword="ListBox.NoMatches" /> if no match is found.</returns>
		// Token: 0x06002C3A RID: 11322 RVA: 0x000C67D1 File Offset: 0x000C49D1
		public int IndexFromPoint(Point p)
		{
			return this.IndexFromPoint(p.X, p.Y);
		}

		/// <summary>Returns the zero-based index of the item at the specified coordinates.</summary>
		/// <param name="x">The x-coordinate of the location to search.</param>
		/// <param name="y">The y-coordinate of the location to search.</param>
		/// <returns>The zero-based index of the item found at the specified coordinates; returns <see langword="ListBox.NoMatches" /> if no match is found.</returns>
		// Token: 0x06002C3B RID: 11323 RVA: 0x000C67E8 File Offset: 0x000C49E8
		public int IndexFromPoint(int x, int y)
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetClientRect(new HandleRef(this, base.Handle), ref rect);
			if (rect.left <= x && x < rect.right && rect.top <= y && y < rect.bottom)
			{
				int num = (int)(long)base.SendMessage(425, 0, (int)(long)NativeMethods.Util.MAKELPARAM(x, y));
				if (NativeMethods.Util.HIWORD(num) == 0)
				{
					return NativeMethods.Util.LOWORD(num);
				}
			}
			return -1;
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000C6868 File Offset: 0x000C4A68
		private int NativeAdd(object item)
		{
			int num = (int)(long)base.SendMessage(384, 0, base.GetItemText(item));
			if (num == -2)
			{
				throw new OutOfMemoryException();
			}
			if (num == -1)
			{
				throw new OutOfMemoryException(SR.GetString("ListBoxItemOverflow"));
			}
			return num;
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000C68AF File Offset: 0x000C4AAF
		private void NativeClear()
		{
			base.SendMessage(388, 0, 0);
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000C68C0 File Offset: 0x000C4AC0
		internal string NativeGetItemText(int index)
		{
			int num = (int)(long)base.SendMessage(394, index, 0);
			StringBuilder stringBuilder = new StringBuilder(num + 1);
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 393, index, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000C690C File Offset: 0x000C4B0C
		private int NativeInsert(int index, object item)
		{
			int num = (int)(long)base.SendMessage(385, index, base.GetItemText(item));
			if (num == -2)
			{
				throw new OutOfMemoryException();
			}
			if (num == -1)
			{
				throw new OutOfMemoryException(SR.GetString("ListBoxItemOverflow"));
			}
			return num;
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000C6954 File Offset: 0x000C4B54
		private void NativeRemoveAt(int index)
		{
			bool flag = (int)(long)base.SendMessage(391, (IntPtr)index, IntPtr.Zero) > 0;
			base.SendMessage(386, index, 0);
			if (flag)
			{
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000C699D File Offset: 0x000C4B9D
		private void NativeSetSelected(int index, bool value)
		{
			if (this.selectionMode == SelectionMode.One)
			{
				base.SendMessage(390, value ? index : (-1), 0);
				return;
			}
			base.SendMessage(389, value ? (-1) : 0, index);
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000C69D4 File Offset: 0x000C4BD4
		private void NativeUpdateSelection()
		{
			int count = this.Items.Count;
			for (int i = 0; i < count; i++)
			{
				this.SelectedItems.SetSelected(i, false);
			}
			int[] array = null;
			SelectionMode selectionMode = this.selectionMode;
			if (selectionMode != SelectionMode.One)
			{
				if (selectionMode - SelectionMode.MultiSimple <= 1)
				{
					int num = (int)(long)base.SendMessage(400, 0, 0);
					if (num > 0)
					{
						array = new int[num];
						UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 401, num, array);
					}
				}
			}
			else
			{
				int num2 = (int)(long)base.SendMessage(392, 0, 0);
				if (num2 >= 0)
				{
					array = new int[] { num2 };
				}
			}
			if (array != null)
			{
				foreach (int num3 in array)
				{
					this.SelectedItems.SetSelected(num3, true);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ChangeUICues" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.UICuesEventArgs" /> that contains the event data.</param>
		// Token: 0x06002C43 RID: 11331 RVA: 0x000C6AAD File Offset: 0x000C4CAD
		protected override void OnChangeUICues(UICuesEventArgs e)
		{
			base.Invalidate();
			base.OnChangeUICues(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListBox.DrawItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data.</param>
		// Token: 0x06002C44 RID: 11332 RVA: 0x000C6ABC File Offset: 0x000C4CBC
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			DrawItemEventHandler drawItemEventHandler = (DrawItemEventHandler)base.Events[ListBox.EVENT_DRAWITEM];
			if (drawItemEventHandler != null)
			{
				drawItemEventHandler(this, e);
			}
		}

		/// <summary>Specifies when the window handle has been created so that column width and other characteristics can be set. Inheriting classes should call <see langword="base.OnHandleCreated" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C45 RID: 11333 RVA: 0x000C6AEC File Offset: 0x000C4CEC
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SendMessage(421, CultureInfo.CurrentCulture.LCID, 0);
			if (this.columnWidth != 0)
			{
				base.SendMessage(405, this.columnWidth, 0);
			}
			if (this.drawMode == DrawMode.OwnerDrawFixed)
			{
				base.SendMessage(416, 0, this.ItemHeight);
			}
			if (this.topIndex != 0)
			{
				base.SendMessage(407, this.topIndex, 0);
			}
			if (this.UseCustomTabOffsets && this.CustomTabOffsets != null)
			{
				int count = this.CustomTabOffsets.Count;
				int[] array = new int[count];
				this.CustomTabOffsets.CopyTo(array, 0);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 402, count, array);
			}
			if (this.itemsCollection != null)
			{
				int count2 = this.itemsCollection.Count;
				for (int i = 0; i < count2; i++)
				{
					this.NativeAdd(this.itemsCollection[i]);
					if (this.selectionMode != SelectionMode.None && this.selectedItems != null)
					{
						this.selectedItems.PushSelectionIntoNativeListBox(i);
					}
				}
			}
			if (this.selectedItems != null && this.selectedItems.Count > 0 && this.selectionMode == SelectionMode.One)
			{
				this.SelectedItems.Dirty();
				this.SelectedItems.EnsureUpToDate();
			}
			this.UpdateHorizontalExtent();
		}

		/// <summary>Overridden to be sure that items are set up and cleared out correctly. Inheriting controls should call <see langword="base.OnHandleDestroyed" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C46 RID: 11334 RVA: 0x000C6C3C File Offset: 0x000C4E3C
		protected override void OnHandleDestroyed(EventArgs e)
		{
			this.SelectedItems.EnsureUpToDate();
			if (base.Disposing)
			{
				this.itemsCollection = null;
			}
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListBox.MeasureItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> that contains the event data.</param>
		// Token: 0x06002C47 RID: 11335 RVA: 0x000C6C60 File Offset: 0x000C4E60
		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			MeasureItemEventHandler measureItemEventHandler = (MeasureItemEventHandler)base.Events[ListBox.EVENT_MEASUREITEM];
			if (measureItemEventHandler != null)
			{
				measureItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C48 RID: 11336 RVA: 0x000C6C8E File Offset: 0x000C4E8E
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.UpdateFontCache();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C49 RID: 11337 RVA: 0x000C6C9D File Offset: 0x000C4E9D
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			if (this.ParentInternal != null)
			{
				base.RecreateHandle();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C4A RID: 11338 RVA: 0x000C6CB4 File Offset: 0x000C4EB4
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.RightToLeft == RightToLeft.Yes || this.HorizontalScrollbar)
			{
				base.Invalidate();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.SelectedValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C4B RID: 11339 RVA: 0x000C6CD4 File Offset: 0x000C4ED4
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			if (base.DataManager != null && base.DataManager.Position != this.SelectedIndex && (!base.FormattingEnabled || this.SelectedIndex != -1))
			{
				base.DataManager.Position = this.SelectedIndex;
			}
			EventHandler eventHandler = (EventHandler)base.Events[ListBox.EVENT_SELECTEDINDEXCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.SelectedValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C4C RID: 11340 RVA: 0x000C6D46 File Offset: 0x000C4F46
		protected override void OnSelectedValueChanged(EventArgs e)
		{
			base.OnSelectedValueChanged(e);
			this.selectedValueChangedFired = true;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.DataSourceChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C4D RID: 11341 RVA: 0x000C6D56 File Offset: 0x000C4F56
		protected override void OnDataSourceChanged(EventArgs e)
		{
			if (base.DataSource == null)
			{
				this.BeginUpdate();
				this.SelectedIndex = -1;
				this.Items.ClearInternal();
				this.EndUpdate();
			}
			base.OnDataSourceChanged(e);
			this.RefreshItems();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListControl.DisplayMemberChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C4E RID: 11342 RVA: 0x000C6D8B File Offset: 0x000C4F8B
		protected override void OnDisplayMemberChanged(EventArgs e)
		{
			base.OnDisplayMemberChanged(e);
			this.RefreshItems();
			if (this.SelectionMode != SelectionMode.None && base.DataManager != null)
			{
				this.SelectedIndex = base.DataManager.Position;
			}
		}

		/// <summary>Forces the control to invalidate its client area and immediately redraw itself and any child controls.</summary>
		// Token: 0x06002C4F RID: 11343 RVA: 0x000C6DBC File Offset: 0x000C4FBC
		public override void Refresh()
		{
			if (this.drawMode == DrawMode.OwnerDrawVariable)
			{
				int count = this.Items.Count;
				Graphics graphics = base.CreateGraphicsInternal();
				try
				{
					for (int i = 0; i < count; i++)
					{
						MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(graphics, i, this.ItemHeight);
						this.OnMeasureItem(measureItemEventArgs);
					}
				}
				finally
				{
					graphics.Dispose();
				}
			}
			base.Refresh();
		}

		/// <summary>Refreshes all <see cref="T:System.Windows.Forms.ListBox" /> items and retrieves new strings for them.</summary>
		// Token: 0x06002C50 RID: 11344 RVA: 0x000C6E28 File Offset: 0x000C5028
		protected override void RefreshItems()
		{
			ListBox.ObjectCollection objectCollection = this.itemsCollection;
			this.itemsCollection = null;
			this.selectedIndices = null;
			if (base.IsHandleCreated)
			{
				this.NativeClear();
			}
			object[] array = null;
			if (base.DataManager != null && base.DataManager.Count != -1)
			{
				array = new object[base.DataManager.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = base.DataManager[i];
				}
			}
			else if (objectCollection != null)
			{
				array = new object[objectCollection.Count];
				objectCollection.CopyTo(array, 0);
			}
			if (array != null)
			{
				this.Items.AddRangeInternal(array);
			}
			if (this.SelectionMode != SelectionMode.None)
			{
				if (base.DataManager != null)
				{
					this.SelectedIndex = base.DataManager.Position;
					return;
				}
				if (objectCollection != null)
				{
					int count = objectCollection.Count;
					for (int j = 0; j < count; j++)
					{
						if (objectCollection.InnerArray.GetState(j, ListBox.SelectedObjectCollection.SelectedObjectMask))
						{
							this.SelectedItem = objectCollection[j];
						}
					}
				}
			}
		}

		/// <summary>Refreshes the item contained at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to refresh.</param>
		// Token: 0x06002C51 RID: 11345 RVA: 0x000C6F24 File Offset: 0x000C5124
		protected override void RefreshItem(int index)
		{
			this.Items.SetItemInternal(index, this.Items[index]);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.BackColor" /> property to its default value.</summary>
		// Token: 0x06002C52 RID: 11346 RVA: 0x000C6F3E File Offset: 0x000C513E
		public override void ResetBackColor()
		{
			base.ResetBackColor();
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property to its default value.</summary>
		// Token: 0x06002C53 RID: 11347 RVA: 0x000C6F46 File Offset: 0x000C5146
		public override void ResetForeColor()
		{
			base.ResetForeColor();
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000C6F4E File Offset: 0x000C514E
		private void ResetItemHeight()
		{
			this.itemHeight = 13;
		}

		/// <summary>Scales a control's location, size, padding and margin.</summary>
		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x06002C55 RID: 11349 RVA: 0x000C6F58 File Offset: 0x000C5158
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			if (factor.Width != 1f && factor.Height != 1f)
			{
				this.UpdateFontCache();
			}
			base.ScaleControl(factor, specified);
		}

		/// <summary>Sets the specified bounds of the <see cref="T:System.Windows.Forms.ListBox" /> control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06002C56 RID: 11350 RVA: 0x000C6F84 File Offset: 0x000C5184
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (!this.integralHeightAdjust && height != base.Height)
			{
				this.requestedHeight = height;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>Clears the contents of the <see cref="T:System.Windows.Forms.ListBox" /> and adds the specified items to the control.</summary>
		/// <param name="value">An array of objects to insert into the control.</param>
		// Token: 0x06002C57 RID: 11351 RVA: 0x000C6FB0 File Offset: 0x000C51B0
		protected override void SetItemsCore(IList value)
		{
			this.BeginUpdate();
			this.Items.ClearInternal();
			this.Items.AddRangeInternal(value);
			this.SelectedItems.Dirty();
			if (base.DataManager != null)
			{
				if (base.DataSource is ICurrencyManagerProvider)
				{
					this.selectedValueChangedFired = false;
				}
				if (base.IsHandleCreated)
				{
					base.SendMessage(390, base.DataManager.Position, 0);
				}
				if (!this.selectedValueChangedFired)
				{
					this.OnSelectedValueChanged(EventArgs.Empty);
					this.selectedValueChangedFired = false;
				}
			}
			this.EndUpdate();
		}

		/// <summary>Sets the object with the specified index in the derived class.</summary>
		/// <param name="index">The array index of the object.</param>
		/// <param name="value">The object.</param>
		// Token: 0x06002C58 RID: 11352 RVA: 0x000C7041 File Offset: 0x000C5241
		protected override void SetItemCore(int index, object value)
		{
			this.Items.SetItemInternal(index, value);
		}

		/// <summary>Selects or clears the selection for the specified item in a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <param name="index">The zero-based index of the item in a <see cref="T:System.Windows.Forms.ListBox" /> to select or clear the selection for.</param>
		/// <param name="value">
		///   <see langword="true" /> to select the specified item; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index was outside the range of valid values.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ListBox.SelectionMode" /> property was set to <see langword="None" />.</exception>
		// Token: 0x06002C59 RID: 11353 RVA: 0x000C7050 File Offset: 0x000C5250
		public void SetSelected(int index, bool value)
		{
			int num = ((this.itemsCollection == null) ? 0 : this.itemsCollection.Count);
			if (index < 0 || index >= num)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (this.selectionMode == SelectionMode.None)
			{
				throw new InvalidOperationException(SR.GetString("ListBoxInvalidSelectionMode"));
			}
			this.SelectedItems.SetSelected(index, value);
			if (base.IsHandleCreated)
			{
				this.NativeSetSelected(index, value);
			}
			this.SelectedItems.Dirty();
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		/// <summary>Sorts the items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		// Token: 0x06002C5A RID: 11354 RVA: 0x000C70FC File Offset: 0x000C52FC
		protected virtual void Sort()
		{
			this.CheckNoDataSource();
			ListBox.SelectedObjectCollection selectedObjectCollection = this.SelectedItems;
			selectedObjectCollection.EnsureUpToDate();
			if (this.sorted && this.itemsCollection != null)
			{
				this.itemsCollection.InnerArray.Sort();
				if (base.IsHandleCreated)
				{
					this.NativeClear();
					int count = this.itemsCollection.Count;
					for (int i = 0; i < count; i++)
					{
						this.NativeAdd(this.itemsCollection[i]);
						if (selectedObjectCollection.GetSelected(i))
						{
							this.NativeSetSelected(i, true);
						}
					}
				}
			}
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>A string that states the control type, the count of items in the <see cref="T:System.Windows.Forms.ListBox" /> control, and the Text property of the first item in the <see cref="T:System.Windows.Forms.ListBox" />, if the count is not 0.</returns>
		// Token: 0x06002C5B RID: 11355 RVA: 0x000C7188 File Offset: 0x000C5388
		public override string ToString()
		{
			string text = base.ToString();
			if (this.itemsCollection != null)
			{
				text = text + ", Items.Count: " + this.Items.Count.ToString(CultureInfo.CurrentCulture);
				if (this.Items.Count > 0)
				{
					string itemText = base.GetItemText(this.Items[0]);
					string text2 = ((itemText.Length > 40) ? itemText.Substring(0, 40) : itemText);
					text = text + ", Items[0]: " + text2;
				}
			}
			return text;
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000C7210 File Offset: 0x000C5410
		private void UpdateFontCache()
		{
			this.fontIsChanged = true;
			this.integralHeightAdjust = true;
			try
			{
				base.Height = this.requestedHeight;
			}
			finally
			{
				this.integralHeightAdjust = false;
			}
			this.maxWidth = -1;
			this.UpdateHorizontalExtent();
			CommonProperties.xClearPreferredSizeCache(this);
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000C7264 File Offset: 0x000C5464
		private void UpdateHorizontalExtent()
		{
			if (!this.multiColumn && this.horizontalScrollbar && base.IsHandleCreated)
			{
				int maxItemWidth = this.horizontalExtent;
				if (maxItemWidth == 0)
				{
					maxItemWidth = this.MaxItemWidth;
				}
				base.SendMessage(404, maxItemWidth, 0);
			}
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000C72A8 File Offset: 0x000C54A8
		private void UpdateMaxItemWidth(object item, bool removing)
		{
			if (!this.horizontalScrollbar || this.horizontalExtent > 0)
			{
				this.maxWidth = -1;
				return;
			}
			if (this.maxWidth > -1)
			{
				int num;
				using (Graphics graphics = base.CreateGraphicsInternal())
				{
					num = (int)Math.Ceiling((double)graphics.MeasureString(base.GetItemText(item), this.Font).Width);
				}
				if (removing)
				{
					if (num >= this.maxWidth)
					{
						this.maxWidth = -1;
						return;
					}
				}
				else if (num > this.maxWidth)
				{
					this.maxWidth = num;
				}
			}
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x000C7344 File Offset: 0x000C5544
		private void UpdateCustomTabOffsets()
		{
			if (base.IsHandleCreated && this.UseCustomTabOffsets && this.CustomTabOffsets != null)
			{
				int count = this.CustomTabOffsets.Count;
				int[] array = new int[count];
				this.CustomTabOffsets.CopyTo(array, 0);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 402, count, array);
				base.Invalidate();
			}
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000C73A8 File Offset: 0x000C55A8
		private void WmPrint(ref Message m)
		{
			base.WndProc(ref m);
			if ((2 & (int)m.LParam) != 0 && Application.RenderWithVisualStyles && this.BorderStyle == BorderStyle.Fixed3D)
			{
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					using (Graphics graphics = Graphics.FromHdc(m.WParam))
					{
						Rectangle rectangle = new Rectangle(0, 0, base.Size.Width - 1, base.Size.Height - 1);
						using (Pen pen = new Pen(VisualStyleInformation.TextControlBorder))
						{
							graphics.DrawRectangle(pen, rectangle);
						}
						rectangle.Inflate(-1, -1);
						graphics.DrawRectangle(SystemPens.Window, rectangle);
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
		}

		/// <summary>Processes the command message the <see cref="T:System.Windows.Forms.ListView" /> control receives from the top-level window.</summary>
		/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> the top-level window sent to the <see cref="T:System.Windows.Forms.ListBox" /> control.</param>
		// Token: 0x06002C61 RID: 11361 RVA: 0x000C7494 File Offset: 0x000C5694
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual void WmReflectCommand(ref Message m)
		{
			int num = NativeMethods.Util.HIWORD(m.WParam);
			if (num != 1)
			{
				return;
			}
			if (this.selectedItems != null)
			{
				this.selectedItems.Dirty();
			}
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000C74D4 File Offset: 0x000C56D4
		private void WmReflectDrawItem(ref Message m)
		{
			NativeMethods.DRAWITEMSTRUCT drawitemstruct = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
			IntPtr hDC = drawitemstruct.hDC;
			IntPtr intPtr = Control.SetUpPalette(hDC, false, false);
			try
			{
				Graphics graphics = Graphics.FromHdcInternal(hDC);
				try
				{
					Rectangle rectangle = Rectangle.FromLTRB(drawitemstruct.rcItem.left, drawitemstruct.rcItem.top, drawitemstruct.rcItem.right, drawitemstruct.rcItem.bottom);
					if (this.HorizontalScrollbar)
					{
						if (this.MultiColumn)
						{
							rectangle.Width = Math.Max(this.ColumnWidth, rectangle.Width);
						}
						else
						{
							rectangle.Width = Math.Max(this.MaxItemWidth, rectangle.Width);
						}
					}
					this.OnDrawItem(new DrawItemEventArgs(graphics, this.Font, rectangle, drawitemstruct.itemID, (DrawItemState)drawitemstruct.itemState, this.ForeColor, this.BackColor));
				}
				finally
				{
					graphics.Dispose();
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.SelectPalette(new HandleRef(null, hDC), new HandleRef(null, intPtr), 0);
				}
			}
			m.Result = (IntPtr)1;
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x000C7608 File Offset: 0x000C5808
		private void WmReflectMeasureItem(ref Message m)
		{
			NativeMethods.MEASUREITEMSTRUCT measureitemstruct = (NativeMethods.MEASUREITEMSTRUCT)m.GetLParam(typeof(NativeMethods.MEASUREITEMSTRUCT));
			if (this.drawMode == DrawMode.OwnerDrawVariable && measureitemstruct.itemID >= 0)
			{
				Graphics graphics = base.CreateGraphicsInternal();
				MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(graphics, measureitemstruct.itemID, this.ItemHeight);
				try
				{
					this.OnMeasureItem(measureItemEventArgs);
					measureitemstruct.itemHeight = measureItemEventArgs.ItemHeight;
					goto IL_6A;
				}
				finally
				{
					graphics.Dispose();
				}
			}
			measureitemstruct.itemHeight = this.ItemHeight;
			IL_6A:
			Marshal.StructureToPtr(measureitemstruct, m.LParam, false);
			m.Result = (IntPtr)1;
		}

		/// <summary>The list's window procedure.</summary>
		/// <param name="m">A Windows Message Object.</param>
		// Token: 0x06002C64 RID: 11364 RVA: 0x000C76A8 File Offset: 0x000C58A8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 791)
			{
				if (msg != 71)
				{
					switch (msg)
					{
					case 513:
						if (this.selectedItems != null)
						{
							this.selectedItems.Dirty();
						}
						base.WndProc(ref m);
						return;
					case 514:
					{
						int num = NativeMethods.Util.SignedLOWORD(m.LParam);
						int num2 = NativeMethods.Util.SignedHIWORD(m.LParam);
						Point point = new Point(num, num2);
						point = base.PointToScreen(point);
						bool capture = base.Capture;
						if (capture && UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == base.Handle)
						{
							if (!this.doubleClickFired && !base.ValidationCancelled)
							{
								this.OnClick(new MouseEventArgs(MouseButtons.Left, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
								this.OnMouseClick(new MouseEventArgs(MouseButtons.Left, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
							}
							else
							{
								this.doubleClickFired = false;
								if (!base.ValidationCancelled)
								{
									this.OnDoubleClick(new MouseEventArgs(MouseButtons.Left, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
									this.OnMouseDoubleClick(new MouseEventArgs(MouseButtons.Left, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
								}
							}
						}
						if (base.GetState(2048))
						{
							base.DefWndProc(ref m);
						}
						else
						{
							base.WndProc(ref m);
						}
						this.doubleClickFired = false;
						return;
					}
					case 515:
						this.doubleClickFired = true;
						base.WndProc(ref m);
						return;
					case 516:
						break;
					case 517:
					{
						int num3 = NativeMethods.Util.SignedLOWORD(m.LParam);
						int num4 = NativeMethods.Util.SignedHIWORD(m.LParam);
						Point point2 = new Point(num3, num4);
						point2 = base.PointToScreen(point2);
						bool capture2 = base.Capture;
						if (capture2 && UnsafeNativeMethods.WindowFromPoint(point2.X, point2.Y) == base.Handle && this.selectedItems != null)
						{
							this.selectedItems.Dirty();
						}
						base.WndProc(ref m);
						return;
					}
					default:
						if (msg == 791)
						{
							this.WmPrint(ref m);
							return;
						}
						break;
					}
				}
				else
				{
					base.WndProc(ref m);
					if (this.integralHeight && this.fontIsChanged)
					{
						base.Height = Math.Max(base.Height, this.ItemHeight);
						this.fontIsChanged = false;
						return;
					}
					return;
				}
			}
			else
			{
				if (msg == 8235)
				{
					this.WmReflectDrawItem(ref m);
					return;
				}
				if (msg == 8236)
				{
					this.WmReflectMeasureItem(ref m);
					return;
				}
				if (msg == 8465)
				{
					this.WmReflectCommand(ref m);
					return;
				}
			}
			base.WndProc(ref m);
		}

		/// <summary>Creates a new accessibility object for this control.</summary>
		/// <returns>An accessibility object for this control.</returns>
		// Token: 0x06002C65 RID: 11365 RVA: 0x000C7958 File Offset: 0x000C5B58
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ListBox.ListBoxAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		/// <summary>Specifies that no matches are found during a search.</summary>
		// Token: 0x0400124E RID: 4686
		public const int NoMatches = -1;

		/// <summary>Specifies the default item height for an owner-drawn <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		// Token: 0x0400124F RID: 4687
		public const int DefaultItemHeight = 13;

		// Token: 0x04001250 RID: 4688
		private const int maxWin9xHeight = 32767;

		// Token: 0x04001251 RID: 4689
		private static readonly object EVENT_SELECTEDINDEXCHANGED = new object();

		// Token: 0x04001252 RID: 4690
		private static readonly object EVENT_DRAWITEM = new object();

		// Token: 0x04001253 RID: 4691
		private static readonly object EVENT_MEASUREITEM = new object();

		// Token: 0x04001254 RID: 4692
		private static bool checkedOS = false;

		// Token: 0x04001255 RID: 4693
		private static bool runningOnWin2K = true;

		// Token: 0x04001256 RID: 4694
		private ListBox.SelectedObjectCollection selectedItems;

		// Token: 0x04001257 RID: 4695
		private ListBox.SelectedIndexCollection selectedIndices;

		// Token: 0x04001258 RID: 4696
		private ListBox.ObjectCollection itemsCollection;

		// Token: 0x04001259 RID: 4697
		private int itemHeight = 13;

		// Token: 0x0400125A RID: 4698
		private int columnWidth;

		// Token: 0x0400125B RID: 4699
		private int requestedHeight;

		// Token: 0x0400125C RID: 4700
		private int topIndex;

		// Token: 0x0400125D RID: 4701
		private int horizontalExtent;

		// Token: 0x0400125E RID: 4702
		private int maxWidth = -1;

		// Token: 0x0400125F RID: 4703
		private int updateCount;

		// Token: 0x04001260 RID: 4704
		private bool sorted;

		// Token: 0x04001261 RID: 4705
		private bool scrollAlwaysVisible;

		// Token: 0x04001262 RID: 4706
		private bool integralHeight = true;

		// Token: 0x04001263 RID: 4707
		private bool integralHeightAdjust;

		// Token: 0x04001264 RID: 4708
		private bool multiColumn;

		// Token: 0x04001265 RID: 4709
		private bool horizontalScrollbar;

		// Token: 0x04001266 RID: 4710
		private bool useTabStops = true;

		// Token: 0x04001267 RID: 4711
		private bool useCustomTabOffsets;

		// Token: 0x04001268 RID: 4712
		private bool fontIsChanged;

		// Token: 0x04001269 RID: 4713
		private bool doubleClickFired;

		// Token: 0x0400126A RID: 4714
		private bool selectedValueChangedFired;

		// Token: 0x0400126B RID: 4715
		private DrawMode drawMode;

		// Token: 0x0400126C RID: 4716
		private BorderStyle borderStyle = BorderStyle.Fixed3D;

		// Token: 0x0400126D RID: 4717
		private SelectionMode selectionMode = SelectionMode.One;

		// Token: 0x0400126E RID: 4718
		private SelectionMode cachedSelectionMode = SelectionMode.One;

		// Token: 0x0400126F RID: 4719
		private bool selectionModeChanging;

		// Token: 0x04001270 RID: 4720
		private ListBox.IntegerCollection customTabOffsets;

		// Token: 0x04001271 RID: 4721
		private const int defaultListItemStartPos = 1;

		// Token: 0x04001272 RID: 4722
		private const int defaultListItemBorderHeight = 1;

		// Token: 0x04001273 RID: 4723
		private const int defaultListItemPaddingBuffer = 3;

		// Token: 0x04001274 RID: 4724
		internal int scaledListItemStartPosition = 1;

		// Token: 0x04001275 RID: 4725
		internal int scaledListItemBordersHeight = 2;

		// Token: 0x04001276 RID: 4726
		internal int scaledListItemPaddingBuffer = 3;

		// Token: 0x020006BE RID: 1726
		internal class ItemArray : IComparer
		{
			// Token: 0x06006911 RID: 26897 RVA: 0x00185FC1 File Offset: 0x001841C1
			public ItemArray(ListControl listControl)
			{
				this.listControl = listControl;
			}

			// Token: 0x170016BF RID: 5823
			// (get) Token: 0x06006912 RID: 26898 RVA: 0x00185FD0 File Offset: 0x001841D0
			public int Version
			{
				get
				{
					return this.version;
				}
			}

			// Token: 0x06006913 RID: 26899 RVA: 0x00185FD8 File Offset: 0x001841D8
			public object Add(object item)
			{
				this.EnsureSpace(1);
				this.version++;
				this.entries[this.count] = new ListBox.ItemArray.Entry(item);
				ListBox.ItemArray.Entry[] array = this.entries;
				int num = this.count;
				this.count = num + 1;
				return array[num];
			}

			// Token: 0x06006914 RID: 26900 RVA: 0x00186028 File Offset: 0x00184228
			public void AddRange(ICollection items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.EnsureSpace(items.Count);
				foreach (object obj in items)
				{
					ListBox.ItemArray.Entry[] array = this.entries;
					int num = this.count;
					this.count = num + 1;
					array[num] = new ListBox.ItemArray.Entry(obj);
				}
				this.version++;
			}

			// Token: 0x06006915 RID: 26901 RVA: 0x001860B8 File Offset: 0x001842B8
			public void Clear()
			{
				if (this.count > 0)
				{
					Array.Clear(this.entries, 0, this.count);
				}
				this.count = 0;
				this.version++;
			}

			// Token: 0x06006916 RID: 26902 RVA: 0x001860EC File Offset: 0x001842EC
			public static int CreateMask()
			{
				int num = ListBox.ItemArray.lastMask;
				ListBox.ItemArray.lastMask <<= 1;
				return num;
			}

			// Token: 0x06006917 RID: 26903 RVA: 0x0018610C File Offset: 0x0018430C
			private void EnsureSpace(int elements)
			{
				if (this.entries == null)
				{
					this.entries = new ListBox.ItemArray.Entry[Math.Max(elements, 4)];
					return;
				}
				if (this.count + elements >= this.entries.Length)
				{
					int num = Math.Max(this.entries.Length * 2, this.entries.Length + elements);
					ListBox.ItemArray.Entry[] array = new ListBox.ItemArray.Entry[num];
					this.entries.CopyTo(array, 0);
					this.entries = array;
				}
			}

			// Token: 0x06006918 RID: 26904 RVA: 0x0018617C File Offset: 0x0018437C
			public int GetActualIndex(int virtualIndex, int stateMask)
			{
				if (stateMask == 0)
				{
					return virtualIndex;
				}
				int num = -1;
				for (int i = 0; i < this.count; i++)
				{
					if ((this.entries[i].state & stateMask) != 0)
					{
						num++;
						if (num == virtualIndex)
						{
							return i;
						}
					}
				}
				return -1;
			}

			// Token: 0x06006919 RID: 26905 RVA: 0x001861C0 File Offset: 0x001843C0
			public int GetCount(int stateMask)
			{
				if (stateMask == 0)
				{
					return this.count;
				}
				int num = 0;
				for (int i = 0; i < this.count; i++)
				{
					if ((this.entries[i].state & stateMask) != 0)
					{
						num++;
					}
				}
				return num;
			}

			// Token: 0x0600691A RID: 26906 RVA: 0x00186200 File Offset: 0x00184400
			public IEnumerator GetEnumerator(int stateMask)
			{
				return this.GetEnumerator(stateMask, false);
			}

			// Token: 0x0600691B RID: 26907 RVA: 0x0018620A File Offset: 0x0018440A
			public IEnumerator GetEnumerator(int stateMask, bool anyBit)
			{
				return new ListBox.ItemArray.EntryEnumerator(this, stateMask, anyBit);
			}

			// Token: 0x0600691C RID: 26908 RVA: 0x00186214 File Offset: 0x00184414
			public object GetItem(int virtualIndex, int stateMask)
			{
				int actualIndex = this.GetActualIndex(virtualIndex, stateMask);
				if (actualIndex == -1)
				{
					throw new IndexOutOfRangeException();
				}
				return this.entries[actualIndex].item;
			}

			// Token: 0x0600691D RID: 26909 RVA: 0x00186244 File Offset: 0x00184444
			internal object GetEntryObject(int virtualIndex, int stateMask)
			{
				int actualIndex = this.GetActualIndex(virtualIndex, stateMask);
				if (actualIndex == -1)
				{
					throw new IndexOutOfRangeException();
				}
				return this.entries[actualIndex];
			}

			// Token: 0x0600691E RID: 26910 RVA: 0x0018626C File Offset: 0x0018446C
			public bool GetState(int index, int stateMask)
			{
				return (this.entries[index].state & stateMask) == stateMask;
			}

			// Token: 0x0600691F RID: 26911 RVA: 0x00186280 File Offset: 0x00184480
			public int IndexOf(object item, int stateMask)
			{
				int num = -1;
				for (int i = 0; i < this.count; i++)
				{
					if (stateMask == 0 || (this.entries[i].state & stateMask) != 0)
					{
						num++;
						if (this.entries[i].item.Equals(item))
						{
							return num;
						}
					}
				}
				return -1;
			}

			// Token: 0x06006920 RID: 26912 RVA: 0x001862D0 File Offset: 0x001844D0
			public int IndexOfIdentifier(object identifier, int stateMask)
			{
				int num = -1;
				for (int i = 0; i < this.count; i++)
				{
					if (stateMask == 0 || (this.entries[i].state & stateMask) != 0)
					{
						num++;
						if (this.entries[i] == identifier)
						{
							return num;
						}
					}
				}
				return -1;
			}

			// Token: 0x06006921 RID: 26913 RVA: 0x00186318 File Offset: 0x00184518
			public void Insert(int index, object item)
			{
				this.EnsureSpace(1);
				if (index < this.count)
				{
					Array.Copy(this.entries, index, this.entries, index + 1, this.count - index);
				}
				this.entries[index] = new ListBox.ItemArray.Entry(item);
				this.count++;
				this.version++;
			}

			// Token: 0x06006922 RID: 26914 RVA: 0x0018637C File Offset: 0x0018457C
			public void Remove(object item)
			{
				int num = this.IndexOf(item, 0);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x06006923 RID: 26915 RVA: 0x001863A0 File Offset: 0x001845A0
			public void RemoveAt(int index)
			{
				this.count--;
				for (int i = index; i < this.count; i++)
				{
					this.entries[i] = this.entries[i + 1];
				}
				this.entries[this.count] = null;
				this.version++;
			}

			// Token: 0x06006924 RID: 26916 RVA: 0x001863FA File Offset: 0x001845FA
			public void SetItem(int index, object item)
			{
				this.entries[index].item = item;
			}

			// Token: 0x06006925 RID: 26917 RVA: 0x0018640A File Offset: 0x0018460A
			public void SetState(int index, int stateMask, bool value)
			{
				if (value)
				{
					this.entries[index].state |= stateMask;
				}
				else
				{
					this.entries[index].state &= ~stateMask;
				}
				this.version++;
			}

			// Token: 0x06006926 RID: 26918 RVA: 0x0018644A File Offset: 0x0018464A
			public int BinarySearch(object element)
			{
				return Array.BinarySearch(this.entries, 0, this.count, element, this);
			}

			// Token: 0x06006927 RID: 26919 RVA: 0x00186460 File Offset: 0x00184660
			public void Sort()
			{
				Array.Sort(this.entries, 0, this.count, this);
			}

			// Token: 0x06006928 RID: 26920 RVA: 0x00186475 File Offset: 0x00184675
			public void Sort(Array externalArray)
			{
				Array.Sort(externalArray, this);
			}

			// Token: 0x06006929 RID: 26921 RVA: 0x00186480 File Offset: 0x00184680
			int IComparer.Compare(object item1, object item2)
			{
				if (item1 == null)
				{
					if (item2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (item2 == null)
					{
						return 1;
					}
					if (item1 is ListBox.ItemArray.Entry)
					{
						item1 = ((ListBox.ItemArray.Entry)item1).item;
					}
					if (item2 is ListBox.ItemArray.Entry)
					{
						item2 = ((ListBox.ItemArray.Entry)item2).item;
					}
					string itemText = this.listControl.GetItemText(item1);
					string itemText2 = this.listControl.GetItemText(item2);
					CompareInfo compareInfo = Application.CurrentCulture.CompareInfo;
					return compareInfo.Compare(itemText, itemText2, CompareOptions.StringSort);
				}
			}

			// Token: 0x04003B1D RID: 15133
			private static int lastMask = 1;

			// Token: 0x04003B1E RID: 15134
			private ListControl listControl;

			// Token: 0x04003B1F RID: 15135
			private ListBox.ItemArray.Entry[] entries;

			// Token: 0x04003B20 RID: 15136
			private int count;

			// Token: 0x04003B21 RID: 15137
			private int version;

			// Token: 0x020008BD RID: 2237
			private class Entry
			{
				// Token: 0x060072A3 RID: 29347 RVA: 0x001A2D54 File Offset: 0x001A0F54
				public Entry(object item)
				{
					this.item = item;
					this.state = 0;
				}

				// Token: 0x04004532 RID: 17714
				public object item;

				// Token: 0x04004533 RID: 17715
				public int state;
			}

			// Token: 0x020008BE RID: 2238
			private class EntryEnumerator : IEnumerator
			{
				// Token: 0x060072A4 RID: 29348 RVA: 0x001A2D6A File Offset: 0x001A0F6A
				public EntryEnumerator(ListBox.ItemArray items, int state, bool anyBit)
				{
					this.items = items;
					this.state = state;
					this.anyBit = anyBit;
					this.version = items.version;
					this.current = -1;
				}

				// Token: 0x060072A5 RID: 29349 RVA: 0x001A2D9C File Offset: 0x001A0F9C
				bool IEnumerator.MoveNext()
				{
					if (this.version != this.items.version)
					{
						throw new InvalidOperationException(SR.GetString("ListEnumVersionMismatch"));
					}
					while (this.current < this.items.count - 1)
					{
						this.current++;
						if (this.anyBit)
						{
							if ((this.items.entries[this.current].state & this.state) != 0)
							{
								return true;
							}
						}
						else if ((this.items.entries[this.current].state & this.state) == this.state)
						{
							return true;
						}
					}
					this.current = this.items.count;
					return false;
				}

				// Token: 0x060072A6 RID: 29350 RVA: 0x001A2E53 File Offset: 0x001A1053
				void IEnumerator.Reset()
				{
					if (this.version != this.items.version)
					{
						throw new InvalidOperationException(SR.GetString("ListEnumVersionMismatch"));
					}
					this.current = -1;
				}

				// Token: 0x17001933 RID: 6451
				// (get) Token: 0x060072A7 RID: 29351 RVA: 0x001A2E80 File Offset: 0x001A1080
				object IEnumerator.Current
				{
					get
					{
						if (this.current == -1 || this.current == this.items.count)
						{
							throw new InvalidOperationException(SR.GetString("ListEnumCurrentOutOfRange"));
						}
						return this.items.entries[this.current].item;
					}
				}

				// Token: 0x04004534 RID: 17716
				private ListBox.ItemArray items;

				// Token: 0x04004535 RID: 17717
				private bool anyBit;

				// Token: 0x04004536 RID: 17718
				private int state;

				// Token: 0x04004537 RID: 17719
				private int current;

				// Token: 0x04004538 RID: 17720
				private int version;
			}
		}

		/// <summary>Represents the collection of items in a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		// Token: 0x020006BF RID: 1727
		[ListBindable(false)]
		public class ObjectCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" />.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListBox" /> that owns the collection.</param>
			// Token: 0x0600692B RID: 26923 RVA: 0x00186500 File Offset: 0x00184700
			public ObjectCollection(ListBox owner)
			{
				this.owner = owner;
			}

			/// <summary>Initializes a new instance of <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> based on another <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" />.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListBox" /> that owns the collection.</param>
			/// <param name="value">A <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> from which the contents are copied to this collection.</param>
			// Token: 0x0600692C RID: 26924 RVA: 0x0018650F File Offset: 0x0018470F
			public ObjectCollection(ListBox owner, ListBox.ObjectCollection value)
			{
				this.owner = owner;
				this.AddRange(value);
			}

			/// <summary>Initializes a new instance of <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> containing an array of objects.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListBox" /> that owns the collection.</param>
			/// <param name="value">An array of objects to add to the collection.</param>
			// Token: 0x0600692D RID: 26925 RVA: 0x00186525 File Offset: 0x00184725
			public ObjectCollection(ListBox owner, object[] value)
			{
				this.owner = owner;
				this.AddRange(value);
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection</returns>
			// Token: 0x170016C0 RID: 5824
			// (get) Token: 0x0600692E RID: 26926 RVA: 0x0018653B File Offset: 0x0018473B
			public int Count
			{
				get
				{
					return this.InnerArray.GetCount(0);
				}
			}

			// Token: 0x170016C1 RID: 5825
			// (get) Token: 0x0600692F RID: 26927 RVA: 0x00186549 File Offset: 0x00184749
			internal ListBox.ItemArray InnerArray
			{
				get
				{
					if (this.items == null)
					{
						this.items = new ListBox.ItemArray(this.owner);
					}
					return this.items;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" />.</returns>
			// Token: 0x170016C2 RID: 5826
			// (get) Token: 0x06006930 RID: 26928 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x170016C3 RID: 5827
			// (get) Token: 0x06006931 RID: 26929 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>
			///   <see langword="true" /> in all cases.</returns>
			// Token: 0x170016C4 RID: 5828
			// (get) Token: 0x06006932 RID: 26930 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if this collection is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x170016C5 RID: 5829
			// (get) Token: 0x06006933 RID: 26931 RVA: 0x0001180C File Offset: 0x0000FA0C
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Adds an item to the list of items for a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
			/// <param name="item">An object representing the item to add to the collection.</param>
			/// <returns>The zero-based index of the item in the collection, or -1 if <see cref="M:System.Windows.Forms.ListBox.BeginUpdate" /> has been called.</returns>
			/// <exception cref="T:System.SystemException">There is insufficient space available to add the new item to the list.</exception>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="item" /> is <see langword="null" />.</exception>
			// Token: 0x06006934 RID: 26932 RVA: 0x0018656C File Offset: 0x0018476C
			public int Add(object item)
			{
				this.owner.CheckNoDataSource();
				int num = this.AddInternal(item);
				this.owner.UpdateHorizontalExtent();
				return num;
			}

			// Token: 0x06006935 RID: 26933 RVA: 0x00186598 File Offset: 0x00184798
			private int AddInternal(object item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				int num = -1;
				if (!this.owner.sorted)
				{
					this.InnerArray.Add(item);
				}
				else
				{
					if (this.Count > 0)
					{
						num = this.InnerArray.BinarySearch(item);
						if (num < 0)
						{
							num = ~num;
						}
					}
					else
					{
						num = 0;
					}
					this.InnerArray.Insert(num, item);
				}
				bool flag = false;
				try
				{
					if (this.owner.sorted)
					{
						if (this.owner.IsHandleCreated)
						{
							this.owner.NativeInsert(num, item);
							this.owner.UpdateMaxItemWidth(item, false);
							if (this.owner.selectedItems != null)
							{
								this.owner.selectedItems.Dirty();
							}
						}
					}
					else
					{
						num = this.Count - 1;
						if (this.owner.IsHandleCreated)
						{
							this.owner.NativeAdd(item);
							this.owner.UpdateMaxItemWidth(item, false);
						}
					}
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.InnerArray.Remove(item);
					}
				}
				return num;
			}

			/// <summary>Adds an object to the <see cref="T:System.Windows.Forms.ListBox" /> class.</summary>
			/// <param name="item">The object to be added to the <see cref="T:System.Windows.Forms.ListBox" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="item" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Windows.Forms.ListBox" /> has a data source.</exception>
			/// <exception cref="T:System.SystemException">There is insufficient space available to store the new item.</exception>
			// Token: 0x06006936 RID: 26934 RVA: 0x001866AC File Offset: 0x001848AC
			int IList.Add(object item)
			{
				return this.Add(item);
			}

			/// <summary>Adds the items of an existing <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> to the list of items in a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> to load into this collection.</param>
			// Token: 0x06006937 RID: 26935 RVA: 0x001866B5 File Offset: 0x001848B5
			public void AddRange(ListBox.ObjectCollection value)
			{
				this.owner.CheckNoDataSource();
				this.AddRangeInternal(value);
			}

			/// <summary>Adds an array of items to the list of items for a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
			/// <param name="items">An array of objects to add to the list.</param>
			// Token: 0x06006938 RID: 26936 RVA: 0x001866B5 File Offset: 0x001848B5
			public void AddRange(object[] items)
			{
				this.owner.CheckNoDataSource();
				this.AddRangeInternal(items);
			}

			// Token: 0x06006939 RID: 26937 RVA: 0x001866CC File Offset: 0x001848CC
			internal void AddRangeInternal(ICollection items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.owner.BeginUpdate();
				try
				{
					foreach (object obj in items)
					{
						this.AddInternal(obj);
					}
				}
				finally
				{
					this.owner.UpdateHorizontalExtent();
					this.owner.EndUpdate();
				}
			}

			/// <summary>Gets or sets the item at the specified index within the collection.</summary>
			/// <param name="index">The index of the item in the collection to get or set.</param>
			/// <returns>An object representing the item located at the specified index within the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> class.</exception>
			// Token: 0x170016C6 RID: 5830
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public virtual object this[int index]
			{
				get
				{
					if (index < 0 || index >= this.InnerArray.GetCount(0))
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.InnerArray.GetItem(index, 0);
				}
				set
				{
					this.owner.CheckNoDataSource();
					this.SetItemInternal(index, value);
				}
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x0600693C RID: 26940 RVA: 0x001867CC File Offset: 0x001849CC
			public virtual void Clear()
			{
				this.owner.CheckNoDataSource();
				this.ClearInternal();
			}

			// Token: 0x0600693D RID: 26941 RVA: 0x001867E0 File Offset: 0x001849E0
			internal void ClearInternal()
			{
				int count = this.owner.Items.Count;
				for (int i = 0; i < count; i++)
				{
					this.owner.UpdateMaxItemWidth(this.InnerArray.GetItem(i, 0), true);
				}
				if (this.owner.IsHandleCreated)
				{
					this.owner.NativeClear();
				}
				this.InnerArray.Clear();
				this.owner.maxWidth = -1;
				this.owner.UpdateHorizontalExtent();
			}

			/// <summary>Determines whether the specified item is located within the collection.</summary>
			/// <param name="value">An object representing the item to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the item is located within the collection; otherwise, <see langword="false" />.</returns>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is <see langword="null" />.</exception>
			// Token: 0x0600693E RID: 26942 RVA: 0x0018685D File Offset: 0x00184A5D
			public bool Contains(object value)
			{
				return this.IndexOf(value) != -1;
			}

			/// <summary>Copies the entire collection into an existing array of objects at a specified location within the array.</summary>
			/// <param name="destination">The object array to which the items from the collection are copied.</param>
			/// <param name="arrayIndex">The location within the destination array to copy the items from the collection to.</param>
			// Token: 0x0600693F RID: 26943 RVA: 0x0018686C File Offset: 0x00184A6C
			public void CopyTo(object[] destination, int arrayIndex)
			{
				int count = this.InnerArray.GetCount(0);
				for (int i = 0; i < count; i++)
				{
					destination[i + arrayIndex] = this.InnerArray.GetItem(i, 0);
				}
			}

			/// <summary>Copies the elements of the collection to an array, starting at a particular array index.</summary>
			/// <param name="destination">The one-dimensional array that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			/// <exception cref="T:System.ArrayTypeMismatchException">The array type is not compatible with the items in the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" />.</exception>
			// Token: 0x06006940 RID: 26944 RVA: 0x001868A4 File Offset: 0x00184AA4
			void ICollection.CopyTo(Array destination, int index)
			{
				int count = this.InnerArray.GetCount(0);
				for (int i = 0; i < count; i++)
				{
					destination.SetValue(this.InnerArray.GetItem(i, 0), i + index);
				}
			}

			/// <summary>Returns an enumerator to use to iterate through the item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			// Token: 0x06006941 RID: 26945 RVA: 0x001868E0 File Offset: 0x00184AE0
			public IEnumerator GetEnumerator()
			{
				return this.InnerArray.GetEnumerator(0);
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <param name="value">An object representing the item to locate in the collection.</param>
			/// <returns>The zero-based index where the item is located within the collection; otherwise, negative one (-1).</returns>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
			// Token: 0x06006942 RID: 26946 RVA: 0x001868EE File Offset: 0x00184AEE
			public int IndexOf(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.InnerArray.IndexOf(value, 0);
			}

			// Token: 0x06006943 RID: 26947 RVA: 0x0018690B File Offset: 0x00184B0B
			internal int IndexOfIdentifier(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.InnerArray.IndexOfIdentifier(value, 0);
			}

			/// <summary>Inserts an item into the list box at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted.</param>
			/// <param name="item">An object representing the item to insert.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> class.</exception>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="item" /> is <see langword="null" />.</exception>
			// Token: 0x06006944 RID: 26948 RVA: 0x00186928 File Offset: 0x00184B28
			public void Insert(int index, object item)
			{
				this.owner.CheckNoDataSource();
				if (index < 0 || index > this.InnerArray.GetCount(0))
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				if (this.owner.sorted)
				{
					this.Add(item);
				}
				else
				{
					this.InnerArray.Insert(index, item);
					if (this.owner.IsHandleCreated)
					{
						bool flag = false;
						try
						{
							this.owner.NativeInsert(index, item);
							this.owner.UpdateMaxItemWidth(item, false);
							flag = true;
						}
						finally
						{
							if (!flag)
							{
								this.InnerArray.RemoveAt(index);
							}
						}
					}
				}
				this.owner.UpdateHorizontalExtent();
			}

			/// <summary>Removes the specified object from the collection.</summary>
			/// <param name="value">An object representing the item to remove from the collection.</param>
			// Token: 0x06006945 RID: 26949 RVA: 0x00186A10 File Offset: 0x00184C10
			public void Remove(object value)
			{
				int num = this.InnerArray.IndexOf(value, 0);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes the item at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> class.</exception>
			// Token: 0x06006946 RID: 26950 RVA: 0x00186A38 File Offset: 0x00184C38
			public void RemoveAt(int index)
			{
				this.owner.CheckNoDataSource();
				if (index < 0 || index >= this.InnerArray.GetCount(0))
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.owner.UpdateMaxItemWidth(this.InnerArray.GetItem(index, 0), true);
				this.InnerArray.RemoveAt(index);
				if (this.owner.IsHandleCreated)
				{
					this.owner.NativeRemoveAt(index);
				}
				this.owner.UpdateHorizontalExtent();
			}

			// Token: 0x06006947 RID: 26951 RVA: 0x00186AE0 File Offset: 0x00184CE0
			internal void SetItemInternal(int index, object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (index < 0 || index >= this.InnerArray.GetCount(0))
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.owner.UpdateMaxItemWidth(this.InnerArray.GetItem(index, 0), true);
				this.InnerArray.SetItem(index, value);
				if (this.owner.IsHandleCreated)
				{
					bool flag = this.owner.SelectedIndex == index;
					if (string.Compare(this.owner.GetItemText(value), this.owner.NativeGetItemText(index), true, CultureInfo.CurrentCulture) != 0)
					{
						this.owner.NativeRemoveAt(index);
						this.owner.SelectedItems.SetSelected(index, false);
						this.owner.NativeInsert(index, value);
						this.owner.UpdateMaxItemWidth(value, false);
						if (flag)
						{
							this.owner.SelectedIndex = index;
						}
					}
					else if (flag)
					{
						this.owner.OnSelectedIndexChanged(EventArgs.Empty);
					}
				}
				this.owner.UpdateHorizontalExtent();
			}

			// Token: 0x04003B22 RID: 15138
			private ListBox owner;

			// Token: 0x04003B23 RID: 15139
			private ListBox.ItemArray items;
		}

		/// <summary>Represents a collection of integers in a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		// Token: 0x020006C0 RID: 1728
		public class IntegerCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListBox" /> that owns the collection.</param>
			// Token: 0x06006948 RID: 26952 RVA: 0x00186C12 File Offset: 0x00184E12
			public IntegerCollection(ListBox owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the number of selected items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
			/// <returns>The number of selected items in the <see cref="T:System.Windows.Forms.ListBox" />.</returns>
			// Token: 0x170016C7 RID: 5831
			// (get) Token: 0x06006949 RID: 26953 RVA: 0x00186C21 File Offset: 0x00184E21
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize to the collection.</returns>
			// Token: 0x170016C8 RID: 5832
			// (get) Token: 0x0600694A RID: 26954 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="true" /> in all cases.</returns>
			// Token: 0x170016C9 RID: 5833
			// (get) Token: 0x0600694B RID: 26955 RVA: 0x00012E4E File Offset: 0x0001104E
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>
			///   <see langword="true" /> in all cases.</returns>
			// Token: 0x170016CA RID: 5834
			// (get) Token: 0x0600694C RID: 26956 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x170016CB RID: 5835
			// (get) Token: 0x0600694D RID: 26957 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool IList.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Determines whether the specified integer is in the collection.</summary>
			/// <param name="item">The integer to search for in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified integer is in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600694E RID: 26958 RVA: 0x00186C29 File Offset: 0x00184E29
			public bool Contains(int item)
			{
				return this.IndexOf(item) != -1;
			}

			/// <summary>Determines whether the specified tab stop is in the collection.</summary>
			/// <param name="item">The tab stop to locate in the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</param>
			/// <returns>
			///   <see langword="true" /> if item is an integer located in the IntegerCollection; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600694F RID: 26959 RVA: 0x00186C38 File Offset: 0x00184E38
			bool IList.Contains(object item)
			{
				return item is int && this.Contains((int)item);
			}

			/// <summary>Removes all integers from the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</summary>
			// Token: 0x06006950 RID: 26960 RVA: 0x00186C50 File Offset: 0x00184E50
			public void Clear()
			{
				this.count = 0;
				this.innerArray = null;
			}

			/// <summary>Retrieves the index within the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" /> of the specified integer.</summary>
			/// <param name="item">The integer for which to retrieve the index.</param>
			/// <returns>The zero-based index of the integer in the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />; otherwise, negative one (-1).</returns>
			// Token: 0x06006951 RID: 26961 RVA: 0x00186C60 File Offset: 0x00184E60
			public int IndexOf(int item)
			{
				int num = -1;
				if (this.innerArray != null)
				{
					num = Array.IndexOf<int>(this.innerArray, item);
					if (num >= this.count)
					{
						num = -1;
					}
				}
				return num;
			}

			/// <summary>Returns the index of the specified tab stop in the collection.</summary>
			/// <param name="item">The tab stop to locate in the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</param>
			/// <returns>The zero-based index of item if it was found in the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />; otherwise, -1.</returns>
			// Token: 0x06006952 RID: 26962 RVA: 0x00186C90 File Offset: 0x00184E90
			int IList.IndexOf(object item)
			{
				if (item is int)
				{
					return this.IndexOf((int)item);
				}
				return -1;
			}

			// Token: 0x06006953 RID: 26963 RVA: 0x00186CA8 File Offset: 0x00184EA8
			private int AddInternal(int item)
			{
				this.EnsureSpace(1);
				int num = this.IndexOf(item);
				if (num == -1)
				{
					int[] array = this.innerArray;
					int num2 = this.count;
					this.count = num2 + 1;
					array[num2] = item;
					Array.Sort<int>(this.innerArray, 0, this.count);
					num = this.IndexOf(item);
				}
				return num;
			}

			/// <summary>Adds a unique integer to the collection in sorted order.</summary>
			/// <param name="item">The integer to add to the collection.</param>
			/// <returns>The index of the added item.</returns>
			/// <exception cref="T:System.SystemException">There is insufficient space available to store the new item.</exception>
			// Token: 0x06006954 RID: 26964 RVA: 0x00186CFC File Offset: 0x00184EFC
			public int Add(int item)
			{
				int num = this.AddInternal(item);
				this.owner.UpdateCustomTabOffsets();
				return num;
			}

			/// <summary>Adds a tab stop to the collection.</summary>
			/// <param name="item">The tab stop to add to the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</param>
			/// <returns>The index at which the integer was added to the collection.</returns>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="item" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="item" /> is not an 32-bit signed integer.</exception>
			/// <exception cref="T:System.SystemException">There is insufficient space to store the new item in the collection.</exception>
			// Token: 0x06006955 RID: 26965 RVA: 0x00186D1D File Offset: 0x00184F1D
			int IList.Add(object item)
			{
				if (!(item is int))
				{
					throw new ArgumentException("item");
				}
				return this.Add((int)item);
			}

			/// <summary>Adds an array of integers to the collection.</summary>
			/// <param name="items">The array of integers to add to the collection.</param>
			// Token: 0x06006956 RID: 26966 RVA: 0x00186D3E File Offset: 0x00184F3E
			public void AddRange(int[] items)
			{
				this.AddRangeInternal(items);
			}

			/// <summary>Adds the contents of an existing <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" /> to another collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" /> to add to another collection.</param>
			// Token: 0x06006957 RID: 26967 RVA: 0x00186D3E File Offset: 0x00184F3E
			public void AddRange(ListBox.IntegerCollection value)
			{
				this.AddRangeInternal(value);
			}

			// Token: 0x06006958 RID: 26968 RVA: 0x00186D48 File Offset: 0x00184F48
			private void AddRangeInternal(ICollection items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.owner.BeginUpdate();
				try
				{
					this.EnsureSpace(items.Count);
					foreach (object obj in items)
					{
						if (!(obj is int))
						{
							throw new ArgumentException("item");
						}
						this.AddInternal((int)obj);
					}
					this.owner.UpdateCustomTabOffsets();
				}
				finally
				{
					this.owner.EndUpdate();
				}
			}

			// Token: 0x06006959 RID: 26969 RVA: 0x00186DFC File Offset: 0x00184FFC
			private void EnsureSpace(int elements)
			{
				if (this.innerArray == null)
				{
					this.innerArray = new int[Math.Max(elements, 4)];
					return;
				}
				if (this.count + elements >= this.innerArray.Length)
				{
					int num = Math.Max(this.innerArray.Length * 2, this.innerArray.Length + elements);
					int[] array = new int[num];
					this.innerArray.CopyTo(array, 0);
					this.innerArray = array;
				}
			}

			/// <summary>Clears all the tab stops from the collection.</summary>
			// Token: 0x0600695A RID: 26970 RVA: 0x00186E6B File Offset: 0x0018506B
			void IList.Clear()
			{
				this.Clear();
			}

			/// <summary>Inserts an item into the collection at a specified index.</summary>
			/// <param name="index">The zero-based index at which value should be inserted.</param>
			/// <param name="value">The object to insert into the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x0600695B RID: 26971 RVA: 0x00186E73 File Offset: 0x00185073
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException(SR.GetString("ListBoxCantInsertIntoIntegerCollection"));
			}

			/// <summary>Removes the first occurrence of an item from the collection.</summary>
			/// <param name="value">The object to add to the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x0600695C RID: 26972 RVA: 0x00186E84 File Offset: 0x00185084
			void IList.Remove(object value)
			{
				if (!(value is int))
				{
					throw new ArgumentException("value");
				}
				this.Remove((int)value);
			}

			/// <summary>Removes the item at a specified index.</summary>
			/// <param name="index">The index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x0600695D RID: 26973 RVA: 0x00186EA5 File Offset: 0x001850A5
			void IList.RemoveAt(int index)
			{
				this.RemoveAt(index);
			}

			/// <summary>Removes the specified integer from the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</summary>
			/// <param name="item">The integer to remove from the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</param>
			// Token: 0x0600695E RID: 26974 RVA: 0x00186EB0 File Offset: 0x001850B0
			public void Remove(int item)
			{
				int num = this.IndexOf(item);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes the integer at the specified index from the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</summary>
			/// <param name="index">The zero-based index of the integer to remove.</param>
			// Token: 0x0600695F RID: 26975 RVA: 0x00186ED0 File Offset: 0x001850D0
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.count--;
				for (int i = index; i < this.count; i++)
				{
					this.innerArray[i] = this.innerArray[i + 1];
				}
			}

			/// <summary>Gets or sets the <see cref="P:System.Windows.Forms.ListBox.IntegerCollection.Item(System.Int32)" /> having the specified index.</summary>
			/// <param name="index">The position of the <see cref="P:System.Windows.Forms.ListBox.IntegerCollection.Item(System.Int32)" /> in the collection.</param>
			/// <returns>The selected <see cref="P:System.Windows.Forms.ListBox.IntegerCollection.Item(System.Int32)" /> at the specified position.</returns>
			// Token: 0x170016CC RID: 5836
			public int this[int index]
			{
				get
				{
					return this.innerArray[index];
				}
				set
				{
					if (index < 0 || index >= this.count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.innerArray[index] = value;
					this.owner.UpdateCustomTabOffsets();
				}
			}

			/// <summary>Gets or sets the tab stop at the specified index.</summary>
			/// <param name="index">The zero-based index that specifies which tab stop to get.</param>
			/// <returns>The tab stop that is stored at the specified location in the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</returns>
			/// <exception cref="T:System.ArgumentException">The object is not an integer.</exception>
			// Token: 0x170016CD RID: 5837
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (!(value is int))
					{
						throw new ArgumentException("value");
					}
					this[index] = (int)value;
				}
			}

			/// <summary>Copies the entire <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" /> into an existing array of integers at a specified location within the array.</summary>
			/// <param name="destination">The array into which the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" /> is copied.</param>
			/// <param name="index">The location within the destination array to which to copy the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</param>
			// Token: 0x06006964 RID: 26980 RVA: 0x00186FE8 File Offset: 0x001851E8
			public void CopyTo(Array destination, int index)
			{
				int num = this.Count;
				for (int i = 0; i < num; i++)
				{
					destination.SetValue(this[i], i + index);
				}
			}

			/// <summary>Retrieves an enumeration of all the integers in the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</returns>
			// Token: 0x06006965 RID: 26981 RVA: 0x0018701D File Offset: 0x0018521D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListBox.IntegerCollection.CustomTabOffsetsEnumerator(this);
			}

			// Token: 0x04003B24 RID: 15140
			private ListBox owner;

			// Token: 0x04003B25 RID: 15141
			private int[] innerArray;

			// Token: 0x04003B26 RID: 15142
			private int count;

			// Token: 0x020008BF RID: 2239
			private class CustomTabOffsetsEnumerator : IEnumerator
			{
				// Token: 0x060072A8 RID: 29352 RVA: 0x001A2ED0 File Offset: 0x001A10D0
				public CustomTabOffsetsEnumerator(ListBox.IntegerCollection items)
				{
					this.items = items;
					this.current = -1;
				}

				// Token: 0x060072A9 RID: 29353 RVA: 0x001A2EE6 File Offset: 0x001A10E6
				bool IEnumerator.MoveNext()
				{
					if (this.current < this.items.Count - 1)
					{
						this.current++;
						return true;
					}
					this.current = this.items.Count;
					return false;
				}

				// Token: 0x060072AA RID: 29354 RVA: 0x001A2F1F File Offset: 0x001A111F
				void IEnumerator.Reset()
				{
					this.current = -1;
				}

				// Token: 0x17001934 RID: 6452
				// (get) Token: 0x060072AB RID: 29355 RVA: 0x001A2F28 File Offset: 0x001A1128
				object IEnumerator.Current
				{
					get
					{
						if (this.current == -1 || this.current == this.items.Count)
						{
							throw new InvalidOperationException(SR.GetString("ListEnumCurrentOutOfRange"));
						}
						return this.items[this.current];
					}
				}

				// Token: 0x04004539 RID: 17721
				private ListBox.IntegerCollection items;

				// Token: 0x0400453A RID: 17722
				private int current;
			}
		}

		/// <summary>Represents the collection containing the indexes to the selected items in a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		// Token: 0x020006C1 RID: 1729
		public class SelectedIndexCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" /> class.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListBox" /> representing the owner of the collection.</param>
			// Token: 0x06006966 RID: 26982 RVA: 0x00187025 File Offset: 0x00185225
			public SelectedIndexCollection(ListBox owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170016CE RID: 5838
			// (get) Token: 0x06006967 RID: 26983 RVA: 0x00187034 File Offset: 0x00185234
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.SelectedItems.Count;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" />.</returns>
			// Token: 0x170016CF RID: 5839
			// (get) Token: 0x06006968 RID: 26984 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x170016D0 RID: 5840
			// (get) Token: 0x06006969 RID: 26985 RVA: 0x00012E4E File Offset: 0x0001104E
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>
			///   <see langword="true" /> in all cases.</returns>
			// Token: 0x170016D1 RID: 5841
			// (get) Token: 0x0600696A RID: 26986 RVA: 0x00012E4E File Offset: 0x0001104E
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x170016D2 RID: 5842
			// (get) Token: 0x0600696B RID: 26987 RVA: 0x00012E4E File Offset: 0x0001104E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Determines whether the specified index is located within the collection.</summary>
			/// <param name="selectedIndex">The index to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified index from the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> for the <see cref="T:System.Windows.Forms.ListBox" /> is an item in this collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600696C RID: 26988 RVA: 0x00187046 File Offset: 0x00185246
			public bool Contains(int selectedIndex)
			{
				return this.IndexOf(selectedIndex) != -1;
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
			/// <param name="selectedIndex">The index to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified index from the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> for the <see cref="T:System.Windows.Forms.ListBox" /> is an item in this collection; otherwise, false.</returns>
			// Token: 0x0600696D RID: 26989 RVA: 0x00187055 File Offset: 0x00185255
			bool IList.Contains(object selectedIndex)
			{
				return selectedIndex is int && this.Contains((int)selectedIndex);
			}

			/// <summary>Returns the index within the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" /> of the specified index from the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> of the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
			/// <param name="selectedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> to locate in this collection.</param>
			/// <returns>The zero-based index in the collection where the specified index of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> was located within the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" />; otherwise, negative one (-1).</returns>
			// Token: 0x0600696E RID: 26990 RVA: 0x00187070 File Offset: 0x00185270
			public int IndexOf(int selectedIndex)
			{
				if (selectedIndex >= 0 && selectedIndex < this.InnerArray.GetCount(0) && this.InnerArray.GetState(selectedIndex, ListBox.SelectedObjectCollection.SelectedObjectMask))
				{
					return this.InnerArray.IndexOf(this.InnerArray.GetItem(selectedIndex, 0), ListBox.SelectedObjectCollection.SelectedObjectMask);
				}
				return -1;
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
			/// <param name="selectedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> to locate in this collection.</param>
			/// <returns>The zero-based index in the collection where the specified index of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> is located if it is in the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" />; otherwise, -1.</returns>
			// Token: 0x0600696F RID: 26991 RVA: 0x001870C2 File Offset: 0x001852C2
			int IList.IndexOf(object selectedIndex)
			{
				if (selectedIndex is int)
				{
					return this.IndexOf((int)selectedIndex);
				}
				return -1;
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <param name="value">The index to add to the collection.</param>
			/// <returns>The position into which the index was inserted.</returns>
			// Token: 0x06006970 RID: 26992 RVA: 0x001870DA File Offset: 0x001852DA
			int IList.Add(object value)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedIndexCollectionIsReadOnly"));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
			// Token: 0x06006971 RID: 26993 RVA: 0x001870DA File Offset: 0x001852DA
			void IList.Clear()
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedIndexCollectionIsReadOnly"));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The index at which value should be inserted.</param>
			/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06006972 RID: 26994 RVA: 0x001870DA File Offset: 0x001852DA
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedIndexCollectionIsReadOnly"));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06006973 RID: 26995 RVA: 0x001870DA File Offset: 0x001852DA
			void IList.Remove(object value)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedIndexCollectionIsReadOnly"));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06006974 RID: 26996 RVA: 0x001870DA File Offset: 0x001852DA
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedIndexCollectionIsReadOnly"));
			}

			/// <summary>Gets the index value at the specified index within this collection.</summary>
			/// <param name="index">The index of the item in the collection to get.</param>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> that is stored at the specified location.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.SelectedIndexCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" /> class.</exception>
			// Token: 0x170016D3 RID: 5843
			public int this[int index]
			{
				get
				{
					object entryObject = this.InnerArray.GetEntryObject(index, ListBox.SelectedObjectCollection.SelectedObjectMask);
					return this.InnerArray.IndexOfIdentifier(entryObject, 0);
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the element to get.</param>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> that is stored at the specified location.</returns>
			// Token: 0x170016D4 RID: 5844
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException(SR.GetString("ListBoxSelectedIndexCollectionIsReadOnly"));
				}
			}

			// Token: 0x170016D5 RID: 5845
			// (get) Token: 0x06006978 RID: 27000 RVA: 0x00187126 File Offset: 0x00185326
			private ListBox.ItemArray InnerArray
			{
				get
				{
					this.owner.SelectedItems.EnsureUpToDate();
					return this.owner.Items.InnerArray;
				}
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="destination">The destination array.</param>
			/// <param name="index">The index in the destination array at which storing begins.</param>
			// Token: 0x06006979 RID: 27001 RVA: 0x00187148 File Offset: 0x00185348
			public void CopyTo(Array destination, int index)
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					destination.SetValue(this[i], i + index);
				}
			}

			/// <summary>Removes all controls from the collection.</summary>
			// Token: 0x0600697A RID: 27002 RVA: 0x0018717D File Offset: 0x0018537D
			public void Clear()
			{
				if (this.owner != null)
				{
					this.owner.ClearSelected();
				}
			}

			/// <summary>Adds the <see cref="T:System.Windows.Forms.ListBox" /> at the specified index location.</summary>
			/// <param name="index">The location in the array at which to add the <see cref="T:System.Windows.Forms.ListBox" />.</param>
			// Token: 0x0600697B RID: 27003 RVA: 0x00187194 File Offset: 0x00185394
			public void Add(int index)
			{
				if (this.owner != null)
				{
					ListBox.ObjectCollection items = this.owner.Items;
					if (items != null && index != -1 && !this.Contains(index))
					{
						this.owner.SetSelected(index, true);
					}
				}
			}

			/// <summary>Removes the specified control from the collection.</summary>
			/// <param name="index">The control to be removed.</param>
			// Token: 0x0600697C RID: 27004 RVA: 0x001871D4 File Offset: 0x001853D4
			public void Remove(int index)
			{
				if (this.owner != null)
				{
					ListBox.ObjectCollection items = this.owner.Items;
					if (items != null && index != -1 && this.Contains(index))
					{
						this.owner.SetSelected(index, false);
					}
				}
			}

			/// <summary>Returns an enumerator to use to iterate through the selected indexes collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the selected indexes collection.</returns>
			// Token: 0x0600697D RID: 27005 RVA: 0x00187212 File Offset: 0x00185412
			public IEnumerator GetEnumerator()
			{
				return new ListBox.SelectedIndexCollection.SelectedIndexEnumerator(this);
			}

			// Token: 0x04003B27 RID: 15143
			private ListBox owner;

			// Token: 0x020008C0 RID: 2240
			private class SelectedIndexEnumerator : IEnumerator
			{
				// Token: 0x060072AC RID: 29356 RVA: 0x001A2F77 File Offset: 0x001A1177
				public SelectedIndexEnumerator(ListBox.SelectedIndexCollection items)
				{
					this.items = items;
					this.current = -1;
				}

				// Token: 0x060072AD RID: 29357 RVA: 0x001A2F8D File Offset: 0x001A118D
				bool IEnumerator.MoveNext()
				{
					if (this.current < this.items.Count - 1)
					{
						this.current++;
						return true;
					}
					this.current = this.items.Count;
					return false;
				}

				// Token: 0x060072AE RID: 29358 RVA: 0x001A2FC6 File Offset: 0x001A11C6
				void IEnumerator.Reset()
				{
					this.current = -1;
				}

				// Token: 0x17001935 RID: 6453
				// (get) Token: 0x060072AF RID: 29359 RVA: 0x001A2FD0 File Offset: 0x001A11D0
				object IEnumerator.Current
				{
					get
					{
						if (this.current == -1 || this.current == this.items.Count)
						{
							throw new InvalidOperationException(SR.GetString("ListEnumCurrentOutOfRange"));
						}
						return this.items[this.current];
					}
				}

				// Token: 0x0400453B RID: 17723
				private ListBox.SelectedIndexCollection items;

				// Token: 0x0400453C RID: 17724
				private int current;
			}
		}

		/// <summary>Represents the collection of selected items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		// Token: 0x020006C2 RID: 1730
		public class SelectedObjectCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListBox.SelectedObjectCollection" /> class.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListBox" /> representing the owner of the collection.</param>
			// Token: 0x0600697E RID: 27006 RVA: 0x0018721A File Offset: 0x0018541A
			public SelectedObjectCollection(ListBox owner)
			{
				this.owner = owner;
				this.stateDirty = true;
				this.lastVersion = -1;
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x170016D6 RID: 5846
			// (get) Token: 0x0600697F RID: 27007 RVA: 0x00187238 File Offset: 0x00185438
			public int Count
			{
				get
				{
					if (!this.owner.IsHandleCreated)
					{
						if (this.lastVersion != this.InnerArray.Version)
						{
							this.lastVersion = this.InnerArray.Version;
							this.count = this.InnerArray.GetCount(ListBox.SelectedObjectCollection.SelectedObjectMask);
						}
						return this.count;
					}
					switch (this.owner.selectionModeChanging ? this.owner.cachedSelectionMode : this.owner.selectionMode)
					{
					case SelectionMode.None:
						return 0;
					case SelectionMode.One:
					{
						int selectedIndex = this.owner.SelectedIndex;
						if (selectedIndex >= 0)
						{
							return 1;
						}
						return 0;
					}
					case SelectionMode.MultiSimple:
					case SelectionMode.MultiExtended:
						return (int)(long)this.owner.SendMessage(400, 0, 0);
					default:
						return 0;
					}
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the underlying list.</returns>
			// Token: 0x170016D7 RID: 5847
			// (get) Token: 0x06006980 RID: 27008 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>
			///   <see langword="true" /> if the list is synchronized; otherwise <see langword="false" />.</returns>
			// Token: 0x170016D8 RID: 5848
			// (get) Token: 0x06006981 RID: 27009 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>
			///   <see langword="true" /> if the underlying list has a fixed size; otherwise <see langword="false" />.</returns>
			// Token: 0x170016D9 RID: 5849
			// (get) Token: 0x06006982 RID: 27010 RVA: 0x00012E4E File Offset: 0x0001104E
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006983 RID: 27011 RVA: 0x00187301 File Offset: 0x00185501
			internal void Dirty()
			{
				this.stateDirty = true;
			}

			// Token: 0x170016DA RID: 5850
			// (get) Token: 0x06006984 RID: 27012 RVA: 0x0018730A File Offset: 0x0018550A
			private ListBox.ItemArray InnerArray
			{
				get
				{
					this.EnsureUpToDate();
					return this.owner.Items.InnerArray;
				}
			}

			// Token: 0x06006985 RID: 27013 RVA: 0x00187322 File Offset: 0x00185522
			internal void EnsureUpToDate()
			{
				if (this.stateDirty)
				{
					this.stateDirty = false;
					if (this.owner.IsHandleCreated)
					{
						this.owner.NativeUpdateSelection();
					}
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x170016DB RID: 5851
			// (get) Token: 0x06006986 RID: 27014 RVA: 0x00012E4E File Offset: 0x0001104E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Determines whether the specified item is located within the collection.</summary>
			/// <param name="selectedObject">An object representing the item to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the specified item is located in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006987 RID: 27015 RVA: 0x0018734B File Offset: 0x0018554B
			public bool Contains(object selectedObject)
			{
				return this.IndexOf(selectedObject) != -1;
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <param name="selectedObject">An object representing the item to locate in the collection.</param>
			/// <returns>The zero-based index of the item in the collection; otherwise, -1.</returns>
			// Token: 0x06006988 RID: 27016 RVA: 0x0018735A File Offset: 0x0018555A
			public int IndexOf(object selectedObject)
			{
				return this.InnerArray.IndexOf(selectedObject, ListBox.SelectedObjectCollection.SelectedObjectMask);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <param name="value">The object to add to the collection.</param>
			/// <returns>The position into which the object was inserted.</returns>
			// Token: 0x06006989 RID: 27017 RVA: 0x0018736D File Offset: 0x0018556D
			int IList.Add(object value)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedObjectCollectionIsReadOnly"));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
			// Token: 0x0600698A RID: 27018 RVA: 0x0018736D File Offset: 0x0018556D
			void IList.Clear()
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedObjectCollectionIsReadOnly"));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The zero-based index at which the object should be inserted.</param>
			/// <param name="value">The object to insert into the <see cref="T:System.Windows.Forms.ListBox.SelectedObjectCollection" />.</param>
			// Token: 0x0600698B RID: 27019 RVA: 0x0018736D File Offset: 0x0018556D
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedObjectCollectionIsReadOnly"));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The object to remove.</param>
			// Token: 0x0600698C RID: 27020 RVA: 0x0018736D File Offset: 0x0018556D
			void IList.Remove(object value)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedObjectCollectionIsReadOnly"));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the object to remove from the <see cref="T:System.Windows.Forms.ListBox.SelectedObjectCollection" />.</param>
			// Token: 0x0600698D RID: 27021 RVA: 0x0018736D File Offset: 0x0018556D
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedObjectCollectionIsReadOnly"));
			}

			// Token: 0x0600698E RID: 27022 RVA: 0x0018737E File Offset: 0x0018557E
			internal object GetObjectAt(int index)
			{
				return this.InnerArray.GetEntryObject(index, ListBox.SelectedObjectCollection.SelectedObjectMask);
			}

			/// <summary>Gets the item at the specified index within the collection.</summary>
			/// <param name="index">The index of the item in the collection to retrieve.</param>
			/// <returns>An object representing the item located at the specified index within the collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.SelectedObjectCollection" /> class.</exception>
			// Token: 0x170016DC RID: 5852
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public object this[int index]
			{
				get
				{
					return this.InnerArray.GetItem(index, ListBox.SelectedObjectCollection.SelectedObjectMask);
				}
				set
				{
					throw new NotSupportedException(SR.GetString("ListBoxSelectedObjectCollectionIsReadOnly"));
				}
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="destination">An array to copy the contents of the collection to.</param>
			/// <param name="index">The location within the destination array to copy the items from the collection to.</param>
			// Token: 0x06006991 RID: 27025 RVA: 0x001873A4 File Offset: 0x001855A4
			public void CopyTo(Array destination, int index)
			{
				int num = this.InnerArray.GetCount(ListBox.SelectedObjectCollection.SelectedObjectMask);
				for (int i = 0; i < num; i++)
				{
					destination.SetValue(this.InnerArray.GetItem(i, ListBox.SelectedObjectCollection.SelectedObjectMask), i + index);
				}
			}

			/// <summary>Returns an enumerator that can be used to iterate through the selected item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			// Token: 0x06006992 RID: 27026 RVA: 0x001873E8 File Offset: 0x001855E8
			public IEnumerator GetEnumerator()
			{
				return this.InnerArray.GetEnumerator(ListBox.SelectedObjectCollection.SelectedObjectMask);
			}

			// Token: 0x06006993 RID: 27027 RVA: 0x001873FA File Offset: 0x001855FA
			internal bool GetSelected(int index)
			{
				return this.InnerArray.GetState(index, ListBox.SelectedObjectCollection.SelectedObjectMask);
			}

			// Token: 0x06006994 RID: 27028 RVA: 0x00187410 File Offset: 0x00185610
			internal void PushSelectionIntoNativeListBox(int index)
			{
				bool state = this.owner.Items.InnerArray.GetState(index, ListBox.SelectedObjectCollection.SelectedObjectMask);
				if (state)
				{
					this.owner.NativeSetSelected(index, true);
				}
			}

			// Token: 0x06006995 RID: 27029 RVA: 0x00187449 File Offset: 0x00185649
			internal void SetSelected(int index, bool value)
			{
				this.InnerArray.SetState(index, ListBox.SelectedObjectCollection.SelectedObjectMask, value);
			}

			/// <summary>Removes all items from the collection of selected items.</summary>
			// Token: 0x06006996 RID: 27030 RVA: 0x0018745D File Offset: 0x0018565D
			public void Clear()
			{
				if (this.owner != null)
				{
					this.owner.ClearSelected();
				}
			}

			/// <summary>Adds an item to the list of selected items for a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
			/// <param name="value">An object representing the item to add to the collection of selected items.</param>
			// Token: 0x06006997 RID: 27031 RVA: 0x00187474 File Offset: 0x00185674
			public void Add(object value)
			{
				if (this.owner != null)
				{
					ListBox.ObjectCollection items = this.owner.Items;
					if (items != null && value != null)
					{
						int num = items.IndexOf(value);
						if (num != -1 && !this.GetSelected(num))
						{
							this.owner.SelectedIndex = num;
						}
					}
				}
			}

			/// <summary>Removes the specified object from the collection of selected items.</summary>
			/// <param name="value">An object representing the item to remove from the collection.</param>
			// Token: 0x06006998 RID: 27032 RVA: 0x001874BC File Offset: 0x001856BC
			public void Remove(object value)
			{
				if (this.owner != null)
				{
					ListBox.ObjectCollection items = this.owner.Items;
					if ((items != null) & (value != null))
					{
						int num = items.IndexOf(value);
						if (num != -1 && this.GetSelected(num))
						{
							this.owner.SetSelected(num, false);
						}
					}
				}
			}

			// Token: 0x04003B28 RID: 15144
			internal static int SelectedObjectMask = ListBox.ItemArray.CreateMask();

			// Token: 0x04003B29 RID: 15145
			private ListBox owner;

			// Token: 0x04003B2A RID: 15146
			private bool stateDirty;

			// Token: 0x04003B2B RID: 15147
			private int lastVersion;

			// Token: 0x04003B2C RID: 15148
			private int count;
		}

		// Token: 0x020006C3 RID: 1731
		private sealed class ListBoxAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x0600699A RID: 27034 RVA: 0x00187516 File Offset: 0x00185716
			public ListBoxAccessibleObject(ListBox control)
				: base(control)
			{
				this.owner = control;
			}

			// Token: 0x0600699B RID: 27035 RVA: 0x00012E4E File Offset: 0x0001104E
			internal override bool IsIAccessibleExSupported()
			{
				return true;
			}

			// Token: 0x0600699C RID: 27036 RVA: 0x00187528 File Offset: 0x00185728
			internal override object GetObjectForChild(int childId)
			{
				IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
				if (ListBox.ListBoxAccessibleObject.IsChildIdValid(childId, systemIAccessibleInternal) && (AccessibleRole)systemIAccessibleInternal.get_accRole(childId) == AccessibleRole.ListItem)
				{
					return new ListBox.ListBoxAccessibleObject.ListBoxItemAccessibleObject(this.owner, childId);
				}
				return base.GetObjectForChild(childId);
			}

			// Token: 0x0600699D RID: 27037 RVA: 0x0018756E File Offset: 0x0018576E
			private static bool IsChildIdValid(int childId, IAccessible systemIAccessible)
			{
				return childId > 0 && childId <= systemIAccessible.accChildCount;
			}

			// Token: 0x04003B2D RID: 15149
			private readonly ListBox owner;

			// Token: 0x020008C1 RID: 2241
			private sealed class ListBoxItemAccessibleObject : AccessibleObject
			{
				// Token: 0x060072B0 RID: 29360 RVA: 0x001A301F File Offset: 0x001A121F
				public ListBoxItemAccessibleObject(ListBox owner, int childId)
				{
					this.owner = owner;
					this.childId = childId;
				}

				// Token: 0x060072B1 RID: 29361 RVA: 0x00012E4E File Offset: 0x0001104E
				internal override bool IsIAccessibleExSupported()
				{
					return true;
				}

				// Token: 0x060072B2 RID: 29362 RVA: 0x001A3035 File Offset: 0x001A1235
				internal override bool IsPatternSupported(int patternId)
				{
					return patternId == 10017 || base.IsPatternSupported(patternId);
				}

				// Token: 0x060072B3 RID: 29363 RVA: 0x001A3048 File Offset: 0x001A1248
				internal override void ScrollIntoView()
				{
					if (this.owner.IsHandleCreated && ListBox.ListBoxAccessibleObject.IsChildIdValid(this.childId, this.owner.AccessibilityObject.GetSystemIAccessibleInternal()))
					{
						this.owner.TopIndex = this.childId - 1;
					}
				}

				// Token: 0x0400453D RID: 17725
				private readonly int childId;

				// Token: 0x0400453E RID: 17726
				private readonly ListBox owner;
			}
		}
	}
}
