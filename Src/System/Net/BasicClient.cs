using System;
using System.Globalization;
using System.Text;

namespace System.Net
{
	// Token: 0x02000195 RID: 405
	internal class BasicClient : IAuthenticationModule
	{
		// Token: 0x06000FA6 RID: 4006 RVA: 0x00051F58 File Offset: 0x00050158
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || credentials is SystemNetworkCredential)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null || httpWebRequest.ChallengedUri == null)
			{
				return null;
			}
			int num = AuthenticationManager.FindSubstringNotInQuotes(challenge, BasicClient.Signature);
			if (num < 0)
			{
				return null;
			}
			return this.Lookup(httpWebRequest, credentials);
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00051FA6 File Offset: 0x000501A6
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00051FAC File Offset: 0x000501AC
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || credentials is SystemNetworkCredential)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			return this.Lookup(httpWebRequest, credentials);
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00051FDA File Offset: 0x000501DA
		public string AuthenticationType
		{
			get
			{
				return "Basic";
			}
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00051FE4 File Offset: 0x000501E4
		private Authorization Lookup(HttpWebRequest httpWebRequest, ICredentials credentials)
		{
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.ChallengedUri, BasicClient.Signature);
			if (credential == null)
			{
				return null;
			}
			ICredentialPolicy credentialPolicy = AuthenticationManager.CredentialPolicy;
			if (credentialPolicy != null && !credentialPolicy.ShouldSendCredential(httpWebRequest.ChallengedUri, httpWebRequest, credential, this))
			{
				return null;
			}
			string text = credential.InternalGetUserName();
			string text2 = credential.InternalGetDomain();
			if (ValidationHelper.IsBlankString(text))
			{
				return null;
			}
			string text3 = ((!ValidationHelper.IsBlankString(text2)) ? (text2 + "\\") : "") + text + ":" + credential.InternalGetPassword();
			byte[] array = BasicClient.EncodingRightGetBytes(text3);
			string text4 = "Basic " + Convert.ToBase64String(array);
			return new Authorization(text4, true);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00052090 File Offset: 0x00050290
		internal static byte[] EncodingRightGetBytes(string rawString)
		{
			byte[] bytes = Encoding.Default.GetBytes(rawString);
			string @string = Encoding.Default.GetString(bytes);
			if (string.Compare(rawString, @string, StringComparison.Ordinal) != 0)
			{
				throw ExceptionHelper.MethodNotSupportedException;
			}
			return bytes;
		}

		// Token: 0x040012CB RID: 4811
		internal const string AuthType = "Basic";

		// Token: 0x040012CC RID: 4812
		internal static string Signature = "Basic".ToLower(CultureInfo.InvariantCulture);

		// Token: 0x040012CD RID: 4813
		internal static int SignatureSize = BasicClient.Signature.Length;
	}
}
