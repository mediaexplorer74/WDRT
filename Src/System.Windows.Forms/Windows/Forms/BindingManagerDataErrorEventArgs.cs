using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event.</summary>
	// Token: 0x02000135 RID: 309
	public class BindingManagerDataErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingManagerDataErrorEventArgs" /> class.</summary>
		/// <param name="exception">The <see cref="T:System.Exception" /> that occurred in the binding process that caused the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event to be raised.</param>
		// Token: 0x06000B4D RID: 2893 RVA: 0x000201B9 File Offset: 0x0001E3B9
		public BindingManagerDataErrorEventArgs(Exception exception)
		{
			this.exception = exception;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> caught in the binding process that caused the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event to be raised.</summary>
		/// <returns>The <see cref="T:System.Exception" /> that caused the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event to be raised.</returns>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x000201C8 File Offset: 0x0001E3C8
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040006BB RID: 1723
		private Exception exception;
	}
}
