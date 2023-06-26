using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
	/// <summary>Implements a serialization surrogate selector that allows one object to perform serialization and deserialization of another.</summary>
	// Token: 0x02000734 RID: 1844
	[ComVisible(true)]
	public interface ISerializationSurrogate
	{
		/// <summary>Populates the provided <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the object.</summary>
		/// <param name="obj">The object to serialize.</param>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060051D9 RID: 20953
		[SecurityCritical]
		void GetObjectData(object obj, SerializationInfo info, StreamingContext context);

		/// <summary>Populates the object using the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" />.</summary>
		/// <param name="obj">The object to populate.</param>
		/// <param name="info">The information to populate the object.</param>
		/// <param name="context">The source from which the object is deserialized.</param>
		/// <param name="selector">The surrogate selector where the search for a compatible surrogate begins.</param>
		/// <returns>The populated deserialized object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x060051DA RID: 20954
		[SecurityCritical]
		object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector);
	}
}
