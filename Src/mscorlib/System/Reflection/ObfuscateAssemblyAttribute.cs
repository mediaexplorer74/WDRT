using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Instructs obfuscation tools to use their standard obfuscation rules for the appropriate assembly type.</summary>
	// Token: 0x0200060D RID: 1549
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	public sealed class ObfuscateAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ObfuscateAssemblyAttribute" /> class, specifying whether the assembly to be obfuscated is public or private.</summary>
		/// <param name="assemblyIsPrivate">
		///   <see langword="true" /> if the assembly is used within the scope of one application; otherwise, <see langword="false" />.</param>
		// Token: 0x06004806 RID: 18438 RVA: 0x0010751C File Offset: 0x0010571C
		public ObfuscateAssemblyAttribute(bool assemblyIsPrivate)
		{
			this.m_assemblyIsPrivate = assemblyIsPrivate;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value indicating whether the assembly was marked private.</summary>
		/// <returns>
		///   <see langword="true" /> if the assembly was marked private; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06004807 RID: 18439 RVA: 0x00107532 File Offset: 0x00105732
		public bool AssemblyIsPrivate
		{
			get
			{
				return this.m_assemblyIsPrivate;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should remove the attribute after processing.</summary>
		/// <returns>
		///   <see langword="true" /> if the obfuscation tool should remove the attribute after processing; otherwise, <see langword="false" />. The default value for this property is <see langword="true" />.</returns>
		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06004808 RID: 18440 RVA: 0x0010753A File Offset: 0x0010573A
		// (set) Token: 0x06004809 RID: 18441 RVA: 0x00107542 File Offset: 0x00105742
		public bool StripAfterObfuscation
		{
			get
			{
				return this.m_strip;
			}
			set
			{
				this.m_strip = value;
			}
		}

		// Token: 0x04001DCB RID: 7627
		private bool m_assemblyIsPrivate;

		// Token: 0x04001DCC RID: 7628
		private bool m_strip = true;
	}
}
