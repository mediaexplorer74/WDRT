using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an access control list (ACL) and is the base class for the <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> and <see cref="T:System.Security.AccessControl.SystemAcl" /> classes.</summary>
	// Token: 0x0200020D RID: 525
	public abstract class CommonAcl : GenericAcl
	{
		// Token: 0x06001EA4 RID: 7844 RVA: 0x0006B1EC File Offset: 0x000693EC
		static CommonAcl()
		{
			for (int i = 0; i < CommonAcl.AFtoPM.Length; i++)
			{
				CommonAcl.AFtoPM[i] = CommonAcl.PM.GO;
			}
			CommonAcl.AFtoPM[0] = CommonAcl.PM.F;
			CommonAcl.AFtoPM[4] = CommonAcl.PM.F | CommonAcl.PM.CO | CommonAcl.PM.GO;
			CommonAcl.AFtoPM[5] = CommonAcl.PM.F | CommonAcl.PM.CO;
			CommonAcl.AFtoPM[6] = CommonAcl.PM.CO | CommonAcl.PM.GO;
			CommonAcl.AFtoPM[7] = CommonAcl.PM.CO;
			CommonAcl.AFtoPM[8] = CommonAcl.PM.F | CommonAcl.PM.CF | CommonAcl.PM.GF;
			CommonAcl.AFtoPM[9] = CommonAcl.PM.F | CommonAcl.PM.CF;
			CommonAcl.AFtoPM[10] = CommonAcl.PM.CF | CommonAcl.PM.GF;
			CommonAcl.AFtoPM[11] = CommonAcl.PM.CF;
			CommonAcl.AFtoPM[12] = CommonAcl.PM.F | CommonAcl.PM.CF | CommonAcl.PM.CO | CommonAcl.PM.GF | CommonAcl.PM.GO;
			CommonAcl.AFtoPM[13] = CommonAcl.PM.F | CommonAcl.PM.CF | CommonAcl.PM.CO;
			CommonAcl.AFtoPM[14] = CommonAcl.PM.CF | CommonAcl.PM.CO | CommonAcl.PM.GF | CommonAcl.PM.GO;
			CommonAcl.AFtoPM[15] = CommonAcl.PM.CF | CommonAcl.PM.CO;
			CommonAcl.PMtoAF = new CommonAcl.AF[32];
			for (int j = 0; j < CommonAcl.PMtoAF.Length; j++)
			{
				CommonAcl.PMtoAF[j] = CommonAcl.AF.NP;
			}
			CommonAcl.PMtoAF[16] = (CommonAcl.AF)0;
			CommonAcl.PMtoAF[21] = CommonAcl.AF.OI;
			CommonAcl.PMtoAF[20] = CommonAcl.AF.OI | CommonAcl.AF.NP;
			CommonAcl.PMtoAF[5] = CommonAcl.AF.OI | CommonAcl.AF.IO;
			CommonAcl.PMtoAF[4] = CommonAcl.AF.OI | CommonAcl.AF.IO | CommonAcl.AF.NP;
			CommonAcl.PMtoAF[26] = CommonAcl.AF.CI;
			CommonAcl.PMtoAF[24] = CommonAcl.AF.CI | CommonAcl.AF.NP;
			CommonAcl.PMtoAF[10] = CommonAcl.AF.CI | CommonAcl.AF.IO;
			CommonAcl.PMtoAF[8] = CommonAcl.AF.CI | CommonAcl.AF.IO | CommonAcl.AF.NP;
			CommonAcl.PMtoAF[31] = CommonAcl.AF.CI | CommonAcl.AF.OI;
			CommonAcl.PMtoAF[28] = CommonAcl.AF.CI | CommonAcl.AF.OI | CommonAcl.AF.NP;
			CommonAcl.PMtoAF[15] = CommonAcl.AF.CI | CommonAcl.AF.OI | CommonAcl.AF.IO;
			CommonAcl.PMtoAF[12] = CommonAcl.AF.CI | CommonAcl.AF.OI | CommonAcl.AF.IO | CommonAcl.AF.NP;
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0006B338 File Offset: 0x00069538
		private static CommonAcl.AF AFFromAceFlags(AceFlags aceFlags, bool isDS)
		{
			CommonAcl.AF af = (CommonAcl.AF)0;
			if ((aceFlags & AceFlags.ContainerInherit) != AceFlags.None)
			{
				af |= CommonAcl.AF.CI;
			}
			if (!isDS && (aceFlags & AceFlags.ObjectInherit) != AceFlags.None)
			{
				af |= CommonAcl.AF.OI;
			}
			if ((aceFlags & AceFlags.InheritOnly) != AceFlags.None)
			{
				af |= CommonAcl.AF.IO;
			}
			if ((aceFlags & AceFlags.NoPropagateInherit) != AceFlags.None)
			{
				af |= CommonAcl.AF.NP;
			}
			return af;
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0006B370 File Offset: 0x00069570
		private static AceFlags AceFlagsFromAF(CommonAcl.AF af, bool isDS)
		{
			AceFlags aceFlags = AceFlags.None;
			if ((af & CommonAcl.AF.CI) != (CommonAcl.AF)0)
			{
				aceFlags |= AceFlags.ContainerInherit;
			}
			if (!isDS && (af & CommonAcl.AF.OI) != (CommonAcl.AF)0)
			{
				aceFlags |= AceFlags.ObjectInherit;
			}
			if ((af & CommonAcl.AF.IO) != (CommonAcl.AF)0)
			{
				aceFlags |= AceFlags.InheritOnly;
			}
			if ((af & CommonAcl.AF.NP) != (CommonAcl.AF)0)
			{
				aceFlags |= AceFlags.NoPropagateInherit;
			}
			return aceFlags;
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x0006B3A8 File Offset: 0x000695A8
		private static bool MergeInheritanceBits(AceFlags left, AceFlags right, bool isDS, out AceFlags result)
		{
			result = AceFlags.None;
			CommonAcl.AF af = CommonAcl.AFFromAceFlags(left, isDS);
			CommonAcl.AF af2 = CommonAcl.AFFromAceFlags(right, isDS);
			CommonAcl.PM pm = CommonAcl.AFtoPM[(int)af];
			CommonAcl.PM pm2 = CommonAcl.AFtoPM[(int)af2];
			if (pm == CommonAcl.PM.GO || pm2 == CommonAcl.PM.GO)
			{
				return false;
			}
			CommonAcl.PM pm3 = pm | pm2;
			CommonAcl.AF af3 = CommonAcl.PMtoAF[(int)pm3];
			if (af3 == CommonAcl.AF.NP)
			{
				return false;
			}
			result = CommonAcl.AceFlagsFromAF(af3, isDS);
			return true;
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x0006B404 File Offset: 0x00069604
		private static bool RemoveInheritanceBits(AceFlags existing, AceFlags remove, bool isDS, out AceFlags result, out bool total)
		{
			result = AceFlags.None;
			total = false;
			CommonAcl.AF af = CommonAcl.AFFromAceFlags(existing, isDS);
			CommonAcl.AF af2 = CommonAcl.AFFromAceFlags(remove, isDS);
			CommonAcl.PM pm = CommonAcl.AFtoPM[(int)af];
			CommonAcl.PM pm2 = CommonAcl.AFtoPM[(int)af2];
			if (pm == CommonAcl.PM.GO || pm2 == CommonAcl.PM.GO)
			{
				return false;
			}
			CommonAcl.PM pm3 = pm & ~pm2;
			if (pm3 == (CommonAcl.PM)0)
			{
				total = true;
				return true;
			}
			CommonAcl.AF af3 = CommonAcl.PMtoAF[(int)pm3];
			if (af3 == CommonAcl.AF.NP)
			{
				return false;
			}
			result = CommonAcl.AceFlagsFromAF(af3, isDS);
			return true;
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x0006B46E File Offset: 0x0006966E
		private void CanonicalizeIfNecessary()
		{
			if (this._isDirty)
			{
				this.Canonicalize(false, this is DiscretionaryAcl);
				this._isDirty = false;
			}
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0006B490 File Offset: 0x00069690
		private static int DaclAcePriority(GenericAce ace)
		{
			AceType aceType = ace.AceType;
			int num;
			if ((ace.AceFlags & AceFlags.Inherited) != AceFlags.None)
			{
				num = 131070 + (int)ace._indexInAcl;
			}
			else if (aceType == AceType.AccessDenied || aceType == AceType.AccessDeniedCallback)
			{
				num = 0;
			}
			else if (aceType == AceType.AccessDeniedObject || aceType == AceType.AccessDeniedCallbackObject)
			{
				num = 1;
			}
			else if (aceType == AceType.AccessAllowed || aceType == AceType.AccessAllowedCallback)
			{
				num = 2;
			}
			else if (aceType == AceType.AccessAllowedObject || aceType == AceType.AccessAllowedCallbackObject)
			{
				num = 3;
			}
			else
			{
				num = (int)(ushort.MaxValue + ace._indexInAcl);
			}
			return num;
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0006B500 File Offset: 0x00069700
		private static int SaclAcePriority(GenericAce ace)
		{
			AceType aceType = ace.AceType;
			int num;
			if ((ace.AceFlags & AceFlags.Inherited) != AceFlags.None)
			{
				num = 131070 + (int)ace._indexInAcl;
			}
			else if (aceType == AceType.SystemAudit || aceType == AceType.SystemAlarm || aceType == AceType.SystemAuditCallback || aceType == AceType.SystemAlarmCallback)
			{
				num = 0;
			}
			else if (aceType == AceType.SystemAuditObject || aceType == AceType.SystemAlarmObject || aceType == AceType.SystemAuditCallbackObject || aceType == AceType.SystemAlarmCallbackObject)
			{
				num = 1;
			}
			else
			{
				num = (int)(ushort.MaxValue + ace._indexInAcl);
			}
			return num;
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x0006B568 File Offset: 0x00069768
		private static CommonAcl.ComparisonResult CompareAces(GenericAce ace1, GenericAce ace2, bool isDacl)
		{
			int num = (isDacl ? CommonAcl.DaclAcePriority(ace1) : CommonAcl.SaclAcePriority(ace1));
			int num2 = (isDacl ? CommonAcl.DaclAcePriority(ace2) : CommonAcl.SaclAcePriority(ace2));
			if (num < num2)
			{
				return CommonAcl.ComparisonResult.LessThan;
			}
			if (num > num2)
			{
				return CommonAcl.ComparisonResult.GreaterThan;
			}
			KnownAce knownAce = ace1 as KnownAce;
			KnownAce knownAce2 = ace2 as KnownAce;
			if (knownAce != null && knownAce2 != null)
			{
				int num3 = knownAce.SecurityIdentifier.CompareTo(knownAce2.SecurityIdentifier);
				if (num3 < 0)
				{
					return CommonAcl.ComparisonResult.LessThan;
				}
				if (num3 > 0)
				{
					return CommonAcl.ComparisonResult.GreaterThan;
				}
			}
			return CommonAcl.ComparisonResult.EqualTo;
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x0006B5E8 File Offset: 0x000697E8
		private void QuickSort(int left, int right, bool isDacl)
		{
			if (left >= right)
			{
				return;
			}
			int num = left;
			int num2 = right;
			GenericAce genericAce = this._acl[left];
			while (left < right)
			{
				while (CommonAcl.CompareAces(this._acl[right], genericAce, isDacl) != CommonAcl.ComparisonResult.LessThan && left < right)
				{
					right--;
				}
				if (left != right)
				{
					this._acl[left] = this._acl[right];
					left++;
				}
				while (CommonAcl.ComparisonResult.GreaterThan != CommonAcl.CompareAces(this._acl[left], genericAce, isDacl) && left < right)
				{
					left++;
				}
				if (left != right)
				{
					this._acl[right] = this._acl[left];
					right--;
				}
			}
			this._acl[left] = genericAce;
			int num3 = left;
			left = num;
			right = num2;
			if (left < num3)
			{
				this.QuickSort(left, num3 - 1, isDacl);
			}
			if (right > num3)
			{
				this.QuickSort(num3 + 1, right, isDacl);
			}
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0006B6CC File Offset: 0x000698CC
		private bool InspectAce(ref GenericAce ace, bool isDacl)
		{
			KnownAce knownAce = ace as KnownAce;
			if (knownAce != null && knownAce.AccessMask == 0)
			{
				return false;
			}
			if (!this.IsContainer)
			{
				if ((ace.AceFlags & AceFlags.InheritOnly) != AceFlags.None)
				{
					return false;
				}
				if ((ace.AceFlags & AceFlags.InheritanceFlags) != AceFlags.None)
				{
					ace.AceFlags &= ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit | AceFlags.NoPropagateInherit | AceFlags.InheritOnly);
				}
			}
			else
			{
				if ((ace.AceFlags & AceFlags.InheritOnly) != AceFlags.None && (ace.AceFlags & AceFlags.ContainerInherit) == AceFlags.None && (ace.AceFlags & AceFlags.ObjectInherit) == AceFlags.None)
				{
					return false;
				}
				if ((ace.AceFlags & AceFlags.NoPropagateInherit) != AceFlags.None && (ace.AceFlags & AceFlags.ContainerInherit) == AceFlags.None && (ace.AceFlags & AceFlags.ObjectInherit) == AceFlags.None)
				{
					ace.AceFlags &= ~AceFlags.NoPropagateInherit;
				}
			}
			QualifiedAce qualifiedAce = knownAce as QualifiedAce;
			if (isDacl)
			{
				ace.AceFlags &= ~(AceFlags.SuccessfulAccess | AceFlags.FailedAccess);
				if (qualifiedAce != null && qualifiedAce.AceQualifier != AceQualifier.AccessAllowed && qualifiedAce.AceQualifier != AceQualifier.AccessDenied)
				{
					return false;
				}
			}
			else
			{
				if ((ace.AceFlags & AceFlags.AuditFlags) == AceFlags.None)
				{
					return false;
				}
				if (qualifiedAce != null && qualifiedAce.AceQualifier != AceQualifier.SystemAudit)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0006B7E0 File Offset: 0x000699E0
		private void RemoveMeaninglessAcesAndFlags(bool isDacl)
		{
			for (int i = this._acl.Count - 1; i >= 0; i--)
			{
				GenericAce genericAce = this._acl[i];
				if (!this.InspectAce(ref genericAce, isDacl))
				{
					this._acl.RemoveAce(i);
				}
			}
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x0006B82C File Offset: 0x00069A2C
		private void Canonicalize(bool compact, bool isDacl)
		{
			ushort num = 0;
			while ((int)num < this._acl.Count)
			{
				this._acl[(int)num]._indexInAcl = num;
				num += 1;
			}
			this.QuickSort(0, this._acl.Count - 1, isDacl);
			if (compact)
			{
				for (int i = 0; i < this.Count - 1; i++)
				{
					QualifiedAce qualifiedAce = this._acl[i] as QualifiedAce;
					if (!(qualifiedAce == null))
					{
						QualifiedAce qualifiedAce2 = this._acl[i + 1] as QualifiedAce;
						if (!(qualifiedAce2 == null) && this.MergeAces(ref qualifiedAce, qualifiedAce2))
						{
							this._acl.RemoveAce(i + 1);
						}
					}
				}
			}
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0006B8E0 File Offset: 0x00069AE0
		private void GetObjectTypesForSplit(ObjectAce originalAce, int accessMask, AceFlags aceFlags, out ObjectAceFlags objectFlags, out Guid objectType, out Guid inheritedObjectType)
		{
			objectFlags = ObjectAceFlags.None;
			objectType = Guid.Empty;
			inheritedObjectType = Guid.Empty;
			if ((accessMask & ObjectAce.AccessMaskWithObjectType) != 0)
			{
				objectType = originalAce.ObjectAceType;
				objectFlags |= originalAce.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent;
			}
			if ((aceFlags & AceFlags.ContainerInherit) != AceFlags.None)
			{
				inheritedObjectType = originalAce.InheritedObjectAceType;
				objectFlags |= originalAce.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent;
			}
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x0006B950 File Offset: 0x00069B50
		private bool ObjectTypesMatch(QualifiedAce ace, QualifiedAce newAce)
		{
			Guid guid = ((ace is ObjectAce) ? ((ObjectAce)ace).ObjectAceType : Guid.Empty);
			Guid guid2 = ((newAce is ObjectAce) ? ((ObjectAce)newAce).ObjectAceType : Guid.Empty);
			return guid.Equals(guid2);
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x0006B99C File Offset: 0x00069B9C
		private bool InheritedObjectTypesMatch(QualifiedAce ace, QualifiedAce newAce)
		{
			Guid guid = ((ace is ObjectAce) ? ((ObjectAce)ace).InheritedObjectAceType : Guid.Empty);
			Guid guid2 = ((newAce is ObjectAce) ? ((ObjectAce)newAce).InheritedObjectAceType : Guid.Empty);
			return guid.Equals(guid2);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x0006B9E8 File Offset: 0x00069BE8
		private bool AccessMasksAreMergeable(QualifiedAce ace, QualifiedAce newAce)
		{
			if (this.ObjectTypesMatch(ace, newAce))
			{
				return true;
			}
			ObjectAceFlags objectAceFlags = ((ace is ObjectAce) ? ((ObjectAce)ace).ObjectAceFlags : ObjectAceFlags.None);
			return (ace.AccessMask & newAce.AccessMask & ObjectAce.AccessMaskWithObjectType) == (newAce.AccessMask & ObjectAce.AccessMaskWithObjectType) && (objectAceFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x0006BA44 File Offset: 0x00069C44
		private bool AceFlagsAreMergeable(QualifiedAce ace, QualifiedAce newAce)
		{
			if (this.InheritedObjectTypesMatch(ace, newAce))
			{
				return true;
			}
			ObjectAceFlags objectAceFlags = ((ace is ObjectAce) ? ((ObjectAce)ace).ObjectAceFlags : ObjectAceFlags.None);
			return (objectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.None;
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x0006BA7C File Offset: 0x00069C7C
		private bool GetAccessMaskForRemoval(QualifiedAce ace, ObjectAceFlags objectFlags, Guid objectType, ref int accessMask)
		{
			if ((ace.AccessMask & accessMask & ObjectAce.AccessMaskWithObjectType) != 0)
			{
				if (ace is ObjectAce)
				{
					ObjectAce objectAce = ace as ObjectAce;
					if ((objectFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None && (objectAce.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None)
					{
						return false;
					}
					if ((objectFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None && !objectAce.ObjectTypesMatch(objectFlags, objectType))
					{
						accessMask &= ~ObjectAce.AccessMaskWithObjectType;
					}
				}
				else if ((objectFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x0006BAE8 File Offset: 0x00069CE8
		private bool GetInheritanceFlagsForRemoval(QualifiedAce ace, ObjectAceFlags objectFlags, Guid inheritedObjectType, ref AceFlags aceFlags)
		{
			if ((ace.AceFlags & AceFlags.ContainerInherit) != AceFlags.None && (aceFlags & AceFlags.ContainerInherit) != AceFlags.None)
			{
				if (ace is ObjectAce)
				{
					ObjectAce objectAce = ace as ObjectAce;
					if ((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None && (objectAce.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.None)
					{
						return false;
					}
					if ((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None && !objectAce.InheritedObjectTypesMatch(objectFlags, inheritedObjectType))
					{
						aceFlags &= ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit | AceFlags.NoPropagateInherit | AceFlags.InheritOnly);
					}
				}
				else if ((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x0006BB54 File Offset: 0x00069D54
		private static bool AceOpaquesMatch(QualifiedAce ace, QualifiedAce newAce)
		{
			byte[] opaque = ace.GetOpaque();
			byte[] opaque2 = newAce.GetOpaque();
			if (opaque == null || opaque2 == null)
			{
				return opaque == opaque2;
			}
			if (opaque.Length != opaque2.Length)
			{
				return false;
			}
			for (int i = 0; i < opaque.Length; i++)
			{
				if (opaque[i] != opaque2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x0006BBA0 File Offset: 0x00069DA0
		private static bool AcesAreMergeable(QualifiedAce ace, QualifiedAce newAce)
		{
			return ace.AceType == newAce.AceType && (ace.AceFlags & AceFlags.Inherited) == AceFlags.None && (newAce.AceFlags & AceFlags.Inherited) == AceFlags.None && ace.AceQualifier == newAce.AceQualifier && !(ace.SecurityIdentifier != newAce.SecurityIdentifier) && CommonAcl.AceOpaquesMatch(ace, newAce);
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x0006BC08 File Offset: 0x00069E08
		private bool MergeAces(ref QualifiedAce ace, QualifiedAce newAce)
		{
			if (!CommonAcl.AcesAreMergeable(ace, newAce))
			{
				return false;
			}
			if (ace.AceFlags == newAce.AceFlags)
			{
				if (!(ace is ObjectAce) && !(newAce is ObjectAce))
				{
					ace.AccessMask |= newAce.AccessMask;
					return true;
				}
				if (this.InheritedObjectTypesMatch(ace, newAce) && this.AccessMasksAreMergeable(ace, newAce))
				{
					ace.AccessMask |= newAce.AccessMask;
					return true;
				}
			}
			if ((ace.AceFlags & AceFlags.InheritanceFlags) == (newAce.AceFlags & AceFlags.InheritanceFlags) && ace.AccessMask == newAce.AccessMask)
			{
				if (!(ace is ObjectAce) && !(newAce is ObjectAce))
				{
					ace.AceFlags |= newAce.AceFlags & AceFlags.AuditFlags;
					return true;
				}
				if (this.InheritedObjectTypesMatch(ace, newAce) && this.ObjectTypesMatch(ace, newAce))
				{
					ace.AceFlags |= newAce.AceFlags & AceFlags.AuditFlags;
					return true;
				}
			}
			if ((ace.AceFlags & AceFlags.AuditFlags) == (newAce.AceFlags & AceFlags.AuditFlags) && ace.AccessMask == newAce.AccessMask)
			{
				AceFlags aceFlags;
				if (ace is ObjectAce || newAce is ObjectAce)
				{
					if (this.ObjectTypesMatch(ace, newAce) && this.AceFlagsAreMergeable(ace, newAce) && CommonAcl.MergeInheritanceBits(ace.AceFlags, newAce.AceFlags, this.IsDS, out aceFlags))
					{
						ace.AceFlags = aceFlags | (ace.AceFlags & AceFlags.AuditFlags);
						return true;
					}
				}
				else if (CommonAcl.MergeInheritanceBits(ace.AceFlags, newAce.AceFlags, this.IsDS, out aceFlags))
				{
					ace.AceFlags = aceFlags | (ace.AceFlags & AceFlags.AuditFlags);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x0006BDC8 File Offset: 0x00069FC8
		private bool CanonicalCheck(bool isDacl)
		{
			if (isDacl)
			{
				int num = 0;
				for (int i = 0; i < this._acl.Count; i++)
				{
					GenericAce genericAce = this._acl[i];
					int num2;
					if ((genericAce.AceFlags & AceFlags.Inherited) != AceFlags.None)
					{
						num2 = 2;
					}
					else
					{
						QualifiedAce qualifiedAce = genericAce as QualifiedAce;
						if (qualifiedAce == null)
						{
							return false;
						}
						if (qualifiedAce.AceQualifier == AceQualifier.AccessAllowed)
						{
							num2 = 1;
						}
						else
						{
							if (qualifiedAce.AceQualifier != AceQualifier.AccessDenied)
							{
								return false;
							}
							num2 = 0;
						}
					}
					if (num2 != 3)
					{
						if (num2 > num)
						{
							num = num2;
						}
						else if (num2 < num)
						{
							return false;
						}
					}
				}
			}
			else
			{
				int num3 = 0;
				for (int j = 0; j < this._acl.Count; j++)
				{
					GenericAce genericAce2 = this._acl[j];
					if (!(genericAce2 == null))
					{
						int num4;
						if ((genericAce2.AceFlags & AceFlags.Inherited) != AceFlags.None)
						{
							num4 = 1;
						}
						else
						{
							QualifiedAce qualifiedAce2 = genericAce2 as QualifiedAce;
							if (qualifiedAce2 == null)
							{
								return false;
							}
							if (qualifiedAce2.AceQualifier != AceQualifier.SystemAudit && qualifiedAce2.AceQualifier != AceQualifier.SystemAlarm)
							{
								return false;
							}
							num4 = 0;
						}
						if (num4 > num3)
						{
							num3 = num4;
						}
						else if (num4 < num3)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x0006BEE8 File Offset: 0x0006A0E8
		private void ThrowIfNotCanonical()
		{
			if (!this._isCanonical)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ModificationOfNonCanonicalAcl"));
			}
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x0006BF02 File Offset: 0x0006A102
		internal CommonAcl(bool isContainer, bool isDS, byte revision, int capacity)
		{
			this._isContainer = isContainer;
			this._isDS = isDS;
			this._acl = new RawAcl(revision, capacity);
			this._isCanonical = true;
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x0006BF30 File Offset: 0x0006A130
		internal CommonAcl(bool isContainer, bool isDS, RawAcl rawAcl, bool trusted, bool isDacl)
		{
			if (rawAcl == null)
			{
				throw new ArgumentNullException("rawAcl");
			}
			this._isContainer = isContainer;
			this._isDS = isDS;
			if (trusted)
			{
				this._acl = rawAcl;
				this.RemoveMeaninglessAcesAndFlags(isDacl);
			}
			else
			{
				this._acl = new RawAcl(rawAcl.Revision, rawAcl.Count);
				for (int i = 0; i < rawAcl.Count; i++)
				{
					GenericAce genericAce = rawAcl[i].Copy();
					if (this.InspectAce(ref genericAce, isDacl))
					{
						this._acl.InsertAce(this._acl.Count, genericAce);
					}
				}
			}
			if (this.CanonicalCheck(isDacl))
			{
				this.Canonicalize(true, isDacl);
				this._isCanonical = true;
				return;
			}
			this._isCanonical = false;
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x0006BFEE File Offset: 0x0006A1EE
		internal RawAcl RawAcl
		{
			get
			{
				return this._acl;
			}
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x0006BFF6 File Offset: 0x0006A1F6
		internal void CheckAccessType(AccessControlType accessType)
		{
			if (accessType != AccessControlType.Allow && accessType != AccessControlType.Deny)
			{
				throw new ArgumentOutOfRangeException("accessType", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x0006C014 File Offset: 0x0006A214
		internal void CheckFlags(InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			if (this.IsContainer)
			{
				if (inheritanceFlags == InheritanceFlags.None && propagationFlags != PropagationFlags.None)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidAnyFlag"), "propagationFlags");
				}
			}
			else
			{
				if (inheritanceFlags != InheritanceFlags.None)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidAnyFlag"), "inheritanceFlags");
				}
				if (propagationFlags != PropagationFlags.None)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidAnyFlag"), "propagationFlags");
				}
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0006C074 File Offset: 0x0006A274
		internal void AddQualifiedAce(SecurityIdentifier sid, AceQualifier qualifier, int accessMask, AceFlags flags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			this.ThrowIfNotCanonical();
			bool flag = false;
			if (qualifier == AceQualifier.SystemAudit && (flags & AceFlags.AuditFlags) == AceFlags.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "flags");
			}
			if (accessMask == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), "accessMask");
			}
			GenericAce genericAce;
			if (!this.IsDS || objectFlags == ObjectAceFlags.None)
			{
				genericAce = new CommonAce(flags, qualifier, accessMask, sid, false, null);
			}
			else
			{
				genericAce = new ObjectAce(flags, qualifier, accessMask, sid, objectFlags, objectType, inheritedObjectType, false, null);
			}
			if (!this.InspectAce(ref genericAce, this is DiscretionaryAcl))
			{
				return;
			}
			for (int i = 0; i < this.Count; i++)
			{
				QualifiedAce qualifiedAce = this._acl[i] as QualifiedAce;
				if (!(qualifiedAce == null) && this.MergeAces(ref qualifiedAce, genericAce as QualifiedAce))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this._acl.InsertAce(this._acl.Count, genericAce);
				this._isDirty = true;
			}
			this.OnAclModificationTried();
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x0006C184 File Offset: 0x0006A384
		internal void SetQualifiedAce(SecurityIdentifier sid, AceQualifier qualifier, int accessMask, AceFlags flags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			if (qualifier == AceQualifier.SystemAudit && (flags & AceFlags.AuditFlags) == AceFlags.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "flags");
			}
			if (accessMask == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), "accessMask");
			}
			this.ThrowIfNotCanonical();
			GenericAce genericAce;
			if (!this.IsDS || objectFlags == ObjectAceFlags.None)
			{
				genericAce = new CommonAce(flags, qualifier, accessMask, sid, false, null);
			}
			else
			{
				genericAce = new ObjectAce(flags, qualifier, accessMask, sid, objectFlags, objectType, inheritedObjectType, false, null);
			}
			if (!this.InspectAce(ref genericAce, this is DiscretionaryAcl))
			{
				return;
			}
			for (int i = 0; i < this.Count; i++)
			{
				QualifiedAce qualifiedAce = this._acl[i] as QualifiedAce;
				if (!(qualifiedAce == null) && (qualifiedAce.AceFlags & AceFlags.Inherited) == AceFlags.None && qualifiedAce.AceQualifier == qualifier && !(qualifiedAce.SecurityIdentifier != sid))
				{
					this._acl.RemoveAce(i);
					i--;
				}
			}
			this._acl.InsertAce(this._acl.Count, genericAce);
			this._isDirty = true;
			this.OnAclModificationTried();
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x0006C2AC File Offset: 0x0006A4AC
		internal bool RemoveQualifiedAces(SecurityIdentifier sid, AceQualifier qualifier, int accessMask, AceFlags flags, bool saclSemantics, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (accessMask == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), "accessMask");
			}
			if (qualifier == AceQualifier.SystemAudit && (flags & AceFlags.AuditFlags) == AceFlags.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "flags");
			}
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			this.ThrowIfNotCanonical();
			bool flag = true;
			bool flag2 = true;
			int num = accessMask;
			AceFlags aceFlags = flags;
			byte[] array = new byte[this.BinaryLength];
			this.GetBinaryForm(array, 0);
			for (;;)
			{
				try
				{
					for (int i = 0; i < this.Count; i++)
					{
						QualifiedAce qualifiedAce = this._acl[i] as QualifiedAce;
						if (!(qualifiedAce == null) && (qualifiedAce.AceFlags & AceFlags.Inherited) == AceFlags.None && qualifiedAce.AceQualifier == qualifier && !(qualifiedAce.SecurityIdentifier != sid))
						{
							if (this.IsDS)
							{
								accessMask = num;
								bool flag3 = !this.GetAccessMaskForRemoval(qualifiedAce, objectFlags, objectType, ref accessMask);
								if ((qualifiedAce.AccessMask & accessMask) == 0)
								{
									goto IL_443;
								}
								flags = aceFlags;
								bool flag4 = !this.GetInheritanceFlagsForRemoval(qualifiedAce, objectFlags, inheritedObjectType, ref flags);
								if (((qualifiedAce.AceFlags & AceFlags.ContainerInherit) == AceFlags.None && (flags & AceFlags.ContainerInherit) != AceFlags.None && (flags & AceFlags.InheritOnly) != AceFlags.None) || ((flags & AceFlags.ContainerInherit) == AceFlags.None && (qualifiedAce.AceFlags & AceFlags.ContainerInherit) != AceFlags.None && (qualifiedAce.AceFlags & AceFlags.InheritOnly) != AceFlags.None) || ((aceFlags & AceFlags.ContainerInherit) != AceFlags.None && (aceFlags & AceFlags.InheritOnly) != AceFlags.None && (flags & AceFlags.ContainerInherit) == AceFlags.None))
								{
									goto IL_443;
								}
								if (flag3 || flag4)
								{
									flag2 = false;
									break;
								}
							}
							else if ((qualifiedAce.AccessMask & accessMask) == 0)
							{
								goto IL_443;
							}
							if (!saclSemantics || (qualifiedAce.AceFlags & flags & AceFlags.AuditFlags) != AceFlags.None)
							{
								ObjectAceFlags objectAceFlags = ObjectAceFlags.None;
								Guid empty = Guid.Empty;
								Guid empty2 = Guid.Empty;
								AceFlags aceFlags2 = AceFlags.None;
								int num2 = 0;
								ObjectAceFlags objectAceFlags2 = ObjectAceFlags.None;
								Guid empty3 = Guid.Empty;
								Guid empty4 = Guid.Empty;
								ObjectAceFlags objectAceFlags3 = ObjectAceFlags.None;
								Guid empty5 = Guid.Empty;
								Guid empty6 = Guid.Empty;
								AceFlags aceFlags3 = AceFlags.None;
								bool flag5 = false;
								AceFlags aceFlags4 = qualifiedAce.AceFlags;
								int num3 = qualifiedAce.AccessMask & ~accessMask;
								if (qualifiedAce is ObjectAce)
								{
									this.GetObjectTypesForSplit(qualifiedAce as ObjectAce, num3, aceFlags4, out objectAceFlags, out empty, out empty2);
								}
								if (saclSemantics)
								{
									aceFlags2 = qualifiedAce.AceFlags & ~(flags & AceFlags.AuditFlags);
									num2 = qualifiedAce.AccessMask & accessMask;
									if (qualifiedAce is ObjectAce)
									{
										this.GetObjectTypesForSplit(qualifiedAce as ObjectAce, num2, aceFlags2, out objectAceFlags2, out empty3, out empty4);
									}
								}
								AceFlags aceFlags5 = (qualifiedAce.AceFlags & AceFlags.InheritanceFlags) | (flags & qualifiedAce.AceFlags & AceFlags.AuditFlags);
								int num4 = qualifiedAce.AccessMask & accessMask;
								if (!saclSemantics || (aceFlags5 & AceFlags.AuditFlags) != AceFlags.None)
								{
									if (!CommonAcl.RemoveInheritanceBits(aceFlags5, flags, this.IsDS, out aceFlags3, out flag5))
									{
										flag2 = false;
										break;
									}
									if (!flag5)
									{
										aceFlags3 |= aceFlags5 & AceFlags.AuditFlags;
										if (qualifiedAce is ObjectAce)
										{
											this.GetObjectTypesForSplit(qualifiedAce as ObjectAce, num4, aceFlags3, out objectAceFlags3, out empty5, out empty6);
										}
									}
								}
								if (!flag)
								{
									if (num3 != 0)
									{
										if (qualifiedAce is ObjectAce && (((ObjectAce)qualifiedAce).ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None && (objectAceFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None)
										{
											this._acl.RemoveAce(i);
											ObjectAce objectAce = new ObjectAce(aceFlags4, qualifier, num3, qualifiedAce.SecurityIdentifier, objectAceFlags, empty, empty2, false, null);
											this._acl.InsertAce(i, objectAce);
										}
										else
										{
											qualifiedAce.AceFlags = aceFlags4;
											qualifiedAce.AccessMask = num3;
											if (qualifiedAce is ObjectAce)
											{
												ObjectAce objectAce2 = qualifiedAce as ObjectAce;
												objectAce2.ObjectAceFlags = objectAceFlags;
												objectAce2.ObjectAceType = empty;
												objectAce2.InheritedObjectAceType = empty2;
											}
										}
									}
									else
									{
										this._acl.RemoveAce(i);
										i--;
									}
									if (saclSemantics && (aceFlags2 & AceFlags.AuditFlags) != AceFlags.None)
									{
										QualifiedAce qualifiedAce2;
										if (qualifiedAce is CommonAce)
										{
											qualifiedAce2 = new CommonAce(aceFlags2, qualifier, num2, qualifiedAce.SecurityIdentifier, false, null);
										}
										else
										{
											qualifiedAce2 = new ObjectAce(aceFlags2, qualifier, num2, qualifiedAce.SecurityIdentifier, objectAceFlags2, empty3, empty4, false, null);
										}
										i++;
										this._acl.InsertAce(i, qualifiedAce2);
									}
									if (!flag5)
									{
										QualifiedAce qualifiedAce2;
										if (qualifiedAce is CommonAce)
										{
											qualifiedAce2 = new CommonAce(aceFlags3, qualifier, num4, qualifiedAce.SecurityIdentifier, false, null);
										}
										else
										{
											qualifiedAce2 = new ObjectAce(aceFlags3, qualifier, num4, qualifiedAce.SecurityIdentifier, objectAceFlags3, empty5, empty6, false, null);
										}
										i++;
										this._acl.InsertAce(i, qualifiedAce2);
									}
								}
							}
						}
						IL_443:;
					}
				}
				catch (OverflowException)
				{
					this._acl.SetBinaryForm(array, 0);
					return false;
				}
				if (!flag || !flag2)
				{
					break;
				}
				flag = false;
			}
			this.OnAclModificationTried();
			return flag2;
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x0006C758 File Offset: 0x0006A958
		internal void RemoveQualifiedAcesSpecific(SecurityIdentifier sid, AceQualifier qualifier, int accessMask, AceFlags flags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (accessMask == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ArgumentZero"), "accessMask");
			}
			if (qualifier == AceQualifier.SystemAudit && (flags & AceFlags.AuditFlags) == AceFlags.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "flags");
			}
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			this.ThrowIfNotCanonical();
			for (int i = 0; i < this.Count; i++)
			{
				QualifiedAce qualifiedAce = this._acl[i] as QualifiedAce;
				if (!(qualifiedAce == null) && (qualifiedAce.AceFlags & AceFlags.Inherited) == AceFlags.None && qualifiedAce.AceQualifier == qualifier && !(qualifiedAce.SecurityIdentifier != sid) && qualifiedAce.AceFlags == flags && qualifiedAce.AccessMask == accessMask)
				{
					if (this.IsDS)
					{
						if (qualifiedAce is ObjectAce && objectFlags != ObjectAceFlags.None)
						{
							ObjectAce objectAce = qualifiedAce as ObjectAce;
							if (!objectAce.ObjectTypesMatch(objectFlags, objectType))
							{
								goto IL_100;
							}
							if (!objectAce.InheritedObjectTypesMatch(objectFlags, inheritedObjectType))
							{
								goto IL_100;
							}
						}
						else if (qualifiedAce is ObjectAce || objectFlags != ObjectAceFlags.None)
						{
							goto IL_100;
						}
					}
					this._acl.RemoveAce(i);
					i--;
				}
				IL_100:;
			}
			this.OnAclModificationTried();
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x0006C87B File Offset: 0x0006AA7B
		internal virtual void OnAclModificationTried()
		{
		}

		/// <summary>Gets the revision level of the <see cref="T:System.Security.AccessControl.CommonAcl" />.</summary>
		/// <returns>A byte value that specifies the revision level of the <see cref="T:System.Security.AccessControl.CommonAcl" />.</returns>
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x0006C87D File Offset: 0x0006AA7D
		public sealed override byte Revision
		{
			get
			{
				return this._acl.Revision;
			}
		}

		/// <summary>Gets the number of access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</summary>
		/// <returns>The number of ACEs in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</returns>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x0006C88A File Offset: 0x0006AA8A
		public sealed override int Count
		{
			get
			{
				this.CanonicalizeIfNecessary();
				return this._acl.Count;
			}
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object. This length should be used before marshaling the access control list (ACL) into a binary array by using the <see cref="M:System.Security.AccessControl.CommonAcl.GetBinaryForm(System.Byte[],System.Int32)" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</returns>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x0006C89D File Offset: 0x0006AA9D
		public sealed override int BinaryLength
		{
			get
			{
				this.CanonicalizeIfNecessary();
				return this._acl.BinaryLength;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object are in canonical order.</summary>
		/// <returns>
		///   <see langword="true" /> if the ACEs in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object are in canonical order; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x0006C8B0 File Offset: 0x0006AAB0
		public bool IsCanonical
		{
			get
			{
				return this._isCanonical;
			}
		}

		/// <summary>Sets whether the <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a container.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a container.</returns>
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x0006C8B8 File Offset: 0x0006AAB8
		public bool IsContainer
		{
			get
			{
				return this._isContainer;
			}
		}

		/// <summary>Sets whether the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a directory object access control list (ACL).</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a directory object ACL.</returns>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x0006C8C0 File Offset: 0x0006AAC0
		public bool IsDS
		{
			get
			{
				return this._isDS;
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.CommonAcl" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.CommonAcl" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		// Token: 0x06001ECD RID: 7885 RVA: 0x0006C8C8 File Offset: 0x0006AAC8
		public sealed override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			this.CanonicalizeIfNecessary();
			this._acl.GetBinaryForm(binaryForm, offset);
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.AccessControl.CommonAce" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.AccessControl.CommonAce" /> to get or set.</param>
		/// <returns>The <see cref="T:System.Security.AccessControl.CommonAce" /> at the specified index.</returns>
		// Token: 0x1700038D RID: 909
		public sealed override GenericAce this[int index]
		{
			get
			{
				this.CanonicalizeIfNecessary();
				return this._acl[index].Copy();
			}
			set
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SetMethod"));
			}
		}

		/// <summary>Removes all inherited access control entries (ACEs) from this <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</summary>
		// Token: 0x06001ED0 RID: 7888 RVA: 0x0006C908 File Offset: 0x0006AB08
		public void RemoveInheritedAces()
		{
			this.ThrowIfNotCanonical();
			for (int i = this._acl.Count - 1; i >= 0; i--)
			{
				GenericAce genericAce = this._acl[i];
				if ((genericAce.AceFlags & AceFlags.Inherited) != AceFlags.None)
				{
					this._acl.RemoveAce(i);
				}
			}
			this.OnAclModificationTried();
		}

		/// <summary>Removes all access control entries (ACEs) contained by this <see cref="T:System.Security.AccessControl.CommonAcl" /> object that are associated with the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> object to check for.</param>
		// Token: 0x06001ED1 RID: 7889 RVA: 0x0006C960 File Offset: 0x0006AB60
		public void Purge(SecurityIdentifier sid)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			this.ThrowIfNotCanonical();
			for (int i = this.Count - 1; i >= 0; i--)
			{
				KnownAce knownAce = this._acl[i] as KnownAce;
				if (!(knownAce == null) && (knownAce.AceFlags & AceFlags.Inherited) == AceFlags.None && knownAce.SecurityIdentifier == sid)
				{
					this._acl.RemoveAce(i);
				}
			}
			this.OnAclModificationTried();
		}

		// Token: 0x04000B0B RID: 2827
		private static CommonAcl.PM[] AFtoPM = new CommonAcl.PM[16];

		// Token: 0x04000B0C RID: 2828
		private static CommonAcl.AF[] PMtoAF;

		// Token: 0x04000B0D RID: 2829
		private RawAcl _acl;

		// Token: 0x04000B0E RID: 2830
		private bool _isDirty;

		// Token: 0x04000B0F RID: 2831
		private readonly bool _isCanonical;

		// Token: 0x04000B10 RID: 2832
		private readonly bool _isContainer;

		// Token: 0x04000B11 RID: 2833
		private readonly bool _isDS;

		// Token: 0x02000B2D RID: 2861
		[Flags]
		private enum AF
		{
			// Token: 0x0400334F RID: 13135
			CI = 8,
			// Token: 0x04003350 RID: 13136
			OI = 4,
			// Token: 0x04003351 RID: 13137
			IO = 2,
			// Token: 0x04003352 RID: 13138
			NP = 1,
			// Token: 0x04003353 RID: 13139
			Invalid = 1
		}

		// Token: 0x02000B2E RID: 2862
		[Flags]
		private enum PM
		{
			// Token: 0x04003355 RID: 13141
			F = 16,
			// Token: 0x04003356 RID: 13142
			CF = 8,
			// Token: 0x04003357 RID: 13143
			CO = 4,
			// Token: 0x04003358 RID: 13144
			GF = 2,
			// Token: 0x04003359 RID: 13145
			GO = 1,
			// Token: 0x0400335A RID: 13146
			Invalid = 1
		}

		// Token: 0x02000B2F RID: 2863
		private enum ComparisonResult
		{
			// Token: 0x0400335C RID: 13148
			LessThan,
			// Token: 0x0400335D RID: 13149
			EqualTo,
			// Token: 0x0400335E RID: 13150
			GreaterThan
		}
	}
}
