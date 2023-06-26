using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Internal;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms.Automation;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;
using Accessibility;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Defines the base class for controls, which are components with visual representation.</summary>
	// Token: 0x02000168 RID: 360
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Text")]
	[DefaultEvent("Click")]
	[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignerSerializer("System.Windows.Forms.Design.ControlCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItemFilter("System.Windows.Forms")]
	public class Control : Component, UnsafeNativeMethods.IOleControl, UnsafeNativeMethods.IOleObject, UnsafeNativeMethods.IOleInPlaceObject, UnsafeNativeMethods.IOleInPlaceActiveObject, UnsafeNativeMethods.IOleWindow, UnsafeNativeMethods.IViewObject, UnsafeNativeMethods.IViewObject2, UnsafeNativeMethods.IPersist, UnsafeNativeMethods.IPersistStreamInit, UnsafeNativeMethods.IPersistPropertyBag, UnsafeNativeMethods.IPersistStorage, UnsafeNativeMethods.IQuickActivate, ISupportOleDropSource, IDropTarget, ISynchronizeInvoke, IWin32Window, IArrangedElement, IComponent, IDisposable, IBindableComponent, IKeyboardToolTip
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class with default settings.</summary>
		// Token: 0x06000F57 RID: 3927 RVA: 0x0002EFCD File Offset: 0x0002D1CD
		public Control()
			: this(true)
		{
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0002EFD8 File Offset: 0x0002D1D8
		internal Control(bool autoInstallSyncContext)
		{
			this.propertyStore = new PropertyStore();
			DpiHelper.InitializeDpiHelperForWinforms();
			this.deviceDpi = DpiHelper.DeviceDpi;
			this.window = new Control.ControlNativeWindow(this);
			this.RequiredScalingEnabled = true;
			this.RequiredScaling = BoundsSpecified.All;
			this.tabIndex = -1;
			this.state = 131086;
			this.state2 = 8;
			this.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.Selectable | ControlStyles.StandardDoubleClick | ControlStyles.AllPaintingInWmPaint | ControlStyles.UseTextForAccessibility, true);
			this.InitMouseWheelSupport();
			if (this.DefaultMargin != CommonProperties.DefaultMargin)
			{
				this.Margin = this.DefaultMargin;
			}
			if (this.DefaultMinimumSize != CommonProperties.DefaultMinimumSize)
			{
				this.MinimumSize = this.DefaultMinimumSize;
			}
			if (this.DefaultMaximumSize != CommonProperties.DefaultMaximumSize)
			{
				this.MaximumSize = this.DefaultMaximumSize;
			}
			Size defaultSize = this.DefaultSize;
			this.width = defaultSize.Width;
			this.height = defaultSize.Height;
			CommonProperties.xClearPreferredSizeCache(this);
			if (this.width != 0 && this.height != 0)
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				rect.left = (rect.right = (rect.top = (rect.bottom = 0)));
				CreateParams createParams = this.CreateParams;
				this.AdjustWindowRectEx(ref rect, createParams.Style, false, createParams.ExStyle);
				this.clientWidth = this.width - (rect.right - rect.left);
				this.clientHeight = this.height - (rect.bottom - rect.top);
			}
			if (autoInstallSyncContext)
			{
				WindowsFormsSynchronizationContext.InstallIfNeeded();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class with specific text.</summary>
		/// <param name="text">The text displayed by the control.</param>
		// Token: 0x06000F59 RID: 3929 RVA: 0x0002F172 File Offset: 0x0002D372
		public Control(string text)
			: this(null, text)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class with specific text, size, and location.</summary>
		/// <param name="text">The text displayed by the control.</param>
		/// <param name="left">The <see cref="P:System.Drawing.Point.X" /> position of the control, in pixels, from the left edge of the control's container. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Left" /> property.</param>
		/// <param name="top">The <see cref="P:System.Drawing.Point.Y" /> position of the control, in pixels, from the top edge of the control's container. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Top" /> property.</param>
		/// <param name="width">The width of the control, in pixels. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Width" /> property.</param>
		/// <param name="height">The height of the control, in pixels. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Height" /> property.</param>
		// Token: 0x06000F5A RID: 3930 RVA: 0x0002F17C File Offset: 0x0002D37C
		public Control(string text, int left, int top, int width, int height)
			: this(null, text, left, top, width, height)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class as a child control, with specific text.</summary>
		/// <param name="parent">The <see cref="T:System.Windows.Forms.Control" /> to be the parent of the control.</param>
		/// <param name="text">The text displayed by the control.</param>
		// Token: 0x06000F5B RID: 3931 RVA: 0x0002F18C File Offset: 0x0002D38C
		public Control(Control parent, string text)
			: this()
		{
			this.Parent = parent;
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class as a child control, with specific text, size, and location.</summary>
		/// <param name="parent">The <see cref="T:System.Windows.Forms.Control" /> to be the parent of the control.</param>
		/// <param name="text">The text displayed by the control.</param>
		/// <param name="left">The <see cref="P:System.Drawing.Point.X" /> position of the control, in pixels, from the left edge of the control's container. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Left" /> property.</param>
		/// <param name="top">The <see cref="P:System.Drawing.Point.Y" /> position of the control, in pixels, from the top edge of the control's container. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Top" /> property.</param>
		/// <param name="width">The width of the control, in pixels. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Width" /> property.</param>
		/// <param name="height">The height of the control, in pixels. The value is assigned to the <see cref="P:System.Windows.Forms.Control.Height" /> property.</param>
		// Token: 0x06000F5C RID: 3932 RVA: 0x0002F1A2 File Offset: 0x0002D3A2
		public Control(Control parent, string text, int left, int top, int width, int height)
			: this(parent, text)
		{
			this.Location = new Point(left, top);
			this.Size = new Size(width, height);
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0002F1C9 File Offset: 0x0002D3C9
		internal DpiAwarenessContext DpiAwarenessContext
		{
			get
			{
				return this.window.DpiAwarenessContext;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.AccessibleObject" /> assigned to the control.</returns>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0002F1D8 File Offset: 0x0002D3D8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlAccessibilityObjectDescr")]
		public AccessibleObject AccessibilityObject
		{
			get
			{
				AccessibleObject accessibleObject = (AccessibleObject)this.Properties.GetObject(Control.PropAccessibility);
				if (accessibleObject == null)
				{
					accessibleObject = this.CreateAccessibilityInstance();
					if (!(accessibleObject is Control.ControlAccessibleObject))
					{
						return null;
					}
					this.Properties.SetObject(Control.PropAccessibility, accessibleObject);
				}
				return accessibleObject;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0002F224 File Offset: 0x0002D424
		private AccessibleObject NcAccessibilityObject
		{
			get
			{
				AccessibleObject accessibleObject = (AccessibleObject)this.Properties.GetObject(Control.PropNcAccessibility);
				if (accessibleObject == null)
				{
					accessibleObject = new Control.ControlAccessibleObject(this, 0);
					this.Properties.SetObject(Control.PropNcAccessibility, accessibleObject);
				}
				return accessibleObject;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x0002F264 File Offset: 0x0002D464
		private InternalAccessibleObject UnsafeAccessibilityObject
		{
			get
			{
				InternalAccessibleObject internalAccessibleObject = (InternalAccessibleObject)this.Properties.GetObject(Control.PropUnsafeAccessibility);
				if (internalAccessibleObject == null)
				{
					internalAccessibleObject = Control.CreateInternalAccessibleObject(this.AccessibilityObject);
					this.Properties.SetObject(Control.PropUnsafeAccessibility, internalAccessibleObject);
				}
				IntSecurity.UnmanagedCode.Assert();
				InternalAccessibleObject internalAccessibleObject2;
				try
				{
					internalAccessibleObject2 = internalAccessibleObject;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				return internalAccessibleObject2;
			}
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0002F2CC File Offset: 0x0002D4CC
		internal static InternalAccessibleObject CreateInternalAccessibleObject(AccessibleObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			IntSecurity.UnmanagedCode.Assert();
			InternalAccessibleObject internalAccessibleObject;
			try
			{
				internalAccessibleObject = new InternalAccessibleObject(obj);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return internalAccessibleObject;
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0002F308 File Offset: 0x0002D508
		private AccessibleObject GetAccessibilityObject(int accObjId)
		{
			AccessibleObject accessibleObject;
			if (accObjId != -4)
			{
				if (accObjId != 0)
				{
					if (accObjId > 0)
					{
						accessibleObject = this.GetAccessibilityObjectById(accObjId);
					}
					else
					{
						accessibleObject = null;
					}
				}
				else
				{
					accessibleObject = this.NcAccessibilityObject;
				}
			}
			else
			{
				accessibleObject = this.AccessibilityObject;
			}
			return accessibleObject;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0002F344 File Offset: 0x0002D544
		private InternalAccessibleObject GetInternalAccessibilityObject(int accObjId)
		{
			if (accObjId == -4)
			{
				return this.UnsafeAccessibilityObject;
			}
			AccessibleObject accessibilityObject = this.GetAccessibilityObject(accObjId);
			if (accessibilityObject == null)
			{
				return null;
			}
			return Control.CreateInternalAccessibleObject(accessibilityObject);
		}

		/// <summary>Retrieves the specified <see cref="T:System.Windows.Forms.AccessibleObject" />.</summary>
		/// <param name="objectId">An <see langword="Int32" /> that identifies the <see cref="T:System.Windows.Forms.AccessibleObject" /> to retrieve.</param>
		/// <returns>The specified <see cref="T:System.Windows.Forms.AccessibleObject" />.</returns>
		// Token: 0x06000F64 RID: 3940 RVA: 0x0002F370 File Offset: 0x0002D570
		protected virtual AccessibleObject GetAccessibilityObjectById(int objectId)
		{
			if (AccessibilityImprovements.Level3 && this is IAutomationLiveRegion)
			{
				return this.AccessibilityObject;
			}
			return null;
		}

		/// <summary>Gets or sets the default action description of the control for use by accessibility client applications.</summary>
		/// <returns>The default action description of the control for use by accessibility client applications.</returns>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0002F389 File Offset: 0x0002D589
		// (set) Token: 0x06000F66 RID: 3942 RVA: 0x0002F3A0 File Offset: 0x0002D5A0
		[SRCategory("CatAccessibility")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlAccessibleDefaultActionDescr")]
		public string AccessibleDefaultActionDescription
		{
			get
			{
				return (string)this.Properties.GetObject(Control.PropAccessibleDefaultActionDescription);
			}
			set
			{
				this.Properties.SetObject(Control.PropAccessibleDefaultActionDescription, value);
			}
		}

		/// <summary>Gets or sets the description of the control used by accessibility client applications.</summary>
		/// <returns>The description of the control used by accessibility client applications. The default is <see langword="null" />.</returns>
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0002F3B3 File Offset: 0x0002D5B3
		// (set) Token: 0x06000F68 RID: 3944 RVA: 0x0002F3CA File Offset: 0x0002D5CA
		[SRCategory("CatAccessibility")]
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("ControlAccessibleDescriptionDescr")]
		public string AccessibleDescription
		{
			get
			{
				return (string)this.Properties.GetObject(Control.PropAccessibleDescription);
			}
			set
			{
				this.Properties.SetObject(Control.PropAccessibleDescription, value);
			}
		}

		/// <summary>Gets or sets the name of the control used by accessibility client applications.</summary>
		/// <returns>The name of the control used by accessibility client applications. The default is <see langword="null" />.</returns>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0002F3DD File Offset: 0x0002D5DD
		// (set) Token: 0x06000F6A RID: 3946 RVA: 0x0002F3F4 File Offset: 0x0002D5F4
		[SRCategory("CatAccessibility")]
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("ControlAccessibleNameDescr")]
		public string AccessibleName
		{
			get
			{
				return (string)this.Properties.GetObject(Control.PropAccessibleName);
			}
			set
			{
				this.Properties.SetObject(Control.PropAccessibleName, value);
			}
		}

		/// <summary>Gets or sets the accessible role of the control</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.AccessibleRole" />. The default is <see langword="Default" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values.</exception>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0002F408 File Offset: 0x0002D608
		// (set) Token: 0x06000F6C RID: 3948 RVA: 0x0002F42E File Offset: 0x0002D62E
		[SRCategory("CatAccessibility")]
		[DefaultValue(AccessibleRole.Default)]
		[SRDescription("ControlAccessibleRoleDescr")]
		public AccessibleRole AccessibleRole
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(Control.PropAccessibleRole, out flag);
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
				this.Properties.SetInteger(Control.PropAccessibleRole, (int)value);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0002F468 File Offset: 0x0002D668
		private Color ActiveXAmbientBackColor
		{
			get
			{
				return this.ActiveXInstance.AmbientBackColor;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0002F475 File Offset: 0x0002D675
		private Color ActiveXAmbientForeColor
		{
			get
			{
				return this.ActiveXInstance.AmbientForeColor;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0002F482 File Offset: 0x0002D682
		private Font ActiveXAmbientFont
		{
			get
			{
				return this.ActiveXInstance.AmbientFont;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0002F48F File Offset: 0x0002D68F
		private bool ActiveXEventsFrozen
		{
			get
			{
				return this.ActiveXInstance.EventsFrozen;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0002F49C File Offset: 0x0002D69C
		private IntPtr ActiveXHWNDParent
		{
			get
			{
				return this.ActiveXInstance.HWNDParent;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0002F4AC File Offset: 0x0002D6AC
		private Control.ActiveXImpl ActiveXInstance
		{
			get
			{
				Control.ActiveXImpl activeXImpl = (Control.ActiveXImpl)this.Properties.GetObject(Control.PropActiveXImpl);
				if (activeXImpl == null)
				{
					if (this.GetState(524288))
					{
						throw new NotSupportedException(SR.GetString("AXTopLevelSource"));
					}
					activeXImpl = new Control.ActiveXImpl(this);
					this.SetState2(1024, true);
					this.Properties.SetObject(Control.PropActiveXImpl, activeXImpl);
				}
				return activeXImpl;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control can accept data that the user drags onto it.</summary>
		/// <returns>
		///   <see langword="true" /> if drag-and-drop operations are allowed in the control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0002F514 File Offset: 0x0002D714
		// (set) Token: 0x06000F74 RID: 3956 RVA: 0x0002F520 File Offset: 0x0002D720
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ControlAllowDropDescr")]
		public virtual bool AllowDrop
		{
			get
			{
				return this.GetState(64);
			}
			set
			{
				if (this.GetState(64) != value)
				{
					if (value && !this.IsHandleCreated)
					{
						IntSecurity.ClipboardRead.Demand();
					}
					this.SetState(64, value);
					if (this.IsHandleCreated)
					{
						try
						{
							this.SetAcceptDrops(value);
						}
						catch
						{
							this.SetState(64, !value);
							throw;
						}
					}
				}
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0002F588 File Offset: 0x0002D788
		private AmbientProperties AmbientPropertiesService
		{
			get
			{
				bool flag;
				AmbientProperties ambientProperties = (AmbientProperties)this.Properties.GetObject(Control.PropAmbientPropertiesService, out flag);
				if (!flag)
				{
					if (this.Site != null)
					{
						ambientProperties = (AmbientProperties)this.Site.GetService(typeof(AmbientProperties));
					}
					else
					{
						ambientProperties = (AmbientProperties)this.GetService(typeof(AmbientProperties));
					}
					if (ambientProperties != null)
					{
						this.Properties.SetObject(Control.PropAmbientPropertiesService, ambientProperties);
					}
				}
				return ambientProperties;
			}
		}

		/// <summary>Gets or sets the edges of the container to which a control is bound and determines how a control is resized with its parent.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values. The default is <see langword="Top" /> and <see langword="Left" />.</returns>
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0002F600 File Offset: 0x0002D800
		// (set) Token: 0x06000F77 RID: 3959 RVA: 0x0002F608 File Offset: 0x0002D808
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
		[SRDescription("ControlAnchorDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public virtual AnchorStyles Anchor
		{
			get
			{
				return DefaultLayout.GetAnchor(this);
			}
			set
			{
				DefaultLayout.SetAnchor(this.ParentInternal, this, value);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0002F617 File Offset: 0x0002D817
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x0002F620 File Offset: 0x0002D820
		[SRCategory("CatLayout")]
		[RefreshProperties(RefreshProperties.All)]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("ControlAutoSizeDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool AutoSize
		{
			get
			{
				return CommonProperties.GetAutoSize(this);
			}
			set
			{
				if (value != this.AutoSize)
				{
					CommonProperties.SetAutoSize(this, value);
					if (this.ParentInternal != null)
					{
						if (value && this.ParentInternal.LayoutEngine == DefaultLayout.Instance)
						{
							this.ParentInternal.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
						}
						LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.AutoSize);
					}
					this.OnAutoSizeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000084 RID: 132
		// (add) Token: 0x06000F7A RID: 3962 RVA: 0x0002F689 File Offset: 0x0002D889
		// (remove) Token: 0x06000F7B RID: 3963 RVA: 0x0002F69C File Offset: 0x0002D89C
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnAutoSizeChangedDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler AutoSizeChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventAutoSizeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventAutoSizeChanged, value);
			}
		}

		/// <summary>Gets or sets where this control is scrolled to in <see cref="M:System.Windows.Forms.ScrollableControl.ScrollControlIntoView(System.Windows.Forms.Control)" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> specifying the scroll location. The default is the upper-left corner of the control.</returns>
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0002F6AF File Offset: 0x0002D8AF
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x0002F6DE File Offset: 0x0002D8DE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DefaultValue(typeof(Point), "0, 0")]
		public virtual Point AutoScrollOffset
		{
			get
			{
				if (this.Properties.ContainsObject(Control.PropAutoScrollOffset))
				{
					return (Point)this.Properties.GetObject(Control.PropAutoScrollOffset);
				}
				return Point.Empty;
			}
			set
			{
				if (this.AutoScrollOffset != value)
				{
					this.Properties.SetObject(Control.PropAutoScrollOffset, value);
				}
			}
		}

		/// <summary>Sets a value indicating how a control will behave when its <see cref="P:System.Windows.Forms.Control.AutoSize" /> property is enabled.</summary>
		/// <param name="mode">One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values.</param>
		// Token: 0x06000F7E RID: 3966 RVA: 0x0002F704 File Offset: 0x0002D904
		protected void SetAutoSizeMode(AutoSizeMode mode)
		{
			CommonProperties.SetAutoSizeMode(this, mode);
		}

		/// <summary>Retrieves a value indicating how a control will behave when its <see cref="P:System.Windows.Forms.Control.AutoSize" /> property is enabled.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values.</returns>
		// Token: 0x06000F7F RID: 3967 RVA: 0x0002F70D File Offset: 0x0002D90D
		protected AutoSizeMode GetAutoSizeMode()
		{
			return CommonProperties.GetAutoSizeMode(this);
		}

		/// <summary>Gets a cached instance of the control's layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> for the control's contents.</returns>
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0002F715 File Offset: 0x0002D915
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual LayoutEngine LayoutEngine
		{
			get
			{
				return DefaultLayout.Instance;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0002F71C File Offset: 0x0002D91C
		internal IntPtr BackColorBrush
		{
			get
			{
				object @object = this.Properties.GetObject(Control.PropBackBrush);
				if (@object != null)
				{
					return (IntPtr)@object;
				}
				if (!this.Properties.ContainsObject(Control.PropBackColor) && this.parent != null && this.parent.BackColor == this.BackColor)
				{
					return this.parent.BackColorBrush;
				}
				Color backColor = this.BackColor;
				IntPtr intPtr;
				if (ColorTranslator.ToOle(backColor) < 0)
				{
					intPtr = SafeNativeMethods.GetSysColorBrush(ColorTranslator.ToOle(backColor) & 255);
					this.SetState(2097152, false);
				}
				else
				{
					intPtr = SafeNativeMethods.CreateSolidBrush(ColorTranslator.ToWin32(backColor));
					this.SetState(2097152, true);
				}
				this.Properties.SetObject(Control.PropBackBrush, intPtr);
				return intPtr;
			}
		}

		/// <summary>Gets or sets the background color for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0002F7E4 File Offset: 0x0002D9E4
		// (set) Token: 0x06000F83 RID: 3971 RVA: 0x0002F86C File Offset: 0x0002DA6C
		[SRCategory("CatAppearance")]
		[DispId(-501)]
		[SRDescription("ControlBackColorDescr")]
		public virtual Color BackColor
		{
			get
			{
				Color color = this.RawBackColor;
				if (!color.IsEmpty)
				{
					return color;
				}
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null && parentInternal.CanAccessProperties)
				{
					color = parentInternal.BackColor;
					if (this.IsValidBackColor(color))
					{
						return color;
					}
				}
				if (this.IsActiveX)
				{
					color = this.ActiveXAmbientBackColor;
				}
				if (color.IsEmpty)
				{
					AmbientProperties ambientPropertiesService = this.AmbientPropertiesService;
					if (ambientPropertiesService != null)
					{
						color = ambientPropertiesService.BackColor;
					}
				}
				if (!color.IsEmpty && this.IsValidBackColor(color))
				{
					return color;
				}
				return Control.DefaultBackColor;
			}
			set
			{
				if (!value.Equals(Color.Empty) && !this.GetStyle(ControlStyles.SupportsTransparentBackColor) && value.A < 255)
				{
					throw new ArgumentException(SR.GetString("TransparentBackColorNotAllowed"));
				}
				Color backColor = this.BackColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(Control.PropBackColor))
				{
					this.Properties.SetColor(Control.PropBackColor, value);
				}
				if (!backColor.Equals(this.BackColor))
				{
					this.OnBackColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackColor" /> property changes.</summary>
		// Token: 0x14000085 RID: 133
		// (add) Token: 0x06000F84 RID: 3972 RVA: 0x0002F915 File Offset: 0x0002DB15
		// (remove) Token: 0x06000F85 RID: 3973 RVA: 0x0002F928 File Offset: 0x0002DB28
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnBackColorChangedDescr")]
		public event EventHandler BackColorChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventBackColor, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventBackColor, value);
			}
		}

		/// <summary>Gets or sets the background image displayed in the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0002F93B File Offset: 0x0002DB3B
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x0002F952 File Offset: 0x0002DB52
		[SRCategory("CatAppearance")]
		[DefaultValue(null)]
		[Localizable(true)]
		[SRDescription("ControlBackgroundImageDescr")]
		public virtual Image BackgroundImage
		{
			get
			{
				return (Image)this.Properties.GetObject(Control.PropBackgroundImage);
			}
			set
			{
				if (this.BackgroundImage != value)
				{
					this.Properties.SetObject(Control.PropBackgroundImage, value);
					this.OnBackgroundImageChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackgroundImage" /> property changes.</summary>
		// Token: 0x14000086 RID: 134
		// (add) Token: 0x06000F88 RID: 3976 RVA: 0x0002F979 File Offset: 0x0002DB79
		// (remove) Token: 0x06000F89 RID: 3977 RVA: 0x0002F98C File Offset: 0x0002DB8C
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnBackgroundImageChangedDescr")]
		public event EventHandler BackgroundImageChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventBackgroundImage, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventBackgroundImage, value);
			}
		}

		/// <summary>Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" /> (<see cref="F:System.Windows.Forms.ImageLayout.Center" /> , <see cref="F:System.Windows.Forms.ImageLayout.None" />, <see cref="F:System.Windows.Forms.ImageLayout.Stretch" />, <see cref="F:System.Windows.Forms.ImageLayout.Tile" />, or <see cref="F:System.Windows.Forms.ImageLayout.Zoom" />). <see cref="F:System.Windows.Forms.ImageLayout.Tile" /> is the default value.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified enumeration value does not exist.</exception>
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0002F9A0 File Offset: 0x0002DBA0
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x0002F9D8 File Offset: 0x0002DBD8
		[SRCategory("CatAppearance")]
		[DefaultValue(ImageLayout.Tile)]
		[Localizable(true)]
		[SRDescription("ControlBackgroundImageLayoutDescr")]
		public virtual ImageLayout BackgroundImageLayout
		{
			get
			{
				if (!this.Properties.ContainsObject(Control.PropBackgroundImageLayout))
				{
					return ImageLayout.Tile;
				}
				return (ImageLayout)this.Properties.GetObject(Control.PropBackgroundImageLayout);
			}
			set
			{
				if (this.BackgroundImageLayout != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ImageLayout));
					}
					if (value == ImageLayout.Center || value == ImageLayout.Zoom || value == ImageLayout.Stretch)
					{
						this.SetStyle(ControlStyles.ResizeRedraw, true);
						if (ControlPaint.IsImageTransparent(this.BackgroundImage))
						{
							this.DoubleBuffered = true;
						}
					}
					this.Properties.SetObject(Control.PropBackgroundImageLayout, value);
					this.OnBackgroundImageLayoutChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06000F8C RID: 3980 RVA: 0x0002FA5E File Offset: 0x0002DC5E
		// (remove) Token: 0x06000F8D RID: 3981 RVA: 0x0002FA71 File Offset: 0x0002DC71
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnBackgroundImageLayoutChangedDescr")]
		public event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventBackgroundImageLayout, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventBackgroundImageLayout, value);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0002FA84 File Offset: 0x0002DC84
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x0002FA8E File Offset: 0x0002DC8E
		internal bool BecomingActiveControl
		{
			get
			{
				return this.GetState2(32);
			}
			set
			{
				if (value != this.BecomingActiveControl)
				{
					Application.ThreadContext.FromCurrent().ActivatingControl = (value ? this : null);
					this.SetState2(32, value);
				}
			}
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0002FAB4 File Offset: 0x0002DCB4
		private bool ShouldSerializeAccessibleName()
		{
			string accessibleName = this.AccessibleName;
			return accessibleName != null && accessibleName.Length > 0;
		}

		/// <summary>Causes a control bound to the <see cref="T:System.Windows.Forms.BindingSource" /> to reread all the items in the list and refresh their displayed values.</summary>
		// Token: 0x06000F91 RID: 3985 RVA: 0x0002FAD8 File Offset: 0x0002DCD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetBindings()
		{
			ControlBindingsCollection controlBindingsCollection = (ControlBindingsCollection)this.Properties.GetObject(Control.PropBindings);
			if (controlBindingsCollection != null)
			{
				controlBindingsCollection.Clear();
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0002FB04 File Offset: 0x0002DD04
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x0002FB48 File Offset: 0x0002DD48
		internal BindingContext BindingContextInternal
		{
			get
			{
				BindingContext bindingContext = (BindingContext)this.Properties.GetObject(Control.PropBindingManager);
				if (bindingContext != null)
				{
					return bindingContext;
				}
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null && parentInternal.CanAccessProperties)
				{
					return parentInternal.BindingContext;
				}
				return null;
			}
			set
			{
				BindingContext bindingContext = (BindingContext)this.Properties.GetObject(Control.PropBindingManager);
				if (bindingContext != value)
				{
					this.Properties.SetObject(Control.PropBindingManager, value);
					this.OnBindingContextChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</returns>
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0002FB8D File Offset: 0x0002DD8D
		// (set) Token: 0x06000F95 RID: 3989 RVA: 0x0002FB95 File Offset: 0x0002DD95
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlBindingContextDescr")]
		public virtual BindingContext BindingContext
		{
			get
			{
				return this.BindingContextInternal;
			}
			set
			{
				this.BindingContextInternal = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="T:System.Windows.Forms.BindingContext" /> property changes.</summary>
		// Token: 0x14000088 RID: 136
		// (add) Token: 0x06000F96 RID: 3990 RVA: 0x0002FB9E File Offset: 0x0002DD9E
		// (remove) Token: 0x06000F97 RID: 3991 RVA: 0x0002FBB1 File Offset: 0x0002DDB1
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnBindingContextChangedDescr")]
		public event EventHandler BindingContextChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventBindingContext, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventBindingContext, value);
			}
		}

		/// <summary>Gets the distance, in pixels, between the bottom edge of the control and the top edge of its container's client area.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the bottom edge of the control and the top edge of its container's client area.</returns>
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0002FBC4 File Offset: 0x0002DDC4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlBottomDescr")]
		[SRCategory("CatLayout")]
		public int Bottom
		{
			get
			{
				return this.y + this.height;
			}
		}

		/// <summary>Gets or sets the size and location of the control including its nonclient elements, in pixels, relative to the parent control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> in pixels relative to the parent control that represents the size and location of the control including its nonclient elements.</returns>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0002FBD3 File Offset: 0x0002DDD3
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x0002FBF2 File Offset: 0x0002DDF2
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlBoundsDescr")]
		[SRCategory("CatLayout")]
		public Rectangle Bounds
		{
			get
			{
				return new Rectangle(this.x, this.y, this.width, this.height);
			}
			set
			{
				this.SetBounds(value.X, value.Y, value.Width, value.Height, BoundsSpecified.All);
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x00012E4E File Offset: 0x0001104E
		internal virtual bool CanAccessProperties
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the control can receive focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the control can receive focus; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0002FC18 File Offset: 0x0002DE18
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatFocus")]
		[SRDescription("ControlCanFocusDescr")]
		public bool CanFocus
		{
			get
			{
				if (!this.IsHandleCreated)
				{
					return false;
				}
				bool flag = SafeNativeMethods.IsWindowVisible(new HandleRef(this.window, this.Handle));
				bool flag2 = SafeNativeMethods.IsWindowEnabled(new HandleRef(this.window, this.Handle));
				return flag && flag2;
			}
		}

		/// <summary>Determines if events can be raised on the control.</summary>
		/// <returns>
		///   <see langword="true" /> if the control is hosted as an ActiveX control whose events are not frozen; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0002FC60 File Offset: 0x0002DE60
		protected override bool CanRaiseEvents
		{
			get
			{
				return !this.IsActiveX || !this.ActiveXEventsFrozen;
			}
		}

		/// <summary>Gets a value indicating whether the control can be selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the control can be selected; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0002FC75 File Offset: 0x0002DE75
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatFocus")]
		[SRDescription("ControlCanSelectDescr")]
		public bool CanSelect
		{
			get
			{
				return this.CanSelectCore();
			}
		}

		/// <summary>Gets or sets a value indicating whether the control has captured the mouse.</summary>
		/// <returns>
		///   <see langword="true" /> if the control has captured the mouse; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0002FC7D File Offset: 0x0002DE7D
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x0002FC85 File Offset: 0x0002DE85
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatFocus")]
		[SRDescription("ControlCaptureDescr")]
		public bool Capture
		{
			get
			{
				return this.CaptureInternal;
			}
			set
			{
				if (value)
				{
					IntSecurity.GetCapture.Demand();
				}
				this.CaptureInternal = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0002FC9B File Offset: 0x0002DE9B
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x0002FCB7 File Offset: 0x0002DEB7
		internal bool CaptureInternal
		{
			get
			{
				return this.IsHandleCreated && UnsafeNativeMethods.GetCapture() == this.Handle;
			}
			set
			{
				if (this.CaptureInternal != value)
				{
					if (value)
					{
						UnsafeNativeMethods.SetCapture(new HandleRef(this, this.Handle));
						return;
					}
					SafeNativeMethods.ReleaseCapture();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control causes validation to be performed on any controls that require validation when it receives focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the control causes validation to be performed on any controls requiring validation when it receives focus; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0002FCDE File Offset: 0x0002DEDE
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x0002FCEB File Offset: 0x0002DEEB
		[SRCategory("CatFocus")]
		[DefaultValue(true)]
		[SRDescription("ControlCausesValidationDescr")]
		public bool CausesValidation
		{
			get
			{
				return this.GetState(131072);
			}
			set
			{
				if (value != this.CausesValidation)
				{
					this.SetState(131072, value);
					this.OnCausesValidationChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.CausesValidation" /> property changes.</summary>
		// Token: 0x14000089 RID: 137
		// (add) Token: 0x06000FA5 RID: 4005 RVA: 0x0002FD0D File Offset: 0x0002DF0D
		// (remove) Token: 0x06000FA6 RID: 4006 RVA: 0x0002FD20 File Offset: 0x0002DF20
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnCausesValidationChangedDescr")]
		public event EventHandler CausesValidationChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventCausesValidation, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventCausesValidation, value);
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x0002FD34 File Offset: 0x0002DF34
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x0002FD68 File Offset: 0x0002DF68
		internal bool CacheTextInternal
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(Control.PropCacheTextCount, out flag);
				return integer > 0 || this.GetStyle(ControlStyles.CacheText);
			}
			set
			{
				if (this.GetStyle(ControlStyles.CacheText) || !this.IsHandleCreated)
				{
					return;
				}
				bool flag;
				int num = this.Properties.GetInteger(Control.PropCacheTextCount, out flag);
				if (value)
				{
					if (num == 0)
					{
						this.Properties.SetObject(Control.PropCacheTextField, this.text);
						if (this.text == null)
						{
							this.text = this.WindowText;
						}
					}
					num++;
				}
				else
				{
					num--;
					if (num == 0)
					{
						this.text = (string)this.Properties.GetObject(Control.PropCacheTextField, out flag);
					}
				}
				this.Properties.SetInteger(Control.PropCacheTextCount, num);
			}
		}

		/// <summary>Gets or sets a value indicating whether to catch calls on the wrong thread that access a control's <see cref="P:System.Windows.Forms.Control.Handle" /> property when an application is being debugged.</summary>
		/// <returns>
		///   <see langword="true" /> if calls on the wrong thread are caught; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0002FE09 File Offset: 0x0002E009
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x0002FE10 File Offset: 0x0002E010
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ControlCheckForIllegalCrossThreadCalls")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public static bool CheckForIllegalCrossThreadCalls
		{
			get
			{
				return Control.checkForIllegalCrossThreadCalls;
			}
			set
			{
				Control.checkForIllegalCrossThreadCalls = value;
			}
		}

		/// <summary>Gets the rectangle that represents the client area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the client area of the control.</returns>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0002FE18 File Offset: 0x0002E018
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatLayout")]
		[SRDescription("ControlClientRectangleDescr")]
		public Rectangle ClientRectangle
		{
			get
			{
				return new Rectangle(0, 0, this.clientWidth, this.clientHeight);
			}
		}

		/// <summary>Gets or sets the height and width of the client area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the dimensions of the client area of the control.</returns>
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0002FE2D File Offset: 0x0002E02D
		// (set) Token: 0x06000FAD RID: 4013 RVA: 0x0002FE40 File Offset: 0x0002E040
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlClientSizeDescr")]
		public Size ClientSize
		{
			get
			{
				return new Size(this.clientWidth, this.clientHeight);
			}
			set
			{
				this.SetClientSizeCore(value.Width, value.Height);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ClientSize" /> property changes.</summary>
		// Token: 0x1400008A RID: 138
		// (add) Token: 0x06000FAE RID: 4014 RVA: 0x0002FE56 File Offset: 0x0002E056
		// (remove) Token: 0x06000FAF RID: 4015 RVA: 0x0002FE69 File Offset: 0x0002E069
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnClientSizeChangedDescr")]
		public event EventHandler ClientSizeChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventClientSize, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventClientSize, value);
			}
		}

		/// <summary>Gets the name of the company or creator of the application containing the control.</summary>
		/// <returns>The company name or creator of the application containing the control.</returns>
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0002FE7C File Offset: 0x0002E07C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("ControlCompanyNameDescr")]
		public string CompanyName
		{
			get
			{
				return this.VersionInfo.CompanyName;
			}
		}

		/// <summary>Gets a value indicating whether the control, or one of its child controls, currently has the input focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the control or one of its child controls currently has the input focus; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0002FE8C File Offset: 0x0002E08C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlContainsFocusDescr")]
		public bool ContainsFocus
		{
			get
			{
				if (!this.IsHandleCreated)
				{
					return false;
				}
				IntPtr focus = UnsafeNativeMethods.GetFocus();
				return !(focus == IntPtr.Zero) && (focus == this.Handle || UnsafeNativeMethods.IsChild(new HandleRef(this, this.Handle), new HandleRef(this, focus)));
			}
		}

		/// <summary>Gets or sets the shortcut menu associated with the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenu" /> that represents the shortcut menu associated with the control.</returns>
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0002FEE5 File Offset: 0x0002E0E5
		// (set) Token: 0x06000FB3 RID: 4019 RVA: 0x0002FEFC File Offset: 0x0002E0FC
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ControlContextMenuDescr")]
		[Browsable(false)]
		public virtual ContextMenu ContextMenu
		{
			get
			{
				return (ContextMenu)this.Properties.GetObject(Control.PropContextMenu);
			}
			set
			{
				ContextMenu contextMenu = (ContextMenu)this.Properties.GetObject(Control.PropContextMenu);
				if (contextMenu != value)
				{
					EventHandler eventHandler = new EventHandler(this.DetachContextMenu);
					if (contextMenu != null)
					{
						contextMenu.Disposed -= eventHandler;
					}
					this.Properties.SetObject(Control.PropContextMenu, value);
					if (value != null)
					{
						value.Disposed += eventHandler;
					}
					this.OnContextMenuChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ContextMenu" /> property changes.</summary>
		// Token: 0x1400008B RID: 139
		// (add) Token: 0x06000FB4 RID: 4020 RVA: 0x0002FF60 File Offset: 0x0002E160
		// (remove) Token: 0x06000FB5 RID: 4021 RVA: 0x0002FF73 File Offset: 0x0002E173
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnContextMenuChangedDescr")]
		[Browsable(false)]
		public event EventHandler ContextMenuChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventContextMenu, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventContextMenu, value);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with this control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> for this control, or <see langword="null" /> if there is no <see cref="T:System.Windows.Forms.ContextMenuStrip" />. The default is <see langword="null" />.</returns>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0002FF86 File Offset: 0x0002E186
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x0002FFA0 File Offset: 0x0002E1A0
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ControlContextMenuDescr")]
		public virtual ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return (ContextMenuStrip)this.Properties.GetObject(Control.PropContextMenuStrip);
			}
			set
			{
				ContextMenuStrip contextMenuStrip = this.Properties.GetObject(Control.PropContextMenuStrip) as ContextMenuStrip;
				if (contextMenuStrip != value)
				{
					EventHandler eventHandler = new EventHandler(this.DetachContextMenuStrip);
					if (contextMenuStrip != null)
					{
						contextMenuStrip.Disposed -= eventHandler;
					}
					this.Properties.SetObject(Control.PropContextMenuStrip, value);
					if (value != null)
					{
						value.Disposed += eventHandler;
					}
					this.OnContextMenuStripChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ContextMenuStrip" /> property changes.</summary>
		// Token: 0x1400008C RID: 140
		// (add) Token: 0x06000FB8 RID: 4024 RVA: 0x00030004 File Offset: 0x0002E204
		// (remove) Token: 0x06000FB9 RID: 4025 RVA: 0x00030017 File Offset: 0x0002E217
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlContextMenuStripChangedDescr")]
		public event EventHandler ContextMenuStripChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventContextMenuStrip, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventContextMenuStrip, value);
			}
		}

		/// <summary>Gets the collection of controls contained within the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control.ControlCollection" /> representing the collection of controls contained within the control.</returns>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x0003002C File Offset: 0x0002E22C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("ControlControlsDescr")]
		public Control.ControlCollection Controls
		{
			get
			{
				Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
				if (controlCollection == null)
				{
					controlCollection = this.CreateControlsInstance();
					this.Properties.SetObject(Control.PropControlsCollection, controlCollection);
				}
				return controlCollection;
			}
		}

		/// <summary>Gets a value indicating whether the control has been created.</summary>
		/// <returns>
		///   <see langword="true" /> if the control has been created; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x0003006B File Offset: 0x0002E26B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlCreatedDescr")]
		public bool Created
		{
			get
			{
				return (this.state & 1) != 0;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x00030078 File Offset: 0x0002E278
		protected virtual CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (Control.needToLoadComCtl)
				{
					if (!(UnsafeNativeMethods.GetModuleHandle("comctl32.dll") != IntPtr.Zero) && !(UnsafeNativeMethods.LoadLibraryFromSystemPathIfAvailable("comctl32.dll") != IntPtr.Zero))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						throw new Win32Exception(lastWin32Error, SR.GetString("LoadDLLError", new object[] { "comctl32.dll" }));
					}
					Control.needToLoadComCtl = false;
				}
				if (this.createParams == null)
				{
					this.createParams = new CreateParams();
				}
				CreateParams createParams = this.createParams;
				createParams.Style = 0;
				createParams.ExStyle = 0;
				createParams.ClassStyle = 0;
				createParams.Caption = this.text;
				createParams.X = this.x;
				createParams.Y = this.y;
				createParams.Width = this.width;
				createParams.Height = this.height;
				createParams.Style = 33554432;
				if (this.GetStyle(ControlStyles.ContainerControl))
				{
					createParams.ExStyle |= 65536;
				}
				createParams.ClassStyle = 8;
				if ((this.state & 524288) == 0)
				{
					createParams.Parent = ((this.parent == null) ? IntPtr.Zero : this.parent.InternalHandle);
					createParams.Style |= 1140850688;
				}
				else
				{
					createParams.Parent = IntPtr.Zero;
				}
				if ((this.state & 8) != 0)
				{
					createParams.Style |= 65536;
				}
				if ((this.state & 2) != 0)
				{
					createParams.Style |= 268435456;
				}
				if (!this.Enabled)
				{
					createParams.Style |= 134217728;
				}
				if (createParams.Parent == IntPtr.Zero && this.IsActiveX)
				{
					createParams.Parent = this.ActiveXHWNDParent;
				}
				if (this.RightToLeft == RightToLeft.Yes)
				{
					createParams.ExStyle |= 8192;
					createParams.ExStyle |= 4096;
					createParams.ExStyle |= 16384;
				}
				return createParams;
			}
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00030282 File Offset: 0x0002E482
		internal virtual void NotifyValidationResult(object sender, CancelEventArgs ev)
		{
			this.ValidationCancelled = ev.Cancel;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00030290 File Offset: 0x0002E490
		internal bool ValidateActiveControl(out bool validatedControlAllowsFocusChange)
		{
			bool flag = true;
			validatedControlAllowsFocusChange = false;
			IContainerControl containerControlInternal = this.GetContainerControlInternal();
			if (containerControlInternal != null && this.CausesValidation)
			{
				ContainerControl containerControl = containerControlInternal as ContainerControl;
				if (containerControl != null)
				{
					while (containerControl.ActiveControl == null)
					{
						Control parentInternal = containerControl.ParentInternal;
						if (parentInternal == null)
						{
							break;
						}
						ContainerControl containerControl2 = parentInternal.GetContainerControlInternal() as ContainerControl;
						if (containerControl2 == null)
						{
							break;
						}
						containerControl = containerControl2;
					}
					flag = containerControl.ValidateInternal(true, out validatedControlAllowsFocusChange);
				}
			}
			return flag;
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x00030300 File Offset: 0x0002E500
		// (set) Token: 0x06000FBF RID: 4031 RVA: 0x000302F0 File Offset: 0x0002E4F0
		internal bool ValidationCancelled
		{
			get
			{
				if (this.GetState(268435456))
				{
					return true;
				}
				Control parentInternal = this.ParentInternal;
				return parentInternal != null && parentInternal.ValidationCancelled;
			}
			set
			{
				this.SetState(268435456, value);
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x0003033C File Offset: 0x0002E53C
		// (set) Token: 0x06000FC1 RID: 4033 RVA: 0x0003032E File Offset: 0x0002E52E
		internal bool IsTopMdiWindowClosing
		{
			get
			{
				return this.GetState2(4096);
			}
			set
			{
				this.SetState2(4096, value);
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00030357 File Offset: 0x0002E557
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x00030349 File Offset: 0x0002E549
		internal bool IsCurrentlyBeingScaled
		{
			get
			{
				return this.GetState2(8192);
			}
			private set
			{
				this.SetState2(8192, value);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00030364 File Offset: 0x0002E564
		internal int CreateThreadId
		{
			get
			{
				if (this.IsHandleCreated)
				{
					int num;
					return SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this, this.Handle), out num);
				}
				return SafeNativeMethods.GetCurrentThreadId();
			}
		}

		/// <summary>Gets or sets the cursor that is displayed when the mouse pointer is over the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor to display when the mouse pointer is over the control.</returns>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00030394 File Offset: 0x0002E594
		// (set) Token: 0x06000FC7 RID: 4039 RVA: 0x0003041C File Offset: 0x0002E61C
		[SRCategory("CatAppearance")]
		[SRDescription("ControlCursorDescr")]
		[AmbientValue(null)]
		public virtual Cursor Cursor
		{
			get
			{
				if (this.GetState(1024))
				{
					return Cursors.WaitCursor;
				}
				Cursor cursor = (Cursor)this.Properties.GetObject(Control.PropCursor);
				if (cursor != null)
				{
					return cursor;
				}
				Cursor defaultCursor = this.DefaultCursor;
				if (defaultCursor != Cursors.Default)
				{
					return defaultCursor;
				}
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null)
				{
					return parentInternal.Cursor;
				}
				AmbientProperties ambientPropertiesService = this.AmbientPropertiesService;
				if (ambientPropertiesService != null && ambientPropertiesService.Cursor != null)
				{
					return ambientPropertiesService.Cursor;
				}
				return defaultCursor;
			}
			set
			{
				Cursor cursor = (Cursor)this.Properties.GetObject(Control.PropCursor);
				Cursor cursor2 = this.Cursor;
				if (cursor != value)
				{
					IntSecurity.ModifyCursor.Demand();
					this.Properties.SetObject(Control.PropCursor, value);
				}
				if (this.IsHandleCreated)
				{
					NativeMethods.POINT point = new NativeMethods.POINT();
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					UnsafeNativeMethods.GetCursorPos(point);
					UnsafeNativeMethods.GetWindowRect(new HandleRef(this, this.Handle), ref rect);
					if ((rect.left <= point.x && point.x < rect.right && rect.top <= point.y && point.y < rect.bottom) || UnsafeNativeMethods.GetCapture() == this.Handle)
					{
						this.SendMessage(32, this.Handle, (IntPtr)1);
					}
				}
				if (!cursor2.Equals(value))
				{
					this.OnCursorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Cursor" /> property changes.</summary>
		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06000FC8 RID: 4040 RVA: 0x00030511 File Offset: 0x0002E711
		// (remove) Token: 0x06000FC9 RID: 4041 RVA: 0x00030524 File Offset: 0x0002E724
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnCursorChangedDescr")]
		public event EventHandler CursorChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventCursor, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventCursor, value);
			}
		}

		/// <summary>Gets the data bindings for the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ControlBindingsCollection" /> that contains the <see cref="T:System.Windows.Forms.Binding" /> objects for the control.</returns>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00030538 File Offset: 0x0002E738
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatData")]
		[SRDescription("ControlBindingsDescr")]
		[RefreshProperties(RefreshProperties.All)]
		[ParenthesizePropertyName(true)]
		public ControlBindingsCollection DataBindings
		{
			get
			{
				ControlBindingsCollection controlBindingsCollection = (ControlBindingsCollection)this.Properties.GetObject(Control.PropBindings);
				if (controlBindingsCollection == null)
				{
					controlBindingsCollection = new ControlBindingsCollection(this);
					this.Properties.SetObject(Control.PropBindings, controlBindingsCollection);
				}
				return controlBindingsCollection;
			}
		}

		/// <summary>Gets the default background color of the control.</summary>
		/// <returns>The default background <see cref="T:System.Drawing.Color" /> of the control. The default is <see cref="P:System.Drawing.SystemColors.Control" />.</returns>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00030577 File Offset: 0x0002E777
		public static Color DefaultBackColor
		{
			get
			{
				return SystemColors.Control;
			}
		}

		/// <summary>Gets or sets the default cursor for the control.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.Cursor" /> representing the current default cursor.</returns>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0003057E File Offset: 0x0002E77E
		protected virtual Cursor DefaultCursor
		{
			get
			{
				return Cursors.Default;
			}
		}

		/// <summary>Gets the default font of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Font" /> of the control. The value returned will vary depending on the user's operating system the local culture setting of their system.</returns>
		/// <exception cref="T:System.ArgumentException">The default font or the regional alternative fonts are not installed on the client computer.</exception>
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00030585 File Offset: 0x0002E785
		public static Font DefaultFont
		{
			get
			{
				if (Control.defaultFont == null)
				{
					Control.defaultFont = SystemFonts.DefaultFont;
				}
				return Control.defaultFont;
			}
		}

		/// <summary>Gets the default foreground color of the control.</summary>
		/// <returns>The default foreground <see cref="T:System.Drawing.Color" /> of the control. The default is <see cref="P:System.Drawing.SystemColors.ControlText" />.</returns>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x0003059D File Offset: 0x0002E79D
		public static Color DefaultForeColor
		{
			get
			{
				return SystemColors.ControlText;
			}
		}

		/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the default space between controls.</returns>
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x000305A4 File Offset: 0x0002E7A4
		protected virtual Padding DefaultMargin
		{
			get
			{
				return CommonProperties.DefaultMargin;
			}
		}

		/// <summary>Gets the length and height, in pixels, that is specified as the default maximum size of a control.</summary>
		/// <returns>A <see cref="M:System.Drawing.Point.#ctor(System.Drawing.Size)" /> representing the size of the control.</returns>
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x000305AB File Offset: 0x0002E7AB
		protected virtual Size DefaultMaximumSize
		{
			get
			{
				return CommonProperties.DefaultMaximumSize;
			}
		}

		/// <summary>Gets the length and height, in pixels, that is specified as the default minimum size of a control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the size of the control.</returns>
		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x000305B2 File Offset: 0x0002E7B2
		protected virtual Size DefaultMinimumSize
		{
			get
			{
				return CommonProperties.DefaultMinimumSize;
			}
		}

		/// <summary>Gets the internal spacing, in pixels, of the contents of a control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the internal spacing of the contents of a control.</returns>
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00019A61 File Offset: 0x00017C61
		protected virtual Padding DefaultPadding
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0001180C File Offset: 0x0000FA0C
		private RightToLeft DefaultRightToLeft
		{
			get
			{
				return RightToLeft.No;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x000305B9 File Offset: 0x0002E7B9
		protected virtual Size DefaultSize
		{
			get
			{
				return Size.Empty;
			}
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x000305C0 File Offset: 0x0002E7C0
		private void DetachContextMenu(object sender, EventArgs e)
		{
			this.ContextMenu = null;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x000305C9 File Offset: 0x0002E7C9
		private void DetachContextMenuStrip(object sender, EventArgs e)
		{
			this.ContextMenuStrip = null;
		}

		/// <summary>Gets the DPI value for the display device where the control is currently being displayed.</summary>
		/// <returns>The DPI value of the display device.</returns>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x000305D2 File Offset: 0x0002E7D2
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int DeviceDpi
		{
			get
			{
				if (DpiHelper.EnableDpiChangedMessageHandling)
				{
					return this.deviceDpi;
				}
				return DpiHelper.DeviceDpi;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x000305E8 File Offset: 0x0002E7E8
		internal Color DisabledColor
		{
			get
			{
				Color color = this.BackColor;
				if (color.A == 0)
				{
					Control control = this.ParentInternal;
					while (color.A == 0)
					{
						if (control == null)
						{
							color = SystemColors.Control;
							break;
						}
						color = control.BackColor;
						control = control.ParentInternal;
					}
				}
				return color;
			}
		}

		/// <summary>Gets the rectangle that represents the display area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the display area of the control.</returns>
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0002FE18 File Offset: 0x0002E018
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlDisplayRectangleDescr")]
		public virtual Rectangle DisplayRectangle
		{
			get
			{
				return new Rectangle(0, 0, this.clientWidth, this.clientHeight);
			}
		}

		/// <summary>Gets a value indicating whether the control has been disposed of.</summary>
		/// <returns>
		///   <see langword="true" /> if the control has been disposed of; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00030631 File Offset: 0x0002E831
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlDisposedDescr")]
		public bool IsDisposed
		{
			get
			{
				return this.GetState(2048);
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00030640 File Offset: 0x0002E840
		private void DisposeFontHandle()
		{
			if (this.Properties.ContainsObject(Control.PropFontHandleWrapper))
			{
				Control.FontHandleWrapper fontHandleWrapper = this.Properties.GetObject(Control.PropFontHandleWrapper) as Control.FontHandleWrapper;
				if (fontHandleWrapper != null)
				{
					fontHandleWrapper.Dispose();
				}
				this.Properties.SetObject(Control.PropFontHandleWrapper, null);
			}
		}

		/// <summary>Gets a value indicating whether the base <see cref="T:System.Windows.Forms.Control" /> class is in the process of disposing.</summary>
		/// <returns>
		///   <see langword="true" /> if the base <see cref="T:System.Windows.Forms.Control" /> class is in the process of disposing; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x0003068F File Offset: 0x0002E88F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlDisposingDescr")]
		public bool Disposing
		{
			get
			{
				return this.GetState(4096);
			}
		}

		/// <summary>Gets or sets which control borders are docked to its parent control and determines how a control is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.DockStyle" /> values.</exception>
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0003069C File Offset: 0x0002E89C
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x000306A4 File Offset: 0x0002E8A4
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(DockStyle.None)]
		[SRDescription("ControlDockDescr")]
		public virtual DockStyle Dock
		{
			get
			{
				return DefaultLayout.GetDock(this);
			}
			set
			{
				if (value != this.Dock)
				{
					this.SuspendLayout();
					try
					{
						DefaultLayout.SetDock(this, value);
						this.OnDockChanged(EventArgs.Empty);
					}
					finally
					{
						this.ResumeLayout();
					}
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Dock" /> property changes.</summary>
		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06000FDF RID: 4063 RVA: 0x000306EC File Offset: 0x0002E8EC
		// (remove) Token: 0x06000FE0 RID: 4064 RVA: 0x000306FF File Offset: 0x0002E8FF
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnDockChangedDescr")]
		public event EventHandler DockChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventDock, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDock, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flicker.</summary>
		/// <returns>
		///   <see langword="true" /> if the surface of the control should be drawn using double buffering; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00030712 File Offset: 0x0002E912
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x0003071F File Offset: 0x0002E91F
		[SRCategory("CatBehavior")]
		[SRDescription("ControlDoubleBufferedDescr")]
		protected virtual bool DoubleBuffered
		{
			get
			{
				return this.GetStyle(ControlStyles.OptimizedDoubleBuffer);
			}
			set
			{
				if (value != this.DoubleBuffered)
				{
					if (value)
					{
						this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value);
						return;
					}
					this.SetStyle(ControlStyles.OptimizedDoubleBuffer, value);
				}
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x00030746 File Offset: 0x0002E946
		private bool DoubleBufferingEnabled
		{
			get
			{
				return this.GetStyle(ControlStyles.UserPaint | ControlStyles.DoubleBuffer);
			}
		}

		/// <summary>Gets or sets a value indicating whether the control can respond to user interaction.</summary>
		/// <returns>
		///   <see langword="true" /> if the control can respond to user interaction; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x00030753 File Offset: 0x0002E953
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x00030778 File Offset: 0x0002E978
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DispId(-514)]
		[SRDescription("ControlEnabledDescr")]
		public bool Enabled
		{
			get
			{
				return this.GetState(4) && (this.ParentInternal == null || this.ParentInternal.Enabled);
			}
			set
			{
				bool enabled = this.Enabled;
				this.SetState(4, value);
				if (enabled != value)
				{
					if (!value)
					{
						this.SelectNextIfFocused();
					}
					this.OnEnabledChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Enabled" /> property value has changed.</summary>
		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06000FE6 RID: 4070 RVA: 0x000307AC File Offset: 0x0002E9AC
		// (remove) Token: 0x06000FE7 RID: 4071 RVA: 0x000307BF File Offset: 0x0002E9BF
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnEnabledChangedDescr")]
		public event EventHandler EnabledChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventEnabled, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventEnabled, value);
			}
		}

		/// <summary>Gets a value indicating whether the control has input focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the control has focus; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x000307D2 File Offset: 0x0002E9D2
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlFocusedDescr")]
		public virtual bool Focused
		{
			get
			{
				return this.IsHandleCreated && UnsafeNativeMethods.GetFocus() == this.Handle;
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x000307F0 File Offset: 0x0002E9F0
		// (set) Token: 0x06000FEA RID: 4074 RVA: 0x00030858 File Offset: 0x0002EA58
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DispId(-512)]
		[AmbientValue(null)]
		[SRDescription("ControlFontDescr")]
		public virtual Font Font
		{
			[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Windows.Forms.Control/ActiveXFontMarshaler)]
			get
			{
				Font font = (Font)this.Properties.GetObject(Control.PropFont);
				if (font != null)
				{
					return font;
				}
				Font font2 = this.GetParentFont();
				if (font2 != null)
				{
					return font2;
				}
				if (this.IsActiveX)
				{
					font2 = this.ActiveXAmbientFont;
					if (font2 != null)
					{
						return font2;
					}
				}
				AmbientProperties ambientPropertiesService = this.AmbientPropertiesService;
				if (ambientPropertiesService != null && ambientPropertiesService.Font != null)
				{
					return ambientPropertiesService.Font;
				}
				return Control.DefaultFont;
			}
			[param: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Windows.Forms.Control/ActiveXFontMarshaler)]
			set
			{
				Font font = (Font)this.Properties.GetObject(Control.PropFont);
				Font font2 = this.Font;
				bool flag = false;
				if (value == null)
				{
					if (font != null)
					{
						flag = true;
					}
				}
				else
				{
					flag = font == null || !value.Equals(font);
				}
				if (flag)
				{
					this.Properties.SetObject(Control.PropFont, value);
					if (!font2.Equals(value))
					{
						this.DisposeFontHandle();
						if (this.Properties.ContainsInteger(Control.PropFontHeight))
						{
							this.Properties.SetInteger(Control.PropFontHeight, (value == null) ? (-1) : value.Height);
						}
						using (new LayoutTransaction(this.ParentInternal, this, PropertyNames.Font))
						{
							this.OnFontChanged(EventArgs.Empty);
							return;
						}
					}
					if (this.IsHandleCreated && !this.GetStyle(ControlStyles.UserPaint))
					{
						this.DisposeFontHandle();
						this.SetWindowFont();
					}
				}
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0003094C File Offset: 0x0002EB4C
		internal void ScaleFont(float factor)
		{
			Font font = (Font)this.Properties.GetObject(Control.PropFont);
			Font font2 = this.Font;
			Font font3 = (DpiHelper.EnableDpiChangedHighDpiImprovements ? new Font(this.Font.FontFamily, this.Font.Size * factor, this.Font.Style, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont) : new Font(this.Font.FontFamily, this.Font.Size * factor, this.Font.Style));
			if (font == null || !font.Equals(font3))
			{
				this.Properties.SetObject(Control.PropFont, font3);
				if (!font2.Equals(font3))
				{
					this.DisposeFontHandle();
					if (this.Properties.ContainsInteger(Control.PropFontHeight))
					{
						this.Properties.SetInteger(Control.PropFontHeight, font3.Height);
					}
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Font" /> property value changes.</summary>
		// Token: 0x14000090 RID: 144
		// (add) Token: 0x06000FEC RID: 4076 RVA: 0x00030A45 File Offset: 0x0002EC45
		// (remove) Token: 0x06000FED RID: 4077 RVA: 0x00030A58 File Offset: 0x0002EC58
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnFontChangedDescr")]
		public event EventHandler FontChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventFont, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventFont, value);
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x00030A6C File Offset: 0x0002EC6C
		internal IntPtr FontHandle
		{
			get
			{
				Font font = (Font)this.Properties.GetObject(Control.PropFont);
				if (font != null)
				{
					Control.FontHandleWrapper fontHandleWrapper = (Control.FontHandleWrapper)this.Properties.GetObject(Control.PropFontHandleWrapper);
					if (fontHandleWrapper == null)
					{
						fontHandleWrapper = new Control.FontHandleWrapper(font);
						this.Properties.SetObject(Control.PropFontHandleWrapper, fontHandleWrapper);
					}
					return fontHandleWrapper.Handle;
				}
				if (this.parent != null)
				{
					return this.parent.FontHandle;
				}
				AmbientProperties ambientPropertiesService = this.AmbientPropertiesService;
				if (ambientPropertiesService != null && ambientPropertiesService.Font != null)
				{
					Control.FontHandleWrapper fontHandleWrapper2 = null;
					Font font2 = (Font)this.Properties.GetObject(Control.PropCurrentAmbientFont);
					if (font2 != null && font2 == ambientPropertiesService.Font)
					{
						fontHandleWrapper2 = (Control.FontHandleWrapper)this.Properties.GetObject(Control.PropFontHandleWrapper);
					}
					else
					{
						this.Properties.SetObject(Control.PropCurrentAmbientFont, ambientPropertiesService.Font);
					}
					if (fontHandleWrapper2 == null)
					{
						font = ambientPropertiesService.Font;
						fontHandleWrapper2 = new Control.FontHandleWrapper(font);
						this.Properties.SetObject(Control.PropFontHandleWrapper, fontHandleWrapper2);
					}
					return fontHandleWrapper2.Handle;
				}
				return Control.GetDefaultFontHandleWrapper().Handle;
			}
		}

		/// <summary>Gets or sets the height of the font of the control.</summary>
		/// <returns>The height of the <see cref="T:System.Drawing.Font" /> of the control in pixels.</returns>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00030B7C File Offset: 0x0002ED7C
		// (set) Token: 0x06000FF0 RID: 4080 RVA: 0x00030C1D File Offset: 0x0002EE1D
		protected int FontHeight
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(Control.PropFontHeight, out flag);
				if (flag && integer != -1)
				{
					return integer;
				}
				Font font = (Font)this.Properties.GetObject(Control.PropFont);
				if (font != null)
				{
					integer = font.Height;
					this.Properties.SetInteger(Control.PropFontHeight, integer);
					return integer;
				}
				int num = -1;
				if (this.ParentInternal != null && this.ParentInternal.CanAccessProperties)
				{
					num = this.ParentInternal.FontHeight;
				}
				if (num == -1)
				{
					num = this.Font.Height;
					this.Properties.SetInteger(Control.PropFontHeight, num);
				}
				return num;
			}
			set
			{
				this.Properties.SetInteger(Control.PropFontHeight, value);
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00030C30 File Offset: 0x0002EE30
		// (set) Token: 0x06000FF2 RID: 4082 RVA: 0x00030CB4 File Offset: 0x0002EEB4
		[SRCategory("CatAppearance")]
		[DispId(-513)]
		[SRDescription("ControlForeColorDescr")]
		public virtual Color ForeColor
		{
			get
			{
				Color color = this.Properties.GetColor(Control.PropForeColor);
				if (!color.IsEmpty)
				{
					return color;
				}
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null && parentInternal.CanAccessProperties)
				{
					return parentInternal.ForeColor;
				}
				Color color2 = Color.Empty;
				if (this.IsActiveX)
				{
					color2 = this.ActiveXAmbientForeColor;
				}
				if (color2.IsEmpty)
				{
					AmbientProperties ambientPropertiesService = this.AmbientPropertiesService;
					if (ambientPropertiesService != null)
					{
						color2 = ambientPropertiesService.ForeColor;
					}
				}
				if (!color2.IsEmpty)
				{
					return color2;
				}
				return Control.DefaultForeColor;
			}
			set
			{
				Color foreColor = this.ForeColor;
				if (!value.IsEmpty || this.Properties.ContainsObject(Control.PropForeColor))
				{
					this.Properties.SetColor(Control.PropForeColor, value);
				}
				if (!foreColor.Equals(this.ForeColor))
				{
					this.OnForeColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property value changes.</summary>
		// Token: 0x14000091 RID: 145
		// (add) Token: 0x06000FF3 RID: 4083 RVA: 0x00030D19 File Offset: 0x0002EF19
		// (remove) Token: 0x06000FF4 RID: 4084 RVA: 0x00030D2C File Offset: 0x0002EF2C
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnForeColorChangedDescr")]
		public event EventHandler ForeColorChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventForeColor, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventForeColor, value);
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00030D3F File Offset: 0x0002EF3F
		private Font GetParentFont()
		{
			if (this.ParentInternal != null && this.ParentInternal.CanAccessProperties)
			{
				return this.ParentInternal.Font;
			}
			return null;
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <param name="proposedSize">The custom-sized area for a control.</param>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x06000FF6 RID: 4086 RVA: 0x00030D64 File Offset: 0x0002EF64
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual Size GetPreferredSize(Size proposedSize)
		{
			Size size;
			if (this.GetState(6144))
			{
				size = CommonProperties.xGetPreferredSizeCache(this);
			}
			else
			{
				proposedSize = LayoutUtils.ConvertZeroToUnbounded(proposedSize);
				proposedSize = this.ApplySizeConstraints(proposedSize);
				if (this.GetState2(2048))
				{
					Size size2 = CommonProperties.xGetPreferredSizeCache(this);
					if (!size2.IsEmpty && proposedSize == LayoutUtils.MaxSize)
					{
						return size2;
					}
				}
				this.CacheTextInternal = true;
				try
				{
					size = this.GetPreferredSizeCore(proposedSize);
				}
				finally
				{
					this.CacheTextInternal = false;
				}
				size = this.ApplySizeConstraints(size);
				if (this.GetState2(2048) && proposedSize == LayoutUtils.MaxSize)
				{
					CommonProperties.xSetPreferredSizeCache(this, size);
				}
			}
			return size;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00030E18 File Offset: 0x0002F018
		internal virtual Size GetPreferredSizeCore(Size proposedSize)
		{
			return CommonProperties.GetSpecifiedBounds(this).Size;
		}

		/// <summary>Gets the window handle that the control is bound to.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that contains the window handle (<see langword="HWND" />) of the control.</returns>
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x00030E34 File Offset: 0x0002F034
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DispId(-515)]
		[SRDescription("ControlHandleDescr")]
		public IntPtr Handle
		{
			get
			{
				if (Control.checkForIllegalCrossThreadCalls && !Control.inCrossThreadSafeCall && this.InvokeRequired)
				{
					throw new InvalidOperationException(SR.GetString("IllegalCrossThreadCall", new object[] { this.Name }));
				}
				if (!this.IsHandleCreated)
				{
					this.CreateHandle();
				}
				return this.HandleInternal;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00030E8A File Offset: 0x0002F08A
		internal IntPtr HandleInternal
		{
			get
			{
				return this.window.Handle;
			}
		}

		/// <summary>Gets a value indicating whether the control contains one or more child controls.</summary>
		/// <returns>
		///   <see langword="true" /> if the control contains one or more child controls; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x00030E98 File Offset: 0x0002F098
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlHasChildrenDescr")]
		public bool HasChildren
		{
			get
			{
				Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
				return controlCollection != null && controlCollection.Count > 0;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool HasMenu
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the height of the control.</summary>
		/// <returns>The height of the control in pixels.</returns>
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x00030EC9 File Offset: 0x0002F0C9
		// (set) Token: 0x06000FFD RID: 4093 RVA: 0x00030ED1 File Offset: 0x0002F0D1
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlHeightDescr")]
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.SetBounds(this.x, this.y, this.width, value, BoundsSpecified.Height);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00030EF0 File Offset: 0x0002F0F0
		internal bool HostedInWin32DialogManager
		{
			get
			{
				if (!this.GetState(16777216))
				{
					Control topMostParent = this.TopMostParent;
					if (this != topMostParent)
					{
						this.SetState(33554432, topMostParent.HostedInWin32DialogManager);
					}
					else
					{
						IntPtr intPtr = UnsafeNativeMethods.GetParent(new HandleRef(this, this.Handle));
						IntPtr intPtr2 = intPtr;
						StringBuilder stringBuilder = new StringBuilder(32);
						this.SetState(33554432, false);
						while (intPtr != IntPtr.Zero)
						{
							int className = UnsafeNativeMethods.GetClassName(new HandleRef(null, intPtr2), null, 0);
							if (className > stringBuilder.Capacity)
							{
								stringBuilder.Capacity = className + 5;
							}
							UnsafeNativeMethods.GetClassName(new HandleRef(null, intPtr2), stringBuilder, stringBuilder.Capacity);
							if (stringBuilder.ToString() == "#32770")
							{
								this.SetState(33554432, true);
								break;
							}
							intPtr2 = intPtr;
							intPtr = UnsafeNativeMethods.GetParent(new HandleRef(null, intPtr));
						}
					}
					this.SetState(16777216, true);
				}
				return this.GetState(33554432);
			}
		}

		/// <summary>Gets a value indicating whether the control has a handle associated with it.</summary>
		/// <returns>
		///   <see langword="true" /> if a handle has been assigned to the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x00030FE3 File Offset: 0x0002F1E3
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlHandleCreatedDescr")]
		public bool IsHandleCreated
		{
			get
			{
				return this.window.Handle != IntPtr.Zero;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x00030FFA File Offset: 0x0002F1FA
		internal bool IsLayoutSuspended
		{
			get
			{
				return this.layoutSuspendCount > 0;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x00031008 File Offset: 0x0002F208
		internal bool IsWindowObscured
		{
			get
			{
				if (!this.IsHandleCreated || !this.Visible)
				{
					return false;
				}
				bool flag = false;
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				Control control = this.ParentInternal;
				if (control != null)
				{
					while (control.ParentInternal != null)
					{
						control = control.ParentInternal;
					}
				}
				UnsafeNativeMethods.GetWindowRect(new HandleRef(this, this.Handle), ref rect);
				Region region = new Region(Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom));
				try
				{
					IntPtr intPtr;
					if (control != null)
					{
						intPtr = control.Handle;
					}
					else
					{
						intPtr = this.Handle;
					}
					IntPtr intPtr2 = intPtr;
					IntPtr intPtr3;
					while ((intPtr3 = UnsafeNativeMethods.GetWindow(new HandleRef(null, intPtr2), 3)) != IntPtr.Zero)
					{
						UnsafeNativeMethods.GetWindowRect(new HandleRef(null, intPtr3), ref rect);
						Rectangle rectangle = Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
						if (SafeNativeMethods.IsWindowVisible(new HandleRef(null, intPtr3)))
						{
							region.Exclude(rectangle);
						}
						intPtr2 = intPtr3;
					}
					Graphics graphics = this.CreateGraphics();
					try
					{
						flag = region.IsEmpty(graphics);
					}
					finally
					{
						graphics.Dispose();
					}
				}
				finally
				{
					region.Dispose();
				}
				return flag;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x00031144 File Offset: 0x0002F344
		internal IntPtr InternalHandle
		{
			get
			{
				if (!this.IsHandleCreated)
				{
					return IntPtr.Zero;
				}
				return this.Handle;
			}
		}

		/// <summary>Gets a value indicating whether the caller must call an invoke method when making method calls to the control because the caller is on a different thread than the one the control was created on.</summary>
		/// <returns>
		///   <see langword="true" /> if the control's <see cref="P:System.Windows.Forms.Control.Handle" /> was created on a different thread than the calling thread (indicating that you must make calls to the control through an invoke method); otherwise, <see langword="false" />.</returns>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x0003115C File Offset: 0x0002F35C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlInvokeRequiredDescr")]
		public bool InvokeRequired
		{
			get
			{
				bool flag;
				using (new Control.MultithreadSafeCallScope())
				{
					HandleRef handleRef;
					if (this.IsHandleCreated)
					{
						handleRef = new HandleRef(this, this.Handle);
					}
					else
					{
						Control control = this.FindMarshalingControl();
						if (!control.IsHandleCreated)
						{
							return false;
						}
						handleRef = new HandleRef(control, control.Handle);
					}
					int num;
					int windowThreadProcessId = SafeNativeMethods.GetWindowThreadProcessId(handleRef, out num);
					int currentThreadId = SafeNativeMethods.GetCurrentThreadId();
					flag = windowThreadProcessId != currentThreadId;
				}
				return flag;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is visible to accessibility applications.</summary>
		/// <returns>
		///   <see langword="true" /> if the control is visible to accessibility applications; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x000311E8 File Offset: 0x0002F3E8
		// (set) Token: 0x06001005 RID: 4101 RVA: 0x000311F5 File Offset: 0x0002F3F5
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlIsAccessibleDescr")]
		public bool IsAccessible
		{
			get
			{
				return this.GetState(1048576);
			}
			set
			{
				this.SetState(1048576, value);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x00031203 File Offset: 0x0002F403
		internal bool IsAccessibilityObjectCreated
		{
			get
			{
				return this.Properties.GetObject(Control.PropAccessibility) is AccessibleObject;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x0003121D File Offset: 0x0002F41D
		internal bool IsInternalAccessibilityObjectCreated
		{
			get
			{
				return this.Properties.GetObject(Control.PropUnsafeAccessibility) is InternalAccessibleObject;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x00031237 File Offset: 0x0002F437
		internal bool IsActiveX
		{
			get
			{
				return this.GetState2(1024);
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool IsContainerControl
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x00031244 File Offset: 0x0002F444
		internal bool IsIEParent
		{
			get
			{
				return this.IsActiveX && this.ActiveXInstance.IsIE;
			}
		}

		/// <summary>Gets a value indicating whether the control is mirrored.</summary>
		/// <returns>
		///   <see langword="true" /> if the control is mirrored; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x0003125C File Offset: 0x0002F45C
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("IsMirroredDescr")]
		public bool IsMirrored
		{
			get
			{
				if (!this.IsHandleCreated)
				{
					CreateParams createParams = this.CreateParams;
					this.SetState(1073741824, (createParams.ExStyle & 4194304) != 0);
				}
				return this.GetState(1073741824);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool IsMnemonicsListenerAxSourced
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0003129D File Offset: 0x0002F49D
		private bool IsValidBackColor(Color c)
		{
			return c.IsEmpty || this.GetStyle(ControlStyles.SupportsTransparentBackColor) || c.A >= byte.MaxValue;
		}

		/// <summary>Gets or sets the distance, in pixels, between the left edge of the control and the left edge of its container's client area.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the left edge of the control and the left edge of its container's client area.</returns>
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x000312C6 File Offset: 0x0002F4C6
		// (set) Token: 0x0600100F RID: 4111 RVA: 0x000312CE File Offset: 0x0002F4CE
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlLeftDescr")]
		public int Left
		{
			get
			{
				return this.x;
			}
			set
			{
				this.SetBounds(value, this.y, this.width, this.height, BoundsSpecified.X);
			}
		}

		/// <summary>Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the control relative to the upper-left corner of its container.</returns>
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x000312EA File Offset: 0x0002F4EA
		// (set) Token: 0x06001011 RID: 4113 RVA: 0x000312FD File Offset: 0x0002F4FD
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("ControlLocationDescr")]
		public Point Location
		{
			get
			{
				return new Point(this.x, this.y);
			}
			set
			{
				this.SetBounds(value.X, value.Y, this.width, this.height, BoundsSpecified.Location);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Location" /> property value has changed.</summary>
		// Token: 0x14000092 RID: 146
		// (add) Token: 0x06001012 RID: 4114 RVA: 0x00031320 File Offset: 0x0002F520
		// (remove) Token: 0x06001013 RID: 4115 RVA: 0x00031333 File Offset: 0x0002F533
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnLocationChangedDescr")]
		public event EventHandler LocationChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventLocation, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventLocation, value);
			}
		}

		/// <summary>Gets or sets the space between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the space between controls.</returns>
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00019A7D File Offset: 0x00017C7D
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x00031346 File Offset: 0x0002F546
		[SRDescription("ControlMarginDescr")]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		public Padding Margin
		{
			get
			{
				return CommonProperties.GetMargin(this);
			}
			set
			{
				value = LayoutUtils.ClampNegativePaddingToZero(value);
				if (value != this.Margin)
				{
					CommonProperties.SetMargin(this, value);
					this.OnMarginChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the control's margin changes.</summary>
		// Token: 0x14000093 RID: 147
		// (add) Token: 0x06001016 RID: 4118 RVA: 0x00031370 File Offset: 0x0002F570
		// (remove) Token: 0x06001017 RID: 4119 RVA: 0x00031383 File Offset: 0x0002F583
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnMarginChangedDescr")]
		public event EventHandler MarginChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventMarginChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMarginChanged, value);
			}
		}

		/// <summary>Gets or sets the size that is the upper limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00031396 File Offset: 0x0002F596
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x000313A4 File Offset: 0x0002F5A4
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("ControlMaximumSizeDescr")]
		[AmbientValue(typeof(Size), "0, 0")]
		public virtual Size MaximumSize
		{
			get
			{
				return CommonProperties.GetMaximumSize(this, this.DefaultMaximumSize);
			}
			set
			{
				if (value == Size.Empty)
				{
					CommonProperties.ClearMaximumSize(this);
					return;
				}
				if (value != this.MaximumSize)
				{
					CommonProperties.SetMaximumSize(this, value);
				}
			}
		}

		/// <summary>Gets or sets the size that is the lower limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x000313CF File Offset: 0x0002F5CF
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x000313DD File Offset: 0x0002F5DD
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("ControlMinimumSizeDescr")]
		public virtual Size MinimumSize
		{
			get
			{
				return CommonProperties.GetMinimumSize(this, this.DefaultMinimumSize);
			}
			set
			{
				if (value != this.MinimumSize)
				{
					CommonProperties.SetMinimumSize(this, value);
				}
			}
		}

		/// <summary>Gets a value indicating which of the modifier keys (SHIFT, CTRL, and ALT) is in a pressed state.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.Keys" /> values. The default is <see cref="F:System.Windows.Forms.Keys.None" />.</returns>
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x000313F4 File Offset: 0x0002F5F4
		public static Keys ModifierKeys
		{
			get
			{
				Keys keys = Keys.None;
				if (UnsafeNativeMethods.GetKeyState(16) < 0)
				{
					keys |= Keys.Shift;
				}
				if (UnsafeNativeMethods.GetKeyState(17) < 0)
				{
					keys |= Keys.Control;
				}
				if (UnsafeNativeMethods.GetKeyState(18) < 0)
				{
					keys |= Keys.Alt;
				}
				return keys;
			}
		}

		/// <summary>Gets a value indicating which of the mouse buttons is in a pressed state.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.MouseButtons" /> enumeration values. The default is <see cref="F:System.Windows.Forms.MouseButtons.None" />.</returns>
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x0003143C File Offset: 0x0002F63C
		public static MouseButtons MouseButtons
		{
			get
			{
				MouseButtons mouseButtons = MouseButtons.None;
				if (UnsafeNativeMethods.GetKeyState(1) < 0)
				{
					mouseButtons |= MouseButtons.Left;
				}
				if (UnsafeNativeMethods.GetKeyState(2) < 0)
				{
					mouseButtons |= MouseButtons.Right;
				}
				if (UnsafeNativeMethods.GetKeyState(4) < 0)
				{
					mouseButtons |= MouseButtons.Middle;
				}
				if (UnsafeNativeMethods.GetKeyState(5) < 0)
				{
					mouseButtons |= MouseButtons.XButton1;
				}
				if (UnsafeNativeMethods.GetKeyState(6) < 0)
				{
					mouseButtons |= MouseButtons.XButton2;
				}
				return mouseButtons;
			}
		}

		/// <summary>Gets the position of the mouse cursor in screen coordinates.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that contains the coordinates of the mouse cursor relative to the upper-left corner of the screen.</returns>
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x000314A4 File Offset: 0x0002F6A4
		public static Point MousePosition
		{
			get
			{
				NativeMethods.POINT point = new NativeMethods.POINT();
				UnsafeNativeMethods.GetCursorPos(point);
				return new Point(point.x, point.y);
			}
		}

		/// <summary>Gets or sets the name of the control.</summary>
		/// <returns>The name of the control. The default is an empty string ("").</returns>
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x000314D0 File Offset: 0x0002F6D0
		// (set) Token: 0x06001020 RID: 4128 RVA: 0x00031519 File Offset: 0x0002F719
		[Browsable(false)]
		public string Name
		{
			get
			{
				string text = (string)this.Properties.GetObject(Control.PropName);
				if (string.IsNullOrEmpty(text))
				{
					if (this.Site != null)
					{
						text = this.Site.Name;
					}
					if (text == null)
					{
						text = "";
					}
				}
				return text;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Properties.SetObject(Control.PropName, null);
					return;
				}
				this.Properties.SetObject(Control.PropName, value);
			}
		}

		/// <summary>Gets or sets the parent container of the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the parent or container control of the control.</returns>
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x00031546 File Offset: 0x0002F746
		// (set) Token: 0x06001022 RID: 4130 RVA: 0x00031558 File Offset: 0x0002F758
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlParentDescr")]
		public Control Parent
		{
			get
			{
				IntSecurity.GetParent.Demand();
				return this.ParentInternal;
			}
			set
			{
				this.ParentInternal = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x00031561 File Offset: 0x0002F761
		// (set) Token: 0x06001024 RID: 4132 RVA: 0x00031569 File Offset: 0x0002F769
		internal virtual Control ParentInternal
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (this.parent != value)
				{
					if (value != null)
					{
						value.Controls.Add(this);
						return;
					}
					this.parent.Controls.Remove(this);
				}
			}
		}

		/// <summary>Gets the product name of the assembly containing the control.</summary>
		/// <returns>The product name of the assembly containing the control.</returns>
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x00031595 File Offset: 0x0002F795
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlProductNameDescr")]
		public string ProductName
		{
			get
			{
				return this.VersionInfo.ProductName;
			}
		}

		/// <summary>Gets the version of the assembly containing the control.</summary>
		/// <returns>The file version of the assembly containing the control.</returns>
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x000315A2 File Offset: 0x0002F7A2
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlProductVersionDescr")]
		public string ProductVersion
		{
			get
			{
				return this.VersionInfo.ProductVersion;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x000315AF File Offset: 0x0002F7AF
		internal PropertyStore Properties
		{
			get
			{
				return this.propertyStore;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x000315B7 File Offset: 0x0002F7B7
		internal Color RawBackColor
		{
			get
			{
				return this.Properties.GetColor(Control.PropBackColor);
			}
		}

		/// <summary>Gets a value indicating whether the control is currently re-creating its handle.</summary>
		/// <returns>
		///   <see langword="true" /> if the control is currently re-creating its handle; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x000315C9 File Offset: 0x0002F7C9
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlRecreatingHandleDescr")]
		public bool RecreatingHandle
		{
			get
			{
				return (this.state & 16) != 0;
			}
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void AddReflectChild()
		{
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void RemoveReflectChild()
		{
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x000315D7 File Offset: 0x0002F7D7
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x000315E0 File Offset: 0x0002F7E0
		private Control ReflectParent
		{
			get
			{
				return this.reflectParent;
			}
			set
			{
				if (value != null)
				{
					value.AddReflectChild();
				}
				Control control = this.ReflectParent;
				this.reflectParent = value;
				if (control != null)
				{
					control.RemoveReflectChild();
				}
			}
		}

		/// <summary>Gets or sets the window region associated with the control.</summary>
		/// <returns>The window <see cref="T:System.Drawing.Region" /> associated with the control.</returns>
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0003160D File Offset: 0x0002F80D
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x00031624 File Offset: 0x0002F824
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlRegionDescr")]
		public Region Region
		{
			get
			{
				return (Region)this.Properties.GetObject(Control.PropRegion);
			}
			set
			{
				if (this.GetState(524288))
				{
					IntSecurity.ChangeWindowRegionForTopLevel.Demand();
				}
				Region region = this.Region;
				if (region != value)
				{
					this.Properties.SetObject(Control.PropRegion, value);
					if (region != null)
					{
						region.Dispose();
					}
					if (this.IsHandleCreated)
					{
						IntPtr intPtr = IntPtr.Zero;
						try
						{
							if (value != null)
							{
								intPtr = this.GetHRgn(value);
							}
							if (this.IsActiveX)
							{
								intPtr = this.ActiveXMergeRegion(intPtr);
							}
							if (UnsafeNativeMethods.SetWindowRgn(new HandleRef(this, this.Handle), new HandleRef(this, intPtr), SafeNativeMethods.IsWindowVisible(new HandleRef(this, this.Handle))) != 0)
							{
								intPtr = IntPtr.Zero;
							}
						}
						finally
						{
							if (intPtr != IntPtr.Zero)
							{
								SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
							}
						}
					}
					this.OnRegionChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Region" /> property changes.</summary>
		// Token: 0x14000094 RID: 148
		// (add) Token: 0x06001030 RID: 4144 RVA: 0x00031704 File Offset: 0x0002F904
		// (remove) Token: 0x06001031 RID: 4145 RVA: 0x00031717 File Offset: 0x0002F917
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlRegionChangedDescr")]
		public event EventHandler RegionChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventRegionChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventRegionChanged, value);
			}
		}

		/// <summary>This property is now obsolete.</summary>
		/// <returns>
		///   <see langword="true" /> if the control is rendered from right to left; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x00012E4E File Offset: 0x0001104E
		[Obsolete("This property has been deprecated. Please use RightToLeft instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected internal bool RenderRightToLeft
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x0003172C File Offset: 0x0002F92C
		internal bool RenderTransparent
		{
			get
			{
				return this.GetStyle(ControlStyles.SupportsTransparentBackColor) && this.BackColor.A < byte.MaxValue;
			}
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0003175D File Offset: 0x0002F95D
		private bool RenderColorTransparent(Color c)
		{
			return this.GetStyle(ControlStyles.SupportsTransparentBackColor) && c.A < byte.MaxValue;
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool RenderTransparencyWithVisualStyles
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0003177C File Offset: 0x0002F97C
		// (set) Token: 0x06001037 RID: 4151 RVA: 0x00031794 File Offset: 0x0002F994
		internal BoundsSpecified RequiredScaling
		{
			get
			{
				if ((this.requiredScaling & 16) != 0)
				{
					return (BoundsSpecified)(this.requiredScaling & 15);
				}
				return BoundsSpecified.None;
			}
			set
			{
				byte b = this.requiredScaling & 16;
				this.requiredScaling = (byte)((value & BoundsSpecified.All) | (BoundsSpecified)b);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x000317B9 File Offset: 0x0002F9B9
		// (set) Token: 0x06001039 RID: 4153 RVA: 0x000317C8 File Offset: 0x0002F9C8
		internal bool RequiredScalingEnabled
		{
			get
			{
				return (this.requiredScaling & 16) > 0;
			}
			set
			{
				byte b = this.requiredScaling & 15;
				this.requiredScaling = b;
				if (value)
				{
					this.requiredScaling |= 16;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control redraws itself when resized.</summary>
		/// <returns>
		///   <see langword="true" /> if the control redraws itself when resized; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x000317FA File Offset: 0x0002F9FA
		// (set) Token: 0x0600103B RID: 4155 RVA: 0x00031804 File Offset: 0x0002FA04
		[SRDescription("ControlResizeRedrawDescr")]
		protected bool ResizeRedraw
		{
			get
			{
				return this.GetStyle(ControlStyles.ResizeRedraw);
			}
			set
			{
				this.SetStyle(ControlStyles.ResizeRedraw, value);
			}
		}

		/// <summary>Gets the distance, in pixels, between the right edge of the control and the left edge of its container's client area.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the right edge of the control and the left edge of its container's client area.</returns>
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x0003180F File Offset: 0x0002FA0F
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlRightDescr")]
		public int Right
		{
			get
			{
				return this.x + this.width;
			}
		}

		/// <summary>Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values. The default is <see cref="F:System.Windows.Forms.RightToLeft.Inherit" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.RightToLeft" /> values.</exception>
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x00031820 File Offset: 0x0002FA20
		// (set) Token: 0x0600103E RID: 4158 RVA: 0x00031864 File Offset: 0x0002FA64
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[AmbientValue(RightToLeft.Inherit)]
		[SRDescription("ControlRightToLeftDescr")]
		public virtual RightToLeft RightToLeft
		{
			get
			{
				bool flag;
				int num = this.Properties.GetInteger(Control.PropRightToLeft, out flag);
				if (!flag)
				{
					num = 2;
				}
				if (num == 2)
				{
					Control parentInternal = this.ParentInternal;
					if (parentInternal != null)
					{
						num = (int)parentInternal.RightToLeft;
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
				if (this.Properties.ContainsInteger(Control.PropRightToLeft) || value != RightToLeft.Inherit)
				{
					this.Properties.SetInteger(Control.PropRightToLeft, (int)value);
				}
				if (rightToLeft != this.RightToLeft)
				{
					using (new LayoutTransaction(this, this, PropertyNames.RightToLeft))
					{
						this.OnRightToLeftChanged(EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value changes.</summary>
		// Token: 0x14000095 RID: 149
		// (add) Token: 0x0600103F RID: 4159 RVA: 0x00031904 File Offset: 0x0002FB04
		// (remove) Token: 0x06001040 RID: 4160 RVA: 0x00031917 File Offset: 0x0002FB17
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftChangedDescr")]
		public event EventHandler RightToLeftChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventRightToLeft, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventRightToLeft, value);
			}
		}

		/// <summary>Gets a value that determines the scaling of child controls.</summary>
		/// <returns>
		///   <see langword="true" /> if child controls will be scaled when the <see cref="M:System.Windows.Forms.Control.Scale(System.Single)" /> method on this control is called; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x00012E4E File Offset: 0x0001104E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual bool ScaleChildren
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the site of the control.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.Windows.Forms.Control" />, if any.</returns>
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0003192A File Offset: 0x0002FB2A
		// (set) Token: 0x06001043 RID: 4163 RVA: 0x00031934 File Offset: 0x0002FB34
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				AmbientProperties ambientPropertiesService = this.AmbientPropertiesService;
				AmbientProperties ambientProperties = null;
				if (value != null)
				{
					ambientProperties = (AmbientProperties)value.GetService(typeof(AmbientProperties));
				}
				if (ambientPropertiesService != ambientProperties)
				{
					bool flag = !this.Properties.ContainsObject(Control.PropFont);
					bool flag2 = !this.Properties.ContainsObject(Control.PropBackColor);
					bool flag3 = !this.Properties.ContainsObject(Control.PropForeColor);
					bool flag4 = !this.Properties.ContainsObject(Control.PropCursor);
					Font font = null;
					Color color = Color.Empty;
					Color color2 = Color.Empty;
					Cursor cursor = null;
					if (flag)
					{
						font = this.Font;
					}
					if (flag2)
					{
						color = this.BackColor;
					}
					if (flag3)
					{
						color2 = this.ForeColor;
					}
					if (flag4)
					{
						cursor = this.Cursor;
					}
					this.Properties.SetObject(Control.PropAmbientPropertiesService, ambientProperties);
					base.Site = value;
					if (flag && !font.Equals(this.Font))
					{
						this.OnFontChanged(EventArgs.Empty);
					}
					if (flag3 && !color2.Equals(this.ForeColor))
					{
						this.OnForeColorChanged(EventArgs.Empty);
					}
					if (flag2 && !color.Equals(this.BackColor))
					{
						this.OnBackColorChanged(EventArgs.Empty);
					}
					if (flag4 && cursor.Equals(this.Cursor))
					{
						this.OnCursorChanged(EventArgs.Empty);
						return;
					}
				}
				else
				{
					base.Site = value;
				}
			}
		}

		/// <summary>Gets or sets the height and width of the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> that represents the height and width of the control in pixels.</returns>
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x00031AAA File Offset: 0x0002FCAA
		// (set) Token: 0x06001045 RID: 4165 RVA: 0x00031ABD File Offset: 0x0002FCBD
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("ControlSizeDescr")]
		public Size Size
		{
			get
			{
				return new Size(this.width, this.height);
			}
			set
			{
				this.SetBounds(this.x, this.y, value.Width, value.Height, BoundsSpecified.Size);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Size" /> property value changes.</summary>
		// Token: 0x14000096 RID: 150
		// (add) Token: 0x06001046 RID: 4166 RVA: 0x00031AE1 File Offset: 0x0002FCE1
		// (remove) Token: 0x06001047 RID: 4167 RVA: 0x00031AF4 File Offset: 0x0002FCF4
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnSizeChangedDescr")]
		public event EventHandler SizeChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventSize, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventSize, value);
			}
		}

		/// <summary>Gets or sets the tab order of the control within its container.</summary>
		/// <returns>The index value of the control within the set of controls within its container. The controls in the container are included in the tab order.</returns>
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x00031B07 File Offset: 0x0002FD07
		// (set) Token: 0x06001049 RID: 4169 RVA: 0x00031B1C File Offset: 0x0002FD1C
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[MergableProperty(false)]
		[SRDescription("ControlTabIndexDescr")]
		public int TabIndex
		{
			get
			{
				if (this.tabIndex != -1)
				{
					return this.tabIndex;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("TabIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"TabIndex",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.tabIndex != value)
				{
					this.tabIndex = value;
					this.OnTabIndexChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.TabIndex" /> property value changes.</summary>
		// Token: 0x14000097 RID: 151
		// (add) Token: 0x0600104A RID: 4170 RVA: 0x00031B8B File Offset: 0x0002FD8B
		// (remove) Token: 0x0600104B RID: 4171 RVA: 0x00031B9E File Offset: 0x0002FD9E
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnTabIndexChangedDescr")]
		public event EventHandler TabIndexChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventTabIndex, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventTabIndex, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can give the focus to the control using the TAB key; otherwise, <see langword="false" />. The default is <see langword="true" />.  
		///
		///  This property will always return <see langword="true" /> for an instance of the <see cref="T:System.Windows.Forms.Form" /> class.</returns>
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x00031BB1 File Offset: 0x0002FDB1
		// (set) Token: 0x0600104D RID: 4173 RVA: 0x00031BB9 File Offset: 0x0002FDB9
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[DispId(-516)]
		[SRDescription("ControlTabStopDescr")]
		public bool TabStop
		{
			get
			{
				return this.TabStopInternal;
			}
			set
			{
				if (this.TabStop != value)
				{
					this.TabStopInternal = value;
					if (this.IsHandleCreated)
					{
						this.SetWindowStyle(65536, value);
					}
					this.OnTabStopChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x00031BEA File Offset: 0x0002FDEA
		// (set) Token: 0x0600104F RID: 4175 RVA: 0x00031BF7 File Offset: 0x0002FDF7
		internal bool TabStopInternal
		{
			get
			{
				return (this.state & 8) != 0;
			}
			set
			{
				if (this.TabStopInternal != value)
				{
					this.SetState(8, value);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.TabStop" /> property value changes.</summary>
		// Token: 0x14000098 RID: 152
		// (add) Token: 0x06001050 RID: 4176 RVA: 0x00031C0A File Offset: 0x0002FE0A
		// (remove) Token: 0x06001051 RID: 4177 RVA: 0x00031C1D File Offset: 0x0002FE1D
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnTabStopChangedDescr")]
		public event EventHandler TabStopChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventTabStop, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventTabStop, value);
			}
		}

		/// <summary>Gets or sets the object that contains data about the control.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the control. The default is <see langword="null" />.</returns>
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x00031C30 File Offset: 0x0002FE30
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x00031C42 File Offset: 0x0002FE42
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.Properties.GetObject(Control.PropUserData);
			}
			set
			{
				this.Properties.SetObject(Control.PropUserData, value);
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x00031C55 File Offset: 0x0002FE55
		// (set) Token: 0x06001055 RID: 4181 RVA: 0x00031C7C File Offset: 0x0002FE7C
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[Bindable(true)]
		[DispId(-517)]
		[SRDescription("ControlTextDescr")]
		public virtual string Text
		{
			get
			{
				if (!this.CacheTextInternal)
				{
					return this.WindowText;
				}
				if (this.text != null)
				{
					return this.text;
				}
				return "";
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value == this.Text)
				{
					return;
				}
				if (this.CacheTextInternal)
				{
					this.text = value;
				}
				this.WindowText = value;
				this.OnTextChanged(EventArgs.Empty);
				if (this.IsMnemonicsListenerAxSourced)
				{
					for (Control control = this; control != null; control = control.ParentInternal)
					{
						Control.ActiveXImpl activeXImpl = (Control.ActiveXImpl)control.Properties.GetObject(Control.PropActiveXImpl);
						if (activeXImpl != null)
						{
							activeXImpl.UpdateAccelTable();
							return;
						}
					}
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Text" /> property value changes.</summary>
		// Token: 0x14000099 RID: 153
		// (add) Token: 0x06001056 RID: 4182 RVA: 0x00031CF9 File Offset: 0x0002FEF9
		// (remove) Token: 0x06001057 RID: 4183 RVA: 0x00031D0C File Offset: 0x0002FF0C
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnTextChangedDescr")]
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventText, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventText, value);
			}
		}

		/// <summary>Gets or sets the distance, in pixels, between the top edge of the control and the top edge of its container's client area.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the bottom edge of the control and the top edge of its container's client area.</returns>
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x00031D1F File Offset: 0x0002FF1F
		// (set) Token: 0x06001059 RID: 4185 RVA: 0x00031D27 File Offset: 0x0002FF27
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlTopDescr")]
		public int Top
		{
			get
			{
				return this.y;
			}
			set
			{
				this.SetBounds(this.x, value, this.width, this.height, BoundsSpecified.Y);
			}
		}

		/// <summary>Gets the parent control that is not parented by another Windows Forms control. Typically, this is the outermost <see cref="T:System.Windows.Forms.Form" /> that the control is contained in.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that represents the top-level control that contains the current control.</returns>
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x00031D43 File Offset: 0x0002FF43
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlTopLevelControlDescr")]
		public Control TopLevelControl
		{
			get
			{
				IntSecurity.GetParent.Demand();
				return this.TopLevelControlInternal;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x00031D58 File Offset: 0x0002FF58
		internal Control TopLevelControlInternal
		{
			get
			{
				Control control = this;
				while (control != null && !control.GetTopLevel())
				{
					control = control.ParentInternal;
				}
				return control;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00031D7C File Offset: 0x0002FF7C
		internal Control TopMostParent
		{
			get
			{
				Control control = this;
				while (control.ParentInternal != null)
				{
					control = control.ParentInternal;
				}
				return control;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x00031D9D File Offset: 0x0002FF9D
		private BufferedGraphicsContext BufferContext
		{
			get
			{
				return BufferedGraphicsManager.Current;
			}
		}

		/// <summary>Gets a value indicating whether the user interface is in the appropriate state to show or hide keyboard accelerators.</summary>
		/// <returns>
		///   <see langword="true" /> if the keyboard accelerators are visible; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00031DA4 File Offset: 0x0002FFA4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected internal virtual bool ShowKeyboardCues
		{
			get
			{
				if (!this.IsHandleCreated || base.DesignMode)
				{
					return true;
				}
				if ((this.uiCuesState & 240) == 0)
				{
					if (SystemInformation.MenuAccessKeysUnderlined)
					{
						this.uiCuesState |= 32;
					}
					else
					{
						int num = (2 | (AccessibilityImprovements.Level1 ? 0 : 1)) << 16;
						this.uiCuesState |= 16;
						UnsafeNativeMethods.SendMessage(new HandleRef(this.TopMostParent, this.TopMostParent.Handle), 295, (IntPtr)(num | 1), IntPtr.Zero);
					}
				}
				return (this.uiCuesState & 240) == 32;
			}
		}

		/// <summary>Gets a value indicating whether the control should display focus rectangles.</summary>
		/// <returns>
		///   <see langword="true" /> if the control should display focus rectangles; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x00031E48 File Offset: 0x00030048
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected internal virtual bool ShowFocusCues
		{
			get
			{
				if (!this.IsHandleCreated)
				{
					return true;
				}
				if ((this.uiCuesState & 15) == 0)
				{
					if (SystemInformation.MenuAccessKeysUnderlined)
					{
						this.uiCuesState |= 2;
					}
					else
					{
						this.uiCuesState |= 1;
						int num = 196608;
						UnsafeNativeMethods.SendMessage(new HandleRef(this.TopMostParent, this.TopMostParent.Handle), 295, (IntPtr)(num | 1), IntPtr.Zero);
					}
				}
				return (this.uiCuesState & 15) == 2;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x00031ECF File Offset: 0x000300CF
		internal virtual int ShowParams
		{
			get
			{
				return 5;
			}
		}

		/// <summary>Gets or sets a value indicating whether to use the wait cursor for the current control and all child controls.</summary>
		/// <returns>
		///   <see langword="true" /> to use the wait cursor for the current control and all child controls; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00031ED2 File Offset: 0x000300D2
		// (set) Token: 0x06001062 RID: 4194 RVA: 0x00031EE0 File Offset: 0x000300E0
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ControlUseWaitCursorDescr")]
		public bool UseWaitCursor
		{
			get
			{
				return this.GetState(1024);
			}
			set
			{
				if (this.GetState(1024) != value)
				{
					this.SetState(1024, value);
					Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
					if (controlCollection != null)
					{
						for (int i = 0; i < controlCollection.Count; i++)
						{
							controlCollection[i].UseWaitCursor = value;
						}
					}
				}
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x00031F40 File Offset: 0x00030140
		// (set) Token: 0x06001064 RID: 4196 RVA: 0x00031F80 File Offset: 0x00030180
		internal bool UseCompatibleTextRenderingInt
		{
			get
			{
				if (this.Properties.ContainsInteger(Control.PropUseCompatibleTextRendering))
				{
					bool flag;
					int integer = this.Properties.GetInteger(Control.PropUseCompatibleTextRendering, out flag);
					if (flag)
					{
						return integer == 1;
					}
				}
				return Control.UseCompatibleTextRenderingDefault;
			}
			set
			{
				if (this.SupportsUseCompatibleTextRendering && this.UseCompatibleTextRenderingInt != value)
				{
					this.Properties.SetInteger(Control.PropUseCompatibleTextRendering, value ? 1 : 0);
					LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.UseCompatibleTextRendering);
					this.Invalidate();
				}
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool SupportsUseCompatibleTextRendering
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x00031FD4 File Offset: 0x000301D4
		private Control.ControlVersionInfo VersionInfo
		{
			get
			{
				Control.ControlVersionInfo controlVersionInfo = (Control.ControlVersionInfo)this.Properties.GetObject(Control.PropControlVersionInfo);
				if (controlVersionInfo == null)
				{
					controlVersionInfo = new Control.ControlVersionInfo(this);
					this.Properties.SetObject(Control.PropControlVersionInfo, controlVersionInfo);
				}
				return controlVersionInfo;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control and all its child controls are displayed.</summary>
		/// <returns>
		///   <see langword="true" /> if the control and all its child controls are displayed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00032013 File Offset: 0x00030213
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x0003201B File Offset: 0x0003021B
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("ControlVisibleDescr")]
		public bool Visible
		{
			get
			{
				return this.GetVisibleCore();
			}
			set
			{
				this.SetVisibleCore(value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Visible" /> property value changes.</summary>
		// Token: 0x1400009A RID: 154
		// (add) Token: 0x06001069 RID: 4201 RVA: 0x00032024 File Offset: 0x00030224
		// (remove) Token: 0x0600106A RID: 4202 RVA: 0x00032037 File Offset: 0x00030237
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnVisibleChangedDescr")]
		public event EventHandler VisibleChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventVisible, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventVisible, value);
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0003204C File Offset: 0x0003024C
		private void WaitForWaitHandle(WaitHandle waitHandle)
		{
			int createThreadId = this.CreateThreadId;
			Application.ThreadContext threadContext = Application.ThreadContext.FromId(createThreadId);
			if (threadContext == null)
			{
				return;
			}
			IntPtr handle = threadContext.GetHandle();
			bool flag = false;
			uint num = 0U;
			while (!flag)
			{
				bool exitCodeThread = UnsafeNativeMethods.GetExitCodeThread(handle, out num);
				if ((exitCodeThread && num != 259U) || (!exitCodeThread && Marshal.GetLastWin32Error() == 6) || AppDomain.CurrentDomain.IsFinalizingForUnload())
				{
					if (!waitHandle.WaitOne(1, false))
					{
						throw new InvalidAsynchronousStateException(SR.GetString("ThreadNoLongerValid"));
					}
					break;
				}
				else
				{
					if (this.IsDisposed && this.threadCallbackList != null && this.threadCallbackList.Count > 0)
					{
						Queue queue = this.threadCallbackList;
						lock (queue)
						{
							Exception ex = new ObjectDisposedException(base.GetType().Name);
							while (this.threadCallbackList.Count > 0)
							{
								Control.ThreadMethodEntry threadMethodEntry = (Control.ThreadMethodEntry)this.threadCallbackList.Dequeue();
								threadMethodEntry.exception = ex;
								threadMethodEntry.Complete();
							}
						}
					}
					flag = waitHandle.WaitOne(1000, false);
				}
			}
		}

		/// <summary>Gets or sets the width of the control.</summary>
		/// <returns>The width of the control in pixels.</returns>
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x00032174 File Offset: 0x00030374
		// (set) Token: 0x0600106D RID: 4205 RVA: 0x0003217C File Offset: 0x0003037C
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlWidthDescr")]
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.SetBounds(this.x, this.y, value, this.height, BoundsSpecified.Width);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x00032198 File Offset: 0x00030398
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x000321B3 File Offset: 0x000303B3
		private int WindowExStyle
		{
			get
			{
				return (int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, this.Handle), -20);
			}
			set
			{
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, this.Handle), -20, new HandleRef(null, (IntPtr)value));
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x000321D5 File Offset: 0x000303D5
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x000321F0 File Offset: 0x000303F0
		internal int WindowStyle
		{
			get
			{
				return (int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, this.Handle), -16);
			}
			set
			{
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, this.Handle), -16, new HandleRef(null, (IntPtr)value));
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The NativeWindow contained within the control.</returns>
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00032212 File Offset: 0x00030412
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x0003221F File Offset: 0x0003041F
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlWindowTargetDescr")]
		public IWindowTarget WindowTarget
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.window.WindowTarget;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				this.window.WindowTarget = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x00032230 File Offset: 0x00030430
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x000322D0 File Offset: 0x000304D0
		internal virtual string WindowText
		{
			get
			{
				if (this.IsHandleCreated)
				{
					string text;
					using (new Control.MultithreadSafeCallScope())
					{
						int num = SafeNativeMethods.GetWindowTextLength(new HandleRef(this.window, this.Handle));
						if (SystemInformation.DbcsEnabled)
						{
							num = num * 2 + 1;
						}
						StringBuilder stringBuilder = new StringBuilder(num + 1);
						UnsafeNativeMethods.GetWindowText(new HandleRef(this.window, this.Handle), stringBuilder, stringBuilder.Capacity);
						text = stringBuilder.ToString();
					}
					return text;
				}
				if (this.text == null)
				{
					return "";
				}
				return this.text;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (!this.WindowText.Equals(value))
				{
					if (this.IsHandleCreated)
					{
						UnsafeNativeMethods.SetWindowText(new HandleRef(this.window, this.Handle), value);
						return;
					}
					if (value.Length == 0)
					{
						this.text = null;
						return;
					}
					this.text = value;
				}
			}
		}

		/// <summary>Occurs when the control is clicked.</summary>
		// Token: 0x1400009B RID: 155
		// (add) Token: 0x06001076 RID: 4214 RVA: 0x0003232D File Offset: 0x0003052D
		// (remove) Token: 0x06001077 RID: 4215 RVA: 0x00032340 File Offset: 0x00030540
		[SRCategory("CatAction")]
		[SRDescription("ControlOnClickDescr")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(Control.EventClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventClick, value);
			}
		}

		/// <summary>Occurs when a new control is added to the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
		// Token: 0x1400009C RID: 156
		// (add) Token: 0x06001078 RID: 4216 RVA: 0x00032353 File Offset: 0x00030553
		// (remove) Token: 0x06001079 RID: 4217 RVA: 0x00032366 File Offset: 0x00030566
		[SRCategory("CatBehavior")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ControlOnControlAddedDescr")]
		public event ControlEventHandler ControlAdded
		{
			add
			{
				base.Events.AddHandler(Control.EventControlAdded, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventControlAdded, value);
			}
		}

		/// <summary>Occurs when a control is removed from the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
		// Token: 0x1400009D RID: 157
		// (add) Token: 0x0600107A RID: 4218 RVA: 0x00032379 File Offset: 0x00030579
		// (remove) Token: 0x0600107B RID: 4219 RVA: 0x0003238C File Offset: 0x0003058C
		[SRCategory("CatBehavior")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ControlOnControlRemovedDescr")]
		public event ControlEventHandler ControlRemoved
		{
			add
			{
				base.Events.AddHandler(Control.EventControlRemoved, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventControlRemoved, value);
			}
		}

		/// <summary>Occurs when a drag-and-drop operation is completed.</summary>
		// Token: 0x1400009E RID: 158
		// (add) Token: 0x0600107C RID: 4220 RVA: 0x0003239F File Offset: 0x0003059F
		// (remove) Token: 0x0600107D RID: 4221 RVA: 0x000323B2 File Offset: 0x000305B2
		[SRCategory("CatDragDrop")]
		[SRDescription("ControlOnDragDropDescr")]
		public event DragEventHandler DragDrop
		{
			add
			{
				base.Events.AddHandler(Control.EventDragDrop, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDragDrop, value);
			}
		}

		/// <summary>Occurs when an object is dragged into the control's bounds.</summary>
		// Token: 0x1400009F RID: 159
		// (add) Token: 0x0600107E RID: 4222 RVA: 0x000323C5 File Offset: 0x000305C5
		// (remove) Token: 0x0600107F RID: 4223 RVA: 0x000323D8 File Offset: 0x000305D8
		[SRCategory("CatDragDrop")]
		[SRDescription("ControlOnDragEnterDescr")]
		public event DragEventHandler DragEnter
		{
			add
			{
				base.Events.AddHandler(Control.EventDragEnter, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDragEnter, value);
			}
		}

		/// <summary>Occurs when an object is dragged over the control's bounds.</summary>
		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x06001080 RID: 4224 RVA: 0x000323EB File Offset: 0x000305EB
		// (remove) Token: 0x06001081 RID: 4225 RVA: 0x000323FE File Offset: 0x000305FE
		[SRCategory("CatDragDrop")]
		[SRDescription("ControlOnDragOverDescr")]
		public event DragEventHandler DragOver
		{
			add
			{
				base.Events.AddHandler(Control.EventDragOver, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDragOver, value);
			}
		}

		/// <summary>Occurs when an object is dragged out of the control's bounds.</summary>
		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x06001082 RID: 4226 RVA: 0x00032411 File Offset: 0x00030611
		// (remove) Token: 0x06001083 RID: 4227 RVA: 0x00032424 File Offset: 0x00030624
		[SRCategory("CatDragDrop")]
		[SRDescription("ControlOnDragLeaveDescr")]
		public event EventHandler DragLeave
		{
			add
			{
				base.Events.AddHandler(Control.EventDragLeave, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDragLeave, value);
			}
		}

		/// <summary>Occurs during a drag operation.</summary>
		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x06001084 RID: 4228 RVA: 0x00032437 File Offset: 0x00030637
		// (remove) Token: 0x06001085 RID: 4229 RVA: 0x0003244A File Offset: 0x0003064A
		[SRCategory("CatDragDrop")]
		[SRDescription("ControlOnGiveFeedbackDescr")]
		public event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				base.Events.AddHandler(Control.EventGiveFeedback, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventGiveFeedback, value);
			}
		}

		/// <summary>Occurs when a handle is created for the control.</summary>
		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x06001086 RID: 4230 RVA: 0x0003245D File Offset: 0x0003065D
		// (remove) Token: 0x06001087 RID: 4231 RVA: 0x00032470 File Offset: 0x00030670
		[SRCategory("CatPrivate")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ControlOnCreateHandleDescr")]
		public event EventHandler HandleCreated
		{
			add
			{
				base.Events.AddHandler(Control.EventHandleCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventHandleCreated, value);
			}
		}

		/// <summary>Occurs when the control's handle is in the process of being destroyed.</summary>
		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x06001088 RID: 4232 RVA: 0x00032483 File Offset: 0x00030683
		// (remove) Token: 0x06001089 RID: 4233 RVA: 0x00032496 File Offset: 0x00030696
		[SRCategory("CatPrivate")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ControlOnDestroyHandleDescr")]
		public event EventHandler HandleDestroyed
		{
			add
			{
				base.Events.AddHandler(Control.EventHandleDestroyed, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventHandleDestroyed, value);
			}
		}

		/// <summary>Occurs when the user requests help for a control.</summary>
		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x0600108A RID: 4234 RVA: 0x000324A9 File Offset: 0x000306A9
		// (remove) Token: 0x0600108B RID: 4235 RVA: 0x000324BC File Offset: 0x000306BC
		[SRCategory("CatBehavior")]
		[SRDescription("ControlOnHelpDescr")]
		public event HelpEventHandler HelpRequested
		{
			add
			{
				base.Events.AddHandler(Control.EventHelpRequested, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventHelpRequested, value);
			}
		}

		/// <summary>Occurs when a control's display requires redrawing.</summary>
		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x0600108C RID: 4236 RVA: 0x000324CF File Offset: 0x000306CF
		// (remove) Token: 0x0600108D RID: 4237 RVA: 0x000324E2 File Offset: 0x000306E2
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SRDescription("ControlOnInvalidateDescr")]
		public event InvalidateEventHandler Invalidated
		{
			add
			{
				base.Events.AddHandler(Control.EventInvalidated, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventInvalidated, value);
			}
		}

		/// <summary>Gets the size of a rectangular area into which the control can fit.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> containing the height and width, in pixels.</returns>
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x000324F5 File Offset: 0x000306F5
		[Browsable(false)]
		public Size PreferredSize
		{
			get
			{
				return this.GetPreferredSize(Size.Empty);
			}
		}

		/// <summary>Gets or sets padding within the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the control's internal spacing characteristics.</returns>
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00032502 File Offset: 0x00030702
		// (set) Token: 0x06001090 RID: 4240 RVA: 0x00032510 File Offset: 0x00030710
		[SRDescription("ControlPaddingDescr")]
		[SRCategory("CatLayout")]
		[Localizable(true)]
		public Padding Padding
		{
			get
			{
				return CommonProperties.GetPadding(this, this.DefaultPadding);
			}
			set
			{
				if (value != this.Padding)
				{
					CommonProperties.SetPadding(this, value);
					this.SetState(8388608, true);
					using (new LayoutTransaction(this.ParentInternal, this, PropertyNames.Padding))
					{
						this.OnPaddingChanged(EventArgs.Empty);
					}
					if (this.GetState(8388608))
					{
						LayoutTransaction.DoLayout(this, this, PropertyNames.Padding);
					}
				}
			}
		}

		/// <summary>Occurs when the control's padding changes.</summary>
		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x06001091 RID: 4241 RVA: 0x00032590 File Offset: 0x00030790
		// (remove) Token: 0x06001092 RID: 4242 RVA: 0x000325A3 File Offset: 0x000307A3
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnPaddingChangedDescr")]
		public event EventHandler PaddingChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventPaddingChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventPaddingChanged, value);
			}
		}

		/// <summary>Occurs when the control is redrawn.</summary>
		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x06001093 RID: 4243 RVA: 0x000325B6 File Offset: 0x000307B6
		// (remove) Token: 0x06001094 RID: 4244 RVA: 0x000325C9 File Offset: 0x000307C9
		[SRCategory("CatAppearance")]
		[SRDescription("ControlOnPaintDescr")]
		public event PaintEventHandler Paint
		{
			add
			{
				base.Events.AddHandler(Control.EventPaint, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventPaint, value);
			}
		}

		/// <summary>Occurs during a drag-and-drop operation and enables the drag source to determine whether the drag-and-drop operation should be canceled.</summary>
		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x06001095 RID: 4245 RVA: 0x000325DC File Offset: 0x000307DC
		// (remove) Token: 0x06001096 RID: 4246 RVA: 0x000325EF File Offset: 0x000307EF
		[SRCategory("CatDragDrop")]
		[SRDescription("ControlOnQueryContinueDragDescr")]
		public event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				base.Events.AddHandler(Control.EventQueryContinueDrag, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventQueryContinueDrag, value);
			}
		}

		/// <summary>Occurs when <see cref="T:System.Windows.Forms.AccessibleObject" /> is providing help to accessibility applications.</summary>
		// Token: 0x140000AA RID: 170
		// (add) Token: 0x06001097 RID: 4247 RVA: 0x00032602 File Offset: 0x00030802
		// (remove) Token: 0x06001098 RID: 4248 RVA: 0x00032615 File Offset: 0x00030815
		[SRCategory("CatBehavior")]
		[SRDescription("ControlOnQueryAccessibilityHelpDescr")]
		public event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
		{
			add
			{
				base.Events.AddHandler(Control.EventQueryAccessibilityHelp, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventQueryAccessibilityHelp, value);
			}
		}

		/// <summary>Occurs when the control is double-clicked.</summary>
		// Token: 0x140000AB RID: 171
		// (add) Token: 0x06001099 RID: 4249 RVA: 0x00032628 File Offset: 0x00030828
		// (remove) Token: 0x0600109A RID: 4250 RVA: 0x0003263B File Offset: 0x0003083B
		[SRCategory("CatAction")]
		[SRDescription("ControlOnDoubleClickDescr")]
		public event EventHandler DoubleClick
		{
			add
			{
				base.Events.AddHandler(Control.EventDoubleClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDoubleClick, value);
			}
		}

		/// <summary>Occurs when the control is entered.</summary>
		// Token: 0x140000AC RID: 172
		// (add) Token: 0x0600109B RID: 4251 RVA: 0x0003264E File Offset: 0x0003084E
		// (remove) Token: 0x0600109C RID: 4252 RVA: 0x00032661 File Offset: 0x00030861
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnEnterDescr")]
		public event EventHandler Enter
		{
			add
			{
				base.Events.AddHandler(Control.EventEnter, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventEnter, value);
			}
		}

		/// <summary>Occurs when the control receives focus.</summary>
		// Token: 0x140000AD RID: 173
		// (add) Token: 0x0600109D RID: 4253 RVA: 0x00032674 File Offset: 0x00030874
		// (remove) Token: 0x0600109E RID: 4254 RVA: 0x00032687 File Offset: 0x00030887
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnGotFocusDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler GotFocus
		{
			add
			{
				base.Events.AddHandler(Control.EventGotFocus, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventGotFocus, value);
			}
		}

		/// <summary>Occurs when a key is pressed while the control has focus.</summary>
		// Token: 0x140000AE RID: 174
		// (add) Token: 0x0600109F RID: 4255 RVA: 0x0003269A File Offset: 0x0003089A
		// (remove) Token: 0x060010A0 RID: 4256 RVA: 0x000326AD File Offset: 0x000308AD
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyDownDescr")]
		public event KeyEventHandler KeyDown
		{
			add
			{
				base.Events.AddHandler(Control.EventKeyDown, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventKeyDown, value);
			}
		}

		/// <summary>Occurs when a character. space or backspace key is pressed while the control has focus.</summary>
		// Token: 0x140000AF RID: 175
		// (add) Token: 0x060010A1 RID: 4257 RVA: 0x000326C0 File Offset: 0x000308C0
		// (remove) Token: 0x060010A2 RID: 4258 RVA: 0x000326D3 File Offset: 0x000308D3
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyPressDescr")]
		public event KeyPressEventHandler KeyPress
		{
			add
			{
				base.Events.AddHandler(Control.EventKeyPress, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventKeyPress, value);
			}
		}

		/// <summary>Occurs when a key is released while the control has focus.</summary>
		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x060010A3 RID: 4259 RVA: 0x000326E6 File Offset: 0x000308E6
		// (remove) Token: 0x060010A4 RID: 4260 RVA: 0x000326F9 File Offset: 0x000308F9
		[SRCategory("CatKey")]
		[SRDescription("ControlOnKeyUpDescr")]
		public event KeyEventHandler KeyUp
		{
			add
			{
				base.Events.AddHandler(Control.EventKeyUp, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventKeyUp, value);
			}
		}

		/// <summary>Occurs when a control should reposition its child controls.</summary>
		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x060010A5 RID: 4261 RVA: 0x0003270C File Offset: 0x0003090C
		// (remove) Token: 0x060010A6 RID: 4262 RVA: 0x0003271F File Offset: 0x0003091F
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnLayoutDescr")]
		public event LayoutEventHandler Layout
		{
			add
			{
				base.Events.AddHandler(Control.EventLayout, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventLayout, value);
			}
		}

		/// <summary>Occurs when the input focus leaves the control.</summary>
		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x060010A7 RID: 4263 RVA: 0x00032732 File Offset: 0x00030932
		// (remove) Token: 0x060010A8 RID: 4264 RVA: 0x00032745 File Offset: 0x00030945
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnLeaveDescr")]
		public event EventHandler Leave
		{
			add
			{
				base.Events.AddHandler(Control.EventLeave, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventLeave, value);
			}
		}

		/// <summary>Occurs when the control loses focus.</summary>
		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x060010A9 RID: 4265 RVA: 0x00032758 File Offset: 0x00030958
		// (remove) Token: 0x060010AA RID: 4266 RVA: 0x0003276B File Offset: 0x0003096B
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnLostFocusDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler LostFocus
		{
			add
			{
				base.Events.AddHandler(Control.EventLostFocus, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventLostFocus, value);
			}
		}

		/// <summary>Occurs when the control is clicked by the mouse.</summary>
		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x060010AB RID: 4267 RVA: 0x0003277E File Offset: 0x0003097E
		// (remove) Token: 0x060010AC RID: 4268 RVA: 0x00032791 File Offset: 0x00030991
		[SRCategory("CatAction")]
		[SRDescription("ControlOnMouseClickDescr")]
		public event MouseEventHandler MouseClick
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseClick, value);
			}
		}

		/// <summary>Occurs when the control is double clicked by the mouse.</summary>
		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x060010AD RID: 4269 RVA: 0x000327A4 File Offset: 0x000309A4
		// (remove) Token: 0x060010AE RID: 4270 RVA: 0x000327B7 File Offset: 0x000309B7
		[SRCategory("CatAction")]
		[SRDescription("ControlOnMouseDoubleClickDescr")]
		public event MouseEventHandler MouseDoubleClick
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseDoubleClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseDoubleClick, value);
			}
		}

		/// <summary>Occurs when the control loses mouse capture.</summary>
		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x060010AF RID: 4271 RVA: 0x000327CA File Offset: 0x000309CA
		// (remove) Token: 0x060010B0 RID: 4272 RVA: 0x000327DD File Offset: 0x000309DD
		[SRCategory("CatAction")]
		[SRDescription("ControlOnMouseCaptureChangedDescr")]
		public event EventHandler MouseCaptureChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseCaptureChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseCaptureChanged, value);
			}
		}

		/// <summary>Occurs when the mouse pointer is over the control and a mouse button is pressed.</summary>
		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x060010B1 RID: 4273 RVA: 0x000327F0 File Offset: 0x000309F0
		// (remove) Token: 0x060010B2 RID: 4274 RVA: 0x00032803 File Offset: 0x00030A03
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseDownDescr")]
		public event MouseEventHandler MouseDown
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseDown, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseDown, value);
			}
		}

		/// <summary>Occurs when the mouse pointer enters the control.</summary>
		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x060010B3 RID: 4275 RVA: 0x00032816 File Offset: 0x00030A16
		// (remove) Token: 0x060010B4 RID: 4276 RVA: 0x00032829 File Offset: 0x00030A29
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseEnterDescr")]
		public event EventHandler MouseEnter
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseEnter, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseEnter, value);
			}
		}

		/// <summary>Occurs when the mouse pointer leaves the control.</summary>
		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x060010B5 RID: 4277 RVA: 0x0003283C File Offset: 0x00030A3C
		// (remove) Token: 0x060010B6 RID: 4278 RVA: 0x0003284F File Offset: 0x00030A4F
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseLeaveDescr")]
		public event EventHandler MouseLeave
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseLeave, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseLeave, value);
			}
		}

		/// <summary>Occurs when the DPI setting for a control is changed programmatically before a DPI change event for its parent control or form has occurred.</summary>
		// Token: 0x140000BA RID: 186
		// (add) Token: 0x060010B7 RID: 4279 RVA: 0x00032862 File Offset: 0x00030A62
		// (remove) Token: 0x060010B8 RID: 4280 RVA: 0x00032875 File Offset: 0x00030A75
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnDpiChangedBeforeParentDescr")]
		public event EventHandler DpiChangedBeforeParent
		{
			add
			{
				base.Events.AddHandler(Control.EventDpiChangedBeforeParent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDpiChangedBeforeParent, value);
			}
		}

		/// <summary>Occurs when the DPI setting for a control is changed programmatically after the DPI of its parent control or form has changed.</summary>
		// Token: 0x140000BB RID: 187
		// (add) Token: 0x060010B9 RID: 4281 RVA: 0x00032888 File Offset: 0x00030A88
		// (remove) Token: 0x060010BA RID: 4282 RVA: 0x0003289B File Offset: 0x00030A9B
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnDpiChangedAfterParentDescr")]
		public event EventHandler DpiChangedAfterParent
		{
			add
			{
				base.Events.AddHandler(Control.EventDpiChangedAfterParent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventDpiChangedAfterParent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer rests on the control.</summary>
		// Token: 0x140000BC RID: 188
		// (add) Token: 0x060010BB RID: 4283 RVA: 0x000328AE File Offset: 0x00030AAE
		// (remove) Token: 0x060010BC RID: 4284 RVA: 0x000328C1 File Offset: 0x00030AC1
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseHoverDescr")]
		public event EventHandler MouseHover
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseHover, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseHover, value);
			}
		}

		/// <summary>Occurs when the mouse pointer is moved over the control.</summary>
		// Token: 0x140000BD RID: 189
		// (add) Token: 0x060010BD RID: 4285 RVA: 0x000328D4 File Offset: 0x00030AD4
		// (remove) Token: 0x060010BE RID: 4286 RVA: 0x000328E7 File Offset: 0x00030AE7
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseMoveDescr")]
		public event MouseEventHandler MouseMove
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseMove, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseMove, value);
			}
		}

		/// <summary>Occurs when the mouse pointer is over the control and a mouse button is released.</summary>
		// Token: 0x140000BE RID: 190
		// (add) Token: 0x060010BF RID: 4287 RVA: 0x000328FA File Offset: 0x00030AFA
		// (remove) Token: 0x060010C0 RID: 4288 RVA: 0x0003290D File Offset: 0x00030B0D
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseUpDescr")]
		public event MouseEventHandler MouseUp
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseUp, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseUp, value);
			}
		}

		/// <summary>Occurs when the mouse wheel moves while the control has focus.</summary>
		// Token: 0x140000BF RID: 191
		// (add) Token: 0x060010C1 RID: 4289 RVA: 0x00032920 File Offset: 0x00030B20
		// (remove) Token: 0x060010C2 RID: 4290 RVA: 0x00032933 File Offset: 0x00030B33
		[SRCategory("CatMouse")]
		[SRDescription("ControlOnMouseWheelDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event MouseEventHandler MouseWheel
		{
			add
			{
				base.Events.AddHandler(Control.EventMouseWheel, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMouseWheel, value);
			}
		}

		/// <summary>Occurs when the control is moved.</summary>
		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x060010C3 RID: 4291 RVA: 0x00032946 File Offset: 0x00030B46
		// (remove) Token: 0x060010C4 RID: 4292 RVA: 0x00032959 File Offset: 0x00030B59
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnMoveDescr")]
		public event EventHandler Move
		{
			add
			{
				base.Events.AddHandler(Control.EventMove, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventMove, value);
			}
		}

		/// <summary>Occurs before the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event when a key is pressed while focus is on this control.</summary>
		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x060010C5 RID: 4293 RVA: 0x0003296C File Offset: 0x00030B6C
		// (remove) Token: 0x060010C6 RID: 4294 RVA: 0x0003297F File Offset: 0x00030B7F
		[SRCategory("CatKey")]
		[SRDescription("PreviewKeyDownDescr")]
		public event PreviewKeyDownEventHandler PreviewKeyDown
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			add
			{
				base.Events.AddHandler(Control.EventPreviewKeyDown, value);
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			remove
			{
				base.Events.RemoveHandler(Control.EventPreviewKeyDown, value);
			}
		}

		/// <summary>Occurs when the control is resized.</summary>
		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x060010C7 RID: 4295 RVA: 0x00032992 File Offset: 0x00030B92
		// (remove) Token: 0x060010C8 RID: 4296 RVA: 0x000329A5 File Offset: 0x00030BA5
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnResizeDescr")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler Resize
		{
			add
			{
				base.Events.AddHandler(Control.EventResize, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventResize, value);
			}
		}

		/// <summary>Occurs when the focus or keyboard user interface (UI) cues change.</summary>
		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x060010C9 RID: 4297 RVA: 0x000329B8 File Offset: 0x00030BB8
		// (remove) Token: 0x060010CA RID: 4298 RVA: 0x000329CB File Offset: 0x00030BCB
		[SRCategory("CatBehavior")]
		[SRDescription("ControlOnChangeUICuesDescr")]
		public event UICuesEventHandler ChangeUICues
		{
			add
			{
				base.Events.AddHandler(Control.EventChangeUICues, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventChangeUICues, value);
			}
		}

		/// <summary>Occurs when the control style changes.</summary>
		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x060010CB RID: 4299 RVA: 0x000329DE File Offset: 0x00030BDE
		// (remove) Token: 0x060010CC RID: 4300 RVA: 0x000329F1 File Offset: 0x00030BF1
		[SRCategory("CatBehavior")]
		[SRDescription("ControlOnStyleChangedDescr")]
		public event EventHandler StyleChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventStyleChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventStyleChanged, value);
			}
		}

		/// <summary>Occurs when the system colors change.</summary>
		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x060010CD RID: 4301 RVA: 0x00032A04 File Offset: 0x00030C04
		// (remove) Token: 0x060010CE RID: 4302 RVA: 0x00032A17 File Offset: 0x00030C17
		[SRCategory("CatBehavior")]
		[SRDescription("ControlOnSystemColorsChangedDescr")]
		public event EventHandler SystemColorsChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventSystemColorsChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventSystemColorsChanged, value);
			}
		}

		/// <summary>Occurs when the control is validating.</summary>
		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x060010CF RID: 4303 RVA: 0x00032A2A File Offset: 0x00030C2A
		// (remove) Token: 0x060010D0 RID: 4304 RVA: 0x00032A3D File Offset: 0x00030C3D
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnValidatingDescr")]
		public event CancelEventHandler Validating
		{
			add
			{
				base.Events.AddHandler(Control.EventValidating, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventValidating, value);
			}
		}

		/// <summary>Occurs when the control is finished validating.</summary>
		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x060010D1 RID: 4305 RVA: 0x00032A50 File Offset: 0x00030C50
		// (remove) Token: 0x060010D2 RID: 4306 RVA: 0x00032A63 File Offset: 0x00030C63
		[SRCategory("CatFocus")]
		[SRDescription("ControlOnValidatedDescr")]
		public event EventHandler Validated
		{
			add
			{
				base.Events.AddHandler(Control.EventValidated, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventValidated, value);
			}
		}

		/// <summary>Notifies the accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" /> for the specified child control.</summary>
		/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of.</param>
		/// <param name="childID">The child <see cref="T:System.Windows.Forms.Control" /> to notify of the accessible event.</param>
		// Token: 0x060010D3 RID: 4307 RVA: 0x00032A76 File Offset: 0x00030C76
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal void AccessibilityNotifyClients(AccessibleEvents accEvent, int childID)
		{
			this.AccessibilityNotifyClients(accEvent, -4, childID);
		}

		/// <summary>Notifies the accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" /> for the specified child control .</summary>
		/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of.</param>
		/// <param name="objectID">The identifier of the <see cref="T:System.Windows.Forms.AccessibleObject" />.</param>
		/// <param name="childID">The child <see cref="T:System.Windows.Forms.Control" /> to notify of the accessible event.</param>
		// Token: 0x060010D4 RID: 4308 RVA: 0x00032A82 File Offset: 0x00030C82
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void AccessibilityNotifyClients(AccessibleEvents accEvent, int objectID, int childID)
		{
			if (this.IsHandleCreated && !LocalAppContextSwitches.NoClientNotifications)
			{
				UnsafeNativeMethods.NotifyWinEvent((int)accEvent, new HandleRef(this, this.Handle), objectID, childID + 1);
			}
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00032AA9 File Offset: 0x00030CA9
		private IntPtr ActiveXMergeRegion(IntPtr region)
		{
			return this.ActiveXInstance.MergeRegion(region);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00032AB7 File Offset: 0x00030CB7
		private void ActiveXOnFocus(bool focus)
		{
			this.ActiveXInstance.OnFocus(focus);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00032AC5 File Offset: 0x00030CC5
		private void ActiveXViewChanged()
		{
			this.ActiveXInstance.ViewChangedInternal();
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00032AD2 File Offset: 0x00030CD2
		private void ActiveXUpdateBounds(ref int x, ref int y, ref int width, ref int height, int flags)
		{
			this.ActiveXInstance.UpdateBounds(ref x, ref y, ref width, ref height, flags);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00032AE8 File Offset: 0x00030CE8
		internal virtual void AssignParent(Control value)
		{
			if (value != null)
			{
				this.RequiredScalingEnabled = value.RequiredScalingEnabled;
			}
			if (this.CanAccessProperties)
			{
				Font font = this.Font;
				Color foreColor = this.ForeColor;
				Color backColor = this.BackColor;
				RightToLeft rightToLeft = this.RightToLeft;
				bool enabled = this.Enabled;
				bool visible = this.Visible;
				this.parent = value;
				this.OnParentChanged(EventArgs.Empty);
				if (this.GetAnyDisposingInHierarchy())
				{
					return;
				}
				if (enabled != this.Enabled)
				{
					this.OnEnabledChanged(EventArgs.Empty);
				}
				bool visible2 = this.Visible;
				if (visible != visible2 && (visible || !visible2 || this.parent != null || this.GetTopLevel()))
				{
					this.OnVisibleChanged(EventArgs.Empty);
				}
				if (!font.Equals(this.Font))
				{
					this.OnFontChanged(EventArgs.Empty);
				}
				if (!foreColor.Equals(this.ForeColor))
				{
					this.OnForeColorChanged(EventArgs.Empty);
				}
				if (!backColor.Equals(this.BackColor))
				{
					this.OnBackColorChanged(EventArgs.Empty);
				}
				if (rightToLeft != this.RightToLeft)
				{
					this.OnRightToLeftChanged(EventArgs.Empty);
				}
				if (this.Properties.GetObject(Control.PropBindingManager) == null && this.Created)
				{
					this.OnBindingContextChanged(EventArgs.Empty);
				}
			}
			else
			{
				this.parent = value;
				this.OnParentChanged(EventArgs.Empty);
			}
			this.SetState(16777216, false);
			if (this.ParentInternal != null)
			{
				this.ParentInternal.LayoutEngine.InitLayout(this, BoundsSpecified.All);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Parent" /> property value changes.</summary>
		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x060010DA RID: 4314 RVA: 0x00032C75 File Offset: 0x00030E75
		// (remove) Token: 0x060010DB RID: 4315 RVA: 0x00032C88 File Offset: 0x00030E88
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnParentChangedDescr")]
		public event EventHandler ParentChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventParent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventParent, value);
			}
		}

		/// <summary>Executes the specified delegate asynchronously on the thread that the control's underlying handle was created on.</summary>
		/// <param name="method">A delegate to a method that takes no parameters.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the result of the <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">No appropriate window handle can be found.</exception>
		// Token: 0x060010DC RID: 4316 RVA: 0x00032C9B File Offset: 0x00030E9B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginInvoke(Delegate method)
		{
			return this.BeginInvoke(method, null);
		}

		/// <summary>Executes the specified delegate asynchronously with the specified arguments, on the thread that the control's underlying handle was created on.</summary>
		/// <param name="method">A delegate to a method that takes parameters of the same number and type that are contained in the <paramref name="args" /> parameter.</param>
		/// <param name="args">An array of objects to pass as arguments to the given method. This can be <see langword="null" /> if no arguments are needed.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the result of the <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" /> operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">No appropriate window handle can be found.</exception>
		// Token: 0x060010DD RID: 4317 RVA: 0x00032CA8 File Offset: 0x00030EA8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginInvoke(Delegate method, params object[] args)
		{
			IAsyncResult asyncResult;
			using (new Control.MultithreadSafeCallScope())
			{
				Control control = this.FindMarshalingControl();
				asyncResult = (IAsyncResult)control.MarshaledInvoke(this, method, args, false);
			}
			return asyncResult;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00032CF0 File Offset: 0x00030EF0
		internal void BeginUpdateInternal()
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			if (this.updateCount == 0)
			{
				this.SendMessage(11, 0, 0);
			}
			this.updateCount += 1;
		}

		/// <summary>Brings the control to the front of the z-order.</summary>
		// Token: 0x060010DF RID: 4319 RVA: 0x00032D20 File Offset: 0x00030F20
		public void BringToFront()
		{
			if (this.parent != null)
			{
				this.parent.Controls.SetChildIndex(this, 0);
				return;
			}
			if (this.IsHandleCreated && this.GetTopLevel() && SafeNativeMethods.IsWindowEnabled(new HandleRef(this.window, this.Handle)))
			{
				SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), NativeMethods.HWND_TOP, 0, 0, 0, 0, 3);
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00032D91 File Offset: 0x00030F91
		internal virtual bool CanProcessMnemonic()
		{
			return this.Enabled && this.Visible && (this.parent == null || this.parent.CanProcessMnemonic());
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00032DBC File Offset: 0x00030FBC
		internal virtual bool CanSelectCore()
		{
			if ((this.controlStyle & ControlStyles.Selectable) != ControlStyles.Selectable)
			{
				return false;
			}
			for (Control control = this; control != null; control = control.parent)
			{
				if (!control.Enabled || !control.Visible)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00032E00 File Offset: 0x00031000
		internal static void CheckParentingCycle(Control bottom, Control toFind)
		{
			Form form = null;
			Control control = null;
			for (Control control2 = bottom; control2 != null; control2 = control2.ParentInternal)
			{
				control = control2;
				if (control2 == toFind)
				{
					throw new ArgumentException(SR.GetString("CircularOwner"));
				}
			}
			if (control != null && control is Form)
			{
				Form form2 = (Form)control;
				for (Form form3 = form2; form3 != null; form3 = form3.OwnerInternal)
				{
					form = form3;
					if (form3 == toFind)
					{
						throw new ArgumentException(SR.GetString("CircularOwner"));
					}
				}
			}
			if (form != null && form.ParentInternal != null)
			{
				Control.CheckParentingCycle(form.ParentInternal, toFind);
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00032E88 File Offset: 0x00031088
		private void ChildGotFocus(Control child)
		{
			if (this.IsActiveX)
			{
				this.ActiveXOnFocus(true);
			}
			if (this.parent != null)
			{
				this.parent.ChildGotFocus(child);
			}
		}

		/// <summary>Retrieves a value indicating whether the specified control is a child of the control.</summary>
		/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if the specified control is a child of the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x060010E4 RID: 4324 RVA: 0x00032EAD File Offset: 0x000310AD
		public bool Contains(Control ctl)
		{
			while (ctl != null)
			{
				ctl = ctl.ParentInternal;
				if (ctl == null)
				{
					return false;
				}
				if (ctl == this)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x060010E5 RID: 4325 RVA: 0x00032EC8 File Offset: 0x000310C8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual AccessibleObject CreateAccessibilityInstance()
		{
			return new Control.ControlAccessibleObject(this);
		}

		/// <summary>Creates a new instance of the control collection for the control.</summary>
		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x060010E6 RID: 4326 RVA: 0x00032ED0 File Offset: 0x000310D0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual Control.ControlCollection CreateControlsInstance()
		{
			return new Control.ControlCollection(this);
		}

		/// <summary>Creates the <see cref="T:System.Drawing.Graphics" /> for the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> for the control.</returns>
		// Token: 0x060010E7 RID: 4327 RVA: 0x00032ED8 File Offset: 0x000310D8
		public Graphics CreateGraphics()
		{
			Graphics graphics;
			using (new Control.MultithreadSafeCallScope())
			{
				IntSecurity.CreateGraphicsForControl.Demand();
				graphics = this.CreateGraphicsInternal();
			}
			return graphics;
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00032F1C File Offset: 0x0003111C
		internal Graphics CreateGraphicsInternal()
		{
			return Graphics.FromHwndInternal(this.Handle);
		}

		/// <summary>Creates a handle for the control.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The object is in a disposed state.</exception>
		// Token: 0x060010E9 RID: 4329 RVA: 0x00032F2C File Offset: 0x0003112C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected virtual void CreateHandle()
		{
			IntPtr intPtr = IntPtr.Zero;
			if (this.GetState(2048))
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (this.GetState(262144))
			{
				return;
			}
			Rectangle bounds;
			try
			{
				this.SetState(262144, true);
				bounds = this.Bounds;
				if (Application.UseVisualStyles)
				{
					intPtr = UnsafeNativeMethods.ThemingScope.Activate();
				}
				CreateParams createParams = this.CreateParams;
				this.SetState(1073741824, (createParams.ExStyle & 4194304) != 0);
				if (this.parent != null)
				{
					Rectangle clientRectangle = this.parent.ClientRectangle;
					if (!clientRectangle.IsEmpty)
					{
						if (createParams.X != -2147483648)
						{
							createParams.X -= clientRectangle.X;
						}
						if (createParams.Y != -2147483648)
						{
							createParams.Y -= clientRectangle.Y;
						}
					}
				}
				if (createParams.Parent == IntPtr.Zero && (createParams.Style & 1073741824) != 0)
				{
					Application.ParkHandle(createParams, this.DpiAwarenessContext);
				}
				this.window.CreateHandle(createParams);
				this.UpdateReflectParent(true);
			}
			finally
			{
				this.SetState(262144, false);
				UnsafeNativeMethods.ThemingScope.Deactivate(intPtr);
			}
			if (this.Bounds != bounds)
			{
				LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
			}
		}

		/// <summary>Forces the creation of the visible control, including the creation of the handle and any visible child controls.</summary>
		// Token: 0x060010EA RID: 4330 RVA: 0x00033090 File Offset: 0x00031290
		public void CreateControl()
		{
			bool created = this.Created;
			this.CreateControl(false);
			if (this.Properties.GetObject(Control.PropBindingManager) == null && this.ParentInternal != null && !created)
			{
				this.OnBindingContextChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x000330D4 File Offset: 0x000312D4
		internal void CreateControl(bool fIgnoreVisible)
		{
			bool flag = (this.state & 1) == 0;
			flag = flag && this.Visible;
			if (flag || fIgnoreVisible)
			{
				this.state |= 1;
				bool flag2 = false;
				try
				{
					if (!this.IsHandleCreated)
					{
						this.CreateHandle();
					}
					Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
					if (controlCollection != null)
					{
						Control[] array = new Control[controlCollection.Count];
						controlCollection.CopyTo(array, 0);
						foreach (Control control in array)
						{
							if (control.IsHandleCreated)
							{
								control.SetParentHandle(this.Handle);
							}
							control.CreateControl(fIgnoreVisible);
						}
					}
					flag2 = true;
				}
				finally
				{
					if (!flag2)
					{
						this.state &= -2;
					}
				}
				this.OnCreateControl();
			}
		}

		/// <summary>Sends the specified message to the default window procedure.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x060010EC RID: 4332 RVA: 0x000331B8 File Offset: 0x000313B8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual void DefWndProc(ref Message m)
		{
			this.window.DefWndProc(ref m);
		}

		/// <summary>Destroys the handle associated with the control.</summary>
		// Token: 0x060010ED RID: 4333 RVA: 0x000331C8 File Offset: 0x000313C8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected virtual void DestroyHandle()
		{
			if (this.RecreatingHandle && this.threadCallbackList != null)
			{
				Queue queue = this.threadCallbackList;
				lock (queue)
				{
					if (Control.threadCallbackMessage != 0)
					{
						NativeMethods.MSG msg = default(NativeMethods.MSG);
						if (UnsafeNativeMethods.PeekMessage(ref msg, new HandleRef(this, this.Handle), Control.threadCallbackMessage, Control.threadCallbackMessage, 0))
						{
							this.SetState(32768, true);
						}
					}
				}
			}
			if (!this.RecreatingHandle && this.threadCallbackList != null)
			{
				Queue queue2 = this.threadCallbackList;
				lock (queue2)
				{
					Exception ex = new ObjectDisposedException(base.GetType().Name);
					while (this.threadCallbackList.Count > 0)
					{
						Control.ThreadMethodEntry threadMethodEntry = (Control.ThreadMethodEntry)this.threadCallbackList.Dequeue();
						threadMethodEntry.exception = ex;
						threadMethodEntry.Complete();
					}
				}
			}
			if ((64 & (int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this.window, this.InternalHandle), -20)) != 0)
			{
				UnsafeNativeMethods.DefMDIChildProc(this.InternalHandle, 16, IntPtr.Zero, IntPtr.Zero);
			}
			else
			{
				this.window.DestroyHandle();
			}
			this.trackMouseEvent = null;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060010EE RID: 4334 RVA: 0x0003331C File Offset: 0x0003151C
		protected override void Dispose(bool disposing)
		{
			if (this.GetState(2097152))
			{
				object @object = this.Properties.GetObject(Control.PropBackBrush);
				if (@object != null)
				{
					IntPtr intPtr = (IntPtr)@object;
					if (intPtr != IntPtr.Zero)
					{
						SafeNativeMethods.DeleteObject(new HandleRef(this, intPtr));
					}
					this.Properties.SetObject(Control.PropBackBrush, null);
				}
			}
			this.UpdateReflectParent(false);
			if (disposing)
			{
				if (this.GetState(4096))
				{
					return;
				}
				if (this.GetState(262144))
				{
					throw new InvalidOperationException(SR.GetString("ClosingWhileCreatingHandle", new object[] { "Dispose" }));
				}
				this.SetState(4096, true);
				this.SuspendLayout();
				try
				{
					this.DisposeAxControls();
					ContextMenu contextMenu = (ContextMenu)this.Properties.GetObject(Control.PropContextMenu);
					if (contextMenu != null)
					{
						contextMenu.Disposed -= this.DetachContextMenu;
					}
					this.ResetBindings();
					if (this.IsHandleCreated)
					{
						this.DestroyHandle();
					}
					if (this.parent != null)
					{
						this.parent.Controls.Remove(this);
					}
					Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
					if (controlCollection != null)
					{
						for (int i = 0; i < controlCollection.Count; i++)
						{
							Control control = controlCollection[i];
							control.parent = null;
							control.Dispose();
						}
						this.Properties.SetObject(Control.PropControlsCollection, null);
					}
					base.Dispose(disposing);
					return;
				}
				finally
				{
					this.ResumeLayout(false);
					this.SetState(4096, false);
					this.SetState(2048, true);
				}
			}
			if (this.window != null)
			{
				this.window.ForceExitMessageLoop();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x000334E0 File Offset: 0x000316E0
		internal virtual void DisposeAxControls()
		{
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].DisposeAxControls();
				}
			}
		}

		/// <summary>Begins a drag-and-drop operation.</summary>
		/// <param name="data">The data to drag.</param>
		/// <param name="allowedEffects">One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</param>
		/// <returns>A value from the <see cref="T:System.Windows.Forms.DragDropEffects" /> enumeration that represents the final effect that was performed during the drag-and-drop operation.</returns>
		// Token: 0x060010F0 RID: 4336 RVA: 0x00033524 File Offset: 0x00031724
		[UIPermission(SecurityAction.Demand, Clipboard = UIPermissionClipboard.OwnClipboard)]
		public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects)
		{
			int[] array = new int[1];
			UnsafeNativeMethods.IOleDropSource oleDropSource = new DropSource(this);
			IDataObject dataObject;
			if (data is IDataObject)
			{
				dataObject = (IDataObject)data;
			}
			else
			{
				DataObject dataObject2;
				if (data is IDataObject)
				{
					dataObject2 = new DataObject((IDataObject)data);
				}
				else
				{
					dataObject2 = new DataObject();
					dataObject2.SetData(data);
				}
				dataObject = dataObject2;
			}
			try
			{
				SafeNativeMethods.DoDragDrop(dataObject, oleDropSource, (int)allowedEffects, array);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
			return (DragDropEffects)array[0];
		}

		/// <summary>Supports rendering to the specified bitmap.</summary>
		/// <param name="bitmap">The bitmap to be drawn to.</param>
		/// <param name="targetBounds">The bounds within which the control is rendered.</param>
		// Token: 0x060010F1 RID: 4337 RVA: 0x000335A8 File Offset: 0x000317A8
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
		public void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
		{
			if (bitmap == null)
			{
				throw new ArgumentNullException("bitmap");
			}
			if (targetBounds.Width <= 0 || targetBounds.Height <= 0 || targetBounds.X < 0 || targetBounds.Y < 0)
			{
				throw new ArgumentException("targetBounds");
			}
			if (!this.IsHandleCreated)
			{
				this.CreateHandle();
			}
			int num = Math.Min(this.Width, targetBounds.Width);
			int num2 = Math.Min(this.Height, targetBounds.Height);
			using (Bitmap bitmap2 = new Bitmap(num, num2, bitmap.PixelFormat))
			{
				using (Graphics graphics = Graphics.FromImage(bitmap2))
				{
					IntPtr hdc = graphics.GetHdc();
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 791, hdc, (IntPtr)30);
					using (Graphics graphics2 = Graphics.FromImage(bitmap))
					{
						IntPtr hdc2 = graphics2.GetHdc();
						SafeNativeMethods.BitBlt(new HandleRef(graphics2, hdc2), targetBounds.X, targetBounds.Y, num, num2, new HandleRef(graphics, hdc), 0, 0, 13369376);
						graphics2.ReleaseHdcInternal(hdc2);
					}
					graphics.ReleaseHdcInternal(hdc);
				}
			}
		}

		/// <summary>Retrieves the return value of the asynchronous operation represented by the <see cref="T:System.IAsyncResult" /> passed.</summary>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> that represents a specific invoke asynchronous operation, returned when calling <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" />.</param>
		/// <returns>The <see cref="T:System.Object" /> generated by the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter value is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> object was not created by a preceding call of the <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" /> method from the same control.</exception>
		// Token: 0x060010F2 RID: 4338 RVA: 0x00033704 File Offset: 0x00031904
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object EndInvoke(IAsyncResult asyncResult)
		{
			object retVal;
			using (new Control.MultithreadSafeCallScope())
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Control.ThreadMethodEntry threadMethodEntry = asyncResult as Control.ThreadMethodEntry;
				if (threadMethodEntry == null)
				{
					throw new ArgumentException(SR.GetString("ControlBadAsyncResult"), "asyncResult");
				}
				if (!asyncResult.IsCompleted)
				{
					Control control = this.FindMarshalingControl();
					int num;
					if (SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(control, control.Handle), out num) == SafeNativeMethods.GetCurrentThreadId())
					{
						control.InvokeMarshaledCallbacks();
					}
					else
					{
						control = threadMethodEntry.marshaler;
						control.WaitForWaitHandle(asyncResult.AsyncWaitHandle);
					}
				}
				if (threadMethodEntry.exception != null)
				{
					throw threadMethodEntry.exception;
				}
				retVal = threadMethodEntry.retVal;
			}
			return retVal;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x000337C0 File Offset: 0x000319C0
		internal bool EndUpdateInternal()
		{
			return this.EndUpdateInternal(true);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x000337C9 File Offset: 0x000319C9
		internal bool EndUpdateInternal(bool invalidate)
		{
			if (this.updateCount > 0)
			{
				this.updateCount -= 1;
				if (this.updateCount == 0)
				{
					this.SendMessage(11, -1, 0);
					if (invalidate)
					{
						this.Invalidate();
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Retrieves the form that the control is on.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Form" /> that the control is on.</returns>
		// Token: 0x060010F5 RID: 4341 RVA: 0x00033802 File Offset: 0x00031A02
		[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
		public Form FindForm()
		{
			return this.FindFormInternal();
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0003380C File Offset: 0x00031A0C
		internal Form FindFormInternal()
		{
			Control control = this;
			while (control != null && !(control is Form))
			{
				control = control.ParentInternal;
			}
			return (Form)control;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00033838 File Offset: 0x00031A38
		private Control FindMarshalingControl()
		{
			Control control2;
			lock (this)
			{
				Control control = this;
				while (control != null && !control.IsHandleCreated)
				{
					Control parentInternal = control.ParentInternal;
					control = parentInternal;
				}
				if (control == null)
				{
					control = this;
				}
				control2 = control;
			}
			return control2;
		}

		/// <summary>Determines if the control is a top-level control.</summary>
		/// <returns>
		///   <see langword="true" /> if the control is a top-level control; otherwise, <see langword="false" />.</returns>
		// Token: 0x060010F8 RID: 4344 RVA: 0x00033890 File Offset: 0x00031A90
		protected bool GetTopLevel()
		{
			return (this.state & 524288) != 0;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000338A4 File Offset: 0x00031AA4
		internal void RaiseCreateHandleEvent(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventHandleCreated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the appropriate key event.</summary>
		/// <param name="key">The event to raise.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x060010FA RID: 4346 RVA: 0x000338D4 File Offset: 0x00031AD4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseKeyEvent(object key, KeyEventArgs e)
		{
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[key];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		/// <summary>Raises the appropriate mouse event.</summary>
		/// <param name="key">The event to raise.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060010FB RID: 4347 RVA: 0x00033900 File Offset: 0x00031B00
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseMouseEvent(object key, MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[key];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Sets input focus to the control.</summary>
		/// <returns>
		///   <see langword="true" /> if the input focus request was successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x060010FC RID: 4348 RVA: 0x0003392A File Offset: 0x00031B2A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool Focus()
		{
			IntSecurity.ModifyFocus.Demand();
			return this.FocusInternal();
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0003393C File Offset: 0x00031B3C
		internal virtual bool FocusInternal()
		{
			if (this.CanFocus)
			{
				UnsafeNativeMethods.SetFocus(new HandleRef(this, this.Handle));
			}
			if (this.Focused && this.ParentInternal != null)
			{
				IContainerControl containerControlInternal = this.ParentInternal.GetContainerControlInternal();
				if (containerControlInternal != null)
				{
					if (containerControlInternal is ContainerControl)
					{
						((ContainerControl)containerControlInternal).SetActiveControlInternal(this);
					}
					else
					{
						containerControlInternal.ActiveControl = this;
					}
				}
			}
			return this.Focused;
		}

		/// <summary>Retrieves the control that contains the specified handle.</summary>
		/// <param name="handle">The window handle (<see langword="HWND" />) to search for.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that represents the control associated with the specified handle; returns <see langword="null" /> if no control with the specified handle is found.</returns>
		// Token: 0x060010FE RID: 4350 RVA: 0x000339A5 File Offset: 0x00031BA5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Control FromChildHandle(IntPtr handle)
		{
			IntSecurity.ControlFromHandleOrLocation.Demand();
			return Control.FromChildHandleInternal(handle);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x000339B8 File Offset: 0x00031BB8
		internal static Control FromChildHandleInternal(IntPtr handle)
		{
			while (handle != IntPtr.Zero)
			{
				Control control = Control.FromHandleInternal(handle);
				if (control != null)
				{
					return control;
				}
				handle = UnsafeNativeMethods.GetAncestor(new HandleRef(null, handle), 1);
			}
			return null;
		}

		/// <summary>Returns the control that is currently associated with the specified handle.</summary>
		/// <param name="handle">The window handle (<see langword="HWND" />) to search for.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the control associated with the specified handle; returns <see langword="null" /> if no control with the specified handle is found.</returns>
		// Token: 0x06001100 RID: 4352 RVA: 0x000339F0 File Offset: 0x00031BF0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Control FromHandle(IntPtr handle)
		{
			IntSecurity.ControlFromHandleOrLocation.Demand();
			return Control.FromHandleInternal(handle);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00033A04 File Offset: 0x00031C04
		internal static Control FromHandleInternal(IntPtr handle)
		{
			NativeWindow nativeWindow = NativeWindow.FromHandle(handle);
			while (nativeWindow != null && !(nativeWindow is Control.ControlNativeWindow))
			{
				nativeWindow = nativeWindow.PreviousWindow;
			}
			if (nativeWindow is Control.ControlNativeWindow)
			{
				return ((Control.ControlNativeWindow)nativeWindow).GetControl();
			}
			return null;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00033A44 File Offset: 0x00031C44
		internal Size ApplySizeConstraints(int width, int height)
		{
			return this.ApplyBoundsConstraints(0, 0, width, height).Size;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00033A64 File Offset: 0x00031C64
		internal Size ApplySizeConstraints(Size proposedSize)
		{
			return this.ApplyBoundsConstraints(0, 0, proposedSize.Width, proposedSize.Height).Size;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00033A90 File Offset: 0x00031C90
		internal virtual Rectangle ApplyBoundsConstraints(int suggestedX, int suggestedY, int proposedWidth, int proposedHeight)
		{
			if (this.MaximumSize != Size.Empty || this.MinimumSize != Size.Empty)
			{
				Size size = LayoutUtils.ConvertZeroToUnbounded(this.MaximumSize);
				Rectangle rectangle = new Rectangle(suggestedX, suggestedY, 0, 0);
				rectangle.Size = LayoutUtils.IntersectSizes(new Size(proposedWidth, proposedHeight), size);
				rectangle.Size = LayoutUtils.UnionSizes(rectangle.Size, this.MinimumSize);
				return rectangle;
			}
			return new Rectangle(suggestedX, suggestedY, proposedWidth, proposedHeight);
		}

		/// <summary>Retrieves the child control that is located at the specified coordinates, specifying whether to ignore child controls of a certain type.</summary>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that contains the coordinates where you want to look for a control. Coordinates are expressed relative to the upper-left corner of the control's client area.</param>
		/// <param name="skipValue">One of the values of <see cref="T:System.Windows.Forms.GetChildAtPointSkip" />, determining whether to ignore child controls of a certain type.</param>
		/// <returns>The child <see cref="T:System.Windows.Forms.Control" /> at the specified coordinates.</returns>
		// Token: 0x06001105 RID: 4357 RVA: 0x00033B14 File Offset: 0x00031D14
		public Control GetChildAtPoint(Point pt, GetChildAtPointSkip skipValue)
		{
			if ((skipValue < GetChildAtPointSkip.None) || skipValue > (GetChildAtPointSkip.Invisible | GetChildAtPointSkip.Disabled | GetChildAtPointSkip.Transparent))
			{
				throw new InvalidEnumArgumentException("skipValue", (int)skipValue, typeof(GetChildAtPointSkip));
			}
			IntPtr intPtr = UnsafeNativeMethods.ChildWindowFromPointEx(new HandleRef(null, this.Handle), pt.X, pt.Y, (int)skipValue);
			Control control = Control.FromChildHandleInternal(intPtr);
			if (control != null && !this.IsDescendant(control))
			{
				IntSecurity.ControlFromHandleOrLocation.Demand();
			}
			if (control != this)
			{
				return control;
			}
			return null;
		}

		/// <summary>Retrieves the child control that is located at the specified coordinates.</summary>
		/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that contains the coordinates where you want to look for a control. Coordinates are expressed relative to the upper-left corner of the control's client area.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the control that is located at the specified point.</returns>
		// Token: 0x06001106 RID: 4358 RVA: 0x00033B86 File Offset: 0x00031D86
		public Control GetChildAtPoint(Point pt)
		{
			return this.GetChildAtPoint(pt, GetChildAtPointSkip.None);
		}

		/// <summary>Returns the next <see cref="T:System.Windows.Forms.ContainerControl" /> up the control's chain of parent controls.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IContainerControl" />, that represents the parent of the <see cref="T:System.Windows.Forms.Control" />.</returns>
		// Token: 0x06001107 RID: 4359 RVA: 0x00033B90 File Offset: 0x00031D90
		public IContainerControl GetContainerControl()
		{
			IntSecurity.GetParent.Demand();
			return this.GetContainerControlInternal();
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00033BA2 File Offset: 0x00031DA2
		private static bool IsFocusManagingContainerControl(Control ctl)
		{
			return (ctl.controlStyle & ControlStyles.ContainerControl) == ControlStyles.ContainerControl && ctl is IContainerControl;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00033BBA File Offset: 0x00031DBA
		internal bool IsUpdating()
		{
			return this.updateCount > 0;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00033BC8 File Offset: 0x00031DC8
		internal IContainerControl GetContainerControlInternal()
		{
			Control control = this;
			if (control != null && this.IsContainerControl)
			{
				control = control.ParentInternal;
			}
			while (control != null && !Control.IsFocusManagingContainerControl(control))
			{
				control = control.ParentInternal;
			}
			return (IContainerControl)control;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00033C03 File Offset: 0x00031E03
		private static Control.FontHandleWrapper GetDefaultFontHandleWrapper()
		{
			if (Control.defaultFontHandleWrapper == null)
			{
				Control.defaultFontHandleWrapper = new Control.FontHandleWrapper(Control.DefaultFont);
			}
			return Control.defaultFontHandleWrapper;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00033C20 File Offset: 0x00031E20
		internal IntPtr GetHRgn(Region region)
		{
			Graphics graphics = this.CreateGraphicsInternal();
			IntPtr hrgn = region.GetHrgn(graphics);
			System.Internal.HandleCollector.Add(hrgn, NativeMethods.CommonHandles.GDI);
			graphics.Dispose();
			return hrgn;
		}

		/// <summary>Retrieves the bounds within which the control is scaled.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display bounds.</param>
		/// <param name="factor">The height and width of the control's bounds.</param>
		/// <param name="specified">One of the values of <see cref="T:System.Windows.Forms.BoundsSpecified" /> that specifies the bounds of the control to use when defining its size and position.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the bounds within which the control is scaled.</returns>
		// Token: 0x0600110D RID: 4365 RVA: 0x00033C50 File Offset: 0x00031E50
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			NativeMethods.RECT rect = new NativeMethods.RECT(0, 0, 0, 0);
			CreateParams createParams = this.CreateParams;
			this.AdjustWindowRectEx(ref rect, createParams.Style, this.HasMenu, createParams.ExStyle);
			float num = factor.Width;
			float num2 = factor.Height;
			int num3 = bounds.X;
			int num4 = bounds.Y;
			bool flag = !this.GetState(524288);
			if (flag)
			{
				ISite site = this.Site;
				if (site != null && site.DesignMode)
				{
					IDesignerHost designerHost = site.GetService(typeof(IDesignerHost)) as IDesignerHost;
					if (designerHost != null && designerHost.RootComponent == this)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				if ((specified & BoundsSpecified.X) != BoundsSpecified.None)
				{
					num3 = (int)Math.Round((double)((float)bounds.X * num));
				}
				if ((specified & BoundsSpecified.Y) != BoundsSpecified.None)
				{
					num4 = (int)Math.Round((double)((float)bounds.Y * num2));
				}
			}
			int num5 = bounds.Width;
			int num6 = bounds.Height;
			if ((this.controlStyle & ControlStyles.FixedWidth) != ControlStyles.FixedWidth && (specified & BoundsSpecified.Width) != BoundsSpecified.None)
			{
				int num7 = rect.right - rect.left;
				int num8 = bounds.Width - num7;
				num5 = (int)Math.Round((double)((float)num8 * num)) + num7;
			}
			if ((this.controlStyle & ControlStyles.FixedHeight) != ControlStyles.FixedHeight && (specified & BoundsSpecified.Height) != BoundsSpecified.None)
			{
				int num9 = rect.bottom - rect.top;
				int num10 = bounds.Height - num9;
				num6 = (int)Math.Round((double)((float)num10 * num2)) + num9;
			}
			return new Rectangle(num3, num4, num5, num6);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00033DCC File Offset: 0x00031FCC
		private MouseButtons GetXButton(int wparam)
		{
			if (wparam == 1)
			{
				return MouseButtons.XButton1;
			}
			if (wparam != 2)
			{
				return MouseButtons.None;
			}
			return MouseButtons.XButton2;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00033DE5 File Offset: 0x00031FE5
		internal virtual bool GetVisibleCore()
		{
			return this.GetState(2) && (this.ParentInternal == null || this.ParentInternal.GetVisibleCore());
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00033E08 File Offset: 0x00032008
		internal bool GetAnyDisposingInHierarchy()
		{
			Control control = this;
			bool flag = false;
			while (control != null)
			{
				if (control.Disposing)
				{
					flag = true;
					break;
				}
				control = control.parent;
			}
			return flag;
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x00033E34 File Offset: 0x00032034
		private MenuItem GetMenuItemFromHandleId(IntPtr hmenu, int item)
		{
			MenuItem menuItem = null;
			int menuItemID = UnsafeNativeMethods.GetMenuItemID(new HandleRef(null, hmenu), item);
			if (menuItemID == -1)
			{
				IntPtr intPtr = IntPtr.Zero;
				intPtr = UnsafeNativeMethods.GetSubMenu(new HandleRef(null, hmenu), item);
				int menuItemCount = UnsafeNativeMethods.GetMenuItemCount(new HandleRef(null, intPtr));
				MenuItem menuItem2 = null;
				for (int i = 0; i < menuItemCount; i++)
				{
					menuItem2 = this.GetMenuItemFromHandleId(intPtr, i);
					if (menuItem2 != null)
					{
						Menu menu = menuItem2.Parent;
						if (menu != null && menu is MenuItem)
						{
							menuItem2 = (MenuItem)menu;
							break;
						}
						menuItem2 = null;
					}
				}
				menuItem = menuItem2;
			}
			else
			{
				Command commandFromID = Command.GetCommandFromID(menuItemID);
				if (commandFromID != null)
				{
					object target = commandFromID.Target;
					if (target != null && target is MenuItem.MenuItemData)
					{
						menuItem = ((MenuItem.MenuItemData)target).baseItem;
					}
				}
			}
			return menuItem;
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00033EF4 File Offset: 0x000320F4
		private ArrayList GetChildControlsTabOrderList(bool handleCreatedOnly)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				if (!handleCreatedOnly || control.IsHandleCreated)
				{
					arrayList.Add(new Control.ControlTabOrderHolder(arrayList.Count, control.TabIndex, control));
				}
			}
			arrayList.Sort(new Control.ControlTabOrderComparer());
			return arrayList;
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00033F7C File Offset: 0x0003217C
		private int[] GetChildWindowsInTabOrder()
		{
			ArrayList childWindowsTabOrderList = this.GetChildWindowsTabOrderList();
			int[] array = new int[childWindowsTabOrderList.Count];
			for (int i = 0; i < childWindowsTabOrderList.Count; i++)
			{
				array[i] = ((Control.ControlTabOrderHolder)childWindowsTabOrderList[i]).oldOrder;
			}
			return array;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00033FC4 File Offset: 0x000321C4
		internal Control[] GetChildControlsInTabOrder(bool handleCreatedOnly)
		{
			ArrayList childControlsTabOrderList = this.GetChildControlsTabOrderList(handleCreatedOnly);
			Control[] array = new Control[childControlsTabOrderList.Count];
			for (int i = 0; i < childControlsTabOrderList.Count; i++)
			{
				array[i] = ((Control.ControlTabOrderHolder)childControlsTabOrderList[i]).control;
			}
			return array;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0003400C File Offset: 0x0003220C
		private static ArrayList GetChildWindows(IntPtr hWndParent)
		{
			ArrayList arrayList = new ArrayList();
			IntPtr intPtr = UnsafeNativeMethods.GetWindow(new HandleRef(null, hWndParent), 5);
			while (intPtr != IntPtr.Zero)
			{
				arrayList.Add(intPtr);
				intPtr = UnsafeNativeMethods.GetWindow(new HandleRef(null, intPtr), 2);
			}
			return arrayList;
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00034058 File Offset: 0x00032258
		private ArrayList GetChildWindowsTabOrderList()
		{
			ArrayList arrayList = new ArrayList();
			ArrayList childWindows = Control.GetChildWindows(this.Handle);
			foreach (object obj in childWindows)
			{
				IntPtr intPtr = (IntPtr)obj;
				Control control = Control.FromHandleInternal(intPtr);
				int num = ((control == null) ? (-1) : control.TabIndex);
				arrayList.Add(new Control.ControlTabOrderHolder(arrayList.Count, num, control));
			}
			arrayList.Sort(new Control.ControlTabOrderComparer());
			return arrayList;
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x000340F4 File Offset: 0x000322F4
		internal virtual Control GetFirstChildControlInTabOrder(bool forward)
		{
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			Control control = null;
			if (controlCollection != null)
			{
				if (forward)
				{
					for (int i = 0; i < controlCollection.Count; i++)
					{
						if (control == null || control.tabIndex > controlCollection[i].tabIndex)
						{
							control = controlCollection[i];
						}
					}
				}
				else
				{
					for (int j = controlCollection.Count - 1; j >= 0; j--)
					{
						if (control == null || control.tabIndex < controlCollection[j].tabIndex)
						{
							control = controlCollection[j];
						}
					}
				}
			}
			return control;
		}

		/// <summary>Retrieves the next control forward or back in the tab order of child controls.</summary>
		/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> to start the search with.</param>
		/// <param name="forward">
		///   <see langword="true" /> to search forward in the tab order; <see langword="false" /> to search backward.</param>
		/// <returns>The next <see cref="T:System.Windows.Forms.Control" /> in the tab order.</returns>
		// Token: 0x06001118 RID: 4376 RVA: 0x00034184 File Offset: 0x00032384
		public Control GetNextControl(Control ctl, bool forward)
		{
			if (!this.Contains(ctl))
			{
				ctl = this;
			}
			if (forward)
			{
				Control.ControlCollection controlCollection = (Control.ControlCollection)ctl.Properties.GetObject(Control.PropControlsCollection);
				if (controlCollection != null && controlCollection.Count > 0 && (ctl == this || !Control.IsFocusManagingContainerControl(ctl)))
				{
					Control firstChildControlInTabOrder = ctl.GetFirstChildControlInTabOrder(true);
					if (firstChildControlInTabOrder != null)
					{
						return firstChildControlInTabOrder;
					}
				}
				while (ctl != this)
				{
					int num = ctl.tabIndex;
					bool flag = false;
					Control control = null;
					Control control2 = ctl.parent;
					int num2 = 0;
					Control.ControlCollection controlCollection2 = (Control.ControlCollection)control2.Properties.GetObject(Control.PropControlsCollection);
					if (controlCollection2 != null)
					{
						num2 = controlCollection2.Count;
					}
					for (int i = 0; i < num2; i++)
					{
						if (controlCollection2[i] != ctl)
						{
							if (controlCollection2[i].tabIndex >= num && (control == null || control.tabIndex > controlCollection2[i].tabIndex) && (controlCollection2[i].tabIndex != num || flag))
							{
								control = controlCollection2[i];
							}
						}
						else
						{
							flag = true;
						}
					}
					if (control != null)
					{
						return control;
					}
					ctl = ctl.parent;
				}
			}
			else
			{
				if (ctl != this)
				{
					int num3 = ctl.tabIndex;
					bool flag2 = false;
					Control control3 = null;
					Control control4 = ctl.parent;
					int num4 = 0;
					Control.ControlCollection controlCollection3 = (Control.ControlCollection)control4.Properties.GetObject(Control.PropControlsCollection);
					if (controlCollection3 != null)
					{
						num4 = controlCollection3.Count;
					}
					for (int j = num4 - 1; j >= 0; j--)
					{
						if (controlCollection3[j] != ctl)
						{
							if (controlCollection3[j].tabIndex <= num3 && (control3 == null || control3.tabIndex < controlCollection3[j].tabIndex) && (controlCollection3[j].tabIndex != num3 || flag2))
							{
								control3 = controlCollection3[j];
							}
						}
						else
						{
							flag2 = true;
						}
					}
					if (control3 != null)
					{
						ctl = control3;
					}
					else
					{
						if (control4 == this)
						{
							return null;
						}
						return control4;
					}
				}
				Control.ControlCollection controlCollection4 = (Control.ControlCollection)ctl.Properties.GetObject(Control.PropControlsCollection);
				while (controlCollection4 != null && controlCollection4.Count > 0 && (ctl == this || !Control.IsFocusManagingContainerControl(ctl)))
				{
					Control firstChildControlInTabOrder2 = ctl.GetFirstChildControlInTabOrder(false);
					if (firstChildControlInTabOrder2 == null)
					{
						break;
					}
					ctl = firstChildControlInTabOrder2;
					controlCollection4 = (Control.ControlCollection)ctl.Properties.GetObject(Control.PropControlsCollection);
				}
			}
			if (ctl != this)
			{
				return ctl;
			}
			return null;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000343E4 File Offset: 0x000325E4
		internal static IntPtr GetSafeHandle(IWin32Window window)
		{
			IntPtr intPtr = IntPtr.Zero;
			Control control = window as Control;
			if (control != null)
			{
				return control.Handle;
			}
			IntSecurity.AllWindows.Demand();
			intPtr = window.Handle;
			if (intPtr == IntPtr.Zero || UnsafeNativeMethods.IsWindow(new HandleRef(null, intPtr)))
			{
				return intPtr;
			}
			throw new Win32Exception(6);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0003443E File Offset: 0x0003263E
		internal bool GetState(int flag)
		{
			return (this.state & flag) != 0;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0003444B File Offset: 0x0003264B
		private bool GetState2(int flag)
		{
			return (this.state2 & flag) != 0;
		}

		/// <summary>Retrieves the value of the specified control style bit for the control.</summary>
		/// <param name="flag">The <see cref="T:System.Windows.Forms.ControlStyles" /> bit to return the value from.</param>
		/// <returns>
		///   <see langword="true" /> if the specified control style bit is set to <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600111C RID: 4380 RVA: 0x00034458 File Offset: 0x00032658
		protected bool GetStyle(ControlStyles flag)
		{
			return (this.controlStyle & flag) == flag;
		}

		/// <summary>Conceals the control from the user.</summary>
		// Token: 0x0600111D RID: 4381 RVA: 0x00034465 File Offset: 0x00032665
		public void Hide()
		{
			this.Visible = false;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00034470 File Offset: 0x00032670
		private void HookMouseEvent()
		{
			if (!this.GetState(16384))
			{
				this.SetState(16384, true);
				if (this.trackMouseEvent == null)
				{
					this.trackMouseEvent = new NativeMethods.TRACKMOUSEEVENT();
					this.trackMouseEvent.dwFlags = 3;
					this.trackMouseEvent.hwndTrack = this.Handle;
				}
				SafeNativeMethods.TrackMouseEvent(this.trackMouseEvent);
			}
		}

		/// <summary>Called after the control has been added to another container.</summary>
		// Token: 0x0600111F RID: 4383 RVA: 0x000344D2 File Offset: 0x000326D2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void InitLayout()
		{
			this.LayoutEngine.InitLayout(this, BoundsSpecified.All);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000344E2 File Offset: 0x000326E2
		private void InitScaling(BoundsSpecified specified)
		{
			this.requiredScaling |= (byte)(specified & BoundsSpecified.All);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x000344F8 File Offset: 0x000326F8
		internal virtual IntPtr InitializeDCForWmCtlColor(IntPtr dc, int msg)
		{
			if (!this.GetStyle(ControlStyles.UserPaint))
			{
				SafeNativeMethods.SetTextColor(new HandleRef(null, dc), ColorTranslator.ToWin32(this.ForeColor));
				SafeNativeMethods.SetBkColor(new HandleRef(null, dc), ColorTranslator.ToWin32(this.BackColor));
				return this.BackColorBrush;
			}
			return UnsafeNativeMethods.GetStockObject(5);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0003454C File Offset: 0x0003274C
		private void InitMouseWheelSupport()
		{
			if (!Control.mouseWheelInit)
			{
				Control.mouseWheelRoutingNeeded = !SystemInformation.NativeMouseWheelSupport;
				if (Control.mouseWheelRoutingNeeded)
				{
					IntPtr intPtr = IntPtr.Zero;
					intPtr = UnsafeNativeMethods.FindWindow("MouseZ", "Magellan MSWHEEL");
					if (intPtr != IntPtr.Zero)
					{
						int num = SafeNativeMethods.RegisterWindowMessage("MSWHEEL_ROLLMSG");
						if (num != 0)
						{
							Control.mouseWheelMessage = num;
						}
					}
				}
				Control.mouseWheelInit = true;
			}
		}

		/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to invalidate.</param>
		// Token: 0x06001123 RID: 4387 RVA: 0x000345B1 File Offset: 0x000327B1
		public void Invalidate(Region region)
		{
			this.Invalidate(region, false);
		}

		/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control. Optionally, invalidates the child controls assigned to the control.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to invalidate.</param>
		/// <param name="invalidateChildren">
		///   <see langword="true" /> to invalidate the control's child controls; otherwise, <see langword="false" />.</param>
		// Token: 0x06001124 RID: 4388 RVA: 0x000345BC File Offset: 0x000327BC
		public void Invalidate(Region region, bool invalidateChildren)
		{
			if (region == null)
			{
				this.Invalidate(invalidateChildren);
				return;
			}
			if (this.IsHandleCreated)
			{
				IntPtr hrgn = this.GetHRgn(region);
				try
				{
					if (invalidateChildren)
					{
						SafeNativeMethods.RedrawWindow(new HandleRef(this, this.Handle), null, new HandleRef(region, hrgn), 133);
					}
					else
					{
						using (new Control.MultithreadSafeCallScope())
						{
							SafeNativeMethods.InvalidateRgn(new HandleRef(this, this.Handle), new HandleRef(region, hrgn), !this.GetStyle(ControlStyles.Opaque));
						}
					}
				}
				finally
				{
					SafeNativeMethods.DeleteObject(new HandleRef(region, hrgn));
				}
				Rectangle rectangle = Rectangle.Empty;
				using (Graphics graphics = this.CreateGraphicsInternal())
				{
					rectangle = Rectangle.Ceiling(region.GetBounds(graphics));
				}
				this.OnInvalidated(new InvalidateEventArgs(rectangle));
			}
		}

		/// <summary>Invalidates the entire surface of the control and causes the control to be redrawn.</summary>
		// Token: 0x06001125 RID: 4389 RVA: 0x000346A8 File Offset: 0x000328A8
		public void Invalidate()
		{
			this.Invalidate(false);
		}

		/// <summary>Invalidates a specific region of the control and causes a paint message to be sent to the control. Optionally, invalidates the child controls assigned to the control.</summary>
		/// <param name="invalidateChildren">
		///   <see langword="true" /> to invalidate the control's child controls; otherwise, <see langword="false" />.</param>
		// Token: 0x06001126 RID: 4390 RVA: 0x000346B4 File Offset: 0x000328B4
		public void Invalidate(bool invalidateChildren)
		{
			if (this.IsHandleCreated)
			{
				if (invalidateChildren)
				{
					SafeNativeMethods.RedrawWindow(new HandleRef(this.window, this.Handle), null, NativeMethods.NullHandleRef, 133);
				}
				else
				{
					using (new Control.MultithreadSafeCallScope())
					{
						SafeNativeMethods.InvalidateRect(new HandleRef(this.window, this.Handle), null, (this.controlStyle & ControlStyles.Opaque) != ControlStyles.Opaque);
					}
				}
				this.NotifyInvalidate(this.ClientRectangle);
			}
		}

		/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control.</summary>
		/// <param name="rc">A <see cref="T:System.Drawing.Rectangle" /> that represents the region to invalidate.</param>
		// Token: 0x06001127 RID: 4391 RVA: 0x00034744 File Offset: 0x00032944
		public void Invalidate(Rectangle rc)
		{
			this.Invalidate(rc, false);
		}

		/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control. Optionally, invalidates the child controls assigned to the control.</summary>
		/// <param name="rc">A <see cref="T:System.Drawing.Rectangle" /> that represents the region to invalidate.</param>
		/// <param name="invalidateChildren">
		///   <see langword="true" /> to invalidate the control's child controls; otherwise, <see langword="false" />.</param>
		// Token: 0x06001128 RID: 4392 RVA: 0x00034750 File Offset: 0x00032950
		public void Invalidate(Rectangle rc, bool invalidateChildren)
		{
			if (rc.IsEmpty)
			{
				this.Invalidate(invalidateChildren);
				return;
			}
			if (this.IsHandleCreated)
			{
				if (invalidateChildren)
				{
					NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(rc.X, rc.Y, rc.Width, rc.Height);
					SafeNativeMethods.RedrawWindow(new HandleRef(this.window, this.Handle), ref rect, NativeMethods.NullHandleRef, 133);
				}
				else
				{
					NativeMethods.RECT rect2 = NativeMethods.RECT.FromXYWH(rc.X, rc.Y, rc.Width, rc.Height);
					using (new Control.MultithreadSafeCallScope())
					{
						SafeNativeMethods.InvalidateRect(new HandleRef(this.window, this.Handle), ref rect2, (this.controlStyle & ControlStyles.Opaque) != ControlStyles.Opaque);
					}
				}
				this.NotifyInvalidate(rc);
			}
		}

		/// <summary>Executes the specified delegate on the thread that owns the control's underlying window handle.</summary>
		/// <param name="method">A delegate that contains a method to be called in the control's thread context.</param>
		/// <returns>The return value from the delegate being invoked, or <see langword="null" /> if the delegate has no return value.</returns>
		// Token: 0x06001129 RID: 4393 RVA: 0x00034838 File Offset: 0x00032A38
		public object Invoke(Delegate method)
		{
			return this.Invoke(method, null);
		}

		/// <summary>Executes the specified delegate, on the thread that owns the control's underlying window handle, with the specified list of arguments.</summary>
		/// <param name="method">A delegate to a method that takes parameters of the same number and type that are contained in the <paramref name="args" /> parameter.</param>
		/// <param name="args">An array of objects to pass as arguments to the specified method. This parameter can be <see langword="null" /> if the method takes no arguments.</param>
		/// <returns>An <see cref="T:System.Object" /> that contains the return value from the delegate being invoked, or <see langword="null" /> if the delegate has no return value.</returns>
		// Token: 0x0600112A RID: 4394 RVA: 0x00034844 File Offset: 0x00032A44
		public object Invoke(Delegate method, params object[] args)
		{
			object obj;
			using (new Control.MultithreadSafeCallScope())
			{
				Control control = this.FindMarshalingControl();
				obj = control.MarshaledInvoke(this, method, args, true);
			}
			return obj;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00034888 File Offset: 0x00032A88
		private void InvokeMarshaledCallback(Control.ThreadMethodEntry tme)
		{
			if (tme.executionContext != null)
			{
				if (Control.invokeMarshaledCallbackHelperDelegate == null)
				{
					Control.invokeMarshaledCallbackHelperDelegate = new ContextCallback(Control.InvokeMarshaledCallbackHelper);
				}
				if (SynchronizationContext.Current == null)
				{
					WindowsFormsSynchronizationContext.InstallIfNeeded();
				}
				tme.syncContext = SynchronizationContext.Current;
				ExecutionContext.Run(tme.executionContext, Control.invokeMarshaledCallbackHelperDelegate, tme);
				return;
			}
			Control.InvokeMarshaledCallbackHelper(tme);
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x000348E4 File Offset: 0x00032AE4
		private static void InvokeMarshaledCallbackHelper(object obj)
		{
			Control.ThreadMethodEntry threadMethodEntry = (Control.ThreadMethodEntry)obj;
			if (threadMethodEntry.syncContext != null)
			{
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				try
				{
					SynchronizationContext.SetSynchronizationContext(threadMethodEntry.syncContext);
					Control.InvokeMarshaledCallbackDo(threadMethodEntry);
					return;
				}
				finally
				{
					SynchronizationContext.SetSynchronizationContext(synchronizationContext);
				}
			}
			Control.InvokeMarshaledCallbackDo(threadMethodEntry);
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00034938 File Offset: 0x00032B38
		private static void InvokeMarshaledCallbackDo(Control.ThreadMethodEntry tme)
		{
			if (tme.method is EventHandler)
			{
				if (tme.args == null || tme.args.Length < 1)
				{
					((EventHandler)tme.method)(tme.caller, EventArgs.Empty);
					return;
				}
				if (tme.args.Length < 2)
				{
					((EventHandler)tme.method)(tme.args[0], EventArgs.Empty);
					return;
				}
				((EventHandler)tme.method)(tme.args[0], (EventArgs)tme.args[1]);
				return;
			}
			else
			{
				if (tme.method is MethodInvoker)
				{
					((MethodInvoker)tme.method)();
					return;
				}
				if (tme.method is WaitCallback)
				{
					((WaitCallback)tme.method)(tme.args[0]);
					return;
				}
				tme.retVal = tme.method.DynamicInvoke(tme.args);
				return;
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00034A2C File Offset: 0x00032C2C
		private void InvokeMarshaledCallbacks()
		{
			Control.ThreadMethodEntry threadMethodEntry = null;
			Queue queue = this.threadCallbackList;
			lock (queue)
			{
				if (this.threadCallbackList.Count > 0)
				{
					threadMethodEntry = (Control.ThreadMethodEntry)this.threadCallbackList.Dequeue();
				}
				goto IL_E8;
			}
			IL_41:
			if (threadMethodEntry.method != null)
			{
				try
				{
					if (NativeWindow.WndProcShouldBeDebuggable && !threadMethodEntry.synchronous)
					{
						this.InvokeMarshaledCallback(threadMethodEntry);
					}
					else
					{
						try
						{
							this.InvokeMarshaledCallback(threadMethodEntry);
						}
						catch (Exception ex)
						{
							threadMethodEntry.exception = ex.GetBaseException();
						}
					}
				}
				finally
				{
					threadMethodEntry.Complete();
					if (!NativeWindow.WndProcShouldBeDebuggable && threadMethodEntry.exception != null && !threadMethodEntry.synchronous)
					{
						Application.OnThreadException(threadMethodEntry.exception);
					}
				}
			}
			Queue queue2 = this.threadCallbackList;
			lock (queue2)
			{
				if (this.threadCallbackList.Count > 0)
				{
					threadMethodEntry = (Control.ThreadMethodEntry)this.threadCallbackList.Dequeue();
				}
				else
				{
					threadMethodEntry = null;
				}
			}
			IL_E8:
			if (threadMethodEntry == null)
			{
				return;
			}
			goto IL_41;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event for the specified control.</summary>
		/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> to assign the <see cref="E:System.Windows.Forms.Control.Paint" /> event to.</param>
		/// <param name="e">An <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x0600112F RID: 4399 RVA: 0x00034B5C File Offset: 0x00032D5C
		protected void InvokePaint(Control c, PaintEventArgs e)
		{
			c.OnPaint(e);
		}

		/// <summary>Raises the <see langword="PaintBackground" /> event for the specified control.</summary>
		/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> to assign the <see cref="E:System.Windows.Forms.Control.Paint" /> event to.</param>
		/// <param name="e">An <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06001130 RID: 4400 RVA: 0x00034B65 File Offset: 0x00032D65
		protected void InvokePaintBackground(Control c, PaintEventArgs e)
		{
			c.OnPaintBackground(e);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00034B70 File Offset: 0x00032D70
		internal bool IsFontSet()
		{
			return (Font)this.Properties.GetObject(Control.PropFont) != null;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00034B9C File Offset: 0x00032D9C
		internal bool IsDescendant(Control descendant)
		{
			for (Control control = descendant; control != null; control = control.ParentInternal)
			{
				if (control == this)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the CAPS LOCK, NUM LOCK, or SCROLL LOCK key is in effect.</summary>
		/// <param name="keyVal">The CAPS LOCK, NUM LOCK, or SCROLL LOCK member of the <see cref="T:System.Windows.Forms.Keys" /> enumeration.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key or keys are in effect; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="keyVal" /> parameter refers to a key other than the CAPS LOCK, NUM LOCK, or SCROLL LOCK key.</exception>
		// Token: 0x06001133 RID: 4403 RVA: 0x00034BC0 File Offset: 0x00032DC0
		public static bool IsKeyLocked(Keys keyVal)
		{
			if (keyVal != Keys.Insert && keyVal != Keys.NumLock && keyVal != Keys.Capital && keyVal != Keys.Scroll)
			{
				throw new NotSupportedException(SR.GetString("ControlIsKeyLockedNumCapsScrollLockKeysSupportedOnly"));
			}
			int keyState = (int)UnsafeNativeMethods.GetKeyState((int)keyVal);
			if (keyVal == Keys.Insert || keyVal == Keys.Capital)
			{
				return (keyState & 1) != 0;
			}
			return (keyState & 32769) != 0;
		}

		/// <summary>Determines if a character is an input character that the control recognizes.</summary>
		/// <param name="charCode">The character to test.</param>
		/// <returns>
		///   <see langword="true" /> if the character should be sent directly to the control and not preprocessed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001134 RID: 4404 RVA: 0x00034C1C File Offset: 0x00032E1C
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected virtual bool IsInputChar(char charCode)
		{
			int num;
			if (charCode == '\t')
			{
				num = 134;
			}
			else
			{
				num = 132;
			}
			return ((int)(long)this.SendMessage(135, 0, 0) & num) != 0;
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key is a regular input key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001135 RID: 4405 RVA: 0x00034C58 File Offset: 0x00032E58
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected virtual bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return false;
			}
			int num = 4;
			Keys keys = keyData & Keys.KeyCode;
			if (keys != Keys.Tab)
			{
				if (keys - Keys.Left <= 3)
				{
					num = 5;
				}
			}
			else
			{
				num = 6;
			}
			return this.IsHandleCreated && ((int)(long)this.SendMessage(135, 0, 0) & num) != 0;
		}

		/// <summary>Determines if the specified character is the mnemonic character assigned to the control in the specified string.</summary>
		/// <param name="charCode">The character to test.</param>
		/// <param name="text">The string to search.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="charCode" /> character is the mnemonic character assigned to the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001136 RID: 4406 RVA: 0x00034CB8 File Offset: 0x00032EB8
		public static bool IsMnemonic(char charCode, string text)
		{
			if (charCode == '&')
			{
				return false;
			}
			if (text != null)
			{
				int num = -1;
				char c = char.ToUpper(charCode, CultureInfo.CurrentCulture);
				while (num + 1 < text.Length)
				{
					num = text.IndexOf('&', num + 1) + 1;
					if (num <= 0 || num >= text.Length)
					{
						break;
					}
					char c2 = char.ToUpper(text[num], CultureInfo.CurrentCulture);
					if (c2 == c || char.ToLower(c2, CultureInfo.CurrentCulture) == char.ToLower(c, CultureInfo.CurrentCulture))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00034D34 File Offset: 0x00032F34
		private void ListenToUserPreferenceChanged(bool listen)
		{
			if (this.GetState2(4))
			{
				if (!listen)
				{
					this.SetState2(4, false);
					SystemEvents.UserPreferenceChanged -= this.UserPreferenceChanged;
					return;
				}
			}
			else if (listen)
			{
				this.SetState2(4, true);
				SystemEvents.UserPreferenceChanged += this.UserPreferenceChanged;
			}
		}

		/// <summary>Converts a Logical DPI value to its equivalent DeviceUnit DPI value.</summary>
		/// <param name="value">The Logical value to convert.</param>
		/// <returns>The resulting DeviceUnit value.</returns>
		// Token: 0x06001138 RID: 4408 RVA: 0x00034D83 File Offset: 0x00032F83
		public int LogicalToDeviceUnits(int value)
		{
			return DpiHelper.LogicalToDeviceUnits(value, this.DeviceDpi);
		}

		/// <summary>Transforms a size from logical to device units by scaling it for the current DPI and rounding down to the nearest integer value for width and height.</summary>
		/// <param name="value">The size to be scaled.</param>
		/// <returns>The scaled size.</returns>
		// Token: 0x06001139 RID: 4409 RVA: 0x00034D91 File Offset: 0x00032F91
		public Size LogicalToDeviceUnits(Size value)
		{
			return DpiHelper.LogicalToDeviceUnits(value, this.DeviceDpi);
		}

		/// <summary>Scales a logical bitmap value to it's equivalent device unit value when a DPI change occurs.</summary>
		/// <param name="logicalBitmap">The bitmap to scale.</param>
		// Token: 0x0600113A RID: 4410 RVA: 0x00034D9F File Offset: 0x00032F9F
		public void ScaleBitmapLogicalToDevice(ref Bitmap logicalBitmap)
		{
			DpiHelper.ScaleBitmapLogicalToDevice(ref logicalBitmap, this.DeviceDpi);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00034DAD File Offset: 0x00032FAD
		internal void AdjustWindowRectEx(ref NativeMethods.RECT rect, int style, bool bMenu, int exStyle)
		{
			if (DpiHelper.EnableDpiChangedMessageHandling)
			{
				SafeNativeMethods.AdjustWindowRectExForDpi(ref rect, style, bMenu, exStyle, (uint)this.deviceDpi);
				return;
			}
			SafeNativeMethods.AdjustWindowRectEx(ref rect, style, bMenu, exStyle);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00034DD4 File Offset: 0x00032FD4
		private object MarshaledInvoke(Control caller, Delegate method, object[] args, bool synchronous)
		{
			if (!this.IsHandleCreated)
			{
				throw new InvalidOperationException(SR.GetString("ErrorNoMarshalingThread"));
			}
			Control.ActiveXImpl activeXImpl = (Control.ActiveXImpl)this.Properties.GetObject(Control.PropActiveXImpl);
			if (activeXImpl != null)
			{
				IntSecurity.UnmanagedCode.Demand();
			}
			bool flag = false;
			int num;
			if (SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this, this.Handle), out num) == SafeNativeMethods.GetCurrentThreadId() && synchronous)
			{
				flag = true;
			}
			ExecutionContext executionContext = null;
			if (!flag)
			{
				executionContext = ExecutionContext.Capture();
			}
			Control.ThreadMethodEntry threadMethodEntry = new Control.ThreadMethodEntry(caller, this, method, args, synchronous, executionContext);
			lock (this)
			{
				if (this.threadCallbackList == null)
				{
					this.threadCallbackList = new Queue();
				}
			}
			Queue queue = this.threadCallbackList;
			lock (queue)
			{
				if (Control.threadCallbackMessage == 0)
				{
					Control.threadCallbackMessage = SafeNativeMethods.RegisterWindowMessage(Application.WindowMessagesVersion + "_ThreadCallbackMessage");
				}
				this.threadCallbackList.Enqueue(threadMethodEntry);
			}
			if (flag)
			{
				this.InvokeMarshaledCallbacks();
			}
			else
			{
				UnsafeNativeMethods.PostMessage(new HandleRef(this, this.Handle), Control.threadCallbackMessage, IntPtr.Zero, IntPtr.Zero);
			}
			if (!synchronous)
			{
				return threadMethodEntry;
			}
			if (!threadMethodEntry.IsCompleted)
			{
				this.WaitForWaitHandle(threadMethodEntry.AsyncWaitHandle);
			}
			if (threadMethodEntry.exception != null)
			{
				throw threadMethodEntry.exception;
			}
			return threadMethodEntry.retVal;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00034F54 File Offset: 0x00033154
		private void MarshalStringToMessage(string value, ref Message m)
		{
			if (m.LParam == IntPtr.Zero)
			{
				m.Result = (IntPtr)((value.Length + 1) * Marshal.SystemDefaultCharSize);
				return;
			}
			if ((int)(long)m.WParam < value.Length + 1)
			{
				m.Result = (IntPtr)(-1);
				return;
			}
			char[] array = new char[1];
			byte[] array2;
			byte[] array3;
			if (Marshal.SystemDefaultCharSize == 1)
			{
				array2 = Encoding.Default.GetBytes(value);
				array3 = Encoding.Default.GetBytes(array);
			}
			else
			{
				array2 = Encoding.Unicode.GetBytes(value);
				array3 = Encoding.Unicode.GetBytes(array);
			}
			Marshal.Copy(array2, 0, m.LParam, array2.Length);
			Marshal.Copy(array3, 0, (IntPtr)((long)m.LParam + (long)array2.Length), array3.Length);
			m.Result = (IntPtr)((array2.Length + array3.Length) / Marshal.SystemDefaultCharSize);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00035038 File Offset: 0x00033238
		internal void NotifyEnter()
		{
			this.OnEnter(EventArgs.Empty);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00035045 File Offset: 0x00033245
		internal void NotifyLeave()
		{
			this.OnLeave(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Invalidated" /> event with a specified region of the control to invalidate.</summary>
		/// <param name="invalidatedArea">A <see cref="T:System.Drawing.Rectangle" /> representing the area to invalidate.</param>
		// Token: 0x06001140 RID: 4416 RVA: 0x00035052 File Offset: 0x00033252
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void NotifyInvalidate(Rectangle invalidatedArea)
		{
			this.OnInvalidated(new InvalidateEventArgs(invalidatedArea));
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00035060 File Offset: 0x00033260
		private bool NotifyValidating()
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			this.OnValidating(cancelEventArgs);
			return cancelEventArgs.Cancel;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00035080 File Offset: 0x00033280
		private void NotifyValidated()
		{
			this.OnValidated(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event for the specified control.</summary>
		/// <param name="toInvoke">The <see cref="T:System.Windows.Forms.Control" /> to assign the <see cref="E:System.Windows.Forms.Control.Click" /> event to.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001143 RID: 4419 RVA: 0x0003508D File Offset: 0x0003328D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void InvokeOnClick(Control toInvoke, EventArgs e)
		{
			if (toInvoke != null)
			{
				toInvoke.OnClick(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.AutoSizeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001144 RID: 4420 RVA: 0x0003509C File Offset: 0x0003329C
		protected virtual void OnAutoSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventAutoSizeChanged] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001145 RID: 4421 RVA: 0x000350CC File Offset: 0x000332CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnBackColorChanged(EventArgs e)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			object @object = this.Properties.GetObject(Control.PropBackBrush);
			if (@object != null)
			{
				if (this.GetState(2097152))
				{
					IntPtr intPtr = (IntPtr)@object;
					if (intPtr != IntPtr.Zero)
					{
						SafeNativeMethods.DeleteObject(new HandleRef(this, intPtr));
					}
				}
				this.Properties.SetObject(Control.PropBackBrush, null);
			}
			this.Invalidate();
			EventHandler eventHandler = base.Events[Control.EventBackColor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentBackColorChanged(e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001146 RID: 4422 RVA: 0x00035198 File Offset: 0x00033398
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnBackgroundImageChanged(EventArgs e)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			this.Invalidate();
			EventHandler eventHandler = base.Events[Control.EventBackgroundImage] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentBackgroundImageChanged(e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageLayoutChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001147 RID: 4423 RVA: 0x0003520C File Offset: 0x0003340C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnBackgroundImageLayoutChanged(EventArgs e)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			this.Invalidate();
			EventHandler eventHandler = base.Events[Control.EventBackgroundImageLayout] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BindingContextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001148 RID: 4424 RVA: 0x0003524C File Offset: 0x0003344C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnBindingContextChanged(EventArgs e)
		{
			if (this.Properties.GetObject(Control.PropBindings) != null)
			{
				this.UpdateBindings();
			}
			EventHandler eventHandler = base.Events[Control.EventBindingContext] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentBindingContextChanged(e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.CausesValidationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001149 RID: 4425 RVA: 0x000352CC File Offset: 0x000334CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnCausesValidationChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventCausesValidation] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000352FA File Offset: 0x000334FA
		internal virtual void OnChildLayoutResuming(Control child, bool performLayout)
		{
			if (this.ParentInternal != null)
			{
				this.ParentInternal.OnChildLayoutResuming(child, performLayout);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ContextMenuChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600114B RID: 4427 RVA: 0x00035314 File Offset: 0x00033514
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnContextMenuChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventContextMenu] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ContextMenuStripChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600114C RID: 4428 RVA: 0x00035344 File Offset: 0x00033544
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnContextMenuStripChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventContextMenuStrip] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.CursorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600114D RID: 4429 RVA: 0x00035374 File Offset: 0x00033574
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnCursorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventCursor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentCursorChanged(e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DockChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600114E RID: 4430 RVA: 0x000353DC File Offset: 0x000335DC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDockChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventDock] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600114F RID: 4431 RVA: 0x0003540C File Offset: 0x0003360C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnEnabledChanged(EventArgs e)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			if (this.IsHandleCreated)
			{
				SafeNativeMethods.EnableWindow(new HandleRef(this, this.Handle), this.Enabled);
				if (this.GetStyle(ControlStyles.UserPaint))
				{
					this.Invalidate();
					this.Update();
				}
			}
			EventHandler eventHandler = base.Events[Control.EventEnabled] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentEnabledChanged(e);
				}
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnFrameWindowActivate(bool fActivate)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001151 RID: 4433 RVA: 0x000354B0 File Offset: 0x000336B0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFontChanged(EventArgs e)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			this.Invalidate();
			if (this.Properties.ContainsInteger(Control.PropFontHeight))
			{
				this.Properties.SetInteger(Control.PropFontHeight, -1);
			}
			this.DisposeFontHandle();
			if (this.IsHandleCreated && !this.GetStyle(ControlStyles.UserPaint))
			{
				this.SetWindowFont();
			}
			EventHandler eventHandler = base.Events[Control.EventFont] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			using (new LayoutTransaction(this, this, PropertyNames.Font, false))
			{
				if (controlCollection != null)
				{
					for (int i = 0; i < controlCollection.Count; i++)
					{
						controlCollection[i].OnParentFontChanged(e);
					}
				}
			}
			LayoutTransaction.DoLayout(this, this, PropertyNames.Font);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001152 RID: 4434 RVA: 0x0003559C File Offset: 0x0003379C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnForeColorChanged(EventArgs e)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			this.Invalidate();
			EventHandler eventHandler = base.Events[Control.EventForeColor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentForeColorChanged(e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001153 RID: 4435 RVA: 0x00035610 File Offset: 0x00033810
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftChanged(EventArgs e)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			this.SetState2(2, true);
			this.RecreateHandle();
			EventHandler eventHandler = base.Events[Control.EventRightToLeft] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentRightToLeftChanged(e);
				}
			}
		}

		/// <summary>Notifies the control of Windows messages.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> that represents the Windows message.</param>
		// Token: 0x06001154 RID: 4436 RVA: 0x000070A6 File Offset: 0x000052A6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnNotifyMessage(Message m)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event when the <see cref="P:System.Windows.Forms.Control.BackColor" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001155 RID: 4437 RVA: 0x0003568C File Offset: 0x0003388C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentBackColorChanged(EventArgs e)
		{
			if (this.Properties.GetColor(Control.PropBackColor).IsEmpty)
			{
				this.OnBackColorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageChanged" /> event when the <see cref="P:System.Windows.Forms.Control.BackgroundImage" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001156 RID: 4438 RVA: 0x000356BA File Offset: 0x000338BA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentBackgroundImageChanged(EventArgs e)
		{
			this.OnBackgroundImageChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BindingContextChanged" /> event when the <see cref="P:System.Windows.Forms.Control.BindingContext" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001157 RID: 4439 RVA: 0x000356C3 File Offset: 0x000338C3
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentBindingContextChanged(EventArgs e)
		{
			if (this.Properties.GetObject(Control.PropBindingManager) == null)
			{
				this.OnBindingContextChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.CursorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001158 RID: 4440 RVA: 0x000356DE File Offset: 0x000338DE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentCursorChanged(EventArgs e)
		{
			if (this.Properties.GetObject(Control.PropCursor) == null)
			{
				this.OnCursorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event when the <see cref="P:System.Windows.Forms.Control.Enabled" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001159 RID: 4441 RVA: 0x000356F9 File Offset: 0x000338F9
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentEnabledChanged(EventArgs e)
		{
			if (this.GetState(4))
			{
				this.OnEnabledChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event when the <see cref="P:System.Windows.Forms.Control.Font" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600115A RID: 4442 RVA: 0x0003570B File Offset: 0x0003390B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentFontChanged(EventArgs e)
		{
			if (this.Properties.GetObject(Control.PropFont) == null)
			{
				this.OnFontChanged(e);
			}
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00035728 File Offset: 0x00033928
		internal virtual void OnParentHandleRecreated()
		{
			Control parentInternal = this.ParentInternal;
			if (parentInternal != null && this.IsHandleCreated)
			{
				UnsafeNativeMethods.SetParent(new HandleRef(this, this.Handle), new HandleRef(parentInternal, parentInternal.Handle));
				this.UpdateZOrder();
			}
			this.SetState(536870912, false);
			if (this.ReflectParent == this.ParentInternal)
			{
				this.RecreateHandle();
			}
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0003578B File Offset: 0x0003398B
		internal virtual void OnParentHandleRecreating()
		{
			this.SetState(536870912, true);
			if (this.IsHandleCreated)
			{
				Application.ParkHandle(new HandleRef(this, this.Handle), this.DpiAwarenessContext);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event when the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600115D RID: 4445 RVA: 0x000357B8 File Offset: 0x000339B8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentForeColorChanged(EventArgs e)
		{
			if (this.Properties.GetColor(Control.PropForeColor).IsEmpty)
			{
				this.OnForeColorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event when the <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600115E RID: 4446 RVA: 0x000357E6 File Offset: 0x000339E6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentRightToLeftChanged(EventArgs e)
		{
			if (!this.Properties.ContainsInteger(Control.PropRightToLeft) || this.Properties.GetInteger(Control.PropRightToLeft) == 2)
			{
				this.OnRightToLeftChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event when the <see cref="P:System.Windows.Forms.Control.Visible" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600115F RID: 4447 RVA: 0x00035814 File Offset: 0x00033A14
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentVisibleChanged(EventArgs e)
		{
			if (this.GetState(2))
			{
				this.OnVisibleChanged(e);
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00035828 File Offset: 0x00033A28
		internal virtual void OnParentBecameInvisible()
		{
			if (this.GetState(2))
			{
				Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
				if (controlCollection != null)
				{
					for (int i = 0; i < controlCollection.Count; i++)
					{
						Control control = controlCollection[i];
						control.OnParentBecameInvisible();
					}
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001161 RID: 4449 RVA: 0x00035878 File Offset: 0x00033A78
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnPrint(PaintEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (this.GetStyle(ControlStyles.UserPaint))
			{
				this.PaintWithErrorHandling(e, 1);
				e.ResetGraphics();
				this.PaintWithErrorHandling(e, 2);
				return;
			}
			Control.PrintPaintEventArgs printPaintEventArgs = e as Control.PrintPaintEventArgs;
			bool flag = false;
			IntPtr intPtr = IntPtr.Zero;
			Message message;
			if (printPaintEventArgs == null)
			{
				IntPtr intPtr2 = (IntPtr)30;
				intPtr = e.HDC;
				if (intPtr == IntPtr.Zero)
				{
					intPtr = e.Graphics.GetHdc();
					flag = true;
				}
				message = Message.Create(this.Handle, 792, intPtr, intPtr2);
			}
			else
			{
				message = printPaintEventArgs.Message;
			}
			try
			{
				this.DefWndProc(ref message);
			}
			finally
			{
				if (flag)
				{
					e.Graphics.ReleaseHdcInternal(intPtr);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TabIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001162 RID: 4450 RVA: 0x00035938 File Offset: 0x00033B38
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnTabIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventTabIndex] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TabStopChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001163 RID: 4451 RVA: 0x00035968 File Offset: 0x00033B68
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnTabStopChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventTabStop] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001164 RID: 4452 RVA: 0x00035998 File Offset: 0x00033B98
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventText] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001165 RID: 4453 RVA: 0x000359C8 File Offset: 0x00033BC8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnVisibleChanged(EventArgs e)
		{
			bool visible = this.Visible;
			if (visible)
			{
				this.UnhookMouseEvent();
				this.trackMouseEvent = null;
			}
			if (this.parent != null && visible && !this.Created && !this.GetAnyDisposingInHierarchy())
			{
				this.CreateControl();
			}
			EventHandler eventHandler = base.Events[Control.EventVisible] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					Control control = controlCollection[i];
					if (control.Visible)
					{
						control.OnParentVisibleChanged(e);
					}
					if (!visible)
					{
						control.OnParentBecameInvisible();
					}
				}
			}
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00035A88 File Offset: 0x00033C88
		internal virtual void OnTopMostActiveXParentChanged(EventArgs e)
		{
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnTopMostActiveXParentChanged(e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001167 RID: 4455 RVA: 0x00035ACC File Offset: 0x00033CCC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventParent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (this.TopMostParent.IsActiveX)
			{
				this.OnTopMostActiveXParentChanged(EventArgs.Empty);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001168 RID: 4456 RVA: 0x00035B14 File Offset: 0x00033D14
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ClientSizeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001169 RID: 4457 RVA: 0x00035B44 File Offset: 0x00033D44
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClientSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventClientSize] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ControlAdded" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data.</param>
		// Token: 0x0600116A RID: 4458 RVA: 0x00035B74 File Offset: 0x00033D74
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnControlAdded(ControlEventArgs e)
		{
			ControlEventHandler controlEventHandler = (ControlEventHandler)base.Events[Control.EventControlAdded];
			if (controlEventHandler != null)
			{
				controlEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ControlRemoved" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data.</param>
		// Token: 0x0600116B RID: 4459 RVA: 0x00035BA4 File Offset: 0x00033DA4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnControlRemoved(ControlEventArgs e)
		{
			ControlEventHandler controlEventHandler = (ControlEventHandler)base.Events[Control.EventControlRemoved];
			if (controlEventHandler != null)
			{
				controlEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.CreateControl" /> method.</summary>
		// Token: 0x0600116C RID: 4460 RVA: 0x000070A6 File Offset: 0x000052A6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnCreateControl()
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600116D RID: 4461 RVA: 0x00035BD4 File Offset: 0x00033DD4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnHandleCreated(EventArgs e)
		{
			if (this.IsHandleCreated)
			{
				if (!this.GetStyle(ControlStyles.UserPaint))
				{
					this.SetWindowFont();
				}
				if (DpiHelper.EnableDpiChangedMessageHandling && !typeof(Form).IsAssignableFrom(base.GetType()))
				{
					int num = this.deviceDpi;
					this.deviceDpi = (int)UnsafeNativeMethods.GetDpiForWindow(new HandleRef(this, this.HandleInternal));
					if (num != this.deviceDpi)
					{
						this.RescaleConstantsForDpi(num, this.deviceDpi);
					}
				}
				this.SetAcceptDrops(this.AllowDrop);
				Region region = (Region)this.Properties.GetObject(Control.PropRegion);
				if (region != null)
				{
					IntPtr intPtr = this.GetHRgn(region);
					try
					{
						if (this.IsActiveX)
						{
							intPtr = this.ActiveXMergeRegion(intPtr);
						}
						if (UnsafeNativeMethods.SetWindowRgn(new HandleRef(this, this.Handle), new HandleRef(this, intPtr), SafeNativeMethods.IsWindowVisible(new HandleRef(this, this.Handle))) != 0)
						{
							intPtr = IntPtr.Zero;
						}
					}
					finally
					{
						if (intPtr != IntPtr.Zero)
						{
							SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
						}
					}
				}
				Control.ControlAccessibleObject controlAccessibleObject = this.Properties.GetObject(Control.PropAccessibility) as Control.ControlAccessibleObject;
				Control.ControlAccessibleObject controlAccessibleObject2 = this.Properties.GetObject(Control.PropNcAccessibility) as Control.ControlAccessibleObject;
				IntPtr handle = this.Handle;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					if (controlAccessibleObject != null)
					{
						controlAccessibleObject.Handle = handle;
					}
					if (controlAccessibleObject2 != null)
					{
						controlAccessibleObject2.Handle = handle;
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (this.text != null && this.text.Length != 0)
				{
					UnsafeNativeMethods.SetWindowText(new HandleRef(this, this.Handle), this.text);
				}
				if (!(this is ScrollableControl) && !this.IsMirrored && this.GetState2(2) && !this.GetState2(1))
				{
					this.BeginInvoke(new EventHandler(this.OnSetScrollPosition));
					this.SetState2(1, true);
					this.SetState2(2, false);
				}
				if (this.GetState2(8))
				{
					this.ListenToUserPreferenceChanged(this.GetTopLevel());
				}
			}
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventHandleCreated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (this.IsHandleCreated && this.GetState(32768))
			{
				UnsafeNativeMethods.PostMessage(new HandleRef(this, this.Handle), Control.threadCallbackMessage, IntPtr.Zero, IntPtr.Zero);
				this.SetState(32768, false);
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00035E44 File Offset: 0x00034044
		private void OnSetScrollPosition(object sender, EventArgs e)
		{
			this.SetState2(1, false);
			this.OnInvokedSetScrollPosition(sender, e);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00035E58 File Offset: 0x00034058
		internal virtual void OnInvokedSetScrollPosition(object sender, EventArgs e)
		{
			if (!(this is ScrollableControl) && !this.IsMirrored)
			{
				NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO();
				scrollinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.SCROLLINFO));
				scrollinfo.fMask = 1;
				if (UnsafeNativeMethods.GetScrollInfo(new HandleRef(this, this.Handle), 0, scrollinfo))
				{
					scrollinfo.nPos = ((this.RightToLeft == RightToLeft.Yes) ? scrollinfo.nMax : scrollinfo.nMin);
					this.SendMessage(276, NativeMethods.Util.MAKELPARAM(4, scrollinfo.nPos), 0);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LocationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001170 RID: 4464 RVA: 0x00035EE4 File Offset: 0x000340E4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLocationChanged(EventArgs e)
		{
			this.OnMove(EventArgs.Empty);
			EventHandler eventHandler = base.Events[Control.EventLocation] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001171 RID: 4465 RVA: 0x00035F20 File Offset: 0x00034120
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnHandleDestroyed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventHandleDestroyed];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			this.UpdateReflectParent(false);
			if (!this.RecreatingHandle)
			{
				if (this.GetState(2097152))
				{
					object @object = this.Properties.GetObject(Control.PropBackBrush);
					if (@object != null)
					{
						IntPtr intPtr = (IntPtr)@object;
						if (intPtr != IntPtr.Zero)
						{
							SafeNativeMethods.DeleteObject(new HandleRef(this, intPtr));
						}
						this.Properties.SetObject(Control.PropBackBrush, null);
					}
				}
				this.ListenToUserPreferenceChanged(false);
			}
			try
			{
				if (!this.GetAnyDisposingInHierarchy())
				{
					this.text = this.Text;
					if (this.text != null && this.text.Length == 0)
					{
						this.text = null;
					}
				}
				this.SetAcceptDrops(false);
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DoubleClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001172 RID: 4466 RVA: 0x00036010 File Offset: 0x00034210
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDoubleClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventDoubleClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragEnter" /> event.</summary>
		/// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x06001173 RID: 4467 RVA: 0x00036040 File Offset: 0x00034240
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragEnter(DragEventArgs drgevent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[Control.EventDragEnter];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, drgevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragOver" /> event.</summary>
		/// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x06001174 RID: 4468 RVA: 0x00036070 File Offset: 0x00034270
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragOver(DragEventArgs drgevent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[Control.EventDragOver];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, drgevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001175 RID: 4469 RVA: 0x000360A0 File Offset: 0x000342A0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragLeave(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventDragLeave];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragDrop" /> event.</summary>
		/// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x06001176 RID: 4470 RVA: 0x000360D0 File Offset: 0x000342D0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragDrop(DragEventArgs drgevent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[Control.EventDragDrop];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, drgevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GiveFeedback" /> event.</summary>
		/// <param name="gfbevent">A <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> that contains the event data.</param>
		// Token: 0x06001177 RID: 4471 RVA: 0x00036100 File Offset: 0x00034300
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
		{
			GiveFeedbackEventHandler giveFeedbackEventHandler = (GiveFeedbackEventHandler)base.Events[Control.EventGiveFeedback];
			if (giveFeedbackEventHandler != null)
			{
				giveFeedbackEventHandler(this, gfbevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001178 RID: 4472 RVA: 0x00036130 File Offset: 0x00034330
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnEnter(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventEnter];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event for the specified control.</summary>
		/// <param name="toInvoke">The <see cref="T:System.Windows.Forms.Control" /> to assign the event to.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001179 RID: 4473 RVA: 0x0003615E File Offset: 0x0003435E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void InvokeGotFocus(Control toInvoke, EventArgs e)
		{
			if (toInvoke != null)
			{
				toInvoke.OnGotFocus(e);
				if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
				{
					KeyboardToolTipStateMachine.Instance.NotifyAboutGotFocus(toInvoke);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600117A RID: 4474 RVA: 0x0003617C File Offset: 0x0003437C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnGotFocus(EventArgs e)
		{
			if (this.IsActiveX)
			{
				this.ActiveXOnFocus(true);
			}
			if (this.parent != null)
			{
				this.parent.ChildGotFocus(this);
			}
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventGotFocus];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HelpRequested" /> event.</summary>
		/// <param name="hevent">A <see cref="T:System.Windows.Forms.HelpEventArgs" /> that contains the event data.</param>
		// Token: 0x0600117B RID: 4475 RVA: 0x000361D0 File Offset: 0x000343D0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnHelpRequested(HelpEventArgs hevent)
		{
			HelpEventHandler helpEventHandler = (HelpEventHandler)base.Events[Control.EventHelpRequested];
			if (helpEventHandler != null)
			{
				helpEventHandler(this, hevent);
				hevent.Handled = true;
			}
			if (!hevent.Handled && this.ParentInternal != null)
			{
				this.ParentInternal.OnHelpRequested(hevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Invalidated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.InvalidateEventArgs" /> that contains the event data.</param>
		// Token: 0x0600117C RID: 4476 RVA: 0x00036224 File Offset: 0x00034424
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnInvalidated(InvalidateEventArgs e)
		{
			if (this.IsActiveX)
			{
				this.ActiveXViewChanged();
			}
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnParentInvalidated(e);
				}
			}
			InvalidateEventHandler invalidateEventHandler = (InvalidateEventHandler)base.Events[Control.EventInvalidated];
			if (invalidateEventHandler != null)
			{
				invalidateEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x0600117D RID: 4477 RVA: 0x00036298 File Offset: 0x00034498
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnKeyDown(KeyEventArgs e)
		{
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[Control.EventKeyDown];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		// Token: 0x0600117E RID: 4478 RVA: 0x000362C8 File Offset: 0x000344C8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnKeyPress(KeyPressEventArgs e)
		{
			KeyPressEventHandler keyPressEventHandler = (KeyPressEventHandler)base.Events[Control.EventKeyPress];
			if (keyPressEventHandler != null)
			{
				keyPressEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x0600117F RID: 4479 RVA: 0x000362F8 File Offset: 0x000344F8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnKeyUp(KeyEventArgs e)
		{
			if (!AccessibilityImprovements.UseLegacyToolTipDisplay && OsVersion.IsWindows11_OrGreater && (e.KeyCode.HasFlag(Keys.ControlKey) || e.KeyCode == Keys.Escape))
			{
				KeyboardToolTipStateMachine.HidePersistentTooltip();
			}
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[Control.EventKeyUp];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06001180 RID: 4480 RVA: 0x0003635C File Offset: 0x0003455C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLayout(LayoutEventArgs levent)
		{
			if (this.IsActiveX)
			{
				this.ActiveXViewChanged();
			}
			LayoutEventHandler layoutEventHandler = (LayoutEventHandler)base.Events[Control.EventLayout];
			if (layoutEventHandler != null)
			{
				layoutEventHandler(this, levent);
			}
			bool flag = this.LayoutEngine.Layout(this, levent);
			if (flag && this.ParentInternal != null)
			{
				this.ParentInternal.SetState(8388608, true);
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x000363C2 File Offset: 0x000345C2
		internal virtual void OnLayoutResuming(bool performLayout)
		{
			if (this.ParentInternal != null)
			{
				this.ParentInternal.OnChildLayoutResuming(this, performLayout);
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnLayoutSuspended()
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001183 RID: 4483 RVA: 0x000363DC File Offset: 0x000345DC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLeave(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventLeave];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event for the specified control.</summary>
		/// <param name="toInvoke">The <see cref="T:System.Windows.Forms.Control" /> to assign the event to.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001184 RID: 4484 RVA: 0x0003640A File Offset: 0x0003460A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void InvokeLostFocus(Control toInvoke, EventArgs e)
		{
			if (toInvoke != null)
			{
				toInvoke.OnLostFocus(e);
				if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
				{
					KeyboardToolTipStateMachine.Instance.NotifyAboutLostFocus(toInvoke);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001185 RID: 4485 RVA: 0x00036428 File Offset: 0x00034628
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLostFocus(EventArgs e)
		{
			if (this.IsActiveX)
			{
				this.ActiveXOnFocus(false);
			}
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventLostFocus];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MarginChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001186 RID: 4486 RVA: 0x00036468 File Offset: 0x00034668
		protected virtual void OnMarginChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventMarginChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDoubleClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001187 RID: 4487 RVA: 0x00036498 File Offset: 0x00034698
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseDoubleClick(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.EventMouseDoubleClick];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001188 RID: 4488 RVA: 0x000364C8 File Offset: 0x000346C8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseClick(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.EventMouseClick];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseCaptureChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001189 RID: 4489 RVA: 0x000364F8 File Offset: 0x000346F8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseCaptureChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventMouseCaptureChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600118A RID: 4490 RVA: 0x00036528 File Offset: 0x00034728
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseDown(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.EventMouseDown];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600118B RID: 4491 RVA: 0x00036558 File Offset: 0x00034758
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseEnter(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventMouseEnter];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600118C RID: 4492 RVA: 0x00036588 File Offset: 0x00034788
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseLeave(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventMouseLeave];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DpiChangedBeforeParent" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.DpiChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x0600118D RID: 4493 RVA: 0x000365B6 File Offset: 0x000347B6
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnDpiChangedBeforeParent(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventDpiChangedBeforeParent];
			if (eventHandler == null)
			{
				return;
			}
			eventHandler(this, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DpiChangedAfterParent" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.DpiChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x0600118E RID: 4494 RVA: 0x000365D9 File Offset: 0x000347D9
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		protected virtual void OnDpiChangedAfterParent(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventDpiChangedAfterParent];
			if (eventHandler == null)
			{
				return;
			}
			eventHandler(this, e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseHover" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600118F RID: 4495 RVA: 0x000365FC File Offset: 0x000347FC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseHover(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventMouseHover];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001190 RID: 4496 RVA: 0x0003662C File Offset: 0x0003482C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseMove(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.EventMouseMove];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001191 RID: 4497 RVA: 0x0003665C File Offset: 0x0003485C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseUp(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.EventMouseUp];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001192 RID: 4498 RVA: 0x0003668C File Offset: 0x0003488C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseWheel(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.EventMouseWheel];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Move" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001193 RID: 4499 RVA: 0x000366BC File Offset: 0x000348BC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMove(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventMove];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (this.RenderTransparent)
			{
				this.Invalidate();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06001194 RID: 4500 RVA: 0x000366F8 File Offset: 0x000348F8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnPaint(PaintEventArgs e)
		{
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[Control.EventPaint];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001195 RID: 4501 RVA: 0x00036728 File Offset: 0x00034928
		protected virtual void OnPaddingChanged(EventArgs e)
		{
			if (this.GetStyle(ControlStyles.ResizeRedraw))
			{
				this.Invalidate();
			}
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventPaddingChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Paints the background of the control.</summary>
		/// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint.</param>
		// Token: 0x06001196 RID: 4502 RVA: 0x00036768 File Offset: 0x00034968
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnPaintBackground(PaintEventArgs pevent)
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetClientRect(new HandleRef(this.window, this.InternalHandle), ref rect);
			this.PaintBackground(pevent, new Rectangle(rect.left, rect.top, rect.right, rect.bottom));
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x000367BC File Offset: 0x000349BC
		private void OnParentInvalidated(InvalidateEventArgs e)
		{
			if (!this.RenderTransparent)
			{
				return;
			}
			if (this.IsHandleCreated)
			{
				Rectangle rectangle = e.InvalidRect;
				Point location = this.Location;
				rectangle.Offset(-location.X, -location.Y);
				rectangle = Rectangle.Intersect(this.ClientRectangle, rectangle);
				if (rectangle.IsEmpty)
				{
					return;
				}
				this.Invalidate(rectangle);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.QueryContinueDrag" /> event.</summary>
		/// <param name="qcdevent">A <see cref="T:System.Windows.Forms.QueryContinueDragEventArgs" /> that contains the event data.</param>
		// Token: 0x06001198 RID: 4504 RVA: 0x00036820 File Offset: 0x00034A20
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
		{
			QueryContinueDragEventHandler queryContinueDragEventHandler = (QueryContinueDragEventHandler)base.Events[Control.EventQueryContinueDrag];
			if (queryContinueDragEventHandler != null)
			{
				queryContinueDragEventHandler(this, qcdevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RegionChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001199 RID: 4505 RVA: 0x00036850 File Offset: 0x00034A50
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRegionChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Control.EventRegionChanged] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600119A RID: 4506 RVA: 0x00036880 File Offset: 0x00034A80
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnResize(EventArgs e)
		{
			if ((this.controlStyle & ControlStyles.ResizeRedraw) == ControlStyles.ResizeRedraw || this.GetState(4194304))
			{
				this.Invalidate();
			}
			LayoutTransaction.DoLayout(this, this, PropertyNames.Bounds);
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventResize];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PreviewKeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PreviewKeyDownEventArgs" /> that contains the event data.</param>
		// Token: 0x0600119B RID: 4507 RVA: 0x000368DC File Offset: 0x00034ADC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
		{
			PreviewKeyDownEventHandler previewKeyDownEventHandler = (PreviewKeyDownEventHandler)base.Events[Control.EventPreviewKeyDown];
			if (previewKeyDownEventHandler != null)
			{
				previewKeyDownEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600119C RID: 4508 RVA: 0x0003690C File Offset: 0x00034B0C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnSizeChanged(EventArgs e)
		{
			this.OnResize(EventArgs.Empty);
			EventHandler eventHandler = base.Events[Control.EventSize] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ChangeUICues" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.UICuesEventArgs" /> that contains the event data.</param>
		// Token: 0x0600119D RID: 4509 RVA: 0x00036948 File Offset: 0x00034B48
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnChangeUICues(UICuesEventArgs e)
		{
			UICuesEventHandler uicuesEventHandler = (UICuesEventHandler)base.Events[Control.EventChangeUICues];
			if (uicuesEventHandler != null)
			{
				uicuesEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.StyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600119E RID: 4510 RVA: 0x00036978 File Offset: 0x00034B78
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventStyleChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SystemColorsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600119F RID: 4511 RVA: 0x000369A8 File Offset: 0x00034BA8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnSystemColorsChanged(EventArgs e)
		{
			Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
			if (controlCollection != null)
			{
				for (int i = 0; i < controlCollection.Count; i++)
				{
					controlCollection[i].OnSystemColorsChanged(EventArgs.Empty);
				}
			}
			this.Invalidate();
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventSystemColorsChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x060011A0 RID: 4512 RVA: 0x00036A18 File Offset: 0x00034C18
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnValidating(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[Control.EventValidating];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060011A1 RID: 4513 RVA: 0x00036A48 File Offset: 0x00034C48
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnValidated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventValidated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Provides constants for rescaling the control when a DPI change occurs.</summary>
		/// <param name="deviceDpiOld">The DPI value prior to the change.</param>
		/// <param name="deviceDpiNew">The DPI value after the change.</param>
		// Token: 0x060011A2 RID: 4514 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00036A76 File Offset: 0x00034C76
		internal void PaintBackground(PaintEventArgs e, Rectangle rectangle)
		{
			this.PaintBackground(e, rectangle, this.BackColor, Point.Empty);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00036A8B File Offset: 0x00034C8B
		internal void PaintBackground(PaintEventArgs e, Rectangle rectangle, Color backColor)
		{
			this.PaintBackground(e, rectangle, backColor, Point.Empty);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00036A9C File Offset: 0x00034C9C
		internal void PaintBackground(PaintEventArgs e, Rectangle rectangle, Color backColor, Point scrollOffset)
		{
			if (this.RenderColorTransparent(backColor))
			{
				this.PaintTransparentBackground(e, rectangle);
			}
			bool flag = (this is Form || this is MdiClient) && this.IsMirrored;
			if (this.BackgroundImage != null && !DisplayInformation.HighContrast && !flag)
			{
				if (this.BackgroundImageLayout == ImageLayout.Tile && ControlPaint.IsImageTransparent(this.BackgroundImage))
				{
					this.PaintTransparentBackground(e, rectangle);
				}
				Point point = scrollOffset;
				ScrollableControl scrollableControl = this as ScrollableControl;
				if (scrollableControl != null && point != Point.Empty)
				{
					point = ((ScrollableControl)this).AutoScrollPosition;
				}
				if (ControlPaint.IsImageTransparent(this.BackgroundImage))
				{
					Control.PaintBackColor(e, rectangle, backColor);
				}
				ControlPaint.DrawBackgroundImage(e.Graphics, this.BackgroundImage, backColor, this.BackgroundImageLayout, this.ClientRectangle, rectangle, point, this.RightToLeft);
				return;
			}
			Control.PaintBackColor(e, rectangle, backColor);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00036B78 File Offset: 0x00034D78
		private static void PaintBackColor(PaintEventArgs e, Rectangle rectangle, Color backColor)
		{
			Color color = backColor;
			if (color.A == 255)
			{
				using (WindowsGraphics windowsGraphics = ((e.HDC != IntPtr.Zero && DisplayInformation.BitsPerPixel > 8) ? WindowsGraphics.FromHdc(e.HDC) : WindowsGraphics.FromGraphics(e.Graphics)))
				{
					color = windowsGraphics.GetNearestColor(color);
					using (WindowsBrush windowsBrush = new WindowsSolidBrush(windowsGraphics.DeviceContext, color))
					{
						windowsGraphics.FillRectangle(windowsBrush, rectangle);
						return;
					}
				}
			}
			if (color.A > 0)
			{
				using (Brush brush = new SolidBrush(color))
				{
					e.Graphics.FillRectangle(brush, rectangle);
				}
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00036C50 File Offset: 0x00034E50
		private void PaintException(PaintEventArgs e)
		{
			int num = 2;
			using (Pen pen = new Pen(Color.Red, (float)num))
			{
				Rectangle clientRectangle = this.ClientRectangle;
				Rectangle rectangle = clientRectangle;
				int num2 = rectangle.X;
				rectangle.X = num2 + 1;
				num2 = rectangle.Y;
				rectangle.Y = num2 + 1;
				num2 = rectangle.Width;
				rectangle.Width = num2 - 1;
				num2 = rectangle.Height;
				rectangle.Height = num2 - 1;
				e.Graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				rectangle.Inflate(-1, -1);
				e.Graphics.FillRectangle(Brushes.White, rectangle);
				e.Graphics.DrawLine(pen, clientRectangle.Left, clientRectangle.Top, clientRectangle.Right, clientRectangle.Bottom);
				e.Graphics.DrawLine(pen, clientRectangle.Left, clientRectangle.Bottom, clientRectangle.Right, clientRectangle.Top);
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00036D74 File Offset: 0x00034F74
		internal void PaintTransparentBackground(PaintEventArgs e, Rectangle rectangle)
		{
			this.PaintTransparentBackground(e, rectangle, null);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00036D80 File Offset: 0x00034F80
		internal void PaintTransparentBackground(PaintEventArgs e, Rectangle rectangle, Region transparentRegion)
		{
			Graphics graphics = e.Graphics;
			Control parentInternal = this.ParentInternal;
			if (parentInternal != null)
			{
				if (Application.RenderWithVisualStyles && parentInternal.RenderTransparencyWithVisualStyles)
				{
					GraphicsState graphicsState = null;
					if (transparentRegion != null)
					{
						graphicsState = graphics.Save();
					}
					try
					{
						if (transparentRegion != null)
						{
							graphics.Clip = transparentRegion;
						}
						ButtonRenderer.DrawParentBackground(graphics, rectangle, this);
						return;
					}
					finally
					{
						if (graphicsState != null)
						{
							graphics.Restore(graphicsState);
						}
					}
				}
				Rectangle rectangle2 = new Rectangle(-this.Left, -this.Top, parentInternal.Width, parentInternal.Height);
				Rectangle rectangle3 = new Rectangle(rectangle.Left + this.Left, rectangle.Top + this.Top, rectangle.Width, rectangle.Height);
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(graphics))
				{
					windowsGraphics.DeviceContext.TranslateTransform(-this.Left, -this.Top);
					using (PaintEventArgs paintEventArgs = new PaintEventArgs(windowsGraphics.GetHdc(), rectangle3))
					{
						if (transparentRegion != null)
						{
							paintEventArgs.Graphics.Clip = transparentRegion;
							paintEventArgs.Graphics.TranslateClip(-rectangle2.X, -rectangle2.Y);
						}
						try
						{
							this.InvokePaintBackground(parentInternal, paintEventArgs);
							this.InvokePaint(parentInternal, paintEventArgs);
							return;
						}
						finally
						{
							if (transparentRegion != null)
							{
								paintEventArgs.Graphics.TranslateClip(rectangle2.X, rectangle2.Y);
							}
						}
					}
				}
			}
			graphics.FillRectangle(SystemBrushes.Control, rectangle);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00036F1C File Offset: 0x0003511C
		private void PaintWithErrorHandling(PaintEventArgs e, short layer)
		{
			try
			{
				this.CacheTextInternal = true;
				if (this.GetState(4194304))
				{
					if (layer == 1)
					{
						this.PaintException(e);
					}
				}
				else
				{
					bool flag = true;
					try
					{
						if (layer != 1)
						{
							if (layer == 2)
							{
								this.OnPaint(e);
							}
						}
						else if (!this.GetStyle(ControlStyles.Opaque))
						{
							this.OnPaintBackground(e);
						}
						flag = false;
					}
					finally
					{
						if (flag)
						{
							this.SetState(4194304, true);
							this.Invalidate();
						}
					}
				}
			}
			finally
			{
				this.CacheTextInternal = false;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00036FB0 File Offset: 0x000351B0
		internal ContainerControl ParentContainerControl
		{
			get
			{
				for (Control control = this.ParentInternal; control != null; control = control.ParentInternal)
				{
					if (control is ContainerControl)
					{
						return control as ContainerControl;
					}
				}
				return null;
			}
		}

		/// <summary>Forces the control to apply layout logic to all its child controls.</summary>
		// Token: 0x060011AC RID: 4524 RVA: 0x00036FE0 File Offset: 0x000351E0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void PerformLayout()
		{
			if (this.cachedLayoutEventArgs != null)
			{
				this.PerformLayout(this.cachedLayoutEventArgs);
				this.cachedLayoutEventArgs = null;
				this.SetState2(64, false);
				return;
			}
			this.PerformLayout(null, null);
		}

		/// <summary>Forces the control to apply layout logic to all its child controls.</summary>
		/// <param name="affectedControl">A <see cref="T:System.Windows.Forms.Control" /> that represents the most recently changed control.</param>
		/// <param name="affectedProperty">The name of the most recently changed property on the control.</param>
		// Token: 0x060011AD RID: 4525 RVA: 0x0003700F File Offset: 0x0003520F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void PerformLayout(Control affectedControl, string affectedProperty)
		{
			this.PerformLayout(new LayoutEventArgs(affectedControl, affectedProperty));
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00037020 File Offset: 0x00035220
		internal void PerformLayout(LayoutEventArgs args)
		{
			if (this.GetAnyDisposingInHierarchy())
			{
				return;
			}
			if (this.layoutSuspendCount > 0)
			{
				this.SetState(512, true);
				if (this.cachedLayoutEventArgs == null || (this.GetState2(64) && args != null))
				{
					this.cachedLayoutEventArgs = args;
					if (this.GetState2(64))
					{
						this.SetState2(64, false);
					}
				}
				this.LayoutEngine.ProcessSuspendedLayoutEventArgs(this, args);
				return;
			}
			this.layoutSuspendCount = 1;
			try
			{
				this.CacheTextInternal = true;
				this.OnLayout(args);
			}
			finally
			{
				this.CacheTextInternal = false;
				this.SetState(8389120, false);
				this.layoutSuspendCount = 0;
				if (this.ParentInternal != null && this.ParentInternal.GetState(8388608))
				{
					LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.PreferredSize);
				}
			}
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x000370F8 File Offset: 0x000352F8
		internal bool PerformControlValidation(bool bulkValidation)
		{
			if (!this.CausesValidation)
			{
				return false;
			}
			if (this.NotifyValidating())
			{
				return true;
			}
			if (bulkValidation || NativeWindow.WndProcShouldBeDebuggable)
			{
				this.NotifyValidated();
			}
			else
			{
				try
				{
					this.NotifyValidated();
				}
				catch (Exception ex)
				{
					Application.OnThreadException(ex);
				}
			}
			return false;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00037150 File Offset: 0x00035350
		internal bool PerformContainerValidation(ValidationConstraints validationConstraints)
		{
			bool flag = false;
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				if ((validationConstraints & ValidationConstraints.ImmediateChildren) != ValidationConstraints.ImmediateChildren && control.ShouldPerformContainerValidation() && control.PerformContainerValidation(validationConstraints))
				{
					flag = true;
				}
				if (((validationConstraints & ValidationConstraints.Selectable) != ValidationConstraints.Selectable || control.GetStyle(ControlStyles.Selectable)) && ((validationConstraints & ValidationConstraints.Enabled) != ValidationConstraints.Enabled || control.Enabled) && ((validationConstraints & ValidationConstraints.Visible) != ValidationConstraints.Visible || control.Visible) && ((validationConstraints & ValidationConstraints.TabStop) != ValidationConstraints.TabStop || control.TabStop) && control.PerformControlValidation(true))
				{
					flag = true;
				}
			}
			return flag;
		}

		/// <summary>Computes the location of the specified screen point into client coordinates.</summary>
		/// <param name="p">The screen coordinate <see cref="T:System.Drawing.Point" /> to convert.</param>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the converted <see cref="T:System.Drawing.Point" />, <paramref name="p" />, in client coordinates.</returns>
		// Token: 0x060011B1 RID: 4529 RVA: 0x00037208 File Offset: 0x00035408
		public Point PointToClient(Point p)
		{
			return this.PointToClientInternal(p);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00037214 File Offset: 0x00035414
		internal Point PointToClientInternal(Point p)
		{
			NativeMethods.POINT point = new NativeMethods.POINT(p.X, p.Y);
			UnsafeNativeMethods.MapWindowPoints(NativeMethods.NullHandleRef, new HandleRef(this, this.Handle), point, 1);
			return new Point(point.x, point.y);
		}

		/// <summary>Computes the location of the specified client point into screen coordinates.</summary>
		/// <param name="p">The client coordinate <see cref="T:System.Drawing.Point" /> to convert.</param>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the converted <see cref="T:System.Drawing.Point" />, <paramref name="p" />, in screen coordinates.</returns>
		// Token: 0x060011B3 RID: 4531 RVA: 0x00037260 File Offset: 0x00035460
		public Point PointToScreen(Point p)
		{
			NativeMethods.POINT point = new NativeMethods.POINT(p.X, p.Y);
			UnsafeNativeMethods.MapWindowPoints(new HandleRef(this, this.Handle), NativeMethods.NullHandleRef, point, 1);
			return new Point(point.x, point.y);
		}

		/// <summary>Preprocesses keyboard or input messages within the message loop before they are dispatched.</summary>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the message to process. The possible values are WM_KEYDOWN, WM_SYSKEYDOWN, WM_CHAR, and WM_SYSCHAR.</param>
		/// <returns>
		///   <see langword="true" /> if the message was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011B4 RID: 4532 RVA: 0x000372AC File Offset: 0x000354AC
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public virtual bool PreProcessMessage(ref Message msg)
		{
			if (msg.Msg == 256 || msg.Msg == 260)
			{
				if (!this.GetState2(512))
				{
					this.ProcessUICues(ref msg);
				}
				Keys keys = (Keys)(long)msg.WParam | Control.ModifierKeys;
				if (this.ProcessCmdKey(ref msg, keys))
				{
					return true;
				}
				if (this.IsInputKey(keys))
				{
					this.SetState2(128, true);
					return false;
				}
				IntSecurity.ModifyFocus.Assert();
				try
				{
					return this.ProcessDialogKey(keys);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			bool flag;
			if (msg.Msg == 258 || msg.Msg == 262)
			{
				if (msg.Msg == 258 && this.IsInputChar((char)(int)msg.WParam))
				{
					this.SetState2(256, true);
					flag = false;
				}
				else
				{
					flag = this.ProcessDialogChar((char)(int)msg.WParam);
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		/// <summary>Preprocesses keyboard or input messages within the message loop before they are dispatched.</summary>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" /> that represents the message to process.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.PreProcessControlState" /> values, depending on whether <see cref="M:System.Windows.Forms.Control.PreProcessMessage(System.Windows.Forms.Message@)" /> is <see langword="true" /> or <see langword="false" /> and whether <see cref="M:System.Windows.Forms.Control.IsInputKey(System.Windows.Forms.Keys)" /> or <see cref="M:System.Windows.Forms.Control.IsInputChar(System.Char)" /> are <see langword="true" /> or <see langword="false" />.</returns>
		// Token: 0x060011B5 RID: 4533 RVA: 0x000373B4 File Offset: 0x000355B4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public PreProcessControlState PreProcessControlMessage(ref Message msg)
		{
			return Control.PreProcessControlMessageInternal(null, ref msg);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x000373C0 File Offset: 0x000355C0
		internal static PreProcessControlState PreProcessControlMessageInternal(Control target, ref Message msg)
		{
			if (target == null)
			{
				target = Control.FromChildHandleInternal(msg.HWnd);
			}
			if (target == null)
			{
				return PreProcessControlState.MessageNotNeeded;
			}
			target.SetState2(128, false);
			target.SetState2(256, false);
			target.SetState2(512, true);
			PreProcessControlState preProcessControlState2;
			try
			{
				Keys keys = (Keys)(long)msg.WParam | Control.ModifierKeys;
				if (msg.Msg == 256 || msg.Msg == 260)
				{
					target.ProcessUICues(ref msg);
					PreviewKeyDownEventArgs previewKeyDownEventArgs = new PreviewKeyDownEventArgs(keys);
					target.OnPreviewKeyDown(previewKeyDownEventArgs);
					if (previewKeyDownEventArgs.IsInputKey)
					{
						return PreProcessControlState.MessageNeeded;
					}
				}
				PreProcessControlState preProcessControlState = PreProcessControlState.MessageNotNeeded;
				if (!target.PreProcessMessage(ref msg))
				{
					if (msg.Msg == 256 || msg.Msg == 260)
					{
						if (target.GetState2(128) || target.IsInputKey(keys))
						{
							preProcessControlState = PreProcessControlState.MessageNeeded;
						}
					}
					else if ((msg.Msg == 258 || msg.Msg == 262) && (target.GetState2(256) || target.IsInputChar((char)(int)msg.WParam)))
					{
						preProcessControlState = PreProcessControlState.MessageNeeded;
					}
				}
				else
				{
					preProcessControlState = PreProcessControlState.MessageProcessed;
				}
				preProcessControlState2 = preProcessControlState;
			}
			finally
			{
				target.SetState2(512, false);
			}
			return preProcessControlState2;
		}

		/// <summary>Processes a command key.</summary>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011B7 RID: 4535 RVA: 0x000374FC File Offset: 0x000356FC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			ContextMenu contextMenu = (ContextMenu)this.Properties.GetObject(Control.PropContextMenu);
			return (contextMenu != null && contextMenu.ProcessCmdKey(ref msg, keyData, this)) || (this.parent != null && this.parent.ProcessCmdKey(ref msg, keyData));
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00037548 File Offset: 0x00035748
		private void PrintToMetaFile(HandleRef hDC, IntPtr lParam)
		{
			lParam = (IntPtr)((long)lParam & -17L);
			NativeMethods.POINT point = new NativeMethods.POINT();
			bool flag = SafeNativeMethods.GetViewportOrgEx(hDC, point);
			HandleRef handleRef = new HandleRef(null, SafeNativeMethods.CreateRectRgn(point.x, point.y, point.x + this.Width, point.y + this.Height));
			try
			{
				NativeMethods.RegionFlags regionFlags = (NativeMethods.RegionFlags)SafeNativeMethods.SelectClipRgn(hDC, handleRef);
				this.PrintToMetaFileRecursive(hDC, lParam, new Rectangle(Point.Empty, this.Size));
			}
			finally
			{
				flag = SafeNativeMethods.DeleteObject(handleRef);
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x000375E4 File Offset: 0x000357E4
		internal virtual void PrintToMetaFileRecursive(HandleRef hDC, IntPtr lParam, Rectangle bounds)
		{
			using (new WindowsFormsUtils.DCMapping(hDC, bounds))
			{
				this.PrintToMetaFile_SendPrintMessage(hDC, (IntPtr)((long)lParam & -5L));
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				bool windowRect = UnsafeNativeMethods.GetWindowRect(new HandleRef(null, this.Handle), ref rect);
				Point point = this.PointToScreen(Point.Empty);
				point = new Point(point.X - rect.left, point.Y - rect.top);
				Rectangle rectangle = new Rectangle(point, this.ClientSize);
				using (new WindowsFormsUtils.DCMapping(hDC, rectangle))
				{
					this.PrintToMetaFile_SendPrintMessage(hDC, (IntPtr)((long)lParam & -3L));
					int count = this.Controls.Count;
					for (int i = count - 1; i >= 0; i--)
					{
						Control control = this.Controls[i];
						if (control.Visible)
						{
							control.PrintToMetaFileRecursive(hDC, lParam, control.Bounds);
						}
					}
				}
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0003770C File Offset: 0x0003590C
		private void PrintToMetaFile_SendPrintMessage(HandleRef hDC, IntPtr lParam)
		{
			if (this.GetStyle(ControlStyles.UserPaint))
			{
				this.SendMessage(791, hDC.Handle, lParam);
				return;
			}
			if (this.Controls.Count == 0)
			{
				lParam = (IntPtr)((long)lParam | 16L);
			}
			using (Control.MetafileDCWrapper metafileDCWrapper = new Control.MetafileDCWrapper(hDC, this.Size))
			{
				this.SendMessage(791, metafileDCWrapper.HDC, lParam);
			}
		}

		/// <summary>Processes a dialog character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011BB RID: 4539 RVA: 0x00037794 File Offset: 0x00035994
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected virtual bool ProcessDialogChar(char charCode)
		{
			return this.parent != null && this.parent.ProcessDialogChar(charCode);
		}

		/// <summary>Processes a dialog key.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the key was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011BC RID: 4540 RVA: 0x000377AC File Offset: 0x000359AC
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected virtual bool ProcessDialogKey(Keys keyData)
		{
			return this.parent != null && this.parent.ProcessDialogKey(keyData);
		}

		/// <summary>Processes a key message and generates the appropriate control events.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <returns>
		///   <see langword="true" /> if the message was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011BD RID: 4541 RVA: 0x000377C4 File Offset: 0x000359C4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual bool ProcessKeyEventArgs(ref Message m)
		{
			KeyEventArgs keyEventArgs = null;
			KeyPressEventArgs keyPressEventArgs = null;
			IntPtr intPtr = IntPtr.Zero;
			if (m.Msg == 258 || m.Msg == 262)
			{
				int num = this.ImeWmCharsToIgnore;
				if (num > 0)
				{
					num--;
					this.ImeWmCharsToIgnore = num;
					return false;
				}
				keyPressEventArgs = new KeyPressEventArgs((char)(long)m.WParam);
				this.OnKeyPress(keyPressEventArgs);
				intPtr = (IntPtr)((int)keyPressEventArgs.KeyChar);
			}
			else if (m.Msg == 646)
			{
				int num2 = this.ImeWmCharsToIgnore;
				if (Marshal.SystemDefaultCharSize == 1)
				{
					char c = '\0';
					byte[] array = new byte[]
					{
						(byte)((int)(long)m.WParam >> 8),
						(byte)(long)m.WParam
					};
					char[] array2 = new char[1];
					int num3 = UnsafeNativeMethods.MultiByteToWideChar(0, 1, array, array.Length, array2, 0);
					if (num3 <= 0)
					{
						throw new Win32Exception();
					}
					array2 = new char[num3];
					UnsafeNativeMethods.MultiByteToWideChar(0, 1, array, array.Length, array2, array2.Length);
					if (array2[0] != '\0')
					{
						c = array2[0];
						num2 += 2;
					}
					else if (array2[0] == '\0' && array2.Length >= 2)
					{
						c = array2[1];
						num2++;
					}
					this.ImeWmCharsToIgnore = num2;
					keyPressEventArgs = new KeyPressEventArgs(c);
				}
				else
				{
					num2 += 3 - Marshal.SystemDefaultCharSize;
					this.ImeWmCharsToIgnore = num2;
					keyPressEventArgs = new KeyPressEventArgs((char)(long)m.WParam);
				}
				char keyChar = keyPressEventArgs.KeyChar;
				this.OnKeyPress(keyPressEventArgs);
				if (keyPressEventArgs.KeyChar == keyChar)
				{
					intPtr = m.WParam;
				}
				else if (Marshal.SystemDefaultCharSize == 1)
				{
					string text = new string(new char[] { keyPressEventArgs.KeyChar });
					int num4 = UnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, null, 0, IntPtr.Zero, IntPtr.Zero);
					if (num4 >= 2)
					{
						byte[] array3 = new byte[num4];
						UnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, array3, array3.Length, IntPtr.Zero, IntPtr.Zero);
						int num5 = Marshal.SizeOf(typeof(IntPtr));
						if (num4 > num5)
						{
							num4 = num5;
						}
						long num6 = 0L;
						for (int i = 0; i < num4; i++)
						{
							num6 <<= 8;
							num6 |= (long)((ulong)array3[i]);
						}
						intPtr = (IntPtr)num6;
					}
					else if (num4 == 1)
					{
						byte[] array3 = new byte[num4];
						UnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, array3, array3.Length, IntPtr.Zero, IntPtr.Zero);
						intPtr = (IntPtr)((int)array3[0]);
					}
					else
					{
						intPtr = m.WParam;
					}
				}
				else
				{
					intPtr = (IntPtr)((int)keyPressEventArgs.KeyChar);
				}
			}
			else
			{
				keyEventArgs = new KeyEventArgs((Keys)(long)m.WParam | Control.ModifierKeys);
				if (m.Msg == 256 || m.Msg == 260)
				{
					this.OnKeyDown(keyEventArgs);
				}
				else
				{
					this.OnKeyUp(keyEventArgs);
				}
			}
			if (keyPressEventArgs != null)
			{
				m.WParam = intPtr;
				return keyPressEventArgs.Handled;
			}
			if (keyEventArgs.SuppressKeyPress)
			{
				this.RemovePendingMessages(258, 258);
				this.RemovePendingMessages(262, 262);
				this.RemovePendingMessages(646, 646);
			}
			return keyEventArgs.Handled;
		}

		/// <summary>Processes a keyboard message.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <returns>
		///   <see langword="true" /> if the message was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011BE RID: 4542 RVA: 0x00037AF6 File Offset: 0x00035CF6
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected internal virtual bool ProcessKeyMessage(ref Message m)
		{
			return (this.parent != null && this.parent.ProcessKeyPreview(ref m)) || this.ProcessKeyEventArgs(ref m);
		}

		/// <summary>Previews a keyboard message.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <returns>
		///   <see langword="true" /> if the message was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011BF RID: 4543 RVA: 0x00037B17 File Offset: 0x00035D17
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual bool ProcessKeyPreview(ref Message m)
		{
			return this.parent != null && this.parent.ProcessKeyPreview(ref m);
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed as a mnemonic by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011C0 RID: 4544 RVA: 0x0001180C File Offset: 0x0000FA0C
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal virtual bool ProcessMnemonic(char charCode)
		{
			return false;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00037B30 File Offset: 0x00035D30
		internal void ProcessUICues(ref Message msg)
		{
			Keys keys = (Keys)((int)msg.WParam & 65535);
			if (keys != Keys.F10 && keys != Keys.Menu && keys != Keys.Tab)
			{
				return;
			}
			Control control = null;
			int num = (int)(long)this.SendMessage(297, 0, 0);
			if (num == 0)
			{
				control = this.TopMostParent;
				num = (int)control.SendMessage(297, 0, 0);
			}
			int num2 = 0;
			if ((keys == Keys.F10 || keys == Keys.Menu) && (num & 2) != 0)
			{
				num2 |= 2;
			}
			if (keys == Keys.Tab && (num & 1) != 0)
			{
				num2 |= 1;
			}
			if (num2 != 0)
			{
				if (control == null)
				{
					control = this.TopMostParent;
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(control, control.Handle), (UnsafeNativeMethods.GetParent(new HandleRef(null, control.Handle)) == IntPtr.Zero) ? 295 : 296, (IntPtr)(2 | (num2 << 16)), IntPtr.Zero);
			}
		}

		/// <summary>Raises the appropriate drag event.</summary>
		/// <param name="key">The event to raise.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x060011C2 RID: 4546 RVA: 0x00037C10 File Offset: 0x00035E10
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaiseDragEvent(object key, DragEventArgs e)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[key];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, e);
			}
		}

		/// <summary>Raises the appropriate paint event.</summary>
		/// <param name="key">The event to raise.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x060011C3 RID: 4547 RVA: 0x00037C3C File Offset: 0x00035E3C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RaisePaintEvent(object key, PaintEventArgs e)
		{
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[Control.EventPaint];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, e);
			}
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00037C6C File Offset: 0x00035E6C
		private void RemovePendingMessages(int msgMin, int msgMax)
		{
			if (!this.IsDisposed)
			{
				NativeMethods.MSG msg = default(NativeMethods.MSG);
				IntPtr handle = this.Handle;
				while (UnsafeNativeMethods.PeekMessage(ref msg, new HandleRef(this, handle), msgMin, msgMax, 1))
				{
				}
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.BackColor" /> property to its default value.</summary>
		// Token: 0x060011C5 RID: 4549 RVA: 0x00037CA3 File Offset: 0x00035EA3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetBackColor()
		{
			this.BackColor = Color.Empty;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.Cursor" /> property to its default value.</summary>
		// Token: 0x060011C6 RID: 4550 RVA: 0x00037CB0 File Offset: 0x00035EB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetCursor()
		{
			this.Cursor = null;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00037CB9 File Offset: 0x00035EB9
		private void ResetEnabled()
		{
			this.Enabled = true;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.Font" /> property to its default value.</summary>
		// Token: 0x060011C8 RID: 4552 RVA: 0x00037CC2 File Offset: 0x00035EC2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetFont()
		{
			this.Font = null;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property to its default value.</summary>
		// Token: 0x060011C9 RID: 4553 RVA: 0x00037CCB File Offset: 0x00035ECB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetForeColor()
		{
			this.ForeColor = Color.Empty;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00037CD8 File Offset: 0x00035ED8
		private void ResetLocation()
		{
			this.Location = new Point(0, 0);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00037CE7 File Offset: 0x00035EE7
		private void ResetMargin()
		{
			this.Margin = this.DefaultMargin;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00037CF5 File Offset: 0x00035EF5
		private void ResetMinimumSize()
		{
			this.MinimumSize = this.DefaultMinimumSize;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00037D03 File Offset: 0x00035F03
		private void ResetPadding()
		{
			CommonProperties.ResetPadding(this);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00037D0B File Offset: 0x00035F0B
		private void ResetSize()
		{
			this.Size = this.DefaultSize;
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property to its default value.</summary>
		// Token: 0x060011CF RID: 4559 RVA: 0x00037D19 File Offset: 0x00035F19
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetRightToLeft()
		{
			this.RightToLeft = RightToLeft.Inherit;
		}

		/// <summary>Forces the re-creation of the handle for the control.</summary>
		// Token: 0x060011D0 RID: 4560 RVA: 0x00037D22 File Offset: 0x00035F22
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RecreateHandle()
		{
			this.RecreateHandleCore();
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00037D2C File Offset: 0x00035F2C
		internal virtual void RecreateHandleCore()
		{
			lock (this)
			{
				if (this.IsHandleCreated)
				{
					bool containsFocus = this.ContainsFocus;
					bool flag2 = (this.state & 1) != 0;
					if (this.GetState(16384))
					{
						this.SetState(8192, true);
						this.UnhookMouseEvent();
					}
					HandleRef handleRef = new HandleRef(this, UnsafeNativeMethods.GetParent(new HandleRef(this, this.Handle)));
					try
					{
						Control[] array = null;
						this.state |= 16;
						try
						{
							Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
							if (controlCollection != null && controlCollection.Count > 0)
							{
								array = new Control[controlCollection.Count];
								for (int i = 0; i < controlCollection.Count; i++)
								{
									Control control = controlCollection[i];
									if (control != null && control.IsHandleCreated)
									{
										control.OnParentHandleRecreating();
										array[i] = control;
									}
									else
									{
										array[i] = null;
									}
								}
							}
							this.DestroyHandle();
							this.CreateHandle();
						}
						finally
						{
							this.state &= -17;
							if (array != null)
							{
								foreach (Control control2 in array)
								{
									if (control2 != null && control2.IsHandleCreated)
									{
										control2.OnParentHandleRecreated();
									}
								}
							}
						}
						if (flag2)
						{
							this.CreateControl();
						}
					}
					finally
					{
						if (handleRef.Handle != IntPtr.Zero && (Control.FromHandleInternal(handleRef.Handle) == null || this.parent == null) && UnsafeNativeMethods.IsWindow(handleRef))
						{
							UnsafeNativeMethods.SetParent(new HandleRef(this, this.Handle), handleRef);
						}
					}
					if (containsFocus)
					{
						this.FocusInternal();
					}
				}
			}
		}

		/// <summary>Computes the size and location of the specified screen rectangle in client coordinates.</summary>
		/// <param name="r">The screen coordinate <see cref="T:System.Drawing.Rectangle" /> to convert.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the converted <see cref="T:System.Drawing.Rectangle" />, <paramref name="r" />, in client coordinates.</returns>
		// Token: 0x060011D2 RID: 4562 RVA: 0x00037F28 File Offset: 0x00036128
		public Rectangle RectangleToClient(Rectangle r)
		{
			NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(r.X, r.Y, r.Width, r.Height);
			UnsafeNativeMethods.MapWindowPoints(NativeMethods.NullHandleRef, new HandleRef(this, this.Handle), ref rect, 2);
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		/// <summary>Computes the size and location of the specified client rectangle in screen coordinates.</summary>
		/// <param name="r">The client coordinate <see cref="T:System.Drawing.Rectangle" /> to convert.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the converted <see cref="T:System.Drawing.Rectangle" />, <paramref name="p" />, in screen coordinates.</returns>
		// Token: 0x060011D3 RID: 4563 RVA: 0x00037F90 File Offset: 0x00036190
		public Rectangle RectangleToScreen(Rectangle r)
		{
			NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(r.X, r.Y, r.Width, r.Height);
			UnsafeNativeMethods.MapWindowPoints(new HandleRef(this, this.Handle), NativeMethods.NullHandleRef, ref rect, 2);
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		/// <summary>Reflects the specified message to the control that is bound to the specified handle.</summary>
		/// <param name="hWnd">An <see cref="T:System.IntPtr" /> representing the handle of the control to reflect the message to.</param>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> representing the Windows message to reflect.</param>
		/// <returns>
		///   <see langword="true" /> if the message was reflected; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011D4 RID: 4564 RVA: 0x00037FF6 File Offset: 0x000361F6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected static bool ReflectMessage(IntPtr hWnd, ref Message m)
		{
			IntSecurity.SendMessages.Demand();
			return Control.ReflectMessageInternal(hWnd, ref m);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0003800C File Offset: 0x0003620C
		internal static bool ReflectMessageInternal(IntPtr hWnd, ref Message m)
		{
			Control control = Control.FromHandleInternal(hWnd);
			if (control == null)
			{
				return false;
			}
			m.Result = control.SendMessage(8192 + m.Msg, m.WParam, m.LParam);
			return true;
		}

		/// <summary>Forces the control to invalidate its client area and immediately redraw itself and any child controls.</summary>
		// Token: 0x060011D6 RID: 4566 RVA: 0x0003804A File Offset: 0x0003624A
		public virtual void Refresh()
		{
			this.Invalidate(true);
			this.Update();
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0003805C File Offset: 0x0003625C
		internal virtual void ReleaseUiaProvider(IntPtr handle)
		{
			if (handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.UiaReturnRawElementProvider(new HandleRef(this, handle), IntPtr.Zero, IntPtr.Zero, null);
				if (this.IsInternalAccessibilityObjectCreated && LocalAppContextSwitches.DisconnectUiaProvidersOnWmDestroy && ApiHelper.IsApiAvailable("UIAutomationCore.dll", "UiaDisconnectProvider"))
				{
					int num = UnsafeNativeMethods.UiaDisconnectProvider(this.UnsafeAccessibilityObject);
				}
			}
		}

		/// <summary>Resets the control to handle the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		// Token: 0x060011D8 RID: 4568 RVA: 0x000380BA File Offset: 0x000362BA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void ResetMouseEventArgs()
		{
			if (this.GetState(16384))
			{
				this.UnhookMouseEvent();
				this.HookMouseEvent();
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.Text" /> property to its default value (<see cref="F:System.String.Empty" />).</summary>
		// Token: 0x060011D9 RID: 4569 RVA: 0x000380D5 File Offset: 0x000362D5
		public virtual void ResetText()
		{
			this.Text = string.Empty;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000380E2 File Offset: 0x000362E2
		private void ResetVisible()
		{
			this.Visible = true;
		}

		/// <summary>Resumes usual layout logic.</summary>
		// Token: 0x060011DB RID: 4571 RVA: 0x000380EB File Offset: 0x000362EB
		public void ResumeLayout()
		{
			this.ResumeLayout(true);
		}

		/// <summary>Resumes usual layout logic, optionally forcing an immediate layout of pending layout requests.</summary>
		/// <param name="performLayout">
		///   <see langword="true" /> to execute pending layout requests; otherwise, <see langword="false" />.</param>
		// Token: 0x060011DC RID: 4572 RVA: 0x000380F4 File Offset: 0x000362F4
		public void ResumeLayout(bool performLayout)
		{
			bool flag = false;
			if (this.layoutSuspendCount > 0)
			{
				if (this.layoutSuspendCount == 1)
				{
					this.layoutSuspendCount += 1;
					try
					{
						this.OnLayoutResuming(performLayout);
					}
					finally
					{
						this.layoutSuspendCount -= 1;
					}
				}
				this.layoutSuspendCount -= 1;
				if (this.layoutSuspendCount == 0 && this.GetState(512) && performLayout)
				{
					this.PerformLayout();
					flag = true;
				}
			}
			if (!flag)
			{
				this.SetState2(64, true);
			}
			if (!performLayout)
			{
				CommonProperties.xClearPreferredSizeCache(this);
				Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
				if (controlCollection != null)
				{
					for (int i = 0; i < controlCollection.Count; i++)
					{
						this.LayoutEngine.InitLayout(controlCollection[i], BoundsSpecified.All);
						CommonProperties.xClearPreferredSizeCache(controlCollection[i]);
					}
				}
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000381E0 File Offset: 0x000363E0
		internal void SetAcceptDrops(bool accept)
		{
			if (accept != this.GetState(128) && this.IsHandleCreated)
			{
				try
				{
					if (Application.OleRequired() != ApartmentState.STA)
					{
						throw new ThreadStateException(SR.GetString("ThreadMustBeSTA"));
					}
					if (accept)
					{
						IntSecurity.ClipboardRead.Demand();
						int num = UnsafeNativeMethods.RegisterDragDrop(new HandleRef(this, this.Handle), new DropTarget(this));
						if (num != 0 && num != -2147221247)
						{
							throw new Win32Exception(num);
						}
					}
					else
					{
						int num2 = UnsafeNativeMethods.RevokeDragDrop(new HandleRef(this, this.Handle));
						if (num2 != 0 && num2 != -2147221248)
						{
							throw new Win32Exception(num2);
						}
					}
					this.SetState(128, accept);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(SR.GetString("DragDropRegFailed"), ex);
				}
			}
		}

		/// <summary>Scales the control and any child controls.</summary>
		/// <param name="ratio">The ratio to use for scaling.</param>
		// Token: 0x060011DE RID: 4574 RVA: 0x000382AC File Offset: 0x000364AC
		[Obsolete("This method has been deprecated. Use the Scale(SizeF ratio) method instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Scale(float ratio)
		{
			this.ScaleCore(ratio, ratio);
		}

		/// <summary>Scales the entire control and any child controls.</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x060011DF RID: 4575 RVA: 0x000382B8 File Offset: 0x000364B8
		[Obsolete("This method has been deprecated. Use the Scale(SizeF ratio) method instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Scale(float dx, float dy)
		{
			this.SuspendLayout();
			try
			{
				this.ScaleCore(dx, dy);
			}
			finally
			{
				this.ResumeLayout();
			}
		}

		/// <summary>Scales the control and all child controls by the specified scaling factor.</summary>
		/// <param name="factor">A <see cref="T:System.Drawing.SizeF" /> containing the horizontal and vertical scaling factors.</param>
		// Token: 0x060011E0 RID: 4576 RVA: 0x000382EC File Offset: 0x000364EC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void Scale(SizeF factor)
		{
			using (new LayoutTransaction(this, this, PropertyNames.Bounds, false))
			{
				this.ScaleControl(factor, factor, this);
				if (this.ScaleChildren)
				{
					Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
					if (controlCollection != null)
					{
						for (int i = 0; i < controlCollection.Count; i++)
						{
							Control control = controlCollection[i];
							control.Scale(factor);
						}
					}
				}
			}
			LayoutTransaction.DoLayout(this, this, PropertyNames.Bounds);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0003837C File Offset: 0x0003657C
		internal virtual void Scale(SizeF includedFactor, SizeF excludedFactor, Control requestingControl)
		{
			using (new LayoutTransaction(this, this, PropertyNames.Bounds, false))
			{
				this.ScaleControl(includedFactor, excludedFactor, requestingControl);
				this.ScaleChildControls(includedFactor, excludedFactor, requestingControl, false);
			}
			LayoutTransaction.DoLayout(this, this, PropertyNames.Bounds);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x000383D4 File Offset: 0x000365D4
		internal void ScaleChildControls(SizeF includedFactor, SizeF excludedFactor, Control requestingControl, bool updateWindowFontIfNeeded = false)
		{
			if (this.ScaleChildren)
			{
				Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
				if (controlCollection != null)
				{
					for (int i = 0; i < controlCollection.Count; i++)
					{
						Control control = controlCollection[i];
						if (updateWindowFontIfNeeded)
						{
							control.UpdateWindowFontIfNeeded();
						}
						control.Scale(includedFactor, excludedFactor, requestingControl);
					}
				}
			}
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0003842E File Offset: 0x0003662E
		internal void UpdateWindowFontIfNeeded()
		{
			if (DpiHelper.EnableDpiChangedHighDpiImprovements && !this.GetStyle(ControlStyles.UserPaint) && this.Properties.GetObject(Control.PropFont) == null)
			{
				this.SetWindowFont();
			}
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00038458 File Offset: 0x00036658
		internal void ScaleControl(SizeF includedFactor, SizeF excludedFactor, Control requestingControl)
		{
			try
			{
				this.IsCurrentlyBeingScaled = true;
				BoundsSpecified boundsSpecified = BoundsSpecified.None;
				BoundsSpecified boundsSpecified2 = BoundsSpecified.None;
				if (!includedFactor.IsEmpty)
				{
					boundsSpecified = this.RequiredScaling;
				}
				if (!excludedFactor.IsEmpty)
				{
					boundsSpecified2 |= ~this.RequiredScaling & BoundsSpecified.All;
				}
				if (boundsSpecified != BoundsSpecified.None)
				{
					this.ScaleControl(includedFactor, boundsSpecified);
				}
				if (boundsSpecified2 != BoundsSpecified.None)
				{
					this.ScaleControl(excludedFactor, boundsSpecified2);
				}
				if (!includedFactor.IsEmpty)
				{
					this.RequiredScaling = BoundsSpecified.None;
				}
			}
			finally
			{
				this.IsCurrentlyBeingScaled = false;
			}
		}

		/// <summary>Scales a control's location, size, padding and margin.</summary>
		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x060011E5 RID: 4581 RVA: 0x000384D8 File Offset: 0x000366D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			CreateParams createParams = this.CreateParams;
			NativeMethods.RECT rect = new NativeMethods.RECT(0, 0, 0, 0);
			this.AdjustWindowRectEx(ref rect, createParams.Style, this.HasMenu, createParams.ExStyle);
			Size size = this.MinimumSize;
			Size size2 = this.MaximumSize;
			this.MinimumSize = Size.Empty;
			this.MaximumSize = Size.Empty;
			Rectangle scaledBounds = this.GetScaledBounds(this.Bounds, factor, specified);
			float num = factor.Width;
			float num2 = factor.Height;
			Padding padding = this.Padding;
			Padding margin = this.Margin;
			if (num == 1f)
			{
				specified &= ~(BoundsSpecified.X | BoundsSpecified.Width);
			}
			if (num2 == 1f)
			{
				specified &= ~(BoundsSpecified.Y | BoundsSpecified.Height);
			}
			if (num != 1f)
			{
				padding.Left = (int)Math.Round((double)((float)padding.Left * num));
				padding.Right = (int)Math.Round((double)((float)padding.Right * num));
				margin.Left = (int)Math.Round((double)((float)margin.Left * num));
				margin.Right = (int)Math.Round((double)((float)margin.Right * num));
			}
			if (num2 != 1f)
			{
				padding.Top = (int)Math.Round((double)((float)padding.Top * num2));
				padding.Bottom = (int)Math.Round((double)((float)padding.Bottom * num2));
				margin.Top = (int)Math.Round((double)((float)margin.Top * num2));
				margin.Bottom = (int)Math.Round((double)((float)margin.Bottom * num2));
			}
			this.Padding = padding;
			this.Margin = margin;
			Size size3 = rect.Size;
			if (!size.IsEmpty)
			{
				size -= size3;
				size = this.ScaleSize(LayoutUtils.UnionSizes(Size.Empty, size), factor.Width, factor.Height) + size3;
			}
			if (!size2.IsEmpty)
			{
				size2 -= size3;
				size2 = this.ScaleSize(LayoutUtils.UnionSizes(Size.Empty, size2), factor.Width, factor.Height) + size3;
			}
			Size size4 = LayoutUtils.ConvertZeroToUnbounded(size2);
			Size size5 = LayoutUtils.IntersectSizes(scaledBounds.Size, size4);
			size5 = LayoutUtils.UnionSizes(size5, size);
			if (DpiHelper.EnableAnchorLayoutHighDpiImprovements && this.ParentInternal != null && this.ParentInternal.LayoutEngine == DefaultLayout.Instance)
			{
				DefaultLayout.ScaleAnchorInfo(this, factor);
			}
			this.SetBoundsCore(scaledBounds.X, scaledBounds.Y, size5.Width, size5.Height, BoundsSpecified.All);
			this.MaximumSize = size2;
			this.MinimumSize = size;
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x060011E6 RID: 4582 RVA: 0x00038768 File Offset: 0x00036968
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void ScaleCore(float dx, float dy)
		{
			this.SuspendLayout();
			try
			{
				int num = (int)Math.Round((double)((float)this.x * dx));
				int num2 = (int)Math.Round((double)((float)this.y * dy));
				int num3 = this.width;
				if ((this.controlStyle & ControlStyles.FixedWidth) != ControlStyles.FixedWidth)
				{
					num3 = (int)Math.Round((double)((float)(this.x + this.width) * dx)) - num;
				}
				int num4 = this.height;
				if ((this.controlStyle & ControlStyles.FixedHeight) != ControlStyles.FixedHeight)
				{
					num4 = (int)Math.Round((double)((float)(this.y + this.height) * dy)) - num2;
				}
				this.SetBounds(num, num2, num3, num4, BoundsSpecified.All);
				Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
				if (controlCollection != null)
				{
					for (int i = 0; i < controlCollection.Count; i++)
					{
						controlCollection[i].Scale(dx, dy);
					}
				}
			}
			finally
			{
				this.ResumeLayout();
			}
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00038860 File Offset: 0x00036A60
		internal Size ScaleSize(Size startSize, float x, float y)
		{
			Size size = startSize;
			if (!this.GetStyle(ControlStyles.FixedWidth))
			{
				size.Width = (int)Math.Round((double)((float)size.Width * x));
			}
			if (!this.GetStyle(ControlStyles.FixedHeight))
			{
				size.Height = (int)Math.Round((double)((float)size.Height * y));
			}
			return size;
		}

		/// <summary>Activates the control.</summary>
		// Token: 0x060011E8 RID: 4584 RVA: 0x000388B4 File Offset: 0x00036AB4
		public void Select()
		{
			this.Select(false, false);
		}

		/// <summary>Activates a child control. Optionally specifies the direction in the tab order to select the control from.</summary>
		/// <param name="directed">
		///   <see langword="true" /> to specify the direction of the control to select; otherwise, <see langword="false" />.</param>
		/// <param name="forward">
		///   <see langword="true" /> to move forward in the tab order; <see langword="false" /> to move backward in the tab order.</param>
		// Token: 0x060011E9 RID: 4585 RVA: 0x000388C0 File Offset: 0x00036AC0
		protected virtual void Select(bool directed, bool forward)
		{
			IContainerControl containerControlInternal = this.GetContainerControlInternal();
			if (containerControlInternal != null)
			{
				containerControlInternal.ActiveControl = this;
			}
		}

		/// <summary>Activates the next control.</summary>
		/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> at which to start the search.</param>
		/// <param name="forward">
		///   <see langword="true" /> to move forward in the tab order; <see langword="false" /> to move backward in the tab order.</param>
		/// <param name="tabStopOnly">
		///   <see langword="true" /> to ignore the controls with the <see cref="P:System.Windows.Forms.Control.TabStop" /> property set to <see langword="false" />; otherwise, <see langword="false" />.</param>
		/// <param name="nested">
		///   <see langword="true" /> to include nested (children of child controls) child controls; otherwise, <see langword="false" />.</param>
		/// <param name="wrap">
		///   <see langword="true" /> to continue searching from the first control in the tab order after the last control has been reached; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if a control was activated; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011EA RID: 4586 RVA: 0x000388E0 File Offset: 0x00036AE0
		public bool SelectNextControl(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
		{
			Control nextSelectableControl = this.GetNextSelectableControl(ctl, forward, tabStopOnly, nested, wrap);
			if (nextSelectableControl != null)
			{
				nextSelectableControl.Select(true, forward);
				return true;
			}
			return false;
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0003890C File Offset: 0x00036B0C
		private Control GetNextSelectableControl(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
		{
			if (!this.Contains(ctl) || (!nested && ctl.parent != this))
			{
				ctl = null;
			}
			bool flag = false;
			Control control = ctl;
			for (;;)
			{
				ctl = this.GetNextControl(ctl, forward);
				if (ctl == null)
				{
					if (!wrap)
					{
						goto IL_71;
					}
					if (flag)
					{
						break;
					}
					flag = true;
				}
				else if (ctl.CanSelect && (!tabStopOnly || ctl.TabStop) && (nested || ctl.parent == this) && (!AccessibilityImprovements.Level3 || !(ctl.parent is ToolStrip)))
				{
					return ctl;
				}
				if (ctl == control)
				{
					goto IL_71;
				}
			}
			return null;
			IL_71:
			return null;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0003898B File Offset: 0x00036B8B
		internal bool SelectNextControlInternal(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
		{
			return this.SelectNextControl(ctl, forward, tabStopOnly, nested, wrap);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0003899C File Offset: 0x00036B9C
		private void SelectNextIfFocused()
		{
			if (this.ContainsFocus && this.ParentInternal != null)
			{
				IContainerControl containerControlInternal = this.ParentInternal.GetContainerControlInternal();
				if (containerControlInternal != null)
				{
					((Control)containerControlInternal).SelectNextControlInternal(this, true, true, true, true);
				}
			}
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x000389D9 File Offset: 0x00036BD9
		internal IntPtr SendMessage(int msg, int wparam, int lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x000389EF File Offset: 0x00036BEF
		internal IntPtr SendMessage(int msg, ref int wparam, ref int lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, ref wparam, ref lparam);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00038A05 File Offset: 0x00036C05
		internal IntPtr SendMessage(int msg, int wparam, IntPtr lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, (IntPtr)wparam, lparam);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00038A20 File Offset: 0x00036C20
		internal IntPtr SendMessage(int msg, IntPtr wparam, IntPtr lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00038A36 File Offset: 0x00036C36
		internal IntPtr SendMessage(int msg, IntPtr wparam, int lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, (IntPtr)lparam);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00038A51 File Offset: 0x00036C51
		internal IntPtr SendMessage(int msg, int wparam, ref NativeMethods.RECT lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, ref lparam);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00038A67 File Offset: 0x00036C67
		internal IntPtr SendMessage(int msg, bool wparam, int lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00038A7D File Offset: 0x00036C7D
		internal IntPtr SendMessage(int msg, int wparam, string lparam)
		{
			return UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
		}

		/// <summary>Sends the control to the back of the z-order.</summary>
		// Token: 0x060011F6 RID: 4598 RVA: 0x00038A94 File Offset: 0x00036C94
		public void SendToBack()
		{
			if (this.parent != null)
			{
				this.parent.Controls.SetChildIndex(this, -1);
				return;
			}
			if (this.IsHandleCreated && this.GetTopLevel())
			{
				SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), NativeMethods.HWND_BOTTOM, 0, 0, 0, 0, 3);
			}
		}

		/// <summary>Sets the bounds of the control to the specified location and size.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		// Token: 0x060011F7 RID: 4599 RVA: 0x00038AF0 File Offset: 0x00036CF0
		public void SetBounds(int x, int y, int width, int height)
		{
			if (this.x != x || this.y != y || this.width != width || this.height != height)
			{
				this.SetBoundsCore(x, y, width, height, BoundsSpecified.All);
				LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
				return;
			}
			this.InitScaling(BoundsSpecified.All);
		}

		/// <summary>Sets the specified bounds of the control to the specified location and size.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. For any parameter not specified, the current value will be used.</param>
		// Token: 0x060011F8 RID: 4600 RVA: 0x00038B4C File Offset: 0x00036D4C
		public void SetBounds(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.X) == BoundsSpecified.None)
			{
				x = this.x;
			}
			if ((specified & BoundsSpecified.Y) == BoundsSpecified.None)
			{
				y = this.y;
			}
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.None)
			{
				width = this.width;
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.None)
			{
				height = this.height;
			}
			if (this.x != x || this.y != y || this.width != width || this.height != height)
			{
				this.SetBoundsCore(x, y, width, height, specified);
				LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
				return;
			}
			this.InitScaling(specified);
		}

		/// <summary>Performs the work of setting the specified bounds of this control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x060011F9 RID: 4601 RVA: 0x00038BE0 File Offset: 0x00036DE0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.ParentInternal != null)
			{
				this.ParentInternal.SuspendLayout();
			}
			try
			{
				if (this.x != x || this.y != y || this.width != width || this.height != height)
				{
					CommonProperties.UpdateSpecifiedBounds(this, x, y, width, height, specified);
					Rectangle rectangle = this.ApplyBoundsConstraints(x, y, width, height);
					width = rectangle.Width;
					height = rectangle.Height;
					x = rectangle.X;
					y = rectangle.Y;
					if (!this.IsHandleCreated)
					{
						this.UpdateBounds(x, y, width, height);
					}
					else if (!this.GetState(65536))
					{
						int num = 20;
						if (this.x == x && this.y == y)
						{
							num |= 2;
						}
						if (this.width == width && this.height == height)
						{
							num |= 1;
						}
						this.OnBoundsUpdate(x, y, width, height);
						SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), NativeMethods.NullHandleRef, x, y, width, height, num);
					}
				}
			}
			finally
			{
				this.InitScaling(specified);
				if (this.ParentInternal != null)
				{
					CommonProperties.xClearPreferredSizeCache(this.ParentInternal);
					this.ParentInternal.LayoutEngine.InitLayout(this, specified);
					this.ParentInternal.ResumeLayout(true);
				}
			}
		}

		/// <summary>Sets the size of the client area of the control.</summary>
		/// <param name="x">The client area width, in pixels.</param>
		/// <param name="y">The client area height, in pixels.</param>
		// Token: 0x060011FA RID: 4602 RVA: 0x00038D38 File Offset: 0x00036F38
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void SetClientSizeCore(int x, int y)
		{
			this.Size = this.SizeFromClientSize(x, y);
			this.clientWidth = x;
			this.clientHeight = y;
			this.OnClientSizeChanged(EventArgs.Empty);
		}

		/// <summary>Determines the size of the entire control from the height and width of its client area.</summary>
		/// <param name="clientSize">A <see cref="T:System.Drawing.Size" /> value representing the height and width of the control's client area.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value representing the height and width of the entire control.</returns>
		// Token: 0x060011FB RID: 4603 RVA: 0x00038D61 File Offset: 0x00036F61
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual Size SizeFromClientSize(Size clientSize)
		{
			return this.SizeFromClientSize(clientSize.Width, clientSize.Height);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00038D78 File Offset: 0x00036F78
		internal Size SizeFromClientSize(int width, int height)
		{
			NativeMethods.RECT rect = new NativeMethods.RECT(0, 0, width, height);
			CreateParams createParams = this.CreateParams;
			this.AdjustWindowRectEx(ref rect, createParams.Style, this.HasMenu, createParams.ExStyle);
			return rect.Size;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00038DB8 File Offset: 0x00036FB8
		private void SetHandle(IntPtr value)
		{
			if (value == IntPtr.Zero)
			{
				this.SetState(1, false);
			}
			this.UpdateRoot();
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00038DD8 File Offset: 0x00036FD8
		private void SetParentHandle(IntPtr value)
		{
			if (this.IsHandleCreated)
			{
				IntPtr intPtr = UnsafeNativeMethods.GetParent(new HandleRef(this.window, this.Handle));
				bool topLevel = this.GetTopLevel();
				if (intPtr != value || (intPtr == IntPtr.Zero && !topLevel))
				{
					bool flag = (intPtr == IntPtr.Zero && !topLevel) || (value == IntPtr.Zero && topLevel);
					if (flag)
					{
						Form form = this as Form;
						if (form != null && !form.CanRecreateHandle())
						{
							flag = false;
							this.UpdateStyles();
						}
					}
					if (flag)
					{
						this.RecreateHandle();
					}
					if (!this.GetTopLevel())
					{
						if (value == IntPtr.Zero)
						{
							Application.ParkHandle(new HandleRef(this.window, this.Handle), this.DpiAwarenessContext);
							this.UpdateRoot();
							return;
						}
						UnsafeNativeMethods.SetParent(new HandleRef(this.window, this.Handle), new HandleRef(null, value));
						if (this.parent != null)
						{
							this.parent.UpdateChildZOrder(this);
						}
						Application.UnparkHandle(new HandleRef(this.window, this.Handle), this.window.DpiAwarenessContext);
						return;
					}
				}
				else if (value == IntPtr.Zero && intPtr == IntPtr.Zero && topLevel)
				{
					UnsafeNativeMethods.SetParent(new HandleRef(this.window, this.Handle), new HandleRef(null, IntPtr.Zero));
					Application.UnparkHandle(new HandleRef(this.window, this.Handle), this.window.DpiAwarenessContext);
				}
			}
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00038F65 File Offset: 0x00037165
		internal void SetState(int flag, bool value)
		{
			this.state = (value ? (this.state | flag) : (this.state & ~flag));
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00038F83 File Offset: 0x00037183
		internal void SetState2(int flag, bool value)
		{
			this.state2 = (value ? (this.state2 | flag) : (this.state2 & ~flag));
		}

		/// <summary>Sets a specified <see cref="T:System.Windows.Forms.ControlStyles" /> flag to either <see langword="true" /> or <see langword="false" />.</summary>
		/// <param name="flag">The <see cref="T:System.Windows.Forms.ControlStyles" /> bit to set.</param>
		/// <param name="value">
		///   <see langword="true" /> to apply the specified style to the control; otherwise, <see langword="false" />.</param>
		// Token: 0x06001201 RID: 4609 RVA: 0x00038FA1 File Offset: 0x000371A1
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void SetStyle(ControlStyles flag, bool value)
		{
			if ((flag & ControlStyles.EnableNotifyMessage) > (ControlStyles)0 && value)
			{
				IntSecurity.UnmanagedCode.Demand();
			}
			this.controlStyle = (value ? (this.controlStyle | flag) : (this.controlStyle & ~flag));
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00038FD8 File Offset: 0x000371D8
		internal static IntPtr SetUpPalette(IntPtr dc, bool force, bool realizePalette)
		{
			IntPtr halftonePalette = Graphics.GetHalftonePalette();
			IntPtr intPtr = SafeNativeMethods.SelectPalette(new HandleRef(null, dc), new HandleRef(null, halftonePalette), force ? 0 : 1);
			if (intPtr != IntPtr.Zero && realizePalette)
			{
				SafeNativeMethods.RealizePalette(new HandleRef(null, dc));
			}
			return intPtr;
		}

		/// <summary>Sets the control as the top-level control.</summary>
		/// <param name="value">
		///   <see langword="true" /> to set the control as the top-level control; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="value" /> parameter is set to <see langword="true" /> and the control is an ActiveX control.</exception>
		/// <exception cref="T:System.Exception">The <see cref="M:System.Windows.Forms.Control.GetTopLevel" /> return value is not equal to the <paramref name="value" /> parameter and the <see cref="P:System.Windows.Forms.Control.Parent" /> property is not <see langword="null" />.</exception>
		// Token: 0x06001203 RID: 4611 RVA: 0x00039024 File Offset: 0x00037224
		protected void SetTopLevel(bool value)
		{
			if (value && this.IsActiveX)
			{
				throw new InvalidOperationException(SR.GetString("TopLevelNotAllowedIfActiveX"));
			}
			if (value)
			{
				if (this is Form)
				{
					IntSecurity.TopLevelWindow.Demand();
				}
				else
				{
					IntSecurity.UnrestrictedWindows.Demand();
				}
			}
			this.SetTopLevelInternal(value);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00039074 File Offset: 0x00037274
		internal void SetTopLevelInternal(bool value)
		{
			if (this.GetTopLevel() != value)
			{
				if (this.parent != null)
				{
					throw new ArgumentException(SR.GetString("TopLevelParentedControl"), "value");
				}
				this.SetState(524288, value);
				if (this.IsHandleCreated && this.GetState2(8))
				{
					this.ListenToUserPreferenceChanged(value);
				}
				this.UpdateStyles();
				this.SetParentHandle(IntPtr.Zero);
				if (value && this.Visible)
				{
					this.CreateControl();
				}
				this.UpdateRoot();
			}
		}

		/// <summary>Sets the control to the specified visible state.</summary>
		/// <param name="value">
		///   <see langword="true" /> to make the control visible; otherwise, <see langword="false" />.</param>
		// Token: 0x06001205 RID: 4613 RVA: 0x000390F4 File Offset: 0x000372F4
		protected virtual void SetVisibleCore(bool value)
		{
			try
			{
				System.Internal.HandleCollector.SuspendCollect();
				if (this.GetVisibleCore() != value)
				{
					if (!value)
					{
						this.SelectNextIfFocused();
					}
					bool flag = false;
					if (this.GetTopLevel())
					{
						if (this.IsHandleCreated || value)
						{
							SafeNativeMethods.ShowWindow(new HandleRef(this, this.Handle), value ? this.ShowParams : 0);
						}
					}
					else if (this.IsHandleCreated || (value && this.parent != null && this.parent.Created))
					{
						this.SetState(2, value);
						flag = true;
						try
						{
							if (value)
							{
								this.CreateControl();
							}
							SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), NativeMethods.NullHandleRef, 0, 0, 0, 0, 23 | (value ? 64 : 128));
						}
						catch
						{
							this.SetState(2, !value);
							throw;
						}
					}
					if (this.GetVisibleCore() != value)
					{
						this.SetState(2, value);
						flag = true;
					}
					if (flag)
					{
						using (new LayoutTransaction(this.parent, this, PropertyNames.Visible))
						{
							this.OnVisibleChanged(EventArgs.Empty);
						}
					}
					this.UpdateRoot();
				}
				else if (this.GetState(2) || value || !this.IsHandleCreated || SafeNativeMethods.IsWindowVisible(new HandleRef(this, this.Handle)))
				{
					this.SetState(2, value);
					if (this.IsHandleCreated)
					{
						SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.Handle), NativeMethods.NullHandleRef, 0, 0, 0, 0, 23 | (value ? 64 : 128));
					}
				}
			}
			finally
			{
				System.Internal.HandleCollector.ResumeCollect();
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000392C8 File Offset: 0x000374C8
		internal static AutoValidate GetAutoValidateForControl(Control control)
		{
			ContainerControl parentContainerControl = control.ParentContainerControl;
			if (parentContainerControl == null)
			{
				return AutoValidate.EnablePreventFocusChange;
			}
			return parentContainerControl.AutoValidate;
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x000392E7 File Offset: 0x000374E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal bool ShouldAutoValidate
		{
			get
			{
				return Control.GetAutoValidateForControl(this) > AutoValidate.Disable;
			}
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x000392F2 File Offset: 0x000374F2
		internal virtual bool ShouldPerformContainerValidation()
		{
			return this.GetStyle(ControlStyles.ContainerControl);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000392FC File Offset: 0x000374FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeBackColor()
		{
			return !this.Properties.GetColor(Control.PropBackColor).IsEmpty;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00039324 File Offset: 0x00037524
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeCursor()
		{
			bool flag;
			object @object = this.Properties.GetObject(Control.PropCursor, out flag);
			return flag && @object != null;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0003934D File Offset: 0x0003754D
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeEnabled()
		{
			return !this.GetState(4);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0003935C File Offset: 0x0003755C
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeForeColor()
		{
			return !this.Properties.GetColor(Control.PropForeColor).IsEmpty;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00039384 File Offset: 0x00037584
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeFont()
		{
			bool flag;
			object @object = this.Properties.GetObject(Control.PropFont, out flag);
			return flag && @object != null;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000393B0 File Offset: 0x000375B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeRightToLeft()
		{
			bool flag;
			int integer = this.Properties.GetInteger(Control.PropRightToLeft, out flag);
			return flag && integer != 2;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000393DC File Offset: 0x000375DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeVisible()
		{
			return !this.GetState(2);
		}

		/// <summary>Converts the specified <see cref="T:System.Windows.Forms.HorizontalAlignment" /> to the appropriate <see cref="T:System.Windows.Forms.HorizontalAlignment" /> to support right-to-left text.</summary>
		/// <param name="align">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</returns>
		// Token: 0x06001210 RID: 4624 RVA: 0x000393E8 File Offset: 0x000375E8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected HorizontalAlignment RtlTranslateAlignment(HorizontalAlignment align)
		{
			return this.RtlTranslateHorizontal(align);
		}

		/// <summary>Converts the specified <see cref="T:System.Windows.Forms.LeftRightAlignment" /> to the appropriate <see cref="T:System.Windows.Forms.LeftRightAlignment" /> to support right-to-left text.</summary>
		/// <param name="align">One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values.</returns>
		// Token: 0x06001211 RID: 4625 RVA: 0x000393F1 File Offset: 0x000375F1
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected LeftRightAlignment RtlTranslateAlignment(LeftRightAlignment align)
		{
			return this.RtlTranslateLeftRight(align);
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.ContentAlignment" /> to the appropriate <see cref="T:System.Drawing.ContentAlignment" /> to support right-to-left text.</summary>
		/// <param name="align">One of the <see cref="T:System.Drawing.ContentAlignment" /> values.</param>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values.</returns>
		// Token: 0x06001212 RID: 4626 RVA: 0x000393FA File Offset: 0x000375FA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected ContentAlignment RtlTranslateAlignment(ContentAlignment align)
		{
			return this.RtlTranslateContent(align);
		}

		/// <summary>Converts the specified <see cref="T:System.Windows.Forms.HorizontalAlignment" /> to the appropriate <see cref="T:System.Windows.Forms.HorizontalAlignment" /> to support right-to-left text.</summary>
		/// <param name="align">One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</returns>
		// Token: 0x06001213 RID: 4627 RVA: 0x00039403 File Offset: 0x00037603
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected HorizontalAlignment RtlTranslateHorizontal(HorizontalAlignment align)
		{
			if (RightToLeft.Yes == this.RightToLeft)
			{
				if (align == HorizontalAlignment.Left)
				{
					return HorizontalAlignment.Right;
				}
				if (HorizontalAlignment.Right == align)
				{
					return HorizontalAlignment.Left;
				}
			}
			return align;
		}

		/// <summary>Converts the specified <see cref="T:System.Windows.Forms.LeftRightAlignment" /> to the appropriate <see cref="T:System.Windows.Forms.LeftRightAlignment" /> to support right-to-left text.</summary>
		/// <param name="align">One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values.</returns>
		// Token: 0x06001214 RID: 4628 RVA: 0x00039403 File Offset: 0x00037603
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected LeftRightAlignment RtlTranslateLeftRight(LeftRightAlignment align)
		{
			if (RightToLeft.Yes == this.RightToLeft)
			{
				if (align == LeftRightAlignment.Left)
				{
					return LeftRightAlignment.Right;
				}
				if (LeftRightAlignment.Right == align)
				{
					return LeftRightAlignment.Left;
				}
			}
			return align;
		}

		/// <summary>Converts the specified <see cref="T:System.Drawing.ContentAlignment" /> to the appropriate <see cref="T:System.Drawing.ContentAlignment" /> to support right-to-left text.</summary>
		/// <param name="align">One of the <see cref="T:System.Drawing.ContentAlignment" /> values.</param>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values.</returns>
		// Token: 0x06001215 RID: 4629 RVA: 0x0003941C File Offset: 0x0003761C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal ContentAlignment RtlTranslateContent(ContentAlignment align)
		{
			if (RightToLeft.Yes == this.RightToLeft)
			{
				if ((align & WindowsFormsUtils.AnyTopAlign) != (ContentAlignment)0)
				{
					if (align == ContentAlignment.TopLeft)
					{
						return ContentAlignment.TopRight;
					}
					if (align == ContentAlignment.TopRight)
					{
						return ContentAlignment.TopLeft;
					}
				}
				if ((align & WindowsFormsUtils.AnyMiddleAlign) != (ContentAlignment)0)
				{
					if (align == ContentAlignment.MiddleLeft)
					{
						return ContentAlignment.MiddleRight;
					}
					if (align == ContentAlignment.MiddleRight)
					{
						return ContentAlignment.MiddleLeft;
					}
				}
				if ((align & WindowsFormsUtils.AnyBottomAlign) != (ContentAlignment)0)
				{
					if (align == ContentAlignment.BottomLeft)
					{
						return ContentAlignment.BottomRight;
					}
					if (align == ContentAlignment.BottomRight)
					{
						return ContentAlignment.BottomLeft;
					}
				}
			}
			return align;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0003948C File Offset: 0x0003768C
		private void SetWindowFont()
		{
			this.SendMessage(48, this.FontHandle, 0);
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000394A0 File Offset: 0x000376A0
		private void SetWindowStyle(int flag, bool value)
		{
			int num = (int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, this.Handle), -16);
			UnsafeNativeMethods.SetWindowLong(new HandleRef(this, this.Handle), -16, new HandleRef(null, (IntPtr)(value ? (num | flag) : (num & ~flag))));
		}

		/// <summary>Displays the control to the user.</summary>
		// Token: 0x06001218 RID: 4632 RVA: 0x000380E2 File Offset: 0x000362E2
		public void Show()
		{
			this.Visible = true;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000394F4 File Offset: 0x000376F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal bool ShouldSerializeMargin()
		{
			return !this.Margin.Equals(this.DefaultMargin);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00039523 File Offset: 0x00037723
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeMaximumSize()
		{
			return this.MaximumSize != this.DefaultMaximumSize;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00039536 File Offset: 0x00037736
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeMinimumSize()
		{
			return this.MinimumSize != this.DefaultMinimumSize;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0003954C File Offset: 0x0003774C
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal bool ShouldSerializePadding()
		{
			return !this.Padding.Equals(this.DefaultPadding);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0003957C File Offset: 0x0003777C
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeSize()
		{
			Size defaultSize = this.DefaultSize;
			return this.width != defaultSize.Width || this.height != defaultSize.Height;
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x000395B3 File Offset: 0x000377B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeText()
		{
			return this.Text.Length != 0;
		}

		/// <summary>Temporarily suspends the layout logic for the control.</summary>
		// Token: 0x0600121F RID: 4639 RVA: 0x000395C3 File Offset: 0x000377C3
		public void SuspendLayout()
		{
			this.layoutSuspendCount += 1;
			if (this.layoutSuspendCount == 1)
			{
				this.OnLayoutSuspended();
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x000395E3 File Offset: 0x000377E3
		private void UnhookMouseEvent()
		{
			this.SetState(16384, false);
		}

		/// <summary>Causes the control to redraw the invalidated regions within its client area.</summary>
		// Token: 0x06001221 RID: 4641 RVA: 0x000395F1 File Offset: 0x000377F1
		public void Update()
		{
			SafeNativeMethods.UpdateWindow(new HandleRef(this.window, this.InternalHandle));
		}

		/// <summary>Updates the bounds of the control with the current size and location.</summary>
		// Token: 0x06001222 RID: 4642 RVA: 0x0003960C File Offset: 0x0003780C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal void UpdateBounds()
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetClientRect(new HandleRef(this.window, this.InternalHandle), ref rect);
			int right = rect.right;
			int bottom = rect.bottom;
			UnsafeNativeMethods.GetWindowRect(new HandleRef(this.window, this.InternalHandle), ref rect);
			if (!this.GetTopLevel())
			{
				UnsafeNativeMethods.MapWindowPoints(NativeMethods.NullHandleRef, new HandleRef(null, UnsafeNativeMethods.GetParent(new HandleRef(this.window, this.InternalHandle))), ref rect, 2);
			}
			this.UpdateBounds(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top, right, bottom);
		}

		/// <summary>Updates the bounds of the control with the specified size and location.</summary>
		/// <param name="x">The <see cref="P:System.Drawing.Point.X" /> coordinate of the control.</param>
		/// <param name="y">The <see cref="P:System.Drawing.Point.Y" /> coordinate of the control.</param>
		/// <param name="width">The <see cref="P:System.Drawing.Size.Width" /> of the control.</param>
		/// <param name="height">The <see cref="P:System.Drawing.Size.Height" /> of the control.</param>
		// Token: 0x06001223 RID: 4643 RVA: 0x000396C4 File Offset: 0x000378C4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void UpdateBounds(int x, int y, int width, int height)
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			rect.left = (rect.right = (rect.top = (rect.bottom = 0)));
			CreateParams createParams = this.CreateParams;
			this.AdjustWindowRectEx(ref rect, createParams.Style, false, createParams.ExStyle);
			int num = width - (rect.right - rect.left);
			int num2 = height - (rect.bottom - rect.top);
			this.UpdateBounds(x, y, width, height, num, num2);
		}

		/// <summary>Updates the bounds of the control with the specified size, location, and client size.</summary>
		/// <param name="x">The <see cref="P:System.Drawing.Point.X" /> coordinate of the control.</param>
		/// <param name="y">The <see cref="P:System.Drawing.Point.Y" /> coordinate of the control.</param>
		/// <param name="width">The <see cref="P:System.Drawing.Size.Width" /> of the control.</param>
		/// <param name="height">The <see cref="P:System.Drawing.Size.Height" /> of the control.</param>
		/// <param name="clientWidth">The client <see cref="P:System.Drawing.Size.Width" /> of the control.</param>
		/// <param name="clientHeight">The client <see cref="P:System.Drawing.Size.Height" /> of the control.</param>
		// Token: 0x06001224 RID: 4644 RVA: 0x00039750 File Offset: 0x00037950
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void UpdateBounds(int x, int y, int width, int height, int clientWidth, int clientHeight)
		{
			bool flag = this.x != x || this.y != y;
			bool flag2 = this.Width != width || this.Height != height || this.clientWidth != clientWidth || this.clientHeight != clientHeight;
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
			this.clientWidth = clientWidth;
			this.clientHeight = clientHeight;
			if (flag)
			{
				this.OnLocationChanged(EventArgs.Empty);
			}
			if (flag2)
			{
				this.OnSizeChanged(EventArgs.Empty);
				this.OnClientSizeChanged(EventArgs.Empty);
				CommonProperties.xClearPreferredSizeCache(this);
				LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
			}
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00039810 File Offset: 0x00037A10
		private void UpdateBindings()
		{
			for (int i = 0; i < this.DataBindings.Count; i++)
			{
				BindingContext.UpdateBinding(this.BindingContext, this.DataBindings[i]);
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0003984C File Offset: 0x00037A4C
		private void UpdateChildControlIndex(Control ctl)
		{
			if (!LocalAppContextSwitches.AllowUpdateChildControlIndexForTabControls && base.GetType().IsAssignableFrom(typeof(TabControl)))
			{
				return;
			}
			int num = 0;
			int childIndex = this.Controls.GetChildIndex(ctl);
			IntPtr internalHandle = ctl.InternalHandle;
			while ((internalHandle = UnsafeNativeMethods.GetWindow(new HandleRef(null, internalHandle), 3)) != IntPtr.Zero)
			{
				Control control = Control.FromHandleInternal(internalHandle);
				if (control != null)
				{
					num = this.Controls.GetChildIndex(control, false) + 1;
					break;
				}
			}
			if (num > childIndex)
			{
				num--;
			}
			if (num != childIndex)
			{
				this.Controls.SetChildIndex(ctl, num);
			}
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x000398E0 File Offset: 0x00037AE0
		private void UpdateReflectParent(bool findNewParent)
		{
			if (!this.Disposing && findNewParent && this.IsHandleCreated)
			{
				IntPtr intPtr = UnsafeNativeMethods.GetParent(new HandleRef(this, this.Handle));
				if (intPtr != IntPtr.Zero)
				{
					this.ReflectParent = Control.FromHandleInternal(intPtr);
					return;
				}
			}
			this.ReflectParent = null;
		}

		/// <summary>Updates the control in its parent's z-order.</summary>
		// Token: 0x06001228 RID: 4648 RVA: 0x00039935 File Offset: 0x00037B35
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void UpdateZOrder()
		{
			if (this.parent != null)
			{
				this.parent.UpdateChildZOrder(this);
			}
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0003994C File Offset: 0x00037B4C
		private void UpdateChildZOrder(Control ctl)
		{
			if (!this.IsHandleCreated || !ctl.IsHandleCreated || ctl.parent != this)
			{
				return;
			}
			IntPtr intPtr = (IntPtr)NativeMethods.HWND_TOP;
			int num = this.Controls.GetChildIndex(ctl);
			while (--num >= 0)
			{
				Control control = this.Controls[num];
				if (control.IsHandleCreated && control.parent == this)
				{
					intPtr = control.Handle;
					break;
				}
			}
			if (UnsafeNativeMethods.GetWindow(new HandleRef(ctl.window, ctl.Handle), 3) != intPtr)
			{
				this.state |= 256;
				try
				{
					SafeNativeMethods.SetWindowPos(new HandleRef(ctl.window, ctl.Handle), new HandleRef(null, intPtr), 0, 0, 0, 0, 3);
				}
				finally
				{
					this.state &= -257;
				}
			}
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00039A38 File Offset: 0x00037C38
		private void UpdateRoot()
		{
			this.window.LockReference(this.GetTopLevel() && this.Visible);
		}

		/// <summary>Forces the assigned styles to be reapplied to the control.</summary>
		// Token: 0x0600122B RID: 4651 RVA: 0x00039A56 File Offset: 0x00037C56
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void UpdateStyles()
		{
			this.UpdateStylesCore();
			this.OnStyleChanged(EventArgs.Empty);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00039A6C File Offset: 0x00037C6C
		internal virtual void UpdateStylesCore()
		{
			if (this.IsHandleCreated)
			{
				CreateParams createParams = this.CreateParams;
				int windowStyle = this.WindowStyle;
				int windowExStyle = this.WindowExStyle;
				if ((this.state & 2) != 0)
				{
					createParams.Style |= 268435456;
				}
				if (windowStyle != createParams.Style)
				{
					this.WindowStyle = createParams.Style;
				}
				if (windowExStyle != createParams.ExStyle)
				{
					this.WindowExStyle = createParams.ExStyle;
					this.SetState(1073741824, (createParams.ExStyle & 4194304) != 0);
				}
				SafeNativeMethods.SetWindowPos(new HandleRef(this, this.Handle), NativeMethods.NullHandleRef, 0, 0, 0, 0, 55);
				this.Invalidate(true);
			}
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00039B1D File Offset: 0x00037D1D
		private void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
		{
			if (pref.Category == UserPreferenceCategory.Color)
			{
				Control.defaultFont = null;
				this.OnSystemColorsChanged(EventArgs.Empty);
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnBoundsUpdate(int x, int y, int width, int height)
		{
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00039B39 File Offset: 0x00037D39
		internal void WindowAssignHandle(IntPtr handle, bool value)
		{
			this.window.AssignHandle(handle, value);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00039B48 File Offset: 0x00037D48
		internal void WindowReleaseHandle()
		{
			this.window.ReleaseHandle();
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00039B58 File Offset: 0x00037D58
		private void WmClose(ref Message m)
		{
			if (this.ParentInternal != null)
			{
				IntPtr handle = this.Handle;
				IntPtr intPtr = handle;
				while (handle != IntPtr.Zero)
				{
					intPtr = handle;
					handle = UnsafeNativeMethods.GetParent(new HandleRef(null, handle));
					int num = (int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(null, intPtr), -16);
					if ((num & 1073741824) == 0)
					{
						break;
					}
				}
				if (intPtr != IntPtr.Zero)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(null, intPtr), 16, IntPtr.Zero, IntPtr.Zero);
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00039BDF File Offset: 0x00037DDF
		private void WmCaptureChanged(ref Message m)
		{
			this.OnMouseCaptureChanged(EventArgs.Empty);
			this.DefWndProc(ref m);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00039BF3 File Offset: 0x00037DF3
		private void WmCommand(ref Message m)
		{
			if (IntPtr.Zero == m.LParam)
			{
				if (Command.DispatchID(NativeMethods.Util.LOWORD(m.WParam)))
				{
					return;
				}
			}
			else if (Control.ReflectMessageInternal(m.LParam, ref m))
			{
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00039C30 File Offset: 0x00037E30
		internal virtual void WmContextMenu(ref Message m)
		{
			this.WmContextMenu(ref m, this);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00039C3C File Offset: 0x00037E3C
		internal void WmContextMenu(ref Message m, Control sourceControl)
		{
			ContextMenu contextMenu = this.Properties.GetObject(Control.PropContextMenu) as ContextMenu;
			ContextMenuStrip contextMenuStrip = ((contextMenu != null) ? null : (this.Properties.GetObject(Control.PropContextMenuStrip) as ContextMenuStrip));
			if (contextMenu == null && contextMenuStrip == null)
			{
				this.DefWndProc(ref m);
				return;
			}
			int num = NativeMethods.Util.SignedLOWORD(m.LParam);
			int num2 = NativeMethods.Util.SignedHIWORD(m.LParam);
			bool flag = false;
			Point point;
			if ((int)(long)m.LParam == -1)
			{
				flag = true;
				point = new Point(this.Width / 2, this.Height / 2);
			}
			else
			{
				point = this.PointToClientInternal(new Point(num, num2));
			}
			if (!this.ClientRectangle.Contains(point))
			{
				this.DefWndProc(ref m);
				return;
			}
			if (contextMenu != null)
			{
				contextMenu.Show(sourceControl, point);
				return;
			}
			if (contextMenuStrip != null)
			{
				contextMenuStrip.ShowInternal(sourceControl, point, flag);
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00039D20 File Offset: 0x00037F20
		private void WmCtlColorControl(ref Message m)
		{
			Control control = Control.FromHandleInternal(m.LParam);
			if (control != null)
			{
				m.Result = control.InitializeDCForWmCtlColor(m.WParam, m.Msg);
				if (m.Result != IntPtr.Zero)
				{
					return;
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00039D6E File Offset: 0x00037F6E
		private void WmDisplayChange(ref Message m)
		{
			BufferedGraphicsManager.Current.Invalidate();
			this.DefWndProc(ref m);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00039D81 File Offset: 0x00037F81
		private void WmDrawItem(ref Message m)
		{
			if (m.WParam == IntPtr.Zero)
			{
				this.WmDrawItemMenuItem(ref m);
				return;
			}
			this.WmOwnerDraw(ref m);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00039DA4 File Offset: 0x00037FA4
		private void WmDrawItemMenuItem(ref Message m)
		{
			NativeMethods.DRAWITEMSTRUCT drawitemstruct = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
			MenuItem menuItemFromItemData = MenuItem.GetMenuItemFromItemData(drawitemstruct.itemData);
			if (menuItemFromItemData != null)
			{
				menuItemFromItemData.WmDrawItem(ref m);
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00039DE0 File Offset: 0x00037FE0
		private void WmEraseBkgnd(ref Message m)
		{
			if (this.GetStyle(ControlStyles.UserPaint))
			{
				if (!this.GetStyle(ControlStyles.AllPaintingInWmPaint))
				{
					IntPtr wparam = m.WParam;
					if (wparam == IntPtr.Zero)
					{
						m.Result = (IntPtr)0;
						return;
					}
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					UnsafeNativeMethods.GetClientRect(new HandleRef(this, this.Handle), ref rect);
					using (PaintEventArgs paintEventArgs = new PaintEventArgs(wparam, Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom)))
					{
						this.PaintWithErrorHandling(paintEventArgs, 1);
					}
				}
				m.Result = (IntPtr)1;
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00039EA0 File Offset: 0x000380A0
		private void WmExitMenuLoop(ref Message m)
		{
			bool flag = (int)(long)m.WParam != 0;
			if (flag)
			{
				ContextMenu contextMenu = (ContextMenu)this.Properties.GetObject(Control.PropContextMenu);
				if (contextMenu != null)
				{
					contextMenu.OnCollapse(EventArgs.Empty);
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00039EF0 File Offset: 0x000380F0
		private void WmGetControlName(ref Message m)
		{
			string text;
			if (this.Site != null)
			{
				text = this.Site.Name;
			}
			else
			{
				text = this.Name;
			}
			if (text == null)
			{
				text = "";
			}
			this.MarshalStringToMessage(text, ref m);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00039F2C File Offset: 0x0003812C
		private void WmGetControlType(ref Message m)
		{
			string assemblyQualifiedName = base.GetType().AssemblyQualifiedName;
			this.MarshalStringToMessage(assemblyQualifiedName, ref m);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00039F50 File Offset: 0x00038150
		private void WmGetObject(ref Message m)
		{
			if (m.Msg == 61 && m.LParam == (IntPtr)(-25) && this.SupportsUiaProviders)
			{
				m.Result = UnsafeNativeMethods.UiaReturnRawElementProvider(new HandleRef(this, this.Handle), m.WParam, m.LParam, this.UnsafeAccessibilityObject);
				return;
			}
			UnsafeNativeMethods.IAccessibleInternal internalAccessibilityObject = this.GetInternalAccessibilityObject((int)(long)m.LParam);
			if (internalAccessibilityObject != null)
			{
				Guid guid = new Guid("{618736E0-3C3D-11CF-810C-00AA00389B71}");
				try
				{
					object obj = internalAccessibilityObject;
					IAccessible accessible = obj as IAccessible;
					if (accessible != null)
					{
						throw new InvalidOperationException(SR.GetString("ControlAccessibileObjectInvalid"));
					}
					if (internalAccessibilityObject == null)
					{
						m.Result = (IntPtr)0;
					}
					else
					{
						IntPtr iunknownForObject = Marshal.GetIUnknownForObject(internalAccessibilityObject);
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							m.Result = UnsafeNativeMethods.LresultFromObject(ref guid, m.WParam, new HandleRef(internalAccessibilityObject, iunknownForObject));
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
							Marshal.Release(iunknownForObject);
						}
					}
					return;
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException(SR.GetString("RichControlLresult"), ex);
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0003A074 File Offset: 0x00038274
		private void WmHelp(ref Message m)
		{
			HelpInfo helpInfo = MessageBox.HelpInfo;
			if (helpInfo != null)
			{
				switch (helpInfo.Option)
				{
				case 1:
					Help.ShowHelp(this, helpInfo.HelpFilePath);
					break;
				case 2:
					Help.ShowHelp(this, helpInfo.HelpFilePath, helpInfo.Keyword);
					break;
				case 3:
					Help.ShowHelp(this, helpInfo.HelpFilePath, helpInfo.Navigator);
					break;
				case 4:
					Help.ShowHelp(this, helpInfo.HelpFilePath, helpInfo.Navigator, helpInfo.Param);
					break;
				}
			}
			NativeMethods.HELPINFO helpinfo = (NativeMethods.HELPINFO)m.GetLParam(typeof(NativeMethods.HELPINFO));
			HelpEventArgs helpEventArgs = new HelpEventArgs(new Point(helpinfo.MousePos.x, helpinfo.MousePos.y));
			this.OnHelpRequested(helpEventArgs);
			if (!helpEventArgs.Handled)
			{
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0003A148 File Offset: 0x00038348
		private void WmInitMenuPopup(ref Message m)
		{
			ContextMenu contextMenu = (ContextMenu)this.Properties.GetObject(Control.PropContextMenu);
			if (contextMenu != null && contextMenu.ProcessInitMenuPopup(m.WParam))
			{
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0003A184 File Offset: 0x00038384
		private void WmMeasureItem(ref Message m)
		{
			if (m.WParam == IntPtr.Zero)
			{
				NativeMethods.MEASUREITEMSTRUCT measureitemstruct = (NativeMethods.MEASUREITEMSTRUCT)m.GetLParam(typeof(NativeMethods.MEASUREITEMSTRUCT));
				MenuItem menuItemFromItemData = MenuItem.GetMenuItemFromItemData(measureitemstruct.itemData);
				if (menuItemFromItemData != null)
				{
					menuItemFromItemData.WmMeasureItem(ref m);
					return;
				}
			}
			else
			{
				this.WmOwnerDraw(ref m);
			}
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0003A1D8 File Offset: 0x000383D8
		private void WmMenuChar(ref Message m)
		{
			Menu contextMenu = this.ContextMenu;
			if (contextMenu != null)
			{
				contextMenu.WmMenuChar(ref m);
				m.Result != IntPtr.Zero;
				return;
			}
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0003A208 File Offset: 0x00038408
		private void WmMenuSelect(ref Message m)
		{
			int num = NativeMethods.Util.LOWORD(m.WParam);
			int num2 = NativeMethods.Util.HIWORD(m.WParam);
			IntPtr lparam = m.LParam;
			MenuItem menuItem = null;
			if ((num2 & 8192) == 0)
			{
				if ((num2 & 16) == 0)
				{
					Command commandFromID = Command.GetCommandFromID(num);
					if (commandFromID != null)
					{
						object target = commandFromID.Target;
						if (target != null && target is MenuItem.MenuItemData)
						{
							menuItem = ((MenuItem.MenuItemData)target).baseItem;
						}
					}
				}
				else
				{
					menuItem = this.GetMenuItemFromHandleId(lparam, num);
				}
			}
			if (menuItem != null)
			{
				menuItem.PerformSelect();
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0003A290 File Offset: 0x00038490
		private void WmCreate(ref Message m)
		{
			this.DefWndProc(ref m);
			if (this.parent != null)
			{
				this.parent.UpdateChildZOrder(this);
			}
			this.UpdateBounds();
			this.OnHandleCreated(EventArgs.Empty);
			if (!this.GetStyle(ControlStyles.CacheText))
			{
				this.text = null;
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0003A2E0 File Offset: 0x000384E0
		private void WmDestroy(ref Message m)
		{
			if (!this.RecreatingHandle && !this.Disposing && !this.IsDisposed && this.GetState(16384))
			{
				this.OnMouseLeave(EventArgs.Empty);
				this.UnhookMouseEvent();
			}
			if (this.SupportsUiaProviders)
			{
				this.ReleaseUiaProvider(this.HandleInternal);
			}
			else if (LocalAppContextSwitches.DisconnectUiaProvidersOnWmDestroy && this.IsInternalAccessibilityObjectCreated)
			{
				this.UnsafeAccessibilityObject.ClearAccessibleObject();
				this.Properties.SetObject(Control.PropUnsafeAccessibility, null);
			}
			this.OnHandleDestroyed(EventArgs.Empty);
			if (!this.Disposing)
			{
				if (!this.RecreatingHandle)
				{
					this.SetState(1, false);
				}
			}
			else
			{
				this.SetState(2, false);
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0003A398 File Offset: 0x00038598
		private void WmKeyChar(ref Message m)
		{
			if (this.ProcessKeyMessage(ref m))
			{
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0003A3AB File Offset: 0x000385AB
		private void WmKillFocus(ref Message m)
		{
			this.WmImeKillFocus();
			this.DefWndProc(ref m);
			this.InvokeLostFocus(this, EventArgs.Empty);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0003A3C8 File Offset: 0x000385C8
		private void WmMouseDown(ref Message m, MouseButtons button, int clicks)
		{
			MouseButtons mouseButtons = Control.MouseButtons;
			this.SetState(134217728, true);
			if (!this.GetStyle(ControlStyles.UserMouse))
			{
				this.DefWndProc(ref m);
				if (this.IsDisposed)
				{
					return;
				}
			}
			else if (button == MouseButtons.Left && this.GetStyle(ControlStyles.Selectable))
			{
				this.FocusInternal();
			}
			if (mouseButtons != Control.MouseButtons)
			{
				return;
			}
			if (!this.GetState2(16))
			{
				this.CaptureInternal = true;
			}
			if (mouseButtons != Control.MouseButtons)
			{
				return;
			}
			if (this.Enabled)
			{
				this.OnMouseDown(new MouseEventArgs(button, clicks, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
			}
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0003A46F File Offset: 0x0003866F
		private void WmMouseEnter(ref Message m)
		{
			this.DefWndProc(ref m);
			if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
			{
				KeyboardToolTipStateMachine.Instance.NotifyAboutMouseEnter(this);
			}
			this.OnMouseEnter(EventArgs.Empty);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0003A495 File Offset: 0x00038695
		private void WmMouseLeave(ref Message m)
		{
			this.DefWndProc(ref m);
			this.OnMouseLeave(EventArgs.Empty);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0003A4AC File Offset: 0x000386AC
		private void WmDpiChangedBeforeParent(ref Message m)
		{
			this.DefWndProc(ref m);
			if (this.IsHandleCreated)
			{
				int num = this.deviceDpi;
				this.deviceDpi = (int)UnsafeNativeMethods.GetDpiForWindow(new HandleRef(this, this.HandleInternal));
				if (num != this.deviceDpi)
				{
					if (DpiHelper.EnableDpiChangedHighDpiImprovements)
					{
						Font font = (Font)this.Properties.GetObject(Control.PropFont);
						if (font != null)
						{
							float num2 = (float)this.deviceDpi / (float)num;
							this.Font = new Font(font.FontFamily, font.Size * num2, font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
						}
					}
					this.RescaleConstantsForDpi(num, this.deviceDpi);
				}
			}
			this.OnDpiChangedBeforeParent(EventArgs.Empty);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0003A568 File Offset: 0x00038768
		private void WmDpiChangedAfterParent(ref Message m)
		{
			this.DefWndProc(ref m);
			uint dpiForWindow = UnsafeNativeMethods.GetDpiForWindow(new HandleRef(this, this.HandleInternal));
			this.OnDpiChangedAfterParent(EventArgs.Empty);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0003A599 File Offset: 0x00038799
		private void WmMouseHover(ref Message m)
		{
			this.DefWndProc(ref m);
			this.OnMouseHover(EventArgs.Empty);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0003A5AD File Offset: 0x000387AD
		private void WmMouseMove(ref Message m)
		{
			if (!this.GetStyle(ControlStyles.UserMouse))
			{
				this.DefWndProc(ref m);
			}
			this.OnMouseMove(new MouseEventArgs(Control.MouseButtons, 0, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0003A5EC File Offset: 0x000387EC
		private void WmMouseUp(ref Message m, MouseButtons button, int clicks)
		{
			try
			{
				int num = NativeMethods.Util.SignedLOWORD(m.LParam);
				int num2 = NativeMethods.Util.SignedHIWORD(m.LParam);
				Point point = new Point(num, num2);
				point = this.PointToScreen(point);
				if (!this.GetStyle(ControlStyles.UserMouse))
				{
					this.DefWndProc(ref m);
				}
				else if (button == MouseButtons.Right)
				{
					this.SendMessage(123, this.Handle, NativeMethods.Util.MAKELPARAM(point.X, point.Y));
				}
				bool flag = false;
				if ((this.controlStyle & ControlStyles.StandardClick) == ControlStyles.StandardClick && this.GetState(134217728) && !this.IsDisposed && UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == this.Handle)
				{
					flag = true;
				}
				if (flag && !this.ValidationCancelled)
				{
					if (!this.GetState(67108864))
					{
						this.OnClick(new MouseEventArgs(button, clicks, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
						this.OnMouseClick(new MouseEventArgs(button, clicks, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
					}
					else
					{
						this.OnDoubleClick(new MouseEventArgs(button, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
						this.OnMouseDoubleClick(new MouseEventArgs(button, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
					}
				}
				this.OnMouseUp(new MouseEventArgs(button, clicks, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
			}
			finally
			{
				this.SetState(67108864, false);
				this.SetState(134217728, false);
				this.SetState(268435456, false);
				this.CaptureInternal = false;
			}
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0003A7CC File Offset: 0x000389CC
		private void WmMouseWheel(ref Message m)
		{
			Point point = new Point(NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam));
			point = this.PointToClient(point);
			HandledMouseEventArgs handledMouseEventArgs = new HandledMouseEventArgs(MouseButtons.None, 0, point.X, point.Y, NativeMethods.Util.SignedHIWORD(m.WParam));
			this.OnMouseWheel(handledMouseEventArgs);
			m.Result = (IntPtr)(handledMouseEventArgs.Handled ? 0 : 1);
			if (!handledMouseEventArgs.Handled)
			{
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0003A84C File Offset: 0x00038A4C
		private void WmMove(ref Message m)
		{
			this.DefWndProc(ref m);
			this.UpdateBounds();
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0003A85C File Offset: 0x00038A5C
		private unsafe void WmNotify(ref Message m)
		{
			NativeMethods.NMHDR* ptr = (NativeMethods.NMHDR*)(void*)m.LParam;
			if (!Control.ReflectMessageInternal(ptr->hwndFrom, ref m))
			{
				if (ptr->code == -521)
				{
					m.Result = UnsafeNativeMethods.SendMessage(new HandleRef(null, ptr->hwndFrom), 8192 + m.Msg, m.WParam, m.LParam);
					return;
				}
				if (ptr->code == -522)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(null, ptr->hwndFrom), 8192 + m.Msg, m.WParam, m.LParam);
				}
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0003A8FE File Offset: 0x00038AFE
		private void WmNotifyFormat(ref Message m)
		{
			if (!Control.ReflectMessageInternal(m.WParam, ref m))
			{
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0003A918 File Offset: 0x00038B18
		private void WmOwnerDraw(ref Message m)
		{
			bool flag = false;
			int num = (int)(long)m.WParam;
			IntPtr intPtr = UnsafeNativeMethods.GetDlgItem(new HandleRef(null, m.HWnd), num);
			if (intPtr == IntPtr.Zero)
			{
				intPtr = (IntPtr)((long)num);
			}
			if (!Control.ReflectMessageInternal(intPtr, ref m))
			{
				IntPtr handleFromID = this.window.GetHandleFromID((short)NativeMethods.Util.LOWORD(m.WParam));
				if (handleFromID != IntPtr.Zero)
				{
					Control control = Control.FromHandleInternal(handleFromID);
					if (control != null)
					{
						m.Result = control.SendMessage(8192 + m.Msg, handleFromID, m.LParam);
						flag = true;
					}
				}
			}
			else
			{
				flag = true;
			}
			if (!flag)
			{
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0003A9C8 File Offset: 0x00038BC8
		private void WmPaint(ref Message m)
		{
			bool flag = this.DoubleBuffered || (this.GetStyle(ControlStyles.AllPaintingInWmPaint) && this.DoubleBufferingEnabled);
			IntPtr intPtr = IntPtr.Zero;
			NativeMethods.PAINTSTRUCT paintstruct = default(NativeMethods.PAINTSTRUCT);
			bool flag2 = false;
			try
			{
				IntPtr intPtr2;
				Rectangle rectangle;
				if (m.WParam == IntPtr.Zero)
				{
					intPtr = this.Handle;
					intPtr2 = UnsafeNativeMethods.BeginPaint(new HandleRef(this, intPtr), ref paintstruct);
					if (intPtr2 == IntPtr.Zero)
					{
						return;
					}
					flag2 = true;
					rectangle = new Rectangle(paintstruct.rcPaint_left, paintstruct.rcPaint_top, paintstruct.rcPaint_right - paintstruct.rcPaint_left, paintstruct.rcPaint_bottom - paintstruct.rcPaint_top);
				}
				else
				{
					intPtr2 = m.WParam;
					rectangle = this.ClientRectangle;
				}
				if (!flag || (rectangle.Width > 0 && rectangle.Height > 0))
				{
					IntPtr intPtr3 = IntPtr.Zero;
					BufferedGraphics bufferedGraphics = null;
					PaintEventArgs paintEventArgs = null;
					GraphicsState graphicsState = null;
					try
					{
						if (flag || m.WParam == IntPtr.Zero)
						{
							intPtr3 = Control.SetUpPalette(intPtr2, false, false);
						}
						if (flag)
						{
							try
							{
								bufferedGraphics = this.BufferContext.Allocate(intPtr2, this.ClientRectangle);
							}
							catch (Exception ex)
							{
								if (ClientUtils.IsCriticalException(ex) && !(ex is OutOfMemoryException))
								{
									throw;
								}
								flag = false;
							}
						}
						if (bufferedGraphics != null)
						{
							bufferedGraphics.Graphics.SetClip(rectangle);
							paintEventArgs = new PaintEventArgs(bufferedGraphics.Graphics, rectangle);
							graphicsState = paintEventArgs.Graphics.Save();
						}
						else
						{
							paintEventArgs = new PaintEventArgs(intPtr2, rectangle);
						}
						using (paintEventArgs)
						{
							try
							{
								if ((m.WParam == IntPtr.Zero && this.GetStyle(ControlStyles.AllPaintingInWmPaint)) || flag)
								{
									this.PaintWithErrorHandling(paintEventArgs, 1);
								}
							}
							finally
							{
								if (graphicsState != null)
								{
									paintEventArgs.Graphics.Restore(graphicsState);
								}
								else
								{
									paintEventArgs.ResetGraphics();
								}
							}
							this.PaintWithErrorHandling(paintEventArgs, 2);
							if (bufferedGraphics != null)
							{
								bufferedGraphics.Render();
							}
						}
					}
					finally
					{
						if (intPtr3 != IntPtr.Zero)
						{
							SafeNativeMethods.SelectPalette(new HandleRef(null, intPtr2), new HandleRef(null, intPtr3), 0);
						}
						if (bufferedGraphics != null)
						{
							bufferedGraphics.Dispose();
						}
					}
				}
			}
			finally
			{
				if (flag2)
				{
					UnsafeNativeMethods.EndPaint(new HandleRef(this, intPtr), ref paintstruct);
				}
			}
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0003AC74 File Offset: 0x00038E74
		private void WmPrintClient(ref Message m)
		{
			using (PaintEventArgs paintEventArgs = new Control.PrintPaintEventArgs(m, m.WParam, this.ClientRectangle))
			{
				this.OnPrint(paintEventArgs);
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0003ACBC File Offset: 0x00038EBC
		private void WmQueryNewPalette(ref Message m)
		{
			IntPtr dc = UnsafeNativeMethods.GetDC(new HandleRef(this, this.Handle));
			try
			{
				Control.SetUpPalette(dc, true, true);
			}
			finally
			{
				UnsafeNativeMethods.ReleaseDC(new HandleRef(this, this.Handle), new HandleRef(null, dc));
			}
			this.Invalidate(true);
			m.Result = (IntPtr)1;
			this.DefWndProc(ref m);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0003AD2C File Offset: 0x00038F2C
		private void WmSetCursor(ref Message m)
		{
			if (m.WParam == this.InternalHandle && NativeMethods.Util.LOWORD(m.LParam) == 1)
			{
				Cursor.CurrentInternal = this.Cursor;
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0003AD64 File Offset: 0x00038F64
		private unsafe void WmWindowPosChanging(ref Message m)
		{
			if (this.IsActiveX)
			{
				NativeMethods.WINDOWPOS* ptr = (NativeMethods.WINDOWPOS*)(void*)m.LParam;
				bool flag = false;
				if ((ptr->flags & 2) == 0 && (ptr->x != this.Left || ptr->y != this.Top))
				{
					flag = true;
				}
				if ((ptr->flags & 1) == 0 && (ptr->cx != this.Width || ptr->cy != this.Height))
				{
					flag = true;
				}
				if (flag)
				{
					this.ActiveXUpdateBounds(ref ptr->x, ref ptr->y, ref ptr->cx, ref ptr->cy, ptr->flags);
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0003AE08 File Offset: 0x00039008
		private void WmParentNotify(ref Message m)
		{
			int num = NativeMethods.Util.LOWORD(m.WParam);
			IntPtr intPtr = IntPtr.Zero;
			if (num != 1)
			{
				if (num != 2)
				{
					intPtr = UnsafeNativeMethods.GetDlgItem(new HandleRef(this, this.Handle), NativeMethods.Util.HIWORD(m.WParam));
				}
			}
			else
			{
				intPtr = m.LParam;
			}
			if (intPtr == IntPtr.Zero || !Control.ReflectMessageInternal(intPtr, ref m))
			{
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0003AE74 File Offset: 0x00039074
		private void WmSetFocus(ref Message m)
		{
			this.WmImeSetFocus();
			if (!this.HostedInWin32DialogManager)
			{
				IContainerControl containerControlInternal = this.GetContainerControlInternal();
				if (containerControlInternal != null)
				{
					ContainerControl containerControl = containerControlInternal as ContainerControl;
					bool flag;
					if (containerControl != null)
					{
						flag = containerControl.ActivateControlInternal(this);
					}
					else
					{
						IntSecurity.ModifyFocus.Assert();
						try
						{
							flag = containerControlInternal.ActivateControl(this);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					if (!flag)
					{
						return;
					}
				}
			}
			this.DefWndProc(ref m);
			this.InvokeGotFocus(this, EventArgs.Empty);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0003AEF0 File Offset: 0x000390F0
		private void WmShowWindow(ref Message m)
		{
			this.DefWndProc(ref m);
			if ((this.state & 16) == 0)
			{
				bool flag = m.WParam != IntPtr.Zero;
				bool visible = this.Visible;
				if (flag)
				{
					bool flag2 = this.GetState(2);
					this.SetState(2, true);
					bool flag3 = false;
					try
					{
						this.CreateControl();
						flag3 = true;
						goto IL_81;
					}
					finally
					{
						if (!flag3)
						{
							this.SetState(2, flag2);
						}
					}
				}
				bool flag4 = this.GetTopLevel();
				if (this.ParentInternal != null)
				{
					flag4 = this.ParentInternal.Visible;
				}
				if (flag4)
				{
					this.SetState(2, false);
				}
				IL_81:
				if (!this.GetState(536870912) && visible != flag)
				{
					this.OnVisibleChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0003AFAC File Offset: 0x000391AC
		private void WmUpdateUIState(ref Message m)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = (this.uiCuesState & 240) != 0;
			bool flag4 = (this.uiCuesState & 15) != 0;
			if (flag3)
			{
				flag = this.ShowKeyboardCues;
			}
			if (flag4)
			{
				flag2 = this.ShowFocusCues;
			}
			this.DefWndProc(ref m);
			int num = NativeMethods.Util.LOWORD(m.WParam);
			if (num == 3)
			{
				return;
			}
			UICues uicues = UICues.None;
			if ((NativeMethods.Util.HIWORD(m.WParam) & 2) != 0)
			{
				bool flag5 = num == 2;
				if (flag5 != flag || !flag3)
				{
					uicues |= UICues.ChangeKeyboard;
					this.uiCuesState &= -241;
					this.uiCuesState |= (flag5 ? 32 : 16);
				}
				if (flag5)
				{
					uicues |= UICues.ShowKeyboard;
				}
			}
			if ((NativeMethods.Util.HIWORD(m.WParam) & 1) != 0)
			{
				bool flag6 = num == 2;
				if (flag6 != flag2 || !flag4)
				{
					uicues |= UICues.ChangeFocus;
					this.uiCuesState &= -16;
					this.uiCuesState |= (flag6 ? 2 : 1);
				}
				if (flag6)
				{
					uicues |= UICues.ShowFocus;
				}
			}
			if ((uicues & UICues.Changed) != UICues.None)
			{
				this.OnChangeUICues(new UICuesEventArgs(uicues));
				this.Invalidate(true);
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0003B0D0 File Offset: 0x000392D0
		private unsafe void WmWindowPosChanged(ref Message m)
		{
			this.DefWndProc(ref m);
			this.UpdateBounds();
			if (this.parent != null && UnsafeNativeMethods.GetParent(new HandleRef(this.window, this.InternalHandle)) == this.parent.InternalHandle && (this.state & 256) == 0)
			{
				NativeMethods.WINDOWPOS* ptr = (NativeMethods.WINDOWPOS*)(void*)m.LParam;
				if ((ptr->flags & 4) == 0)
				{
					this.parent.UpdateChildControlIndex(this);
				}
			}
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x0600125F RID: 4703 RVA: 0x0003B14C File Offset: 0x0003934C
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual void WndProc(ref Message m)
		{
			if ((this.controlStyle & ControlStyles.EnableNotifyMessage) == ControlStyles.EnableNotifyMessage)
			{
				this.OnNotifyMessage(m);
			}
			int msg = m.Msg;
			if (msg <= 261)
			{
				if (msg <= 47)
				{
					if (msg <= 20)
					{
						if (msg <= 15)
						{
							switch (msg)
							{
							case 1:
								this.WmCreate(ref m);
								return;
							case 2:
								this.WmDestroy(ref m);
								return;
							case 3:
								this.WmMove(ref m);
								return;
							case 4:
							case 5:
							case 6:
								goto IL_65D;
							case 7:
								this.WmSetFocus(ref m);
								return;
							case 8:
								this.WmKillFocus(ref m);
								return;
							default:
								if (msg != 15)
								{
									goto IL_65D;
								}
								if (this.GetStyle(ControlStyles.UserPaint))
								{
									this.WmPaint(ref m);
									return;
								}
								this.DefWndProc(ref m);
								return;
							}
						}
						else
						{
							if (msg == 16)
							{
								this.WmClose(ref m);
								return;
							}
							if (msg != 20)
							{
								goto IL_65D;
							}
							this.WmEraseBkgnd(ref m);
							return;
						}
					}
					else if (msg <= 25)
					{
						if (msg == 24)
						{
							this.WmShowWindow(ref m);
							return;
						}
						if (msg != 25)
						{
							goto IL_65D;
						}
					}
					else
					{
						if (msg == 32)
						{
							this.WmSetCursor(ref m);
							return;
						}
						switch (msg)
						{
						case 43:
							this.WmDrawItem(ref m);
							return;
						case 44:
							this.WmMeasureItem(ref m);
							return;
						case 45:
						case 46:
						case 47:
							goto IL_426;
						default:
							goto IL_65D;
						}
					}
				}
				else if (msg <= 71)
				{
					if (msg <= 61)
					{
						if (msg == 57)
						{
							goto IL_426;
						}
						if (msg != 61)
						{
							goto IL_65D;
						}
						this.WmGetObject(ref m);
						return;
					}
					else
					{
						if (msg == 70)
						{
							this.WmWindowPosChanging(ref m);
							return;
						}
						if (msg != 71)
						{
							goto IL_65D;
						}
						this.WmWindowPosChanged(ref m);
						return;
					}
				}
				else if (msg <= 123)
				{
					switch (msg)
					{
					case 78:
						this.WmNotify(ref m);
						return;
					case 79:
					case 82:
					case 84:
						goto IL_65D;
					case 80:
						this.WmInputLangChangeRequest(ref m);
						return;
					case 81:
						this.WmInputLangChange(ref m);
						return;
					case 83:
						this.WmHelp(ref m);
						return;
					case 85:
						this.WmNotifyFormat(ref m);
						return;
					default:
						if (msg != 123)
						{
							goto IL_65D;
						}
						this.WmContextMenu(ref m);
						return;
					}
				}
				else
				{
					if (msg == 126)
					{
						this.WmDisplayChange(ref m);
						return;
					}
					if (msg - 256 > 2 && msg - 260 > 1)
					{
						goto IL_65D;
					}
					this.WmKeyChar(ref m);
					return;
				}
			}
			else if (msg <= 646)
			{
				if (msg <= 296)
				{
					if (msg <= 287)
					{
						switch (msg)
						{
						case 269:
							this.WmImeStartComposition(ref m);
							return;
						case 270:
							this.WmImeEndComposition(ref m);
							return;
						case 271:
						case 272:
						case 275:
						case 278:
							goto IL_65D;
						case 273:
							this.WmCommand(ref m);
							return;
						case 274:
							if (((int)(long)m.WParam & 65520) == 61696 && ToolStripManager.ProcessMenuKey(ref m))
							{
								m.Result = IntPtr.Zero;
								return;
							}
							this.DefWndProc(ref m);
							return;
						case 276:
						case 277:
							goto IL_426;
						case 279:
							this.WmInitMenuPopup(ref m);
							return;
						default:
							if (msg != 287)
							{
								goto IL_65D;
							}
							this.WmMenuSelect(ref m);
							return;
						}
					}
					else
					{
						if (msg == 288)
						{
							this.WmMenuChar(ref m);
							return;
						}
						if (msg != 296)
						{
							goto IL_65D;
						}
						this.WmUpdateUIState(ref m);
						return;
					}
				}
				else if (msg <= 533)
				{
					if (msg - 306 > 6)
					{
						switch (msg)
						{
						case 512:
							this.WmMouseMove(ref m);
							return;
						case 513:
							this.WmMouseDown(ref m, MouseButtons.Left, 1);
							return;
						case 514:
							this.WmMouseUp(ref m, MouseButtons.Left, 1);
							return;
						case 515:
							this.WmMouseDown(ref m, MouseButtons.Left, 2);
							if (this.GetStyle(ControlStyles.StandardDoubleClick))
							{
								this.SetState(67108864, true);
								return;
							}
							return;
						case 516:
							this.WmMouseDown(ref m, MouseButtons.Right, 1);
							return;
						case 517:
							this.WmMouseUp(ref m, MouseButtons.Right, 1);
							return;
						case 518:
							this.WmMouseDown(ref m, MouseButtons.Right, 2);
							if (this.GetStyle(ControlStyles.StandardDoubleClick))
							{
								this.SetState(67108864, true);
								return;
							}
							return;
						case 519:
							this.WmMouseDown(ref m, MouseButtons.Middle, 1);
							return;
						case 520:
							this.WmMouseUp(ref m, MouseButtons.Middle, 1);
							return;
						case 521:
							this.WmMouseDown(ref m, MouseButtons.Middle, 2);
							if (this.GetStyle(ControlStyles.StandardDoubleClick))
							{
								this.SetState(67108864, true);
								return;
							}
							return;
						case 522:
							this.WmMouseWheel(ref m);
							return;
						case 523:
							this.WmMouseDown(ref m, this.GetXButton(NativeMethods.Util.HIWORD(m.WParam)), 1);
							return;
						case 524:
							this.WmMouseUp(ref m, this.GetXButton(NativeMethods.Util.HIWORD(m.WParam)), 1);
							return;
						case 525:
							this.WmMouseDown(ref m, this.GetXButton(NativeMethods.Util.HIWORD(m.WParam)), 2);
							if (this.GetStyle(ControlStyles.StandardDoubleClick))
							{
								this.SetState(67108864, true);
								return;
							}
							return;
						case 526:
						case 527:
						case 529:
						case 531:
						case 532:
							goto IL_65D;
						case 528:
							this.WmParentNotify(ref m);
							return;
						case 530:
							this.WmExitMenuLoop(ref m);
							return;
						case 533:
							this.WmCaptureChanged(ref m);
							return;
						default:
							goto IL_65D;
						}
					}
				}
				else
				{
					if (msg == 642)
					{
						this.WmImeNotify(ref m);
						return;
					}
					if (msg != 646)
					{
						goto IL_65D;
					}
					this.WmImeChar(ref m);
					return;
				}
			}
			else if (msg <= 739)
			{
				if (msg <= 675)
				{
					if (msg == 673)
					{
						this.WmMouseHover(ref m);
						return;
					}
					if (msg != 675)
					{
						goto IL_65D;
					}
					this.WmMouseLeave(ref m);
					return;
				}
				else if (msg != 738)
				{
					if (msg != 739)
					{
						goto IL_65D;
					}
					if (DpiHelper.EnableDpiChangedMessageHandling)
					{
						this.WmDpiChangedAfterParent(ref m);
						m.Result = IntPtr.Zero;
						return;
					}
					return;
				}
				else
				{
					if (DpiHelper.EnableDpiChangedMessageHandling)
					{
						this.WmDpiChangedBeforeParent(ref m);
						m.Result = IntPtr.Zero;
						return;
					}
					return;
				}
			}
			else if (msg <= 792)
			{
				if (msg == 783)
				{
					this.WmQueryNewPalette(ref m);
					return;
				}
				if (msg != 792)
				{
					goto IL_65D;
				}
				if (this.GetStyle(ControlStyles.UserPaint))
				{
					this.WmPrintClient(ref m);
					return;
				}
				this.DefWndProc(ref m);
				return;
			}
			else if (msg != 8217)
			{
				if (msg == 8277)
				{
					m.Result = (IntPtr)((Marshal.SystemDefaultCharSize == 1) ? 1 : 2);
					return;
				}
				if (msg - 8498 > 6)
				{
					goto IL_65D;
				}
			}
			this.WmCtlColorControl(ref m);
			return;
			IL_426:
			if (!Control.ReflectMessageInternal(m.LParam, ref m))
			{
				this.DefWndProc(ref m);
				return;
			}
			return;
			IL_65D:
			if (m.Msg == Control.threadCallbackMessage && m.Msg != 0)
			{
				this.InvokeMarshaledCallbacks();
				return;
			}
			if (m.Msg == Control.WM_GETCONTROLNAME)
			{
				this.WmGetControlName(ref m);
				return;
			}
			if (m.Msg == Control.WM_GETCONTROLTYPE)
			{
				this.WmGetControlType(ref m);
				return;
			}
			if (Control.mouseWheelRoutingNeeded && m.Msg == Control.mouseWheelMessage)
			{
				Keys keys = Keys.None;
				keys |= ((UnsafeNativeMethods.GetKeyState(17) < 0) ? Keys.Back : Keys.None);
				keys |= ((UnsafeNativeMethods.GetKeyState(16) < 0) ? Keys.MButton : Keys.None);
				IntPtr focus = UnsafeNativeMethods.GetFocus();
				if (focus == IntPtr.Zero)
				{
					this.SendMessage(m.Msg, (IntPtr)(((int)(long)m.WParam << 16) | (int)keys), m.LParam);
				}
				else
				{
					IntPtr intPtr = IntPtr.Zero;
					IntPtr desktopWindow = UnsafeNativeMethods.GetDesktopWindow();
					while (intPtr == IntPtr.Zero && focus != IntPtr.Zero && focus != desktopWindow)
					{
						intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(null, focus), 522, ((int)(long)m.WParam << 16) | (int)keys, m.LParam);
						focus = UnsafeNativeMethods.GetParent(new HandleRef(null, focus));
					}
				}
			}
			if (m.Msg == NativeMethods.WM_MOUSEENTER)
			{
				this.WmMouseEnter(ref m);
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0003B8FD File Offset: 0x00039AFD
		private void WndProcException(Exception e)
		{
			Application.OnThreadException(e);
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x0003B908 File Offset: 0x00039B08
		ArrangedElementCollection IArrangedElement.Children
		{
			get
			{
				Control.ControlCollection controlCollection = (Control.ControlCollection)this.Properties.GetObject(Control.PropControlsCollection);
				if (controlCollection == null)
				{
					return ArrangedElementCollection.Empty;
				}
				return controlCollection;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0003B935 File Offset: 0x00039B35
		IArrangedElement IArrangedElement.Container
		{
			get
			{
				return this.ParentInternal;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x0003B93D File Offset: 0x00039B3D
		bool IArrangedElement.ParticipatesInLayout
		{
			get
			{
				return this.GetState(2);
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0003B946 File Offset: 0x00039B46
		void IArrangedElement.PerformLayout(IArrangedElement affectedElement, string affectedProperty)
		{
			this.PerformLayout(new LayoutEventArgs(affectedElement, affectedProperty));
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x0003B955 File Offset: 0x00039B55
		PropertyStore IArrangedElement.Properties
		{
			get
			{
				return this.Properties;
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0003B960 File Offset: 0x00039B60
		void IArrangedElement.SetBounds(Rectangle bounds, BoundsSpecified specified)
		{
			ISite site = this.Site;
			IComponentChangeService componentChangeService = null;
			PropertyDescriptor propertyDescriptor = null;
			PropertyDescriptor propertyDescriptor2 = null;
			bool flag = false;
			bool flag2 = false;
			if (site != null && site.DesignMode)
			{
				componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					propertyDescriptor = TypeDescriptor.GetProperties(this)[PropertyNames.Size];
					propertyDescriptor2 = TypeDescriptor.GetProperties(this)[PropertyNames.Location];
					try
					{
						if (propertyDescriptor != null && !propertyDescriptor.IsReadOnly && (bounds.Width != this.Width || bounds.Height != this.Height))
						{
							if (!(site is INestedSite))
							{
								componentChangeService.OnComponentChanging(this, propertyDescriptor);
							}
							flag = true;
						}
						if (propertyDescriptor2 != null && !propertyDescriptor2.IsReadOnly && (bounds.X != this.x || bounds.Y != this.y))
						{
							if (!(site is INestedSite))
							{
								componentChangeService.OnComponentChanging(this, propertyDescriptor2);
							}
							flag2 = true;
						}
					}
					catch (InvalidOperationException)
					{
					}
				}
			}
			this.SetBoundsCore(bounds.X, bounds.Y, bounds.Width, bounds.Height, specified);
			if (site != null && componentChangeService != null)
			{
				try
				{
					if (flag)
					{
						componentChangeService.OnComponentChanged(this, propertyDescriptor, null, null);
					}
					if (flag2)
					{
						componentChangeService.OnComponentChanged(this, propertyDescriptor2, null, null);
					}
				}
				catch (InvalidOperationException)
				{
				}
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool SupportsUiaProviders
		{
			get
			{
				return false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragEnter" /> event.</summary>
		/// <param name="drgEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x06001268 RID: 4712 RVA: 0x0003BAB4 File Offset: 0x00039CB4
		void IDropTarget.OnDragEnter(DragEventArgs drgEvent)
		{
			this.OnDragEnter(drgEvent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragOver" /> event.</summary>
		/// <param name="drgEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x06001269 RID: 4713 RVA: 0x0003BABD File Offset: 0x00039CBD
		void IDropTarget.OnDragOver(DragEventArgs drgEvent)
		{
			this.OnDragOver(drgEvent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragLeave" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600126A RID: 4714 RVA: 0x0003BAC6 File Offset: 0x00039CC6
		void IDropTarget.OnDragLeave(EventArgs e)
		{
			this.OnDragLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragDrop" /> event.</summary>
		/// <param name="drgEvent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data.</param>
		// Token: 0x0600126B RID: 4715 RVA: 0x0003BACF File Offset: 0x00039CCF
		void IDropTarget.OnDragDrop(DragEventArgs drgEvent)
		{
			this.OnDragDrop(drgEvent);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0003BAD8 File Offset: 0x00039CD8
		void ISupportOleDropSource.OnGiveFeedback(GiveFeedbackEventArgs giveFeedbackEventArgs)
		{
			this.OnGiveFeedback(giveFeedbackEventArgs);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0003BAE1 File Offset: 0x00039CE1
		void ISupportOleDropSource.OnQueryContinueDrag(QueryContinueDragEventArgs queryContinueDragEventArgs)
		{
			this.OnQueryContinueDrag(queryContinueDragEventArgs);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0003BAEC File Offset: 0x00039CEC
		int UnsafeNativeMethods.IOleControl.GetControlInfo(NativeMethods.tagCONTROLINFO pCI)
		{
			pCI.cb = Marshal.SizeOf(typeof(NativeMethods.tagCONTROLINFO));
			pCI.hAccel = IntPtr.Zero;
			pCI.cAccel = 0;
			pCI.dwFlags = 0;
			if (this.IsInputKey(Keys.Return))
			{
				pCI.dwFlags |= 1;
			}
			if (this.IsInputKey(Keys.Escape))
			{
				pCI.dwFlags |= 2;
			}
			this.ActiveXInstance.GetControlInfo(pCI);
			return 0;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0003BB68 File Offset: 0x00039D68
		int UnsafeNativeMethods.IOleControl.OnMnemonic(ref NativeMethods.MSG pMsg)
		{
			bool flag = this.ProcessMnemonic((char)(int)pMsg.wParam);
			return 0;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0003BB89 File Offset: 0x00039D89
		int UnsafeNativeMethods.IOleControl.OnAmbientPropertyChange(int dispID)
		{
			this.ActiveXInstance.OnAmbientPropertyChange(dispID);
			return 0;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0003BB98 File Offset: 0x00039D98
		int UnsafeNativeMethods.IOleControl.FreezeEvents(int bFreeze)
		{
			this.ActiveXInstance.EventsFrozen = bFreeze != 0;
			return 0;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0003BBAA File Offset: 0x00039DAA
		int UnsafeNativeMethods.IOleInPlaceActiveObject.GetWindow(out IntPtr hwnd)
		{
			return ((UnsafeNativeMethods.IOleInPlaceObject)this).GetWindow(out hwnd);
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0003BBB3 File Offset: 0x00039DB3
		void UnsafeNativeMethods.IOleInPlaceActiveObject.ContextSensitiveHelp(int fEnterMode)
		{
			((UnsafeNativeMethods.IOleInPlaceObject)this).ContextSensitiveHelp(fEnterMode);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0003BBBC File Offset: 0x00039DBC
		int UnsafeNativeMethods.IOleInPlaceActiveObject.TranslateAccelerator(ref NativeMethods.MSG lpmsg)
		{
			return this.ActiveXInstance.TranslateAccelerator(ref lpmsg);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0003BBCA File Offset: 0x00039DCA
		void UnsafeNativeMethods.IOleInPlaceActiveObject.OnFrameWindowActivate(bool fActivate)
		{
			this.OnFrameWindowActivate(fActivate);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0003BBD3 File Offset: 0x00039DD3
		void UnsafeNativeMethods.IOleInPlaceActiveObject.OnDocWindowActivate(int fActivate)
		{
			this.ActiveXInstance.OnDocWindowActivate(fActivate);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x000070A6 File Offset: 0x000052A6
		void UnsafeNativeMethods.IOleInPlaceActiveObject.ResizeBorder(NativeMethods.COMRECT prcBorder, UnsafeNativeMethods.IOleInPlaceUIWindow pUIWindow, bool fFrameWindow)
		{
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000070A6 File Offset: 0x000052A6
		void UnsafeNativeMethods.IOleInPlaceActiveObject.EnableModeless(int fEnable)
		{
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0003BBE4 File Offset: 0x00039DE4
		int UnsafeNativeMethods.IOleInPlaceObject.GetWindow(out IntPtr hwnd)
		{
			return this.ActiveXInstance.GetWindow(out hwnd);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0003BBFF File Offset: 0x00039DFF
		void UnsafeNativeMethods.IOleInPlaceObject.ContextSensitiveHelp(int fEnterMode)
		{
			if (fEnterMode != 0)
			{
				this.OnHelpRequested(new HelpEventArgs(Control.MousePosition));
			}
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0003BC14 File Offset: 0x00039E14
		void UnsafeNativeMethods.IOleInPlaceObject.InPlaceDeactivate()
		{
			this.ActiveXInstance.InPlaceDeactivate();
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0003BC21 File Offset: 0x00039E21
		int UnsafeNativeMethods.IOleInPlaceObject.UIDeactivate()
		{
			return this.ActiveXInstance.UIDeactivate();
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0003BC2E File Offset: 0x00039E2E
		void UnsafeNativeMethods.IOleInPlaceObject.SetObjectRects(NativeMethods.COMRECT lprcPosRect, NativeMethods.COMRECT lprcClipRect)
		{
			this.ActiveXInstance.SetObjectRects(lprcPosRect, lprcClipRect);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x000070A6 File Offset: 0x000052A6
		void UnsafeNativeMethods.IOleInPlaceObject.ReactivateAndUndo()
		{
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0003BC3D File Offset: 0x00039E3D
		int UnsafeNativeMethods.IOleObject.SetClientSite(UnsafeNativeMethods.IOleClientSite pClientSite)
		{
			this.ActiveXInstance.SetClientSite(pClientSite);
			return 0;
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0003BC4C File Offset: 0x00039E4C
		UnsafeNativeMethods.IOleClientSite UnsafeNativeMethods.IOleObject.GetClientSite()
		{
			return this.ActiveXInstance.GetClientSite();
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0001180C File Offset: 0x0000FA0C
		int UnsafeNativeMethods.IOleObject.SetHostNames(string szContainerApp, string szContainerObj)
		{
			return 0;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0003BC59 File Offset: 0x00039E59
		int UnsafeNativeMethods.IOleObject.Close(int dwSaveOption)
		{
			this.ActiveXInstance.Close(dwSaveOption);
			return 0;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleObject.SetMoniker(int dwWhichMoniker, object pmk)
		{
			return -2147467263;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0003BC6F File Offset: 0x00039E6F
		int UnsafeNativeMethods.IOleObject.GetMoniker(int dwAssign, int dwWhichMoniker, out object moniker)
		{
			moniker = null;
			return -2147467263;
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IOleObject.InitFromData(IDataObject pDataObject, int fCreation, int dwReserved)
		{
			return -2147467263;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x000160D7 File Offset: 0x000142D7
		int UnsafeNativeMethods.IOleObject.GetClipboardData(int dwReserved, out IDataObject data)
		{
			data = null;
			return -2147467263;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0003BC7C File Offset: 0x00039E7C
		int UnsafeNativeMethods.IOleObject.DoVerb(int iVerb, IntPtr lpmsg, UnsafeNativeMethods.IOleClientSite pActiveSite, int lindex, IntPtr hwndParent, NativeMethods.COMRECT lprcPosRect)
		{
			short num = (short)iVerb;
			iVerb = (int)num;
			try
			{
				this.ActiveXInstance.DoVerb(iVerb, lpmsg, pActiveSite, lindex, hwndParent, lprcPosRect);
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
			}
			return 0;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0003BCC8 File Offset: 0x00039EC8
		int UnsafeNativeMethods.IOleObject.EnumVerbs(out UnsafeNativeMethods.IEnumOLEVERB e)
		{
			return Control.ActiveXImpl.EnumVerbs(out e);
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0001180C File Offset: 0x0000FA0C
		int UnsafeNativeMethods.IOleObject.OleUpdate()
		{
			return 0;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0001180C File Offset: 0x0000FA0C
		int UnsafeNativeMethods.IOleObject.IsUpToDate()
		{
			return 0;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0003BCD0 File Offset: 0x00039ED0
		int UnsafeNativeMethods.IOleObject.GetUserClassID(ref Guid pClsid)
		{
			pClsid = base.GetType().GUID;
			return 0;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0003BCE4 File Offset: 0x00039EE4
		int UnsafeNativeMethods.IOleObject.GetUserType(int dwFormOfType, out string userType)
		{
			if (dwFormOfType == 1)
			{
				userType = base.GetType().FullName;
			}
			else
			{
				userType = base.GetType().Name;
			}
			return 0;
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0003BD07 File Offset: 0x00039F07
		int UnsafeNativeMethods.IOleObject.SetExtent(int dwDrawAspect, NativeMethods.tagSIZEL pSizel)
		{
			this.ActiveXInstance.SetExtent(dwDrawAspect, pSizel);
			return 0;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0003BD17 File Offset: 0x00039F17
		int UnsafeNativeMethods.IOleObject.GetExtent(int dwDrawAspect, NativeMethods.tagSIZEL pSizel)
		{
			this.ActiveXInstance.GetExtent(dwDrawAspect, pSizel);
			return 0;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0003BD27 File Offset: 0x00039F27
		int UnsafeNativeMethods.IOleObject.Advise(IAdviseSink pAdvSink, out int cookie)
		{
			cookie = this.ActiveXInstance.Advise(pAdvSink);
			return 0;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0003BD38 File Offset: 0x00039F38
		int UnsafeNativeMethods.IOleObject.Unadvise(int dwConnection)
		{
			this.ActiveXInstance.Unadvise(dwConnection);
			return 0;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0003BD47 File Offset: 0x00039F47
		int UnsafeNativeMethods.IOleObject.EnumAdvise(out IEnumSTATDATA e)
		{
			e = null;
			return -2147467263;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0003BD54 File Offset: 0x00039F54
		int UnsafeNativeMethods.IOleObject.GetMiscStatus(int dwAspect, out int cookie)
		{
			if ((dwAspect & 1) != 0)
			{
				int num = 131456;
				if (this.GetStyle(ControlStyles.ResizeRedraw))
				{
					num |= 1;
				}
				if (this is IButtonControl)
				{
					num |= 4096;
				}
				cookie = num;
				return 0;
			}
			cookie = 0;
			return -2147221397;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0001180C File Offset: 0x0000FA0C
		int UnsafeNativeMethods.IOleObject.SetColorScheme(NativeMethods.tagLOGPALETTE pLogpal)
		{
			return 0;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0003BBAA File Offset: 0x00039DAA
		int UnsafeNativeMethods.IOleWindow.GetWindow(out IntPtr hwnd)
		{
			return ((UnsafeNativeMethods.IOleInPlaceObject)this).GetWindow(out hwnd);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0003BBB3 File Offset: 0x00039DB3
		void UnsafeNativeMethods.IOleWindow.ContextSensitiveHelp(int fEnterMode)
		{
			((UnsafeNativeMethods.IOleInPlaceObject)this).ContextSensitiveHelp(fEnterMode);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0003BD99 File Offset: 0x00039F99
		void UnsafeNativeMethods.IPersist.GetClassID(out Guid pClassID)
		{
			pClassID = base.GetType().GUID;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000070A6 File Offset: 0x000052A6
		void UnsafeNativeMethods.IPersistPropertyBag.InitNew()
		{
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0003BD99 File Offset: 0x00039F99
		void UnsafeNativeMethods.IPersistPropertyBag.GetClassID(out Guid pClassID)
		{
			pClassID = base.GetType().GUID;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0003BDAC File Offset: 0x00039FAC
		void UnsafeNativeMethods.IPersistPropertyBag.Load(UnsafeNativeMethods.IPropertyBag pPropBag, UnsafeNativeMethods.IErrorLog pErrorLog)
		{
			this.ActiveXInstance.Load(pPropBag, pErrorLog);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0003BDBB File Offset: 0x00039FBB
		void UnsafeNativeMethods.IPersistPropertyBag.Save(UnsafeNativeMethods.IPropertyBag pPropBag, bool fClearDirty, bool fSaveAllProperties)
		{
			this.ActiveXInstance.Save(pPropBag, fClearDirty, fSaveAllProperties);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0003BD99 File Offset: 0x00039F99
		void UnsafeNativeMethods.IPersistStorage.GetClassID(out Guid pClassID)
		{
			pClassID = base.GetType().GUID;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0003BDCB File Offset: 0x00039FCB
		int UnsafeNativeMethods.IPersistStorage.IsDirty()
		{
			return this.ActiveXInstance.IsDirty();
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x000070A6 File Offset: 0x000052A6
		void UnsafeNativeMethods.IPersistStorage.InitNew(UnsafeNativeMethods.IStorage pstg)
		{
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0003BDD8 File Offset: 0x00039FD8
		int UnsafeNativeMethods.IPersistStorage.Load(UnsafeNativeMethods.IStorage pstg)
		{
			this.ActiveXInstance.Load(pstg);
			return 0;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0003BDE7 File Offset: 0x00039FE7
		void UnsafeNativeMethods.IPersistStorage.Save(UnsafeNativeMethods.IStorage pstg, bool fSameAsLoad)
		{
			this.ActiveXInstance.Save(pstg, fSameAsLoad);
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x000070A6 File Offset: 0x000052A6
		void UnsafeNativeMethods.IPersistStorage.SaveCompleted(UnsafeNativeMethods.IStorage pStgNew)
		{
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000070A6 File Offset: 0x000052A6
		void UnsafeNativeMethods.IPersistStorage.HandsOffStorage()
		{
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0003BD99 File Offset: 0x00039F99
		void UnsafeNativeMethods.IPersistStreamInit.GetClassID(out Guid pClassID)
		{
			pClassID = base.GetType().GUID;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0003BDCB File Offset: 0x00039FCB
		int UnsafeNativeMethods.IPersistStreamInit.IsDirty()
		{
			return this.ActiveXInstance.IsDirty();
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0003BDF6 File Offset: 0x00039FF6
		void UnsafeNativeMethods.IPersistStreamInit.Load(UnsafeNativeMethods.IStream pstm)
		{
			this.ActiveXInstance.Load(pstm);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0003BE04 File Offset: 0x0003A004
		void UnsafeNativeMethods.IPersistStreamInit.Save(UnsafeNativeMethods.IStream pstm, bool fClearDirty)
		{
			this.ActiveXInstance.Save(pstm, fClearDirty);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x000070A6 File Offset: 0x000052A6
		void UnsafeNativeMethods.IPersistStreamInit.GetSizeMax(long pcbSize)
		{
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x000070A6 File Offset: 0x000052A6
		void UnsafeNativeMethods.IPersistStreamInit.InitNew()
		{
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0003BE13 File Offset: 0x0003A013
		void UnsafeNativeMethods.IQuickActivate.QuickActivate(UnsafeNativeMethods.tagQACONTAINER pQaContainer, UnsafeNativeMethods.tagQACONTROL pQaControl)
		{
			this.ActiveXInstance.QuickActivate(pQaContainer, pQaControl);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0003BE22 File Offset: 0x0003A022
		void UnsafeNativeMethods.IQuickActivate.SetContentExtent(NativeMethods.tagSIZEL pSizel)
		{
			this.ActiveXInstance.SetExtent(1, pSizel);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0003BE31 File Offset: 0x0003A031
		void UnsafeNativeMethods.IQuickActivate.GetContentExtent(NativeMethods.tagSIZEL pSizel)
		{
			this.ActiveXInstance.GetExtent(1, pSizel);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0003BE40 File Offset: 0x0003A040
		int UnsafeNativeMethods.IViewObject.Draw(int dwDrawAspect, int lindex, IntPtr pvAspect, NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, NativeMethods.COMRECT lprcBounds, NativeMethods.COMRECT lprcWBounds, IntPtr pfnContinue, int dwContinue)
		{
			try
			{
				this.ActiveXInstance.Draw(dwDrawAspect, lindex, pvAspect, ptd, hdcTargetDev, hdcDraw, lprcBounds, lprcWBounds, pfnContinue, dwContinue);
			}
			catch (ExternalException ex)
			{
				return ex.ErrorCode;
			}
			finally
			{
			}
			return 0;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IViewObject.GetColorSet(int dwDrawAspect, int lindex, IntPtr pvAspect, NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hicTargetDev, NativeMethods.tagLOGPALETTE ppColorSet)
		{
			return -2147467263;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IViewObject.Freeze(int dwDrawAspect, int lindex, IntPtr pvAspect, IntPtr pdwFreeze)
		{
			return -2147467263;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IViewObject.Unfreeze(int dwFreeze)
		{
			return -2147467263;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0003BE98 File Offset: 0x0003A098
		void UnsafeNativeMethods.IViewObject.SetAdvise(int aspects, int advf, IAdviseSink pAdvSink)
		{
			this.ActiveXInstance.SetAdvise(aspects, advf, pAdvSink);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0003BEA8 File Offset: 0x0003A0A8
		void UnsafeNativeMethods.IViewObject.GetAdvise(int[] paspects, int[] padvf, IAdviseSink[] pAdvSink)
		{
			this.ActiveXInstance.GetAdvise(paspects, padvf, pAdvSink);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0003BEB8 File Offset: 0x0003A0B8
		void UnsafeNativeMethods.IViewObject2.Draw(int dwDrawAspect, int lindex, IntPtr pvAspect, NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, NativeMethods.COMRECT lprcBounds, NativeMethods.COMRECT lprcWBounds, IntPtr pfnContinue, int dwContinue)
		{
			this.ActiveXInstance.Draw(dwDrawAspect, lindex, pvAspect, ptd, hdcTargetDev, hdcDraw, lprcBounds, lprcWBounds, pfnContinue, dwContinue);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IViewObject2.GetColorSet(int dwDrawAspect, int lindex, IntPtr pvAspect, NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hicTargetDev, NativeMethods.tagLOGPALETTE ppColorSet)
		{
			return -2147467263;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IViewObject2.Freeze(int dwDrawAspect, int lindex, IntPtr pvAspect, IntPtr pdwFreeze)
		{
			return -2147467263;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0003BC68 File Offset: 0x00039E68
		int UnsafeNativeMethods.IViewObject2.Unfreeze(int dwFreeze)
		{
			return -2147467263;
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0003BE98 File Offset: 0x0003A098
		void UnsafeNativeMethods.IViewObject2.SetAdvise(int aspects, int advf, IAdviseSink pAdvSink)
		{
			this.ActiveXInstance.SetAdvise(aspects, advf, pAdvSink);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0003BEA8 File Offset: 0x0003A0A8
		void UnsafeNativeMethods.IViewObject2.GetAdvise(int[] paspects, int[] padvf, IAdviseSink[] pAdvSink)
		{
			this.ActiveXInstance.GetAdvise(paspects, padvf, pAdvSink);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0003BEE1 File Offset: 0x0003A0E1
		void UnsafeNativeMethods.IViewObject2.GetExtent(int dwDrawAspect, int lindex, NativeMethods.tagDVTARGETDEVICE ptd, NativeMethods.tagSIZEL lpsizel)
		{
			((UnsafeNativeMethods.IOleObject)this).GetExtent(dwDrawAspect, lpsizel);
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0003BEF0 File Offset: 0x0003A0F0
		bool IKeyboardToolTip.CanShowToolTipsNow()
		{
			IKeyboardToolTip toolStripControlHost = this.ToolStripControlHost;
			return this.IsHandleCreated && this.Visible && (toolStripControlHost == null || toolStripControlHost.CanShowToolTipsNow());
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0003BF21 File Offset: 0x0003A121
		Rectangle IKeyboardToolTip.GetNativeScreenRectangle()
		{
			return this.GetToolNativeScreenRectangle();
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0003BF2C File Offset: 0x0003A12C
		IList<Rectangle> IKeyboardToolTip.GetNeighboringToolsRectangles()
		{
			IKeyboardToolTip toolStripControlHost = this.ToolStripControlHost;
			if (toolStripControlHost == null)
			{
				return this.GetOwnNeighboringToolsRectangles();
			}
			return toolStripControlHost.GetNeighboringToolsRectangles();
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0003BF50 File Offset: 0x0003A150
		bool IKeyboardToolTip.IsHoveredWithMouse()
		{
			return this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition));
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0003BF78 File Offset: 0x0003A178
		bool IKeyboardToolTip.HasRtlModeEnabled()
		{
			Control topLevelControlInternal = this.TopLevelControlInternal;
			return topLevelControlInternal != null && topLevelControlInternal.RightToLeft == RightToLeft.Yes && !this.IsMirrored;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0003BFA4 File Offset: 0x0003A1A4
		bool IKeyboardToolTip.AllowsToolTip()
		{
			IKeyboardToolTip toolStripControlHost = this.ToolStripControlHost;
			return (toolStripControlHost == null || toolStripControlHost.AllowsToolTip()) && this.AllowsKeyboardToolTip();
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00006A49 File Offset: 0x00004C49
		IWin32Window IKeyboardToolTip.GetOwnerWindow()
		{
			return this;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0003BFCB File Offset: 0x0003A1CB
		void IKeyboardToolTip.OnHooked(ToolTip toolTip)
		{
			this.OnKeyboardToolTipHook(toolTip);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0003BFD4 File Offset: 0x0003A1D4
		void IKeyboardToolTip.OnUnhooked(ToolTip toolTip)
		{
			this.OnKeyboardToolTipUnhook(toolTip);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0003BFE0 File Offset: 0x0003A1E0
		string IKeyboardToolTip.GetCaptionForTool(ToolTip toolTip)
		{
			IKeyboardToolTip toolStripControlHost = this.ToolStripControlHost;
			if (toolStripControlHost == null)
			{
				return toolTip.GetCaptionForTool(this);
			}
			return toolStripControlHost.GetCaptionForTool(toolTip);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0003C008 File Offset: 0x0003A208
		bool IKeyboardToolTip.ShowsOwnToolTip()
		{
			IKeyboardToolTip toolStripControlHost = this.ToolStripControlHost;
			return (toolStripControlHost == null || toolStripControlHost.ShowsOwnToolTip()) && this.ShowsOwnKeyboardToolTip();
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0003C02F File Offset: 0x0003A22F
		bool IKeyboardToolTip.IsBeingTabbedTo()
		{
			return Control.AreCommonNavigationalKeysDown();
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0003C036 File Offset: 0x0003A236
		bool IKeyboardToolTip.AllowsChildrenToShowToolTips()
		{
			return this.AllowsChildrenToShowToolTips();
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0003C040 File Offset: 0x0003A240
		private IList<Rectangle> GetOwnNeighboringToolsRectangles()
		{
			Control parentInternal = this.ParentInternal;
			if (parentInternal != null)
			{
				Control[] array = new Control[]
				{
					parentInternal.GetNextSelectableControl(this, true, true, true, false),
					parentInternal.GetNextSelectableControl(this, false, true, true, false),
					parentInternal.GetNextSelectableControl(this, true, false, false, true),
					parentInternal.GetNextSelectableControl(this, false, false, false, true)
				};
				List<Rectangle> list = new List<Rectangle>(4);
				foreach (Control control in array)
				{
					if (control != null && control.IsHandleCreated)
					{
						list.Add(((IKeyboardToolTip)control).GetNativeScreenRectangle());
					}
				}
				return list;
			}
			return new Rectangle[0];
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00012E4E File Offset: 0x0001104E
		internal virtual bool ShowsOwnKeyboardToolTip()
		{
			return true;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnKeyboardToolTipHook(ToolTip toolTip)
		{
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnKeyboardToolTipUnhook(ToolTip toolTip)
		{
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0003C0DC File Offset: 0x0003A2DC
		internal virtual Rectangle GetToolNativeScreenRectangle()
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetWindowRect(new HandleRef(this, this.Handle), ref rect);
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00012E4E File Offset: 0x0001104E
		internal virtual bool AllowsKeyboardToolTip()
		{
			return true;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0003C122 File Offset: 0x0003A322
		private static bool IsKeyDown(Keys key)
		{
			return (Control.tempKeyboardStateArray[(int)key] & 128) > 0;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0003C134 File Offset: 0x0003A334
		internal static bool AreCommonNavigationalKeysDown()
		{
			if (Control.tempKeyboardStateArray == null)
			{
				Control.tempKeyboardStateArray = new byte[256];
			}
			UnsafeNativeMethods.GetKeyboardState(Control.tempKeyboardStateArray);
			return Control.IsKeyDown(Keys.Tab) || Control.IsKeyDown(Keys.Up) || Control.IsKeyDown(Keys.Down) || Control.IsKeyDown(Keys.Left) || Control.IsKeyDown(Keys.Right) || Control.IsKeyDown(Keys.Menu) || Control.IsKeyDown(Keys.F10) || Control.IsKeyDown(Keys.Escape);
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0003C1AC File Offset: 0x0003A3AC
		// (set) Token: 0x060012CE RID: 4814 RVA: 0x0003C1C8 File Offset: 0x0003A3C8
		internal ToolStripControlHost ToolStripControlHost
		{
			get
			{
				ToolStripControlHost toolStripControlHost;
				this.toolStripControlHostReference.TryGetTarget(out toolStripControlHost);
				return toolStripControlHost;
			}
			set
			{
				this.toolStripControlHostReference.SetTarget(value);
			}
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00012E4E File Offset: 0x0001104E
		internal virtual bool AllowsChildrenToShowToolTips()
		{
			return true;
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0003C1D8 File Offset: 0x0003A3D8
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x0003C21C File Offset: 0x0003A41C
		internal ImeMode CachedImeMode
		{
			get
			{
				bool flag;
				ImeMode imeMode = (ImeMode)this.Properties.GetInteger(Control.PropImeMode, out flag);
				if (!flag)
				{
					imeMode = this.DefaultImeMode;
				}
				if (imeMode == ImeMode.Inherit)
				{
					Control parentInternal = this.ParentInternal;
					if (parentInternal != null)
					{
						imeMode = parentInternal.CachedImeMode;
					}
					else
					{
						imeMode = ImeMode.NoControl;
					}
				}
				return imeMode;
			}
			set
			{
				this.Properties.SetInteger(Control.PropImeMode, (int)value);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property can be set to an active value, to enable IME support.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0003C22F File Offset: 0x0003A42F
		protected virtual bool CanEnableIme
		{
			get
			{
				return this.ImeSupported;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0003C237 File Offset: 0x0003A437
		internal ImeMode CurrentImeContextMode
		{
			get
			{
				if (this.IsHandleCreated)
				{
					return ImeContext.GetImeMode(this.Handle);
				}
				return ImeMode.Inherit;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00015C93 File Offset: 0x00013E93
		protected virtual ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Inherit;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0003C250 File Offset: 0x0003A450
		// (set) Token: 0x060012D6 RID: 4822 RVA: 0x0003C271 File Offset: 0x0003A471
		internal int DisableImeModeChangedCount
		{
			get
			{
				bool flag;
				return this.Properties.GetInteger(Control.PropDisableImeModeChangedCount, out flag);
			}
			set
			{
				this.Properties.SetInteger(Control.PropDisableImeModeChangedCount, value);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0003C284 File Offset: 0x0003A484
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x0003C298 File Offset: 0x0003A498
		private static bool IgnoreWmImeNotify
		{
			get
			{
				return Control.ignoreWmImeNotify;
			}
			set
			{
				Control.ignoreWmImeNotify = value;
			}
		}

		/// <summary>Gets or sets the Input Method Editor (IME) mode of the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values. The default is <see cref="F:System.Windows.Forms.ImeMode.Inherit" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ImeMode" /> enumeration values.</exception>
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0003C2A0 File Offset: 0x0003A4A0
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x0003C2BC File Offset: 0x0003A4BC
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[AmbientValue(ImeMode.Inherit)]
		[SRDescription("ControlIMEModeDescr")]
		public ImeMode ImeMode
		{
			get
			{
				ImeMode imeMode = this.ImeModeBase;
				if (imeMode == ImeMode.OnHalf)
				{
					imeMode = ImeMode.On;
				}
				return imeMode;
			}
			set
			{
				this.ImeModeBase = value;
			}
		}

		/// <summary>Gets or sets the IME mode of a control.</summary>
		/// <returns>The IME mode of the control.</returns>
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0003C2C8 File Offset: 0x0003A4C8
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x0003C2E0 File Offset: 0x0003A4E0
		protected virtual ImeMode ImeModeBase
		{
			get
			{
				return this.CachedImeMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, -1, 12))
				{
					throw new InvalidEnumArgumentException("ImeMode", (int)value, typeof(ImeMode));
				}
				ImeMode cachedImeMode = this.CachedImeMode;
				this.CachedImeMode = value;
				if (cachedImeMode != value)
				{
					Control control = null;
					if (!base.DesignMode && ImeModeConversion.InputLanguageTable != ImeModeConversion.UnsupportedTable)
					{
						if (this.Focused)
						{
							control = this;
						}
						else if (this.ContainsFocus)
						{
							control = Control.FromChildHandleInternal(UnsafeNativeMethods.GetFocus());
						}
						if (control != null && control.CanEnableIme)
						{
							int num = this.DisableImeModeChangedCount;
							this.DisableImeModeChangedCount = num + 1;
							try
							{
								control.UpdateImeContextMode();
							}
							finally
							{
								num = this.DisableImeModeChangedCount;
								this.DisableImeModeChangedCount = num - 1;
							}
						}
					}
					this.VerifyImeModeChanged(cachedImeMode, this.CachedImeMode);
				}
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x0003C3AC File Offset: 0x0003A5AC
		private bool ImeSupported
		{
			get
			{
				return this.DefaultImeMode != ImeMode.Disable;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property has changed.</summary>
		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x060012DE RID: 4830 RVA: 0x0003C3BA File Offset: 0x0003A5BA
		// (remove) Token: 0x060012DF RID: 4831 RVA: 0x0003C3CD File Offset: 0x0003A5CD
		[WinCategory("Behavior")]
		[SRDescription("ControlOnImeModeChangedDescr")]
		public event EventHandler ImeModeChanged
		{
			add
			{
				base.Events.AddHandler(Control.EventImeModeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EventImeModeChanged, value);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x0003C3E0 File Offset: 0x0003A5E0
		// (set) Token: 0x060012E1 RID: 4833 RVA: 0x0003C3F2 File Offset: 0x0003A5F2
		internal int ImeWmCharsToIgnore
		{
			get
			{
				return this.Properties.GetInteger(Control.PropImeWmCharsToIgnore);
			}
			set
			{
				if (this.ImeWmCharsToIgnore != -1)
				{
					this.Properties.SetInteger(Control.PropImeWmCharsToIgnore, value);
				}
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x0003C410 File Offset: 0x0003A610
		// (set) Token: 0x060012E3 RID: 4835 RVA: 0x0003C43D File Offset: 0x0003A63D
		private bool LastCanEnableIme
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(Control.PropLastCanEnableIme, out flag);
				flag = !flag || integer == 1;
				return flag;
			}
			set
			{
				this.Properties.SetInteger(Control.PropLastCanEnableIme, value ? 1 : 0);
			}
		}

		/// <summary>Gets an object that represents a propagating IME mode.</summary>
		/// <returns>An object that represents a propagating IME mode.</returns>
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x0003C458 File Offset: 0x0003A658
		// (set) Token: 0x060012E5 RID: 4837 RVA: 0x0003C4BA File Offset: 0x0003A6BA
		private protected static ImeMode PropagatingImeMode
		{
			protected get
			{
				if (Control.propagatingImeMode == ImeMode.Inherit)
				{
					ImeMode imeMode = ImeMode.Inherit;
					IntPtr intPtr = UnsafeNativeMethods.GetFocus();
					if (intPtr != IntPtr.Zero)
					{
						imeMode = ImeContext.GetImeMode(intPtr);
						if (imeMode == ImeMode.Disable)
						{
							intPtr = UnsafeNativeMethods.GetAncestor(new HandleRef(null, intPtr), 2);
							if (intPtr != IntPtr.Zero)
							{
								imeMode = ImeContext.GetImeMode(intPtr);
							}
						}
					}
					Control.PropagatingImeMode = imeMode;
				}
				return Control.propagatingImeMode;
			}
			private set
			{
				if (Control.propagatingImeMode != value)
				{
					if (value == ImeMode.NoControl || value == ImeMode.Disable)
					{
						return;
					}
					Control.propagatingImeMode = value;
				}
			}
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0003C4D4 File Offset: 0x0003A6D4
		internal void UpdateImeContextMode()
		{
			ImeMode[] inputLanguageTable = ImeModeConversion.InputLanguageTable;
			if (!base.DesignMode && inputLanguageTable != ImeModeConversion.UnsupportedTable && this.Focused)
			{
				ImeMode imeMode = ImeMode.Disable;
				ImeMode cachedImeMode = this.CachedImeMode;
				if (this.ImeSupported && this.CanEnableIme)
				{
					imeMode = ((cachedImeMode == ImeMode.NoControl) ? Control.PropagatingImeMode : cachedImeMode);
				}
				if (this.CurrentImeContextMode != imeMode && imeMode != ImeMode.Inherit)
				{
					int num = this.DisableImeModeChangedCount;
					this.DisableImeModeChangedCount = num + 1;
					ImeMode imeMode2 = Control.PropagatingImeMode;
					try
					{
						ImeContext.SetImeStatus(imeMode, this.Handle);
					}
					finally
					{
						num = this.DisableImeModeChangedCount;
						this.DisableImeModeChangedCount = num - 1;
						if (imeMode == ImeMode.Disable && inputLanguageTable == ImeModeConversion.ChineseTable)
						{
							Control.PropagatingImeMode = imeMode2;
						}
					}
					if (cachedImeMode == ImeMode.NoControl)
					{
						if (this.CanEnableIme)
						{
							Control.PropagatingImeMode = this.CurrentImeContextMode;
							return;
						}
					}
					else
					{
						if (this.CanEnableIme)
						{
							this.CachedImeMode = this.CurrentImeContextMode;
						}
						this.VerifyImeModeChanged(imeMode, this.CachedImeMode);
					}
				}
			}
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0003C5D4 File Offset: 0x0003A7D4
		private void VerifyImeModeChanged(ImeMode oldMode, ImeMode newMode)
		{
			if (this.ImeSupported && this.DisableImeModeChangedCount == 0 && newMode != ImeMode.NoControl && oldMode != newMode)
			{
				this.OnImeModeChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0003C5F8 File Offset: 0x0003A7F8
		internal void VerifyImeRestrictedModeChanged()
		{
			bool canEnableIme = this.CanEnableIme;
			if (this.LastCanEnableIme != canEnableIme)
			{
				if (this.Focused)
				{
					int num = this.DisableImeModeChangedCount;
					this.DisableImeModeChangedCount = num + 1;
					try
					{
						this.UpdateImeContextMode();
					}
					finally
					{
						num = this.DisableImeModeChangedCount;
						this.DisableImeModeChangedCount = num - 1;
					}
				}
				ImeMode imeMode = this.CachedImeMode;
				ImeMode imeMode2 = ImeMode.Disable;
				if (canEnableIme)
				{
					imeMode2 = imeMode;
					imeMode = ImeMode.Disable;
				}
				this.VerifyImeModeChanged(imeMode, imeMode2);
				this.LastCanEnableIme = canEnableIme;
			}
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0003C678 File Offset: 0x0003A878
		internal void OnImeContextStatusChanged(IntPtr handle)
		{
			ImeMode imeMode = ImeContext.GetImeMode(handle);
			if (imeMode != ImeMode.Inherit)
			{
				ImeMode cachedImeMode = this.CachedImeMode;
				if (this.CanEnableIme)
				{
					if (cachedImeMode != ImeMode.NoControl)
					{
						this.CachedImeMode = imeMode;
						this.VerifyImeModeChanged(cachedImeMode, this.CachedImeMode);
						return;
					}
					Control.PropagatingImeMode = imeMode;
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ImeModeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060012EA RID: 4842 RVA: 0x0003C6C0 File Offset: 0x0003A8C0
		protected virtual void OnImeModeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventImeModeChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property to its default value.</summary>
		// Token: 0x060012EB RID: 4843 RVA: 0x0003C6EE File Offset: 0x0003A8EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetImeMode()
		{
			this.ImeMode = this.DefaultImeMode;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0003C6FC File Offset: 0x0003A8FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeImeMode()
		{
			bool flag;
			int integer = this.Properties.GetInteger(Control.PropImeMode, out flag);
			return flag && integer != (int)this.DefaultImeMode;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0003C730 File Offset: 0x0003A930
		private void WmInputLangChange(ref Message m)
		{
			this.UpdateImeContextMode();
			if (ImeModeConversion.InputLanguageTable == ImeModeConversion.UnsupportedTable)
			{
				Control.PropagatingImeMode = ImeMode.Off;
			}
			if (LocalAppContextSwitches.EnableLegacyChineseIMEIndicator && ImeModeConversion.InputLanguageTable == ImeModeConversion.ChineseTable)
			{
				Control.IgnoreWmImeNotify = false;
			}
			Form form = this.FindFormInternal();
			if (form != null)
			{
				InputLanguageChangedEventArgs inputLanguageChangedEventArgs = InputLanguage.CreateInputLanguageChangedEventArgs(m);
				form.PerformOnInputLanguageChanged(inputLanguageChangedEventArgs);
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0003C794 File Offset: 0x0003A994
		private void WmInputLangChangeRequest(ref Message m)
		{
			InputLanguageChangingEventArgs inputLanguageChangingEventArgs = InputLanguage.CreateInputLanguageChangingEventArgs(m);
			Form form = this.FindFormInternal();
			if (form != null)
			{
				form.PerformOnInputLanguageChanging(inputLanguageChangingEventArgs);
			}
			if (!inputLanguageChangingEventArgs.Cancel)
			{
				this.DefWndProc(ref m);
				return;
			}
			m.Result = IntPtr.Zero;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0003C7D9 File Offset: 0x0003A9D9
		private void WmImeChar(ref Message m)
		{
			if (this.ProcessKeyEventArgs(ref m))
			{
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0003C7EC File Offset: 0x0003A9EC
		private void WmImeEndComposition(ref Message m)
		{
			this.ImeWmCharsToIgnore = -1;
			this.DefWndProc(ref m);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0003C7FC File Offset: 0x0003A9FC
		private void WmImeNotify(ref Message m)
		{
			ImeMode[] inputLanguageTable = ImeModeConversion.InputLanguageTable;
			if (LocalAppContextSwitches.EnableLegacyChineseIMEIndicator && inputLanguageTable == ImeModeConversion.ChineseTable && !Control.lastLanguageChinese)
			{
				Control.IgnoreWmImeNotify = true;
			}
			Control.lastLanguageChinese = inputLanguageTable == ImeModeConversion.ChineseTable;
			if (this.ImeSupported && inputLanguageTable != ImeModeConversion.UnsupportedTable && !Control.IgnoreWmImeNotify)
			{
				int num = (int)m.WParam;
				if (num == 6 || num == 8)
				{
					this.OnImeContextStatusChanged(this.Handle);
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0003C876 File Offset: 0x0003AA76
		internal void WmImeSetFocus()
		{
			if (ImeModeConversion.InputLanguageTable != ImeModeConversion.UnsupportedTable)
			{
				this.UpdateImeContextMode();
			}
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0003C88A File Offset: 0x0003AA8A
		private void WmImeStartComposition(ref Message m)
		{
			this.Properties.SetInteger(Control.PropImeWmCharsToIgnore, 0);
			this.DefWndProc(ref m);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0003C8A4 File Offset: 0x0003AAA4
		private void WmImeKillFocus()
		{
			Control topMostParent = this.TopMostParent;
			Form form = topMostParent as Form;
			if ((form == null || form.Modal) && !topMostParent.ContainsFocus && Control.propagatingImeMode != ImeMode.Inherit)
			{
				Control.IgnoreWmImeNotify = true;
				try
				{
					ImeContext.SetImeStatus(Control.PropagatingImeMode, topMostParent.Handle);
					Control.PropagatingImeMode = ImeMode.Inherit;
				}
				finally
				{
					Control.IgnoreWmImeNotify = false;
				}
			}
		}

		// Token: 0x04000814 RID: 2068
		internal static readonly TraceSwitch ControlKeyboardRouting;

		// Token: 0x04000815 RID: 2069
		internal static readonly TraceSwitch PaletteTracing;

		// Token: 0x04000816 RID: 2070
		internal static readonly TraceSwitch FocusTracing;

		// Token: 0x04000817 RID: 2071
		internal static readonly BooleanSwitch BufferPinkRect;

		// Token: 0x04000818 RID: 2072
		private static int WM_GETCONTROLNAME = SafeNativeMethods.RegisterWindowMessage("WM_GETCONTROLNAME");

		// Token: 0x04000819 RID: 2073
		private static int WM_GETCONTROLTYPE = SafeNativeMethods.RegisterWindowMessage("WM_GETCONTROLTYPE");

		// Token: 0x0400081A RID: 2074
		internal const int STATE_CREATED = 1;

		// Token: 0x0400081B RID: 2075
		internal const int STATE_VISIBLE = 2;

		// Token: 0x0400081C RID: 2076
		internal const int STATE_ENABLED = 4;

		// Token: 0x0400081D RID: 2077
		internal const int STATE_TABSTOP = 8;

		// Token: 0x0400081E RID: 2078
		internal const int STATE_RECREATE = 16;

		// Token: 0x0400081F RID: 2079
		internal const int STATE_MODAL = 32;

		// Token: 0x04000820 RID: 2080
		internal const int STATE_ALLOWDROP = 64;

		// Token: 0x04000821 RID: 2081
		internal const int STATE_DROPTARGET = 128;

		// Token: 0x04000822 RID: 2082
		internal const int STATE_NOZORDER = 256;

		// Token: 0x04000823 RID: 2083
		internal const int STATE_LAYOUTDEFERRED = 512;

		// Token: 0x04000824 RID: 2084
		internal const int STATE_USEWAITCURSOR = 1024;

		// Token: 0x04000825 RID: 2085
		internal const int STATE_DISPOSED = 2048;

		// Token: 0x04000826 RID: 2086
		internal const int STATE_DISPOSING = 4096;

		// Token: 0x04000827 RID: 2087
		internal const int STATE_MOUSEENTERPENDING = 8192;

		// Token: 0x04000828 RID: 2088
		internal const int STATE_TRACKINGMOUSEEVENT = 16384;

		// Token: 0x04000829 RID: 2089
		internal const int STATE_THREADMARSHALLPENDING = 32768;

		// Token: 0x0400082A RID: 2090
		internal const int STATE_SIZELOCKEDBYOS = 65536;

		// Token: 0x0400082B RID: 2091
		internal const int STATE_CAUSESVALIDATION = 131072;

		// Token: 0x0400082C RID: 2092
		internal const int STATE_CREATINGHANDLE = 262144;

		// Token: 0x0400082D RID: 2093
		internal const int STATE_TOPLEVEL = 524288;

		// Token: 0x0400082E RID: 2094
		internal const int STATE_ISACCESSIBLE = 1048576;

		// Token: 0x0400082F RID: 2095
		internal const int STATE_OWNCTLBRUSH = 2097152;

		// Token: 0x04000830 RID: 2096
		internal const int STATE_EXCEPTIONWHILEPAINTING = 4194304;

		// Token: 0x04000831 RID: 2097
		internal const int STATE_LAYOUTISDIRTY = 8388608;

		// Token: 0x04000832 RID: 2098
		internal const int STATE_CHECKEDHOST = 16777216;

		// Token: 0x04000833 RID: 2099
		internal const int STATE_HOSTEDINDIALOG = 33554432;

		// Token: 0x04000834 RID: 2100
		internal const int STATE_DOUBLECLICKFIRED = 67108864;

		// Token: 0x04000835 RID: 2101
		internal const int STATE_MOUSEPRESSED = 134217728;

		// Token: 0x04000836 RID: 2102
		internal const int STATE_VALIDATIONCANCELLED = 268435456;

		// Token: 0x04000837 RID: 2103
		internal const int STATE_PARENTRECREATING = 536870912;

		// Token: 0x04000838 RID: 2104
		internal const int STATE_MIRRORED = 1073741824;

		// Token: 0x04000839 RID: 2105
		private const int STATE2_HAVEINVOKED = 1;

		// Token: 0x0400083A RID: 2106
		private const int STATE2_SETSCROLLPOS = 2;

		// Token: 0x0400083B RID: 2107
		private const int STATE2_LISTENINGTOUSERPREFERENCECHANGED = 4;

		// Token: 0x0400083C RID: 2108
		internal const int STATE2_INTERESTEDINUSERPREFERENCECHANGED = 8;

		// Token: 0x0400083D RID: 2109
		internal const int STATE2_MAINTAINSOWNCAPTUREMODE = 16;

		// Token: 0x0400083E RID: 2110
		private const int STATE2_BECOMINGACTIVECONTROL = 32;

		// Token: 0x0400083F RID: 2111
		private const int STATE2_CLEARLAYOUTARGS = 64;

		// Token: 0x04000840 RID: 2112
		private const int STATE2_INPUTKEY = 128;

		// Token: 0x04000841 RID: 2113
		private const int STATE2_INPUTCHAR = 256;

		// Token: 0x04000842 RID: 2114
		private const int STATE2_UICUES = 512;

		// Token: 0x04000843 RID: 2115
		private const int STATE2_ISACTIVEX = 1024;

		// Token: 0x04000844 RID: 2116
		internal const int STATE2_USEPREFERREDSIZECACHE = 2048;

		// Token: 0x04000845 RID: 2117
		internal const int STATE2_TOPMDIWINDOWCLOSING = 4096;

		// Token: 0x04000846 RID: 2118
		internal const int STATE2_CURRENTLYBEINGSCALED = 8192;

		// Token: 0x04000847 RID: 2119
		private static readonly object EventAutoSizeChanged = new object();

		// Token: 0x04000848 RID: 2120
		private static readonly object EventKeyDown = new object();

		// Token: 0x04000849 RID: 2121
		private static readonly object EventKeyPress = new object();

		// Token: 0x0400084A RID: 2122
		private static readonly object EventKeyUp = new object();

		// Token: 0x0400084B RID: 2123
		private static readonly object EventMouseDown = new object();

		// Token: 0x0400084C RID: 2124
		private static readonly object EventMouseEnter = new object();

		// Token: 0x0400084D RID: 2125
		private static readonly object EventMouseLeave = new object();

		// Token: 0x0400084E RID: 2126
		private static readonly object EventDpiChangedBeforeParent = new object();

		// Token: 0x0400084F RID: 2127
		private static readonly object EventDpiChangedAfterParent = new object();

		// Token: 0x04000850 RID: 2128
		private static readonly object EventMouseHover = new object();

		// Token: 0x04000851 RID: 2129
		private static readonly object EventMouseMove = new object();

		// Token: 0x04000852 RID: 2130
		private static readonly object EventMouseUp = new object();

		// Token: 0x04000853 RID: 2131
		private static readonly object EventMouseWheel = new object();

		// Token: 0x04000854 RID: 2132
		private static readonly object EventClick = new object();

		// Token: 0x04000855 RID: 2133
		private static readonly object EventClientSize = new object();

		// Token: 0x04000856 RID: 2134
		private static readonly object EventDoubleClick = new object();

		// Token: 0x04000857 RID: 2135
		private static readonly object EventMouseClick = new object();

		// Token: 0x04000858 RID: 2136
		private static readonly object EventMouseDoubleClick = new object();

		// Token: 0x04000859 RID: 2137
		private static readonly object EventMouseCaptureChanged = new object();

		// Token: 0x0400085A RID: 2138
		private static readonly object EventMove = new object();

		// Token: 0x0400085B RID: 2139
		private static readonly object EventResize = new object();

		// Token: 0x0400085C RID: 2140
		private static readonly object EventLayout = new object();

		// Token: 0x0400085D RID: 2141
		private static readonly object EventGotFocus = new object();

		// Token: 0x0400085E RID: 2142
		private static readonly object EventLostFocus = new object();

		// Token: 0x0400085F RID: 2143
		private static readonly object EventEnabledChanged = new object();

		// Token: 0x04000860 RID: 2144
		private static readonly object EventEnter = new object();

		// Token: 0x04000861 RID: 2145
		private static readonly object EventLeave = new object();

		// Token: 0x04000862 RID: 2146
		private static readonly object EventHandleCreated = new object();

		// Token: 0x04000863 RID: 2147
		private static readonly object EventHandleDestroyed = new object();

		// Token: 0x04000864 RID: 2148
		private static readonly object EventVisibleChanged = new object();

		// Token: 0x04000865 RID: 2149
		private static readonly object EventControlAdded = new object();

		// Token: 0x04000866 RID: 2150
		private static readonly object EventControlRemoved = new object();

		// Token: 0x04000867 RID: 2151
		private static readonly object EventChangeUICues = new object();

		// Token: 0x04000868 RID: 2152
		private static readonly object EventSystemColorsChanged = new object();

		// Token: 0x04000869 RID: 2153
		private static readonly object EventValidating = new object();

		// Token: 0x0400086A RID: 2154
		private static readonly object EventValidated = new object();

		// Token: 0x0400086B RID: 2155
		private static readonly object EventStyleChanged = new object();

		// Token: 0x0400086C RID: 2156
		private static readonly object EventImeModeChanged = new object();

		// Token: 0x0400086D RID: 2157
		private static readonly object EventHelpRequested = new object();

		// Token: 0x0400086E RID: 2158
		private static readonly object EventPaint = new object();

		// Token: 0x0400086F RID: 2159
		private static readonly object EventInvalidated = new object();

		// Token: 0x04000870 RID: 2160
		private static readonly object EventQueryContinueDrag = new object();

		// Token: 0x04000871 RID: 2161
		private static readonly object EventGiveFeedback = new object();

		// Token: 0x04000872 RID: 2162
		private static readonly object EventDragEnter = new object();

		// Token: 0x04000873 RID: 2163
		private static readonly object EventDragLeave = new object();

		// Token: 0x04000874 RID: 2164
		private static readonly object EventDragOver = new object();

		// Token: 0x04000875 RID: 2165
		private static readonly object EventDragDrop = new object();

		// Token: 0x04000876 RID: 2166
		private static readonly object EventQueryAccessibilityHelp = new object();

		// Token: 0x04000877 RID: 2167
		private static readonly object EventBackgroundImage = new object();

		// Token: 0x04000878 RID: 2168
		private static readonly object EventBackgroundImageLayout = new object();

		// Token: 0x04000879 RID: 2169
		private static readonly object EventBindingContext = new object();

		// Token: 0x0400087A RID: 2170
		private static readonly object EventBackColor = new object();

		// Token: 0x0400087B RID: 2171
		private static readonly object EventParent = new object();

		// Token: 0x0400087C RID: 2172
		private static readonly object EventVisible = new object();

		// Token: 0x0400087D RID: 2173
		private static readonly object EventText = new object();

		// Token: 0x0400087E RID: 2174
		private static readonly object EventTabStop = new object();

		// Token: 0x0400087F RID: 2175
		private static readonly object EventTabIndex = new object();

		// Token: 0x04000880 RID: 2176
		private static readonly object EventSize = new object();

		// Token: 0x04000881 RID: 2177
		private static readonly object EventRightToLeft = new object();

		// Token: 0x04000882 RID: 2178
		private static readonly object EventLocation = new object();

		// Token: 0x04000883 RID: 2179
		private static readonly object EventForeColor = new object();

		// Token: 0x04000884 RID: 2180
		private static readonly object EventFont = new object();

		// Token: 0x04000885 RID: 2181
		private static readonly object EventEnabled = new object();

		// Token: 0x04000886 RID: 2182
		private static readonly object EventDock = new object();

		// Token: 0x04000887 RID: 2183
		private static readonly object EventCursor = new object();

		// Token: 0x04000888 RID: 2184
		private static readonly object EventContextMenu = new object();

		// Token: 0x04000889 RID: 2185
		private static readonly object EventContextMenuStrip = new object();

		// Token: 0x0400088A RID: 2186
		private static readonly object EventCausesValidation = new object();

		// Token: 0x0400088B RID: 2187
		private static readonly object EventRegionChanged = new object();

		// Token: 0x0400088C RID: 2188
		private static readonly object EventMarginChanged = new object();

		// Token: 0x0400088D RID: 2189
		internal static readonly object EventPaddingChanged = new object();

		// Token: 0x0400088E RID: 2190
		private static readonly object EventPreviewKeyDown = new object();

		// Token: 0x0400088F RID: 2191
		private static int mouseWheelMessage = 522;

		// Token: 0x04000890 RID: 2192
		private static bool mouseWheelRoutingNeeded;

		// Token: 0x04000891 RID: 2193
		private static bool mouseWheelInit;

		// Token: 0x04000892 RID: 2194
		private static int threadCallbackMessage;

		// Token: 0x04000893 RID: 2195
		private static bool checkForIllegalCrossThreadCalls = Debugger.IsAttached;

		// Token: 0x04000894 RID: 2196
		private static ContextCallback invokeMarshaledCallbackHelperDelegate;

		// Token: 0x04000895 RID: 2197
		[ThreadStatic]
		private static bool inCrossThreadSafeCall = false;

		// Token: 0x04000896 RID: 2198
		[ThreadStatic]
		internal static HelpInfo currentHelpInfo = null;

		// Token: 0x04000897 RID: 2199
		private static Control.FontHandleWrapper defaultFontHandleWrapper;

		// Token: 0x04000898 RID: 2200
		private const short PaintLayerBackground = 1;

		// Token: 0x04000899 RID: 2201
		private const short PaintLayerForeground = 2;

		// Token: 0x0400089A RID: 2202
		private const byte RequiredScalingEnabledMask = 16;

		// Token: 0x0400089B RID: 2203
		private const byte RequiredScalingMask = 15;

		// Token: 0x0400089C RID: 2204
		private const byte HighOrderBitMask = 128;

		// Token: 0x0400089D RID: 2205
		private static Font defaultFont;

		// Token: 0x0400089E RID: 2206
		private static readonly int PropName = PropertyStore.CreateKey();

		// Token: 0x0400089F RID: 2207
		private static readonly int PropBackBrush = PropertyStore.CreateKey();

		// Token: 0x040008A0 RID: 2208
		private static readonly int PropFontHeight = PropertyStore.CreateKey();

		// Token: 0x040008A1 RID: 2209
		private static readonly int PropCurrentAmbientFont = PropertyStore.CreateKey();

		// Token: 0x040008A2 RID: 2210
		private static readonly int PropControlsCollection = PropertyStore.CreateKey();

		// Token: 0x040008A3 RID: 2211
		private static readonly int PropBackColor = PropertyStore.CreateKey();

		// Token: 0x040008A4 RID: 2212
		private static readonly int PropForeColor = PropertyStore.CreateKey();

		// Token: 0x040008A5 RID: 2213
		private static readonly int PropFont = PropertyStore.CreateKey();

		// Token: 0x040008A6 RID: 2214
		private static readonly int PropBackgroundImage = PropertyStore.CreateKey();

		// Token: 0x040008A7 RID: 2215
		private static readonly int PropFontHandleWrapper = PropertyStore.CreateKey();

		// Token: 0x040008A8 RID: 2216
		private static readonly int PropUserData = PropertyStore.CreateKey();

		// Token: 0x040008A9 RID: 2217
		private static readonly int PropContextMenu = PropertyStore.CreateKey();

		// Token: 0x040008AA RID: 2218
		private static readonly int PropCursor = PropertyStore.CreateKey();

		// Token: 0x040008AB RID: 2219
		private static readonly int PropRegion = PropertyStore.CreateKey();

		// Token: 0x040008AC RID: 2220
		private static readonly int PropRightToLeft = PropertyStore.CreateKey();

		// Token: 0x040008AD RID: 2221
		private static readonly int PropBindings = PropertyStore.CreateKey();

		// Token: 0x040008AE RID: 2222
		private static readonly int PropBindingManager = PropertyStore.CreateKey();

		// Token: 0x040008AF RID: 2223
		private static readonly int PropAccessibleDefaultActionDescription = PropertyStore.CreateKey();

		// Token: 0x040008B0 RID: 2224
		private static readonly int PropAccessibleDescription = PropertyStore.CreateKey();

		// Token: 0x040008B1 RID: 2225
		private static readonly int PropAccessibility = PropertyStore.CreateKey();

		// Token: 0x040008B2 RID: 2226
		private static readonly int PropUnsafeAccessibility = PropertyStore.CreateKey();

		// Token: 0x040008B3 RID: 2227
		private static readonly int PropNcAccessibility = PropertyStore.CreateKey();

		// Token: 0x040008B4 RID: 2228
		private static readonly int PropAccessibleName = PropertyStore.CreateKey();

		// Token: 0x040008B5 RID: 2229
		private static readonly int PropAccessibleRole = PropertyStore.CreateKey();

		// Token: 0x040008B6 RID: 2230
		private static readonly int PropPaintingException = PropertyStore.CreateKey();

		// Token: 0x040008B7 RID: 2231
		private static readonly int PropActiveXImpl = PropertyStore.CreateKey();

		// Token: 0x040008B8 RID: 2232
		private static readonly int PropControlVersionInfo = PropertyStore.CreateKey();

		// Token: 0x040008B9 RID: 2233
		private static readonly int PropBackgroundImageLayout = PropertyStore.CreateKey();

		// Token: 0x040008BA RID: 2234
		private static readonly int PropAccessibleHelpProvider = PropertyStore.CreateKey();

		// Token: 0x040008BB RID: 2235
		private static readonly int PropContextMenuStrip = PropertyStore.CreateKey();

		// Token: 0x040008BC RID: 2236
		private static readonly int PropAutoScrollOffset = PropertyStore.CreateKey();

		// Token: 0x040008BD RID: 2237
		private static readonly int PropUseCompatibleTextRendering = PropertyStore.CreateKey();

		// Token: 0x040008BE RID: 2238
		private static readonly int PropImeWmCharsToIgnore = PropertyStore.CreateKey();

		// Token: 0x040008BF RID: 2239
		private static readonly int PropImeMode = PropertyStore.CreateKey();

		// Token: 0x040008C0 RID: 2240
		private static readonly int PropDisableImeModeChangedCount = PropertyStore.CreateKey();

		// Token: 0x040008C1 RID: 2241
		private static readonly int PropLastCanEnableIme = PropertyStore.CreateKey();

		// Token: 0x040008C2 RID: 2242
		private static readonly int PropCacheTextCount = PropertyStore.CreateKey();

		// Token: 0x040008C3 RID: 2243
		private static readonly int PropCacheTextField = PropertyStore.CreateKey();

		// Token: 0x040008C4 RID: 2244
		private static readonly int PropAmbientPropertiesService = PropertyStore.CreateKey();

		// Token: 0x040008C5 RID: 2245
		private static bool needToLoadComCtl = true;

		// Token: 0x040008C6 RID: 2246
		internal static bool UseCompatibleTextRenderingDefault = true;

		// Token: 0x040008C7 RID: 2247
		private Control.ControlNativeWindow window;

		// Token: 0x040008C8 RID: 2248
		private Control parent;

		// Token: 0x040008C9 RID: 2249
		private Control reflectParent;

		// Token: 0x040008CA RID: 2250
		private CreateParams createParams;

		// Token: 0x040008CB RID: 2251
		private int x;

		// Token: 0x040008CC RID: 2252
		private int y;

		// Token: 0x040008CD RID: 2253
		private int width;

		// Token: 0x040008CE RID: 2254
		private int height;

		// Token: 0x040008CF RID: 2255
		private int clientWidth;

		// Token: 0x040008D0 RID: 2256
		private int clientHeight;

		// Token: 0x040008D1 RID: 2257
		private int state;

		// Token: 0x040008D2 RID: 2258
		private int state2;

		// Token: 0x040008D3 RID: 2259
		private ControlStyles controlStyle;

		// Token: 0x040008D4 RID: 2260
		private int tabIndex;

		// Token: 0x040008D5 RID: 2261
		private string text;

		// Token: 0x040008D6 RID: 2262
		private byte layoutSuspendCount;

		// Token: 0x040008D7 RID: 2263
		private byte requiredScaling;

		// Token: 0x040008D8 RID: 2264
		private PropertyStore propertyStore;

		// Token: 0x040008D9 RID: 2265
		private NativeMethods.TRACKMOUSEEVENT trackMouseEvent;

		// Token: 0x040008DA RID: 2266
		private short updateCount;

		// Token: 0x040008DB RID: 2267
		private LayoutEventArgs cachedLayoutEventArgs;

		// Token: 0x040008DC RID: 2268
		private Queue threadCallbackList;

		// Token: 0x040008DD RID: 2269
		internal int deviceDpi;

		// Token: 0x040008DE RID: 2270
		private int uiCuesState;

		// Token: 0x040008DF RID: 2271
		private const int UISTATE_FOCUS_CUES_MASK = 15;

		// Token: 0x040008E0 RID: 2272
		private const int UISTATE_FOCUS_CUES_HIDDEN = 1;

		// Token: 0x040008E1 RID: 2273
		private const int UISTATE_FOCUS_CUES_SHOW = 2;

		// Token: 0x040008E2 RID: 2274
		private const int UISTATE_KEYBOARD_CUES_MASK = 240;

		// Token: 0x040008E3 RID: 2275
		private const int UISTATE_KEYBOARD_CUES_HIDDEN = 16;

		// Token: 0x040008E4 RID: 2276
		private const int UISTATE_KEYBOARD_CUES_SHOW = 32;

		// Token: 0x040008E5 RID: 2277
		[ThreadStatic]
		private static byte[] tempKeyboardStateArray;

		// Token: 0x040008E6 RID: 2278
		private readonly WeakReference<ToolStripControlHost> toolStripControlHostReference = new WeakReference<ToolStripControlHost>(null);

		// Token: 0x040008E7 RID: 2279
		private const int ImeCharsToIgnoreDisabled = -1;

		// Token: 0x040008E8 RID: 2280
		private const int ImeCharsToIgnoreEnabled = 0;

		// Token: 0x040008E9 RID: 2281
		private static ImeMode propagatingImeMode = ImeMode.Inherit;

		// Token: 0x040008EA RID: 2282
		private static bool ignoreWmImeNotify;

		// Token: 0x040008EB RID: 2283
		private static bool lastLanguageChinese = false;

		// Token: 0x02000632 RID: 1586
		private class ControlTabOrderHolder
		{
			// Token: 0x060063E1 RID: 25569 RVA: 0x0017165C File Offset: 0x0016F85C
			internal ControlTabOrderHolder(int oldOrder, int newOrder, Control control)
			{
				this.oldOrder = oldOrder;
				this.newOrder = newOrder;
				this.control = control;
			}

			// Token: 0x0400394B RID: 14667
			internal readonly int oldOrder;

			// Token: 0x0400394C RID: 14668
			internal readonly int newOrder;

			// Token: 0x0400394D RID: 14669
			internal readonly Control control;
		}

		// Token: 0x02000633 RID: 1587
		private class ControlTabOrderComparer : IComparer
		{
			// Token: 0x060063E2 RID: 25570 RVA: 0x0017167C File Offset: 0x0016F87C
			int IComparer.Compare(object x, object y)
			{
				Control.ControlTabOrderHolder controlTabOrderHolder = (Control.ControlTabOrderHolder)x;
				Control.ControlTabOrderHolder controlTabOrderHolder2 = (Control.ControlTabOrderHolder)y;
				int num = controlTabOrderHolder.newOrder - controlTabOrderHolder2.newOrder;
				if (num == 0)
				{
					num = controlTabOrderHolder.oldOrder - controlTabOrderHolder2.oldOrder;
				}
				return num;
			}
		}

		// Token: 0x02000634 RID: 1588
		internal sealed class ControlNativeWindow : NativeWindow, IWindowTarget
		{
			// Token: 0x060063E4 RID: 25572 RVA: 0x001716B7 File Offset: 0x0016F8B7
			internal ControlNativeWindow(Control control)
			{
				this.control = control;
				this.target = this;
			}

			// Token: 0x060063E5 RID: 25573 RVA: 0x001716CD File Offset: 0x0016F8CD
			internal Control GetControl()
			{
				return this.control;
			}

			// Token: 0x060063E6 RID: 25574 RVA: 0x001716D5 File Offset: 0x0016F8D5
			protected override void OnHandleChange()
			{
				this.target.OnHandleChange(base.Handle);
			}

			// Token: 0x060063E7 RID: 25575 RVA: 0x001716E8 File Offset: 0x0016F8E8
			public void OnHandleChange(IntPtr newHandle)
			{
				this.control.SetHandle(newHandle);
			}

			// Token: 0x060063E8 RID: 25576 RVA: 0x001716F6 File Offset: 0x0016F8F6
			internal void LockReference(bool locked)
			{
				if (locked)
				{
					if (!this.rootRef.IsAllocated)
					{
						this.rootRef = GCHandle.Alloc(this.GetControl(), GCHandleType.Normal);
						return;
					}
				}
				else if (this.rootRef.IsAllocated)
				{
					this.rootRef.Free();
				}
			}

			// Token: 0x060063E9 RID: 25577 RVA: 0x00171733 File Offset: 0x0016F933
			protected override void OnThreadException(Exception e)
			{
				this.control.WndProcException(e);
			}

			// Token: 0x060063EA RID: 25578 RVA: 0x00171741 File Offset: 0x0016F941
			public void OnMessage(ref Message m)
			{
				this.control.WndProc(ref m);
			}

			// Token: 0x17001567 RID: 5479
			// (get) Token: 0x060063EB RID: 25579 RVA: 0x0017174F File Offset: 0x0016F94F
			// (set) Token: 0x060063EC RID: 25580 RVA: 0x00171757 File Offset: 0x0016F957
			internal IWindowTarget WindowTarget
			{
				get
				{
					return this.target;
				}
				set
				{
					this.target = value;
				}
			}

			// Token: 0x060063ED RID: 25581 RVA: 0x00171760 File Offset: 0x0016F960
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg != 512)
				{
					if (msg != 522)
					{
						if (msg == 675)
						{
							this.control.UnhookMouseEvent();
						}
					}
					else
					{
						this.control.ResetMouseEventArgs();
					}
				}
				else if (!this.control.GetState(16384))
				{
					this.control.HookMouseEvent();
					if (!this.control.GetState(8192))
					{
						this.control.SendMessage(NativeMethods.WM_MOUSEENTER, 0, 0);
					}
					else
					{
						this.control.SetState(8192, false);
					}
				}
				this.target.OnMessage(ref m);
			}

			// Token: 0x0400394E RID: 14670
			private Control control;

			// Token: 0x0400394F RID: 14671
			private GCHandle rootRef;

			// Token: 0x04003950 RID: 14672
			internal IWindowTarget target;
		}

		/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.Control" /> objects.</summary>
		// Token: 0x02000635 RID: 1589
		[ListBindable(false)]
		[ComVisible(false)]
		public class ControlCollection : ArrangedElementCollection, IList, ICollection, IEnumerable, ICloneable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control.ControlCollection" /> class.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.Control" /> representing the control that owns the control collection.</param>
			// Token: 0x060063EE RID: 25582 RVA: 0x00171807 File Offset: 0x0016FA07
			public ControlCollection(Control owner)
			{
				this.owner = owner;
			}

			/// <summary>Determines whether the <see cref="T:System.Windows.Forms.Control.ControlCollection" /> contains an item with the specified key.</summary>
			/// <param name="key">The key to locate in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</param>
			/// <returns>
			///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.Control.ControlCollection" /> contains an item with the specified key; otherwise, <see langword="false" />.</returns>
			// Token: 0x060063EF RID: 25583 RVA: 0x0017181D File Offset: 0x0016FA1D
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			/// <summary>Adds the specified control to the control collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to add to the control collection.</param>
			/// <exception cref="T:System.Exception">The specified control is a top-level control, or a circular control reference would result if this control were added to the control collection.</exception>
			/// <exception cref="T:System.ArgumentException">The object assigned to the <paramref name="value" /> parameter is not a <see cref="T:System.Windows.Forms.Control" />.</exception>
			// Token: 0x060063F0 RID: 25584 RVA: 0x0017182C File Offset: 0x0016FA2C
			public virtual void Add(Control value)
			{
				if (value == null)
				{
					return;
				}
				if (value.GetTopLevel())
				{
					throw new ArgumentException(SR.GetString("TopLevelControlAdd"));
				}
				if (this.owner.CreateThreadId != value.CreateThreadId)
				{
					throw new ArgumentException(SR.GetString("AddDifferentThreads"));
				}
				Control.CheckParentingCycle(this.owner, value);
				if (value.parent == this.owner)
				{
					value.SendToBack();
					return;
				}
				if (value.parent != null)
				{
					value.parent.Controls.Remove(value);
				}
				base.InnerList.Add(value);
				if (value.tabIndex == -1)
				{
					int num = 0;
					for (int i = 0; i < this.Count - 1; i++)
					{
						int tabIndex = this[i].TabIndex;
						if (num <= tabIndex)
						{
							num = tabIndex + 1;
						}
					}
					value.tabIndex = num;
				}
				this.owner.SuspendLayout();
				try
				{
					Control parent = value.parent;
					try
					{
						value.AssignParent(this.owner);
					}
					finally
					{
						if (parent != value.parent && (this.owner.state & 1) != 0)
						{
							value.SetParentHandle(this.owner.InternalHandle);
							if (value.Visible)
							{
								value.CreateControl();
							}
						}
					}
					value.InitLayout();
				}
				finally
				{
					this.owner.ResumeLayout(false);
				}
				LayoutTransaction.DoLayout(this.owner, value, PropertyNames.Parent);
				this.owner.OnControlAdded(new ControlEventArgs(value));
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <param name="control">The object to add to this collection.</param>
			/// <returns>The position into which the new element was inserted, or -1 to indicate that the item was not inserted into the collection.</returns>
			// Token: 0x060063F1 RID: 25585 RVA: 0x001719A4 File Offset: 0x0016FBA4
			int IList.Add(object control)
			{
				if (control is Control)
				{
					this.Add((Control)control);
					return this.IndexOf((Control)control);
				}
				throw new ArgumentException(SR.GetString("ControlBadControl"), "control");
			}

			/// <summary>Adds an array of control objects to the collection.</summary>
			/// <param name="controls">An array of <see cref="T:System.Windows.Forms.Control" /> objects to add to the collection.</param>
			// Token: 0x060063F2 RID: 25586 RVA: 0x001719DC File Offset: 0x0016FBDC
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public virtual void AddRange(Control[] controls)
			{
				if (controls == null)
				{
					throw new ArgumentNullException("controls");
				}
				if (controls.Length != 0)
				{
					this.owner.SuspendLayout();
					try
					{
						for (int i = 0; i < controls.Length; i++)
						{
							this.Add(controls[i]);
						}
					}
					finally
					{
						this.owner.ResumeLayout(true);
					}
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
			/// <returns>A new object that is a copy of this instance.</returns>
			// Token: 0x060063F3 RID: 25587 RVA: 0x00171A3C File Offset: 0x0016FC3C
			object ICloneable.Clone()
			{
				Control.ControlCollection controlCollection = this.owner.CreateControlsInstance();
				controlCollection.InnerList.AddRange(this);
				return controlCollection;
			}

			/// <summary>Determines whether the specified control is a member of the collection.</summary>
			/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.Control" /> is a member of the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x060063F4 RID: 25588 RVA: 0x0011C768 File Offset: 0x0011A968
			public bool Contains(Control control)
			{
				return base.InnerList.Contains(control);
			}

			/// <summary>Searches for controls by their <see cref="P:System.Windows.Forms.Control.Name" /> property and builds an array of all the controls that match.</summary>
			/// <param name="key">The key to locate in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</param>
			/// <param name="searchAllChildren">
			///   <see langword="true" /> to search all child controls; otherwise, <see langword="false" />.</param>
			/// <returns>An array of type <see cref="T:System.Windows.Forms.Control" /> containing the matching controls.</returns>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="key" /> is <see langword="null" /> or the empty string ("").</exception>
			// Token: 0x060063F5 RID: 25589 RVA: 0x00171A64 File Offset: 0x0016FC64
			public Control[] Find(string key, bool searchAllChildren)
			{
				if (string.IsNullOrEmpty(key))
				{
					throw new ArgumentNullException("key", SR.GetString("FindKeyMayNotBeEmptyOrNull"));
				}
				ArrayList arrayList = this.FindInternal(key, searchAllChildren, this, new ArrayList());
				Control[] array = new Control[arrayList.Count];
				arrayList.CopyTo(array, 0);
				return array;
			}

			// Token: 0x060063F6 RID: 25590 RVA: 0x00171AB4 File Offset: 0x0016FCB4
			private ArrayList FindInternal(string key, bool searchAllChildren, Control.ControlCollection controlsToLookIn, ArrayList foundControls)
			{
				if (controlsToLookIn == null || foundControls == null)
				{
					return null;
				}
				try
				{
					for (int i = 0; i < controlsToLookIn.Count; i++)
					{
						if (controlsToLookIn[i] != null && WindowsFormsUtils.SafeCompareStrings(controlsToLookIn[i].Name, key, true))
						{
							foundControls.Add(controlsToLookIn[i]);
						}
					}
					if (searchAllChildren)
					{
						for (int j = 0; j < controlsToLookIn.Count; j++)
						{
							if (controlsToLookIn[j] != null && controlsToLookIn[j].Controls != null && controlsToLookIn[j].Controls.Count > 0)
							{
								foundControls = this.FindInternal(key, searchAllChildren, controlsToLookIn[j].Controls, foundControls);
							}
						}
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				return foundControls;
			}

			/// <summary>Retrieves a reference to an enumerator object that is used to iterate over a <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" />.</returns>
			// Token: 0x060063F7 RID: 25591 RVA: 0x00171B84 File Offset: 0x0016FD84
			public override IEnumerator GetEnumerator()
			{
				return new Control.ControlCollection.ControlCollectionEnumerator(this);
			}

			/// <summary>Retrieves the index of the specified control in the control collection.</summary>
			/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to locate in the collection.</param>
			/// <returns>A zero-based index value that represents the position of the specified <see cref="T:System.Windows.Forms.Control" /> in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
			// Token: 0x060063F8 RID: 25592 RVA: 0x0011CACC File Offset: 0x0011ACCC
			public int IndexOf(Control control)
			{
				return base.InnerList.IndexOf(control);
			}

			/// <summary>Retrieves the index of the first occurrence of the specified item within the collection.</summary>
			/// <param name="key">The name of the control to search for.</param>
			/// <returns>The zero-based index of the first occurrence of the control with the specified name in the collection.</returns>
			// Token: 0x060063F9 RID: 25593 RVA: 0x00171B8C File Offset: 0x0016FD8C
			public virtual int IndexOfKey(string key)
			{
				if (string.IsNullOrEmpty(key))
				{
					return -1;
				}
				if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
				{
					return this.lastAccessedIndex;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, true))
					{
						this.lastAccessedIndex = i;
						return i;
					}
				}
				this.lastAccessedIndex = -1;
				return -1;
			}

			// Token: 0x060063FA RID: 25594 RVA: 0x0011CB5C File Offset: 0x0011AD5C
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			/// <summary>Gets the control that owns this <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that owns this <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
			// Token: 0x17001568 RID: 5480
			// (get) Token: 0x060063FB RID: 25595 RVA: 0x00171C09 File Offset: 0x0016FE09
			public Control Owner
			{
				get
				{
					return this.owner;
				}
			}

			/// <summary>Removes the specified control from the control collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to remove from the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</param>
			// Token: 0x060063FC RID: 25596 RVA: 0x00171C14 File Offset: 0x0016FE14
			public virtual void Remove(Control value)
			{
				if (value == null)
				{
					return;
				}
				if (value.ParentInternal == this.owner)
				{
					value.SetParentHandle(IntPtr.Zero);
					base.InnerList.Remove(value);
					value.AssignParent(null);
					LayoutTransaction.DoLayout(this.owner, value, PropertyNames.Parent);
					this.owner.OnControlRemoved(new ControlEventArgs(value));
					ContainerControl containerControl = this.owner.GetContainerControlInternal() as ContainerControl;
					if (containerControl != null)
					{
						containerControl.AfterControlRemoved(value, this.owner);
					}
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="control" />
			// Token: 0x060063FD RID: 25597 RVA: 0x00171C94 File Offset: 0x0016FE94
			void IList.Remove(object control)
			{
				if (control is Control)
				{
					this.Remove((Control)control);
				}
			}

			/// <summary>Removes a control from the control collection at the specified indexed location.</summary>
			/// <param name="index">The index value of the <see cref="T:System.Windows.Forms.Control" /> to remove.</param>
			// Token: 0x060063FE RID: 25598 RVA: 0x00171CAA File Offset: 0x0016FEAA
			public void RemoveAt(int index)
			{
				this.Remove(this[index]);
			}

			/// <summary>Removes the child control with the specified key.</summary>
			/// <param name="key">The name of the child control to remove.</param>
			// Token: 0x060063FF RID: 25599 RVA: 0x00171CBC File Offset: 0x0016FEBC
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (this.IsValidIndex(num))
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Indicates the <see cref="T:System.Windows.Forms.Control" /> at the specified indexed location in the collection.</summary>
			/// <param name="index">The index of the control to retrieve from the control collection.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.Control" /> located at the specified index location within the control collection.</returns>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than zero or is greater than or equal to the number of controls in the collection.</exception>
			// Token: 0x17001569 RID: 5481
			public virtual Control this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("IndexOutOfRange", new object[] { index.ToString(CultureInfo.CurrentCulture) }));
					}
					return (Control)base.InnerList[index];
				}
			}

			/// <summary>Indicates a <see cref="T:System.Windows.Forms.Control" /> with the specified key in the collection.</summary>
			/// <param name="key">The name of the control to retrieve from the control collection.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.Control" /> with the specified key within the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
			// Token: 0x1700156A RID: 5482
			public virtual Control this[string key]
			{
				get
				{
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					int num = this.IndexOfKey(key);
					if (this.IsValidIndex(num))
					{
						return this[num];
					}
					return null;
				}
			}

			/// <summary>Removes all controls from the collection.</summary>
			// Token: 0x06006402 RID: 25602 RVA: 0x00171D70 File Offset: 0x0016FF70
			public virtual void Clear()
			{
				this.owner.SuspendLayout();
				CommonProperties.xClearAllPreferredSizeCaches(this.owner);
				try
				{
					while (this.Count != 0)
					{
						this.RemoveAt(this.Count - 1);
					}
				}
				finally
				{
					this.owner.ResumeLayout();
				}
			}

			/// <summary>Retrieves the index of the specified child control within the control collection.</summary>
			/// <param name="child">The <see cref="T:System.Windows.Forms.Control" /> to search for in the control collection.</param>
			/// <returns>A zero-based index value that represents the location of the specified child control within the control collection.</returns>
			/// <exception cref="T:System.ArgumentException">The <paramref name="child" /><see cref="T:System.Windows.Forms.Control" /> is not in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</exception>
			// Token: 0x06006403 RID: 25603 RVA: 0x00171DCC File Offset: 0x0016FFCC
			public int GetChildIndex(Control child)
			{
				return this.GetChildIndex(child, true);
			}

			/// <summary>Retrieves the index of the specified child control within the control collection, and optionally raises an exception if the specified control is not within the control collection.</summary>
			/// <param name="child">The <see cref="T:System.Windows.Forms.Control" /> to search for in the control collection.</param>
			/// <param name="throwException">
			///   <see langword="true" /> to throw an exception if the <see cref="T:System.Windows.Forms.Control" /> specified in the <paramref name="child" /> parameter is not a control in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />; otherwise, <see langword="false" />.</param>
			/// <returns>A zero-based index value that represents the location of the specified child control within the control collection; otherwise -1 if the specified <see cref="T:System.Windows.Forms.Control" /> is not found in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
			/// <exception cref="T:System.ArgumentException">The <paramref name="child" /><see cref="T:System.Windows.Forms.Control" /> is not in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />, and the <paramref name="throwException" /> parameter value is <see langword="true" />.</exception>
			// Token: 0x06006404 RID: 25604 RVA: 0x00171DD8 File Offset: 0x0016FFD8
			public virtual int GetChildIndex(Control child, bool throwException)
			{
				int num = this.IndexOf(child);
				if (num == -1 && throwException)
				{
					throw new ArgumentException(SR.GetString("ControlNotChild"));
				}
				return num;
			}

			// Token: 0x06006405 RID: 25605 RVA: 0x00171E08 File Offset: 0x00170008
			internal virtual void SetChildIndexInternal(Control child, int newIndex)
			{
				if (child == null)
				{
					throw new ArgumentNullException("child");
				}
				int childIndex = this.GetChildIndex(child);
				if (childIndex == newIndex)
				{
					return;
				}
				if (newIndex >= this.Count || newIndex == -1)
				{
					newIndex = this.Count - 1;
				}
				base.MoveElement(child, childIndex, newIndex);
				child.UpdateZOrder();
				LayoutTransaction.DoLayout(this.owner, child, PropertyNames.ChildIndex);
			}

			/// <summary>Sets the index of the specified child control in the collection to the specified index value.</summary>
			/// <param name="child">The <paramref name="child" /><see cref="T:System.Windows.Forms.Control" /> to search for.</param>
			/// <param name="newIndex">The new index value of the control.</param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="child" /> control is not in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</exception>
			// Token: 0x06006406 RID: 25606 RVA: 0x00171E67 File Offset: 0x00170067
			public virtual void SetChildIndex(Control child, int newIndex)
			{
				this.SetChildIndexInternal(child, newIndex);
			}

			// Token: 0x04003951 RID: 14673
			private Control owner;

			// Token: 0x04003952 RID: 14674
			private int lastAccessedIndex = -1;

			// Token: 0x020008B4 RID: 2228
			private class ControlCollectionEnumerator : IEnumerator
			{
				// Token: 0x0600728D RID: 29325 RVA: 0x001A29BC File Offset: 0x001A0BBC
				public ControlCollectionEnumerator(Control.ControlCollection controls)
				{
					this.controls = controls;
					this.originalCount = controls.Count;
					this.current = -1;
				}

				// Token: 0x0600728E RID: 29326 RVA: 0x001A29DE File Offset: 0x001A0BDE
				public bool MoveNext()
				{
					if (this.current < this.controls.Count - 1 && this.current < this.originalCount - 1)
					{
						this.current++;
						return true;
					}
					return false;
				}

				// Token: 0x0600728F RID: 29327 RVA: 0x001A2A16 File Offset: 0x001A0C16
				public void Reset()
				{
					this.current = -1;
				}

				// Token: 0x17001931 RID: 6449
				// (get) Token: 0x06007290 RID: 29328 RVA: 0x001A2A1F File Offset: 0x001A0C1F
				public object Current
				{
					get
					{
						if (this.current == -1)
						{
							return null;
						}
						return this.controls[this.current];
					}
				}

				// Token: 0x04004527 RID: 17703
				private Control.ControlCollection controls;

				// Token: 0x04004528 RID: 17704
				private int current;

				// Token: 0x04004529 RID: 17705
				private int originalCount;
			}
		}

		// Token: 0x02000636 RID: 1590
		private class ActiveXImpl : MarshalByRefObject, IWindowTarget
		{
			// Token: 0x06006407 RID: 25607 RVA: 0x00171E74 File Offset: 0x00170074
			internal ActiveXImpl(Control control)
			{
				this.control = control;
				this.controlWindowTarget = control.WindowTarget;
				control.WindowTarget = this;
				this.adviseList = new ArrayList();
				this.activeXState = default(BitVector32);
				this.ambientProperties = new Control.AmbientProperty[]
				{
					new Control.AmbientProperty("Font", -703),
					new Control.AmbientProperty("BackColor", -701),
					new Control.AmbientProperty("ForeColor", -704)
				};
			}

			// Token: 0x1700156B RID: 5483
			// (get) Token: 0x06006408 RID: 25608 RVA: 0x00171F04 File Offset: 0x00170104
			[Browsable(false)]
			[EditorBrowsable(EditorBrowsableState.Advanced)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			internal Color AmbientBackColor
			{
				get
				{
					Control.AmbientProperty ambientProperty = this.LookupAmbient(-701);
					if (ambientProperty.Empty)
					{
						object obj = null;
						if (this.GetAmbientProperty(-701, ref obj) && obj != null)
						{
							try
							{
								ambientProperty.Value = ColorTranslator.FromOle(Convert.ToInt32(obj, CultureInfo.InvariantCulture));
							}
							catch (Exception ex)
							{
								if (ClientUtils.IsSecurityOrCriticalException(ex))
								{
									throw;
								}
							}
						}
					}
					if (ambientProperty.Value == null)
					{
						return Color.Empty;
					}
					return (Color)ambientProperty.Value;
				}
			}

			// Token: 0x1700156C RID: 5484
			// (get) Token: 0x06006409 RID: 25609 RVA: 0x00171F8C File Offset: 0x0017018C
			[Browsable(false)]
			[EditorBrowsable(EditorBrowsableState.Advanced)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			internal Font AmbientFont
			{
				get
				{
					Control.AmbientProperty ambientProperty = this.LookupAmbient(-703);
					if (ambientProperty.Empty)
					{
						object obj = null;
						if (this.GetAmbientProperty(-703, ref obj))
						{
							try
							{
								IntPtr intPtr = IntPtr.Zero;
								UnsafeNativeMethods.IFont font = (UnsafeNativeMethods.IFont)obj;
								IntSecurity.ObjectFromWin32Handle.Assert();
								Font font2 = null;
								try
								{
									intPtr = font.GetHFont();
									font2 = Font.FromHfont(intPtr);
								}
								finally
								{
									CodeAccessPermission.RevertAssert();
								}
								ambientProperty.Value = font2;
							}
							catch (Exception ex)
							{
								if (ClientUtils.IsSecurityOrCriticalException(ex))
								{
									throw;
								}
								ambientProperty.Value = null;
							}
						}
					}
					return (Font)ambientProperty.Value;
				}
			}

			// Token: 0x1700156D RID: 5485
			// (get) Token: 0x0600640A RID: 25610 RVA: 0x00172038 File Offset: 0x00170238
			[Browsable(false)]
			[EditorBrowsable(EditorBrowsableState.Advanced)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			internal Color AmbientForeColor
			{
				get
				{
					Control.AmbientProperty ambientProperty = this.LookupAmbient(-704);
					if (ambientProperty.Empty)
					{
						object obj = null;
						if (this.GetAmbientProperty(-704, ref obj) && obj != null)
						{
							try
							{
								ambientProperty.Value = ColorTranslator.FromOle(Convert.ToInt32(obj, CultureInfo.InvariantCulture));
							}
							catch (Exception ex)
							{
								if (ClientUtils.IsSecurityOrCriticalException(ex))
								{
									throw;
								}
							}
						}
					}
					if (ambientProperty.Value == null)
					{
						return Color.Empty;
					}
					return (Color)ambientProperty.Value;
				}
			}

			// Token: 0x1700156E RID: 5486
			// (get) Token: 0x0600640B RID: 25611 RVA: 0x001720C0 File Offset: 0x001702C0
			// (set) Token: 0x0600640C RID: 25612 RVA: 0x001720D2 File Offset: 0x001702D2
			[Browsable(false)]
			[EditorBrowsable(EditorBrowsableState.Advanced)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			internal bool EventsFrozen
			{
				get
				{
					return this.activeXState[Control.ActiveXImpl.eventsFrozen];
				}
				set
				{
					this.activeXState[Control.ActiveXImpl.eventsFrozen] = value;
				}
			}

			// Token: 0x1700156F RID: 5487
			// (get) Token: 0x0600640D RID: 25613 RVA: 0x001720E5 File Offset: 0x001702E5
			internal IntPtr HWNDParent
			{
				get
				{
					return this.hwndParent;
				}
			}

			// Token: 0x17001570 RID: 5488
			// (get) Token: 0x0600640E RID: 25614 RVA: 0x001720F0 File Offset: 0x001702F0
			internal bool IsIE
			{
				[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (!Control.ActiveXImpl.checkedIE)
					{
						if (this.clientSite == null)
						{
							return false;
						}
						if (Assembly.GetEntryAssembly() == null)
						{
							UnsafeNativeMethods.IOleContainer oleContainer;
							if (NativeMethods.Succeeded(this.clientSite.GetContainer(out oleContainer)) && oleContainer is NativeMethods.IHTMLDocument)
							{
								Control.ActiveXImpl.isIE = true;
							}
							if (oleContainer != null && UnsafeNativeMethods.IsComObject(oleContainer))
							{
								UnsafeNativeMethods.ReleaseComObject(oleContainer);
							}
						}
						Control.ActiveXImpl.checkedIE = true;
					}
					return Control.ActiveXImpl.isIE;
				}
			}

			// Token: 0x17001571 RID: 5489
			// (get) Token: 0x0600640F RID: 25615 RVA: 0x0017215C File Offset: 0x0017035C
			private Point LogPixels
			{
				get
				{
					if (Control.ActiveXImpl.logPixels.IsEmpty)
					{
						Control.ActiveXImpl.logPixels = default(Point);
						IntPtr dc = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
						Control.ActiveXImpl.logPixels.X = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 88);
						Control.ActiveXImpl.logPixels.Y = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 90);
						UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dc));
					}
					return Control.ActiveXImpl.logPixels;
				}
			}

			// Token: 0x06006410 RID: 25616 RVA: 0x001721D2 File Offset: 0x001703D2
			internal int Advise(IAdviseSink pAdvSink)
			{
				this.adviseList.Add(pAdvSink);
				return this.adviseList.Count;
			}

			// Token: 0x06006411 RID: 25617 RVA: 0x001721EC File Offset: 0x001703EC
			internal void Close(int dwSaveOption)
			{
				if (this.activeXState[Control.ActiveXImpl.inPlaceActive])
				{
					this.InPlaceDeactivate();
				}
				if ((dwSaveOption == 0 || dwSaveOption == 2) && this.activeXState[Control.ActiveXImpl.isDirty])
				{
					if (this.clientSite != null)
					{
						this.clientSite.SaveObject();
					}
					this.SendOnSave();
				}
			}

			// Token: 0x06006412 RID: 25618 RVA: 0x00172244 File Offset: 0x00170444
			internal void DoVerb(int iVerb, IntPtr lpmsg, UnsafeNativeMethods.IOleClientSite pActiveSite, int lindex, IntPtr hwndParent, NativeMethods.COMRECT lprcPosRect)
			{
				switch (iVerb)
				{
				case -5:
				case -4:
				case -1:
				case 0:
				{
					this.InPlaceActivate(iVerb);
					if (!(lpmsg != IntPtr.Zero))
					{
						return;
					}
					NativeMethods.MSG msg = (NativeMethods.MSG)UnsafeNativeMethods.PtrToStructure(lpmsg, typeof(NativeMethods.MSG));
					Control control = this.control;
					if (msg.hwnd != this.control.Handle && msg.message >= 512 && msg.message <= 522)
					{
						IntPtr intPtr = ((msg.hwnd == IntPtr.Zero) ? hwndParent : msg.hwnd);
						NativeMethods.POINT point = new NativeMethods.POINT();
						point.x = NativeMethods.Util.LOWORD(msg.lParam);
						point.y = NativeMethods.Util.HIWORD(msg.lParam);
						UnsafeNativeMethods.MapWindowPoints(new HandleRef(null, intPtr), new HandleRef(this.control, this.control.Handle), point, 1);
						Control childAtPoint = control.GetChildAtPoint(new Point(point.x, point.y));
						if (childAtPoint != null && childAtPoint != control)
						{
							UnsafeNativeMethods.MapWindowPoints(new HandleRef(control, control.Handle), new HandleRef(childAtPoint, childAtPoint.Handle), point, 1);
							control = childAtPoint;
						}
						msg.lParam = NativeMethods.Util.MAKELPARAM(point.x, point.y);
					}
					if (msg.message == 256 && msg.wParam == (IntPtr)9)
					{
						control.SelectNextControl(null, Control.ModifierKeys != Keys.Shift, true, true, true);
						return;
					}
					control.SendMessage(msg.message, msg.wParam, msg.lParam);
					return;
				}
				case -3:
					this.UIDeactivate();
					this.InPlaceDeactivate();
					if (this.activeXState[Control.ActiveXImpl.inPlaceVisible])
					{
						this.SetInPlaceVisible(false);
						return;
					}
					return;
				}
				Control.ActiveXImpl.ThrowHr(-2147467263);
			}

			// Token: 0x06006413 RID: 25619 RVA: 0x00172438 File Offset: 0x00170638
			internal void Draw(int dwDrawAspect, int lindex, IntPtr pvAspect, NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, NativeMethods.COMRECT prcBounds, NativeMethods.COMRECT lprcWBounds, IntPtr pfnContinue, int dwContinue)
			{
				if (dwDrawAspect != 1 && dwDrawAspect != 16 && dwDrawAspect != 32)
				{
					Control.ActiveXImpl.ThrowHr(-2147221397);
				}
				int objectType = UnsafeNativeMethods.GetObjectType(new HandleRef(null, hdcDraw));
				if (objectType == 4)
				{
					Control.ActiveXImpl.ThrowHr(-2147221184);
				}
				NativeMethods.POINT point = new NativeMethods.POINT();
				NativeMethods.POINT point2 = new NativeMethods.POINT();
				NativeMethods.SIZE size = new NativeMethods.SIZE();
				NativeMethods.SIZE size2 = new NativeMethods.SIZE();
				int num = 1;
				if (!this.control.IsHandleCreated)
				{
					this.control.CreateHandle();
				}
				if (prcBounds != null)
				{
					NativeMethods.RECT rect = new NativeMethods.RECT(prcBounds.left, prcBounds.top, prcBounds.right, prcBounds.bottom);
					SafeNativeMethods.LPtoDP(new HandleRef(null, hdcDraw), ref rect, 2);
					num = SafeNativeMethods.SetMapMode(new HandleRef(null, hdcDraw), 8);
					SafeNativeMethods.SetWindowOrgEx(new HandleRef(null, hdcDraw), 0, 0, point2);
					SafeNativeMethods.SetWindowExtEx(new HandleRef(null, hdcDraw), this.control.Width, this.control.Height, size);
					SafeNativeMethods.SetViewportOrgEx(new HandleRef(null, hdcDraw), rect.left, rect.top, point);
					SafeNativeMethods.SetViewportExtEx(new HandleRef(null, hdcDraw), rect.right - rect.left, rect.bottom - rect.top, size2);
				}
				try
				{
					IntPtr intPtr = (IntPtr)30;
					if (objectType != 12)
					{
						this.control.SendMessage(791, hdcDraw, intPtr);
					}
					else
					{
						this.control.PrintToMetaFile(new HandleRef(null, hdcDraw), intPtr);
					}
				}
				finally
				{
					if (prcBounds != null)
					{
						SafeNativeMethods.SetWindowOrgEx(new HandleRef(null, hdcDraw), point2.x, point2.y, null);
						SafeNativeMethods.SetWindowExtEx(new HandleRef(null, hdcDraw), size.cx, size.cy, null);
						SafeNativeMethods.SetViewportOrgEx(new HandleRef(null, hdcDraw), point.x, point.y, null);
						SafeNativeMethods.SetViewportExtEx(new HandleRef(null, hdcDraw), size2.cx, size2.cy, null);
						SafeNativeMethods.SetMapMode(new HandleRef(null, hdcDraw), num);
					}
				}
			}

			// Token: 0x06006414 RID: 25620 RVA: 0x00172650 File Offset: 0x00170850
			internal static int EnumVerbs(out UnsafeNativeMethods.IEnumOLEVERB e)
			{
				if (Control.ActiveXImpl.axVerbs == null)
				{
					NativeMethods.tagOLEVERB tagOLEVERB = new NativeMethods.tagOLEVERB();
					NativeMethods.tagOLEVERB tagOLEVERB2 = new NativeMethods.tagOLEVERB();
					NativeMethods.tagOLEVERB tagOLEVERB3 = new NativeMethods.tagOLEVERB();
					NativeMethods.tagOLEVERB tagOLEVERB4 = new NativeMethods.tagOLEVERB();
					NativeMethods.tagOLEVERB tagOLEVERB5 = new NativeMethods.tagOLEVERB();
					NativeMethods.tagOLEVERB tagOLEVERB6 = new NativeMethods.tagOLEVERB();
					tagOLEVERB.lVerb = -1;
					tagOLEVERB2.lVerb = -5;
					tagOLEVERB3.lVerb = -4;
					tagOLEVERB4.lVerb = -3;
					tagOLEVERB5.lVerb = 0;
					tagOLEVERB6.lVerb = -7;
					tagOLEVERB6.lpszVerbName = SR.GetString("AXProperties");
					tagOLEVERB6.grfAttribs = 2;
					Control.ActiveXImpl.axVerbs = new NativeMethods.tagOLEVERB[] { tagOLEVERB, tagOLEVERB2, tagOLEVERB3, tagOLEVERB4, tagOLEVERB5 };
				}
				e = new Control.ActiveXVerbEnum(Control.ActiveXImpl.axVerbs);
				return 0;
			}

			// Token: 0x06006415 RID: 25621 RVA: 0x00172704 File Offset: 0x00170904
			private static byte[] FromBase64WrappedString(string text)
			{
				if (text.IndexOfAny(new char[] { ' ', '\r', '\n' }) != -1)
				{
					StringBuilder stringBuilder = new StringBuilder(text.Length);
					for (int i = 0; i < text.Length; i++)
					{
						char c = text[i];
						if (c != '\n' && c != '\r' && c != ' ')
						{
							stringBuilder.Append(text[i]);
						}
					}
					return Convert.FromBase64String(stringBuilder.ToString());
				}
				return Convert.FromBase64String(text);
			}

			// Token: 0x06006416 RID: 25622 RVA: 0x00172780 File Offset: 0x00170980
			internal void GetAdvise(int[] paspects, int[] padvf, IAdviseSink[] pAdvSink)
			{
				if (paspects != null)
				{
					paspects[0] = 1;
				}
				if (padvf != null)
				{
					padvf[0] = 0;
					if (this.activeXState[Control.ActiveXImpl.viewAdviseOnlyOnce])
					{
						padvf[0] |= 4;
					}
					if (this.activeXState[Control.ActiveXImpl.viewAdvisePrimeFirst])
					{
						padvf[0] |= 2;
					}
				}
				if (pAdvSink != null)
				{
					pAdvSink[0] = this.viewAdviseSink;
				}
			}

			// Token: 0x06006417 RID: 25623 RVA: 0x001727E4 File Offset: 0x001709E4
			private bool GetAmbientProperty(int dispid, ref object obj)
			{
				if (this.clientSite is UnsafeNativeMethods.IDispatch)
				{
					UnsafeNativeMethods.IDispatch dispatch = (UnsafeNativeMethods.IDispatch)this.clientSite;
					object[] array = new object[1];
					Guid empty = Guid.Empty;
					int num = -2147467259;
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						num = dispatch.Invoke(dispid, ref empty, NativeMethods.LOCALE_USER_DEFAULT, 2, new NativeMethods.tagDISPPARAMS(), array, null, null);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					if (NativeMethods.Succeeded(num))
					{
						obj = array[0];
						return true;
					}
				}
				return false;
			}

			// Token: 0x06006418 RID: 25624 RVA: 0x00172868 File Offset: 0x00170A68
			internal UnsafeNativeMethods.IOleClientSite GetClientSite()
			{
				return this.clientSite;
			}

			// Token: 0x06006419 RID: 25625 RVA: 0x00172870 File Offset: 0x00170A70
			[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
			internal int GetControlInfo(NativeMethods.tagCONTROLINFO pCI)
			{
				if (this.accelCount == -1)
				{
					ArrayList arrayList = new ArrayList();
					this.GetMnemonicList(this.control, arrayList);
					this.accelCount = (short)arrayList.Count;
					if (this.accelCount > 0)
					{
						int num = UnsafeNativeMethods.SizeOf(typeof(NativeMethods.ACCEL));
						IntPtr intPtr = Marshal.AllocHGlobal(num * (int)this.accelCount * 2);
						try
						{
							NativeMethods.ACCEL accel = new NativeMethods.ACCEL();
							accel.cmd = 0;
							this.accelCount = 0;
							foreach (object obj in arrayList)
							{
								char c = (char)obj;
								IntPtr intPtr2 = (IntPtr)((long)intPtr + (long)((int)this.accelCount * num));
								if (c >= 'A' && c <= 'Z')
								{
									accel.fVirt = 17;
									accel.key = UnsafeNativeMethods.VkKeyScan(c) & 255;
									Marshal.StructureToPtr(accel, intPtr2, false);
									this.accelCount += 1;
									intPtr2 = (IntPtr)((long)intPtr2 + (long)num);
									accel.fVirt = 21;
									Marshal.StructureToPtr(accel, intPtr2, false);
								}
								else
								{
									accel.fVirt = 17;
									short num2 = UnsafeNativeMethods.VkKeyScan(c);
									if ((num2 & 256) != 0)
									{
										NativeMethods.ACCEL accel2 = accel;
										accel2.fVirt |= 4;
									}
									accel.key = num2 & 255;
									Marshal.StructureToPtr(accel, intPtr2, false);
								}
								NativeMethods.ACCEL accel3 = accel;
								accel3.cmd += 1;
								this.accelCount += 1;
							}
							if (this.accelTable != IntPtr.Zero)
							{
								UnsafeNativeMethods.DestroyAcceleratorTable(new HandleRef(this, this.accelTable));
								this.accelTable = IntPtr.Zero;
							}
							this.accelTable = UnsafeNativeMethods.CreateAcceleratorTable(new HandleRef(null, intPtr), (int)this.accelCount);
						}
						finally
						{
							if (intPtr != IntPtr.Zero)
							{
								Marshal.FreeHGlobal(intPtr);
							}
						}
					}
				}
				pCI.cAccel = this.accelCount;
				pCI.hAccel = this.accelTable;
				return 0;
			}

			// Token: 0x0600641A RID: 25626 RVA: 0x00172AAC File Offset: 0x00170CAC
			internal void GetExtent(int dwDrawAspect, NativeMethods.tagSIZEL pSizel)
			{
				if ((dwDrawAspect & 1) != 0)
				{
					Size size = this.control.Size;
					Point point = this.PixelToHiMetric(size.Width, size.Height);
					pSizel.cx = point.X;
					pSizel.cy = point.Y;
					return;
				}
				Control.ActiveXImpl.ThrowHr(-2147221397);
			}

			// Token: 0x0600641B RID: 25627 RVA: 0x00172B04 File Offset: 0x00170D04
			private void GetMnemonicList(Control control, ArrayList mnemonicList)
			{
				char mnemonic = WindowsFormsUtils.GetMnemonic(control.Text, true);
				if (mnemonic != '\0')
				{
					mnemonicList.Add(mnemonic);
				}
				foreach (object obj in control.Controls)
				{
					Control control2 = (Control)obj;
					if (control2 != null)
					{
						this.GetMnemonicList(control2, mnemonicList);
					}
				}
			}

			// Token: 0x0600641C RID: 25628 RVA: 0x00172B80 File Offset: 0x00170D80
			private string GetStreamName()
			{
				string text = this.control.GetType().FullName;
				int length = text.Length;
				if (length > 31)
				{
					text = text.Substring(length - 31);
				}
				return text;
			}

			// Token: 0x0600641D RID: 25629 RVA: 0x00172BB6 File Offset: 0x00170DB6
			internal int GetWindow(out IntPtr hwnd)
			{
				if (!this.activeXState[Control.ActiveXImpl.inPlaceActive])
				{
					hwnd = IntPtr.Zero;
					return -2147467259;
				}
				hwnd = this.control.Handle;
				return 0;
			}

			// Token: 0x0600641E RID: 25630 RVA: 0x00172BE8 File Offset: 0x00170DE8
			private Point HiMetricToPixel(int x, int y)
			{
				return new Point
				{
					X = (this.LogPixels.X * x + Control.ActiveXImpl.hiMetricPerInch / 2) / Control.ActiveXImpl.hiMetricPerInch,
					Y = (this.LogPixels.Y * y + Control.ActiveXImpl.hiMetricPerInch / 2) / Control.ActiveXImpl.hiMetricPerInch
				};
			}

			// Token: 0x0600641F RID: 25631 RVA: 0x00172C48 File Offset: 0x00170E48
			[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
			internal void InPlaceActivate(int verb)
			{
				UnsafeNativeMethods.IOleInPlaceSite oleInPlaceSite = this.clientSite as UnsafeNativeMethods.IOleInPlaceSite;
				if (oleInPlaceSite == null)
				{
					return;
				}
				if (!this.activeXState[Control.ActiveXImpl.inPlaceActive])
				{
					int num = oleInPlaceSite.CanInPlaceActivate();
					if (num != 0)
					{
						if (NativeMethods.Succeeded(num))
						{
							num = -2147467259;
						}
						Control.ActiveXImpl.ThrowHr(num);
					}
					oleInPlaceSite.OnInPlaceActivate();
					this.activeXState[Control.ActiveXImpl.inPlaceActive] = true;
				}
				if (!this.activeXState[Control.ActiveXImpl.inPlaceVisible])
				{
					NativeMethods.tagOIFI tagOIFI = new NativeMethods.tagOIFI();
					tagOIFI.cb = UnsafeNativeMethods.SizeOf(typeof(NativeMethods.tagOIFI));
					IntPtr intPtr = IntPtr.Zero;
					intPtr = oleInPlaceSite.GetWindow();
					NativeMethods.COMRECT comrect = new NativeMethods.COMRECT();
					NativeMethods.COMRECT comrect2 = new NativeMethods.COMRECT();
					if (this.inPlaceUiWindow != null && UnsafeNativeMethods.IsComObject(this.inPlaceUiWindow))
					{
						UnsafeNativeMethods.ReleaseComObject(this.inPlaceUiWindow);
						this.inPlaceUiWindow = null;
					}
					if (this.inPlaceFrame != null && UnsafeNativeMethods.IsComObject(this.inPlaceFrame))
					{
						UnsafeNativeMethods.ReleaseComObject(this.inPlaceFrame);
						this.inPlaceFrame = null;
					}
					UnsafeNativeMethods.IOleInPlaceFrame oleInPlaceFrame;
					UnsafeNativeMethods.IOleInPlaceUIWindow oleInPlaceUIWindow;
					oleInPlaceSite.GetWindowContext(out oleInPlaceFrame, out oleInPlaceUIWindow, comrect, comrect2, tagOIFI);
					this.SetObjectRects(comrect, comrect2);
					this.inPlaceFrame = oleInPlaceFrame;
					this.inPlaceUiWindow = oleInPlaceUIWindow;
					this.hwndParent = intPtr;
					UnsafeNativeMethods.SetParent(new HandleRef(this.control, this.control.Handle), new HandleRef(null, intPtr));
					this.control.CreateControl();
					this.clientSite.ShowObject();
					this.SetInPlaceVisible(true);
				}
				if (verb != 0 && verb != -4)
				{
					return;
				}
				if (!this.activeXState[Control.ActiveXImpl.uiActive])
				{
					this.activeXState[Control.ActiveXImpl.uiActive] = true;
					oleInPlaceSite.OnUIActivate();
					if (!this.control.ContainsFocus)
					{
						this.control.FocusInternal();
					}
					this.inPlaceFrame.SetActiveObject(this.control, null);
					if (this.inPlaceUiWindow != null)
					{
						this.inPlaceUiWindow.SetActiveObject(this.control, null);
					}
					int num2 = this.inPlaceFrame.SetBorderSpace(null);
					if (NativeMethods.Failed(num2) && num2 != -2147221491 && num2 != -2147221087 && num2 != -2147467263)
					{
						UnsafeNativeMethods.ThrowExceptionForHR(num2);
					}
					if (this.inPlaceUiWindow != null)
					{
						num2 = this.inPlaceFrame.SetBorderSpace(null);
						if (NativeMethods.Failed(num2) && num2 != -2147221491 && num2 != -2147221087 && num2 != -2147467263)
						{
							UnsafeNativeMethods.ThrowExceptionForHR(num2);
						}
					}
				}
			}

			// Token: 0x06006420 RID: 25632 RVA: 0x00172EB0 File Offset: 0x001710B0
			internal void InPlaceDeactivate()
			{
				if (!this.activeXState[Control.ActiveXImpl.inPlaceActive])
				{
					return;
				}
				if (this.activeXState[Control.ActiveXImpl.uiActive])
				{
					this.UIDeactivate();
				}
				this.activeXState[Control.ActiveXImpl.inPlaceActive] = false;
				this.activeXState[Control.ActiveXImpl.inPlaceVisible] = false;
				UnsafeNativeMethods.IOleInPlaceSite oleInPlaceSite = this.clientSite as UnsafeNativeMethods.IOleInPlaceSite;
				if (oleInPlaceSite != null)
				{
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						oleInPlaceSite.OnInPlaceDeactivate();
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				this.control.Visible = false;
				this.hwndParent = IntPtr.Zero;
				if (this.inPlaceUiWindow != null && UnsafeNativeMethods.IsComObject(this.inPlaceUiWindow))
				{
					UnsafeNativeMethods.ReleaseComObject(this.inPlaceUiWindow);
					this.inPlaceUiWindow = null;
				}
				if (this.inPlaceFrame != null && UnsafeNativeMethods.IsComObject(this.inPlaceFrame))
				{
					UnsafeNativeMethods.ReleaseComObject(this.inPlaceFrame);
					this.inPlaceFrame = null;
				}
			}

			// Token: 0x06006421 RID: 25633 RVA: 0x00172FAC File Offset: 0x001711AC
			internal int IsDirty()
			{
				if (this.activeXState[Control.ActiveXImpl.isDirty])
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06006422 RID: 25634 RVA: 0x00172FC4 File Offset: 0x001711C4
			private bool IsResourceProp(PropertyDescriptor prop)
			{
				TypeConverter converter = prop.Converter;
				Type[] array = new Type[]
				{
					typeof(string),
					typeof(byte[])
				};
				foreach (Type type in array)
				{
					if (converter.CanConvertTo(type) && converter.CanConvertFrom(type))
					{
						return false;
					}
				}
				return prop.GetValue(this.control) is ISerializable;
			}

			// Token: 0x06006423 RID: 25635 RVA: 0x00173038 File Offset: 0x00171238
			internal void Load(UnsafeNativeMethods.IStorage stg)
			{
				UnsafeNativeMethods.IStream stream = null;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					stream = stg.OpenStream(this.GetStreamName(), IntPtr.Zero, 16, 0);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode != -2147287038)
					{
						throw;
					}
					stream = stg.OpenStream(base.GetType().FullName, IntPtr.Zero, 16, 0);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				this.Load(stream);
				stream = null;
				if (UnsafeNativeMethods.IsComObject(stg))
				{
					UnsafeNativeMethods.ReleaseComObject(stg);
				}
			}

			// Token: 0x06006424 RID: 25636 RVA: 0x001730D0 File Offset: 0x001712D0
			internal void Load(UnsafeNativeMethods.IStream stream)
			{
				Control.ActiveXImpl.PropertyBagStream propertyBagStream = new Control.ActiveXImpl.PropertyBagStream();
				propertyBagStream.Read(stream);
				this.Load(propertyBagStream, null);
				if (UnsafeNativeMethods.IsComObject(stream))
				{
					UnsafeNativeMethods.ReleaseComObject(stream);
				}
			}

			// Token: 0x06006425 RID: 25637 RVA: 0x00173104 File Offset: 0x00171304
			internal void Load(UnsafeNativeMethods.IPropertyBag pPropBag, UnsafeNativeMethods.IErrorLog pErrorLog)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.control, new Attribute[] { DesignerSerializationVisibilityAttribute.Visible });
				for (int i = 0; i < properties.Count; i++)
				{
					try
					{
						object obj = null;
						int num = -2147467259;
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							num = pPropBag.Read(properties[i].Name, ref obj, pErrorLog);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
						if (NativeMethods.Succeeded(num) && obj != null)
						{
							string text = null;
							int num2 = 0;
							try
							{
								if (obj.GetType() != typeof(string))
								{
									obj = Convert.ToString(obj, CultureInfo.InvariantCulture);
								}
								if (this.IsResourceProp(properties[i]))
								{
									byte[] array = Convert.FromBase64String(obj.ToString());
									MemoryStream memoryStream = new MemoryStream(array);
									BinaryFormatter binaryFormatter = new BinaryFormatter();
									properties[i].SetValue(this.control, binaryFormatter.Deserialize(memoryStream));
								}
								else
								{
									TypeConverter converter = properties[i].Converter;
									object obj2 = null;
									if (converter.CanConvertFrom(typeof(string)))
									{
										obj2 = converter.ConvertFromInvariantString(obj.ToString());
									}
									else if (converter.CanConvertFrom(typeof(byte[])))
									{
										string text2 = obj.ToString();
										obj2 = converter.ConvertFrom(null, CultureInfo.InvariantCulture, Control.ActiveXImpl.FromBase64WrappedString(text2));
									}
									properties[i].SetValue(this.control, obj2);
								}
							}
							catch (Exception ex)
							{
								text = ex.ToString();
								if (ex is ExternalException)
								{
									num2 = ((ExternalException)ex).ErrorCode;
								}
								else
								{
									num2 = -2147467259;
								}
							}
							if (text != null && pErrorLog != null)
							{
								NativeMethods.tagEXCEPINFO tagEXCEPINFO = new NativeMethods.tagEXCEPINFO();
								tagEXCEPINFO.bstrSource = this.control.GetType().FullName;
								tagEXCEPINFO.bstrDescription = text;
								tagEXCEPINFO.scode = num2;
								pErrorLog.AddError(properties[i].Name, tagEXCEPINFO);
							}
						}
					}
					catch (Exception ex2)
					{
						if (ClientUtils.IsSecurityOrCriticalException(ex2))
						{
							throw;
						}
					}
				}
				if (UnsafeNativeMethods.IsComObject(pPropBag))
				{
					UnsafeNativeMethods.ReleaseComObject(pPropBag);
				}
			}

			// Token: 0x06006426 RID: 25638 RVA: 0x00173358 File Offset: 0x00171558
			private Control.AmbientProperty LookupAmbient(int dispid)
			{
				for (int i = 0; i < this.ambientProperties.Length; i++)
				{
					if (this.ambientProperties[i].DispID == dispid)
					{
						return this.ambientProperties[i];
					}
				}
				return this.ambientProperties[0];
			}

			// Token: 0x06006427 RID: 25639 RVA: 0x0017339C File Offset: 0x0017159C
			internal IntPtr MergeRegion(IntPtr region)
			{
				if (this.clipRegion == IntPtr.Zero)
				{
					return region;
				}
				if (region == IntPtr.Zero)
				{
					return this.clipRegion;
				}
				IntPtr intPtr2;
				try
				{
					IntPtr intPtr = SafeNativeMethods.CreateRectRgn(0, 0, 0, 0);
					try
					{
						SafeNativeMethods.CombineRgn(new HandleRef(null, intPtr), new HandleRef(null, region), new HandleRef(this, this.clipRegion), 4);
						SafeNativeMethods.DeleteObject(new HandleRef(null, region));
					}
					catch
					{
						SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
						throw;
					}
					intPtr2 = intPtr;
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
					intPtr2 = region;
				}
				return intPtr2;
			}

			// Token: 0x06006428 RID: 25640 RVA: 0x0017344C File Offset: 0x0017164C
			private void CallParentPropertyChanged(Control control, string propName)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(propName);
				if (num <= 2626085950U)
				{
					if (num <= 777198197U)
					{
						if (num != 41545325U)
						{
							if (num != 777198197U)
							{
								return;
							}
							if (!(propName == "BackColor"))
							{
								return;
							}
							control.OnParentBackColorChanged(EventArgs.Empty);
							return;
						}
						else
						{
							if (!(propName == "BindingContext"))
							{
								return;
							}
							control.OnParentBindingContextChanged(EventArgs.Empty);
							return;
						}
					}
					else if (num != 1495943489U)
					{
						if (num != 2626085950U)
						{
							return;
						}
						if (!(propName == "Enabled"))
						{
							return;
						}
						control.OnParentEnabledChanged(EventArgs.Empty);
						return;
					}
					else
					{
						if (!(propName == "Visible"))
						{
							return;
						}
						control.OnParentVisibleChanged(EventArgs.Empty);
						return;
					}
				}
				else if (num <= 2936102910U)
				{
					if (num != 2809814704U)
					{
						if (num != 2936102910U)
						{
							return;
						}
						if (!(propName == "ForeColor"))
						{
							return;
						}
						control.OnParentForeColorChanged(EventArgs.Empty);
						return;
					}
					else
					{
						if (!(propName == "Font"))
						{
							return;
						}
						control.OnParentFontChanged(EventArgs.Empty);
						return;
					}
				}
				else if (num != 3049818181U)
				{
					if (num != 3770400898U)
					{
						return;
					}
					if (!(propName == "BackgroundImage"))
					{
						return;
					}
					control.OnParentBackgroundImageChanged(EventArgs.Empty);
					return;
				}
				else
				{
					if (!(propName == "RightToLeft"))
					{
						return;
					}
					control.OnParentRightToLeftChanged(EventArgs.Empty);
					return;
				}
			}

			// Token: 0x06006429 RID: 25641 RVA: 0x00173590 File Offset: 0x00171790
			internal void OnAmbientPropertyChange(int dispID)
			{
				if (dispID != -1)
				{
					for (int i = 0; i < this.ambientProperties.Length; i++)
					{
						if (this.ambientProperties[i].DispID == dispID)
						{
							this.ambientProperties[i].ResetValue();
							this.CallParentPropertyChanged(this.control, this.ambientProperties[i].Name);
							return;
						}
					}
					object obj = new object();
					if (dispID != -713)
					{
						if (dispID == -710 && this.GetAmbientProperty(-710, ref obj))
						{
							this.activeXState[Control.ActiveXImpl.uiDead] = (bool)obj;
							return;
						}
					}
					else
					{
						IButtonControl buttonControl = this.control as IButtonControl;
						if (buttonControl != null && this.GetAmbientProperty(-713, ref obj))
						{
							buttonControl.NotifyDefault((bool)obj);
							return;
						}
					}
				}
				else
				{
					for (int j = 0; j < this.ambientProperties.Length; j++)
					{
						this.ambientProperties[j].ResetValue();
						this.CallParentPropertyChanged(this.control, this.ambientProperties[j].Name);
					}
				}
			}

			// Token: 0x0600642A RID: 25642 RVA: 0x00173694 File Offset: 0x00171894
			internal void OnDocWindowActivate(int fActivate)
			{
				if (this.activeXState[Control.ActiveXImpl.uiActive] && fActivate != 0 && this.inPlaceFrame != null)
				{
					IntSecurity.UnmanagedCode.Assert();
					int num;
					try
					{
						num = this.inPlaceFrame.SetBorderSpace(null);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					if (NativeMethods.Failed(num) && num != -2147221087 && num != -2147467263)
					{
						UnsafeNativeMethods.ThrowExceptionForHR(num);
					}
				}
			}

			// Token: 0x0600642B RID: 25643 RVA: 0x0017370C File Offset: 0x0017190C
			internal void OnFocus(bool focus)
			{
				if (this.activeXState[Control.ActiveXImpl.inPlaceActive] && this.clientSite is UnsafeNativeMethods.IOleControlSite)
				{
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						((UnsafeNativeMethods.IOleControlSite)this.clientSite).OnFocus(focus ? 1 : 0);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				if (focus && this.activeXState[Control.ActiveXImpl.inPlaceActive] && !this.activeXState[Control.ActiveXImpl.uiActive])
				{
					this.InPlaceActivate(-4);
				}
			}

			// Token: 0x0600642C RID: 25644 RVA: 0x001737A4 File Offset: 0x001719A4
			private Point PixelToHiMetric(int x, int y)
			{
				return new Point
				{
					X = (Control.ActiveXImpl.hiMetricPerInch * x + (this.LogPixels.X >> 1)) / this.LogPixels.X,
					Y = (Control.ActiveXImpl.hiMetricPerInch * y + (this.LogPixels.Y >> 1)) / this.LogPixels.Y
				};
			}

			// Token: 0x0600642D RID: 25645 RVA: 0x00173818 File Offset: 0x00171A18
			internal void QuickActivate(UnsafeNativeMethods.tagQACONTAINER pQaContainer, UnsafeNativeMethods.tagQACONTROL pQaControl)
			{
				Control.AmbientProperty ambientProperty = this.LookupAmbient(-701);
				ambientProperty.Value = ColorTranslator.FromOle((int)pQaContainer.colorBack);
				ambientProperty = this.LookupAmbient(-704);
				ambientProperty.Value = ColorTranslator.FromOle((int)pQaContainer.colorFore);
				if (pQaContainer.pFont != null)
				{
					ambientProperty = this.LookupAmbient(-703);
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						IntPtr intPtr = IntPtr.Zero;
						object pFont = pQaContainer.pFont;
						UnsafeNativeMethods.IFont font = (UnsafeNativeMethods.IFont)pFont;
						intPtr = font.GetHFont();
						Font font2 = Font.FromHfont(intPtr);
						ambientProperty.Value = font2;
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsSecurityOrCriticalException(ex))
						{
							throw;
						}
						ambientProperty.Value = null;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				pQaControl.cbSize = UnsafeNativeMethods.SizeOf(typeof(UnsafeNativeMethods.tagQACONTROL));
				this.SetClientSite(pQaContainer.pClientSite);
				if (pQaContainer.pAdviseSink != null)
				{
					this.SetAdvise(1, 0, (IAdviseSink)pQaContainer.pAdviseSink);
				}
				IntSecurity.UnmanagedCode.Assert();
				int num;
				try
				{
					((UnsafeNativeMethods.IOleObject)this.control).GetMiscStatus(1, out num);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				pQaControl.dwMiscStatus = num;
				if (pQaContainer.pUnkEventSink != null && this.control is UserControl)
				{
					Type defaultEventsInterface = Control.ActiveXImpl.GetDefaultEventsInterface(this.control.GetType());
					if (defaultEventsInterface != null)
					{
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							Control.ActiveXImpl.AdviseHelper.AdviseConnectionPoint(this.control, pQaContainer.pUnkEventSink, defaultEventsInterface, out pQaControl.dwEventCookie);
						}
						catch (Exception ex2)
						{
							if (ClientUtils.IsSecurityOrCriticalException(ex2))
							{
								throw;
							}
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
				}
				if (pQaContainer.pPropertyNotifySink != null && UnsafeNativeMethods.IsComObject(pQaContainer.pPropertyNotifySink))
				{
					UnsafeNativeMethods.ReleaseComObject(pQaContainer.pPropertyNotifySink);
				}
				if (pQaContainer.pUnkEventSink != null && UnsafeNativeMethods.IsComObject(pQaContainer.pUnkEventSink))
				{
					UnsafeNativeMethods.ReleaseComObject(pQaContainer.pUnkEventSink);
				}
			}

			// Token: 0x0600642E RID: 25646 RVA: 0x00173A24 File Offset: 0x00171C24
			private static Type GetDefaultEventsInterface(Type controlType)
			{
				Type type = null;
				object[] customAttributes = controlType.GetCustomAttributes(typeof(ComSourceInterfacesAttribute), false);
				if (customAttributes.Length != 0)
				{
					ComSourceInterfacesAttribute comSourceInterfacesAttribute = (ComSourceInterfacesAttribute)customAttributes[0];
					string text = comSourceInterfacesAttribute.Value.Split(new char[1])[0];
					type = controlType.Module.Assembly.GetType(text, false);
					if (type == null)
					{
						type = Type.GetType(text, false);
					}
				}
				return type;
			}

			// Token: 0x0600642F RID: 25647 RVA: 0x00173A8C File Offset: 0x00171C8C
			internal void Save(UnsafeNativeMethods.IStorage stg, bool fSameAsLoad)
			{
				UnsafeNativeMethods.IStream stream = null;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					stream = stg.CreateStream(this.GetStreamName(), 4113, 0, 0);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				this.Save(stream, true);
				UnsafeNativeMethods.ReleaseComObject(stream);
			}

			// Token: 0x06006430 RID: 25648 RVA: 0x00173AE0 File Offset: 0x00171CE0
			internal void Save(UnsafeNativeMethods.IStream stream, bool fClearDirty)
			{
				Control.ActiveXImpl.PropertyBagStream propertyBagStream = new Control.ActiveXImpl.PropertyBagStream();
				this.Save(propertyBagStream, fClearDirty, false);
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					propertyBagStream.Write(stream);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (UnsafeNativeMethods.IsComObject(stream))
				{
					UnsafeNativeMethods.ReleaseComObject(stream);
				}
			}

			// Token: 0x06006431 RID: 25649 RVA: 0x00173B34 File Offset: 0x00171D34
			[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
			internal void Save(UnsafeNativeMethods.IPropertyBag pPropBag, bool fClearDirty, bool fSaveAllProperties)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.control, new Attribute[] { DesignerSerializationVisibilityAttribute.Visible });
				for (int i = 0; i < properties.Count; i++)
				{
					if (fSaveAllProperties || properties[i].ShouldSerializeValue(this.control))
					{
						if (this.IsResourceProp(properties[i]))
						{
							MemoryStream memoryStream = new MemoryStream();
							BinaryFormatter binaryFormatter = new BinaryFormatter();
							binaryFormatter.Serialize(memoryStream, properties[i].GetValue(this.control));
							byte[] array = new byte[(int)memoryStream.Length];
							memoryStream.Position = 0L;
							memoryStream.Read(array, 0, array.Length);
							object obj = Convert.ToBase64String(array);
							pPropBag.Write(properties[i].Name, ref obj);
						}
						else
						{
							TypeConverter converter = properties[i].Converter;
							if (converter.CanConvertFrom(typeof(string)))
							{
								object obj = converter.ConvertToInvariantString(properties[i].GetValue(this.control));
								pPropBag.Write(properties[i].Name, ref obj);
							}
							else if (converter.CanConvertFrom(typeof(byte[])))
							{
								byte[] array2 = (byte[])converter.ConvertTo(null, CultureInfo.InvariantCulture, properties[i].GetValue(this.control), typeof(byte[]));
								object obj = Convert.ToBase64String(array2);
								pPropBag.Write(properties[i].Name, ref obj);
							}
						}
					}
				}
				if (UnsafeNativeMethods.IsComObject(pPropBag))
				{
					UnsafeNativeMethods.ReleaseComObject(pPropBag);
				}
				if (fClearDirty)
				{
					this.activeXState[Control.ActiveXImpl.isDirty] = false;
				}
			}

			// Token: 0x06006432 RID: 25650 RVA: 0x00173CE0 File Offset: 0x00171EE0
			private void SendOnSave()
			{
				int count = this.adviseList.Count;
				IntSecurity.UnmanagedCode.Assert();
				for (int i = 0; i < count; i++)
				{
					IAdviseSink adviseSink = (IAdviseSink)this.adviseList[i];
					adviseSink.OnSave();
				}
			}

			// Token: 0x06006433 RID: 25651 RVA: 0x00173D28 File Offset: 0x00171F28
			internal void SetAdvise(int aspects, int advf, IAdviseSink pAdvSink)
			{
				if ((aspects & 1) == 0)
				{
					Control.ActiveXImpl.ThrowHr(-2147221397);
				}
				this.activeXState[Control.ActiveXImpl.viewAdvisePrimeFirst] = (advf & 2) != 0;
				this.activeXState[Control.ActiveXImpl.viewAdviseOnlyOnce] = (advf & 4) != 0;
				if (this.viewAdviseSink != null && UnsafeNativeMethods.IsComObject(this.viewAdviseSink))
				{
					UnsafeNativeMethods.ReleaseComObject(this.viewAdviseSink);
				}
				this.viewAdviseSink = pAdvSink;
				if (this.activeXState[Control.ActiveXImpl.viewAdvisePrimeFirst])
				{
					this.ViewChanged();
				}
			}

			// Token: 0x06006434 RID: 25652 RVA: 0x00173DB8 File Offset: 0x00171FB8
			internal void SetClientSite(UnsafeNativeMethods.IOleClientSite value)
			{
				if (this.clientSite != null)
				{
					if (value == null)
					{
						Control.ActiveXImpl.globalActiveXCount--;
						if (Control.ActiveXImpl.globalActiveXCount == 0 && this.IsIE)
						{
							new PermissionSet(PermissionState.Unrestricted).Assert();
							try
							{
								MethodInfo method = typeof(SystemEvents).GetMethod("Shutdown", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, new Type[0], new ParameterModifier[0]);
								if (method != null)
								{
									method.Invoke(null, null);
								}
							}
							finally
							{
								CodeAccessPermission.RevertAssert();
							}
						}
					}
					if (UnsafeNativeMethods.IsComObject(this.clientSite))
					{
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							Marshal.FinalReleaseComObject(this.clientSite);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
				}
				this.clientSite = value;
				if (this.clientSite != null)
				{
					this.control.Site = new Control.AxSourcingSite(this.control, this.clientSite, "ControlAxSourcingSite");
				}
				else
				{
					this.control.Site = null;
				}
				object obj = new object();
				if (this.GetAmbientProperty(-710, ref obj))
				{
					this.activeXState[Control.ActiveXImpl.uiDead] = (bool)obj;
				}
				if (this.control is IButtonControl && this.GetAmbientProperty(-710, ref obj))
				{
					((IButtonControl)this.control).NotifyDefault((bool)obj);
				}
				if (this.clientSite == null)
				{
					if (this.accelTable != IntPtr.Zero)
					{
						UnsafeNativeMethods.DestroyAcceleratorTable(new HandleRef(this, this.accelTable));
						this.accelTable = IntPtr.Zero;
						this.accelCount = -1;
					}
					if (this.IsIE)
					{
						this.control.Dispose();
					}
				}
				else
				{
					Control.ActiveXImpl.globalActiveXCount++;
					if (Control.ActiveXImpl.globalActiveXCount == 1 && this.IsIE)
					{
						new PermissionSet(PermissionState.Unrestricted).Assert();
						try
						{
							MethodInfo method2 = typeof(SystemEvents).GetMethod("Startup", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, new Type[0], new ParameterModifier[0]);
							if (method2 != null)
							{
								method2.Invoke(null, null);
							}
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
				}
				this.control.OnTopMostActiveXParentChanged(EventArgs.Empty);
			}

			// Token: 0x06006435 RID: 25653 RVA: 0x00173FF4 File Offset: 0x001721F4
			[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
			internal void SetExtent(int dwDrawAspect, NativeMethods.tagSIZEL pSizel)
			{
				if ((dwDrawAspect & 1) != 0)
				{
					if (this.activeXState[Control.ActiveXImpl.changingExtents])
					{
						return;
					}
					this.activeXState[Control.ActiveXImpl.changingExtents] = true;
					try
					{
						Size size = new Size(this.HiMetricToPixel(pSizel.cx, pSizel.cy));
						if (this.activeXState[Control.ActiveXImpl.inPlaceActive])
						{
							UnsafeNativeMethods.IOleInPlaceSite oleInPlaceSite = this.clientSite as UnsafeNativeMethods.IOleInPlaceSite;
							if (oleInPlaceSite != null)
							{
								Rectangle bounds = this.control.Bounds;
								bounds.Location = new Point(bounds.X, bounds.Y);
								Size size2 = new Size(size.Width, size.Height);
								bounds.Width = size2.Width;
								bounds.Height = size2.Height;
								oleInPlaceSite.OnPosRectChange(NativeMethods.COMRECT.FromXYWH(bounds.X, bounds.Y, bounds.Width, bounds.Height));
							}
						}
						this.control.Size = size;
						if (!this.control.Size.Equals(size))
						{
							this.activeXState[Control.ActiveXImpl.isDirty] = true;
							if (!this.activeXState[Control.ActiveXImpl.inPlaceActive])
							{
								this.ViewChanged();
							}
							if (!this.activeXState[Control.ActiveXImpl.inPlaceActive] && this.clientSite != null)
							{
								this.clientSite.RequestNewObjectLayout();
							}
						}
						return;
					}
					finally
					{
						this.activeXState[Control.ActiveXImpl.changingExtents] = false;
					}
				}
				Control.ActiveXImpl.ThrowHr(-2147221397);
			}

			// Token: 0x06006436 RID: 25654 RVA: 0x001741A0 File Offset: 0x001723A0
			private void SetInPlaceVisible(bool visible)
			{
				this.activeXState[Control.ActiveXImpl.inPlaceVisible] = visible;
				this.control.Visible = visible;
			}

			// Token: 0x06006437 RID: 25655 RVA: 0x001741C0 File Offset: 0x001723C0
			internal void SetObjectRects(NativeMethods.COMRECT lprcPosRect, NativeMethods.COMRECT lprcClipRect)
			{
				Rectangle rectangle = Rectangle.FromLTRB(lprcPosRect.left, lprcPosRect.top, lprcPosRect.right, lprcPosRect.bottom);
				if (this.activeXState[Control.ActiveXImpl.adjustingRect])
				{
					this.adjustRect.left = rectangle.X;
					this.adjustRect.top = rectangle.Y;
					this.adjustRect.right = rectangle.Width + rectangle.X;
					this.adjustRect.bottom = rectangle.Height + rectangle.Y;
				}
				else
				{
					this.activeXState[Control.ActiveXImpl.adjustingRect] = true;
					try
					{
						this.control.Bounds = rectangle;
					}
					finally
					{
						this.activeXState[Control.ActiveXImpl.adjustingRect] = false;
					}
				}
				bool flag = false;
				if (this.clipRegion != IntPtr.Zero)
				{
					this.clipRegion = IntPtr.Zero;
					flag = true;
				}
				if (lprcClipRect != null)
				{
					Rectangle rectangle2 = Rectangle.FromLTRB(lprcClipRect.left, lprcClipRect.top, lprcClipRect.right, lprcClipRect.bottom);
					Rectangle rectangle3;
					if (!rectangle2.IsEmpty)
					{
						rectangle3 = Rectangle.Intersect(rectangle, rectangle2);
					}
					else
					{
						rectangle3 = rectangle;
					}
					if (!rectangle3.Equals(rectangle))
					{
						NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(rectangle3.X, rectangle3.Y, rectangle3.Width, rectangle3.Height);
						IntPtr parent = UnsafeNativeMethods.GetParent(new HandleRef(this.control, this.control.Handle));
						UnsafeNativeMethods.MapWindowPoints(new HandleRef(null, parent), new HandleRef(this.control, this.control.Handle), ref rect, 2);
						this.clipRegion = SafeNativeMethods.CreateRectRgn(rect.left, rect.top, rect.right, rect.bottom);
						flag = true;
					}
				}
				if (flag && this.control.IsHandleCreated)
				{
					IntPtr intPtr = this.clipRegion;
					Region region = this.control.Region;
					if (region != null)
					{
						IntPtr hrgn = this.control.GetHRgn(region);
						intPtr = this.MergeRegion(hrgn);
					}
					UnsafeNativeMethods.SetWindowRgn(new HandleRef(this.control, this.control.Handle), new HandleRef(this, intPtr), SafeNativeMethods.IsWindowVisible(new HandleRef(this.control, this.control.Handle)));
				}
				this.control.Invalidate();
			}

			// Token: 0x06006438 RID: 25656 RVA: 0x00174428 File Offset: 0x00172628
			internal static void ThrowHr(int hr)
			{
				ExternalException ex = new ExternalException(SR.GetString("ExternalException"), hr);
				throw ex;
			}

			// Token: 0x06006439 RID: 25657 RVA: 0x00174448 File Offset: 0x00172648
			internal int TranslateAccelerator(ref NativeMethods.MSG lpmsg)
			{
				bool flag = false;
				switch (lpmsg.message)
				{
				case 256:
				case 258:
				case 260:
				case 262:
					flag = true;
					break;
				}
				Message message = Message.Create(lpmsg.hwnd, lpmsg.message, lpmsg.wParam, lpmsg.lParam);
				if (flag)
				{
					Control control = Control.FromChildHandleInternal(lpmsg.hwnd);
					if (control != null && (this.control == control || this.control.Contains(control)))
					{
						switch (Control.PreProcessControlMessageInternal(control, ref message))
						{
						case PreProcessControlState.MessageProcessed:
							lpmsg.message = message.Msg;
							lpmsg.wParam = message.WParam;
							lpmsg.lParam = message.LParam;
							return 0;
						case PreProcessControlState.MessageNeeded:
							UnsafeNativeMethods.TranslateMessage(ref lpmsg);
							if (SafeNativeMethods.IsWindowUnicode(new HandleRef(null, lpmsg.hwnd)))
							{
								UnsafeNativeMethods.DispatchMessageW(ref lpmsg);
							}
							else
							{
								UnsafeNativeMethods.DispatchMessageA(ref lpmsg);
							}
							return 0;
						}
					}
				}
				int num = 1;
				UnsafeNativeMethods.IOleControlSite oleControlSite = this.clientSite as UnsafeNativeMethods.IOleControlSite;
				if (oleControlSite != null)
				{
					int num2 = 0;
					if (UnsafeNativeMethods.GetKeyState(16) < 0)
					{
						num2 |= 1;
					}
					if (UnsafeNativeMethods.GetKeyState(17) < 0)
					{
						num2 |= 2;
					}
					if (UnsafeNativeMethods.GetKeyState(18) < 0)
					{
						num2 |= 4;
					}
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						num = oleControlSite.TranslateAccelerator(ref lpmsg, num2);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				return num;
			}

			// Token: 0x0600643A RID: 25658 RVA: 0x001745C4 File Offset: 0x001727C4
			internal int UIDeactivate()
			{
				if (!this.activeXState[Control.ActiveXImpl.uiActive])
				{
					return 0;
				}
				this.activeXState[Control.ActiveXImpl.uiActive] = false;
				if (this.inPlaceUiWindow != null)
				{
					this.inPlaceUiWindow.SetActiveObject(null, null);
				}
				IntSecurity.UnmanagedCode.Assert();
				this.inPlaceFrame.SetActiveObject(null, null);
				UnsafeNativeMethods.IOleInPlaceSite oleInPlaceSite = this.clientSite as UnsafeNativeMethods.IOleInPlaceSite;
				if (oleInPlaceSite != null)
				{
					oleInPlaceSite.OnUIDeactivate(0);
				}
				return 0;
			}

			// Token: 0x0600643B RID: 25659 RVA: 0x0017463C File Offset: 0x0017283C
			internal void Unadvise(int dwConnection)
			{
				if (dwConnection > this.adviseList.Count || this.adviseList[dwConnection - 1] == null)
				{
					Control.ActiveXImpl.ThrowHr(-2147221500);
				}
				IAdviseSink adviseSink = (IAdviseSink)this.adviseList[dwConnection - 1];
				this.adviseList.RemoveAt(dwConnection - 1);
				if (adviseSink != null && UnsafeNativeMethods.IsComObject(adviseSink))
				{
					UnsafeNativeMethods.ReleaseComObject(adviseSink);
				}
			}

			// Token: 0x0600643C RID: 25660 RVA: 0x001746A8 File Offset: 0x001728A8
			internal void UpdateBounds(ref int x, ref int y, ref int width, ref int height, int flags)
			{
				if (!this.activeXState[Control.ActiveXImpl.adjustingRect] && this.activeXState[Control.ActiveXImpl.inPlaceVisible])
				{
					UnsafeNativeMethods.IOleInPlaceSite oleInPlaceSite = this.clientSite as UnsafeNativeMethods.IOleInPlaceSite;
					if (oleInPlaceSite != null)
					{
						NativeMethods.COMRECT comrect = new NativeMethods.COMRECT();
						if ((flags & 2) != 0)
						{
							comrect.left = this.control.Left;
							comrect.top = this.control.Top;
						}
						else
						{
							comrect.left = x;
							comrect.top = y;
						}
						if ((flags & 1) != 0)
						{
							comrect.right = comrect.left + this.control.Width;
							comrect.bottom = comrect.top + this.control.Height;
						}
						else
						{
							comrect.right = comrect.left + width;
							comrect.bottom = comrect.top + height;
						}
						this.adjustRect = comrect;
						this.activeXState[Control.ActiveXImpl.adjustingRect] = true;
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							oleInPlaceSite.OnPosRectChange(comrect);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
							this.adjustRect = null;
							this.activeXState[Control.ActiveXImpl.adjustingRect] = false;
						}
						if ((flags & 2) == 0)
						{
							x = comrect.left;
							y = comrect.top;
						}
						if ((flags & 1) == 0)
						{
							width = comrect.right - comrect.left;
							height = comrect.bottom - comrect.top;
						}
					}
				}
			}

			// Token: 0x0600643D RID: 25661 RVA: 0x00174820 File Offset: 0x00172A20
			internal void UpdateAccelTable()
			{
				this.accelCount = -1;
				UnsafeNativeMethods.IOleControlSite oleControlSite = this.clientSite as UnsafeNativeMethods.IOleControlSite;
				if (oleControlSite != null)
				{
					IntSecurity.UnmanagedCode.Assert();
					oleControlSite.OnControlInfoChanged();
				}
			}

			// Token: 0x0600643E RID: 25662 RVA: 0x00174854 File Offset: 0x00172A54
			internal void ViewChangedInternal()
			{
				this.ViewChanged();
			}

			// Token: 0x0600643F RID: 25663 RVA: 0x0017485C File Offset: 0x00172A5C
			private void ViewChanged()
			{
				if (this.viewAdviseSink != null && !this.activeXState[Control.ActiveXImpl.saving])
				{
					IntSecurity.UnmanagedCode.Assert();
					try
					{
						this.viewAdviseSink.OnViewChange(1, -1);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					if (this.activeXState[Control.ActiveXImpl.viewAdviseOnlyOnce])
					{
						if (UnsafeNativeMethods.IsComObject(this.viewAdviseSink))
						{
							UnsafeNativeMethods.ReleaseComObject(this.viewAdviseSink);
						}
						this.viewAdviseSink = null;
					}
				}
			}

			// Token: 0x06006440 RID: 25664 RVA: 0x001748E4 File Offset: 0x00172AE4
			void IWindowTarget.OnHandleChange(IntPtr newHandle)
			{
				this.controlWindowTarget.OnHandleChange(newHandle);
			}

			// Token: 0x06006441 RID: 25665 RVA: 0x001748F4 File Offset: 0x00172AF4
			void IWindowTarget.OnMessage(ref Message m)
			{
				if (this.activeXState[Control.ActiveXImpl.uiDead])
				{
					if (m.Msg >= 512 && m.Msg <= 522)
					{
						return;
					}
					if (m.Msg >= 161 && m.Msg <= 169)
					{
						return;
					}
					if (m.Msg >= 256 && m.Msg <= 264)
					{
						return;
					}
				}
				IntSecurity.UnmanagedCode.Assert();
				this.controlWindowTarget.OnMessage(ref m);
			}

			// Token: 0x04003953 RID: 14675
			private static readonly int hiMetricPerInch = 2540;

			// Token: 0x04003954 RID: 14676
			private static readonly int viewAdviseOnlyOnce = BitVector32.CreateMask();

			// Token: 0x04003955 RID: 14677
			private static readonly int viewAdvisePrimeFirst = BitVector32.CreateMask(Control.ActiveXImpl.viewAdviseOnlyOnce);

			// Token: 0x04003956 RID: 14678
			private static readonly int eventsFrozen = BitVector32.CreateMask(Control.ActiveXImpl.viewAdvisePrimeFirst);

			// Token: 0x04003957 RID: 14679
			private static readonly int changingExtents = BitVector32.CreateMask(Control.ActiveXImpl.eventsFrozen);

			// Token: 0x04003958 RID: 14680
			private static readonly int saving = BitVector32.CreateMask(Control.ActiveXImpl.changingExtents);

			// Token: 0x04003959 RID: 14681
			private static readonly int isDirty = BitVector32.CreateMask(Control.ActiveXImpl.saving);

			// Token: 0x0400395A RID: 14682
			private static readonly int inPlaceActive = BitVector32.CreateMask(Control.ActiveXImpl.isDirty);

			// Token: 0x0400395B RID: 14683
			private static readonly int inPlaceVisible = BitVector32.CreateMask(Control.ActiveXImpl.inPlaceActive);

			// Token: 0x0400395C RID: 14684
			private static readonly int uiActive = BitVector32.CreateMask(Control.ActiveXImpl.inPlaceVisible);

			// Token: 0x0400395D RID: 14685
			private static readonly int uiDead = BitVector32.CreateMask(Control.ActiveXImpl.uiActive);

			// Token: 0x0400395E RID: 14686
			private static readonly int adjustingRect = BitVector32.CreateMask(Control.ActiveXImpl.uiDead);

			// Token: 0x0400395F RID: 14687
			private static Point logPixels = Point.Empty;

			// Token: 0x04003960 RID: 14688
			private static NativeMethods.tagOLEVERB[] axVerbs;

			// Token: 0x04003961 RID: 14689
			private static int globalActiveXCount = 0;

			// Token: 0x04003962 RID: 14690
			private static bool checkedIE;

			// Token: 0x04003963 RID: 14691
			private static bool isIE;

			// Token: 0x04003964 RID: 14692
			private Control control;

			// Token: 0x04003965 RID: 14693
			private IWindowTarget controlWindowTarget;

			// Token: 0x04003966 RID: 14694
			private IntPtr clipRegion;

			// Token: 0x04003967 RID: 14695
			private UnsafeNativeMethods.IOleClientSite clientSite;

			// Token: 0x04003968 RID: 14696
			private UnsafeNativeMethods.IOleInPlaceUIWindow inPlaceUiWindow;

			// Token: 0x04003969 RID: 14697
			private UnsafeNativeMethods.IOleInPlaceFrame inPlaceFrame;

			// Token: 0x0400396A RID: 14698
			private ArrayList adviseList;

			// Token: 0x0400396B RID: 14699
			private IAdviseSink viewAdviseSink;

			// Token: 0x0400396C RID: 14700
			private BitVector32 activeXState;

			// Token: 0x0400396D RID: 14701
			private Control.AmbientProperty[] ambientProperties;

			// Token: 0x0400396E RID: 14702
			private IntPtr hwndParent;

			// Token: 0x0400396F RID: 14703
			private IntPtr accelTable;

			// Token: 0x04003970 RID: 14704
			private short accelCount = -1;

			// Token: 0x04003971 RID: 14705
			private NativeMethods.COMRECT adjustRect;

			// Token: 0x020008B5 RID: 2229
			internal static class AdviseHelper
			{
				// Token: 0x06007291 RID: 29329 RVA: 0x001A2A40 File Offset: 0x001A0C40
				public static bool AdviseConnectionPoint(object connectionPoint, object sink, Type eventInterface, out int cookie)
				{
					bool flag;
					using (Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer comConnectionPointContainer = new Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer(connectionPoint, true))
					{
						flag = Control.ActiveXImpl.AdviseHelper.AdviseConnectionPoint(comConnectionPointContainer, sink, eventInterface, out cookie);
					}
					return flag;
				}

				// Token: 0x06007292 RID: 29330 RVA: 0x001A2A7C File Offset: 0x001A0C7C
				internal static bool AdviseConnectionPoint(Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer cpc, object sink, Type eventInterface, out int cookie)
				{
					bool flag;
					using (Control.ActiveXImpl.AdviseHelper.ComConnectionPoint comConnectionPoint = cpc.FindConnectionPoint(eventInterface))
					{
						using (Control.ActiveXImpl.AdviseHelper.SafeIUnknown safeIUnknown = new Control.ActiveXImpl.AdviseHelper.SafeIUnknown(sink, true))
						{
							flag = comConnectionPoint.Advise(safeIUnknown.DangerousGetHandle(), out cookie);
						}
					}
					return flag;
				}

				// Token: 0x02000982 RID: 2434
				internal class SafeIUnknown : SafeHandle
				{
					// Token: 0x0600756E RID: 30062 RVA: 0x001A8131 File Offset: 0x001A6331
					public SafeIUnknown(object obj, bool addRefIntPtr)
						: this(obj, addRefIntPtr, Guid.Empty)
					{
					}

					// Token: 0x0600756F RID: 30063 RVA: 0x001A8140 File Offset: 0x001A6340
					public SafeIUnknown(object obj, bool addRefIntPtr, Guid iid)
						: base(IntPtr.Zero, true)
					{
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
						}
						finally
						{
							IntPtr intPtr;
							if (obj is IntPtr)
							{
								intPtr = (IntPtr)obj;
								if (addRefIntPtr)
								{
									Marshal.AddRef(intPtr);
								}
							}
							else
							{
								intPtr = Marshal.GetIUnknownForObject(obj);
							}
							if (iid != Guid.Empty)
							{
								IntPtr intPtr2 = intPtr;
								try
								{
									intPtr = Control.ActiveXImpl.AdviseHelper.SafeIUnknown.InternalQueryInterface(intPtr, ref iid);
								}
								finally
								{
									Marshal.Release(intPtr2);
								}
							}
							this.handle = intPtr;
						}
					}

					// Token: 0x06007570 RID: 30064 RVA: 0x001A81C8 File Offset: 0x001A63C8
					private static IntPtr InternalQueryInterface(IntPtr pUnk, ref Guid iid)
					{
						IntPtr intPtr;
						if (Marshal.QueryInterface(pUnk, ref iid, out intPtr) != 0 || intPtr == IntPtr.Zero)
						{
							throw new InvalidCastException(SR.GetString("AxInterfaceNotSupported"));
						}
						return intPtr;
					}

					// Token: 0x17001B02 RID: 6914
					// (get) Token: 0x06007571 RID: 30065 RVA: 0x001A8200 File Offset: 0x001A6400
					public sealed override bool IsInvalid
					{
						get
						{
							return base.IsClosed || IntPtr.Zero == this.handle;
						}
					}

					// Token: 0x06007572 RID: 30066 RVA: 0x001A821C File Offset: 0x001A641C
					protected sealed override bool ReleaseHandle()
					{
						IntPtr handle = this.handle;
						this.handle = IntPtr.Zero;
						if (IntPtr.Zero != handle)
						{
							Marshal.Release(handle);
						}
						return true;
					}

					// Token: 0x06007573 RID: 30067 RVA: 0x001A8250 File Offset: 0x001A6450
					protected V LoadVtable<V>()
					{
						IntPtr intPtr = Marshal.ReadIntPtr(this.handle, 0);
						return (V)((object)Marshal.PtrToStructure(intPtr, typeof(V)));
					}
				}

				// Token: 0x02000983 RID: 2435
				internal sealed class ComConnectionPointContainer : Control.ActiveXImpl.AdviseHelper.SafeIUnknown
				{
					// Token: 0x06007574 RID: 30068 RVA: 0x001A827F File Offset: 0x001A647F
					public ComConnectionPointContainer(object obj, bool addRefIntPtr)
						: base(obj, addRefIntPtr, typeof(IConnectionPointContainer).GUID)
					{
						this.vtbl = base.LoadVtable<Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer.VTABLE>();
					}

					// Token: 0x06007575 RID: 30069 RVA: 0x001A82A4 File Offset: 0x001A64A4
					public Control.ActiveXImpl.AdviseHelper.ComConnectionPoint FindConnectionPoint(Type eventInterface)
					{
						Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer.FindConnectionPointD findConnectionPointD = (Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer.FindConnectionPointD)Marshal.GetDelegateForFunctionPointer(this.vtbl.FindConnectionPointPtr, typeof(Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer.FindConnectionPointD));
						IntPtr zero = IntPtr.Zero;
						Guid guid = eventInterface.GUID;
						if (findConnectionPointD(this.handle, ref guid, out zero) != 0 || zero == IntPtr.Zero)
						{
							throw new ArgumentException(SR.GetString("AXNoConnectionPoint", new object[] { eventInterface.Name }));
						}
						return new Control.ActiveXImpl.AdviseHelper.ComConnectionPoint(zero, false);
					}

					// Token: 0x040047D6 RID: 18390
					private Control.ActiveXImpl.AdviseHelper.ComConnectionPointContainer.VTABLE vtbl;

					// Token: 0x02000986 RID: 2438
					[StructLayout(LayoutKind.Sequential)]
					private class VTABLE
					{
						// Token: 0x040047D9 RID: 18393
						public IntPtr QueryInterfacePtr;

						// Token: 0x040047DA RID: 18394
						public IntPtr AddRefPtr;

						// Token: 0x040047DB RID: 18395
						public IntPtr ReleasePtr;

						// Token: 0x040047DC RID: 18396
						public IntPtr EnumConnectionPointsPtr;

						// Token: 0x040047DD RID: 18397
						public IntPtr FindConnectionPointPtr;
					}

					// Token: 0x02000987 RID: 2439
					// (Invoke) Token: 0x0600757A RID: 30074
					[UnmanagedFunctionPointer(CallingConvention.StdCall)]
					private delegate int FindConnectionPointD(IntPtr This, ref Guid iid, out IntPtr ppv);
				}

				// Token: 0x02000984 RID: 2436
				internal sealed class ComConnectionPoint : Control.ActiveXImpl.AdviseHelper.SafeIUnknown
				{
					// Token: 0x06007576 RID: 30070 RVA: 0x001A832A File Offset: 0x001A652A
					public ComConnectionPoint(object obj, bool addRefIntPtr)
						: base(obj, addRefIntPtr, typeof(IConnectionPoint).GUID)
					{
						this.vtbl = base.LoadVtable<Control.ActiveXImpl.AdviseHelper.ComConnectionPoint.VTABLE>();
					}

					// Token: 0x06007577 RID: 30071 RVA: 0x001A8350 File Offset: 0x001A6550
					public bool Advise(IntPtr punkEventSink, out int cookie)
					{
						Control.ActiveXImpl.AdviseHelper.ComConnectionPoint.AdviseD adviseD = (Control.ActiveXImpl.AdviseHelper.ComConnectionPoint.AdviseD)Marshal.GetDelegateForFunctionPointer(this.vtbl.AdvisePtr, typeof(Control.ActiveXImpl.AdviseHelper.ComConnectionPoint.AdviseD));
						return adviseD(this.handle, punkEventSink, out cookie) == 0;
					}

					// Token: 0x040047D7 RID: 18391
					private Control.ActiveXImpl.AdviseHelper.ComConnectionPoint.VTABLE vtbl;

					// Token: 0x02000988 RID: 2440
					[StructLayout(LayoutKind.Sequential)]
					private class VTABLE
					{
						// Token: 0x040047DE RID: 18398
						public IntPtr QueryInterfacePtr;

						// Token: 0x040047DF RID: 18399
						public IntPtr AddRefPtr;

						// Token: 0x040047E0 RID: 18400
						public IntPtr ReleasePtr;

						// Token: 0x040047E1 RID: 18401
						public IntPtr GetConnectionInterfacePtr;

						// Token: 0x040047E2 RID: 18402
						public IntPtr GetConnectionPointContainterPtr;

						// Token: 0x040047E3 RID: 18403
						public IntPtr AdvisePtr;

						// Token: 0x040047E4 RID: 18404
						public IntPtr UnadvisePtr;

						// Token: 0x040047E5 RID: 18405
						public IntPtr EnumConnectionsPtr;
					}

					// Token: 0x02000989 RID: 2441
					// (Invoke) Token: 0x0600757F RID: 30079
					[UnmanagedFunctionPointer(CallingConvention.StdCall)]
					private delegate int AdviseD(IntPtr This, IntPtr punkEventSink, out int cookie);
				}
			}

			// Token: 0x020008B6 RID: 2230
			private class PropertyBagStream : UnsafeNativeMethods.IPropertyBag
			{
				// Token: 0x06007293 RID: 29331 RVA: 0x001A2ADC File Offset: 0x001A0CDC
				[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
				internal void Read(UnsafeNativeMethods.IStream istream)
				{
					Stream stream = new DataStreamFromComStream(istream);
					byte[] array = new byte[4096];
					int num = 0;
					int num2 = stream.Read(array, num, 4096);
					int num3 = num2;
					while (num2 == 4096)
					{
						byte[] array2 = new byte[array.Length + 4096];
						Array.Copy(array, array2, array.Length);
						array = array2;
						num += 4096;
						num2 = stream.Read(array, num, 4096);
						num3 += num2;
					}
					stream = new MemoryStream(array);
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					try
					{
						this.bag = (Hashtable)binaryFormatter.Deserialize(stream);
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsSecurityOrCriticalException(ex))
						{
							throw;
						}
						this.bag = new Hashtable();
					}
				}

				// Token: 0x06007294 RID: 29332 RVA: 0x001A2BA0 File Offset: 0x001A0DA0
				[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
				int UnsafeNativeMethods.IPropertyBag.Read(string pszPropName, ref object pVar, UnsafeNativeMethods.IErrorLog pErrorLog)
				{
					if (!this.bag.Contains(pszPropName))
					{
						return -2147024809;
					}
					pVar = this.bag[pszPropName];
					return 0;
				}

				// Token: 0x06007295 RID: 29333 RVA: 0x001A2BC5 File Offset: 0x001A0DC5
				[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
				int UnsafeNativeMethods.IPropertyBag.Write(string pszPropName, ref object pVar)
				{
					this.bag[pszPropName] = pVar;
					return 0;
				}

				// Token: 0x06007296 RID: 29334 RVA: 0x001A2BD8 File Offset: 0x001A0DD8
				[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
				internal void Write(UnsafeNativeMethods.IStream istream)
				{
					Stream stream = new DataStreamFromComStream(istream);
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(stream, this.bag);
				}

				// Token: 0x0400452A RID: 17706
				private Hashtable bag = new Hashtable();
			}
		}

		// Token: 0x02000637 RID: 1591
		private class AxSourcingSite : ISite, IServiceProvider
		{
			// Token: 0x06006443 RID: 25667 RVA: 0x00174A43 File Offset: 0x00172C43
			internal AxSourcingSite(IComponent component, UnsafeNativeMethods.IOleClientSite clientSite, string name)
			{
				this.component = component;
				this.clientSite = clientSite;
				this.name = name;
			}

			// Token: 0x17001572 RID: 5490
			// (get) Token: 0x06006444 RID: 25668 RVA: 0x00174A60 File Offset: 0x00172C60
			public IComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x17001573 RID: 5491
			// (get) Token: 0x06006445 RID: 25669 RVA: 0x00015C90 File Offset: 0x00013E90
			public IContainer Container
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006446 RID: 25670 RVA: 0x00174A68 File Offset: 0x00172C68
			public object GetService(Type service)
			{
				object obj = null;
				if (service == typeof(HtmlDocument))
				{
					UnsafeNativeMethods.IOleContainer oleContainer;
					int container;
					try
					{
						IntSecurity.UnmanagedCode.Assert();
						container = this.clientSite.GetContainer(out oleContainer);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					if (NativeMethods.Succeeded(container) && oleContainer is UnsafeNativeMethods.IHTMLDocument)
					{
						if (this.shimManager == null)
						{
							this.shimManager = new HtmlShimManager();
						}
						obj = new HtmlDocument(this.shimManager, oleContainer as UnsafeNativeMethods.IHTMLDocument);
					}
				}
				else if (this.clientSite.GetType().IsAssignableFrom(service))
				{
					IntSecurity.UnmanagedCode.Demand();
					obj = this.clientSite;
				}
				return obj;
			}

			// Token: 0x17001574 RID: 5492
			// (get) Token: 0x06006447 RID: 25671 RVA: 0x0001180C File Offset: 0x0000FA0C
			public bool DesignMode
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001575 RID: 5493
			// (get) Token: 0x06006448 RID: 25672 RVA: 0x00174B18 File Offset: 0x00172D18
			// (set) Token: 0x06006449 RID: 25673 RVA: 0x00174B20 File Offset: 0x00172D20
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					if (value == null || this.name == null)
					{
						this.name = value;
					}
				}
			}

			// Token: 0x04003972 RID: 14706
			private IComponent component;

			// Token: 0x04003973 RID: 14707
			private UnsafeNativeMethods.IOleClientSite clientSite;

			// Token: 0x04003974 RID: 14708
			private string name;

			// Token: 0x04003975 RID: 14709
			private HtmlShimManager shimManager;
		}

		// Token: 0x02000638 RID: 1592
		private class ActiveXFontMarshaler : ICustomMarshaler
		{
			// Token: 0x0600644A RID: 25674 RVA: 0x000070A6 File Offset: 0x000052A6
			public void CleanUpManagedData(object obj)
			{
			}

			// Token: 0x0600644B RID: 25675 RVA: 0x00174B34 File Offset: 0x00172D34
			public void CleanUpNativeData(IntPtr pObj)
			{
				Marshal.Release(pObj);
			}

			// Token: 0x0600644C RID: 25676 RVA: 0x00174B3D File Offset: 0x00172D3D
			internal static ICustomMarshaler GetInstance(string cookie)
			{
				if (Control.ActiveXFontMarshaler.instance == null)
				{
					Control.ActiveXFontMarshaler.instance = new Control.ActiveXFontMarshaler();
				}
				return Control.ActiveXFontMarshaler.instance;
			}

			// Token: 0x0600644D RID: 25677 RVA: 0x00015C93 File Offset: 0x00013E93
			public int GetNativeDataSize()
			{
				return -1;
			}

			// Token: 0x0600644E RID: 25678 RVA: 0x00174B58 File Offset: 0x00172D58
			public IntPtr MarshalManagedToNative(object obj)
			{
				Font font = (Font)obj;
				NativeMethods.tagFONTDESC tagFONTDESC = new NativeMethods.tagFONTDESC();
				NativeMethods.LOGFONT logfont = new NativeMethods.LOGFONT();
				IntSecurity.ObjectFromWin32Handle.Assert();
				try
				{
					font.ToLogFont(logfont);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				tagFONTDESC.lpstrName = font.Name;
				tagFONTDESC.cySize = (long)(font.SizeInPoints * 10000f);
				tagFONTDESC.sWeight = (short)logfont.lfWeight;
				tagFONTDESC.sCharset = (short)logfont.lfCharSet;
				tagFONTDESC.fItalic = font.Italic;
				tagFONTDESC.fUnderline = font.Underline;
				tagFONTDESC.fStrikethrough = font.Strikeout;
				Guid guid = typeof(UnsafeNativeMethods.IFont).GUID;
				UnsafeNativeMethods.IFont font2 = UnsafeNativeMethods.OleCreateFontIndirect(tagFONTDESC, ref guid);
				IntPtr iunknownForObject = Marshal.GetIUnknownForObject(font2);
				IntPtr intPtr;
				int num = Marshal.QueryInterface(iunknownForObject, ref guid, out intPtr);
				Marshal.Release(iunknownForObject);
				if (NativeMethods.Failed(num))
				{
					Marshal.ThrowExceptionForHR(num);
				}
				return intPtr;
			}

			// Token: 0x0600644F RID: 25679 RVA: 0x00174C48 File Offset: 0x00172E48
			public object MarshalNativeToManaged(IntPtr pObj)
			{
				UnsafeNativeMethods.IFont font = (UnsafeNativeMethods.IFont)Marshal.GetObjectForIUnknown(pObj);
				IntPtr intPtr = IntPtr.Zero;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					intPtr = font.GetHFont();
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				Font font2 = null;
				IntSecurity.ObjectFromWin32Handle.Assert();
				try
				{
					font2 = Font.FromHfont(intPtr);
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
					font2 = Control.DefaultFont;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				return font2;
			}

			// Token: 0x04003976 RID: 14710
			private static Control.ActiveXFontMarshaler instance;
		}

		// Token: 0x02000639 RID: 1593
		private class ActiveXVerbEnum : UnsafeNativeMethods.IEnumOLEVERB
		{
			// Token: 0x06006451 RID: 25681 RVA: 0x00174CD8 File Offset: 0x00172ED8
			internal ActiveXVerbEnum(NativeMethods.tagOLEVERB[] verbs)
			{
				this.verbs = verbs;
				this.current = 0;
			}

			// Token: 0x06006452 RID: 25682 RVA: 0x00174CF0 File Offset: 0x00172EF0
			public int Next(int celt, NativeMethods.tagOLEVERB rgelt, int[] pceltFetched)
			{
				int num = 0;
				if (celt != 1)
				{
					celt = 1;
				}
				while (celt > 0 && this.current < this.verbs.Length)
				{
					rgelt.lVerb = this.verbs[this.current].lVerb;
					rgelt.lpszVerbName = this.verbs[this.current].lpszVerbName;
					rgelt.fuFlags = this.verbs[this.current].fuFlags;
					rgelt.grfAttribs = this.verbs[this.current].grfAttribs;
					celt--;
					this.current++;
					num++;
				}
				if (pceltFetched != null)
				{
					pceltFetched[0] = num;
				}
				if (celt != 0)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06006453 RID: 25683 RVA: 0x00174DA3 File Offset: 0x00172FA3
			public int Skip(int celt)
			{
				if (this.current + celt < this.verbs.Length)
				{
					this.current += celt;
					return 0;
				}
				this.current = this.verbs.Length;
				return 1;
			}

			// Token: 0x06006454 RID: 25684 RVA: 0x00174DD6 File Offset: 0x00172FD6
			public void Reset()
			{
				this.current = 0;
			}

			// Token: 0x06006455 RID: 25685 RVA: 0x00174DDF File Offset: 0x00172FDF
			public void Clone(out UnsafeNativeMethods.IEnumOLEVERB ppenum)
			{
				ppenum = new Control.ActiveXVerbEnum(this.verbs);
			}

			// Token: 0x04003977 RID: 14711
			private NativeMethods.tagOLEVERB[] verbs;

			// Token: 0x04003978 RID: 14712
			private int current;
		}

		// Token: 0x0200063A RID: 1594
		private class AmbientProperty
		{
			// Token: 0x06006456 RID: 25686 RVA: 0x00174DEE File Offset: 0x00172FEE
			internal AmbientProperty(string name, int dispID)
			{
				this.name = name;
				this.dispID = dispID;
				this.value = null;
				this.empty = true;
			}

			// Token: 0x17001576 RID: 5494
			// (get) Token: 0x06006457 RID: 25687 RVA: 0x00174E12 File Offset: 0x00173012
			internal string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17001577 RID: 5495
			// (get) Token: 0x06006458 RID: 25688 RVA: 0x00174E1A File Offset: 0x0017301A
			internal int DispID
			{
				get
				{
					return this.dispID;
				}
			}

			// Token: 0x17001578 RID: 5496
			// (get) Token: 0x06006459 RID: 25689 RVA: 0x00174E22 File Offset: 0x00173022
			internal bool Empty
			{
				get
				{
					return this.empty;
				}
			}

			// Token: 0x17001579 RID: 5497
			// (get) Token: 0x0600645A RID: 25690 RVA: 0x00174E2A File Offset: 0x0017302A
			// (set) Token: 0x0600645B RID: 25691 RVA: 0x00174E32 File Offset: 0x00173032
			internal object Value
			{
				get
				{
					return this.value;
				}
				set
				{
					this.value = value;
					this.empty = false;
				}
			}

			// Token: 0x0600645C RID: 25692 RVA: 0x00174E42 File Offset: 0x00173042
			internal void ResetValue()
			{
				this.empty = true;
				this.value = null;
			}

			// Token: 0x04003979 RID: 14713
			private string name;

			// Token: 0x0400397A RID: 14714
			private int dispID;

			// Token: 0x0400397B RID: 14715
			private object value;

			// Token: 0x0400397C RID: 14716
			private bool empty;
		}

		// Token: 0x0200063B RID: 1595
		private class MetafileDCWrapper : IDisposable
		{
			// Token: 0x0600645D RID: 25693 RVA: 0x00174E54 File Offset: 0x00173054
			internal MetafileDCWrapper(HandleRef hOriginalDC, Size size)
			{
				if (size.Width < 0 || size.Height < 0)
				{
					throw new ArgumentException("size", SR.GetString("ControlMetaFileDCWrapperSizeInvalid"));
				}
				this.hMetafileDC = hOriginalDC;
				this.destRect = new NativeMethods.RECT(0, 0, size.Width, size.Height);
				this.hBitmapDC = new HandleRef(this, UnsafeNativeMethods.CreateCompatibleDC(NativeMethods.NullHandleRef));
				int deviceCaps = UnsafeNativeMethods.GetDeviceCaps(this.hBitmapDC, 14);
				int deviceCaps2 = UnsafeNativeMethods.GetDeviceCaps(this.hBitmapDC, 12);
				this.hBitmap = new HandleRef(this, SafeNativeMethods.CreateBitmap(size.Width, size.Height, deviceCaps, deviceCaps2, IntPtr.Zero));
				this.hOriginalBmp = new HandleRef(this, SafeNativeMethods.SelectObject(this.hBitmapDC, this.hBitmap));
			}

			// Token: 0x0600645E RID: 25694 RVA: 0x00174F54 File Offset: 0x00173154
			~MetafileDCWrapper()
			{
				((IDisposable)this).Dispose();
			}

			// Token: 0x0600645F RID: 25695 RVA: 0x00174F80 File Offset: 0x00173180
			void IDisposable.Dispose()
			{
				if (this.hBitmapDC.Handle == IntPtr.Zero || this.hMetafileDC.Handle == IntPtr.Zero || this.hBitmap.Handle == IntPtr.Zero)
				{
					return;
				}
				try
				{
					bool flag = this.DICopy(this.hMetafileDC, this.hBitmapDC, this.destRect, true);
					SafeNativeMethods.SelectObject(this.hBitmapDC, this.hOriginalBmp);
					flag = SafeNativeMethods.DeleteObject(this.hBitmap);
					flag = UnsafeNativeMethods.DeleteCompatibleDC(this.hBitmapDC);
				}
				finally
				{
					this.hBitmapDC = NativeMethods.NullHandleRef;
					this.hBitmap = NativeMethods.NullHandleRef;
					this.hOriginalBmp = NativeMethods.NullHandleRef;
					GC.SuppressFinalize(this);
				}
			}

			// Token: 0x1700157A RID: 5498
			// (get) Token: 0x06006460 RID: 25696 RVA: 0x00175054 File Offset: 0x00173254
			internal IntPtr HDC
			{
				get
				{
					return this.hBitmapDC.Handle;
				}
			}

			// Token: 0x06006461 RID: 25697 RVA: 0x00175064 File Offset: 0x00173264
			private unsafe bool DICopy(HandleRef hdcDest, HandleRef hdcSrc, NativeMethods.RECT rect, bool bStretch)
			{
				bool flag = false;
				HandleRef handleRef = new HandleRef(this, SafeNativeMethods.CreateBitmap(1, 1, 1, 1, IntPtr.Zero));
				if (handleRef.Handle == IntPtr.Zero)
				{
					return flag;
				}
				try
				{
					HandleRef handleRef2 = new HandleRef(this, SafeNativeMethods.SelectObject(hdcSrc, handleRef));
					if (handleRef2.Handle == IntPtr.Zero)
					{
						return flag;
					}
					SafeNativeMethods.SelectObject(hdcSrc, handleRef2);
					NativeMethods.BITMAP bitmap = new NativeMethods.BITMAP();
					if (UnsafeNativeMethods.GetObject(handleRef2, Marshal.SizeOf(bitmap), bitmap) == 0)
					{
						return flag;
					}
					NativeMethods.BITMAPINFO_FLAT bitmapinfo_FLAT = default(NativeMethods.BITMAPINFO_FLAT);
					bitmapinfo_FLAT.bmiHeader_biSize = Marshal.SizeOf(typeof(NativeMethods.BITMAPINFOHEADER));
					bitmapinfo_FLAT.bmiHeader_biWidth = bitmap.bmWidth;
					bitmapinfo_FLAT.bmiHeader_biHeight = bitmap.bmHeight;
					bitmapinfo_FLAT.bmiHeader_biPlanes = 1;
					bitmapinfo_FLAT.bmiHeader_biBitCount = bitmap.bmBitsPixel;
					bitmapinfo_FLAT.bmiHeader_biCompression = 0;
					bitmapinfo_FLAT.bmiHeader_biSizeImage = 0;
					bitmapinfo_FLAT.bmiHeader_biXPelsPerMeter = 0;
					bitmapinfo_FLAT.bmiHeader_biYPelsPerMeter = 0;
					bitmapinfo_FLAT.bmiHeader_biClrUsed = 0;
					bitmapinfo_FLAT.bmiHeader_biClrImportant = 0;
					bitmapinfo_FLAT.bmiColors = new byte[1024];
					long num = 1L << (int)((bitmap.bmBitsPixel * bitmap.bmPlanes) & 31);
					if (num <= 256L)
					{
						byte[] array = new byte[Marshal.SizeOf(typeof(NativeMethods.PALETTEENTRY)) * 256];
						SafeNativeMethods.GetSystemPaletteEntries(hdcSrc, 0, (int)num, array);
						try
						{
							byte[] array2;
							byte* ptr;
							if ((array2 = bitmapinfo_FLAT.bmiColors) == null || array2.Length == 0)
							{
								ptr = null;
							}
							else
							{
								ptr = &array2[0];
							}
							try
							{
								byte[] array3;
								byte* ptr2;
								if ((array3 = array) == null || array3.Length == 0)
								{
									ptr2 = null;
								}
								else
								{
									ptr2 = &array3[0];
								}
								NativeMethods.RGBQUAD* ptr3 = (NativeMethods.RGBQUAD*)ptr;
								NativeMethods.PALETTEENTRY* ptr4 = (NativeMethods.PALETTEENTRY*)ptr2;
								for (long num2 = 0L; num2 < (long)((int)num); num2 += 1L)
								{
									ptr3[num2 * (long)sizeof(NativeMethods.RGBQUAD) / (long)sizeof(NativeMethods.RGBQUAD)].rgbRed = ptr4[num2 * (long)sizeof(NativeMethods.PALETTEENTRY) / (long)sizeof(NativeMethods.PALETTEENTRY)].peRed;
									ptr3[num2 * (long)sizeof(NativeMethods.RGBQUAD) / (long)sizeof(NativeMethods.RGBQUAD)].rgbBlue = ptr4[num2 * (long)sizeof(NativeMethods.PALETTEENTRY) / (long)sizeof(NativeMethods.PALETTEENTRY)].peBlue;
									ptr3[num2 * (long)sizeof(NativeMethods.RGBQUAD) / (long)sizeof(NativeMethods.RGBQUAD)].rgbGreen = ptr4[num2 * (long)sizeof(NativeMethods.PALETTEENTRY) / (long)sizeof(NativeMethods.PALETTEENTRY)].peGreen;
								}
							}
							finally
							{
								byte[] array3 = null;
							}
						}
						finally
						{
							byte[] array2 = null;
						}
					}
					long num3 = (long)bitmap.bmBitsPixel * (long)bitmap.bmWidth;
					long num4 = (num3 + 7L) / 8L;
					long num5 = num4 * (long)bitmap.bmHeight;
					byte[] array4 = new byte[num5];
					if (SafeNativeMethods.GetDIBits(hdcSrc, handleRef2, 0, bitmap.bmHeight, array4, ref bitmapinfo_FLAT, 0) == 0)
					{
						return flag;
					}
					int num6;
					int num7;
					int num8;
					int num9;
					if (bStretch)
					{
						num6 = rect.left;
						num7 = rect.top;
						num8 = rect.right - rect.left;
						num9 = rect.bottom - rect.top;
					}
					else
					{
						num6 = rect.left;
						num7 = rect.top;
						num8 = bitmap.bmWidth;
						num9 = bitmap.bmHeight;
					}
					int num10 = SafeNativeMethods.StretchDIBits(hdcDest, num6, num7, num8, num9, 0, 0, bitmap.bmWidth, bitmap.bmHeight, array4, ref bitmapinfo_FLAT, 0, 13369376);
					if (num10 == -1)
					{
						return flag;
					}
					flag = true;
				}
				finally
				{
					SafeNativeMethods.DeleteObject(handleRef);
				}
				return flag;
			}

			// Token: 0x0400397D RID: 14717
			private HandleRef hBitmapDC = NativeMethods.NullHandleRef;

			// Token: 0x0400397E RID: 14718
			private HandleRef hBitmap = NativeMethods.NullHandleRef;

			// Token: 0x0400397F RID: 14719
			private HandleRef hOriginalBmp = NativeMethods.NullHandleRef;

			// Token: 0x04003980 RID: 14720
			private HandleRef hMetafileDC = NativeMethods.NullHandleRef;

			// Token: 0x04003981 RID: 14721
			private NativeMethods.RECT destRect;
		}

		/// <summary>Provides information about a control that can be used by an accessibility application.</summary>
		// Token: 0x0200063C RID: 1596
		[ComVisible(true)]
		public class ControlAccessibleObject : AccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" /> class.</summary>
			/// <param name="ownerControl">The <see cref="T:System.Windows.Forms.Control" /> that owns the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="ownerControl" /> parameter value is <see langword="null" />.</exception>
			// Token: 0x06006462 RID: 25698 RVA: 0x001753DC File Offset: 0x001735DC
			public ControlAccessibleObject(Control ownerControl)
			{
				if (ownerControl == null)
				{
					throw new ArgumentNullException("ownerControl");
				}
				this.ownerControl = ownerControl;
				IntPtr intPtr = ownerControl.Handle;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					this.Handle = intPtr;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}

			// Token: 0x06006463 RID: 25699 RVA: 0x00175440 File Offset: 0x00173640
			internal ControlAccessibleObject(Control ownerControl, int accObjId)
			{
				if (ownerControl == null)
				{
					throw new ArgumentNullException("ownerControl");
				}
				base.AccessibleObjectId = accObjId;
				this.ownerControl = ownerControl;
				IntPtr intPtr = ownerControl.Handle;
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					this.Handle = intPtr;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}

			// Token: 0x06006464 RID: 25700 RVA: 0x001754AC File Offset: 0x001736AC
			internal void ClearOwnerControl()
			{
				if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
				{
					this.ownerControl = null;
				}
			}

			// Token: 0x06006465 RID: 25701 RVA: 0x001754BC File Offset: 0x001736BC
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.Owner == null)
				{
					return base.FragmentNavigate(direction);
				}
				if (this.Owner.ToolStripControlHost != null && (direction == UnsafeNativeMethods.NavigateDirection.Parent || direction == UnsafeNativeMethods.NavigateDirection.PreviousSibling || direction == UnsafeNativeMethods.NavigateDirection.NextSibling))
				{
					return this.Owner.ToolStripControlHost.AccessibilityObject.FragmentNavigate(direction);
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x1700157B RID: 5499
			// (get) Token: 0x06006466 RID: 25702 RVA: 0x00175518 File Offset: 0x00173718
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.Owner == null)
					{
						return base.FragmentRoot;
					}
					ToolStripControlHost toolStripControlHost = this.Owner.ToolStripControlHost;
					ToolStrip toolStrip = ((toolStripControlHost != null) ? toolStripControlHost.Owner : null);
					if (toolStrip != null && toolStrip.IsHandleCreated)
					{
						return toolStrip.AccessibilityObject;
					}
					return base.FragmentRoot;
				}
			}

			// Token: 0x06006467 RID: 25703 RVA: 0x0017556B File Offset: 0x0017376B
			internal override int[] GetSysChildOrder()
			{
				if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.ownerControl == null)
				{
					return new int[0];
				}
				if (this.ownerControl.GetStyle(ControlStyles.ContainerControl))
				{
					return this.ownerControl.GetChildWindowsInTabOrder();
				}
				return base.GetSysChildOrder();
			}

			// Token: 0x06006468 RID: 25704 RVA: 0x001755A4 File Offset: 0x001737A4
			internal override bool GetSysChild(AccessibleNavigation navdir, out AccessibleObject accessibleObject)
			{
				accessibleObject = null;
				if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.ownerControl == null)
				{
					return false;
				}
				Control parentInternal = this.ownerControl.ParentInternal;
				int num = -1;
				Control[] array = null;
				switch (navdir)
				{
				case AccessibleNavigation.Next:
					if (base.IsNonClientObject && parentInternal != null)
					{
						array = parentInternal.GetChildControlsInTabOrder(true);
						num = Array.IndexOf<Control>(array, this.ownerControl);
						if (num != -1)
						{
							num++;
						}
					}
					break;
				case AccessibleNavigation.Previous:
					if (base.IsNonClientObject && parentInternal != null)
					{
						array = parentInternal.GetChildControlsInTabOrder(true);
						num = Array.IndexOf<Control>(array, this.ownerControl);
						if (num != -1)
						{
							num--;
						}
					}
					break;
				case AccessibleNavigation.FirstChild:
					if (base.IsClientObject)
					{
						array = this.ownerControl.GetChildControlsInTabOrder(true);
						num = 0;
					}
					break;
				case AccessibleNavigation.LastChild:
					if (base.IsClientObject)
					{
						array = this.ownerControl.GetChildControlsInTabOrder(true);
						num = array.Length - 1;
					}
					break;
				}
				if (array == null || array.Length == 0)
				{
					return false;
				}
				if (num >= 0 && num < array.Length)
				{
					accessibleObject = array[num].NcAccessibilityObject;
				}
				return true;
			}

			/// <summary>Gets a string that describes the default action of the object. Not all objects have a default action.</summary>
			/// <returns>A description of the default action for an object, or <see langword="null" /> if this object has no default action.</returns>
			// Token: 0x1700157C RID: 5500
			// (get) Token: 0x06006469 RID: 25705 RVA: 0x0017569C File Offset: 0x0017389C
			public override string DefaultAction
			{
				get
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.ownerControl == null)
					{
						return base.DefaultAction;
					}
					string accessibleDefaultActionDescription = this.ownerControl.AccessibleDefaultActionDescription;
					if (accessibleDefaultActionDescription != null)
					{
						return accessibleDefaultActionDescription;
					}
					return base.DefaultAction;
				}
			}

			// Token: 0x1700157D RID: 5501
			// (get) Token: 0x0600646A RID: 25706 RVA: 0x001756D6 File Offset: 0x001738D6
			internal override int[] RuntimeId
			{
				get
				{
					if (this.runtimeId == null)
					{
						this.runtimeId = new int[2];
						this.runtimeId[0] = 42;
						this.runtimeId[1] = (int)(long)this.Handle;
					}
					return this.runtimeId;
				}
			}

			/// <summary>Gets the description of the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />.</summary>
			/// <returns>A string describing the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />.</returns>
			// Token: 0x1700157E RID: 5502
			// (get) Token: 0x0600646B RID: 25707 RVA: 0x00175710 File Offset: 0x00173910
			public override string Description
			{
				get
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.ownerControl == null)
					{
						return base.Description;
					}
					string accessibleDescription = this.ownerControl.AccessibleDescription;
					if (accessibleDescription != null)
					{
						return accessibleDescription;
					}
					return base.Description;
				}
			}

			/// <summary>Gets or sets the handle of the accessible object.</summary>
			/// <returns>An <see cref="T:System.IntPtr" /> that represents the handle of the control.</returns>
			// Token: 0x1700157F RID: 5503
			// (get) Token: 0x0600646C RID: 25708 RVA: 0x0017574A File Offset: 0x0017394A
			// (set) Token: 0x0600646D RID: 25709 RVA: 0x00175754 File Offset: 0x00173954
			public IntPtr Handle
			{
				get
				{
					return this.handle;
				}
				set
				{
					IntSecurity.UnmanagedCode.Demand();
					if (this.handle != value)
					{
						this.handle = value;
						if (Control.ControlAccessibleObject.oleAccAvailable == IntPtr.Zero)
						{
							return;
						}
						bool flag = false;
						if (Control.ControlAccessibleObject.oleAccAvailable == NativeMethods.InvalidIntPtr)
						{
							Control.ControlAccessibleObject.oleAccAvailable = UnsafeNativeMethods.LoadLibraryFromSystemPathIfAvailable("oleacc.dll");
							flag = Control.ControlAccessibleObject.oleAccAvailable != IntPtr.Zero;
						}
						if (this.handle != IntPtr.Zero && Control.ControlAccessibleObject.oleAccAvailable != IntPtr.Zero)
						{
							base.UseStdAccessibleObjects(this.handle);
						}
						if (flag)
						{
							UnsafeNativeMethods.FreeLibrary(new HandleRef(null, Control.ControlAccessibleObject.oleAccAvailable));
						}
					}
				}
			}

			/// <summary>Gets the description of what the object does or how the object is used.</summary>
			/// <returns>The description of what the object does or how the object is used.</returns>
			// Token: 0x17001580 RID: 5504
			// (get) Token: 0x0600646E RID: 25710 RVA: 0x0017580C File Offset: 0x00173A0C
			public override string Help
			{
				get
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.Owner == null)
					{
						return base.Help;
					}
					QueryAccessibilityHelpEventHandler queryAccessibilityHelpEventHandler = (QueryAccessibilityHelpEventHandler)this.Owner.Events[Control.EventQueryAccessibilityHelp];
					if (queryAccessibilityHelpEventHandler != null)
					{
						QueryAccessibilityHelpEventArgs queryAccessibilityHelpEventArgs = new QueryAccessibilityHelpEventArgs();
						queryAccessibilityHelpEventHandler(this.Owner, queryAccessibilityHelpEventArgs);
						return queryAccessibilityHelpEventArgs.HelpString;
					}
					return base.Help;
				}
			}

			/// <summary>Gets the object shortcut key or access key for an accessible object.</summary>
			/// <returns>The object shortcut key or access key for an accessible object, or <see langword="null" /> if there is no shortcut key associated with the object.</returns>
			// Token: 0x17001581 RID: 5505
			// (get) Token: 0x0600646F RID: 25711 RVA: 0x00175870 File Offset: 0x00173A70
			public override string KeyboardShortcut
			{
				get
				{
					char mnemonic = WindowsFormsUtils.GetMnemonic(this.TextLabel, false);
					if (mnemonic != '\0')
					{
						return "Alt+" + mnemonic.ToString();
					}
					return null;
				}
			}

			/// <summary>Gets or sets the accessible object name.</summary>
			/// <returns>The accessible object name.</returns>
			// Token: 0x17001582 RID: 5506
			// (get) Token: 0x06006470 RID: 25712 RVA: 0x001758A0 File Offset: 0x00173AA0
			// (set) Token: 0x06006471 RID: 25713 RVA: 0x001758E4 File Offset: 0x00173AE4
			public override string Name
			{
				get
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.ownerControl == null)
					{
						return WindowsFormsUtils.TextWithoutMnemonics(this.TextLabel);
					}
					string accessibleName = this.ownerControl.AccessibleName;
					if (accessibleName != null)
					{
						return accessibleName;
					}
					return WindowsFormsUtils.TextWithoutMnemonics(this.TextLabel);
				}
				set
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.ownerControl == null)
					{
						return;
					}
					this.ownerControl.AccessibleName = value;
				}
			}

			/// <summary>Gets the parent of an accessible object.</summary>
			/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the parent of an accessible object, or <see langword="null" /> if there is no parent object.</returns>
			// Token: 0x17001583 RID: 5507
			// (get) Token: 0x06006472 RID: 25714 RVA: 0x00175902 File Offset: 0x00173B02
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return base.Parent;
				}
			}

			// Token: 0x17001584 RID: 5508
			// (get) Token: 0x06006473 RID: 25715 RVA: 0x0017590C File Offset: 0x00173B0C
			internal string TextLabel
			{
				get
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.ownerControl == null)
					{
						return null;
					}
					if (this.ownerControl.GetStyle(ControlStyles.UseTextForAccessibility))
					{
						string text = this.ownerControl.Text;
						if (!string.IsNullOrEmpty(text))
						{
							return text;
						}
					}
					Label previousLabel = this.PreviousLabel;
					if (previousLabel != null)
					{
						string text2 = previousLabel.Text;
						if (!string.IsNullOrEmpty(text2))
						{
							return text2;
						}
					}
					return null;
				}
			}

			/// <summary>Gets the owner of the accessible object.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that owns the <see cref="T:System.Windows.Forms.Control.ControlAccessibleObject" />.</returns>
			// Token: 0x17001585 RID: 5509
			// (get) Token: 0x06006474 RID: 25716 RVA: 0x0017596E File Offset: 0x00173B6E
			public Control Owner
			{
				get
				{
					return this.ownerControl;
				}
			}

			// Token: 0x17001586 RID: 5510
			// (get) Token: 0x06006475 RID: 25717 RVA: 0x00175978 File Offset: 0x00173B78
			internal Label PreviousLabel
			{
				get
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.Owner == null)
					{
						return null;
					}
					Control parentInternal = this.Owner.ParentInternal;
					if (parentInternal == null)
					{
						return null;
					}
					ContainerControl containerControl = parentInternal.GetContainerControlInternal() as ContainerControl;
					if (containerControl == null)
					{
						return null;
					}
					for (Control control = containerControl.GetNextControl(this.Owner, false); control != null; control = containerControl.GetNextControl(control, false))
					{
						if (control is Label)
						{
							return control as Label;
						}
						if (control.Visible && control.TabStop)
						{
							break;
						}
					}
					return null;
				}
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values.</returns>
			// Token: 0x17001587 RID: 5511
			// (get) Token: 0x06006476 RID: 25718 RVA: 0x001759F4 File Offset: 0x00173BF4
			public override AccessibleRole Role
			{
				get
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.ownerControl == null)
					{
						return base.Role;
					}
					AccessibleRole accessibleRole = this.ownerControl.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return base.Role;
				}
			}

			/// <summary>Gets an identifier for a Help topic and the path to the Help file associated with this accessible object.</summary>
			/// <param name="fileName">When this method returns, contains a string that represents the path to the Help file associated with this accessible object. This parameter is passed uninitialized.</param>
			/// <returns>An identifier for a Help topic, or -1 if there is no Help topic. On return, the <paramref name="fileName" /> parameter will contain the path to the Help file associated with this accessible object, or <see langword="null" /> if there is no <see langword="IAccessible" /> interface specified.</returns>
			// Token: 0x06006477 RID: 25719 RVA: 0x00175A30 File Offset: 0x00173C30
			public override int GetHelpTopic(out string fileName)
			{
				int num = 0;
				if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.ownerControl == null)
				{
					fileName = string.Empty;
					return num;
				}
				QueryAccessibilityHelpEventHandler queryAccessibilityHelpEventHandler = (QueryAccessibilityHelpEventHandler)this.Owner.Events[Control.EventQueryAccessibilityHelp];
				if (queryAccessibilityHelpEventHandler != null)
				{
					QueryAccessibilityHelpEventArgs queryAccessibilityHelpEventArgs = new QueryAccessibilityHelpEventArgs();
					queryAccessibilityHelpEventHandler(this.Owner, queryAccessibilityHelpEventArgs);
					fileName = queryAccessibilityHelpEventArgs.HelpNamespace;
					if (!string.IsNullOrEmpty(fileName))
					{
						IntSecurity.DemandFileIO(FileIOPermissionAccess.PathDiscovery, fileName);
					}
					try
					{
						num = int.Parse(queryAccessibilityHelpEventArgs.HelpKeyword, CultureInfo.InvariantCulture);
					}
					catch (Exception ex)
					{
						if (ClientUtils.IsSecurityOrCriticalException(ex))
						{
							throw;
						}
					}
					return num;
				}
				return base.GetHelpTopic(out fileName);
			}

			/// <summary>Notifies accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" />.</summary>
			/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of.</param>
			// Token: 0x06006478 RID: 25720 RVA: 0x00175ADC File Offset: 0x00173CDC
			public void NotifyClients(AccessibleEvents accEvent)
			{
				if (LocalAppContextSwitches.NoClientNotifications)
				{
					return;
				}
				UnsafeNativeMethods.NotifyWinEvent((int)accEvent, new HandleRef(this, this.Handle), -4, 0);
			}

			/// <summary>Notifies the accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" /> for the specified child control.</summary>
			/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of.</param>
			/// <param name="childID">The child <see cref="T:System.Windows.Forms.Control" /> to notify of the accessible event.</param>
			// Token: 0x06006479 RID: 25721 RVA: 0x00175AFB File Offset: 0x00173CFB
			public void NotifyClients(AccessibleEvents accEvent, int childID)
			{
				if (LocalAppContextSwitches.NoClientNotifications)
				{
					return;
				}
				UnsafeNativeMethods.NotifyWinEvent((int)accEvent, new HandleRef(this, this.Handle), -4, childID + 1);
			}

			/// <summary>Notifies the accessibility client applications of the specified <see cref="T:System.Windows.Forms.AccessibleEvents" /> for the specified child control, giving the identification of the <see cref="T:System.Windows.Forms.AccessibleObject" />.</summary>
			/// <param name="accEvent">The <see cref="T:System.Windows.Forms.AccessibleEvents" /> to notify the accessibility client applications of.</param>
			/// <param name="objectID">The identifier of the <see cref="T:System.Windows.Forms.AccessibleObject" />.</param>
			/// <param name="childID">The child <see cref="T:System.Windows.Forms.Control" /> to notify of the accessible event.</param>
			// Token: 0x0600647A RID: 25722 RVA: 0x00175B1C File Offset: 0x00173D1C
			public void NotifyClients(AccessibleEvents accEvent, int objectID, int childID)
			{
				if (LocalAppContextSwitches.NoClientNotifications)
				{
					return;
				}
				UnsafeNativeMethods.NotifyWinEvent((int)accEvent, new HandleRef(this, this.Handle), objectID, childID + 1);
			}

			/// <summary>Raises the LiveRegionChanged UI automation event.</summary>
			/// <returns>
			///   <see langword="true" /> if the operation succeeds; otherwise, <see langword="false" /> otherwise.</returns>
			/// <exception cref="T:System.InvalidOperationException">The owner control is not a live region.</exception>
			// Token: 0x0600647B RID: 25723 RVA: 0x00175B3C File Offset: 0x00173D3C
			public override bool RaiseLiveRegionChanged()
			{
				if (!(this.Owner is IAutomationLiveRegion))
				{
					throw new InvalidOperationException(SR.GetString("OwnerControlIsNotALiveRegion"));
				}
				return base.RaiseAutomationEvent(20024);
			}

			// Token: 0x0600647C RID: 25724 RVA: 0x00175B66 File Offset: 0x00173D66
			internal override bool IsIAccessibleExSupported()
			{
				return (AccessibilityImprovements.Level3 && this.Owner is IAutomationLiveRegion) || base.IsIAccessibleExSupported();
			}

			// Token: 0x0600647D RID: 25725 RVA: 0x00175B84 File Offset: 0x00173D84
			internal override object GetPropertyValue(int propertyID)
			{
				if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.Owner == null)
				{
					return null;
				}
				if (AccessibilityImprovements.Level3 && propertyID == 30135 && this.Owner is IAutomationLiveRegion)
				{
					return ((IAutomationLiveRegion)this.Owner).LiveSetting;
				}
				if (this.Owner.SupportsUiaProviders)
				{
					if (propertyID <= 30009)
					{
						if (propertyID == 30007)
						{
							return string.Empty;
						}
						if (propertyID == 30009)
						{
							return this.Owner.CanSelect;
						}
					}
					else if (propertyID != 30013)
					{
						if (propertyID == 30019 || propertyID == 30022)
						{
							return false;
						}
					}
					else
					{
						string help = this.Help;
						if (!AccessibilityImprovements.Level3)
						{
							return help;
						}
						return help ?? string.Empty;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x17001588 RID: 5512
			// (get) Token: 0x0600647E RID: 25726 RVA: 0x00175C58 File Offset: 0x00173E58
			internal override UnsafeNativeMethods.IRawElementProviderSimple HostRawElementProvider
			{
				get
				{
					if (AccessibilityImprovements.Level3)
					{
						UnsafeNativeMethods.IRawElementProviderSimple rawElementProviderSimple;
						UnsafeNativeMethods.UiaHostProviderFromHwnd(new HandleRef(this, this.Handle), out rawElementProviderSimple);
						return rawElementProviderSimple;
					}
					return base.HostRawElementProvider;
				}
			}

			/// <summary>Returns a string that represents the current object.</summary>
			/// <returns>A string that represents the current object.</returns>
			// Token: 0x0600647F RID: 25727 RVA: 0x00175C88 File Offset: 0x00173E88
			public override string ToString()
			{
				if (this.Owner != null)
				{
					return "ControlAccessibleObject: Owner = " + this.Owner.ToString();
				}
				return "ControlAccessibleObject: Owner = null";
			}

			// Token: 0x04003982 RID: 14722
			private static IntPtr oleAccAvailable = NativeMethods.InvalidIntPtr;

			// Token: 0x04003983 RID: 14723
			private IntPtr handle = IntPtr.Zero;

			// Token: 0x04003984 RID: 14724
			private Control ownerControl;

			// Token: 0x04003985 RID: 14725
			private int[] runtimeId;
		}

		// Token: 0x0200063D RID: 1597
		internal sealed class FontHandleWrapper : MarshalByRefObject, IDisposable
		{
			// Token: 0x06006481 RID: 25729 RVA: 0x00175CB9 File Offset: 0x00173EB9
			internal FontHandleWrapper(Font font)
			{
				this.handle = font.ToHfont();
				System.Internal.HandleCollector.Add(this.handle, NativeMethods.CommonHandles.GDI);
			}

			// Token: 0x17001589 RID: 5513
			// (get) Token: 0x06006482 RID: 25730 RVA: 0x00175CDE File Offset: 0x00173EDE
			internal IntPtr Handle
			{
				get
				{
					return this.handle;
				}
			}

			// Token: 0x06006483 RID: 25731 RVA: 0x00175CE6 File Offset: 0x00173EE6
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06006484 RID: 25732 RVA: 0x00175CF5 File Offset: 0x00173EF5
			private void Dispose(bool disposing)
			{
				if (this.handle != IntPtr.Zero)
				{
					SafeNativeMethods.DeleteObject(new HandleRef(this, this.handle));
					this.handle = IntPtr.Zero;
				}
			}

			// Token: 0x06006485 RID: 25733 RVA: 0x00175D28 File Offset: 0x00173F28
			~FontHandleWrapper()
			{
				this.Dispose(false);
			}

			// Token: 0x04003986 RID: 14726
			private IntPtr handle;
		}

		// Token: 0x0200063E RID: 1598
		private class ThreadMethodEntry : IAsyncResult
		{
			// Token: 0x06006486 RID: 25734 RVA: 0x00175D58 File Offset: 0x00173F58
			internal ThreadMethodEntry(Control caller, Control marshaler, Delegate method, object[] args, bool synchronous, ExecutionContext executionContext)
			{
				this.caller = caller;
				this.marshaler = marshaler;
				this.method = method;
				this.args = args;
				this.exception = null;
				this.retVal = null;
				this.synchronous = synchronous;
				this.isCompleted = false;
				this.resetEvent = null;
				this.executionContext = executionContext;
			}

			// Token: 0x06006487 RID: 25735 RVA: 0x00175DC0 File Offset: 0x00173FC0
			~ThreadMethodEntry()
			{
				if (this.resetEvent != null)
				{
					this.resetEvent.Close();
				}
			}

			// Token: 0x1700158A RID: 5514
			// (get) Token: 0x06006488 RID: 25736 RVA: 0x00015C90 File Offset: 0x00013E90
			public object AsyncState
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700158B RID: 5515
			// (get) Token: 0x06006489 RID: 25737 RVA: 0x00175DFC File Offset: 0x00173FFC
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					if (this.resetEvent == null)
					{
						object obj = this.invokeSyncObject;
						lock (obj)
						{
							if (this.resetEvent == null)
							{
								this.resetEvent = new ManualResetEvent(false);
								if (this.isCompleted)
								{
									this.resetEvent.Set();
								}
							}
						}
					}
					return this.resetEvent;
				}
			}

			// Token: 0x1700158C RID: 5516
			// (get) Token: 0x0600648A RID: 25738 RVA: 0x00175E6C File Offset: 0x0017406C
			public bool CompletedSynchronously
			{
				get
				{
					return this.isCompleted && this.synchronous;
				}
			}

			// Token: 0x1700158D RID: 5517
			// (get) Token: 0x0600648B RID: 25739 RVA: 0x00175E81 File Offset: 0x00174081
			public bool IsCompleted
			{
				get
				{
					return this.isCompleted;
				}
			}

			// Token: 0x0600648C RID: 25740 RVA: 0x00175E8C File Offset: 0x0017408C
			internal void Complete()
			{
				object obj = this.invokeSyncObject;
				lock (obj)
				{
					this.isCompleted = true;
					if (this.resetEvent != null)
					{
						this.resetEvent.Set();
					}
				}
			}

			// Token: 0x04003987 RID: 14727
			internal Control caller;

			// Token: 0x04003988 RID: 14728
			internal Control marshaler;

			// Token: 0x04003989 RID: 14729
			internal Delegate method;

			// Token: 0x0400398A RID: 14730
			internal object[] args;

			// Token: 0x0400398B RID: 14731
			internal object retVal;

			// Token: 0x0400398C RID: 14732
			internal Exception exception;

			// Token: 0x0400398D RID: 14733
			internal bool synchronous;

			// Token: 0x0400398E RID: 14734
			private bool isCompleted;

			// Token: 0x0400398F RID: 14735
			private ManualResetEvent resetEvent;

			// Token: 0x04003990 RID: 14736
			private object invokeSyncObject = new object();

			// Token: 0x04003991 RID: 14737
			internal ExecutionContext executionContext;

			// Token: 0x04003992 RID: 14738
			internal SynchronizationContext syncContext;
		}

		// Token: 0x0200063F RID: 1599
		private class ControlVersionInfo
		{
			// Token: 0x0600648D RID: 25741 RVA: 0x00175EE4 File Offset: 0x001740E4
			internal ControlVersionInfo(Control owner)
			{
				this.owner = owner;
			}

			// Token: 0x1700158E RID: 5518
			// (get) Token: 0x0600648E RID: 25742 RVA: 0x00175EF4 File Offset: 0x001740F4
			internal string CompanyName
			{
				get
				{
					if (this.companyName == null)
					{
						object[] customAttributes = this.owner.GetType().Module.Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
						if (customAttributes != null && customAttributes.Length != 0)
						{
							this.companyName = ((AssemblyCompanyAttribute)customAttributes[0]).Company;
						}
						if (this.companyName == null || this.companyName.Length == 0)
						{
							this.companyName = this.GetFileVersionInfo().CompanyName;
							if (this.companyName != null)
							{
								this.companyName = this.companyName.Trim();
							}
						}
						if (this.companyName == null || this.companyName.Length == 0)
						{
							string text = this.owner.GetType().Namespace;
							if (text == null)
							{
								text = string.Empty;
							}
							int num = text.IndexOf("/");
							if (num != -1)
							{
								this.companyName = text.Substring(0, num);
							}
							else
							{
								this.companyName = text;
							}
						}
					}
					return this.companyName;
				}
			}

			// Token: 0x1700158F RID: 5519
			// (get) Token: 0x0600648F RID: 25743 RVA: 0x00175FE8 File Offset: 0x001741E8
			internal string ProductName
			{
				get
				{
					if (this.productName == null)
					{
						object[] customAttributes = this.owner.GetType().Module.Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
						if (customAttributes != null && customAttributes.Length != 0)
						{
							this.productName = ((AssemblyProductAttribute)customAttributes[0]).Product;
						}
						if (this.productName == null || this.productName.Length == 0)
						{
							this.productName = this.GetFileVersionInfo().ProductName;
							if (this.productName != null)
							{
								this.productName = this.productName.Trim();
							}
						}
						if (this.productName == null || this.productName.Length == 0)
						{
							string text = this.owner.GetType().Namespace;
							if (text == null)
							{
								text = string.Empty;
							}
							int num = text.IndexOf(".");
							if (num != -1)
							{
								this.productName = text.Substring(num + 1);
							}
							else
							{
								this.productName = text;
							}
						}
					}
					return this.productName;
				}
			}

			// Token: 0x17001590 RID: 5520
			// (get) Token: 0x06006490 RID: 25744 RVA: 0x001760DC File Offset: 0x001742DC
			internal string ProductVersion
			{
				get
				{
					if (this.productVersion == null)
					{
						object[] customAttributes = this.owner.GetType().Module.Assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
						if (customAttributes != null && customAttributes.Length != 0)
						{
							this.productVersion = ((AssemblyInformationalVersionAttribute)customAttributes[0]).InformationalVersion;
						}
						if (this.productVersion == null || this.productVersion.Length == 0)
						{
							this.productVersion = this.GetFileVersionInfo().ProductVersion;
							if (this.productVersion != null)
							{
								this.productVersion = this.productVersion.Trim();
							}
						}
						if (this.productVersion == null || this.productVersion.Length == 0)
						{
							this.productVersion = "1.0.0.0";
						}
					}
					return this.productVersion;
				}
			}

			// Token: 0x06006491 RID: 25745 RVA: 0x0017619C File Offset: 0x0017439C
			private FileVersionInfo GetFileVersionInfo()
			{
				if (this.versionInfo == null)
				{
					new FileIOPermission(PermissionState.None)
					{
						AllFiles = FileIOPermissionAccess.PathDiscovery
					}.Assert();
					string fullyQualifiedName;
					try
					{
						fullyQualifiedName = this.owner.GetType().Module.FullyQualifiedName;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					new FileIOPermission(FileIOPermissionAccess.Read, fullyQualifiedName).Assert();
					try
					{
						this.versionInfo = FileVersionInfo.GetVersionInfo(fullyQualifiedName);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				return this.versionInfo;
			}

			// Token: 0x04003993 RID: 14739
			private string companyName;

			// Token: 0x04003994 RID: 14740
			private string productName;

			// Token: 0x04003995 RID: 14741
			private string productVersion;

			// Token: 0x04003996 RID: 14742
			private FileVersionInfo versionInfo;

			// Token: 0x04003997 RID: 14743
			private Control owner;
		}

		// Token: 0x02000640 RID: 1600
		private sealed class MultithreadSafeCallScope : IDisposable
		{
			// Token: 0x06006492 RID: 25746 RVA: 0x00176228 File Offset: 0x00174428
			internal MultithreadSafeCallScope()
			{
				if (Control.checkForIllegalCrossThreadCalls && !Control.inCrossThreadSafeCall)
				{
					Control.inCrossThreadSafeCall = true;
					this.resultedInSet = true;
					return;
				}
				this.resultedInSet = false;
			}

			// Token: 0x06006493 RID: 25747 RVA: 0x00176253 File Offset: 0x00174453
			void IDisposable.Dispose()
			{
				if (this.resultedInSet)
				{
					Control.inCrossThreadSafeCall = false;
				}
			}

			// Token: 0x04003998 RID: 14744
			private bool resultedInSet;
		}

		// Token: 0x02000641 RID: 1601
		private sealed class PrintPaintEventArgs : PaintEventArgs
		{
			// Token: 0x06006494 RID: 25748 RVA: 0x00176263 File Offset: 0x00174463
			internal PrintPaintEventArgs(Message m, IntPtr dc, Rectangle clipRect)
				: base(dc, clipRect)
			{
				this.m = m;
			}

			// Token: 0x17001591 RID: 5521
			// (get) Token: 0x06006495 RID: 25749 RVA: 0x00176274 File Offset: 0x00174474
			internal Message Message
			{
				get
				{
					return this.m;
				}
			}

			// Token: 0x04003999 RID: 14745
			private Message m;
		}
	}
}
