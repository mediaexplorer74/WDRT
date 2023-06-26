using System;
using System.Text;

namespace System.Collections.Generic
{
	/// <summary>Defines a key/value pair that can be set or retrieved.</summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	// Token: 0x020004D9 RID: 1241
	[__DynamicallyInvokable]
	[Serializable]
	public struct KeyValuePair<TKey, TValue>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyValuePair`2" /> structure with the specified key and value.</summary>
		/// <param name="key">The object defined in each key/value pair.</param>
		/// <param name="value">The definition associated with <paramref name="key" />.</param>
		// Token: 0x06003AFF RID: 15103 RVA: 0x000E1238 File Offset: 0x000DF438
		[__DynamicallyInvokable]
		public KeyValuePair(TKey key, TValue value)
		{
			this.key = key;
			this.value = value;
		}

		/// <summary>Gets the key in the key/value pair.</summary>
		/// <returns>A <typeparamref name="TKey" /> that is the key of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />.</returns>
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x000E1248 File Offset: 0x000DF448
		[__DynamicallyInvokable]
		public TKey Key
		{
			[__DynamicallyInvokable]
			get
			{
				return this.key;
			}
		}

		/// <summary>Gets the value in the key/value pair.</summary>
		/// <returns>A <typeparamref name="TValue" /> that is the value of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />.</returns>
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06003B01 RID: 15105 RVA: 0x000E1250 File Offset: 0x000DF450
		[__DynamicallyInvokable]
		public TValue Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.value;
			}
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />, using the string representations of the key and value.</summary>
		/// <returns>A string representation of the <see cref="T:System.Collections.Generic.KeyValuePair`2" />, which includes the string representations of the key and value.</returns>
		// Token: 0x06003B02 RID: 15106 RVA: 0x000E1258 File Offset: 0x000DF458
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			stringBuilder.Append('[');
			if (this.Key != null)
			{
				StringBuilder stringBuilder2 = stringBuilder;
				TKey tkey = this.Key;
				stringBuilder2.Append(tkey.ToString());
			}
			stringBuilder.Append(", ");
			if (this.Value != null)
			{
				StringBuilder stringBuilder3 = stringBuilder;
				TValue tvalue = this.Value;
				stringBuilder3.Append(tvalue.ToString());
			}
			stringBuilder.Append(']');
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x04001960 RID: 6496
		private TKey key;

		// Token: 0x04001961 RID: 6497
		private TValue value;
	}
}
