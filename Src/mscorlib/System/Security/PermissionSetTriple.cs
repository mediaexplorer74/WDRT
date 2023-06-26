using System;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x020001E3 RID: 483
	[Serializable]
	internal sealed class PermissionSetTriple
	{
		// Token: 0x06001D39 RID: 7481 RVA: 0x000659F4 File Offset: 0x00063BF4
		internal PermissionSetTriple()
		{
			this.Reset();
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x00065A02 File Offset: 0x00063C02
		internal PermissionSetTriple(PermissionSetTriple triple)
		{
			this.AssertSet = triple.AssertSet;
			this.GrantSet = triple.GrantSet;
			this.RefusedSet = triple.RefusedSet;
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x00065A2E File Offset: 0x00063C2E
		internal void Reset()
		{
			this.AssertSet = null;
			this.GrantSet = null;
			this.RefusedSet = null;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x00065A45 File Offset: 0x00063C45
		internal bool IsEmpty()
		{
			return this.AssertSet == null && this.GrantSet == null && this.RefusedSet == null;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x00065A62 File Offset: 0x00063C62
		private PermissionToken ZoneToken
		{
			[SecurityCritical]
			get
			{
				if (PermissionSetTriple.s_zoneToken == null)
				{
					PermissionSetTriple.s_zoneToken = PermissionToken.GetToken(typeof(ZoneIdentityPermission));
				}
				return PermissionSetTriple.s_zoneToken;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x00065A8A File Offset: 0x00063C8A
		private PermissionToken UrlToken
		{
			[SecurityCritical]
			get
			{
				if (PermissionSetTriple.s_urlToken == null)
				{
					PermissionSetTriple.s_urlToken = PermissionToken.GetToken(typeof(UrlIdentityPermission));
				}
				return PermissionSetTriple.s_urlToken;
			}
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x00065AB4 File Offset: 0x00063CB4
		[SecurityCritical]
		internal bool Update(PermissionSetTriple psTriple, out PermissionSetTriple retTriple)
		{
			retTriple = null;
			retTriple = this.UpdateAssert(psTriple.AssertSet);
			if (psTriple.AssertSet != null && psTriple.AssertSet.IsUnrestricted())
			{
				return true;
			}
			this.UpdateGrant(psTriple.GrantSet);
			this.UpdateRefused(psTriple.RefusedSet);
			return false;
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x00065B04 File Offset: 0x00063D04
		[SecurityCritical]
		internal PermissionSetTriple UpdateAssert(PermissionSet in_a)
		{
			PermissionSetTriple permissionSetTriple = null;
			if (in_a != null)
			{
				if (in_a.IsSubsetOf(this.AssertSet))
				{
					return null;
				}
				PermissionSet permissionSet;
				if (this.GrantSet != null)
				{
					permissionSet = in_a.Intersect(this.GrantSet);
				}
				else
				{
					this.GrantSet = new PermissionSet(true);
					permissionSet = in_a.Copy();
				}
				bool flag = false;
				if (this.RefusedSet != null)
				{
					permissionSet = PermissionSet.RemoveRefusedPermissionSet(permissionSet, this.RefusedSet, out flag);
				}
				if (!flag)
				{
					flag = PermissionSet.IsIntersectingAssertedPermissions(permissionSet, this.AssertSet);
				}
				if (flag)
				{
					permissionSetTriple = new PermissionSetTriple(this);
					this.Reset();
					this.GrantSet = permissionSetTriple.GrantSet.Copy();
				}
				if (this.AssertSet == null)
				{
					this.AssertSet = permissionSet;
				}
				else
				{
					this.AssertSet.InplaceUnion(permissionSet);
				}
			}
			return permissionSetTriple;
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x00065BBC File Offset: 0x00063DBC
		[SecurityCritical]
		internal void UpdateGrant(PermissionSet in_g, out ZoneIdentityPermission z, out UrlIdentityPermission u)
		{
			z = null;
			u = null;
			if (in_g != null)
			{
				if (this.GrantSet == null)
				{
					this.GrantSet = in_g.Copy();
				}
				else
				{
					this.GrantSet.InplaceIntersect(in_g);
				}
				z = (ZoneIdentityPermission)in_g.GetPermission(this.ZoneToken);
				u = (UrlIdentityPermission)in_g.GetPermission(this.UrlToken);
			}
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x00065C1A File Offset: 0x00063E1A
		[SecurityCritical]
		internal void UpdateGrant(PermissionSet in_g)
		{
			if (in_g != null)
			{
				if (this.GrantSet == null)
				{
					this.GrantSet = in_g.Copy();
					return;
				}
				this.GrantSet.InplaceIntersect(in_g);
			}
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x00065C40 File Offset: 0x00063E40
		internal void UpdateRefused(PermissionSet in_r)
		{
			if (in_r != null)
			{
				if (this.RefusedSet == null)
				{
					this.RefusedSet = in_r.Copy();
					return;
				}
				this.RefusedSet.InplaceUnion(in_r);
			}
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x00065C68 File Offset: 0x00063E68
		[SecurityCritical]
		private static bool CheckAssert(PermissionSet pSet, CodeAccessPermission demand, PermissionToken permToken)
		{
			if (pSet != null)
			{
				pSet.CheckDecoded(demand, permToken);
				CodeAccessPermission codeAccessPermission = (CodeAccessPermission)pSet.GetPermission(demand);
				try
				{
					if (pSet.IsUnrestricted() || demand.CheckAssert(codeAccessPermission))
					{
						return false;
					}
				}
				catch (ArgumentException)
				{
				}
				return true;
			}
			return true;
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x00065CBC File Offset: 0x00063EBC
		[SecurityCritical]
		private static bool CheckAssert(PermissionSet assertPset, PermissionSet demandSet, out PermissionSet newDemandSet)
		{
			newDemandSet = null;
			if (assertPset != null)
			{
				assertPset.CheckDecoded(demandSet);
				if (demandSet.CheckAssertion(assertPset))
				{
					return false;
				}
				PermissionSet.RemoveAssertedPermissionSet(demandSet, assertPset, out newDemandSet);
			}
			return true;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x00065CDF File Offset: 0x00063EDF
		[SecurityCritical]
		internal bool CheckDemand(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
		{
			if (!PermissionSetTriple.CheckAssert(this.AssertSet, demand, permToken))
			{
				return false;
			}
			CodeAccessSecurityEngine.CheckHelper(this.GrantSet, this.RefusedSet, demand, permToken, rmh, null, SecurityAction.Demand, true);
			return true;
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00065D0B File Offset: 0x00063F0B
		[SecurityCritical]
		internal bool CheckSetDemand(PermissionSet demandSet, out PermissionSet alteredDemandset, RuntimeMethodHandleInternal rmh)
		{
			alteredDemandset = null;
			if (!PermissionSetTriple.CheckAssert(this.AssertSet, demandSet, out alteredDemandset))
			{
				return false;
			}
			if (alteredDemandset != null)
			{
				demandSet = alteredDemandset;
			}
			CodeAccessSecurityEngine.CheckSetHelper(this.GrantSet, this.RefusedSet, demandSet, rmh, null, SecurityAction.Demand, true);
			return true;
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x00065D41 File Offset: 0x00063F41
		[SecurityCritical]
		internal bool CheckDemandNoThrow(CodeAccessPermission demand, PermissionToken permToken)
		{
			return CodeAccessSecurityEngine.CheckHelper(this.GrantSet, this.RefusedSet, demand, permToken, RuntimeMethodHandleInternal.EmptyHandle, null, SecurityAction.Demand, false);
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x00065D5E File Offset: 0x00063F5E
		[SecurityCritical]
		internal bool CheckSetDemandNoThrow(PermissionSet demandSet)
		{
			return CodeAccessSecurityEngine.CheckSetHelper(this.GrantSet, this.RefusedSet, demandSet, RuntimeMethodHandleInternal.EmptyHandle, null, SecurityAction.Demand, false);
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x00065D7C File Offset: 0x00063F7C
		[SecurityCritical]
		internal bool CheckFlags(ref int flags)
		{
			if (this.AssertSet != null)
			{
				int specialFlags = SecurityManager.GetSpecialFlags(this.AssertSet, null);
				if ((flags & specialFlags) != 0)
				{
					flags &= ~specialFlags;
				}
			}
			return (SecurityManager.GetSpecialFlags(this.GrantSet, this.RefusedSet) & flags) == flags;
		}

		// Token: 0x04000A3B RID: 2619
		private static volatile PermissionToken s_zoneToken;

		// Token: 0x04000A3C RID: 2620
		private static volatile PermissionToken s_urlToken;

		// Token: 0x04000A3D RID: 2621
		internal PermissionSet AssertSet;

		// Token: 0x04000A3E RID: 2622
		internal PermissionSet GrantSet;

		// Token: 0x04000A3F RID: 2623
		internal PermissionSet RefusedSet;
	}
}
