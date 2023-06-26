using System;
using System.Collections;
using System.Reflection;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000865 RID: 2149
	internal class StackBasedReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage, IInternalMessage
	{
		// Token: 0x06005B44 RID: 23364 RVA: 0x001414B0 File Offset: 0x0013F6B0
		internal StackBasedReturnMessage()
		{
		}

		// Token: 0x06005B45 RID: 23365 RVA: 0x001414B8 File Offset: 0x0013F6B8
		internal void InitFields(Message m)
		{
			this._m = m;
			if (this._h != null)
			{
				this._h.Clear();
			}
			if (this._d != null)
			{
				this._d.Clear();
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06005B46 RID: 23366 RVA: 0x001414E7 File Offset: 0x0013F6E7
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return this._m.Uri;
			}
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06005B47 RID: 23367 RVA: 0x001414F4 File Offset: 0x0013F6F4
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				return this._m.MethodName;
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06005B48 RID: 23368 RVA: 0x00141501 File Offset: 0x0013F701
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				return this._m.TypeName;
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06005B49 RID: 23369 RVA: 0x0014150E File Offset: 0x0013F70E
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return this._m.MethodSignature;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06005B4A RID: 23370 RVA: 0x0014151B File Offset: 0x0013F71B
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return this._m.MethodBase;
			}
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06005B4B RID: 23371 RVA: 0x00141528 File Offset: 0x0013F728
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return this._m.HasVarArgs;
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06005B4C RID: 23372 RVA: 0x00141535 File Offset: 0x0013F735
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				return this._m.ArgCount;
			}
		}

		// Token: 0x06005B4D RID: 23373 RVA: 0x00141542 File Offset: 0x0013F742
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			return this._m.GetArg(argNum);
		}

		// Token: 0x06005B4E RID: 23374 RVA: 0x00141550 File Offset: 0x0013F750
		[SecurityCritical]
		public string GetArgName(int index)
		{
			return this._m.GetArgName(index);
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06005B4F RID: 23375 RVA: 0x0014155E File Offset: 0x0013F75E
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return this._m.Args;
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06005B50 RID: 23376 RVA: 0x0014156B File Offset: 0x0013F76B
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this._m.GetLogicalCallContext();
			}
		}

		// Token: 0x06005B51 RID: 23377 RVA: 0x00141578 File Offset: 0x0013F778
		[SecurityCritical]
		internal LogicalCallContext GetLogicalCallContext()
		{
			return this._m.GetLogicalCallContext();
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x00141585 File Offset: 0x0013F785
		[SecurityCritical]
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
		{
			return this._m.SetLogicalCallContext(callCtx);
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06005B53 RID: 23379 RVA: 0x00141593 File Offset: 0x0013F793
		public int OutArgCount
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, true);
				}
				return this._argMapper.ArgCount;
			}
		}

		// Token: 0x06005B54 RID: 23380 RVA: 0x001415B5 File Offset: 0x0013F7B5
		[SecurityCritical]
		public object GetOutArg(int argNum)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArg(argNum);
		}

		// Token: 0x06005B55 RID: 23381 RVA: 0x001415D8 File Offset: 0x0013F7D8
		[SecurityCritical]
		public string GetOutArgName(int index)
		{
			if (this._argMapper == null)
			{
				this._argMapper = new ArgMapper(this, true);
			}
			return this._argMapper.GetArgName(index);
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06005B56 RID: 23382 RVA: 0x001415FB File Offset: 0x0013F7FB
		public object[] OutArgs
		{
			[SecurityCritical]
			get
			{
				if (this._argMapper == null)
				{
					this._argMapper = new ArgMapper(this, true);
				}
				return this._argMapper.Args;
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06005B57 RID: 23383 RVA: 0x0014161D File Offset: 0x0013F81D
		public Exception Exception
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06005B58 RID: 23384 RVA: 0x00141620 File Offset: 0x0013F820
		public object ReturnValue
		{
			[SecurityCritical]
			get
			{
				return this._m.GetReturnValue();
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06005B59 RID: 23385 RVA: 0x00141630 File Offset: 0x0013F830
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				IDictionary d;
				lock (this)
				{
					if (this._h == null)
					{
						this._h = new Hashtable();
					}
					if (this._d == null)
					{
						this._d = new MRMDictionary(this, this._h);
					}
					d = this._d;
				}
				return d;
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06005B5A RID: 23386 RVA: 0x0014169C File Offset: 0x0013F89C
		// (set) Token: 0x06005B5B RID: 23387 RVA: 0x0014169F File Offset: 0x0013F89F
		ServerIdentity IInternalMessage.ServerIdentityObject
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06005B5C RID: 23388 RVA: 0x001416A1 File Offset: 0x0013F8A1
		// (set) Token: 0x06005B5D RID: 23389 RVA: 0x001416A4 File Offset: 0x0013F8A4
		Identity IInternalMessage.IdentityObject
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
			}
		}

		// Token: 0x06005B5E RID: 23390 RVA: 0x001416A6 File Offset: 0x0013F8A6
		[SecurityCritical]
		void IInternalMessage.SetURI(string val)
		{
			this._m.Uri = val;
		}

		// Token: 0x06005B5F RID: 23391 RVA: 0x001416B4 File Offset: 0x0013F8B4
		[SecurityCritical]
		void IInternalMessage.SetCallContext(LogicalCallContext newCallContext)
		{
			this._m.SetLogicalCallContext(newCallContext);
		}

		// Token: 0x06005B60 RID: 23392 RVA: 0x001416C3 File Offset: 0x0013F8C3
		[SecurityCritical]
		bool IInternalMessage.HasProperties()
		{
			return this._h != null;
		}

		// Token: 0x04002951 RID: 10577
		private Message _m;

		// Token: 0x04002952 RID: 10578
		private Hashtable _h;

		// Token: 0x04002953 RID: 10579
		private MRMDictionary _d;

		// Token: 0x04002954 RID: 10580
		private ArgMapper _argMapper;
	}
}
