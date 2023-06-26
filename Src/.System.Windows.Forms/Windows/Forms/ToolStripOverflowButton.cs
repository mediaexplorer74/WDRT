using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Hosts a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that displays items that overflow the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	// Token: 0x020003E9 RID: 1001
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.None)]
	public class ToolStripOverflowButton : ToolStripDropDownButton
	{
		// Token: 0x0600444A RID: 17482 RVA: 0x00120CD4 File Offset: 0x0011EED4
		internal ToolStripOverflowButton(ToolStrip parentToolStrip)
		{
			if (!ToolStripOverflowButton.isScalingInitialized)
			{
				if (DpiHelper.IsScalingRequired)
				{
					ToolStripOverflowButton.maxWidth = DpiHelper.LogicalToDeviceUnitsX(16);
					ToolStripOverflowButton.maxHeight = DpiHelper.LogicalToDeviceUnitsY(16);
				}
				ToolStripOverflowButton.isScalingInitialized = true;
			}
			base.SupportsItemClick = false;
			this.parentToolStrip = parentToolStrip;
		}

		/// <summary>Called by the <see cref="M:System.ComponentModel.Component.Dispose(System.Boolean)" /> and <see cref="M:System.ComponentModel.Component.Finalize" /> methods to release the managed and unmanaged resources used by the current instance of the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> class.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600444B RID: 17483 RVA: 0x00120D21 File Offset: 0x0011EF21
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.HasDropDownItems)
			{
				base.DropDown.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the space between controls.</returns>
		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x0600444C RID: 17484 RVA: 0x00019A61 File Offset: 0x00017C61
		protected internal override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> has items that overflow the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" /> has overflow items; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x0600444D RID: 17485 RVA: 0x00120D40 File Offset: 0x0011EF40
		public override bool HasDropDownItems
		{
			get
			{
				return base.ParentInternal.OverflowItems.Count > 0;
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x0600444E RID: 17486 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool OppositeDropDownAlign
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x0600444F RID: 17487 RVA: 0x00120D55 File Offset: 0x0011EF55
		internal ToolStrip ParentToolStrip
		{
			get
			{
				return this.parentToolStrip;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> to enable automatic mirroring; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x06004450 RID: 17488 RVA: 0x00111BD8 File Offset: 0x0010FDD8
		// (set) Token: 0x06004451 RID: 17489 RVA: 0x00111BE0 File Offset: 0x0010FDE0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool RightToLeftAutoMirrorImage
		{
			get
			{
				return base.RightToLeftAutoMirrorImage;
			}
			set
			{
				base.RightToLeftAutoMirrorImage = value;
			}
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06004452 RID: 17490 RVA: 0x00120D5D File Offset: 0x0011EF5D
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripOverflowButton.ToolStripOverflowButtonAccessibleObject(this);
		}

		/// <summary>Creates an empty <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that can be dropped down and to which events can be attached.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control.</returns>
		// Token: 0x06004453 RID: 17491 RVA: 0x00120D65 File Offset: 0x0011EF65
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripOverflow(this);
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can fit.</summary>
		/// <param name="constrainingSize">The custom-sized area for a control.</param>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x06004454 RID: 17492 RVA: 0x00120D70 File Offset: 0x0011EF70
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size size = constrainingSize;
			if (base.ParentInternal != null)
			{
				if (base.ParentInternal.Orientation == Orientation.Horizontal)
				{
					size.Width = Math.Min(constrainingSize.Width, ToolStripOverflowButton.maxWidth);
				}
				else
				{
					size.Height = Math.Min(constrainingSize.Height, ToolStripOverflowButton.maxHeight);
				}
			}
			return size + this.Padding.Size;
		}

		/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> representing the size and location of the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</param>
		// Token: 0x06004455 RID: 17493 RVA: 0x00120DDC File Offset: 0x0011EFDC
		protected internal override void SetBounds(Rectangle bounds)
		{
			if (base.ParentInternal != null && base.ParentInternal.LayoutEngine is ToolStripSplitStackLayout)
			{
				if (base.ParentInternal.Orientation == Orientation.Horizontal)
				{
					bounds.Height = base.ParentInternal.Height;
					bounds.Y = 0;
				}
				else
				{
					bounds.Width = base.ParentInternal.Width;
					bounds.X = 0;
				}
			}
			base.SetBounds(bounds);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004456 RID: 17494 RVA: 0x00120E50 File Offset: 0x0011F050
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.ParentInternal != null)
			{
				ToolStripRenderer renderer = base.ParentInternal.Renderer;
				renderer.DrawOverflowButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
			}
		}

		// Token: 0x04002614 RID: 9748
		private ToolStrip parentToolStrip;

		// Token: 0x04002615 RID: 9749
		private static bool isScalingInitialized = false;

		// Token: 0x04002616 RID: 9750
		private const int MAX_WIDTH = 16;

		// Token: 0x04002617 RID: 9751
		private const int MAX_HEIGHT = 16;

		// Token: 0x04002618 RID: 9752
		private static int maxWidth = 16;

		// Token: 0x04002619 RID: 9753
		private static int maxHeight = 16;

		// Token: 0x0200080B RID: 2059
		internal class ToolStripOverflowButtonAccessibleObject : ToolStripDropDownItemAccessibleObject
		{
			// Token: 0x06006F13 RID: 28435 RVA: 0x00196E85 File Offset: 0x00195085
			public ToolStripOverflowButtonAccessibleObject(ToolStripOverflowButton owner)
				: base(owner)
			{
			}

			// Token: 0x17001850 RID: 6224
			// (get) Token: 0x06006F14 RID: 28436 RVA: 0x00196E90 File Offset: 0x00195090
			// (set) Token: 0x06006F15 RID: 28437 RVA: 0x0019586D File Offset: 0x00193A6D
			public override string Name
			{
				get
				{
					string accessibleName = base.Owner.AccessibleName;
					if (accessibleName != null)
					{
						return accessibleName;
					}
					if (string.IsNullOrEmpty(this.stockName))
					{
						this.stockName = SR.GetString("ToolStripOptions");
					}
					return this.stockName;
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x06006F16 RID: 28438 RVA: 0x00196ED1 File Offset: 0x001950D1
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3 && propertyID == 30003)
				{
					return 50011;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x04004311 RID: 17169
			private string stockName;
		}
	}
}
