using System;
using System.Collections;

namespace System.Runtime.Serialization
{
	// Token: 0x02000756 RID: 1878
	internal static class SerializationEventsCache
	{
		// Token: 0x060052FF RID: 21247 RVA: 0x00124F34 File Offset: 0x00123134
		internal static SerializationEvents GetSerializationEventsForType(Type t)
		{
			SerializationEvents serializationEvents;
			if ((serializationEvents = (SerializationEvents)SerializationEventsCache.cache[t]) == null)
			{
				object syncRoot = SerializationEventsCache.cache.SyncRoot;
				lock (syncRoot)
				{
					if ((serializationEvents = (SerializationEvents)SerializationEventsCache.cache[t]) == null)
					{
						serializationEvents = new SerializationEvents(t);
						SerializationEventsCache.cache[t] = serializationEvents;
					}
				}
			}
			return serializationEvents;
		}

		// Token: 0x040024CC RID: 9420
		private static Hashtable cache = new Hashtable();
	}
}
