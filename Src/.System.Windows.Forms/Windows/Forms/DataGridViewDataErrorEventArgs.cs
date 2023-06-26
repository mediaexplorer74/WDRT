using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event.</summary>
	// Token: 0x020001CD RID: 461
	public class DataGridViewDataErrorEventArgs : DataGridViewCellCancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs" /> class.</summary>
		/// <param name="exception">The exception that occurred.</param>
		/// <param name="columnIndex">The column index of the cell that raised the <see cref="E:System.Windows.Forms.DataGridView.DataError" />.</param>
		/// <param name="rowIndex">The row index of the cell that raised the <see cref="E:System.Windows.Forms.DataGridView.DataError" />.</param>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values indicating the context in which the error occurred.</param>
		// Token: 0x06002055 RID: 8277 RVA: 0x0009B6DE File Offset: 0x000998DE
		public DataGridViewDataErrorEventArgs(Exception exception, int columnIndex, int rowIndex, DataGridViewDataErrorContexts context)
			: base(columnIndex, rowIndex)
		{
			this.exception = exception;
			this.context = context;
		}

		/// <summary>Gets details about the state of the <see cref="T:System.Windows.Forms.DataGridView" /> when the error occurred.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the context in which the error occurred.</returns>
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x0009B6F7 File Offset: 0x000998F7
		public DataGridViewDataErrorContexts Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets the exception that represents the error.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the error.</returns>
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x0009B6FF File Offset: 0x000998FF
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		/// <summary>Gets or sets a value indicating whether to throw the exception after the <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventHandler" /> delegate is finished with it.</summary>
		/// <returns>
		///   <see langword="true" /> if the exception should be thrown; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">When setting this property to <see langword="true" />, the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.Exception" /> property value is <see langword="null" />.</exception>
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x0009B707 File Offset: 0x00099907
		// (set) Token: 0x06002059 RID: 8281 RVA: 0x0009B70F File Offset: 0x0009990F
		public bool ThrowException
		{
			get
			{
				return this.throwException;
			}
			set
			{
				if (value && this.exception == null)
				{
					throw new ArgumentException(SR.GetString("DataGridView_CannotThrowNullException"));
				}
				this.throwException = value;
			}
		}

		// Token: 0x04000DA3 RID: 3491
		private Exception exception;

		// Token: 0x04000DA4 RID: 3492
		private bool throwException;

		// Token: 0x04000DA5 RID: 3493
		private DataGridViewDataErrorContexts context;
	}
}
