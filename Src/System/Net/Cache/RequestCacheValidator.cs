using System;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x02000318 RID: 792
	internal abstract class RequestCacheValidator
	{
		// Token: 0x06001C18 RID: 7192
		internal abstract RequestCacheValidator CreateValidator();

		// Token: 0x06001C19 RID: 7193 RVA: 0x00085D59 File Offset: 0x00083F59
		protected RequestCacheValidator(bool strictCacheErrors, TimeSpan unspecifiedMaxAge)
		{
			this._StrictCacheErrors = strictCacheErrors;
			this._UnspecifiedMaxAge = unspecifiedMaxAge;
			this._ValidationStatus = CacheValidationStatus.DoNotUseCache;
			this._CacheFreshnessStatus = CacheFreshnessStatus.Undefined;
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x00085D7D File Offset: 0x00083F7D
		internal bool StrictCacheErrors
		{
			get
			{
				return this._StrictCacheErrors;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x00085D85 File Offset: 0x00083F85
		internal TimeSpan UnspecifiedMaxAge
		{
			get
			{
				return this._UnspecifiedMaxAge;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x00085D8D File Offset: 0x00083F8D
		protected internal Uri Uri
		{
			get
			{
				return this._Uri;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x00085D95 File Offset: 0x00083F95
		protected internal WebRequest Request
		{
			get
			{
				return this._Request;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x00085D9D File Offset: 0x00083F9D
		protected internal WebResponse Response
		{
			get
			{
				return this._Response;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x00085DA5 File Offset: 0x00083FA5
		protected internal RequestCachePolicy Policy
		{
			get
			{
				return this._Policy;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x00085DAD File Offset: 0x00083FAD
		protected internal int ResponseCount
		{
			get
			{
				return this._ResponseCount;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x00085DB5 File Offset: 0x00083FB5
		protected internal CacheValidationStatus ValidationStatus
		{
			get
			{
				return this._ValidationStatus;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x00085DBD File Offset: 0x00083FBD
		protected internal CacheFreshnessStatus CacheFreshnessStatus
		{
			get
			{
				return this._CacheFreshnessStatus;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x00085DC5 File Offset: 0x00083FC5
		protected internal RequestCacheEntry CacheEntry
		{
			get
			{
				return this._CacheEntry;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x00085DCD File Offset: 0x00083FCD
		// (set) Token: 0x06001C25 RID: 7205 RVA: 0x00085DD5 File Offset: 0x00083FD5
		protected internal Stream CacheStream
		{
			get
			{
				return this._CacheStream;
			}
			set
			{
				this._CacheStream = value;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x00085DDE File Offset: 0x00083FDE
		// (set) Token: 0x06001C27 RID: 7207 RVA: 0x00085DE6 File Offset: 0x00083FE6
		protected internal long CacheStreamOffset
		{
			get
			{
				return this._CacheStreamOffset;
			}
			set
			{
				this._CacheStreamOffset = value;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x00085DEF File Offset: 0x00083FEF
		// (set) Token: 0x06001C29 RID: 7209 RVA: 0x00085DF7 File Offset: 0x00083FF7
		protected internal long CacheStreamLength
		{
			get
			{
				return this._CacheStreamLength;
			}
			set
			{
				this._CacheStreamLength = value;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001C2A RID: 7210 RVA: 0x00085E00 File Offset: 0x00084000
		protected internal string CacheKey
		{
			get
			{
				return this._CacheKey;
			}
		}

		// Token: 0x06001C2B RID: 7211
		protected internal abstract CacheValidationStatus ValidateRequest();

		// Token: 0x06001C2C RID: 7212
		protected internal abstract CacheFreshnessStatus ValidateFreshness();

		// Token: 0x06001C2D RID: 7213
		protected internal abstract CacheValidationStatus ValidateCache();

		// Token: 0x06001C2E RID: 7214
		protected internal abstract CacheValidationStatus ValidateResponse();

		// Token: 0x06001C2F RID: 7215
		protected internal abstract CacheValidationStatus RevalidateCache();

		// Token: 0x06001C30 RID: 7216
		protected internal abstract CacheValidationStatus UpdateCache();

		// Token: 0x06001C31 RID: 7217 RVA: 0x00085E08 File Offset: 0x00084008
		protected internal virtual void FailRequest(WebExceptionStatus webStatus)
		{
			if (Logging.On)
			{
				Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_failing_request_with_exception", new object[] { webStatus.ToString() }));
			}
			if (webStatus == WebExceptionStatus.CacheEntryNotFound)
			{
				throw ExceptionHelper.CacheEntryNotFoundException;
			}
			if (webStatus == WebExceptionStatus.RequestProhibitedByCachePolicy)
			{
				throw ExceptionHelper.RequestProhibitedByCachePolicyException;
			}
			throw new WebException(NetRes.GetWebStatusString("net_requestaborted", webStatus), webStatus);
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x00085E70 File Offset: 0x00084070
		internal void FetchRequest(Uri uri, WebRequest request)
		{
			this._Request = request;
			this._Policy = request.CachePolicy;
			this._Response = null;
			this._ResponseCount = 0;
			this._ValidationStatus = CacheValidationStatus.DoNotUseCache;
			this._CacheFreshnessStatus = CacheFreshnessStatus.Undefined;
			this._CacheStream = null;
			this._CacheStreamOffset = 0L;
			this._CacheStreamLength = 0L;
			if (!uri.Equals(this._Uri))
			{
				this._CacheKey = uri.GetParts(UriComponents.AbsoluteUri, UriFormat.Unescaped);
			}
			this._Uri = uri;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x00085EE7 File Offset: 0x000840E7
		internal void FetchCacheEntry(RequestCacheEntry fetchEntry)
		{
			this._CacheEntry = fetchEntry;
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00085EF0 File Offset: 0x000840F0
		internal void FetchResponse(WebResponse fetchResponse)
		{
			this._ResponseCount++;
			this._Response = fetchResponse;
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00085F07 File Offset: 0x00084107
		internal void SetFreshnessStatus(CacheFreshnessStatus status)
		{
			this._CacheFreshnessStatus = status;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00085F10 File Offset: 0x00084110
		internal void SetValidationStatus(CacheValidationStatus status)
		{
			this._ValidationStatus = status;
		}

		// Token: 0x04001B6E RID: 7022
		internal WebRequest _Request;

		// Token: 0x04001B6F RID: 7023
		internal WebResponse _Response;

		// Token: 0x04001B70 RID: 7024
		internal Stream _CacheStream;

		// Token: 0x04001B71 RID: 7025
		private RequestCachePolicy _Policy;

		// Token: 0x04001B72 RID: 7026
		private Uri _Uri;

		// Token: 0x04001B73 RID: 7027
		private string _CacheKey;

		// Token: 0x04001B74 RID: 7028
		private RequestCacheEntry _CacheEntry;

		// Token: 0x04001B75 RID: 7029
		private int _ResponseCount;

		// Token: 0x04001B76 RID: 7030
		private CacheValidationStatus _ValidationStatus;

		// Token: 0x04001B77 RID: 7031
		private CacheFreshnessStatus _CacheFreshnessStatus;

		// Token: 0x04001B78 RID: 7032
		private long _CacheStreamOffset;

		// Token: 0x04001B79 RID: 7033
		private long _CacheStreamLength;

		// Token: 0x04001B7A RID: 7034
		private bool _StrictCacheErrors;

		// Token: 0x04001B7B RID: 7035
		private TimeSpan _UnspecifiedMaxAge;
	}
}
