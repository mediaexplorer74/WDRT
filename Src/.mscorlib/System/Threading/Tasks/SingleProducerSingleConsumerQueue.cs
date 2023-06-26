using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000582 RID: 1410
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SingleProducerSingleConsumerQueue<>.SingleProducerSingleConsumerQueue_DebugView))]
	internal sealed class SingleProducerSingleConsumerQueue<T> : IProducerConsumerQueue<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06004284 RID: 17028 RVA: 0x000F8A78 File Offset: 0x000F6C78
		internal SingleProducerSingleConsumerQueue()
		{
			this.m_head = (this.m_tail = new SingleProducerSingleConsumerQueue<T>.Segment(32));
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x000F8AA8 File Offset: 0x000F6CA8
		public void Enqueue(T item)
		{
			SingleProducerSingleConsumerQueue<T>.Segment tail = this.m_tail;
			T[] array = tail.m_array;
			int last = tail.m_state.m_last;
			int num = (last + 1) & (array.Length - 1);
			if (num != tail.m_state.m_firstCopy)
			{
				array[last] = item;
				tail.m_state.m_last = num;
				return;
			}
			this.EnqueueSlow(item, ref tail);
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x000F8B0C File Offset: 0x000F6D0C
		private void EnqueueSlow(T item, ref SingleProducerSingleConsumerQueue<T>.Segment segment)
		{
			if (segment.m_state.m_firstCopy != segment.m_state.m_first)
			{
				segment.m_state.m_firstCopy = segment.m_state.m_first;
				this.Enqueue(item);
				return;
			}
			int num = this.m_tail.m_array.Length << 1;
			if (num > 16777216)
			{
				num = 16777216;
			}
			SingleProducerSingleConsumerQueue<T>.Segment segment2 = new SingleProducerSingleConsumerQueue<T>.Segment(num);
			segment2.m_array[0] = item;
			segment2.m_state.m_last = 1;
			segment2.m_state.m_lastCopy = 1;
			try
			{
			}
			finally
			{
				Volatile.Write<SingleProducerSingleConsumerQueue<T>.Segment>(ref this.m_tail.m_next, segment2);
				this.m_tail = segment2;
			}
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x000F8BD4 File Offset: 0x000F6DD4
		public bool TryDequeue(out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first != head.m_state.m_lastCopy)
			{
				result = array[first];
				array[first] = default(T);
				head.m_state.m_first = (first + 1) & (array.Length - 1);
				return true;
			}
			return this.TryDequeueSlow(ref head, ref array, out result);
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x000F8C50 File Offset: 0x000F6E50
		private bool TryDequeueSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryDequeue(out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			array[first] = default(T);
			segment.m_state.m_first = (first + 1) & (segment.m_array.Length - 1);
			segment.m_state.m_lastCopy = segment.m_state.m_last;
			return true;
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x000F8D60 File Offset: 0x000F6F60
		public bool TryPeek(out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first != head.m_state.m_lastCopy)
			{
				result = array[first];
				return true;
			}
			return this.TryPeekSlow(ref head, ref array, out result);
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x000F8DB4 File Offset: 0x000F6FB4
		private bool TryPeekSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryPeek(out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			return true;
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x000F8E7C File Offset: 0x000F707C
		public bool TryDequeueIf(Predicate<T> predicate, out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first == head.m_state.m_lastCopy)
			{
				return this.TryDequeueIfSlow(predicate, ref head, ref array, out result);
			}
			result = array[first];
			if (predicate == null || predicate(result))
			{
				array[first] = default(T);
				head.m_state.m_first = (first + 1) & (array.Length - 1);
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x000F8F10 File Offset: 0x000F7110
		private bool TryDequeueIfSlow(Predicate<T> predicate, ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryDequeueIf(predicate, out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			if (predicate == null || predicate(result))
			{
				array[first] = default(T);
				segment.m_state.m_first = (first + 1) & (segment.m_array.Length - 1);
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x000F9040 File Offset: 0x000F7240
		public void Clear()
		{
			T t;
			while (this.TryDequeue(out t))
			{
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x0600428E RID: 17038 RVA: 0x000F9058 File Offset: 0x000F7258
		public bool IsEmpty
		{
			get
			{
				SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
				return head.m_state.m_first == head.m_state.m_lastCopy && head.m_state.m_first == head.m_state.m_last && head.m_next == null;
			}
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x000F90B1 File Offset: 0x000F72B1
		public IEnumerator<T> GetEnumerator()
		{
			SingleProducerSingleConsumerQueue<T>.Segment segment;
			for (segment = this.m_head; segment != null; segment = segment.m_next)
			{
				for (int pt = segment.m_state.m_first; pt != segment.m_state.m_last; pt = (pt + 1) & (segment.m_array.Length - 1))
				{
					yield return segment.m_array[pt];
				}
			}
			segment = null;
			yield break;
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x000F90C0 File Offset: 0x000F72C0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06004291 RID: 17041 RVA: 0x000F90C8 File Offset: 0x000F72C8
		public int Count
		{
			get
			{
				int num = 0;
				for (SingleProducerSingleConsumerQueue<T>.Segment segment = this.m_head; segment != null; segment = segment.m_next)
				{
					int num2 = segment.m_array.Length;
					int first;
					int last;
					do
					{
						first = segment.m_state.m_first;
						last = segment.m_state.m_last;
					}
					while (first != segment.m_state.m_first);
					num += (last - first) & (num2 - 1);
				}
				return num;
			}
		}

		// Token: 0x06004292 RID: 17042 RVA: 0x000F9130 File Offset: 0x000F7330
		int IProducerConsumerQueue<T>.GetCountSafe(object syncObj)
		{
			int count;
			lock (syncObj)
			{
				count = this.Count;
			}
			return count;
		}

		// Token: 0x04001BA3 RID: 7075
		private const int INIT_SEGMENT_SIZE = 32;

		// Token: 0x04001BA4 RID: 7076
		private const int MAX_SEGMENT_SIZE = 16777216;

		// Token: 0x04001BA5 RID: 7077
		private volatile SingleProducerSingleConsumerQueue<T>.Segment m_head;

		// Token: 0x04001BA6 RID: 7078
		private volatile SingleProducerSingleConsumerQueue<T>.Segment m_tail;

		// Token: 0x02000C25 RID: 3109
		[StructLayout(LayoutKind.Sequential)]
		private sealed class Segment
		{
			// Token: 0x0600703A RID: 28730 RVA: 0x00184276 File Offset: 0x00182476
			internal Segment(int size)
			{
				this.m_array = new T[size];
			}

			// Token: 0x040036F1 RID: 14065
			internal SingleProducerSingleConsumerQueue<T>.Segment m_next;

			// Token: 0x040036F2 RID: 14066
			internal readonly T[] m_array;

			// Token: 0x040036F3 RID: 14067
			internal SingleProducerSingleConsumerQueue<T>.SegmentState m_state;
		}

		// Token: 0x02000C26 RID: 3110
		private struct SegmentState
		{
			// Token: 0x040036F4 RID: 14068
			internal PaddingFor32 m_pad0;

			// Token: 0x040036F5 RID: 14069
			internal volatile int m_first;

			// Token: 0x040036F6 RID: 14070
			internal int m_lastCopy;

			// Token: 0x040036F7 RID: 14071
			internal PaddingFor32 m_pad1;

			// Token: 0x040036F8 RID: 14072
			internal int m_firstCopy;

			// Token: 0x040036F9 RID: 14073
			internal volatile int m_last;

			// Token: 0x040036FA RID: 14074
			internal PaddingFor32 m_pad2;
		}

		// Token: 0x02000C27 RID: 3111
		private sealed class SingleProducerSingleConsumerQueue_DebugView
		{
			// Token: 0x0600703B RID: 28731 RVA: 0x0018428A File Offset: 0x0018248A
			public SingleProducerSingleConsumerQueue_DebugView(SingleProducerSingleConsumerQueue<T> queue)
			{
				this.m_queue = queue;
			}

			// Token: 0x17001337 RID: 4919
			// (get) Token: 0x0600703C RID: 28732 RVA: 0x0018429C File Offset: 0x0018249C
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public T[] Items
			{
				get
				{
					List<T> list = new List<T>();
					foreach (T t in this.m_queue)
					{
						list.Add(t);
					}
					return list.ToArray();
				}
			}

			// Token: 0x040036FB RID: 14075
			private readonly SingleProducerSingleConsumerQueue<T> m_queue;
		}
	}
}
