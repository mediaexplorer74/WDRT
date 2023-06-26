using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
	/// <summary>Enumerates the text elements of a string.</summary>
	// Token: 0x020003D1 RID: 977
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class TextElementEnumerator : IEnumerator
	{
		// Token: 0x060031AF RID: 12719 RVA: 0x000BFA78 File Offset: 0x000BDC78
		internal TextElementEnumerator(string str, int startIndex, int strLen)
		{
			this.str = str;
			this.startIndex = startIndex;
			this.strLen = strLen;
			this.Reset();
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000BFA9B File Offset: 0x000BDC9B
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.charLen = -1;
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000BFAA4 File Offset: 0x000BDCA4
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.strLen = this.endIndex + 1;
			this.currTextElementLen = this.nextTextElementLen;
			if (this.charLen == -1)
			{
				this.uc = CharUnicodeInfo.InternalGetUnicodeCategory(this.str, this.index, out this.charLen);
			}
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000BFAF1 File Offset: 0x000BDCF1
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.endIndex = this.strLen - 1;
			this.nextTextElementLen = this.currTextElementLen;
		}

		/// <summary>Advances the enumerator to the next text element of the string.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next text element; <see langword="false" /> if the enumerator has passed the end of the string.</returns>
		// Token: 0x060031B3 RID: 12723 RVA: 0x000BFB10 File Offset: 0x000BDD10
		[__DynamicallyInvokable]
		public bool MoveNext()
		{
			if (this.index >= this.strLen)
			{
				this.index = this.strLen + 1;
				return false;
			}
			this.currTextElementLen = StringInfo.GetCurrentTextElementLen(this.str, this.index, this.strLen, ref this.uc, ref this.charLen);
			this.index += this.currTextElementLen;
			return true;
		}

		/// <summary>Gets the current text element in the string.</summary>
		/// <returns>An object containing the current text element in the string.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first text element of the string or after the last text element.</exception>
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060031B4 RID: 12724 RVA: 0x000BFB78 File Offset: 0x000BDD78
		[__DynamicallyInvokable]
		public object Current
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetTextElement();
			}
		}

		/// <summary>Gets the current text element in the string.</summary>
		/// <returns>A new string containing the current text element in the string being read.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first text element of the string or after the last text element.</exception>
		// Token: 0x060031B5 RID: 12725 RVA: 0x000BFB80 File Offset: 0x000BDD80
		[__DynamicallyInvokable]
		public string GetTextElement()
		{
			if (this.index == this.startIndex)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
			}
			if (this.index > this.strLen)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
			}
			return this.str.Substring(this.index - this.currTextElementLen, this.currTextElementLen);
		}

		/// <summary>Gets the index of the text element that the enumerator is currently positioned over.</summary>
		/// <returns>The index of the text element that the enumerator is currently positioned over.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first text element of the string or after the last text element.</exception>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060031B6 RID: 12726 RVA: 0x000BFBE7 File Offset: 0x000BDDE7
		[__DynamicallyInvokable]
		public int ElementIndex
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.index == this.startIndex)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
				}
				return this.index - this.currTextElementLen;
			}
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first text element in the string.</summary>
		// Token: 0x060031B7 RID: 12727 RVA: 0x000BFC14 File Offset: 0x000BDE14
		[__DynamicallyInvokable]
		public void Reset()
		{
			this.index = this.startIndex;
			if (this.index < this.strLen)
			{
				this.uc = CharUnicodeInfo.InternalGetUnicodeCategory(this.str, this.index, out this.charLen);
			}
		}

		// Token: 0x0400152D RID: 5421
		private string str;

		// Token: 0x0400152E RID: 5422
		private int index;

		// Token: 0x0400152F RID: 5423
		private int startIndex;

		// Token: 0x04001530 RID: 5424
		[NonSerialized]
		private int strLen;

		// Token: 0x04001531 RID: 5425
		[NonSerialized]
		private int currTextElementLen;

		// Token: 0x04001532 RID: 5426
		[OptionalField(VersionAdded = 2)]
		private UnicodeCategory uc;

		// Token: 0x04001533 RID: 5427
		[OptionalField(VersionAdded = 2)]
		private int charLen;

		// Token: 0x04001534 RID: 5428
		private int endIndex;

		// Token: 0x04001535 RID: 5429
		private int nextTextElementLen;
	}
}
