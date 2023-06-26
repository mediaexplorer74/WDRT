using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.MaskedTextBox.TypeValidationCompleted" /> event.</summary>
	// Token: 0x02000423 RID: 1059
	public class TypeValidationEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TypeValidationEventArgs" /> class.</summary>
		/// <param name="validatingType">The <see cref="T:System.Type" /> that the formatted input string was being validated against.</param>
		/// <param name="isValidInput">A <see cref="T:System.Boolean" /> value indicating whether the formatted string was successfully converted to the validating type.</param>
		/// <param name="returnValue">An <see cref="T:System.Object" /> that is the result of the formatted string being converted to the target type.</param>
		/// <param name="message">A <see cref="T:System.String" /> containing a description of the conversion process.</param>
		// Token: 0x060049D6 RID: 18902 RVA: 0x001371AB File Offset: 0x001353AB
		public TypeValidationEventArgs(Type validatingType, bool isValidInput, object returnValue, string message)
		{
			this.validatingType = validatingType;
			this.isValidInput = isValidInput;
			this.returnValue = returnValue;
			this.message = message;
		}

		/// <summary>Gets or sets a value indicating whether the event should be canceled.</summary>
		/// <returns>
		///   <see langword="true" /> if the event should be canceled and focus retained by the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control; otherwise, <see langword="false" /> to continue validation processing.</returns>
		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x060049D7 RID: 18903 RVA: 0x001371D0 File Offset: 0x001353D0
		// (set) Token: 0x060049D8 RID: 18904 RVA: 0x001371D8 File Offset: 0x001353D8
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
			}
		}

		/// <summary>Gets a value indicating whether the formatted input string was successfully converted to the validating type.</summary>
		/// <returns>
		///   <see langword="true" /> if the formatted input string can be converted into the type specified by the <see cref="P:System.Windows.Forms.TypeValidationEventArgs.ValidatingType" /> property; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x060049D9 RID: 18905 RVA: 0x001371E1 File Offset: 0x001353E1
		public bool IsValidInput
		{
			get
			{
				return this.isValidInput;
			}
		}

		/// <summary>Gets a text message describing the conversion process.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a description of the conversion process.</returns>
		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x060049DA RID: 18906 RVA: 0x001371E9 File Offset: 0x001353E9
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		/// <summary>Gets the object that results from the conversion of the formatted input string.</summary>
		/// <returns>If the validation is successful, an <see cref="T:System.Object" /> that represents the converted type; otherwise, <see langword="null" />.</returns>
		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x060049DB RID: 18907 RVA: 0x001371F1 File Offset: 0x001353F1
		public object ReturnValue
		{
			get
			{
				return this.returnValue;
			}
		}

		/// <summary>Gets the type that the formatted input string is being validated against.</summary>
		/// <returns>The target <see cref="T:System.Type" /> of the conversion process. This should never be <see langword="null" />.</returns>
		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x060049DC RID: 18908 RVA: 0x001371F9 File Offset: 0x001353F9
		public Type ValidatingType
		{
			get
			{
				return this.validatingType;
			}
		}

		// Token: 0x040027AB RID: 10155
		private Type validatingType;

		// Token: 0x040027AC RID: 10156
		private string message;

		// Token: 0x040027AD RID: 10157
		private bool isValidInput;

		// Token: 0x040027AE RID: 10158
		private object returnValue;

		// Token: 0x040027AF RID: 10159
		private bool cancel;
	}
}
