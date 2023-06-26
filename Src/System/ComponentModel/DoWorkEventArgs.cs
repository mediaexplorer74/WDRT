using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event handler.</summary>
	// Token: 0x02000548 RID: 1352
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DoWorkEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DoWorkEventArgs" /> class.</summary>
		/// <param name="argument">Specifies an argument for an asynchronous operation.</param>
		// Token: 0x060032D1 RID: 13009 RVA: 0x000E241D File Offset: 0x000E061D
		[global::__DynamicallyInvokable]
		public DoWorkEventArgs(object argument)
		{
			this.argument = argument;
		}

		/// <summary>Gets a value that represents the argument of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the argument of an asynchronous operation.</returns>
		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x060032D2 RID: 13010 RVA: 0x000E242C File Offset: 0x000E062C
		[SRDescription("BackgroundWorker_DoWorkEventArgs_Argument")]
		[global::__DynamicallyInvokable]
		public object Argument
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.argument;
			}
		}

		/// <summary>Gets or sets a value that represents the result of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the result of an asynchronous operation.</returns>
		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x060032D3 RID: 13011 RVA: 0x000E2434 File Offset: 0x000E0634
		// (set) Token: 0x060032D4 RID: 13012 RVA: 0x000E243C File Offset: 0x000E063C
		[SRDescription("BackgroundWorker_DoWorkEventArgs_Result")]
		[global::__DynamicallyInvokable]
		public object Result
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.result;
			}
			[global::__DynamicallyInvokable]
			set
			{
				this.result = value;
			}
		}

		// Token: 0x0400298A RID: 10634
		private object result;

		// Token: 0x0400298B RID: 10635
		private object argument;
	}
}
