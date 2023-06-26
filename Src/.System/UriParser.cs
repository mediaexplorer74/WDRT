using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.Versioning;

namespace System
{
	/// <summary>Parses a new URI scheme. This is an abstract class.</summary>
	// Token: 0x0200003C RID: 60
	public abstract class UriParser
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0001109C File Offset: 0x0000F29C
		internal string SchemeName
		{
			get
			{
				return this.m_Scheme;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x000110A4 File Offset: 0x0000F2A4
		internal int DefaultPort
		{
			get
			{
				return this.m_Port;
			}
		}

		/// <summary>Constructs a default URI parser.</summary>
		// Token: 0x060002E6 RID: 742 RVA: 0x000110AC File Offset: 0x0000F2AC
		protected UriParser()
			: this(UriSyntaxFlags.MayHavePath)
		{
		}

		/// <summary>Invoked by a <see cref="T:System.Uri" /> constructor to get a <see cref="T:System.UriParser" /> instance</summary>
		/// <returns>A <see cref="T:System.UriParser" /> for the constructed <see cref="T:System.Uri" />.</returns>
		// Token: 0x060002E7 RID: 743 RVA: 0x000110B6 File Offset: 0x0000F2B6
		protected virtual UriParser OnNewUri()
		{
			return this;
		}

		/// <summary>Invoked by the Framework when a <see cref="T:System.UriParser" /> method is registered.</summary>
		/// <param name="schemeName">The scheme that is associated with this <see cref="T:System.UriParser" />.</param>
		/// <param name="defaultPort">The port number of the scheme.</param>
		// Token: 0x060002E8 RID: 744 RVA: 0x000110B9 File Offset: 0x0000F2B9
		protected virtual void OnRegister(string schemeName, int defaultPort)
		{
		}

		/// <summary>Initialize the state of the parser and validate the URI.</summary>
		/// <param name="uri">The T:System.Uri to validate.</param>
		/// <param name="parsingError">Validation errors, if any.</param>
		// Token: 0x060002E9 RID: 745 RVA: 0x000110BB File Offset: 0x0000F2BB
		protected virtual void InitializeAndValidate(Uri uri, out UriFormatException parsingError)
		{
			parsingError = uri.ParseMinimal();
		}

		/// <summary>Called by <see cref="T:System.Uri" /> constructors and <see cref="Overload:System.Uri.TryCreate" /> to resolve a relative URI.</summary>
		/// <param name="baseUri">A base URI.</param>
		/// <param name="relativeUri">A relative URI.</param>
		/// <param name="parsingError">Errors during the resolve process, if any.</param>
		/// <returns>The string of the resolved relative <see cref="T:System.Uri" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="baseUri" /> parameter is not an absolute <see cref="T:System.Uri" />  
		/// -or-
		///  <paramref name="baseUri" /> parameter requires user-driven parsing.</exception>
		// Token: 0x060002EA RID: 746 RVA: 0x000110C8 File Offset: 0x0000F2C8
		protected virtual string Resolve(Uri baseUri, Uri relativeUri, out UriFormatException parsingError)
		{
			if (baseUri.UserDrivenParsing)
			{
				throw new InvalidOperationException(SR.GetString("net_uri_UserDrivenParsing", new object[] { base.GetType().FullName }));
			}
			if (!baseUri.IsAbsoluteUri)
			{
				throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
			}
			string text = null;
			bool flag = false;
			Uri uri = Uri.ResolveHelper(baseUri, relativeUri, ref text, ref flag, out parsingError);
			if (parsingError != null)
			{
				return null;
			}
			if (uri != null)
			{
				return uri.OriginalString;
			}
			return text;
		}

		/// <summary>Determines whether <paramref name="baseUri" /> is a base URI for <paramref name="relativeUri" />.</summary>
		/// <param name="baseUri">The base URI.</param>
		/// <param name="relativeUri">The URI to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="baseUri" /> is a base URI for <paramref name="relativeUri" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002EB RID: 747 RVA: 0x00011141 File Offset: 0x0000F341
		protected virtual bool IsBaseOf(Uri baseUri, Uri relativeUri)
		{
			return baseUri.IsBaseOfHelper(relativeUri);
		}

		/// <summary>Gets the components from a URI.</summary>
		/// <param name="uri">The URI to parse.</param>
		/// <param name="components">The <see cref="T:System.UriComponents" /> to retrieve from <paramref name="uri" />.</param>
		/// <param name="format">One of the <see cref="T:System.UriFormat" /> values that controls how special characters are escaped.</param>
		/// <returns>A string that contains the components.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="uriFormat" /> is invalid.  
		/// -or-
		///  <paramref name="uriComponents" /> is not a combination of valid <see cref="T:System.UriComponents" /> values.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="uri" /> requires user-driven parsing  
		/// -or-
		///  <paramref name="uri" /> is not an absolute URI. Relative URIs cannot be used with this method.</exception>
		// Token: 0x060002EC RID: 748 RVA: 0x0001114C File Offset: 0x0000F34C
		protected virtual string GetComponents(Uri uri, UriComponents components, UriFormat format)
		{
			if ((components & UriComponents.SerializationInfoString) != (UriComponents)0 && components != UriComponents.SerializationInfoString)
			{
				throw new ArgumentOutOfRangeException("components", components, SR.GetString("net_uri_NotJustSerialization"));
			}
			if ((format & (UriFormat)(-4)) != (UriFormat)0)
			{
				throw new ArgumentOutOfRangeException("format");
			}
			if (uri.UserDrivenParsing)
			{
				throw new InvalidOperationException(SR.GetString("net_uri_UserDrivenParsing", new object[] { base.GetType().FullName }));
			}
			if (!uri.IsAbsoluteUri)
			{
				throw new InvalidOperationException(SR.GetString("net_uri_NotAbsolute"));
			}
			return uri.GetComponentsHelper(components, format);
		}

		/// <summary>Indicates whether a URI is well-formed.</summary>
		/// <param name="uri">The URI to check.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="uri" /> is well-formed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002ED RID: 749 RVA: 0x000111E2 File Offset: 0x0000F3E2
		protected virtual bool IsWellFormedOriginalString(Uri uri)
		{
			return uri.InternalIsWellFormedOriginalString();
		}

		/// <summary>Associates a scheme and port number with a <see cref="T:System.UriParser" />.</summary>
		/// <param name="uriParser">The URI parser to register.</param>
		/// <param name="schemeName">The name of the scheme that is associated with this parser.</param>
		/// <param name="defaultPort">The default port number for the specified scheme.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriParser" /> parameter is null  
		/// -or-
		///  <paramref name="schemeName" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="schemeName" /> parameter is not valid  
		/// -or-
		///  <paramref name="defaultPort" /> parameter is not valid. The <paramref name="defaultPort" /> parameter is less than -1 or greater than 65,534.</exception>
		// Token: 0x060002EE RID: 750 RVA: 0x000111EC File Offset: 0x0000F3EC
		public static void Register(UriParser uriParser, string schemeName, int defaultPort)
		{
			ExceptionHelper.InfrastructurePermission.Demand();
			if (uriParser == null)
			{
				throw new ArgumentNullException("uriParser");
			}
			if (schemeName == null)
			{
				throw new ArgumentNullException("schemeName");
			}
			if (schemeName.Length == 1)
			{
				throw new ArgumentOutOfRangeException("schemeName");
			}
			if (!Uri.CheckSchemeName(schemeName))
			{
				throw new ArgumentOutOfRangeException("schemeName");
			}
			if ((defaultPort >= 65535 || defaultPort < 0) && defaultPort != -1)
			{
				throw new ArgumentOutOfRangeException("defaultPort");
			}
			schemeName = schemeName.ToLower(CultureInfo.InvariantCulture);
			UriParser.FetchSyntax(uriParser, schemeName, defaultPort);
		}

		/// <summary>Indicates whether the parser for a scheme is registered.</summary>
		/// <param name="schemeName">The scheme name to check.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="schemeName" /> has been registered; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemeName" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="schemeName" /> parameter is not valid.</exception>
		// Token: 0x060002EF RID: 751 RVA: 0x00011278 File Offset: 0x0000F478
		public static bool IsKnownScheme(string schemeName)
		{
			if (schemeName == null)
			{
				throw new ArgumentNullException("schemeName");
			}
			if (!Uri.CheckSchemeName(schemeName))
			{
				throw new ArgumentOutOfRangeException("schemeName");
			}
			UriParser syntax = UriParser.GetSyntax(schemeName.ToLower(CultureInfo.InvariantCulture));
			return syntax != null && syntax.NotAny(UriSyntaxFlags.V1_UnknownUri);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000112C7 File Offset: 0x0000F4C7
		internal static bool ShouldUseLegacyV2Quirks
		{
			get
			{
				return UriParser.s_QuirksVersion <= UriParser.UriQuirksVersion.V2;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000112D4 File Offset: 0x0000F4D4
		internal static bool DontEnableStrictRFC3986ReservedCharacterSets
		{
			get
			{
				return LocalAppContextSwitches.DontEnableStrictRFC3986ReservedCharacterSets;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x000112DB File Offset: 0x0000F4DB
		internal static bool DontKeepUnicodeBidiFormattingCharacters
		{
			get
			{
				return LocalAppContextSwitches.DontKeepUnicodeBidiFormattingCharacters;
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000112E4 File Offset: 0x0000F4E4
		static UriParser()
		{
			UriParser.m_Table[UriParser.HttpUri.SchemeName] = UriParser.HttpUri;
			UriParser.HttpsUri = new UriParser.BuiltInUriParser("https", 443, UriParser.HttpUri.m_Flags);
			UriParser.m_Table[UriParser.HttpsUri.SchemeName] = UriParser.HttpsUri;
			UriParser.WsUri = new UriParser.BuiltInUriParser("ws", 80, UriParser.HttpSyntaxFlags);
			UriParser.m_Table[UriParser.WsUri.SchemeName] = UriParser.WsUri;
			UriParser.WssUri = new UriParser.BuiltInUriParser("wss", 443, UriParser.HttpSyntaxFlags);
			UriParser.m_Table[UriParser.WssUri.SchemeName] = UriParser.WssUri;
			UriParser.FtpUri = new UriParser.BuiltInUriParser("ftp", 21, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.FtpUri.SchemeName] = UriParser.FtpUri;
			UriParser.FileUri = new UriParser.BuiltInUriParser("file", -1, UriParser.FileSyntaxFlags);
			UriParser.m_Table[UriParser.FileUri.SchemeName] = UriParser.FileUri;
			UriParser.GopherUri = new UriParser.BuiltInUriParser("gopher", 70, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.GopherUri.SchemeName] = UriParser.GopherUri;
			UriParser.NntpUri = new UriParser.BuiltInUriParser("nntp", 119, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NntpUri.SchemeName] = UriParser.NntpUri;
			UriParser.NewsUri = new UriParser.BuiltInUriParser("news", -1, UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NewsUri.SchemeName] = UriParser.NewsUri;
			UriParser.MailToUri = new UriParser.BuiltInUriParser("mailto", 25, UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.MailToLikeUri | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.MailToUri.SchemeName] = UriParser.MailToUri;
			UriParser.UuidUri = new UriParser.BuiltInUriParser("uuid", -1, UriParser.NewsUri.m_Flags);
			UriParser.m_Table[UriParser.UuidUri.SchemeName] = UriParser.UuidUri;
			UriParser.TelnetUri = new UriParser.BuiltInUriParser("telnet", 23, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.TelnetUri.SchemeName] = UriParser.TelnetUri;
			UriParser.LdapUri = new UriParser.BuiltInUriParser("ldap", 389, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.LdapUri.SchemeName] = UriParser.LdapUri;
			UriParser.NetTcpUri = new UriParser.BuiltInUriParser("net.tcp", 808, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NetTcpUri.SchemeName] = UriParser.NetTcpUri;
			UriParser.NetPipeUri = new UriParser.BuiltInUriParser("net.pipe", -1, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NetPipeUri.SchemeName] = UriParser.NetPipeUri;
			UriParser.VsMacrosUri = new UriParser.BuiltInUriParser("vsmacros", -1, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.VsMacrosUri.SchemeName] = UriParser.VsMacrosUri;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0001168D File Offset: 0x0000F88D
		internal UriSyntaxFlags Flags
		{
			get
			{
				return this.m_Flags;
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00011695 File Offset: 0x0000F895
		internal bool NotAny(UriSyntaxFlags flags)
		{
			return this.IsFullMatch(flags, UriSyntaxFlags.None);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0001169F File Offset: 0x0000F89F
		internal bool InFact(UriSyntaxFlags flags)
		{
			return !this.IsFullMatch(flags, UriSyntaxFlags.None);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000116AC File Offset: 0x0000F8AC
		internal bool IsAllSet(UriSyntaxFlags flags)
		{
			return this.IsFullMatch(flags, flags);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000116B8 File Offset: 0x0000F8B8
		private bool IsFullMatch(UriSyntaxFlags flags, UriSyntaxFlags expected)
		{
			UriSyntaxFlags uriSyntaxFlags;
			if ((flags & UriSyntaxFlags.UnEscapeDotsAndSlashes) == UriSyntaxFlags.None || !this.m_UpdatableFlagsUsed)
			{
				uriSyntaxFlags = this.m_Flags;
			}
			else
			{
				uriSyntaxFlags = (this.m_Flags & ~UriSyntaxFlags.UnEscapeDotsAndSlashes) | this.m_UpdatableFlags;
			}
			return (uriSyntaxFlags & flags) == expected;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000116FD File Offset: 0x0000F8FD
		internal UriParser(UriSyntaxFlags flags)
		{
			this.m_Flags = flags;
			this.m_Scheme = string.Empty;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00011718 File Offset: 0x0000F918
		private static void FetchSyntax(UriParser syntax, string lwrCaseSchemeName, int defaultPort)
		{
			if (syntax.SchemeName.Length != 0)
			{
				throw new InvalidOperationException(SR.GetString("net_uri_NeedFreshParser", new object[] { syntax.SchemeName }));
			}
			Dictionary<string, UriParser> table = UriParser.m_Table;
			lock (table)
			{
				syntax.m_Flags &= ~UriSyntaxFlags.V1_UnknownUri;
				UriParser uriParser = null;
				UriParser.m_Table.TryGetValue(lwrCaseSchemeName, out uriParser);
				if (uriParser != null)
				{
					throw new InvalidOperationException(SR.GetString("net_uri_AlreadyRegistered", new object[] { uriParser.SchemeName }));
				}
				UriParser.m_TempTable.TryGetValue(syntax.SchemeName, out uriParser);
				if (uriParser != null)
				{
					lwrCaseSchemeName = uriParser.m_Scheme;
					UriParser.m_TempTable.Remove(lwrCaseSchemeName);
				}
				syntax.OnRegister(lwrCaseSchemeName, defaultPort);
				syntax.m_Scheme = lwrCaseSchemeName;
				syntax.CheckSetIsSimpleFlag();
				syntax.m_Port = defaultPort;
				UriParser.m_Table[syntax.SchemeName] = syntax;
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00011818 File Offset: 0x0000FA18
		internal static UriParser FindOrFetchAsUnknownV1Syntax(string lwrCaseScheme)
		{
			UriParser uriParser = null;
			UriParser.m_Table.TryGetValue(lwrCaseScheme, out uriParser);
			if (uriParser != null)
			{
				return uriParser;
			}
			UriParser.m_TempTable.TryGetValue(lwrCaseScheme, out uriParser);
			if (uriParser != null)
			{
				return uriParser;
			}
			Dictionary<string, UriParser> table = UriParser.m_Table;
			UriParser uriParser2;
			lock (table)
			{
				if (UriParser.m_TempTable.Count >= 512)
				{
					UriParser.m_TempTable = new Dictionary<string, UriParser>(25);
				}
				uriParser = new UriParser.BuiltInUriParser(lwrCaseScheme, -1, UriSyntaxFlags.OptionalAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.V1_UnknownUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
				UriParser.m_TempTable[lwrCaseScheme] = uriParser;
				uriParser2 = uriParser;
			}
			return uriParser2;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000118B4 File Offset: 0x0000FAB4
		internal static UriParser GetSyntax(string lwrCaseScheme)
		{
			UriParser uriParser = null;
			UriParser.m_Table.TryGetValue(lwrCaseScheme, out uriParser);
			if (uriParser == null)
			{
				UriParser.m_TempTable.TryGetValue(lwrCaseScheme, out uriParser);
			}
			return uriParser;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000118E3 File Offset: 0x0000FAE3
		internal bool IsSimple
		{
			get
			{
				return this.InFact(UriSyntaxFlags.SimpleUserSyntax);
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000118F0 File Offset: 0x0000FAF0
		internal void CheckSetIsSimpleFlag()
		{
			Type type = base.GetType();
			if (type == typeof(GenericUriParser) || type == typeof(HttpStyleUriParser) || type == typeof(FtpStyleUriParser) || type == typeof(FileStyleUriParser) || type == typeof(NewsStyleUriParser) || type == typeof(GopherStyleUriParser) || type == typeof(NetPipeStyleUriParser) || type == typeof(NetTcpStyleUriParser) || type == typeof(LdapStyleUriParser))
			{
				this.m_Flags |= UriSyntaxFlags.SimpleUserSyntax;
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000119BB File Offset: 0x0000FBBB
		internal void SetUpdatableFlags(UriSyntaxFlags flags)
		{
			this.m_UpdatableFlags = flags;
			this.m_UpdatableFlagsUsed = true;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000119D0 File Offset: 0x0000FBD0
		internal UriParser InternalOnNewUri()
		{
			UriParser uriParser = this.OnNewUri();
			if (this != uriParser)
			{
				uriParser.m_Scheme = this.m_Scheme;
				uriParser.m_Port = this.m_Port;
				uriParser.m_Flags = this.m_Flags;
			}
			return uriParser;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00011A0D File Offset: 0x0000FC0D
		internal void InternalValidate(Uri thisUri, out UriFormatException parsingError)
		{
			this.InitializeAndValidate(thisUri, out parsingError);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00011A17 File Offset: 0x0000FC17
		internal string InternalResolve(Uri thisBaseUri, Uri uriLink, out UriFormatException parsingError)
		{
			return this.Resolve(thisBaseUri, uriLink, out parsingError);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00011A22 File Offset: 0x0000FC22
		internal bool InternalIsBaseOf(Uri thisBaseUri, Uri uriLink)
		{
			return this.IsBaseOf(thisBaseUri, uriLink);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00011A2C File Offset: 0x0000FC2C
		internal string InternalGetComponents(Uri thisUri, UriComponents uriComponents, UriFormat uriFormat)
		{
			return this.GetComponents(thisUri, uriComponents, uriFormat);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00011A37 File Offset: 0x0000FC37
		internal bool InternalIsWellFormedOriginalString(Uri thisUri)
		{
			return this.IsWellFormedOriginalString(thisUri);
		}

		// Token: 0x040003E5 RID: 997
		private const UriSyntaxFlags SchemeOnlyFlags = UriSyntaxFlags.MayHavePath;

		// Token: 0x040003E6 RID: 998
		private static readonly Dictionary<string, UriParser> m_Table = new Dictionary<string, UriParser>(25);

		// Token: 0x040003E7 RID: 999
		private static Dictionary<string, UriParser> m_TempTable = new Dictionary<string, UriParser>(25);

		// Token: 0x040003E8 RID: 1000
		private UriSyntaxFlags m_Flags;

		// Token: 0x040003E9 RID: 1001
		private volatile UriSyntaxFlags m_UpdatableFlags;

		// Token: 0x040003EA RID: 1002
		private volatile bool m_UpdatableFlagsUsed;

		// Token: 0x040003EB RID: 1003
		private const UriSyntaxFlags c_UpdatableFlags = UriSyntaxFlags.UnEscapeDotsAndSlashes;

		// Token: 0x040003EC RID: 1004
		private int m_Port;

		// Token: 0x040003ED RID: 1005
		private string m_Scheme;

		// Token: 0x040003EE RID: 1006
		internal const int NoDefaultPort = -1;

		// Token: 0x040003EF RID: 1007
		private const int c_InitialTableSize = 25;

		// Token: 0x040003F0 RID: 1008
		internal static UriParser HttpUri = new UriParser.BuiltInUriParser("http", 80, UriParser.HttpSyntaxFlags);

		// Token: 0x040003F1 RID: 1009
		internal static UriParser HttpsUri;

		// Token: 0x040003F2 RID: 1010
		internal static UriParser WsUri;

		// Token: 0x040003F3 RID: 1011
		internal static UriParser WssUri;

		// Token: 0x040003F4 RID: 1012
		internal static UriParser FtpUri;

		// Token: 0x040003F5 RID: 1013
		internal static UriParser FileUri;

		// Token: 0x040003F6 RID: 1014
		internal static UriParser GopherUri;

		// Token: 0x040003F7 RID: 1015
		internal static UriParser NntpUri;

		// Token: 0x040003F8 RID: 1016
		internal static UriParser NewsUri;

		// Token: 0x040003F9 RID: 1017
		internal static UriParser MailToUri;

		// Token: 0x040003FA RID: 1018
		internal static UriParser UuidUri;

		// Token: 0x040003FB RID: 1019
		internal static UriParser TelnetUri;

		// Token: 0x040003FC RID: 1020
		internal static UriParser LdapUri;

		// Token: 0x040003FD RID: 1021
		internal static UriParser NetTcpUri;

		// Token: 0x040003FE RID: 1022
		internal static UriParser NetPipeUri;

		// Token: 0x040003FF RID: 1023
		internal static UriParser VsMacrosUri;

		// Token: 0x04000400 RID: 1024
		private static readonly UriParser.UriQuirksVersion s_QuirksVersion = (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5 ? UriParser.UriQuirksVersion.V3 : UriParser.UriQuirksVersion.V2);

		// Token: 0x04000401 RID: 1025
		private const int c_MaxCapacity = 512;

		// Token: 0x04000402 RID: 1026
		private const UriSyntaxFlags UnknownV1SyntaxFlags = UriSyntaxFlags.OptionalAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.V1_UnknownUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000403 RID: 1027
		private static readonly UriSyntaxFlags HttpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | (UriParser.ShouldUseLegacyV2Quirks ? UriSyntaxFlags.UnEscapeDotsAndSlashes : UriSyntaxFlags.None) | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000404 RID: 1028
		private const UriSyntaxFlags FtpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000405 RID: 1029
		private static readonly UriSyntaxFlags FileSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | (UriParser.ShouldUseLegacyV2Quirks ? UriSyntaxFlags.None : UriSyntaxFlags.MayHaveQuery) | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000406 RID: 1030
		private const UriSyntaxFlags VsmacrosSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000407 RID: 1031
		private const UriSyntaxFlags GopherSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000408 RID: 1032
		private const UriSyntaxFlags NewsSyntaxFlags = UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000409 RID: 1033
		private const UriSyntaxFlags NntpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400040A RID: 1034
		private const UriSyntaxFlags TelnetSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400040B RID: 1035
		private const UriSyntaxFlags LdapSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400040C RID: 1036
		private const UriSyntaxFlags MailtoSyntaxFlags = UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.MailToLikeUri | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400040D RID: 1037
		private const UriSyntaxFlags NetPipeSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400040E RID: 1038
		private const UriSyntaxFlags NetTcpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x020006DC RID: 1756
		private enum UriQuirksVersion
		{
			// Token: 0x04002FF7 RID: 12279
			V2 = 2,
			// Token: 0x04002FF8 RID: 12280
			V3
		}

		// Token: 0x020006DD RID: 1757
		private class BuiltInUriParser : UriParser
		{
			// Token: 0x0600401B RID: 16411 RVA: 0x0010D33C File Offset: 0x0010B53C
			internal BuiltInUriParser(string lwrCaseScheme, int defaultPort, UriSyntaxFlags syntaxFlags)
				: base(syntaxFlags | UriSyntaxFlags.SimpleUserSyntax | UriSyntaxFlags.BuiltInSyntax)
			{
				this.m_Scheme = lwrCaseScheme;
				this.m_Port = defaultPort;
			}
		}
	}
}
