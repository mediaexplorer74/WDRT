using System;
using System.Threading;
using System.Threading.Tasks;
using Nokia.Mira.IO;
using Nokia.Mira.Primitives;

namespace Nokia.Mira.Strategies
{
	// Token: 0x02000032 RID: 50
	internal sealed class ChunklessDownloadStrategy : IDownloadStrategy
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00004550 File Offset: 0x00002750
		public ChunklessDownloadStrategy(IWebResponse response, IFileStreamFactory fileStreamFactory, CancellationToken token, Action<long> reportBytesDownloaded, Action<long> reportBytesWritten)
		{
			ChunklessDownloadStrategy.DisplayClass4 f8__locals1 
				= new ChunklessDownloadStrategy.DisplayClass4();
			f8__locals1.response = response;
			f8__locals1.fileStreamFactory = fileStreamFactory;
			f8__locals1.token = token;
			f8__locals1.reportBytesDownloaded = reportBytesDownloaded;
			f8__locals1.reportBytesWritten = reportBytesWritten;
			
			//RnD
			//base..ctor();
			
			long bytesWritten = 0L;
			Action<long> bytesWrittenAction = delegate(long v)
			{
				bytesWritten += v;
				f8__locals1.reportBytesWritten(bytesWritten);
				f8__locals1.reportBytesDownloaded(v);
			};
			this.taskLazy = new Lazy<Task>(() => Task.Factory.StartNew(
				delegate()
			{
				f8__locals1.response.DownloadResponseStream(
					f8__locals1.token, f8__locals1.fileStreamFactory, 
					bytesWrittenAction);
			}, f8__locals1.token).ContinueWith<Task>(delegate(Task t)
			{
				f8__locals1.response.Close();
				return t;
			}).Unwrap());
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000045CC File Offset: 0x000027CC
		public ChunkStatus GetNextChunk(out Lazy<Task> chunk)
		{
			chunk = this.taskLazy;
			if (this.taskLazy == null)
			{
				return ChunkStatus.RanToCompletion;
			}
			this.taskLazy = null;
			return ChunkStatus.WaitingToCompletion;
		}

		// Token: 0x04000072 RID: 114
		private Lazy<Task> taskLazy;

        private class DisplayClass4
        {
            internal IWebResponse response;
            internal IFileStreamFactory fileStreamFactory;
            internal CancellationToken token;
            internal Action<long> reportBytesDownloaded;
            internal Action<long> reportBytesWritten;

            public DisplayClass4()
            {
            }
        }
    }
}
