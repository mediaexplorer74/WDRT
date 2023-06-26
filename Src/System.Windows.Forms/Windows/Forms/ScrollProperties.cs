using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Encapsulates properties related to scrolling.</summary>
	// Token: 0x0200035D RID: 861
	public abstract class ScrollProperties
	{
		/// <summary>Gets the control to which this scroll information applies.</summary>
		/// <returns>The control to which this scroll information applies.</returns>
		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06003858 RID: 14424 RVA: 0x000FA30C File Offset: 0x000F850C
		protected ScrollableControl ParentControl
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollProperties" /> class.</summary>
		/// <param name="container">The <see cref="T:System.Windows.Forms.ScrollableControl" /> whose scrolling properties this object describes.</param>
		// Token: 0x06003859 RID: 14425 RVA: 0x000FA314 File Offset: 0x000F8514
		protected ScrollProperties(ScrollableControl container)
		{
			this.parent = container;
		}

		/// <summary>Gets or sets whether the scroll bar can be used on the container.</summary>
		/// <returns>
		///   <see langword="true" /> if the scroll bar can be used; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x0600385A RID: 14426 RVA: 0x000FA341 File Offset: 0x000F8541
		// (set) Token: 0x0600385B RID: 14427 RVA: 0x000FA349 File Offset: 0x000F8549
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("ScrollBarEnableDescr")]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (this.parent.AutoScroll)
				{
					return;
				}
				if (value != this.enabled)
				{
					this.enabled = value;
					this.EnableScroll(value);
				}
			}
		}

		/// <summary>Gets or sets the distance to move a scroll bar in response to a large scroll command.</summary>
		/// <returns>An <see cref="T:System.Int32" /> describing how far, in pixels, to move the scroll bar in response to a large change.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Windows.Forms.ScrollProperties.LargeChange" /> cannot be less than zero.</exception>
		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x0600385C RID: 14428 RVA: 0x000FA370 File Offset: 0x000F8570
		// (set) Token: 0x0600385D RID: 14429 RVA: 0x000FA38C File Offset: 0x000F858C
		[SRCategory("CatBehavior")]
		[DefaultValue(10)]
		[SRDescription("ScrollBarLargeChangeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int LargeChange
		{
			get
			{
				return Math.Min(this.largeChange, this.maximum - this.minimum + 1);
			}
			set
			{
				if (this.largeChange != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("LargeChange", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"LargeChange",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.largeChange = value;
					this.largeChangeSetExternally = true;
					this.UpdateScrollInfo();
				}
			}
		}

		/// <summary>Gets or sets the upper limit of the scrollable range.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the maximum range of the scroll bar.</returns>
		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x0600385E RID: 14430 RVA: 0x000FA3FD File Offset: 0x000F85FD
		// (set) Token: 0x0600385F RID: 14431 RVA: 0x000FA408 File Offset: 0x000F8608
		[SRCategory("CatBehavior")]
		[DefaultValue(100)]
		[SRDescription("ScrollBarMaximumDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int Maximum
		{
			get
			{
				return this.maximum;
			}
			set
			{
				if (this.parent.AutoScroll)
				{
					return;
				}
				if (this.maximum != value)
				{
					if (this.minimum > value)
					{
						this.minimum = value;
					}
					if (value < this.value)
					{
						this.Value = value;
					}
					this.maximum = value;
					this.maximumSetExternally = true;
					this.UpdateScrollInfo();
				}
			}
		}

		/// <summary>Gets or sets the lower limit of the scrollable range.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the lower range of the scroll bar.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Windows.Forms.ScrollProperties.Minimum" /> cannot be less than zero.</exception>
		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06003860 RID: 14432 RVA: 0x000FA460 File Offset: 0x000F8660
		// (set) Token: 0x06003861 RID: 14433 RVA: 0x000FA468 File Offset: 0x000F8668
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[SRDescription("ScrollBarMinimumDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int Minimum
		{
			get
			{
				return this.minimum;
			}
			set
			{
				if (this.parent.AutoScroll)
				{
					return;
				}
				if (this.minimum != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("Minimum", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"Minimum",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.maximum < value)
					{
						this.maximum = value;
					}
					if (value > this.value)
					{
						this.value = value;
					}
					this.minimum = value;
					this.UpdateScrollInfo();
				}
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06003862 RID: 14434
		internal abstract int PageSize { get; }

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06003863 RID: 14435
		internal abstract int Orientation { get; }

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06003864 RID: 14436
		internal abstract int HorizontalDisplayPosition { get; }

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06003865 RID: 14437
		internal abstract int VerticalDisplayPosition { get; }

		/// <summary>Gets or sets the distance to move a scroll bar in response to a small scroll command.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing how far, in pixels, to move the scroll bar.</returns>
		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06003866 RID: 14438 RVA: 0x000FA500 File Offset: 0x000F8700
		// (set) Token: 0x06003867 RID: 14439 RVA: 0x000FA514 File Offset: 0x000F8714
		[SRCategory("CatBehavior")]
		[DefaultValue(1)]
		[SRDescription("ScrollBarSmallChangeDescr")]
		public int SmallChange
		{
			get
			{
				return Math.Min(this.smallChange, this.LargeChange);
			}
			set
			{
				if (this.smallChange != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("SmallChange", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"SmallChange",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.smallChange = value;
					this.smallChangeSetExternally = true;
					this.UpdateScrollInfo();
				}
			}
		}

		/// <summary>Gets or sets a numeric value that represents the current position of the scroll bar box.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the position of the scroll bar box, in pixels.</returns>
		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06003868 RID: 14440 RVA: 0x000FA585 File Offset: 0x000F8785
		// (set) Token: 0x06003869 RID: 14441 RVA: 0x000FA590 File Offset: 0x000F8790
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Bindable(true)]
		[SRDescription("ScrollBarValueDescr")]
		public int Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (this.value != value)
				{
					if (value < this.minimum || value > this.maximum)
					{
						throw new ArgumentOutOfRangeException("Value", SR.GetString("InvalidBoundArgument", new object[]
						{
							"Value",
							value.ToString(CultureInfo.CurrentCulture),
							"'minimum'",
							"'maximum'"
						}));
					}
					this.value = value;
					this.UpdateScrollInfo();
					this.parent.SetDisplayFromScrollProps(this.HorizontalDisplayPosition, this.VerticalDisplayPosition);
				}
			}
		}

		/// <summary>Gets or sets whether the scroll bar can be seen by the user.</summary>
		/// <returns>
		///   <see langword="true" /> if it can be seen; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x0600386A RID: 14442 RVA: 0x000FA61E File Offset: 0x000F881E
		// (set) Token: 0x0600386B RID: 14443 RVA: 0x000FA628 File Offset: 0x000F8828
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ScrollBarVisibleDescr")]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (this.parent.AutoScroll)
				{
					return;
				}
				if (value != this.visible)
				{
					this.visible = value;
					this.parent.UpdateStylesCore();
					this.UpdateScrollInfo();
					this.parent.SetDisplayFromScrollProps(this.HorizontalDisplayPosition, this.VerticalDisplayPosition);
				}
			}
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000FA67C File Offset: 0x000F887C
		internal void UpdateScrollInfo()
		{
			if (this.parent.IsHandleCreated && this.visible)
			{
				NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO();
				scrollinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.SCROLLINFO));
				scrollinfo.fMask = 23;
				scrollinfo.nMin = this.minimum;
				scrollinfo.nMax = this.maximum;
				scrollinfo.nPage = (this.parent.AutoScroll ? this.PageSize : this.LargeChange);
				scrollinfo.nPos = this.value;
				scrollinfo.nTrackPos = 0;
				UnsafeNativeMethods.SetScrollInfo(new HandleRef(this.parent, this.parent.Handle), this.Orientation, scrollinfo, true);
			}
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000FA738 File Offset: 0x000F8938
		private void EnableScroll(bool enable)
		{
			if (enable)
			{
				UnsafeNativeMethods.EnableScrollBar(new HandleRef(this.parent, this.parent.Handle), this.Orientation, 0);
				return;
			}
			UnsafeNativeMethods.EnableScrollBar(new HandleRef(this.parent, this.parent.Handle), this.Orientation, 3);
		}

		// Token: 0x040021A0 RID: 8608
		internal int minimum;

		// Token: 0x040021A1 RID: 8609
		internal int maximum = 100;

		// Token: 0x040021A2 RID: 8610
		internal int smallChange = 1;

		// Token: 0x040021A3 RID: 8611
		internal int largeChange = 10;

		// Token: 0x040021A4 RID: 8612
		internal int value;

		// Token: 0x040021A5 RID: 8613
		internal bool maximumSetExternally;

		// Token: 0x040021A6 RID: 8614
		internal bool smallChangeSetExternally;

		// Token: 0x040021A7 RID: 8615
		internal bool largeChangeSetExternally;

		// Token: 0x040021A8 RID: 8616
		private ScrollableControl parent;

		// Token: 0x040021A9 RID: 8617
		private const int SCROLL_LINE = 5;

		// Token: 0x040021AA RID: 8618
		internal bool visible;

		// Token: 0x040021AB RID: 8619
		private bool enabled = true;
	}
}
