using System;

namespace System.Data.Services.Client
{
	// Token: 0x02000127 RID: 295
	internal class UriResolver
	{
		// Token: 0x060009D9 RID: 2521 RVA: 0x00028248 File Offset: 0x00026448
		private UriResolver(Uri baseUri, Func<string, Uri> resolveEntitySet)
		{
			this.baseUri = baseUri;
			this.resolveEntitySet = resolveEntitySet;
			if (this.baseUri != null)
			{
				this.baseUriWithSlash = UriResolver.ForceSlashTerminatedUri(this.baseUri);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x0002827D File Offset: 0x0002647D
		internal Func<string, Uri> ResolveEntitySet
		{
			get
			{
				return this.resolveEntitySet;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00028285 File Offset: 0x00026485
		internal Uri RawBaseUriValue
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x0002828D File Offset: 0x0002648D
		internal Uri BaseUriOrNull
		{
			get
			{
				return this.baseUriWithSlash;
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00028295 File Offset: 0x00026495
		internal static UriResolver CreateFromBaseUri(Uri baseUri, string parameterName)
		{
			UriResolver.ConvertToAbsoluteAndValidateBaseUri(ref baseUri, parameterName);
			return new UriResolver(baseUri, null);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000282A6 File Offset: 0x000264A6
		internal UriResolver CloneWithOverrideValue(Uri overrideBaseUriValue, string parameterName)
		{
			UriResolver.ConvertToAbsoluteAndValidateBaseUri(ref overrideBaseUriValue, parameterName);
			return new UriResolver(overrideBaseUriValue, this.resolveEntitySet);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x000282BC File Offset: 0x000264BC
		internal UriResolver CloneWithOverrideValue(Func<string, Uri> overrideResolveEntitySetValue)
		{
			return new UriResolver(this.baseUri, overrideResolveEntitySetValue);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000282CC File Offset: 0x000264CC
		internal Uri GetEntitySetUri(string entitySetName)
		{
			Uri entitySetUriFromResolver = this.GetEntitySetUriFromResolver(entitySetName);
			if (entitySetUriFromResolver != null)
			{
				return UriResolver.ForceNonSlashTerminatedUri(entitySetUriFromResolver);
			}
			if (this.baseUriWithSlash != null)
			{
				return UriUtil.CreateUri(this.baseUriWithSlash, UriUtil.CreateUri(entitySetName, UriKind.Relative));
			}
			throw Error.InvalidOperation(Strings.Context_ResolveEntitySetOrBaseUriRequired(entitySetName));
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00028324 File Offset: 0x00026524
		internal Uri GetBaseUriWithSlash()
		{
			return this.GetBaseUriWithSlash(() => Strings.Context_BaseUriRequired);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00028350 File Offset: 0x00026550
		internal Uri GetOrCreateAbsoluteUri(Uri requestUri)
		{
			Util.CheckArgumentNull<Uri>(requestUri, "requestUri");
			if (!requestUri.IsAbsoluteUri)
			{
				return UriUtil.CreateUri(this.GetBaseUriWithSlash(() => Strings.Context_RequestUriIsRelativeBaseUriRequired), requestUri);
			}
			return requestUri;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0002839C File Offset: 0x0002659C
		private static void ConvertToAbsoluteAndValidateBaseUri(ref Uri baseUri, string parameterName)
		{
			baseUri = UriResolver.ConvertToAbsoluteUri(baseUri);
			if (UriResolver.IsValidBaseUri(baseUri))
			{
				return;
			}
			if (parameterName != null)
			{
				throw Error.Argument(Strings.Context_BaseUri, parameterName);
			}
			throw Error.InvalidOperation(Strings.Context_BaseUri);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x000283CC File Offset: 0x000265CC
		private static bool IsValidBaseUri(Uri baseUri)
		{
			return baseUri == null || (baseUri.IsAbsoluteUri && Uri.IsWellFormedUriString(UriUtil.UriToString(baseUri), UriKind.Absolute) && string.IsNullOrEmpty(baseUri.Query) && string.IsNullOrEmpty(baseUri.Fragment) && (!(baseUri.Scheme != "http") || !(baseUri.Scheme != "https")));
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002843B File Offset: 0x0002663B
		private static Uri ConvertToAbsoluteUri(Uri baseUri)
		{
			if (baseUri == null)
			{
				return null;
			}
			return baseUri;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002844C File Offset: 0x0002664C
		private static Uri ForceNonSlashTerminatedUri(Uri uri)
		{
			string text = UriUtil.UriToString(uri);
			if (text[text.Length - 1] == '/')
			{
				return UriUtil.CreateUri(text.Substring(0, text.Length - 1), UriKind.Absolute);
			}
			return uri;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0002848C File Offset: 0x0002668C
		private static Uri ForceSlashTerminatedUri(Uri uri)
		{
			string text = UriUtil.UriToString(uri);
			if (text[text.Length - 1] != '/')
			{
				return UriUtil.CreateUri(text + "/", UriKind.Absolute);
			}
			return uri;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x000284C5 File Offset: 0x000266C5
		private Uri GetBaseUriWithSlash(Func<string> getErrorMessage)
		{
			if (this.baseUriWithSlash == null)
			{
				throw Error.InvalidOperation(getErrorMessage());
			}
			return this.baseUriWithSlash;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000284E8 File Offset: 0x000266E8
		private Uri GetEntitySetUriFromResolver(string entitySetName)
		{
			if (this.resolveEntitySet != null)
			{
				Uri uri = this.resolveEntitySet(entitySetName);
				if (uri != null)
				{
					if (UriResolver.IsValidBaseUri(uri))
					{
						return uri;
					}
					throw Error.InvalidOperation(Strings.Context_ResolveReturnedInvalidUri);
				}
			}
			return null;
		}

		// Token: 0x040005A6 RID: 1446
		private readonly Uri baseUri;

		// Token: 0x040005A7 RID: 1447
		private readonly Func<string, Uri> resolveEntitySet;

		// Token: 0x040005A8 RID: 1448
		private readonly Uri baseUriWithSlash;
	}
}
