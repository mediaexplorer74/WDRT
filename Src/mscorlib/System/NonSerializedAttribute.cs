using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates that a field of a serializable class should not be serialized. This class cannot be inherited.</summary>
	// Token: 0x02000113 RID: 275
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class NonSerializedAttribute : Attribute
	{
		// Token: 0x06001072 RID: 4210 RVA: 0x000314B0 File Offset: 0x0002F6B0
		internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
		{
			if ((field.Attributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope)
			{
				return null;
			}
			return new NonSerializedAttribute();
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x000314C7 File Offset: 0x0002F6C7
		internal static bool IsDefined(RuntimeFieldInfo field)
		{
			return (field.Attributes & FieldAttributes.NotSerialized) > FieldAttributes.PrivateScope;
		}
	}
}
