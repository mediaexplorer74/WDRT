using System;
using System.Collections.Generic;

namespace System.Windows.Forms
{
	// Token: 0x02000283 RID: 643
	internal sealed class HtmlShimManager : IDisposable
	{
		// Token: 0x06002937 RID: 10551 RVA: 0x00002843 File Offset: 0x00000A43
		internal HtmlShimManager()
		{
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000BCF70 File Offset: 0x000BB170
		public void AddDocumentShim(HtmlDocument doc)
		{
			HtmlDocument.HtmlDocumentShim htmlDocumentShim = null;
			if (this.htmlDocumentShims == null)
			{
				this.htmlDocumentShims = new Dictionary<HtmlDocument, HtmlDocument.HtmlDocumentShim>();
				htmlDocumentShim = new HtmlDocument.HtmlDocumentShim(doc);
				this.htmlDocumentShims[doc] = htmlDocumentShim;
			}
			else if (!this.htmlDocumentShims.ContainsKey(doc))
			{
				htmlDocumentShim = new HtmlDocument.HtmlDocumentShim(doc);
				this.htmlDocumentShims[doc] = htmlDocumentShim;
			}
			if (htmlDocumentShim != null)
			{
				this.OnShimAdded(htmlDocumentShim);
			}
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000BCFD4 File Offset: 0x000BB1D4
		public void AddWindowShim(HtmlWindow window)
		{
			HtmlWindow.HtmlWindowShim htmlWindowShim = null;
			if (this.htmlWindowShims == null)
			{
				this.htmlWindowShims = new Dictionary<HtmlWindow, HtmlWindow.HtmlWindowShim>();
				htmlWindowShim = new HtmlWindow.HtmlWindowShim(window);
				this.htmlWindowShims[window] = htmlWindowShim;
			}
			else if (!this.htmlWindowShims.ContainsKey(window))
			{
				htmlWindowShim = new HtmlWindow.HtmlWindowShim(window);
				this.htmlWindowShims[window] = htmlWindowShim;
			}
			if (htmlWindowShim != null)
			{
				this.OnShimAdded(htmlWindowShim);
			}
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000BD038 File Offset: 0x000BB238
		public void AddElementShim(HtmlElement element)
		{
			HtmlElement.HtmlElementShim htmlElementShim = null;
			if (this.htmlElementShims == null)
			{
				this.htmlElementShims = new Dictionary<HtmlElement, HtmlElement.HtmlElementShim>();
				htmlElementShim = new HtmlElement.HtmlElementShim(element);
				this.htmlElementShims[element] = htmlElementShim;
			}
			else if (!this.htmlElementShims.ContainsKey(element))
			{
				htmlElementShim = new HtmlElement.HtmlElementShim(element);
				this.htmlElementShims[element] = htmlElementShim;
			}
			if (htmlElementShim != null)
			{
				this.OnShimAdded(htmlElementShim);
			}
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000BD09C File Offset: 0x000BB29C
		internal HtmlDocument.HtmlDocumentShim GetDocumentShim(HtmlDocument document)
		{
			if (this.htmlDocumentShims == null)
			{
				return null;
			}
			if (this.htmlDocumentShims.ContainsKey(document))
			{
				return this.htmlDocumentShims[document];
			}
			return null;
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000BD0C4 File Offset: 0x000BB2C4
		internal HtmlElement.HtmlElementShim GetElementShim(HtmlElement element)
		{
			if (this.htmlElementShims == null)
			{
				return null;
			}
			if (this.htmlElementShims.ContainsKey(element))
			{
				return this.htmlElementShims[element];
			}
			return null;
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000BD0EC File Offset: 0x000BB2EC
		internal HtmlWindow.HtmlWindowShim GetWindowShim(HtmlWindow window)
		{
			if (this.htmlWindowShims == null)
			{
				return null;
			}
			if (this.htmlWindowShims.ContainsKey(window))
			{
				return this.htmlWindowShims[window];
			}
			return null;
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000BD114 File Offset: 0x000BB314
		private void OnShimAdded(HtmlShim addedShim)
		{
			if (addedShim != null && !(addedShim is HtmlWindow.HtmlWindowShim))
			{
				this.AddWindowShim(new HtmlWindow(this, addedShim.AssociatedWindow));
			}
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000BD134 File Offset: 0x000BB334
		internal void OnWindowUnloaded(HtmlWindow unloadedWindow)
		{
			if (unloadedWindow != null)
			{
				if (this.htmlDocumentShims != null)
				{
					HtmlDocument.HtmlDocumentShim[] array = new HtmlDocument.HtmlDocumentShim[this.htmlDocumentShims.Count];
					this.htmlDocumentShims.Values.CopyTo(array, 0);
					foreach (HtmlDocument.HtmlDocumentShim htmlDocumentShim in array)
					{
						if (htmlDocumentShim.AssociatedWindow == unloadedWindow.NativeHtmlWindow)
						{
							this.htmlDocumentShims.Remove(htmlDocumentShim.Document);
							htmlDocumentShim.Dispose();
						}
					}
				}
				if (this.htmlElementShims != null)
				{
					HtmlElement.HtmlElementShim[] array3 = new HtmlElement.HtmlElementShim[this.htmlElementShims.Count];
					this.htmlElementShims.Values.CopyTo(array3, 0);
					foreach (HtmlElement.HtmlElementShim htmlElementShim in array3)
					{
						if (htmlElementShim.AssociatedWindow == unloadedWindow.NativeHtmlWindow)
						{
							this.htmlElementShims.Remove(htmlElementShim.Element);
							htmlElementShim.Dispose();
						}
					}
				}
				if (this.htmlWindowShims != null && this.htmlWindowShims.ContainsKey(unloadedWindow))
				{
					HtmlWindow.HtmlWindowShim htmlWindowShim = this.htmlWindowShims[unloadedWindow];
					this.htmlWindowShims.Remove(unloadedWindow);
					htmlWindowShim.Dispose();
				}
			}
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x000BD25E File Offset: 0x000BB45E
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000BD268 File Offset: 0x000BB468
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.htmlElementShims != null)
				{
					foreach (HtmlElement.HtmlElementShim htmlElementShim in this.htmlElementShims.Values)
					{
						htmlElementShim.Dispose();
					}
				}
				if (this.htmlDocumentShims != null)
				{
					foreach (HtmlDocument.HtmlDocumentShim htmlDocumentShim in this.htmlDocumentShims.Values)
					{
						htmlDocumentShim.Dispose();
					}
				}
				if (this.htmlWindowShims != null)
				{
					foreach (HtmlWindow.HtmlWindowShim htmlWindowShim in this.htmlWindowShims.Values)
					{
						htmlWindowShim.Dispose();
					}
				}
				this.htmlWindowShims = null;
				this.htmlDocumentShims = null;
				this.htmlWindowShims = null;
			}
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000BD384 File Offset: 0x000BB584
		~HtmlShimManager()
		{
			this.Dispose(false);
		}

		// Token: 0x040010D3 RID: 4307
		private Dictionary<HtmlWindow, HtmlWindow.HtmlWindowShim> htmlWindowShims;

		// Token: 0x040010D4 RID: 4308
		private Dictionary<HtmlElement, HtmlElement.HtmlElementShim> htmlElementShims;

		// Token: 0x040010D5 RID: 4309
		private Dictionary<HtmlDocument, HtmlDocument.HtmlDocumentShim> htmlDocumentShims;
	}
}
