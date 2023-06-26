using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Provides access to manifest resources, which are XML files that describe application dependencies.</summary>
	// Token: 0x020005F1 RID: 1521
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public class ManifestResourceInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ManifestResourceInfo" /> class for a resource that is contained by the specified assembly and file, and that has the specified location.</summary>
		/// <param name="containingAssembly">The assembly that contains the manifest resource.</param>
		/// <param name="containingFileName">The name of the file that contains the manifest resource, if the file is not the same as the manifest file.</param>
		/// <param name="resourceLocation">A bitwise combination of enumeration values that provides information about the location of the manifest resource.</param>
		// Token: 0x0600468C RID: 18060 RVA: 0x00103ED7 File Offset: 0x001020D7
		[__DynamicallyInvokable]
		public ManifestResourceInfo(Assembly containingAssembly, string containingFileName, ResourceLocation resourceLocation)
		{
			this._containingAssembly = containingAssembly;
			this._containingFileName = containingFileName;
			this._resourceLocation = resourceLocation;
		}

		/// <summary>Gets the containing assembly for the manifest resource.</summary>
		/// <returns>The manifest resource's containing assembly.</returns>
		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x0600468D RID: 18061 RVA: 0x00103EF4 File Offset: 0x001020F4
		[__DynamicallyInvokable]
		public virtual Assembly ReferencedAssembly
		{
			[__DynamicallyInvokable]
			get
			{
				return this._containingAssembly;
			}
		}

		/// <summary>Gets the name of the file that contains the manifest resource, if it is not the same as the manifest file.</summary>
		/// <returns>The manifest resource's file name.</returns>
		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x0600468E RID: 18062 RVA: 0x00103EFC File Offset: 0x001020FC
		[__DynamicallyInvokable]
		public virtual string FileName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._containingFileName;
			}
		}

		/// <summary>Gets the manifest resource's location.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Reflection.ResourceLocation" /> flags that indicates the location of the manifest resource.</returns>
		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600468F RID: 18063 RVA: 0x00103F04 File Offset: 0x00102104
		[__DynamicallyInvokable]
		public virtual ResourceLocation ResourceLocation
		{
			[__DynamicallyInvokable]
			get
			{
				return this._resourceLocation;
			}
		}

		// Token: 0x04001CE0 RID: 7392
		private Assembly _containingAssembly;

		// Token: 0x04001CE1 RID: 7393
		private string _containingFileName;

		// Token: 0x04001CE2 RID: 7394
		private ResourceLocation _resourceLocation;
	}
}
