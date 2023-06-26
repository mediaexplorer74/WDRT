using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a chain-building engine for <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> certificates.</summary>
	// Token: 0x0200046C RID: 1132
	public class X509Chain : IDisposable
	{
		/// <summary>Creates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object after querying for the mapping defined in the CryptoConfig file, and maps the chain to that mapping.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</returns>
		// Token: 0x06002A15 RID: 10773 RVA: 0x000C0169 File Offset: 0x000BE369
		public static X509Chain Create()
		{
			return (X509Chain)CryptoConfig.CreateFromName("X509Chain");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class.</summary>
		// Token: 0x06002A16 RID: 10774 RVA: 0x000C017A File Offset: 0x000BE37A
		[SecurityCritical]
		public X509Chain()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class specifying a value that indicates whether the machine context should be used.</summary>
		/// <param name="useMachineContext">
		///   <see langword="true" /> to use the machine context; <see langword="false" /> to use the current user context.</param>
		// Token: 0x06002A17 RID: 10775 RVA: 0x000C0184 File Offset: 0x000BE384
		[SecurityCritical]
		public X509Chain(bool useMachineContext)
		{
			this.m_syncRoot = new object();
			base..ctor();
			this.m_status = 0U;
			this.m_chainPolicy = null;
			this.m_chainStatus = null;
			this.m_chainElementCollection = new X509ChainElementCollection();
			this.m_safeCertChainHandle = SafeX509ChainHandle.InvalidHandle;
			this.m_useMachineContext = useMachineContext;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class using an <see cref="T:System.IntPtr" /> handle to an X.509 chain.</summary>
		/// <param name="chainContext">An <see cref="T:System.IntPtr" /> handle to an X.509 chain.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="chainContext" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="chainContext" /> parameter points to an invalid context.</exception>
		// Token: 0x06002A18 RID: 10776 RVA: 0x000C01D4 File Offset: 0x000BE3D4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public X509Chain(IntPtr chainContext)
		{
			this.m_syncRoot = new object();
			base..ctor();
			if (chainContext == IntPtr.Zero)
			{
				throw new ArgumentNullException("chainContext");
			}
			this.m_safeCertChainHandle = CAPISafe.CertDuplicateCertificateChain(chainContext);
			if (this.m_safeCertChainHandle == null || this.m_safeCertChainHandle == SafeX509ChainHandle.InvalidHandle)
			{
				throw new CryptographicException(SR.GetString("Cryptography_InvalidContextHandle"), "chainContext");
			}
			this.Init();
		}

		/// <summary>Gets a handle to an X.509 chain.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> handle to an X.509 chain.</returns>
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x000C0246 File Offset: 0x000BE446
		public IntPtr ChainContext
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.m_safeCertChainHandle.DangerousGetHandle();
			}
		}

		/// <summary>Gets a safe handle for this <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> instance.</summary>
		/// <returns>The safe handle for this <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> instance.</returns>
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06002A1A RID: 10778 RVA: 0x000C0253 File Offset: 0x000BE453
		public SafeX509ChainHandle SafeHandle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this.m_safeCertChainHandle;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> to use when building an X.509 certificate chain.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> object associated with this X.509 chain.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value being set for this property is <see langword="null" />.</exception>
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x000C025B File Offset: 0x000BE45B
		// (set) Token: 0x06002A1C RID: 10780 RVA: 0x000C0276 File Offset: 0x000BE476
		public X509ChainPolicy ChainPolicy
		{
			get
			{
				if (this.m_chainPolicy == null)
				{
					this.m_chainPolicy = new X509ChainPolicy();
				}
				return this.m_chainPolicy;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_chainPolicy = value;
			}
		}

		/// <summary>Gets the status of each element in an <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</summary>
		/// <returns>An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatus" /> objects.</returns>
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x000C028D File Offset: 0x000BE48D
		public X509ChainStatus[] ChainStatus
		{
			get
			{
				if (this.m_chainStatus == null)
				{
					if (this.m_status == 0U)
					{
						this.m_chainStatus = new X509ChainStatus[0];
					}
					else
					{
						this.m_chainStatus = X509Chain.GetChainStatusInformation(this.m_status);
					}
				}
				return this.m_chainStatus;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> objects.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object.</returns>
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06002A1E RID: 10782 RVA: 0x000C02C4 File Offset: 0x000BE4C4
		public X509ChainElementCollection ChainElements
		{
			get
			{
				return this.m_chainElementCollection;
			}
		}

		/// <summary>Builds an X.509 chain using the policy specified in <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" />.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the X.509 certificate is valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="certificate" /> is not a valid certificate or is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificate" /> is unreadable.</exception>
		// Token: 0x06002A1F RID: 10783 RVA: 0x000C02CC File Offset: 0x000BE4CC
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public bool Build(X509Certificate2 certificate)
		{
			object syncRoot = this.m_syncRoot;
			bool flag2;
			lock (syncRoot)
			{
				if (certificate == null || certificate.CertContext.IsInvalid)
				{
					throw new ArgumentException(SR.GetString("Cryptography_InvalidContextHandle"), "certificate");
				}
				StorePermission storePermission = new StorePermission(StorePermissionFlags.OpenStore | StorePermissionFlags.EnumerateCertificates);
				storePermission.Demand();
				X509ChainPolicy chainPolicy = this.ChainPolicy;
				if (chainPolicy.RevocationMode == X509RevocationMode.Online && (certificate.Extensions["2.5.29.31"] != null || certificate.Extensions["1.3.6.1.5.5.7.1.1"] != null))
				{
					PermissionSet permissionSet = new PermissionSet(PermissionState.None);
					permissionSet.AddPermission(new WebPermission(PermissionState.Unrestricted));
					permissionSet.AddPermission(new StorePermission(StorePermissionFlags.AddToStore));
					permissionSet.Demand();
				}
				this.Reset();
				int num = X509Chain.BuildChain(this.m_useMachineContext ? new IntPtr(1L) : new IntPtr(0L), certificate.CertContext, chainPolicy.ExtraStore, chainPolicy.ApplicationPolicy, chainPolicy.CertificatePolicy, chainPolicy.RevocationMode, chainPolicy.RevocationFlag, chainPolicy.VerificationTime, chainPolicy.UrlRetrievalTimeout, ref this.m_safeCertChainHandle);
				if (num != 0)
				{
					flag2 = false;
				}
				else
				{
					this.Init();
					CAPIBase.CERT_CHAIN_POLICY_PARA cert_CHAIN_POLICY_PARA = new CAPIBase.CERT_CHAIN_POLICY_PARA(Marshal.SizeOf(typeof(CAPIBase.CERT_CHAIN_POLICY_PARA)));
					CAPIBase.CERT_CHAIN_POLICY_STATUS cert_CHAIN_POLICY_STATUS = new CAPIBase.CERT_CHAIN_POLICY_STATUS(Marshal.SizeOf(typeof(CAPIBase.CERT_CHAIN_POLICY_STATUS)));
					cert_CHAIN_POLICY_PARA.dwFlags = (uint)chainPolicy.VerificationFlags;
					if (!CAPISafe.CertVerifyCertificateChainPolicy(new IntPtr(1L), this.m_safeCertChainHandle, ref cert_CHAIN_POLICY_PARA, ref cert_CHAIN_POLICY_STATUS))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					CAPISafe.SetLastError(cert_CHAIN_POLICY_STATUS.dwError);
					flag2 = cert_CHAIN_POLICY_STATUS.dwError == 0U;
				}
			}
			return flag2;
		}

		/// <summary>Clears the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</summary>
		// Token: 0x06002A20 RID: 10784 RVA: 0x000C048C File Offset: 0x000BE68C
		[SecurityCritical]
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public void Reset()
		{
			this.m_status = 0U;
			this.m_chainStatus = null;
			this.m_chainElementCollection = new X509ChainElementCollection();
			if (!this.m_safeCertChainHandle.IsInvalid)
			{
				this.m_safeCertChainHandle.Dispose();
				this.m_safeCertChainHandle = SafeX509ChainHandle.InvalidHandle;
			}
		}

		/// <summary>Releases all of the resources used by this <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" />.</summary>
		// Token: 0x06002A21 RID: 10785 RVA: 0x000C04CA File Offset: 0x000BE6CA
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by this <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002A22 RID: 10786 RVA: 0x000C04D9 File Offset: 0x000BE6D9
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Reset();
			}
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x000C04E4 File Offset: 0x000BE6E4
		[SecurityCritical]
		private unsafe void Init()
		{
			using (SafeX509ChainHandle safeX509ChainHandle = CAPISafe.CertDuplicateCertificateChain(this.m_safeCertChainHandle))
			{
				CAPIBase.CERT_CHAIN_CONTEXT cert_CHAIN_CONTEXT = new CAPIBase.CERT_CHAIN_CONTEXT(Marshal.SizeOf(typeof(CAPIBase.CERT_CHAIN_CONTEXT)));
				uint num = (uint)Marshal.ReadInt32(safeX509ChainHandle.DangerousGetHandle());
				if ((ulong)num > (ulong)((long)Marshal.SizeOf(cert_CHAIN_CONTEXT)))
				{
					num = (uint)Marshal.SizeOf(cert_CHAIN_CONTEXT);
				}
				X509Utils.memcpy(this.m_safeCertChainHandle.DangerousGetHandle(), new IntPtr((void*)(&cert_CHAIN_CONTEXT)), num);
				this.m_status = cert_CHAIN_CONTEXT.dwErrorStatus;
				this.m_chainElementCollection = new X509ChainElementCollection(Marshal.ReadIntPtr(cert_CHAIN_CONTEXT.rgpChain));
			}
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x000C0594 File Offset: 0x000BE794
		internal static X509ChainStatus[] GetChainStatusInformation(uint dwStatus)
		{
			if (dwStatus == 0U)
			{
				return new X509ChainStatus[0];
			}
			int num = 0;
			for (uint num2 = dwStatus; num2 != 0U; num2 >>= 1)
			{
				if ((num2 & 1U) != 0U)
				{
					num++;
				}
			}
			X509ChainStatus[] array = new X509ChainStatus[num];
			int num3 = 0;
			foreach (X509Chain.X509ChainErrorMapping x509ChainErrorMapping in X509Chain.s_x509ChainErrorMappings)
			{
				if ((dwStatus & x509ChainErrorMapping.Win32Flag) != 0U)
				{
					array[num3].StatusInformation = X509Utils.GetSystemErrorString(x509ChainErrorMapping.Win32ErrorCode);
					array[num3].Status = x509ChainErrorMapping.ChainStatusFlag;
					num3++;
					dwStatus &= ~x509ChainErrorMapping.Win32Flag;
				}
			}
			int num4 = 0;
			for (uint num5 = dwStatus; num5 != 0U; num5 >>= 1)
			{
				if ((num5 & 1U) != 0U)
				{
					array[num3].Status = (X509ChainStatusFlags)(1 << num4);
					array[num3].StatusInformation = SR.GetString("Unknown_Error");
					num3++;
				}
				num4++;
			}
			return array;
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x000C0684 File Offset: 0x000BE884
		[SecurityCritical]
		internal unsafe static int BuildChain(IntPtr hChainEngine, SafeCertContextHandle pCertContext, X509Certificate2Collection extraStore, OidCollection applicationPolicy, OidCollection certificatePolicy, X509RevocationMode revocationMode, X509RevocationFlag revocationFlag, DateTime verificationTime, TimeSpan timeout, ref SafeX509ChainHandle ppChainContext)
		{
			if (pCertContext == null || pCertContext.IsInvalid)
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidContextHandle"), "pCertContext");
			}
			SafeCertStoreHandle safeCertStoreHandle = SafeCertStoreHandle.InvalidHandle;
			if (extraStore != null && extraStore.Count > 0)
			{
				safeCertStoreHandle = X509Utils.ExportToMemoryStore(extraStore);
			}
			CAPIBase.CERT_CHAIN_PARA cert_CHAIN_PARA = default(CAPIBase.CERT_CHAIN_PARA);
			cert_CHAIN_PARA.cbSize = (uint)Marshal.SizeOf(cert_CHAIN_PARA);
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			SafeLocalAllocHandle safeLocalAllocHandle2 = SafeLocalAllocHandle.InvalidHandle;
			try
			{
				if (applicationPolicy != null && applicationPolicy.Count > 0)
				{
					cert_CHAIN_PARA.RequestedUsage.dwType = 0U;
					cert_CHAIN_PARA.RequestedUsage.Usage.cUsageIdentifier = (uint)applicationPolicy.Count;
					safeLocalAllocHandle = X509Utils.CopyOidsToUnmanagedMemory(applicationPolicy);
					cert_CHAIN_PARA.RequestedUsage.Usage.rgpszUsageIdentifier = safeLocalAllocHandle.DangerousGetHandle();
				}
				if (certificatePolicy != null && certificatePolicy.Count > 0)
				{
					cert_CHAIN_PARA.RequestedIssuancePolicy.dwType = 0U;
					cert_CHAIN_PARA.RequestedIssuancePolicy.Usage.cUsageIdentifier = (uint)certificatePolicy.Count;
					safeLocalAllocHandle2 = X509Utils.CopyOidsToUnmanagedMemory(certificatePolicy);
					cert_CHAIN_PARA.RequestedIssuancePolicy.Usage.rgpszUsageIdentifier = safeLocalAllocHandle2.DangerousGetHandle();
				}
				cert_CHAIN_PARA.dwUrlRetrievalTimeout = (uint)Math.Floor(timeout.TotalMilliseconds);
				System.Runtime.InteropServices.ComTypes.FILETIME filetime = default(System.Runtime.InteropServices.ComTypes.FILETIME);
				*(long*)(&filetime) = verificationTime.ToFileTime();
				uint num = X509Utils.MapRevocationFlags(revocationMode, revocationFlag);
				if (!CAPISafe.CertGetCertificateChain(hChainEngine, pCertContext, ref filetime, safeCertStoreHandle, ref cert_CHAIN_PARA, num, IntPtr.Zero, ref ppChainContext))
				{
					return Marshal.GetHRForLastWin32Error();
				}
			}
			finally
			{
				safeLocalAllocHandle.Dispose();
				safeLocalAllocHandle2.Dispose();
			}
			return 0;
		}

		// Token: 0x040025E5 RID: 9701
		private uint m_status;

		// Token: 0x040025E6 RID: 9702
		private X509ChainPolicy m_chainPolicy;

		// Token: 0x040025E7 RID: 9703
		private X509ChainStatus[] m_chainStatus;

		// Token: 0x040025E8 RID: 9704
		private X509ChainElementCollection m_chainElementCollection;

		// Token: 0x040025E9 RID: 9705
		[SecurityCritical]
		private SafeX509ChainHandle m_safeCertChainHandle;

		// Token: 0x040025EA RID: 9706
		private bool m_useMachineContext;

		// Token: 0x040025EB RID: 9707
		private readonly object m_syncRoot;

		// Token: 0x040025EC RID: 9708
		private static readonly X509Chain.X509ChainErrorMapping[] s_x509ChainErrorMappings = new X509Chain.X509ChainErrorMapping[]
		{
			new X509Chain.X509ChainErrorMapping(8U, -2146869244, X509ChainStatusFlags.NotSignatureValid),
			new X509Chain.X509ChainErrorMapping(262144U, -2146869244, X509ChainStatusFlags.CtlNotSignatureValid),
			new X509Chain.X509ChainErrorMapping(32U, -2146762487, X509ChainStatusFlags.UntrustedRoot),
			new X509Chain.X509ChainErrorMapping(65536U, -2146762486, X509ChainStatusFlags.PartialChain),
			new X509Chain.X509ChainErrorMapping(4U, -2146885616, X509ChainStatusFlags.Revoked),
			new X509Chain.X509ChainErrorMapping(16U, -2146762480, X509ChainStatusFlags.NotValidForUsage),
			new X509Chain.X509ChainErrorMapping(524288U, -2146762480, X509ChainStatusFlags.CtlNotValidForUsage),
			new X509Chain.X509ChainErrorMapping(1U, -2146762495, X509ChainStatusFlags.NotTimeValid),
			new X509Chain.X509ChainErrorMapping(131072U, -2146762495, X509ChainStatusFlags.CtlNotTimeValid),
			new X509Chain.X509ChainErrorMapping(2048U, -2146762476, X509ChainStatusFlags.InvalidNameConstraints),
			new X509Chain.X509ChainErrorMapping(4096U, -2146762476, X509ChainStatusFlags.HasNotSupportedNameConstraint),
			new X509Chain.X509ChainErrorMapping(8192U, -2146762476, X509ChainStatusFlags.HasNotDefinedNameConstraint),
			new X509Chain.X509ChainErrorMapping(16384U, -2146762476, X509ChainStatusFlags.HasNotPermittedNameConstraint),
			new X509Chain.X509ChainErrorMapping(32768U, -2146762476, X509ChainStatusFlags.HasExcludedNameConstraint),
			new X509Chain.X509ChainErrorMapping(512U, -2146762477, X509ChainStatusFlags.InvalidPolicyConstraints),
			new X509Chain.X509ChainErrorMapping(33554432U, -2146762477, X509ChainStatusFlags.NoIssuanceChainPolicy),
			new X509Chain.X509ChainErrorMapping(1024U, -2146869223, X509ChainStatusFlags.InvalidBasicConstraints),
			new X509Chain.X509ChainErrorMapping(2U, -2146762494, X509ChainStatusFlags.NotTimeNested),
			new X509Chain.X509ChainErrorMapping(64U, -2146885614, X509ChainStatusFlags.RevocationStatusUnknown),
			new X509Chain.X509ChainErrorMapping(16777216U, -2146885613, X509ChainStatusFlags.OfflineRevocation),
			new X509Chain.X509ChainErrorMapping(67108864U, -2146762479, X509ChainStatusFlags.ExplicitDistrust),
			new X509Chain.X509ChainErrorMapping(134217728U, -2146762491, X509ChainStatusFlags.HasNotSupportedCriticalExtension),
			new X509Chain.X509ChainErrorMapping(1048576U, -2146877418, X509ChainStatusFlags.HasWeakSignature)
		};

		// Token: 0x02000877 RID: 2167
		private struct X509ChainErrorMapping
		{
			// Token: 0x06004547 RID: 17735 RVA: 0x00120CF6 File Offset: 0x0011EEF6
			public X509ChainErrorMapping(uint win32Flag, int win32ErrorCode, X509ChainStatusFlags chainStatusFlag)
			{
				this.Win32Flag = win32Flag;
				this.Win32ErrorCode = win32ErrorCode;
				this.ChainStatusFlag = chainStatusFlag;
			}

			// Token: 0x04003707 RID: 14087
			public readonly uint Win32Flag;

			// Token: 0x04003708 RID: 14088
			public readonly int Win32ErrorCode;

			// Token: 0x04003709 RID: 14089
			public readonly X509ChainStatusFlags ChainStatusFlag;
		}
	}
}
