using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Provides a formatter-friendly mechanism for parsing the data in <see cref="T:System.Runtime.Serialization.SerializationInfo" />. This class cannot be inherited.</summary>
	// Token: 0x02000741 RID: 1857
	[ComVisible(true)]
	public sealed class SerializationInfoEnumerator : IEnumerator
	{
		// Token: 0x0600523E RID: 21054 RVA: 0x0012223D File Offset: 0x0012043D
		internal SerializationInfoEnumerator(string[] members, object[] info, Type[] types, int numItems)
		{
			this.m_members = members;
			this.m_data = info;
			this.m_types = types;
			this.m_numItems = numItems - 1;
			this.m_currItem = -1;
			this.m_current = false;
		}

		/// <summary>Updates the enumerator to the next item.</summary>
		/// <returns>
		///   <see langword="true" /> if a new element is found; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600523F RID: 21055 RVA: 0x00122272 File Offset: 0x00120472
		public bool MoveNext()
		{
			if (this.m_currItem < this.m_numItems)
			{
				this.m_currItem++;
				this.m_current = true;
			}
			else
			{
				this.m_current = false;
			}
			return this.m_current;
		}

		/// <summary>Gets the current item in the collection.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.SerializationEntry" /> that contains the current serialization data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumeration has not started or has already ended.</exception>
		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06005240 RID: 21056 RVA: 0x001222A8 File Offset: 0x001204A8
		object IEnumerator.Current
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
				return new SerializationEntry(this.m_members[this.m_currItem], this.m_data[this.m_currItem], this.m_types[this.m_currItem]);
			}
		}

		/// <summary>Gets the item currently being examined.</summary>
		/// <returns>The item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration.</exception>
		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06005241 RID: 21057 RVA: 0x00122300 File Offset: 0x00120500
		public SerializationEntry Current
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
				return new SerializationEntry(this.m_members[this.m_currItem], this.m_data[this.m_currItem], this.m_types[this.m_currItem]);
			}
		}

		/// <summary>Resets the enumerator to the first item.</summary>
		// Token: 0x06005242 RID: 21058 RVA: 0x00122351 File Offset: 0x00120551
		public void Reset()
		{
			this.m_currItem = -1;
			this.m_current = false;
		}

		/// <summary>Gets the name for the item currently being examined.</summary>
		/// <returns>The item name.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration.</exception>
		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06005243 RID: 21059 RVA: 0x00122361 File Offset: 0x00120561
		public string Name
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
				return this.m_members[this.m_currItem];
			}
		}

		/// <summary>Gets the value of the item currently being examined.</summary>
		/// <returns>The value of the item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration.</exception>
		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06005244 RID: 21060 RVA: 0x00122388 File Offset: 0x00120588
		public object Value
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
				return this.m_data[this.m_currItem];
			}
		}

		/// <summary>Gets the type of the item currently being examined.</summary>
		/// <returns>The type of the item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration.</exception>
		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06005245 RID: 21061 RVA: 0x001223AF File Offset: 0x001205AF
		public Type ObjectType
		{
			get
			{
				if (!this.m_current)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
				return this.m_types[this.m_currItem];
			}
		}

		// Token: 0x04002463 RID: 9315
		private string[] m_members;

		// Token: 0x04002464 RID: 9316
		private object[] m_data;

		// Token: 0x04002465 RID: 9317
		private Type[] m_types;

		// Token: 0x04002466 RID: 9318
		private int m_numItems;

		// Token: 0x04002467 RID: 9319
		private int m_currItem;

		// Token: 0x04002468 RID: 9320
		private bool m_current;
	}
}
