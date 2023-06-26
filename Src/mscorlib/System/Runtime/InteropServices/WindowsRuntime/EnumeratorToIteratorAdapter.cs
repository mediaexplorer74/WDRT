using System;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D1 RID: 2513
	internal sealed class EnumeratorToIteratorAdapter<T> : IIterator<T>, IBindableIterator
	{
		// Token: 0x0600641A RID: 25626 RVA: 0x00156828 File Offset: 0x00154A28
		internal EnumeratorToIteratorAdapter(IEnumerator<T> enumerator)
		{
			this.m_enumerator = enumerator;
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x0600641B RID: 25627 RVA: 0x0015683E File Offset: 0x00154A3E
		public T Current
		{
			get
			{
				if (this.m_firstItem)
				{
					this.m_firstItem = false;
					this.MoveNext();
				}
				if (!this.m_hasCurrent)
				{
					throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483637, null);
				}
				return this.m_enumerator.Current;
			}
		}

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x0600641C RID: 25628 RVA: 0x00156875 File Offset: 0x00154A75
		object IBindableIterator.Current
		{
			get
			{
				return ((IIterator<T>)this).Current;
			}
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x0600641D RID: 25629 RVA: 0x00156882 File Offset: 0x00154A82
		public bool HasCurrent
		{
			get
			{
				if (this.m_firstItem)
				{
					this.m_firstItem = false;
					this.MoveNext();
				}
				return this.m_hasCurrent;
			}
		}

		// Token: 0x0600641E RID: 25630 RVA: 0x001568A0 File Offset: 0x00154AA0
		public bool MoveNext()
		{
			try
			{
				this.m_hasCurrent = this.m_enumerator.MoveNext();
			}
			catch (InvalidOperationException ex)
			{
				throw WindowsRuntimeMarshal.GetExceptionForHR(-2147483636, ex);
			}
			return this.m_hasCurrent;
		}

		// Token: 0x0600641F RID: 25631 RVA: 0x001568E4 File Offset: 0x00154AE4
		public int GetMany(T[] items)
		{
			if (items == null)
			{
				return 0;
			}
			int num = 0;
			while (num < items.Length && this.HasCurrent)
			{
				items[num] = this.Current;
				this.MoveNext();
				num++;
			}
			if (typeof(T) == typeof(string))
			{
				string[] array = items as string[];
				for (int i = num; i < items.Length; i++)
				{
					array[i] = string.Empty;
				}
			}
			return num;
		}

		// Token: 0x04002CF3 RID: 11507
		private IEnumerator<T> m_enumerator;

		// Token: 0x04002CF4 RID: 11508
		private bool m_firstItem = true;

		// Token: 0x04002CF5 RID: 11509
		private bool m_hasCurrent;
	}
}
