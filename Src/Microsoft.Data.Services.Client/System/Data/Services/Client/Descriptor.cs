using System;

namespace System.Data.Services.Client
{
	// Token: 0x0200001F RID: 31
	public abstract class Descriptor
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00004C2B File Offset: 0x00002E2B
		internal Descriptor(EntityStates state)
		{
			this.state = state;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004C41 File Offset: 0x00002E41
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004C49 File Offset: 0x00002E49
		public EntityStates State
		{
			get
			{
				return this.state;
			}
			internal set
			{
				this.state = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000AF RID: 175
		internal abstract DescriptorKind DescriptorKind { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004C52 File Offset: 0x00002E52
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004C5A File Offset: 0x00002E5A
		internal uint ChangeOrder
		{
			get
			{
				return this.changeOrder;
			}
			set
			{
				this.changeOrder = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004C63 File Offset: 0x00002E63
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00004C6B File Offset: 0x00002E6B
		internal bool ContentGeneratedForSave
		{
			get
			{
				return this.saveContentGenerated;
			}
			set
			{
				this.saveContentGenerated = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004C74 File Offset: 0x00002E74
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00004C7C File Offset: 0x00002E7C
		internal EntityStates SaveResultWasProcessed
		{
			get
			{
				return this.saveResultProcessed;
			}
			set
			{
				this.saveResultProcessed = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004C85 File Offset: 0x00002E85
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00004C8D File Offset: 0x00002E8D
		internal Exception SaveError
		{
			get
			{
				return this.saveError;
			}
			set
			{
				this.saveError = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004C96 File Offset: 0x00002E96
		internal virtual bool IsModified
		{
			get
			{
				return EntityStates.Unchanged != this.state;
			}
		}

		// Token: 0x060000B9 RID: 185
		internal abstract void ClearChanges();

		// Token: 0x04000199 RID: 409
		private uint changeOrder = uint.MaxValue;

		// Token: 0x0400019A RID: 410
		private bool saveContentGenerated;

		// Token: 0x0400019B RID: 411
		private EntityStates saveResultProcessed;

		// Token: 0x0400019C RID: 412
		private Exception saveError;

		// Token: 0x0400019D RID: 413
		private EntityStates state;
	}
}
