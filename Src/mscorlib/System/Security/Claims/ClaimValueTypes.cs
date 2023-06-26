using System;
using System.Runtime.InteropServices;

namespace System.Security.Claims
{
	/// <summary>Defines claim value types according to the type URIs defined by W3C and OASIS. This class cannot be inherited.</summary>
	// Token: 0x0200031E RID: 798
	[ComVisible(false)]
	public static class ClaimValueTypes
	{
		// Token: 0x04000FEF RID: 4079
		private const string XmlSchemaNamespace = "http://www.w3.org/2001/XMLSchema";

		/// <summary>A URI that represents the <see langword="base64Binary" /> XML data type.</summary>
		// Token: 0x04000FF0 RID: 4080
		public const string Base64Binary = "http://www.w3.org/2001/XMLSchema#base64Binary";

		/// <summary>A URI that represents the <see langword="base64Octet" /> XML data type.</summary>
		// Token: 0x04000FF1 RID: 4081
		public const string Base64Octet = "http://www.w3.org/2001/XMLSchema#base64Octet";

		/// <summary>A URI that represents the <see langword="boolean" /> XML data type.</summary>
		// Token: 0x04000FF2 RID: 4082
		public const string Boolean = "http://www.w3.org/2001/XMLSchema#boolean";

		/// <summary>A URI that represents the <see langword="date" /> XML data type.</summary>
		// Token: 0x04000FF3 RID: 4083
		public const string Date = "http://www.w3.org/2001/XMLSchema#date";

		/// <summary>A URI that represents the <see langword="dateTime" /> XML data type.</summary>
		// Token: 0x04000FF4 RID: 4084
		public const string DateTime = "http://www.w3.org/2001/XMLSchema#dateTime";

		/// <summary>A URI that represents the <see langword="double" /> XML data type.</summary>
		// Token: 0x04000FF5 RID: 4085
		public const string Double = "http://www.w3.org/2001/XMLSchema#double";

		/// <summary>A URI that represents the <see langword="fqbn" /> XML data type.</summary>
		// Token: 0x04000FF6 RID: 4086
		public const string Fqbn = "http://www.w3.org/2001/XMLSchema#fqbn";

		/// <summary>A URI that represents the <see langword="hexBinary" /> XML data type.</summary>
		// Token: 0x04000FF7 RID: 4087
		public const string HexBinary = "http://www.w3.org/2001/XMLSchema#hexBinary";

		/// <summary>A URI that represents the <see langword="integer" /> XML data type.</summary>
		// Token: 0x04000FF8 RID: 4088
		public const string Integer = "http://www.w3.org/2001/XMLSchema#integer";

		/// <summary>A URI that represents the <see langword="integer32" /> XML data type.</summary>
		// Token: 0x04000FF9 RID: 4089
		public const string Integer32 = "http://www.w3.org/2001/XMLSchema#integer32";

		/// <summary>A URI that represents the <see langword="integer64" /> XML data type.</summary>
		// Token: 0x04000FFA RID: 4090
		public const string Integer64 = "http://www.w3.org/2001/XMLSchema#integer64";

		/// <summary>A URI that represents the <see langword="sid" /> XML data type.</summary>
		// Token: 0x04000FFB RID: 4091
		public const string Sid = "http://www.w3.org/2001/XMLSchema#sid";

		/// <summary>A URI that represents the <see langword="string" /> XML data type.</summary>
		// Token: 0x04000FFC RID: 4092
		public const string String = "http://www.w3.org/2001/XMLSchema#string";

		/// <summary>A URI that represents the <see langword="time" /> XML data type.</summary>
		// Token: 0x04000FFD RID: 4093
		public const string Time = "http://www.w3.org/2001/XMLSchema#time";

		/// <summary>A URI that represents the <see langword="uinteger32" /> XML data type.</summary>
		// Token: 0x04000FFE RID: 4094
		public const string UInteger32 = "http://www.w3.org/2001/XMLSchema#uinteger32";

		/// <summary>A URI that represents the <see langword="uinteger64" /> XML data type.</summary>
		// Token: 0x04000FFF RID: 4095
		public const string UInteger64 = "http://www.w3.org/2001/XMLSchema#uinteger64";

		// Token: 0x04001000 RID: 4096
		private const string SoapSchemaNamespace = "http://schemas.xmlsoap.org/";

		/// <summary>A URI that represents the <see langword="dns" /> SOAP data type.</summary>
		// Token: 0x04001001 RID: 4097
		public const string DnsName = "http://schemas.xmlsoap.org/claims/dns";

		/// <summary>A URI that represents the <see langword="emailaddress" /> SOAP data type.</summary>
		// Token: 0x04001002 RID: 4098
		public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

		/// <summary>A URI that represents the <see langword="rsa" /> SOAP data type.</summary>
		// Token: 0x04001003 RID: 4099
		public const string Rsa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";

		/// <summary>A URI that represents the <see langword="UPN" /> SOAP data type.</summary>
		// Token: 0x04001004 RID: 4100
		public const string UpnName = "http://schemas.xmlsoap.org/claims/UPN";

		// Token: 0x04001005 RID: 4101
		private const string XmlSignatureConstantsNamespace = "http://www.w3.org/2000/09/xmldsig#";

		/// <summary>A URI that represents the <see langword="DSAKeyValue" /> XML Signature data type.</summary>
		// Token: 0x04001006 RID: 4102
		public const string DsaKeyValue = "http://www.w3.org/2000/09/xmldsig#DSAKeyValue";

		/// <summary>A URI that represents the <see langword="KeyInfo" /> XML Signature data type.</summary>
		// Token: 0x04001007 RID: 4103
		public const string KeyInfo = "http://www.w3.org/2000/09/xmldsig#KeyInfo";

		/// <summary>A URI that represents the <see langword="RSAKeyValue" /> XML Signature data type.</summary>
		// Token: 0x04001008 RID: 4104
		public const string RsaKeyValue = "http://www.w3.org/2000/09/xmldsig#RSAKeyValue";

		// Token: 0x04001009 RID: 4105
		private const string XQueryOperatorsNameSpace = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816";

		/// <summary>A URI that represents the <see langword="daytimeDuration" /> XQuery data type.</summary>
		// Token: 0x0400100A RID: 4106
		public const string DaytimeDuration = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816#dayTimeDuration";

		/// <summary>A URI that represents the <see langword="yearMonthDuration" /> XQuery data type.</summary>
		// Token: 0x0400100B RID: 4107
		public const string YearMonthDuration = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816#yearMonthDuration";

		// Token: 0x0400100C RID: 4108
		private const string Xacml10Namespace = "urn:oasis:names:tc:xacml:1.0";

		/// <summary>A URI that represents the <see langword="rfc822Name" /> XACML 1.0 data type.</summary>
		// Token: 0x0400100D RID: 4109
		public const string Rfc822Name = "urn:oasis:names:tc:xacml:1.0:data-type:rfc822Name";

		/// <summary>A URI that represents the <see langword="x500Name" /> XACML 1.0 data type.</summary>
		// Token: 0x0400100E RID: 4110
		public const string X500Name = "urn:oasis:names:tc:xacml:1.0:data-type:x500Name";
	}
}
