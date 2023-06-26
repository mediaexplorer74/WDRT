using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000862 RID: 2146
	internal class MRMDictionary : MessageDictionary
	{
		// Token: 0x06005B22 RID: 23330 RVA: 0x00140D7B File Offset: 0x0013EF7B
		[SecurityCritical]
		public MRMDictionary(IMethodReturnMessage msg, IDictionary idict)
			: base((msg.Exception != null) ? MRMDictionary.MCMkeysFault : MRMDictionary.MCMkeysNoFault, idict)
		{
			this.fault = msg.Exception != null;
			this._mrmsg = msg;
		}

		// Token: 0x06005B23 RID: 23331 RVA: 0x00140DB0 File Offset: 0x0013EFB0
		[SecuritySafeCritical]
		internal override object GetMessageValue(int i)
		{
			switch (i)
			{
			case 0:
				if (this.fault)
				{
					return this.FetchLogicalCallContext();
				}
				return this._mrmsg.Uri;
			case 1:
				return this._mrmsg.MethodName;
			case 2:
				return this._mrmsg.MethodSignature;
			case 3:
				return this._mrmsg.TypeName;
			case 4:
				if (this.fault)
				{
					return this._mrmsg.Exception;
				}
				return this._mrmsg.ReturnValue;
			case 5:
				return this._mrmsg.Args;
			case 6:
				return this.FetchLogicalCallContext();
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x06005B24 RID: 23332 RVA: 0x00140E64 File Offset: 0x0013F064
		[SecurityCritical]
		private LogicalCallContext FetchLogicalCallContext()
		{
			ReturnMessage returnMessage = this._mrmsg as ReturnMessage;
			if (returnMessage != null)
			{
				return returnMessage.GetLogicalCallContext();
			}
			MethodResponse methodResponse = this._mrmsg as MethodResponse;
			if (methodResponse != null)
			{
				return methodResponse.GetLogicalCallContext();
			}
			StackBasedReturnMessage stackBasedReturnMessage = this._mrmsg as StackBasedReturnMessage;
			if (stackBasedReturnMessage != null)
			{
				return stackBasedReturnMessage.GetLogicalCallContext();
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x00140EC4 File Offset: 0x0013F0C4
		[SecurityCritical]
		internal override void SetSpecialKey(int keyNum, object value)
		{
			ReturnMessage returnMessage = this._mrmsg as ReturnMessage;
			MethodResponse methodResponse = this._mrmsg as MethodResponse;
			if (keyNum != 0)
			{
				if (keyNum != 1)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
				}
				if (returnMessage != null)
				{
					returnMessage.SetLogicalCallContext((LogicalCallContext)value);
					return;
				}
				if (methodResponse != null)
				{
					methodResponse.SetLogicalCallContext((LogicalCallContext)value);
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			}
			else
			{
				if (returnMessage != null)
				{
					returnMessage.Uri = (string)value;
					return;
				}
				if (methodResponse != null)
				{
					methodResponse.Uri = (string)value;
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			}
		}

		// Token: 0x04002948 RID: 10568
		public static string[] MCMkeysFault = new string[] { "__CallContext" };

		// Token: 0x04002949 RID: 10569
		public static string[] MCMkeysNoFault = new string[] { "__Uri", "__MethodName", "__MethodSignature", "__TypeName", "__Return", "__OutArgs", "__CallContext" };

		// Token: 0x0400294A RID: 10570
		internal IMethodReturnMessage _mrmsg;

		// Token: 0x0400294B RID: 10571
		internal bool fault;
	}
}
