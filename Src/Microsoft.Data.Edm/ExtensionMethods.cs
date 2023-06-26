using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Evaluation;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Validation;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm
{
	// Token: 0x020001B3 RID: 435
	public static class ExtensionMethods
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x0001D3C3 File Offset: 0x0001B5C3
		public static Version GetEdmVersion(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmVersion");
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001D3E2 File Offset: 0x0001B5E2
		public static void SetEdmVersion(this IEdmModel model, Version version)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmVersion", version);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0001D402 File Offset: 0x0001B602
		public static IEdmSchemaType FindType(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findType, new Func<IEdmSchemaType, IEdmSchemaType, IEdmSchemaType>(RegistrationHelper.CreateAmbiguousTypeBinding));
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001D434 File Offset: 0x0001B634
		public static IEdmValueTerm FindValueTerm(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findValueTerm, new Func<IEdmValueTerm, IEdmValueTerm, IEdmValueTerm>(RegistrationHelper.CreateAmbiguousValueTermBinding));
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001D466 File Offset: 0x0001B666
		public static IEnumerable<IEdmFunction> FindFunctions(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findFunctions, ExtensionMethods.mergeFunctions);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001D491 File Offset: 0x0001B691
		public static IEdmEntityContainer FindEntityContainer(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findEntityContainer, new Func<IEdmEntityContainer, IEdmEntityContainer, IEdmEntityContainer>(RegistrationHelper.CreateAmbiguousEntityContainerBinding));
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001D4C3 File Offset: 0x0001B6C3
		public static IEnumerable<IEdmEntityContainer> EntityContainers(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.SchemaElements.OfType<IEdmEntityContainer>();
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0001D4DC File Offset: 0x0001B6DC
		public static IEnumerable<IEdmVocabularyAnnotation> FindVocabularyAnnotationsIncludingInheritedAnnotations(this IEdmModel model, IEdmVocabularyAnnotatable element)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			IEnumerable<IEdmVocabularyAnnotation> enumerable = model.FindDeclaredVocabularyAnnotations(element);
			IEdmStructuredType edmStructuredType = element as IEdmStructuredType;
			if (edmStructuredType != null)
			{
				for (edmStructuredType = edmStructuredType.BaseType; edmStructuredType != null; edmStructuredType = edmStructuredType.BaseType)
				{
					IEdmVocabularyAnnotatable edmVocabularyAnnotatable = edmStructuredType as IEdmVocabularyAnnotatable;
					if (edmVocabularyAnnotatable != null)
					{
						enumerable = enumerable.Concat(model.FindDeclaredVocabularyAnnotations(edmVocabularyAnnotatable));
					}
				}
			}
			return enumerable;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0001D540 File Offset: 0x0001B740
		public static IEnumerable<IEdmVocabularyAnnotation> FindVocabularyAnnotations(this IEdmModel model, IEdmVocabularyAnnotatable element)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			IEnumerable<IEdmVocabularyAnnotation> enumerable = model.FindVocabularyAnnotationsIncludingInheritedAnnotations(element);
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				enumerable = enumerable.Concat(edmModel.FindVocabularyAnnotationsIncludingInheritedAnnotations(element));
			}
			return enumerable;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001D5B8 File Offset: 0x0001B7B8
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			return model.FindVocabularyAnnotations(element, term, null);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001D5E8 File Offset: 0x0001B7E8
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term, string qualifier) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			List<T> list = null;
			foreach (T t in model.FindVocabularyAnnotations(element).OfType<T>())
			{
				if (t.Term == term && (qualifier == null || qualifier == t.Qualifier))
				{
					if (list == null)
					{
						list = new List<T>();
					}
					list.Add(t);
				}
			}
			return list ?? Enumerable.Empty<T>();
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001D69C File Offset: 0x0001B89C
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			return model.FindVocabularyAnnotations(element, termName, null);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001D968 File Offset: 0x0001BB68
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			string namespaceName;
			string name;
			if (EdmUtil.TryGetNamespaceNameFromQualifiedName(termName, out namespaceName, out name))
			{
				foreach (T annotation in model.FindVocabularyAnnotations(element).OfType<T>())
				{
					T t = annotation;
					IEdmTerm annotationTerm = t.Term;
					if (annotationTerm.Namespace == namespaceName && annotationTerm.Name == name)
					{
						if (qualifier != null)
						{
							T t2 = annotation;
							if (!(qualifier == t2.Qualifier))
							{
								continue;
							}
						}
						yield return annotation;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001D99C File Offset: 0x0001BB9C
		public static IEdmValue GetPropertyValue(this IEdmModel model, IEdmStructuredValue context, IEdmProperty property, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetPropertyValue(context, context.Type.AsEntity().EntityDefinition(), property, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001DA00 File Offset: 0x0001BC00
		public static IEdmValue GetPropertyValue(this IEdmModel model, IEdmStructuredValue context, IEdmProperty property, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetPropertyValue(context, context.Type.AsEntity().EntityDefinition(), property, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001DA64 File Offset: 0x0001BC64
		public static T GetPropertyValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmProperty property, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetPropertyValue(context, context.Type.AsEntity().EntityDefinition(), property, null, new Func<IEdmExpression, IEdmStructuredValue, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001DAC8 File Offset: 0x0001BCC8
		public static T GetPropertyValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmProperty property, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetPropertyValue(context, context.Type.AsEntity().EntityDefinition(), property, qualifier, new Func<IEdmExpression, IEdmStructuredValue, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001DB2C File Offset: 0x0001BD2C
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, string termName, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0001DB90 File Offset: 0x0001BD90
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, string termName, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001DBF4 File Offset: 0x0001BDF4
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, IEdmValueTerm term, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001DC58 File Offset: 0x0001BE58
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, IEdmValueTerm term, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001DCBC File Offset: 0x0001BEBC
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, string termName, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001DD20 File Offset: 0x0001BF20
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, string termName, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001DD84 File Offset: 0x0001BF84
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmValueTerm term, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001DDE8 File Offset: 0x0001BFE8
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmValueTerm term, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001DE4C File Offset: 0x0001C04C
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001DEA0 File Offset: 0x0001C0A0
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001DEF4 File Offset: 0x0001C0F4
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001DF48 File Offset: 0x0001C148
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0001DF9C File Offset: 0x0001C19C
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001DFF0 File Offset: 0x0001C1F0
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001E044 File Offset: 0x0001C244
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001E098 File Offset: 0x0001C298
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001E0EC File Offset: 0x0001C2EC
		public static IEdmDocumentation GetDocumentation(this IEdmModel model, IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return (IEdmDocumentation)model.GetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/documentation", "Documentation");
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001E11C File Offset: 0x0001C31C
		public static void SetDocumentation(this IEdmModel model, IEdmElement element, IEdmDocumentation documentation)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			model.SetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/documentation", "Documentation", documentation);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001E148 File Offset: 0x0001C348
		public static object GetAnnotationValue(this IEdmModel model, IEdmElement element, string namespaceName, string localName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.DirectValueAnnotationsManager.GetAnnotationValue(element, namespaceName, localName);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001E170 File Offset: 0x0001C370
		public static T GetAnnotationValue<T>(this IEdmModel model, IEdmElement element, string namespaceName, string localName) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return ExtensionMethods.AnnotationValue<T>(model.GetAnnotationValue(element, namespaceName, localName));
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001E198 File Offset: 0x0001C398
		public static T GetAnnotationValue<T>(this IEdmModel model, IEdmElement element) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.GetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/internal", ExtensionMethods.TypeName<T>.LocalName);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001E1C3 File Offset: 0x0001C3C3
		public static void SetAnnotationValue(this IEdmModel model, IEdmElement element, string namespaceName, string localName, object value)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			model.DirectValueAnnotationsManager.SetAnnotationValue(element, namespaceName, localName, value);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001E1F0 File Offset: 0x0001C3F0
		public static IEnumerable<IEdmSchemaElement> SchemaElementsAcrossModels(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			IEnumerable<IEdmSchemaElement> enumerable = new IEdmSchemaElement[0];
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				enumerable = enumerable.Concat(edmModel.SchemaElements);
			}
			enumerable = enumerable.Concat(model.SchemaElements);
			return enumerable;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001E264 File Offset: 0x0001C464
		public static IEnumerable<IEdmStructuredType> FindAllDerivedTypes(this IEdmModel model, IEdmStructuredType baseType)
		{
			List<IEdmStructuredType> list = new List<IEdmStructuredType>();
			if (baseType is IEdmSchemaElement)
			{
				model.DerivedFrom(baseType, new HashSetInternal<IEdmStructuredType>(), list);
			}
			return list;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0001E28D File Offset: 0x0001C48D
		public static void SetAnnotationValue<T>(this IEdmModel model, IEdmElement element, T value) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			model.SetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/internal", ExtensionMethods.TypeName<T>.LocalName, value);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001E2BE File Offset: 0x0001C4BE
		public static object[] GetAnnotationValues(this IEdmModel model, IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmDirectValueAnnotationBinding>>(annotations, "annotations");
			return model.DirectValueAnnotationsManager.GetAnnotationValues(annotations);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001E2E4 File Offset: 0x0001C4E4
		public static void SetAnnotationValues(this IEdmModel model, IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmDirectValueAnnotationBinding>>(annotations, "annotations");
			model.DirectValueAnnotationsManager.SetAnnotationValues(annotations);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001E30A File Offset: 0x0001C50A
		public static IEnumerable<IEdmDirectValueAnnotation> DirectValueAnnotations(this IEdmModel model, IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.DirectValueAnnotationsManager.GetDirectValueAnnotations(element);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001E330 File Offset: 0x0001C530
		public static EdmLocation Location(this IEdmElement item)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(item, "item");
			IEdmLocatable edmLocatable = item as IEdmLocatable;
			if (edmLocatable == null || edmLocatable.Location == null)
			{
				return new ObjectLocation(item);
			}
			return edmLocatable.Location;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001E368 File Offset: 0x0001C568
		public static IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations(this IEdmVocabularyAnnotatable element, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.FindVocabularyAnnotations(element);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001E389 File Offset: 0x0001C589
		public static string FullName(this IEdmSchemaElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmSchemaElement>(element, "element");
			return (element.Namespace ?? string.Empty) + "." + (element.Name ?? string.Empty);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001E3BF File Offset: 0x0001C5BF
		public static IEnumerable<IEdmEntitySet> EntitySets(this IEdmEntityContainer container)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			return container.Elements.OfType<IEdmEntitySet>();
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001E3D8 File Offset: 0x0001C5D8
		public static IEnumerable<IEdmFunctionImport> FunctionImports(this IEdmEntityContainer container)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			return container.Elements.OfType<IEdmFunctionImport>();
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001E3F4 File Offset: 0x0001C5F4
		public static EdmTypeKind TypeKind(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmType definition = type.Definition;
			if (definition == null)
			{
				return EdmTypeKind.None;
			}
			return definition.TypeKind;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001E420 File Offset: 0x0001C620
		public static string FullName(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmSchemaElement edmSchemaElement = type.Definition as IEdmSchemaElement;
			if (edmSchemaElement == null)
			{
				return null;
			}
			return edmSchemaElement.FullName();
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001E450 File Offset: 0x0001C650
		public static IEdmPrimitiveType PrimitiveDefinition(this IEdmPrimitiveTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmPrimitiveTypeReference>(type, "type");
			return (IEdmPrimitiveType)type.Definition;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001E46C File Offset: 0x0001C66C
		public static EdmPrimitiveTypeKind PrimitiveKind(this IEdmPrimitiveTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmPrimitiveTypeReference>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type.PrimitiveDefinition();
			if (edmPrimitiveType == null)
			{
				return EdmPrimitiveTypeKind.None;
			}
			return edmPrimitiveType.PrimitiveKind;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001E71C File Offset: 0x0001C91C
		public static IEnumerable<IEdmProperty> Properties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			if (type.BaseType != null)
			{
				foreach (IEdmProperty baseProperty in type.BaseType.Properties())
				{
					yield return baseProperty;
				}
			}
			if (type.DeclaredProperties != null)
			{
				foreach (IEdmProperty declaredProperty in type.DeclaredProperties)
				{
					yield return declaredProperty;
				}
			}
			yield break;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001E739 File Offset: 0x0001C939
		public static IEnumerable<IEdmStructuralProperty> DeclaredStructuralProperties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return type.DeclaredProperties.OfType<IEdmStructuralProperty>();
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001E752 File Offset: 0x0001C952
		public static IEnumerable<IEdmStructuralProperty> StructuralProperties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return type.Properties().OfType<IEdmStructuralProperty>();
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001E76B File Offset: 0x0001C96B
		public static IEdmStructuredType StructuredDefinition(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return (IEdmStructuredType)type.Definition;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0001E784 File Offset: 0x0001C984
		public static bool IsAbstract(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().IsAbstract;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0001E79D File Offset: 0x0001C99D
		public static bool IsOpen(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().IsOpen;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001E7B6 File Offset: 0x0001C9B6
		public static IEdmStructuredType BaseType(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().BaseType;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001E7CF File Offset: 0x0001C9CF
		public static IEnumerable<IEdmStructuralProperty> DeclaredStructuralProperties(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().DeclaredStructuralProperties();
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001E7E8 File Offset: 0x0001C9E8
		public static IEnumerable<IEdmStructuralProperty> StructuralProperties(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().StructuralProperties();
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001E801 File Offset: 0x0001CA01
		public static IEdmProperty FindProperty(this IEdmStructuredTypeReference type, string name)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			return type.StructuredDefinition().FindProperty(name);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001E827 File Offset: 0x0001CA27
		public static IEdmEntityType BaseEntityType(this IEdmEntityType type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			return type.BaseType as IEdmEntityType;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0001E840 File Offset: 0x0001CA40
		public static IEnumerable<IEdmNavigationProperty> DeclaredNavigationProperties(this IEdmEntityType type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			return type.DeclaredProperties.OfType<IEdmNavigationProperty>();
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0001E859 File Offset: 0x0001CA59
		public static IEnumerable<IEdmNavigationProperty> NavigationProperties(this IEdmEntityType type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			return type.Properties().OfType<IEdmNavigationProperty>();
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001E874 File Offset: 0x0001CA74
		public static IEnumerable<IEdmStructuralProperty> Key(this IEdmEntityType type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			for (IEdmEntityType edmEntityType = type; edmEntityType != null; edmEntityType = edmEntityType.BaseEntityType())
			{
				if (edmEntityType.DeclaredKey != null)
				{
					return edmEntityType.DeclaredKey;
				}
			}
			return Enumerable.Empty<IEdmStructuralProperty>();
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001E8C4 File Offset: 0x0001CAC4
		public static bool HasDeclaredKeyProperty(this IEdmEntityType entityType, IEdmProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(entityType, "entityType");
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			while (entityType != null)
			{
				if (entityType.DeclaredKey != null)
				{
					if (entityType.DeclaredKey.Any((IEdmStructuralProperty k) => k == property))
					{
						return true;
					}
				}
				entityType = entityType.BaseEntityType();
			}
			return false;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001E933 File Offset: 0x0001CB33
		public static IEdmEntityType EntityDefinition(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return (IEdmEntityType)type.Definition;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001E94C File Offset: 0x0001CB4C
		public static IEdmEntityType BaseEntityType(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().BaseEntityType();
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001E965 File Offset: 0x0001CB65
		public static IEnumerable<IEdmStructuralProperty> Key(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().Key();
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0001E97E File Offset: 0x0001CB7E
		public static IEnumerable<IEdmNavigationProperty> NavigationProperties(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().NavigationProperties();
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001E997 File Offset: 0x0001CB97
		public static IEnumerable<IEdmNavigationProperty> DeclaredNavigationProperties(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().DeclaredNavigationProperties();
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001E9B0 File Offset: 0x0001CBB0
		public static IEdmNavigationProperty FindNavigationProperty(this IEdmEntityTypeReference type, string name)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			return type.EntityDefinition().FindProperty(name) as IEdmNavigationProperty;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001E9DB File Offset: 0x0001CBDB
		public static IEdmComplexType BaseComplexType(this IEdmComplexType type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexType>(type, "type");
			return type.BaseType as IEdmComplexType;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public static IEdmComplexType ComplexDefinition(this IEdmComplexTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexTypeReference>(type, "type");
			return (IEdmComplexType)type.Definition;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001EA0D File Offset: 0x0001CC0D
		public static IEdmComplexType BaseComplexType(this IEdmComplexTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexTypeReference>(type, "type");
			return type.ComplexDefinition().BaseComplexType();
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001EA26 File Offset: 0x0001CC26
		public static IEdmEntityReferenceType EntityReferenceDefinition(this IEdmEntityReferenceTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityReferenceTypeReference>(type, "type");
			return (IEdmEntityReferenceType)type.Definition;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001EA3F File Offset: 0x0001CC3F
		public static IEdmEntityType EntityType(this IEdmEntityReferenceTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityReferenceTypeReference>(type, "type");
			return type.EntityReferenceDefinition().EntityType;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001EA58 File Offset: 0x0001CC58
		public static IEdmCollectionType CollectionDefinition(this IEdmCollectionTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmCollectionTypeReference>(type, "type");
			return (IEdmCollectionType)type.Definition;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001EA71 File Offset: 0x0001CC71
		public static IEdmTypeReference ElementType(this IEdmCollectionTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmCollectionTypeReference>(type, "type");
			return type.CollectionDefinition().ElementType;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001EA8A File Offset: 0x0001CC8A
		public static IEdmEnumType EnumDefinition(this IEdmEnumTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumTypeReference>(type, "type");
			return (IEdmEnumType)type.Definition;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001EAA4 File Offset: 0x0001CCA4
		public static EdmMultiplicity Multiplicity(this IEdmNavigationProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			IEdmNavigationProperty partner = property.Partner;
			if (partner == null)
			{
				return EdmMultiplicity.One;
			}
			IEdmTypeReference type = partner.Type;
			if (type.IsCollection())
			{
				return EdmMultiplicity.Many;
			}
			if (!type.IsNullable)
			{
				return EdmMultiplicity.One;
			}
			return EdmMultiplicity.ZeroOrOne;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001EAE8 File Offset: 0x0001CCE8
		public static IEdmEntityType ToEntityType(this IEdmNavigationProperty property)
		{
			IEdmType edmType = property.Type.Definition;
			if (edmType.TypeKind == EdmTypeKind.Collection)
			{
				edmType = ((IEdmCollectionType)edmType).ElementType.Definition;
			}
			if (edmType.TypeKind == EdmTypeKind.EntityReference)
			{
				edmType = ((IEdmEntityReferenceType)edmType).EntityType;
			}
			return edmType as IEdmEntityType;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001EB36 File Offset: 0x0001CD36
		public static IEdmEntityType DeclaringEntityType(this IEdmNavigationProperty property)
		{
			return (IEdmEntityType)property.DeclaringType;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001EB43 File Offset: 0x0001CD43
		public static IEdmRowType RowDefinition(this IEdmRowTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmRowTypeReference>(type, "type");
			return (IEdmRowType)type.Definition;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001EB5C File Offset: 0x0001CD5C
		public static IEdmPropertyValueBinding FindPropertyBinding(this IEdmTypeAnnotation annotation, IEdmProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			foreach (IEdmPropertyValueBinding edmPropertyValueBinding in annotation.PropertyValueBindings)
			{
				if (edmPropertyValueBinding.BoundProperty == property)
				{
					return edmPropertyValueBinding;
				}
			}
			return null;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001EBCC File Offset: 0x0001CDCC
		public static IEdmPropertyValueBinding FindPropertyBinding(this IEdmTypeAnnotation annotation, string propertyName)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<string>(propertyName, "propertyName");
			foreach (IEdmPropertyValueBinding edmPropertyValueBinding in annotation.PropertyValueBindings)
			{
				if (edmPropertyValueBinding.BoundProperty.Name.EqualsOrdinal(propertyName))
				{
					return edmPropertyValueBinding;
				}
			}
			return null;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001EC44 File Offset: 0x0001CE44
		public static IEdmValueTerm ValueTerm(this IEdmValueAnnotation annotation)
		{
			EdmUtil.CheckArgumentNull<IEdmValueAnnotation>(annotation, "annotation");
			return (IEdmValueTerm)annotation.Term;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0001EC60 File Offset: 0x0001CE60
		public static bool TryGetStaticEntitySet(this IEdmFunctionImport functionImport, out IEdmEntitySet entitySet)
		{
			IEdmEntitySetReferenceExpression edmEntitySetReferenceExpression = functionImport.EntitySet as IEdmEntitySetReferenceExpression;
			entitySet = ((edmEntitySetReferenceExpression != null) ? edmEntitySetReferenceExpression.ReferencedEntitySet : null);
			return entitySet != null;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001EC90 File Offset: 0x0001CE90
		public static bool TryGetRelativeEntitySetPath(this IEdmFunctionImport functionImport, IEdmModel model, out IEdmFunctionParameter parameter, out IEnumerable<IEdmNavigationProperty> path)
		{
			parameter = null;
			path = null;
			IEdmPathExpression edmPathExpression = functionImport.EntitySet as IEdmPathExpression;
			if (edmPathExpression == null)
			{
				return false;
			}
			List<string> list = edmPathExpression.Path.ToList<string>();
			if (list.Count == 0)
			{
				return false;
			}
			parameter = functionImport.FindParameter(list[0]);
			if (parameter == null)
			{
				return false;
			}
			if (list.Count == 1)
			{
				path = Enumerable.Empty<IEdmNavigationProperty>();
				return true;
			}
			IEdmEntityType edmEntityType = ExtensionMethods.GetPathSegmentEntityType(parameter.Type);
			List<IEdmNavigationProperty> list2 = new List<IEdmNavigationProperty>();
			for (int i = 1; i < list.Count; i++)
			{
				string text = list[i];
				if (EdmUtil.IsQualifiedName(text))
				{
					if (i == list.Count - 1)
					{
						return false;
					}
					IEdmEntityType edmEntityType2 = model.FindDeclaredType(text) as IEdmEntityType;
					if (edmEntityType2 == null || !edmEntityType2.IsOrInheritsFrom(edmEntityType))
					{
						return false;
					}
					edmEntityType = edmEntityType2;
				}
				else
				{
					IEdmNavigationProperty edmNavigationProperty = edmEntityType.FindProperty(text) as IEdmNavigationProperty;
					if (edmNavigationProperty == null)
					{
						return false;
					}
					list2.Add(edmNavigationProperty);
					edmEntityType = ExtensionMethods.GetPathSegmentEntityType(edmNavigationProperty.Type);
				}
			}
			path = list2;
			return true;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001ED8C File Offset: 0x0001CF8C
		public static IEdmPropertyConstructor FindProperty(this IEdmRecordExpression expression, string name)
		{
			foreach (IEdmPropertyConstructor edmPropertyConstructor in expression.Properties)
			{
				if (edmPropertyConstructor.Name == name)
				{
					return edmPropertyConstructor;
				}
			}
			return null;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001EDE8 File Offset: 0x0001CFE8
		internal static IEdmEntityType GetPathSegmentEntityType(IEdmTypeReference segmentType)
		{
			return (segmentType.IsCollection() ? segmentType.AsCollection().ElementType() : segmentType).AsEntity().EntityDefinition();
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001EE0C File Offset: 0x0001D00C
		private static T FindAcrossModels<T>(this IEdmModel model, string qualifiedName, Func<IEdmModel, string, T> finder, Func<T, T, T> ambiguousCreator)
		{
			T t = finder(model, qualifiedName);
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				T t2 = finder(edmModel, qualifiedName);
				if (t2 != null)
				{
					t = ((t == null) ? t2 : ambiguousCreator(t, t2));
				}
			}
			return t;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001EE84 File Offset: 0x0001D084
		private static T GetPropertyValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmEntityType contextType, IEdmProperty property, string qualifier, Func<IEdmExpression, IEdmStructuredValue, T> evaluator)
		{
			IEdmEntityType edmEntityType = (IEdmEntityType)property.DeclaringType;
			IEnumerable<IEdmTypeAnnotation> enumerable = model.FindVocabularyAnnotations(contextType, edmEntityType, qualifier);
			if (enumerable.Count<IEdmTypeAnnotation>() != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoTermTypeAnnotationOnType(contextType.ToTraceString(), edmEntityType.ToTraceString()));
			}
			IEdmPropertyValueBinding edmPropertyValueBinding = enumerable.Single<IEdmTypeAnnotation>().FindPropertyBinding(property);
			if (edmPropertyValueBinding == null)
			{
				return default(T);
			}
			return evaluator(edmPropertyValueBinding.Value, context);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0001EEF0 File Offset: 0x0001D0F0
		private static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmEntityType contextType, IEdmValueTerm term, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmValueAnnotation> enumerable = model.FindVocabularyAnnotations(contextType, term, qualifier);
			if (enumerable.Count<IEdmValueAnnotation>() != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnType(contextType.ToTraceString(), term.ToTraceString()));
			}
			return evaluator(enumerable.Single<IEdmValueAnnotation>().Value, context, term.Type);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0001EF44 File Offset: 0x0001D144
		private static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmEntityType contextType, string termName, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmValueAnnotation> enumerable = model.FindVocabularyAnnotations(contextType, termName, qualifier);
			if (enumerable.Count<IEdmValueAnnotation>() != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnType(contextType.ToTraceString(), termName));
			}
			IEdmValueAnnotation edmValueAnnotation = enumerable.Single<IEdmValueAnnotation>();
			return evaluator(edmValueAnnotation.Value, context, edmValueAnnotation.ValueTerm().Type);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001EF98 File Offset: 0x0001D198
		private static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmValueAnnotation> enumerable = model.FindVocabularyAnnotations(element, term, qualifier);
			if (enumerable.Count<IEdmValueAnnotation>() != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnElement(term.ToTraceString()));
			}
			return evaluator(enumerable.Single<IEdmValueAnnotation>().Value, null, term.Type);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001EFE4 File Offset: 0x0001D1E4
		private static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmValueAnnotation> enumerable = model.FindVocabularyAnnotations(element, termName, qualifier);
			if (enumerable.Count<IEdmValueAnnotation>() != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnElement(termName));
			}
			IEdmValueAnnotation edmValueAnnotation = enumerable.Single<IEdmValueAnnotation>();
			return evaluator(edmValueAnnotation.Value, null, edmValueAnnotation.ValueTerm().Type);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0001F030 File Offset: 0x0001D230
		private static T AnnotationValue<T>(object annotation) where T : class
		{
			if (annotation == null)
			{
				return default(T);
			}
			T t = annotation as T;
			if (t != null)
			{
				return t;
			}
			IEdmValue edmValue = annotation as IEdmValue;
			throw new InvalidOperationException(Strings.Annotations_TypeMismatch(annotation.GetType().Name, typeof(T).Name));
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001F090 File Offset: 0x0001D290
		private static void DerivedFrom(this IEdmModel model, IEdmStructuredType baseType, HashSetInternal<IEdmStructuredType> visited, List<IEdmStructuredType> derivedTypes)
		{
			if (visited.Add(baseType))
			{
				IEnumerable<IEdmStructuredType> enumerable = model.FindDirectlyDerivedTypes(baseType);
				if (enumerable != null && enumerable.Any<IEdmStructuredType>())
				{
					foreach (IEdmStructuredType edmStructuredType in enumerable)
					{
						derivedTypes.Add(edmStructuredType);
						model.DerivedFrom(edmStructuredType, visited, derivedTypes);
					}
				}
				foreach (IEdmModel edmModel in model.ReferencedModels)
				{
					enumerable = edmModel.FindDirectlyDerivedTypes(baseType);
					if (enumerable != null && enumerable.Any<IEdmStructuredType>())
					{
						foreach (IEdmStructuredType edmStructuredType2 in enumerable)
						{
							derivedTypes.Add(edmStructuredType2);
							model.DerivedFrom(edmStructuredType2, visited, derivedTypes);
						}
					}
				}
			}
		}

		// Token: 0x040004B5 RID: 1205
		private static readonly Func<IEdmModel, string, IEdmSchemaType> findType = (IEdmModel model, string qualifiedName) => model.FindDeclaredType(qualifiedName);

		// Token: 0x040004B6 RID: 1206
		private static readonly Func<IEdmModel, string, IEdmValueTerm> findValueTerm = (IEdmModel model, string qualifiedName) => model.FindDeclaredValueTerm(qualifiedName);

		// Token: 0x040004B7 RID: 1207
		private static readonly Func<IEdmModel, string, IEnumerable<IEdmFunction>> findFunctions = (IEdmModel model, string qualifiedName) => model.FindDeclaredFunctions(qualifiedName);

		// Token: 0x040004B8 RID: 1208
		private static readonly Func<IEdmModel, string, IEdmEntityContainer> findEntityContainer = (IEdmModel model, string qualifiedName) => model.FindDeclaredEntityContainer(qualifiedName);

		// Token: 0x040004B9 RID: 1209
		private static readonly Func<IEnumerable<IEdmFunction>, IEnumerable<IEdmFunction>, IEnumerable<IEdmFunction>> mergeFunctions = (IEnumerable<IEdmFunction> f1, IEnumerable<IEdmFunction> f2) => f1.Concat(f2);

		// Token: 0x020001B4 RID: 436
		internal static class TypeName<T>
		{
			// Token: 0x040004BF RID: 1215
			public static readonly string LocalName = typeof(T).ToString().Replace("_", "_____").Replace('.', '_')
				.Replace("[", "")
				.Replace("]", "")
				.Replace(",", "__")
				.Replace("`", "___")
				.Replace("+", "____");
		}
	}
}
