using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a variable within a symbol store.</summary>
	// Token: 0x02000401 RID: 1025
	[ComVisible(true)]
	public interface ISymbolVariable
	{
		/// <summary>Gets the name of the variable.</summary>
		/// <returns>The name of the variable.</returns>
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060033DB RID: 13275
		string Name { get; }

		/// <summary>Gets the attributes of the variable.</summary>
		/// <returns>The variable attributes.</returns>
		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060033DC RID: 13276
		object Attributes { get; }

		/// <summary>Gets the variable signature.</summary>
		/// <returns>The variable signature as an opaque blob.</returns>
		// Token: 0x060033DD RID: 13277
		byte[] GetSignature();

		/// <summary>Gets the <see cref="T:System.Diagnostics.SymbolStore.SymAddressKind" /> value describing the type of the address.</summary>
		/// <returns>The type of the address. One of the <see cref="T:System.Diagnostics.SymbolStore.SymAddressKind" /> values.</returns>
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060033DE RID: 13278
		SymAddressKind AddressKind { get; }

		/// <summary>Gets the first address of a variable.</summary>
		/// <returns>The first address of the variable.</returns>
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060033DF RID: 13279
		int AddressField1 { get; }

		/// <summary>Gets the second address of a variable.</summary>
		/// <returns>The second address of the variable.</returns>
		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060033E0 RID: 13280
		int AddressField2 { get; }

		/// <summary>Gets the third address of a variable.</summary>
		/// <returns>The third address of the variable.</returns>
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060033E1 RID: 13281
		int AddressField3 { get; }

		/// <summary>Gets the start offset of the variable within the scope of the variable.</summary>
		/// <returns>The start offset of the variable.</returns>
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060033E2 RID: 13282
		int StartOffset { get; }

		/// <summary>Gets the end offset of a variable within the scope of the variable.</summary>
		/// <returns>The end offset of the variable.</returns>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060033E3 RID: 13283
		int EndOffset { get; }
	}
}
