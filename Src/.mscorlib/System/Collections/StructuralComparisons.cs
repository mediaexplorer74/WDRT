using System;

namespace System.Collections
{
	/// <summary>Provides objects for performing a structural comparison of two collection objects.</summary>
	// Token: 0x020004A5 RID: 1189
	[__DynamicallyInvokable]
	public static class StructuralComparisons
	{
		/// <summary>Gets a predefined object that performs a structural comparison of two objects.</summary>
		/// <returns>A predefined object that is used to perform a structural comparison of two collection objects.</returns>
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06003914 RID: 14612 RVA: 0x000DBC64 File Offset: 0x000D9E64
		[__DynamicallyInvokable]
		public static IComparer StructuralComparer
		{
			[__DynamicallyInvokable]
			get
			{
				IComparer comparer = StructuralComparisons.s_StructuralComparer;
				if (comparer == null)
				{
					comparer = new StructuralComparer();
					StructuralComparisons.s_StructuralComparer = comparer;
				}
				return comparer;
			}
		}

		/// <summary>Gets a predefined object that compares two objects for structural equality.</summary>
		/// <returns>A predefined object that is used to compare two collection objects for structural equality.</returns>
		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06003915 RID: 14613 RVA: 0x000DBC8C File Offset: 0x000D9E8C
		[__DynamicallyInvokable]
		public static IEqualityComparer StructuralEqualityComparer
		{
			[__DynamicallyInvokable]
			get
			{
				IEqualityComparer equalityComparer = StructuralComparisons.s_StructuralEqualityComparer;
				if (equalityComparer == null)
				{
					equalityComparer = new StructuralEqualityComparer();
					StructuralComparisons.s_StructuralEqualityComparer = equalityComparer;
				}
				return equalityComparer;
			}
		}

		// Token: 0x04001916 RID: 6422
		private static volatile IComparer s_StructuralComparer;

		// Token: 0x04001917 RID: 6423
		private static volatile IEqualityComparer s_StructuralEqualityComparer;
	}
}
