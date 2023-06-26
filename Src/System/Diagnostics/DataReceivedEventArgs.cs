using System;

namespace System.Diagnostics
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.Process.OutputDataReceived" /> and <see cref="E:System.Diagnostics.Process.ErrorDataReceived" /> events.</summary>
	// Token: 0x020004C4 RID: 1220
	public class DataReceivedEventArgs : EventArgs
	{
		// Token: 0x06002D8C RID: 11660 RVA: 0x000CCD7D File Offset: 0x000CAF7D
		internal DataReceivedEventArgs(string data)
		{
			this._data = data;
		}

		/// <summary>Gets the line of characters that was written to a redirected <see cref="T:System.Diagnostics.Process" /> output stream.</summary>
		/// <returns>The line that was written by an associated <see cref="T:System.Diagnostics.Process" /> to its redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> or <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</returns>
		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06002D8D RID: 11661 RVA: 0x000CCD8C File Offset: 0x000CAF8C
		public string Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x0400271B RID: 10011
		internal string _data;
	}
}
