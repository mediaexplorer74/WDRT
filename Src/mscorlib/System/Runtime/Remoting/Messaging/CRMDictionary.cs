using System;
using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000860 RID: 2144
	internal class CRMDictionary : MessageDictionary
	{
		// Token: 0x06005B18 RID: 23320 RVA: 0x00140993 File Offset: 0x0013EB93
		[SecurityCritical]
		public CRMDictionary(IConstructionReturnMessage msg, IDictionary idict)
			: base((msg.Exception != null) ? CRMDictionary.CRMkeysFault : CRMDictionary.CRMkeysNoFault, idict)
		{
			this.fault = msg.Exception != null;
			this._crmsg = msg;
		}

		// Token: 0x06005B19 RID: 23321 RVA: 0x001409C8 File Offset: 0x0013EBC8
		[SecuritySafeCritical]
		internal override object GetMessageValue(int i)
		{
			switch (i)
			{
			case 0:
				return this._crmsg.Uri;
			case 1:
				return this._crmsg.MethodName;
			case 2:
				return this._crmsg.MethodSignature;
			case 3:
				return this._crmsg.TypeName;
			case 4:
				if (!this.fault)
				{
					return this._crmsg.ReturnValue;
				}
				return this.FetchLogicalCallContext();
			case 5:
				return this._crmsg.Args;
			case 6:
				return this.FetchLogicalCallContext();
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x06005B1A RID: 23322 RVA: 0x00140A68 File Offset: 0x0013EC68
		[SecurityCritical]
		private LogicalCallContext FetchLogicalCallContext()
		{
			ReturnMessage returnMessage = this._crmsg as ReturnMessage;
			if (returnMessage != null)
			{
				return returnMessage.GetLogicalCallContext();
			}
			MethodResponse methodResponse = this._crmsg as MethodResponse;
			if (methodResponse != null)
			{
				return methodResponse.GetLogicalCallContext();
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
		}

		// Token: 0x06005B1B RID: 23323 RVA: 0x00140AB0 File Offset: 0x0013ECB0
		[SecurityCritical]
		internal override void SetSpecialKey(int keyNum, object value)
		{
			ReturnMessage returnMessage = this._crmsg as ReturnMessage;
			MethodResponse methodResponse = this._crmsg as MethodResponse;
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

		// Token: 0x04002942 RID: 10562
		public static string[] CRMkeysFault = new string[] { "__Uri", "__MethodName", "__MethodSignature", "__TypeName", "__CallContext" };

		// Token: 0x04002943 RID: 10563
		public static string[] CRMkeysNoFault = new string[] { "__Uri", "__MethodName", "__MethodSignature", "__TypeName", "__Return", "__OutArgs", "__CallContext" };

		// Token: 0x04002944 RID: 10564
		internal IConstructionReturnMessage _crmsg;

		// Token: 0x04002945 RID: 10565
		internal bool fault;
	}
}
