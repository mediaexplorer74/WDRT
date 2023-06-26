using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a document referenced by a symbol store.</summary>
	// Token: 0x020003FB RID: 1019
	[ComVisible(true)]
	public interface ISymbolDocument
	{
		/// <summary>Gets the URL of the current document.</summary>
		/// <returns>The URL of the current document.</returns>
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060033B1 RID: 13233
		string URL { get; }

		/// <summary>Gets the type of the current document.</summary>
		/// <returns>The type of the current document.</returns>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060033B2 RID: 13234
		Guid DocumentType { get; }

		/// <summary>Gets the language of the current document.</summary>
		/// <returns>The language of the current document.</returns>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060033B3 RID: 13235
		Guid Language { get; }

		/// <summary>Gets the language vendor of the current document.</summary>
		/// <returns>The language vendor of the current document.</returns>
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060033B4 RID: 13236
		Guid LanguageVendor { get; }

		/// <summary>Gets the checksum algorithm identifier.</summary>
		/// <returns>A GUID identifying the checksum algorithm. The value is all zeros, if there is no checksum.</returns>
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060033B5 RID: 13237
		Guid CheckSumAlgorithmId { get; }

		/// <summary>Gets the checksum.</summary>
		/// <returns>The checksum.</returns>
		// Token: 0x060033B6 RID: 13238
		byte[] GetCheckSum();

		/// <summary>Returns the closest line that is a sequence point, given a line in the current document that might or might not be a sequence point.</summary>
		/// <param name="line">The specified line in the document.</param>
		/// <returns>The closest line that is a sequence point.</returns>
		// Token: 0x060033B7 RID: 13239
		int FindClosestLine(int line);

		/// <summary>Checks whether the current document is stored in the symbol store.</summary>
		/// <returns>
		///   <see langword="true" /> if the current document is stored in the symbol store; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060033B8 RID: 13240
		bool HasEmbeddedSource { get; }

		/// <summary>Gets the length, in bytes, of the embedded source.</summary>
		/// <returns>The source length of the current document.</returns>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060033B9 RID: 13241
		int SourceLength { get; }

		/// <summary>Gets the embedded document source for the specified range.</summary>
		/// <param name="startLine">The starting line in the current document.</param>
		/// <param name="startColumn">The starting column in the current document.</param>
		/// <param name="endLine">The ending line in the current document.</param>
		/// <param name="endColumn">The ending column in the current document.</param>
		/// <returns>The document source for the specified range.</returns>
		// Token: 0x060033BA RID: 13242
		byte[] GetSourceRange(int startLine, int startColumn, int endLine, int endColumn);
	}
}
