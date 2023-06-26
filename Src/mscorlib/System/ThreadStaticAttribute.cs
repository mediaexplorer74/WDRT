using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates that the value of a static field is unique for each thread.</summary>
	// Token: 0x02000161 RID: 353
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ThreadStaticAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ThreadStaticAttribute" /> class.</summary>
		// Token: 0x060015CD RID: 5581 RVA: 0x000403D4 File Offset: 0x0003E5D4
		[__DynamicallyInvokable]
		public ThreadStaticAttribute()
		{
		}
	}
}
