using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>When applied to a method, specifies that the method is called during deserialization of an object in an object graph. The order of deserialization relative to other objects in the graph is non-deterministic.</summary>
	// Token: 0x0200073A RID: 1850
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class OnDeserializingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.OnDeserializingAttribute" /> class.</summary>
		// Token: 0x060051E6 RID: 20966 RVA: 0x00121593 File Offset: 0x0011F793
		[__DynamicallyInvokable]
		public OnDeserializingAttribute()
		{
		}
	}
}
