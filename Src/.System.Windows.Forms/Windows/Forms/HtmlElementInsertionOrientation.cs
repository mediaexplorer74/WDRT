using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values that describe where to insert a new element when using <see cref="M:System.Windows.Forms.HtmlElement.InsertAdjacentElement(System.Windows.Forms.HtmlElementInsertionOrientation,System.Windows.Forms.HtmlElement)" />.</summary>
	// Token: 0x0200027A RID: 634
	public enum HtmlElementInsertionOrientation
	{
		/// <summary>Insert the element before the current element.</summary>
		// Token: 0x040010BC RID: 4284
		BeforeBegin,
		/// <summary>Insert the element after the current element, but before all other content in the current element.</summary>
		// Token: 0x040010BD RID: 4285
		AfterBegin,
		/// <summary>Insert the element after the current element.</summary>
		// Token: 0x040010BE RID: 4286
		BeforeEnd,
		/// <summary>Insert the element after the current element, but after all other content in the current element.</summary>
		// Token: 0x040010BF RID: 4287
		AfterEnd
	}
}
