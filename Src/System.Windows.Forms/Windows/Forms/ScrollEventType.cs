using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the type of action used to raise the <see cref="E:System.Windows.Forms.ScrollBar.Scroll" /> event.</summary>
	// Token: 0x0200035B RID: 859
	[ComVisible(true)]
	public enum ScrollEventType
	{
		/// <summary>The scroll box was moved a small distance. The user clicked the left(horizontal) or top(vertical) scroll arrow, or pressed the UP ARROW key.</summary>
		// Token: 0x04002194 RID: 8596
		SmallDecrement,
		/// <summary>The scroll box was moved a small distance. The user clicked the right(horizontal) or bottom(vertical) scroll arrow, or pressed the DOWN ARROW key.</summary>
		// Token: 0x04002195 RID: 8597
		SmallIncrement,
		/// <summary>The scroll box moved a large distance. The user clicked the scroll bar to the left(horizontal) or above(vertical) the scroll box, or pressed the PAGE UP key.</summary>
		// Token: 0x04002196 RID: 8598
		LargeDecrement,
		/// <summary>The scroll box moved a large distance. The user clicked the scroll bar to the right(horizontal) or below(vertical) the scroll box, or pressed the PAGE DOWN key.</summary>
		// Token: 0x04002197 RID: 8599
		LargeIncrement,
		/// <summary>The scroll box was moved.</summary>
		// Token: 0x04002198 RID: 8600
		ThumbPosition,
		/// <summary>The scroll box is currently being moved.</summary>
		// Token: 0x04002199 RID: 8601
		ThumbTrack,
		/// <summary>The scroll box was moved to the <see cref="P:System.Windows.Forms.ScrollBar.Minimum" /> position.</summary>
		// Token: 0x0400219A RID: 8602
		First,
		/// <summary>The scroll box was moved to the <see cref="P:System.Windows.Forms.ScrollBar.Maximum" /> position.</summary>
		// Token: 0x0400219B RID: 8603
		Last,
		/// <summary>The scroll box has stopped moving.</summary>
		// Token: 0x0400219C RID: 8604
		EndScroll
	}
}
