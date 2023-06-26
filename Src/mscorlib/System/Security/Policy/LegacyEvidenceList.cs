using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x0200034F RID: 847
	[Serializable]
	internal sealed class LegacyEvidenceList : EvidenceBase, IEnumerable<EvidenceBase>, IEnumerable, ILegacyEvidenceAdapter
	{
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x0009E333 File Offset: 0x0009C533
		public object EvidenceObject
		{
			get
			{
				if (this.m_legacyEvidenceList.Count <= 0)
				{
					return null;
				}
				return this.m_legacyEvidenceList[0];
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x0009E354 File Offset: 0x0009C554
		public Type EvidenceType
		{
			get
			{
				ILegacyEvidenceAdapter legacyEvidenceAdapter = this.m_legacyEvidenceList[0] as ILegacyEvidenceAdapter;
				if (legacyEvidenceAdapter != null)
				{
					return legacyEvidenceAdapter.EvidenceType;
				}
				return this.m_legacyEvidenceList[0].GetType();
			}
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x0009E38E File Offset: 0x0009C58E
		public void Add(EvidenceBase evidence)
		{
			this.m_legacyEvidenceList.Add(evidence);
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x0009E39C File Offset: 0x0009C59C
		public IEnumerator<EvidenceBase> GetEnumerator()
		{
			return this.m_legacyEvidenceList.GetEnumerator();
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x0009E3AE File Offset: 0x0009C5AE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_legacyEvidenceList.GetEnumerator();
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x0009E3C0 File Offset: 0x0009C5C0
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override EvidenceBase Clone()
		{
			return base.Clone();
		}

		// Token: 0x04001145 RID: 4421
		private List<EvidenceBase> m_legacyEvidenceList = new List<EvidenceBase>();
	}
}
