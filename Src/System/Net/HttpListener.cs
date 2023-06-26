using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	/// <summary>Provides a simple, programmatically controlled HTTP protocol listener. This class cannot be inherited.</summary>
	// Token: 0x020000F7 RID: 247
	public sealed class HttpListener : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListener" /> class.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">This class cannot be used on the current operating system. Windows Server 2003 or Windows XP SP2 is required to use instances of this class.</exception>
		// Token: 0x06000894 RID: 2196 RVA: 0x0002F830 File Offset: 0x0002DA30
		public HttpListener()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "HttpListener", "");
			}
			if (!UnsafeNclNativeMethods.HttpApi.Supported)
			{
				throw new PlatformNotSupportedException();
			}
			this.m_State = HttpListener.State.Stopped;
			this.m_InternalLock = new object();
			this.m_DefaultServiceNames = new ServiceNameStore();
			this.m_TimeoutManager = new HttpListenerTimeoutManager(this);
			this.m_ExtendedProtectionPolicy = new ExtendedProtectionPolicy(PolicyEnforcement.Never);
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "HttpListener", "");
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0002F8D5 File Offset: 0x0002DAD5
		internal CriticalHandle RequestQueueHandle
		{
			get
			{
				return this.m_RequestQueueHandle;
			}
		}

		/// <summary>Gets or sets the delegate called to determine the protocol used to authenticate clients.</summary>
		/// <returns>An <see cref="T:System.Net.AuthenticationSchemeSelector" /> delegate that invokes the method used to select an authentication protocol. The default value is <see langword="null" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0002F8E0 File Offset: 0x0002DAE0
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x0002F900 File Offset: 0x0002DB00
		public AuthenticationSchemeSelector AuthenticationSchemeSelectorDelegate
		{
			get
			{
				HttpListener.AuthenticationSelectorInfo authenticationDelegate = this.m_AuthenticationDelegate;
				if (authenticationDelegate != null)
				{
					return authenticationDelegate.Delegate;
				}
				return null;
			}
			set
			{
				this.CheckDisposed();
				try
				{
					new SecurityPermission(SecurityPermissionFlag.ControlPrincipal).Demand();
					this.m_AuthenticationDelegate = new HttpListener.AuthenticationSelectorInfo(value, true);
				}
				catch (SecurityException ex)
				{
					this.m_SecurityException = ex;
					this.m_AuthenticationDelegate = new HttpListener.AuthenticationSelectorInfo(value, false);
				}
			}
		}

		/// <summary>Gets or sets the delegate called to determine the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> to use for each request.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that specifies the policy to use for extended protection.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionSelectorDelegate" /> property, but the <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> property must be <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionSelectorDelegate" /> property to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionSelectorDelegate" /> property after the <see cref="M:System.Net.HttpListener.Start" /> method was already called.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionSelectorDelegate" /> property on a platform that does not support extended protection.</exception>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0002F958 File Offset: 0x0002DB58
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x0002F960 File Offset: 0x0002DB60
		public HttpListener.ExtendedProtectionSelector ExtendedProtectionSelectorDelegate
		{
			get
			{
				return this.m_ExtendedProtectionSelectorDelegate;
			}
			set
			{
				this.CheckDisposed();
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (!AuthenticationManager.OSSupportsExtendedProtection)
				{
					throw new PlatformNotSupportedException(SR.GetString("security_ExtendedProtection_NoOSSupport"));
				}
				this.m_ExtendedProtectionSelectorDelegate = value;
			}
		}

		/// <summary>Gets or sets the scheme used to authenticate clients.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Net.AuthenticationSchemes" /> enumeration values that indicates how clients are to be authenticated. The default value is <see cref="F:System.Net.AuthenticationSchemes.Anonymous" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0002F98F File Offset: 0x0002DB8F
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x0002F997 File Offset: 0x0002DB97
		public AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return this.m_AuthenticationScheme;
			}
			set
			{
				this.CheckDisposed();
				if ((value & (AuthenticationSchemes.Digest | AuthenticationSchemes.Negotiate | AuthenticationSchemes.Ntlm)) != AuthenticationSchemes.None)
				{
					new SecurityPermission(SecurityPermissionFlag.ControlPrincipal).Demand();
				}
				this.m_AuthenticationScheme = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> to use for extended protection for a session.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that specifies the policy to use for extended protection.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionPolicy" /> property, but the <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> property was not <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionPolicy" /> property to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionPolicy" /> property after the <see cref="M:System.Net.HttpListener.Start" /> method was already called.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.PolicyEnforcement" /> property was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0002F9BA File Offset: 0x0002DBBA
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x0002F9C4 File Offset: 0x0002DBC4
		public ExtendedProtectionPolicy ExtendedProtectionPolicy
		{
			get
			{
				return this.m_ExtendedProtectionPolicy;
			}
			set
			{
				this.CheckDisposed();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!AuthenticationManager.OSSupportsExtendedProtection && value.PolicyEnforcement == PolicyEnforcement.Always)
				{
					throw new PlatformNotSupportedException(SR.GetString("security_ExtendedProtection_NoOSSupport"));
				}
				if (value.CustomChannelBinding != null)
				{
					throw new ArgumentException(SR.GetString("net_listener_cannot_set_custom_cbt"), "CustomChannelBinding");
				}
				this.m_ExtendedProtectionPolicy = value;
			}
		}

		/// <summary>Gets a default list of Service Provider Names (SPNs) as determined by registered prefixes.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ServiceNameCollection" /> that contains a list of SPNs.</returns>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0002FA29 File Offset: 0x0002DC29
		public ServiceNameCollection DefaultServiceNames
		{
			get
			{
				return this.m_DefaultServiceNames.ServiceNames;
			}
		}

		/// <summary>Gets or sets the realm, or resource partition, associated with this <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the name of the realm associated with the <see cref="T:System.Net.HttpListener" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0002FA36 File Offset: 0x0002DC36
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0002FA3E File Offset: 0x0002DC3E
		public string Realm
		{
			get
			{
				return this.m_Realm;
			}
			set
			{
				this.CheckDisposed();
				this.m_Realm = value;
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002FA50 File Offset: 0x0002DC50
		private void ValidateV2Property()
		{
			object internalLock = this.m_InternalLock;
			lock (internalLock)
			{
				this.CheckDisposed();
				this.SetupV2Config();
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002FA98 File Offset: 0x0002DC98
		private void SetUrlGroupProperty(UnsafeNclNativeMethods.HttpApi.HTTP_SERVER_PROPERTY property, IntPtr info, uint infosize)
		{
			uint num = UnsafeNclNativeMethods.HttpApi.HttpSetUrlGroupProperty(this.m_UrlGroupId, property, info, infosize);
			if (num != 0U)
			{
				HttpListenerException ex = new HttpListenerException((int)num);
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "HttpSetUrlGroupProperty:: Property: " + property.ToString(), ex);
				}
				throw ex;
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002FAEC File Offset: 0x0002DCEC
		internal unsafe void SetServerTimeout(int[] timeouts, uint minSendBytesPerSecond)
		{
			this.ValidateV2Property();
			UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_LIMIT_INFO http_TIMEOUT_LIMIT_INFO = default(UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_LIMIT_INFO);
			http_TIMEOUT_LIMIT_INFO.Flags = UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_RECEIVE_REQUEST_FLAG_COPY_BODY;
			http_TIMEOUT_LIMIT_INFO.DrainEntityBody = (ushort)timeouts[1];
			http_TIMEOUT_LIMIT_INFO.EntityBody = (ushort)timeouts[0];
			http_TIMEOUT_LIMIT_INFO.RequestQueue = (ushort)timeouts[2];
			http_TIMEOUT_LIMIT_INFO.IdleConnection = (ushort)timeouts[3];
			http_TIMEOUT_LIMIT_INFO.HeaderWait = (ushort)timeouts[4];
			http_TIMEOUT_LIMIT_INFO.MinSendRate = minSendBytesPerSecond;
			IntPtr intPtr = new IntPtr((void*)(&http_TIMEOUT_LIMIT_INFO));
			this.SetUrlGroupProperty(UnsafeNclNativeMethods.HttpApi.HTTP_SERVER_PROPERTY.HttpServerTimeoutsProperty, intPtr, (uint)Marshal.SizeOf(typeof(UnsafeNclNativeMethods.HttpApi.HTTP_TIMEOUT_LIMIT_INFO)));
		}

		/// <summary>The timeout manager for this <see cref="T:System.Net.HttpListener" /> instance.</summary>
		/// <returns>The timeout manager for this <see cref="T:System.Net.HttpListener" /> instance.</returns>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0002FB6F File Offset: 0x0002DD6F
		public HttpListenerTimeoutManager TimeoutManager
		{
			get
			{
				this.ValidateV2Property();
				return this.m_TimeoutManager;
			}
		}

		/// <summary>Gets a value that indicates whether <see cref="T:System.Net.HttpListener" /> can be used with the current operating system.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="T:System.Net.HttpListener" /> is supported; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0002FB7D File Offset: 0x0002DD7D
		public static bool IsSupported
		{
			get
			{
				return UnsafeNclNativeMethods.HttpApi.Supported;
			}
		}

		/// <summary>Gets a value that indicates whether <see cref="T:System.Net.HttpListener" /> has been started.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.HttpListener" /> was started; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0002FB84 File Offset: 0x0002DD84
		public bool IsListening
		{
			get
			{
				return this.m_State == HttpListener.State.Started;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether your application receives exceptions that occur when an <see cref="T:System.Net.HttpListener" /> sends the response to the client.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Net.HttpListener" /> should not return exceptions that occur when sending the response to the client; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0002FB91 File Offset: 0x0002DD91
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0002FB99 File Offset: 0x0002DD99
		public bool IgnoreWriteExceptions
		{
			get
			{
				return this.m_IgnoreWriteExceptions;
			}
			set
			{
				this.CheckDisposed();
				this.m_IgnoreWriteExceptions = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether, when NTLM is used, additional requests using the same Transmission Control Protocol (TCP) connection are required to authenticate.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Principal.IIdentity" /> of the first request will be used for subsequent requests on the same connection; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0002FBA8 File Offset: 0x0002DDA8
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x0002FBB0 File Offset: 0x0002DDB0
		public bool UnsafeConnectionNtlmAuthentication
		{
			get
			{
				return this.m_UnsafeConnectionNtlmAuthentication;
			}
			set
			{
				this.CheckDisposed();
				if (this.m_UnsafeConnectionNtlmAuthentication == value)
				{
					return;
				}
				object syncRoot = this.DisconnectResults.SyncRoot;
				lock (syncRoot)
				{
					if (this.m_UnsafeConnectionNtlmAuthentication != value)
					{
						this.m_UnsafeConnectionNtlmAuthentication = value;
						if (!value)
						{
							foreach (object obj in this.DisconnectResults.Values)
							{
								HttpListener.DisconnectAsyncResult disconnectAsyncResult = (HttpListener.DisconnectAsyncResult)obj;
								disconnectAsyncResult.AuthenticatedConnection = null;
							}
						}
					}
				}
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0002FC64 File Offset: 0x0002DE64
		private Hashtable DisconnectResults
		{
			get
			{
				if (this.m_DisconnectResults == null)
				{
					object internalLock = this.m_InternalLock;
					lock (internalLock)
					{
						if (this.m_DisconnectResults == null)
						{
							this.m_DisconnectResults = Hashtable.Synchronized(new Hashtable());
						}
					}
				}
				return this.m_DisconnectResults;
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0002FCC4 File Offset: 0x0002DEC4
		internal unsafe void AddPrefix(string uriPrefix)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "AddPrefix", "uriPrefix:" + uriPrefix);
			}
			string text = null;
			try
			{
				if (uriPrefix == null)
				{
					throw new ArgumentNullException("uriPrefix");
				}
				new WebPermission(NetworkAccess.Accept, uriPrefix).Demand();
				this.CheckDisposed();
				int num;
				if (string.Compare(uriPrefix, 0, "http://", 0, 7, StringComparison.OrdinalIgnoreCase) == 0)
				{
					num = 7;
				}
				else
				{
					if (string.Compare(uriPrefix, 0, "https://", 0, 8, StringComparison.OrdinalIgnoreCase) != 0)
					{
						throw new ArgumentException(SR.GetString("net_listener_scheme"), "uriPrefix");
					}
					num = 8;
				}
				bool flag = false;
				int num2 = num;
				while (num2 < uriPrefix.Length && uriPrefix[num2] != '/' && (uriPrefix[num2] != ':' || flag))
				{
					if (uriPrefix[num2] == '[')
					{
						if (flag)
						{
							num2 = num;
							break;
						}
						flag = true;
					}
					if (flag && uriPrefix[num2] == ']')
					{
						flag = false;
					}
					num2++;
				}
				if (num == num2)
				{
					throw new ArgumentException(SR.GetString("net_listener_host"), "uriPrefix");
				}
				if (uriPrefix[uriPrefix.Length - 1] != '/')
				{
					throw new ArgumentException(SR.GetString("net_listener_slash"), "uriPrefix");
				}
				text = ((uriPrefix[num2] == ':') ? string.Copy(uriPrefix) : (uriPrefix.Substring(0, num2) + ((num == 7) ? ":80" : ":443") + uriPrefix.Substring(num2)));
				try
				{
					fixed (string text2 = text)
					{
						char* ptr = text2;
						if (ptr != null)
						{
							ptr += RuntimeHelpers.OffsetToStringData / 2;
						}
						num = 0;
						while (ptr[num] != ':')
						{
							ptr[num] = (char)CaseInsensitiveAscii.AsciiToLower[(int)((byte)ptr[num])];
							num++;
						}
					}
				}
				finally
				{
					string text2 = null;
				}
				if (this.m_State == HttpListener.State.Started)
				{
					uint num3 = this.InternalAddPrefix(text);
					if (num3 != 0U)
					{
						if (num3 == 183U)
						{
							throw new HttpListenerException((int)num3, SR.GetString("net_listener_already", new object[] { text }));
						}
						throw new HttpListenerException((int)num3);
					}
				}
				this.m_UriPrefixes[uriPrefix] = text;
				this.m_DefaultServiceNames.Add(uriPrefix);
			}
			catch (Exception ex)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "AddPrefix", ex);
				}
				throw;
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "AddPrefix", "prefix:" + text);
				}
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) prefixes handled by this <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerPrefixCollection" /> that contains the URI prefixes that this <see cref="T:System.Net.HttpListener" /> object is configured to handle.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0002FF58 File Offset: 0x0002E158
		public HttpListenerPrefixCollection Prefixes
		{
			get
			{
				if (Logging.On)
				{
					Logging.Enter(Logging.HttpListener, this, "Prefixes_get", "");
				}
				this.CheckDisposed();
				if (this.m_Prefixes == null)
				{
					this.m_Prefixes = new HttpListenerPrefixCollection(this);
				}
				return this.m_Prefixes;
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0002FF98 File Offset: 0x0002E198
		internal bool RemovePrefix(string uriPrefix)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "RemovePrefix", "uriPrefix:" + uriPrefix);
			}
			try
			{
				this.CheckDisposed();
				if (uriPrefix == null)
				{
					throw new ArgumentNullException("uriPrefix");
				}
				if (!this.m_UriPrefixes.Contains(uriPrefix))
				{
					return false;
				}
				if (this.m_State == HttpListener.State.Started)
				{
					this.InternalRemovePrefix((string)this.m_UriPrefixes[uriPrefix]);
				}
				this.m_UriPrefixes.Remove(uriPrefix);
				this.m_DefaultServiceNames.Remove(uriPrefix);
			}
			catch (Exception ex)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "RemovePrefix", ex);
				}
				throw;
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "RemovePrefix", "uriPrefix:" + uriPrefix);
				}
			}
			return true;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0003008C File Offset: 0x0002E28C
		internal void RemoveAll(bool clear)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "RemoveAll", "");
			}
			try
			{
				this.CheckDisposed();
				if (this.m_UriPrefixes.Count > 0)
				{
					if (this.m_State == HttpListener.State.Started)
					{
						foreach (object obj in this.m_UriPrefixes.Values)
						{
							string text = (string)obj;
							this.InternalRemovePrefix(text);
						}
					}
					if (clear)
					{
						this.m_UriPrefixes.Clear();
						this.m_DefaultServiceNames.Clear();
					}
				}
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "RemoveAll", "");
				}
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0003016C File Offset: 0x0002E36C
		private IntPtr DangerousGetHandle()
		{
			return ((HttpRequestQueueV2Handle)this.m_RequestQueueHandle).DangerousGetHandle();
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00030180 File Offset: 0x0002E380
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal void EnsureBoundHandle()
		{
			if (!this.m_RequestHandleBound)
			{
				object internalLock = this.m_InternalLock;
				lock (internalLock)
				{
					if (!this.m_RequestHandleBound)
					{
						ThreadPool.BindHandle(this.DangerousGetHandle());
						this.m_RequestHandleBound = true;
					}
				}
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000301E0 File Offset: 0x0002E3E0
		private unsafe void SetupV2Config()
		{
			ulong num = 0UL;
			if (this.m_V2Initialized)
			{
				return;
			}
			try
			{
				uint num2 = UnsafeNclNativeMethods.HttpApi.HttpCreateServerSession(UnsafeNclNativeMethods.HttpApi.Version, &num, 0U);
				if (num2 != 0U)
				{
					throw new HttpListenerException((int)num2);
				}
				this.m_ServerSessionHandle = new HttpServerSessionHandle(num);
				num = 0UL;
				num2 = UnsafeNclNativeMethods.HttpApi.HttpCreateUrlGroup(this.m_ServerSessionHandle.DangerousGetServerSessionId(), &num, 0U);
				if (num2 != 0U)
				{
					throw new HttpListenerException((int)num2);
				}
				this.m_UrlGroupId = num;
				this.m_V2Initialized = true;
			}
			catch (Exception ex)
			{
				this.m_State = HttpListener.State.Closed;
				if (this.m_ServerSessionHandle != null)
				{
					this.m_ServerSessionHandle.Close();
				}
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "SetupV2Config", ex);
				}
				throw;
			}
		}

		/// <summary>Allows this instance to receive incoming requests.</summary>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x060008B3 RID: 2227 RVA: 0x0003029C File Offset: 0x0002E49C
		public void Start()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Start", "");
			}
			object internalLock = this.m_InternalLock;
			lock (internalLock)
			{
				try
				{
					this.CheckDisposed();
					if (this.m_State != HttpListener.State.Started)
					{
						this.SetupV2Config();
						this.CreateRequestQueueHandle();
						this.AttachRequestQueueToUrlGroup();
						try
						{
							this.AddAllPrefixes();
						}
						catch (HttpListenerException)
						{
							this.DetachRequestQueueFromUrlGroup();
							this.ClearDigestCache();
							throw;
						}
						this.m_State = HttpListener.State.Started;
					}
				}
				catch (Exception ex)
				{
					this.m_State = HttpListener.State.Closed;
					this.CloseRequestQueueHandle();
					this.CleanupV2Config();
					if (Logging.On)
					{
						Logging.Exception(Logging.HttpListener, this, "Start", ex);
					}
					throw;
				}
				finally
				{
					if (Logging.On)
					{
						Logging.Exit(Logging.HttpListener, this, "Start", "");
					}
				}
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000303AC File Offset: 0x0002E5AC
		private void CleanupV2Config()
		{
			if (!this.m_V2Initialized)
			{
				return;
			}
			uint num = UnsafeNclNativeMethods.HttpApi.HttpCloseUrlGroup(this.m_UrlGroupId);
			if (num != 0U && Logging.On)
			{
				Logging.PrintError(Logging.HttpListener, this, "CloseV2Config", SR.GetString("net_listener_close_urlgroup_error", new object[] { num }));
			}
			this.m_UrlGroupId = 0UL;
			this.m_ServerSessionHandle.Close();
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00030414 File Offset: 0x0002E614
		private unsafe void AttachRequestQueueToUrlGroup()
		{
			UnsafeNclNativeMethods.HttpApi.HTTP_BINDING_INFO http_BINDING_INFO = default(UnsafeNclNativeMethods.HttpApi.HTTP_BINDING_INFO);
			http_BINDING_INFO.Flags = UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_RECEIVE_REQUEST_FLAG_COPY_BODY;
			http_BINDING_INFO.RequestQueueHandle = this.DangerousGetHandle();
			IntPtr intPtr = new IntPtr((void*)(&http_BINDING_INFO));
			this.SetUrlGroupProperty(UnsafeNclNativeMethods.HttpApi.HTTP_SERVER_PROPERTY.HttpServerBindingProperty, intPtr, (uint)Marshal.SizeOf(typeof(UnsafeNclNativeMethods.HttpApi.HTTP_BINDING_INFO)));
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00030460 File Offset: 0x0002E660
		private unsafe void DetachRequestQueueFromUrlGroup()
		{
			UnsafeNclNativeMethods.HttpApi.HTTP_BINDING_INFO http_BINDING_INFO = default(UnsafeNclNativeMethods.HttpApi.HTTP_BINDING_INFO);
			http_BINDING_INFO.Flags = UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE;
			http_BINDING_INFO.RequestQueueHandle = IntPtr.Zero;
			IntPtr intPtr = new IntPtr((void*)(&http_BINDING_INFO));
			uint num = UnsafeNclNativeMethods.HttpApi.HttpSetUrlGroupProperty(this.m_UrlGroupId, UnsafeNclNativeMethods.HttpApi.HTTP_SERVER_PROPERTY.HttpServerBindingProperty, intPtr, (uint)Marshal.SizeOf(typeof(UnsafeNclNativeMethods.HttpApi.HTTP_BINDING_INFO)));
			if (num != 0U && Logging.On)
			{
				Logging.PrintError(Logging.HttpListener, this, "DetachRequestQueueFromUrlGroup", SR.GetString("net_listener_detach_error", new object[] { num }));
			}
		}

		/// <summary>Causes this instance to stop receiving incoming requests.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x060008B7 RID: 2231 RVA: 0x000304E4 File Offset: 0x0002E6E4
		public void Stop()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Stop", "");
			}
			try
			{
				object internalLock = this.m_InternalLock;
				lock (internalLock)
				{
					this.CheckDisposed();
					if (this.m_State == HttpListener.State.Stopped)
					{
						return;
					}
					this.RemoveAll(false);
					this.DetachRequestQueueFromUrlGroup();
					this.CloseRequestQueueHandle();
					this.m_State = HttpListener.State.Stopped;
				}
				this.ClearDigestCache();
			}
			catch (Exception ex)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "Stop", ex);
				}
				throw;
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "Stop", "");
				}
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x000305C4 File Offset: 0x0002E7C4
		private void CreateRequestQueueHandle()
		{
			HttpRequestQueueV2Handle httpRequestQueueV2Handle = null;
			uint num = UnsafeNclNativeMethods.SafeNetHandles.HttpCreateRequestQueue(UnsafeNclNativeMethods.HttpApi.Version, null, null, 0U, out httpRequestQueueV2Handle);
			if (num != 0U)
			{
				throw new HttpListenerException((int)num);
			}
			if (HttpListener.SkipIOCPCallbackOnSuccess && !UnsafeNclNativeMethods.SetFileCompletionNotificationModes(httpRequestQueueV2Handle, UnsafeNclNativeMethods.FileCompletionNotificationModes.SkipCompletionPortOnSuccess | UnsafeNclNativeMethods.FileCompletionNotificationModes.SkipSetEventOnHandle))
			{
				throw new HttpListenerException(Marshal.GetLastWin32Error());
			}
			this.m_RequestQueueHandle = httpRequestQueueV2Handle;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00030611 File Offset: 0x0002E811
		private void CloseRequestQueueHandle()
		{
			if (this.m_RequestQueueHandle != null && !this.m_RequestQueueHandle.IsInvalid)
			{
				this.m_RequestQueueHandle.Close();
				this.m_RequestHandleBound = false;
			}
		}

		/// <summary>Shuts down the <see cref="T:System.Net.HttpListener" /> object immediately, discarding all currently queued requests.</summary>
		// Token: 0x060008BA RID: 2234 RVA: 0x0003063C File Offset: 0x0002E83C
		public void Abort()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Abort", "");
			}
			object internalLock = this.m_InternalLock;
			lock (internalLock)
			{
				try
				{
					if (this.m_State != HttpListener.State.Closed)
					{
						if (this.m_State == HttpListener.State.Started)
						{
							this.DetachRequestQueueFromUrlGroup();
							this.CloseRequestQueueHandle();
						}
						this.CleanupV2Config();
						this.ClearDigestCache();
					}
				}
				catch (Exception ex)
				{
					if (Logging.On)
					{
						Logging.Exception(Logging.HttpListener, this, "Abort", ex);
					}
					throw;
				}
				finally
				{
					this.m_State = HttpListener.State.Closed;
					if (Logging.On)
					{
						Logging.Exit(Logging.HttpListener, this, "Abort", "");
					}
				}
			}
		}

		/// <summary>Shuts down the <see cref="T:System.Net.HttpListener" />.</summary>
		// Token: 0x060008BB RID: 2235 RVA: 0x0003071C File Offset: 0x0002E91C
		public void Close()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Close", "");
			}
			try
			{
				((IDisposable)this).Dispose();
			}
			catch (Exception ex)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "Close", ex);
				}
				throw;
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "Close", "");
				}
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000307A4 File Offset: 0x0002E9A4
		private void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Dispose", "");
			}
			object internalLock = this.m_InternalLock;
			lock (internalLock)
			{
				try
				{
					if (this.m_State != HttpListener.State.Closed)
					{
						this.Stop();
						this.CleanupV2Config();
					}
				}
				catch (Exception ex)
				{
					if (Logging.On)
					{
						Logging.Exception(Logging.HttpListener, this, "Dispose", ex);
					}
					throw;
				}
				finally
				{
					this.m_State = HttpListener.State.Closed;
					if (Logging.On)
					{
						Logging.Exit(Logging.HttpListener, this, "Dispose", "");
					}
				}
			}
		}

		/// <summary>Releases the resources held by this <see cref="T:System.Net.HttpListener" /> object.</summary>
		// Token: 0x060008BD RID: 2237 RVA: 0x00030870 File Offset: 0x0002EA70
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0003087C File Offset: 0x0002EA7C
		private uint InternalAddPrefix(string uriPrefix)
		{
			return UnsafeNclNativeMethods.HttpApi.HttpAddUrlToUrlGroup(this.m_UrlGroupId, uriPrefix, 0UL, 0U);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0003089C File Offset: 0x0002EA9C
		private bool InternalRemovePrefix(string uriPrefix)
		{
			uint num = UnsafeNclNativeMethods.HttpApi.HttpRemoveUrlFromUrlGroup(this.m_UrlGroupId, uriPrefix, 0U);
			return num != 1168U;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x000308C4 File Offset: 0x0002EAC4
		private void AddAllPrefixes()
		{
			if (this.m_UriPrefixes.Count > 0)
			{
				foreach (object obj in this.m_UriPrefixes.Values)
				{
					string text = (string)obj;
					uint num = this.InternalAddPrefix(text);
					if (num != 0U)
					{
						if (num == 183U)
						{
							throw new HttpListenerException((int)num, SR.GetString("net_listener_already", new object[] { text }));
						}
						throw new HttpListenerException((int)num);
					}
				}
			}
		}

		/// <summary>Waits for an incoming request and returns when one is received.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerContext" /> object that represents a client request.</returns>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.InvalidOperationException">This object has not been started or is currently stopped.  
		///  -or-  
		///  The <see cref="T:System.Net.HttpListener" /> does not have any Uniform Resource Identifier (URI) prefixes to respond to.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x060008C1 RID: 2241 RVA: 0x00030960 File Offset: 0x0002EB60
		public unsafe HttpListenerContext GetContext()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "GetContext", "");
			}
			SyncRequestContext syncRequestContext = null;
			HttpListenerContext httpListenerContext = null;
			bool flag = false;
			checked
			{
				HttpListenerContext httpListenerContext2;
				try
				{
					this.CheckDisposed();
					if (this.m_State == HttpListener.State.Stopped)
					{
						throw new InvalidOperationException(SR.GetString("net_listener_mustcall", new object[] { "Start()" }));
					}
					if (this.m_UriPrefixes.Count == 0)
					{
						throw new InvalidOperationException(SR.GetString("net_listener_mustcall", new object[] { "AddPrefix()" }));
					}
					uint num = 4096U;
					ulong num2 = 0UL;
					syncRequestContext = new SyncRequestContext((int)num);
					uint num4;
					for (;;)
					{
						uint num3 = 0U;
						num4 = UnsafeNclNativeMethods.HttpApi.HttpReceiveHttpRequest(this.m_RequestQueueHandle, num2, 1U, syncRequestContext.RequestBlob, num, &num3, null);
						if (num4 == 87U && num2 != 0UL)
						{
							num2 = 0UL;
						}
						else if (num4 == 234U)
						{
							num = num3;
							num2 = syncRequestContext.RequestBlob->RequestId;
							syncRequestContext.Reset((int)num);
						}
						else
						{
							if (num4 != 0U)
							{
								break;
							}
							if (this.ValidateRequest(syncRequestContext))
							{
								httpListenerContext = this.HandleAuthentication(syncRequestContext, out flag);
							}
							if (flag)
							{
								syncRequestContext = null;
								flag = false;
							}
							if (httpListenerContext != null)
							{
								goto Block_13;
							}
							if (syncRequestContext == null)
							{
								syncRequestContext = new SyncRequestContext((int)num);
							}
							num2 = 0UL;
						}
					}
					throw new HttpListenerException((int)num4);
					Block_13:
					httpListenerContext2 = httpListenerContext;
				}
				catch (Exception ex)
				{
					if (Logging.On)
					{
						Logging.Exception(Logging.HttpListener, this, "GetContext", ex);
					}
					throw;
				}
				finally
				{
					if (syncRequestContext != null && !flag)
					{
						syncRequestContext.ReleasePins();
						syncRequestContext.Close();
					}
					if (Logging.On)
					{
						Logging.Exit(Logging.HttpListener, this, "GetContext", "HttpListenerContext#" + ValidationHelper.HashString(httpListenerContext) + " RequestTraceIdentifier#" + ((httpListenerContext != null) ? httpListenerContext.Request.RequestTraceIdentifier.ToString() : "<null>"));
					}
				}
				return httpListenerContext2;
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00030B4C File Offset: 0x0002ED4C
		internal unsafe bool ValidateRequest(RequestContextBase requestMemory)
		{
			if (requestMemory.RequestBlob->Headers.UnknownHeaderCount > 1000)
			{
				this.SendError(requestMemory.RequestBlob->RequestId, HttpStatusCode.BadRequest, null);
				return false;
			}
			return true;
		}

		/// <summary>Begins asynchronously retrieving an incoming request.</summary>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when a client request is available.</param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="callback" /> delegate when the operation completes.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that indicates the status of the asynchronous operation.</returns>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.InvalidOperationException">This object has not been started or is currently stopped.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x060008C3 RID: 2243 RVA: 0x00030B80 File Offset: 0x0002ED80
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginGetContext(AsyncCallback callback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "BeginGetContext", "");
			}
			ListenerAsyncResult listenerAsyncResult = null;
			try
			{
				this.CheckDisposed();
				if (this.m_State == HttpListener.State.Stopped)
				{
					throw new InvalidOperationException(SR.GetString("net_listener_mustcall", new object[] { "Start()" }));
				}
				listenerAsyncResult = new ListenerAsyncResult(this, state, callback);
				uint num = listenerAsyncResult.QueueBeginGetContext();
				if (num != 0U && num != 997U)
				{
					throw new HttpListenerException((int)num);
				}
			}
			catch (Exception ex)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "BeginGetContext", ex);
				}
				throw;
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Enter(Logging.HttpListener, this, "BeginGetContext", "IAsyncResult#" + ValidationHelper.HashString(listenerAsyncResult));
				}
			}
			return listenerAsyncResult;
		}

		/// <summary>Completes an asynchronous operation to retrieve an incoming client request.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that was obtained when the asynchronous operation was started.</param>
		/// <returns>An <see cref="T:System.Net.HttpListenerContext" /> object that represents the client request.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling the <see cref="M:System.Net.HttpListener.BeginGetContext(System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.HttpListener.EndGetContext(System.IAsyncResult)" /> method was already called for the specified <paramref name="asyncResult" /> object.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x060008C4 RID: 2244 RVA: 0x00030C60 File Offset: 0x0002EE60
		public HttpListenerContext EndGetContext(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "EndGetContext", "IAsyncResult#" + ValidationHelper.HashString(asyncResult));
			}
			HttpListenerContext httpListenerContext = null;
			try
			{
				this.CheckDisposed();
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				ListenerAsyncResult listenerAsyncResult = asyncResult as ListenerAsyncResult;
				if (listenerAsyncResult == null || listenerAsyncResult.AsyncObject != this)
				{
					throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
				}
				if (listenerAsyncResult.EndCalled)
				{
					throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndGetContext" }));
				}
				listenerAsyncResult.EndCalled = true;
				httpListenerContext = listenerAsyncResult.InternalWaitForCompletion() as HttpListenerContext;
				if (httpListenerContext == null)
				{
					throw listenerAsyncResult.Result as Exception;
				}
			}
			catch (Exception ex)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "EndGetContext", ex);
				}
				throw;
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "EndGetContext", (httpListenerContext == null) ? "<no context>" : ("HttpListenerContext#" + ValidationHelper.HashString(httpListenerContext) + " RequestTraceIdentifier#" + httpListenerContext.Request.RequestTraceIdentifier.ToString()));
				}
			}
			return httpListenerContext;
		}

		/// <summary>Waits for an incoming request as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.HttpListenerContext" /> object that represents a client request.</returns>
		// Token: 0x060008C5 RID: 2245 RVA: 0x00030DA4 File Offset: 0x0002EFA4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<HttpListenerContext> GetContextAsync()
		{
			return Task<HttpListenerContext>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetContext), new Func<IAsyncResult, HttpListenerContext>(this.EndGetContext), null);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00030DC9 File Offset: 0x0002EFC9
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		internal static WindowsIdentity CreateWindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType, bool isAuthenticated)
		{
			return new WindowsIdentity(userToken, type, acctType, isAuthenticated);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00030DD4 File Offset: 0x0002EFD4
		internal unsafe HttpListenerContext HandleAuthentication(RequestContextBase memoryBlob, out bool stoleBlob)
		{
			string text = null;
			stoleBlob = false;
			string verb = UnsafeNclNativeMethods.HttpApi.GetVerb(memoryBlob.RequestBlob);
			string knownHeader = UnsafeNclNativeMethods.HttpApi.GetKnownHeader(memoryBlob.RequestBlob, 24);
			ulong connectionId = memoryBlob.RequestBlob->ConnectionId;
			ulong requestId = memoryBlob.RequestBlob->RequestId;
			bool flag = memoryBlob.RequestBlob->pSslInfo != null;
			HttpListener.DisconnectAsyncResult disconnectAsyncResult = (HttpListener.DisconnectAsyncResult)this.DisconnectResults[connectionId];
			if (this.UnsafeConnectionNtlmAuthentication)
			{
				if (knownHeader == null)
				{
					WindowsPrincipal windowsPrincipal = ((disconnectAsyncResult == null) ? null : disconnectAsyncResult.AuthenticatedConnection);
					if (windowsPrincipal != null)
					{
						stoleBlob = true;
						HttpListenerContext httpListenerContext = new HttpListenerContext(this, memoryBlob);
						httpListenerContext.SetIdentity(windowsPrincipal, null);
						httpListenerContext.Request.ReleasePins();
						return httpListenerContext;
					}
				}
				else if (disconnectAsyncResult != null)
				{
					disconnectAsyncResult.AuthenticatedConnection = null;
				}
			}
			stoleBlob = true;
			HttpListenerContext httpListenerContext2 = null;
			NTAuthentication ntauthentication = null;
			NTAuthentication ntauthentication2 = null;
			NTAuthentication ntauthentication3 = null;
			AuthenticationSchemes authenticationSchemes = AuthenticationSchemes.None;
			AuthenticationSchemes authenticationSchemes2 = this.AuthenticationSchemes;
			ExtendedProtectionPolicy extendedProtectionPolicy = this.m_ExtendedProtectionPolicy;
			HttpListenerContext httpListenerContext3;
			try
			{
				if (disconnectAsyncResult != null && !disconnectAsyncResult.StartOwningDisconnectHandling())
				{
					disconnectAsyncResult = null;
				}
				if (disconnectAsyncResult != null)
				{
					ntauthentication = disconnectAsyncResult.Session;
				}
				httpListenerContext2 = new HttpListenerContext(this, memoryBlob);
				HttpListener.AuthenticationSelectorInfo authenticationDelegate = this.m_AuthenticationDelegate;
				if (authenticationDelegate != null)
				{
					try
					{
						httpListenerContext2.Request.ReleasePins();
						authenticationSchemes2 = authenticationDelegate.Delegate(httpListenerContext2.Request);
						httpListenerContext2.AuthenticationSchemes = authenticationSchemes2;
						if (!authenticationDelegate.AdvancedAuth && (authenticationSchemes2 & (AuthenticationSchemes.Digest | AuthenticationSchemes.Negotiate | AuthenticationSchemes.Ntlm)) != AuthenticationSchemes.None)
						{
							throw this.m_SecurityException;
						}
						goto IL_1A7;
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						if (Logging.On)
						{
							Logging.PrintError(Logging.HttpListener, this, "HandleAuthentication", SR.GetString("net_log_listener_delegate_exception", new object[] { ex }));
						}
						this.SendError(requestId, HttpStatusCode.InternalServerError, null);
						HttpListener.FreeContext(ref httpListenerContext2, memoryBlob);
						return null;
					}
				}
				stoleBlob = false;
				IL_1A7:
				HttpListener.ExtendedProtectionSelector extendedProtectionSelectorDelegate = this.m_ExtendedProtectionSelectorDelegate;
				if (extendedProtectionSelectorDelegate != null)
				{
					extendedProtectionPolicy = extendedProtectionSelectorDelegate(httpListenerContext2.Request);
					if (extendedProtectionPolicy == null)
					{
						extendedProtectionPolicy = new ExtendedProtectionPolicy(PolicyEnforcement.Never);
					}
					httpListenerContext2.ExtendedProtectionPolicy = extendedProtectionPolicy;
				}
				int num = -1;
				if (knownHeader != null && (authenticationSchemes2 & ~AuthenticationSchemes.Anonymous) != AuthenticationSchemes.None)
				{
					num = 0;
					while (num < knownHeader.Length && knownHeader[num] != ' ' && knownHeader[num] != '\t' && knownHeader[num] != '\r' && knownHeader[num] != '\n')
					{
						num++;
					}
					if (num < knownHeader.Length)
					{
						if ((authenticationSchemes2 & AuthenticationSchemes.Negotiate) != AuthenticationSchemes.None && string.Compare(knownHeader, 0, "Negotiate", 0, num, StringComparison.OrdinalIgnoreCase) == 0)
						{
							authenticationSchemes = AuthenticationSchemes.Negotiate;
						}
						else if ((authenticationSchemes2 & AuthenticationSchemes.Ntlm) != AuthenticationSchemes.None && string.Compare(knownHeader, 0, "NTLM", 0, num, StringComparison.OrdinalIgnoreCase) == 0)
						{
							authenticationSchemes = AuthenticationSchemes.Ntlm;
						}
						else if ((authenticationSchemes2 & AuthenticationSchemes.Digest) != AuthenticationSchemes.None && string.Compare(knownHeader, 0, "Digest", 0, num, StringComparison.OrdinalIgnoreCase) == 0)
						{
							authenticationSchemes = AuthenticationSchemes.Digest;
						}
						else if ((authenticationSchemes2 & AuthenticationSchemes.Basic) != AuthenticationSchemes.None && string.Compare(knownHeader, 0, "Basic", 0, num, StringComparison.OrdinalIgnoreCase) == 0)
						{
							authenticationSchemes = AuthenticationSchemes.Basic;
						}
						else if (Logging.On)
						{
							Logging.PrintWarning(Logging.HttpListener, this, "HandleAuthentication", SR.GetString("net_log_listener_unsupported_authentication_scheme", new object[] { knownHeader, authenticationSchemes2 }));
						}
					}
				}
				HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
				bool flag2 = false;
				if (authenticationSchemes == AuthenticationSchemes.None)
				{
					if (Logging.On)
					{
						Logging.PrintWarning(Logging.HttpListener, this, "HandleAuthentication", SR.GetString("net_log_listener_unmatched_authentication_scheme", new object[]
						{
							ValidationHelper.ToString(authenticationSchemes2),
							(knownHeader == null) ? "<null>" : knownHeader
						}));
					}
					if ((authenticationSchemes2 & AuthenticationSchemes.Anonymous) != AuthenticationSchemes.None)
					{
						if (!stoleBlob)
						{
							stoleBlob = true;
							httpListenerContext2.Request.ReleasePins();
						}
						return httpListenerContext2;
					}
					httpStatusCode = HttpStatusCode.Unauthorized;
					HttpListener.FreeContext(ref httpListenerContext2, memoryBlob);
				}
				else
				{
					byte[] array = null;
					byte[] array2 = null;
					string text2 = null;
					num++;
					while (num < knownHeader.Length && (knownHeader[num] == ' ' || knownHeader[num] == '\t' || knownHeader[num] == '\r' || knownHeader[num] == '\n'))
					{
						num++;
					}
					string text3 = ((num < knownHeader.Length) ? knownHeader.Substring(num) : "");
					IPrincipal principal = null;
					switch (authenticationSchemes)
					{
					case AuthenticationSchemes.Digest:
					{
						ChannelBinding channelBinding = this.GetChannelBinding(connectionId, flag, extendedProtectionPolicy);
						ntauthentication3 = new NTAuthentication(true, "WDigest", null, this.GetContextFlags(extendedProtectionPolicy, flag), channelBinding);
						SecurityStatus securityStatus;
						text2 = ntauthentication3.GetOutgoingDigestBlob(text3, verb, null, this.Realm, false, false, out securityStatus);
						if (securityStatus == SecurityStatus.OK)
						{
							text2 = null;
						}
						if (ntauthentication3.IsValidContext)
						{
							SafeCloseHandle safeCloseHandle = null;
							try
							{
								if (!this.CheckSpn(ntauthentication3, flag, extendedProtectionPolicy))
								{
									httpStatusCode = HttpStatusCode.Unauthorized;
								}
								else
								{
									httpListenerContext2.Request.ServiceName = ntauthentication3.ClientSpecifiedSpn;
									safeCloseHandle = ntauthentication3.GetContextToken(out securityStatus);
									if (securityStatus != SecurityStatus.OK)
									{
										httpStatusCode = this.HttpStatusFromSecurityStatus(securityStatus);
									}
									else if (safeCloseHandle == null)
									{
										httpStatusCode = HttpStatusCode.Unauthorized;
									}
									else
									{
										principal = new WindowsPrincipal(HttpListener.CreateWindowsIdentity(safeCloseHandle.DangerousGetHandle(), "Digest", WindowsAccountType.Normal, true));
									}
								}
							}
							finally
							{
								if (safeCloseHandle != null)
								{
									safeCloseHandle.Close();
								}
							}
							ntauthentication2 = ntauthentication3;
							if (text2 != null)
							{
								text = "Digest " + text2;
							}
						}
						else
						{
							httpStatusCode = this.HttpStatusFromSecurityStatus(securityStatus);
						}
						break;
					}
					case AuthenticationSchemes.Negotiate:
					case AuthenticationSchemes.Ntlm:
					{
						string text4 = ((authenticationSchemes == AuthenticationSchemes.Ntlm) ? "NTLM" : "Negotiate");
						if (ntauthentication != null && ntauthentication.Package == text4)
						{
							ntauthentication3 = ntauthentication;
						}
						else
						{
							ChannelBinding channelBinding = this.GetChannelBinding(connectionId, flag, extendedProtectionPolicy);
							ntauthentication3 = new NTAuthentication(true, text4, null, this.GetContextFlags(extendedProtectionPolicy, flag), channelBinding);
						}
						try
						{
							array = Convert.FromBase64String(text3);
						}
						catch (FormatException)
						{
							httpStatusCode = HttpStatusCode.BadRequest;
							flag2 = true;
						}
						if (!flag2)
						{
							SecurityStatus securityStatus;
							array2 = ntauthentication3.GetOutgoingBlob(array, false, out securityStatus);
							flag2 = !ntauthentication3.IsValidContext;
							if (flag2)
							{
								if (securityStatus == SecurityStatus.InvalidHandle && ntauthentication == null && array != null && array.Length != 0)
								{
									securityStatus = SecurityStatus.InvalidToken;
								}
								httpStatusCode = this.HttpStatusFromSecurityStatus(securityStatus);
							}
						}
						if (array2 != null)
						{
							text2 = Convert.ToBase64String(array2);
						}
						if (!flag2)
						{
							if (ntauthentication3.IsCompleted)
							{
								SafeCloseHandle safeCloseHandle2 = null;
								try
								{
									if (!this.CheckSpn(ntauthentication3, flag, extendedProtectionPolicy))
									{
										httpStatusCode = HttpStatusCode.Unauthorized;
									}
									else
									{
										httpListenerContext2.Request.ServiceName = ntauthentication3.ClientSpecifiedSpn;
										SecurityStatus securityStatus;
										safeCloseHandle2 = ntauthentication3.GetContextToken(out securityStatus);
										if (securityStatus != SecurityStatus.OK)
										{
											httpStatusCode = this.HttpStatusFromSecurityStatus(securityStatus);
										}
										else
										{
											WindowsPrincipal windowsPrincipal2 = new WindowsPrincipal(HttpListener.CreateWindowsIdentity(safeCloseHandle2.DangerousGetHandle(), ntauthentication3.ProtocolName, WindowsAccountType.Normal, true));
											principal = windowsPrincipal2;
											if (this.UnsafeConnectionNtlmAuthentication && ntauthentication3.ProtocolName == "NTLM")
											{
												if (disconnectAsyncResult == null)
												{
													this.RegisterForDisconnectNotification(connectionId, ref disconnectAsyncResult);
												}
												if (disconnectAsyncResult != null)
												{
													object syncRoot = this.DisconnectResults.SyncRoot;
													lock (syncRoot)
													{
														if (this.UnsafeConnectionNtlmAuthentication)
														{
															disconnectAsyncResult.AuthenticatedConnection = windowsPrincipal2;
														}
													}
												}
											}
										}
									}
									break;
								}
								finally
								{
									if (safeCloseHandle2 != null)
									{
										safeCloseHandle2.Close();
									}
								}
							}
							ntauthentication2 = ntauthentication3;
							text = ((authenticationSchemes == AuthenticationSchemes.Ntlm) ? "NTLM" : "Negotiate");
							if (!string.IsNullOrEmpty(text2))
							{
								text = text + " " + text2;
							}
						}
						break;
					}
					case AuthenticationSchemes.Digest | AuthenticationSchemes.Negotiate:
						break;
					default:
						if (authenticationSchemes == AuthenticationSchemes.Basic)
						{
							try
							{
								array = Convert.FromBase64String(text3);
								text3 = WebHeaderCollection.HeaderEncoding.GetString(array, 0, array.Length);
								num = text3.IndexOf(':');
								if (num != -1)
								{
									string text5 = text3.Substring(0, num);
									string text6 = text3.Substring(num + 1);
									principal = new GenericPrincipal(new HttpListenerBasicIdentity(text5, text6), null);
								}
								else
								{
									httpStatusCode = HttpStatusCode.BadRequest;
								}
							}
							catch (FormatException)
							{
							}
						}
						break;
					}
					if (principal != null)
					{
						httpListenerContext2.SetIdentity(principal, text2);
					}
					else
					{
						if (Logging.On)
						{
							Logging.PrintWarning(Logging.HttpListener, this, "HandleAuthentication", SR.GetString("net_log_listener_create_valid_identity_failed"));
						}
						HttpListener.FreeContext(ref httpListenerContext2, memoryBlob);
					}
				}
				ArrayList arrayList = null;
				if (httpListenerContext2 == null)
				{
					if (text != null)
					{
						HttpListener.AddChallenge(ref arrayList, text);
					}
					else
					{
						if (ntauthentication2 != null)
						{
							if (ntauthentication2 == ntauthentication3)
							{
								ntauthentication3 = null;
							}
							if (ntauthentication2 != ntauthentication)
							{
								NTAuthentication ntauthentication4 = ntauthentication2;
								ntauthentication2 = null;
								ntauthentication4.CloseContext();
							}
							else
							{
								ntauthentication2 = null;
							}
						}
						if (httpStatusCode != HttpStatusCode.Unauthorized)
						{
							this.SendError(requestId, httpStatusCode, null);
							return null;
						}
						arrayList = this.BuildChallenge(authenticationSchemes2, connectionId, out ntauthentication2, extendedProtectionPolicy, flag);
					}
				}
				if (disconnectAsyncResult == null && ntauthentication2 != null)
				{
					this.RegisterForDisconnectNotification(connectionId, ref disconnectAsyncResult);
					if (disconnectAsyncResult == null)
					{
						if (ntauthentication2 != null)
						{
							if (ntauthentication2 == ntauthentication3)
							{
								ntauthentication3 = null;
							}
							if (ntauthentication2 != ntauthentication)
							{
								NTAuthentication ntauthentication5 = ntauthentication2;
								ntauthentication2 = null;
								ntauthentication5.CloseContext();
							}
							else
							{
								ntauthentication2 = null;
							}
						}
						this.SendError(requestId, HttpStatusCode.InternalServerError, null);
						HttpListener.FreeContext(ref httpListenerContext2, memoryBlob);
						return null;
					}
				}
				if (ntauthentication != ntauthentication2)
				{
					if (ntauthentication == ntauthentication3)
					{
						ntauthentication3 = null;
					}
					NTAuthentication ntauthentication6 = ntauthentication;
					ntauthentication = ntauthentication2;
					disconnectAsyncResult.Session = ntauthentication2;
					if (ntauthentication6 != null)
					{
						if ((authenticationSchemes2 & AuthenticationSchemes.Digest) != AuthenticationSchemes.None)
						{
							this.SaveDigestContext(ntauthentication6);
						}
						else
						{
							ntauthentication6.CloseContext();
						}
					}
				}
				if (httpListenerContext2 == null)
				{
					this.SendError(requestId, (arrayList != null && arrayList.Count > 0) ? HttpStatusCode.Unauthorized : HttpStatusCode.Forbidden, arrayList);
					httpListenerContext3 = null;
				}
				else
				{
					if (!stoleBlob)
					{
						stoleBlob = true;
						httpListenerContext2.Request.ReleasePins();
					}
					httpListenerContext3 = httpListenerContext2;
				}
			}
			catch
			{
				HttpListener.FreeContext(ref httpListenerContext2, memoryBlob);
				if (ntauthentication2 != null)
				{
					if (ntauthentication2 == ntauthentication3)
					{
						ntauthentication3 = null;
					}
					if (ntauthentication2 != ntauthentication)
					{
						NTAuthentication ntauthentication7 = ntauthentication2;
						ntauthentication2 = null;
						ntauthentication7.CloseContext();
					}
					else
					{
						ntauthentication2 = null;
					}
				}
				throw;
			}
			finally
			{
				try
				{
					if (ntauthentication != null && ntauthentication != ntauthentication2)
					{
						if (ntauthentication2 == null && disconnectAsyncResult != null)
						{
							disconnectAsyncResult.Session = null;
						}
						if ((authenticationSchemes2 & AuthenticationSchemes.Digest) != AuthenticationSchemes.None)
						{
							this.SaveDigestContext(ntauthentication);
						}
						else
						{
							ntauthentication.CloseContext();
						}
					}
					if (ntauthentication3 != null && ntauthentication != ntauthentication3 && ntauthentication2 != ntauthentication3)
					{
						ntauthentication3.CloseContext();
					}
				}
				finally
				{
					if (disconnectAsyncResult != null)
					{
						disconnectAsyncResult.FinishOwningDisconnectHandling();
					}
				}
			}
			return httpListenerContext3;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00031818 File Offset: 0x0002FA18
		private static void FreeContext(ref HttpListenerContext httpContext, RequestContextBase memoryBlob)
		{
			if (httpContext != null)
			{
				httpContext.Request.DetachBlob(memoryBlob);
				httpContext.Close();
				httpContext = null;
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00031838 File Offset: 0x0002FA38
		internal void SetAuthenticationHeaders(HttpListenerContext context)
		{
			HttpListenerRequest request = context.Request;
			HttpListenerResponse response = context.Response;
			NTAuthentication ntauthentication;
			ArrayList arrayList = this.BuildChallenge(context.AuthenticationSchemes, request.m_ConnectionId, out ntauthentication, context.ExtendedProtectionPolicy, request.IsSecureConnection);
			if (arrayList != null)
			{
				if (ntauthentication != null)
				{
					this.SaveDigestContext(ntauthentication);
				}
				foreach (object obj in arrayList)
				{
					string text = (string)obj;
					response.Headers.AddInternal("WWW-Authenticate", text);
				}
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000318DC File Offset: 0x0002FADC
		private static bool ScenarioChecksChannelBinding(bool isSecureConnection, ProtectionScenario scenario)
		{
			return isSecureConnection && scenario == ProtectionScenario.TransportSelected;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000318E8 File Offset: 0x0002FAE8
		private ChannelBinding GetChannelBinding(ulong connectionId, bool isSecureConnection, ExtendedProtectionPolicy policy)
		{
			if (policy.PolicyEnforcement == PolicyEnforcement.Never)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_no_cbt_disabled"));
				}
				return null;
			}
			if (!isSecureConnection)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_no_cbt_http"));
				}
				return null;
			}
			if (!AuthenticationManager.OSSupportsExtendedProtection)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_no_cbt_platform"));
				}
				return null;
			}
			if (policy.ProtectionScenario == ProtectionScenario.TrustedProxy)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_no_cbt_trustedproxy"));
				}
				return null;
			}
			ChannelBinding channelBindingFromTls = this.GetChannelBindingFromTls(connectionId);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_cbt"));
			}
			return channelBindingFromTls;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000319B0 File Offset: 0x0002FBB0
		private bool CheckSpn(NTAuthentication context, bool isSecureConnection, ExtendedProtectionPolicy policy)
		{
			if (context.IsKerberos)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_no_spn_kerberos"));
				}
				return true;
			}
			if (policy.PolicyEnforcement == PolicyEnforcement.Never)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_no_spn_disabled"));
				}
				return true;
			}
			if (HttpListener.ScenarioChecksChannelBinding(isSecureConnection, policy.ProtectionScenario))
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_no_spn_cbt"));
				}
				return true;
			}
			if (!AuthenticationManager.OSSupportsExtendedProtection)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_no_spn_platform"));
				}
				return true;
			}
			string clientSpecifiedSpn = context.ClientSpecifiedSpn;
			if (string.IsNullOrEmpty(clientSpecifiedSpn))
			{
				if (policy.PolicyEnforcement == PolicyEnforcement.WhenSupported)
				{
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_no_spn_whensupported"));
					}
					return true;
				}
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_spn_failed_always"));
				}
				return false;
			}
			else
			{
				if (ServiceNameCollection.Match(clientSpecifiedSpn, "http/localhost"))
				{
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_no_spn_loopback"));
					}
					return true;
				}
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_spn", new object[] { clientSpecifiedSpn }));
				}
				ServiceNameCollection serviceNames = this.GetServiceNames(policy);
				bool flag = serviceNames.Contains(clientSpecifiedSpn);
				if (Logging.On)
				{
					if (flag)
					{
						Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_spn_passed"));
					}
					else
					{
						Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_spn_failed"));
						if (serviceNames.Count == 0)
						{
							Logging.PrintWarning(Logging.HttpListener, this, "CheckSpn", SR.GetString("net_log_listener_spn_failed_empty"));
						}
						else
						{
							Logging.PrintInfo(Logging.HttpListener, this, SR.GetString("net_log_listener_spn_failed_dump"));
							foreach (object obj in serviceNames)
							{
								string text = (string)obj;
								Logging.PrintInfo(Logging.HttpListener, this, "\t" + text);
							}
						}
					}
				}
				return flag;
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00031BE0 File Offset: 0x0002FDE0
		private ServiceNameCollection GetServiceNames(ExtendedProtectionPolicy policy)
		{
			ServiceNameCollection serviceNameCollection;
			if (policy.CustomServiceNames == null)
			{
				if (this.m_DefaultServiceNames.ServiceNames.Count == 0)
				{
					throw new InvalidOperationException(SR.GetString("net_listener_no_spns"));
				}
				serviceNameCollection = this.m_DefaultServiceNames.ServiceNames;
			}
			else
			{
				serviceNameCollection = policy.CustomServiceNames;
			}
			return serviceNameCollection;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00031C30 File Offset: 0x0002FE30
		private ContextFlags GetContextFlags(ExtendedProtectionPolicy policy, bool isSecureConnection)
		{
			ContextFlags contextFlags = ContextFlags.Connection;
			if (policy.PolicyEnforcement != PolicyEnforcement.Never)
			{
				if (policy.PolicyEnforcement == PolicyEnforcement.WhenSupported)
				{
					contextFlags |= ContextFlags.AllowMissingBindings;
				}
				if (policy.ProtectionScenario == ProtectionScenario.TrustedProxy)
				{
					contextFlags |= ContextFlags.ProxyBindings;
				}
			}
			return contextFlags;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00031C6E File Offset: 0x0002FE6E
		private static void AddChallenge(ref ArrayList challenges, string challenge)
		{
			if (challenge != null)
			{
				challenge = challenge.Trim();
				if (challenge.Length > 0)
				{
					if (challenges == null)
					{
						challenges = new ArrayList(4);
					}
					challenges.Add(challenge);
				}
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00031C9C File Offset: 0x0002FE9C
		private ArrayList BuildChallenge(AuthenticationSchemes authenticationScheme, ulong connectionId, out NTAuthentication newContext, ExtendedProtectionPolicy policy, bool isSecureConnection)
		{
			ArrayList arrayList = null;
			newContext = null;
			if ((authenticationScheme & AuthenticationSchemes.Negotiate) != AuthenticationSchemes.None)
			{
				HttpListener.AddChallenge(ref arrayList, "Negotiate");
			}
			if ((authenticationScheme & AuthenticationSchemes.Ntlm) != AuthenticationSchemes.None)
			{
				HttpListener.AddChallenge(ref arrayList, "NTLM");
			}
			if ((authenticationScheme & AuthenticationSchemes.Digest) != AuthenticationSchemes.None)
			{
				NTAuthentication ntauthentication = null;
				try
				{
					ChannelBinding channelBinding = this.GetChannelBinding(connectionId, isSecureConnection, policy);
					ntauthentication = new NTAuthentication(true, "WDigest", null, this.GetContextFlags(policy, isSecureConnection), channelBinding);
					SecurityStatus securityStatus;
					string outgoingDigestBlob = ntauthentication.GetOutgoingDigestBlob(null, null, null, this.Realm, false, false, out securityStatus);
					if (ntauthentication.IsValidContext)
					{
						newContext = ntauthentication;
					}
					HttpListener.AddChallenge(ref arrayList, "Digest" + (string.IsNullOrEmpty(outgoingDigestBlob) ? "" : (" " + outgoingDigestBlob)));
				}
				finally
				{
					if (ntauthentication != null && newContext != ntauthentication)
					{
						ntauthentication.CloseContext();
					}
				}
			}
			if ((authenticationScheme & AuthenticationSchemes.Basic) != AuthenticationSchemes.None)
			{
				HttpListener.AddChallenge(ref arrayList, "Basic realm=\"" + this.Realm + "\"");
			}
			return arrayList;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00031D90 File Offset: 0x0002FF90
		private void RegisterForDisconnectNotification(ulong connectionId, ref HttpListener.DisconnectAsyncResult disconnectResult)
		{
			try
			{
				HttpListener.DisconnectAsyncResult disconnectAsyncResult = new HttpListener.DisconnectAsyncResult(this, connectionId);
				this.EnsureBoundHandle();
				uint num = UnsafeNclNativeMethods.HttpApi.HttpWaitForDisconnect(this.m_RequestQueueHandle, connectionId, disconnectAsyncResult.NativeOverlapped);
				if (num == 0U || num == 997U)
				{
					disconnectResult = disconnectAsyncResult;
					this.DisconnectResults[connectionId] = disconnectResult;
				}
				if (num == 0U && HttpListener.SkipIOCPCallbackOnSuccess)
				{
					disconnectAsyncResult.IOCompleted(num, 0U, disconnectAsyncResult.NativeOverlapped);
				}
			}
			catch (Win32Exception ex)
			{
				uint nativeErrorCode = (uint)ex.NativeErrorCode;
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00031E14 File Offset: 0x00030014
		private unsafe void SendError(ulong requestId, HttpStatusCode httpStatusCode, ArrayList challenges)
		{
			UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE http_RESPONSE = default(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE);
			http_RESPONSE.Version = default(UnsafeNclNativeMethods.HttpApi.HTTP_VERSION);
			http_RESPONSE.Version.MajorVersion = 1;
			http_RESPONSE.Version.MinorVersion = 1;
			http_RESPONSE.StatusCode = (ushort)httpStatusCode;
			string text = HttpStatusDescription.Get(httpStatusCode);
			uint num = 0U;
			byte[] bytes = Encoding.Default.GetBytes(text);
			byte[] array;
			byte* ptr;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			http_RESPONSE.pReason = (sbyte*)ptr;
			http_RESPONSE.ReasonLength = (ushort)bytes.Length;
			byte[] bytes2 = Encoding.Default.GetBytes("0");
			byte[] array2;
			byte* ptr2;
			if ((array2 = bytes2) == null || array2.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array2[0];
			}
			(&http_RESPONSE.Headers.KnownHeaders)[11].pRawValue = (sbyte*)ptr2;
			(&http_RESPONSE.Headers.KnownHeaders)[11].RawValueLength = (ushort)bytes2.Length;
			http_RESPONSE.Headers.UnknownHeaderCount = checked((ushort)((challenges == null) ? 0 : challenges.Count));
			GCHandle[] array3 = null;
			UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER[] array4 = null;
			GCHandle gchandle = default(GCHandle);
			GCHandle gchandle2 = default(GCHandle);
			if (http_RESPONSE.Headers.UnknownHeaderCount > 0)
			{
				array3 = new GCHandle[(int)http_RESPONSE.Headers.UnknownHeaderCount];
				array4 = new UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER[(int)http_RESPONSE.Headers.UnknownHeaderCount];
			}
			uint num2;
			try
			{
				if (http_RESPONSE.Headers.UnknownHeaderCount > 0)
				{
					gchandle = GCHandle.Alloc(array4, GCHandleType.Pinned);
					http_RESPONSE.Headers.pUnknownHeaders = (UnsafeNclNativeMethods.HttpApi.HTTP_UNKNOWN_HEADER*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(array4, 0);
					gchandle2 = GCHandle.Alloc(HttpListener.s_WwwAuthenticateBytes, GCHandleType.Pinned);
					sbyte* ptr3 = (sbyte*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(HttpListener.s_WwwAuthenticateBytes, 0);
					for (int i = 0; i < array3.Length; i++)
					{
						byte[] bytes3 = Encoding.Default.GetBytes((string)challenges[i]);
						array3[i] = GCHandle.Alloc(bytes3, GCHandleType.Pinned);
						array4[i].pName = ptr3;
						array4[i].NameLength = (ushort)HttpListener.s_WwwAuthenticateBytes.Length;
						array4[i].pRawValue = (sbyte*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(bytes3, 0);
						array4[i].RawValueLength = checked((ushort)bytes3.Length);
					}
				}
				num2 = UnsafeNclNativeMethods.HttpApi.HttpSendHttpResponse(this.m_RequestQueueHandle, requestId, 0U, &http_RESPONSE, null, &num, SafeLocalFree.Zero, 0U, null, null);
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
				if (gchandle2.IsAllocated)
				{
					gchandle2.Free();
				}
				if (array3 != null)
				{
					for (int j = 0; j < array3.Length; j++)
					{
						if (array3[j].IsAllocated)
						{
							array3[j].Free();
						}
					}
				}
			}
			array2 = null;
			array = null;
			if (num2 != 0U)
			{
				HttpListenerContext.CancelRequest(this.m_RequestQueueHandle, requestId);
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00032108 File Offset: 0x00030308
		private static int GetTokenOffsetFromBlob(IntPtr blob)
		{
			IntPtr intPtr = Marshal.ReadIntPtr(blob, (int)Marshal.OffsetOf(HttpListener.ChannelBindingStatusType, "ChannelToken"));
			return (int)IntPtrHelper.Subtract(intPtr, blob);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00032138 File Offset: 0x00030338
		private static int GetTokenSizeFromBlob(IntPtr blob)
		{
			return Marshal.ReadInt32(blob, (int)Marshal.OffsetOf(HttpListener.ChannelBindingStatusType, "ChannelTokenSize"));
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00032154 File Offset: 0x00030354
		internal unsafe ChannelBinding GetChannelBindingFromTls(ulong connectionId)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, "HttpListener#" + ValidationHelper.HashString(this) + "::GetChannelBindingFromTls() connectionId: " + connectionId.ToString());
			}
			int num = HttpListener.RequestChannelBindStatusSize + 128;
			SafeLocalFreeChannelBinding safeLocalFreeChannelBinding = null;
			uint num2 = 0U;
			uint num3;
			for (;;)
			{
				byte[] array = new byte[num];
				byte[] array2;
				byte* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				num3 = UnsafeNclNativeMethods.HttpApi.HttpReceiveClientCertificate(this.RequestQueueHandle, connectionId, 1U, ptr, (uint)num, &num2, null);
				if (num3 == 0U)
				{
					int tokenOffsetFromBlob = HttpListener.GetTokenOffsetFromBlob((IntPtr)((void*)ptr));
					int tokenSizeFromBlob = HttpListener.GetTokenSizeFromBlob((IntPtr)((void*)ptr));
					safeLocalFreeChannelBinding = SafeLocalFreeChannelBinding.LocalAlloc(tokenSizeFromBlob);
					if (safeLocalFreeChannelBinding.IsInvalid)
					{
						break;
					}
					Marshal.Copy(array, tokenOffsetFromBlob, safeLocalFreeChannelBinding.DangerousGetHandle(), tokenSizeFromBlob);
				}
				else
				{
					if (num3 != 234U)
					{
						goto IL_E4;
					}
					int tokenSizeFromBlob2 = HttpListener.GetTokenSizeFromBlob((IntPtr)((void*)ptr));
					num = HttpListener.RequestChannelBindStatusSize + tokenSizeFromBlob2;
				}
				array2 = null;
				if (num3 == 0U)
				{
					return safeLocalFreeChannelBinding;
				}
			}
			throw new OutOfMemoryException();
			IL_E4:
			if (num3 == 87U)
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.HttpListener, "HttpListener#" + ValidationHelper.HashString(this) + "::GetChannelBindingFromTls() " + SR.GetString("net_ssp_dont_support_cbt"));
				}
				return null;
			}
			throw new HttpListenerException((int)num3);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00032290 File Offset: 0x00030490
		internal void CheckDisposed()
		{
			if (this.m_State == HttpListener.State.Closed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000322AE File Offset: 0x000304AE
		private HttpStatusCode HttpStatusFromSecurityStatus(SecurityStatus status)
		{
			if (NclUtilities.IsCredentialFailure(status))
			{
				return HttpStatusCode.Unauthorized;
			}
			if (NclUtilities.IsClientFault(status))
			{
				return HttpStatusCode.BadRequest;
			}
			return HttpStatusCode.InternalServerError;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x000322D4 File Offset: 0x000304D4
		private void SaveDigestContext(NTAuthentication digestContext)
		{
			if (this.m_SavedDigests == null)
			{
				Interlocked.CompareExchange<HttpListener.DigestContext[]>(ref this.m_SavedDigests, new HttpListener.DigestContext[1024], null);
			}
			NTAuthentication ntauthentication = null;
			ArrayList arrayList = null;
			HttpListener.DigestContext[] savedDigests = this.m_SavedDigests;
			lock (savedDigests)
			{
				if (!this.IsListening)
				{
					digestContext.CloseContext();
					return;
				}
				int num = (((num = Environment.TickCount) == 0) ? 1 : num);
				this.m_NewestContext = (this.m_NewestContext + 1) & 1023;
				int timestamp = this.m_SavedDigests[this.m_NewestContext].timestamp;
				ntauthentication = this.m_SavedDigests[this.m_NewestContext].context;
				this.m_SavedDigests[this.m_NewestContext].timestamp = num;
				this.m_SavedDigests[this.m_NewestContext].context = digestContext;
				if (this.m_OldestContext == this.m_NewestContext)
				{
					this.m_OldestContext = (this.m_NewestContext + 1) & 1023;
				}
				while (num - this.m_SavedDigests[this.m_OldestContext].timestamp >= 300 && this.m_SavedDigests[this.m_OldestContext].context != null)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(this.m_SavedDigests[this.m_OldestContext].context);
					this.m_SavedDigests[this.m_OldestContext].context = null;
					this.m_OldestContext = (this.m_OldestContext + 1) & 1023;
				}
				if (ntauthentication != null && num - timestamp <= 10000)
				{
					if (this.m_ExtraSavedDigests == null || num - this.m_ExtraSavedDigestsTimestamp > 10000)
					{
						arrayList = this.m_ExtraSavedDigestsBaking;
						this.m_ExtraSavedDigestsBaking = this.m_ExtraSavedDigests;
						this.m_ExtraSavedDigestsTimestamp = num;
						this.m_ExtraSavedDigests = new ArrayList();
					}
					this.m_ExtraSavedDigests.Add(ntauthentication);
					ntauthentication = null;
				}
			}
			if (ntauthentication != null)
			{
				ntauthentication.CloseContext();
			}
			if (arrayList != null)
			{
				for (int i = 0; i < arrayList.Count; i++)
				{
					((NTAuthentication)arrayList[i]).CloseContext();
				}
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00032514 File Offset: 0x00030714
		private void ClearDigestCache()
		{
			if (this.m_SavedDigests == null)
			{
				return;
			}
			ArrayList[] array = new ArrayList[3];
			HttpListener.DigestContext[] savedDigests = this.m_SavedDigests;
			lock (savedDigests)
			{
				array[0] = this.m_ExtraSavedDigestsBaking;
				this.m_ExtraSavedDigestsBaking = null;
				array[1] = this.m_ExtraSavedDigests;
				this.m_ExtraSavedDigests = null;
				this.m_NewestContext = 0;
				this.m_OldestContext = 0;
				array[2] = new ArrayList();
				for (int i = 0; i < 1024; i++)
				{
					if (this.m_SavedDigests[i].context != null)
					{
						array[2].Add(this.m_SavedDigests[i].context);
						this.m_SavedDigests[i].context = null;
					}
					this.m_SavedDigests[i].timestamp = 0;
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j] != null)
				{
					for (int k = 0; k < array[j].Count; k++)
					{
						((NTAuthentication)array[j][k]).CloseContext();
					}
				}
			}
		}

		// Token: 0x04000DD9 RID: 3545
		private static readonly Type ChannelBindingStatusType = typeof(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_CHANNEL_BIND_STATUS);

		// Token: 0x04000DDA RID: 3546
		private static readonly int RequestChannelBindStatusSize = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_CHANNEL_BIND_STATUS));

		// Token: 0x04000DDB RID: 3547
		internal static readonly bool SkipIOCPCallbackOnSuccess = ComNetOS.IsWin8orLater;

		// Token: 0x04000DDC RID: 3548
		private const int UnknownHeaderLimit = 1000;

		// Token: 0x04000DDD RID: 3549
		private static byte[] s_WwwAuthenticateBytes = new byte[]
		{
			87, 87, 87, 45, 65, 117, 116, 104, 101, 110,
			116, 105, 99, 97, 116, 101
		};

		// Token: 0x04000DDE RID: 3550
		private HttpListener.AuthenticationSelectorInfo m_AuthenticationDelegate;

		// Token: 0x04000DDF RID: 3551
		private AuthenticationSchemes m_AuthenticationScheme = AuthenticationSchemes.Anonymous;

		// Token: 0x04000DE0 RID: 3552
		private SecurityException m_SecurityException;

		// Token: 0x04000DE1 RID: 3553
		private string m_Realm;

		// Token: 0x04000DE2 RID: 3554
		private CriticalHandle m_RequestQueueHandle;

		// Token: 0x04000DE3 RID: 3555
		private bool m_RequestHandleBound;

		// Token: 0x04000DE4 RID: 3556
		private volatile HttpListener.State m_State;

		// Token: 0x04000DE5 RID: 3557
		private HttpListenerPrefixCollection m_Prefixes;

		// Token: 0x04000DE6 RID: 3558
		private bool m_IgnoreWriteExceptions;

		// Token: 0x04000DE7 RID: 3559
		private bool m_UnsafeConnectionNtlmAuthentication;

		// Token: 0x04000DE8 RID: 3560
		private HttpListener.ExtendedProtectionSelector m_ExtendedProtectionSelectorDelegate;

		// Token: 0x04000DE9 RID: 3561
		private ExtendedProtectionPolicy m_ExtendedProtectionPolicy;

		// Token: 0x04000DEA RID: 3562
		private ServiceNameStore m_DefaultServiceNames;

		// Token: 0x04000DEB RID: 3563
		private HttpServerSessionHandle m_ServerSessionHandle;

		// Token: 0x04000DEC RID: 3564
		private ulong m_UrlGroupId;

		// Token: 0x04000DED RID: 3565
		private HttpListenerTimeoutManager m_TimeoutManager;

		// Token: 0x04000DEE RID: 3566
		private bool m_V2Initialized;

		// Token: 0x04000DEF RID: 3567
		private Hashtable m_DisconnectResults;

		// Token: 0x04000DF0 RID: 3568
		private object m_InternalLock;

		// Token: 0x04000DF1 RID: 3569
		internal Hashtable m_UriPrefixes = new Hashtable();

		// Token: 0x04000DF2 RID: 3570
		private const int DigestLifetimeSeconds = 300;

		// Token: 0x04000DF3 RID: 3571
		private const int MaximumDigests = 1024;

		// Token: 0x04000DF4 RID: 3572
		private const int MinimumDigestLifetimeSeconds = 10;

		// Token: 0x04000DF5 RID: 3573
		private HttpListener.DigestContext[] m_SavedDigests;

		// Token: 0x04000DF6 RID: 3574
		private ArrayList m_ExtraSavedDigests;

		// Token: 0x04000DF7 RID: 3575
		private ArrayList m_ExtraSavedDigestsBaking;

		// Token: 0x04000DF8 RID: 3576
		private int m_ExtraSavedDigestsTimestamp;

		// Token: 0x04000DF9 RID: 3577
		private int m_NewestContext;

		// Token: 0x04000DFA RID: 3578
		private int m_OldestContext;

		// Token: 0x020006FC RID: 1788
		private class AuthenticationSelectorInfo
		{
			// Token: 0x06004061 RID: 16481 RVA: 0x0010DDD6 File Offset: 0x0010BFD6
			internal AuthenticationSelectorInfo(AuthenticationSchemeSelector selectorDelegate, bool canUseAdvancedAuth)
			{
				this.m_SelectorDelegate = selectorDelegate;
				this.m_CanUseAdvancedAuth = canUseAdvancedAuth;
			}

			// Token: 0x17000EE2 RID: 3810
			// (get) Token: 0x06004062 RID: 16482 RVA: 0x0010DDEC File Offset: 0x0010BFEC
			internal AuthenticationSchemeSelector Delegate
			{
				get
				{
					return this.m_SelectorDelegate;
				}
			}

			// Token: 0x17000EE3 RID: 3811
			// (get) Token: 0x06004063 RID: 16483 RVA: 0x0010DDF4 File Offset: 0x0010BFF4
			internal bool AdvancedAuth
			{
				get
				{
					return this.m_CanUseAdvancedAuth;
				}
			}

			// Token: 0x040030A4 RID: 12452
			private AuthenticationSchemeSelector m_SelectorDelegate;

			// Token: 0x040030A5 RID: 12453
			private bool m_CanUseAdvancedAuth;
		}

		/// <summary>A delegate called to determine the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> to use for each <see cref="T:System.Net.HttpListener" /> request.</summary>
		/// <param name="request">The <see cref="T:System.Net.HttpListenerRequest" /> to determine the extended protection policy that the <see cref="T:System.Net.HttpListener" /> instance will use to provide extended protection.</param>
		/// <returns>An <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> object that specifies the extended protection policy to use for this request.</returns>
		// Token: 0x020006FD RID: 1789
		// (Invoke) Token: 0x06004065 RID: 16485
		public delegate ExtendedProtectionPolicy ExtendedProtectionSelector(HttpListenerRequest request);

		// Token: 0x020006FE RID: 1790
		private enum State
		{
			// Token: 0x040030A7 RID: 12455
			Stopped,
			// Token: 0x040030A8 RID: 12456
			Started,
			// Token: 0x040030A9 RID: 12457
			Closed
		}

		// Token: 0x020006FF RID: 1791
		private struct DigestContext
		{
			// Token: 0x040030AA RID: 12458
			internal NTAuthentication context;

			// Token: 0x040030AB RID: 12459
			internal int timestamp;
		}

		// Token: 0x02000700 RID: 1792
		private class DisconnectAsyncResult : IAsyncResult
		{
			// Token: 0x17000EE4 RID: 3812
			// (get) Token: 0x06004068 RID: 16488 RVA: 0x0010DDFC File Offset: 0x0010BFFC
			internal unsafe NativeOverlapped* NativeOverlapped
			{
				get
				{
					return this.m_NativeOverlapped;
				}
			}

			// Token: 0x17000EE5 RID: 3813
			// (get) Token: 0x06004069 RID: 16489 RVA: 0x0010DE04 File Offset: 0x0010C004
			public object AsyncState
			{
				get
				{
					throw ExceptionHelper.PropertyNotImplementedException;
				}
			}

			// Token: 0x17000EE6 RID: 3814
			// (get) Token: 0x0600406A RID: 16490 RVA: 0x0010DE0B File Offset: 0x0010C00B
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					throw ExceptionHelper.PropertyNotImplementedException;
				}
			}

			// Token: 0x17000EE7 RID: 3815
			// (get) Token: 0x0600406B RID: 16491 RVA: 0x0010DE12 File Offset: 0x0010C012
			public bool CompletedSynchronously
			{
				get
				{
					throw ExceptionHelper.PropertyNotImplementedException;
				}
			}

			// Token: 0x17000EE8 RID: 3816
			// (get) Token: 0x0600406C RID: 16492 RVA: 0x0010DE19 File Offset: 0x0010C019
			public bool IsCompleted
			{
				get
				{
					throw ExceptionHelper.PropertyNotImplementedException;
				}
			}

			// Token: 0x0600406D RID: 16493 RVA: 0x0010DE20 File Offset: 0x0010C020
			internal DisconnectAsyncResult(HttpListener httpListener, ulong connectionId)
			{
				this.m_OwnershipState = 1;
				this.m_HttpListener = httpListener;
				this.m_ConnectionId = connectionId;
				this.m_NativeOverlapped = new Overlapped
				{
					AsyncResult = this
				}.UnsafePack(HttpListener.DisconnectAsyncResult.s_IOCallback, null);
			}

			// Token: 0x0600406E RID: 16494 RVA: 0x0010DE68 File Offset: 0x0010C068
			internal bool StartOwningDisconnectHandling()
			{
				int num;
				while ((num = Interlocked.CompareExchange(ref this.m_OwnershipState, 1, 0)) == 2)
				{
					Thread.SpinWait(1);
				}
				return num < 2;
			}

			// Token: 0x0600406F RID: 16495 RVA: 0x0010DE93 File Offset: 0x0010C093
			internal void FinishOwningDisconnectHandling()
			{
				if (Interlocked.CompareExchange(ref this.m_OwnershipState, 0, 1) == 2)
				{
					this.HandleDisconnect();
				}
			}

			// Token: 0x06004070 RID: 16496 RVA: 0x0010DEAB File Offset: 0x0010C0AB
			internal unsafe void IOCompleted(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
			{
				HttpListener.DisconnectAsyncResult.IOCompleted(this, errorCode, numBytes, nativeOverlapped);
			}

			// Token: 0x06004071 RID: 16497 RVA: 0x0010DEB6 File Offset: 0x0010C0B6
			private unsafe static void IOCompleted(HttpListener.DisconnectAsyncResult asyncResult, uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
			{
				Overlapped.Free(nativeOverlapped);
				if (Interlocked.Exchange(ref asyncResult.m_OwnershipState, 2) == 0)
				{
					asyncResult.HandleDisconnect();
				}
			}

			// Token: 0x06004072 RID: 16498 RVA: 0x0010DED4 File Offset: 0x0010C0D4
			private unsafe static void WaitCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
			{
				Overlapped overlapped = Overlapped.Unpack(nativeOverlapped);
				HttpListener.DisconnectAsyncResult disconnectAsyncResult = (HttpListener.DisconnectAsyncResult)overlapped.AsyncResult;
				HttpListener.DisconnectAsyncResult.IOCompleted(disconnectAsyncResult, errorCode, numBytes, nativeOverlapped);
			}

			// Token: 0x06004073 RID: 16499 RVA: 0x0010DF00 File Offset: 0x0010C100
			private void HandleDisconnect()
			{
				this.m_HttpListener.DisconnectResults.Remove(this.m_ConnectionId);
				if (this.m_Session != null)
				{
					if (this.m_Session.Package == "WDigest")
					{
						this.m_HttpListener.SaveDigestContext(this.m_Session);
					}
					else
					{
						this.m_Session.CloseContext();
					}
				}
				IDisposable disposable = ((this.m_AuthenticatedConnection == null) ? null : (this.m_AuthenticatedConnection.Identity as IDisposable));
				if (disposable != null && this.m_AuthenticatedConnection.Identity.AuthenticationType == "NTLM" && this.m_HttpListener.UnsafeConnectionNtlmAuthentication)
				{
					disposable.Dispose();
				}
				int num = Interlocked.Exchange(ref this.m_OwnershipState, 3);
			}

			// Token: 0x17000EE9 RID: 3817
			// (get) Token: 0x06004074 RID: 16500 RVA: 0x0010DFC0 File Offset: 0x0010C1C0
			// (set) Token: 0x06004075 RID: 16501 RVA: 0x0010DFC8 File Offset: 0x0010C1C8
			internal WindowsPrincipal AuthenticatedConnection
			{
				get
				{
					return this.m_AuthenticatedConnection;
				}
				set
				{
					this.m_AuthenticatedConnection = value;
				}
			}

			// Token: 0x17000EEA RID: 3818
			// (get) Token: 0x06004076 RID: 16502 RVA: 0x0010DFD1 File Offset: 0x0010C1D1
			// (set) Token: 0x06004077 RID: 16503 RVA: 0x0010DFD9 File Offset: 0x0010C1D9
			internal NTAuthentication Session
			{
				get
				{
					return this.m_Session;
				}
				set
				{
					this.m_Session = value;
				}
			}

			// Token: 0x040030AC RID: 12460
			private static readonly IOCompletionCallback s_IOCallback = new IOCompletionCallback(HttpListener.DisconnectAsyncResult.WaitCallback);

			// Token: 0x040030AD RID: 12461
			private ulong m_ConnectionId;

			// Token: 0x040030AE RID: 12462
			private HttpListener m_HttpListener;

			// Token: 0x040030AF RID: 12463
			private unsafe NativeOverlapped* m_NativeOverlapped;

			// Token: 0x040030B0 RID: 12464
			private int m_OwnershipState;

			// Token: 0x040030B1 RID: 12465
			private WindowsPrincipal m_AuthenticatedConnection;

			// Token: 0x040030B2 RID: 12466
			private NTAuthentication m_Session;

			// Token: 0x040030B3 RID: 12467
			internal const string NTLM = "NTLM";
		}
	}
}
