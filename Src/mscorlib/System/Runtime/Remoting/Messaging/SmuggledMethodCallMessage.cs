using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000875 RID: 2165
	internal class SmuggledMethodCallMessage : MessageSmuggler
	{
		// Token: 0x06005C49 RID: 23625 RVA: 0x001449F4 File Offset: 0x00142BF4
		[SecurityCritical]
		internal static SmuggledMethodCallMessage SmuggleIfPossible(IMessage msg)
		{
			IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
			if (methodCallMessage == null)
			{
				return null;
			}
			return new SmuggledMethodCallMessage(methodCallMessage);
		}

		// Token: 0x06005C4A RID: 23626 RVA: 0x00144A13 File Offset: 0x00142C13
		private SmuggledMethodCallMessage()
		{
		}

		// Token: 0x06005C4B RID: 23627 RVA: 0x00144A1C File Offset: 0x00142C1C
		[SecurityCritical]
		private SmuggledMethodCallMessage(IMethodCallMessage mcm)
		{
			this._uri = mcm.Uri;
			this._methodName = mcm.MethodName;
			this._typeName = mcm.TypeName;
			ArrayList arrayList = null;
			IInternalMessage internalMessage = mcm as IInternalMessage;
			if (internalMessage == null || internalMessage.HasProperties())
			{
				this._propertyCount = MessageSmuggler.StoreUserPropertiesForMethodMessage(mcm, ref arrayList);
			}
			if (mcm.MethodBase.IsGenericMethod)
			{
				Type[] genericArguments = mcm.MethodBase.GetGenericArguments();
				if (genericArguments != null && genericArguments.Length != 0)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					this._instantiation = new MessageSmuggler.SerializedArg(arrayList.Count);
					arrayList.Add(genericArguments);
				}
			}
			if (RemotingServices.IsMethodOverloaded(mcm))
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._methodSignature = new MessageSmuggler.SerializedArg(arrayList.Count);
				arrayList.Add(mcm.MethodSignature);
			}
			LogicalCallContext logicalCallContext = mcm.LogicalCallContext;
			if (logicalCallContext == null)
			{
				this._callContext = null;
			}
			else if (logicalCallContext.HasInfo)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._callContext = new MessageSmuggler.SerializedArg(arrayList.Count);
				arrayList.Add(logicalCallContext);
			}
			else
			{
				this._callContext = logicalCallContext.RemotingData.LogicalCallID;
			}
			this._args = MessageSmuggler.FixupArgs(mcm.Args, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CrossAppDomainSerializer.SerializeMessageParts(arrayList);
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06005C4C RID: 23628 RVA: 0x00144B64 File Offset: 0x00142D64
		[SecurityCritical]
		internal ArrayList FixupForNewAppDomain()
		{
			ArrayList arrayList = null;
			if (this._serializedArgs != null)
			{
				arrayList = CrossAppDomainSerializer.DeserializeMessageParts(new MemoryStream(this._serializedArgs));
				this._serializedArgs = null;
			}
			return arrayList;
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06005C4D RID: 23629 RVA: 0x00144B94 File Offset: 0x00142D94
		internal string Uri
		{
			get
			{
				return this._uri;
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06005C4E RID: 23630 RVA: 0x00144B9C File Offset: 0x00142D9C
		internal string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06005C4F RID: 23631 RVA: 0x00144BA4 File Offset: 0x00142DA4
		internal string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x06005C50 RID: 23632 RVA: 0x00144BAC File Offset: 0x00142DAC
		internal Type[] GetInstantiation(ArrayList deserializedArgs)
		{
			if (this._instantiation != null)
			{
				return (Type[])deserializedArgs[this._instantiation.Index];
			}
			return null;
		}

		// Token: 0x06005C51 RID: 23633 RVA: 0x00144BCE File Offset: 0x00142DCE
		internal object[] GetMethodSignature(ArrayList deserializedArgs)
		{
			if (this._methodSignature != null)
			{
				return (object[])deserializedArgs[this._methodSignature.Index];
			}
			return null;
		}

		// Token: 0x06005C52 RID: 23634 RVA: 0x00144BF0 File Offset: 0x00142DF0
		[SecurityCritical]
		internal object[] GetArgs(ArrayList deserializedArgs)
		{
			return MessageSmuggler.UndoFixupArgs(this._args, deserializedArgs);
		}

		// Token: 0x06005C53 RID: 23635 RVA: 0x00144C00 File Offset: 0x00142E00
		[SecurityCritical]
		internal LogicalCallContext GetCallContext(ArrayList deserializedArgs)
		{
			if (this._callContext == null)
			{
				return null;
			}
			if (this._callContext is string)
			{
				return new LogicalCallContext
				{
					RemotingData = 
					{
						LogicalCallID = (string)this._callContext
					}
				};
			}
			return (LogicalCallContext)deserializedArgs[((MessageSmuggler.SerializedArg)this._callContext).Index];
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06005C54 RID: 23636 RVA: 0x00144C5D File Offset: 0x00142E5D
		internal int MessagePropertyCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x06005C55 RID: 23637 RVA: 0x00144C68 File Offset: 0x00142E68
		internal void PopulateMessageProperties(IDictionary dict, ArrayList deserializedArgs)
		{
			for (int i = 0; i < this._propertyCount; i++)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)deserializedArgs[i];
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x040029A7 RID: 10663
		private string _uri;

		// Token: 0x040029A8 RID: 10664
		private string _methodName;

		// Token: 0x040029A9 RID: 10665
		private string _typeName;

		// Token: 0x040029AA RID: 10666
		private object[] _args;

		// Token: 0x040029AB RID: 10667
		private byte[] _serializedArgs;

		// Token: 0x040029AC RID: 10668
		private MessageSmuggler.SerializedArg _methodSignature;

		// Token: 0x040029AD RID: 10669
		private MessageSmuggler.SerializedArg _instantiation;

		// Token: 0x040029AE RID: 10670
		private object _callContext;

		// Token: 0x040029AF RID: 10671
		private int _propertyCount;
	}
}
