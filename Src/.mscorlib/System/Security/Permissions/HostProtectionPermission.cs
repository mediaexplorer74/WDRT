using System;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002E4 RID: 740
	[Serializable]
	internal sealed class HostProtectionPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x0600264B RID: 9803 RVA: 0x0008D1D9 File Offset: 0x0008B3D9
		public HostProtectionPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.Resources = HostProtectionResource.All;
				return;
			}
			if (state == PermissionState.None)
			{
				this.Resources = HostProtectionResource.None;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x0008D20B File Offset: 0x0008B40B
		public HostProtectionPermission(HostProtectionResource resources)
		{
			this.Resources = resources;
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x0008D21A File Offset: 0x0008B41A
		public bool IsUnrestricted()
		{
			return this.Resources == HostProtectionResource.All;
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x0008D25D File Offset: 0x0008B45D
		// (set) Token: 0x0600264E RID: 9806 RVA: 0x0008D229 File Offset: 0x0008B429
		public HostProtectionResource Resources
		{
			get
			{
				return this.m_resources;
			}
			set
			{
				if (value < HostProtectionResource.None || value > HostProtectionResource.All)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)value }));
				}
				this.m_resources = value;
			}
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x0008D268 File Offset: 0x0008B468
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_resources == HostProtectionResource.None;
			}
			if (base.GetType() != target.GetType())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			return (this.m_resources & ((HostProtectionPermission)target).m_resources) == this.m_resources;
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x0008D2D4 File Offset: 0x0008B4D4
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (base.GetType() != target.GetType())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			HostProtectionResource hostProtectionResource = this.m_resources | ((HostProtectionPermission)target).m_resources;
			return new HostProtectionPermission(hostProtectionResource);
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x0008D33C File Offset: 0x0008B53C
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (base.GetType() != target.GetType())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			HostProtectionResource hostProtectionResource = this.m_resources & ((HostProtectionPermission)target).m_resources;
			if (hostProtectionResource == HostProtectionResource.None)
			{
				return null;
			}
			return new HostProtectionPermission(hostProtectionResource);
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x0008D3A3 File Offset: 0x0008B5A3
		public override IPermission Copy()
		{
			return new HostProtectionPermission(this.m_resources);
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x0008D3B0 File Offset: 0x0008B5B0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, base.GetType().FullName);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("Resources", XMLUtil.BitFieldEnumToString(typeof(HostProtectionResource), this.Resources));
			}
			return securityElement;
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x0008D410 File Offset: 0x0008B610
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.Resources = HostProtectionResource.All;
				return;
			}
			string text = esd.Attribute("Resources");
			if (text == null)
			{
				this.Resources = HostProtectionResource.None;
				return;
			}
			this.Resources = (HostProtectionResource)Enum.Parse(typeof(HostProtectionResource), text);
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x0008D46A File Offset: 0x0008B66A
		int IBuiltInPermission.GetTokenIndex()
		{
			return HostProtectionPermission.GetTokenIndex();
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x0008D471 File Offset: 0x0008B671
		internal static int GetTokenIndex()
		{
			return 9;
		}

		// Token: 0x04000EA6 RID: 3750
		internal static volatile HostProtectionResource protectedResources;

		// Token: 0x04000EA7 RID: 3751
		private HostProtectionResource m_resources;
	}
}
