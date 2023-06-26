using System;
using System.Security.Policy;

namespace System.Security.Permissions
{
	// Token: 0x02000307 RID: 775
	[Serializable]
	internal sealed class StrongName2
	{
		// Token: 0x0600276D RID: 10093 RVA: 0x00090289 File Offset: 0x0008E489
		public StrongName2(StrongNamePublicKeyBlob publicKeyBlob, string name, Version version)
		{
			this.m_publicKeyBlob = publicKeyBlob;
			this.m_name = name;
			this.m_version = version;
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000902A6 File Offset: 0x0008E4A6
		public StrongName2 Copy()
		{
			return new StrongName2(this.m_publicKeyBlob, this.m_name, this.m_version);
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x000902C0 File Offset: 0x0008E4C0
		public bool IsSubsetOf(StrongName2 target)
		{
			return this.m_publicKeyBlob == null || (this.m_publicKeyBlob.Equals(target.m_publicKeyBlob) && (this.m_name == null || (target.m_name != null && StrongName.CompareNames(target.m_name, this.m_name))) && (this.m_version == null || (target.m_version != null && target.m_version.CompareTo(this.m_version) == 0)));
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x00090337 File Offset: 0x0008E537
		public StrongName2 Intersect(StrongName2 target)
		{
			if (target.IsSubsetOf(this))
			{
				return target.Copy();
			}
			if (this.IsSubsetOf(target))
			{
				return this.Copy();
			}
			return null;
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x0009035A File Offset: 0x0008E55A
		public bool Equals(StrongName2 target)
		{
			return target.IsSubsetOf(this) && this.IsSubsetOf(target);
		}

		// Token: 0x04000F4C RID: 3916
		public StrongNamePublicKeyBlob m_publicKeyBlob;

		// Token: 0x04000F4D RID: 3917
		public string m_name;

		// Token: 0x04000F4E RID: 3918
		public Version m_version;
	}
}
