using System;

namespace System.Runtime
{
	/// <summary>Specifies patch band information for targeted patching of the .NET Framework.</summary>
	// Token: 0x02000716 RID: 1814
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTargetedPatchBandAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.AssemblyTargetedPatchBandAttribute" /> class.</summary>
		/// <param name="targetedPatchBand">The patch band.</param>
		// Token: 0x06005147 RID: 20807 RVA: 0x0011FDD7 File Offset: 0x0011DFD7
		public AssemblyTargetedPatchBandAttribute(string targetedPatchBand)
		{
			this.m_targetedPatchBand = targetedPatchBand;
		}

		/// <summary>Gets the patch band.</summary>
		/// <returns>The patch band information.</returns>
		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06005148 RID: 20808 RVA: 0x0011FDE6 File Offset: 0x0011DFE6
		public string TargetedPatchBand
		{
			get
			{
				return this.m_targetedPatchBand;
			}
		}

		// Token: 0x04002403 RID: 9219
		private string m_targetedPatchBand;
	}
}
