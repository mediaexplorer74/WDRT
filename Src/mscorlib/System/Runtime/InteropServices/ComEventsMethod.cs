using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009AC RID: 2476
	internal class ComEventsMethod
	{
		// Token: 0x06006331 RID: 25393 RVA: 0x0015348D File Offset: 0x0015168D
		internal ComEventsMethod(int dispid)
		{
			this._delegateWrappers = null;
			this._dispid = dispid;
		}

		// Token: 0x06006332 RID: 25394 RVA: 0x001534A3 File Offset: 0x001516A3
		internal static ComEventsMethod Find(ComEventsMethod methods, int dispid)
		{
			while (methods != null && methods._dispid != dispid)
			{
				methods = methods._next;
			}
			return methods;
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x001534BC File Offset: 0x001516BC
		internal static ComEventsMethod Add(ComEventsMethod methods, ComEventsMethod method)
		{
			method._next = methods;
			return method;
		}

		// Token: 0x06006334 RID: 25396 RVA: 0x001534C8 File Offset: 0x001516C8
		internal static ComEventsMethod Remove(ComEventsMethod methods, ComEventsMethod method)
		{
			if (methods == method)
			{
				methods = methods._next;
			}
			else
			{
				ComEventsMethod comEventsMethod = methods;
				while (comEventsMethod != null && comEventsMethod._next != method)
				{
					comEventsMethod = comEventsMethod._next;
				}
				if (comEventsMethod != null)
				{
					comEventsMethod._next = method._next;
				}
			}
			return methods;
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06006335 RID: 25397 RVA: 0x0015350A File Offset: 0x0015170A
		internal int DispId
		{
			get
			{
				return this._dispid;
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06006336 RID: 25398 RVA: 0x00153512 File Offset: 0x00151712
		internal bool Empty
		{
			get
			{
				return this._delegateWrappers == null || this._delegateWrappers.Length == 0;
			}
		}

		// Token: 0x06006337 RID: 25399 RVA: 0x00153528 File Offset: 0x00151728
		internal void AddDelegate(Delegate d)
		{
			int num = 0;
			if (this._delegateWrappers != null)
			{
				num = this._delegateWrappers.Length;
			}
			for (int i = 0; i < num; i++)
			{
				if (this._delegateWrappers[i].Delegate.GetType() == d.GetType())
				{
					this._delegateWrappers[i].Delegate = Delegate.Combine(this._delegateWrappers[i].Delegate, d);
					return;
				}
			}
			ComEventsMethod.DelegateWrapper[] array = new ComEventsMethod.DelegateWrapper[num + 1];
			if (num > 0)
			{
				this._delegateWrappers.CopyTo(array, 0);
			}
			ComEventsMethod.DelegateWrapper delegateWrapper = new ComEventsMethod.DelegateWrapper(d);
			array[num] = delegateWrapper;
			this._delegateWrappers = array;
		}

		// Token: 0x06006338 RID: 25400 RVA: 0x001535C0 File Offset: 0x001517C0
		internal void RemoveDelegate(Delegate d)
		{
			int num = this._delegateWrappers.Length;
			int num2 = -1;
			for (int i = 0; i < num; i++)
			{
				if (this._delegateWrappers[i].Delegate.GetType() == d.GetType())
				{
					num2 = i;
					break;
				}
			}
			if (num2 < 0)
			{
				return;
			}
			Delegate @delegate = Delegate.Remove(this._delegateWrappers[num2].Delegate, d);
			if (@delegate != null)
			{
				this._delegateWrappers[num2].Delegate = @delegate;
				return;
			}
			if (num == 1)
			{
				this._delegateWrappers = null;
				return;
			}
			ComEventsMethod.DelegateWrapper[] array = new ComEventsMethod.DelegateWrapper[num - 1];
			int j;
			for (j = 0; j < num2; j++)
			{
				array[j] = this._delegateWrappers[j];
			}
			while (j < num - 1)
			{
				array[j] = this._delegateWrappers[j + 1];
				j++;
			}
			this._delegateWrappers = array;
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x00153690 File Offset: 0x00151890
		internal object Invoke(object[] args)
		{
			object obj = null;
			ComEventsMethod.DelegateWrapper[] delegateWrappers = this._delegateWrappers;
			foreach (ComEventsMethod.DelegateWrapper delegateWrapper in delegateWrappers)
			{
				if (delegateWrapper != null && delegateWrapper.Delegate != null)
				{
					obj = delegateWrapper.Invoke(args);
				}
			}
			return obj;
		}

		// Token: 0x04002CBF RID: 11455
		private ComEventsMethod.DelegateWrapper[] _delegateWrappers;

		// Token: 0x04002CC0 RID: 11456
		private int _dispid;

		// Token: 0x04002CC1 RID: 11457
		private ComEventsMethod _next;

		// Token: 0x02000C96 RID: 3222
		internal class DelegateWrapper
		{
			// Token: 0x06007135 RID: 28981 RVA: 0x00186D84 File Offset: 0x00184F84
			public DelegateWrapper(Delegate d)
			{
				this._d = d;
			}

			// Token: 0x17001366 RID: 4966
			// (get) Token: 0x06007136 RID: 28982 RVA: 0x00186D93 File Offset: 0x00184F93
			// (set) Token: 0x06007137 RID: 28983 RVA: 0x00186D9B File Offset: 0x00184F9B
			public Delegate Delegate
			{
				get
				{
					return this._d;
				}
				set
				{
					this._d = value;
				}
			}

			// Token: 0x06007138 RID: 28984 RVA: 0x00186DA4 File Offset: 0x00184FA4
			public object Invoke(object[] args)
			{
				if (this._d == null)
				{
					return null;
				}
				if (!this._once)
				{
					this.PreProcessSignature();
					this._once = true;
				}
				if (this._cachedTargetTypes != null && this._expectedParamsCount == args.Length)
				{
					for (int i = 0; i < this._expectedParamsCount; i++)
					{
						if (this._cachedTargetTypes[i] != null)
						{
							args[i] = Enum.ToObject(this._cachedTargetTypes[i], args[i]);
						}
					}
				}
				return this._d.DynamicInvoke(args);
			}

			// Token: 0x06007139 RID: 28985 RVA: 0x00186E24 File Offset: 0x00185024
			private void PreProcessSignature()
			{
				ParameterInfo[] parameters = this._d.Method.GetParameters();
				this._expectedParamsCount = parameters.Length;
				Type[] array = new Type[this._expectedParamsCount];
				bool flag = false;
				for (int i = 0; i < this._expectedParamsCount; i++)
				{
					ParameterInfo parameterInfo = parameters[i];
					if (parameterInfo.ParameterType.IsByRef && parameterInfo.ParameterType.HasElementType && parameterInfo.ParameterType.GetElementType().IsEnum)
					{
						flag = true;
						array[i] = parameterInfo.ParameterType.GetElementType();
					}
				}
				if (flag)
				{
					this._cachedTargetTypes = array;
				}
			}

			// Token: 0x04003859 RID: 14425
			private Delegate _d;

			// Token: 0x0400385A RID: 14426
			private bool _once;

			// Token: 0x0400385B RID: 14427
			private int _expectedParamsCount;

			// Token: 0x0400385C RID: 14428
			private Type[] _cachedTargetTypes;
		}
	}
}
