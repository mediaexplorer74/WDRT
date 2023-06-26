using System;
using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200085D RID: 2141
	[SecurityCritical]
	internal class ConstructorReturnMessage : ReturnMessage, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
	{
		// Token: 0x06005AEC RID: 23276 RVA: 0x00140337 File Offset: 0x0013E537
		public ConstructorReturnMessage(MarshalByRefObject o, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IConstructionCallMessage ccm)
			: base(o, outArgs, outArgsCount, callCtx, ccm)
		{
			this._o = o;
			this._iFlags = 1;
		}

		// Token: 0x06005AED RID: 23277 RVA: 0x00140354 File Offset: 0x0013E554
		public ConstructorReturnMessage(Exception e, IConstructionCallMessage ccm)
			: base(e, ccm)
		{
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06005AEE RID: 23278 RVA: 0x0014035E File Offset: 0x0013E55E
		public override object ReturnValue
		{
			[SecurityCritical]
			get
			{
				if (this._iFlags == 1)
				{
					return RemotingServices.MarshalInternal(this._o, null, null);
				}
				return base.ReturnValue;
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06005AEF RID: 23279 RVA: 0x00140380 File Offset: 0x0013E580
		public override IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					object obj = new CRMDictionary(this, new Hashtable());
					Interlocked.CompareExchange(ref this._properties, obj, null);
				}
				return (IDictionary)this._properties;
			}
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x001403BA File Offset: 0x0013E5BA
		internal object GetObject()
		{
			return this._o;
		}

		// Token: 0x04002931 RID: 10545
		private const int Intercept = 1;

		// Token: 0x04002932 RID: 10546
		private MarshalByRefObject _o;

		// Token: 0x04002933 RID: 10547
		private int _iFlags;
	}
}
