using System;

namespace System.Security.Policy
{
	// Token: 0x02000350 RID: 848
	[Serializable]
	internal sealed class EvidenceTypeDescriptor
	{
		// Token: 0x06002A65 RID: 10853 RVA: 0x0009E3DB File Offset: 0x0009C5DB
		public EvidenceTypeDescriptor()
		{
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x0009E3E4 File Offset: 0x0009C5E4
		private EvidenceTypeDescriptor(EvidenceTypeDescriptor descriptor)
		{
			this.m_hostCanGenerate = descriptor.m_hostCanGenerate;
			if (descriptor.m_assemblyEvidence != null)
			{
				this.m_assemblyEvidence = descriptor.m_assemblyEvidence.Clone();
			}
			if (descriptor.m_hostEvidence != null)
			{
				this.m_hostEvidence = descriptor.m_hostEvidence.Clone();
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06002A67 RID: 10855 RVA: 0x0009E435 File Offset: 0x0009C635
		// (set) Token: 0x06002A68 RID: 10856 RVA: 0x0009E43D File Offset: 0x0009C63D
		public EvidenceBase AssemblyEvidence
		{
			get
			{
				return this.m_assemblyEvidence;
			}
			set
			{
				this.m_assemblyEvidence = value;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002A69 RID: 10857 RVA: 0x0009E446 File Offset: 0x0009C646
		// (set) Token: 0x06002A6A RID: 10858 RVA: 0x0009E44E File Offset: 0x0009C64E
		public bool Generated
		{
			get
			{
				return this.m_generated;
			}
			set
			{
				this.m_generated = value;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002A6B RID: 10859 RVA: 0x0009E457 File Offset: 0x0009C657
		// (set) Token: 0x06002A6C RID: 10860 RVA: 0x0009E45F File Offset: 0x0009C65F
		public bool HostCanGenerate
		{
			get
			{
				return this.m_hostCanGenerate;
			}
			set
			{
				this.m_hostCanGenerate = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06002A6D RID: 10861 RVA: 0x0009E468 File Offset: 0x0009C668
		// (set) Token: 0x06002A6E RID: 10862 RVA: 0x0009E470 File Offset: 0x0009C670
		public EvidenceBase HostEvidence
		{
			get
			{
				return this.m_hostEvidence;
			}
			set
			{
				this.m_hostEvidence = value;
			}
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x0009E479 File Offset: 0x0009C679
		public EvidenceTypeDescriptor Clone()
		{
			return new EvidenceTypeDescriptor(this);
		}

		// Token: 0x04001146 RID: 4422
		[NonSerialized]
		private bool m_hostCanGenerate;

		// Token: 0x04001147 RID: 4423
		[NonSerialized]
		private bool m_generated;

		// Token: 0x04001148 RID: 4424
		private EvidenceBase m_hostEvidence;

		// Token: 0x04001149 RID: 4425
		private EvidenceBase m_assemblyEvidence;
	}
}
