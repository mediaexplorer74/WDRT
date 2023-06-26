using System;

namespace System
{
	/// <summary>Provides data for the <see cref="E:System.Console.CancelKeyPress" /> event. This class cannot be inherited.</summary>
	// Token: 0x020000C4 RID: 196
	[Serializable]
	public sealed class ConsoleCancelEventArgs : EventArgs
	{
		// Token: 0x06000B97 RID: 2967 RVA: 0x00024F95 File Offset: 0x00023195
		internal ConsoleCancelEventArgs(ConsoleSpecialKey type)
		{
			this._type = type;
			this._cancel = false;
		}

		/// <summary>Gets or sets a value that indicates whether simultaneously pressing the <see cref="F:System.ConsoleModifiers.Control" /> modifier key and the <see cref="F:System.ConsoleKey.C" /> console key (Ctrl+C) or the Ctrl+Break keys terminates the current process. The default is <see langword="false" />, which terminates the current process.</summary>
		/// <returns>
		///   <see langword="true" /> if the current process should resume when the event handler concludes; <see langword="false" /> if the current process should terminate. The default value is <see langword="false" />; the current process terminates when the event handler returns. If <see langword="true" />, the current process continues.</returns>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00024FAB File Offset: 0x000231AB
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x00024FB3 File Offset: 0x000231B3
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = value;
			}
		}

		/// <summary>Gets the combination of modifier and console keys that interrupted the current process.</summary>
		/// <returns>One of the enumeration values that specifies the key combination that interrupted the current process. There is no default value.</returns>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00024FBC File Offset: 0x000231BC
		public ConsoleSpecialKey SpecialKey
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000483 RID: 1155
		private ConsoleSpecialKey _type;

		// Token: 0x04000484 RID: 1156
		private bool _cancel;
	}
}
