using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows progress bar control contained in a <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
	// Token: 0x020003F6 RID: 1014
	[DefaultProperty("Value")]
	public class ToolStripProgressBar : ToolStripControlHost
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> class.</summary>
		// Token: 0x060045C2 RID: 17858 RVA: 0x00126A64 File Offset: 0x00124C64
		public ToolStripProgressBar()
			: base(ToolStripProgressBar.CreateControlInstance())
		{
			ToolStripProgressBar.ToolStripProgressBarControl toolStripProgressBarControl = base.Control as ToolStripProgressBar.ToolStripProgressBarControl;
			if (toolStripProgressBarControl != null)
			{
				toolStripProgressBarControl.Owner = this;
			}
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledDefaultMargin = DpiHelper.LogicalToDeviceUnits(ToolStripProgressBar.defaultMargin, 0);
				this.scaledDefaultStatusStripMargin = DpiHelper.LogicalToDeviceUnits(ToolStripProgressBar.defaultStatusStripMargin, 0);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> class with specified name.</summary>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</param>
		// Token: 0x060045C3 RID: 17859 RVA: 0x00126AD1 File Offset: 0x00124CD1
		public ToolStripProgressBar(string name)
			: this()
		{
			base.Name = name;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ProgressBar" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ProgressBar" /> object associated with the control.</returns>
		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x060045C4 RID: 17860 RVA: 0x00126AE0 File Offset: 0x00124CE0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ProgressBar ProgressBar
		{
			get
			{
				return base.Control as ProgressBar;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The background image displayed in the control.</returns>
		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x060045C5 RID: 17861 RVA: 0x00010F8F File Offset: 0x0000F18F
		// (set) Token: 0x060045C6 RID: 17862 RVA: 0x00010F97 File Offset: 0x0000F197
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x060045C7 RID: 17863 RVA: 0x00010FA0 File Offset: 0x0000F1A0
		// (set) Token: 0x060045C8 RID: 17864 RVA: 0x00010FA8 File Offset: 0x0000F1A8
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

		/// <summary>Gets the height and width of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> in pixels.</summary>
		/// <returns>A <see cref="M:System.Drawing.Point.#ctor(System.Drawing.Size)" /> value representing the height and width.</returns>
		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x060045C9 RID: 17865 RVA: 0x00126AED File Offset: 0x00124CED
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 15);
			}
		}

		/// <summary>Gets the spacing between the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> and adjacent items.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x060045CA RID: 17866 RVA: 0x00126AF8 File Offset: 0x00124CF8
		protected internal override Padding DefaultMargin
		{
			get
			{
				if (base.Owner != null && base.Owner is StatusStrip)
				{
					return this.scaledDefaultStatusStripMargin;
				}
				return this.scaledDefaultMargin;
			}
		}

		/// <summary>Gets or sets a value representing the delay between each <see cref="F:System.Windows.Forms.ProgressBarStyle.Marquee" /> display update, in milliseconds.</summary>
		/// <returns>An integer representing the delay, in milliseconds.</returns>
		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x060045CB RID: 17867 RVA: 0x00126B1C File Offset: 0x00124D1C
		// (set) Token: 0x060045CC RID: 17868 RVA: 0x00126B29 File Offset: 0x00124D29
		[DefaultValue(100)]
		[SRCategory("CatBehavior")]
		[SRDescription("ProgressBarMarqueeAnimationSpeed")]
		public int MarqueeAnimationSpeed
		{
			get
			{
				return this.ProgressBar.MarqueeAnimationSpeed;
			}
			set
			{
				this.ProgressBar.MarqueeAnimationSpeed = value;
			}
		}

		/// <summary>Gets or sets the upper bound of the range that is defined for this <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
		/// <returns>An integer representing the upper bound of the range. The default is 100.</returns>
		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x060045CD RID: 17869 RVA: 0x00126B37 File Offset: 0x00124D37
		// (set) Token: 0x060045CE RID: 17870 RVA: 0x00126B44 File Offset: 0x00124D44
		[DefaultValue(100)]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ProgressBarMaximumDescr")]
		public int Maximum
		{
			get
			{
				return this.ProgressBar.Maximum;
			}
			set
			{
				this.ProgressBar.Maximum = value;
			}
		}

		/// <summary>Gets or sets the lower bound of the range that is defined for this <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
		/// <returns>An integer representing the lower bound of the range. The default is 0.</returns>
		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x060045CF RID: 17871 RVA: 0x00126B52 File Offset: 0x00124D52
		// (set) Token: 0x060045D0 RID: 17872 RVA: 0x00126B5F File Offset: 0x00124D5F
		[DefaultValue(0)]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ProgressBarMinimumDescr")]
		public int Minimum
		{
			get
			{
				return this.ProgressBar.Minimum;
			}
			set
			{
				this.ProgressBar.Minimum = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> layout is right-to-left or left-to-right when the <see cref="T:System.Windows.Forms.RightToLeft" /> property is set to <see cref="F:System.Windows.Forms.RightToLeft.Yes" />.</summary>
		/// <returns>
		///   <see langword="true" /> to turn on mirroring and lay out control from right to left when the <see cref="T:System.Windows.Forms.RightToLeft" /> property is set to <see cref="F:System.Windows.Forms.RightToLeft.Yes" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x060045D1 RID: 17873 RVA: 0x00126B6D File Offset: 0x00124D6D
		// (set) Token: 0x060045D2 RID: 17874 RVA: 0x00126B7A File Offset: 0x00124D7A
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("ControlRightToLeftLayoutDescr")]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.ProgressBar.RightToLeftLayout;
			}
			set
			{
				this.ProgressBar.RightToLeftLayout = value;
			}
		}

		/// <summary>Gets or sets the amount by which to increment the current value of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> when the <see cref="M:System.Windows.Forms.ToolStripProgressBar.PerformStep" /> method is called.</summary>
		/// <returns>An integer representing the incremental amount. The default value is 10.</returns>
		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x060045D3 RID: 17875 RVA: 0x00126B88 File Offset: 0x00124D88
		// (set) Token: 0x060045D4 RID: 17876 RVA: 0x00126B95 File Offset: 0x00124D95
		[DefaultValue(10)]
		[SRCategory("CatBehavior")]
		[SRDescription("ProgressBarStepDescr")]
		public int Step
		{
			get
			{
				return this.ProgressBar.Step;
			}
			set
			{
				this.ProgressBar.Step = value;
			}
		}

		/// <summary>Gets or sets the style of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ProgressBarStyle" /> values. The default value is <see cref="F:System.Windows.Forms.ProgressBarStyle.Blocks" />.</returns>
		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x060045D5 RID: 17877 RVA: 0x00126BA3 File Offset: 0x00124DA3
		// (set) Token: 0x060045D6 RID: 17878 RVA: 0x00126BB0 File Offset: 0x00124DB0
		[DefaultValue(ProgressBarStyle.Blocks)]
		[SRCategory("CatBehavior")]
		[SRDescription("ProgressBarStyleDescr")]
		public ProgressBarStyle Style
		{
			get
			{
				return this.ProgressBar.Style;
			}
			set
			{
				this.ProgressBar.Style = value;
			}
		}

		/// <summary>Gets or sets the text displayed on the <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the display text.</returns>
		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x060045D7 RID: 17879 RVA: 0x00111CD9 File Offset: 0x0010FED9
		// (set) Token: 0x060045D8 RID: 17880 RVA: 0x00111CE6 File Offset: 0x0010FEE6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return base.Control.Text;
			}
			set
			{
				base.Control.Text = value;
			}
		}

		/// <summary>Gets or sets the current value of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
		/// <returns>An integer representing the current value.</returns>
		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x060045D9 RID: 17881 RVA: 0x00126BBE File Offset: 0x00124DBE
		// (set) Token: 0x060045DA RID: 17882 RVA: 0x00126BCB File Offset: 0x00124DCB
		[DefaultValue(0)]
		[SRCategory("CatBehavior")]
		[Bindable(true)]
		[SRDescription("ProgressBarValueDescr")]
		public int Value
		{
			get
			{
				return this.ProgressBar.Value;
			}
			set
			{
				this.ProgressBar.Value = value;
			}
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new accessibility object for the control.</returns>
		// Token: 0x060045DB RID: 17883 RVA: 0x00126BD9 File Offset: 0x00124DD9
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ToolStripProgressBar.ToolStripProgressBarAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x00126BF0 File Offset: 0x00124DF0
		private static Control CreateControlInstance()
		{
			ProgressBar progressBar = (AccessibilityImprovements.Level3 ? new ToolStripProgressBar.ToolStripProgressBarControl() : new ProgressBar());
			progressBar.Size = new Size(100, 15);
			return progressBar;
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x00126C21 File Offset: 0x00124E21
		private void HandleRightToLeftLayoutChanged(object sender, EventArgs e)
		{
			this.OnRightToLeftLayoutChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ProgressBar.RightToLeftLayoutChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060045DE RID: 17886 RVA: 0x00126C2A File Offset: 0x00124E2A
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripProgressBar.EventRightToLeftLayoutChanged, e);
		}

		/// <summary>Subscribes events from the hosted control.</summary>
		/// <param name="control">The control from which to subscribe events.</param>
		// Token: 0x060045DF RID: 17887 RVA: 0x00126C38 File Offset: 0x00124E38
		protected override void OnSubscribeControlEvents(Control control)
		{
			ProgressBar progressBar = control as ProgressBar;
			if (progressBar != null)
			{
				progressBar.RightToLeftLayoutChanged += this.HandleRightToLeftLayoutChanged;
			}
			base.OnSubscribeControlEvents(control);
		}

		/// <summary>Unsubscribes events from the hosted control.</summary>
		/// <param name="control">The control from which to unsubscribe events.</param>
		// Token: 0x060045E0 RID: 17888 RVA: 0x00126C68 File Offset: 0x00124E68
		protected override void OnUnsubscribeControlEvents(Control control)
		{
			ProgressBar progressBar = control as ProgressBar;
			if (progressBar != null)
			{
				progressBar.RightToLeftLayoutChanged -= this.HandleRightToLeftLayoutChanged;
			}
			base.OnUnsubscribeControlEvents(control);
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400036A RID: 874
		// (add) Token: 0x060045E1 RID: 17889 RVA: 0x00126C98 File Offset: 0x00124E98
		// (remove) Token: 0x060045E2 RID: 17890 RVA: 0x00126CA1 File Offset: 0x00124EA1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400036B RID: 875
		// (add) Token: 0x060045E3 RID: 17891 RVA: 0x00126CAA File Offset: 0x00124EAA
		// (remove) Token: 0x060045E4 RID: 17892 RVA: 0x00126CB3 File Offset: 0x00124EB3
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400036C RID: 876
		// (add) Token: 0x060045E5 RID: 17893 RVA: 0x00126CBC File Offset: 0x00124EBC
		// (remove) Token: 0x060045E6 RID: 17894 RVA: 0x00126CC5 File Offset: 0x00124EC5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400036D RID: 877
		// (add) Token: 0x060045E7 RID: 17895 RVA: 0x00126CCE File Offset: 0x00124ECE
		// (remove) Token: 0x060045E8 RID: 17896 RVA: 0x00126CD7 File Offset: 0x00124ED7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler LocationChanged
		{
			add
			{
				base.LocationChanged += value;
			}
			remove
			{
				base.LocationChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400036E RID: 878
		// (add) Token: 0x060045E9 RID: 17897 RVA: 0x00126CE0 File Offset: 0x00124EE0
		// (remove) Token: 0x060045EA RID: 17898 RVA: 0x00126CE9 File Offset: 0x00124EE9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler OwnerChanged
		{
			add
			{
				base.OwnerChanged += value;
			}
			remove
			{
				base.OwnerChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripProgressBar.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x1400036F RID: 879
		// (add) Token: 0x060045EB RID: 17899 RVA: 0x00126CF2 File Offset: 0x00124EF2
		// (remove) Token: 0x060045EC RID: 17900 RVA: 0x00126D05 File Offset: 0x00124F05
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripProgressBar.EventRightToLeftLayoutChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripProgressBar.EventRightToLeftLayoutChanged, value);
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000370 RID: 880
		// (add) Token: 0x060045ED RID: 17901 RVA: 0x00126D18 File Offset: 0x00124F18
		// (remove) Token: 0x060045EE RID: 17902 RVA: 0x00126D21 File Offset: 0x00124F21
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000371 RID: 881
		// (add) Token: 0x060045EF RID: 17903 RVA: 0x00126D2A File Offset: 0x00124F2A
		// (remove) Token: 0x060045F0 RID: 17904 RVA: 0x00126D33 File Offset: 0x00124F33
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Validated
		{
			add
			{
				base.Validated += value;
			}
			remove
			{
				base.Validated -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000372 RID: 882
		// (add) Token: 0x060045F1 RID: 17905 RVA: 0x00126D3C File Offset: 0x00124F3C
		// (remove) Token: 0x060045F2 RID: 17906 RVA: 0x00126D45 File Offset: 0x00124F45
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event CancelEventHandler Validating
		{
			add
			{
				base.Validating += value;
			}
			remove
			{
				base.Validating -= value;
			}
		}

		/// <summary>Advances the current position of the progress bar by the specified amount.</summary>
		/// <param name="value">The amount by which to increment the progress bar's current position.</param>
		// Token: 0x060045F3 RID: 17907 RVA: 0x00126D4E File Offset: 0x00124F4E
		public void Increment(int value)
		{
			this.ProgressBar.Increment(value);
		}

		/// <summary>Advances the current position of the progress bar by the amount of the <see cref="P:System.Windows.Forms.ToolStripProgressBar.Step" /> property.</summary>
		// Token: 0x060045F4 RID: 17908 RVA: 0x00126D5C File Offset: 0x00124F5C
		public void PerformStep()
		{
			this.ProgressBar.PerformStep();
		}

		// Token: 0x04002670 RID: 9840
		internal static readonly object EventRightToLeftLayoutChanged = new object();

		// Token: 0x04002671 RID: 9841
		private static readonly Padding defaultMargin = new Padding(1, 2, 1, 1);

		// Token: 0x04002672 RID: 9842
		private static readonly Padding defaultStatusStripMargin = new Padding(1, 3, 1, 3);

		// Token: 0x04002673 RID: 9843
		private Padding scaledDefaultMargin = ToolStripProgressBar.defaultMargin;

		// Token: 0x04002674 RID: 9844
		private Padding scaledDefaultStatusStripMargin = ToolStripProgressBar.defaultStatusStripMargin;

		// Token: 0x02000814 RID: 2068
		[ComVisible(true)]
		internal class ToolStripProgressBarAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			// Token: 0x06006F88 RID: 28552 RVA: 0x00199553 File Offset: 0x00197753
			public ToolStripProgressBarAccessibleObject(ToolStripProgressBar ownerItem)
				: base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x17001864 RID: 6244
			// (get) Token: 0x06006F89 RID: 28553 RVA: 0x00199564 File Offset: 0x00197764
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.ProgressBar;
				}
			}

			// Token: 0x06006F8A RID: 28554 RVA: 0x00199585 File Offset: 0x00197785
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild || direction == UnsafeNativeMethods.NavigateDirection.LastChild)
				{
					return this.ownerItem.ProgressBar.AccessibilityObject;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x0400431E RID: 17182
			private ToolStripProgressBar ownerItem;
		}

		// Token: 0x02000815 RID: 2069
		internal class ToolStripProgressBarControl : ProgressBar
		{
			// Token: 0x17001865 RID: 6245
			// (get) Token: 0x06006F8B RID: 28555 RVA: 0x001995A7 File Offset: 0x001977A7
			// (set) Token: 0x06006F8C RID: 28556 RVA: 0x001995AF File Offset: 0x001977AF
			public ToolStripProgressBar Owner
			{
				get
				{
					return this.ownerItem;
				}
				set
				{
					this.ownerItem = value;
				}
			}

			// Token: 0x17001866 RID: 6246
			// (get) Token: 0x06006F8D RID: 28557 RVA: 0x000A83A1 File Offset: 0x000A65A1
			internal override bool SupportsUiaProviders
			{
				get
				{
					return AccessibilityImprovements.Level3;
				}
			}

			// Token: 0x06006F8E RID: 28558 RVA: 0x001995B8 File Offset: 0x001977B8
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				if (AccessibilityImprovements.Level3)
				{
					return new ToolStripProgressBar.ToolStripProgressBarControlAccessibleObject(this);
				}
				return base.CreateAccessibilityInstance();
			}

			// Token: 0x0400431F RID: 17183
			private ToolStripProgressBar ownerItem;
		}

		// Token: 0x02000816 RID: 2070
		internal class ToolStripProgressBarControlAccessibleObject : ProgressBar.ProgressBarAccessibleObject
		{
			// Token: 0x06006F90 RID: 28560 RVA: 0x001995D6 File Offset: 0x001977D6
			public ToolStripProgressBarControlAccessibleObject(ToolStripProgressBar.ToolStripProgressBarControl toolStripProgressBarControl)
				: base(toolStripProgressBarControl)
			{
				this._ownerToolStripProgressBarControl = toolStripProgressBarControl;
			}

			// Token: 0x17001867 RID: 6247
			// (get) Token: 0x06006F91 RID: 28561 RVA: 0x001995E8 File Offset: 0x001977E8
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					ToolStripProgressBar.ToolStripProgressBarControl toolStripProgressBarControl = base.Owner as ToolStripProgressBar.ToolStripProgressBarControl;
					if (toolStripProgressBarControl != null)
					{
						return toolStripProgressBarControl.Owner.Owner.AccessibilityObject;
					}
					return base.FragmentRoot;
				}
			}

			// Token: 0x06006F92 RID: 28562 RVA: 0x0019961C File Offset: 0x0019781C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction <= UnsafeNativeMethods.NavigateDirection.PreviousSibling)
				{
					ToolStripProgressBar.ToolStripProgressBarControl toolStripProgressBarControl = base.Owner as ToolStripProgressBar.ToolStripProgressBarControl;
					if (toolStripProgressBarControl != null)
					{
						return toolStripProgressBarControl.Owner.AccessibilityObject.FragmentNavigate(direction);
					}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x06006F93 RID: 28563 RVA: 0x00199655 File Offset: 0x00197855
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level5 && propertyID == 30022)
				{
					return ToolStripItem.GetIsOffscreenPropertyValue(this._ownerToolStripProgressBarControl.Owner.Placement, this.Bounds);
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x04004320 RID: 17184
			private readonly ToolStripProgressBar.ToolStripProgressBarControl _ownerToolStripProgressBarControl;
		}
	}
}
