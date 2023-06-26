using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants that define the state of the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
	// Token: 0x02000431 RID: 1073
	public enum WebBrowserReadyState
	{
		/// <summary>No document is currently loaded.</summary>
		// Token: 0x040027EF RID: 10223
		Uninitialized,
		/// <summary>The control is loading a new document.</summary>
		// Token: 0x040027F0 RID: 10224
		Loading,
		/// <summary>The control has loaded and initialized the new document, but has not yet received all the document data.</summary>
		// Token: 0x040027F1 RID: 10225
		Loaded,
		/// <summary>The control has loaded enough of the document to allow limited user interaction, such as clicking hyperlinks that have been displayed.</summary>
		// Token: 0x040027F2 RID: 10226
		Interactive,
		/// <summary>The control has finished loading the new document and all its contents.</summary>
		// Token: 0x040027F3 RID: 10227
		Complete
	}
}
