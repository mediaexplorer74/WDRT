using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents a small rectangular pop-up window that displays a brief description of a control's purpose when the user rests the pointer on the control.</summary>
	// Token: 0x0200040A RID: 1034
	[ProvideProperty("ToolTip", typeof(Control))]
	[DefaultEvent("Popup")]
	[ToolboxItemFilter("System.Windows.Forms")]
	[SRDescription("DescriptionToolTip")]
	public class ToolTip : Component, IExtenderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolTip" /> class with a specified container.</summary>
		/// <param name="cont">An <see cref="T:System.ComponentModel.IContainer" /> that represents the container of the <see cref="T:System.Windows.Forms.ToolTip" />.</param>
		// Token: 0x0600476A RID: 18282 RVA: 0x0012BF85 File Offset: 0x0012A185
		public ToolTip(IContainer cont)
			: this()
		{
			if (cont == null)
			{
				throw new ArgumentNullException("cont");
			}
			cont.Add(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolTip" /> without a specified container.</summary>
		// Token: 0x0600476B RID: 18283 RVA: 0x0012BFA4 File Offset: 0x0012A1A4
		public ToolTip()
		{
			this.window = new ToolTip.ToolTipNativeWindow(this);
			this.auto = true;
			this.delayTimes[0] = 500;
			this.AdjustBaseFromAuto();
			this.IsPersistent = OsVersion.IsWindows11_OrGreater;
		}

		/// <summary>Gets or sets a value indicating whether the ToolTip is currently active.</summary>
		/// <returns>
		///   <see langword="true" /> if the ToolTip is currently active; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x0600476C RID: 18284 RVA: 0x0012C052 File Offset: 0x0012A252
		// (set) Token: 0x0600476D RID: 18285 RVA: 0x0012C05C File Offset: 0x0012A25C
		[SRDescription("ToolTipActiveDescr")]
		[DefaultValue(true)]
		public bool Active
		{
			get
			{
				return this.active;
			}
			set
			{
				if (this.active != value)
				{
					this.active = value;
					if (!base.DesignMode && this.GetHandleCreated())
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1025, value ? 1 : 0, 0);
					}
				}
			}
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x0012C0A8 File Offset: 0x0012A2A8
		internal void HideToolTip(IKeyboardToolTip currentTool)
		{
			this.Hide(currentTool.GetOwnerWindow());
		}

		/// <summary>Gets or sets the automatic delay for the ToolTip.</summary>
		/// <returns>The automatic delay, in milliseconds. The default is 500.</returns>
		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x0600476F RID: 18287 RVA: 0x0012C0B6 File Offset: 0x0012A2B6
		// (set) Token: 0x06004770 RID: 18288 RVA: 0x0012C0C0 File Offset: 0x0012A2C0
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("ToolTipAutomaticDelayDescr")]
		[DefaultValue(500)]
		public int AutomaticDelay
		{
			get
			{
				return this.delayTimes[0];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("AutomaticDelay", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"AutomaticDelay",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.SetDelayTime(0, value);
			}
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x0012C11C File Offset: 0x0012A31C
		internal string GetCaptionForTool(Control tool)
		{
			ToolTip.TipInfo tipInfo = (ToolTip.TipInfo)this.tools[tool];
			if (tipInfo == null)
			{
				return null;
			}
			return tipInfo.Caption;
		}

		/// <summary>Gets or sets the period of time the ToolTip remains visible if the pointer is stationary on a control with specified ToolTip text.</summary>
		/// <returns>The period of time, in milliseconds, that the <see cref="T:System.Windows.Forms.ToolTip" /> remains visible when the pointer is stationary on a control. The default value is 5000.</returns>
		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06004772 RID: 18290 RVA: 0x0012C13A File Offset: 0x0012A33A
		// (set) Token: 0x06004773 RID: 18291 RVA: 0x0012C144 File Offset: 0x0012A344
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("ToolTipAutoPopDelayDescr")]
		public int AutoPopDelay
		{
			get
			{
				return this.delayTimes[2];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("AutoPopDelay", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"AutoPopDelay",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.SetDelayTime(2, value);
			}
		}

		/// <summary>Gets or sets the background color for the ToolTip.</summary>
		/// <returns>The background <see cref="T:System.Drawing.Color" />.</returns>
		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06004774 RID: 18292 RVA: 0x0012C1A0 File Offset: 0x0012A3A0
		// (set) Token: 0x06004775 RID: 18293 RVA: 0x0012C1A8 File Offset: 0x0012A3A8
		[SRDescription("ToolTipBackColorDescr")]
		[DefaultValue(typeof(Color), "Info")]
		public Color BackColor
		{
			get
			{
				return this.backColor;
			}
			set
			{
				this.backColor = value;
				if (this.GetHandleCreated())
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1043, ColorTranslator.ToWin32(this.backColor), 0);
				}
			}
		}

		/// <summary>Gets the creation parameters for the ToolTip window.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> containing the information needed to create the ToolTip.</returns>
		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06004776 RID: 18294 RVA: 0x0012C1DC File Offset: 0x0012A3DC
		protected virtual CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = new CreateParams();
				if (this.TopLevelControl != null && !this.TopLevelControl.IsDisposed)
				{
					createParams.Parent = this.TopLevelControl.Handle;
				}
				createParams.ClassName = "tooltips_class32";
				if (this.showAlways)
				{
					createParams.Style = 1;
				}
				if (this.isBalloon)
				{
					createParams.Style |= 64;
				}
				if (!this.stripAmpersands)
				{
					createParams.Style |= 2;
				}
				if (!this.useAnimation)
				{
					createParams.Style |= 16;
				}
				if (!this.useFading)
				{
					createParams.Style |= 32;
				}
				createParams.ExStyle = 0;
				createParams.Caption = null;
				return createParams;
			}
		}

		/// <summary>Gets or sets the foreground color for the ToolTip.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" />.</returns>
		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06004777 RID: 18295 RVA: 0x0012C299 File Offset: 0x0012A499
		// (set) Token: 0x06004778 RID: 18296 RVA: 0x0012C2A4 File Offset: 0x0012A4A4
		[SRDescription("ToolTipForeColorDescr")]
		[DefaultValue(typeof(Color), "InfoText")]
		public Color ForeColor
		{
			get
			{
				return this.foreColor;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("ToolTipEmptyColor", new object[] { "ForeColor" }));
				}
				this.foreColor = value;
				if (this.GetHandleCreated())
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1044, ColorTranslator.ToWin32(this.foreColor), 0);
				}
			}
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06004779 RID: 18297 RVA: 0x0012C30A File Offset: 0x0012A50A
		internal IntPtr Handle
		{
			get
			{
				if (!this.GetHandleCreated())
				{
					this.CreateHandle();
				}
				return this.window.Handle;
			}
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x0600477A RID: 18298 RVA: 0x0012C328 File Offset: 0x0012A528
		private bool HasAllWindowsPermission
		{
			get
			{
				try
				{
					IntSecurity.AllWindows.Demand();
					return true;
				}
				catch (SecurityException)
				{
				}
				return false;
			}
		}

		/// <summary>Gets or sets a value indicating whether the ToolTip should use a balloon window.</summary>
		/// <returns>
		///   <see langword="true" /> if a balloon window should be used; otherwise, <see langword="false" /> if a standard rectangular window should be used. The default is <see langword="false" />.</returns>
		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x0600477B RID: 18299 RVA: 0x0012C35C File Offset: 0x0012A55C
		// (set) Token: 0x0600477C RID: 18300 RVA: 0x0012C364 File Offset: 0x0012A564
		[SRDescription("ToolTipIsBalloonDescr")]
		[DefaultValue(false)]
		public bool IsBalloon
		{
			get
			{
				return this.isBalloon;
			}
			set
			{
				if (this.isBalloon != value)
				{
					this.isBalloon = value;
					if (this.GetHandleCreated())
					{
						this.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x0012C384 File Offset: 0x0012A584
		private bool IsWindowActive(IWin32Window window)
		{
			Control control = window as Control;
			if (control != null && (control.ShowParams & 15) != 4)
			{
				IntPtr activeWindow = UnsafeNativeMethods.GetActiveWindow();
				IntPtr ancestor = UnsafeNativeMethods.GetAncestor(new HandleRef(window, window.Handle), 2);
				if (activeWindow != ancestor)
				{
					ToolTip.TipInfo tipInfo = (ToolTip.TipInfo)this.tools[control];
					if (tipInfo != null && (tipInfo.TipType & ToolTip.TipInfo.Type.SemiAbsolute) != ToolTip.TipInfo.Type.None)
					{
						this.tools.Remove(control);
						this.DestroyRegion(control);
					}
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets or sets the time that passes before the ToolTip appears.</summary>
		/// <returns>The period of time, in milliseconds, that the pointer must remain stationary on a control before the ToolTip window is displayed.</returns>
		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x0600477E RID: 18302 RVA: 0x0012C3FE File Offset: 0x0012A5FE
		// (set) Token: 0x0600477F RID: 18303 RVA: 0x0012C408 File Offset: 0x0012A608
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("ToolTipInitialDelayDescr")]
		public int InitialDelay
		{
			get
			{
				return this.delayTimes[3];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("InitialDelay", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"InitialDelay",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.SetDelayTime(3, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the ToolTip is drawn by the operating system or by code that you provide.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolTip" /> is drawn by code that you provide; <see langword="false" /> if the <see cref="T:System.Windows.Forms.ToolTip" /> is drawn by the operating system. The default is <see langword="false" />.</returns>
		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06004780 RID: 18304 RVA: 0x0012C464 File Offset: 0x0012A664
		// (set) Token: 0x06004781 RID: 18305 RVA: 0x0012C46C File Offset: 0x0012A66C
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ToolTipOwnerDrawDescr")]
		public bool OwnerDraw
		{
			get
			{
				return this.ownerDraw;
			}
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			set
			{
				this.ownerDraw = value;
			}
		}

		/// <summary>Gets or sets the length of time that must transpire before subsequent ToolTip windows appear as the pointer moves from one control to another.</summary>
		/// <returns>The length of time, in milliseconds, that it takes subsequent ToolTip windows to appear.</returns>
		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06004782 RID: 18306 RVA: 0x0012C475 File Offset: 0x0012A675
		// (set) Token: 0x06004783 RID: 18307 RVA: 0x0012C480 File Offset: 0x0012A680
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("ToolTipReshowDelayDescr")]
		public int ReshowDelay
		{
			get
			{
				return this.delayTimes[1];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("ReshowDelay", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"ReshowDelay",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.SetDelayTime(1, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether a ToolTip window is displayed, even when its parent control is not active.</summary>
		/// <returns>
		///   <see langword="true" /> if the ToolTip is always displayed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06004784 RID: 18308 RVA: 0x0012C4DC File Offset: 0x0012A6DC
		// (set) Token: 0x06004785 RID: 18309 RVA: 0x0012C4E4 File Offset: 0x0012A6E4
		[DefaultValue(false)]
		[SRDescription("ToolTipShowAlwaysDescr")]
		public bool ShowAlways
		{
			get
			{
				return this.showAlways;
			}
			set
			{
				if (this.showAlways != value)
				{
					this.showAlways = value;
					if (this.GetHandleCreated())
					{
						this.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets or sets a value that determines how ampersand (&amp;) characters are treated.</summary>
		/// <returns>
		///   <see langword="true" /> if ampersand characters are stripped from the ToolTip text; otherwise, <see langword="false" />. The default is false.</returns>
		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06004786 RID: 18310 RVA: 0x0012C504 File Offset: 0x0012A704
		// (set) Token: 0x06004787 RID: 18311 RVA: 0x0012C50C File Offset: 0x0012A70C
		[SRDescription("ToolTipStripAmpersandsDescr")]
		[Browsable(true)]
		[DefaultValue(false)]
		public bool StripAmpersands
		{
			get
			{
				return this.stripAmpersands;
			}
			set
			{
				if (this.stripAmpersands != value)
				{
					this.stripAmpersands = value;
					if (this.GetHandleCreated())
					{
						this.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets or sets the object that contains programmer-supplied data associated with the <see cref="T:System.Windows.Forms.ToolTip" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.ToolTip" />. The default is <see langword="null" />.</returns>
		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x06004788 RID: 18312 RVA: 0x0012C52C File Offset: 0x0012A72C
		// (set) Token: 0x06004789 RID: 18313 RVA: 0x0012C534 File Offset: 0x0012A734
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
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		/// <summary>Gets or sets a value that defines the type of icon to be displayed alongside the ToolTip text.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolTipIcon" /> enumerated values.</returns>
		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x0600478A RID: 18314 RVA: 0x0012C53D File Offset: 0x0012A73D
		// (set) Token: 0x0600478B RID: 18315 RVA: 0x0012C548 File Offset: 0x0012A748
		[DefaultValue(ToolTipIcon.None)]
		[SRDescription("ToolTipToolTipIconDescr")]
		public ToolTipIcon ToolTipIcon
		{
			get
			{
				return this.toolTipIcon;
			}
			set
			{
				if (this.toolTipIcon != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolTipIcon));
					}
					this.toolTipIcon = value;
					if (this.toolTipIcon > ToolTipIcon.None && this.GetHandleCreated())
					{
						string text = ((!string.IsNullOrEmpty(this.toolTipTitle)) ? this.toolTipTitle : " ");
						UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_SETTITLE, (int)this.toolTipIcon, text);
						UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1053, 0, 0);
					}
				}
			}
		}

		/// <summary>Gets or sets a title for the ToolTip window.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the window title.</returns>
		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x0600478C RID: 18316 RVA: 0x0012C5F1 File Offset: 0x0012A7F1
		// (set) Token: 0x0600478D RID: 18317 RVA: 0x0012C5FC File Offset: 0x0012A7FC
		[DefaultValue("")]
		[SRDescription("ToolTipTitleDescr")]
		public string ToolTipTitle
		{
			get
			{
				return this.toolTipTitle;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this.toolTipTitle != value)
				{
					this.toolTipTitle = value;
					if (this.GetHandleCreated())
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_SETTITLE, (int)this.toolTipIcon, this.toolTipTitle);
						UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1053, 0, 0);
					}
				}
			}
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x0600478E RID: 18318 RVA: 0x0012C66C File Offset: 0x0012A86C
		private Control TopLevelControl
		{
			get
			{
				Control control = null;
				if (this.topLevelControl == null)
				{
					Control[] array = new Control[this.tools.Keys.Count];
					this.tools.Keys.CopyTo(array, 0);
					if (array != null && array.Length != 0)
					{
						foreach (Control control2 in array)
						{
							control = control2.TopLevelControlInternal;
							if (control != null)
							{
								break;
							}
							if (control2.IsActiveX)
							{
								control = control2;
								break;
							}
							if (control == null && control2 != null && control2.ParentInternal != null)
							{
								while (control2.ParentInternal != null)
								{
									control2 = control2.ParentInternal;
								}
								control = control2;
								if (control != null)
								{
									break;
								}
							}
						}
					}
					this.topLevelControl = control;
					if (control != null)
					{
						control.HandleCreated += this.TopLevelCreated;
						control.HandleDestroyed += this.TopLevelDestroyed;
						if (control.IsHandleCreated)
						{
							this.TopLevelCreated(control, EventArgs.Empty);
						}
						control.ParentChanged += this.OnTopLevelPropertyChanged;
					}
				}
				else
				{
					control = this.topLevelControl;
				}
				return control;
			}
		}

		/// <summary>Gets or sets a value determining whether an animation effect should be used when displaying the ToolTip.</summary>
		/// <returns>
		///   <see langword="true" /> if window animation should be used; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x0600478F RID: 18319 RVA: 0x0012C75F File Offset: 0x0012A95F
		// (set) Token: 0x06004790 RID: 18320 RVA: 0x0012C767 File Offset: 0x0012A967
		[SRDescription("ToolTipUseAnimationDescr")]
		[Browsable(true)]
		[DefaultValue(true)]
		public bool UseAnimation
		{
			get
			{
				return this.useAnimation;
			}
			set
			{
				if (this.useAnimation != value)
				{
					this.useAnimation = value;
					if (this.GetHandleCreated())
					{
						this.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets or sets a value determining whether a fade effect should be used when displaying the ToolTip.</summary>
		/// <returns>
		///   <see langword="true" /> if window fading should be used; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06004791 RID: 18321 RVA: 0x0012C787 File Offset: 0x0012A987
		// (set) Token: 0x06004792 RID: 18322 RVA: 0x0012C78F File Offset: 0x0012A98F
		[SRDescription("ToolTipUseFadingDescr")]
		[Browsable(true)]
		[DefaultValue(true)]
		public bool UseFading
		{
			get
			{
				return this.useFading;
			}
			set
			{
				if (this.useFading != value)
				{
					this.useFading = value;
					if (this.GetHandleCreated())
					{
						this.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Occurs when the ToolTip is drawn and the <see cref="P:System.Windows.Forms.ToolTip.OwnerDraw" /> property is set to <see langword="true" /> and the <see cref="P:System.Windows.Forms.ToolTip.IsBalloon" /> property is <see langword="false" />.</summary>
		// Token: 0x1400038E RID: 910
		// (add) Token: 0x06004793 RID: 18323 RVA: 0x0012C7AF File Offset: 0x0012A9AF
		// (remove) Token: 0x06004794 RID: 18324 RVA: 0x0012C7C8 File Offset: 0x0012A9C8
		[SRCategory("CatBehavior")]
		[SRDescription("ToolTipDrawEventDescr")]
		public event DrawToolTipEventHandler Draw
		{
			add
			{
				this.onDraw = (DrawToolTipEventHandler)Delegate.Combine(this.onDraw, value);
			}
			remove
			{
				this.onDraw = (DrawToolTipEventHandler)Delegate.Remove(this.onDraw, value);
			}
		}

		/// <summary>Occurs before a ToolTip is initially displayed. This is the default event for the <see cref="T:System.Windows.Forms.ToolTip" /> class.</summary>
		// Token: 0x1400038F RID: 911
		// (add) Token: 0x06004795 RID: 18325 RVA: 0x0012C7E1 File Offset: 0x0012A9E1
		// (remove) Token: 0x06004796 RID: 18326 RVA: 0x0012C7FA File Offset: 0x0012A9FA
		[SRCategory("CatBehavior")]
		[SRDescription("ToolTipPopupEventDescr")]
		public event PopupEventHandler Popup
		{
			add
			{
				this.onPopup = (PopupEventHandler)Delegate.Combine(this.onPopup, value);
			}
			remove
			{
				this.onPopup = (PopupEventHandler)Delegate.Remove(this.onPopup, value);
			}
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x0012C813 File Offset: 0x0012AA13
		private void AdjustBaseFromAuto()
		{
			this.delayTimes[1] = this.delayTimes[0] / 5;
			this.delayTimes[2] = this.delayTimes[0] * 10;
			this.delayTimes[3] = this.delayTimes[0];
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x0012C84C File Offset: 0x0012AA4C
		private void HandleCreated(object sender, EventArgs eventargs)
		{
			this.ClearTopLevelControlEvents();
			this.topLevelControl = null;
			Control control = (Control)sender;
			this.CreateRegion(control);
			this.CheckNativeToolTip(control);
			this.CheckCompositeControls(control);
			if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
			{
				KeyboardToolTipStateMachine.Instance.Hook(control, this);
			}
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x0012C898 File Offset: 0x0012AA98
		private void CheckNativeToolTip(Control associatedControl)
		{
			if (!this.GetHandleCreated())
			{
				return;
			}
			TreeView treeView = associatedControl as TreeView;
			if (treeView != null && treeView.ShowNodeToolTips)
			{
				treeView.SetToolTip(this, this.GetToolTip(associatedControl));
			}
			if (associatedControl is ToolBar)
			{
				((ToolBar)associatedControl).SetToolTip(this);
			}
			TabControl tabControl = associatedControl as TabControl;
			if (tabControl != null && tabControl.ShowToolTips)
			{
				tabControl.SetToolTip(this, this.GetToolTip(associatedControl));
			}
			if (associatedControl is ListView)
			{
				((ListView)associatedControl).SetToolTip(this, this.GetToolTip(associatedControl));
			}
			if (associatedControl is StatusBar)
			{
				((StatusBar)associatedControl).SetToolTip(this);
			}
			if (associatedControl is Label)
			{
				((Label)associatedControl).SetToolTip(this);
			}
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x0012C945 File Offset: 0x0012AB45
		private void CheckCompositeControls(Control associatedControl)
		{
			if (associatedControl is UpDownBase)
			{
				((UpDownBase)associatedControl).SetToolTip(this, this.GetToolTip(associatedControl));
			}
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x0012C964 File Offset: 0x0012AB64
		private void HandleDestroyed(object sender, EventArgs eventargs)
		{
			Control control = (Control)sender;
			this.DestroyRegion(control);
			if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
			{
				KeyboardToolTipStateMachine.Instance.Unhook(control, this);
			}
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x0012C992 File Offset: 0x0012AB92
		private void OnDraw(DrawToolTipEventArgs e)
		{
			if (this.onDraw != null)
			{
				this.onDraw(this, e);
			}
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x0012C9A9 File Offset: 0x0012ABA9
		private void OnPopup(PopupEventArgs e)
		{
			if (this.onPopup != null)
			{
				this.onPopup(this, e);
			}
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x0012C9C0 File Offset: 0x0012ABC0
		private void TopLevelCreated(object sender, EventArgs eventargs)
		{
			this.CreateHandle();
			this.CreateAllRegions();
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x0012C9CE File Offset: 0x0012ABCE
		private void TopLevelDestroyed(object sender, EventArgs eventargs)
		{
			this.DestoyAllRegions();
			this.DestroyHandle();
		}

		/// <summary>Returns <see langword="true" /> if the ToolTip can offer an extender property to the specified target component.</summary>
		/// <param name="target">The target object to add an extender property to.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolTip" /> class can offer one or more extender properties; otherwise, <see langword="false" />.</returns>
		// Token: 0x060047A0 RID: 18336 RVA: 0x0012C9DC File Offset: 0x0012ABDC
		public bool CanExtend(object target)
		{
			return target is Control && !(target is ToolTip);
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x0012C9F4 File Offset: 0x0012ABF4
		private void ClearTopLevelControlEvents()
		{
			if (this.topLevelControl != null)
			{
				this.topLevelControl.ParentChanged -= this.OnTopLevelPropertyChanged;
				this.topLevelControl.HandleCreated -= this.TopLevelCreated;
				this.topLevelControl.HandleDestroyed -= this.TopLevelDestroyed;
			}
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x0012CA50 File Offset: 0x0012AC50
		private void CreateHandle()
		{
			if (this.GetHandleCreated())
			{
				return;
			}
			IntPtr intPtr = UnsafeNativeMethods.ThemingScope.Activate();
			try
			{
				SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
				{
					dwICC = 8
				});
				CreateParams createParams = this.CreateParams;
				if (this.GetHandleCreated())
				{
					return;
				}
				this.window.CreateHandle(createParams);
			}
			finally
			{
				UnsafeNativeMethods.ThemingScope.Deactivate(intPtr);
			}
			if (this.ownerDraw)
			{
				int num = (int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, this.Handle), -16);
				num &= -8388609;
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, this.Handle), -16, new HandleRef(null, (IntPtr)num));
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
			if (this.auto)
			{
				if (this.delayTimes[0] != 500)
				{
					this.SetDelayTime(0, this.delayTimes[0]);
				}
				else
				{
					this.delayTimes[2] = this.GetDelayTime(2);
					this.delayTimes[3] = this.GetDelayTime(3);
					this.delayTimes[1] = this.GetDelayTime(1);
				}
			}
			else
			{
				int num2 = this.delayTimes[2];
				if (num2 >= 1 && num2 != 5000)
				{
					this.SetDelayTime(2, num2);
				}
				num2 = this.delayTimes[3];
				if (num2 >= 1)
				{
					this.SetDelayTime(3, num2);
				}
				num2 = this.delayTimes[1];
				if (num2 >= 1)
				{
					this.SetDelayTime(1, num2);
				}
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1025, this.active ? 1 : 0, 0);
			if (this.BackColor != SystemColors.Info)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1043, ColorTranslator.ToWin32(this.BackColor), 0);
			}
			if (this.ForeColor != SystemColors.InfoText)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1044, ColorTranslator.ToWin32(this.ForeColor), 0);
			}
			if (this.toolTipIcon > ToolTipIcon.None || !string.IsNullOrEmpty(this.toolTipTitle))
			{
				string text = ((!string.IsNullOrEmpty(this.toolTipTitle)) ? this.toolTipTitle : " ");
				UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_SETTITLE, (int)this.toolTipIcon, text);
			}
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x0012CCB4 File Offset: 0x0012AEB4
		private void CreateAllRegions()
		{
			Control[] array = new Control[this.tools.Keys.Count];
			this.tools.Keys.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] is DataGridView)
				{
					return;
				}
				this.CreateRegion(array[i]);
			}
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x0012CD0C File Offset: 0x0012AF0C
		private void DestoyAllRegions()
		{
			Control[] array = new Control[this.tools.Keys.Count];
			this.tools.Keys.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] is DataGridView)
				{
					return;
				}
				this.DestroyRegion(array[i]);
			}
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x0012CD64 File Offset: 0x0012AF64
		private void SetToolInfo(Control ctl, string caption)
		{
			bool flag;
			NativeMethods.TOOLINFO_TOOLTIP toolinfo = this.GetTOOLINFO(ctl, caption, out flag);
			try
			{
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_ADDTOOL, 0, toolinfo);
				if (ctl is TreeView || ctl is ListView)
				{
					TreeView treeView = ctl as TreeView;
					if (treeView != null && treeView.ShowNodeToolTips)
					{
						return;
					}
					ListView listView = ctl as ListView;
					if (listView != null && listView.ShowItemToolTips)
					{
						return;
					}
				}
				if (num == 0)
				{
					throw new InvalidOperationException(SR.GetString("ToolTipAddFailed"));
				}
			}
			finally
			{
				if (flag && IntPtr.Zero != toolinfo.lpszText)
				{
					Marshal.FreeHGlobal(toolinfo.lpszText);
				}
			}
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x0012CE1C File Offset: 0x0012B01C
		private void CreateRegion(Control ctl)
		{
			string toolTip = this.GetToolTip(ctl);
			bool flag = toolTip != null && toolTip.Length > 0;
			bool flag2 = ctl.IsHandleCreated && this.TopLevelControl != null && this.TopLevelControl.IsHandleCreated;
			if (!this.created.ContainsKey(ctl) && flag && flag2 && !base.DesignMode)
			{
				this.SetToolInfo(ctl, toolTip);
				this.created[ctl] = ctl;
			}
			if (ctl.IsHandleCreated && this.topLevelControl == null)
			{
				ctl.MouseMove -= this.MouseMove;
				ctl.MouseMove += this.MouseMove;
			}
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x0012CEC8 File Offset: 0x0012B0C8
		private void MouseMove(object sender, MouseEventArgs me)
		{
			Control control = (Control)sender;
			if (!this.created.ContainsKey(control) && control.IsHandleCreated && this.TopLevelControl != null)
			{
				this.CreateRegion(control);
			}
			if (this.created.ContainsKey(control))
			{
				control.MouseMove -= this.MouseMove;
			}
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x0012CF21 File Offset: 0x0012B121
		internal void DestroyHandle()
		{
			if (this.GetHandleCreated())
			{
				this.window.DestroyHandle();
			}
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x0012CF38 File Offset: 0x0012B138
		private void DestroyRegion(Control ctl)
		{
			bool flag = ctl.IsHandleCreated && this.topLevelControl != null && this.topLevelControl.IsHandleCreated && !this.isDisposing;
			Form form = this.topLevelControl as Form;
			if (form == null || (form != null && !form.Modal))
			{
				flag = flag && this.GetHandleCreated();
			}
			if (this.created.ContainsKey(ctl) && flag && !base.DesignMode)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_DELTOOL, 0, this.GetMinTOOLINFO(ctl));
				this.created.Remove(ctl);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060047AA RID: 18346 RVA: 0x0012CFDC File Offset: 0x0012B1DC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.isDisposing = true;
				try
				{
					this.ClearTopLevelControlEvents();
					this.StopTimer();
					this.DestroyHandle();
					this.RemoveAll();
					this.window = null;
					Form form = this.TopLevelControl as Form;
					if (form != null)
					{
						form.Deactivate -= this.BaseFormDeactivate;
					}
				}
				finally
				{
					this.isDisposing = false;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x0012D054 File Offset: 0x0012B254
		internal int GetDelayTime(int type)
		{
			if (this.GetHandleCreated())
			{
				return (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1045, type, 0);
			}
			return this.delayTimes[type];
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x0012D084 File Offset: 0x0012B284
		internal bool GetHandleCreated()
		{
			return this.window != null && this.window.Handle != IntPtr.Zero;
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x0012D0A5 File Offset: 0x0012B2A5
		private NativeMethods.TOOLINFO_TOOLTIP GetMinTOOLINFO(Control ctl)
		{
			return this.GetMinToolInfoForHandle(ctl.Handle);
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x0012D0B3 File Offset: 0x0012B2B3
		private NativeMethods.TOOLINFO_TOOLTIP GetMinToolInfoForTool(IWin32Window tool)
		{
			return this.GetMinToolInfoForHandle(tool.Handle);
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x0012D0C4 File Offset: 0x0012B2C4
		private NativeMethods.TOOLINFO_TOOLTIP GetMinToolInfoForHandle(IntPtr handle)
		{
			NativeMethods.TOOLINFO_TOOLTIP toolinfo_TOOLTIP = new NativeMethods.TOOLINFO_TOOLTIP();
			toolinfo_TOOLTIP.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_TOOLTIP));
			toolinfo_TOOLTIP.hwnd = handle;
			toolinfo_TOOLTIP.uFlags |= 1;
			toolinfo_TOOLTIP.uId = handle;
			return toolinfo_TOOLTIP;
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x0012D10C File Offset: 0x0012B30C
		private NativeMethods.TOOLINFO_TOOLTIP GetTOOLINFO(Control ctl, string caption, out bool allocatedString)
		{
			allocatedString = false;
			NativeMethods.TOOLINFO_TOOLTIP minTOOLINFO = this.GetMinTOOLINFO(ctl);
			minTOOLINFO.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_TOOLTIP));
			minTOOLINFO.uFlags |= 272;
			Control control = this.TopLevelControl;
			if (control != null && control.RightToLeft == RightToLeft.Yes && !ctl.IsMirrored)
			{
				minTOOLINFO.uFlags |= 4;
			}
			if (ctl is TreeView || ctl is ListView)
			{
				TreeView treeView = ctl as TreeView;
				if (treeView != null && treeView.ShowNodeToolTips)
				{
					minTOOLINFO.lpszText = NativeMethods.InvalidIntPtr;
				}
				else
				{
					ListView listView = ctl as ListView;
					if (listView != null && listView.ShowItemToolTips)
					{
						minTOOLINFO.lpszText = NativeMethods.InvalidIntPtr;
					}
					else
					{
						minTOOLINFO.lpszText = Marshal.StringToHGlobalAuto(caption);
						allocatedString = true;
					}
				}
			}
			else
			{
				minTOOLINFO.lpszText = Marshal.StringToHGlobalAuto(caption);
				allocatedString = true;
			}
			return minTOOLINFO;
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x0012D1E4 File Offset: 0x0012B3E4
		private NativeMethods.TOOLINFO_TOOLTIP GetWinTOOLINFO(IntPtr hWnd)
		{
			NativeMethods.TOOLINFO_TOOLTIP toolinfo_TOOLTIP = new NativeMethods.TOOLINFO_TOOLTIP();
			toolinfo_TOOLTIP.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_TOOLTIP));
			toolinfo_TOOLTIP.hwnd = hWnd;
			toolinfo_TOOLTIP.uFlags |= 273;
			Control control = this.TopLevelControl;
			if (control != null && control.RightToLeft == RightToLeft.Yes && ((int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, hWnd), -16) & 4194304) != 4194304)
			{
				toolinfo_TOOLTIP.uFlags |= 4;
			}
			toolinfo_TOOLTIP.uId = toolinfo_TOOLTIP.hwnd;
			return toolinfo_TOOLTIP;
		}

		/// <summary>Retrieves the ToolTip text associated with the specified control.</summary>
		/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> for which to retrieve the <see cref="T:System.Windows.Forms.ToolTip" /> text.</param>
		/// <returns>A <see cref="T:System.String" /> containing the ToolTip text for the specified control.</returns>
		// Token: 0x060047B2 RID: 18354 RVA: 0x0012D278 File Offset: 0x0012B478
		[DefaultValue("")]
		[Localizable(true)]
		[SRDescription("ToolTipToolTipDescr")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string GetToolTip(Control control)
		{
			if (control == null)
			{
				return string.Empty;
			}
			ToolTip.TipInfo tipInfo = (ToolTip.TipInfo)this.tools[control];
			if (tipInfo == null || tipInfo.Caption == null)
			{
				return "";
			}
			return tipInfo.Caption;
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x0012D2B8 File Offset: 0x0012B4B8
		private IntPtr GetWindowFromPoint(Point screenCoords, ref bool success)
		{
			Control control = this.TopLevelControl;
			if (control != null && control.IsActiveX)
			{
				IntPtr intPtr = UnsafeNativeMethods.WindowFromPoint(screenCoords.X, screenCoords.Y);
				if (intPtr != IntPtr.Zero)
				{
					Control control2 = Control.FromHandleInternal(intPtr);
					if (control2 != null && this.tools != null && this.tools.ContainsKey(control2))
					{
						return intPtr;
					}
				}
				return IntPtr.Zero;
			}
			IntPtr intPtr2 = IntPtr.Zero;
			if (control != null)
			{
				intPtr2 = control.Handle;
			}
			IntPtr intPtr3 = IntPtr.Zero;
			bool flag = false;
			while (!flag)
			{
				Point point = screenCoords;
				if (control != null)
				{
					point = control.PointToClientInternal(screenCoords);
				}
				IntPtr intPtr4 = UnsafeNativeMethods.ChildWindowFromPointEx(new HandleRef(null, intPtr2), point.X, point.Y, 1);
				if (intPtr4 == intPtr2)
				{
					intPtr3 = intPtr4;
					flag = true;
				}
				else if (intPtr4 == IntPtr.Zero)
				{
					flag = true;
				}
				else
				{
					control = Control.FromHandleInternal(intPtr4);
					if (control == null)
					{
						control = Control.FromChildHandleInternal(intPtr4);
						if (control != null)
						{
							intPtr3 = control.Handle;
						}
						flag = true;
					}
					else
					{
						intPtr2 = control.Handle;
					}
				}
			}
			if (intPtr3 != IntPtr.Zero)
			{
				Control control3 = Control.FromHandleInternal(intPtr3);
				if (control3 != null)
				{
					Control control4 = control3;
					while (control4 != null && control4.Visible)
					{
						control4 = control4.ParentInternal;
					}
					if (control4 != null)
					{
						intPtr3 = IntPtr.Zero;
					}
					success = true;
				}
			}
			return intPtr3;
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x0012D3FE File Offset: 0x0012B5FE
		private void OnTopLevelPropertyChanged(object s, EventArgs e)
		{
			this.ClearTopLevelControlEvents();
			this.topLevelControl = null;
			this.topLevelControl = this.TopLevelControl;
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x0012D419 File Offset: 0x0012B619
		private void RecreateHandle()
		{
			if (!base.DesignMode)
			{
				if (this.GetHandleCreated())
				{
					this.DestroyHandle();
				}
				this.created.Clear();
				this.CreateHandle();
				this.CreateAllRegions();
			}
		}

		/// <summary>Removes all ToolTip text currently associated with the ToolTip component.</summary>
		// Token: 0x060047B6 RID: 18358 RVA: 0x0012D448 File Offset: 0x0012B648
		public void RemoveAll()
		{
			Control[] array = new Control[this.tools.Keys.Count];
			this.tools.Keys.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].IsHandleCreated)
				{
					this.DestroyRegion(array[i]);
				}
				array[i].HandleCreated -= this.HandleCreated;
				array[i].HandleDestroyed -= this.HandleDestroyed;
				if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
				{
					KeyboardToolTipStateMachine.Instance.Unhook(array[i], this);
				}
			}
			this.created.Clear();
			this.tools.Clear();
			this.ClearTopLevelControlEvents();
			this.topLevelControl = null;
			if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
			{
				KeyboardToolTipStateMachine.Instance.ResetStateMachine(this);
			}
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x0012D510 File Offset: 0x0012B710
		private void SetDelayTime(int type, int time)
		{
			if (type == 0)
			{
				this.auto = true;
			}
			else
			{
				this.auto = false;
			}
			this.delayTimes[type] = time;
			if (this.GetHandleCreated() && time >= 0)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1027, type, time);
				if (type == 2 && time != 32767)
				{
					this.IsPersistent = false;
				}
				if (this.auto)
				{
					this.delayTimes[2] = this.GetDelayTime(2);
					this.delayTimes[3] = this.GetDelayTime(3);
					this.delayTimes[1] = this.GetDelayTime(1);
					return;
				}
			}
			else if (this.auto)
			{
				this.AdjustBaseFromAuto();
			}
		}

		/// <summary>Associates ToolTip text with the specified control.</summary>
		/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to associate the ToolTip text with.</param>
		/// <param name="caption">The ToolTip text to display when the pointer is on the control.</param>
		// Token: 0x060047B8 RID: 18360 RVA: 0x0012D5B8 File Offset: 0x0012B7B8
		public void SetToolTip(Control control, string caption)
		{
			ToolTip.TipInfo tipInfo = new ToolTip.TipInfo(caption, ToolTip.TipInfo.Type.Auto);
			this.SetToolTipInternal(control, tipInfo);
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x0012D5D8 File Offset: 0x0012B7D8
		private void SetToolTipInternal(Control control, ToolTip.TipInfo info)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			bool flag = false;
			bool flag2 = false;
			if (this.tools.ContainsKey(control))
			{
				flag = true;
			}
			if (info == null || string.IsNullOrEmpty(info.Caption))
			{
				flag2 = true;
			}
			if (flag && flag2)
			{
				this.tools.Remove(control);
			}
			else if (!flag2)
			{
				this.tools[control] = info;
			}
			if (!flag2 && !flag)
			{
				control.HandleCreated += this.HandleCreated;
				control.HandleDestroyed += this.HandleDestroyed;
				if (control.IsHandleCreated)
				{
					this.HandleCreated(control, EventArgs.Empty);
					return;
				}
			}
			else
			{
				bool flag3 = control.IsHandleCreated && this.TopLevelControl != null && this.TopLevelControl.IsHandleCreated;
				if (flag && !flag2 && flag3 && !base.DesignMode)
				{
					bool flag4;
					NativeMethods.TOOLINFO_TOOLTIP toolinfo = this.GetTOOLINFO(control, info.Caption, out flag4);
					try
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_SETTOOLINFO, 0, toolinfo);
					}
					finally
					{
						if (flag4 && IntPtr.Zero != toolinfo.lpszText)
						{
							Marshal.FreeHGlobal(toolinfo.lpszText);
						}
					}
					this.CheckNativeToolTip(control);
					this.CheckCompositeControls(control);
					return;
				}
				if (flag2 && flag && !base.DesignMode)
				{
					control.HandleCreated -= this.HandleCreated;
					control.HandleDestroyed -= this.HandleDestroyed;
					if (control.IsHandleCreated)
					{
						this.HandleDestroyed(control, EventArgs.Empty);
					}
					this.created.Remove(control);
				}
			}
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x060047BA RID: 18362 RVA: 0x0012D774 File Offset: 0x0012B974
		// (set) Token: 0x060047BB RID: 18363 RVA: 0x0012D77C File Offset: 0x0012B97C
		internal bool IsPersistent { get; private set; }

		// Token: 0x060047BC RID: 18364 RVA: 0x0012D785 File Offset: 0x0012B985
		private bool ShouldSerializeAutomaticDelay()
		{
			return this.auto && this.AutomaticDelay != 500;
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x0012D79F File Offset: 0x0012B99F
		private bool ShouldSerializeAutoPopDelay()
		{
			return !this.auto;
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x0012D79F File Offset: 0x0012B99F
		private bool ShouldSerializeInitialDelay()
		{
			return !this.auto;
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x0012D79F File Offset: 0x0012B99F
		private bool ShouldSerializeReshowDelay()
		{
			return !this.auto;
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x0012D7AC File Offset: 0x0012B9AC
		private void ShowTooltip(string text, IWin32Window win, int duration)
		{
			if (win == null)
			{
				throw new ArgumentNullException("win");
			}
			Control control = win as Control;
			if (control != null)
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				UnsafeNativeMethods.GetWindowRect(new HandleRef(control, control.Handle), ref rect);
				Cursor currentInternal = Cursor.CurrentInternal;
				Point position = Cursor.Position;
				Point point = position;
				Screen screen = Screen.FromPoint(position);
				if (position.X < rect.left || position.X > rect.right || position.Y < rect.top || position.Y > rect.bottom)
				{
					NativeMethods.RECT rect2 = default(NativeMethods.RECT);
					rect2.left = ((rect.left < screen.WorkingArea.Left) ? screen.WorkingArea.Left : rect.left);
					rect2.top = ((rect.top < screen.WorkingArea.Top) ? screen.WorkingArea.Top : rect.top);
					rect2.right = ((rect.right > screen.WorkingArea.Right) ? screen.WorkingArea.Right : rect.right);
					rect2.bottom = ((rect.bottom > screen.WorkingArea.Bottom) ? screen.WorkingArea.Bottom : rect.bottom);
					point.X = rect2.left + (rect2.right - rect2.left) / 2;
					point.Y = rect2.top + (rect2.bottom - rect2.top) / 2;
					control.PointToClientInternal(point);
					this.SetTrackPosition(point.X, point.Y);
					this.SetTool(win, text, ToolTip.TipInfo.Type.SemiAbsolute, point);
					if (duration > 0)
					{
						this.StartTimer(this.window, duration);
						return;
					}
				}
				else
				{
					ToolTip.TipInfo tipInfo = (ToolTip.TipInfo)this.tools[control];
					if (tipInfo == null)
					{
						tipInfo = new ToolTip.TipInfo(text, ToolTip.TipInfo.Type.SemiAbsolute);
					}
					else
					{
						tipInfo.TipType |= ToolTip.TipInfo.Type.SemiAbsolute;
						tipInfo.Caption = text;
					}
					tipInfo.Position = point;
					if (duration > 0)
					{
						if (this.originalPopupDelay == 0)
						{
							this.originalPopupDelay = this.AutoPopDelay;
						}
						this.AutoPopDelay = duration;
					}
					this.SetToolTipInternal(control, tipInfo);
				}
			}
		}

		/// <summary>Sets the ToolTip text associated with the specified control, and displays the ToolTip modally.</summary>
		/// <param name="text">A <see cref="T:System.String" /> containing the new ToolTip text.</param>
		/// <param name="window">The <see cref="T:System.Windows.Forms.Control" /> to display the ToolTip for.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="window" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060047C1 RID: 18369 RVA: 0x0012DA0E File Offset: 0x0012BC0E
		public void Show(string text, IWin32Window window)
		{
			if (this.HasAllWindowsPermission && this.IsWindowActive(window))
			{
				this.ShowTooltip(text, window, 0);
			}
		}

		/// <summary>Sets the ToolTip text associated with the specified control, and then displays the ToolTip for the specified duration.</summary>
		/// <param name="text">A <see cref="T:System.String" /> containing the new ToolTip text.</param>
		/// <param name="window">The <see cref="T:System.Windows.Forms.Control" /> to display the ToolTip for.</param>
		/// <param name="duration">An <see cref="T:System.Int32" /> containing the duration, in milliseconds, to display the ToolTip.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="window" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="duration" /> is less than or equal to 0.</exception>
		// Token: 0x060047C2 RID: 18370 RVA: 0x0012DA2C File Offset: 0x0012BC2C
		public void Show(string text, IWin32Window window, int duration)
		{
			if (duration < 0)
			{
				throw new ArgumentOutOfRangeException("duration", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"duration",
					duration.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (this.HasAllWindowsPermission && this.IsWindowActive(window))
			{
				this.ShowTooltip(text, window, duration);
			}
		}

		/// <summary>Sets the ToolTip text associated with the specified control, and then displays the ToolTip modally at the specified relative position.</summary>
		/// <param name="text">A <see cref="T:System.String" /> containing the new ToolTip text.</param>
		/// <param name="window">The <see cref="T:System.Windows.Forms.Control" /> to display the ToolTip for.</param>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> containing the offset, in pixels, relative to the upper-left corner of the associated control window, to display the ToolTip.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="window" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060047C3 RID: 18371 RVA: 0x0012DA9C File Offset: 0x0012BC9C
		public void Show(string text, IWin32Window window, Point point)
		{
			if (window == null)
			{
				throw new ArgumentNullException("window");
			}
			if (this.HasAllWindowsPermission && this.IsWindowActive(window))
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				UnsafeNativeMethods.GetWindowRect(new HandleRef(window, Control.GetSafeHandle(window)), ref rect);
				int num = rect.left + point.X;
				int num2 = rect.top + point.Y;
				this.SetTrackPosition(num, num2);
				this.SetTool(window, text, ToolTip.TipInfo.Type.Absolute, new Point(num, num2));
			}
		}

		/// <summary>Sets the ToolTip text associated with the specified control, and then displays the ToolTip for the specified duration at the specified relative position.</summary>
		/// <param name="text">A <see cref="T:System.String" /> containing the new ToolTip text.</param>
		/// <param name="window">The <see cref="T:System.Windows.Forms.Control" /> to display the ToolTip for.</param>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> containing the offset, in pixels, relative to the upper-left corner of the associated control window, to display the ToolTip.</param>
		/// <param name="duration">An <see cref="T:System.Int32" /> containing the duration, in milliseconds, to display the ToolTip.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="window" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="duration" /> is less than or equal to 0.</exception>
		// Token: 0x060047C4 RID: 18372 RVA: 0x0012DB1C File Offset: 0x0012BD1C
		public void Show(string text, IWin32Window window, Point point, int duration)
		{
			if (window == null)
			{
				throw new ArgumentNullException("window");
			}
			if (duration < 0)
			{
				throw new ArgumentOutOfRangeException("duration", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"duration",
					duration.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (this.HasAllWindowsPermission && this.IsWindowActive(window))
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				UnsafeNativeMethods.GetWindowRect(new HandleRef(window, Control.GetSafeHandle(window)), ref rect);
				int num = rect.left + point.X;
				int num2 = rect.top + point.Y;
				this.SetTrackPosition(num, num2);
				this.SetTool(window, text, ToolTip.TipInfo.Type.Absolute, new Point(num, num2));
				this.StartTimer(window, duration);
			}
		}

		/// <summary>Sets the ToolTip text associated with the specified control, and then displays the ToolTip modally at the specified relative position.</summary>
		/// <param name="text">A <see cref="T:System.String" /> containing the new ToolTip text.</param>
		/// <param name="window">The <see cref="T:System.Windows.Forms.Control" /> to display the ToolTip for.</param>
		/// <param name="x">The horizontal offset, in pixels, relative to the upper-left corner of the associated control window, to display the ToolTip.</param>
		/// <param name="y">The vertical offset, in pixels, relative to the upper-left corner of the associated control window, to display the ToolTip.</param>
		// Token: 0x060047C5 RID: 18373 RVA: 0x0012DBEC File Offset: 0x0012BDEC
		public void Show(string text, IWin32Window window, int x, int y)
		{
			if (window == null)
			{
				throw new ArgumentNullException("window");
			}
			if (this.HasAllWindowsPermission && this.IsWindowActive(window))
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				UnsafeNativeMethods.GetWindowRect(new HandleRef(window, Control.GetSafeHandle(window)), ref rect);
				int num = rect.left + x;
				int num2 = rect.top + y;
				this.SetTrackPosition(num, num2);
				this.SetTool(window, text, ToolTip.TipInfo.Type.Absolute, new Point(num, num2));
			}
		}

		/// <summary>Sets the ToolTip text associated with the specified control, and then displays the ToolTip for the specified duration at the specified relative position.</summary>
		/// <param name="text">A <see cref="T:System.String" /> containing the new ToolTip text.</param>
		/// <param name="window">The <see cref="T:System.Windows.Forms.Control" /> to display the ToolTip for.</param>
		/// <param name="x">The horizontal offset, in pixels, relative to the upper-left corner of the associated control window, to display the ToolTip.</param>
		/// <param name="y">The vertical offset, in pixels, relative to the upper-left corner of the associated control window, to display the ToolTip.</param>
		/// <param name="duration">An <see cref="T:System.Int32" /> containing the duration, in milliseconds, to display the ToolTip.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="window" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="duration" /> is less than or equal to 0.</exception>
		// Token: 0x060047C6 RID: 18374 RVA: 0x0012DC60 File Offset: 0x0012BE60
		public void Show(string text, IWin32Window window, int x, int y, int duration)
		{
			if (window == null)
			{
				throw new ArgumentNullException("window");
			}
			if (duration < 0)
			{
				throw new ArgumentOutOfRangeException("duration", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"duration",
					duration.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (this.HasAllWindowsPermission && this.IsWindowActive(window))
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				UnsafeNativeMethods.GetWindowRect(new HandleRef(window, Control.GetSafeHandle(window)), ref rect);
				int num = rect.left + x;
				int num2 = rect.top + y;
				this.SetTrackPosition(num, num2);
				this.SetTool(window, text, ToolTip.TipInfo.Type.Absolute, new Point(num, num2));
				this.StartTimer(window, duration);
			}
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x0012DD24 File Offset: 0x0012BF24
		internal void ShowKeyboardToolTip(string text, IKeyboardToolTip tool, int duration)
		{
			if (tool == null)
			{
				throw new ArgumentNullException("tool");
			}
			if (duration < 0)
			{
				throw new ArgumentOutOfRangeException("duration", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"duration",
					duration.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			Rectangle nativeScreenRectangle = tool.GetNativeScreenRectangle();
			int num = (nativeScreenRectangle.Left + nativeScreenRectangle.Right) / 2;
			int num2 = (nativeScreenRectangle.Top + nativeScreenRectangle.Bottom) / 2;
			this.SetTool(tool.GetOwnerWindow(), text, ToolTip.TipInfo.Type.Absolute, new Point(num, num2));
			Size size;
			if (this.TryGetBubbleSize(tool, nativeScreenRectangle, out size))
			{
				Point optimalToolTipPosition = this.GetOptimalToolTipPosition(tool, nativeScreenRectangle, size.Width, size.Height);
				num = optimalToolTipPosition.X;
				num2 = optimalToolTipPosition.Y;
				ToolTip.TipInfo tipInfo = (ToolTip.TipInfo)(this.tools[tool] ?? this.tools[tool.GetOwnerWindow()]);
				tipInfo.Position = new Point(num, num2);
				this.Reposition(optimalToolTipPosition, size);
			}
			this.SetTrackPosition(num, num2);
			if (!this.IsPersistent)
			{
				this.StartTimer(tool.GetOwnerWindow(), duration);
			}
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x0012DE58 File Offset: 0x0012C058
		private bool TryGetBubbleSize(IKeyboardToolTip tool, Rectangle toolRectangle, out Size bubbleSize)
		{
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1054, 0, this.GetMinToolInfoForTool(tool.GetOwnerWindow()));
			if (intPtr.ToInt32() != 1)
			{
				int num = NativeMethods.Util.LOWORD(intPtr);
				int num2 = NativeMethods.Util.HIWORD(intPtr);
				bubbleSize = new Size(num, num2);
				return true;
			}
			bubbleSize = Size.Empty;
			return false;
		}

		// Token: 0x060047C9 RID: 18377 RVA: 0x0012DEBC File Offset: 0x0012C0BC
		private Point GetOptimalToolTipPosition(IKeyboardToolTip tool, Rectangle toolRectangle, int width, int height)
		{
			int num = toolRectangle.Left + toolRectangle.Width / 2 - width / 2;
			int num2 = toolRectangle.Top + toolRectangle.Height / 2 - height / 2;
			Rectangle[] array = new Rectangle[]
			{
				new Rectangle(num, toolRectangle.Top - height, width, height),
				new Rectangle(toolRectangle.Right, num2, width, height),
				new Rectangle(num, toolRectangle.Bottom, width, height),
				new Rectangle(toolRectangle.Left - width, num2, width, height)
			};
			IList<Rectangle> neighboringToolsRectangles = tool.GetNeighboringToolsRectangles();
			long[] array2 = new long[4];
			for (int i = 0; i < array.Length; i++)
			{
				checked
				{
					foreach (Rectangle rectangle in neighboringToolsRectangles)
					{
						Rectangle rectangle2 = Rectangle.Intersect(array[i], rectangle);
						array2[i] += Math.Abs(unchecked((long)rectangle2.Width) * unchecked((long)rectangle2.Height));
					}
				}
			}
			Rectangle virtualScreen = SystemInformation.VirtualScreen;
			long[] array3 = new long[4];
			for (int j = 0; j < array.Length; j++)
			{
				Rectangle rectangle3 = Rectangle.Intersect(virtualScreen, array[j]);
				checked
				{
					array3[j] = (Math.Abs(unchecked((long)array[j].Width)) - Math.Abs(unchecked((long)rectangle3.Width))) * (Math.Abs(unchecked((long)array[j].Height)) - Math.Abs(unchecked((long)rectangle3.Height)));
				}
			}
			long[] array4 = new long[4];
			Control control = this.TopLevelControl;
			Rectangle rectangle4 = ((control != null) ? ((IKeyboardToolTip)control).GetNativeScreenRectangle() : Rectangle.Empty);
			if (!rectangle4.IsEmpty)
			{
				for (int k = 0; k < array.Length; k++)
				{
					Rectangle rectangle5 = Rectangle.Intersect(rectangle4, array[k]);
					checked
					{
						array4[k] = Math.Abs(unchecked((long)rectangle5.Height) * unchecked((long)rectangle5.Width));
					}
				}
			}
			long num3 = array2[0];
			long num4 = array3[0];
			long num5 = array4[0];
			int num6 = 0;
			Rectangle rectangle6 = array[0];
			bool flag = tool.HasRtlModeEnabled();
			for (int l = 1; l < array.Length; l++)
			{
				if (this.IsCompetingLocationBetter(num4, num3, num5, num6, array3[l], array2[l], array4[l], l, flag))
				{
					num3 = array2[l];
					num4 = array3[l];
					num5 = array4[l];
					num6 = l;
					rectangle6 = array[l];
				}
			}
			return new Point(rectangle6.Left, rectangle6.Top);
		}

		// Token: 0x060047CA RID: 18378 RVA: 0x0012E160 File Offset: 0x0012C360
		private bool IsCompetingLocationBetter(long originalLocationClippedArea, long originalLocationWeight, long originalLocationAreaWithinTopControl, int originalIndex, long competingLocationClippedArea, long competingLocationWeight, long competingLocationAreaWithinTopControl, int competingIndex, bool rtlEnabled)
		{
			if (competingLocationClippedArea < originalLocationClippedArea)
			{
				return true;
			}
			if (competingLocationWeight < originalLocationWeight)
			{
				return true;
			}
			if (competingLocationWeight == originalLocationWeight && competingLocationClippedArea == originalLocationClippedArea)
			{
				if (competingLocationAreaWithinTopControl > originalLocationAreaWithinTopControl)
				{
					return true;
				}
				if (competingLocationAreaWithinTopControl == originalLocationAreaWithinTopControl)
				{
					switch (originalIndex)
					{
					case 0:
						return true;
					case 1:
						if (rtlEnabled && competingIndex == 3)
						{
							return true;
						}
						break;
					case 2:
						if (competingIndex == 3 || competingIndex == 1)
						{
							return true;
						}
						break;
					case 3:
						if (!rtlEnabled && competingIndex == 1)
						{
							return true;
						}
						break;
					default:
						throw new NotSupportedException("Unsupported location index value");
					}
				}
			}
			return false;
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x0012E1DC File Offset: 0x0012C3DC
		private void SetTrackPosition(int pointX, int pointY)
		{
			try
			{
				this.trackPosition = true;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1042, 0, NativeMethods.Util.MAKELONG(pointX, pointY));
			}
			finally
			{
				this.trackPosition = false;
			}
		}

		/// <summary>Hides the specified ToolTip window.</summary>
		/// <param name="win">The <see cref="T:System.Windows.Forms.IWin32Window" /> of the associated window or control that the ToolTip is associated with.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="win" /> is <see langword="null" />.</exception>
		// Token: 0x060047CC RID: 18380 RVA: 0x0012E22C File Offset: 0x0012C42C
		public void Hide(IWin32Window win)
		{
			if (win == null)
			{
				throw new ArgumentNullException("win");
			}
			if (this.HasAllWindowsPermission)
			{
				if (this.window == null)
				{
					return;
				}
				if (this.GetHandleCreated())
				{
					IntPtr safeHandle = Control.GetSafeHandle(win);
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1041, 0, this.GetWinTOOLINFO(safeHandle));
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_DELTOOL, 0, this.GetWinTOOLINFO(safeHandle));
				}
				this.StopTimer();
				Control control = win as Control;
				if (control == null)
				{
					this.owners.Remove(win.Handle);
				}
				else
				{
					if (this.tools.ContainsKey(control))
					{
						this.SetToolInfo(control, this.GetToolTip(control));
					}
					else
					{
						this.owners.Remove(win.Handle);
					}
					Form form = control.FindFormInternal();
					if (form != null)
					{
						form.Deactivate -= this.BaseFormDeactivate;
					}
				}
				this.ClearTopLevelControlEvents();
				this.topLevelControl = null;
			}
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x0012E32D File Offset: 0x0012C52D
		private void BaseFormDeactivate(object sender, EventArgs e)
		{
			this.HideAllToolTips();
			if (!AccessibilityImprovements.UseLegacyToolTipDisplay)
			{
				KeyboardToolTipStateMachine.Instance.NotifyAboutFormDeactivation(this);
			}
		}

		// Token: 0x060047CE RID: 18382 RVA: 0x0012E348 File Offset: 0x0012C548
		private void HideAllToolTips()
		{
			Control[] array = new Control[this.owners.Values.Count];
			this.owners.Values.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				this.Hide(array[i]);
			}
		}

		// Token: 0x060047CF RID: 18383 RVA: 0x0012E394 File Offset: 0x0012C594
		private void SetTool(IWin32Window win, string text, ToolTip.TipInfo.Type type, Point position)
		{
			Control control = win as Control;
			if (control != null && this.tools.ContainsKey(control))
			{
				bool flag = false;
				NativeMethods.TOOLINFO_TOOLTIP toolinfo_TOOLTIP = new NativeMethods.TOOLINFO_TOOLTIP();
				try
				{
					toolinfo_TOOLTIP.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_TOOLTIP));
					toolinfo_TOOLTIP.hwnd = control.Handle;
					toolinfo_TOOLTIP.uId = control.Handle;
					int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_GETTOOLINFO, 0, toolinfo_TOOLTIP);
					if (num != 0)
					{
						toolinfo_TOOLTIP.uFlags |= 32;
						if (type == ToolTip.TipInfo.Type.Absolute || type == ToolTip.TipInfo.Type.SemiAbsolute)
						{
							toolinfo_TOOLTIP.uFlags |= 128;
						}
						toolinfo_TOOLTIP.lpszText = Marshal.StringToHGlobalAuto(text);
						flag = true;
					}
					ToolTip.TipInfo tipInfo = (ToolTip.TipInfo)this.tools[control];
					if (tipInfo == null)
					{
						tipInfo = new ToolTip.TipInfo(text, type);
					}
					else
					{
						tipInfo.TipType |= type;
						tipInfo.Caption = text;
					}
					tipInfo.Position = position;
					this.tools[control] = tipInfo;
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_SETTOOLINFO, 0, toolinfo_TOOLTIP);
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1041, 1, toolinfo_TOOLTIP);
					goto IL_25E;
				}
				finally
				{
					if (flag && IntPtr.Zero != toolinfo_TOOLTIP.lpszText)
					{
						Marshal.FreeHGlobal(toolinfo_TOOLTIP.lpszText);
					}
				}
			}
			this.Hide(win);
			ToolTip.TipInfo tipInfo2 = (ToolTip.TipInfo)this.tools[control];
			if (tipInfo2 == null)
			{
				tipInfo2 = new ToolTip.TipInfo(text, type);
			}
			else
			{
				tipInfo2.TipType |= type;
				tipInfo2.Caption = text;
			}
			tipInfo2.Position = position;
			this.tools[control] = tipInfo2;
			IntPtr safeHandle = Control.GetSafeHandle(win);
			this.owners[safeHandle] = win;
			NativeMethods.TOOLINFO_TOOLTIP winTOOLINFO = this.GetWinTOOLINFO(safeHandle);
			winTOOLINFO.uFlags |= 32;
			if (type == ToolTip.TipInfo.Type.Absolute || type == ToolTip.TipInfo.Type.SemiAbsolute)
			{
				winTOOLINFO.uFlags |= 128;
			}
			try
			{
				winTOOLINFO.lpszText = Marshal.StringToHGlobalAuto(text);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_ADDTOOL, 0, winTOOLINFO);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1041, 1, winTOOLINFO);
			}
			finally
			{
				if (IntPtr.Zero != winTOOLINFO.lpszText)
				{
					Marshal.FreeHGlobal(winTOOLINFO.lpszText);
				}
			}
			IL_25E:
			if (control != null)
			{
				Form form = control.FindFormInternal();
				if (form != null)
				{
					form.Deactivate += this.BaseFormDeactivate;
				}
			}
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x0012E658 File Offset: 0x0012C858
		private void StartTimer(IWin32Window owner, int interval)
		{
			if (this.timer == null)
			{
				this.timer = new ToolTip.ToolTipTimer(owner);
				this.timer.Tick += this.TimerHandler;
			}
			this.timer.Interval = interval;
			this.timer.Start();
		}

		/// <summary>Stops the timer that hides displayed ToolTips.</summary>
		// Token: 0x060047D1 RID: 18385 RVA: 0x0012E6A8 File Offset: 0x0012C8A8
		protected void StopTimer()
		{
			ToolTip.ToolTipTimer toolTipTimer = this.timer;
			if (toolTipTimer != null)
			{
				toolTipTimer.Stop();
				toolTipTimer.Dispose();
				this.timer = null;
			}
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x0012E6D2 File Offset: 0x0012C8D2
		private void TimerHandler(object source, EventArgs args)
		{
			this.Hide(((ToolTip.ToolTipTimer)source).Host);
		}

		/// <summary>Releases the unmanaged resources and performs other cleanup operations before the <see cref="T:System.Windows.Forms.Cursor" /> is reclaimed by the garbage collector.</summary>
		// Token: 0x060047D3 RID: 18387 RVA: 0x0012E6E8 File Offset: 0x0012C8E8
		~ToolTip()
		{
			this.DestroyHandle();
		}

		/// <summary>Returns a string representation for this control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a description of the <see cref="T:System.Windows.Forms.ToolTip" />.</returns>
		// Token: 0x060047D4 RID: 18388 RVA: 0x0012E714 File Offset: 0x0012C914
		public override string ToString()
		{
			string text = base.ToString();
			return string.Concat(new string[]
			{
				text,
				" InitialDelay: ",
				this.InitialDelay.ToString(CultureInfo.CurrentCulture),
				", ShowAlways: ",
				this.ShowAlways.ToString(CultureInfo.CurrentCulture)
			});
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x0012E774 File Offset: 0x0012C974
		private void Reposition(Point tipPosition, Size tipSize)
		{
			Point point = tipPosition;
			Screen screen = Screen.FromPoint(point);
			if (point.X + tipSize.Width > screen.WorkingArea.Right)
			{
				point.X = screen.WorkingArea.Right - tipSize.Width;
			}
			if (point.Y + tipSize.Height > screen.WorkingArea.Bottom)
			{
				point.Y = screen.WorkingArea.Bottom - tipSize.Height;
			}
			SafeNativeMethods.SetWindowPos(new HandleRef(this, this.Handle), NativeMethods.HWND_TOPMOST, point.X, point.Y, tipSize.Width, tipSize.Height, 529);
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x0012E83C File Offset: 0x0012CA3C
		private void WmMove()
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetWindowRect(new HandleRef(this, this.Handle), ref rect);
			NativeMethods.TOOLINFO_TOOLTIP toolinfo_TOOLTIP = new NativeMethods.TOOLINFO_TOOLTIP();
			toolinfo_TOOLTIP.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_TOOLTIP));
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_GETCURRENTTOOL, 0, toolinfo_TOOLTIP);
			if (num != 0)
			{
				IWin32Window win32Window = (IWin32Window)this.owners[toolinfo_TOOLTIP.hwnd];
				if (win32Window == null)
				{
					win32Window = Control.FromHandleInternal(toolinfo_TOOLTIP.hwnd);
				}
				if (win32Window == null)
				{
					return;
				}
				ToolTip.TipInfo tipInfo = (ToolTip.TipInfo)this.tools[win32Window];
				if (win32Window == null || tipInfo == null)
				{
					return;
				}
				TreeView treeView = win32Window as TreeView;
				if (treeView != null && treeView.ShowNodeToolTips)
				{
					return;
				}
				if (tipInfo.Position != Point.Empty)
				{
					this.Reposition(tipInfo.Position, rect.Size);
				}
			}
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x0012E92C File Offset: 0x0012CB2C
		private void WmMouseActivate(ref Message msg)
		{
			NativeMethods.TOOLINFO_TOOLTIP toolinfo_TOOLTIP = new NativeMethods.TOOLINFO_TOOLTIP();
			toolinfo_TOOLTIP.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_TOOLTIP));
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_GETCURRENTTOOL, 0, toolinfo_TOOLTIP);
			if (num != 0)
			{
				IWin32Window win32Window = (IWin32Window)this.owners[toolinfo_TOOLTIP.hwnd];
				if (win32Window == null)
				{
					win32Window = Control.FromHandleInternal(toolinfo_TOOLTIP.hwnd);
				}
				if (win32Window == null)
				{
					return;
				}
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				UnsafeNativeMethods.GetWindowRect(new HandleRef(win32Window, Control.GetSafeHandle(win32Window)), ref rect);
				Point position = Cursor.Position;
				if (position.X >= rect.left && position.X <= rect.right && position.Y >= rect.top && position.Y <= rect.bottom)
				{
					msg.Result = (IntPtr)3;
				}
			}
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x0012EA14 File Offset: 0x0012CC14
		private void WmWindowFromPoint(ref Message msg)
		{
			NativeMethods.POINT point = (NativeMethods.POINT)msg.GetLParam(typeof(NativeMethods.POINT));
			Point point2 = new Point(point.x, point.y);
			bool flag = false;
			msg.Result = this.GetWindowFromPoint(point2, ref flag);
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x0012EA5C File Offset: 0x0012CC5C
		private void WmShow()
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetWindowRect(new HandleRef(this, this.Handle), ref rect);
			NativeMethods.TOOLINFO_TOOLTIP toolinfo_TOOLTIP = new NativeMethods.TOOLINFO_TOOLTIP();
			toolinfo_TOOLTIP.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_TOOLTIP));
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_GETCURRENTTOOL, 0, toolinfo_TOOLTIP);
			if (num != 0)
			{
				IWin32Window win32Window = (IWin32Window)this.owners[toolinfo_TOOLTIP.hwnd];
				if (win32Window == null)
				{
					win32Window = Control.FromHandleInternal(toolinfo_TOOLTIP.hwnd);
				}
				if (win32Window == null)
				{
					return;
				}
				Control control = win32Window as Control;
				Size size = rect.Size;
				PopupEventArgs popupEventArgs = new PopupEventArgs(win32Window, control, this.IsBalloon, size);
				this.OnPopup(popupEventArgs);
				DataGridView dataGridView = control as DataGridView;
				if (dataGridView != null && dataGridView.CancelToolTipPopup(this))
				{
					popupEventArgs.Cancel = true;
				}
				UnsafeNativeMethods.GetWindowRect(new HandleRef(this, this.Handle), ref rect);
				size = ((popupEventArgs.ToolTipSize == size) ? rect.Size : popupEventArgs.ToolTipSize);
				if (this.IsBalloon)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1055, 1, ref rect);
					if (rect.Size.Height > size.Height)
					{
						size.Height = rect.Size.Height;
					}
				}
				if (size != rect.Size)
				{
					Screen screen = Screen.FromPoint(Cursor.Position);
					int num2 = (this.IsBalloon ? Math.Min(size.Width - 20, screen.WorkingArea.Width) : Math.Min(size.Width, screen.WorkingArea.Width));
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1048, 0, num2);
				}
				if (popupEventArgs.Cancel)
				{
					this.cancelled = true;
					SafeNativeMethods.SetWindowPos(new HandleRef(this, this.Handle), NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, 528);
					return;
				}
				this.cancelled = false;
				SafeNativeMethods.SetWindowPos(new HandleRef(this, this.Handle), NativeMethods.HWND_TOPMOST, rect.left, rect.top, size.Width, size.Height, 528);
			}
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x0012ECB0 File Offset: 0x0012CEB0
		private bool WmWindowPosChanged()
		{
			if (this.cancelled)
			{
				SafeNativeMethods.ShowWindow(new HandleRef(this, this.Handle), 0);
				return true;
			}
			return false;
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x0012ECD0 File Offset: 0x0012CED0
		private unsafe void WmWindowPosChanging(ref Message m)
		{
			if (this.cancelled || this.isDisposing)
			{
				return;
			}
			NativeMethods.WINDOWPOS* ptr = (NativeMethods.WINDOWPOS*)(void*)m.LParam;
			Cursor currentInternal = Cursor.CurrentInternal;
			Point position = Cursor.Position;
			NativeMethods.TOOLINFO_TOOLTIP toolinfo_TOOLTIP = new NativeMethods.TOOLINFO_TOOLTIP();
			toolinfo_TOOLTIP.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_TOOLTIP));
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_GETCURRENTTOOL, 0, toolinfo_TOOLTIP);
			if (num != 0)
			{
				IWin32Window win32Window = (IWin32Window)this.owners[toolinfo_TOOLTIP.hwnd];
				if (win32Window == null)
				{
					win32Window = Control.FromHandleInternal(toolinfo_TOOLTIP.hwnd);
				}
				if (win32Window == null || !this.IsWindowActive(win32Window))
				{
					return;
				}
				ToolTip.TipInfo tipInfo = null;
				if (win32Window != null)
				{
					tipInfo = (ToolTip.TipInfo)this.tools[win32Window];
					if (tipInfo == null)
					{
						return;
					}
					TreeView treeView = win32Window as TreeView;
					if (treeView != null && treeView.ShowNodeToolTips)
					{
						return;
					}
				}
				if (this.IsBalloon)
				{
					ptr->cx += 20;
					return;
				}
				if ((tipInfo.TipType & ToolTip.TipInfo.Type.Auto) != ToolTip.TipInfo.Type.None && this.window != null)
				{
					this.window.DefWndProc(ref m);
					return;
				}
				if ((tipInfo.TipType & ToolTip.TipInfo.Type.SemiAbsolute) != ToolTip.TipInfo.Type.None && tipInfo.Position == Point.Empty)
				{
					Screen screen = Screen.FromPoint(position);
					if (currentInternal != null)
					{
						ptr->x = position.X;
						try
						{
							IntSecurity.ObjectFromWin32Handle.Assert();
							ptr->y = position.Y;
							if (ptr->y + ptr->cy + currentInternal.Size.Height - currentInternal.HotSpot.Y > screen.WorkingArea.Bottom)
							{
								ptr->y = position.Y - ptr->cy;
							}
							else
							{
								ptr->y = position.Y + currentInternal.Size.Height - currentInternal.HotSpot.Y;
							}
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					if (ptr->x + ptr->cx > screen.WorkingArea.Right)
					{
						ptr->x = screen.WorkingArea.Right - ptr->cx;
					}
				}
				else if ((tipInfo.TipType & ToolTip.TipInfo.Type.SemiAbsolute) != ToolTip.TipInfo.Type.None && tipInfo.Position != Point.Empty)
				{
					Screen screen2 = Screen.FromPoint(tipInfo.Position);
					ptr->x = tipInfo.Position.X;
					if (ptr->x + ptr->cx > screen2.WorkingArea.Right)
					{
						ptr->x = screen2.WorkingArea.Right - ptr->cx;
					}
					ptr->y = tipInfo.Position.Y;
					if (ptr->y + ptr->cy > screen2.WorkingArea.Bottom)
					{
						ptr->y = screen2.WorkingArea.Bottom - ptr->cy;
					}
				}
			}
			m.Result = IntPtr.Zero;
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x0012F004 File Offset: 0x0012D204
		private void WmPop()
		{
			NativeMethods.TOOLINFO_TOOLTIP toolinfo_TOOLTIP = new NativeMethods.TOOLINFO_TOOLTIP();
			toolinfo_TOOLTIP.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_TOOLTIP));
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_GETCURRENTTOOL, 0, toolinfo_TOOLTIP);
			if (num != 0)
			{
				IWin32Window win32Window = (IWin32Window)this.owners[toolinfo_TOOLTIP.hwnd];
				if (win32Window == null)
				{
					win32Window = Control.FromHandleInternal(toolinfo_TOOLTIP.hwnd);
				}
				if (win32Window == null)
				{
					return;
				}
				Control control = win32Window as Control;
				ToolTip.TipInfo tipInfo = (ToolTip.TipInfo)this.tools[win32Window];
				if (tipInfo == null)
				{
					return;
				}
				if ((tipInfo.TipType & ToolTip.TipInfo.Type.Auto) != ToolTip.TipInfo.Type.None || (tipInfo.TipType & ToolTip.TipInfo.Type.SemiAbsolute) != ToolTip.TipInfo.Type.None)
				{
					Screen screen = Screen.FromPoint(Cursor.Position);
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1048, 0, screen.WorkingArea.Width);
				}
				if ((tipInfo.TipType & ToolTip.TipInfo.Type.Auto) == ToolTip.TipInfo.Type.None)
				{
					this.tools.Remove(control);
					this.owners.Remove(win32Window.Handle);
					control.HandleCreated -= this.HandleCreated;
					control.HandleDestroyed -= this.HandleDestroyed;
					this.created.Remove(control);
					if (this.originalPopupDelay != 0)
					{
						this.AutoPopDelay = this.originalPopupDelay;
						this.originalPopupDelay = 0;
						return;
					}
				}
				else
				{
					tipInfo.TipType = ToolTip.TipInfo.Type.Auto;
					tipInfo.Position = Point.Empty;
					this.tools[control] = tipInfo;
				}
			}
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x0012F184 File Offset: 0x0012D384
		private void WndProc(ref Message msg)
		{
			int msg2 = msg.Msg;
			if (msg2 <= 70)
			{
				if (msg2 <= 15)
				{
					if (msg2 == 3)
					{
						this.WmMove();
						return;
					}
					if (msg2 != 15)
					{
						goto IL_291;
					}
				}
				else
				{
					if (msg2 == 33)
					{
						this.WmMouseActivate(ref msg);
						return;
					}
					if (msg2 != 70)
					{
						goto IL_291;
					}
					this.WmWindowPosChanging(ref msg);
					return;
				}
			}
			else if (msg2 <= 792)
			{
				if (msg2 != 71)
				{
					if (msg2 != 792)
					{
						goto IL_291;
					}
				}
				else
				{
					if (!this.WmWindowPosChanged() && this.window != null)
					{
						this.window.DefWndProc(ref msg);
						return;
					}
					return;
				}
			}
			else
			{
				if (msg2 == 1040)
				{
					this.WmWindowFromPoint(ref msg);
					return;
				}
				if (msg2 != 8270)
				{
					goto IL_291;
				}
				NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)msg.GetLParam(typeof(NativeMethods.NMHDR));
				if (nmhdr.code == -521 && !this.trackPosition)
				{
					this.WmShow();
					return;
				}
				if (nmhdr.code != -522)
				{
					return;
				}
				this.WmPop();
				if (this.window != null)
				{
					this.window.DefWndProc(ref msg);
					return;
				}
				return;
			}
			if (this.ownerDraw && !this.isBalloon && !this.trackPosition)
			{
				NativeMethods.PAINTSTRUCT paintstruct = default(NativeMethods.PAINTSTRUCT);
				IntPtr intPtr = UnsafeNativeMethods.BeginPaint(new HandleRef(this, this.Handle), ref paintstruct);
				Graphics graphics = Graphics.FromHdcInternal(intPtr);
				try
				{
					Rectangle rectangle = new Rectangle(paintstruct.rcPaint_left, paintstruct.rcPaint_top, paintstruct.rcPaint_right - paintstruct.rcPaint_left, paintstruct.rcPaint_bottom - paintstruct.rcPaint_top);
					if (rectangle == Rectangle.Empty)
					{
						return;
					}
					NativeMethods.TOOLINFO_TOOLTIP toolinfo_TOOLTIP = new NativeMethods.TOOLINFO_TOOLTIP();
					toolinfo_TOOLTIP.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_TOOLTIP));
					int num = (int)(long)UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_GETCURRENTTOOL, 0, toolinfo_TOOLTIP);
					if (num != 0)
					{
						IWin32Window win32Window = (IWin32Window)this.owners[toolinfo_TOOLTIP.hwnd];
						Control control = Control.FromHandleInternal(toolinfo_TOOLTIP.hwnd);
						if (win32Window == null)
						{
							win32Window = control;
						}
						IntSecurity.ObjectFromWin32Handle.Assert();
						Font font;
						try
						{
							font = Font.FromHfont(UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 49, 0, 0));
						}
						catch (ArgumentException)
						{
							font = Control.DefaultFont;
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
						this.OnDraw(new DrawToolTipEventArgs(graphics, win32Window, control, rectangle, this.GetToolTip(control), this.BackColor, this.ForeColor, font));
						return;
					}
				}
				finally
				{
					graphics.Dispose();
					UnsafeNativeMethods.EndPaint(new HandleRef(this, this.Handle), ref paintstruct);
				}
			}
			IL_291:
			if (this.window != null)
			{
				this.window.DefWndProc(ref msg);
			}
		}

		// Token: 0x040026DF RID: 9951
		private const int DEFAULT_DELAY = 500;

		// Token: 0x040026E0 RID: 9952
		private const int RESHOW_RATIO = 5;

		// Token: 0x040026E1 RID: 9953
		private const int AUTOPOP_RATIO = 10;

		// Token: 0x040026E2 RID: 9954
		private const int INFINITE_DELAY = 32767;

		// Token: 0x040026E3 RID: 9955
		private const int XBALLOONOFFSET = 10;

		// Token: 0x040026E4 RID: 9956
		private const int YBALLOONOFFSET = 8;

		// Token: 0x040026E5 RID: 9957
		private const int TOP_LOCATION_INDEX = 0;

		// Token: 0x040026E6 RID: 9958
		private const int RIGHT_LOCATION_INDEX = 1;

		// Token: 0x040026E7 RID: 9959
		private const int BOTTOM_LOCATION_INDEX = 2;

		// Token: 0x040026E8 RID: 9960
		private const int LEFT_LOCATION_INDEX = 3;

		// Token: 0x040026E9 RID: 9961
		private const int LOCATION_TOTAL = 4;

		// Token: 0x040026EA RID: 9962
		private Hashtable tools = new Hashtable();

		// Token: 0x040026EB RID: 9963
		private int[] delayTimes = new int[4];

		// Token: 0x040026EC RID: 9964
		private bool auto = true;

		// Token: 0x040026ED RID: 9965
		private bool showAlways;

		// Token: 0x040026EE RID: 9966
		private ToolTip.ToolTipNativeWindow window;

		// Token: 0x040026EF RID: 9967
		private Control topLevelControl;

		// Token: 0x040026F0 RID: 9968
		private bool active = true;

		// Token: 0x040026F1 RID: 9969
		private bool ownerDraw;

		// Token: 0x040026F2 RID: 9970
		private object userData;

		// Token: 0x040026F3 RID: 9971
		private Color backColor = SystemColors.Info;

		// Token: 0x040026F4 RID: 9972
		private Color foreColor = SystemColors.InfoText;

		// Token: 0x040026F5 RID: 9973
		private bool isBalloon;

		// Token: 0x040026F6 RID: 9974
		private bool isDisposing;

		// Token: 0x040026F7 RID: 9975
		private string toolTipTitle = string.Empty;

		// Token: 0x040026F8 RID: 9976
		private ToolTipIcon toolTipIcon;

		// Token: 0x040026F9 RID: 9977
		private ToolTip.ToolTipTimer timer;

		// Token: 0x040026FA RID: 9978
		private Hashtable owners = new Hashtable();

		// Token: 0x040026FB RID: 9979
		private bool stripAmpersands;

		// Token: 0x040026FC RID: 9980
		private bool useAnimation = true;

		// Token: 0x040026FD RID: 9981
		private bool useFading = true;

		// Token: 0x040026FE RID: 9982
		private int originalPopupDelay;

		// Token: 0x040026FF RID: 9983
		private bool trackPosition;

		// Token: 0x04002700 RID: 9984
		private PopupEventHandler onPopup;

		// Token: 0x04002701 RID: 9985
		private DrawToolTipEventHandler onDraw;

		// Token: 0x04002702 RID: 9986
		private Hashtable created = new Hashtable();

		// Token: 0x04002703 RID: 9987
		private bool cancelled;

		// Token: 0x02000821 RID: 2081
		private class ToolTipNativeWindow : NativeWindow
		{
			// Token: 0x06006FC8 RID: 28616 RVA: 0x00199BE8 File Offset: 0x00197DE8
			internal ToolTipNativeWindow(ToolTip control)
			{
				this.control = control;
			}

			// Token: 0x06006FC9 RID: 28617 RVA: 0x00199BF7 File Offset: 0x00197DF7
			protected override void WndProc(ref Message m)
			{
				if (this.control != null)
				{
					this.control.WndProc(ref m);
				}
			}

			// Token: 0x04004331 RID: 17201
			private ToolTip control;
		}

		// Token: 0x02000822 RID: 2082
		private class ToolTipTimer : Timer
		{
			// Token: 0x06006FCA RID: 28618 RVA: 0x00199C0D File Offset: 0x00197E0D
			public ToolTipTimer(IWin32Window owner)
			{
				this.host = owner;
			}

			// Token: 0x17001878 RID: 6264
			// (get) Token: 0x06006FCB RID: 28619 RVA: 0x00199C1C File Offset: 0x00197E1C
			public IWin32Window Host
			{
				get
				{
					return this.host;
				}
			}

			// Token: 0x04004332 RID: 17202
			private IWin32Window host;
		}

		// Token: 0x02000823 RID: 2083
		private class TipInfo
		{
			// Token: 0x06006FCC RID: 28620 RVA: 0x00199C24 File Offset: 0x00197E24
			public TipInfo(string caption, ToolTip.TipInfo.Type type)
			{
				this.caption = caption;
				this.TipType = type;
				if (type == ToolTip.TipInfo.Type.Auto)
				{
					this.designerText = caption;
				}
			}

			// Token: 0x17001879 RID: 6265
			// (get) Token: 0x06006FCD RID: 28621 RVA: 0x00199C57 File Offset: 0x00197E57
			// (set) Token: 0x06006FCE RID: 28622 RVA: 0x00199C70 File Offset: 0x00197E70
			public string Caption
			{
				get
				{
					if ((this.TipType & (ToolTip.TipInfo.Type.Absolute | ToolTip.TipInfo.Type.SemiAbsolute)) == ToolTip.TipInfo.Type.None)
					{
						return this.designerText;
					}
					return this.caption;
				}
				set
				{
					this.caption = value;
				}
			}

			// Token: 0x04004333 RID: 17203
			public ToolTip.TipInfo.Type TipType = ToolTip.TipInfo.Type.Auto;

			// Token: 0x04004334 RID: 17204
			private string caption;

			// Token: 0x04004335 RID: 17205
			private string designerText;

			// Token: 0x04004336 RID: 17206
			public Point Position = Point.Empty;

			// Token: 0x020008CE RID: 2254
			[Flags]
			public enum Type
			{
				// Token: 0x04004559 RID: 17753
				None = 0,
				// Token: 0x0400455A RID: 17754
				Auto = 1,
				// Token: 0x0400455B RID: 17755
				Absolute = 2,
				// Token: 0x0400455C RID: 17756
				SemiAbsolute = 4
			}
		}
	}
}
