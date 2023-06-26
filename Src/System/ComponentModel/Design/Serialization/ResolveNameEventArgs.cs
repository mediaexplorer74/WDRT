using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.Serialization.IDesignerSerializationManager.ResolveName" /> event.</summary>
	// Token: 0x02000611 RID: 1553
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ResolveNameEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.ResolveNameEventArgs" /> class.</summary>
		/// <param name="name">The name to resolve.</param>
		// Token: 0x060038D2 RID: 14546 RVA: 0x000F1932 File Offset: 0x000EFB32
		public ResolveNameEventArgs(string name)
		{
			this.name = name;
			this.value = null;
		}

		/// <summary>Gets the name of the object to resolve.</summary>
		/// <returns>The name of the object to resolve.</returns>
		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x060038D3 RID: 14547 RVA: 0x000F1948 File Offset: 0x000EFB48
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets the object that matches the name.</summary>
		/// <returns>The object that the name is associated with.</returns>
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000F1950 File Offset: 0x000EFB50
		// (set) Token: 0x060038D5 RID: 14549 RVA: 0x000F1958 File Offset: 0x000EFB58
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x04002B65 RID: 11109
		private string name;

		// Token: 0x04002B66 RID: 11110
		private object value;
	}
}
