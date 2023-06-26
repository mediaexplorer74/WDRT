using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a lexical scope within <see cref="T:System.Diagnostics.SymbolStore.ISymbolMethod" />, providing access to the start and end offsets of the scope, as well as its child and parent scopes.</summary>
	// Token: 0x02000400 RID: 1024
	[ComVisible(true)]
	public interface ISymbolScope
	{
		/// <summary>Gets the method that contains the current lexical scope.</summary>
		/// <returns>The method that contains the current lexical scope.</returns>
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060033D4 RID: 13268
		ISymbolMethod Method { get; }

		/// <summary>Gets the parent lexical scope of the current scope.</summary>
		/// <returns>The parent lexical scope of the current scope.</returns>
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060033D5 RID: 13269
		ISymbolScope Parent { get; }

		/// <summary>Gets the child lexical scopes of the current lexical scope.</summary>
		/// <returns>The child lexical scopes that of the current lexical scope.</returns>
		// Token: 0x060033D6 RID: 13270
		ISymbolScope[] GetChildren();

		/// <summary>Gets the start offset of the current lexical scope.</summary>
		/// <returns>The start offset of the current lexical scope.</returns>
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060033D7 RID: 13271
		int StartOffset { get; }

		/// <summary>Gets the end offset of the current lexical scope.</summary>
		/// <returns>The end offset of the current lexical scope.</returns>
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060033D8 RID: 13272
		int EndOffset { get; }

		/// <summary>Gets the local variables within the current lexical scope.</summary>
		/// <returns>The local variables within the current lexical scope.</returns>
		// Token: 0x060033D9 RID: 13273
		ISymbolVariable[] GetLocals();

		/// <summary>Gets the namespaces that are used within the current scope.</summary>
		/// <returns>The namespaces that are used within the current scope.</returns>
		// Token: 0x060033DA RID: 13274
		ISymbolNamespace[] GetNamespaces();
	}
}
