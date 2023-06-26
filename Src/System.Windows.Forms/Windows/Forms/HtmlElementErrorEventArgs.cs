using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.HtmlWindow.Error" /> event.</summary>
	// Token: 0x0200027C RID: 636
	public sealed class HtmlElementErrorEventArgs : EventArgs
	{
		// Token: 0x060028EC RID: 10476 RVA: 0x000BC857 File Offset: 0x000BAA57
		internal HtmlElementErrorEventArgs(string description, string urlString, int lineNumber)
		{
			this.description = description;
			this.urlString = urlString;
			this.lineNumber = lineNumber;
		}

		/// <summary>Gets the descriptive string corresponding to the error.</summary>
		/// <returns>The descriptive string corresponding to the error.</returns>
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060028ED RID: 10477 RVA: 0x000BC874 File Offset: 0x000BAA74
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		/// <summary>Gets or sets whether this error has been handled by the application hosting the document.</summary>
		/// <returns>
		///   <see langword="true" /> if the event has been handled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x000BC87C File Offset: 0x000BAA7C
		// (set) Token: 0x060028EF RID: 10479 RVA: 0x000BC884 File Offset: 0x000BAA84
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		/// <summary>Gets the line of HTML script code on which the error occurred.</summary>
		/// <returns>An <see cref="T:System.Int32" /> designating the script line number.</returns>
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x000BC88D File Offset: 0x000BAA8D
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the location of the document that generated the error.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that represents the location of the document that generated the error.</returns>
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x000BC895 File Offset: 0x000BAA95
		public Uri Url
		{
			get
			{
				if (this.url == null)
				{
					this.url = new Uri(this.urlString);
				}
				return this.url;
			}
		}

		// Token: 0x040010C3 RID: 4291
		private string description;

		// Token: 0x040010C4 RID: 4292
		private string urlString;

		// Token: 0x040010C5 RID: 4293
		private Uri url;

		// Token: 0x040010C6 RID: 4294
		private int lineNumber;

		// Token: 0x040010C7 RID: 4295
		private bool handled;
	}
}
