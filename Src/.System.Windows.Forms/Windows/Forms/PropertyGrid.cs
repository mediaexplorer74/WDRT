using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Automation;
using System.Windows.Forms.ComponentModel.Com2Interop;
using System.Windows.Forms.Design;
using System.Windows.Forms.PropertyGridInternal;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Provides a user interface for browsing the properties of an object.</summary>
	// Token: 0x0200032A RID: 810
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.PropertyGridDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionPropertyGrid")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class PropertyGrid : ContainerControl, IComPropertyBrowser, UnsafeNativeMethods.IPropertyNotifySink
	{
		// Token: 0x06003400 RID: 13312 RVA: 0x000EBA37 File Offset: 0x000E9C37
		private bool GetFlag(ushort flag)
		{
			return (this.flags & flag) > 0;
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000EBA44 File Offset: 0x000E9C44
		private void SetFlag(ushort flag, bool value)
		{
			if (value)
			{
				this.flags |= flag;
				return;
			}
			this.flags &= ~flag;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyGrid" /> class.</summary>
		// Token: 0x06003402 RID: 13314 RVA: 0x000EBA6C File Offset: 0x000E9C6C
		public PropertyGrid()
		{
			this.onComponentAdd = new ComponentEventHandler(this.OnComponentAdd);
			this.onComponentRemove = new ComponentEventHandler(this.OnComponentRemove);
			this.onComponentChanged = new ComponentChangedEventHandler(this.OnComponentChanged);
			base.SuspendLayout();
			base.AutoScaleMode = AutoScaleMode.None;
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.RescaleConstants();
			}
			else if (!PropertyGrid.isScalingInitialized)
			{
				if (DpiHelper.IsScalingRequired)
				{
					PropertyGrid.normalButtonSize = base.LogicalToDeviceUnits(PropertyGrid.DEFAULT_NORMAL_BUTTON_SIZE);
					PropertyGrid.largeButtonSize = base.LogicalToDeviceUnits(PropertyGrid.DEFAULT_LARGE_BUTTON_SIZE);
				}
				PropertyGrid.isScalingInitialized = true;
			}
			try
			{
				this.gridView = this.CreateGridView(null);
				this.gridView.TabStop = true;
				this.gridView.MouseMove += this.OnChildMouseMove;
				this.gridView.MouseDown += this.OnChildMouseDown;
				this.gridView.TabIndex = 2;
				this.separator1 = this.CreateSeparatorButton();
				this.separator2 = this.CreateSeparatorButton();
				this.toolStrip = (AccessibilityImprovements.Level3 ? new PropertyGridToolStrip(this) : new ToolStrip());
				this.toolStrip.SuspendLayout();
				this.toolStrip.ShowItemToolTips = true;
				this.toolStrip.AccessibleName = (AccessibilityImprovements.Level4 ? "Property Grid" : SR.GetString("PropertyGridToolbarAccessibleName"));
				this.toolStrip.AccessibleRole = AccessibleRole.ToolBar;
				this.toolStrip.TabStop = true;
				this.toolStrip.AllowMerge = false;
				this.toolStrip.Text = "PropertyGridToolBar";
				this.toolStrip.Dock = DockStyle.None;
				this.toolStrip.AutoSize = false;
				this.toolStrip.TabIndex = 1;
				this.toolStrip.ImageScalingSize = PropertyGrid.normalButtonSize;
				this.toolStrip.CanOverflow = false;
				this.toolStrip.GripStyle = ToolStripGripStyle.Hidden;
				Padding padding = this.toolStrip.Padding;
				padding.Left = 2;
				this.toolStrip.Padding = padding;
				this.SetToolStripRenderer();
				this.AddRefTab(this.DefaultTabType, null, PropertyTabScope.Static, true);
				this.doccomment = new DocComment(this);
				this.doccomment.SuspendLayout();
				this.doccomment.TabStop = false;
				this.doccomment.Dock = DockStyle.None;
				this.doccomment.BackColor = SystemColors.Control;
				this.doccomment.ForeColor = SystemColors.ControlText;
				this.doccomment.MouseMove += this.OnChildMouseMove;
				this.doccomment.MouseDown += this.OnChildMouseDown;
				if (AccessibilityImprovements.Level4)
				{
					this.doccomment.AccessibleName = "Description";
				}
				this.hotcommands = new HotCommands(this);
				this.hotcommands.SuspendLayout();
				this.hotcommands.TabIndex = 3;
				this.hotcommands.Dock = DockStyle.None;
				this.SetHotCommandColors(false);
				this.hotcommands.Visible = false;
				this.hotcommands.MouseMove += this.OnChildMouseMove;
				this.hotcommands.MouseDown += this.OnChildMouseDown;
				if (AccessibilityImprovements.Level4)
				{
					this.hotcommands.AccessibleName = "Hot commands";
				}
				this.Controls.AddRange(new Control[] { this.doccomment, this.hotcommands, this.gridView, this.toolStrip });
				base.SetActiveControlInternal(this.gridView);
				this.toolStrip.ResumeLayout(false);
				this.SetupToolbar();
				this.PropertySort = PropertySort.CategorizedAlphabetical;
				this.Text = "PropertyGrid";
				this.SetSelectState(0);
			}
			catch (Exception ex)
			{
			}
			finally
			{
				if (this.doccomment != null)
				{
					this.doccomment.ResumeLayout(false);
				}
				if (this.hotcommands != null)
				{
					this.hotcommands.ResumeLayout(false);
				}
				base.ResumeLayout(true);
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06003403 RID: 13315 RVA: 0x000EBF28 File Offset: 0x000EA128
		// (set) Token: 0x06003404 RID: 13316 RVA: 0x000EBF54 File Offset: 0x000EA154
		internal IDesignerHost ActiveDesigner
		{
			get
			{
				if (this.designerHost == null)
				{
					this.designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				}
				return this.designerHost;
			}
			set
			{
				if (value != this.designerHost)
				{
					this.SetFlag(32, true);
					if (this.designerHost != null)
					{
						IComponentChangeService componentChangeService = (IComponentChangeService)this.designerHost.GetService(typeof(IComponentChangeService));
						if (componentChangeService != null)
						{
							componentChangeService.ComponentAdded -= this.onComponentAdd;
							componentChangeService.ComponentRemoved -= this.onComponentRemove;
							componentChangeService.ComponentChanged -= this.onComponentChanged;
						}
						IPropertyValueUIService propertyValueUIService = (IPropertyValueUIService)this.designerHost.GetService(typeof(IPropertyValueUIService));
						if (propertyValueUIService != null)
						{
							propertyValueUIService.PropertyUIValueItemsChanged -= this.OnNotifyPropertyValueUIItemsChanged;
						}
						this.designerHost.TransactionOpened -= this.OnTransactionOpened;
						this.designerHost.TransactionClosed -= this.OnTransactionClosed;
						this.SetFlag(16, false);
						this.RemoveTabs(PropertyTabScope.Document, true);
						this.designerHost = null;
					}
					if (value != null)
					{
						IComponentChangeService componentChangeService2 = (IComponentChangeService)value.GetService(typeof(IComponentChangeService));
						if (componentChangeService2 != null)
						{
							componentChangeService2.ComponentAdded += this.onComponentAdd;
							componentChangeService2.ComponentRemoved += this.onComponentRemove;
							componentChangeService2.ComponentChanged += this.onComponentChanged;
						}
						value.TransactionOpened += this.OnTransactionOpened;
						value.TransactionClosed += this.OnTransactionClosed;
						this.SetFlag(16, false);
						IPropertyValueUIService propertyValueUIService2 = (IPropertyValueUIService)value.GetService(typeof(IPropertyValueUIService));
						if (propertyValueUIService2 != null)
						{
							propertyValueUIService2.PropertyUIValueItemsChanged += this.OnNotifyPropertyValueUIItemsChanged;
						}
					}
					this.designerHost = value;
					if (this.peMain != null)
					{
						this.peMain.DesignerHost = value;
					}
					this.RefreshTabs(PropertyTabScope.Document);
				}
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06003405 RID: 13317 RVA: 0x000B0A3B File Offset: 0x000AEC3B
		// (set) Token: 0x06003406 RID: 13318 RVA: 0x000EC0F6 File Offset: 0x000EA2F6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		/// <summary>Gets or sets the background color for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06003407 RID: 13319 RVA: 0x0001A049 File Offset: 0x00018249
		// (set) Token: 0x06003408 RID: 13320 RVA: 0x000EC0FF File Offset: 0x000EA2FF
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
				this.toolStrip.BackColor = value;
				this.toolStrip.Invalidate(true);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The background image of the property grid.</returns>
		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06003409 RID: 13321 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x0600340A RID: 13322 RVA: 0x00011884 File Offset: 0x0000FA84
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PropertyGrid.BackgroundImage" /> property changes.</summary>
		// Token: 0x14000272 RID: 626
		// (add) Token: 0x0600340B RID: 13323 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x0600340C RID: 13324 RVA: 0x00011896 File Offset: 0x0000FA96
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

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The background image layout of the property grid.</returns>
		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x0600340D RID: 13325 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x0600340E RID: 13326 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PropertyGrid.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x14000273 RID: 627
		// (add) Token: 0x0600340F RID: 13327 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x06003410 RID: 13328 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>Gets or sets the browsable attributes associated with the object that the property grid is attached to.</summary>
		/// <returns>The collection of browsable attributes associated with the object.</returns>
		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x000EC1A4 File Offset: 0x000EA3A4
		// (set) Token: 0x06003411 RID: 13329 RVA: 0x000EC120 File Offset: 0x000EA320
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AttributeCollection BrowsableAttributes
		{
			get
			{
				if (this.browsableAttributes == null)
				{
					this.browsableAttributes = new AttributeCollection(new Attribute[]
					{
						new BrowsableAttribute(true)
					});
				}
				return this.browsableAttributes;
			}
			set
			{
				if (value == null || value == AttributeCollection.Empty)
				{
					this.browsableAttributes = new AttributeCollection(new Attribute[] { BrowsableAttribute.Yes });
				}
				else
				{
					Attribute[] array = new Attribute[value.Count];
					value.CopyTo(array, 0);
					this.browsableAttributes = new AttributeCollection(array);
				}
				if (this.currentObjects != null && this.currentObjects.Length != 0 && this.peMain != null)
				{
					this.peMain.BrowsableAttributes = this.BrowsableAttributes;
					this.Refresh(true);
				}
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x000EC1CE File Offset: 0x000EA3CE
		private bool CanCopy
		{
			get
			{
				return this.gridView.CanCopy;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06003414 RID: 13332 RVA: 0x000EC1DB File Offset: 0x000EA3DB
		private bool CanCut
		{
			get
			{
				return this.gridView.CanCut;
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x000EC1E8 File Offset: 0x000EA3E8
		private bool CanPaste
		{
			get
			{
				return this.gridView.CanPaste;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06003416 RID: 13334 RVA: 0x000EC1F5 File Offset: 0x000EA3F5
		private bool CanUndo
		{
			get
			{
				return this.gridView.CanUndo;
			}
		}

		/// <summary>Gets a value indicating whether the commands pane can be made visible for the currently selected objects.</summary>
		/// <returns>
		///   <see langword="true" /> if the commands pane can be made visible; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x000EC202 File Offset: 0x000EA402
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("PropertyGridCanShowCommandsDesc")]
		public virtual bool CanShowCommands
		{
			get
			{
				return this.hotcommands.WouldBeVisible;
			}
		}

		/// <summary>Gets or sets the text color used for category headings.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the text color.</returns>
		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06003418 RID: 13336 RVA: 0x000EC20F File Offset: 0x000EA40F
		// (set) Token: 0x06003419 RID: 13337 RVA: 0x000EC217 File Offset: 0x000EA417
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridCategoryForeColorDesc")]
		[DefaultValue(typeof(Color), "ControlText")]
		public Color CategoryForeColor
		{
			get
			{
				return this.categoryForeColor;
			}
			set
			{
				if (this.categoryForeColor != value)
				{
					this.categoryForeColor = value;
					this.gridView.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the background color of the hot commands region.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is the default system color for controls.</returns>
		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x0600341A RID: 13338 RVA: 0x000EC239 File Offset: 0x000EA439
		// (set) Token: 0x0600341B RID: 13339 RVA: 0x000EC246 File Offset: 0x000EA446
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridCommandsBackColorDesc")]
		public Color CommandsBackColor
		{
			get
			{
				return this.hotcommands.BackColor;
			}
			set
			{
				this.hotcommands.BackColor = value;
				this.hotcommands.Label.BackColor = value;
			}
		}

		/// <summary>Gets or sets the foreground color for the hot commands region.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is the default system color for control text.</returns>
		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x0600341C RID: 13340 RVA: 0x000EC265 File Offset: 0x000EA465
		// (set) Token: 0x0600341D RID: 13341 RVA: 0x000EC272 File Offset: 0x000EA472
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridCommandsForeColorDesc")]
		public Color CommandsForeColor
		{
			get
			{
				return this.hotcommands.ForeColor;
			}
			set
			{
				this.hotcommands.ForeColor = value;
				this.hotcommands.Label.ForeColor = value;
			}
		}

		/// <summary>Gets or sets the link color for the executable commands region.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the link color for the executable commands region.</returns>
		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x0600341E RID: 13342 RVA: 0x000EC291 File Offset: 0x000EA491
		// (set) Token: 0x0600341F RID: 13343 RVA: 0x000EC2A3 File Offset: 0x000EA4A3
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridCommandsLinkColorDesc")]
		public Color CommandsLinkColor
		{
			get
			{
				return this.hotcommands.Label.LinkColor;
			}
			set
			{
				this.hotcommands.Label.LinkColor = value;
			}
		}

		/// <summary>Gets or sets the color of active links in the executable commands region.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the active link color.</returns>
		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06003420 RID: 13344 RVA: 0x000EC2B6 File Offset: 0x000EA4B6
		// (set) Token: 0x06003421 RID: 13345 RVA: 0x000EC2C8 File Offset: 0x000EA4C8
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridCommandsActiveLinkColorDesc")]
		public Color CommandsActiveLinkColor
		{
			get
			{
				return this.hotcommands.Label.ActiveLinkColor;
			}
			set
			{
				this.hotcommands.Label.ActiveLinkColor = value;
			}
		}

		/// <summary>Gets or sets the unavailable link color for the executable commands region.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the unavailable link color.</returns>
		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x000EC2DB File Offset: 0x000EA4DB
		// (set) Token: 0x06003423 RID: 13347 RVA: 0x000EC2ED File Offset: 0x000EA4ED
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridCommandsDisabledLinkColorDesc")]
		public Color CommandsDisabledLinkColor
		{
			get
			{
				return this.hotcommands.Label.DisabledLinkColor;
			}
			set
			{
				this.hotcommands.Label.DisabledLinkColor = value;
			}
		}

		/// <summary>Gets or sets the color of the border surrounding the hot commands region.</summary>
		/// <returns>The color of the commands border.</returns>
		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06003424 RID: 13348 RVA: 0x000EC300 File Offset: 0x000EA500
		// (set) Token: 0x06003425 RID: 13349 RVA: 0x000EC30D File Offset: 0x000EA50D
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridCommandsBorderColorDesc")]
		[DefaultValue(typeof(Color), "ControlDark")]
		public Color CommandsBorderColor
		{
			get
			{
				return this.hotcommands.BorderColor;
			}
			set
			{
				this.hotcommands.BorderColor = value;
			}
		}

		/// <summary>Gets a value indicating whether the commands pane is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the commands pane is visible; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06003426 RID: 13350 RVA: 0x000EC31B File Offset: 0x000EA51B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual bool CommandsVisible
		{
			get
			{
				return this.hotcommands.Visible;
			}
		}

		/// <summary>Gets or sets a value indicating whether the commands pane is visible for objects that expose verbs.</summary>
		/// <returns>
		///   <see langword="true" /> if the commands pane is visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06003427 RID: 13351 RVA: 0x000EC328 File Offset: 0x000EA528
		// (set) Token: 0x06003428 RID: 13352 RVA: 0x000EC338 File Offset: 0x000EA538
		[SRCategory("CatAppearance")]
		[DefaultValue(true)]
		[SRDescription("PropertyGridCommandsVisibleIfAvailable")]
		public virtual bool CommandsVisibleIfAvailable
		{
			get
			{
				return this.hotcommands.AllowVisible;
			}
			set
			{
				bool visible = this.hotcommands.Visible;
				this.hotcommands.AllowVisible = value;
				if (visible != this.hotcommands.Visible)
				{
					this.OnLayoutInternal(false);
					this.hotcommands.Invalidate();
				}
			}
		}

		/// <summary>Gets the default location for the shortcut menu.</summary>
		/// <returns>The default location for the shortcut menu if the command is invoked. Typically, this is centered over the selected property.</returns>
		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06003429 RID: 13353 RVA: 0x000EC37D File Offset: 0x000EA57D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Point ContextMenuDefaultLocation
		{
			get
			{
				return this.GetPropertyGridView().ContextMenuDefaultLocation;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The controls associated with the property grid.</returns>
		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x000EC38A File Offset: 0x000EA58A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Control.ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x000EC392 File Offset: 0x000EA592
		protected override Size DefaultSize
		{
			get
			{
				return new Size(130, 130);
			}
		}

		/// <summary>Gets the type of the default tab.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the default tab.</returns>
		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x0600342C RID: 13356 RVA: 0x000EC3A3 File Offset: 0x000EA5A3
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual Type DefaultTabType
		{
			get
			{
				return typeof(PropertiesTab);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.PropertyGrid" /> control paints its toolbar with flat buttons.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.PropertyGrid" /> paints its toolbar with flat buttons; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x0600342D RID: 13357 RVA: 0x000EC3AF File Offset: 0x000EA5AF
		// (set) Token: 0x0600342E RID: 13358 RVA: 0x000EC3B7 File Offset: 0x000EA5B7
		protected bool DrawFlatToolbar
		{
			get
			{
				return this.drawFlatToolBar;
			}
			set
			{
				if (this.drawFlatToolBar != value)
				{
					this.drawFlatToolBar = value;
					this.SetToolStripRenderer();
				}
				this.SetHotCommandColors(value && !AccessibilityImprovements.Level2);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The foreground color of the control.</returns>
		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x0001A0E7 File Offset: 0x000182E7
		// (set) Token: 0x06003430 RID: 13360 RVA: 0x00013024 File Offset: 0x00011224
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PropertyGrid.ForeColor" /> property changes.</summary>
		// Token: 0x14000274 RID: 628
		// (add) Token: 0x06003431 RID: 13361 RVA: 0x0005A8EE File Offset: 0x00058AEE
		// (remove) Token: 0x06003432 RID: 13362 RVA: 0x0005A8F7 File Offset: 0x00058AF7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x000EC3E3 File Offset: 0x000EA5E3
		// (set) Token: 0x06003434 RID: 13364 RVA: 0x000EC3F0 File Offset: 0x000EA5F0
		private bool FreezePainting
		{
			get
			{
				return this.paintFrozen > 0;
			}
			set
			{
				if (value && base.IsHandleCreated && base.Visible)
				{
					int num = this.paintFrozen;
					this.paintFrozen = num + 1;
					if (num == 0)
					{
						base.SendMessage(11, 0, 0);
					}
				}
				if (!value)
				{
					if (this.paintFrozen == 0)
					{
						return;
					}
					int num = this.paintFrozen - 1;
					this.paintFrozen = num;
					if (num == 0)
					{
						base.SendMessage(11, 1, 0);
						base.Invalidate(true);
					}
				}
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x000EC45F File Offset: 0x000EA65F
		internal AccessibleObject HelpAccessibleObject
		{
			get
			{
				return this.doccomment.AccessibilityObject;
			}
		}

		/// <summary>Gets or sets the background color for the Help region.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is the default system color for controls.</returns>
		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x000EC46C File Offset: 0x000EA66C
		// (set) Token: 0x06003437 RID: 13367 RVA: 0x000EC479 File Offset: 0x000EA679
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridHelpBackColorDesc")]
		[DefaultValue(typeof(Color), "Control")]
		public Color HelpBackColor
		{
			get
			{
				return this.doccomment.BackColor;
			}
			set
			{
				this.doccomment.BackColor = value;
			}
		}

		/// <summary>Gets or sets the foreground color for the Help region.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is the default system color for control text.</returns>
		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06003438 RID: 13368 RVA: 0x000EC487 File Offset: 0x000EA687
		// (set) Token: 0x06003439 RID: 13369 RVA: 0x000EC494 File Offset: 0x000EA694
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridHelpForeColorDesc")]
		[DefaultValue(typeof(Color), "ControlText")]
		public Color HelpForeColor
		{
			get
			{
				return this.doccomment.ForeColor;
			}
			set
			{
				this.doccomment.ForeColor = value;
			}
		}

		/// <summary>Gets or sets the color of the border surrounding the description pane.</summary>
		/// <returns>The color of the help border.</returns>
		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x0600343A RID: 13370 RVA: 0x000EC4A2 File Offset: 0x000EA6A2
		// (set) Token: 0x0600343B RID: 13371 RVA: 0x000EC4AF File Offset: 0x000EA6AF
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridHelpBorderColorDesc")]
		[DefaultValue(typeof(Color), "ControlDark")]
		public Color HelpBorderColor
		{
			get
			{
				return this.doccomment.BorderColor;
			}
			set
			{
				this.doccomment.BorderColor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Help text is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the help text is visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x0600343C RID: 13372 RVA: 0x000EC4BD File Offset: 0x000EA6BD
		// (set) Token: 0x0600343D RID: 13373 RVA: 0x000EC4C5 File Offset: 0x000EA6C5
		[SRCategory("CatAppearance")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("PropertyGridHelpVisibleDesc")]
		public virtual bool HelpVisible
		{
			get
			{
				return this.helpVisible;
			}
			set
			{
				this.helpVisible = value;
				this.doccomment.Visible = value;
				this.OnLayoutInternal(false);
				base.Invalidate();
				this.doccomment.Invalidate();
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x0600343E RID: 13374 RVA: 0x000EC4F2 File Offset: 0x000EA6F2
		internal AccessibleObject HotCommandsAccessibleObject
		{
			get
			{
				return this.hotcommands.AccessibilityObject;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x0600343F RID: 13375 RVA: 0x000EC4FF File Offset: 0x000EA6FF
		internal AccessibleObject GridViewAccessibleObject
		{
			get
			{
				return this.gridView.AccessibilityObject;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06003440 RID: 13376 RVA: 0x000EC50C File Offset: 0x000EA70C
		internal bool GridViewVisible
		{
			get
			{
				return this.gridView != null && this.gridView.Visible;
			}
		}

		/// <summary>Gets or sets the background color of selected items that have the input focus.</summary>
		/// <returns>The background color of focused, selected items.</returns>
		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06003441 RID: 13377 RVA: 0x000EC523 File Offset: 0x000EA723
		// (set) Token: 0x06003442 RID: 13378 RVA: 0x000EC52B File Offset: 0x000EA72B
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridSelectedItemWithFocusBackColorDesc")]
		[DefaultValue(typeof(Color), "Highlight")]
		public Color SelectedItemWithFocusBackColor
		{
			get
			{
				return this.selectedItemWithFocusBackColor;
			}
			set
			{
				if (this.selectedItemWithFocusBackColor != value)
				{
					this.selectedItemWithFocusBackColor = value;
					this.gridView.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the foreground color of selected items that have the input focus.</summary>
		/// <returns>The foreground color of focused, selected items.</returns>
		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06003443 RID: 13379 RVA: 0x000EC54D File Offset: 0x000EA74D
		// (set) Token: 0x06003444 RID: 13380 RVA: 0x000EC555 File Offset: 0x000EA755
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridSelectedItemWithFocusForeColorDesc")]
		[DefaultValue(typeof(Color), "HighlightText")]
		public Color SelectedItemWithFocusForeColor
		{
			get
			{
				return this.selectedItemWithFocusForeColor;
			}
			set
			{
				if (this.selectedItemWithFocusForeColor != value)
				{
					this.selectedItemWithFocusForeColor = value;
					this.gridView.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the foreground color of disabled text in the grid area.</summary>
		/// <returns>The foreground color of disabled items.</returns>
		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06003445 RID: 13381 RVA: 0x000EC577 File Offset: 0x000EA777
		// (set) Token: 0x06003446 RID: 13382 RVA: 0x000EC584 File Offset: 0x000EA784
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridDisabledItemForeColorDesc")]
		[DefaultValue(typeof(Color), "GrayText")]
		public Color DisabledItemForeColor
		{
			get
			{
				return this.gridView.GrayTextColor;
			}
			set
			{
				this.gridView.GrayTextColor = value;
				this.gridView.Invalidate();
			}
		}

		/// <summary>Gets or sets the color of the line that separates categories in the grid area.</summary>
		/// <returns>The color of the category splitter.</returns>
		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06003447 RID: 13383 RVA: 0x000EC59D File Offset: 0x000EA79D
		// (set) Token: 0x06003448 RID: 13384 RVA: 0x000EC5A5 File Offset: 0x000EA7A5
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridCategorySplitterColorDesc")]
		[DefaultValue(typeof(Color), "Control")]
		public Color CategorySplitterColor
		{
			get
			{
				return this.categorySplitterColor;
			}
			set
			{
				if (this.categorySplitterColor != value)
				{
					this.categorySplitterColor = value;
					this.gridView.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether OS-specific visual style glyphs are used for the expansion nodes in the grid area.</summary>
		/// <returns>
		///   <see langword="true" /> to use the visual style glyphs; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x000EC5C7 File Offset: 0x000EA7C7
		// (set) Token: 0x0600344A RID: 13386 RVA: 0x000EC5CF File Offset: 0x000EA7CF
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridCanShowVisualStyleGlyphsDesc")]
		[DefaultValue(true)]
		public bool CanShowVisualStyleGlyphs
		{
			get
			{
				return this.canShowVisualStyleGlyphs;
			}
			set
			{
				if (this.canShowVisualStyleGlyphs != value)
				{
					this.canShowVisualStyleGlyphs = value;
					this.gridView.Invalidate();
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Windows.Forms.ComponentModel.Com2Interop.IComPropertyBrowser.InPropertySet" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.PropertyGrid" /> control is currently setting one of the properties of its selected object; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x0600344B RID: 13387 RVA: 0x000EC5EC File Offset: 0x000EA7EC
		bool IComPropertyBrowser.InPropertySet
		{
			get
			{
				return this.GetPropertyGridView().GetInPropertySet();
			}
		}

		/// <summary>Gets or sets the color of the gridlines and borders.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is the default system color for scroll bars.</returns>
		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x0600344C RID: 13388 RVA: 0x000EC5F9 File Offset: 0x000EA7F9
		// (set) Token: 0x0600344D RID: 13389 RVA: 0x000EC604 File Offset: 0x000EA804
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridLineColorDesc")]
		[DefaultValue(typeof(Color), "InactiveBorder")]
		public Color LineColor
		{
			get
			{
				return this.lineColor;
			}
			set
			{
				if (this.lineColor != value)
				{
					this.lineColor = value;
					this.developerOverride = true;
					if (this.lineBrush != null)
					{
						this.lineBrush.Dispose();
						this.lineBrush = null;
					}
					this.gridView.Invalidate();
				}
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x0600344E RID: 13390 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x0600344F RID: 13391 RVA: 0x0001344A File Offset: 0x0001164A
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.PropertyGrid.Padding" /> property changes.</summary>
		// Token: 0x14000275 RID: 629
		// (add) Token: 0x06003450 RID: 13392 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x06003451 RID: 13393 RVA: 0x0001345C File Offset: 0x0001165C
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

		/// <summary>Gets or sets the type of sorting the <see cref="T:System.Windows.Forms.PropertyGrid" /> uses to display properties.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.PropertySort" /> values. The default is <see cref="F:System.Windows.Forms.PropertySort.Categorized" /> or <see cref="F:System.Windows.Forms.PropertySort.Alphabetical" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.PropertySort" /> values.</exception>
		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06003452 RID: 13394 RVA: 0x000EC652 File Offset: 0x000EA852
		// (set) Token: 0x06003453 RID: 13395 RVA: 0x000EC65C File Offset: 0x000EA85C
		[SRCategory("CatAppearance")]
		[DefaultValue(PropertySort.CategorizedAlphabetical)]
		[SRDescription("PropertyGridPropertySortDesc")]
		public PropertySort PropertySort
		{
			get
			{
				return this.propertySortValue;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(PropertySort));
				}
				ToolStripButton toolStripButton;
				if ((value & PropertySort.Categorized) != PropertySort.NoSort)
				{
					toolStripButton = this.viewSortButtons[0];
				}
				else if ((value & PropertySort.Alphabetical) != PropertySort.NoSort)
				{
					toolStripButton = this.viewSortButtons[1];
				}
				else
				{
					toolStripButton = this.viewSortButtons[2];
				}
				GridItem selectedGridItem = this.SelectedGridItem;
				this.OnViewSortButtonClick(toolStripButton, EventArgs.Empty);
				this.propertySortValue = value;
				if (selectedGridItem != null)
				{
					try
					{
						this.SelectedGridItem = selectedGridItem;
					}
					catch (ArgumentException)
					{
					}
				}
			}
		}

		/// <summary>Gets the collection of property tabs that are displayed in the grid.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.PropertyGrid.PropertyTabCollection" /> containing the collection of <see cref="T:System.Windows.Forms.Design.PropertyTab" /> objects being displayed by the <see cref="T:System.Windows.Forms.PropertyGrid" />.</returns>
		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x000EC6F4 File Offset: 0x000EA8F4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PropertyGrid.PropertyTabCollection PropertyTabs
		{
			get
			{
				return new PropertyGrid.PropertyTabCollection(this);
			}
		}

		/// <summary>Gets or sets the object for which the grid displays properties.</summary>
		/// <returns>The first object in the object list. If there is no currently selected object the return is <see langword="null" />.</returns>
		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06003455 RID: 13397 RVA: 0x000EC6FC File Offset: 0x000EA8FC
		// (set) Token: 0x06003456 RID: 13398 RVA: 0x000EC719 File Offset: 0x000EA919
		[DefaultValue(null)]
		[SRDescription("PropertyGridSelectedObjectDesc")]
		[SRCategory("CatBehavior")]
		[TypeConverter(typeof(PropertyGrid.SelectedObjectConverter))]
		public object SelectedObject
		{
			get
			{
				if (this.currentObjects == null || this.currentObjects.Length == 0)
				{
					return null;
				}
				return this.currentObjects[0];
			}
			set
			{
				if (value == null)
				{
					this.SelectedObjects = new object[0];
					return;
				}
				this.SelectedObjects = new object[] { value };
			}
		}

		/// <summary>Gets or sets the currently selected objects.</summary>
		/// <returns>An array of type <see cref="T:System.Object" />. The default is an empty array.</returns>
		/// <exception cref="T:System.ArgumentException">One of the items in the array of objects had a null value.</exception>
		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x000ECC80 File Offset: 0x000EAE80
		// (set) Token: 0x06003457 RID: 13399 RVA: 0x000EC73C File Offset: 0x000EA93C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object[] SelectedObjects
		{
			get
			{
				if (this.currentObjects == null)
				{
					return new object[0];
				}
				return (object[])this.currentObjects.Clone();
			}
			set
			{
				try
				{
					this.FreezePainting = true;
					this.SetFlag(128, false);
					if (this.GetFlag(16))
					{
						this.SetFlag(256, false);
					}
					this.gridView.EnsurePendingChangesCommitted();
					bool flag = false;
					bool flag2 = false;
					bool flag3 = true;
					if (value != null && value.Length != 0)
					{
						for (int i = 0; i < value.Length; i++)
						{
							if (value[i] == null)
							{
								throw new ArgumentException(SR.GetString("PropertyGridSetNull", new object[]
								{
									i.ToString(CultureInfo.CurrentCulture),
									value.Length.ToString(CultureInfo.CurrentCulture)
								}));
							}
							if (value[i] is PropertyGrid.IUnimplemented)
							{
								throw new NotSupportedException(SR.GetString("PropertyGridRemotedObject", new object[] { value[i].GetType().FullName }));
							}
						}
					}
					else
					{
						flag3 = false;
					}
					if (this.currentObjects != null && value != null && this.currentObjects.Length == value.Length)
					{
						flag = true;
						flag2 = true;
						int num = 0;
						while (num < value.Length && (flag || flag2))
						{
							if (flag && this.currentObjects[num] != value[num])
							{
								flag = false;
							}
							Type type = this.GetUnwrappedObject(num).GetType();
							object obj = value[num];
							if (obj is ICustomTypeDescriptor)
							{
								obj = ((ICustomTypeDescriptor)obj).GetPropertyOwner(null);
							}
							Type type2 = obj.GetType();
							if (flag2 && (type != type2 || (type.IsCOMObject && type2.IsCOMObject)))
							{
								flag2 = false;
							}
							num++;
						}
					}
					if (!flag)
					{
						this.EnsureDesignerEventService();
						flag3 = flag3 && this.GetFlag(2);
						this.SetStatusBox("", "");
						this.ClearCachedProps();
						this.peDefault = null;
						if (value == null)
						{
							this.currentObjects = new object[0];
						}
						else
						{
							this.currentObjects = (object[])value.Clone();
						}
						this.SinkPropertyNotifyEvents();
						this.SetFlag(1, true);
						if (this.gridView != null)
						{
							try
							{
								this.gridView.RemoveSelectedEntryHelpAttributes();
							}
							catch (COMException)
							{
							}
						}
						if (this.peMain != null)
						{
							this.peMain.Dispose();
						}
						if (!flag2 && !this.GetFlag(8) && this.selectedViewTab < this.viewTabButtons.Length)
						{
							Type type3 = ((this.selectedViewTab == -1) ? null : this.viewTabs[this.selectedViewTab].GetType());
							ToolStripButton toolStripButton = null;
							this.RefreshTabs(PropertyTabScope.Component);
							this.EnableTabs();
							if (type3 != null)
							{
								for (int j = 0; j < this.viewTabs.Length; j++)
								{
									if (this.viewTabs[j].GetType() == type3 && this.viewTabButtons[j].Visible)
									{
										toolStripButton = this.viewTabButtons[j];
										break;
									}
								}
							}
							this.SelectViewTabButtonDefault(toolStripButton);
						}
						if (flag3 && this.viewTabs != null && this.viewTabs.Length > 1 && this.viewTabs[1] is EventsTab)
						{
							flag3 = this.viewTabButtons[1].Visible;
							Attribute[] array = new Attribute[this.BrowsableAttributes.Count];
							this.BrowsableAttributes.CopyTo(array, 0);
							Hashtable hashtable = null;
							if (this.currentObjects.Length > 10)
							{
								hashtable = new Hashtable();
							}
							int num2 = 0;
							while (num2 < this.currentObjects.Length && flag3)
							{
								object obj2 = this.currentObjects[num2];
								if (obj2 is ICustomTypeDescriptor)
								{
									obj2 = ((ICustomTypeDescriptor)obj2).GetPropertyOwner(null);
								}
								Type type4 = obj2.GetType();
								if (hashtable == null || !hashtable.Contains(type4))
								{
									flag3 = flag3 && obj2 is IComponent && ((IComponent)obj2).Site != null;
									PropertyDescriptorCollection properties = ((EventsTab)this.viewTabs[1]).GetProperties(obj2, array);
									flag3 = flag3 && properties != null && properties.Count > 0;
									if (flag3 && hashtable != null)
									{
										hashtable[type4] = type4;
									}
								}
								num2++;
							}
						}
						this.ShowEventsButton(flag3 && this.currentObjects.Length != 0);
						this.DisplayHotCommands();
						if (this.currentObjects.Length == 1)
						{
							this.EnablePropPageButton(this.currentObjects[0]);
						}
						else
						{
							this.EnablePropPageButton(null);
						}
						this.OnSelectedObjectsChanged(EventArgs.Empty);
					}
					if (!this.GetFlag(8))
					{
						if (this.currentObjects.Length != 0 && this.GetFlag(32))
						{
							object activeDesigner = this.ActiveDesigner;
							if (activeDesigner != null && this.designerSelections != null && this.designerSelections.ContainsKey(activeDesigner.GetHashCode()))
							{
								int num3 = (int)this.designerSelections[activeDesigner.GetHashCode()];
								if (num3 < this.viewTabs.Length && (num3 == 0 || this.viewTabButtons[num3].Visible))
								{
									this.SelectViewTabButton(this.viewTabButtons[num3], true);
								}
							}
							else
							{
								this.Refresh(false);
							}
							this.SetFlag(32, false);
						}
						else
						{
							this.Refresh(true);
						}
						if (this.currentObjects.Length != 0)
						{
							this.SaveTabSelection();
						}
					}
				}
				finally
				{
					this.FreezePainting = false;
				}
			}
		}

		/// <summary>Gets the currently selected property tab.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Design.PropertyTab" /> that is providing the selected view.</returns>
		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06003459 RID: 13401 RVA: 0x000ECCA1 File Offset: 0x000EAEA1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PropertyTab SelectedTab
		{
			get
			{
				return this.viewTabs[this.selectedViewTab];
			}
		}

		/// <summary>Gets or sets the selected grid item.</summary>
		/// <returns>The currently selected row in the property grid.</returns>
		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x000ECCB0 File Offset: 0x000EAEB0
		// (set) Token: 0x0600345B RID: 13403 RVA: 0x000ECCD4 File Offset: 0x000EAED4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GridItem SelectedGridItem
		{
			get
			{
				GridItem selectedGridEntry = this.gridView.SelectedGridEntry;
				if (selectedGridEntry == null)
				{
					return this.peMain;
				}
				return selectedGridEntry;
			}
			set
			{
				this.gridView.SelectedGridEntry = (GridEntry)value;
			}
		}

		/// <summary>Gets a value indicating whether the control should display focus rectangles.</summary>
		/// <returns>
		///   <see langword="true" /> if the control should display focus rectangles; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x00012E4E File Offset: 0x0001104E
		protected internal override bool ShowFocusCues
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the site of the control.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the Control, if any.</returns>
		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x0600345D RID: 13405 RVA: 0x00048990 File Offset: 0x00046B90
		// (set) Token: 0x0600345E RID: 13406 RVA: 0x000ECCE8 File Offset: 0x000EAEE8
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.SuspendAllLayout(this);
				base.Site = value;
				this.gridView.ServiceProvider = value;
				if (value == null)
				{
					this.ActiveDesigner = null;
				}
				else
				{
					this.ActiveDesigner = (IDesignerHost)value.GetService(typeof(IDesignerHost));
				}
				base.ResumeAllLayout(this, true);
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x0600345F RID: 13407 RVA: 0x000ECD3E File Offset: 0x000EAF3E
		internal bool SortedByCategories
		{
			get
			{
				return (this.PropertySort & PropertySort.Categorized) > PropertySort.NoSort;
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06003460 RID: 13408 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x06003461 RID: 13409 RVA: 0x00023FE9 File Offset: 0x000221E9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>Occurs when the text of the <see cref="T:System.Windows.Forms.PropertyGrid" /> changes.</summary>
		// Token: 0x14000276 RID: 630
		// (add) Token: 0x06003462 RID: 13410 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06003463 RID: 13411 RVA: 0x0004659A File Offset: 0x0004479A
		[Browsable(false)]
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

		/// <summary>Gets or sets a value indicating whether buttons appear in standard size or in large size.</summary>
		/// <returns>
		///   <see langword="true" /> if buttons on the control appear large; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06003464 RID: 13412 RVA: 0x000ECD4B File Offset: 0x000EAF4B
		// (set) Token: 0x06003465 RID: 13413 RVA: 0x000ECD58 File Offset: 0x000EAF58
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridLargeButtonsDesc")]
		[DefaultValue(false)]
		public bool LargeButtons
		{
			get
			{
				return this.buttonType == 1;
			}
			set
			{
				if (value == (this.buttonType == 1))
				{
					return;
				}
				this.buttonType = (value ? 1 : 0);
				if (value)
				{
					this.EnsureLargeButtons();
					if (this.imageList != null && this.imageList[1] != null)
					{
						this.toolStrip.ImageScalingSize = this.imageList[1].ImageSize;
					}
				}
				else if (this.imageList != null && this.imageList[0] != null)
				{
					this.toolStrip.ImageScalingSize = this.imageList[0].ImageSize;
				}
				this.toolStrip.ImageList = this.imageList[this.buttonType];
				this.OnLayoutInternal(false);
				base.Invalidate();
				this.toolStrip.Invalidate();
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06003466 RID: 13414 RVA: 0x000ECE0E File Offset: 0x000EB00E
		internal AccessibleObject ToolbarAccessibleObject
		{
			get
			{
				return this.toolStrip.AccessibilityObject;
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the toolbar is visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06003467 RID: 13415 RVA: 0x000ECE1B File Offset: 0x000EB01B
		// (set) Token: 0x06003468 RID: 13416 RVA: 0x000ECE23 File Offset: 0x000EB023
		[SRCategory("CatAppearance")]
		[DefaultValue(true)]
		[SRDescription("PropertyGridToolbarVisibleDesc")]
		public virtual bool ToolbarVisible
		{
			get
			{
				return this.toolbarVisible;
			}
			set
			{
				this.toolbarVisible = value;
				this.toolStrip.Visible = value;
				this.OnLayoutInternal(false);
				if (value)
				{
					this.SetupToolbar(this.viewTabsDirty);
				}
				base.Invalidate();
				this.toolStrip.Invalidate();
			}
		}

		/// <summary>Gets or sets the painting functionality for <see cref="T:System.Windows.Forms.ToolStrip" /> objects.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripRenderer" /> for the <see cref="T:System.Windows.Forms.PropertyGrid" />.</returns>
		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06003469 RID: 13417 RVA: 0x000ECE5F File Offset: 0x000EB05F
		// (set) Token: 0x0600346A RID: 13418 RVA: 0x000ECE76 File Offset: 0x000EB076
		protected ToolStripRenderer ToolStripRenderer
		{
			get
			{
				if (this.toolStrip != null)
				{
					return this.toolStrip.Renderer;
				}
				return null;
			}
			set
			{
				if (this.toolStrip != null)
				{
					this.toolStrip.Renderer = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating the background color in the grid.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is the default system color for windows.</returns>
		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x000ECE8C File Offset: 0x000EB08C
		// (set) Token: 0x0600346C RID: 13420 RVA: 0x000ECE99 File Offset: 0x000EB099
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridViewBackColorDesc")]
		[DefaultValue(typeof(Color), "Window")]
		public Color ViewBackColor
		{
			get
			{
				return this.gridView.BackColor;
			}
			set
			{
				this.gridView.BackColor = value;
				this.gridView.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating the color of the text in the grid.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values. The default is current system color for text in windows.</returns>
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000ECEB2 File Offset: 0x000EB0B2
		// (set) Token: 0x0600346E RID: 13422 RVA: 0x000ECEBF File Offset: 0x000EB0BF
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridViewForeColorDesc")]
		[DefaultValue(typeof(Color), "WindowText")]
		public Color ViewForeColor
		{
			get
			{
				return this.gridView.ForeColor;
			}
			set
			{
				this.gridView.ForeColor = value;
				this.gridView.Invalidate();
			}
		}

		/// <summary>Gets or sets the color of the border surrounding the grid area.</summary>
		/// <returns>The color of the property grid border.</returns>
		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000ECED8 File Offset: 0x000EB0D8
		// (set) Token: 0x06003470 RID: 13424 RVA: 0x000ECEE0 File Offset: 0x000EB0E0
		[SRCategory("CatAppearance")]
		[SRDescription("PropertyGridViewBorderColorDesc")]
		[DefaultValue(typeof(Color), "ControlDark")]
		public Color ViewBorderColor
		{
			get
			{
				return this.viewBorderColor;
			}
			set
			{
				if (this.viewBorderColor != value)
				{
					this.viewBorderColor = value;
					this.gridView.Invalidate();
				}
			}
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000ECF04 File Offset: 0x000EB104
		private int AddImage(Bitmap image)
		{
			image.MakeTransparent();
			if (DpiHelper.IsScalingRequired && (image.Size.Width != PropertyGrid.normalButtonSize.Width || image.Size.Height != PropertyGrid.normalButtonSize.Height))
			{
				image = DpiHelper.CreateResizedBitmap(image, PropertyGrid.normalButtonSize);
			}
			int count = this.imageList[0].Images.Count;
			this.imageList[0].Images.Add(image);
			return count;
		}

		/// <summary>Occurs when a key is first pressed.</summary>
		// Token: 0x14000277 RID: 631
		// (add) Token: 0x06003472 RID: 13426 RVA: 0x000B9116 File Offset: 0x000B7316
		// (remove) Token: 0x06003473 RID: 13427 RVA: 0x000B911F File Offset: 0x000B731F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				base.KeyDown += value;
			}
			remove
			{
				base.KeyDown -= value;
			}
		}

		/// <summary>Occurs when a key is pressed while the control has focus.</summary>
		// Token: 0x14000278 RID: 632
		// (add) Token: 0x06003474 RID: 13428 RVA: 0x000B9128 File Offset: 0x000B7328
		// (remove) Token: 0x06003475 RID: 13429 RVA: 0x000B9131 File Offset: 0x000B7331
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				base.KeyPress += value;
			}
			remove
			{
				base.KeyPress -= value;
			}
		}

		/// <summary>Occurs when a key is released while the control has focus.</summary>
		// Token: 0x14000279 RID: 633
		// (add) Token: 0x06003476 RID: 13430 RVA: 0x000B9104 File Offset: 0x000B7304
		// (remove) Token: 0x06003477 RID: 13431 RVA: 0x000B910D File Offset: 0x000B730D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				base.KeyUp += value;
			}
			remove
			{
				base.KeyUp -= value;
			}
		}

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.PropertyGrid" /> control with the mouse.</summary>
		// Token: 0x1400027A RID: 634
		// (add) Token: 0x06003478 RID: 13432 RVA: 0x000B913A File Offset: 0x000B733A
		// (remove) Token: 0x06003479 RID: 13433 RVA: 0x000B9143 File Offset: 0x000B7343
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseDown
		{
			add
			{
				base.MouseDown += value;
			}
			remove
			{
				base.MouseDown -= value;
			}
		}

		/// <summary>Occurs when the mouse pointer is over the control and the user releases a mouse button.</summary>
		// Token: 0x1400027B RID: 635
		// (add) Token: 0x0600347A RID: 13434 RVA: 0x000B914C File Offset: 0x000B734C
		// (remove) Token: 0x0600347B RID: 13435 RVA: 0x000B9155 File Offset: 0x000B7355
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseUp
		{
			add
			{
				base.MouseUp += value;
			}
			remove
			{
				base.MouseUp -= value;
			}
		}

		/// <summary>Occurs when the mouse pointer moves over the control.</summary>
		// Token: 0x1400027C RID: 636
		// (add) Token: 0x0600347C RID: 13436 RVA: 0x00011A7E File Offset: 0x0000FC7E
		// (remove) Token: 0x0600347D RID: 13437 RVA: 0x00011A87 File Offset: 0x0000FC87
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				base.MouseMove += value;
			}
			remove
			{
				base.MouseMove -= value;
			}
		}

		/// <summary>Occurs when the mouse pointer enters the control.</summary>
		// Token: 0x1400027D RID: 637
		// (add) Token: 0x0600347E RID: 13438 RVA: 0x00011A48 File Offset: 0x0000FC48
		// (remove) Token: 0x0600347F RID: 13439 RVA: 0x00011A51 File Offset: 0x0000FC51
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler MouseEnter
		{
			add
			{
				base.MouseEnter += value;
			}
			remove
			{
				base.MouseEnter -= value;
			}
		}

		/// <summary>Occurs when the mouse pointer leaves the control.</summary>
		// Token: 0x1400027E RID: 638
		// (add) Token: 0x06003480 RID: 13440 RVA: 0x00011A5A File Offset: 0x0000FC5A
		// (remove) Token: 0x06003481 RID: 13441 RVA: 0x00011A63 File Offset: 0x0000FC63
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler MouseLeave
		{
			add
			{
				base.MouseLeave += value;
			}
			remove
			{
				base.MouseLeave -= value;
			}
		}

		/// <summary>Occurs when a property value changes.</summary>
		// Token: 0x1400027F RID: 639
		// (add) Token: 0x06003482 RID: 13442 RVA: 0x000ECF86 File Offset: 0x000EB186
		// (remove) Token: 0x06003483 RID: 13443 RVA: 0x000ECF99 File Offset: 0x000EB199
		[SRCategory("CatPropertyChanged")]
		[SRDescription("PropertyGridPropertyValueChangedDescr")]
		public event PropertyValueChangedEventHandler PropertyValueChanged
		{
			add
			{
				base.Events.AddHandler(PropertyGrid.EventPropertyValueChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGrid.EventPropertyValueChanged, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.PropertyGrid" /> control is browsing a COM object and the user renames the object.</summary>
		// Token: 0x14000280 RID: 640
		// (add) Token: 0x06003484 RID: 13444 RVA: 0x000ECFAC File Offset: 0x000EB1AC
		// (remove) Token: 0x06003485 RID: 13445 RVA: 0x000ECFBF File Offset: 0x000EB1BF
		event ComponentRenameEventHandler IComPropertyBrowser.ComComponentNameChanged
		{
			add
			{
				base.Events.AddHandler(PropertyGrid.EventComComponentNameChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGrid.EventComComponentNameChanged, value);
			}
		}

		/// <summary>Occurs when a property tab changes.</summary>
		// Token: 0x14000281 RID: 641
		// (add) Token: 0x06003486 RID: 13446 RVA: 0x000ECFD2 File Offset: 0x000EB1D2
		// (remove) Token: 0x06003487 RID: 13447 RVA: 0x000ECFE5 File Offset: 0x000EB1E5
		[SRCategory("CatPropertyChanged")]
		[SRDescription("PropertyGridPropertyTabchangedDescr")]
		public event PropertyTabChangedEventHandler PropertyTabChanged
		{
			add
			{
				base.Events.AddHandler(PropertyGrid.EventPropertyTabChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGrid.EventPropertyTabChanged, value);
			}
		}

		/// <summary>Occurs when the sort mode is changed.</summary>
		// Token: 0x14000282 RID: 642
		// (add) Token: 0x06003488 RID: 13448 RVA: 0x000ECFF8 File Offset: 0x000EB1F8
		// (remove) Token: 0x06003489 RID: 13449 RVA: 0x000ED00B File Offset: 0x000EB20B
		[SRCategory("CatPropertyChanged")]
		[SRDescription("PropertyGridPropertySortChangedDescr")]
		public event EventHandler PropertySortChanged
		{
			add
			{
				base.Events.AddHandler(PropertyGrid.EventPropertySortChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGrid.EventPropertySortChanged, value);
			}
		}

		/// <summary>Occurs when the selected <see cref="T:System.Windows.Forms.GridItem" /> is changed.</summary>
		// Token: 0x14000283 RID: 643
		// (add) Token: 0x0600348A RID: 13450 RVA: 0x000ED01E File Offset: 0x000EB21E
		// (remove) Token: 0x0600348B RID: 13451 RVA: 0x000ED031 File Offset: 0x000EB231
		[SRCategory("CatPropertyChanged")]
		[SRDescription("PropertyGridSelectedGridItemChangedDescr")]
		public event SelectedGridItemChangedEventHandler SelectedGridItemChanged
		{
			add
			{
				base.Events.AddHandler(PropertyGrid.EventSelectedGridItemChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGrid.EventSelectedGridItemChanged, value);
			}
		}

		/// <summary>Occurs when the objects selected by the <see cref="P:System.Windows.Forms.PropertyGrid.SelectedObjects" /> property have changed.</summary>
		// Token: 0x14000284 RID: 644
		// (add) Token: 0x0600348C RID: 13452 RVA: 0x000ED044 File Offset: 0x000EB244
		// (remove) Token: 0x0600348D RID: 13453 RVA: 0x000ED057 File Offset: 0x000EB257
		[SRCategory("CatPropertyChanged")]
		[SRDescription("PropertyGridSelectedObjectsChangedDescr")]
		public event EventHandler SelectedObjectsChanged
		{
			add
			{
				base.Events.AddHandler(PropertyGrid.EventSelectedObjectsChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyGrid.EventSelectedObjectsChanged, value);
			}
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000ED06A File Offset: 0x000EB26A
		internal void AddTab(Type tabType, PropertyTabScope scope)
		{
			this.AddRefTab(tabType, null, scope, true);
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000ED078 File Offset: 0x000EB278
		internal void AddRefTab(Type tabType, object component, PropertyTabScope type, bool setupToolbar)
		{
			PropertyTab propertyTab = null;
			int num = -1;
			if (this.viewTabs != null)
			{
				for (int i = 0; i < this.viewTabs.Length; i++)
				{
					if (tabType == this.viewTabs[i].GetType())
					{
						propertyTab = this.viewTabs[i];
						num = i;
						break;
					}
				}
			}
			else
			{
				num = 0;
			}
			if (propertyTab == null)
			{
				IDesignerHost designerHost = null;
				if (component != null && component is IComponent && ((IComponent)component).Site != null)
				{
					designerHost = (IDesignerHost)((IComponent)component).Site.GetService(typeof(IDesignerHost));
				}
				try
				{
					propertyTab = this.CreateTab(tabType, designerHost);
				}
				catch (Exception ex)
				{
					return;
				}
				if (this.viewTabs != null)
				{
					num = this.viewTabs.Length;
					if (tabType == this.DefaultTabType)
					{
						num = 0;
					}
					else if (typeof(EventsTab).IsAssignableFrom(tabType))
					{
						num = 1;
					}
					else
					{
						for (int j = 1; j < this.viewTabs.Length; j++)
						{
							if (!(this.viewTabs[j] is EventsTab) && string.Compare(propertyTab.TabName, this.viewTabs[j].TabName, false, CultureInfo.InvariantCulture) < 0)
							{
								num = j;
								break;
							}
						}
					}
				}
				PropertyTab[] array = new PropertyTab[this.viewTabs.Length + 1];
				Array.Copy(this.viewTabs, 0, array, 0, num);
				Array.Copy(this.viewTabs, num, array, num + 1, this.viewTabs.Length - num);
				array[num] = propertyTab;
				this.viewTabs = array;
				this.viewTabsDirty = true;
				PropertyTabScope[] array2 = new PropertyTabScope[this.viewTabScopes.Length + 1];
				Array.Copy(this.viewTabScopes, 0, array2, 0, num);
				Array.Copy(this.viewTabScopes, num, array2, num + 1, this.viewTabScopes.Length - num);
				array2[num] = type;
				this.viewTabScopes = array2;
			}
			if (propertyTab != null && component != null)
			{
				try
				{
					object[] components = propertyTab.Components;
					int num2 = ((components == null) ? 0 : components.Length);
					object[] array3 = new object[num2 + 1];
					if (num2 > 0)
					{
						Array.Copy(components, array3, num2);
					}
					array3[num2] = component;
					propertyTab.Components = array3;
				}
				catch (Exception ex2)
				{
					this.RemoveTab(num, false);
				}
			}
			if (setupToolbar)
			{
				this.SetupToolbar();
				this.ShowEventsButton(false);
			}
		}

		/// <summary>Collapses all the categories in the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
		// Token: 0x06003490 RID: 13456 RVA: 0x000ED2BC File Offset: 0x000EB4BC
		public void CollapseAllGridItems()
		{
			this.gridView.RecursivelyExpand(this.peMain, false, false, -1);
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000ED2D2 File Offset: 0x000EB4D2
		private void ClearCachedProps()
		{
			if (this.viewTabProps != null)
			{
				this.viewTabProps.Clear();
			}
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000ED2E7 File Offset: 0x000EB4E7
		internal void ClearValueCaches()
		{
			if (this.peMain != null)
			{
				this.peMain.ClearCachedValues();
			}
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x000ED2FC File Offset: 0x000EB4FC
		internal void ClearTabs(PropertyTabScope tabScope)
		{
			if (tabScope < PropertyTabScope.Document)
			{
				throw new ArgumentException(SR.GetString("PropertyGridTabScope"));
			}
			this.RemoveTabs(tabScope, true);
		}

		/// <summary>Creates a new accessibility object for this control.</summary>
		/// <returns>An accessibiity object for this control.</returns>
		// Token: 0x06003494 RID: 13460 RVA: 0x000ED31A File Offset: 0x000EB51A
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new PropertyGridAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x000ED330 File Offset: 0x000EB530
		private PropertyGridView CreateGridView(IServiceProvider sp)
		{
			return new PropertyGridView(sp, this);
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x000ED33C File Offset: 0x000EB53C
		private ToolStripSeparator CreateSeparatorButton()
		{
			ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
			if (DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
			{
				toolStripSeparator.DeviceDpi = base.DeviceDpi;
			}
			return toolStripSeparator;
		}

		/// <summary>When overridden in a derived class, enables the creation of a <see cref="T:System.Windows.Forms.Design.PropertyTab" />.</summary>
		/// <param name="tabType">The type of tab to create.</param>
		/// <returns>The newly created property tab. Returns <see langword="null" /> in its default implementation.</returns>
		// Token: 0x06003497 RID: 13463 RVA: 0x00015C90 File Offset: 0x00013E90
		protected virtual PropertyTab CreatePropertyTab(Type tabType)
		{
			return null;
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x000ED364 File Offset: 0x000EB564
		private PropertyTab CreateTab(Type tabType, IDesignerHost host)
		{
			PropertyTab propertyTab = this.CreatePropertyTab(tabType);
			if (propertyTab == null)
			{
				ConstructorInfo constructorInfo = tabType.GetConstructor(new Type[] { typeof(IServiceProvider) });
				object obj = null;
				if (constructorInfo == null)
				{
					constructorInfo = tabType.GetConstructor(new Type[] { typeof(IDesignerHost) });
					if (constructorInfo != null)
					{
						obj = host;
					}
				}
				else
				{
					obj = this.Site;
				}
				if (obj != null && constructorInfo != null)
				{
					propertyTab = (PropertyTab)constructorInfo.Invoke(new object[] { obj });
				}
				else
				{
					propertyTab = (PropertyTab)Activator.CreateInstance(tabType);
				}
			}
			if (propertyTab != null)
			{
				Bitmap bitmap = propertyTab.Bitmap;
				if (bitmap == null)
				{
					throw new ArgumentException(SR.GetString("PropertyGridNoBitmap", new object[] { propertyTab.GetType().FullName }));
				}
				Size size = bitmap.Size;
				if (size.Width != 16 || size.Height != 16)
				{
					bitmap = new Bitmap(bitmap, new Size(16, 16));
				}
				string tabName = propertyTab.TabName;
				if (tabName == null || tabName.Length == 0)
				{
					throw new ArgumentException(SR.GetString("PropertyGridTabName", new object[] { propertyTab.GetType().FullName }));
				}
			}
			return propertyTab;
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x000ED4A0 File Offset: 0x000EB6A0
		private ToolStripButton CreatePushButton(string toolTipText, int imageIndex, EventHandler eventHandler, bool useCheckButtonRole = false)
		{
			ToolStripButton toolStripButton = new ToolStripButton();
			if (DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
			{
				toolStripButton.DeviceDpi = base.DeviceDpi;
			}
			toolStripButton.Text = toolTipText;
			toolStripButton.AutoToolTip = true;
			toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
			toolStripButton.ImageIndex = imageIndex;
			toolStripButton.Click += eventHandler;
			toolStripButton.ImageScaling = ToolStripItemImageScaling.SizeToFit;
			if (AccessibilityImprovements.Level1 && useCheckButtonRole)
			{
				toolStripButton.AccessibleRole = AccessibleRole.CheckButton;
			}
			return toolStripButton;
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000ED504 File Offset: 0x000EB704
		internal void DumpPropsToConsole()
		{
			this.gridView.DumpPropsToConsole(this.peMain, "");
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000ED51C File Offset: 0x000EB71C
		private void DisplayHotCommands()
		{
			bool visible = this.hotcommands.Visible;
			IComponent component = null;
			DesignerVerb[] array = null;
			if (this.currentObjects != null && this.currentObjects.Length != 0)
			{
				for (int i = 0; i < this.currentObjects.Length; i++)
				{
					object unwrappedObject = this.GetUnwrappedObject(i);
					if (unwrappedObject is IComponent)
					{
						component = (IComponent)unwrappedObject;
						break;
					}
				}
				if (component != null)
				{
					ISite site = component.Site;
					if (site != null)
					{
						IMenuCommandService menuCommandService = (IMenuCommandService)site.GetService(typeof(IMenuCommandService));
						if (menuCommandService != null)
						{
							array = new DesignerVerb[menuCommandService.Verbs.Count];
							menuCommandService.Verbs.CopyTo(array, 0);
						}
						else if (this.currentObjects.Length == 1 && this.GetUnwrappedObject(0) is IComponent)
						{
							IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
							if (designerHost != null)
							{
								IDesigner designer = designerHost.GetDesigner(component);
								if (designer != null)
								{
									array = new DesignerVerb[designer.Verbs.Count];
									designer.Verbs.CopyTo(array, 0);
								}
							}
						}
					}
				}
			}
			if (!base.DesignMode)
			{
				if (array != null && array.Length != 0)
				{
					this.hotcommands.SetVerbs(component, array);
				}
				else
				{
					this.hotcommands.SetVerbs(null, null);
				}
				if (visible != this.hotcommands.Visible)
				{
					this.OnLayoutInternal(false);
				}
			}
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600349C RID: 13468 RVA: 0x000ED678 File Offset: 0x000EB878
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.GetFlag(2))
				{
					if (this.designerEventService != null)
					{
						this.designerEventService.ActiveDesignerChanged -= this.OnActiveDesignerChanged;
					}
					this.designerEventService = null;
					this.SetFlag(2, false);
				}
				this.ActiveDesigner = null;
				if (this.viewTabs != null)
				{
					for (int i = 0; i < this.viewTabs.Length; i++)
					{
						this.viewTabs[i].Dispose();
					}
					this.viewTabs = null;
				}
				if (this.imageList != null)
				{
					for (int j = 0; j < this.imageList.Length; j++)
					{
						if (this.imageList[j] != null)
						{
							this.imageList[j].Dispose();
						}
					}
					this.imageList = null;
				}
				if (this.bmpAlpha != null)
				{
					this.bmpAlpha.Dispose();
					this.bmpAlpha = null;
				}
				if (this.bmpCategory != null)
				{
					this.bmpCategory.Dispose();
					this.bmpCategory = null;
				}
				if (this.bmpPropPage != null)
				{
					this.bmpPropPage.Dispose();
					this.bmpPropPage = null;
				}
				if (this.lineBrush != null)
				{
					this.lineBrush.Dispose();
					this.lineBrush = null;
				}
				if (this.peMain != null)
				{
					this.peMain.Dispose();
					this.peMain = null;
				}
				if (this.currentObjects != null)
				{
					this.currentObjects = null;
					this.SinkPropertyNotifyEvents();
				}
				this.ClearCachedProps();
				this.currentPropEntries = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000ED7DC File Offset: 0x000EB9DC
		private void DividerDraw(int y)
		{
			if (y == -1)
			{
				return;
			}
			Rectangle bounds = this.gridView.Bounds;
			bounds.Y = y - PropertyGrid.cyDivider;
			bounds.Height = PropertyGrid.cyDivider;
			PropertyGrid.DrawXorBar(this, bounds);
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000ED81C File Offset: 0x000EBA1C
		private PropertyGrid.SnappableControl DividerInside(int x, int y)
		{
			int num = -1;
			if (this.hotcommands.Visible)
			{
				Point location = this.hotcommands.Location;
				if (y >= location.Y - PropertyGrid.cyDivider && y <= location.Y + 1)
				{
					return this.hotcommands;
				}
				num = 0;
			}
			if (this.doccomment.Visible)
			{
				Point location2 = this.doccomment.Location;
				if (y >= location2.Y - PropertyGrid.cyDivider && y <= location2.Y + 1)
				{
					return this.doccomment;
				}
				if (num == -1)
				{
					num = 1;
				}
			}
			if (num != -1)
			{
				int y2 = this.gridView.Location.Y;
				int num2 = y2 + this.gridView.Size.Height;
				if (Math.Abs(num2 - y) <= 1 && y > y2)
				{
					if (num == 0)
					{
						return this.hotcommands;
					}
					if (num == 1)
					{
						return this.doccomment;
					}
				}
			}
			return null;
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000ED904 File Offset: 0x000EBB04
		private int DividerLimitHigh(PropertyGrid.SnappableControl target)
		{
			int num = this.gridView.Location.Y + 20;
			if (target == this.doccomment && this.hotcommands.Visible)
			{
				num += this.hotcommands.Size.Height + 2;
			}
			return num;
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000ED958 File Offset: 0x000EBB58
		private int DividerLimitMove(PropertyGrid.SnappableControl target, int y)
		{
			Rectangle bounds = target.Bounds;
			int num = Math.Min(bounds.Y + bounds.Height - 15, y);
			return Math.Max(this.DividerLimitHigh(target), num);
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000ED998 File Offset: 0x000EBB98
		private static void DrawXorBar(Control ctlDrawTo, Rectangle rcFrame)
		{
			Rectangle rectangle = ctlDrawTo.RectangleToScreen(rcFrame);
			if (rectangle.Width < rectangle.Height)
			{
				for (int i = 0; i < rectangle.Width; i++)
				{
					ControlPaint.DrawReversibleLine(new Point(rectangle.X + i, rectangle.Y), new Point(rectangle.X + i, rectangle.Y + rectangle.Height), ctlDrawTo.BackColor);
				}
				return;
			}
			for (int j = 0; j < rectangle.Height; j++)
			{
				ControlPaint.DrawReversibleLine(new Point(rectangle.X, rectangle.Y + j), new Point(rectangle.X + rectangle.Width, rectangle.Y + j), ctlDrawTo.BackColor);
			}
		}

		/// <summary>Closes any open drop-down controls on the <see cref="T:System.Windows.Forms.PropertyGrid" /> control. For a description of this member, see <see cref="M:System.Windows.Forms.ComponentModel.Com2Interop.IComPropertyBrowser.DropDownDone" />.</summary>
		// Token: 0x060034A2 RID: 13474 RVA: 0x000EDA5C File Offset: 0x000EBC5C
		void IComPropertyBrowser.DropDownDone()
		{
			this.GetPropertyGridView().DropDownDone();
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000EDA6C File Offset: 0x000EBC6C
		private bool EnablePropPageButton(object obj)
		{
			if (obj == null)
			{
				this.btnViewPropertyPages.Enabled = false;
				return false;
			}
			IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
			bool flag;
			if (iuiservice != null)
			{
				flag = iuiservice.CanShowComponentEditor(obj);
			}
			else
			{
				flag = TypeDescriptor.GetEditor(obj, typeof(ComponentEditor)) != null;
			}
			this.btnViewPropertyPages.Enabled = flag;
			return flag;
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000EDAD0 File Offset: 0x000EBCD0
		private void EnableTabs()
		{
			if (this.currentObjects != null)
			{
				this.SetupToolbar();
				for (int i = 1; i < this.viewTabs.Length; i++)
				{
					bool flag = true;
					for (int j = 0; j < this.currentObjects.Length; j++)
					{
						try
						{
							if (!this.viewTabs[i].CanExtend(this.GetUnwrappedObject(j)))
							{
								flag = false;
								break;
							}
						}
						catch (Exception ex)
						{
							flag = false;
							break;
						}
					}
					if (flag != this.viewTabButtons[i].Visible)
					{
						this.viewTabButtons[i].Visible = flag;
						if (!flag && i == this.selectedViewTab)
						{
							this.SelectViewTabButton(this.viewTabButtons[0], true);
						}
					}
				}
			}
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000EDB84 File Offset: 0x000EBD84
		private void EnsureDesignerEventService()
		{
			if (this.GetFlag(2))
			{
				return;
			}
			this.designerEventService = (IDesignerEventService)this.GetService(typeof(IDesignerEventService));
			if (this.designerEventService != null)
			{
				this.SetFlag(2, true);
				this.designerEventService.ActiveDesignerChanged += this.OnActiveDesignerChanged;
				this.OnActiveDesignerChanged(null, new ActiveDesignerEventArgs(null, this.designerEventService.ActiveDesigner));
			}
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000EDBF8 File Offset: 0x000EBDF8
		private void EnsureLargeButtons()
		{
			if (this.imageList[1] == null)
			{
				this.imageList[1] = new ImageList();
				this.imageList[1].ImageSize = PropertyGrid.largeButtonSize;
				if (DpiHelper.IsScalingRequired)
				{
					this.AddLargeImage(this.bmpAlpha);
					this.AddLargeImage(this.bmpCategory);
					foreach (PropertyTab propertyTab in this.viewTabs)
					{
						this.AddLargeImage(propertyTab.Bitmap);
					}
					this.AddLargeImage(this.bmpPropPage);
					return;
				}
				ImageList.ImageCollection images = this.imageList[0].Images;
				for (int j = 0; j < images.Count; j++)
				{
					if (images[j] is Bitmap)
					{
						this.imageList[1].Images.Add(new Bitmap((Bitmap)images[j], PropertyGrid.largeButtonSize.Width, PropertyGrid.largeButtonSize.Height));
					}
				}
			}
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000EDCEC File Offset: 0x000EBEEC
		private void AddLargeImage(Bitmap originalBitmap)
		{
			if (originalBitmap == null)
			{
				return;
			}
			try
			{
				Bitmap bitmap = new Bitmap(originalBitmap);
				bitmap.MakeTransparent();
				Bitmap bitmap2 = DpiHelper.CreateResizedBitmap(bitmap, PropertyGrid.largeButtonSize);
				bitmap.Dispose();
				this.imageList[1].Images.Add(bitmap2);
			}
			catch (Exception ex)
			{
			}
		}

		/// <summary>Commits all pending changes to the <see cref="T:System.Windows.Forms.PropertyGrid" /> control. For a description of this member, see <see cref="M:System.Windows.Forms.ComponentModel.Com2Interop.IComPropertyBrowser.EnsurePendingChangesCommitted" />.</summary>
		/// <returns>
		///   <see langword="true" /> if all the <see cref="T:System.Windows.Forms.PropertyGrid" /> successfully commits changes; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034A8 RID: 13480 RVA: 0x000EDD48 File Offset: 0x000EBF48
		bool IComPropertyBrowser.EnsurePendingChangesCommitted()
		{
			bool flag;
			try
			{
				if (this.designerHost != null)
				{
					this.designerHost.TransactionOpened -= this.OnTransactionOpened;
					this.designerHost.TransactionClosed -= this.OnTransactionClosed;
				}
				flag = this.GetPropertyGridView().EnsurePendingChangesCommitted();
			}
			finally
			{
				if (this.designerHost != null)
				{
					this.designerHost.TransactionOpened += this.OnTransactionOpened;
					this.designerHost.TransactionClosed += this.OnTransactionClosed;
				}
			}
			return flag;
		}

		/// <summary>Expands all the categories in the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
		// Token: 0x060034A9 RID: 13481 RVA: 0x000EDDE4 File Offset: 0x000EBFE4
		public void ExpandAllGridItems()
		{
			this.gridView.RecursivelyExpand(this.peMain, false, true, 10);
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000EDDFC File Offset: 0x000EBFFC
		private static Type[] GetCommonTabs(object[] objs, PropertyTabScope tabScope)
		{
			if (objs == null || objs.Length == 0)
			{
				return new Type[0];
			}
			Type[] array = new Type[5];
			int num = 0;
			PropertyTabAttribute propertyTabAttribute = (PropertyTabAttribute)TypeDescriptor.GetAttributes(objs[0])[typeof(PropertyTabAttribute)];
			if (propertyTabAttribute == null)
			{
				return new Type[0];
			}
			int i;
			for (i = 0; i < propertyTabAttribute.TabScopes.Length; i++)
			{
				PropertyTabScope propertyTabScope = propertyTabAttribute.TabScopes[i];
				if (propertyTabScope == tabScope)
				{
					if (num == array.Length)
					{
						Type[] array2 = new Type[num * 2];
						Array.Copy(array, 0, array2, 0, num);
						array = array2;
					}
					array[num++] = propertyTabAttribute.TabClasses[i];
				}
			}
			if (num == 0)
			{
				return new Type[0];
			}
			i = 1;
			while (i < objs.Length && num > 0)
			{
				propertyTabAttribute = (PropertyTabAttribute)TypeDescriptor.GetAttributes(objs[i])[typeof(PropertyTabAttribute)];
				if (propertyTabAttribute == null)
				{
					return new Type[0];
				}
				for (int j = 0; j < num; j++)
				{
					bool flag = false;
					for (int k = 0; k < propertyTabAttribute.TabClasses.Length; k++)
					{
						if (propertyTabAttribute.TabClasses[k] == array[j])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						array[j] = array[num - 1];
						array[num - 1] = null;
						num--;
						j--;
					}
				}
				i++;
			}
			Type[] array3 = new Type[num];
			if (num > 0)
			{
				Array.Copy(array, 0, array3, 0, num);
			}
			return array3;
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000EDF59 File Offset: 0x000EC159
		internal GridEntry GetDefaultGridEntry()
		{
			if (this.peDefault == null && this.currentPropEntries != null)
			{
				this.peDefault = (GridEntry)this.currentPropEntries[0];
			}
			return this.peDefault;
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000EDF88 File Offset: 0x000EC188
		internal Control GetElementFromPoint(Point point)
		{
			if (this.ToolbarAccessibleObject.Bounds.Contains(point))
			{
				return this.toolStrip;
			}
			if (this.GridViewAccessibleObject.Bounds.Contains(point))
			{
				return this.gridView;
			}
			if (this.HotCommandsAccessibleObject.Bounds.Contains(point))
			{
				return this.hotcommands;
			}
			if (this.HelpAccessibleObject.Bounds.Contains(point))
			{
				return this.doccomment;
			}
			return null;
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000EE00C File Offset: 0x000EC20C
		private object GetUnwrappedObject(int index)
		{
			if (this.currentObjects == null || index < 0 || index > this.currentObjects.Length)
			{
				return null;
			}
			object obj = this.currentObjects[index];
			if (obj is ICustomTypeDescriptor)
			{
				obj = ((ICustomTypeDescriptor)obj).GetPropertyOwner(null);
			}
			return obj;
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000EE051 File Offset: 0x000EC251
		internal GridEntryCollection GetPropEntries()
		{
			if (this.currentPropEntries == null)
			{
				this.UpdateSelection();
			}
			this.SetFlag(1, false);
			return this.currentPropEntries;
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000EE06F File Offset: 0x000EC26F
		private PropertyGridView GetPropertyGridView()
		{
			return this.gridView;
		}

		/// <summary>Activates the <see cref="T:System.Windows.Forms.PropertyGrid" /> control when the user chooses properties for a control in Design view. For a description of this member, see <see cref="M:System.Windows.Forms.ComponentModel.Com2Interop.IComPropertyBrowser.HandleF4" />.</summary>
		// Token: 0x060034B0 RID: 13488 RVA: 0x000EE077 File Offset: 0x000EC277
		void IComPropertyBrowser.HandleF4()
		{
			if (this.gridView.ContainsFocus)
			{
				return;
			}
			if (base.ActiveControl != this.gridView)
			{
				base.SetActiveControlInternal(this.gridView);
			}
			this.gridView.FocusInternal();
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000EE0AD File Offset: 0x000EC2AD
		internal bool HavePropEntriesChanged()
		{
			return this.GetFlag(1);
		}

		/// <summary>Loads user states from the registry into the <see cref="T:System.Windows.Forms.PropertyGrid" /> control. For a description of this member, see <see cref="M:System.Windows.Forms.ComponentModel.Com2Interop.IComPropertyBrowser.LoadState(Microsoft.Win32.RegistryKey)" />.</summary>
		/// <param name="optRoot">The registry key that contains the user states.</param>
		// Token: 0x060034B2 RID: 13490 RVA: 0x000EE0B8 File Offset: 0x000EC2B8
		void IComPropertyBrowser.LoadState(RegistryKey optRoot)
		{
			if (optRoot != null)
			{
				object obj = optRoot.GetValue("PbrsAlpha", "0");
				if (obj != null && obj.ToString().Equals("1"))
				{
					this.PropertySort = PropertySort.Alphabetical;
				}
				else
				{
					this.PropertySort = PropertySort.CategorizedAlphabetical;
				}
				obj = optRoot.GetValue("PbrsShowDesc", "1");
				this.HelpVisible = obj != null && obj.ToString().Equals("1");
				obj = optRoot.GetValue("PbrsShowCommands", "0");
				this.CommandsVisibleIfAvailable = obj != null && obj.ToString().Equals("1");
				obj = optRoot.GetValue("PbrsDescHeightRatio", "-1");
				bool flag = false;
				if (obj is string)
				{
					int num = int.Parse((string)obj, CultureInfo.InvariantCulture);
					if (num > 0)
					{
						this.dcSizeRatio = num;
						flag = true;
					}
				}
				obj = optRoot.GetValue("PbrsHotCommandHeightRatio", "-1");
				if (obj is string)
				{
					int num2 = int.Parse((string)obj, CultureInfo.InvariantCulture);
					if (num2 > 0)
					{
						this.dcSizeRatio = num2;
						flag = true;
					}
				}
				if (flag)
				{
					this.OnLayoutInternal(false);
					return;
				}
			}
			else
			{
				this.PropertySort = PropertySort.CategorizedAlphabetical;
				this.HelpVisible = true;
				this.CommandsVisibleIfAvailable = false;
			}
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x000EE1EC File Offset: 0x000EC3EC
		private void OnActiveDesignerChanged(object sender, ActiveDesignerEventArgs e)
		{
			if (e.OldDesigner != null && e.OldDesigner == this.designerHost)
			{
				this.ActiveDesigner = null;
			}
			if (e.NewDesigner != null && e.NewDesigner != this.designerHost)
			{
				this.ActiveDesigner = e.NewDesigner;
			}
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x000EE238 File Offset: 0x000EC438
		void UnsafeNativeMethods.IPropertyNotifySink.OnChanged(int dispID)
		{
			bool flag = false;
			PropertyDescriptorGridEntry propertyDescriptorGridEntry = this.gridView.SelectedGridEntry as PropertyDescriptorGridEntry;
			if (propertyDescriptorGridEntry != null && propertyDescriptorGridEntry.PropertyDescriptor != null && propertyDescriptorGridEntry.PropertyDescriptor.Attributes != null)
			{
				DispIdAttribute dispIdAttribute = (DispIdAttribute)propertyDescriptorGridEntry.PropertyDescriptor.Attributes[typeof(DispIdAttribute)];
				if (dispIdAttribute != null && !dispIdAttribute.IsDefaultAttribute())
				{
					flag = dispID != dispIdAttribute.Value;
				}
			}
			if (!this.GetFlag(512))
			{
				if (!this.gridView.GetInPropertySet() || flag)
				{
					this.Refresh(flag);
				}
				object unwrappedObject = this.GetUnwrappedObject(0);
				if (ComNativeDescriptor.Instance.IsNameDispId(unwrappedObject, dispID) || dispID == -800)
				{
					this.OnComComponentNameChanged(new ComponentRenameEventArgs(unwrappedObject, null, TypeDescriptor.GetClassName(unwrappedObject)));
				}
			}
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000EE300 File Offset: 0x000EC500
		private void OnChildMouseMove(object sender, MouseEventArgs me)
		{
			Point empty = Point.Empty;
			if (this.ShouldForwardChildMouseMessage((Control)sender, me, ref empty))
			{
				this.OnMouseMove(new MouseEventArgs(me.Button, me.Clicks, empty.X, empty.Y, me.Delta));
				return;
			}
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000EE350 File Offset: 0x000EC550
		private void OnChildMouseDown(object sender, MouseEventArgs me)
		{
			Point empty = Point.Empty;
			if (this.ShouldForwardChildMouseMessage((Control)sender, me, ref empty))
			{
				this.OnMouseDown(new MouseEventArgs(me.Button, me.Clicks, empty.X, empty.Y, me.Delta));
				return;
			}
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000EE3A0 File Offset: 0x000EC5A0
		private void OnComponentAdd(object sender, ComponentEventArgs e)
		{
			PropertyTabAttribute propertyTabAttribute = (PropertyTabAttribute)TypeDescriptor.GetAttributes(e.Component.GetType())[typeof(PropertyTabAttribute)];
			if (propertyTabAttribute == null)
			{
				return;
			}
			for (int i = 0; i < propertyTabAttribute.TabClasses.Length; i++)
			{
				if (propertyTabAttribute.TabScopes[i] == PropertyTabScope.Document)
				{
					this.AddRefTab(propertyTabAttribute.TabClasses[i], e.Component, PropertyTabScope.Document, true);
				}
			}
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000EE40C File Offset: 0x000EC60C
		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			bool flag = this.GetFlag(16);
			if (flag || this.GetFlag(4) || this.gridView.GetInPropertySet() || this.currentObjects == null || this.currentObjects.Length == 0)
			{
				if (flag && !this.gridView.GetInPropertySet())
				{
					this.SetFlag(256, true);
				}
				return;
			}
			int num = this.currentObjects.Length;
			for (int i = 0; i < num; i++)
			{
				if (this.currentObjects[i] == e.Component)
				{
					this.Refresh(false);
					return;
				}
			}
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000EE498 File Offset: 0x000EC698
		private void OnComponentRemove(object sender, ComponentEventArgs e)
		{
			PropertyTabAttribute propertyTabAttribute = (PropertyTabAttribute)TypeDescriptor.GetAttributes(e.Component.GetType())[typeof(PropertyTabAttribute)];
			if (propertyTabAttribute == null)
			{
				return;
			}
			for (int i = 0; i < propertyTabAttribute.TabClasses.Length; i++)
			{
				if (propertyTabAttribute.TabScopes[i] == PropertyTabScope.Document)
				{
					this.ReleaseTab(propertyTabAttribute.TabClasses[i], e.Component);
				}
			}
			for (int j = 0; j < this.currentObjects.Length; j++)
			{
				if (e.Component == this.currentObjects[j])
				{
					object[] array = new object[this.currentObjects.Length - 1];
					Array.Copy(this.currentObjects, 0, array, 0, j);
					if (j < array.Length)
					{
						Array.Copy(this.currentObjects, j + 1, array, j, array.Length - j);
					}
					if (!this.GetFlag(16))
					{
						this.SelectedObjects = array;
					}
					else
					{
						this.gridView.ClearProps();
						this.currentObjects = array;
						this.SetFlag(128, true);
					}
				}
			}
			this.SetupToolbar();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060034BA RID: 13498 RVA: 0x000EE597 File Offset: 0x000EC797
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			this.Refresh();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060034BB RID: 13499 RVA: 0x000EE5A6 File Offset: 0x000EC7A6
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.Refresh();
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x00047E32 File Offset: 0x00046032
		internal void OnGridViewMouseWheel(MouseEventArgs e)
		{
			this.OnMouseWheel(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060034BD RID: 13501 RVA: 0x000EE5B5 File Offset: 0x000EC7B5
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.OnLayoutInternal(false);
			TypeDescriptor.Refreshed += this.OnTypeDescriptorRefreshed;
			if (this.currentObjects != null && this.currentObjects.Length != 0)
			{
				this.Refresh(true);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060034BE RID: 13502 RVA: 0x000EE5EE File Offset: 0x000EC7EE
		protected override void OnHandleDestroyed(EventArgs e)
		{
			TypeDescriptor.Refreshed -= this.OnTypeDescriptorRefreshed;
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060034BF RID: 13503 RVA: 0x000EE608 File Offset: 0x000EC808
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (base.ActiveControl == null)
			{
				base.SetActiveControlInternal(this.gridView);
				return;
			}
			if (!base.ActiveControl.FocusInternal())
			{
				base.SetActiveControlInternal(this.gridView);
			}
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x060034C0 RID: 13504 RVA: 0x000EE640 File Offset: 0x000EC840
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			int num = (int)Math.Round((double)((float)base.Left * dx));
			int num2 = (int)Math.Round((double)((float)base.Top * dy));
			int num3 = base.Width;
			num3 = (int)Math.Round((double)((float)(base.Left + base.Width) * dx - (float)num));
			int num4 = base.Height;
			num4 = (int)Math.Round((double)((float)(base.Top + base.Height) * dy - (float)num2));
			base.SetBounds(num, num2, num3, num4, BoundsSpecified.All);
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000EE6C0 File Offset: 0x000EC8C0
		private void OnLayoutInternal(bool dividerOnly)
		{
			if (!base.IsHandleCreated || !base.Visible)
			{
				return;
			}
			try
			{
				this.FreezePainting = true;
				if (!dividerOnly)
				{
					if (!this.toolStrip.Visible && !this.doccomment.Visible && !this.hotcommands.Visible)
					{
						this.gridView.Location = new Point(0, 0);
						this.gridView.Size = base.Size;
						return;
					}
					if (this.toolStrip.Visible)
					{
						int width = base.Width;
						int num = (this.LargeButtons ? PropertyGrid.largeButtonSize : PropertyGrid.normalButtonSize).Height + this.toolStripButtonPaddingY;
						Rectangle rectangle = new Rectangle(0, 1, width, num);
						this.toolStrip.Bounds = rectangle;
						int y = this.gridView.Location.Y;
						this.gridView.Location = new Point(0, this.toolStrip.Height + this.toolStrip.Top);
					}
					else
					{
						this.gridView.Location = new Point(0, 0);
					}
				}
				int num2 = base.Size.Height;
				if (num2 >= 20)
				{
					int num3 = num2 - (this.gridView.Location.Y + 20);
					int num4 = 0;
					int num5 = 0;
					int num6 = 0;
					int num7 = 0;
					if (dividerOnly)
					{
						num4 = (this.doccomment.Visible ? this.doccomment.Size.Height : 0);
						num5 = (this.hotcommands.Visible ? this.hotcommands.Size.Height : 0);
					}
					else
					{
						if (this.doccomment.Visible)
						{
							num6 = this.doccomment.GetOptimalHeight(base.Size.Width - PropertyGrid.cyDivider);
							if (this.doccomment.userSized)
							{
								num4 = this.doccomment.Size.Height;
							}
							else if (this.dcSizeRatio != -1)
							{
								num4 = base.Height * this.dcSizeRatio / 100;
							}
							else
							{
								num4 = num6;
							}
						}
						if (this.hotcommands.Visible)
						{
							num7 = this.hotcommands.GetOptimalHeight(base.Size.Width - PropertyGrid.cyDivider);
							if (this.hotcommands.userSized)
							{
								num5 = this.hotcommands.Size.Height;
							}
							else if (this.hcSizeRatio != -1)
							{
								num5 = base.Height * this.hcSizeRatio / 100;
							}
							else
							{
								num5 = num7;
							}
						}
					}
					if (num4 > 0)
					{
						num3 -= PropertyGrid.cyDivider;
						int num8;
						if (num5 == 0 || num4 + num5 < num3)
						{
							num8 = Math.Min(num4, num3);
						}
						else if (num5 > 0 && num5 < num3)
						{
							num8 = num3 - num5;
						}
						else
						{
							num8 = Math.Min(num4, num3 / 2 - 1);
						}
						num8 = Math.Max(num8, PropertyGrid.cyDivider * 2);
						this.doccomment.SetBounds(0, num2 - num8, base.Size.Width, num8);
						if (num8 <= num6 && num8 < num4)
						{
							this.doccomment.userSized = false;
						}
						else if (this.dcSizeRatio != -1 || this.doccomment.userSized)
						{
							this.dcSizeRatio = this.doccomment.Height * 100 / base.Height;
						}
						this.doccomment.Invalidate();
						num2 = this.doccomment.Location.Y - PropertyGrid.cyDivider;
						num3 -= num8;
					}
					if (num5 > 0)
					{
						num3 -= PropertyGrid.cyDivider;
						int num8;
						if (num3 > num5)
						{
							num8 = Math.Min(num5, num3);
						}
						else
						{
							num8 = num3;
						}
						num8 = Math.Max(num8, PropertyGrid.cyDivider * 2);
						if (num8 <= num7 && num8 < num5)
						{
							this.hotcommands.userSized = false;
						}
						else if (this.hcSizeRatio != -1 || this.hotcommands.userSized)
						{
							this.hcSizeRatio = this.hotcommands.Height * 100 / base.Height;
						}
						this.hotcommands.SetBounds(0, num2 - num8, base.Size.Width, num8);
						this.hotcommands.Invalidate();
						num2 = this.hotcommands.Location.Y - PropertyGrid.cyDivider;
					}
					this.gridView.Size = new Size(base.Size.Width, num2 - this.gridView.Location.Y);
				}
			}
			finally
			{
				this.FreezePainting = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="me">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060034C2 RID: 13506 RVA: 0x000EEB58 File Offset: 0x000ECD58
		protected override void OnMouseDown(MouseEventArgs me)
		{
			PropertyGrid.SnappableControl snappableControl = this.DividerInside(me.X, me.Y);
			if (snappableControl != null && me.Button == MouseButtons.Left)
			{
				base.CaptureInternal = true;
				this.targetMove = snappableControl;
				this.dividerMoveY = me.Y;
				this.DividerDraw(this.dividerMoveY);
			}
			base.OnMouseDown(me);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
		/// <param name="me">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060034C3 RID: 13507 RVA: 0x000EEBB8 File Offset: 0x000ECDB8
		protected override void OnMouseMove(MouseEventArgs me)
		{
			if (this.dividerMoveY != -1)
			{
				int num = this.DividerLimitMove(this.targetMove, me.Y);
				if (num != this.dividerMoveY)
				{
					this.DividerDraw(this.dividerMoveY);
					this.dividerMoveY = num;
					this.DividerDraw(this.dividerMoveY);
				}
				base.OnMouseMove(me);
				return;
			}
			if (this.DividerInside(me.X, me.Y) != null)
			{
				this.Cursor = Cursors.HSplit;
				return;
			}
			this.Cursor = null;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="me">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060034C4 RID: 13508 RVA: 0x000EEC38 File Offset: 0x000ECE38
		protected override void OnMouseUp(MouseEventArgs me)
		{
			if (this.dividerMoveY == -1)
			{
				return;
			}
			this.Cursor = null;
			this.DividerDraw(this.dividerMoveY);
			this.dividerMoveY = this.DividerLimitMove(this.targetMove, me.Y);
			Rectangle bounds = this.targetMove.Bounds;
			if (this.dividerMoveY != bounds.Y)
			{
				int num = bounds.Height + bounds.Y - this.dividerMoveY - PropertyGrid.cyDivider / 2;
				Size size = this.targetMove.Size;
				size.Height = Math.Max(0, num);
				this.targetMove.Size = size;
				this.targetMove.userSized = true;
				this.OnLayoutInternal(true);
				base.Invalidate(new Rectangle(0, me.Y - PropertyGrid.cyDivider, base.Size.Width, me.Y + PropertyGrid.cyDivider));
				this.gridView.Invalidate(new Rectangle(0, this.gridView.Size.Height - PropertyGrid.cyDivider, base.Size.Width, PropertyGrid.cyDivider));
			}
			base.CaptureInternal = false;
			this.dividerMoveY = -1;
			this.targetMove = null;
			base.OnMouseUp(me);
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x0001180C File Offset: 0x0000FA0C
		int UnsafeNativeMethods.IPropertyNotifySink.OnRequestEdit(int dispID)
		{
			return 0;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060034C6 RID: 13510 RVA: 0x000EED7B File Offset: 0x000ECF7B
		protected override void OnResize(EventArgs e)
		{
			if (base.IsHandleCreated && base.Visible)
			{
				this.OnLayoutInternal(false);
			}
			base.OnResize(e);
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x000EED9B File Offset: 0x000ECF9B
		private void OnButtonClick(object sender, EventArgs e)
		{
			if (sender != this.btnViewPropertyPages)
			{
				this.gridView.FocusInternal();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ComponentModel.Com2Interop.IComPropertyBrowser.ComComponentNameChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentRenameEventArgs" /> that contains the event data.</param>
		// Token: 0x060034C8 RID: 13512 RVA: 0x000EEDB4 File Offset: 0x000ECFB4
		protected void OnComComponentNameChanged(ComponentRenameEventArgs e)
		{
			ComponentRenameEventHandler componentRenameEventHandler = (ComponentRenameEventHandler)base.Events[PropertyGrid.EventComComponentNameChanged];
			if (componentRenameEventHandler != null)
			{
				componentRenameEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="M:System.Drawing.Design.IPropertyValueUIService.NotifyPropertyValueUIItemsChanged" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060034C9 RID: 13513 RVA: 0x000EEDE2 File Offset: 0x000ECFE2
		protected void OnNotifyPropertyValueUIItemsChanged(object sender, EventArgs e)
		{
			this.gridView.LabelPaintMargin = 0;
			this.gridView.Invalidate(true);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x060034CA RID: 13514 RVA: 0x000EEDFC File Offset: 0x000ECFFC
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Point location = this.gridView.Location;
			int width = base.Size.Width;
			Brush brush;
			if (this.BackColor.IsSystemColor)
			{
				brush = SystemBrushes.FromSystemColor(this.BackColor);
			}
			else
			{
				brush = new SolidBrush(this.BackColor);
			}
			pevent.Graphics.FillRectangle(brush, new Rectangle(0, 0, width, location.Y));
			int num = location.Y + this.gridView.Size.Height;
			if (this.hotcommands.Visible)
			{
				pevent.Graphics.FillRectangle(brush, new Rectangle(0, num, width, this.hotcommands.Location.Y - num));
				num += this.hotcommands.Size.Height;
			}
			if (this.doccomment.Visible)
			{
				pevent.Graphics.FillRectangle(brush, new Rectangle(0, num, width, this.doccomment.Location.Y - num));
				num += this.doccomment.Size.Height;
			}
			pevent.Graphics.FillRectangle(brush, new Rectangle(0, num, width, base.Size.Height - num));
			if (!this.BackColor.IsSystemColor)
			{
				brush.Dispose();
			}
			base.OnPaint(pevent);
			if (this.lineBrush != null)
			{
				this.lineBrush.Dispose();
				this.lineBrush = null;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PropertyGrid.PropertySortChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060034CB RID: 13515 RVA: 0x000EEF84 File Offset: 0x000ED184
		protected virtual void OnPropertySortChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PropertyGrid.EventPropertySortChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PropertyGrid.PropertyTabChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PropertyTabChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x060034CC RID: 13516 RVA: 0x000EEFB4 File Offset: 0x000ED1B4
		protected virtual void OnPropertyTabChanged(PropertyTabChangedEventArgs e)
		{
			PropertyTabChangedEventHandler propertyTabChangedEventHandler = (PropertyTabChangedEventHandler)base.Events[PropertyGrid.EventPropertyTabChanged];
			if (propertyTabChangedEventHandler != null)
			{
				propertyTabChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PropertyGrid.PropertyValueChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PropertyValueChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x060034CD RID: 13517 RVA: 0x000EEFE4 File Offset: 0x000ED1E4
		protected virtual void OnPropertyValueChanged(PropertyValueChangedEventArgs e)
		{
			PropertyValueChangedEventHandler propertyValueChangedEventHandler = (PropertyValueChangedEventHandler)base.Events[PropertyGrid.EventPropertyValueChanged];
			if (propertyValueChangedEventHandler != null)
			{
				propertyValueChangedEventHandler(this, e);
			}
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000EF014 File Offset: 0x000ED214
		internal void OnPropertyValueSet(GridItem changedItem, object oldValue)
		{
			this.OnPropertyValueChanged(new PropertyValueChangedEventArgs(changedItem, oldValue));
			if (AccessibilityImprovements.Level3 && changedItem != null)
			{
				bool flag = false;
				Type propertyType = changedItem.PropertyDescriptor.PropertyType;
				UITypeEditor uitypeEditor = (UITypeEditor)TypeDescriptor.GetEditor(propertyType, typeof(UITypeEditor));
				if (uitypeEditor != null)
				{
					flag = uitypeEditor.GetEditStyle() == UITypeEditorEditStyle.DropDown;
				}
				else
				{
					GridEntry gridEntry = changedItem as GridEntry;
					if (gridEntry != null && gridEntry.Enumerable)
					{
						flag = true;
					}
				}
				if (flag && !this.gridView.DropDownVisible)
				{
					base.AccessibilityObject.RaiseAutomationNotification(AutomationNotificationKind.ActionCompleted, AutomationNotificationProcessing.All, SR.GetString("PropertyGridPropertyValueSelectedFormat", new object[] { changedItem.Value }));
				}
			}
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000EF0B9 File Offset: 0x000ED2B9
		internal void OnSelectedGridItemChanged(GridEntry oldEntry, GridEntry newEntry)
		{
			this.OnSelectedGridItemChanged(new SelectedGridItemChangedEventArgs(oldEntry, newEntry));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PropertyGrid.SelectedGridItemChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.SelectedGridItemChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x060034D0 RID: 13520 RVA: 0x000EF0C8 File Offset: 0x000ED2C8
		protected virtual void OnSelectedGridItemChanged(SelectedGridItemChangedEventArgs e)
		{
			SelectedGridItemChangedEventHandler selectedGridItemChangedEventHandler = (SelectedGridItemChangedEventHandler)base.Events[PropertyGrid.EventSelectedGridItemChanged];
			if (selectedGridItemChangedEventHandler != null)
			{
				selectedGridItemChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.PropertyGrid.SelectedObjectsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060034D1 RID: 13521 RVA: 0x000EF0F8 File Offset: 0x000ED2F8
		protected virtual void OnSelectedObjectsChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PropertyGrid.EventSelectedObjectsChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000EF128 File Offset: 0x000ED328
		private void OnTransactionClosed(object sender, DesignerTransactionCloseEventArgs e)
		{
			if (e.LastTransaction)
			{
				IComponent component = this.SelectedObject as IComponent;
				if (component != null && component.Site == null)
				{
					this.SelectedObject = null;
					return;
				}
				this.SetFlag(16, false);
				if (this.GetFlag(128))
				{
					this.SelectedObjects = this.currentObjects;
					this.SetFlag(128, false);
				}
				else if (this.GetFlag(256))
				{
					this.Refresh(false);
				}
				this.SetFlag(256, false);
			}
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000EF1AC File Offset: 0x000ED3AC
		private void OnTransactionOpened(object sender, EventArgs e)
		{
			this.SetFlag(16, true);
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000EF1B7 File Offset: 0x000ED3B7
		private void OnTypeDescriptorRefreshed(RefreshEventArgs e)
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new RefreshEventHandler(this.OnTypeDescriptorRefreshedInvoke), new object[] { e });
				return;
			}
			this.OnTypeDescriptorRefreshedInvoke(e);
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000EF1E8 File Offset: 0x000ED3E8
		private void OnTypeDescriptorRefreshedInvoke(RefreshEventArgs e)
		{
			if (this.currentObjects != null)
			{
				for (int i = 0; i < this.currentObjects.Length; i++)
				{
					Type typeChanged = e.TypeChanged;
					if (this.currentObjects[i] == e.ComponentChanged || (typeChanged != null && typeChanged.IsAssignableFrom(this.currentObjects[i].GetType())))
					{
						this.ClearCachedProps();
						this.Refresh(true);
						return;
					}
				}
			}
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000EF254 File Offset: 0x000ED454
		private void OnViewSortButtonClick(object sender, EventArgs e)
		{
			try
			{
				this.FreezePainting = true;
				if (sender == this.viewSortButtons[this.selectedViewSort])
				{
					this.viewSortButtons[this.selectedViewSort].Checked = true;
					return;
				}
				this.viewSortButtons[this.selectedViewSort].Checked = false;
				int num = 0;
				while (num < this.viewSortButtons.Length && this.viewSortButtons[num] != sender)
				{
					num++;
				}
				this.selectedViewSort = num;
				this.viewSortButtons[this.selectedViewSort].Checked = true;
				switch (this.selectedViewSort)
				{
				case 0:
					this.propertySortValue = PropertySort.CategorizedAlphabetical;
					break;
				case 1:
					this.propertySortValue = PropertySort.Alphabetical;
					break;
				case 2:
					this.propertySortValue = PropertySort.NoSort;
					break;
				}
				this.OnPropertySortChanged(EventArgs.Empty);
				this.Refresh(false);
				this.OnLayoutInternal(false);
			}
			finally
			{
				this.FreezePainting = false;
			}
			this.OnButtonClick(sender, e);
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000EF34C File Offset: 0x000ED54C
		private void OnViewTabButtonClick(object sender, EventArgs e)
		{
			try
			{
				this.FreezePainting = true;
				this.SelectViewTabButton((ToolStripButton)sender, true);
				this.OnLayoutInternal(false);
				this.SaveTabSelection();
			}
			finally
			{
				this.FreezePainting = false;
			}
			this.OnButtonClick(sender, e);
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000EF39C File Offset: 0x000ED59C
		private void OnViewButtonClickPP(object sender, EventArgs e)
		{
			if (this.btnViewPropertyPages.Enabled && this.currentObjects != null && this.currentObjects.Length != 0)
			{
				object obj = this.currentObjects[0];
				object obj2 = obj;
				bool flag = false;
				IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
				try
				{
					if (iuiservice != null)
					{
						flag = iuiservice.ShowComponentEditor(obj2, this);
					}
					else
					{
						try
						{
							ComponentEditor componentEditor = (ComponentEditor)TypeDescriptor.GetEditor(obj2, typeof(ComponentEditor));
							if (componentEditor != null)
							{
								if (componentEditor is WindowsFormsComponentEditor)
								{
									flag = ((WindowsFormsComponentEditor)componentEditor).EditComponent(null, obj2, this);
								}
								else
								{
									flag = componentEditor.EditComponent(obj2);
								}
							}
						}
						catch
						{
						}
					}
					if (flag)
					{
						if (obj is IComponent && this.connectionPointCookies[0] == null)
						{
							ISite site = ((IComponent)obj).Site;
							if (site != null)
							{
								IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
								if (componentChangeService != null)
								{
									try
									{
										componentChangeService.OnComponentChanging(obj, null);
									}
									catch (CheckoutException ex)
									{
										if (ex == CheckoutException.Canceled)
										{
											return;
										}
										throw ex;
									}
									try
									{
										this.SetFlag(4, true);
										componentChangeService.OnComponentChanged(obj, null, null, null);
									}
									finally
									{
										this.SetFlag(4, false);
									}
								}
							}
						}
						this.gridView.Refresh();
					}
				}
				catch (Exception ex2)
				{
					string @string = SR.GetString("ErrorPropertyPageFailed");
					if (iuiservice != null)
					{
						iuiservice.ShowError(ex2, @string);
					}
					else
					{
						RTLAwareMessageBox.Show(null, @string, SR.GetString("PropertyGridTitle"), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
					}
				}
			}
			this.OnButtonClick(sender, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060034D9 RID: 13529 RVA: 0x000EF548 File Offset: 0x000ED748
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible && base.IsHandleCreated)
			{
				this.OnLayoutInternal(false);
				this.SetupToolbar();
			}
		}

		/// <summary>Processes a dialog key.</summary>
		/// <param name="keyData">Specifies key codes and modifiers.</param>
		/// <returns>
		///   <see langword="true" /> if the key was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034DA RID: 13530 RVA: 0x000EF570 File Offset: 0x000ED770
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			Keys keys = keyData & Keys.KeyCode;
			if (keys != Keys.Tab || (keyData & Keys.Control) != Keys.None || (keyData & Keys.Alt) != Keys.None)
			{
				return base.ProcessDialogKey(keyData);
			}
			if ((keyData & Keys.Shift) != Keys.None)
			{
				if (this.hotcommands.Visible && this.hotcommands.ContainsFocus)
				{
					this.gridView.ReverseFocus();
				}
				else if (this.gridView.FocusInside)
				{
					if (!this.toolStrip.Visible)
					{
						return base.ProcessDialogKey(keyData);
					}
					this.toolStrip.FocusInternal();
					if (AccessibilityImprovements.Level1 && this.toolStrip.Items.Count > 0)
					{
						this.toolStrip.SelectNextToolStripItem(null, true);
					}
				}
				else
				{
					if (this.toolStrip.Focused || !this.toolStrip.Visible)
					{
						return base.ProcessDialogKey(keyData);
					}
					if (this.hotcommands.Visible)
					{
						this.hotcommands.Select(false);
					}
					else if (this.peMain != null)
					{
						this.gridView.ReverseFocus();
					}
					else
					{
						if (!this.toolStrip.Visible)
						{
							return base.ProcessDialogKey(keyData);
						}
						this.toolStrip.FocusInternal();
					}
				}
				return true;
			}
			bool flag = false;
			if (this.toolStrip.Focused)
			{
				if (this.peMain != null)
				{
					this.gridView.FocusInternal();
				}
				else
				{
					base.ProcessDialogKey(keyData);
				}
				return true;
			}
			if (this.gridView.FocusInside)
			{
				if (this.hotcommands.Visible)
				{
					this.hotcommands.Select(true);
					return true;
				}
				flag = true;
			}
			else if (this.hotcommands.ContainsFocus)
			{
				flag = true;
			}
			else if (this.toolStrip.Visible)
			{
				this.toolStrip.FocusInternal();
			}
			else
			{
				this.gridView.FocusInternal();
			}
			if (flag)
			{
				bool flag2 = base.ProcessDialogKey(keyData);
				if (!flag2 && base.Parent == null)
				{
					IntPtr parent = UnsafeNativeMethods.GetParent(new HandleRef(this, base.Handle));
					if (parent != IntPtr.Zero)
					{
						UnsafeNativeMethods.SetFocus(new HandleRef(null, parent));
					}
				}
				return flag2;
			}
			return true;
		}

		/// <summary>Forces the control to invalidate its client area and immediately redraw itself and any child controls.</summary>
		// Token: 0x060034DB RID: 13531 RVA: 0x000EF790 File Offset: 0x000ED990
		public override void Refresh()
		{
			if (this.GetFlag(512))
			{
				return;
			}
			this.Refresh(true);
			base.Refresh();
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000EF7B0 File Offset: 0x000ED9B0
		private void Refresh(bool clearCached)
		{
			if (base.Disposing)
			{
				return;
			}
			if (this.GetFlag(512))
			{
				return;
			}
			try
			{
				this.FreezePainting = true;
				this.SetFlag(512, true);
				if (clearCached)
				{
					this.ClearCachedProps();
				}
				this.RefreshProperties(clearCached);
				this.gridView.Refresh();
				this.DisplayHotCommands();
			}
			finally
			{
				this.FreezePainting = false;
				this.SetFlag(512, false);
			}
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000EF830 File Offset: 0x000EDA30
		internal void RefreshProperties(bool clearCached)
		{
			if (clearCached && this.selectedViewTab != -1 && this.viewTabs != null)
			{
				PropertyTab propertyTab = this.viewTabs[this.selectedViewTab];
				if (propertyTab != null && this.viewTabProps != null)
				{
					string text = propertyTab.TabName + this.propertySortValue.ToString();
					this.viewTabProps.Remove(text);
				}
			}
			this.SetFlag(1, true);
			this.UpdateSelection();
		}

		/// <summary>Refreshes the property tabs of the specified scope.</summary>
		/// <param name="tabScope">Either the <see langword="Component" /> or <see langword="Document" /> value of <see cref="T:System.ComponentModel.PropertyTabScope" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tabScope" /> parameter is not the <see langword="Component" /> or <see langword="Document" /> value of <see cref="T:System.ComponentModel.PropertyTabScope" />.</exception>
		// Token: 0x060034DE RID: 13534 RVA: 0x000EF8A4 File Offset: 0x000EDAA4
		public void RefreshTabs(PropertyTabScope tabScope)
		{
			if (tabScope < PropertyTabScope.Document)
			{
				throw new ArgumentException(SR.GetString("PropertyGridTabScope"));
			}
			this.RemoveTabs(tabScope, false);
			if (tabScope <= PropertyTabScope.Component && this.currentObjects != null && this.currentObjects.Length != 0)
			{
				Type[] commonTabs = PropertyGrid.GetCommonTabs(this.currentObjects, PropertyTabScope.Component);
				for (int i = 0; i < commonTabs.Length; i++)
				{
					for (int j = 0; j < this.currentObjects.Length; j++)
					{
						this.AddRefTab(commonTabs[i], this.currentObjects[j], PropertyTabScope.Component, false);
					}
				}
			}
			if (tabScope <= PropertyTabScope.Document && this.designerHost != null)
			{
				IContainer container = this.designerHost.Container;
				if (container != null)
				{
					ComponentCollection components = container.Components;
					if (components != null)
					{
						foreach (object obj in components)
						{
							IComponent component = (IComponent)obj;
							PropertyTabAttribute propertyTabAttribute = (PropertyTabAttribute)TypeDescriptor.GetAttributes(component.GetType())[typeof(PropertyTabAttribute)];
							if (propertyTabAttribute != null)
							{
								for (int k = 0; k < propertyTabAttribute.TabClasses.Length; k++)
								{
									if (propertyTabAttribute.TabScopes[k] == PropertyTabScope.Document)
									{
										this.AddRefTab(propertyTabAttribute.TabClasses[k], component, PropertyTabScope.Document, false);
									}
								}
							}
						}
					}
				}
			}
			this.SetupToolbar();
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x000EFA04 File Offset: 0x000EDC04
		internal void ReleaseTab(Type tabType, object component)
		{
			PropertyTab propertyTab = null;
			int num = -1;
			for (int i = 0; i < this.viewTabs.Length; i++)
			{
				if (tabType == this.viewTabs[i].GetType())
				{
					propertyTab = this.viewTabs[i];
					num = i;
					break;
				}
			}
			if (propertyTab == null)
			{
				return;
			}
			object[] array = propertyTab.Components;
			bool flag = false;
			try
			{
				int num2 = -1;
				if (array != null)
				{
					num2 = Array.IndexOf<object>(array, component);
				}
				if (num2 >= 0)
				{
					object[] array2 = new object[array.Length - 1];
					Array.Copy(array, 0, array2, 0, num2);
					Array.Copy(array, num2 + 1, array2, num2, array.Length - num2 - 1);
					array = array2;
					propertyTab.Components = array;
				}
				flag = array.Length == 0;
			}
			catch (Exception ex)
			{
				flag = true;
			}
			if (flag && this.viewTabScopes[num] > PropertyTabScope.Global)
			{
				this.RemoveTab(num, false);
			}
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x000EFAE0 File Offset: 0x000EDCE0
		private void RemoveImage(int index)
		{
			this.imageList[0].Images.RemoveAt(index);
			if (this.imageList[1] != null)
			{
				this.imageList[1].Images.RemoveAt(index);
			}
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000EFB14 File Offset: 0x000EDD14
		internal void RemoveTabs(PropertyTabScope classification, bool setupToolbar)
		{
			if (classification == PropertyTabScope.Static)
			{
				throw new ArgumentException(SR.GetString("PropertyGridRemoveStaticTabs"));
			}
			if (this.viewTabButtons == null || this.viewTabs == null || this.viewTabScopes == null)
			{
				return;
			}
			ToolStripButton toolStripButton = ((this.selectedViewTab >= 0 && this.selectedViewTab < this.viewTabButtons.Length) ? this.viewTabButtons[this.selectedViewTab] : null);
			for (int i = this.viewTabs.Length - 1; i >= 0; i--)
			{
				if (this.viewTabScopes[i] >= classification)
				{
					if (this.selectedViewTab == i)
					{
						this.selectedViewTab = -1;
					}
					else if (this.selectedViewTab > i)
					{
						this.selectedViewTab--;
					}
					PropertyTab[] array = new PropertyTab[this.viewTabs.Length - 1];
					Array.Copy(this.viewTabs, 0, array, 0, i);
					Array.Copy(this.viewTabs, i + 1, array, i, this.viewTabs.Length - i - 1);
					this.viewTabs = array;
					PropertyTabScope[] array2 = new PropertyTabScope[this.viewTabScopes.Length - 1];
					Array.Copy(this.viewTabScopes, 0, array2, 0, i);
					Array.Copy(this.viewTabScopes, i + 1, array2, i, this.viewTabScopes.Length - i - 1);
					this.viewTabScopes = array2;
					this.viewTabsDirty = true;
				}
			}
			if (setupToolbar && this.viewTabsDirty)
			{
				this.SetupToolbar();
				this.selectedViewTab = -1;
				this.SelectViewTabButtonDefault(toolStripButton);
				for (int j = 0; j < this.viewTabs.Length; j++)
				{
					this.viewTabs[j].Components = new object[0];
				}
			}
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000EFCA0 File Offset: 0x000EDEA0
		internal void RemoveTab(int tabIndex, bool setupToolbar)
		{
			if (tabIndex >= this.viewTabs.Length || tabIndex < 0)
			{
				throw new ArgumentOutOfRangeException("tabIndex", SR.GetString("PropertyGridBadTabIndex"));
			}
			if (this.viewTabScopes[tabIndex] == PropertyTabScope.Static)
			{
				throw new ArgumentException(SR.GetString("PropertyGridRemoveStaticTabs"));
			}
			if (this.selectedViewTab == tabIndex)
			{
				this.selectedViewTab = 0;
			}
			if (!this.GetFlag(32) && this.ActiveDesigner != null)
			{
				int hashCode = this.ActiveDesigner.GetHashCode();
				if (this.designerSelections != null && this.designerSelections.ContainsKey(hashCode) && (int)this.designerSelections[hashCode] == tabIndex)
				{
					this.designerSelections.Remove(hashCode);
				}
			}
			ToolStripButton toolStripButton = this.viewTabButtons[this.selectedViewTab];
			PropertyTab[] array = new PropertyTab[this.viewTabs.Length - 1];
			Array.Copy(this.viewTabs, 0, array, 0, tabIndex);
			Array.Copy(this.viewTabs, tabIndex + 1, array, tabIndex, this.viewTabs.Length - tabIndex - 1);
			this.viewTabs = array;
			PropertyTabScope[] array2 = new PropertyTabScope[this.viewTabScopes.Length - 1];
			Array.Copy(this.viewTabScopes, 0, array2, 0, tabIndex);
			Array.Copy(this.viewTabScopes, tabIndex + 1, array2, tabIndex, this.viewTabScopes.Length - tabIndex - 1);
			this.viewTabScopes = array2;
			this.viewTabsDirty = true;
			if (setupToolbar)
			{
				this.SetupToolbar();
				this.selectedViewTab = -1;
				this.SelectViewTabButtonDefault(toolStripButton);
			}
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000EFE10 File Offset: 0x000EE010
		internal void RemoveTab(Type tabType)
		{
			int num = -1;
			for (int i = 0; i < this.viewTabs.Length; i++)
			{
				if (tabType == this.viewTabs[i].GetType())
				{
					PropertyTab propertyTab = this.viewTabs[i];
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return;
			}
			PropertyTab[] array = new PropertyTab[this.viewTabs.Length - 1];
			Array.Copy(this.viewTabs, 0, array, 0, num);
			Array.Copy(this.viewTabs, num + 1, array, num, this.viewTabs.Length - num - 1);
			this.viewTabs = array;
			PropertyTabScope[] array2 = new PropertyTabScope[this.viewTabScopes.Length - 1];
			Array.Copy(this.viewTabScopes, 0, array2, 0, num);
			Array.Copy(this.viewTabScopes, num + 1, array2, num, this.viewTabScopes.Length - num - 1);
			this.viewTabScopes = array2;
			this.viewTabsDirty = true;
			this.SetupToolbar();
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000EFEF3 File Offset: 0x000EE0F3
		private void ResetCommandsBackColor()
		{
			this.hotcommands.ResetBackColor();
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000EFF00 File Offset: 0x000EE100
		private void ResetCommandsForeColor()
		{
			this.hotcommands.ResetForeColor();
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x000EFF0D File Offset: 0x000EE10D
		private void ResetCommandsLinkColor()
		{
			this.hotcommands.Label.ResetLinkColor();
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x000EFF1F File Offset: 0x000EE11F
		private void ResetCommandsActiveLinkColor()
		{
			this.hotcommands.Label.ResetActiveLinkColor();
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000EFF31 File Offset: 0x000EE131
		private void ResetCommandsDisabledLinkColor()
		{
			this.hotcommands.Label.ResetDisabledLinkColor();
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000EFF43 File Offset: 0x000EE143
		private void ResetHelpBackColor()
		{
			this.doccomment.ResetBackColor();
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000EFF43 File Offset: 0x000EE143
		private void ResetHelpForeColor()
		{
			this.doccomment.ResetBackColor();
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000EFF50 File Offset: 0x000EE150
		internal void ReplaceSelectedObject(object oldObject, object newObject)
		{
			for (int i = 0; i < this.currentObjects.Length; i++)
			{
				if (this.currentObjects[i] == oldObject)
				{
					this.currentObjects[i] = newObject;
					this.Refresh(true);
					return;
				}
			}
		}

		/// <summary>Resets the selected property to its default value.</summary>
		// Token: 0x060034EC RID: 13548 RVA: 0x000EFF8C File Offset: 0x000EE18C
		public void ResetSelectedProperty()
		{
			this.GetPropertyGridView().Reset();
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000EFF9C File Offset: 0x000EE19C
		private void SaveTabSelection()
		{
			if (this.designerHost != null)
			{
				if (this.designerSelections == null)
				{
					this.designerSelections = new Hashtable();
				}
				this.designerSelections[this.designerHost.GetHashCode()] = this.selectedViewTab;
			}
		}

		/// <summary>Saves user states from the <see cref="T:System.Windows.Forms.PropertyGrid" /> control to the registry. For a description of this member, see <see cref="M:System.Windows.Forms.ComponentModel.Com2Interop.IComPropertyBrowser.SaveState(Microsoft.Win32.RegistryKey)" />.</summary>
		/// <param name="optRoot">The registry key that contains the user states.</param>
		// Token: 0x060034EE RID: 13550 RVA: 0x000EFFEC File Offset: 0x000EE1EC
		void IComPropertyBrowser.SaveState(RegistryKey optRoot)
		{
			if (optRoot == null)
			{
				return;
			}
			optRoot.SetValue("PbrsAlpha", (this.PropertySort == PropertySort.Alphabetical) ? "1" : "0");
			optRoot.SetValue("PbrsShowDesc", this.HelpVisible ? "1" : "0");
			optRoot.SetValue("PbrsShowCommands", this.CommandsVisibleIfAvailable ? "1" : "0");
			optRoot.SetValue("PbrsDescHeightRatio", this.dcSizeRatio.ToString(CultureInfo.InvariantCulture));
			optRoot.SetValue("PbrsHotCommandHeightRatio", this.hcSizeRatio.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000F0094 File Offset: 0x000EE294
		private void SetHotCommandColors(bool vscompat)
		{
			if (vscompat)
			{
				this.hotcommands.SetColors(SystemColors.Control, SystemColors.ControlText, SystemColors.ActiveCaption, SystemColors.ActiveCaption, SystemColors.ActiveCaption, SystemColors.ControlDark);
				return;
			}
			this.hotcommands.SetColors(SystemColors.Control, SystemColors.ControlText, Color.Empty, Color.Empty, Color.Empty, Color.Empty);
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000F00F7 File Offset: 0x000EE2F7
		internal void SetStatusBox(string title, string desc)
		{
			this.doccomment.SetComment(title, desc);
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000F0108 File Offset: 0x000EE308
		private void SelectViewTabButton(ToolStripButton button, bool updateSelection)
		{
			int num = this.selectedViewTab;
			this.SelectViewTabButtonDefault(button);
			if (updateSelection)
			{
				this.Refresh(false);
			}
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000F0130 File Offset: 0x000EE330
		private bool SelectViewTabButtonDefault(ToolStripButton button)
		{
			if (this.selectedViewTab >= 0 && this.selectedViewTab >= this.viewTabButtons.Length)
			{
				this.selectedViewTab = -1;
			}
			if (this.selectedViewTab >= 0 && this.selectedViewTab < this.viewTabButtons.Length && button == this.viewTabButtons[this.selectedViewTab])
			{
				this.viewTabButtons[this.selectedViewTab].Checked = true;
				return true;
			}
			PropertyTab propertyTab = null;
			if (this.selectedViewTab != -1)
			{
				this.viewTabButtons[this.selectedViewTab].Checked = false;
				propertyTab = this.viewTabs[this.selectedViewTab];
			}
			for (int i = 0; i < this.viewTabButtons.Length; i++)
			{
				if (this.viewTabButtons[i] == button)
				{
					this.selectedViewTab = i;
					this.viewTabButtons[i].Checked = true;
					try
					{
						this.SetFlag(8, true);
						this.OnPropertyTabChanged(new PropertyTabChangedEventArgs(propertyTab, this.viewTabs[i]));
					}
					finally
					{
						this.SetFlag(8, false);
					}
					return true;
				}
			}
			this.selectedViewTab = 0;
			this.SelectViewTabButton(this.viewTabButtons[0], false);
			return false;
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x000F024C File Offset: 0x000EE44C
		private void SetSelectState(int state)
		{
			if (state >= this.viewTabs.Length * this.viewSortButtons.Length)
			{
				state = 0;
			}
			else if (state < 0)
			{
				state = this.viewTabs.Length * this.viewSortButtons.Length - 1;
			}
			int num = this.viewSortButtons.Length;
			if (num > 0)
			{
				int num2 = state / num;
				int num3 = state % num;
				this.OnViewTabButtonClick(this.viewTabButtons[num2], EventArgs.Empty);
				this.OnViewSortButtonClick(this.viewSortButtons[num3], EventArgs.Empty);
			}
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000F02C8 File Offset: 0x000EE4C8
		private void SetToolStripRenderer()
		{
			if (this.DrawFlatToolbar || (SystemInformation.HighContrast && AccessibilityImprovements.Level1))
			{
				this.ToolStripRenderer = new ToolStripProfessionalRenderer(new ProfessionalColorTable
				{
					UseSystemColors = true
				});
				return;
			}
			this.ToolStripRenderer = new ToolStripSystemRenderer();
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000F0310 File Offset: 0x000EE510
		private void SetupToolbar()
		{
			this.SetupToolbar(false);
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000F031C File Offset: 0x000EE51C
		private void SetupToolbar(bool fullRebuild)
		{
			if (!this.viewTabsDirty && !fullRebuild)
			{
				return;
			}
			try
			{
				this.FreezePainting = true;
				if (this.imageList[0] == null || fullRebuild)
				{
					this.imageList[0] = new ImageList();
					if (DpiHelper.IsScalingRequired)
					{
						this.imageList[0].ImageSize = PropertyGrid.normalButtonSize;
					}
				}
				EventHandler eventHandler = new EventHandler(this.OnViewTabButtonClick);
				EventHandler eventHandler2 = new EventHandler(this.OnViewSortButtonClick);
				EventHandler eventHandler3 = new EventHandler(this.OnViewButtonClickPP);
				ArrayList arrayList;
				if (fullRebuild)
				{
					arrayList = new ArrayList();
				}
				else
				{
					arrayList = new ArrayList(this.toolStrip.Items);
				}
				if (this.viewSortButtons == null || fullRebuild)
				{
					this.viewSortButtons = new ToolStripButton[3];
					int num = -1;
					int num2 = -1;
					try
					{
						if (this.bmpAlpha == null)
						{
							this.bmpAlpha = this.SortByPropertyImage;
						}
						num = this.AddImage(this.bmpAlpha);
					}
					catch (Exception ex)
					{
					}
					try
					{
						if (this.bmpCategory == null)
						{
							this.bmpCategory = this.SortByCategoryImage;
						}
						num2 = this.AddImage(this.bmpCategory);
					}
					catch (Exception ex2)
					{
					}
					this.viewSortButtons[1] = this.CreatePushButton(SR.GetString("PBRSToolTipAlphabetic"), num, eventHandler2, true);
					this.viewSortButtons[0] = this.CreatePushButton(SR.GetString("PBRSToolTipCategorized"), num2, eventHandler2, true);
					this.viewSortButtons[2] = this.CreatePushButton("", 0, eventHandler2, true);
					this.viewSortButtons[2].Visible = false;
					for (int i = 0; i < this.viewSortButtons.Length; i++)
					{
						arrayList.Add(this.viewSortButtons[i]);
					}
				}
				else
				{
					int num3 = arrayList.Count;
					for (int i = num3 - 1; i >= 2; i--)
					{
						arrayList.RemoveAt(i);
					}
					num3 = this.imageList[0].Images.Count;
					for (int i = num3 - 1; i >= 2; i--)
					{
						this.RemoveImage(i);
					}
				}
				arrayList.Add(this.separator1);
				this.viewTabButtons = new ToolStripButton[this.viewTabs.Length];
				bool flag = this.viewTabs.Length > 1;
				for (int i = 0; i < this.viewTabs.Length; i++)
				{
					try
					{
						Bitmap bitmap = this.viewTabs[i].Bitmap;
						this.viewTabButtons[i] = this.CreatePushButton(this.viewTabs[i].TabName, this.AddImage(bitmap), eventHandler, true);
						if (flag)
						{
							arrayList.Add(this.viewTabButtons[i]);
						}
					}
					catch (Exception ex3)
					{
					}
				}
				if (flag)
				{
					arrayList.Add(this.separator2);
				}
				int num4 = 0;
				try
				{
					if (this.bmpPropPage == null)
					{
						this.bmpPropPage = this.ShowPropertyPageImage;
					}
					num4 = this.AddImage(this.bmpPropPage);
				}
				catch (Exception ex4)
				{
				}
				this.btnViewPropertyPages = this.CreatePushButton(SR.GetString("PBRSToolTipPropertyPages"), num4, eventHandler3, false);
				this.btnViewPropertyPages.Enabled = false;
				arrayList.Add(this.btnViewPropertyPages);
				if (this.imageList[1] != null)
				{
					this.imageList[1].Dispose();
					this.imageList[1] = null;
				}
				if (this.buttonType != 0)
				{
					this.EnsureLargeButtons();
				}
				this.toolStrip.ImageList = this.imageList[this.buttonType];
				this.toolStrip.SuspendLayout();
				this.toolStrip.Items.Clear();
				for (int j = 0; j < arrayList.Count; j++)
				{
					this.toolStrip.Items.Add(arrayList[j] as ToolStripItem);
				}
				this.toolStrip.ResumeLayout();
				if (this.viewTabsDirty)
				{
					this.OnLayoutInternal(false);
				}
				this.viewTabsDirty = false;
			}
			finally
			{
				this.FreezePainting = false;
			}
		}

		/// <summary>Displays or hides the events button.</summary>
		/// <param name="value">
		///   <see langword="true" /> to show the events button; <see langword="false" /> to hide the events button.</param>
		// Token: 0x060034F7 RID: 13559 RVA: 0x000F0748 File Offset: 0x000EE948
		protected void ShowEventsButton(bool value)
		{
			if (this.viewTabs != null && this.viewTabs.Length > 1 && this.viewTabs[1] is EventsTab)
			{
				this.viewTabButtons[1].Visible = value;
				if (!value && this.selectedViewTab == 1)
				{
					this.SelectViewTabButton(this.viewTabButtons[0], true);
				}
			}
			this.UpdatePropertiesViewTabVisibility();
		}

		/// <summary>Gets the image that represents sorting grid items by property name.</summary>
		/// <returns>The image that represents sorting by property.</returns>
		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x060034F8 RID: 13560 RVA: 0x000F07A6 File Offset: 0x000EE9A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual Bitmap SortByPropertyImage
		{
			get
			{
				return new Bitmap(typeof(PropertyGrid), "PBAlpha.bmp");
			}
		}

		/// <summary>Gets the image that represents sorting grid items by category.</summary>
		/// <returns>The image that represents sorting by category.</returns>
		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x060034F9 RID: 13561 RVA: 0x000F07BC File Offset: 0x000EE9BC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual Bitmap SortByCategoryImage
		{
			get
			{
				return new Bitmap(typeof(PropertyGrid), "PBCatego.bmp");
			}
		}

		/// <summary>Gets the image that represents the property page.</summary>
		/// <returns>The image that represents the property page.</returns>
		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x060034FA RID: 13562 RVA: 0x000F07D2 File Offset: 0x000EE9D2
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual Bitmap ShowPropertyPageImage
		{
			get
			{
				return new Bitmap(typeof(PropertyGrid), "PBPPage.bmp");
			}
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000F07E8 File Offset: 0x000EE9E8
		private bool ShouldSerializeCommandsBackColor()
		{
			return this.hotcommands.ShouldSerializeBackColor();
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000F07F5 File Offset: 0x000EE9F5
		private bool ShouldSerializeCommandsForeColor()
		{
			return this.hotcommands.ShouldSerializeForeColor();
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000F0802 File Offset: 0x000EEA02
		private bool ShouldSerializeCommandsLinkColor()
		{
			return this.hotcommands.Label.ShouldSerializeLinkColor();
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000F0814 File Offset: 0x000EEA14
		private bool ShouldSerializeCommandsActiveLinkColor()
		{
			return this.hotcommands.Label.ShouldSerializeActiveLinkColor();
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000F0826 File Offset: 0x000EEA26
		private bool ShouldSerializeCommandsDisabledLinkColor()
		{
			return this.hotcommands.Label.ShouldSerializeDisabledLinkColor();
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000F0838 File Offset: 0x000EEA38
		private void SinkPropertyNotifyEvents()
		{
			int num = 0;
			while (this.connectionPointCookies != null && num < this.connectionPointCookies.Length)
			{
				if (this.connectionPointCookies[num] != null)
				{
					this.connectionPointCookies[num].Disconnect();
					this.connectionPointCookies[num] = null;
				}
				num++;
			}
			if (this.currentObjects == null || this.currentObjects.Length == 0)
			{
				this.connectionPointCookies = null;
				return;
			}
			if (this.connectionPointCookies == null || this.currentObjects.Length > this.connectionPointCookies.Length)
			{
				this.connectionPointCookies = new AxHost.ConnectionPointCookie[this.currentObjects.Length];
			}
			for (int i = 0; i < this.currentObjects.Length; i++)
			{
				try
				{
					object unwrappedObject = this.GetUnwrappedObject(i);
					if (Marshal.IsComObject(unwrappedObject))
					{
						this.connectionPointCookies[i] = new AxHost.ConnectionPointCookie(unwrappedObject, this, typeof(UnsafeNativeMethods.IPropertyNotifySink), false);
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000F091C File Offset: 0x000EEB1C
		private bool ShouldForwardChildMouseMessage(Control child, MouseEventArgs me, ref Point pt)
		{
			Size size = child.Size;
			if (me.Y <= 1 || size.Height - me.Y <= 1)
			{
				NativeMethods.POINT point = new NativeMethods.POINT();
				point.x = me.X;
				point.y = me.Y;
				UnsafeNativeMethods.MapWindowPoints(new HandleRef(child, child.Handle), new HandleRef(this, base.Handle), point, 1);
				pt.X = point.x;
				pt.Y = point.y;
				return true;
			}
			return false;
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000F09A4 File Offset: 0x000EEBA4
		private void UpdatePropertiesViewTabVisibility()
		{
			if (this.viewTabButtons != null)
			{
				int num = 0;
				for (int i = 1; i < this.viewTabButtons.Length; i++)
				{
					if (this.viewTabButtons[i].Visible)
					{
						num++;
					}
				}
				if (num > 0)
				{
					this.viewTabButtons[0].Visible = true;
					this.separator2.Visible = true;
					return;
				}
				this.viewTabButtons[0].Visible = false;
				this.separator2.Visible = false;
			}
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000F0A1C File Offset: 0x000EEC1C
		internal void UpdateSelection()
		{
			if (!this.GetFlag(1))
			{
				return;
			}
			if (this.viewTabs == null)
			{
				return;
			}
			string text = this.viewTabs[this.selectedViewTab].TabName + this.propertySortValue.ToString();
			if (this.viewTabProps != null && this.viewTabProps.ContainsKey(text))
			{
				this.peMain = (GridEntry)this.viewTabProps[text];
				if (this.peMain != null)
				{
					this.peMain.Refresh();
				}
			}
			else
			{
				if (this.currentObjects != null && this.currentObjects.Length != 0)
				{
					this.peMain = (GridEntry)GridEntry.Create(this.gridView, this.currentObjects, new PropertyGrid.PropertyGridServiceProvider(this), this.designerHost, this.SelectedTab, this.propertySortValue);
				}
				else
				{
					this.peMain = null;
				}
				if (this.peMain == null)
				{
					this.currentPropEntries = new GridEntryCollection(null, new GridEntry[0]);
					this.gridView.ClearProps();
					return;
				}
				if (this.BrowsableAttributes != null)
				{
					this.peMain.BrowsableAttributes = this.BrowsableAttributes;
				}
				if (this.viewTabProps == null)
				{
					this.viewTabProps = new Hashtable();
				}
				this.viewTabProps[text] = this.peMain;
			}
			this.currentPropEntries = this.peMain.Children;
			this.peDefault = this.peMain.DefaultChild;
			this.gridView.Invalidate();
		}

		/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06003504 RID: 13572 RVA: 0x00024807 File Offset: 0x00022A07
		// (set) Token: 0x06003505 RID: 13573 RVA: 0x000F0B8C File Offset: 0x000EED8C
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("UseCompatibleTextRenderingDescr")]
		public bool UseCompatibleTextRendering
		{
			get
			{
				return base.UseCompatibleTextRenderingInt;
			}
			set
			{
				base.UseCompatibleTextRenderingInt = value;
				this.doccomment.UpdateTextRenderingEngine();
				this.gridView.Invalidate();
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06003506 RID: 13574 RVA: 0x00028BBB File Offset: 0x00026DBB
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3 && !base.DesignMode;
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06003507 RID: 13575 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool SupportsUseCompatibleTextRendering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal override bool AllowsKeyboardToolTip()
		{
			return false;
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000F0BAB File Offset: 0x000EEDAB
		internal bool WantsTab(bool forward)
		{
			if (forward)
			{
				return this.toolStrip.Visible && this.toolStrip.Focused;
			}
			return this.gridView.ContainsFocus && this.toolStrip.Visible;
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000F0BE8 File Offset: 0x000EEDE8
		private void GetDataFromCopyData(IntPtr lparam)
		{
			NativeMethods.COPYDATASTRUCT copydatastruct = (NativeMethods.COPYDATASTRUCT)UnsafeNativeMethods.PtrToStructure(lparam, typeof(NativeMethods.COPYDATASTRUCT));
			if (copydatastruct != null && copydatastruct.lpData != IntPtr.Zero)
			{
				this.propName = Marshal.PtrToStringAuto(copydatastruct.lpData);
				this.dwMsg = copydatastruct.dwData;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SystemColorsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600350B RID: 13579 RVA: 0x000F0C3D File Offset: 0x000EEE3D
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			this.SetupToolbar(true);
			if (!this.GetFlag(64))
			{
				this.SetupToolbar(true);
				this.SetFlag(64, true);
			}
			base.OnSystemColorsChanged(e);
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x000F0C68 File Offset: 0x000EEE68
		private void RescaleConstants()
		{
			PropertyGrid.normalButtonSize = base.LogicalToDeviceUnits(PropertyGrid.DEFAULT_NORMAL_BUTTON_SIZE);
			PropertyGrid.largeButtonSize = base.LogicalToDeviceUnits(PropertyGrid.DEFAULT_LARGE_BUTTON_SIZE);
			PropertyGrid.cyDivider = base.LogicalToDeviceUnits(3);
			this.toolStripButtonPaddingY = base.LogicalToDeviceUnits(9);
			if (this.hotcommands != null && this.hotcommands.Visible)
			{
				this.Controls.Remove(this.hotcommands);
				this.Controls.Add(this.hotcommands);
			}
		}

		/// <summary>Provides constants for rescaling the <see cref="T:System.Windows.Forms.PropertyGrid" /> control when a DPI change occurs.</summary>
		/// <param name="deviceDpiOld">The DPI value prior to the change.</param>
		/// <param name="deviceDpiNew">The DPI value after the change.</param>
		// Token: 0x0600350D RID: 13581 RVA: 0x000F0CE6 File Offset: 0x000EEEE6
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.RescaleConstants();
				this.SetupToolbar(true);
			}
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x0600350E RID: 13582 RVA: 0x000F0D04 File Offset: 0x000EEF04
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 74)
			{
				switch (msg)
				{
				case 768:
					if ((long)m.LParam == 0L)
					{
						this.gridView.DoCutCommand();
						return;
					}
					m.Result = (this.CanCut ? ((IntPtr)1) : ((IntPtr)0));
					return;
				case 769:
					if ((long)m.LParam == 0L)
					{
						this.gridView.DoCopyCommand();
						return;
					}
					m.Result = (this.CanCopy ? ((IntPtr)1) : ((IntPtr)0));
					return;
				case 770:
					if ((long)m.LParam == 0L)
					{
						this.gridView.DoPasteCommand();
						return;
					}
					m.Result = (this.CanPaste ? ((IntPtr)1) : ((IntPtr)0));
					return;
				case 771:
					break;
				case 772:
					if ((long)m.LParam == 0L)
					{
						this.gridView.DoUndoCommand();
						return;
					}
					m.Result = (this.CanUndo ? ((IntPtr)1) : ((IntPtr)0));
					return;
				default:
					switch (msg)
					{
					case 1104:
						if (this.toolStrip != null)
						{
							m.Result = (IntPtr)this.toolStrip.Items.Count;
							return;
						}
						break;
					case 1105:
						if (this.toolStrip != null)
						{
							int num = (int)(long)m.WParam;
							if (num >= 0 && num < this.toolStrip.Items.Count)
							{
								ToolStripButton toolStripButton = this.toolStrip.Items[num] as ToolStripButton;
								if (toolStripButton != null)
								{
									toolStripButton.Checked = !toolStripButton.Checked;
									if (toolStripButton == this.btnViewPropertyPages)
									{
										this.OnViewButtonClickPP(toolStripButton, EventArgs.Empty);
										return;
									}
									int num2 = (int)(long)m.WParam;
									if (num2 <= 1)
									{
										this.OnViewSortButtonClick(toolStripButton, EventArgs.Empty);
										return;
									}
									this.SelectViewTabButton(toolStripButton, true);
								}
							}
							return;
						}
						break;
					case 1106:
						if (this.toolStrip != null)
						{
							int num3 = (int)(long)m.WParam;
							if (num3 >= 0 && num3 < this.toolStrip.Items.Count)
							{
								ToolStripButton toolStripButton2 = this.toolStrip.Items[num3] as ToolStripButton;
								if (toolStripButton2 != null)
								{
									m.Result = (IntPtr)(toolStripButton2.Checked ? 1 : 0);
									return;
								}
								m.Result = IntPtr.Zero;
							}
							return;
						}
						break;
					case 1107:
					case 1108:
						if (this.toolStrip != null)
						{
							int num4 = (int)(long)m.WParam;
							if (num4 >= 0 && num4 < this.toolStrip.Items.Count)
							{
								string text;
								if (m.Msg == 1107)
								{
									text = this.toolStrip.Items[num4].Text;
								}
								else
								{
									text = this.toolStrip.Items[num4].ToolTipText;
								}
								m.Result = AutomationMessages.WriteAutomationText(text);
							}
							return;
						}
						break;
					case 1109:
						if (m.Msg == this.dwMsg)
						{
							m.Result = (IntPtr)this.gridView.GetPropertyLocation(this.propName, m.LParam == IntPtr.Zero, m.WParam == IntPtr.Zero);
							return;
						}
						break;
					case 1110:
					case 1111:
						m.Result = this.gridView.SendMessage(m.Msg, m.WParam, m.LParam);
						return;
					case 1112:
						if (m.LParam != IntPtr.Zero)
						{
							string text2 = AutomationMessages.ReadAutomationText(m.LParam);
							for (int i = 0; i < this.viewTabs.Length; i++)
							{
								if (this.viewTabs[i].GetType().FullName == text2 && this.viewTabButtons[i].Visible)
								{
									this.SelectViewTabButtonDefault(this.viewTabButtons[i]);
									m.Result = (IntPtr)1;
									break;
								}
							}
						}
						m.Result = (IntPtr)0;
						return;
					case 1113:
					{
						string testingInfo = this.gridView.GetTestingInfo((int)(long)m.WParam);
						m.Result = AutomationMessages.WriteAutomationText(testingInfo);
						return;
					}
					}
					break;
				}
				base.WndProc(ref m);
				return;
			}
			this.GetDataFromCopyData(m.LParam);
			m.Result = (IntPtr)1;
		}

		// Token: 0x04001ED1 RID: 7889
		private DocComment doccomment;

		// Token: 0x04001ED2 RID: 7890
		private int dcSizeRatio = -1;

		// Token: 0x04001ED3 RID: 7891
		private int hcSizeRatio = -1;

		// Token: 0x04001ED4 RID: 7892
		private HotCommands hotcommands;

		// Token: 0x04001ED5 RID: 7893
		private ToolStrip toolStrip;

		// Token: 0x04001ED6 RID: 7894
		private bool helpVisible = true;

		// Token: 0x04001ED7 RID: 7895
		private bool toolbarVisible = true;

		// Token: 0x04001ED8 RID: 7896
		private ImageList[] imageList = new ImageList[2];

		// Token: 0x04001ED9 RID: 7897
		private Bitmap bmpAlpha;

		// Token: 0x04001EDA RID: 7898
		private Bitmap bmpCategory;

		// Token: 0x04001EDB RID: 7899
		private Bitmap bmpPropPage;

		// Token: 0x04001EDC RID: 7900
		private bool viewTabsDirty = true;

		// Token: 0x04001EDD RID: 7901
		private bool drawFlatToolBar;

		// Token: 0x04001EDE RID: 7902
		private PropertyTab[] viewTabs = new PropertyTab[0];

		// Token: 0x04001EDF RID: 7903
		private PropertyTabScope[] viewTabScopes = new PropertyTabScope[0];

		// Token: 0x04001EE0 RID: 7904
		private Hashtable viewTabProps;

		// Token: 0x04001EE1 RID: 7905
		private ToolStripButton[] viewTabButtons;

		// Token: 0x04001EE2 RID: 7906
		private int selectedViewTab;

		// Token: 0x04001EE3 RID: 7907
		private ToolStripButton[] viewSortButtons;

		// Token: 0x04001EE4 RID: 7908
		private int selectedViewSort;

		// Token: 0x04001EE5 RID: 7909
		private PropertySort propertySortValue;

		// Token: 0x04001EE6 RID: 7910
		private ToolStripButton btnViewPropertyPages;

		// Token: 0x04001EE7 RID: 7911
		private ToolStripSeparator separator1;

		// Token: 0x04001EE8 RID: 7912
		private ToolStripSeparator separator2;

		// Token: 0x04001EE9 RID: 7913
		private int buttonType;

		// Token: 0x04001EEA RID: 7914
		private PropertyGridView gridView;

		// Token: 0x04001EEB RID: 7915
		private IDesignerHost designerHost;

		// Token: 0x04001EEC RID: 7916
		private IDesignerEventService designerEventService;

		// Token: 0x04001EED RID: 7917
		private Hashtable designerSelections;

		// Token: 0x04001EEE RID: 7918
		private GridEntry peDefault;

		// Token: 0x04001EEF RID: 7919
		private GridEntry peMain;

		// Token: 0x04001EF0 RID: 7920
		private GridEntryCollection currentPropEntries;

		// Token: 0x04001EF1 RID: 7921
		private object[] currentObjects;

		// Token: 0x04001EF2 RID: 7922
		private int paintFrozen;

		// Token: 0x04001EF3 RID: 7923
		private Color lineColor = (SystemInformation.HighContrast ? (AccessibilityImprovements.Level1 ? SystemColors.ControlDarkDark : SystemColors.ControlDark) : SystemColors.InactiveBorder);

		// Token: 0x04001EF4 RID: 7924
		internal bool developerOverride;

		// Token: 0x04001EF5 RID: 7925
		internal Brush lineBrush;

		// Token: 0x04001EF6 RID: 7926
		private Color categoryForeColor = SystemColors.ControlText;

		// Token: 0x04001EF7 RID: 7927
		private Color categorySplitterColor = SystemColors.Control;

		// Token: 0x04001EF8 RID: 7928
		private Color viewBorderColor = SystemColors.ControlDark;

		// Token: 0x04001EF9 RID: 7929
		private Color selectedItemWithFocusForeColor = SystemColors.HighlightText;

		// Token: 0x04001EFA RID: 7930
		private Color selectedItemWithFocusBackColor = SystemColors.Highlight;

		// Token: 0x04001EFB RID: 7931
		internal Brush selectedItemWithFocusBackBrush;

		// Token: 0x04001EFC RID: 7932
		private bool canShowVisualStyleGlyphs = true;

		// Token: 0x04001EFD RID: 7933
		private AttributeCollection browsableAttributes;

		// Token: 0x04001EFE RID: 7934
		private PropertyGrid.SnappableControl targetMove;

		// Token: 0x04001EFF RID: 7935
		private int dividerMoveY = -1;

		// Token: 0x04001F00 RID: 7936
		private const int CYDIVIDER = 3;

		// Token: 0x04001F01 RID: 7937
		private static int cyDivider = 3;

		// Token: 0x04001F02 RID: 7938
		private const int CXINDENT = 0;

		// Token: 0x04001F03 RID: 7939
		private const int CYINDENT = 2;

		// Token: 0x04001F04 RID: 7940
		private const int MIN_GRID_HEIGHT = 20;

		// Token: 0x04001F05 RID: 7941
		private const int PROPERTIES = 0;

		// Token: 0x04001F06 RID: 7942
		private const int EVENTS = 1;

		// Token: 0x04001F07 RID: 7943
		private const int ALPHA = 1;

		// Token: 0x04001F08 RID: 7944
		private const int CATEGORIES = 0;

		// Token: 0x04001F09 RID: 7945
		private const int NO_SORT = 2;

		// Token: 0x04001F0A RID: 7946
		private const int NORMAL_BUTTONS = 0;

		// Token: 0x04001F0B RID: 7947
		private const int LARGE_BUTTONS = 1;

		// Token: 0x04001F0C RID: 7948
		private const int TOOLSTRIP_BUTTON_PADDING_Y = 9;

		// Token: 0x04001F0D RID: 7949
		private int toolStripButtonPaddingY = 9;

		// Token: 0x04001F0E RID: 7950
		private static readonly Size DEFAULT_LARGE_BUTTON_SIZE = new Size(32, 32);

		// Token: 0x04001F0F RID: 7951
		private static readonly Size DEFAULT_NORMAL_BUTTON_SIZE = new Size(16, 16);

		// Token: 0x04001F10 RID: 7952
		private static Size largeButtonSize = PropertyGrid.DEFAULT_LARGE_BUTTON_SIZE;

		// Token: 0x04001F11 RID: 7953
		private static Size normalButtonSize = PropertyGrid.DEFAULT_NORMAL_BUTTON_SIZE;

		// Token: 0x04001F12 RID: 7954
		private static bool isScalingInitialized = false;

		// Token: 0x04001F13 RID: 7955
		private const ushort PropertiesChanged = 1;

		// Token: 0x04001F14 RID: 7956
		private const ushort GotDesignerEventService = 2;

		// Token: 0x04001F15 RID: 7957
		private const ushort InternalChange = 4;

		// Token: 0x04001F16 RID: 7958
		private const ushort TabsChanging = 8;

		// Token: 0x04001F17 RID: 7959
		private const ushort BatchMode = 16;

		// Token: 0x04001F18 RID: 7960
		private const ushort ReInitTab = 32;

		// Token: 0x04001F19 RID: 7961
		private const ushort SysColorChangeRefresh = 64;

		// Token: 0x04001F1A RID: 7962
		private const ushort FullRefreshAfterBatch = 128;

		// Token: 0x04001F1B RID: 7963
		private const ushort BatchModeChange = 256;

		// Token: 0x04001F1C RID: 7964
		private const ushort RefreshingProperties = 512;

		// Token: 0x04001F1D RID: 7965
		private ushort flags;

		// Token: 0x04001F1E RID: 7966
		private readonly ComponentEventHandler onComponentAdd;

		// Token: 0x04001F1F RID: 7967
		private readonly ComponentEventHandler onComponentRemove;

		// Token: 0x04001F20 RID: 7968
		private readonly ComponentChangedEventHandler onComponentChanged;

		// Token: 0x04001F21 RID: 7969
		private AxHost.ConnectionPointCookie[] connectionPointCookies;

		// Token: 0x04001F22 RID: 7970
		private static object EventPropertyValueChanged = new object();

		// Token: 0x04001F23 RID: 7971
		private static object EventComComponentNameChanged = new object();

		// Token: 0x04001F24 RID: 7972
		private static object EventPropertyTabChanged = new object();

		// Token: 0x04001F25 RID: 7973
		private static object EventSelectedGridItemChanged = new object();

		// Token: 0x04001F26 RID: 7974
		private static object EventPropertySortChanged = new object();

		// Token: 0x04001F27 RID: 7975
		private static object EventSelectedObjectsChanged = new object();

		// Token: 0x04001F28 RID: 7976
		private string propName;

		// Token: 0x04001F29 RID: 7977
		private int dwMsg;

		// Token: 0x020007CC RID: 1996
		internal abstract class SnappableControl : Control
		{
			// Token: 0x06006D61 RID: 28001
			public abstract int GetOptimalHeight(int width);

			// Token: 0x06006D62 RID: 28002
			public abstract int SnapHeightRequest(int request);

			// Token: 0x06006D63 RID: 28003 RVA: 0x00191584 File Offset: 0x0018F784
			public SnappableControl(PropertyGrid ownerGrid)
			{
				this.ownerGrid = ownerGrid;
				base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			}

			// Token: 0x170017ED RID: 6125
			// (get) Token: 0x06006D64 RID: 28004 RVA: 0x0003057E File Offset: 0x0002E77E
			// (set) Token: 0x06006D65 RID: 28005 RVA: 0x0001A0A8 File Offset: 0x000182A8
			public override Cursor Cursor
			{
				get
				{
					return Cursors.Default;
				}
				set
				{
					base.Cursor = value;
				}
			}

			// Token: 0x06006D66 RID: 28006 RVA: 0x000070A6 File Offset: 0x000052A6
			protected override void OnControlAdded(ControlEventArgs ce)
			{
			}

			// Token: 0x170017EE RID: 6126
			// (get) Token: 0x06006D67 RID: 28007 RVA: 0x001915AA File Offset: 0x0018F7AA
			// (set) Token: 0x06006D68 RID: 28008 RVA: 0x001915B2 File Offset: 0x0018F7B2
			public Color BorderColor
			{
				get
				{
					return this.borderColor;
				}
				set
				{
					this.borderColor = value;
				}
			}

			// Token: 0x06006D69 RID: 28009 RVA: 0x001915BC File Offset: 0x0018F7BC
			protected override void OnPaint(PaintEventArgs e)
			{
				base.OnPaint(e);
				Rectangle clientRectangle = base.ClientRectangle;
				int num = clientRectangle.Width;
				clientRectangle.Width = num - 1;
				num = clientRectangle.Height;
				clientRectangle.Height = num - 1;
				using (Pen pen = new Pen(this.BorderColor, 1f))
				{
					e.Graphics.DrawRectangle(pen, clientRectangle);
				}
			}

			// Token: 0x04004295 RID: 17045
			private Color borderColor = SystemColors.ControlDark;

			// Token: 0x04004296 RID: 17046
			protected PropertyGrid ownerGrid;

			// Token: 0x04004297 RID: 17047
			internal bool userSized;
		}

		/// <summary>Contains a collection of <see cref="T:System.Windows.Forms.Design.PropertyTab" /> objects.</summary>
		// Token: 0x020007CD RID: 1997
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public class PropertyTabCollection : ICollection, IEnumerable
		{
			// Token: 0x06006D6A RID: 28010 RVA: 0x00191634 File Offset: 0x0018F834
			internal PropertyTabCollection(PropertyGrid owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the number of Property tabs in the collection.</summary>
			/// <returns>The number of Property tabs in the collection.</returns>
			// Token: 0x170017EF RID: 6127
			// (get) Token: 0x06006D6B RID: 28011 RVA: 0x00191643 File Offset: 0x0018F843
			public int Count
			{
				get
				{
					if (this.owner == null)
					{
						return 0;
					}
					return this.owner.viewTabs.Length;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the underlying list.</returns>
			// Token: 0x170017F0 RID: 6128
			// (get) Token: 0x06006D6C RID: 28012 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>
			///   <see langword="true" /> to indicate the list is synchronized; otherwise <see langword="false" />.</returns>
			// Token: 0x170017F1 RID: 6129
			// (get) Token: 0x06006D6D RID: 28013 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.PropertyTab" /> at the specified index.</summary>
			/// <param name="index">The index of the <see cref="T:System.Windows.Forms.Design.PropertyTab" /> to return.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.Design.PropertyTab" /> at the specified index.</returns>
			// Token: 0x170017F2 RID: 6130
			public PropertyTab this[int index]
			{
				get
				{
					if (this.owner == null)
					{
						throw new InvalidOperationException(SR.GetString("PropertyGridPropertyTabCollectionReadOnly"));
					}
					return this.owner.viewTabs[index];
				}
			}

			/// <summary>Adds a Property tab of the specified type to the collection.</summary>
			/// <param name="propertyTabType">The Property tab type to add to the grid.</param>
			// Token: 0x06006D6F RID: 28015 RVA: 0x00191683 File Offset: 0x0018F883
			public void AddTabType(Type propertyTabType)
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("PropertyGridPropertyTabCollectionReadOnly"));
				}
				this.owner.AddTab(propertyTabType, PropertyTabScope.Global);
			}

			/// <summary>Adds a Property tab of the specified type and with the specified scope to the collection.</summary>
			/// <param name="propertyTabType">The Property tab type to add to the grid.</param>
			/// <param name="tabScope">One of the <see cref="T:System.ComponentModel.PropertyTabScope" /> values.</param>
			// Token: 0x06006D70 RID: 28016 RVA: 0x001916AA File Offset: 0x0018F8AA
			public void AddTabType(Type propertyTabType, PropertyTabScope tabScope)
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("PropertyGridPropertyTabCollectionReadOnly"));
				}
				this.owner.AddTab(propertyTabType, tabScope);
			}

			/// <summary>Removes all the Property tabs of the specified scope from the collection.</summary>
			/// <param name="tabScope">The scope of the tabs to clear.</param>
			/// <exception cref="T:System.ArgumentException">The assigned value of the <paramref name="tabScope" /> parameter is less than the <see langword="Document" /> value of <see cref="T:System.ComponentModel.PropertyTabScope" />.</exception>
			// Token: 0x06006D71 RID: 28017 RVA: 0x001916D1 File Offset: 0x0018F8D1
			public void Clear(PropertyTabScope tabScope)
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("PropertyGridPropertyTabCollectionReadOnly"));
				}
				this.owner.ClearTabs(tabScope);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="dest">A zero-based array that receives the copied items from the collection.</param>
			/// <param name="index">The first position in the specified array to receive copied contents.</param>
			// Token: 0x06006D72 RID: 28018 RVA: 0x001916F7 File Offset: 0x0018F8F7
			void ICollection.CopyTo(Array dest, int index)
			{
				if (this.owner == null)
				{
					return;
				}
				if (this.owner.viewTabs.Length != 0)
				{
					Array.Copy(this.owner.viewTabs, 0, dest, index, this.owner.viewTabs.Length);
				}
			}

			/// <summary>Returns an enumeration of all the Property tabs in the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Windows.Forms.PropertyGrid.PropertyTabCollection" />.</returns>
			// Token: 0x06006D73 RID: 28019 RVA: 0x00191730 File Offset: 0x0018F930
			public IEnumerator GetEnumerator()
			{
				if (this.owner == null)
				{
					return new PropertyTab[0].GetEnumerator();
				}
				return this.owner.viewTabs.GetEnumerator();
			}

			/// <summary>Removes the specified tab type from the collection.</summary>
			/// <param name="propertyTabType">The tab type to remove from the collection.</param>
			// Token: 0x06006D74 RID: 28020 RVA: 0x00191756 File Offset: 0x0018F956
			public void RemoveTabType(Type propertyTabType)
			{
				if (this.owner == null)
				{
					throw new InvalidOperationException(SR.GetString("PropertyGridPropertyTabCollectionReadOnly"));
				}
				this.owner.RemoveTab(propertyTabType);
			}

			// Token: 0x04004298 RID: 17048
			internal static PropertyGrid.PropertyTabCollection Empty = new PropertyGrid.PropertyTabCollection(null);

			// Token: 0x04004299 RID: 17049
			private PropertyGrid owner;
		}

		// Token: 0x020007CE RID: 1998
		private interface IUnimplemented
		{
		}

		// Token: 0x020007CF RID: 1999
		internal class SelectedObjectConverter : ReferenceConverter
		{
			// Token: 0x06006D76 RID: 28022 RVA: 0x00191789 File Offset: 0x0018F989
			public SelectedObjectConverter()
				: base(typeof(IComponent))
			{
			}
		}

		// Token: 0x020007D0 RID: 2000
		private class PropertyGridServiceProvider : IServiceProvider
		{
			// Token: 0x06006D77 RID: 28023 RVA: 0x0019179B File Offset: 0x0018F99B
			public PropertyGridServiceProvider(PropertyGrid owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006D78 RID: 28024 RVA: 0x001917AC File Offset: 0x0018F9AC
			public object GetService(Type serviceType)
			{
				object obj = null;
				if (this.owner.ActiveDesigner != null)
				{
					obj = this.owner.ActiveDesigner.GetService(serviceType);
				}
				if (obj == null)
				{
					obj = this.owner.gridView.GetService(serviceType);
				}
				if (obj == null && this.owner.Site != null)
				{
					obj = this.owner.Site.GetService(serviceType);
				}
				return obj;
			}

			// Token: 0x0400429A RID: 17050
			private PropertyGrid owner;
		}

		// Token: 0x020007D1 RID: 2001
		internal static class MeasureTextHelper
		{
			// Token: 0x06006D79 RID: 28025 RVA: 0x00191812 File Offset: 0x0018FA12
			public static SizeF MeasureText(PropertyGrid owner, Graphics g, string text, Font font)
			{
				return PropertyGrid.MeasureTextHelper.MeasureTextSimple(owner, g, text, font, new SizeF(0f, 0f));
			}

			// Token: 0x06006D7A RID: 28026 RVA: 0x0019182C File Offset: 0x0018FA2C
			public static SizeF MeasureText(PropertyGrid owner, Graphics g, string text, Font font, int width)
			{
				return PropertyGrid.MeasureTextHelper.MeasureText(owner, g, text, font, new SizeF((float)width, 999999f));
			}

			// Token: 0x06006D7B RID: 28027 RVA: 0x00191844 File Offset: 0x0018FA44
			public static SizeF MeasureTextSimple(PropertyGrid owner, Graphics g, string text, Font font, SizeF size)
			{
				SizeF sizeF;
				if (owner.UseCompatibleTextRendering)
				{
					sizeF = g.MeasureString(text, font, size);
				}
				else
				{
					sizeF = TextRenderer.MeasureText(g, text, font, Size.Ceiling(size), PropertyGrid.MeasureTextHelper.GetTextRendererFlags());
				}
				return sizeF;
			}

			// Token: 0x06006D7C RID: 28028 RVA: 0x00191884 File Offset: 0x0018FA84
			public static SizeF MeasureText(PropertyGrid owner, Graphics g, string text, Font font, SizeF size)
			{
				SizeF sizeF;
				if (owner.UseCompatibleTextRendering)
				{
					sizeF = g.MeasureString(text, font, size);
				}
				else
				{
					TextFormatFlags textFormatFlags = PropertyGrid.MeasureTextHelper.GetTextRendererFlags() | TextFormatFlags.LeftAndRightPadding | TextFormatFlags.WordBreak | TextFormatFlags.NoFullWidthCharacterBreak;
					sizeF = TextRenderer.MeasureText(g, text, font, Size.Ceiling(size), textFormatFlags);
				}
				return sizeF;
			}

			// Token: 0x06006D7D RID: 28029 RVA: 0x001918D2 File Offset: 0x0018FAD2
			public static TextFormatFlags GetTextRendererFlags()
			{
				return TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform;
			}
		}
	}
}
