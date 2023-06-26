using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32;

namespace System.Net.Security
{
	// Token: 0x02000350 RID: 848
	internal class SecureChannel
	{
		// Token: 0x06001E4F RID: 7759 RVA: 0x0008DDA4 File Offset: 0x0008BFA4
		internal SecureChannel(string hostname, bool serverMode, SchProtocols protocolFlags, X509Certificate serverCertificate, X509CertificateCollection clientCertificates, bool remoteCertRequired, bool checkCertName, bool checkCertRevocationStatus, EncryptionPolicy encryptionPolicy, LocalCertSelectionCallback certSelectionDelegate)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, ".ctor", string.Concat(new string[]
				{
					"hostname=",
					hostname,
					", #clientCertificates=",
					(clientCertificates == null) ? "0" : clientCertificates.Count.ToString(NumberFormatInfo.InvariantInfo),
					", encryptionPolicy=",
					encryptionPolicy.ToString()
				}));
			}
			SSPIWrapper.GetVerifyPackageInfo(GlobalSSPI.SSPISecureChannel, "Microsoft Unified Security Protocol Provider", true);
			this.m_Destination = hostname;
			this.m_HostName = hostname;
			this.m_ServerMode = serverMode;
			if (serverMode)
			{
				this.m_ProtocolFlags = protocolFlags & SchProtocols.ServerMask;
			}
			else
			{
				this.m_ProtocolFlags = protocolFlags & SchProtocols.ClientMask;
			}
			this.m_ServerCertificate = serverCertificate;
			this.m_ClientCertificates = clientCertificates;
			this.m_RemoteCertRequired = remoteCertRequired;
			this.m_SecurityContext = null;
			this.m_CheckCertRevocation = checkCertRevocationStatus;
			this.m_CheckCertName = checkCertName;
			this.m_CertSelectionDelegate = certSelectionDelegate;
			this.m_RefreshCredentialNeeded = true;
			this.m_EncryptionPolicy = encryptionPolicy;
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0008DEF8 File Offset: 0x0008C0F8
		private static bool CheckWindowsVersion()
		{
			try
			{
				int num;
				if ((int)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CurrentMajorVersionNumber", 0) >= 10 && int.TryParse((string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CurrentBuildNumber", "0"), out num) && num >= 18836)
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x0008DF6C File Offset: 0x0008C16C
		internal X509Certificate LocalServerCertificate
		{
			get
			{
				return this.m_ServerCertificate;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x0008DF74 File Offset: 0x0008C174
		internal X509Certificate LocalClientCertificate
		{
			get
			{
				return this.m_SelectedClientCertificate;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x0008DF7C File Offset: 0x0008C17C
		internal bool IsRemoteCertificateAvailable
		{
			get
			{
				return this.m_IsRemoteCertificateAvailable;
			}
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0008DF84 File Offset: 0x0008C184
		internal X509Certificate2 GetRemoteCertificate(out X509Certificate2Collection remoteCertificateStore)
		{
			remoteCertificateStore = null;
			if (this.m_SecurityContext == null)
			{
				return null;
			}
			X509Certificate2 x509Certificate = null;
			SafeFreeCertContext safeFreeCertContext = null;
			try
			{
				safeFreeCertContext = SSPIWrapper.QueryContextAttributes(GlobalSSPI.SSPISecureChannel, this.m_SecurityContext, ContextAttribute.RemoteCertificate) as SafeFreeCertContext;
				if (safeFreeCertContext != null && !safeFreeCertContext.IsInvalid)
				{
					x509Certificate = new X509Certificate2(safeFreeCertContext.DangerousGetHandle());
				}
			}
			finally
			{
				if (safeFreeCertContext != null)
				{
					remoteCertificateStore = SecureChannel.UnmanagedCertificateContext.GetStore(safeFreeCertContext);
					safeFreeCertContext.Close();
				}
			}
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, System.SR.GetString("net_log_remote_certificate", new object[] { (x509Certificate == null) ? "null" : x509Certificate.ToString(true) }));
			}
			return x509Certificate;
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0008E02C File Offset: 0x0008C22C
		internal ChannelBinding GetChannelBinding(ChannelBindingKind kind)
		{
			ChannelBinding channelBinding = null;
			if (this.m_SecurityContext != null)
			{
				channelBinding = SSPIWrapper.QueryContextChannelBinding(GlobalSSPI.SSPISecureChannel, this.m_SecurityContext, (ContextAttribute)kind);
			}
			return channelBinding;
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x0008E056 File Offset: 0x0008C256
		internal bool CheckCertRevocationStatus
		{
			get
			{
				return this.m_CheckCertRevocation;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x0008E05E File Offset: 0x0008C25E
		internal X509CertificateCollection ClientCertificates
		{
			get
			{
				return this.m_ClientCertificates;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x0008E066 File Offset: 0x0008C266
		internal int HeaderSize
		{
			get
			{
				return this.m_HeaderSize;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x0008E06E File Offset: 0x0008C26E
		internal int MaxDataSize
		{
			get
			{
				return this.m_MaxDataSize;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x0008E076 File Offset: 0x0008C276
		internal SslConnectionInfo ConnectionInfo
		{
			get
			{
				return this.m_ConnectionInfo;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x0008E07E File Offset: 0x0008C27E
		internal bool IsValidContext
		{
			get
			{
				return this.m_SecurityContext != null && !this.m_SecurityContext.IsInvalid;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x0008E098 File Offset: 0x0008C298
		internal bool IsServer
		{
			get
			{
				return this.m_ServerMode;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x0008E0A0 File Offset: 0x0008C2A0
		internal bool RemoteCertRequired
		{
			get
			{
				return this.m_RemoteCertRequired;
			}
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x0008E0A8 File Offset: 0x0008C2A8
		internal void SetRefreshCredentialNeeded()
		{
			this.m_RefreshCredentialNeeded = true;
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0008E0B1 File Offset: 0x0008C2B1
		internal void Close()
		{
			if (this.m_SecurityContext != null)
			{
				this.m_SecurityContext.Close();
			}
			if (this.m_CredentialsHandle != null)
			{
				this.m_CredentialsHandle.Close();
			}
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0008E0DC File Offset: 0x0008C2DC
		private X509Certificate2 EnsurePrivateKey(X509Certificate certificate)
		{
			if (certificate == null)
			{
				return null;
			}
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_locating_private_key_for_certificate", new object[] { certificate.ToString(true) }));
			}
			try
			{
				X509Certificate2 x509Certificate = certificate as X509Certificate2;
				Type type = certificate.GetType();
				string text = null;
				if (type != typeof(X509Certificate2) && type != typeof(X509Certificate))
				{
					if (certificate.Handle != IntPtr.Zero)
					{
						x509Certificate = new X509Certificate2(certificate);
						text = x509Certificate.GetCertHashString();
					}
				}
				else
				{
					text = certificate.GetCertHashString();
				}
				if (x509Certificate != null)
				{
					if (x509Certificate.HasPrivateKey)
					{
						if (Logging.On)
						{
							Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_cert_is_of_type_2"));
						}
						return x509Certificate;
					}
					if (certificate != x509Certificate)
					{
						x509Certificate.Reset();
					}
				}
				ExceptionHelper.KeyContainerPermissionOpen.Demand();
				X509Store x509Store = SecureChannel.EnsureStoreOpened(this.m_ServerMode);
				if (x509Store != null)
				{
					X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, text, false);
					if (x509Certificate2Collection.Count > 0 && x509Certificate2Collection[0].HasPrivateKey)
					{
						if (Logging.On)
						{
							Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_found_cert_in_store", new object[] { this.m_ServerMode ? "LocalMachine" : "CurrentUser" }));
						}
						return x509Certificate2Collection[0];
					}
				}
				x509Store = SecureChannel.EnsureStoreOpened(!this.m_ServerMode);
				if (x509Store != null)
				{
					X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, text, false);
					if (x509Certificate2Collection.Count > 0 && x509Certificate2Collection[0].HasPrivateKey)
					{
						if (Logging.On)
						{
							Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_found_cert_in_store", new object[] { this.m_ServerMode ? "CurrentUser" : "LocalMachine" }));
						}
						return x509Certificate2Collection[0];
					}
				}
			}
			catch (CryptographicException)
			{
			}
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_did_not_find_cert_in_store"));
			}
			return null;
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0008E2FC File Offset: 0x0008C4FC
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		internal static X509Store EnsureStoreOpened(bool isMachineStore)
		{
			X509Store x509Store = (isMachineStore ? SecureChannel.s_MyMachineCertStoreEx : SecureChannel.s_MyCertStoreEx);
			if (x509Store == null)
			{
				object obj = SecureChannel.s_SyncObject;
				lock (obj)
				{
					x509Store = (isMachineStore ? SecureChannel.s_MyMachineCertStoreEx : SecureChannel.s_MyCertStoreEx);
					if (x509Store == null)
					{
						StoreLocation storeLocation = (isMachineStore ? StoreLocation.LocalMachine : StoreLocation.CurrentUser);
						x509Store = new X509Store(StoreName.My, storeLocation);
						try
						{
							try
							{
								using (WindowsIdentity.Impersonate(IntPtr.Zero))
								{
									x509Store.Open(OpenFlags.OpenExistingOnly);
								}
							}
							catch
							{
								throw;
							}
							if (isMachineStore)
							{
								SecureChannel.s_MyMachineCertStoreEx = x509Store;
							}
							else
							{
								SecureChannel.s_MyCertStoreEx = x509Store;
							}
							return x509Store;
						}
						catch (Exception ex)
						{
							if (ex is CryptographicException || ex is SecurityException)
							{
								return null;
							}
							if (Logging.On)
							{
								Logging.PrintError(Logging.Web, System.SR.GetString("net_log_open_store_failed", new object[] { storeLocation, ex }));
							}
							throw;
						}
					}
				}
				return x509Store;
			}
			return x509Store;
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x0008E42C File Offset: 0x0008C62C
		private static X509Certificate2 MakeEx(X509Certificate certificate)
		{
			if (certificate.GetType() == typeof(X509Certificate2))
			{
				return (X509Certificate2)certificate;
			}
			X509Certificate2 x509Certificate = null;
			try
			{
				if (certificate.Handle != IntPtr.Zero)
				{
					x509Certificate = new X509Certificate2(certificate);
				}
			}
			catch (SecurityException)
			{
			}
			catch (CryptographicException)
			{
			}
			return x509Certificate;
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x0008E498 File Offset: 0x0008C698
		private unsafe string[] GetIssuers()
		{
			string[] array = new string[0];
			if (this.IsValidContext)
			{
				IssuerListInfoEx issuerListInfoEx = (IssuerListInfoEx)SSPIWrapper.QueryContextAttributes(GlobalSSPI.SSPISecureChannel, this.m_SecurityContext, ContextAttribute.IssuerListInfoEx);
				try
				{
					if (issuerListInfoEx.cIssuers > 0U)
					{
						uint cIssuers = issuerListInfoEx.cIssuers;
						array = new string[issuerListInfoEx.cIssuers];
						_CERT_CHAIN_ELEMENT* ptr = (_CERT_CHAIN_ELEMENT*)(void*)issuerListInfoEx.aIssuers.DangerousGetHandle();
						for (uint num = 0U; num < cIssuers; num += 1U)
						{
							_CERT_CHAIN_ELEMENT* ptr2 = ptr + (ulong)num * (ulong)((long)sizeof(_CERT_CHAIN_ELEMENT)) / (ulong)sizeof(_CERT_CHAIN_ELEMENT);
							uint cbSize = ptr2->cbSize;
							byte* ptr3 = (byte*)(void*)ptr2->pCertContext;
							byte[] array2 = new byte[cbSize];
							for (uint num2 = 0U; num2 < cbSize; num2 += 1U)
							{
								array2[(int)num2] = ptr3[num2];
							}
							X500DistinguishedName x500DistinguishedName = new X500DistinguishedName(array2);
							array[(int)num] = x500DistinguishedName.Name;
						}
					}
				}
				finally
				{
					if (issuerListInfoEx.aIssuers != null)
					{
						issuerListInfoEx.aIssuers.Close();
					}
				}
			}
			return array;
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x0008E598 File Offset: 0x0008C798
		[StorePermission(SecurityAction.Assert, Unrestricted = true)]
		private bool AcquireClientCredentials(ref byte[] thumbPrint)
		{
			X509Certificate x509Certificate = null;
			ArrayList arrayList = new ArrayList();
			string[] array = null;
			bool flag = false;
			if (this.m_CertSelectionDelegate != null)
			{
				if (array == null)
				{
					array = this.GetIssuers();
				}
				X509Certificate2 x509Certificate2 = null;
				try
				{
					X509Certificate2Collection x509Certificate2Collection;
					x509Certificate2 = this.GetRemoteCertificate(out x509Certificate2Collection);
					x509Certificate = this.m_CertSelectionDelegate(this.m_HostName, this.ClientCertificates, x509Certificate2, array);
				}
				finally
				{
					if (x509Certificate2 != null)
					{
						x509Certificate2.Reset();
					}
				}
				if (x509Certificate != null)
				{
					if (this.m_CredentialsHandle == null)
					{
						flag = true;
					}
					arrayList.Add(x509Certificate);
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_got_certificate_from_delegate"));
					}
				}
				else if (this.ClientCertificates.Count == 0)
				{
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_no_delegate_and_have_no_client_cert"));
					}
					flag = true;
				}
				else if (Logging.On)
				{
					Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_no_delegate_but_have_client_cert"));
				}
			}
			else if (this.m_CredentialsHandle == null && this.m_ClientCertificates != null && this.m_ClientCertificates.Count > 0)
			{
				x509Certificate = this.ClientCertificates[0];
				flag = true;
				if (x509Certificate != null)
				{
					arrayList.Add(x509Certificate);
				}
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_attempting_restart_using_cert", new object[] { (x509Certificate == null) ? "null" : x509Certificate.ToString(true) }));
				}
			}
			else if (this.m_ClientCertificates != null && this.m_ClientCertificates.Count > 0)
			{
				if (array == null)
				{
					array = this.GetIssuers();
				}
				if (Logging.On)
				{
					if (array == null || array.Length == 0)
					{
						Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_no_issuers_try_all_certs"));
					}
					else
					{
						Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_server_issuers_look_for_matching_certs", new object[] { array.Length }));
					}
				}
				int i = 0;
				while (i < this.m_ClientCertificates.Count)
				{
					if (array != null && array.Length != 0)
					{
						X509Certificate2 x509Certificate3 = null;
						X509Chain x509Chain = null;
						try
						{
							x509Certificate3 = SecureChannel.MakeEx(this.m_ClientCertificates[i]);
							if (x509Certificate3 == null)
							{
								goto IL_306;
							}
							x509Chain = new X509Chain();
							x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
							x509Chain.ChainPolicy.VerificationFlags = X509VerificationFlags.IgnoreInvalidName;
							x509Chain.Build(x509Certificate3);
							bool flag2 = false;
							if (x509Chain.ChainElements.Count > 0)
							{
								for (int j = 0; j < x509Chain.ChainElements.Count; j++)
								{
									string issuer = x509Chain.ChainElements[j].Certificate.Issuer;
									flag2 = Array.IndexOf<string>(array, issuer) != -1;
									if (flag2)
									{
										break;
									}
								}
							}
							if (!flag2)
							{
								goto IL_306;
							}
						}
						finally
						{
							if (x509Chain != null)
							{
								x509Chain.Reset();
							}
							if (x509Certificate3 != null && x509Certificate3 != this.m_ClientCertificates[i])
							{
								x509Certificate3.Reset();
							}
						}
						goto IL_2BA;
					}
					goto IL_2BA;
					IL_306:
					i++;
					continue;
					IL_2BA:
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_selected_cert", new object[] { this.m_ClientCertificates[i].ToString(true) }));
					}
					arrayList.Add(this.m_ClientCertificates[i]);
					goto IL_306;
				}
			}
			bool flag3 = false;
			X509Certificate2 x509Certificate4 = null;
			x509Certificate = null;
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_n_certs_after_filtering", new object[] { arrayList.Count }));
				if (arrayList.Count != 0)
				{
					Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_finding_matching_certs"));
				}
			}
			for (int k = 0; k < arrayList.Count; k++)
			{
				x509Certificate = arrayList[k] as X509Certificate;
				if ((x509Certificate4 = this.EnsurePrivateKey(x509Certificate)) != null)
				{
					break;
				}
				x509Certificate = null;
				x509Certificate4 = null;
			}
			try
			{
				byte[] array2 = ((x509Certificate4 == null) ? null : x509Certificate4.GetCertHash());
				SafeFreeCredentials safeFreeCredentials = SslSessionsCache.TryCachedCredential(array2, this.m_ProtocolFlags, this.m_EncryptionPolicy);
				if (flag && safeFreeCredentials == null && x509Certificate4 != null)
				{
					if (x509Certificate != x509Certificate4)
					{
						x509Certificate4.Reset();
					}
					array2 = null;
					x509Certificate4 = null;
					x509Certificate = null;
				}
				if (safeFreeCredentials != null)
				{
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.Web, System.SR.GetString("net_log_using_cached_credential"));
					}
					this.m_CredentialsHandle = safeFreeCredentials;
					this.m_SelectedClientCertificate = x509Certificate;
					flag3 = true;
				}
				else
				{
					SecureCredential.Flags flags = SecureCredential.Flags.ValidateManual | SecureCredential.Flags.NoDefaultCred;
					if (!ServicePointManager.DisableSendAuxRecord)
					{
						flags |= SecureCredential.Flags.SendAuxRecord;
					}
					if (!ServicePointManager.DisableStrongCrypto && (this.m_ProtocolFlags == SchProtocols.Zero || (this.m_ProtocolFlags & (SchProtocols.Tls10Client | SchProtocols.Tls10Server | SchProtocols.Tls11Client | SchProtocols.Tls11Server | SchProtocols.Tls12Client | SchProtocols.Tls12Server | SchProtocols.Tls13Client | SchProtocols.Tls13Server)) != SchProtocols.Zero) && this.m_EncryptionPolicy != EncryptionPolicy.AllowNoEncryption && this.m_EncryptionPolicy != EncryptionPolicy.NoEncryption)
					{
						flags |= SecureCredential.Flags.UseStrongCrypto;
					}
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.Web, this, ".AcquireClientCredentials, new SecureCredential() ", string.Concat(new string[]
						{
							"flags=(",
							flags.ToString(),
							"), m_ProtocolFlags=(",
							this.m_ProtocolFlags.ToString(),
							"), m_EncryptionPolicy=",
							this.m_EncryptionPolicy.ToString()
						}));
					}
					this.m_CredentialsHandle = this.AcquireCredentialsHandle(CredentialUse.Outbound, x509Certificate4, flags);
					thumbPrint = array2;
					this.m_SelectedClientCertificate = x509Certificate;
				}
			}
			finally
			{
				if (x509Certificate4 != null && x509Certificate != x509Certificate4)
				{
					x509Certificate4.Reset();
				}
			}
			return flag3;
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0008EB0C File Offset: 0x0008CD0C
		[StorePermission(SecurityAction.Assert, Unrestricted = true)]
		private bool AcquireServerCredentials(ref byte[] thumbPrint)
		{
			X509Certificate x509Certificate = null;
			bool flag = false;
			if (this.m_CertSelectionDelegate != null)
			{
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.Add(this.m_ServerCertificate);
				x509Certificate = this.m_CertSelectionDelegate(string.Empty, x509CertificateCollection, null, new string[0]);
			}
			else
			{
				x509Certificate = this.m_ServerCertificate;
			}
			if (x509Certificate == null)
			{
				throw new NotSupportedException(System.SR.GetString("net_ssl_io_no_server_cert"));
			}
			X509Certificate2 x509Certificate2 = this.EnsurePrivateKey(x509Certificate);
			if (x509Certificate2 == null)
			{
				throw new NotSupportedException(System.SR.GetString("net_ssl_io_no_server_cert"));
			}
			byte[] certHash = x509Certificate2.GetCertHash();
			try
			{
				SafeFreeCredentials safeFreeCredentials = SslSessionsCache.TryCachedCredential(certHash, this.m_ProtocolFlags, this.m_EncryptionPolicy);
				if (safeFreeCredentials != null)
				{
					this.m_CredentialsHandle = safeFreeCredentials;
					this.m_ServerCertificate = x509Certificate;
					flag = true;
				}
				else
				{
					SecureCredential.Flags flags = SecureCredential.Flags.Zero;
					if (!ServicePointManager.DisableSendAuxRecord)
					{
						flags |= SecureCredential.Flags.SendAuxRecord;
					}
					this.m_CredentialsHandle = this.AcquireCredentialsHandle(CredentialUse.Inbound, x509Certificate2, flags);
					thumbPrint = certHash;
					this.m_ServerCertificate = x509Certificate;
				}
			}
			finally
			{
				if (x509Certificate != x509Certificate2)
				{
					x509Certificate2.Reset();
				}
			}
			return flag;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0008EC0C File Offset: 0x0008CE0C
		private unsafe SafeFreeCredentials AcquireCredentialsHandle(CredentialUse credUsage, X509Certificate2 selectedCert, SecureCredential.Flags flags)
		{
			if ((this.m_ProtocolFlags != SchProtocols.Zero && (this.m_ProtocolFlags & SchProtocols.Tls13) == SchProtocols.Zero) || !SecureChannel.CanUseNewCryptoApi || this.m_EncryptionPolicy == EncryptionPolicy.NoEncryption || LocalAppContextSwitches.DontEnableTls13)
			{
				SecureCredential secureCredential = new SecureCredential(4, selectedCert, flags, this.m_ProtocolFlags, this.m_EncryptionPolicy);
				return this.AcquireCredentialsHandle(credUsage, ref secureCredential);
			}
			SecureCredential2 secureCredential2 = new SecureCredential2((SecureCredential2.Flags)flags, this.m_ProtocolFlags, this.m_EncryptionPolicy);
			if (this.m_ProtocolFlags != SchProtocols.Zero)
			{
				TlsParamaters tlsParamaters = new TlsParamaters(this.m_ProtocolFlags);
				secureCredential2.cTlsParameters = 1;
				secureCredential2.pTlsParameters = &tlsParamaters;
			}
			void* ptr = null;
			if (selectedCert != null)
			{
				ptr = (void*)selectedCert.Handle;
				secureCredential2.certContextArray = &ptr;
				secureCredential2.cCreds = 1;
			}
			return this.AcquireCredentialsHandle(credUsage, ref secureCredential2);
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0008ECD0 File Offset: 0x0008CED0
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		private SafeFreeCredentials AcquireCredentialsHandle(CredentialUse credUsage, ref SecureCredential secureCredential)
		{
			SafeFreeCredentials safeFreeCredentials;
			try
			{
				using (WindowsIdentity.Impersonate(IntPtr.Zero))
				{
					safeFreeCredentials = SSPIWrapper.AcquireCredentialsHandle(GlobalSSPI.SSPISecureChannel, "Microsoft Unified Security Protocol Provider", credUsage, secureCredential);
				}
			}
			catch
			{
				safeFreeCredentials = SSPIWrapper.AcquireCredentialsHandle(GlobalSSPI.SSPISecureChannel, "Microsoft Unified Security Protocol Provider", credUsage, secureCredential);
			}
			return safeFreeCredentials;
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x0008ED44 File Offset: 0x0008CF44
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.ControlPrincipal)]
		private SafeFreeCredentials AcquireCredentialsHandle(CredentialUse credUsage, ref SecureCredential2 secureCredential)
		{
			SafeFreeCredentials safeFreeCredentials;
			try
			{
				using (WindowsIdentity.Impersonate(IntPtr.Zero))
				{
					safeFreeCredentials = SSPIWrapper.AcquireCredentialsHandle(GlobalSSPI.SSPISecureChannel, "Microsoft Unified Security Protocol Provider", credUsage, secureCredential);
				}
			}
			catch
			{
				safeFreeCredentials = SSPIWrapper.AcquireCredentialsHandle(GlobalSSPI.SSPISecureChannel, "Microsoft Unified Security Protocol Provider", credUsage, secureCredential);
			}
			return safeFreeCredentials;
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0008EDB8 File Offset: 0x0008CFB8
		internal ProtocolToken NextMessage(byte[] incoming, int offset, int count)
		{
			byte[] array = null;
			SecurityStatus securityStatus = this.GenerateToken(incoming, offset, count, ref array);
			if (!this.m_ServerMode && securityStatus == SecurityStatus.CredentialsNeeded)
			{
				this.SetRefreshCredentialNeeded();
				securityStatus = this.GenerateToken(incoming, offset, count, ref array);
			}
			return new ProtocolToken(array, securityStatus);
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0008EE00 File Offset: 0x0008D000
		private SecurityStatus GenerateToken(byte[] input, int offset, int count, ref byte[] output)
		{
			if (offset < 0 || offset > ((input == null) ? 0 : input.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > ((input == null) ? 0 : (input.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			SecurityBuffer securityBuffer = null;
			SecurityBuffer[] array = null;
			if (input != null)
			{
				securityBuffer = new SecurityBuffer(input, offset, count, BufferType.Token);
				array = new SecurityBuffer[]
				{
					securityBuffer,
					new SecurityBuffer(null, 0, 0, BufferType.Empty)
				};
			}
			SecurityBuffer securityBuffer2 = new SecurityBuffer(null, BufferType.Token);
			int num = 0;
			bool flag = false;
			byte[] array2 = null;
			try
			{
				do
				{
					array2 = null;
					if (this.m_RefreshCredentialNeeded)
					{
						flag = (this.m_ServerMode ? this.AcquireServerCredentials(ref array2) : this.AcquireClientCredentials(ref array2));
					}
					if (this.m_ServerMode)
					{
						num = SSPIWrapper.AcceptSecurityContext(GlobalSSPI.SSPISecureChannel, ref this.m_CredentialsHandle, ref this.m_SecurityContext, ContextFlags.ReplayDetect | ContextFlags.SequenceDetect | ContextFlags.Confidentiality | ContextFlags.AllocateMemory | ContextFlags.AcceptStream | (this.m_RemoteCertRequired ? ContextFlags.MutualAuth : ContextFlags.Zero), Endianness.Native, securityBuffer, securityBuffer2, ref this.m_Attributes);
					}
					else if (securityBuffer == null)
					{
						num = SSPIWrapper.InitializeSecurityContext(GlobalSSPI.SSPISecureChannel, ref this.m_CredentialsHandle, ref this.m_SecurityContext, this.m_Destination, ContextFlags.ReplayDetect | ContextFlags.SequenceDetect | ContextFlags.Confidentiality | ContextFlags.AllocateMemory | ContextFlags.InitManualCredValidation, Endianness.Native, securityBuffer, securityBuffer2, ref this.m_Attributes);
						if ((num == 0 || num == 590610) && ComNetOS.IsWin8orLater && Microsoft.Win32.UnsafeNativeMethods.IsPackagedProcess.Value)
						{
							int num2 = SSPIWrapper.SetContextAttributes(GlobalSSPI.SSPISecureChannel, this.m_SecurityContext, ContextAttribute.UiInfo, UnsafeNclNativeMethods.AppXHelper.PrimaryWindowHandle.Value);
						}
					}
					else
					{
						num = SSPIWrapper.InitializeSecurityContext(GlobalSSPI.SSPISecureChannel, this.m_CredentialsHandle, ref this.m_SecurityContext, this.m_Destination, ContextFlags.ReplayDetect | ContextFlags.SequenceDetect | ContextFlags.Confidentiality | ContextFlags.AllocateMemory | ContextFlags.InitManualCredValidation, Endianness.Native, array, securityBuffer2, ref this.m_Attributes);
					}
				}
				while (flag && this.m_CredentialsHandle == null);
			}
			finally
			{
				if (this.m_RefreshCredentialNeeded)
				{
					this.m_RefreshCredentialNeeded = false;
					if (this.m_CredentialsHandle != null)
					{
						this.m_CredentialsHandle.Close();
					}
					if (!flag && this.m_SecurityContext != null && !this.m_SecurityContext.IsInvalid && !this.m_CredentialsHandle.IsInvalid)
					{
						SslSessionsCache.CacheCredential(this.m_CredentialsHandle, array2, this.m_ProtocolFlags, this.m_EncryptionPolicy);
					}
				}
			}
			output = securityBuffer2.token;
			return (SecurityStatus)num;
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0008F01C File Offset: 0x0008D21C
		internal void ProcessHandshakeSuccess()
		{
			StreamSizes streamSizes = SSPIWrapper.QueryContextAttributes(GlobalSSPI.SSPISecureChannel, this.m_SecurityContext, ContextAttribute.StreamSizes) as StreamSizes;
			if (streamSizes != null)
			{
				try
				{
					this.m_HeaderSize = streamSizes.header;
					this.m_TrailerSize = streamSizes.trailer;
					this.m_MaxDataSize = checked(streamSizes.maximumMessage - (this.m_HeaderSize + this.m_TrailerSize));
				}
				catch (Exception ex)
				{
					NclUtilities.IsFatal(ex);
					throw;
				}
			}
			this.m_ConnectionInfo = SSPIWrapper.QueryContextAttributes(GlobalSSPI.SSPISecureChannel, this.m_SecurityContext, ContextAttribute.ConnectionInfo) as SslConnectionInfo;
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0008F0B0 File Offset: 0x0008D2B0
		internal SecurityStatus Encrypt(byte[] buffer, int offset, int size, ref byte[] output, out int resultSize)
		{
			byte[] array;
			try
			{
				if (offset < 0 || offset > ((buffer == null) ? 0 : buffer.Length))
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (size < 0 || size > ((buffer == null) ? 0 : (buffer.Length - offset)))
				{
					throw new ArgumentOutOfRangeException("size");
				}
				resultSize = 0;
				int num = checked(size + this.m_HeaderSize + this.m_TrailerSize);
				if (output != null && num <= output.Length)
				{
					array = output;
				}
				else
				{
					array = new byte[num];
				}
				Buffer.BlockCopy(buffer, offset, array, this.m_HeaderSize, size);
			}
			catch (Exception ex)
			{
				NclUtilities.IsFatal(ex);
				throw;
			}
			SecurityBuffer[] array2 = new SecurityBuffer[]
			{
				new SecurityBuffer(array, 0, this.m_HeaderSize, BufferType.Header),
				new SecurityBuffer(array, this.m_HeaderSize, size, BufferType.Data),
				new SecurityBuffer(array, this.m_HeaderSize + size, this.m_TrailerSize, BufferType.Trailer),
				new SecurityBuffer(null, BufferType.Empty)
			};
			int num2 = SSPIWrapper.EncryptMessage(GlobalSSPI.SSPISecureChannel, this.m_SecurityContext, array2, 0U);
			if (num2 != 0)
			{
				return (SecurityStatus)num2;
			}
			output = array;
			resultSize = array2[0].size + array2[1].size + array2[2].size;
			return SecurityStatus.OK;
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x0008F1D8 File Offset: 0x0008D3D8
		internal SecurityStatus Decrypt(byte[] payload, ref int offset, ref int count)
		{
			if (offset < 0 || offset > ((payload == null) ? 0 : payload.Length))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > ((payload == null) ? 0 : (payload.Length - offset)))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			SecurityBuffer[] array = new SecurityBuffer[]
			{
				new SecurityBuffer(payload, offset, count, BufferType.Data),
				new SecurityBuffer(null, BufferType.Empty),
				new SecurityBuffer(null, BufferType.Empty),
				new SecurityBuffer(null, BufferType.Empty)
			};
			SecurityStatus securityStatus = (SecurityStatus)SSPIWrapper.DecryptMessage(GlobalSSPI.SSPISecureChannel, this.m_SecurityContext, array, 0U);
			count = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if ((securityStatus == SecurityStatus.OK && array[i].type == BufferType.Data) || (securityStatus != SecurityStatus.OK && array[i].type == BufferType.Extra))
				{
					offset = array[i].offset;
					count = array[i].size;
					break;
				}
			}
			return securityStatus;
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0008F2AC File Offset: 0x0008D4AC
		[StorePermission(SecurityAction.Assert, Unrestricted = true)]
		internal unsafe bool VerifyRemoteCertificate(RemoteCertValidationCallback remoteCertValidationCallback, ref ProtocolToken alertToken)
		{
			SslPolicyErrors sslPolicyErrors = SslPolicyErrors.None;
			bool flag = false;
			X509Chain x509Chain = null;
			X509Certificate2 x509Certificate = null;
			try
			{
				X509Certificate2Collection x509Certificate2Collection;
				x509Certificate = this.GetRemoteCertificate(out x509Certificate2Collection);
				if (x509Certificate == null)
				{
					sslPolicyErrors |= SslPolicyErrors.RemoteCertificateNotAvailable;
					this.m_IsRemoteCertificateAvailable = false;
				}
				else
				{
					if (this.m_IsRemoteCertificateAvailable)
					{
						SslConnectionInfo connectionInfo = this.m_ConnectionInfo;
						if (connectionInfo == null || (connectionInfo.Protocol & 12288) != 0)
						{
							return true;
						}
					}
					this.m_IsRemoteCertificateAvailable = true;
					x509Chain = new X509Chain();
					x509Chain.ChainPolicy.RevocationMode = (this.m_CheckCertRevocation ? X509RevocationMode.Online : X509RevocationMode.NoCheck);
					x509Chain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot;
					if (!ServicePointManager.DisableCertificateEKUs)
					{
						x509Chain.ChainPolicy.ApplicationPolicy.Add(this.m_ServerMode ? this.m_ClientAuthOid : this.m_ServerAuthOid);
					}
					if (x509Certificate2Collection != null)
					{
						x509Chain.ChainPolicy.ExtraStore.AddRange(x509Certificate2Collection);
					}
					if (!x509Chain.Build(x509Certificate) && x509Chain.ChainContext == IntPtr.Zero)
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					if (this.m_CheckCertName)
					{
						ChainPolicyParameter chainPolicyParameter = default(ChainPolicyParameter);
						chainPolicyParameter.cbSize = ChainPolicyParameter.StructSize;
						chainPolicyParameter.dwFlags = 0U;
						SSL_EXTRA_CERT_CHAIN_POLICY_PARA ssl_EXTRA_CERT_CHAIN_POLICY_PARA = new SSL_EXTRA_CERT_CHAIN_POLICY_PARA(this.IsServer);
						chainPolicyParameter.pvExtraPolicyPara = &ssl_EXTRA_CERT_CHAIN_POLICY_PARA;
						try
						{
							fixed (string text = this.m_HostName)
							{
								char* ptr = text;
								if (ptr != null)
								{
									ptr += RuntimeHelpers.OffsetToStringData / 2;
								}
								ssl_EXTRA_CERT_CHAIN_POLICY_PARA.pwszServerName = ptr;
								chainPolicyParameter.dwFlags |= 4031U;
								SafeFreeCertChain safeFreeCertChain = new SafeFreeCertChain(x509Chain.ChainContext);
								uint num = PolicyWrapper.VerifyChainPolicy(safeFreeCertChain, ref chainPolicyParameter);
								if (num == 2148204815U)
								{
									sslPolicyErrors |= SslPolicyErrors.RemoteCertificateNameMismatch;
								}
							}
						}
						finally
						{
							string text = null;
						}
					}
					X509ChainStatus[] chainStatus = x509Chain.ChainStatus;
					if (chainStatus != null && chainStatus.Length != 0)
					{
						sslPolicyErrors |= SslPolicyErrors.RemoteCertificateChainErrors;
					}
				}
				if (remoteCertValidationCallback != null)
				{
					flag = remoteCertValidationCallback(this.m_HostName, x509Certificate, x509Chain, sslPolicyErrors);
				}
				else
				{
					flag = (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNotAvailable && !this.m_RemoteCertRequired) || sslPolicyErrors == SslPolicyErrors.None;
				}
				if (Logging.On)
				{
					this.LogCertificateValidation(remoteCertValidationCallback, sslPolicyErrors, flag, x509Chain);
				}
				if (LocalAppContextSwitches.DontEnableTlsAlerts)
				{
					alertToken = null;
				}
				else if (!flag)
				{
					alertToken = this.CreateFatalHandshakeAlertToken(sslPolicyErrors, x509Chain);
				}
			}
			finally
			{
				if (x509Chain != null)
				{
					x509Chain.Reset();
				}
				if (x509Certificate != null)
				{
					x509Certificate.Reset();
				}
			}
			return flag;
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0008F4FC File Offset: 0x0008D6FC
		public ProtocolToken CreateFatalHandshakeAlertToken(SslPolicyErrors sslPolicyErrors, X509Chain chain)
		{
			TlsAlertMessage tlsAlertMessage;
			switch (sslPolicyErrors)
			{
			case SslPolicyErrors.RemoteCertificateNameMismatch:
				tlsAlertMessage = TlsAlertMessage.BadCertificate;
				goto IL_2B;
			case SslPolicyErrors.RemoteCertificateChainErrors:
				tlsAlertMessage = SecureChannel.GetAlertMessageFromChain(chain);
				goto IL_2B;
			}
			tlsAlertMessage = TlsAlertMessage.CertificateUnknown;
			IL_2B:
			SecurityStatus securityStatus = (SecurityStatus)SSPIWrapper.ApplyAlertToken(GlobalSSPI.SSPISecureChannel, ref this.m_CredentialsHandle, this.m_SecurityContext, TlsAlertType.Fatal, tlsAlertMessage);
			if (securityStatus != SecurityStatus.OK)
			{
				throw new Win32Exception((int)securityStatus);
			}
			return this.GenerateAlertToken();
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x0008F560 File Offset: 0x0008D760
		public ProtocolToken CreateShutdownToken()
		{
			SecurityStatus securityStatus = (SecurityStatus)SSPIWrapper.ApplyShutdownToken(GlobalSSPI.SSPISecureChannel, ref this.m_CredentialsHandle, this.m_SecurityContext);
			if (securityStatus != SecurityStatus.OK)
			{
				throw new Win32Exception((int)securityStatus);
			}
			return this.GenerateAlertToken();
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0008F598 File Offset: 0x0008D798
		private ProtocolToken GenerateAlertToken()
		{
			byte[] array = null;
			SecurityStatus securityStatus = this.GenerateToken(null, 0, 0, ref array);
			return new ProtocolToken(array, securityStatus);
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x0008F5BC File Offset: 0x0008D7BC
		private static TlsAlertMessage GetAlertMessageFromChain(X509Chain chain)
		{
			X509ChainStatus[] chainStatus = chain.ChainStatus;
			int i = 0;
			while (i < chainStatus.Length)
			{
				X509ChainStatus x509ChainStatus = chainStatus[i];
				if (x509ChainStatus.Status != X509ChainStatusFlags.NoError)
				{
					if ((x509ChainStatus.Status & (X509ChainStatusFlags.UntrustedRoot | X509ChainStatusFlags.Cyclic | X509ChainStatusFlags.PartialChain)) != X509ChainStatusFlags.NoError)
					{
						return TlsAlertMessage.UnknownCA;
					}
					if ((x509ChainStatus.Status & (X509ChainStatusFlags.Revoked | X509ChainStatusFlags.OfflineRevocation)) != X509ChainStatusFlags.NoError)
					{
						return TlsAlertMessage.CertificateRevoked;
					}
					if ((x509ChainStatus.Status & (X509ChainStatusFlags.NotTimeValid | X509ChainStatusFlags.NotTimeNested | X509ChainStatusFlags.CtlNotTimeValid)) != X509ChainStatusFlags.NoError)
					{
						return TlsAlertMessage.CertificateExpired;
					}
					if ((x509ChainStatus.Status & X509ChainStatusFlags.CtlNotValidForUsage) != X509ChainStatusFlags.NoError)
					{
						return TlsAlertMessage.UnsupportedCert;
					}
					if (((x509ChainStatus.Status & (X509ChainStatusFlags.NotSignatureValid | X509ChainStatusFlags.InvalidExtension | X509ChainStatusFlags.InvalidPolicyConstraints | X509ChainStatusFlags.CtlNotSignatureValid)) | X509ChainStatusFlags.NoIssuanceChainPolicy | X509ChainStatusFlags.NotValidForUsage) != X509ChainStatusFlags.NoError)
					{
						return TlsAlertMessage.BadCertificate;
					}
					return TlsAlertMessage.CertificateUnknown;
				}
				else
				{
					i++;
				}
			}
			return TlsAlertMessage.BadCertificate;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0008F65C File Offset: 0x0008D85C
		private void LogCertificateValidation(RemoteCertValidationCallback remoteCertValidationCallback, SslPolicyErrors sslPolicyErrors, bool success, X509Chain chain)
		{
			if (sslPolicyErrors != SslPolicyErrors.None)
			{
				Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_remote_cert_has_errors"));
				if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) != SslPolicyErrors.None)
				{
					Logging.PrintInfo(Logging.Web, this, "\t" + System.SR.GetString("net_log_remote_cert_not_available"));
				}
				if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) != SslPolicyErrors.None)
				{
					Logging.PrintInfo(Logging.Web, this, "\t" + System.SR.GetString("net_log_remote_cert_name_mismatch"));
				}
				if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) != SslPolicyErrors.None)
				{
					foreach (X509ChainStatus x509ChainStatus in chain.ChainStatus)
					{
						Logging.PrintInfo(Logging.Web, this, "\t" + x509ChainStatus.StatusInformation);
					}
				}
			}
			if (!success)
			{
				if (remoteCertValidationCallback != null)
				{
					Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_remote_cert_user_declared_invalid"));
				}
				return;
			}
			if (remoteCertValidationCallback != null)
			{
				Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_remote_cert_user_declared_valid"));
				return;
			}
			Logging.PrintInfo(Logging.Web, this, System.SR.GetString("net_log_remote_cert_has_no_errors"));
		}

		// Token: 0x04001CAF RID: 7343
		internal const string SecurityPackage = "Microsoft Unified Security Protocol Provider";

		// Token: 0x04001CB0 RID: 7344
		private static readonly object s_SyncObject = new object();

		// Token: 0x04001CB1 RID: 7345
		private static readonly bool CanUseNewCryptoApi = SecureChannel.CheckWindowsVersion();

		// Token: 0x04001CB2 RID: 7346
		private const ContextFlags RequiredFlags = ContextFlags.ReplayDetect | ContextFlags.SequenceDetect | ContextFlags.Confidentiality | ContextFlags.AllocateMemory;

		// Token: 0x04001CB3 RID: 7347
		private const ContextFlags ServerRequiredFlags = ContextFlags.ReplayDetect | ContextFlags.SequenceDetect | ContextFlags.Confidentiality | ContextFlags.AllocateMemory | ContextFlags.AcceptStream;

		// Token: 0x04001CB4 RID: 7348
		private const int ChainRevocationCheckExcludeRoot = 1073741824;

		// Token: 0x04001CB5 RID: 7349
		internal const int ReadHeaderSize = 5;

		// Token: 0x04001CB6 RID: 7350
		private static volatile X509Store s_MyCertStoreEx;

		// Token: 0x04001CB7 RID: 7351
		private static volatile X509Store s_MyMachineCertStoreEx;

		// Token: 0x04001CB8 RID: 7352
		private SafeFreeCredentials m_CredentialsHandle;

		// Token: 0x04001CB9 RID: 7353
		private SafeDeleteContext m_SecurityContext;

		// Token: 0x04001CBA RID: 7354
		private ContextFlags m_Attributes;

		// Token: 0x04001CBB RID: 7355
		private readonly string m_Destination;

		// Token: 0x04001CBC RID: 7356
		private readonly string m_HostName;

		// Token: 0x04001CBD RID: 7357
		private readonly bool m_ServerMode;

		// Token: 0x04001CBE RID: 7358
		private readonly bool m_RemoteCertRequired;

		// Token: 0x04001CBF RID: 7359
		private readonly SchProtocols m_ProtocolFlags;

		// Token: 0x04001CC0 RID: 7360
		private readonly EncryptionPolicy m_EncryptionPolicy;

		// Token: 0x04001CC1 RID: 7361
		private SslConnectionInfo m_ConnectionInfo;

		// Token: 0x04001CC2 RID: 7362
		private X509Certificate m_ServerCertificate;

		// Token: 0x04001CC3 RID: 7363
		private X509Certificate m_SelectedClientCertificate;

		// Token: 0x04001CC4 RID: 7364
		private bool m_IsRemoteCertificateAvailable;

		// Token: 0x04001CC5 RID: 7365
		private readonly X509CertificateCollection m_ClientCertificates;

		// Token: 0x04001CC6 RID: 7366
		private LocalCertSelectionCallback m_CertSelectionDelegate;

		// Token: 0x04001CC7 RID: 7367
		private int m_HeaderSize = 5;

		// Token: 0x04001CC8 RID: 7368
		private int m_TrailerSize = 16;

		// Token: 0x04001CC9 RID: 7369
		private int m_MaxDataSize = 16354;

		// Token: 0x04001CCA RID: 7370
		private bool m_CheckCertRevocation;

		// Token: 0x04001CCB RID: 7371
		private bool m_CheckCertName;

		// Token: 0x04001CCC RID: 7372
		private bool m_RefreshCredentialNeeded;

		// Token: 0x04001CCD RID: 7373
		private readonly Oid m_ServerAuthOid = new Oid("1.3.6.1.5.5.7.3.1", "1.3.6.1.5.5.7.3.1");

		// Token: 0x04001CCE RID: 7374
		private readonly Oid m_ClientAuthOid = new Oid("1.3.6.1.5.5.7.3.2", "1.3.6.1.5.5.7.3.2");

		// Token: 0x020007CB RID: 1995
		private static class UnmanagedCertificateContext
		{
			// Token: 0x0600437E RID: 17278 RVA: 0x0011C514 File Offset: 0x0011A714
			internal static X509Certificate2Collection GetStore(SafeFreeCertContext certContext)
			{
				X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
				if (certContext.IsInvalid)
				{
					return x509Certificate2Collection;
				}
				SecureChannel.UnmanagedCertificateContext._CERT_CONTEXT cert_CONTEXT = (SecureChannel.UnmanagedCertificateContext._CERT_CONTEXT)Marshal.PtrToStructure(certContext.DangerousGetHandle(), typeof(SecureChannel.UnmanagedCertificateContext._CERT_CONTEXT));
				if (cert_CONTEXT.hCertStore != IntPtr.Zero)
				{
					X509Store x509Store = null;
					try
					{
						x509Store = new X509Store(cert_CONTEXT.hCertStore);
						x509Certificate2Collection = x509Store.Certificates;
					}
					finally
					{
						if (x509Store != null)
						{
							x509Store.Close();
						}
					}
				}
				return x509Certificate2Collection;
			}

			// Token: 0x0200091F RID: 2335
			private struct _CERT_CONTEXT
			{
				// Token: 0x04003D88 RID: 15752
				internal int dwCertEncodingType;

				// Token: 0x04003D89 RID: 15753
				internal IntPtr pbCertEncoded;

				// Token: 0x04003D8A RID: 15754
				internal int cbCertEncoded;

				// Token: 0x04003D8B RID: 15755
				internal IntPtr pCertInfo;

				// Token: 0x04003D8C RID: 15756
				internal IntPtr hCertStore;
			}
		}
	}
}
