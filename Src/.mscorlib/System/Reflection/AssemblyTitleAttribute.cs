using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies a description for an assembly.</summary>
	// Token: 0x020005B7 RID: 1463
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyTitleAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyTitleAttribute" /> class.</summary>
		/// <param name="title">The assembly title.</param>
		// Token: 0x06004473 RID: 17523 RVA: 0x000FDB8F File Offset: 0x000FBD8F
		[__DynamicallyInvokable]
		public AssemblyTitleAttribute(string title)
		{
			this.m_title = title;
		}

		/// <summary>Gets assembly title information.</summary>
		/// <returns>The assembly title.</returns>
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06004474 RID: 17524 RVA: 0x000FDB9E File Offset: 0x000FBD9E
		[__DynamicallyInvokable]
		public string Title
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_title;
			}
		}

		// Token: 0x04001C0A RID: 7178
		private string m_title;
	}
}
