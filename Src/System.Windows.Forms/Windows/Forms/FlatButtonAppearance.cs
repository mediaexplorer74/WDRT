using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Provides properties that specify the appearance of <see cref="T:System.Windows.Forms.Button" /> controls whose <see cref="T:System.Windows.Forms.FlatStyle" /> is <see cref="F:System.Windows.Forms.FlatStyle.Flat" />.</summary>
	// Token: 0x02000252 RID: 594
	[TypeConverter(typeof(FlatButtonAppearanceConverter))]
	public class FlatButtonAppearance
	{
		// Token: 0x060025A7 RID: 9639 RVA: 0x000AF710 File Offset: 0x000AD910
		internal FlatButtonAppearance(ButtonBase owner)
		{
			this.owner = owner;
		}

		/// <summary>Gets or sets a value that specifies the size, in pixels, of the border around the button.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the size, in pixels, of the border around the button.</returns>
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x000AF75D File Offset: 0x000AD95D
		// (set) Token: 0x060025A9 RID: 9641 RVA: 0x000AF768 File Offset: 0x000AD968
		[Browsable(true)]
		[ApplicableToButton]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ButtonBorderSizeDescr")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(1)]
		public int BorderSize
		{
			get
			{
				return this.borderSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("BorderSize", value, SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"BorderSize",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.borderSize != value)
				{
					this.borderSize = value;
					if (this.owner != null && this.owner.ParentInternal != null)
					{
						LayoutTransaction.DoLayoutIf(this.owner.AutoSize, this.owner.ParentInternal, this.owner, PropertyNames.FlatAppearanceBorderSize);
					}
					this.owner.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the color of the border around the button.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the border around the button.</returns>
		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x000AF818 File Offset: 0x000ADA18
		// (set) Token: 0x060025AB RID: 9643 RVA: 0x000AF820 File Offset: 0x000ADA20
		[Browsable(true)]
		[ApplicableToButton]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ButtonBorderColorDescr")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(typeof(Color), "")]
		public Color BorderColor
		{
			get
			{
				return this.borderColor;
			}
			set
			{
				if (value.Equals(Color.Transparent))
				{
					throw new NotSupportedException(SR.GetString("ButtonFlatAppearanceInvalidBorderColor"));
				}
				if (this.borderColor != value)
				{
					this.borderColor = value;
					this.owner.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the color of the client area of the button when the button is checked and the mouse pointer is outside the bounds of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the client area of the button.</returns>
		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x000AF876 File Offset: 0x000ADA76
		// (set) Token: 0x060025AD RID: 9645 RVA: 0x000AF87E File Offset: 0x000ADA7E
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ButtonCheckedBackColorDescr")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(typeof(Color), "")]
		public Color CheckedBackColor
		{
			get
			{
				return this.checkedBackColor;
			}
			set
			{
				if (this.checkedBackColor != value)
				{
					this.checkedBackColor = value;
					this.owner.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the color of the client area of the button when the mouse is pressed within the bounds of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the client area of the button.</returns>
		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x060025AE RID: 9646 RVA: 0x000AF8A0 File Offset: 0x000ADAA0
		// (set) Token: 0x060025AF RID: 9647 RVA: 0x000AF8A8 File Offset: 0x000ADAA8
		[Browsable(true)]
		[ApplicableToButton]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ButtonMouseDownBackColorDescr")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(typeof(Color), "")]
		public Color MouseDownBackColor
		{
			get
			{
				return this.mouseDownBackColor;
			}
			set
			{
				if (this.mouseDownBackColor != value)
				{
					this.mouseDownBackColor = value;
					this.owner.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the color of the client area of the button when the mouse pointer is within the bounds of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the client area of the button.</returns>
		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x000AF8CA File Offset: 0x000ADACA
		// (set) Token: 0x060025B1 RID: 9649 RVA: 0x000AF8D2 File Offset: 0x000ADAD2
		[Browsable(true)]
		[ApplicableToButton]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ButtonMouseOverBackColorDescr")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(typeof(Color), "")]
		public Color MouseOverBackColor
		{
			get
			{
				return this.mouseOverBackColor;
			}
			set
			{
				if (this.mouseOverBackColor != value)
				{
					this.mouseOverBackColor = value;
					this.owner.Invalidate();
				}
			}
		}

		// Token: 0x04000F9B RID: 3995
		private ButtonBase owner;

		// Token: 0x04000F9C RID: 3996
		private int borderSize = 1;

		// Token: 0x04000F9D RID: 3997
		private Color borderColor = Color.Empty;

		// Token: 0x04000F9E RID: 3998
		private Color checkedBackColor = Color.Empty;

		// Token: 0x04000F9F RID: 3999
		private Color mouseDownBackColor = Color.Empty;

		// Token: 0x04000FA0 RID: 4000
		private Color mouseOverBackColor = Color.Empty;
	}
}
