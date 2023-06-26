using System;

namespace System.Reflection
{
	/// <summary>Contains methods for converting <see cref="T:System.Type" /> objects.</summary>
	// Token: 0x020005EA RID: 1514
	[__DynamicallyInvokable]
	public static class IntrospectionExtensions
	{
		/// <summary>Returns the <see cref="T:System.Reflection.TypeInfo" /> representation of the specified type.</summary>
		/// <param name="type">The type to convert.</param>
		/// <returns>The converted object.</returns>
		// Token: 0x0600466A RID: 18026 RVA: 0x00103C9C File Offset: 0x00101E9C
		[__DynamicallyInvokable]
		public static TypeInfo GetTypeInfo(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			IReflectableType reflectableType = (IReflectableType)type;
			if (reflectableType == null)
			{
				return null;
			}
			return reflectableType.GetTypeInfo();
		}
	}
}
