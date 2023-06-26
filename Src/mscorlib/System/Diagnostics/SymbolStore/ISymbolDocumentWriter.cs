using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a document referenced by a symbol store.</summary>
	// Token: 0x020003FC RID: 1020
	[ComVisible(true)]
	public interface ISymbolDocumentWriter
	{
		/// <summary>Stores the raw source for a document in the symbol store.</summary>
		/// <param name="source">The document source represented as unsigned bytes.</param>
		// Token: 0x060033BB RID: 13243
		void SetSource(byte[] source);

		/// <summary>Sets checksum information.</summary>
		/// <param name="algorithmId">The GUID representing the algorithm ID.</param>
		/// <param name="checkSum">The checksum.</param>
		// Token: 0x060033BC RID: 13244
		void SetCheckSum(Guid algorithmId, byte[] checkSum);
	}
}
