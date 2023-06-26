using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Csdl.Internal.Serialization
{
	// Token: 0x020001B0 RID: 432
	internal class EdmModelCsdlSchemaWriter
	{
		// Token: 0x06000988 RID: 2440 RVA: 0x0001A115 File Offset: 0x00018315
		internal EdmModelCsdlSchemaWriter(IEdmModel model, VersioningDictionary<string, string> namespaceAliasMappings, XmlWriter xmlWriter, Version edmVersion)
		{
			this.xmlWriter = xmlWriter;
			this.version = edmVersion;
			this.model = model;
			this.namespaceAliasMappings = namespaceAliasMappings;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0001A13C File Offset: 0x0001833C
		internal void WriteValueTermElementHeader(IEdmValueTerm term, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("ValueTerm");
			this.WriteRequiredAttribute<string>("Name", term.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (inlineType && term.Type != null)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", term.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001A19E File Offset: 0x0001839E
		internal void WriteAssociationElementHeader(IEdmNavigationProperty navigationProperty)
		{
			this.xmlWriter.WriteStartElement("Association");
			this.WriteRequiredAttribute<string>("Name", this.model.GetAssociationName(navigationProperty), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001A1D4 File Offset: 0x000183D4
		internal void WriteAssociationSetElementHeader(IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.xmlWriter.WriteStartElement("AssociationSet");
			this.WriteRequiredAttribute<string>("Name", this.model.GetAssociationSetName(entitySet, navigationProperty), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Association", this.model.GetAssociationFullName(navigationProperty), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001A238 File Offset: 0x00018438
		internal void WriteComplexTypeElementHeader(IEdmComplexType complexType)
		{
			this.xmlWriter.WriteStartElement("ComplexType");
			this.WriteRequiredAttribute<string>("Name", complexType.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<IEdmComplexType>("BaseType", complexType.BaseComplexType(), new Func<IEdmComplexType, string>(this.TypeDefinitionAsXml));
			this.WriteOptionalAttribute<bool>("Abstract", complexType.IsAbstract, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001A2B0 File Offset: 0x000184B0
		internal void WriteEnumTypeElementHeader(IEdmEnumType enumType)
		{
			this.xmlWriter.WriteStartElement("EnumType");
			this.WriteRequiredAttribute<string>("Name", enumType.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (enumType.UnderlyingType.PrimitiveKind != EdmPrimitiveTypeKind.Int32)
			{
				this.WriteRequiredAttribute<IEdmPrimitiveType>("UnderlyingType", enumType.UnderlyingType, new Func<IEdmPrimitiveType, string>(this.TypeDefinitionAsXml));
			}
			this.WriteOptionalAttribute<bool>("IsFlags", enumType.IsFlags, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001A334 File Offset: 0x00018534
		internal void WriteDocumentationElement(IEdmDocumentation documentation)
		{
			this.xmlWriter.WriteStartElement("Documentation");
			if (documentation.Summary != null)
			{
				this.xmlWriter.WriteStartElement("Summary");
				this.xmlWriter.WriteString(documentation.Summary);
				this.WriteEndElement();
			}
			if (documentation.Description != null)
			{
				this.xmlWriter.WriteStartElement("LongDescription");
				this.xmlWriter.WriteString(documentation.Description);
				this.WriteEndElement();
			}
			this.WriteEndElement();
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001A3B8 File Offset: 0x000185B8
		internal void WriteAssociationSetEndElementHeader(IEdmEntitySet entitySet, IEdmNavigationProperty property)
		{
			this.xmlWriter.WriteStartElement("End");
			this.WriteRequiredAttribute<string>("Role", this.model.GetAssociationEndName(property), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("EntitySet", entitySet.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001A418 File Offset: 0x00018618
		internal void WriteAssociationEndElementHeader(IEdmNavigationProperty associationEnd)
		{
			this.xmlWriter.WriteStartElement("End");
			this.WriteRequiredAttribute<string>("Type", ((IEdmEntityType)associationEnd.DeclaringType).FullName(), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Role", this.model.GetAssociationEndName(associationEnd), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<EdmMultiplicity>("Multiplicity", associationEnd.Multiplicity(), new Func<EdmMultiplicity, string>(EdmModelCsdlSchemaWriter.MultiplicityAsXml));
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001A49C File Offset: 0x0001869C
		internal void WriteEntityContainerElementHeader(IEdmEntityContainer container)
		{
			this.xmlWriter.WriteStartElement("EntityContainer");
			this.WriteRequiredAttribute<string>("Name", container.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001A4CC File Offset: 0x000186CC
		internal void WriteEntitySetElementHeader(IEdmEntitySet entitySet)
		{
			this.xmlWriter.WriteStartElement("EntitySet");
			this.WriteRequiredAttribute<string>("Name", entitySet.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("EntityType", entitySet.ElementType.FullName(), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001A528 File Offset: 0x00018728
		internal void WriteEntityTypeElementHeader(IEdmEntityType entityType)
		{
			this.xmlWriter.WriteStartElement("EntityType");
			this.WriteRequiredAttribute<string>("Name", entityType.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<IEdmEntityType>("BaseType", entityType.BaseEntityType(), new Func<IEdmEntityType, string>(this.TypeDefinitionAsXml));
			this.WriteOptionalAttribute<bool>("Abstract", entityType.IsAbstract, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			this.WriteOptionalAttribute<bool>("OpenType", entityType.IsOpen, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001A5BB File Offset: 0x000187BB
		internal void WriteDelaredKeyPropertiesElementHeader()
		{
			this.xmlWriter.WriteStartElement("Key");
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001A5CD File Offset: 0x000187CD
		internal void WritePropertyRefElement(IEdmStructuralProperty property)
		{
			this.xmlWriter.WriteStartElement("PropertyRef");
			this.WriteRequiredAttribute<string>("Name", property.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001A604 File Offset: 0x00018804
		internal void WriteNavigationPropertyElementHeader(IEdmNavigationProperty member)
		{
			this.xmlWriter.WriteStartElement("NavigationProperty");
			this.WriteRequiredAttribute<string>("Name", member.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Relationship", this.model.GetAssociationFullName(member), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("ToRole", this.model.GetAssociationEndName(member.Partner), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("FromRole", this.model.GetAssociationEndName(member), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<bool>("ContainsTarget", member.ContainsTarget, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001A6CA File Offset: 0x000188CA
		internal void WriteOperationActionElement(string elementName, EdmOnDeleteAction operationAction)
		{
			this.xmlWriter.WriteStartElement(elementName);
			this.WriteRequiredAttribute<string>("Action", operationAction.ToString(), new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001A700 File Offset: 0x00018900
		internal void WriteSchemaElementHeader(EdmSchema schema, string alias, IEnumerable<KeyValuePair<string, string>> mappings)
		{
			string csdlNamespace = EdmModelCsdlSchemaWriter.GetCsdlNamespace(this.version);
			this.xmlWriter.WriteStartElement("Schema", csdlNamespace);
			this.WriteOptionalAttribute<string>("Namespace", schema.Namespace, string.Empty, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<string>("Alias", alias, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (mappings != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in mappings)
				{
					this.xmlWriter.WriteAttributeString("xmlns", keyValuePair.Key, null, keyValuePair.Value);
				}
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0001A7BC File Offset: 0x000189BC
		internal void WriteAnnotationsElementHeader(string annotationsTarget)
		{
			this.xmlWriter.WriteStartElement("Annotations");
			this.WriteRequiredAttribute<string>("Target", annotationsTarget, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001A7E8 File Offset: 0x000189E8
		internal void WriteStructuralPropertyElementHeader(IEdmStructuralProperty property, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("Property");
			this.WriteRequiredAttribute<string>("Name", property.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", property.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
			this.WriteOptionalAttribute<EdmConcurrencyMode>("ConcurrencyMode", property.ConcurrencyMode, EdmConcurrencyMode.None, new Func<EdmConcurrencyMode, string>(EdmModelCsdlSchemaWriter.ConcurrencyModeAsXml));
			this.WriteOptionalAttribute<string>("DefaultValue", property.DefaultValueString, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001A880 File Offset: 0x00018A80
		internal void WriteEnumMemberElementHeader(IEdmEnumMember member)
		{
			this.xmlWriter.WriteStartElement("Member");
			this.WriteRequiredAttribute<string>("Name", member.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			bool? flag = member.IsValueExplicit(this.model);
			if (flag == null || flag.Value)
			{
				this.WriteRequiredAttribute<IEdmPrimitiveValue>("Value", member.Value, new Func<IEdmPrimitiveValue, string>(EdmValueWriter.PrimitiveValueAsXml));
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001A8F6 File Offset: 0x00018AF6
		internal void WriteNullableAttribute(IEdmTypeReference reference)
		{
			this.WriteOptionalAttribute<bool>("Nullable", reference.IsNullable, true, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001A918 File Offset: 0x00018B18
		internal void WriteBinaryTypeAttributes(IEdmBinaryTypeReference reference)
		{
			if (reference.IsUnbounded)
			{
				this.WriteRequiredAttribute<string>("MaxLength", "Max", new Func<string, string>(EdmValueWriter.StringAsXml));
			}
			else
			{
				this.WriteOptionalAttribute<int?>("MaxLength", reference.MaxLength, new Func<int?, string>(EdmValueWriter.IntAsXml));
			}
			this.WriteOptionalAttribute<bool?>("FixedLength", reference.IsFixedLength, new Func<bool?, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0001A985 File Offset: 0x00018B85
		internal void WriteDecimalTypeAttributes(IEdmDecimalTypeReference reference)
		{
			this.WriteOptionalAttribute<int?>("Precision", reference.Precision, new Func<int?, string>(EdmValueWriter.IntAsXml));
			this.WriteOptionalAttribute<int?>("Scale", reference.Scale, new Func<int?, string>(EdmValueWriter.IntAsXml));
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001A9C1 File Offset: 0x00018BC1
		internal void WriteSpatialTypeAttributes(IEdmSpatialTypeReference reference)
		{
			this.WriteRequiredAttribute<int?>("SRID", reference.SpatialReferenceIdentifier, new Func<int?, string>(EdmModelCsdlSchemaWriter.SridAsXml));
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001A9E0 File Offset: 0x00018BE0
		internal void WriteStringTypeAttributes(IEdmStringTypeReference reference)
		{
			this.WriteOptionalAttribute<string>("Collation", reference.Collation, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (reference.IsUnbounded)
			{
				this.WriteRequiredAttribute<string>("MaxLength", "Max", new Func<string, string>(EdmValueWriter.StringAsXml));
			}
			else
			{
				this.WriteOptionalAttribute<int?>("MaxLength", reference.MaxLength, new Func<int?, string>(EdmValueWriter.IntAsXml));
			}
			this.WriteOptionalAttribute<bool?>("FixedLength", reference.IsFixedLength, new Func<bool?, string>(EdmValueWriter.BooleanAsXml));
			this.WriteOptionalAttribute<bool?>("Unicode", reference.IsUnicode, new Func<bool?, string>(EdmValueWriter.BooleanAsXml));
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001AA87 File Offset: 0x00018C87
		internal void WriteTemporalTypeAttributes(IEdmTemporalTypeReference reference)
		{
			this.WriteOptionalAttribute<int?>("Precision", reference.Precision, new Func<int?, string>(EdmValueWriter.IntAsXml));
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001AAA6 File Offset: 0x00018CA6
		internal void WriteReferentialConstraintElementHeader(IEdmNavigationProperty constraint)
		{
			this.xmlWriter.WriteStartElement("ReferentialConstraint");
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001AAB8 File Offset: 0x00018CB8
		internal void WriteReferentialConstraintPrincipalEndElementHeader(IEdmNavigationProperty end)
		{
			this.xmlWriter.WriteStartElement("Principal");
			this.WriteRequiredAttribute<string>("Role", this.model.GetAssociationEndName(end), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001AAED File Offset: 0x00018CED
		internal void WriteReferentialConstraintDependentEndElementHeader(IEdmNavigationProperty end)
		{
			this.xmlWriter.WriteStartElement("Dependent");
			this.WriteRequiredAttribute<string>("Role", this.model.GetAssociationEndName(end), new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0001AB24 File Offset: 0x00018D24
		internal void WriteNamespaceUsingElement(string usingNamespace, string alias)
		{
			this.xmlWriter.WriteStartElement("Using");
			this.WriteRequiredAttribute<string>("Namespace", usingNamespace, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteRequiredAttribute<string>("Alias", alias, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteEndElement();
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0001AB78 File Offset: 0x00018D78
		internal void WriteAnnotationStringAttribute(IEdmDirectValueAnnotation annotation)
		{
			IEdmPrimitiveValue edmPrimitiveValue = (IEdmPrimitiveValue)annotation.Value;
			if (edmPrimitiveValue != null)
			{
				this.xmlWriter.WriteAttributeString(annotation.Name, annotation.NamespaceUri, EdmValueWriter.PrimitiveValueAsXml(edmPrimitiveValue));
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001ABB4 File Offset: 0x00018DB4
		internal void WriteAnnotationStringElement(IEdmDirectValueAnnotation annotation)
		{
			IEdmPrimitiveValue edmPrimitiveValue = (IEdmPrimitiveValue)annotation.Value;
			if (edmPrimitiveValue != null)
			{
				this.xmlWriter.WriteRaw(((IEdmStringValue)edmPrimitiveValue).Value);
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0001ABE8 File Offset: 0x00018DE8
		internal void WriteFunctionElementHeader(IEdmFunction function, bool inlineReturnType)
		{
			this.xmlWriter.WriteStartElement("Function");
			this.WriteRequiredAttribute<string>("Name", function.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (inlineReturnType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("ReturnType", function.ReturnType, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001AC42 File Offset: 0x00018E42
		internal void WriteDefiningExpressionElement(string expression)
		{
			this.xmlWriter.WriteStartElement("DefiningExpression");
			this.xmlWriter.WriteString(expression);
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001AC6B File Offset: 0x00018E6B
		internal void WriteReturnTypeElementHeader()
		{
			this.xmlWriter.WriteStartElement("ReturnType");
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001AC80 File Offset: 0x00018E80
		internal void WriteFunctionImportElementHeader(IEdmFunctionImport functionImport)
		{
			if (functionImport.IsComposable && functionImport.IsSideEffecting)
			{
				throw new InvalidOperationException(Strings.EdmModel_Validator_Semantic_ComposableFunctionImportCannotBeSideEffecting(functionImport.Name));
			}
			this.xmlWriter.WriteStartElement("FunctionImport");
			this.WriteRequiredAttribute<string>("Name", functionImport.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			this.WriteOptionalAttribute<IEdmTypeReference>("ReturnType", functionImport.ReturnType, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			if (!functionImport.IsComposable && !functionImport.IsSideEffecting)
			{
				this.WriteRequiredAttribute<bool>("IsSideEffecting", functionImport.IsSideEffecting, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			}
			this.WriteOptionalAttribute<bool>("IsComposable", functionImport.IsComposable, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			this.WriteOptionalAttribute<bool>("IsBindable", functionImport.IsBindable, false, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
			if (functionImport.EntitySet == null)
			{
				return;
			}
			IEdmEntitySetReferenceExpression edmEntitySetReferenceExpression = functionImport.EntitySet as IEdmEntitySetReferenceExpression;
			if (edmEntitySetReferenceExpression != null)
			{
				this.WriteOptionalAttribute<string>("EntitySet", edmEntitySetReferenceExpression.ReferencedEntitySet.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
				return;
			}
			IEdmPathExpression edmPathExpression = functionImport.EntitySet as IEdmPathExpression;
			if (edmPathExpression != null)
			{
				this.WriteOptionalAttribute<IEnumerable<string>>("EntitySetPath", edmPathExpression.Path, new Func<IEnumerable<string>, string>(EdmModelCsdlSchemaWriter.PathAsXml));
				return;
			}
			throw new InvalidOperationException(Strings.EdmModel_Validator_Semantic_FunctionImportEntitySetExpressionIsInvalid(functionImport.Name));
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001ADDC File Offset: 0x00018FDC
		internal void WriteFunctionParameterElementHeader(IEdmFunctionParameter parameter, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("Parameter");
			this.WriteRequiredAttribute<string>("Name", parameter.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", parameter.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
			this.WriteOptionalAttribute<EdmFunctionParameterMode>("Mode", parameter.Mode, EdmFunctionParameterMode.In, new Func<EdmFunctionParameterMode, string>(EdmModelCsdlSchemaWriter.FunctionParameterModeAsXml));
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001AE54 File Offset: 0x00019054
		internal void WriteCollectionTypeElementHeader(IEdmCollectionType collectionType, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("CollectionType");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("ElementType", collectionType.ElementType, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001AE86 File Offset: 0x00019086
		internal void WriteRowTypeElementHeader()
		{
			this.xmlWriter.WriteStartElement("RowType");
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0001AE98 File Offset: 0x00019098
		internal void WriteInlineExpression(IEdmExpression expression)
		{
			switch (expression.ExpressionKind)
			{
			case EdmExpressionKind.BinaryConstant:
				this.WriteRequiredAttribute<byte[]>("Binary", ((IEdmBinaryConstantExpression)expression).Value, new Func<byte[], string>(EdmValueWriter.BinaryAsXml));
				return;
			case EdmExpressionKind.BooleanConstant:
				this.WriteRequiredAttribute<bool>("Bool", ((IEdmBooleanConstantExpression)expression).Value, new Func<bool, string>(EdmValueWriter.BooleanAsXml));
				return;
			case EdmExpressionKind.DateTimeConstant:
				this.WriteRequiredAttribute<DateTime>("DateTime", ((IEdmDateTimeConstantExpression)expression).Value, new Func<DateTime, string>(EdmValueWriter.DateTimeAsXml));
				return;
			case EdmExpressionKind.DateTimeOffsetConstant:
				this.WriteRequiredAttribute<DateTimeOffset>("DateTimeOffset", ((IEdmDateTimeOffsetConstantExpression)expression).Value, new Func<DateTimeOffset, string>(EdmValueWriter.DateTimeOffsetAsXml));
				return;
			case EdmExpressionKind.DecimalConstant:
				this.WriteRequiredAttribute<decimal>("Decimal", ((IEdmDecimalConstantExpression)expression).Value, new Func<decimal, string>(EdmValueWriter.DecimalAsXml));
				return;
			case EdmExpressionKind.FloatingConstant:
				this.WriteRequiredAttribute<double>("Float", ((IEdmFloatingConstantExpression)expression).Value, new Func<double, string>(EdmValueWriter.FloatAsXml));
				return;
			case EdmExpressionKind.GuidConstant:
				this.WriteRequiredAttribute<Guid>("Guid", ((IEdmGuidConstantExpression)expression).Value, new Func<Guid, string>(EdmValueWriter.GuidAsXml));
				return;
			case EdmExpressionKind.IntegerConstant:
				this.WriteRequiredAttribute<long>("Int", ((IEdmIntegerConstantExpression)expression).Value, new Func<long, string>(EdmValueWriter.LongAsXml));
				return;
			case EdmExpressionKind.StringConstant:
				this.WriteRequiredAttribute<string>("String", ((IEdmStringConstantExpression)expression).Value, new Func<string, string>(EdmValueWriter.StringAsXml));
				return;
			case EdmExpressionKind.TimeConstant:
				this.WriteRequiredAttribute<TimeSpan>("Time", ((IEdmTimeConstantExpression)expression).Value, new Func<TimeSpan, string>(EdmValueWriter.TimeAsXml));
				break;
			case EdmExpressionKind.Null:
			case EdmExpressionKind.Record:
			case EdmExpressionKind.Collection:
				break;
			case EdmExpressionKind.Path:
				this.WriteRequiredAttribute<IEnumerable<string>>("Path", ((IEdmPathExpression)expression).Path, new Func<IEnumerable<string>, string>(EdmModelCsdlSchemaWriter.PathAsXml));
				return;
			default:
				return;
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0001B070 File Offset: 0x00019270
		internal void WriteValueAnnotationElementHeader(IEdmValueAnnotation annotation, bool isInline)
		{
			this.xmlWriter.WriteStartElement("ValueAnnotation");
			this.WriteRequiredAttribute<IEdmTerm>("Term", annotation.Term, new Func<IEdmTerm, string>(this.TermAsXml));
			this.WriteOptionalAttribute<string>("Qualifier", annotation.Qualifier, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (isInline)
			{
				this.WriteInlineExpression(annotation.Value);
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0001B0D8 File Offset: 0x000192D8
		internal void WriteTypeAnnotationElementHeader(IEdmTypeAnnotation annotation)
		{
			this.xmlWriter.WriteStartElement("TypeAnnotation");
			this.WriteRequiredAttribute<IEdmTerm>("Term", annotation.Term, new Func<IEdmTerm, string>(this.TermAsXml));
			this.WriteOptionalAttribute<string>("Qualifier", annotation.Qualifier, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001B130 File Offset: 0x00019330
		internal void WritePropertyValueElementHeader(IEdmPropertyValueBinding value, bool isInline)
		{
			this.xmlWriter.WriteStartElement("PropertyValue");
			this.WriteRequiredAttribute<string>("Property", value.BoundProperty.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (isInline)
			{
				this.WriteInlineExpression(value.Value);
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001B17E File Offset: 0x0001937E
		internal void WriteRecordExpressionElementHeader(IEdmRecordExpression expression)
		{
			this.xmlWriter.WriteStartElement("Record");
			this.WriteOptionalAttribute<IEdmStructuredTypeReference>("Type", expression.DeclaredType, new Func<IEdmStructuredTypeReference, string>(this.TypeReferenceAsXml));
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001B1AD File Offset: 0x000193AD
		internal void WritePropertyConstructorElementHeader(IEdmPropertyConstructor constructor, bool isInline)
		{
			this.xmlWriter.WriteStartElement("PropertyValue");
			this.WriteRequiredAttribute<string>("Property", constructor.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
			if (isInline)
			{
				this.WriteInlineExpression(constructor.Value);
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001B1EB File Offset: 0x000193EB
		internal void WriteStringConstantExpressionElement(IEdmStringConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("String");
			this.xmlWriter.WriteString(EdmValueWriter.StringAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0001B219 File Offset: 0x00019419
		internal void WriteBinaryConstantExpressionElement(IEdmBinaryConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("String");
			this.xmlWriter.WriteString(EdmValueWriter.BinaryAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001B247 File Offset: 0x00019447
		internal void WriteBooleanConstantExpressionElement(IEdmBooleanConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Bool");
			this.xmlWriter.WriteString(EdmValueWriter.BooleanAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001B275 File Offset: 0x00019475
		internal void WriteNullConstantExpressionElement(IEdmNullExpression expression)
		{
			this.xmlWriter.WriteStartElement("Null");
			this.WriteEndElement();
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0001B28D File Offset: 0x0001948D
		internal void WriteDateTimeConstantExpressionElement(IEdmDateTimeConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("DateTime");
			this.xmlWriter.WriteString(EdmValueWriter.DateTimeAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001B2BB File Offset: 0x000194BB
		internal void WriteDateTimeOffsetConstantExpressionElement(IEdmDateTimeOffsetConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("DateTimeOffset");
			this.xmlWriter.WriteString(EdmValueWriter.DateTimeOffsetAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001B2E9 File Offset: 0x000194E9
		internal void WriteDecimalConstantExpressionElement(IEdmDecimalConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Decimal");
			this.xmlWriter.WriteString(EdmValueWriter.DecimalAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001B317 File Offset: 0x00019517
		internal void WriteFloatingConstantExpressionElement(IEdmFloatingConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Float");
			this.xmlWriter.WriteString(EdmValueWriter.FloatAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001B345 File Offset: 0x00019545
		internal void WriteFunctionApplicationElementHeader(IEdmApplyExpression expression, bool isFunction)
		{
			this.xmlWriter.WriteStartElement("Apply");
			if (isFunction)
			{
				this.WriteRequiredAttribute<IEdmFunction>("Function", ((IEdmFunctionReferenceExpression)expression.AppliedFunction).ReferencedFunction, new Func<IEdmFunction, string>(this.FunctionAsXml));
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001B381 File Offset: 0x00019581
		internal void WriteGuidConstantExpressionElement(IEdmGuidConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Guid");
			this.xmlWriter.WriteString(EdmValueWriter.GuidAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001B3AF File Offset: 0x000195AF
		internal void WriteIntegerConstantExpressionElement(IEdmIntegerConstantExpression expression)
		{
			this.xmlWriter.WriteStartElement("Int");
			this.xmlWriter.WriteString(EdmValueWriter.LongAsXml(expression.Value));
			this.WriteEndElement();
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0001B3DD File Offset: 0x000195DD
		internal void WritePathExpressionElement(IEdmPathExpression expression)
		{
			this.xmlWriter.WriteStartElement("Path");
			this.xmlWriter.WriteString(EdmModelCsdlSchemaWriter.PathAsXml(expression.Path));
			this.WriteEndElement();
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001B40B File Offset: 0x0001960B
		internal void WriteIfExpressionElementHeader(IEdmIfExpression expression)
		{
			this.xmlWriter.WriteStartElement("If");
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001B41D File Offset: 0x0001961D
		internal void WriteCollectionExpressionElementHeader(IEdmCollectionExpression expression)
		{
			this.xmlWriter.WriteStartElement("Collection");
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0001B42F File Offset: 0x0001962F
		internal void WriteLabeledElementHeader(IEdmLabeledExpression labeledElement)
		{
			this.xmlWriter.WriteStartElement("LabeledElement");
			this.WriteRequiredAttribute<string>("Name", labeledElement.Name, new Func<string, string>(EdmValueWriter.StringAsXml));
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0001B45E File Offset: 0x0001965E
		internal void WriteIsTypeExpressionElementHeader(IEdmIsTypeExpression expression, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("IsType");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", expression.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0001B490 File Offset: 0x00019690
		internal void WriteAssertTypeExpressionElementHeader(IEdmAssertTypeExpression expression, bool inlineType)
		{
			this.xmlWriter.WriteStartElement("AssertType");
			if (inlineType)
			{
				this.WriteRequiredAttribute<IEdmTypeReference>("Type", expression.Type, new Func<IEdmTypeReference, string>(this.TypeReferenceAsXml));
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0001B4C2 File Offset: 0x000196C2
		internal void WriteEntitySetReferenceExpressionElement(IEdmEntitySetReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("EntitySetReference");
			this.WriteRequiredAttribute<IEdmEntitySet>("Name", expression.ReferencedEntitySet, new Func<IEdmEntitySet, string>(EdmModelCsdlSchemaWriter.EntitySetAsXml));
			this.WriteEndElement();
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0001B4F7 File Offset: 0x000196F7
		internal void WriteParameterReferenceExpressionElement(IEdmParameterReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("ParameterReference");
			this.WriteRequiredAttribute<IEdmFunctionParameter>("Name", expression.ReferencedParameter, new Func<IEdmFunctionParameter, string>(EdmModelCsdlSchemaWriter.ParameterAsXml));
			this.WriteEndElement();
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001B52C File Offset: 0x0001972C
		internal void WriteFunctionReferenceExpressionElement(IEdmFunctionReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("FunctionReference");
			this.WriteRequiredAttribute<IEdmFunction>("Name", expression.ReferencedFunction, new Func<IEdmFunction, string>(this.FunctionAsXml));
			this.WriteEndElement();
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0001B561 File Offset: 0x00019761
		internal void WriteEnumMemberReferenceExpressionElement(IEdmEnumMemberReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("EnumMemberReference");
			this.WriteRequiredAttribute<IEdmEnumMember>("Name", expression.ReferencedEnumMember, new Func<IEdmEnumMember, string>(EdmModelCsdlSchemaWriter.EnumMemberAsXml));
			this.WriteEndElement();
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0001B596 File Offset: 0x00019796
		internal void WritePropertyReferenceExpressionElementHeader(IEdmPropertyReferenceExpression expression)
		{
			this.xmlWriter.WriteStartElement("PropertyReference");
			this.WriteRequiredAttribute<IEdmProperty>("Name", expression.ReferencedProperty, new Func<IEdmProperty, string>(EdmModelCsdlSchemaWriter.PropertyAsXml));
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0001B5C5 File Offset: 0x000197C5
		internal void WriteEndElement()
		{
			this.xmlWriter.WriteEndElement();
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0001B5D2 File Offset: 0x000197D2
		internal void WriteOptionalAttribute<T>(string attribute, T value, T defaultValue, Func<T, string> toXml)
		{
			if (!value.Equals(defaultValue))
			{
				this.xmlWriter.WriteAttributeString(attribute, toXml(value));
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0001B5FD File Offset: 0x000197FD
		internal void WriteOptionalAttribute<T>(string attribute, T value, Func<T, string> toXml)
		{
			if (value != null)
			{
				this.xmlWriter.WriteAttributeString(attribute, toXml(value));
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0001B61A File Offset: 0x0001981A
		internal void WriteRequiredAttribute<T>(string attribute, T value, Func<T, string> toXml)
		{
			this.xmlWriter.WriteAttributeString(attribute, toXml(value));
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0001B630 File Offset: 0x00019830
		private static string MultiplicityAsXml(EdmMultiplicity endKind)
		{
			switch (endKind)
			{
			case EdmMultiplicity.ZeroOrOne:
				return "0..1";
			case EdmMultiplicity.One:
				return "1";
			case EdmMultiplicity.Many:
				return "*";
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_Multiplicity(endKind.ToString()));
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001B67C File Offset: 0x0001987C
		private static string FunctionParameterModeAsXml(EdmFunctionParameterMode mode)
		{
			switch (mode)
			{
			case EdmFunctionParameterMode.In:
				return "In";
			case EdmFunctionParameterMode.Out:
				return "Out";
			case EdmFunctionParameterMode.InOut:
				return "InOut";
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_FunctionParameterMode(mode.ToString()));
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0001B6C8 File Offset: 0x000198C8
		private static string ConcurrencyModeAsXml(EdmConcurrencyMode mode)
		{
			switch (mode)
			{
			case EdmConcurrencyMode.None:
				return "None";
			case EdmConcurrencyMode.Fixed:
				return "Fixed";
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_ConcurrencyMode(mode.ToString()));
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0001B708 File Offset: 0x00019908
		private static string PathAsXml(IEnumerable<string> path)
		{
			return EdmUtil.JoinInternal<string>("/", path);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0001B715 File Offset: 0x00019915
		private static string ParameterAsXml(IEdmFunctionParameter parameter)
		{
			return parameter.Name;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0001B71D File Offset: 0x0001991D
		private static string PropertyAsXml(IEdmProperty property)
		{
			return property.Name;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0001B725 File Offset: 0x00019925
		private static string EnumMemberAsXml(IEdmEnumMember member)
		{
			return member.DeclaringType.FullName() + "/" + member.Name;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0001B742 File Offset: 0x00019942
		private static string EntitySetAsXml(IEdmEntitySet set)
		{
			return set.Container.FullName() + "/" + set.Name;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0001B75F File Offset: 0x0001995F
		private static string SridAsXml(int? i)
		{
			if (i == null)
			{
				return "Variable";
			}
			return Convert.ToString(i.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001B784 File Offset: 0x00019984
		private static string GetCsdlNamespace(Version edmVersion)
		{
			string[] array;
			if (CsdlConstants.SupportedVersions.TryGetValue(edmVersion, out array))
			{
				return array[0];
			}
			throw new InvalidOperationException(Strings.Serializer_UnknownEdmVersion);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001B7B0 File Offset: 0x000199B0
		private string SerializationName(IEdmSchemaElement element)
		{
			string text;
			if (this.namespaceAliasMappings != null && this.namespaceAliasMappings.TryGetValue(element.Namespace, out text))
			{
				return text + "." + element.Name;
			}
			return element.FullName();
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001B7F4 File Offset: 0x000199F4
		private string TypeReferenceAsXml(IEdmTypeReference type)
		{
			if (type.IsCollection())
			{
				IEdmCollectionTypeReference edmCollectionTypeReference = type.AsCollection();
				return "Collection(" + this.SerializationName((IEdmSchemaElement)edmCollectionTypeReference.ElementType().Definition) + ")";
			}
			if (type.IsEntityReference())
			{
				return "Ref(" + this.SerializationName(type.AsEntityReference().EntityReferenceDefinition().EntityType) + ")";
			}
			return this.SerializationName((IEdmSchemaElement)type.Definition);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001B875 File Offset: 0x00019A75
		private string TypeDefinitionAsXml(IEdmSchemaType type)
		{
			return this.SerializationName(type);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001B87E File Offset: 0x00019A7E
		private string FunctionAsXml(IEdmFunction function)
		{
			return this.SerializationName(function);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001B887 File Offset: 0x00019A87
		private string TermAsXml(IEdmTerm term)
		{
			if (term == null)
			{
				return string.Empty;
			}
			return this.SerializationName(term);
		}

		// Token: 0x040004A5 RID: 1189
		protected XmlWriter xmlWriter;

		// Token: 0x040004A6 RID: 1190
		protected Version version;

		// Token: 0x040004A7 RID: 1191
		private readonly VersioningDictionary<string, string> namespaceAliasMappings;

		// Token: 0x040004A8 RID: 1192
		private readonly IEdmModel model;
	}
}
