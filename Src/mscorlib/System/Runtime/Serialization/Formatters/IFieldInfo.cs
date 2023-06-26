using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Allows access to field names and field types of objects that support the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface.</summary>
	// Token: 0x0200075F RID: 1887
	[ComVisible(true)]
	public interface IFieldInfo
	{
		/// <summary>Gets or sets the field names of serialized objects.</summary>
		/// <returns>The field names of serialized objects.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x0600531C RID: 21276
		// (set) Token: 0x0600531D RID: 21277
		string[] FieldNames
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		/// <summary>Gets or sets the field types of the serialized objects.</summary>
		/// <returns>The field types of the serialized objects.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x0600531E RID: 21278
		// (set) Token: 0x0600531F RID: 21279
		Type[] FieldTypes
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}
	}
}
