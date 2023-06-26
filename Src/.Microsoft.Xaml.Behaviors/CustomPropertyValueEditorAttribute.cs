using System;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class CustomPropertyValueEditorAttribute : Attribute
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000028E0 File Offset: 0x00000AE0
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000028E8 File Offset: 0x00000AE8
		public CustomPropertyValueEditor CustomPropertyValueEditor { get; private set; }

		// Token: 0x06000026 RID: 38 RVA: 0x000028F1 File Offset: 0x00000AF1
		public CustomPropertyValueEditorAttribute(CustomPropertyValueEditor customPropertyValueEditor)
		{
			this.CustomPropertyValueEditor = customPropertyValueEditor;
		}
	}
}
