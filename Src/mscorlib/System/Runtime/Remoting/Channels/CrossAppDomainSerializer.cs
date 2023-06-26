using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000838 RID: 2104
	internal static class CrossAppDomainSerializer
	{
		// Token: 0x06005A0B RID: 23051 RVA: 0x0013EAB8 File Offset: 0x0013CCB8
		[SecurityCritical]
		internal static MemoryStream SerializeMessage(IMessage msg)
		{
			MemoryStream memoryStream = new MemoryStream();
			RemotingSurrogateSelector remotingSurrogateSelector = new RemotingSurrogateSelector();
			new BinaryFormatter
			{
				SurrogateSelector = remotingSurrogateSelector,
				Context = new StreamingContext(StreamingContextStates.CrossAppDomain)
			}.Serialize(memoryStream, msg, null, false);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06005A0C RID: 23052 RVA: 0x0013EB04 File Offset: 0x0013CD04
		[SecurityCritical]
		internal static MemoryStream SerializeMessageParts(ArrayList argsToSerialize)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			RemotingSurrogateSelector remotingSurrogateSelector = new RemotingSurrogateSelector();
			binaryFormatter.SurrogateSelector = remotingSurrogateSelector;
			binaryFormatter.Context = new StreamingContext(StreamingContextStates.CrossAppDomain);
			binaryFormatter.Serialize(memoryStream, argsToSerialize, null, false);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06005A0D RID: 23053 RVA: 0x0013EB50 File Offset: 0x0013CD50
		[SecurityCritical]
		internal static void SerializeObject(object obj, MemoryStream stm)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			RemotingSurrogateSelector remotingSurrogateSelector = new RemotingSurrogateSelector();
			binaryFormatter.SurrogateSelector = remotingSurrogateSelector;
			binaryFormatter.Context = new StreamingContext(StreamingContextStates.CrossAppDomain);
			binaryFormatter.Serialize(stm, obj, null, false);
		}

		// Token: 0x06005A0E RID: 23054 RVA: 0x0013EB8C File Offset: 0x0013CD8C
		[SecurityCritical]
		internal static MemoryStream SerializeObject(object obj)
		{
			MemoryStream memoryStream = new MemoryStream();
			CrossAppDomainSerializer.SerializeObject(obj, memoryStream);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06005A0F RID: 23055 RVA: 0x0013EBAF File Offset: 0x0013CDAF
		[SecurityCritical]
		internal static IMessage DeserializeMessage(MemoryStream stm)
		{
			return CrossAppDomainSerializer.DeserializeMessage(stm, null);
		}

		// Token: 0x06005A10 RID: 23056 RVA: 0x0013EBB8 File Offset: 0x0013CDB8
		[SecurityCritical]
		internal static IMessage DeserializeMessage(MemoryStream stm, IMethodCallMessage reqMsg)
		{
			if (stm == null)
			{
				throw new ArgumentNullException("stm");
			}
			stm.Position = 0L;
			return (IMessage)new BinaryFormatter
			{
				SurrogateSelector = null,
				Context = new StreamingContext(StreamingContextStates.CrossAppDomain)
			}.Deserialize(stm, null, false, true, reqMsg);
		}

		// Token: 0x06005A11 RID: 23057 RVA: 0x0013EC08 File Offset: 0x0013CE08
		[SecurityCritical]
		internal static ArrayList DeserializeMessageParts(MemoryStream stm)
		{
			return (ArrayList)CrossAppDomainSerializer.DeserializeObject(stm);
		}

		// Token: 0x06005A12 RID: 23058 RVA: 0x0013EC18 File Offset: 0x0013CE18
		[SecurityCritical]
		internal static object DeserializeObject(MemoryStream stm)
		{
			stm.Position = 0L;
			return new BinaryFormatter
			{
				Context = new StreamingContext(StreamingContextStates.CrossAppDomain)
			}.Deserialize(stm, null, false, true, null);
		}
	}
}
