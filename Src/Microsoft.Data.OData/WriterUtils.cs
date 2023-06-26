using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000259 RID: 601
	internal static class WriterUtils
	{
		// Token: 0x060013D2 RID: 5074 RVA: 0x0004AA1E File Offset: 0x00048C1E
		internal static bool ShouldSkipProperty(this ProjectedPropertiesAnnotation projectedProperties, string propertyName)
		{
			return projectedProperties != null && (object.ReferenceEquals(ProjectedPropertiesAnnotation.EmptyProjectedPropertiesInstance, projectedProperties) || (!object.ReferenceEquals(ProjectedPropertiesAnnotation.AllProjectedPropertiesInstance, projectedProperties) && !projectedProperties.IsPropertyProjected(propertyName)));
		}
	}
}
