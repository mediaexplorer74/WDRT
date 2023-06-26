using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the MethodName<see langword="Completed" /> event.</summary>
	// Token: 0x020005A7 RID: 1447
	[global::__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class RunWorkerCompletedEventArgs : AsyncCompletedEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RunWorkerCompletedEventArgs" /> class.</summary>
		/// <param name="result">The result of an asynchronous operation.</param>
		/// <param name="error">Any error that occurred during the asynchronous operation.</param>
		/// <param name="cancelled">A value indicating whether the asynchronous operation was canceled.</param>
		// Token: 0x060035FD RID: 13821 RVA: 0x000EBCAC File Offset: 0x000E9EAC
		[global::__DynamicallyInvokable]
		public RunWorkerCompletedEventArgs(object result, Exception error, bool cancelled)
			: base(error, cancelled, null)
		{
			this.result = result;
		}

		/// <summary>Gets a value that represents the result of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the result of an asynchronous operation.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">
		///   <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" /> is not <see langword="null" />. The <see cref="P:System.Exception.InnerException" /> property holds a reference to <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Cancelled" /> is <see langword="true" />.</exception>
		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x000EBCBE File Offset: 0x000E9EBE
		[global::__DynamicallyInvokable]
		public object Result
		{
			[global::__DynamicallyInvokable]
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.result;
			}
		}

		/// <summary>Gets a value that represents the user state.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the user state.</returns>
		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x000EBCCC File Offset: 0x000E9ECC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[global::__DynamicallyInvokable]
		public new object UserState
		{
			[global::__DynamicallyInvokable]
			get
			{
				return base.UserState;
			}
		}

		// Token: 0x04002A7E RID: 10878
		private object result;
	}
}
