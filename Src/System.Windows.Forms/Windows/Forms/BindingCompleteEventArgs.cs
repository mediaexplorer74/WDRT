using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event.</summary>
	// Token: 0x02000130 RID: 304
	public class BindingCompleteEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" /> class with the specified binding, error state and text, binding context, exception, and whether the binding should be cancelled.</summary>
		/// <param name="binding">The binding associated with this occurrence of a <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.BindingCompleteState" /> values.</param>
		/// <param name="context">One of the <see cref="T:System.Windows.Forms.BindingCompleteContext" /> values.</param>
		/// <param name="errorText">The error text or exception message for errors that occurred during the binding.</param>
		/// <param name="exception">The <see cref="T:System.Exception" /> that occurred during the binding.</param>
		/// <param name="cancel">
		///   <see langword="true" /> to cancel the binding and keep focus on the current control; <see langword="false" /> to allow focus to shift to another control.</param>
		// Token: 0x06000AF9 RID: 2809 RVA: 0x0001F625 File Offset: 0x0001D825
		public BindingCompleteEventArgs(Binding binding, BindingCompleteState state, BindingCompleteContext context, string errorText, Exception exception, bool cancel)
			: base(cancel)
		{
			this.binding = binding;
			this.state = state;
			this.context = context;
			this.errorText = ((errorText == null) ? string.Empty : errorText);
			this.exception = exception;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" /> class with the specified binding, error state and text, binding context, and exception.</summary>
		/// <param name="binding">The binding associated with this occurrence of a <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.BindingCompleteState" /> values.</param>
		/// <param name="context">One of the <see cref="T:System.Windows.Forms.BindingCompleteContext" /> values.</param>
		/// <param name="errorText">The error text or exception message for errors that occurred during the binding.</param>
		/// <param name="exception">The <see cref="T:System.Exception" /> that occurred during the binding.</param>
		// Token: 0x06000AFA RID: 2810 RVA: 0x0001F65F File Offset: 0x0001D85F
		public BindingCompleteEventArgs(Binding binding, BindingCompleteState state, BindingCompleteContext context, string errorText, Exception exception)
			: this(binding, state, context, errorText, exception, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" /> class with the specified binding, error state and text, and binding context.</summary>
		/// <param name="binding">The binding associated with this occurrence of a <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.BindingCompleteState" /> values.</param>
		/// <param name="context">One of the <see cref="T:System.Windows.Forms.BindingCompleteContext" /> values.</param>
		/// <param name="errorText">The error text or exception message for errors that occurred during the binding.</param>
		// Token: 0x06000AFB RID: 2811 RVA: 0x0001F66F File Offset: 0x0001D86F
		public BindingCompleteEventArgs(Binding binding, BindingCompleteState state, BindingCompleteContext context, string errorText)
			: this(binding, state, context, errorText, null, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" /> class with the specified binding, error state, and binding context.</summary>
		/// <param name="binding">The binding associated with this occurrence of a <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.BindingCompleteState" /> values.</param>
		/// <param name="context">One of the <see cref="T:System.Windows.Forms.BindingCompleteContext" /> values.</param>
		// Token: 0x06000AFC RID: 2812 RVA: 0x0001F67E File Offset: 0x0001D87E
		public BindingCompleteEventArgs(Binding binding, BindingCompleteState state, BindingCompleteContext context)
			: this(binding, state, context, string.Empty, null, false)
		{
		}

		/// <summary>Gets the binding associated with this occurrence of a <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Binding" /> associated with this <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" />.</returns>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0001F690 File Offset: 0x0001D890
		public Binding Binding
		{
			get
			{
				return this.binding;
			}
		}

		/// <summary>Gets the completion state of the binding operation.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BindingCompleteState" /> values.</returns>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0001F698 File Offset: 0x0001D898
		public BindingCompleteState BindingCompleteState
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Gets the direction of the binding operation.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BindingCompleteContext" /> values.</returns>
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0001F6A0 File Offset: 0x0001D8A0
		public BindingCompleteContext BindingCompleteContext
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets the text description of the error that occurred during the binding operation.</summary>
		/// <returns>The text description of the error that occurred during the binding operation.</returns>
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0001F6A8 File Offset: 0x0001D8A8
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		/// <summary>Gets the exception that occurred during the binding operation.</summary>
		/// <returns>The <see cref="T:System.Exception" /> that occurred during the binding operation.</returns>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0001F6B0 File Offset: 0x0001D8B0
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040006AA RID: 1706
		private Binding binding;

		// Token: 0x040006AB RID: 1707
		private BindingCompleteState state;

		// Token: 0x040006AC RID: 1708
		private BindingCompleteContext context;

		// Token: 0x040006AD RID: 1709
		private string errorText;

		// Token: 0x040006AE RID: 1710
		private Exception exception;
	}
}
