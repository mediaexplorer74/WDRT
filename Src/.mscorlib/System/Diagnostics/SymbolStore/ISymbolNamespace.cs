using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a namespace within a symbol store.</summary>
	// Token: 0x020003FE RID: 1022
	[ComVisible(true)]
	public interface ISymbolNamespace
	{
		/// <summary>Gets the current namespace.</summary>
		/// <returns>The current namespace.</returns>
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060033C7 RID: 13255
		string Name { get; }

		/// <summary>Gets the child members of the current namespace.</summary>
		/// <returns>The child members of the current namespace.</returns>
		// Token: 0x060033C8 RID: 13256
		ISymbolNamespace[] GetNamespaces();

		/// <summary>Gets all the variables defined at global scope within the current namespace.</summary>
		/// <returns>The variables defined at global scope within the current namespace.</returns>
		// Token: 0x060033C9 RID: 13257
		ISymbolVariable[] GetVariables();
	}
}
