using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows the use of declarative security actions to determine host protection requirements. This class cannot be inherited.</summary>
	// Token: 0x020002E3 RID: 739
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class HostProtectionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.HostProtectionAttribute" /> class with default values.</summary>
		// Token: 0x06002634 RID: 9780 RVA: 0x0008CFE7 File Offset: 0x0008B1E7
		public HostProtectionAttribute()
			: base(SecurityAction.LinkDemand)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.HostProtectionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" /> value.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not <see cref="F:System.Security.Permissions.SecurityAction.LinkDemand" />.</exception>
		// Token: 0x06002635 RID: 9781 RVA: 0x0008CFF0 File Offset: 0x0008B1F0
		public HostProtectionAttribute(SecurityAction action)
			: base(action)
		{
			if (action != SecurityAction.LinkDemand)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"));
			}
		}

		/// <summary>Gets or sets flags specifying categories of functionality that are potentially harmful to the host.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.HostProtectionResource" /> values. The default is <see cref="F:System.Security.Permissions.HostProtectionResource.None" />.</returns>
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x0008D00D File Offset: 0x0008B20D
		// (set) Token: 0x06002637 RID: 9783 RVA: 0x0008D015 File Offset: 0x0008B215
		public HostProtectionResource Resources
		{
			get
			{
				return this.m_resources;
			}
			set
			{
				this.m_resources = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether synchronization is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if synchronization is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x0008D01E File Offset: 0x0008B21E
		// (set) Token: 0x06002639 RID: 9785 RVA: 0x0008D02B File Offset: 0x0008B22B
		public bool Synchronization
		{
			get
			{
				return (this.m_resources & HostProtectionResource.Synchronization) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.Synchronization) : (this.m_resources & ~HostProtectionResource.Synchronization));
			}
		}

		/// <summary>Gets or sets a value indicating whether shared state is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if shared state is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x0008D049 File Offset: 0x0008B249
		// (set) Token: 0x0600263B RID: 9787 RVA: 0x0008D056 File Offset: 0x0008B256
		public bool SharedState
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SharedState) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SharedState) : (this.m_resources & ~HostProtectionResource.SharedState));
			}
		}

		/// <summary>Gets or sets a value indicating whether external process management is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if external process management is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x0008D074 File Offset: 0x0008B274
		// (set) Token: 0x0600263D RID: 9789 RVA: 0x0008D081 File Offset: 0x0008B281
		public bool ExternalProcessMgmt
		{
			get
			{
				return (this.m_resources & HostProtectionResource.ExternalProcessMgmt) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.ExternalProcessMgmt) : (this.m_resources & ~HostProtectionResource.ExternalProcessMgmt));
			}
		}

		/// <summary>Gets or sets a value indicating whether self-affecting process management is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if self-affecting process management is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x0008D09F File Offset: 0x0008B29F
		// (set) Token: 0x0600263F RID: 9791 RVA: 0x0008D0AC File Offset: 0x0008B2AC
		public bool SelfAffectingProcessMgmt
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SelfAffectingProcessMgmt) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SelfAffectingProcessMgmt) : (this.m_resources & ~HostProtectionResource.SelfAffectingProcessMgmt));
			}
		}

		/// <summary>Gets or sets a value indicating whether external threading is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if external threading is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x0008D0CA File Offset: 0x0008B2CA
		// (set) Token: 0x06002641 RID: 9793 RVA: 0x0008D0D8 File Offset: 0x0008B2D8
		public bool ExternalThreading
		{
			get
			{
				return (this.m_resources & HostProtectionResource.ExternalThreading) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.ExternalThreading) : (this.m_resources & ~HostProtectionResource.ExternalThreading));
			}
		}

		/// <summary>Gets or sets a value indicating whether self-affecting threading is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if self-affecting threading is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06002642 RID: 9794 RVA: 0x0008D0F7 File Offset: 0x0008B2F7
		// (set) Token: 0x06002643 RID: 9795 RVA: 0x0008D105 File Offset: 0x0008B305
		public bool SelfAffectingThreading
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SelfAffectingThreading) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SelfAffectingThreading) : (this.m_resources & ~HostProtectionResource.SelfAffectingThreading));
			}
		}

		/// <summary>Gets or sets a value indicating whether the security infrastructure is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if the security infrastructure is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x0008D124 File Offset: 0x0008B324
		// (set) Token: 0x06002645 RID: 9797 RVA: 0x0008D132 File Offset: 0x0008B332
		[ComVisible(true)]
		public bool SecurityInfrastructure
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SecurityInfrastructure) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SecurityInfrastructure) : (this.m_resources & ~HostProtectionResource.SecurityInfrastructure));
			}
		}

		/// <summary>Gets or sets a value indicating whether the user interface is exposed.</summary>
		/// <returns>
		///   <see langword="true" /> if the user interface is exposed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06002646 RID: 9798 RVA: 0x0008D151 File Offset: 0x0008B351
		// (set) Token: 0x06002647 RID: 9799 RVA: 0x0008D162 File Offset: 0x0008B362
		public bool UI
		{
			get
			{
				return (this.m_resources & HostProtectionResource.UI) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.UI) : (this.m_resources & ~HostProtectionResource.UI));
			}
		}

		/// <summary>Gets or sets a value indicating whether resources might leak memory if the operation is terminated.</summary>
		/// <returns>
		///   <see langword="true" /> if resources might leak memory on termination; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06002648 RID: 9800 RVA: 0x0008D187 File Offset: 0x0008B387
		// (set) Token: 0x06002649 RID: 9801 RVA: 0x0008D198 File Offset: 0x0008B398
		public bool MayLeakOnAbort
		{
			get
			{
				return (this.m_resources & HostProtectionResource.MayLeakOnAbort) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.MayLeakOnAbort) : (this.m_resources & ~HostProtectionResource.MayLeakOnAbort));
			}
		}

		/// <summary>Creates and returns a new host protection permission.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that corresponds to the current attribute.</returns>
		// Token: 0x0600264A RID: 9802 RVA: 0x0008D1BD File Offset: 0x0008B3BD
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new HostProtectionPermission(PermissionState.Unrestricted);
			}
			return new HostProtectionPermission(this.m_resources);
		}

		// Token: 0x04000EA5 RID: 3749
		private HostProtectionResource m_resources;
	}
}
