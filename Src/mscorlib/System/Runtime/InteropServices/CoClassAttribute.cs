using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the class identifier of a coclass imported from a type library.</summary>
	// Token: 0x02000938 RID: 2360
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class CoClassAttribute : Attribute
	{
		/// <summary>Initializes new instance of the <see cref="T:System.Runtime.InteropServices.CoClassAttribute" /> with the class identifier of the original coclass.</summary>
		/// <param name="coClass">A <see cref="T:System.Type" /> that contains the class identifier of the original coclass.</param>
		// Token: 0x06006067 RID: 24679 RVA: 0x0014D651 File Offset: 0x0014B851
		[__DynamicallyInvokable]
		public CoClassAttribute(Type coClass)
		{
			this._CoClass = coClass;
		}

		/// <summary>Gets the class identifier of the original coclass.</summary>
		/// <returns>A <see cref="T:System.Type" /> containing the class identifier of the original coclass.</returns>
		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06006068 RID: 24680 RVA: 0x0014D660 File Offset: 0x0014B860
		[__DynamicallyInvokable]
		public Type CoClass
		{
			[__DynamicallyInvokable]
			get
			{
				return this._CoClass;
			}
		}

		// Token: 0x04002B2D RID: 11053
		internal Type _CoClass;
	}
}
