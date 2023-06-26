using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Principal
{
	/// <summary>Represents a Windows user.</summary>
	// Token: 0x0200032A RID: 810
	[ComVisible(true)]
	[Serializable]
	public class WindowsIdentity : ClaimsIdentity, ISerializable, IDeserializationCallback, IDisposable
	{
		// Token: 0x060028BA RID: 10426 RVA: 0x00096731 File Offset: 0x00094931
		[SecurityCritical]
		private WindowsIdentity()
			: base(null, null, null, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid")
		{
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x0009676E File Offset: 0x0009496E
		[SecurityCritical]
		internal WindowsIdentity(SafeAccessTokenHandle safeTokenHandle)
			: this(safeTokenHandle.DangerousGetHandle(), null, -1)
		{
			GC.KeepAlive(safeTokenHandle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified Windows account token.</summary>
		/// <param name="userToken">The account token for the user on whose behalf the code is running.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="userToken" /> is 0.  
		/// -or-  
		/// <paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions.  
		///  -or-  
		///  A Win32 error occurred.</exception>
		// Token: 0x060028BC RID: 10428 RVA: 0x00096784 File Offset: 0x00094984
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public WindowsIdentity(IntPtr userToken)
			: this(userToken, null, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified Windows account token and the specified authentication type.</summary>
		/// <param name="userToken">The account token for the user on whose behalf the code is running.</param>
		/// <param name="type">(Informational use only.) The type of authentication used to identify the user.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="userToken" /> is 0.  
		/// -or-  
		/// <paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions.  
		///  -or-  
		///  A Win32 error occurred.</exception>
		// Token: 0x060028BD RID: 10429 RVA: 0x0009678F File Offset: 0x0009498F
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public WindowsIdentity(IntPtr userToken, string type)
			: this(userToken, type, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified Windows account token, the specified authentication type, and the specified Windows account type.</summary>
		/// <param name="userToken">The account token for the user on whose behalf the code is running.</param>
		/// <param name="type">(Informational use only.) The type of authentication used to identify the user.</param>
		/// <param name="acctType">One of the enumeration values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="userToken" /> is 0.  
		/// -or-  
		/// <paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions.  
		///  -or-  
		///  A Win32 error occurred.</exception>
		// Token: 0x060028BE RID: 10430 RVA: 0x0009679A File Offset: 0x0009499A
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType)
			: this(userToken, type, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified Windows account token, the specified authentication type, the specified Windows account type, and the specified authentication status.</summary>
		/// <param name="userToken">The account token for the user on whose behalf the code is running.</param>
		/// <param name="type">(Informational use only.) The type of authentication used to identify the user.</param>
		/// <param name="acctType">One of the enumeration values.</param>
		/// <param name="isAuthenticated">
		///   <see langword="true" /> to indicate that the user is authenticated; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="userToken" /> is 0.  
		/// -or-  
		/// <paramref name="userToken" /> is duplicated and invalid for impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions.  
		///  -or-  
		///  A Win32 error occurred.</exception>
		// Token: 0x060028BF RID: 10431 RVA: 0x000967A5 File Offset: 0x000949A5
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType, bool isAuthenticated)
			: this(userToken, type, isAuthenticated ? 1 : 0)
		{
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000967B8 File Offset: 0x000949B8
		[SecurityCritical]
		private WindowsIdentity(IntPtr userToken, string authType, int isAuthenticated)
			: base(null, null, null, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid")
		{
			this.CreateFromToken(userToken);
			this.m_authType = authType;
			this.m_isAuthenticated = isAuthenticated;
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x00096818 File Offset: 0x00094A18
		[SecurityCritical]
		private void CreateFromToken(IntPtr userToken)
		{
			if (userToken == IntPtr.Zero)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TokenZero"));
			}
			uint num = (uint)Marshal.SizeOf(typeof(uint));
			bool tokenInformation = Win32Native.GetTokenInformation(userToken, 8U, SafeLocalAllocHandle.InvalidHandle, 0U, out num);
			if (Marshal.GetLastWin32Error() == 6)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidImpersonationToken"));
			}
			if (!Win32Native.DuplicateHandle(Win32Native.GetCurrentProcess(), userToken, Win32Native.GetCurrentProcess(), ref this.m_safeTokenHandle, 0U, true, 2U))
			{
				throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified User Principal Name (UPN).</summary>
		/// <param name="sUserPrincipalName">The UPN for the user on whose behalf the code is running.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions.  
		///  -or-  
		///  The computer is not attached to a Windows 2003 or later domain.  
		///  -or-  
		///  The computer is not running Windows 2003 or later.  
		///  -or-  
		///  The user is not a member of the domain the computer is attached to.</exception>
		// Token: 0x060028C2 RID: 10434 RVA: 0x000968A5 File Offset: 0x00094AA5
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		public WindowsIdentity(string sUserPrincipalName)
			: this(sUserPrincipalName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by the specified User Principal Name (UPN) and the specified authentication type.</summary>
		/// <param name="sUserPrincipalName">The UPN for the user on whose behalf the code is running.</param>
		/// <param name="type">(Informational use only.) The type of authentication used to identify the user.</param>
		/// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions.  
		///  -or-  
		///  The computer is not attached to a Windows 2003 or later domain.  
		///  -or-  
		///  The computer is not running Windows 2003 or later.  
		///  -or-  
		///  The user is not a member of the domain the computer is attached to.</exception>
		// Token: 0x060028C3 RID: 10435 RVA: 0x000968B0 File Offset: 0x00094AB0
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		public WindowsIdentity(string sUserPrincipalName, string type)
			: base(null, null, null, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid")
		{
			WindowsIdentity.KerbS4ULogon(sUserPrincipalName, ref this.m_safeTokenHandle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class for the user represented by information in a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> stream.</summary>
		/// <param name="info">The object containing the account information for the user.</param>
		/// <param name="context">An object that indicates the stream characteristics.</param>
		/// <exception cref="T:System.NotSupportedException">A <see cref="T:System.Security.Principal.WindowsIdentity" /> cannot be serialized across processes.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions.  
		///  -or-  
		///  A Win32 error occurred.</exception>
		// Token: 0x060028C4 RID: 10436 RVA: 0x00096905 File Offset: 0x00094B05
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public WindowsIdentity(SerializationInfo info, StreamingContext context)
			: this(info)
		{
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x00096910 File Offset: 0x00094B10
		[SecurityCritical]
		private WindowsIdentity(SerializationInfo info)
			: base(info)
		{
			this.m_claimsInitialized = false;
			IntPtr intPtr = (IntPtr)info.GetValue("m_userToken", typeof(IntPtr));
			if (intPtr != IntPtr.Zero)
			{
				this.CreateFromToken(intPtr);
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate an instance of this execution context.</summary>
		/// <param name="info">An object containing the information required to serialize the <see cref="T:System.Collections.Hashtable" />.</param>
		/// <param name="context">An object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Hashtable" />.</param>
		// Token: 0x060028C6 RID: 10438 RVA: 0x00096984 File Offset: 0x00094B84
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("m_userToken", this.m_safeTokenHandle.DangerousGetHandle());
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x060028C7 RID: 10439 RVA: 0x000969A9 File Offset: 0x00094BA9
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		/// <summary>Returns a <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents the current Windows user.</summary>
		/// <returns>An object that represents the current user.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions.</exception>
		// Token: 0x060028C8 RID: 10440 RVA: 0x000969AB File Offset: 0x00094BAB
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		public static WindowsIdentity GetCurrent()
		{
			return WindowsIdentity.GetCurrentInternal(TokenAccessLevels.MaximumAllowed, false);
		}

		/// <summary>Returns a <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents the Windows identity for either the thread or the process, depending on the value of the <paramref name="ifImpersonating" /> parameter.</summary>
		/// <param name="ifImpersonating">
		///   <see langword="true" /> to return the <see cref="T:System.Security.Principal.WindowsIdentity" /> only if the thread is currently impersonating; <see langword="false" /> to return the <see cref="T:System.Security.Principal.WindowsIdentity" /> of the thread if it is impersonating or the <see cref="T:System.Security.Principal.WindowsIdentity" /> of the process if the thread is not currently impersonating.</param>
		/// <returns>An object that represents a Windows user.</returns>
		// Token: 0x060028C9 RID: 10441 RVA: 0x000969B8 File Offset: 0x00094BB8
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		public static WindowsIdentity GetCurrent(bool ifImpersonating)
		{
			return WindowsIdentity.GetCurrentInternal(TokenAccessLevels.MaximumAllowed, ifImpersonating);
		}

		/// <summary>Returns a <see cref="T:System.Security.Principal.WindowsIdentity" /> object that represents the current Windows user, using the specified desired token access level.</summary>
		/// <param name="desiredAccess">A bitwise combination of the enumeration values.</param>
		/// <returns>An object that represents the current user.</returns>
		// Token: 0x060028CA RID: 10442 RVA: 0x000969C5 File Offset: 0x00094BC5
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPrincipal)]
		public static WindowsIdentity GetCurrent(TokenAccessLevels desiredAccess)
		{
			return WindowsIdentity.GetCurrentInternal(desiredAccess, false);
		}

		/// <summary>Returns a <see cref="T:System.Security.Principal.WindowsIdentity" /> object that you can use as a sentinel value in your code to represent an anonymous user. The property value does not represent the built-in anonymous identity used by the Windows operating system.</summary>
		/// <returns>An object that represents an anonymous user.</returns>
		// Token: 0x060028CB RID: 10443 RVA: 0x000969CE File Offset: 0x00094BCE
		[SecuritySafeCritical]
		public static WindowsIdentity GetAnonymous()
		{
			return new WindowsIdentity();
		}

		/// <summary>Gets the type of authentication used to identify the user.</summary>
		/// <returns>The type of authentication used to identify the user.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions.  
		///  -or-  
		///  The computer is not attached to a Windows 2003 or later domain.  
		///  -or-  
		///  The computer is not running Windows 2003 or later.  
		///  -or-  
		///  The user is not a member of the domain the computer is attached to.</exception>
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060028CC RID: 10444 RVA: 0x000969D8 File Offset: 0x00094BD8
		public sealed override string AuthenticationType
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeTokenHandle.IsInvalid)
				{
					return string.Empty;
				}
				if (this.m_authType == null)
				{
					Win32Native.LUID logonAuthId = WindowsIdentity.GetLogonAuthId(this.m_safeTokenHandle);
					if (logonAuthId.LowPart == 998U)
					{
						return string.Empty;
					}
					SafeLsaReturnBufferHandle invalidHandle = SafeLsaReturnBufferHandle.InvalidHandle;
					try
					{
						int num = Win32Native.LsaGetLogonSessionData(ref logonAuthId, ref invalidHandle);
						if (num < 0)
						{
							throw WindowsIdentity.GetExceptionFromNtStatus(num);
						}
						invalidHandle.Initialize((ulong)Marshal.SizeOf(typeof(Win32Native.SECURITY_LOGON_SESSION_DATA)));
						Win32Native.SECURITY_LOGON_SESSION_DATA security_LOGON_SESSION_DATA = invalidHandle.Read<Win32Native.SECURITY_LOGON_SESSION_DATA>(0UL);
						return Marshal.PtrToStringUni(security_LOGON_SESSION_DATA.AuthenticationPackage.Buffer);
					}
					finally
					{
						if (!invalidHandle.IsInvalid)
						{
							invalidHandle.Dispose();
						}
					}
				}
				return this.m_authType;
			}
		}

		/// <summary>Gets the impersonation level for the user.</summary>
		/// <returns>One of the enumeration values that specifies the impersonation level.</returns>
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060028CD RID: 10445 RVA: 0x00096A98 File Offset: 0x00094C98
		[ComVisible(false)]
		public TokenImpersonationLevel ImpersonationLevel
		{
			[SecuritySafeCritical]
			get
			{
				if (!this.m_impersonationLevelInitialized)
				{
					TokenImpersonationLevel tokenImpersonationLevel;
					if (this.m_safeTokenHandle.IsInvalid)
					{
						tokenImpersonationLevel = TokenImpersonationLevel.Anonymous;
					}
					else
					{
						TokenType tokenInformation = (TokenType)this.GetTokenInformation<int>(TokenInformationClass.TokenType);
						if (tokenInformation == TokenType.TokenPrimary)
						{
							tokenImpersonationLevel = TokenImpersonationLevel.None;
						}
						else
						{
							int tokenInformation2 = this.GetTokenInformation<int>(TokenInformationClass.TokenImpersonationLevel);
							tokenImpersonationLevel = tokenInformation2 + TokenImpersonationLevel.Anonymous;
						}
					}
					this.m_impersonationLevel = tokenImpersonationLevel;
					this.m_impersonationLevelInitialized = true;
				}
				return this.m_impersonationLevel;
			}
		}

		/// <summary>Gets a value indicating whether the user has been authenticated by Windows.</summary>
		/// <returns>
		///   <see langword="true" /> if the user was authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060028CE RID: 10446 RVA: 0x00096AF9 File Offset: 0x00094CF9
		public override bool IsAuthenticated
		{
			get
			{
				if (this.m_isAuthenticated == -1)
				{
					this.m_isAuthenticated = (this.CheckNtTokenForSid(new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[] { 11 })) ? 1 : 0);
				}
				return this.m_isAuthenticated == 1;
			}
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x00096B34 File Offset: 0x00094D34
		[SecuritySafeCritical]
		[ComVisible(false)]
		private bool CheckNtTokenForSid(SecurityIdentifier sid)
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				return false;
			}
			SafeAccessTokenHandle invalidHandle = SafeAccessTokenHandle.InvalidHandle;
			TokenImpersonationLevel impersonationLevel = this.ImpersonationLevel;
			bool flag = false;
			try
			{
				if (impersonationLevel == TokenImpersonationLevel.None && !Win32Native.DuplicateTokenEx(this.m_safeTokenHandle, 8U, IntPtr.Zero, 2U, 2U, ref invalidHandle))
				{
					throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
				}
				if (!Win32Native.CheckTokenMembership((impersonationLevel != TokenImpersonationLevel.None) ? this.m_safeTokenHandle : invalidHandle, sid.BinaryForm, ref flag))
				{
					throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
				}
			}
			finally
			{
				if (invalidHandle != SafeAccessTokenHandle.InvalidHandle)
				{
					invalidHandle.Dispose();
				}
			}
			return flag;
		}

		/// <summary>Gets a value indicating whether the user account is identified as a <see cref="F:System.Security.Principal.WindowsAccountType.Guest" /> account by the system.</summary>
		/// <returns>
		///   <see langword="true" /> if the user account is a <see cref="F:System.Security.Principal.WindowsAccountType.Guest" /> account; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060028D0 RID: 10448 RVA: 0x00096BD8 File Offset: 0x00094DD8
		public virtual bool IsGuest
		{
			[SecuritySafeCritical]
			get
			{
				return !this.m_safeTokenHandle.IsInvalid && this.CheckNtTokenForSid(new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[] { 32, 546 }));
			}
		}

		/// <summary>Gets a value indicating whether the user account is identified as a <see cref="F:System.Security.Principal.WindowsAccountType.System" /> account by the system.</summary>
		/// <returns>
		///   <see langword="true" /> if the user account is a <see cref="F:System.Security.Principal.WindowsAccountType.System" /> account; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x00096C0C File Offset: 0x00094E0C
		public virtual bool IsSystem
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeTokenHandle.IsInvalid)
				{
					return false;
				}
				SecurityIdentifier securityIdentifier = new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[] { 18 });
				return this.User == securityIdentifier;
			}
		}

		/// <summary>Gets a value that indicates whether the user account is identified as an anonymous account by the system.</summary>
		/// <returns>
		///   <see langword="true" /> if the user account is an anonymous account; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060028D2 RID: 10450 RVA: 0x00096C48 File Offset: 0x00094E48
		public virtual bool IsAnonymous
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeTokenHandle.IsInvalid)
				{
					return true;
				}
				SecurityIdentifier securityIdentifier = new SecurityIdentifier(IdentifierAuthority.NTAuthority, new int[] { 7 });
				return this.User == securityIdentifier;
			}
		}

		/// <summary>Gets the user's Windows logon name.</summary>
		/// <returns>The Windows logon name of the user on whose behalf the code is being run.</returns>
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060028D3 RID: 10451 RVA: 0x00096C82 File Offset: 0x00094E82
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				return this.GetName();
			}
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x00096C8C File Offset: 0x00094E8C
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal string GetName()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (this.m_safeTokenHandle.IsInvalid)
			{
				return string.Empty;
			}
			if (this.m_name == null)
			{
				using (WindowsIdentity.SafeRevertToSelf(ref stackCrawlMark))
				{
					NTAccount ntaccount = this.User.Translate(typeof(NTAccount)) as NTAccount;
					this.m_name = ntaccount.ToString();
				}
			}
			return this.m_name;
		}

		/// <summary>Gets the security identifier (SID) for the token owner.</summary>
		/// <returns>An object for the token owner.</returns>
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060028D5 RID: 10453 RVA: 0x00096D08 File Offset: 0x00094F08
		[ComVisible(false)]
		public SecurityIdentifier Owner
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeTokenHandle.IsInvalid)
				{
					return null;
				}
				if (this.m_owner == null)
				{
					using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenOwner))
					{
						this.m_owner = new SecurityIdentifier(tokenInformation.Read<IntPtr>(0UL), true);
					}
				}
				return this.m_owner;
			}
		}

		/// <summary>Gets the security identifier (SID) for the user.</summary>
		/// <returns>An object for the user.</returns>
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060028D6 RID: 10454 RVA: 0x00096D78 File Offset: 0x00094F78
		[ComVisible(false)]
		public SecurityIdentifier User
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeTokenHandle.IsInvalid)
				{
					return null;
				}
				if (this.m_user == null)
				{
					using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenUser))
					{
						this.m_user = new SecurityIdentifier(tokenInformation.Read<IntPtr>(0UL), true);
					}
				}
				return this.m_user;
			}
		}

		/// <summary>Gets the groups the current Windows user belongs to.</summary>
		/// <returns>An object representing the groups the current Windows user belongs to.</returns>
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060028D7 RID: 10455 RVA: 0x00096DE8 File Offset: 0x00094FE8
		public IdentityReferenceCollection Groups
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeTokenHandle.IsInvalid)
				{
					return null;
				}
				if (this.m_groups == null)
				{
					IdentityReferenceCollection identityReferenceCollection = new IdentityReferenceCollection();
					using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenGroups))
					{
						uint num = tokenInformation.Read<uint>(0UL);
						if (num != 0U)
						{
							Win32Native.TOKEN_GROUPS token_GROUPS = tokenInformation.Read<Win32Native.TOKEN_GROUPS>(0UL);
							Win32Native.SID_AND_ATTRIBUTES[] array = new Win32Native.SID_AND_ATTRIBUTES[token_GROUPS.GroupCount];
							tokenInformation.ReadArray<Win32Native.SID_AND_ATTRIBUTES>((ulong)Marshal.OffsetOf(typeof(Win32Native.TOKEN_GROUPS), "Groups").ToInt32(), array, 0, array.Length);
							foreach (Win32Native.SID_AND_ATTRIBUTES sid_AND_ATTRIBUTES in array)
							{
								uint num2 = 3221225492U;
								if ((sid_AND_ATTRIBUTES.Attributes & num2) == 4U)
								{
									identityReferenceCollection.Add(new SecurityIdentifier(sid_AND_ATTRIBUTES.Sid, true));
								}
							}
						}
					}
					Interlocked.CompareExchange(ref this.m_groups, identityReferenceCollection, null);
				}
				return this.m_groups as IdentityReferenceCollection;
			}
		}

		/// <summary>Gets the Windows account token for the user.</summary>
		/// <returns>The handle of the access token associated with the current execution thread.</returns>
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060028D8 RID: 10456 RVA: 0x00096EF0 File Offset: 0x000950F0
		public virtual IntPtr Token
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.m_safeTokenHandle.DangerousGetHandle();
			}
		}

		/// <summary>Runs the specified action as the impersonated Windows identity. Instead of using an impersonated method call and running your function in <see cref="T:System.Security.Principal.WindowsImpersonationContext" />, you can use <see cref="M:System.Security.Principal.WindowsIdentity.RunImpersonated(Microsoft.Win32.SafeHandles.SafeAccessTokenHandle,System.Action)" /> and provide your function directly as a parameter.</summary>
		/// <param name="safeAccessTokenHandle">The SafeAccessTokenHandle of the impersonated Windows identity.</param>
		/// <param name="action">The System.Action to run.</param>
		// Token: 0x060028D9 RID: 10457 RVA: 0x00096F00 File Offset: 0x00095100
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void RunImpersonated(SafeAccessTokenHandle safeAccessTokenHandle, Action action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			WindowsIdentity windowsIdentity = null;
			if (!safeAccessTokenHandle.IsInvalid)
			{
				windowsIdentity = new WindowsIdentity(safeAccessTokenHandle);
			}
			using (WindowsIdentity.SafeImpersonate(safeAccessTokenHandle, windowsIdentity, ref stackCrawlMark))
			{
				action();
			}
		}

		/// <summary>Runs the specified function as the impersonated Windows identity. Instead of using an impersonated method call and running your function in <see cref="T:System.Security.Principal.WindowsImpersonationContext" />, you can use <see cref="M:System.Security.Principal.WindowsIdentity.RunImpersonated(Microsoft.Win32.SafeHandles.SafeAccessTokenHandle,System.Action)" /> and provide your function directly as a parameter.</summary>
		/// <param name="safeAccessTokenHandle">The SafeAccessTokenHandle of the impersonated Windows identity.</param>
		/// <param name="func">The System.Func to run.</param>
		/// <typeparam name="T">The type of object used by and returned by the function.</typeparam>
		/// <returns>The result of the function.</returns>
		// Token: 0x060028DA RID: 10458 RVA: 0x00096F5C File Offset: 0x0009515C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static T RunImpersonated<T>(SafeAccessTokenHandle safeAccessTokenHandle, Func<T> func)
		{
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			WindowsIdentity windowsIdentity = null;
			if (!safeAccessTokenHandle.IsInvalid)
			{
				windowsIdentity = new WindowsIdentity(safeAccessTokenHandle);
			}
			T t = default(T);
			using (WindowsIdentity.SafeImpersonate(safeAccessTokenHandle, windowsIdentity, ref stackCrawlMark))
			{
				t = func();
			}
			return t;
		}

		/// <summary>Impersonates the user represented by the <see cref="T:System.Security.Principal.WindowsIdentity" /> object.</summary>
		/// <returns>An object that represents the Windows user prior to impersonation; this can be used to revert to the original user's context.</returns>
		/// <exception cref="T:System.InvalidOperationException">An anonymous identity attempted to perform an impersonation.</exception>
		/// <exception cref="T:System.Security.SecurityException">A Win32 error occurred.</exception>
		// Token: 0x060028DB RID: 10459 RVA: 0x00096FC0 File Offset: 0x000951C0
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public virtual WindowsImpersonationContext Impersonate()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.Impersonate(ref stackCrawlMark);
		}

		/// <summary>Impersonates the user represented by the specified user token.</summary>
		/// <param name="userToken">The handle of a Windows account token. This token is usually retrieved through a call to unmanaged code, such as a call to the Win32 API <see langword="LogonUser" /> function.</param>
		/// <returns>An object that represents the Windows user prior to impersonation; this object can be used to revert to the original user's context.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Windows returned the Windows NT status code STATUS_ACCESS_DENIED.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions.</exception>
		// Token: 0x060028DC RID: 10460 RVA: 0x00096FD8 File Offset: 0x000951D8
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlPrincipal)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static WindowsImpersonationContext Impersonate(IntPtr userToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (userToken == IntPtr.Zero)
			{
				return WindowsIdentity.SafeRevertToSelf(ref stackCrawlMark);
			}
			WindowsIdentity windowsIdentity = new WindowsIdentity(userToken, null, -1);
			return windowsIdentity.Impersonate(ref stackCrawlMark);
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x0009700D File Offset: 0x0009520D
		[SecurityCritical]
		internal WindowsImpersonationContext Impersonate(ref StackCrawlMark stackMark)
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AnonymousCannotImpersonate"));
			}
			return WindowsIdentity.SafeImpersonate(this.m_safeTokenHandle, this, ref stackMark);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Principal.WindowsIdentity" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060028DE RID: 10462 RVA: 0x00097039 File Offset: 0x00095239
		[SecuritySafeCritical]
		[ComVisible(false)]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.m_safeTokenHandle != null && !this.m_safeTokenHandle.IsClosed)
			{
				this.m_safeTokenHandle.Dispose();
			}
			this.m_name = null;
			this.m_owner = null;
			this.m_user = null;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Principal.WindowsIdentity" />.</summary>
		// Token: 0x060028DF RID: 10463 RVA: 0x00097073 File Offset: 0x00095273
		[ComVisible(false)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Gets this <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" /> for this <see cref="T:System.Security.Principal.WindowsIdentity" /> instance.</summary>
		/// <returns>Returns a <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" />.</returns>
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060028E0 RID: 10464 RVA: 0x0009707C File Offset: 0x0009527C
		public SafeAccessTokenHandle AccessToken
		{
			[SecurityCritical]
			get
			{
				return this.m_safeTokenHandle;
			}
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x00097084 File Offset: 0x00095284
		[SecurityCritical]
		internal static WindowsImpersonationContext SafeRevertToSelf(ref StackCrawlMark stackMark)
		{
			return WindowsIdentity.SafeImpersonate(WindowsIdentity.s_invalidTokenHandle, null, ref stackMark);
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x00097094 File Offset: 0x00095294
		[SecurityCritical]
		internal static WindowsImpersonationContext SafeImpersonate(SafeAccessTokenHandle userToken, WindowsIdentity wi, ref StackCrawlMark stackMark)
		{
			int num = 0;
			bool flag;
			SafeAccessTokenHandle currentToken = WindowsIdentity.GetCurrentToken(TokenAccessLevels.MaximumAllowed, false, out flag, out num);
			if (currentToken == null || currentToken.IsInvalid)
			{
				throw new SecurityException(Win32Native.GetMessage(num));
			}
			FrameSecurityDescriptor securityObjectForFrame = SecurityRuntime.GetSecurityObjectForFrame(ref stackMark, true);
			if (securityObjectForFrame == null)
			{
				throw new SecurityException(Environment.GetResourceString("ExecutionEngine_MissingSecurityDescriptor"));
			}
			WindowsImpersonationContext windowsImpersonationContext = new WindowsImpersonationContext(currentToken, WindowsIdentity.GetCurrentThreadWI(), flag, securityObjectForFrame);
			if (userToken.IsInvalid)
			{
				num = Win32.RevertToSelf();
				if (num < 0)
				{
					Environment.FailFast(Win32Native.GetMessage(num));
				}
				WindowsIdentity.UpdateThreadWI(wi);
				securityObjectForFrame.SetTokenHandles(currentToken, (wi == null) ? null : wi.AccessToken);
			}
			else
			{
				num = Win32.RevertToSelf();
				if (num < 0)
				{
					Environment.FailFast(Win32Native.GetMessage(num));
				}
				num = Win32.ImpersonateLoggedOnUser(userToken);
				if (num < 0)
				{
					windowsImpersonationContext.Undo();
					throw new SecurityException(Environment.GetResourceString("Argument_ImpersonateUser"));
				}
				WindowsIdentity.UpdateThreadWI(wi);
				securityObjectForFrame.SetTokenHandles(currentToken, (wi == null) ? null : wi.AccessToken);
			}
			return windowsImpersonationContext;
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x0009717E File Offset: 0x0009537E
		[SecurityCritical]
		internal static WindowsIdentity GetCurrentThreadWI()
		{
			return SecurityContext.GetCurrentWI(Thread.CurrentThread.GetExecutionContextReader());
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x00097190 File Offset: 0x00095390
		[SecurityCritical]
		internal static void UpdateThreadWI(WindowsIdentity wi)
		{
			Thread currentThread = Thread.CurrentThread;
			if (currentThread.GetExecutionContextReader().SecurityContext.WindowsIdentity != wi)
			{
				ExecutionContext mutableExecutionContext = currentThread.GetMutableExecutionContext();
				SecurityContext securityContext = mutableExecutionContext.SecurityContext;
				if (wi != null && securityContext == null)
				{
					securityContext = new SecurityContext();
					mutableExecutionContext.SecurityContext = securityContext;
				}
				if (securityContext != null)
				{
					securityContext.WindowsIdentity = wi;
				}
			}
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000971F0 File Offset: 0x000953F0
		[SecurityCritical]
		internal static WindowsIdentity GetCurrentInternal(TokenAccessLevels desiredAccess, bool threadOnly)
		{
			int num = 0;
			bool flag;
			SafeAccessTokenHandle currentToken = WindowsIdentity.GetCurrentToken(desiredAccess, threadOnly, out flag, out num);
			if (currentToken != null && !currentToken.IsInvalid)
			{
				WindowsIdentity windowsIdentity = new WindowsIdentity();
				windowsIdentity.m_safeTokenHandle.Dispose();
				windowsIdentity.m_safeTokenHandle = currentToken;
				return windowsIdentity;
			}
			if (threadOnly && !flag)
			{
				return null;
			}
			throw new SecurityException(Win32Native.GetMessage(num));
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x00097243 File Offset: 0x00095443
		internal static RuntimeConstructorInfo GetSpecialSerializationCtor()
		{
			return WindowsIdentity.s_specialSerializationCtor;
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0009724A File Offset: 0x0009544A
		private static int GetHRForWin32Error(int dwLastError)
		{
			if (((long)dwLastError & (long)((ulong)(-2147483648))) == (long)((ulong)(-2147483648)))
			{
				return dwLastError;
			}
			return (dwLastError & 65535) | -2147024896;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0009726C File Offset: 0x0009546C
		[SecurityCritical]
		private static Exception GetExceptionFromNtStatus(int status)
		{
			if (status == -1073741790)
			{
				return new UnauthorizedAccessException();
			}
			if (status == -1073741670 || status == -1073741801)
			{
				return new OutOfMemoryException();
			}
			int num = Win32Native.LsaNtStatusToWinError(status);
			return new SecurityException(Win32Native.GetMessage(num));
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x000972B0 File Offset: 0x000954B0
		[SecurityCritical]
		private static SafeAccessTokenHandle GetCurrentToken(TokenAccessLevels desiredAccess, bool threadOnly, out bool isImpersonating, out int hr)
		{
			isImpersonating = true;
			SafeAccessTokenHandle safeAccessTokenHandle = WindowsIdentity.GetCurrentThreadToken(desiredAccess, out hr);
			if (safeAccessTokenHandle == null && hr == WindowsIdentity.GetHRForWin32Error(1008))
			{
				isImpersonating = false;
				if (!threadOnly)
				{
					safeAccessTokenHandle = WindowsIdentity.GetCurrentProcessToken(desiredAccess, out hr);
				}
			}
			return safeAccessTokenHandle;
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000972E8 File Offset: 0x000954E8
		[SecurityCritical]
		private static SafeAccessTokenHandle GetCurrentProcessToken(TokenAccessLevels desiredAccess, out int hr)
		{
			hr = 0;
			SafeAccessTokenHandle safeAccessTokenHandle;
			if (!Win32Native.OpenProcessToken(Win32Native.GetCurrentProcess(), desiredAccess, out safeAccessTokenHandle))
			{
				hr = WindowsIdentity.GetHRForWin32Error(Marshal.GetLastWin32Error());
			}
			return safeAccessTokenHandle;
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x00097314 File Offset: 0x00095514
		[SecurityCritical]
		internal static SafeAccessTokenHandle GetCurrentThreadToken(TokenAccessLevels desiredAccess, out int hr)
		{
			SafeAccessTokenHandle safeAccessTokenHandle;
			hr = Win32.OpenThreadToken(desiredAccess, WinSecurityContext.Both, out safeAccessTokenHandle);
			return safeAccessTokenHandle;
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x00097330 File Offset: 0x00095530
		[SecurityCritical]
		private T GetTokenInformation<T>(TokenInformationClass tokenInformationClass) where T : struct
		{
			T t;
			using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, tokenInformationClass))
			{
				t = tokenInformation.Read<T>(0UL);
			}
			return t;
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x00097370 File Offset: 0x00095570
		[SecurityCritical]
		internal static ImpersonationQueryResult QueryImpersonation()
		{
			SafeAccessTokenHandle safeAccessTokenHandle = null;
			int num = Win32.OpenThreadToken(TokenAccessLevels.Query, WinSecurityContext.Thread, out safeAccessTokenHandle);
			if (safeAccessTokenHandle != null)
			{
				safeAccessTokenHandle.Close();
				return ImpersonationQueryResult.Impersonated;
			}
			if (num == WindowsIdentity.GetHRForWin32Error(5))
			{
				return ImpersonationQueryResult.Impersonated;
			}
			if (num == WindowsIdentity.GetHRForWin32Error(1008))
			{
				return ImpersonationQueryResult.NotImpersonated;
			}
			return ImpersonationQueryResult.Failed;
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000973B0 File Offset: 0x000955B0
		[SecurityCritical]
		private static Win32Native.LUID GetLogonAuthId(SafeAccessTokenHandle safeTokenHandle)
		{
			Win32Native.LUID authenticationId;
			using (SafeLocalAllocHandle tokenInformation = WindowsIdentity.GetTokenInformation(safeTokenHandle, TokenInformationClass.TokenStatistics))
			{
				Win32Native.TOKEN_STATISTICS token_STATISTICS = tokenInformation.Read<Win32Native.TOKEN_STATISTICS>(0UL);
				authenticationId = token_STATISTICS.AuthenticationId;
			}
			return authenticationId;
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000973F4 File Offset: 0x000955F4
		[SecurityCritical]
		private static SafeLocalAllocHandle GetTokenInformation(SafeAccessTokenHandle tokenHandle, TokenInformationClass tokenInformationClass)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			uint num = (uint)Marshal.SizeOf(typeof(uint));
			bool tokenInformation = Win32Native.GetTokenInformation(tokenHandle, (uint)tokenInformationClass, safeLocalAllocHandle, 0U, out num);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 6)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidImpersonationToken"));
			}
			if (lastWin32Error != 24 && lastWin32Error != 122)
			{
				throw new SecurityException(Win32Native.GetMessage(lastWin32Error));
			}
			UIntPtr uintPtr = new UIntPtr(num);
			safeLocalAllocHandle.Dispose();
			safeLocalAllocHandle = Win32Native.LocalAlloc(0, uintPtr);
			if (safeLocalAllocHandle == null || safeLocalAllocHandle.IsInvalid)
			{
				throw new OutOfMemoryException();
			}
			safeLocalAllocHandle.Initialize((ulong)num);
			if (!Win32Native.GetTokenInformation(tokenHandle, (uint)tokenInformationClass, safeLocalAllocHandle, num, out num))
			{
				throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
			}
			return safeLocalAllocHandle;
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x000974A4 File Offset: 0x000956A4
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		private unsafe static SafeAccessTokenHandle KerbS4ULogon(string upn, ref SafeAccessTokenHandle safeTokenHandle)
		{
			byte[] array = new byte[] { 67, 76, 82 };
			UIntPtr uintPtr = new UIntPtr((uint)(array.Length + 1));
			SafeAccessTokenHandle safeAccessTokenHandle;
			using (SafeLocalAllocHandle safeLocalAllocHandle = Win32Native.LocalAlloc(64, uintPtr))
			{
				if (safeLocalAllocHandle == null || safeLocalAllocHandle.IsInvalid)
				{
					throw new OutOfMemoryException();
				}
				safeLocalAllocHandle.Initialize((ulong)((long)array.Length + 1L));
				safeLocalAllocHandle.WriteArray<byte>(0UL, array, 0, array.Length);
				Win32Native.UNICODE_INTPTR_STRING unicode_INTPTR_STRING = new Win32Native.UNICODE_INTPTR_STRING(array.Length, safeLocalAllocHandle);
				SafeLsaLogonProcessHandle invalidHandle = SafeLsaLogonProcessHandle.InvalidHandle;
				SafeLsaReturnBufferHandle invalidHandle2 = SafeLsaReturnBufferHandle.InvalidHandle;
				try
				{
					Privilege privilege = null;
					RuntimeHelpers.PrepareConstrainedRegions();
					int num;
					try
					{
						try
						{
							privilege = new Privilege("SeTcbPrivilege");
							privilege.Enable();
						}
						catch (PrivilegeNotHeldException)
						{
						}
						IntPtr zero = IntPtr.Zero;
						num = Win32Native.LsaRegisterLogonProcess(ref unicode_INTPTR_STRING, ref invalidHandle, ref zero);
						if (5 == Win32Native.LsaNtStatusToWinError(num))
						{
							num = Win32Native.LsaConnectUntrusted(ref invalidHandle);
						}
					}
					catch
					{
						if (privilege != null)
						{
							privilege.Revert();
						}
						throw;
					}
					finally
					{
						if (privilege != null)
						{
							privilege.Revert();
						}
					}
					if (num < 0)
					{
						throw WindowsIdentity.GetExceptionFromNtStatus(num);
					}
					byte[] array2 = new byte["Kerberos".Length + 1];
					Encoding.ASCII.GetBytes("Kerberos", 0, "Kerberos".Length, array2, 0);
					uintPtr = new UIntPtr((uint)array2.Length);
					using (SafeLocalAllocHandle safeLocalAllocHandle2 = Win32Native.LocalAlloc(0, uintPtr))
					{
						if (safeLocalAllocHandle2 == null || safeLocalAllocHandle2.IsInvalid)
						{
							throw new OutOfMemoryException();
						}
						safeLocalAllocHandle2.Initialize((ulong)array2.Length);
						safeLocalAllocHandle2.WriteArray<byte>(0UL, array2, 0, array2.Length);
						Win32Native.UNICODE_INTPTR_STRING unicode_INTPTR_STRING2 = new Win32Native.UNICODE_INTPTR_STRING("Kerberos".Length, safeLocalAllocHandle2);
						uint num2 = 0U;
						num = Win32Native.LsaLookupAuthenticationPackage(invalidHandle, ref unicode_INTPTR_STRING2, ref num2);
						if (num < 0)
						{
							throw WindowsIdentity.GetExceptionFromNtStatus(num);
						}
						Win32Native.TOKEN_SOURCE token_SOURCE = default(Win32Native.TOKEN_SOURCE);
						if (!Win32Native.AllocateLocallyUniqueId(ref token_SOURCE.SourceIdentifier))
						{
							throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
						}
						token_SOURCE.Name = new char[8];
						token_SOURCE.Name[0] = 'C';
						token_SOURCE.Name[1] = 'L';
						token_SOURCE.Name[2] = 'R';
						uint num3 = 0U;
						Win32Native.LUID luid = default(Win32Native.LUID);
						Win32Native.QUOTA_LIMITS quota_LIMITS = default(Win32Native.QUOTA_LIMITS);
						int num4 = 0;
						byte[] bytes = Encoding.Unicode.GetBytes(upn);
						uint num5 = (uint)(Marshal.SizeOf(typeof(Win32Native.KERB_S4U_LOGON)) + bytes.Length);
						using (SafeLocalAllocHandle safeLocalAllocHandle3 = Win32Native.LocalAlloc(64, new UIntPtr(num5)))
						{
							if (safeLocalAllocHandle3 == null || safeLocalAllocHandle3.IsInvalid)
							{
								throw new OutOfMemoryException();
							}
							safeLocalAllocHandle3.Initialize((ulong)num5);
							ulong num6 = (ulong)((long)Marshal.SizeOf(typeof(Win32Native.KERB_S4U_LOGON)));
							safeLocalAllocHandle3.WriteArray<byte>(num6, bytes, 0, bytes.Length);
							byte* ptr = null;
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
								safeLocalAllocHandle3.AcquirePointer(ref ptr);
								safeLocalAllocHandle3.Write<Win32Native.KERB_S4U_LOGON>(0UL, new Win32Native.KERB_S4U_LOGON
								{
									MessageType = 12U,
									Flags = 0U,
									ClientUpn = new Win32Native.UNICODE_INTPTR_STRING(bytes.Length, new IntPtr((void*)(ptr + num6)))
								});
								num = Win32Native.LsaLogonUser(invalidHandle, ref unicode_INTPTR_STRING, 3U, num2, new IntPtr((void*)ptr), (uint)safeLocalAllocHandle3.ByteLength, IntPtr.Zero, ref token_SOURCE, ref invalidHandle2, ref num3, ref luid, ref safeTokenHandle, ref quota_LIMITS, ref num4);
								if (num == -1073741714 && num4 < 0)
								{
									num = num4;
								}
								if (num < 0)
								{
									throw WindowsIdentity.GetExceptionFromNtStatus(num);
								}
								if (num4 < 0)
								{
									throw WindowsIdentity.GetExceptionFromNtStatus(num4);
								}
							}
							finally
							{
								if (ptr != null)
								{
									safeLocalAllocHandle3.ReleasePointer();
								}
							}
						}
						safeAccessTokenHandle = safeTokenHandle;
					}
				}
				finally
				{
					if (!invalidHandle.IsInvalid)
					{
						invalidHandle.Dispose();
					}
					if (!invalidHandle2.IsInvalid)
					{
						invalidHandle2.Dispose();
					}
				}
			}
			return safeAccessTokenHandle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsIdentity" /> class by using the specified <see cref="T:System.Security.Principal.WindowsIdentity" /> object.</summary>
		/// <param name="identity">The object from which to construct the new instance of <see cref="T:System.Security.Principal.WindowsIdentity" />.</param>
		// Token: 0x060028F1 RID: 10481 RVA: 0x000978D8 File Offset: 0x00095AD8
		[SecuritySafeCritical]
		protected WindowsIdentity(WindowsIdentity identity)
			: base(identity, null, identity.m_authType, null, null, false)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (!identity.m_safeTokenHandle.IsInvalid && identity.m_safeTokenHandle != SafeAccessTokenHandle.InvalidHandle && identity.m_safeTokenHandle.DangerousGetHandle() != IntPtr.Zero)
				{
					identity.m_safeTokenHandle.DangerousAddRef(ref flag);
					if (!identity.m_safeTokenHandle.IsInvalid && identity.m_safeTokenHandle.DangerousGetHandle() != IntPtr.Zero)
					{
						this.CreateFromToken(identity.m_safeTokenHandle.DangerousGetHandle());
					}
					this.m_authType = identity.m_authType;
					this.m_isAuthenticated = identity.m_isAuthenticated;
				}
			}
			finally
			{
				if (flag)
				{
					identity.m_safeTokenHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x000979E0 File Offset: 0x00095BE0
		[SecurityCritical]
		internal IntPtr GetTokenInternal()
		{
			return this.m_safeTokenHandle.DangerousGetHandle();
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000979F0 File Offset: 0x00095BF0
		[SecurityCritical]
		internal WindowsIdentity(ClaimsIdentity claimsIdentity, IntPtr userToken)
			: base(claimsIdentity)
		{
			if (userToken != IntPtr.Zero && userToken.ToInt64() > 0L)
			{
				this.CreateFromToken(userToken);
			}
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x00097A4B File Offset: 0x00095C4B
		internal ClaimsIdentity CloneAsBase()
		{
			return base.Clone();
		}

		/// <summary>Creates a new  object that is a copy of the current instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x060028F5 RID: 10485 RVA: 0x00097A53 File Offset: 0x00095C53
		public override ClaimsIdentity Clone()
		{
			return new WindowsIdentity(this);
		}

		/// <summary>Gets claims that have the <see cref="F:System.Security.Claims.ClaimTypes.WindowsUserClaim" /> property key.</summary>
		/// <returns>A collection of claims that have the <see cref="F:System.Security.Claims.ClaimTypes.WindowsUserClaim" /> property key.</returns>
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x00097A5B File Offset: 0x00095C5B
		public virtual IEnumerable<Claim> UserClaims
		{
			get
			{
				this.InitializeClaims();
				return this.m_userClaims.AsReadOnly();
			}
		}

		/// <summary>Gets claims that have the <see cref="F:System.Security.Claims.ClaimTypes.WindowsDeviceClaim" /> property key.</summary>
		/// <returns>A collection of claims that have the <see cref="F:System.Security.Claims.ClaimTypes.WindowsDeviceClaim" /> property key.</returns>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x00097A6E File Offset: 0x00095C6E
		public virtual IEnumerable<Claim> DeviceClaims
		{
			get
			{
				this.InitializeClaims();
				return this.m_deviceClaims.AsReadOnly();
			}
		}

		/// <summary>Gets all claims for the user represented by this Windows identity.</summary>
		/// <returns>A collection of claims for this <see cref="T:System.Security.Principal.WindowsIdentity" /> object.</returns>
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x00097A84 File Offset: 0x00095C84
		public override IEnumerable<Claim> Claims
		{
			get
			{
				if (!this.m_claimsInitialized)
				{
					this.InitializeClaims();
				}
				foreach (Claim claim in base.Claims)
				{
					yield return claim;
				}
				IEnumerator<Claim> enumerator = null;
				foreach (Claim claim2 in this.m_userClaims)
				{
					yield return claim2;
				}
				List<Claim>.Enumerator enumerator2 = default(List<Claim>.Enumerator);
				foreach (Claim claim3 in this.m_deviceClaims)
				{
					yield return claim3;
				}
				enumerator2 = default(List<Claim>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x00097AA4 File Offset: 0x00095CA4
		[SecuritySafeCritical]
		private void InitializeClaims()
		{
			if (!this.m_claimsInitialized)
			{
				object claimsIntiailizedLock = this.m_claimsIntiailizedLock;
				lock (claimsIntiailizedLock)
				{
					if (!this.m_claimsInitialized)
					{
						this.m_userClaims = new List<Claim>();
						this.m_deviceClaims = new List<Claim>();
						if (!string.IsNullOrEmpty(this.Name))
						{
							this.m_userClaims.Add(new Claim(base.NameClaimType, this.Name, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this));
						}
						this.AddPrimarySidClaim(this.m_userClaims);
						this.AddGroupSidClaims(this.m_userClaims);
						if (Environment.IsWindows8OrAbove)
						{
							this.AddDeviceGroupSidClaims(this.m_deviceClaims, TokenInformationClass.TokenDeviceGroups);
							this.AddTokenClaims(this.m_userClaims, TokenInformationClass.TokenUserClaimAttributes, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim");
							this.AddTokenClaims(this.m_deviceClaims, TokenInformationClass.TokenDeviceClaimAttributes, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim");
						}
						this.m_claimsInitialized = true;
					}
				}
			}
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x00097BA8 File Offset: 0x00095DA8
		[SecurityCritical]
		private void AddDeviceGroupSidClaims(List<Claim> instanceClaims, TokenInformationClass tokenInformationClass)
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				return;
			}
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			try
			{
				safeLocalAllocHandle = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, tokenInformationClass);
				int num = Marshal.ReadInt32(safeLocalAllocHandle.DangerousGetHandle());
				IntPtr intPtr = new IntPtr((long)safeLocalAllocHandle.DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(Win32Native.TOKEN_GROUPS), "Groups"));
				for (int i = 0; i < num; i++)
				{
					Win32Native.SID_AND_ATTRIBUTES sid_AND_ATTRIBUTES = (Win32Native.SID_AND_ATTRIBUTES)Marshal.PtrToStructure(intPtr, typeof(Win32Native.SID_AND_ATTRIBUTES));
					uint num2 = 3221225492U;
					SecurityIdentifier securityIdentifier = new SecurityIdentifier(sid_AND_ATTRIBUTES.Sid, true);
					if ((sid_AND_ATTRIBUTES.Attributes & num2) == 4U)
					{
						string text = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup";
						instanceClaims.Add(new Claim(text, securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier.IdentifierAuthority, CultureInfo.InvariantCulture))
						{
							Properties = { { text, "" } }
						});
					}
					else if ((sid_AND_ATTRIBUTES.Attributes & num2) == 16U)
					{
						string text = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup";
						instanceClaims.Add(new Claim(text, securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier.IdentifierAuthority, CultureInfo.InvariantCulture))
						{
							Properties = { { text, "" } }
						});
					}
					intPtr = new IntPtr((long)intPtr + Win32Native.SID_AND_ATTRIBUTES.SizeOf);
				}
			}
			finally
			{
				safeLocalAllocHandle.Close();
			}
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x00097D60 File Offset: 0x00095F60
		[SecurityCritical]
		private void AddGroupSidClaims(List<Claim> instanceClaims)
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				return;
			}
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			SafeLocalAllocHandle safeLocalAllocHandle2 = SafeLocalAllocHandle.InvalidHandle;
			try
			{
				safeLocalAllocHandle2 = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenPrimaryGroup);
				Win32Native.TOKEN_PRIMARY_GROUP token_PRIMARY_GROUP = (Win32Native.TOKEN_PRIMARY_GROUP)Marshal.PtrToStructure(safeLocalAllocHandle2.DangerousGetHandle(), typeof(Win32Native.TOKEN_PRIMARY_GROUP));
				SecurityIdentifier securityIdentifier = new SecurityIdentifier(token_PRIMARY_GROUP.PrimaryGroup, true);
				bool flag = false;
				safeLocalAllocHandle = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenGroups);
				int num = Marshal.ReadInt32(safeLocalAllocHandle.DangerousGetHandle());
				IntPtr intPtr = new IntPtr((long)safeLocalAllocHandle.DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(Win32Native.TOKEN_GROUPS), "Groups"));
				for (int i = 0; i < num; i++)
				{
					Win32Native.SID_AND_ATTRIBUTES sid_AND_ATTRIBUTES = (Win32Native.SID_AND_ATTRIBUTES)Marshal.PtrToStructure(intPtr, typeof(Win32Native.SID_AND_ATTRIBUTES));
					uint num2 = 3221225492U;
					SecurityIdentifier securityIdentifier2 = new SecurityIdentifier(sid_AND_ATTRIBUTES.Sid, true);
					if ((sid_AND_ATTRIBUTES.Attributes & num2) == 4U)
					{
						if (!flag && StringComparer.Ordinal.Equals(securityIdentifier2.Value, securityIdentifier.Value))
						{
							instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier2.IdentifierAuthority, CultureInfo.InvariantCulture)));
							flag = true;
						}
						instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier2.IdentifierAuthority, CultureInfo.InvariantCulture)));
					}
					else if ((sid_AND_ATTRIBUTES.Attributes & num2) == 16U)
					{
						if (!flag && StringComparer.Ordinal.Equals(securityIdentifier2.Value, securityIdentifier.Value))
						{
							instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier2.IdentifierAuthority, CultureInfo.InvariantCulture)));
							flag = true;
						}
						instanceClaims.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid", securityIdentifier2.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier2.IdentifierAuthority, CultureInfo.InvariantCulture)));
					}
					intPtr = new IntPtr((long)intPtr + Win32Native.SID_AND_ATTRIBUTES.SizeOf);
				}
			}
			finally
			{
				safeLocalAllocHandle.Close();
				safeLocalAllocHandle2.Close();
			}
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x00098000 File Offset: 0x00096200
		[SecurityCritical]
		private void AddPrimarySidClaim(List<Claim> instanceClaims)
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				return;
			}
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			try
			{
				safeLocalAllocHandle = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, TokenInformationClass.TokenUser);
				Win32Native.SID_AND_ATTRIBUTES sid_AND_ATTRIBUTES = (Win32Native.SID_AND_ATTRIBUTES)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(Win32Native.SID_AND_ATTRIBUTES));
				uint num = 16U;
				SecurityIdentifier securityIdentifier = new SecurityIdentifier(sid_AND_ATTRIBUTES.Sid, true);
				if (sid_AND_ATTRIBUTES.Attributes == 0U)
				{
					instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid", securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier.IdentifierAuthority, CultureInfo.InvariantCulture)));
				}
				else if ((sid_AND_ATTRIBUTES.Attributes & num) == 16U)
				{
					instanceClaims.Add(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid", securityIdentifier.Value, "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", Convert.ToString(securityIdentifier.IdentifierAuthority, CultureInfo.InvariantCulture)));
				}
			}
			finally
			{
				safeLocalAllocHandle.Close();
			}
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x0009810C File Offset: 0x0009630C
		[SecurityCritical]
		private void AddTokenClaims(List<Claim> instanceClaims, TokenInformationClass tokenInformationClass, string propertyValue)
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				return;
			}
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			try
			{
				SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
				safeLocalAllocHandle = WindowsIdentity.GetTokenInformation(this.m_safeTokenHandle, tokenInformationClass);
				Win32Native.CLAIM_SECURITY_ATTRIBUTES_INFORMATION claim_SECURITY_ATTRIBUTES_INFORMATION = (Win32Native.CLAIM_SECURITY_ATTRIBUTES_INFORMATION)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(Win32Native.CLAIM_SECURITY_ATTRIBUTES_INFORMATION));
				long num = 0L;
				int num2 = 0;
				while ((long)num2 < (long)((ulong)claim_SECURITY_ATTRIBUTES_INFORMATION.AttributeCount))
				{
					IntPtr intPtr = new IntPtr(claim_SECURITY_ATTRIBUTES_INFORMATION.Attribute.pAttributeV1.ToInt64() + num);
					Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1 claim_SECURITY_ATTRIBUTE_V = (Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1)Marshal.PtrToStructure(intPtr, typeof(Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1));
					switch (claim_SECURITY_ATTRIBUTE_V.ValueType)
					{
					case 1:
					{
						long[] array = new long[claim_SECURITY_ATTRIBUTE_V.ValueCount];
						Marshal.Copy(claim_SECURITY_ATTRIBUTE_V.Values.pInt64, array, 0, (int)claim_SECURITY_ATTRIBUTE_V.ValueCount);
						int num3 = 0;
						while ((long)num3 < (long)((ulong)claim_SECURITY_ATTRIBUTE_V.ValueCount))
						{
							instanceClaims.Add(new Claim(claim_SECURITY_ATTRIBUTE_V.Name, Convert.ToString(array[num3], CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#integer64", this.m_issuerName, this.m_issuerName, this, propertyValue, string.Empty));
							num3++;
						}
						break;
					}
					case 2:
					{
						long[] array2 = new long[claim_SECURITY_ATTRIBUTE_V.ValueCount];
						Marshal.Copy(claim_SECURITY_ATTRIBUTE_V.Values.pUint64, array2, 0, (int)claim_SECURITY_ATTRIBUTE_V.ValueCount);
						int num4 = 0;
						while ((long)num4 < (long)((ulong)claim_SECURITY_ATTRIBUTE_V.ValueCount))
						{
							instanceClaims.Add(new Claim(claim_SECURITY_ATTRIBUTE_V.Name, Convert.ToString((ulong)array2[num4], CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#uinteger64", this.m_issuerName, this.m_issuerName, this, propertyValue, string.Empty));
							num4++;
						}
						break;
					}
					case 3:
					{
						IntPtr[] array3 = new IntPtr[claim_SECURITY_ATTRIBUTE_V.ValueCount];
						Marshal.Copy(claim_SECURITY_ATTRIBUTE_V.Values.ppString, array3, 0, (int)claim_SECURITY_ATTRIBUTE_V.ValueCount);
						int num5 = 0;
						while ((long)num5 < (long)((ulong)claim_SECURITY_ATTRIBUTE_V.ValueCount))
						{
							instanceClaims.Add(new Claim(claim_SECURITY_ATTRIBUTE_V.Name, Marshal.PtrToStringAuto(array3[num5]), "http://www.w3.org/2001/XMLSchema#string", this.m_issuerName, this.m_issuerName, this, propertyValue, string.Empty));
							num5++;
						}
						break;
					}
					case 6:
					{
						long[] array4 = new long[claim_SECURITY_ATTRIBUTE_V.ValueCount];
						Marshal.Copy(claim_SECURITY_ATTRIBUTE_V.Values.pUint64, array4, 0, (int)claim_SECURITY_ATTRIBUTE_V.ValueCount);
						int num6 = 0;
						while ((long)num6 < (long)((ulong)claim_SECURITY_ATTRIBUTE_V.ValueCount))
						{
							instanceClaims.Add(new Claim(claim_SECURITY_ATTRIBUTE_V.Name, (array4[num6] == 0L) ? Convert.ToString(false, CultureInfo.InvariantCulture) : Convert.ToString(true, CultureInfo.InvariantCulture), "http://www.w3.org/2001/XMLSchema#boolean", this.m_issuerName, this.m_issuerName, this, propertyValue, string.Empty));
							num6++;
						}
						break;
					}
					}
					num += (long)Marshal.SizeOf<Win32Native.CLAIM_SECURITY_ATTRIBUTE_V1>(claim_SECURITY_ATTRIBUTE_V);
					num2++;
				}
			}
			finally
			{
				safeLocalAllocHandle.Close();
			}
		}

		// Token: 0x0400103B RID: 4155
		[SecurityCritical]
		private static SafeAccessTokenHandle s_invalidTokenHandle = SafeAccessTokenHandle.InvalidHandle;

		// Token: 0x0400103C RID: 4156
		private string m_name;

		// Token: 0x0400103D RID: 4157
		private SecurityIdentifier m_owner;

		// Token: 0x0400103E RID: 4158
		private SecurityIdentifier m_user;

		// Token: 0x0400103F RID: 4159
		private object m_groups;

		// Token: 0x04001040 RID: 4160
		[SecurityCritical]
		private SafeAccessTokenHandle m_safeTokenHandle = SafeAccessTokenHandle.InvalidHandle;

		// Token: 0x04001041 RID: 4161
		private string m_authType;

		// Token: 0x04001042 RID: 4162
		private int m_isAuthenticated = -1;

		// Token: 0x04001043 RID: 4163
		private volatile TokenImpersonationLevel m_impersonationLevel;

		// Token: 0x04001044 RID: 4164
		private volatile bool m_impersonationLevelInitialized;

		// Token: 0x04001045 RID: 4165
		private static RuntimeConstructorInfo s_specialSerializationCtor = typeof(WindowsIdentity).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(SerializationInfo) }, null) as RuntimeConstructorInfo;

		/// <summary>Identifies the name of the default <see cref="T:System.Security.Claims.ClaimsIdentity" /> issuer.</summary>
		// Token: 0x04001046 RID: 4166
		[NonSerialized]
		public new const string DefaultIssuer = "AD AUTHORITY";

		// Token: 0x04001047 RID: 4167
		[NonSerialized]
		private string m_issuerName = "AD AUTHORITY";

		// Token: 0x04001048 RID: 4168
		[NonSerialized]
		private object m_claimsIntiailizedLock = new object();

		// Token: 0x04001049 RID: 4169
		[NonSerialized]
		private volatile bool m_claimsInitialized;

		// Token: 0x0400104A RID: 4170
		[NonSerialized]
		private List<Claim> m_deviceClaims;

		// Token: 0x0400104B RID: 4171
		[NonSerialized]
		private List<Claim> m_userClaims;
	}
}
