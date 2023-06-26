using System;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Executes an operation on a separate thread.</summary>
	// Token: 0x02000516 RID: 1302
	[SRDescription("BackgroundWorker_Desc")]
	[DefaultEvent("DoWork")]
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class BackgroundWorker : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BackgroundWorker" /> class.</summary>
		// Token: 0x06003143 RID: 12611 RVA: 0x000DED85 File Offset: 0x000DCF85
		[global::__DynamicallyInvokable]
		public BackgroundWorker()
		{
			this.threadStart = new BackgroundWorker.WorkerThreadStartDelegate(this.WorkerThreadStart);
			this.operationCompleted = new SendOrPostCallback(this.AsyncOperationCompleted);
			this.progressReporter = new SendOrPostCallback(this.ProgressReporter);
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000DEDC3 File Offset: 0x000DCFC3
		private void AsyncOperationCompleted(object arg)
		{
			this.isRunning = false;
			this.cancellationPending = false;
			this.OnRunWorkerCompleted((RunWorkerCompletedEventArgs)arg);
		}

		/// <summary>Gets a value indicating whether the application has requested cancellation of a background operation.</summary>
		/// <returns>
		///   <see langword="true" /> if the application has requested cancellation of a background operation; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x000DEDDF File Offset: 0x000DCFDF
		[Browsable(false)]
		[SRDescription("BackgroundWorker_CancellationPending")]
		[global::__DynamicallyInvokable]
		public bool CancellationPending
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.cancellationPending;
			}
		}

		/// <summary>Requests cancellation of a pending background operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.WorkerSupportsCancellation" /> is <see langword="false" />.</exception>
		// Token: 0x06003146 RID: 12614 RVA: 0x000DEDE7 File Offset: 0x000DCFE7
		[global::__DynamicallyInvokable]
		public void CancelAsync()
		{
			if (!this.WorkerSupportsCancellation)
			{
				throw new InvalidOperationException(SR.GetString("BackgroundWorker_WorkerDoesntSupportCancellation"));
			}
			this.cancellationPending = true;
		}

		/// <summary>Occurs when <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync" /> is called.</summary>
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06003147 RID: 12615 RVA: 0x000DEE08 File Offset: 0x000DD008
		// (remove) Token: 0x06003148 RID: 12616 RVA: 0x000DEE1B File Offset: 0x000DD01B
		[SRCategory("PropertyCategoryAsynchronous")]
		[SRDescription("BackgroundWorker_DoWork")]
		[global::__DynamicallyInvokable]
		public event DoWorkEventHandler DoWork
		{
			[global::__DynamicallyInvokable]
			add
			{
				base.Events.AddHandler(BackgroundWorker.doWorkKey, value);
			}
			[global::__DynamicallyInvokable]
			remove
			{
				base.Events.RemoveHandler(BackgroundWorker.doWorkKey, value);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> is running an asynchronous operation.</summary>
		/// <returns>
		///   <see langword="true" />, if the <see cref="T:System.ComponentModel.BackgroundWorker" /> is running an asynchronous operation; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06003149 RID: 12617 RVA: 0x000DEE2E File Offset: 0x000DD02E
		[Browsable(false)]
		[SRDescription("BackgroundWorker_IsBusy")]
		[global::__DynamicallyInvokable]
		public bool IsBusy
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.isRunning;
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600314A RID: 12618 RVA: 0x000DEE38 File Offset: 0x000DD038
		[global::__DynamicallyInvokable]
		protected virtual void OnDoWork(DoWorkEventArgs e)
		{
			DoWorkEventHandler doWorkEventHandler = (DoWorkEventHandler)base.Events[BackgroundWorker.doWorkKey];
			if (doWorkEventHandler != null)
			{
				doWorkEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.RunWorkerCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600314B RID: 12619 RVA: 0x000DEE68 File Offset: 0x000DD068
		[global::__DynamicallyInvokable]
		protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
		{
			RunWorkerCompletedEventHandler runWorkerCompletedEventHandler = (RunWorkerCompletedEventHandler)base.Events[BackgroundWorker.runWorkerCompletedKey];
			if (runWorkerCompletedEventHandler != null)
			{
				runWorkerCompletedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600314C RID: 12620 RVA: 0x000DEE98 File Offset: 0x000DD098
		[global::__DynamicallyInvokable]
		protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
		{
			ProgressChangedEventHandler progressChangedEventHandler = (ProgressChangedEventHandler)base.Events[BackgroundWorker.progressChangedKey];
			if (progressChangedEventHandler != null)
			{
				progressChangedEventHandler(this, e);
			}
		}

		/// <summary>Occurs when <see cref="M:System.ComponentModel.BackgroundWorker.ReportProgress(System.Int32)" /> is called.</summary>
		// Token: 0x14000046 RID: 70
		// (add) Token: 0x0600314D RID: 12621 RVA: 0x000DEEC6 File Offset: 0x000DD0C6
		// (remove) Token: 0x0600314E RID: 12622 RVA: 0x000DEED9 File Offset: 0x000DD0D9
		[SRCategory("PropertyCategoryAsynchronous")]
		[SRDescription("BackgroundWorker_ProgressChanged")]
		[global::__DynamicallyInvokable]
		public event ProgressChangedEventHandler ProgressChanged
		{
			[global::__DynamicallyInvokable]
			add
			{
				base.Events.AddHandler(BackgroundWorker.progressChangedKey, value);
			}
			[global::__DynamicallyInvokable]
			remove
			{
				base.Events.RemoveHandler(BackgroundWorker.progressChangedKey, value);
			}
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000DEEEC File Offset: 0x000DD0EC
		private void ProgressReporter(object arg)
		{
			this.OnProgressChanged((ProgressChangedEventArgs)arg);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="percentProgress">The percentage, from 0 to 100, of the background operation that is complete.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.BackgroundWorker.WorkerReportsProgress" /> property is set to <see langword="false" />.</exception>
		// Token: 0x06003150 RID: 12624 RVA: 0x000DEEFA File Offset: 0x000DD0FA
		[global::__DynamicallyInvokable]
		public void ReportProgress(int percentProgress)
		{
			this.ReportProgress(percentProgress, null);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="percentProgress">The percentage, from 0 to 100, of the background operation that is complete.</param>
		/// <param name="userState">The state object passed to <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync(System.Object)" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.BackgroundWorker.WorkerReportsProgress" /> property is set to <see langword="false" />.</exception>
		// Token: 0x06003151 RID: 12625 RVA: 0x000DEF04 File Offset: 0x000DD104
		[global::__DynamicallyInvokable]
		public void ReportProgress(int percentProgress, object userState)
		{
			if (!this.WorkerReportsProgress)
			{
				throw new InvalidOperationException(SR.GetString("BackgroundWorker_WorkerDoesntReportProgress"));
			}
			ProgressChangedEventArgs progressChangedEventArgs = new ProgressChangedEventArgs(percentProgress, userState);
			if (this.asyncOperation != null)
			{
				this.asyncOperation.Post(this.progressReporter, progressChangedEventArgs);
				return;
			}
			this.progressReporter(progressChangedEventArgs);
		}

		/// <summary>Starts execution of a background operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is <see langword="true" />.</exception>
		// Token: 0x06003152 RID: 12626 RVA: 0x000DEF58 File Offset: 0x000DD158
		[global::__DynamicallyInvokable]
		public void RunWorkerAsync()
		{
			this.RunWorkerAsync(null);
		}

		/// <summary>Starts execution of a background operation.</summary>
		/// <param name="argument">A parameter for use by the background operation to be executed in the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event handler.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is <see langword="true" />.</exception>
		// Token: 0x06003153 RID: 12627 RVA: 0x000DEF64 File Offset: 0x000DD164
		[global::__DynamicallyInvokable]
		public void RunWorkerAsync(object argument)
		{
			if (this.isRunning)
			{
				throw new InvalidOperationException(SR.GetString("BackgroundWorker_WorkerAlreadyRunning"));
			}
			this.isRunning = true;
			this.cancellationPending = false;
			this.asyncOperation = AsyncOperationManager.CreateOperation(null);
			this.threadStart.BeginInvoke(argument, null, null);
		}

		/// <summary>Occurs when the background operation has completed, has been canceled, or has raised an exception.</summary>
		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06003154 RID: 12628 RVA: 0x000DEFB2 File Offset: 0x000DD1B2
		// (remove) Token: 0x06003155 RID: 12629 RVA: 0x000DEFC5 File Offset: 0x000DD1C5
		[SRCategory("PropertyCategoryAsynchronous")]
		[SRDescription("BackgroundWorker_RunWorkerCompleted")]
		[global::__DynamicallyInvokable]
		public event RunWorkerCompletedEventHandler RunWorkerCompleted
		{
			[global::__DynamicallyInvokable]
			add
			{
				base.Events.AddHandler(BackgroundWorker.runWorkerCompletedKey, value);
			}
			[global::__DynamicallyInvokable]
			remove
			{
				base.Events.RemoveHandler(BackgroundWorker.runWorkerCompletedKey, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> can report progress updates.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports progress updates; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x000DEFD8 File Offset: 0x000DD1D8
		// (set) Token: 0x06003157 RID: 12631 RVA: 0x000DEFE0 File Offset: 0x000DD1E0
		[SRCategory("PropertyCategoryAsynchronous")]
		[SRDescription("BackgroundWorker_WorkerReportsProgress")]
		[DefaultValue(false)]
		[global::__DynamicallyInvokable]
		public bool WorkerReportsProgress
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.workerReportsProgress;
			}
			[global::__DynamicallyInvokable]
			set
			{
				this.workerReportsProgress = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports asynchronous cancellation.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports cancellation; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x000DEFE9 File Offset: 0x000DD1E9
		// (set) Token: 0x06003159 RID: 12633 RVA: 0x000DEFF1 File Offset: 0x000DD1F1
		[SRCategory("PropertyCategoryAsynchronous")]
		[SRDescription("BackgroundWorker_WorkerSupportsCancellation")]
		[DefaultValue(false)]
		[global::__DynamicallyInvokable]
		public bool WorkerSupportsCancellation
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.canCancelWorker;
			}
			[global::__DynamicallyInvokable]
			set
			{
				this.canCancelWorker = value;
			}
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000DEFFC File Offset: 0x000DD1FC
		private void WorkerThreadStart(object argument)
		{
			object obj = null;
			Exception ex = null;
			bool flag = false;
			try
			{
				DoWorkEventArgs doWorkEventArgs = new DoWorkEventArgs(argument);
				this.OnDoWork(doWorkEventArgs);
				if (doWorkEventArgs.Cancel)
				{
					flag = true;
				}
				else
				{
					obj = doWorkEventArgs.Result;
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			RunWorkerCompletedEventArgs runWorkerCompletedEventArgs = new RunWorkerCompletedEventArgs(obj, ex, flag);
			this.asyncOperation.PostOperationCompleted(this.operationCompleted, runWorkerCompletedEventArgs);
		}

		// Token: 0x04002901 RID: 10497
		private static readonly object doWorkKey = new object();

		// Token: 0x04002902 RID: 10498
		private static readonly object runWorkerCompletedKey = new object();

		// Token: 0x04002903 RID: 10499
		private static readonly object progressChangedKey = new object();

		// Token: 0x04002904 RID: 10500
		private bool canCancelWorker;

		// Token: 0x04002905 RID: 10501
		private bool workerReportsProgress;

		// Token: 0x04002906 RID: 10502
		private bool cancellationPending;

		// Token: 0x04002907 RID: 10503
		private bool isRunning;

		// Token: 0x04002908 RID: 10504
		private AsyncOperation asyncOperation;

		// Token: 0x04002909 RID: 10505
		private readonly BackgroundWorker.WorkerThreadStartDelegate threadStart;

		// Token: 0x0400290A RID: 10506
		private readonly SendOrPostCallback operationCompleted;

		// Token: 0x0400290B RID: 10507
		private readonly SendOrPostCallback progressReporter;

		// Token: 0x0200088D RID: 2189
		// (Invoke) Token: 0x06004561 RID: 17761
		private delegate void WorkerThreadStartDelegate(object argument);
	}
}
