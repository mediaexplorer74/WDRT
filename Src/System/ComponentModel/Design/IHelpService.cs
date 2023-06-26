using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides methods for showing Help topics and adding and removing Help keywords at design time.</summary>
	// Token: 0x020005EF RID: 1519
	public interface IHelpService
	{
		/// <summary>Adds a context attribute to the document.</summary>
		/// <param name="name">The name of the attribute to add.</param>
		/// <param name="value">The value of the attribute.</param>
		/// <param name="keywordType">The type of the keyword, from the enumeration <see cref="T:System.ComponentModel.Design.HelpKeywordType" />.</param>
		// Token: 0x0600381B RID: 14363
		void AddContextAttribute(string name, string value, HelpKeywordType keywordType);

		/// <summary>Removes all existing context attributes from the document.</summary>
		// Token: 0x0600381C RID: 14364
		void ClearContextAttributes();

		/// <summary>Creates a local <see cref="T:System.ComponentModel.Design.IHelpService" /> to manage subcontexts.</summary>
		/// <param name="contextType">The priority type of the subcontext to add.</param>
		/// <returns>The newly created <see cref="T:System.ComponentModel.Design.IHelpService" />.</returns>
		// Token: 0x0600381D RID: 14365
		IHelpService CreateLocalContext(HelpContextType contextType);

		/// <summary>Removes a previously added context attribute.</summary>
		/// <param name="name">The name of the attribute to remove.</param>
		/// <param name="value">The value of the attribute to remove.</param>
		// Token: 0x0600381E RID: 14366
		void RemoveContextAttribute(string name, string value);

		/// <summary>Removes a context created with <see cref="M:System.ComponentModel.Design.IHelpService.CreateLocalContext(System.ComponentModel.Design.HelpContextType)" />.</summary>
		/// <param name="localContext">The local context <see cref="T:System.ComponentModel.Design.IHelpService" /> to remove.</param>
		// Token: 0x0600381F RID: 14367
		void RemoveLocalContext(IHelpService localContext);

		/// <summary>Shows the Help topic that corresponds to the specified keyword.</summary>
		/// <param name="helpKeyword">The keyword of the Help topic to display.</param>
		// Token: 0x06003820 RID: 14368
		void ShowHelpFromKeyword(string helpKeyword);

		/// <summary>Shows the Help topic that corresponds to the specified URL.</summary>
		/// <param name="helpUrl">The URL of the Help topic to display.</param>
		// Token: 0x06003821 RID: 14369
		void ShowHelpFromUrl(string helpUrl);
	}
}
