using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Defines evidence that represents permission requests. This class cannot be inherited.</summary>
	// Token: 0x02000360 RID: 864
	[ComVisible(true)]
	[Obsolete("Assembly level declarative security is obsolete and is no longer enforced by the CLR by default. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
	[Serializable]
	public sealed class PermissionRequestEvidence : EvidenceBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PermissionRequestEvidence" /> class with the permission request of a code assembly.</summary>
		/// <param name="request">The minimum permissions the code requires to run.</param>
		/// <param name="optional">The permissions the code can use if they are granted, but that are not required.</param>
		/// <param name="denied">The permissions the code explicitly asks not to be granted.</param>
		// Token: 0x06002AE2 RID: 10978 RVA: 0x0009FF0C File Offset: 0x0009E10C
		public PermissionRequestEvidence(PermissionSet request, PermissionSet optional, PermissionSet denied)
		{
			if (request == null)
			{
				this.m_request = null;
			}
			else
			{
				this.m_request = request.Copy();
			}
			if (optional == null)
			{
				this.m_optional = null;
			}
			else
			{
				this.m_optional = optional.Copy();
			}
			if (denied == null)
			{
				this.m_denied = null;
				return;
			}
			this.m_denied = denied.Copy();
		}

		/// <summary>Gets the minimum permissions the code requires to run.</summary>
		/// <returns>The minimum permissions the code requires to run.</returns>
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x0009FF66 File Offset: 0x0009E166
		public PermissionSet RequestedPermissions
		{
			get
			{
				return this.m_request;
			}
		}

		/// <summary>Gets the permissions the code can use if they are granted, but are not required.</summary>
		/// <returns>The permissions the code can use if they are granted, but are not required.</returns>
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x0009FF6E File Offset: 0x0009E16E
		public PermissionSet OptionalPermissions
		{
			get
			{
				return this.m_optional;
			}
		}

		/// <summary>Gets the permissions the code explicitly asks not to be granted.</summary>
		/// <returns>The permissions the code explicitly asks not to be granted.</returns>
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x0009FF76 File Offset: 0x0009E176
		public PermissionSet DeniedPermissions
		{
			get
			{
				return this.m_denied;
			}
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x06002AE6 RID: 10982 RVA: 0x0009FF7E File Offset: 0x0009E17E
		public override EvidenceBase Clone()
		{
			return this.Copy();
		}

		/// <summary>Creates an equivalent copy of the current <see cref="T:System.Security.Policy.PermissionRequestEvidence" />.</summary>
		/// <returns>An equivalent copy of the current <see cref="T:System.Security.Policy.PermissionRequestEvidence" />.</returns>
		// Token: 0x06002AE7 RID: 10983 RVA: 0x0009FF86 File Offset: 0x0009E186
		public PermissionRequestEvidence Copy()
		{
			return new PermissionRequestEvidence(this.m_request, this.m_optional, this.m_denied);
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x0009FFA0 File Offset: 0x0009E1A0
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.PermissionRequestEvidence");
			securityElement.AddAttribute("version", "1");
			if (this.m_request != null)
			{
				SecurityElement securityElement2 = new SecurityElement("Request");
				securityElement2.AddChild(this.m_request.ToXml());
				securityElement.AddChild(securityElement2);
			}
			if (this.m_optional != null)
			{
				SecurityElement securityElement2 = new SecurityElement("Optional");
				securityElement2.AddChild(this.m_optional.ToXml());
				securityElement.AddChild(securityElement2);
			}
			if (this.m_denied != null)
			{
				SecurityElement securityElement2 = new SecurityElement("Denied");
				securityElement2.AddChild(this.m_denied.ToXml());
				securityElement.AddChild(securityElement2);
			}
			return securityElement;
		}

		/// <summary>Gets a string representation of the state of the <see cref="T:System.Security.Policy.PermissionRequestEvidence" />.</summary>
		/// <returns>A representation of the state of the <see cref="T:System.Security.Policy.PermissionRequestEvidence" />.</returns>
		// Token: 0x06002AE9 RID: 10985 RVA: 0x000A004A File Offset: 0x0009E24A
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x04001177 RID: 4471
		private PermissionSet m_request;

		// Token: 0x04001178 RID: 4472
		private PermissionSet m_optional;

		// Token: 0x04001179 RID: 4473
		private PermissionSet m_denied;

		// Token: 0x0400117A RID: 4474
		private string m_strRequest;

		// Token: 0x0400117B RID: 4475
		private string m_strOptional;

		// Token: 0x0400117C RID: 4476
		private string m_strDenied;
	}
}
