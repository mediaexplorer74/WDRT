using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Manages the list of documents and Web sites the user has visited within the current session.</summary>
	// Token: 0x02000280 RID: 640
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class HtmlHistory : IDisposable
	{
		// Token: 0x0600290B RID: 10507 RVA: 0x000BCA72 File Offset: 0x000BAC72
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		internal HtmlHistory(UnsafeNativeMethods.IOmHistory history)
		{
			this.htmlHistory = history;
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x0600290C RID: 10508 RVA: 0x000BCA81 File Offset: 0x000BAC81
		private UnsafeNativeMethods.IOmHistory NativeOmHistory
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				return this.htmlHistory;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.HtmlHistory" />.</summary>
		// Token: 0x0600290D RID: 10509 RVA: 0x000BCAA2 File Offset: 0x000BACA2
		public void Dispose()
		{
			this.htmlHistory = null;
			this.disposed = true;
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets the size of the history stack.</summary>
		/// <returns>The current number of entries in the Uniform Resource Locator (URL) history.</returns>
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x0600290E RID: 10510 RVA: 0x000BCAB8 File Offset: 0x000BACB8
		public int Length
		{
			get
			{
				return (int)this.NativeOmHistory.GetLength();
			}
		}

		/// <summary>Navigates backward in the navigation stack by the specified number of entries.</summary>
		/// <param name="numberBack">The number of entries to navigate backward in the navigation stack. This number must be a positive integer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Argument is not a positive 32-bit integer.</exception>
		// Token: 0x0600290F RID: 10511 RVA: 0x000BCAC8 File Offset: 0x000BACC8
		public void Back(int numberBack)
		{
			if (numberBack < 0)
			{
				throw new ArgumentOutOfRangeException("numberBack", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"numberBack",
					numberBack.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (numberBack > 0)
			{
				object obj = -numberBack;
				this.NativeOmHistory.Go(ref obj);
			}
		}

		/// <summary>Navigates forward in the navigation stack by the specified number of entries.</summary>
		/// <param name="numberForward">The number of entries to navigate forward in the navigation stack. This number must be a positive integer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Argument is not a positive 32-bit integer.</exception>
		// Token: 0x06002910 RID: 10512 RVA: 0x000BCB38 File Offset: 0x000BAD38
		public void Forward(int numberForward)
		{
			if (numberForward < 0)
			{
				throw new ArgumentOutOfRangeException("numberForward", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"numberForward",
					numberForward.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (numberForward > 0)
			{
				object obj = numberForward;
				this.NativeOmHistory.Go(ref obj);
			}
		}

		/// <summary>Navigates to the specified Uniform Resource Locator (URL).</summary>
		/// <param name="url">The URL as a <see cref="T:System.Uri" /> object.</param>
		// Token: 0x06002911 RID: 10513 RVA: 0x000BCBA4 File Offset: 0x000BADA4
		public void Go(Uri url)
		{
			this.Go(url.ToString());
		}

		/// <summary>Navigates to the specified Uniform Resource Locator (URL).</summary>
		/// <param name="urlString">The URL you want to display. This may be a relative or virtual URL (for example, page.html, path/page.html, or /path/to/page.html), in which case the current Web page's URL is used as a base.</param>
		// Token: 0x06002912 RID: 10514 RVA: 0x000BCBB4 File Offset: 0x000BADB4
		public void Go(string urlString)
		{
			object obj = urlString;
			this.NativeOmHistory.Go(ref obj);
		}

		/// <summary>Navigates to the specified relative position in the browser's history.</summary>
		/// <param name="relativePosition">The entry in the navigation stack you want to display.</param>
		// Token: 0x06002913 RID: 10515 RVA: 0x000BCBD0 File Offset: 0x000BADD0
		public void Go(int relativePosition)
		{
			object obj = relativePosition;
			this.NativeOmHistory.Go(ref obj);
		}

		/// <summary>Gets the unmanaged interface wrapped by this class.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be cast into an <see langword="IOmHistory" /> interface pointer.</returns>
		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002914 RID: 10516 RVA: 0x000BCBF1 File Offset: 0x000BADF1
		public object DomHistory
		{
			get
			{
				return this.NativeOmHistory;
			}
		}

		// Token: 0x040010CA RID: 4298
		private UnsafeNativeMethods.IOmHistory htmlHistory;

		// Token: 0x040010CB RID: 4299
		private bool disposed;
	}
}
