using System;
using System.Security.Principal;

namespace System.Security.Permissions
{
	// Token: 0x02000302 RID: 770
	[Serializable]
	internal class IDRole
	{
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x0008EC94 File Offset: 0x0008CE94
		internal SecurityIdentifier Sid
		{
			[SecurityCritical]
			get
			{
				if (string.IsNullOrEmpty(this.m_role))
				{
					return null;
				}
				if (this.m_sid == null)
				{
					NTAccount ntaccount = new NTAccount(this.m_role);
					IdentityReferenceCollection identityReferenceCollection = NTAccount.Translate(new IdentityReferenceCollection(1) { ntaccount }, typeof(SecurityIdentifier), false);
					this.m_sid = identityReferenceCollection[0] as SecurityIdentifier;
				}
				return this.m_sid;
			}
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x0008ED04 File Offset: 0x0008CF04
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("Identity");
			if (this.m_authenticated)
			{
				securityElement.AddAttribute("Authenticated", "true");
			}
			if (this.m_id != null)
			{
				securityElement.AddAttribute("ID", SecurityElement.Escape(this.m_id));
			}
			if (this.m_role != null)
			{
				securityElement.AddAttribute("Role", SecurityElement.Escape(this.m_role));
			}
			return securityElement;
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x0008ED74 File Offset: 0x0008CF74
		internal void FromXml(SecurityElement e)
		{
			string text = e.Attribute("Authenticated");
			if (text != null)
			{
				this.m_authenticated = string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0;
			}
			else
			{
				this.m_authenticated = false;
			}
			string text2 = e.Attribute("ID");
			if (text2 != null)
			{
				this.m_id = text2;
			}
			else
			{
				this.m_id = null;
			}
			string text3 = e.Attribute("Role");
			if (text3 != null)
			{
				this.m_role = text3;
				return;
			}
			this.m_role = null;
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x0008EDEB File Offset: 0x0008CFEB
		public override int GetHashCode()
		{
			return (this.m_authenticated ? 0 : 101) + ((this.m_id == null) ? 0 : this.m_id.GetHashCode()) + ((this.m_role == null) ? 0 : this.m_role.GetHashCode());
		}

		// Token: 0x04000F26 RID: 3878
		internal bool m_authenticated;

		// Token: 0x04000F27 RID: 3879
		internal string m_id;

		// Token: 0x04000F28 RID: 3880
		internal string m_role;

		// Token: 0x04000F29 RID: 3881
		[NonSerialized]
		private SecurityIdentifier m_sid;
	}
}
