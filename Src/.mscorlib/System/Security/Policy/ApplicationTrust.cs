using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	/// <summary>Encapsulates security decisions about an application. This class cannot be inherited.</summary>
	// Token: 0x02000344 RID: 836
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationTrust : EvidenceBase, ISecurityEncodable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationTrust" /> class with an <see cref="T:System.ApplicationIdentity" />.</summary>
		/// <param name="applicationIdentity">An <see cref="T:System.ApplicationIdentity" /> that uniquely identifies an application.</param>
		// Token: 0x060029AB RID: 10667 RVA: 0x0009B10B File Offset: 0x0009930B
		public ApplicationTrust(ApplicationIdentity applicationIdentity)
			: this()
		{
			this.ApplicationIdentity = applicationIdentity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationTrust" /> class.</summary>
		// Token: 0x060029AC RID: 10668 RVA: 0x0009B11A File Offset: 0x0009931A
		public ApplicationTrust()
			: this(new PermissionSet(PermissionState.None))
		{
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x0009B128 File Offset: 0x00099328
		internal ApplicationTrust(PermissionSet defaultGrantSet)
		{
			this.InitDefaultGrantSet(defaultGrantSet);
			this.m_fullTrustAssemblies = new List<StrongName>().AsReadOnly();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationTrust" /> class using the provided grant set and collection of full-trust assemblies.</summary>
		/// <param name="defaultGrantSet">A default permission set that is granted to all assemblies that do not have specific grants.</param>
		/// <param name="fullTrustAssemblies">An array of strong names that represent assemblies that should be considered fully trusted in an application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fullTrustAssemblies" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fullTrustAssemblies" /> contains an assembly that does not have a <see cref="T:System.Security.Policy.StrongName" />.</exception>
		// Token: 0x060029AE RID: 10670 RVA: 0x0009B148 File Offset: 0x00099348
		public ApplicationTrust(PermissionSet defaultGrantSet, IEnumerable<StrongName> fullTrustAssemblies)
		{
			if (fullTrustAssemblies == null)
			{
				throw new ArgumentNullException("fullTrustAssemblies");
			}
			this.InitDefaultGrantSet(defaultGrantSet);
			List<StrongName> list = new List<StrongName>();
			foreach (StrongName strongName in fullTrustAssemblies)
			{
				if (strongName == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NullFullTrustAssembly"));
				}
				list.Add(new StrongName(strongName.PublicKey, strongName.Name, strongName.Version));
			}
			this.m_fullTrustAssemblies = list.AsReadOnly();
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x0009B1E8 File Offset: 0x000993E8
		private void InitDefaultGrantSet(PermissionSet defaultGrantSet)
		{
			if (defaultGrantSet == null)
			{
				throw new ArgumentNullException("defaultGrantSet");
			}
			this.DefaultGrantSet = new PolicyStatement(defaultGrantSet);
		}

		/// <summary>Gets or sets the application identity for the application trust object.</summary>
		/// <returns>An <see cref="T:System.ApplicationIdentity" /> for the application trust object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="T:System.ApplicationIdentity" /> cannot be set because it has a value of <see langword="null" />.</exception>
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060029B0 RID: 10672 RVA: 0x0009B204 File Offset: 0x00099404
		// (set) Token: 0x060029B1 RID: 10673 RVA: 0x0009B20C File Offset: 0x0009940C
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return this.m_appId;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("Argument_InvalidAppId"));
				}
				this.m_appId = value;
			}
		}

		/// <summary>Gets or sets the policy statement defining the default grant set.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.PolicyStatement" /> describing the default grants.</returns>
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060029B2 RID: 10674 RVA: 0x0009B228 File Offset: 0x00099428
		// (set) Token: 0x060029B3 RID: 10675 RVA: 0x0009B244 File Offset: 0x00099444
		public PolicyStatement DefaultGrantSet
		{
			get
			{
				if (this.m_psDefaultGrant == null)
				{
					return new PolicyStatement(new PermissionSet(PermissionState.None));
				}
				return this.m_psDefaultGrant;
			}
			set
			{
				if (value == null)
				{
					this.m_psDefaultGrant = null;
					this.m_grantSetSpecialFlags = 0;
					return;
				}
				this.m_psDefaultGrant = value;
				this.m_grantSetSpecialFlags = SecurityManager.GetSpecialFlags(this.m_psDefaultGrant.PermissionSet, null);
			}
		}

		/// <summary>Gets the list of full-trust assemblies for this application trust.</summary>
		/// <returns>A list of full-trust assemblies.</returns>
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060029B4 RID: 10676 RVA: 0x0009B276 File Offset: 0x00099476
		public IList<StrongName> FullTrustAssemblies
		{
			get
			{
				return this.m_fullTrustAssemblies;
			}
		}

		/// <summary>Gets or sets a value indicating whether the application has the required permission grants and is trusted to run.</summary>
		/// <returns>
		///   <see langword="true" /> if the application is trusted to run; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060029B5 RID: 10677 RVA: 0x0009B27E File Offset: 0x0009947E
		// (set) Token: 0x060029B6 RID: 10678 RVA: 0x0009B286 File Offset: 0x00099486
		public bool IsApplicationTrustedToRun
		{
			get
			{
				return this.m_appTrustedToRun;
			}
			set
			{
				this.m_appTrustedToRun = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether application trust information is persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if application trust information is persisted; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060029B7 RID: 10679 RVA: 0x0009B28F File Offset: 0x0009948F
		// (set) Token: 0x060029B8 RID: 10680 RVA: 0x0009B297 File Offset: 0x00099497
		public bool Persist
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

		/// <summary>Gets or sets extra security information about the application.</summary>
		/// <returns>An object containing additional security information about the application.</returns>
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x0009B2A0 File Offset: 0x000994A0
		// (set) Token: 0x060029BA RID: 10682 RVA: 0x0009B2C8 File Offset: 0x000994C8
		public object ExtraInfo
		{
			get
			{
				if (this.m_elExtraInfo != null)
				{
					this.m_extraInfo = ApplicationTrust.ObjectFromXml(this.m_elExtraInfo);
					this.m_elExtraInfo = null;
				}
				return this.m_extraInfo;
			}
			set
			{
				this.m_elExtraInfo = null;
				this.m_extraInfo = value;
			}
		}

		/// <summary>Creates an XML encoding of the <see cref="T:System.Security.Policy.ApplicationTrust" /> object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x060029BB RID: 10683 RVA: 0x0009B2D8 File Offset: 0x000994D8
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("ApplicationTrust");
			securityElement.AddAttribute("version", "1");
			if (this.m_appId != null)
			{
				securityElement.AddAttribute("FullName", SecurityElement.Escape(this.m_appId.FullName));
			}
			if (this.m_appTrustedToRun)
			{
				securityElement.AddAttribute("TrustedToRun", "true");
			}
			if (this.m_persist)
			{
				securityElement.AddAttribute("Persist", "true");
			}
			if (this.m_psDefaultGrant != null)
			{
				SecurityElement securityElement2 = new SecurityElement("DefaultGrant");
				securityElement2.AddChild(this.m_psDefaultGrant.ToXml());
				securityElement.AddChild(securityElement2);
			}
			if (this.m_fullTrustAssemblies.Count > 0)
			{
				SecurityElement securityElement3 = new SecurityElement("FullTrustAssemblies");
				foreach (StrongName strongName in this.m_fullTrustAssemblies)
				{
					securityElement3.AddChild(strongName.ToXml());
				}
				securityElement.AddChild(securityElement3);
			}
			if (this.ExtraInfo != null)
			{
				securityElement.AddChild(ApplicationTrust.ObjectToXml("ExtraInfo", this.ExtraInfo));
			}
			return securityElement;
		}

		/// <summary>Reconstructs an <see cref="T:System.Security.Policy.ApplicationTrust" /> object with a given state from an XML encoding.</summary>
		/// <param name="element">The XML encoding to use to reconstruct the <see cref="T:System.Security.Policy.ApplicationTrust" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The XML encoding used for <paramref name="element" /> is invalid.</exception>
		// Token: 0x060029BC RID: 10684 RVA: 0x0009B404 File Offset: 0x00099604
		public void FromXml(SecurityElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (string.Compare(element.Tag, "ApplicationTrust", StringComparison.Ordinal) != 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
			}
			this.m_appTrustedToRun = false;
			string text = element.Attribute("TrustedToRun");
			if (text != null && string.Compare(text, "true", StringComparison.Ordinal) == 0)
			{
				this.m_appTrustedToRun = true;
			}
			this.m_persist = false;
			string text2 = element.Attribute("Persist");
			if (text2 != null && string.Compare(text2, "true", StringComparison.Ordinal) == 0)
			{
				this.m_persist = true;
			}
			this.m_appId = null;
			string text3 = element.Attribute("FullName");
			if (text3 != null && text3.Length > 0)
			{
				this.m_appId = new ApplicationIdentity(text3);
			}
			this.m_psDefaultGrant = null;
			this.m_grantSetSpecialFlags = 0;
			SecurityElement securityElement = element.SearchForChildByTag("DefaultGrant");
			if (securityElement != null)
			{
				SecurityElement securityElement2 = securityElement.SearchForChildByTag("PolicyStatement");
				if (securityElement2 != null)
				{
					PolicyStatement policyStatement = new PolicyStatement(null);
					policyStatement.FromXml(securityElement2);
					this.m_psDefaultGrant = policyStatement;
					this.m_grantSetSpecialFlags = SecurityManager.GetSpecialFlags(policyStatement.PermissionSet, null);
				}
			}
			List<StrongName> list = new List<StrongName>();
			SecurityElement securityElement3 = element.SearchForChildByTag("FullTrustAssemblies");
			if (securityElement3 != null && securityElement3.InternalChildren != null)
			{
				IEnumerator enumerator = securityElement3.Children.GetEnumerator();
				while (enumerator.MoveNext())
				{
					StrongName strongName = new StrongName();
					strongName.FromXml(enumerator.Current as SecurityElement);
					list.Add(strongName);
				}
			}
			this.m_fullTrustAssemblies = list.AsReadOnly();
			this.m_elExtraInfo = element.SearchForChildByTag("ExtraInfo");
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x0009B598 File Offset: 0x00099798
		private static SecurityElement ObjectToXml(string tag, object obj)
		{
			ISecurityEncodable securityEncodable = obj as ISecurityEncodable;
			SecurityElement securityElement;
			if (securityEncodable != null)
			{
				securityElement = securityEncodable.ToXml();
				if (!securityElement.Tag.Equals(tag))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
				}
			}
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, obj);
			byte[] array = memoryStream.ToArray();
			securityElement = new SecurityElement(tag);
			securityElement.AddAttribute("Data", Hex.EncodeHexString(array));
			return securityElement;
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x0009B60C File Offset: 0x0009980C
		private static object ObjectFromXml(SecurityElement elObject)
		{
			if (elObject.Attribute("class") != null)
			{
				ISecurityEncodable securityEncodable = XMLUtil.CreateCodeGroup(elObject) as ISecurityEncodable;
				if (securityEncodable != null)
				{
					securityEncodable.FromXml(elObject);
					return securityEncodable;
				}
			}
			string text = elObject.Attribute("Data");
			MemoryStream memoryStream = new MemoryStream(Hex.DecodeHexString(text));
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			return binaryFormatter.Deserialize(memoryStream);
		}

		/// <summary>Creates a new object that is a complete copy of the current instance.</summary>
		/// <returns>A duplicate copy of this application trust object.</returns>
		// Token: 0x060029BF RID: 10687 RVA: 0x0009B663 File Offset: 0x00099863
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override EvidenceBase Clone()
		{
			return base.Clone();
		}

		// Token: 0x0400111C RID: 4380
		private ApplicationIdentity m_appId;

		// Token: 0x0400111D RID: 4381
		private bool m_appTrustedToRun;

		// Token: 0x0400111E RID: 4382
		private bool m_persist;

		// Token: 0x0400111F RID: 4383
		private object m_extraInfo;

		// Token: 0x04001120 RID: 4384
		private SecurityElement m_elExtraInfo;

		// Token: 0x04001121 RID: 4385
		private PolicyStatement m_psDefaultGrant;

		// Token: 0x04001122 RID: 4386
		private IList<StrongName> m_fullTrustAssemblies;

		// Token: 0x04001123 RID: 4387
		[NonSerialized]
		private int m_grantSetSpecialFlags;
	}
}
