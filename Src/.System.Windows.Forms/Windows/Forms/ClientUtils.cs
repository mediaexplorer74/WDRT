using System;
using System.Collections;
using System.Security;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x0200010F RID: 271
	internal static class ClientUtils
	{
		// Token: 0x0600073B RID: 1851 RVA: 0x00014BFA File Offset: 0x00012DFA
		public static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is ExecutionEngineException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00014C37 File Offset: 0x00012E37
		public static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || ClientUtils.IsCriticalException(ex);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00014C4C File Offset: 0x00012E4C
		public static int GetBitCount(uint x)
		{
			int num = 0;
			while (x > 0U)
			{
				x &= x - 1U;
				num++;
			}
			return num;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00014C70 File Offset: 0x00012E70
		public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue)
		{
			return value >= minValue && value <= maxValue;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00014C90 File Offset: 0x00012E90
		public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue, int maxNumberOfBitsOn)
		{
			bool flag = value >= minValue && value <= maxValue;
			return flag && ClientUtils.GetBitCount((uint)value) <= maxNumberOfBitsOn;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00014CC4 File Offset: 0x00012EC4
		public static bool IsEnumValid_Masked(Enum enumValue, int value, uint mask)
		{
			return ((long)value & (long)((ulong)mask)) == (long)value;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00014CDC File Offset: 0x00012EDC
		public static bool IsEnumValid_NotSequential(Enum enumValue, int value, params int[] enumValues)
		{
			for (int i = 0; i < enumValues.Length; i++)
			{
				if (enumValues[i] == value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x020005FA RID: 1530
		internal class WeakRefCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006192 RID: 24978 RVA: 0x00168A85 File Offset: 0x00166C85
			internal WeakRefCollection()
			{
				this._innerList = new ArrayList(4);
			}

			// Token: 0x06006193 RID: 24979 RVA: 0x00168AA4 File Offset: 0x00166CA4
			internal WeakRefCollection(int size)
			{
				this._innerList = new ArrayList(size);
			}

			// Token: 0x170014F7 RID: 5367
			// (get) Token: 0x06006194 RID: 24980 RVA: 0x00168AC3 File Offset: 0x00166CC3
			internal ArrayList InnerList
			{
				get
				{
					return this._innerList;
				}
			}

			// Token: 0x170014F8 RID: 5368
			// (get) Token: 0x06006195 RID: 24981 RVA: 0x00168ACB File Offset: 0x00166CCB
			// (set) Token: 0x06006196 RID: 24982 RVA: 0x00168AD3 File Offset: 0x00166CD3
			public int RefCheckThreshold
			{
				get
				{
					return this.refCheckThreshold;
				}
				set
				{
					this.refCheckThreshold = value;
				}
			}

			// Token: 0x170014F9 RID: 5369
			public object this[int index]
			{
				get
				{
					ClientUtils.WeakRefCollection.WeakRefObject weakRefObject = this.InnerList[index] as ClientUtils.WeakRefCollection.WeakRefObject;
					if (weakRefObject != null && weakRefObject.IsAlive)
					{
						return weakRefObject.Target;
					}
					return null;
				}
				set
				{
					this.InnerList[index] = this.CreateWeakRefObject(value);
				}
			}

			// Token: 0x06006199 RID: 24985 RVA: 0x00168B24 File Offset: 0x00166D24
			public void ScavengeReferences()
			{
				int num = 0;
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					if (this[num] == null)
					{
						this.InnerList.RemoveAt(num);
					}
					else
					{
						num++;
					}
				}
			}

			// Token: 0x0600619A RID: 24986 RVA: 0x00168B64 File Offset: 0x00166D64
			public override bool Equals(object obj)
			{
				ClientUtils.WeakRefCollection weakRefCollection = obj as ClientUtils.WeakRefCollection;
				if (weakRefCollection == this)
				{
					return true;
				}
				if (weakRefCollection == null || this.Count != weakRefCollection.Count)
				{
					return false;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (this.InnerList[i] != weakRefCollection.InnerList[i] && (this.InnerList[i] == null || !this.InnerList[i].Equals(weakRefCollection.InnerList[i])))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600619B RID: 24987 RVA: 0x0014D2ED File Offset: 0x0014B4ED
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x0600619C RID: 24988 RVA: 0x00168BEC File Offset: 0x00166DEC
			private ClientUtils.WeakRefCollection.WeakRefObject CreateWeakRefObject(object value)
			{
				if (value == null)
				{
					return null;
				}
				return new ClientUtils.WeakRefCollection.WeakRefObject(value);
			}

			// Token: 0x0600619D RID: 24989 RVA: 0x00168BFC File Offset: 0x00166DFC
			private static void Copy(ClientUtils.WeakRefCollection sourceList, int sourceIndex, ClientUtils.WeakRefCollection destinationList, int destinationIndex, int length)
			{
				if (sourceIndex < destinationIndex)
				{
					sourceIndex += length;
					destinationIndex += length;
					while (length > 0)
					{
						destinationList.InnerList[--destinationIndex] = sourceList.InnerList[--sourceIndex];
						length--;
					}
					return;
				}
				while (length > 0)
				{
					destinationList.InnerList[destinationIndex++] = sourceList.InnerList[sourceIndex++];
					length--;
				}
			}

			// Token: 0x0600619E RID: 24990 RVA: 0x00168C78 File Offset: 0x00166E78
			public void RemoveByHashCode(object value)
			{
				if (value == null)
				{
					return;
				}
				int hashCode = value.GetHashCode();
				for (int i = 0; i < this.InnerList.Count; i++)
				{
					if (this.InnerList[i] != null && this.InnerList[i].GetHashCode() == hashCode)
					{
						this.RemoveAt(i);
						return;
					}
				}
			}

			// Token: 0x0600619F RID: 24991 RVA: 0x00168CD0 File Offset: 0x00166ED0
			public void Clear()
			{
				this.InnerList.Clear();
			}

			// Token: 0x170014FA RID: 5370
			// (get) Token: 0x060061A0 RID: 24992 RVA: 0x00168CDD File Offset: 0x00166EDD
			public bool IsFixedSize
			{
				get
				{
					return this.InnerList.IsFixedSize;
				}
			}

			// Token: 0x060061A1 RID: 24993 RVA: 0x00168CEA File Offset: 0x00166EEA
			public bool Contains(object value)
			{
				return this.InnerList.Contains(this.CreateWeakRefObject(value));
			}

			// Token: 0x060061A2 RID: 24994 RVA: 0x00168CFE File Offset: 0x00166EFE
			public void RemoveAt(int index)
			{
				this.InnerList.RemoveAt(index);
			}

			// Token: 0x060061A3 RID: 24995 RVA: 0x00168D0C File Offset: 0x00166F0C
			public void Remove(object value)
			{
				this.InnerList.Remove(this.CreateWeakRefObject(value));
			}

			// Token: 0x060061A4 RID: 24996 RVA: 0x00168D20 File Offset: 0x00166F20
			public int IndexOf(object value)
			{
				return this.InnerList.IndexOf(this.CreateWeakRefObject(value));
			}

			// Token: 0x060061A5 RID: 24997 RVA: 0x00168D34 File Offset: 0x00166F34
			public void Insert(int index, object value)
			{
				this.InnerList.Insert(index, this.CreateWeakRefObject(value));
			}

			// Token: 0x060061A6 RID: 24998 RVA: 0x00168D49 File Offset: 0x00166F49
			public int Add(object value)
			{
				if (this.Count > this.RefCheckThreshold)
				{
					this.ScavengeReferences();
				}
				return this.InnerList.Add(this.CreateWeakRefObject(value));
			}

			// Token: 0x170014FB RID: 5371
			// (get) Token: 0x060061A7 RID: 24999 RVA: 0x00168D71 File Offset: 0x00166F71
			public int Count
			{
				get
				{
					return this.InnerList.Count;
				}
			}

			// Token: 0x170014FC RID: 5372
			// (get) Token: 0x060061A8 RID: 25000 RVA: 0x00168D7E File Offset: 0x00166F7E
			object ICollection.SyncRoot
			{
				get
				{
					return this.InnerList.SyncRoot;
				}
			}

			// Token: 0x170014FD RID: 5373
			// (get) Token: 0x060061A9 RID: 25001 RVA: 0x00168D8B File Offset: 0x00166F8B
			public bool IsReadOnly
			{
				get
				{
					return this.InnerList.IsReadOnly;
				}
			}

			// Token: 0x060061AA RID: 25002 RVA: 0x00168D98 File Offset: 0x00166F98
			public void CopyTo(Array array, int index)
			{
				this.InnerList.CopyTo(array, index);
			}

			// Token: 0x170014FE RID: 5374
			// (get) Token: 0x060061AB RID: 25003 RVA: 0x00168DA7 File Offset: 0x00166FA7
			bool ICollection.IsSynchronized
			{
				get
				{
					return this.InnerList.IsSynchronized;
				}
			}

			// Token: 0x060061AC RID: 25004 RVA: 0x00168DB4 File Offset: 0x00166FB4
			public IEnumerator GetEnumerator()
			{
				return this.InnerList.GetEnumerator();
			}

			// Token: 0x04003890 RID: 14480
			private int refCheckThreshold = int.MaxValue;

			// Token: 0x04003891 RID: 14481
			private ArrayList _innerList;

			// Token: 0x020008AF RID: 2223
			internal class WeakRefObject
			{
				// Token: 0x06007254 RID: 29268 RVA: 0x001A2483 File Offset: 0x001A0683
				internal WeakRefObject(object obj)
				{
					this.weakHolder = new WeakReference(obj);
					this.hash = obj.GetHashCode();
				}

				// Token: 0x1700191E RID: 6430
				// (get) Token: 0x06007255 RID: 29269 RVA: 0x001A24A3 File Offset: 0x001A06A3
				internal bool IsAlive
				{
					get
					{
						return this.weakHolder.IsAlive;
					}
				}

				// Token: 0x1700191F RID: 6431
				// (get) Token: 0x06007256 RID: 29270 RVA: 0x001A24B0 File Offset: 0x001A06B0
				internal object Target
				{
					get
					{
						return this.weakHolder.Target;
					}
				}

				// Token: 0x06007257 RID: 29271 RVA: 0x001A24BD File Offset: 0x001A06BD
				public override int GetHashCode()
				{
					return this.hash;
				}

				// Token: 0x06007258 RID: 29272 RVA: 0x001A24C8 File Offset: 0x001A06C8
				public override bool Equals(object obj)
				{
					ClientUtils.WeakRefCollection.WeakRefObject weakRefObject = obj as ClientUtils.WeakRefCollection.WeakRefObject;
					return weakRefObject == this || (weakRefObject != null && (weakRefObject.Target == this.Target || (this.Target != null && this.Target.Equals(weakRefObject.Target))));
				}

				// Token: 0x0400451D RID: 17693
				private int hash;

				// Token: 0x0400451E RID: 17694
				private WeakReference weakHolder;
			}
		}
	}
}
