using System;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single successful subexpression capture.</summary>
	// Token: 0x0200068B RID: 1675
	[global::__DynamicallyInvokable]
	[Serializable]
	public class Capture
	{
		// Token: 0x06003DD9 RID: 15833 RVA: 0x000FD3E8 File Offset: 0x000FB5E8
		internal Capture(string text, int i, int l)
		{
			this._text = text;
			this._index = i;
			this._length = l;
		}

		/// <summary>The position in the original string where the first character of the captured substring is found.</summary>
		/// <returns>The zero-based starting position in the original string where the captured substring is found.</returns>
		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06003DDA RID: 15834 RVA: 0x000FD405 File Offset: 0x000FB605
		[global::__DynamicallyInvokable]
		public int Index
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._index;
			}
		}

		/// <summary>Gets the length of the captured substring.</summary>
		/// <returns>The length of the captured substring.</returns>
		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06003DDB RID: 15835 RVA: 0x000FD40D File Offset: 0x000FB60D
		[global::__DynamicallyInvokable]
		public int Length
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._length;
			}
		}

		/// <summary>Gets the captured substring from the input string.</summary>
		/// <returns>The substring that is captured by the match.</returns>
		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06003DDC RID: 15836 RVA: 0x000FD415 File Offset: 0x000FB615
		[global::__DynamicallyInvokable]
		public string Value
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._text.Substring(this._index, this._length);
			}
		}

		/// <summary>Retrieves the captured substring from the input string by calling the <see cref="P:System.Text.RegularExpressions.Capture.Value" /> property.</summary>
		/// <returns>The substring that was captured by the match.</returns>
		// Token: 0x06003DDD RID: 15837 RVA: 0x000FD42E File Offset: 0x000FB62E
		[global::__DynamicallyInvokable]
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x000FD436 File Offset: 0x000FB636
		internal string GetOriginalString()
		{
			return this._text;
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x000FD43E File Offset: 0x000FB63E
		internal string GetLeftSubstring()
		{
			return this._text.Substring(0, this._index);
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x000FD452 File Offset: 0x000FB652
		internal string GetRightSubstring()
		{
			return this._text.Substring(this._index + this._length, this._text.Length - this._index - this._length);
		}

		// Token: 0x04002CE4 RID: 11492
		internal string _text;

		// Token: 0x04002CE5 RID: 11493
		internal int _index;

		// Token: 0x04002CE6 RID: 11494
		internal int _length;
	}
}
