using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Supports iterating over a <see cref="T:System.String" /> object and reading its individual characters. This class cannot be inherited.</summary>
	// Token: 0x020000B7 RID: 183
	[ComVisible(true)]
	[Serializable]
	public sealed class CharEnumerator : IEnumerator, ICloneable, IEnumerator<char>, IDisposable
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002253F File Offset: 0x0002073F
		internal CharEnumerator(string str)
		{
			this.str = str;
			this.index = -1;
		}

		/// <summary>Creates a copy of the current <see cref="T:System.CharEnumerator" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a copy of the current <see cref="T:System.CharEnumerator" /> object.</returns>
		// Token: 0x06000AD9 RID: 2777 RVA: 0x00022555 File Offset: 0x00020755
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Increments the internal index of the current <see cref="T:System.CharEnumerator" /> object to the next character of the enumerated string.</summary>
		/// <returns>
		///   <see langword="true" /> if the index is successfully incremented and within the enumerated string; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000ADA RID: 2778 RVA: 0x00022560 File Offset: 0x00020760
		public bool MoveNext()
		{
			if (this.index < this.str.Length - 1)
			{
				this.index++;
				this.currentElement = this.str[this.index];
				return true;
			}
			this.index = this.str.Length;
			return false;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.CharEnumerator" /> class.</summary>
		// Token: 0x06000ADB RID: 2779 RVA: 0x000225BB File Offset: 0x000207BB
		public void Dispose()
		{
			if (this.str != null)
			{
				this.index = this.str.Length;
			}
			this.str = null;
		}

		/// <summary>Gets the currently referenced character in the string enumerated by this <see cref="T:System.CharEnumerator" /> object. For a description of this member, see <see cref="P:System.Collections.IEnumerator.Current" />.</summary>
		/// <returns>The boxed Unicode character currently referenced by this <see cref="T:System.CharEnumerator" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">Enumeration has not started.  
		///  -or-  
		///  Enumeration has ended.</exception>
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x000225E0 File Offset: 0x000207E0
		object IEnumerator.Current
		{
			get
			{
				if (this.index == -1)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
				}
				if (this.index >= this.str.Length)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
				}
				return this.currentElement;
			}
		}

		/// <summary>Gets the currently referenced character in the string enumerated by this <see cref="T:System.CharEnumerator" /> object.</summary>
		/// <returns>The Unicode character currently referenced by this <see cref="T:System.CharEnumerator" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The index is invalid; that is, it is before the first or after the last character of the enumerated string.</exception>
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00022634 File Offset: 0x00020834
		public char Current
		{
			get
			{
				if (this.index == -1)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
				}
				if (this.index >= this.str.Length)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
				}
				return this.currentElement;
			}
		}

		/// <summary>Initializes the index to a position logically before the first character of the enumerated string.</summary>
		// Token: 0x06000ADE RID: 2782 RVA: 0x00022683 File Offset: 0x00020883
		public void Reset()
		{
			this.currentElement = '\0';
			this.index = -1;
		}

		// Token: 0x04000400 RID: 1024
		private string str;

		// Token: 0x04000401 RID: 1025
		private int index;

		// Token: 0x04000402 RID: 1026
		private char currentElement;
	}
}
