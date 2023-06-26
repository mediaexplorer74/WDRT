using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Provides a menu system for a form.</summary>
	// Token: 0x020002F5 RID: 757
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[SRDescription("DescriptionMenuStrip")]
	public class MenuStrip : ToolStrip
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MenuStrip" /> class.</summary>
		// Token: 0x0600301B RID: 12315 RVA: 0x000D8CB7 File Offset: 0x000D6EB7
		public MenuStrip()
		{
			this.CanOverflow = false;
			this.GripStyle = ToolStripGripStyle.Hidden;
			this.Stretch = true;
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600301C RID: 12316 RVA: 0x000D8CD4 File Offset: 0x000D6ED4
		// (set) Token: 0x0600301D RID: 12317 RVA: 0x000D8CDC File Offset: 0x000D6EDC
		internal override bool KeyboardActive
		{
			get
			{
				return base.KeyboardActive;
			}
			set
			{
				if (base.KeyboardActive != value)
				{
					base.KeyboardActive = value;
					if (value)
					{
						this.OnMenuActivate(EventArgs.Empty);
						return;
					}
					this.OnMenuDeactivate(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuStrip" /> supports overflow functionality.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.MenuStrip" /> supports overflow functionality; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x0600301E RID: 12318 RVA: 0x000D8D08 File Offset: 0x000D6F08
		// (set) Token: 0x0600301F RID: 12319 RVA: 0x000D8D10 File Offset: 0x000D6F10
		[DefaultValue(false)]
		[SRDescription("ToolStripCanOverflowDescr")]
		[SRCategory("CatLayout")]
		[Browsable(false)]
		public new bool CanOverflow
		{
			get
			{
				return base.CanOverflow;
			}
			set
			{
				base.CanOverflow = value;
			}
		}

		/// <summary>Gets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.MenuStrip" /> by default.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06003020 RID: 12320 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected override bool DefaultShowItemToolTips
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the default spacing, in pixels, between the sizing grip and the edges of the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Forms.Padding" /> values representing the spacing, in pixels.</returns>
		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x000D8D19 File Offset: 0x000D6F19
		protected override Padding DefaultGripMargin
		{
			get
			{
				if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					return new Padding(2, 2, 0, 2);
				}
				return DpiHelper.LogicalToDeviceUnits(new Padding(2, 2, 0, 2), base.DeviceDpi);
			}
		}

		/// <summary>Gets the horizontal and vertical dimensions, in pixels, of the <see cref="T:System.Windows.Forms.MenuStrip" /> when it is first created.</summary>
		/// <returns>A <see cref="M:System.Drawing.Point.#ctor(System.Drawing.Size)" /> value representing the <see cref="T:System.Windows.Forms.MenuStrip" /> horizontal and vertical dimensions, in pixels. The default is 200 x 21 pixels.</returns>
		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06003022 RID: 12322 RVA: 0x000D8D40 File Offset: 0x000D6F40
		protected override Size DefaultSize
		{
			get
			{
				if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					return new Size(200, 24);
				}
				return DpiHelper.LogicalToDeviceUnits(new Size(200, 24), base.DeviceDpi);
			}
		}

		/// <summary>Gets the spacing, in pixels, between the left, right, top, and bottom edges of the <see cref="T:System.Windows.Forms.MenuStrip" /> from the edges of the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the spacing. The default is <c>{Left=6, Top=2, Right=0, Bottom=2}</c>.</returns>
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x000D8D70 File Offset: 0x000D6F70
		protected override Padding DefaultPadding
		{
			get
			{
				if (this.GripStyle == ToolStripGripStyle.Visible)
				{
					if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
					{
						return new Padding(3, 2, 0, 2);
					}
					return DpiHelper.LogicalToDeviceUnits(new Padding(3, 2, 0, 2), base.DeviceDpi);
				}
				else
				{
					if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
					{
						return new Padding(6, 2, 0, 2);
					}
					return DpiHelper.LogicalToDeviceUnits(new Padding(6, 2, 0, 2), base.DeviceDpi);
				}
			}
		}

		/// <summary>Gets or sets the visibility of the grip used to reposition the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripGripStyle.Hidden" />.</returns>
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06003024 RID: 12324 RVA: 0x000D8DD1 File Offset: 0x000D6FD1
		// (set) Token: 0x06003025 RID: 12325 RVA: 0x000D8DD9 File Offset: 0x000D6FD9
		[DefaultValue(ToolStripGripStyle.Hidden)]
		public new ToolStripGripStyle GripStyle
		{
			get
			{
				return base.GripStyle;
			}
			set
			{
				base.GripStyle = value;
			}
		}

		/// <summary>Occurs when the user accesses the menu with the keyboard or mouse.</summary>
		// Token: 0x1400022E RID: 558
		// (add) Token: 0x06003026 RID: 12326 RVA: 0x000D8DE2 File Offset: 0x000D6FE2
		// (remove) Token: 0x06003027 RID: 12327 RVA: 0x000D8DF5 File Offset: 0x000D6FF5
		[SRCategory("CatBehavior")]
		[SRDescription("MenuStripMenuActivateDescr")]
		public event EventHandler MenuActivate
		{
			add
			{
				base.Events.AddHandler(MenuStrip.EventMenuActivate, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuStrip.EventMenuActivate, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.MenuStrip" /> is deactivated.</summary>
		// Token: 0x1400022F RID: 559
		// (add) Token: 0x06003028 RID: 12328 RVA: 0x000D8E08 File Offset: 0x000D7008
		// (remove) Token: 0x06003029 RID: 12329 RVA: 0x000D8E1B File Offset: 0x000D701B
		[SRCategory("CatBehavior")]
		[SRDescription("MenuStripMenuDeactivateDescr")]
		public event EventHandler MenuDeactivate
		{
			add
			{
				base.Events.AddHandler(MenuStrip.EventMenuDeactivate, value);
			}
			remove
			{
				base.Events.RemoveHandler(MenuStrip.EventMenuDeactivate, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>
		///   <see langword="true" /> if ToolTips are shown for the <see cref="T:System.Windows.Forms.MenuStrip" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x0600302A RID: 12330 RVA: 0x000D8E2E File Offset: 0x000D702E
		// (set) Token: 0x0600302B RID: 12331 RVA: 0x000D8E36 File Offset: 0x000D7036
		[DefaultValue(false)]
		[SRDescription("ToolStripShowItemToolTipsDescr")]
		[SRCategory("CatBehavior")]
		public new bool ShowItemToolTips
		{
			get
			{
				return base.ShowItemToolTips;
			}
			set
			{
				base.ShowItemToolTips = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuStrip" /> stretches from end to end in its container.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.MenuStrip" /> stretches from end to end in its container; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x0600302C RID: 12332 RVA: 0x000D8E3F File Offset: 0x000D703F
		// (set) Token: 0x0600302D RID: 12333 RVA: 0x000D8E47 File Offset: 0x000D7047
		[DefaultValue(true)]
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripStretchDescr")]
		public new bool Stretch
		{
			get
			{
				return base.Stretch;
			}
			set
			{
				base.Stretch = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> that is used to display a list of Multiple-document interface (MDI) child forms.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> that represents the menu item displaying a list of MDI child forms that are open in the application.</returns>
		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x000D8E50 File Offset: 0x000D7050
		// (set) Token: 0x0600302F RID: 12335 RVA: 0x000D8E58 File Offset: 0x000D7058
		[DefaultValue(null)]
		[MergableProperty(false)]
		[SRDescription("MenuStripMdiWindowListItem")]
		[SRCategory("CatBehavior")]
		[TypeConverter(typeof(MdiWindowListItemConverter))]
		public ToolStripMenuItem MdiWindowListItem
		{
			get
			{
				return this.mdiWindowListItem;
			}
			set
			{
				this.mdiWindowListItem = value;
			}
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06003030 RID: 12336 RVA: 0x000D8E61 File Offset: 0x000D7061
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new MenuStrip.MenuStripAccessibleObject(this);
		}

		/// <summary>Creates a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> with the specified text, image, and event handler on a new <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <param name="text">The text to use for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />. If the <paramref name="text" /> parameter is a hyphen (-), this method creates a <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked.</param>
		/// <returns>A <see cref="M:System.Windows.Forms.ToolStripMenuItem.#ctor(System.String,System.Drawing.Image,System.EventHandler)" />, or a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> if the <paramref name="text" /> parameter is a hyphen (-).</returns>
		// Token: 0x06003031 RID: 12337 RVA: 0x000D8E69 File Offset: 0x000D7069
		protected internal override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
		{
			if (text == "-")
			{
				return new ToolStripSeparator();
			}
			return new ToolStripMenuItem(text, image, onClick);
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x000D8E88 File Offset: 0x000D7088
		internal override ToolStripItem GetNextItem(ToolStripItem start, ArrowDirection direction, bool rtlAware)
		{
			ToolStripItem toolStripItem = base.GetNextItem(start, direction, rtlAware);
			if (toolStripItem is MdiControlStrip.SystemMenuItem && AccessibilityImprovements.Level2)
			{
				toolStripItem = base.GetNextItem(toolStripItem, direction, rtlAware);
			}
			return toolStripItem;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuStrip.MenuActivate" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003033 RID: 12339 RVA: 0x000D8EBC File Offset: 0x000D70BC
		protected virtual void OnMenuActivate(EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.SystemMenuStart, -1);
			}
			EventHandler eventHandler = (EventHandler)base.Events[MenuStrip.EventMenuActivate];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuStrip.MenuDeactivate" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003034 RID: 12340 RVA: 0x000D8EFC File Offset: 0x000D70FC
		protected virtual void OnMenuDeactivate(EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.SystemMenuEnd, -1);
			}
			EventHandler eventHandler = (EventHandler)base.Events[MenuStrip.EventMenuDeactivate];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x000D8F3C File Offset: 0x000D713C
		internal bool OnMenuKey()
		{
			if (!this.Focused && !base.ContainsFocus)
			{
				ToolStripManager.ModalMenuFilter.SetActiveToolStrip(this, true);
				if (this.DisplayedItems.Count > 0)
				{
					if (this.DisplayedItems[0] is MdiControlStrip.SystemMenuItem)
					{
						base.SelectNextToolStripItem(this.DisplayedItems[0], true);
					}
					else
					{
						base.SelectNextToolStripItem(null, this.RightToLeft == RightToLeft.No);
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Processes a command key.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003036 RID: 12342 RVA: 0x000D8FAC File Offset: 0x000D71AC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			if (ToolStripManager.ModalMenuFilter.InMenuMode && keyData == Keys.Space && (this.Focused || !base.ContainsFocus))
			{
				base.NotifySelectionChange(null);
				ToolStripManager.ModalMenuFilter.ExitMenuMode();
				UnsafeNativeMethods.PostMessage(WindowsFormsUtils.GetRootHWnd(this), 274, 61696, 32);
				return true;
			}
			return base.ProcessCmdKey(ref m, keyData);
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06003037 RID: 12343 RVA: 0x000D9004 File Offset: 0x000D7204
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 33 && base.ActiveDropDowns.Count == 0)
			{
				Point point = base.PointToClient(WindowsFormsUtils.LastCursorPoint);
				ToolStripItem itemAt = base.GetItemAt(point);
				if (itemAt != null && !(itemAt is ToolStripControlHost))
				{
					this.KeyboardActive = true;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x040013D2 RID: 5074
		private ToolStripMenuItem mdiWindowListItem;

		// Token: 0x040013D3 RID: 5075
		private static readonly object EventMenuActivate = new object();

		// Token: 0x040013D4 RID: 5076
		private static readonly object EventMenuDeactivate = new object();

		// Token: 0x020006DB RID: 1755
		[ComVisible(true)]
		internal class MenuStripAccessibleObject : ToolStrip.ToolStripAccessibleObject
		{
			// Token: 0x06006AFB RID: 27387 RVA: 0x0018BC34 File Offset: 0x00189E34
			public MenuStripAccessibleObject(MenuStrip owner)
				: base(owner)
			{
			}

			// Token: 0x1700173A RID: 5946
			// (get) Token: 0x06006AFC RID: 27388 RVA: 0x0018BC40 File Offset: 0x00189E40
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.MenuBar;
				}
			}

			// Token: 0x06006AFD RID: 27389 RVA: 0x0018BC60 File Offset: 0x00189E60
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3 && propertyID == 30003)
				{
					return 50010;
				}
				return base.GetPropertyValue(propertyID);
			}
		}
	}
}
