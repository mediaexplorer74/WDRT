using System;

namespace System.Net
{
	// Token: 0x020001E0 RID: 480
	internal class ProxyScriptChain : ProxyChain
	{
		// Token: 0x060012BB RID: 4795 RVA: 0x00063515 File Offset: 0x00061715
		internal ProxyScriptChain(WebProxy proxy, Uri destination)
			: base(destination)
		{
			this.m_Proxy = proxy;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00063528 File Offset: 0x00061728
		protected override bool GetNextProxy(out Uri proxy)
		{
			if (this.m_CurrentIndex < 0)
			{
				proxy = null;
				return false;
			}
			if (this.m_CurrentIndex == 0)
			{
				this.m_ScriptProxies = this.m_Proxy.GetProxiesAuto(base.Destination, ref this.m_SyncStatus);
			}
			if (this.m_ScriptProxies == null || this.m_CurrentIndex >= this.m_ScriptProxies.Length)
			{
				proxy = this.m_Proxy.GetProxyAutoFailover(base.Destination);
				this.m_CurrentIndex = -1;
				return true;
			}
			Uri[] scriptProxies = this.m_ScriptProxies;
			int currentIndex = this.m_CurrentIndex;
			this.m_CurrentIndex = currentIndex + 1;
			proxy = scriptProxies[currentIndex];
			return true;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x000635B7 File Offset: 0x000617B7
		internal override void Abort()
		{
			this.m_Proxy.AbortGetProxiesAuto(ref this.m_SyncStatus);
		}

		// Token: 0x04001508 RID: 5384
		private WebProxy m_Proxy;

		// Token: 0x04001509 RID: 5385
		private Uri[] m_ScriptProxies;

		// Token: 0x0400150A RID: 5386
		private int m_CurrentIndex;

		// Token: 0x0400150B RID: 5387
		private int m_SyncStatus;
	}
}
