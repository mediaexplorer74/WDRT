using System;
using System.Threading;

namespace System.Management
{
	// Token: 0x020000A7 RID: 167
	internal class ThreadDispatch
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00021BC7 File Offset: 0x0001FDC7
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00021BCF File Offset: 0x0001FDCF
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x00021BD7 File Offset: 0x0001FDD7
		public object Parameter
		{
			get
			{
				return this.threadParams;
			}
			set
			{
				this.threadParams = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00021BE0 File Offset: 0x0001FDE0
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00021BE8 File Offset: 0x0001FDE8
		public bool IsBackgroundThread
		{
			get
			{
				return this.backgroundThread;
			}
			set
			{
				this.backgroundThread = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00021BF1 File Offset: 0x0001FDF1
		public object Result
		{
			get
			{
				return this.threadReturn;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00021BF9 File Offset: 0x0001FDF9
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x00021C01 File Offset: 0x0001FE01
		public ApartmentState ApartmentType
		{
			get
			{
				return this.apartmentType;
			}
			set
			{
				this.apartmentType = value;
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00021C0A File Offset: 0x0001FE0A
		public ThreadDispatch(ThreadDispatch.ThreadWorkerMethodWithReturn workerMethod)
			: this()
		{
			this.InitializeThreadState(null, workerMethod, ApartmentState.MTA, false);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00021C1C File Offset: 0x0001FE1C
		public ThreadDispatch(ThreadDispatch.ThreadWorkerMethodWithReturnAndParam workerMethod)
			: this()
		{
			this.InitializeThreadState(null, workerMethod, ApartmentState.MTA, false);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00021C2E File Offset: 0x0001FE2E
		public ThreadDispatch(ThreadDispatch.ThreadWorkerMethodWithParam workerMethod)
			: this()
		{
			this.InitializeThreadState(null, workerMethod, ApartmentState.MTA, false);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00021C40 File Offset: 0x0001FE40
		public ThreadDispatch(ThreadDispatch.ThreadWorkerMethod workerMethod)
			: this()
		{
			this.InitializeThreadState(null, workerMethod, ApartmentState.MTA, false);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00021C52 File Offset: 0x0001FE52
		public void Start()
		{
			this.exception = null;
			this.DispatchThread();
			if (this.Exception != null)
			{
				throw this.Exception;
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00021C70 File Offset: 0x0001FE70
		private ThreadDispatch()
		{
			this.thread = null;
			this.exception = null;
			this.threadParams = null;
			this.threadWorkerMethodWithReturn = null;
			this.threadWorkerMethodWithReturnAndParam = null;
			this.threadWorkerMethod = null;
			this.threadWorkerMethodWithParam = null;
			this.threadReturn = null;
			this.backgroundThread = false;
			this.apartmentType = ApartmentState.MTA;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00021CC9 File Offset: 0x0001FEC9
		private void InitializeThreadState(object threadParams, ThreadDispatch.ThreadWorkerMethodWithReturn workerMethod, ApartmentState aptState, bool background)
		{
			this.threadParams = threadParams;
			this.threadWorkerMethodWithReturn = workerMethod;
			this.thread = new Thread(new ThreadStart(this.ThreadEntryPointMethodWithReturn));
			this.thread.SetApartmentState(aptState);
			this.backgroundThread = background;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00021D04 File Offset: 0x0001FF04
		private void InitializeThreadState(object threadParams, ThreadDispatch.ThreadWorkerMethodWithReturnAndParam workerMethod, ApartmentState aptState, bool background)
		{
			this.threadParams = threadParams;
			this.threadWorkerMethodWithReturnAndParam = workerMethod;
			this.thread = new Thread(new ThreadStart(this.ThreadEntryPointMethodWithReturnAndParam));
			this.thread.SetApartmentState(aptState);
			this.backgroundThread = background;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00021D3F File Offset: 0x0001FF3F
		private void InitializeThreadState(object threadParams, ThreadDispatch.ThreadWorkerMethod workerMethod, ApartmentState aptState, bool background)
		{
			this.threadParams = threadParams;
			this.threadWorkerMethod = workerMethod;
			this.thread = new Thread(new ThreadStart(this.ThreadEntryPoint));
			this.thread.SetApartmentState(aptState);
			this.backgroundThread = background;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00021D7A File Offset: 0x0001FF7A
		private void InitializeThreadState(object threadParams, ThreadDispatch.ThreadWorkerMethodWithParam workerMethod, ApartmentState aptState, bool background)
		{
			this.threadParams = threadParams;
			this.threadWorkerMethodWithParam = workerMethod;
			this.thread = new Thread(new ThreadStart(this.ThreadEntryPointMethodWithParam));
			this.thread.SetApartmentState(aptState);
			this.backgroundThread = background;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00021DB5 File Offset: 0x0001FFB5
		private void DispatchThread()
		{
			this.thread.Start();
			this.thread.Join();
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00021DD0 File Offset: 0x0001FFD0
		private void ThreadEntryPoint()
		{
			try
			{
				this.threadWorkerMethod();
			}
			catch (Exception ex)
			{
				this.exception = ex;
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00021E04 File Offset: 0x00020004
		private void ThreadEntryPointMethodWithParam()
		{
			try
			{
				this.threadWorkerMethodWithParam(this.threadParams);
			}
			catch (Exception ex)
			{
				this.exception = ex;
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00021E40 File Offset: 0x00020040
		private void ThreadEntryPointMethodWithReturn()
		{
			try
			{
				this.threadReturn = this.threadWorkerMethodWithReturn();
			}
			catch (Exception ex)
			{
				this.exception = ex;
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00021E7C File Offset: 0x0002007C
		private void ThreadEntryPointMethodWithReturnAndParam()
		{
			try
			{
				this.threadReturn = this.threadWorkerMethodWithReturnAndParam(this.threadParams);
			}
			catch (Exception ex)
			{
				this.exception = ex;
			}
		}

		// Token: 0x0400048E RID: 1166
		private Thread thread;

		// Token: 0x0400048F RID: 1167
		private Exception exception;

		// Token: 0x04000490 RID: 1168
		private ThreadDispatch.ThreadWorkerMethodWithReturn threadWorkerMethodWithReturn;

		// Token: 0x04000491 RID: 1169
		private ThreadDispatch.ThreadWorkerMethodWithReturnAndParam threadWorkerMethodWithReturnAndParam;

		// Token: 0x04000492 RID: 1170
		private ThreadDispatch.ThreadWorkerMethod threadWorkerMethod;

		// Token: 0x04000493 RID: 1171
		private ThreadDispatch.ThreadWorkerMethodWithParam threadWorkerMethodWithParam;

		// Token: 0x04000494 RID: 1172
		private object threadReturn;

		// Token: 0x04000495 RID: 1173
		private object threadParams;

		// Token: 0x04000496 RID: 1174
		private bool backgroundThread;

		// Token: 0x04000497 RID: 1175
		private ApartmentState apartmentType;

		// Token: 0x02000103 RID: 259
		// (Invoke) Token: 0x06000661 RID: 1633
		public delegate object ThreadWorkerMethodWithReturn();

		// Token: 0x02000104 RID: 260
		// (Invoke) Token: 0x06000665 RID: 1637
		public delegate object ThreadWorkerMethodWithReturnAndParam(object param);

		// Token: 0x02000105 RID: 261
		// (Invoke) Token: 0x06000669 RID: 1641
		public delegate void ThreadWorkerMethod();

		// Token: 0x02000106 RID: 262
		// (Invoke) Token: 0x0600066D RID: 1645
		public delegate void ThreadWorkerMethodWithParam(object param);
	}
}
