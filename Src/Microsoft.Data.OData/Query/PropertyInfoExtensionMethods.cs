using System;
using System.Reflection;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000BE RID: 190
	internal static class PropertyInfoExtensionMethods
	{
		// Token: 0x060004A2 RID: 1186 RVA: 0x0000FD0C File Offset: 0x0000DF0C
		internal static PropertyInfo GetPropertyInfo(this IEdmStructuredTypeReference typeReference, IEdmProperty property, IEdmModel model)
		{
			IEdmStructuredType edmStructuredType = typeReference.StructuredDefinition();
			return PropertyInfoTypeAnnotation.GetPropertyInfoTypeAnnotation(edmStructuredType, model).GetPropertyInfo(edmStructuredType, property, model);
		}
	}
}
