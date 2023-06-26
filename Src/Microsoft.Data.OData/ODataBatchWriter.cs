using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x02000271 RID: 625
	public sealed class ODataBatchWriter : IODataBatchOperationListener, IODataOutputInStreamErrorListener
	{
		// Token: 0x06001493 RID: 5267 RVA: 0x0004D047 File Offset: 0x0004B247
		internal ODataBatchWriter(ODataRawOutputContext rawOutputContext, string batchBoundary)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(batchBoundary, "batchBoundary");
			this.rawOutputContext = rawOutputContext;
			this.batchBoundary = batchBoundary;
			this.urlResolver = new ODataBatchUrlResolver(rawOutputContext.UrlResolver);
			this.rawOutputContext.InitializeRawValueWriter();
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0004D084 File Offset: 0x0004B284
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x0004D08C File Offset: 0x0004B28C
		private ODataBatchOperationRequestMessage CurrentOperationRequestMessage
		{
			get
			{
				return this.currentOperationRequestMessage;
			}
			set
			{
				this.currentOperationRequestMessage = value;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x0004D095 File Offset: 0x0004B295
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x0004D09D File Offset: 0x0004B29D
		private ODataBatchOperationResponseMessage CurrentOperationResponseMessage
		{
			get
			{
				return this.currentOperationResponseMessage;
			}
			set
			{
				this.currentOperationResponseMessage = value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0004D0A6 File Offset: 0x0004B2A6
		private ODataBatchOperationMessage CurrentOperationMessage
		{
			get
			{
				if (this.currentOperationRequestMessage != null)
				{
					return this.currentOperationRequestMessage.OperationMessage;
				}
				if (this.currentOperationResponseMessage != null)
				{
					return this.currentOperationResponseMessage.OperationMessage;
				}
				return null;
			}
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0004D0D1 File Offset: 0x0004B2D1
		public void WriteStartBatch()
		{
			this.VerifyCanWriteStartBatch(true);
			this.WriteStartBatchImplementation();
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0004D0E0 File Offset: 0x0004B2E0
		public Task WriteStartBatchAsync()
		{
			this.VerifyCanWriteStartBatch(false);
			return TaskUtils.GetTaskForSynchronousOperation(new Action(this.WriteStartBatchImplementation));
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0004D0FA File Offset: 0x0004B2FA
		public void WriteEndBatch()
		{
			this.VerifyCanWriteEndBatch(true);
			this.WriteEndBatchImplementation();
			this.Flush();
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0004D117 File Offset: 0x0004B317
		public Task WriteEndBatchAsync()
		{
			this.VerifyCanWriteEndBatch(false);
			return TaskUtils.GetTaskForSynchronousOperation(new Action(this.WriteEndBatchImplementation)).FollowOnSuccessWithTask((Task task) => this.FlushAsync());
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0004D142 File Offset: 0x0004B342
		public void WriteStartChangeset()
		{
			this.VerifyCanWriteStartChangeset(true);
			this.WriteStartChangesetImplementation();
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0004D151 File Offset: 0x0004B351
		public Task WriteStartChangesetAsync()
		{
			this.VerifyCanWriteStartChangeset(false);
			return TaskUtils.GetTaskForSynchronousOperation(new Action(this.WriteStartChangesetImplementation));
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0004D16B File Offset: 0x0004B36B
		public void WriteEndChangeset()
		{
			this.VerifyCanWriteEndChangeset(true);
			this.WriteEndChangesetImplementation();
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0004D17A File Offset: 0x0004B37A
		public Task WriteEndChangesetAsync()
		{
			this.VerifyCanWriteEndChangeset(false);
			return TaskUtils.GetTaskForSynchronousOperation(new Action(this.WriteEndChangesetImplementation));
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0004D194 File Offset: 0x0004B394
		public ODataBatchOperationRequestMessage CreateOperationRequestMessage(string method, Uri uri)
		{
			this.VerifyCanCreateOperationRequestMessage(true, method, uri);
			return this.CreateOperationRequestMessageImplementation(method, uri);
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0004D1C8 File Offset: 0x0004B3C8
		public Task<ODataBatchOperationRequestMessage> CreateOperationRequestMessageAsync(string method, Uri uri)
		{
			this.VerifyCanCreateOperationRequestMessage(false, method, uri);
			return TaskUtils.GetTaskForSynchronousOperation<ODataBatchOperationRequestMessage>(() => this.CreateOperationRequestMessageImplementation(method, uri));
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0004D214 File Offset: 0x0004B414
		public ODataBatchOperationResponseMessage CreateOperationResponseMessage()
		{
			this.VerifyCanCreateOperationResponseMessage(true);
			return this.CreateOperationResponseMessageImplementation();
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0004D223 File Offset: 0x0004B423
		public Task<ODataBatchOperationResponseMessage> CreateOperationResponseMessageAsync()
		{
			this.VerifyCanCreateOperationResponseMessage(false);
			return TaskUtils.GetTaskForSynchronousOperation<ODataBatchOperationResponseMessage>(new Func<ODataBatchOperationResponseMessage>(this.CreateOperationResponseMessageImplementation));
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0004D240 File Offset: 0x0004B440
		public void Flush()
		{
			this.VerifyCanFlush(true);
			try
			{
				this.rawOutputContext.Flush();
			}
			catch
			{
				this.SetState(ODataBatchWriter.BatchWriterState.Error);
				throw;
			}
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0004D285 File Offset: 0x0004B485
		public Task FlushAsync()
		{
			this.VerifyCanFlush(false);
			return this.rawOutputContext.FlushAsync().FollowOnFaultWith(delegate(Task t)
			{
				this.SetState(ODataBatchWriter.BatchWriterState.Error);
			});
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0004D2AA File Offset: 0x0004B4AA
		void IODataBatchOperationListener.BatchOperationContentStreamRequested()
		{
			this.StartBatchOperationContent();
			this.rawOutputContext.FlushBuffers();
			this.DisposeBatchWriterAndSetContentStreamRequestedState();
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0004D2CB File Offset: 0x0004B4CB
		Task IODataBatchOperationListener.BatchOperationContentStreamRequestedAsync()
		{
			this.StartBatchOperationContent();
			return this.rawOutputContext.FlushBuffersAsync().FollowOnSuccessWith(delegate(Task task)
			{
				this.DisposeBatchWriterAndSetContentStreamRequestedState();
			});
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0004D2EF File Offset: 0x0004B4EF
		void IODataBatchOperationListener.BatchOperationContentStreamDisposed()
		{
			this.SetState(ODataBatchWriter.BatchWriterState.OperationStreamDisposed);
			this.CurrentOperationRequestMessage = null;
			this.CurrentOperationResponseMessage = null;
			this.rawOutputContext.InitializeRawValueWriter();
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0004D311 File Offset: 0x0004B511
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			this.rawOutputContext.VerifyNotDisposed();
			this.SetState(ODataBatchWriter.BatchWriterState.Error);
			this.rawOutputContext.TextWriter.Flush();
			throw new ODataException(Strings.ODataBatchWriter_CannotWriteInStreamErrorForBatch);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0004D33F File Offset: 0x0004B53F
		private static bool IsErrorState(ODataBatchWriter.BatchWriterState state)
		{
			return state == ODataBatchWriter.BatchWriterState.Error;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0004D345 File Offset: 0x0004B545
		private void VerifyCanWriteStartBatch(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0004D354 File Offset: 0x0004B554
		private void WriteStartBatchImplementation()
		{
			this.SetState(ODataBatchWriter.BatchWriterState.BatchStarted);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0004D35D File Offset: 0x0004B55D
		private void VerifyCanWriteEndBatch(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0004D36C File Offset: 0x0004B56C
		private void WriteEndBatchImplementation()
		{
			this.WritePendingMessageData(true);
			this.SetState(ODataBatchWriter.BatchWriterState.BatchCompleted);
			ODataBatchWriterUtils.WriteEndBoundary(this.rawOutputContext.TextWriter, this.batchBoundary, !this.batchStartBoundaryWritten);
			this.rawOutputContext.TextWriter.WriteLine();
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0004D3AB File Offset: 0x0004B5AB
		private void VerifyCanWriteStartChangeset(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0004D3BC File Offset: 0x0004B5BC
		private void WriteStartChangesetImplementation()
		{
			this.WritePendingMessageData(true);
			this.SetState(ODataBatchWriter.BatchWriterState.ChangeSetStarted);
			this.ResetChangeSetSize();
			this.InterceptException(new Action(this.IncreaseBatchSize));
			ODataBatchWriterUtils.WriteStartBoundary(this.rawOutputContext.TextWriter, this.batchBoundary, !this.batchStartBoundaryWritten);
			this.batchStartBoundaryWritten = true;
			ODataBatchWriterUtils.WriteChangeSetPreamble(this.rawOutputContext.TextWriter, this.changeSetBoundary);
			this.changesetStartBoundaryWritten = false;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0004D432 File Offset: 0x0004B632
		private void VerifyCanWriteEndChangeset(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0004D444 File Offset: 0x0004B644
		private void WriteEndChangesetImplementation()
		{
			this.WritePendingMessageData(true);
			string text = this.changeSetBoundary;
			this.SetState(ODataBatchWriter.BatchWriterState.ChangeSetCompleted);
			ODataBatchWriterUtils.WriteEndBoundary(this.rawOutputContext.TextWriter, text, !this.changesetStartBoundaryWritten);
			this.urlResolver.Reset();
			this.currentOperationContentId = null;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0004D494 File Offset: 0x0004B694
		private void VerifyCanCreateOperationRequestMessage(bool synchronousCall, string method, Uri uri)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
			if (this.rawOutputContext.WritingResponse)
			{
				this.ThrowODataException(Strings.ODataBatchWriter_CannotCreateRequestOperationWhenWritingResponse);
			}
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(method, "method");
			if (this.changeSetBoundary == null)
			{
				if (!HttpUtils.IsQueryMethod(method))
				{
					this.ThrowODataException(Strings.ODataBatch_InvalidHttpMethodForQueryOperation(method));
				}
			}
			else if (HttpUtils.IsQueryMethod(method))
			{
				this.ThrowODataException(Strings.ODataBatch_InvalidHttpMethodForChangeSetRequest(method));
			}
			ExceptionUtils.CheckArgumentNotNull<Uri>(uri, "uri");
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0004D54C File Offset: 0x0004B74C
		private ODataBatchOperationRequestMessage CreateOperationRequestMessageImplementation(string method, Uri uri)
		{
			if (this.changeSetBoundary == null)
			{
				this.InterceptException(new Action(this.IncreaseBatchSize));
			}
			else
			{
				this.InterceptException(new Action(this.IncreaseChangeSetSize));
			}
			this.WritePendingMessageData(true);
			if (this.currentOperationContentId != null)
			{
				this.urlResolver.AddContentId(this.currentOperationContentId);
			}
			this.InterceptException(delegate
			{
				uri = ODataBatchUtils.CreateOperationRequestUri(uri, this.rawOutputContext.MessageWriterSettings.BaseUri, this.urlResolver);
			});
			this.CurrentOperationRequestMessage = ODataBatchOperationRequestMessage.CreateWriteMessage(this.rawOutputContext.OutputStream, method, uri, this, this.urlResolver);
			this.SetState(ODataBatchWriter.BatchWriterState.OperationCreated);
			this.WriteStartBoundaryForOperation();
			ODataBatchWriterUtils.WriteRequestPreamble(this.rawOutputContext.TextWriter, method, uri);
			return this.CurrentOperationRequestMessage;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0004D61B File Offset: 0x0004B81B
		private void VerifyCanCreateOperationResponseMessage(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
			if (!this.rawOutputContext.WritingResponse)
			{
				this.ThrowODataException(Strings.ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest);
			}
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0004D644 File Offset: 0x0004B844
		private ODataBatchOperationResponseMessage CreateOperationResponseMessageImplementation()
		{
			this.WritePendingMessageData(true);
			this.CurrentOperationResponseMessage = ODataBatchOperationResponseMessage.CreateWriteMessage(this.rawOutputContext.OutputStream, this, this.urlResolver.BatchMessageUrlResolver);
			this.SetState(ODataBatchWriter.BatchWriterState.OperationCreated);
			this.WriteStartBoundaryForOperation();
			ODataBatchWriterUtils.WriteResponsePreamble(this.rawOutputContext.TextWriter);
			return this.CurrentOperationResponseMessage;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0004D69D File Offset: 0x0004B89D
		private void StartBatchOperationContent()
		{
			this.WritePendingMessageData(false);
			this.rawOutputContext.TextWriter.Flush();
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0004D6B6 File Offset: 0x0004B8B6
		private void DisposeBatchWriterAndSetContentStreamRequestedState()
		{
			this.rawOutputContext.CloseWriter();
			this.SetState(ODataBatchWriter.BatchWriterState.OperationStreamRequested);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0004D6CA File Offset: 0x0004B8CA
		private void RememberContentIdHeader(string contentId)
		{
			this.currentOperationContentId = contentId;
			if (contentId != null && this.urlResolver.ContainsContentId(contentId))
			{
				throw new ODataException(Strings.ODataBatchWriter_DuplicateContentIDsNotAllowed(contentId));
			}
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0004D6F0 File Offset: 0x0004B8F0
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.rawOutputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.state == ODataBatchWriter.BatchWriterState.OperationStreamRequested)
			{
				this.ThrowODataException(Strings.ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState);
			}
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0004D718 File Offset: 0x0004B918
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				if (!this.rawOutputContext.Synchronous)
				{
					throw new ODataException(Strings.ODataBatchWriter_SyncCallOnAsyncWriter);
				}
			}
			else if (this.rawOutputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataBatchWriter_AsyncCallOnSyncWriter);
			}
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0004D750 File Offset: 0x0004B950
		private void InterceptException(Action action)
		{
			try
			{
				action();
			}
			catch
			{
				if (!ODataBatchWriter.IsErrorState(this.state))
				{
					this.SetState(ODataBatchWriter.BatchWriterState.Error);
				}
				throw;
			}
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0004D7A8 File Offset: 0x0004B9A8
		private void SetState(ODataBatchWriter.BatchWriterState newState)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			ODataBatchWriter.BatchWriterState newState2 = newState;
			switch (newState2)
			{
			case ODataBatchWriter.BatchWriterState.BatchStarted:
				break;
			case ODataBatchWriter.BatchWriterState.ChangeSetStarted:
				this.changeSetBoundary = ODataBatchWriterUtils.CreateChangeSetBoundary(this.rawOutputContext.WritingResponse);
				break;
			default:
				if (newState2 == ODataBatchWriter.BatchWriterState.ChangeSetCompleted)
				{
					this.changeSetBoundary = null;
				}
				break;
			}
			this.state = newState;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0004D824 File Offset: 0x0004BA24
		private void ValidateTransition(ODataBatchWriter.BatchWriterState newState)
		{
			if (!ODataBatchWriter.IsErrorState(this.state) && ODataBatchWriter.IsErrorState(newState))
			{
				return;
			}
			if (newState == ODataBatchWriter.BatchWriterState.ChangeSetStarted && this.changeSetBoundary != null)
			{
				throw new ODataException(Strings.ODataBatchWriter_CannotStartChangeSetWithActiveChangeSet);
			}
			if (newState == ODataBatchWriter.BatchWriterState.ChangeSetCompleted && this.changeSetBoundary == null)
			{
				throw new ODataException(Strings.ODataBatchWriter_CannotCompleteChangeSetWithoutActiveChangeSet);
			}
			if (newState == ODataBatchWriter.BatchWriterState.BatchCompleted && this.changeSetBoundary != null)
			{
				throw new ODataException(Strings.ODataBatchWriter_CannotCompleteBatchWithActiveChangeSet);
			}
			switch (this.state)
			{
			case ODataBatchWriter.BatchWriterState.Start:
				if (newState != ODataBatchWriter.BatchWriterState.BatchStarted)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromStart);
				}
				break;
			case ODataBatchWriter.BatchWriterState.BatchStarted:
				if (newState != ODataBatchWriter.BatchWriterState.ChangeSetStarted && newState != ODataBatchWriter.BatchWriterState.OperationCreated && newState != ODataBatchWriter.BatchWriterState.BatchCompleted)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromBatchStarted);
				}
				break;
			case ODataBatchWriter.BatchWriterState.ChangeSetStarted:
				if (newState != ODataBatchWriter.BatchWriterState.OperationCreated && newState != ODataBatchWriter.BatchWriterState.ChangeSetCompleted)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromChangeSetStarted);
				}
				break;
			case ODataBatchWriter.BatchWriterState.OperationCreated:
				if (newState != ODataBatchWriter.BatchWriterState.OperationCreated && newState != ODataBatchWriter.BatchWriterState.OperationStreamRequested && newState != ODataBatchWriter.BatchWriterState.ChangeSetStarted && newState != ODataBatchWriter.BatchWriterState.ChangeSetCompleted && newState != ODataBatchWriter.BatchWriterState.BatchCompleted)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromOperationCreated);
				}
				break;
			case ODataBatchWriter.BatchWriterState.OperationStreamRequested:
				if (newState != ODataBatchWriter.BatchWriterState.OperationStreamDisposed)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested);
				}
				break;
			case ODataBatchWriter.BatchWriterState.OperationStreamDisposed:
				if (newState != ODataBatchWriter.BatchWriterState.OperationCreated && newState != ODataBatchWriter.BatchWriterState.ChangeSetStarted && newState != ODataBatchWriter.BatchWriterState.ChangeSetCompleted && newState != ODataBatchWriter.BatchWriterState.BatchCompleted)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromOperationContentStreamDisposed);
				}
				break;
			case ODataBatchWriter.BatchWriterState.ChangeSetCompleted:
				if (newState != ODataBatchWriter.BatchWriterState.BatchCompleted && newState != ODataBatchWriter.BatchWriterState.ChangeSetStarted && newState != ODataBatchWriter.BatchWriterState.OperationCreated)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromChangeSetCompleted);
				}
				break;
			case ODataBatchWriter.BatchWriterState.BatchCompleted:
				throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromBatchCompleted);
			case ODataBatchWriter.BatchWriterState.Error:
				if (newState != ODataBatchWriter.BatchWriterState.Error)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromError(this.state.ToString(), newState.ToString()));
				}
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchWriter_ValidateTransition_UnreachableCodePath));
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0004D9C6 File Offset: 0x0004BBC6
		private void ValidateWriterReady()
		{
			this.rawOutputContext.VerifyNotDisposed();
			if (this.state == ODataBatchWriter.BatchWriterState.OperationStreamRequested)
			{
				throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested);
			}
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0004D9E8 File Offset: 0x0004BBE8
		private void WritePendingMessageData(bool reportMessageCompleted)
		{
			if (this.CurrentOperationMessage != null)
			{
				if (this.CurrentOperationResponseMessage != null)
				{
					int statusCode = this.CurrentOperationResponseMessage.StatusCode;
					string statusMessage = HttpUtils.GetStatusMessage(statusCode);
					this.rawOutputContext.TextWriter.WriteLine("{0} {1} {2}", "HTTP/1.1", statusCode, statusMessage);
				}
				bool flag = this.CurrentOperationRequestMessage != null && this.changeSetBoundary != null;
				string text = null;
				IEnumerable<KeyValuePair<string, string>> headers = this.CurrentOperationMessage.Headers;
				if (headers != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in headers)
					{
						string key = keyValuePair.Key;
						string value = keyValuePair.Value;
						this.rawOutputContext.TextWriter.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[] { key, value }));
						if (flag && string.CompareOrdinal("Content-ID", key) == 0)
						{
							text = value;
						}
					}
				}
				if (flag)
				{
					this.RememberContentIdHeader(text);
				}
				this.rawOutputContext.TextWriter.WriteLine();
				if (reportMessageCompleted)
				{
					this.CurrentOperationMessage.PartHeaderProcessingCompleted();
					this.CurrentOperationRequestMessage = null;
					this.CurrentOperationResponseMessage = null;
				}
			}
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0004DB38 File Offset: 0x0004BD38
		private void WriteStartBoundaryForOperation()
		{
			if (this.changeSetBoundary == null)
			{
				ODataBatchWriterUtils.WriteStartBoundary(this.rawOutputContext.TextWriter, this.batchBoundary, !this.batchStartBoundaryWritten);
				this.batchStartBoundaryWritten = true;
				return;
			}
			ODataBatchWriterUtils.WriteStartBoundary(this.rawOutputContext.TextWriter, this.changeSetBoundary, !this.changesetStartBoundaryWritten);
			this.changesetStartBoundaryWritten = true;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0004DB9A File Offset: 0x0004BD9A
		private void ThrowODataException(string errorMessage)
		{
			this.SetState(ODataBatchWriter.BatchWriterState.Error);
			throw new ODataException(errorMessage);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0004DBAC File Offset: 0x0004BDAC
		private void IncreaseBatchSize()
		{
			this.currentBatchSize += 1U;
			if ((ulong)this.currentBatchSize > (ulong)((long)this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxPartsPerBatch))
			{
				throw new ODataException(Strings.ODataBatchWriter_MaxBatchSizeExceeded(this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxPartsPerBatch));
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0004DC0C File Offset: 0x0004BE0C
		private void IncreaseChangeSetSize()
		{
			this.currentChangeSetSize += 1U;
			if ((ulong)this.currentChangeSetSize > (ulong)((long)this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxOperationsPerChangeset))
			{
				throw new ODataException(Strings.ODataBatchWriter_MaxChangeSetSizeExceeded(this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxOperationsPerChangeset));
			}
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0004DC6B File Offset: 0x0004BE6B
		private void ResetChangeSetSize()
		{
			this.currentChangeSetSize = 0U;
		}

		// Token: 0x04000740 RID: 1856
		private readonly ODataRawOutputContext rawOutputContext;

		// Token: 0x04000741 RID: 1857
		private readonly string batchBoundary;

		// Token: 0x04000742 RID: 1858
		private readonly ODataBatchUrlResolver urlResolver;

		// Token: 0x04000743 RID: 1859
		private ODataBatchWriter.BatchWriterState state;

		// Token: 0x04000744 RID: 1860
		private string changeSetBoundary;

		// Token: 0x04000745 RID: 1861
		private bool batchStartBoundaryWritten;

		// Token: 0x04000746 RID: 1862
		private bool changesetStartBoundaryWritten;

		// Token: 0x04000747 RID: 1863
		private ODataBatchOperationRequestMessage currentOperationRequestMessage;

		// Token: 0x04000748 RID: 1864
		private ODataBatchOperationResponseMessage currentOperationResponseMessage;

		// Token: 0x04000749 RID: 1865
		private string currentOperationContentId;

		// Token: 0x0400074A RID: 1866
		private uint currentBatchSize;

		// Token: 0x0400074B RID: 1867
		private uint currentChangeSetSize;

		// Token: 0x02000272 RID: 626
		private enum BatchWriterState
		{
			// Token: 0x0400074D RID: 1869
			Start,
			// Token: 0x0400074E RID: 1870
			BatchStarted,
			// Token: 0x0400074F RID: 1871
			ChangeSetStarted,
			// Token: 0x04000750 RID: 1872
			OperationCreated,
			// Token: 0x04000751 RID: 1873
			OperationStreamRequested,
			// Token: 0x04000752 RID: 1874
			OperationStreamDisposed,
			// Token: 0x04000753 RID: 1875
			ChangeSetCompleted,
			// Token: 0x04000754 RID: 1876
			BatchCompleted,
			// Token: 0x04000755 RID: 1877
			Error
		}
	}
}
