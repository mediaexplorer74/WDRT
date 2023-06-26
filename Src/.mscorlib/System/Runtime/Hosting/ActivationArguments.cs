using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
	/// <summary>Provides data for manifest-based activation of an application. This class cannot be inherited.</summary>
	// Token: 0x02000A55 RID: 2645
	[ComVisible(true)]
	[Serializable]
	public sealed class ActivationArguments : EvidenceBase
	{
		// Token: 0x060066DF RID: 26335 RVA: 0x0015B79F File Offset: 0x0015999F
		private ActivationArguments()
		{
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x060066E0 RID: 26336 RVA: 0x0015B7A7 File Offset: 0x001599A7
		internal bool UseFusionActivationContext
		{
			get
			{
				return this.m_useFusionActivationContext;
			}
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x060066E1 RID: 26337 RVA: 0x0015B7AF File Offset: 0x001599AF
		// (set) Token: 0x060066E2 RID: 26338 RVA: 0x0015B7B7 File Offset: 0x001599B7
		internal bool ActivateInstance
		{
			get
			{
				return this.m_activateInstance;
			}
			set
			{
				this.m_activateInstance = value;
			}
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x060066E3 RID: 26339 RVA: 0x0015B7C0 File Offset: 0x001599C0
		internal string ApplicationFullName
		{
			get
			{
				return this.m_appFullName;
			}
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x060066E4 RID: 26340 RVA: 0x0015B7C8 File Offset: 0x001599C8
		internal string[] ApplicationManifestPaths
		{
			get
			{
				return this.m_appManifestPaths;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified application identity.</summary>
		/// <param name="applicationIdentity">An object that identifies the manifest-based activation application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationIdentity" /> is <see langword="null" />.</exception>
		// Token: 0x060066E5 RID: 26341 RVA: 0x0015B7D0 File Offset: 0x001599D0
		public ActivationArguments(ApplicationIdentity applicationIdentity)
			: this(applicationIdentity, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified application identity and activation data.</summary>
		/// <param name="applicationIdentity">An object that identifies the manifest-based activation application.</param>
		/// <param name="activationData">An array of strings containing host-provided activation data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationIdentity" /> is <see langword="null" />.</exception>
		// Token: 0x060066E6 RID: 26342 RVA: 0x0015B7DA File Offset: 0x001599DA
		public ActivationArguments(ApplicationIdentity applicationIdentity, string[] activationData)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			this.m_appFullName = applicationIdentity.FullName;
			this.m_activationData = activationData;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified activation context.</summary>
		/// <param name="activationData">An object that identifies the manifest-based activation application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationData" /> is <see langword="null" />.</exception>
		// Token: 0x060066E7 RID: 26343 RVA: 0x0015B803 File Offset: 0x00159A03
		public ActivationArguments(ActivationContext activationData)
			: this(activationData, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified activation context and activation data.</summary>
		/// <param name="activationContext">An object that identifies the manifest-based activation application.</param>
		/// <param name="activationData">An array of strings containing host-provided activation data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is <see langword="null" />.</exception>
		// Token: 0x060066E8 RID: 26344 RVA: 0x0015B810 File Offset: 0x00159A10
		public ActivationArguments(ActivationContext activationContext, string[] activationData)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			this.m_appFullName = activationContext.Identity.FullName;
			this.m_appManifestPaths = activationContext.ManifestPaths;
			this.m_activationData = activationData;
			this.m_useFusionActivationContext = true;
		}

		// Token: 0x060066E9 RID: 26345 RVA: 0x0015B85C File Offset: 0x00159A5C
		internal ActivationArguments(string appFullName, string[] appManifestPaths, string[] activationData)
		{
			if (appFullName == null)
			{
				throw new ArgumentNullException("appFullName");
			}
			this.m_appFullName = appFullName;
			this.m_appManifestPaths = appManifestPaths;
			this.m_activationData = activationData;
			this.m_useFusionActivationContext = true;
		}

		/// <summary>Gets the application identity for a manifest-activated application.</summary>
		/// <returns>An object that identifies an application for manifest-based activation.</returns>
		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x060066EA RID: 26346 RVA: 0x0015B88E File Offset: 0x00159A8E
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return new ApplicationIdentity(this.m_appFullName);
			}
		}

		/// <summary>Gets the activation context for manifest-based activation of an application.</summary>
		/// <returns>An object that identifies a manifest-based activation application.</returns>
		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x060066EB RID: 26347 RVA: 0x0015B89B File Offset: 0x00159A9B
		public ActivationContext ActivationContext
		{
			get
			{
				if (!this.UseFusionActivationContext)
				{
					return null;
				}
				if (this.m_appManifestPaths == null)
				{
					return new ActivationContext(new ApplicationIdentity(this.m_appFullName));
				}
				return new ActivationContext(new ApplicationIdentity(this.m_appFullName), this.m_appManifestPaths);
			}
		}

		/// <summary>Gets activation data from the host.</summary>
		/// <returns>An array of strings containing host-provided activation data.</returns>
		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x060066EC RID: 26348 RVA: 0x0015B8D6 File Offset: 0x00159AD6
		public string[] ActivationData
		{
			get
			{
				return this.m_activationData;
			}
		}

		/// <summary>Produces a copy of the current <see cref="T:System.Runtime.Hosting.ActivationArguments" /> object.</summary>
		/// <returns>A copy of the current object.</returns>
		// Token: 0x060066ED RID: 26349 RVA: 0x0015B8E0 File Offset: 0x00159AE0
		public override EvidenceBase Clone()
		{
			ActivationArguments activationArguments = new ActivationArguments();
			activationArguments.m_useFusionActivationContext = this.m_useFusionActivationContext;
			activationArguments.m_activateInstance = this.m_activateInstance;
			activationArguments.m_appFullName = this.m_appFullName;
			if (this.m_appManifestPaths != null)
			{
				activationArguments.m_appManifestPaths = new string[this.m_appManifestPaths.Length];
				Array.Copy(this.m_appManifestPaths, activationArguments.m_appManifestPaths, activationArguments.m_appManifestPaths.Length);
			}
			if (this.m_activationData != null)
			{
				activationArguments.m_activationData = new string[this.m_activationData.Length];
				Array.Copy(this.m_activationData, activationArguments.m_activationData, activationArguments.m_activationData.Length);
			}
			activationArguments.m_activateInstance = this.m_activateInstance;
			activationArguments.m_appFullName = this.m_appFullName;
			activationArguments.m_useFusionActivationContext = this.m_useFusionActivationContext;
			return activationArguments;
		}

		// Token: 0x04002E1F RID: 11807
		private bool m_useFusionActivationContext;

		// Token: 0x04002E20 RID: 11808
		private bool m_activateInstance;

		// Token: 0x04002E21 RID: 11809
		private string m_appFullName;

		// Token: 0x04002E22 RID: 11810
		private string[] m_appManifestPaths;

		// Token: 0x04002E23 RID: 11811
		private string[] m_activationData;
	}
}
