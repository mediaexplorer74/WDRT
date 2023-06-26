using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A0D RID: 2573
	internal class ICustomPropertyProviderProxy<T1, T2> : IGetProxyTarget, ICustomPropertyProvider, ICustomQueryInterface, IEnumerable, IBindableVector, IBindableIterable, IBindableVectorView
	{
		// Token: 0x060065A0 RID: 26016 RVA: 0x0015ACE0 File Offset: 0x00158EE0
		internal ICustomPropertyProviderProxy(object target, InterfaceForwardingSupport flags)
		{
			this._target = target;
			this._flags = flags;
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x0015ACF8 File Offset: 0x00158EF8
		internal static object CreateInstance(object target)
		{
			InterfaceForwardingSupport interfaceForwardingSupport = InterfaceForwardingSupport.None;
			if (target is IList)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IBindableVector;
			}
			if (target is IList<T1>)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IVector;
			}
			if (target is IBindableVectorView)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IBindableVectorView;
			}
			if (target is IReadOnlyList<T2>)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IVectorView;
			}
			if (target is IEnumerable)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IBindableIterableOrIIterable;
			}
			return new ICustomPropertyProviderProxy<T1, T2>(target, interfaceForwardingSupport);
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x0015AD4B File Offset: 0x00158F4B
		ICustomProperty ICustomPropertyProvider.GetCustomProperty(string name)
		{
			return ICustomPropertyProviderImpl.CreateProperty(this._target, name);
		}

		// Token: 0x060065A3 RID: 26019 RVA: 0x0015AD59 File Offset: 0x00158F59
		ICustomProperty ICustomPropertyProvider.GetIndexedProperty(string name, Type indexParameterType)
		{
			return ICustomPropertyProviderImpl.CreateIndexedProperty(this._target, name, indexParameterType);
		}

		// Token: 0x060065A4 RID: 26020 RVA: 0x0015AD68 File Offset: 0x00158F68
		string ICustomPropertyProvider.GetStringRepresentation()
		{
			return IStringableHelper.ToString(this._target);
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x060065A5 RID: 26021 RVA: 0x0015AD75 File Offset: 0x00158F75
		Type ICustomPropertyProvider.Type
		{
			get
			{
				return this._target.GetType();
			}
		}

		// Token: 0x060065A6 RID: 26022 RVA: 0x0015AD82 File Offset: 0x00158F82
		public override string ToString()
		{
			return IStringableHelper.ToString(this._target);
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x0015AD8F File Offset: 0x00158F8F
		object IGetProxyTarget.GetTarget()
		{
			return this._target;
		}

		// Token: 0x060065A8 RID: 26024 RVA: 0x0015AD98 File Offset: 0x00158F98
		[SecurityCritical]
		public CustomQueryInterfaceResult GetInterface([In] ref Guid iid, out IntPtr ppv)
		{
			ppv = IntPtr.Zero;
			if (iid == typeof(IBindableIterable).GUID && (this._flags & InterfaceForwardingSupport.IBindableIterableOrIIterable) == InterfaceForwardingSupport.None)
			{
				return CustomQueryInterfaceResult.Failed;
			}
			if (iid == typeof(IBindableVector).GUID && (this._flags & (InterfaceForwardingSupport.IBindableVector | InterfaceForwardingSupport.IVector)) == InterfaceForwardingSupport.None)
			{
				return CustomQueryInterfaceResult.Failed;
			}
			if (iid == typeof(IBindableVectorView).GUID && (this._flags & (InterfaceForwardingSupport.IBindableVectorView | InterfaceForwardingSupport.IVectorView)) == InterfaceForwardingSupport.None)
			{
				return CustomQueryInterfaceResult.Failed;
			}
			return CustomQueryInterfaceResult.NotHandled;
		}

		// Token: 0x060065A9 RID: 26025 RVA: 0x0015AE27 File Offset: 0x00159027
		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this._target).GetEnumerator();
		}

		// Token: 0x060065AA RID: 26026 RVA: 0x0015AE3C File Offset: 0x0015903C
		object IBindableVector.GetAt(uint index)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				return ibindableVectorNoThrow.GetAt(index);
			}
			return this.GetVectorOfT().GetAt(index);
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x060065AB RID: 26027 RVA: 0x0015AE6C File Offset: 0x0015906C
		uint IBindableVector.Size
		{
			get
			{
				IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
				if (ibindableVectorNoThrow != null)
				{
					return ibindableVectorNoThrow.Size;
				}
				return this.GetVectorOfT().Size;
			}
		}

		// Token: 0x060065AC RID: 26028 RVA: 0x0015AE98 File Offset: 0x00159098
		IBindableVectorView IBindableVector.GetView()
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				return ibindableVectorNoThrow.GetView();
			}
			return new ICustomPropertyProviderProxy<T1, T2>.IVectorViewToIBindableVectorViewAdapter<T1>(this.GetVectorOfT().GetView());
		}

		// Token: 0x060065AD RID: 26029 RVA: 0x0015AEC8 File Offset: 0x001590C8
		bool IBindableVector.IndexOf(object value, out uint index)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				return ibindableVectorNoThrow.IndexOf(value, out index);
			}
			return this.GetVectorOfT().IndexOf(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value), out index);
		}

		// Token: 0x060065AE RID: 26030 RVA: 0x0015AEFC File Offset: 0x001590FC
		void IBindableVector.SetAt(uint index, object value)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.SetAt(index, value);
				return;
			}
			this.GetVectorOfT().SetAt(index, ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value));
		}

		// Token: 0x060065AF RID: 26031 RVA: 0x0015AF30 File Offset: 0x00159130
		void IBindableVector.InsertAt(uint index, object value)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.InsertAt(index, value);
				return;
			}
			this.GetVectorOfT().InsertAt(index, ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value));
		}

		// Token: 0x060065B0 RID: 26032 RVA: 0x0015AF64 File Offset: 0x00159164
		void IBindableVector.RemoveAt(uint index)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.RemoveAt(index);
				return;
			}
			this.GetVectorOfT().RemoveAt(index);
		}

		// Token: 0x060065B1 RID: 26033 RVA: 0x0015AF90 File Offset: 0x00159190
		void IBindableVector.Append(object value)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.Append(value);
				return;
			}
			this.GetVectorOfT().Append(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value));
		}

		// Token: 0x060065B2 RID: 26034 RVA: 0x0015AFC0 File Offset: 0x001591C0
		void IBindableVector.RemoveAtEnd()
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.RemoveAtEnd();
				return;
			}
			this.GetVectorOfT().RemoveAtEnd();
		}

		// Token: 0x060065B3 RID: 26035 RVA: 0x0015AFEC File Offset: 0x001591EC
		void IBindableVector.Clear()
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.Clear();
				return;
			}
			this.GetVectorOfT().Clear();
		}

		// Token: 0x060065B4 RID: 26036 RVA: 0x0015B015 File Offset: 0x00159215
		[SecuritySafeCritical]
		private IBindableVector GetIBindableVectorNoThrow()
		{
			if ((this._flags & InterfaceForwardingSupport.IBindableVector) != InterfaceForwardingSupport.None)
			{
				return JitHelpers.UnsafeCast<IBindableVector>(this._target);
			}
			return null;
		}

		// Token: 0x060065B5 RID: 26037 RVA: 0x0015B02E File Offset: 0x0015922E
		[SecuritySafeCritical]
		private IVector_Raw<T1> GetVectorOfT()
		{
			if ((this._flags & InterfaceForwardingSupport.IVector) != InterfaceForwardingSupport.None)
			{
				return JitHelpers.UnsafeCast<IVector_Raw<T1>>(this._target);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060065B6 RID: 26038 RVA: 0x0015B04C File Offset: 0x0015924C
		object IBindableVectorView.GetAt(uint index)
		{
			IBindableVectorView ibindableVectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
			if (ibindableVectorViewNoThrow != null)
			{
				return ibindableVectorViewNoThrow.GetAt(index);
			}
			return this.GetVectorViewOfT().GetAt(index);
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x060065B7 RID: 26039 RVA: 0x0015B07C File Offset: 0x0015927C
		uint IBindableVectorView.Size
		{
			get
			{
				IBindableVectorView ibindableVectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
				if (ibindableVectorViewNoThrow != null)
				{
					return ibindableVectorViewNoThrow.Size;
				}
				return this.GetVectorViewOfT().Size;
			}
		}

		// Token: 0x060065B8 RID: 26040 RVA: 0x0015B0A8 File Offset: 0x001592A8
		bool IBindableVectorView.IndexOf(object value, out uint index)
		{
			IBindableVectorView ibindableVectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
			if (ibindableVectorViewNoThrow != null)
			{
				return ibindableVectorViewNoThrow.IndexOf(value, out index);
			}
			return this.GetVectorViewOfT().IndexOf(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T2>(value), out index);
		}

		// Token: 0x060065B9 RID: 26041 RVA: 0x0015B0DC File Offset: 0x001592DC
		IBindableIterator IBindableIterable.First()
		{
			IBindableVectorView ibindableVectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
			if (ibindableVectorViewNoThrow != null)
			{
				return ibindableVectorViewNoThrow.First();
			}
			return new ICustomPropertyProviderProxy<T1, T2>.IteratorOfTToIteratorAdapter<T2>(this.GetVectorViewOfT().First());
		}

		// Token: 0x060065BA RID: 26042 RVA: 0x0015B10A File Offset: 0x0015930A
		[SecuritySafeCritical]
		private IBindableVectorView GetIBindableVectorViewNoThrow()
		{
			if ((this._flags & InterfaceForwardingSupport.IBindableVectorView) != InterfaceForwardingSupport.None)
			{
				return JitHelpers.UnsafeCast<IBindableVectorView>(this._target);
			}
			return null;
		}

		// Token: 0x060065BB RID: 26043 RVA: 0x0015B123 File Offset: 0x00159323
		[SecuritySafeCritical]
		private IVectorView<T2> GetVectorViewOfT()
		{
			if ((this._flags & InterfaceForwardingSupport.IVectorView) != InterfaceForwardingSupport.None)
			{
				return JitHelpers.UnsafeCast<IVectorView<T2>>(this._target);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060065BC RID: 26044 RVA: 0x0015B140 File Offset: 0x00159340
		private static T ConvertTo<T>(object value)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
			return (T)((object)value);
		}

		// Token: 0x04002D49 RID: 11593
		private object _target;

		// Token: 0x04002D4A RID: 11594
		private InterfaceForwardingSupport _flags;

		// Token: 0x02000CA2 RID: 3234
		private sealed class IVectorViewToIBindableVectorViewAdapter<T> : IBindableVectorView, IBindableIterable
		{
			// Token: 0x06007163 RID: 29027 RVA: 0x001876AB File Offset: 0x001858AB
			public IVectorViewToIBindableVectorViewAdapter(IVectorView<T> vectorView)
			{
				this._vectorView = vectorView;
			}

			// Token: 0x06007164 RID: 29028 RVA: 0x001876BA File Offset: 0x001858BA
			object IBindableVectorView.GetAt(uint index)
			{
				return this._vectorView.GetAt(index);
			}

			// Token: 0x1700136C RID: 4972
			// (get) Token: 0x06007165 RID: 29029 RVA: 0x001876CD File Offset: 0x001858CD
			uint IBindableVectorView.Size
			{
				get
				{
					return this._vectorView.Size;
				}
			}

			// Token: 0x06007166 RID: 29030 RVA: 0x001876DA File Offset: 0x001858DA
			bool IBindableVectorView.IndexOf(object value, out uint index)
			{
				return this._vectorView.IndexOf(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T>(value), out index);
			}

			// Token: 0x06007167 RID: 29031 RVA: 0x001876EE File Offset: 0x001858EE
			IBindableIterator IBindableIterable.First()
			{
				return new ICustomPropertyProviderProxy<T1, T2>.IteratorOfTToIteratorAdapter<T>(this._vectorView.First());
			}

			// Token: 0x04003886 RID: 14470
			private IVectorView<T> _vectorView;
		}

		// Token: 0x02000CA3 RID: 3235
		private sealed class IteratorOfTToIteratorAdapter<T> : IBindableIterator
		{
			// Token: 0x06007168 RID: 29032 RVA: 0x00187700 File Offset: 0x00185900
			public IteratorOfTToIteratorAdapter(IIterator<T> iterator)
			{
				this._iterator = iterator;
			}

			// Token: 0x1700136D RID: 4973
			// (get) Token: 0x06007169 RID: 29033 RVA: 0x0018770F File Offset: 0x0018590F
			public bool HasCurrent
			{
				get
				{
					return this._iterator.HasCurrent;
				}
			}

			// Token: 0x1700136E RID: 4974
			// (get) Token: 0x0600716A RID: 29034 RVA: 0x0018771C File Offset: 0x0018591C
			public object Current
			{
				get
				{
					return this._iterator.Current;
				}
			}

			// Token: 0x0600716B RID: 29035 RVA: 0x0018772E File Offset: 0x0018592E
			public bool MoveNext()
			{
				return this._iterator.MoveNext();
			}

			// Token: 0x04003887 RID: 14471
			private IIterator<T> _iterator;
		}
	}
}
