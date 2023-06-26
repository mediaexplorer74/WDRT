using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a selectable option displayed on a <see cref="T:System.Windows.Forms.MenuStrip" /> or <see cref="T:System.Windows.Forms.ContextMenuStrip" />. Although <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.MenuItem" /> control of previous versions, <see cref="T:System.Windows.Forms.MenuItem" /> is retained for both backward compatibility and future use if you choose.</summary>
	// Token: 0x020003E5 RID: 997
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	[DesignerSerializer("System.Windows.Forms.Design.ToolStripMenuItemCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ToolStripMenuItem : ToolStripDropDownItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class.</summary>
		// Token: 0x060043D6 RID: 17366 RVA: 0x0011ED44 File Offset: 0x0011CF44
		public ToolStripMenuItem()
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text.</summary>
		/// <param name="text">The text to display on the menu item.</param>
		// Token: 0x060043D7 RID: 17367 RVA: 0x0011EDB0 File Offset: 0x0011CFB0
		public ToolStripMenuItem(string text)
			: base(text, null, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
		// Token: 0x060043D8 RID: 17368 RVA: 0x0011EE1C File Offset: 0x0011D01C
		public ToolStripMenuItem(Image image)
			: base(null, image, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text and image.</summary>
		/// <param name="text">The text to display on the menu item.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
		// Token: 0x060043D9 RID: 17369 RVA: 0x0011EE88 File Offset: 0x0011D088
		public ToolStripMenuItem(string text, Image image)
			: base(text, image, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text and image and that does the specified action when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked.</summary>
		/// <param name="text">The text to display on the menu item.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the control is clicked.</param>
		// Token: 0x060043DA RID: 17370 RVA: 0x0011EEF4 File Offset: 0x0011D0F4
		public ToolStripMenuItem(string text, Image image, EventHandler onClick)
			: base(text, image, onClick)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class with the specified name that displays the specified text and image that does the specified action when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked.</summary>
		/// <param name="text">The text to display on the menu item.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the control is clicked.</param>
		/// <param name="name">The name of the menu item.</param>
		// Token: 0x060043DB RID: 17371 RVA: 0x0011EF60 File Offset: 0x0011D160
		public ToolStripMenuItem(string text, Image image, EventHandler onClick, string name)
			: base(text, image, onClick, name)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text and image and that contains the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> collection.</summary>
		/// <param name="text">The text to display on the menu item.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
		/// <param name="dropDownItems">The menu items to display when the control is clicked.</param>
		// Token: 0x060043DC RID: 17372 RVA: 0x0011EFD0 File Offset: 0x0011D1D0
		public ToolStripMenuItem(string text, Image image, params ToolStripItem[] dropDownItems)
			: base(text, image, dropDownItems)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> class that displays the specified text and image, does the specified action when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked, and displays the specified shortcut keys.</summary>
		/// <param name="text">The text to display on the menu item.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the control.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the control is clicked.</param>
		/// <param name="shortcutKeys">One of the values of <see cref="T:System.Windows.Forms.Keys" /> that represents the shortcut key for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</param>
		// Token: 0x060043DD RID: 17373 RVA: 0x0011F03C File Offset: 0x0011D23C
		public ToolStripMenuItem(string text, Image image, EventHandler onClick, Keys shortcutKeys)
			: base(text, image, onClick)
		{
			this.Initialize();
			this.ShortcutKeys = shortcutKeys;
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x0011F0B0 File Offset: 0x0011D2B0
		internal ToolStripMenuItem(Form mdiForm)
		{
			this.Initialize();
			base.Properties.SetObject(ToolStripMenuItem.PropMdiForm, mdiForm);
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x0011F12C File Offset: 0x0011D32C
		internal ToolStripMenuItem(IntPtr hMenu, int nativeMenuCommandId, IWin32Window targetWindow)
		{
			this.Initialize();
			this.Overflow = ToolStripItemOverflow.Never;
			this.nativeMenuCommandID = nativeMenuCommandId;
			this.targetWindowHandle = Control.GetSafeHandle(targetWindow);
			this.nativeMenuHandle = hMenu;
			this.Image = this.GetNativeMenuItemImage();
			base.ImageScaling = ToolStripItemImageScaling.None;
			string nativeMenuItemTextAndShortcut = this.GetNativeMenuItemTextAndShortcut();
			if (nativeMenuItemTextAndShortcut != null)
			{
				string[] array = nativeMenuItemTextAndShortcut.Split(new char[] { '\t' });
				if (array.Length >= 1)
				{
					this.Text = array[0];
				}
				if (array.Length >= 2)
				{
					this.ShowShortcutKeys = true;
					this.ShortcutKeyDisplayString = array[1];
				}
			}
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x0011F20A File Offset: 0x0011D40A
		internal override void AutoHide(ToolStripItem otherItemBeingSelected)
		{
			if (base.IsOnDropDown)
			{
				ToolStripMenuItem.MenuTimer.Transition(this, otherItemBeingSelected as ToolStripMenuItem);
				return;
			}
			base.AutoHide(otherItemBeingSelected);
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x0011F22D File Offset: 0x0011D42D
		private void ClearShortcutCache()
		{
			this.cachedShortcutSize = Size.Empty;
			this.cachedShortcutText = null;
		}

		/// <summary>Creates a generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.</summary>
		/// <returns>A generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which can be defined.</returns>
		// Token: 0x060043E2 RID: 17378 RVA: 0x00114874 File Offset: 0x00112A74
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripDropDownMenu(this, true);
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x0011F241 File Offset: 0x0011D441
		internal override ToolStripItemInternalLayout CreateInternalLayout()
		{
			return new ToolStripMenuItemInternalLayout(this);
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</returns>
		// Token: 0x060043E4 RID: 17380 RVA: 0x0011F249 File Offset: 0x0011D449
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripMenuItem.ToolStripMenuItemAccessibleObject(this);
		}

		// Token: 0x060043E5 RID: 17381 RVA: 0x0011F254 File Offset: 0x0011D454
		private void Initialize()
		{
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledDefaultPadding = DpiHelper.LogicalToDeviceUnits(ToolStripMenuItem.defaultPadding, 0);
				this.scaledDefaultDropDownPadding = DpiHelper.LogicalToDeviceUnits(ToolStripMenuItem.defaultDropDownPadding, 0);
				this.scaledCheckMarkBitmapSize = DpiHelper.LogicalToDeviceUnits(ToolStripMenuItem.checkMarkBitmapSize, 0);
			}
			this.Overflow = ToolStripItemOverflow.Never;
			base.MouseDownAndUpMustBeInSameItem = false;
			base.SupportsDisabledHotTracking = true;
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />, measured in pixels. The default is 100 pixels horizontally.</returns>
		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x060043E6 RID: 17382 RVA: 0x0011F2B0 File Offset: 0x0011D4B0
		protected override Size DefaultSize
		{
			get
			{
				if (!DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements)
				{
					return new Size(32, 19);
				}
				return DpiHelper.LogicalToDeviceUnits(new Size(32, 19), this.DeviceDpi);
			}
		}

		/// <summary>Gets the spacing between the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> and an adjacent item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x060043E7 RID: 17383 RVA: 0x00019A61 File Offset: 0x00017C61
		protected internal override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		/// <summary>Gets the internal spacing within the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x060043E8 RID: 17384 RVA: 0x0011F2D7 File Offset: 0x0011D4D7
		protected override Padding DefaultPadding
		{
			get
			{
				if (base.IsOnDropDown)
				{
					return this.scaledDefaultDropDownPadding;
				}
				return this.scaledDefaultPadding;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the control is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x060043E9 RID: 17385 RVA: 0x0011F2F0 File Offset: 0x0011D4F0
		// (set) Token: 0x060043EA RID: 17386 RVA: 0x0011F341 File Offset: 0x0011D541
		public override bool Enabled
		{
			get
			{
				if (this.nativeMenuCommandID != -1)
				{
					return base.Enabled && this.nativeMenuHandle != IntPtr.Zero && this.targetWindowHandle != IntPtr.Zero && this.GetNativeMenuItemEnabled();
				}
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is checked.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is checked or is in an indeterminate state; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x060043EB RID: 17387 RVA: 0x0011F34A File Offset: 0x0011D54A
		// (set) Token: 0x060043EC RID: 17388 RVA: 0x0011F355 File Offset: 0x0011D555
		[Bindable(true)]
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("CheckBoxCheckedDescr")]
		public bool Checked
		{
			get
			{
				return this.CheckState > CheckState.Unchecked;
			}
			set
			{
				if (value != this.Checked)
				{
					this.CheckState = (value ? CheckState.Checked : CheckState.Unchecked);
					base.InvokePaint();
				}
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x060043ED RID: 17389 RVA: 0x0011F374 File Offset: 0x0011D574
		internal Image CheckedImage
		{
			get
			{
				CheckState checkState = this.CheckState;
				if (checkState == CheckState.Indeterminate)
				{
					if (ToolStripMenuItem.indeterminateCheckedImage == null)
					{
						if (DpiHelper.EnableToolStripHighDpiImprovements)
						{
							ToolStripMenuItem.indeterminateCheckedImage = ToolStripMenuItem.GetBitmapFromIcon("IndeterminateChecked.ico", this.scaledCheckMarkBitmapSize);
						}
						else
						{
							Bitmap bitmap = new Bitmap(typeof(ToolStripMenuItem), "IndeterminateChecked.bmp");
							if (bitmap != null)
							{
								bitmap.MakeTransparent(bitmap.GetPixel(1, 1));
								if (DpiHelper.IsScalingRequired)
								{
									DpiHelper.ScaleBitmapLogicalToDevice(ref bitmap, 0);
								}
								ToolStripMenuItem.indeterminateCheckedImage = bitmap;
							}
						}
					}
					return ToolStripMenuItem.indeterminateCheckedImage;
				}
				if (checkState == CheckState.Checked)
				{
					if (ToolStripMenuItem.checkedImage == null)
					{
						if (DpiHelper.EnableToolStripHighDpiImprovements)
						{
							ToolStripMenuItem.checkedImage = ToolStripMenuItem.GetBitmapFromIcon("Checked.ico", this.scaledCheckMarkBitmapSize);
						}
						else
						{
							Bitmap bitmap2 = new Bitmap(typeof(ToolStripMenuItem), "Checked.bmp");
							if (bitmap2 != null)
							{
								bitmap2.MakeTransparent(bitmap2.GetPixel(1, 1));
								if (DpiHelper.IsScalingRequired)
								{
									DpiHelper.ScaleBitmapLogicalToDevice(ref bitmap2, 0);
								}
								ToolStripMenuItem.checkedImage = bitmap2;
							}
						}
					}
					return ToolStripMenuItem.checkedImage;
				}
				return null;
			}
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x0011F460 File Offset: 0x0011D660
		private static Bitmap GetBitmapFromIcon(string iconName, Size desiredIconSize)
		{
			Bitmap bitmap = null;
			Icon icon = new Icon(typeof(ToolStripMenuItem), iconName);
			if (icon != null)
			{
				Icon icon2 = new Icon(icon, desiredIconSize);
				if (icon2 != null)
				{
					try
					{
						bitmap = icon2.ToBitmap();
						if (bitmap != null)
						{
							bitmap.MakeTransparent(bitmap.GetPixel(1, 1));
							if (DpiHelper.IsScalingRequired && (bitmap.Size.Width != desiredIconSize.Width || bitmap.Size.Height != desiredIconSize.Height))
							{
								Bitmap bitmap2 = DpiHelper.CreateResizedBitmap(bitmap, desiredIconSize);
								if (bitmap2 != null)
								{
									bitmap.Dispose();
									bitmap = bitmap2;
								}
							}
						}
					}
					finally
					{
						icon.Dispose();
						icon2.Dispose();
					}
				}
			}
			return bitmap;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> should automatically appear checked and unchecked when clicked.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> should automatically appear checked when clicked; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x060043EF RID: 17391 RVA: 0x0011F514 File Offset: 0x0011D714
		// (set) Token: 0x060043F0 RID: 17392 RVA: 0x0011F51C File Offset: 0x0011D71C
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripButtonCheckOnClickDescr")]
		public bool CheckOnClick
		{
			get
			{
				return this.checkOnClick;
			}
			set
			{
				this.checkOnClick = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is in the checked, unchecked, or indeterminate state.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> values. The default is <see langword="Unchecked" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <see cref="P:System.Windows.Forms.ToolStripMenuItem.CheckState" /> property is not set to one of the <see cref="T:System.Windows.Forms.CheckState" /> values.</exception>
		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x060043F1 RID: 17393 RVA: 0x0011F528 File Offset: 0x0011D728
		// (set) Token: 0x060043F2 RID: 17394 RVA: 0x0011F55C File Offset: 0x0011D75C
		[Bindable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(CheckState.Unchecked)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("CheckBoxCheckStateDescr")]
		public CheckState CheckState
		{
			get
			{
				bool flag = false;
				object obj = base.Properties.GetInteger(ToolStripMenuItem.PropCheckState, out flag);
				if (!flag)
				{
					return CheckState.Unchecked;
				}
				return (CheckState)obj;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(CheckState));
				}
				if (value != this.CheckState)
				{
					base.Properties.SetInteger(ToolStripMenuItem.PropCheckState, (int)value);
					this.OnCheckedChanged(EventArgs.Empty);
					this.OnCheckStateChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripMenuItem.Checked" /> property changes.</summary>
		// Token: 0x14000354 RID: 852
		// (add) Token: 0x060043F3 RID: 17395 RVA: 0x0011F5BF File Offset: 0x0011D7BF
		// (remove) Token: 0x060043F4 RID: 17396 RVA: 0x0011F5D2 File Offset: 0x0011D7D2
		[SRDescription("CheckBoxOnCheckedChangedDescr")]
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripMenuItem.EventCheckedChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripMenuItem.EventCheckedChanged, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripMenuItem.CheckState" /> property changes.</summary>
		// Token: 0x14000355 RID: 853
		// (add) Token: 0x060043F5 RID: 17397 RVA: 0x0011F5E5 File Offset: 0x0011D7E5
		// (remove) Token: 0x060043F6 RID: 17398 RVA: 0x0011F5F8 File Offset: 0x0011D7F8
		[SRDescription("CheckBoxOnCheckStateChangedDescr")]
		public event EventHandler CheckStateChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripMenuItem.EventCheckStateChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripMenuItem.EventCheckStateChanged, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is attached to the <see cref="T:System.Windows.Forms.ToolStrip" /> or the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> or whether it can float between the two.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemOverflow" /> values. The default is <see langword="Never" />.</returns>
		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x060043F7 RID: 17399 RVA: 0x0011F60B File Offset: 0x0011D80B
		// (set) Token: 0x060043F8 RID: 17400 RVA: 0x0011F613 File Offset: 0x0011D813
		[DefaultValue(ToolStripItemOverflow.Never)]
		[SRDescription("ToolStripItemOverflowDescr")]
		[SRCategory("CatLayout")]
		public new ToolStripItemOverflow Overflow
		{
			get
			{
				return base.Overflow;
			}
			set
			{
				base.Overflow = value;
			}
		}

		/// <summary>Gets or sets the shortcut keys associated with the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Keys" /> values. The default is <see cref="F:System.Windows.Forms.Keys.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property was not set to one of the <see cref="T:System.Windows.Forms.Keys" /> values.</exception>
		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x060043F9 RID: 17401 RVA: 0x0011F61C File Offset: 0x0011D81C
		// (set) Token: 0x060043FA RID: 17402 RVA: 0x0011F650 File Offset: 0x0011D850
		[Localizable(true)]
		[DefaultValue(Keys.None)]
		[SRDescription("MenuItemShortCutDescr")]
		public Keys ShortcutKeys
		{
			get
			{
				bool flag = false;
				object obj = base.Properties.GetInteger(ToolStripMenuItem.PropShortcutKeys, out flag);
				if (!flag)
				{
					return Keys.None;
				}
				return (Keys)obj;
			}
			set
			{
				if (value != Keys.None && !ToolStripManager.IsValidShortcut(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(Keys));
				}
				Keys shortcutKeys = this.ShortcutKeys;
				if (shortcutKeys != value)
				{
					this.ClearShortcutCache();
					ToolStrip owner = base.Owner;
					if (owner != null)
					{
						if (shortcutKeys != Keys.None)
						{
							owner.Shortcuts.Remove(shortcutKeys);
						}
						if (owner.Shortcuts.Contains(value))
						{
							owner.Shortcuts[value] = this;
						}
						else
						{
							owner.Shortcuts.Add(value, this);
						}
					}
					base.Properties.SetInteger(ToolStripMenuItem.PropShortcutKeys, (int)value);
					if (this.ShowShortcutKeys && base.IsOnDropDown)
					{
						ToolStripDropDownMenu toolStripDropDownMenu = base.GetCurrentParentDropDown() as ToolStripDropDownMenu;
						if (toolStripDropDownMenu != null)
						{
							LayoutTransaction.DoLayout(base.ParentInternal, this, "ShortcutKeys");
							toolStripDropDownMenu.AdjustSize();
						}
					}
				}
			}
		}

		/// <summary>Gets or sets the shortcut key text.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the shortcut key.</returns>
		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x060043FB RID: 17403 RVA: 0x0011F730 File Offset: 0x0011D930
		// (set) Token: 0x060043FC RID: 17404 RVA: 0x0011F738 File Offset: 0x0011D938
		[SRDescription("ToolStripMenuItemShortcutKeyDisplayStringDescr")]
		[SRCategory("CatAppearance")]
		[DefaultValue(null)]
		[Localizable(true)]
		public string ShortcutKeyDisplayString
		{
			get
			{
				return this.shortcutKeyDisplayString;
			}
			set
			{
				if (this.shortcutKeyDisplayString != value)
				{
					this.shortcutKeyDisplayString = value;
					this.ClearShortcutCache();
					if (this.ShowShortcutKeys)
					{
						ToolStripDropDown toolStripDropDown = base.ParentInternal as ToolStripDropDown;
						if (toolStripDropDown != null)
						{
							LayoutTransaction.DoLayout(toolStripDropDown, this, "ShortcutKeyDisplayString");
							toolStripDropDown.AdjustSize();
						}
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the shortcut keys that are associated with the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> are displayed next to the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the shortcut keys are shown; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x060043FD RID: 17405 RVA: 0x0011F789 File Offset: 0x0011D989
		// (set) Token: 0x060043FE RID: 17406 RVA: 0x0011F794 File Offset: 0x0011D994
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("MenuItemShowShortCutDescr")]
		public bool ShowShortcutKeys
		{
			get
			{
				return this.showShortcutKeys;
			}
			set
			{
				if (value != this.showShortcutKeys)
				{
					this.ClearShortcutCache();
					this.showShortcutKeys = value;
					ToolStripDropDown toolStripDropDown = base.ParentInternal as ToolStripDropDown;
					if (toolStripDropDown != null)
					{
						LayoutTransaction.DoLayout(toolStripDropDown, this, "ShortcutKeys");
						toolStripDropDown.AdjustSize();
					}
				}
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x060043FF RID: 17407 RVA: 0x0011F7D8 File Offset: 0x0011D9D8
		internal bool IsTopLevel
		{
			get
			{
				return !(base.ParentInternal is ToolStripDropDown);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> appears on a multiple document interface (MDI) window list.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> appears on a MDI window list; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06004400 RID: 17408 RVA: 0x0011F7E8 File Offset: 0x0011D9E8
		[Browsable(false)]
		public bool IsMdiWindowListEntry
		{
			get
			{
				return this.MdiForm != null;
			}
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x06004401 RID: 17409 RVA: 0x0011F7F3 File Offset: 0x0011D9F3
		internal static MenuTimer MenuTimer
		{
			get
			{
				return ToolStripMenuItem.menuTimer;
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x06004402 RID: 17410 RVA: 0x0011F7FA File Offset: 0x0011D9FA
		internal Form MdiForm
		{
			get
			{
				if (base.Properties.ContainsObject(ToolStripMenuItem.PropMdiForm))
				{
					return base.Properties.GetObject(ToolStripMenuItem.PropMdiForm) as Form;
				}
				return null;
			}
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x0011F828 File Offset: 0x0011DA28
		internal ToolStripMenuItem Clone()
		{
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
			toolStripMenuItem.Events.AddHandlers(base.Events);
			toolStripMenuItem.AccessibleName = base.AccessibleName;
			toolStripMenuItem.AccessibleRole = base.AccessibleRole;
			toolStripMenuItem.Alignment = base.Alignment;
			toolStripMenuItem.AllowDrop = this.AllowDrop;
			toolStripMenuItem.Anchor = base.Anchor;
			toolStripMenuItem.AutoSize = base.AutoSize;
			toolStripMenuItem.AutoToolTip = base.AutoToolTip;
			toolStripMenuItem.BackColor = this.BackColor;
			toolStripMenuItem.BackgroundImage = this.BackgroundImage;
			toolStripMenuItem.BackgroundImageLayout = this.BackgroundImageLayout;
			toolStripMenuItem.Checked = this.Checked;
			toolStripMenuItem.CheckOnClick = this.CheckOnClick;
			toolStripMenuItem.CheckState = this.CheckState;
			toolStripMenuItem.DisplayStyle = this.DisplayStyle;
			toolStripMenuItem.Dock = base.Dock;
			toolStripMenuItem.DoubleClickEnabled = base.DoubleClickEnabled;
			toolStripMenuItem.Enabled = this.Enabled;
			toolStripMenuItem.Font = this.Font;
			toolStripMenuItem.ForeColor = this.ForeColor;
			toolStripMenuItem.Image = this.Image;
			toolStripMenuItem.ImageAlign = base.ImageAlign;
			toolStripMenuItem.ImageScaling = base.ImageScaling;
			toolStripMenuItem.ImageTransparentColor = base.ImageTransparentColor;
			toolStripMenuItem.Margin = base.Margin;
			toolStripMenuItem.MergeAction = base.MergeAction;
			toolStripMenuItem.MergeIndex = base.MergeIndex;
			toolStripMenuItem.Name = base.Name;
			toolStripMenuItem.Overflow = this.Overflow;
			toolStripMenuItem.Padding = this.Padding;
			toolStripMenuItem.RightToLeft = this.RightToLeft;
			toolStripMenuItem.ShortcutKeys = this.ShortcutKeys;
			toolStripMenuItem.ShowShortcutKeys = this.ShowShortcutKeys;
			toolStripMenuItem.Tag = base.Tag;
			toolStripMenuItem.Text = this.Text;
			toolStripMenuItem.TextAlign = this.TextAlign;
			toolStripMenuItem.TextDirection = this.TextDirection;
			toolStripMenuItem.TextImageRelation = base.TextImageRelation;
			toolStripMenuItem.ToolTipText = base.ToolTipText;
			toolStripMenuItem.Visible = ((IArrangedElement)this).ParticipatesInLayout;
			if (!base.AutoSize)
			{
				toolStripMenuItem.Size = this.Size;
			}
			return toolStripMenuItem;
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x06004404 RID: 17412 RVA: 0x00110F47 File Offset: 0x0010F147
		// (set) Token: 0x06004405 RID: 17413 RVA: 0x0011FA35 File Offset: 0x0011DC35
		internal override int DeviceDpi
		{
			get
			{
				return base.DeviceDpi;
			}
			set
			{
				base.DeviceDpi = value;
				this.scaledDefaultPadding = DpiHelper.LogicalToDeviceUnits(ToolStripMenuItem.defaultPadding, value);
				this.scaledDefaultDropDownPadding = DpiHelper.LogicalToDeviceUnits(ToolStripMenuItem.defaultDropDownPadding, value);
				this.scaledCheckMarkBitmapSize = DpiHelper.LogicalToDeviceUnits(ToolStripMenuItem.checkMarkBitmapSize, value);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06004406 RID: 17414 RVA: 0x0011FA74 File Offset: 0x0011DC74
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.lastOwner != null)
			{
				Keys shortcutKeys = this.ShortcutKeys;
				if (shortcutKeys != Keys.None && this.lastOwner.Shortcuts.ContainsKey(shortcutKeys))
				{
					this.lastOwner.Shortcuts.Remove(shortcutKeys);
				}
				this.lastOwner = null;
				if (this.MdiForm != null)
				{
					base.Properties.SetObject(ToolStripMenuItem.PropMdiForm, null);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x0011FAEC File Offset: 0x0011DCEC
		private bool GetNativeMenuItemEnabled()
		{
			if (this.nativeMenuCommandID == -1 || this.nativeMenuHandle == IntPtr.Zero)
			{
				return false;
			}
			NativeMethods.MENUITEMINFO_T_RW menuiteminfo_T_RW = new NativeMethods.MENUITEMINFO_T_RW();
			menuiteminfo_T_RW.cbSize = Marshal.SizeOf(typeof(NativeMethods.MENUITEMINFO_T_RW));
			menuiteminfo_T_RW.fMask = 1;
			menuiteminfo_T_RW.fType = 1;
			menuiteminfo_T_RW.wID = this.nativeMenuCommandID;
			UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(this, this.nativeMenuHandle), this.nativeMenuCommandID, false, menuiteminfo_T_RW);
			return (menuiteminfo_T_RW.fState & 3) == 0;
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x0011FB70 File Offset: 0x0011DD70
		private string GetNativeMenuItemTextAndShortcut()
		{
			if (this.nativeMenuCommandID == -1 || this.nativeMenuHandle == IntPtr.Zero)
			{
				return null;
			}
			string text = null;
			NativeMethods.MENUITEMINFO_T_RW menuiteminfo_T_RW = new NativeMethods.MENUITEMINFO_T_RW();
			menuiteminfo_T_RW.fMask = 64;
			menuiteminfo_T_RW.fType = 64;
			menuiteminfo_T_RW.wID = this.nativeMenuCommandID;
			menuiteminfo_T_RW.dwTypeData = IntPtr.Zero;
			UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(this, this.nativeMenuHandle), this.nativeMenuCommandID, false, menuiteminfo_T_RW);
			if (menuiteminfo_T_RW.cch > 0)
			{
				menuiteminfo_T_RW.cch++;
				menuiteminfo_T_RW.wID = this.nativeMenuCommandID;
				IntPtr intPtr = Marshal.AllocCoTaskMem(menuiteminfo_T_RW.cch * Marshal.SystemDefaultCharSize);
				menuiteminfo_T_RW.dwTypeData = intPtr;
				try
				{
					UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(this, this.nativeMenuHandle), this.nativeMenuCommandID, false, menuiteminfo_T_RW);
					if (menuiteminfo_T_RW.dwTypeData != IntPtr.Zero)
					{
						text = Marshal.PtrToStringAuto(menuiteminfo_T_RW.dwTypeData, menuiteminfo_T_RW.cch);
					}
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(intPtr);
					}
				}
			}
			return text;
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x0011FC88 File Offset: 0x0011DE88
		private Image GetNativeMenuItemImage()
		{
			if (this.nativeMenuCommandID == -1 || this.nativeMenuHandle == IntPtr.Zero)
			{
				return null;
			}
			NativeMethods.MENUITEMINFO_T_RW menuiteminfo_T_RW = new NativeMethods.MENUITEMINFO_T_RW();
			menuiteminfo_T_RW.fMask = 128;
			menuiteminfo_T_RW.fType = 128;
			menuiteminfo_T_RW.wID = this.nativeMenuCommandID;
			UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(this, this.nativeMenuHandle), this.nativeMenuCommandID, false, menuiteminfo_T_RW);
			if (menuiteminfo_T_RW.hbmpItem != IntPtr.Zero && menuiteminfo_T_RW.hbmpItem.ToInt32() > 11)
			{
				return Image.FromHbitmap(menuiteminfo_T_RW.hbmpItem);
			}
			int num = -1;
			switch (menuiteminfo_T_RW.hbmpItem.ToInt32())
			{
			case 2:
			case 9:
				num = 3;
				break;
			case 3:
			case 7:
			case 11:
				num = 1;
				break;
			case 5:
			case 6:
			case 8:
				num = 0;
				break;
			case 10:
				num = 2;
				break;
			}
			if (num > -1)
			{
				Bitmap bitmap = new Bitmap(16, 16);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					ControlPaint.DrawCaptionButton(graphics, new Rectangle(Point.Empty, bitmap.Size), (CaptionButton)num, ButtonState.Flat);
					graphics.DrawRectangle(SystemPens.Control, 0, 0, bitmap.Width - 1, bitmap.Height - 1);
				}
				bitmap.MakeTransparent(SystemColors.Control);
				return bitmap;
			}
			return null;
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x0011FDF8 File Offset: 0x0011DFF8
		internal Size GetShortcutTextSize()
		{
			if (!this.ShowShortcutKeys)
			{
				return Size.Empty;
			}
			string shortcutText = this.GetShortcutText();
			if (string.IsNullOrEmpty(shortcutText))
			{
				return Size.Empty;
			}
			if (this.cachedShortcutSize == Size.Empty)
			{
				this.cachedShortcutSize = TextRenderer.MeasureText(shortcutText, this.Font);
			}
			return this.cachedShortcutSize;
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x0011FE52 File Offset: 0x0011E052
		internal string GetShortcutText()
		{
			if (this.cachedShortcutText == null)
			{
				this.cachedShortcutText = ToolStripMenuItem.ShortcutToText(this.ShortcutKeys, this.ShortcutKeyDisplayString);
			}
			return this.cachedShortcutText;
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x0011FE7C File Offset: 0x0011E07C
		internal void HandleAutoExpansion()
		{
			if (this.Enabled && base.ParentInternal != null && base.ParentInternal.MenuAutoExpand && this.HasDropDownItems)
			{
				base.ShowDropDown();
				if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
				{
					KeyboardToolTipStateMachine.Instance.NotifyAboutLostFocus(this);
				}
				base.DropDown.SelectNextToolStripItem(null, true);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600440D RID: 17421 RVA: 0x0011FED4 File Offset: 0x0011E0D4
		protected override void OnClick(EventArgs e)
		{
			if (this.checkOnClick)
			{
				this.Checked = !this.Checked;
			}
			base.OnClick(e);
			if (this.nativeMenuCommandID != -1)
			{
				if ((this.nativeMenuCommandID & 61440) != 0)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(this, this.targetWindowHandle), 274, this.nativeMenuCommandID, 0);
				}
				else
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(this, this.targetWindowHandle), 273, this.nativeMenuCommandID, 0);
				}
				base.Invalidate();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripMenuItem.CheckedChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600440E RID: 17422 RVA: 0x0011FF5C File Offset: 0x0011E15C
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripMenuItem.EventCheckedChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripMenuItem.CheckStateChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600440F RID: 17423 RVA: 0x0011FF8C File Offset: 0x0011E18C
		protected virtual void OnCheckStateChanged(EventArgs e)
		{
			base.AccessibilityNotifyClients(AccessibleEvents.StateChange);
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripMenuItem.EventCheckStateChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raised in response to the <see cref="M:System.Windows.Forms.ToolStripDropDownItem.HideDropDown" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004410 RID: 17424 RVA: 0x0011FFC5 File Offset: 0x0011E1C5
		protected override void OnDropDownHide(EventArgs e)
		{
			ToolStripMenuItem.MenuTimer.Cancel(this);
			base.OnDropDownHide(e);
		}

		/// <summary>Raised in response to the <see cref="M:System.Windows.Forms.ToolStripDropDownItem.ShowDropDown" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004411 RID: 17425 RVA: 0x0011FFD9 File Offset: 0x0011E1D9
		protected override void OnDropDownShow(EventArgs e)
		{
			ToolStripMenuItem.MenuTimer.Cancel(this);
			if (base.ParentInternal != null)
			{
				base.ParentInternal.MenuAutoExpand = true;
			}
			base.OnDropDownShow(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004412 RID: 17426 RVA: 0x00120001 File Offset: 0x0011E201
		protected override void OnFontChanged(EventArgs e)
		{
			this.ClearShortcutCache();
			base.OnFontChanged(e);
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x00120010 File Offset: 0x0011E210
		internal void OnMenuAutoExpand()
		{
			base.ShowDropDown();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06004414 RID: 17428 RVA: 0x00120018 File Offset: 0x0011E218
		protected override void OnMouseDown(MouseEventArgs e)
		{
			ToolStripMenuItem.MenuTimer.Cancel(this);
			this.OnMouseButtonStateChange(e, true);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06004415 RID: 17429 RVA: 0x0012002D File Offset: 0x0011E22D
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.OnMouseButtonStateChange(e, false);
			base.OnMouseUp(e);
		}

		// Token: 0x06004416 RID: 17430 RVA: 0x00120040 File Offset: 0x0011E240
		private void OnMouseButtonStateChange(MouseEventArgs e, bool isMouseDown)
		{
			bool flag = true;
			if (base.IsOnDropDown)
			{
				ToolStripDropDown currentParentDropDown = base.GetCurrentParentDropDown();
				base.SupportsRightClick = currentParentDropDown.GetFirstDropDown() is ContextMenuStrip;
			}
			else
			{
				flag = !base.DropDown.Visible;
				base.SupportsRightClick = false;
			}
			if (e.Button == MouseButtons.Left || (e.Button == MouseButtons.Right && base.SupportsRightClick))
			{
				if (isMouseDown && flag)
				{
					this.openMouseId = ((base.ParentInternal == null) ? 0 : base.ParentInternal.GetMouseId());
					base.ShowDropDown(true);
					return;
				}
				if (!isMouseDown && !flag)
				{
					byte b = ((base.ParentInternal == null) ? 0 : base.ParentInternal.GetMouseId());
					int num = (int)this.openMouseId;
					if ((int)b != num)
					{
						this.openMouseId = 0;
						ToolStripManager.ModalMenuFilter.CloseActiveDropDown(base.DropDown, ToolStripDropDownCloseReason.AppClicked);
						base.Select();
					}
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004417 RID: 17431 RVA: 0x00120116 File Offset: 0x0011E316
		protected override void OnMouseEnter(EventArgs e)
		{
			if (base.ParentInternal != null && base.ParentInternal.MenuAutoExpand && this.Selected)
			{
				ToolStripMenuItem.MenuTimer.Cancel(this);
				ToolStripMenuItem.MenuTimer.Start(this);
			}
			base.OnMouseEnter(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004418 RID: 17432 RVA: 0x00120152 File Offset: 0x0011E352
		protected override void OnMouseLeave(EventArgs e)
		{
			ToolStripMenuItem.MenuTimer.Cancel(this);
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.OwnerChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004419 RID: 17433 RVA: 0x00120168 File Offset: 0x0011E368
		protected override void OnOwnerChanged(EventArgs e)
		{
			Keys shortcutKeys = this.ShortcutKeys;
			if (shortcutKeys != Keys.None)
			{
				if (this.lastOwner != null)
				{
					this.lastOwner.Shortcuts.Remove(shortcutKeys);
				}
				if (base.Owner != null)
				{
					if (base.Owner.Shortcuts.Contains(shortcutKeys))
					{
						base.Owner.Shortcuts[shortcutKeys] = this;
					}
					else
					{
						base.Owner.Shortcuts.Add(shortcutKeys, this);
					}
					this.lastOwner = base.Owner;
				}
			}
			base.OnOwnerChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600441A RID: 17434 RVA: 0x00120200 File Offset: 0x0011E400
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.Owner != null)
			{
				ToolStripRenderer renderer = base.Renderer;
				Graphics graphics = e.Graphics;
				renderer.DrawMenuItemBackground(new ToolStripItemRenderEventArgs(graphics, this));
				Color color = SystemColors.MenuText;
				if (base.IsForeColorSet)
				{
					color = this.ForeColor;
				}
				else if (!this.IsTopLevel || ToolStripManager.VisualStylesEnabled)
				{
					if (this.Selected || this.Pressed)
					{
						color = SystemColors.HighlightText;
					}
					else
					{
						color = SystemColors.MenuText;
					}
				}
				bool flag = this.RightToLeft == RightToLeft.Yes;
				ToolStripMenuItemInternalLayout toolStripMenuItemInternalLayout = base.InternalLayout as ToolStripMenuItemInternalLayout;
				if (toolStripMenuItemInternalLayout != null && toolStripMenuItemInternalLayout.UseMenuLayout)
				{
					if (this.CheckState != CheckState.Unchecked && toolStripMenuItemInternalLayout.PaintCheck)
					{
						Rectangle rectangle = toolStripMenuItemInternalLayout.CheckRectangle;
						if (!toolStripMenuItemInternalLayout.ShowCheckMargin)
						{
							rectangle = toolStripMenuItemInternalLayout.ImageRectangle;
						}
						if (rectangle.Width != 0)
						{
							renderer.DrawItemCheck(new ToolStripItemImageRenderEventArgs(graphics, this, this.CheckedImage, rectangle));
						}
					}
					if ((this.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
					{
						renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(graphics, this, this.Text, base.InternalLayout.TextRectangle, color, this.Font, flag ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft));
						bool flag2 = this.ShowShortcutKeys;
						if (!base.DesignMode)
						{
							flag2 = flag2 && !this.HasDropDownItems;
						}
						if (flag2)
						{
							renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(graphics, this, this.GetShortcutText(), base.InternalLayout.TextRectangle, color, this.Font, flag ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleRight));
						}
					}
					if (this.HasDropDownItems)
					{
						ArrowDirection arrowDirection = (flag ? ArrowDirection.Left : ArrowDirection.Right);
						Color color2 = ((this.Selected || this.Pressed) ? SystemColors.HighlightText : SystemColors.MenuText);
						color2 = (this.Enabled ? color2 : SystemColors.ControlDark);
						renderer.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, this, toolStripMenuItemInternalLayout.ArrowRectangle, color2, arrowDirection));
					}
					if (toolStripMenuItemInternalLayout.PaintImage && (this.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image && this.Image != null)
					{
						renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(graphics, this, base.InternalLayout.ImageRectangle));
						return;
					}
				}
				else
				{
					if ((this.DisplayStyle & ToolStripItemDisplayStyle.Text) == ToolStripItemDisplayStyle.Text)
					{
						renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(graphics, this, this.Text, base.InternalLayout.TextRectangle, color, this.Font, base.InternalLayout.TextFormat));
					}
					if ((this.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image && this.Image != null)
					{
						renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(graphics, this, base.InternalLayout.ImageRectangle));
					}
				}
			}
		}

		/// <summary>Processes a command key.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, which represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600441B RID: 17435 RVA: 0x00120477 File Offset: 0x0011E677
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected internal override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			if (this.Enabled && this.ShortcutKeys == keyData && !this.HasDropDownItems)
			{
				base.FireEvent(ToolStripItemEventType.Click);
				return true;
			}
			return base.ProcessCmdKey(ref m, keyData);
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed as a mnemonic by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600441C RID: 17436 RVA: 0x001204A3 File Offset: 0x0011E6A3
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (this.HasDropDownItems)
			{
				base.Select();
				base.ShowDropDown();
				if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
				{
					KeyboardToolTipStateMachine.Instance.NotifyAboutLostFocus(this);
				}
				base.DropDown.SelectNextToolStripItem(null, true);
				return true;
			}
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</param>
		// Token: 0x0600441D RID: 17437 RVA: 0x001204E4 File Offset: 0x0011E6E4
		protected internal override void SetBounds(Rectangle rect)
		{
			ToolStripMenuItemInternalLayout toolStripMenuItemInternalLayout = base.InternalLayout as ToolStripMenuItemInternalLayout;
			if (toolStripMenuItemInternalLayout != null && toolStripMenuItemInternalLayout.UseMenuLayout)
			{
				ToolStripDropDownMenu toolStripDropDownMenu = base.Owner as ToolStripDropDownMenu;
				if (toolStripDropDownMenu != null)
				{
					rect.X -= toolStripDropDownMenu.Padding.Left;
					rect.X = Math.Max(rect.X, 0);
				}
			}
			base.SetBounds(rect);
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x0012054E File Offset: 0x0011E74E
		internal void SetNativeTargetWindow(IWin32Window window)
		{
			this.targetWindowHandle = Control.GetSafeHandle(window);
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x0012055C File Offset: 0x0011E75C
		internal void SetNativeTargetMenu(IntPtr hMenu)
		{
			this.nativeMenuHandle = hMenu;
		}

		// Token: 0x06004420 RID: 17440 RVA: 0x00120565 File Offset: 0x0011E765
		internal static string ShortcutToText(Keys shortcutKeys, string shortcutKeyDisplayString)
		{
			if (!string.IsNullOrEmpty(shortcutKeyDisplayString))
			{
				return shortcutKeyDisplayString;
			}
			if (shortcutKeys == Keys.None)
			{
				return string.Empty;
			}
			return TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(shortcutKeys);
		}

		// Token: 0x06004421 RID: 17441 RVA: 0x00120594 File Offset: 0x0011E794
		internal override bool IsBeingTabbedTo()
		{
			return base.IsBeingTabbedTo() || ToolStripManager.ModalMenuFilter.InMenuMode;
		}

		// Token: 0x040025F3 RID: 9715
		private static MenuTimer menuTimer = new MenuTimer();

		// Token: 0x040025F4 RID: 9716
		private static readonly int PropShortcutKeys = PropertyStore.CreateKey();

		// Token: 0x040025F5 RID: 9717
		private static readonly int PropCheckState = PropertyStore.CreateKey();

		// Token: 0x040025F6 RID: 9718
		private static readonly int PropMdiForm = PropertyStore.CreateKey();

		// Token: 0x040025F7 RID: 9719
		private bool checkOnClick;

		// Token: 0x040025F8 RID: 9720
		private bool showShortcutKeys = true;

		// Token: 0x040025F9 RID: 9721
		private ToolStrip lastOwner;

		// Token: 0x040025FA RID: 9722
		private int nativeMenuCommandID = -1;

		// Token: 0x040025FB RID: 9723
		private IntPtr targetWindowHandle = IntPtr.Zero;

		// Token: 0x040025FC RID: 9724
		private IntPtr nativeMenuHandle = IntPtr.Zero;

		// Token: 0x040025FD RID: 9725
		[ThreadStatic]
		private static Image indeterminateCheckedImage;

		// Token: 0x040025FE RID: 9726
		[ThreadStatic]
		private static Image checkedImage;

		// Token: 0x040025FF RID: 9727
		private string shortcutKeyDisplayString;

		// Token: 0x04002600 RID: 9728
		private string cachedShortcutText;

		// Token: 0x04002601 RID: 9729
		private Size cachedShortcutSize = Size.Empty;

		// Token: 0x04002602 RID: 9730
		private static readonly Padding defaultPadding = new Padding(4, 0, 4, 0);

		// Token: 0x04002603 RID: 9731
		private static readonly Padding defaultDropDownPadding = new Padding(0, 1, 0, 1);

		// Token: 0x04002604 RID: 9732
		private static readonly Size checkMarkBitmapSize = new Size(16, 16);

		// Token: 0x04002605 RID: 9733
		private Padding scaledDefaultPadding = ToolStripMenuItem.defaultPadding;

		// Token: 0x04002606 RID: 9734
		private Padding scaledDefaultDropDownPadding = ToolStripMenuItem.defaultDropDownPadding;

		// Token: 0x04002607 RID: 9735
		private Size scaledCheckMarkBitmapSize = ToolStripMenuItem.checkMarkBitmapSize;

		// Token: 0x04002608 RID: 9736
		private byte openMouseId;

		// Token: 0x04002609 RID: 9737
		private static readonly object EventCheckedChanged = new object();

		// Token: 0x0400260A RID: 9738
		private static readonly object EventCheckStateChanged = new object();

		// Token: 0x02000809 RID: 2057
		[ComVisible(true)]
		internal class ToolStripMenuItemAccessibleObject : ToolStripDropDownItemAccessibleObject
		{
			// Token: 0x06006F0D RID: 28429 RVA: 0x00196DC4 File Offset: 0x00194FC4
			public ToolStripMenuItemAccessibleObject(ToolStripMenuItem ownerItem)
				: base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x1700184F RID: 6223
			// (get) Token: 0x06006F0E RID: 28430 RVA: 0x00196DD4 File Offset: 0x00194FD4
			public override AccessibleStates State
			{
				get
				{
					if (this.ownerItem.Enabled)
					{
						AccessibleStates accessibleStates = base.State;
						if ((accessibleStates & AccessibleStates.Pressed) == AccessibleStates.Pressed)
						{
							accessibleStates &= ~AccessibleStates.Pressed;
						}
						if (this.ownerItem.Checked)
						{
							accessibleStates |= AccessibleStates.Checked;
						}
						return accessibleStates;
					}
					return base.State;
				}
			}

			// Token: 0x06006F0F RID: 28431 RVA: 0x00196E1A File Offset: 0x0019501A
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50011;
				}
				if (AccessibilityImprovements.Level2 && propertyID == 30006)
				{
					return this.ownerItem.GetShortcutText();
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x04004310 RID: 17168
			private ToolStripMenuItem ownerItem;
		}
	}
}
