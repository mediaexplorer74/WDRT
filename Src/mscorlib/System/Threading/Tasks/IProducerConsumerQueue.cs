using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Threading.Tasks
{
	// Token: 0x02000580 RID: 1408
	internal interface IProducerConsumerQueue<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06004279 RID: 17017
		void Enqueue(T item);

		// Token: 0x0600427A RID: 17018
		bool TryDequeue(out T result);

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x0600427B RID: 17019
		bool IsEmpty { get; }

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x0600427C RID: 17020
		int Count { get; }

		// Token: 0x0600427D RID: 17021
		int GetCountSafe(object syncObj);
	}
}
