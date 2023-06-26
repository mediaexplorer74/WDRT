using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Provides functionality for formatting serialized objects.</summary>
	// Token: 0x02000730 RID: 1840
	[ComVisible(true)]
	public interface IFormatter
	{
		/// <summary>Deserializes the data on the provided stream and reconstitutes the graph of objects.</summary>
		/// <param name="serializationStream">The stream that contains the data to deserialize.</param>
		/// <returns>The top object of the deserialized graph.</returns>
		// Token: 0x060051BE RID: 20926
		object Deserialize(Stream serializationStream);

		/// <summary>Serializes an object, or graph of objects with the given root to the provided stream.</summary>
		/// <param name="serializationStream">The stream where the formatter puts the serialized data. This stream can reference a variety of backing stores (such as files, network, memory, and so on).</param>
		/// <param name="graph">The object, or root of the object graph, to serialize. All child objects of this root object are automatically serialized.</param>
		// Token: 0x060051BF RID: 20927
		void Serialize(Stream serializationStream, object graph);

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> used by the current formatter.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SurrogateSelector" /> used by this formatter.</returns>
		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x060051C0 RID: 20928
		// (set) Token: 0x060051C1 RID: 20929
		ISurrogateSelector SurrogateSelector { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.SerializationBinder" /> that performs type lookups during deserialization.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SerializationBinder" /> that performs type lookups during deserialization.</returns>
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060051C2 RID: 20930
		// (set) Token: 0x060051C3 RID: 20931
		SerializationBinder Binder { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.StreamingContext" /> used for serialization and deserialization.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.StreamingContext" /> used for serialization and deserialization.</returns>
		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060051C4 RID: 20932
		// (set) Token: 0x060051C5 RID: 20933
		StreamingContext Context { get; set; }
	}
}
