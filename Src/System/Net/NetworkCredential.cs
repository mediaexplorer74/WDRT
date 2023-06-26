using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Provides credentials for password-based authentication schemes such as basic, digest, NTLM, and Kerberos authentication.</summary>
	// Token: 0x02000155 RID: 341
	[global::__DynamicallyInvokable]
	public class NetworkCredential : ICredentials, ICredentialsByHost
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class.</summary>
		// Token: 0x06000BED RID: 3053 RVA: 0x00040D77 File Offset: 0x0003EF77
		[global::__DynamicallyInvokable]
		public NetworkCredential()
			: this(string.Empty, string.Empty, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name and password.</summary>
		/// <param name="userName">The user name associated with the credentials.</param>
		/// <param name="password">The password for the user name associated with the credentials.</param>
		// Token: 0x06000BEE RID: 3054 RVA: 0x00040D8E File Offset: 0x0003EF8E
		[global::__DynamicallyInvokable]
		public NetworkCredential(string userName, string password)
			: this(userName, password, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name and password.</summary>
		/// <param name="userName">The user name associated with the credentials.</param>
		/// <param name="password">The password for the user name associated with the credentials.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.SecureString" /> class is not supported on this platform.</exception>
		// Token: 0x06000BEF RID: 3055 RVA: 0x00040D9D File Offset: 0x0003EF9D
		public NetworkCredential(string userName, SecureString password)
			: this(userName, password, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name, password, and domain.</summary>
		/// <param name="userName">The user name associated with the credentials.</param>
		/// <param name="password">The password for the user name associated with the credentials.</param>
		/// <param name="domain">The domain associated with these credentials.</param>
		// Token: 0x06000BF0 RID: 3056 RVA: 0x00040DAC File Offset: 0x0003EFAC
		[global::__DynamicallyInvokable]
		public NetworkCredential(string userName, string password, string domain)
		{
			this.UserName = userName;
			this.Password = password;
			this.Domain = domain;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name, password, and domain.</summary>
		/// <param name="userName">The user name associated with the credentials.</param>
		/// <param name="password">The password for the user name associated with the credentials.</param>
		/// <param name="domain">The domain associated with these credentials.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.SecureString" /> class is not supported on this platform.</exception>
		// Token: 0x06000BF1 RID: 3057 RVA: 0x00040DC9 File Offset: 0x0003EFC9
		public NetworkCredential(string userName, SecureString password, string domain)
		{
			this.UserName = userName;
			this.SecurePassword = password;
			this.Domain = domain;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00040DE8 File Offset: 0x0003EFE8
		private void InitializePart1()
		{
			if (NetworkCredential.m_environmentUserNamePermission == null)
			{
				object obj = NetworkCredential.lockingObject;
				lock (obj)
				{
					if (NetworkCredential.m_environmentUserNamePermission == null)
					{
						NetworkCredential.m_environmentDomainNamePermission = new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERDOMAIN");
						NetworkCredential.m_environmentUserNamePermission = new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME");
					}
				}
			}
		}

		/// <summary>Gets or sets the user name associated with the credentials.</summary>
		/// <returns>The user name associated with the credentials.</returns>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00040E58 File Offset: 0x0003F058
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x00040E72 File Offset: 0x0003F072
		[global::__DynamicallyInvokable]
		public string UserName
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.InitializePart1();
				NetworkCredential.m_environmentUserNamePermission.Demand();
				return this.InternalGetUserName();
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					this.m_userName = string.Empty;
					return;
				}
				this.m_userName = value;
			}
		}

		/// <summary>Gets or sets the password for the user name associated with the credentials.</summary>
		/// <returns>The password associated with the credentials. If this <see cref="T:System.Net.NetworkCredential" /> instance was initialized with the <paramref name="password" /> parameter set to <see langword="null" />, then the <see cref="P:System.Net.NetworkCredential.Password" /> property will return an empty string.</returns>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00040E8A File Offset: 0x0003F08A
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x00040E9C File Offset: 0x0003F09C
		[global::__DynamicallyInvokable]
		public string Password
		{
			[global::__DynamicallyInvokable]
			get
			{
				ExceptionHelper.UnmanagedPermission.Demand();
				return this.InternalGetPassword();
			}
			[global::__DynamicallyInvokable]
			set
			{
				this.m_password = UnsafeNclNativeMethods.SecureStringHelper.CreateSecureString(value);
			}
		}

		/// <summary>Gets or sets the password as a <see cref="T:System.Security.SecureString" /> instance.</summary>
		/// <returns>The password for the user name associated with the credentials.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.SecureString" /> class is not supported on this platform.</exception>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00040EAA File Offset: 0x0003F0AA
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x00040EC1 File Offset: 0x0003F0C1
		public SecureString SecurePassword
		{
			get
			{
				ExceptionHelper.UnmanagedPermission.Demand();
				return this.InternalGetSecurePassword().Copy();
			}
			set
			{
				if (value == null)
				{
					this.m_password = new SecureString();
					return;
				}
				this.m_password = value.Copy();
			}
		}

		/// <summary>Gets or sets the domain or computer name that verifies the credentials.</summary>
		/// <returns>The name of the domain associated with the credentials.</returns>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00040EDE File Offset: 0x0003F0DE
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x00040EF8 File Offset: 0x0003F0F8
		[global::__DynamicallyInvokable]
		public string Domain
		{
			[global::__DynamicallyInvokable]
			get
			{
				this.InitializePart1();
				NetworkCredential.m_environmentDomainNamePermission.Demand();
				return this.InternalGetDomain();
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					this.m_domain = string.Empty;
					return;
				}
				this.m_domain = value;
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00040F10 File Offset: 0x0003F110
		internal string InternalGetUserName()
		{
			return this.m_userName;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00040F18 File Offset: 0x0003F118
		internal string InternalGetPassword()
		{
			return UnsafeNclNativeMethods.SecureStringHelper.CreateString(this.m_password);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00040F32 File Offset: 0x0003F132
		internal SecureString InternalGetSecurePassword()
		{
			return this.m_password;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00040F3A File Offset: 0x0003F13A
		internal string InternalGetDomain()
		{
			return this.m_domain;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00040F44 File Offset: 0x0003F144
		internal string InternalGetDomainUserName()
		{
			string text = this.InternalGetDomain();
			if (text.Length != 0)
			{
				text += "\\";
			}
			return text + this.InternalGetUserName();
		}

		/// <summary>Returns an instance of the <see cref="T:System.Net.NetworkCredential" /> class for the specified Uniform Resource Identifier (URI) and authentication type.</summary>
		/// <param name="uri">The URI that the client provides authentication for.</param>
		/// <param name="authType">The type of authentication requested, as defined in the <see cref="P:System.Net.IAuthenticationModule.AuthenticationType" /> property.</param>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> object.</returns>
		// Token: 0x06000C00 RID: 3072 RVA: 0x00040F7A File Offset: 0x0003F17A
		[global::__DynamicallyInvokable]
		public NetworkCredential GetCredential(Uri uri, string authType)
		{
			return this;
		}

		/// <summary>Returns an instance of the <see cref="T:System.Net.NetworkCredential" /> class for the specified host, port, and authentication type.</summary>
		/// <param name="host">The host computer that authenticates the client.</param>
		/// <param name="port">The port on the <paramref name="host" /> that the client communicates with.</param>
		/// <param name="authenticationType">The type of authentication requested, as defined in the <see cref="P:System.Net.IAuthenticationModule.AuthenticationType" /> property.</param>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> for the specified host, port, and authentication protocol, or <see langword="null" /> if there are no credentials available for the specified host, port, and authentication protocol.</returns>
		// Token: 0x06000C01 RID: 3073 RVA: 0x00040F7D File Offset: 0x0003F17D
		[global::__DynamicallyInvokable]
		public NetworkCredential GetCredential(string host, int port, string authenticationType)
		{
			return this;
		}

		// Token: 0x0400112F RID: 4399
		private static volatile EnvironmentPermission m_environmentUserNamePermission;

		// Token: 0x04001130 RID: 4400
		private static volatile EnvironmentPermission m_environmentDomainNamePermission;

		// Token: 0x04001131 RID: 4401
		private static readonly object lockingObject = new object();

		// Token: 0x04001132 RID: 4402
		private string m_domain;

		// Token: 0x04001133 RID: 4403
		private string m_userName;

		// Token: 0x04001134 RID: 4404
		private SecureString m_password;
	}
}
