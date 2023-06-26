using System;
using System.Data.Services.Client.Metadata;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000044 RID: 68
	internal class ComplexValueMaterializationPolicy : StructuralValueMaterializationPolicy
	{
		// Token: 0x0600021F RID: 543 RVA: 0x0000B374 File Offset: 0x00009574
		internal ComplexValueMaterializationPolicy(IODataMaterializerContext materializerContext, SimpleLazy<PrimitivePropertyConverter> lazyPrimitivePropertyConverter)
			: base(materializerContext, lazyPrimitivePropertyConverter)
		{
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000B380 File Offset: 0x00009580
		internal void MaterializeComplexTypeProperty(Type propertyType, ODataComplexValue complexValue)
		{
			if (complexValue == null || complexValue.HasMaterializedValue())
			{
				return;
			}
			ClientTypeAnnotation clientTypeAnnotation;
			if (WebUtil.IsWireTypeCollection(complexValue.TypeName))
			{
				clientTypeAnnotation = base.MaterializerContext.ResolveTypeForMaterialization(propertyType, complexValue.TypeName);
			}
			else
			{
				ClientEdmModel model = base.MaterializerContext.Model;
				clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(propertyType));
			}
			object obj = this.CreateNewInstance(clientTypeAnnotation.EdmType.ToEdmTypeReference(true), propertyType);
			base.MaterializeDataValues(clientTypeAnnotation, complexValue.Properties, base.MaterializerContext.IgnoreMissingProperties);
			base.ApplyDataValues(clientTypeAnnotation, complexValue.Properties, obj);
			complexValue.SetMaterializedValue(obj);
		}
	}
}
