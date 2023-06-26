using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents the abstract base class that manages events and layout for all the elements that a <see cref="T:System.Windows.Forms.ToolStrip" /> or <see cref="T:System.Windows.Forms.ToolStripDropDown" /> can contain.</summary>
	// Token: 0x020003C9 RID: 969
	[DesignTimeVisible(false)]
	[Designer("System.Windows.Forms.Design.ToolStripItemDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("Click")]
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	public abstract class ToolStripItem : Component, IDropTarget, ISupportOleDropSource, IArrangedElement, IComponent, IDisposable, IKeyboardToolTip
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItem" /> class.</summary>
		// Token: 0x06004192 RID: 16786 RVA: 0x00118930 File Offset: 0x00116B30
		protected ToolStripItem()
		{
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledDefaultMargin = DpiHelper.LogicalToDeviceUnits(ToolStripItem.defaultMargin, 0);
				this.scaledDefaultStatusStripMargin = DpiHelper.LogicalToDeviceUnits(ToolStripItem.defaultStatusStripMargin, 0);
			}
			this.state[ToolStripItem.stateEnabled | ToolStripItem.stateAutoSize | ToolStripItem.stateVisible | ToolStripItem.stateContstructing | ToolStripItem.stateSupportsItemClick | ToolStripItem.stateInvalidMirroredImage | ToolStripItem.stateMouseDownAndUpMustBeInSameItem | ToolStripItem.stateUseAmbientMargin] = true;
			this.state[ToolStripItem.stateAllowDrop | ToolStripItem.stateMouseDownAndNoDrag | ToolStripItem.stateSupportsRightClick | ToolStripItem.statePressed | ToolStripItem.stateSelected | ToolStripItem.stateDisposed | ToolStripItem.stateDoubleClickEnabled | ToolStripItem.stateRightToLeftAutoMirrorImage | ToolStripItem.stateSupportsSpaceKey] = false;
			this.SetAmbientMargin();
			this.Size = this.DefaultSize;
			this.DisplayStyle = this.DefaultDisplayStyle;
			CommonProperties.SetAutoSize(this, true);
			this.state[ToolStripItem.stateContstructing] = false;
			this.AutoToolTip = this.DefaultAutoToolTip;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItem" /> class with the specified name, image, and event handler.</summary>
		/// <param name="text">A <see cref="T:System.String" /> representing the name of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="onClick">Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event when the user clicks the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		// Token: 0x06004193 RID: 16787 RVA: 0x00118AAA File Offset: 0x00116CAA
		protected ToolStripItem(string text, Image image, EventHandler onClick)
			: this(text, image, onClick, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItem" /> class with the specified display text, image, event handler, and name.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="image">The Image to display on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="onClick">The event handler for the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		// Token: 0x06004194 RID: 16788 RVA: 0x00118AB6 File Offset: 0x00116CB6
		protected ToolStripItem(string text, Image image, EventHandler onClick, string name)
			: this()
		{
			this.Text = text;
			this.Image = image;
			if (onClick != null)
			{
				this.Click += onClick;
			}
			this.Name = name;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control; if no <see cref="T:System.Windows.Forms.AccessibleObject" /> is currently assigned to the control, a new instance is created when this property is first accessed</returns>
		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x00118AE0 File Offset: 0x00116CE0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ToolStripItemAccessibilityObjectDescr")]
		public AccessibleObject AccessibilityObject
		{
			get
			{
				AccessibleObject accessibleObject = (AccessibleObject)this.Properties.GetObject(ToolStripItem.PropAccessibility);
				if (accessibleObject == null)
				{
					accessibleObject = this.CreateAccessibilityInstance();
					this.Properties.SetObject(ToolStripItem.PropAccessibility, accessibleObject);
				}
				return accessibleObject;
			}
		}

		/// <summary>Gets or sets the default action description of the control for use by accessibility client applications.</summary>
		/// <returns>The default action description of the control, for use by accessibility client applications.</returns>
		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x00118B1F File Offset: 0x00116D1F
		// (set) Token: 0x06004197 RID: 16791 RVA: 0x00118B36 File Offset: 0x00116D36
		[SRCategory("CatAccessibility")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ToolStripItemAccessibleDefaultActionDescr")]
		public string AccessibleDefaultActionDescription
		{
			get
			{
				return (string)this.Properties.GetObject(ToolStripItem.PropAccessibleDefaultActionDescription);
			}
			set
			{
				this.Properties.SetObject(ToolStripItem.PropAccessibleDefaultActionDescription, value);
				this.OnAccessibleDefaultActionDescriptionChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the description that will be reported to accessibility client applications.</summary>
		/// <returns>The description of the control used by accessibility client applications. The default is <see langword="null" />.</returns>
		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06004198 RID: 16792 RVA: 0x00118B54 File Offset: 0x00116D54
		// (set) Token: 0x06004199 RID: 16793 RVA: 0x00118B6B File Offset: 0x00116D6B
		[SRCategory("CatAccessibility")]
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("ToolStripItemAccessibleDescriptionDescr")]
		public string AccessibleDescription
		{
			get
			{
				return (string)this.Properties.GetObject(ToolStripItem.PropAccessibleDescription);
			}
			set
			{
				this.Properties.SetObject(ToolStripItem.PropAccessibleDescription, value);
				this.OnAccessibleDescriptionChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the name of the control for use by accessibility client applications.</summary>
		/// <returns>The name of the control, for use by accessibility client applications. The default is <see langword="null" />.</returns>
		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x0600419A RID: 16794 RVA: 0x00118B89 File Offset: 0x00116D89
		// (set) Token: 0x0600419B RID: 16795 RVA: 0x00118BA0 File Offset: 0x00116DA0
		[SRCategory("CatAccessibility")]
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("ToolStripItemAccessibleNameDescr")]
		public string AccessibleName
		{
			get
			{
				return (string)this.Properties.GetObject(ToolStripItem.PropAccessibleName);
			}
			set
			{
				this.Properties.SetObject(ToolStripItem.PropAccessibleName, value);
				this.OnAccessibleNameChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the accessible role of the control, which specifies the type of user interface element of the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values. The default is <see cref="F:System.Windows.Forms.AccessibleRole.PushButton" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values.</exception>
		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x0600419C RID: 16796 RVA: 0x00118BC0 File Offset: 0x00116DC0
		// (set) Token: 0x0600419D RID: 16797 RVA: 0x00118BE8 File Offset: 0x00116DE8
		[SRCategory("CatAccessibility")]
		[DefaultValue(AccessibleRole.Default)]
		[SRDescription("ToolStripItemAccessibleRoleDescr")]
		public AccessibleRole AccessibleRole
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(ToolStripItem.PropAccessibleRole, out flag);
				if (flag)
				{
					return (AccessibleRole)integer;
				}
				return AccessibleRole.Default;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, -1, 64))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AccessibleRole));
				}
				this.Properties.SetInteger(ToolStripItem.PropAccessibleRole, (int)value);
				this.OnAccessibleRoleChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether the item aligns towards the beginning or end of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemAlignment" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.ToolStripItemAlignment" /> values.</exception>
		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x0600419E RID: 16798 RVA: 0x00118C38 File Offset: 0x00116E38
		// (set) Token: 0x0600419F RID: 16799 RVA: 0x00118C40 File Offset: 0x00116E40
		[DefaultValue(ToolStripItemAlignment.Left)]
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripItemAlignmentDescr")]
		public ToolStripItemAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripItemAlignment));
				}
				if (this.alignment != value)
				{
					this.alignment = value;
					if (this.ParentInternal != null && this.ParentInternal.IsHandleCreated)
					{
						this.ParentInternal.PerformLayout();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether drag-and-drop and item reordering are handled through events that you implement.</summary>
		/// <returns>
		///   <see langword="true" /> if drag-and-drop operations are allowed in the control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Windows.Forms.ToolStripItem.AllowDrop" /> and <see cref="P:System.Windows.Forms.ToolStrip.AllowItemReorder" /> are both set to <see langword="true" />.</exception>
		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x060041A0 RID: 16800 RVA: 0x00118CA3 File Offset: 0x00116EA3
		// (set) Token: 0x060041A1 RID: 16801 RVA: 0x00118CB5 File Offset: 0x00116EB5
		[SRCategory("CatDragDrop")]
		[DefaultValue(false)]
		[SRDescription("ToolStripItemAllowDropDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public virtual bool AllowDrop
		{
			get
			{
				return this.state[ToolStripItem.stateAllowDrop];
			}
			set
			{
				if (value != this.state[ToolStripItem.stateAllowDrop])
				{
					this.EnsureParentDropTargetRegistered();
					this.state[ToolStripItem.stateAllowDrop] = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is automatically sized.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is automatically sized; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x060041A2 RID: 16802 RVA: 0x00118CE1 File Offset: 0x00116EE1
		// (set) Token: 0x060041A3 RID: 16803 RVA: 0x00118CF3 File Offset: 0x00116EF3
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.All)]
		[Localizable(true)]
		[SRDescription("ToolStripItemAutoSizeDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool AutoSize
		{
			get
			{
				return this.state[ToolStripItem.stateAutoSize];
			}
			set
			{
				if (this.state[ToolStripItem.stateAutoSize] != value)
				{
					this.state[ToolStripItem.stateAutoSize] = value;
					CommonProperties.SetAutoSize(this, value);
					this.InvalidateItemLayout(PropertyNames.AutoSize);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether to use the <see cref="P:System.Windows.Forms.ToolStripItem.Text" /> property or the <see cref="P:System.Windows.Forms.ToolStripItem.ToolTipText" /> property for the <see cref="T:System.Windows.Forms.ToolStripItem" /> ToolTip.</summary>
		/// <returns>
		///   <see langword="true" /> to use the <see cref="P:System.Windows.Forms.ToolStripItem.Text" /> property for the ToolTip; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x060041A4 RID: 16804 RVA: 0x00118D2B File Offset: 0x00116F2B
		// (set) Token: 0x060041A5 RID: 16805 RVA: 0x00118D3D File Offset: 0x00116F3D
		[DefaultValue(false)]
		[SRDescription("ToolStripItemAutoToolTipDescr")]
		[SRCategory("CatBehavior")]
		public bool AutoToolTip
		{
			get
			{
				return this.state[ToolStripItem.stateAutoToolTip];
			}
			set
			{
				this.state[ToolStripItem.stateAutoToolTip] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripItem" /> should be placed on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is placed on a <see cref="T:System.Windows.Forms.ToolStrip" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x060041A6 RID: 16806 RVA: 0x00118D50 File Offset: 0x00116F50
		// (set) Token: 0x060041A7 RID: 16807 RVA: 0x00118D62 File Offset: 0x00116F62
		[Browsable(false)]
		[SRDescription("ToolStripItemAvailableDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Available
		{
			get
			{
				return this.state[ToolStripItem.stateVisible];
			}
			set
			{
				this.SetVisibleCore(value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripItem.Available" /> property changes.</summary>
		// Token: 0x14000338 RID: 824
		// (add) Token: 0x060041A8 RID: 16808 RVA: 0x00118D6B File Offset: 0x00116F6B
		// (remove) Token: 0x060041A9 RID: 16809 RVA: 0x00118D7E File Offset: 0x00116F7E
		[Browsable(false)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnAvailableChangedDescr")]
		public event EventHandler AvailableChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventAvailableChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventAvailableChanged, value);
			}
		}

		/// <summary>Gets or sets the background image displayed in the item.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the item.</returns>
		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x060041AA RID: 16810 RVA: 0x00118D91 File Offset: 0x00116F91
		// (set) Token: 0x060041AB RID: 16811 RVA: 0x00118DA8 File Offset: 0x00116FA8
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageDescr")]
		[DefaultValue(null)]
		public virtual Image BackgroundImage
		{
			get
			{
				return this.Properties.GetObject(ToolStripItem.PropBackgroundImage) as Image;
			}
			set
			{
				if (this.BackgroundImage != value)
				{
					this.Properties.SetObject(ToolStripItem.PropBackgroundImage, value);
					this.Invalidate();
				}
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x060041AC RID: 16812 RVA: 0x00118DCA File Offset: 0x00116FCA
		// (set) Token: 0x060041AD RID: 16813 RVA: 0x00118DD2 File Offset: 0x00116FD2
		internal virtual int DeviceDpi
		{
			get
			{
				return this.deviceDpi;
			}
			set
			{
				this.deviceDpi = value;
			}
		}

		/// <summary>Gets or sets the background image layout used for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values. The default value is <see cref="F:System.Windows.Forms.ImageLayout.Tile" />.</returns>
		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x060041AE RID: 16814 RVA: 0x00118DDC File Offset: 0x00116FDC
		// (set) Token: 0x060041AF RID: 16815 RVA: 0x00118E14 File Offset: 0x00117014
		[SRCategory("CatAppearance")]
		[DefaultValue(ImageLayout.Tile)]
		[Localizable(true)]
		[SRDescription("ControlBackgroundImageLayoutDescr")]
		public virtual ImageLayout BackgroundImageLayout
		{
			get
			{
				if (!this.Properties.ContainsObject(ToolStripItem.PropBackgroundImageLayout))
				{
					return ImageLayout.Tile;
				}
				return (ImageLayout)this.Properties.GetObject(ToolStripItem.PropBackgroundImageLayout);
			}
			set
			{
				if (this.BackgroundImageLayout != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ImageLayout));
					}
					this.Properties.SetObject(ToolStripItem.PropBackgroundImageLayout, value);
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the background color for the item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the item. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x060041B0 RID: 16816 RVA: 0x00118E6C File Offset: 0x0011706C
		// (set) Token: 0x060041B1 RID: 16817 RVA: 0x00118EA4 File Offset: 0x001170A4
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemBackColorDescr")]
		public virtual Color BackColor
		{
			get
			{
				Color rawBackColor = this.RawBackColor;
				if (!rawBackColor.IsEmpty)
				{
					return rawBackColor;
				}
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null)
				{
					return parentInternal.BackColor;
				}
				return Control.DefaultBackColor;
			}
			set
			{
				Color backColor = this.BackColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(ToolStripItem.PropBackColor))
				{
					this.Properties.SetColor(ToolStripItem.PropBackColor, value);
				}
				if (!backColor.Equals(this.BackColor))
				{
					this.OnBackColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripItem.BackColor" /> property changes.</summary>
		// Token: 0x14000339 RID: 825
		// (add) Token: 0x060041B2 RID: 16818 RVA: 0x00118F09 File Offset: 0x00117109
		// (remove) Token: 0x060041B3 RID: 16819 RVA: 0x00118F1C File Offset: 0x0011711C
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnBackColorChangedDescr")]
		public event EventHandler BackColorChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventBackColorChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventBackColorChanged, value);
			}
		}

		/// <summary>Gets the size and location of the item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x060041B4 RID: 16820 RVA: 0x00118F2F File Offset: 0x0011712F
		[Browsable(false)]
		public virtual Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x060041B5 RID: 16821 RVA: 0x00118F38 File Offset: 0x00117138
		internal Rectangle ClientBounds
		{
			get
			{
				Rectangle rectangle = this.bounds;
				rectangle.Location = Point.Empty;
				return rectangle;
			}
		}

		/// <summary>Gets the area where content, such as text and icons, can be placed within a <see cref="T:System.Windows.Forms.ToolStripItem" /> without overwriting background borders.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> containing four integers that represent the location and size of <see cref="T:System.Windows.Forms.ToolStripItem" /> contents, excluding its border.</returns>
		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x060041B6 RID: 16822 RVA: 0x00118F5C File Offset: 0x0011715C
		[Browsable(false)]
		public Rectangle ContentRectangle
		{
			get
			{
				Rectangle rectangle = LayoutUtils.InflateRect(this.InternalLayout.ContentRectangle, this.Padding);
				rectangle.Size = LayoutUtils.UnionSizes(Size.Empty, rectangle.Size);
				return rectangle;
			}
		}

		/// <summary>Gets a value indicating whether the item can be selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripItem" /> can be selected; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x060041B7 RID: 16823 RVA: 0x00012E4E File Offset: 0x0001104E
		[Browsable(false)]
		public virtual bool CanSelect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x060041B8 RID: 16824 RVA: 0x00118F99 File Offset: 0x00117199
		internal virtual bool CanKeyboardSelect
		{
			get
			{
				return this.CanSelect;
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripItem" /> is clicked.</summary>
		// Token: 0x1400033A RID: 826
		// (add) Token: 0x060041B9 RID: 16825 RVA: 0x00118FA1 File Offset: 0x001171A1
		// (remove) Token: 0x060041BA RID: 16826 RVA: 0x00118FB4 File Offset: 0x001171B4
		[SRCategory("CatAction")]
		[SRDescription("ToolStripItemOnClickDescr")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventClick, value);
			}
		}

		/// <summary>Gets or sets the edges of the container to which a <see cref="T:System.Windows.Forms.ToolStripItem" /> is bound and determines how a <see cref="T:System.Windows.Forms.ToolStripItem" /> is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</exception>
		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x060041BB RID: 16827 RVA: 0x00118FC7 File Offset: 0x001171C7
		// (set) Token: 0x060041BC RID: 16828 RVA: 0x00118FCF File Offset: 0x001171CF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
		public AnchorStyles Anchor
		{
			get
			{
				return CommonProperties.xGetAnchor(this);
			}
			set
			{
				if (value != this.Anchor)
				{
					CommonProperties.xSetAnchor(this, value);
					if (this.ParentInternal != null)
					{
						LayoutTransaction.DoLayout(this, this.ParentInternal, PropertyNames.Anchor);
					}
				}
			}
		}

		/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.ToolStripItem" /> borders are docked to its parent control and determines how a <see cref="T:System.Windows.Forms.ToolStripItem" /> is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.DockStyle" /> values.</exception>
		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x060041BD RID: 16829 RVA: 0x00118FFA File Offset: 0x001171FA
		// (set) Token: 0x060041BE RID: 16830 RVA: 0x00119004 File Offset: 0x00117204
		[Browsable(false)]
		[DefaultValue(DockStyle.None)]
		public DockStyle Dock
		{
			get
			{
				return CommonProperties.xGetDock(this);
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 5))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DockStyle));
				}
				if (value != this.Dock)
				{
					CommonProperties.xSetDock(this, value);
					if (this.ParentInternal != null)
					{
						LayoutTransaction.DoLayout(this, this.ParentInternal, PropertyNames.Dock);
					}
				}
			}
		}

		/// <summary>Gets a value indicating whether to display the <see cref="T:System.Windows.Forms.ToolTip" /> that is defined as the default.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x060041BF RID: 16831 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual bool DefaultAutoToolTip
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the default margin of an item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the margin.</returns>
		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x060041C0 RID: 16832 RVA: 0x00119060 File Offset: 0x00117260
		protected internal virtual Padding DefaultMargin
		{
			get
			{
				if (this.Owner != null && this.Owner is StatusStrip)
				{
					return this.scaledDefaultStatusStripMargin;
				}
				return this.scaledDefaultMargin;
			}
		}

		/// <summary>Gets the internal spacing characteristics of the item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Padding" /> values.</returns>
		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x060041C1 RID: 16833 RVA: 0x00019A61 File Offset: 0x00017C61
		protected virtual Padding DefaultPadding
		{
			get
			{
				return Padding.Empty;
			}
		}

		/// <summary>Gets the default size of the item.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x060041C2 RID: 16834 RVA: 0x00119084 File Offset: 0x00117284
		protected virtual Size DefaultSize
		{
			get
			{
				if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					return new Size(23, 23);
				}
				return DpiHelper.LogicalToDeviceUnits(new Size(23, 23), this.DeviceDpi);
			}
		}

		/// <summary>Gets a value indicating what is displayed on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemDisplayStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText" />.</returns>
		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x060041C3 RID: 16835 RVA: 0x00023BD7 File Offset: 0x00021DD7
		protected virtual ToolStripItemDisplayStyle DefaultDisplayStyle
		{
			get
			{
				return ToolStripItemDisplayStyle.ImageAndText;
			}
		}

		/// <summary>Gets a value indicating whether items on a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> are hidden after they are clicked.</summary>
		/// <returns>
		///   <see langword="true" /> if the item is hidden after it is clicked; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x060041C4 RID: 16836 RVA: 0x00012E4E File Offset: 0x0001104E
		protected internal virtual bool DismissWhenClicked
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets whether text and images are displayed on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemDisplayStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText" /> .</returns>
		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x060041C5 RID: 16837 RVA: 0x001190AB File Offset: 0x001172AB
		// (set) Token: 0x060041C6 RID: 16838 RVA: 0x001190B4 File Offset: 0x001172B4
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemDisplayStyleDescr")]
		public virtual ToolStripItemDisplayStyle DisplayStyle
		{
			get
			{
				return this.displayStyle;
			}
			set
			{
				if (this.displayStyle != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripItemDisplayStyle));
					}
					this.displayStyle = value;
					if (!this.state[ToolStripItem.stateContstructing])
					{
						this.InvalidateItemLayout(PropertyNames.DisplayStyle);
						this.OnDisplayStyleChanged(new EventArgs());
					}
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.DisplayStyle" /> has changed.</summary>
		// Token: 0x1400033B RID: 827
		// (add) Token: 0x060041C7 RID: 16839 RVA: 0x001119C3 File Offset: 0x0010FBC3
		// (remove) Token: 0x060041C8 RID: 16840 RVA: 0x001119D6 File Offset: 0x0010FBD6
		public event EventHandler DisplayStyleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventDisplayStyleChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventDisplayStyleChanged, value);
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x060041C9 RID: 16841 RVA: 0x00016041 File Offset: 0x00014241
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		private RightToLeft DefaultRightToLeft
		{
			get
			{
				return RightToLeft.Inherit;
			}
		}

		/// <summary>Occurs when the item is double-clicked with the mouse.</summary>
		// Token: 0x1400033C RID: 828
		// (add) Token: 0x060041CA RID: 16842 RVA: 0x0011911F File Offset: 0x0011731F
		// (remove) Token: 0x060041CB RID: 16843 RVA: 0x00119132 File Offset: 0x00117332
		[SRCategory("CatAction")]
		[SRDescription("ControlOnDoubleClickDescr")]
		public event EventHandler DoubleClick
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventDoubleClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventDoubleClick, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripItem" /> can be activated by double-clicking the mouse.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripItem" /> can be activated by double-clicking the mouse; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x060041CC RID: 16844 RVA: 0x00119145 File Offset: 0x00117345
		// (set) Token: 0x060041CD RID: 16845 RVA: 0x00119157 File Offset: 0x00117357
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripItemDoubleClickedEnabledDescr")]
		public bool DoubleClickEnabled
		{
			get
			{
				return this.state[ToolStripItem.stateDoubleClickEnabled];
			}
			set
			{
				this.state[ToolStripItem.stateDoubleClickEnabled] = value;
			}
		}

		/// <summary>Occurs when the user drags an item and the user releases the mouse button, indicating that the item should be dropped into this item.</summary>
		// Token: 0x1400033D RID: 829
		// (add) Token: 0x060041CE RID: 16846 RVA: 0x0011916A File Offset: 0x0011736A
		// (remove) Token: 0x060041CF RID: 16847 RVA: 0x0011917D File Offset: 0x0011737D
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnDragDropDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event DragEventHandler DragDrop
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventDragDrop, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventDragDrop, value);
			}
		}

		/// <summary>Occurs when the user drags an item into the client area of this item.</summary>
		// Token: 0x1400033E RID: 830
		// (add) Token: 0x060041D0 RID: 16848 RVA: 0x00119190 File Offset: 0x00117390
		// (remove) Token: 0x060041D1 RID: 16849 RVA: 0x001191A3 File Offset: 0x001173A3
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnDragEnterDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event DragEventHandler DragEnter
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventDragEnter, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventDragEnter, value);
			}
		}

		/// <summary>Occurs when the user drags an item over the client area of this item.</summary>
		// Token: 0x1400033F RID: 831
		// (add) Token: 0x060041D2 RID: 16850 RVA: 0x001191B6 File Offset: 0x001173B6
		// (remove) Token: 0x060041D3 RID: 16851 RVA: 0x001191C9 File Offset: 0x001173C9
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnDragOverDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event DragEventHandler DragOver
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventDragOver, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventDragOver, value);
			}
		}

		/// <summary>Occurs when the user drags an item and the mouse pointer is no longer over the client area of this item.</summary>
		// Token: 0x14000340 RID: 832
		// (add) Token: 0x060041D4 RID: 16852 RVA: 0x001191DC File Offset: 0x001173DC
		// (remove) Token: 0x060041D5 RID: 16853 RVA: 0x001191EF File Offset: 0x001173EF
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnDragLeaveDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event EventHandler DragLeave
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventDragLeave, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventDragLeave, value);
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x060041D6 RID: 16854 RVA: 0x00119202 File Offset: 0x00117402
		private DropSource DropSource
		{
			get
			{
				if (this.ParentInternal != null && this.ParentInternal.AllowItemReorder && this.ParentInternal.ItemReorderDropSource != null)
				{
					return new DropSource(this.ParentInternal.ItemReorderDropSource);
				}
				return new DropSource(this);
			}
		}

		/// <summary>Gets or sets a value indicating whether the parent control of the <see cref="T:System.Windows.Forms.ToolStripItem" /> is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the parent control of the <see cref="T:System.Windows.Forms.ToolStripItem" /> is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x060041D7 RID: 16855 RVA: 0x00119240 File Offset: 0x00117440
		// (set) Token: 0x060041D8 RID: 16856 RVA: 0x00119278 File Offset: 0x00117478
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("ToolStripItemEnabledDescr")]
		[DefaultValue(true)]
		public virtual bool Enabled
		{
			get
			{
				bool flag = true;
				if (this.Owner != null)
				{
					flag = this.Owner.Enabled;
				}
				return this.state[ToolStripItem.stateEnabled] && flag;
			}
			set
			{
				if (this.state[ToolStripItem.stateEnabled] != value)
				{
					this.state[ToolStripItem.stateEnabled] = value;
					if (!this.state[ToolStripItem.stateEnabled])
					{
						bool flag = this.state[ToolStripItem.stateSelected];
						this.state[ToolStripItem.stateSelected | ToolStripItem.statePressed] = false;
						if (flag && !AccessibilityImprovements.UseLegacyToolTipDisplay)
						{
							KeyboardToolTipStateMachine.Instance.NotifyAboutLostFocus(this);
						}
					}
					this.OnEnabledChanged(EventArgs.Empty);
					this.Invalidate();
				}
				this.OnInternalEnabledChanged(EventArgs.Empty);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.Enabled" /> property value has changed.</summary>
		// Token: 0x14000341 RID: 833
		// (add) Token: 0x060041D9 RID: 16857 RVA: 0x00119314 File Offset: 0x00117514
		// (remove) Token: 0x060041DA RID: 16858 RVA: 0x00119327 File Offset: 0x00117527
		[SRDescription("ToolStripItemEnabledChangedDescr")]
		public event EventHandler EnabledChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventEnabledChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventEnabledChanged, value);
			}
		}

		// Token: 0x14000342 RID: 834
		// (add) Token: 0x060041DB RID: 16859 RVA: 0x0011933A File Offset: 0x0011753A
		// (remove) Token: 0x060041DC RID: 16860 RVA: 0x0011934D File Offset: 0x0011754D
		internal event EventHandler InternalEnabledChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventInternalEnabledChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventInternalEnabledChanged, value);
			}
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x00119360 File Offset: 0x00117560
		private void EnsureParentDropTargetRegistered()
		{
			if (this.ParentInternal != null)
			{
				IntSecurity.ClipboardRead.Demand();
				this.ParentInternal.DropTargetManager.EnsureRegistered(this);
			}
		}

		/// <summary>Gets or sets the foreground color of the item.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the item. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x060041DE RID: 16862 RVA: 0x00119388 File Offset: 0x00117588
		// (set) Token: 0x060041DF RID: 16863 RVA: 0x001193C8 File Offset: 0x001175C8
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemForeColorDescr")]
		public virtual Color ForeColor
		{
			get
			{
				Color color = this.Properties.GetColor(ToolStripItem.PropForeColor);
				if (!color.IsEmpty)
				{
					return color;
				}
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null)
				{
					return parentInternal.ForeColor;
				}
				return Control.DefaultForeColor;
			}
			set
			{
				Color foreColor = this.ForeColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(ToolStripItem.PropForeColor))
				{
					this.Properties.SetColor(ToolStripItem.PropForeColor, value);
				}
				if (!foreColor.Equals(this.ForeColor))
				{
					this.OnForeColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.ForeColor" /> property value changes.</summary>
		// Token: 0x14000343 RID: 835
		// (add) Token: 0x060041E0 RID: 16864 RVA: 0x0011942D File Offset: 0x0011762D
		// (remove) Token: 0x060041E1 RID: 16865 RVA: 0x00119440 File Offset: 0x00117640
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnForeColorChangedDescr")]
		public event EventHandler ForeColorChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventForeColorChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventForeColorChanged, value);
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the item.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the <see cref="T:System.Windows.Forms.ToolStripItem" />. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x060041E2 RID: 16866 RVA: 0x00119454 File Offset: 0x00117654
		// (set) Token: 0x060041E3 RID: 16867 RVA: 0x0011949C File Offset: 0x0011769C
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolStripItemFontDescr")]
		public virtual Font Font
		{
			get
			{
				Font font = (Font)this.Properties.GetObject(ToolStripItem.PropFont);
				if (font != null)
				{
					return font;
				}
				Font ownerFont = this.GetOwnerFont();
				if (ownerFont != null)
				{
					return ownerFont;
				}
				if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					return ToolStripManager.DefaultFont;
				}
				return this.defaultFont;
			}
			set
			{
				Font font = (Font)this.Properties.GetObject(ToolStripItem.PropFont);
				if (font != value)
				{
					this.Properties.SetObject(ToolStripItem.PropFont, value);
					this.OnFontChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs during a drag operation.</summary>
		// Token: 0x14000344 RID: 836
		// (add) Token: 0x060041E4 RID: 16868 RVA: 0x001194DF File Offset: 0x001176DF
		// (remove) Token: 0x060041E5 RID: 16869 RVA: 0x001194F2 File Offset: 0x001176F2
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnGiveFeedbackDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventGiveFeedback, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventGiveFeedback, value);
			}
		}

		/// <summary>Gets or sets the height, in pixels, of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the height, in pixels.</returns>
		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x060041E6 RID: 16870 RVA: 0x00119508 File Offset: 0x00117708
		// (set) Token: 0x060041E7 RID: 16871 RVA: 0x00119524 File Offset: 0x00117724
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Height
		{
			get
			{
				return this.Bounds.Height;
			}
			set
			{
				Rectangle rectangle = this.Bounds;
				this.SetBounds(rectangle.X, rectangle.Y, rectangle.Width, value);
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x060041E8 RID: 16872 RVA: 0x00119554 File Offset: 0x00117754
		ArrangedElementCollection IArrangedElement.Children
		{
			get
			{
				return ToolStripItem.EmptyChildCollection;
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x060041E9 RID: 16873 RVA: 0x0011955B File Offset: 0x0011775B
		IArrangedElement IArrangedElement.Container
		{
			get
			{
				if (this.ParentInternal == null)
				{
					return this.Owner;
				}
				return this.ParentInternal;
			}
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x060041EA RID: 16874 RVA: 0x00114C20 File Offset: 0x00112E20
		Rectangle IArrangedElement.DisplayRectangle
		{
			get
			{
				return this.Bounds;
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x060041EB RID: 16875 RVA: 0x00118D50 File Offset: 0x00116F50
		bool IArrangedElement.ParticipatesInLayout
		{
			get
			{
				return this.state[ToolStripItem.stateVisible];
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x060041EC RID: 16876 RVA: 0x00119572 File Offset: 0x00117772
		PropertyStore IArrangedElement.Properties
		{
			get
			{
				return this.Properties;
			}
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x0011957A File Offset: 0x0011777A
		void IArrangedElement.SetBounds(Rectangle bounds, BoundsSpecified specified)
		{
			this.SetBounds(bounds);
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x000070A6 File Offset: 0x000052A6
		void IArrangedElement.PerformLayout(IArrangedElement container, string propertyName)
		{
		}

		/// <summary>Gets or sets the alignment of the image on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values.</exception>
		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x060041EF RID: 16879 RVA: 0x00119583 File Offset: 0x00117783
		// (set) Token: 0x060041F0 RID: 16880 RVA: 0x0011958B File Offset: 0x0011778B
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageAlignDescr")]
		public ContentAlignment ImageAlign
		{
			get
			{
				return this.imageAlign;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (this.imageAlign != value)
				{
					this.imageAlign = value;
					this.InvalidateItemLayout(PropertyNames.ImageAlign);
				}
			}
		}

		/// <summary>Gets or sets the image that is displayed on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> to be displayed.</returns>
		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x060041F1 RID: 16881 RVA: 0x001195C8 File Offset: 0x001177C8
		// (set) Token: 0x060041F2 RID: 16882 RVA: 0x00119684 File Offset: 0x00117884
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageDescr")]
		public virtual Image Image
		{
			get
			{
				Image image = (Image)this.Properties.GetObject(ToolStripItem.PropImage);
				if (image != null || this.Owner == null || this.Owner.ImageList == null || this.ImageIndexer.ActualIndex < 0)
				{
					return image;
				}
				if (this.ImageIndexer.ActualIndex < this.Owner.ImageList.Images.Count)
				{
					image = this.Owner.ImageList.Images[this.ImageIndexer.ActualIndex];
					this.state[ToolStripItem.stateInvalidMirroredImage] = true;
					this.Properties.SetObject(ToolStripItem.PropImage, image);
					return image;
				}
				return null;
			}
			set
			{
				if (this.Image != value)
				{
					this.StopAnimate();
					Bitmap bitmap = value as Bitmap;
					if (bitmap != null && this.ImageTransparentColor != Color.Empty)
					{
						if (bitmap.RawFormat.Guid != ImageFormat.Icon.Guid && !ImageAnimator.CanAnimate(bitmap))
						{
							bitmap.MakeTransparent(this.ImageTransparentColor);
						}
						value = bitmap;
					}
					if (value != null)
					{
						this.ImageIndex = -1;
					}
					this.Properties.SetObject(ToolStripItem.PropImage, value);
					this.state[ToolStripItem.stateInvalidMirroredImage] = true;
					this.Animate();
					this.InvalidateItemLayout(PropertyNames.Image);
				}
			}
		}

		/// <summary>Gets or sets the color to treat as transparent in a <see cref="T:System.Windows.Forms.ToolStripItem" /> image.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values.</returns>
		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x060041F3 RID: 16883 RVA: 0x0011972F File Offset: 0x0011792F
		// (set) Token: 0x060041F4 RID: 16884 RVA: 0x00119738 File Offset: 0x00117938
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemImageTransparentColorDescr")]
		public Color ImageTransparentColor
		{
			get
			{
				return this.imageTransparentColor;
			}
			set
			{
				if (this.imageTransparentColor != value)
				{
					this.imageTransparentColor = value;
					Bitmap bitmap = this.Image as Bitmap;
					if (bitmap != null && value != Color.Empty && bitmap.RawFormat.Guid != ImageFormat.Icon.Guid && !ImageAnimator.CanAnimate(bitmap))
					{
						bitmap.MakeTransparent(this.imageTransparentColor);
					}
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the index value of the image that is displayed on the item.</summary>
		/// <returns>The zero-based index of the image in the <see cref="P:System.Windows.Forms.ToolStrip.ImageList" /> that is displayed for the item. The default is -1, signifying that the image list is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified is less than -1.</exception>
		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x060041F5 RID: 16885 RVA: 0x001197AC File Offset: 0x001179AC
		// (set) Token: 0x060041F6 RID: 16886 RVA: 0x00119824 File Offset: 0x00117A24
		[SRDescription("ToolStripItemImageIndexDescr")]
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ToolStripImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Browsable(false)]
		[RelatedImageList("Owner.ImageList")]
		public int ImageIndex
		{
			get
			{
				if (this.Owner != null && this.ImageIndexer.Index != -1 && this.Owner.ImageList != null && this.ImageIndexer.Index >= this.Owner.ImageList.Images.Count)
				{
					return this.Owner.ImageList.Images.Count - 1;
				}
				return this.ImageIndexer.Index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("ImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"ImageIndex",
						value.ToString(CultureInfo.CurrentCulture),
						(-1).ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.ImageIndexer.Index = value;
				this.state[ToolStripItem.stateInvalidMirroredImage] = true;
				this.Properties.SetObject(ToolStripItem.PropImage, null);
				this.InvalidateItemLayout(PropertyNames.ImageIndex);
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x060041F7 RID: 16887 RVA: 0x001198B1 File Offset: 0x00117AB1
		internal ToolStripItemImageIndexer ImageIndexer
		{
			get
			{
				if (this.imageIndexer == null)
				{
					this.imageIndexer = new ToolStripItemImageIndexer(this);
				}
				return this.imageIndexer;
			}
		}

		/// <summary>Gets or sets the key accessor for the image in the <see cref="P:System.Windows.Forms.ToolStrip.ImageList" /> that is displayed on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A string representing the key of the image.</returns>
		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x060041F8 RID: 16888 RVA: 0x001198CD File Offset: 0x00117ACD
		// (set) Token: 0x060041F9 RID: 16889 RVA: 0x001198DA File Offset: 0x00117ADA
		[SRDescription("ToolStripItemImageKeyDescr")]
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[TypeConverter(typeof(ImageKeyConverter))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Editor("System.Windows.Forms.Design.ToolStripImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Browsable(false)]
		[RelatedImageList("Owner.ImageList")]
		public string ImageKey
		{
			get
			{
				return this.ImageIndexer.Key;
			}
			set
			{
				this.ImageIndexer.Key = value;
				this.state[ToolStripItem.stateInvalidMirroredImage] = true;
				this.Properties.SetObject(ToolStripItem.PropImage, null);
				this.InvalidateItemLayout(PropertyNames.ImageKey);
			}
		}

		/// <summary>Gets or sets a value indicating whether an image on a <see cref="T:System.Windows.Forms.ToolStripItem" /> is automatically resized to fit in a container.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemImageScaling" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemImageScaling.SizeToFit" />.</returns>
		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x060041FA RID: 16890 RVA: 0x00119915 File Offset: 0x00117B15
		// (set) Token: 0x060041FB RID: 16891 RVA: 0x00119920 File Offset: 0x00117B20
		[SRCategory("CatAppearance")]
		[DefaultValue(ToolStripItemImageScaling.SizeToFit)]
		[Localizable(true)]
		[SRDescription("ToolStripItemImageScalingDescr")]
		public ToolStripItemImageScaling ImageScaling
		{
			get
			{
				return this.imageScaling;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripItemImageScaling));
				}
				if (this.imageScaling != value)
				{
					this.imageScaling = value;
					this.InvalidateItemLayout(PropertyNames.ImageScaling);
				}
			}
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x060041FC RID: 16892 RVA: 0x0011996E File Offset: 0x00117B6E
		internal ToolStripItemInternalLayout InternalLayout
		{
			get
			{
				if (this.toolStripItemInternalLayout == null)
				{
					this.toolStripItemInternalLayout = this.CreateInternalLayout();
				}
				return this.toolStripItemInternalLayout;
			}
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x060041FD RID: 16893 RVA: 0x0011998C File Offset: 0x00117B8C
		internal bool IsForeColorSet
		{
			get
			{
				if (!this.Properties.GetColor(ToolStripItem.PropForeColor).IsEmpty)
				{
					return true;
				}
				Control parentInternal = this.ParentInternal;
				return parentInternal != null && parentInternal.ShouldSerializeForeColor();
			}
		}

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x060041FE RID: 16894 RVA: 0x0010C201 File Offset: 0x0010A401
		internal bool IsInDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		/// <summary>Gets a value indicating whether the object has been disposed of.</summary>
		/// <returns>
		///   <see langword="true" /> if the control has been disposed of; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x060041FF RID: 16895 RVA: 0x001199C7 File Offset: 0x00117BC7
		[Browsable(false)]
		public bool IsDisposed
		{
			get
			{
				return this.state[ToolStripItem.stateDisposed];
			}
		}

		/// <summary>Gets a value indicating whether the container of the current <see cref="T:System.Windows.Forms.Control" /> is a <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the container of the current <see cref="T:System.Windows.Forms.Control" /> is a <see cref="T:System.Windows.Forms.ToolStripDropDown" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06004200 RID: 16896 RVA: 0x001199D9 File Offset: 0x00117BD9
		[Browsable(false)]
		public bool IsOnDropDown
		{
			get
			{
				if (this.ParentInternal != null)
				{
					return this.ParentInternal.IsDropDown;
				}
				return this.Owner != null && this.Owner.IsDropDown;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.ToolStripItem.Placement" /> property is set to <see cref="F:System.Windows.Forms.ToolStripItemPlacement.Overflow" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.ToolStripItem.Placement" /> property is set to <see cref="F:System.Windows.Forms.ToolStripItemPlacement.Overflow" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06004201 RID: 16897 RVA: 0x00119A07 File Offset: 0x00117C07
		[Browsable(false)]
		public bool IsOnOverflow
		{
			get
			{
				return this.Placement == ToolStripItemPlacement.Overflow;
			}
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06004202 RID: 16898 RVA: 0x00012E4E File Offset: 0x0001104E
		internal virtual bool IsMnemonicsListenerAxSourced
		{
			get
			{
				return true;
			}
		}

		/// <summary>Occurs when the location of a <see cref="T:System.Windows.Forms.ToolStripItem" /> is updated.</summary>
		// Token: 0x14000345 RID: 837
		// (add) Token: 0x06004203 RID: 16899 RVA: 0x00119A12 File Offset: 0x00117C12
		// (remove) Token: 0x06004204 RID: 16900 RVA: 0x00119A25 File Offset: 0x00117C25
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripItemOnLocationChangedDescr")]
		public event EventHandler LocationChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventLocationChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventLocationChanged, value);
			}
		}

		/// <summary>Gets or sets the space between the item and adjacent items.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the space between the item and adjacent items.</returns>
		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x06004205 RID: 16901 RVA: 0x00019A7D File Offset: 0x00017C7D
		// (set) Token: 0x06004206 RID: 16902 RVA: 0x00119A38 File Offset: 0x00117C38
		[SRDescription("ToolStripItemMarginDescr")]
		[SRCategory("CatLayout")]
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
					this.state[ToolStripItem.stateUseAmbientMargin] = false;
					CommonProperties.SetMargin(this, value);
				}
			}
		}

		/// <summary>Gets or sets how child menus are merged with parent menus.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.MergeAction" /> values. The default is <see cref="F:System.Windows.Forms.MergeAction.MatchOnly" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.MergeAction" /> values.</exception>
		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06004207 RID: 16903 RVA: 0x00119A60 File Offset: 0x00117C60
		// (set) Token: 0x06004208 RID: 16904 RVA: 0x00119A86 File Offset: 0x00117C86
		[SRDescription("ToolStripMergeActionDescr")]
		[DefaultValue(MergeAction.Append)]
		[SRCategory("CatLayout")]
		public MergeAction MergeAction
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(ToolStripItem.PropMergeAction, out flag);
				if (flag)
				{
					return (MergeAction)integer;
				}
				return MergeAction.Append;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(MergeAction));
				}
				this.Properties.SetInteger(ToolStripItem.PropMergeAction, (int)value);
			}
		}

		/// <summary>Gets or sets the position of a merged item within the current <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>An integer representing the index of the merged item, if a match is found, or -1 if a match is not found.</returns>
		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06004209 RID: 16905 RVA: 0x00119AC0 File Offset: 0x00117CC0
		// (set) Token: 0x0600420A RID: 16906 RVA: 0x00119AE6 File Offset: 0x00117CE6
		[SRDescription("ToolStripMergeIndexDescr")]
		[DefaultValue(-1)]
		[SRCategory("CatLayout")]
		public int MergeIndex
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(ToolStripItem.PropMergeIndex, out flag);
				if (flag)
				{
					return integer;
				}
				return -1;
			}
			set
			{
				this.Properties.SetInteger(ToolStripItem.PropMergeIndex, value);
			}
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x00119AF9 File Offset: 0x00117CF9
		// (set) Token: 0x0600420C RID: 16908 RVA: 0x00119B0B File Offset: 0x00117D0B
		internal bool MouseDownAndUpMustBeInSameItem
		{
			get
			{
				return this.state[ToolStripItem.stateMouseDownAndUpMustBeInSameItem];
			}
			set
			{
				this.state[ToolStripItem.stateMouseDownAndUpMustBeInSameItem] = value;
			}
		}

		/// <summary>Occurs when the mouse pointer is over the item and a mouse button is pressed.</summary>
		// Token: 0x14000346 RID: 838
		// (add) Token: 0x0600420D RID: 16909 RVA: 0x00119B1E File Offset: 0x00117D1E
		// (remove) Token: 0x0600420E RID: 16910 RVA: 0x00119B31 File Offset: 0x00117D31
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseDownDescr")]
		public event MouseEventHandler MouseDown
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventMouseDown, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventMouseDown, value);
			}
		}

		/// <summary>Occurs when the mouse pointer enters the item.</summary>
		// Token: 0x14000347 RID: 839
		// (add) Token: 0x0600420F RID: 16911 RVA: 0x00119B44 File Offset: 0x00117D44
		// (remove) Token: 0x06004210 RID: 16912 RVA: 0x00119B57 File Offset: 0x00117D57
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseEnterDescr")]
		public event EventHandler MouseEnter
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventMouseEnter, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventMouseEnter, value);
			}
		}

		/// <summary>Occurs when the mouse pointer leaves the item.</summary>
		// Token: 0x14000348 RID: 840
		// (add) Token: 0x06004211 RID: 16913 RVA: 0x00119B6A File Offset: 0x00117D6A
		// (remove) Token: 0x06004212 RID: 16914 RVA: 0x00119B7D File Offset: 0x00117D7D
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseLeaveDescr")]
		public event EventHandler MouseLeave
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventMouseLeave, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventMouseLeave, value);
			}
		}

		/// <summary>Occurs when the mouse pointer hovers over the item.</summary>
		// Token: 0x14000349 RID: 841
		// (add) Token: 0x06004213 RID: 16915 RVA: 0x00119B90 File Offset: 0x00117D90
		// (remove) Token: 0x06004214 RID: 16916 RVA: 0x00119BA3 File Offset: 0x00117DA3
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseHoverDescr")]
		public event EventHandler MouseHover
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventMouseHover, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventMouseHover, value);
			}
		}

		/// <summary>Occurs when the mouse pointer is moved over the item.</summary>
		// Token: 0x1400034A RID: 842
		// (add) Token: 0x06004215 RID: 16917 RVA: 0x00119BB6 File Offset: 0x00117DB6
		// (remove) Token: 0x06004216 RID: 16918 RVA: 0x00119BC9 File Offset: 0x00117DC9
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseMoveDescr")]
		public event MouseEventHandler MouseMove
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventMouseMove, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventMouseMove, value);
			}
		}

		/// <summary>Occurs when the mouse pointer is over the item and a mouse button is released.</summary>
		// Token: 0x1400034B RID: 843
		// (add) Token: 0x06004217 RID: 16919 RVA: 0x00119BDC File Offset: 0x00117DDC
		// (remove) Token: 0x06004218 RID: 16920 RVA: 0x00119BEF File Offset: 0x00117DEF
		[SRCategory("CatMouse")]
		[SRDescription("ToolStripItemOnMouseUpDescr")]
		public event MouseEventHandler MouseUp
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventMouseUp, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventMouseUp, value);
			}
		}

		/// <summary>Gets or sets the name of the item.</summary>
		/// <returns>A string representing the name. The default value is <see langword="null" />.</returns>
		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06004219 RID: 16921 RVA: 0x00119C02 File Offset: 0x00117E02
		// (set) Token: 0x0600421A RID: 16922 RVA: 0x00119C1F File Offset: 0x00117E1F
		[Browsable(false)]
		[DefaultValue(null)]
		public string Name
		{
			get
			{
				return WindowsFormsUtils.GetComponentName(this, (string)this.Properties.GetObject(ToolStripItem.PropName));
			}
			set
			{
				if (base.DesignMode)
				{
					return;
				}
				this.Properties.SetObject(ToolStripItem.PropName, value);
			}
		}

		/// <summary>Gets or sets the owner of this item.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> that owns or is to own the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x0600421B RID: 16923 RVA: 0x00119C3B File Offset: 0x00117E3B
		// (set) Token: 0x0600421C RID: 16924 RVA: 0x00119C43 File Offset: 0x00117E43
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStrip Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				if (this.owner != value)
				{
					if (this.owner != null)
					{
						this.owner.Items.Remove(this);
					}
					if (value != null)
					{
						value.Items.Add(this);
					}
				}
			}
		}

		/// <summary>Gets the parent <see cref="T:System.Windows.Forms.ToolStripItem" /> of this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>The parent <see cref="T:System.Windows.Forms.ToolStripItem" /> of this <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x0600421D RID: 16925 RVA: 0x00119C78 File Offset: 0x00117E78
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStripItem OwnerItem
		{
			get
			{
				ToolStripDropDown toolStripDropDown = null;
				if (this.ParentInternal != null)
				{
					toolStripDropDown = this.ParentInternal as ToolStripDropDown;
				}
				else if (this.Owner != null)
				{
					toolStripDropDown = this.Owner as ToolStripDropDown;
				}
				if (toolStripDropDown != null)
				{
					return toolStripDropDown.OwnerItem;
				}
				return null;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.Owner" /> property changes.</summary>
		// Token: 0x1400034C RID: 844
		// (add) Token: 0x0600421E RID: 16926 RVA: 0x00119CBC File Offset: 0x00117EBC
		// (remove) Token: 0x0600421F RID: 16927 RVA: 0x00119CCF File Offset: 0x00117ECF
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripItemOwnerChangedDescr")]
		public event EventHandler OwnerChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventOwnerChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventOwnerChanged, value);
			}
		}

		/// <summary>Occurs when the item is redrawn.</summary>
		// Token: 0x1400034D RID: 845
		// (add) Token: 0x06004220 RID: 16928 RVA: 0x00119CE2 File Offset: 0x00117EE2
		// (remove) Token: 0x06004221 RID: 16929 RVA: 0x00119CF5 File Offset: 0x00117EF5
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemOnPaintDescr")]
		public event PaintEventHandler Paint
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventPaint, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventPaint, value);
			}
		}

		/// <summary>Gets or sets the parent container of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStrip" /> that is the parent container of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06004222 RID: 16930 RVA: 0x00119D08 File Offset: 0x00117F08
		// (set) Token: 0x06004223 RID: 16931 RVA: 0x00119D10 File Offset: 0x00117F10
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected internal ToolStrip Parent
		{
			get
			{
				return this.ParentInternal;
			}
			set
			{
				this.ParentInternal = value;
			}
		}

		/// <summary>Gets or sets whether the item is attached to the <see cref="T:System.Windows.Forms.ToolStrip" /> or <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> or can float between the two.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemOverflow" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemOverflow.AsNeeded" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.ToolStripItemOverflow" /> values.</exception>
		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06004224 RID: 16932 RVA: 0x00119D19 File Offset: 0x00117F19
		// (set) Token: 0x06004225 RID: 16933 RVA: 0x00119D24 File Offset: 0x00117F24
		[DefaultValue(ToolStripItemOverflow.AsNeeded)]
		[SRDescription("ToolStripItemOverflowDescr")]
		[SRCategory("CatLayout")]
		public ToolStripItemOverflow Overflow
		{
			get
			{
				return this.overflow;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripGripStyle));
				}
				if (this.overflow != value)
				{
					this.overflow = value;
					if (this.Owner != null)
					{
						LayoutTransaction.DoLayout(this.Owner, this.Owner, "Overflow");
					}
				}
			}
		}

		/// <summary>Gets or sets the internal spacing, in pixels, between the item's contents and its edges.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the item's internal spacing, in pixels.</returns>
		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x06004226 RID: 16934 RVA: 0x00119D85 File Offset: 0x00117F85
		// (set) Token: 0x06004227 RID: 16935 RVA: 0x00119D93 File Offset: 0x00117F93
		[SRDescription("ToolStripItemPaddingDescr")]
		[SRCategory("CatLayout")]
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
					this.InvalidateItemLayout(PropertyNames.Padding);
				}
			}
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x06004228 RID: 16936 RVA: 0x00119DB5 File Offset: 0x00117FB5
		// (set) Token: 0x06004229 RID: 16937 RVA: 0x00119DC0 File Offset: 0x00117FC0
		internal ToolStrip ParentInternal
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (this.parent != value)
				{
					ToolStrip toolStrip = this.parent;
					this.parent = value;
					this.OnParentChanged(toolStrip, value);
				}
			}
		}

		/// <summary>Gets the current layout of the item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemPlacement" /> values.</returns>
		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x0600422A RID: 16938 RVA: 0x00119DEC File Offset: 0x00117FEC
		[Browsable(false)]
		public ToolStripItemPlacement Placement
		{
			get
			{
				return this.placement;
			}
		}

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x0600422B RID: 16939 RVA: 0x00119DF4 File Offset: 0x00117FF4
		internal Size PreferredImageSize
		{
			get
			{
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Image) != ToolStripItemDisplayStyle.Image)
				{
					return Size.Empty;
				}
				Image image = (Image)this.Properties.GetObject(ToolStripItem.PropImage);
				bool flag = this.Owner != null && this.Owner.ImageList != null && this.ImageIndexer.ActualIndex >= 0;
				if (this.ImageScaling == ToolStripItemImageScaling.SizeToFit)
				{
					ToolStrip toolStrip = this.Owner;
					if (toolStrip != null && (image != null || flag))
					{
						return toolStrip.ImageScalingSize;
					}
				}
				Size size = Size.Empty;
				if (flag)
				{
					size = this.Owner.ImageList.ImageSize;
				}
				else
				{
					size = ((image == null) ? Size.Empty : image.Size);
				}
				return size;
			}
		}

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x0600422C RID: 16940 RVA: 0x00119EA2 File Offset: 0x001180A2
		internal PropertyStore Properties
		{
			get
			{
				if (this.propertyStore == null)
				{
					this.propertyStore = new PropertyStore();
				}
				return this.propertyStore;
			}
		}

		/// <summary>Gets a value indicating whether the state of the item is pressed.</summary>
		/// <returns>
		///   <see langword="true" /> if the state of the item is pressed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x0600422D RID: 16941 RVA: 0x00119EBD File Offset: 0x001180BD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Pressed
		{
			get
			{
				return this.CanSelect && this.state[ToolStripItem.statePressed];
			}
		}

		/// <summary>Occurs during a drag-and-drop operation and allows the drag source to determine whether the drag-and-drop operation should be canceled.</summary>
		// Token: 0x1400034E RID: 846
		// (add) Token: 0x0600422E RID: 16942 RVA: 0x00119ED9 File Offset: 0x001180D9
		// (remove) Token: 0x0600422F RID: 16943 RVA: 0x00119EEC File Offset: 0x001180EC
		[SRCategory("CatDragDrop")]
		[SRDescription("ToolStripItemOnQueryContinueDragDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventQueryContinueDrag, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventQueryContinueDrag, value);
			}
		}

		/// <summary>Occurs when an accessibility client application invokes help for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		// Token: 0x1400034F RID: 847
		// (add) Token: 0x06004230 RID: 16944 RVA: 0x00119EFF File Offset: 0x001180FF
		// (remove) Token: 0x06004231 RID: 16945 RVA: 0x00119F12 File Offset: 0x00118112
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripItemOnQueryAccessibilityHelpDescr")]
		public event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventQueryAccessibilityHelp, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventQueryAccessibilityHelp, value);
			}
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06004232 RID: 16946 RVA: 0x00119F25 File Offset: 0x00118125
		internal Color RawBackColor
		{
			get
			{
				return this.Properties.GetColor(ToolStripItem.PropBackColor);
			}
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06004233 RID: 16947 RVA: 0x00119F37 File Offset: 0x00118137
		internal ToolStripRenderer Renderer
		{
			get
			{
				if (this.Owner != null)
				{
					return this.Owner.Renderer;
				}
				if (this.ParentInternal == null)
				{
					return null;
				}
				return this.ParentInternal.Renderer;
			}
		}

		/// <summary>Gets or sets a value indicating whether items are to be placed from right to left and text is to be written from right to left.</summary>
		/// <returns>
		///   <see langword="true" /> if items are to be placed from right to left and text is to be written from right to left; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x06004234 RID: 16948 RVA: 0x00119F64 File Offset: 0x00118164
		// (set) Token: 0x06004235 RID: 16949 RVA: 0x00119FC4 File Offset: 0x001181C4
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolStripItemRightToLeftDescr")]
		public virtual RightToLeft RightToLeft
		{
			get
			{
				bool flag;
				int num = this.Properties.GetInteger(ToolStripItem.PropRightToLeft, out flag);
				if (!flag)
				{
					num = 2;
				}
				if (num == 2)
				{
					if (this.Owner != null)
					{
						num = (int)this.Owner.RightToLeft;
					}
					else if (this.ParentInternal != null)
					{
						num = (int)this.ParentInternal.RightToLeft;
					}
					else
					{
						num = (int)this.DefaultRightToLeft;
					}
				}
				return (RightToLeft)num;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("RightToLeft", (int)value, typeof(RightToLeft));
				}
				RightToLeft rightToLeft = this.RightToLeft;
				if (this.Properties.ContainsInteger(ToolStripItem.PropRightToLeft) || value != RightToLeft.Inherit)
				{
					this.Properties.SetInteger(ToolStripItem.PropRightToLeft, (int)value);
				}
				if (rightToLeft != this.RightToLeft)
				{
					this.OnRightToLeftChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Mirrors automatically the <see cref="T:System.Windows.Forms.ToolStripItem" /> image when the <see cref="P:System.Windows.Forms.ToolStripItem.RightToLeft" /> property is set to <see cref="F:System.Windows.Forms.RightToLeft.Yes" />.</summary>
		/// <returns>
		///   <see langword="true" /> to automatically mirror the image; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x06004236 RID: 16950 RVA: 0x0011A039 File Offset: 0x00118239
		// (set) Token: 0x06004237 RID: 16951 RVA: 0x0011A04B File Offset: 0x0011824B
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolStripItemRightToLeftAutoMirrorImageDescr")]
		public bool RightToLeftAutoMirrorImage
		{
			get
			{
				return this.state[ToolStripItem.stateRightToLeftAutoMirrorImage];
			}
			set
			{
				if (this.state[ToolStripItem.stateRightToLeftAutoMirrorImage] != value)
				{
					this.state[ToolStripItem.stateRightToLeftAutoMirrorImage] = value;
					this.Invalidate();
				}
			}
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06004238 RID: 16952 RVA: 0x0011A078 File Offset: 0x00118278
		internal Image MirroredImage
		{
			get
			{
				if (!this.state[ToolStripItem.stateInvalidMirroredImage])
				{
					return this.Properties.GetObject(ToolStripItem.PropMirroredImage) as Image;
				}
				Image image = this.Image;
				if (image != null)
				{
					Image image2 = image.Clone() as Image;
					image2.RotateFlip(RotateFlipType.RotateNoneFlipX);
					this.Properties.SetObject(ToolStripItem.PropMirroredImage, image2);
					this.state[ToolStripItem.stateInvalidMirroredImage] = false;
					return image2;
				}
				return null;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.RightToLeft" /> property value changes.</summary>
		// Token: 0x14000350 RID: 848
		// (add) Token: 0x06004239 RID: 16953 RVA: 0x0011A0EF File Offset: 0x001182EF
		// (remove) Token: 0x0600423A RID: 16954 RVA: 0x0011A102 File Offset: 0x00118302
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnRightToLeftChangedDescr")]
		public event EventHandler RightToLeftChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventRightToLeft, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventRightToLeft, value);
			}
		}

		/// <summary>Gets a value indicating whether the item is selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is selected; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x0600423B RID: 16955 RVA: 0x0011A118 File Offset: 0x00118318
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Selected
		{
			get
			{
				return this.CanSelect && !base.DesignMode && (this.state[ToolStripItem.stateSelected] || (this.ParentInternal != null && this.ParentInternal.IsSelectionSuspended && this.ParentInternal.LastMouseDownedItem == this));
			}
		}

		/// <summary>Gets a value indicating whether to show or hide shortcut keys.</summary>
		/// <returns>
		///   <see langword="true" /> to show shortcut keys; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x0600423C RID: 16956 RVA: 0x0011A170 File Offset: 0x00118370
		protected internal virtual bool ShowKeyboardCues
		{
			get
			{
				return base.DesignMode || ToolStripManager.ShowMenuFocusCues;
			}
		}

		/// <summary>Gets or sets the size of the item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" />, representing the width and height of a rectangle.</returns>
		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x0600423D RID: 16957 RVA: 0x0011A184 File Offset: 0x00118384
		// (set) Token: 0x0600423E RID: 16958 RVA: 0x0011A1A0 File Offset: 0x001183A0
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("ToolStripItemSizeDescr")]
		public virtual Size Size
		{
			get
			{
				return this.Bounds.Size;
			}
			set
			{
				Rectangle rectangle = this.Bounds;
				rectangle.Size = value;
				this.SetBounds(rectangle);
			}
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x0600423F RID: 16959 RVA: 0x0011A1C3 File Offset: 0x001183C3
		// (set) Token: 0x06004240 RID: 16960 RVA: 0x0011A1D5 File Offset: 0x001183D5
		internal bool SupportsRightClick
		{
			get
			{
				return this.state[ToolStripItem.stateSupportsRightClick];
			}
			set
			{
				this.state[ToolStripItem.stateSupportsRightClick] = value;
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x06004241 RID: 16961 RVA: 0x0011A1E8 File Offset: 0x001183E8
		// (set) Token: 0x06004242 RID: 16962 RVA: 0x0011A1FA File Offset: 0x001183FA
		internal bool SupportsItemClick
		{
			get
			{
				return this.state[ToolStripItem.stateSupportsItemClick];
			}
			set
			{
				this.state[ToolStripItem.stateSupportsItemClick] = value;
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x06004243 RID: 16963 RVA: 0x0011A20D File Offset: 0x0011840D
		// (set) Token: 0x06004244 RID: 16964 RVA: 0x0011A21F File Offset: 0x0011841F
		internal bool SupportsSpaceKey
		{
			get
			{
				return this.state[ToolStripItem.stateSupportsSpaceKey];
			}
			set
			{
				this.state[ToolStripItem.stateSupportsSpaceKey] = value;
			}
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x06004245 RID: 16965 RVA: 0x0011A232 File Offset: 0x00118432
		// (set) Token: 0x06004246 RID: 16966 RVA: 0x0011A244 File Offset: 0x00118444
		internal bool SupportsDisabledHotTracking
		{
			get
			{
				return this.state[ToolStripItem.stateSupportsDisabledHotTracking];
			}
			set
			{
				this.state[ToolStripItem.stateSupportsDisabledHotTracking] = value;
			}
		}

		/// <summary>Gets or sets the object that contains data about the item.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the control. The default is <see langword="null" />.</returns>
		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06004247 RID: 16967 RVA: 0x0011A257 File Offset: 0x00118457
		// (set) Token: 0x06004248 RID: 16968 RVA: 0x0011A27D File Offset: 0x0011847D
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ToolStripItemTagDescr")]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				if (this.Properties.ContainsObject(ToolStripItem.PropTag))
				{
					return this.propertyStore.GetObject(ToolStripItem.PropTag);
				}
				return null;
			}
			set
			{
				this.Properties.SetObject(ToolStripItem.PropTag, value);
			}
		}

		/// <summary>Gets or sets the text that is to be displayed on the item.</summary>
		/// <returns>A string representing the item's text. The default value is the empty string ("").</returns>
		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06004249 RID: 16969 RVA: 0x0011A290 File Offset: 0x00118490
		// (set) Token: 0x0600424A RID: 16970 RVA: 0x0011A2BF File Offset: 0x001184BF
		[DefaultValue("")]
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolStripItemTextDescr")]
		public virtual string Text
		{
			get
			{
				if (this.Properties.ContainsObject(ToolStripItem.PropText))
				{
					return (string)this.Properties.GetObject(ToolStripItem.PropText);
				}
				return "";
			}
			set
			{
				if (value != this.Text)
				{
					this.Properties.SetObject(ToolStripItem.PropText, value);
					this.OnTextChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the alignment of the text on a <see cref="T:System.Windows.Forms.ToolStripLabel" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleRight" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values.</exception>
		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x0600424B RID: 16971 RVA: 0x0011A2EB File Offset: 0x001184EB
		// (set) Token: 0x0600424C RID: 16972 RVA: 0x0011A2F3 File Offset: 0x001184F3
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripItemTextAlignDescr")]
		public virtual ContentAlignment TextAlign
		{
			get
			{
				return this.textAlign;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (this.textAlign != value)
				{
					this.textAlign = value;
					this.InvalidateItemLayout(PropertyNames.TextAlign);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripItem.Text" /> property changes.</summary>
		// Token: 0x14000351 RID: 849
		// (add) Token: 0x0600424D RID: 16973 RVA: 0x0011A32E File Offset: 0x0011852E
		// (remove) Token: 0x0600424E RID: 16974 RVA: 0x0011A341 File Offset: 0x00118541
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnTextChangedDescr")]
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventText, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventText, value);
			}
		}

		/// <summary>Gets the orientation of text used on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values.</returns>
		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x0600424F RID: 16975 RVA: 0x0011A354 File Offset: 0x00118554
		// (set) Token: 0x06004250 RID: 16976 RVA: 0x0011A3BC File Offset: 0x001185BC
		[SRDescription("ToolStripTextDirectionDescr")]
		[SRCategory("CatAppearance")]
		public virtual ToolStripTextDirection TextDirection
		{
			get
			{
				ToolStripTextDirection toolStripTextDirection = ToolStripTextDirection.Inherit;
				if (this.Properties.ContainsObject(ToolStripItem.PropTextDirection))
				{
					toolStripTextDirection = (ToolStripTextDirection)this.Properties.GetObject(ToolStripItem.PropTextDirection);
				}
				if (toolStripTextDirection == ToolStripTextDirection.Inherit)
				{
					if (this.ParentInternal != null)
					{
						toolStripTextDirection = this.ParentInternal.TextDirection;
					}
					else
					{
						toolStripTextDirection = ((this.Owner == null) ? ToolStripTextDirection.Horizontal : this.Owner.TextDirection);
					}
				}
				return toolStripTextDirection;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolStripTextDirection));
				}
				this.Properties.SetObject(ToolStripItem.PropTextDirection, value);
				this.InvalidateItemLayout("TextDirection");
			}
		}

		/// <summary>Gets or sets the position of <see cref="T:System.Windows.Forms.ToolStripItem" /> text and image relative to each other.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TextImageRelation" /> values. The default is <see cref="F:System.Windows.Forms.TextImageRelation.ImageBeforeText" />.</returns>
		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x06004251 RID: 16977 RVA: 0x0011A410 File Offset: 0x00118610
		// (set) Token: 0x06004252 RID: 16978 RVA: 0x0011A418 File Offset: 0x00118618
		[DefaultValue(TextImageRelation.ImageBeforeText)]
		[Localizable(true)]
		[SRDescription("ToolStripItemTextImageRelationDescr")]
		[SRCategory("CatAppearance")]
		public TextImageRelation TextImageRelation
		{
			get
			{
				return this.textImageRelation;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidTextImageRelation(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(TextImageRelation));
				}
				if (value != this.TextImageRelation)
				{
					this.textImageRelation = value;
					this.InvalidateItemLayout(PropertyNames.TextImageRelation);
				}
			}
		}

		/// <summary>Gets or sets the text that appears as a <see cref="T:System.Windows.Forms.ToolTip" /> for a control.</summary>
		/// <returns>A string representing the ToolTip text.</returns>
		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x06004253 RID: 16979 RVA: 0x0011A454 File Offset: 0x00118654
		// (set) Token: 0x06004254 RID: 16980 RVA: 0x0011A4A9 File Offset: 0x001186A9
		[SRDescription("ToolStripItemToolTipTextDescr")]
		[SRCategory("CatBehavior")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		public string ToolTipText
		{
			get
			{
				if (this.AutoToolTip && string.IsNullOrEmpty(this.toolTipText))
				{
					string text = this.Text;
					if (WindowsFormsUtils.ContainsMnemonic(text))
					{
						text = string.Join("", text.Split(new char[] { '&' }));
					}
					return text;
				}
				return this.toolTipText;
			}
			set
			{
				this.toolTipText = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the item is displayed.</summary>
		/// <returns>
		///   <see langword="true" /> if the item is displayed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x06004255 RID: 16981 RVA: 0x0011A4B2 File Offset: 0x001186B2
		// (set) Token: 0x06004256 RID: 16982 RVA: 0x00118D62 File Offset: 0x00116F62
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("ToolStripItemVisibleDescr")]
		public bool Visible
		{
			get
			{
				return this.ParentInternal != null && this.ParentInternal.Visible && this.Available;
			}
			set
			{
				this.SetVisibleCore(value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripItem.Visible" /> property changes.</summary>
		// Token: 0x14000352 RID: 850
		// (add) Token: 0x06004257 RID: 16983 RVA: 0x0011A4D1 File Offset: 0x001186D1
		// (remove) Token: 0x06004258 RID: 16984 RVA: 0x0011A4E4 File Offset: 0x001186E4
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripItemOnVisibleChangedDescr")]
		public event EventHandler VisibleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripItem.EventVisibleChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripItem.EventVisibleChanged, value);
			}
		}

		/// <summary>Gets or sets the width in pixels of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the width in pixels.</returns>
		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x06004259 RID: 16985 RVA: 0x0011A4F8 File Offset: 0x001186F8
		// (set) Token: 0x0600425A RID: 16986 RVA: 0x0011A514 File Offset: 0x00118714
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Width
		{
			get
			{
				return this.Bounds.Width;
			}
			set
			{
				Rectangle rectangle = this.Bounds;
				this.SetBounds(rectangle.X, rectangle.Y, value, rectangle.Height);
			}
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x0011A544 File Offset: 0x00118744
		internal void AccessibilityNotifyClients(AccessibleEvents accEvent)
		{
			if (this.ParentInternal != null)
			{
				int num = this.ParentInternal.DisplayedItems.IndexOf(this);
				this.ParentInternal.AccessibilityNotifyClients(accEvent, num);
			}
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x0011A578 File Offset: 0x00118778
		private void Animate()
		{
			this.Animate(!base.DesignMode && this.Visible && this.Enabled && this.ParentInternal != null);
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x0011A5A4 File Offset: 0x001187A4
		private void StopAnimate()
		{
			this.Animate(false);
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x0011A5B0 File Offset: 0x001187B0
		private void Animate(bool animate)
		{
			if (animate != this.state[ToolStripItem.stateCurrentlyAnimatingImage])
			{
				if (animate)
				{
					if (this.Image != null)
					{
						ImageAnimator.Animate(this.Image, new EventHandler(this.OnAnimationFrameChanged));
						this.state[ToolStripItem.stateCurrentlyAnimatingImage] = animate;
						return;
					}
				}
				else if (this.Image != null)
				{
					ImageAnimator.StopAnimate(this.Image, new EventHandler(this.OnAnimationFrameChanged));
					this.state[ToolStripItem.stateCurrentlyAnimatingImage] = animate;
				}
			}
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x0011A634 File Offset: 0x00118834
		internal bool BeginDragForItemReorder()
		{
			if (Control.ModifierKeys == Keys.Alt && this.ParentInternal.Items.Contains(this) && this.ParentInternal.AllowItemReorder)
			{
				this.DoDragDrop(this, DragDropEffects.Move);
				return true;
			}
			return false;
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x06004260 RID: 16992 RVA: 0x0011A67B File Offset: 0x0011887B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripItem.ToolStripItemAccessibleObject(this);
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x0011A683 File Offset: 0x00118883
		internal virtual ToolStripItemInternalLayout CreateInternalLayout()
		{
			return new ToolStripItemInternalLayout(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripItem" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06004262 RID: 16994 RVA: 0x0011A68C File Offset: 0x0011888C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.state[ToolStripItem.stateDisposing] = true;
				if (this.Owner != null)
				{
					this.StopAnimate();
					this.Owner.Items.Remove(this);
					this.toolStripItemInternalLayout = null;
					this.state[ToolStripItem.stateDisposed] = true;
				}
			}
			base.Dispose(disposing);
			if (disposing)
			{
				this.Properties.SetObject(ToolStripItem.PropMirroredImage, null);
				this.Properties.SetObject(ToolStripItem.PropImage, null);
				this.state[ToolStripItem.stateDisposing] = false;
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x06004263 RID: 16995 RVA: 0x0011A721 File Offset: 0x00118921
		internal static long DoubleClickTicks
		{
			get
			{
				return (long)(SystemInformation.DoubleClickTime * 10000);
			}
		}

		/// <summary>Begins a drag-and-drop operation.</summary>
		/// <param name="data">The object to be dragged.</param>
		/// <param name="allowedEffects">The drag operations that can occur.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</returns>
		// Token: 0x06004264 RID: 16996 RVA: 0x0011A730 File Offset: 0x00118930
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[UIPermission(SecurityAction.Demand, Clipboard = UIPermissionClipboard.OwnClipboard)]
		public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects)
		{
			int[] array = new int[1];
			UnsafeNativeMethods.IOleDropSource dropSource = this.DropSource;
			IDataObject dataObject = data as IDataObject;
			if (dataObject == null)
			{
				IDataObject dataObject2 = data as IDataObject;
				DataObject dataObject3;
				if (dataObject2 != null)
				{
					dataObject3 = new DataObject(dataObject2);
				}
				else if (data is ToolStripItem)
				{
					dataObject3 = new DataObject();
					dataObject3.SetData(typeof(ToolStripItem).ToString(), data);
				}
				else
				{
					dataObject3 = new DataObject();
					dataObject3.SetData(data);
				}
				dataObject = dataObject3;
			}
			try
			{
				SafeNativeMethods.DoDragDrop(dataObject, dropSource, (int)allowedEffects, array);
			}
			catch
			{
			}
			return (DragDropEffects)array[0];
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x0011A7C8 File Offset: 0x001189C8
		internal void FireEvent(ToolStripItemEventType met)
		{
			this.FireEvent(new EventArgs(), met);
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x0011A7D8 File Offset: 0x001189D8
		internal void FireEvent(EventArgs e, ToolStripItemEventType met)
		{
			switch (met)
			{
			case ToolStripItemEventType.Paint:
				this.HandlePaint(e as PaintEventArgs);
				return;
			case ToolStripItemEventType.LocationChanged:
				this.OnLocationChanged(e);
				return;
			case ToolStripItemEventType.MouseMove:
				if (!this.Enabled && this.ParentInternal != null)
				{
					this.BeginDragForItemReorder();
					return;
				}
				this.FireEventInteractive(e, met);
				return;
			case ToolStripItemEventType.MouseEnter:
				this.HandleMouseEnter(e);
				return;
			case ToolStripItemEventType.MouseLeave:
				if (!this.Enabled && this.ParentInternal != null)
				{
					this.ParentInternal.UpdateToolTip(null);
					return;
				}
				this.HandleMouseLeave(e);
				return;
			case ToolStripItemEventType.MouseHover:
				if (!this.Enabled && this.ParentInternal != null && !string.IsNullOrEmpty(this.ToolTipText))
				{
					this.ParentInternal.UpdateToolTip(this);
					return;
				}
				this.FireEventInteractive(e, met);
				return;
			}
			this.FireEventInteractive(e, met);
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x0011A8B0 File Offset: 0x00118AB0
		internal void FireEventInteractive(EventArgs e, ToolStripItemEventType met)
		{
			if (this.Enabled)
			{
				switch (met)
				{
				case ToolStripItemEventType.MouseUp:
					this.HandleMouseUp(e as MouseEventArgs);
					return;
				case ToolStripItemEventType.MouseDown:
					this.HandleMouseDown(e as MouseEventArgs);
					return;
				case ToolStripItemEventType.MouseMove:
					this.HandleMouseMove(e as MouseEventArgs);
					return;
				case ToolStripItemEventType.MouseEnter:
				case ToolStripItemEventType.MouseLeave:
					break;
				case ToolStripItemEventType.MouseHover:
					this.HandleMouseHover(e);
					return;
				case ToolStripItemEventType.Click:
					this.HandleClick(e);
					return;
				case ToolStripItemEventType.DoubleClick:
					this.HandleDoubleClick(e);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x0011A92C File Offset: 0x00118B2C
		private Font GetOwnerFont()
		{
			if (this.Owner != null)
			{
				return this.Owner.Font;
			}
			return null;
		}

		/// <summary>Retrieves the <see cref="T:System.Windows.Forms.ToolStrip" /> that is the container of the current <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStrip" /> that is the container of the current <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		// Token: 0x06004269 RID: 17001 RVA: 0x0011A943 File Offset: 0x00118B43
		public ToolStrip GetCurrentParent()
		{
			return this.Parent;
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x0011A94B File Offset: 0x00118B4B
		internal ToolStripDropDown GetCurrentParentDropDown()
		{
			if (this.ParentInternal != null)
			{
				return this.ParentInternal as ToolStripDropDown;
			}
			return this.Owner as ToolStripDropDown;
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fit.</summary>
		/// <param name="constrainingSize">The custom-sized area for a control.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> ordered pair, representing the width and height of a rectangle.</returns>
		// Token: 0x0600426B RID: 17003 RVA: 0x0011A96C File Offset: 0x00118B6C
		public virtual Size GetPreferredSize(Size constrainingSize)
		{
			constrainingSize = LayoutUtils.ConvertZeroToUnbounded(constrainingSize);
			return this.InternalLayout.GetPreferredSize(constrainingSize - this.Padding.Size) + this.Padding.Size;
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x0011A9B4 File Offset: 0x00118BB4
		internal Size GetTextSize()
		{
			if (string.IsNullOrEmpty(this.Text))
			{
				return Size.Empty;
			}
			if (this.cachedTextSize == Size.Empty)
			{
				this.cachedTextSize = TextRenderer.MeasureText(this.Text, this.Font);
			}
			return this.cachedTextSize;
		}

		/// <summary>Invalidates the entire surface of the <see cref="T:System.Windows.Forms.ToolStripItem" /> and causes it to be redrawn.</summary>
		// Token: 0x0600426D RID: 17005 RVA: 0x0011AA03 File Offset: 0x00118C03
		public void Invalidate()
		{
			if (this.ParentInternal != null)
			{
				this.ParentInternal.Invalidate(this.Bounds, true);
			}
		}

		/// <summary>Invalidates the specified region of the <see cref="T:System.Windows.Forms.ToolStripItem" /> by adding it to the update region of the <see cref="T:System.Windows.Forms.ToolStripItem" />, which is the area that will be repainted at the next paint operation, and causes a paint message to be sent to the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="r">A <see cref="T:System.Drawing.Rectangle" /> that represents the region to invalidate.</param>
		// Token: 0x0600426E RID: 17006 RVA: 0x0011AA20 File Offset: 0x00118C20
		public void Invalidate(Rectangle r)
		{
			Point point = this.TranslatePoint(r.Location, ToolStripPointType.ToolStripItemCoords, ToolStripPointType.ToolStripCoords);
			if (this.ParentInternal != null)
			{
				this.ParentInternal.Invalidate(new Rectangle(point, r.Size), true);
			}
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x0011AA5E File Offset: 0x00118C5E
		internal void InvalidateItemLayout(string affectedProperty, bool invalidatePainting)
		{
			this.toolStripItemInternalLayout = null;
			if (this.Owner != null)
			{
				LayoutTransaction.DoLayout(this.Owner, this, affectedProperty);
			}
			if (invalidatePainting && this.Owner != null)
			{
				this.Owner.Invalidate();
			}
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x0011AA92 File Offset: 0x00118C92
		internal void InvalidateItemLayout(string affectedProperty)
		{
			this.InvalidateItemLayout(affectedProperty, true);
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x0011AA9C File Offset: 0x00118C9C
		internal void InvalidateImageListImage()
		{
			if (this.ImageIndexer.ActualIndex >= 0)
			{
				this.Properties.SetObject(ToolStripItem.PropImage, null);
				this.InvalidateItemLayout(PropertyNames.Image);
			}
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x0011AAC8 File Offset: 0x00118CC8
		internal void InvokePaint()
		{
			if (this.ParentInternal != null)
			{
				this.ParentInternal.InvokePaintItem(this);
			}
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key is a regular input key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004273 RID: 17011 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected internal virtual bool IsInputKey(Keys keyData)
		{
			return false;
		}

		/// <summary>Determines whether a character is an input character that the item recognizes.</summary>
		/// <param name="charCode">The character to test.</param>
		/// <returns>
		///   <see langword="true" /> if the character should be sent directly to the item and not preprocessed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004274 RID: 17012 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected internal virtual bool IsInputChar(char charCode)
		{
			return false;
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x0011AAE0 File Offset: 0x00118CE0
		private void HandleClick(EventArgs e)
		{
			try
			{
				if (!base.DesignMode)
				{
					this.state[ToolStripItem.statePressed] = true;
				}
				this.InvokePaint();
				if (this.SupportsItemClick && this.Owner != null)
				{
					this.Owner.HandleItemClick(this);
				}
				this.OnClick(e);
				if (this.SupportsItemClick && this.Owner != null)
				{
					this.Owner.HandleItemClicked(this);
				}
			}
			finally
			{
				this.state[ToolStripItem.statePressed] = false;
			}
			this.Invalidate();
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x0011AB78 File Offset: 0x00118D78
		private void HandleDoubleClick(EventArgs e)
		{
			this.OnDoubleClick(e);
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x0011AB81 File Offset: 0x00118D81
		private void HandlePaint(PaintEventArgs e)
		{
			this.Animate();
			ImageAnimator.UpdateFrames(this.Image);
			this.OnPaint(e);
			this.RaisePaintEvent(ToolStripItem.EventPaint, e);
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x0011ABA8 File Offset: 0x00118DA8
		private void HandleMouseEnter(EventArgs e)
		{
			if (!base.DesignMode && this.ParentInternal != null && this.ParentInternal.CanHotTrack && this.ParentInternal.ShouldSelectItem())
			{
				if (this.Enabled)
				{
					bool menuAutoExpand = this.ParentInternal.MenuAutoExpand;
					if (this.ParentInternal.LastMouseDownedItem == this && UnsafeNativeMethods.GetKeyState(1) < 0)
					{
						this.Push(true);
					}
					this.Select();
					this.ParentInternal.MenuAutoExpand = menuAutoExpand;
				}
				else if (this.SupportsDisabledHotTracking)
				{
					this.Select();
				}
			}
			if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
			{
				KeyboardToolTipStateMachine.Instance.NotifyAboutMouseEnter(this);
			}
			if (this.Enabled)
			{
				this.OnMouseEnter(e);
				this.RaiseEvent(ToolStripItem.EventMouseEnter, e);
			}
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x0011AC60 File Offset: 0x00118E60
		private void HandleMouseMove(MouseEventArgs mea)
		{
			if (this.Enabled && this.CanSelect && !this.Selected && this.ParentInternal != null && this.ParentInternal.CanHotTrack && this.ParentInternal.ShouldSelectItem())
			{
				this.Select();
			}
			this.OnMouseMove(mea);
			this.RaiseMouseEvent(ToolStripItem.EventMouseMove, mea);
		}

		// Token: 0x0600427A RID: 17018 RVA: 0x0011ACC0 File Offset: 0x00118EC0
		private void HandleMouseHover(EventArgs e)
		{
			this.OnMouseHover(e);
			this.RaiseEvent(ToolStripItem.EventMouseHover, e);
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x0011ACD8 File Offset: 0x00118ED8
		private void HandleLeave()
		{
			if (this.state[ToolStripItem.stateMouseDownAndNoDrag] || this.state[ToolStripItem.statePressed] || this.state[ToolStripItem.stateSelected])
			{
				this.state[ToolStripItem.stateMouseDownAndNoDrag | ToolStripItem.statePressed | ToolStripItem.stateSelected] = false;
				if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
				{
					KeyboardToolTipStateMachine.Instance.NotifyAboutLostFocus(this);
				}
				this.Invalidate();
			}
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x0011AD50 File Offset: 0x00118F50
		private void HandleMouseLeave(EventArgs e)
		{
			this.HandleLeave();
			if (this.Enabled)
			{
				this.OnMouseLeave(e);
				this.RaiseEvent(ToolStripItem.EventMouseLeave, e);
			}
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x0011AD74 File Offset: 0x00118F74
		private void HandleMouseDown(MouseEventArgs e)
		{
			this.state[ToolStripItem.stateMouseDownAndNoDrag] = !this.BeginDragForItemReorder();
			if (this.state[ToolStripItem.stateMouseDownAndNoDrag])
			{
				if (e.Button == MouseButtons.Left)
				{
					this.Push(true);
				}
				this.OnMouseDown(e);
				this.RaiseMouseEvent(ToolStripItem.EventMouseDown, e);
			}
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x0011ADD4 File Offset: 0x00118FD4
		private void HandleMouseUp(MouseEventArgs e)
		{
			bool flag = this.ParentInternal.LastMouseDownedItem == this;
			if (!flag && !this.MouseDownAndUpMustBeInSameItem)
			{
				flag = this.ParentInternal.ShouldSelectItem();
			}
			if (this.state[ToolStripItem.stateMouseDownAndNoDrag] || flag)
			{
				this.Push(false);
				if (e.Button == MouseButtons.Left || (e.Button == MouseButtons.Right && this.state[ToolStripItem.stateSupportsRightClick]))
				{
					bool flag2 = false;
					if (this.DoubleClickEnabled)
					{
						long ticks = DateTime.Now.Ticks;
						long num = ticks - this.lastClickTime;
						this.lastClickTime = ticks;
						if (num >= 0L && num < ToolStripItem.DoubleClickTicks)
						{
							flag2 = true;
						}
					}
					if (flag2)
					{
						this.HandleDoubleClick(new EventArgs());
						this.lastClickTime = 0L;
					}
					else
					{
						this.HandleClick(new EventArgs());
					}
				}
				this.OnMouseUp(e);
				this.RaiseMouseEvent(ToolStripItem.EventMouseUp, e);
			}
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnAccessibleDescriptionChanged(EventArgs e)
		{
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnAccessibleNameChanged(EventArgs e)
		{
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnAccessibleDefaultActionDescriptionChanged(EventArgs e)
		{
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnAccessibleRoleChanged(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004283 RID: 17027 RVA: 0x0011AEBF File Offset: 0x001190BF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnBackColorChanged(EventArgs e)
		{
			this.Invalidate();
			this.RaiseEvent(ToolStripItem.EventBackColorChanged, e);
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.Bounds" /> property changes.</summary>
		// Token: 0x06004284 RID: 17028 RVA: 0x0011AED3 File Offset: 0x001190D3
		protected virtual void OnBoundsChanged()
		{
			LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
			this.InternalLayout.PerformLayout();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004285 RID: 17029 RVA: 0x0011AEF1 File Offset: 0x001190F1
		protected virtual void OnClick(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventClick, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06004286 RID: 17030 RVA: 0x000070A6 File Offset: 0x000052A6
		protected internal virtual void OnLayout(LayoutEventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragEnter" /> event.</summary>
		/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x06004287 RID: 17031 RVA: 0x0011AEFF File Offset: 0x001190FF
		void IDropTarget.OnDragEnter(DragEventArgs dragEvent)
		{
			this.OnDragEnter(dragEvent);
		}

		/// <summary>Raises the <see langword="DragOver" /> event.</summary>
		/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x06004288 RID: 17032 RVA: 0x0011AF08 File Offset: 0x00119108
		void IDropTarget.OnDragOver(DragEventArgs dragEvent)
		{
			this.OnDragOver(dragEvent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004289 RID: 17033 RVA: 0x0011AF11 File Offset: 0x00119111
		void IDropTarget.OnDragLeave(EventArgs e)
		{
			this.OnDragLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragDrop" /> event.</summary>
		/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x0600428A RID: 17034 RVA: 0x0011AF1A File Offset: 0x0011911A
		void IDropTarget.OnDragDrop(DragEventArgs dragEvent)
		{
			this.OnDragDrop(dragEvent);
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x0011AF23 File Offset: 0x00119123
		void ISupportOleDropSource.OnGiveFeedback(GiveFeedbackEventArgs giveFeedbackEventArgs)
		{
			this.OnGiveFeedback(giveFeedbackEventArgs);
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x0011AF2C File Offset: 0x0011912C
		void ISupportOleDropSource.OnQueryContinueDrag(QueryContinueDragEventArgs queryContinueDragEventArgs)
		{
			this.OnQueryContinueDrag(queryContinueDragEventArgs);
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x0011AF38 File Offset: 0x00119138
		private void OnAnimationFrameChanged(object o, EventArgs e)
		{
			ToolStrip parentInternal = this.ParentInternal;
			if (parentInternal != null)
			{
				if (parentInternal.Disposing || parentInternal.IsDisposed)
				{
					return;
				}
				if (parentInternal.IsHandleCreated && parentInternal.InvokeRequired)
				{
					parentInternal.BeginInvoke(new EventHandler(this.OnAnimationFrameChanged), new object[] { o, e });
					return;
				}
				this.Invalidate();
			}
		}

		/// <summary>Raises the AvailableChanged event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600428E RID: 17038 RVA: 0x0011AF98 File Offset: 0x00119198
		protected virtual void OnAvailableChanged(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventAvailableChanged, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragEnter" /> event.</summary>
		/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x0600428F RID: 17039 RVA: 0x0011AFA6 File Offset: 0x001191A6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragEnter(DragEventArgs dragEvent)
		{
			this.RaiseDragEvent(ToolStripItem.EventDragEnter, dragEvent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragOver" /> event.</summary>
		/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x06004290 RID: 17040 RVA: 0x0011AFB4 File Offset: 0x001191B4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragOver(DragEventArgs dragEvent)
		{
			this.RaiseDragEvent(ToolStripItem.EventDragOver, dragEvent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004291 RID: 17041 RVA: 0x0011AFC2 File Offset: 0x001191C2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragLeave(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventDragLeave, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DragDrop" /> event.</summary>
		/// <param name="dragEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x06004292 RID: 17042 RVA: 0x0011AFD0 File Offset: 0x001191D0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragDrop(DragEventArgs dragEvent)
		{
			this.RaiseDragEvent(ToolStripItem.EventDragDrop, dragEvent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DisplayStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004293 RID: 17043 RVA: 0x0011AFDE File Offset: 0x001191DE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDisplayStyleChanged(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventDisplayStyleChanged, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.GiveFeedback" /> event.</summary>
		/// <param name="giveFeedbackEvent">A <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> that contains the event data.</param>
		// Token: 0x06004294 RID: 17044 RVA: 0x0011AFEC File Offset: 0x001191EC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnGiveFeedback(GiveFeedbackEventArgs giveFeedbackEvent)
		{
			GiveFeedbackEventHandler giveFeedbackEventHandler = (GiveFeedbackEventHandler)base.Events[ToolStripItem.EventGiveFeedback];
			if (giveFeedbackEventHandler != null)
			{
				giveFeedbackEventHandler(this, giveFeedbackEvent);
			}
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnImageScalingSizeChanged(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.QueryContinueDrag" /> event.</summary>
		/// <param name="queryContinueDragEvent">A <see cref="T:System.Windows.Forms.QueryContinueDragEventArgs" /> that contains the event data.</param>
		// Token: 0x06004296 RID: 17046 RVA: 0x0011B01A File Offset: 0x0011921A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs queryContinueDragEvent)
		{
			this.RaiseQueryContinueDragEvent(ToolStripItem.EventQueryContinueDrag, queryContinueDragEvent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.DoubleClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004297 RID: 17047 RVA: 0x0011B028 File Offset: 0x00119228
		protected virtual void OnDoubleClick(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventDoubleClick, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004298 RID: 17048 RVA: 0x0011B036 File Offset: 0x00119236
		protected virtual void OnEnabledChanged(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventEnabledChanged, e);
			this.Animate();
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x0011B04A File Offset: 0x0011924A
		internal void OnInternalEnabledChanged(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventInternalEnabledChanged, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600429A RID: 17050 RVA: 0x0011B058 File Offset: 0x00119258
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnForeColorChanged(EventArgs e)
		{
			this.Invalidate();
			this.RaiseEvent(ToolStripItem.EventForeColorChanged, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600429B RID: 17051 RVA: 0x0011B06C File Offset: 0x0011926C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFontChanged(EventArgs e)
		{
			this.cachedTextSize = Size.Empty;
			if ((this.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
			{
				this.InvalidateItemLayout(PropertyNames.Font);
			}
			else
			{
				this.toolStripItemInternalLayout = null;
			}
			this.RaiseEvent(ToolStripItem.EventFontChanged, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.LocationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600429C RID: 17052 RVA: 0x0011B0A4 File Offset: 0x001192A4
		protected virtual void OnLocationChanged(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventLocationChanged, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600429D RID: 17053 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnMouseEnter(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseMove" /> event.</summary>
		/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600429E RID: 17054 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnMouseMove(MouseEventArgs mea)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseHover" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600429F RID: 17055 RVA: 0x0011B0B2 File Offset: 0x001192B2
		protected virtual void OnMouseHover(EventArgs e)
		{
			if (this.ParentInternal != null && !string.IsNullOrEmpty(this.ToolTipText))
			{
				this.ParentInternal.UpdateToolTip(this);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060042A0 RID: 17056 RVA: 0x0011B0D5 File Offset: 0x001192D5
		protected virtual void OnMouseLeave(EventArgs e)
		{
			if (this.ParentInternal != null)
			{
				this.ParentInternal.UpdateToolTip(null);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060042A1 RID: 17057 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnMouseDown(MouseEventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060042A2 RID: 17058 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnMouseUp(MouseEventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x060042A3 RID: 17059 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnPaint(PaintEventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060042A4 RID: 17060 RVA: 0x0011B0EC File Offset: 0x001192EC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentBackColorChanged(EventArgs e)
		{
			if (this.Properties.GetColor(ToolStripItem.PropBackColor).IsEmpty)
			{
				this.OnBackColorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="oldParent">The original parent of the item.</param>
		/// <param name="newParent">The new parent of the item.</param>
		// Token: 0x060042A5 RID: 17061 RVA: 0x0011B11A File Offset: 0x0011931A
		protected virtual void OnParentChanged(ToolStrip oldParent, ToolStrip newParent)
		{
			this.SetAmbientMargin();
			if (oldParent != null && oldParent.DropTargetManager != null)
			{
				oldParent.DropTargetManager.EnsureUnRegistered(this);
			}
			if (this.AllowDrop && newParent != null)
			{
				this.EnsureParentDropTargetRegistered();
			}
			this.Animate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.EnabledChanged" /> event when the <see cref="P:System.Windows.Forms.ToolStripItem.Enabled" /> property value of the item's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060042A6 RID: 17062 RVA: 0x0011B150 File Offset: 0x00119350
		protected internal virtual void OnParentEnabledChanged(EventArgs e)
		{
			this.OnEnabledChanged(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event when the <see cref="P:System.Windows.Forms.ToolStripItem.Font" /> property has changed on the parent of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060042A7 RID: 17063 RVA: 0x0011B15D File Offset: 0x0011935D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void OnOwnerFontChanged(EventArgs e)
		{
			if (this.Properties.GetObject(ToolStripItem.PropFont) == null)
			{
				this.OnFontChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060042A8 RID: 17064 RVA: 0x0011B178 File Offset: 0x00119378
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentForeColorChanged(EventArgs e)
		{
			if (this.Properties.GetColor(ToolStripItem.PropForeColor).IsEmpty)
			{
				this.OnForeColorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060042A9 RID: 17065 RVA: 0x0011B1A6 File Offset: 0x001193A6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void OnParentRightToLeftChanged(EventArgs e)
		{
			if (!this.Properties.ContainsInteger(ToolStripItem.PropRightToLeft) || this.Properties.GetInteger(ToolStripItem.PropRightToLeft) == 2)
			{
				this.OnRightToLeftChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.OwnerChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060042AA RID: 17066 RVA: 0x0011B1D4 File Offset: 0x001193D4
		protected virtual void OnOwnerChanged(EventArgs e)
		{
			this.RaiseEvent(ToolStripItem.EventOwnerChanged, e);
			this.SetAmbientMargin();
			if (this.Owner != null)
			{
				bool flag = false;
				int num = this.Properties.GetInteger(ToolStripItem.PropRightToLeft, out flag);
				if (!flag)
				{
					num = 2;
				}
				if (num == 2 && this.RightToLeft != this.DefaultRightToLeft)
				{
					this.OnRightToLeftChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x0011B234 File Offset: 0x00119434
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal void OnOwnerTextDirectionChanged()
		{
			ToolStripTextDirection toolStripTextDirection = ToolStripTextDirection.Inherit;
			if (this.Properties.ContainsObject(ToolStripItem.PropTextDirection))
			{
				toolStripTextDirection = (ToolStripTextDirection)this.Properties.GetObject(ToolStripItem.PropTextDirection);
			}
			if (toolStripTextDirection == ToolStripTextDirection.Inherit)
			{
				this.InvalidateItemLayout("TextDirection");
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060042AC RID: 17068 RVA: 0x0011B279 File Offset: 0x00119479
		protected virtual void OnRightToLeftChanged(EventArgs e)
		{
			this.InvalidateItemLayout(PropertyNames.RightToLeft);
			this.RaiseEvent(ToolStripItem.EventRightToLeft, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060042AD RID: 17069 RVA: 0x0011B292 File Offset: 0x00119492
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnTextChanged(EventArgs e)
		{
			this.cachedTextSize = Size.Empty;
			this.InvalidateItemLayout(PropertyNames.Text);
			this.RaiseEvent(ToolStripItem.EventText, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060042AE RID: 17070 RVA: 0x0011B2B8 File Offset: 0x001194B8
		protected virtual void OnVisibleChanged(EventArgs e)
		{
			if (this.Owner != null && !this.Owner.IsDisposed && !this.Owner.Disposing)
			{
				this.Owner.OnItemVisibleChanged(new ToolStripItemEventArgs(this), true);
			}
			this.RaiseEvent(ToolStripItem.EventVisibleChanged, e);
			this.Animate();
		}

		/// <summary>Generates a <see langword="Click" /> event for a <see langword="ToolStripItem" />.</summary>
		// Token: 0x060042AF RID: 17071 RVA: 0x0011B30B File Offset: 0x0011950B
		public void PerformClick()
		{
			if (this.Enabled && this.Available)
			{
				this.FireEvent(ToolStripItemEventType.Click);
			}
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x0011B324 File Offset: 0x00119524
		internal void Push(bool push)
		{
			if (!this.CanSelect || !this.Enabled || base.DesignMode)
			{
				return;
			}
			if (this.state[ToolStripItem.statePressed] != push)
			{
				this.state[ToolStripItem.statePressed] = push;
				if (this.Available)
				{
					this.Invalidate();
				}
			}
		}

		/// <summary>Processes a dialog key.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the key was processed by the item; otherwise, <see langword="false" />.</returns>
		// Token: 0x060042B1 RID: 17073 RVA: 0x0011B37C File Offset: 0x0011957C
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal virtual bool ProcessDialogKey(Keys keyData)
		{
			if (keyData == Keys.Return || (this.state[ToolStripItem.stateSupportsSpaceKey] && keyData == Keys.Space))
			{
				this.FireEvent(ToolStripItemEventType.Click);
				if (this.ParentInternal != null && !this.ParentInternal.IsDropDown && (!AccessibilityImprovements.Level2 || this.Enabled))
				{
					this.ParentInternal.RestoreFocusInternal();
				}
				return true;
			}
			return false;
		}

		/// <summary>Processes a command key.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x060042B2 RID: 17074 RVA: 0x0001180C File Offset: 0x0000FA0C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected internal virtual bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			return false;
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x060042B3 RID: 17075 RVA: 0x0011B3DE File Offset: 0x001195DE
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal virtual bool ProcessMnemonic(char charCode)
		{
			this.FireEvent(ToolStripItemEventType.Click);
			return true;
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x0011B3E8 File Offset: 0x001195E8
		internal void RaiseCancelEvent(object key, CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[key];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x0011B414 File Offset: 0x00119614
		internal void RaiseDragEvent(object key, DragEventArgs e)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[key];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, e);
			}
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x0011B440 File Offset: 0x00119640
		internal void RaiseEvent(object key, EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[key];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x0011B46C File Offset: 0x0011966C
		internal void RaiseKeyEvent(object key, KeyEventArgs e)
		{
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[key];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x0011B498 File Offset: 0x00119698
		internal void RaiseKeyPressEvent(object key, KeyPressEventArgs e)
		{
			KeyPressEventHandler keyPressEventHandler = (KeyPressEventHandler)base.Events[key];
			if (keyPressEventHandler != null)
			{
				keyPressEventHandler(this, e);
			}
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x0011B4C4 File Offset: 0x001196C4
		internal void RaiseMouseEvent(object key, MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[key];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x0011B4F0 File Offset: 0x001196F0
		internal void RaisePaintEvent(object key, PaintEventArgs e)
		{
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[key];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, e);
			}
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x0011B51C File Offset: 0x0011971C
		internal void RaiseQueryContinueDragEvent(object key, QueryContinueDragEventArgs e)
		{
			QueryContinueDragEventHandler queryContinueDragEventHandler = (QueryContinueDragEventHandler)base.Events[key];
			if (queryContinueDragEventHandler != null)
			{
				queryContinueDragEventHandler(this, e);
			}
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x0011B546 File Offset: 0x00119746
		private void ResetToolTipText()
		{
			this.toolTipText = null;
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x0011B54F File Offset: 0x0011974F
		internal virtual void ToolStrip_RescaleConstants(int oldDpi, int newDpi)
		{
			this.DeviceDpi = newDpi;
			this.RescaleConstantsInternal(newDpi);
			this.OnFontChanged(EventArgs.Empty);
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x0011B56A File Offset: 0x0011976A
		internal void RescaleConstantsInternal(int newDpi)
		{
			ToolStripManager.CurrentDpi = newDpi;
			this.defaultFont = ToolStripManager.DefaultFont;
			this.scaledDefaultMargin = DpiHelper.LogicalToDeviceUnits(ToolStripItem.defaultMargin, this.deviceDpi);
			this.scaledDefaultStatusStripMargin = DpiHelper.LogicalToDeviceUnits(ToolStripItem.defaultStatusStripMargin, this.deviceDpi);
		}

		/// <summary>Selects the item.</summary>
		// Token: 0x060042BF RID: 17087 RVA: 0x0011B5AC File Offset: 0x001197AC
		public void Select()
		{
			if (!this.CanSelect)
			{
				return;
			}
			if (this.Owner != null && this.Owner.IsCurrentlyDragging)
			{
				return;
			}
			if (this.ParentInternal != null && this.ParentInternal.IsSelectionSuspended)
			{
				return;
			}
			if (!this.Selected)
			{
				this.state[ToolStripItem.stateSelected] = true;
				if (this.ParentInternal != null)
				{
					this.ParentInternal.NotifySelectionChange(this);
				}
				if (this.IsOnDropDown && this.OwnerItem != null && this.OwnerItem.IsOnDropDown)
				{
					this.OwnerItem.Select();
				}
				if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
				{
					KeyboardToolTipStateMachine.Instance.NotifyAboutGotFocus(this);
				}
				if (AccessibilityImprovements.Level3 && this.AccessibilityObject is ToolStripItem.ToolStripItemAccessibleObject)
				{
					((ToolStripItem.ToolStripItemAccessibleObject)this.AccessibilityObject).RaiseFocusChanged();
				}
			}
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x0011B67C File Offset: 0x0011987C
		internal void SetOwner(ToolStrip newOwner)
		{
			if (this.owner != newOwner)
			{
				Font font = this.Font;
				if (this.owner != null)
				{
					ToolStrip toolStrip = this.owner;
					toolStrip.rescaleConstsCallbackDelegate = (Action<int, int>)Delegate.Remove(toolStrip.rescaleConstsCallbackDelegate, new Action<int, int>(this.ToolStrip_RescaleConstants));
				}
				this.owner = newOwner;
				if (this.owner != null)
				{
					ToolStrip toolStrip2 = this.owner;
					toolStrip2.rescaleConstsCallbackDelegate = (Action<int, int>)Delegate.Combine(toolStrip2.rescaleConstsCallbackDelegate, new Action<int, int>(this.ToolStrip_RescaleConstants));
				}
				if (newOwner == null)
				{
					this.ParentInternal = null;
				}
				if (!this.state[ToolStripItem.stateDisposing] && !this.IsDisposed)
				{
					this.OnOwnerChanged(EventArgs.Empty);
					if (font != this.Font)
					{
						this.OnFontChanged(EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> to the specified visible state.</summary>
		/// <param name="visible">
		///   <see langword="true" /> to make the <see cref="T:System.Windows.Forms.ToolStripItem" /> visible; otherwise, <see langword="false" />.</param>
		// Token: 0x060042C1 RID: 17089 RVA: 0x0011B748 File Offset: 0x00119948
		protected virtual void SetVisibleCore(bool visible)
		{
			if (this.state[ToolStripItem.stateVisible] != visible)
			{
				this.state[ToolStripItem.stateVisible] = visible;
				this.Unselect();
				this.Push(false);
				this.OnAvailableChanged(EventArgs.Empty);
				this.OnVisibleChanged(EventArgs.Empty);
			}
		}

		/// <summary>Sets the size and location of the item.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the <see cref="T:System.Windows.Forms.ToolStripItem" /></param>
		// Token: 0x060042C2 RID: 17090 RVA: 0x0011B79C File Offset: 0x0011999C
		protected internal virtual void SetBounds(Rectangle bounds)
		{
			Rectangle rectangle = this.bounds;
			this.bounds = bounds;
			if (!this.state[ToolStripItem.stateContstructing])
			{
				if (this.bounds != rectangle)
				{
					this.OnBoundsChanged();
				}
				if (this.bounds.Location != rectangle.Location)
				{
					this.OnLocationChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x0011B801 File Offset: 0x00119A01
		internal void SetBounds(int x, int y, int width, int height)
		{
			this.SetBounds(new Rectangle(x, y, width, height));
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x0011B813 File Offset: 0x00119A13
		internal void SetPlacement(ToolStripItemPlacement placement)
		{
			this.placement = placement;
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x0011B81C File Offset: 0x00119A1C
		internal void SetAmbientMargin()
		{
			if (this.state[ToolStripItem.stateUseAmbientMargin] && this.Margin != this.DefaultMargin)
			{
				CommonProperties.SetMargin(this, this.DefaultMargin);
			}
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x0011B84F File Offset: 0x00119A4F
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeImageTransparentColor()
		{
			return this.ImageTransparentColor != Color.Empty;
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x0011B864 File Offset: 0x00119A64
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeBackColor()
		{
			return !this.Properties.GetColor(ToolStripItem.PropBackColor).IsEmpty;
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x0011B88C File Offset: 0x00119A8C
		private bool ShouldSerializeDisplayStyle()
		{
			return this.DisplayStyle != this.DefaultDisplayStyle;
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x0011B89F File Offset: 0x00119A9F
		private bool ShouldSerializeToolTipText()
		{
			return !string.IsNullOrEmpty(this.toolTipText);
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x0011B8B0 File Offset: 0x00119AB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeForeColor()
		{
			return !this.Properties.GetColor(ToolStripItem.PropForeColor).IsEmpty;
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x0011B8D8 File Offset: 0x00119AD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeFont()
		{
			bool flag;
			object @object = this.Properties.GetObject(ToolStripItem.PropFont, out flag);
			return flag && @object != null;
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x0011B901 File Offset: 0x00119B01
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializePadding()
		{
			return this.Padding != this.DefaultPadding;
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x0011B914 File Offset: 0x00119B14
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeMargin()
		{
			return this.Margin != this.DefaultMargin;
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x0011B927 File Offset: 0x00119B27
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeVisible()
		{
			return !this.state[ToolStripItem.stateVisible];
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x0011B93C File Offset: 0x00119B3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeImage()
		{
			return this.Image != null && this.ImageIndexer.ActualIndex < 0;
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x0011B956 File Offset: 0x00119B56
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeImageKey()
		{
			return this.Image != null && this.ImageIndexer.ActualIndex >= 0 && this.ImageIndexer.Key != null && this.ImageIndexer.Key.Length != 0;
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x0011B992 File Offset: 0x00119B92
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeImageIndex()
		{
			return this.Image != null && this.ImageIndexer.ActualIndex >= 0 && this.ImageIndexer.Index != -1;
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x0011B9C0 File Offset: 0x00119BC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeRightToLeft()
		{
			bool flag = false;
			int integer = this.Properties.GetInteger(ToolStripItem.PropRightToLeft, out flag);
			return flag && integer != (int)this.DefaultRightToLeft;
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x0011B9F4 File Offset: 0x00119BF4
		private bool ShouldSerializeTextDirection()
		{
			ToolStripTextDirection toolStripTextDirection = ToolStripTextDirection.Inherit;
			if (this.Properties.ContainsObject(ToolStripItem.PropTextDirection))
			{
				toolStripTextDirection = (ToolStripTextDirection)this.Properties.GetObject(ToolStripItem.PropTextDirection);
			}
			return toolStripTextDirection > ToolStripTextDirection.Inherit;
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x060042D4 RID: 17108 RVA: 0x0011BA2F File Offset: 0x00119C2F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetBackColor()
		{
			this.BackColor = Color.Empty;
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x060042D5 RID: 17109 RVA: 0x0011BA3C File Offset: 0x00119C3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetDisplayStyle()
		{
			this.DisplayStyle = this.DefaultDisplayStyle;
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x060042D6 RID: 17110 RVA: 0x0011BA4A File Offset: 0x00119C4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetForeColor()
		{
			this.ForeColor = Color.Empty;
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x060042D7 RID: 17111 RVA: 0x0011BA57 File Offset: 0x00119C57
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetFont()
		{
			this.Font = null;
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x060042D8 RID: 17112 RVA: 0x0011BA60 File Offset: 0x00119C60
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetImage()
		{
			this.Image = null;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x0011BA69 File Offset: 0x00119C69
		[EditorBrowsable(EditorBrowsableState.Never)]
		private void ResetImageTransparentColor()
		{
			this.ImageTransparentColor = Color.Empty;
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x060042DA RID: 17114 RVA: 0x0011BA76 File Offset: 0x00119C76
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetMargin()
		{
			this.state[ToolStripItem.stateUseAmbientMargin] = true;
			this.SetAmbientMargin();
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x060042DB RID: 17115 RVA: 0x00037D03 File Offset: 0x00035F03
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetPadding()
		{
			CommonProperties.ResetPadding(this);
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x060042DC RID: 17116 RVA: 0x0011BA8F File Offset: 0x00119C8F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetRightToLeft()
		{
			this.RightToLeft = RightToLeft.Inherit;
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x060042DD RID: 17117 RVA: 0x0011BA98 File Offset: 0x00119C98
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetTextDirection()
		{
			this.TextDirection = ToolStripTextDirection.Inherit;
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x0011BAA4 File Offset: 0x00119CA4
		internal Point TranslatePoint(Point fromPoint, ToolStripPointType fromPointType, ToolStripPointType toPointType)
		{
			ToolStrip toolStrip = this.ParentInternal;
			if (toolStrip == null)
			{
				toolStrip = ((this.IsOnOverflow && this.Owner != null) ? this.Owner.OverflowButton.DropDown : this.Owner);
			}
			if (toolStrip == null)
			{
				return fromPoint;
			}
			if (fromPointType == toPointType)
			{
				return fromPoint;
			}
			Point point = Point.Empty;
			Point location = this.Bounds.Location;
			if (fromPointType == ToolStripPointType.ScreenCoords)
			{
				point = toolStrip.PointToClient(fromPoint);
				if (toPointType == ToolStripPointType.ToolStripItemCoords)
				{
					point.X += location.X;
					point.Y += location.Y;
				}
			}
			else
			{
				if (fromPointType == ToolStripPointType.ToolStripItemCoords)
				{
					fromPoint.X += location.X;
					fromPoint.Y += location.Y;
				}
				if (toPointType == ToolStripPointType.ScreenCoords)
				{
					point = toolStrip.PointToScreen(fromPoint);
				}
				else if (toPointType == ToolStripPointType.ToolStripItemCoords)
				{
					fromPoint.X -= location.X;
					fromPoint.Y -= location.Y;
					point = fromPoint;
				}
				else
				{
					point = fromPoint;
				}
			}
			return point;
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x0011BBB4 File Offset: 0x00119DB4
		internal ToolStrip RootToolStrip
		{
			get
			{
				ToolStripItem toolStripItem = this;
				while (toolStripItem.OwnerItem != null)
				{
					toolStripItem = toolStripItem.OwnerItem;
				}
				return toolStripItem.ParentInternal;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x060042E0 RID: 17120 RVA: 0x0011BBDA File Offset: 0x00119DDA
		public override string ToString()
		{
			if (this.Text != null && this.Text.Length != 0)
			{
				return this.Text;
			}
			return base.ToString();
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x0011BC00 File Offset: 0x00119E00
		internal void Unselect()
		{
			if (this.state[ToolStripItem.stateSelected])
			{
				this.state[ToolStripItem.stateSelected] = false;
				if (this.Available)
				{
					this.Invalidate();
					if (this.ParentInternal != null)
					{
						this.ParentInternal.NotifySelectionChange(this);
					}
					if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
					{
						KeyboardToolTipStateMachine.Instance.NotifyAboutLostFocus(this);
					}
				}
			}
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x0011BC64 File Offset: 0x00119E64
		bool IKeyboardToolTip.CanShowToolTipsNow()
		{
			return this.Visible && this.parent != null && ((IKeyboardToolTip)this.parent).AllowsChildrenToShowToolTips();
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x0011BC83 File Offset: 0x00119E83
		Rectangle IKeyboardToolTip.GetNativeScreenRectangle()
		{
			return this.AccessibilityObject.Bounds;
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x0011BC90 File Offset: 0x00119E90
		IList<Rectangle> IKeyboardToolTip.GetNeighboringToolsRectangles()
		{
			List<Rectangle> list = new List<Rectangle>(3);
			if (this.parent != null)
			{
				ToolStripItemCollection displayedItems = this.parent.DisplayedItems;
				int num = 0;
				int count = displayedItems.Count;
				bool flag = false;
				while (!flag && num < count)
				{
					flag = displayedItems[num] == this;
					if (flag)
					{
						int num2 = num - 1;
						if (num2 >= 0)
						{
							list.Add(((IKeyboardToolTip)displayedItems[num2]).GetNativeScreenRectangle());
						}
						int num3 = num + 1;
						if (num3 < count)
						{
							list.Add(((IKeyboardToolTip)displayedItems[num3]).GetNativeScreenRectangle());
						}
					}
					else
					{
						num++;
					}
				}
			}
			ToolStripDropDown toolStripDropDown = this.parent as ToolStripDropDown;
			if (toolStripDropDown != null && toolStripDropDown.OwnerItem != null)
			{
				list.Add(((IKeyboardToolTip)toolStripDropDown.OwnerItem).GetNativeScreenRectangle());
			}
			return list;
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x0011BD4C File Offset: 0x00119F4C
		bool IKeyboardToolTip.IsHoveredWithMouse()
		{
			return ((IKeyboardToolTip)this).GetNativeScreenRectangle().Contains(Control.MousePosition);
		}

		// Token: 0x060042E6 RID: 17126 RVA: 0x0011BD6C File Offset: 0x00119F6C
		bool IKeyboardToolTip.HasRtlModeEnabled()
		{
			return this.parent != null && ((IKeyboardToolTip)this.parent).HasRtlModeEnabled();
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x00012E4E File Offset: 0x0001104E
		bool IKeyboardToolTip.AllowsToolTip()
		{
			return true;
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x00119D08 File Offset: 0x00117F08
		IWin32Window IKeyboardToolTip.GetOwnerWindow()
		{
			return this.ParentInternal;
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x0011BD83 File Offset: 0x00119F83
		void IKeyboardToolTip.OnHooked(ToolTip toolTip)
		{
			this.OnKeyboardToolTipHook(toolTip);
		}

		// Token: 0x060042EA RID: 17130 RVA: 0x0011BD8C File Offset: 0x00119F8C
		void IKeyboardToolTip.OnUnhooked(ToolTip toolTip)
		{
			this.OnKeyboardToolTipUnhook(toolTip);
		}

		// Token: 0x060042EB RID: 17131 RVA: 0x0011BD95 File Offset: 0x00119F95
		string IKeyboardToolTip.GetCaptionForTool(ToolTip toolTip)
		{
			return this.ToolTipText;
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x00012E4E File Offset: 0x0001104E
		bool IKeyboardToolTip.ShowsOwnToolTip()
		{
			return true;
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x0011BD9D File Offset: 0x00119F9D
		bool IKeyboardToolTip.IsBeingTabbedTo()
		{
			return this.IsBeingTabbedTo();
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x00012E4E File Offset: 0x0001104E
		bool IKeyboardToolTip.AllowsChildrenToShowToolTips()
		{
			return true;
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnKeyboardToolTipHook(ToolTip toolTip)
		{
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnKeyboardToolTipUnhook(ToolTip toolTip)
		{
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x0003C02F File Offset: 0x0003A22F
		internal virtual bool IsBeingTabbedTo()
		{
			return Control.AreCommonNavigationalKeysDown();
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x0011BDA5 File Offset: 0x00119FA5
		internal static bool GetIsOffscreenPropertyValue(ToolStripItemPlacement toolStripItemPlacement, Rectangle bounds)
		{
			return toolStripItemPlacement != ToolStripItemPlacement.Main || bounds.Height <= 0 || bounds.Width <= 0;
		}

		// Token: 0x04002521 RID: 9505
		internal static readonly TraceSwitch MouseDebugging;

		// Token: 0x04002522 RID: 9506
		private Rectangle bounds = Rectangle.Empty;

		// Token: 0x04002523 RID: 9507
		private PropertyStore propertyStore;

		// Token: 0x04002524 RID: 9508
		private ToolStripItemAlignment alignment;

		// Token: 0x04002525 RID: 9509
		private ToolStrip parent;

		// Token: 0x04002526 RID: 9510
		private ToolStrip owner;

		// Token: 0x04002527 RID: 9511
		private ToolStripItemOverflow overflow = ToolStripItemOverflow.AsNeeded;

		// Token: 0x04002528 RID: 9512
		private ToolStripItemPlacement placement = ToolStripItemPlacement.None;

		// Token: 0x04002529 RID: 9513
		private ContentAlignment imageAlign = ContentAlignment.MiddleCenter;

		// Token: 0x0400252A RID: 9514
		private ContentAlignment textAlign = ContentAlignment.MiddleCenter;

		// Token: 0x0400252B RID: 9515
		private TextImageRelation textImageRelation = TextImageRelation.ImageBeforeText;

		// Token: 0x0400252C RID: 9516
		private ToolStripItemImageIndexer imageIndexer;

		// Token: 0x0400252D RID: 9517
		private ToolStripItemInternalLayout toolStripItemInternalLayout;

		// Token: 0x0400252E RID: 9518
		private BitVector32 state;

		// Token: 0x0400252F RID: 9519
		private string toolTipText;

		// Token: 0x04002530 RID: 9520
		private Color imageTransparentColor = Color.Empty;

		// Token: 0x04002531 RID: 9521
		private ToolStripItemImageScaling imageScaling = ToolStripItemImageScaling.SizeToFit;

		// Token: 0x04002532 RID: 9522
		private Size cachedTextSize = Size.Empty;

		// Token: 0x04002533 RID: 9523
		private static readonly Padding defaultMargin = new Padding(0, 1, 0, 2);

		// Token: 0x04002534 RID: 9524
		private static readonly Padding defaultStatusStripMargin = new Padding(0, 2, 0, 0);

		// Token: 0x04002535 RID: 9525
		private Padding scaledDefaultMargin = ToolStripItem.defaultMargin;

		// Token: 0x04002536 RID: 9526
		private Padding scaledDefaultStatusStripMargin = ToolStripItem.defaultStatusStripMargin;

		// Token: 0x04002537 RID: 9527
		private ToolStripItemDisplayStyle displayStyle = ToolStripItemDisplayStyle.ImageAndText;

		// Token: 0x04002538 RID: 9528
		private static readonly ArrangedElementCollection EmptyChildCollection = new ArrangedElementCollection();

		// Token: 0x04002539 RID: 9529
		internal static readonly object EventMouseDown = new object();

		// Token: 0x0400253A RID: 9530
		internal static readonly object EventMouseEnter = new object();

		// Token: 0x0400253B RID: 9531
		internal static readonly object EventMouseLeave = new object();

		// Token: 0x0400253C RID: 9532
		internal static readonly object EventMouseHover = new object();

		// Token: 0x0400253D RID: 9533
		internal static readonly object EventMouseMove = new object();

		// Token: 0x0400253E RID: 9534
		internal static readonly object EventMouseUp = new object();

		// Token: 0x0400253F RID: 9535
		internal static readonly object EventMouseWheel = new object();

		// Token: 0x04002540 RID: 9536
		internal static readonly object EventClick = new object();

		// Token: 0x04002541 RID: 9537
		internal static readonly object EventDoubleClick = new object();

		// Token: 0x04002542 RID: 9538
		internal static readonly object EventDragDrop = new object();

		// Token: 0x04002543 RID: 9539
		internal static readonly object EventDragEnter = new object();

		// Token: 0x04002544 RID: 9540
		internal static readonly object EventDragLeave = new object();

		// Token: 0x04002545 RID: 9541
		internal static readonly object EventDragOver = new object();

		// Token: 0x04002546 RID: 9542
		internal static readonly object EventDisplayStyleChanged = new object();

		// Token: 0x04002547 RID: 9543
		internal static readonly object EventEnabledChanged = new object();

		// Token: 0x04002548 RID: 9544
		internal static readonly object EventInternalEnabledChanged = new object();

		// Token: 0x04002549 RID: 9545
		internal static readonly object EventFontChanged = new object();

		// Token: 0x0400254A RID: 9546
		internal static readonly object EventForeColorChanged = new object();

		// Token: 0x0400254B RID: 9547
		internal static readonly object EventBackColorChanged = new object();

		// Token: 0x0400254C RID: 9548
		internal static readonly object EventGiveFeedback = new object();

		// Token: 0x0400254D RID: 9549
		internal static readonly object EventQueryContinueDrag = new object();

		// Token: 0x0400254E RID: 9550
		internal static readonly object EventQueryAccessibilityHelp = new object();

		// Token: 0x0400254F RID: 9551
		internal static readonly object EventMove = new object();

		// Token: 0x04002550 RID: 9552
		internal static readonly object EventResize = new object();

		// Token: 0x04002551 RID: 9553
		internal static readonly object EventLayout = new object();

		// Token: 0x04002552 RID: 9554
		internal static readonly object EventLocationChanged = new object();

		// Token: 0x04002553 RID: 9555
		internal static readonly object EventRightToLeft = new object();

		// Token: 0x04002554 RID: 9556
		internal static readonly object EventVisibleChanged = new object();

		// Token: 0x04002555 RID: 9557
		internal static readonly object EventAvailableChanged = new object();

		// Token: 0x04002556 RID: 9558
		internal static readonly object EventOwnerChanged = new object();

		// Token: 0x04002557 RID: 9559
		internal static readonly object EventPaint = new object();

		// Token: 0x04002558 RID: 9560
		internal static readonly object EventText = new object();

		// Token: 0x04002559 RID: 9561
		internal static readonly object EventSelectedChanged = new object();

		// Token: 0x0400255A RID: 9562
		private static readonly int PropName = PropertyStore.CreateKey();

		// Token: 0x0400255B RID: 9563
		private static readonly int PropText = PropertyStore.CreateKey();

		// Token: 0x0400255C RID: 9564
		private static readonly int PropBackColor = PropertyStore.CreateKey();

		// Token: 0x0400255D RID: 9565
		private static readonly int PropForeColor = PropertyStore.CreateKey();

		// Token: 0x0400255E RID: 9566
		private static readonly int PropImage = PropertyStore.CreateKey();

		// Token: 0x0400255F RID: 9567
		private static readonly int PropFont = PropertyStore.CreateKey();

		// Token: 0x04002560 RID: 9568
		private static readonly int PropRightToLeft = PropertyStore.CreateKey();

		// Token: 0x04002561 RID: 9569
		private static readonly int PropTag = PropertyStore.CreateKey();

		// Token: 0x04002562 RID: 9570
		private static readonly int PropAccessibility = PropertyStore.CreateKey();

		// Token: 0x04002563 RID: 9571
		private static readonly int PropAccessibleName = PropertyStore.CreateKey();

		// Token: 0x04002564 RID: 9572
		private static readonly int PropAccessibleRole = PropertyStore.CreateKey();

		// Token: 0x04002565 RID: 9573
		private static readonly int PropAccessibleHelpProvider = PropertyStore.CreateKey();

		// Token: 0x04002566 RID: 9574
		private static readonly int PropAccessibleDefaultActionDescription = PropertyStore.CreateKey();

		// Token: 0x04002567 RID: 9575
		private static readonly int PropAccessibleDescription = PropertyStore.CreateKey();

		// Token: 0x04002568 RID: 9576
		private static readonly int PropTextDirection = PropertyStore.CreateKey();

		// Token: 0x04002569 RID: 9577
		private static readonly int PropMirroredImage = PropertyStore.CreateKey();

		// Token: 0x0400256A RID: 9578
		private static readonly int PropBackgroundImage = PropertyStore.CreateKey();

		// Token: 0x0400256B RID: 9579
		private static readonly int PropBackgroundImageLayout = PropertyStore.CreateKey();

		// Token: 0x0400256C RID: 9580
		private static readonly int PropMergeAction = PropertyStore.CreateKey();

		// Token: 0x0400256D RID: 9581
		private static readonly int PropMergeIndex = PropertyStore.CreateKey();

		// Token: 0x0400256E RID: 9582
		private static readonly int stateAllowDrop = BitVector32.CreateMask();

		// Token: 0x0400256F RID: 9583
		private static readonly int stateVisible = BitVector32.CreateMask(ToolStripItem.stateAllowDrop);

		// Token: 0x04002570 RID: 9584
		private static readonly int stateEnabled = BitVector32.CreateMask(ToolStripItem.stateVisible);

		// Token: 0x04002571 RID: 9585
		private static readonly int stateMouseDownAndNoDrag = BitVector32.CreateMask(ToolStripItem.stateEnabled);

		// Token: 0x04002572 RID: 9586
		private static readonly int stateAutoSize = BitVector32.CreateMask(ToolStripItem.stateMouseDownAndNoDrag);

		// Token: 0x04002573 RID: 9587
		private static readonly int statePressed = BitVector32.CreateMask(ToolStripItem.stateAutoSize);

		// Token: 0x04002574 RID: 9588
		private static readonly int stateSelected = BitVector32.CreateMask(ToolStripItem.statePressed);

		// Token: 0x04002575 RID: 9589
		private static readonly int stateContstructing = BitVector32.CreateMask(ToolStripItem.stateSelected);

		// Token: 0x04002576 RID: 9590
		private static readonly int stateDisposed = BitVector32.CreateMask(ToolStripItem.stateContstructing);

		// Token: 0x04002577 RID: 9591
		private static readonly int stateCurrentlyAnimatingImage = BitVector32.CreateMask(ToolStripItem.stateDisposed);

		// Token: 0x04002578 RID: 9592
		private static readonly int stateDoubleClickEnabled = BitVector32.CreateMask(ToolStripItem.stateCurrentlyAnimatingImage);

		// Token: 0x04002579 RID: 9593
		private static readonly int stateAutoToolTip = BitVector32.CreateMask(ToolStripItem.stateDoubleClickEnabled);

		// Token: 0x0400257A RID: 9594
		private static readonly int stateSupportsRightClick = BitVector32.CreateMask(ToolStripItem.stateAutoToolTip);

		// Token: 0x0400257B RID: 9595
		private static readonly int stateSupportsItemClick = BitVector32.CreateMask(ToolStripItem.stateSupportsRightClick);

		// Token: 0x0400257C RID: 9596
		private static readonly int stateRightToLeftAutoMirrorImage = BitVector32.CreateMask(ToolStripItem.stateSupportsItemClick);

		// Token: 0x0400257D RID: 9597
		private static readonly int stateInvalidMirroredImage = BitVector32.CreateMask(ToolStripItem.stateRightToLeftAutoMirrorImage);

		// Token: 0x0400257E RID: 9598
		private static readonly int stateSupportsSpaceKey = BitVector32.CreateMask(ToolStripItem.stateInvalidMirroredImage);

		// Token: 0x0400257F RID: 9599
		private static readonly int stateMouseDownAndUpMustBeInSameItem = BitVector32.CreateMask(ToolStripItem.stateSupportsSpaceKey);

		// Token: 0x04002580 RID: 9600
		private static readonly int stateSupportsDisabledHotTracking = BitVector32.CreateMask(ToolStripItem.stateMouseDownAndUpMustBeInSameItem);

		// Token: 0x04002581 RID: 9601
		private static readonly int stateUseAmbientMargin = BitVector32.CreateMask(ToolStripItem.stateSupportsDisabledHotTracking);

		// Token: 0x04002582 RID: 9602
		private static readonly int stateDisposing = BitVector32.CreateMask(ToolStripItem.stateUseAmbientMargin);

		// Token: 0x04002583 RID: 9603
		private long lastClickTime;

		// Token: 0x04002584 RID: 9604
		private int deviceDpi = DpiHelper.DeviceDpi;

		// Token: 0x04002585 RID: 9605
		internal Font defaultFont = ToolStripManager.DefaultFont;

		/// <summary>Provides information that accessibility applications use to adjust the user interface of a <see cref="T:System.Windows.Forms.ToolStripItem" /> for users with impairments.</summary>
		// Token: 0x02000803 RID: 2051
		[ComVisible(true)]
		public class ToolStripItemAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject" /> class.</summary>
			/// <param name="ownerItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> that owns this <see cref="T:System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject" />.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="ownerItem" /> parameter is <see langword="null" />.</exception>
			// Token: 0x06006EC4 RID: 28356 RVA: 0x001958CA File Offset: 0x00193ACA
			public ToolStripItemAccessibleObject(ToolStripItem ownerItem)
			{
				if (ownerItem == null)
				{
					throw new ArgumentNullException("ownerItem");
				}
				this.ownerItem = ownerItem;
			}

			/// <summary>Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
			/// <returns>A description of the default action of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
			// Token: 0x17001839 RID: 6201
			// (get) Token: 0x06006EC5 RID: 28357 RVA: 0x001958E8 File Offset: 0x00193AE8
			public override string DefaultAction
			{
				get
				{
					string accessibleDefaultActionDescription = this.ownerItem.AccessibleDefaultActionDescription;
					if (accessibleDefaultActionDescription != null)
					{
						return accessibleDefaultActionDescription;
					}
					return SR.GetString("AccessibleActionPress");
				}
			}

			/// <summary>Gets the description of the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />.</summary>
			/// <returns>A string describing the <see cref="T:System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject" />.</returns>
			// Token: 0x1700183A RID: 6202
			// (get) Token: 0x06006EC6 RID: 28358 RVA: 0x00195910 File Offset: 0x00193B10
			public override string Description
			{
				get
				{
					string accessibleDescription = this.ownerItem.AccessibleDescription;
					if (accessibleDescription != null)
					{
						return accessibleDescription;
					}
					return base.Description;
				}
			}

			/// <summary>Gets the description of what the object does or how the object is used.</summary>
			/// <returns>A string describing what the object does or how the object is used.</returns>
			// Token: 0x1700183B RID: 6203
			// (get) Token: 0x06006EC7 RID: 28359 RVA: 0x00195934 File Offset: 0x00193B34
			public override string Help
			{
				get
				{
					QueryAccessibilityHelpEventHandler queryAccessibilityHelpEventHandler = (QueryAccessibilityHelpEventHandler)this.Owner.Events[ToolStripItem.EventQueryAccessibilityHelp];
					if (queryAccessibilityHelpEventHandler != null)
					{
						QueryAccessibilityHelpEventArgs queryAccessibilityHelpEventArgs = new QueryAccessibilityHelpEventArgs();
						queryAccessibilityHelpEventHandler(this.Owner, queryAccessibilityHelpEventArgs);
						return queryAccessibilityHelpEventArgs.HelpString;
					}
					return base.Help;
				}
			}

			// Token: 0x06006EC8 RID: 28360 RVA: 0x00183041 File Offset: 0x00181241
			internal override bool IsPatternSupported(int patternId)
			{
				return (AccessibilityImprovements.Level3 && patternId == 10018) || base.IsPatternSupported(patternId);
			}

			/// <summary>Gets the shortcut key or access key for the accessible object.</summary>
			/// <returns>The shortcut key or access key for the accessible object, or <see langword="null" /> if there is no shortcut key associated with the object.</returns>
			// Token: 0x1700183C RID: 6204
			// (get) Token: 0x06006EC9 RID: 28361 RVA: 0x00195980 File Offset: 0x00193B80
			public override string KeyboardShortcut
			{
				get
				{
					char mnemonic = WindowsFormsUtils.GetMnemonic(this.ownerItem.Text, false);
					if (this.ownerItem.IsOnDropDown)
					{
						if (mnemonic != '\0')
						{
							return mnemonic.ToString();
						}
						return string.Empty;
					}
					else
					{
						if (mnemonic != '\0')
						{
							return "Alt+" + mnemonic.ToString();
						}
						return string.Empty;
					}
				}
			}

			// Token: 0x1700183D RID: 6205
			// (get) Token: 0x06006ECA RID: 28362 RVA: 0x001959D8 File Offset: 0x00193BD8
			internal override int[] RuntimeId
			{
				get
				{
					if (AccessibilityImprovements.Level1)
					{
						if (this.runtimeId == null)
						{
							this.runtimeId = new int[2];
							this.runtimeId[0] = (AccessibilityImprovements.Level3 ? 3 : 42);
							this.runtimeId[1] = this.ownerItem.GetHashCode();
						}
						return this.runtimeId;
					}
					return base.RuntimeId;
				}
			}

			// Token: 0x06006ECB RID: 28363 RVA: 0x00195A34 File Offset: 0x00193C34
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level1)
				{
					if (propertyID == 30005)
					{
						return this.Name;
					}
					if (propertyID == 30028)
					{
						return this.IsPatternSupported(10005);
					}
				}
				if (AccessibilityImprovements.Level3)
				{
					switch (propertyID)
					{
					case 30007:
						return this.KeyboardShortcut;
					case 30008:
						return this.ownerItem.Selected;
					case 30009:
						return this.ownerItem.CanSelect;
					case 30010:
						return this.ownerItem.Enabled;
					case 30011:
					case 30012:
						break;
					case 30013:
						return this.Help ?? string.Empty;
					default:
						if (propertyID == 30019)
						{
							return false;
						}
						if (propertyID == 30022)
						{
							return (!AccessibilityImprovements.Level5) ? (this.ownerItem.Placement > ToolStripItemPlacement.Main) : ToolStripItem.GetIsOffscreenPropertyValue(this.ownerItem.Placement, this.Bounds);
						}
						break;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			/// <summary>Gets or sets the name of the accessible object.</summary>
			/// <returns>The object name, or <see langword="null" /> if the property has not been set.</returns>
			// Token: 0x1700183E RID: 6206
			// (get) Token: 0x06006ECC RID: 28364 RVA: 0x00195B44 File Offset: 0x00193D44
			// (set) Token: 0x06006ECD RID: 28365 RVA: 0x00195B86 File Offset: 0x00193D86
			public override string Name
			{
				get
				{
					string accessibleName = this.ownerItem.AccessibleName;
					if (accessibleName != null)
					{
						return accessibleName;
					}
					string name = base.Name;
					if (name == null || name.Length == 0)
					{
						return WindowsFormsUtils.TextWithoutMnemonics(this.ownerItem.Text);
					}
					return name;
				}
				set
				{
					this.ownerItem.AccessibleName = value;
				}
			}

			// Token: 0x1700183F RID: 6207
			// (get) Token: 0x06006ECE RID: 28366 RVA: 0x00195B94 File Offset: 0x00193D94
			internal ToolStripItem Owner
			{
				get
				{
					return this.ownerItem;
				}
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values.</returns>
			// Token: 0x17001840 RID: 6208
			// (get) Token: 0x06006ECF RID: 28367 RVA: 0x00195B9C File Offset: 0x00193D9C
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = this.ownerItem.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.PushButton;
				}
			}

			/// <summary>Gets the state of this accessible object.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values, or <see cref="F:System.Windows.Forms.AccessibleStates.None" /> if no state has been set.</returns>
			// Token: 0x17001841 RID: 6209
			// (get) Token: 0x06006ED0 RID: 28368 RVA: 0x00195BC0 File Offset: 0x00193DC0
			public override AccessibleStates State
			{
				get
				{
					if (!this.ownerItem.CanSelect)
					{
						return base.State | this.additionalState;
					}
					if (this.ownerItem.Enabled)
					{
						AccessibleStates accessibleStates = AccessibleStates.Focusable | this.additionalState;
						if (this.ownerItem.Selected || this.ownerItem.Pressed)
						{
							accessibleStates |= AccessibleStates.Focused | AccessibleStates.HotTracked;
						}
						if (this.ownerItem.Pressed)
						{
							accessibleStates |= AccessibleStates.Pressed;
						}
						return accessibleStates;
					}
					if (AccessibilityImprovements.Level2 && this.ownerItem.Selected && this.ownerItem is ToolStripMenuItem)
					{
						return AccessibleStates.Unavailable | this.additionalState | AccessibleStates.Focused;
					}
					if (AccessibilityImprovements.Level1 && this.ownerItem.Selected && this.ownerItem is ToolStripMenuItem)
					{
						return AccessibleStates.Focused;
					}
					return AccessibleStates.Unavailable | this.additionalState;
				}
			}

			/// <summary>Performs the default action associated with this accessible object.</summary>
			// Token: 0x06006ED1 RID: 28369 RVA: 0x00195C8E File Offset: 0x00193E8E
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (this.Owner != null)
				{
					this.Owner.PerformClick();
				}
			}

			/// <summary>Gets an identifier for a Help topic and the path to the Help file associated with this accessible object.</summary>
			/// <param name="fileName">When this method returns, contains a string that represents the path to the Help file associated with this accessible object. This parameter is passed without being initialized.</param>
			/// <returns>An identifier for a Help topic, or -1 if there is no Help topic. On return, the <paramref name="fileName" /> parameter will contain the path to the Help file associated with this accessible object, or <see langword="null" /> if there is no <see langword="IAccessible" /> interface specified.</returns>
			// Token: 0x06006ED2 RID: 28370 RVA: 0x00195CA4 File Offset: 0x00193EA4
			public override int GetHelpTopic(out string fileName)
			{
				int num = 0;
				QueryAccessibilityHelpEventHandler queryAccessibilityHelpEventHandler = (QueryAccessibilityHelpEventHandler)this.Owner.Events[ToolStripItem.EventQueryAccessibilityHelp];
				if (queryAccessibilityHelpEventHandler != null)
				{
					QueryAccessibilityHelpEventArgs queryAccessibilityHelpEventArgs = new QueryAccessibilityHelpEventArgs();
					queryAccessibilityHelpEventHandler(this.Owner, queryAccessibilityHelpEventArgs);
					fileName = queryAccessibilityHelpEventArgs.HelpNamespace;
					if (fileName != null && fileName.Length > 0)
					{
						IntSecurity.DemandFileIO(FileIOPermissionAccess.PathDiscovery, fileName);
					}
					try
					{
						num = int.Parse(queryAccessibilityHelpEventArgs.HelpKeyword, CultureInfo.InvariantCulture);
					}
					catch
					{
					}
					return num;
				}
				return base.GetHelpTopic(out fileName);
			}

			/// <summary>Navigates to another accessible object.</summary>
			/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents one of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</returns>
			// Token: 0x06006ED3 RID: 28371 RVA: 0x00195D34 File Offset: 0x00193F34
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				ToolStripItem toolStripItem = null;
				if (this.Owner != null)
				{
					ToolStrip parentInternal = this.Owner.ParentInternal;
					if (parentInternal == null)
					{
						return null;
					}
					bool flag = parentInternal.RightToLeft == RightToLeft.No;
					switch (navigationDirection)
					{
					case AccessibleNavigation.Up:
						toolStripItem = (this.Owner.IsOnDropDown ? parentInternal.GetNextItem(this.Owner, ArrowDirection.Up) : parentInternal.GetNextItem(this.Owner, ArrowDirection.Left, true));
						break;
					case AccessibleNavigation.Down:
						toolStripItem = (this.Owner.IsOnDropDown ? parentInternal.GetNextItem(this.Owner, ArrowDirection.Down) : parentInternal.GetNextItem(this.Owner, ArrowDirection.Right, true));
						break;
					case AccessibleNavigation.Left:
					case AccessibleNavigation.Previous:
						toolStripItem = parentInternal.GetNextItem(this.Owner, ArrowDirection.Left, true);
						break;
					case AccessibleNavigation.Right:
					case AccessibleNavigation.Next:
						toolStripItem = parentInternal.GetNextItem(this.Owner, ArrowDirection.Right, true);
						break;
					case AccessibleNavigation.FirstChild:
						toolStripItem = parentInternal.GetNextItem(null, ArrowDirection.Right, true);
						break;
					case AccessibleNavigation.LastChild:
						toolStripItem = parentInternal.GetNextItem(null, ArrowDirection.Left, true);
						break;
					}
				}
				if (toolStripItem != null)
				{
					return toolStripItem.AccessibilityObject;
				}
				return null;
			}

			/// <summary>Adds a <see cref="P:System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject.State" /> if <see cref="T:System.Windows.Forms.AccessibleStates" /> is <see cref="F:System.Windows.Forms.AccessibleStates.None" />.</summary>
			/// <param name="state">One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values other than <see cref="F:System.Windows.Forms.AccessibleStates.None" />.</param>
			// Token: 0x06006ED4 RID: 28372 RVA: 0x00195E3A File Offset: 0x0019403A
			public void AddState(AccessibleStates state)
			{
				if (state == AccessibleStates.None)
				{
					this.additionalState = state;
					return;
				}
				this.additionalState |= state;
			}

			/// <summary>Returns a string that represents the current object.</summary>
			/// <returns>A string that represents the current object.</returns>
			// Token: 0x06006ED5 RID: 28373 RVA: 0x00195E55 File Offset: 0x00194055
			public override string ToString()
			{
				if (this.Owner != null)
				{
					return "ToolStripItemAccessibleObject: Owner = " + this.Owner.ToString();
				}
				return "ToolStripItemAccessibleObject: Owner = null";
			}

			/// <summary>Gets the bounds of the accessible object, in screen coordinates.</summary>
			/// <returns>An object of type <see cref="T:System.Drawing.Rectangle" /> representing the bounds.</returns>
			// Token: 0x17001842 RID: 6210
			// (get) Token: 0x06006ED6 RID: 28374 RVA: 0x00195E7C File Offset: 0x0019407C
			public override Rectangle Bounds
			{
				get
				{
					Rectangle bounds = this.Owner.Bounds;
					if (this.Owner.ParentInternal != null && this.Owner.ParentInternal.Visible)
					{
						return new Rectangle(this.Owner.ParentInternal.PointToScreen(bounds.Location), bounds.Size);
					}
					return Rectangle.Empty;
				}
			}

			/// <summary>Gets or sets the parent of an accessible object.</summary>
			/// <returns>An object of type <see cref="T:System.Windows.Forms.AccessibleObject" /> representing the parent.</returns>
			// Token: 0x17001843 RID: 6211
			// (get) Token: 0x06006ED7 RID: 28375 RVA: 0x00195EE0 File Offset: 0x001940E0
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (this.Owner.IsOnDropDown)
					{
						ToolStripDropDown currentParentDropDown = this.Owner.GetCurrentParentDropDown();
						if (currentParentDropDown.OwnerItem != null)
						{
							return currentParentDropDown.OwnerItem.AccessibilityObject;
						}
						return currentParentDropDown.AccessibilityObject;
					}
					else
					{
						if (this.Owner.Parent == null)
						{
							return base.Parent;
						}
						return this.Owner.Parent.AccessibilityObject;
					}
				}
			}

			// Token: 0x17001844 RID: 6212
			// (get) Token: 0x06006ED8 RID: 28376 RVA: 0x00195F45 File Offset: 0x00194145
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					ToolStrip rootToolStrip = this.ownerItem.RootToolStrip;
					if (rootToolStrip == null)
					{
						return null;
					}
					return rootToolStrip.AccessibilityObject;
				}
			}

			// Token: 0x06006ED9 RID: 28377 RVA: 0x00195F60 File Offset: 0x00194160
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.Parent)
				{
					return this.Parent;
				}
				if (direction - UnsafeNativeMethods.NavigateDirection.NextSibling > 1)
				{
					return base.FragmentNavigate(direction);
				}
				int num = this.GetChildFragmentIndex();
				if (num == -1)
				{
					return null;
				}
				int num2 = ((direction == UnsafeNativeMethods.NavigateDirection.NextSibling) ? 1 : (-1));
				AccessibleObject accessibleObject = null;
				if (AccessibilityImprovements.Level3)
				{
					num += num2;
					int childFragmentCount = this.GetChildFragmentCount();
					if (num >= 0 && num < childFragmentCount)
					{
						accessibleObject = this.GetChildFragment(num, direction);
					}
				}
				else
				{
					do
					{
						num += num2;
						accessibleObject = ((num >= 0 && num < this.Parent.GetChildCount()) ? this.Parent.GetChild(num) : null);
					}
					while (accessibleObject != null && accessibleObject is Control.ControlAccessibleObject);
				}
				return accessibleObject;
			}

			// Token: 0x06006EDA RID: 28378 RVA: 0x00195FF8 File Offset: 0x001941F8
			private AccessibleObject GetChildFragment(int index, UnsafeNativeMethods.NavigateDirection direction = UnsafeNativeMethods.NavigateDirection.NextSibling)
			{
				ToolStrip.ToolStripAccessibleObject toolStripAccessibleObject = this.Parent as ToolStrip.ToolStripAccessibleObject;
				if (toolStripAccessibleObject != null)
				{
					return toolStripAccessibleObject.GetChildFragment(index, false, direction);
				}
				ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject toolStripOverflowButtonAccessibleObject = this.Parent as ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject;
				if (toolStripOverflowButtonAccessibleObject != null)
				{
					ToolStrip.ToolStripAccessibleObject toolStripAccessibleObject2 = toolStripOverflowButtonAccessibleObject.Parent as ToolStrip.ToolStripAccessibleObject;
					if (toolStripAccessibleObject2 != null)
					{
						return toolStripAccessibleObject2.GetChildFragment(index, true, direction);
					}
				}
				ToolStripDropDownItemAccessibleObject toolStripDropDownItemAccessibleObject = this.Parent as ToolStripDropDownItemAccessibleObject;
				if (toolStripDropDownItemAccessibleObject != null)
				{
					return toolStripDropDownItemAccessibleObject.GetChildFragment(index, direction);
				}
				return null;
			}

			// Token: 0x06006EDB RID: 28379 RVA: 0x00196060 File Offset: 0x00194260
			private int GetChildFragmentCount()
			{
				ToolStrip.ToolStripAccessibleObject toolStripAccessibleObject = this.Parent as ToolStrip.ToolStripAccessibleObject;
				if (toolStripAccessibleObject != null)
				{
					return toolStripAccessibleObject.GetChildFragmentCount();
				}
				ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject toolStripOverflowButtonAccessibleObject = this.Parent as ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject;
				if (toolStripOverflowButtonAccessibleObject != null)
				{
					ToolStrip.ToolStripAccessibleObject toolStripAccessibleObject2 = toolStripOverflowButtonAccessibleObject.Parent as ToolStrip.ToolStripAccessibleObject;
					if (toolStripAccessibleObject2 != null)
					{
						return toolStripAccessibleObject2.GetChildOverflowFragmentCount();
					}
				}
				ToolStripDropDownItemAccessibleObject toolStripDropDownItemAccessibleObject = this.Parent as ToolStripDropDownItemAccessibleObject;
				if (toolStripDropDownItemAccessibleObject != null)
				{
					return toolStripDropDownItemAccessibleObject.GetChildCount();
				}
				return -1;
			}

			// Token: 0x06006EDC RID: 28380 RVA: 0x001960C0 File Offset: 0x001942C0
			private int GetChildFragmentIndex()
			{
				ToolStrip.ToolStripAccessibleObject toolStripAccessibleObject = this.Parent as ToolStrip.ToolStripAccessibleObject;
				if (toolStripAccessibleObject != null)
				{
					return toolStripAccessibleObject.GetChildFragmentIndex(this);
				}
				ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject toolStripOverflowButtonAccessibleObject = this.Parent as ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject;
				if (toolStripOverflowButtonAccessibleObject != null)
				{
					ToolStrip.ToolStripAccessibleObject toolStripAccessibleObject2 = toolStripOverflowButtonAccessibleObject.Parent as ToolStrip.ToolStripAccessibleObject;
					if (toolStripAccessibleObject2 != null)
					{
						return toolStripAccessibleObject2.GetChildFragmentIndex(this);
					}
				}
				ToolStripDropDownItemAccessibleObject toolStripDropDownItemAccessibleObject = this.Parent as ToolStripDropDownItemAccessibleObject;
				if (toolStripDropDownItemAccessibleObject != null)
				{
					return toolStripDropDownItemAccessibleObject.GetChildFragmentIndex(this);
				}
				return -1;
			}

			// Token: 0x06006EDD RID: 28381 RVA: 0x00196122 File Offset: 0x00194322
			internal override void SetFocus()
			{
				this.Owner.Select();
			}

			// Token: 0x06006EDE RID: 28382 RVA: 0x00196130 File Offset: 0x00194330
			internal void RaiseFocusChanged()
			{
				ToolStrip rootToolStrip = this.ownerItem.RootToolStrip;
				if (rootToolStrip != null && rootToolStrip.SupportsUiaProviders)
				{
					base.RaiseAutomationEvent(20005);
				}
			}

			// Token: 0x040042F8 RID: 17144
			private ToolStripItem ownerItem;

			// Token: 0x040042F9 RID: 17145
			private AccessibleStates additionalState;

			// Token: 0x040042FA RID: 17146
			private int[] runtimeId;
		}
	}
}
