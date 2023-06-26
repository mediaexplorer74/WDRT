using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Util;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020002FC RID: 764
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class PublisherIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PublisherIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002707 RID: 9991 RVA: 0x0008E6C8 File Offset: 0x0008C8C8
		public PublisherIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.m_x509cert = null;
			this.m_certFile = null;
			this.m_signedFile = null;
		}

		/// <summary>Gets or sets an Authenticode X.509v3 certificate that identifies the publisher of the calling code.</summary>
		/// <returns>A hexadecimal representation of the X.509 certificate.</returns>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x0008E6E6 File Offset: 0x0008C8E6
		// (set) Token: 0x06002709 RID: 9993 RVA: 0x0008E6EE File Offset: 0x0008C8EE
		public string X509Certificate
		{
			get
			{
				return this.m_x509cert;
			}
			set
			{
				this.m_x509cert = value;
			}
		}

		/// <summary>Gets or sets a certification file containing an Authenticode X.509v3 certificate.</summary>
		/// <returns>The file path of an X.509 certificate file (usually has the extension.cer).</returns>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x0008E6F7 File Offset: 0x0008C8F7
		// (set) Token: 0x0600270B RID: 9995 RVA: 0x0008E6FF File Offset: 0x0008C8FF
		public string CertFile
		{
			get
			{
				return this.m_certFile;
			}
			set
			{
				this.m_certFile = value;
			}
		}

		/// <summary>Gets or sets a signed file from which to extract an Authenticode X.509v3 certificate.</summary>
		/// <returns>The file path of a file signed with the Authenticode signature.</returns>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x0008E708 File Offset: 0x0008C908
		// (set) Token: 0x0600270D RID: 9997 RVA: 0x0008E710 File Offset: 0x0008C910
		public string SignedFile
		{
			get
			{
				return this.m_signedFile;
			}
			set
			{
				this.m_signedFile = value;
			}
		}

		/// <summary>Creates and returns a new instance of <see cref="T:System.Security.Permissions.PublisherIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x0600270E RID: 9998 RVA: 0x0008E71C File Offset: 0x0008C91C
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new PublisherIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.m_x509cert != null)
			{
				return new PublisherIdentityPermission(new X509Certificate(Hex.DecodeHexString(this.m_x509cert)));
			}
			if (this.m_certFile != null)
			{
				return new PublisherIdentityPermission(System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(this.m_certFile));
			}
			if (this.m_signedFile != null)
			{
				return new PublisherIdentityPermission(System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromSignedFile(this.m_signedFile));
			}
			return new PublisherIdentityPermission(PermissionState.None);
		}

		// Token: 0x04000F13 RID: 3859
		private string m_x509cert;

		// Token: 0x04000F14 RID: 3860
		private string m_certFile;

		// Token: 0x04000F15 RID: 3861
		private string m_signedFile;
	}
}
