using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Represents a window or dialog box that makes up an application's user interface.</summary>
	// Token: 0x0200025B RID: 603
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ToolboxItemFilter("System.Windows.Forms.Control.TopLevel")]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[Designer("System.Windows.Forms.Design.FormDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[DesignerCategory("Form")]
	[DefaultEvent("Load")]
	[InitializationEvent("Load")]
	public class Form : ContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Form" /> class.</summary>
		// Token: 0x06002604 RID: 9732 RVA: 0x000B05B4 File Offset: 0x000AE7B4
		public Form()
		{
			bool isRestrictedWindow = this.IsRestrictedWindow;
			this.formStateEx[Form.FormStateExShowIcon] = 1;
			base.SetState(2, false);
			base.SetState(524288, true);
		}

		/// <summary>Gets or sets the button on the form that is clicked when the user presses the ENTER key.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IButtonControl" /> that represents the button to use as the accept button for the form.</returns>
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x000B0637 File Offset: 0x000AE837
		// (set) Token: 0x06002606 RID: 9734 RVA: 0x000B064E File Offset: 0x000AE84E
		[DefaultValue(null)]
		[SRDescription("FormAcceptButtonDescr")]
		public IButtonControl AcceptButton
		{
			get
			{
				return (IButtonControl)base.Properties.GetObject(Form.PropAcceptButton);
			}
			set
			{
				if (this.AcceptButton != value)
				{
					base.Properties.SetObject(Form.PropAcceptButton, value);
					this.UpdateDefaultButton();
				}
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x000B0670 File Offset: 0x000AE870
		// (set) Token: 0x06002608 RID: 9736 RVA: 0x000B06AC File Offset: 0x000AE8AC
		internal bool Active
		{
			get
			{
				Form parentFormInternal = base.ParentFormInternal;
				if (parentFormInternal == null)
				{
					return this.formState[Form.FormStateIsActive] != 0;
				}
				return parentFormInternal.ActiveControl == this && parentFormInternal.Active;
			}
			set
			{
				if (this.formState[Form.FormStateIsActive] != 0 != value)
				{
					if (value && !this.CanRecreateHandle())
					{
						return;
					}
					this.formState[Form.FormStateIsActive] = (value ? 1 : 0);
					if (value)
					{
						this.formState[Form.FormStateIsWindowActivated] = 1;
						if (this.IsRestrictedWindow)
						{
							this.WindowText = this.userWindowText;
						}
						if (!base.ValidationCancelled)
						{
							if (base.ActiveControl == null)
							{
								base.SelectNextControlInternal(null, true, true, true, false);
							}
							base.InnerMostActiveContainerControl.FocusActiveControlInternal();
						}
						this.OnActivated(EventArgs.Empty);
						return;
					}
					this.formState[Form.FormStateIsWindowActivated] = 0;
					if (this.IsRestrictedWindow)
					{
						this.Text = this.userWindowText;
					}
					this.OnDeactivate(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the currently active form for this application.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> that represents the currently active form, or <see langword="null" /> if there is no active form.</returns>
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x000B0780 File Offset: 0x000AE980
		public static Form ActiveForm
		{
			get
			{
				IntSecurity.GetParent.Demand();
				IntPtr foregroundWindow = UnsafeNativeMethods.GetForegroundWindow();
				Control control = Control.FromHandleInternal(foregroundWindow);
				if (control != null && control is Form)
				{
					return (Form)control;
				}
				return null;
			}
		}

		/// <summary>Gets the currently active multiple-document interface (MDI) child window.</summary>
		/// <returns>Returns a <see cref="T:System.Windows.Forms.Form" /> that represents the currently active MDI child window, or <see langword="null" /> if there are currently no child windows present.</returns>
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x000B07B8 File Offset: 0x000AE9B8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormActiveMDIChildDescr")]
		public Form ActiveMdiChild
		{
			get
			{
				Form form = this.ActiveMdiChildInternal;
				if (form == null && this.ctlClient != null && this.ctlClient.IsHandleCreated)
				{
					IntPtr intPtr = this.ctlClient.SendMessage(553, 0, 0);
					form = Control.FromHandleInternal(intPtr) as Form;
				}
				if (form != null && form.Visible && form.Enabled)
				{
					return form;
				}
				return null;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x000B0819 File Offset: 0x000AEA19
		// (set) Token: 0x0600260C RID: 9740 RVA: 0x000B0830 File Offset: 0x000AEA30
		internal Form ActiveMdiChildInternal
		{
			get
			{
				return (Form)base.Properties.GetObject(Form.PropActiveMdiChild);
			}
			set
			{
				base.Properties.SetObject(Form.PropActiveMdiChild, value);
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x000B0843 File Offset: 0x000AEA43
		// (set) Token: 0x0600260E RID: 9742 RVA: 0x000B085A File Offset: 0x000AEA5A
		private Form FormerlyActiveMdiChild
		{
			get
			{
				return (Form)base.Properties.GetObject(Form.PropFormerlyActiveMdiChild);
			}
			set
			{
				base.Properties.SetObject(Form.PropFormerlyActiveMdiChild, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the opacity of the form can be adjusted.</summary>
		/// <returns>
		///   <see langword="true" /> if the opacity of the form can be changed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x000B086D File Offset: 0x000AEA6D
		// (set) Token: 0x06002610 RID: 9744 RVA: 0x000B0884 File Offset: 0x000AEA84
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlAllowTransparencyDescr")]
		public bool AllowTransparency
		{
			get
			{
				return this.formState[Form.FormStateAllowTransparency] != 0;
			}
			set
			{
				if (value != (this.formState[Form.FormStateAllowTransparency] != 0) && OSFeature.Feature.IsPresent(OSFeature.LayeredWindows))
				{
					this.formState[Form.FormStateAllowTransparency] = (value ? 1 : 0);
					this.formState[Form.FormStateLayered] = this.formState[Form.FormStateAllowTransparency];
					base.UpdateStyles();
					if (!value)
					{
						if (base.Properties.ContainsObject(Form.PropOpacity))
						{
							base.Properties.SetObject(Form.PropOpacity, 1f);
						}
						if (base.Properties.ContainsObject(Form.PropTransparencyKey))
						{
							base.Properties.SetObject(Form.PropTransparencyKey, Color.Empty);
						}
						this.UpdateLayered();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the form adjusts its size to fit the height of the font used on the form and scales its controls.</summary>
		/// <returns>
		///   <see langword="true" /> if the form will automatically scale itself and its controls based on the current font assigned to the form; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x000B095C File Offset: 0x000AEB5C
		// (set) Token: 0x06002612 RID: 9746 RVA: 0x000B0974 File Offset: 0x000AEB74
		[SRCategory("CatLayout")]
		[SRDescription("FormAutoScaleDescr")]
		[Obsolete("This property has been deprecated. Use the AutoScaleMode property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AutoScale
		{
			get
			{
				return this.formState[Form.FormStateAutoScaling] != 0;
			}
			set
			{
				this.formStateEx[Form.FormStateExSettingAutoScale] = 1;
				try
				{
					if (value)
					{
						this.formState[Form.FormStateAutoScaling] = 1;
						base.AutoScaleMode = AutoScaleMode.None;
					}
					else
					{
						this.formState[Form.FormStateAutoScaling] = 0;
					}
				}
				finally
				{
					this.formStateEx[Form.FormStateExSettingAutoScale] = 0;
				}
			}
		}

		/// <summary>Gets or sets the base size used for autoscaling of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the base size that this form uses for autoscaling.</returns>
		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x000B09E4 File Offset: 0x000AEBE4
		// (set) Token: 0x06002614 RID: 9748 RVA: 0x000B0A32 File Offset: 0x000AEC32
		[Localizable(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual Size AutoScaleBaseSize
		{
			get
			{
				if (this.autoScaleBaseSize.IsEmpty)
				{
					SizeF autoScaleSize = Form.GetAutoScaleSize(this.Font);
					return new Size((int)Math.Round((double)autoScaleSize.Width), (int)Math.Round((double)autoScaleSize.Height));
				}
				return this.autoScaleBaseSize;
			}
			set
			{
				this.autoScaleBaseSize = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form enables autoscrolling.</summary>
		/// <returns>
		///   <see langword="true" /> to enable autoscrolling on the form; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x000B0A3B File Offset: 0x000AEC3B
		// (set) Token: 0x06002616 RID: 9750 RVA: 0x000B0A43 File Offset: 0x000AEC43
		[Localizable(true)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				if (value)
				{
					this.IsMdiContainer = false;
				}
				base.AutoScroll = value;
			}
		}

		/// <summary>Resize the form according to the setting of <see cref="P:System.Windows.Forms.Form.AutoSizeMode" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the form will automatically resize; <see langword="false" /> if it must be manually resized.</returns>
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x000B0A56 File Offset: 0x000AEC56
		// (set) Token: 0x06002618 RID: 9752 RVA: 0x000B0A6C File Offset: 0x000AEC6C
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get
			{
				return this.formStateEx[Form.FormStateExAutoSize] != 0;
			}
			set
			{
				if (value != this.AutoSize)
				{
					this.formStateEx[Form.FormStateExAutoSize] = (value ? 1 : 0);
					if (!this.AutoSize)
					{
						this.minAutoSize = Size.Empty;
						this.Size = CommonProperties.GetSpecifiedBounds(this).Size;
					}
					LayoutTransaction.DoLayout(this, this, PropertyNames.AutoSize);
					this.OnAutoSizeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Form.AutoSize" /> property changes.</summary>
		// Token: 0x1400019D RID: 413
		// (add) Token: 0x06002619 RID: 9753 RVA: 0x00011842 File Offset: 0x0000FA42
		// (remove) Token: 0x0600261A RID: 9754 RVA: 0x0001184B File Offset: 0x0000FA4B
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnAutoSizeChangedDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		/// <summary>Gets or sets the mode by which the form automatically resizes itself.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AutoSizeMode" /> enumerated value. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not a valid <see cref="T:System.Windows.Forms.AutoSizeMode" /> value.</exception>
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x00023551 File Offset: 0x00021751
		// (set) Token: 0x0600261C RID: 9756 RVA: 0x000B0AD8 File Offset: 0x000AECD8
		[SRDescription("ControlAutoSizeModeDescr")]
		[SRCategory("CatLayout")]
		[Browsable(true)]
		[DefaultValue(AutoSizeMode.GrowOnly)]
		[Localizable(true)]
		public AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.GetAutoSizeMode();
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoSizeMode));
				}
				if (base.GetAutoSizeMode() != value)
				{
					base.SetAutoSizeMode(value);
					Control control = ((base.DesignMode || this.ParentInternal == null) ? this : this.ParentInternal);
					if (control != null)
					{
						if (control.LayoutEngine == DefaultLayout.Instance)
						{
							control.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
						}
						LayoutTransaction.DoLayout(control, this, PropertyNames.AutoSize);
					}
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether controls in this container will be automatically validated when the focus changes.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AutoValidate" /> enumerated value that indicates whether contained controls are implicitly validated on focus change. The default is Inherit.</returns>
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x0600261D RID: 9757 RVA: 0x000B0B5F File Offset: 0x000AED5F
		// (set) Token: 0x0600261E RID: 9758 RVA: 0x000B0B67 File Offset: 0x000AED67
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override AutoValidate AutoValidate
		{
			get
			{
				return base.AutoValidate;
			}
			set
			{
				base.AutoValidate = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Form.AutoValidate" /> property changes.</summary>
		// Token: 0x1400019E RID: 414
		// (add) Token: 0x0600261F RID: 9759 RVA: 0x000B0B70 File Offset: 0x000AED70
		// (remove) Token: 0x06002620 RID: 9760 RVA: 0x000B0B79 File Offset: 0x000AED79
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler AutoValidateChanged
		{
			add
			{
				base.AutoValidateChanged += value;
			}
			remove
			{
				base.AutoValidateChanged -= value;
			}
		}

		/// <summary>Gets or sets the background color for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x000B0B84 File Offset: 0x000AED84
		// (set) Token: 0x06002622 RID: 9762 RVA: 0x00012D84 File Offset: 0x00010F84
		public override Color BackColor
		{
			get
			{
				Color rawBackColor = base.RawBackColor;
				if (!rawBackColor.IsEmpty)
				{
					return rawBackColor;
				}
				return Control.DefaultBackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x000B0BA8 File Offset: 0x000AEDA8
		// (set) Token: 0x06002624 RID: 9764 RVA: 0x000B0BBD File Offset: 0x000AEDBD
		private bool CalledClosing
		{
			get
			{
				return this.formStateEx[Form.FormStateExCalledClosing] != 0;
			}
			set
			{
				this.formStateEx[Form.FormStateExCalledClosing] = (value ? 1 : 0);
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002625 RID: 9765 RVA: 0x000B0BD6 File Offset: 0x000AEDD6
		// (set) Token: 0x06002626 RID: 9766 RVA: 0x000B0BEB File Offset: 0x000AEDEB
		private bool CalledCreateControl
		{
			get
			{
				return this.formStateEx[Form.FormStateExCalledCreateControl] != 0;
			}
			set
			{
				this.formStateEx[Form.FormStateExCalledCreateControl] = (value ? 1 : 0);
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x000B0C04 File Offset: 0x000AEE04
		// (set) Token: 0x06002628 RID: 9768 RVA: 0x000B0C19 File Offset: 0x000AEE19
		private bool CalledMakeVisible
		{
			get
			{
				return this.formStateEx[Form.FormStateExCalledMakeVisible] != 0;
			}
			set
			{
				this.formStateEx[Form.FormStateExCalledMakeVisible] = (value ? 1 : 0);
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x000B0C32 File Offset: 0x000AEE32
		// (set) Token: 0x0600262A RID: 9770 RVA: 0x000B0C47 File Offset: 0x000AEE47
		private bool CalledOnLoad
		{
			get
			{
				return this.formStateEx[Form.FormStateExCalledOnLoad] != 0;
			}
			set
			{
				this.formStateEx[Form.FormStateExCalledOnLoad] = (value ? 1 : 0);
			}
		}

		/// <summary>Gets or sets the border style of the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormBorderStyle" /> that represents the style of border to display for the form. The default is <see langword="FormBorderStyle.Sizable" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values.</exception>
		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x000B0C60 File Offset: 0x000AEE60
		// (set) Token: 0x0600262C RID: 9772 RVA: 0x000B0C74 File Offset: 0x000AEE74
		[SRCategory("CatAppearance")]
		[DefaultValue(FormBorderStyle.Sizable)]
		[DispId(-504)]
		[SRDescription("FormBorderStyleDescr")]
		public FormBorderStyle FormBorderStyle
		{
			get
			{
				return (FormBorderStyle)this.formState[Form.FormStateBorderStyle];
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 6))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FormBorderStyle));
				}
				if (this.IsRestrictedWindow)
				{
					switch (value)
					{
					case FormBorderStyle.None:
						value = FormBorderStyle.FixedSingle;
						break;
					case FormBorderStyle.FixedSingle:
					case FormBorderStyle.Fixed3D:
					case FormBorderStyle.FixedDialog:
					case FormBorderStyle.Sizable:
						break;
					case FormBorderStyle.FixedToolWindow:
						value = FormBorderStyle.FixedSingle;
						break;
					case FormBorderStyle.SizableToolWindow:
						value = FormBorderStyle.Sizable;
						break;
					default:
						value = FormBorderStyle.Sizable;
						break;
					}
				}
				this.formState[Form.FormStateBorderStyle] = (int)value;
				if (this.formState[Form.FormStateSetClientSize] == 1 && !base.IsHandleCreated)
				{
					this.ClientSize = this.ClientSize;
				}
				Rectangle rectangle = this.restoredWindowBounds;
				BoundsSpecified boundsSpecified = this.restoredWindowBoundsSpecified;
				int num = this.formStateEx[Form.FormStateExWindowBoundsWidthIsClientSize];
				int num2 = this.formStateEx[Form.FormStateExWindowBoundsHeightIsClientSize];
				this.UpdateFormStyles();
				if (this.formState[Form.FormStateIconSet] == 0 && !this.IsRestrictedWindow)
				{
					this.UpdateWindowIcon(false);
				}
				if (this.WindowState != FormWindowState.Normal)
				{
					this.restoredWindowBounds = rectangle;
					this.restoredWindowBoundsSpecified = boundsSpecified;
					this.formStateEx[Form.FormStateExWindowBoundsWidthIsClientSize] = num;
					this.formStateEx[Form.FormStateExWindowBoundsHeightIsClientSize] = num2;
				}
			}
		}

		/// <summary>Gets or sets the button control that is clicked when the user presses the ESC key.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IButtonControl" /> that represents the cancel button for the form.</returns>
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x000B0DAC File Offset: 0x000AEFAC
		// (set) Token: 0x0600262E RID: 9774 RVA: 0x000B0DC3 File Offset: 0x000AEFC3
		[DefaultValue(null)]
		[SRDescription("FormCancelButtonDescr")]
		public IButtonControl CancelButton
		{
			get
			{
				return (IButtonControl)base.Properties.GetObject(Form.PropCancelButton);
			}
			set
			{
				base.Properties.SetObject(Form.PropCancelButton, value);
				if (value != null && value.DialogResult == DialogResult.None)
				{
					value.DialogResult = DialogResult.Cancel;
				}
			}
		}

		/// <summary>Gets or sets the size of the client area of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the form's client area.</returns>
		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x000B0DE8 File Offset: 0x000AEFE8
		// (set) Token: 0x06002630 RID: 9776 RVA: 0x000B0DF0 File Offset: 0x000AEFF0
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new Size ClientSize
		{
			get
			{
				return base.ClientSize;
			}
			set
			{
				base.ClientSize = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a control box is displayed in the caption bar of the form.</summary>
		/// <returns>
		///   <see langword="true" /> if the form displays a control box in the upper-right corner of the form; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x000B0DF9 File Offset: 0x000AEFF9
		// (set) Token: 0x06002632 RID: 9778 RVA: 0x000B0E0E File Offset: 0x000AF00E
		[SRCategory("CatWindowStyle")]
		[DefaultValue(true)]
		[SRDescription("FormControlBoxDescr")]
		public bool ControlBox
		{
			get
			{
				return this.formState[Form.FormStateControlBox] != 0;
			}
			set
			{
				if (this.IsRestrictedWindow)
				{
					return;
				}
				if (value)
				{
					this.formState[Form.FormStateControlBox] = 1;
				}
				else
				{
					this.formState[Form.FormStateControlBox] = 0;
				}
				this.UpdateFormStyles();
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x000B0E48 File Offset: 0x000AF048
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				if (base.IsHandleCreated && (base.WindowStyle & 134217728) != 0)
				{
					createParams.Style |= 134217728;
				}
				else if (this.TopLevel)
				{
					createParams.Style &= -134217729;
				}
				if (this.TopLevel && this.formState[Form.FormStateLayered] != 0)
				{
					createParams.ExStyle |= 524288;
				}
				IWin32Window win32Window = (IWin32Window)base.Properties.GetObject(Form.PropDialogOwner);
				if (win32Window != null)
				{
					createParams.Parent = Control.GetSafeHandle(win32Window);
				}
				this.FillInCreateParamsBorderStyles(createParams);
				this.FillInCreateParamsWindowState(createParams);
				this.FillInCreateParamsBorderIcons(createParams);
				if (this.formState[Form.FormStateTaskBar] != 0)
				{
					createParams.ExStyle |= 262144;
				}
				FormBorderStyle formBorderStyle = this.FormBorderStyle;
				if (!this.ShowIcon && (formBorderStyle == FormBorderStyle.Sizable || formBorderStyle == FormBorderStyle.Fixed3D || formBorderStyle == FormBorderStyle.FixedSingle))
				{
					createParams.ExStyle |= 1;
				}
				if (this.IsMdiChild)
				{
					if (base.Visible && (this.WindowState == FormWindowState.Maximized || this.WindowState == FormWindowState.Normal))
					{
						Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
						Form activeMdiChildInternal = form.ActiveMdiChildInternal;
						if (activeMdiChildInternal != null && activeMdiChildInternal.WindowState == FormWindowState.Maximized)
						{
							createParams.Style |= 16777216;
							this.formState[Form.FormStateWindowState] = 2;
							base.SetState(65536, true);
						}
					}
					if (this.formState[Form.FormStateMdiChildMax] != 0)
					{
						createParams.Style |= 16777216;
					}
					createParams.ExStyle |= 64;
				}
				if (this.TopLevel || this.IsMdiChild)
				{
					this.FillInCreateParamsStartPosition(createParams);
					if ((createParams.Style & 268435456) != 0)
					{
						this.formState[Form.FormStateShowWindowOnCreate] = 1;
						createParams.Style &= -268435457;
					}
					else
					{
						this.formState[Form.FormStateShowWindowOnCreate] = 0;
					}
				}
				if (this.IsRestrictedWindow)
				{
					createParams.Caption = this.RestrictedWindowText(createParams.Caption);
				}
				if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
				{
					createParams.ExStyle |= 5242880;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x000B10B0 File Offset: 0x000AF2B0
		// (set) Token: 0x06002635 RID: 9781 RVA: 0x000B10B8 File Offset: 0x000AF2B8
		internal CloseReason CloseReason
		{
			get
			{
				return this.closeReason;
			}
			set
			{
				this.closeReason = value;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x000B10C4 File Offset: 0x000AF2C4
		internal static Icon DefaultIcon
		{
			get
			{
				if (Form.defaultIcon == null)
				{
					object obj = Form.internalSyncObject;
					lock (obj)
					{
						if (Form.defaultIcon == null)
						{
							Form.defaultIcon = new Icon(typeof(Form), "wfc.ico");
						}
					}
				}
				return Form.defaultIcon;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.NoControl;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x000B112C File Offset: 0x000AF32C
		private static Icon DefaultRestrictedIcon
		{
			get
			{
				if (Form.defaultRestrictedIcon == null)
				{
					object obj = Form.internalSyncObject;
					lock (obj)
					{
						if (Form.defaultRestrictedIcon == null)
						{
							Form.defaultRestrictedIcon = new Icon(typeof(Form), "wfsecurity.ico");
						}
					}
				}
				return Form.defaultRestrictedIcon;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x000B1194 File Offset: 0x000AF394
		protected override Size DefaultSize
		{
			get
			{
				return new Size(300, 300);
			}
		}

		/// <summary>Gets or sets the size and location of the form on the Windows desktop.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the form on the Windows desktop using desktop coordinates.</returns>
		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x000B11A8 File Offset: 0x000AF3A8
		// (set) Token: 0x0600263B RID: 9787 RVA: 0x000B11ED File Offset: 0x000AF3ED
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormDesktopBoundsDescr")]
		public Rectangle DesktopBounds
		{
			get
			{
				Rectangle workingArea = SystemInformation.WorkingArea;
				Rectangle bounds = base.Bounds;
				bounds.X -= workingArea.X;
				bounds.Y -= workingArea.Y;
				return bounds;
			}
			set
			{
				this.SetDesktopBounds(value.X, value.Y, value.Width, value.Height);
			}
		}

		/// <summary>Gets or sets the location of the form on the Windows desktop.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the location of the form on the desktop.</returns>
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x000B1214 File Offset: 0x000AF414
		// (set) Token: 0x0600263D RID: 9789 RVA: 0x000B1259 File Offset: 0x000AF459
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormDesktopLocationDescr")]
		public Point DesktopLocation
		{
			get
			{
				Rectangle workingArea = SystemInformation.WorkingArea;
				Point location = this.Location;
				location.X -= workingArea.X;
				location.Y -= workingArea.Y;
				return location;
			}
			set
			{
				this.SetDesktopLocation(value.X, value.Y);
			}
		}

		/// <summary>Gets or sets the dialog result for the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DialogResult" /> that represents the result of the form when used as a dialog box.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values.</exception>
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x000B126F File Offset: 0x000AF46F
		// (set) Token: 0x0600263F RID: 9791 RVA: 0x000B1277 File Offset: 0x000AF477
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormDialogResultDescr")]
		public DialogResult DialogResult
		{
			get
			{
				return this.dialogResult;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DialogResult));
				}
				this.dialogResult = value;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x000B12A8 File Offset: 0x000AF4A8
		internal override bool HasMenu
		{
			get
			{
				bool flag = false;
				Menu menu = this.Menu;
				if (this.TopLevel && menu != null && menu.ItemCount > 0)
				{
					flag = true;
				}
				return flag;
			}
		}

		/// <summary>Gets or sets a value indicating whether a Help button should be displayed in the caption box of the form.</summary>
		/// <returns>
		///   <see langword="true" /> to display a Help button in the form's caption bar; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x000B12D5 File Offset: 0x000AF4D5
		// (set) Token: 0x06002642 RID: 9794 RVA: 0x000B12EA File Offset: 0x000AF4EA
		[SRCategory("CatWindowStyle")]
		[DefaultValue(false)]
		[SRDescription("FormHelpButtonDescr")]
		public bool HelpButton
		{
			get
			{
				return this.formState[Form.FormStateHelpButton] != 0;
			}
			set
			{
				if (value)
				{
					this.formState[Form.FormStateHelpButton] = 1;
				}
				else
				{
					this.formState[Form.FormStateHelpButton] = 0;
				}
				this.UpdateFormStyles();
			}
		}

		/// <summary>Occurs when the Help button is clicked.</summary>
		// Token: 0x1400019F RID: 415
		// (add) Token: 0x06002643 RID: 9795 RVA: 0x000B1319 File Offset: 0x000AF519
		// (remove) Token: 0x06002644 RID: 9796 RVA: 0x000B132C File Offset: 0x000AF52C
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[SRCategory("CatBehavior")]
		[SRDescription("FormHelpButtonClickedDescr")]
		public event CancelEventHandler HelpButtonClicked
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_HELPBUTTONCLICKED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_HELPBUTTONCLICKED, value);
			}
		}

		/// <summary>Gets or sets the icon for the form.</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> that represents the icon for the form.</returns>
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002645 RID: 9797 RVA: 0x000B133F File Offset: 0x000AF53F
		// (set) Token: 0x06002646 RID: 9798 RVA: 0x000B1370 File Offset: 0x000AF570
		[AmbientValue(null)]
		[Localizable(true)]
		[SRCategory("CatWindowStyle")]
		[SRDescription("FormIconDescr")]
		public Icon Icon
		{
			get
			{
				if (this.formState[Form.FormStateIconSet] != 0)
				{
					return this.icon;
				}
				if (this.IsRestrictedWindow)
				{
					return Form.DefaultRestrictedIcon;
				}
				return Form.DefaultIcon;
			}
			set
			{
				if (this.icon != value && !this.IsRestrictedWindow)
				{
					if (value == Form.defaultIcon)
					{
						value = null;
					}
					this.formState[Form.FormStateIconSet] = ((value == null) ? 0 : 1);
					this.icon = value;
					if (this.smallIcon != null)
					{
						this.smallIcon.Dispose();
						this.smallIcon = null;
					}
					this.UpdateWindowIcon(true);
				}
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x000B13D8 File Offset: 0x000AF5D8
		// (set) Token: 0x06002648 RID: 9800 RVA: 0x000B13ED File Offset: 0x000AF5ED
		private bool IsClosing
		{
			get
			{
				return this.formStateEx[Form.FormStateExWindowClosing] == 1;
			}
			set
			{
				this.formStateEx[Form.FormStateExWindowClosing] = (value ? 1 : 0);
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x000B1406 File Offset: 0x000AF606
		private bool IsMaximized
		{
			get
			{
				return this.WindowState == FormWindowState.Maximized || (this.IsMdiChild && this.formState[Form.FormStateMdiChildMax] == 1);
			}
		}

		/// <summary>Gets a value indicating whether the form is a multiple-document interface (MDI) child form.</summary>
		/// <returns>
		///   <see langword="true" /> if the form is an MDI child form; otherwise, <see langword="false" />.</returns>
		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x0600264A RID: 9802 RVA: 0x000B1430 File Offset: 0x000AF630
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormIsMDIChildDescr")]
		public bool IsMdiChild
		{
			get
			{
				return base.Properties.GetObject(Form.PropFormMdiParent) != null;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x000B1445 File Offset: 0x000AF645
		// (set) Token: 0x0600264C RID: 9804 RVA: 0x000B1470 File Offset: 0x000AF670
		internal bool IsMdiChildFocusable
		{
			get
			{
				return base.Properties.ContainsObject(Form.PropMdiChildFocusable) && (bool)base.Properties.GetObject(Form.PropMdiChildFocusable);
			}
			set
			{
				if (value != this.IsMdiChildFocusable)
				{
					base.Properties.SetObject(Form.PropMdiChildFocusable, value);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the form is a container for multiple-document interface (MDI) child forms.</summary>
		/// <returns>
		///   <see langword="true" /> if the form is a container for MDI child forms; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x000B1491 File Offset: 0x000AF691
		// (set) Token: 0x0600264E RID: 9806 RVA: 0x000B149C File Offset: 0x000AF69C
		[SRCategory("CatWindowStyle")]
		[DefaultValue(false)]
		[SRDescription("FormIsMDIContainerDescr")]
		public bool IsMdiContainer
		{
			get
			{
				return this.ctlClient != null;
			}
			set
			{
				if (value == this.IsMdiContainer)
				{
					return;
				}
				if (value)
				{
					this.AllowTransparency = false;
					base.Controls.Add(new MdiClient());
				}
				else
				{
					this.ActiveMdiChildInternal = null;
					this.ctlClient.Dispose();
				}
				base.Invalidate();
			}
		}

		/// <summary>Gets a value indicating whether the form can use all windows and user input events without restriction.</summary>
		/// <returns>
		///   <see langword="true" /> if the form has restrictions; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x000B14DC File Offset: 0x000AF6DC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsRestrictedWindow
		{
			get
			{
				if (this.formState[Form.FormStateIsRestrictedWindowChecked] == 0)
				{
					this.formState[Form.FormStateIsRestrictedWindow] = 0;
					try
					{
						IntSecurity.WindowAdornmentModification.Demand();
					}
					catch (SecurityException)
					{
						this.formState[Form.FormStateIsRestrictedWindow] = 1;
					}
					catch
					{
						this.formState[Form.FormStateIsRestrictedWindow] = 1;
						this.formState[Form.FormStateIsRestrictedWindowChecked] = 1;
						throw;
					}
					this.formState[Form.FormStateIsRestrictedWindowChecked] = 1;
				}
				return this.formState[Form.FormStateIsRestrictedWindow] != 0;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form will receive key events before the event is passed to the control that has focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the form will receive all key events; <see langword="false" /> if the currently selected control on the form receives key events. The default is <see langword="false" />.</returns>
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x000B1594 File Offset: 0x000AF794
		// (set) Token: 0x06002651 RID: 9809 RVA: 0x000B15A9 File Offset: 0x000AF7A9
		[DefaultValue(false)]
		[SRDescription("FormKeyPreviewDescr")]
		public bool KeyPreview
		{
			get
			{
				return this.formState[Form.FormStateKeyPreview] != 0;
			}
			set
			{
				if (value)
				{
					this.formState[Form.FormStateKeyPreview] = 1;
					return;
				}
				this.formState[Form.FormStateKeyPreview] = 0;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the <see cref="T:System.Windows.Forms.Form" /> in screen coordinates.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the <see cref="T:System.Windows.Forms.Form" /> in screen coordinates.</returns>
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x000B15D1 File Offset: 0x000AF7D1
		// (set) Token: 0x06002653 RID: 9811 RVA: 0x000B15D9 File Offset: 0x000AF7D9
		[SettingsBindable(true)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		/// <summary>Gets or sets the size of the form when it is maximized.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the form when it is maximized.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Drawing.Rectangle.Top" /> property is greater than the height of the form.  
		///  -or-  
		///  The value of the <see cref="P:System.Drawing.Rectangle.Left" /> property is greater than the width of the form.</exception>
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x000B15E2 File Offset: 0x000AF7E2
		// (set) Token: 0x06002655 RID: 9813 RVA: 0x000B15F4 File Offset: 0x000AF7F4
		protected Rectangle MaximizedBounds
		{
			get
			{
				return base.Properties.GetRectangle(Form.PropMaximizedBounds);
			}
			set
			{
				if (!value.Equals(this.MaximizedBounds))
				{
					base.Properties.SetRectangle(Form.PropMaximizedBounds, value);
					this.OnMaximizedBoundsChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Form.MaximizedBounds" /> property has changed.</summary>
		// Token: 0x140001A0 RID: 416
		// (add) Token: 0x06002656 RID: 9814 RVA: 0x000B162C File Offset: 0x000AF82C
		// (remove) Token: 0x06002657 RID: 9815 RVA: 0x000B163F File Offset: 0x000AF83F
		[SRCategory("CatPropertyChanged")]
		[SRDescription("FormOnMaximizedBoundsChangedDescr")]
		public event EventHandler MaximizedBoundsChanged
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_MAXIMIZEDBOUNDSCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_MAXIMIZEDBOUNDSCHANGED, value);
			}
		}

		/// <summary>Gets the maximum size the form can be resized to.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the maximum size for the form.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The values of the height or width within the <see cref="T:System.Drawing.Size" /> object are less than zero.</exception>
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x000B1652 File Offset: 0x000AF852
		// (set) Token: 0x06002659 RID: 9817 RVA: 0x000B1694 File Offset: 0x000AF894
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("FormMaximumSizeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(typeof(Size), "0, 0")]
		public override Size MaximumSize
		{
			get
			{
				if (base.Properties.ContainsInteger(Form.PropMaxTrackSizeWidth))
				{
					return new Size(base.Properties.GetInteger(Form.PropMaxTrackSizeWidth), base.Properties.GetInteger(Form.PropMaxTrackSizeHeight));
				}
				return Size.Empty;
			}
			set
			{
				if (!value.Equals(this.MaximumSize))
				{
					if (value.Width < 0 || value.Height < 0)
					{
						throw new ArgumentOutOfRangeException("MaximumSize");
					}
					base.Properties.SetInteger(Form.PropMaxTrackSizeWidth, value.Width);
					base.Properties.SetInteger(Form.PropMaxTrackSizeHeight, value.Height);
					if (!this.MinimumSize.IsEmpty && !value.IsEmpty)
					{
						if (base.Properties.GetInteger(Form.PropMinTrackSizeWidth) > value.Width)
						{
							base.Properties.SetInteger(Form.PropMinTrackSizeWidth, value.Width);
						}
						if (base.Properties.GetInteger(Form.PropMinTrackSizeHeight) > value.Height)
						{
							base.Properties.SetInteger(Form.PropMinTrackSizeHeight, value.Height);
						}
					}
					Size size = this.Size;
					if (!value.IsEmpty && (size.Width > value.Width || size.Height > value.Height))
					{
						this.Size = new Size(Math.Min(size.Width, value.Width), Math.Min(size.Height, value.Height));
					}
					this.OnMaximumSizeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Form.MaximumSize" /> property has changed.</summary>
		// Token: 0x140001A1 RID: 417
		// (add) Token: 0x0600265A RID: 9818 RVA: 0x000B17F0 File Offset: 0x000AF9F0
		// (remove) Token: 0x0600265B RID: 9819 RVA: 0x000B1803 File Offset: 0x000AFA03
		[SRCategory("CatPropertyChanged")]
		[SRDescription("FormOnMaximumSizeChangedDescr")]
		public event EventHandler MaximumSizeChanged
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_MAXIMUMSIZECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_MAXIMUMSIZECHANGED, value);
			}
		}

		/// <summary>Gets or sets the primary menu container for the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MenuStrip" /> that represents the container for the menu structure of the form. The default is <see langword="null" />.</returns>
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x000B1816 File Offset: 0x000AFA16
		// (set) Token: 0x0600265D RID: 9821 RVA: 0x000B182D File Offset: 0x000AFA2D
		[SRCategory("CatWindowStyle")]
		[DefaultValue(null)]
		[SRDescription("FormMenuStripDescr")]
		[TypeConverter(typeof(ReferenceConverter))]
		public MenuStrip MainMenuStrip
		{
			get
			{
				return (MenuStrip)base.Properties.GetObject(Form.PropMainMenuStrip);
			}
			set
			{
				base.Properties.SetObject(Form.PropMainMenuStrip, value);
				if (base.IsHandleCreated && this.Menu == null)
				{
					this.UpdateMenuHandles();
				}
			}
		}

		/// <summary>Gets or sets the space between controls.</summary>
		/// <returns>A value that represents the space between controls.</returns>
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x000B1856 File Offset: 0x000AFA56
		// (set) Token: 0x0600265F RID: 9823 RVA: 0x000B185E File Offset: 0x000AFA5E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Padding Margin
		{
			get
			{
				return base.Margin;
			}
			set
			{
				base.Margin = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Form.Margin" /> property changes.</summary>
		// Token: 0x140001A2 RID: 418
		// (add) Token: 0x06002660 RID: 9824 RVA: 0x000B1867 File Offset: 0x000AFA67
		// (remove) Token: 0x06002661 RID: 9825 RVA: 0x000B1870 File Offset: 0x000AFA70
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MarginChanged
		{
			add
			{
				base.MarginChanged += value;
			}
			remove
			{
				base.MarginChanged -= value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.MainMenu" /> that is displayed in the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MainMenu" /> that represents the menu to display in the form.</returns>
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x000B1879 File Offset: 0x000AFA79
		// (set) Token: 0x06002663 RID: 9827 RVA: 0x000B1890 File Offset: 0x000AFA90
		[SRCategory("CatWindowStyle")]
		[DefaultValue(null)]
		[SRDescription("FormMenuDescr")]
		[TypeConverter(typeof(ReferenceConverter))]
		[Browsable(false)]
		public MainMenu Menu
		{
			get
			{
				return (MainMenu)base.Properties.GetObject(Form.PropMainMenu);
			}
			set
			{
				MainMenu menu = this.Menu;
				if (menu != value)
				{
					if (menu != null)
					{
						menu.form = null;
					}
					base.Properties.SetObject(Form.PropMainMenu, value);
					if (value != null)
					{
						if (value.form != null)
						{
							value.form.Menu = null;
						}
						value.form = this;
					}
					if (this.formState[Form.FormStateSetClientSize] == 1 && !base.IsHandleCreated)
					{
						this.ClientSize = this.ClientSize;
					}
					this.MenuChanged(0, value);
				}
			}
		}

		/// <summary>Gets or sets the minimum size the form can be resized to.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum size for the form.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The values of the height or width within the <see cref="T:System.Drawing.Size" /> object are less than zero.</exception>
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x000B1910 File Offset: 0x000AFB10
		// (set) Token: 0x06002665 RID: 9829 RVA: 0x000B1950 File Offset: 0x000AFB50
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("FormMinimumSizeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public override Size MinimumSize
		{
			get
			{
				if (base.Properties.ContainsInteger(Form.PropMinTrackSizeWidth))
				{
					return new Size(base.Properties.GetInteger(Form.PropMinTrackSizeWidth), base.Properties.GetInteger(Form.PropMinTrackSizeHeight));
				}
				return this.DefaultMinimumSize;
			}
			set
			{
				if (!value.Equals(this.MinimumSize))
				{
					if (value.Width < 0 || value.Height < 0)
					{
						throw new ArgumentOutOfRangeException("MinimumSize");
					}
					Rectangle bounds = base.Bounds;
					bounds.Size = value;
					value = WindowsFormsUtils.ConstrainToScreenWorkingAreaBounds(bounds).Size;
					base.Properties.SetInteger(Form.PropMinTrackSizeWidth, value.Width);
					base.Properties.SetInteger(Form.PropMinTrackSizeHeight, value.Height);
					if (!this.MaximumSize.IsEmpty && !value.IsEmpty)
					{
						if (base.Properties.GetInteger(Form.PropMaxTrackSizeWidth) < value.Width)
						{
							base.Properties.SetInteger(Form.PropMaxTrackSizeWidth, value.Width);
						}
						if (base.Properties.GetInteger(Form.PropMaxTrackSizeHeight) < value.Height)
						{
							base.Properties.SetInteger(Form.PropMaxTrackSizeHeight, value.Height);
						}
					}
					Size size = this.Size;
					if (size.Width < value.Width || size.Height < value.Height)
					{
						this.Size = new Size(Math.Max(size.Width, value.Width), Math.Max(size.Height, value.Height));
					}
					if (base.IsHandleCreated)
					{
						SafeNativeMethods.SetWindowPos(new HandleRef(this, base.Handle), NativeMethods.NullHandleRef, this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height, 4);
					}
					this.OnMinimumSizeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Form.MinimumSize" /> property has changed.</summary>
		// Token: 0x140001A3 RID: 419
		// (add) Token: 0x06002666 RID: 9830 RVA: 0x000B1B1C File Offset: 0x000AFD1C
		// (remove) Token: 0x06002667 RID: 9831 RVA: 0x000B1B2F File Offset: 0x000AFD2F
		[SRCategory("CatPropertyChanged")]
		[SRDescription("FormOnMinimumSizeChangedDescr")]
		public event EventHandler MinimumSizeChanged
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_MINIMUMSIZECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_MINIMUMSIZECHANGED, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the Maximize button is displayed in the caption bar of the form.</summary>
		/// <returns>
		///   <see langword="true" /> to display a Maximize button for the form; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x000B1B42 File Offset: 0x000AFD42
		// (set) Token: 0x06002669 RID: 9833 RVA: 0x000B1B57 File Offset: 0x000AFD57
		[SRCategory("CatWindowStyle")]
		[DefaultValue(true)]
		[SRDescription("FormMaximizeBoxDescr")]
		public bool MaximizeBox
		{
			get
			{
				return this.formState[Form.FormStateMaximizeBox] != 0;
			}
			set
			{
				if (value)
				{
					this.formState[Form.FormStateMaximizeBox] = 1;
				}
				else
				{
					this.formState[Form.FormStateMaximizeBox] = 0;
				}
				this.UpdateFormStyles();
			}
		}

		/// <summary>Gets an array of forms that represent the multiple-document interface (MDI) child forms that are parented to this form.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.Form" /> objects, each of which identifies one of this form's MDI child forms.</returns>
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x000B1B86 File Offset: 0x000AFD86
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormMDIChildrenDescr")]
		public Form[] MdiChildren
		{
			get
			{
				if (this.ctlClient != null)
				{
					return this.ctlClient.MdiChildren;
				}
				return new Form[0];
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x000B1BA2 File Offset: 0x000AFDA2
		internal MdiClient MdiClient
		{
			get
			{
				return this.ctlClient;
			}
		}

		/// <summary>Gets or sets the current multiple-document interface (MDI) parent form of this form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> that represents the MDI parent form.</returns>
		/// <exception cref="T:System.Exception">The <see cref="T:System.Windows.Forms.Form" /> assigned to this property is not marked as an MDI container.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.Form" /> assigned to this property is both a child and an MDI container form.  
		///  -or-  
		///  The <see cref="T:System.Windows.Forms.Form" /> assigned to this property is located on a different thread.</exception>
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x000B1BAA File Offset: 0x000AFDAA
		// (set) Token: 0x0600266D RID: 9837 RVA: 0x000B1BBC File Offset: 0x000AFDBC
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormMDIParentDescr")]
		public Form MdiParent
		{
			get
			{
				IntSecurity.GetParent.Demand();
				return this.MdiParentInternal;
			}
			set
			{
				this.MdiParentInternal = value;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x000B1BC5 File Offset: 0x000AFDC5
		// (set) Token: 0x0600266F RID: 9839 RVA: 0x000B1BDC File Offset: 0x000AFDDC
		private Form MdiParentInternal
		{
			get
			{
				return (Form)base.Properties.GetObject(Form.PropFormMdiParent);
			}
			set
			{
				Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
				if (value == form && (value != null || this.ParentInternal == null))
				{
					return;
				}
				if (value != null && base.CreateThreadId != value.CreateThreadId)
				{
					throw new ArgumentException(SR.GetString("AddDifferentThreads"), "value");
				}
				bool state = base.GetState(2);
				base.Visible = false;
				try
				{
					if (value == null)
					{
						this.ParentInternal = null;
						base.SetTopLevel(true);
					}
					else
					{
						if (this.IsMdiContainer)
						{
							throw new ArgumentException(SR.GetString("FormMDIParentAndChild"), "value");
						}
						if (!value.IsMdiContainer)
						{
							throw new ArgumentException(SR.GetString("MDIParentNotContainer"), "value");
						}
						this.Dock = DockStyle.None;
						base.Properties.SetObject(Form.PropFormMdiParent, value);
						base.SetState(524288, false);
						this.ParentInternal = value.MdiClient;
						if (this.ParentInternal.IsHandleCreated && this.IsMdiChild && base.IsHandleCreated)
						{
							this.DestroyHandle();
						}
					}
					this.InvalidateMergedMenu();
					this.UpdateMenuHandles();
				}
				finally
				{
					base.UpdateStyles();
					base.Visible = state;
				}
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x000B1D14 File Offset: 0x000AFF14
		// (set) Token: 0x06002671 RID: 9841 RVA: 0x000B1D2B File Offset: 0x000AFF2B
		private MdiWindowListStrip MdiWindowListStrip
		{
			get
			{
				return base.Properties.GetObject(Form.PropMdiWindowListStrip) as MdiWindowListStrip;
			}
			set
			{
				base.Properties.SetObject(Form.PropMdiWindowListStrip, value);
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x000B1D3E File Offset: 0x000AFF3E
		// (set) Token: 0x06002673 RID: 9843 RVA: 0x000B1D55 File Offset: 0x000AFF55
		private MdiControlStrip MdiControlStrip
		{
			get
			{
				return base.Properties.GetObject(Form.PropMdiControlStrip) as MdiControlStrip;
			}
			set
			{
				base.Properties.SetObject(Form.PropMdiControlStrip, value);
			}
		}

		/// <summary>Gets the merged menu for the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MainMenu" /> that represents the merged menu of the form.</returns>
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x000B1D68 File Offset: 0x000AFF68
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormMergedMenuDescr")]
		public MainMenu MergedMenu
		{
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
			get
			{
				return this.MergedMenuPrivate;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x000B1D70 File Offset: 0x000AFF70
		private MainMenu MergedMenuPrivate
		{
			get
			{
				Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
				if (form == null)
				{
					return null;
				}
				MainMenu mainMenu = (MainMenu)base.Properties.GetObject(Form.PropMergedMenu);
				if (mainMenu != null)
				{
					return mainMenu;
				}
				MainMenu menu = form.Menu;
				MainMenu menu2 = this.Menu;
				if (menu2 == null)
				{
					return menu;
				}
				if (menu == null)
				{
					return menu2;
				}
				mainMenu = new MainMenu();
				mainMenu.ownerForm = this;
				mainMenu.MergeMenu(menu);
				mainMenu.MergeMenu(menu2);
				base.Properties.SetObject(Form.PropMergedMenu, mainMenu);
				return mainMenu;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Minimize button is displayed in the caption bar of the form.</summary>
		/// <returns>
		///   <see langword="true" /> to display a Minimize button for the form; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x000B1DF8 File Offset: 0x000AFFF8
		// (set) Token: 0x06002677 RID: 9847 RVA: 0x000B1E0D File Offset: 0x000B000D
		[SRCategory("CatWindowStyle")]
		[DefaultValue(true)]
		[SRDescription("FormMinimizeBoxDescr")]
		public bool MinimizeBox
		{
			get
			{
				return this.formState[Form.FormStateMinimizeBox] != 0;
			}
			set
			{
				if (value)
				{
					this.formState[Form.FormStateMinimizeBox] = 1;
				}
				else
				{
					this.formState[Form.FormStateMinimizeBox] = 0;
				}
				this.UpdateFormStyles();
			}
		}

		/// <summary>Gets a value indicating whether this form is displayed modally.</summary>
		/// <returns>
		///   <see langword="true" /> if the form is displayed modally; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x000B1E3C File Offset: 0x000B003C
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormModalDescr")]
		public bool Modal
		{
			get
			{
				return base.GetState(32);
			}
		}

		/// <summary>Gets or sets the opacity level of the form.</summary>
		/// <returns>The level of opacity for the form. The default is 1.00.</returns>
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x000B1E48 File Offset: 0x000B0048
		// (set) Token: 0x0600267A RID: 9850 RVA: 0x000B1E80 File Offset: 0x000B0080
		[SRCategory("CatWindowStyle")]
		[TypeConverter(typeof(OpacityConverter))]
		[SRDescription("FormOpacityDescr")]
		[DefaultValue(1.0)]
		public double Opacity
		{
			get
			{
				object @object = base.Properties.GetObject(Form.PropOpacity);
				if (@object != null)
				{
					return Convert.ToDouble(@object, CultureInfo.InvariantCulture);
				}
				return 1.0;
			}
			set
			{
				if (this.IsRestrictedWindow)
				{
					value = Math.Max(value, 0.5);
				}
				if (value > 1.0)
				{
					value = 1.0;
				}
				else if (value < 0.0)
				{
					value = 0.0;
				}
				base.Properties.SetObject(Form.PropOpacity, value);
				bool flag = this.formState[Form.FormStateLayered] != 0;
				if (this.OpacityAsByte < 255 && OSFeature.Feature.IsPresent(OSFeature.LayeredWindows))
				{
					this.AllowTransparency = true;
					if (this.formState[Form.FormStateLayered] != 1)
					{
						this.formState[Form.FormStateLayered] = 1;
						if (!flag)
						{
							base.UpdateStyles();
						}
					}
				}
				else
				{
					this.formState[Form.FormStateLayered] = ((this.TransparencyKey != Color.Empty) ? 1 : 0);
					if (flag != (this.formState[Form.FormStateLayered] != 0))
					{
						int num = (int)(long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -20);
						CreateParams createParams = this.CreateParams;
						if (num != createParams.ExStyle)
						{
							UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -20, new HandleRef(null, (IntPtr)createParams.ExStyle));
						}
					}
				}
				this.UpdateLayered();
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x000B1FEE File Offset: 0x000B01EE
		private byte OpacityAsByte
		{
			get
			{
				return (byte)(this.Opacity * 255.0);
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Windows.Forms.Form" /> objects that represent all forms that are owned by this form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> array that represents the owned forms for this form.</returns>
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x000B2004 File Offset: 0x000B0204
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormOwnedFormsDescr")]
		public Form[] OwnedForms
		{
			get
			{
				Form[] array = (Form[])base.Properties.GetObject(Form.PropOwnedForms);
				int integer = base.Properties.GetInteger(Form.PropOwnedFormsCount);
				Form[] array2 = new Form[integer];
				if (integer > 0)
				{
					Array.Copy(array, 0, array2, 0, integer);
				}
				return array2;
			}
		}

		/// <summary>Gets or sets the form that owns this form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> that represents the form that is the owner of this form.</returns>
		/// <exception cref="T:System.Exception">A top-level window cannot have an owner.</exception>
		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x000B204E File Offset: 0x000B024E
		// (set) Token: 0x0600267E RID: 9854 RVA: 0x000B2060 File Offset: 0x000B0260
		[SRCategory("CatWindowStyle")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormOwnerDescr")]
		public Form Owner
		{
			get
			{
				IntSecurity.GetParent.Demand();
				return this.OwnerInternal;
			}
			set
			{
				Form ownerInternal = this.OwnerInternal;
				if (ownerInternal == value)
				{
					return;
				}
				if (value != null && !this.TopLevel)
				{
					throw new ArgumentException(SR.GetString("NonTopLevelCantHaveOwner"), "value");
				}
				Control.CheckParentingCycle(this, value);
				Control.CheckParentingCycle(value, this);
				base.Properties.SetObject(Form.PropOwner, null);
				if (ownerInternal != null)
				{
					ownerInternal.RemoveOwnedForm(this);
				}
				base.Properties.SetObject(Form.PropOwner, value);
				if (value != null)
				{
					value.AddOwnedForm(this);
				}
				this.UpdateHandleWithOwner();
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x000B20E3 File Offset: 0x000B02E3
		internal Form OwnerInternal
		{
			get
			{
				return (Form)base.Properties.GetObject(Form.PropOwner);
			}
		}

		/// <summary>Gets the location and size of the form in its normal window state.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that contains the location and size of the form in the normal window state.</returns>
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x000B20FC File Offset: 0x000B02FC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Rectangle RestoreBounds
		{
			get
			{
				if (this.restoreBounds.Width == -1 && this.restoreBounds.Height == -1 && this.restoreBounds.X == -1 && this.restoreBounds.Y == -1)
				{
					return base.Bounds;
				}
				return this.restoreBounds;
			}
		}

		/// <summary>Gets or sets a value indicating whether right-to-left mirror placement is turned on.</summary>
		/// <returns>
		///   <see langword="true" /> if right-to-left mirror placement is turned on; otherwise, <see langword="false" /> for standard child control placement. The default is <see langword="false" />.</returns>
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x000B214E File Offset: 0x000B034E
		// (set) Token: 0x06002682 RID: 9858 RVA: 0x000B2158 File Offset: 0x000B0358
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("ControlRightToLeftLayoutDescr")]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.rightToLeftLayout;
			}
			set
			{
				if (value != this.rightToLeftLayout)
				{
					this.rightToLeftLayout = value;
					using (new LayoutTransaction(this, this, PropertyNames.RightToLeftLayout))
					{
						this.OnRightToLeftLayoutChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06002683 RID: 9859 RVA: 0x000B21AC File Offset: 0x000B03AC
		// (set) Token: 0x06002684 RID: 9860 RVA: 0x000B21B4 File Offset: 0x000B03B4
		internal override Control ParentInternal
		{
			get
			{
				return base.ParentInternal;
			}
			set
			{
				if (value != null)
				{
					this.Owner = null;
				}
				base.ParentInternal = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form is displayed in the Windows taskbar.</summary>
		/// <returns>
		///   <see langword="true" /> to display the form in the Windows taskbar at run time; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x000B21C7 File Offset: 0x000B03C7
		// (set) Token: 0x06002686 RID: 9862 RVA: 0x000B21DC File Offset: 0x000B03DC
		[DefaultValue(true)]
		[SRCategory("CatWindowStyle")]
		[SRDescription("FormShowInTaskbarDescr")]
		public bool ShowInTaskbar
		{
			get
			{
				return this.formState[Form.FormStateTaskBar] != 0;
			}
			set
			{
				if (this.IsRestrictedWindow)
				{
					return;
				}
				if (this.ShowInTaskbar != value)
				{
					if (value)
					{
						this.formState[Form.FormStateTaskBar] = 1;
					}
					else
					{
						this.formState[Form.FormStateTaskBar] = 0;
					}
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether an icon is displayed in the caption bar of the form.</summary>
		/// <returns>
		///   <see langword="true" /> if the form displays an icon in the caption bar; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x000B2230 File Offset: 0x000B0430
		// (set) Token: 0x06002688 RID: 9864 RVA: 0x000B2245 File Offset: 0x000B0445
		[DefaultValue(true)]
		[SRCategory("CatWindowStyle")]
		[SRDescription("FormShowIconDescr")]
		public bool ShowIcon
		{
			get
			{
				return this.formStateEx[Form.FormStateExShowIcon] != 0;
			}
			set
			{
				if (value)
				{
					this.formStateEx[Form.FormStateExShowIcon] = 1;
				}
				else
				{
					if (this.IsRestrictedWindow)
					{
						return;
					}
					this.formStateEx[Form.FormStateExShowIcon] = 0;
					base.UpdateStyles();
				}
				this.UpdateWindowIcon(true);
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002689 RID: 9865 RVA: 0x000B2284 File Offset: 0x000B0484
		internal override int ShowParams
		{
			get
			{
				FormWindowState windowState = this.WindowState;
				if (windowState == FormWindowState.Minimized)
				{
					return 2;
				}
				if (windowState == FormWindowState.Maximized)
				{
					return 3;
				}
				if (this.ShowWithoutActivation)
				{
					return 4;
				}
				return 5;
			}
		}

		/// <summary>Gets a value indicating whether the window will be activated when it is shown.</summary>
		/// <returns>
		///   <see langword="true" /> if the window will not be activated when it is shown; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x0001180C File Offset: 0x0000FA0C
		[Browsable(false)]
		protected virtual bool ShowWithoutActivation
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the size of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the form.</returns>
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x000B22AF File Offset: 0x000B04AF
		// (set) Token: 0x0600268C RID: 9868 RVA: 0x000B22B7 File Offset: 0x000B04B7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(false)]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		/// <summary>Gets or sets the style of the size grip to display in the lower-right corner of the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.SizeGripStyle" /> that represents the style of the size grip to display. The default is <see cref="F:System.Windows.Forms.SizeGripStyle.Auto" /></returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values.</exception>
		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x000B22C0 File Offset: 0x000B04C0
		// (set) Token: 0x0600268E RID: 9870 RVA: 0x000B22D4 File Offset: 0x000B04D4
		[SRCategory("CatWindowStyle")]
		[DefaultValue(SizeGripStyle.Auto)]
		[SRDescription("FormSizeGripStyleDescr")]
		public SizeGripStyle SizeGripStyle
		{
			get
			{
				return (SizeGripStyle)this.formState[Form.FormStateSizeGripStyle];
			}
			set
			{
				if (this.SizeGripStyle != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(SizeGripStyle));
					}
					this.formState[Form.FormStateSizeGripStyle] = (int)value;
					this.UpdateRenderSizeGrip();
				}
			}
		}

		/// <summary>Gets or sets the starting position of the form at run time.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormStartPosition" /> that represents the starting position of the form.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values.</exception>
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x0600268F RID: 9871 RVA: 0x000B2327 File Offset: 0x000B0527
		// (set) Token: 0x06002690 RID: 9872 RVA: 0x000B2339 File Offset: 0x000B0539
		[Localizable(true)]
		[SRCategory("CatLayout")]
		[DefaultValue(FormStartPosition.WindowsDefaultLocation)]
		[SRDescription("FormStartPositionDescr")]
		public FormStartPosition StartPosition
		{
			get
			{
				return (FormStartPosition)this.formState[Form.FormStateStartPos];
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FormStartPosition));
				}
				this.formState[Form.FormStateStartPos] = (int)value;
			}
		}

		/// <summary>Gets or sets the tab order of the control within its container.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the index of the control within the set of controls within its container that is included in the tab order.</returns>
		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06002691 RID: 9873 RVA: 0x000B2372 File Offset: 0x000B0572
		// (set) Token: 0x06002692 RID: 9874 RVA: 0x000B237A File Offset: 0x000B057A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Form.TabIndex" /> property changes.</summary>
		// Token: 0x140001A4 RID: 420
		// (add) Token: 0x06002693 RID: 9875 RVA: 0x000B2383 File Offset: 0x000B0583
		// (remove) Token: 0x06002694 RID: 9876 RVA: 0x000B238C File Offset: 0x000B058C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabIndexChanged
		{
			add
			{
				base.TabIndexChanged += value;
			}
			remove
			{
				base.TabIndexChanged -= value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can give the focus to the control using the TAB key; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x000B2395 File Offset: 0x000B0595
		// (set) Token: 0x06002696 RID: 9878 RVA: 0x000B239D File Offset: 0x000B059D
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DispId(-516)]
		[SRDescription("ControlTabStopDescr")]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Form.TabStop" /> property changes.</summary>
		// Token: 0x140001A5 RID: 421
		// (add) Token: 0x06002697 RID: 9879 RVA: 0x000B23A6 File Offset: 0x000B05A6
		// (remove) Token: 0x06002698 RID: 9880 RVA: 0x000B23AF File Offset: 0x000B05AF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x000B23B8 File Offset: 0x000B05B8
		private HandleRef TaskbarOwner
		{
			get
			{
				if (this.ownerWindow == null)
				{
					this.ownerWindow = new NativeWindow();
				}
				if (this.ownerWindow.Handle == IntPtr.Zero)
				{
					CreateParams createParams = new CreateParams();
					createParams.ExStyle = 128;
					this.ownerWindow.CreateHandle(createParams);
				}
				return new HandleRef(this.ownerWindow, this.ownerWindow.Handle);
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x0600269B RID: 9883 RVA: 0x00023FE9 File Offset: 0x000221E9
		[SettingsBindable(true)]
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

		/// <summary>Gets or sets a value indicating whether to display the form as a top-level window.</summary>
		/// <returns>
		///   <see langword="true" /> to display the form as a top-level window; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.Exception">A Multiple-document interface (MDI) parent form must be a top-level window.</exception>
		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x000B2422 File Offset: 0x000B0622
		// (set) Token: 0x0600269D RID: 9885 RVA: 0x000B242A File Offset: 0x000B062A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool TopLevel
		{
			get
			{
				return base.GetTopLevel();
			}
			set
			{
				if (!value && this.IsMdiContainer && !base.DesignMode)
				{
					throw new ArgumentException(SR.GetString("MDIContainerMustBeTopLevel"), "value");
				}
				base.SetTopLevel(value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the form should be displayed as a topmost form.</summary>
		/// <returns>
		///   <see langword="true" /> to display the form as a topmost form; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x000B245B File Offset: 0x000B065B
		// (set) Token: 0x0600269F RID: 9887 RVA: 0x000B2470 File Offset: 0x000B0670
		[DefaultValue(false)]
		[SRCategory("CatWindowStyle")]
		[SRDescription("FormTopMostDescr")]
		public bool TopMost
		{
			get
			{
				return this.formState[Form.FormStateTopMost] != 0;
			}
			set
			{
				if (this.IsRestrictedWindow)
				{
					return;
				}
				if (base.IsHandleCreated && this.TopLevel)
				{
					HandleRef handleRef = (value ? NativeMethods.HWND_TOPMOST : NativeMethods.HWND_NOTOPMOST);
					SafeNativeMethods.SetWindowPos(new HandleRef(this, base.Handle), handleRef, 0, 0, 0, 0, 3);
				}
				if (value)
				{
					this.formState[Form.FormStateTopMost] = 1;
					return;
				}
				this.formState[Form.FormStateTopMost] = 0;
			}
		}

		/// <summary>Gets or sets the color that will represent transparent areas of the form.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to display transparently on the form.</returns>
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x000B24E4 File Offset: 0x000B06E4
		// (set) Token: 0x060026A1 RID: 9889 RVA: 0x000B2514 File Offset: 0x000B0714
		[SRCategory("CatWindowStyle")]
		[SRDescription("FormTransparencyKeyDescr")]
		public Color TransparencyKey
		{
			get
			{
				object @object = base.Properties.GetObject(Form.PropTransparencyKey);
				if (@object != null)
				{
					return (Color)@object;
				}
				return Color.Empty;
			}
			set
			{
				base.Properties.SetObject(Form.PropTransparencyKey, value);
				if (!this.IsMdiContainer)
				{
					bool flag = this.formState[Form.FormStateLayered] == 1;
					if (value != Color.Empty)
					{
						IntSecurity.TransparentWindows.Demand();
						this.AllowTransparency = true;
						this.formState[Form.FormStateLayered] = 1;
					}
					else
					{
						this.formState[Form.FormStateLayered] = ((this.OpacityAsByte < byte.MaxValue) ? 1 : 0);
					}
					if (flag != (this.formState[Form.FormStateLayered] != 0))
					{
						base.UpdateStyles();
					}
					this.UpdateLayered();
				}
			}
		}

		/// <summary>Sets the control to the specified visible state.</summary>
		/// <param name="value">
		///   <see langword="true" /> to make the control visible; otherwise, <see langword="false" />.</param>
		// Token: 0x060026A2 RID: 9890 RVA: 0x000B25CC File Offset: 0x000B07CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void SetVisibleCore(bool value)
		{
			if (this.GetVisibleCore() == value && this.dialogResult == DialogResult.OK)
			{
				return;
			}
			if (this.GetVisibleCore() == value && (!value || this.CalledMakeVisible))
			{
				base.SetVisibleCore(value);
				return;
			}
			if (value)
			{
				this.CalledMakeVisible = true;
				if (this.CalledCreateControl)
				{
					if (this.CalledOnLoad)
					{
						if (!Application.OpenFormsInternal.Contains(this))
						{
							Application.OpenFormsInternalAdd(this);
						}
					}
					else
					{
						this.CalledOnLoad = true;
						this.OnLoad(EventArgs.Empty);
						if (this.dialogResult != DialogResult.None)
						{
							value = false;
						}
					}
				}
			}
			else
			{
				this.ResetSecurityTip(true);
			}
			if (!this.IsMdiChild)
			{
				base.SetVisibleCore(value);
				if (this.formState[Form.FormStateSWCalled] == 0)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 24, value ? 1 : 0, 0);
				}
			}
			else
			{
				if (base.IsHandleCreated)
				{
					this.DestroyHandle();
				}
				if (!value)
				{
					this.InvalidateMergedMenu();
					base.SetState(2, false);
				}
				else
				{
					base.SetState(2, true);
					this.MdiParentInternal.MdiClient.PerformLayout();
					if (this.ParentInternal != null && this.ParentInternal.Visible)
					{
						base.SuspendLayout();
						try
						{
							SafeNativeMethods.ShowWindow(new HandleRef(this, base.Handle), 5);
							base.CreateControl();
							if (this.WindowState == FormWindowState.Maximized)
							{
								this.MdiParentInternal.UpdateWindowIcon(true);
							}
						}
						finally
						{
							base.ResumeLayout();
						}
					}
				}
				this.OnVisibleChanged(EventArgs.Empty);
			}
			if (value && !this.IsMdiChild && (this.WindowState == FormWindowState.Maximized || this.TopMost))
			{
				if (base.ActiveControl == null)
				{
					base.SelectNextControlInternal(null, true, true, true, false);
				}
				base.FocusActiveControlInternal();
			}
		}

		/// <summary>Gets or sets a value that indicates whether form is minimized, maximized, or normal.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormWindowState" /> that represents whether form is minimized, maximized, or normal. The default is <see langword="FormWindowState.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values.</exception>
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x000B2780 File Offset: 0x000B0980
		// (set) Token: 0x060026A4 RID: 9892 RVA: 0x000B2794 File Offset: 0x000B0994
		[SRCategory("CatLayout")]
		[DefaultValue(FormWindowState.Normal)]
		[SRDescription("FormWindowStateDescr")]
		public FormWindowState WindowState
		{
			get
			{
				return (FormWindowState)this.formState[Form.FormStateWindowState];
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FormWindowState));
				}
				if (this.TopLevel && this.IsRestrictedWindow && value != FormWindowState.Normal)
				{
					return;
				}
				if (value != FormWindowState.Normal)
				{
					if (value - FormWindowState.Minimized <= 1)
					{
						base.SetState(65536, true);
					}
				}
				else
				{
					base.SetState(65536, false);
				}
				if (base.IsHandleCreated && base.Visible)
				{
					IntPtr handle = base.Handle;
					switch (value)
					{
					case FormWindowState.Normal:
						SafeNativeMethods.ShowWindow(new HandleRef(this, handle), 1);
						break;
					case FormWindowState.Minimized:
						SafeNativeMethods.ShowWindow(new HandleRef(this, handle), 6);
						break;
					case FormWindowState.Maximized:
						SafeNativeMethods.ShowWindow(new HandleRef(this, handle), 3);
						break;
					}
				}
				this.formState[Form.FormStateWindowState] = (int)value;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060026A5 RID: 9893 RVA: 0x000B286A File Offset: 0x000B0A6A
		// (set) Token: 0x060026A6 RID: 9894 RVA: 0x000B28A4 File Offset: 0x000B0AA4
		internal override string WindowText
		{
			get
			{
				if (!this.IsRestrictedWindow || this.formState[Form.FormStateIsWindowActivated] != 1)
				{
					return base.WindowText;
				}
				if (this.userWindowText == null)
				{
					return "";
				}
				return this.userWindowText;
			}
			set
			{
				string windowText = this.WindowText;
				this.userWindowText = value;
				if (this.IsRestrictedWindow && this.formState[Form.FormStateIsWindowActivated] == 1)
				{
					if (value == null)
					{
						value = "";
					}
					base.WindowText = this.RestrictedWindowText(value);
				}
				else
				{
					base.WindowText = value;
				}
				if (windowText == null || windowText.Length == 0 || value == null || value.Length == 0)
				{
					this.UpdateFormStyles();
				}
			}
		}

		/// <summary>Occurs when the form is activated in code or by the user.</summary>
		// Token: 0x140001A6 RID: 422
		// (add) Token: 0x060026A7 RID: 9895 RVA: 0x000B2916 File Offset: 0x000B0B16
		// (remove) Token: 0x060026A8 RID: 9896 RVA: 0x000B2929 File Offset: 0x000B0B29
		[SRCategory("CatFocus")]
		[SRDescription("FormOnActivateDescr")]
		public event EventHandler Activated
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_ACTIVATED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_ACTIVATED, value);
			}
		}

		/// <summary>Occurs when the form is closing.</summary>
		// Token: 0x140001A7 RID: 423
		// (add) Token: 0x060026A9 RID: 9897 RVA: 0x000B293C File Offset: 0x000B0B3C
		// (remove) Token: 0x060026AA RID: 9898 RVA: 0x000B294F File Offset: 0x000B0B4F
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnClosingDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event CancelEventHandler Closing
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_CLOSING, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_CLOSING, value);
			}
		}

		/// <summary>Occurs when the form is closed.</summary>
		// Token: 0x140001A8 RID: 424
		// (add) Token: 0x060026AB RID: 9899 RVA: 0x000B2962 File Offset: 0x000B0B62
		// (remove) Token: 0x060026AC RID: 9900 RVA: 0x000B2975 File Offset: 0x000B0B75
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnClosedDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler Closed
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_CLOSED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_CLOSED, value);
			}
		}

		/// <summary>Occurs when the form loses focus and is no longer the active form.</summary>
		// Token: 0x140001A9 RID: 425
		// (add) Token: 0x060026AD RID: 9901 RVA: 0x000B2988 File Offset: 0x000B0B88
		// (remove) Token: 0x060026AE RID: 9902 RVA: 0x000B299B File Offset: 0x000B0B9B
		[SRCategory("CatFocus")]
		[SRDescription("FormOnDeactivateDescr")]
		public event EventHandler Deactivate
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_DEACTIVATE, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_DEACTIVATE, value);
			}
		}

		/// <summary>Occurs before the form is closed.</summary>
		// Token: 0x140001AA RID: 426
		// (add) Token: 0x060026AF RID: 9903 RVA: 0x000B29AE File Offset: 0x000B0BAE
		// (remove) Token: 0x060026B0 RID: 9904 RVA: 0x000B29C1 File Offset: 0x000B0BC1
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnFormClosingDescr")]
		public event FormClosingEventHandler FormClosing
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_FORMCLOSING, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_FORMCLOSING, value);
			}
		}

		/// <summary>Occurs after the form is closed.</summary>
		// Token: 0x140001AB RID: 427
		// (add) Token: 0x060026B1 RID: 9905 RVA: 0x000B29D4 File Offset: 0x000B0BD4
		// (remove) Token: 0x060026B2 RID: 9906 RVA: 0x000B29E7 File Offset: 0x000B0BE7
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnFormClosedDescr")]
		public event FormClosedEventHandler FormClosed
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_FORMCLOSED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_FORMCLOSED, value);
			}
		}

		/// <summary>Occurs before a form is displayed for the first time.</summary>
		// Token: 0x140001AC RID: 428
		// (add) Token: 0x060026B3 RID: 9907 RVA: 0x000B29FA File Offset: 0x000B0BFA
		// (remove) Token: 0x060026B4 RID: 9908 RVA: 0x000B2A0D File Offset: 0x000B0C0D
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnLoadDescr")]
		public event EventHandler Load
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_LOAD, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_LOAD, value);
			}
		}

		/// <summary>Occurs when a multiple-document interface (MDI) child form is activated or closed within an MDI application.</summary>
		// Token: 0x140001AD RID: 429
		// (add) Token: 0x060026B5 RID: 9909 RVA: 0x000B2A20 File Offset: 0x000B0C20
		// (remove) Token: 0x060026B6 RID: 9910 RVA: 0x000B2A33 File Offset: 0x000B0C33
		[SRCategory("CatLayout")]
		[SRDescription("FormOnMDIChildActivateDescr")]
		public event EventHandler MdiChildActivate
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_MDI_CHILD_ACTIVATE, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_MDI_CHILD_ACTIVATE, value);
			}
		}

		/// <summary>Occurs when the menu of a form loses focus.</summary>
		// Token: 0x140001AE RID: 430
		// (add) Token: 0x060026B7 RID: 9911 RVA: 0x000B2A46 File Offset: 0x000B0C46
		// (remove) Token: 0x060026B8 RID: 9912 RVA: 0x000B2A59 File Offset: 0x000B0C59
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnMenuCompleteDescr")]
		[Browsable(false)]
		public event EventHandler MenuComplete
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_MENUCOMPLETE, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_MENUCOMPLETE, value);
			}
		}

		/// <summary>Occurs when the menu of a form receives focus.</summary>
		// Token: 0x140001AF RID: 431
		// (add) Token: 0x060026B9 RID: 9913 RVA: 0x000B2A6C File Offset: 0x000B0C6C
		// (remove) Token: 0x060026BA RID: 9914 RVA: 0x000B2A7F File Offset: 0x000B0C7F
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnMenuStartDescr")]
		[Browsable(false)]
		public event EventHandler MenuStart
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_MENUSTART, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_MENUSTART, value);
			}
		}

		/// <summary>Occurs after the input language of the form has changed.</summary>
		// Token: 0x140001B0 RID: 432
		// (add) Token: 0x060026BB RID: 9915 RVA: 0x000B2A92 File Offset: 0x000B0C92
		// (remove) Token: 0x060026BC RID: 9916 RVA: 0x000B2AA5 File Offset: 0x000B0CA5
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnInputLangChangeDescr")]
		public event InputLanguageChangedEventHandler InputLanguageChanged
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_INPUTLANGCHANGE, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_INPUTLANGCHANGE, value);
			}
		}

		/// <summary>Occurs when the user attempts to change the input language for the form.</summary>
		// Token: 0x140001B1 RID: 433
		// (add) Token: 0x060026BD RID: 9917 RVA: 0x000B2AB8 File Offset: 0x000B0CB8
		// (remove) Token: 0x060026BE RID: 9918 RVA: 0x000B2ACB File Offset: 0x000B0CCB
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnInputLangChangeRequestDescr")]
		public event InputLanguageChangingEventHandler InputLanguageChanging
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_INPUTLANGCHANGEREQUEST, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_INPUTLANGCHANGEREQUEST, value);
			}
		}

		/// <summary>Occurs after the value of the <see cref="P:System.Windows.Forms.Form.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x140001B2 RID: 434
		// (add) Token: 0x060026BF RID: 9919 RVA: 0x000B2ADE File Offset: 0x000B0CDE
		// (remove) Token: 0x060026C0 RID: 9920 RVA: 0x000B2AF1 File Offset: 0x000B0CF1
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
		}

		/// <summary>Occurs whenever the form is first displayed.</summary>
		// Token: 0x140001B3 RID: 435
		// (add) Token: 0x060026C1 RID: 9921 RVA: 0x000B2B04 File Offset: 0x000B0D04
		// (remove) Token: 0x060026C2 RID: 9922 RVA: 0x000B2B17 File Offset: 0x000B0D17
		[SRCategory("CatBehavior")]
		[SRDescription("FormOnShownDescr")]
		public event EventHandler Shown
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_SHOWN, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_SHOWN, value);
			}
		}

		/// <summary>Activates the form and gives it focus.</summary>
		// Token: 0x060026C3 RID: 9923 RVA: 0x000B2B2C File Offset: 0x000B0D2C
		public void Activate()
		{
			IntSecurity.ModifyFocus.Demand();
			if (base.Visible && base.IsHandleCreated)
			{
				if (this.IsMdiChild)
				{
					this.MdiParentInternal.MdiClient.SendMessage(546, base.Handle, 0);
					return;
				}
				UnsafeNativeMethods.SetForegroundWindow(new HandleRef(this, base.Handle));
			}
		}

		/// <summary>Activates the MDI child of a form.</summary>
		/// <param name="form">The child form to activate.</param>
		// Token: 0x060026C4 RID: 9924 RVA: 0x000B2B8B File Offset: 0x000B0D8B
		protected void ActivateMdiChild(Form form)
		{
			IntSecurity.ModifyFocus.Demand();
			this.ActivateMdiChildInternal(form);
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x000B2BA0 File Offset: 0x000B0DA0
		private void ActivateMdiChildInternal(Form form)
		{
			if (this.FormerlyActiveMdiChild != null && !this.FormerlyActiveMdiChild.IsClosing)
			{
				this.FormerlyActiveMdiChild.UpdateWindowIcon(true);
				this.FormerlyActiveMdiChild = null;
			}
			Form activeMdiChildInternal = this.ActiveMdiChildInternal;
			if (activeMdiChildInternal == form)
			{
				return;
			}
			if (activeMdiChildInternal != null)
			{
				activeMdiChildInternal.Active = false;
			}
			this.ActiveMdiChildInternal = form;
			if (form != null)
			{
				form.IsMdiChildFocusable = true;
				form.Active = true;
			}
			else if (this.Active)
			{
				base.ActivateControlInternal(this);
			}
			this.OnMdiChildActivate(EventArgs.Empty);
		}

		/// <summary>Adds an owned form to this form.</summary>
		/// <param name="ownedForm">The <see cref="T:System.Windows.Forms.Form" /> that this form will own.</param>
		// Token: 0x060026C6 RID: 9926 RVA: 0x000B2C24 File Offset: 0x000B0E24
		public void AddOwnedForm(Form ownedForm)
		{
			if (ownedForm == null)
			{
				return;
			}
			if (ownedForm.OwnerInternal != this)
			{
				ownedForm.Owner = this;
				return;
			}
			Form[] array = (Form[])base.Properties.GetObject(Form.PropOwnedForms);
			int integer = base.Properties.GetInteger(Form.PropOwnedFormsCount);
			for (int i = 0; i < integer; i++)
			{
				if (array[i] == ownedForm)
				{
					return;
				}
			}
			if (array == null)
			{
				array = new Form[4];
				base.Properties.SetObject(Form.PropOwnedForms, array);
			}
			else if (array.Length == integer)
			{
				Form[] array2 = new Form[integer * 2];
				Array.Copy(array, 0, array2, 0, integer);
				array = array2;
				base.Properties.SetObject(Form.PropOwnedForms, array);
			}
			array[integer] = ownedForm;
			base.Properties.SetInteger(Form.PropOwnedFormsCount, integer + 1);
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000B2CE0 File Offset: 0x000B0EE0
		private float AdjustScale(float scale)
		{
			if (scale < 0.92f)
			{
				return scale + 0.08f;
			}
			if (scale < 1f)
			{
				return 1f;
			}
			if (scale > 1.01f)
			{
				return scale + 0.08f;
			}
			return scale;
		}

		/// <summary>Adjusts the scroll bars on the container based on the current control positions and the control currently selected.</summary>
		/// <param name="displayScrollbars">
		///   <see langword="true" /> to show the scroll bars; otherwise, <see langword="false" />.</param>
		// Token: 0x060026C8 RID: 9928 RVA: 0x000B2D11 File Offset: 0x000B0F11
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void AdjustFormScrollbars(bool displayScrollbars)
		{
			if (this.WindowState != FormWindowState.Minimized)
			{
				base.AdjustFormScrollbars(displayScrollbars);
			}
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x000B2D24 File Offset: 0x000B0F24
		private void AdjustSystemMenu(IntPtr hmenu)
		{
			this.UpdateWindowState();
			FormWindowState windowState = this.WindowState;
			FormBorderStyle formBorderStyle = this.FormBorderStyle;
			bool flag = formBorderStyle == FormBorderStyle.SizableToolWindow || formBorderStyle == FormBorderStyle.Sizable;
			bool flag2 = this.MinimizeBox && windowState != FormWindowState.Minimized;
			bool flag3 = this.MaximizeBox && windowState != FormWindowState.Maximized;
			bool controlBox = this.ControlBox;
			bool flag4 = windowState > FormWindowState.Normal;
			bool flag5 = flag && windowState != FormWindowState.Minimized && windowState != FormWindowState.Maximized;
			if (!flag2)
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61472, 1);
			}
			else
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61472, 0);
			}
			if (!flag3)
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61488, 1);
			}
			else
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61488, 0);
			}
			if (!controlBox)
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61536, 1);
			}
			else
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61536, 0);
			}
			if (!flag4)
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61728, 1);
			}
			else
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61728, 0);
			}
			if (!flag5)
			{
				UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61440, 1);
				return;
			}
			UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, hmenu), 61440, 0);
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000B2E74 File Offset: 0x000B1074
		private void AdjustSystemMenu()
		{
			if (base.IsHandleCreated)
			{
				IntPtr intPtr = UnsafeNativeMethods.GetSystemMenu(new HandleRef(this, base.Handle), false);
				this.AdjustSystemMenu(intPtr);
				intPtr = IntPtr.Zero;
			}
		}

		/// <summary>Resizes the form according to the current value of the <see cref="P:System.Windows.Forms.Form.AutoScaleBaseSize" /> property and the size of the current font.</summary>
		// Token: 0x060026CB RID: 9931 RVA: 0x000B2EAC File Offset: 0x000B10AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This method has been deprecated. Use the ApplyAutoScaling method instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected void ApplyAutoScaling()
		{
			if (!this.autoScaleBaseSize.IsEmpty)
			{
				Size size = this.AutoScaleBaseSize;
				SizeF autoScaleSize = Form.GetAutoScaleSize(this.Font);
				Size size2 = new Size((int)Math.Round((double)autoScaleSize.Width), (int)Math.Round((double)autoScaleSize.Height));
				if (size.Equals(size2))
				{
					return;
				}
				float num = this.AdjustScale((float)size2.Height / (float)size.Height);
				float num2 = this.AdjustScale((float)size2.Width / (float)size.Width);
				base.Scale(num2, num);
				this.AutoScaleBaseSize = size2;
			}
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x000B2F58 File Offset: 0x000B1158
		private void ApplyClientSize()
		{
			if (this.formState[Form.FormStateWindowState] != 0 || !base.IsHandleCreated)
			{
				return;
			}
			Size clientSize = this.ClientSize;
			bool hscroll = base.HScroll;
			bool vscroll = base.VScroll;
			bool flag = false;
			if (this.formState[Form.FormStateSetClientSize] != 0)
			{
				flag = true;
				this.formState[Form.FormStateSetClientSize] = 0;
			}
			if (flag)
			{
				if (hscroll)
				{
					clientSize.Height += SystemInformation.HorizontalScrollBarHeight;
				}
				if (vscroll)
				{
					clientSize.Width += SystemInformation.VerticalScrollBarWidth;
				}
			}
			IntPtr handle = base.Handle;
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			SafeNativeMethods.GetClientRect(new HandleRef(this, handle), ref rect);
			Rectangle rectangle = Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			Rectangle bounds = base.Bounds;
			if (clientSize.Width != rectangle.Width)
			{
				Size size = this.ComputeWindowSize(clientSize);
				if (vscroll)
				{
					size.Width += SystemInformation.VerticalScrollBarWidth;
				}
				if (hscroll)
				{
					size.Height += SystemInformation.HorizontalScrollBarHeight;
				}
				bounds.Width = size.Width;
				bounds.Height = size.Height;
				base.Bounds = bounds;
				SafeNativeMethods.GetClientRect(new HandleRef(this, handle), ref rect);
				rectangle = Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			}
			if (clientSize.Height != rectangle.Height)
			{
				int num = clientSize.Height - rectangle.Height;
				bounds.Height += num;
				base.Bounds = bounds;
			}
			base.UpdateBounds();
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000B3114 File Offset: 0x000B1314
		internal override void AssignParent(Control value)
		{
			Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
			if (form != null && form.MdiClient != value)
			{
				base.Properties.SetObject(Form.PropFormMdiParent, null);
			}
			base.AssignParent(value);
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x000B315C File Offset: 0x000B135C
		internal bool CheckCloseDialog(bool closingOnly)
		{
			if (this.dialogResult == DialogResult.None && base.Visible)
			{
				return false;
			}
			try
			{
				FormClosingEventArgs formClosingEventArgs = new FormClosingEventArgs(this.closeReason, false);
				if (!this.CalledClosing)
				{
					this.OnClosing(formClosingEventArgs);
					this.OnFormClosing(formClosingEventArgs);
					if (formClosingEventArgs.Cancel)
					{
						this.dialogResult = DialogResult.None;
					}
					else
					{
						this.CalledClosing = true;
					}
				}
				if (!closingOnly && this.dialogResult != DialogResult.None)
				{
					FormClosedEventArgs formClosedEventArgs = new FormClosedEventArgs(this.closeReason);
					this.OnClosed(formClosedEventArgs);
					this.OnFormClosed(formClosedEventArgs);
					this.CalledClosing = false;
				}
			}
			catch (Exception ex)
			{
				this.dialogResult = DialogResult.None;
				if (NativeWindow.WndProcShouldBeDebuggable)
				{
					throw;
				}
				Application.OnThreadException(ex);
			}
			return this.dialogResult != DialogResult.None || !base.Visible;
		}

		/// <summary>Closes the form.</summary>
		/// <exception cref="T:System.InvalidOperationException">The form was closed while a handle was being created.</exception>
		/// <exception cref="T:System.ObjectDisposedException">You cannot call this method from the <see cref="E:System.Windows.Forms.Form.Activated" /> event when <see cref="P:System.Windows.Forms.Form.WindowState" /> is set to <see cref="F:System.Windows.Forms.FormWindowState.Maximized" />.</exception>
		// Token: 0x060026CF RID: 9935 RVA: 0x000B3224 File Offset: 0x000B1424
		public void Close()
		{
			if (base.GetState(262144))
			{
				throw new InvalidOperationException(SR.GetString("ClosingWhileCreatingHandle", new object[] { "Close" }));
			}
			if (base.IsHandleCreated)
			{
				this.closeReason = CloseReason.UserClosing;
				base.SendMessage(16, 0, 0);
				return;
			}
			base.Dispose();
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x000B3280 File Offset: 0x000B1480
		private Size ComputeWindowSize(Size clientSize)
		{
			CreateParams createParams = this.CreateParams;
			return this.ComputeWindowSize(clientSize, createParams.Style, createParams.ExStyle);
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x000B32A8 File Offset: 0x000B14A8
		private Size ComputeWindowSize(Size clientSize, int style, int exStyle)
		{
			NativeMethods.RECT rect = new NativeMethods.RECT(0, 0, clientSize.Width, clientSize.Height);
			base.AdjustWindowRectEx(ref rect, style, this.HasMenu, exStyle);
			return new Size(rect.right - rect.left, rect.bottom - rect.top);
		}

		/// <summary>Creates a new instance of the control collection for the control.</summary>
		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x060026D2 RID: 9938 RVA: 0x000B32FB File Offset: 0x000B14FB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new Form.ControlCollection(this);
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x000B3303 File Offset: 0x000B1503
		internal override void AfterControlRemoved(Control control, Control oldParent)
		{
			base.AfterControlRemoved(control, oldParent);
			if (control == this.AcceptButton)
			{
				this.AcceptButton = null;
			}
			if (control == this.CancelButton)
			{
				this.CancelButton = null;
			}
			if (control == this.ctlClient)
			{
				this.ctlClient = null;
				this.UpdateMenuHandles();
			}
		}

		/// <summary>Creates the handle for the form. If a derived class overrides this function, it must call the base implementation.</summary>
		/// <exception cref="T:System.InvalidOperationException">A handle for this <see cref="T:System.Windows.Forms.Form" /> has already been created.</exception>
		// Token: 0x060026D4 RID: 9940 RVA: 0x000B3344 File Offset: 0x000B1544
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void CreateHandle()
		{
			Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
			if (form != null)
			{
				form.SuspendUpdateMenuHandles();
			}
			try
			{
				if (this.IsMdiChild && this.MdiParentInternal.IsHandleCreated)
				{
					MdiClient mdiClient = this.MdiParentInternal.MdiClient;
					if (mdiClient != null && !mdiClient.IsHandleCreated)
					{
						mdiClient.CreateControl();
					}
				}
				if (this.IsMdiChild && this.formState[Form.FormStateWindowState] == 2)
				{
					this.formState[Form.FormStateWindowState] = 0;
					this.formState[Form.FormStateMdiChildMax] = 1;
					base.CreateHandle();
					this.formState[Form.FormStateWindowState] = 2;
					this.formState[Form.FormStateMdiChildMax] = 0;
				}
				else
				{
					base.CreateHandle();
				}
				this.UpdateHandleWithOwner();
				this.UpdateWindowIcon(false);
				this.AdjustSystemMenu();
				if (this.formState[Form.FormStateStartPos] != 3)
				{
					this.ApplyClientSize();
				}
				if (this.formState[Form.FormStateShowWindowOnCreate] == 1)
				{
					base.Visible = true;
				}
				if (this.Menu != null || !this.TopLevel || this.IsMdiContainer)
				{
					this.UpdateMenuHandles();
				}
				if (!this.ShowInTaskbar && this.OwnerInternal == null && this.TopLevel)
				{
					UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -8, this.TaskbarOwner);
					Icon icon = this.Icon;
					if (icon != null && this.TaskbarOwner.Handle != IntPtr.Zero)
					{
						UnsafeNativeMethods.SendMessage(this.TaskbarOwner, 128, 1, icon.Handle);
					}
				}
				if (this.formState[Form.FormStateTopMost] != 0)
				{
					this.TopMost = true;
				}
			}
			finally
			{
				if (form != null)
				{
					form.ResumeUpdateMenuHandles();
				}
				base.UpdateStyles();
			}
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x000B352C File Offset: 0x000B172C
		private void DeactivateMdiChild()
		{
			Form activeMdiChildInternal = this.ActiveMdiChildInternal;
			if (activeMdiChildInternal != null)
			{
				Form mdiParentInternal = activeMdiChildInternal.MdiParentInternal;
				activeMdiChildInternal.Active = false;
				activeMdiChildInternal.IsMdiChildFocusable = false;
				if (!activeMdiChildInternal.IsClosing)
				{
					this.FormerlyActiveMdiChild = activeMdiChildInternal;
				}
				bool flag = true;
				foreach (Form form in mdiParentInternal.MdiChildren)
				{
					if (form != this && form.Visible)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					mdiParentInternal.ActivateMdiChildInternal(null);
				}
				this.ActiveMdiChildInternal = null;
				this.UpdateMenuHandles();
				this.UpdateToolStrip();
			}
		}

		/// <summary>Sends the specified message to the default window procedure.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x060026D6 RID: 9942 RVA: 0x000B35B8 File Offset: 0x000B17B8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void DefWndProc(ref Message m)
		{
			if (this.ctlClient != null && this.ctlClient.IsHandleCreated && this.ctlClient.ParentInternal == this)
			{
				m.Result = UnsafeNativeMethods.DefFrameProc(m.HWnd, this.ctlClient.Handle, m.Msg, m.WParam, m.LParam);
				return;
			}
			if (this.formStateEx[Form.FormStateExUseMdiChildProc] != 0)
			{
				m.Result = UnsafeNativeMethods.DefMDIChildProc(m.HWnd, m.Msg, m.WParam, m.LParam);
				return;
			}
			base.DefWndProc(ref m);
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.Form" />.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060026D7 RID: 9943 RVA: 0x000B3654 File Offset: 0x000B1854
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.CalledOnLoad = false;
				this.CalledMakeVisible = false;
				this.CalledCreateControl = false;
				if (base.Properties.ContainsObject(Form.PropAcceptButton))
				{
					base.Properties.SetObject(Form.PropAcceptButton, null);
				}
				if (base.Properties.ContainsObject(Form.PropCancelButton))
				{
					base.Properties.SetObject(Form.PropCancelButton, null);
				}
				if (base.Properties.ContainsObject(Form.PropDefaultButton))
				{
					base.Properties.SetObject(Form.PropDefaultButton, null);
				}
				if (base.Properties.ContainsObject(Form.PropActiveMdiChild))
				{
					base.Properties.SetObject(Form.PropActiveMdiChild, null);
				}
				if (this.MdiWindowListStrip != null)
				{
					this.MdiWindowListStrip.Dispose();
					this.MdiWindowListStrip = null;
				}
				if (this.MdiControlStrip != null)
				{
					this.MdiControlStrip.Dispose();
					this.MdiControlStrip = null;
				}
				if (this.MainMenuStrip != null)
				{
					this.MainMenuStrip = null;
				}
				Form form = (Form)base.Properties.GetObject(Form.PropOwner);
				if (form != null)
				{
					form.RemoveOwnedForm(this);
					base.Properties.SetObject(Form.PropOwner, null);
				}
				Form[] array = (Form[])base.Properties.GetObject(Form.PropOwnedForms);
				int integer = base.Properties.GetInteger(Form.PropOwnedFormsCount);
				for (int i = integer - 1; i >= 0; i--)
				{
					if (array[i] != null)
					{
						array[i].Dispose();
					}
				}
				if (this.smallIcon != null)
				{
					this.smallIcon.Dispose();
					this.smallIcon = null;
				}
				this.ResetSecurityTip(false);
				base.Dispose(disposing);
				this.ctlClient = null;
				MainMenu menu = this.Menu;
				if (menu != null && menu.ownerForm == this)
				{
					menu.Dispose();
					base.Properties.SetObject(Form.PropMainMenu, null);
				}
				if (base.Properties.GetObject(Form.PropCurMenu) != null)
				{
					base.Properties.SetObject(Form.PropCurMenu, null);
				}
				this.MenuChanged(0, null);
				MainMenu mainMenu = (MainMenu)base.Properties.GetObject(Form.PropDummyMenu);
				if (mainMenu != null)
				{
					mainMenu.Dispose();
					base.Properties.SetObject(Form.PropDummyMenu, null);
				}
				MainMenu mainMenu2 = (MainMenu)base.Properties.GetObject(Form.PropMergedMenu);
				if (mainMenu2 != null)
				{
					if (mainMenu2.ownerForm == this || mainMenu2.form == null)
					{
						mainMenu2.Dispose();
					}
					base.Properties.SetObject(Form.PropMergedMenu, null);
					return;
				}
			}
			else
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x000B38CC File Offset: 0x000B1ACC
		private void FillInCreateParamsBorderIcons(CreateParams cp)
		{
			if (this.FormBorderStyle != FormBorderStyle.None)
			{
				if (this.Text != null && this.Text.Length != 0)
				{
					cp.Style |= 12582912;
				}
				if (this.ControlBox || this.IsRestrictedWindow)
				{
					cp.Style |= 13107200;
				}
				else
				{
					cp.Style &= -524289;
				}
				if (this.MaximizeBox || this.IsRestrictedWindow)
				{
					cp.Style |= 65536;
				}
				else
				{
					cp.Style &= -65537;
				}
				if (this.MinimizeBox || this.IsRestrictedWindow)
				{
					cp.Style |= 131072;
				}
				else
				{
					cp.Style &= -131073;
				}
				if (this.HelpButton && !this.MaximizeBox && !this.MinimizeBox && this.ControlBox)
				{
					cp.ExStyle |= 1024;
					return;
				}
				cp.ExStyle &= -1025;
			}
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000B39F4 File Offset: 0x000B1BF4
		private void FillInCreateParamsBorderStyles(CreateParams cp)
		{
			switch (this.formState[Form.FormStateBorderStyle])
			{
			case 0:
				if (!this.IsRestrictedWindow)
				{
					return;
				}
				break;
			case 1:
				break;
			case 2:
				cp.Style |= 8388608;
				cp.ExStyle |= 512;
				return;
			case 3:
				cp.Style |= 8388608;
				cp.ExStyle |= 1;
				return;
			case 4:
				cp.Style |= 8650752;
				return;
			case 5:
				cp.Style |= 8388608;
				cp.ExStyle |= 128;
				return;
			case 6:
				cp.Style |= 8650752;
				cp.ExStyle |= 128;
				return;
			default:
				return;
			}
			cp.Style |= 8388608;
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x000B3AF8 File Offset: 0x000B1CF8
		private void FillInCreateParamsStartPosition(CreateParams cp)
		{
			if (this.formState[Form.FormStateSetClientSize] != 0)
			{
				int num = cp.Style & -553648129;
				Size size = this.ComputeWindowSize(this.ClientSize, num, cp.ExStyle);
				if (this.IsRestrictedWindow)
				{
					size = this.ApplyBoundsConstraints(cp.X, cp.Y, size.Width, size.Height).Size;
				}
				cp.Width = size.Width;
				cp.Height = size.Height;
			}
			switch (this.formState[Form.FormStateStartPos])
			{
			case 1:
			{
				if (this.IsMdiChild)
				{
					Control mdiClient = this.MdiParentInternal.MdiClient;
					Rectangle clientRectangle = mdiClient.ClientRectangle;
					cp.X = Math.Max(clientRectangle.X, clientRectangle.X + (clientRectangle.Width - cp.Width) / 2);
					cp.Y = Math.Max(clientRectangle.Y, clientRectangle.Y + (clientRectangle.Height - cp.Height) / 2);
					return;
				}
				IWin32Window win32Window = (IWin32Window)base.Properties.GetObject(Form.PropDialogOwner);
				Screen screen;
				if (this.OwnerInternal != null || win32Window != null)
				{
					IntPtr intPtr = ((win32Window != null) ? Control.GetSafeHandle(win32Window) : this.OwnerInternal.Handle);
					screen = Screen.FromHandleInternal(intPtr);
				}
				else
				{
					screen = Screen.FromPoint(Control.MousePosition);
				}
				Rectangle workingArea = screen.WorkingArea;
				if (this.WindowState != FormWindowState.Maximized)
				{
					cp.X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - cp.Width) / 2);
					cp.Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - cp.Height) / 2);
					return;
				}
				return;
			}
			case 2:
			case 4:
				break;
			case 3:
				cp.Width = int.MinValue;
				cp.Height = int.MinValue;
				break;
			default:
				return;
			}
			if (!this.IsMdiChild || this.Dock == DockStyle.None)
			{
				cp.X = int.MinValue;
				cp.Y = int.MinValue;
				return;
			}
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x000B3D20 File Offset: 0x000B1F20
		private void FillInCreateParamsWindowState(CreateParams cp)
		{
			FormWindowState formWindowState = (FormWindowState)this.formState[Form.FormStateWindowState];
			if (formWindowState != FormWindowState.Minimized)
			{
				if (formWindowState == FormWindowState.Maximized)
				{
					cp.Style |= 16777216;
					return;
				}
			}
			else
			{
				cp.Style |= 536870912;
			}
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x000B3D6B File Offset: 0x000B1F6B
		internal override bool FocusInternal()
		{
			if (this.IsMdiChild)
			{
				this.MdiParentInternal.MdiClient.SendMessage(546, base.Handle, 0);
				return this.Focused;
			}
			return base.FocusInternal();
		}

		/// <summary>Gets the size when autoscaling the form based on a specified font.</summary>
		/// <param name="font">A <see cref="T:System.Drawing.Font" /> representing the font to determine the autoscaled base size of the form.</param>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> representing the autoscaled size of the form.</returns>
		// Token: 0x060026DD RID: 9949 RVA: 0x000B3DA0 File Offset: 0x000B1FA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This method has been deprecated. Use the AutoScaleDimensions property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public static SizeF GetAutoScaleSize(Font font)
		{
			float num = (float)font.Height;
			float num2 = 9f;
			try
			{
				using (Graphics graphics = Graphics.FromHwndInternal(IntPtr.Zero))
				{
					string text = "The quick brown fox jumped over the lazy dog.";
					double num3 = 44.549996948242189;
					float width = graphics.MeasureString(text, font).Width;
					num2 = (float)((double)width / num3);
				}
			}
			catch
			{
			}
			return new SizeF(num2, num);
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x000B3E28 File Offset: 0x000B2028
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			return base.GetPreferredSizeCore(proposedSize);
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x000B3E40 File Offset: 0x000B2040
		private void ResolveZoneAndSiteNames(ArrayList sites, ref string securityZone, ref string securitySite)
		{
			securityZone = SR.GetString("SecurityRestrictedWindowTextUnknownZone");
			securitySite = SR.GetString("SecurityRestrictedWindowTextUnknownSite");
			try
			{
				if (sites != null && sites.Count != 0)
				{
					ArrayList arrayList = new ArrayList();
					foreach (object obj in sites)
					{
						if (obj == null)
						{
							return;
						}
						string text = obj.ToString();
						if (text.Length == 0)
						{
							return;
						}
						Zone zone = Zone.CreateFromUrl(text);
						if (!zone.SecurityZone.Equals(SecurityZone.MyComputer))
						{
							string text2 = zone.SecurityZone.ToString();
							if (!arrayList.Contains(text2))
							{
								arrayList.Add(text2);
							}
						}
					}
					if (arrayList.Count == 0)
					{
						securityZone = SecurityZone.MyComputer.ToString();
					}
					else if (arrayList.Count == 1)
					{
						securityZone = arrayList[0].ToString();
					}
					else
					{
						securityZone = SR.GetString("SecurityRestrictedWindowTextMixedZone");
					}
					ArrayList arrayList2 = new ArrayList();
					new FileIOPermission(PermissionState.None)
					{
						AllFiles = FileIOPermissionAccess.PathDiscovery
					}.Assert();
					try
					{
						foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
						{
							if (assembly.GlobalAssemblyCache)
							{
								arrayList2.Add(assembly.CodeBase.ToUpper(CultureInfo.InvariantCulture));
							}
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					ArrayList arrayList3 = new ArrayList();
					foreach (object obj2 in sites)
					{
						Uri uri = new Uri(obj2.ToString());
						if (!arrayList2.Contains(uri.AbsoluteUri.ToUpper(CultureInfo.InvariantCulture)))
						{
							string host = uri.Host;
							if (host.Length > 0 && !arrayList3.Contains(host))
							{
								arrayList3.Add(host);
							}
						}
					}
					if (arrayList3.Count == 0)
					{
						new EnvironmentPermission(PermissionState.Unrestricted).Assert();
						try
						{
							securitySite = Environment.MachineName;
							goto IL_24D;
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					if (arrayList3.Count == 1)
					{
						securitySite = arrayList3[0].ToString();
					}
					else
					{
						securitySite = SR.GetString("SecurityRestrictedWindowTextMultipleSites");
					}
					IL_24D:;
				}
			}
			catch
			{
			}
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x000B411C File Offset: 0x000B231C
		private string RestrictedWindowText(string original)
		{
			this.EnsureSecurityInformation();
			return string.Format(CultureInfo.CurrentCulture, Application.SafeTopLevelCaptionFormat, new object[] { original, this.securityZone, this.securitySite });
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000B4150 File Offset: 0x000B2350
		private void EnsureSecurityInformation()
		{
			if (this.securityZone == null || this.securitySite == null)
			{
				ArrayList arrayList;
				ArrayList arrayList2;
				SecurityManager.GetZoneAndOrigin(out arrayList, out arrayList2);
				this.ResolveZoneAndSiteNames(arrayList2, ref this.securityZone, ref this.securitySite);
			}
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000B4189 File Offset: 0x000B2389
		private void CallShownEvent()
		{
			this.OnShown(EventArgs.Empty);
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000B4196 File Offset: 0x000B2396
		internal override bool CanSelectCore()
		{
			return base.GetStyle(ControlStyles.Selectable) && base.Enabled && base.Visible;
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x000B41B8 File Offset: 0x000B23B8
		internal bool CanRecreateHandle()
		{
			return !this.IsMdiChild || (base.GetState(2) && base.IsHandleCreated);
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000B41D5 File Offset: 0x000B23D5
		internal override bool CanProcessMnemonic()
		{
			return (!this.IsMdiChild || (this.formStateEx[Form.FormStateExMnemonicProcessed] != 1 && this == this.MdiParentInternal.ActiveMdiChildInternal && this.WindowState != FormWindowState.Minimized)) && base.CanProcessMnemonic();
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed as a mnemonic by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x060026E6 RID: 9958 RVA: 0x000B4214 File Offset: 0x000B2414
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (base.ProcessMnemonic(charCode))
			{
				return true;
			}
			if (this.IsMdiContainer)
			{
				if (base.Controls.Count > 1)
				{
					for (int i = 0; i < base.Controls.Count; i++)
					{
						Control control = base.Controls[i];
						if (!(control is MdiClient) && control.ProcessMnemonic(charCode))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		/// <summary>Centers the position of the form within the bounds of the parent form.</summary>
		// Token: 0x060026E7 RID: 9959 RVA: 0x000B427C File Offset: 0x000B247C
		protected void CenterToParent()
		{
			if (this.TopLevel)
			{
				Point point = default(Point);
				Size size = this.Size;
				IntPtr intPtr = IntPtr.Zero;
				intPtr = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -8);
				if (intPtr != IntPtr.Zero)
				{
					Screen screen = Screen.FromHandleInternal(intPtr);
					Rectangle workingArea = screen.WorkingArea;
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					UnsafeNativeMethods.GetWindowRect(new HandleRef(null, intPtr), ref rect);
					point.X = (rect.left + rect.right - size.Width) / 2;
					if (point.X < workingArea.X)
					{
						point.X = workingArea.X;
					}
					else if (point.X + size.Width > workingArea.X + workingArea.Width)
					{
						point.X = workingArea.X + workingArea.Width - size.Width;
					}
					point.Y = (rect.top + rect.bottom - size.Height) / 2;
					if (point.Y < workingArea.Y)
					{
						point.Y = workingArea.Y;
					}
					else if (point.Y + size.Height > workingArea.Y + workingArea.Height)
					{
						point.Y = workingArea.Y + workingArea.Height - size.Height;
					}
					this.Location = point;
					return;
				}
				this.CenterToScreen();
			}
		}

		/// <summary>Centers the form on the current screen.</summary>
		// Token: 0x060026E8 RID: 9960 RVA: 0x000B4400 File Offset: 0x000B2600
		protected void CenterToScreen()
		{
			Point point = default(Point);
			Screen screen;
			if (this.OwnerInternal != null)
			{
				screen = Screen.FromControl(this.OwnerInternal);
			}
			else
			{
				IntPtr intPtr = IntPtr.Zero;
				if (this.TopLevel)
				{
					intPtr = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -8);
				}
				if (intPtr != IntPtr.Zero)
				{
					screen = Screen.FromHandleInternal(intPtr);
				}
				else
				{
					screen = Screen.FromPoint(Control.MousePosition);
				}
			}
			Rectangle workingArea = screen.WorkingArea;
			point.X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - base.Width) / 2);
			point.Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - base.Height) / 2);
			this.Location = point;
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000B44D4 File Offset: 0x000B26D4
		private void InvalidateMergedMenu()
		{
			if (base.Properties.ContainsObject(Form.PropMergedMenu))
			{
				MainMenu mainMenu = base.Properties.GetObject(Form.PropMergedMenu) as MainMenu;
				if (mainMenu != null && mainMenu.ownerForm == this)
				{
					mainMenu.Dispose();
				}
				base.Properties.SetObject(Form.PropMergedMenu, null);
			}
			Form parentFormInternal = base.ParentFormInternal;
			if (parentFormInternal != null)
			{
				parentFormInternal.MenuChanged(0, parentFormInternal.Menu);
			}
		}

		/// <summary>Arranges the multiple-document interface (MDI) child forms within the MDI parent form.</summary>
		/// <param name="value">One of the <see cref="T:System.Windows.Forms.MdiLayout" /> values that defines the layout of MDI child forms.</param>
		// Token: 0x060026EA RID: 9962 RVA: 0x000B4543 File Offset: 0x000B2743
		public void LayoutMdi(MdiLayout value)
		{
			if (this.ctlClient == null)
			{
				return;
			}
			this.ctlClient.LayoutMdi(value);
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000B455C File Offset: 0x000B275C
		internal void MenuChanged(int change, Menu menu)
		{
			Form parentFormInternal = base.ParentFormInternal;
			if (parentFormInternal != null && this == parentFormInternal.ActiveMdiChildInternal)
			{
				parentFormInternal.MenuChanged(change, menu);
				return;
			}
			switch (change)
			{
			case 0:
			case 3:
				if (this.ctlClient != null && this.ctlClient.IsHandleCreated)
				{
					if (base.IsHandleCreated)
					{
						this.UpdateMenuHandles(null, false);
					}
					Control.ControlCollection controls = this.ctlClient.Controls;
					int count = controls.Count;
					while (count-- > 0)
					{
						Control control = controls[count];
						if (control is Form && control.Properties.ContainsObject(Form.PropMergedMenu))
						{
							MainMenu mainMenu = control.Properties.GetObject(Form.PropMergedMenu) as MainMenu;
							if (mainMenu != null && mainMenu.ownerForm == control)
							{
								mainMenu.Dispose();
							}
							control.Properties.SetObject(Form.PropMergedMenu, null);
						}
					}
					this.UpdateMenuHandles();
					return;
				}
				if (menu == this.Menu && change == 0)
				{
					this.UpdateMenuHandles();
					return;
				}
				break;
			case 1:
				if (menu == this.Menu || (this.ActiveMdiChildInternal != null && menu == this.ActiveMdiChildInternal.Menu))
				{
					this.UpdateMenuHandles();
					return;
				}
				break;
			case 2:
				if (this.ctlClient != null && this.ctlClient.IsHandleCreated)
				{
					this.UpdateMenuHandles();
				}
				break;
			default:
				return;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Activated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060026EC RID: 9964 RVA: 0x000B46A0 File Offset: 0x000B28A0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnActivated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_ACTIVATED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000B46CE File Offset: 0x000B28CE
		internal override void OnAutoScaleModeChanged()
		{
			base.OnAutoScaleModeChanged();
			if (this.formStateEx[Form.FormStateExSettingAutoScale] != 1)
			{
				this.AutoScale = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the data.</param>
		// Token: 0x060026EE RID: 9966 RVA: 0x000B46F0 File Offset: 0x000B28F0
		protected override void OnBackgroundImageChanged(EventArgs e)
		{
			base.OnBackgroundImageChanged(e);
			if (this.IsMdiContainer)
			{
				this.MdiClient.BackgroundImage = this.BackgroundImage;
				this.MdiClient.Invalidate();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageLayoutChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060026EF RID: 9967 RVA: 0x000B471D File Offset: 0x000B291D
		protected override void OnBackgroundImageLayoutChanged(EventArgs e)
		{
			base.OnBackgroundImageLayoutChanged(e);
			if (this.IsMdiContainer)
			{
				this.MdiClient.BackgroundImageLayout = this.BackgroundImageLayout;
				this.MdiClient.Invalidate();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Closing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x060026F0 RID: 9968 RVA: 0x000B474C File Offset: 0x000B294C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClosing(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[Form.EVENT_CLOSING];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Closed" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060026F1 RID: 9969 RVA: 0x000B477C File Offset: 0x000B297C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClosed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_CLOSED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.FormClosing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> that contains the event data.</param>
		// Token: 0x060026F2 RID: 9970 RVA: 0x000B47AC File Offset: 0x000B29AC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFormClosing(FormClosingEventArgs e)
		{
			FormClosingEventHandler formClosingEventHandler = (FormClosingEventHandler)base.Events[Form.EVENT_FORMCLOSING];
			if (formClosingEventHandler != null)
			{
				formClosingEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.FormClosed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.FormClosedEventArgs" /> that contains the event data.</param>
		// Token: 0x060026F3 RID: 9971 RVA: 0x000B47DC File Offset: 0x000B29DC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFormClosed(FormClosedEventArgs e)
		{
			Application.OpenFormsInternalRemove(this);
			FormClosedEventHandler formClosedEventHandler = (FormClosedEventHandler)base.Events[Form.EVENT_FORMCLOSED];
			if (formClosedEventHandler != null)
			{
				formClosedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see langword="CreateControl" /> event.</summary>
		// Token: 0x060026F4 RID: 9972 RVA: 0x000B4810 File Offset: 0x000B2A10
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnCreateControl()
		{
			this.CalledCreateControl = true;
			base.OnCreateControl();
			if (this.CalledMakeVisible && !this.CalledOnLoad)
			{
				this.CalledOnLoad = true;
				this.OnLoad(EventArgs.Empty);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Deactivate" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060026F5 RID: 9973 RVA: 0x000B4844 File Offset: 0x000B2A44
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDeactivate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_DEACTIVATE];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060026F6 RID: 9974 RVA: 0x000B4874 File Offset: 0x000B2A74
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			if (!base.DesignMode && base.Enabled && this.Active)
			{
				if (base.ActiveControl == null)
				{
					base.SelectNextControlInternal(this, true, true, true, true);
					return;
				}
				base.FocusActiveControlInternal();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060026F7 RID: 9975 RVA: 0x000B48BD File Offset: 0x000B2ABD
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			if (this.IsMdiChild)
			{
				base.UpdateFocusedControl();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060026F8 RID: 9976 RVA: 0x000B48D4 File Offset: 0x000B2AD4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnFontChanged(EventArgs e)
		{
			if (base.DesignMode)
			{
				this.UpdateAutoScaleBaseSize();
			}
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060026F9 RID: 9977 RVA: 0x000B48EB File Offset: 0x000B2AEB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleCreated(EventArgs e)
		{
			this.formStateEx[Form.FormStateExUseMdiChildProc] = ((this.IsMdiChild && base.Visible) ? 1 : 0);
			base.OnHandleCreated(e);
			this.UpdateLayered();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060026FA RID: 9978 RVA: 0x000B491E File Offset: 0x000B2B1E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			this.formStateEx[Form.FormStateExUseMdiChildProc] = 0;
			Application.OpenFormsInternalRemove(this);
			this.ResetSecurityTip(true);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.HelpButtonClicked" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x060026FB RID: 9979 RVA: 0x000B4948 File Offset: 0x000B2B48
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnHelpButtonClicked(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[Form.EVENT_HELPBUTTONCLICKED];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="levent">The event data.</param>
		// Token: 0x060026FC RID: 9980 RVA: 0x000B4978 File Offset: 0x000B2B78
		protected override void OnLayout(LayoutEventArgs levent)
		{
			if (this.AutoSize)
			{
				Size preferredSize = base.PreferredSize;
				this.minAutoSize = preferredSize;
				Size size = ((this.AutoSizeMode == AutoSizeMode.GrowAndShrink) ? preferredSize : LayoutUtils.UnionSizes(preferredSize, this.Size));
				if (this != null)
				{
					((IArrangedElement)this).SetBounds(new Rectangle(base.Left, base.Top, size.Width, size.Height), BoundsSpecified.None);
				}
			}
			base.OnLayout(levent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060026FD RID: 9981 RVA: 0x000B49E8 File Offset: 0x000B2BE8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLoad(EventArgs e)
		{
			Application.OpenFormsInternalAdd(this);
			if (Application.UseWaitCursor)
			{
				base.UseWaitCursor = true;
			}
			if (this.formState[Form.FormStateAutoScaling] == 1 && !base.DesignMode)
			{
				this.formState[Form.FormStateAutoScaling] = 0;
				this.ApplyAutoScaling();
			}
			if (base.GetState(32))
			{
				FormStartPosition formStartPosition = (FormStartPosition)this.formState[Form.FormStateStartPos];
				if (formStartPosition == FormStartPosition.CenterParent)
				{
					this.CenterToParent();
				}
				else if (formStartPosition == FormStartPosition.CenterScreen)
				{
					this.CenterToScreen();
				}
			}
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_LOAD];
			if (eventHandler != null)
			{
				string text = this.Text;
				eventHandler(this, e);
				foreach (object obj in base.Controls)
				{
					Control control = (Control)obj;
					control.Invalidate();
				}
			}
			if (base.IsHandleCreated)
			{
				base.BeginInvoke(new MethodInvoker(this.CallShownEvent));
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MaximizedBoundsChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060026FE RID: 9982 RVA: 0x000B4B00 File Offset: 0x000B2D00
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMaximizedBoundsChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Form.EVENT_MAXIMIZEDBOUNDSCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MaximumSizeChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060026FF RID: 9983 RVA: 0x000B4B30 File Offset: 0x000B2D30
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMaximumSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Form.EVENT_MAXIMUMSIZECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MinimumSizeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002700 RID: 9984 RVA: 0x000B4B60 File Offset: 0x000B2D60
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMinimumSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Form.EVENT_MINIMUMSIZECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.InputLanguageChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.InputLanguageChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06002701 RID: 9985 RVA: 0x000B4B90 File Offset: 0x000B2D90
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnInputLanguageChanged(InputLanguageChangedEventArgs e)
		{
			InputLanguageChangedEventHandler inputLanguageChangedEventHandler = (InputLanguageChangedEventHandler)base.Events[Form.EVENT_INPUTLANGCHANGE];
			if (inputLanguageChangedEventHandler != null)
			{
				inputLanguageChangedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.InputLanguageChanging" /> event.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.InputLanguageChangingEventArgs" /> that contains the event data.</param>
		// Token: 0x06002702 RID: 9986 RVA: 0x000B4BC0 File Offset: 0x000B2DC0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnInputLanguageChanging(InputLanguageChangingEventArgs e)
		{
			InputLanguageChangingEventHandler inputLanguageChangingEventHandler = (InputLanguageChangingEventHandler)base.Events[Form.EVENT_INPUTLANGCHANGEREQUEST];
			if (inputLanguageChangingEventHandler != null)
			{
				inputLanguageChangingEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002703 RID: 9987 RVA: 0x000B4BF0 File Offset: 0x000B2DF0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnVisibleChanged(EventArgs e)
		{
			this.UpdateRenderSizeGrip();
			Form mdiParentInternal = this.MdiParentInternal;
			if (mdiParentInternal != null)
			{
				mdiParentInternal.UpdateMdiWindowListStrip();
			}
			base.OnVisibleChanged(e);
			bool flag = false;
			if (base.IsHandleCreated && base.Visible && this.AcceptButton != null && UnsafeNativeMethods.SystemParametersInfo(95, 0, ref flag, 0) && flag)
			{
				Control control = this.AcceptButton as Control;
				NativeMethods.POINT point = new NativeMethods.POINT(control.Left + control.Width / 2, control.Top + control.Height / 2);
				UnsafeNativeMethods.ClientToScreen(new HandleRef(this, base.Handle), point);
				if (!control.IsWindowObscured)
				{
					IntSecurity.AdjustCursorPosition.Assert();
					try
					{
						Cursor.Position = new Point(point.x, point.y);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MdiChildActivate" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002704 RID: 9988 RVA: 0x000B4CCC File Offset: 0x000B2ECC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMdiChildActivate(EventArgs e)
		{
			this.UpdateMenuHandles();
			this.UpdateToolStrip();
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_MDI_CHILD_ACTIVATE];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MenuStart" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002705 RID: 9989 RVA: 0x000B4D08 File Offset: 0x000B2F08
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMenuStart(EventArgs e)
		{
			Form.SecurityToolTip securityToolTip = (Form.SecurityToolTip)base.Properties.GetObject(Form.PropSecurityTip);
			if (securityToolTip != null)
			{
				securityToolTip.Pop(true);
			}
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_MENUSTART];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.MenuComplete" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002706 RID: 9990 RVA: 0x000B4D58 File Offset: 0x000B2F58
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMenuComplete(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_MENUCOMPLETE];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06002707 RID: 9991 RVA: 0x000B4D88 File Offset: 0x000B2F88
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.formState[Form.FormStateRenderSizeGrip] != 0)
			{
				Size clientSize = this.ClientSize;
				if (Application.RenderWithVisualStyles)
				{
					if (this.sizeGripRenderer == null)
					{
						this.sizeGripRenderer = new VisualStyleRenderer(VisualStyleElement.Status.Gripper.Normal);
					}
					this.sizeGripRenderer.DrawBackground(e.Graphics, new Rectangle(clientSize.Width - 16, clientSize.Height - 16, 16, 16));
				}
				else
				{
					ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, clientSize.Width - 16, clientSize.Height - 16, 16, 16);
				}
			}
			if (this.IsMdiContainer)
			{
				e.Graphics.FillRectangle(SystemBrushes.AppWorkspace, base.ClientRectangle);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002708 RID: 9992 RVA: 0x000B4E4B File Offset: 0x000B304B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.formState[Form.FormStateRenderSizeGrip] != 0)
			{
				base.Invalidate();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.DpiChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.DpiChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06002709 RID: 9993 RVA: 0x000B4E6C File Offset: 0x000B306C
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		protected virtual void OnDpiChanged(DpiChangedEventArgs e)
		{
			if (e.DeviceDpiNew != e.DeviceDpiOld)
			{
				CommonProperties.xClearAllPreferredSizeCaches(this);
				DpiChangedEventHandler dpiChangedEventHandler = (DpiChangedEventHandler)base.Events[Form.EVENT_DPI_CHANGED];
				if (dpiChangedEventHandler != null)
				{
					dpiChangedEventHandler(this, e);
				}
				if (!e.Cancel)
				{
					float num = (float)e.DeviceDpiNew / (float)e.DeviceDpiOld;
					base.SuspendAllLayout(this);
					try
					{
						if (DpiHelper.EnableDpiChangedHighDpiImprovements && num < 1f)
						{
							this.MinimumSize = new Size(e.SuggestedRectangle.Width, e.SuggestedRectangle.Height);
						}
						SafeNativeMethods.SetWindowPos(new HandleRef(this, base.HandleInternal), NativeMethods.NullHandleRef, e.SuggestedRectangle.X, e.SuggestedRectangle.Y, e.SuggestedRectangle.Width, e.SuggestedRectangle.Height, 20);
						if (base.AutoScaleMode != AutoScaleMode.Font)
						{
							this.Font = (DpiHelper.EnableDpiChangedHighDpiImprovements ? new Font(this.Font.FontFamily, this.Font.Size * num, this.Font.Style, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont) : new Font(this.Font.FontFamily, this.Font.Size * num, this.Font.Style));
							base.FormDpiChanged(num);
						}
						else
						{
							base.ScaleFont(num);
							base.FormDpiChanged(num);
						}
					}
					finally
					{
						base.ResumeAllLayout(this, DpiHelper.EnableDpiChangedHighDpiImprovements);
					}
				}
			}
		}

		/// <summary>Occurs when the DPI setting changes on the display device where the form is currently displayed.</summary>
		// Token: 0x140001B4 RID: 436
		// (add) Token: 0x0600270A RID: 9994 RVA: 0x000B502C File Offset: 0x000B322C
		// (remove) Token: 0x0600270B RID: 9995 RVA: 0x000B503F File Offset: 0x000B323F
		[SRCategory("CatLayout")]
		[SRDescription("FormOnDpiChangedDescr")]
		public event DpiChangedEventHandler DpiChanged
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_DPI_CHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_DPI_CHANGED, value);
			}
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000B5054 File Offset: 0x000B3254
		private void WmDpiChanged(ref Message m)
		{
			this.DefWndProc(ref m);
			DpiChangedEventArgs dpiChangedEventArgs = new DpiChangedEventArgs(this.deviceDpi, m);
			this.deviceDpi = dpiChangedEventArgs.DeviceDpiNew;
			this.OnDpiChanged(dpiChangedEventArgs);
		}

		/// <summary>Raises the GetDpiScaledSize event.</summary>
		/// <param name="deviceDpiOld">The DPI value for the display device where the form was previously displayed.</param>
		/// <param name="deviceDpiNew">The DPI value for the display device where the form will be displayed.</param>
		/// <param name="desiredSize">A <see cref="T:System.Drawing.Size" /> representing the new size of the form based on the new DPI value.</param>
		/// <returns>
		///   <see langword="true" /> if successful; otherwise <see langword="false" />.</returns>
		// Token: 0x0600270D RID: 9997 RVA: 0x0001180C File Offset: 0x0000FA0C
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual bool OnGetDpiScaledSize(int deviceDpiOld, int deviceDpiNew, ref Size desiredSize)
		{
			return false;
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000B5090 File Offset: 0x000B3290
		private void WmGetDpiScaledSize(ref Message m)
		{
			this.DefWndProc(ref m);
			Size size = default(Size);
			if (this.OnGetDpiScaledSize(this.deviceDpi, NativeMethods.Util.SignedLOWORD(m.WParam), ref size))
			{
				m.Result = (IntPtr)(((this.Size.Height & 65535) << 16) | (this.Size.Width & 65535));
				return;
			}
			m.Result = IntPtr.Zero;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.RightToLeftLayoutChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600270F RID: 9999 RVA: 0x000B510C File Offset: 0x000B330C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			if (base.GetAnyDisposingInHierarchy())
			{
				return;
			}
			if (this.RightToLeft == RightToLeft.Yes)
			{
				base.RecreateHandle();
			}
			EventHandler eventHandler = base.Events[Form.EVENT_RIGHTTOLEFTLAYOUTCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (this.RightToLeft == RightToLeft.Yes)
			{
				foreach (object obj in base.Controls)
				{
					Control control = (Control)obj;
					control.RecreateHandleCore();
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Shown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002710 RID: 10000 RVA: 0x000B51A8 File Offset: 0x000B33A8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnShown(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_SHOWN];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002711 RID: 10001 RVA: 0x000B51D8 File Offset: 0x000B33D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			int num = ((this.Text.Length == 0) ? 1 : 0);
			if (!this.ControlBox && this.formState[Form.FormStateIsTextEmpty] != num)
			{
				base.RecreateHandle();
			}
			this.formState[Form.FormStateIsTextEmpty] = num;
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x000B5230 File Offset: 0x000B3430
		internal void PerformOnInputLanguageChanged(InputLanguageChangedEventArgs iplevent)
		{
			this.OnInputLanguageChanged(iplevent);
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000B5239 File Offset: 0x000B3439
		internal void PerformOnInputLanguageChanging(InputLanguageChangingEventArgs iplcevent)
		{
			this.OnInputLanguageChanging(iplcevent);
		}

		/// <summary>Processes a command key.</summary>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the Win32 message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the keystroke was processed and consumed by the control; otherwise, <see langword="false" /> to allow further processing.</returns>
		// Token: 0x06002714 RID: 10004 RVA: 0x000B5244 File Offset: 0x000B3444
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (base.ProcessCmdKey(ref msg, keyData))
			{
				return true;
			}
			MainMenu mainMenu = (MainMenu)base.Properties.GetObject(Form.PropCurMenu);
			if (mainMenu != null && mainMenu.ProcessCmdKey(ref msg, keyData))
			{
				return true;
			}
			bool flag = false;
			NativeMethods.MSG msg2 = default(NativeMethods.MSG);
			msg2.message = msg.Msg;
			msg2.wParam = msg.WParam;
			msg2.lParam = msg.LParam;
			msg2.hwnd = msg.HWnd;
			if (this.ctlClient != null && this.ctlClient.Handle != IntPtr.Zero && UnsafeNativeMethods.TranslateMDISysAccel(this.ctlClient.Handle, ref msg2))
			{
				flag = true;
			}
			msg.Msg = msg2.message;
			msg.WParam = msg2.wParam;
			msg.LParam = msg2.lParam;
			msg.HWnd = msg2.hwnd;
			return flag;
		}

		/// <summary>Processes a dialog box key.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the keystroke was processed and consumed by the control; otherwise, <see langword="false" /> to allow further processing.</returns>
		// Token: 0x06002715 RID: 10005 RVA: 0x000B5328 File Offset: 0x000B3528
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & (Keys.Control | Keys.Alt)) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys != Keys.Return)
				{
					if (keys == Keys.Escape)
					{
						IButtonControl buttonControl = (IButtonControl)base.Properties.GetObject(Form.PropCancelButton);
						if (buttonControl != null)
						{
							buttonControl.PerformClick();
							return true;
						}
					}
				}
				else
				{
					IButtonControl buttonControl = (IButtonControl)base.Properties.GetObject(Form.PropDefaultButton);
					if (buttonControl != null)
					{
						if (buttonControl is Control)
						{
							buttonControl.PerformClick();
						}
						return true;
					}
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		/// <summary>Processes a dialog character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002716 RID: 10006 RVA: 0x000B53A4 File Offset: 0x000B35A4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogChar(char charCode)
		{
			if (this.IsMdiChild && charCode != ' ')
			{
				if (this.ProcessMnemonic(charCode))
				{
					return true;
				}
				this.formStateEx[Form.FormStateExMnemonicProcessed] = 1;
				try
				{
					return base.ProcessDialogChar(charCode);
				}
				finally
				{
					this.formStateEx[Form.FormStateExMnemonicProcessed] = 0;
				}
			}
			return base.ProcessDialogChar(charCode);
		}

		/// <summary>Previews a keyboard message.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <returns>
		///   <see langword="true" /> if the message was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002717 RID: 10007 RVA: 0x000B5410 File Offset: 0x000B3610
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessKeyPreview(ref Message m)
		{
			return (this.formState[Form.FormStateKeyPreview] != 0 && this.ProcessKeyEventArgs(ref m)) || base.ProcessKeyPreview(ref m);
		}

		/// <summary>Selects the next available control and makes it the active control.</summary>
		/// <param name="forward">
		///   <see langword="true" /> to cycle forward through the controls in the ContainerControl; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if a control is selected; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002718 RID: 10008 RVA: 0x000B5438 File Offset: 0x000B3638
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessTabKey(bool forward)
		{
			if (base.SelectNextControl(base.ActiveControl, forward, true, true, true))
			{
				return true;
			}
			if (this.IsMdiChild || base.ParentFormInternal == null)
			{
				bool flag = base.SelectNextControl(null, forward, true, true, false);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000B547C File Offset: 0x000B367C
		internal void RaiseFormClosedOnAppExit()
		{
			if (!this.Modal)
			{
				int integer = base.Properties.GetInteger(Form.PropOwnedFormsCount);
				if (integer > 0)
				{
					Form[] ownedForms = this.OwnedForms;
					FormClosedEventArgs formClosedEventArgs = new FormClosedEventArgs(CloseReason.FormOwnerClosing);
					for (int i = integer - 1; i >= 0; i--)
					{
						if (ownedForms[i] != null && !Application.OpenFormsInternal.Contains(ownedForms[i]))
						{
							ownedForms[i].OnFormClosed(formClosedEventArgs);
						}
					}
				}
			}
			this.OnFormClosed(new FormClosedEventArgs(CloseReason.ApplicationExitCall));
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000B54EC File Offset: 0x000B36EC
		internal bool RaiseFormClosingOnAppExit()
		{
			FormClosingEventArgs formClosingEventArgs = new FormClosingEventArgs(CloseReason.ApplicationExitCall, false);
			if (!this.Modal)
			{
				int integer = base.Properties.GetInteger(Form.PropOwnedFormsCount);
				if (integer > 0)
				{
					Form[] ownedForms = this.OwnedForms;
					FormClosingEventArgs formClosingEventArgs2 = new FormClosingEventArgs(CloseReason.FormOwnerClosing, false);
					for (int i = integer - 1; i >= 0; i--)
					{
						if (ownedForms[i] != null && !Application.OpenFormsInternal.Contains(ownedForms[i]))
						{
							ownedForms[i].OnFormClosing(formClosingEventArgs2);
							if (formClosingEventArgs2.Cancel)
							{
								formClosingEventArgs.Cancel = true;
								break;
							}
						}
					}
				}
			}
			this.OnFormClosing(formClosingEventArgs);
			return formClosingEventArgs.Cancel;
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000B5580 File Offset: 0x000B3780
		internal override void RecreateHandleCore()
		{
			NativeMethods.WINDOWPLACEMENT windowplacement = default(NativeMethods.WINDOWPLACEMENT);
			FormStartPosition formStartPosition = FormStartPosition.Manual;
			if (!this.IsMdiChild && (this.WindowState == FormWindowState.Minimized || this.WindowState == FormWindowState.Maximized))
			{
				windowplacement.length = Marshal.SizeOf(typeof(NativeMethods.WINDOWPLACEMENT));
				UnsafeNativeMethods.GetWindowPlacement(new HandleRef(this, base.Handle), ref windowplacement);
			}
			if (this.StartPosition != FormStartPosition.Manual)
			{
				formStartPosition = this.StartPosition;
				this.StartPosition = FormStartPosition.Manual;
			}
			Form.EnumThreadWindowsCallback enumThreadWindowsCallback = null;
			SafeNativeMethods.EnumThreadWindowsCallback enumThreadWindowsCallback2 = null;
			if (base.IsHandleCreated)
			{
				enumThreadWindowsCallback = new Form.EnumThreadWindowsCallback();
				if (enumThreadWindowsCallback != null)
				{
					enumThreadWindowsCallback2 = new SafeNativeMethods.EnumThreadWindowsCallback(enumThreadWindowsCallback.Callback);
					UnsafeNativeMethods.EnumThreadWindows(SafeNativeMethods.GetCurrentThreadId(), new NativeMethods.EnumThreadWindowsCallback(enumThreadWindowsCallback2.Invoke), new HandleRef(this, base.Handle));
					enumThreadWindowsCallback.ResetOwners();
				}
			}
			base.RecreateHandleCore();
			if (enumThreadWindowsCallback != null)
			{
				enumThreadWindowsCallback.SetOwners(new HandleRef(this, base.Handle));
			}
			if (formStartPosition != FormStartPosition.Manual)
			{
				this.StartPosition = formStartPosition;
			}
			if (windowplacement.length > 0)
			{
				UnsafeNativeMethods.SetWindowPlacement(new HandleRef(this, base.Handle), ref windowplacement);
			}
			if (enumThreadWindowsCallback2 != null)
			{
				GC.KeepAlive(enumThreadWindowsCallback2);
			}
		}

		/// <summary>Removes an owned form from this form.</summary>
		/// <param name="ownedForm">A <see cref="T:System.Windows.Forms.Form" /> representing the form to remove from the list of owned forms for this form.</param>
		// Token: 0x0600271C RID: 10012 RVA: 0x000B5688 File Offset: 0x000B3888
		public void RemoveOwnedForm(Form ownedForm)
		{
			if (ownedForm == null)
			{
				return;
			}
			if (ownedForm.OwnerInternal != null)
			{
				ownedForm.Owner = null;
				return;
			}
			Form[] array = (Form[])base.Properties.GetObject(Form.PropOwnedForms);
			int num = base.Properties.GetInteger(Form.PropOwnedFormsCount);
			if (array != null)
			{
				for (int i = 0; i < num; i++)
				{
					if (ownedForm.Equals(array[i]))
					{
						array[i] = null;
						if (i + 1 < num)
						{
							Array.Copy(array, i + 1, array, i, num - i - 1);
							array[num - 1] = null;
						}
						num--;
					}
				}
				base.Properties.SetInteger(Form.PropOwnedFormsCount, num);
			}
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x000B571F File Offset: 0x000B391F
		private void ResetIcon()
		{
			this.icon = null;
			if (this.smallIcon != null)
			{
				this.smallIcon.Dispose();
				this.smallIcon = null;
			}
			this.formState[Form.FormStateIconSet] = 0;
			this.UpdateWindowIcon(true);
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x000B575C File Offset: 0x000B395C
		private void ResetSecurityTip(bool modalOnly)
		{
			Form.SecurityToolTip securityToolTip = (Form.SecurityToolTip)base.Properties.GetObject(Form.PropSecurityTip);
			if (securityToolTip != null && ((modalOnly && securityToolTip.Modal) || !modalOnly))
			{
				securityToolTip.Dispose();
				base.Properties.SetObject(Form.PropSecurityTip, null);
			}
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x000B57A9 File Offset: 0x000B39A9
		private void ResetTransparencyKey()
		{
			this.TransparencyKey = Color.Empty;
		}

		/// <summary>Occurs when a form enters resizing mode.</summary>
		// Token: 0x140001B5 RID: 437
		// (add) Token: 0x06002720 RID: 10016 RVA: 0x000B57B6 File Offset: 0x000B39B6
		// (remove) Token: 0x06002721 RID: 10017 RVA: 0x000B57C9 File Offset: 0x000B39C9
		[SRCategory("CatAction")]
		[SRDescription("FormOnResizeBeginDescr")]
		public event EventHandler ResizeBegin
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_RESIZEBEGIN, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_RESIZEBEGIN, value);
			}
		}

		/// <summary>Occurs when a form exits resizing mode.</summary>
		// Token: 0x140001B6 RID: 438
		// (add) Token: 0x06002722 RID: 10018 RVA: 0x000B57DC File Offset: 0x000B39DC
		// (remove) Token: 0x06002723 RID: 10019 RVA: 0x000B57EF File Offset: 0x000B39EF
		[SRCategory("CatAction")]
		[SRDescription("FormOnResizeEndDescr")]
		public event EventHandler ResizeEnd
		{
			add
			{
				base.Events.AddHandler(Form.EVENT_RESIZEEND, value);
			}
			remove
			{
				base.Events.RemoveHandler(Form.EVENT_RESIZEEND, value);
			}
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x000B5802 File Offset: 0x000B3A02
		private void ResumeLayoutFromMinimize()
		{
			if (this.formState[Form.FormStateWindowState] == 1)
			{
				base.ResumeLayout();
			}
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x000B5820 File Offset: 0x000B3A20
		private void RestoreWindowBoundsIfNecessary()
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				Size size = this.restoredWindowBounds.Size;
				if ((this.restoredWindowBoundsSpecified & BoundsSpecified.Size) != BoundsSpecified.None)
				{
					size = base.SizeFromClientSize(size.Width, size.Height);
				}
				base.SetBounds(this.restoredWindowBounds.X, this.restoredWindowBounds.Y, (this.formStateEx[Form.FormStateExWindowBoundsWidthIsClientSize] == 1) ? size.Width : this.restoredWindowBounds.Width, (this.formStateEx[Form.FormStateExWindowBoundsHeightIsClientSize] == 1) ? size.Height : this.restoredWindowBounds.Height, this.restoredWindowBoundsSpecified);
				this.restoredWindowBoundsSpecified = BoundsSpecified.None;
				this.restoredWindowBounds = new Rectangle(-1, -1, -1, -1);
				this.formStateEx[Form.FormStateExWindowBoundsHeightIsClientSize] = 0;
				this.formStateEx[Form.FormStateExWindowBoundsWidthIsClientSize] = 0;
			}
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000B590C File Offset: 0x000B3B0C
		private void RestrictedProcessNcActivate()
		{
			if (base.IsDisposed || base.Disposing)
			{
				return;
			}
			Form.SecurityToolTip securityToolTip = (Form.SecurityToolTip)base.Properties.GetObject(Form.PropSecurityTip);
			if (securityToolTip == null)
			{
				if (base.IsHandleCreated && UnsafeNativeMethods.GetForegroundWindow() == base.Handle)
				{
					securityToolTip = new Form.SecurityToolTip(this);
					base.Properties.SetObject(Form.PropSecurityTip, securityToolTip);
					return;
				}
			}
			else
			{
				if (!base.IsHandleCreated || UnsafeNativeMethods.GetForegroundWindow() != base.Handle)
				{
					securityToolTip.Pop(false);
					return;
				}
				securityToolTip.Show();
			}
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000B59A0 File Offset: 0x000B3BA0
		private void ResumeUpdateMenuHandles()
		{
			int num = this.formStateEx[Form.FormStateExUpdateMenuHandlesSuspendCount];
			if (num <= 0)
			{
				throw new InvalidOperationException(SR.GetString("TooManyResumeUpdateMenuHandles"));
			}
			if ((this.formStateEx[Form.FormStateExUpdateMenuHandlesSuspendCount] = num - 1) == 0 && this.formStateEx[Form.FormStateExUpdateMenuHandlesDeferred] != 0)
			{
				this.UpdateMenuHandles();
			}
		}

		/// <summary>Selects this form, and optionally selects the next or previous control.</summary>
		/// <param name="directed">If set to true that the active control is changed</param>
		/// <param name="forward">If directed is true, then this controls the direction in which focus is moved. If this is <see langword="true" />, then the next control is selected; otherwise, the previous control is selected.</param>
		// Token: 0x06002728 RID: 10024 RVA: 0x000B5A02 File Offset: 0x000B3C02
		protected override void Select(bool directed, bool forward)
		{
			IntSecurity.ModifyFocus.Demand();
			this.SelectInternal(directed, forward);
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x000B5A18 File Offset: 0x000B3C18
		private void SelectInternal(bool directed, bool forward)
		{
			IntSecurity.ModifyFocus.Assert();
			if (directed)
			{
				base.SelectNextControl(null, forward, true, true, false);
			}
			if (this.TopLevel)
			{
				UnsafeNativeMethods.SetActiveWindow(new HandleRef(this, base.Handle));
				return;
			}
			if (this.IsMdiChild)
			{
				UnsafeNativeMethods.SetActiveWindow(new HandleRef(this.MdiParentInternal, this.MdiParentInternal.Handle));
				this.MdiParentInternal.MdiClient.SendMessage(546, base.Handle, 0);
				return;
			}
			Form parentFormInternal = base.ParentFormInternal;
			if (parentFormInternal != null)
			{
				parentFormInternal.ActiveControl = this;
			}
		}

		/// <summary>Performs scaling of the form.</summary>
		/// <param name="x">Percentage to scale the form horizontally</param>
		/// <param name="y">Percentage to scale the form vertically</param>
		// Token: 0x0600272A RID: 10026 RVA: 0x000B5AAC File Offset: 0x000B3CAC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float x, float y)
		{
			base.SuspendLayout();
			try
			{
				if (this.WindowState == FormWindowState.Normal)
				{
					Size clientSize = this.ClientSize;
					Size minimumSize = this.MinimumSize;
					Size maximumSize = this.MaximumSize;
					if (!this.MinimumSize.IsEmpty)
					{
						this.MinimumSize = base.ScaleSize(minimumSize, x, y);
					}
					if (!this.MaximumSize.IsEmpty)
					{
						this.MaximumSize = base.ScaleSize(maximumSize, x, y);
					}
					this.ClientSize = base.ScaleSize(clientSize, x, y);
				}
				base.ScaleDockPadding(x, y);
				foreach (object obj in base.Controls)
				{
					Control control = (Control)obj;
					if (control != null)
					{
						control.Scale(x, y);
					}
				}
			}
			finally
			{
				base.ResumeLayout();
			}
		}

		/// <summary>Retrieves the bounds within which the control is scaled.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display bounds.</param>
		/// <param name="factor">The height and width of the control's bounds.</param>
		/// <param name="specified">One of the values of <see cref="T:System.Windows.Forms.BoundsSpecified" /> that specifies the bounds of the control to use when defining its size and position.</param>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the bounds within which the control is scaled.</returns>
		// Token: 0x0600272B RID: 10027 RVA: 0x000B5BA0 File Offset: 0x000B3DA0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			if (this.WindowState != FormWindowState.Normal)
			{
				bounds = this.RestoreBounds;
			}
			return base.GetScaledBounds(bounds, factor, specified);
		}

		/// <summary>Scales the location, size, padding, and margin of a control.</summary>
		/// <param name="factor">The factor by which the height and width of the control are scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x0600272C RID: 10028 RVA: 0x000B5BBC File Offset: 0x000B3DBC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			this.formStateEx[Form.FormStateExInScale] = 1;
			try
			{
				if (this.MdiParentInternal != null)
				{
					specified &= ~(BoundsSpecified.X | BoundsSpecified.Y);
				}
				base.ScaleControl(factor, specified);
			}
			finally
			{
				this.formStateEx[Form.FormStateExInScale] = 0;
			}
		}

		/// <summary>Performs the work of setting the specified bounds of this control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x0600272D RID: 10029 RVA: 0x000B5C14 File Offset: 0x000B3E14
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.WindowState != FormWindowState.Normal)
			{
				if (x != -1 || y != -1)
				{
					this.restoredWindowBoundsSpecified |= specified & BoundsSpecified.Location;
				}
				this.restoredWindowBoundsSpecified |= specified & BoundsSpecified.Size;
				if ((specified & BoundsSpecified.X) != BoundsSpecified.None)
				{
					this.restoredWindowBounds.X = x;
				}
				if ((specified & BoundsSpecified.Y) != BoundsSpecified.None)
				{
					this.restoredWindowBounds.Y = y;
				}
				if ((specified & BoundsSpecified.Width) != BoundsSpecified.None)
				{
					this.restoredWindowBounds.Width = width;
					this.formStateEx[Form.FormStateExWindowBoundsWidthIsClientSize] = 0;
				}
				if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
				{
					this.restoredWindowBounds.Height = height;
					this.formStateEx[Form.FormStateExWindowBoundsHeightIsClientSize] = 0;
				}
			}
			if ((specified & BoundsSpecified.X) != BoundsSpecified.None)
			{
				this.restoreBounds.X = x;
			}
			if ((specified & BoundsSpecified.Y) != BoundsSpecified.None)
			{
				this.restoreBounds.Y = y;
			}
			if ((specified & BoundsSpecified.Width) != BoundsSpecified.None || this.restoreBounds.Width == -1)
			{
				this.restoreBounds.Width = width;
			}
			if ((specified & BoundsSpecified.Height) != BoundsSpecified.None || this.restoreBounds.Height == -1)
			{
				this.restoreBounds.Height = height;
			}
			if (this.WindowState == FormWindowState.Normal && (base.Height != height || base.Width != width))
			{
				Size maxWindowTrackSize = SystemInformation.MaxWindowTrackSize;
				if (height > maxWindowTrackSize.Height)
				{
					height = maxWindowTrackSize.Height;
				}
				if (width > maxWindowTrackSize.Width)
				{
					width = maxWindowTrackSize.Width;
				}
			}
			FormBorderStyle formBorderStyle = this.FormBorderStyle;
			if (formBorderStyle != FormBorderStyle.None && formBorderStyle != FormBorderStyle.FixedToolWindow && formBorderStyle != FormBorderStyle.SizableToolWindow && this.ParentInternal == null)
			{
				Size minWindowTrackSize = SystemInformation.MinWindowTrackSize;
				if (height < minWindowTrackSize.Height)
				{
					height = minWindowTrackSize.Height;
				}
				if (width < minWindowTrackSize.Width)
				{
					width = minWindowTrackSize.Width;
				}
			}
			if (this.IsRestrictedWindow)
			{
				Rectangle rectangle = this.ApplyBoundsConstraints(x, y, width, height);
				if (rectangle != new Rectangle(x, y, width, height))
				{
					base.SetBoundsCore(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, BoundsSpecified.All);
					return;
				}
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000B5E10 File Offset: 0x000B4010
		internal override Rectangle ApplyBoundsConstraints(int suggestedX, int suggestedY, int proposedWidth, int proposedHeight)
		{
			Rectangle rectangle = base.ApplyBoundsConstraints(suggestedX, suggestedY, proposedWidth, proposedHeight);
			if (this.IsRestrictedWindow)
			{
				Screen[] allScreens = Screen.AllScreens;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				for (int i = 0; i < allScreens.Length; i++)
				{
					Rectangle workingArea = allScreens[i].WorkingArea;
					if (workingArea.Contains(suggestedX, suggestedY))
					{
						flag = true;
					}
					if (workingArea.Contains(suggestedX + proposedWidth, suggestedY))
					{
						flag2 = true;
					}
					if (workingArea.Contains(suggestedX, suggestedY + proposedHeight))
					{
						flag3 = true;
					}
					if (workingArea.Contains(suggestedX + proposedWidth, suggestedY + proposedHeight))
					{
						flag4 = true;
					}
				}
				if (!flag || !flag2 || !flag3 || !flag4)
				{
					if (this.formStateEx[Form.FormStateExInScale] == 1)
					{
						rectangle = WindowsFormsUtils.ConstrainToScreenWorkingAreaBounds(rectangle);
					}
					else
					{
						rectangle.X = base.Left;
						rectangle.Y = base.Top;
						rectangle.Width = base.Width;
						rectangle.Height = base.Height;
					}
				}
			}
			return rectangle;
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000B5F00 File Offset: 0x000B4100
		private void SetDefaultButton(IButtonControl button)
		{
			IButtonControl buttonControl = (IButtonControl)base.Properties.GetObject(Form.PropDefaultButton);
			if (buttonControl != button)
			{
				if (buttonControl != null)
				{
					buttonControl.NotifyDefault(false);
				}
				base.Properties.SetObject(Form.PropDefaultButton, button);
				if (button != null)
				{
					button.NotifyDefault(true);
				}
			}
		}

		/// <summary>Sets the client size of the form. This will adjust the bounds of the form to make the client size the requested size.</summary>
		/// <param name="x">Requested width of the client region.</param>
		/// <param name="y">Requested height of the client region.</param>
		// Token: 0x06002730 RID: 10032 RVA: 0x000B5F4C File Offset: 0x000B414C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void SetClientSizeCore(int x, int y)
		{
			bool hscroll = base.HScroll;
			bool vscroll = base.VScroll;
			base.SetClientSizeCore(x, y);
			if (base.IsHandleCreated)
			{
				if (base.VScroll != vscroll && base.VScroll)
				{
					x += SystemInformation.VerticalScrollBarWidth;
				}
				if (base.HScroll != hscroll && base.HScroll)
				{
					y += SystemInformation.HorizontalScrollBarHeight;
				}
				if (x != this.ClientSize.Width || y != this.ClientSize.Height)
				{
					base.SetClientSizeCore(x, y);
				}
			}
			this.formState[Form.FormStateSetClientSize] = 1;
		}

		/// <summary>Sets the bounds of the form in desktop coordinates.</summary>
		/// <param name="x">The x-coordinate of the form's location.</param>
		/// <param name="y">The y-coordinate of the form's location.</param>
		/// <param name="width">The width of the form.</param>
		/// <param name="height">The height of the form.</param>
		// Token: 0x06002731 RID: 10033 RVA: 0x000B5FE8 File Offset: 0x000B41E8
		public void SetDesktopBounds(int x, int y, int width, int height)
		{
			Rectangle workingArea = SystemInformation.WorkingArea;
			base.SetBounds(x + workingArea.X, y + workingArea.Y, width, height, BoundsSpecified.All);
		}

		/// <summary>Sets the location of the form in desktop coordinates.</summary>
		/// <param name="x">The x-coordinate of the form's location.</param>
		/// <param name="y">The y-coordinate of the form's location.</param>
		// Token: 0x06002732 RID: 10034 RVA: 0x000B6018 File Offset: 0x000B4218
		public void SetDesktopLocation(int x, int y)
		{
			Rectangle workingArea = SystemInformation.WorkingArea;
			this.Location = new Point(workingArea.X + x, workingArea.Y + y);
		}

		/// <summary>Shows the form with the specified owner to the user.</summary>
		/// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> and represents the top-level window that will own this form.</param>
		/// <exception cref="T:System.InvalidOperationException">The form being shown is already visible.  
		///  -or-  
		///  The form specified in the <paramref name="owner" /> parameter is the same as the form being shown.  
		///  -or-  
		///  The form being shown is disabled.  
		///  -or-  
		///  The form being shown is not a top-level window.  
		///  -or-  
		///  The form being shown as a dialog box is already a modal form.  
		///  -or-  
		///  The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" />).</exception>
		// Token: 0x06002733 RID: 10035 RVA: 0x000B6048 File Offset: 0x000B4248
		public void Show(IWin32Window owner)
		{
			if (owner == this)
			{
				throw new InvalidOperationException(SR.GetString("OwnsSelfOrOwner", new object[] { "Show" }));
			}
			if (base.Visible)
			{
				throw new InvalidOperationException(SR.GetString("ShowDialogOnVisible", new object[] { "Show" }));
			}
			if (!base.Enabled)
			{
				throw new InvalidOperationException(SR.GetString("ShowDialogOnDisabled", new object[] { "Show" }));
			}
			if (!this.TopLevel)
			{
				throw new InvalidOperationException(SR.GetString("ShowDialogOnNonTopLevel", new object[] { "Show" }));
			}
			if (!SystemInformation.UserInteractive)
			{
				throw new InvalidOperationException(SR.GetString("CantShowModalOnNonInteractive"));
			}
			if (owner != null && ((int)UnsafeNativeMethods.GetWindowLong(new HandleRef(owner, Control.GetSafeHandle(owner)), -20) & 8) == 0 && owner is Control)
			{
				owner = ((Control)owner).TopLevelControlInternal;
			}
			IntPtr activeWindow = UnsafeNativeMethods.GetActiveWindow();
			IntPtr intPtr = ((owner == null) ? activeWindow : Control.GetSafeHandle(owner));
			IntPtr intPtr2 = IntPtr.Zero;
			base.Properties.SetObject(Form.PropDialogOwner, owner);
			Form ownerInternal = this.OwnerInternal;
			if (owner is Form && owner != ownerInternal)
			{
				this.Owner = (Form)owner;
			}
			if (intPtr != IntPtr.Zero && intPtr != base.Handle)
			{
				if (UnsafeNativeMethods.GetWindowLong(new HandleRef(owner, intPtr), -8) == base.Handle)
				{
					throw new ArgumentException(SR.GetString("OwnsSelfOrOwner", new object[] { "show" }), "owner");
				}
				intPtr2 = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -8);
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -8, new HandleRef(owner, intPtr));
			}
			base.Visible = true;
		}

		/// <summary>Shows the form as a modal dialog box.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.InvalidOperationException">The form being shown is already visible.  
		///  -or-  
		///  The form being shown is disabled.  
		///  -or-  
		///  The form being shown is not a top-level window.  
		///  -or-  
		///  The form being shown as a dialog box is already a modal form.  
		///  -or-  
		///  The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" />).</exception>
		// Token: 0x06002734 RID: 10036 RVA: 0x000B620C File Offset: 0x000B440C
		public DialogResult ShowDialog()
		{
			return this.ShowDialog(null);
		}

		/// <summary>Shows the form as a modal dialog box with the specified owner.</summary>
		/// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The form specified in the <paramref name="owner" /> parameter is the same as the form being shown.</exception>
		/// <exception cref="T:System.InvalidOperationException">The form being shown is already visible.  
		///  -or-  
		///  The form being shown is disabled.  
		///  -or-  
		///  The form being shown is not a top-level window.  
		///  -or-  
		///  The form being shown as a dialog box is already a modal form.  
		///  -or-  
		///  The current process is not running in user interactive mode (for more information, see <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" />).</exception>
		// Token: 0x06002735 RID: 10037 RVA: 0x000B6218 File Offset: 0x000B4418
		public DialogResult ShowDialog(IWin32Window owner)
		{
			if (owner == this)
			{
				throw new ArgumentException(SR.GetString("OwnsSelfOrOwner", new object[] { "showDialog" }), "owner");
			}
			if (base.Visible)
			{
				throw new InvalidOperationException(SR.GetString("ShowDialogOnVisible", new object[] { "showDialog" }));
			}
			if (!base.Enabled)
			{
				throw new InvalidOperationException(SR.GetString("ShowDialogOnDisabled", new object[] { "showDialog" }));
			}
			if (!this.TopLevel)
			{
				throw new InvalidOperationException(SR.GetString("ShowDialogOnNonTopLevel", new object[] { "showDialog" }));
			}
			if (this.Modal)
			{
				throw new InvalidOperationException(SR.GetString("ShowDialogOnModal", new object[] { "showDialog" }));
			}
			if (!SystemInformation.UserInteractive)
			{
				throw new InvalidOperationException(SR.GetString("CantShowModalOnNonInteractive"));
			}
			if (owner != null && ((int)UnsafeNativeMethods.GetWindowLong(new HandleRef(owner, Control.GetSafeHandle(owner)), -20) & 8) == 0 && owner is Control)
			{
				owner = ((Control)owner).TopLevelControlInternal;
			}
			this.CalledOnLoad = false;
			this.CalledMakeVisible = false;
			this.CloseReason = CloseReason.None;
			IntPtr capture = UnsafeNativeMethods.GetCapture();
			if (capture != IntPtr.Zero)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(null, capture), 31, IntPtr.Zero, IntPtr.Zero);
				SafeNativeMethods.ReleaseCapture();
			}
			IntPtr intPtr = UnsafeNativeMethods.GetActiveWindow();
			IntPtr intPtr2 = ((owner == null) ? intPtr : Control.GetSafeHandle(owner));
			IntPtr intPtr3 = IntPtr.Zero;
			base.Properties.SetObject(Form.PropDialogOwner, owner);
			Form ownerInternal = this.OwnerInternal;
			if (owner is Form && owner != ownerInternal)
			{
				this.Owner = (Form)owner;
			}
			try
			{
				base.SetState(32, true);
				this.dialogResult = DialogResult.None;
				base.CreateControl();
				if (intPtr2 != IntPtr.Zero && intPtr2 != base.Handle)
				{
					if (UnsafeNativeMethods.GetWindowLong(new HandleRef(owner, intPtr2), -8) == base.Handle)
					{
						throw new ArgumentException(SR.GetString("OwnsSelfOrOwner", new object[] { "showDialog" }), "owner");
					}
					intPtr3 = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -8);
					UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -8, new HandleRef(owner, intPtr2));
				}
				try
				{
					if (this.dialogResult == DialogResult.None)
					{
						Application.RunDialog(this);
					}
				}
				finally
				{
					if (!UnsafeNativeMethods.IsWindow(new HandleRef(null, intPtr)))
					{
						intPtr = intPtr2;
					}
					if (UnsafeNativeMethods.IsWindow(new HandleRef(null, intPtr)) && SafeNativeMethods.IsWindowVisible(new HandleRef(null, intPtr)))
					{
						UnsafeNativeMethods.SetActiveWindow(new HandleRef(null, intPtr));
					}
					else if (UnsafeNativeMethods.IsWindow(new HandleRef(null, intPtr2)) && SafeNativeMethods.IsWindowVisible(new HandleRef(null, intPtr2)))
					{
						UnsafeNativeMethods.SetActiveWindow(new HandleRef(null, intPtr2));
					}
					this.SetVisibleCore(false);
					if (base.IsHandleCreated)
					{
						if (this.OwnerInternal != null && this.OwnerInternal.IsMdiContainer)
						{
							this.OwnerInternal.Invalidate(true);
							this.OwnerInternal.Update();
						}
						this.DestroyHandle();
					}
					base.SetState(32, false);
				}
			}
			finally
			{
				this.Owner = ownerInternal;
				base.Properties.SetObject(Form.PropDialogOwner, null);
			}
			return this.DialogResult;
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x000B095C File Offset: 0x000AEB5C
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeAutoScaleBaseSize()
		{
			return this.formState[Form.FormStateAutoScaling] != 0;
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x00012E4E File Offset: 0x0001104E
		private bool ShouldSerializeClientSize()
		{
			return true;
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000B6578 File Offset: 0x000B4778
		private bool ShouldSerializeIcon()
		{
			return this.formState[Form.FormStateIconSet] == 1;
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000B658D File Offset: 0x000B478D
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeLocation()
		{
			return base.Left != 0 || base.Top != 0;
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x0001180C File Offset: 0x0000FA0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal override bool ShouldSerializeSize()
		{
			return false;
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000B65A4 File Offset: 0x000B47A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal bool ShouldSerializeTransparencyKey()
		{
			return !this.TransparencyKey.Equals(Color.Empty);
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000B65D2 File Offset: 0x000B47D2
		private void SuspendLayoutForMinimize()
		{
			if (this.formState[Form.FormStateWindowState] != 1)
			{
				base.SuspendLayout();
			}
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000B65F0 File Offset: 0x000B47F0
		private void SuspendUpdateMenuHandles()
		{
			int num = this.formStateEx[Form.FormStateExUpdateMenuHandlesSuspendCount];
			this.formStateEx[Form.FormStateExUpdateMenuHandlesSuspendCount] = num + 1;
		}

		/// <summary>Gets a string representing the current instance of the form.</summary>
		/// <returns>A string consisting of the fully qualified name of the form object's class, with the <see cref="P:System.Windows.Forms.Form.Text" /> property of the form appended to the end. For example, if the form is derived from the class <c>MyForm</c> in the <c>MyNamespace</c> namespace, and the <see cref="P:System.Windows.Forms.Form.Text" /> property is set to <c>Hello, World</c>, this method will return <c>MyNamespace.MyForm, Text: Hello, World</c>.</returns>
		// Token: 0x0600273E RID: 10046 RVA: 0x000B6624 File Offset: 0x000B4824
		public override string ToString()
		{
			string text = base.ToString();
			return text + ", Text: " + this.Text;
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000B6649 File Offset: 0x000B4849
		private void UpdateAutoScaleBaseSize()
		{
			this.autoScaleBaseSize = Size.Empty;
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000B6658 File Offset: 0x000B4858
		private void UpdateRenderSizeGrip()
		{
			int num = this.formState[Form.FormStateRenderSizeGrip];
			switch (this.FormBorderStyle)
			{
			case FormBorderStyle.None:
			case FormBorderStyle.FixedSingle:
			case FormBorderStyle.Fixed3D:
			case FormBorderStyle.FixedDialog:
			case FormBorderStyle.FixedToolWindow:
				this.formState[Form.FormStateRenderSizeGrip] = 0;
				break;
			case FormBorderStyle.Sizable:
			case FormBorderStyle.SizableToolWindow:
				switch (this.SizeGripStyle)
				{
				case SizeGripStyle.Auto:
					if (base.GetState(32))
					{
						this.formState[Form.FormStateRenderSizeGrip] = 1;
					}
					else
					{
						this.formState[Form.FormStateRenderSizeGrip] = 0;
					}
					break;
				case SizeGripStyle.Show:
					this.formState[Form.FormStateRenderSizeGrip] = 1;
					break;
				case SizeGripStyle.Hide:
					this.formState[Form.FormStateRenderSizeGrip] = 0;
					break;
				}
				break;
			}
			if (this.formState[Form.FormStateRenderSizeGrip] != num)
			{
				base.Invalidate();
			}
		}

		/// <summary>Updates which button is the default button.</summary>
		// Token: 0x06002741 RID: 10049 RVA: 0x000B6740 File Offset: 0x000B4940
		protected override void UpdateDefaultButton()
		{
			ContainerControl containerControl = this;
			while (containerControl.ActiveControl is ContainerControl)
			{
				containerControl = containerControl.ActiveControl as ContainerControl;
				if (containerControl is Form)
				{
					containerControl = this;
					break;
				}
			}
			if (containerControl.ActiveControl is IButtonControl)
			{
				this.SetDefaultButton((IButtonControl)containerControl.ActiveControl);
				return;
			}
			this.SetDefaultButton(this.AcceptButton);
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000B67A4 File Offset: 0x000B49A4
		private void UpdateHandleWithOwner()
		{
			if (base.IsHandleCreated && this.TopLevel)
			{
				HandleRef handleRef = NativeMethods.NullHandleRef;
				Form form = (Form)base.Properties.GetObject(Form.PropOwner);
				if (form != null)
				{
					handleRef = new HandleRef(form, form.Handle);
				}
				else if (!this.ShowInTaskbar)
				{
					handleRef = this.TaskbarOwner;
				}
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -8, handleRef);
			}
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000B6814 File Offset: 0x000B4A14
		private void UpdateLayered()
		{
			if (this.formState[Form.FormStateLayered] != 0 && base.IsHandleCreated && this.TopLevel && OSFeature.Feature.IsPresent(OSFeature.LayeredWindows))
			{
				Color transparencyKey = this.TransparencyKey;
				bool flag;
				if (transparencyKey.IsEmpty)
				{
					flag = UnsafeNativeMethods.SetLayeredWindowAttributes(new HandleRef(this, base.Handle), 0, this.OpacityAsByte, 2);
				}
				else if (this.OpacityAsByte == 255)
				{
					flag = UnsafeNativeMethods.SetLayeredWindowAttributes(new HandleRef(this, base.Handle), ColorTranslator.ToWin32(transparencyKey), 0, 1);
				}
				else
				{
					flag = UnsafeNativeMethods.SetLayeredWindowAttributes(new HandleRef(this, base.Handle), ColorTranslator.ToWin32(transparencyKey), this.OpacityAsByte, 3);
				}
				if (!flag)
				{
					throw new Win32Exception();
				}
			}
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000B68DC File Offset: 0x000B4ADC
		private void UpdateMenuHandles()
		{
			if (base.Properties.GetObject(Form.PropCurMenu) != null)
			{
				base.Properties.SetObject(Form.PropCurMenu, null);
			}
			if (base.IsHandleCreated)
			{
				if (!this.TopLevel)
				{
					this.UpdateMenuHandles(null, true);
					return;
				}
				Form activeMdiChildInternal = this.ActiveMdiChildInternal;
				if (activeMdiChildInternal != null)
				{
					this.UpdateMenuHandles(activeMdiChildInternal.MergedMenuPrivate, true);
					return;
				}
				this.UpdateMenuHandles(this.Menu, true);
			}
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x000B694C File Offset: 0x000B4B4C
		private void UpdateMenuHandles(MainMenu menu, bool forceRedraw)
		{
			int num = this.formStateEx[Form.FormStateExUpdateMenuHandlesSuspendCount];
			if (num > 0 && menu != null)
			{
				this.formStateEx[Form.FormStateExUpdateMenuHandlesDeferred] = 1;
				return;
			}
			if (menu != null)
			{
				menu.form = this;
			}
			if (menu != null || base.Properties.ContainsObject(Form.PropCurMenu))
			{
				base.Properties.SetObject(Form.PropCurMenu, menu);
			}
			if (this.ctlClient == null || !this.ctlClient.IsHandleCreated)
			{
				if (menu != null)
				{
					UnsafeNativeMethods.SetMenu(new HandleRef(this, base.Handle), new HandleRef(menu, menu.Handle));
				}
				else
				{
					UnsafeNativeMethods.SetMenu(new HandleRef(this, base.Handle), NativeMethods.NullHandleRef);
				}
			}
			else
			{
				MenuStrip mainMenuStrip = this.MainMenuStrip;
				if (mainMenuStrip == null || menu != null)
				{
					MainMenu mainMenu = (MainMenu)base.Properties.GetObject(Form.PropDummyMenu);
					if (mainMenu == null)
					{
						mainMenu = new MainMenu();
						mainMenu.ownerForm = this;
						base.Properties.SetObject(Form.PropDummyMenu, mainMenu);
					}
					UnsafeNativeMethods.SendMessage(new HandleRef(this.ctlClient, this.ctlClient.Handle), 560, mainMenu.Handle, IntPtr.Zero);
					if (menu != null)
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this.ctlClient, this.ctlClient.Handle), 560, menu.Handle, IntPtr.Zero);
					}
				}
				if (menu == null && mainMenuStrip != null)
				{
					IntPtr menu2 = UnsafeNativeMethods.GetMenu(new HandleRef(this, base.Handle));
					if (menu2 != IntPtr.Zero)
					{
						UnsafeNativeMethods.SetMenu(new HandleRef(this, base.Handle), NativeMethods.NullHandleRef);
						Form activeMdiChildInternal = this.ActiveMdiChildInternal;
						if (activeMdiChildInternal != null && activeMdiChildInternal.WindowState == FormWindowState.Maximized)
						{
							activeMdiChildInternal.RecreateHandle();
						}
						CommonProperties.xClearPreferredSizeCache(this);
					}
				}
			}
			if (forceRedraw)
			{
				SafeNativeMethods.DrawMenuBar(new HandleRef(this, base.Handle));
			}
			this.formStateEx[Form.FormStateExUpdateMenuHandlesDeferred] = 0;
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000B6B38 File Offset: 0x000B4D38
		internal void UpdateFormStyles()
		{
			Size clientSize = this.ClientSize;
			base.UpdateStyles();
			if (!this.ClientSize.Equals(clientSize))
			{
				this.ClientSize = clientSize;
			}
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000B6B78 File Offset: 0x000B4D78
		private static Type FindClosestStockType(Type type)
		{
			Type[] array = new Type[] { typeof(MenuStrip) };
			foreach (Type type2 in array)
			{
				if (type2.IsAssignableFrom(type))
				{
					return type2;
				}
			}
			return null;
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000B6BBC File Offset: 0x000B4DBC
		private void UpdateToolStrip()
		{
			ToolStrip mainMenuStrip = this.MainMenuStrip;
			ArrayList arrayList = ToolStripManager.FindMergeableToolStrips(this.ActiveMdiChildInternal);
			if (mainMenuStrip != null)
			{
				ToolStripManager.RevertMerge(mainMenuStrip);
			}
			this.UpdateMdiWindowListStrip();
			if (this.ActiveMdiChildInternal != null)
			{
				foreach (object obj in arrayList)
				{
					ToolStrip toolStrip = (ToolStrip)obj;
					Type type = Form.FindClosestStockType(toolStrip.GetType());
					if (mainMenuStrip != null)
					{
						Type type2 = Form.FindClosestStockType(mainMenuStrip.GetType());
						if (type2 != null && type != null && type == type2 && mainMenuStrip.GetType().IsAssignableFrom(toolStrip.GetType()))
						{
							ToolStripManager.Merge(toolStrip, mainMenuStrip);
							break;
						}
					}
				}
			}
			Form activeMdiChildInternal = this.ActiveMdiChildInternal;
			this.UpdateMdiControlStrip(activeMdiChildInternal != null && activeMdiChildInternal.IsMaximized);
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000B6CB0 File Offset: 0x000B4EB0
		private void UpdateMdiControlStrip(bool maximized)
		{
			if (this.formStateEx[Form.FormStateExInUpdateMdiControlStrip] != 0)
			{
				return;
			}
			this.formStateEx[Form.FormStateExInUpdateMdiControlStrip] = 1;
			try
			{
				MdiControlStrip mdiControlStrip = this.MdiControlStrip;
				if (this.MdiControlStrip != null)
				{
					if (mdiControlStrip.MergedMenu != null)
					{
						ToolStripManager.RevertMergeInternal(mdiControlStrip.MergedMenu, mdiControlStrip, true);
					}
					mdiControlStrip.MergedMenu = null;
					mdiControlStrip.Dispose();
					this.MdiControlStrip = null;
				}
				if (this.ActiveMdiChildInternal != null && maximized && this.ActiveMdiChildInternal.ControlBox && this.Menu == null)
				{
					IntPtr menu = UnsafeNativeMethods.GetMenu(new HandleRef(this, base.Handle));
					if (menu == IntPtr.Zero)
					{
						MenuStrip mainMenuStrip = ToolStripManager.GetMainMenuStrip(this);
						if (mainMenuStrip != null)
						{
							this.MdiControlStrip = new MdiControlStrip(this.ActiveMdiChildInternal);
							ToolStripManager.Merge(this.MdiControlStrip, mainMenuStrip);
							this.MdiControlStrip.MergedMenu = mainMenuStrip;
						}
					}
				}
			}
			finally
			{
				this.formStateEx[Form.FormStateExInUpdateMdiControlStrip] = 0;
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000B6DB4 File Offset: 0x000B4FB4
		internal void UpdateMdiWindowListStrip()
		{
			if (this.IsMdiContainer)
			{
				if (this.MdiWindowListStrip != null && this.MdiWindowListStrip.MergedMenu != null)
				{
					ToolStripManager.RevertMergeInternal(this.MdiWindowListStrip.MergedMenu, this.MdiWindowListStrip, true);
				}
				MenuStrip mainMenuStrip = ToolStripManager.GetMainMenuStrip(this);
				if (mainMenuStrip != null && mainMenuStrip.MdiWindowListItem != null)
				{
					if (this.MdiWindowListStrip == null)
					{
						this.MdiWindowListStrip = new MdiWindowListStrip();
					}
					int count = mainMenuStrip.MdiWindowListItem.DropDownItems.Count;
					bool flag = count > 0 && !(mainMenuStrip.MdiWindowListItem.DropDownItems[count - 1] is ToolStripSeparator);
					this.MdiWindowListStrip.PopulateItems(this, mainMenuStrip.MdiWindowListItem, flag);
					ToolStripManager.Merge(this.MdiWindowListStrip, mainMenuStrip);
					this.MdiWindowListStrip.MergedMenu = mainMenuStrip;
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.ResizeBegin" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600274B RID: 10059 RVA: 0x000B6E84 File Offset: 0x000B5084
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnResizeBegin(EventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_RESIZEBEGIN];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.ResizeEnd" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600274C RID: 10060 RVA: 0x000B6EBC File Offset: 0x000B50BC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnResizeEnd(EventArgs e)
		{
			if (this.CanRaiseEvents)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Form.EVENT_RESIZEEND];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.StyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600274D RID: 10061 RVA: 0x000B6EF2 File Offset: 0x000B50F2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnStyleChanged(EventArgs e)
		{
			base.OnStyleChanged(e);
			this.AdjustSystemMenu();
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000B6F04 File Offset: 0x000B5104
		private void UpdateWindowIcon(bool redrawFrame)
		{
			if (base.IsHandleCreated)
			{
				Icon icon;
				if ((this.FormBorderStyle == FormBorderStyle.FixedDialog && this.formState[Form.FormStateIconSet] == 0 && !this.IsRestrictedWindow) || !this.ShowIcon)
				{
					icon = null;
				}
				else
				{
					icon = this.Icon;
				}
				if (icon != null)
				{
					if (this.smallIcon == null)
					{
						try
						{
							this.smallIcon = new Icon(icon, SystemInformation.SmallIconSize);
						}
						catch
						{
						}
					}
					if (this.smallIcon != null)
					{
						base.SendMessage(128, 0, this.smallIcon.Handle);
					}
					base.SendMessage(128, 1, icon.Handle);
				}
				else
				{
					base.SendMessage(128, 0, 0);
					base.SendMessage(128, 1, 0);
				}
				if (redrawFrame)
				{
					SafeNativeMethods.RedrawWindow(new HandleRef(this, base.Handle), null, NativeMethods.NullHandleRef, 1025);
				}
			}
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000B6FF4 File Offset: 0x000B51F4
		private void UpdateWindowState()
		{
			if (base.IsHandleCreated)
			{
				FormWindowState windowState = this.WindowState;
				NativeMethods.WINDOWPLACEMENT windowplacement = default(NativeMethods.WINDOWPLACEMENT);
				windowplacement.length = Marshal.SizeOf(typeof(NativeMethods.WINDOWPLACEMENT));
				UnsafeNativeMethods.GetWindowPlacement(new HandleRef(this, base.Handle), ref windowplacement);
				switch (windowplacement.showCmd)
				{
				case 1:
				case 4:
				case 5:
				case 8:
				case 9:
					if (this.formState[Form.FormStateWindowState] != 0)
					{
						this.formState[Form.FormStateWindowState] = 0;
					}
					break;
				case 2:
				case 6:
				case 7:
					if (this.formState[Form.FormStateMdiChildMax] == 0)
					{
						this.formState[Form.FormStateWindowState] = 1;
					}
					break;
				case 3:
					if (this.formState[Form.FormStateMdiChildMax] == 0)
					{
						this.formState[Form.FormStateWindowState] = 2;
					}
					break;
				}
				if (windowState == FormWindowState.Normal && this.WindowState != FormWindowState.Normal)
				{
					if (this.WindowState == FormWindowState.Minimized)
					{
						this.SuspendLayoutForMinimize();
					}
					this.restoredWindowBounds.Size = this.ClientSize;
					this.formStateEx[Form.FormStateExWindowBoundsWidthIsClientSize] = 1;
					this.formStateEx[Form.FormStateExWindowBoundsHeightIsClientSize] = 1;
					this.restoredWindowBoundsSpecified = BoundsSpecified.Size;
					this.restoredWindowBounds.Location = this.Location;
					this.restoredWindowBoundsSpecified |= BoundsSpecified.Location;
					this.restoreBounds.Size = this.Size;
					this.restoreBounds.Location = this.Location;
				}
				if (windowState == FormWindowState.Minimized && this.WindowState != FormWindowState.Minimized)
				{
					this.ResumeLayoutFromMinimize();
				}
				FormWindowState windowState2 = this.WindowState;
				if (windowState2 != FormWindowState.Normal)
				{
					if (windowState2 - FormWindowState.Minimized <= 1)
					{
						base.SetState(65536, true);
					}
				}
				else
				{
					base.SetState(65536, false);
				}
				if (windowState != this.WindowState)
				{
					this.AdjustSystemMenu();
				}
			}
		}

		/// <summary>Causes all of the child controls within a control that support validation to validate their data.</summary>
		/// <returns>
		///   <see langword="true" /> if all of the children validated successfully; otherwise, <see langword="false" />. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return <see langword="false" />.</returns>
		// Token: 0x06002750 RID: 10064 RVA: 0x000B71D3 File Offset: 0x000B53D3
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override bool ValidateChildren()
		{
			return base.ValidateChildren();
		}

		/// <summary>Causes all of the child controls within a control that support validation to validate their data.</summary>
		/// <param name="validationConstraints">Places restrictions on which controls have their <see cref="E:System.Windows.Forms.Control.Validating" /> event raised.</param>
		/// <returns>
		///   <see langword="true" /> if all of the children validated successfully; otherwise, <see langword="false" />. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return <see langword="false" />.</returns>
		// Token: 0x06002751 RID: 10065 RVA: 0x000B71DB File Offset: 0x000B53DB
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override bool ValidateChildren(ValidationConstraints validationConstraints)
		{
			return base.ValidateChildren(validationConstraints);
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000B71E4 File Offset: 0x000B53E4
		private void WmActivate(ref Message m)
		{
			Application.FormActivated(this.Modal, true);
			this.Active = NativeMethods.Util.LOWORD(m.WParam) != 0;
			Application.FormActivated(this.Modal, this.Active);
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000B7217 File Offset: 0x000B5417
		private void WmEnterSizeMove(ref Message m)
		{
			this.formStateEx[Form.FormStateExInModalSizingLoop] = 1;
			this.OnResizeBegin(EventArgs.Empty);
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000B7235 File Offset: 0x000B5435
		private void WmExitSizeMove(ref Message m)
		{
			this.formStateEx[Form.FormStateExInModalSizingLoop] = 0;
			this.OnResizeEnd(EventArgs.Empty);
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000B7254 File Offset: 0x000B5454
		private void WmCreate(ref Message m)
		{
			base.WndProc(ref m);
			NativeMethods.STARTUPINFO_I startupinfo_I = new NativeMethods.STARTUPINFO_I();
			UnsafeNativeMethods.GetStartupInfo(startupinfo_I);
			if (this.TopLevel && (startupinfo_I.dwFlags & 1) != 0)
			{
				short wShowWindow = startupinfo_I.wShowWindow;
				if (wShowWindow == 3)
				{
					this.WindowState = FormWindowState.Maximized;
					return;
				}
				if (wShowWindow != 6)
				{
					return;
				}
				this.WindowState = FormWindowState.Minimized;
			}
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000B72A8 File Offset: 0x000B54A8
		private void WmClose(ref Message m)
		{
			FormClosingEventArgs formClosingEventArgs = new FormClosingEventArgs(this.CloseReason, false);
			if (m.Msg != 22)
			{
				if (this.Modal)
				{
					if (this.dialogResult == DialogResult.None)
					{
						this.dialogResult = DialogResult.Cancel;
					}
					this.CalledClosing = false;
					formClosingEventArgs.Cancel = !this.CheckCloseDialog(true);
				}
				else
				{
					formClosingEventArgs.Cancel = !base.Validate(true);
					if (this.IsMdiContainer)
					{
						FormClosingEventArgs formClosingEventArgs2 = new FormClosingEventArgs(CloseReason.MdiFormClosing, formClosingEventArgs.Cancel);
						foreach (Form form in this.MdiChildren)
						{
							if (form.IsHandleCreated)
							{
								form.OnClosing(formClosingEventArgs2);
								form.OnFormClosing(formClosingEventArgs2);
								if (formClosingEventArgs2.Cancel)
								{
									formClosingEventArgs.Cancel = true;
									break;
								}
							}
						}
					}
					Form[] ownedForms = this.OwnedForms;
					int integer = base.Properties.GetInteger(Form.PropOwnedFormsCount);
					for (int j = integer - 1; j >= 0; j--)
					{
						FormClosingEventArgs formClosingEventArgs3 = new FormClosingEventArgs(CloseReason.FormOwnerClosing, formClosingEventArgs.Cancel);
						if (ownedForms[j] != null)
						{
							ownedForms[j].OnFormClosing(formClosingEventArgs3);
							if (formClosingEventArgs3.Cancel)
							{
								formClosingEventArgs.Cancel = true;
								break;
							}
						}
					}
					this.OnClosing(formClosingEventArgs);
					this.OnFormClosing(formClosingEventArgs);
				}
				if (m.Msg == 17)
				{
					m.Result = (IntPtr)(formClosingEventArgs.Cancel ? 0 : 1);
				}
				else if (formClosingEventArgs.Cancel && this.MdiParent != null)
				{
					this.CloseReason = CloseReason.None;
				}
				if (this.Modal)
				{
					return;
				}
			}
			else
			{
				formClosingEventArgs.Cancel = m.WParam == IntPtr.Zero;
			}
			if (m.Msg != 17 && !formClosingEventArgs.Cancel)
			{
				this.IsClosing = true;
				FormClosedEventArgs formClosedEventArgs;
				if (this.IsMdiContainer)
				{
					formClosedEventArgs = new FormClosedEventArgs(CloseReason.MdiFormClosing);
					foreach (Form form2 in this.MdiChildren)
					{
						if (form2.IsHandleCreated)
						{
							form2.IsTopMdiWindowClosing = this.IsClosing;
							form2.OnClosed(formClosedEventArgs);
							form2.OnFormClosed(formClosedEventArgs);
						}
					}
				}
				Form[] ownedForms2 = this.OwnedForms;
				int integer2 = base.Properties.GetInteger(Form.PropOwnedFormsCount);
				for (int l = integer2 - 1; l >= 0; l--)
				{
					formClosedEventArgs = new FormClosedEventArgs(CloseReason.FormOwnerClosing);
					if (ownedForms2[l] != null)
					{
						ownedForms2[l].OnClosed(formClosedEventArgs);
						ownedForms2[l].OnFormClosed(formClosedEventArgs);
					}
				}
				formClosedEventArgs = new FormClosedEventArgs(this.CloseReason);
				this.OnClosed(formClosedEventArgs);
				this.OnFormClosed(formClosedEventArgs);
				base.Dispose();
			}
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000B7525 File Offset: 0x000B5725
		private void WmEnterMenuLoop(ref Message m)
		{
			this.OnMenuStart(EventArgs.Empty);
			base.WndProc(ref m);
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000B7539 File Offset: 0x000B5739
		private void WmEraseBkgnd(ref Message m)
		{
			this.UpdateWindowState();
			base.WndProc(ref m);
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000B7548 File Offset: 0x000B5748
		private void WmExitMenuLoop(ref Message m)
		{
			this.OnMenuComplete(EventArgs.Empty);
			base.WndProc(ref m);
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000B755C File Offset: 0x000B575C
		private void WmGetMinMaxInfo(ref Message m)
		{
			Size size = ((this.AutoSize && this.formStateEx[Form.FormStateExInModalSizingLoop] == 1) ? LayoutUtils.UnionSizes(this.minAutoSize, this.MinimumSize) : this.MinimumSize);
			Size maximumSize = this.MaximumSize;
			Rectangle maximizedBounds = this.MaximizedBounds;
			if (!size.IsEmpty || !maximumSize.IsEmpty || !maximizedBounds.IsEmpty || this.IsRestrictedWindow)
			{
				this.WmGetMinMaxInfoHelper(ref m, size, maximumSize, maximizedBounds);
			}
			if (this.IsMdiChild)
			{
				base.WndProc(ref m);
				return;
			}
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000B75EC File Offset: 0x000B57EC
		private void WmGetMinMaxInfoHelper(ref Message m, Size minTrack, Size maxTrack, Rectangle maximizedBounds)
		{
			NativeMethods.MINMAXINFO minmaxinfo = (NativeMethods.MINMAXINFO)m.GetLParam(typeof(NativeMethods.MINMAXINFO));
			if (!minTrack.IsEmpty)
			{
				minmaxinfo.ptMinTrackSize.x = minTrack.Width;
				minmaxinfo.ptMinTrackSize.y = minTrack.Height;
				if (maxTrack.IsEmpty)
				{
					Size size = SystemInformation.VirtualScreen.Size;
					if (minTrack.Height > size.Height)
					{
						minmaxinfo.ptMaxTrackSize.y = int.MaxValue;
					}
					if (minTrack.Width > size.Width)
					{
						minmaxinfo.ptMaxTrackSize.x = int.MaxValue;
					}
				}
			}
			if (!maxTrack.IsEmpty)
			{
				Size minWindowTrackSize = SystemInformation.MinWindowTrackSize;
				minmaxinfo.ptMaxTrackSize.x = Math.Max(maxTrack.Width, minWindowTrackSize.Width);
				minmaxinfo.ptMaxTrackSize.y = Math.Max(maxTrack.Height, minWindowTrackSize.Height);
			}
			if (!maximizedBounds.IsEmpty && !this.IsRestrictedWindow)
			{
				minmaxinfo.ptMaxPosition.x = maximizedBounds.X;
				minmaxinfo.ptMaxPosition.y = maximizedBounds.Y;
				minmaxinfo.ptMaxSize.x = maximizedBounds.Width;
				minmaxinfo.ptMaxSize.y = maximizedBounds.Height;
			}
			if (this.IsRestrictedWindow)
			{
				minmaxinfo.ptMinTrackSize.x = Math.Max(minmaxinfo.ptMinTrackSize.x, 100);
				minmaxinfo.ptMinTrackSize.y = Math.Max(minmaxinfo.ptMinTrackSize.y, SystemInformation.CaptionButtonSize.Height * 3);
			}
			Marshal.StructureToPtr(minmaxinfo, m.LParam, false);
			m.Result = IntPtr.Zero;
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000B77A0 File Offset: 0x000B59A0
		private void WmInitMenuPopup(ref Message m)
		{
			MainMenu mainMenu = (MainMenu)base.Properties.GetObject(Form.PropCurMenu);
			if (mainMenu != null && mainMenu.ProcessInitMenuPopup(m.WParam))
			{
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x000B77DC File Offset: 0x000B59DC
		private void WmMenuChar(ref Message m)
		{
			MainMenu mainMenu = (MainMenu)base.Properties.GetObject(Form.PropCurMenu);
			if (mainMenu == null)
			{
				Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
				if (form != null && form.Menu != null)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(form, form.Handle), 274, new IntPtr(61696), m.WParam);
					m.Result = (IntPtr)NativeMethods.Util.MAKELONG(0, 1);
					return;
				}
			}
			if (mainMenu != null)
			{
				mainMenu.WmMenuChar(ref m);
				if (m.Result != IntPtr.Zero)
				{
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000B7884 File Offset: 0x000B5A84
		private void WmMdiActivate(ref Message m)
		{
			base.WndProc(ref m);
			Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
			if (form != null)
			{
				if (base.Handle == m.WParam)
				{
					form.DeactivateMdiChild();
					return;
				}
				if (base.Handle == m.LParam)
				{
					form.ActivateMdiChildInternal(this);
				}
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000B78E8 File Offset: 0x000B5AE8
		private void WmNcButtonDown(ref Message m)
		{
			if (this.IsMdiChild)
			{
				Form form = (Form)base.Properties.GetObject(Form.PropFormMdiParent);
				if (form.ActiveMdiChildInternal == this && base.ActiveControl != null && !base.ActiveControl.ContainsFocus)
				{
					base.InnerMostActiveContainerControl.FocusActiveControlInternal();
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000B7944 File Offset: 0x000B5B44
		private void WmNCDestroy(ref Message m)
		{
			MainMenu menu = this.Menu;
			MainMenu mainMenu = (MainMenu)base.Properties.GetObject(Form.PropDummyMenu);
			MainMenu mainMenu2 = (MainMenu)base.Properties.GetObject(Form.PropCurMenu);
			MainMenu mainMenu3 = (MainMenu)base.Properties.GetObject(Form.PropMergedMenu);
			if (menu != null)
			{
				menu.ClearHandles();
			}
			if (mainMenu2 != null)
			{
				mainMenu2.ClearHandles();
			}
			if (mainMenu3 != null)
			{
				mainMenu3.ClearHandles();
			}
			if (mainMenu != null)
			{
				mainMenu.ClearHandles();
			}
			base.WndProc(ref m);
			if (this.ownerWindow != null)
			{
				this.ownerWindow.DestroyHandle();
				this.ownerWindow = null;
			}
			if (this.Modal && this.dialogResult == DialogResult.None)
			{
				this.DialogResult = DialogResult.Cancel;
			}
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000B79F8 File Offset: 0x000B5BF8
		private void WmNCHitTest(ref Message m)
		{
			if (this.formState[Form.FormStateRenderSizeGrip] != 0)
			{
				int num = NativeMethods.Util.LOWORD(m.LParam);
				int num2 = NativeMethods.Util.HIWORD(m.LParam);
				NativeMethods.POINT point = new NativeMethods.POINT(num, num2);
				UnsafeNativeMethods.ScreenToClient(new HandleRef(this, base.Handle), point);
				Size clientSize = this.ClientSize;
				if (point.x >= clientSize.Width - 16 && point.y >= clientSize.Height - 16 && clientSize.Height >= 16)
				{
					m.Result = (base.IsMirrored ? ((IntPtr)16) : ((IntPtr)17));
					return;
				}
			}
			base.WndProc(ref m);
			if (this.AutoSizeMode == AutoSizeMode.GrowAndShrink)
			{
				int num3 = (int)(long)m.Result;
				if (num3 >= 10 && num3 <= 17)
				{
					m.Result = (IntPtr)18;
				}
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000B7AD8 File Offset: 0x000B5CD8
		private void WmShowWindow(ref Message m)
		{
			this.formState[Form.FormStateSWCalled] = 1;
			base.WndProc(ref m);
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000B7AF4 File Offset: 0x000B5CF4
		private void WmSysCommand(ref Message m)
		{
			bool flag = true;
			int num = NativeMethods.Util.LOWORD(m.WParam) & 65520;
			if (num <= 61456)
			{
				if (num == 61440 || num == 61456)
				{
					this.formStateEx[Form.FormStateExInModalSizingLoop] = 1;
				}
			}
			else if (num != 61536)
			{
				if (num != 61696)
				{
					if (num == 61824)
					{
						CancelEventArgs cancelEventArgs = new CancelEventArgs(false);
						this.OnHelpButtonClicked(cancelEventArgs);
						if (cancelEventArgs.Cancel)
						{
							flag = false;
						}
					}
				}
				else if (this.IsMdiChild && !this.ControlBox)
				{
					flag = false;
				}
			}
			else
			{
				this.CloseReason = CloseReason.UserClosing;
				if (this.IsMdiChild && !this.ControlBox)
				{
					flag = false;
				}
			}
			if (Command.DispatchID(NativeMethods.Util.LOWORD(m.WParam)))
			{
				flag = false;
			}
			if (flag)
			{
				base.WndProc(ref m);
			}
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x000B7BC4 File Offset: 0x000B5DC4
		private void WmSize(ref Message m)
		{
			if (this.ctlClient == null)
			{
				base.WndProc(ref m);
				if (this.MdiControlStrip == null && this.MdiParentInternal != null && this.MdiParentInternal.ActiveMdiChildInternal == this)
				{
					int num = m.WParam.ToInt32();
					this.MdiParentInternal.UpdateMdiControlStrip(num == 2);
				}
			}
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000B7C1C File Offset: 0x000B5E1C
		private void WmUnInitMenuPopup(ref Message m)
		{
			if (this.Menu != null)
			{
				this.Menu.OnCollapse(EventArgs.Empty);
			}
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000B7C36 File Offset: 0x000B5E36
		private void WmWindowPosChanged(ref Message m)
		{
			this.UpdateWindowState();
			base.WndProc(ref m);
			this.RestoreWindowBoundsIfNecessary();
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06002767 RID: 10087 RVA: 0x000B7C4C File Offset: 0x000B5E4C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 167)
			{
				if (msg <= 36)
				{
					if (msg <= 5)
					{
						if (msg == 1)
						{
							this.WmCreate(ref m);
							return;
						}
						if (msg != 5)
						{
							goto IL_2D0;
						}
						this.WmSize(ref m);
						return;
					}
					else
					{
						if (msg == 6)
						{
							this.WmActivate(ref m);
							return;
						}
						switch (msg)
						{
						case 16:
							if (this.CloseReason == CloseReason.None)
							{
								this.CloseReason = CloseReason.TaskManagerClosing;
							}
							this.WmClose(ref m);
							return;
						case 17:
						case 22:
							this.CloseReason = CloseReason.WindowsShutDown;
							this.WmClose(ref m);
							return;
						case 18:
						case 19:
						case 21:
						case 23:
							goto IL_2D0;
						case 20:
							this.WmEraseBkgnd(ref m);
							return;
						case 24:
							this.WmShowWindow(ref m);
							return;
						default:
							if (msg != 36)
							{
								goto IL_2D0;
							}
							this.WmGetMinMaxInfo(ref m);
							return;
						}
					}
				}
				else if (msg <= 134)
				{
					if (msg == 71)
					{
						this.WmWindowPosChanged(ref m);
						return;
					}
					switch (msg)
					{
					case 130:
						this.WmNCDestroy(ref m);
						return;
					case 131:
					case 133:
						goto IL_2D0;
					case 132:
						this.WmNCHitTest(ref m);
						return;
					case 134:
						if (this.IsRestrictedWindow)
						{
							base.BeginInvoke(new MethodInvoker(this.RestrictedProcessNcActivate));
						}
						base.WndProc(ref m);
						return;
					default:
						goto IL_2D0;
					}
				}
				else if (msg != 161 && msg != 164 && msg != 167)
				{
					goto IL_2D0;
				}
			}
			else if (msg <= 293)
			{
				if (msg <= 274)
				{
					if (msg != 171)
					{
						if (msg != 274)
						{
							goto IL_2D0;
						}
						this.WmSysCommand(ref m);
						return;
					}
				}
				else
				{
					if (msg == 279)
					{
						this.WmInitMenuPopup(ref m);
						return;
					}
					if (msg == 288)
					{
						this.WmMenuChar(ref m);
						return;
					}
					if (msg != 293)
					{
						goto IL_2D0;
					}
					this.WmUnInitMenuPopup(ref m);
					return;
				}
			}
			else if (msg <= 561)
			{
				switch (msg)
				{
				case 529:
					this.WmEnterMenuLoop(ref m);
					return;
				case 530:
					this.WmExitMenuLoop(ref m);
					return;
				case 531:
				case 532:
					goto IL_2D0;
				case 533:
					base.WndProc(ref m);
					if (base.CaptureInternal && Control.MouseButtons == MouseButtons.None)
					{
						base.CaptureInternal = false;
						return;
					}
					return;
				default:
					if (msg == 546)
					{
						this.WmMdiActivate(ref m);
						return;
					}
					if (msg != 561)
					{
						goto IL_2D0;
					}
					this.WmEnterSizeMove(ref m);
					this.DefWndProc(ref m);
					return;
				}
			}
			else
			{
				if (msg == 562)
				{
					this.WmExitSizeMove(ref m);
					this.DefWndProc(ref m);
					return;
				}
				if (msg != 736)
				{
					if (msg != 737)
					{
						goto IL_2D0;
					}
					if (DpiHelper.EnableDpiChangedMessageHandling)
					{
						this.WmGetDpiScaledSize(ref m);
						return;
					}
					m.Result = IntPtr.Zero;
					return;
				}
				else
				{
					if (DpiHelper.EnableDpiChangedMessageHandling)
					{
						this.WmDpiChanged(ref m);
						m.Result = IntPtr.Zero;
						return;
					}
					m.Result = (IntPtr)1;
					return;
				}
			}
			this.WmNcButtonDown(ref m);
			return;
			IL_2D0:
			base.WndProc(ref m);
		}

		// Token: 0x04000FBC RID: 4028
		private static readonly object EVENT_ACTIVATED = new object();

		// Token: 0x04000FBD RID: 4029
		private static readonly object EVENT_CLOSING = new object();

		// Token: 0x04000FBE RID: 4030
		private static readonly object EVENT_CLOSED = new object();

		// Token: 0x04000FBF RID: 4031
		private static readonly object EVENT_FORMCLOSING = new object();

		// Token: 0x04000FC0 RID: 4032
		private static readonly object EVENT_FORMCLOSED = new object();

		// Token: 0x04000FC1 RID: 4033
		private static readonly object EVENT_DEACTIVATE = new object();

		// Token: 0x04000FC2 RID: 4034
		private static readonly object EVENT_LOAD = new object();

		// Token: 0x04000FC3 RID: 4035
		private static readonly object EVENT_MDI_CHILD_ACTIVATE = new object();

		// Token: 0x04000FC4 RID: 4036
		private static readonly object EVENT_INPUTLANGCHANGE = new object();

		// Token: 0x04000FC5 RID: 4037
		private static readonly object EVENT_INPUTLANGCHANGEREQUEST = new object();

		// Token: 0x04000FC6 RID: 4038
		private static readonly object EVENT_MENUSTART = new object();

		// Token: 0x04000FC7 RID: 4039
		private static readonly object EVENT_MENUCOMPLETE = new object();

		// Token: 0x04000FC8 RID: 4040
		private static readonly object EVENT_MAXIMUMSIZECHANGED = new object();

		// Token: 0x04000FC9 RID: 4041
		private static readonly object EVENT_MINIMUMSIZECHANGED = new object();

		// Token: 0x04000FCA RID: 4042
		private static readonly object EVENT_HELPBUTTONCLICKED = new object();

		// Token: 0x04000FCB RID: 4043
		private static readonly object EVENT_SHOWN = new object();

		// Token: 0x04000FCC RID: 4044
		private static readonly object EVENT_RESIZEBEGIN = new object();

		// Token: 0x04000FCD RID: 4045
		private static readonly object EVENT_RESIZEEND = new object();

		// Token: 0x04000FCE RID: 4046
		private static readonly object EVENT_RIGHTTOLEFTLAYOUTCHANGED = new object();

		// Token: 0x04000FCF RID: 4047
		private static readonly object EVENT_DPI_CHANGED = new object();

		// Token: 0x04000FD0 RID: 4048
		private static readonly BitVector32.Section FormStateAllowTransparency = BitVector32.CreateSection(1);

		// Token: 0x04000FD1 RID: 4049
		private static readonly BitVector32.Section FormStateBorderStyle = BitVector32.CreateSection(6, Form.FormStateAllowTransparency);

		// Token: 0x04000FD2 RID: 4050
		private static readonly BitVector32.Section FormStateTaskBar = BitVector32.CreateSection(1, Form.FormStateBorderStyle);

		// Token: 0x04000FD3 RID: 4051
		private static readonly BitVector32.Section FormStateControlBox = BitVector32.CreateSection(1, Form.FormStateTaskBar);

		// Token: 0x04000FD4 RID: 4052
		private static readonly BitVector32.Section FormStateKeyPreview = BitVector32.CreateSection(1, Form.FormStateControlBox);

		// Token: 0x04000FD5 RID: 4053
		private static readonly BitVector32.Section FormStateLayered = BitVector32.CreateSection(1, Form.FormStateKeyPreview);

		// Token: 0x04000FD6 RID: 4054
		private static readonly BitVector32.Section FormStateMaximizeBox = BitVector32.CreateSection(1, Form.FormStateLayered);

		// Token: 0x04000FD7 RID: 4055
		private static readonly BitVector32.Section FormStateMinimizeBox = BitVector32.CreateSection(1, Form.FormStateMaximizeBox);

		// Token: 0x04000FD8 RID: 4056
		private static readonly BitVector32.Section FormStateHelpButton = BitVector32.CreateSection(1, Form.FormStateMinimizeBox);

		// Token: 0x04000FD9 RID: 4057
		private static readonly BitVector32.Section FormStateStartPos = BitVector32.CreateSection(4, Form.FormStateHelpButton);

		// Token: 0x04000FDA RID: 4058
		private static readonly BitVector32.Section FormStateWindowState = BitVector32.CreateSection(2, Form.FormStateStartPos);

		// Token: 0x04000FDB RID: 4059
		private static readonly BitVector32.Section FormStateShowWindowOnCreate = BitVector32.CreateSection(1, Form.FormStateWindowState);

		// Token: 0x04000FDC RID: 4060
		private static readonly BitVector32.Section FormStateAutoScaling = BitVector32.CreateSection(1, Form.FormStateShowWindowOnCreate);

		// Token: 0x04000FDD RID: 4061
		private static readonly BitVector32.Section FormStateSetClientSize = BitVector32.CreateSection(1, Form.FormStateAutoScaling);

		// Token: 0x04000FDE RID: 4062
		private static readonly BitVector32.Section FormStateTopMost = BitVector32.CreateSection(1, Form.FormStateSetClientSize);

		// Token: 0x04000FDF RID: 4063
		private static readonly BitVector32.Section FormStateSWCalled = BitVector32.CreateSection(1, Form.FormStateTopMost);

		// Token: 0x04000FE0 RID: 4064
		private static readonly BitVector32.Section FormStateMdiChildMax = BitVector32.CreateSection(1, Form.FormStateSWCalled);

		// Token: 0x04000FE1 RID: 4065
		private static readonly BitVector32.Section FormStateRenderSizeGrip = BitVector32.CreateSection(1, Form.FormStateMdiChildMax);

		// Token: 0x04000FE2 RID: 4066
		private static readonly BitVector32.Section FormStateSizeGripStyle = BitVector32.CreateSection(2, Form.FormStateRenderSizeGrip);

		// Token: 0x04000FE3 RID: 4067
		private static readonly BitVector32.Section FormStateIsRestrictedWindow = BitVector32.CreateSection(1, Form.FormStateSizeGripStyle);

		// Token: 0x04000FE4 RID: 4068
		private static readonly BitVector32.Section FormStateIsRestrictedWindowChecked = BitVector32.CreateSection(1, Form.FormStateIsRestrictedWindow);

		// Token: 0x04000FE5 RID: 4069
		private static readonly BitVector32.Section FormStateIsWindowActivated = BitVector32.CreateSection(1, Form.FormStateIsRestrictedWindowChecked);

		// Token: 0x04000FE6 RID: 4070
		private static readonly BitVector32.Section FormStateIsTextEmpty = BitVector32.CreateSection(1, Form.FormStateIsWindowActivated);

		// Token: 0x04000FE7 RID: 4071
		private static readonly BitVector32.Section FormStateIsActive = BitVector32.CreateSection(1, Form.FormStateIsTextEmpty);

		// Token: 0x04000FE8 RID: 4072
		private static readonly BitVector32.Section FormStateIconSet = BitVector32.CreateSection(1, Form.FormStateIsActive);

		// Token: 0x04000FE9 RID: 4073
		private static readonly BitVector32.Section FormStateExCalledClosing = BitVector32.CreateSection(1);

		// Token: 0x04000FEA RID: 4074
		private static readonly BitVector32.Section FormStateExUpdateMenuHandlesSuspendCount = BitVector32.CreateSection(8, Form.FormStateExCalledClosing);

		// Token: 0x04000FEB RID: 4075
		private static readonly BitVector32.Section FormStateExUpdateMenuHandlesDeferred = BitVector32.CreateSection(1, Form.FormStateExUpdateMenuHandlesSuspendCount);

		// Token: 0x04000FEC RID: 4076
		private static readonly BitVector32.Section FormStateExUseMdiChildProc = BitVector32.CreateSection(1, Form.FormStateExUpdateMenuHandlesDeferred);

		// Token: 0x04000FED RID: 4077
		private static readonly BitVector32.Section FormStateExCalledOnLoad = BitVector32.CreateSection(1, Form.FormStateExUseMdiChildProc);

		// Token: 0x04000FEE RID: 4078
		private static readonly BitVector32.Section FormStateExCalledMakeVisible = BitVector32.CreateSection(1, Form.FormStateExCalledOnLoad);

		// Token: 0x04000FEF RID: 4079
		private static readonly BitVector32.Section FormStateExCalledCreateControl = BitVector32.CreateSection(1, Form.FormStateExCalledMakeVisible);

		// Token: 0x04000FF0 RID: 4080
		private static readonly BitVector32.Section FormStateExAutoSize = BitVector32.CreateSection(1, Form.FormStateExCalledCreateControl);

		// Token: 0x04000FF1 RID: 4081
		private static readonly BitVector32.Section FormStateExInUpdateMdiControlStrip = BitVector32.CreateSection(1, Form.FormStateExAutoSize);

		// Token: 0x04000FF2 RID: 4082
		private static readonly BitVector32.Section FormStateExShowIcon = BitVector32.CreateSection(1, Form.FormStateExInUpdateMdiControlStrip);

		// Token: 0x04000FF3 RID: 4083
		private static readonly BitVector32.Section FormStateExMnemonicProcessed = BitVector32.CreateSection(1, Form.FormStateExShowIcon);

		// Token: 0x04000FF4 RID: 4084
		private static readonly BitVector32.Section FormStateExInScale = BitVector32.CreateSection(1, Form.FormStateExMnemonicProcessed);

		// Token: 0x04000FF5 RID: 4085
		private static readonly BitVector32.Section FormStateExInModalSizingLoop = BitVector32.CreateSection(1, Form.FormStateExInScale);

		// Token: 0x04000FF6 RID: 4086
		private static readonly BitVector32.Section FormStateExSettingAutoScale = BitVector32.CreateSection(1, Form.FormStateExInModalSizingLoop);

		// Token: 0x04000FF7 RID: 4087
		private static readonly BitVector32.Section FormStateExWindowBoundsWidthIsClientSize = BitVector32.CreateSection(1, Form.FormStateExSettingAutoScale);

		// Token: 0x04000FF8 RID: 4088
		private static readonly BitVector32.Section FormStateExWindowBoundsHeightIsClientSize = BitVector32.CreateSection(1, Form.FormStateExWindowBoundsWidthIsClientSize);

		// Token: 0x04000FF9 RID: 4089
		private static readonly BitVector32.Section FormStateExWindowClosing = BitVector32.CreateSection(1, Form.FormStateExWindowBoundsHeightIsClientSize);

		// Token: 0x04000FFA RID: 4090
		private const int SizeGripSize = 16;

		// Token: 0x04000FFB RID: 4091
		private static Icon defaultIcon = null;

		// Token: 0x04000FFC RID: 4092
		private static Icon defaultRestrictedIcon = null;

		// Token: 0x04000FFD RID: 4093
		private static object internalSyncObject = new object();

		// Token: 0x04000FFE RID: 4094
		private static readonly int PropAcceptButton = PropertyStore.CreateKey();

		// Token: 0x04000FFF RID: 4095
		private static readonly int PropCancelButton = PropertyStore.CreateKey();

		// Token: 0x04001000 RID: 4096
		private static readonly int PropDefaultButton = PropertyStore.CreateKey();

		// Token: 0x04001001 RID: 4097
		private static readonly int PropDialogOwner = PropertyStore.CreateKey();

		// Token: 0x04001002 RID: 4098
		private static readonly int PropMainMenu = PropertyStore.CreateKey();

		// Token: 0x04001003 RID: 4099
		private static readonly int PropDummyMenu = PropertyStore.CreateKey();

		// Token: 0x04001004 RID: 4100
		private static readonly int PropCurMenu = PropertyStore.CreateKey();

		// Token: 0x04001005 RID: 4101
		private static readonly int PropMergedMenu = PropertyStore.CreateKey();

		// Token: 0x04001006 RID: 4102
		private static readonly int PropOwner = PropertyStore.CreateKey();

		// Token: 0x04001007 RID: 4103
		private static readonly int PropOwnedForms = PropertyStore.CreateKey();

		// Token: 0x04001008 RID: 4104
		private static readonly int PropMaximizedBounds = PropertyStore.CreateKey();

		// Token: 0x04001009 RID: 4105
		private static readonly int PropOwnedFormsCount = PropertyStore.CreateKey();

		// Token: 0x0400100A RID: 4106
		private static readonly int PropMinTrackSizeWidth = PropertyStore.CreateKey();

		// Token: 0x0400100B RID: 4107
		private static readonly int PropMinTrackSizeHeight = PropertyStore.CreateKey();

		// Token: 0x0400100C RID: 4108
		private static readonly int PropMaxTrackSizeWidth = PropertyStore.CreateKey();

		// Token: 0x0400100D RID: 4109
		private static readonly int PropMaxTrackSizeHeight = PropertyStore.CreateKey();

		// Token: 0x0400100E RID: 4110
		private static readonly int PropFormMdiParent = PropertyStore.CreateKey();

		// Token: 0x0400100F RID: 4111
		private static readonly int PropActiveMdiChild = PropertyStore.CreateKey();

		// Token: 0x04001010 RID: 4112
		private static readonly int PropFormerlyActiveMdiChild = PropertyStore.CreateKey();

		// Token: 0x04001011 RID: 4113
		private static readonly int PropMdiChildFocusable = PropertyStore.CreateKey();

		// Token: 0x04001012 RID: 4114
		private static readonly int PropMainMenuStrip = PropertyStore.CreateKey();

		// Token: 0x04001013 RID: 4115
		private static readonly int PropMdiWindowListStrip = PropertyStore.CreateKey();

		// Token: 0x04001014 RID: 4116
		private static readonly int PropMdiControlStrip = PropertyStore.CreateKey();

		// Token: 0x04001015 RID: 4117
		private static readonly int PropSecurityTip = PropertyStore.CreateKey();

		// Token: 0x04001016 RID: 4118
		private static readonly int PropOpacity = PropertyStore.CreateKey();

		// Token: 0x04001017 RID: 4119
		private static readonly int PropTransparencyKey = PropertyStore.CreateKey();

		// Token: 0x04001018 RID: 4120
		private BitVector32 formState = new BitVector32(135992);

		// Token: 0x04001019 RID: 4121
		private BitVector32 formStateEx;

		// Token: 0x0400101A RID: 4122
		private Icon icon;

		// Token: 0x0400101B RID: 4123
		private Icon smallIcon;

		// Token: 0x0400101C RID: 4124
		private Size autoScaleBaseSize = Size.Empty;

		// Token: 0x0400101D RID: 4125
		private Size minAutoSize = Size.Empty;

		// Token: 0x0400101E RID: 4126
		private Rectangle restoredWindowBounds = new Rectangle(-1, -1, -1, -1);

		// Token: 0x0400101F RID: 4127
		private BoundsSpecified restoredWindowBoundsSpecified;

		// Token: 0x04001020 RID: 4128
		private DialogResult dialogResult;

		// Token: 0x04001021 RID: 4129
		private MdiClient ctlClient;

		// Token: 0x04001022 RID: 4130
		private NativeWindow ownerWindow;

		// Token: 0x04001023 RID: 4131
		private string userWindowText;

		// Token: 0x04001024 RID: 4132
		private string securityZone;

		// Token: 0x04001025 RID: 4133
		private string securitySite;

		// Token: 0x04001026 RID: 4134
		private bool rightToLeftLayout;

		// Token: 0x04001027 RID: 4135
		private Rectangle restoreBounds = new Rectangle(-1, -1, -1, -1);

		// Token: 0x04001028 RID: 4136
		private CloseReason closeReason;

		// Token: 0x04001029 RID: 4137
		private VisualStyleRenderer sizeGripRenderer;

		// Token: 0x0400102A RID: 4138
		private static readonly object EVENT_MAXIMIZEDBOUNDSCHANGED = new object();

		/// <summary>Represents a collection of controls on the form.</summary>
		// Token: 0x0200069F RID: 1695
		[ComVisible(false)]
		public new class ControlCollection : Control.ControlCollection
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Form.ControlCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.Form" /> to contain the controls added to the control collection.</param>
			// Token: 0x060067B5 RID: 26549 RVA: 0x001827EF File Offset: 0x001809EF
			public ControlCollection(Form owner)
				: base(owner)
			{
				this.owner = owner;
			}

			/// <summary>Adds a control to the form.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to add to the form.</param>
			/// <exception cref="T:System.Exception">A multiple document interface (MDI) parent form cannot have controls added to it.</exception>
			// Token: 0x060067B6 RID: 26550 RVA: 0x00182800 File Offset: 0x00180A00
			public override void Add(Control value)
			{
				if (value is MdiClient && this.owner.ctlClient == null)
				{
					if (!this.owner.TopLevel && !this.owner.DesignMode)
					{
						throw new ArgumentException(SR.GetString("MDIContainerMustBeTopLevel"), "value");
					}
					this.owner.AutoScroll = false;
					if (this.owner.IsMdiChild)
					{
						throw new ArgumentException(SR.GetString("FormMDIParentAndChild"), "value");
					}
					this.owner.ctlClient = (MdiClient)value;
				}
				if (value is Form && ((Form)value).MdiParentInternal != null)
				{
					throw new ArgumentException(SR.GetString("FormMDIParentCannotAdd"), "value");
				}
				base.Add(value);
				if (this.owner.ctlClient != null)
				{
					this.owner.ctlClient.SendToBack();
				}
			}

			/// <summary>Removes a control from the form.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.Control" /> to remove from the form.</param>
			// Token: 0x060067B7 RID: 26551 RVA: 0x001828DE File Offset: 0x00180ADE
			public override void Remove(Control value)
			{
				if (value == this.owner.ctlClient)
				{
					this.owner.ctlClient = null;
				}
				base.Remove(value);
			}

			// Token: 0x04003AD3 RID: 15059
			private Form owner;
		}

		// Token: 0x020006A0 RID: 1696
		private class EnumThreadWindowsCallback
		{
			// Token: 0x060067B8 RID: 26552 RVA: 0x00002843 File Offset: 0x00000A43
			internal EnumThreadWindowsCallback()
			{
			}

			// Token: 0x060067B9 RID: 26553 RVA: 0x00182904 File Offset: 0x00180B04
			internal bool Callback(IntPtr hWnd, IntPtr lParam)
			{
				HandleRef handleRef = new HandleRef(null, hWnd);
				IntPtr windowLong = UnsafeNativeMethods.GetWindowLong(handleRef, -8);
				if (windowLong == lParam)
				{
					if (this.ownedWindows == null)
					{
						this.ownedWindows = new List<HandleRef>();
					}
					this.ownedWindows.Add(handleRef);
				}
				return true;
			}

			// Token: 0x060067BA RID: 26554 RVA: 0x0018294C File Offset: 0x00180B4C
			internal void ResetOwners()
			{
				if (this.ownedWindows != null)
				{
					foreach (HandleRef handleRef in this.ownedWindows)
					{
						UnsafeNativeMethods.SetWindowLong(handleRef, -8, NativeMethods.NullHandleRef);
					}
				}
			}

			// Token: 0x060067BB RID: 26555 RVA: 0x001829B0 File Offset: 0x00180BB0
			internal void SetOwners(HandleRef hRefOwner)
			{
				if (this.ownedWindows != null)
				{
					foreach (HandleRef handleRef in this.ownedWindows)
					{
						UnsafeNativeMethods.SetWindowLong(handleRef, -8, hRefOwner);
					}
				}
			}

			// Token: 0x04003AD4 RID: 15060
			private List<HandleRef> ownedWindows;
		}

		// Token: 0x020006A1 RID: 1697
		private class SecurityToolTip : IDisposable
		{
			// Token: 0x060067BC RID: 26556 RVA: 0x00182A10 File Offset: 0x00180C10
			internal SecurityToolTip(Form owner)
			{
				this.owner = owner;
				this.SetupText();
				this.window = new Form.SecurityToolTip.ToolTipNativeWindow(this);
				this.SetupToolTip();
				owner.LocationChanged += this.FormLocationChanged;
				owner.HandleCreated += this.FormHandleCreated;
			}

			// Token: 0x17001687 RID: 5767
			// (get) Token: 0x060067BD RID: 26557 RVA: 0x00182A70 File Offset: 0x00180C70
			private CreateParams CreateParams
			{
				get
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 8
					});
					CreateParams createParams = new CreateParams();
					createParams.Parent = this.owner.Handle;
					createParams.ClassName = "tooltips_class32";
					createParams.Style |= 65;
					createParams.ExStyle = 0;
					createParams.Caption = null;
					return createParams;
				}
			}

			// Token: 0x17001688 RID: 5768
			// (get) Token: 0x060067BE RID: 26558 RVA: 0x00182AD1 File Offset: 0x00180CD1
			internal bool Modal
			{
				get
				{
					return this.first;
				}
			}

			// Token: 0x060067BF RID: 26559 RVA: 0x00182ADC File Offset: 0x00180CDC
			public void Dispose()
			{
				if (this.owner != null)
				{
					this.owner.LocationChanged -= this.FormLocationChanged;
				}
				if (this.window.Handle != IntPtr.Zero)
				{
					this.window.DestroyHandle();
					this.window = null;
				}
			}

			// Token: 0x060067C0 RID: 26560 RVA: 0x00182B34 File Offset: 0x00180D34
			private NativeMethods.TOOLINFO_T GetTOOLINFO()
			{
				NativeMethods.TOOLINFO_T toolinfo_T = new NativeMethods.TOOLINFO_T();
				toolinfo_T.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_T));
				toolinfo_T.uFlags |= 16;
				toolinfo_T.lpszText = this.toolTipText;
				if (this.owner.RightToLeft == RightToLeft.Yes)
				{
					toolinfo_T.uFlags |= 4;
				}
				if (!this.first)
				{
					toolinfo_T.uFlags |= 256;
					toolinfo_T.hwnd = this.owner.Handle;
					Size captionButtonSize = SystemInformation.CaptionButtonSize;
					Rectangle rectangle = new Rectangle(this.owner.Left, this.owner.Top, captionButtonSize.Width, SystemInformation.CaptionHeight);
					rectangle = this.owner.RectangleToClient(rectangle);
					rectangle.Width -= rectangle.X;
					rectangle.Y++;
					toolinfo_T.rect = NativeMethods.RECT.FromXYWH(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
					toolinfo_T.uId = IntPtr.Zero;
				}
				else
				{
					toolinfo_T.uFlags |= 33;
					toolinfo_T.hwnd = IntPtr.Zero;
					toolinfo_T.uId = this.owner.Handle;
				}
				return toolinfo_T;
			}

			// Token: 0x060067C1 RID: 26561 RVA: 0x00182C84 File Offset: 0x00180E84
			private void SetupText()
			{
				this.owner.EnsureSecurityInformation();
				string @string = SR.GetString("SecurityToolTipMainText");
				string string2 = SR.GetString("SecurityToolTipSourceInformation", new object[] { this.owner.securitySite });
				this.toolTipText = SR.GetString("SecurityToolTipTextFormat", new object[] { @string, string2 });
			}

			// Token: 0x060067C2 RID: 26562 RVA: 0x00182CE4 File Offset: 0x00180EE4
			private void SetupToolTip()
			{
				this.window.CreateHandle(this.CreateParams);
				SafeNativeMethods.SetWindowPos(new HandleRef(this.window, this.window.Handle), NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, 19);
				UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), 1048, 0, this.owner.Width);
				UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), NativeMethods.TTM_SETTITLE, 2, SR.GetString("SecurityToolTipCaption"));
				(int)UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), NativeMethods.TTM_ADDTOOL, 0, this.GetTOOLINFO());
				UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), 1025, 1, 0);
				this.Show();
			}

			// Token: 0x060067C3 RID: 26563 RVA: 0x00182DD8 File Offset: 0x00180FD8
			private void RecreateHandle()
			{
				if (this.window != null)
				{
					if (this.window.Handle != IntPtr.Zero)
					{
						this.window.DestroyHandle();
					}
					this.SetupToolTip();
				}
			}

			// Token: 0x060067C4 RID: 26564 RVA: 0x00182E0A File Offset: 0x0018100A
			private void FormHandleCreated(object sender, EventArgs e)
			{
				this.RecreateHandle();
			}

			// Token: 0x060067C5 RID: 26565 RVA: 0x00182E14 File Offset: 0x00181014
			private void FormLocationChanged(object sender, EventArgs e)
			{
				if (this.window == null || !this.first)
				{
					this.Pop(true);
					return;
				}
				Size captionButtonSize = SystemInformation.CaptionButtonSize;
				if (this.owner.WindowState == FormWindowState.Minimized)
				{
					this.Pop(true);
					return;
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), 1042, 0, NativeMethods.Util.MAKELONG(this.owner.Left + captionButtonSize.Width / 2, this.owner.Top + SystemInformation.CaptionHeight));
			}

			// Token: 0x060067C6 RID: 26566 RVA: 0x00182EA4 File Offset: 0x001810A4
			internal void Pop(bool noLongerFirst)
			{
				if (noLongerFirst)
				{
					this.first = false;
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), 1041, 0, this.GetTOOLINFO());
				UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), NativeMethods.TTM_DELTOOL, 0, this.GetTOOLINFO());
				UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), NativeMethods.TTM_ADDTOOL, 0, this.GetTOOLINFO());
			}

			// Token: 0x060067C7 RID: 26567 RVA: 0x00182F34 File Offset: 0x00181134
			internal void Show()
			{
				if (this.first)
				{
					Size captionButtonSize = SystemInformation.CaptionButtonSize;
					UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), 1042, 0, NativeMethods.Util.MAKELONG(this.owner.Left + captionButtonSize.Width / 2, this.owner.Top + SystemInformation.CaptionHeight));
					UnsafeNativeMethods.SendMessage(new HandleRef(this.window, this.window.Handle), 1041, 1, this.GetTOOLINFO());
				}
			}

			// Token: 0x060067C8 RID: 26568 RVA: 0x00182FC4 File Offset: 0x001811C4
			private void WndProc(ref Message msg)
			{
				if (this.first && (msg.Msg == 513 || msg.Msg == 516 || msg.Msg == 519 || msg.Msg == 523))
				{
					this.Pop(true);
				}
				this.window.DefWndProc(ref msg);
			}

			// Token: 0x04003AD5 RID: 15061
			private Form owner;

			// Token: 0x04003AD6 RID: 15062
			private string toolTipText;

			// Token: 0x04003AD7 RID: 15063
			private bool first = true;

			// Token: 0x04003AD8 RID: 15064
			private Form.SecurityToolTip.ToolTipNativeWindow window;

			// Token: 0x020008BB RID: 2235
			private sealed class ToolTipNativeWindow : NativeWindow
			{
				// Token: 0x0600729E RID: 29342 RVA: 0x001A2D1E File Offset: 0x001A0F1E
				internal ToolTipNativeWindow(Form.SecurityToolTip control)
				{
					this.control = control;
				}

				// Token: 0x0600729F RID: 29343 RVA: 0x001A2D2D File Offset: 0x001A0F2D
				protected override void WndProc(ref Message m)
				{
					if (this.control != null)
					{
						this.control.WndProc(ref m);
					}
				}

				// Token: 0x04004530 RID: 17712
				private Form.SecurityToolTip control;
			}
		}
	}
}
