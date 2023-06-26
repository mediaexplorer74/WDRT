using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanged" /> event. This class cannot be inherited.</summary>
	// Token: 0x020005CB RID: 1483
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ComponentChangedEventArgs : EventArgs
	{
		/// <summary>Gets the component that was modified.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the component that was modified.</returns>
		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06003751 RID: 14161 RVA: 0x000EFBEA File Offset: 0x000EDDEA
		public object Component
		{
			get
			{
				return this.component;
			}
		}

		/// <summary>Gets the member that has been changed.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.MemberDescriptor" /> that indicates the member that has been changed.</returns>
		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06003752 RID: 14162 RVA: 0x000EFBF2 File Offset: 0x000EDDF2
		public MemberDescriptor Member
		{
			get
			{
				return this.member;
			}
		}

		/// <summary>Gets the new value of the changed member.</summary>
		/// <returns>The new value of the changed member. This property can be <see langword="null" />.</returns>
		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06003753 RID: 14163 RVA: 0x000EFBFA File Offset: 0x000EDDFA
		public object NewValue
		{
			get
			{
				return this.newValue;
			}
		}

		/// <summary>Gets the old value of the changed member.</summary>
		/// <returns>The old value of the changed member. This property can be <see langword="null" />.</returns>
		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06003754 RID: 14164 RVA: 0x000EFC02 File Offset: 0x000EDE02
		public object OldValue
		{
			get
			{
				return this.oldValue;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> class.</summary>
		/// <param name="component">The component that was changed.</param>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that represents the member that was changed.</param>
		/// <param name="oldValue">The old value of the changed member.</param>
		/// <param name="newValue">The new value of the changed member.</param>
		// Token: 0x06003755 RID: 14165 RVA: 0x000EFC0A File Offset: 0x000EDE0A
		public ComponentChangedEventArgs(object component, MemberDescriptor member, object oldValue, object newValue)
		{
			this.component = component;
			this.member = member;
			this.oldValue = oldValue;
			this.newValue = newValue;
		}

		// Token: 0x04002AD4 RID: 10964
		private object component;

		// Token: 0x04002AD5 RID: 10965
		private MemberDescriptor member;

		// Token: 0x04002AD6 RID: 10966
		private object oldValue;

		// Token: 0x04002AD7 RID: 10967
		private object newValue;
	}
}
