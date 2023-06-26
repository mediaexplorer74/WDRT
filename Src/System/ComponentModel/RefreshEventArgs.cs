using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.TypeDescriptor.Refreshed" /> event.</summary>
	// Token: 0x020005A4 RID: 1444
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class RefreshEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshEventArgs" /> class with the component that has changed.</summary>
		/// <param name="componentChanged">The component that changed.</param>
		// Token: 0x060035EF RID: 13807 RVA: 0x000EBBF5 File Offset: 0x000E9DF5
		public RefreshEventArgs(object componentChanged)
		{
			this.componentChanged = componentChanged;
			this.typeChanged = componentChanged.GetType();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshEventArgs" /> class with the type of component that has changed.</summary>
		/// <param name="typeChanged">The <see cref="T:System.Type" /> that changed.</param>
		// Token: 0x060035F0 RID: 13808 RVA: 0x000EBC10 File Offset: 0x000E9E10
		public RefreshEventArgs(Type typeChanged)
		{
			this.typeChanged = typeChanged;
		}

		/// <summary>Gets the component that changed its properties, events, or extenders.</summary>
		/// <returns>The component that changed its properties, events, or extenders, or <see langword="null" /> if all components of the same type have changed.</returns>
		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x060035F1 RID: 13809 RVA: 0x000EBC1F File Offset: 0x000E9E1F
		public object ComponentChanged
		{
			get
			{
				return this.componentChanged;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that changed its properties or events.</summary>
		/// <returns>The <see cref="T:System.Type" /> that changed its properties or events.</returns>
		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x060035F2 RID: 13810 RVA: 0x000EBC27 File Offset: 0x000E9E27
		public Type TypeChanged
		{
			get
			{
				return this.typeChanged;
			}
		}

		// Token: 0x04002A78 RID: 10872
		private object componentChanged;

		// Token: 0x04002A79 RID: 10873
		private Type typeChanged;
	}
}
