using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Provides data for loader resolution events, such as the <see cref="E:System.AppDomain.TypeResolve" />, <see cref="E:System.AppDomain.ResourceResolve" />, <see cref="E:System.AppDomain.ReflectionOnlyAssemblyResolve" />, and <see cref="E:System.AppDomain.AssemblyResolve" /> events.</summary>
	// Token: 0x02000091 RID: 145
	[ComVisible(true)]
	public class ResolveEventArgs : EventArgs
	{
		/// <summary>Gets the name of the item to resolve.</summary>
		/// <returns>The name of the item to resolve.</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00019FDF File Offset: 0x000181DF
		public string Name
		{
			get
			{
				return this._Name;
			}
		}

		/// <summary>Gets the assembly whose dependency is being resolved.</summary>
		/// <returns>The assembly that requested the item specified by the <see cref="P:System.ResolveEventArgs.Name" /> property.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x00019FE7 File Offset: 0x000181E7
		public Assembly RequestingAssembly
		{
			get
			{
				return this._RequestingAssembly;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ResolveEventArgs" /> class, specifying the name of the item to resolve.</summary>
		/// <param name="name">The name of an item to resolve.</param>
		// Token: 0x06000773 RID: 1907 RVA: 0x00019FEF File Offset: 0x000181EF
		public ResolveEventArgs(string name)
		{
			this._Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ResolveEventArgs" /> class, specifying the name of the item to resolve and the assembly whose dependency is being resolved.</summary>
		/// <param name="name">The name of an item to resolve.</param>
		/// <param name="requestingAssembly">The assembly whose dependency is being resolved.</param>
		// Token: 0x06000774 RID: 1908 RVA: 0x00019FFE File Offset: 0x000181FE
		public ResolveEventArgs(string name, Assembly requestingAssembly)
		{
			this._Name = name;
			this._RequestingAssembly = requestingAssembly;
		}

		// Token: 0x0400037A RID: 890
		private string _Name;

		// Token: 0x0400037B RID: 891
		private Assembly _RequestingAssembly;
	}
}
