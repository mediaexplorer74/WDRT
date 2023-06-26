using System;
using System.Drawing;

namespace System.Windows.Forms.Layout
{
	/// <summary>Provides the base class for implementing layout engines.</summary>
	// Token: 0x020004CB RID: 1227
	public abstract class LayoutEngine
	{
		// Token: 0x06005098 RID: 20632 RVA: 0x0014F550 File Offset: 0x0014D750
		internal IArrangedElement CastToArrangedElement(object obj)
		{
			IArrangedElement arrangedElement = obj as IArrangedElement;
			if (obj == null)
			{
				throw new NotSupportedException(SR.GetString("LayoutEngineUnsupportedType", new object[] { obj.GetType() }));
			}
			return arrangedElement;
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x000305B9 File Offset: 0x0002E7B9
		internal virtual Size GetPreferredSize(IArrangedElement container, Size proposedConstraints)
		{
			return Size.Empty;
		}

		/// <summary>Initializes the layout engine.</summary>
		/// <param name="child">The container on which the layout engine will operate.</param>
		/// <param name="specified">The bounds defining the container's size and position.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="child" /> is not a type on which <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> can perform layout.</exception>
		// Token: 0x0600509A RID: 20634 RVA: 0x0014F587 File Offset: 0x0014D787
		public virtual void InitLayout(object child, BoundsSpecified specified)
		{
			this.InitLayoutCore(this.CastToArrangedElement(child), specified);
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void InitLayoutCore(IArrangedElement element, BoundsSpecified bounds)
		{
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void ProcessSuspendedLayoutEventArgs(IArrangedElement container, LayoutEventArgs args)
		{
		}

		/// <summary>Requests that the layout engine perform a layout operation.</summary>
		/// <param name="container">The container on which the layout engine will operate.</param>
		/// <param name="layoutEventArgs">An event argument from a <see cref="E:System.Windows.Forms.Control.Layout" /> event.</param>
		/// <returns>
		///   <see langword="true" /> if layout should be performed again by the parent of <paramref name="container" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="container" /> is not a type on which <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> can perform layout.</exception>
		// Token: 0x0600509D RID: 20637 RVA: 0x0014F598 File Offset: 0x0014D798
		public virtual bool Layout(object container, LayoutEventArgs layoutEventArgs)
		{
			return this.LayoutCore(this.CastToArrangedElement(container), layoutEventArgs);
		}

		// Token: 0x0600509E RID: 20638 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal virtual bool LayoutCore(IArrangedElement container, LayoutEventArgs layoutEventArgs)
		{
			return false;
		}
	}
}
