using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Provides data for the <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve" /> event.</summary>
	// Token: 0x020009F6 RID: 2550
	[ComVisible(false)]
	public class NamespaceResolveEventArgs : EventArgs
	{
		/// <summary>Gets the name of the namespace to resolve.</summary>
		/// <returns>The name of the namespace to resolve.</returns>
		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x060064FC RID: 25852 RVA: 0x001593B4 File Offset: 0x001575B4
		public string NamespaceName
		{
			get
			{
				return this._NamespaceName;
			}
		}

		/// <summary>Gets the name of the assembly whose dependency is being resolved.</summary>
		/// <returns>The name of the assembly whose dependency is being resolved.</returns>
		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x060064FD RID: 25853 RVA: 0x001593BC File Offset: 0x001575BC
		public Assembly RequestingAssembly
		{
			get
			{
				return this._RequestingAssembly;
			}
		}

		/// <summary>Gets a collection of assemblies; when the event handler for the <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve" /> event is invoked, the collection is empty, and the event handler is responsible for adding the necessary assemblies.</summary>
		/// <returns>A collection of assemblies that define the requested namespace.</returns>
		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x060064FE RID: 25854 RVA: 0x001593C4 File Offset: 0x001575C4
		public Collection<Assembly> ResolvedAssemblies
		{
			get
			{
				return this._ResolvedAssemblies;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.NamespaceResolveEventArgs" /> class, specifying the namespace to resolve and the assembly whose dependency is being resolved.</summary>
		/// <param name="namespaceName">The namespace to resolve.</param>
		/// <param name="requestingAssembly">The assembly whose dependency is being resolved.</param>
		// Token: 0x060064FF RID: 25855 RVA: 0x001593CC File Offset: 0x001575CC
		public NamespaceResolveEventArgs(string namespaceName, Assembly requestingAssembly)
		{
			this._NamespaceName = namespaceName;
			this._RequestingAssembly = requestingAssembly;
			this._ResolvedAssemblies = new Collection<Assembly>();
		}

		// Token: 0x04002D34 RID: 11572
		private string _NamespaceName;

		// Token: 0x04002D35 RID: 11573
		private Assembly _RequestingAssembly;

		// Token: 0x04002D36 RID: 11574
		private Collection<Assembly> _ResolvedAssemblies;
	}
}
