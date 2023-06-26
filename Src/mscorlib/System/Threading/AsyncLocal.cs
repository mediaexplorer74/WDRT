using System;
using System.Security;

namespace System.Threading
{
	/// <summary>Represents ambient data that is local to a given asynchronous control flow, such as an asynchronous method.</summary>
	/// <typeparam name="T">The type of the ambient data.</typeparam>
	// Token: 0x020004E3 RID: 1251
	[__DynamicallyInvokable]
	public sealed class AsyncLocal<T> : IAsyncLocal
	{
		/// <summary>Instantiates an <see cref="T:System.Threading.AsyncLocal`1" /> instance that does not receive change notifications.</summary>
		// Token: 0x06003B8F RID: 15247 RVA: 0x000E3BCB File Offset: 0x000E1DCB
		[__DynamicallyInvokable]
		public AsyncLocal()
		{
		}

		/// <summary>Instantiates an <see cref="T:System.Threading.AsyncLocal`1" /> local instance that receives change notifications.</summary>
		/// <param name="valueChangedHandler">The delegate that is called whenever the current value changes on any thread.</param>
		// Token: 0x06003B90 RID: 15248 RVA: 0x000E3BD3 File Offset: 0x000E1DD3
		[SecurityCritical]
		[__DynamicallyInvokable]
		public AsyncLocal(Action<AsyncLocalValueChangedArgs<T>> valueChangedHandler)
		{
			this.m_valueChangedHandler = valueChangedHandler;
		}

		/// <summary>Gets or sets the value of the ambient data.</summary>
		/// <returns>The value of the ambient data. If no value has been set, the returned value is default(T).</returns>
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06003B91 RID: 15249 RVA: 0x000E3BE4 File Offset: 0x000E1DE4
		// (set) Token: 0x06003B92 RID: 15250 RVA: 0x000E3C0B File Offset: 0x000E1E0B
		[__DynamicallyInvokable]
		public T Value
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				object localValue = ExecutionContext.GetLocalValue(this);
				if (localValue != null)
				{
					return (T)((object)localValue);
				}
				return default(T);
			}
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			set
			{
				ExecutionContext.SetLocalValue(this, value, this.m_valueChangedHandler != null);
			}
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x000E3C24 File Offset: 0x000E1E24
		[SecurityCritical]
		void IAsyncLocal.OnValueChanged(object previousValueObj, object currentValueObj, bool contextChanged)
		{
			T t = ((previousValueObj == null) ? default(T) : ((T)((object)previousValueObj)));
			T t2 = ((currentValueObj == null) ? default(T) : ((T)((object)currentValueObj)));
			this.m_valueChangedHandler(new AsyncLocalValueChangedArgs<T>(t, t2, contextChanged));
		}

		// Token: 0x0400196E RID: 6510
		[SecurityCritical]
		private readonly Action<AsyncLocalValueChangedArgs<T>> m_valueChangedHandler;
	}
}
