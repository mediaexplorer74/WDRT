using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a basic designer loader interface that can be used to implement a custom designer loader.</summary>
	// Token: 0x02000605 RID: 1541
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class DesignerLoader
	{
		/// <summary>Gets a value indicating whether the loader is currently loading a document.</summary>
		/// <returns>
		///   <see langword="true" /> if the loader is currently loading a document; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06003892 RID: 14482 RVA: 0x000F132A File Offset: 0x000EF52A
		public virtual bool Loading
		{
			get
			{
				return false;
			}
		}

		/// <summary>Begins loading a designer.</summary>
		/// <param name="host">The loader host through which this loader loads components.</param>
		// Token: 0x06003893 RID: 14483
		public abstract void BeginLoad(IDesignerLoaderHost host);

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.Serialization.DesignerLoader" />.</summary>
		// Token: 0x06003894 RID: 14484
		public abstract void Dispose();

		/// <summary>Writes cached changes to the location that the designer was loaded from.</summary>
		// Token: 0x06003895 RID: 14485 RVA: 0x000F132D File Offset: 0x000EF52D
		public virtual void Flush()
		{
		}
	}
}
