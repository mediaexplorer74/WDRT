using System;

namespace System.Windows.Forms
{
	/// <summary>The site for a <see cref="T:System.Windows.Forms.Design.ComponentEditorPage" />.</summary>
	// Token: 0x02000445 RID: 1093
	public interface IComponentEditorPageSite
	{
		/// <summary>Returns the parent control for the page window.</summary>
		/// <returns>The parent control for the page window.</returns>
		// Token: 0x06004BFC RID: 19452
		Control GetControl();

		/// <summary>Notifies the site that the editor is in a modified state.</summary>
		// Token: 0x06004BFD RID: 19453
		void SetDirty();
	}
}
