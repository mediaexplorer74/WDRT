using System;
using System.Collections;
using System.Security;
using System.Threading;

namespace System.Drawing
{
	// Token: 0x0200000D RID: 13
	internal static class ClientUtils
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002994 File Offset: 0x00000B94
		public static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is ExecutionEngineException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000029D1 File Offset: 0x00000BD1
		public static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || ClientUtils.IsCriticalException(ex);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000029E4 File Offset: 0x00000BE4
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

		// Token: 0x06000029 RID: 41 RVA: 0x00002A08 File Offset: 0x00000C08
		public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue)
		{
			return value >= minValue && value <= maxValue;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002A28 File Offset: 0x00000C28
		public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue, int maxNumberOfBitsOn)
		{
			bool flag = value >= minValue && value <= maxValue;
			return flag && ClientUtils.GetBitCount((uint)value) <= maxNumberOfBitsOn;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002A5C File Offset: 0x00000C5C
		public static bool IsEnumValid_Masked(Enum enumValue, int value, uint mask)
		{
			return ((long)value & (long)((ulong)mask)) == (long)value;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A74 File Offset: 0x00000C74
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

		// Token: 0x020000F3 RID: 243
		internal class WeakRefCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06000C75 RID: 3189 RVA: 0x0002C150 File Offset: 0x0002A350
			internal WeakRefCollection()
			{
				this._innerList = new ArrayList(4);
			}

			// Token: 0x06000C76 RID: 3190 RVA: 0x0002C16F File Offset: 0x0002A36F
			internal WeakRefCollection(int size)
			{
				this._innerList = new ArrayList(size);
			}

			// Token: 0x170003D6 RID: 982
			// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0002C18E File Offset: 0x0002A38E
			internal ArrayList InnerList
			{
				get
				{
					return this._innerList;
				}
			}

			// Token: 0x170003D7 RID: 983
			// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0002C196 File Offset: 0x0002A396
			// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0002C19E File Offset: 0x0002A39E
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

			// Token: 0x170003D8 RID: 984
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

			// Token: 0x06000C7C RID: 3196 RVA: 0x0002C1F0 File Offset: 0x0002A3F0
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

			// Token: 0x06000C7D RID: 3197 RVA: 0x0002C230 File Offset: 0x0002A430
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

			// Token: 0x06000C7E RID: 3198 RVA: 0x00029207 File Offset: 0x00027407
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x06000C7F RID: 3199 RVA: 0x0002C2B8 File Offset: 0x0002A4B8
			private ClientUtils.WeakRefCollection.WeakRefObject CreateWeakRefObject(object value)
			{
				if (value == null)
				{
					return null;
				}
				return new ClientUtils.WeakRefCollection.WeakRefObject(value);
			}

			// Token: 0x06000C80 RID: 3200 RVA: 0x0002C2C8 File Offset: 0x0002A4C8
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

			// Token: 0x06000C81 RID: 3201 RVA: 0x0002C344 File Offset: 0x0002A544
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

			// Token: 0x06000C82 RID: 3202 RVA: 0x0002C39C File Offset: 0x0002A59C
			public void Clear()
			{
				this.InnerList.Clear();
			}

			// Token: 0x170003D9 RID: 985
			// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0002C3A9 File Offset: 0x0002A5A9
			public bool IsFixedSize
			{
				get
				{
					return this.InnerList.IsFixedSize;
				}
			}

			// Token: 0x06000C84 RID: 3204 RVA: 0x0002C3B6 File Offset: 0x0002A5B6
			public bool Contains(object value)
			{
				return this.InnerList.Contains(this.CreateWeakRefObject(value));
			}

			// Token: 0x06000C85 RID: 3205 RVA: 0x0002C3CA File Offset: 0x0002A5CA
			public void RemoveAt(int index)
			{
				this.InnerList.RemoveAt(index);
			}

			// Token: 0x06000C86 RID: 3206 RVA: 0x0002C3D8 File Offset: 0x0002A5D8
			public void Remove(object value)
			{
				this.InnerList.Remove(this.CreateWeakRefObject(value));
			}

			// Token: 0x06000C87 RID: 3207 RVA: 0x0002C3EC File Offset: 0x0002A5EC
			public int IndexOf(object value)
			{
				return this.InnerList.IndexOf(this.CreateWeakRefObject(value));
			}

			// Token: 0x06000C88 RID: 3208 RVA: 0x0002C400 File Offset: 0x0002A600
			public void Insert(int index, object value)
			{
				this.InnerList.Insert(index, this.CreateWeakRefObject(value));
			}

			// Token: 0x06000C89 RID: 3209 RVA: 0x0002C415 File Offset: 0x0002A615
			public int Add(object value)
			{
				if (this.Count > this.RefCheckThreshold)
				{
					this.ScavengeReferences();
				}
				return this.InnerList.Add(this.CreateWeakRefObject(value));
			}

			// Token: 0x170003DA RID: 986
			// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0002C43D File Offset: 0x0002A63D
			public int Count
			{
				get
				{
					return this.InnerList.Count;
				}
			}

			// Token: 0x170003DB RID: 987
			// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0002C44A File Offset: 0x0002A64A
			object ICollection.SyncRoot
			{
				get
				{
					return this.InnerList.SyncRoot;
				}
			}

			// Token: 0x170003DC RID: 988
			// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0002C457 File Offset: 0x0002A657
			public bool IsReadOnly
			{
				get
				{
					return this.InnerList.IsReadOnly;
				}
			}

			// Token: 0x06000C8D RID: 3213 RVA: 0x0002C464 File Offset: 0x0002A664
			public void CopyTo(Array array, int index)
			{
				this.InnerList.CopyTo(array, index);
			}

			// Token: 0x170003DD RID: 989
			// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0002C473 File Offset: 0x0002A673
			bool ICollection.IsSynchronized
			{
				get
				{
					return this.InnerList.IsSynchronized;
				}
			}

			// Token: 0x06000C8F RID: 3215 RVA: 0x0002C480 File Offset: 0x0002A680
			public IEnumerator GetEnumerator()
			{
				return this.InnerList.GetEnumerator();
			}

			// Token: 0x04000AE7 RID: 2791
			private int refCheckThreshold = int.MaxValue;

			// Token: 0x04000AE8 RID: 2792
			private ArrayList _innerList;

			// Token: 0x02000133 RID: 307
			internal class WeakRefObject
			{
				// Token: 0x06000FAF RID: 4015 RVA: 0x0002E67C File Offset: 0x0002C87C
				internal WeakRefObject(object obj)
				{
					this.weakHolder = new WeakReference(obj);
					this.hash = obj.GetHashCode();
				}

				// Token: 0x17000401 RID: 1025
				// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0002E69C File Offset: 0x0002C89C
				internal bool IsAlive
				{
					get
					{
						return this.weakHolder.IsAlive;
					}
				}

				// Token: 0x17000402 RID: 1026
				// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0002E6A9 File Offset: 0x0002C8A9
				internal object Target
				{
					get
					{
						return this.weakHolder.Target;
					}
				}

				// Token: 0x06000FB2 RID: 4018 RVA: 0x0002E6B6 File Offset: 0x0002C8B6
				public override int GetHashCode()
				{
					return this.hash;
				}

				// Token: 0x06000FB3 RID: 4019 RVA: 0x0002E6C0 File Offset: 0x0002C8C0
				public override bool Equals(object obj)
				{
					ClientUtils.WeakRefCollection.WeakRefObject weakRefObject = obj as ClientUtils.WeakRefCollection.WeakRefObject;
					return weakRefObject == this || (weakRefObject != null && (weakRefObject.Target == this.Target || (this.Target != null && this.Target.Equals(weakRefObject.Target))));
				}

				// Token: 0x04000CE2 RID: 3298
				private int hash;

				// Token: 0x04000CE3 RID: 3299
				private WeakReference weakHolder;
			}
		}
	}
}
