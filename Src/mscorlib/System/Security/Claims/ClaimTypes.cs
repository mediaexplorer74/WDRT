using System;
using System.Runtime.InteropServices;

namespace System.Security.Claims
{
	/// <summary>Defines constants for the well-known claim types that can be assigned to a subject. This class cannot be inherited.</summary>
	// Token: 0x0200031D RID: 797
	[ComVisible(false)]
	public static class ClaimTypes
	{
		// Token: 0x04000FB6 RID: 4022
		internal const string ClaimTypeNamespace = "http://schemas.microsoft.com/ws/2008/06/identity/claims";

		/// <summary>The URI for a claim that specifies the instant at which an entity was authenticated; http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant.</summary>
		// Token: 0x04000FB7 RID: 4023
		public const string AuthenticationInstant = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant";

		/// <summary>The URI for a claim that specifies the method with which an entity was authenticated; http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod.</summary>
		// Token: 0x04000FB8 RID: 4024
		public const string AuthenticationMethod = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod";

		/// <summary>The URI for a claim that specifies the cookie path; http://schemas.microsoft.com/ws/2008/06/identity/claims/cookiepath.</summary>
		// Token: 0x04000FB9 RID: 4025
		public const string CookiePath = "http://schemas.microsoft.com/ws/2008/06/identity/claims/cookiepath";

		/// <summary>The URI for a claim that specifies the deny-only primary SID on an entity; http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid. A deny-only SID denies the specified entity to a securable object.</summary>
		// Token: 0x04000FBA RID: 4026
		public const string DenyOnlyPrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid";

		/// <summary>The URI for a claim that specifies the deny-only primary group SID on an entity; http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid. A deny-only SID denies the specified entity to a securable object.</summary>
		// Token: 0x04000FBB RID: 4027
		public const string DenyOnlyPrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup.</summary>
		// Token: 0x04000FBC RID: 4028
		public const string DenyOnlyWindowsDeviceGroup = "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/dsa.</summary>
		// Token: 0x04000FBD RID: 4029
		public const string Dsa = "http://schemas.microsoft.com/ws/2008/06/identity/claims/dsa";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration.</summary>
		// Token: 0x04000FBE RID: 4030
		public const string Expiration = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/expired.</summary>
		// Token: 0x04000FBF RID: 4031
		public const string Expired = "http://schemas.microsoft.com/ws/2008/06/identity/claims/expired";

		/// <summary>The URI for a claim that specifies the SID for the group of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid.</summary>
		// Token: 0x04000FC0 RID: 4032
		public const string GroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/ispersistent.</summary>
		// Token: 0x04000FC1 RID: 4033
		public const string IsPersistent = "http://schemas.microsoft.com/ws/2008/06/identity/claims/ispersistent";

		/// <summary>The URI for a claim that specifies the primary group SID of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid.</summary>
		// Token: 0x04000FC2 RID: 4034
		public const string PrimaryGroupSid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid";

		/// <summary>The URI for a claim that specifies the primary SID of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid.</summary>
		// Token: 0x04000FC3 RID: 4035
		public const string PrimarySid = "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid";

		/// <summary>The URI for a claim that specifies the role of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/role.</summary>
		// Token: 0x04000FC4 RID: 4036
		public const string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

		/// <summary>The URI for a claim that specifies a serial number, http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber.</summary>
		// Token: 0x04000FC5 RID: 4037
		public const string SerialNumber = "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata.</summary>
		// Token: 0x04000FC6 RID: 4038
		public const string UserData = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/version.</summary>
		// Token: 0x04000FC7 RID: 4039
		public const string Version = "http://schemas.microsoft.com/ws/2008/06/identity/claims/version";

		/// <summary>The URI for a claim that specifies the Windows domain account name of an entity, http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname.</summary>
		// Token: 0x04000FC8 RID: 4040
		public const string WindowsAccountName = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim.</summary>
		// Token: 0x04000FC9 RID: 4041
		public const string WindowsDeviceClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup.</summary>
		// Token: 0x04000FCA RID: 4042
		public const string WindowsDeviceGroup = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim.</summary>
		// Token: 0x04000FCB RID: 4043
		public const string WindowsUserClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsfqbnversion.</summary>
		// Token: 0x04000FCC RID: 4044
		public const string WindowsFqbnVersion = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsfqbnversion";

		/// <summary>http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority.</summary>
		// Token: 0x04000FCD RID: 4045
		public const string WindowsSubAuthority = "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority";

		// Token: 0x04000FCE RID: 4046
		internal const string ClaimType2005Namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";

		/// <summary>The URI for a claim that specifies the anonymous user; http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous.</summary>
		// Token: 0x04000FCF RID: 4047
		public const string Anonymous = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous";

		/// <summary>The URI for a claim that specifies details about whether an identity is authenticated, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authenticated.</summary>
		// Token: 0x04000FD0 RID: 4048
		public const string Authentication = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authentication";

		/// <summary>The URI for a claim that specifies an authorization decision on an entity; http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision.</summary>
		// Token: 0x04000FD1 RID: 4049
		public const string AuthorizationDecision = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision";

		/// <summary>The URI for a claim that specifies the country/region in which an entity resides, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country.</summary>
		// Token: 0x04000FD2 RID: 4050
		public const string Country = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country";

		/// <summary>The URI for a claim that specifies the date of birth of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth.</summary>
		// Token: 0x04000FD3 RID: 4051
		public const string DateOfBirth = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth";

		/// <summary>The URI for a claim that specifies the DNS name associated with the computer name or with the alternative name of either the subject or issuer of an X.509 certificate, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns.</summary>
		// Token: 0x04000FD4 RID: 4052
		public const string Dns = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns";

		/// <summary>The URI for a claim that specifies a deny-only security identifier (SID) for an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid. A deny-only SID denies the specified entity to a securable object.</summary>
		// Token: 0x04000FD5 RID: 4053
		public const string DenyOnlySid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid";

		/// <summary>The URI for a claim that specifies the email address of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress.</summary>
		// Token: 0x04000FD6 RID: 4054
		public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

		/// <summary>The URI for a claim that specifies the gender of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender.</summary>
		// Token: 0x04000FD7 RID: 4055
		public const string Gender = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender";

		/// <summary>The URI for a claim that specifies the given name of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname.</summary>
		// Token: 0x04000FD8 RID: 4056
		public const string GivenName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";

		/// <summary>The URI for a claim that specifies a hash value, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash.</summary>
		// Token: 0x04000FD9 RID: 4057
		public const string Hash = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash";

		/// <summary>The URI for a claim that specifies the home phone number of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone.</summary>
		// Token: 0x04000FDA RID: 4058
		public const string HomePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone";

		/// <summary>The URI for a claim that specifies the locale in which an entity resides, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality.</summary>
		// Token: 0x04000FDB RID: 4059
		public const string Locality = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality";

		/// <summary>The URI for a claim that specifies the mobile phone number of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone.</summary>
		// Token: 0x04000FDC RID: 4060
		public const string MobilePhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone";

		/// <summary>The URI for a claim that specifies the name of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name.</summary>
		// Token: 0x04000FDD RID: 4061
		public const string Name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

		/// <summary>The URI for a claim that specifies the name of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier.</summary>
		// Token: 0x04000FDE RID: 4062
		public const string NameIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

		/// <summary>The URI for a claim that specifies the alternative phone number of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone.</summary>
		// Token: 0x04000FDF RID: 4063
		public const string OtherPhone = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone";

		/// <summary>The URI for a claim that specifies the postal code of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode.</summary>
		// Token: 0x04000FE0 RID: 4064
		public const string PostalCode = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode";

		/// <summary>The URI for a claim that specifies an RSA key, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa.</summary>
		// Token: 0x04000FE1 RID: 4065
		public const string Rsa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";

		/// <summary>The URI for a claim that specifies a security identifier (SID), http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid.</summary>
		// Token: 0x04000FE2 RID: 4066
		public const string Sid = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid";

		/// <summary>The URI for a claim that specifies a service principal name (SPN) claim, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn.</summary>
		// Token: 0x04000FE3 RID: 4067
		public const string Spn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn";

		/// <summary>The URI for a claim that specifies the state or province in which an entity resides, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince.</summary>
		// Token: 0x04000FE4 RID: 4068
		public const string StateOrProvince = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince";

		/// <summary>The URI for a claim that specifies the street address of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress.</summary>
		// Token: 0x04000FE5 RID: 4069
		public const string StreetAddress = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress";

		/// <summary>The URI for a claim that specifies the surname of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname.</summary>
		// Token: 0x04000FE6 RID: 4070
		public const string Surname = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";

		/// <summary>The URI for a claim that identifies the system entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system.</summary>
		// Token: 0x04000FE7 RID: 4071
		public const string System = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system";

		/// <summary>The URI for a claim that specifies a thumbprint, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint. A thumbprint is a globally unique SHA-1 hash of an X.509 certificate.</summary>
		// Token: 0x04000FE8 RID: 4072
		public const string Thumbprint = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint";

		/// <summary>The URI for a claim that specifies a user principal name (UPN), http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn.</summary>
		// Token: 0x04000FE9 RID: 4073
		public const string Upn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn";

		/// <summary>The URI for a claim that specifies a URI, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri.</summary>
		// Token: 0x04000FEA RID: 4074
		public const string Uri = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri";

		/// <summary>The URI for a claim that specifies the webpage of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage.</summary>
		// Token: 0x04000FEB RID: 4075
		public const string Webpage = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage";

		/// <summary>The URI for a distinguished name claim of an X.509 certificate, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname. The X.500 standard defines the methodology for defining distinguished names that are used by X.509 certificates.</summary>
		// Token: 0x04000FEC RID: 4076
		public const string X500DistinguishedName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname";

		// Token: 0x04000FED RID: 4077
		internal const string ClaimType2009Namespace = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims";

		/// <summary>http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor.</summary>
		// Token: 0x04000FEE RID: 4078
		public const string Actor = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor";
	}
}
