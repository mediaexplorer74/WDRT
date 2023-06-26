using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms.ComponentModel.Com2Interop;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Wraps ActiveX controls and exposes them as fully featured Windows Forms controls.</summary>
	// Token: 0x0200012C RID: 300
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultEvent("Enter")]
	[Designer("System.Windows.Forms.Design.AxHostDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class AxHost : Control, ISupportInitialize, ICustomTypeDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost" /> class, wrapping the ActiveX control indicated by the specified CLSID.</summary>
		/// <param name="clsid">The CLSID of the ActiveX control to wrap.</param>
		// Token: 0x06000996 RID: 2454 RVA: 0x00019E68 File Offset: 0x00018068
		protected AxHost(string clsid)
			: this(clsid, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost" /> class, wrapping the ActiveX control indicated by the specified CLSID, and using the shortcut-menu behavior indicated by the specified <paramref name="flags" /> value.</summary>
		/// <param name="clsid">The CLSID of the ActiveX control to wrap.</param>
		/// <param name="flags">An <see cref="T:System.Int32" /> that modifies the shortcut-menu behavior for the control.</param>
		// Token: 0x06000997 RID: 2455 RVA: 0x00019E74 File Offset: 0x00018074
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		protected AxHost(string clsid, int flags)
		{
			if (Application.OleRequired() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.GetString("AXMTAThread", new object[] { clsid }));
			}
			this.oleSite = new AxHost.OleInterfaces(this);
			this.selectionChangeHandler = new EventHandler(this.OnNewSelection);
			this.clsid = new Guid(clsid);
			this.flags = flags;
			this.axState[AxHost.assignUniqueID] = !base.GetType().GUID.Equals(AxHost.comctlImageCombo_Clsid);
			this.axState[AxHost.needLicenseKey] = true;
			this.axState[AxHost.rejectSelection] = true;
			this.isMaskEdit = this.clsid.Equals(AxHost.maskEdit_Clsid);
			this.onContainerVisibleChanged = new EventHandler(this.OnContainerVisibleChanged);
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x00019F90 File Offset: 0x00018190
		private bool CanUIActivate
		{
			get
			{
				return this.IsUserMode() || this.editMode != 0;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00019FA8 File Offset: 0x000181A8
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				if (this.axState[AxHost.fOwnWindow] && this.IsUserMode())
				{
					createParams.Style &= -268435457;
				}
				return createParams;
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00019FE9 File Offset: 0x000181E9
		private bool GetAxState(int mask)
		{
			return this.axState[mask];
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00019FF7 File Offset: 0x000181F7
		private void SetAxState(int mask, bool value)
		{
			this.axState[mask] = value;
		}

		/// <summary>When overridden in a derived class, attaches interfaces to the underlying ActiveX control.</summary>
		// Token: 0x0600099C RID: 2460 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void AttachInterfaces()
		{
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001A008 File Offset: 0x00018208
		private void RealizeStyles()
		{
			base.SetStyle(ControlStyles.UserPaint, false);
			int num = 0;
			int miscStatus = this.GetOleObject().GetMiscStatus(1, out num);
			if (!NativeMethods.Failed(miscStatus))
			{
				this.miscStatusBits = num;
				this.ParseMiscBits(this.miscStatusBits);
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A Color that represents the background color of the control.</returns>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0001A049 File Offset: 0x00018249
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x00012D84 File Offset: 0x00010F84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The background image displayed in the control.</returns>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00011884 File Offset: 0x0000FA84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x000118A7 File Offset: 0x0000FAA7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImeMode" /> value.</returns>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0001A051 File Offset: 0x00018251
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x0001A059 File Offset: 0x00018259
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060009A6 RID: 2470 RVA: 0x0001A062 File Offset: 0x00018262
		// (remove) Token: 0x060009A7 RID: 2471 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseClick
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseClick" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060009A8 RID: 2472 RVA: 0x0001A081 File Offset: 0x00018281
		// (remove) Token: 0x060009A9 RID: 2473 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseDoubleClick
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseDoubleClick" }));
			}
			remove
			{
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The foreground color of the control.</returns>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0001A0A0 File Offset: 0x000182A0
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x0001A0A8 File Offset: 0x000182A8
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

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The shortcut menu associated with the control.</returns>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00011919 File Offset: 0x0000FB19
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x0001A0B1 File Offset: 0x000182B1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ContextMenu ContextMenu
		{
			get
			{
				return base.ContextMenu;
			}
			set
			{
				base.ContextMenu = value;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0001A0BA File Offset: 0x000182BA
		protected override Size DefaultSize
		{
			get
			{
				return new Size(75, 23);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value.</returns>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0001A0C5 File Offset: 0x000182C5
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0001A0CD File Offset: 0x000182CD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new virtual bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The font of the text displayed by the control.</returns>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0001A0D6 File Offset: 0x000182D6
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0001A0DE File Offset: 0x000182DE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The foreground color of the control.</returns>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0001A0E7 File Offset: 0x000182E7
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x00013024 File Offset: 0x00011224
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

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value.</returns>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0001A0F0 File Offset: 0x000182F0
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x0001A108 File Offset: 0x00018308
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Localizable(true)]
		public new virtual bool RightToLeft
		{
			get
			{
				RightToLeft rightToLeft = base.RightToLeft;
				return rightToLeft == System.Windows.Forms.RightToLeft.Yes;
			}
			set
			{
				base.RightToLeft = (value ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.No);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0001A117 File Offset: 0x00018317
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x0001A11F File Offset: 0x0001831F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0001A128 File Offset: 0x00018328
		internal override bool CanAccessProperties
		{
			get
			{
				int num = this.GetOcState();
				return (this.axState[AxHost.fOwnWindow] && (num > 2 || (this.IsUserMode() && num >= 2))) || num >= 4;
			}
		}

		/// <summary>Returns a value that indicates whether the hosted control is in a state in which its properties can be accessed.</summary>
		/// <returns>
		///   <see langword="true" /> if the properties of the hosted control can be accessed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060009BA RID: 2490 RVA: 0x0001A167 File Offset: 0x00018367
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected bool PropsValid()
		{
			return this.CanAccessProperties;
		}

		/// <summary>Begins the initialization of the ActiveX control.</summary>
		// Token: 0x060009BB RID: 2491 RVA: 0x000070A6 File Offset: 0x000052A6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void BeginInit()
		{
		}

		/// <summary>Ends the initialization of an ActiveX control.</summary>
		// Token: 0x060009BC RID: 2492 RVA: 0x0001A170 File Offset: 0x00018370
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndInit()
		{
			if (this.ParentInternal != null)
			{
				this.ParentInternal.CreateControl(true);
				ContainerControl containerControl = this.ContainingControl;
				if (containerControl != null)
				{
					containerControl.VisibleChanged += this.onContainerVisibleChanged;
				}
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001A1A8 File Offset: 0x000183A8
		private void OnContainerVisibleChanged(object sender, EventArgs e)
		{
			ContainerControl containerControl = this.ContainingControl;
			if (containerControl != null)
			{
				if (containerControl.Visible && base.Visible && !this.axState[AxHost.fOwnWindow])
				{
					this.MakeVisibleWithShow();
					return;
				}
				if (!containerControl.Visible && base.Visible && base.IsHandleCreated && this.GetOcState() >= 4)
				{
					this.HideAxControl();
					return;
				}
				if (containerControl.Visible && !base.GetState(2) && base.IsHandleCreated && this.GetOcState() >= 4)
				{
					this.HideAxControl();
				}
			}
		}

		/// <summary>Returns a value that indicates whether the hosted control is in edit mode.</summary>
		/// <returns>
		///   <see langword="true" /> if the hosted control is in edit mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0001A238 File Offset: 0x00018438
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool EditMode
		{
			get
			{
				return this.editMode != 0;
			}
		}

		/// <summary>Gets a value indicating whether the ActiveX control has an About dialog box.</summary>
		/// <returns>
		///   <see langword="true" /> if the ActiveX control has an About dialog box; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0001A243 File Offset: 0x00018443
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasAboutBox
		{
			get
			{
				return this.aboutBoxDelegate != null;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0001A24E File Offset: 0x0001844E
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x0001A256 File Offset: 0x00018456
		private int NoComponentChangeEvents
		{
			get
			{
				return this.noComponentChange;
			}
			set
			{
				this.noComponentChange = value;
			}
		}

		/// <summary>Displays the ActiveX control's About dialog box.</summary>
		// Token: 0x060009C2 RID: 2498 RVA: 0x0001A25F File Offset: 0x0001845F
		public void ShowAboutBox()
		{
			if (this.aboutBoxDelegate != null)
			{
				this.aboutBoxDelegate();
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.BackColorChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060009C3 RID: 2499 RVA: 0x0001A274 File Offset: 0x00018474
		// (remove) Token: 0x060009C4 RID: 2500 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "BackColorChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.BackgroundImageChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060009C5 RID: 2501 RVA: 0x0001A293 File Offset: 0x00018493
		// (remove) Token: 0x060009C6 RID: 2502 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "BackgroundImageChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060009C7 RID: 2503 RVA: 0x0001A2B2 File Offset: 0x000184B2
		// (remove) Token: 0x060009C8 RID: 2504 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "BackgroundImageLayoutChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.BindingContextChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060009C9 RID: 2505 RVA: 0x0001A2D1 File Offset: 0x000184D1
		// (remove) Token: 0x060009CA RID: 2506 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BindingContextChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "BindingContextChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.ContextMenuChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060009CB RID: 2507 RVA: 0x0001A2F0 File Offset: 0x000184F0
		// (remove) Token: 0x060009CC RID: 2508 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ContextMenuChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "ContextMenuChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.CursorChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060009CD RID: 2509 RVA: 0x0001A30F File Offset: 0x0001850F
		// (remove) Token: 0x060009CE RID: 2510 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CursorChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "CursorChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.EnabledChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060009CF RID: 2511 RVA: 0x0001A32E File Offset: 0x0001852E
		// (remove) Token: 0x060009D0 RID: 2512 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "EnabledChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.FontChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060009D1 RID: 2513 RVA: 0x0001A34D File Offset: 0x0001854D
		// (remove) Token: 0x060009D2 RID: 2514 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler FontChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "FontChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.ForeColorChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060009D3 RID: 2515 RVA: 0x0001A36C File Offset: 0x0001856C
		// (remove) Token: 0x060009D4 RID: 2516 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "ForeColorChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.RightToLeftChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060009D5 RID: 2517 RVA: 0x0001A38B File Offset: 0x0001858B
		// (remove) Token: 0x060009D6 RID: 2518 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "RightToLeftChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.TextChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060009D7 RID: 2519 RVA: 0x0001A3AA File Offset: 0x000185AA
		// (remove) Token: 0x060009D8 RID: 2520 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "TextChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.Click" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060009D9 RID: 2521 RVA: 0x0001A3C9 File Offset: 0x000185C9
		// (remove) Token: 0x060009DA RID: 2522 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Click
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "Click" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DragDrop" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060009DB RID: 2523 RVA: 0x0001A3E8 File Offset: 0x000185E8
		// (remove) Token: 0x060009DC RID: 2524 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event DragEventHandler DragDrop
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "DragDrop" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DragEnter" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060009DD RID: 2525 RVA: 0x0001A407 File Offset: 0x00018607
		// (remove) Token: 0x060009DE RID: 2526 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event DragEventHandler DragEnter
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "DragEnter" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DragOver" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060009DF RID: 2527 RVA: 0x0001A426 File Offset: 0x00018626
		// (remove) Token: 0x060009E0 RID: 2528 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event DragEventHandler DragOver
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "DragOver" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DragLeave" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060009E1 RID: 2529 RVA: 0x0001A445 File Offset: 0x00018645
		// (remove) Token: 0x060009E2 RID: 2530 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DragLeave
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "DragLeave" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.GiveFeedback" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060009E3 RID: 2531 RVA: 0x0001A464 File Offset: 0x00018664
		// (remove) Token: 0x060009E4 RID: 2532 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "GiveFeedback" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.HelpRequested" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060009E5 RID: 2533 RVA: 0x0001A483 File Offset: 0x00018683
		// (remove) Token: 0x060009E6 RID: 2534 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event HelpEventHandler HelpRequested
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "HelpRequested" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.Paint" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060009E7 RID: 2535 RVA: 0x0001A4A2 File Offset: 0x000186A2
		// (remove) Token: 0x060009E8 RID: 2536 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event PaintEventHandler Paint
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "Paint" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.QueryContinueDrag" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060009E9 RID: 2537 RVA: 0x0001A4C1 File Offset: 0x000186C1
		// (remove) Token: 0x060009EA RID: 2538 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "QueryContinueDrag" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.QueryAccessibilityHelp" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x060009EB RID: 2539 RVA: 0x0001A4E0 File Offset: 0x000186E0
		// (remove) Token: 0x060009EC RID: 2540 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "QueryAccessibilityHelp" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.DoubleClick" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x060009ED RID: 2541 RVA: 0x0001A4FF File Offset: 0x000186FF
		// (remove) Token: 0x060009EE RID: 2542 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DoubleClick
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "DoubleClick" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.ImeModeChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x1400003E RID: 62
		// (add) Token: 0x060009EF RID: 2543 RVA: 0x0001A51E File Offset: 0x0001871E
		// (remove) Token: 0x060009F0 RID: 2544 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "ImeModeChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.KeyDown" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x1400003F RID: 63
		// (add) Token: 0x060009F1 RID: 2545 RVA: 0x0001A53D File Offset: 0x0001873D
		// (remove) Token: 0x060009F2 RID: 2546 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "KeyDown" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.KeyPress" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x060009F3 RID: 2547 RVA: 0x0001A55C File Offset: 0x0001875C
		// (remove) Token: 0x060009F4 RID: 2548 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "KeyPress" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.KeyUp" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x060009F5 RID: 2549 RVA: 0x0001A57B File Offset: 0x0001877B
		// (remove) Token: 0x060009F6 RID: 2550 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "KeyUp" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.Layout" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060009F7 RID: 2551 RVA: 0x0001A59A File Offset: 0x0001879A
		// (remove) Token: 0x060009F8 RID: 2552 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event LayoutEventHandler Layout
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "Layout" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseDown" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060009F9 RID: 2553 RVA: 0x0001A5B9 File Offset: 0x000187B9
		// (remove) Token: 0x060009FA RID: 2554 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseDown
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseDown" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseEnter" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060009FB RID: 2555 RVA: 0x0001A5D8 File Offset: 0x000187D8
		// (remove) Token: 0x060009FC RID: 2556 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseEnter
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseEnter" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseLeave" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x060009FD RID: 2557 RVA: 0x0001A5F7 File Offset: 0x000187F7
		// (remove) Token: 0x060009FE RID: 2558 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseLeave
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseLeave" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseHover" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060009FF RID: 2559 RVA: 0x0001A616 File Offset: 0x00018816
		// (remove) Token: 0x06000A00 RID: 2560 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseHover
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseHover" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseMove" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06000A01 RID: 2561 RVA: 0x0001A635 File Offset: 0x00018835
		// (remove) Token: 0x06000A02 RID: 2562 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseMove" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseUp" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06000A03 RID: 2563 RVA: 0x0001A654 File Offset: 0x00018854
		// (remove) Token: 0x06000A04 RID: 2564 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseUp
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseUp" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.MouseWheel" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06000A05 RID: 2565 RVA: 0x0001A673 File Offset: 0x00018873
		// (remove) Token: 0x06000A06 RID: 2566 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseWheel
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "MouseWheel" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.ChangeUICues" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06000A07 RID: 2567 RVA: 0x0001A692 File Offset: 0x00018892
		// (remove) Token: 0x06000A08 RID: 2568 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event UICuesEventHandler ChangeUICues
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "ChangeUICues" }));
			}
			remove
			{
			}
		}

		/// <summary>The <see cref="E:System.Windows.Forms.AxHost.StyleChanged" /> event is not supported by the <see cref="T:System.Windows.Forms.AxHost" /> class.</summary>
		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06000A09 RID: 2569 RVA: 0x0001A6B1 File Offset: 0x000188B1
		// (remove) Token: 0x06000A0A RID: 2570 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler StyleChanged
		{
			add
			{
				throw new NotSupportedException(SR.GetString("AXAddInvalidEvent", new object[] { "StyleChanged" }));
			}
			remove
			{
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000A0B RID: 2571 RVA: 0x0001A6D0 File Offset: 0x000188D0
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.AmbientChanged(-703);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000A0C RID: 2572 RVA: 0x0001A6E4 File Offset: 0x000188E4
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			this.AmbientChanged(-704);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000A0D RID: 2573 RVA: 0x0001A6F8 File Offset: 0x000188F8
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			this.AmbientChanged(-701);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0001A70C File Offset: 0x0001890C
		private void AmbientChanged(int dispid)
		{
			if (this.GetOcx() != null)
			{
				try
				{
					base.Invalidate();
					this.GetOleControl().OnAmbientPropertyChange(dispid);
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001A74C File Offset: 0x0001894C
		private bool OwnWindow()
		{
			return this.axState[AxHost.fOwnWindow] || this.axState[AxHost.fFakingWindow];
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001A772 File Offset: 0x00018972
		private IntPtr GetHandleNoCreate()
		{
			if (base.IsHandleCreated)
			{
				return base.Handle;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0001A788 File Offset: 0x00018988
		private ISelectionService GetSelectionService()
		{
			return AxHost.GetSelectionService(this);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001A790 File Offset: 0x00018990
		private static ISelectionService GetSelectionService(Control ctl)
		{
			ISite site = ctl.Site;
			if (site != null)
			{
				object service = site.GetService(typeof(ISelectionService));
				return service as ISelectionService;
			}
			return null;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0001A7C0 File Offset: 0x000189C0
		private void AddSelectionHandler()
		{
			if (this.axState[AxHost.addedSelectionHandler])
			{
				return;
			}
			ISelectionService selectionService = this.GetSelectionService();
			if (selectionService != null)
			{
				selectionService.SelectionChanging += this.selectionChangeHandler;
			}
			this.axState[AxHost.addedSelectionHandler] = true;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0001A808 File Offset: 0x00018A08
		private void OnComponentRename(object sender, ComponentRenameEventArgs e)
		{
			if (e.Component == this)
			{
				UnsafeNativeMethods.IOleControl oleControl = this.GetOcx() as UnsafeNativeMethods.IOleControl;
				if (oleControl != null)
				{
					oleControl.OnAmbientPropertyChange(-702);
				}
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001A83C File Offset: 0x00018A3C
		private bool RemoveSelectionHandler()
		{
			if (!this.axState[AxHost.addedSelectionHandler])
			{
				return false;
			}
			ISelectionService selectionService = this.GetSelectionService();
			if (selectionService != null)
			{
				selectionService.SelectionChanging -= this.selectionChangeHandler;
			}
			this.axState[AxHost.addedSelectionHandler] = false;
			return true;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0001A888 File Offset: 0x00018A88
		private void SyncRenameNotification(bool hook)
		{
			if (base.DesignMode && hook != this.axState[AxHost.renameEventHooked])
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					if (hook)
					{
						componentChangeService.ComponentRename += this.OnComponentRename;
					}
					else
					{
						componentChangeService.ComponentRename -= this.OnComponentRename;
					}
					this.axState[AxHost.renameEventHooked] = hook;
				}
			}
		}

		/// <summary>Gets or sets the site of the control.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the Control, if any.</returns>
		// Token: 0x170002A2 RID: 674
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0001A904 File Offset: 0x00018B04
		public override ISite Site
		{
			set
			{
				if (this.axState[AxHost.disposed])
				{
					return;
				}
				bool flag = this.RemoveSelectionHandler();
				bool flag2 = this.IsUserMode();
				this.SyncRenameNotification(false);
				base.Site = value;
				bool flag3 = this.IsUserMode();
				if (!flag3)
				{
					this.GetOcxCreate();
				}
				if (flag)
				{
					this.AddSelectionHandler();
				}
				this.SyncRenameNotification(value != null);
				if (value != null && !flag3 && flag2 != flag3 && this.GetOcState() > 1)
				{
					this.TransitionDownTo(1);
					this.TransitionUpTo(4);
					ContainerControl containerControl = this.ContainingControl;
					if (containerControl != null && containerControl.Visible && base.Visible)
					{
						this.MakeVisibleWithShow();
					}
				}
				if (flag2 != flag3 && !base.IsHandleCreated && !this.axState[AxHost.disposed] && this.GetOcx() != null)
				{
					this.RealizeStyles();
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000A18 RID: 2584 RVA: 0x0001A9D4 File Offset: 0x00018BD4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLostFocus(EventArgs e)
		{
			bool flag = this.GetHandleNoCreate() != this.hwndFocus;
			if (flag && base.IsHandleCreated)
			{
				flag = !UnsafeNativeMethods.IsChild(new HandleRef(this, this.GetHandleNoCreate()), new HandleRef(null, this.hwndFocus));
			}
			base.OnLostFocus(e);
			if (flag)
			{
				this.UiDeactivate();
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001AA30 File Offset: 0x00018C30
		private void OnNewSelection(object sender, EventArgs e)
		{
			if (this.IsUserMode())
			{
				return;
			}
			ISelectionService selectionService = this.GetSelectionService();
			if (selectionService != null)
			{
				if (this.GetOcState() >= 8 && !selectionService.GetComponentSelected(this))
				{
					int num = this.UiDeactivate();
					NativeMethods.Failed(num);
				}
				if (!selectionService.GetComponentSelected(this))
				{
					if (this.editMode != 0)
					{
						this.GetParentContainer().OnExitEditMode(this);
						this.editMode = 0;
					}
					this.SetSelectionStyle(1);
					this.RemoveSelectionHandler();
					return;
				}
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)["SelectionStyle"];
				if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(int))
				{
					int num2 = (int)propertyDescriptor.GetValue(this);
					if (num2 != this.selectionStyle)
					{
						propertyDescriptor.SetValue(this, this.selectionStyle);
					}
				}
			}
		}

		/// <summary>This method is not supported by this control.</summary>
		/// <param name="bitmap">A <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="targetBounds">A <see cref="T:System.Drawing.Rectangle" />.</param>
		// Token: 0x06000A1A RID: 2586 RVA: 0x0001AAF9 File Offset: 0x00018CF9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
		{
			base.DrawToBitmap(bitmap, targetBounds);
		}

		/// <summary>Creates a handle for the control.</summary>
		// Token: 0x06000A1B RID: 2587 RVA: 0x0001AB04 File Offset: 0x00018D04
		protected override void CreateHandle()
		{
			if (!base.IsHandleCreated)
			{
				this.TransitionUpTo(2);
				if (!this.axState[AxHost.fOwnWindow])
				{
					if (this.axState[AxHost.fNeedOwnWindow])
					{
						this.axState[AxHost.fNeedOwnWindow] = false;
						this.axState[AxHost.fFakingWindow] = true;
						base.CreateHandle();
					}
					else
					{
						this.TransitionUpTo(4);
						if (this.axState[AxHost.fNeedOwnWindow])
						{
							this.CreateHandle();
							return;
						}
					}
				}
				else
				{
					base.SetState(2, false);
					base.CreateHandle();
				}
				this.GetParentContainer().ControlCreated(this);
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001ABAB File Offset: 0x00018DAB
		private NativeMethods.COMRECT GetClipRect(NativeMethods.COMRECT clipRect)
		{
			if (clipRect != null)
			{
				AxHost.FillInRect(clipRect, new Rectangle(0, 0, 32000, 32000));
			}
			return clipRect;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001ABCC File Offset: 0x00018DCC
		private static int SetupLogPixels(bool force)
		{
			if (AxHost.logPixelsX == -1 || force)
			{
				IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
				if (dc == IntPtr.Zero)
				{
					return -2147467259;
				}
				AxHost.logPixelsX = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 88);
				AxHost.logPixelsY = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 90);
				UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
			}
			return 0;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001AC3C File Offset: 0x00018E3C
		private void HiMetric2Pixel(NativeMethods.tagSIZEL sz, NativeMethods.tagSIZEL szout)
		{
			NativeMethods._POINTL pointl = new NativeMethods._POINTL();
			pointl.x = sz.cx;
			pointl.y = sz.cy;
			NativeMethods.tagPOINTF tagPOINTF = new NativeMethods.tagPOINTF();
			((UnsafeNativeMethods.IOleControlSite)this.oleSite).TransformCoords(pointl, tagPOINTF, 6);
			szout.cx = (int)tagPOINTF.x;
			szout.cy = (int)tagPOINTF.y;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001AC98 File Offset: 0x00018E98
		private void Pixel2hiMetric(NativeMethods.tagSIZEL sz, NativeMethods.tagSIZEL szout)
		{
			NativeMethods.tagPOINTF tagPOINTF = new NativeMethods.tagPOINTF();
			tagPOINTF.x = (float)sz.cx;
			tagPOINTF.y = (float)sz.cy;
			NativeMethods._POINTL pointl = new NativeMethods._POINTL();
			((UnsafeNativeMethods.IOleControlSite)this.oleSite).TransformCoords(pointl, tagPOINTF, 10);
			szout.cx = pointl.x;
			szout.cy = pointl.y;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001ACF4 File Offset: 0x00018EF4
		private static int Pixel2Twip(int v, bool xDirection)
		{
			AxHost.SetupLogPixels(false);
			int num = (xDirection ? AxHost.logPixelsX : AxHost.logPixelsY);
			return (int)((double)v / (double)num * 72.0 * 20.0);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001AD34 File Offset: 0x00018F34
		private static int Twip2Pixel(double v, bool xDirection)
		{
			AxHost.SetupLogPixels(false);
			int num = (xDirection ? AxHost.logPixelsX : AxHost.logPixelsY);
			return (int)(v / 20.0 / 72.0 * (double)num);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001AD74 File Offset: 0x00018F74
		private static int Twip2Pixel(int v, bool xDirection)
		{
			AxHost.SetupLogPixels(false);
			int num = (xDirection ? AxHost.logPixelsX : AxHost.logPixelsY);
			return (int)((double)v / 20.0 / 72.0 * (double)num);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001ADB4 File Offset: 0x00018FB4
		private Size SetExtent(int width, int height)
		{
			NativeMethods.tagSIZEL tagSIZEL = new NativeMethods.tagSIZEL();
			tagSIZEL.cx = width;
			tagSIZEL.cy = height;
			bool flag = !this.IsUserMode();
			try
			{
				this.Pixel2hiMetric(tagSIZEL, tagSIZEL);
				this.GetOleObject().SetExtent(1, tagSIZEL);
			}
			catch (COMException)
			{
				flag = true;
			}
			if (flag)
			{
				this.GetOleObject().GetExtent(1, tagSIZEL);
				try
				{
					this.GetOleObject().SetExtent(1, tagSIZEL);
				}
				catch (COMException ex)
				{
				}
			}
			return this.GetExtent();
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001AE44 File Offset: 0x00019044
		private Size GetExtent()
		{
			NativeMethods.tagSIZEL tagSIZEL = new NativeMethods.tagSIZEL();
			this.GetOleObject().GetExtent(1, tagSIZEL);
			this.HiMetric2Pixel(tagSIZEL, tagSIZEL);
			return new Size(tagSIZEL.cx, tagSIZEL.cy);
		}

		/// <summary>Called by the system to retrieve the current bounds of the ActiveX control.</summary>
		/// <param name="bounds">The original bounds of the ActiveX control.</param>
		/// <param name="factor">A scaling factor.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value.</param>
		/// <returns>The unmodified <paramref name="bounds" /> value.</returns>
		// Token: 0x06000A25 RID: 2597 RVA: 0x0001AE7E File Offset: 0x0001907E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			return bounds;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001AE81 File Offset: 0x00019081
		private void SetObjectRects(Rectangle bounds)
		{
			if (this.GetOcState() < 4)
			{
				return;
			}
			this.GetInPlaceObject().SetObjectRects(AxHost.FillInRect(new NativeMethods.COMRECT(), bounds), this.GetClipRect(new NativeMethods.COMRECT()));
		}

		/// <summary>Performs the work of setting the specified bounds of this control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06000A27 RID: 2599 RVA: 0x0001AEB0 File Offset: 0x000190B0
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.GetAxState(AxHost.handlePosRectChanged))
			{
				return;
			}
			this.axState[AxHost.handlePosRectChanged] = true;
			Size size = base.ApplySizeConstraints(width, height);
			width = size.Width;
			height = size.Height;
			try
			{
				if (this.axState[AxHost.fFakingWindow])
				{
					base.SetBoundsCore(x, y, width, height, specified);
				}
				else
				{
					Rectangle bounds = base.Bounds;
					if (bounds.X != x || bounds.Y != y || bounds.Width != width || bounds.Height != height)
					{
						if (!base.IsHandleCreated)
						{
							base.UpdateBounds(x, y, width, height);
						}
						else
						{
							if (this.GetOcState() > 2)
							{
								this.CheckSubclassing();
								if (width != bounds.Width || height != bounds.Height)
								{
									Size size2 = this.SetExtent(width, height);
									width = size2.Width;
									height = size2.Height;
								}
							}
							if (this.axState[AxHost.manualUpdate])
							{
								this.SetObjectRects(new Rectangle(x, y, width, height));
								this.CheckSubclassing();
								base.UpdateBounds();
							}
							else
							{
								this.SetObjectRects(new Rectangle(x, y, width, height));
								base.SetBoundsCore(x, y, width, height, specified);
								base.Invalidate();
							}
						}
					}
				}
			}
			finally
			{
				this.axState[AxHost.handlePosRectChanged] = false;
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001B030 File Offset: 0x00019230
		private bool CheckSubclassing()
		{
			if (!base.IsHandleCreated || this.wndprocAddr == IntPtr.Zero)
			{
				return true;
			}
			IntPtr handle = base.Handle;
			IntPtr windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, handle), -4);
			if (windowLong == this.wndprocAddr)
			{
				return true;
			}
			if ((int)(long)base.SendMessage(this.REGMSG_MSG, 0, 0) == 123)
			{
				this.wndprocAddr = windowLong;
				return true;
			}
			base.WindowReleaseHandle();
			UnsafeNativeMethods.SetWindowLong(new HandleRef(this, handle), -4, new HandleRef(this, windowLong));
			base.WindowAssignHandle(handle, this.axState[AxHost.assignUniqueID]);
			this.InformOfNewHandle();
			this.axState[AxHost.manualUpdate] = true;
			return false;
		}

		/// <summary>Destroys the handle associated with the control.</summary>
		// Token: 0x06000A29 RID: 2601 RVA: 0x0001B0EB File Offset: 0x000192EB
		protected override void DestroyHandle()
		{
			if (this.axState[AxHost.fOwnWindow])
			{
				base.DestroyHandle();
				return;
			}
			if (base.IsHandleCreated)
			{
				this.TransitionDownTo(2);
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001B118 File Offset: 0x00019318
		private void TransitionDownTo(int state)
		{
			if (this.axState[AxHost.inTransition])
			{
				return;
			}
			try
			{
				this.axState[AxHost.inTransition] = true;
				while (state < this.GetOcState())
				{
					int num = this.GetOcState();
					switch (num)
					{
					case 1:
						this.ReleaseAxControl();
						this.SetOcState(0);
						continue;
					case 2:
						this.StopEvents();
						this.DisposeAxControl();
						this.SetOcState(1);
						continue;
					case 3:
						break;
					case 4:
						if (this.axState[AxHost.fFakingWindow])
						{
							this.DestroyFakeWindow();
							this.SetOcState(2);
						}
						else
						{
							this.InPlaceDeactivate();
						}
						this.SetOcState(2);
						continue;
					default:
						if (num == 8)
						{
							int num2 = this.UiDeactivate();
							this.SetOcState(4);
							continue;
						}
						if (num == 16)
						{
							this.SetOcState(8);
							continue;
						}
						break;
					}
					this.SetOcState(this.GetOcState() - 1);
				}
			}
			finally
			{
				this.axState[AxHost.inTransition] = false;
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0001B224 File Offset: 0x00019424
		private void TransitionUpTo(int state)
		{
			if (this.axState[AxHost.inTransition])
			{
				return;
			}
			try
			{
				this.axState[AxHost.inTransition] = true;
				while (state > this.GetOcState())
				{
					switch (this.GetOcState())
					{
					case 0:
						this.axState[AxHost.disposed] = false;
						this.GetOcxCreate();
						this.SetOcState(1);
						continue;
					case 1:
						this.ActivateAxControl();
						this.SetOcState(2);
						if (this.IsUserMode())
						{
							this.StartEvents();
							continue;
						}
						continue;
					case 2:
						this.axState[AxHost.ownDisposing] = false;
						if (!this.axState[AxHost.fOwnWindow])
						{
							this.InPlaceActivate();
							if (!base.Visible && this.ContainingControl != null && this.ContainingControl.Visible)
							{
								this.HideAxControl();
							}
							else
							{
								base.CreateControl(true);
								if (!this.IsUserMode() && !this.axState[AxHost.ocxStateSet])
								{
									Size extent = this.GetExtent();
									Rectangle bounds = base.Bounds;
									if (bounds.Size.Equals(this.DefaultSize) && !bounds.Size.Equals(extent))
									{
										bounds.Width = extent.Width;
										bounds.Height = extent.Height;
										base.Bounds = bounds;
									}
								}
							}
						}
						if (this.GetOcState() < 4)
						{
							this.SetOcState(4);
						}
						this.OnInPlaceActive();
						continue;
					case 4:
						this.DoVerb(-1);
						this.SetOcState(8);
						continue;
					}
					this.SetOcState(this.GetOcState() + 1);
				}
			}
			finally
			{
				this.axState[AxHost.inTransition] = false;
			}
		}

		/// <summary>Called when the control transitions to the in-place active state.</summary>
		// Token: 0x06000A2C RID: 2604 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnInPlaceActive()
		{
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001B424 File Offset: 0x00019624
		private void InPlaceActivate()
		{
			try
			{
				this.DoVerb(-5);
			}
			catch (Exception ex)
			{
				throw new TargetInvocationException(SR.GetString("AXNohWnd", new object[] { base.GetType().Name }), ex);
			}
			this.EnsureWindowPresent();
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0001B478 File Offset: 0x00019678
		private void InPlaceDeactivate()
		{
			this.axState[AxHost.ownDisposing] = true;
			ContainerControl containerControl = this.ContainingControl;
			if (containerControl != null && containerControl.ActiveControl == this)
			{
				containerControl.ActiveControl = null;
			}
			try
			{
				this.GetInPlaceObject().InPlaceDeactivate();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001B4D4 File Offset: 0x000196D4
		private void UiActivate()
		{
			if (this.CanUIActivate)
			{
				this.DoVerb(-4);
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001B4E6 File Offset: 0x000196E6
		private void DestroyFakeWindow()
		{
			this.axState[AxHost.fFakingWindow] = false;
			base.DestroyHandle();
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001B500 File Offset: 0x00019700
		private void EnsureWindowPresent()
		{
			if (!base.IsHandleCreated)
			{
				try
				{
					((UnsafeNativeMethods.IOleClientSite)this.oleSite).ShowObject();
				}
				catch
				{
				}
			}
			if (base.IsHandleCreated)
			{
				return;
			}
			if (this.ParentInternal != null)
			{
				throw new NotSupportedException(SR.GetString("AXNohWnd", new object[] { base.GetType().Name }));
			}
		}

		/// <summary>Sets the control to the specified visible state.</summary>
		/// <param name="value">
		///   <see langword="true" /> to make the control visible; otherwise, <see langword="false" />.</param>
		// Token: 0x06000A32 RID: 2610 RVA: 0x0001B56C File Offset: 0x0001976C
		protected override void SetVisibleCore(bool value)
		{
			if (base.GetState(2) != value)
			{
				bool visible = base.Visible;
				if ((base.IsHandleCreated || value) && this.ParentInternal != null && this.ParentInternal.Created && !this.axState[AxHost.fOwnWindow])
				{
					this.TransitionUpTo(2);
					if (value)
					{
						if (this.axState[AxHost.fFakingWindow])
						{
							this.DestroyFakeWindow();
						}
						if (!base.IsHandleCreated)
						{
							try
							{
								this.SetExtent(base.Width, base.Height);
								this.InPlaceActivate();
								base.CreateControl(true);
								goto IL_AE;
							}
							catch
							{
								this.MakeVisibleWithShow();
								goto IL_AE;
							}
						}
						this.MakeVisibleWithShow();
					}
					else
					{
						this.HideAxControl();
					}
				}
				IL_AE:
				if (!value)
				{
					this.axState[AxHost.fNeedOwnWindow] = false;
				}
				if (!this.axState[AxHost.fOwnWindow])
				{
					base.SetState(2, value);
					if (base.Visible != visible)
					{
						this.OnVisibleChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001B67C File Offset: 0x0001987C
		private void MakeVisibleWithShow()
		{
			ContainerControl containerControl = this.ContainingControl;
			Control control = ((containerControl == null) ? null : containerControl.ActiveControl);
			try
			{
				this.DoVerb(-1);
			}
			catch (Exception ex)
			{
				throw new TargetInvocationException(SR.GetString("AXNohWnd", new object[] { base.GetType().Name }), ex);
			}
			this.EnsureWindowPresent();
			base.CreateControl(true);
			if (containerControl != null && containerControl.ActiveControl != control)
			{
				containerControl.ActiveControl = control;
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001B6FC File Offset: 0x000198FC
		private void HideAxControl()
		{
			this.DoVerb(-3);
			if (this.GetOcState() < 4)
			{
				this.axState[AxHost.fNeedOwnWindow] = true;
				this.SetOcState(4);
			}
		}

		/// <summary>Determines if a character is an input character that the ActiveX control recognizes.</summary>
		/// <param name="charCode">The character to test.</param>
		/// <returns>
		///   <see langword="true" /> if the character should be sent directly to the ActiveX control and not preprocessed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A35 RID: 2613 RVA: 0x00012E4E File Offset: 0x0001104E
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool IsInputChar(char charCode)
		{
			return true;
		}

		/// <summary>Processes a dialog key.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the key was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A36 RID: 2614 RVA: 0x0001B727 File Offset: 0x00019927
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			return !this.ignoreDialogKeys && base.ProcessDialogKey(keyData);
		}

		/// <summary>Preprocesses keyboard or input messages within the message loop before they are dispatched.</summary>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the message to process. The possible values are WM_KEYDOWN, WM_SYSKEYDOWN, WM_CHAR, and WM_SYSCHAR.</param>
		/// <returns>
		///   <see langword="true" /> if the message was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A37 RID: 2615 RVA: 0x0001B73C File Offset: 0x0001993C
		public override bool PreProcessMessage(ref Message msg)
		{
			if (this.IsUserMode())
			{
				if (this.axState[AxHost.siteProcessedInputKey])
				{
					return base.PreProcessMessage(ref msg);
				}
				NativeMethods.MSG msg2 = default(NativeMethods.MSG);
				msg2.message = msg.Msg;
				msg2.wParam = msg.WParam;
				msg2.lParam = msg.LParam;
				msg2.hwnd = msg.HWnd;
				this.axState[AxHost.siteProcessedInputKey] = false;
				try
				{
					UnsafeNativeMethods.IOleInPlaceActiveObject inPlaceActiveObject = this.GetInPlaceActiveObject();
					if (inPlaceActiveObject != null)
					{
						int num = inPlaceActiveObject.TranslateAccelerator(ref msg2);
						msg.Msg = msg2.message;
						msg.WParam = msg2.wParam;
						msg.LParam = msg2.lParam;
						msg.HWnd = msg2.hwnd;
						if (num == 0)
						{
							return true;
						}
						if (num == 1)
						{
							bool flag = false;
							this.ignoreDialogKeys = true;
							try
							{
								flag = base.PreProcessMessage(ref msg);
							}
							finally
							{
								this.ignoreDialogKeys = false;
							}
							return flag;
						}
						if (this.axState[AxHost.siteProcessedInputKey])
						{
							return base.PreProcessMessage(ref msg);
						}
						return false;
					}
				}
				finally
				{
					this.axState[AxHost.siteProcessedInputKey] = false;
				}
				return false;
			}
			return false;
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed as a mnemonic by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A38 RID: 2616 RVA: 0x0001B884 File Offset: 0x00019A84
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (base.CanSelect)
			{
				try
				{
					NativeMethods.tagCONTROLINFO tagCONTROLINFO = new NativeMethods.tagCONTROLINFO();
					int controlInfo = this.GetOleControl().GetControlInfo(tagCONTROLINFO);
					if (NativeMethods.Failed(controlInfo))
					{
						return false;
					}
					NativeMethods.MSG msg = default(NativeMethods.MSG);
					msg.hwnd = ((this.ContainingControl == null) ? IntPtr.Zero : this.ContainingControl.Handle);
					msg.message = 260;
					msg.wParam = (IntPtr)((int)char.ToUpper(charCode, CultureInfo.CurrentCulture));
					msg.lParam = (IntPtr)538443777;
					msg.time = SafeNativeMethods.GetTickCount();
					NativeMethods.POINT point = new NativeMethods.POINT();
					UnsafeNativeMethods.GetCursorPos(point);
					msg.pt_x = point.x;
					msg.pt_y = point.y;
					if (SafeNativeMethods.IsAccelerator(new HandleRef(tagCONTROLINFO, tagCONTROLINFO.hAccel), (int)tagCONTROLINFO.cAccel, ref msg, null))
					{
						this.GetOleControl().OnMnemonic(ref msg);
						base.Focus();
						return true;
					}
				}
				catch (Exception ex)
				{
					return false;
				}
				return false;
			}
			return false;
		}

		/// <summary>Calls the <see cref="M:System.Windows.Forms.AxHost.ShowAboutBox" /> method to display the ActiveX control's About dialog box.</summary>
		/// <param name="d">The <see cref="T:System.Windows.Forms.AxHost.AboutBoxDelegate" /> to call.</param>
		// Token: 0x06000A39 RID: 2617 RVA: 0x0001B9A4 File Offset: 0x00019BA4
		protected void SetAboutBoxDelegate(AxHost.AboutBoxDelegate d)
		{
			this.aboutBoxDelegate = (AxHost.AboutBoxDelegate)Delegate.Combine(this.aboutBoxDelegate, d);
		}

		/// <summary>Gets or sets the persisted state of the ActiveX control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.AxHost.State" /> that represents the persisted state of the ActiveX control.</returns>
		/// <exception cref="T:System.Exception">The ActiveX control is already loaded.</exception>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0001B9BD File Offset: 0x00019BBD
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x0001B9E8 File Offset: 0x00019BE8
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.All)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AxHost.State OcxState
		{
			get
			{
				if (this.IsDirty() || this.ocxState == null)
				{
					this.ocxState = this.CreateNewOcxState(this.ocxState);
				}
				return this.ocxState;
			}
			set
			{
				this.axState[AxHost.ocxStateSet] = true;
				if (value == null)
				{
					return;
				}
				if (this.storageType != -1 && this.storageType != value.type)
				{
					throw new InvalidOperationException(SR.GetString("AXOcxStateLoaded"));
				}
				if (this.ocxState == value)
				{
					return;
				}
				this.ocxState = value;
				if (this.ocxState != null)
				{
					this.axState[AxHost.manualUpdate] = this.ocxState._GetManualUpdate();
					this.licenseKey = this.ocxState._GetLicenseKey();
				}
				else
				{
					this.axState[AxHost.manualUpdate] = false;
					this.licenseKey = null;
				}
				if (this.ocxState != null && this.GetOcState() >= 2)
				{
					this.DepersistControl();
				}
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001BAA8 File Offset: 0x00019CA8
		private AxHost.State CreateNewOcxState(AxHost.State oldOcxState)
		{
			int num = this.NoComponentChangeEvents;
			this.NoComponentChangeEvents = num + 1;
			try
			{
				if (this.GetOcState() < 2)
				{
					return null;
				}
				try
				{
					AxHost.PropertyBagStream propertyBagStream = null;
					if (this.iPersistPropBag != null)
					{
						propertyBagStream = new AxHost.PropertyBagStream();
						this.iPersistPropBag.Save(propertyBagStream, true, true);
					}
					int num2 = this.storageType;
					if (num2 > 1)
					{
						if (num2 != 2)
						{
							return null;
						}
						if (oldOcxState != null)
						{
							return oldOcxState.RefreshStorage(this.iPersistStorage);
						}
						return null;
					}
					else
					{
						MemoryStream memoryStream = new MemoryStream();
						if (this.storageType == 0)
						{
							this.iPersistStream.Save(new UnsafeNativeMethods.ComStreamFromDataStream(memoryStream), true);
						}
						else
						{
							this.iPersistStreamInit.Save(new UnsafeNativeMethods.ComStreamFromDataStream(memoryStream), true);
						}
						if (memoryStream != null)
						{
							return new AxHost.State(memoryStream, this.storageType, this, propertyBagStream);
						}
						if (propertyBagStream != null)
						{
							return new AxHost.State(propertyBagStream);
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
			finally
			{
				num = this.NoComponentChangeEvents;
				this.NoComponentChangeEvents = num - 1;
			}
			return null;
		}

		/// <summary>Gets or sets the control containing the ActiveX control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContainerControl" /> that represents the control containing the ActiveX control.</returns>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0001BBB0 File Offset: 0x00019DB0
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x0001BBD6 File Offset: 0x00019DD6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ContainerControl ContainingControl
		{
			get
			{
				IntSecurity.GetParent.Demand();
				if (this.containingControl == null)
				{
					this.containingControl = this.FindContainerControlInternal();
				}
				return this.containingControl;
			}
			set
			{
				this.containingControl = value;
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001BBE0 File Offset: 0x00019DE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal override bool ShouldSerializeText()
		{
			bool flag = false;
			try
			{
				flag = this.Text.Length != 0;
			}
			catch (COMException)
			{
			}
			return flag;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001BC14 File Offset: 0x00019E14
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeContainingControl()
		{
			return this.ContainingControl != this.ParentInternal;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001BC28 File Offset: 0x00019E28
		private ContainerControl FindContainerControlInternal()
		{
			if (this.Site != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.Site.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					ContainerControl containerControl = designerHost.RootComponent as ContainerControl;
					if (containerControl != null)
					{
						return containerControl;
					}
				}
			}
			ContainerControl containerControl2 = null;
			for (Control control = this; control != null; control = control.ParentInternal)
			{
				ContainerControl containerControl3 = control as ContainerControl;
				if (containerControl3 != null)
				{
					containerControl2 = containerControl3;
					break;
				}
			}
			return containerControl2;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001BC90 File Offset: 0x00019E90
		private bool IsDirty()
		{
			if (this.GetOcState() < 2)
			{
				return false;
			}
			if (this.axState[AxHost.valueChanged])
			{
				this.axState[AxHost.valueChanged] = false;
				return true;
			}
			int num;
			switch (this.storageType)
			{
			case 0:
				num = this.iPersistStream.IsDirty();
				break;
			case 1:
				num = this.iPersistStreamInit.IsDirty();
				break;
			case 2:
				num = this.iPersistStorage.IsDirty();
				break;
			default:
				return true;
			}
			if (num == 1)
			{
				return false;
			}
			NativeMethods.Failed(num);
			return true;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001BD28 File Offset: 0x00019F28
		internal bool IsUserMode()
		{
			ISite site = this.Site;
			return site == null || !site.DesignMode;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001BD4C File Offset: 0x00019F4C
		private object GetAmbientProperty(int dispid)
		{
			Control parentInternal = this.ParentInternal;
			if (dispid != -732)
			{
				switch (dispid)
				{
				case -715:
					return true;
				case -713:
					return false;
				case -712:
					return false;
				case -711:
					return false;
				case -710:
					return false;
				case -709:
					return this.IsUserMode();
				case -706:
					return true;
				case -705:
					return Thread.CurrentThread.CurrentCulture.LCID;
				case -704:
					if (parentInternal != null)
					{
						return AxHost.GetOleColorFromColor(parentInternal.ForeColor);
					}
					return null;
				case -703:
					if (parentInternal != null)
					{
						return AxHost.GetIFontFromFont(parentInternal.Font);
					}
					return null;
				case -702:
				{
					string text = this.GetParentContainer().GetNameForControl(this);
					if (text == null)
					{
						text = "";
					}
					return text;
				}
				case -701:
					if (parentInternal != null)
					{
						return AxHost.GetOleColorFromColor(parentInternal.BackColor);
					}
					return null;
				}
				return null;
			}
			Control control = this;
			while (control != null)
			{
				if (control.RightToLeft == System.Windows.Forms.RightToLeft.No)
				{
					return false;
				}
				if (control.RightToLeft == System.Windows.Forms.RightToLeft.Yes)
				{
					return true;
				}
				if (control.RightToLeft == System.Windows.Forms.RightToLeft.Inherit)
				{
					control = control.Parent;
				}
			}
			return null;
		}

		/// <summary>Requests that an object perform an action in response to an end-user's action.</summary>
		/// <param name="verb">One of the actions enumerated for the object in IOleObject::EnumVerbs.</param>
		// Token: 0x06000A45 RID: 2629 RVA: 0x0001BE94 File Offset: 0x0001A094
		public void DoVerb(int verb)
		{
			Control parentInternal = this.ParentInternal;
			this.GetOleObject().DoVerb(verb, IntPtr.Zero, this.oleSite, -1, (parentInternal != null) ? parentInternal.Handle : IntPtr.Zero, AxHost.FillInRect(new NativeMethods.COMRECT(), base.Bounds));
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001BEE1 File Offset: 0x0001A0E1
		private bool AwaitingDefreezing()
		{
			return this.freezeCount > 0;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001BEEC File Offset: 0x0001A0EC
		private void Freeze(bool v)
		{
			if (v)
			{
				try
				{
					this.GetOleControl().FreezeEvents(-1);
				}
				catch (COMException ex)
				{
				}
				this.freezeCount++;
				return;
			}
			try
			{
				this.GetOleControl().FreezeEvents(0);
			}
			catch (COMException ex2)
			{
			}
			this.freezeCount--;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001BF5C File Offset: 0x0001A15C
		private int UiDeactivate()
		{
			bool flag = this.axState[AxHost.ownDisposing];
			this.axState[AxHost.ownDisposing] = true;
			int num = 0;
			try
			{
				num = this.GetInPlaceObject().UIDeactivate();
			}
			finally
			{
				this.axState[AxHost.ownDisposing] = flag;
			}
			return num;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001BFC0 File Offset: 0x0001A1C0
		private int GetOcState()
		{
			return this.ocState;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001BFC8 File Offset: 0x0001A1C8
		private void SetOcState(int nv)
		{
			this.ocState = nv;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001BFD1 File Offset: 0x0001A1D1
		private string GetLicenseKey()
		{
			return this.GetLicenseKey(this.clsid);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001BFE0 File Offset: 0x0001A1E0
		private string GetLicenseKey(Guid clsid)
		{
			if (this.licenseKey != null || !this.axState[AxHost.needLicenseKey])
			{
				return this.licenseKey;
			}
			try
			{
				UnsafeNativeMethods.IClassFactory2 classFactory = UnsafeNativeMethods.CoGetClassObject(ref clsid, 1, 0, ref AxHost.icf2_Guid);
				NativeMethods.tagLICINFO tagLICINFO = new NativeMethods.tagLICINFO();
				classFactory.GetLicInfo(tagLICINFO);
				if (tagLICINFO.fRuntimeAvailable != 0)
				{
					string[] array = new string[1];
					classFactory.RequestLicKey(0, array);
					this.licenseKey = array[0];
					return this.licenseKey;
				}
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == AxHost.E_NOINTERFACE.ErrorCode)
				{
					return null;
				}
				this.axState[AxHost.needLicenseKey] = false;
			}
			catch (Exception ex2)
			{
				this.axState[AxHost.needLicenseKey] = false;
			}
			return null;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001C0B8 File Offset: 0x0001A2B8
		private void CreateWithoutLicense(Guid clsid)
		{
			object obj = UnsafeNativeMethods.CoCreateInstance(ref clsid, null, 1, ref NativeMethods.ActiveX.IID_IUnknown);
			this.instance = obj;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001C0E0 File Offset: 0x0001A2E0
		private void CreateWithLicense(string license, Guid clsid)
		{
			if (license != null)
			{
				try
				{
					UnsafeNativeMethods.IClassFactory2 classFactory = UnsafeNativeMethods.CoGetClassObject(ref clsid, 1, 0, ref AxHost.icf2_Guid);
					if (classFactory != null)
					{
						classFactory.CreateInstanceLic(null, null, ref NativeMethods.ActiveX.IID_IUnknown, license, out this.instance);
					}
				}
				catch (Exception ex)
				{
				}
			}
			if (this.instance == null)
			{
				this.CreateWithoutLicense(clsid);
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001C13C File Offset: 0x0001A33C
		private void CreateInstance()
		{
			try
			{
				this.instance = this.CreateInstanceCore(this.clsid);
			}
			catch (ExternalException ex)
			{
				if (ex.ErrorCode == -2147221230)
				{
					throw new LicenseException(base.GetType(), this, SR.GetString("AXNoLicenseToUse"));
				}
				throw;
			}
			this.SetOcState(1);
		}

		/// <summary>Called by the system to create the ActiveX control.</summary>
		/// <param name="clsid">The CLSID of the ActiveX control.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the ActiveX control.</returns>
		// Token: 0x06000A50 RID: 2640 RVA: 0x0001C19C File Offset: 0x0001A39C
		protected virtual object CreateInstanceCore(Guid clsid)
		{
			if (this.IsUserMode())
			{
				this.CreateWithLicense(this.licenseKey, clsid);
			}
			else
			{
				this.CreateWithoutLicense(clsid);
			}
			return this.instance;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001C1C4 File Offset: 0x0001A3C4
		private CategoryAttribute GetCategoryForDispid(int dispid)
		{
			NativeMethods.ICategorizeProperties categorizeProperties = this.GetCategorizeProperties();
			if (categorizeProperties == null)
			{
				return null;
			}
			int num = 0;
			try
			{
				categorizeProperties.MapPropertyToCategory(dispid, ref num);
				if (num != 0)
				{
					int num2 = -num;
					if (num2 > 0 && num2 < AxHost.categoryNames.Length && AxHost.categoryNames[num2] != null)
					{
						return AxHost.categoryNames[num2];
					}
					num2 = -num2;
					int num3 = num2;
					if (this.objectDefinedCategoryNames != null)
					{
						CategoryAttribute categoryAttribute = (CategoryAttribute)this.objectDefinedCategoryNames[num3];
						if (categoryAttribute != null)
						{
							return categoryAttribute;
						}
					}
					string text = null;
					if (categorizeProperties.GetCategoryName(num2, CultureInfo.CurrentCulture.LCID, out text) == 0 && text != null)
					{
						CategoryAttribute categoryAttribute = new CategoryAttribute(text);
						if (this.objectDefinedCategoryNames == null)
						{
							this.objectDefinedCategoryNames = new Hashtable();
						}
						this.objectDefinedCategoryNames.Add(num3, categoryAttribute);
						return categoryAttribute;
					}
				}
			}
			catch (Exception ex)
			{
			}
			return null;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001C2B4 File Offset: 0x0001A4B4
		private void SetSelectionStyle(int selectionStyle)
		{
			if (!this.IsUserMode())
			{
				ISelectionService selectionService = this.GetSelectionService();
				this.selectionStyle = selectionStyle;
				if (selectionService != null && selectionService.GetComponentSelected(this))
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)["SelectionStyle"];
					if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(int))
					{
						propertyDescriptor.SetValue(this, selectionStyle);
					}
				}
			}
		}

		/// <summary>Attempts to activate the editing mode of the hosted control.</summary>
		// Token: 0x06000A53 RID: 2643 RVA: 0x0001C31C File Offset: 0x0001A51C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void InvokeEditMode()
		{
			if (this.editMode != 0)
			{
				return;
			}
			this.AddSelectionHandler();
			this.editMode = 2;
			this.SetSelectionStyle(2);
			IntPtr focus = UnsafeNativeMethods.GetFocus();
			try
			{
				this.UiActivate();
			}
			catch (Exception ex)
			{
			}
		}

		/// <summary>Returns a collection of type <see cref="T:System.Attribute" /> for the current object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> with the attributes for the current object.</returns>
		// Token: 0x06000A54 RID: 2644 RVA: 0x0001C368 File Offset: 0x0001A568
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			if (!this.axState[AxHost.editorRefresh] && this.HasPropertyPages())
			{
				this.axState[AxHost.editorRefresh] = true;
				TypeDescriptor.Refresh(base.GetType());
			}
			return TypeDescriptor.GetAttributes(this, true);
		}

		/// <summary>Returns the class name of the current object.</summary>
		/// <returns>Returns <see langword="null" /> in all cases.</returns>
		// Token: 0x06000A55 RID: 2645 RVA: 0x00015C90 File Offset: 0x00013E90
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		/// <summary>Returns the name of the current object.</summary>
		/// <returns>Returns <see langword="null" /> in all cases.</returns>
		// Token: 0x06000A56 RID: 2646 RVA: 0x00015C90 File Offset: 0x00013E90
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		/// <summary>Returns a type converter for the current object.</summary>
		/// <returns>Returns <see langword="null" /> in all cases.</returns>
		// Token: 0x06000A57 RID: 2647 RVA: 0x00015C90 File Offset: 0x00013E90
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		/// <summary>Returns the default event for the current object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents the default event for the current object, or <see langword="null" /> if the object does not have events.</returns>
		// Token: 0x06000A58 RID: 2648 RVA: 0x0001C3A7 File Offset: 0x0001A5A7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		/// <summary>Returns the default property for the current object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property for the current object, or <see langword="null" /> if the object does not have properties.</returns>
		// Token: 0x06000A59 RID: 2649 RVA: 0x0001C3B0 File Offset: 0x0001A5B0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		/// <summary>Returns an editor of the specified type for the current object.</summary>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for the current object.</param>
		/// <returns>An object of the specified type that is the editor for the current object, or <see langword="null" /> if the editor cannot be found.</returns>
		// Token: 0x06000A5A RID: 2650 RVA: 0x0001C3BC File Offset: 0x0001A5BC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			if (editorBaseType != typeof(ComponentEditor))
			{
				return null;
			}
			if (this.editor != null)
			{
				return this.editor;
			}
			if (this.editor == null && this.HasPropertyPages())
			{
				this.editor = new AxHost.AxComponentEditor();
			}
			return this.editor;
		}

		/// <summary>Returns the events for the current object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for the current object.</returns>
		// Token: 0x06000A5B RID: 2651 RVA: 0x0001C40D File Offset: 0x0001A60D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		/// <summary>Returns the events for the current object using the specified attribute array as a filter.</summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for the <see cref="T:System.Windows.Forms.AxHost" /> that match the given set of attributes.</returns>
		// Token: 0x06000A5C RID: 2652 RVA: 0x0001C416 File Offset: 0x0001A616
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001C420 File Offset: 0x0001A620
		private void OnIdle(object sender, EventArgs e)
		{
			if (this.axState[AxHost.refreshProperties])
			{
				TypeDescriptor.Refresh(base.GetType());
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0001C43F File Offset: 0x0001A63F
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x0001C454 File Offset: 0x0001A654
		private bool RefreshAllProperties
		{
			get
			{
				return this.axState[AxHost.refreshProperties];
			}
			set
			{
				this.axState[AxHost.refreshProperties] = value;
				if (value && !this.axState[AxHost.listeningToIdle])
				{
					Application.Idle += this.OnIdle;
					this.axState[AxHost.listeningToIdle] = true;
					return;
				}
				if (!value && this.axState[AxHost.listeningToIdle])
				{
					Application.Idle -= this.OnIdle;
					this.axState[AxHost.listeningToIdle] = false;
				}
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001C4E4 File Offset: 0x0001A6E4
		private PropertyDescriptorCollection FillProperties(Attribute[] attributes)
		{
			if (this.RefreshAllProperties)
			{
				this.RefreshAllProperties = false;
				this.propsStash = null;
				this.attribsStash = null;
			}
			else if (this.propsStash != null)
			{
				if (attributes == null && this.attribsStash == null)
				{
					return this.propsStash;
				}
				if (attributes != null && this.attribsStash != null && attributes.Length == this.attribsStash.Length)
				{
					bool flag = true;
					int num = 0;
					foreach (Attribute attribute in attributes)
					{
						if (!attribute.Equals(this.attribsStash[num++]))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return this.propsStash;
					}
				}
			}
			ArrayList arrayList = new ArrayList();
			if (this.properties == null)
			{
				this.properties = new Hashtable();
			}
			if (this.propertyInfos == null)
			{
				this.propertyInfos = new Hashtable();
				PropertyInfo[] array = base.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
				foreach (PropertyInfo propertyInfo in array)
				{
					this.propertyInfos.Add(propertyInfo.Name, propertyInfo);
				}
			}
			PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(this, null, true);
			if (propertyDescriptorCollection != null)
			{
				for (int k = 0; k < propertyDescriptorCollection.Count; k++)
				{
					if (propertyDescriptorCollection[k].DesignTimeOnly)
					{
						arrayList.Add(propertyDescriptorCollection[k]);
					}
					else
					{
						string name = propertyDescriptorCollection[k].Name;
						PropertyInfo propertyInfo2 = (PropertyInfo)this.propertyInfos[name];
						if (!(propertyInfo2 != null) || propertyInfo2.CanRead)
						{
							if (!this.properties.ContainsKey(name))
							{
								PropertyDescriptor propertyDescriptor;
								if (propertyInfo2 != null)
								{
									propertyDescriptor = new AxHost.AxPropertyDescriptor(propertyDescriptorCollection[k], this);
									((AxHost.AxPropertyDescriptor)propertyDescriptor).UpdateAttributes();
								}
								else
								{
									propertyDescriptor = propertyDescriptorCollection[k];
								}
								this.properties.Add(name, propertyDescriptor);
								arrayList.Add(propertyDescriptor);
							}
							else
							{
								PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)this.properties[name];
								AxHost.AxPropertyDescriptor axPropertyDescriptor = propertyDescriptor2 as AxHost.AxPropertyDescriptor;
								if ((!(propertyInfo2 == null) || axPropertyDescriptor == null) && (!(propertyInfo2 != null) || axPropertyDescriptor != null))
								{
									if (axPropertyDescriptor != null)
									{
										axPropertyDescriptor.UpdateAttributes();
									}
									arrayList.Add(propertyDescriptor2);
								}
							}
						}
					}
				}
				if (attributes != null)
				{
					Attribute attribute2 = null;
					foreach (Attribute attribute3 in attributes)
					{
						if (attribute3 is BrowsableAttribute)
						{
							attribute2 = attribute3;
						}
					}
					if (attribute2 != null)
					{
						ArrayList arrayList2 = null;
						foreach (object obj in arrayList)
						{
							PropertyDescriptor propertyDescriptor3 = (PropertyDescriptor)obj;
							if (propertyDescriptor3 is AxHost.AxPropertyDescriptor)
							{
								Attribute attribute4 = propertyDescriptor3.Attributes[typeof(BrowsableAttribute)];
								if (attribute4 != null && !attribute4.Equals(attribute2))
								{
									if (arrayList2 == null)
									{
										arrayList2 = new ArrayList();
									}
									arrayList2.Add(propertyDescriptor3);
								}
							}
						}
						if (arrayList2 != null)
						{
							foreach (object obj2 in arrayList2)
							{
								arrayList.Remove(obj2);
							}
						}
					}
				}
			}
			PropertyDescriptor[] array3 = new PropertyDescriptor[arrayList.Count];
			arrayList.CopyTo(array3, 0);
			this.propsStash = new PropertyDescriptorCollection(array3);
			this.attribsStash = attributes;
			return this.propsStash;
		}

		/// <summary>Returns the properties for the current object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the events for the current object.</returns>
		// Token: 0x06000A61 RID: 2657 RVA: 0x0001C86C File Offset: 0x0001AA6C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return this.FillProperties(null);
		}

		/// <summary>Returns the properties for the current object using the specified attribute array as a filter.</summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the events for the current <see cref="T:System.Windows.Forms.AxHost" /> that match the given set of attributes.</returns>
		// Token: 0x06000A62 RID: 2658 RVA: 0x0001C875 File Offset: 0x0001AA75
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return this.FillProperties(attributes);
		}

		/// <summary>Returns the object that owns the specified value.</summary>
		/// <param name="pd">Not used.</param>
		/// <returns>The current object.</returns>
		// Token: 0x06000A63 RID: 2659 RVA: 0x00006A49 File Offset: 0x00004C49
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001C880 File Offset: 0x0001AA80
		private AxHost.AxPropertyDescriptor GetPropertyDescriptorFromDispid(int dispid)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = this.FillProperties(null);
			foreach (object obj in propertyDescriptorCollection)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				AxHost.AxPropertyDescriptor axPropertyDescriptor = propertyDescriptor as AxHost.AxPropertyDescriptor;
				if (axPropertyDescriptor != null && axPropertyDescriptor.Dispid == dispid)
				{
					return axPropertyDescriptor;
				}
			}
			return null;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001C8F4 File Offset: 0x0001AAF4
		private void ActivateAxControl()
		{
			if (this.QuickActivate())
			{
				this.DepersistControl();
			}
			else
			{
				this.SlowActivate();
			}
			this.SetOcState(2);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001C913 File Offset: 0x0001AB13
		private void DepersistFromIPropertyBag(UnsafeNativeMethods.IPropertyBag propBag)
		{
			this.iPersistPropBag.Load(propBag, null);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001C922 File Offset: 0x0001AB22
		private void DepersistFromIStream(UnsafeNativeMethods.IStream istream)
		{
			this.storageType = 0;
			this.iPersistStream.Load(istream);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001C937 File Offset: 0x0001AB37
		private void DepersistFromIStreamInit(UnsafeNativeMethods.IStream istream)
		{
			this.storageType = 1;
			this.iPersistStreamInit.Load(istream);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001C94C File Offset: 0x0001AB4C
		private void DepersistFromIStorage(UnsafeNativeMethods.IStorage storage)
		{
			this.storageType = 2;
			if (storage != null)
			{
				int num = this.iPersistStorage.Load(storage);
			}
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001C974 File Offset: 0x0001AB74
		private void DepersistControl()
		{
			this.Freeze(true);
			if (this.ocxState != null)
			{
				switch (this.ocxState.Type)
				{
				case 0:
					try
					{
						this.iPersistStream = (UnsafeNativeMethods.IPersistStream)this.instance;
						this.DepersistFromIStream(this.ocxState.GetStream());
						goto IL_1D5;
					}
					catch (Exception ex)
					{
						goto IL_1D5;
					}
					break;
				case 1:
					break;
				case 2:
					try
					{
						this.iPersistStorage = (UnsafeNativeMethods.IPersistStorage)this.instance;
						this.DepersistFromIStorage(this.ocxState.GetStorage());
						goto IL_1D5;
					}
					catch (Exception ex2)
					{
						goto IL_1D5;
					}
					goto IL_1C5;
				default:
					goto IL_1C5;
				}
				if (this.instance is UnsafeNativeMethods.IPersistStreamInit)
				{
					try
					{
						this.iPersistStreamInit = (UnsafeNativeMethods.IPersistStreamInit)this.instance;
						this.DepersistFromIStreamInit(this.ocxState.GetStream());
					}
					catch (Exception ex3)
					{
					}
					this.GetControlEnabled();
					goto IL_1D5;
				}
				this.ocxState.Type = 0;
				this.DepersistControl();
				return;
				IL_1C5:
				throw new InvalidOperationException(SR.GetString("UnableToInitComponent"));
				IL_1D5:
				if (this.ocxState.GetPropBag() != null)
				{
					try
					{
						this.iPersistPropBag = (UnsafeNativeMethods.IPersistPropertyBag)this.instance;
						this.DepersistFromIPropertyBag(this.ocxState.GetPropBag());
					}
					catch (Exception ex4)
					{
					}
				}
				return;
			}
			if (this.instance is UnsafeNativeMethods.IPersistStreamInit)
			{
				this.iPersistStreamInit = (UnsafeNativeMethods.IPersistStreamInit)this.instance;
				try
				{
					this.storageType = 1;
					this.iPersistStreamInit.InitNew();
				}
				catch (Exception ex5)
				{
				}
				return;
			}
			if (this.instance is UnsafeNativeMethods.IPersistStream)
			{
				this.storageType = 0;
				this.iPersistStream = (UnsafeNativeMethods.IPersistStream)this.instance;
				return;
			}
			if (this.instance is UnsafeNativeMethods.IPersistStorage)
			{
				this.storageType = 2;
				this.ocxState = new AxHost.State(this);
				this.iPersistStorage = (UnsafeNativeMethods.IPersistStorage)this.instance;
				try
				{
					this.iPersistStorage.InitNew(this.ocxState.GetStorage());
				}
				catch (Exception ex6)
				{
				}
				return;
			}
			if (this.instance is UnsafeNativeMethods.IPersistPropertyBag)
			{
				this.iPersistPropBag = (UnsafeNativeMethods.IPersistPropertyBag)this.instance;
				try
				{
					this.iPersistPropBag.InitNew();
				}
				catch (Exception ex7)
				{
				}
			}
			throw new InvalidOperationException(SR.GetString("UnableToInitComponent"));
		}

		/// <summary>Retrieves a reference to the underlying ActiveX control.</summary>
		/// <returns>An object that represents the ActiveX control.</returns>
		// Token: 0x06000A6B RID: 2667 RVA: 0x0001CBE4 File Offset: 0x0001ADE4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object GetOcx()
		{
			return this.instance;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001CBEC File Offset: 0x0001ADEC
		private object GetOcxCreate()
		{
			if (this.instance == null)
			{
				this.CreateInstance();
				this.RealizeStyles();
				this.AttachInterfaces();
				this.oleSite.OnOcxCreate();
			}
			return this.instance;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0001CC1C File Offset: 0x0001AE1C
		private void StartEvents()
		{
			if (!this.axState[AxHost.sinkAttached])
			{
				try
				{
					this.CreateSink();
					this.oleSite.StartEvents();
				}
				catch (Exception ex)
				{
				}
				this.axState[AxHost.sinkAttached] = true;
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0001CC74 File Offset: 0x0001AE74
		private void StopEvents()
		{
			if (this.axState[AxHost.sinkAttached])
			{
				try
				{
					this.DetachSink();
				}
				catch (Exception ex)
				{
				}
				this.axState[AxHost.sinkAttached] = false;
			}
			this.oleSite.StopEvents();
		}

		/// <summary>Called by the control to prepare it for listening to events.</summary>
		// Token: 0x06000A6F RID: 2671 RVA: 0x000070A6 File Offset: 0x000052A6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void CreateSink()
		{
		}

		/// <summary>Called by the control when it stops listening to events.</summary>
		// Token: 0x06000A70 RID: 2672 RVA: 0x000070A6 File Offset: 0x000052A6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void DetachSink()
		{
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001CCCC File Offset: 0x0001AECC
		private bool CanShowPropertyPages()
		{
			return this.GetOcState() >= 2 && this.GetOcx() is NativeMethods.ISpecifyPropertyPages;
		}

		/// <summary>Determines if the ActiveX control has a property page.</summary>
		/// <returns>
		///   <see langword="true" /> if the ActiveX control has a property page; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A72 RID: 2674 RVA: 0x0001CCE8 File Offset: 0x0001AEE8
		public bool HasPropertyPages()
		{
			if (!this.CanShowPropertyPages())
			{
				return false;
			}
			NativeMethods.ISpecifyPropertyPages specifyPropertyPages = (NativeMethods.ISpecifyPropertyPages)this.GetOcx();
			try
			{
				NativeMethods.tagCAUUID tagCAUUID = new NativeMethods.tagCAUUID();
				try
				{
					specifyPropertyPages.GetPages(tagCAUUID);
					if (tagCAUUID.cElems > 0)
					{
						return true;
					}
				}
				finally
				{
					if (tagCAUUID.pElems != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(tagCAUUID.pElems);
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001CD6C File Offset: 0x0001AF6C
		private unsafe void ShowPropertyPageForDispid(int dispid, Guid guid)
		{
			try
			{
				IntPtr iunknownForObject = Marshal.GetIUnknownForObject(this.GetOcx());
				UnsafeNativeMethods.OleCreatePropertyFrameIndirect(new NativeMethods.OCPFIPARAMS
				{
					hwndOwner = ((this.ContainingControl == null) ? IntPtr.Zero : this.ContainingControl.Handle),
					lpszCaption = base.Name,
					ppUnk = (IntPtr)(&iunknownForObject),
					uuid = (IntPtr)(&guid),
					dispidInitial = dispid
				});
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>Announces to the component change service that the <see cref="T:System.Windows.Forms.AxHost" /> has changed.</summary>
		// Token: 0x06000A74 RID: 2676 RVA: 0x0001CDF8 File Offset: 0x0001AFF8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void MakeDirty()
		{
			ISite site = this.Site;
			if (site == null)
			{
				return;
			}
			IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
			if (componentChangeService == null)
			{
				return;
			}
			componentChangeService.OnComponentChanging(this, null);
			componentChangeService.OnComponentChanged(this, null, null, null);
		}

		/// <summary>Displays the property pages associated with the ActiveX control.</summary>
		// Token: 0x06000A75 RID: 2677 RVA: 0x0001CE3C File Offset: 0x0001B03C
		public void ShowPropertyPages()
		{
			if (this.ParentInternal == null)
			{
				return;
			}
			if (!this.ParentInternal.IsHandleCreated)
			{
				return;
			}
			this.ShowPropertyPages(this.ParentInternal);
		}

		/// <summary>Displays the property pages associated with the ActiveX control assigned to the specified parent control.</summary>
		/// <param name="control">The parent <see cref="T:System.Windows.Forms.Control" /> of the ActiveX control.</param>
		// Token: 0x06000A76 RID: 2678 RVA: 0x0001CE64 File Offset: 0x0001B064
		public void ShowPropertyPages(Control control)
		{
			try
			{
				if (this.CanShowPropertyPages())
				{
					NativeMethods.ISpecifyPropertyPages specifyPropertyPages = (NativeMethods.ISpecifyPropertyPages)this.GetOcx();
					NativeMethods.tagCAUUID tagCAUUID = new NativeMethods.tagCAUUID();
					try
					{
						specifyPropertyPages.GetPages(tagCAUUID);
						if (tagCAUUID.cElems <= 0)
						{
							return;
						}
					}
					catch
					{
						return;
					}
					IDesignerHost designerHost = null;
					if (this.Site != null)
					{
						designerHost = (IDesignerHost)this.Site.GetService(typeof(IDesignerHost));
					}
					DesignerTransaction designerTransaction = null;
					try
					{
						if (designerHost != null)
						{
							designerTransaction = designerHost.CreateTransaction(SR.GetString("AXEditProperties"));
						}
						string text = null;
						object ocx = this.GetOcx();
						IntPtr intPtr = ((this.ContainingControl == null) ? IntPtr.Zero : this.ContainingControl.Handle);
						SafeNativeMethods.OleCreatePropertyFrame(new HandleRef(this, intPtr), 0, 0, text, 1, ref ocx, tagCAUUID.cElems, new HandleRef(null, tagCAUUID.pElems), Application.CurrentCulture.LCID, 0, IntPtr.Zero);
					}
					finally
					{
						if (this.oleSite != null)
						{
							((UnsafeNativeMethods.IPropertyNotifySink)this.oleSite).OnChanged(-1);
						}
						if (designerTransaction != null)
						{
							designerTransaction.Commit();
						}
						if (tagCAUUID.pElems != IntPtr.Zero)
						{
							Marshal.FreeCoTaskMem(tagCAUUID.pElems);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001CFD8 File Offset: 0x0001B1D8
		internal override IntPtr InitializeDCForWmCtlColor(IntPtr dc, int msg)
		{
			if (this.isMaskEdit)
			{
				return base.InitializeDCForWmCtlColor(dc, msg);
			}
			return IntPtr.Zero;
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06000A78 RID: 2680 RVA: 0x0001CFF0 File Offset: 0x0001B1F0
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 83)
			{
				if (msg <= 21)
				{
					if (msg != 2)
					{
						if (msg == 8)
						{
							this.hwndFocus = m.WParam;
							try
							{
								base.WndProc(ref m);
								return;
							}
							finally
							{
								this.hwndFocus = IntPtr.Zero;
							}
							goto IL_F9;
						}
						if (msg - 20 > 1)
						{
							goto IL_1D0;
						}
					}
					else
					{
						if (this.GetOcState() >= 4)
						{
							UnsafeNativeMethods.IOleInPlaceObject inPlaceObject = this.GetInPlaceObject();
							IntPtr intPtr;
							if (NativeMethods.Succeeded(inPlaceObject.GetWindow(out intPtr)))
							{
								Application.ParkHandle(new HandleRef(inPlaceObject, intPtr), DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED);
							}
						}
						bool state = base.GetState(2);
						this.TransitionDownTo(2);
						this.DetachAndForward(ref m);
						if (state != base.GetState(2))
						{
							base.SetState(2, state);
							return;
						}
						return;
					}
				}
				else if (msg != 32 && msg != 43)
				{
					if (msg != 83)
					{
						goto IL_1D0;
					}
					base.WndProc(ref m);
					this.DefWndProc(ref m);
					return;
				}
			}
			else if (msg <= 257)
			{
				if (msg != 123)
				{
					if (msg != 130)
					{
						if (msg != 257)
						{
							goto IL_1D0;
						}
						if (this.axState[AxHost.processingKeyUp])
						{
							return;
						}
						this.axState[AxHost.processingKeyUp] = true;
						try
						{
							if (base.PreProcessControlMessage(ref m) != PreProcessControlState.MessageProcessed)
							{
								this.DefWndProc(ref m);
							}
							return;
						}
						finally
						{
							this.axState[AxHost.processingKeyUp] = false;
						}
					}
					this.DetachAndForward(ref m);
					return;
				}
				this.DefWndProc(ref m);
				return;
			}
			else
			{
				if (msg == 273)
				{
					goto IL_F9;
				}
				switch (msg)
				{
				case 513:
				case 516:
				case 519:
					if (this.IsUserMode())
					{
						base.Focus();
					}
					this.DefWndProc(ref m);
					return;
				case 514:
				case 515:
				case 517:
				case 518:
				case 520:
				case 521:
					break;
				default:
					if (msg != 8277)
					{
						goto IL_1D0;
					}
					break;
				}
			}
			this.DefWndProc(ref m);
			return;
			IL_F9:
			if (!Control.ReflectMessageInternal(m.LParam, ref m))
			{
				this.DefWndProc(ref m);
				return;
			}
			return;
			IL_1D0:
			if (m.Msg == this.REGMSG_MSG)
			{
				m.Result = (IntPtr)123;
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001D20C File Offset: 0x0001B40C
		private void DetachAndForward(ref Message m)
		{
			IntPtr handleNoCreate = this.GetHandleNoCreate();
			this.DetachWindow();
			if (handleNoCreate != IntPtr.Zero)
			{
				IntPtr windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, handleNoCreate), -4);
				m.Result = UnsafeNativeMethods.CallWindowProc(windowLong, handleNoCreate, m.Msg, m.WParam, m.LParam);
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001D264 File Offset: 0x0001B464
		private void DetachWindow()
		{
			if (base.IsHandleCreated)
			{
				this.OnHandleDestroyed(EventArgs.Empty);
				for (Control control = this; control != null; control = control.ParentInternal)
				{
				}
				base.WindowReleaseHandle();
			}
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0001D298 File Offset: 0x0001B498
		private void InformOfNewHandle()
		{
			for (Control control = this; control != null; control = control.ParentInternal)
			{
			}
			this.wndprocAddr = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -4);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001D2CC File Offset: 0x0001B4CC
		private void AttachWindow(IntPtr hwnd)
		{
			if (!this.axState[AxHost.fFakingWindow])
			{
				base.WindowAssignHandle(hwnd, this.axState[AxHost.assignUniqueID]);
			}
			base.UpdateZOrder();
			Size size = base.Size;
			base.UpdateBounds();
			Size extent = this.GetExtent();
			Point location = base.Location;
			if (size.Width < extent.Width || size.Height < extent.Height)
			{
				base.Bounds = new Rectangle(location.X, location.Y, extent.Width, extent.Height);
			}
			else
			{
				Size size2 = this.SetExtent(size.Width, size.Height);
				if (!size2.Equals(size))
				{
					base.Bounds = new Rectangle(location.X, location.Y, size2.Width, size2.Height);
				}
			}
			this.OnHandleCreated(EventArgs.Empty);
			this.InformOfNewHandle();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000A7D RID: 2685 RVA: 0x0001D3CE File Offset: 0x0001B5CE
		protected override void OnHandleCreated(EventArgs e)
		{
			if (Application.OleRequired() != ApartmentState.STA)
			{
				throw new ThreadStateException(SR.GetString("ThreadMustBeSTA"));
			}
			base.SetAcceptDrops(this.AllowDrop);
			base.RaiseCreateHandleEvent(e);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001D3FA File Offset: 0x0001B5FA
		private int Pix2HM(int pix, int logP)
		{
			return (2540 * pix + (logP >> 1)) / logP;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001D409 File Offset: 0x0001B609
		private int HM2Pix(int hm, int logP)
		{
			return (logP * hm + 1270) / 2540;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001D41C File Offset: 0x0001B61C
		private bool QuickActivate()
		{
			if (!(this.instance is UnsafeNativeMethods.IQuickActivate))
			{
				return false;
			}
			UnsafeNativeMethods.IQuickActivate quickActivate = (UnsafeNativeMethods.IQuickActivate)this.instance;
			UnsafeNativeMethods.tagQACONTAINER tagQACONTAINER = new UnsafeNativeMethods.tagQACONTAINER();
			UnsafeNativeMethods.tagQACONTROL tagQACONTROL = new UnsafeNativeMethods.tagQACONTROL();
			tagQACONTAINER.pClientSite = this.oleSite;
			tagQACONTAINER.pPropertyNotifySink = this.oleSite;
			tagQACONTAINER.pFont = AxHost.GetIFontFromFont(this.GetParentContainer().parent.Font);
			tagQACONTAINER.dwAppearance = 0;
			tagQACONTAINER.lcid = Application.CurrentCulture.LCID;
			Control parentInternal = this.ParentInternal;
			if (parentInternal != null)
			{
				tagQACONTAINER.colorFore = AxHost.GetOleColorFromColor(parentInternal.ForeColor);
				tagQACONTAINER.colorBack = AxHost.GetOleColorFromColor(parentInternal.BackColor);
			}
			else
			{
				tagQACONTAINER.colorFore = AxHost.GetOleColorFromColor(SystemColors.WindowText);
				tagQACONTAINER.colorBack = AxHost.GetOleColorFromColor(SystemColors.Window);
			}
			tagQACONTAINER.dwAmbientFlags = 224;
			if (this.IsUserMode())
			{
				tagQACONTAINER.dwAmbientFlags |= 4;
			}
			try
			{
				quickActivate.QuickActivate(tagQACONTAINER, tagQACONTROL);
			}
			catch (Exception ex)
			{
				this.DisposeAxControl();
				return false;
			}
			this.miscStatusBits = tagQACONTROL.dwMiscStatus;
			this.ParseMiscBits(this.miscStatusBits);
			return true;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001D550 File Offset: 0x0001B750
		internal override void DisposeAxControls()
		{
			this.axState[AxHost.rejectSelection] = true;
			base.DisposeAxControls();
			this.TransitionDownTo(0);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001D570 File Offset: 0x0001B770
		private bool GetControlEnabled()
		{
			bool flag;
			try
			{
				flag = base.IsHandleCreated;
			}
			catch (Exception ex)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001D59C File Offset: 0x0001B79C
		internal override bool CanSelectCore()
		{
			return this.GetControlEnabled() && !this.axState[AxHost.rejectSelection] && base.CanSelectCore();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000A84 RID: 2692 RVA: 0x0001D5C0 File Offset: 0x0001B7C0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.TransitionDownTo(0);
				if (this.newParent != null)
				{
					this.newParent.Dispose();
				}
				if (this.oleSite != null)
				{
					this.oleSite.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001D5F9 File Offset: 0x0001B7F9
		private bool GetSiteOwnsDeactivation()
		{
			return this.axState[AxHost.ownDisposing];
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001D60B File Offset: 0x0001B80B
		private void DisposeAxControl()
		{
			if (this.GetParentContainer() != null)
			{
				this.GetParentContainer().RemoveControl(this);
			}
			this.TransitionDownTo(2);
			if (this.GetOcState() == 2)
			{
				this.GetOleObject().SetClientSite(null);
				this.SetOcState(1);
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001D648 File Offset: 0x0001B848
		private void ReleaseAxControl()
		{
			int num = this.NoComponentChangeEvents;
			this.NoComponentChangeEvents = num + 1;
			ContainerControl containerControl = this.ContainingControl;
			if (containerControl != null)
			{
				containerControl.VisibleChanged -= this.onContainerVisibleChanged;
			}
			try
			{
				if (this.instance != null)
				{
					Marshal.FinalReleaseComObject(this.instance);
					this.instance = null;
					this.iOleInPlaceObject = null;
					this.iOleObject = null;
					this.iOleControl = null;
					this.iOleInPlaceActiveObject = null;
					this.iOleInPlaceActiveObjectExternal = null;
					this.iPerPropertyBrowsing = null;
					this.iCategorizeProperties = null;
					this.iPersistStream = null;
					this.iPersistStreamInit = null;
					this.iPersistStorage = null;
				}
				this.axState[AxHost.checkedIppb] = false;
				this.axState[AxHost.checkedCP] = false;
				this.axState[AxHost.disposed] = true;
				this.freezeCount = 0;
				this.axState[AxHost.sinkAttached] = false;
				this.wndprocAddr = IntPtr.Zero;
				this.SetOcState(0);
			}
			finally
			{
				num = this.NoComponentChangeEvents;
				this.NoComponentChangeEvents = num - 1;
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001D760 File Offset: 0x0001B960
		private void ParseMiscBits(int bits)
		{
			this.axState[AxHost.fOwnWindow] = (bits & 1024) != 0 && this.IsUserMode();
			this.axState[AxHost.fSimpleFrame] = (bits & 65536) != 0;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001D7A0 File Offset: 0x0001B9A0
		private void SlowActivate()
		{
			bool flag = false;
			if ((this.miscStatusBits & 131072) != 0)
			{
				this.GetOleObject().SetClientSite(this.oleSite);
				flag = true;
			}
			this.DepersistControl();
			if (!flag)
			{
				this.GetOleObject().SetClientSite(this.oleSite);
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001D7EC File Offset: 0x0001B9EC
		private static NativeMethods.COMRECT FillInRect(NativeMethods.COMRECT dest, Rectangle source)
		{
			dest.left = source.X;
			dest.top = source.Y;
			dest.right = source.Width + source.X;
			dest.bottom = source.Height + source.Y;
			return dest;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001D840 File Offset: 0x0001BA40
		private AxHost.AxContainer GetParentContainer()
		{
			IntSecurity.GetParent.Demand();
			if (this.container == null)
			{
				this.container = AxHost.AxContainer.FindContainerForControl(this);
			}
			if (this.container == null)
			{
				ContainerControl containerControl = this.ContainingControl;
				if (containerControl == null)
				{
					if (this.newParent == null)
					{
						this.newParent = new ContainerControl();
						this.axContainer = this.newParent.CreateAxContainer();
						this.axContainer.AddControl(this);
					}
					return this.axContainer;
				}
				this.container = containerControl.CreateAxContainer();
				this.container.AddControl(this);
				this.containingControl = containerControl;
			}
			return this.container;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001D8D9 File Offset: 0x0001BAD9
		private UnsafeNativeMethods.IOleControl GetOleControl()
		{
			if (this.iOleControl == null)
			{
				this.iOleControl = (UnsafeNativeMethods.IOleControl)this.instance;
			}
			return this.iOleControl;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0001D8FC File Offset: 0x0001BAFC
		private UnsafeNativeMethods.IOleInPlaceActiveObject GetInPlaceActiveObject()
		{
			if (this.iOleInPlaceActiveObjectExternal != null)
			{
				return this.iOleInPlaceActiveObjectExternal;
			}
			if (this.iOleInPlaceActiveObject == null)
			{
				try
				{
					this.iOleInPlaceActiveObject = (UnsafeNativeMethods.IOleInPlaceActiveObject)this.instance;
				}
				catch (InvalidCastException ex)
				{
				}
			}
			return this.iOleInPlaceActiveObject;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001D94C File Offset: 0x0001BB4C
		private UnsafeNativeMethods.IOleObject GetOleObject()
		{
			if (this.iOleObject == null)
			{
				this.iOleObject = (UnsafeNativeMethods.IOleObject)this.instance;
			}
			return this.iOleObject;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001D96D File Offset: 0x0001BB6D
		private UnsafeNativeMethods.IOleInPlaceObject GetInPlaceObject()
		{
			if (this.iOleInPlaceObject == null)
			{
				this.iOleInPlaceObject = (UnsafeNativeMethods.IOleInPlaceObject)this.instance;
			}
			return this.iOleInPlaceObject;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001D990 File Offset: 0x0001BB90
		private NativeMethods.ICategorizeProperties GetCategorizeProperties()
		{
			if (this.iCategorizeProperties == null && !this.axState[AxHost.checkedCP] && this.instance != null)
			{
				this.axState[AxHost.checkedCP] = true;
				if (this.instance is NativeMethods.ICategorizeProperties)
				{
					this.iCategorizeProperties = (NativeMethods.ICategorizeProperties)this.instance;
				}
			}
			return this.iCategorizeProperties;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001D9F4 File Offset: 0x0001BBF4
		private NativeMethods.IPerPropertyBrowsing GetPerPropertyBrowsing()
		{
			if (this.iPerPropertyBrowsing == null && !this.axState[AxHost.checkedIppb] && this.instance != null)
			{
				this.axState[AxHost.checkedIppb] = true;
				if (this.instance is NativeMethods.IPerPropertyBrowsing)
				{
					this.iPerPropertyBrowsing = (NativeMethods.IPerPropertyBrowsing)this.instance;
				}
			}
			return this.iPerPropertyBrowsing;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001DA58 File Offset: 0x0001BC58
		private static object GetPICTDESCFromPicture(Image image)
		{
			Bitmap bitmap = image as Bitmap;
			if (bitmap != null)
			{
				return new NativeMethods.PICTDESCbmp(bitmap);
			}
			Metafile metafile = image as Metafile;
			if (metafile != null)
			{
				return new NativeMethods.PICTDESCemf(metafile);
			}
			throw new ArgumentException(SR.GetString("AXUnknownImage"), "image");
		}

		/// <summary>Returns an OLE <see langword="IPicture" /> object corresponding to the specified <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the OLE <see langword="IPicture" /> object.</returns>
		// Token: 0x06000A93 RID: 2707 RVA: 0x0001DA9C File Offset: 0x0001BC9C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static object GetIPictureFromPicture(Image image)
		{
			if (image == null)
			{
				return null;
			}
			object pictdescfromPicture = AxHost.GetPICTDESCFromPicture(image);
			return UnsafeNativeMethods.OleCreateIPictureIndirect(pictdescfromPicture, ref AxHost.ipicture_Guid, true);
		}

		/// <summary>Returns an OLE <see langword="IPicture" /> object corresponding to the specified <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <param name="cursor">A <see cref="T:System.Windows.Forms.Cursor" />, which is an image that represents the Windows mouse pointer.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the OLE <see langword="IPicture" /> object.</returns>
		// Token: 0x06000A94 RID: 2708 RVA: 0x0001DAC4 File Offset: 0x0001BCC4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static object GetIPictureFromCursor(Cursor cursor)
		{
			if (cursor == null)
			{
				return null;
			}
			NativeMethods.PICTDESCicon pictdescicon = new NativeMethods.PICTDESCicon(Icon.FromHandle(cursor.Handle));
			return UnsafeNativeMethods.OleCreateIPictureIndirect(pictdescicon, ref AxHost.ipicture_Guid, true);
		}

		/// <summary>Returns an OLE <see langword="IPictureDisp" /> object corresponding to the specified <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the OLE <see langword="IPictureDisp" /> object.</returns>
		// Token: 0x06000A95 RID: 2709 RVA: 0x0001DAFC File Offset: 0x0001BCFC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static object GetIPictureDispFromPicture(Image image)
		{
			if (image == null)
			{
				return null;
			}
			object pictdescfromPicture = AxHost.GetPICTDESCFromPicture(image);
			return UnsafeNativeMethods.OleCreateIPictureDispIndirect(pictdescfromPicture, ref AxHost.ipictureDisp_Guid, true);
		}

		/// <summary>Returns an <see cref="T:System.Drawing.Image" /> corresponding to the specified OLE <see langword="IPicture" /> object.</summary>
		/// <param name="picture">The <see langword="IPicture" /> to convert.</param>
		/// <returns>An <see cref="T:System.Drawing.Image" /> representing the <see langword="IPicture" />.</returns>
		// Token: 0x06000A96 RID: 2710 RVA: 0x0001DB24 File Offset: 0x0001BD24
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static Image GetPictureFromIPicture(object picture)
		{
			if (picture == null)
			{
				return null;
			}
			IntPtr intPtr = IntPtr.Zero;
			UnsafeNativeMethods.IPicture picture2 = (UnsafeNativeMethods.IPicture)picture;
			int pictureType = (int)picture2.GetPictureType();
			if (pictureType == 1)
			{
				try
				{
					intPtr = picture2.GetHPal();
				}
				catch (COMException)
				{
				}
			}
			return AxHost.GetPictureFromParams(picture2, picture2.GetHandle(), pictureType, intPtr, picture2.GetWidth(), picture2.GetHeight());
		}

		/// <summary>Returns an <see cref="T:System.Drawing.Image" /> corresponding to the specified OLE <see langword="IPictureDisp" /> object.</summary>
		/// <param name="picture">The <see langword="IPictureDisp" /> to convert.</param>
		/// <returns>An <see cref="T:System.Drawing.Image" /> representing the <see langword="IPictureDisp" />.</returns>
		// Token: 0x06000A97 RID: 2711 RVA: 0x0001DB84 File Offset: 0x0001BD84
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static Image GetPictureFromIPictureDisp(object picture)
		{
			if (picture == null)
			{
				return null;
			}
			IntPtr intPtr = IntPtr.Zero;
			UnsafeNativeMethods.IPictureDisp pictureDisp = (UnsafeNativeMethods.IPictureDisp)picture;
			int pictureType = (int)pictureDisp.PictureType;
			if (pictureType == 1)
			{
				try
				{
					intPtr = pictureDisp.HPal;
				}
				catch (COMException)
				{
				}
			}
			return AxHost.GetPictureFromParams(pictureDisp, pictureDisp.Handle, pictureType, intPtr, pictureDisp.Width, pictureDisp.Height);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001DBE4 File Offset: 0x0001BDE4
		private static Image GetPictureFromParams(object pict, IntPtr handle, int type, IntPtr paletteHandle, int width, int height)
		{
			switch (type)
			{
			case -1:
				return null;
			case 0:
				return null;
			case 1:
				return Image.FromHbitmap(handle, paletteHandle);
			case 2:
				return (Image)new Metafile(handle, new WmfPlaceableFileHeader
				{
					BboxRight = (short)width,
					BboxBottom = (short)height
				}, false).Clone();
			case 3:
				return (Image)Icon.FromHandle(handle).Clone();
			case 4:
				return (Image)new Metafile(handle, false).Clone();
			default:
				throw new ArgumentException(SR.GetString("AXUnknownImage"), "type");
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0001DC84 File Offset: 0x0001BE84
		private static NativeMethods.FONTDESC GetFONTDESCFromFont(Font font)
		{
			NativeMethods.FONTDESC fontdesc = null;
			if (AxHost.fontTable == null)
			{
				AxHost.fontTable = new Hashtable();
			}
			else
			{
				fontdesc = (NativeMethods.FONTDESC)AxHost.fontTable[font];
			}
			if (fontdesc == null)
			{
				fontdesc = new NativeMethods.FONTDESC();
				fontdesc.lpstrName = font.Name;
				fontdesc.cySize = (long)(font.SizeInPoints * 10000f);
				NativeMethods.LOGFONT logfont = new NativeMethods.LOGFONT();
				font.ToLogFont(logfont);
				fontdesc.sWeight = (short)logfont.lfWeight;
				fontdesc.sCharset = (short)logfont.lfCharSet;
				fontdesc.fItalic = font.Italic;
				fontdesc.fUnderline = font.Underline;
				fontdesc.fStrikethrough = font.Strikeout;
				AxHost.fontTable[font] = fontdesc;
			}
			return fontdesc;
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Color" /> structure that corresponds to the specified OLE color value.</summary>
		/// <param name="color">The OLE color value to translate.</param>
		/// <returns>The <see cref="T:System.Drawing.Color" /> structure that represents the translated OLE color value.</returns>
		// Token: 0x06000A9A RID: 2714 RVA: 0x0001DD36 File Offset: 0x0001BF36
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static Color GetColorFromOleColor(uint color)
		{
			return ColorTranslator.FromOle((int)color);
		}

		/// <summary>Returns an OLE color value that corresponds to the specified <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <param name="color">The <see cref="T:System.Drawing.Color" /> structure to translate.</param>
		/// <returns>The OLE color value that represents the translated <see cref="T:System.Drawing.Color" /> structure.</returns>
		// Token: 0x06000A9B RID: 2715 RVA: 0x0001DD3E File Offset: 0x0001BF3E
		[CLSCompliant(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static uint GetOleColorFromColor(Color color)
		{
			return (uint)ColorTranslator.ToOle(color);
		}

		/// <summary>Returns an OLE IFont object created from the specified <see cref="T:System.Drawing.Font" /> object.</summary>
		/// <param name="font">The font to create an IFont object from.</param>
		/// <returns>The IFont object created from the specified font, or <see langword="null" /> if <paramref name="font" /> is <see langword="null" /> or the IFont could not be created.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Font.Unit" /> property value is not <see cref="F:System.Drawing.GraphicsUnit.Point" />.</exception>
		// Token: 0x06000A9C RID: 2716 RVA: 0x0001DD48 File Offset: 0x0001BF48
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static object GetIFontFromFont(Font font)
		{
			if (font == null)
			{
				return null;
			}
			if (font.Unit != GraphicsUnit.Point)
			{
				throw new ArgumentException(SR.GetString("AXFontUnitNotPoint"), "font");
			}
			object obj;
			try
			{
				obj = UnsafeNativeMethods.OleCreateIFontIndirect(AxHost.GetFONTDESCFromFont(font), ref AxHost.ifont_Guid);
			}
			catch
			{
				obj = null;
			}
			return obj;
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Font" /> created from the specified OLE IFont object.</summary>
		/// <param name="font">The IFont to create a <see cref="T:System.Drawing.Font" /> from.</param>
		/// <returns>The <see cref="T:System.Drawing.Font" /> created from the specified IFont, <see cref="P:System.Windows.Forms.Control.DefaultFont" /> if the font could not be created, or <see langword="null" /> if <paramref name="font" /> is <see langword="null" />.</returns>
		// Token: 0x06000A9D RID: 2717 RVA: 0x0001DDA4 File Offset: 0x0001BFA4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static Font GetFontFromIFont(object font)
		{
			if (font == null)
			{
				return null;
			}
			UnsafeNativeMethods.IFont font2 = (UnsafeNativeMethods.IFont)font;
			Font font4;
			try
			{
				Font font3 = Font.FromHfont(font2.GetHFont());
				if (font3.Unit != GraphicsUnit.Point)
				{
					font3 = new Font(font3.Name, font3.SizeInPoints, font3.Style, GraphicsUnit.Point, font3.GdiCharSet, font3.GdiVerticalFont);
				}
				font4 = font3;
			}
			catch (Exception ex)
			{
				font4 = Control.DefaultFont;
			}
			return font4;
		}

		/// <summary>Returns an OLE IFontDisp object created from the specified <see cref="T:System.Drawing.Font" /> object.</summary>
		/// <param name="font">The font to create an IFontDisp object from.</param>
		/// <returns>The IFontDisp object created from the specified font or <see langword="null" /> if <paramref name="font" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Font.Unit" /> property value is not <see cref="F:System.Drawing.GraphicsUnit.Point" />.</exception>
		// Token: 0x06000A9E RID: 2718 RVA: 0x0001DE18 File Offset: 0x0001C018
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static object GetIFontDispFromFont(Font font)
		{
			if (font == null)
			{
				return null;
			}
			if (font.Unit != GraphicsUnit.Point)
			{
				throw new ArgumentException(SR.GetString("AXFontUnitNotPoint"), "font");
			}
			return SafeNativeMethods.OleCreateIFontDispIndirect(AxHost.GetFONTDESCFromFont(font), ref AxHost.ifontDisp_Guid);
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Font" /> created from the specified OLE IFontDisp object.</summary>
		/// <param name="font">The IFontDisp to create a <see cref="T:System.Drawing.Font" /> from.</param>
		/// <returns>The <see cref="T:System.Drawing.Font" /> created from the specified IFontDisp, <see cref="P:System.Windows.Forms.Control.DefaultFont" /> if the font could not be created, or <see langword="null" /> if <paramref name="font" /> is <see langword="null" />.</returns>
		// Token: 0x06000A9F RID: 2719 RVA: 0x0001DE5C File Offset: 0x0001C05C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static Font GetFontFromIFontDisp(object font)
		{
			if (font == null)
			{
				return null;
			}
			UnsafeNativeMethods.IFont font2 = font as UnsafeNativeMethods.IFont;
			if (font2 != null)
			{
				return AxHost.GetFontFromIFont(font2);
			}
			SafeNativeMethods.IFontDisp fontDisp = (SafeNativeMethods.IFontDisp)font;
			FontStyle fontStyle = FontStyle.Regular;
			Font font4;
			try
			{
				if (fontDisp.Bold)
				{
					fontStyle |= FontStyle.Bold;
				}
				if (fontDisp.Italic)
				{
					fontStyle |= FontStyle.Italic;
				}
				if (fontDisp.Underline)
				{
					fontStyle |= FontStyle.Underline;
				}
				if (fontDisp.Strikethrough)
				{
					fontStyle |= FontStyle.Strikeout;
				}
				if (fontDisp.Weight >= 700)
				{
					fontStyle |= FontStyle.Bold;
				}
				Font font3 = new Font(fontDisp.Name, (float)fontDisp.Size / 10000f, fontStyle, GraphicsUnit.Point, (byte)fontDisp.Charset);
				font4 = font3;
			}
			catch (Exception ex)
			{
				font4 = Control.DefaultFont;
			}
			return font4;
		}

		/// <summary>Returns an OLE Automation date that corresponds to the specified <see cref="T:System.DateTime" /> structure.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> structure to translate.</param>
		/// <returns>A double-precision floating-point number that contains an OLE Automation date equivalent to specified <paramref name="time" /> value.</returns>
		/// <exception cref="T:System.OverflowException">The value of this instance cannot be represented as an OLE Automation Date.</exception>
		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001DF10 File Offset: 0x0001C110
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static double GetOADateFromTime(DateTime time)
		{
			return time.ToOADate();
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> structure that corresponds to the specified OLE Automation date.</summary>
		/// <param name="date">The OLE Automate date to translate.</param>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the same date and time as <paramref name="date" />.</returns>
		/// <exception cref="T:System.ArgumentException">The date is not a valid OLE Automation Date value.</exception>
		// Token: 0x06000AA1 RID: 2721 RVA: 0x0001DF19 File Offset: 0x0001C119
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static DateTime GetTimeFromOADate(double date)
		{
			return DateTime.FromOADate(date);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001DF24 File Offset: 0x0001C124
		private int Convert2int(object o, bool xDirection)
		{
			o = ((Array)o).GetValue(0);
			if (o.GetType() == typeof(float))
			{
				return AxHost.Twip2Pixel(Convert.ToDouble(o, CultureInfo.InvariantCulture), xDirection);
			}
			return Convert.ToInt32(o, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001DF73 File Offset: 0x0001C173
		private short Convert2short(object o)
		{
			o = ((Array)o).GetValue(0);
			return Convert.ToInt16(o, CultureInfo.InvariantCulture);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseMove" /> event using the specified objects.</summary>
		/// <param name="o1">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed.</param>
		/// <param name="o2">Not used.</param>
		/// <param name="o3">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="o4">The y-coordinate of a mouse click, in pixels.</param>
		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001DF8E File Offset: 0x0001C18E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseMove(object o1, object o2, object o3, object o4)
		{
			this.RaiseOnMouseMove(this.Convert2short(o1), this.Convert2short(o2), this.Convert2int(o3, true), this.Convert2int(o4, false));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseMove" /> event using the specified single-precision floating-point numbers.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed.</param>
		/// <param name="shift">Not used.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels.</param>
		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001DFB5 File Offset: 0x0001C1B5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseMove(short button, short shift, float x, float y)
		{
			this.RaiseOnMouseMove(button, shift, AxHost.Twip2Pixel((int)x, true), AxHost.Twip2Pixel((int)y, false));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseMove" /> event using the specified 32-bit signed integers.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed.</param>
		/// <param name="shift">Not used.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels.</param>
		// Token: 0x06000AA6 RID: 2726 RVA: 0x0001DFD0 File Offset: 0x0001C1D0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseMove(short button, short shift, int x, int y)
		{
			base.OnMouseMove(new MouseEventArgs((MouseButtons)(button << 20), 1, x, y, 0));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseUp" /> event using the specified objects.</summary>
		/// <param name="o1">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed.</param>
		/// <param name="o2">Not used.</param>
		/// <param name="o3">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="o4">The y-coordinate of a mouse click, in pixels.</param>
		// Token: 0x06000AA7 RID: 2727 RVA: 0x0001DFE6 File Offset: 0x0001C1E6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseUp(object o1, object o2, object o3, object o4)
		{
			this.RaiseOnMouseUp(this.Convert2short(o1), this.Convert2short(o2), this.Convert2int(o3, true), this.Convert2int(o4, false));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseUp" /> event using the specified single-precision floating-point numbers.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed.</param>
		/// <param name="shift">Not used.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels.</param>
		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001E00D File Offset: 0x0001C20D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseUp(short button, short shift, float x, float y)
		{
			this.RaiseOnMouseUp(button, shift, AxHost.Twip2Pixel((int)x, true), AxHost.Twip2Pixel((int)y, false));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseUp" /> event using the specified 32-bit signed integers.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed.</param>
		/// <param name="shift">Not used.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels.</param>
		// Token: 0x06000AA9 RID: 2729 RVA: 0x0001E028 File Offset: 0x0001C228
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseUp(short button, short shift, int x, int y)
		{
			base.OnMouseUp(new MouseEventArgs((MouseButtons)(button << 20), 1, x, y, 0));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseDown" /> event using the specified objects.</summary>
		/// <param name="o1">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed.</param>
		/// <param name="o2">Not used.</param>
		/// <param name="o3">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="o4">The y-coordinate of a mouse click, in pixels.</param>
		// Token: 0x06000AAA RID: 2730 RVA: 0x0001E03E File Offset: 0x0001C23E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseDown(object o1, object o2, object o3, object o4)
		{
			this.RaiseOnMouseDown(this.Convert2short(o1), this.Convert2short(o2), this.Convert2int(o3, true), this.Convert2int(o4, false));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseDown" /> event using the specified single-precision floating-point numbers.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed.</param>
		/// <param name="shift">Not used.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels.</param>
		// Token: 0x06000AAB RID: 2731 RVA: 0x0001E065 File Offset: 0x0001C265
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseDown(short button, short shift, float x, float y)
		{
			this.RaiseOnMouseDown(button, shift, AxHost.Twip2Pixel((int)x, true), AxHost.Twip2Pixel((int)y, false));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AxHost.MouseDown" /> event using the specified 32-bit signed integers.</summary>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that indicate which mouse button was pressed.</param>
		/// <param name="shift">Not used.</param>
		/// <param name="x">The x-coordinate of a mouse click, in pixels.</param>
		/// <param name="y">The y-coordinate of a mouse click, in pixels.</param>
		// Token: 0x06000AAC RID: 2732 RVA: 0x0001E080 File Offset: 0x0001C280
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseOnMouseDown(short button, short shift, int x, int y)
		{
			base.OnMouseDown(new MouseEventArgs((MouseButtons)(button << 20), 1, x, y, 0));
		}

		// Token: 0x0400061D RID: 1565
		private static TraceSwitch AxHTraceSwitch = new TraceSwitch("AxHTrace", "ActiveX handle tracing");

		// Token: 0x0400061E RID: 1566
		private static TraceSwitch AxPropTraceSwitch = new TraceSwitch("AxPropTrace", "ActiveX property tracing");

		// Token: 0x0400061F RID: 1567
		private static TraceSwitch AxHostSwitch = new TraceSwitch("AxHost", "ActiveX host creation");

		// Token: 0x04000620 RID: 1568
		private static BooleanSwitch AxIgnoreTMSwitch = new BooleanSwitch("AxIgnoreTM", "ActiveX switch to ignore thread models");

		// Token: 0x04000621 RID: 1569
		private static BooleanSwitch AxAlwaysSaveSwitch = new BooleanSwitch("AxAlwaysSave", "ActiveX to save all controls regardless of their IsDirty function return value");

		// Token: 0x04000622 RID: 1570
		private static COMException E_NOTIMPL = new COMException(SR.GetString("AXNotImplemented"), -2147483647);

		// Token: 0x04000623 RID: 1571
		private static COMException E_INVALIDARG = new COMException(SR.GetString("AXInvalidArgument"), -2147024809);

		// Token: 0x04000624 RID: 1572
		private static COMException E_FAIL = new COMException(SR.GetString("AXUnknownError"), -2147467259);

		// Token: 0x04000625 RID: 1573
		private static COMException E_NOINTERFACE = new COMException(SR.GetString("AxInterfaceNotSupported"), -2147467262);

		// Token: 0x04000626 RID: 1574
		private const int INPROC_SERVER = 1;

		// Token: 0x04000627 RID: 1575
		private const int OC_PASSIVE = 0;

		// Token: 0x04000628 RID: 1576
		private const int OC_LOADED = 1;

		// Token: 0x04000629 RID: 1577
		private const int OC_RUNNING = 2;

		// Token: 0x0400062A RID: 1578
		private const int OC_INPLACE = 4;

		// Token: 0x0400062B RID: 1579
		private const int OC_UIACTIVE = 8;

		// Token: 0x0400062C RID: 1580
		private const int OC_OPEN = 16;

		// Token: 0x0400062D RID: 1581
		private const int EDITM_NONE = 0;

		// Token: 0x0400062E RID: 1582
		private const int EDITM_OBJECT = 1;

		// Token: 0x0400062F RID: 1583
		private const int EDITM_HOST = 2;

		// Token: 0x04000630 RID: 1584
		private const int STG_UNKNOWN = -1;

		// Token: 0x04000631 RID: 1585
		private const int STG_STREAM = 0;

		// Token: 0x04000632 RID: 1586
		private const int STG_STREAMINIT = 1;

		// Token: 0x04000633 RID: 1587
		private const int STG_STORAGE = 2;

		// Token: 0x04000634 RID: 1588
		private const int OLEIVERB_SHOW = -1;

		// Token: 0x04000635 RID: 1589
		private const int OLEIVERB_HIDE = -3;

		// Token: 0x04000636 RID: 1590
		private const int OLEIVERB_UIACTIVATE = -4;

		// Token: 0x04000637 RID: 1591
		private const int OLEIVERB_INPLACEACTIVATE = -5;

		// Token: 0x04000638 RID: 1592
		private const int OLEIVERB_PROPERTIES = -7;

		// Token: 0x04000639 RID: 1593
		private const int OLEIVERB_PRIMARY = 0;

		// Token: 0x0400063A RID: 1594
		private readonly int REGMSG_MSG = SafeNativeMethods.RegisterWindowMessage(Application.WindowMessagesVersion + "_subclassCheck");

		// Token: 0x0400063B RID: 1595
		private const int REGMSG_RETVAL = 123;

		// Token: 0x0400063C RID: 1596
		private static int logPixelsX = -1;

		// Token: 0x0400063D RID: 1597
		private static int logPixelsY = -1;

		// Token: 0x0400063E RID: 1598
		private static Guid icf2_Guid = typeof(UnsafeNativeMethods.IClassFactory2).GUID;

		// Token: 0x0400063F RID: 1599
		private static Guid ifont_Guid = typeof(UnsafeNativeMethods.IFont).GUID;

		// Token: 0x04000640 RID: 1600
		private static Guid ifontDisp_Guid = typeof(SafeNativeMethods.IFontDisp).GUID;

		// Token: 0x04000641 RID: 1601
		private static Guid ipicture_Guid = typeof(UnsafeNativeMethods.IPicture).GUID;

		// Token: 0x04000642 RID: 1602
		private static Guid ipictureDisp_Guid = typeof(UnsafeNativeMethods.IPictureDisp).GUID;

		// Token: 0x04000643 RID: 1603
		private static Guid ivbformat_Guid = typeof(UnsafeNativeMethods.IVBFormat).GUID;

		// Token: 0x04000644 RID: 1604
		private static Guid ioleobject_Guid = typeof(UnsafeNativeMethods.IOleObject).GUID;

		// Token: 0x04000645 RID: 1605
		private static Guid dataSource_Guid = new Guid("{7C0FFAB3-CD84-11D0-949A-00A0C91110ED}");

		// Token: 0x04000646 RID: 1606
		private static Guid windowsMediaPlayer_Clsid = new Guid("{22d6f312-b0f6-11d0-94ab-0080c74c7e95}");

		// Token: 0x04000647 RID: 1607
		private static Guid comctlImageCombo_Clsid = new Guid("{a98a24c0-b06f-3684-8c12-c52ae341e0bc}");

		// Token: 0x04000648 RID: 1608
		private static Guid maskEdit_Clsid = new Guid("{c932ba85-4374-101b-a56c-00aa003668dc}");

		// Token: 0x04000649 RID: 1609
		private static Hashtable fontTable;

		// Token: 0x0400064A RID: 1610
		private static readonly int ocxStateSet = BitVector32.CreateMask();

		// Token: 0x0400064B RID: 1611
		private static readonly int editorRefresh = BitVector32.CreateMask(AxHost.ocxStateSet);

		// Token: 0x0400064C RID: 1612
		private static readonly int listeningToIdle = BitVector32.CreateMask(AxHost.editorRefresh);

		// Token: 0x0400064D RID: 1613
		private static readonly int refreshProperties = BitVector32.CreateMask(AxHost.listeningToIdle);

		// Token: 0x0400064E RID: 1614
		private static readonly int checkedIppb = BitVector32.CreateMask(AxHost.refreshProperties);

		// Token: 0x0400064F RID: 1615
		private static readonly int checkedCP = BitVector32.CreateMask(AxHost.checkedIppb);

		// Token: 0x04000650 RID: 1616
		private static readonly int fNeedOwnWindow = BitVector32.CreateMask(AxHost.checkedCP);

		// Token: 0x04000651 RID: 1617
		private static readonly int fOwnWindow = BitVector32.CreateMask(AxHost.fNeedOwnWindow);

		// Token: 0x04000652 RID: 1618
		private static readonly int fSimpleFrame = BitVector32.CreateMask(AxHost.fOwnWindow);

		// Token: 0x04000653 RID: 1619
		private static readonly int fFakingWindow = BitVector32.CreateMask(AxHost.fSimpleFrame);

		// Token: 0x04000654 RID: 1620
		private static readonly int rejectSelection = BitVector32.CreateMask(AxHost.fFakingWindow);

		// Token: 0x04000655 RID: 1621
		private static readonly int ownDisposing = BitVector32.CreateMask(AxHost.rejectSelection);

		// Token: 0x04000656 RID: 1622
		private static readonly int sinkAttached = BitVector32.CreateMask(AxHost.ownDisposing);

		// Token: 0x04000657 RID: 1623
		private static readonly int disposed = BitVector32.CreateMask(AxHost.sinkAttached);

		// Token: 0x04000658 RID: 1624
		private static readonly int manualUpdate = BitVector32.CreateMask(AxHost.disposed);

		// Token: 0x04000659 RID: 1625
		private static readonly int addedSelectionHandler = BitVector32.CreateMask(AxHost.manualUpdate);

		// Token: 0x0400065A RID: 1626
		private static readonly int valueChanged = BitVector32.CreateMask(AxHost.addedSelectionHandler);

		// Token: 0x0400065B RID: 1627
		private static readonly int handlePosRectChanged = BitVector32.CreateMask(AxHost.valueChanged);

		// Token: 0x0400065C RID: 1628
		private static readonly int siteProcessedInputKey = BitVector32.CreateMask(AxHost.handlePosRectChanged);

		// Token: 0x0400065D RID: 1629
		private static readonly int needLicenseKey = BitVector32.CreateMask(AxHost.siteProcessedInputKey);

		// Token: 0x0400065E RID: 1630
		private static readonly int inTransition = BitVector32.CreateMask(AxHost.needLicenseKey);

		// Token: 0x0400065F RID: 1631
		private static readonly int processingKeyUp = BitVector32.CreateMask(AxHost.inTransition);

		// Token: 0x04000660 RID: 1632
		private static readonly int assignUniqueID = BitVector32.CreateMask(AxHost.processingKeyUp);

		// Token: 0x04000661 RID: 1633
		private static readonly int renameEventHooked = BitVector32.CreateMask(AxHost.assignUniqueID);

		// Token: 0x04000662 RID: 1634
		private BitVector32 axState;

		// Token: 0x04000663 RID: 1635
		private int storageType = -1;

		// Token: 0x04000664 RID: 1636
		private int ocState;

		// Token: 0x04000665 RID: 1637
		private int miscStatusBits;

		// Token: 0x04000666 RID: 1638
		private int freezeCount;

		// Token: 0x04000667 RID: 1639
		private int flags;

		// Token: 0x04000668 RID: 1640
		private int selectionStyle;

		// Token: 0x04000669 RID: 1641
		private int editMode;

		// Token: 0x0400066A RID: 1642
		private int noComponentChange;

		// Token: 0x0400066B RID: 1643
		private IntPtr wndprocAddr = IntPtr.Zero;

		// Token: 0x0400066C RID: 1644
		private Guid clsid;

		// Token: 0x0400066D RID: 1645
		private string text = "";

		// Token: 0x0400066E RID: 1646
		private string licenseKey;

		// Token: 0x0400066F RID: 1647
		private readonly AxHost.OleInterfaces oleSite;

		// Token: 0x04000670 RID: 1648
		private AxHost.AxComponentEditor editor;

		// Token: 0x04000671 RID: 1649
		private AxHost.AxContainer container;

		// Token: 0x04000672 RID: 1650
		private ContainerControl containingControl;

		// Token: 0x04000673 RID: 1651
		private ContainerControl newParent;

		// Token: 0x04000674 RID: 1652
		private AxHost.AxContainer axContainer;

		// Token: 0x04000675 RID: 1653
		private AxHost.State ocxState;

		// Token: 0x04000676 RID: 1654
		private IntPtr hwndFocus = IntPtr.Zero;

		// Token: 0x04000677 RID: 1655
		private Hashtable properties;

		// Token: 0x04000678 RID: 1656
		private Hashtable propertyInfos;

		// Token: 0x04000679 RID: 1657
		private PropertyDescriptorCollection propsStash;

		// Token: 0x0400067A RID: 1658
		private Attribute[] attribsStash;

		// Token: 0x0400067B RID: 1659
		private object instance;

		// Token: 0x0400067C RID: 1660
		private UnsafeNativeMethods.IOleInPlaceObject iOleInPlaceObject;

		// Token: 0x0400067D RID: 1661
		private UnsafeNativeMethods.IOleObject iOleObject;

		// Token: 0x0400067E RID: 1662
		private UnsafeNativeMethods.IOleControl iOleControl;

		// Token: 0x0400067F RID: 1663
		private UnsafeNativeMethods.IOleInPlaceActiveObject iOleInPlaceActiveObject;

		// Token: 0x04000680 RID: 1664
		private UnsafeNativeMethods.IOleInPlaceActiveObject iOleInPlaceActiveObjectExternal;

		// Token: 0x04000681 RID: 1665
		private NativeMethods.IPerPropertyBrowsing iPerPropertyBrowsing;

		// Token: 0x04000682 RID: 1666
		private NativeMethods.ICategorizeProperties iCategorizeProperties;

		// Token: 0x04000683 RID: 1667
		private UnsafeNativeMethods.IPersistPropertyBag iPersistPropBag;

		// Token: 0x04000684 RID: 1668
		private UnsafeNativeMethods.IPersistStream iPersistStream;

		// Token: 0x04000685 RID: 1669
		private UnsafeNativeMethods.IPersistStreamInit iPersistStreamInit;

		// Token: 0x04000686 RID: 1670
		private UnsafeNativeMethods.IPersistStorage iPersistStorage;

		// Token: 0x04000687 RID: 1671
		private AxHost.AboutBoxDelegate aboutBoxDelegate;

		// Token: 0x04000688 RID: 1672
		private EventHandler selectionChangeHandler;

		// Token: 0x04000689 RID: 1673
		private bool isMaskEdit;

		// Token: 0x0400068A RID: 1674
		private bool ignoreDialogKeys;

		// Token: 0x0400068B RID: 1675
		private EventHandler onContainerVisibleChanged;

		// Token: 0x0400068C RID: 1676
		private static CategoryAttribute[] categoryNames = new CategoryAttribute[]
		{
			null,
			new WinCategoryAttribute("Default"),
			new WinCategoryAttribute("Default"),
			new WinCategoryAttribute("Font"),
			new WinCategoryAttribute("Layout"),
			new WinCategoryAttribute("Appearance"),
			new WinCategoryAttribute("Behavior"),
			new WinCategoryAttribute("Data"),
			new WinCategoryAttribute("List"),
			new WinCategoryAttribute("Text"),
			new WinCategoryAttribute("Scale"),
			new WinCategoryAttribute("DDE")
		};

		// Token: 0x0400068D RID: 1677
		private Hashtable objectDefinedCategoryNames;

		// Token: 0x0400068E RID: 1678
		private const int HMperInch = 2540;

		// Token: 0x02000606 RID: 1542
		internal class AxFlags
		{
			// Token: 0x040038D0 RID: 14544
			internal const int PreventEditMode = 1;

			// Token: 0x040038D1 RID: 14545
			internal const int IncludePropertiesVerb = 2;

			// Token: 0x040038D2 RID: 14546
			internal const int IgnoreThreadModel = 268435456;
		}

		/// <summary>Specifies the CLSID of an ActiveX control hosted by an <see cref="T:System.Windows.Forms.AxHost" /> control.</summary>
		// Token: 0x02000607 RID: 1543
		[AttributeUsage(AttributeTargets.Class, Inherited = false)]
		public sealed class ClsidAttribute : Attribute
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.ClsidAttribute" /> class.</summary>
			/// <param name="clsid">The CLSID of the ActiveX control.</param>
			// Token: 0x0600622C RID: 25132 RVA: 0x0016B44C File Offset: 0x0016964C
			public ClsidAttribute(string clsid)
			{
				this.val = clsid;
			}

			/// <summary>The CLSID of the ActiveX control.</summary>
			/// <returns>The CLSID of the ActiveX control.</returns>
			// Token: 0x17001508 RID: 5384
			// (get) Token: 0x0600622D RID: 25133 RVA: 0x0016B45B File Offset: 0x0016965B
			public string Value
			{
				get
				{
					return this.val;
				}
			}

			// Token: 0x040038D3 RID: 14547
			private string val;
		}

		/// <summary>Specifies a date and time associated with the type library of an ActiveX control hosted by an <see cref="T:System.Windows.Forms.AxHost" /> control.</summary>
		// Token: 0x02000608 RID: 1544
		[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
		public sealed class TypeLibraryTimeStampAttribute : Attribute
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.TypeLibraryTimeStampAttribute" /> class.</summary>
			/// <param name="timestamp">A <see cref="T:System.DateTime" /> value representing the date and time to associate with the type library.</param>
			// Token: 0x0600622E RID: 25134 RVA: 0x0016B463 File Offset: 0x00169663
			public TypeLibraryTimeStampAttribute(string timestamp)
			{
				this.val = DateTime.Parse(timestamp, CultureInfo.InvariantCulture);
			}

			/// <summary>The date and time to associate with the type library.</summary>
			/// <returns>A <see cref="T:System.DateTime" /> value representing the date and time to associate with the type library.</returns>
			// Token: 0x17001509 RID: 5385
			// (get) Token: 0x0600622F RID: 25135 RVA: 0x0016B47C File Offset: 0x0016967C
			public DateTime Value
			{
				get
				{
					return this.val;
				}
			}

			// Token: 0x040038D4 RID: 14548
			private DateTime val;
		}

		/// <summary>Connects an ActiveX control to a client that handles the control's events.</summary>
		// Token: 0x02000609 RID: 1545
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public class ConnectionPointCookie
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.ConnectionPointCookie" /> class.</summary>
			/// <param name="source">A connectable object that contains connection points.</param>
			/// <param name="sink">The client's sink which receives outgoing calls from the connection point.</param>
			/// <param name="eventInterface">The outgoing interface whose connection point object is being requested.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="source" /> does not implement <paramref name="eventInterface" />.</exception>
			/// <exception cref="T:System.InvalidCastException">
			///   <paramref name="sink" /> does not implement <paramref name="eventInterface" />.  
			/// -or-  
			/// <paramref name="source" /> does not implement <see cref="T:System.Runtime.InteropServices.ComTypes.IConnectionPointContainer" />.</exception>
			/// <exception cref="T:System.InvalidOperationException">The connection point has already reached its limit of connections and cannot accept any more.</exception>
			// Token: 0x06006230 RID: 25136 RVA: 0x0016B484 File Offset: 0x00169684
			public ConnectionPointCookie(object source, object sink, Type eventInterface)
				: this(source, sink, eventInterface, true)
			{
			}

			// Token: 0x06006231 RID: 25137 RVA: 0x0016B490 File Offset: 0x00169690
			internal ConnectionPointCookie(object source, object sink, Type eventInterface, bool throwException)
			{
				if (source is UnsafeNativeMethods.IConnectionPointContainer)
				{
					UnsafeNativeMethods.IConnectionPointContainer connectionPointContainer = (UnsafeNativeMethods.IConnectionPointContainer)source;
					try
					{
						Guid guid = eventInterface.GUID;
						if (connectionPointContainer.FindConnectionPoint(ref guid, out this.connectionPoint) != 0)
						{
							this.connectionPoint = null;
						}
					}
					catch
					{
						this.connectionPoint = null;
					}
					if (this.connectionPoint == null)
					{
						if (throwException)
						{
							throw new ArgumentException(SR.GetString("AXNoEventInterface", new object[] { eventInterface.Name }));
						}
					}
					else if (sink == null || !eventInterface.IsInstanceOfType(sink))
					{
						if (throwException)
						{
							throw new InvalidCastException(SR.GetString("AXNoSinkImplementation", new object[] { eventInterface.Name }));
						}
					}
					else
					{
						int num = this.connectionPoint.Advise(sink, ref this.cookie);
						if (num == 0)
						{
							this.threadId = Thread.CurrentThread.ManagedThreadId;
						}
						else
						{
							this.cookie = 0;
							Marshal.ReleaseComObject(this.connectionPoint);
							this.connectionPoint = null;
							if (throwException)
							{
								throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, SR.GetString("AXNoSinkAdvise", new object[] { eventInterface.Name }), new object[] { num }));
							}
						}
					}
				}
				else if (throwException)
				{
					throw new InvalidCastException(SR.GetString("AXNoConnectionPointContainer"));
				}
				if (this.connectionPoint == null || this.cookie == 0)
				{
					if (this.connectionPoint != null)
					{
						Marshal.ReleaseComObject(this.connectionPoint);
					}
					if (throwException)
					{
						throw new ArgumentException(SR.GetString("AXNoConnectionPoint", new object[] { eventInterface.Name }));
					}
				}
			}

			/// <summary>Disconnects the ActiveX control from the client.</summary>
			// Token: 0x06006232 RID: 25138 RVA: 0x0016B628 File Offset: 0x00169828
			public void Disconnect()
			{
				if (this.connectionPoint != null && this.cookie != 0)
				{
					try
					{
						this.connectionPoint.Unadvise(this.cookie);
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsCriticalException(ex))
						{
							throw;
						}
					}
					finally
					{
						this.cookie = 0;
					}
					try
					{
						Marshal.ReleaseComObject(this.connectionPoint);
					}
					catch (Exception ex2)
					{
						if (ClientUtils.IsCriticalException(ex2))
						{
							throw;
						}
					}
					finally
					{
						this.connectionPoint = null;
					}
				}
			}

			/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.AxHost.ConnectionPointCookie" /> class.</summary>
			// Token: 0x06006233 RID: 25139 RVA: 0x0016B6C8 File Offset: 0x001698C8
			protected override void Finalize()
			{
				try
				{
					if (this.connectionPoint != null && this.cookie != 0 && !AppDomain.CurrentDomain.IsFinalizingForUnload())
					{
						SynchronizationContext synchronizationContext = SynchronizationContext.Current;
						if (synchronizationContext != null)
						{
							synchronizationContext.Post(new SendOrPostCallback(this.AttemptDisconnect), null);
						}
					}
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x06006234 RID: 25140 RVA: 0x0016B728 File Offset: 0x00169928
			private void AttemptDisconnect(object trash)
			{
				if (this.threadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.Disconnect();
				}
			}

			// Token: 0x1700150A RID: 5386
			// (get) Token: 0x06006235 RID: 25141 RVA: 0x0016B742 File Offset: 0x00169942
			internal bool Connected
			{
				get
				{
					return this.connectionPoint != null && this.cookie != 0;
				}
			}

			// Token: 0x040038D5 RID: 14549
			private UnsafeNativeMethods.IConnectionPoint connectionPoint;

			// Token: 0x040038D6 RID: 14550
			private int cookie;

			// Token: 0x040038D7 RID: 14551
			internal int threadId;
		}

		/// <summary>Specifies the type of member that referenced the ActiveX control while it was in an invalid state.</summary>
		// Token: 0x0200060A RID: 1546
		public enum ActiveXInvokeKind
		{
			/// <summary>A method referenced the ActiveX control.</summary>
			// Token: 0x040038D9 RID: 14553
			MethodInvoke,
			/// <summary>The get accessor of a property referenced the ActiveX control.</summary>
			// Token: 0x040038DA RID: 14554
			PropertyGet,
			/// <summary>The set accessor of a property referenced the ActiveX control.</summary>
			// Token: 0x040038DB RID: 14555
			PropertySet
		}

		/// <summary>The exception that is thrown when the ActiveX control is referenced while in an invalid state.</summary>
		// Token: 0x0200060B RID: 1547
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public class InvalidActiveXStateException : Exception
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.InvalidActiveXStateException" /> class and indicates the name of the member that referenced the ActiveX control and the kind of reference it made.</summary>
			/// <param name="name">The name of the member that referenced the ActiveX control while it was in an invalid state.</param>
			/// <param name="kind">One of the <see cref="T:System.Windows.Forms.AxHost.ActiveXInvokeKind" /> values.</param>
			// Token: 0x06006236 RID: 25142 RVA: 0x0016B757 File Offset: 0x00169957
			public InvalidActiveXStateException(string name, AxHost.ActiveXInvokeKind kind)
			{
				this.name = name;
				this.kind = kind;
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.InvalidActiveXStateException" /> class without specifying information about the member that referenced the ActiveX control.</summary>
			// Token: 0x06006237 RID: 25143 RVA: 0x0016B76D File Offset: 0x0016996D
			public InvalidActiveXStateException()
			{
			}

			/// <summary>Creates and returns a string representation of the current exception.</summary>
			/// <returns>A string representation of the current exception.</returns>
			// Token: 0x06006238 RID: 25144 RVA: 0x0016B778 File Offset: 0x00169978
			public override string ToString()
			{
				switch (this.kind)
				{
				case AxHost.ActiveXInvokeKind.MethodInvoke:
					return SR.GetString("AXInvalidMethodInvoke", new object[] { this.name });
				case AxHost.ActiveXInvokeKind.PropertyGet:
					return SR.GetString("AXInvalidPropertyGet", new object[] { this.name });
				case AxHost.ActiveXInvokeKind.PropertySet:
					return SR.GetString("AXInvalidPropertySet", new object[] { this.name });
				default:
					return base.ToString();
				}
			}

			// Token: 0x040038DC RID: 14556
			private string name;

			// Token: 0x040038DD RID: 14557
			private AxHost.ActiveXInvokeKind kind;
		}

		// Token: 0x0200060C RID: 1548
		private class OleInterfaces : UnsafeNativeMethods.IOleControlSite, UnsafeNativeMethods.IOleClientSite, UnsafeNativeMethods.IOleInPlaceSite, UnsafeNativeMethods.ISimpleFrameSite, UnsafeNativeMethods.IVBGetControl, UnsafeNativeMethods.IGetVBAObject, UnsafeNativeMethods.IPropertyNotifySink, IReflect, IDisposable
		{
			// Token: 0x06006239 RID: 25145 RVA: 0x0016B7F4 File Offset: 0x001699F4
			internal OleInterfaces(AxHost host)
			{
				if (host == null)
				{
					throw new ArgumentNullException("host");
				}
				this.host = host;
			}

			// Token: 0x0600623A RID: 25146 RVA: 0x0016B814 File Offset: 0x00169A14
			private void Dispose(bool disposing)
			{
				if (disposing && !AppDomain.CurrentDomain.IsFinalizingForUnload())
				{
					SynchronizationContext synchronizationContext = SynchronizationContext.Current;
					if (synchronizationContext != null)
					{
						synchronizationContext.Post(new SendOrPostCallback(this.AttemptStopEvents), null);
					}
				}
			}

			// Token: 0x0600623B RID: 25147 RVA: 0x0016B84C File Offset: 0x00169A4C
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x0600623C RID: 25148 RVA: 0x0016B85B File Offset: 0x00169A5B
			internal AxHost GetAxHost()
			{
				return this.host;
			}

			// Token: 0x0600623D RID: 25149 RVA: 0x0016B863 File Offset: 0x00169A63
			internal void OnOcxCreate()
			{
				this.StartEvents();
			}

			// Token: 0x0600623E RID: 25150 RVA: 0x0016B86C File Offset: 0x00169A6C
			internal void StartEvents()
			{
				if (this.connectionPoint != null)
				{
					return;
				}
				object ocx = this.host.GetOcx();
				try
				{
					this.connectionPoint = new AxHost.ConnectionPointCookie(ocx, this, typeof(UnsafeNativeMethods.IPropertyNotifySink));
				}
				catch
				{
				}
			}

			// Token: 0x0600623F RID: 25151 RVA: 0x0016B8BC File Offset: 0x00169ABC
			private void AttemptStopEvents(object trash)
			{
				if (this.connectionPoint == null)
				{
					return;
				}
				if (this.connectionPoint.threadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.StopEvents();
				}
			}

			// Token: 0x06006240 RID: 25152 RVA: 0x0016B8E4 File Offset: 0x00169AE4
			internal void StopEvents()
			{
				if (this.connectionPoint != null)
				{
					this.connectionPoint.Disconnect();
					this.connectionPoint = null;
				}
			}

			// Token: 0x06006241 RID: 25153 RVA: 0x0016B900 File Offset: 0x00169B00
			int UnsafeNativeMethods.IGetVBAObject.GetObject(ref Guid riid, UnsafeNativeMethods.IVBFormat[] rval, int dwReserved)
			{
				if (rval == null || riid.Equals(Guid.Empty))
				{
					return -2147024809;
				}
				if (riid.Equals(AxHost.ivbformat_Guid))
				{
					rval[0] = new AxHost.VBFormat();
					return 0;
				}
				rval[0] = null;
				return -2147467262;
			}

			// Token: 0x06006242 RID: 25154 RVA: 0x0016B938 File Offset: 0x00169B38
			int UnsafeNativeMethods.IVBGetControl.EnumControls(int dwOleContF, int dwWhich, out UnsafeNativeMethods.IEnumUnknown ppenum)
			{
				ppenum = null;
				ppenum = this.host.GetParentContainer().EnumControls(this.host, dwOleContF, dwWhich);
				return 0;
			}

			// Token: 0x06006243 RID: 25155 RVA: 0x0001180C File Offset: 0x0000FA0C
			int UnsafeNativeMethods.ISimpleFrameSite.PreMessageFilter(IntPtr hwnd, int msg, IntPtr wp, IntPtr lp, ref IntPtr plResult, ref int pdwCookie)
			{
				return 0;
			}

			// Token: 0x06006244 RID: 25156 RVA: 0x00012E4E File Offset: 0x0001104E
			int UnsafeNativeMethods.ISimpleFrameSite.PostMessageFilter(IntPtr hwnd, int msg, IntPtr wp, IntPtr lp, ref IntPtr plResult, int dwCookie)
			{
				return 1;
			}

			// Token: 0x06006245 RID: 25157 RVA: 0x00015C90 File Offset: 0x00013E90
			MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
			{
				return null;
			}

			// Token: 0x06006246 RID: 25158 RVA: 0x00015C90 File Offset: 0x00013E90
			MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
			{
				return null;
			}

			// Token: 0x06006247 RID: 25159 RVA: 0x0016B958 File Offset: 0x00169B58
			MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
			{
				return new MethodInfo[0];
			}

			// Token: 0x06006248 RID: 25160 RVA: 0x00015C90 File Offset: 0x00013E90
			FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
			{
				return null;
			}

			// Token: 0x06006249 RID: 25161 RVA: 0x0016B960 File Offset: 0x00169B60
			FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
			{
				return new FieldInfo[0];
			}

			// Token: 0x0600624A RID: 25162 RVA: 0x00015C90 File Offset: 0x00013E90
			PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
			{
				return null;
			}

			// Token: 0x0600624B RID: 25163 RVA: 0x00015C90 File Offset: 0x00013E90
			PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
			{
				return null;
			}

			// Token: 0x0600624C RID: 25164 RVA: 0x0016B968 File Offset: 0x00169B68
			PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
			{
				return new PropertyInfo[0];
			}

			// Token: 0x0600624D RID: 25165 RVA: 0x0016B970 File Offset: 0x00169B70
			MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
			{
				return new MemberInfo[0];
			}

			// Token: 0x0600624E RID: 25166 RVA: 0x0016B970 File Offset: 0x00169B70
			MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
			{
				return new MemberInfo[0];
			}

			// Token: 0x0600624F RID: 25167 RVA: 0x0016B978 File Offset: 0x00169B78
			object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
			{
				if (name.StartsWith("[DISPID="))
				{
					int num = name.IndexOf("]");
					int num2 = int.Parse(name.Substring(8, num - 8), CultureInfo.InvariantCulture);
					object ambientProperty = this.host.GetAmbientProperty(num2);
					if (ambientProperty != null)
					{
						return ambientProperty;
					}
				}
				throw AxHost.E_FAIL;
			}

			// Token: 0x1700150B RID: 5387
			// (get) Token: 0x06006250 RID: 25168 RVA: 0x00015C90 File Offset: 0x00013E90
			Type IReflect.UnderlyingSystemType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006251 RID: 25169 RVA: 0x0001180C File Offset: 0x0000FA0C
			int UnsafeNativeMethods.IOleControlSite.OnControlInfoChanged()
			{
				return 0;
			}

			// Token: 0x06006252 RID: 25170 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IOleControlSite.LockInPlaceActive(int fLock)
			{
				return -2147467263;
			}

			// Token: 0x06006253 RID: 25171 RVA: 0x0016B9CA File Offset: 0x00169BCA
			int UnsafeNativeMethods.IOleControlSite.GetExtendedControl(out object ppDisp)
			{
				ppDisp = this.host.GetParentContainer().GetProxyForControl(this.host);
				if (ppDisp == null)
				{
					return -2147467263;
				}
				return 0;
			}

			// Token: 0x06006254 RID: 25172 RVA: 0x0016B9F0 File Offset: 0x00169BF0
			int UnsafeNativeMethods.IOleControlSite.TransformCoords(NativeMethods._POINTL pPtlHimetric, NativeMethods.tagPOINTF pPtfContainer, int dwFlags)
			{
				int num = AxHost.SetupLogPixels(false);
				if (NativeMethods.Failed(num))
				{
					return num;
				}
				if ((dwFlags & 4) != 0)
				{
					if ((dwFlags & 2) != 0)
					{
						pPtfContainer.x = (float)this.host.HM2Pix(pPtlHimetric.x, AxHost.logPixelsX);
						pPtfContainer.y = (float)this.host.HM2Pix(pPtlHimetric.y, AxHost.logPixelsY);
					}
					else
					{
						if ((dwFlags & 1) == 0)
						{
							return -2147024809;
						}
						pPtfContainer.x = (float)this.host.HM2Pix(pPtlHimetric.x, AxHost.logPixelsX);
						pPtfContainer.y = (float)this.host.HM2Pix(pPtlHimetric.y, AxHost.logPixelsY);
					}
				}
				else
				{
					if ((dwFlags & 8) == 0)
					{
						return -2147024809;
					}
					if ((dwFlags & 2) != 0)
					{
						pPtlHimetric.x = this.host.Pix2HM((int)pPtfContainer.x, AxHost.logPixelsX);
						pPtlHimetric.y = this.host.Pix2HM((int)pPtfContainer.y, AxHost.logPixelsY);
					}
					else
					{
						if ((dwFlags & 1) == 0)
						{
							return -2147024809;
						}
						pPtlHimetric.x = this.host.Pix2HM((int)pPtfContainer.x, AxHost.logPixelsX);
						pPtlHimetric.y = this.host.Pix2HM((int)pPtfContainer.y, AxHost.logPixelsY);
					}
				}
				return 0;
			}

			// Token: 0x06006255 RID: 25173 RVA: 0x0016BB3C File Offset: 0x00169D3C
			int UnsafeNativeMethods.IOleControlSite.TranslateAccelerator(ref NativeMethods.MSG pMsg, int grfModifiers)
			{
				this.host.SetAxState(AxHost.siteProcessedInputKey, true);
				Message message = default(Message);
				message.Msg = pMsg.message;
				message.WParam = pMsg.wParam;
				message.LParam = pMsg.lParam;
				message.HWnd = pMsg.hwnd;
				int num;
				try
				{
					num = (this.host.PreProcessMessage(ref message) ? 0 : 1);
				}
				finally
				{
					this.host.SetAxState(AxHost.siteProcessedInputKey, false);
				}
				return num;
			}

			// Token: 0x06006256 RID: 25174 RVA: 0x0001180C File Offset: 0x0000FA0C
			int UnsafeNativeMethods.IOleControlSite.OnFocus(int fGotFocus)
			{
				return 0;
			}

			// Token: 0x06006257 RID: 25175 RVA: 0x0016BBD4 File Offset: 0x00169DD4
			int UnsafeNativeMethods.IOleControlSite.ShowPropertyFrame()
			{
				if (this.host.CanShowPropertyPages())
				{
					this.host.ShowPropertyPages();
					return 0;
				}
				return -2147467263;
			}

			// Token: 0x06006258 RID: 25176 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IOleClientSite.SaveObject()
			{
				return -2147467263;
			}

			// Token: 0x06006259 RID: 25177 RVA: 0x0003BC6F File Offset: 0x00039E6F
			int UnsafeNativeMethods.IOleClientSite.GetMoniker(int dwAssign, int dwWhichMoniker, out object moniker)
			{
				moniker = null;
				return -2147467263;
			}

			// Token: 0x0600625A RID: 25178 RVA: 0x0016BBF5 File Offset: 0x00169DF5
			int UnsafeNativeMethods.IOleClientSite.GetContainer(out UnsafeNativeMethods.IOleContainer container)
			{
				container = this.host.GetParentContainer();
				return 0;
			}

			// Token: 0x0600625B RID: 25179 RVA: 0x0016BC08 File Offset: 0x00169E08
			int UnsafeNativeMethods.IOleClientSite.ShowObject()
			{
				if (this.host.GetAxState(AxHost.fOwnWindow))
				{
					return 0;
				}
				if (this.host.GetAxState(AxHost.fFakingWindow))
				{
					this.host.DestroyFakeWindow();
					this.host.TransitionDownTo(1);
					this.host.TransitionUpTo(4);
				}
				if (this.host.GetOcState() < 4)
				{
					return 0;
				}
				IntPtr intPtr;
				if (NativeMethods.Succeeded(this.host.GetInPlaceObject().GetWindow(out intPtr)))
				{
					if (this.host.GetHandleNoCreate() != intPtr)
					{
						this.host.DetachWindow();
						if (intPtr != IntPtr.Zero)
						{
							this.host.AttachWindow(intPtr);
						}
					}
				}
				else if (this.host.GetInPlaceObject() is UnsafeNativeMethods.IOleInPlaceObjectWindowless)
				{
					throw new InvalidOperationException(SR.GetString("AXWindowlessControl"));
				}
				return 0;
			}

			// Token: 0x0600625C RID: 25180 RVA: 0x0001180C File Offset: 0x0000FA0C
			int UnsafeNativeMethods.IOleClientSite.OnShowWindow(int fShow)
			{
				return 0;
			}

			// Token: 0x0600625D RID: 25181 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IOleClientSite.RequestNewObjectLayout()
			{
				return -2147467263;
			}

			// Token: 0x0600625E RID: 25182 RVA: 0x0016BCE4 File Offset: 0x00169EE4
			IntPtr UnsafeNativeMethods.IOleInPlaceSite.GetWindow()
			{
				IntPtr intPtr;
				try
				{
					Control parentInternal = this.host.ParentInternal;
					intPtr = ((parentInternal != null) ? parentInternal.Handle : IntPtr.Zero);
				}
				catch (Exception ex)
				{
					throw ex;
				}
				return intPtr;
			}

			// Token: 0x0600625F RID: 25183 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IOleInPlaceSite.ContextSensitiveHelp(int fEnterMode)
			{
				return -2147467263;
			}

			// Token: 0x06006260 RID: 25184 RVA: 0x0001180C File Offset: 0x0000FA0C
			int UnsafeNativeMethods.IOleInPlaceSite.CanInPlaceActivate()
			{
				return 0;
			}

			// Token: 0x06006261 RID: 25185 RVA: 0x0016BD24 File Offset: 0x00169F24
			int UnsafeNativeMethods.IOleInPlaceSite.OnInPlaceActivate()
			{
				this.host.SetAxState(AxHost.ownDisposing, false);
				this.host.SetAxState(AxHost.rejectSelection, false);
				this.host.SetOcState(4);
				return 0;
			}

			// Token: 0x06006262 RID: 25186 RVA: 0x0016BD55 File Offset: 0x00169F55
			int UnsafeNativeMethods.IOleInPlaceSite.OnUIActivate()
			{
				this.host.SetOcState(8);
				this.host.GetParentContainer().OnUIActivate(this.host);
				return 0;
			}

			// Token: 0x06006263 RID: 25187 RVA: 0x0016BD7C File Offset: 0x00169F7C
			int UnsafeNativeMethods.IOleInPlaceSite.GetWindowContext(out UnsafeNativeMethods.IOleInPlaceFrame ppFrame, out UnsafeNativeMethods.IOleInPlaceUIWindow ppDoc, NativeMethods.COMRECT lprcPosRect, NativeMethods.COMRECT lprcClipRect, NativeMethods.tagOIFI lpFrameInfo)
			{
				ppDoc = null;
				ppFrame = this.host.GetParentContainer();
				AxHost.FillInRect(lprcPosRect, this.host.Bounds);
				this.host.GetClipRect(lprcClipRect);
				if (lpFrameInfo != null)
				{
					lpFrameInfo.cb = Marshal.SizeOf(typeof(NativeMethods.tagOIFI));
					lpFrameInfo.fMDIApp = false;
					lpFrameInfo.hAccel = IntPtr.Zero;
					lpFrameInfo.cAccelEntries = 0;
					lpFrameInfo.hwndFrame = this.host.ParentInternal.Handle;
				}
				return 0;
			}

			// Token: 0x06006264 RID: 25188 RVA: 0x0016BE08 File Offset: 0x0016A008
			int UnsafeNativeMethods.IOleInPlaceSite.Scroll(NativeMethods.tagSIZE scrollExtant)
			{
				try
				{
				}
				catch (Exception ex)
				{
					throw ex;
				}
				return 1;
			}

			// Token: 0x06006265 RID: 25189 RVA: 0x0016BE2C File Offset: 0x0016A02C
			int UnsafeNativeMethods.IOleInPlaceSite.OnUIDeactivate(int fUndoable)
			{
				this.host.GetParentContainer().OnUIDeactivate(this.host);
				if (this.host.GetOcState() > 4)
				{
					this.host.SetOcState(4);
				}
				return 0;
			}

			// Token: 0x06006266 RID: 25190 RVA: 0x0016BE60 File Offset: 0x0016A060
			int UnsafeNativeMethods.IOleInPlaceSite.OnInPlaceDeactivate()
			{
				if (this.host.GetOcState() == 8)
				{
					((UnsafeNativeMethods.IOleInPlaceSite)this).OnUIDeactivate(0);
				}
				this.host.GetParentContainer().OnInPlaceDeactivate(this.host);
				this.host.DetachWindow();
				this.host.SetOcState(2);
				return 0;
			}

			// Token: 0x06006267 RID: 25191 RVA: 0x0001180C File Offset: 0x0000FA0C
			int UnsafeNativeMethods.IOleInPlaceSite.DiscardUndoState()
			{
				return 0;
			}

			// Token: 0x06006268 RID: 25192 RVA: 0x0016BEB1 File Offset: 0x0016A0B1
			int UnsafeNativeMethods.IOleInPlaceSite.DeactivateAndUndo()
			{
				return this.host.GetInPlaceObject().UIDeactivate();
			}

			// Token: 0x06006269 RID: 25193 RVA: 0x0016BEC4 File Offset: 0x0016A0C4
			int UnsafeNativeMethods.IOleInPlaceSite.OnPosRectChange(NativeMethods.COMRECT lprcPosRect)
			{
				bool flag = true;
				if (AxHost.windowsMediaPlayer_Clsid.Equals(this.host.clsid))
				{
					flag = this.host.GetAxState(AxHost.handlePosRectChanged);
				}
				if (flag)
				{
					this.host.GetInPlaceObject().SetObjectRects(lprcPosRect, this.host.GetClipRect(new NativeMethods.COMRECT()));
					this.host.MakeDirty();
				}
				return 0;
			}

			// Token: 0x0600626A RID: 25194 RVA: 0x0016BF2C File Offset: 0x0016A12C
			void UnsafeNativeMethods.IPropertyNotifySink.OnChanged(int dispid)
			{
				if (this.host.NoComponentChangeEvents != 0)
				{
					return;
				}
				AxHost axHost = this.host;
				int num = axHost.NoComponentChangeEvents;
				axHost.NoComponentChangeEvents = num + 1;
				try
				{
					AxHost.AxPropertyDescriptor axPropertyDescriptor = null;
					if (dispid != -1)
					{
						axPropertyDescriptor = this.host.GetPropertyDescriptorFromDispid(dispid);
						if (axPropertyDescriptor != null)
						{
							axPropertyDescriptor.OnValueChanged(this.host);
							if (!axPropertyDescriptor.SettingValue)
							{
								axPropertyDescriptor.UpdateTypeConverterAndTypeEditor(true);
							}
						}
					}
					else
					{
						PropertyDescriptorCollection properties = ((ICustomTypeDescriptor)this.host).GetProperties();
						foreach (object obj in properties)
						{
							PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
							axPropertyDescriptor = propertyDescriptor as AxHost.AxPropertyDescriptor;
							if (axPropertyDescriptor != null && !axPropertyDescriptor.SettingValue)
							{
								axPropertyDescriptor.UpdateTypeConverterAndTypeEditor(true);
							}
						}
					}
					ISite site = this.host.Site;
					if (site != null)
					{
						IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
						if (componentChangeService != null)
						{
							try
							{
								componentChangeService.OnComponentChanging(this.host, axPropertyDescriptor);
							}
							catch (CheckoutException ex)
							{
								if (ex == CheckoutException.Canceled)
								{
									return;
								}
								throw ex;
							}
							componentChangeService.OnComponentChanged(this.host, axPropertyDescriptor, null, (axPropertyDescriptor != null) ? axPropertyDescriptor.GetValue(this.host) : null);
						}
					}
				}
				catch (Exception ex2)
				{
					throw ex2;
				}
				finally
				{
					AxHost axHost2 = this.host;
					num = axHost2.NoComponentChangeEvents;
					axHost2.NoComponentChangeEvents = num - 1;
				}
			}

			// Token: 0x0600626B RID: 25195 RVA: 0x0001180C File Offset: 0x0000FA0C
			int UnsafeNativeMethods.IPropertyNotifySink.OnRequestEdit(int dispid)
			{
				return 0;
			}

			// Token: 0x040038DE RID: 14558
			private AxHost host;

			// Token: 0x040038DF RID: 14559
			private AxHost.ConnectionPointCookie connectionPoint;
		}

		// Token: 0x0200060D RID: 1549
		private class VBFormat : UnsafeNativeMethods.IVBFormat
		{
			// Token: 0x0600626C RID: 25196 RVA: 0x0016C0DC File Offset: 0x0016A2DC
			int UnsafeNativeMethods.IVBFormat.Format(ref object var, IntPtr pszFormat, IntPtr lpBuffer, short cpBuffer, int lcid, short firstD, short firstW, short[] result)
			{
				if (result == null)
				{
					return -2147024809;
				}
				result[0] = 0;
				if (lpBuffer == IntPtr.Zero || cpBuffer < 2)
				{
					return -2147024809;
				}
				IntPtr zero = IntPtr.Zero;
				int num = UnsafeNativeMethods.VarFormat(ref var, new HandleRef(null, pszFormat), (int)firstD, (int)firstW, 32U, ref zero);
				try
				{
					int num2 = 0;
					if (zero != IntPtr.Zero)
					{
						cpBuffer -= 1;
						short num3;
						while (num2 < (int)cpBuffer && (num3 = Marshal.ReadInt16(zero, num2 * 2)) != 0)
						{
							Marshal.WriteInt16(lpBuffer, num2 * 2, num3);
							num2++;
						}
					}
					Marshal.WriteInt16(lpBuffer, num2 * 2, 0);
					result[0] = (short)num2;
				}
				finally
				{
					SafeNativeMethods.SysFreeString(new HandleRef(null, zero));
				}
				return 0;
			}
		}

		// Token: 0x0200060E RID: 1550
		internal class EnumUnknown : UnsafeNativeMethods.IEnumUnknown
		{
			// Token: 0x0600626E RID: 25198 RVA: 0x0016C198 File Offset: 0x0016A398
			internal EnumUnknown(object[] arr)
			{
				this.arr = arr;
				this.loc = 0;
				this.size = ((arr == null) ? 0 : arr.Length);
			}

			// Token: 0x0600626F RID: 25199 RVA: 0x0016C1BD File Offset: 0x0016A3BD
			private EnumUnknown(object[] arr, int loc)
				: this(arr)
			{
				this.loc = loc;
			}

			// Token: 0x06006270 RID: 25200 RVA: 0x0016C1D0 File Offset: 0x0016A3D0
			int UnsafeNativeMethods.IEnumUnknown.Next(int celt, IntPtr rgelt, IntPtr pceltFetched)
			{
				if (pceltFetched != IntPtr.Zero)
				{
					Marshal.WriteInt32(pceltFetched, 0, 0);
				}
				if (celt < 0)
				{
					return -2147024809;
				}
				int num = 0;
				if (this.loc >= this.size)
				{
					num = 0;
				}
				else
				{
					while (this.loc < this.size && num < celt)
					{
						if (this.arr[this.loc] != null)
						{
							Marshal.WriteIntPtr(rgelt, Marshal.GetIUnknownForObject(this.arr[this.loc]));
							rgelt = (IntPtr)((long)rgelt + (long)sizeof(IntPtr));
							num++;
						}
						this.loc++;
					}
				}
				if (pceltFetched != IntPtr.Zero)
				{
					Marshal.WriteInt32(pceltFetched, 0, num);
				}
				if (num != celt)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06006271 RID: 25201 RVA: 0x0016C28C File Offset: 0x0016A48C
			int UnsafeNativeMethods.IEnumUnknown.Skip(int celt)
			{
				this.loc += celt;
				if (this.loc >= this.size)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06006272 RID: 25202 RVA: 0x0016C2AD File Offset: 0x0016A4AD
			void UnsafeNativeMethods.IEnumUnknown.Reset()
			{
				this.loc = 0;
			}

			// Token: 0x06006273 RID: 25203 RVA: 0x0016C2B6 File Offset: 0x0016A4B6
			void UnsafeNativeMethods.IEnumUnknown.Clone(out UnsafeNativeMethods.IEnumUnknown ppenum)
			{
				ppenum = new AxHost.EnumUnknown(this.arr, this.loc);
			}

			// Token: 0x040038E0 RID: 14560
			private object[] arr;

			// Token: 0x040038E1 RID: 14561
			private int loc;

			// Token: 0x040038E2 RID: 14562
			private int size;
		}

		// Token: 0x0200060F RID: 1551
		internal class AxContainer : UnsafeNativeMethods.IOleContainer, UnsafeNativeMethods.IOleInPlaceFrame, IReflect
		{
			// Token: 0x06006274 RID: 25204 RVA: 0x0016C2CB File Offset: 0x0016A4CB
			internal AxContainer(ContainerControl parent)
			{
				this.parent = parent;
				if (parent.Created)
				{
					this.FormCreated();
				}
			}

			// Token: 0x06006275 RID: 25205 RVA: 0x00015C90 File Offset: 0x00013E90
			MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
			{
				return null;
			}

			// Token: 0x06006276 RID: 25206 RVA: 0x00015C90 File Offset: 0x00013E90
			MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
			{
				return null;
			}

			// Token: 0x06006277 RID: 25207 RVA: 0x0016B958 File Offset: 0x00169B58
			MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
			{
				return new MethodInfo[0];
			}

			// Token: 0x06006278 RID: 25208 RVA: 0x00015C90 File Offset: 0x00013E90
			FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
			{
				return null;
			}

			// Token: 0x06006279 RID: 25209 RVA: 0x0016B960 File Offset: 0x00169B60
			FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
			{
				return new FieldInfo[0];
			}

			// Token: 0x0600627A RID: 25210 RVA: 0x00015C90 File Offset: 0x00013E90
			PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
			{
				return null;
			}

			// Token: 0x0600627B RID: 25211 RVA: 0x00015C90 File Offset: 0x00013E90
			PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
			{
				return null;
			}

			// Token: 0x0600627C RID: 25212 RVA: 0x0016B968 File Offset: 0x00169B68
			PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
			{
				return new PropertyInfo[0];
			}

			// Token: 0x0600627D RID: 25213 RVA: 0x0016B970 File Offset: 0x00169B70
			MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
			{
				return new MemberInfo[0];
			}

			// Token: 0x0600627E RID: 25214 RVA: 0x0016B970 File Offset: 0x00169B70
			MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
			{
				return new MemberInfo[0];
			}

			// Token: 0x0600627F RID: 25215 RVA: 0x0016C2F4 File Offset: 0x0016A4F4
			object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
			{
				foreach (object obj in this.containerCache)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string nameForControl = this.GetNameForControl((Control)dictionaryEntry.Key);
					if (nameForControl.Equals(name))
					{
						return this.GetProxyForControl((Control)dictionaryEntry.Value);
					}
				}
				throw AxHost.E_FAIL;
			}

			// Token: 0x1700150C RID: 5388
			// (get) Token: 0x06006280 RID: 25216 RVA: 0x00015C90 File Offset: 0x00013E90
			Type IReflect.UnderlyingSystemType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006281 RID: 25217 RVA: 0x0016C384 File Offset: 0x0016A584
			internal UnsafeNativeMethods.IExtender GetProxyForControl(Control ctl)
			{
				UnsafeNativeMethods.IExtender extender = null;
				if (this.proxyCache == null)
				{
					this.proxyCache = new Hashtable();
				}
				else
				{
					extender = (UnsafeNativeMethods.IExtender)this.proxyCache[ctl];
				}
				if (extender == null)
				{
					if (ctl != this.parent && !this.GetControlBelongs(ctl))
					{
						AxHost.AxContainer axContainer = AxHost.AxContainer.FindContainerForControl(ctl);
						if (axContainer == null)
						{
							return null;
						}
						extender = new AxHost.AxContainer.ExtenderProxy(ctl, axContainer);
					}
					else
					{
						extender = new AxHost.AxContainer.ExtenderProxy(ctl, this);
					}
					this.proxyCache.Add(ctl, extender);
				}
				return extender;
			}

			// Token: 0x06006282 RID: 25218 RVA: 0x0016C3FC File Offset: 0x0016A5FC
			internal string GetNameForControl(Control ctl)
			{
				string text = ((ctl.Site != null) ? ctl.Site.Name : ctl.Name);
				if (text != null)
				{
					return text;
				}
				return "";
			}

			// Token: 0x06006283 RID: 25219 RVA: 0x00006A49 File Offset: 0x00004C49
			internal object GetProxyForContainer()
			{
				return this;
			}

			// Token: 0x06006284 RID: 25220 RVA: 0x0016C430 File Offset: 0x0016A630
			internal void AddControl(Control ctl)
			{
				lock (this)
				{
					if (this.containerCache.Contains(ctl))
					{
						throw new ArgumentException(SR.GetString("AXDuplicateControl", new object[] { this.GetNameForControl(ctl) }), "ctl");
					}
					this.containerCache.Add(ctl, ctl);
					if (this.assocContainer == null)
					{
						ISite site = ctl.Site;
						if (site != null)
						{
							this.assocContainer = site.Container;
							IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
							if (componentChangeService != null)
							{
								componentChangeService.ComponentRemoved += this.OnComponentRemoved;
							}
						}
					}
				}
			}

			// Token: 0x06006285 RID: 25221 RVA: 0x0016C4F0 File Offset: 0x0016A6F0
			internal void RemoveControl(Control ctl)
			{
				lock (this)
				{
					if (this.containerCache.Contains(ctl))
					{
						this.containerCache.Remove(ctl);
					}
				}
			}

			// Token: 0x06006286 RID: 25222 RVA: 0x0016C540 File Offset: 0x0016A740
			private void LockComponents()
			{
				this.lockCount++;
			}

			// Token: 0x06006287 RID: 25223 RVA: 0x0016C550 File Offset: 0x0016A750
			private void UnlockComponents()
			{
				this.lockCount--;
				if (this.lockCount == 0)
				{
					this.components = null;
				}
			}

			// Token: 0x06006288 RID: 25224 RVA: 0x0016C570 File Offset: 0x0016A770
			internal UnsafeNativeMethods.IEnumUnknown EnumControls(Control ctl, int dwOleContF, int dwWhich)
			{
				this.GetComponents();
				this.LockComponents();
				UnsafeNativeMethods.IEnumUnknown enumUnknown;
				try
				{
					ArrayList arrayList = null;
					bool flag = (dwWhich & 1073741824) != 0;
					bool flag2 = (dwWhich & 134217728) != 0;
					bool flag3 = (dwWhich & 268435456) != 0;
					bool flag4 = (dwWhich & 536870912) != 0;
					dwWhich &= -2013265921;
					if (flag3 && flag4)
					{
						throw AxHost.E_INVALIDARG;
					}
					if ((dwWhich == 2 || dwWhich == 3) && (flag3 || flag4))
					{
						throw AxHost.E_INVALIDARG;
					}
					int num = 0;
					int num2 = -1;
					Control[] array = null;
					switch (dwWhich)
					{
					case 1:
					{
						Control parentInternal = ctl.ParentInternal;
						if (parentInternal != null)
						{
							array = parentInternal.GetChildControlsInTabOrder(false);
							if (flag4)
							{
								num2 = ctl.TabIndex;
							}
							else if (flag3)
							{
								num = ctl.TabIndex + 1;
							}
						}
						else
						{
							array = new Control[0];
						}
						ctl = null;
						break;
					}
					case 2:
						arrayList = new ArrayList();
						this.MaybeAdd(arrayList, ctl, flag, dwOleContF, false);
						while (ctl != null)
						{
							AxHost.AxContainer axContainer = AxHost.AxContainer.FindContainerForControl(ctl);
							if (axContainer == null)
							{
								break;
							}
							this.MaybeAdd(arrayList, axContainer.parent, flag, dwOleContF, true);
							ctl = axContainer.parent;
						}
						break;
					case 3:
						array = ctl.GetChildControlsInTabOrder(false);
						ctl = null;
						break;
					case 4:
					{
						Hashtable hashtable = this.GetComponents();
						array = new Control[hashtable.Keys.Count];
						hashtable.Keys.CopyTo(array, 0);
						ctl = this.parent;
						break;
					}
					default:
						throw AxHost.E_INVALIDARG;
					}
					if (arrayList == null)
					{
						arrayList = new ArrayList();
						if (num2 == -1 && array != null)
						{
							num2 = array.Length;
						}
						if (ctl != null)
						{
							this.MaybeAdd(arrayList, ctl, flag, dwOleContF, false);
						}
						for (int i = num; i < num2; i++)
						{
							this.MaybeAdd(arrayList, array[i], flag, dwOleContF, false);
						}
					}
					object[] array2 = new object[arrayList.Count];
					arrayList.CopyTo(array2, 0);
					if (flag2)
					{
						int j = 0;
						int num3 = array2.Length - 1;
						while (j < num3)
						{
							object obj = array2[j];
							array2[j] = array2[num3];
							array2[num3] = obj;
							j++;
							num3--;
						}
					}
					enumUnknown = new AxHost.EnumUnknown(array2);
				}
				finally
				{
					this.UnlockComponents();
				}
				return enumUnknown;
			}

			// Token: 0x06006289 RID: 25225 RVA: 0x0016C794 File Offset: 0x0016A994
			private void MaybeAdd(ArrayList l, Control ctl, bool selected, int dwOleContF, bool ignoreBelong)
			{
				if (!ignoreBelong && ctl != this.parent && !this.GetControlBelongs(ctl))
				{
					return;
				}
				if (selected)
				{
					ISelectionService selectionService = AxHost.GetSelectionService(ctl);
					if (selectionService == null || !selectionService.GetComponentSelected(this))
					{
						return;
					}
				}
				AxHost axHost = ctl as AxHost;
				if (axHost != null && (dwOleContF & 1) != 0)
				{
					l.Add(axHost.GetOcx());
					return;
				}
				if ((dwOleContF & 4) != 0)
				{
					object proxyForControl = this.GetProxyForControl(ctl);
					if (proxyForControl != null)
					{
						l.Add(proxyForControl);
					}
				}
			}

			// Token: 0x0600628A RID: 25226 RVA: 0x0016C808 File Offset: 0x0016AA08
			private void FillComponentsTable(IContainer container)
			{
				if (container != null)
				{
					ComponentCollection componentCollection = container.Components;
					if (componentCollection != null)
					{
						this.components = new Hashtable();
						foreach (object obj in componentCollection)
						{
							IComponent component = (IComponent)obj;
							if (component is Control && component != this.parent && component.Site != null)
							{
								this.components.Add(component, component);
							}
						}
						return;
					}
				}
				bool flag = true;
				Control[] array = new Control[this.containerCache.Values.Count];
				this.containerCache.Values.CopyTo(array, 0);
				if (array != null)
				{
					if (array.Length != 0 && this.components == null)
					{
						this.components = new Hashtable();
						flag = false;
					}
					for (int i = 0; i < array.Length; i++)
					{
						if (flag && !this.components.Contains(array[i]))
						{
							this.components.Add(array[i], array[i]);
						}
					}
				}
				this.GetAllChildren(this.parent);
			}

			// Token: 0x0600628B RID: 25227 RVA: 0x0016C928 File Offset: 0x0016AB28
			private void GetAllChildren(Control ctl)
			{
				if (ctl == null)
				{
					return;
				}
				if (this.components == null)
				{
					this.components = new Hashtable();
				}
				if (ctl != this.parent && !this.components.Contains(ctl))
				{
					this.components.Add(ctl, ctl);
				}
				foreach (object obj in ctl.Controls)
				{
					Control control = (Control)obj;
					this.GetAllChildren(control);
				}
			}

			// Token: 0x0600628C RID: 25228 RVA: 0x0016C9BC File Offset: 0x0016ABBC
			private Hashtable GetComponents()
			{
				return this.GetComponents(this.GetParentsContainer());
			}

			// Token: 0x0600628D RID: 25229 RVA: 0x0016C9CA File Offset: 0x0016ABCA
			private Hashtable GetComponents(IContainer cont)
			{
				if (this.lockCount == 0)
				{
					this.FillComponentsTable(cont);
				}
				return this.components;
			}

			// Token: 0x0600628E RID: 25230 RVA: 0x0016C9E4 File Offset: 0x0016ABE4
			private bool GetControlBelongs(Control ctl)
			{
				Hashtable hashtable = this.GetComponents();
				return hashtable[ctl] != null;
			}

			// Token: 0x0600628F RID: 25231 RVA: 0x0016CA04 File Offset: 0x0016AC04
			private IContainer GetParentIsDesigned()
			{
				ISite site = this.parent.Site;
				if (site != null && site.DesignMode)
				{
					return site.Container;
				}
				return null;
			}

			// Token: 0x06006290 RID: 25232 RVA: 0x0016CA30 File Offset: 0x0016AC30
			private IContainer GetParentsContainer()
			{
				IContainer parentIsDesigned = this.GetParentIsDesigned();
				if (parentIsDesigned != null)
				{
					return parentIsDesigned;
				}
				return this.assocContainer;
			}

			// Token: 0x06006291 RID: 25233 RVA: 0x0016CA50 File Offset: 0x0016AC50
			private bool RegisterControl(AxHost ctl)
			{
				ISite site = ctl.Site;
				if (site != null)
				{
					IContainer container = site.Container;
					if (container != null)
					{
						if (this.assocContainer != null)
						{
							return container == this.assocContainer;
						}
						this.assocContainer = container;
						IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
						if (componentChangeService != null)
						{
							componentChangeService.ComponentRemoved += this.OnComponentRemoved;
						}
						return true;
					}
				}
				return false;
			}

			// Token: 0x06006292 RID: 25234 RVA: 0x0016CAB8 File Offset: 0x0016ACB8
			private void OnComponentRemoved(object sender, ComponentEventArgs e)
			{
				Control control = e.Component as Control;
				if (sender == this.assocContainer && control != null)
				{
					this.RemoveControl(control);
				}
			}

			// Token: 0x06006293 RID: 25235 RVA: 0x0016CAE4 File Offset: 0x0016ACE4
			internal static AxHost.AxContainer FindContainerForControl(Control ctl)
			{
				AxHost axHost = ctl as AxHost;
				if (axHost != null)
				{
					if (axHost.container != null)
					{
						return axHost.container;
					}
					ContainerControl containingControl = axHost.ContainingControl;
					if (containingControl != null)
					{
						AxHost.AxContainer axContainer = containingControl.CreateAxContainer();
						if (axContainer.RegisterControl(axHost))
						{
							axContainer.AddControl(axHost);
							return axContainer;
						}
					}
				}
				return null;
			}

			// Token: 0x06006294 RID: 25236 RVA: 0x0016CB2E File Offset: 0x0016AD2E
			internal void OnInPlaceDeactivate(AxHost site)
			{
				if (this.siteActive == site)
				{
					this.siteActive = null;
					if (site.GetSiteOwnsDeactivation())
					{
						this.parent.ActiveControl = null;
					}
				}
			}

			// Token: 0x06006295 RID: 25237 RVA: 0x0016CB54 File Offset: 0x0016AD54
			internal void OnUIDeactivate(AxHost site)
			{
				this.siteUIActive = null;
				site.RemoveSelectionHandler();
				site.SetSelectionStyle(1);
				site.editMode = 0;
				if (site.GetSiteOwnsDeactivation())
				{
					ContainerControl containingControl = site.ContainingControl;
				}
			}

			// Token: 0x06006296 RID: 25238 RVA: 0x0016CB90 File Offset: 0x0016AD90
			internal void OnUIActivate(AxHost site)
			{
				if (this.siteUIActive == site)
				{
					return;
				}
				if (this.siteUIActive != null && this.siteUIActive != site)
				{
					AxHost axHost = this.siteUIActive;
					bool axState = axHost.GetAxState(AxHost.ownDisposing);
					try
					{
						axHost.SetAxState(AxHost.ownDisposing, true);
						axHost.GetInPlaceObject().UIDeactivate();
					}
					finally
					{
						axHost.SetAxState(AxHost.ownDisposing, axState);
					}
				}
				site.AddSelectionHandler();
				this.siteUIActive = site;
				ContainerControl containingControl = site.ContainingControl;
				if (containingControl != null)
				{
					containingControl.ActiveControl = site;
				}
			}

			// Token: 0x06006297 RID: 25239 RVA: 0x0016CC20 File Offset: 0x0016AE20
			private void ListAxControls(ArrayList list, bool fuseOcx)
			{
				Hashtable hashtable = this.GetComponents();
				if (hashtable == null)
				{
					return;
				}
				Control[] array = new Control[hashtable.Keys.Count];
				hashtable.Keys.CopyTo(array, 0);
				if (array != null)
				{
					foreach (Control control in array)
					{
						AxHost axHost = control as AxHost;
						if (axHost != null)
						{
							if (fuseOcx)
							{
								list.Add(axHost.GetOcx());
							}
							else
							{
								list.Add(control);
							}
						}
					}
				}
			}

			// Token: 0x06006298 RID: 25240 RVA: 0x0016CC92 File Offset: 0x0016AE92
			internal void ControlCreated(AxHost invoker)
			{
				if (this.formAlreadyCreated)
				{
					if (invoker.IsUserMode() && invoker.AwaitingDefreezing())
					{
						invoker.Freeze(false);
						return;
					}
				}
				else
				{
					this.parent.CreateAxContainer();
				}
			}

			// Token: 0x06006299 RID: 25241 RVA: 0x0016CCC0 File Offset: 0x0016AEC0
			internal void FormCreated()
			{
				if (this.formAlreadyCreated)
				{
					return;
				}
				this.formAlreadyCreated = true;
				ArrayList arrayList = new ArrayList();
				this.ListAxControls(arrayList, false);
				AxHost[] array = new AxHost[arrayList.Count];
				arrayList.CopyTo(array, 0);
				foreach (AxHost axHost in array)
				{
					if (axHost.GetOcState() >= 2 && axHost.IsUserMode() && axHost.AwaitingDefreezing())
					{
						axHost.Freeze(false);
					}
				}
			}

			// Token: 0x0600629A RID: 25242 RVA: 0x00139C27 File Offset: 0x00137E27
			int UnsafeNativeMethods.IOleContainer.ParseDisplayName(object pbc, string pszDisplayName, int[] pchEaten, object[] ppmkOut)
			{
				if (ppmkOut != null)
				{
					ppmkOut[0] = null;
				}
				return -2147467263;
			}

			// Token: 0x0600629B RID: 25243 RVA: 0x0016CD34 File Offset: 0x0016AF34
			int UnsafeNativeMethods.IOleContainer.EnumObjects(int grfFlags, out UnsafeNativeMethods.IEnumUnknown ppenum)
			{
				ppenum = null;
				if ((grfFlags & 1) != 0)
				{
					ArrayList arrayList = new ArrayList();
					this.ListAxControls(arrayList, true);
					if (arrayList.Count > 0)
					{
						object[] array = new object[arrayList.Count];
						arrayList.CopyTo(array, 0);
						ppenum = new AxHost.EnumUnknown(array);
						return 0;
					}
				}
				ppenum = new AxHost.EnumUnknown(null);
				return 0;
			}

			// Token: 0x0600629C RID: 25244 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IOleContainer.LockContainer(bool fLock)
			{
				return -2147467263;
			}

			// Token: 0x0600629D RID: 25245 RVA: 0x0016CD87 File Offset: 0x0016AF87
			IntPtr UnsafeNativeMethods.IOleInPlaceFrame.GetWindow()
			{
				return this.parent.Handle;
			}

			// Token: 0x0600629E RID: 25246 RVA: 0x0001180C File Offset: 0x0000FA0C
			int UnsafeNativeMethods.IOleInPlaceFrame.ContextSensitiveHelp(int fEnterMode)
			{
				return 0;
			}

			// Token: 0x0600629F RID: 25247 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IOleInPlaceFrame.GetBorder(NativeMethods.COMRECT lprectBorder)
			{
				return -2147467263;
			}

			// Token: 0x060062A0 RID: 25248 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IOleInPlaceFrame.RequestBorderSpace(NativeMethods.COMRECT pborderwidths)
			{
				return -2147467263;
			}

			// Token: 0x060062A1 RID: 25249 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IOleInPlaceFrame.SetBorderSpace(NativeMethods.COMRECT pborderwidths)
			{
				return -2147467263;
			}

			// Token: 0x060062A2 RID: 25250 RVA: 0x0016CD94 File Offset: 0x0016AF94
			internal void OnExitEditMode(AxHost ctl)
			{
				if (this.ctlInEditMode == null || this.ctlInEditMode != ctl)
				{
					return;
				}
				this.ctlInEditMode = null;
			}

			// Token: 0x060062A3 RID: 25251 RVA: 0x0016CDB0 File Offset: 0x0016AFB0
			int UnsafeNativeMethods.IOleInPlaceFrame.SetActiveObject(UnsafeNativeMethods.IOleInPlaceActiveObject pActiveObject, string pszObjName)
			{
				if (this.siteUIActive != null && this.siteUIActive.iOleInPlaceActiveObjectExternal != pActiveObject)
				{
					if (this.siteUIActive.iOleInPlaceActiveObjectExternal != null)
					{
						Marshal.ReleaseComObject(this.siteUIActive.iOleInPlaceActiveObjectExternal);
					}
					this.siteUIActive.iOleInPlaceActiveObjectExternal = pActiveObject;
				}
				if (pActiveObject == null)
				{
					if (this.ctlInEditMode != null)
					{
						this.ctlInEditMode.editMode = 0;
						this.ctlInEditMode = null;
					}
					return 0;
				}
				AxHost axHost = null;
				if (pActiveObject is UnsafeNativeMethods.IOleObject)
				{
					UnsafeNativeMethods.IOleObject oleObject = (UnsafeNativeMethods.IOleObject)pActiveObject;
					try
					{
						UnsafeNativeMethods.IOleClientSite clientSite = oleObject.GetClientSite();
						if (clientSite is AxHost.OleInterfaces)
						{
							axHost = ((AxHost.OleInterfaces)clientSite).GetAxHost();
						}
					}
					catch (COMException ex)
					{
					}
					if (this.ctlInEditMode != null)
					{
						this.ctlInEditMode.SetSelectionStyle(1);
						this.ctlInEditMode.editMode = 0;
					}
					if (axHost == null)
					{
						this.ctlInEditMode = null;
					}
					else if (!axHost.IsUserMode())
					{
						this.ctlInEditMode = axHost;
						axHost.editMode = 1;
						axHost.AddSelectionHandler();
						axHost.SetSelectionStyle(2);
					}
				}
				return 0;
			}

			// Token: 0x060062A4 RID: 25252 RVA: 0x0001180C File Offset: 0x0000FA0C
			int UnsafeNativeMethods.IOleInPlaceFrame.InsertMenus(IntPtr hmenuShared, NativeMethods.tagOleMenuGroupWidths lpMenuWidths)
			{
				return 0;
			}

			// Token: 0x060062A5 RID: 25253 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IOleInPlaceFrame.SetMenu(IntPtr hmenuShared, IntPtr holemenu, IntPtr hwndActiveObject)
			{
				return -2147467263;
			}

			// Token: 0x060062A6 RID: 25254 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IOleInPlaceFrame.RemoveMenus(IntPtr hmenuShared)
			{
				return -2147467263;
			}

			// Token: 0x060062A7 RID: 25255 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IOleInPlaceFrame.SetStatusText(string pszStatusText)
			{
				return -2147467263;
			}

			// Token: 0x060062A8 RID: 25256 RVA: 0x0003BC68 File Offset: 0x00039E68
			int UnsafeNativeMethods.IOleInPlaceFrame.EnableModeless(bool fEnable)
			{
				return -2147467263;
			}

			// Token: 0x060062A9 RID: 25257 RVA: 0x00012E4E File Offset: 0x0001104E
			int UnsafeNativeMethods.IOleInPlaceFrame.TranslateAccelerator(ref NativeMethods.MSG lpmsg, short wID)
			{
				return 1;
			}

			// Token: 0x040038E3 RID: 14563
			internal ContainerControl parent;

			// Token: 0x040038E4 RID: 14564
			private IContainer assocContainer;

			// Token: 0x040038E5 RID: 14565
			private AxHost siteUIActive;

			// Token: 0x040038E6 RID: 14566
			private AxHost siteActive;

			// Token: 0x040038E7 RID: 14567
			private bool formAlreadyCreated;

			// Token: 0x040038E8 RID: 14568
			private Hashtable containerCache = new Hashtable();

			// Token: 0x040038E9 RID: 14569
			private int lockCount;

			// Token: 0x040038EA RID: 14570
			private Hashtable components;

			// Token: 0x040038EB RID: 14571
			private Hashtable proxyCache;

			// Token: 0x040038EC RID: 14572
			private AxHost ctlInEditMode;

			// Token: 0x040038ED RID: 14573
			private const int GC_CHILD = 1;

			// Token: 0x040038EE RID: 14574
			private const int GC_LASTSIBLING = 2;

			// Token: 0x040038EF RID: 14575
			private const int GC_FIRSTSIBLING = 4;

			// Token: 0x040038F0 RID: 14576
			private const int GC_CONTAINER = 32;

			// Token: 0x040038F1 RID: 14577
			private const int GC_PREVSIBLING = 64;

			// Token: 0x040038F2 RID: 14578
			private const int GC_NEXTSIBLING = 128;

			// Token: 0x020008B3 RID: 2227
			private class ExtenderProxy : UnsafeNativeMethods.IExtender, UnsafeNativeMethods.IVBGetControl, UnsafeNativeMethods.IGetVBAObject, UnsafeNativeMethods.IGetOleObject, IReflect
			{
				// Token: 0x0600725E RID: 29278 RVA: 0x001A2513 File Offset: 0x001A0713
				internal ExtenderProxy(Control principal, AxHost.AxContainer container)
				{
					this.pRef = new WeakReference(principal);
					this.pContainer = new WeakReference(container);
				}

				// Token: 0x0600725F RID: 29279 RVA: 0x001A2533 File Offset: 0x001A0733
				private Control GetP()
				{
					return (Control)this.pRef.Target;
				}

				// Token: 0x06007260 RID: 29280 RVA: 0x001A2545 File Offset: 0x001A0745
				private AxHost.AxContainer GetC()
				{
					return (AxHost.AxContainer)this.pContainer.Target;
				}

				// Token: 0x06007261 RID: 29281 RVA: 0x001A2557 File Offset: 0x001A0757
				int UnsafeNativeMethods.IVBGetControl.EnumControls(int dwOleContF, int dwWhich, out UnsafeNativeMethods.IEnumUnknown ppenum)
				{
					ppenum = this.GetC().EnumControls(this.GetP(), dwOleContF, dwWhich);
					return 0;
				}

				// Token: 0x06007262 RID: 29282 RVA: 0x001A2570 File Offset: 0x001A0770
				object UnsafeNativeMethods.IGetOleObject.GetOleObject(ref Guid riid)
				{
					if (!riid.Equals(AxHost.ioleobject_Guid))
					{
						throw AxHost.E_INVALIDARG;
					}
					Control p = this.GetP();
					if (p != null && p is AxHost)
					{
						return ((AxHost)p).GetOcx();
					}
					throw AxHost.E_FAIL;
				}

				// Token: 0x06007263 RID: 29283 RVA: 0x0016B900 File Offset: 0x00169B00
				int UnsafeNativeMethods.IGetVBAObject.GetObject(ref Guid riid, UnsafeNativeMethods.IVBFormat[] rval, int dwReserved)
				{
					if (rval == null || riid.Equals(Guid.Empty))
					{
						return -2147024809;
					}
					if (riid.Equals(AxHost.ivbformat_Guid))
					{
						rval[0] = new AxHost.VBFormat();
						return 0;
					}
					rval[0] = null;
					return -2147467262;
				}

				// Token: 0x17001920 RID: 6432
				// (get) Token: 0x06007264 RID: 29284 RVA: 0x001A25B4 File Offset: 0x001A07B4
				// (set) Token: 0x06007265 RID: 29285 RVA: 0x001A25D8 File Offset: 0x001A07D8
				public int Align
				{
					get
					{
						int num = (int)this.GetP().Dock;
						if (num < 0 || num > 4)
						{
							num = 0;
						}
						return num;
					}
					set
					{
						this.GetP().Dock = (DockStyle)value;
					}
				}

				// Token: 0x17001921 RID: 6433
				// (get) Token: 0x06007266 RID: 29286 RVA: 0x001A25E6 File Offset: 0x001A07E6
				// (set) Token: 0x06007267 RID: 29287 RVA: 0x001A25F8 File Offset: 0x001A07F8
				public uint BackColor
				{
					get
					{
						return AxHost.GetOleColorFromColor(this.GetP().BackColor);
					}
					set
					{
						this.GetP().BackColor = AxHost.GetColorFromOleColor(value);
					}
				}

				// Token: 0x17001922 RID: 6434
				// (get) Token: 0x06007268 RID: 29288 RVA: 0x001A260B File Offset: 0x001A080B
				// (set) Token: 0x06007269 RID: 29289 RVA: 0x001A2618 File Offset: 0x001A0818
				public bool Enabled
				{
					get
					{
						return this.GetP().Enabled;
					}
					set
					{
						this.GetP().Enabled = value;
					}
				}

				// Token: 0x17001923 RID: 6435
				// (get) Token: 0x0600726A RID: 29290 RVA: 0x001A2626 File Offset: 0x001A0826
				// (set) Token: 0x0600726B RID: 29291 RVA: 0x001A2638 File Offset: 0x001A0838
				public uint ForeColor
				{
					get
					{
						return AxHost.GetOleColorFromColor(this.GetP().ForeColor);
					}
					set
					{
						this.GetP().ForeColor = AxHost.GetColorFromOleColor(value);
					}
				}

				// Token: 0x17001924 RID: 6436
				// (get) Token: 0x0600726C RID: 29292 RVA: 0x001A264B File Offset: 0x001A084B
				// (set) Token: 0x0600726D RID: 29293 RVA: 0x001A265E File Offset: 0x001A085E
				public int Height
				{
					get
					{
						return AxHost.Pixel2Twip(this.GetP().Height, false);
					}
					set
					{
						this.GetP().Height = AxHost.Twip2Pixel(value, false);
					}
				}

				// Token: 0x17001925 RID: 6437
				// (get) Token: 0x0600726E RID: 29294 RVA: 0x001A2672 File Offset: 0x001A0872
				// (set) Token: 0x0600726F RID: 29295 RVA: 0x001A2685 File Offset: 0x001A0885
				public int Left
				{
					get
					{
						return AxHost.Pixel2Twip(this.GetP().Left, true);
					}
					set
					{
						this.GetP().Left = AxHost.Twip2Pixel(value, true);
					}
				}

				// Token: 0x17001926 RID: 6438
				// (get) Token: 0x06007270 RID: 29296 RVA: 0x001A2699 File Offset: 0x001A0899
				public object Parent
				{
					get
					{
						return this.GetC().GetProxyForControl(this.GetC().parent);
					}
				}

				// Token: 0x17001927 RID: 6439
				// (get) Token: 0x06007271 RID: 29297 RVA: 0x001A26B1 File Offset: 0x001A08B1
				// (set) Token: 0x06007272 RID: 29298 RVA: 0x001A26BF File Offset: 0x001A08BF
				public short TabIndex
				{
					get
					{
						return (short)this.GetP().TabIndex;
					}
					set
					{
						this.GetP().TabIndex = (int)value;
					}
				}

				// Token: 0x17001928 RID: 6440
				// (get) Token: 0x06007273 RID: 29299 RVA: 0x001A26CD File Offset: 0x001A08CD
				// (set) Token: 0x06007274 RID: 29300 RVA: 0x001A26DA File Offset: 0x001A08DA
				public bool TabStop
				{
					get
					{
						return this.GetP().TabStop;
					}
					set
					{
						this.GetP().TabStop = value;
					}
				}

				// Token: 0x17001929 RID: 6441
				// (get) Token: 0x06007275 RID: 29301 RVA: 0x001A26E8 File Offset: 0x001A08E8
				// (set) Token: 0x06007276 RID: 29302 RVA: 0x001A26FB File Offset: 0x001A08FB
				public int Top
				{
					get
					{
						return AxHost.Pixel2Twip(this.GetP().Top, false);
					}
					set
					{
						this.GetP().Top = AxHost.Twip2Pixel(value, false);
					}
				}

				// Token: 0x1700192A RID: 6442
				// (get) Token: 0x06007277 RID: 29303 RVA: 0x001A270F File Offset: 0x001A090F
				// (set) Token: 0x06007278 RID: 29304 RVA: 0x001A271C File Offset: 0x001A091C
				public bool Visible
				{
					get
					{
						return this.GetP().Visible;
					}
					set
					{
						this.GetP().Visible = value;
					}
				}

				// Token: 0x1700192B RID: 6443
				// (get) Token: 0x06007279 RID: 29305 RVA: 0x001A272A File Offset: 0x001A092A
				// (set) Token: 0x0600727A RID: 29306 RVA: 0x001A273D File Offset: 0x001A093D
				public int Width
				{
					get
					{
						return AxHost.Pixel2Twip(this.GetP().Width, true);
					}
					set
					{
						this.GetP().Width = AxHost.Twip2Pixel(value, true);
					}
				}

				// Token: 0x1700192C RID: 6444
				// (get) Token: 0x0600727B RID: 29307 RVA: 0x001A2751 File Offset: 0x001A0951
				public string Name
				{
					get
					{
						return this.GetC().GetNameForControl(this.GetP());
					}
				}

				// Token: 0x1700192D RID: 6445
				// (get) Token: 0x0600727C RID: 29308 RVA: 0x001A2764 File Offset: 0x001A0964
				public IntPtr Hwnd
				{
					get
					{
						return this.GetP().Handle;
					}
				}

				// Token: 0x1700192E RID: 6446
				// (get) Token: 0x0600727D RID: 29309 RVA: 0x001A2771 File Offset: 0x001A0971
				public object Container
				{
					get
					{
						return this.GetC().GetProxyForContainer();
					}
				}

				// Token: 0x1700192F RID: 6447
				// (get) Token: 0x0600727E RID: 29310 RVA: 0x001A277E File Offset: 0x001A097E
				// (set) Token: 0x0600727F RID: 29311 RVA: 0x001A278B File Offset: 0x001A098B
				public string Text
				{
					get
					{
						return this.GetP().Text;
					}
					set
					{
						this.GetP().Text = value;
					}
				}

				// Token: 0x06007280 RID: 29312 RVA: 0x000070A6 File Offset: 0x000052A6
				public void Move(object left, object top, object width, object height)
				{
				}

				// Token: 0x06007281 RID: 29313 RVA: 0x00015C90 File Offset: 0x00013E90
				MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
				{
					return null;
				}

				// Token: 0x06007282 RID: 29314 RVA: 0x00015C90 File Offset: 0x00013E90
				MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
				{
					return null;
				}

				// Token: 0x06007283 RID: 29315 RVA: 0x001A2799 File Offset: 0x001A0999
				MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
				{
					return new MethodInfo[] { base.GetType().GetMethod("Move") };
				}

				// Token: 0x06007284 RID: 29316 RVA: 0x00015C90 File Offset: 0x00013E90
				FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
				{
					return null;
				}

				// Token: 0x06007285 RID: 29317 RVA: 0x0016B960 File Offset: 0x00169B60
				FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
				{
					return new FieldInfo[0];
				}

				// Token: 0x06007286 RID: 29318 RVA: 0x001A27B4 File Offset: 0x001A09B4
				PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
				{
					PropertyInfo propertyInfo = this.GetP().GetType().GetProperty(name, bindingAttr);
					if (propertyInfo == null)
					{
						propertyInfo = base.GetType().GetProperty(name, bindingAttr);
					}
					return propertyInfo;
				}

				// Token: 0x06007287 RID: 29319 RVA: 0x001A27EC File Offset: 0x001A09EC
				PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
				{
					PropertyInfo propertyInfo = this.GetP().GetType().GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
					if (propertyInfo == null)
					{
						propertyInfo = base.GetType().GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
					}
					return propertyInfo;
				}

				// Token: 0x06007288 RID: 29320 RVA: 0x001A2834 File Offset: 0x001A0A34
				PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
				{
					PropertyInfo[] properties = base.GetType().GetProperties(bindingAttr);
					PropertyInfo[] properties2 = this.GetP().GetType().GetProperties(bindingAttr);
					if (properties == null)
					{
						return properties2;
					}
					if (properties2 == null)
					{
						return properties;
					}
					int num = 0;
					PropertyInfo[] array = new PropertyInfo[properties.Length + properties2.Length];
					foreach (PropertyInfo propertyInfo in properties)
					{
						array[num++] = propertyInfo;
					}
					foreach (PropertyInfo propertyInfo2 in properties2)
					{
						array[num++] = propertyInfo2;
					}
					return array;
				}

				// Token: 0x06007289 RID: 29321 RVA: 0x001A28C8 File Offset: 0x001A0AC8
				MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
				{
					MemberInfo[] array = this.GetP().GetType().GetMember(name, bindingAttr);
					if (array == null)
					{
						array = base.GetType().GetMember(name, bindingAttr);
					}
					return array;
				}

				// Token: 0x0600728A RID: 29322 RVA: 0x001A28FC File Offset: 0x001A0AFC
				MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
				{
					MemberInfo[] members = base.GetType().GetMembers(bindingAttr);
					MemberInfo[] members2 = this.GetP().GetType().GetMembers(bindingAttr);
					if (members == null)
					{
						return members2;
					}
					if (members2 == null)
					{
						return members;
					}
					MemberInfo[] array = new MemberInfo[members.Length + members2.Length];
					Array.Copy(members, 0, array, 0, members.Length);
					Array.Copy(members2, 0, array, members.Length, members2.Length);
					return array;
				}

				// Token: 0x0600728B RID: 29323 RVA: 0x001A295C File Offset: 0x001A0B5C
				object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
				{
					object obj;
					try
					{
						obj = base.GetType().InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
					}
					catch (MissingMethodException)
					{
						obj = this.GetP().GetType().InvokeMember(name, invokeAttr, binder, this.GetP(), args, modifiers, culture, namedParameters);
					}
					return obj;
				}

				// Token: 0x17001930 RID: 6448
				// (get) Token: 0x0600728C RID: 29324 RVA: 0x00015C90 File Offset: 0x00013E90
				Type IReflect.UnderlyingSystemType
				{
					get
					{
						return null;
					}
				}

				// Token: 0x04004525 RID: 17701
				private WeakReference pRef;

				// Token: 0x04004526 RID: 17702
				private WeakReference pContainer;
			}
		}

		/// <summary>Converts <see cref="T:System.Windows.Forms.AxHost.State" /> objects from one data type to another.</summary>
		// Token: 0x02000610 RID: 1552
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public class StateConverter : TypeConverter
		{
			/// <summary>Returns whether the <see cref="T:System.Windows.Forms.AxHost.StateConverter" /> can convert an object of the specified type to an <see cref="T:System.Windows.Forms.AxHost.State" />, using the specified context.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type from which to convert.</param>
			/// <returns>
			///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.AxHost.StateConverter" /> can perform the conversion; otherwise, <see langword="false" />.</returns>
			// Token: 0x060062AA RID: 25258 RVA: 0x0016CEB0 File Offset: 0x0016B0B0
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(byte[]) || base.CanConvertFrom(context, sourceType);
			}

			/// <summary>Returns whether the <see cref="T:System.Windows.Forms.AxHost.StateConverter" /> can convert an object to the given destination type using the context.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type from which to convert.</param>
			/// <returns>
			///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.AxHost.StateConverter" /> can perform the conversion; otherwise, <see langword="false" />.</returns>
			// Token: 0x060062AB RID: 25259 RVA: 0x0016CECE File Offset: 0x0016B0CE
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(byte[]) || base.CanConvertTo(context, destinationType);
			}

			/// <summary>This member overrides <see cref="M:System.ComponentModel.TypeConverter.ConvertFrom(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object)" />.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
			/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
			/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
			// Token: 0x060062AC RID: 25260 RVA: 0x0016CEEC File Offset: 0x0016B0EC
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is byte[])
				{
					MemoryStream memoryStream = new MemoryStream((byte[])value);
					return new AxHost.State(memoryStream);
				}
				return base.ConvertFrom(context, culture, value);
			}

			/// <summary>This member overrides <see cref="M:System.ComponentModel.TypeConverter.ConvertTo(System.ComponentModel.ITypeDescriptorContext,System.Globalization.CultureInfo,System.Object,System.Type)" />.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If <see langword="null" /> is passed, the current culture is assumed.</param>
			/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
			/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
			/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
			// Token: 0x060062AD RID: 25261 RVA: 0x0016CF20 File Offset: 0x0016B120
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw new ArgumentNullException("destinationType");
				}
				if (!(destinationType == typeof(byte[])))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				if (value != null)
				{
					MemoryStream memoryStream = new MemoryStream();
					AxHost.State state = (AxHost.State)value;
					state.Save(memoryStream);
					memoryStream.Close();
					return memoryStream.ToArray();
				}
				return new byte[0];
			}
		}

		/// <summary>Encapsulates the persisted state of an ActiveX control.</summary>
		// Token: 0x02000611 RID: 1553
		[TypeConverter(typeof(TypeConverter))]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		[Serializable]
		public class State : ISerializable
		{
			// Token: 0x060062AF RID: 25263 RVA: 0x0016CF8C File Offset: 0x0016B18C
			internal State(MemoryStream ms, int storageType, AxHost ctl, AxHost.PropertyBagStream propBag)
			{
				this.type = storageType;
				this.propBag = propBag;
				this.length = (int)ms.Length;
				this.ms = ms;
				this.manualUpdate = ctl.GetAxState(AxHost.manualUpdate);
				this.licenseKey = ctl.GetLicenseKey();
			}

			// Token: 0x060062B0 RID: 25264 RVA: 0x0016CFE6 File Offset: 0x0016B1E6
			internal State(AxHost.PropertyBagStream propBag)
			{
				this.propBag = propBag;
			}

			// Token: 0x060062B1 RID: 25265 RVA: 0x0016CFFC File Offset: 0x0016B1FC
			internal State(MemoryStream ms)
			{
				this.ms = ms;
				this.length = (int)ms.Length;
				this.InitializeFromStream(ms);
			}

			// Token: 0x060062B2 RID: 25266 RVA: 0x0016D026 File Offset: 0x0016B226
			internal State(AxHost ctl)
			{
				this.CreateStorage();
				this.manualUpdate = ctl.GetAxState(AxHost.manualUpdate);
				this.licenseKey = ctl.GetLicenseKey();
				this.type = 2;
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.State" /> class for serializing a state.</summary>
			/// <param name="ms">A <see cref="T:System.IO.Stream" /> in which the state is stored.</param>
			/// <param name="storageType">An <see cref="T:System.Int32" /> indicating the storage type.</param>
			/// <param name="manualUpdate">
			///   <see langword="true" /> for manual updates; otherwise, <see langword="false" />.</param>
			/// <param name="licKey">The license key of the control.</param>
			// Token: 0x060062B3 RID: 25267 RVA: 0x0016D05F File Offset: 0x0016B25F
			public State(Stream ms, int storageType, bool manualUpdate, string licKey)
			{
				this.type = storageType;
				this.length = (int)ms.Length;
				this.manualUpdate = manualUpdate;
				this.licenseKey = licKey;
				this.InitializeBufferFromStream(ms);
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AxHost.State" /> class for deserializing a state.</summary>
			/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> value.</param>
			/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> value.</param>
			// Token: 0x060062B4 RID: 25268 RVA: 0x0016D098 File Offset: 0x0016B298
			protected State(SerializationInfo info, StreamingContext context)
			{
				SerializationInfoEnumerator enumerator = info.GetEnumerator();
				if (enumerator == null)
				{
					return;
				}
				while (enumerator.MoveNext())
				{
					if (string.Compare(enumerator.Name, "Data", true, CultureInfo.InvariantCulture) == 0)
					{
						try
						{
							byte[] array = (byte[])enumerator.Value;
							if (array != null)
							{
								this.InitializeFromStream(new MemoryStream(array));
							}
							continue;
						}
						catch (Exception ex)
						{
							continue;
						}
					}
					if (string.Compare(enumerator.Name, "PropertyBagBinary", true, CultureInfo.InvariantCulture) == 0)
					{
						try
						{
							byte[] array2 = (byte[])enumerator.Value;
							if (array2 != null)
							{
								this.propBag = new AxHost.PropertyBagStream();
								this.propBag.Read(new MemoryStream(array2));
							}
						}
						catch (Exception ex2)
						{
						}
					}
				}
			}

			// Token: 0x1700150D RID: 5389
			// (get) Token: 0x060062B5 RID: 25269 RVA: 0x0016D168 File Offset: 0x0016B368
			// (set) Token: 0x060062B6 RID: 25270 RVA: 0x0016D170 File Offset: 0x0016B370
			internal int Type
			{
				get
				{
					return this.type;
				}
				set
				{
					this.type = value;
				}
			}

			// Token: 0x060062B7 RID: 25271 RVA: 0x0016D179 File Offset: 0x0016B379
			internal bool _GetManualUpdate()
			{
				return this.manualUpdate;
			}

			// Token: 0x060062B8 RID: 25272 RVA: 0x0016D181 File Offset: 0x0016B381
			internal string _GetLicenseKey()
			{
				return this.licenseKey;
			}

			// Token: 0x060062B9 RID: 25273 RVA: 0x0016D18C File Offset: 0x0016B38C
			private void CreateStorage()
			{
				IntPtr intPtr = IntPtr.Zero;
				if (this.buffer != null)
				{
					intPtr = UnsafeNativeMethods.GlobalAlloc(2, this.length);
					IntPtr intPtr2 = UnsafeNativeMethods.GlobalLock(new HandleRef(null, intPtr));
					try
					{
						if (intPtr2 != IntPtr.Zero)
						{
							Marshal.Copy(this.buffer, 0, intPtr2, this.length);
						}
					}
					finally
					{
						UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, intPtr));
					}
				}
				bool flag = false;
				try
				{
					this.iLockBytes = UnsafeNativeMethods.CreateILockBytesOnHGlobal(new HandleRef(null, intPtr), true);
					if (this.buffer == null)
					{
						this.storage = UnsafeNativeMethods.StgCreateDocfileOnILockBytes(this.iLockBytes, 4114, 0);
					}
					else
					{
						this.storage = UnsafeNativeMethods.StgOpenStorageOnILockBytes(this.iLockBytes, null, 18, 0, 0);
					}
				}
				catch (Exception ex)
				{
					flag = true;
				}
				if (flag)
				{
					if (this.iLockBytes == null && intPtr != IntPtr.Zero)
					{
						UnsafeNativeMethods.GlobalFree(new HandleRef(null, intPtr));
					}
					else
					{
						this.iLockBytes = null;
					}
					this.storage = null;
				}
			}

			// Token: 0x060062BA RID: 25274 RVA: 0x0016D298 File Offset: 0x0016B498
			internal UnsafeNativeMethods.IPropertyBag GetPropBag()
			{
				return this.propBag;
			}

			// Token: 0x060062BB RID: 25275 RVA: 0x0016D2A0 File Offset: 0x0016B4A0
			internal UnsafeNativeMethods.IStorage GetStorage()
			{
				if (this.storage == null)
				{
					this.CreateStorage();
				}
				return this.storage;
			}

			// Token: 0x060062BC RID: 25276 RVA: 0x0016D2B8 File Offset: 0x0016B4B8
			internal UnsafeNativeMethods.IStream GetStream()
			{
				if (this.ms == null)
				{
					if (this.buffer == null)
					{
						return null;
					}
					this.ms = new MemoryStream(this.buffer);
				}
				else
				{
					this.ms.Seek(0L, SeekOrigin.Begin);
				}
				return new UnsafeNativeMethods.ComStreamFromDataStream(this.ms);
			}

			// Token: 0x060062BD RID: 25277 RVA: 0x0016D304 File Offset: 0x0016B504
			private void InitializeFromStream(Stream ids)
			{
				BinaryReader binaryReader = new BinaryReader(ids);
				this.type = binaryReader.ReadInt32();
				int num = binaryReader.ReadInt32();
				this.manualUpdate = binaryReader.ReadBoolean();
				int num2 = binaryReader.ReadInt32();
				if (num2 != 0)
				{
					this.licenseKey = new string(binaryReader.ReadChars(num2));
				}
				for (int i = binaryReader.ReadInt32(); i > 0; i--)
				{
					int num3 = binaryReader.ReadInt32();
					ids.Position += (long)num3;
				}
				this.length = binaryReader.ReadInt32();
				if (this.length > 0)
				{
					this.buffer = binaryReader.ReadBytes(this.length);
				}
			}

			// Token: 0x060062BE RID: 25278 RVA: 0x0016D3A4 File Offset: 0x0016B5A4
			private void InitializeBufferFromStream(Stream ids)
			{
				BinaryReader binaryReader = new BinaryReader(ids);
				this.length = binaryReader.ReadInt32();
				if (this.length > 0)
				{
					this.buffer = binaryReader.ReadBytes(this.length);
				}
			}

			// Token: 0x060062BF RID: 25279 RVA: 0x0016D3E0 File Offset: 0x0016B5E0
			internal AxHost.State RefreshStorage(UnsafeNativeMethods.IPersistStorage iPersistStorage)
			{
				if (this.storage == null || this.iLockBytes == null)
				{
					return null;
				}
				iPersistStorage.Save(this.storage, true);
				this.storage.Commit(0);
				iPersistStorage.HandsOffStorage();
				try
				{
					this.buffer = null;
					this.ms = null;
					NativeMethods.STATSTG statstg = new NativeMethods.STATSTG();
					this.iLockBytes.Stat(statstg, 1);
					this.length = (int)statstg.cbSize;
					this.buffer = new byte[this.length];
					IntPtr hglobalFromILockBytes = UnsafeNativeMethods.GetHGlobalFromILockBytes(this.iLockBytes);
					IntPtr intPtr = UnsafeNativeMethods.GlobalLock(new HandleRef(null, hglobalFromILockBytes));
					try
					{
						if (intPtr != IntPtr.Zero)
						{
							Marshal.Copy(intPtr, this.buffer, 0, this.length);
						}
						else
						{
							this.length = 0;
							this.buffer = null;
						}
					}
					finally
					{
						UnsafeNativeMethods.GlobalUnlock(new HandleRef(null, hglobalFromILockBytes));
					}
				}
				finally
				{
					iPersistStorage.SaveCompleted(this.storage);
				}
				return this;
			}

			// Token: 0x060062C0 RID: 25280 RVA: 0x0016D4E0 File Offset: 0x0016B6E0
			internal void Save(MemoryStream stream)
			{
				BinaryWriter binaryWriter = new BinaryWriter(stream);
				binaryWriter.Write(this.type);
				binaryWriter.Write(this.VERSION);
				binaryWriter.Write(this.manualUpdate);
				if (this.licenseKey != null)
				{
					binaryWriter.Write(this.licenseKey.Length);
					binaryWriter.Write(this.licenseKey.ToCharArray());
				}
				else
				{
					binaryWriter.Write(0);
				}
				binaryWriter.Write(0);
				binaryWriter.Write(this.length);
				if (this.buffer != null)
				{
					binaryWriter.Write(this.buffer);
					return;
				}
				if (this.ms != null)
				{
					this.ms.Position = 0L;
					this.ms.WriteTo(stream);
				}
			}

			/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
			/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
			/// <param name="context">The destination for this serialization.</param>
			/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
			// Token: 0x060062C1 RID: 25281 RVA: 0x0016D594 File Offset: 0x0016B794
			void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
			{
				IntSecurity.UnmanagedCode.Demand();
				MemoryStream memoryStream = new MemoryStream();
				this.Save(memoryStream);
				si.AddValue("Data", memoryStream.ToArray());
				if (this.propBag != null)
				{
					try
					{
						memoryStream = new MemoryStream();
						this.propBag.Write(memoryStream);
						si.AddValue("PropertyBagBinary", memoryStream.ToArray());
					}
					catch (Exception ex)
					{
					}
				}
			}

			// Token: 0x040038F3 RID: 14579
			private int VERSION = 1;

			// Token: 0x040038F4 RID: 14580
			private int length;

			// Token: 0x040038F5 RID: 14581
			private byte[] buffer;

			// Token: 0x040038F6 RID: 14582
			internal int type;

			// Token: 0x040038F7 RID: 14583
			private MemoryStream ms;

			// Token: 0x040038F8 RID: 14584
			private UnsafeNativeMethods.IStorage storage;

			// Token: 0x040038F9 RID: 14585
			private UnsafeNativeMethods.ILockBytes iLockBytes;

			// Token: 0x040038FA RID: 14586
			private bool manualUpdate;

			// Token: 0x040038FB RID: 14587
			private string licenseKey;

			// Token: 0x040038FC RID: 14588
			private AxHost.PropertyBagStream propBag;
		}

		// Token: 0x02000612 RID: 1554
		internal class PropertyBagStream : UnsafeNativeMethods.IPropertyBag
		{
			// Token: 0x060062C2 RID: 25282 RVA: 0x0016D60C File Offset: 0x0016B80C
			internal void Read(Stream stream)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				try
				{
					this.bag = (Hashtable)binaryFormatter.Deserialize(stream);
				}
				catch
				{
					this.bag = new Hashtable();
				}
			}

			// Token: 0x060062C3 RID: 25283 RVA: 0x0016D654 File Offset: 0x0016B854
			int UnsafeNativeMethods.IPropertyBag.Read(string pszPropName, ref object pVar, UnsafeNativeMethods.IErrorLog pErrorLog)
			{
				if (!this.bag.Contains(pszPropName))
				{
					return -2147024809;
				}
				pVar = this.bag[pszPropName];
				if (pVar != null)
				{
					return 0;
				}
				return -2147024809;
			}

			// Token: 0x060062C4 RID: 25284 RVA: 0x0016D683 File Offset: 0x0016B883
			int UnsafeNativeMethods.IPropertyBag.Write(string pszPropName, ref object pVar)
			{
				if (pVar != null && !pVar.GetType().IsSerializable)
				{
					return 0;
				}
				this.bag[pszPropName] = pVar;
				return 0;
			}

			// Token: 0x060062C5 RID: 25285 RVA: 0x0016D6A8 File Offset: 0x0016B8A8
			internal void Write(Stream stream)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(stream, this.bag);
			}

			// Token: 0x040038FD RID: 14589
			private Hashtable bag = new Hashtable();
		}

		/// <summary>Represents the method that will display an ActiveX control's About dialog box.</summary>
		// Token: 0x02000613 RID: 1555
		// (Invoke) Token: 0x060062C8 RID: 25288
		protected delegate void AboutBoxDelegate();

		/// <summary>Provides an editor that uses a modal dialog box to display a property page for an ActiveX control.</summary>
		// Token: 0x02000614 RID: 1556
		[ComVisible(false)]
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public class AxComponentEditor : WindowsFormsComponentEditor
		{
			/// <summary>Creates an editor window that allows the user to edit the specified component.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
			/// <param name="obj">The component to edit.</param>
			/// <param name="parent">An <see cref="T:System.Windows.Forms.IWin32Window" /> that the component belongs to.</param>
			/// <returns>
			///   <see langword="true" /> if the component was changed during editing; otherwise, <see langword="false" />.</returns>
			// Token: 0x060062CB RID: 25291 RVA: 0x0016D6DC File Offset: 0x0016B8DC
			public override bool EditComponent(ITypeDescriptorContext context, object obj, IWin32Window parent)
			{
				AxHost axHost = obj as AxHost;
				if (axHost != null)
				{
					try
					{
						((UnsafeNativeMethods.IOleControlSite)axHost.oleSite).ShowPropertyFrame();
						return true;
					}
					catch (Exception ex)
					{
						throw;
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x02000615 RID: 1557
		internal class AxPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x060062CD RID: 25293 RVA: 0x0016D71C File Offset: 0x0016B91C
			internal AxPropertyDescriptor(PropertyDescriptor baseProp, AxHost owner)
				: base(baseProp)
			{
				this.baseProp = baseProp;
				this.owner = owner;
				this.dispid = (DispIdAttribute)baseProp.Attributes[typeof(DispIdAttribute)];
				if (this.dispid != null)
				{
					if (!this.IsBrowsable && !this.IsReadOnly)
					{
						Guid propertyPage = this.GetPropertyPage(this.dispid.Value);
						if (!Guid.Empty.Equals(propertyPage))
						{
							this.AddAttribute(new BrowsableAttribute(true));
						}
					}
					CategoryAttribute categoryForDispid = owner.GetCategoryForDispid(this.dispid.Value);
					if (categoryForDispid != null)
					{
						this.AddAttribute(categoryForDispid);
					}
					if (this.PropertyType.GUID.Equals(AxHost.dataSource_Guid))
					{
						this.SetFlag(8, true);
					}
				}
			}

			// Token: 0x1700150E RID: 5390
			// (get) Token: 0x060062CE RID: 25294 RVA: 0x0016D7ED File Offset: 0x0016B9ED
			public override Type ComponentType
			{
				get
				{
					return this.baseProp.ComponentType;
				}
			}

			// Token: 0x1700150F RID: 5391
			// (get) Token: 0x060062CF RID: 25295 RVA: 0x0016D7FA File Offset: 0x0016B9FA
			public override TypeConverter Converter
			{
				get
				{
					if (this.dispid != null)
					{
						this.UpdateTypeConverterAndTypeEditorInternal(false, this.Dispid);
					}
					if (this.converter == null)
					{
						return base.Converter;
					}
					return this.converter;
				}
			}

			// Token: 0x17001510 RID: 5392
			// (get) Token: 0x060062D0 RID: 25296 RVA: 0x0016D828 File Offset: 0x0016BA28
			internal int Dispid
			{
				get
				{
					DispIdAttribute dispIdAttribute = (DispIdAttribute)this.baseProp.Attributes[typeof(DispIdAttribute)];
					if (dispIdAttribute != null)
					{
						return dispIdAttribute.Value;
					}
					return -1;
				}
			}

			// Token: 0x17001511 RID: 5393
			// (get) Token: 0x060062D1 RID: 25297 RVA: 0x0016D860 File Offset: 0x0016BA60
			public override bool IsReadOnly
			{
				get
				{
					return this.baseProp.IsReadOnly;
				}
			}

			// Token: 0x17001512 RID: 5394
			// (get) Token: 0x060062D2 RID: 25298 RVA: 0x0016D86D File Offset: 0x0016BA6D
			public override Type PropertyType
			{
				get
				{
					return this.baseProp.PropertyType;
				}
			}

			// Token: 0x17001513 RID: 5395
			// (get) Token: 0x060062D3 RID: 25299 RVA: 0x0016D87A File Offset: 0x0016BA7A
			internal bool SettingValue
			{
				get
				{
					return this.GetFlag(16);
				}
			}

			// Token: 0x060062D4 RID: 25300 RVA: 0x0016D884 File Offset: 0x0016BA84
			private void AddAttribute(Attribute attr)
			{
				this.updateAttrs.Add(attr);
			}

			// Token: 0x060062D5 RID: 25301 RVA: 0x0016D893 File Offset: 0x0016BA93
			public override bool CanResetValue(object o)
			{
				return this.baseProp.CanResetValue(o);
			}

			// Token: 0x060062D6 RID: 25302 RVA: 0x0016D8A1 File Offset: 0x0016BAA1
			public override object GetEditor(Type editorBaseType)
			{
				this.UpdateTypeConverterAndTypeEditorInternal(false, this.dispid.Value);
				if (editorBaseType.Equals(typeof(UITypeEditor)) && this.editor != null)
				{
					return this.editor;
				}
				return base.GetEditor(editorBaseType);
			}

			// Token: 0x060062D7 RID: 25303 RVA: 0x0016D8DD File Offset: 0x0016BADD
			private bool GetFlag(int flagValue)
			{
				return (this.flags & flagValue) == flagValue;
			}

			// Token: 0x060062D8 RID: 25304 RVA: 0x0016D8EC File Offset: 0x0016BAEC
			private Guid GetPropertyPage(int dispid)
			{
				try
				{
					NativeMethods.IPerPropertyBrowsing perPropertyBrowsing = this.owner.GetPerPropertyBrowsing();
					if (perPropertyBrowsing == null)
					{
						return Guid.Empty;
					}
					Guid guid;
					if (NativeMethods.Succeeded(perPropertyBrowsing.MapPropertyToPage(dispid, out guid)))
					{
						return guid;
					}
				}
				catch (COMException)
				{
				}
				catch (Exception ex)
				{
				}
				return Guid.Empty;
			}

			// Token: 0x060062D9 RID: 25305 RVA: 0x0016D950 File Offset: 0x0016BB50
			public override object GetValue(object component)
			{
				if ((!this.GetFlag(8) && !this.owner.CanAccessProperties) || this.GetFlag(4))
				{
					return null;
				}
				object value;
				try
				{
					AxHost axHost = this.owner;
					int num = axHost.NoComponentChangeEvents;
					axHost.NoComponentChangeEvents = num + 1;
					value = this.baseProp.GetValue(component);
				}
				catch (Exception ex)
				{
					if (!this.GetFlag(2))
					{
						this.SetFlag(2, true);
						this.AddAttribute(new BrowsableAttribute(false));
						this.owner.RefreshAllProperties = true;
						this.SetFlag(4, true);
					}
					throw ex;
				}
				finally
				{
					AxHost axHost2 = this.owner;
					int num = axHost2.NoComponentChangeEvents;
					axHost2.NoComponentChangeEvents = num - 1;
				}
				return value;
			}

			// Token: 0x060062DA RID: 25306 RVA: 0x0016DA0C File Offset: 0x0016BC0C
			public void OnValueChanged(object component)
			{
				this.OnValueChanged(component, EventArgs.Empty);
			}

			// Token: 0x060062DB RID: 25307 RVA: 0x0016DA1A File Offset: 0x0016BC1A
			public override void ResetValue(object o)
			{
				this.baseProp.ResetValue(o);
			}

			// Token: 0x060062DC RID: 25308 RVA: 0x0016DA28 File Offset: 0x0016BC28
			private void SetFlag(int flagValue, bool value)
			{
				if (value)
				{
					this.flags |= flagValue;
					return;
				}
				this.flags &= ~flagValue;
			}

			// Token: 0x060062DD RID: 25309 RVA: 0x0016DA4C File Offset: 0x0016BC4C
			public override void SetValue(object component, object value)
			{
				if (!this.GetFlag(8) && !this.owner.CanAccessProperties)
				{
					return;
				}
				try
				{
					this.SetFlag(16, true);
					if (this.PropertyType.IsEnum && value.GetType() != this.PropertyType)
					{
						this.baseProp.SetValue(component, Enum.ToObject(this.PropertyType, value));
					}
					else
					{
						this.baseProp.SetValue(component, value);
					}
				}
				finally
				{
					this.SetFlag(16, false);
				}
				this.OnValueChanged(component);
				if (this.owner == component)
				{
					this.owner.SetAxState(AxHost.valueChanged, true);
				}
			}

			// Token: 0x060062DE RID: 25310 RVA: 0x0016DB00 File Offset: 0x0016BD00
			public override bool ShouldSerializeValue(object o)
			{
				return this.baseProp.ShouldSerializeValue(o);
			}

			// Token: 0x060062DF RID: 25311 RVA: 0x0016DB10 File Offset: 0x0016BD10
			internal void UpdateAttributes()
			{
				if (this.updateAttrs.Count == 0)
				{
					return;
				}
				ArrayList arrayList = new ArrayList(this.AttributeArray);
				foreach (object obj in this.updateAttrs)
				{
					Attribute attribute = (Attribute)obj;
					arrayList.Add(attribute);
				}
				Attribute[] array = new Attribute[arrayList.Count];
				arrayList.CopyTo(array, 0);
				this.AttributeArray = array;
				this.updateAttrs.Clear();
			}

			// Token: 0x060062E0 RID: 25312 RVA: 0x0016DBB0 File Offset: 0x0016BDB0
			internal void UpdateTypeConverterAndTypeEditor(bool force)
			{
				if (this.GetFlag(1) && force)
				{
					this.SetFlag(1, false);
				}
			}

			// Token: 0x060062E1 RID: 25313 RVA: 0x0016DBC8 File Offset: 0x0016BDC8
			internal void UpdateTypeConverterAndTypeEditorInternal(bool force, int dispid)
			{
				if (this.GetFlag(1) && !force)
				{
					return;
				}
				if (this.owner.GetOcx() == null)
				{
					return;
				}
				try
				{
					NativeMethods.IPerPropertyBrowsing perPropertyBrowsing = this.owner.GetPerPropertyBrowsing();
					if (perPropertyBrowsing != null)
					{
						NativeMethods.CA_STRUCT ca_STRUCT = new NativeMethods.CA_STRUCT();
						NativeMethods.CA_STRUCT ca_STRUCT2 = new NativeMethods.CA_STRUCT();
						int num = 0;
						try
						{
							num = perPropertyBrowsing.GetPredefinedStrings(dispid, ca_STRUCT, ca_STRUCT2);
						}
						catch (ExternalException ex)
						{
							num = ex.ErrorCode;
						}
						bool flag;
						if (num != 0)
						{
							flag = false;
							if (this.converter is Com2EnumConverter)
							{
								this.converter = null;
							}
						}
						else
						{
							flag = true;
						}
						if (flag)
						{
							OleStrCAMarshaler oleStrCAMarshaler = new OleStrCAMarshaler(ca_STRUCT);
							Int32CAMarshaler int32CAMarshaler = new Int32CAMarshaler(ca_STRUCT2);
							if (oleStrCAMarshaler.Count > 0 && int32CAMarshaler.Count > 0)
							{
								if (this.converter == null)
								{
									this.converter = new AxHost.AxEnumConverter(this, new AxHost.AxPerPropertyBrowsingEnum(this, this.owner, oleStrCAMarshaler, int32CAMarshaler, true));
								}
								else if (this.converter is AxHost.AxEnumConverter)
								{
									((AxHost.AxEnumConverter)this.converter).RefreshValues();
									AxHost.AxPerPropertyBrowsingEnum axPerPropertyBrowsingEnum = ((AxHost.AxEnumConverter)this.converter).com2Enum as AxHost.AxPerPropertyBrowsingEnum;
									if (axPerPropertyBrowsingEnum != null)
									{
										axPerPropertyBrowsingEnum.RefreshArrays(oleStrCAMarshaler, int32CAMarshaler);
									}
								}
							}
						}
						else if ((ComAliasNameAttribute)this.baseProp.Attributes[typeof(ComAliasNameAttribute)] == null)
						{
							Guid propertyPage = this.GetPropertyPage(dispid);
							if (!Guid.Empty.Equals(propertyPage))
							{
								this.editor = new AxHost.AxPropertyTypeEditor(this, propertyPage);
								if (!this.IsBrowsable)
								{
									this.AddAttribute(new BrowsableAttribute(true));
								}
							}
						}
					}
					this.SetFlag(1, true);
				}
				catch (Exception ex2)
				{
				}
			}

			// Token: 0x040038FE RID: 14590
			private PropertyDescriptor baseProp;

			// Token: 0x040038FF RID: 14591
			internal AxHost owner;

			// Token: 0x04003900 RID: 14592
			private DispIdAttribute dispid;

			// Token: 0x04003901 RID: 14593
			private TypeConverter converter;

			// Token: 0x04003902 RID: 14594
			private UITypeEditor editor;

			// Token: 0x04003903 RID: 14595
			private ArrayList updateAttrs = new ArrayList();

			// Token: 0x04003904 RID: 14596
			private int flags;

			// Token: 0x04003905 RID: 14597
			private const int FlagUpdatedEditorAndConverter = 1;

			// Token: 0x04003906 RID: 14598
			private const int FlagCheckGetter = 2;

			// Token: 0x04003907 RID: 14599
			private const int FlagGettterThrew = 4;

			// Token: 0x04003908 RID: 14600
			private const int FlagIgnoreCanAccessProperties = 8;

			// Token: 0x04003909 RID: 14601
			private const int FlagSettingValue = 16;
		}

		// Token: 0x02000616 RID: 1558
		private class AxPropertyTypeEditor : UITypeEditor
		{
			// Token: 0x060062E2 RID: 25314 RVA: 0x0016DD94 File Offset: 0x0016BF94
			public AxPropertyTypeEditor(AxHost.AxPropertyDescriptor pd, Guid guid)
			{
				this.propDesc = pd;
				this.guid = guid;
			}

			// Token: 0x060062E3 RID: 25315 RVA: 0x0016DDAC File Offset: 0x0016BFAC
			public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
			{
				try
				{
					object instance = context.Instance;
					this.propDesc.owner.ShowPropertyPageForDispid(this.propDesc.Dispid, this.guid);
				}
				catch (Exception ex)
				{
					if (provider != null)
					{
						IUIService iuiservice = (IUIService)provider.GetService(typeof(IUIService));
						if (iuiservice != null)
						{
							iuiservice.ShowError(ex, SR.GetString("ErrorTypeConverterFailed"));
						}
					}
				}
				return value;
			}

			// Token: 0x060062E4 RID: 25316 RVA: 0x00016041 File Offset: 0x00014241
			public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
			{
				return UITypeEditorEditStyle.Modal;
			}

			// Token: 0x0400390A RID: 14602
			private AxHost.AxPropertyDescriptor propDesc;

			// Token: 0x0400390B RID: 14603
			private Guid guid;
		}

		// Token: 0x02000617 RID: 1559
		private class AxEnumConverter : Com2EnumConverter
		{
			// Token: 0x060062E5 RID: 25317 RVA: 0x0016DE24 File Offset: 0x0016C024
			public AxEnumConverter(AxHost.AxPropertyDescriptor target, Com2Enum com2Enum)
				: base(com2Enum)
			{
				this.target = target;
			}

			// Token: 0x060062E6 RID: 25318 RVA: 0x0016DE34 File Offset: 0x0016C034
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				TypeConverter converter = this.target.Converter;
				return base.GetStandardValues(context);
			}

			// Token: 0x0400390C RID: 14604
			private AxHost.AxPropertyDescriptor target;
		}

		// Token: 0x02000618 RID: 1560
		private class AxPerPropertyBrowsingEnum : Com2Enum
		{
			// Token: 0x060062E7 RID: 25319 RVA: 0x0016DE56 File Offset: 0x0016C056
			public AxPerPropertyBrowsingEnum(AxHost.AxPropertyDescriptor targetObject, AxHost owner, OleStrCAMarshaler names, Int32CAMarshaler values, bool allowUnknowns)
				: base(new string[0], new object[0], allowUnknowns)
			{
				this.target = targetObject;
				this.nameMarshaller = names;
				this.valueMarshaller = values;
				this.owner = owner;
				this.arraysFetched = false;
			}

			// Token: 0x17001514 RID: 5396
			// (get) Token: 0x060062E8 RID: 25320 RVA: 0x0016DE90 File Offset: 0x0016C090
			public override object[] Values
			{
				get
				{
					this.EnsureArrays();
					return base.Values;
				}
			}

			// Token: 0x17001515 RID: 5397
			// (get) Token: 0x060062E9 RID: 25321 RVA: 0x0016DE9E File Offset: 0x0016C09E
			public override string[] Names
			{
				get
				{
					this.EnsureArrays();
					return base.Names;
				}
			}

			// Token: 0x060062EA RID: 25322 RVA: 0x0016DEAC File Offset: 0x0016C0AC
			private void EnsureArrays()
			{
				if (this.arraysFetched)
				{
					return;
				}
				this.arraysFetched = true;
				try
				{
					object[] items = this.nameMarshaller.Items;
					object[] items2 = this.valueMarshaller.Items;
					NativeMethods.IPerPropertyBrowsing perPropertyBrowsing = this.owner.GetPerPropertyBrowsing();
					int num = 0;
					if (items.Length != 0)
					{
						object[] array = new object[items2.Length];
						NativeMethods.VARIANT variant = new NativeMethods.VARIANT();
						for (int i = 0; i < items.Length; i++)
						{
							int num2 = (int)items2[i];
							if (items[i] != null && items[i] is string)
							{
								variant.vt = 0;
								if (perPropertyBrowsing.GetPredefinedValue(this.target.Dispid, num2, variant) == 0 && variant.vt != 0)
								{
									array[i] = variant.ToObject();
								}
								variant.Clear();
								num++;
							}
						}
						if (num > 0)
						{
							string[] array2 = new string[num];
							Array.Copy(items, 0, array2, 0, num);
							base.PopulateArrays(array2, array);
						}
					}
				}
				catch (Exception ex)
				{
				}
			}

			// Token: 0x060062EB RID: 25323 RVA: 0x0016DFB0 File Offset: 0x0016C1B0
			internal void RefreshArrays(OleStrCAMarshaler names, Int32CAMarshaler values)
			{
				this.nameMarshaller = names;
				this.valueMarshaller = values;
				this.arraysFetched = false;
			}

			// Token: 0x060062EC RID: 25324 RVA: 0x000070A6 File Offset: 0x000052A6
			protected override void PopulateArrays(string[] names, object[] values)
			{
			}

			// Token: 0x060062ED RID: 25325 RVA: 0x0016DFC7 File Offset: 0x0016C1C7
			public override object FromString(string s)
			{
				this.EnsureArrays();
				return base.FromString(s);
			}

			// Token: 0x060062EE RID: 25326 RVA: 0x0016DFD6 File Offset: 0x0016C1D6
			public override string ToString(object v)
			{
				this.EnsureArrays();
				return base.ToString(v);
			}

			// Token: 0x0400390D RID: 14605
			private AxHost.AxPropertyDescriptor target;

			// Token: 0x0400390E RID: 14606
			private AxHost owner;

			// Token: 0x0400390F RID: 14607
			private OleStrCAMarshaler nameMarshaller;

			// Token: 0x04003910 RID: 14608
			private Int32CAMarshaler valueMarshaller;

			// Token: 0x04003911 RID: 14609
			private bool arraysFetched;
		}
	}
}
