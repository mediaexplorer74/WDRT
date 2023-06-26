using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200088B RID: 2187
	internal class IllogicalCallContext
	{
		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06005CD1 RID: 23761 RVA: 0x001469AC File Offset: 0x00144BAC
		private Hashtable Datastore
		{
			get
			{
				if (this.m_Datastore == null)
				{
					this.m_Datastore = new Hashtable();
				}
				return this.m_Datastore;
			}
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06005CD2 RID: 23762 RVA: 0x001469C7 File Offset: 0x00144BC7
		// (set) Token: 0x06005CD3 RID: 23763 RVA: 0x001469CF File Offset: 0x00144BCF
		internal object HostContext
		{
			get
			{
				return this.m_HostContext;
			}
			set
			{
				this.m_HostContext = value;
			}
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06005CD4 RID: 23764 RVA: 0x001469D8 File Offset: 0x00144BD8
		internal bool HasUserData
		{
			get
			{
				return this.m_Datastore != null && this.m_Datastore.Count > 0;
			}
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x001469F2 File Offset: 0x00144BF2
		public void FreeNamedDataSlot(string name)
		{
			this.Datastore.Remove(name);
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x00146A00 File Offset: 0x00144C00
		public object GetData(string name)
		{
			return this.Datastore[name];
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x00146A0E File Offset: 0x00144C0E
		public void SetData(string name, object data)
		{
			this.Datastore[name] = data;
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x00146A20 File Offset: 0x00144C20
		public IllogicalCallContext CreateCopy()
		{
			IllogicalCallContext illogicalCallContext = new IllogicalCallContext();
			illogicalCallContext.HostContext = this.HostContext;
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					illogicalCallContext.Datastore[(string)enumerator.Key] = enumerator.Value;
				}
			}
			return illogicalCallContext;
		}

		// Token: 0x040029DF RID: 10719
		private Hashtable m_Datastore;

		// Token: 0x040029E0 RID: 10720
		private object m_HostContext;

		// Token: 0x02000C76 RID: 3190
		internal struct Reader
		{
			// Token: 0x060070D7 RID: 28887 RVA: 0x001863A0 File Offset: 0x001845A0
			public Reader(IllogicalCallContext ctx)
			{
				this.m_ctx = ctx;
			}

			// Token: 0x17001356 RID: 4950
			// (get) Token: 0x060070D8 RID: 28888 RVA: 0x001863A9 File Offset: 0x001845A9
			public bool IsNull
			{
				get
				{
					return this.m_ctx == null;
				}
			}

			// Token: 0x060070D9 RID: 28889 RVA: 0x001863B4 File Offset: 0x001845B4
			[SecurityCritical]
			public object GetData(string name)
			{
				if (!this.IsNull)
				{
					return this.m_ctx.GetData(name);
				}
				return null;
			}

			// Token: 0x17001357 RID: 4951
			// (get) Token: 0x060070DA RID: 28890 RVA: 0x001863CC File Offset: 0x001845CC
			public object HostContext
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ctx.HostContext;
					}
					return null;
				}
			}

			// Token: 0x04003809 RID: 14345
			private IllogicalCallContext m_ctx;
		}
	}
}
