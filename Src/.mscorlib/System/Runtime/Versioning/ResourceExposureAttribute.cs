using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	/// <summary>Specifies the resource exposure for a member of a class. This class cannot be inherited.</summary>
	// Token: 0x0200071E RID: 1822
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	public sealed class ResourceExposureAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.ResourceExposureAttribute" /> class with the specified exposure level.</summary>
		/// <param name="exposureLevel">The scope of the resource.</param>
		// Token: 0x06005172 RID: 20850 RVA: 0x001204F0 File Offset: 0x0011E6F0
		public ResourceExposureAttribute(ResourceScope exposureLevel)
		{
			this._resourceExposureLevel = exposureLevel;
		}

		/// <summary>Gets the resource exposure scope.</summary>
		/// <returns>A <see cref="T:System.Runtime.Versioning.ResourceScope" /> object.</returns>
		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06005173 RID: 20851 RVA: 0x001204FF File Offset: 0x0011E6FF
		public ResourceScope ResourceExposureLevel
		{
			get
			{
				return this._resourceExposureLevel;
			}
		}

		// Token: 0x04002419 RID: 9241
		private ResourceScope _resourceExposureLevel;
	}
}
