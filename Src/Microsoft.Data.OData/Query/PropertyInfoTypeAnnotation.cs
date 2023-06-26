using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000BF RID: 191
	internal sealed class PropertyInfoTypeAnnotation
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x0000FD30 File Offset: 0x0000DF30
		internal static PropertyInfoTypeAnnotation GetPropertyInfoTypeAnnotation(IEdmStructuredType structuredType, IEdmModel model)
		{
			PropertyInfoTypeAnnotation propertyInfoTypeAnnotation = model.GetAnnotationValue(structuredType);
			if (propertyInfoTypeAnnotation == null)
			{
				propertyInfoTypeAnnotation = new PropertyInfoTypeAnnotation();
				model.SetAnnotationValue(structuredType, propertyInfoTypeAnnotation);
			}
			return propertyInfoTypeAnnotation;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000FD58 File Offset: 0x0000DF58
		internal PropertyInfo GetPropertyInfo(IEdmStructuredType structuredType, IEdmProperty property, IEdmModel model)
		{
			if (this.propertyInfosDeclaredOnThisType == null)
			{
				this.propertyInfosDeclaredOnThisType = new Dictionary<IEdmProperty, PropertyInfo>(ReferenceEqualityComparer<IEdmProperty>.Instance);
			}
			PropertyInfo property2;
			if (!this.propertyInfosDeclaredOnThisType.TryGetValue(property, out property2))
			{
				BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
				property2 = structuredType.GetInstanceType(model).GetProperty(property.Name, bindingFlags);
				if (property2 == null)
				{
					throw new ODataException(Strings.PropertyInfoTypeAnnotation_CannotFindProperty(structuredType.ODataFullName(), structuredType.GetInstanceType(model), property.Name));
				}
				this.propertyInfosDeclaredOnThisType.Add(property, property2);
			}
			return property2;
		}

		// Token: 0x04000194 RID: 404
		private Dictionary<IEdmProperty, PropertyInfo> propertyInfosDeclaredOnThisType;
	}
}
