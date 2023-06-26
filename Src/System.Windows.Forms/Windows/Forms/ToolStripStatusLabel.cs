using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Automation;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a panel in a <see cref="T:System.Windows.Forms.StatusStrip" /> control.</summary>
	// Token: 0x02000404 RID: 1028
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.StatusStrip)]
	public class ToolStripStatusLabel : ToolStripLabel, IAutomationLiveRegion
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class.</summary>
		// Token: 0x0600472D RID: 18221 RVA: 0x0012AD24 File Offset: 0x00128F24
		public ToolStripStatusLabel()
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class that displays the specified text.</summary>
		/// <param name="text">A <see cref="T:System.String" /> representing the text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		// Token: 0x0600472E RID: 18222 RVA: 0x0012AD48 File Offset: 0x00128F48
		public ToolStripStatusLabel(string text)
			: base(text, null, false, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class that displays the specified image.</summary>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> that is displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		// Token: 0x0600472F RID: 18223 RVA: 0x0012AD70 File Offset: 0x00128F70
		public ToolStripStatusLabel(Image image)
			: base(null, image, false, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class that displays the specified image and text.</summary>
		/// <param name="text">A <see cref="T:System.String" /> representing the text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> that is displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		// Token: 0x06004730 RID: 18224 RVA: 0x0012AD98 File Offset: 0x00128F98
		public ToolStripStatusLabel(string text, Image image)
			: base(text, image, false, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class that displays the specified image and text, and that carries out the specified action when the user clicks the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</summary>
		/// <param name="text">A <see cref="T:System.String" /> representing the text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> that is displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		/// <param name="onClick">Specifies the action to carry out when the control is clicked.</param>
		// Token: 0x06004731 RID: 18225 RVA: 0x0012ADC0 File Offset: 0x00128FC0
		public ToolStripStatusLabel(string text, Image image, EventHandler onClick)
			: base(text, image, false, onClick, null)
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class with the specified name that displays the specified image and text, and that carries out the specified action when the user clicks the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</summary>
		/// <param name="text">A <see cref="T:System.String" /> representing the text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> that is displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		/// <param name="onClick">Specifies the action to carry out when the control is clicked.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		// Token: 0x06004732 RID: 18226 RVA: 0x0012ADE9 File Offset: 0x00128FE9
		public ToolStripStatusLabel(string text, Image image, EventHandler onClick, string name)
			: base(text, image, false, onClick, name)
		{
			this.Initialize();
		}

		/// <summary>Creates a new instance of the accessibility object for the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> control.</summary>
		/// <returns>The accessibility object for this <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> control.</returns>
		// Token: 0x06004733 RID: 18227 RVA: 0x0012AE13 File Offset: 0x00129013
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ToolStripStatusLabel.ToolStripStatusLabelAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x0012AE29 File Offset: 0x00129029
		internal override ToolStripItemInternalLayout CreateInternalLayout()
		{
			return new ToolStripStatusLabel.ToolStripStatusLabelLayout(this);
		}

		/// <summary>Gets or sets a value that determines where the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> is aligned on the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemAlignment" /> values.</returns>
		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06004735 RID: 18229 RVA: 0x0012AE31 File Offset: 0x00129031
		// (set) Token: 0x06004736 RID: 18230 RVA: 0x0012AE39 File Offset: 0x00129039
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new ToolStripItemAlignment Alignment
		{
			get
			{
				return base.Alignment;
			}
			set
			{
				base.Alignment = value;
			}
		}

		/// <summary>Gets or sets the border style of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values. The default is <see cref="F:System.Windows.Forms.Border3DStyle.Flat" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of <see cref="P:System.Windows.Forms.ToolStripStatusLabel.BorderStyle" /> is not one of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values.</exception>
		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06004737 RID: 18231 RVA: 0x0012AE42 File Offset: 0x00129042
		// (set) Token: 0x06004738 RID: 18232 RVA: 0x0012AE4C File Offset: 0x0012904C
		[DefaultValue(Border3DStyle.Flat)]
		[SRDescription("ToolStripStatusLabelBorderStyleDescr")]
		[SRCategory("CatAppearance")]
		public Border3DStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid_NotSequential(value, (int)value, new int[] { 8192, 9, 6, 16394, 5, 4, 1, 10, 8, 2 }))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(Border3DStyle));
				}
				if (this.borderStyle != value)
				{
					this.borderStyle = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value that indicates which sides of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> show borders.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripStatusLabelBorderSides" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripStatusLabelBorderSides.None" />.</returns>
		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x06004739 RID: 18233 RVA: 0x0012AEA5 File Offset: 0x001290A5
		// (set) Token: 0x0600473A RID: 18234 RVA: 0x0012AEAD File Offset: 0x001290AD
		[DefaultValue(ToolStripStatusLabelBorderSides.None)]
		[SRDescription("ToolStripStatusLabelBorderSidesDescr")]
		[SRCategory("CatAppearance")]
		public ToolStripStatusLabelBorderSides BorderSides
		{
			get
			{
				return this.borderSides;
			}
			set
			{
				if (this.borderSides != value)
				{
					this.borderSides = value;
					LayoutTransaction.DoLayout(base.Owner, this, PropertyNames.BorderStyle);
					base.Invalidate();
				}
			}
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x0012AED6 File Offset: 0x001290D6
		private void Initialize()
		{
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledDefaultMargin = DpiHelper.LogicalToDeviceUnits(ToolStripStatusLabel.defaultMargin, 0);
			}
		}

		/// <summary>Gets the default margin of an item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the margin.</returns>
		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x0600473C RID: 18236 RVA: 0x0012AEF0 File Offset: 0x001290F0
		protected internal override Padding DefaultMargin
		{
			get
			{
				return this.scaledDefaultMargin;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> automatically fills the available space on the <see cref="T:System.Windows.Forms.StatusStrip" /> as the form is resized.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> automatically fills the available space on the <see cref="T:System.Windows.Forms.StatusStrip" /> as the form is resized; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x0600473D RID: 18237 RVA: 0x0012AEF8 File Offset: 0x001290F8
		// (set) Token: 0x0600473E RID: 18238 RVA: 0x0012AF00 File Offset: 0x00129100
		[DefaultValue(false)]
		[SRDescription("ToolStripStatusLabelSpringDescr")]
		[SRCategory("CatAppearance")]
		public bool Spring
		{
			get
			{
				return this.spring;
			}
			set
			{
				if (this.spring != value)
				{
					this.spring = value;
					if (base.ParentInternal != null)
					{
						LayoutTransaction.DoLayout(base.ParentInternal, this, PropertyNames.Spring);
					}
				}
			}
		}

		/// <summary>Indicates the politeness level that a client should use to notify the user of changes to the live region.</summary>
		/// <returns>The politeness level for notifications. Its default value is <see cref="F:System.Windows.Forms.Automation.AutomationLiveSetting.Off" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.Automation.AutomationLiveSetting" /> values.</exception>
		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x0600473F RID: 18239 RVA: 0x0012AF2B File Offset: 0x0012912B
		// (set) Token: 0x06004740 RID: 18240 RVA: 0x0012AF33 File Offset: 0x00129133
		[SRCategory("CatAccessibility")]
		[DefaultValue(AutomationLiveSetting.Off)]
		[SRDescription("LiveRegionAutomationLiveSettingDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutomationLiveSetting LiveSetting
		{
			get
			{
				return this.liveSetting;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutomationLiveSetting));
				}
				this.liveSetting = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.TextChanged" /> event.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x06004741 RID: 18241 RVA: 0x0012AF62 File Offset: 0x00129162
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			if (AccessibilityImprovements.Level3 && this.LiveSetting != AutomationLiveSetting.Off)
			{
				base.AccessibilityObject.RaiseLiveRegionChanged();
			}
		}

		/// <summary>Retrieves the size of a rectangular area into which a <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> can be fitted.</summary>
		/// <param name="constrainingSize">The custom-sized area for a control.</param>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" />, representing the width and height of a rectangle.</returns>
		// Token: 0x06004742 RID: 18242 RVA: 0x0012AF86 File Offset: 0x00129186
		public override Size GetPreferredSize(Size constrainingSize)
		{
			if (this.BorderSides != ToolStripStatusLabelBorderSides.None)
			{
				return base.GetPreferredSize(constrainingSize) + new Size(4, 4);
			}
			return base.GetPreferredSize(constrainingSize);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06004743 RID: 18243 RVA: 0x0012AFAC File Offset: 0x001291AC
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.Owner != null)
			{
				ToolStripRenderer renderer = base.Renderer;
				renderer.DrawToolStripStatusLabelBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image)
				{
					renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, base.InternalLayout.ImageRectangle));
				}
				base.PaintText(e.Graphics);
			}
		}

		// Token: 0x040026CA RID: 9930
		private static readonly Padding defaultMargin = new Padding(0, 3, 0, 2);

		// Token: 0x040026CB RID: 9931
		private Padding scaledDefaultMargin = ToolStripStatusLabel.defaultMargin;

		// Token: 0x040026CC RID: 9932
		private Border3DStyle borderStyle = Border3DStyle.Flat;

		// Token: 0x040026CD RID: 9933
		private ToolStripStatusLabelBorderSides borderSides;

		// Token: 0x040026CE RID: 9934
		private bool spring;

		// Token: 0x040026CF RID: 9935
		private AutomationLiveSetting liveSetting;

		// Token: 0x0200081F RID: 2079
		[ComVisible(true)]
		internal class ToolStripStatusLabelAccessibleObject : ToolStripLabel.ToolStripLabelAccessibleObject
		{
			// Token: 0x06006FC3 RID: 28611 RVA: 0x00199B6A File Offset: 0x00197D6A
			public ToolStripStatusLabelAccessibleObject(ToolStripStatusLabel ownerItem)
				: base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x06006FC4 RID: 28612 RVA: 0x00199B7A File Offset: 0x00197D7A
			public override bool RaiseLiveRegionChanged()
			{
				return base.RaiseAutomationEvent(20024);
			}

			// Token: 0x06006FC5 RID: 28613 RVA: 0x00199B87 File Offset: 0x00197D87
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50020;
				}
				if (propertyID == 30135)
				{
					return this.ownerItem.LiveSetting;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x0400432F RID: 17199
			private ToolStripStatusLabel ownerItem;
		}

		// Token: 0x02000820 RID: 2080
		private class ToolStripStatusLabelLayout : ToolStripItemInternalLayout
		{
			// Token: 0x06006FC6 RID: 28614 RVA: 0x00199BBC File Offset: 0x00197DBC
			public ToolStripStatusLabelLayout(ToolStripStatusLabel owner)
				: base(owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006FC7 RID: 28615 RVA: 0x00199BCC File Offset: 0x00197DCC
			protected override ToolStripItemInternalLayout.ToolStripItemLayoutOptions CommonLayoutOptions()
			{
				ToolStripItemInternalLayout.ToolStripItemLayoutOptions toolStripItemLayoutOptions = base.CommonLayoutOptions();
				toolStripItemLayoutOptions.borderSize = 0;
				return toolStripItemLayoutOptions;
			}

			// Token: 0x04004330 RID: 17200
			private ToolStripStatusLabel owner;
		}
	}
}
