using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Validation.Internal
{
	// Token: 0x020001EF RID: 495
	internal class InterfaceValidator
	{
		// Token: 0x06000BE5 RID: 3045 RVA: 0x00022B08 File Offset: 0x00020D08
		private InterfaceValidator(HashSetInternal<object> skipVisitation, IEdmModel model, bool validateDirectValueAnnotations)
		{
			this.skipVisitation = skipVisitation;
			this.model = model;
			this.validateDirectValueAnnotations = validateDirectValueAnnotations;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00022B78 File Offset: 0x00020D78
		public static IEnumerable<EdmError> ValidateModelStructureAndSemantics(IEdmModel model, ValidationRuleSet semanticRuleSet)
		{
			InterfaceValidator modelValidator = new InterfaceValidator(null, model, true);
			List<EdmError> list = new List<EdmError>(modelValidator.ValidateStructure(model));
			InterfaceValidator referencesValidator = new InterfaceValidator(modelValidator.visited, model, false);
			IEnumerable<object> enumerable = modelValidator.danglingReferences;
			while (enumerable.FirstOrDefault<object>() != null)
			{
				foreach (object obj in enumerable)
				{
					list.AddRange(referencesValidator.ValidateStructure(obj));
				}
				enumerable = referencesValidator.danglingReferences.ToArray<object>();
			}
			if (list.Any(new Func<EdmError, bool>(ValidationHelper.IsInterfaceCritical)))
			{
				return list;
			}
			ValidationContext validationContext = new ValidationContext(model, (object item) => modelValidator.visitedBad.Contains(item) || referencesValidator.visitedBad.Contains(item));
			Dictionary<Type, List<ValidationRule>> dictionary = new Dictionary<Type, List<ValidationRule>>();
			foreach (object obj2 in modelValidator.visited)
			{
				if (!modelValidator.visitedBad.Contains(obj2))
				{
					foreach (ValidationRule validationRule in InterfaceValidator.GetSemanticInterfaceVisitorsForObject(obj2.GetType(), semanticRuleSet, dictionary))
					{
						validationRule.Evaluate(validationContext, obj2);
					}
				}
			}
			list.AddRange(validationContext.Errors);
			return list;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00022D20 File Offset: 0x00020F20
		public static IEnumerable<EdmError> GetStructuralErrors(IEdmElement item)
		{
			IEdmModel edmModel = item as IEdmModel;
			InterfaceValidator interfaceValidator = new InterfaceValidator(null, edmModel, edmModel != null);
			return interfaceValidator.ValidateStructure(item);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00022D4C File Offset: 0x00020F4C
		private static Dictionary<Type, InterfaceValidator.VisitorBase> CreateInterfaceVisitorsMap()
		{
			Dictionary<Type, InterfaceValidator.VisitorBase> dictionary = new Dictionary<Type, InterfaceValidator.VisitorBase>();
			foreach (Type type in typeof(InterfaceValidator).GetNonPublicNestedTypes())
			{
				if (type.IsClass())
				{
					Type baseType = type.GetBaseType();
					if (baseType.IsGenericType() && baseType.GetBaseType() == typeof(InterfaceValidator.VisitorBase))
					{
						dictionary.Add(baseType.GetGenericArguments()[0], (InterfaceValidator.VisitorBase)Activator.CreateInstance(type));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00022DEC File Offset: 0x00020FEC
		private static IEnumerable<InterfaceValidator.VisitorBase> ComputeInterfaceVisitorsForObject(Type objectType)
		{
			List<InterfaceValidator.VisitorBase> list = new List<InterfaceValidator.VisitorBase>();
			foreach (Type type in objectType.GetInterfaces())
			{
				InterfaceValidator.VisitorBase visitorBase;
				if (InterfaceValidator.InterfaceVisitors.TryGetValue(type, out visitorBase))
				{
					list.Add(visitorBase);
				}
			}
			return list;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00022E34 File Offset: 0x00021034
		private static EdmError CreatePropertyMustNotBeNullError<T>(T item, string propertyName)
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalPropertyValueMustNotBeNull, Strings.EdmModel_Validator_Syntactic_PropertyMustNotBeNull(typeof(T).Name, propertyName));
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00022E5D File Offset: 0x0002105D
		private static EdmError CreateEnumPropertyOutOfRangeError<T, E>(T item, E enumValue, string propertyName)
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalEnumPropertyValueOutOfRange, Strings.EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange(typeof(T).Name, propertyName, typeof(E).Name, enumValue));
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00022E9C File Offset: 0x0002109C
		private static EdmError CheckForInterfaceKindValueMismatchError<T, K, I>(T item, K kind, string propertyName)
		{
			if (item is I)
			{
				return null;
			}
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueMismatch, Strings.EdmModel_Validator_Syntactic_InterfaceKindValueMismatch(kind, typeof(T).Name, propertyName, typeof(I).Name));
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00022EF4 File Offset: 0x000210F4
		private static EdmError CreateInterfaceKindValueUnexpectedError<T, K>(T item, K kind, string propertyName)
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueUnexpected, Strings.EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected(kind, typeof(T).Name, propertyName));
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00022F23 File Offset: 0x00021123
		private static EdmError CreateTypeRefInterfaceTypeKindValueMismatchError<T>(T item) where T : IEdmTypeReference
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueMismatch, Strings.EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch(typeof(T).Name, item.Definition.TypeKind));
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00022F64 File Offset: 0x00021164
		private static EdmError CreatePrimitiveTypeRefInterfaceTypeKindValueMismatchError<T>(T item) where T : IEdmPrimitiveTypeReference
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueMismatch, Strings.EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch(typeof(T).Name, ((IEdmPrimitiveType)item.Definition).PrimitiveKind));
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00022FB4 File Offset: 0x000211B4
		private static void ProcessEnumerable<T, E>(T item, IEnumerable<E> enumerable, string propertyName, IList targetList, ref List<EdmError> errors)
		{
			if (enumerable == null)
			{
				InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<T>(item, propertyName), ref errors);
				return;
			}
			foreach (E e in enumerable)
			{
				if (e == null)
				{
					InterfaceValidator.CollectErrors(new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalEnumerableMustNotHaveNullElements, Strings.EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements(typeof(T).Name, propertyName)), ref errors);
					break;
				}
				targetList.Add(e);
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00023050 File Offset: 0x00021250
		private static void CollectErrors(EdmError newError, ref List<EdmError> errors)
		{
			if (newError != null)
			{
				if (errors == null)
				{
					errors = new List<EdmError>();
				}
				errors.Add(newError);
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00023068 File Offset: 0x00021268
		private static bool IsCheckableBad(object element)
		{
			IEdmCheckable edmCheckable = element as IEdmCheckable;
			return edmCheckable != null && edmCheckable.Errors != null && edmCheckable.Errors.Count<EdmError>() > 0;
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00023098 File Offset: 0x00021298
		private static EdmLocation GetLocation(object item)
		{
			IEdmLocatable edmLocatable = item as IEdmLocatable;
			if (edmLocatable == null || edmLocatable.Location == null)
			{
				return new ObjectLocation(item);
			}
			return edmLocatable.Location;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000230C4 File Offset: 0x000212C4
		private static IEnumerable<ValidationRule> GetSemanticInterfaceVisitorsForObject(Type objectType, ValidationRuleSet ruleSet, Dictionary<Type, List<ValidationRule>> concreteTypeSemanticInterfaceVisitors)
		{
			List<ValidationRule> list;
			if (!concreteTypeSemanticInterfaceVisitors.TryGetValue(objectType, out list))
			{
				list = new List<ValidationRule>();
				foreach (Type type in objectType.GetInterfaces())
				{
					list.AddRange(ruleSet.GetRules(type));
				}
				concreteTypeSemanticInterfaceVisitors.Add(objectType, list);
			}
			return list;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00023114 File Offset: 0x00021314
		private IEnumerable<EdmError> ValidateStructure(object item)
		{
			if (item is IEdmValidCoreModelElement || this.visited.Contains(item) || (this.skipVisitation != null && this.skipVisitation.Contains(item)))
			{
				return Enumerable.Empty<EdmError>();
			}
			this.visited.Add(item);
			if (this.danglingReferences.Contains(item))
			{
				this.danglingReferences.Remove(item);
			}
			List<EdmError> list = null;
			List<object> list2 = new List<object>();
			List<object> list3 = new List<object>();
			IEnumerable<InterfaceValidator.VisitorBase> enumerable = InterfaceValidator.ConcreteTypeInterfaceVisitors.Evaluate(item.GetType());
			foreach (InterfaceValidator.VisitorBase visitorBase in enumerable)
			{
				IEnumerable<EdmError> enumerable2 = visitorBase.Visit(item, list2, list3);
				if (enumerable2 != null)
				{
					foreach (EdmError edmError in enumerable2)
					{
						if (list == null)
						{
							list = new List<EdmError>();
						}
						list.Add(edmError);
					}
				}
			}
			if (list != null)
			{
				this.visitedBad.Add(item);
				return list;
			}
			List<EdmError> list4 = new List<EdmError>();
			if (this.validateDirectValueAnnotations)
			{
				IEdmElement edmElement = item as IEdmElement;
				if (edmElement != null)
				{
					foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation in this.model.DirectValueAnnotations(edmElement))
					{
						list4.AddRange(this.ValidateStructure(edmDirectValueAnnotation));
					}
				}
			}
			foreach (object obj in list2)
			{
				list4.AddRange(this.ValidateStructure(obj));
			}
			foreach (object obj2 in list3)
			{
				this.CollectReference(obj2);
			}
			return list4;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00023334 File Offset: 0x00021534
		private void CollectReference(object reference)
		{
			if (!(reference is IEdmValidCoreModelElement) && !this.visited.Contains(reference) && (this.skipVisitation == null || !this.skipVisitation.Contains(reference)))
			{
				this.danglingReferences.Add(reference);
			}
		}

		// Token: 0x04000590 RID: 1424
		private static readonly Dictionary<Type, InterfaceValidator.VisitorBase> InterfaceVisitors = InterfaceValidator.CreateInterfaceVisitorsMap();

		// Token: 0x04000591 RID: 1425
		private static readonly Memoizer<Type, IEnumerable<InterfaceValidator.VisitorBase>> ConcreteTypeInterfaceVisitors = new Memoizer<Type, IEnumerable<InterfaceValidator.VisitorBase>>(new Func<Type, IEnumerable<InterfaceValidator.VisitorBase>>(InterfaceValidator.ComputeInterfaceVisitorsForObject), null);

		// Token: 0x04000592 RID: 1426
		private readonly HashSetInternal<object> visited = new HashSetInternal<object>();

		// Token: 0x04000593 RID: 1427
		private readonly HashSetInternal<object> visitedBad = new HashSetInternal<object>();

		// Token: 0x04000594 RID: 1428
		private readonly HashSetInternal<object> danglingReferences = new HashSetInternal<object>();

		// Token: 0x04000595 RID: 1429
		private readonly HashSetInternal<object> skipVisitation;

		// Token: 0x04000596 RID: 1430
		private readonly bool validateDirectValueAnnotations;

		// Token: 0x04000597 RID: 1431
		private readonly IEdmModel model;

		// Token: 0x020001F0 RID: 496
		private abstract class VisitorBase
		{
			// Token: 0x06000BF8 RID: 3064
			public abstract IEnumerable<EdmError> Visit(object item, List<object> followup, List<object> references);
		}

		// Token: 0x020001F1 RID: 497
		private abstract class VisitorOfT<T> : InterfaceValidator.VisitorBase
		{
			// Token: 0x06000BFA RID: 3066 RVA: 0x0002339A File Offset: 0x0002159A
			public override IEnumerable<EdmError> Visit(object item, List<object> followup, List<object> references)
			{
				return this.VisitT((T)((object)item), followup, references);
			}

			// Token: 0x06000BFB RID: 3067
			protected abstract IEnumerable<EdmError> VisitT(T item, List<object> followup, List<object> references);
		}

		// Token: 0x020001F2 RID: 498
		private sealed class VisitorOfIEdmCheckable : InterfaceValidator.VisitorOfT<IEdmCheckable>
		{
			// Token: 0x06000BFD RID: 3069 RVA: 0x000233B4 File Offset: 0x000215B4
			protected override IEnumerable<EdmError> VisitT(IEdmCheckable checkable, List<object> followup, List<object> references)
			{
				List<EdmError> list = new List<EdmError>();
				List<EdmError> list2 = null;
				InterfaceValidator.ProcessEnumerable<IEdmCheckable, EdmError>(checkable, checkable.Errors, "Errors", list, ref list2);
				return list2 ?? list;
			}
		}

		// Token: 0x020001F3 RID: 499
		private sealed class VisitorOfIEdmElement : InterfaceValidator.VisitorOfT<IEdmElement>
		{
			// Token: 0x06000BFF RID: 3071 RVA: 0x000233EB File Offset: 0x000215EB
			protected override IEnumerable<EdmError> VisitT(IEdmElement element, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x020001F4 RID: 500
		private sealed class VisitorOfIEdmNamedElement : InterfaceValidator.VisitorOfT<IEdmNamedElement>
		{
			// Token: 0x06000C01 RID: 3073 RVA: 0x000233F8 File Offset: 0x000215F8
			protected override IEnumerable<EdmError> VisitT(IEdmNamedElement element, List<object> followup, List<object> references)
			{
				if (element.Name == null)
				{
					return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmNamedElement>(element, "Name") };
				}
				return null;
			}
		}

		// Token: 0x020001F5 RID: 501
		private sealed class VisitorOfIEdmSchemaElement : InterfaceValidator.VisitorOfT<IEdmSchemaElement>
		{
			// Token: 0x06000C03 RID: 3075 RVA: 0x00023430 File Offset: 0x00021630
			protected override IEnumerable<EdmError> VisitT(IEdmSchemaElement element, List<object> followup, List<object> references)
			{
				List<EdmError> list = new List<EdmError>();
				switch (element.SchemaElementKind)
				{
				case EdmSchemaElementKind.None:
					break;
				case EdmSchemaElementKind.TypeDefinition:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmSchemaElement, EdmSchemaElementKind, IEdmSchemaType>(element, element.SchemaElementKind, "SchemaElementKind"), ref list);
					break;
				case EdmSchemaElementKind.Function:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmSchemaElement, EdmSchemaElementKind, IEdmFunction>(element, element.SchemaElementKind, "SchemaElementKind"), ref list);
					break;
				case EdmSchemaElementKind.ValueTerm:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmSchemaElement, EdmSchemaElementKind, IEdmValueTerm>(element, element.SchemaElementKind, "SchemaElementKind"), ref list);
					break;
				case EdmSchemaElementKind.EntityContainer:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmSchemaElement, EdmSchemaElementKind, IEdmEntityContainer>(element, element.SchemaElementKind, "SchemaElementKind"), ref list);
					break;
				default:
					InterfaceValidator.CollectErrors(InterfaceValidator.CreateEnumPropertyOutOfRangeError<IEdmSchemaElement, EdmSchemaElementKind>(element, element.SchemaElementKind, "SchemaElementKind"), ref list);
					break;
				}
				if (element.Namespace == null)
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmSchemaElement>(element, "Namespace"), ref list);
				}
				return list;
			}
		}

		// Token: 0x020001F6 RID: 502
		private sealed class VisitorOfIEdmModel : InterfaceValidator.VisitorOfT<IEdmModel>
		{
			// Token: 0x06000C05 RID: 3077 RVA: 0x0002350C File Offset: 0x0002170C
			protected override IEnumerable<EdmError> VisitT(IEdmModel model, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmModel, IEdmSchemaElement>(model, model.SchemaElements, "SchemaElements", followup, ref list);
				InterfaceValidator.ProcessEnumerable<IEdmModel, IEdmVocabularyAnnotation>(model, model.VocabularyAnnotations, "VocabularyAnnotations", followup, ref list);
				return list;
			}
		}

		// Token: 0x020001F7 RID: 503
		private sealed class VisitorOfIEdmEntityContainer : InterfaceValidator.VisitorOfT<IEdmEntityContainer>
		{
			// Token: 0x06000C07 RID: 3079 RVA: 0x0002354C File Offset: 0x0002174C
			protected override IEnumerable<EdmError> VisitT(IEdmEntityContainer container, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmEntityContainer, IEdmEntityContainerElement>(container, container.Elements, "Elements", followup, ref list);
				return list;
			}
		}

		// Token: 0x020001F8 RID: 504
		private sealed class VisitorOfIEdmEntityContainerElement : InterfaceValidator.VisitorOfT<IEdmEntityContainerElement>
		{
			// Token: 0x06000C09 RID: 3081 RVA: 0x00023578 File Offset: 0x00021778
			protected override IEnumerable<EdmError> VisitT(IEdmEntityContainerElement element, List<object> followup, List<object> references)
			{
				EdmError edmError = null;
				switch (element.ContainerElementKind)
				{
				case EdmContainerElementKind.None:
					break;
				case EdmContainerElementKind.EntitySet:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmEntityContainerElement, EdmContainerElementKind, IEdmEntitySet>(element, element.ContainerElementKind, "ContainerElementKind");
					break;
				case EdmContainerElementKind.FunctionImport:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmEntityContainerElement, EdmContainerElementKind, IEdmFunctionImport>(element, element.ContainerElementKind, "ContainerElementKind");
					break;
				default:
					edmError = InterfaceValidator.CreateEnumPropertyOutOfRangeError<IEdmEntityContainerElement, EdmContainerElementKind>(element, element.ContainerElementKind, "ContainerElementKind");
					break;
				}
				if (edmError == null)
				{
					return null;
				}
				return new EdmError[] { edmError };
			}
		}

		// Token: 0x020001F9 RID: 505
		private sealed class VisitorOfIEdmEntitySet : InterfaceValidator.VisitorOfT<IEdmEntitySet>
		{
			// Token: 0x06000C0B RID: 3083 RVA: 0x000235F8 File Offset: 0x000217F8
			protected override IEnumerable<EdmError> VisitT(IEdmEntitySet set, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (set.ElementType != null)
				{
					references.Add(set.ElementType);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEntitySet>(set, "ElementType"), ref list);
				}
				List<IEdmNavigationTargetMapping> list2 = new List<IEdmNavigationTargetMapping>();
				InterfaceValidator.ProcessEnumerable<IEdmEntitySet, IEdmNavigationTargetMapping>(set, set.NavigationTargets, "NavigationTargets", list2, ref list);
				foreach (IEdmNavigationTargetMapping edmNavigationTargetMapping in list2)
				{
					if (edmNavigationTargetMapping.NavigationProperty != null)
					{
						references.Add(edmNavigationTargetMapping.NavigationProperty);
					}
					else
					{
						InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmNavigationTargetMapping>(edmNavigationTargetMapping, "NavigationProperty"), ref list);
					}
					if (edmNavigationTargetMapping.TargetEntitySet != null)
					{
						references.Add(edmNavigationTargetMapping.TargetEntitySet);
					}
					else
					{
						InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmNavigationTargetMapping>(edmNavigationTargetMapping, "TargetEntitySet"), ref list);
					}
				}
				return list;
			}
		}

		// Token: 0x020001FA RID: 506
		private sealed class VisitorOfIEdmTypeReference : InterfaceValidator.VisitorOfT<IEdmTypeReference>
		{
			// Token: 0x06000C0D RID: 3085 RVA: 0x000236DC File Offset: 0x000218DC
			protected override IEnumerable<EdmError> VisitT(IEdmTypeReference type, List<object> followup, List<object> references)
			{
				if (type.Definition != null)
				{
					if (type.Definition is IEdmSchemaType)
					{
						references.Add(type.Definition);
					}
					else
					{
						followup.Add(type.Definition);
					}
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmTypeReference>(type, "Definition") };
			}
		}

		// Token: 0x020001FB RID: 507
		private sealed class VisitorOfIEdmType : InterfaceValidator.VisitorOfT<IEdmType>
		{
			// Token: 0x06000C0F RID: 3087 RVA: 0x00023738 File Offset: 0x00021938
			protected override IEnumerable<EdmError> VisitT(IEdmType type, List<object> followup, List<object> references)
			{
				EdmError edmError = null;
				switch (type.TypeKind)
				{
				case EdmTypeKind.None:
					break;
				case EdmTypeKind.Primitive:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmType, EdmTypeKind, IEdmPrimitiveType>(type, type.TypeKind, "TypeKind");
					break;
				case EdmTypeKind.Entity:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmType, EdmTypeKind, IEdmEntityType>(type, type.TypeKind, "TypeKind");
					break;
				case EdmTypeKind.Complex:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmType, EdmTypeKind, IEdmComplexType>(type, type.TypeKind, "TypeKind");
					break;
				case EdmTypeKind.Row:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmType, EdmTypeKind, IEdmRowType>(type, type.TypeKind, "TypeKind");
					break;
				case EdmTypeKind.Collection:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmType, EdmTypeKind, IEdmCollectionType>(type, type.TypeKind, "TypeKind");
					break;
				case EdmTypeKind.EntityReference:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmType, EdmTypeKind, IEdmEntityReferenceType>(type, type.TypeKind, "TypeKind");
					break;
				case EdmTypeKind.Enum:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmType, EdmTypeKind, IEdmEnumType>(type, type.TypeKind, "TypeKind");
					break;
				default:
					edmError = InterfaceValidator.CreateInterfaceKindValueUnexpectedError<IEdmType, EdmTypeKind>(type, type.TypeKind, "TypeKind");
					break;
				}
				if (edmError == null)
				{
					return null;
				}
				return new EdmError[] { edmError };
			}
		}

		// Token: 0x020001FC RID: 508
		private sealed class VisitorOfIEdmPrimitiveType : InterfaceValidator.VisitorOfT<IEdmPrimitiveType>
		{
			// Token: 0x06000C11 RID: 3089 RVA: 0x00023834 File Offset: 0x00021A34
			protected override IEnumerable<EdmError> VisitT(IEdmPrimitiveType type, List<object> followup, List<object> references)
			{
				if (!InterfaceValidator.IsCheckableBad(type) && (type.PrimitiveKind < EdmPrimitiveTypeKind.None || type.PrimitiveKind > EdmPrimitiveTypeKind.GeometryMultiPoint))
				{
					return new EdmError[] { InterfaceValidator.CreateInterfaceKindValueUnexpectedError<IEdmPrimitiveType, EdmPrimitiveTypeKind>(type, type.PrimitiveKind, "PrimitiveKind") };
				}
				return null;
			}
		}

		// Token: 0x020001FD RID: 509
		private sealed class VisitorOfIEdmStructuredType : InterfaceValidator.VisitorOfT<IEdmStructuredType>
		{
			// Token: 0x06000C13 RID: 3091 RVA: 0x00023884 File Offset: 0x00021A84
			protected override IEnumerable<EdmError> VisitT(IEdmStructuredType type, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmStructuredType, IEdmProperty>(type, type.DeclaredProperties, "DeclaredProperties", followup, ref list);
				if (type.BaseType != null)
				{
					HashSetInternal<IEdmStructuredType> hashSetInternal = new HashSetInternal<IEdmStructuredType>();
					hashSetInternal.Add(type);
					for (IEdmStructuredType edmStructuredType = type.BaseType; edmStructuredType != null; edmStructuredType = edmStructuredType.BaseType)
					{
						if (hashSetInternal.Contains(edmStructuredType))
						{
							IEdmSchemaType edmSchemaType = type as IEdmSchemaType;
							string text = ((edmSchemaType != null) ? edmSchemaType.FullName() : typeof(Type).Name);
							InterfaceValidator.CollectErrors(new EdmError(InterfaceValidator.GetLocation(type), EdmErrorCode.InterfaceCriticalCycleInTypeHierarchy, Strings.EdmModel_Validator_Syntactic_InterfaceCriticalCycleInTypeHierarchy(text)), ref list);
							break;
						}
					}
					references.Add(type.BaseType);
				}
				return list;
			}
		}

		// Token: 0x020001FE RID: 510
		private sealed class VisitorOfIEdmEntityType : InterfaceValidator.VisitorOfT<IEdmEntityType>
		{
			// Token: 0x06000C15 RID: 3093 RVA: 0x00023930 File Offset: 0x00021B30
			protected override IEnumerable<EdmError> VisitT(IEdmEntityType type, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (type.DeclaredKey != null)
				{
					InterfaceValidator.ProcessEnumerable<IEdmEntityType, IEdmStructuralProperty>(type, type.DeclaredKey, "DeclaredKey", references, ref list);
				}
				return list;
			}
		}

		// Token: 0x020001FF RID: 511
		private sealed class VisitorOfIEdmEntityReferenceType : InterfaceValidator.VisitorOfT<IEdmEntityReferenceType>
		{
			// Token: 0x06000C17 RID: 3095 RVA: 0x00023964 File Offset: 0x00021B64
			protected override IEnumerable<EdmError> VisitT(IEdmEntityReferenceType type, List<object> followup, List<object> references)
			{
				if (type.EntityType != null)
				{
					references.Add(type.EntityType);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEntityReferenceType>(type, "EntityType") };
			}
		}

		// Token: 0x02000200 RID: 512
		private sealed class VisitorOfIEdmEnumType : InterfaceValidator.VisitorOfT<IEdmEnumType>
		{
			// Token: 0x06000C19 RID: 3097 RVA: 0x000239A8 File Offset: 0x00021BA8
			protected override IEnumerable<EdmError> VisitT(IEdmEnumType type, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmEnumType, IEdmEnumMember>(type, type.Members, "Members", followup, ref list);
				if (type.UnderlyingType != null)
				{
					references.Add(type.UnderlyingType);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEnumType>(type, "UnderlyingType"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000201 RID: 513
		private sealed class VisitorOfIEdmTerm : InterfaceValidator.VisitorOfT<IEdmTerm>
		{
			// Token: 0x06000C1B RID: 3099 RVA: 0x000239FC File Offset: 0x00021BFC
			protected override IEnumerable<EdmError> VisitT(IEdmTerm term, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				switch (term.TermKind)
				{
				case EdmTermKind.None:
					break;
				case EdmTermKind.Type:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmTerm, EdmTermKind, IEdmSchemaType>(term, term.TermKind, "TermKind"), ref list);
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmTerm, EdmTermKind, IEdmStructuredType>(term, term.TermKind, "TermKind"), ref list);
					break;
				case EdmTermKind.Value:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmTerm, EdmTermKind, IEdmValueTerm>(term, term.TermKind, "TermKind"), ref list);
					break;
				default:
					InterfaceValidator.CollectErrors(InterfaceValidator.CreateInterfaceKindValueUnexpectedError<IEdmTerm, EdmTermKind>(term, term.TermKind, "TermKind"), ref list);
					break;
				}
				return list;
			}
		}

		// Token: 0x02000202 RID: 514
		private sealed class VisitorOfIEdmValueTerm : InterfaceValidator.VisitorOfT<IEdmValueTerm>
		{
			// Token: 0x06000C1D RID: 3101 RVA: 0x00023A94 File Offset: 0x00021C94
			protected override IEnumerable<EdmError> VisitT(IEdmValueTerm term, List<object> followup, List<object> references)
			{
				if (term.Type != null)
				{
					followup.Add(term.Type);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmValueTerm>(term, "Type") };
			}
		}

		// Token: 0x02000203 RID: 515
		private sealed class VisitorOfIEdmCollectionType : InterfaceValidator.VisitorOfT<IEdmCollectionType>
		{
			// Token: 0x06000C1F RID: 3103 RVA: 0x00023AD8 File Offset: 0x00021CD8
			protected override IEnumerable<EdmError> VisitT(IEdmCollectionType type, List<object> followup, List<object> references)
			{
				if (type.ElementType != null)
				{
					followup.Add(type.ElementType);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmCollectionType>(type, "ElementType") };
			}
		}

		// Token: 0x02000204 RID: 516
		private sealed class VisitorOfIEdmProperty : InterfaceValidator.VisitorOfT<IEdmProperty>
		{
			// Token: 0x06000C21 RID: 3105 RVA: 0x00023B1C File Offset: 0x00021D1C
			protected override IEnumerable<EdmError> VisitT(IEdmProperty property, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				switch (property.PropertyKind)
				{
				case EdmPropertyKind.Structural:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmProperty, EdmPropertyKind, IEdmStructuralProperty>(property, property.PropertyKind, "PropertyKind"), ref list);
					break;
				case EdmPropertyKind.Navigation:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmProperty, EdmPropertyKind, IEdmNavigationProperty>(property, property.PropertyKind, "PropertyKind"), ref list);
					break;
				case EdmPropertyKind.None:
					break;
				default:
					InterfaceValidator.CollectErrors(InterfaceValidator.CreateInterfaceKindValueUnexpectedError<IEdmProperty, EdmPropertyKind>(property, property.PropertyKind, "PropertyKind"), ref list);
					break;
				}
				if (property.Type != null)
				{
					followup.Add(property.Type);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmProperty>(property, "Type"), ref list);
				}
				if (property.DeclaringType != null)
				{
					references.Add(property.DeclaringType);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmProperty>(property, "DeclaringType"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000205 RID: 517
		private sealed class VisitorOfIEdmStructuralProperty : InterfaceValidator.VisitorOfT<IEdmStructuralProperty>
		{
			// Token: 0x06000C23 RID: 3107 RVA: 0x00023BEC File Offset: 0x00021DEC
			protected override IEnumerable<EdmError> VisitT(IEdmStructuralProperty property, List<object> followup, List<object> references)
			{
				if (property.ConcurrencyMode < EdmConcurrencyMode.None || property.ConcurrencyMode > EdmConcurrencyMode.Fixed)
				{
					return new EdmError[] { InterfaceValidator.CreateEnumPropertyOutOfRangeError<IEdmStructuralProperty, EdmConcurrencyMode>(property, property.ConcurrencyMode, "ConcurrencyMode") };
				}
				return null;
			}
		}

		// Token: 0x02000206 RID: 518
		private sealed class VisitorOfIEdmNavigationProperty : InterfaceValidator.VisitorOfT<IEdmNavigationProperty>
		{
			// Token: 0x06000C25 RID: 3109 RVA: 0x00023C34 File Offset: 0x00021E34
			protected override IEnumerable<EdmError> VisitT(IEdmNavigationProperty property, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (property.Partner != null)
				{
					if (!property.Partner.DeclaringType.DeclaredProperties.Contains(property.Partner))
					{
						followup.Add(property.Partner);
					}
					else
					{
						references.Add(property.Partner);
					}
					if (property.Partner.Partner != property || property.Partner == property)
					{
						InterfaceValidator.CollectErrors(new EdmError(InterfaceValidator.GetLocation(property), EdmErrorCode.InterfaceCriticalNavigationPartnerInvalid, Strings.EdmModel_Validator_Syntactic_NavigationPartnerInvalid(property.Name)), ref list);
					}
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmNavigationProperty>(property, "Partner"), ref list);
				}
				if (property.DependentProperties != null)
				{
					InterfaceValidator.ProcessEnumerable<IEdmNavigationProperty, IEdmStructuralProperty>(property, property.DependentProperties, "DependentProperties", references, ref list);
				}
				if (property.OnDelete < EdmOnDeleteAction.None || property.OnDelete > EdmOnDeleteAction.Cascade)
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreateEnumPropertyOutOfRangeError<IEdmNavigationProperty, EdmOnDeleteAction>(property, property.OnDelete, "OnDelete"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000207 RID: 519
		private sealed class VisitorOfIEdmEnumMember : InterfaceValidator.VisitorOfT<IEdmEnumMember>
		{
			// Token: 0x06000C27 RID: 3111 RVA: 0x00023D1C File Offset: 0x00021F1C
			protected override IEnumerable<EdmError> VisitT(IEdmEnumMember member, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (member.DeclaringType != null)
				{
					references.Add(member.DeclaringType);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEnumMember>(member, "DeclaringType"), ref list);
				}
				if (member.Value != null)
				{
					followup.Add(member.Value);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEnumMember>(member, "Value"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000208 RID: 520
		private sealed class VisitorOfIEdmFunctionBase : InterfaceValidator.VisitorOfT<IEdmFunctionBase>
		{
			// Token: 0x06000C29 RID: 3113 RVA: 0x00023D84 File Offset: 0x00021F84
			protected override IEnumerable<EdmError> VisitT(IEdmFunctionBase function, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmFunctionBase, IEdmFunctionParameter>(function, function.Parameters, "Parameters", followup, ref list);
				if (function.ReturnType != null)
				{
					followup.Add(function.ReturnType);
				}
				return list;
			}
		}

		// Token: 0x02000209 RID: 521
		private sealed class VisitorOfIEdmFunction : InterfaceValidator.VisitorOfT<IEdmFunction>
		{
			// Token: 0x06000C2B RID: 3115 RVA: 0x00023DC4 File Offset: 0x00021FC4
			protected override IEnumerable<EdmError> VisitT(IEdmFunction function, List<object> followup, List<object> references)
			{
				if (function.ReturnType != null)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmFunction>(function, "ReturnType") };
			}
		}

		// Token: 0x0200020A RID: 522
		private sealed class VisitorOfIEdmFunctionImport : InterfaceValidator.VisitorOfT<IEdmFunctionImport>
		{
			// Token: 0x06000C2D RID: 3117 RVA: 0x00023DF9 File Offset: 0x00021FF9
			protected override IEnumerable<EdmError> VisitT(IEdmFunctionImport functionImport, List<object> followup, List<object> references)
			{
				if (functionImport.EntitySet != null)
				{
					followup.Add(functionImport.EntitySet);
				}
				return null;
			}
		}

		// Token: 0x0200020B RID: 523
		private sealed class VisitorOfIEdmFunctionParameter : InterfaceValidator.VisitorOfT<IEdmFunctionParameter>
		{
			// Token: 0x06000C2F RID: 3119 RVA: 0x00023E18 File Offset: 0x00022018
			protected override IEnumerable<EdmError> VisitT(IEdmFunctionParameter parameter, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (parameter.Type != null)
				{
					followup.Add(parameter.Type);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmFunctionParameter>(parameter, "Type"), ref list);
				}
				if (parameter.DeclaringFunction != null)
				{
					references.Add(parameter.DeclaringFunction);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmFunctionParameter>(parameter, "DeclaringFunction"), ref list);
				}
				if (parameter.Mode < EdmFunctionParameterMode.None || parameter.Mode > EdmFunctionParameterMode.InOut)
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreateEnumPropertyOutOfRangeError<IEdmFunctionParameter, EdmFunctionParameterMode>(parameter, parameter.Mode, "Mode"), ref list);
				}
				return list;
			}
		}

		// Token: 0x0200020C RID: 524
		private sealed class VisitorOfIEdmCollectionTypeReference : InterfaceValidator.VisitorOfT<IEdmCollectionTypeReference>
		{
			// Token: 0x06000C31 RID: 3121 RVA: 0x00023EAC File Offset: 0x000220AC
			protected override IEnumerable<EdmError> VisitT(IEdmCollectionTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Collection)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmCollectionTypeReference>(typeRef) };
			}
		}

		// Token: 0x0200020D RID: 525
		private sealed class VisitorOfIEdmEntityReferenceTypeReference : InterfaceValidator.VisitorOfT<IEdmEntityReferenceTypeReference>
		{
			// Token: 0x06000C33 RID: 3123 RVA: 0x00023EEC File Offset: 0x000220EC
			protected override IEnumerable<EdmError> VisitT(IEdmEntityReferenceTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.EntityReference)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmEntityReferenceTypeReference>(typeRef) };
			}
		}

		// Token: 0x0200020E RID: 526
		private sealed class VisitorOfIEdmStructuredTypeReference : InterfaceValidator.VisitorOfT<IEdmStructuredTypeReference>
		{
			// Token: 0x06000C35 RID: 3125 RVA: 0x00023F2C File Offset: 0x0002212C
			protected override IEnumerable<EdmError> VisitT(IEdmStructuredTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind.IsStructured())
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmStructuredTypeReference>(typeRef) };
			}
		}

		// Token: 0x0200020F RID: 527
		private sealed class VisitorOfIEdmEntityTypeReference : InterfaceValidator.VisitorOfT<IEdmEntityTypeReference>
		{
			// Token: 0x06000C37 RID: 3127 RVA: 0x00023F70 File Offset: 0x00022170
			protected override IEnumerable<EdmError> VisitT(IEdmEntityTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Entity)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmEntityTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000210 RID: 528
		private sealed class VisitorOfIEdmComplexTypeReference : InterfaceValidator.VisitorOfT<IEdmComplexTypeReference>
		{
			// Token: 0x06000C39 RID: 3129 RVA: 0x00023FB0 File Offset: 0x000221B0
			protected override IEnumerable<EdmError> VisitT(IEdmComplexTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Complex)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmComplexTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000211 RID: 529
		private sealed class VisitorOfIEdmRowTypeReference : InterfaceValidator.VisitorOfT<IEdmRowTypeReference>
		{
			// Token: 0x06000C3B RID: 3131 RVA: 0x00023FF0 File Offset: 0x000221F0
			protected override IEnumerable<EdmError> VisitT(IEdmRowTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Row)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmRowTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000212 RID: 530
		private sealed class VisitorOfIEdmEnumTypeReference : InterfaceValidator.VisitorOfT<IEdmEnumTypeReference>
		{
			// Token: 0x06000C3D RID: 3133 RVA: 0x00024030 File Offset: 0x00022230
			protected override IEnumerable<EdmError> VisitT(IEdmEnumTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Enum)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmEnumTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000213 RID: 531
		private sealed class VisitorOfIEdmPrimitiveTypeReference : InterfaceValidator.VisitorOfT<IEdmPrimitiveTypeReference>
		{
			// Token: 0x06000C3F RID: 3135 RVA: 0x00024070 File Offset: 0x00022270
			protected override IEnumerable<EdmError> VisitT(IEdmPrimitiveTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Primitive)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmPrimitiveTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000214 RID: 532
		private sealed class VisitorOfIEdmBinaryTypeReference : InterfaceValidator.VisitorOfT<IEdmBinaryTypeReference>
		{
			// Token: 0x06000C41 RID: 3137 RVA: 0x000240B0 File Offset: 0x000222B0
			protected override IEnumerable<EdmError> VisitT(IEdmBinaryTypeReference typeRef, List<object> followup, List<object> references)
			{
				IEdmPrimitiveType edmPrimitiveType = typeRef.Definition as IEdmPrimitiveType;
				if (edmPrimitiveType == null || edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.Binary)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePrimitiveTypeRefInterfaceTypeKindValueMismatchError<IEdmBinaryTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000215 RID: 533
		private sealed class VisitorOfIEdmDecimalTypeReference : InterfaceValidator.VisitorOfT<IEdmDecimalTypeReference>
		{
			// Token: 0x06000C43 RID: 3139 RVA: 0x000240F0 File Offset: 0x000222F0
			protected override IEnumerable<EdmError> VisitT(IEdmDecimalTypeReference typeRef, List<object> followup, List<object> references)
			{
				IEdmPrimitiveType edmPrimitiveType = typeRef.Definition as IEdmPrimitiveType;
				if (edmPrimitiveType == null || edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.Decimal)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePrimitiveTypeRefInterfaceTypeKindValueMismatchError<IEdmDecimalTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000216 RID: 534
		private sealed class VisitorOfIEdmStringTypeReference : InterfaceValidator.VisitorOfT<IEdmStringTypeReference>
		{
			// Token: 0x06000C45 RID: 3141 RVA: 0x00024130 File Offset: 0x00022330
			protected override IEnumerable<EdmError> VisitT(IEdmStringTypeReference typeRef, List<object> followup, List<object> references)
			{
				IEdmPrimitiveType edmPrimitiveType = typeRef.Definition as IEdmPrimitiveType;
				if (edmPrimitiveType == null || edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.String)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePrimitiveTypeRefInterfaceTypeKindValueMismatchError<IEdmStringTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000217 RID: 535
		private sealed class VisitorOfIEdmTemporalTypeReference : InterfaceValidator.VisitorOfT<IEdmTemporalTypeReference>
		{
			// Token: 0x06000C47 RID: 3143 RVA: 0x00024174 File Offset: 0x00022374
			protected override IEnumerable<EdmError> VisitT(IEdmTemporalTypeReference typeRef, List<object> followup, List<object> references)
			{
				IEdmPrimitiveType edmPrimitiveType = typeRef.Definition as IEdmPrimitiveType;
				if (edmPrimitiveType == null || edmPrimitiveType.PrimitiveKind.IsTemporal())
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePrimitiveTypeRefInterfaceTypeKindValueMismatchError<IEdmTemporalTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000218 RID: 536
		private sealed class VisitorOfIEdmSpatialTypeReference : InterfaceValidator.VisitorOfT<IEdmSpatialTypeReference>
		{
			// Token: 0x06000C49 RID: 3145 RVA: 0x000241B8 File Offset: 0x000223B8
			protected override IEnumerable<EdmError> VisitT(IEdmSpatialTypeReference typeRef, List<object> followup, List<object> references)
			{
				IEdmPrimitiveType edmPrimitiveType = typeRef.Definition as IEdmPrimitiveType;
				if (edmPrimitiveType == null || edmPrimitiveType.PrimitiveKind.IsSpatial())
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePrimitiveTypeRefInterfaceTypeKindValueMismatchError<IEdmSpatialTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000219 RID: 537
		private sealed class VisitorOfIEdmExpression : InterfaceValidator.VisitorOfT<IEdmExpression>
		{
			// Token: 0x06000C4B RID: 3147 RVA: 0x000241FC File Offset: 0x000223FC
			protected override IEnumerable<EdmError> VisitT(IEdmExpression expression, List<object> followup, List<object> references)
			{
				EdmError edmError = null;
				if (!InterfaceValidator.IsCheckableBad(expression))
				{
					switch (expression.ExpressionKind)
					{
					case EdmExpressionKind.BinaryConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmBinaryConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.BooleanConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmBooleanConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.DateTimeConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmDateTimeConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.DateTimeOffsetConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmDateTimeOffsetConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.DecimalConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmDecimalConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.FloatingConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmFloatingConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.GuidConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmGuidConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.IntegerConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmIntegerConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.StringConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmStringConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.TimeConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmTimeConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.Null:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmNullExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.Record:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmRecordExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.Collection:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmCollectionExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.Path:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmPathExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.ParameterReference:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmParameterReferenceExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.FunctionReference:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmFunctionReferenceExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.PropertyReference:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmPropertyReferenceExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.ValueTermReference:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmValueTermReferenceExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.EntitySetReference:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmEntitySetReferenceExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.EnumMemberReference:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmEnumMemberReferenceExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.If:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmIfExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.AssertType:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmAssertTypeExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.IsType:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmIsTypeExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.FunctionApplication:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmApplyExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.LabeledExpressionReference:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmLabeledExpressionReferenceExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.Labeled:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmLabeledExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					default:
						edmError = InterfaceValidator.CreateInterfaceKindValueUnexpectedError<IEdmExpression, EdmExpressionKind>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					}
				}
				if (edmError == null)
				{
					return null;
				}
				return new EdmError[] { edmError };
			}
		}

		// Token: 0x0200021A RID: 538
		private sealed class VisitorOfIEdmRecordExpression : InterfaceValidator.VisitorOfT<IEdmRecordExpression>
		{
			// Token: 0x06000C4D RID: 3149 RVA: 0x00024504 File Offset: 0x00022704
			protected override IEnumerable<EdmError> VisitT(IEdmRecordExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmRecordExpression, IEdmPropertyConstructor>(expression, expression.Properties, "Properties", followup, ref list);
				if (expression.DeclaredType != null)
				{
					followup.Add(expression.DeclaredType);
				}
				return list;
			}
		}

		// Token: 0x0200021B RID: 539
		private sealed class VisitorOfIEdmPropertyConstructor : InterfaceValidator.VisitorOfT<IEdmPropertyConstructor>
		{
			// Token: 0x06000C4F RID: 3151 RVA: 0x00024544 File Offset: 0x00022744
			protected override IEnumerable<EdmError> VisitT(IEdmPropertyConstructor expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.Name == null)
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyConstructor>(expression, "Name"), ref list);
				}
				if (expression.Value != null)
				{
					followup.Add(expression.Value);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyConstructor>(expression, "Value"), ref list);
				}
				return list;
			}
		}

		// Token: 0x0200021C RID: 540
		private sealed class VisitorOfIEdmCollectionExpression : InterfaceValidator.VisitorOfT<IEdmCollectionExpression>
		{
			// Token: 0x06000C51 RID: 3153 RVA: 0x000245A0 File Offset: 0x000227A0
			protected override IEnumerable<EdmError> VisitT(IEdmCollectionExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmCollectionExpression, IEdmExpression>(expression, expression.Elements, "Elements", followup, ref list);
				if (expression.DeclaredType != null)
				{
					followup.Add(expression.DeclaredType);
				}
				return list;
			}
		}

		// Token: 0x0200021D RID: 541
		private sealed class VisitorOfIEdmLabeledElement : InterfaceValidator.VisitorOfT<IEdmLabeledExpression>
		{
			// Token: 0x06000C53 RID: 3155 RVA: 0x000245E0 File Offset: 0x000227E0
			protected override IEnumerable<EdmError> VisitT(IEdmLabeledExpression expression, List<object> followup, List<object> references)
			{
				if (expression.Expression != null)
				{
					followup.Add(expression.Expression);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmLabeledExpression>(expression, "Expression") };
			}
		}

		// Token: 0x0200021E RID: 542
		private sealed class VisitorOfIEdmPathExpression : InterfaceValidator.VisitorOfT<IEdmPathExpression>
		{
			// Token: 0x06000C55 RID: 3157 RVA: 0x00024624 File Offset: 0x00022824
			protected override IEnumerable<EdmError> VisitT(IEdmPathExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				List<string> list2 = new List<string>();
				InterfaceValidator.ProcessEnumerable<IEdmPathExpression, string>(expression, expression.Path, "Path", list2, ref list);
				return list;
			}
		}

		// Token: 0x0200021F RID: 543
		private sealed class VisitorOfIEdmParameterReferenceExpression : InterfaceValidator.VisitorOfT<IEdmParameterReferenceExpression>
		{
			// Token: 0x06000C57 RID: 3159 RVA: 0x00024658 File Offset: 0x00022858
			protected override IEnumerable<EdmError> VisitT(IEdmParameterReferenceExpression expression, List<object> followup, List<object> references)
			{
				if (expression.ReferencedParameter != null)
				{
					references.Add(expression.ReferencedParameter);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmParameterReferenceExpression>(expression, "ReferencedParameter") };
			}
		}

		// Token: 0x02000220 RID: 544
		private sealed class VisitorOfIEdmFunctionReferenceExpression : InterfaceValidator.VisitorOfT<IEdmFunctionReferenceExpression>
		{
			// Token: 0x06000C59 RID: 3161 RVA: 0x0002469C File Offset: 0x0002289C
			protected override IEnumerable<EdmError> VisitT(IEdmFunctionReferenceExpression expression, List<object> followup, List<object> references)
			{
				if (expression.ReferencedFunction != null)
				{
					references.Add(expression.ReferencedFunction);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmFunctionReferenceExpression>(expression, "ReferencedFunction") };
			}
		}

		// Token: 0x02000221 RID: 545
		private sealed class VisitorOfIEdmPropertyReferenceExpression : InterfaceValidator.VisitorOfT<IEdmPropertyReferenceExpression>
		{
			// Token: 0x06000C5B RID: 3163 RVA: 0x000246E0 File Offset: 0x000228E0
			protected override IEnumerable<EdmError> VisitT(IEdmPropertyReferenceExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.Base != null)
				{
					followup.Add(expression.Base);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyReferenceExpression>(expression, "Base"), ref list);
				}
				if (expression.ReferencedProperty != null)
				{
					references.Add(expression.ReferencedProperty);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyReferenceExpression>(expression, "ReferencedProperty"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000222 RID: 546
		private sealed class VisitorOfIEdmValueTermReferenceExpression : InterfaceValidator.VisitorOfT<IEdmValueTermReferenceExpression>
		{
			// Token: 0x06000C5D RID: 3165 RVA: 0x00024748 File Offset: 0x00022948
			protected override IEnumerable<EdmError> VisitT(IEdmValueTermReferenceExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.Base != null)
				{
					followup.Add(expression.Base);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmValueTermReferenceExpression>(expression, "Base"), ref list);
				}
				if (expression.Term != null)
				{
					references.Add(expression.Term);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmValueTermReferenceExpression>(expression, "Term"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000223 RID: 547
		private sealed class VistorOfIEdmEntitySetReferenceExpression : InterfaceValidator.VisitorOfT<IEdmEntitySetReferenceExpression>
		{
			// Token: 0x06000C5F RID: 3167 RVA: 0x000247B0 File Offset: 0x000229B0
			protected override IEnumerable<EdmError> VisitT(IEdmEntitySetReferenceExpression expression, List<object> followup, List<object> references)
			{
				if (expression.ReferencedEntitySet != null)
				{
					references.Add(expression.ReferencedEntitySet);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEntitySetReferenceExpression>(expression, "ReferencedEntitySet") };
			}
		}

		// Token: 0x02000224 RID: 548
		private sealed class VistorOfIEdmEnumMemberReferenceExpression : InterfaceValidator.VisitorOfT<IEdmEnumMemberReferenceExpression>
		{
			// Token: 0x06000C61 RID: 3169 RVA: 0x000247F4 File Offset: 0x000229F4
			protected override IEnumerable<EdmError> VisitT(IEdmEnumMemberReferenceExpression expression, List<object> followup, List<object> references)
			{
				if (expression.ReferencedEnumMember != null)
				{
					references.Add(expression.ReferencedEnumMember);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEnumMemberReferenceExpression>(expression, "ReferencedEnumMember") };
			}
		}

		// Token: 0x02000225 RID: 549
		private sealed class VistorOfIEdmIfExpression : InterfaceValidator.VisitorOfT<IEdmIfExpression>
		{
			// Token: 0x06000C63 RID: 3171 RVA: 0x00024838 File Offset: 0x00022A38
			protected override IEnumerable<EdmError> VisitT(IEdmIfExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.TestExpression != null)
				{
					followup.Add(expression.TestExpression);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmIfExpression>(expression, "TestExpression"), ref list);
				}
				if (expression.TrueExpression != null)
				{
					followup.Add(expression.TrueExpression);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmIfExpression>(expression, "TrueExpression"), ref list);
				}
				if (expression.FalseExpression != null)
				{
					followup.Add(expression.FalseExpression);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmIfExpression>(expression, "FalseExpression"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000226 RID: 550
		private sealed class VistorOfIEdmAssertTypeExpression : InterfaceValidator.VisitorOfT<IEdmAssertTypeExpression>
		{
			// Token: 0x06000C65 RID: 3173 RVA: 0x000248C8 File Offset: 0x00022AC8
			protected override IEnumerable<EdmError> VisitT(IEdmAssertTypeExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.Operand != null)
				{
					followup.Add(expression.Operand);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmAssertTypeExpression>(expression, "Operand"), ref list);
				}
				if (expression.Type != null)
				{
					followup.Add(expression.Type);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmAssertTypeExpression>(expression, "Type"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000227 RID: 551
		private sealed class VistorOfIEdmIsTypeExpression : InterfaceValidator.VisitorOfT<IEdmIsTypeExpression>
		{
			// Token: 0x06000C67 RID: 3175 RVA: 0x00024930 File Offset: 0x00022B30
			protected override IEnumerable<EdmError> VisitT(IEdmIsTypeExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.Operand != null)
				{
					followup.Add(expression.Operand);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmIsTypeExpression>(expression, "Operand"), ref list);
				}
				if (expression.Type != null)
				{
					followup.Add(expression.Type);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmIsTypeExpression>(expression, "Type"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000228 RID: 552
		private sealed class VistorOfIEdmFunctionApplicationExpression : InterfaceValidator.VisitorOfT<IEdmApplyExpression>
		{
			// Token: 0x06000C69 RID: 3177 RVA: 0x00024998 File Offset: 0x00022B98
			protected override IEnumerable<EdmError> VisitT(IEdmApplyExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.AppliedFunction != null)
				{
					followup.Add(expression.AppliedFunction);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmApplyExpression>(expression, "AppliedFunction"), ref list);
				}
				InterfaceValidator.ProcessEnumerable<IEdmApplyExpression, IEdmExpression>(expression, expression.Arguments, "Arguments", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000229 RID: 553
		private sealed class VistorOfIEdmLabeledElementReferenceExpression : InterfaceValidator.VisitorOfT<IEdmLabeledExpressionReferenceExpression>
		{
			// Token: 0x06000C6B RID: 3179 RVA: 0x000249EC File Offset: 0x00022BEC
			protected override IEnumerable<EdmError> VisitT(IEdmLabeledExpressionReferenceExpression expression, List<object> followup, List<object> references)
			{
				if (expression.ReferencedLabeledExpression != null)
				{
					references.Add(expression.ReferencedLabeledExpression);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmLabeledExpressionReferenceExpression>(expression, "ReferencedLabeledExpression") };
			}
		}

		// Token: 0x0200022A RID: 554
		private sealed class VisitorOfIEdmValue : InterfaceValidator.VisitorOfT<IEdmValue>
		{
			// Token: 0x06000C6D RID: 3181 RVA: 0x00024A30 File Offset: 0x00022C30
			protected override IEnumerable<EdmError> VisitT(IEdmValue value, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (value.Type != null)
				{
					followup.Add(value.Type);
				}
				switch (value.ValueKind)
				{
				case EdmValueKind.Binary:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmBinaryValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Boolean:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmBooleanValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Collection:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmCollectionValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.DateTimeOffset:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmDateTimeOffsetValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.DateTime:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmDateTimeValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Decimal:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmDecimalValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Enum:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmEnumValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Floating:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmFloatingValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Guid:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmGuidValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Integer:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmIntegerValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Null:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmNullValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.String:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmStringValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Structured:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmStructuredValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Time:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmTimeValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.None:
					break;
				default:
					InterfaceValidator.CollectErrors(InterfaceValidator.CreateInterfaceKindValueUnexpectedError<IEdmValue, EdmValueKind>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				}
				return list;
			}
		}

		// Token: 0x0200022B RID: 555
		private sealed class VisitorOfIEdmDelayedValue : InterfaceValidator.VisitorOfT<IEdmDelayedValue>
		{
			// Token: 0x06000C6F RID: 3183 RVA: 0x00024C4C File Offset: 0x00022E4C
			protected override IEnumerable<EdmError> VisitT(IEdmDelayedValue value, List<object> followup, List<object> references)
			{
				if (value.Value != null)
				{
					followup.Add(value.Value);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmDelayedValue>(value, "Value") };
			}
		}

		// Token: 0x0200022C RID: 556
		private sealed class VisitorOfIEdmPropertyValue : InterfaceValidator.VisitorOfT<IEdmPropertyValue>
		{
			// Token: 0x06000C71 RID: 3185 RVA: 0x00024C90 File Offset: 0x00022E90
			protected override IEnumerable<EdmError> VisitT(IEdmPropertyValue value, List<object> followup, List<object> references)
			{
				if (value.Name != null)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyValue>(value, "Name") };
			}
		}

		// Token: 0x0200022D RID: 557
		private sealed class VisitorOfIEdmEnumValue : InterfaceValidator.VisitorOfT<IEdmEnumValue>
		{
			// Token: 0x06000C73 RID: 3187 RVA: 0x00024CC8 File Offset: 0x00022EC8
			protected override IEnumerable<EdmError> VisitT(IEdmEnumValue value, List<object> followup, List<object> references)
			{
				if (value.Value != null)
				{
					followup.Add(value.Value);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEnumValue>(value, "Value") };
			}
		}

		// Token: 0x0200022E RID: 558
		private sealed class VisitorOfIEdmCollectionValue : InterfaceValidator.VisitorOfT<IEdmCollectionValue>
		{
			// Token: 0x06000C75 RID: 3189 RVA: 0x00024D0C File Offset: 0x00022F0C
			protected override IEnumerable<EdmError> VisitT(IEdmCollectionValue value, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmCollectionValue, IEdmDelayedValue>(value, value.Elements, "Elements", followup, ref list);
				return list;
			}
		}

		// Token: 0x0200022F RID: 559
		private sealed class VisitorOfIEdmStructuredValue : InterfaceValidator.VisitorOfT<IEdmStructuredValue>
		{
			// Token: 0x06000C77 RID: 3191 RVA: 0x00024D38 File Offset: 0x00022F38
			protected override IEnumerable<EdmError> VisitT(IEdmStructuredValue value, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmStructuredValue, IEdmPropertyValue>(value, value.PropertyValues, "PropertyValues", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000230 RID: 560
		private sealed class VisitorOfIEdmBinaryValue : InterfaceValidator.VisitorOfT<IEdmBinaryValue>
		{
			// Token: 0x06000C79 RID: 3193 RVA: 0x00024D64 File Offset: 0x00022F64
			protected override IEnumerable<EdmError> VisitT(IEdmBinaryValue value, List<object> followup, List<object> references)
			{
				if (value.Value != null)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmBinaryValue>(value, "Value") };
			}
		}

		// Token: 0x02000231 RID: 561
		private sealed class VisitorOfIEdmStringValue : InterfaceValidator.VisitorOfT<IEdmStringValue>
		{
			// Token: 0x06000C7B RID: 3195 RVA: 0x00024D9C File Offset: 0x00022F9C
			protected override IEnumerable<EdmError> VisitT(IEdmStringValue value, List<object> followup, List<object> references)
			{
				if (value.Value != null)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmStringValue>(value, "Value") };
			}
		}

		// Token: 0x02000232 RID: 562
		private sealed class VisitorOfIEdmVocabularyAnnotation : InterfaceValidator.VisitorOfT<IEdmVocabularyAnnotation>
		{
			// Token: 0x06000C7D RID: 3197 RVA: 0x00024DD4 File Offset: 0x00022FD4
			protected override IEnumerable<EdmError> VisitT(IEdmVocabularyAnnotation annotation, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (annotation.Term != null)
				{
					references.Add(annotation.Term);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmVocabularyAnnotation>(annotation, "Term"), ref list);
				}
				if (annotation.Target != null)
				{
					references.Add(annotation.Target);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmVocabularyAnnotation>(annotation, "Target"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000233 RID: 563
		private sealed class VisitorOfIEdmValueAnnotation : InterfaceValidator.VisitorOfT<IEdmValueAnnotation>
		{
			// Token: 0x06000C7F RID: 3199 RVA: 0x00024E3C File Offset: 0x0002303C
			protected override IEnumerable<EdmError> VisitT(IEdmValueAnnotation annotation, List<object> followup, List<object> references)
			{
				if (annotation.Value != null)
				{
					followup.Add(annotation.Value);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmValueAnnotation>(annotation, "Value") };
			}
		}

		// Token: 0x02000234 RID: 564
		private sealed class VisitorOfIEdmTypeAnnotation : InterfaceValidator.VisitorOfT<IEdmTypeAnnotation>
		{
			// Token: 0x06000C81 RID: 3201 RVA: 0x00024E80 File Offset: 0x00023080
			protected override IEnumerable<EdmError> VisitT(IEdmTypeAnnotation annotation, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmTypeAnnotation, IEdmPropertyValueBinding>(annotation, annotation.PropertyValueBindings, "PropertyValueBindings", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000235 RID: 565
		private sealed class VisitorOfIEdmPropertyValueBinding : InterfaceValidator.VisitorOfT<IEdmPropertyValueBinding>
		{
			// Token: 0x06000C83 RID: 3203 RVA: 0x00024EAC File Offset: 0x000230AC
			protected override IEnumerable<EdmError> VisitT(IEdmPropertyValueBinding binding, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (binding.Value != null)
				{
					followup.Add(binding.Value);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyValueBinding>(binding, "Value"), ref list);
				}
				if (binding.BoundProperty != null)
				{
					references.Add(binding.BoundProperty);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyValueBinding>(binding, "BoundProperty"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000236 RID: 566
		private sealed class VisitorOfIEdmDirectValueAnnotation : InterfaceValidator.VisitorOfT<IEdmDirectValueAnnotation>
		{
			// Token: 0x06000C85 RID: 3205 RVA: 0x00024F14 File Offset: 0x00023114
			protected override IEnumerable<EdmError> VisitT(IEdmDirectValueAnnotation annotation, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (annotation.NamespaceUri == null)
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmDirectValueAnnotation>(annotation, "NamespaceUri"), ref list);
				}
				if (annotation.Value == null)
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmDirectValueAnnotation>(annotation, "Value"), ref list);
				}
				return list;
			}
		}
	}
}
