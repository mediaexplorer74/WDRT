using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x0200031F RID: 799
	internal class RequestCacheProtocol
	{
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x00087CC8 File Offset: 0x00085EC8
		internal CacheValidationStatus ProtocolStatus
		{
			get
			{
				return this._ProtocolStatus;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x00087CD0 File Offset: 0x00085ED0
		internal Exception ProtocolException
		{
			get
			{
				return this._ProtocolException;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x00087CD8 File Offset: 0x00085ED8
		internal Stream ResponseStream
		{
			get
			{
				return this._ResponseStream;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x00087CE0 File Offset: 0x00085EE0
		internal long ResponseStreamLength
		{
			get
			{
				return this._ResponseStreamLength;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x00087CE8 File Offset: 0x00085EE8
		internal RequestCacheValidator Validator
		{
			get
			{
				return this._Validator;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x00087CF0 File Offset: 0x00085EF0
		internal bool IsCacheFresh
		{
			get
			{
				return this._Validator != null && this._Validator.CacheFreshnessStatus == CacheFreshnessStatus.Fresh;
			}
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x00087D0A File Offset: 0x00085F0A
		internal RequestCacheProtocol(RequestCache cache, RequestCacheValidator defaultValidator)
		{
			this._RequestCache = cache;
			this._Validator = defaultValidator;
			this._CanTakeNewRequest = true;
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x00087D28 File Offset: 0x00085F28
		internal CacheValidationStatus GetRetrieveStatus(Uri cacheUri, WebRequest request)
		{
			if (cacheUri == null)
			{
				throw new ArgumentNullException("cacheUri");
			}
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (!this._CanTakeNewRequest || this._ProtocolStatus == CacheValidationStatus.RetryResponseFromServer)
			{
				return CacheValidationStatus.Continue;
			}
			this._CanTakeNewRequest = false;
			this._ResponseStream = null;
			this._ResponseStreamLength = 0L;
			this._ProtocolStatus = CacheValidationStatus.Continue;
			this._ProtocolException = null;
			if (Logging.On)
			{
				Logging.Enter(Logging.RequestCache, this, "GetRetrieveStatus", request);
			}
			try
			{
				if (request.CachePolicy == null || request.CachePolicy.Level == RequestCacheLevel.BypassCache)
				{
					this._ProtocolStatus = CacheValidationStatus.DoNotUseCache;
					return this._ProtocolStatus;
				}
				if (this._RequestCache == null || this._Validator == null)
				{
					this._ProtocolStatus = CacheValidationStatus.DoNotUseCache;
					return this._ProtocolStatus;
				}
				this._Validator.FetchRequest(cacheUri, request);
				CacheValidationStatus cacheValidationStatus = (this._ProtocolStatus = this.ValidateRequest());
				switch (cacheValidationStatus)
				{
				case CacheValidationStatus.DoNotUseCache:
				case CacheValidationStatus.DoNotTakeFromCache:
					break;
				case CacheValidationStatus.Fail:
					this._ProtocolException = new InvalidOperationException(SR.GetString("net_cache_validator_fail", new object[] { "ValidateRequest" }));
					break;
				default:
					if (cacheValidationStatus != CacheValidationStatus.Continue)
					{
						this._ProtocolStatus = CacheValidationStatus.Fail;
						this._ProtocolException = new InvalidOperationException(SR.GetString("net_cache_validator_result", new object[]
						{
							"ValidateRequest",
							this._Validator.ValidationStatus.ToString()
						}));
						if (Logging.On)
						{
							Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_unexpected_status", new object[]
							{
								"ValidateRequest()",
								this._Validator.ValidationStatus.ToString()
							}));
						}
					}
					break;
				}
				if (this._ProtocolStatus != CacheValidationStatus.Continue)
				{
					return this._ProtocolStatus;
				}
				this.CheckRetrieveBeforeSubmit();
			}
			catch (Exception ex)
			{
				this._ProtocolException = ex;
				this._ProtocolStatus = CacheValidationStatus.Fail;
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (Logging.On)
				{
					Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_object_and_exception", new object[]
					{
						"CacheProtocol#" + this.GetHashCode().ToString(NumberFormatInfo.InvariantInfo),
						(ex is WebException) ? ex.Message : ex.ToString()
					}));
				}
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.RequestCache, this, "GetRetrieveStatus", "result = " + this._ProtocolStatus.ToString());
				}
			}
			return this._ProtocolStatus;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x00087FF8 File Offset: 0x000861F8
		internal CacheValidationStatus GetRevalidateStatus(WebResponse response, Stream responseStream)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			if (this._ProtocolStatus == CacheValidationStatus.DoNotUseCache)
			{
				return CacheValidationStatus.DoNotUseCache;
			}
			if (this._ProtocolStatus == CacheValidationStatus.ReturnCachedResponse)
			{
				this._ProtocolStatus = CacheValidationStatus.DoNotUseCache;
				return this._ProtocolStatus;
			}
			try
			{
				if (Logging.On)
				{
					Logging.Enter(Logging.RequestCache, this, "GetRevalidateStatus", (this._Validator == null) ? null : this._Validator.Request);
				}
				this._Validator.FetchResponse(response);
				if (this._ProtocolStatus != CacheValidationStatus.Continue && this._ProtocolStatus != CacheValidationStatus.RetryResponseFromServer)
				{
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_revalidation_not_needed", new object[] { "GetRevalidateStatus()" }));
					}
					return this._ProtocolStatus;
				}
				this.CheckRetrieveOnResponse(responseStream);
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.RequestCache, this, "GetRevalidateStatus", "result = " + this._ProtocolStatus.ToString());
				}
			}
			return this._ProtocolStatus;
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x00088108 File Offset: 0x00086308
		internal CacheValidationStatus GetUpdateStatus(WebResponse response, Stream responseStream)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			if (this._ProtocolStatus == CacheValidationStatus.DoNotUseCache)
			{
				return CacheValidationStatus.DoNotUseCache;
			}
			try
			{
				if (Logging.On)
				{
					Logging.Enter(Logging.RequestCache, this, "GetUpdateStatus", null);
				}
				if (this._Validator.Response == null)
				{
					this._Validator.FetchResponse(response);
				}
				if (this._ProtocolStatus == CacheValidationStatus.RemoveFromCache)
				{
					this.EnsureCacheRemoval(this._Validator.CacheKey);
					return this._ProtocolStatus;
				}
				if (this._ProtocolStatus != CacheValidationStatus.DoNotTakeFromCache && this._ProtocolStatus != CacheValidationStatus.ReturnCachedResponse && this._ProtocolStatus != CacheValidationStatus.CombineCachedAndServerResponse)
				{
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_not_updated_based_on_cache_protocol_status", new object[]
						{
							"GetUpdateStatus()",
							this._ProtocolStatus.ToString()
						}));
					}
					return this._ProtocolStatus;
				}
				this.CheckUpdateOnResponse(responseStream);
			}
			catch (Exception ex)
			{
				this._ProtocolException = ex;
				this._ProtocolStatus = CacheValidationStatus.Fail;
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (Logging.On)
				{
					Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_object_and_exception", new object[]
					{
						"CacheProtocol#" + this.GetHashCode().ToString(NumberFormatInfo.InvariantInfo),
						(ex is WebException) ? ex.Message : ex.ToString()
					}));
				}
			}
			finally
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.RequestCache, this, "GetUpdateStatus", "result = " + this._ProtocolStatus.ToString());
				}
			}
			return this._ProtocolStatus;
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x000882EC File Offset: 0x000864EC
		internal void Reset()
		{
			this._CanTakeNewRequest = true;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x000882F8 File Offset: 0x000864F8
		internal void Abort()
		{
			if (this._CanTakeNewRequest)
			{
				return;
			}
			Stream responseStream = this._ResponseStream;
			if (responseStream != null)
			{
				try
				{
					if (Logging.On)
					{
						Logging.PrintWarning(Logging.RequestCache, SR.GetString("net_log_cache_closing_cache_stream", new object[]
						{
							"CacheProtocol#" + this.GetHashCode().ToString(NumberFormatInfo.InvariantInfo),
							"Abort()",
							responseStream.GetType().FullName,
							this._Validator.CacheKey
						}));
					}
					ICloseEx closeEx = responseStream as ICloseEx;
					if (closeEx != null)
					{
						closeEx.CloseEx(CloseExState.Abort | CloseExState.Silent);
					}
					else
					{
						responseStream.Close();
					}
				}
				catch (Exception ex)
				{
					if (NclUtilities.IsFatal(ex))
					{
						throw;
					}
					if (Logging.On)
					{
						Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_exception_ignored", new object[]
						{
							"CacheProtocol#" + this.GetHashCode().ToString(NumberFormatInfo.InvariantInfo),
							"stream.Close()",
							ex.ToString()
						}));
					}
				}
			}
			this.Reset();
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00088410 File Offset: 0x00086610
		private void CheckRetrieveBeforeSubmit()
		{
			try
			{
				CacheValidationStatus protocolStatus;
				for (;;)
				{
					if (this._Validator.CacheStream != null && this._Validator.CacheStream != Stream.Null)
					{
						this._Validator.CacheStream.Close();
						this._Validator.CacheStream = Stream.Null;
					}
					RequestCacheEntry requestCacheEntry;
					if (this._Validator.StrictCacheErrors)
					{
						this._Validator.CacheStream = this._RequestCache.Retrieve(this._Validator.CacheKey, out requestCacheEntry);
					}
					else
					{
						Stream stream;
						this._RequestCache.TryRetrieve(this._Validator.CacheKey, out requestCacheEntry, out stream);
						this._Validator.CacheStream = stream;
					}
					if (requestCacheEntry == null)
					{
						requestCacheEntry = new RequestCacheEntry();
						requestCacheEntry.IsPrivateEntry = this._RequestCache.IsPrivateCache;
						this._Validator.FetchCacheEntry(requestCacheEntry);
					}
					if (this._Validator.CacheStream == null)
					{
						this._Validator.CacheStream = Stream.Null;
					}
					this.ValidateFreshness(requestCacheEntry);
					this._ProtocolStatus = this.ValidateCache();
					protocolStatus = this._ProtocolStatus;
					switch (protocolStatus)
					{
					case CacheValidationStatus.DoNotUseCache:
					case CacheValidationStatus.DoNotTakeFromCache:
						goto IL_33D;
					case CacheValidationStatus.Fail:
						goto IL_288;
					case CacheValidationStatus.RetryResponseFromCache:
						continue;
					case CacheValidationStatus.RetryResponseFromServer:
						goto IL_2B0;
					case CacheValidationStatus.ReturnCachedResponse:
						goto IL_120;
					}
					break;
				}
				if (protocolStatus != CacheValidationStatus.Continue)
				{
					goto IL_2B0;
				}
				this._ResponseStream = this._Validator.CacheStream;
				goto IL_33D;
				IL_120:
				if (this._Validator.CacheStream == null || this._Validator.CacheStream == Stream.Null)
				{
					if (Logging.On)
					{
						Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_no_cache_entry", new object[] { "ValidateCache()" }));
					}
					this._ProtocolStatus = CacheValidationStatus.Fail;
					this._ProtocolException = new InvalidOperationException(SR.GetString("net_cache_no_stream", new object[] { this._Validator.CacheKey }));
					goto IL_33D;
				}
				Stream stream2 = this._Validator.CacheStream;
				this._RequestCache.UnlockEntry(this._Validator.CacheStream);
				if (this._Validator.CacheStreamOffset != 0L || this._Validator.CacheStreamLength != this._Validator.CacheEntry.StreamSize)
				{
					stream2 = new RangeStream(stream2, this._Validator.CacheStreamOffset, this._Validator.CacheStreamLength);
					if (Logging.On)
					{
						Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_returned_range_cache", new object[]
						{
							"ValidateCache()",
							this._Validator.CacheStreamOffset,
							this._Validator.CacheStreamLength
						}));
					}
				}
				this._ResponseStream = stream2;
				this._ResponseStreamLength = this._Validator.CacheStreamLength;
				goto IL_33D;
				IL_288:
				this._ProtocolException = new InvalidOperationException(SR.GetString("net_cache_validator_fail", new object[] { "ValidateCache" }));
				goto IL_33D;
				IL_2B0:
				this._ProtocolStatus = CacheValidationStatus.Fail;
				this._ProtocolException = new InvalidOperationException(SR.GetString("net_cache_validator_result", new object[]
				{
					"ValidateCache",
					this._Validator.ValidationStatus.ToString()
				}));
				if (Logging.On)
				{
					Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_unexpected_status", new object[]
					{
						"ValidateCache()",
						this._Validator.ValidationStatus.ToString()
					}));
				}
				IL_33D:;
			}
			catch (Exception ex)
			{
				this._ProtocolStatus = CacheValidationStatus.Fail;
				this._ProtocolException = ex;
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (Logging.On)
				{
					Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_object_and_exception", new object[]
					{
						"CacheProtocol#" + this.GetHashCode().ToString(NumberFormatInfo.InvariantInfo),
						(ex is WebException) ? ex.Message : ex.ToString()
					}));
				}
			}
			finally
			{
				if (this._ResponseStream == null && this._Validator.CacheStream != null && this._Validator.CacheStream != Stream.Null)
				{
					this._Validator.CacheStream.Close();
					this._Validator.CacheStream = Stream.Null;
				}
			}
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0008886C File Offset: 0x00086A6C
		private void CheckRetrieveOnResponse(Stream responseStream)
		{
			bool flag = true;
			try
			{
				CacheValidationStatus cacheValidationStatus = (this._ProtocolStatus = this.ValidateResponse());
				switch (cacheValidationStatus)
				{
				case CacheValidationStatus.DoNotUseCache:
					goto IL_F9;
				case CacheValidationStatus.Fail:
					this._ProtocolStatus = CacheValidationStatus.Fail;
					this._ProtocolException = new InvalidOperationException(SR.GetString("net_cache_validator_fail", new object[] { "ValidateResponse" }));
					goto IL_F9;
				case CacheValidationStatus.DoNotTakeFromCache:
				case CacheValidationStatus.RetryResponseFromCache:
					break;
				case CacheValidationStatus.RetryResponseFromServer:
					flag = false;
					goto IL_F9;
				default:
					if (cacheValidationStatus == CacheValidationStatus.Continue)
					{
						flag = false;
						goto IL_F9;
					}
					break;
				}
				this._ProtocolStatus = CacheValidationStatus.Fail;
				this._ProtocolException = new InvalidOperationException(SR.GetString("net_cache_validator_result", new object[]
				{
					"ValidateResponse",
					this._Validator.ValidationStatus.ToString()
				}));
				if (Logging.On)
				{
					Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_unexpected_status", new object[]
					{
						"ValidateResponse()",
						this._Validator.ValidationStatus.ToString()
					}));
				}
				IL_F9:;
			}
			catch (Exception ex)
			{
				flag = true;
				this._ProtocolException = ex;
				this._ProtocolStatus = CacheValidationStatus.Fail;
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (Logging.On)
				{
					Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_object_and_exception", new object[]
					{
						"CacheProtocol#" + this.GetHashCode().ToString(NumberFormatInfo.InvariantInfo),
						(ex is WebException) ? ex.Message : ex.ToString()
					}));
				}
			}
			finally
			{
				if (flag && this._ResponseStream != null)
				{
					this._ResponseStream.Close();
					this._ResponseStream = null;
					this._Validator.CacheStream = Stream.Null;
				}
			}
			if (this._ProtocolStatus != CacheValidationStatus.Continue)
			{
				return;
			}
			try
			{
				switch (this._ProtocolStatus = this.RevalidateCache())
				{
				case CacheValidationStatus.DoNotUseCache:
				case CacheValidationStatus.DoNotTakeFromCache:
				case CacheValidationStatus.RemoveFromCache:
					flag = true;
					goto IL_4C9;
				case CacheValidationStatus.Fail:
					flag = true;
					this._ProtocolException = new InvalidOperationException(SR.GetString("net_cache_validator_fail", new object[] { "RevalidateCache" }));
					goto IL_4C9;
				case CacheValidationStatus.ReturnCachedResponse:
					if (this._Validator.CacheStream != null && this._Validator.CacheStream != Stream.Null)
					{
						Stream stream = this._Validator.CacheStream;
						if (this._Validator.CacheStreamOffset != 0L || this._Validator.CacheStreamLength != this._Validator.CacheEntry.StreamSize)
						{
							stream = new RangeStream(stream, this._Validator.CacheStreamOffset, this._Validator.CacheStreamLength);
							if (Logging.On)
							{
								Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_returned_range_cache", new object[]
								{
									"RevalidateCache()",
									this._Validator.CacheStreamOffset,
									this._Validator.CacheStreamLength
								}));
							}
						}
						this._ResponseStream = stream;
						this._ResponseStreamLength = this._Validator.CacheStreamLength;
						goto IL_4C9;
					}
					this._ProtocolStatus = CacheValidationStatus.Fail;
					this._ProtocolException = new InvalidOperationException(SR.GetString("net_cache_no_stream", new object[] { this._Validator.CacheKey }));
					if (Logging.On)
					{
						Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_null_cached_stream", new object[] { "RevalidateCache()" }));
						goto IL_4C9;
					}
					goto IL_4C9;
				case CacheValidationStatus.CombineCachedAndServerResponse:
					if (this._Validator.CacheStream != null && this._Validator.CacheStream != Stream.Null)
					{
						Stream stream;
						if (responseStream != null)
						{
							stream = new CombinedReadStream(this._Validator.CacheStream, responseStream);
						}
						else
						{
							stream = this._Validator.CacheStream;
						}
						this._ResponseStream = stream;
						this._ResponseStreamLength = this._Validator.CacheStreamLength;
						goto IL_4C9;
					}
					this._ProtocolStatus = CacheValidationStatus.Fail;
					this._ProtocolException = new InvalidOperationException(SR.GetString("net_cache_no_stream", new object[] { this._Validator.CacheKey }));
					if (Logging.On)
					{
						Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_requested_combined_but_null_cached_stream", new object[] { "RevalidateCache()" }));
						goto IL_4C9;
					}
					goto IL_4C9;
				}
				flag = true;
				this._ProtocolStatus = CacheValidationStatus.Fail;
				this._ProtocolException = new InvalidOperationException(SR.GetString("net_cache_validator_result", new object[]
				{
					"RevalidateCache",
					this._Validator.ValidationStatus.ToString()
				}));
				if (Logging.On)
				{
					Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_unexpected_status", new object[]
					{
						"RevalidateCache()",
						this._Validator.ValidationStatus.ToString()
					}));
				}
				IL_4C9:;
			}
			catch (Exception ex2)
			{
				flag = true;
				this._ProtocolException = ex2;
				this._ProtocolStatus = CacheValidationStatus.Fail;
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				if (Logging.On)
				{
					Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_object_and_exception", new object[]
					{
						"CacheProtocol#" + this.GetHashCode().ToString(NumberFormatInfo.InvariantInfo),
						(ex2 is WebException) ? ex2.Message : ex2.ToString()
					}));
				}
			}
			finally
			{
				if (flag && this._ResponseStream != null)
				{
					this._ResponseStream.Close();
					this._ResponseStream = null;
					this._Validator.CacheStream = Stream.Null;
				}
			}
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00088E6C File Offset: 0x0008706C
		private void CheckUpdateOnResponse(Stream responseStream)
		{
			if (this._Validator.CacheEntry == null)
			{
				RequestCacheEntry requestCacheEntry = new RequestCacheEntry();
				requestCacheEntry.IsPrivateEntry = this._RequestCache.IsPrivateCache;
				this._Validator.FetchCacheEntry(requestCacheEntry);
			}
			string cacheKey = this._Validator.CacheKey;
			bool flag = true;
			try
			{
				switch (this._ProtocolStatus = this.UpdateCache())
				{
				case CacheValidationStatus.DoNotUseCache:
				case CacheValidationStatus.DoNotUpdateCache:
					goto IL_320;
				case CacheValidationStatus.Fail:
					this._ProtocolException = new InvalidOperationException(SR.GetString("net_cache_validator_fail", new object[] { "UpdateCache" }));
					return;
				case CacheValidationStatus.CacheResponse:
				{
					Stream stream;
					if (this._Validator.StrictCacheErrors)
					{
						stream = this._RequestCache.Store(this._Validator.CacheKey, this._Validator.CacheEntry.StreamSize, this._Validator.CacheEntry.ExpiresUtc, this._Validator.CacheEntry.LastModifiedUtc, this._Validator.CacheEntry.MaxStale, this._Validator.CacheEntry.EntryMetadata, this._Validator.CacheEntry.SystemMetadata);
					}
					else
					{
						this._RequestCache.TryStore(this._Validator.CacheKey, this._Validator.CacheEntry.StreamSize, this._Validator.CacheEntry.ExpiresUtc, this._Validator.CacheEntry.LastModifiedUtc, this._Validator.CacheEntry.MaxStale, this._Validator.CacheEntry.EntryMetadata, this._Validator.CacheEntry.SystemMetadata, out stream);
					}
					if (stream == null)
					{
						this._ProtocolStatus = CacheValidationStatus.DoNotUpdateCache;
						return;
					}
					this._ResponseStream = new ForwardingReadStream(responseStream, stream, this._Validator.CacheStreamOffset, this._Validator.StrictCacheErrors);
					this._ProtocolStatus = CacheValidationStatus.UpdateResponseInformation;
					return;
				}
				case CacheValidationStatus.UpdateResponseInformation:
					this._ResponseStream = new MetadataUpdateStream(responseStream, this._RequestCache, this._Validator.CacheKey, this._Validator.CacheEntry.ExpiresUtc, this._Validator.CacheEntry.LastModifiedUtc, this._Validator.CacheEntry.LastSynchronizedUtc, this._Validator.CacheEntry.MaxStale, this._Validator.CacheEntry.EntryMetadata, this._Validator.CacheEntry.SystemMetadata, this._Validator.StrictCacheErrors);
					flag = false;
					this._ProtocolStatus = CacheValidationStatus.UpdateResponseInformation;
					return;
				case CacheValidationStatus.RemoveFromCache:
					this.EnsureCacheRemoval(cacheKey);
					flag = false;
					return;
				}
				this._ProtocolStatus = CacheValidationStatus.Fail;
				this._ProtocolException = new InvalidOperationException(SR.GetString("net_cache_validator_result", new object[]
				{
					"UpdateCache",
					this._Validator.ValidationStatus.ToString()
				}));
				if (Logging.On)
				{
					Logging.PrintError(Logging.RequestCache, SR.GetString("net_log_cache_unexpected_status", new object[]
					{
						"UpdateCache()",
						this._Validator.ValidationStatus.ToString()
					}));
				}
				IL_320:;
			}
			finally
			{
				if (flag)
				{
					this._RequestCache.UnlockEntry(this._Validator.CacheStream);
				}
			}
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x000891D4 File Offset: 0x000873D4
		private CacheValidationStatus ValidateRequest()
		{
			if (Logging.On)
			{
				TraceSource requestCache = Logging.RequestCache;
				string[] array = new string[6];
				array[0] = "Request#";
				array[1] = this._Validator.Request.GetHashCode().ToString(NumberFormatInfo.InvariantInfo);
				array[2] = ", Policy = ";
				array[3] = this._Validator.Request.CachePolicy.ToString();
				array[4] = ", Cache Uri = ";
				int num = 5;
				Uri uri = this._Validator.Uri;
				array[num] = ((uri != null) ? uri.ToString() : null);
				Logging.PrintInfo(requestCache, string.Concat(array));
			}
			CacheValidationStatus cacheValidationStatus = this._Validator.ValidateRequest();
			this._Validator.SetValidationStatus(cacheValidationStatus);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.RequestCache, "Selected cache Key = " + this._Validator.CacheKey);
			}
			return cacheValidationStatus;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x000892A8 File Offset: 0x000874A8
		private void ValidateFreshness(RequestCacheEntry fetchEntry)
		{
			this._Validator.FetchCacheEntry(fetchEntry);
			if (this._Validator.CacheStream == null || this._Validator.CacheStream == Stream.Null)
			{
				if (Logging.On)
				{
					Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_entry_not_found_freshness_undefined", new object[] { "ValidateFreshness()" }));
				}
				this._Validator.SetFreshnessStatus(CacheFreshnessStatus.Undefined);
				return;
			}
			if (Logging.On && Logging.IsVerbose(Logging.RequestCache))
			{
				Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_dumping_cache_context"));
				if (fetchEntry == null)
				{
					Logging.PrintInfo(Logging.RequestCache, "<null>");
				}
				else
				{
					string[] array = fetchEntry.ToString(Logging.IsVerbose(Logging.RequestCache)).Split(RequestCache.LineSplits);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].Length != 0)
						{
							Logging.PrintInfo(Logging.RequestCache, array[i]);
						}
					}
				}
			}
			CacheFreshnessStatus cacheFreshnessStatus = this._Validator.ValidateFreshness();
			this._Validator.SetFreshnessStatus(cacheFreshnessStatus);
			this._IsCacheFresh = cacheFreshnessStatus == CacheFreshnessStatus.Fresh;
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_result", new object[]
				{
					"ValidateFreshness()",
					cacheFreshnessStatus.ToString()
				}));
			}
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x000893EC File Offset: 0x000875EC
		private CacheValidationStatus ValidateCache()
		{
			CacheValidationStatus cacheValidationStatus = this._Validator.ValidateCache();
			this._Validator.SetValidationStatus(cacheValidationStatus);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_result", new object[]
				{
					"ValidateCache()",
					cacheValidationStatus.ToString()
				}));
			}
			return cacheValidationStatus;
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0008944C File Offset: 0x0008764C
		private CacheValidationStatus RevalidateCache()
		{
			CacheValidationStatus cacheValidationStatus = this._Validator.RevalidateCache();
			this._Validator.SetValidationStatus(cacheValidationStatus);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_result", new object[]
				{
					"RevalidateCache()",
					cacheValidationStatus.ToString()
				}));
			}
			return cacheValidationStatus;
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x000894AC File Offset: 0x000876AC
		private CacheValidationStatus ValidateResponse()
		{
			CacheValidationStatus cacheValidationStatus = this._Validator.ValidateResponse();
			this._Validator.SetValidationStatus(cacheValidationStatus);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.RequestCache, SR.GetString("net_log_cache_result", new object[]
				{
					"ValidateResponse()",
					cacheValidationStatus.ToString()
				}));
			}
			return cacheValidationStatus;
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0008950C File Offset: 0x0008770C
		private CacheValidationStatus UpdateCache()
		{
			CacheValidationStatus cacheValidationStatus = this._Validator.UpdateCache();
			this._Validator.SetValidationStatus(cacheValidationStatus);
			return cacheValidationStatus;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x00089534 File Offset: 0x00087734
		private void EnsureCacheRemoval(string retrieveKey)
		{
			this._RequestCache.UnlockEntry(this._Validator.CacheStream);
			if (this._Validator.StrictCacheErrors)
			{
				this._RequestCache.Remove(retrieveKey);
			}
			else
			{
				this._RequestCache.TryRemove(retrieveKey);
			}
			if (retrieveKey != this._Validator.CacheKey)
			{
				if (this._Validator.StrictCacheErrors)
				{
					this._RequestCache.Remove(this._Validator.CacheKey);
					return;
				}
				this._RequestCache.TryRemove(this._Validator.CacheKey);
			}
		}

		// Token: 0x04001B99 RID: 7065
		private CacheValidationStatus _ProtocolStatus;

		// Token: 0x04001B9A RID: 7066
		private Exception _ProtocolException;

		// Token: 0x04001B9B RID: 7067
		private Stream _ResponseStream;

		// Token: 0x04001B9C RID: 7068
		private long _ResponseStreamLength;

		// Token: 0x04001B9D RID: 7069
		private RequestCacheValidator _Validator;

		// Token: 0x04001B9E RID: 7070
		private RequestCache _RequestCache;

		// Token: 0x04001B9F RID: 7071
		private bool _IsCacheFresh;

		// Token: 0x04001BA0 RID: 7072
		private bool _CanTakeNewRequest;
	}
}
