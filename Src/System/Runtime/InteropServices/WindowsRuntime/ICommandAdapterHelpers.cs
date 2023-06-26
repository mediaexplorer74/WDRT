using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F7 RID: 1015
	internal static class ICommandAdapterHelpers
	{
		// Token: 0x06002640 RID: 9792 RVA: 0x000B085C File Offset: 0x000AEA5C
		internal static EventHandler<object> CreateWrapperHandler(EventHandler handler)
		{
			return delegate(object sender, object e)
			{
				EventArgs eventArgs = e as EventArgs;
				handler(sender, (eventArgs == null) ? EventArgs.Empty : eventArgs);
			};
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000B0884 File Offset: 0x000AEA84
		internal static EventHandler CreateWrapperHandler(EventHandler<object> handler)
		{
			return delegate(object sender, EventArgs e)
			{
				handler(sender, e);
			};
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x000B08AC File Offset: 0x000AEAAC
		internal static EventHandler<object> GetValueFromEquivalentKey(ConditionalWeakTable<EventHandler, EventHandler<object>> table, EventHandler key, ConditionalWeakTable<EventHandler, EventHandler<object>>.CreateValueCallback callback)
		{
			EventHandler<object> eventHandler;
			if (table.FindEquivalentKeyUnsafe(key, out eventHandler) == null)
			{
				eventHandler = callback(key);
				table.Add(key, eventHandler);
			}
			return eventHandler;
		}
	}
}
