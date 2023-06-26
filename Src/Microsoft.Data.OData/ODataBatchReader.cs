using System;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x0200024D RID: 589
	public sealed class ODataBatchReader : IODataBatchOperationListener
	{
		// Token: 0x060012E1 RID: 4833 RVA: 0x00046FED File Offset: 0x000451ED
		internal ODataBatchReader(ODataRawInputContext inputContext, string batchBoundary, Encoding batchEncoding, bool synchronous)
		{
			this.inputContext = inputContext;
			this.synchronous = synchronous;
			this.urlResolver = new ODataBatchUrlResolver(inputContext.UrlResolver);
			this.batchStream = new ODataBatchReaderStream(inputContext, batchBoundary, batchEncoding);
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x00047023 File Offset: 0x00045223
		// (set) Token: 0x060012E3 RID: 4835 RVA: 0x00047036 File Offset: 0x00045236
		public ODataBatchReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.batchReaderState;
			}
			private set
			{
				this.batchReaderState = value;
			}
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0004703F File Offset: 0x0004523F
		public bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00047063 File Offset: 0x00045263
		public Task<bool> ReadAsync()
		{
			this.VerifyCanRead(false);
			return this.ReadAsynchronously().FollowOnFaultWith(delegate(Task<bool> t)
			{
				this.State = ODataBatchReaderState.Exception;
			});
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00047083 File Offset: 0x00045283
		public ODataBatchOperationRequestMessage CreateOperationRequestMessage()
		{
			this.VerifyCanCreateOperationRequestMessage(true);
			return this.InterceptException<ODataBatchOperationRequestMessage>(new Func<ODataBatchOperationRequestMessage>(this.CreateOperationRequestMessageImplementation));
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000470A7 File Offset: 0x000452A7
		public Task<ODataBatchOperationRequestMessage> CreateOperationRequestMessageAsync()
		{
			this.VerifyCanCreateOperationRequestMessage(false);
			return TaskUtils.GetTaskForSynchronousOperation<ODataBatchOperationRequestMessage>(new Func<ODataBatchOperationRequestMessage>(this.CreateOperationRequestMessageImplementation)).FollowOnFaultWith(delegate(Task<ODataBatchOperationRequestMessage> t)
			{
				this.State = ODataBatchReaderState.Exception;
			});
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000470D2 File Offset: 0x000452D2
		public ODataBatchOperationResponseMessage CreateOperationResponseMessage()
		{
			this.VerifyCanCreateOperationResponseMessage(true);
			return this.InterceptException<ODataBatchOperationResponseMessage>(new Func<ODataBatchOperationResponseMessage>(this.CreateOperationResponseMessageImplementation));
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x000470F6 File Offset: 0x000452F6
		public Task<ODataBatchOperationResponseMessage> CreateOperationResponseMessageAsync()
		{
			this.VerifyCanCreateOperationResponseMessage(false);
			return TaskUtils.GetTaskForSynchronousOperation<ODataBatchOperationResponseMessage>(new Func<ODataBatchOperationResponseMessage>(this.CreateOperationResponseMessageImplementation)).FollowOnFaultWith(delegate(Task<ODataBatchOperationResponseMessage> t)
			{
				this.State = ODataBatchReaderState.Exception;
			});
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00047121 File Offset: 0x00045321
		void IODataBatchOperationListener.BatchOperationContentStreamRequested()
		{
			this.operationState = ODataBatchReader.OperationState.StreamRequested;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0004712A File Offset: 0x0004532A
		Task IODataBatchOperationListener.BatchOperationContentStreamRequestedAsync()
		{
			this.operationState = ODataBatchReader.OperationState.StreamRequested;
			return TaskUtils.CompletedTask;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00047138 File Offset: 0x00045338
		void IODataBatchOperationListener.BatchOperationContentStreamDisposed()
		{
			this.operationState = ODataBatchReader.OperationState.StreamDisposed;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00047144 File Offset: 0x00045344
		private ODataBatchReaderState GetEndBoundaryState()
		{
			switch (this.batchReaderState)
			{
			case ODataBatchReaderState.Initial:
				return ODataBatchReaderState.Completed;
			case ODataBatchReaderState.Operation:
				if (this.batchStream.ChangeSetBoundary != null)
				{
					return ODataBatchReaderState.ChangesetEnd;
				}
				return ODataBatchReaderState.Completed;
			case ODataBatchReaderState.ChangesetStart:
				return ODataBatchReaderState.ChangesetEnd;
			case ODataBatchReaderState.ChangesetEnd:
				return ODataBatchReaderState.Completed;
			case ODataBatchReaderState.Completed:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_GetEndBoundary_Completed));
			case ODataBatchReaderState.Exception:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_GetEndBoundary_Exception));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_GetEndBoundary_UnknownValue));
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x000471C4 File Offset: 0x000453C4
		private bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000471CC File Offset: 0x000453CC
		private Task<bool> ReadAsynchronously()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadImplementation));
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x000471E0 File Offset: 0x000453E0
		private bool ReadImplementation()
		{
			switch (this.State)
			{
			case ODataBatchReaderState.Initial:
				this.batchReaderState = this.SkipToNextPartAndReadHeaders();
				break;
			case ODataBatchReaderState.Operation:
				if (this.operationState == ODataBatchReader.OperationState.None)
				{
					throw new ODataException(Strings.ODataBatchReader_NoMessageWasCreatedForOperation);
				}
				this.operationState = ODataBatchReader.OperationState.None;
				if (this.contentIdToAddOnNextRead != null)
				{
					this.urlResolver.AddContentId(this.contentIdToAddOnNextRead);
					this.contentIdToAddOnNextRead = null;
				}
				this.batchReaderState = this.SkipToNextPartAndReadHeaders();
				break;
			case ODataBatchReaderState.ChangesetStart:
				this.batchReaderState = this.SkipToNextPartAndReadHeaders();
				break;
			case ODataBatchReaderState.ChangesetEnd:
				this.ResetChangeSetSize();
				this.batchStream.ResetChangeSetBoundary();
				this.batchReaderState = this.SkipToNextPartAndReadHeaders();
				break;
			case ODataBatchReaderState.Completed:
			case ODataBatchReaderState.Exception:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_ReadImplementation));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_ReadImplementation));
			}
			return this.batchReaderState != ODataBatchReaderState.Completed && this.batchReaderState != ODataBatchReaderState.Exception;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x000472D8 File Offset: 0x000454D8
		private ODataBatchReaderState SkipToNextPartAndReadHeaders()
		{
			bool flag;
			bool flag2;
			if (this.batchStream.SkipToBoundary(out flag, out flag2))
			{
				ODataBatchReaderState odataBatchReaderState;
				if (flag || flag2)
				{
					odataBatchReaderState = this.GetEndBoundaryState();
					if (odataBatchReaderState == ODataBatchReaderState.ChangesetEnd)
					{
						this.urlResolver.Reset();
					}
				}
				else
				{
					bool flag3 = this.batchStream.ChangeSetBoundary != null;
					bool flag4 = this.batchStream.ProcessPartHeader();
					if (flag3)
					{
						odataBatchReaderState = ODataBatchReaderState.Operation;
						this.IncreaseChangeSetSize();
					}
					else
					{
						odataBatchReaderState = (flag4 ? ODataBatchReaderState.ChangesetStart : ODataBatchReaderState.Operation);
						this.IncreaseBatchSize();
					}
				}
				return odataBatchReaderState;
			}
			if (this.batchStream.ChangeSetBoundary == null)
			{
				return ODataBatchReaderState.Completed;
			}
			return ODataBatchReaderState.ChangesetEnd;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00047368 File Offset: 0x00045568
		private ODataBatchOperationRequestMessage CreateOperationRequestMessageImplementation()
		{
			this.operationState = ODataBatchReader.OperationState.MessageCreated;
			string text = this.batchStream.ReadFirstNonEmptyLine();
			string text2;
			Uri uri;
			this.ParseRequestLine(text, out text2, out uri);
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.batchStream.ReadHeaders();
			ODataBatchOperationRequestMessage odataBatchOperationRequestMessage = ODataBatchOperationRequestMessage.CreateReadMessage(this.batchStream, text2, uri, odataBatchOperationHeaders, this, this.urlResolver);
			string text3;
			if (odataBatchOperationHeaders.TryGetValue("Content-ID", out text3))
			{
				if (text3 != null && this.urlResolver.ContainsContentId(text3))
				{
					throw new ODataException(Strings.ODataBatchReader_DuplicateContentIDsNotAllowed(text3));
				}
				this.contentIdToAddOnNextRead = text3;
			}
			return odataBatchOperationRequestMessage;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000473F0 File Offset: 0x000455F0
		private ODataBatchOperationResponseMessage CreateOperationResponseMessageImplementation()
		{
			this.operationState = ODataBatchReader.OperationState.MessageCreated;
			string text = this.batchStream.ReadFirstNonEmptyLine();
			int num = this.ParseResponseLine(text);
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.batchStream.ReadHeaders();
			return ODataBatchOperationResponseMessage.CreateReadMessage(this.batchStream, num, odataBatchOperationHeaders, this, this.urlResolver.BatchMessageUrlResolver);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00047440 File Offset: 0x00045640
		private void ParseRequestLine(string requestLine, out string httpMethod, out Uri requestUri)
		{
			int num = requestLine.IndexOf(' ');
			if (num <= 0 || requestLine.Length - 3 <= num)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidRequestLine(requestLine));
			}
			int num2 = requestLine.LastIndexOf(' ');
			if (num2 < 0 || num2 - num - 1 <= 0 || requestLine.Length - 1 <= num2)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidRequestLine(requestLine));
			}
			httpMethod = requestLine.Substring(0, num);
			string text = requestLine.Substring(num + 1, num2 - num - 1);
			string text2 = requestLine.Substring(num2 + 1);
			if (string.CompareOrdinal("HTTP/1.1", text2) != 0)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidHttpVersionSpecified(text2, "HTTP/1.1"));
			}
			HttpUtils.ValidateHttpMethod(httpMethod);
			if (this.batchStream.ChangeSetBoundary == null)
			{
				if (!HttpUtils.IsQueryMethod(httpMethod))
				{
					throw new ODataException(Strings.ODataBatch_InvalidHttpMethodForQueryOperation(httpMethod));
				}
			}
			else if (HttpUtils.IsQueryMethod(httpMethod))
			{
				throw new ODataException(Strings.ODataBatch_InvalidHttpMethodForChangeSetRequest(httpMethod));
			}
			requestUri = new Uri(text, UriKind.RelativeOrAbsolute);
			requestUri = ODataBatchUtils.CreateOperationRequestUri(requestUri, this.inputContext.MessageReaderSettings.BaseUri, this.urlResolver);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00047548 File Offset: 0x00045748
		private int ParseResponseLine(string responseLine)
		{
			int num = responseLine.IndexOf(' ');
			if (num <= 0 || responseLine.Length - 3 <= num)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidResponseLine(responseLine));
			}
			int num2 = responseLine.IndexOf(' ', num + 1);
			if (num2 < 0 || num2 - num - 1 <= 0 || responseLine.Length - 1 <= num2)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidResponseLine(responseLine));
			}
			string text = responseLine.Substring(0, num);
			string text2 = responseLine.Substring(num + 1, num2 - num - 1);
			if (string.CompareOrdinal("HTTP/1.1", text) != 0)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidHttpVersionSpecified(text, "HTTP/1.1"));
			}
			int num3;
			if (!int.TryParse(text2, out num3))
			{
				throw new ODataException(Strings.ODataBatchReaderStream_NonIntegerHttpStatusCode(text2));
			}
			return num3;
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000475F8 File Offset: 0x000457F8
		private void VerifyCanCreateOperationRequestMessage(bool synchronousCall)
		{
			this.VerifyReaderReady();
			this.VerifyCallAllowed(synchronousCall);
			if (this.inputContext.ReadingResponse)
			{
				this.ThrowODataException(Strings.ODataBatchReader_CannotCreateRequestOperationWhenReadingResponse);
			}
			if (this.State != ODataBatchReaderState.Operation)
			{
				this.ThrowODataException(Strings.ODataBatchReader_InvalidStateForCreateOperationRequestMessage(this.State));
			}
			if (this.operationState != ODataBatchReader.OperationState.None)
			{
				this.ThrowODataException(Strings.ODataBatchReader_OperationRequestMessageAlreadyCreated);
			}
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0004765C File Offset: 0x0004585C
		private void VerifyCanCreateOperationResponseMessage(bool synchronousCall)
		{
			this.VerifyReaderReady();
			this.VerifyCallAllowed(synchronousCall);
			if (!this.inputContext.ReadingResponse)
			{
				this.ThrowODataException(Strings.ODataBatchReader_CannotCreateResponseOperationWhenReadingRequest);
			}
			if (this.State != ODataBatchReaderState.Operation)
			{
				this.ThrowODataException(Strings.ODataBatchReader_InvalidStateForCreateOperationResponseMessage(this.State));
			}
			if (this.operationState != ODataBatchReader.OperationState.None)
			{
				this.ThrowODataException(Strings.ODataBatchReader_OperationResponseMessageAlreadyCreated);
			}
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000476C0 File Offset: 0x000458C0
		private void VerifyCanRead(bool synchronousCall)
		{
			this.VerifyReaderReady();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataBatchReaderState.Exception || this.State == ODataBatchReaderState.Completed)
			{
				throw new ODataException(Strings.ODataBatchReader_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000476F7 File Offset: 0x000458F7
		private void VerifyReaderReady()
		{
			this.inputContext.VerifyNotDisposed();
			if (this.operationState == ODataBatchReader.OperationState.StreamRequested)
			{
				throw new ODataException(Strings.ODataBatchReader_CannotUseReaderWhileOperationStreamActive);
			}
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00047718 File Offset: 0x00045918
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				if (!this.synchronous)
				{
					throw new ODataException(Strings.ODataBatchReader_SyncCallOnAsyncReader);
				}
			}
			else if (this.synchronous)
			{
				throw new ODataException(Strings.ODataBatchReader_AsyncCallOnSyncReader);
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00047744 File Offset: 0x00045944
		private void IncreaseBatchSize()
		{
			this.currentBatchSize += 1U;
			if ((ulong)this.currentBatchSize > (ulong)((long)this.inputContext.MessageReaderSettings.MessageQuotas.MaxPartsPerBatch))
			{
				throw new ODataException(Strings.ODataBatchReader_MaxBatchSizeExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxPartsPerBatch));
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000477A4 File Offset: 0x000459A4
		private void IncreaseChangeSetSize()
		{
			this.currentChangeSetSize += 1U;
			if ((ulong)this.currentChangeSetSize > (ulong)((long)this.inputContext.MessageReaderSettings.MessageQuotas.MaxOperationsPerChangeset))
			{
				throw new ODataException(Strings.ODataBatchReader_MaxChangeSetSizeExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxOperationsPerChangeset));
			}
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00047803 File Offset: 0x00045A03
		private void ResetChangeSetSize()
		{
			this.currentChangeSetSize = 0U;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0004780C File Offset: 0x00045A0C
		private void ThrowODataException(string errorMessage)
		{
			this.State = ODataBatchReaderState.Exception;
			throw new ODataException(errorMessage);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0004781C File Offset: 0x00045A1C
		private T InterceptException<T>(Func<T> action)
		{
			T t;
			try
			{
				t = action();
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex))
				{
					this.State = ODataBatchReaderState.Exception;
				}
				throw;
			}
			return t;
		}

		// Token: 0x040006C3 RID: 1731
		private readonly ODataRawInputContext inputContext;

		// Token: 0x040006C4 RID: 1732
		private readonly ODataBatchReaderStream batchStream;

		// Token: 0x040006C5 RID: 1733
		private readonly bool synchronous;

		// Token: 0x040006C6 RID: 1734
		private readonly ODataBatchUrlResolver urlResolver;

		// Token: 0x040006C7 RID: 1735
		private ODataBatchReaderState batchReaderState;

		// Token: 0x040006C8 RID: 1736
		private uint currentBatchSize;

		// Token: 0x040006C9 RID: 1737
		private uint currentChangeSetSize;

		// Token: 0x040006CA RID: 1738
		private ODataBatchReader.OperationState operationState;

		// Token: 0x040006CB RID: 1739
		private string contentIdToAddOnNextRead;

		// Token: 0x0200024E RID: 590
		private enum OperationState
		{
			// Token: 0x040006CD RID: 1741
			None,
			// Token: 0x040006CE RID: 1742
			MessageCreated,
			// Token: 0x040006CF RID: 1743
			StreamRequested,
			// Token: 0x040006D0 RID: 1744
			StreamDisposed
		}
	}
}
