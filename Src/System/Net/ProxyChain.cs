using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x020001DF RID: 479
	internal abstract class ProxyChain : IEnumerable<Uri>, IEnumerable, IDisposable
	{
		// Token: 0x060012B1 RID: 4785 RVA: 0x00063470 File Offset: 0x00061670
		protected ProxyChain(Uri destination)
		{
			this.m_Destination = destination;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0006348C File Offset: 0x0006168C
		public IEnumerator<Uri> GetEnumerator()
		{
			ProxyChain.ProxyEnumerator proxyEnumerator = new ProxyChain.ProxyEnumerator(this);
			if (this.m_MainEnumerator == null)
			{
				this.m_MainEnumerator = proxyEnumerator;
			}
			return proxyEnumerator;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x000634B0 File Offset: 0x000616B0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x000634B8 File Offset: 0x000616B8
		public virtual void Dispose()
		{
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x000634BC File Offset: 0x000616BC
		internal IEnumerator<Uri> Enumerator
		{
			get
			{
				if (this.m_MainEnumerator != null)
				{
					return this.m_MainEnumerator;
				}
				return this.GetEnumerator();
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x000634E0 File Offset: 0x000616E0
		internal Uri Destination
		{
			get
			{
				return this.m_Destination;
			}
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x000634E8 File Offset: 0x000616E8
		internal virtual void Abort()
		{
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x000634EA File Offset: 0x000616EA
		internal bool HttpAbort(HttpWebRequest request, WebException webException)
		{
			this.Abort();
			return true;
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x000634F3 File Offset: 0x000616F3
		internal HttpAbortDelegate HttpAbortDelegate
		{
			get
			{
				if (this.m_HttpAbortDelegate == null)
				{
					this.m_HttpAbortDelegate = new HttpAbortDelegate(this.HttpAbort);
				}
				return this.m_HttpAbortDelegate;
			}
		}

		// Token: 0x060012BA RID: 4794
		protected abstract bool GetNextProxy(out Uri proxy);

		// Token: 0x04001503 RID: 5379
		private List<Uri> m_Cache = new List<Uri>();

		// Token: 0x04001504 RID: 5380
		private bool m_CacheComplete;

		// Token: 0x04001505 RID: 5381
		private ProxyChain.ProxyEnumerator m_MainEnumerator;

		// Token: 0x04001506 RID: 5382
		private Uri m_Destination;

		// Token: 0x04001507 RID: 5383
		private HttpAbortDelegate m_HttpAbortDelegate;

		// Token: 0x02000754 RID: 1876
		private class ProxyEnumerator : IEnumerator<Uri>, IDisposable, IEnumerator
		{
			// Token: 0x060041DD RID: 16861 RVA: 0x00111725 File Offset: 0x0010F925
			internal ProxyEnumerator(ProxyChain chain)
			{
				this.m_Chain = chain;
			}

			// Token: 0x17000F0E RID: 3854
			// (get) Token: 0x060041DE RID: 16862 RVA: 0x0011173B File Offset: 0x0010F93B
			public Uri Current
			{
				get
				{
					if (this.m_Finished || this.m_CurrentIndex < 0)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.m_Chain.m_Cache[this.m_CurrentIndex];
				}
			}

			// Token: 0x17000F0F RID: 3855
			// (get) Token: 0x060041DF RID: 16863 RVA: 0x00111774 File Offset: 0x0010F974
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060041E0 RID: 16864 RVA: 0x0011177C File Offset: 0x0010F97C
			public bool MoveNext()
			{
				if (this.m_Finished)
				{
					return false;
				}
				checked
				{
					this.m_CurrentIndex++;
					if (this.m_Chain.m_Cache.Count > this.m_CurrentIndex)
					{
						return true;
					}
					if (this.m_Chain.m_CacheComplete)
					{
						this.m_Finished = true;
						return false;
					}
					List<Uri> cache = this.m_Chain.m_Cache;
					bool flag2;
					lock (cache)
					{
						if (this.m_Chain.m_Cache.Count > this.m_CurrentIndex)
						{
							flag2 = true;
						}
						else if (this.m_Chain.m_CacheComplete)
						{
							this.m_Finished = true;
							flag2 = false;
						}
						else
						{
							Uri uri;
							while (this.m_Chain.GetNextProxy(out uri))
							{
								if (uri == null)
								{
									if (this.m_TriedDirect)
									{
										continue;
									}
									this.m_TriedDirect = true;
								}
								this.m_Chain.m_Cache.Add(uri);
								return true;
							}
							this.m_Finished = true;
							this.m_Chain.m_CacheComplete = true;
							flag2 = false;
						}
					}
					return flag2;
				}
			}

			// Token: 0x060041E1 RID: 16865 RVA: 0x0011188C File Offset: 0x0010FA8C
			public void Reset()
			{
				this.m_Finished = false;
				this.m_CurrentIndex = -1;
			}

			// Token: 0x060041E2 RID: 16866 RVA: 0x0011189C File Offset: 0x0010FA9C
			public void Dispose()
			{
			}

			// Token: 0x040031F5 RID: 12789
			private ProxyChain m_Chain;

			// Token: 0x040031F6 RID: 12790
			private bool m_Finished;

			// Token: 0x040031F7 RID: 12791
			private int m_CurrentIndex = -1;

			// Token: 0x040031F8 RID: 12792
			private bool m_TriedDirect;
		}
	}
}
