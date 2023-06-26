using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000861 RID: 2145
	internal class MCMDictionary : MessageDictionary
	{
		// Token: 0x06005B1D RID: 23325 RVA: 0x00140BD3 File Offset: 0x0013EDD3
		public MCMDictionary(IMethodCallMessage msg, IDictionary idict)
			: base(MCMDictionary.MCMkeys, idict)
		{
			this._mcmsg = msg;
		}

		// Token: 0x06005B1E RID: 23326 RVA: 0x00140BE8 File Offset: 0x0013EDE8
		[SecuritySafeCritical]
		internal override object GetMessageValue(int i)
		{
			switch (i)
			{
			case 0:
				return this._mcmsg.Uri;
			case 1:
				return this._mcmsg.MethodName;
			case 2:
				return this._mcmsg.MethodSignature;
			case 3:
				return this._mcmsg.TypeName;
			case 4:
				return this._mcmsg.Args;
			case 5:
				return this.FetchLogicalCallContext();
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x06005B1F RID: 23327 RVA: 0x00140C68 File Offset: 0x0013EE68
		[SecurityCritical]
		private LogicalCallContext FetchLogicalCallContext()
		{
			Message message = this._mcmsg as Message;
			if (message != null)
			{
				return message.GetLogicalCallContext();
			}
			MethodCall methodCall = this._mcmsg as MethodCall;
			if (methodCall != null)
			{
				return methodCall.GetLogicalCallContext();
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
		}

		// Token: 0x06005B20 RID: 23328 RVA: 0x00140CB0 File Offset: 0x0013EEB0
		[SecurityCritical]
		internal override void SetSpecialKey(int keyNum, object value)
		{
			Message message = this._mcmsg as Message;
			MethodCall methodCall = this._mcmsg as MethodCall;
			if (keyNum != 0)
			{
				if (keyNum != 1)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
				}
				if (message != null)
				{
					message.SetLogicalCallContext((LogicalCallContext)value);
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			}
			else
			{
				if (message != null)
				{
					message.Uri = (string)value;
					return;
				}
				if (methodCall != null)
				{
					methodCall.Uri = (string)value;
					return;
				}
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
			}
		}

		// Token: 0x04002946 RID: 10566
		public static string[] MCMkeys = new string[] { "__Uri", "__MethodName", "__MethodSignature", "__TypeName", "__Args", "__CallContext" };

		// Token: 0x04002947 RID: 10567
		internal IMethodCallMessage _mcmsg;
	}
}
