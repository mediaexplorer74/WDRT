using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.ProcessModule" /> objects.</summary>
	// Token: 0x020004FC RID: 1276
	public class ProcessModuleCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessModuleCollection" /> class, with no associated <see cref="T:System.Diagnostics.ProcessModule" /> instances.</summary>
		// Token: 0x06003053 RID: 12371 RVA: 0x000DB1BD File Offset: 0x000D93BD
		protected ProcessModuleCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessModuleCollection" /> class, using the specified array of <see cref="T:System.Diagnostics.ProcessModule" /> instances.</summary>
		/// <param name="processModules">An array of <see cref="T:System.Diagnostics.ProcessModule" /> instances with which to initialize this <see cref="T:System.Diagnostics.ProcessModuleCollection" /> instance.</param>
		// Token: 0x06003054 RID: 12372 RVA: 0x000DB1C5 File Offset: 0x000D93C5
		public ProcessModuleCollection(ProcessModule[] processModules)
		{
			base.InnerList.AddRange(processModules);
		}

		/// <summary>Gets an index for iterating over the set of process modules.</summary>
		/// <param name="index">The zero-based index value of the module in the collection.</param>
		/// <returns>A <see cref="T:System.Diagnostics.ProcessModule" /> that indexes the modules in the collection</returns>
		// Token: 0x17000BCB RID: 3019
		public ProcessModule this[int index]
		{
			get
			{
				return (ProcessModule)base.InnerList[index];
			}
		}

		/// <summary>Provides the location of a specified module within the collection.</summary>
		/// <param name="module">The <see cref="T:System.Diagnostics.ProcessModule" /> whose index is retrieved.</param>
		/// <returns>The zero-based index that defines the location of the module within the <see cref="T:System.Diagnostics.ProcessModuleCollection" />.</returns>
		// Token: 0x06003056 RID: 12374 RVA: 0x000DB1EC File Offset: 0x000D93EC
		public int IndexOf(ProcessModule module)
		{
			return base.InnerList.IndexOf(module);
		}

		/// <summary>Determines whether the specified process module exists in the collection.</summary>
		/// <param name="module">A <see cref="T:System.Diagnostics.ProcessModule" /> instance that indicates the module to find in this collection.</param>
		/// <returns>
		///   <see langword="true" /> if the module exists in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003057 RID: 12375 RVA: 0x000DB1FA File Offset: 0x000D93FA
		public bool Contains(ProcessModule module)
		{
			return base.InnerList.Contains(module);
		}

		/// <summary>Copies an array of <see cref="T:System.Diagnostics.ProcessModule" /> instances to the collection, at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Diagnostics.ProcessModule" /> instances to add to the collection.</param>
		/// <param name="index">The location at which to add the new instances.</param>
		// Token: 0x06003058 RID: 12376 RVA: 0x000DB208 File Offset: 0x000D9408
		public void CopyTo(ProcessModule[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}
	}
}
