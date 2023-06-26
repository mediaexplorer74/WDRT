using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Policy
{
	// Token: 0x0200035F RID: 863
	internal sealed class PEFileEvidenceFactory : IRuntimeEvidenceFactory
	{
		// Token: 0x06002AD2 RID: 10962 RVA: 0x0009FC72 File Offset: 0x0009DE72
		[SecurityCritical]
		private PEFileEvidenceFactory(SafePEFileHandle peFile)
		{
			this.m_peFile = peFile;
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x0009FC81 File Offset: 0x0009DE81
		internal SafePEFileHandle PEFile
		{
			[SecurityCritical]
			get
			{
				return this.m_peFile;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x0009FC89 File Offset: 0x0009DE89
		public IEvidenceFactory Target
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x0009FC8C File Offset: 0x0009DE8C
		[SecurityCritical]
		private static Evidence CreateSecurityIdentity(SafePEFileHandle peFile, Evidence hostProvidedEvidence)
		{
			PEFileEvidenceFactory pefileEvidenceFactory = new PEFileEvidenceFactory(peFile);
			Evidence evidence = new Evidence(pefileEvidenceFactory);
			if (hostProvidedEvidence != null)
			{
				evidence.MergeWithNoDuplicates(hostProvidedEvidence);
			}
			return evidence;
		}

		// Token: 0x06002AD6 RID: 10966
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void FireEvidenceGeneratedEvent(SafePEFileHandle peFile, EvidenceTypeGenerated type);

		// Token: 0x06002AD7 RID: 10967 RVA: 0x0009FCB2 File Offset: 0x0009DEB2
		[SecuritySafeCritical]
		internal void FireEvidenceGeneratedEvent(EvidenceTypeGenerated type)
		{
			PEFileEvidenceFactory.FireEvidenceGeneratedEvent(this.m_peFile, type);
		}

		// Token: 0x06002AD8 RID: 10968
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetAssemblySuppliedEvidence(SafePEFileHandle peFile, ObjectHandleOnStack retSerializedEvidence);

		// Token: 0x06002AD9 RID: 10969 RVA: 0x0009FCC0 File Offset: 0x0009DEC0
		[SecuritySafeCritical]
		public IEnumerable<EvidenceBase> GetFactorySuppliedEvidence()
		{
			if (this.m_assemblyProvidedEvidence == null)
			{
				byte[] array = null;
				PEFileEvidenceFactory.GetAssemblySuppliedEvidence(this.m_peFile, JitHelpers.GetObjectHandleOnStack<byte[]>(ref array));
				this.m_assemblyProvidedEvidence = new List<EvidenceBase>();
				if (array != null)
				{
					Evidence evidence = new Evidence();
					new SecurityPermission(SecurityPermissionFlag.SerializationFormatter).Assert();
					try
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						using (MemoryStream memoryStream = new MemoryStream(array))
						{
							evidence = (Evidence)binaryFormatter.Deserialize(memoryStream);
						}
					}
					catch
					{
					}
					CodeAccessPermission.RevertAssert();
					if (evidence != null)
					{
						IEnumerator assemblyEnumerator = evidence.GetAssemblyEnumerator();
						while (assemblyEnumerator.MoveNext())
						{
							if (assemblyEnumerator.Current != null)
							{
								EvidenceBase evidenceBase = assemblyEnumerator.Current as EvidenceBase;
								if (evidenceBase == null)
								{
									evidenceBase = new LegacyEvidenceWrapper(assemblyEnumerator.Current);
								}
								this.m_assemblyProvidedEvidence.Add(evidenceBase);
							}
						}
					}
				}
			}
			return this.m_assemblyProvidedEvidence;
		}

		// Token: 0x06002ADA RID: 10970
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetLocationEvidence(SafePEFileHandle peFile, out SecurityZone zone, StringHandleOnStack retUrl);

		// Token: 0x06002ADB RID: 10971
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetPublisherCertificate(SafePEFileHandle peFile, ObjectHandleOnStack retCertificate);

		// Token: 0x06002ADC RID: 10972 RVA: 0x0009FDB0 File Offset: 0x0009DFB0
		public EvidenceBase GenerateEvidence(Type evidenceType)
		{
			if (evidenceType == typeof(Site))
			{
				return this.GenerateSiteEvidence();
			}
			if (evidenceType == typeof(Url))
			{
				return this.GenerateUrlEvidence();
			}
			if (evidenceType == typeof(Zone))
			{
				return this.GenerateZoneEvidence();
			}
			if (evidenceType == typeof(Publisher))
			{
				return this.GeneratePublisherEvidence();
			}
			return null;
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x0009FE24 File Offset: 0x0009E024
		[SecuritySafeCritical]
		private void GenerateLocationEvidence()
		{
			if (!this.m_generatedLocationEvidence)
			{
				SecurityZone securityZone = SecurityZone.NoZone;
				string text = null;
				PEFileEvidenceFactory.GetLocationEvidence(this.m_peFile, out securityZone, JitHelpers.GetStringHandleOnStack(ref text));
				if (securityZone != SecurityZone.NoZone)
				{
					this.m_zoneEvidence = new Zone(securityZone);
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.m_urlEvidence = new Url(text, true);
					if (!text.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
					{
						this.m_siteEvidence = Site.CreateFromUrl(text);
					}
				}
				this.m_generatedLocationEvidence = true;
			}
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x0009FE98 File Offset: 0x0009E098
		[SecuritySafeCritical]
		private Publisher GeneratePublisherEvidence()
		{
			byte[] array = null;
			PEFileEvidenceFactory.GetPublisherCertificate(this.m_peFile, JitHelpers.GetObjectHandleOnStack<byte[]>(ref array));
			if (array == null)
			{
				return null;
			}
			return new Publisher(new X509Certificate(array));
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x0009FEC9 File Offset: 0x0009E0C9
		private Site GenerateSiteEvidence()
		{
			if (this.m_siteEvidence == null)
			{
				this.GenerateLocationEvidence();
			}
			return this.m_siteEvidence;
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x0009FEDF File Offset: 0x0009E0DF
		private Url GenerateUrlEvidence()
		{
			if (this.m_urlEvidence == null)
			{
				this.GenerateLocationEvidence();
			}
			return this.m_urlEvidence;
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x0009FEF5 File Offset: 0x0009E0F5
		private Zone GenerateZoneEvidence()
		{
			if (this.m_zoneEvidence == null)
			{
				this.GenerateLocationEvidence();
			}
			return this.m_zoneEvidence;
		}

		// Token: 0x04001171 RID: 4465
		[SecurityCritical]
		private SafePEFileHandle m_peFile;

		// Token: 0x04001172 RID: 4466
		private List<EvidenceBase> m_assemblyProvidedEvidence;

		// Token: 0x04001173 RID: 4467
		private bool m_generatedLocationEvidence;

		// Token: 0x04001174 RID: 4468
		private Site m_siteEvidence;

		// Token: 0x04001175 RID: 4469
		private Url m_urlEvidence;

		// Token: 0x04001176 RID: 4470
		private Zone m_zoneEvidence;
	}
}
