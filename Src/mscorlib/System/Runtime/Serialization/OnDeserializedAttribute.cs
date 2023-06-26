using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>When applied to a method, specifies that the method is called immediately after deserialization of an object in an object graph. The order of deserialization relative to other objects in the graph is non-deterministic.</summary>
	// Token: 0x0200073B RID: 1851
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class OnDeserializedAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.OnDeserializedAttribute" /> class.</summary>
		// Token: 0x060051E7 RID: 20967 RVA: 0x0012159B File Offset: 0x0011F79B
		[__DynamicallyInvokable]
		public OnDeserializedAttribute()
		{
		}
	}
}
