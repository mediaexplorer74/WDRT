using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace System.Deployment.Internal.CodeSigning
{
	// Token: 0x0200000C RID: 12
	internal class SignedCmiManifest
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002843 File Offset: 0x00000A43
		private SignedCmiManifest()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000284B File Offset: 0x00000A4B
		internal SignedCmiManifest(XmlDocument manifestDom)
		{
			if (manifestDom == null)
			{
				throw new ArgumentNullException("manifestDom");
			}
			this.m_manifestDom = manifestDom;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002868 File Offset: 0x00000A68
		internal void Sign(CmiManifestSigner signer)
		{
			this.Sign(signer, null);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002874 File Offset: 0x00000A74
		internal void Sign(CmiManifestSigner signer, string timeStampUrl)
		{
			this.m_strongNameSignerInfo = null;
			this.m_authenticodeSignerInfo = null;
			if (signer == null || signer.StrongNameKey == null)
			{
				throw new ArgumentNullException("signer");
			}
			SignedCmiManifest.RemoveExistingSignature(this.m_manifestDom);
			if ((signer.Flag & CmiManifestSignerFlag.DontReplacePublicKeyToken) == CmiManifestSignerFlag.None)
			{
				SignedCmiManifest.ReplacePublicKeyToken(this.m_manifestDom, signer.StrongNameKey);
			}
			XmlDocument xmlDocument = null;
			if (signer.Certificate != null)
			{
				SignedCmiManifest.InsertPublisherIdentity(this.m_manifestDom, signer.Certificate);
				xmlDocument = SignedCmiManifest.CreateLicenseDom(signer, this.ExtractPrincipalFromManifest(), SignedCmiManifest.ComputeHashFromManifest(this.m_manifestDom));
				SignedCmiManifest.AuthenticodeSignLicenseDom(xmlDocument, signer, timeStampUrl);
			}
			SignedCmiManifest.StrongNameSignManifestDom(this.m_manifestDom, xmlDocument, signer);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002914 File Offset: 0x00000B14
		internal void Verify(CmiManifestVerifyFlags verifyFlags)
		{
			this.m_strongNameSignerInfo = null;
			this.m_authenticodeSignerInfo = null;
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.m_manifestDom.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement = this.m_manifestDom.SelectSingleNode("//ds:Signature", xmlNamespaceManager) as XmlElement;
			if (xmlElement == null)
			{
				throw new CryptographicException(-2146762496);
			}
			string text = "Id";
			if (!xmlElement.HasAttribute(text))
			{
				text = "id";
				if (!xmlElement.HasAttribute(text))
				{
					text = "ID";
					if (!xmlElement.HasAttribute(text))
					{
						throw new CryptographicException(-2146762749);
					}
				}
			}
			string attribute = xmlElement.GetAttribute(text);
			if (attribute == null || string.Compare(attribute, "StrongNameSignature", StringComparison.Ordinal) != 0)
			{
				throw new CryptographicException(-2146762749);
			}
			bool flag = false;
			XmlNodeList xmlNodeList = xmlElement.SelectNodes("ds:SignedInfo/ds:Reference", xmlNamespaceManager);
			foreach (object obj in xmlNodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement2 = xmlNode as XmlElement;
				if (xmlElement2 != null && xmlElement2.HasAttribute("URI"))
				{
					string attribute2 = xmlElement2.GetAttribute("URI");
					if (attribute2 != null && attribute2.Length == 0)
					{
						XmlNode xmlNode2 = xmlElement2.SelectSingleNode("ds:Transforms", xmlNamespaceManager);
						if (xmlNode2 == null)
						{
							throw new CryptographicException(-2146762749);
						}
						XmlNodeList xmlNodeList2 = xmlNode2.SelectNodes("ds:Transform", xmlNamespaceManager);
						if (xmlNodeList2.Count < 2)
						{
							throw new CryptographicException(-2146762749);
						}
						bool flag2 = false;
						bool flag3 = false;
						for (int i = 0; i < xmlNodeList2.Count; i++)
						{
							XmlElement xmlElement3 = xmlNodeList2[i] as XmlElement;
							string attribute3 = xmlElement3.GetAttribute("Algorithm");
							if (attribute3 == null)
							{
								break;
							}
							if (string.Compare(attribute3, "http://www.w3.org/2001/10/xml-exc-c14n#", StringComparison.Ordinal) != 0)
							{
								flag2 = true;
								if (flag3)
								{
									flag = true;
									break;
								}
							}
							else if (string.Compare(attribute3, "http://www.w3.org/2000/09/xmldsig#enveloped-signature", StringComparison.Ordinal) != 0)
							{
								flag3 = true;
								if (flag2)
								{
									flag = true;
									break;
								}
							}
						}
					}
				}
			}
			if (!flag)
			{
				throw new CryptographicException(-2146762749);
			}
			string text2 = this.VerifyPublicKeyToken();
			this.m_strongNameSignerInfo = new CmiStrongNameSignerInfo(-2146762485, text2);
			ManifestSignedXml manifestSignedXml = new ManifestSignedXml(this.m_manifestDom, true);
			manifestSignedXml.LoadXml(xmlElement);
			AsymmetricAlgorithm asymmetricAlgorithm = null;
			bool flag4 = manifestSignedXml.CheckSignatureReturningKey(out asymmetricAlgorithm);
			this.m_strongNameSignerInfo.PublicKey = asymmetricAlgorithm;
			if (!flag4)
			{
				this.m_strongNameSignerInfo.ErrorCode = -2146869232;
				throw new CryptographicException(-2146869232);
			}
			if ((verifyFlags & CmiManifestVerifyFlags.StrongNameOnly) != CmiManifestVerifyFlags.StrongNameOnly)
			{
				this.VerifyLicense(verifyFlags);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002BC4 File Offset: 0x00000DC4
		internal CmiStrongNameSignerInfo StrongNameSignerInfo
		{
			get
			{
				return this.m_strongNameSignerInfo;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002BCC File Offset: 0x00000DCC
		internal CmiAuthenticodeSignerInfo AuthenticodeSignerInfo
		{
			get
			{
				return this.m_authenticodeSignerInfo;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002BD4 File Offset: 0x00000DD4
		private unsafe void VerifyLicense(CmiManifestVerifyFlags verifyFlags)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.m_manifestDom.NameTable);
			xmlNamespaceManager.AddNamespace("asm", "urn:schemas-microsoft-com:asm.v1");
			xmlNamespaceManager.AddNamespace("asm2", "urn:schemas-microsoft-com:asm.v2");
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			xmlNamespaceManager.AddNamespace("msrel", "http://schemas.microsoft.com/windows/rel/2005/reldata");
			xmlNamespaceManager.AddNamespace("r", "urn:mpeg:mpeg21:2003:01-REL-R-NS");
			xmlNamespaceManager.AddNamespace("as", "http://schemas.microsoft.com/windows/pki/2005/Authenticode");
			XmlElement xmlElement = this.m_manifestDom.SelectSingleNode("asm:assembly/ds:Signature/ds:KeyInfo/msrel:RelData/r:license", xmlNamespaceManager) as XmlElement;
			if (xmlElement == null)
			{
				return;
			}
			this.VerifyAssemblyIdentity(xmlNamespaceManager);
			this.m_authenticodeSignerInfo = new CmiAuthenticodeSignerInfo(-2146762485);
			byte[] bytes = Encoding.UTF8.GetBytes(xmlElement.OuterXml);
			byte[] array;
			byte* ptr;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			Win32.AXL_SIGNER_INFO axl_SIGNER_INFO = default(Win32.AXL_SIGNER_INFO);
			axl_SIGNER_INFO.cbSize = (uint)Marshal.SizeOf(typeof(Win32.AXL_SIGNER_INFO));
			Win32.AXL_TIMESTAMPER_INFO axl_TIMESTAMPER_INFO = default(Win32.AXL_TIMESTAMPER_INFO);
			axl_TIMESTAMPER_INFO.cbSize = (uint)Marshal.SizeOf(typeof(Win32.AXL_TIMESTAMPER_INFO));
			Win32.CRYPT_DATA_BLOB crypt_DATA_BLOB = default(Win32.CRYPT_DATA_BLOB);
			IntPtr intPtr = new IntPtr((void*)ptr);
			crypt_DATA_BLOB.cbData = (uint)bytes.Length;
			crypt_DATA_BLOB.pbData = intPtr;
			int num = Win32.CertVerifyAuthenticodeLicense(ref crypt_DATA_BLOB, (uint)verifyFlags, ref axl_SIGNER_INFO, ref axl_TIMESTAMPER_INFO);
			if (2148204800U != axl_SIGNER_INFO.dwError)
			{
				this.m_authenticodeSignerInfo = new CmiAuthenticodeSignerInfo(axl_SIGNER_INFO, axl_TIMESTAMPER_INFO);
			}
			Win32.CertFreeAuthenticodeSignerInfo(ref axl_SIGNER_INFO);
			Win32.CertFreeAuthenticodeTimestamperInfo(ref axl_TIMESTAMPER_INFO);
			if (num != 0)
			{
				throw new CryptographicException(num);
			}
			array = null;
			this.VerifyPublisherIdentity(xmlNamespaceManager);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002D60 File Offset: 0x00000F60
		private XmlElement ExtractPrincipalFromManifest()
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.m_manifestDom.NameTable);
			xmlNamespaceManager.AddNamespace("asm", "urn:schemas-microsoft-com:asm.v1");
			XmlNode xmlNode = this.m_manifestDom.SelectSingleNode("asm:assembly/asm:assemblyIdentity", xmlNamespaceManager);
			if (xmlNode == null)
			{
				throw new CryptographicException(-2146762749);
			}
			return xmlNode as XmlElement;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002DB4 File Offset: 0x00000FB4
		private void VerifyAssemblyIdentity(XmlNamespaceManager nsm)
		{
			XmlElement xmlElement = this.m_manifestDom.SelectSingleNode("asm:assembly/asm:assemblyIdentity", nsm) as XmlElement;
			XmlElement xmlElement2 = this.m_manifestDom.SelectSingleNode("asm:assembly/ds:Signature/ds:KeyInfo/msrel:RelData/r:license/r:grant/as:ManifestInformation/as:assemblyIdentity", nsm) as XmlElement;
			if (xmlElement == null || xmlElement2 == null || !xmlElement.HasAttributes || !xmlElement2.HasAttributes)
			{
				throw new CryptographicException(-2146762749);
			}
			XmlAttributeCollection attributes = xmlElement.Attributes;
			if (attributes.Count == 0 || attributes.Count != xmlElement2.Attributes.Count)
			{
				throw new CryptographicException(-2146762749);
			}
			foreach (object obj in attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				if (!xmlElement2.HasAttribute(xmlAttribute.LocalName) || xmlAttribute.Value != xmlElement2.GetAttribute(xmlAttribute.LocalName))
				{
					throw new CryptographicException(-2146762749);
				}
			}
			this.VerifyHash(nsm);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002EC0 File Offset: 0x000010C0
		private void VerifyPublisherIdentity(XmlNamespaceManager nsm)
		{
			if (this.m_authenticodeSignerInfo.ErrorCode == -2146762496)
			{
				return;
			}
			X509Certificate2 certificate = this.m_authenticodeSignerInfo.SignerChain.ChainElements[0].Certificate;
			XmlElement xmlElement = this.m_manifestDom.SelectSingleNode("asm:assembly/asm2:publisherIdentity", nsm) as XmlElement;
			if (xmlElement == null || !xmlElement.HasAttributes)
			{
				throw new CryptographicException(-2146762749);
			}
			if (!xmlElement.HasAttribute("name") || !xmlElement.HasAttribute("issuerKeyHash"))
			{
				throw new CryptographicException(-2146762749);
			}
			string attribute = xmlElement.GetAttribute("name");
			string attribute2 = xmlElement.GetAttribute("issuerKeyHash");
			IntPtr intPtr = 0;
			int num = Win32._AxlGetIssuerPublicKeyHash(certificate.Handle, ref intPtr);
			if (num != 0)
			{
				throw new CryptographicException(num);
			}
			string text = Marshal.PtrToStringUni(intPtr);
			Win32.HeapFree(Win32.GetProcessHeap(), 0U, intPtr);
			if (string.Compare(attribute, certificate.SubjectName.Name, StringComparison.Ordinal) != 0 || string.Compare(attribute2, text, StringComparison.Ordinal) != 0)
			{
				throw new CryptographicException(-2146762485);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002FCC File Offset: 0x000011CC
		private void VerifyHash(XmlNamespaceManager nsm)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			xmlDocument = (XmlDocument)this.m_manifestDom.Clone();
			XmlElement xmlElement = xmlDocument.SelectSingleNode("asm:assembly/ds:Signature/ds:KeyInfo/msrel:RelData/r:license/r:grant/as:ManifestInformation", nsm) as XmlElement;
			if (xmlElement == null)
			{
				throw new CryptographicException(-2146762749);
			}
			if (!xmlElement.HasAttribute("Hash"))
			{
				throw new CryptographicException(-2146762749);
			}
			string attribute = xmlElement.GetAttribute("Hash");
			if (attribute == null || attribute.Length == 0)
			{
				throw new CryptographicException(-2146762749);
			}
			XmlElement xmlElement2 = xmlDocument.SelectSingleNode("asm:assembly/ds:Signature", nsm) as XmlElement;
			if (xmlElement2 == null)
			{
				throw new CryptographicException(-2146762749);
			}
			xmlElement2.ParentNode.RemoveChild(xmlElement2);
			byte[] array = SignedCmiManifest.HexStringToBytes(xmlElement.GetAttribute("Hash"));
			byte[] array2 = SignedCmiManifest.ComputeHashFromManifest(xmlDocument);
			if (array.Length == 0 || array.Length != array2.Length)
			{
				throw new CryptographicException(-2146869232);
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != array2[i])
				{
					throw new CryptographicException(-2146869232);
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000030DC File Offset: 0x000012DC
		private unsafe string VerifyPublicKeyToken()
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.m_manifestDom.NameTable);
			xmlNamespaceManager.AddNamespace("asm", "urn:schemas-microsoft-com:asm.v1");
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement = this.m_manifestDom.SelectSingleNode("asm:assembly/ds:Signature/ds:KeyInfo/ds:KeyValue/ds:RSAKeyValue/ds:Modulus", xmlNamespaceManager) as XmlElement;
			XmlElement xmlElement2 = this.m_manifestDom.SelectSingleNode("asm:assembly/ds:Signature/ds:KeyInfo/ds:KeyValue/ds:RSAKeyValue/ds:Exponent", xmlNamespaceManager) as XmlElement;
			if (xmlElement == null || xmlElement2 == null)
			{
				throw new CryptographicException(-2146762749);
			}
			byte[] bytes = Encoding.UTF8.GetBytes(xmlElement.InnerXml);
			byte[] bytes2 = Encoding.UTF8.GetBytes(xmlElement2.InnerXml);
			string publicKeyToken = SignedCmiManifest.GetPublicKeyToken(this.m_manifestDom);
			byte[] array = SignedCmiManifest.HexStringToBytes(publicKeyToken);
			byte[] array2;
			byte* ptr;
			if ((array2 = bytes) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			byte[] array3;
			byte* ptr2;
			if ((array3 = bytes2) == null || array3.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array3[0];
			}
			Win32.CRYPT_DATA_BLOB crypt_DATA_BLOB = default(Win32.CRYPT_DATA_BLOB);
			Win32.CRYPT_DATA_BLOB crypt_DATA_BLOB2 = default(Win32.CRYPT_DATA_BLOB);
			IntPtr intPtr = 0;
			crypt_DATA_BLOB.cbData = (uint)bytes.Length;
			crypt_DATA_BLOB.pbData = new IntPtr((void*)ptr);
			crypt_DATA_BLOB2.cbData = (uint)bytes2.Length;
			crypt_DATA_BLOB2.pbData = new IntPtr((void*)ptr2);
			int num = Win32._AxlRSAKeyValueToPublicKeyToken(ref crypt_DATA_BLOB, ref crypt_DATA_BLOB2, ref intPtr);
			if (num != 0)
			{
				throw new CryptographicException(num);
			}
			byte[] array4 = SignedCmiManifest.HexStringToBytes(Marshal.PtrToStringUni(intPtr));
			Win32.HeapFree(Win32.GetProcessHeap(), 0U, intPtr);
			array3 = null;
			array2 = null;
			if (array.Length == 0 || array.Length != array4.Length)
			{
				throw new CryptographicException(-2146762485);
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != array4[i])
				{
					throw new CryptographicException(-2146762485);
				}
			}
			return publicKeyToken;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003298 File Offset: 0x00001498
		private static void InsertPublisherIdentity(XmlDocument manifestDom, X509Certificate2 signerCert)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(manifestDom.NameTable);
			xmlNamespaceManager.AddNamespace("asm", "urn:schemas-microsoft-com:asm.v1");
			xmlNamespaceManager.AddNamespace("asm2", "urn:schemas-microsoft-com:asm.v2");
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement = manifestDom.SelectSingleNode("asm:assembly", xmlNamespaceManager) as XmlElement;
			if (!(manifestDom.SelectSingleNode("asm:assembly/asm:assemblyIdentity", xmlNamespaceManager) is XmlElement))
			{
				throw new CryptographicException(-2146762749);
			}
			XmlElement xmlElement2 = manifestDom.SelectSingleNode("asm:assembly/asm2:publisherIdentity", xmlNamespaceManager) as XmlElement;
			if (xmlElement2 == null)
			{
				xmlElement2 = manifestDom.CreateElement("publisherIdentity", "urn:schemas-microsoft-com:asm.v2");
			}
			IntPtr intPtr = 0;
			int num = Win32._AxlGetIssuerPublicKeyHash(signerCert.Handle, ref intPtr);
			if (num != 0)
			{
				throw new CryptographicException(num);
			}
			string text = Marshal.PtrToStringUni(intPtr);
			Win32.HeapFree(Win32.GetProcessHeap(), 0U, intPtr);
			xmlElement2.SetAttribute("name", signerCert.SubjectName.Name);
			xmlElement2.SetAttribute("issuerKeyHash", text);
			XmlElement xmlElement3 = manifestDom.SelectSingleNode("asm:assembly/ds:Signature", xmlNamespaceManager) as XmlElement;
			if (xmlElement3 != null)
			{
				xmlElement.InsertBefore(xmlElement2, xmlElement3);
				return;
			}
			xmlElement.AppendChild(xmlElement2);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000033C0 File Offset: 0x000015C0
		private static void RemoveExistingSignature(XmlDocument manifestDom)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(manifestDom.NameTable);
			xmlNamespaceManager.AddNamespace("asm", "urn:schemas-microsoft-com:asm.v1");
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlNode xmlNode = manifestDom.SelectSingleNode("asm:assembly/ds:Signature", xmlNamespaceManager);
			if (xmlNode != null)
			{
				xmlNode.ParentNode.RemoveChild(xmlNode);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003418 File Offset: 0x00001618
		private unsafe static void ReplacePublicKeyToken(XmlDocument manifestDom, AsymmetricAlgorithm snKey)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(manifestDom.NameTable);
			xmlNamespaceManager.AddNamespace("asm", "urn:schemas-microsoft-com:asm.v1");
			XmlElement xmlElement = manifestDom.SelectSingleNode("asm:assembly/asm:assemblyIdentity", xmlNamespaceManager) as XmlElement;
			if (xmlElement == null)
			{
				throw new CryptographicException(-2146762749);
			}
			if (!xmlElement.HasAttribute("publicKeyToken"))
			{
				throw new CryptographicException(-2146762749);
			}
			byte[] array = ((RSACryptoServiceProvider)snKey).ExportCspBlob(false);
			if (array == null || array.Length == 0)
			{
				throw new CryptographicException(-2146893821);
			}
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			Win32.CRYPT_DATA_BLOB crypt_DATA_BLOB = default(Win32.CRYPT_DATA_BLOB);
			crypt_DATA_BLOB.cbData = (uint)array.Length;
			crypt_DATA_BLOB.pbData = new IntPtr((void*)ptr);
			IntPtr intPtr = 0;
			int num = Win32._AxlPublicKeyBlobToPublicKeyToken(ref crypt_DATA_BLOB, ref intPtr);
			if (num != 0)
			{
				throw new CryptographicException(num);
			}
			string text = Marshal.PtrToStringUni(intPtr);
			Win32.HeapFree(Win32.GetProcessHeap(), 0U, intPtr);
			xmlElement.SetAttribute("publicKeyToken", text);
			array2 = null;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003518 File Offset: 0x00001718
		private static string GetPublicKeyToken(XmlDocument manifestDom)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(manifestDom.NameTable);
			xmlNamespaceManager.AddNamespace("asm", "urn:schemas-microsoft-com:asm.v1");
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement = manifestDom.SelectSingleNode("asm:assembly/asm:assemblyIdentity", xmlNamespaceManager) as XmlElement;
			if (xmlElement == null || !xmlElement.HasAttribute("publicKeyToken"))
			{
				throw new CryptographicException(-2146762749);
			}
			return xmlElement.GetAttribute("publicKeyToken");
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000358C File Offset: 0x0000178C
		private static byte[] ComputeHashFromManifest(XmlDocument manifestDom)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			using (TextReader textReader = new StringReader(manifestDom.OuterXml))
			{
				XmlReader xmlReader = XmlReader.Create(textReader, new XmlReaderSettings
				{
					DtdProcessing = DtdProcessing.Parse
				}, manifestDom.BaseURI);
				xmlDocument.Load(xmlReader);
			}
			XmlDsigExcC14NTransform xmlDsigExcC14NTransform = new XmlDsigExcC14NTransform();
			xmlDsigExcC14NTransform.LoadInput(xmlDocument);
			byte[] array2;
			using (SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider())
			{
				byte[] array = sha1CryptoServiceProvider.ComputeHash(xmlDsigExcC14NTransform.GetOutput() as MemoryStream);
				if (array == null)
				{
					throw new CryptographicException(-2146869232);
				}
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000364C File Offset: 0x0000184C
		private static XmlDocument CreateLicenseDom(CmiManifestSigner signer, XmlElement principal, byte[] hash)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			xmlDocument.LoadXml("<r:license xmlns:r=\"urn:mpeg:mpeg21:2003:01-REL-R-NS\" xmlns:as=\"http://schemas.microsoft.com/windows/pki/2005/Authenticode\"><r:grant><as:ManifestInformation><as:assemblyIdentity /></as:ManifestInformation><as:SignedBy/><as:AuthenticodePublisher><as:X509SubjectName>CN=dummy</as:X509SubjectName></as:AuthenticodePublisher></r:grant><r:issuer></r:issuer></r:license>");
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("r", "urn:mpeg:mpeg21:2003:01-REL-R-NS");
			xmlNamespaceManager.AddNamespace("as", "http://schemas.microsoft.com/windows/pki/2005/Authenticode");
			XmlElement xmlElement = xmlDocument.SelectSingleNode("r:license/r:grant/as:ManifestInformation/as:assemblyIdentity", xmlNamespaceManager) as XmlElement;
			xmlElement.RemoveAllAttributes();
			foreach (object obj in principal.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				xmlElement.SetAttribute(xmlAttribute.Name, xmlAttribute.Value);
			}
			XmlElement xmlElement2 = xmlDocument.SelectSingleNode("r:license/r:grant/as:ManifestInformation", xmlNamespaceManager) as XmlElement;
			xmlElement2.SetAttribute("Hash", (hash.Length == 0) ? "" : SignedCmiManifest.BytesToHexString(hash, 0, hash.Length));
			xmlElement2.SetAttribute("Description", (signer.Description == null) ? "" : signer.Description);
			xmlElement2.SetAttribute("Url", (signer.DescriptionUrl == null) ? "" : signer.DescriptionUrl);
			XmlElement xmlElement3 = xmlDocument.SelectSingleNode("r:license/r:grant/as:AuthenticodePublisher/as:X509SubjectName", xmlNamespaceManager) as XmlElement;
			xmlElement3.InnerText = signer.Certificate.SubjectName.Name;
			return xmlDocument;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000037B4 File Offset: 0x000019B4
		private static void AuthenticodeSignLicenseDom(XmlDocument licenseDom, CmiManifestSigner signer, string timeStampUrl)
		{
			RSA rsapublicKey = CngLightup.GetRSAPublicKey(signer.Certificate);
			if (rsapublicKey == null)
			{
				throw new NotSupportedException();
			}
			ManifestSignedXml manifestSignedXml = new ManifestSignedXml(licenseDom);
			manifestSignedXml.SigningKey = CngLightup.GetRSAPrivateKey(signer.Certificate);
			manifestSignedXml.SignedInfo.CanonicalizationMethod = "http://www.w3.org/2001/10/xml-exc-c14n#";
			manifestSignedXml.KeyInfo.AddClause(new RSAKeyValue(rsapublicKey));
			manifestSignedXml.KeyInfo.AddClause(new KeyInfoX509Data(signer.Certificate, signer.IncludeOption));
			Reference reference = new Reference();
			reference.Uri = "";
			reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
			reference.AddTransform(new XmlDsigExcC14NTransform());
			manifestSignedXml.AddReference(reference);
			manifestSignedXml.ComputeSignature();
			XmlElement xml = manifestSignedXml.GetXml();
			xml.SetAttribute("Id", "AuthenticodeSignature");
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(licenseDom.NameTable);
			xmlNamespaceManager.AddNamespace("r", "urn:mpeg:mpeg21:2003:01-REL-R-NS");
			XmlElement xmlElement = licenseDom.SelectSingleNode("r:license/r:issuer", xmlNamespaceManager) as XmlElement;
			xmlElement.AppendChild(licenseDom.ImportNode(xml, true));
			if (timeStampUrl != null && timeStampUrl.Length != 0)
			{
				SignedCmiManifest.TimestampSignedLicenseDom(licenseDom, timeStampUrl);
			}
			licenseDom.DocumentElement.ParentNode.InnerXml = "<msrel:RelData xmlns:msrel=\"http://schemas.microsoft.com/windows/rel/2005/reldata\">" + licenseDom.OuterXml + "</msrel:RelData>";
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000038F0 File Offset: 0x00001AF0
		private unsafe static void TimestampSignedLicenseDom(XmlDocument licenseDom, string timeStampUrl)
		{
			Win32.CRYPT_DATA_BLOB crypt_DATA_BLOB = default(Win32.CRYPT_DATA_BLOB);
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(licenseDom.NameTable);
			xmlNamespaceManager.AddNamespace("r", "urn:mpeg:mpeg21:2003:01-REL-R-NS");
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			xmlNamespaceManager.AddNamespace("as", "http://schemas.microsoft.com/windows/pki/2005/Authenticode");
			byte[] bytes = Encoding.UTF8.GetBytes(licenseDom.OuterXml);
			byte[] array;
			byte* ptr;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			Win32.CRYPT_DATA_BLOB crypt_DATA_BLOB2 = default(Win32.CRYPT_DATA_BLOB);
			IntPtr intPtr = new IntPtr((void*)ptr);
			crypt_DATA_BLOB2.cbData = (uint)bytes.Length;
			crypt_DATA_BLOB2.pbData = intPtr;
			int num = Win32.CertTimestampAuthenticodeLicense(ref crypt_DATA_BLOB2, timeStampUrl, ref crypt_DATA_BLOB);
			if (num != 0)
			{
				throw new CryptographicException(num);
			}
			array = null;
			byte[] array2 = new byte[crypt_DATA_BLOB.cbData];
			Marshal.Copy(crypt_DATA_BLOB.pbData, array2, 0, array2.Length);
			Win32.HeapFree(Win32.GetProcessHeap(), 0U, crypt_DATA_BLOB.pbData);
			XmlElement xmlElement = licenseDom.CreateElement("as", "Timestamp", "http://schemas.microsoft.com/windows/pki/2005/Authenticode");
			xmlElement.InnerText = Encoding.UTF8.GetString(array2);
			XmlElement xmlElement2 = licenseDom.CreateElement("Object", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement2.AppendChild(xmlElement);
			XmlElement xmlElement3 = licenseDom.SelectSingleNode("r:license/r:issuer/ds:Signature", xmlNamespaceManager) as XmlElement;
			xmlElement3.AppendChild(xmlElement2);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003A40 File Offset: 0x00001C40
		private static void StrongNameSignManifestDom(XmlDocument manifestDom, XmlDocument licenseDom, CmiManifestSigner signer)
		{
			RSA rsa = signer.StrongNameKey as RSA;
			if (rsa == null)
			{
				throw new NotSupportedException();
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(manifestDom.NameTable);
			xmlNamespaceManager.AddNamespace("asm", "urn:schemas-microsoft-com:asm.v1");
			XmlElement xmlElement = manifestDom.SelectSingleNode("asm:assembly", xmlNamespaceManager) as XmlElement;
			if (xmlElement == null)
			{
				throw new CryptographicException(-2146762749);
			}
			ManifestSignedXml manifestSignedXml = new ManifestSignedXml(xmlElement);
			manifestSignedXml.SigningKey = signer.StrongNameKey;
			manifestSignedXml.SignedInfo.CanonicalizationMethod = "http://www.w3.org/2001/10/xml-exc-c14n#";
			manifestSignedXml.KeyInfo.AddClause(new RSAKeyValue(rsa));
			if (licenseDom != null)
			{
				manifestSignedXml.KeyInfo.AddClause(new KeyInfoNode(licenseDom.DocumentElement));
			}
			manifestSignedXml.KeyInfo.Id = "StrongNameKeyInfo";
			Reference reference = new Reference();
			reference.Uri = "";
			reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
			reference.AddTransform(new XmlDsigExcC14NTransform());
			manifestSignedXml.AddReference(reference);
			manifestSignedXml.ComputeSignature();
			XmlElement xml = manifestSignedXml.GetXml();
			xml.SetAttribute("Id", "StrongNameSignature");
			xmlElement.AppendChild(xml);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003B58 File Offset: 0x00001D58
		private static string BytesToHexString(byte[] array, int start, int end)
		{
			string text = null;
			if (array != null)
			{
				char[] array2 = new char[(end - start) * 2];
				int num = end;
				int num2 = 0;
				while (num-- > start)
				{
					int num3 = (array[num] & 240) >> 4;
					array2[num2++] = SignedCmiManifest.hexValues[num3];
					num3 = (int)(array[num] & 15);
					array2[num2++] = SignedCmiManifest.hexValues[num3];
				}
				text = new string(array2);
			}
			return text;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003BC0 File Offset: 0x00001DC0
		private static byte[] HexStringToBytes(string hexString)
		{
			uint num = (uint)(hexString.Length / 2);
			byte[] array = new byte[num];
			int num2 = hexString.Length - 2;
			int num3 = 0;
			while ((long)num3 < (long)((ulong)num))
			{
				array[num3] = (byte)(((int)SignedCmiManifest.HexToByte(hexString[num2]) << 4) | (int)SignedCmiManifest.HexToByte(hexString[num2 + 1]));
				num2 -= 2;
				num3++;
			}
			return array;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003C1A File Offset: 0x00001E1A
		private static byte HexToByte(char val)
		{
			if (val <= '9' && val >= '0')
			{
				return (byte)(val - '0');
			}
			if (val >= 'a' && val <= 'f')
			{
				return (byte)(val - 'a' + '\n');
			}
			if (val >= 'A' && val <= 'F')
			{
				return (byte)(val - 'A' + '\n');
			}
			return byte.MaxValue;
		}

		// Token: 0x040000B2 RID: 178
		private XmlDocument m_manifestDom;

		// Token: 0x040000B3 RID: 179
		private CmiStrongNameSignerInfo m_strongNameSignerInfo;

		// Token: 0x040000B4 RID: 180
		private CmiAuthenticodeSignerInfo m_authenticodeSignerInfo;

		// Token: 0x040000B5 RID: 181
		private const string AssemblyNamespaceUri = "urn:schemas-microsoft-com:asm.v1";

		// Token: 0x040000B6 RID: 182
		private const string AssemblyV2NamespaceUri = "urn:schemas-microsoft-com:asm.v2";

		// Token: 0x040000B7 RID: 183
		private const string MSRelNamespaceUri = "http://schemas.microsoft.com/windows/rel/2005/reldata";

		// Token: 0x040000B8 RID: 184
		private const string LicenseNamespaceUri = "urn:mpeg:mpeg21:2003:01-REL-R-NS";

		// Token: 0x040000B9 RID: 185
		private const string AuthenticodeNamespaceUri = "http://schemas.microsoft.com/windows/pki/2005/Authenticode";

		// Token: 0x040000BA RID: 186
		private const string licenseTemplate = "<r:license xmlns:r=\"urn:mpeg:mpeg21:2003:01-REL-R-NS\" xmlns:as=\"http://schemas.microsoft.com/windows/pki/2005/Authenticode\"><r:grant><as:ManifestInformation><as:assemblyIdentity /></as:ManifestInformation><as:SignedBy/><as:AuthenticodePublisher><as:X509SubjectName>CN=dummy</as:X509SubjectName></as:AuthenticodePublisher></r:grant><r:issuer></r:issuer></r:license>";

		// Token: 0x040000BB RID: 187
		private static readonly char[] hexValues = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'a', 'b', 'c', 'd', 'e', 'f'
		};
	}
}
