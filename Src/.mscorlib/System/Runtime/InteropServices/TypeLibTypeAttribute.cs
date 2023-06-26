using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Contains the <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> that were originally imported for this type from the COM type library.</summary>
	// Token: 0x02000924 RID: 2340
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see langword="TypeLibTypeAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value for the attributed type as found in the type library it was imported from.</param>
		// Token: 0x0600602C RID: 24620 RVA: 0x0014CFE5 File Offset: 0x0014B1E5
		public TypeLibTypeAttribute(TypeLibTypeFlags flags)
		{
			this._val = flags;
		}

		/// <summary>Initializes a new instance of the <see langword="TypeLibTypeAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value for the attributed type as found in the type library it was imported from.</param>
		// Token: 0x0600602D RID: 24621 RVA: 0x0014CFF4 File Offset: 0x0014B1F4
		public TypeLibTypeAttribute(short flags)
		{
			this._val = (TypeLibTypeFlags)flags;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value for this type.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value for this type.</returns>
		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x0600602E RID: 24622 RVA: 0x0014D003 File Offset: 0x0014B203
		public TypeLibTypeFlags Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002AAF RID: 10927
		internal TypeLibTypeFlags _val;
	}
}
