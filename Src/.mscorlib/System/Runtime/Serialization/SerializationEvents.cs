using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000755 RID: 1877
	internal class SerializationEvents
	{
		// Token: 0x060052F7 RID: 21239 RVA: 0x00124B5C File Offset: 0x00122D5C
		private List<MethodInfo> GetMethodsWithAttribute(Type attribute, Type t)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			Type type = t;
			while (type != null && type != typeof(object))
			{
				RuntimeType runtimeType = (RuntimeType)type;
				MethodInfo[] methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (MethodInfo methodInfo in methods)
				{
					if (methodInfo.IsDefined(attribute, false))
					{
						list.Add(methodInfo);
					}
				}
				type = type.BaseType;
			}
			list.Reverse();
			if (list.Count != 0)
			{
				return list;
			}
			return null;
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x00124BE8 File Offset: 0x00122DE8
		internal SerializationEvents(Type t)
		{
			this.m_OnSerializingMethods = this.GetMethodsWithAttribute(typeof(OnSerializingAttribute), t);
			this.m_OnSerializedMethods = this.GetMethodsWithAttribute(typeof(OnSerializedAttribute), t);
			this.m_OnDeserializingMethods = this.GetMethodsWithAttribute(typeof(OnDeserializingAttribute), t);
			this.m_OnDeserializedMethods = this.GetMethodsWithAttribute(typeof(OnDeserializedAttribute), t);
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x060052F9 RID: 21241 RVA: 0x00124C57 File Offset: 0x00122E57
		internal bool HasOnSerializingEvents
		{
			get
			{
				return this.m_OnSerializingMethods != null || this.m_OnSerializedMethods != null;
			}
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x00124C6C File Offset: 0x00122E6C
		[SecuritySafeCritical]
		internal void InvokeOnSerializing(object obj, StreamingContext context)
		{
			if (this.m_OnSerializingMethods != null)
			{
				object[] array = new object[] { context };
				SerializationEventHandler serializationEventHandler = null;
				foreach (MethodInfo methodInfo in this.m_OnSerializingMethods)
				{
					SerializationEventHandler serializationEventHandler2 = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					serializationEventHandler = (SerializationEventHandler)Delegate.Combine(serializationEventHandler, serializationEventHandler2);
				}
				serializationEventHandler(context);
			}
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x00124D04 File Offset: 0x00122F04
		[SecuritySafeCritical]
		internal void InvokeOnDeserializing(object obj, StreamingContext context)
		{
			if (this.m_OnDeserializingMethods != null)
			{
				object[] array = new object[] { context };
				SerializationEventHandler serializationEventHandler = null;
				foreach (MethodInfo methodInfo in this.m_OnDeserializingMethods)
				{
					SerializationEventHandler serializationEventHandler2 = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					serializationEventHandler = (SerializationEventHandler)Delegate.Combine(serializationEventHandler, serializationEventHandler2);
				}
				serializationEventHandler(context);
			}
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x00124D9C File Offset: 0x00122F9C
		[SecuritySafeCritical]
		internal void InvokeOnDeserialized(object obj, StreamingContext context)
		{
			if (this.m_OnDeserializedMethods != null)
			{
				object[] array = new object[] { context };
				SerializationEventHandler serializationEventHandler = null;
				foreach (MethodInfo methodInfo in this.m_OnDeserializedMethods)
				{
					SerializationEventHandler serializationEventHandler2 = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					serializationEventHandler = (SerializationEventHandler)Delegate.Combine(serializationEventHandler, serializationEventHandler2);
				}
				serializationEventHandler(context);
			}
		}

		// Token: 0x060052FD RID: 21245 RVA: 0x00124E34 File Offset: 0x00123034
		[SecurityCritical]
		internal SerializationEventHandler AddOnSerialized(object obj, SerializationEventHandler handler)
		{
			if (this.m_OnSerializedMethods != null)
			{
				foreach (MethodInfo methodInfo in this.m_OnSerializedMethods)
				{
					SerializationEventHandler serializationEventHandler = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					handler = (SerializationEventHandler)Delegate.Combine(handler, serializationEventHandler);
				}
			}
			return handler;
		}

		// Token: 0x060052FE RID: 21246 RVA: 0x00124EB4 File Offset: 0x001230B4
		[SecurityCritical]
		internal SerializationEventHandler AddOnDeserialized(object obj, SerializationEventHandler handler)
		{
			if (this.m_OnDeserializedMethods != null)
			{
				foreach (MethodInfo methodInfo in this.m_OnDeserializedMethods)
				{
					SerializationEventHandler serializationEventHandler = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, methodInfo);
					handler = (SerializationEventHandler)Delegate.Combine(handler, serializationEventHandler);
				}
			}
			return handler;
		}

		// Token: 0x040024C8 RID: 9416
		private List<MethodInfo> m_OnSerializingMethods;

		// Token: 0x040024C9 RID: 9417
		private List<MethodInfo> m_OnSerializedMethods;

		// Token: 0x040024CA RID: 9418
		private List<MethodInfo> m_OnDeserializingMethods;

		// Token: 0x040024CB RID: 9419
		private List<MethodInfo> m_OnDeserializedMethods;
	}
}
