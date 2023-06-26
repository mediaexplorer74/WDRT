using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Data.OData
{
	// Token: 0x02000261 RID: 609
	internal abstract class ODataMessage
	{
		// Token: 0x06001419 RID: 5145 RVA: 0x0004B756 File Offset: 0x00049956
		protected ODataMessage(bool writing, bool disableMessageStreamDisposal, long maxMessageSize)
		{
			this.writing = writing;
			this.disableMessageStreamDisposal = disableMessageStreamDisposal;
			this.maxMessageSize = maxMessageSize;
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600141A RID: 5146
		public abstract IEnumerable<KeyValuePair<string, string>> Headers { get; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0004B773 File Offset: 0x00049973
		protected internal BufferingReadStream BufferingReadStream
		{
			get
			{
				return this.bufferingReadStream;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0004B77B File Offset: 0x0004997B
		// (set) Token: 0x0600141D RID: 5149 RVA: 0x0004B783 File Offset: 0x00049983
		protected internal bool? UseBufferingReadStream
		{
			get
			{
				return this.useBufferingReadStream;
			}
			set
			{
				this.useBufferingReadStream = value;
			}
		}

		// Token: 0x0600141E RID: 5150
		public abstract string GetHeader(string headerName);

		// Token: 0x0600141F RID: 5151
		public abstract void SetHeader(string headerName, string headerValue);

		// Token: 0x06001420 RID: 5152
		public abstract Stream GetStream();

		// Token: 0x06001421 RID: 5153
		public abstract Task<Stream> GetStreamAsync();

		// Token: 0x06001422 RID: 5154
		internal abstract TInterface QueryInterface<TInterface>() where TInterface : class;

		// Token: 0x06001423 RID: 5155 RVA: 0x0004B78C File Offset: 0x0004998C
		protected internal Stream GetStream(Func<Stream> messageStreamFunc, bool isRequest)
		{
			if (!this.writing)
			{
				BufferingReadStream bufferingReadStream = this.TryGetBufferingReadStream();
				if (bufferingReadStream != null)
				{
					return bufferingReadStream;
				}
			}
			Stream stream = messageStreamFunc();
			ODataMessage.ValidateMessageStream(stream, isRequest);
			bool flag = !this.writing && this.maxMessageSize > 0L;
			if (this.disableMessageStreamDisposal && flag)
			{
				stream = MessageStreamWrapper.CreateNonDisposingStreamWithMaxSize(stream, this.maxMessageSize);
			}
			else if (this.disableMessageStreamDisposal)
			{
				stream = MessageStreamWrapper.CreateNonDisposingStream(stream);
			}
			else if (flag)
			{
				stream = MessageStreamWrapper.CreateStreamWithMaxSize(stream, this.maxMessageSize);
			}
			if (!this.writing && this.useBufferingReadStream == true)
			{
				this.bufferingReadStream = new BufferingReadStream(stream);
				stream = this.bufferingReadStream;
			}
			return stream;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0004B92C File Offset: 0x00049B2C
		protected internal Task<Stream> GetStreamAsync(Func<Task<Stream>> streamFuncAsync, bool isRequest)
		{
			if (!this.writing)
			{
				Stream stream = this.TryGetBufferingReadStream();
				if (stream != null)
				{
					return TaskUtils.GetCompletedTask<Stream>(stream);
				}
			}
			Task<Stream> task = streamFuncAsync();
			ODataMessage.ValidateMessageStreamTask(task, isRequest);
			task = task.FollowOnSuccessWith(delegate(Task<Stream> streamTask)
			{
				Stream stream2 = streamTask.Result;
				ODataMessage.ValidateMessageStream(stream2, isRequest);
				bool flag = !this.writing && this.maxMessageSize > 0L;
				if (this.disableMessageStreamDisposal && flag)
				{
					stream2 = MessageStreamWrapper.CreateNonDisposingStreamWithMaxSize(stream2, this.maxMessageSize);
				}
				else if (this.disableMessageStreamDisposal)
				{
					stream2 = MessageStreamWrapper.CreateNonDisposingStream(stream2);
				}
				else if (flag)
				{
					stream2 = MessageStreamWrapper.CreateStreamWithMaxSize(stream2, this.maxMessageSize);
				}
				return stream2;
			});
			if (!this.writing)
			{
				task = task.FollowOnSuccessWithTask((Task<Stream> streamTask) => BufferedReadStream.BufferStreamAsync(streamTask.Result)).FollowOnSuccessWith((Task<BufferedReadStream> streamTask) => streamTask.Result);
				if (this.useBufferingReadStream == true)
				{
					task = task.FollowOnSuccessWith(delegate(Task<Stream> streamTask)
					{
						Stream result = streamTask.Result;
						this.bufferingReadStream = new BufferingReadStream(result);
						return this.bufferingReadStream;
					});
				}
			}
			return task;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0004BA12 File Offset: 0x00049C12
		protected void VerifyCanSetHeader()
		{
			if (!this.writing)
			{
				throw new ODataException(Strings.ODataMessage_MustNotModifyMessage);
			}
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0004BA28 File Offset: 0x00049C28
		private static void ValidateMessageStream(Stream stream, bool isRequest)
		{
			if (stream == null)
			{
				string text = (isRequest ? Strings.ODataRequestMessage_MessageStreamIsNull : Strings.ODataResponseMessage_MessageStreamIsNull);
				throw new ODataException(text);
			}
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0004BA50 File Offset: 0x00049C50
		private static void ValidateMessageStreamTask(Task<Stream> streamTask, bool isRequest)
		{
			if (streamTask == null)
			{
				string text = (isRequest ? Strings.ODataRequestMessage_StreamTaskIsNull : Strings.ODataResponseMessage_StreamTaskIsNull);
				throw new ODataException(text);
			}
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0004BA78 File Offset: 0x00049C78
		private BufferingReadStream TryGetBufferingReadStream()
		{
			if (this.bufferingReadStream == null)
			{
				return null;
			}
			BufferingReadStream bufferingReadStream = this.bufferingReadStream;
			if (this.bufferingReadStream.IsBuffering)
			{
				this.bufferingReadStream.ResetStream();
			}
			else
			{
				this.bufferingReadStream = null;
			}
			return bufferingReadStream;
		}

		// Token: 0x04000721 RID: 1825
		private readonly bool writing;

		// Token: 0x04000722 RID: 1826
		private readonly bool disableMessageStreamDisposal;

		// Token: 0x04000723 RID: 1827
		private readonly long maxMessageSize;

		// Token: 0x04000724 RID: 1828
		private bool? useBufferingReadStream;

		// Token: 0x04000725 RID: 1829
		private BufferingReadStream bufferingReadStream;
	}
}
