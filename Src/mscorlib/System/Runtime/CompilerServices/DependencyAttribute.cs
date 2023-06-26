using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates when a dependency is to be loaded by the referring assembly. This class cannot be inherited.</summary>
	// Token: 0x020008C3 RID: 2243
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Serializable]
	public sealed class DependencyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DependencyAttribute" /> class with the specified <see cref="T:System.Runtime.CompilerServices.LoadHint" /> value.</summary>
		/// <param name="dependentAssemblyArgument">The dependent assembly to bind to.</param>
		/// <param name="loadHintArgument">One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values.</param>
		// Token: 0x06005DD6 RID: 24022 RVA: 0x0014B016 File Offset: 0x00149216
		public DependencyAttribute(string dependentAssemblyArgument, LoadHint loadHintArgument)
		{
			this.dependentAssembly = dependentAssemblyArgument;
			this.loadHint = loadHintArgument;
		}

		/// <summary>Gets the value of the dependent assembly.</summary>
		/// <returns>The name of the dependent assembly.</returns>
		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06005DD7 RID: 24023 RVA: 0x0014B02C File Offset: 0x0014922C
		public string DependentAssembly
		{
			get
			{
				return this.dependentAssembly;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> value that indicates when an assembly is to load a dependency.</summary>
		/// <returns>One of the <see cref="T:System.Runtime.CompilerServices.LoadHint" /> values.</returns>
		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06005DD8 RID: 24024 RVA: 0x0014B034 File Offset: 0x00149234
		public LoadHint LoadHint
		{
			get
			{
				return this.loadHint;
			}
		}

		// Token: 0x04002A39 RID: 10809
		private string dependentAssembly;

		// Token: 0x04002A3A RID: 10810
		private LoadHint loadHint;
	}
}
