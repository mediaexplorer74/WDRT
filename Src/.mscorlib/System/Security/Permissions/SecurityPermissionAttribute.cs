using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.SecurityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020002F6 RID: 758
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SecurityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x060026CD RID: 9933 RVA: 0x0008E1F5 File Offset: 0x0008C3F5
		public SecurityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets all permission flags comprising the <see cref="T:System.Security.Permissions.SecurityPermission" /> permissions.</summary>
		/// <returns>One or more of the <see cref="T:System.Security.Permissions.SecurityPermissionFlag" /> values combined using a bitwise OR.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.SecurityPermissionFlag" /> for the valid values.</exception>
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x0008E1FE File Offset: 0x0008C3FE
		// (set) Token: 0x060026CF RID: 9935 RVA: 0x0008E206 File Offset: 0x0008C406
		public SecurityPermissionFlag Flags
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

		/// <summary>Gets or sets a value indicating whether permission to assert that all this code's callers have the requisite permission for the operation is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if permission to assert is declared; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x0008E20F File Offset: 0x0008C40F
		// (set) Token: 0x060026D1 RID: 9937 RVA: 0x0008E21C File Offset: 0x0008C41C
		public bool Assertion
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.Assertion) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.Assertion) : (this.m_flag & ~SecurityPermissionFlag.Assertion));
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to call unmanaged code is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if permission to call unmanaged code is declared; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060026D2 RID: 9938 RVA: 0x0008E23A File Offset: 0x0008C43A
		// (set) Token: 0x060026D3 RID: 9939 RVA: 0x0008E247 File Offset: 0x0008C447
		public bool UnmanagedCode
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.UnmanagedCode) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.UnmanagedCode) : (this.m_flag & ~SecurityPermissionFlag.UnmanagedCode));
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to bypass code verification is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if permission to bypass code verification is declared; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x0008E265 File Offset: 0x0008C465
		// (set) Token: 0x060026D5 RID: 9941 RVA: 0x0008E272 File Offset: 0x0008C472
		public bool SkipVerification
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.SkipVerification) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.SkipVerification) : (this.m_flag & ~SecurityPermissionFlag.SkipVerification));
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to execute code is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if permission to execute code is declared; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x0008E290 File Offset: 0x0008C490
		// (set) Token: 0x060026D7 RID: 9943 RVA: 0x0008E29D File Offset: 0x0008C49D
		public bool Execution
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.Execution) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.Execution) : (this.m_flag & ~SecurityPermissionFlag.Execution));
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to manipulate threads is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if permission to manipulate threads is declared; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x0008E2BB File Offset: 0x0008C4BB
		// (set) Token: 0x060026D9 RID: 9945 RVA: 0x0008E2C9 File Offset: 0x0008C4C9
		public bool ControlThread
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlThread) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlThread) : (this.m_flag & ~SecurityPermissionFlag.ControlThread));
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to alter or manipulate evidence is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if the ability to alter or manipulate evidence is declared; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x0008E2E8 File Offset: 0x0008C4E8
		// (set) Token: 0x060026DB RID: 9947 RVA: 0x0008E2F6 File Offset: 0x0008C4F6
		public bool ControlEvidence
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlEvidence) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlEvidence) : (this.m_flag & ~SecurityPermissionFlag.ControlEvidence));
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to view and manipulate security policy is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if permission to manipulate security policy is declared; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x0008E315 File Offset: 0x0008C515
		// (set) Token: 0x060026DD RID: 9949 RVA: 0x0008E323 File Offset: 0x0008C523
		public bool ControlPolicy
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlPolicy) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlPolicy) : (this.m_flag & ~SecurityPermissionFlag.ControlPolicy));
			}
		}

		/// <summary>Gets or sets a value indicating whether code can use a serialization formatter to serialize or deserialize an object.</summary>
		/// <returns>
		///   <see langword="true" /> if code can use a serialization formatter to serialize or deserialize an object; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x0008E342 File Offset: 0x0008C542
		// (set) Token: 0x060026DF RID: 9951 RVA: 0x0008E353 File Offset: 0x0008C553
		public bool SerializationFormatter
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.SerializationFormatter) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.SerializationFormatter) : (this.m_flag & ~SecurityPermissionFlag.SerializationFormatter));
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to alter or manipulate domain security policy is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if permission to alter or manipulate security policy in an application domain is declared; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060026E0 RID: 9952 RVA: 0x0008E378 File Offset: 0x0008C578
		// (set) Token: 0x060026E1 RID: 9953 RVA: 0x0008E389 File Offset: 0x0008C589
		public bool ControlDomainPolicy
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlDomainPolicy) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlDomainPolicy) : (this.m_flag & ~SecurityPermissionFlag.ControlDomainPolicy));
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to manipulate the current principal is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if permission to manipulate the current principal is declared; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060026E2 RID: 9954 RVA: 0x0008E3AE File Offset: 0x0008C5AE
		// (set) Token: 0x060026E3 RID: 9955 RVA: 0x0008E3BF File Offset: 0x0008C5BF
		public bool ControlPrincipal
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlPrincipal) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlPrincipal) : (this.m_flag & ~SecurityPermissionFlag.ControlPrincipal));
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to manipulate <see cref="T:System.AppDomain" /> is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if permission to manipulate <see cref="T:System.AppDomain" /> is declared; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060026E4 RID: 9956 RVA: 0x0008E3E4 File Offset: 0x0008C5E4
		// (set) Token: 0x060026E5 RID: 9957 RVA: 0x0008E3F5 File Offset: 0x0008C5F5
		public bool ControlAppDomain
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlAppDomain) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlAppDomain) : (this.m_flag & ~SecurityPermissionFlag.ControlAppDomain));
			}
		}

		/// <summary>Gets or sets a value indicating whether code can configure remoting types and channels.</summary>
		/// <returns>
		///   <see langword="true" /> if code can configure remoting types and channels; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x0008E41A File Offset: 0x0008C61A
		// (set) Token: 0x060026E7 RID: 9959 RVA: 0x0008E42B File Offset: 0x0008C62B
		public bool RemotingConfiguration
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.RemotingConfiguration) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.RemotingConfiguration) : (this.m_flag & ~SecurityPermissionFlag.RemotingConfiguration));
			}
		}

		/// <summary>Gets or sets a value indicating whether code can plug into the common language runtime infrastructure, such as adding Remoting Context Sinks, Envoy Sinks and Dynamic Sinks.</summary>
		/// <returns>
		///   <see langword="true" /> if code can plug into the common language runtime infrastructure; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x0008E450 File Offset: 0x0008C650
		// (set) Token: 0x060026E9 RID: 9961 RVA: 0x0008E461 File Offset: 0x0008C661
		[ComVisible(true)]
		public bool Infrastructure
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.Infrastructure) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.Infrastructure) : (this.m_flag & ~SecurityPermissionFlag.Infrastructure));
			}
		}

		/// <summary>Gets or sets a value that indicates whether code has permission to perform binding redirection in the application configuration file.</summary>
		/// <returns>
		///   <see langword="true" /> if code can perform binding redirects; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060026EA RID: 9962 RVA: 0x0008E486 File Offset: 0x0008C686
		// (set) Token: 0x060026EB RID: 9963 RVA: 0x0008E497 File Offset: 0x0008C697
		public bool BindingRedirects
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.BindingRedirects) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.BindingRedirects) : (this.m_flag & ~SecurityPermissionFlag.BindingRedirects));
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.SecurityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.SecurityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x060026EC RID: 9964 RVA: 0x0008E4BC File Offset: 0x0008C6BC
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			return new SecurityPermission(this.m_flag);
		}

		// Token: 0x04000F0A RID: 3850
		private SecurityPermissionFlag m_flag;
	}
}
