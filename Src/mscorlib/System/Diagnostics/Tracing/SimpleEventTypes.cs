using System;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044F RID: 1103
	internal class SimpleEventTypes<T> : TraceLoggingEventTypes
	{
		// Token: 0x06003676 RID: 13942 RVA: 0x000D49CA File Offset: 0x000D2BCA
		private SimpleEventTypes(TraceLoggingTypeInfo<T> typeInfo)
			: base(typeInfo.Name, typeInfo.Tags, new TraceLoggingTypeInfo[] { typeInfo })
		{
			this.typeInfo = typeInfo;
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06003677 RID: 13943 RVA: 0x000D49EF File Offset: 0x000D2BEF
		public static SimpleEventTypes<T> Instance
		{
			get
			{
				return SimpleEventTypes<T>.instance ?? SimpleEventTypes<T>.InitInstance();
			}
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x000D4A00 File Offset: 0x000D2C00
		private static SimpleEventTypes<T> InitInstance()
		{
			SimpleEventTypes<T> simpleEventTypes = new SimpleEventTypes<T>(TraceLoggingTypeInfo<T>.Instance);
			Interlocked.CompareExchange<SimpleEventTypes<T>>(ref SimpleEventTypes<T>.instance, simpleEventTypes, null);
			return SimpleEventTypes<T>.instance;
		}

		// Token: 0x04001862 RID: 6242
		private static SimpleEventTypes<T> instance;

		// Token: 0x04001863 RID: 6243
		internal readonly TraceLoggingTypeInfo<T> typeInfo;
	}
}
