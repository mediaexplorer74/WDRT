using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for events that need a <see langword="true" /> or <see langword="false" /> answer to a question.</summary>
	// Token: 0x0200033A RID: 826
	public class QuestionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QuestionEventArgs" /> class using a default <see cref="P:System.Windows.Forms.QuestionEventArgs.Response" /> property value of <see langword="false" />.</summary>
		// Token: 0x0600357B RID: 13691 RVA: 0x000F267D File Offset: 0x000F087D
		public QuestionEventArgs()
		{
			this.response = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QuestionEventArgs" /> class using the specified default value for the <see cref="P:System.Windows.Forms.QuestionEventArgs.Response" /> property.</summary>
		/// <param name="response">The default value of the <see cref="P:System.Windows.Forms.QuestionEventArgs.Response" /> property.</param>
		// Token: 0x0600357C RID: 13692 RVA: 0x000F268C File Offset: 0x000F088C
		public QuestionEventArgs(bool response)
		{
			this.response = response;
		}

		/// <summary>Gets or sets a value indicating the response to a question represented by the event.</summary>
		/// <returns>
		///   <see langword="true" /> for an affirmative response; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x0600357D RID: 13693 RVA: 0x000F269B File Offset: 0x000F089B
		// (set) Token: 0x0600357E RID: 13694 RVA: 0x000F26A3 File Offset: 0x000F08A3
		public bool Response
		{
			get
			{
				return this.response;
			}
			set
			{
				this.response = value;
			}
		}

		// Token: 0x04001F4E RID: 8014
		private bool response;
	}
}
