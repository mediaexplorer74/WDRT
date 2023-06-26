using System;
using System.Deployment.Internal.Isolation;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Threading;

namespace System.Security.Policy
{
	/// <summary>Holds the security evidence for an application. This class cannot be inherited.</summary>
	// Token: 0x02000341 RID: 833
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public sealed class ApplicationSecurityInfo
	{
		// Token: 0x06002998 RID: 10648 RVA: 0x0009A9EC File Offset: 0x00098BEC
		internal ApplicationSecurityInfo()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationSecurityInfo" /> class using the provided activation context.</summary>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> object that uniquely identifies the target application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is <see langword="null" />.</exception>
		// Token: 0x06002999 RID: 10649 RVA: 0x0009A9F4 File Offset: 0x00098BF4
		public ApplicationSecurityInfo(ActivationContext activationContext)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			this.m_context = activationContext;
		}

		/// <summary>Gets or sets the application identity information.</summary>
		/// <returns>An <see cref="T:System.ApplicationId" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.ApplicationId" /> is set to <see langword="null" />.</exception>
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x0009AA14 File Offset: 0x00098C14
		// (set) Token: 0x0600299B RID: 10651 RVA: 0x0009AA5D File Offset: 0x00098C5D
		public ApplicationId ApplicationId
		{
			get
			{
				if (this.m_appId == null && this.m_context != null)
				{
					ICMS applicationComponentManifest = this.m_context.ApplicationComponentManifest;
					ApplicationId applicationId = ApplicationSecurityInfo.ParseApplicationId(applicationComponentManifest);
					Interlocked.CompareExchange(ref this.m_appId, applicationId, null);
				}
				return this.m_appId as ApplicationId;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_appId = value;
			}
		}

		/// <summary>Gets or sets the top element in the application, which is described in the deployment identity.</summary>
		/// <returns>An <see cref="T:System.ApplicationId" /> object describing the top element of the application.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.DeploymentId" /> is set to <see langword="null" />.</exception>
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x0009AA74 File Offset: 0x00098C74
		// (set) Token: 0x0600299D RID: 10653 RVA: 0x0009AABD File Offset: 0x00098CBD
		public ApplicationId DeploymentId
		{
			get
			{
				if (this.m_deployId == null && this.m_context != null)
				{
					ICMS deploymentComponentManifest = this.m_context.DeploymentComponentManifest;
					ApplicationId applicationId = ApplicationSecurityInfo.ParseApplicationId(deploymentComponentManifest);
					Interlocked.CompareExchange(ref this.m_deployId, applicationId, null);
				}
				return this.m_deployId as ApplicationId;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_deployId = value;
			}
		}

		/// <summary>Gets or sets the default permission set.</summary>
		/// <returns>A <see cref="T:System.Security.PermissionSet" /> object representing the default permissions for the application. The default is a <see cref="T:System.Security.PermissionSet" /> with a permission state of <see cref="F:System.Security.Permissions.PermissionState.None" /></returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.DefaultRequestSet" /> is set to <see langword="null" />.</exception>
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x0009AAD4 File Offset: 0x00098CD4
		// (set) Token: 0x0600299F RID: 10655 RVA: 0x0009AC88 File Offset: 0x00098E88
		public PermissionSet DefaultRequestSet
		{
			get
			{
				if (this.m_defaultRequest == null)
				{
					PermissionSet permissionSet = new PermissionSet(PermissionState.None);
					if (this.m_context != null)
					{
						ICMS applicationComponentManifest = this.m_context.ApplicationComponentManifest;
						string defaultPermissionSetID = ((IMetadataSectionEntry)applicationComponentManifest.MetadataSectionEntry).defaultPermissionSetID;
						object obj = null;
						if (defaultPermissionSetID != null && defaultPermissionSetID.Length > 0)
						{
							((ISectionWithStringKey)applicationComponentManifest.PermissionSetSection).Lookup(defaultPermissionSetID, out obj);
							IPermissionSetEntry permissionSetEntry = obj as IPermissionSetEntry;
							if (permissionSetEntry != null)
							{
								SecurityElement securityElement = SecurityElement.FromString(permissionSetEntry.AllData.XmlSegment);
								string text = securityElement.Attribute("temp:Unrestricted");
								if (text != null)
								{
									securityElement.AddAttribute("Unrestricted", text);
								}
								string text2 = securityElement.Attribute("SameSite");
								if (string.Compare(text2, "Site", StringComparison.OrdinalIgnoreCase) == 0)
								{
									Url url = new Url(this.m_context.Identity.CodeBase);
									URLString urlstring = url.GetURLString();
									NetCodeGroup netCodeGroup = new NetCodeGroup(new AllMembershipCondition());
									SecurityElement securityElement2 = netCodeGroup.CreateWebPermission(urlstring.Host, urlstring.Scheme, urlstring.Port, "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
									if (securityElement2 != null)
									{
										securityElement.AddChild(securityElement2);
									}
									if (string.Compare("file:", 0, this.m_context.Identity.CodeBase, 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
									{
										FileCodeGroup fileCodeGroup = new FileCodeGroup(new AllMembershipCondition(), FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery);
										PolicyStatement policyStatement = fileCodeGroup.CalculatePolicy(url);
										if (policyStatement != null)
										{
											PermissionSet permissionSet2 = policyStatement.PermissionSet;
											if (permissionSet2 != null)
											{
												securityElement.AddChild(permissionSet2.GetPermission(typeof(FileIOPermission)).ToXml());
											}
										}
									}
								}
								permissionSet = new ReadOnlyPermissionSet(securityElement);
							}
						}
					}
					Interlocked.CompareExchange(ref this.m_defaultRequest, permissionSet, null);
				}
				return this.m_defaultRequest as PermissionSet;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_defaultRequest = value;
			}
		}

		/// <summary>Gets or sets the evidence for the application.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object for the application.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.ApplicationEvidence" /> is set to <see langword="null" />.</exception>
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060029A0 RID: 10656 RVA: 0x0009ACA0 File Offset: 0x00098EA0
		// (set) Token: 0x060029A1 RID: 10657 RVA: 0x0009AD9D File Offset: 0x00098F9D
		public Evidence ApplicationEvidence
		{
			get
			{
				if (this.m_appEvidence == null)
				{
					Evidence evidence = new Evidence();
					if (this.m_context != null)
					{
						evidence = new Evidence();
						Url url = new Url(this.m_context.Identity.CodeBase);
						evidence.AddHostEvidence<Url>(url);
						evidence.AddHostEvidence<Zone>(Zone.CreateFromUrl(this.m_context.Identity.CodeBase));
						if (string.Compare("file:", 0, this.m_context.Identity.CodeBase, 0, 5, StringComparison.OrdinalIgnoreCase) != 0)
						{
							evidence.AddHostEvidence<Site>(Site.CreateFromUrl(this.m_context.Identity.CodeBase));
						}
						evidence.AddHostEvidence<StrongName>(new StrongName(new StrongNamePublicKeyBlob(this.DeploymentId.m_publicKeyToken), this.DeploymentId.Name, this.DeploymentId.Version));
						evidence.AddHostEvidence<ActivationArguments>(new ActivationArguments(this.m_context));
					}
					Interlocked.CompareExchange(ref this.m_appEvidence, evidence, null);
				}
				return this.m_appEvidence as Evidence;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_appEvidence = value;
			}
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x0009ADB4 File Offset: 0x00098FB4
		private static ApplicationId ParseApplicationId(ICMS manifest)
		{
			if (manifest.Identity == null)
			{
				return null;
			}
			return new ApplicationId(Hex.DecodeHexString(manifest.Identity.GetAttribute("", "publicKeyToken")), manifest.Identity.GetAttribute("", "name"), new Version(manifest.Identity.GetAttribute("", "version")), manifest.Identity.GetAttribute("", "processorArchitecture"), manifest.Identity.GetAttribute("", "culture"));
		}

		// Token: 0x04001112 RID: 4370
		private ActivationContext m_context;

		// Token: 0x04001113 RID: 4371
		private object m_appId;

		// Token: 0x04001114 RID: 4372
		private object m_deployId;

		// Token: 0x04001115 RID: 4373
		private object m_defaultRequest;

		// Token: 0x04001116 RID: 4374
		private object m_appEvidence;
	}
}
