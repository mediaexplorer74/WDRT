using System;
using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that the use of a value tuple on a member is meant to be treated as a tuple with element names.</summary>
	// Token: 0x020008FC RID: 2300
	[CLSCompliant(false)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public sealed class TupleElementNamesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.TupleElementNamesAttribute" /> class.</summary>
		/// <param name="transformNames">A string array that specifies, in a pre-order depth-first traversal of a type's construction, which value tuple occurrences are meant to carry element names.</param>
		// Token: 0x06005E6C RID: 24172 RVA: 0x0014CCA3 File Offset: 0x0014AEA3
		public TupleElementNamesAttribute(string[] transformNames)
		{
			if (transformNames == null)
			{
				throw new ArgumentNullException("transformNames");
			}
			this._transformNames = transformNames;
		}

		/// <summary>Specifies, in a pre-order depth-first traversal of a type's construction, which value tuple elements are meant to carry element names.</summary>
		/// <returns>An array that indicates which value tuple elements are meant to carry element names.</returns>
		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x06005E6D RID: 24173 RVA: 0x0014CCC0 File Offset: 0x0014AEC0
		public IList<string> TransformNames
		{
			get
			{
				return this._transformNames;
			}
		}

		// Token: 0x04002A61 RID: 10849
		private readonly string[] _transformNames;
	}
}
