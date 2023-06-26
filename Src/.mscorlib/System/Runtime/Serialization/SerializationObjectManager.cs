using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Serialization
{
	/// <summary>Manages serialization processes at run time. This class cannot be inherited.</summary>
	// Token: 0x02000754 RID: 1876
	public sealed class SerializationObjectManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationObjectManager" /> class.</summary>
		/// <param name="context">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class that contains information about the current serialization operation.</param>
		// Token: 0x060052F3 RID: 21235 RVA: 0x00124A9A File Offset: 0x00122C9A
		public SerializationObjectManager(StreamingContext context)
		{
			this.m_context = context;
			this.m_objectSeenTable = new Hashtable();
		}

		/// <summary>Registers the object upon which events will be raised.</summary>
		/// <param name="obj">The object to register.</param>
		// Token: 0x060052F4 RID: 21236 RVA: 0x00124AC0 File Offset: 0x00122CC0
		[SecurityCritical]
		public void RegisterObject(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			if (serializationEventsForType.HasOnSerializingEvents && this.m_objectSeenTable[obj] == null)
			{
				this.m_objectSeenTable[obj] = true;
				serializationEventsForType.InvokeOnSerializing(obj, this.m_context);
				this.AddOnSerialized(obj);
			}
		}

		/// <summary>Invokes the OnSerializing callback event if the type of the object has one; and registers the object for raising the OnSerialized event if the type of the object has one.</summary>
		// Token: 0x060052F5 RID: 21237 RVA: 0x00124B15 File Offset: 0x00122D15
		public void RaiseOnSerializedEvent()
		{
			if (this.m_onSerializedHandler != null)
			{
				this.m_onSerializedHandler(this.m_context);
			}
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x00124B30 File Offset: 0x00122D30
		[SecuritySafeCritical]
		private void AddOnSerialized(object obj)
		{
			SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(obj.GetType());
			this.m_onSerializedHandler = serializationEventsForType.AddOnSerialized(obj, this.m_onSerializedHandler);
		}

		// Token: 0x040024C5 RID: 9413
		private Hashtable m_objectSeenTable = new Hashtable();

		// Token: 0x040024C6 RID: 9414
		private SerializationEventHandler m_onSerializedHandler;

		// Token: 0x040024C7 RID: 9415
		private StreamingContext m_context;
	}
}
