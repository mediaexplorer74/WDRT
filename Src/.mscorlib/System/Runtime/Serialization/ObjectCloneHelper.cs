﻿using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000747 RID: 1863
	internal static class ObjectCloneHelper
	{
		// Token: 0x06005272 RID: 21106 RVA: 0x00122740 File Offset: 0x00120940
		[SecurityCritical]
		internal static object GetObjectData(object serObj, out string typeName, out string assemName, out string[] fieldNames, out object[] fieldValues)
		{
			object obj = null;
			Type type;
			if (RemotingServices.IsTransparentProxy(serObj))
			{
				type = typeof(MarshalByRefObject);
			}
			else
			{
				type = serObj.GetType();
			}
			SerializationInfo serializationInfo = new SerializationInfo(type, ObjectCloneHelper.s_converter);
			if (serObj is ObjRef)
			{
				ObjectCloneHelper.s_ObjRefRemotingSurrogate.GetObjectData(serObj, serializationInfo, ObjectCloneHelper.s_cloneContext);
			}
			else if (RemotingServices.IsTransparentProxy(serObj) || serObj is MarshalByRefObject)
			{
				if (!RemotingServices.IsTransparentProxy(serObj) || RemotingServices.GetRealProxy(serObj) is RemotingProxy)
				{
					ObjRef objRef = RemotingServices.MarshalInternal((MarshalByRefObject)serObj, null, null);
					if (objRef.CanSmuggle())
					{
						if (RemotingServices.IsTransparentProxy(serObj))
						{
							RealProxy realProxy = RemotingServices.GetRealProxy(serObj);
							objRef.SetServerIdentity(realProxy._srvIdentity);
							objRef.SetDomainID(realProxy._domainID);
						}
						else
						{
							ServerIdentity serverIdentity = (ServerIdentity)MarshalByRefObject.GetIdentity((MarshalByRefObject)serObj);
							serverIdentity.SetHandle();
							objRef.SetServerIdentity(serverIdentity.GetHandle());
							objRef.SetDomainID(AppDomain.CurrentDomain.GetId());
						}
						objRef.SetMarshaledObject();
						obj = objRef;
					}
				}
				if (obj == null)
				{
					ObjectCloneHelper.s_RemotingSurrogate.GetObjectData(serObj, serializationInfo, ObjectCloneHelper.s_cloneContext);
				}
			}
			else
			{
				if (!(serObj is ISerializable))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_SerializationException"));
				}
				((ISerializable)serObj).GetObjectData(serializationInfo, ObjectCloneHelper.s_cloneContext);
			}
			if (obj == null)
			{
				typeName = serializationInfo.FullTypeName;
				assemName = serializationInfo.AssemblyName;
				fieldNames = serializationInfo.MemberNames;
				fieldValues = serializationInfo.MemberValues;
			}
			else
			{
				typeName = null;
				assemName = null;
				fieldNames = null;
				fieldValues = null;
			}
			return obj;
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x001228B8 File Offset: 0x00120AB8
		[SecurityCritical]
		internal static SerializationInfo PrepareConstructorArgs(object serObj, string[] fieldNames, object[] fieldValues, out StreamingContext context)
		{
			SerializationInfo serializationInfo = null;
			if (serObj is ISerializable)
			{
				serializationInfo = new SerializationInfo(serObj.GetType(), ObjectCloneHelper.s_converter);
				for (int i = 0; i < fieldNames.Length; i++)
				{
					if (fieldNames[i] != null)
					{
						serializationInfo.AddValue(fieldNames[i], fieldValues[i]);
					}
				}
			}
			else
			{
				Hashtable hashtable = new Hashtable();
				int j = 0;
				int num = 0;
				while (j < fieldNames.Length)
				{
					if (fieldNames[j] != null)
					{
						hashtable[fieldNames[j]] = fieldValues[j];
						num++;
					}
					j++;
				}
				MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(serObj.GetType());
				for (int k = 0; k < serializableMembers.Length; k++)
				{
					string name = serializableMembers[k].Name;
					if (!hashtable.Contains(name))
					{
						object[] customAttributes = serializableMembers[k].GetCustomAttributes(typeof(OptionalFieldAttribute), false);
						if (customAttributes == null || customAttributes.Length == 0)
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_MissingMember", new object[]
							{
								serializableMembers[k],
								serObj.GetType(),
								typeof(OptionalFieldAttribute).FullName
							}));
						}
					}
					else
					{
						object obj = hashtable[name];
						FormatterServices.SerializationSetValue(serializableMembers[k], serObj, obj);
					}
				}
			}
			context = ObjectCloneHelper.s_cloneContext;
			return serializationInfo;
		}

		// Token: 0x04002477 RID: 9335
		private static readonly IFormatterConverter s_converter = new FormatterConverter();

		// Token: 0x04002478 RID: 9336
		private static readonly StreamingContext s_cloneContext = new StreamingContext(StreamingContextStates.CrossAppDomain);

		// Token: 0x04002479 RID: 9337
		private static readonly ISerializationSurrogate s_RemotingSurrogate = new RemotingSurrogate();

		// Token: 0x0400247A RID: 9338
		private static readonly ISerializationSurrogate s_ObjRefRemotingSurrogate = new ObjRefSurrogate();
	}
}
