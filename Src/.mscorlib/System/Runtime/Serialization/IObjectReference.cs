using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
	/// <summary>Indicates that the current interface implementer is a reference to another object.</summary>
	// Token: 0x02000732 RID: 1842
	[ComVisible(true)]
	public interface IObjectReference
	{
		/// <summary>Returns the real object that should be deserialized, rather than the object that the serialized stream specifies.</summary>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> from which the current object is deserialized.</param>
		/// <returns>The actual object that is put into the graph.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. The call will not work on a medium trusted server.</exception>
		// Token: 0x060051D7 RID: 20951
		[SecurityCritical]
		object GetRealObject(StreamingContext context);
	}
}
