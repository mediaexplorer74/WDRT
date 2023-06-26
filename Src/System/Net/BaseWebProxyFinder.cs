using System;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x020000C7 RID: 199
	internal abstract class BaseWebProxyFinder : IWebProxyFinder, IDisposable
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x000252CA File Offset: 0x000234CA
		public BaseWebProxyFinder(AutoWebProxyScriptEngine engine)
		{
			this.engine = engine;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x000252D9 File Offset: 0x000234D9
		public bool IsValid
		{
			get
			{
				return this.state == BaseWebProxyFinder.AutoWebProxyState.Completed || this.state == BaseWebProxyFinder.AutoWebProxyState.Uninitialized;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x000252EF File Offset: 0x000234EF
		public bool IsUnrecognizedScheme
		{
			get
			{
				return this.state == BaseWebProxyFinder.AutoWebProxyState.UnrecognizedScheme;
			}
		}

		// Token: 0x060006A5 RID: 1701
		public abstract bool GetProxies(Uri destination, out IList<string> proxyList);

		// Token: 0x060006A6 RID: 1702
		public abstract void Abort();

		// Token: 0x060006A7 RID: 1703 RVA: 0x000252FA File Offset: 0x000234FA
		public virtual void Reset()
		{
			this.State = BaseWebProxyFinder.AutoWebProxyState.Uninitialized;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00025303 File Offset: 0x00023503
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0002530C File Offset: 0x0002350C
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x00025314 File Offset: 0x00023514
		protected BaseWebProxyFinder.AutoWebProxyState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0002531D File Offset: 0x0002351D
		protected AutoWebProxyScriptEngine Engine
		{
			get
			{
				return this.engine;
			}
		}

		// Token: 0x060006AC RID: 1708
		protected abstract void Dispose(bool disposing);

		// Token: 0x04000C86 RID: 3206
		private BaseWebProxyFinder.AutoWebProxyState state;

		// Token: 0x04000C87 RID: 3207
		private AutoWebProxyScriptEngine engine;

		// Token: 0x020006EC RID: 1772
		protected enum AutoWebProxyState
		{
			// Token: 0x0400305C RID: 12380
			Uninitialized,
			// Token: 0x0400305D RID: 12381
			DiscoveryFailure,
			// Token: 0x0400305E RID: 12382
			DownloadFailure,
			// Token: 0x0400305F RID: 12383
			CompilationFailure,
			// Token: 0x04003060 RID: 12384
			UnrecognizedScheme,
			// Token: 0x04003061 RID: 12385
			Completed
		}
	}
}
