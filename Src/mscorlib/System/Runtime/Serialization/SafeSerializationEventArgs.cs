using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.Exception.SerializeObjectState" /> event.</summary>
	// Token: 0x02000751 RID: 1873
	public sealed class SafeSerializationEventArgs : EventArgs
	{
		// Token: 0x060052E4 RID: 21220 RVA: 0x0012473E File Offset: 0x0012293E
		internal SafeSerializationEventArgs(StreamingContext streamingContext)
		{
			this.m_streamingContext = streamingContext;
		}

		/// <summary>Stores the state of the exception.</summary>
		/// <param name="serializedState">A state object that is serialized with the instance.</param>
		// Token: 0x060052E5 RID: 21221 RVA: 0x00124758 File Offset: 0x00122958
		public void AddSerializedState(ISafeSerializationData serializedState)
		{
			if (serializedState == null)
			{
				throw new ArgumentNullException("serializedState");
			}
			if (!serializedState.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Serialization_NonSerType", new object[]
				{
					serializedState.GetType(),
					serializedState.GetType().Assembly.FullName
				}));
			}
			this.m_serializedStates.Add(serializedState);
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x060052E6 RID: 21222 RVA: 0x001247BE File Offset: 0x001229BE
		internal IList<object> SerializedStates
		{
			get
			{
				return this.m_serializedStates;
			}
		}

		/// <summary>Gets or sets an object that describes the source and destination of a serialized stream.</summary>
		/// <returns>An object that describes the source and destination of a serialized stream.</returns>
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x060052E7 RID: 21223 RVA: 0x001247C6 File Offset: 0x001229C6
		public StreamingContext StreamingContext
		{
			get
			{
				return this.m_streamingContext;
			}
		}

		// Token: 0x040024BD RID: 9405
		private StreamingContext m_streamingContext;

		// Token: 0x040024BE RID: 9406
		private List<object> m_serializedStates = new List<object>();
	}
}
