using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007C RID: 124
	[NullableContext(1)]
	[Nullable(0)]
	public class ExpressionValueProvider : IValueProvider
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x0001BB81 File Offset: 0x00019D81
		public ExpressionValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, "memberInfo");
			this._memberInfo = memberInfo;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001BB9C File Offset: 0x00019D9C
		public void SetValue(object target, [Nullable(2)] object value)
		{
			try
			{
				if (this._setter == null)
				{
					this._setter = ExpressionReflectionDelegateFactory.Instance.CreateSet<object>(this._memberInfo);
				}
				this._setter(target, value);
			}
			catch (Exception ex)
			{
				throw new JsonSerializationException("Error setting value to '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), ex);
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001BC10 File Offset: 0x00019E10
		[return: Nullable(2)]
		public object GetValue(object target)
		{
			object obj;
			try
			{
				if (this._getter == null)
				{
					this._getter = ExpressionReflectionDelegateFactory.Instance.CreateGet<object>(this._memberInfo);
				}
				obj = this._getter(target);
			}
			catch (Exception ex)
			{
				throw new JsonSerializationException("Error getting value from '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, this._memberInfo.Name, target.GetType()), ex);
			}
			return obj;
		}

		// Token: 0x0400022E RID: 558
		private readonly MemberInfo _memberInfo;

		// Token: 0x0400022F RID: 559
		[Nullable(new byte[] { 2, 1, 2 })]
		private Func<object, object> _getter;

		// Token: 0x04000230 RID: 560
		[Nullable(new byte[] { 2, 1, 2 })]
		private Action<object, object> _setter;
	}
}
