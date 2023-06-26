using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Provides data for the <see cref="E:System.AppDomain.AssemblyLoad" /> event.</summary>
	// Token: 0x02000092 RID: 146
	[ComVisible(true)]
	public class AssemblyLoadEventArgs : EventArgs
	{
		/// <summary>Gets an <see cref="T:System.Reflection.Assembly" /> that represents the currently loaded assembly.</summary>
		/// <returns>An instance of <see cref="T:System.Reflection.Assembly" /> that represents the currently loaded assembly.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0001A014 File Offset: 0x00018214
		public Assembly LoadedAssembly
		{
			get
			{
				return this._LoadedAssembly;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AssemblyLoadEventArgs" /> class using the specified <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <param name="loadedAssembly">An instance that represents the currently loaded assembly.</param>
		// Token: 0x06000776 RID: 1910 RVA: 0x0001A01C File Offset: 0x0001821C
		public AssemblyLoadEventArgs(Assembly loadedAssembly)
		{
			this._LoadedAssembly = loadedAssembly;
		}

		// Token: 0x0400037C RID: 892
		private Assembly _LoadedAssembly;
	}
}
