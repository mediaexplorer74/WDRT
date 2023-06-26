using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Manages the overflow behavior of a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	// Token: 0x020003E8 RID: 1000
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class ToolStripOverflow : ToolStripDropDown, IArrangedElement, IComponent, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripOverflow" /> class derived from a base <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="parentItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> from which to derive this <see cref="T:System.Windows.Forms.ToolStripOverflow" /> instance.</param>
		// Token: 0x0600443C RID: 17468 RVA: 0x00120B73 File Offset: 0x0011ED73
		public ToolStripOverflow(ToolStripItem parentItem)
			: base(parentItem)
		{
			if (parentItem == null)
			{
				throw new ArgumentNullException("parentItem");
			}
			this.ownerItem = parentItem as ToolStripOverflowButton;
		}

		/// <summary>Gets all of the items that are currently being displayed on the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> that includes all items on this <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x0600443D RID: 17469 RVA: 0x00120B98 File Offset: 0x0011ED98
		protected internal override ToolStripItemCollection DisplayedItems
		{
			get
			{
				if (this.ParentToolStrip != null)
				{
					return this.ParentToolStrip.OverflowItems;
				}
				return new ToolStripItemCollection(null, false);
			}
		}

		/// <summary>Gets all of the items on the <see cref="T:System.Windows.Forms.ToolStrip" />, whether they are currently being displayed or not.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> containing all of the items.</returns>
		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x0600443E RID: 17470 RVA: 0x00120BC2 File Offset: 0x0011EDC2
		public override ToolStripItemCollection Items
		{
			get
			{
				return new ToolStripItemCollection(null, false, true);
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x0600443F RID: 17471 RVA: 0x00120BCC File Offset: 0x0011EDCC
		private ToolStrip ParentToolStrip
		{
			get
			{
				if (this.ownerItem != null)
				{
					return this.ownerItem.ParentToolStrip;
				}
				return null;
			}
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06004440 RID: 17472 RVA: 0x00120BE3 File Offset: 0x0011EDE3
		ArrangedElementCollection IArrangedElement.Children
		{
			get
			{
				return this.DisplayedItems;
			}
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06004441 RID: 17473 RVA: 0x0003B935 File Offset: 0x00039B35
		IArrangedElement IArrangedElement.Container
		{
			get
			{
				return this.ParentInternal;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x06004442 RID: 17474 RVA: 0x0003B93D File Offset: 0x00039B3D
		bool IArrangedElement.ParticipatesInLayout
		{
			get
			{
				return base.GetState(2);
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06004443 RID: 17475 RVA: 0x0003B955 File Offset: 0x00039B55
		PropertyStore IArrangedElement.Properties
		{
			get
			{
				return base.Properties;
			}
		}

		// Token: 0x06004444 RID: 17476 RVA: 0x00110203 File Offset: 0x0010E403
		void IArrangedElement.SetBounds(Rectangle bounds, BoundsSpecified specified)
		{
			this.SetBoundsCore(bounds.X, bounds.Y, bounds.Width, bounds.Height, specified);
		}

		/// <summary>Passes a reference to the cached <see cref="P:System.Windows.Forms.Control.LayoutEngine" /> returned by the layout engine interface.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> that represents the cached layout engine returned by the layout engine interface.</returns>
		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06004445 RID: 17477 RVA: 0x000AF974 File Offset: 0x000ADB74
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return FlowLayout.Instance;
			}
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06004446 RID: 17478 RVA: 0x00120BEB File Offset: 0x0011EDEB
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripOverflow.ToolStripOverflowAccessibleObject(this);
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <param name="constrainingSize">The custom-sized area for a control.</param>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x06004447 RID: 17479 RVA: 0x00120BF3 File Offset: 0x0011EDF3
		public override Size GetPreferredSize(Size constrainingSize)
		{
			constrainingSize.Width = 200;
			return base.GetPreferredSize(constrainingSize);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06004448 RID: 17480 RVA: 0x00120C08 File Offset: 0x0011EE08
		protected override void OnLayout(LayoutEventArgs e)
		{
			if (this.ParentToolStrip != null && this.ParentToolStrip.IsInDesignMode)
			{
				if (FlowLayout.GetFlowDirection(this) != FlowDirection.TopDown)
				{
					FlowLayout.SetFlowDirection(this, FlowDirection.TopDown);
				}
				if (FlowLayout.GetWrapContents(this))
				{
					FlowLayout.SetWrapContents(this, false);
				}
			}
			else
			{
				if (FlowLayout.GetFlowDirection(this) != FlowDirection.LeftToRight)
				{
					FlowLayout.SetFlowDirection(this, FlowDirection.LeftToRight);
				}
				if (!FlowLayout.GetWrapContents(this))
				{
					FlowLayout.SetWrapContents(this, true);
				}
			}
			base.OnLayout(e);
		}

		/// <summary>Resets the collection of displayed and overflow items after a layout is done.</summary>
		// Token: 0x06004449 RID: 17481 RVA: 0x00120C70 File Offset: 0x0011EE70
		protected override void SetDisplayedItems()
		{
			Size size = Size.Empty;
			for (int i = 0; i < this.DisplayedItems.Count; i++)
			{
				ToolStripItem toolStripItem = this.DisplayedItems[i];
				if (((IArrangedElement)toolStripItem).ParticipatesInLayout)
				{
					base.HasVisibleItems = true;
					size = LayoutUtils.UnionSizes(size, toolStripItem.Bounds.Size);
				}
			}
			base.SetLargestItemSize(size);
		}

		// Token: 0x04002612 RID: 9746
		internal static readonly TraceSwitch PopupLayoutDebug;

		// Token: 0x04002613 RID: 9747
		private ToolStripOverflowButton ownerItem;

		// Token: 0x0200080A RID: 2058
		internal class ToolStripOverflowAccessibleObject : ToolStrip.ToolStripAccessibleObject
		{
			// Token: 0x06006F10 RID: 28432 RVA: 0x0018BC34 File Offset: 0x00189E34
			public ToolStripOverflowAccessibleObject(ToolStripOverflow owner)
				: base(owner)
			{
			}

			// Token: 0x06006F11 RID: 28433 RVA: 0x00196E51 File Offset: 0x00195051
			public override AccessibleObject GetChild(int index)
			{
				return ((ToolStripOverflow)base.Owner).DisplayedItems[index].AccessibilityObject;
			}

			// Token: 0x06006F12 RID: 28434 RVA: 0x00196E6E File Offset: 0x0019506E
			public override int GetChildCount()
			{
				return ((ToolStripOverflow)base.Owner).DisplayedItems.Count;
			}
		}
	}
}
