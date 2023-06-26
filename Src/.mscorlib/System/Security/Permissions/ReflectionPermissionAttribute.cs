using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.ReflectionPermission" /> to be applied to code using declarative security.</summary>
	// Token: 0x020002F4 RID: 756
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ReflectionPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ReflectionPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x060026B1 RID: 9905 RVA: 0x0008DFD7 File Offset: 0x0008C1D7
		public ReflectionPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets the current allowed uses of reflection.</summary>
		/// <returns>One or more of the <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> values combined using a bitwise OR.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> for the valid values.</exception>
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x0008DFE0 File Offset: 0x0008C1E0
		// (set) Token: 0x060026B3 RID: 9907 RVA: 0x0008DFE8 File Offset: 0x0008C1E8
		public ReflectionPermissionFlag Flags
		{
			get
			{
				return this.m_flag;
			}
			set
			{
				this.m_flag = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether reflection on members that are not visible is allowed.</summary>
		/// <returns>
		///   <see langword="true" /> if reflection on members that are not visible is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x0008DFF1 File Offset: 0x0008C1F1
		// (set) Token: 0x060026B5 RID: 9909 RVA: 0x0008DFFE File Offset: 0x0008C1FE
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		public bool TypeInformation
		{
			get
			{
				return (this.m_flag & ReflectionPermissionFlag.TypeInformation) > ReflectionPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | ReflectionPermissionFlag.TypeInformation) : (this.m_flag & ~ReflectionPermissionFlag.TypeInformation));
			}
		}

		/// <summary>Gets or sets a value that indicates whether invocation of operations on non-public members is allowed.</summary>
		/// <returns>
		///   <see langword="true" /> if invocation of operations on non-public members is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x0008E01C File Offset: 0x0008C21C
		// (set) Token: 0x060026B7 RID: 9911 RVA: 0x0008E029 File Offset: 0x0008C229
		public bool MemberAccess
		{
			get
			{
				return (this.m_flag & ReflectionPermissionFlag.MemberAccess) > ReflectionPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | ReflectionPermissionFlag.MemberAccess) : (this.m_flag & ~ReflectionPermissionFlag.MemberAccess));
			}
		}

		/// <summary>Gets or sets a value that indicates whether use of certain features in <see cref="N:System.Reflection.Emit" />, such as emitting debug symbols, is allowed.</summary>
		/// <returns>
		///   <see langword="true" /> if use of the affected features is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x0008E047 File Offset: 0x0008C247
		// (set) Token: 0x060026B9 RID: 9913 RVA: 0x0008E054 File Offset: 0x0008C254
		[Obsolete("This permission is no longer used by the CLR.")]
		public bool ReflectionEmit
		{
			get
			{
				return (this.m_flag & ReflectionPermissionFlag.ReflectionEmit) > ReflectionPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | ReflectionPermissionFlag.ReflectionEmit) : (this.m_flag & ~ReflectionPermissionFlag.ReflectionEmit));
			}
		}

		/// <summary>Gets or sets a value that indicates whether restricted invocation of non-public members is allowed. Restricted invocation means that the grant set of the assembly that contains the non-public member that is being invoked must be equal to, or a subset of, the grant set of the invoking assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if restricted invocation of non-public members is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x0008E072 File Offset: 0x0008C272
		// (set) Token: 0x060026BB RID: 9915 RVA: 0x0008E07F File Offset: 0x0008C27F
		public bool RestrictedMemberAccess
		{
			get
			{
				return (this.m_flag & ReflectionPermissionFlag.RestrictedMemberAccess) > ReflectionPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | ReflectionPermissionFlag.RestrictedMemberAccess) : (this.m_flag & ~ReflectionPermissionFlag.RestrictedMemberAccess));
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.ReflectionPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.ReflectionPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x060026BC RID: 9916 RVA: 0x0008E09D File Offset: 0x0008C29D
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new ReflectionPermission(PermissionState.Unrestricted);
			}
			return new ReflectionPermission(this.m_flag);
		}

		// Token: 0x04000F04 RID: 3844
		private ReflectionPermissionFlag m_flag;
	}
}
