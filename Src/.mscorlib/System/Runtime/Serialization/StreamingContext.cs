using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Describes the source and destination of a given serialized stream, and provides an additional caller-defined context.</summary>
	// Token: 0x02000742 RID: 1858
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct StreamingContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class with a given context state.</summary>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> values that specify the source or destination context for this <see cref="T:System.Runtime.Serialization.StreamingContext" />.</param>
		// Token: 0x06005246 RID: 21062 RVA: 0x001223D6 File Offset: 0x001205D6
		public StreamingContext(StreamingContextStates state)
		{
			this = new StreamingContext(state, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class with a given context state, and some additional information.</summary>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> values that specify the source or destination context for this <see cref="T:System.Runtime.Serialization.StreamingContext" />.</param>
		/// <param name="additional">Any additional information to be associated with the <see cref="T:System.Runtime.Serialization.StreamingContext" />. This information is available to any object that implements <see cref="T:System.Runtime.Serialization.ISerializable" /> or any serialization surrogate. Most users do not need to set this parameter.</param>
		// Token: 0x06005247 RID: 21063 RVA: 0x001223E0 File Offset: 0x001205E0
		public StreamingContext(StreamingContextStates state, object additional)
		{
			this.m_state = state;
			this.m_additionalContext = additional;
		}

		/// <summary>Gets context specified as part of the additional context.</summary>
		/// <returns>The context specified as part of the additional context.</returns>
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06005248 RID: 21064 RVA: 0x001223F0 File Offset: 0x001205F0
		public object Context
		{
			get
			{
				return this.m_additionalContext;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances contain the same values.</summary>
		/// <param name="obj">An object to compare with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is an instance of <see cref="T:System.Runtime.Serialization.StreamingContext" /> and equals the value of the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005249 RID: 21065 RVA: 0x001223F8 File Offset: 0x001205F8
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is StreamingContext && (((StreamingContext)obj).m_additionalContext == this.m_additionalContext && ((StreamingContext)obj).m_state == this.m_state);
		}

		/// <summary>Returns a hash code of this object.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> value that contains the source or destination of the serialization for this <see cref="T:System.Runtime.Serialization.StreamingContext" />.</returns>
		// Token: 0x0600524A RID: 21066 RVA: 0x0012242D File Offset: 0x0012062D
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this.m_state;
		}

		/// <summary>Gets the source or destination of the transmitted data.</summary>
		/// <returns>During serialization, the destination of the transmitted data. During deserialization, the source of the data.</returns>
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x0600524B RID: 21067 RVA: 0x00122435 File Offset: 0x00120635
		public StreamingContextStates State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x04002469 RID: 9321
		internal object m_additionalContext;

		// Token: 0x0400246A RID: 9322
		internal StreamingContextStates m_state;
	}
}
