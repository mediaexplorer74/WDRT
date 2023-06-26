using System;

namespace System.IO.Compression
{
	// Token: 0x0200041E RID: 1054
	internal class DeflateInput
	{
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x000B5777 File Offset: 0x000B3977
		// (set) Token: 0x0600275F RID: 10079 RVA: 0x000B577F File Offset: 0x000B397F
		internal byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
			set
			{
				this.buffer = value;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002760 RID: 10080 RVA: 0x000B5788 File Offset: 0x000B3988
		// (set) Token: 0x06002761 RID: 10081 RVA: 0x000B5790 File Offset: 0x000B3990
		internal int Count
		{
			get
			{
				return this.count;
			}
			set
			{
				this.count = value;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06002762 RID: 10082 RVA: 0x000B5799 File Offset: 0x000B3999
		// (set) Token: 0x06002763 RID: 10083 RVA: 0x000B57A1 File Offset: 0x000B39A1
		internal int StartIndex
		{
			get
			{
				return this.startIndex;
			}
			set
			{
				this.startIndex = value;
			}
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x000B57AA File Offset: 0x000B39AA
		internal void ConsumeBytes(int n)
		{
			this.startIndex += n;
			this.count -= n;
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000B57C8 File Offset: 0x000B39C8
		internal DeflateInput.InputState DumpState()
		{
			DeflateInput.InputState inputState;
			inputState.count = this.count;
			inputState.startIndex = this.startIndex;
			return inputState;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000B57F0 File Offset: 0x000B39F0
		internal void RestoreState(DeflateInput.InputState state)
		{
			this.count = state.count;
			this.startIndex = state.startIndex;
		}

		// Token: 0x04002159 RID: 8537
		private byte[] buffer;

		// Token: 0x0400215A RID: 8538
		private int count;

		// Token: 0x0400215B RID: 8539
		private int startIndex;

		// Token: 0x02000815 RID: 2069
		internal struct InputState
		{
			// Token: 0x04003577 RID: 13687
			internal int count;

			// Token: 0x04003578 RID: 13688
			internal int startIndex;
		}
	}
}
