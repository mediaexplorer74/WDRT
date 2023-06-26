using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading.Tasks
{
	// Token: 0x02000581 RID: 1409
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class MultiProducerMultiConsumerQueue<T> : ConcurrentQueue<T>, IProducerConsumerQueue<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600427E RID: 17022 RVA: 0x000F8A43 File Offset: 0x000F6C43
		void IProducerConsumerQueue<T>.Enqueue(T item)
		{
			base.Enqueue(item);
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x000F8A4C File Offset: 0x000F6C4C
		bool IProducerConsumerQueue<T>.TryDequeue(out T result)
		{
			return base.TryDequeue(out result);
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06004280 RID: 17024 RVA: 0x000F8A55 File Offset: 0x000F6C55
		bool IProducerConsumerQueue<T>.IsEmpty
		{
			get
			{
				return base.IsEmpty;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06004281 RID: 17025 RVA: 0x000F8A5D File Offset: 0x000F6C5D
		int IProducerConsumerQueue<T>.Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x000F8A65 File Offset: 0x000F6C65
		int IProducerConsumerQueue<T>.GetCountSafe(object syncObj)
		{
			return base.Count;
		}
	}
}
