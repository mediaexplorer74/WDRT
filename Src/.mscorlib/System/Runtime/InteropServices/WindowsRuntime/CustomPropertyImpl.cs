using System;
using System.Globalization;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A0F RID: 2575
	internal sealed class CustomPropertyImpl : ICustomProperty
	{
		// Token: 0x060065C5 RID: 26053 RVA: 0x0015B150 File Offset: 0x00159350
		public CustomPropertyImpl(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw new ArgumentNullException("propertyInfo");
			}
			this.m_property = propertyInfo;
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x060065C6 RID: 26054 RVA: 0x0015B173 File Offset: 0x00159373
		public string Name
		{
			get
			{
				return this.m_property.Name;
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x060065C7 RID: 26055 RVA: 0x0015B180 File Offset: 0x00159380
		public bool CanRead
		{
			get
			{
				return this.m_property.GetGetMethod() != null;
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x060065C8 RID: 26056 RVA: 0x0015B193 File Offset: 0x00159393
		public bool CanWrite
		{
			get
			{
				return this.m_property.GetSetMethod() != null;
			}
		}

		// Token: 0x060065C9 RID: 26057 RVA: 0x0015B1A6 File Offset: 0x001593A6
		public object GetValue(object target)
		{
			return this.InvokeInternal(target, null, true);
		}

		// Token: 0x060065CA RID: 26058 RVA: 0x0015B1B1 File Offset: 0x001593B1
		public object GetValue(object target, object indexValue)
		{
			return this.InvokeInternal(target, new object[] { indexValue }, true);
		}

		// Token: 0x060065CB RID: 26059 RVA: 0x0015B1C5 File Offset: 0x001593C5
		public void SetValue(object target, object value)
		{
			this.InvokeInternal(target, new object[] { value }, false);
		}

		// Token: 0x060065CC RID: 26060 RVA: 0x0015B1DA File Offset: 0x001593DA
		public void SetValue(object target, object value, object indexValue)
		{
			this.InvokeInternal(target, new object[] { indexValue, value }, false);
		}

		// Token: 0x060065CD RID: 26061 RVA: 0x0015B1F4 File Offset: 0x001593F4
		[SecuritySafeCritical]
		private object InvokeInternal(object target, object[] args, bool getValue)
		{
			IGetProxyTarget getProxyTarget = target as IGetProxyTarget;
			if (getProxyTarget != null)
			{
				target = getProxyTarget.GetTarget();
			}
			MethodInfo methodInfo = (getValue ? this.m_property.GetGetMethod(true) : this.m_property.GetSetMethod(true));
			if (methodInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString(getValue ? "Arg_GetMethNotFnd" : "Arg_SetMethNotFnd"));
			}
			if (!methodInfo.IsPublic)
			{
				throw new MethodAccessException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Arg_MethodAccessException_WithMethodName"), methodInfo.ToString(), methodInfo.DeclaringType.FullName));
			}
			RuntimeMethodInfo runtimeMethodInfo = methodInfo as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
			}
			return runtimeMethodInfo.UnsafeInvoke(target, BindingFlags.Default, null, args, null);
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x060065CE RID: 26062 RVA: 0x0015B2B2 File Offset: 0x001594B2
		public Type Type
		{
			get
			{
				return this.m_property.PropertyType;
			}
		}

		// Token: 0x04002D4B RID: 11595
		private PropertyInfo m_property;
	}
}
