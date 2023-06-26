using System;

namespace System.Windows.Forms.Layout
{
	// Token: 0x020004CD RID: 1229
	internal sealed class LayoutTransaction : IDisposable
	{
		// Token: 0x060050CD RID: 20685 RVA: 0x00150197 File Offset: 0x0014E397
		public LayoutTransaction(Control controlToLayout, IArrangedElement controlCausingLayout, string property)
			: this(controlToLayout, controlCausingLayout, property, true)
		{
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x001501A4 File Offset: 0x0014E3A4
		public LayoutTransaction(Control controlToLayout, IArrangedElement controlCausingLayout, string property, bool resumeLayout)
		{
			CommonProperties.xClearPreferredSizeCache(controlCausingLayout);
			this._controlToLayout = controlToLayout;
			this._resumeLayout = resumeLayout;
			if (this._controlToLayout != null)
			{
				this._controlToLayout.SuspendLayout();
				CommonProperties.xClearPreferredSizeCache(this._controlToLayout);
				if (resumeLayout)
				{
					this._controlToLayout.PerformLayout(new LayoutEventArgs(controlCausingLayout, property));
				}
			}
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x00150200 File Offset: 0x0014E400
		public void Dispose()
		{
			if (this._controlToLayout != null)
			{
				this._controlToLayout.ResumeLayout(this._resumeLayout);
			}
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x0015021C File Offset: 0x0014E41C
		public static IDisposable CreateTransactionIf(bool condition, Control controlToLayout, IArrangedElement elementCausingLayout, string property)
		{
			if (condition)
			{
				return new LayoutTransaction(controlToLayout, elementCausingLayout, property);
			}
			CommonProperties.xClearPreferredSizeCache(elementCausingLayout);
			return default(NullLayoutTransaction);
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x00150249 File Offset: 0x0014E449
		public static void DoLayout(IArrangedElement elementToLayout, IArrangedElement elementCausingLayout, string property)
		{
			if (elementCausingLayout != null)
			{
				CommonProperties.xClearPreferredSizeCache(elementCausingLayout);
				if (elementToLayout != null)
				{
					CommonProperties.xClearPreferredSizeCache(elementToLayout);
					elementToLayout.PerformLayout(elementCausingLayout, property);
				}
			}
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x00150265 File Offset: 0x0014E465
		public static void DoLayoutIf(bool condition, IArrangedElement elementToLayout, IArrangedElement elementCausingLayout, string property)
		{
			if (!condition)
			{
				if (elementCausingLayout != null)
				{
					CommonProperties.xClearPreferredSizeCache(elementCausingLayout);
					return;
				}
			}
			else
			{
				LayoutTransaction.DoLayout(elementToLayout, elementCausingLayout, property);
			}
		}

		// Token: 0x0400349B RID: 13467
		private Control _controlToLayout;

		// Token: 0x0400349C RID: 13468
		private bool _resumeLayout;
	}
}
