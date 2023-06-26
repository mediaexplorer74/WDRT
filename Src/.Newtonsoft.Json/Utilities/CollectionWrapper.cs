using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000045 RID: 69
	[NullableContext(1)]
	[Nullable(0)]
	internal class CollectionWrapper<[Nullable(2)] T> : ICollection<T>, IEnumerable<T>, IEnumerable, IWrappedCollection, IList, ICollection
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x00010AD8 File Offset: 0x0000ECD8
		public CollectionWrapper(IList list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			ICollection<T> collection = list as ICollection<T>;
			if (collection != null)
			{
				this._genericCollection = collection;
				return;
			}
			this._list = list;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00010B0F File Offset: 0x0000ED0F
		public CollectionWrapper(ICollection<T> list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			this._genericCollection = list;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00010B29 File Offset: 0x0000ED29
		public virtual void Add(T item)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Add(item);
				return;
			}
			this._list.Add(item);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00010B52 File Offset: 0x0000ED52
		public virtual void Clear()
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.Clear();
				return;
			}
			this._list.Clear();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00010B73 File Offset: 0x0000ED73
		public virtual bool Contains(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Contains(item);
			}
			return this._list.Contains(item);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00010B9B File Offset: 0x0000ED9B
		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			if (this._genericCollection != null)
			{
				this._genericCollection.CopyTo(array, arrayIndex);
				return;
			}
			this._list.CopyTo(array, arrayIndex);
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00010BC0 File Offset: 0x0000EDC0
		public virtual int Count
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.Count;
				}
				return this._list.Count;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00010BE1 File Offset: 0x0000EDE1
		public virtual bool IsReadOnly
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.IsReadOnly;
				}
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00010C02 File Offset: 0x0000EE02
		public virtual bool Remove(T item)
		{
			if (this._genericCollection != null)
			{
				return this._genericCollection.Remove(item);
			}
			bool flag = this._list.Contains(item);
			if (flag)
			{
				this._list.Remove(item);
			}
			return flag;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00010C40 File Offset: 0x0000EE40
		public virtual IEnumerator<T> GetEnumerator()
		{
			IEnumerable<T> genericCollection = this._genericCollection;
			return (genericCollection ?? this._list.Cast<T>()).GetEnumerator();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00010C6C File Offset: 0x0000EE6C
		IEnumerator IEnumerable.GetEnumerator()
		{
			IEnumerable genericCollection = this._genericCollection;
			return (genericCollection ?? this._list).GetEnumerator();
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00010C90 File Offset: 0x0000EE90
		int IList.Add(object value)
		{
			CollectionWrapper<T>.VerifyValueType(value);
			this.Add((T)((object)value));
			return this.Count - 1;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00010CAC File Offset: 0x0000EEAC
		bool IList.Contains(object value)
		{
			return CollectionWrapper<T>.IsCompatibleObject(value) && this.Contains((T)((object)value));
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00010CC4 File Offset: 0x0000EEC4
		int IList.IndexOf(object value)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support IndexOf.");
			}
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				return this._list.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00010CF9 File Offset: 0x0000EEF9
		void IList.RemoveAt(int index)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support RemoveAt.");
			}
			this._list.RemoveAt(index);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00010D1A File Offset: 0x0000EF1A
		void IList.Insert(int index, object value)
		{
			if (this._genericCollection != null)
			{
				throw new InvalidOperationException("Wrapped ICollection<T> does not support Insert.");
			}
			CollectionWrapper<T>.VerifyValueType(value);
			this._list.Insert(index, (T)((object)value));
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00010D4C File Offset: 0x0000EF4C
		bool IList.IsFixedSize
		{
			get
			{
				if (this._genericCollection != null)
				{
					return this._genericCollection.IsReadOnly;
				}
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00010D6D File Offset: 0x0000EF6D
		void IList.Remove(object value)
		{
			if (CollectionWrapper<T>.IsCompatibleObject(value))
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x170000A8 RID: 168
		object IList.this[int index]
		{
			get
			{
				if (this._genericCollection != null)
				{
					throw new InvalidOperationException("Wrapped ICollection<T> does not support indexer.");
				}
				return this._list[index];
			}
			set
			{
				if (this._genericCollection != null)
				{
					throw new InvalidOperationException("Wrapped ICollection<T> does not support indexer.");
				}
				CollectionWrapper<T>.VerifyValueType(value);
				this._list[index] = (T)((object)value);
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00010DD7 File Offset: 0x0000EFD7
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			this.CopyTo((T[])array, arrayIndex);
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00010DE6 File Offset: 0x0000EFE6
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00010DE9 File Offset: 0x0000EFE9
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00010E0B File Offset: 0x0000F00B
		private static void VerifyValueType(object value)
		{
			if (!CollectionWrapper<T>.IsCompatibleObject(value))
			{
				throw new ArgumentException("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.".FormatWith(CultureInfo.InvariantCulture, value, typeof(T)), "value");
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00010E3A File Offset: 0x0000F03A
		private static bool IsCompatibleObject(object value)
		{
			return value is T || (value == null && (!typeof(T).IsValueType() || ReflectionUtils.IsNullableType(typeof(T))));
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00010E6C File Offset: 0x0000F06C
		public object UnderlyingCollection
		{
			get
			{
				return this._genericCollection ?? this._list;
			}
		}

		// Token: 0x04000158 RID: 344
		[Nullable(2)]
		private readonly IList _list;

		// Token: 0x04000159 RID: 345
		[Nullable(new byte[] { 2, 1 })]
		private readonly ICollection<T> _genericCollection;

		// Token: 0x0400015A RID: 346
		[Nullable(2)]
		private object _syncRoot;
	}
}
