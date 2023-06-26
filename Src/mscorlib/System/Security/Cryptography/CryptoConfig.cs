using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Util;
using System.Threading;
using Microsoft.Win32;

namespace System.Security.Cryptography
{
	/// <summary>Accesses the cryptography configuration information.</summary>
	// Token: 0x02000255 RID: 597
	[ComVisible(true)]
	public class CryptoConfig
	{
		/// <summary>Indicates whether the runtime should enforce the policy to create only Federal Information Processing Standard (FIPS) certified algorithms.</summary>
		/// <returns>
		///   <see langword="true" /> to enforce the policy; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x00073A24 File Offset: 0x00071C24
		public static bool AllowOnlyFipsAlgorithms
		{
			[SecuritySafeCritical]
			get
			{
				if (!CryptoConfig.s_haveFipsAlgorithmPolicy)
				{
					if (Utils._GetEnforceFipsPolicySetting())
					{
						if (Environment.OSVersion.Version.Major >= 6)
						{
							bool flag;
							uint num = Win32Native.BCryptGetFipsAlgorithmMode(out flag);
							bool flag2 = num == 0U || num == 3221225524U;
							CryptoConfig.s_fipsAlgorithmPolicy = !flag2 || flag;
							CryptoConfig.s_haveFipsAlgorithmPolicy = true;
						}
						else
						{
							CryptoConfig.s_fipsAlgorithmPolicy = Utils.ReadLegacyFipsPolicy();
							CryptoConfig.s_haveFipsAlgorithmPolicy = true;
						}
					}
					else
					{
						CryptoConfig.s_fipsAlgorithmPolicy = false;
						CryptoConfig.s_haveFipsAlgorithmPolicy = true;
					}
				}
				return CryptoConfig.s_fipsAlgorithmPolicy;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x00073AAE File Offset: 0x00071CAE
		private static string Version
		{
			[SecurityCritical]
			get
			{
				if (CryptoConfig.version == null)
				{
					CryptoConfig.version = ((RuntimeType)typeof(CryptoConfig)).GetRuntimeAssembly().GetVersion().ToString();
				}
				return CryptoConfig.version;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x00073AE8 File Offset: 0x00071CE8
		private static object InternalSyncObject
		{
			get
			{
				if (CryptoConfig.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref CryptoConfig.s_InternalSyncObject, obj, null);
				}
				return CryptoConfig.s_InternalSyncObject;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x00073B14 File Offset: 0x00071D14
		private static Dictionary<string, string> DefaultOidHT
		{
			get
			{
				if (CryptoConfig.defaultOidHT == null)
				{
					CryptoConfig.defaultOidHT = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
					{
						{ "SHA", "1.3.14.3.2.26" },
						{ "SHA1", "1.3.14.3.2.26" },
						{ "System.Security.Cryptography.SHA1", "1.3.14.3.2.26" },
						{ "System.Security.Cryptography.SHA1CryptoServiceProvider", "1.3.14.3.2.26" },
						{ "System.Security.Cryptography.SHA1Cng", "1.3.14.3.2.26" },
						{ "System.Security.Cryptography.SHA1Managed", "1.3.14.3.2.26" },
						{ "SHA256", "2.16.840.1.101.3.4.2.1" },
						{ "System.Security.Cryptography.SHA256", "2.16.840.1.101.3.4.2.1" },
						{ "System.Security.Cryptography.SHA256CryptoServiceProvider", "2.16.840.1.101.3.4.2.1" },
						{ "System.Security.Cryptography.SHA256Cng", "2.16.840.1.101.3.4.2.1" },
						{ "System.Security.Cryptography.SHA256Managed", "2.16.840.1.101.3.4.2.1" },
						{ "SHA384", "2.16.840.1.101.3.4.2.2" },
						{ "System.Security.Cryptography.SHA384", "2.16.840.1.101.3.4.2.2" },
						{ "System.Security.Cryptography.SHA384CryptoServiceProvider", "2.16.840.1.101.3.4.2.2" },
						{ "System.Security.Cryptography.SHA384Cng", "2.16.840.1.101.3.4.2.2" },
						{ "System.Security.Cryptography.SHA384Managed", "2.16.840.1.101.3.4.2.2" },
						{ "SHA512", "2.16.840.1.101.3.4.2.3" },
						{ "System.Security.Cryptography.SHA512", "2.16.840.1.101.3.4.2.3" },
						{ "System.Security.Cryptography.SHA512CryptoServiceProvider", "2.16.840.1.101.3.4.2.3" },
						{ "System.Security.Cryptography.SHA512Cng", "2.16.840.1.101.3.4.2.3" },
						{ "System.Security.Cryptography.SHA512Managed", "2.16.840.1.101.3.4.2.3" },
						{ "RIPEMD160", "1.3.36.3.2.1" },
						{ "System.Security.Cryptography.RIPEMD160", "1.3.36.3.2.1" },
						{ "System.Security.Cryptography.RIPEMD160Managed", "1.3.36.3.2.1" },
						{ "MD5", "1.2.840.113549.2.5" },
						{ "System.Security.Cryptography.MD5", "1.2.840.113549.2.5" },
						{ "System.Security.Cryptography.MD5CryptoServiceProvider", "1.2.840.113549.2.5" },
						{ "System.Security.Cryptography.MD5Managed", "1.2.840.113549.2.5" },
						{ "TripleDESKeyWrap", "1.2.840.113549.1.9.16.3.6" },
						{ "RC2", "1.2.840.113549.3.2" },
						{ "System.Security.Cryptography.RC2CryptoServiceProvider", "1.2.840.113549.3.2" },
						{ "DES", "1.3.14.3.2.7" },
						{ "System.Security.Cryptography.DESCryptoServiceProvider", "1.3.14.3.2.7" },
						{ "TripleDES", "1.2.840.113549.3.7" },
						{ "System.Security.Cryptography.TripleDESCryptoServiceProvider", "1.2.840.113549.3.7" }
					};
				}
				return CryptoConfig.defaultOidHT;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600212C RID: 8492 RVA: 0x00073D78 File Offset: 0x00071F78
		private static Dictionary<string, object> DefaultNameHT
		{
			get
			{
				if (CryptoConfig.defaultNameHT == null)
				{
					Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
					Type typeFromHandle = typeof(SHA1CryptoServiceProvider);
					Type typeFromHandle2 = typeof(MD5CryptoServiceProvider);
					Type typeFromHandle3 = typeof(RIPEMD160Managed);
					Type typeFromHandle4 = typeof(HMACMD5);
					Type typeFromHandle5 = typeof(HMACRIPEMD160);
					Type typeFromHandle6 = typeof(HMACSHA1);
					Type typeFromHandle7 = typeof(HMACSHA256);
					Type typeFromHandle8 = typeof(HMACSHA384);
					Type typeFromHandle9 = typeof(HMACSHA512);
					Type typeFromHandle10 = typeof(MACTripleDES);
					Type typeFromHandle11 = typeof(RSACryptoServiceProvider);
					Type typeFromHandle12 = typeof(DSACryptoServiceProvider);
					Type typeFromHandle13 = typeof(DESCryptoServiceProvider);
					Type typeFromHandle14 = typeof(TripleDESCryptoServiceProvider);
					Type typeFromHandle15 = typeof(RC2CryptoServiceProvider);
					Type typeFromHandle16 = typeof(RijndaelManaged);
					Type typeFromHandle17 = typeof(DSASignatureDescription);
					Type typeFromHandle18 = typeof(RSAPKCS1SHA1SignatureDescription);
					Type typeFromHandle19 = typeof(RSAPKCS1SHA256SignatureDescription);
					Type typeFromHandle20 = typeof(RSAPKCS1SHA384SignatureDescription);
					Type typeFromHandle21 = typeof(RSAPKCS1SHA512SignatureDescription);
					Type typeFromHandle22 = typeof(RNGCryptoServiceProvider);
					string text = "System.Security.Cryptography.AesCryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
					string text2 = "System.Security.Cryptography.RSACng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
					string text3 = "System.Security.Cryptography.DSACng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
					string text4 = "System.Security.Cryptography.AesManaged, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
					string text5 = "System.Security.Cryptography.ECDiffieHellmanCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
					string text6 = "System.Security.Cryptography.ECDsaCng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
					string text7 = "System.Security.Cryptography.MD5Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
					string text8 = "System.Security.Cryptography.SHA1Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
					string text9 = "System.Security.Cryptography.SHA256Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
					string text10 = "System.Security.Cryptography.SHA256CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
					string text11 = "System.Security.Cryptography.SHA384Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
					string text12 = "System.Security.Cryptography.SHA384CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
					string text13 = "System.Security.Cryptography.SHA512Cng, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
					string text14 = "System.Security.Cryptography.SHA512CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
					bool allowOnlyFipsAlgorithms = CryptoConfig.AllowOnlyFipsAlgorithms;
					object obj = typeof(SHA256Managed);
					if (allowOnlyFipsAlgorithms)
					{
						obj = text9;
					}
					object obj2 = (allowOnlyFipsAlgorithms ? text11 : typeof(SHA384Managed));
					object obj3 = (allowOnlyFipsAlgorithms ? text13 : typeof(SHA512Managed));
					string text15 = "System.Security.Cryptography.DpapiDataProtector, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
					dictionary.Add("RandomNumberGenerator", typeFromHandle22);
					dictionary.Add("System.Security.Cryptography.RandomNumberGenerator", typeFromHandle22);
					dictionary.Add("SHA", typeFromHandle);
					dictionary.Add("SHA1", typeFromHandle);
					dictionary.Add("System.Security.Cryptography.SHA1", typeFromHandle);
					dictionary.Add("System.Security.Cryptography.SHA1Cng", text8);
					dictionary.Add("System.Security.Cryptography.HashAlgorithm", typeFromHandle);
					dictionary.Add("MD5", typeFromHandle2);
					dictionary.Add("System.Security.Cryptography.MD5", typeFromHandle2);
					dictionary.Add("System.Security.Cryptography.MD5Cng", text7);
					dictionary.Add("SHA256", obj);
					dictionary.Add("SHA-256", obj);
					dictionary.Add("System.Security.Cryptography.SHA256", obj);
					dictionary.Add("System.Security.Cryptography.SHA256Cng", text9);
					dictionary.Add("System.Security.Cryptography.SHA256CryptoServiceProvider", text10);
					dictionary.Add("SHA384", obj2);
					dictionary.Add("SHA-384", obj2);
					dictionary.Add("System.Security.Cryptography.SHA384", obj2);
					dictionary.Add("System.Security.Cryptography.SHA384Cng", text11);
					dictionary.Add("System.Security.Cryptography.SHA384CryptoServiceProvider", text12);
					dictionary.Add("SHA512", obj3);
					dictionary.Add("SHA-512", obj3);
					dictionary.Add("System.Security.Cryptography.SHA512", obj3);
					dictionary.Add("System.Security.Cryptography.SHA512Cng", text13);
					dictionary.Add("System.Security.Cryptography.SHA512CryptoServiceProvider", text14);
					dictionary.Add("RIPEMD160", typeFromHandle3);
					dictionary.Add("RIPEMD-160", typeFromHandle3);
					dictionary.Add("System.Security.Cryptography.RIPEMD160", typeFromHandle3);
					dictionary.Add("System.Security.Cryptography.RIPEMD160Managed", typeFromHandle3);
					dictionary.Add("System.Security.Cryptography.HMAC", typeFromHandle6);
					dictionary.Add("System.Security.Cryptography.KeyedHashAlgorithm", typeFromHandle6);
					dictionary.Add("HMACMD5", typeFromHandle4);
					dictionary.Add("System.Security.Cryptography.HMACMD5", typeFromHandle4);
					dictionary.Add("HMACRIPEMD160", typeFromHandle5);
					dictionary.Add("System.Security.Cryptography.HMACRIPEMD160", typeFromHandle5);
					dictionary.Add("HMACSHA1", typeFromHandle6);
					dictionary.Add("System.Security.Cryptography.HMACSHA1", typeFromHandle6);
					dictionary.Add("HMACSHA256", typeFromHandle7);
					dictionary.Add("System.Security.Cryptography.HMACSHA256", typeFromHandle7);
					dictionary.Add("HMACSHA384", typeFromHandle8);
					dictionary.Add("System.Security.Cryptography.HMACSHA384", typeFromHandle8);
					dictionary.Add("HMACSHA512", typeFromHandle9);
					dictionary.Add("System.Security.Cryptography.HMACSHA512", typeFromHandle9);
					dictionary.Add("MACTripleDES", typeFromHandle10);
					dictionary.Add("System.Security.Cryptography.MACTripleDES", typeFromHandle10);
					dictionary.Add("RSA", typeFromHandle11);
					dictionary.Add("System.Security.Cryptography.RSA", typeFromHandle11);
					dictionary.Add("System.Security.Cryptography.AsymmetricAlgorithm", typeFromHandle11);
					dictionary.Add("RSAPSS", text2);
					dictionary.Add("DSA-FIPS186-3", text3);
					dictionary.Add("DSA", typeFromHandle12);
					dictionary.Add("System.Security.Cryptography.DSA", typeFromHandle12);
					dictionary.Add("ECDsa", text6);
					dictionary.Add("ECDsaCng", text6);
					dictionary.Add("System.Security.Cryptography.ECDsaCng", text6);
					dictionary.Add("ECDH", text5);
					dictionary.Add("ECDiffieHellman", text5);
					dictionary.Add("ECDiffieHellmanCng", text5);
					dictionary.Add("System.Security.Cryptography.ECDiffieHellmanCng", text5);
					dictionary.Add("DES", typeFromHandle13);
					dictionary.Add("System.Security.Cryptography.DES", typeFromHandle13);
					dictionary.Add("3DES", typeFromHandle14);
					dictionary.Add("TripleDES", typeFromHandle14);
					dictionary.Add("Triple DES", typeFromHandle14);
					dictionary.Add("System.Security.Cryptography.TripleDES", typeFromHandle14);
					dictionary.Add("RC2", typeFromHandle15);
					dictionary.Add("System.Security.Cryptography.RC2", typeFromHandle15);
					dictionary.Add("Rijndael", typeFromHandle16);
					dictionary.Add("System.Security.Cryptography.Rijndael", typeFromHandle16);
					dictionary.Add("System.Security.Cryptography.SymmetricAlgorithm", typeFromHandle16);
					dictionary.Add("AES", text);
					dictionary.Add("AesCryptoServiceProvider", text);
					dictionary.Add("System.Security.Cryptography.AesCryptoServiceProvider", text);
					dictionary.Add("AesManaged", text4);
					dictionary.Add("System.Security.Cryptography.AesManaged", text4);
					dictionary.Add("DpapiDataProtector", text15);
					dictionary.Add("System.Security.Cryptography.DpapiDataProtector", text15);
					dictionary.Add("http://www.w3.org/2000/09/xmldsig#dsa-sha1", typeFromHandle17);
					dictionary.Add("System.Security.Cryptography.DSASignatureDescription", typeFromHandle17);
					dictionary.Add("http://www.w3.org/2000/09/xmldsig#rsa-sha1", typeFromHandle18);
					dictionary.Add("System.Security.Cryptography.RSASignatureDescription", typeFromHandle18);
					dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", typeFromHandle19);
					dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#rsa-sha384", typeFromHandle20);
					dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#rsa-sha512", typeFromHandle21);
					dictionary.Add("http://www.w3.org/2000/09/xmldsig#sha1", typeFromHandle);
					dictionary.Add("http://www.w3.org/2001/04/xmlenc#sha256", obj);
					dictionary.Add("http://www.w3.org/2001/04/xmlenc#sha512", obj3);
					dictionary.Add("http://www.w3.org/2001/04/xmlenc#ripemd160", typeFromHandle3);
					dictionary.Add("http://www.w3.org/2001/04/xmlenc#des-cbc", typeFromHandle13);
					dictionary.Add("http://www.w3.org/2001/04/xmlenc#tripledes-cbc", typeFromHandle14);
					dictionary.Add("http://www.w3.org/2001/04/xmlenc#kw-tripledes", typeFromHandle14);
					dictionary.Add("http://www.w3.org/2001/04/xmlenc#aes128-cbc", typeFromHandle16);
					dictionary.Add("http://www.w3.org/2001/04/xmlenc#kw-aes128", typeFromHandle16);
					dictionary.Add("http://www.w3.org/2001/04/xmlenc#aes192-cbc", typeFromHandle16);
					dictionary.Add("http://www.w3.org/2001/04/xmlenc#kw-aes192", typeFromHandle16);
					dictionary.Add("http://www.w3.org/2001/04/xmlenc#aes256-cbc", typeFromHandle16);
					dictionary.Add("http://www.w3.org/2001/04/xmlenc#kw-aes256", typeFromHandle16);
					dictionary.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315", "System.Security.Cryptography.Xml.XmlDsigC14NTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments", "System.Security.Cryptography.Xml.XmlDsigC14NWithCommentsTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/2001/10/xml-exc-c14n#", "System.Security.Cryptography.Xml.XmlDsigExcC14NTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/2001/10/xml-exc-c14n#WithComments", "System.Security.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/2000/09/xmldsig#base64", "System.Security.Cryptography.Xml.XmlDsigBase64Transform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/TR/1999/REC-xpath-19991116", "System.Security.Cryptography.Xml.XmlDsigXPathTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/TR/1999/REC-xslt-19991116", "System.Security.Cryptography.Xml.XmlDsigXsltTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/2000/09/xmldsig#enveloped-signature", "System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/2002/07/decrypt#XML", "System.Security.Cryptography.Xml.XmlDecryptionTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform", "System.Security.Cryptography.Xml.XmlLicenseTransform, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/2000/09/xmldsig# X509Data", "System.Security.Cryptography.Xml.KeyInfoX509Data, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/2000/09/xmldsig# KeyName", "System.Security.Cryptography.Xml.KeyInfoName, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/2000/09/xmldsig# KeyValue/DSAKeyValue", "System.Security.Cryptography.Xml.DSAKeyValue, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/2000/09/xmldsig# KeyValue/RSAKeyValue", "System.Security.Cryptography.Xml.RSAKeyValue, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/2000/09/xmldsig# RetrievalMethod", "System.Security.Cryptography.Xml.KeyInfoRetrievalMethod, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/2001/04/xmlenc# EncryptedKey", "System.Security.Cryptography.Xml.KeyInfoEncryptedKey, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("http://www.w3.org/2000/09/xmldsig#hmac-sha1", typeFromHandle6);
					dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#md5", typeFromHandle2);
					dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#sha384", obj2);
					dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-md5", typeFromHandle4);
					dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160", typeFromHandle5);
					dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha256", typeFromHandle7);
					dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha384", typeFromHandle8);
					dictionary.Add("http://www.w3.org/2001/04/xmldsig-more#hmac-sha512", typeFromHandle9);
					dictionary.Add("2.5.29.10", "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
					dictionary.Add("2.5.29.19", "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
					dictionary.Add("2.5.29.14", "System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
					dictionary.Add("2.5.29.15", "System.Security.Cryptography.X509Certificates.X509KeyUsageExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
					dictionary.Add("2.5.29.37", "System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
					dictionary.Add("X509Chain", "System.Security.Cryptography.X509Certificates.X509Chain, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
					dictionary.Add("1.2.840.113549.1.9.3", "System.Security.Cryptography.Pkcs.Pkcs9ContentType, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("1.2.840.113549.1.9.4", "System.Security.Cryptography.Pkcs.Pkcs9MessageDigest, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("1.2.840.113549.1.9.5", "System.Security.Cryptography.Pkcs.Pkcs9SigningTime, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("1.3.6.1.4.1.311.88.2.1", "System.Security.Cryptography.Pkcs.Pkcs9DocumentName, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					dictionary.Add("1.3.6.1.4.1.311.88.2.2", "System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription, System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
					CryptoConfig.defaultNameHT = dictionary;
				}
				return CryptoConfig.defaultNameHT;
			}
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00074654 File Offset: 0x00072854
		[SecurityCritical]
		private static void InitializeConfigInfo()
		{
			if (CryptoConfig.machineNameHT == null)
			{
				object internalSyncObject = CryptoConfig.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (CryptoConfig.machineNameHT == null)
					{
						ConfigNode configNode = CryptoConfig.OpenCryptoConfig();
						if (configNode != null)
						{
							foreach (ConfigNode configNode2 in configNode.Children)
							{
								if (CryptoConfig.machineNameHT != null && CryptoConfig.machineOidHT != null)
								{
									break;
								}
								if (CryptoConfig.machineNameHT == null && string.Compare(configNode2.Name, "cryptoNameMapping", StringComparison.Ordinal) == 0)
								{
									CryptoConfig.machineNameHT = CryptoConfig.InitializeNameMappings(configNode2);
								}
								else if (CryptoConfig.machineOidHT == null && string.Compare(configNode2.Name, "oidMap", StringComparison.Ordinal) == 0)
								{
									CryptoConfig.machineOidHT = CryptoConfig.InitializeOidMappings(configNode2);
								}
							}
						}
						if (CryptoConfig.machineNameHT == null)
						{
							CryptoConfig.machineNameHT = new Dictionary<string, string>();
						}
						if (CryptoConfig.machineOidHT == null)
						{
							CryptoConfig.machineOidHT = new Dictionary<string, string>();
						}
					}
				}
			}
		}

		/// <summary>Adds a set of names to algorithm mappings to be used for the current application domain.</summary>
		/// <param name="algorithm">The algorithm to map to.</param>
		/// <param name="names">An array of names to map to the algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithm" /> or <paramref name="names" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="algorithm" /> cannot be accessed from outside the assembly.  
		/// -or-  
		/// One of the entries in the <paramref name="names" /> parameter is empty or <see langword="null" />.</exception>
		// Token: 0x0600212E RID: 8494 RVA: 0x00074784 File Offset: 0x00072984
		[SecurityCritical]
		public static void AddAlgorithm(Type algorithm, params string[] names)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (!algorithm.IsVisible)
			{
				throw new ArgumentException(Environment.GetResourceString("Cryptography_AlgorithmTypesMustBeVisible"), "algorithm");
			}
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			string[] array = new string[names.Length];
			Array.Copy(names, array, array.Length);
			foreach (string text in array)
			{
				if (string.IsNullOrEmpty(text))
				{
					throw new ArgumentException(Environment.GetResourceString("Cryptography_AddNullOrEmptyName"));
				}
			}
			object internalSyncObject = CryptoConfig.InternalSyncObject;
			lock (internalSyncObject)
			{
				foreach (string text2 in array)
				{
					CryptoConfig.appNameHT[text2] = algorithm;
				}
			}
		}

		/// <summary>Creates a new instance of the specified cryptographic object with the specified arguments.</summary>
		/// <param name="name">The simple name of the cryptographic object of which to create an instance.</param>
		/// <param name="args">The arguments used to create the specified cryptographic object.</param>
		/// <returns>A new instance of the specified cryptographic object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="name" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x0600212F RID: 8495 RVA: 0x0007486C File Offset: 0x00072A6C
		[SecuritySafeCritical]
		public static object CreateFromName(string name, params object[] args)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Type type = null;
			CryptoConfig.InitializeConfigInfo();
			object internalSyncObject = CryptoConfig.InternalSyncObject;
			lock (internalSyncObject)
			{
				type = CryptoConfig.appNameHT.GetValueOrDefault(name);
			}
			if (type == null)
			{
				string valueOrDefault = CryptoConfig.machineNameHT.GetValueOrDefault(name);
				if (valueOrDefault != null)
				{
					type = Type.GetType(valueOrDefault, false, false);
					if (type != null && !type.IsVisible)
					{
						type = null;
					}
				}
			}
			if (type == null)
			{
				object valueOrDefault2 = CryptoConfig.DefaultNameHT.GetValueOrDefault(name);
				if (valueOrDefault2 != null)
				{
					if (valueOrDefault2 is Type)
					{
						type = (Type)valueOrDefault2;
					}
					else if (valueOrDefault2 is string)
					{
						type = Type.GetType((string)valueOrDefault2, false, false);
						if (type != null && !type.IsVisible)
						{
							type = null;
						}
					}
				}
			}
			if (type == null)
			{
				type = Type.GetType(name, false, false);
				if (type != null && !type.IsVisible)
				{
					type = null;
				}
			}
			if (type == null)
			{
				return null;
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				return null;
			}
			if (args == null)
			{
				args = new object[0];
			}
			MethodBase[] constructors = runtimeType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance);
			MethodBase[] array = constructors;
			if (array == null)
			{
				return null;
			}
			List<MethodBase> list = new List<MethodBase>();
			foreach (MethodBase methodBase in array)
			{
				if (methodBase.GetParameters().Length == args.Length)
				{
					list.Add(methodBase);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			array = list.ToArray();
			object obj;
			RuntimeConstructorInfo runtimeConstructorInfo = Type.DefaultBinder.BindToMethod(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, array, ref args, null, null, null, out obj) as RuntimeConstructorInfo;
			if (runtimeConstructorInfo == null || typeof(Delegate).IsAssignableFrom(runtimeConstructorInfo.DeclaringType))
			{
				return null;
			}
			object obj2 = runtimeConstructorInfo.Invoke(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, Type.DefaultBinder, args, null);
			if (obj != null)
			{
				Type.DefaultBinder.ReorderArgumentArray(ref args, obj);
			}
			return obj2;
		}

		/// <summary>Creates a new instance of the specified cryptographic object.</summary>
		/// <param name="name">The simple name of the cryptographic object of which to create an instance.</param>
		/// <returns>A new instance of the specified cryptographic object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="name" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x06002130 RID: 8496 RVA: 0x00074A74 File Offset: 0x00072C74
		public static object CreateFromName(string name)
		{
			return CryptoConfig.CreateFromName(name, null);
		}

		/// <summary>Adds a set of names to object identifier (OID) mappings to be used for the current application domain.</summary>
		/// <param name="oid">The object identifier (OID) to map to.</param>
		/// <param name="names">An array of names to map to the OID.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="oid" /> or <paramref name="names" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">One of the entries in the <paramref name="names" /> parameter is empty or <see langword="null" />.</exception>
		// Token: 0x06002131 RID: 8497 RVA: 0x00074A80 File Offset: 0x00072C80
		[SecurityCritical]
		public static void AddOID(string oid, params string[] names)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			string[] array = new string[names.Length];
			Array.Copy(names, array, array.Length);
			foreach (string text in array)
			{
				if (string.IsNullOrEmpty(text))
				{
					throw new ArgumentException(Environment.GetResourceString("Cryptography_AddNullOrEmptyName"));
				}
			}
			object internalSyncObject = CryptoConfig.InternalSyncObject;
			lock (internalSyncObject)
			{
				foreach (string text2 in array)
				{
					CryptoConfig.appOidHT[text2] = oid;
				}
			}
		}

		/// <summary>Gets the object identifier (OID) of the algorithm corresponding to the specified simple name.</summary>
		/// <param name="name">The simple name of the algorithm for which to get the OID.</param>
		/// <returns>The OID of the specified algorithm.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002132 RID: 8498 RVA: 0x00074B48 File Offset: 0x00072D48
		public static string MapNameToOID(string name)
		{
			return CryptoConfig.MapNameToOID(name, OidGroup.AllGroups);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x00074B54 File Offset: 0x00072D54
		[SecuritySafeCritical]
		internal static string MapNameToOID(string name, OidGroup oidGroup)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			CryptoConfig.InitializeConfigInfo();
			string text = null;
			object internalSyncObject = CryptoConfig.InternalSyncObject;
			lock (internalSyncObject)
			{
				text = CryptoConfig.appOidHT.GetValueOrDefault(name);
			}
			if (text == null)
			{
				text = CryptoConfig.machineOidHT.GetValueOrDefault(name);
			}
			if (text == null)
			{
				text = CryptoConfig.DefaultOidHT.GetValueOrDefault(name);
			}
			if (text == null)
			{
				text = X509Utils.GetOidFromFriendlyName(name, oidGroup);
			}
			return text;
		}

		/// <summary>Encodes the specified object identifier (OID).</summary>
		/// <param name="str">The OID to encode.</param>
		/// <returns>A byte array containing the encoded OID.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="str" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">An error occurred while encoding the OID.</exception>
		// Token: 0x06002134 RID: 8500 RVA: 0x00074BDC File Offset: 0x00072DDC
		public static byte[] EncodeOID(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			char[] array = new char[] { '.' };
			string[] array2 = str.Split(array);
			uint[] array3 = new uint[array2.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array3[i] = (uint)int.Parse(array2[i], CultureInfo.InvariantCulture);
			}
			byte[] array4 = new byte[array3.Length * 5];
			int num = 0;
			if (array3.Length < 2)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_InvalidOID"));
			}
			uint num2 = array3[0] * 40U + array3[1];
			byte[] array5 = CryptoConfig.EncodeSingleOIDNum(num2);
			Array.Copy(array5, 0, array4, num, array5.Length);
			num += array5.Length;
			for (int j = 2; j < array3.Length; j++)
			{
				array5 = CryptoConfig.EncodeSingleOIDNum(array3[j]);
				Buffer.InternalBlockCopy(array5, 0, array4, num, array5.Length);
				num += array5.Length;
			}
			if (num > 127)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_Config_EncodedOIDError"));
			}
			array5 = new byte[num + 2];
			array5[0] = 6;
			array5[1] = (byte)num;
			Buffer.InternalBlockCopy(array4, 0, array5, 2, num);
			return array5;
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x00074CFC File Offset: 0x00072EFC
		private static byte[] EncodeSingleOIDNum(uint dwValue)
		{
			if (dwValue < 128U)
			{
				return new byte[] { (byte)dwValue };
			}
			if (dwValue < 16384U)
			{
				return new byte[]
				{
					(byte)((dwValue >> 7) | 128U),
					(byte)(dwValue & 127U)
				};
			}
			if (dwValue < 2097152U)
			{
				return new byte[]
				{
					(byte)((dwValue >> 14) | 128U),
					(byte)((dwValue >> 7) | 128U),
					(byte)(dwValue & 127U)
				};
			}
			if (dwValue < 268435456U)
			{
				return new byte[]
				{
					(byte)((dwValue >> 21) | 128U),
					(byte)((dwValue >> 14) | 128U),
					(byte)((dwValue >> 7) | 128U),
					(byte)(dwValue & 127U)
				};
			}
			return new byte[]
			{
				(byte)((dwValue >> 28) | 128U),
				(byte)((dwValue >> 21) | 128U),
				(byte)((dwValue >> 14) | 128U),
				(byte)((dwValue >> 7) | 128U),
				(byte)(dwValue & 127U)
			};
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x00074E04 File Offset: 0x00073004
		private static Dictionary<string, string> InitializeNameMappings(ConfigNode nameMappingNode)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			foreach (ConfigNode configNode in nameMappingNode.Children)
			{
				if (string.Compare(configNode.Name, "cryptoClasses", StringComparison.Ordinal) == 0)
				{
					using (List<ConfigNode>.Enumerator enumerator2 = configNode.Children.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ConfigNode configNode2 = enumerator2.Current;
							if (string.Compare(configNode2.Name, "cryptoClass", StringComparison.Ordinal) == 0 && configNode2.Attributes.Count > 0)
							{
								DictionaryEntry dictionaryEntry = configNode2.Attributes[0];
								dictionary2.Add((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
							}
						}
						continue;
					}
				}
				if (string.Compare(configNode.Name, "nameEntry", StringComparison.Ordinal) == 0)
				{
					string text = null;
					string text2 = null;
					foreach (DictionaryEntry dictionaryEntry2 in configNode.Attributes)
					{
						if (string.Compare((string)dictionaryEntry2.Key, "name", StringComparison.Ordinal) == 0)
						{
							text = (string)dictionaryEntry2.Value;
						}
						else if (string.Compare((string)dictionaryEntry2.Key, "class", StringComparison.Ordinal) == 0)
						{
							text2 = (string)dictionaryEntry2.Value;
						}
					}
					if (text != null && text2 != null)
					{
						string valueOrDefault = dictionary2.GetValueOrDefault(text2);
						if (valueOrDefault != null)
						{
							dictionary.Add(text, valueOrDefault);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x00074FF8 File Offset: 0x000731F8
		private static Dictionary<string, string> InitializeOidMappings(ConfigNode oidMappingNode)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (ConfigNode configNode in oidMappingNode.Children)
			{
				if (string.Compare(configNode.Name, "oidEntry", StringComparison.Ordinal) == 0)
				{
					string text = null;
					string text2 = null;
					foreach (DictionaryEntry dictionaryEntry in configNode.Attributes)
					{
						if (string.Compare((string)dictionaryEntry.Key, "OID", StringComparison.Ordinal) == 0)
						{
							text = (string)dictionaryEntry.Value;
						}
						else if (string.Compare((string)dictionaryEntry.Key, "name", StringComparison.Ordinal) == 0)
						{
							text2 = (string)dictionaryEntry.Value;
						}
					}
					if (text2 != null && text != null)
					{
						dictionary.Add(text2, text);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00075108 File Offset: 0x00073308
		[SecurityCritical]
		private static ConfigNode OpenCryptoConfig()
		{
			string text = Config.MachineDirectory + "machine.config";
			new FileIOPermission(FileIOPermissionAccess.Read, text).Assert();
			if (!File.Exists(text))
			{
				return null;
			}
			CodeAccessPermission.RevertAssert();
			ConfigTreeParser configTreeParser = new ConfigTreeParser();
			ConfigNode configNode = configTreeParser.Parse(text, "configuration", true);
			if (configNode == null)
			{
				return null;
			}
			ConfigNode configNode2 = null;
			foreach (ConfigNode configNode3 in configNode.Children)
			{
				bool flag = false;
				if (string.Compare(configNode3.Name, "mscorlib", StringComparison.Ordinal) == 0)
				{
					foreach (DictionaryEntry dictionaryEntry in configNode3.Attributes)
					{
						if (string.Compare((string)dictionaryEntry.Key, "version", StringComparison.Ordinal) == 0)
						{
							flag = true;
							if (string.Compare((string)dictionaryEntry.Value, CryptoConfig.Version, StringComparison.Ordinal) == 0)
							{
								configNode2 = configNode3;
								break;
							}
						}
					}
					if (!flag)
					{
						configNode2 = configNode3;
					}
				}
				if (configNode2 != null)
				{
					break;
				}
			}
			if (configNode2 == null)
			{
				return null;
			}
			foreach (ConfigNode configNode4 in configNode2.Children)
			{
				if (string.Compare(configNode4.Name, "cryptographySettings", StringComparison.Ordinal) == 0)
				{
					return configNode4;
				}
			}
			return null;
		}

		// Token: 0x04000C0E RID: 3086
		private static volatile Dictionary<string, string> defaultOidHT = null;

		// Token: 0x04000C0F RID: 3087
		private static volatile Dictionary<string, object> defaultNameHT = null;

		// Token: 0x04000C10 RID: 3088
		private static volatile Dictionary<string, string> machineOidHT = null;

		// Token: 0x04000C11 RID: 3089
		private static volatile Dictionary<string, string> machineNameHT = null;

		// Token: 0x04000C12 RID: 3090
		private static volatile Dictionary<string, Type> appNameHT = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000C13 RID: 3091
		private static volatile Dictionary<string, string> appOidHT = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000C14 RID: 3092
		private const string MachineConfigFilename = "machine.config";

		// Token: 0x04000C15 RID: 3093
		private static volatile string version = null;

		// Token: 0x04000C16 RID: 3094
		private static volatile bool s_fipsAlgorithmPolicy;

		// Token: 0x04000C17 RID: 3095
		private static volatile bool s_haveFipsAlgorithmPolicy;

		// Token: 0x04000C18 RID: 3096
		private static object s_InternalSyncObject;
	}
}
