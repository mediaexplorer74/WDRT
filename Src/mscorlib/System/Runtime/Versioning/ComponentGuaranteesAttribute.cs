using System;

namespace System.Runtime.Versioning
{
	/// <summary>Defines the compatibility guarantee of a component, type, or type member that may span multiple versions.</summary>
	// Token: 0x0200071C RID: 1820
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class ComponentGuaranteesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.ComponentGuaranteesAttribute" /> class with a value that indicates a library, type, or member's guaranteed level of compatibility across multiple versions.</summary>
		/// <param name="guarantees">One of the enumeration values that specifies the level of compatibility that is guaranteed across multiple versions.</param>
		// Token: 0x0600516C RID: 20844 RVA: 0x00120498 File Offset: 0x0011E698
		public ComponentGuaranteesAttribute(ComponentGuaranteesOptions guarantees)
		{
			this._guarantees = guarantees;
		}

		/// <summary>Gets a value that indicates the guaranteed level of compatibility of a library, type, or type member that spans multiple versions.</summary>
		/// <returns>One of the enumeration values that specifies the level of compatibility that is guaranteed across multiple versions.</returns>
		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x0600516D RID: 20845 RVA: 0x001204A7 File Offset: 0x0011E6A7
		public ComponentGuaranteesOptions Guarantees
		{
			get
			{
				return this._guarantees;
			}
		}

		// Token: 0x04002416 RID: 9238
		private ComponentGuaranteesOptions _guarantees;
	}
}
