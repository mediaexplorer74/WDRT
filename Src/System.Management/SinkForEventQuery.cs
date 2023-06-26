using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Management
{
	// Token: 0x02000018 RID: 24
	internal class SinkForEventQuery : IWmiEventSource
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000047C0 File Offset: 0x000029C0
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000047C8 File Offset: 0x000029C8
		public int Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000047D4 File Offset: 0x000029D4
		public SinkForEventQuery(ManagementEventWatcher eventWatcher, object context, IWbemServices services)
		{
			this.services = services;
			this.context = context;
			this.eventWatcher = eventWatcher;
			this.status = 0;
			this.isLocal = false;
			if (string.Compare(eventWatcher.Scope.Path.Server, ".", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(eventWatcher.Scope.Path.Server, Environment.MachineName, StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.isLocal = true;
			}
			if (MTAHelper.IsNoContextMTA())
			{
				this.HackToCreateStubInMTA(this);
				return;
			}
			new ThreadDispatch(new ThreadDispatch.ThreadWorkerMethodWithParam(this.HackToCreateStubInMTA))
			{
				Parameter = this
			}.Start();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000487C File Offset: 0x00002A7C
		private void HackToCreateStubInMTA(object param)
		{
			SinkForEventQuery sinkForEventQuery = (SinkForEventQuery)param;
			object obj = null;
			sinkForEventQuery.Status = WmiNetUtilsHelper.GetDemultiplexedStub_f(sinkForEventQuery, sinkForEventQuery.isLocal, out obj);
			sinkForEventQuery.stub = (IWbemObjectSink)obj;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000048B7 File Offset: 0x00002AB7
		internal IWbemObjectSink Stub
		{
			get
			{
				return this.stub;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000048C0 File Offset: 0x00002AC0
		public void Indicate(IntPtr pWbemClassObject)
		{
			Marshal.AddRef(pWbemClassObject);
			IWbemClassObjectFreeThreaded wbemClassObjectFreeThreaded = new IWbemClassObjectFreeThreaded(pWbemClassObject);
			try
			{
				EventArrivedEventArgs eventArrivedEventArgs = new EventArrivedEventArgs(this.context, new ManagementBaseObject(wbemClassObjectFreeThreaded));
				this.eventWatcher.FireEventArrived(eventArrivedEventArgs);
			}
			catch
			{
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004910 File Offset: 0x00002B10
		public void SetStatus(int flags, int hResult, string message, IntPtr pErrObj)
		{
			try
			{
				this.eventWatcher.FireStopped(new StoppedEventArgs(this.context, hResult));
				if (hResult != -2147217358 && hResult != 262150)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.Cancel2));
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000496C File Offset: 0x00002B6C
		private void Cancel2(object o)
		{
			try
			{
				this.Cancel();
			}
			catch
			{
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004994 File Offset: 0x00002B94
		internal void Cancel()
		{
			this.CancelInternal(false);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000049A0 File Offset: 0x00002BA0
		internal void CancelInternal(bool inEventWatcherFinalizer)
		{
			if (this.stub != null)
			{
				lock (this)
				{
					if (this.stub != null)
					{
						int num = this.services.CancelAsyncCall_(this.stub);
						this.ReleaseStub();
						if (num < 0 && (!inEventWatcherFinalizer || !Environment.HasShutdownStarted || AppDomain.CurrentDomain.IsFinalizingForUnload()))
						{
							if (((long)num & (long)((ulong)(-4096))) == (long)((ulong)(-2147217408)))
							{
								ManagementException.ThrowWithExtendedInfo((ManagementStatus)num);
							}
							else
							{
								Marshal.ThrowExceptionForHR(num, WmiNetUtilsHelper.GetErrorInfo_f());
							}
						}
					}
				}
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004A50 File Offset: 0x00002C50
		internal void ReleaseStub()
		{
			if (this.stub != null)
			{
				lock (this)
				{
					if (this.stub != null)
					{
						try
						{
							Marshal.ReleaseComObject(this.stub);
							this.stub = null;
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x04000091 RID: 145
		private ManagementEventWatcher eventWatcher;

		// Token: 0x04000092 RID: 146
		private object context;

		// Token: 0x04000093 RID: 147
		private IWbemServices services;

		// Token: 0x04000094 RID: 148
		private IWbemObjectSink stub;

		// Token: 0x04000095 RID: 149
		private int status;

		// Token: 0x04000096 RID: 150
		private bool isLocal;
	}
}
