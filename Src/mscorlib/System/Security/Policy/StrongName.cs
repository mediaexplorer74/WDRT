using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	/// <summary>Provides the strong name of a code assembly as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x0200036A RID: 874
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongName : EvidenceBase, IIdentityPermissionFactory, IDelayEvaluatedEvidence
	{
		// Token: 0x06002B68 RID: 11112 RVA: 0x000A2F58 File Offset: 0x000A1158
		internal StrongName()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.StrongName" /> class with the strong name public key blob, name, and version.</summary>
		/// <param name="blob">The <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the software publisher.</param>
		/// <param name="name">The simple name section of the strong name.</param>
		/// <param name="version">The <see cref="T:System.Version" /> of the strong name.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="blob" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="name" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="version" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string ("").</exception>
		// Token: 0x06002B69 RID: 11113 RVA: 0x000A2F60 File Offset: 0x000A1160
		public StrongName(StrongNamePublicKeyBlob blob, string name, Version version)
			: this(blob, name, version, null)
		{
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000A2F6C File Offset: 0x000A116C
		internal StrongName(StrongNamePublicKeyBlob blob, string name, Version version, Assembly assembly)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyStrongName"));
			}
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
			if (assembly != null && runtimeAssembly == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "assembly");
			}
			this.m_publicKeyBlob = blob;
			this.m_name = name;
			this.m_version = version;
			this.m_assembly = runtimeAssembly;
		}

		/// <summary>Gets the <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x000A3013 File Offset: 0x000A1213
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				return this.m_publicKeyBlob;
			}
		}

		/// <summary>Gets the simple name of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The simple name part of the <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x000A301B File Offset: 0x000A121B
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the <see cref="T:System.Version" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The <see cref="T:System.Version" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002B6D RID: 11117 RVA: 0x000A3023 File Offset: 0x000A1223
		public Version Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x000A302B File Offset: 0x000A122B
		bool IDelayEvaluatedEvidence.IsVerified
		{
			[SecurityCritical]
			get
			{
				return !(this.m_assembly != null) || this.m_assembly.IsStrongNameVerified;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06002B6F RID: 11119 RVA: 0x000A3048 File Offset: 0x000A1248
		bool IDelayEvaluatedEvidence.WasUsed
		{
			get
			{
				return this.m_wasUsed;
			}
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000A3050 File Offset: 0x000A1250
		void IDelayEvaluatedEvidence.MarkUsed()
		{
			this.m_wasUsed = true;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000A305C File Offset: 0x000A125C
		internal static bool CompareNames(string asmName, string mcName)
		{
			if (mcName.Length > 0 && mcName[mcName.Length - 1] == '*' && mcName.Length - 1 <= asmName.Length)
			{
				return string.Compare(mcName, 0, asmName, 0, mcName.Length - 1, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return string.Compare(mcName, asmName, StringComparison.OrdinalIgnoreCase) == 0;
		}

		/// <summary>Creates a <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> that corresponds to the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> from which to construct the <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" />.</param>
		/// <returns>A <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x06002B72 RID: 11122 RVA: 0x000A30B5 File Offset: 0x000A12B5
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new StrongNameIdentityPermission(this.m_publicKeyBlob, this.m_name, this.m_version);
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x06002B73 RID: 11123 RVA: 0x000A30CE File Offset: 0x000A12CE
		public override EvidenceBase Clone()
		{
			return new StrongName(this.m_publicKeyBlob, this.m_name, this.m_version);
		}

		/// <summary>Creates an equivalent copy of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>A new, identical copy of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x06002B74 RID: 11124 RVA: 0x000A30E7 File Offset: 0x000A12E7
		public object Copy()
		{
			return this.Clone();
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000A30F0 File Offset: 0x000A12F0
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("StrongName");
			securityElement.AddAttribute("version", "1");
			if (this.m_publicKeyBlob != null)
			{
				securityElement.AddAttribute("Key", Hex.EncodeHexString(this.m_publicKeyBlob.PublicKey));
			}
			if (this.m_name != null)
			{
				securityElement.AddAttribute("Name", this.m_name);
			}
			if (this.m_version != null)
			{
				securityElement.AddAttribute("Version", this.m_version.ToString());
			}
			return securityElement;
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000A317C File Offset: 0x000A137C
		internal void FromXml(SecurityElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (string.Compare(element.Tag, "StrongName", StringComparison.Ordinal) != 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
			}
			this.m_publicKeyBlob = null;
			this.m_version = null;
			string text = element.Attribute("Key");
			if (text != null)
			{
				this.m_publicKeyBlob = new StrongNamePublicKeyBlob(Hex.DecodeHexString(text));
			}
			this.m_name = element.Attribute("Name");
			string text2 = element.Attribute("Version");
			if (text2 != null)
			{
				this.m_version = new Version(text2);
			}
		}

		/// <summary>Creates a string representation of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x06002B77 RID: 11127 RVA: 0x000A3214 File Offset: 0x000A1414
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		/// <summary>Determines whether the specified strong name is equal to the current strong name.</summary>
		/// <param name="o">The strong name to compare against the current strong name.</param>
		/// <returns>
		///   <see langword="true" /> if the specified strong name is equal to the current strong name; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B78 RID: 11128 RVA: 0x000A3224 File Offset: 0x000A1424
		public override bool Equals(object o)
		{
			StrongName strongName = o as StrongName;
			return strongName != null && object.Equals(this.m_publicKeyBlob, strongName.m_publicKeyBlob) && object.Equals(this.m_name, strongName.m_name) && object.Equals(this.m_version, strongName.m_version);
		}

		/// <summary>Gets the hash code of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The hash code of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x06002B79 RID: 11129 RVA: 0x000A3274 File Offset: 0x000A1474
		public override int GetHashCode()
		{
			if (this.m_publicKeyBlob != null)
			{
				return this.m_publicKeyBlob.GetHashCode();
			}
			if (this.m_name != null || this.m_version != null)
			{
				return ((this.m_name == null) ? 0 : this.m_name.GetHashCode()) + ((this.m_version == null) ? 0 : this.m_version.GetHashCode());
			}
			return typeof(StrongName).GetHashCode();
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000A32F0 File Offset: 0x000A14F0
		internal object Normalize()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(this.m_publicKeyBlob.PublicKey);
			binaryWriter.Write(this.m_version.Major);
			binaryWriter.Write(this.m_name);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x040011A3 RID: 4515
		private StrongNamePublicKeyBlob m_publicKeyBlob;

		// Token: 0x040011A4 RID: 4516
		private string m_name;

		// Token: 0x040011A5 RID: 4517
		private Version m_version;

		// Token: 0x040011A6 RID: 4518
		[NonSerialized]
		private RuntimeAssembly m_assembly;

		// Token: 0x040011A7 RID: 4519
		[NonSerialized]
		private bool m_wasUsed;
	}
}
