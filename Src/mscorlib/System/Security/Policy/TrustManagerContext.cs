using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Represents the context for the trust manager to consider when making the decision to run an application, and when setting up the security on a new <see cref="T:System.AppDomain" /> in which to run an application.</summary>
	// Token: 0x0200035B RID: 859
	[ComVisible(true)]
	public class TrustManagerContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.TrustManagerContext" /> class.</summary>
		// Token: 0x06002A94 RID: 10900 RVA: 0x0009E98A File Offset: 0x0009CB8A
		public TrustManagerContext()
			: this(TrustManagerUIContext.Run)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.TrustManagerContext" /> class using the specified <see cref="T:System.Security.Policy.TrustManagerUIContext" /> object.</summary>
		/// <param name="uiContext">One of the <see cref="T:System.Security.Policy.TrustManagerUIContext" /> values that specifies the type of trust manager user interface to use.</param>
		// Token: 0x06002A95 RID: 10901 RVA: 0x0009E993 File Offset: 0x0009CB93
		public TrustManagerContext(TrustManagerUIContext uiContext)
		{
			this.m_ignorePersistedDecision = false;
			this.m_uiContext = uiContext;
			this.m_keepAlive = false;
			this.m_persist = true;
		}

		/// <summary>Gets or sets the type of user interface the trust manager should display.</summary>
		/// <returns>One of the <see cref="T:System.Security.Policy.TrustManagerUIContext" /> values. The default is <see cref="F:System.Security.Policy.TrustManagerUIContext.Run" />.</returns>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x0009E9B7 File Offset: 0x0009CBB7
		// (set) Token: 0x06002A97 RID: 10903 RVA: 0x0009E9BF File Offset: 0x0009CBBF
		public virtual TrustManagerUIContext UIContext
		{
			get
			{
				return this.m_uiContext;
			}
			set
			{
				this.m_uiContext = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the trust manager should prompt the user for trust decisions.</summary>
		/// <returns>
		///   <see langword="true" /> to not prompt the user; <see langword="false" /> to prompt the user. The default is <see langword="false" />.</returns>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x0009E9C8 File Offset: 0x0009CBC8
		// (set) Token: 0x06002A99 RID: 10905 RVA: 0x0009E9D0 File Offset: 0x0009CBD0
		public virtual bool NoPrompt
		{
			get
			{
				return this.m_noPrompt;
			}
			set
			{
				this.m_noPrompt = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the application security manager should ignore any persisted decisions and call the trust manager.</summary>
		/// <returns>
		///   <see langword="true" /> to call the trust manager; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x0009E9D9 File Offset: 0x0009CBD9
		// (set) Token: 0x06002A9B RID: 10907 RVA: 0x0009E9E1 File Offset: 0x0009CBE1
		public virtual bool IgnorePersistedDecision
		{
			get
			{
				return this.m_ignorePersistedDecision;
			}
			set
			{
				this.m_ignorePersistedDecision = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the trust manager should cache state for this application, to facilitate future requests to determine application trust.</summary>
		/// <returns>
		///   <see langword="true" /> to cache state data; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x0009E9EA File Offset: 0x0009CBEA
		// (set) Token: 0x06002A9D RID: 10909 RVA: 0x0009E9F2 File Offset: 0x0009CBF2
		public virtual bool KeepAlive
		{
			get
			{
				return this.m_keepAlive;
			}
			set
			{
				this.m_keepAlive = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user's response to the consent dialog should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> to cache state data; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x0009E9FB File Offset: 0x0009CBFB
		// (set) Token: 0x06002A9F RID: 10911 RVA: 0x0009EA03 File Offset: 0x0009CC03
		public virtual bool Persist
		{
			get
			{
				return this.m_persist;
			}
			set
			{
				this.m_persist = value;
			}
		}

		/// <summary>Gets or sets the identity of the previous application identity.</summary>
		/// <returns>An <see cref="T:System.ApplicationIdentity" /> object representing the previous <see cref="T:System.ApplicationIdentity" />.</returns>
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x0009EA0C File Offset: 0x0009CC0C
		// (set) Token: 0x06002AA1 RID: 10913 RVA: 0x0009EA14 File Offset: 0x0009CC14
		public virtual ApplicationIdentity PreviousApplicationIdentity
		{
			get
			{
				return this.m_appId;
			}
			set
			{
				this.m_appId = value;
			}
		}

		// Token: 0x0400114F RID: 4431
		private bool m_ignorePersistedDecision;

		// Token: 0x04001150 RID: 4432
		private TrustManagerUIContext m_uiContext;

		// Token: 0x04001151 RID: 4433
		private bool m_noPrompt;

		// Token: 0x04001152 RID: 4434
		private bool m_keepAlive;

		// Token: 0x04001153 RID: 4435
		private bool m_persist;

		// Token: 0x04001154 RID: 4436
		private ApplicationIdentity m_appId;
	}
}
