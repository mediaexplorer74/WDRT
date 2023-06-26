using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000876 RID: 2166
	internal class SmuggledMethodReturnMessage : MessageSmuggler
	{
		// Token: 0x06005C56 RID: 23638 RVA: 0x00144CA8 File Offset: 0x00142EA8
		[SecurityCritical]
		internal static SmuggledMethodReturnMessage SmuggleIfPossible(IMessage msg)
		{
			IMethodReturnMessage methodReturnMessage = msg as IMethodReturnMessage;
			if (methodReturnMessage == null)
			{
				return null;
			}
			return new SmuggledMethodReturnMessage(methodReturnMessage);
		}

		// Token: 0x06005C57 RID: 23639 RVA: 0x00144CC7 File Offset: 0x00142EC7
		private SmuggledMethodReturnMessage()
		{
		}

		// Token: 0x06005C58 RID: 23640 RVA: 0x00144CD0 File Offset: 0x00142ED0
		[SecurityCritical]
		private SmuggledMethodReturnMessage(IMethodReturnMessage mrm)
		{
			ArrayList arrayList = null;
			ReturnMessage returnMessage = mrm as ReturnMessage;
			if (returnMessage == null || returnMessage.HasProperties())
			{
				this._propertyCount = MessageSmuggler.StoreUserPropertiesForMethodMessage(mrm, ref arrayList);
			}
			Exception exception = mrm.Exception;
			if (exception != null)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._exception = new MessageSmuggler.SerializedArg(arrayList.Count);
				arrayList.Add(exception);
			}
			LogicalCallContext logicalCallContext = mrm.LogicalCallContext;
			if (logicalCallContext == null)
			{
				this._callContext = null;
			}
			else if (logicalCallContext.HasInfo)
			{
				if (logicalCallContext.Principal != null)
				{
					logicalCallContext.Principal = null;
				}
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
			this._returnValue = MessageSmuggler.FixupArg(mrm.ReturnValue, ref arrayList);
			this._args = MessageSmuggler.FixupArgs(mrm.Args, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CrossAppDomainSerializer.SerializeMessageParts(arrayList);
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06005C59 RID: 23641 RVA: 0x00144DD0 File Offset: 0x00142FD0
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

		// Token: 0x06005C5A RID: 23642 RVA: 0x00144E00 File Offset: 0x00143000
		[SecurityCritical]
		internal object GetReturnValue(ArrayList deserializedArgs)
		{
			return MessageSmuggler.UndoFixupArg(this._returnValue, deserializedArgs);
		}

		// Token: 0x06005C5B RID: 23643 RVA: 0x00144E10 File Offset: 0x00143010
		[SecurityCritical]
		internal object[] GetArgs(ArrayList deserializedArgs)
		{
			return MessageSmuggler.UndoFixupArgs(this._args, deserializedArgs);
		}

		// Token: 0x06005C5C RID: 23644 RVA: 0x00144E2B File Offset: 0x0014302B
		internal Exception GetException(ArrayList deserializedArgs)
		{
			if (this._exception != null)
			{
				return (Exception)deserializedArgs[this._exception.Index];
			}
			return null;
		}

		// Token: 0x06005C5D RID: 23645 RVA: 0x00144E50 File Offset: 0x00143050
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

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06005C5E RID: 23646 RVA: 0x00144EAD File Offset: 0x001430AD
		internal int MessagePropertyCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x06005C5F RID: 23647 RVA: 0x00144EB8 File Offset: 0x001430B8
		internal void PopulateMessageProperties(IDictionary dict, ArrayList deserializedArgs)
		{
			for (int i = 0; i < this._propertyCount; i++)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)deserializedArgs[i];
				dict[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
		}

		// Token: 0x040029B0 RID: 10672
		private object[] _args;

		// Token: 0x040029B1 RID: 10673
		private object _returnValue;

		// Token: 0x040029B2 RID: 10674
		private byte[] _serializedArgs;

		// Token: 0x040029B3 RID: 10675
		private MessageSmuggler.SerializedArg _exception;

		// Token: 0x040029B4 RID: 10676
		private object _callContext;

		// Token: 0x040029B5 RID: 10677
		private int _propertyCount;
	}
}
