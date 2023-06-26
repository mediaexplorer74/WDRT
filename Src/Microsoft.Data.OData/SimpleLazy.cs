using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200014F RID: 335
	internal sealed class SimpleLazy<T>
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x0001CC79 File Offset: 0x0001AE79
		internal SimpleLazy(Func<T> factory)
			: this(factory, false)
		{
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001CC83 File Offset: 0x0001AE83
		internal SimpleLazy(Func<T> factory, bool isThreadSafe)
		{
			this.factory = factory;
			this.valueCreated = false;
			if (isThreadSafe)
			{
				this.mutex = new object();
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x0001CCA8 File Offset: 0x0001AEA8
		internal T Value
		{
			get
			{
				if (!this.valueCreated)
				{
					if (this.mutex != null)
					{
						lock (this.mutex)
						{
							if (!this.valueCreated)
							{
								this.CreateValue();
							}
							goto IL_41;
						}
					}
					this.CreateValue();
				}
				IL_41:
				return this.value;
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001CD0C File Offset: 0x0001AF0C
		private void CreateValue()
		{
			this.value = this.factory();
			this.valueCreated = true;
		}

		// Token: 0x04000361 RID: 865
		private readonly object mutex;

		// Token: 0x04000362 RID: 866
		private readonly Func<T> factory;

		// Token: 0x04000363 RID: 867
		private T value;

		// Token: 0x04000364 RID: 868
		private bool valueCreated;
	}
}
