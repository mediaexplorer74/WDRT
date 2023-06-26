using System;
using System.Collections.ObjectModel;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Provides data for the <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.DesignerNamespaceResolve" /> event.</summary>
	// Token: 0x020009F7 RID: 2551
	[ComVisible(false)]
	public class DesignerNamespaceResolveEventArgs : EventArgs
	{
		/// <summary>Gets the name of the namespace to resolve.</summary>
		/// <returns>The name of the namespace to resolve.</returns>
		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06006500 RID: 25856 RVA: 0x001593ED File Offset: 0x001575ED
		public string NamespaceName
		{
			get
			{
				return this._NamespaceName;
			}
		}

		/// <summary>Gets a collection of assembly file paths; when the event handler for the <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.DesignerNamespaceResolve" /> event is invoked, the collection is empty, and the event handler is responsible for adding the necessary assembly files.</summary>
		/// <returns>A collection of assembly files that define the requested namespace.</returns>
		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06006501 RID: 25857 RVA: 0x001593F5 File Offset: 0x001575F5
		public Collection<string> ResolvedAssemblyFiles
		{
			get
			{
				return this._ResolvedAssemblyFiles;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.DesignerNamespaceResolveEventArgs" /> class.</summary>
		/// <param name="namespaceName">The name of the namespace to resolve.</param>
		// Token: 0x06006502 RID: 25858 RVA: 0x001593FD File Offset: 0x001575FD
		public DesignerNamespaceResolveEventArgs(string namespaceName)
		{
			this._NamespaceName = namespaceName;
			this._ResolvedAssemblyFiles = new Collection<string>();
		}

		// Token: 0x04002D37 RID: 11575
		private string _NamespaceName;

		// Token: 0x04002D38 RID: 11576
		private Collection<string> _ResolvedAssemblyFiles;
	}
}
