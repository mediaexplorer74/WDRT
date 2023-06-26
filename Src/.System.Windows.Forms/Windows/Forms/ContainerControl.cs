using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Provides focus-management functionality for controls that can function as a container for other controls.</summary>
	// Token: 0x02000165 RID: 357
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class ContainerControl : ScrollableControl, IContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContainerControl" /> class.</summary>
		// Token: 0x06000EEB RID: 3819 RVA: 0x0002CEF0 File Offset: 0x0002B0F0
		public ContainerControl()
		{
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, false);
			base.SetState2(2048, true);
		}

		/// <summary>Gets or sets the dimensions that the control was designed to.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> containing the dots per inch (DPI) or <see cref="T:System.Drawing.Font" /> size that the control was designed to.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The width or height of the <see cref="T:System.Drawing.SizeF" /> value is less than 0 when setting this value.</exception>
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0002CF3F File Offset: 0x0002B13F
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x0002CF48 File Offset: 0x0002B148
		[Localizable(true)]
		[Browsable(false)]
		[SRCategory("CatLayout")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SizeF AutoScaleDimensions
		{
			get
			{
				return this.autoScaleDimensions;
			}
			set
			{
				if (value.Width < 0f || value.Height < 0f)
				{
					throw new ArgumentOutOfRangeException(SR.GetString("ContainerControlInvalidAutoScaleDimensions"), "value");
				}
				this.autoScaleDimensions = value;
				if (!this.autoScaleDimensions.IsEmpty)
				{
					this.LayoutScalingNeeded();
				}
			}
		}

		/// <summary>Gets the scaling factor between the current and design-time automatic scaling dimensions.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> containing the scaling ratio between the current and design-time scaling automatic scaling dimensions.</returns>
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0002CFA0 File Offset: 0x0002B1A0
		protected SizeF AutoScaleFactor
		{
			get
			{
				SizeF sizeF = this.CurrentAutoScaleDimensions;
				SizeF sizeF2 = this.AutoScaleDimensions;
				if (sizeF2.IsEmpty)
				{
					return new SizeF(1f, 1f);
				}
				return new SizeF(sizeF.Width / sizeF2.Width, sizeF.Height / sizeF2.Height);
			}
		}

		/// <summary>Gets or sets the automatic scaling mode of the control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AutoScaleMode" /> that represents the current scaling mode. The default is <see cref="F:System.Windows.Forms.AutoScaleMode.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">An <see cref="T:System.Windows.Forms.AutoScaleMode" /> value that is not valid was used to set this property.</exception>
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0002CFF7 File Offset: 0x0002B1F7
		// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x0002D000 File Offset: 0x0002B200
		[SRCategory("CatLayout")]
		[SRDescription("ContainerControlAutoScaleModeDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AutoScaleMode AutoScaleMode
		{
			get
			{
				return this.autoScaleMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoScaleMode));
				}
				bool flag = false;
				if (value != this.autoScaleMode)
				{
					if (this.autoScaleMode != AutoScaleMode.Inherit)
					{
						this.autoScaleDimensions = SizeF.Empty;
					}
					this.currentAutoScaleDimensions = SizeF.Empty;
					this.autoScaleMode = value;
					flag = true;
				}
				this.OnAutoScaleModeChanged();
				if (flag)
				{
					this.LayoutScalingNeeded();
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether controls in this container will be automatically validated when the focus changes.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AutoValidate" /> enumerated value that indicates whether contained controls are implicitly validated on focus change. The default is <see cref="F:System.Windows.Forms.AutoValidate.Inherit" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A <see cref="T:System.Windows.Forms.AutoValidate" /> value that is not valid was used to set this property.</exception>
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0002D075 File Offset: 0x0002B275
		// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x0002D08D File Offset: 0x0002B28D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[AmbientValue(AutoValidate.Inherit)]
		[SRCategory("CatBehavior")]
		[SRDescription("ContainerControlAutoValidate")]
		public virtual AutoValidate AutoValidate
		{
			get
			{
				if (this.autoValidate == AutoValidate.Inherit)
				{
					return Control.GetAutoValidateForControl(this);
				}
				return this.autoValidate;
			}
			set
			{
				if (value - AutoValidate.Inherit > 3)
				{
					throw new InvalidEnumArgumentException("AutoValidate", (int)value, typeof(AutoValidate));
				}
				if (this.autoValidate != value)
				{
					this.autoValidate = value;
					this.OnAutoValidateChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ContainerControl.AutoValidate" /> property changes.</summary>
		// Token: 0x14000081 RID: 129
		// (add) Token: 0x06000EF3 RID: 3827 RVA: 0x0002D0C6 File Offset: 0x0002B2C6
		// (remove) Token: 0x06000EF4 RID: 3828 RVA: 0x0002D0DF File Offset: 0x0002B2DF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ContainerControlOnAutoValidateChangedDescr")]
		public event EventHandler AutoValidateChanged
		{
			add
			{
				this.autoValidateChanged = (EventHandler)Delegate.Combine(this.autoValidateChanged, value);
			}
			remove
			{
				this.autoValidateChanged = (EventHandler)Delegate.Remove(this.autoValidateChanged, value);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</returns>
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0002D0F8 File Offset: 0x0002B2F8
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x0002D11D File Offset: 0x0002B31D
		[Browsable(false)]
		[SRDescription("ContainerControlBindingContextDescr")]
		public override BindingContext BindingContext
		{
			get
			{
				BindingContext bindingContext = base.BindingContext;
				if (bindingContext == null)
				{
					bindingContext = new BindingContext();
					this.BindingContext = bindingContext;
				}
				return bindingContext;
			}
			set
			{
				base.BindingContext = value;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property can be set to an active value, to enable IME support.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected override bool CanEnableIme
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the active control on the container control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that is currently active on the <see cref="T:System.Windows.Forms.ContainerControl" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.Control" /> assigned could not be activated.</exception>
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0002D126 File Offset: 0x0002B326
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x0002D12E File Offset: 0x0002B32E
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ContainerControlActiveControlDescr")]
		public Control ActiveControl
		{
			get
			{
				return this.activeControl;
			}
			set
			{
				this.SetActiveControl(value);
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x0002D138 File Offset: 0x0002B338
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 65536;
				return createParams;
			}
		}

		/// <summary>Gets the current run-time dimensions of the screen.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> containing the current dots per inch (DPI) or <see cref="T:System.Drawing.Font" /> size of the screen.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A Win32 device context could not be created for the current screen.</exception>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x0002D160 File Offset: 0x0002B360
		[Browsable(false)]
		[SRCategory("CatLayout")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SizeF CurrentAutoScaleDimensions
		{
			get
			{
				if (this.currentAutoScaleDimensions.IsEmpty)
				{
					AutoScaleMode autoScaleMode = this.AutoScaleMode;
					if (autoScaleMode != AutoScaleMode.Font)
					{
						if (autoScaleMode != AutoScaleMode.Dpi)
						{
							this.currentAutoScaleDimensions = this.AutoScaleDimensions;
						}
						else if (DpiHelper.EnableDpiChangedMessageHandling)
						{
							this.currentAutoScaleDimensions = new SizeF((float)this.deviceDpi, (float)this.deviceDpi);
						}
						else
						{
							this.currentAutoScaleDimensions = WindowsGraphicsCacheManager.MeasurementGraphics.DeviceContext.Dpi;
						}
					}
					else
					{
						this.currentAutoScaleDimensions = this.GetFontAutoScaleDimensions();
					}
				}
				return this.currentAutoScaleDimensions;
			}
		}

		/// <summary>Gets the form that the container control is assigned to.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Form" /> that the container control is assigned to. This property will return null if the control is hosted inside of Internet Explorer or in another hosting context where there is no parent form.</returns>
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0002D1E9 File Offset: 0x0002B3E9
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ContainerControlParentFormDescr")]
		public Form ParentForm
		{
			get
			{
				IntSecurity.GetParent.Demand();
				return this.ParentFormInternal;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0002D1FB File Offset: 0x0002B3FB
		internal Form ParentFormInternal
		{
			get
			{
				if (this.ParentInternal != null)
				{
					return this.ParentInternal.FindFormInternal();
				}
				if (this is Form)
				{
					return null;
				}
				return base.FindFormInternal();
			}
		}

		/// <summary>Activates the specified control.</summary>
		/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to activate.</param>
		/// <returns>
		///   <see langword="true" /> if the control is successfully activated; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EFE RID: 3838 RVA: 0x0002D221 File Offset: 0x0002B421
		bool IContainerControl.ActivateControl(Control control)
		{
			IntSecurity.ModifyFocus.Demand();
			return this.ActivateControlInternal(control, true);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0002D235 File Offset: 0x0002B435
		internal bool ActivateControlInternal(Control control)
		{
			return this.ActivateControlInternal(control, true);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0002D240 File Offset: 0x0002B440
		internal bool ActivateControlInternal(Control control, bool originator)
		{
			bool flag = true;
			bool flag2 = false;
			ContainerControl containerControl = null;
			Control parentInternal = this.ParentInternal;
			if (parentInternal != null)
			{
				containerControl = parentInternal.GetContainerControlInternal() as ContainerControl;
				if (containerControl != null)
				{
					flag2 = containerControl.ActiveControl != this;
				}
			}
			if (control != this.activeControl || flag2)
			{
				if (flag2 && !containerControl.ActivateControlInternal(this, false))
				{
					return false;
				}
				flag = this.AssignActiveControlInternal((control == this) ? null : control);
			}
			if (originator)
			{
				this.ScrollActiveControlIntoView();
			}
			return flag;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0002D2B4 File Offset: 0x0002B4B4
		internal bool HasFocusableChild()
		{
			Control control = null;
			do
			{
				control = base.GetNextControl(control, true);
			}
			while ((control == null || !control.CanSelect || !control.TabStop) && control != null);
			return control != null;
		}

		/// <summary>Adjusts the scroll bars on the container based on the current control positions and the control currently selected.</summary>
		/// <param name="displayScrollbars">
		///   <see langword="true" /> to show the scroll bars; otherwise, <see langword="false" />.</param>
		// Token: 0x06000F02 RID: 3842 RVA: 0x0002D2E6 File Offset: 0x0002B4E6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void AdjustFormScrollbars(bool displayScrollbars)
		{
			base.AdjustFormScrollbars(displayScrollbars);
			if (!base.GetScrollState(8))
			{
				this.ScrollActiveControlIntoView();
			}
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0002D300 File Offset: 0x0002B500
		internal virtual void AfterControlRemoved(Control control, Control oldParent)
		{
			ContainerControl containerControl;
			if (control == this.activeControl || control.Contains(this.activeControl))
			{
				IntSecurity.ModifyFocus.Assert();
				bool flag;
				try
				{
					flag = base.SelectNextControl(control, true, true, true, true);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (flag && this.activeControl != control)
				{
					if (!this.activeControl.Parent.IsTopMdiWindowClosing)
					{
						this.FocusActiveControlInternal();
					}
				}
				else
				{
					this.SetActiveControlInternal(null);
				}
			}
			else if (this.activeControl == null && this.ParentInternal != null)
			{
				containerControl = this.ParentInternal.GetContainerControlInternal() as ContainerControl;
				if (containerControl != null && containerControl.ActiveControl == this)
				{
					Form form = base.FindFormInternal();
					if (form != null)
					{
						IntSecurity.ModifyFocus.Assert();
						try
						{
							form.SelectNextControl(this, true, true, true, true);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
				}
			}
			containerControl = this;
			while (containerControl != null)
			{
				Control parentInternal = containerControl.ParentInternal;
				if (parentInternal == null)
				{
					break;
				}
				containerControl = parentInternal.GetContainerControlInternal() as ContainerControl;
				if (containerControl != null && containerControl.unvalidatedControl != null && (containerControl.unvalidatedControl == control || control.Contains(containerControl.unvalidatedControl)))
				{
					containerControl.unvalidatedControl = oldParent;
				}
			}
			if (control == this.unvalidatedControl || control.Contains(this.unvalidatedControl))
			{
				this.unvalidatedControl = null;
			}
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0002D448 File Offset: 0x0002B648
		private bool AssignActiveControlInternal(Control value)
		{
			if (this.activeControl != value)
			{
				try
				{
					if (value != null)
					{
						value.BecomingActiveControl = true;
					}
					this.activeControl = value;
					this.UpdateFocusedControl();
				}
				finally
				{
					if (value != null)
					{
						value.BecomingActiveControl = false;
					}
				}
				if (this.activeControl == value)
				{
					Form form = base.FindFormInternal();
					if (form != null)
					{
						form.UpdateDefaultButton();
					}
				}
			}
			else
			{
				this.focusedControl = this.activeControl;
			}
			return this.activeControl == value;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0002D4C4 File Offset: 0x0002B6C4
		private void AxContainerFormCreated()
		{
			((AxHost.AxContainer)base.Properties.GetObject(ContainerControl.PropAxContainer)).FormCreated();
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0002D4E0 File Offset: 0x0002B6E0
		internal override bool CanProcessMnemonic()
		{
			return this.state[ContainerControl.stateProcessingMnemonic] || base.CanProcessMnemonic();
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0002D4FC File Offset: 0x0002B6FC
		internal AxHost.AxContainer CreateAxContainer()
		{
			object obj = base.Properties.GetObject(ContainerControl.PropAxContainer);
			if (obj == null)
			{
				obj = new AxHost.AxContainer(this);
				base.Properties.SetObject(ContainerControl.PropAxContainer, obj);
			}
			return (AxHost.AxContainer)obj;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000F08 RID: 3848 RVA: 0x0002D53B File Offset: 0x0002B73B
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.activeControl = null;
			}
			base.Dispose(disposing);
			this.focusedControl = null;
			this.unvalidatedControl = null;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0002D55C File Offset: 0x0002B75C
		private void EnableRequiredScaling(Control start, bool enable)
		{
			start.RequiredScalingEnabled = enable;
			foreach (object obj in start.Controls)
			{
				Control control = (Control)obj;
				this.EnableRequiredScaling(control, enable);
			}
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0002D5C0 File Offset: 0x0002B7C0
		internal void FocusActiveControlInternal()
		{
			if (this.activeControl != null && this.activeControl.Visible)
			{
				IntPtr focus = UnsafeNativeMethods.GetFocus();
				if (focus == IntPtr.Zero || Control.FromChildHandleInternal(focus) != this.activeControl)
				{
					UnsafeNativeMethods.SetFocus(new HandleRef(this.activeControl, this.activeControl.Handle));
					return;
				}
			}
			else
			{
				ContainerControl containerControl = this;
				while (containerControl != null && !containerControl.Visible)
				{
					Control parentInternal = containerControl.ParentInternal;
					if (parentInternal == null)
					{
						break;
					}
					containerControl = parentInternal.GetContainerControlInternal() as ContainerControl;
				}
				if (containerControl != null && containerControl.Visible)
				{
					UnsafeNativeMethods.SetFocus(new HandleRef(containerControl, containerControl.Handle));
				}
			}
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0002D664 File Offset: 0x0002B864
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			Size size = this.SizeFromClientSize(Size.Empty);
			Size size2 = size + base.Padding.Size;
			return this.LayoutEngine.GetPreferredSize(this, proposedSize - size2) + size2;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0002D6AC File Offset: 0x0002B8AC
		internal override Rectangle GetToolNativeScreenRectangle()
		{
			if (base.GetTopLevel())
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				UnsafeNativeMethods.GetClientRect(new HandleRef(this, base.Handle), ref rect);
				NativeMethods.POINT point = new NativeMethods.POINT(0, 0);
				UnsafeNativeMethods.ClientToScreen(new HandleRef(this, base.Handle), point);
				return new Rectangle(point.x, point.y, rect.right, rect.bottom);
			}
			return base.GetToolNativeScreenRectangle();
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0002D71C File Offset: 0x0002B91C
		private SizeF GetFontAutoScaleDimensions()
		{
			SizeF empty = SizeF.Empty;
			IntPtr intPtr = UnsafeNativeMethods.CreateCompatibleDC(NativeMethods.NullHandleRef);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception();
			}
			HandleRef handleRef = new HandleRef(this, intPtr);
			try
			{
				HandleRef handleRef2 = new HandleRef(this, base.FontHandle);
				HandleRef handleRef3 = new HandleRef(this, SafeNativeMethods.SelectObject(handleRef, handleRef2));
				try
				{
					NativeMethods.TEXTMETRIC textmetric = default(NativeMethods.TEXTMETRIC);
					SafeNativeMethods.GetTextMetrics(handleRef, ref textmetric);
					empty.Height = (float)textmetric.tmHeight;
					if ((textmetric.tmPitchAndFamily & 1) != 0)
					{
						IntNativeMethods.SIZE size = new IntNativeMethods.SIZE();
						IntUnsafeNativeMethods.GetTextExtentPoint32(handleRef, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", size);
						empty.Width = (float)((int)Math.Round((double)((float)size.cx / (float)"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".Length)));
					}
					else
					{
						empty.Width = (float)textmetric.tmAveCharWidth;
					}
				}
				finally
				{
					SafeNativeMethods.SelectObject(handleRef, handleRef3);
				}
			}
			finally
			{
				UnsafeNativeMethods.DeleteCompatibleDC(handleRef);
			}
			return empty;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0002D818 File Offset: 0x0002BA18
		private void LayoutScalingNeeded()
		{
			this.EnableRequiredScaling(this, true);
			this.state[ContainerControl.stateScalingNeededOnLayout] = true;
			if (!base.IsLayoutSuspended)
			{
				LayoutTransaction.DoLayout(this, this, PropertyNames.Bounds);
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnAutoScaleModeChanged()
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ContainerControl.AutoValidateChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000F10 RID: 3856 RVA: 0x0002D847 File Offset: 0x0002BA47
		protected virtual void OnAutoValidateChanged(EventArgs e)
		{
			if (this.autoValidateChanged != null)
			{
				this.autoValidateChanged(this, e);
			}
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0002D860 File Offset: 0x0002BA60
		internal override void OnFrameWindowActivate(bool fActivate)
		{
			if (fActivate)
			{
				IntSecurity.ModifyFocus.Assert();
				try
				{
					if (this.ActiveControl == null)
					{
						base.SelectNextControl(null, true, true, true, false);
					}
					this.InnerMostActiveContainerControl.FocusActiveControlInternal();
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0002D8B4 File Offset: 0x0002BAB4
		internal override void OnChildLayoutResuming(Control child, bool performLayout)
		{
			base.OnChildLayoutResuming(child, performLayout);
			if (DpiHelper.EnableSinglePassScalingOfDpiForms && this.AutoScaleMode == AutoScaleMode.Dpi)
			{
				return;
			}
			if (!this.state[ContainerControl.stateScalingChild] && !performLayout && this.AutoScaleMode != AutoScaleMode.None && this.AutoScaleMode != AutoScaleMode.Inherit && this.state[ContainerControl.stateScalingNeededOnLayout])
			{
				this.state[ContainerControl.stateScalingChild] = true;
				try
				{
					child.Scale(this.AutoScaleFactor, SizeF.Empty, this);
				}
				finally
				{
					this.state[ContainerControl.stateScalingChild] = false;
				}
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.CreateControl" /> method.</summary>
		// Token: 0x06000F13 RID: 3859 RVA: 0x0002D95C File Offset: 0x0002BB5C
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (base.Properties.GetObject(ContainerControl.PropAxContainer) != null)
			{
				this.AxContainerFormCreated();
			}
			this.OnBindingContextChanged(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000F14 RID: 3860 RVA: 0x0002D988 File Offset: 0x0002BB88
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnFontChanged(EventArgs e)
		{
			if (this.AutoScaleMode == AutoScaleMode.Font)
			{
				this.currentAutoScaleDimensions = SizeF.Empty;
				this.SuspendAllLayout(this);
				try
				{
					this.PerformAutoScale(!base.RequiredScalingEnabled, true);
				}
				finally
				{
					this.ResumeAllLayout(this, false);
				}
			}
			base.OnFontChanged(e);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0002D9E4 File Offset: 0x0002BBE4
		internal void FormDpiChanged(float factor)
		{
			this.currentAutoScaleDimensions = SizeF.Empty;
			this.SuspendAllLayout(this);
			SizeF sizeF = new SizeF(factor, factor);
			try
			{
				base.ScaleChildControls(sizeF, sizeF, this, true);
			}
			finally
			{
				this.ResumeAllLayout(this, false);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06000F16 RID: 3862 RVA: 0x0002DA34 File Offset: 0x0002BC34
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.PerformNeededAutoScaleOnLayout();
			base.OnLayout(e);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0002DA43 File Offset: 0x0002BC43
		internal override void OnLayoutResuming(bool performLayout)
		{
			this.PerformNeededAutoScaleOnLayout();
			base.OnLayoutResuming(performLayout);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000F18 RID: 3864 RVA: 0x0002DA52 File Offset: 0x0002BC52
		protected override void OnParentChanged(EventArgs e)
		{
			this.state[ContainerControl.stateParentChanged] = !base.RequiredScalingEnabled;
			base.OnParentChanged(e);
		}

		/// <summary>Performs scaling of the container control and its children.</summary>
		// Token: 0x06000F19 RID: 3865 RVA: 0x0002DA74 File Offset: 0x0002BC74
		public void PerformAutoScale()
		{
			this.PerformAutoScale(true, true);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0002DA80 File Offset: 0x0002BC80
		private void PerformAutoScale(bool includedBounds, bool excludedBounds)
		{
			bool flag = false;
			try
			{
				if (this.AutoScaleMode != AutoScaleMode.None && this.AutoScaleMode != AutoScaleMode.Inherit)
				{
					this.SuspendAllLayout(this);
					flag = true;
					SizeF sizeF = SizeF.Empty;
					SizeF sizeF2 = SizeF.Empty;
					if (includedBounds)
					{
						sizeF = this.AutoScaleFactor;
					}
					if (excludedBounds)
					{
						sizeF2 = this.AutoScaleFactor;
					}
					this.Scale(sizeF, sizeF2, this);
					this.autoScaleDimensions = this.CurrentAutoScaleDimensions;
				}
			}
			finally
			{
				if (includedBounds)
				{
					this.state[ContainerControl.stateScalingNeededOnLayout] = false;
					this.EnableRequiredScaling(this, false);
				}
				this.state[ContainerControl.stateParentChanged] = false;
				if (flag)
				{
					this.ResumeAllLayout(this, false);
				}
			}
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0002DB2C File Offset: 0x0002BD2C
		private void PerformNeededAutoScaleOnLayout()
		{
			if (this.state[ContainerControl.stateScalingNeededOnLayout])
			{
				this.PerformAutoScale(this.state[ContainerControl.stateScalingNeededOnLayout], false);
			}
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0002DB58 File Offset: 0x0002BD58
		internal void ResumeAllLayout(Control start, bool performLayout)
		{
			Control.ControlCollection controls = start.Controls;
			for (int i = 0; i < controls.Count; i++)
			{
				this.ResumeAllLayout(controls[i], performLayout);
			}
			start.ResumeLayout(performLayout);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0002DB94 File Offset: 0x0002BD94
		internal void SuspendAllLayout(Control start)
		{
			start.SuspendLayout();
			CommonProperties.xClearPreferredSizeCache(start);
			Control.ControlCollection controls = start.Controls;
			for (int i = 0; i < controls.Count; i++)
			{
				this.SuspendAllLayout(controls[i]);
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0002DBD4 File Offset: 0x0002BDD4
		internal override void Scale(SizeF includedFactor, SizeF excludedFactor, Control requestingControl)
		{
			if (this.AutoScaleMode == AutoScaleMode.Inherit)
			{
				base.Scale(includedFactor, excludedFactor, requestingControl);
				return;
			}
			SizeF sizeF = excludedFactor;
			SizeF sizeF2 = includedFactor;
			if (!sizeF.IsEmpty)
			{
				sizeF = this.AutoScaleFactor;
			}
			if (this.AutoScaleMode == AutoScaleMode.None)
			{
				sizeF2 = this.AutoScaleFactor;
			}
			using (new LayoutTransaction(this, this, PropertyNames.Bounds, false))
			{
				SizeF sizeF3 = sizeF;
				if (!excludedFactor.IsEmpty && this.ParentInternal != null)
				{
					sizeF3 = SizeF.Empty;
					bool flag = requestingControl != this || this.state[ContainerControl.stateParentChanged];
					if (!flag)
					{
						bool flag2 = false;
						bool flag3 = false;
						ISite site = this.Site;
						ISite site2 = this.ParentInternal.Site;
						if (site != null)
						{
							flag2 = site.DesignMode;
						}
						if (site2 != null)
						{
							flag3 = site2.DesignMode;
						}
						if (flag2 && !flag3)
						{
							flag = true;
						}
					}
					if (flag)
					{
						sizeF3 = excludedFactor;
					}
				}
				base.ScaleControl(includedFactor, sizeF3, requestingControl);
				base.ScaleChildControls(sizeF2, sizeF, requestingControl, false);
			}
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0002DCD0 File Offset: 0x0002BED0
		private bool ProcessArrowKey(bool forward)
		{
			Control control = this;
			if (this.activeControl != null)
			{
				control = this.activeControl.ParentInternal;
			}
			return control.SelectNextControl(this.activeControl, forward, false, false, true);
		}

		/// <summary>Processes a dialog character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000F20 RID: 3872 RVA: 0x0002DD04 File Offset: 0x0002BF04
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogChar(char charCode)
		{
			ContainerControl containerControl = base.GetContainerControlInternal() as ContainerControl;
			return (containerControl != null && charCode != ' ' && this.ProcessMnemonic(charCode)) || base.ProcessDialogChar(charCode);
		}

		/// <summary>Processes a dialog key.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the key was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000F21 RID: 3873 RVA: 0x0002DD38 File Offset: 0x0002BF38
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & (Keys.Control | Keys.Alt)) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys != Keys.Tab)
				{
					if (keys - Keys.Left <= 3)
					{
						if (this.ProcessArrowKey(keys == Keys.Right || keys == Keys.Down))
						{
							return true;
						}
					}
				}
				else if (this.ProcessTabKey((keyData & Keys.Shift) == Keys.None))
				{
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		/// <summary>Processes a command key.</summary>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000F22 RID: 3874 RVA: 0x0002DD96 File Offset: 0x0002BF96
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return base.ProcessCmdKey(ref msg, keyData) || (this.ParentInternal == null && ToolStripManager.ProcessCmdKey(ref msg, keyData));
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed as a mnemonic by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000F23 RID: 3875 RVA: 0x0002DDB8 File Offset: 0x0002BFB8
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (!this.CanProcessMnemonic())
			{
				return false;
			}
			if (base.Controls.Count == 0)
			{
				return false;
			}
			Control control = this.ActiveControl;
			this.state[ContainerControl.stateProcessingMnemonic] = true;
			bool flag = false;
			try
			{
				bool flag2 = false;
				Control control2 = control;
				for (;;)
				{
					control2 = base.GetNextControl(control2, true);
					if (control2 != null)
					{
						if (control2.ProcessMnemonic(charCode))
						{
							break;
						}
					}
					else
					{
						if (flag2)
						{
							goto Block_7;
						}
						flag2 = true;
					}
					if (control2 == control)
					{
						goto Block_8;
					}
				}
				flag = true;
				Block_7:
				Block_8:;
			}
			finally
			{
				this.state[ContainerControl.stateProcessingMnemonic] = false;
			}
			return flag;
		}

		/// <summary>Selects the next available control and makes it the active control.</summary>
		/// <param name="forward">
		///   <see langword="true" /> to cycle forward through the controls in the <see cref="T:System.Windows.Forms.ContainerControl" />; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if a control is selected; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000F24 RID: 3876 RVA: 0x0002DE48 File Offset: 0x0002C048
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected virtual bool ProcessTabKey(bool forward)
		{
			return base.SelectNextControl(this.activeControl, forward, true, true, false);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0002DE60 File Offset: 0x0002C060
		private ScrollableControl FindScrollableParent(Control ctl)
		{
			Control control = ctl.ParentInternal;
			while (control != null && !(control is ScrollableControl))
			{
				control = control.ParentInternal;
			}
			if (control != null)
			{
				return (ScrollableControl)control;
			}
			return null;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0002DE94 File Offset: 0x0002C094
		private void ScrollActiveControlIntoView()
		{
			Control control = this.activeControl;
			if (control != null)
			{
				for (ScrollableControl scrollableControl = this.FindScrollableParent(control); scrollableControl != null; scrollableControl = this.FindScrollableParent(scrollableControl))
				{
					scrollableControl.ScrollControlIntoView(this.activeControl);
				}
			}
		}

		/// <summary>Activates a child control. Optionally specifies the direction in the tab order to select the control from.</summary>
		/// <param name="directed">
		///   <see langword="true" /> to specify the direction of the control to select; otherwise, <see langword="false" />.</param>
		/// <param name="forward">
		///   <see langword="true" /> to move forward in the tab order; <see langword="false" /> to move backward in the tab order.</param>
		// Token: 0x06000F27 RID: 3879 RVA: 0x0002DED0 File Offset: 0x0002C0D0
		protected override void Select(bool directed, bool forward)
		{
			bool flag = true;
			if (this.ParentInternal != null)
			{
				IContainerControl containerControlInternal = this.ParentInternal.GetContainerControlInternal();
				if (containerControlInternal != null)
				{
					containerControlInternal.ActiveControl = this;
					flag = containerControlInternal.ActiveControl == this;
				}
			}
			if (directed && flag)
			{
				base.SelectNextControl(null, forward, true, true, false);
			}
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0002DF18 File Offset: 0x0002C118
		private void SetActiveControl(Control ctl)
		{
			this.SetActiveControlInternal(ctl);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0002DF24 File Offset: 0x0002C124
		internal void SetActiveControlInternal(Control value)
		{
			if (this.activeControl != value || (value != null && !value.Focused))
			{
				if (value != null && !base.Contains(value))
				{
					throw new ArgumentException(SR.GetString("CannotActivateControl"));
				}
				ContainerControl containerControl = this;
				if (value != null && value.ParentInternal != null)
				{
					containerControl = value.ParentInternal.GetContainerControlInternal() as ContainerControl;
				}
				bool flag;
				if (containerControl != null)
				{
					flag = containerControl.ActivateControlInternal(value, false);
				}
				else
				{
					flag = this.AssignActiveControlInternal(value);
				}
				if (containerControl != null && flag)
				{
					ContainerControl containerControl2 = this;
					while (containerControl2.ParentInternal != null && containerControl2.ParentInternal.GetContainerControlInternal() is ContainerControl)
					{
						containerControl2 = containerControl2.ParentInternal.GetContainerControlInternal() as ContainerControl;
					}
					if (containerControl2.ContainsFocus && (value == null || !(value is UserControl) || (value is UserControl && !((UserControl)value).HasFocusableChild())))
					{
						containerControl.FocusActiveControlInternal();
					}
				}
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x0002E000 File Offset: 0x0002C200
		internal ContainerControl InnerMostActiveContainerControl
		{
			get
			{
				ContainerControl containerControl = this;
				while (containerControl.ActiveControl is ContainerControl)
				{
					containerControl = (ContainerControl)containerControl.ActiveControl;
				}
				return containerControl;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0002E02C File Offset: 0x0002C22C
		internal ContainerControl InnerMostFocusedContainerControl
		{
			get
			{
				ContainerControl containerControl = this;
				while (containerControl.focusedControl is ContainerControl)
				{
					containerControl = (ContainerControl)containerControl.focusedControl;
				}
				return containerControl;
			}
		}

		/// <summary>When overridden by a derived class, updates which button is the default button.</summary>
		// Token: 0x06000F2C RID: 3884 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void UpdateDefaultButton()
		{
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0002E058 File Offset: 0x0002C258
		internal void UpdateFocusedControl()
		{
			this.EnsureUnvalidatedControl(this.focusedControl);
			Control control = this.focusedControl;
			while (this.activeControl != control)
			{
				if (control == null || control.IsDescendant(this.activeControl))
				{
					Control parentInternal = this.activeControl;
					for (;;)
					{
						Control parentInternal2 = parentInternal.ParentInternal;
						if (parentInternal2 == this || parentInternal2 == control)
						{
							break;
						}
						parentInternal = parentInternal.ParentInternal;
					}
					Control control2 = (this.focusedControl = control);
					this.EnterValidation(parentInternal);
					if (this.focusedControl != control2)
					{
						control = this.focusedControl;
						continue;
					}
					control = parentInternal;
					if (NativeWindow.WndProcShouldBeDebuggable)
					{
						control.NotifyEnter();
						continue;
					}
					try
					{
						control.NotifyEnter();
						continue;
					}
					catch (Exception ex)
					{
						Application.OnThreadException(ex);
						continue;
					}
				}
				ContainerControl innerMostFocusedContainerControl = this.InnerMostFocusedContainerControl;
				Control control3 = null;
				if (innerMostFocusedContainerControl.focusedControl != null)
				{
					control = innerMostFocusedContainerControl.focusedControl;
					control3 = innerMostFocusedContainerControl;
					if (innerMostFocusedContainerControl != this)
					{
						innerMostFocusedContainerControl.focusedControl = null;
						if (innerMostFocusedContainerControl.ParentInternal == null || !(innerMostFocusedContainerControl.ParentInternal is MdiClient))
						{
							innerMostFocusedContainerControl.activeControl = null;
						}
					}
				}
				else
				{
					control = innerMostFocusedContainerControl;
					if (innerMostFocusedContainerControl.ParentInternal != null)
					{
						ContainerControl containerControl = innerMostFocusedContainerControl.ParentInternal.GetContainerControlInternal() as ContainerControl;
						control3 = containerControl;
						if (containerControl != null && containerControl != this)
						{
							containerControl.focusedControl = null;
							containerControl.activeControl = null;
						}
					}
				}
				do
				{
					Control control4 = control;
					if (control != null)
					{
						control = control.ParentInternal;
					}
					if (control == this)
					{
						control = null;
					}
					if (control4 != null)
					{
						if (NativeWindow.WndProcShouldBeDebuggable)
						{
							control4.NotifyLeave();
						}
						else
						{
							try
							{
								control4.NotifyLeave();
							}
							catch (Exception ex2)
							{
								Application.OnThreadException(ex2);
							}
						}
					}
				}
				while (control != null && control != control3 && !control.IsDescendant(this.activeControl));
			}
			this.focusedControl = this.activeControl;
			if (this.activeControl != null)
			{
				this.EnterValidation(this.activeControl);
			}
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0002E228 File Offset: 0x0002C428
		private void EnsureUnvalidatedControl(Control candidate)
		{
			if (this.state[ContainerControl.stateValidating])
			{
				return;
			}
			if (this.unvalidatedControl != null)
			{
				return;
			}
			if (candidate == null)
			{
				return;
			}
			if (!candidate.ShouldAutoValidate)
			{
				return;
			}
			this.unvalidatedControl = candidate;
			while (this.unvalidatedControl is ContainerControl)
			{
				ContainerControl containerControl = this.unvalidatedControl as ContainerControl;
				if (containerControl.unvalidatedControl != null && containerControl.unvalidatedControl.ShouldAutoValidate)
				{
					this.unvalidatedControl = containerControl.unvalidatedControl;
				}
				else
				{
					if (containerControl.activeControl == null || !containerControl.activeControl.ShouldAutoValidate)
					{
						break;
					}
					this.unvalidatedControl = containerControl.activeControl;
				}
			}
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0002E2C4 File Offset: 0x0002C4C4
		private void EnterValidation(Control enterControl)
		{
			if (this.unvalidatedControl == null)
			{
				return;
			}
			if (!enterControl.CausesValidation)
			{
				return;
			}
			AutoValidate autoValidateForControl = Control.GetAutoValidateForControl(this.unvalidatedControl);
			if (autoValidateForControl == AutoValidate.Disable)
			{
				return;
			}
			Control control = enterControl;
			while (control != null && !control.IsDescendant(this.unvalidatedControl))
			{
				control = control.ParentInternal;
			}
			bool flag = autoValidateForControl == AutoValidate.EnablePreventFocusChange;
			this.ValidateThroughAncestor(control, flag);
		}

		/// <summary>Verifies the value of the control losing focus by causing the <see cref="E:System.Windows.Forms.Control.Validating" /> and <see cref="E:System.Windows.Forms.Control.Validated" /> events to occur, in that order.</summary>
		/// <returns>
		///   <see langword="true" /> if validation is successful; otherwise, <see langword="false" />. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return <see langword="false" />.</returns>
		// Token: 0x06000F30 RID: 3888 RVA: 0x0002E31D File Offset: 0x0002C51D
		public bool Validate()
		{
			return this.Validate(false);
		}

		/// <summary>Verifies the value of the control that is losing focus; conditionally dependent on whether automatic validation is turned on.</summary>
		/// <param name="checkAutoValidate">If <see langword="true" />, the value of the <see cref="P:System.Windows.Forms.ContainerControl.AutoValidate" /> property is used to determine if validation should be performed; if <see langword="false" />, validation is unconditionally performed.</param>
		/// <returns>
		///   <see langword="true" /> if validation is successful; otherwise, <see langword="false" />. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return <see langword="false" />.</returns>
		// Token: 0x06000F31 RID: 3889 RVA: 0x0002E328 File Offset: 0x0002C528
		public bool Validate(bool checkAutoValidate)
		{
			bool flag;
			return this.ValidateInternal(checkAutoValidate, out flag);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0002E340 File Offset: 0x0002C540
		internal bool ValidateInternal(bool checkAutoValidate, out bool validatedControlAllowsFocusChange)
		{
			validatedControlAllowsFocusChange = false;
			if (this.AutoValidate == AutoValidate.EnablePreventFocusChange || (this.activeControl != null && this.activeControl.CausesValidation))
			{
				if (this.unvalidatedControl == null)
				{
					if (this.focusedControl is ContainerControl && this.focusedControl.CausesValidation)
					{
						ContainerControl containerControl = (ContainerControl)this.focusedControl;
						if (!containerControl.ValidateInternal(checkAutoValidate, out validatedControlAllowsFocusChange))
						{
							return false;
						}
					}
					else
					{
						this.unvalidatedControl = this.focusedControl;
					}
				}
				bool flag = true;
				Control control = ((this.unvalidatedControl != null) ? this.unvalidatedControl : this.focusedControl);
				if (control != null)
				{
					AutoValidate autoValidateForControl = Control.GetAutoValidateForControl(control);
					if (checkAutoValidate && autoValidateForControl == AutoValidate.Disable)
					{
						return true;
					}
					flag = autoValidateForControl == AutoValidate.EnablePreventFocusChange;
					validatedControlAllowsFocusChange = autoValidateForControl == AutoValidate.EnableAllowFocusChange;
				}
				return this.ValidateThroughAncestor(null, flag);
			}
			return true;
		}

		/// <summary>Causes all of the child controls within a control that support validation to validate their data.</summary>
		/// <returns>
		///   <see langword="true" /> if all of the children validated successfully; otherwise, <see langword="false" />. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return <see langword="false" />.</returns>
		// Token: 0x06000F33 RID: 3891 RVA: 0x0002E3FA File Offset: 0x0002C5FA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ValidateChildren()
		{
			return this.ValidateChildren(ValidationConstraints.Selectable);
		}

		/// <summary>Causes all of the child controls within a control that support validation to validate their data.</summary>
		/// <param name="validationConstraints">Places restrictions on which controls have their <see cref="E:System.Windows.Forms.Control.Validating" /> event raised.</param>
		/// <returns>
		///   <see langword="true" /> if all of the children validated successfully; otherwise, <see langword="false" />. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return <see langword="false" />.</returns>
		// Token: 0x06000F34 RID: 3892 RVA: 0x0002E403 File Offset: 0x0002C603
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ValidateChildren(ValidationConstraints validationConstraints)
		{
			if ((validationConstraints < ValidationConstraints.None) || validationConstraints > (ValidationConstraints.Selectable | ValidationConstraints.Enabled | ValidationConstraints.Visible | ValidationConstraints.TabStop | ValidationConstraints.ImmediateChildren))
			{
				throw new InvalidEnumArgumentException("validationConstraints", (int)validationConstraints, typeof(ValidationConstraints));
			}
			return !base.PerformContainerValidation(validationConstraints);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0002E430 File Offset: 0x0002C630
		private bool ValidateThroughAncestor(Control ancestorControl, bool preventFocusChangeOnError)
		{
			if (ancestorControl == null)
			{
				ancestorControl = this;
			}
			if (this.state[ContainerControl.stateValidating])
			{
				return false;
			}
			if (this.unvalidatedControl == null)
			{
				this.unvalidatedControl = this.focusedControl;
			}
			if (this.unvalidatedControl == null)
			{
				return true;
			}
			if (!ancestorControl.IsDescendant(this.unvalidatedControl))
			{
				return false;
			}
			this.state[ContainerControl.stateValidating] = true;
			bool flag = false;
			Control control = this.activeControl;
			Control parentInternal = this.unvalidatedControl;
			if (control != null)
			{
				control.ValidationCancelled = false;
				if (control is ContainerControl)
				{
					ContainerControl containerControl = control as ContainerControl;
					containerControl.ResetValidationFlag();
				}
			}
			try
			{
				while (parentInternal != null && parentInternal != ancestorControl)
				{
					try
					{
						flag = parentInternal.PerformControlValidation(false);
					}
					catch
					{
						flag = true;
						throw;
					}
					if (flag)
					{
						break;
					}
					parentInternal = parentInternal.ParentInternal;
				}
				if (flag && preventFocusChangeOnError)
				{
					if (this.unvalidatedControl == null && parentInternal != null && ancestorControl.IsDescendant(parentInternal))
					{
						this.unvalidatedControl = parentInternal;
					}
					if (control == this.activeControl && control != null)
					{
						control.NotifyValidationResult(parentInternal, new CancelEventArgs
						{
							Cancel = true
						});
						if (control is ContainerControl)
						{
							ContainerControl containerControl2 = control as ContainerControl;
							if (containerControl2.focusedControl != null)
							{
								containerControl2.focusedControl.ValidationCancelled = true;
							}
							containerControl2.ResetActiveAndFocusedControlsRecursive();
						}
					}
					this.SetActiveControlInternal(this.unvalidatedControl);
				}
			}
			finally
			{
				this.unvalidatedControl = null;
				this.state[ContainerControl.stateValidating] = false;
			}
			return !flag;
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0002E5A4 File Offset: 0x0002C7A4
		private void ResetValidationFlag()
		{
			Control.ControlCollection controls = base.Controls;
			int count = controls.Count;
			for (int i = 0; i < count; i++)
			{
				controls[i].ValidationCancelled = false;
			}
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0002E5D8 File Offset: 0x0002C7D8
		internal void ResetActiveAndFocusedControlsRecursive()
		{
			if (this.activeControl is ContainerControl)
			{
				((ContainerControl)this.activeControl).ResetActiveAndFocusedControlsRecursive();
			}
			this.activeControl = null;
			this.focusedControl = null;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0002E605 File Offset: 0x0002C805
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeAutoValidate()
		{
			return this.autoValidate != AutoValidate.Inherit;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0002E614 File Offset: 0x0002C814
		private void WmSetFocus(ref Message m)
		{
			if (base.HostedInWin32DialogManager)
			{
				base.WndProc(ref m);
				return;
			}
			if (this.ActiveControl != null)
			{
				base.WmImeSetFocus();
				if (!this.ActiveControl.Visible)
				{
					base.InvokeGotFocus(this, EventArgs.Empty);
				}
				this.FocusActiveControlInternal();
				return;
			}
			if (this.ParentInternal != null)
			{
				IContainerControl containerControlInternal = this.ParentInternal.GetContainerControlInternal();
				if (containerControlInternal != null)
				{
					bool flag = false;
					ContainerControl containerControl = containerControlInternal as ContainerControl;
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
			base.WndProc(ref m);
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06000F3A RID: 3898 RVA: 0x0002E6C4 File Offset: 0x0002C8C4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 7)
			{
				this.WmSetFocus(ref m);
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x04000800 RID: 2048
		private Control activeControl;

		// Token: 0x04000801 RID: 2049
		private Control focusedControl;

		// Token: 0x04000802 RID: 2050
		private Control unvalidatedControl;

		// Token: 0x04000803 RID: 2051
		private AutoValidate autoValidate = AutoValidate.Inherit;

		// Token: 0x04000804 RID: 2052
		private EventHandler autoValidateChanged;

		// Token: 0x04000805 RID: 2053
		private SizeF autoScaleDimensions = SizeF.Empty;

		// Token: 0x04000806 RID: 2054
		private SizeF currentAutoScaleDimensions = SizeF.Empty;

		// Token: 0x04000807 RID: 2055
		private AutoScaleMode autoScaleMode = AutoScaleMode.Inherit;

		// Token: 0x04000808 RID: 2056
		private BitVector32 state;

		// Token: 0x04000809 RID: 2057
		private static readonly int stateScalingNeededOnLayout = BitVector32.CreateMask();

		// Token: 0x0400080A RID: 2058
		private static readonly int stateValidating = BitVector32.CreateMask(ContainerControl.stateScalingNeededOnLayout);

		// Token: 0x0400080B RID: 2059
		private static readonly int stateProcessingMnemonic = BitVector32.CreateMask(ContainerControl.stateValidating);

		// Token: 0x0400080C RID: 2060
		private static readonly int stateScalingChild = BitVector32.CreateMask(ContainerControl.stateProcessingMnemonic);

		// Token: 0x0400080D RID: 2061
		private static readonly int stateParentChanged = BitVector32.CreateMask(ContainerControl.stateScalingChild);

		// Token: 0x0400080E RID: 2062
		private static readonly int PropAxContainer = PropertyStore.CreateKey();

		// Token: 0x0400080F RID: 2063
		private const string fontMeasureString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
	}
}
