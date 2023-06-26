using System;

namespace System.Windows.Forms
{
	// Token: 0x020002B9 RID: 697
	internal class LabelImageIndexer : ImageList.Indexer
	{
		// Token: 0x06002B10 RID: 11024 RVA: 0x000C1FC0 File Offset: 0x000C01C0
		public LabelImageIndexer(Label owner)
		{
			this.owner = owner;
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002B11 RID: 11025 RVA: 0x000C1FD6 File Offset: 0x000C01D6
		// (set) Token: 0x06002B12 RID: 11026 RVA: 0x000070A6 File Offset: 0x000052A6
		public override ImageList ImageList
		{
			get
			{
				if (this.owner != null)
				{
					return this.owner.ImageList;
				}
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x000C1FED File Offset: 0x000C01ED
		// (set) Token: 0x06002B14 RID: 11028 RVA: 0x000C1FF5 File Offset: 0x000C01F5
		public override string Key
		{
			get
			{
				return base.Key;
			}
			set
			{
				base.Key = value;
				this.useIntegerIndex = false;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06002B15 RID: 11029 RVA: 0x000C2005 File Offset: 0x000C0205
		// (set) Token: 0x06002B16 RID: 11030 RVA: 0x000C200D File Offset: 0x000C020D
		public override int Index
		{
			get
			{
				return base.Index;
			}
			set
			{
				base.Index = value;
				this.useIntegerIndex = true;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06002B17 RID: 11031 RVA: 0x000C2020 File Offset: 0x000C0220
		public override int ActualIndex
		{
			get
			{
				if (this.useIntegerIndex)
				{
					if (this.Index >= this.ImageList.Images.Count)
					{
						return this.ImageList.Images.Count - 1;
					}
					return this.Index;
				}
				else
				{
					if (this.ImageList != null)
					{
						return this.ImageList.Images.IndexOfKey(this.Key);
					}
					return -1;
				}
			}
		}

		// Token: 0x04001217 RID: 4631
		private Label owner;

		// Token: 0x04001218 RID: 4632
		private bool useIntegerIndex = true;
	}
}
