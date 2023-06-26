using System;
using System.ComponentModel;

namespace System.Drawing.Printing
{
	/// <summary>Provides data for the <see cref="E:System.Drawing.Printing.PrintDocument.BeginPrint" /> and <see cref="E:System.Drawing.Printing.PrintDocument.EndPrint" /> events.</summary>
	// Token: 0x02000067 RID: 103
	public class PrintEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintEventArgs" /> class.</summary>
		// Token: 0x060007F0 RID: 2032 RVA: 0x000207E0 File Offset: 0x0001E9E0
		public PrintEventArgs()
		{
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000207E8 File Offset: 0x0001E9E8
		internal PrintEventArgs(PrintAction action)
		{
			this.printAction = action;
		}

		/// <summary>Returns <see cref="F:System.Drawing.Printing.PrintAction.PrintToFile" /> in all cases.</summary>
		/// <returns>
		///   <see cref="F:System.Drawing.Printing.PrintAction.PrintToFile" /> in all cases.</returns>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x000207F7 File Offset: 0x0001E9F7
		public PrintAction PrintAction
		{
			get
			{
				return this.printAction;
			}
		}

		// Token: 0x040006EA RID: 1770
		private PrintAction printAction;
	}
}
