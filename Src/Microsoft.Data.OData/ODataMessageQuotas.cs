using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001BB RID: 443
	public sealed class ODataMessageQuotas
	{
		// Token: 0x06000DCD RID: 3533 RVA: 0x00030774 File Offset: 0x0002E974
		public ODataMessageQuotas()
		{
			this.maxPartsPerBatch = 100;
			this.maxOperationsPerChangeset = 1000;
			this.maxNestingDepth = 100;
			this.maxReceivedMessageSize = 1048576L;
			this.maxEntityPropertyMappingsPerType = 100;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000307AC File Offset: 0x0002E9AC
		public ODataMessageQuotas(ODataMessageQuotas other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageQuotas>(other, "other");
			this.maxPartsPerBatch = other.maxPartsPerBatch;
			this.maxOperationsPerChangeset = other.maxOperationsPerChangeset;
			this.maxNestingDepth = other.maxNestingDepth;
			this.maxReceivedMessageSize = other.maxReceivedMessageSize;
			this.maxEntityPropertyMappingsPerType = other.maxEntityPropertyMappingsPerType;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00030806 File Offset: 0x0002EA06
		// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x0003080E File Offset: 0x0002EA0E
		public int MaxPartsPerBatch
		{
			get
			{
				return this.maxPartsPerBatch;
			}
			set
			{
				ExceptionUtils.CheckIntegerNotNegative(value, "MaxPartsPerBatch");
				this.maxPartsPerBatch = value;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00030822 File Offset: 0x0002EA22
		// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x0003082A File Offset: 0x0002EA2A
		public int MaxOperationsPerChangeset
		{
			get
			{
				return this.maxOperationsPerChangeset;
			}
			set
			{
				ExceptionUtils.CheckIntegerNotNegative(value, "MaxOperationsPerChangeset");
				this.maxOperationsPerChangeset = value;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0003083E File Offset: 0x0002EA3E
		// (set) Token: 0x06000DD4 RID: 3540 RVA: 0x00030846 File Offset: 0x0002EA46
		public int MaxNestingDepth
		{
			get
			{
				return this.maxNestingDepth;
			}
			set
			{
				ExceptionUtils.CheckIntegerPositive(value, "MaxNestingDepth");
				this.maxNestingDepth = value;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0003085A File Offset: 0x0002EA5A
		// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x00030862 File Offset: 0x0002EA62
		public long MaxReceivedMessageSize
		{
			get
			{
				return this.maxReceivedMessageSize;
			}
			set
			{
				ExceptionUtils.CheckLongPositive(value, "MaxReceivedMessageSize");
				this.maxReceivedMessageSize = value;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00030876 File Offset: 0x0002EA76
		// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x0003087E File Offset: 0x0002EA7E
		public int MaxEntityPropertyMappingsPerType
		{
			get
			{
				return this.maxEntityPropertyMappingsPerType;
			}
			set
			{
				ExceptionUtils.CheckIntegerNotNegative(value, "MaxEntityPropertyMappingsPerType");
				this.maxEntityPropertyMappingsPerType = value;
			}
		}

		// Token: 0x04000497 RID: 1175
		private int maxPartsPerBatch;

		// Token: 0x04000498 RID: 1176
		private int maxOperationsPerChangeset;

		// Token: 0x04000499 RID: 1177
		private int maxNestingDepth;

		// Token: 0x0400049A RID: 1178
		private long maxReceivedMessageSize;

		// Token: 0x0400049B RID: 1179
		private int maxEntityPropertyMappingsPerType;
	}
}
