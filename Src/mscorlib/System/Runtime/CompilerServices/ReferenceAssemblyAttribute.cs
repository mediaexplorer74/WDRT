using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Identifies an assembly as a reference assembly, which contains metadata but no executable code.</summary>
	// Token: 0x020008E0 RID: 2272
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class ReferenceAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.ReferenceAssemblyAttribute" /> class.</summary>
		// Token: 0x06005DF5 RID: 24053 RVA: 0x0014B196 File Offset: 0x00149396
		[__DynamicallyInvokable]
		public ReferenceAssemblyAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.ReferenceAssemblyAttribute" /> class by using the specified description.</summary>
		/// <param name="description">The description of the reference assembly.</param>
		// Token: 0x06005DF6 RID: 24054 RVA: 0x0014B19E File Offset: 0x0014939E
		[__DynamicallyInvokable]
		public ReferenceAssemblyAttribute(string description)
		{
			this._description = description;
		}

		/// <summary>Gets the description of the reference assembly.</summary>
		/// <returns>The description of the reference assembly.</returns>
		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06005DF7 RID: 24055 RVA: 0x0014B1AD File Offset: 0x001493AD
		[__DynamicallyInvokable]
		public string Description
		{
			[__DynamicallyInvokable]
			get
			{
				return this._description;
			}
		}

		// Token: 0x04002A43 RID: 10819
		private string _description;
	}
}
