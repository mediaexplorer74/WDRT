using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event. This class cannot be inherited.</summary>
	// Token: 0x020005CD RID: 1485
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ComponentChangingEventArgs : EventArgs
	{
		/// <summary>Gets the component that is about to be changed or the component that is the parent container of the member that is about to be changed.</summary>
		/// <returns>The component that is about to have a member changed.</returns>
		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x0600375A RID: 14170 RVA: 0x000EFC2F File Offset: 0x000EDE2F
		public object Component
		{
			get
			{
				return this.component;
			}
		}

		/// <summary>Gets the member that is about to be changed.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.MemberDescriptor" /> indicating the member that is about to be changed, if known, or <see langword="null" /> otherwise.</returns>
		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x0600375B RID: 14171 RVA: 0x000EFC37 File Offset: 0x000EDE37
		public MemberDescriptor Member
		{
			get
			{
				return this.member;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentChangingEventArgs" /> class.</summary>
		/// <param name="component">The component that is about to be changed.</param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> indicating the member of the component that is about to be changed.</param>
		// Token: 0x0600375C RID: 14172 RVA: 0x000EFC3F File Offset: 0x000EDE3F
		public ComponentChangingEventArgs(object component, MemberDescriptor member)
		{
			this.component = component;
			this.member = member;
		}

		// Token: 0x04002AD8 RID: 10968
		private object component;

		// Token: 0x04002AD9 RID: 10969
		private MemberDescriptor member;
	}
}
