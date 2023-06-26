using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for controls that derive from the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
	// Token: 0x02000428 RID: 1064
	public class UpDownEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.UpDownEventArgs" /> class</summary>
		/// <param name="buttonPushed">The button that was clicked on the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</param>
		// Token: 0x060049EB RID: 18923 RVA: 0x0013724F File Offset: 0x0013544F
		public UpDownEventArgs(int buttonPushed)
		{
			this.buttonID = buttonPushed;
		}

		/// <summary>Gets a value that represents which button the user clicked.</summary>
		/// <returns>A value that represents which button the user clicked.</returns>
		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x060049EC RID: 18924 RVA: 0x0013725E File Offset: 0x0013545E
		public int ButtonID
		{
			get
			{
				return this.buttonID;
			}
		}

		// Token: 0x040027B9 RID: 10169
		private int buttonID;
	}
}
