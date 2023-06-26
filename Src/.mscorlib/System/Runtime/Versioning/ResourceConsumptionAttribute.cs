using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	/// <summary>Specifies the resource consumed by the member of a class. This class cannot be inherited.</summary>
	// Token: 0x0200071D RID: 1821
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	public sealed class ResourceConsumptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.ResourceConsumptionAttribute" /> class specifying the scope of the consumed resource.</summary>
		/// <param name="resourceScope">The <see cref="T:System.Runtime.Versioning.ResourceScope" /> for the consumed resource.</param>
		// Token: 0x0600516E RID: 20846 RVA: 0x001204AF File Offset: 0x0011E6AF
		public ResourceConsumptionAttribute(ResourceScope resourceScope)
		{
			this._resourceScope = resourceScope;
			this._consumptionScope = this._resourceScope;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.ResourceConsumptionAttribute" /> class specifying the scope of the consumed resource and the scope of how it is consumed.</summary>
		/// <param name="resourceScope">The <see cref="T:System.Runtime.Versioning.ResourceScope" /> for the consumed resource.</param>
		/// <param name="consumptionScope">The <see cref="T:System.Runtime.Versioning.ResourceScope" /> used by this member.</param>
		// Token: 0x0600516F RID: 20847 RVA: 0x001204CA File Offset: 0x0011E6CA
		public ResourceConsumptionAttribute(ResourceScope resourceScope, ResourceScope consumptionScope)
		{
			this._resourceScope = resourceScope;
			this._consumptionScope = consumptionScope;
		}

		/// <summary>Gets the resource scope for the consumed resource.</summary>
		/// <returns>A <see cref="T:System.Runtime.Versioning.ResourceScope" /> object specifying the resource scope of the consumed member.</returns>
		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06005170 RID: 20848 RVA: 0x001204E0 File Offset: 0x0011E6E0
		public ResourceScope ResourceScope
		{
			get
			{
				return this._resourceScope;
			}
		}

		/// <summary>Gets the consumption scope for this member.</summary>
		/// <returns>A <see cref="T:System.Runtime.Versioning.ResourceScope" /> object specifying the resource scope used by this member.</returns>
		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06005171 RID: 20849 RVA: 0x001204E8 File Offset: 0x0011E6E8
		public ResourceScope ConsumptionScope
		{
			get
			{
				return this._consumptionScope;
			}
		}

		// Token: 0x04002417 RID: 9239
		private ResourceScope _consumptionScope;

		// Token: 0x04002418 RID: 9240
		private ResourceScope _resourceScope;
	}
}
