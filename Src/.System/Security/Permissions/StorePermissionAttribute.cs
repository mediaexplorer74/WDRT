using System;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.StorePermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000484 RID: 1156
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class StorePermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StorePermissionAttribute" /> class with the specified security action.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002AD8 RID: 10968 RVA: 0x000C3306 File Offset: 0x000C1506
		public StorePermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the store permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.StorePermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.StorePermissionFlags.NoFlags" />.</returns>
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x000C330F File Offset: 0x000C150F
		// (set) Token: 0x06002ADA RID: 10970 RVA: 0x000C3317 File Offset: 0x000C1517
		public StorePermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				StorePermission.VerifyFlags(value);
				this.m_flags = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to create a store.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to create a store is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x000C3326 File Offset: 0x000C1526
		// (set) Token: 0x06002ADC RID: 10972 RVA: 0x000C3333 File Offset: 0x000C1533
		public bool CreateStore
		{
			get
			{
				return (this.m_flags & StorePermissionFlags.CreateStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | StorePermissionFlags.CreateStore) : (this.m_flags & ~StorePermissionFlags.CreateStore));
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to delete a store.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to delete a store is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x000C3351 File Offset: 0x000C1551
		// (set) Token: 0x06002ADE RID: 10974 RVA: 0x000C335E File Offset: 0x000C155E
		public bool DeleteStore
		{
			get
			{
				return (this.m_flags & StorePermissionFlags.DeleteStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | StorePermissionFlags.DeleteStore) : (this.m_flags & ~StorePermissionFlags.DeleteStore));
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to enumerate stores.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to enumerate stores is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06002ADF RID: 10975 RVA: 0x000C337C File Offset: 0x000C157C
		// (set) Token: 0x06002AE0 RID: 10976 RVA: 0x000C3389 File Offset: 0x000C1589
		public bool EnumerateStores
		{
			get
			{
				return (this.m_flags & StorePermissionFlags.EnumerateStores) > StorePermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | StorePermissionFlags.EnumerateStores) : (this.m_flags & ~StorePermissionFlags.EnumerateStores));
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to open a store.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to open a store is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x000C33A7 File Offset: 0x000C15A7
		// (set) Token: 0x06002AE2 RID: 10978 RVA: 0x000C33B5 File Offset: 0x000C15B5
		public bool OpenStore
		{
			get
			{
				return (this.m_flags & StorePermissionFlags.OpenStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | StorePermissionFlags.OpenStore) : (this.m_flags & ~StorePermissionFlags.OpenStore));
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to add to a store.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to add to a store is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x000C33D4 File Offset: 0x000C15D4
		// (set) Token: 0x06002AE4 RID: 10980 RVA: 0x000C33E2 File Offset: 0x000C15E2
		public bool AddToStore
		{
			get
			{
				return (this.m_flags & StorePermissionFlags.AddToStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | StorePermissionFlags.AddToStore) : (this.m_flags & ~StorePermissionFlags.AddToStore));
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to remove a certificate from a store.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to remove a certificate from a store is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x000C3401 File Offset: 0x000C1601
		// (set) Token: 0x06002AE6 RID: 10982 RVA: 0x000C340F File Offset: 0x000C160F
		public bool RemoveFromStore
		{
			get
			{
				return (this.m_flags & StorePermissionFlags.RemoveFromStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | StorePermissionFlags.RemoveFromStore) : (this.m_flags & ~StorePermissionFlags.RemoveFromStore));
			}
		}

		/// <summary>Gets or sets a value indicating whether the code is permitted to enumerate the certificates in a store.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to enumerate certificates is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x000C342E File Offset: 0x000C162E
		// (set) Token: 0x06002AE8 RID: 10984 RVA: 0x000C343F File Offset: 0x000C163F
		public bool EnumerateCertificates
		{
			get
			{
				return (this.m_flags & StorePermissionFlags.EnumerateCertificates) > StorePermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | StorePermissionFlags.EnumerateCertificates) : (this.m_flags & ~StorePermissionFlags.EnumerateCertificates));
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.StorePermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StorePermission" /> that corresponds to the attribute.</returns>
		// Token: 0x06002AE9 RID: 10985 RVA: 0x000C3464 File Offset: 0x000C1664
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new StorePermission(PermissionState.Unrestricted);
			}
			return new StorePermission(this.m_flags);
		}

		// Token: 0x04002646 RID: 9798
		private StorePermissionFlags m_flags;
	}
}
