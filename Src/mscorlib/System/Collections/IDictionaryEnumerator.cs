using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Enumerates the elements of a nongeneric dictionary.</summary>
	// Token: 0x0200049B RID: 1179
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IDictionaryEnumerator : IEnumerator
	{
		/// <summary>Gets the key of the current dictionary entry.</summary>
		/// <returns>The key of the current element of the enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry.</exception>
		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060038CF RID: 14543
		[__DynamicallyInvokable]
		object Key
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the value of the current dictionary entry.</summary>
		/// <returns>The value of the current element of the enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry.</exception>
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060038D0 RID: 14544
		[__DynamicallyInvokable]
		object Value
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets both the key and the value of the current dictionary entry.</summary>
		/// <returns>A <see cref="T:System.Collections.DictionaryEntry" /> containing both the key and the value of the current dictionary entry.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry.</exception>
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060038D1 RID: 14545
		[__DynamicallyInvokable]
		DictionaryEntry Entry
		{
			[__DynamicallyInvokable]
			get;
		}
	}
}
