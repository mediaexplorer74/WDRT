using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Specifies the contextual information about an application thread.</summary>
	// Token: 0x02000121 RID: 289
	public class ApplicationContext : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ApplicationContext" /> class with no context.</summary>
		// Token: 0x0600094D RID: 2381 RVA: 0x0001983B File Offset: 0x00017A3B
		public ApplicationContext()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ApplicationContext" /> class with the specified <see cref="T:System.Windows.Forms.Form" />.</summary>
		/// <param name="mainForm">The main <see cref="T:System.Windows.Forms.Form" /> of the application to use for context.</param>
		// Token: 0x0600094E RID: 2382 RVA: 0x00019844 File Offset: 0x00017A44
		public ApplicationContext(Form mainForm)
		{
			this.MainForm = mainForm;
		}

		/// <summary>Attempts to free resources and perform other cleanup operations before the application context is reclaimed by garbage collection.</summary>
		// Token: 0x0600094F RID: 2383 RVA: 0x00019854 File Offset: 0x00017A54
		~ApplicationContext()
		{
			this.Dispose(false);
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.Form" /> to use as context.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Form" /> to use as context.</returns>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00019884 File Offset: 0x00017A84
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x0001988C File Offset: 0x00017A8C
		public Form MainForm
		{
			get
			{
				return this.mainForm;
			}
			set
			{
				EventHandler eventHandler = new EventHandler(this.OnMainFormDestroy);
				if (this.mainForm != null)
				{
					this.mainForm.HandleDestroyed -= eventHandler;
				}
				this.mainForm = value;
				if (this.mainForm != null)
				{
					this.mainForm.HandleDestroyed += eventHandler;
				}
			}
		}

		/// <summary>Gets or sets an object that contains data about the control.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the control. The default is <see langword="null" />.</returns>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x000198D5 File Offset: 0x00017AD5
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x000198DD File Offset: 0x00017ADD
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		/// <summary>Occurs when the message loop of the thread should be terminated, by calling <see cref="M:System.Windows.Forms.ApplicationContext.ExitThread" />.</summary>
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000954 RID: 2388 RVA: 0x000198E8 File Offset: 0x00017AE8
		// (remove) Token: 0x06000955 RID: 2389 RVA: 0x00019920 File Offset: 0x00017B20
		public event EventHandler ThreadExit;

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.ApplicationContext" />.</summary>
		// Token: 0x06000956 RID: 2390 RVA: 0x00019955 File Offset: 0x00017B55
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ApplicationContext" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000957 RID: 2391 RVA: 0x00019964 File Offset: 0x00017B64
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.mainForm != null)
			{
				if (!this.mainForm.IsDisposed)
				{
					this.mainForm.Dispose();
				}
				this.mainForm = null;
			}
		}

		/// <summary>Terminates the message loop of the thread.</summary>
		// Token: 0x06000958 RID: 2392 RVA: 0x00019990 File Offset: 0x00017B90
		public void ExitThread()
		{
			this.ExitThreadCore();
		}

		/// <summary>Terminates the message loop of the thread.</summary>
		// Token: 0x06000959 RID: 2393 RVA: 0x00019998 File Offset: 0x00017B98
		protected virtual void ExitThreadCore()
		{
			if (this.ThreadExit != null)
			{
				this.ThreadExit(this, EventArgs.Empty);
			}
		}

		/// <summary>Calls <see cref="M:System.Windows.Forms.ApplicationContext.ExitThreadCore" />, which raises the <see cref="E:System.Windows.Forms.ApplicationContext.ThreadExit" /> event.</summary>
		/// <param name="sender">The object that raised the event.</param>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600095A RID: 2394 RVA: 0x00019990 File Offset: 0x00017B90
		protected virtual void OnMainFormClosed(object sender, EventArgs e)
		{
			this.ExitThreadCore();
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x000199B4 File Offset: 0x00017BB4
		private void OnMainFormDestroy(object sender, EventArgs e)
		{
			Form form = (Form)sender;
			if (!form.RecreatingHandle)
			{
				form.HandleDestroyed -= this.OnMainFormDestroy;
				this.OnMainFormClosed(sender, e);
			}
		}

		// Token: 0x040005E2 RID: 1506
		private Form mainForm;

		// Token: 0x040005E3 RID: 1507
		private object userData;
	}
}
