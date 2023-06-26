using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000753 RID: 1875
	[Serializable]
	internal sealed class SafeSerializationManager : IObjectReference, ISerializable
	{
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060052E9 RID: 21225 RVA: 0x001247D0 File Offset: 0x001229D0
		// (remove) Token: 0x060052EA RID: 21226 RVA: 0x00124808 File Offset: 0x00122A08
		internal event EventHandler<SafeSerializationEventArgs> SerializeObjectState;

		// Token: 0x060052EB RID: 21227 RVA: 0x0012483D File Offset: 0x00122A3D
		internal SafeSerializationManager()
		{
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x00124848 File Offset: 0x00122A48
		[SecurityCritical]
		private SafeSerializationManager(SerializationInfo info, StreamingContext context)
		{
			RuntimeType runtimeType = info.GetValueNoThrow("CLR_SafeSerializationManager_RealType", typeof(RuntimeType)) as RuntimeType;
			if (runtimeType == null)
			{
				this.m_serializedStates = info.GetValue("m_serializedStates", typeof(List<object>)) as List<object>;
				return;
			}
			this.m_realType = runtimeType;
			this.m_savedSerializationInfo = info;
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x060052ED RID: 21229 RVA: 0x001248AE File Offset: 0x00122AAE
		internal bool IsActive
		{
			get
			{
				return this.SerializeObjectState != null;
			}
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x001248BC File Offset: 0x00122ABC
		[SecurityCritical]
		internal void CompleteSerialization(object serializedObject, SerializationInfo info, StreamingContext context)
		{
			this.m_serializedStates = null;
			EventHandler<SafeSerializationEventArgs> serializeObjectState = this.SerializeObjectState;
			if (serializeObjectState != null)
			{
				SafeSerializationEventArgs safeSerializationEventArgs = new SafeSerializationEventArgs(context);
				serializeObjectState(serializedObject, safeSerializationEventArgs);
				this.m_serializedStates = safeSerializationEventArgs.SerializedStates;
				info.AddValue("CLR_SafeSerializationManager_RealType", serializedObject.GetType(), typeof(RuntimeType));
				info.SetType(typeof(SafeSerializationManager));
			}
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x00124920 File Offset: 0x00122B20
		internal void CompleteDeserialization(object deserializedObject)
		{
			if (this.m_serializedStates != null)
			{
				foreach (object obj in this.m_serializedStates)
				{
					ISafeSerializationData safeSerializationData = (ISafeSerializationData)obj;
					safeSerializationData.CompleteDeserialization(deserializedObject);
				}
			}
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x0012497C File Offset: 0x00122B7C
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_serializedStates", this.m_serializedStates, typeof(List<IDeserializationCallback>));
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x0012499C File Offset: 0x00122B9C
		[SecurityCritical]
		object IObjectReference.GetRealObject(StreamingContext context)
		{
			if (this.m_realObject != null)
			{
				return this.m_realObject;
			}
			if (this.m_realType == null)
			{
				return this;
			}
			Stack stack = new Stack();
			RuntimeType runtimeType = this.m_realType;
			do
			{
				stack.Push(runtimeType);
				runtimeType = runtimeType.BaseType as RuntimeType;
			}
			while (runtimeType != typeof(object));
			RuntimeType runtimeType2;
			RuntimeConstructorInfo runtimeConstructorInfo;
			do
			{
				runtimeType2 = runtimeType;
				runtimeType = stack.Pop() as RuntimeType;
				runtimeConstructorInfo = runtimeType.GetSerializationCtor();
			}
			while (runtimeConstructorInfo != null && runtimeConstructorInfo.IsSecurityCritical);
			runtimeConstructorInfo = ObjectManager.GetConstructor(runtimeType2);
			object uninitializedObject = FormatterServices.GetUninitializedObject(this.m_realType);
			runtimeConstructorInfo.SerializationInvoke(uninitializedObject, this.m_savedSerializationInfo, context);
			this.m_savedSerializationInfo = null;
			this.m_realType = null;
			this.m_realObject = uninitializedObject;
			return uninitializedObject;
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x00124A60 File Offset: 0x00122C60
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.m_realObject != null)
			{
				SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(this.m_realObject.GetType());
				serializationEventsForType.InvokeOnDeserialized(this.m_realObject, context);
				this.m_realObject = null;
			}
		}

		// Token: 0x040024BF RID: 9407
		private IList<object> m_serializedStates;

		// Token: 0x040024C0 RID: 9408
		private SerializationInfo m_savedSerializationInfo;

		// Token: 0x040024C1 RID: 9409
		private object m_realObject;

		// Token: 0x040024C2 RID: 9410
		private RuntimeType m_realType;

		// Token: 0x040024C4 RID: 9412
		private const string RealTypeSerializationName = "CLR_SafeSerializationManager_RealType";
	}
}
