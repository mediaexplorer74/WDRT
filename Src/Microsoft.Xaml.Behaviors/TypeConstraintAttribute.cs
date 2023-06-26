using System;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class TypeConstraintAttribute : Attribute
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005055 File Offset: 0x00003255
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000505D File Offset: 0x0000325D
		public Type Constraint { get; private set; }

		// Token: 0x06000129 RID: 297 RVA: 0x00005066 File Offset: 0x00003266
		public TypeConstraintAttribute(Type constraint)
		{
			this.Constraint = constraint;
		}
	}
}
