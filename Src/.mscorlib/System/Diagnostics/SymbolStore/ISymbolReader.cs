using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a symbol reader for managed code.</summary>
	// Token: 0x020003FF RID: 1023
	[ComVisible(true)]
	public interface ISymbolReader
	{
		/// <summary>Gets a document specified by the language, vendor, and type.</summary>
		/// <param name="url">The URL that identifies the document.</param>
		/// <param name="language">The document language. You can specify this parameter as <see cref="F:System.Guid.Empty" />.</param>
		/// <param name="languageVendor">The identity of the vendor for the document language. You can specify this parameter as <see cref="F:System.Guid.Empty" />.</param>
		/// <param name="documentType">The type of the document. You can specify this parameter as <see cref="F:System.Guid.Empty" />.</param>
		/// <returns>The specified document.</returns>
		// Token: 0x060033CA RID: 13258
		ISymbolDocument GetDocument(string url, Guid language, Guid languageVendor, Guid documentType);

		/// <summary>Gets an array of all documents defined in the symbol store.</summary>
		/// <returns>An array of all documents defined in the symbol store.</returns>
		// Token: 0x060033CB RID: 13259
		ISymbolDocument[] GetDocuments();

		/// <summary>Gets the metadata token for the method that was specified as the user entry point for the module, if any.</summary>
		/// <returns>The metadata token for the method that is the user entry point for the module.</returns>
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060033CC RID: 13260
		SymbolToken UserEntryPoint { get; }

		/// <summary>Gets a symbol reader method object when given the identifier of a method.</summary>
		/// <param name="method">The metadata token of the method.</param>
		/// <returns>The symbol reader method object for the specified method identifier.</returns>
		// Token: 0x060033CD RID: 13261
		ISymbolMethod GetMethod(SymbolToken method);

		/// <summary>Gets a symbol reader method object when given the identifier of a method and its edit and continue version.</summary>
		/// <param name="method">The metadata token of the method.</param>
		/// <param name="version">The edit and continue version of the method.</param>
		/// <returns>The symbol reader method object for the specified method identifier.</returns>
		// Token: 0x060033CE RID: 13262
		ISymbolMethod GetMethod(SymbolToken method, int version);

		/// <summary>Gets the variables that are not local when given the parent.</summary>
		/// <param name="parent">The metadata token for the type for which the variables are requested.</param>
		/// <returns>An array of variables for the parent.</returns>
		// Token: 0x060033CF RID: 13263
		ISymbolVariable[] GetVariables(SymbolToken parent);

		/// <summary>Gets all global variables in the module.</summary>
		/// <returns>An array of all variables in the module.</returns>
		// Token: 0x060033D0 RID: 13264
		ISymbolVariable[] GetGlobalVariables();

		/// <summary>Gets a symbol reader method object that contains a specified position in a document.</summary>
		/// <param name="document">The document in which the method is located.</param>
		/// <param name="line">The position of the line within the document. The lines are numbered, beginning with 1.</param>
		/// <param name="column">The position of column within the document. The columns are numbered, beginning with 1.</param>
		/// <returns>The reader method object for the specified position in the document.</returns>
		// Token: 0x060033D1 RID: 13265
		ISymbolMethod GetMethodFromDocumentPosition(ISymbolDocument document, int line, int column);

		/// <summary>Gets an attribute value when given the attribute name.</summary>
		/// <param name="parent">The metadata token for the object for which the attribute is requested.</param>
		/// <param name="name">The attribute name.</param>
		/// <returns>The value of the attribute.</returns>
		// Token: 0x060033D2 RID: 13266
		byte[] GetSymAttribute(SymbolToken parent, string name);

		/// <summary>Gets the namespaces that are defined in the global scope within the current symbol store.</summary>
		/// <returns>The namespaces defined in the global scope within the current symbol store.</returns>
		// Token: 0x060033D3 RID: 13267
		ISymbolNamespace[] GetNamespaces();
	}
}
