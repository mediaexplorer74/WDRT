using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Defines a dictionary key/value pair that can be set or retrieved.</summary>
	// Token: 0x02000497 RID: 1175
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct DictionaryEntry
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Collections.DictionaryEntry" /> type with the specified key and value.</summary>
		/// <param name="key">The object defined in each key/value pair.</param>
		/// <param name="value">The definition associated with <paramref name="key" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" /> and the .NET Framework version is 1.0 or 1.1.</exception>
		// Token: 0x060038BA RID: 14522 RVA: 0x000DB3FB File Offset: 0x000D95FB
		[__DynamicallyInvokable]
		public DictionaryEntry(object key, object value)
		{
			this._key = key;
			this._value = value;
		}

		/// <summary>Gets or sets the key in the key/value pair.</summary>
		/// <returns>The key in the key/value pair.</returns>
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060038BB RID: 14523 RVA: 0x000DB40B File Offset: 0x000D960B
		// (set) Token: 0x060038BC RID: 14524 RVA: 0x000DB413 File Offset: 0x000D9613
		[__DynamicallyInvokable]
		public object Key
		{
			[__DynamicallyInvokable]
			get
			{
				return this._key;
			}
			[__DynamicallyInvokable]
			set
			{
				this._key = value;
			}
		}

		/// <summary>Gets or sets the value in the key/value pair.</summary>
		/// <returns>The value in the key/value pair.</returns>
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060038BD RID: 14525 RVA: 0x000DB41C File Offset: 0x000D961C
		// (set) Token: 0x060038BE RID: 14526 RVA: 0x000DB424 File Offset: 0x000D9624
		[__DynamicallyInvokable]
		public object Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._value;
			}
			[__DynamicallyInvokable]
			set
			{
				this._value = value;
			}
		}

		// Token: 0x04001908 RID: 6408
		private object _key;

		// Token: 0x04001909 RID: 6409
		private object _value;
	}
}
