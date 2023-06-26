using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Security.Cryptography
{
	// Token: 0x02000451 RID: 1105
	internal abstract class CAPIBase
	{
		// Token: 0x04002272 RID: 8818
		internal const string ADVAPI32 = "advapi32.dll";

		// Token: 0x04002273 RID: 8819
		internal const string CRYPT32 = "crypt32.dll";

		// Token: 0x04002274 RID: 8820
		internal const string KERNEL32 = "kernel32.dll";

		// Token: 0x04002275 RID: 8821
		internal const uint LMEM_FIXED = 0U;

		// Token: 0x04002276 RID: 8822
		internal const uint LMEM_ZEROINIT = 64U;

		// Token: 0x04002277 RID: 8823
		internal const uint LPTR = 64U;

		// Token: 0x04002278 RID: 8824
		internal const int S_OK = 0;

		// Token: 0x04002279 RID: 8825
		internal const int S_FALSE = 1;

		// Token: 0x0400227A RID: 8826
		internal const uint FORMAT_MESSAGE_FROM_SYSTEM = 4096U;

		// Token: 0x0400227B RID: 8827
		internal const uint FORMAT_MESSAGE_IGNORE_INSERTS = 512U;

		// Token: 0x0400227C RID: 8828
		internal const uint VER_PLATFORM_WIN32s = 0U;

		// Token: 0x0400227D RID: 8829
		internal const uint VER_PLATFORM_WIN32_WINDOWS = 1U;

		// Token: 0x0400227E RID: 8830
		internal const uint VER_PLATFORM_WIN32_NT = 2U;

		// Token: 0x0400227F RID: 8831
		internal const uint VER_PLATFORM_WINCE = 3U;

		// Token: 0x04002280 RID: 8832
		internal const uint ASN_TAG_NULL = 5U;

		// Token: 0x04002281 RID: 8833
		internal const uint ASN_TAG_OBJID = 6U;

		// Token: 0x04002282 RID: 8834
		internal const uint CERT_QUERY_OBJECT_FILE = 1U;

		// Token: 0x04002283 RID: 8835
		internal const uint CERT_QUERY_OBJECT_BLOB = 2U;

		// Token: 0x04002284 RID: 8836
		internal const uint CERT_QUERY_CONTENT_CERT = 1U;

		// Token: 0x04002285 RID: 8837
		internal const uint CERT_QUERY_CONTENT_CTL = 2U;

		// Token: 0x04002286 RID: 8838
		internal const uint CERT_QUERY_CONTENT_CRL = 3U;

		// Token: 0x04002287 RID: 8839
		internal const uint CERT_QUERY_CONTENT_SERIALIZED_STORE = 4U;

		// Token: 0x04002288 RID: 8840
		internal const uint CERT_QUERY_CONTENT_SERIALIZED_CERT = 5U;

		// Token: 0x04002289 RID: 8841
		internal const uint CERT_QUERY_CONTENT_SERIALIZED_CTL = 6U;

		// Token: 0x0400228A RID: 8842
		internal const uint CERT_QUERY_CONTENT_SERIALIZED_CRL = 7U;

		// Token: 0x0400228B RID: 8843
		internal const uint CERT_QUERY_CONTENT_PKCS7_SIGNED = 8U;

		// Token: 0x0400228C RID: 8844
		internal const uint CERT_QUERY_CONTENT_PKCS7_UNSIGNED = 9U;

		// Token: 0x0400228D RID: 8845
		internal const uint CERT_QUERY_CONTENT_PKCS7_SIGNED_EMBED = 10U;

		// Token: 0x0400228E RID: 8846
		internal const uint CERT_QUERY_CONTENT_PKCS10 = 11U;

		// Token: 0x0400228F RID: 8847
		internal const uint CERT_QUERY_CONTENT_PFX = 12U;

		// Token: 0x04002290 RID: 8848
		internal const uint CERT_QUERY_CONTENT_CERT_PAIR = 13U;

		// Token: 0x04002291 RID: 8849
		internal const uint CERT_QUERY_CONTENT_FLAG_CERT = 2U;

		// Token: 0x04002292 RID: 8850
		internal const uint CERT_QUERY_CONTENT_FLAG_CTL = 4U;

		// Token: 0x04002293 RID: 8851
		internal const uint CERT_QUERY_CONTENT_FLAG_CRL = 8U;

		// Token: 0x04002294 RID: 8852
		internal const uint CERT_QUERY_CONTENT_FLAG_SERIALIZED_STORE = 16U;

		// Token: 0x04002295 RID: 8853
		internal const uint CERT_QUERY_CONTENT_FLAG_SERIALIZED_CERT = 32U;

		// Token: 0x04002296 RID: 8854
		internal const uint CERT_QUERY_CONTENT_FLAG_SERIALIZED_CTL = 64U;

		// Token: 0x04002297 RID: 8855
		internal const uint CERT_QUERY_CONTENT_FLAG_SERIALIZED_CRL = 128U;

		// Token: 0x04002298 RID: 8856
		internal const uint CERT_QUERY_CONTENT_FLAG_PKCS7_SIGNED = 256U;

		// Token: 0x04002299 RID: 8857
		internal const uint CERT_QUERY_CONTENT_FLAG_PKCS7_UNSIGNED = 512U;

		// Token: 0x0400229A RID: 8858
		internal const uint CERT_QUERY_CONTENT_FLAG_PKCS7_SIGNED_EMBED = 1024U;

		// Token: 0x0400229B RID: 8859
		internal const uint CERT_QUERY_CONTENT_FLAG_PKCS10 = 2048U;

		// Token: 0x0400229C RID: 8860
		internal const uint CERT_QUERY_CONTENT_FLAG_PFX = 4096U;

		// Token: 0x0400229D RID: 8861
		internal const uint CERT_QUERY_CONTENT_FLAG_CERT_PAIR = 8192U;

		// Token: 0x0400229E RID: 8862
		internal const uint CERT_QUERY_CONTENT_FLAG_ALL = 16382U;

		// Token: 0x0400229F RID: 8863
		internal const uint CERT_QUERY_FORMAT_BINARY = 1U;

		// Token: 0x040022A0 RID: 8864
		internal const uint CERT_QUERY_FORMAT_BASE64_ENCODED = 2U;

		// Token: 0x040022A1 RID: 8865
		internal const uint CERT_QUERY_FORMAT_ASN_ASCII_HEX_ENCODED = 3U;

		// Token: 0x040022A2 RID: 8866
		internal const uint CERT_QUERY_FORMAT_FLAG_BINARY = 2U;

		// Token: 0x040022A3 RID: 8867
		internal const uint CERT_QUERY_FORMAT_FLAG_BASE64_ENCODED = 4U;

		// Token: 0x040022A4 RID: 8868
		internal const uint CERT_QUERY_FORMAT_FLAG_ASN_ASCII_HEX_ENCODED = 8U;

		// Token: 0x040022A5 RID: 8869
		internal const uint CERT_QUERY_FORMAT_FLAG_ALL = 14U;

		// Token: 0x040022A6 RID: 8870
		internal const uint CRYPT_OID_INFO_OID_KEY = 1U;

		// Token: 0x040022A7 RID: 8871
		internal const uint CRYPT_OID_INFO_NAME_KEY = 2U;

		// Token: 0x040022A8 RID: 8872
		internal const uint CRYPT_OID_INFO_ALGID_KEY = 3U;

		// Token: 0x040022A9 RID: 8873
		internal const uint CRYPT_OID_INFO_SIGN_KEY = 4U;

		// Token: 0x040022AA RID: 8874
		internal const uint CRYPT_HASH_ALG_OID_GROUP_ID = 1U;

		// Token: 0x040022AB RID: 8875
		internal const uint CRYPT_ENCRYPT_ALG_OID_GROUP_ID = 2U;

		// Token: 0x040022AC RID: 8876
		internal const uint CRYPT_PUBKEY_ALG_OID_GROUP_ID = 3U;

		// Token: 0x040022AD RID: 8877
		internal const uint CRYPT_SIGN_ALG_OID_GROUP_ID = 4U;

		// Token: 0x040022AE RID: 8878
		internal const uint CRYPT_RDN_ATTR_OID_GROUP_ID = 5U;

		// Token: 0x040022AF RID: 8879
		internal const uint CRYPT_EXT_OR_ATTR_OID_GROUP_ID = 6U;

		// Token: 0x040022B0 RID: 8880
		internal const uint CRYPT_ENHKEY_USAGE_OID_GROUP_ID = 7U;

		// Token: 0x040022B1 RID: 8881
		internal const uint CRYPT_POLICY_OID_GROUP_ID = 8U;

		// Token: 0x040022B2 RID: 8882
		internal const uint CRYPT_TEMPLATE_OID_GROUP_ID = 9U;

		// Token: 0x040022B3 RID: 8883
		internal const uint CRYPT_LAST_OID_GROUP_ID = 9U;

		// Token: 0x040022B4 RID: 8884
		internal const uint CRYPT_FIRST_ALG_OID_GROUP_ID = 1U;

		// Token: 0x040022B5 RID: 8885
		internal const uint CRYPT_LAST_ALG_OID_GROUP_ID = 4U;

		// Token: 0x040022B6 RID: 8886
		internal const uint CRYPT_ASN_ENCODING = 1U;

		// Token: 0x040022B7 RID: 8887
		internal const uint CRYPT_NDR_ENCODING = 2U;

		// Token: 0x040022B8 RID: 8888
		internal const uint X509_ASN_ENCODING = 1U;

		// Token: 0x040022B9 RID: 8889
		internal const uint X509_NDR_ENCODING = 2U;

		// Token: 0x040022BA RID: 8890
		internal const uint PKCS_7_ASN_ENCODING = 65536U;

		// Token: 0x040022BB RID: 8891
		internal const uint PKCS_7_NDR_ENCODING = 131072U;

		// Token: 0x040022BC RID: 8892
		internal const uint PKCS_7_OR_X509_ASN_ENCODING = 65537U;

		// Token: 0x040022BD RID: 8893
		internal const uint CERT_STORE_PROV_MSG = 1U;

		// Token: 0x040022BE RID: 8894
		internal const uint CERT_STORE_PROV_MEMORY = 2U;

		// Token: 0x040022BF RID: 8895
		internal const uint CERT_STORE_PROV_FILE = 3U;

		// Token: 0x040022C0 RID: 8896
		internal const uint CERT_STORE_PROV_REG = 4U;

		// Token: 0x040022C1 RID: 8897
		internal const uint CERT_STORE_PROV_PKCS7 = 5U;

		// Token: 0x040022C2 RID: 8898
		internal const uint CERT_STORE_PROV_SERIALIZED = 6U;

		// Token: 0x040022C3 RID: 8899
		internal const uint CERT_STORE_PROV_FILENAME_A = 7U;

		// Token: 0x040022C4 RID: 8900
		internal const uint CERT_STORE_PROV_FILENAME_W = 8U;

		// Token: 0x040022C5 RID: 8901
		internal const uint CERT_STORE_PROV_FILENAME = 8U;

		// Token: 0x040022C6 RID: 8902
		internal const uint CERT_STORE_PROV_SYSTEM_A = 9U;

		// Token: 0x040022C7 RID: 8903
		internal const uint CERT_STORE_PROV_SYSTEM_W = 10U;

		// Token: 0x040022C8 RID: 8904
		internal const uint CERT_STORE_PROV_SYSTEM = 10U;

		// Token: 0x040022C9 RID: 8905
		internal const uint CERT_STORE_PROV_COLLECTION = 11U;

		// Token: 0x040022CA RID: 8906
		internal const uint CERT_STORE_PROV_SYSTEM_REGISTRY_A = 12U;

		// Token: 0x040022CB RID: 8907
		internal const uint CERT_STORE_PROV_SYSTEM_REGISTRY_W = 13U;

		// Token: 0x040022CC RID: 8908
		internal const uint CERT_STORE_PROV_SYSTEM_REGISTRY = 13U;

		// Token: 0x040022CD RID: 8909
		internal const uint CERT_STORE_PROV_PHYSICAL_W = 14U;

		// Token: 0x040022CE RID: 8910
		internal const uint CERT_STORE_PROV_PHYSICAL = 14U;

		// Token: 0x040022CF RID: 8911
		internal const uint CERT_STORE_PROV_SMART_CARD_W = 15U;

		// Token: 0x040022D0 RID: 8912
		internal const uint CERT_STORE_PROV_SMART_CARD = 15U;

		// Token: 0x040022D1 RID: 8913
		internal const uint CERT_STORE_PROV_LDAP_W = 16U;

		// Token: 0x040022D2 RID: 8914
		internal const uint CERT_STORE_PROV_LDAP = 16U;

		// Token: 0x040022D3 RID: 8915
		internal const uint CERT_STORE_NO_CRYPT_RELEASE_FLAG = 1U;

		// Token: 0x040022D4 RID: 8916
		internal const uint CERT_STORE_SET_LOCALIZED_NAME_FLAG = 2U;

		// Token: 0x040022D5 RID: 8917
		internal const uint CERT_STORE_DEFER_CLOSE_UNTIL_LAST_FREE_FLAG = 4U;

		// Token: 0x040022D6 RID: 8918
		internal const uint CERT_STORE_DELETE_FLAG = 16U;

		// Token: 0x040022D7 RID: 8919
		internal const uint CERT_STORE_SHARE_STORE_FLAG = 64U;

		// Token: 0x040022D8 RID: 8920
		internal const uint CERT_STORE_SHARE_CONTEXT_FLAG = 128U;

		// Token: 0x040022D9 RID: 8921
		internal const uint CERT_STORE_MANIFOLD_FLAG = 256U;

		// Token: 0x040022DA RID: 8922
		internal const uint CERT_STORE_ENUM_ARCHIVED_FLAG = 512U;

		// Token: 0x040022DB RID: 8923
		internal const uint CERT_STORE_UPDATE_KEYID_FLAG = 1024U;

		// Token: 0x040022DC RID: 8924
		internal const uint CERT_STORE_BACKUP_RESTORE_FLAG = 2048U;

		// Token: 0x040022DD RID: 8925
		internal const uint CERT_STORE_READONLY_FLAG = 32768U;

		// Token: 0x040022DE RID: 8926
		internal const uint CERT_STORE_OPEN_EXISTING_FLAG = 16384U;

		// Token: 0x040022DF RID: 8927
		internal const uint CERT_STORE_CREATE_NEW_FLAG = 8192U;

		// Token: 0x040022E0 RID: 8928
		internal const uint CERT_STORE_MAXIMUM_ALLOWED_FLAG = 4096U;

		// Token: 0x040022E1 RID: 8929
		internal const uint CERT_SYSTEM_STORE_UNPROTECTED_FLAG = 1073741824U;

		// Token: 0x040022E2 RID: 8930
		internal const uint CERT_SYSTEM_STORE_LOCATION_MASK = 16711680U;

		// Token: 0x040022E3 RID: 8931
		internal const uint CERT_SYSTEM_STORE_LOCATION_SHIFT = 16U;

		// Token: 0x040022E4 RID: 8932
		internal const uint CERT_SYSTEM_STORE_CURRENT_USER_ID = 1U;

		// Token: 0x040022E5 RID: 8933
		internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_ID = 2U;

		// Token: 0x040022E6 RID: 8934
		internal const uint CERT_SYSTEM_STORE_CURRENT_SERVICE_ID = 4U;

		// Token: 0x040022E7 RID: 8935
		internal const uint CERT_SYSTEM_STORE_SERVICES_ID = 5U;

		// Token: 0x040022E8 RID: 8936
		internal const uint CERT_SYSTEM_STORE_USERS_ID = 6U;

		// Token: 0x040022E9 RID: 8937
		internal const uint CERT_SYSTEM_STORE_CURRENT_USER_GROUP_POLICY_ID = 7U;

		// Token: 0x040022EA RID: 8938
		internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_GROUP_POLICY_ID = 8U;

		// Token: 0x040022EB RID: 8939
		internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_ENTERPRISE_ID = 9U;

		// Token: 0x040022EC RID: 8940
		internal const uint CERT_SYSTEM_STORE_CURRENT_USER = 65536U;

		// Token: 0x040022ED RID: 8941
		internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE = 131072U;

		// Token: 0x040022EE RID: 8942
		internal const uint CERT_SYSTEM_STORE_CURRENT_SERVICE = 262144U;

		// Token: 0x040022EF RID: 8943
		internal const uint CERT_SYSTEM_STORE_SERVICES = 327680U;

		// Token: 0x040022F0 RID: 8944
		internal const uint CERT_SYSTEM_STORE_USERS = 393216U;

		// Token: 0x040022F1 RID: 8945
		internal const uint CERT_SYSTEM_STORE_CURRENT_USER_GROUP_POLICY = 458752U;

		// Token: 0x040022F2 RID: 8946
		internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_GROUP_POLICY = 524288U;

		// Token: 0x040022F3 RID: 8947
		internal const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_ENTERPRISE = 589824U;

		// Token: 0x040022F4 RID: 8948
		internal const uint CERT_NAME_EMAIL_TYPE = 1U;

		// Token: 0x040022F5 RID: 8949
		internal const uint CERT_NAME_RDN_TYPE = 2U;

		// Token: 0x040022F6 RID: 8950
		internal const uint CERT_NAME_ATTR_TYPE = 3U;

		// Token: 0x040022F7 RID: 8951
		internal const uint CERT_NAME_SIMPLE_DISPLAY_TYPE = 4U;

		// Token: 0x040022F8 RID: 8952
		internal const uint CERT_NAME_FRIENDLY_DISPLAY_TYPE = 5U;

		// Token: 0x040022F9 RID: 8953
		internal const uint CERT_NAME_DNS_TYPE = 6U;

		// Token: 0x040022FA RID: 8954
		internal const uint CERT_NAME_URL_TYPE = 7U;

		// Token: 0x040022FB RID: 8955
		internal const uint CERT_NAME_UPN_TYPE = 8U;

		// Token: 0x040022FC RID: 8956
		internal const uint CERT_SIMPLE_NAME_STR = 1U;

		// Token: 0x040022FD RID: 8957
		internal const uint CERT_OID_NAME_STR = 2U;

		// Token: 0x040022FE RID: 8958
		internal const uint CERT_X500_NAME_STR = 3U;

		// Token: 0x040022FF RID: 8959
		internal const uint CERT_NAME_STR_SEMICOLON_FLAG = 1073741824U;

		// Token: 0x04002300 RID: 8960
		internal const uint CERT_NAME_STR_NO_PLUS_FLAG = 536870912U;

		// Token: 0x04002301 RID: 8961
		internal const uint CERT_NAME_STR_NO_QUOTING_FLAG = 268435456U;

		// Token: 0x04002302 RID: 8962
		internal const uint CERT_NAME_STR_CRLF_FLAG = 134217728U;

		// Token: 0x04002303 RID: 8963
		internal const uint CERT_NAME_STR_COMMA_FLAG = 67108864U;

		// Token: 0x04002304 RID: 8964
		internal const uint CERT_NAME_STR_REVERSE_FLAG = 33554432U;

		// Token: 0x04002305 RID: 8965
		internal const uint CERT_NAME_ISSUER_FLAG = 1U;

		// Token: 0x04002306 RID: 8966
		internal const uint CERT_NAME_STR_DISABLE_IE4_UTF8_FLAG = 65536U;

		// Token: 0x04002307 RID: 8967
		internal const uint CERT_NAME_STR_ENABLE_T61_UNICODE_FLAG = 131072U;

		// Token: 0x04002308 RID: 8968
		internal const uint CERT_NAME_STR_ENABLE_UTF8_UNICODE_FLAG = 262144U;

		// Token: 0x04002309 RID: 8969
		internal const uint CERT_NAME_STR_FORCE_UTF8_DIR_STR_FLAG = 524288U;

		// Token: 0x0400230A RID: 8970
		internal const uint CERT_KEY_PROV_HANDLE_PROP_ID = 1U;

		// Token: 0x0400230B RID: 8971
		internal const uint CERT_KEY_PROV_INFO_PROP_ID = 2U;

		// Token: 0x0400230C RID: 8972
		internal const uint CERT_SHA1_HASH_PROP_ID = 3U;

		// Token: 0x0400230D RID: 8973
		internal const uint CERT_MD5_HASH_PROP_ID = 4U;

		// Token: 0x0400230E RID: 8974
		internal const uint CERT_HASH_PROP_ID = 3U;

		// Token: 0x0400230F RID: 8975
		internal const uint CERT_KEY_CONTEXT_PROP_ID = 5U;

		// Token: 0x04002310 RID: 8976
		internal const uint CERT_KEY_SPEC_PROP_ID = 6U;

		// Token: 0x04002311 RID: 8977
		internal const uint CERT_IE30_RESERVED_PROP_ID = 7U;

		// Token: 0x04002312 RID: 8978
		internal const uint CERT_PUBKEY_HASH_RESERVED_PROP_ID = 8U;

		// Token: 0x04002313 RID: 8979
		internal const uint CERT_ENHKEY_USAGE_PROP_ID = 9U;

		// Token: 0x04002314 RID: 8980
		internal const uint CERT_CTL_USAGE_PROP_ID = 9U;

		// Token: 0x04002315 RID: 8981
		internal const uint CERT_NEXT_UPDATE_LOCATION_PROP_ID = 10U;

		// Token: 0x04002316 RID: 8982
		internal const uint CERT_FRIENDLY_NAME_PROP_ID = 11U;

		// Token: 0x04002317 RID: 8983
		internal const uint CERT_PVK_FILE_PROP_ID = 12U;

		// Token: 0x04002318 RID: 8984
		internal const uint CERT_DESCRIPTION_PROP_ID = 13U;

		// Token: 0x04002319 RID: 8985
		internal const uint CERT_ACCESS_STATE_PROP_ID = 14U;

		// Token: 0x0400231A RID: 8986
		internal const uint CERT_SIGNATURE_HASH_PROP_ID = 15U;

		// Token: 0x0400231B RID: 8987
		internal const uint CERT_SMART_CARD_DATA_PROP_ID = 16U;

		// Token: 0x0400231C RID: 8988
		internal const uint CERT_EFS_PROP_ID = 17U;

		// Token: 0x0400231D RID: 8989
		internal const uint CERT_FORTEZZA_DATA_PROP_ID = 18U;

		// Token: 0x0400231E RID: 8990
		internal const uint CERT_ARCHIVED_PROP_ID = 19U;

		// Token: 0x0400231F RID: 8991
		internal const uint CERT_KEY_IDENTIFIER_PROP_ID = 20U;

		// Token: 0x04002320 RID: 8992
		internal const uint CERT_AUTO_ENROLL_PROP_ID = 21U;

		// Token: 0x04002321 RID: 8993
		internal const uint CERT_PUBKEY_ALG_PARA_PROP_ID = 22U;

		// Token: 0x04002322 RID: 8994
		internal const uint CERT_CROSS_CERT_DIST_POINTS_PROP_ID = 23U;

		// Token: 0x04002323 RID: 8995
		internal const uint CERT_ISSUER_PUBLIC_KEY_MD5_HASH_PROP_ID = 24U;

		// Token: 0x04002324 RID: 8996
		internal const uint CERT_SUBJECT_PUBLIC_KEY_MD5_HASH_PROP_ID = 25U;

		// Token: 0x04002325 RID: 8997
		internal const uint CERT_ENROLLMENT_PROP_ID = 26U;

		// Token: 0x04002326 RID: 8998
		internal const uint CERT_DATE_STAMP_PROP_ID = 27U;

		// Token: 0x04002327 RID: 8999
		internal const uint CERT_ISSUER_SERIAL_NUMBER_MD5_HASH_PROP_ID = 28U;

		// Token: 0x04002328 RID: 9000
		internal const uint CERT_SUBJECT_NAME_MD5_HASH_PROP_ID = 29U;

		// Token: 0x04002329 RID: 9001
		internal const uint CERT_EXTENDED_ERROR_INFO_PROP_ID = 30U;

		// Token: 0x0400232A RID: 9002
		internal const uint CERT_RENEWAL_PROP_ID = 64U;

		// Token: 0x0400232B RID: 9003
		internal const uint CERT_ARCHIVED_KEY_HASH_PROP_ID = 65U;

		// Token: 0x0400232C RID: 9004
		internal const uint CERT_FIRST_RESERVED_PROP_ID = 66U;

		// Token: 0x0400232D RID: 9005
		internal const uint CERT_NCRYPT_KEY_HANDLE_PROP_ID = 78U;

		// Token: 0x0400232E RID: 9006
		internal const uint CERT_DELETE_KEYSET_PROP_ID = 125U;

		// Token: 0x0400232F RID: 9007
		internal const uint CERT_SET_PROPERTY_IGNORE_PERSIST_ERROR_FLAG = 2147483648U;

		// Token: 0x04002330 RID: 9008
		internal const uint CERT_SET_PROPERTY_INHIBIT_PERSIST_FLAG = 1073741824U;

		// Token: 0x04002331 RID: 9009
		internal const uint CERT_INFO_VERSION_FLAG = 1U;

		// Token: 0x04002332 RID: 9010
		internal const uint CERT_INFO_SERIAL_NUMBER_FLAG = 2U;

		// Token: 0x04002333 RID: 9011
		internal const uint CERT_INFO_SIGNATURE_ALGORITHM_FLAG = 3U;

		// Token: 0x04002334 RID: 9012
		internal const uint CERT_INFO_ISSUER_FLAG = 4U;

		// Token: 0x04002335 RID: 9013
		internal const uint CERT_INFO_NOT_BEFORE_FLAG = 5U;

		// Token: 0x04002336 RID: 9014
		internal const uint CERT_INFO_NOT_AFTER_FLAG = 6U;

		// Token: 0x04002337 RID: 9015
		internal const uint CERT_INFO_SUBJECT_FLAG = 7U;

		// Token: 0x04002338 RID: 9016
		internal const uint CERT_INFO_SUBJECT_PUBLIC_KEY_INFO_FLAG = 8U;

		// Token: 0x04002339 RID: 9017
		internal const uint CERT_INFO_ISSUER_UNIQUE_ID_FLAG = 9U;

		// Token: 0x0400233A RID: 9018
		internal const uint CERT_INFO_SUBJECT_UNIQUE_ID_FLAG = 10U;

		// Token: 0x0400233B RID: 9019
		internal const uint CERT_INFO_EXTENSION_FLAG = 11U;

		// Token: 0x0400233C RID: 9020
		internal const uint CERT_COMPARE_MASK = 65535U;

		// Token: 0x0400233D RID: 9021
		internal const uint CERT_COMPARE_SHIFT = 16U;

		// Token: 0x0400233E RID: 9022
		internal const uint CERT_COMPARE_ANY = 0U;

		// Token: 0x0400233F RID: 9023
		internal const uint CERT_COMPARE_SHA1_HASH = 1U;

		// Token: 0x04002340 RID: 9024
		internal const uint CERT_COMPARE_NAME = 2U;

		// Token: 0x04002341 RID: 9025
		internal const uint CERT_COMPARE_ATTR = 3U;

		// Token: 0x04002342 RID: 9026
		internal const uint CERT_COMPARE_MD5_HASH = 4U;

		// Token: 0x04002343 RID: 9027
		internal const uint CERT_COMPARE_PROPERTY = 5U;

		// Token: 0x04002344 RID: 9028
		internal const uint CERT_COMPARE_PUBLIC_KEY = 6U;

		// Token: 0x04002345 RID: 9029
		internal const uint CERT_COMPARE_HASH = 1U;

		// Token: 0x04002346 RID: 9030
		internal const uint CERT_COMPARE_NAME_STR_A = 7U;

		// Token: 0x04002347 RID: 9031
		internal const uint CERT_COMPARE_NAME_STR_W = 8U;

		// Token: 0x04002348 RID: 9032
		internal const uint CERT_COMPARE_KEY_SPEC = 9U;

		// Token: 0x04002349 RID: 9033
		internal const uint CERT_COMPARE_ENHKEY_USAGE = 10U;

		// Token: 0x0400234A RID: 9034
		internal const uint CERT_COMPARE_CTL_USAGE = 10U;

		// Token: 0x0400234B RID: 9035
		internal const uint CERT_COMPARE_SUBJECT_CERT = 11U;

		// Token: 0x0400234C RID: 9036
		internal const uint CERT_COMPARE_ISSUER_OF = 12U;

		// Token: 0x0400234D RID: 9037
		internal const uint CERT_COMPARE_EXISTING = 13U;

		// Token: 0x0400234E RID: 9038
		internal const uint CERT_COMPARE_SIGNATURE_HASH = 14U;

		// Token: 0x0400234F RID: 9039
		internal const uint CERT_COMPARE_KEY_IDENTIFIER = 15U;

		// Token: 0x04002350 RID: 9040
		internal const uint CERT_COMPARE_CERT_ID = 16U;

		// Token: 0x04002351 RID: 9041
		internal const uint CERT_COMPARE_CROSS_CERT_DIST_POINTS = 17U;

		// Token: 0x04002352 RID: 9042
		internal const uint CERT_COMPARE_PUBKEY_MD5_HASH = 18U;

		// Token: 0x04002353 RID: 9043
		internal const uint CERT_FIND_ANY = 0U;

		// Token: 0x04002354 RID: 9044
		internal const uint CERT_FIND_SHA1_HASH = 65536U;

		// Token: 0x04002355 RID: 9045
		internal const uint CERT_FIND_MD5_HASH = 262144U;

		// Token: 0x04002356 RID: 9046
		internal const uint CERT_FIND_SIGNATURE_HASH = 917504U;

		// Token: 0x04002357 RID: 9047
		internal const uint CERT_FIND_KEY_IDENTIFIER = 983040U;

		// Token: 0x04002358 RID: 9048
		internal const uint CERT_FIND_HASH = 65536U;

		// Token: 0x04002359 RID: 9049
		internal const uint CERT_FIND_PROPERTY = 327680U;

		// Token: 0x0400235A RID: 9050
		internal const uint CERT_FIND_PUBLIC_KEY = 393216U;

		// Token: 0x0400235B RID: 9051
		internal const uint CERT_FIND_SUBJECT_NAME = 131079U;

		// Token: 0x0400235C RID: 9052
		internal const uint CERT_FIND_SUBJECT_ATTR = 196615U;

		// Token: 0x0400235D RID: 9053
		internal const uint CERT_FIND_ISSUER_NAME = 131076U;

		// Token: 0x0400235E RID: 9054
		internal const uint CERT_FIND_ISSUER_ATTR = 196612U;

		// Token: 0x0400235F RID: 9055
		internal const uint CERT_FIND_SUBJECT_STR_A = 458759U;

		// Token: 0x04002360 RID: 9056
		internal const uint CERT_FIND_SUBJECT_STR_W = 524295U;

		// Token: 0x04002361 RID: 9057
		internal const uint CERT_FIND_SUBJECT_STR = 524295U;

		// Token: 0x04002362 RID: 9058
		internal const uint CERT_FIND_ISSUER_STR_A = 458756U;

		// Token: 0x04002363 RID: 9059
		internal const uint CERT_FIND_ISSUER_STR_W = 524292U;

		// Token: 0x04002364 RID: 9060
		internal const uint CERT_FIND_ISSUER_STR = 524292U;

		// Token: 0x04002365 RID: 9061
		internal const uint CERT_FIND_KEY_SPEC = 589824U;

		// Token: 0x04002366 RID: 9062
		internal const uint CERT_FIND_ENHKEY_USAGE = 655360U;

		// Token: 0x04002367 RID: 9063
		internal const uint CERT_FIND_CTL_USAGE = 655360U;

		// Token: 0x04002368 RID: 9064
		internal const uint CERT_FIND_SUBJECT_CERT = 720896U;

		// Token: 0x04002369 RID: 9065
		internal const uint CERT_FIND_ISSUER_OF = 786432U;

		// Token: 0x0400236A RID: 9066
		internal const uint CERT_FIND_EXISTING = 851968U;

		// Token: 0x0400236B RID: 9067
		internal const uint CERT_FIND_CERT_ID = 1048576U;

		// Token: 0x0400236C RID: 9068
		internal const uint CERT_FIND_CROSS_CERT_DIST_POINTS = 1114112U;

		// Token: 0x0400236D RID: 9069
		internal const uint CERT_FIND_PUBKEY_MD5_HASH = 1179648U;

		// Token: 0x0400236E RID: 9070
		internal const uint CERT_ENCIPHER_ONLY_KEY_USAGE = 1U;

		// Token: 0x0400236F RID: 9071
		internal const uint CERT_CRL_SIGN_KEY_USAGE = 2U;

		// Token: 0x04002370 RID: 9072
		internal const uint CERT_KEY_CERT_SIGN_KEY_USAGE = 4U;

		// Token: 0x04002371 RID: 9073
		internal const uint CERT_KEY_AGREEMENT_KEY_USAGE = 8U;

		// Token: 0x04002372 RID: 9074
		internal const uint CERT_DATA_ENCIPHERMENT_KEY_USAGE = 16U;

		// Token: 0x04002373 RID: 9075
		internal const uint CERT_KEY_ENCIPHERMENT_KEY_USAGE = 32U;

		// Token: 0x04002374 RID: 9076
		internal const uint CERT_NON_REPUDIATION_KEY_USAGE = 64U;

		// Token: 0x04002375 RID: 9077
		internal const uint CERT_DIGITAL_SIGNATURE_KEY_USAGE = 128U;

		// Token: 0x04002376 RID: 9078
		internal const uint CERT_DECIPHER_ONLY_KEY_USAGE = 32768U;

		// Token: 0x04002377 RID: 9079
		internal const uint CERT_STORE_ADD_NEW = 1U;

		// Token: 0x04002378 RID: 9080
		internal const uint CERT_STORE_ADD_USE_EXISTING = 2U;

		// Token: 0x04002379 RID: 9081
		internal const uint CERT_STORE_ADD_REPLACE_EXISTING = 3U;

		// Token: 0x0400237A RID: 9082
		internal const uint CERT_STORE_ADD_ALWAYS = 4U;

		// Token: 0x0400237B RID: 9083
		internal const uint CERT_STORE_ADD_REPLACE_EXISTING_INHERIT_PROPERTIES = 5U;

		// Token: 0x0400237C RID: 9084
		internal const uint CERT_STORE_ADD_NEWER = 6U;

		// Token: 0x0400237D RID: 9085
		internal const uint CERT_STORE_ADD_NEWER_INHERIT_PROPERTIES = 7U;

		// Token: 0x0400237E RID: 9086
		internal const uint CRYPT_FORMAT_STR_MULTI_LINE = 1U;

		// Token: 0x0400237F RID: 9087
		internal const uint CRYPT_FORMAT_STR_NO_HEX = 16U;

		// Token: 0x04002380 RID: 9088
		internal const uint CERT_STORE_SAVE_AS_STORE = 1U;

		// Token: 0x04002381 RID: 9089
		internal const uint CERT_STORE_SAVE_AS_PKCS7 = 2U;

		// Token: 0x04002382 RID: 9090
		internal const uint CERT_STORE_SAVE_TO_FILE = 1U;

		// Token: 0x04002383 RID: 9091
		internal const uint CERT_STORE_SAVE_TO_MEMORY = 2U;

		// Token: 0x04002384 RID: 9092
		internal const uint CERT_STORE_SAVE_TO_FILENAME_A = 3U;

		// Token: 0x04002385 RID: 9093
		internal const uint CERT_STORE_SAVE_TO_FILENAME_W = 4U;

		// Token: 0x04002386 RID: 9094
		internal const uint CERT_STORE_SAVE_TO_FILENAME = 4U;

		// Token: 0x04002387 RID: 9095
		internal const uint CERT_CA_SUBJECT_FLAG = 128U;

		// Token: 0x04002388 RID: 9096
		internal const uint CERT_END_ENTITY_SUBJECT_FLAG = 64U;

		// Token: 0x04002389 RID: 9097
		internal const uint REPORT_NO_PRIVATE_KEY = 1U;

		// Token: 0x0400238A RID: 9098
		internal const uint REPORT_NOT_ABLE_TO_EXPORT_PRIVATE_KEY = 2U;

		// Token: 0x0400238B RID: 9099
		internal const uint EXPORT_PRIVATE_KEYS = 4U;

		// Token: 0x0400238C RID: 9100
		internal const uint PKCS12_EXPORT_RESERVED_MASK = 4294901760U;

		// Token: 0x0400238D RID: 9101
		internal const uint RSA_CSP_PUBLICKEYBLOB = 19U;

		// Token: 0x0400238E RID: 9102
		internal const uint X509_MULTI_BYTE_UINT = 38U;

		// Token: 0x0400238F RID: 9103
		internal const uint X509_DSS_PUBLICKEY = 38U;

		// Token: 0x04002390 RID: 9104
		internal const uint X509_DSS_PARAMETERS = 39U;

		// Token: 0x04002391 RID: 9105
		internal const uint X509_DSS_SIGNATURE = 40U;

		// Token: 0x04002392 RID: 9106
		internal const uint X509_EXTENSIONS = 5U;

		// Token: 0x04002393 RID: 9107
		internal const uint X509_NAME_VALUE = 6U;

		// Token: 0x04002394 RID: 9108
		internal const uint X509_NAME = 7U;

		// Token: 0x04002395 RID: 9109
		internal const uint X509_AUTHORITY_KEY_ID = 9U;

		// Token: 0x04002396 RID: 9110
		internal const uint X509_KEY_USAGE_RESTRICTION = 11U;

		// Token: 0x04002397 RID: 9111
		internal const uint X509_BASIC_CONSTRAINTS = 13U;

		// Token: 0x04002398 RID: 9112
		internal const uint X509_KEY_USAGE = 14U;

		// Token: 0x04002399 RID: 9113
		internal const uint X509_BASIC_CONSTRAINTS2 = 15U;

		// Token: 0x0400239A RID: 9114
		internal const uint X509_CERT_POLICIES = 16U;

		// Token: 0x0400239B RID: 9115
		internal const uint PKCS_UTC_TIME = 17U;

		// Token: 0x0400239C RID: 9116
		internal const uint PKCS_ATTRIBUTE = 22U;

		// Token: 0x0400239D RID: 9117
		internal const uint X509_UNICODE_NAME_VALUE = 24U;

		// Token: 0x0400239E RID: 9118
		internal const uint X509_OCTET_STRING = 25U;

		// Token: 0x0400239F RID: 9119
		internal const uint X509_BITS = 26U;

		// Token: 0x040023A0 RID: 9120
		internal const uint X509_ANY_STRING = 6U;

		// Token: 0x040023A1 RID: 9121
		internal const uint X509_UNICODE_ANY_STRING = 24U;

		// Token: 0x040023A2 RID: 9122
		internal const uint X509_ENHANCED_KEY_USAGE = 36U;

		// Token: 0x040023A3 RID: 9123
		internal const uint PKCS_RC2_CBC_PARAMETERS = 41U;

		// Token: 0x040023A4 RID: 9124
		internal const uint X509_CERTIFICATE_TEMPLATE = 64U;

		// Token: 0x040023A5 RID: 9125
		internal const uint PKCS7_SIGNER_INFO = 500U;

		// Token: 0x040023A6 RID: 9126
		internal const uint CMS_SIGNER_INFO = 501U;

		// Token: 0x040023A7 RID: 9127
		internal const string szOID_COMMON_NAME = "2.5.4.3";

		// Token: 0x040023A8 RID: 9128
		internal const string szOID_AUTHORITY_KEY_IDENTIFIER = "2.5.29.1";

		// Token: 0x040023A9 RID: 9129
		internal const string szOID_KEY_USAGE_RESTRICTION = "2.5.29.4";

		// Token: 0x040023AA RID: 9130
		internal const string szOID_SUBJECT_ALT_NAME = "2.5.29.7";

		// Token: 0x040023AB RID: 9131
		internal const string szOID_ISSUER_ALT_NAME = "2.5.29.8";

		// Token: 0x040023AC RID: 9132
		internal const string szOID_BASIC_CONSTRAINTS = "2.5.29.10";

		// Token: 0x040023AD RID: 9133
		internal const string szOID_SUBJECT_KEY_IDENTIFIER = "2.5.29.14";

		// Token: 0x040023AE RID: 9134
		internal const string szOID_KEY_USAGE = "2.5.29.15";

		// Token: 0x040023AF RID: 9135
		internal const string szOID_SUBJECT_ALT_NAME2 = "2.5.29.17";

		// Token: 0x040023B0 RID: 9136
		internal const string szOID_ISSUER_ALT_NAME2 = "2.5.29.18";

		// Token: 0x040023B1 RID: 9137
		internal const string szOID_BASIC_CONSTRAINTS2 = "2.5.29.19";

		// Token: 0x040023B2 RID: 9138
		internal const string szOID_CRL_DIST_POINTS = "2.5.29.31";

		// Token: 0x040023B3 RID: 9139
		internal const string szOID_CERT_POLICIES = "2.5.29.32";

		// Token: 0x040023B4 RID: 9140
		internal const string szOID_ENHANCED_KEY_USAGE = "2.5.29.37";

		// Token: 0x040023B5 RID: 9141
		internal const string szOID_KEYID_RDN = "1.3.6.1.4.1.311.10.7.1";

		// Token: 0x040023B6 RID: 9142
		internal const string szOID_ENROLL_CERTTYPE_EXTENSION = "1.3.6.1.4.1.311.20.2";

		// Token: 0x040023B7 RID: 9143
		internal const string szOID_NT_PRINCIPAL_NAME = "1.3.6.1.4.1.311.20.2.3";

		// Token: 0x040023B8 RID: 9144
		internal const string szOID_CERTIFICATE_TEMPLATE = "1.3.6.1.4.1.311.21.7";

		// Token: 0x040023B9 RID: 9145
		internal const string szOID_RDN_DUMMY_SIGNER = "1.3.6.1.4.1.311.21.9";

		// Token: 0x040023BA RID: 9146
		internal const string szOID_AUTHORITY_INFO_ACCESS = "1.3.6.1.5.5.7.1.1";

		// Token: 0x040023BB RID: 9147
		internal const uint CERT_CHAIN_POLICY_BASE = 1U;

		// Token: 0x040023BC RID: 9148
		internal const uint CERT_CHAIN_POLICY_AUTHENTICODE = 2U;

		// Token: 0x040023BD RID: 9149
		internal const uint CERT_CHAIN_POLICY_AUTHENTICODE_TS = 3U;

		// Token: 0x040023BE RID: 9150
		internal const uint CERT_CHAIN_POLICY_SSL = 4U;

		// Token: 0x040023BF RID: 9151
		internal const uint CERT_CHAIN_POLICY_BASIC_CONSTRAINTS = 5U;

		// Token: 0x040023C0 RID: 9152
		internal const uint CERT_CHAIN_POLICY_NT_AUTH = 6U;

		// Token: 0x040023C1 RID: 9153
		internal const uint CERT_CHAIN_POLICY_MICROSOFT_ROOT = 7U;

		// Token: 0x040023C2 RID: 9154
		internal const uint USAGE_MATCH_TYPE_AND = 0U;

		// Token: 0x040023C3 RID: 9155
		internal const uint USAGE_MATCH_TYPE_OR = 1U;

		// Token: 0x040023C4 RID: 9156
		internal const uint CERT_CHAIN_REVOCATION_CHECK_END_CERT = 268435456U;

		// Token: 0x040023C5 RID: 9157
		internal const uint CERT_CHAIN_REVOCATION_CHECK_CHAIN = 536870912U;

		// Token: 0x040023C6 RID: 9158
		internal const uint CERT_CHAIN_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT = 1073741824U;

		// Token: 0x040023C7 RID: 9159
		internal const uint CERT_CHAIN_REVOCATION_CHECK_CACHE_ONLY = 2147483648U;

		// Token: 0x040023C8 RID: 9160
		internal const uint CERT_CHAIN_REVOCATION_ACCUMULATIVE_TIMEOUT = 134217728U;

		// Token: 0x040023C9 RID: 9161
		internal const uint CERT_TRUST_NO_ERROR = 0U;

		// Token: 0x040023CA RID: 9162
		internal const uint CERT_TRUST_IS_NOT_TIME_VALID = 1U;

		// Token: 0x040023CB RID: 9163
		internal const uint CERT_TRUST_IS_NOT_TIME_NESTED = 2U;

		// Token: 0x040023CC RID: 9164
		internal const uint CERT_TRUST_IS_REVOKED = 4U;

		// Token: 0x040023CD RID: 9165
		internal const uint CERT_TRUST_IS_NOT_SIGNATURE_VALID = 8U;

		// Token: 0x040023CE RID: 9166
		internal const uint CERT_TRUST_IS_NOT_VALID_FOR_USAGE = 16U;

		// Token: 0x040023CF RID: 9167
		internal const uint CERT_TRUST_IS_UNTRUSTED_ROOT = 32U;

		// Token: 0x040023D0 RID: 9168
		internal const uint CERT_TRUST_REVOCATION_STATUS_UNKNOWN = 64U;

		// Token: 0x040023D1 RID: 9169
		internal const uint CERT_TRUST_IS_CYCLIC = 128U;

		// Token: 0x040023D2 RID: 9170
		internal const uint CERT_TRUST_INVALID_EXTENSION = 256U;

		// Token: 0x040023D3 RID: 9171
		internal const uint CERT_TRUST_INVALID_POLICY_CONSTRAINTS = 512U;

		// Token: 0x040023D4 RID: 9172
		internal const uint CERT_TRUST_INVALID_BASIC_CONSTRAINTS = 1024U;

		// Token: 0x040023D5 RID: 9173
		internal const uint CERT_TRUST_INVALID_NAME_CONSTRAINTS = 2048U;

		// Token: 0x040023D6 RID: 9174
		internal const uint CERT_TRUST_HAS_NOT_SUPPORTED_NAME_CONSTRAINT = 4096U;

		// Token: 0x040023D7 RID: 9175
		internal const uint CERT_TRUST_HAS_NOT_DEFINED_NAME_CONSTRAINT = 8192U;

		// Token: 0x040023D8 RID: 9176
		internal const uint CERT_TRUST_HAS_NOT_PERMITTED_NAME_CONSTRAINT = 16384U;

		// Token: 0x040023D9 RID: 9177
		internal const uint CERT_TRUST_HAS_EXCLUDED_NAME_CONSTRAINT = 32768U;

		// Token: 0x040023DA RID: 9178
		internal const uint CERT_TRUST_IS_OFFLINE_REVOCATION = 16777216U;

		// Token: 0x040023DB RID: 9179
		internal const uint CERT_TRUST_NO_ISSUANCE_CHAIN_POLICY = 33554432U;

		// Token: 0x040023DC RID: 9180
		internal const uint CERT_TRUST_IS_EXPLICIT_DISTRUST = 67108864U;

		// Token: 0x040023DD RID: 9181
		internal const uint CERT_TRUST_HAS_NOT_SUPPORTED_CRITICAL_EXT = 134217728U;

		// Token: 0x040023DE RID: 9182
		internal const uint CERT_TRUST_HAS_WEAK_SIGNATURE = 1048576U;

		// Token: 0x040023DF RID: 9183
		internal const uint CERT_TRUST_IS_PARTIAL_CHAIN = 65536U;

		// Token: 0x040023E0 RID: 9184
		internal const uint CERT_TRUST_CTL_IS_NOT_TIME_VALID = 131072U;

		// Token: 0x040023E1 RID: 9185
		internal const uint CERT_TRUST_CTL_IS_NOT_SIGNATURE_VALID = 262144U;

		// Token: 0x040023E2 RID: 9186
		internal const uint CERT_TRUST_CTL_IS_NOT_VALID_FOR_USAGE = 524288U;

		// Token: 0x040023E3 RID: 9187
		internal const uint CERT_CHAIN_POLICY_IGNORE_NOT_TIME_VALID_FLAG = 1U;

		// Token: 0x040023E4 RID: 9188
		internal const uint CERT_CHAIN_POLICY_IGNORE_CTL_NOT_TIME_VALID_FLAG = 2U;

		// Token: 0x040023E5 RID: 9189
		internal const uint CERT_CHAIN_POLICY_IGNORE_NOT_TIME_NESTED_FLAG = 4U;

		// Token: 0x040023E6 RID: 9190
		internal const uint CERT_CHAIN_POLICY_IGNORE_INVALID_BASIC_CONSTRAINTS_FLAG = 8U;

		// Token: 0x040023E7 RID: 9191
		internal const uint CERT_CHAIN_POLICY_ALLOW_UNKNOWN_CA_FLAG = 16U;

		// Token: 0x040023E8 RID: 9192
		internal const uint CERT_CHAIN_POLICY_IGNORE_WRONG_USAGE_FLAG = 32U;

		// Token: 0x040023E9 RID: 9193
		internal const uint CERT_CHAIN_POLICY_IGNORE_INVALID_NAME_FLAG = 64U;

		// Token: 0x040023EA RID: 9194
		internal const uint CERT_CHAIN_POLICY_IGNORE_INVALID_POLICY_FLAG = 128U;

		// Token: 0x040023EB RID: 9195
		internal const uint CERT_CHAIN_POLICY_IGNORE_END_REV_UNKNOWN_FLAG = 256U;

		// Token: 0x040023EC RID: 9196
		internal const uint CERT_CHAIN_POLICY_IGNORE_CTL_SIGNER_REV_UNKNOWN_FLAG = 512U;

		// Token: 0x040023ED RID: 9197
		internal const uint CERT_CHAIN_POLICY_IGNORE_CA_REV_UNKNOWN_FLAG = 1024U;

		// Token: 0x040023EE RID: 9198
		internal const uint CERT_CHAIN_POLICY_IGNORE_ROOT_REV_UNKNOWN_FLAG = 2048U;

		// Token: 0x040023EF RID: 9199
		internal const uint CERT_CHAIN_POLICY_IGNORE_ALL_REV_UNKNOWN_FLAGS = 3840U;

		// Token: 0x040023F0 RID: 9200
		internal const uint CERT_TRUST_HAS_EXACT_MATCH_ISSUER = 1U;

		// Token: 0x040023F1 RID: 9201
		internal const uint CERT_TRUST_HAS_KEY_MATCH_ISSUER = 2U;

		// Token: 0x040023F2 RID: 9202
		internal const uint CERT_TRUST_HAS_NAME_MATCH_ISSUER = 4U;

		// Token: 0x040023F3 RID: 9203
		internal const uint CERT_TRUST_IS_SELF_SIGNED = 8U;

		// Token: 0x040023F4 RID: 9204
		internal const uint CERT_TRUST_HAS_PREFERRED_ISSUER = 256U;

		// Token: 0x040023F5 RID: 9205
		internal const uint CERT_TRUST_HAS_ISSUANCE_CHAIN_POLICY = 512U;

		// Token: 0x040023F6 RID: 9206
		internal const uint CERT_TRUST_HAS_VALID_NAME_CONSTRAINTS = 1024U;

		// Token: 0x040023F7 RID: 9207
		internal const uint CERT_TRUST_IS_COMPLEX_CHAIN = 65536U;

		// Token: 0x040023F8 RID: 9208
		internal const string szOID_PKIX_NO_SIGNATURE = "1.3.6.1.5.5.7.6.2";

		// Token: 0x040023F9 RID: 9209
		internal const string szOID_PKIX_KP_SERVER_AUTH = "1.3.6.1.5.5.7.3.1";

		// Token: 0x040023FA RID: 9210
		internal const string szOID_PKIX_KP_CLIENT_AUTH = "1.3.6.1.5.5.7.3.2";

		// Token: 0x040023FB RID: 9211
		internal const string szOID_PKIX_KP_CODE_SIGNING = "1.3.6.1.5.5.7.3.3";

		// Token: 0x040023FC RID: 9212
		internal const string szOID_PKIX_KP_EMAIL_PROTECTION = "1.3.6.1.5.5.7.3.4";

		// Token: 0x040023FD RID: 9213
		internal const string SPC_INDIVIDUAL_SP_KEY_PURPOSE_OBJID = "1.3.6.1.4.1.311.2.1.21";

		// Token: 0x040023FE RID: 9214
		internal const string SPC_COMMERCIAL_SP_KEY_PURPOSE_OBJID = "1.3.6.1.4.1.311.2.1.22";

		// Token: 0x040023FF RID: 9215
		internal const uint HCCE_CURRENT_USER = 0U;

		// Token: 0x04002400 RID: 9216
		internal const uint HCCE_LOCAL_MACHINE = 1U;

		// Token: 0x04002401 RID: 9217
		internal const string szOID_PKCS_1 = "1.2.840.113549.1.1";

		// Token: 0x04002402 RID: 9218
		internal const string szOID_PKCS_2 = "1.2.840.113549.1.2";

		// Token: 0x04002403 RID: 9219
		internal const string szOID_PKCS_3 = "1.2.840.113549.1.3";

		// Token: 0x04002404 RID: 9220
		internal const string szOID_PKCS_4 = "1.2.840.113549.1.4";

		// Token: 0x04002405 RID: 9221
		internal const string szOID_PKCS_5 = "1.2.840.113549.1.5";

		// Token: 0x04002406 RID: 9222
		internal const string szOID_PKCS_6 = "1.2.840.113549.1.6";

		// Token: 0x04002407 RID: 9223
		internal const string szOID_PKCS_7 = "1.2.840.113549.1.7";

		// Token: 0x04002408 RID: 9224
		internal const string szOID_PKCS_8 = "1.2.840.113549.1.8";

		// Token: 0x04002409 RID: 9225
		internal const string szOID_PKCS_9 = "1.2.840.113549.1.9";

		// Token: 0x0400240A RID: 9226
		internal const string szOID_PKCS_10 = "1.2.840.113549.1.10";

		// Token: 0x0400240B RID: 9227
		internal const string szOID_PKCS_12 = "1.2.840.113549.1.12";

		// Token: 0x0400240C RID: 9228
		internal const string szOID_RSA_data = "1.2.840.113549.1.7.1";

		// Token: 0x0400240D RID: 9229
		internal const string szOID_RSA_signedData = "1.2.840.113549.1.7.2";

		// Token: 0x0400240E RID: 9230
		internal const string szOID_RSA_envelopedData = "1.2.840.113549.1.7.3";

		// Token: 0x0400240F RID: 9231
		internal const string szOID_RSA_signEnvData = "1.2.840.113549.1.7.4";

		// Token: 0x04002410 RID: 9232
		internal const string szOID_RSA_digestedData = "1.2.840.113549.1.7.5";

		// Token: 0x04002411 RID: 9233
		internal const string szOID_RSA_hashedData = "1.2.840.113549.1.7.5";

		// Token: 0x04002412 RID: 9234
		internal const string szOID_RSA_encryptedData = "1.2.840.113549.1.7.6";

		// Token: 0x04002413 RID: 9235
		internal const string szOID_RSA_emailAddr = "1.2.840.113549.1.9.1";

		// Token: 0x04002414 RID: 9236
		internal const string szOID_RSA_unstructName = "1.2.840.113549.1.9.2";

		// Token: 0x04002415 RID: 9237
		internal const string szOID_RSA_contentType = "1.2.840.113549.1.9.3";

		// Token: 0x04002416 RID: 9238
		internal const string szOID_RSA_messageDigest = "1.2.840.113549.1.9.4";

		// Token: 0x04002417 RID: 9239
		internal const string szOID_RSA_signingTime = "1.2.840.113549.1.9.5";

		// Token: 0x04002418 RID: 9240
		internal const string szOID_RSA_counterSign = "1.2.840.113549.1.9.6";

		// Token: 0x04002419 RID: 9241
		internal const string szOID_RSA_challengePwd = "1.2.840.113549.1.9.7";

		// Token: 0x0400241A RID: 9242
		internal const string szOID_RSA_unstructAddr = "1.2.840.113549.1.9.8";

		// Token: 0x0400241B RID: 9243
		internal const string szOID_RSA_extCertAttrs = "1.2.840.113549.1.9.9";

		// Token: 0x0400241C RID: 9244
		internal const string szOID_RSA_SMIMECapabilities = "1.2.840.113549.1.9.15";

		// Token: 0x0400241D RID: 9245
		internal const string szOID_CAPICOM = "1.3.6.1.4.1.311.88";

		// Token: 0x0400241E RID: 9246
		internal const string szOID_CAPICOM_version = "1.3.6.1.4.1.311.88.1";

		// Token: 0x0400241F RID: 9247
		internal const string szOID_CAPICOM_attribute = "1.3.6.1.4.1.311.88.2";

		// Token: 0x04002420 RID: 9248
		internal const string szOID_CAPICOM_documentName = "1.3.6.1.4.1.311.88.2.1";

		// Token: 0x04002421 RID: 9249
		internal const string szOID_CAPICOM_documentDescription = "1.3.6.1.4.1.311.88.2.2";

		// Token: 0x04002422 RID: 9250
		internal const string szOID_CAPICOM_encryptedData = "1.3.6.1.4.1.311.88.3";

		// Token: 0x04002423 RID: 9251
		internal const string szOID_CAPICOM_encryptedContent = "1.3.6.1.4.1.311.88.3.1";

		// Token: 0x04002424 RID: 9252
		internal const string szOID_OIWSEC_sha1 = "1.3.14.3.2.26";

		// Token: 0x04002425 RID: 9253
		internal const string szOID_RSA_MD5 = "1.2.840.113549.2.5";

		// Token: 0x04002426 RID: 9254
		internal const string szOID_OIWSEC_SHA256 = "2.16.840.1.101.3.4.1";

		// Token: 0x04002427 RID: 9255
		internal const string szOID_OIWSEC_SHA384 = "2.16.840.1.101.3.4.2";

		// Token: 0x04002428 RID: 9256
		internal const string szOID_OIWSEC_SHA512 = "2.16.840.1.101.3.4.3";

		// Token: 0x04002429 RID: 9257
		internal const string szOID_RSA_RC2CBC = "1.2.840.113549.3.2";

		// Token: 0x0400242A RID: 9258
		internal const string szOID_RSA_RC4 = "1.2.840.113549.3.4";

		// Token: 0x0400242B RID: 9259
		internal const string szOID_RSA_DES_EDE3_CBC = "1.2.840.113549.3.7";

		// Token: 0x0400242C RID: 9260
		internal const string szOID_OIWSEC_desCBC = "1.3.14.3.2.7";

		// Token: 0x0400242D RID: 9261
		internal const string szOID_RSA_SMIMEalg = "1.2.840.113549.1.9.16.3";

		// Token: 0x0400242E RID: 9262
		internal const string szOID_RSA_SMIMEalgESDH = "1.2.840.113549.1.9.16.3.5";

		// Token: 0x0400242F RID: 9263
		internal const string szOID_RSA_SMIMEalgCMS3DESwrap = "1.2.840.113549.1.9.16.3.6";

		// Token: 0x04002430 RID: 9264
		internal const string szOID_RSA_SMIMEalgCMSRC2wrap = "1.2.840.113549.1.9.16.3.7";

		// Token: 0x04002431 RID: 9265
		internal const string szOID_X957_DSA = "1.2.840.10040.4.1";

		// Token: 0x04002432 RID: 9266
		internal const string szOID_X957_sha1DSA = "1.2.840.10040.4.3";

		// Token: 0x04002433 RID: 9267
		internal const string szOID_OIWSEC_sha1RSASign = "1.3.14.3.2.29";

		// Token: 0x04002434 RID: 9268
		internal const uint CERT_ALT_NAME_OTHER_NAME = 1U;

		// Token: 0x04002435 RID: 9269
		internal const uint CERT_ALT_NAME_RFC822_NAME = 2U;

		// Token: 0x04002436 RID: 9270
		internal const uint CERT_ALT_NAME_DNS_NAME = 3U;

		// Token: 0x04002437 RID: 9271
		internal const uint CERT_ALT_NAME_X400_ADDRESS = 4U;

		// Token: 0x04002438 RID: 9272
		internal const uint CERT_ALT_NAME_DIRECTORY_NAME = 5U;

		// Token: 0x04002439 RID: 9273
		internal const uint CERT_ALT_NAME_EDI_PARTY_NAME = 6U;

		// Token: 0x0400243A RID: 9274
		internal const uint CERT_ALT_NAME_URL = 7U;

		// Token: 0x0400243B RID: 9275
		internal const uint CERT_ALT_NAME_IP_ADDRESS = 8U;

		// Token: 0x0400243C RID: 9276
		internal const uint CERT_ALT_NAME_REGISTERED_ID = 9U;

		// Token: 0x0400243D RID: 9277
		internal const uint CERT_RDN_ANY_TYPE = 0U;

		// Token: 0x0400243E RID: 9278
		internal const uint CERT_RDN_ENCODED_BLOB = 1U;

		// Token: 0x0400243F RID: 9279
		internal const uint CERT_RDN_OCTET_STRING = 2U;

		// Token: 0x04002440 RID: 9280
		internal const uint CERT_RDN_NUMERIC_STRING = 3U;

		// Token: 0x04002441 RID: 9281
		internal const uint CERT_RDN_PRINTABLE_STRING = 4U;

		// Token: 0x04002442 RID: 9282
		internal const uint CERT_RDN_TELETEX_STRING = 5U;

		// Token: 0x04002443 RID: 9283
		internal const uint CERT_RDN_T61_STRING = 5U;

		// Token: 0x04002444 RID: 9284
		internal const uint CERT_RDN_VIDEOTEX_STRING = 6U;

		// Token: 0x04002445 RID: 9285
		internal const uint CERT_RDN_IA5_STRING = 7U;

		// Token: 0x04002446 RID: 9286
		internal const uint CERT_RDN_GRAPHIC_STRING = 8U;

		// Token: 0x04002447 RID: 9287
		internal const uint CERT_RDN_VISIBLE_STRING = 9U;

		// Token: 0x04002448 RID: 9288
		internal const uint CERT_RDN_ISO646_STRING = 9U;

		// Token: 0x04002449 RID: 9289
		internal const uint CERT_RDN_GENERAL_STRING = 10U;

		// Token: 0x0400244A RID: 9290
		internal const uint CERT_RDN_UNIVERSAL_STRING = 11U;

		// Token: 0x0400244B RID: 9291
		internal const uint CERT_RDN_INT4_STRING = 11U;

		// Token: 0x0400244C RID: 9292
		internal const uint CERT_RDN_BMP_STRING = 12U;

		// Token: 0x0400244D RID: 9293
		internal const uint CERT_RDN_UNICODE_STRING = 12U;

		// Token: 0x0400244E RID: 9294
		internal const uint CERT_RDN_UTF8_STRING = 13U;

		// Token: 0x0400244F RID: 9295
		internal const uint CERT_RDN_TYPE_MASK = 255U;

		// Token: 0x04002450 RID: 9296
		internal const uint CERT_RDN_FLAGS_MASK = 4278190080U;

		// Token: 0x04002451 RID: 9297
		internal const uint CERT_STORE_CTRL_RESYNC = 1U;

		// Token: 0x04002452 RID: 9298
		internal const uint CERT_STORE_CTRL_NOTIFY_CHANGE = 2U;

		// Token: 0x04002453 RID: 9299
		internal const uint CERT_STORE_CTRL_COMMIT = 3U;

		// Token: 0x04002454 RID: 9300
		internal const uint CERT_STORE_CTRL_AUTO_RESYNC = 4U;

		// Token: 0x04002455 RID: 9301
		internal const uint CERT_STORE_CTRL_CANCEL_NOTIFY = 5U;

		// Token: 0x04002456 RID: 9302
		internal const uint CERT_ID_ISSUER_SERIAL_NUMBER = 1U;

		// Token: 0x04002457 RID: 9303
		internal const uint CERT_ID_KEY_IDENTIFIER = 2U;

		// Token: 0x04002458 RID: 9304
		internal const uint CERT_ID_SHA1_HASH = 3U;

		// Token: 0x04002459 RID: 9305
		internal const string MS_ENHANCED_PROV = "Microsoft Enhanced Cryptographic Provider v1.0";

		// Token: 0x0400245A RID: 9306
		internal const string MS_STRONG_PROV = "Microsoft Strong Cryptographic Provider";

		// Token: 0x0400245B RID: 9307
		internal const string MS_DEF_PROV = "Microsoft Base Cryptographic Provider v1.0";

		// Token: 0x0400245C RID: 9308
		internal const string MS_DEF_DSS_DH_PROV = "Microsoft Base DSS and Diffie-Hellman Cryptographic Provider";

		// Token: 0x0400245D RID: 9309
		internal const string MS_ENH_DSS_DH_PROV = "Microsoft Enhanced DSS and Diffie-Hellman Cryptographic Provider";

		// Token: 0x0400245E RID: 9310
		internal const string DummySignerCommonName = "CN=Dummy Signer";

		// Token: 0x0400245F RID: 9311
		internal const uint PROV_RSA_FULL = 1U;

		// Token: 0x04002460 RID: 9312
		internal const uint PROV_DSS_DH = 13U;

		// Token: 0x04002461 RID: 9313
		internal const uint ALG_TYPE_ANY = 0U;

		// Token: 0x04002462 RID: 9314
		internal const uint ALG_TYPE_DSS = 512U;

		// Token: 0x04002463 RID: 9315
		internal const uint ALG_TYPE_RSA = 1024U;

		// Token: 0x04002464 RID: 9316
		internal const uint ALG_TYPE_BLOCK = 1536U;

		// Token: 0x04002465 RID: 9317
		internal const uint ALG_TYPE_STREAM = 2048U;

		// Token: 0x04002466 RID: 9318
		internal const uint ALG_TYPE_DH = 2560U;

		// Token: 0x04002467 RID: 9319
		internal const uint ALG_TYPE_SECURECHANNEL = 3072U;

		// Token: 0x04002468 RID: 9320
		internal const uint ALG_CLASS_ANY = 0U;

		// Token: 0x04002469 RID: 9321
		internal const uint ALG_CLASS_SIGNATURE = 8192U;

		// Token: 0x0400246A RID: 9322
		internal const uint ALG_CLASS_MSG_ENCRYPT = 16384U;

		// Token: 0x0400246B RID: 9323
		internal const uint ALG_CLASS_DATA_ENCRYPT = 24576U;

		// Token: 0x0400246C RID: 9324
		internal const uint ALG_CLASS_HASH = 32768U;

		// Token: 0x0400246D RID: 9325
		internal const uint ALG_CLASS_KEY_EXCHANGE = 40960U;

		// Token: 0x0400246E RID: 9326
		internal const uint ALG_CLASS_ALL = 57344U;

		// Token: 0x0400246F RID: 9327
		internal const uint ALG_SID_ANY = 0U;

		// Token: 0x04002470 RID: 9328
		internal const uint ALG_SID_RSA_ANY = 0U;

		// Token: 0x04002471 RID: 9329
		internal const uint ALG_SID_RSA_PKCS = 1U;

		// Token: 0x04002472 RID: 9330
		internal const uint ALG_SID_RSA_MSATWORK = 2U;

		// Token: 0x04002473 RID: 9331
		internal const uint ALG_SID_RSA_ENTRUST = 3U;

		// Token: 0x04002474 RID: 9332
		internal const uint ALG_SID_RSA_PGP = 4U;

		// Token: 0x04002475 RID: 9333
		internal const uint ALG_SID_DSS_ANY = 0U;

		// Token: 0x04002476 RID: 9334
		internal const uint ALG_SID_DSS_PKCS = 1U;

		// Token: 0x04002477 RID: 9335
		internal const uint ALG_SID_DSS_DMS = 2U;

		// Token: 0x04002478 RID: 9336
		internal const uint ALG_SID_DES = 1U;

		// Token: 0x04002479 RID: 9337
		internal const uint ALG_SID_3DES = 3U;

		// Token: 0x0400247A RID: 9338
		internal const uint ALG_SID_DESX = 4U;

		// Token: 0x0400247B RID: 9339
		internal const uint ALG_SID_IDEA = 5U;

		// Token: 0x0400247C RID: 9340
		internal const uint ALG_SID_CAST = 6U;

		// Token: 0x0400247D RID: 9341
		internal const uint ALG_SID_SAFERSK64 = 7U;

		// Token: 0x0400247E RID: 9342
		internal const uint ALG_SID_SAFERSK128 = 8U;

		// Token: 0x0400247F RID: 9343
		internal const uint ALG_SID_3DES_112 = 9U;

		// Token: 0x04002480 RID: 9344
		internal const uint ALG_SID_CYLINK_MEK = 12U;

		// Token: 0x04002481 RID: 9345
		internal const uint ALG_SID_RC5 = 13U;

		// Token: 0x04002482 RID: 9346
		internal const uint ALG_SID_AES_128 = 14U;

		// Token: 0x04002483 RID: 9347
		internal const uint ALG_SID_AES_192 = 15U;

		// Token: 0x04002484 RID: 9348
		internal const uint ALG_SID_AES_256 = 16U;

		// Token: 0x04002485 RID: 9349
		internal const uint ALG_SID_AES = 17U;

		// Token: 0x04002486 RID: 9350
		internal const uint ALG_SID_SKIPJACK = 10U;

		// Token: 0x04002487 RID: 9351
		internal const uint ALG_SID_TEK = 11U;

		// Token: 0x04002488 RID: 9352
		internal const uint ALG_SID_RC2 = 2U;

		// Token: 0x04002489 RID: 9353
		internal const uint ALG_SID_RC4 = 1U;

		// Token: 0x0400248A RID: 9354
		internal const uint ALG_SID_SEAL = 2U;

		// Token: 0x0400248B RID: 9355
		internal const uint ALG_SID_DH_SANDF = 1U;

		// Token: 0x0400248C RID: 9356
		internal const uint ALG_SID_DH_EPHEM = 2U;

		// Token: 0x0400248D RID: 9357
		internal const uint ALG_SID_AGREED_KEY_ANY = 3U;

		// Token: 0x0400248E RID: 9358
		internal const uint ALG_SID_KEA = 4U;

		// Token: 0x0400248F RID: 9359
		internal const uint ALG_SID_MD2 = 1U;

		// Token: 0x04002490 RID: 9360
		internal const uint ALG_SID_MD4 = 2U;

		// Token: 0x04002491 RID: 9361
		internal const uint ALG_SID_MD5 = 3U;

		// Token: 0x04002492 RID: 9362
		internal const uint ALG_SID_SHA = 4U;

		// Token: 0x04002493 RID: 9363
		internal const uint ALG_SID_SHA1 = 4U;

		// Token: 0x04002494 RID: 9364
		internal const uint ALG_SID_MAC = 5U;

		// Token: 0x04002495 RID: 9365
		internal const uint ALG_SID_RIPEMD = 6U;

		// Token: 0x04002496 RID: 9366
		internal const uint ALG_SID_RIPEMD160 = 7U;

		// Token: 0x04002497 RID: 9367
		internal const uint ALG_SID_SSL3SHAMD5 = 8U;

		// Token: 0x04002498 RID: 9368
		internal const uint ALG_SID_HMAC = 9U;

		// Token: 0x04002499 RID: 9369
		internal const uint ALG_SID_TLS1PRF = 10U;

		// Token: 0x0400249A RID: 9370
		internal const uint ALG_SID_HASH_REPLACE_OWF = 11U;

		// Token: 0x0400249B RID: 9371
		internal const uint ALG_SID_SSL3_MASTER = 1U;

		// Token: 0x0400249C RID: 9372
		internal const uint ALG_SID_SCHANNEL_MASTER_HASH = 2U;

		// Token: 0x0400249D RID: 9373
		internal const uint ALG_SID_SCHANNEL_MAC_KEY = 3U;

		// Token: 0x0400249E RID: 9374
		internal const uint ALG_SID_PCT1_MASTER = 4U;

		// Token: 0x0400249F RID: 9375
		internal const uint ALG_SID_SSL2_MASTER = 5U;

		// Token: 0x040024A0 RID: 9376
		internal const uint ALG_SID_TLS1_MASTER = 6U;

		// Token: 0x040024A1 RID: 9377
		internal const uint ALG_SID_SCHANNEL_ENC_KEY = 7U;

		// Token: 0x040024A2 RID: 9378
		internal const uint CALG_MD2 = 32769U;

		// Token: 0x040024A3 RID: 9379
		internal const uint CALG_MD4 = 32770U;

		// Token: 0x040024A4 RID: 9380
		internal const uint CALG_MD5 = 32771U;

		// Token: 0x040024A5 RID: 9381
		internal const uint CALG_SHA = 32772U;

		// Token: 0x040024A6 RID: 9382
		internal const uint CALG_SHA1 = 32772U;

		// Token: 0x040024A7 RID: 9383
		internal const uint CALG_MAC = 32773U;

		// Token: 0x040024A8 RID: 9384
		internal const uint CALG_RSA_SIGN = 9216U;

		// Token: 0x040024A9 RID: 9385
		internal const uint CALG_DSS_SIGN = 8704U;

		// Token: 0x040024AA RID: 9386
		internal const uint CALG_NO_SIGN = 8192U;

		// Token: 0x040024AB RID: 9387
		internal const uint CALG_RSA_KEYX = 41984U;

		// Token: 0x040024AC RID: 9388
		internal const uint CALG_DES = 26113U;

		// Token: 0x040024AD RID: 9389
		internal const uint CALG_3DES_112 = 26121U;

		// Token: 0x040024AE RID: 9390
		internal const uint CALG_3DES = 26115U;

		// Token: 0x040024AF RID: 9391
		internal const uint CALG_DESX = 26116U;

		// Token: 0x040024B0 RID: 9392
		internal const uint CALG_RC2 = 26114U;

		// Token: 0x040024B1 RID: 9393
		internal const uint CALG_RC4 = 26625U;

		// Token: 0x040024B2 RID: 9394
		internal const uint CALG_SEAL = 26626U;

		// Token: 0x040024B3 RID: 9395
		internal const uint CALG_DH_SF = 43521U;

		// Token: 0x040024B4 RID: 9396
		internal const uint CALG_DH_EPHEM = 43522U;

		// Token: 0x040024B5 RID: 9397
		internal const uint CALG_AGREEDKEY_ANY = 43523U;

		// Token: 0x040024B6 RID: 9398
		internal const uint CALG_KEA_KEYX = 43524U;

		// Token: 0x040024B7 RID: 9399
		internal const uint CALG_HUGHES_MD5 = 40963U;

		// Token: 0x040024B8 RID: 9400
		internal const uint CALG_SKIPJACK = 26122U;

		// Token: 0x040024B9 RID: 9401
		internal const uint CALG_TEK = 26123U;

		// Token: 0x040024BA RID: 9402
		internal const uint CALG_CYLINK_MEK = 26124U;

		// Token: 0x040024BB RID: 9403
		internal const uint CALG_SSL3_SHAMD5 = 32776U;

		// Token: 0x040024BC RID: 9404
		internal const uint CALG_SSL3_MASTER = 19457U;

		// Token: 0x040024BD RID: 9405
		internal const uint CALG_SCHANNEL_MASTER_HASH = 19458U;

		// Token: 0x040024BE RID: 9406
		internal const uint CALG_SCHANNEL_MAC_KEY = 19459U;

		// Token: 0x040024BF RID: 9407
		internal const uint CALG_SCHANNEL_ENC_KEY = 19463U;

		// Token: 0x040024C0 RID: 9408
		internal const uint CALG_PCT1_MASTER = 19460U;

		// Token: 0x040024C1 RID: 9409
		internal const uint CALG_SSL2_MASTER = 19461U;

		// Token: 0x040024C2 RID: 9410
		internal const uint CALG_TLS1_MASTER = 19462U;

		// Token: 0x040024C3 RID: 9411
		internal const uint CALG_RC5 = 26125U;

		// Token: 0x040024C4 RID: 9412
		internal const uint CALG_HMAC = 32777U;

		// Token: 0x040024C5 RID: 9413
		internal const uint CALG_TLS1PRF = 32778U;

		// Token: 0x040024C6 RID: 9414
		internal const uint CALG_HASH_REPLACE_OWF = 32779U;

		// Token: 0x040024C7 RID: 9415
		internal const uint CALG_AES_128 = 26126U;

		// Token: 0x040024C8 RID: 9416
		internal const uint CALG_AES_192 = 26127U;

		// Token: 0x040024C9 RID: 9417
		internal const uint CALG_AES_256 = 26128U;

		// Token: 0x040024CA RID: 9418
		internal const uint CALG_AES = 26129U;

		// Token: 0x040024CB RID: 9419
		internal const uint CRYPT_FIRST = 1U;

		// Token: 0x040024CC RID: 9420
		internal const uint CRYPT_NEXT = 2U;

		// Token: 0x040024CD RID: 9421
		internal const uint PP_ENUMALGS_EX = 22U;

		// Token: 0x040024CE RID: 9422
		internal const uint CRYPT_VERIFYCONTEXT = 4026531840U;

		// Token: 0x040024CF RID: 9423
		internal const uint CRYPT_NEWKEYSET = 8U;

		// Token: 0x040024D0 RID: 9424
		internal const uint CRYPT_DELETEKEYSET = 16U;

		// Token: 0x040024D1 RID: 9425
		internal const uint CRYPT_MACHINE_KEYSET = 32U;

		// Token: 0x040024D2 RID: 9426
		internal const uint CRYPT_SILENT = 64U;

		// Token: 0x040024D3 RID: 9427
		internal const uint CRYPT_USER_KEYSET = 4096U;

		// Token: 0x040024D4 RID: 9428
		internal const uint PKCS12_ALWAYS_CNG_KSP = 512U;

		// Token: 0x040024D5 RID: 9429
		internal const uint PKCS12_NO_PERSIST_KEY = 32768U;

		// Token: 0x040024D6 RID: 9430
		internal const uint CRYPT_EXPORTABLE = 1U;

		// Token: 0x040024D7 RID: 9431
		internal const uint CRYPT_USER_PROTECTED = 2U;

		// Token: 0x040024D8 RID: 9432
		internal const uint CRYPT_CREATE_SALT = 4U;

		// Token: 0x040024D9 RID: 9433
		internal const uint CRYPT_UPDATE_KEY = 8U;

		// Token: 0x040024DA RID: 9434
		internal const uint CRYPT_NO_SALT = 16U;

		// Token: 0x040024DB RID: 9435
		internal const uint CRYPT_PREGEN = 64U;

		// Token: 0x040024DC RID: 9436
		internal const uint CRYPT_RECIPIENT = 16U;

		// Token: 0x040024DD RID: 9437
		internal const uint CRYPT_INITIATOR = 64U;

		// Token: 0x040024DE RID: 9438
		internal const uint CRYPT_ONLINE = 128U;

		// Token: 0x040024DF RID: 9439
		internal const uint CRYPT_SF = 256U;

		// Token: 0x040024E0 RID: 9440
		internal const uint CRYPT_CREATE_IV = 512U;

		// Token: 0x040024E1 RID: 9441
		internal const uint CRYPT_KEK = 1024U;

		// Token: 0x040024E2 RID: 9442
		internal const uint CRYPT_DATA_KEY = 2048U;

		// Token: 0x040024E3 RID: 9443
		internal const uint CRYPT_VOLATILE = 4096U;

		// Token: 0x040024E4 RID: 9444
		internal const uint CRYPT_SGCKEY = 8192U;

		// Token: 0x040024E5 RID: 9445
		internal const uint CRYPT_ARCHIVABLE = 16384U;

		// Token: 0x040024E6 RID: 9446
		internal const byte CUR_BLOB_VERSION = 2;

		// Token: 0x040024E7 RID: 9447
		internal const byte SIMPLEBLOB = 1;

		// Token: 0x040024E8 RID: 9448
		internal const byte PUBLICKEYBLOB = 6;

		// Token: 0x040024E9 RID: 9449
		internal const byte PRIVATEKEYBLOB = 7;

		// Token: 0x040024EA RID: 9450
		internal const byte PLAINTEXTKEYBLOB = 8;

		// Token: 0x040024EB RID: 9451
		internal const byte OPAQUEKEYBLOB = 9;

		// Token: 0x040024EC RID: 9452
		internal const byte PUBLICKEYBLOBEX = 10;

		// Token: 0x040024ED RID: 9453
		internal const byte SYMMETRICWRAPKEYBLOB = 11;

		// Token: 0x040024EE RID: 9454
		internal const uint DSS_MAGIC = 827544388U;

		// Token: 0x040024EF RID: 9455
		internal const uint DSS_PRIVATE_MAGIC = 844321604U;

		// Token: 0x040024F0 RID: 9456
		internal const uint DSS_PUB_MAGIC_VER3 = 861098820U;

		// Token: 0x040024F1 RID: 9457
		internal const uint DSS_PRIV_MAGIC_VER3 = 877876036U;

		// Token: 0x040024F2 RID: 9458
		internal const uint RSA_PUB_MAGIC = 826364754U;

		// Token: 0x040024F3 RID: 9459
		internal const uint RSA_PRIV_MAGIC = 843141970U;

		// Token: 0x040024F4 RID: 9460
		internal const uint CRYPT_ACQUIRE_CACHE_FLAG = 1U;

		// Token: 0x040024F5 RID: 9461
		internal const uint CRYPT_ACQUIRE_USE_PROV_INFO_FLAG = 2U;

		// Token: 0x040024F6 RID: 9462
		internal const uint CRYPT_ACQUIRE_COMPARE_KEY_FLAG = 4U;

		// Token: 0x040024F7 RID: 9463
		internal const uint CRYPT_ACQUIRE_SILENT_FLAG = 64U;

		// Token: 0x040024F8 RID: 9464
		internal const uint CMSG_BARE_CONTENT_FLAG = 1U;

		// Token: 0x040024F9 RID: 9465
		internal const uint CMSG_LENGTH_ONLY_FLAG = 2U;

		// Token: 0x040024FA RID: 9466
		internal const uint CMSG_DETACHED_FLAG = 4U;

		// Token: 0x040024FB RID: 9467
		internal const uint CMSG_AUTHENTICATED_ATTRIBUTES_FLAG = 8U;

		// Token: 0x040024FC RID: 9468
		internal const uint CMSG_CONTENTS_OCTETS_FLAG = 16U;

		// Token: 0x040024FD RID: 9469
		internal const uint CMSG_MAX_LENGTH_FLAG = 32U;

		// Token: 0x040024FE RID: 9470
		internal const uint CMSG_TYPE_PARAM = 1U;

		// Token: 0x040024FF RID: 9471
		internal const uint CMSG_CONTENT_PARAM = 2U;

		// Token: 0x04002500 RID: 9472
		internal const uint CMSG_BARE_CONTENT_PARAM = 3U;

		// Token: 0x04002501 RID: 9473
		internal const uint CMSG_INNER_CONTENT_TYPE_PARAM = 4U;

		// Token: 0x04002502 RID: 9474
		internal const uint CMSG_SIGNER_COUNT_PARAM = 5U;

		// Token: 0x04002503 RID: 9475
		internal const uint CMSG_SIGNER_INFO_PARAM = 6U;

		// Token: 0x04002504 RID: 9476
		internal const uint CMSG_SIGNER_CERT_INFO_PARAM = 7U;

		// Token: 0x04002505 RID: 9477
		internal const uint CMSG_SIGNER_HASH_ALGORITHM_PARAM = 8U;

		// Token: 0x04002506 RID: 9478
		internal const uint CMSG_SIGNER_AUTH_ATTR_PARAM = 9U;

		// Token: 0x04002507 RID: 9479
		internal const uint CMSG_SIGNER_UNAUTH_ATTR_PARAM = 10U;

		// Token: 0x04002508 RID: 9480
		internal const uint CMSG_CERT_COUNT_PARAM = 11U;

		// Token: 0x04002509 RID: 9481
		internal const uint CMSG_CERT_PARAM = 12U;

		// Token: 0x0400250A RID: 9482
		internal const uint CMSG_CRL_COUNT_PARAM = 13U;

		// Token: 0x0400250B RID: 9483
		internal const uint CMSG_CRL_PARAM = 14U;

		// Token: 0x0400250C RID: 9484
		internal const uint CMSG_ENVELOPE_ALGORITHM_PARAM = 15U;

		// Token: 0x0400250D RID: 9485
		internal const uint CMSG_RECIPIENT_COUNT_PARAM = 17U;

		// Token: 0x0400250E RID: 9486
		internal const uint CMSG_RECIPIENT_INDEX_PARAM = 18U;

		// Token: 0x0400250F RID: 9487
		internal const uint CMSG_RECIPIENT_INFO_PARAM = 19U;

		// Token: 0x04002510 RID: 9488
		internal const uint CMSG_HASH_ALGORITHM_PARAM = 20U;

		// Token: 0x04002511 RID: 9489
		internal const uint CMSG_HASH_DATA_PARAM = 21U;

		// Token: 0x04002512 RID: 9490
		internal const uint CMSG_COMPUTED_HASH_PARAM = 22U;

		// Token: 0x04002513 RID: 9491
		internal const uint CMSG_ENCRYPT_PARAM = 26U;

		// Token: 0x04002514 RID: 9492
		internal const uint CMSG_ENCRYPTED_DIGEST = 27U;

		// Token: 0x04002515 RID: 9493
		internal const uint CMSG_ENCODED_SIGNER = 28U;

		// Token: 0x04002516 RID: 9494
		internal const uint CMSG_ENCODED_MESSAGE = 29U;

		// Token: 0x04002517 RID: 9495
		internal const uint CMSG_VERSION_PARAM = 30U;

		// Token: 0x04002518 RID: 9496
		internal const uint CMSG_ATTR_CERT_COUNT_PARAM = 31U;

		// Token: 0x04002519 RID: 9497
		internal const uint CMSG_ATTR_CERT_PARAM = 32U;

		// Token: 0x0400251A RID: 9498
		internal const uint CMSG_CMS_RECIPIENT_COUNT_PARAM = 33U;

		// Token: 0x0400251B RID: 9499
		internal const uint CMSG_CMS_RECIPIENT_INDEX_PARAM = 34U;

		// Token: 0x0400251C RID: 9500
		internal const uint CMSG_CMS_RECIPIENT_ENCRYPTED_KEY_INDEX_PARAM = 35U;

		// Token: 0x0400251D RID: 9501
		internal const uint CMSG_CMS_RECIPIENT_INFO_PARAM = 36U;

		// Token: 0x0400251E RID: 9502
		internal const uint CMSG_UNPROTECTED_ATTR_PARAM = 37U;

		// Token: 0x0400251F RID: 9503
		internal const uint CMSG_SIGNER_CERT_ID_PARAM = 38U;

		// Token: 0x04002520 RID: 9504
		internal const uint CMSG_CMS_SIGNER_INFO_PARAM = 39U;

		// Token: 0x04002521 RID: 9505
		internal const uint CMSG_CTRL_VERIFY_SIGNATURE = 1U;

		// Token: 0x04002522 RID: 9506
		internal const uint CMSG_CTRL_DECRYPT = 2U;

		// Token: 0x04002523 RID: 9507
		internal const uint CMSG_CTRL_VERIFY_HASH = 5U;

		// Token: 0x04002524 RID: 9508
		internal const uint CMSG_CTRL_ADD_SIGNER = 6U;

		// Token: 0x04002525 RID: 9509
		internal const uint CMSG_CTRL_DEL_SIGNER = 7U;

		// Token: 0x04002526 RID: 9510
		internal const uint CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR = 8U;

		// Token: 0x04002527 RID: 9511
		internal const uint CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR = 9U;

		// Token: 0x04002528 RID: 9512
		internal const uint CMSG_CTRL_ADD_CERT = 10U;

		// Token: 0x04002529 RID: 9513
		internal const uint CMSG_CTRL_DEL_CERT = 11U;

		// Token: 0x0400252A RID: 9514
		internal const uint CMSG_CTRL_ADD_CRL = 12U;

		// Token: 0x0400252B RID: 9515
		internal const uint CMSG_CTRL_DEL_CRL = 13U;

		// Token: 0x0400252C RID: 9516
		internal const uint CMSG_CTRL_ADD_ATTR_CERT = 14U;

		// Token: 0x0400252D RID: 9517
		internal const uint CMSG_CTRL_DEL_ATTR_CERT = 15U;

		// Token: 0x0400252E RID: 9518
		internal const uint CMSG_CTRL_KEY_TRANS_DECRYPT = 16U;

		// Token: 0x0400252F RID: 9519
		internal const uint CMSG_CTRL_KEY_AGREE_DECRYPT = 17U;

		// Token: 0x04002530 RID: 9520
		internal const uint CMSG_CTRL_MAIL_LIST_DECRYPT = 18U;

		// Token: 0x04002531 RID: 9521
		internal const uint CMSG_CTRL_VERIFY_SIGNATURE_EX = 19U;

		// Token: 0x04002532 RID: 9522
		internal const uint CMSG_CTRL_ADD_CMS_SIGNER_INFO = 20U;

		// Token: 0x04002533 RID: 9523
		internal const uint CMSG_VERIFY_SIGNER_PUBKEY = 1U;

		// Token: 0x04002534 RID: 9524
		internal const uint CMSG_VERIFY_SIGNER_CERT = 2U;

		// Token: 0x04002535 RID: 9525
		internal const uint CMSG_VERIFY_SIGNER_CHAIN = 3U;

		// Token: 0x04002536 RID: 9526
		internal const uint CMSG_VERIFY_SIGNER_NULL = 4U;

		// Token: 0x04002537 RID: 9527
		internal const uint CMSG_DATA = 1U;

		// Token: 0x04002538 RID: 9528
		internal const uint CMSG_SIGNED = 2U;

		// Token: 0x04002539 RID: 9529
		internal const uint CMSG_ENVELOPED = 3U;

		// Token: 0x0400253A RID: 9530
		internal const uint CMSG_SIGNED_AND_ENVELOPED = 4U;

		// Token: 0x0400253B RID: 9531
		internal const uint CMSG_HASHED = 5U;

		// Token: 0x0400253C RID: 9532
		internal const uint CMSG_ENCRYPTED = 6U;

		// Token: 0x0400253D RID: 9533
		internal const uint CMSG_KEY_TRANS_RECIPIENT = 1U;

		// Token: 0x0400253E RID: 9534
		internal const uint CMSG_KEY_AGREE_RECIPIENT = 2U;

		// Token: 0x0400253F RID: 9535
		internal const uint CMSG_MAIL_LIST_RECIPIENT = 3U;

		// Token: 0x04002540 RID: 9536
		internal const uint CMSG_KEY_AGREE_ORIGINATOR_CERT = 1U;

		// Token: 0x04002541 RID: 9537
		internal const uint CMSG_KEY_AGREE_ORIGINATOR_PUBLIC_KEY = 2U;

		// Token: 0x04002542 RID: 9538
		internal const uint CMSG_KEY_AGREE_EPHEMERAL_KEY_CHOICE = 1U;

		// Token: 0x04002543 RID: 9539
		internal const uint CMSG_KEY_AGREE_STATIC_KEY_CHOICE = 2U;

		// Token: 0x04002544 RID: 9540
		internal const uint CMSG_ENVELOPED_RECIPIENT_V0 = 0U;

		// Token: 0x04002545 RID: 9541
		internal const uint CMSG_ENVELOPED_RECIPIENT_V2 = 2U;

		// Token: 0x04002546 RID: 9542
		internal const uint CMSG_ENVELOPED_RECIPIENT_V3 = 3U;

		// Token: 0x04002547 RID: 9543
		internal const uint CMSG_ENVELOPED_RECIPIENT_V4 = 4U;

		// Token: 0x04002548 RID: 9544
		internal const uint CMSG_KEY_TRANS_PKCS_1_5_VERSION = 0U;

		// Token: 0x04002549 RID: 9545
		internal const uint CMSG_KEY_TRANS_CMS_VERSION = 2U;

		// Token: 0x0400254A RID: 9546
		internal const uint CMSG_KEY_AGREE_VERSION = 3U;

		// Token: 0x0400254B RID: 9547
		internal const uint CMSG_MAIL_LIST_VERSION = 4U;

		// Token: 0x0400254C RID: 9548
		internal const uint CRYPT_RC2_40BIT_VERSION = 160U;

		// Token: 0x0400254D RID: 9549
		internal const uint CRYPT_RC2_56BIT_VERSION = 52U;

		// Token: 0x0400254E RID: 9550
		internal const uint CRYPT_RC2_64BIT_VERSION = 120U;

		// Token: 0x0400254F RID: 9551
		internal const uint CRYPT_RC2_128BIT_VERSION = 58U;

		// Token: 0x04002550 RID: 9552
		internal const int E_NOTIMPL = -2147483647;

		// Token: 0x04002551 RID: 9553
		internal const int E_OUTOFMEMORY = -2147024882;

		// Token: 0x04002552 RID: 9554
		internal const int NTE_NO_KEY = -2146893811;

		// Token: 0x04002553 RID: 9555
		internal const int NTE_BAD_PUBLIC_KEY = -2146893803;

		// Token: 0x04002554 RID: 9556
		internal const int NTE_BAD_KEYSET = -2146893802;

		// Token: 0x04002555 RID: 9557
		internal const int CRYPT_E_MSG_ERROR = -2146889727;

		// Token: 0x04002556 RID: 9558
		internal const int CRYPT_E_UNKNOWN_ALGO = -2146889726;

		// Token: 0x04002557 RID: 9559
		internal const int CRYPT_E_INVALID_MSG_TYPE = -2146889724;

		// Token: 0x04002558 RID: 9560
		internal const int CRYPT_E_RECIPIENT_NOT_FOUND = -2146889717;

		// Token: 0x04002559 RID: 9561
		internal const int CRYPT_E_ISSUER_SERIALNUMBER = -2146889715;

		// Token: 0x0400255A RID: 9562
		internal const int CRYPT_E_SIGNER_NOT_FOUND = -2146889714;

		// Token: 0x0400255B RID: 9563
		internal const int CRYPT_E_ATTRIBUTES_MISSING = -2146889713;

		// Token: 0x0400255C RID: 9564
		internal const int CRYPT_E_BAD_ENCODE = -2146885630;

		// Token: 0x0400255D RID: 9565
		internal const int CRYPT_E_NOT_FOUND = -2146885628;

		// Token: 0x0400255E RID: 9566
		internal const int CRYPT_E_NO_MATCH = -2146885623;

		// Token: 0x0400255F RID: 9567
		internal const int CRYPT_E_NO_SIGNER = -2146885618;

		// Token: 0x04002560 RID: 9568
		internal const int CRYPT_E_REVOKED = -2146885616;

		// Token: 0x04002561 RID: 9569
		internal const int CRYPT_E_NO_REVOCATION_CHECK = -2146885614;

		// Token: 0x04002562 RID: 9570
		internal const int CRYPT_E_REVOCATION_OFFLINE = -2146885613;

		// Token: 0x04002563 RID: 9571
		internal const int CRYPT_E_ASN1_BADTAG = -2146881269;

		// Token: 0x04002564 RID: 9572
		internal const int CERTSRV_E_WEAK_SIGNATURE_OR_KEY = -2146877418;

		// Token: 0x04002565 RID: 9573
		internal const int TRUST_E_CERT_SIGNATURE = -2146869244;

		// Token: 0x04002566 RID: 9574
		internal const int TRUST_E_BASIC_CONSTRAINTS = -2146869223;

		// Token: 0x04002567 RID: 9575
		internal const int CERT_E_EXPIRED = -2146762495;

		// Token: 0x04002568 RID: 9576
		internal const int CERT_E_VALIDITYPERIODNESTING = -2146762494;

		// Token: 0x04002569 RID: 9577
		internal const int CERT_E_CRITICAL = -2146762491;

		// Token: 0x0400256A RID: 9578
		internal const int CERT_E_UNTRUSTEDROOT = -2146762487;

		// Token: 0x0400256B RID: 9579
		internal const int CERT_E_CHAINING = -2146762486;

		// Token: 0x0400256C RID: 9580
		internal const int TRUST_E_FAIL = -2146762485;

		// Token: 0x0400256D RID: 9581
		internal const int CERT_E_REVOKED = -2146762484;

		// Token: 0x0400256E RID: 9582
		internal const int CERT_E_UNTRUSTEDTESTROOT = -2146762483;

		// Token: 0x0400256F RID: 9583
		internal const int CERT_E_REVOCATION_FAILURE = -2146762482;

		// Token: 0x04002570 RID: 9584
		internal const int CERT_E_WRONG_USAGE = -2146762480;

		// Token: 0x04002571 RID: 9585
		internal const int TRUST_E_EXPLICIT_DISTRUST = -2146762479;

		// Token: 0x04002572 RID: 9586
		internal const int CERT_E_INVALID_POLICY = -2146762477;

		// Token: 0x04002573 RID: 9587
		internal const int CERT_E_INVALID_NAME = -2146762476;

		// Token: 0x04002574 RID: 9588
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x04002575 RID: 9589
		internal const int ERROR_CALL_NOT_IMPLEMENTED = 120;

		// Token: 0x04002576 RID: 9590
		internal const int ERROR_CANCELLED = 1223;

		// Token: 0x0200082C RID: 2092
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct BLOBHEADER
		{
			// Token: 0x040035CE RID: 13774
			internal byte bType;

			// Token: 0x040035CF RID: 13775
			internal byte bVersion;

			// Token: 0x040035D0 RID: 13776
			internal short reserved;

			// Token: 0x040035D1 RID: 13777
			internal uint aiKeyAlg;
		}

		// Token: 0x0200082D RID: 2093
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_ALT_NAME_INFO
		{
			// Token: 0x040035D2 RID: 13778
			internal uint cAltEntry;

			// Token: 0x040035D3 RID: 13779
			internal IntPtr rgAltEntry;
		}

		// Token: 0x0200082E RID: 2094
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_ALT_NAME_ENTRY
		{
			// Token: 0x040035D4 RID: 13780
			internal uint dwAltNameChoice;

			// Token: 0x040035D5 RID: 13781
			internal CAPIBase.CERT_ALT_NAME_ENTRY_UNION Value;
		}

		// Token: 0x0200082F RID: 2095
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		internal struct CERT_ALT_NAME_ENTRY_UNION
		{
			// Token: 0x040035D6 RID: 13782
			[FieldOffset(0)]
			internal IntPtr pOtherName;

			// Token: 0x040035D7 RID: 13783
			[FieldOffset(0)]
			internal IntPtr pwszRfc822Name;

			// Token: 0x040035D8 RID: 13784
			[FieldOffset(0)]
			internal IntPtr pwszDNSName;

			// Token: 0x040035D9 RID: 13785
			[FieldOffset(0)]
			internal CAPIBase.CRYPTOAPI_BLOB DirectoryName;

			// Token: 0x040035DA RID: 13786
			[FieldOffset(0)]
			internal IntPtr pwszURL;

			// Token: 0x040035DB RID: 13787
			[FieldOffset(0)]
			internal CAPIBase.CRYPTOAPI_BLOB IPAddress;

			// Token: 0x040035DC RID: 13788
			[FieldOffset(0)]
			internal IntPtr pszRegisteredID;
		}

		// Token: 0x02000830 RID: 2096
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_BASIC_CONSTRAINTS_INFO
		{
			// Token: 0x040035DD RID: 13789
			internal CAPIBase.CRYPT_BIT_BLOB SubjectType;

			// Token: 0x040035DE RID: 13790
			internal bool fPathLenConstraint;

			// Token: 0x040035DF RID: 13791
			internal uint dwPathLenConstraint;

			// Token: 0x040035E0 RID: 13792
			internal uint cSubtreesConstraint;

			// Token: 0x040035E1 RID: 13793
			internal IntPtr rgSubtreesConstraint;
		}

		// Token: 0x02000831 RID: 2097
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_BASIC_CONSTRAINTS2_INFO
		{
			// Token: 0x040035E2 RID: 13794
			internal int fCA;

			// Token: 0x040035E3 RID: 13795
			internal int fPathLenConstraint;

			// Token: 0x040035E4 RID: 13796
			internal uint dwPathLenConstraint;
		}

		// Token: 0x02000832 RID: 2098
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CHAIN_CONTEXT
		{
			// Token: 0x06004527 RID: 17703 RVA: 0x00120850 File Offset: 0x0011EA50
			internal CERT_CHAIN_CONTEXT(int size)
			{
				this.cbSize = (uint)size;
				this.dwErrorStatus = 0U;
				this.dwInfoStatus = 0U;
				this.cChain = 0U;
				this.rgpChain = IntPtr.Zero;
				this.cLowerQualityChainContext = 0U;
				this.rgpLowerQualityChainContext = IntPtr.Zero;
				this.fHasRevocationFreshnessTime = 0U;
				this.dwRevocationFreshnessTime = 0U;
			}

			// Token: 0x040035E5 RID: 13797
			internal uint cbSize;

			// Token: 0x040035E6 RID: 13798
			internal uint dwErrorStatus;

			// Token: 0x040035E7 RID: 13799
			internal uint dwInfoStatus;

			// Token: 0x040035E8 RID: 13800
			internal uint cChain;

			// Token: 0x040035E9 RID: 13801
			internal IntPtr rgpChain;

			// Token: 0x040035EA RID: 13802
			internal uint cLowerQualityChainContext;

			// Token: 0x040035EB RID: 13803
			internal IntPtr rgpLowerQualityChainContext;

			// Token: 0x040035EC RID: 13804
			internal uint fHasRevocationFreshnessTime;

			// Token: 0x040035ED RID: 13805
			internal uint dwRevocationFreshnessTime;
		}

		// Token: 0x02000833 RID: 2099
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CHAIN_ELEMENT
		{
			// Token: 0x06004528 RID: 17704 RVA: 0x001208A4 File Offset: 0x0011EAA4
			internal CERT_CHAIN_ELEMENT(int size)
			{
				this.cbSize = (uint)size;
				this.pCertContext = IntPtr.Zero;
				this.dwErrorStatus = 0U;
				this.dwInfoStatus = 0U;
				this.pRevocationInfo = IntPtr.Zero;
				this.pIssuanceUsage = IntPtr.Zero;
				this.pApplicationUsage = IntPtr.Zero;
				this.pwszExtendedErrorInfo = IntPtr.Zero;
			}

			// Token: 0x040035EE RID: 13806
			internal uint cbSize;

			// Token: 0x040035EF RID: 13807
			internal IntPtr pCertContext;

			// Token: 0x040035F0 RID: 13808
			internal uint dwErrorStatus;

			// Token: 0x040035F1 RID: 13809
			internal uint dwInfoStatus;

			// Token: 0x040035F2 RID: 13810
			internal IntPtr pRevocationInfo;

			// Token: 0x040035F3 RID: 13811
			internal IntPtr pIssuanceUsage;

			// Token: 0x040035F4 RID: 13812
			internal IntPtr pApplicationUsage;

			// Token: 0x040035F5 RID: 13813
			internal IntPtr pwszExtendedErrorInfo;
		}

		// Token: 0x02000834 RID: 2100
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CHAIN_PARA
		{
			// Token: 0x040035F6 RID: 13814
			internal uint cbSize;

			// Token: 0x040035F7 RID: 13815
			internal CAPIBase.CERT_USAGE_MATCH RequestedUsage;

			// Token: 0x040035F8 RID: 13816
			internal CAPIBase.CERT_USAGE_MATCH RequestedIssuancePolicy;

			// Token: 0x040035F9 RID: 13817
			internal uint dwUrlRetrievalTimeout;

			// Token: 0x040035FA RID: 13818
			internal bool fCheckRevocationFreshnessTime;

			// Token: 0x040035FB RID: 13819
			internal uint dwRevocationFreshnessTime;
		}

		// Token: 0x02000835 RID: 2101
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CHAIN_POLICY_PARA
		{
			// Token: 0x06004529 RID: 17705 RVA: 0x001208FD File Offset: 0x0011EAFD
			internal CERT_CHAIN_POLICY_PARA(int size)
			{
				this.cbSize = (uint)size;
				this.dwFlags = 0U;
				this.pvExtraPolicyPara = IntPtr.Zero;
			}

			// Token: 0x040035FC RID: 13820
			internal uint cbSize;

			// Token: 0x040035FD RID: 13821
			internal uint dwFlags;

			// Token: 0x040035FE RID: 13822
			internal IntPtr pvExtraPolicyPara;
		}

		// Token: 0x02000836 RID: 2102
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CHAIN_POLICY_STATUS
		{
			// Token: 0x0600452A RID: 17706 RVA: 0x00120918 File Offset: 0x0011EB18
			internal CERT_CHAIN_POLICY_STATUS(int size)
			{
				this.cbSize = (uint)size;
				this.dwError = 0U;
				this.lChainIndex = IntPtr.Zero;
				this.lElementIndex = IntPtr.Zero;
				this.pvExtraPolicyStatus = IntPtr.Zero;
			}

			// Token: 0x040035FF RID: 13823
			internal uint cbSize;

			// Token: 0x04003600 RID: 13824
			internal uint dwError;

			// Token: 0x04003601 RID: 13825
			internal IntPtr lChainIndex;

			// Token: 0x04003602 RID: 13826
			internal IntPtr lElementIndex;

			// Token: 0x04003603 RID: 13827
			internal IntPtr pvExtraPolicyStatus;
		}

		// Token: 0x02000837 RID: 2103
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_CONTEXT
		{
			// Token: 0x04003604 RID: 13828
			internal uint dwCertEncodingType;

			// Token: 0x04003605 RID: 13829
			internal IntPtr pbCertEncoded;

			// Token: 0x04003606 RID: 13830
			internal uint cbCertEncoded;

			// Token: 0x04003607 RID: 13831
			internal IntPtr pCertInfo;

			// Token: 0x04003608 RID: 13832
			internal IntPtr hCertStore;
		}

		// Token: 0x02000838 RID: 2104
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_DSS_PARAMETERS
		{
			// Token: 0x04003609 RID: 13833
			internal CAPIBase.CRYPTOAPI_BLOB p;

			// Token: 0x0400360A RID: 13834
			internal CAPIBase.CRYPTOAPI_BLOB q;

			// Token: 0x0400360B RID: 13835
			internal CAPIBase.CRYPTOAPI_BLOB g;
		}

		// Token: 0x02000839 RID: 2105
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_ENHKEY_USAGE
		{
			// Token: 0x0400360C RID: 13836
			internal uint cUsageIdentifier;

			// Token: 0x0400360D RID: 13837
			internal IntPtr rgpszUsageIdentifier;
		}

		// Token: 0x0200083A RID: 2106
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_EXTENSION
		{
			// Token: 0x0400360E RID: 13838
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszObjId;

			// Token: 0x0400360F RID: 13839
			internal bool fCritical;

			// Token: 0x04003610 RID: 13840
			internal CAPIBase.CRYPTOAPI_BLOB Value;
		}

		// Token: 0x0200083B RID: 2107
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_ID
		{
			// Token: 0x04003611 RID: 13841
			internal uint dwIdChoice;

			// Token: 0x04003612 RID: 13842
			internal CAPIBase.CERT_ID_UNION Value;
		}

		// Token: 0x0200083C RID: 2108
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		internal struct CERT_ID_UNION
		{
			// Token: 0x04003613 RID: 13843
			[FieldOffset(0)]
			internal CAPIBase.CERT_ISSUER_SERIAL_NUMBER IssuerSerialNumber;

			// Token: 0x04003614 RID: 13844
			[FieldOffset(0)]
			internal CAPIBase.CRYPTOAPI_BLOB KeyId;

			// Token: 0x04003615 RID: 13845
			[FieldOffset(0)]
			internal CAPIBase.CRYPTOAPI_BLOB HashId;
		}

		// Token: 0x0200083D RID: 2109
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_ISSUER_SERIAL_NUMBER
		{
			// Token: 0x04003616 RID: 13846
			internal CAPIBase.CRYPTOAPI_BLOB Issuer;

			// Token: 0x04003617 RID: 13847
			internal CAPIBase.CRYPTOAPI_BLOB SerialNumber;
		}

		// Token: 0x0200083E RID: 2110
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_INFO
		{
			// Token: 0x04003618 RID: 13848
			internal uint dwVersion;

			// Token: 0x04003619 RID: 13849
			internal CAPIBase.CRYPTOAPI_BLOB SerialNumber;

			// Token: 0x0400361A RID: 13850
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm;

			// Token: 0x0400361B RID: 13851
			internal CAPIBase.CRYPTOAPI_BLOB Issuer;

			// Token: 0x0400361C RID: 13852
			internal System.Runtime.InteropServices.ComTypes.FILETIME NotBefore;

			// Token: 0x0400361D RID: 13853
			internal System.Runtime.InteropServices.ComTypes.FILETIME NotAfter;

			// Token: 0x0400361E RID: 13854
			internal CAPIBase.CRYPTOAPI_BLOB Subject;

			// Token: 0x0400361F RID: 13855
			internal CAPIBase.CERT_PUBLIC_KEY_INFO SubjectPublicKeyInfo;

			// Token: 0x04003620 RID: 13856
			internal CAPIBase.CRYPT_BIT_BLOB IssuerUniqueId;

			// Token: 0x04003621 RID: 13857
			internal CAPIBase.CRYPT_BIT_BLOB SubjectUniqueId;

			// Token: 0x04003622 RID: 13858
			internal uint cExtension;

			// Token: 0x04003623 RID: 13859
			internal IntPtr rgExtension;
		}

		// Token: 0x0200083F RID: 2111
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_KEY_USAGE_RESTRICTION_INFO
		{
			// Token: 0x04003624 RID: 13860
			internal uint cCertPolicyId;

			// Token: 0x04003625 RID: 13861
			internal IntPtr rgCertPolicyId;

			// Token: 0x04003626 RID: 13862
			internal CAPIBase.CRYPT_BIT_BLOB RestrictedKeyUsage;
		}

		// Token: 0x02000840 RID: 2112
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_NAME_INFO
		{
			// Token: 0x04003627 RID: 13863
			internal uint cRDN;

			// Token: 0x04003628 RID: 13864
			internal IntPtr rgRDN;
		}

		// Token: 0x02000841 RID: 2113
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_NAME_VALUE
		{
			// Token: 0x04003629 RID: 13865
			internal uint dwValueType;

			// Token: 0x0400362A RID: 13866
			internal CAPIBase.CRYPTOAPI_BLOB Value;
		}

		// Token: 0x02000842 RID: 2114
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_OTHER_NAME
		{
			// Token: 0x0400362B RID: 13867
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszObjId;

			// Token: 0x0400362C RID: 13868
			internal CAPIBase.CRYPTOAPI_BLOB Value;
		}

		// Token: 0x02000843 RID: 2115
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_POLICY_ID
		{
			// Token: 0x0400362D RID: 13869
			internal uint cCertPolicyElementId;

			// Token: 0x0400362E RID: 13870
			internal IntPtr rgpszCertPolicyElementId;
		}

		// Token: 0x02000844 RID: 2116
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_POLICIES_INFO
		{
			// Token: 0x0400362F RID: 13871
			internal uint cPolicyInfo;

			// Token: 0x04003630 RID: 13872
			internal IntPtr rgPolicyInfo;
		}

		// Token: 0x02000845 RID: 2117
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_POLICY_INFO
		{
			// Token: 0x04003631 RID: 13873
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszPolicyIdentifier;

			// Token: 0x04003632 RID: 13874
			internal uint cPolicyQualifier;

			// Token: 0x04003633 RID: 13875
			internal IntPtr rgPolicyQualifier;
		}

		// Token: 0x02000846 RID: 2118
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_POLICY_QUALIFIER_INFO
		{
			// Token: 0x04003634 RID: 13876
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszPolicyQualifierId;

			// Token: 0x04003635 RID: 13877
			private CAPIBase.CRYPTOAPI_BLOB Qualifier;
		}

		// Token: 0x02000847 RID: 2119
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_PUBLIC_KEY_INFO
		{
			// Token: 0x04003636 RID: 13878
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER Algorithm;

			// Token: 0x04003637 RID: 13879
			internal CAPIBase.CRYPT_BIT_BLOB PublicKey;
		}

		// Token: 0x02000848 RID: 2120
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_PUBLIC_KEY_INFO2
		{
			// Token: 0x04003638 RID: 13880
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER2 Algorithm;

			// Token: 0x04003639 RID: 13881
			internal CAPIBase.CRYPT_BIT_BLOB PublicKey;
		}

		// Token: 0x02000849 RID: 2121
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_RDN
		{
			// Token: 0x0400363A RID: 13882
			internal uint cRDNAttr;

			// Token: 0x0400363B RID: 13883
			internal IntPtr rgRDNAttr;
		}

		// Token: 0x0200084A RID: 2122
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_RDN_ATTR
		{
			// Token: 0x0400363C RID: 13884
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszObjId;

			// Token: 0x0400363D RID: 13885
			internal uint dwValueType;

			// Token: 0x0400363E RID: 13886
			internal CAPIBase.CRYPTOAPI_BLOB Value;
		}

		// Token: 0x0200084B RID: 2123
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_SIMPLE_CHAIN
		{
			// Token: 0x0600452B RID: 17707 RVA: 0x0012094C File Offset: 0x0011EB4C
			internal CERT_SIMPLE_CHAIN(int size)
			{
				this.cbSize = (uint)size;
				this.dwErrorStatus = 0U;
				this.dwInfoStatus = 0U;
				this.cElement = 0U;
				this.rgpElement = IntPtr.Zero;
				this.pTrustListInfo = IntPtr.Zero;
				this.fHasRevocationFreshnessTime = 0U;
				this.dwRevocationFreshnessTime = 0U;
			}

			// Token: 0x0400363F RID: 13887
			internal uint cbSize;

			// Token: 0x04003640 RID: 13888
			internal uint dwErrorStatus;

			// Token: 0x04003641 RID: 13889
			internal uint dwInfoStatus;

			// Token: 0x04003642 RID: 13890
			internal uint cElement;

			// Token: 0x04003643 RID: 13891
			internal IntPtr rgpElement;

			// Token: 0x04003644 RID: 13892
			internal IntPtr pTrustListInfo;

			// Token: 0x04003645 RID: 13893
			internal uint fHasRevocationFreshnessTime;

			// Token: 0x04003646 RID: 13894
			internal uint dwRevocationFreshnessTime;
		}

		// Token: 0x0200084C RID: 2124
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_TEMPLATE_EXT
		{
			// Token: 0x04003647 RID: 13895
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszObjId;

			// Token: 0x04003648 RID: 13896
			internal uint dwMajorVersion;

			// Token: 0x04003649 RID: 13897
			private bool fMinorVersion;

			// Token: 0x0400364A RID: 13898
			private uint dwMinorVersion;
		}

		// Token: 0x0200084D RID: 2125
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_TRUST_STATUS
		{
			// Token: 0x0400364B RID: 13899
			internal uint dwErrorStatus;

			// Token: 0x0400364C RID: 13900
			internal uint dwInfoStatus;
		}

		// Token: 0x0200084E RID: 2126
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CERT_USAGE_MATCH
		{
			// Token: 0x0400364D RID: 13901
			internal uint dwType;

			// Token: 0x0400364E RID: 13902
			internal CAPIBase.CERT_ENHKEY_USAGE Usage;
		}

		// Token: 0x0200084F RID: 2127
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CMS_RECIPIENT_INFO
		{
			// Token: 0x0400364F RID: 13903
			internal uint dwRecipientChoice;

			// Token: 0x04003650 RID: 13904
			internal IntPtr pRecipientInfo;
		}

		// Token: 0x02000850 RID: 2128
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CMS_SIGNER_INFO
		{
			// Token: 0x04003651 RID: 13905
			internal uint dwVersion;

			// Token: 0x04003652 RID: 13906
			internal CAPIBase.CERT_ID SignerId;

			// Token: 0x04003653 RID: 13907
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER HashAlgorithm;

			// Token: 0x04003654 RID: 13908
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER HashEncryptionAlgorithm;

			// Token: 0x04003655 RID: 13909
			internal CAPIBase.CRYPTOAPI_BLOB EncryptedHash;

			// Token: 0x04003656 RID: 13910
			internal CAPIBase.CRYPT_ATTRIBUTES AuthAttrs;

			// Token: 0x04003657 RID: 13911
			internal CAPIBase.CRYPT_ATTRIBUTES UnauthAttrs;
		}

		// Token: 0x02000851 RID: 2129
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA
		{
			// Token: 0x0600452C RID: 17708 RVA: 0x00120999 File Offset: 0x0011EB99
			internal CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA(int size)
			{
				this.cbSize = (uint)size;
				this.dwSignerIndex = 0U;
				this.blob = default(CAPIBase.CRYPTOAPI_BLOB);
			}

			// Token: 0x04003658 RID: 13912
			internal uint cbSize;

			// Token: 0x04003659 RID: 13913
			internal uint dwSignerIndex;

			// Token: 0x0400365A RID: 13914
			internal CAPIBase.CRYPTOAPI_BLOB blob;
		}

		// Token: 0x02000852 RID: 2130
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CTRL_DECRYPT_PARA
		{
			// Token: 0x0600452D RID: 17709 RVA: 0x001209B5 File Offset: 0x0011EBB5
			internal CMSG_CTRL_DECRYPT_PARA(int size)
			{
				this.cbSize = (uint)size;
				this.hCryptProv = IntPtr.Zero;
				this.dwKeySpec = 0U;
				this.dwRecipientIndex = 0U;
			}

			// Token: 0x0400365B RID: 13915
			internal uint cbSize;

			// Token: 0x0400365C RID: 13916
			internal IntPtr hCryptProv;

			// Token: 0x0400365D RID: 13917
			internal uint dwKeySpec;

			// Token: 0x0400365E RID: 13918
			internal uint dwRecipientIndex;
		}

		// Token: 0x02000853 RID: 2131
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA
		{
			// Token: 0x0600452E RID: 17710 RVA: 0x001209D7 File Offset: 0x0011EBD7
			internal CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA(int size)
			{
				this.cbSize = (uint)size;
				this.dwSignerIndex = 0U;
				this.dwUnauthAttrIndex = 0U;
			}

			// Token: 0x0400365F RID: 13919
			internal uint cbSize;

			// Token: 0x04003660 RID: 13920
			internal uint dwSignerIndex;

			// Token: 0x04003661 RID: 13921
			internal uint dwUnauthAttrIndex;
		}

		// Token: 0x02000854 RID: 2132
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CTRL_KEY_TRANS_DECRYPT_PARA
		{
			// Token: 0x04003662 RID: 13922
			internal uint cbSize;

			// Token: 0x04003663 RID: 13923
			internal SafeCryptProvHandle hCryptProv;

			// Token: 0x04003664 RID: 13924
			internal uint dwKeySpec;

			// Token: 0x04003665 RID: 13925
			internal IntPtr pKeyTrans;

			// Token: 0x04003666 RID: 13926
			internal uint dwRecipientIndex;
		}

		// Token: 0x02000855 RID: 2133
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO
		{
			// Token: 0x04003667 RID: 13927
			internal uint cbSize;

			// Token: 0x04003668 RID: 13928
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;

			// Token: 0x04003669 RID: 13929
			internal IntPtr pvKeyEncryptionAuxInfo;

			// Token: 0x0400366A RID: 13930
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER KeyWrapAlgorithm;

			// Token: 0x0400366B RID: 13931
			internal IntPtr pvKeyWrapAuxInfo;

			// Token: 0x0400366C RID: 13932
			internal IntPtr hCryptProv;

			// Token: 0x0400366D RID: 13933
			internal uint dwKeySpec;

			// Token: 0x0400366E RID: 13934
			internal uint dwKeyChoice;

			// Token: 0x0400366F RID: 13935
			internal IntPtr pEphemeralAlgorithmOrSenderId;

			// Token: 0x04003670 RID: 13936
			internal CAPIBase.CRYPTOAPI_BLOB UserKeyingMaterial;

			// Token: 0x04003671 RID: 13937
			internal uint cRecipientEncryptedKeys;

			// Token: 0x04003672 RID: 13938
			internal IntPtr rgpRecipientEncryptedKeys;
		}

		// Token: 0x02000856 RID: 2134
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO
		{
			// Token: 0x04003673 RID: 13939
			internal uint cbSize;

			// Token: 0x04003674 RID: 13940
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;

			// Token: 0x04003675 RID: 13941
			internal IntPtr pvKeyEncryptionAuxInfo;

			// Token: 0x04003676 RID: 13942
			internal IntPtr hCryptProv;

			// Token: 0x04003677 RID: 13943
			internal CAPIBase.CRYPT_BIT_BLOB RecipientPublicKey;

			// Token: 0x04003678 RID: 13944
			internal CAPIBase.CERT_ID RecipientId;
		}

		// Token: 0x02000857 RID: 2135
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_RC2_AUX_INFO
		{
			// Token: 0x0600452F RID: 17711 RVA: 0x001209EE File Offset: 0x0011EBEE
			internal CMSG_RC2_AUX_INFO(int size)
			{
				this.cbSize = (uint)size;
				this.dwBitLen = 0U;
			}

			// Token: 0x04003679 RID: 13945
			internal uint cbSize;

			// Token: 0x0400367A RID: 13946
			internal uint dwBitLen;
		}

		// Token: 0x02000858 RID: 2136
		internal struct CMSG_RECIPIENT_ENCODE_INFO
		{
			// Token: 0x0400367B RID: 13947
			internal uint dwRecipientChoice;

			// Token: 0x0400367C RID: 13948
			internal IntPtr pRecipientInfo;
		}

		// Token: 0x02000859 RID: 2137
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_RECIPIENT_ENCRYPTED_KEY_ENCODE_INFO
		{
			// Token: 0x0400367D RID: 13949
			internal uint cbSize;

			// Token: 0x0400367E RID: 13950
			internal CAPIBase.CRYPT_BIT_BLOB RecipientPublicKey;

			// Token: 0x0400367F RID: 13951
			internal CAPIBase.CERT_ID RecipientId;

			// Token: 0x04003680 RID: 13952
			internal System.Runtime.InteropServices.ComTypes.FILETIME Date;

			// Token: 0x04003681 RID: 13953
			internal IntPtr pOtherAttr;
		}

		// Token: 0x0200085A RID: 2138
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_ENVELOPED_ENCODE_INFO
		{
			// Token: 0x06004530 RID: 17712 RVA: 0x00120A00 File Offset: 0x0011EC00
			internal CMSG_ENVELOPED_ENCODE_INFO(int size)
			{
				this.cbSize = (uint)size;
				this.hCryptProv = IntPtr.Zero;
				this.ContentEncryptionAlgorithm = default(CAPIBase.CRYPT_ALGORITHM_IDENTIFIER);
				this.pvEncryptionAuxInfo = IntPtr.Zero;
				this.cRecipients = 0U;
				this.rgpRecipients = IntPtr.Zero;
				this.rgCmsRecipients = IntPtr.Zero;
				this.cCertEncoded = 0U;
				this.rgCertEncoded = IntPtr.Zero;
				this.cCrlEncoded = 0U;
				this.rgCrlEncoded = IntPtr.Zero;
				this.cAttrCertEncoded = 0U;
				this.rgAttrCertEncoded = IntPtr.Zero;
				this.cUnprotectedAttr = 0U;
				this.rgUnprotectedAttr = IntPtr.Zero;
			}

			// Token: 0x04003682 RID: 13954
			internal uint cbSize;

			// Token: 0x04003683 RID: 13955
			internal IntPtr hCryptProv;

			// Token: 0x04003684 RID: 13956
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER ContentEncryptionAlgorithm;

			// Token: 0x04003685 RID: 13957
			internal IntPtr pvEncryptionAuxInfo;

			// Token: 0x04003686 RID: 13958
			internal uint cRecipients;

			// Token: 0x04003687 RID: 13959
			internal IntPtr rgpRecipients;

			// Token: 0x04003688 RID: 13960
			internal IntPtr rgCmsRecipients;

			// Token: 0x04003689 RID: 13961
			internal uint cCertEncoded;

			// Token: 0x0400368A RID: 13962
			internal IntPtr rgCertEncoded;

			// Token: 0x0400368B RID: 13963
			internal uint cCrlEncoded;

			// Token: 0x0400368C RID: 13964
			internal IntPtr rgCrlEncoded;

			// Token: 0x0400368D RID: 13965
			internal uint cAttrCertEncoded;

			// Token: 0x0400368E RID: 13966
			internal IntPtr rgAttrCertEncoded;

			// Token: 0x0400368F RID: 13967
			internal uint cUnprotectedAttr;

			// Token: 0x04003690 RID: 13968
			internal IntPtr rgUnprotectedAttr;
		}

		// Token: 0x0200085B RID: 2139
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CTRL_KEY_AGREE_DECRYPT_PARA
		{
			// Token: 0x06004531 RID: 17713 RVA: 0x00120A9B File Offset: 0x0011EC9B
			internal CMSG_CTRL_KEY_AGREE_DECRYPT_PARA(int size)
			{
				this.cbSize = (uint)size;
				this.hCryptProv = IntPtr.Zero;
				this.dwKeySpec = 0U;
				this.pKeyAgree = IntPtr.Zero;
				this.dwRecipientIndex = 0U;
				this.dwRecipientEncryptedKeyIndex = 0U;
				this.OriginatorPublicKey = default(CAPIBase.CRYPT_BIT_BLOB);
			}

			// Token: 0x04003691 RID: 13969
			internal uint cbSize;

			// Token: 0x04003692 RID: 13970
			internal IntPtr hCryptProv;

			// Token: 0x04003693 RID: 13971
			internal uint dwKeySpec;

			// Token: 0x04003694 RID: 13972
			internal IntPtr pKeyAgree;

			// Token: 0x04003695 RID: 13973
			internal uint dwRecipientIndex;

			// Token: 0x04003696 RID: 13974
			internal uint dwRecipientEncryptedKeyIndex;

			// Token: 0x04003697 RID: 13975
			internal CAPIBase.CRYPT_BIT_BLOB OriginatorPublicKey;
		}

		// Token: 0x0200085C RID: 2140
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_KEY_AGREE_RECIPIENT_INFO
		{
			// Token: 0x04003698 RID: 13976
			internal uint dwVersion;

			// Token: 0x04003699 RID: 13977
			internal uint dwOriginatorChoice;
		}

		// Token: 0x0200085D RID: 2141
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO
		{
			// Token: 0x0400369A RID: 13978
			internal uint dwVersion;

			// Token: 0x0400369B RID: 13979
			internal uint dwOriginatorChoice;

			// Token: 0x0400369C RID: 13980
			internal CAPIBase.CERT_ID OriginatorCertId;

			// Token: 0x0400369D RID: 13981
			internal IntPtr Padding;

			// Token: 0x0400369E RID: 13982
			internal CAPIBase.CRYPTOAPI_BLOB UserKeyingMaterial;

			// Token: 0x0400369F RID: 13983
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;

			// Token: 0x040036A0 RID: 13984
			internal uint cRecipientEncryptedKeys;

			// Token: 0x040036A1 RID: 13985
			internal IntPtr rgpRecipientEncryptedKeys;
		}

		// Token: 0x0200085E RID: 2142
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO
		{
			// Token: 0x040036A2 RID: 13986
			internal uint dwVersion;

			// Token: 0x040036A3 RID: 13987
			internal uint dwOriginatorChoice;

			// Token: 0x040036A4 RID: 13988
			internal CAPIBase.CERT_PUBLIC_KEY_INFO OriginatorPublicKeyInfo;

			// Token: 0x040036A5 RID: 13989
			internal CAPIBase.CRYPTOAPI_BLOB UserKeyingMaterial;

			// Token: 0x040036A6 RID: 13990
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;

			// Token: 0x040036A7 RID: 13991
			internal uint cRecipientEncryptedKeys;

			// Token: 0x040036A8 RID: 13992
			internal IntPtr rgpRecipientEncryptedKeys;
		}

		// Token: 0x0200085F RID: 2143
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_RECIPIENT_ENCRYPTED_KEY_INFO
		{
			// Token: 0x040036A9 RID: 13993
			internal CAPIBase.CERT_ID RecipientId;

			// Token: 0x040036AA RID: 13994
			internal CAPIBase.CRYPTOAPI_BLOB EncryptedKey;

			// Token: 0x040036AB RID: 13995
			internal System.Runtime.InteropServices.ComTypes.FILETIME Date;

			// Token: 0x040036AC RID: 13996
			internal IntPtr pOtherAttr;
		}

		// Token: 0x02000860 RID: 2144
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_CTRL_VERIFY_SIGNATURE_EX_PARA
		{
			// Token: 0x06004532 RID: 17714 RVA: 0x00120ADB File Offset: 0x0011ECDB
			internal CMSG_CTRL_VERIFY_SIGNATURE_EX_PARA(int size)
			{
				this.cbSize = (uint)size;
				this.hCryptProv = IntPtr.Zero;
				this.dwSignerIndex = 0U;
				this.dwSignerType = 0U;
				this.pvSigner = IntPtr.Zero;
			}

			// Token: 0x040036AD RID: 13997
			internal uint cbSize;

			// Token: 0x040036AE RID: 13998
			internal IntPtr hCryptProv;

			// Token: 0x040036AF RID: 13999
			internal uint dwSignerIndex;

			// Token: 0x040036B0 RID: 14000
			internal uint dwSignerType;

			// Token: 0x040036B1 RID: 14001
			internal IntPtr pvSigner;
		}

		// Token: 0x02000861 RID: 2145
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_KEY_TRANS_RECIPIENT_INFO
		{
			// Token: 0x040036B2 RID: 14002
			internal uint dwVersion;

			// Token: 0x040036B3 RID: 14003
			internal CAPIBase.CERT_ID RecipientId;

			// Token: 0x040036B4 RID: 14004
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER KeyEncryptionAlgorithm;

			// Token: 0x040036B5 RID: 14005
			internal CAPIBase.CRYPTOAPI_BLOB EncryptedKey;
		}

		// Token: 0x02000862 RID: 2146
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_SIGNED_ENCODE_INFO
		{
			// Token: 0x06004533 RID: 17715 RVA: 0x00120B08 File Offset: 0x0011ED08
			internal CMSG_SIGNED_ENCODE_INFO(int size)
			{
				this.cbSize = (uint)size;
				this.cSigners = 0U;
				this.rgSigners = IntPtr.Zero;
				this.cCertEncoded = 0U;
				this.rgCertEncoded = IntPtr.Zero;
				this.cCrlEncoded = 0U;
				this.rgCrlEncoded = IntPtr.Zero;
				this.cAttrCertEncoded = 0U;
				this.rgAttrCertEncoded = IntPtr.Zero;
			}

			// Token: 0x040036B6 RID: 14006
			internal uint cbSize;

			// Token: 0x040036B7 RID: 14007
			internal uint cSigners;

			// Token: 0x040036B8 RID: 14008
			internal IntPtr rgSigners;

			// Token: 0x040036B9 RID: 14009
			internal uint cCertEncoded;

			// Token: 0x040036BA RID: 14010
			internal IntPtr rgCertEncoded;

			// Token: 0x040036BB RID: 14011
			internal uint cCrlEncoded;

			// Token: 0x040036BC RID: 14012
			internal IntPtr rgCrlEncoded;

			// Token: 0x040036BD RID: 14013
			internal uint cAttrCertEncoded;

			// Token: 0x040036BE RID: 14014
			internal IntPtr rgAttrCertEncoded;
		}

		// Token: 0x02000863 RID: 2147
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_SIGNER_ENCODE_INFO
		{
			// Token: 0x06004534 RID: 17716
			[DllImport("kernel32.dll", SetLastError = true)]
			internal static extern IntPtr LocalFree(IntPtr hMem);

			// Token: 0x06004535 RID: 17717
			[DllImport("advapi32.dll", SetLastError = true)]
			internal static extern bool CryptReleaseContext([In] IntPtr hProv, [In] uint dwFlags);

			// Token: 0x06004536 RID: 17718 RVA: 0x00120B64 File Offset: 0x0011ED64
			internal CMSG_SIGNER_ENCODE_INFO(int size)
			{
				this.cbSize = (uint)size;
				this.pCertInfo = IntPtr.Zero;
				this.hCryptProv = IntPtr.Zero;
				this.dwKeySpec = 0U;
				this.HashAlgorithm = default(CAPIBase.CRYPT_ALGORITHM_IDENTIFIER);
				this.pvHashAuxInfo = IntPtr.Zero;
				this.cAuthAttr = 0U;
				this.rgAuthAttr = IntPtr.Zero;
				this.cUnauthAttr = 0U;
				this.rgUnauthAttr = IntPtr.Zero;
				this.SignerId = default(CAPIBase.CERT_ID);
				this.HashEncryptionAlgorithm = default(CAPIBase.CRYPT_ALGORITHM_IDENTIFIER);
				this.pvHashEncryptionAuxInfo = IntPtr.Zero;
			}

			// Token: 0x06004537 RID: 17719 RVA: 0x00120BF4 File Offset: 0x0011EDF4
			internal void Dispose()
			{
				if (this.hCryptProv != IntPtr.Zero)
				{
					CAPIBase.CMSG_SIGNER_ENCODE_INFO.CryptReleaseContext(this.hCryptProv, 0U);
				}
				if (this.SignerId.Value.KeyId.pbData != IntPtr.Zero)
				{
					CAPIBase.CMSG_SIGNER_ENCODE_INFO.LocalFree(this.SignerId.Value.KeyId.pbData);
				}
				if (this.rgAuthAttr != IntPtr.Zero)
				{
					CAPIBase.CMSG_SIGNER_ENCODE_INFO.LocalFree(this.rgAuthAttr);
				}
				if (this.rgUnauthAttr != IntPtr.Zero)
				{
					CAPIBase.CMSG_SIGNER_ENCODE_INFO.LocalFree(this.rgUnauthAttr);
				}
			}

			// Token: 0x040036BF RID: 14015
			internal uint cbSize;

			// Token: 0x040036C0 RID: 14016
			internal IntPtr pCertInfo;

			// Token: 0x040036C1 RID: 14017
			internal IntPtr hCryptProv;

			// Token: 0x040036C2 RID: 14018
			internal uint dwKeySpec;

			// Token: 0x040036C3 RID: 14019
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER HashAlgorithm;

			// Token: 0x040036C4 RID: 14020
			internal IntPtr pvHashAuxInfo;

			// Token: 0x040036C5 RID: 14021
			internal uint cAuthAttr;

			// Token: 0x040036C6 RID: 14022
			internal IntPtr rgAuthAttr;

			// Token: 0x040036C7 RID: 14023
			internal uint cUnauthAttr;

			// Token: 0x040036C8 RID: 14024
			internal IntPtr rgUnauthAttr;

			// Token: 0x040036C9 RID: 14025
			internal CAPIBase.CERT_ID SignerId;

			// Token: 0x040036CA RID: 14026
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER HashEncryptionAlgorithm;

			// Token: 0x040036CB RID: 14027
			internal IntPtr pvHashEncryptionAuxInfo;
		}

		// Token: 0x02000864 RID: 2148
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CMSG_SIGNER_INFO
		{
			// Token: 0x040036CC RID: 14028
			internal uint dwVersion;

			// Token: 0x040036CD RID: 14029
			internal CAPIBase.CRYPTOAPI_BLOB Issuer;

			// Token: 0x040036CE RID: 14030
			internal CAPIBase.CRYPTOAPI_BLOB SerialNumber;

			// Token: 0x040036CF RID: 14031
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER HashAlgorithm;

			// Token: 0x040036D0 RID: 14032
			internal CAPIBase.CRYPT_ALGORITHM_IDENTIFIER HashEncryptionAlgorithm;

			// Token: 0x040036D1 RID: 14033
			internal CAPIBase.CRYPTOAPI_BLOB EncryptedHash;

			// Token: 0x040036D2 RID: 14034
			internal CAPIBase.CRYPT_ATTRIBUTES AuthAttrs;

			// Token: 0x040036D3 RID: 14035
			internal CAPIBase.CRYPT_ATTRIBUTES UnauthAttrs;
		}

		// Token: 0x02000865 RID: 2149
		// (Invoke) Token: 0x06004539 RID: 17721
		internal delegate bool PFN_CMSG_STREAM_OUTPUT(IntPtr pvArg, IntPtr pbData, uint cbData, bool fFinal);

		// Token: 0x02000866 RID: 2150
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal class CMSG_STREAM_INFO
		{
			// Token: 0x0600453C RID: 17724 RVA: 0x00120C98 File Offset: 0x0011EE98
			internal CMSG_STREAM_INFO(uint cbContent, CAPIBase.PFN_CMSG_STREAM_OUTPUT pfnStreamOutput, IntPtr pvArg)
			{
				this.cbContent = cbContent;
				this.pfnStreamOutput = pfnStreamOutput;
				this.pvArg = pvArg;
			}

			// Token: 0x040036D4 RID: 14036
			internal uint cbContent;

			// Token: 0x040036D5 RID: 14037
			internal CAPIBase.PFN_CMSG_STREAM_OUTPUT pfnStreamOutput;

			// Token: 0x040036D6 RID: 14038
			internal IntPtr pvArg;
		}

		// Token: 0x02000867 RID: 2151
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_ALGORITHM_IDENTIFIER
		{
			// Token: 0x040036D7 RID: 14039
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszObjId;

			// Token: 0x040036D8 RID: 14040
			internal CAPIBase.CRYPTOAPI_BLOB Parameters;
		}

		// Token: 0x02000868 RID: 2152
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_ALGORITHM_IDENTIFIER2
		{
			// Token: 0x040036D9 RID: 14041
			internal IntPtr pszObjId;

			// Token: 0x040036DA RID: 14042
			internal CAPIBase.CRYPTOAPI_BLOB Parameters;
		}

		// Token: 0x02000869 RID: 2153
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_ATTRIBUTE
		{
			// Token: 0x040036DB RID: 14043
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszObjId;

			// Token: 0x040036DC RID: 14044
			internal uint cValue;

			// Token: 0x040036DD RID: 14045
			internal IntPtr rgValue;
		}

		// Token: 0x0200086A RID: 2154
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_ATTRIBUTES
		{
			// Token: 0x040036DE RID: 14046
			internal uint cAttr;

			// Token: 0x040036DF RID: 14047
			internal IntPtr rgAttr;
		}

		// Token: 0x0200086B RID: 2155
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_ATTRIBUTE_TYPE_VALUE
		{
			// Token: 0x040036E0 RID: 14048
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszObjId;

			// Token: 0x040036E1 RID: 14049
			internal CAPIBase.CRYPTOAPI_BLOB Value;
		}

		// Token: 0x0200086C RID: 2156
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_BIT_BLOB
		{
			// Token: 0x040036E2 RID: 14050
			internal uint cbData;

			// Token: 0x040036E3 RID: 14051
			internal IntPtr pbData;

			// Token: 0x040036E4 RID: 14052
			internal uint cUnusedBits;
		}

		// Token: 0x0200086D RID: 2157
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_KEY_PROV_INFO
		{
			// Token: 0x040036E5 RID: 14053
			internal string pwszContainerName;

			// Token: 0x040036E6 RID: 14054
			internal string pwszProvName;

			// Token: 0x040036E7 RID: 14055
			internal uint dwProvType;

			// Token: 0x040036E8 RID: 14056
			internal uint dwFlags;

			// Token: 0x040036E9 RID: 14057
			internal uint cProvParam;

			// Token: 0x040036EA RID: 14058
			internal IntPtr rgProvParam;

			// Token: 0x040036EB RID: 14059
			internal uint dwKeySpec;
		}

		// Token: 0x0200086E RID: 2158
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_OID_INFO
		{
			// Token: 0x0600453D RID: 17725 RVA: 0x00120CB5 File Offset: 0x0011EEB5
			internal CRYPT_OID_INFO(int size)
			{
				this.cbSize = (uint)size;
				this.pszOID = null;
				this.pwszName = null;
				this.dwGroupId = 0U;
				this.Algid = 0U;
				this.ExtraInfo = default(CAPIBase.CRYPTOAPI_BLOB);
			}

			// Token: 0x040036EC RID: 14060
			internal uint cbSize;

			// Token: 0x040036ED RID: 14061
			[MarshalAs(UnmanagedType.LPStr)]
			internal string pszOID;

			// Token: 0x040036EE RID: 14062
			internal string pwszName;

			// Token: 0x040036EF RID: 14063
			internal uint dwGroupId;

			// Token: 0x040036F0 RID: 14064
			internal uint Algid;

			// Token: 0x040036F1 RID: 14065
			internal CAPIBase.CRYPTOAPI_BLOB ExtraInfo;
		}

		// Token: 0x0200086F RID: 2159
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPT_RC2_CBC_PARAMETERS
		{
			// Token: 0x040036F2 RID: 14066
			internal uint dwVersion;

			// Token: 0x040036F3 RID: 14067
			internal bool fIV;

			// Token: 0x040036F4 RID: 14068
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			internal byte[] rgbIV;
		}

		// Token: 0x02000870 RID: 2160
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct CRYPTOAPI_BLOB
		{
			// Token: 0x040036F5 RID: 14069
			internal uint cbData;

			// Token: 0x040036F6 RID: 14070
			internal IntPtr pbData;
		}

		// Token: 0x02000871 RID: 2161
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct DSSPUBKEY
		{
			// Token: 0x040036F7 RID: 14071
			internal uint magic;

			// Token: 0x040036F8 RID: 14072
			internal uint bitlen;
		}

		// Token: 0x02000872 RID: 2162
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct KEY_USAGE_STRUCT
		{
			// Token: 0x0600453E RID: 17726 RVA: 0x00120CE6 File Offset: 0x0011EEE6
			internal KEY_USAGE_STRUCT(string pwszKeyUsage, uint dwKeyUsageBit)
			{
				this.pwszKeyUsage = pwszKeyUsage;
				this.dwKeyUsageBit = dwKeyUsageBit;
			}

			// Token: 0x040036F9 RID: 14073
			internal string pwszKeyUsage;

			// Token: 0x040036FA RID: 14074
			internal uint dwKeyUsageBit;
		}

		// Token: 0x02000873 RID: 2163
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct PROV_ENUMALGS_EX
		{
			// Token: 0x040036FB RID: 14075
			internal uint aiAlgid;

			// Token: 0x040036FC RID: 14076
			internal uint dwDefaultLen;

			// Token: 0x040036FD RID: 14077
			internal uint dwMinLen;

			// Token: 0x040036FE RID: 14078
			internal uint dwMaxLen;

			// Token: 0x040036FF RID: 14079
			internal uint dwProtocols;

			// Token: 0x04003700 RID: 14080
			internal uint dwNameLen;

			// Token: 0x04003701 RID: 14081
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
			internal byte[] szName;

			// Token: 0x04003702 RID: 14082
			internal uint dwLongNameLen;

			// Token: 0x04003703 RID: 14083
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
			internal byte[] szLongName;
		}

		// Token: 0x02000874 RID: 2164
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct RSAPUBKEY
		{
			// Token: 0x04003704 RID: 14084
			internal uint magic;

			// Token: 0x04003705 RID: 14085
			internal uint bitlen;

			// Token: 0x04003706 RID: 14086
			internal uint pubexp;
		}
	}
}
