using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Net.Configuration;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Threading;

namespace System.Net
{
	/// <summary>Manages the collection of <see cref="T:System.Net.ServicePoint" /> objects.</summary>
	// Token: 0x02000160 RID: 352
	public class ServicePointManager
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00042A13 File Offset: 0x00040C13
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x00042A31 File Offset: 0x00040C31
		private static int InternalConnectionLimit
		{
			get
			{
				if (ServicePointManager.s_ConfigTable == null)
				{
					ServicePointManager.s_ConfigTable = ServicePointManager.ConfigTable;
				}
				return ServicePointManager.s_ConnectionLimit;
			}
			set
			{
				if (ServicePointManager.s_ConfigTable == null)
				{
					ServicePointManager.s_ConfigTable = ServicePointManager.ConfigTable;
				}
				ServicePointManager.s_UserChangedLimit = true;
				ServicePointManager.s_ConnectionLimit = value;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00042A58 File Offset: 0x00040C58
		private static int PersistentConnectionLimit
		{
			get
			{
				if (ComNetOS.IsAspNetServer)
				{
					return 10;
				}
				return 2;
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00042A68 File Offset: 0x00040C68
		[Conditional("DEBUG")]
		internal static void DebugMembers(int requestHash)
		{
			try
			{
				foreach (object obj in ServicePointManager.s_ServicePointTable)
				{
					WeakReference weakReference = (WeakReference)obj;
					if (weakReference != null && weakReference.IsAlive)
					{
						ServicePoint servicePoint = (ServicePoint)weakReference.Target;
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00042B08 File Offset: 0x00040D08
		private static Hashtable ConfigTable
		{
			get
			{
				if (ServicePointManager.s_ConfigTable == null)
				{
					Hashtable hashtable = ServicePointManager.s_ServicePointTable;
					lock (hashtable)
					{
						if (ServicePointManager.s_ConfigTable == null)
						{
							ConnectionManagementSectionInternal section = ConnectionManagementSectionInternal.GetSection();
							Hashtable hashtable2 = null;
							if (section != null)
							{
								hashtable2 = section.ConnectionManagement;
							}
							if (hashtable2 == null)
							{
								hashtable2 = new Hashtable();
							}
							if (hashtable2.ContainsKey("*"))
							{
								int num = (int)hashtable2["*"];
								if (num < 1)
								{
									num = ServicePointManager.PersistentConnectionLimit;
								}
								ServicePointManager.s_ConnectionLimit = num;
							}
							ServicePointManager.s_ConfigTable = hashtable2;
						}
					}
				}
				return ServicePointManager.s_ConfigTable;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x00042BB4 File Offset: 0x00040DB4
		internal static TimerThread.Callback IdleServicePointTimeoutDelegate
		{
			get
			{
				return ServicePointManager.s_IdleServicePointTimeoutDelegate;
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00042BBC File Offset: 0x00040DBC
		private static void IdleServicePointTimeoutCallback(TimerThread.Timer timer, int timeNoticed, object context)
		{
			ServicePoint servicePoint = (ServicePoint)context;
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_log_closed_idle", new object[]
				{
					"ServicePoint",
					servicePoint.GetHashCode()
				}));
			}
			Hashtable hashtable = ServicePointManager.s_ServicePointTable;
			lock (hashtable)
			{
				ServicePointManager.s_ServicePointTable.Remove(servicePoint.LookupString);
			}
			servicePoint.ReleaseAllConnectionGroups();
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00042C4C File Offset: 0x00040E4C
		private ServicePointManager()
		{
		}

		/// <summary>Gets or sets the security protocol used by the <see cref="T:System.Net.ServicePoint" /> objects managed by the <see cref="T:System.Net.ServicePointManager" /> object.</summary>
		/// <returns>One of the values defined in the <see cref="T:System.Net.SecurityProtocolType" /> enumeration.</returns>
		/// <exception cref="T:System.NotSupportedException">The value specified to set the property is not a valid <see cref="T:System.Net.SecurityProtocolType" /> enumeration value.</exception>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00042C54 File Offset: 0x00040E54
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00042C60 File Offset: 0x00040E60
		public static SecurityProtocolType SecurityProtocol
		{
			get
			{
				ServicePointManager.EnsureConfigurationLoaded();
				return ServicePointManager.s_SecurityProtocolType;
			}
			set
			{
				ServicePointManager.EnsureConfigurationLoaded();
				ServicePointManager.ValidateSecurityProtocol(value);
				ServicePointManager.s_SecurityProtocolType = value;
			}
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00042C74 File Offset: 0x00040E74
		private static void ValidateSecurityProtocol(SecurityProtocolType value)
		{
			SecurityProtocolType securityProtocolType = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
			if ((value & ~(securityProtocolType != SecurityProtocolType.SystemDefault)) != SecurityProtocolType.SystemDefault)
			{
				throw new NotSupportedException(SR.GetString("net_securityprotocolnotsupported"));
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00042C9D File Offset: 0x00040E9D
		internal static bool DisableStrongCrypto
		{
			get
			{
				ServicePointManager.EnsureConfigurationLoaded();
				return ServicePointManager.s_disableStrongCrypto;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x00042CA9 File Offset: 0x00040EA9
		internal static bool DisableSystemDefaultTlsVersions
		{
			get
			{
				ServicePointManager.EnsureConfigurationLoaded();
				return ServicePointManager.s_disableSystemDefaultTlsVersions;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00042CB5 File Offset: 0x00040EB5
		internal static bool DisableSendAuxRecord
		{
			get
			{
				ServicePointManager.EnsureConfigurationLoaded();
				return ServicePointManager.s_disableSendAuxRecord;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x00042CC1 File Offset: 0x00040EC1
		internal static bool DisableCertificateEKUs
		{
			get
			{
				ServicePointManager.EnsureConfigurationLoaded();
				return ServicePointManager.s_disableCertificateEKUs;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00042CCD File Offset: 0x00040ECD
		internal static SslProtocols DefaultSslProtocols
		{
			get
			{
				ServicePointManager.EnsureConfigurationLoaded();
				return ServicePointManager.s_defaultSslProtocols;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00042CD9 File Offset: 0x00040ED9
		internal static bool UseHttpPipeliningAndBufferPooling
		{
			get
			{
				ServicePointManager.EnsureConfigurationLoaded();
				return ServicePointManager.s_useHttpPipeliningAndBufferPooling;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00042CE5 File Offset: 0x00040EE5
		internal static bool UseSafeSynchronousClose
		{
			get
			{
				ServicePointManager.EnsureConfigurationLoaded();
				return ServicePointManager.s_useSafeSynchronousClose;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00042CF1 File Offset: 0x00040EF1
		internal static bool UseStrictRfcInterimResponseHandling
		{
			get
			{
				ServicePointManager.EnsureConfigurationLoaded();
				return ServicePointManager.s_useStrictRfcInterimResponseHandling;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00042CFD File Offset: 0x00040EFD
		internal static bool AllowDangerousUnicodeDecompositions
		{
			get
			{
				ServicePointManager.EnsureConfigurationLoaded();
				return ServicePointManager.s_allowDangerousUnicodeDecompositions;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00042D09 File Offset: 0x00040F09
		internal static bool AllowFullDomainLiterals
		{
			get
			{
				ServicePointManager.EnsureConfigurationLoaded();
				return ServicePointManager.s_allowFullDomainLiterals;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00042D15 File Offset: 0x00040F15
		internal static bool UseStrictIPv6AddressParsing
		{
			get
			{
				ServicePointManager.EnsureConfigurationLoaded();
				return ServicePointManager.s_useStrictIPv6AddressParsing;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x00042D21 File Offset: 0x00040F21
		internal static bool AllowAllUriEncodingExpansion
		{
			get
			{
				ServicePointManager.EnsureConfigurationLoaded();
				return ServicePointManager.s_allowAllUriEncodingExpansion;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00042D2D File Offset: 0x00040F2D
		internal static bool FinishProxyTunnelConnectionEarly
		{
			get
			{
				ServicePointManager.EnsureConfigurationLoaded();
				return ServicePointManager.s_finishProxyTunnelConnectionEarly;
			}
		}

		/// <summary>Gets or sets the maximum number of <see cref="T:System.Net.ServicePoint" /> objects to maintain at any time.</summary>
		/// <returns>The maximum number of <see cref="T:System.Net.ServicePoint" /> objects to maintain. The default value is 0, which means there is no limit to the number of <see cref="T:System.Net.ServicePoint" /> objects.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> is less than 0 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00042D39 File Offset: 0x00040F39
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x00042D40 File Offset: 0x00040F40
		public static int MaxServicePoints
		{
			get
			{
				return ServicePointManager.s_MaxServicePoints;
			}
			set
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				if (!ValidationHelper.ValidateRange(value, 0, 2147483647))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ServicePointManager.s_MaxServicePoints = value;
			}
		}

		/// <summary>Gets or sets the maximum number of concurrent connections allowed by a <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum number of concurrent connections allowed by a <see cref="T:System.Net.ServicePoint" /> object. The default connection limit is 10 for ASP.NET hosted applications and 2 for all others. When an app is running as an ASP.NET host, it is not possible to alter the value of this property through the config file if the autoConfig property is set to <see langword="true" />. However, you can change the value programmatically when the autoConfig property is <see langword="true" />. Set your preferred value once, when the AppDomain loads.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> is less than or equal to 0.</exception>
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00042D6B File Offset: 0x00040F6B
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x00042D72 File Offset: 0x00040F72
		public static int DefaultConnectionLimit
		{
			get
			{
				return ServicePointManager.InternalConnectionLimit;
			}
			set
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				if (value > 0)
				{
					ServicePointManager.InternalConnectionLimit = value;
					return;
				}
				throw new ArgumentOutOfRangeException("value", SR.GetString("net_toosmall"));
			}
		}

		/// <summary>Gets or sets the maximum idle time of a <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum idle time, in milliseconds, of a <see cref="T:System.Net.ServicePoint" /> object. The default value is 100,000 milliseconds (100 seconds).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.MaxServicePointIdleTime" /> is less than <see cref="F:System.Threading.Timeout.Infinite" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00042D9D File Offset: 0x00040F9D
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x00042DAC File Offset: 0x00040FAC
		public static int MaxServicePointIdleTime
		{
			get
			{
				return ServicePointManager.s_ServicePointIdlingQueue.Duration;
			}
			set
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				if (!ValidationHelper.ValidateRange(value, -1, 2147483647))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (ServicePointManager.s_ServicePointIdlingQueue.Duration != value)
				{
					ServicePointManager.s_ServicePointIdlingQueue = TimerThread.GetOrCreateQueue(value);
				}
			}
		}

		/// <summary>Determines whether the Nagle algorithm is used by the service points managed by this <see cref="T:System.Net.ServicePointManager" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> to use the Nagle algorithm; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00042DF8 File Offset: 0x00040FF8
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x00042E04 File Offset: 0x00041004
		public static bool UseNagleAlgorithm
		{
			get
			{
				return SettingsSectionInternal.Section.UseNagleAlgorithm;
			}
			set
			{
				SettingsSectionInternal.Section.UseNagleAlgorithm = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>
		///   <see langword="true" /> to enable 100-Continue behavior. The default value is <see langword="true" />.</returns>
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00042E11 File Offset: 0x00041011
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x00042E1D File Offset: 0x0004101D
		public static bool Expect100Continue
		{
			get
			{
				return SettingsSectionInternal.Section.Expect100Continue;
			}
			set
			{
				SettingsSectionInternal.Section.Expect100Continue = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether a Domain Name Service (DNS) resolution rotates among the applicable Internet Protocol (IP) addresses.</summary>
		/// <returns>
		///   <see langword="false" /> if a DNS resolution always returns the first IP address for a particular host; otherwise <see langword="true" />. The default is <see langword="false" />.</returns>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x00042E2A File Offset: 0x0004102A
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x00042E36 File Offset: 0x00041036
		public static bool EnableDnsRoundRobin
		{
			get
			{
				return SettingsSectionInternal.Section.EnableDnsRoundRobin;
			}
			set
			{
				SettingsSectionInternal.Section.EnableDnsRoundRobin = value;
			}
		}

		/// <summary>Gets or sets a value that indicates how long a Domain Name Service (DNS) resolution is considered valid.</summary>
		/// <returns>The time-out value, in milliseconds. A value of -1 indicates an infinite time-out period. The default value is 120,000 milliseconds (two minutes).</returns>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00042E43 File Offset: 0x00041043
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x00042E4F File Offset: 0x0004104F
		public static int DnsRefreshTimeout
		{
			get
			{
				return SettingsSectionInternal.Section.DnsRefreshTimeout;
			}
			set
			{
				if (value < -1)
				{
					SettingsSectionInternal.Section.DnsRefreshTimeout = -1;
					return;
				}
				SettingsSectionInternal.Section.DnsRefreshTimeout = value;
			}
		}

		/// <summary>Gets or sets policy for server certificates.</summary>
		/// <returns>An object that implements the <see cref="T:System.Net.ICertificatePolicy" /> interface.</returns>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00042E6C File Offset: 0x0004106C
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x00042E73 File Offset: 0x00041073
		[Obsolete("CertificatePolicy is obsoleted for this type, please use ServerCertificateValidationCallback instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public static ICertificatePolicy CertificatePolicy
		{
			get
			{
				return ServicePointManager.GetLegacyCertificatePolicy();
			}
			set
			{
				ExceptionHelper.UnmanagedPermission.Demand();
				ServicePointManager.s_CertPolicyValidationCallback = new CertPolicyValidationCallback(value);
			}
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00042E8C File Offset: 0x0004108C
		internal static ICertificatePolicy GetLegacyCertificatePolicy()
		{
			if (ServicePointManager.s_CertPolicyValidationCallback == null)
			{
				return null;
			}
			return ServicePointManager.s_CertPolicyValidationCallback.CertificatePolicy;
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00042EA5 File Offset: 0x000410A5
		internal static CertPolicyValidationCallback CertPolicyValidationCallback
		{
			get
			{
				return ServicePointManager.s_CertPolicyValidationCallback;
			}
		}

		/// <summary>Gets or sets the callback to validate a server certificate.</summary>
		/// <returns>A <see cref="T:System.Net.Security.RemoteCertificateValidationCallback" />. The default value is <see langword="null" />.</returns>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x00042EAE File Offset: 0x000410AE
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x00042EC7 File Offset: 0x000410C7
		public static RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				if (ServicePointManager.s_ServerCertValidationCallback == null)
				{
					return null;
				}
				return ServicePointManager.s_ServerCertValidationCallback.ValidationCallback;
			}
			set
			{
				ExceptionHelper.InfrastructurePermission.Demand();
				if (value == null)
				{
					ServicePointManager.s_ServerCertValidationCallback = null;
					return;
				}
				ServicePointManager.s_ServerCertValidationCallback = new ServerCertValidationCallback(value);
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00042EEC File Offset: 0x000410EC
		internal static ServerCertValidationCallback ServerCertValidationCallback
		{
			get
			{
				return ServicePointManager.s_ServerCertValidationCallback;
			}
		}

		/// <summary>Setting this property value to <see langword="true" /> causes all outbound TCP connections from HttpWebRequest to use the native socket option SO_REUSE_UNICASTPORT on the socket. This causes the underlying outgoing ports to be shared. This is useful for scenarios where a large number of outgoing connections are made in a short time, and the app risks running out of ports.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x00042EF5 File Offset: 0x000410F5
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x00042EFC File Offset: 0x000410FC
		public static bool ReusePort
		{
			get
			{
				return ServicePointManager.s_reusePort;
			}
			set
			{
				ServicePointManager.s_reusePort = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x00042F04 File Offset: 0x00041104
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x00042F0B File Offset: 0x0004110B
		internal static bool? ReusePortSupported
		{
			get
			{
				return ServicePointManager.s_reusePortSupported;
			}
			set
			{
				ServicePointManager.s_reusePortSupported = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates whether the certificate is checked against the certificate authority revocation list.</summary>
		/// <returns>
		///   <see langword="true" /> if the certificate revocation list is checked; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x00042F13 File Offset: 0x00041113
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x00042F1F File Offset: 0x0004111F
		public static bool CheckCertificateRevocationList
		{
			get
			{
				return SettingsSectionInternal.Section.CheckCertificateRevocationList;
			}
			set
			{
				ExceptionHelper.UnmanagedPermission.Demand();
				SettingsSectionInternal.Section.CheckCertificateRevocationList = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.Security.EncryptionPolicy" /> for this <see cref="T:System.Net.ServicePointManager" /> instance.</summary>
		/// <returns>The encryption policy to use for this <see cref="T:System.Net.ServicePointManager" /> instance.</returns>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x00042F36 File Offset: 0x00041136
		public static EncryptionPolicy EncryptionPolicy
		{
			get
			{
				return SettingsSectionInternal.Section.EncryptionPolicy;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x00042F42 File Offset: 0x00041142
		internal static bool CheckCertificateName
		{
			get
			{
				return SettingsSectionInternal.Section.CheckCertificateName;
			}
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00042F50 File Offset: 0x00041150
		internal static string MakeQueryString(Uri address)
		{
			if (address.IsDefaultPort)
			{
				return address.Scheme + "://" + address.DnsSafeHost;
			}
			return string.Concat(new string[]
			{
				address.Scheme,
				"://",
				address.DnsSafeHost,
				":",
				address.Port.ToString()
			});
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00042FBA File Offset: 0x000411BA
		internal static string MakeQueryString(Uri address1, bool isProxy)
		{
			if (isProxy)
			{
				return ServicePointManager.MakeQueryString(address1) + "://proxy";
			}
			return ServicePointManager.MakeQueryString(address1);
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified <see cref="T:System.Uri" /> object.</summary>
		/// <param name="address">The <see cref="T:System.Uri" /> object of the Internet resource to contact.</param>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached.</exception>
		// Token: 0x06000C95 RID: 3221 RVA: 0x00042FD6 File Offset: 0x000411D6
		public static ServicePoint FindServicePoint(Uri address)
		{
			return ServicePointManager.FindServicePoint(address, null);
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified Uniform Resource Identifier (URI).</summary>
		/// <param name="uriString">The URI of the Internet resource to be contacted.</param>
		/// <param name="proxy">The proxy data for this request.</param>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <exception cref="T:System.UriFormatException">The URI specified in <paramref name="uriString" /> is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached.</exception>
		// Token: 0x06000C96 RID: 3222 RVA: 0x00042FE0 File Offset: 0x000411E0
		public static ServicePoint FindServicePoint(string uriString, IWebProxy proxy)
		{
			Uri uri = new Uri(uriString);
			return ServicePointManager.FindServicePoint(uri, proxy);
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified <see cref="T:System.Uri" /> object.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> object that contains the address of the Internet resource to contact.</param>
		/// <param name="proxy">The proxy data for this request.</param>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached.</exception>
		// Token: 0x06000C97 RID: 3223 RVA: 0x00042FFC File Offset: 0x000411FC
		public static ServicePoint FindServicePoint(Uri address, IWebProxy proxy)
		{
			HttpAbortDelegate httpAbortDelegate = null;
			int num = 0;
			ProxyChain proxyChain;
			return ServicePointManager.FindServicePoint(address, proxy, out proxyChain, ref httpAbortDelegate, ref num);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0004301C File Offset: 0x0004121C
		internal static ServicePoint FindServicePoint(Uri address, IWebProxy proxy, out ProxyChain chain, ref HttpAbortDelegate abortDelegate, ref int abortState)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			bool flag = false;
			chain = null;
			Uri uri = null;
			if (proxy != null && !address.IsLoopback)
			{
				IAutoWebProxy autoWebProxy = proxy as IAutoWebProxy;
				if (autoWebProxy != null)
				{
					chain = autoWebProxy.GetProxies(address);
					abortDelegate = chain.HttpAbortDelegate;
					try
					{
						Thread.MemoryBarrier();
						if (abortState != 0)
						{
							Exception ex = new WebException(NetRes.GetWebStatusString(WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
							throw ex;
						}
						chain.Enumerator.MoveNext();
						uri = chain.Enumerator.Current;
						goto IL_8E;
					}
					finally
					{
						abortDelegate = null;
					}
				}
				if (!proxy.IsBypassed(address))
				{
					uri = proxy.GetProxy(address);
				}
				IL_8E:
				if (uri != null)
				{
					address = uri;
					flag = true;
				}
			}
			return ServicePointManager.FindServicePointHelper(address, flag);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x000430E0 File Offset: 0x000412E0
		internal static ServicePoint FindServicePoint(ProxyChain chain)
		{
			if (!chain.Enumerator.MoveNext())
			{
				return null;
			}
			Uri uri = chain.Enumerator.Current;
			return ServicePointManager.FindServicePointHelper((uri == null) ? chain.Destination : uri, uri != null);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00043128 File Offset: 0x00041328
		private static ServicePoint FindServicePointHelper(Uri address, bool isProxyServicePoint)
		{
			if (isProxyServicePoint && address.Scheme != Uri.UriSchemeHttp)
			{
				Exception ex = new NotSupportedException(SR.GetString("net_proxyschemenotsupported", new object[] { address.Scheme }));
				throw ex;
			}
			string text = ServicePointManager.MakeQueryString(address, isProxyServicePoint);
			ServicePoint servicePoint = null;
			Hashtable hashtable = ServicePointManager.s_ServicePointTable;
			lock (hashtable)
			{
				WeakReference weakReference = ServicePointManager.s_ServicePointTable[text] as WeakReference;
				if (weakReference != null)
				{
					servicePoint = (ServicePoint)weakReference.Target;
				}
				if (servicePoint == null)
				{
					if (ServicePointManager.s_MaxServicePoints > 0 && ServicePointManager.s_ServicePointTable.Count >= ServicePointManager.s_MaxServicePoints)
					{
						Exception ex2 = new InvalidOperationException(SR.GetString("net_maxsrvpoints"));
						throw ex2;
					}
					int num = ServicePointManager.InternalConnectionLimit;
					string text2 = ServicePointManager.MakeQueryString(address);
					bool flag2 = ServicePointManager.s_UserChangedLimit;
					if (ServicePointManager.ConfigTable.ContainsKey(text2))
					{
						num = (int)ServicePointManager.ConfigTable[text2];
						flag2 = true;
					}
					servicePoint = new ServicePoint(address, ServicePointManager.s_ServicePointIdlingQueue, num, text, flag2, isProxyServicePoint);
					weakReference = new WeakReference(servicePoint);
					ServicePointManager.s_ServicePointTable[text] = weakReference;
				}
			}
			return servicePoint;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00043264 File Offset: 0x00041464
		internal static ServicePoint FindServicePoint(string host, int port)
		{
			if (host == null)
			{
				throw new ArgumentNullException("address");
			}
			bool flag = false;
			string text = "ByHost:" + host + ":" + port.ToString(CultureInfo.InvariantCulture);
			ServicePoint servicePoint = null;
			Hashtable hashtable = ServicePointManager.s_ServicePointTable;
			lock (hashtable)
			{
				WeakReference weakReference = ServicePointManager.s_ServicePointTable[text] as WeakReference;
				if (weakReference != null)
				{
					servicePoint = (ServicePoint)weakReference.Target;
				}
				if (servicePoint == null)
				{
					if (ServicePointManager.s_MaxServicePoints > 0 && ServicePointManager.s_ServicePointTable.Count >= ServicePointManager.s_MaxServicePoints)
					{
						Exception ex = new InvalidOperationException(SR.GetString("net_maxsrvpoints"));
						throw ex;
					}
					int num = ServicePointManager.InternalConnectionLimit;
					bool flag3 = ServicePointManager.s_UserChangedLimit;
					string text2 = host + ":" + port.ToString(CultureInfo.InvariantCulture);
					if (ServicePointManager.ConfigTable.ContainsKey(text2))
					{
						num = (int)ServicePointManager.ConfigTable[text2];
						flag3 = true;
					}
					servicePoint = new ServicePoint(host, port, ServicePointManager.s_ServicePointIdlingQueue, num, text, flag3, flag);
					weakReference = new WeakReference(servicePoint);
					ServicePointManager.s_ServicePointTable[text] = weakReference;
				}
			}
			return servicePoint;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x000433A0 File Offset: 0x000415A0
		[FriendAccessAllowed]
		internal static void CloseConnectionGroups(string connectionGroupName)
		{
			Hashtable hashtable = ServicePointManager.s_ServicePointTable;
			lock (hashtable)
			{
				foreach (object obj in ServicePointManager.s_ServicePointTable)
				{
					WeakReference weakReference = ((DictionaryEntry)obj).Value as WeakReference;
					if (weakReference != null)
					{
						ServicePoint servicePoint = (ServicePoint)weakReference.Target;
						if (servicePoint != null)
						{
							servicePoint.CloseConnectionGroupInternal(connectionGroupName);
						}
					}
				}
			}
		}

		/// <summary>Enables or disables the keep-alive option on a TCP connection.</summary>
		/// <param name="enabled">If set to true, then the TCP keep-alive option on a TCP connection will be enabled using the specified <paramref name="keepAliveTime" /> and <paramref name="keepAliveInterval" /> values.  
		///  If set to false, then the TCP keep-alive option is disabled and the remaining parameters are ignored.  
		///  The default value is false.</param>
		/// <param name="keepAliveTime">Specifies the timeout, in milliseconds, with no activity until the first keep-alive packet is sent.  
		///  The value must be greater than 0.  If a value of less than or equal to zero is passed an <see cref="T:System.ArgumentOutOfRangeException" /> is thrown.</param>
		/// <param name="keepAliveInterval">Specifies the interval, in milliseconds, between when successive keep-alive packets are sent if no acknowledgement is received.  
		///  The value must be greater than 0.  If a value of less than or equal to zero is passed an <see cref="T:System.ArgumentOutOfRangeException" /> is thrown.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for <paramref name="keepAliveTime" /> or <paramref name="keepAliveInterval" /> parameter is less than or equal to 0.</exception>
		// Token: 0x06000C9D RID: 3229 RVA: 0x00043448 File Offset: 0x00041648
		public static void SetTcpKeepAlive(bool enabled, int keepAliveTime, int keepAliveInterval)
		{
			if (!enabled)
			{
				ServicePointManager.s_UseTcpKeepAlive = false;
				ServicePointManager.s_TcpKeepAliveTime = 0;
				ServicePointManager.s_TcpKeepAliveInterval = 0;
				return;
			}
			ServicePointManager.s_UseTcpKeepAlive = true;
			if (keepAliveTime <= 0)
			{
				throw new ArgumentOutOfRangeException("keepAliveTime");
			}
			if (keepAliveInterval <= 0)
			{
				throw new ArgumentOutOfRangeException("keepAliveInterval");
			}
			ServicePointManager.s_TcpKeepAliveTime = keepAliveTime;
			ServicePointManager.s_TcpKeepAliveInterval = keepAliveInterval;
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x000434A8 File Offset: 0x000416A8
		private static void LoadConfiguration()
		{
			ServicePointManager.s_reusePort = ServicePointManager.TryInitialize<bool>(new Func<bool, bool>(ServicePointManager.LoadReusePortConfiguration), false);
			ServicePointManager.s_useHttpPipeliningAndBufferPooling = ServicePointManager.TryInitialize<bool>(new Func<bool, bool>(ServicePointManager.LoadUseHttpPipeliningAndBufferPoolingConfiguration), true);
			ServicePointManager.s_useSafeSynchronousClose = ServicePointManager.TryInitialize<bool>(new Func<bool, bool>(ServicePointManager.LoadUseSafeSynchronousClose), true);
			ServicePointManager.s_useStrictRfcInterimResponseHandling = ServicePointManager.TryInitialize<bool>(new Func<bool, bool>(ServicePointManager.LoadUseStrictRfcInterimResponseHandlingConfiguration), true);
			ServicePointManager.s_allowDangerousUnicodeDecompositions = ServicePointManager.TryInitialize<bool>(new Func<bool, bool>(ServicePointManager.LoadAllowDangerousUnicodeDecompositionsConfiguration), false);
			ServicePointManager.s_useStrictIPv6AddressParsing = ServicePointManager.TryInitialize<bool>(new Func<bool, bool>(ServicePointManager.LoadUseStrictIPv6AddressParsingConfiguration), true);
			ServicePointManager.s_allowAllUriEncodingExpansion = ServicePointManager.TryInitialize<bool>(new Func<bool, bool>(ServicePointManager.LoadAllowAllUriEncodingExpansionConfiguration), false);
			ServicePointManager.s_allowFullDomainLiterals = ServicePointManager.TryInitialize<bool>(new Func<bool, bool>(ServicePointManager.LoadAllowFullDomainLiteralsConfiguration), false);
			ServicePointManager.s_finishProxyTunnelConnectionEarly = ServicePointManager.TryInitialize<bool>(new Func<bool, bool>(ServicePointManager.LoadFinishProxyTunnelConnectionEarlyConfiguration), true);
			ServicePointManager.s_disableStrongCrypto = ServicePointManager.TryInitialize<bool>(new Func<bool, bool>(ServicePointManager.LoadDisableStrongCryptoConfiguration), true);
			ServicePointManager.s_disableSendAuxRecord = ServicePointManager.TryInitialize<bool>(new Func<bool, bool>(ServicePointManager.LoadDisableSendAuxRecordConfiguration), false);
			ServicePointManager.s_disableSystemDefaultTlsVersions = ServicePointManager.TryInitialize<bool>(new Func<bool, bool>(ServicePointManager.LoadDisableSystemDefaultTlsVersionsConfiguration), true);
			ServicePointManager.s_disableCertificateEKUs = ServicePointManager.TryInitialize<bool>(new Func<bool, bool>(ServicePointManager.LoadDisableCertificateEKUsConfiguration), false);
			ServicePointManager.s_defaultSslProtocols = ServicePointManager.TryInitialize<SslProtocols>(new Func<SslProtocols, SslProtocols>(ServicePointManager.LoadSecureProtocolConfiguration), SslProtocols.Default);
			ServicePointManager.s_SecurityProtocolType = (SecurityProtocolType)ServicePointManager.s_defaultSslProtocols;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00043608 File Offset: 0x00041808
		private static bool LoadDisableStrongCryptoConfiguration(bool disable)
		{
			if (LocalAppContextSwitches.DontEnableSchUseStrongCrypto)
			{
				int num = RegistryConfiguration.GlobalConfigReadInt("SchUseStrongCrypto", 0);
				disable = num != 1;
			}
			else
			{
				int num = RegistryConfiguration.GlobalConfigReadInt("SchUseStrongCrypto", 1);
				disable = num == 0;
			}
			return disable;
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00043648 File Offset: 0x00041848
		private static bool LoadDisableSendAuxRecordConfiguration(bool disable)
		{
			return LocalAppContextSwitches.DontEnableSchSendAuxRecord || RegistryConfiguration.AppConfigReadInt("System.Net.ServicePointManager.SchSendAuxRecord", 1) == 0 || RegistryConfiguration.GlobalConfigReadInt("SchSendAuxRecord", 1) == 0 || disable;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00043684 File Offset: 0x00041884
		private static bool LoadDisableSystemDefaultTlsVersionsConfiguration(bool disable)
		{
			if (LocalAppContextSwitches.DontEnableSystemDefaultTlsVersions)
			{
				int num = RegistryConfiguration.GlobalConfigReadInt("SystemDefaultTlsVersions", 0);
				disable = num != 1;
			}
			else
			{
				int num2 = RegistryConfiguration.GlobalConfigReadInt("SystemDefaultTlsVersions", 1);
				disable = num2 == 0;
			}
			if (!disable)
			{
				int num3 = RegistryConfiguration.AppConfigReadInt("System.Net.ServicePointManager.SystemDefaultTlsVersions", 1);
				disable = num3 != 1;
			}
			return disable;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000436DC File Offset: 0x000418DC
		private static SslProtocols LoadSecureProtocolConfiguration(SslProtocols defaultValue)
		{
			if (!ServicePointManager.s_disableSystemDefaultTlsVersions)
			{
				defaultValue = SslProtocols.None;
			}
			else if (!ServicePointManager.s_disableStrongCrypto)
			{
				defaultValue = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
			}
			else
			{
				defaultValue = SslProtocols.Default;
			}
			if (!ServicePointManager.s_disableStrongCrypto || !ServicePointManager.s_disableSystemDefaultTlsVersions)
			{
				string text = RegistryConfiguration.AppConfigReadString("System.Net.ServicePointManager.SecurityProtocol", null);
				SecurityProtocolType securityProtocolType;
				if (Enum.TryParse<SecurityProtocolType>(text, out securityProtocolType))
				{
					ServicePointManager.ValidateSecurityProtocol(securityProtocolType);
					defaultValue = (SslProtocols)securityProtocolType;
				}
			}
			return defaultValue;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0004373C File Offset: 0x0004193C
		private static bool LoadReusePortConfiguration(bool reusePortInternal)
		{
			int num = RegistryConfiguration.GlobalConfigReadInt("HWRPortReuseOnSocketBind", 0);
			if (num == 1)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.Web, typeof(ServicePointManager), SR.GetString("net_log_set_socketoption_reuseport_default_on"));
				}
				reusePortInternal = true;
			}
			return reusePortInternal;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00043784 File Offset: 0x00041984
		private static bool LoadDisableCertificateEKUsConfiguration(bool disable)
		{
			return LocalAppContextSwitches.DontCheckCertificateEKUs || RegistryConfiguration.AppConfigReadInt("System.Net.ServicePointManager.RequireCertificateEKUs", 1) == 0 || RegistryConfiguration.GlobalConfigReadInt("RequireCertificateEKUs", 1) == 0 || disable;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x000437C0 File Offset: 0x000419C0
		private static bool LoadUseHttpPipeliningAndBufferPoolingConfiguration(bool useFeature)
		{
			return RegistryConfiguration.AppConfigReadInt("System.Net.ServicePointManager.UseHttpPipeliningAndBufferPooling", 1) != 0 && RegistryConfiguration.GlobalConfigReadInt("UseHttpPipeliningAndBufferPooling", 1) != 0 && useFeature;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000437F0 File Offset: 0x000419F0
		private static bool LoadUseSafeSynchronousClose(bool useFeature)
		{
			return RegistryConfiguration.AppConfigReadInt("System.Net.ServicePointManager.UseSafeSynchronousClose", 1) != 0 && RegistryConfiguration.GlobalConfigReadInt("UseSafeSynchronousClose", 1) != 0 && useFeature;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00043820 File Offset: 0x00041A20
		private static bool LoadUseStrictRfcInterimResponseHandlingConfiguration(bool useFeature)
		{
			return RegistryConfiguration.AppConfigReadInt("System.Net.ServicePointManager.UseStrictRfcInterimResponseHandling", 1) != 0 && RegistryConfiguration.GlobalConfigReadInt("UseStrictRfcInterimResponseHandling", 1) != 0 && useFeature;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00043850 File Offset: 0x00041A50
		private static bool LoadAllowDangerousUnicodeDecompositionsConfiguration(bool useFeature)
		{
			int num = RegistryConfiguration.AppConfigReadInt("System.Uri.AllowDangerousUnicodeDecompositions", 0);
			if (num == 1)
			{
				return true;
			}
			num = RegistryConfiguration.GlobalConfigReadInt("AllowDangerousUnicodeDecompositions", 0);
			return num == 1 || useFeature;
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00043884 File Offset: 0x00041A84
		private static bool LoadUseStrictIPv6AddressParsingConfiguration(bool useFeature)
		{
			return RegistryConfiguration.AppConfigReadInt("System.Uri.UseStrictIPv6AddressParsing", 1) != 0 && RegistryConfiguration.GlobalConfigReadInt("UseStrictIPv6AddressParsing", 1) != 0 && useFeature;
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x000438B4 File Offset: 0x00041AB4
		private static bool LoadAllowAllUriEncodingExpansionConfiguration(bool useFeature)
		{
			int num = RegistryConfiguration.AppConfigReadInt("System.Uri.AllowAllUriEncodingExpansion", 0);
			if (num == 1)
			{
				return true;
			}
			num = RegistryConfiguration.GlobalConfigReadInt("AllowAllUriEncodingExpansion", 0);
			return num == 1 || useFeature;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000438E8 File Offset: 0x00041AE8
		private static bool LoadAllowFullDomainLiteralsConfiguration(bool useFeature)
		{
			int num = RegistryConfiguration.AppConfigReadInt("System.Net.AllowFullDomainLiterals", 0);
			if (num == 1)
			{
				return true;
			}
			num = RegistryConfiguration.GlobalConfigReadInt("AllowFullDomainLiterals", 0);
			return num == 1 || useFeature;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0004391C File Offset: 0x00041B1C
		private static bool LoadFinishProxyTunnelConnectionEarlyConfiguration(bool useFeature)
		{
			return RegistryConfiguration.AppConfigReadInt("System.Net.ServicePointManager.FinishProxyTunnelConnectionEarly", 1) != 0 && RegistryConfiguration.GlobalConfigReadInt("FinishProxyTunnelConnectionEarly", 1) != 0 && useFeature;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0004394C File Offset: 0x00041B4C
		private static void EnsureConfigurationLoaded()
		{
			if (ServicePointManager.s_configurationLoaded)
			{
				return;
			}
			object obj = ServicePointManager.s_configurationLoadedLock;
			lock (obj)
			{
				if (!ServicePointManager.s_configurationLoaded)
				{
					ServicePointManager.LoadConfiguration();
					ServicePointManager.s_configurationLoaded = true;
				}
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x000439A8 File Offset: 0x00041BA8
		private static T TryInitialize<T>(Func<T, T> loadConfiguration, T fallbackDefault)
		{
			T t;
			try
			{
				t = loadConfiguration(fallbackDefault);
			}
			catch (Exception ex)
			{
				if (NclUtilities.IsFatal(ex))
				{
					throw;
				}
				t = fallbackDefault;
			}
			return t;
		}

		/// <summary>The default number of non-persistent connections (4) allowed on a <see cref="T:System.Net.ServicePoint" /> object connected to an HTTP/1.0 or later server. This field is constant but is no longer used in the .NET Framework 2.0.</summary>
		// Token: 0x04001169 RID: 4457
		public const int DefaultNonPersistentConnectionLimit = 4;

		/// <summary>The default number of persistent connections (2) allowed on a <see cref="T:System.Net.ServicePoint" /> object connected to an HTTP/1.1 or later server. This field is constant and is used to initialize the <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> property if the value of the <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> property has not been set either directly or through configuration.</summary>
		// Token: 0x0400116A RID: 4458
		public const int DefaultPersistentConnectionLimit = 2;

		// Token: 0x0400116B RID: 4459
		private const int DefaultAspPersistentConnectionLimit = 10;

		// Token: 0x0400116C RID: 4460
		internal static readonly string SpecialConnectGroupName = "/.NET/NetClasses/HttpWebRequest/CONNECT__Group$$/";

		// Token: 0x0400116D RID: 4461
		internal static readonly TimerThread.Callback s_IdleServicePointTimeoutDelegate = new TimerThread.Callback(ServicePointManager.IdleServicePointTimeoutCallback);

		// Token: 0x0400116E RID: 4462
		private static Hashtable s_ServicePointTable = new Hashtable(10);

		// Token: 0x0400116F RID: 4463
		private static volatile TimerThread.Queue s_ServicePointIdlingQueue = TimerThread.GetOrCreateQueue(100000);

		// Token: 0x04001170 RID: 4464
		private static int s_MaxServicePoints = 0;

		// Token: 0x04001171 RID: 4465
		private static volatile CertPolicyValidationCallback s_CertPolicyValidationCallback = new CertPolicyValidationCallback();

		// Token: 0x04001172 RID: 4466
		private static volatile ServerCertValidationCallback s_ServerCertValidationCallback = null;

		// Token: 0x04001173 RID: 4467
		private static SecurityProtocolType s_SecurityProtocolType;

		// Token: 0x04001174 RID: 4468
		private static bool s_reusePort;

		// Token: 0x04001175 RID: 4469
		private static bool? s_reusePortSupported = null;

		// Token: 0x04001176 RID: 4470
		private static bool s_disableStrongCrypto;

		// Token: 0x04001177 RID: 4471
		private static bool s_disableSendAuxRecord;

		// Token: 0x04001178 RID: 4472
		private static bool s_disableSystemDefaultTlsVersions;

		// Token: 0x04001179 RID: 4473
		private static SslProtocols s_defaultSslProtocols;

		// Token: 0x0400117A RID: 4474
		private static bool s_disableCertificateEKUs;

		// Token: 0x0400117B RID: 4475
		private static bool s_useHttpPipeliningAndBufferPooling;

		// Token: 0x0400117C RID: 4476
		private static bool s_useSafeSynchronousClose;

		// Token: 0x0400117D RID: 4477
		private static bool s_useStrictRfcInterimResponseHandling;

		// Token: 0x0400117E RID: 4478
		private static bool s_allowDangerousUnicodeDecompositions;

		// Token: 0x0400117F RID: 4479
		private static bool s_useStrictIPv6AddressParsing;

		// Token: 0x04001180 RID: 4480
		private static bool s_allowAllUriEncodingExpansion;

		// Token: 0x04001181 RID: 4481
		private static bool s_allowFullDomainLiterals;

		// Token: 0x04001182 RID: 4482
		private static bool s_finishProxyTunnelConnectionEarly;

		// Token: 0x04001183 RID: 4483
		private static volatile Hashtable s_ConfigTable = null;

		// Token: 0x04001184 RID: 4484
		private static volatile int s_ConnectionLimit = ServicePointManager.PersistentConnectionLimit;

		// Token: 0x04001185 RID: 4485
		internal static volatile bool s_UseTcpKeepAlive = false;

		// Token: 0x04001186 RID: 4486
		internal static volatile int s_TcpKeepAliveTime;

		// Token: 0x04001187 RID: 4487
		internal static volatile int s_TcpKeepAliveInterval;

		// Token: 0x04001188 RID: 4488
		private static volatile bool s_UserChangedLimit;

		// Token: 0x04001189 RID: 4489
		private static object s_configurationLoadedLock = new object();

		// Token: 0x0400118A RID: 4490
		private static volatile bool s_configurationLoaded = false;

		// Token: 0x0400118B RID: 4491
		private const string RegistryGlobalStrongCryptoName = "SchUseStrongCrypto";

		// Token: 0x0400118C RID: 4492
		private const string RegistryGlobalReusePortName = "HWRPortReuseOnSocketBind";

		// Token: 0x0400118D RID: 4493
		private const string RegistryGlobalSendAuxRecordName = "SchSendAuxRecord";

		// Token: 0x0400118E RID: 4494
		private const string RegistryLocalSendAuxRecordName = "System.Net.ServicePointManager.SchSendAuxRecord";

		// Token: 0x0400118F RID: 4495
		private const string RegistryGlobalSystemDefaultTlsVersionsName = "SystemDefaultTlsVersions";

		// Token: 0x04001190 RID: 4496
		private const string RegistryLocalSystemDefaultTlsVersionsName = "System.Net.ServicePointManager.SystemDefaultTlsVersions";

		// Token: 0x04001191 RID: 4497
		private const string RegistryLocalSecureProtocolName = "System.Net.ServicePointManager.SecurityProtocol";

		// Token: 0x04001192 RID: 4498
		private const string RegistryGlobalRequireCertificateEKUs = "RequireCertificateEKUs";

		// Token: 0x04001193 RID: 4499
		private const string RegistryLocalRequireCertificateEKUs = "System.Net.ServicePointManager.RequireCertificateEKUs";

		// Token: 0x04001194 RID: 4500
		private const string RegistryGlobalUseHttpPipeliningAndBufferPooling = "UseHttpPipeliningAndBufferPooling";

		// Token: 0x04001195 RID: 4501
		private const string RegistryLocalUseHttpPipeliningAndBufferPooling = "System.Net.ServicePointManager.UseHttpPipeliningAndBufferPooling";

		// Token: 0x04001196 RID: 4502
		private const string RegistryGlobalUseSafeSynchronousClose = "UseSafeSynchronousClose";

		// Token: 0x04001197 RID: 4503
		private const string RegistryLocalUseSafeSynchronousClose = "System.Net.ServicePointManager.UseSafeSynchronousClose";

		// Token: 0x04001198 RID: 4504
		private const string RegistryGlobalUseStrictRfcInterimResponseHandling = "UseStrictRfcInterimResponseHandling";

		// Token: 0x04001199 RID: 4505
		private const string RegistryLocalUseStrictRfcInterimResponseHandling = "System.Net.ServicePointManager.UseStrictRfcInterimResponseHandling";

		// Token: 0x0400119A RID: 4506
		private const string RegistryGlobalAllowDangerousUnicodeDecompositions = "AllowDangerousUnicodeDecompositions";

		// Token: 0x0400119B RID: 4507
		private const string RegistryLocalAllowDangerousUnicodeDecompositions = "System.Uri.AllowDangerousUnicodeDecompositions";

		// Token: 0x0400119C RID: 4508
		private const string RegistryGlobalUseStrictIPv6AddressParsing = "UseStrictIPv6AddressParsing";

		// Token: 0x0400119D RID: 4509
		private const string RegistryLocalUseStrictIPv6AddressParsing = "System.Uri.UseStrictIPv6AddressParsing";

		// Token: 0x0400119E RID: 4510
		private const string RegistryGlobalAllowAllUriEncodingExpansion = "AllowAllUriEncodingExpansion";

		// Token: 0x0400119F RID: 4511
		private const string RegistryLocalAllowAllUriEncodingExpansion = "System.Uri.AllowAllUriEncodingExpansion";

		// Token: 0x040011A0 RID: 4512
		private const string RegistryGlobalAllowFullDomainLiterals = "AllowFullDomainLiterals";

		// Token: 0x040011A1 RID: 4513
		private const string RegistryLocalAllowFullDomainLiterals = "System.Net.AllowFullDomainLiterals";

		// Token: 0x040011A2 RID: 4514
		private const string RegistryGlobalFinishProxyTunnelConnectionEarly = "FinishProxyTunnelConnectionEarly";

		// Token: 0x040011A3 RID: 4515
		private const string RegistryLocalFinishProxyTunnelConnectionEarly = "System.Net.ServicePointManager.FinishProxyTunnelConnectionEarly";
	}
}
