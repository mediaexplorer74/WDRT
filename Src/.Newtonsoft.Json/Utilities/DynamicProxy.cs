using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200004F RID: 79
	[NullableContext(1)]
	[Nullable(0)]
	internal class DynamicProxy<[Nullable(2)] T>
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x00013E5A File Offset: 0x0001205A
		public virtual IEnumerable<string> GetDynamicMemberNames(T instance)
		{
			return CollectionUtils.ArrayEmpty<string>();
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00013E61 File Offset: 0x00012061
		public virtual bool TryBinaryOperation(T instance, BinaryOperationBinder binder, object arg, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00013E68 File Offset: 0x00012068
		public virtual bool TryConvert(T instance, ConvertBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00013E6E File Offset: 0x0001206E
		public virtual bool TryCreateInstance(T instance, CreateInstanceBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00013E75 File Offset: 0x00012075
		public virtual bool TryDeleteIndex(T instance, DeleteIndexBinder binder, object[] indexes)
		{
			return false;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00013E78 File Offset: 0x00012078
		public virtual bool TryDeleteMember(T instance, DeleteMemberBinder binder)
		{
			return false;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00013E7B File Offset: 0x0001207B
		public virtual bool TryGetIndex(T instance, GetIndexBinder binder, object[] indexes, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00013E82 File Offset: 0x00012082
		public virtual bool TryGetMember(T instance, GetMemberBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00013E88 File Offset: 0x00012088
		public virtual bool TryInvoke(T instance, InvokeBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00013E8F File Offset: 0x0001208F
		public virtual bool TryInvokeMember(T instance, InvokeMemberBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00013E96 File Offset: 0x00012096
		public virtual bool TrySetIndex(T instance, SetIndexBinder binder, object[] indexes, object value)
		{
			return false;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00013E99 File Offset: 0x00012099
		public virtual bool TrySetMember(T instance, SetMemberBinder binder, object value)
		{
			return false;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00013E9C File Offset: 0x0001209C
		public virtual bool TryUnaryOperation(T instance, UnaryOperationBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}
	}
}
