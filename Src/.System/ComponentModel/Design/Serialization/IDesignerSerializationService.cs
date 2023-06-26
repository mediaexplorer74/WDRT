using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an interface that can invoke serialization and deserialization.</summary>
	// Token: 0x0200060C RID: 1548
	public interface IDesignerSerializationService
	{
		/// <summary>Deserializes the specified serialization data object and returns a collection of objects represented by that data.</summary>
		/// <param name="serializationData">An object consisting of serialized data.</param>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of objects rebuilt from the specified serialization data object.</returns>
		// Token: 0x060038B6 RID: 14518
		ICollection Deserialize(object serializationData);

		/// <summary>Serializes the specified collection of objects and stores them in a serialization data object.</summary>
		/// <param name="objects">A collection of objects to serialize.</param>
		/// <returns>An object that contains the serialized state of the specified collection of objects.</returns>
		// Token: 0x060038B7 RID: 14519
		object Serialize(ICollection objects);
	}
}
