using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Specifies the network resource access that is granted to code.</summary>
	// Token: 0x0200035C RID: 860
	[ComVisible(true)]
	[Serializable]
	public class CodeConnectAccess
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.CodeConnectAccess" /> class.</summary>
		/// <param name="allowScheme">The URI scheme represented by the current instance.</param>
		/// <param name="allowPort">The port represented by the current instance.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowScheme" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="allowScheme" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="allowScheme" /> contains characters that are not permitted in schemes.  
		/// -or-  
		/// <paramref name="allowPort" /> is less than 0.  
		/// -or-  
		/// <paramref name="allowPort" /> is greater than 65,535.</exception>
		// Token: 0x06002AA2 RID: 10914 RVA: 0x0009EA1D File Offset: 0x0009CC1D
		public CodeConnectAccess(string allowScheme, int allowPort)
		{
			if (!CodeConnectAccess.IsValidScheme(allowScheme))
			{
				throw new ArgumentOutOfRangeException("allowScheme");
			}
			this.SetCodeConnectAccess(allowScheme.ToLower(CultureInfo.InvariantCulture), allowPort);
		}

		/// <summary>Returns a <see cref="T:System.Security.Policy.CodeConnectAccess" /> instance that represents access to the specified port using the code's scheme of origin.</summary>
		/// <param name="allowPort">The port represented by the returned instance.</param>
		/// <returns>A <see cref="T:System.Security.Policy.CodeConnectAccess" /> instance for the specified port.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowPort" /> is less than 0.  
		/// -or-  
		/// <paramref name="allowPort" /> is greater than 65,535.</exception>
		// Token: 0x06002AA3 RID: 10915 RVA: 0x0009EA4C File Offset: 0x0009CC4C
		public static CodeConnectAccess CreateOriginSchemeAccess(int allowPort)
		{
			CodeConnectAccess codeConnectAccess = new CodeConnectAccess();
			codeConnectAccess.SetCodeConnectAccess(CodeConnectAccess.OriginScheme, allowPort);
			return codeConnectAccess;
		}

		/// <summary>Returns a <see cref="T:System.Security.Policy.CodeConnectAccess" /> instance that represents access to the specified port using any scheme.</summary>
		/// <param name="allowPort">The port represented by the returned instance.</param>
		/// <returns>A <see cref="T:System.Security.Policy.CodeConnectAccess" /> instance for the specified port.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="allowPort" /> is less than 0.  
		/// -or-  
		/// <paramref name="allowPort" /> is greater than 65,535.</exception>
		// Token: 0x06002AA4 RID: 10916 RVA: 0x0009EA6C File Offset: 0x0009CC6C
		public static CodeConnectAccess CreateAnySchemeAccess(int allowPort)
		{
			CodeConnectAccess codeConnectAccess = new CodeConnectAccess();
			codeConnectAccess.SetCodeConnectAccess(CodeConnectAccess.AnyScheme, allowPort);
			return codeConnectAccess;
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x0009EA8C File Offset: 0x0009CC8C
		private CodeConnectAccess()
		{
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x0009EA94 File Offset: 0x0009CC94
		private void SetCodeConnectAccess(string lowerCaseScheme, int allowPort)
		{
			this._LowerCaseScheme = lowerCaseScheme;
			if (allowPort == CodeConnectAccess.DefaultPort)
			{
				this._LowerCasePort = "$default";
			}
			else if (allowPort == CodeConnectAccess.OriginPort)
			{
				this._LowerCasePort = "$origin";
			}
			else
			{
				if (allowPort < 0 || allowPort > 65535)
				{
					throw new ArgumentOutOfRangeException("allowPort");
				}
				this._LowerCasePort = allowPort.ToString(CultureInfo.InvariantCulture);
			}
			this._IntPort = allowPort;
		}

		/// <summary>Gets the URI scheme represented by the current instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that identifies a URI scheme, converted to lowercase.</returns>
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06002AA7 RID: 10919 RVA: 0x0009EB02 File Offset: 0x0009CD02
		public string Scheme
		{
			get
			{
				return this._LowerCaseScheme;
			}
		}

		/// <summary>Gets the port represented by the current instance.</summary>
		/// <returns>A <see cref="T:System.Int32" /> value that identifies a computer port used in conjunction with the <see cref="P:System.Security.Policy.CodeConnectAccess.Scheme" /> property.</returns>
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x0009EB0A File Offset: 0x0009CD0A
		public int Port
		{
			get
			{
				return this._IntPort;
			}
		}

		/// <summary>Returns a value indicating whether two <see cref="T:System.Security.Policy.CodeConnectAccess" /> objects represent the same scheme and port.</summary>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.CodeConnectAccess" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the two objects represent the same scheme and port; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002AA9 RID: 10921 RVA: 0x0009EB14 File Offset: 0x0009CD14
		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			CodeConnectAccess codeConnectAccess = o as CodeConnectAccess;
			return codeConnectAccess != null && this.Scheme == codeConnectAccess.Scheme && this.Port == codeConnectAccess.Port;
		}

		/// <summary>Serves as a hash function for a particular type.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06002AAA RID: 10922 RVA: 0x0009EB58 File Offset: 0x0009CD58
		public override int GetHashCode()
		{
			return this.Scheme.GetHashCode() + this.Port.GetHashCode();
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x0009EB80 File Offset: 0x0009CD80
		internal CodeConnectAccess(string allowScheme, string allowPort)
		{
			if (allowScheme == null || allowScheme.Length == 0)
			{
				throw new ArgumentNullException("allowScheme");
			}
			if (allowPort == null || allowPort.Length == 0)
			{
				throw new ArgumentNullException("allowPort");
			}
			this._LowerCaseScheme = allowScheme.ToLower(CultureInfo.InvariantCulture);
			if (this._LowerCaseScheme == CodeConnectAccess.OriginScheme)
			{
				this._LowerCaseScheme = CodeConnectAccess.OriginScheme;
			}
			else if (this._LowerCaseScheme == CodeConnectAccess.AnyScheme)
			{
				this._LowerCaseScheme = CodeConnectAccess.AnyScheme;
			}
			else if (!CodeConnectAccess.IsValidScheme(this._LowerCaseScheme))
			{
				throw new ArgumentOutOfRangeException("allowScheme");
			}
			this._LowerCasePort = allowPort.ToLower(CultureInfo.InvariantCulture);
			if (this._LowerCasePort == "$default")
			{
				this._IntPort = CodeConnectAccess.DefaultPort;
				return;
			}
			if (this._LowerCasePort == "$origin")
			{
				this._IntPort = CodeConnectAccess.OriginPort;
				return;
			}
			this._IntPort = int.Parse(allowPort, CultureInfo.InvariantCulture);
			if (this._IntPort < 0 || this._IntPort > 65535)
			{
				throw new ArgumentOutOfRangeException("allowPort");
			}
			this._LowerCasePort = this._IntPort.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06002AAC RID: 10924 RVA: 0x0009ECBB File Offset: 0x0009CEBB
		internal bool IsOriginScheme
		{
			get
			{
				return this._LowerCaseScheme == CodeConnectAccess.OriginScheme;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06002AAD RID: 10925 RVA: 0x0009ECCA File Offset: 0x0009CECA
		internal bool IsAnyScheme
		{
			get
			{
				return this._LowerCaseScheme == CodeConnectAccess.AnyScheme;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x0009ECD9 File Offset: 0x0009CED9
		internal bool IsDefaultPort
		{
			get
			{
				return this.Port == CodeConnectAccess.DefaultPort;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06002AAF RID: 10927 RVA: 0x0009ECE8 File Offset: 0x0009CEE8
		internal bool IsOriginPort
		{
			get
			{
				return this.Port == CodeConnectAccess.OriginPort;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x0009ECF7 File Offset: 0x0009CEF7
		internal string StrPort
		{
			get
			{
				return this._LowerCasePort;
			}
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x0009ED00 File Offset: 0x0009CF00
		internal static bool IsValidScheme(string scheme)
		{
			if (scheme == null || scheme.Length == 0 || !CodeConnectAccess.IsAsciiLetter(scheme[0]))
			{
				return false;
			}
			for (int i = scheme.Length - 1; i > 0; i--)
			{
				if (!CodeConnectAccess.IsAsciiLetterOrDigit(scheme[i]) && scheme[i] != '+' && scheme[i] != '-' && scheme[i] != '.')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x0009ED6D File Offset: 0x0009CF6D
		private static bool IsAsciiLetterOrDigit(char character)
		{
			return CodeConnectAccess.IsAsciiLetter(character) || (character >= '0' && character <= '9');
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x0009ED88 File Offset: 0x0009CF88
		private static bool IsAsciiLetter(char character)
		{
			return (character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z');
		}

		// Token: 0x04001155 RID: 4437
		private string _LowerCaseScheme;

		// Token: 0x04001156 RID: 4438
		private string _LowerCasePort;

		// Token: 0x04001157 RID: 4439
		private int _IntPort;

		// Token: 0x04001158 RID: 4440
		private const string DefaultStr = "$default";

		// Token: 0x04001159 RID: 4441
		private const string OriginStr = "$origin";

		// Token: 0x0400115A RID: 4442
		internal const int NoPort = -1;

		// Token: 0x0400115B RID: 4443
		internal const int AnyPort = -2;

		/// <summary>Contains the value used to represent the default port.</summary>
		// Token: 0x0400115C RID: 4444
		public static readonly int DefaultPort = -3;

		/// <summary>Contains the value used to represent the port value in the URI where code originated.</summary>
		// Token: 0x0400115D RID: 4445
		public static readonly int OriginPort = -4;

		/// <summary>Contains the value used to represent the scheme in the URL where the code originated.</summary>
		// Token: 0x0400115E RID: 4446
		public static readonly string OriginScheme = "$origin";

		/// <summary>Contains the string value that represents the scheme wildcard.</summary>
		// Token: 0x0400115F RID: 4447
		public static readonly string AnyScheme = "*";
	}
}
