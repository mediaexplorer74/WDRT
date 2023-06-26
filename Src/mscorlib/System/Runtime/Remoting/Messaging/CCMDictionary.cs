using System;
using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200085F RID: 2143
	internal class CCMDictionary : MessageDictionary
	{
		// Token: 0x06005B13 RID: 23315 RVA: 0x0014079E File Offset: 0x0013E99E
		public CCMDictionary(IConstructionCallMessage msg, IDictionary idict)
			: base(CCMDictionary.CCMkeys, idict)
		{
			this._ccmsg = msg;
		}

		// Token: 0x06005B14 RID: 23316 RVA: 0x001407B4 File Offset: 0x0013E9B4
		[SecuritySafeCritical]
		internal override object GetMessageValue(int i)
		{
			switch (i)
			{
			case 0:
				return this._ccmsg.Uri;
			case 1:
				return this._ccmsg.MethodName;
			case 2:
				return this._ccmsg.MethodSignature;
			case 3:
				return this._ccmsg.TypeName;
			case 4:
				return this._ccmsg.Args;
			case 5:
				return this.FetchLogicalCallContext();
			case 6:
				return this._ccmsg.CallSiteActivationAttributes;
			case 7:
				return null;
			case 8:
				return this._ccmsg.ContextProperties;
			case 9:
				return this._ccmsg.Activator;
			case 10:
				return this._ccmsg.ActivationTypeName;
			default:
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x06005B15 RID: 23317 RVA: 0x0014087C File Offset: 0x0013EA7C
		[SecurityCritical]
		private LogicalCallContext FetchLogicalCallContext()
		{
			ConstructorCallMessage constructorCallMessage = this._ccmsg as ConstructorCallMessage;
			if (constructorCallMessage != null)
			{
				return constructorCallMessage.GetLogicalCallContext();
			}
			if (this._ccmsg is ConstructionCall)
			{
				return ((MethodCall)this._ccmsg).GetLogicalCallContext();
			}
			throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
		}

		// Token: 0x06005B16 RID: 23318 RVA: 0x001408CC File Offset: 0x0013EACC
		[SecurityCritical]
		internal override void SetSpecialKey(int keyNum, object value)
		{
			if (keyNum == 0)
			{
				((ConstructorCallMessage)this._ccmsg).Uri = (string)value;
				return;
			}
			if (keyNum != 1)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
			((ConstructorCallMessage)this._ccmsg).SetLogicalCallContext((LogicalCallContext)value);
		}

		// Token: 0x04002940 RID: 10560
		public static string[] CCMkeys = new string[]
		{
			"__Uri", "__MethodName", "__MethodSignature", "__TypeName", "__Args", "__CallContext", "__CallSiteActivationAttributes", "__ActivationType", "__ContextProperties", "__Activator",
			"__ActivationTypeName"
		};

		// Token: 0x04002941 RID: 10561
		internal IConstructionCallMessage _ccmsg;
	}
}
