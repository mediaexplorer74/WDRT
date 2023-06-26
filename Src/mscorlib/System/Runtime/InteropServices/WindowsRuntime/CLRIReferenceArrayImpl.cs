using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A00 RID: 2560
	internal sealed class CLRIReferenceArrayImpl<T> : CLRIPropertyValueImpl, IReferenceArray<T>, IPropertyValue, ICustomPropertyProvider, IList, ICollection, IEnumerable
	{
		// Token: 0x0600654F RID: 25935 RVA: 0x0015A447 File Offset: 0x00158647
		public CLRIReferenceArrayImpl(PropertyType type, T[] obj)
			: base(type, obj)
		{
			this._value = obj;
			this._list = this._value;
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x06006550 RID: 25936 RVA: 0x0015A464 File Offset: 0x00158664
		public T[] Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06006551 RID: 25937 RVA: 0x0015A46C File Offset: 0x0015866C
		public override string ToString()
		{
			if (this._value != null)
			{
				return this._value.ToString();
			}
			return base.ToString();
		}

		// Token: 0x06006552 RID: 25938 RVA: 0x0015A488 File Offset: 0x00158688
		ICustomProperty ICustomPropertyProvider.GetCustomProperty(string name)
		{
			return ICustomPropertyProviderImpl.CreateProperty(this._value, name);
		}

		// Token: 0x06006553 RID: 25939 RVA: 0x0015A496 File Offset: 0x00158696
		ICustomProperty ICustomPropertyProvider.GetIndexedProperty(string name, Type indexParameterType)
		{
			return ICustomPropertyProviderImpl.CreateIndexedProperty(this._value, name, indexParameterType);
		}

		// Token: 0x06006554 RID: 25940 RVA: 0x0015A4A5 File Offset: 0x001586A5
		string ICustomPropertyProvider.GetStringRepresentation()
		{
			return this._value.ToString();
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06006555 RID: 25941 RVA: 0x0015A4B2 File Offset: 0x001586B2
		Type ICustomPropertyProvider.Type
		{
			get
			{
				return this._value.GetType();
			}
		}

		// Token: 0x06006556 RID: 25942 RVA: 0x0015A4BF File Offset: 0x001586BF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._value.GetEnumerator();
		}

		// Token: 0x17001165 RID: 4453
		object IList.this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				this._list[index] = value;
			}
		}

		// Token: 0x06006559 RID: 25945 RVA: 0x0015A4E9 File Offset: 0x001586E9
		int IList.Add(object value)
		{
			return this._list.Add(value);
		}

		// Token: 0x0600655A RID: 25946 RVA: 0x0015A4F7 File Offset: 0x001586F7
		bool IList.Contains(object value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x0600655B RID: 25947 RVA: 0x0015A505 File Offset: 0x00158705
		void IList.Clear()
		{
			this._list.Clear();
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x0600655C RID: 25948 RVA: 0x0015A512 File Offset: 0x00158712
		bool IList.IsReadOnly
		{
			get
			{
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x0600655D RID: 25949 RVA: 0x0015A51F File Offset: 0x0015871F
		bool IList.IsFixedSize
		{
			get
			{
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x0600655E RID: 25950 RVA: 0x0015A52C File Offset: 0x0015872C
		int IList.IndexOf(object value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x0600655F RID: 25951 RVA: 0x0015A53A File Offset: 0x0015873A
		void IList.Insert(int index, object value)
		{
			this._list.Insert(index, value);
		}

		// Token: 0x06006560 RID: 25952 RVA: 0x0015A549 File Offset: 0x00158749
		void IList.Remove(object value)
		{
			this._list.Remove(value);
		}

		// Token: 0x06006561 RID: 25953 RVA: 0x0015A557 File Offset: 0x00158757
		void IList.RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		// Token: 0x06006562 RID: 25954 RVA: 0x0015A565 File Offset: 0x00158765
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06006563 RID: 25955 RVA: 0x0015A574 File Offset: 0x00158774
		int ICollection.Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x06006564 RID: 25956 RVA: 0x0015A581 File Offset: 0x00158781
		object ICollection.SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x06006565 RID: 25957 RVA: 0x0015A58E File Offset: 0x0015878E
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x06006566 RID: 25958 RVA: 0x0015A59C File Offset: 0x0015879C
		[FriendAccessAllowed]
		internal static object UnboxHelper(object wrapper)
		{
			IReferenceArray<T> referenceArray = (IReferenceArray<T>)wrapper;
			return referenceArray.Value;
		}

		// Token: 0x04002D3D RID: 11581
		private T[] _value;

		// Token: 0x04002D3E RID: 11582
		private IList _list;
	}
}
