using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009FF RID: 2559
	internal sealed class CLRIReferenceImpl<T> : CLRIPropertyValueImpl, IReference<T>, IPropertyValue, ICustomPropertyProvider
	{
		// Token: 0x06006547 RID: 25927 RVA: 0x0015A398 File Offset: 0x00158598
		public CLRIReferenceImpl(PropertyType type, T obj)
			: base(type, obj)
		{
			this._value = obj;
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06006548 RID: 25928 RVA: 0x0015A3AE File Offset: 0x001585AE
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06006549 RID: 25929 RVA: 0x0015A3B6 File Offset: 0x001585B6
		public override string ToString()
		{
			if (this._value != null)
			{
				return this._value.ToString();
			}
			return base.ToString();
		}

		// Token: 0x0600654A RID: 25930 RVA: 0x0015A3DD File Offset: 0x001585DD
		ICustomProperty ICustomPropertyProvider.GetCustomProperty(string name)
		{
			return ICustomPropertyProviderImpl.CreateProperty(this._value, name);
		}

		// Token: 0x0600654B RID: 25931 RVA: 0x0015A3F0 File Offset: 0x001585F0
		ICustomProperty ICustomPropertyProvider.GetIndexedProperty(string name, Type indexParameterType)
		{
			return ICustomPropertyProviderImpl.CreateIndexedProperty(this._value, name, indexParameterType);
		}

		// Token: 0x0600654C RID: 25932 RVA: 0x0015A404 File Offset: 0x00158604
		string ICustomPropertyProvider.GetStringRepresentation()
		{
			return this._value.ToString();
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x0600654D RID: 25933 RVA: 0x0015A416 File Offset: 0x00158616
		Type ICustomPropertyProvider.Type
		{
			get
			{
				return this._value.GetType();
			}
		}

		// Token: 0x0600654E RID: 25934 RVA: 0x0015A428 File Offset: 0x00158628
		[FriendAccessAllowed]
		internal static object UnboxHelper(object wrapper)
		{
			IReference<T> reference = (IReference<T>)wrapper;
			return reference.Value;
		}

		// Token: 0x04002D3C RID: 11580
		private T _value;
	}
}
