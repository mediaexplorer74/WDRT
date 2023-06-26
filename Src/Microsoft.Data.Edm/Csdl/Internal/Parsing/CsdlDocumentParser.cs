using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Common;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Validation;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing
{
	// Token: 0x0200015F RID: 351
	internal class CsdlDocumentParser : EdmXmlDocumentParser<CsdlSchema>
	{
		// Token: 0x060006EA RID: 1770 RVA: 0x000118DE File Offset: 0x0000FADE
		internal CsdlDocumentParser(string documentPath, XmlReader reader)
			: base(documentPath, reader)
		{
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0001193A File Offset: 0x0000FB3A
		internal override IEnumerable<KeyValuePair<Version, string>> SupportedVersions
		{
			get
			{
				return CsdlConstants.SupportedVersions.SelectMany((KeyValuePair<Version, string[]> kvp) => kvp.Value.Select((string ns) => new KeyValuePair<Version, string>(kvp.Key, ns)));
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00011963 File Offset: 0x0000FB63
		protected override bool TryGetDocumentElementParser(Version csdlArtifactVersion, XmlElementInfo rootElement, out XmlElementParser<CsdlSchema> parser)
		{
			EdmUtil.CheckArgumentNull<XmlElementInfo>(rootElement, "rootElement");
			this.artifactVersion = csdlArtifactVersion;
			if (string.Equals(rootElement.Name, "Schema", StringComparison.Ordinal))
			{
				parser = this.CreateRootElementParser();
				return true;
			}
			parser = null;
			return false;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001199C File Offset: 0x0000FB9C
		protected override void AnnotateItem(object result, XmlElementValueCollection childValues)
		{
			CsdlElement csdlElement = result as CsdlElement;
			if (csdlElement == null)
			{
				return;
			}
			foreach (XmlAnnotationInfo xmlAnnotationInfo in this.currentElement.Annotations)
			{
				csdlElement.AddAnnotation(new CsdlDirectValueAnnotation(xmlAnnotationInfo.NamespaceName, xmlAnnotationInfo.Name, xmlAnnotationInfo.Value, xmlAnnotationInfo.IsAttribute, xmlAnnotationInfo.Location));
			}
			foreach (CsdlVocabularyAnnotationBase csdlVocabularyAnnotationBase in childValues.ValuesOfType<CsdlVocabularyAnnotationBase>())
			{
				csdlElement.AddAnnotation(csdlVocabularyAnnotationBase);
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00011A5C File Offset: 0x0000FC5C
		private void AddChildParsers(XmlElementParser parent, IEnumerable<XmlElementParser> children)
		{
			foreach (XmlElementParser xmlElementParser in children)
			{
				parent.AddChildParser(xmlElementParser);
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00011ACC File Offset: 0x0000FCCC
		private XmlElementParser<CsdlSchema> CreateRootElementParser()
		{
			string text = "Documentation";
			Func<XmlElementInfo, XmlElementValueCollection, CsdlDocumentation> func = new Func<XmlElementInfo, XmlElementValueCollection, CsdlDocumentation>(this.OnDocumentationElement);
			XmlElementParser[] array = new XmlElementParser[2];
			array[0] = this.Element<string>("Summary", (XmlElementInfo element, XmlElementValueCollection children) => children.FirstText.Value, new XmlElementParser[0]);
			array[1] = this.Element<string>("LongDescription", (XmlElementInfo element, XmlElementValueCollection children) => children.FirstText.TextValue, new XmlElementParser[0]);
			XmlElementParser<CsdlDocumentation> xmlElementParser = base.CsdlElement<CsdlDocumentation>(text, func, array);
			XmlElementParser<CsdlTypeReference> xmlElementParser2 = base.CsdlElement<CsdlTypeReference>("ReferenceType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnEntityReferenceTypeElement), new XmlElementParser[] { xmlElementParser });
			XmlElementParser<CsdlTypeReference> xmlElementParser3 = base.CsdlElement<CsdlTypeReference>("RowType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnRowTypeElement), new XmlElementParser[0]);
			XmlElementParser<CsdlTypeReference> xmlElementParser4 = base.CsdlElement<CsdlTypeReference>("CollectionType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnCollectionTypeElement), new XmlElementParser[]
			{
				xmlElementParser,
				base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
				xmlElementParser3,
				xmlElementParser2
			});
			XmlElementParser<CsdlProperty> xmlElementParser5 = base.CsdlElement<CsdlProperty>("Property", new Func<XmlElementInfo, XmlElementValueCollection, CsdlProperty>(this.OnPropertyElement), new XmlElementParser[] { xmlElementParser });
			XmlElementParser<CsdlProperty> xmlElementParser6 = base.CsdlElement<CsdlProperty>("Property", new Func<XmlElementInfo, XmlElementValueCollection, CsdlProperty>(this.OnPropertyElement), new XmlElementParser[]
			{
				xmlElementParser,
				base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
				xmlElementParser3,
				xmlElementParser4,
				xmlElementParser2
			});
			XmlElementParser<CsdlExpressionBase> xmlElementParser7 = base.CsdlElement<CsdlExpressionBase>("String", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnStringConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser8 = base.CsdlElement<CsdlExpressionBase>("Binary", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnBinaryConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser9 = base.CsdlElement<CsdlExpressionBase>("Int", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnIntConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser10 = base.CsdlElement<CsdlExpressionBase>("Float", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnFloatConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser11 = base.CsdlElement<CsdlExpressionBase>("Guid", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnGuidConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser12 = base.CsdlElement<CsdlExpressionBase>("Decimal", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnDecimalConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser13 = base.CsdlElement<CsdlExpressionBase>("Bool", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnBoolConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser14 = base.CsdlElement<CsdlExpressionBase>("DateTime", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnDateTimeConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser15 = base.CsdlElement<CsdlExpressionBase>("Time", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnTimeConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser16 = base.CsdlElement<CsdlExpressionBase>("DateTimeOffset", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnDateTimeOffsetConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser17 = base.CsdlElement<CsdlExpressionBase>("Null", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnNullConstantExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser18 = base.CsdlElement<CsdlExpressionBase>("Path", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(CsdlDocumentParser.OnPathExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser19 = base.CsdlElement<CsdlExpressionBase>("FunctionReference", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnFunctionReferenceExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser20 = base.CsdlElement<CsdlExpressionBase>("ParameterReference", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnParameterReferenceExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser21 = base.CsdlElement<CsdlExpressionBase>("EntitySetReference", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnEntitySetReferenceExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser22 = base.CsdlElement<CsdlExpressionBase>("EnumMemberReference", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnEnumMemberReferenceExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser23 = base.CsdlElement<CsdlExpressionBase>("PropertyReference", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnPropertyReferenceExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser24 = base.CsdlElement<CsdlExpressionBase>("If", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnIfExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser25 = base.CsdlElement<CsdlExpressionBase>("AssertType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnAssertTypeExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser26 = base.CsdlElement<CsdlExpressionBase>("IsType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnIsTypeExpression), new XmlElementParser[0]);
			XmlElementParser<CsdlPropertyValue> xmlElementParser27 = base.CsdlElement<CsdlPropertyValue>("PropertyValue", new Func<XmlElementInfo, XmlElementValueCollection, CsdlPropertyValue>(this.OnPropertyValueElement), new XmlElementParser[0]);
			XmlElementParser<CsdlRecordExpression> xmlElementParser28 = base.CsdlElement<CsdlRecordExpression>("Record", new Func<XmlElementInfo, XmlElementValueCollection, CsdlRecordExpression>(this.OnRecordElement), new XmlElementParser[] { xmlElementParser27 });
			XmlElementParser<CsdlLabeledExpression> xmlElementParser29 = base.CsdlElement<CsdlLabeledExpression>("LabeledElement", new Func<XmlElementInfo, XmlElementValueCollection, CsdlLabeledExpression>(this.OnLabeledElement), new XmlElementParser[0]);
			XmlElementParser<CsdlCollectionExpression> xmlElementParser30 = base.CsdlElement<CsdlCollectionExpression>("Collection", new Func<XmlElementInfo, XmlElementValueCollection, CsdlCollectionExpression>(this.OnCollectionElement), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser31 = base.CsdlElement<CsdlExpressionBase>("Apply", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnApplyElement), new XmlElementParser[0]);
			XmlElementParser<CsdlExpressionBase> xmlElementParser32 = base.CsdlElement<CsdlExpressionBase>("LabeledElementReference", new Func<XmlElementInfo, XmlElementValueCollection, CsdlExpressionBase>(this.OnLabeledElementReferenceExpression), new XmlElementParser[0]);
			XmlElementParser[] array2 = new XmlElementParser[]
			{
				xmlElementParser7, xmlElementParser8, xmlElementParser9, xmlElementParser10, xmlElementParser11, xmlElementParser12, xmlElementParser13, xmlElementParser14, xmlElementParser16, xmlElementParser15,
				xmlElementParser17, xmlElementParser18, xmlElementParser24, xmlElementParser26, xmlElementParser25, xmlElementParser28, xmlElementParser30, xmlElementParser32, xmlElementParser23, xmlElementParser27,
				xmlElementParser29, xmlElementParser19, xmlElementParser21, xmlElementParser22, xmlElementParser20, xmlElementParser31
			};
			this.AddChildParsers(xmlElementParser23, array2);
			this.AddChildParsers(xmlElementParser24, array2);
			this.AddChildParsers(xmlElementParser25, array2);
			this.AddChildParsers(xmlElementParser26, array2);
			this.AddChildParsers(xmlElementParser27, array2);
			this.AddChildParsers(xmlElementParser30, array2);
			this.AddChildParsers(xmlElementParser29, array2);
			this.AddChildParsers(xmlElementParser31, array2);
			XmlElementParser<CsdlValueAnnotation> xmlElementParser33 = base.CsdlElement<CsdlValueAnnotation>("ValueAnnotation", new Func<XmlElementInfo, XmlElementValueCollection, CsdlValueAnnotation>(this.OnValueAnnotationElement), new XmlElementParser[0]);
			this.AddChildParsers(xmlElementParser33, array2);
			XmlElementParser<CsdlTypeAnnotation> xmlElementParser34 = base.CsdlElement<CsdlTypeAnnotation>("TypeAnnotation", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeAnnotation>(this.OnTypeAnnotationElement), new XmlElementParser[] { xmlElementParser27 });
			xmlElementParser5.AddChildParser(xmlElementParser33);
			xmlElementParser5.AddChildParser(xmlElementParser34);
			xmlElementParser6.AddChildParser(xmlElementParser33);
			xmlElementParser6.AddChildParser(xmlElementParser34);
			xmlElementParser3.AddChildParser(xmlElementParser6);
			xmlElementParser4.AddChildParser(xmlElementParser4);
			string text2 = "Schema";
			Func<XmlElementInfo, XmlElementValueCollection, CsdlSchema> func2 = new Func<XmlElementInfo, XmlElementValueCollection, CsdlSchema>(this.OnSchemaElement);
			XmlElementParser[] array3 = new XmlElementParser[10];
			array3[0] = xmlElementParser;
			array3[1] = base.CsdlElement<CsdlUsing>("Using", new Func<XmlElementInfo, XmlElementValueCollection, CsdlUsing>(this.OnUsingElement), new XmlElementParser[0]);
			array3[2] = base.CsdlElement<CsdlComplexType>("ComplexType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlComplexType>(this.OnComplexTypeElement), new XmlElementParser[] { xmlElementParser, xmlElementParser5, xmlElementParser33, xmlElementParser34 });
			array3[3] = base.CsdlElement<CsdlEntityType>("EntityType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEntityType>(this.OnEntityTypeElement), new XmlElementParser[]
			{
				xmlElementParser,
				base.CsdlElement<CsdlKey>("Key", new Func<XmlElementInfo, XmlElementValueCollection, CsdlKey>(CsdlDocumentParser.OnEntityKeyElement), new XmlElementParser[] { base.CsdlElement<CsdlPropertyReference>("PropertyRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlPropertyReference>(this.OnPropertyRefElement), new XmlElementParser[0]) }),
				xmlElementParser5,
				base.CsdlElement<CsdlNavigationProperty>("NavigationProperty", new Func<XmlElementInfo, XmlElementValueCollection, CsdlNavigationProperty>(this.OnNavigationPropertyElement), new XmlElementParser[] { xmlElementParser, xmlElementParser33, xmlElementParser34 }),
				xmlElementParser33,
				xmlElementParser34
			});
			array3[4] = base.CsdlElement<CsdlAssociation>("Association", new Func<XmlElementInfo, XmlElementValueCollection, CsdlAssociation>(this.OnAssociationElement), new XmlElementParser[]
			{
				xmlElementParser,
				base.CsdlElement<CsdlAssociationEnd>("End", new Func<XmlElementInfo, XmlElementValueCollection, CsdlAssociationEnd>(this.OnAssociationEndElement), new XmlElementParser[]
				{
					xmlElementParser,
					base.CsdlElement<CsdlOnDelete>("OnDelete", new Func<XmlElementInfo, XmlElementValueCollection, CsdlOnDelete>(this.OnDeleteActionElement), new XmlElementParser[] { xmlElementParser })
				}),
				base.CsdlElement<CsdlReferentialConstraint>("ReferentialConstraint", new Func<XmlElementInfo, XmlElementValueCollection, CsdlReferentialConstraint>(this.OnReferentialConstraintElement), new XmlElementParser[]
				{
					xmlElementParser,
					base.CsdlElement<CsdlReferentialConstraintRole>("Principal", new Func<XmlElementInfo, XmlElementValueCollection, CsdlReferentialConstraintRole>(this.OnReferentialConstraintRoleElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlPropertyReference>("PropertyRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlPropertyReference>(this.OnPropertyRefElement), new XmlElementParser[0])
					}),
					base.CsdlElement<CsdlReferentialConstraintRole>("Dependent", new Func<XmlElementInfo, XmlElementValueCollection, CsdlReferentialConstraintRole>(this.OnReferentialConstraintRoleElement), new XmlElementParser[]
					{
						xmlElementParser,
						base.CsdlElement<CsdlPropertyReference>("PropertyRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlPropertyReference>(this.OnPropertyRefElement), new XmlElementParser[0])
					})
				})
			});
			array3[5] = base.CsdlElement<CsdlEnumType>("EnumType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEnumType>(this.OnEnumTypeElement), new XmlElementParser[]
			{
				xmlElementParser,
				base.CsdlElement<CsdlEnumMember>("Member", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEnumMember>(this.OnEnumMemberElement), new XmlElementParser[] { xmlElementParser }),
				xmlElementParser33,
				xmlElementParser34
			});
			XmlElementParser[] array4 = array3;
			int num = 6;
			string text3 = "Function";
			Func<XmlElementInfo, XmlElementValueCollection, CsdlFunction> func3 = new Func<XmlElementInfo, XmlElementValueCollection, CsdlFunction>(this.OnFunctionElement);
			XmlElementParser[] array5 = new XmlElementParser[6];
			array5[0] = xmlElementParser;
			array5[1] = base.CsdlElement<CsdlFunctionParameter>("Parameter", new Func<XmlElementInfo, XmlElementValueCollection, CsdlFunctionParameter>(this.OnParameterElement), new XmlElementParser[]
			{
				xmlElementParser,
				base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
				xmlElementParser3,
				xmlElementParser4,
				xmlElementParser2,
				xmlElementParser33,
				xmlElementParser34
			});
			array5[2] = this.Element<string>("DefiningExpression", (XmlElementInfo element, XmlElementValueCollection children) => children.FirstText.Value, new XmlElementParser[0]);
			array5[3] = base.CsdlElement<CsdlFunctionReturnType>("ReturnType", new Func<XmlElementInfo, XmlElementValueCollection, CsdlFunctionReturnType>(this.OnReturnTypeElement), new XmlElementParser[]
			{
				xmlElementParser,
				base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
				xmlElementParser3,
				xmlElementParser4,
				xmlElementParser2
			});
			array5[4] = xmlElementParser33;
			array5[5] = xmlElementParser34;
			array4[num] = base.CsdlElement<CsdlFunction>(text3, func3, array5);
			array3[7] = base.CsdlElement<CsdlValueTerm>("ValueTerm", new Func<XmlElementInfo, XmlElementValueCollection, CsdlValueTerm>(this.OnValueTermElement), new XmlElementParser[]
			{
				base.CsdlElement<CsdlTypeReference>("TypeRef", new Func<XmlElementInfo, XmlElementValueCollection, CsdlTypeReference>(this.OnTypeRefElement), new XmlElementParser[] { xmlElementParser }),
				xmlElementParser3,
				xmlElementParser4,
				xmlElementParser2,
				xmlElementParser33,
				xmlElementParser34
			});
			array3[8] = base.CsdlElement<CsdlAnnotations>("Annotations", new Func<XmlElementInfo, XmlElementValueCollection, CsdlAnnotations>(this.OnAnnotationsElement), new XmlElementParser[] { xmlElementParser33, xmlElementParser34 });
			array3[9] = base.CsdlElement<CsdlEntityContainer>("EntityContainer", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEntityContainer>(this.OnEntityContainerElement), new XmlElementParser[]
			{
				xmlElementParser,
				base.CsdlElement<CsdlEntitySet>("EntitySet", new Func<XmlElementInfo, XmlElementValueCollection, CsdlEntitySet>(this.OnEntitySetElement), new XmlElementParser[] { xmlElementParser, xmlElementParser33, xmlElementParser34 }),
				base.CsdlElement<CsdlAssociationSet>("AssociationSet", new Func<XmlElementInfo, XmlElementValueCollection, CsdlAssociationSet>(this.OnAssociationSetElement), new XmlElementParser[]
				{
					xmlElementParser,
					base.CsdlElement<CsdlAssociationSetEnd>("End", new Func<XmlElementInfo, XmlElementValueCollection, CsdlAssociationSetEnd>(this.OnAssociationSetEndElement), new XmlElementParser[] { xmlElementParser })
				}),
				base.CsdlElement<CsdlFunctionImport>("FunctionImport", new Func<XmlElementInfo, XmlElementValueCollection, CsdlFunctionImport>(this.OnFunctionImportElement), new XmlElementParser[]
				{
					xmlElementParser,
					base.CsdlElement<CsdlFunctionParameter>("Parameter", new Func<XmlElementInfo, XmlElementValueCollection, CsdlFunctionParameter>(this.OnFunctionImportParameterElement), new XmlElementParser[] { xmlElementParser, xmlElementParser33, xmlElementParser34 }),
					xmlElementParser33,
					xmlElementParser34
				}),
				xmlElementParser33,
				xmlElementParser34
			});
			return base.CsdlElement<CsdlSchema>(text2, func2, array3);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00012727 File Offset: 0x00010927
		private static CsdlDocumentation Documentation(XmlElementValueCollection childValues)
		{
			return childValues.ValuesOfType<CsdlDocumentation>().FirstOrDefault<CsdlDocumentation>();
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00012734 File Offset: 0x00010934
		private CsdlSchema OnSchemaElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Optional("Namespace") ?? string.Empty;
			string text2 = base.OptionalAlias("Alias");
			return new CsdlSchema(text, text2, this.artifactVersion, childValues.ValuesOfType<CsdlUsing>(), childValues.ValuesOfType<CsdlAssociation>(), childValues.ValuesOfType<CsdlStructuredType>(), childValues.ValuesOfType<CsdlEnumType>(), childValues.ValuesOfType<CsdlFunction>(), childValues.ValuesOfType<CsdlValueTerm>(), childValues.ValuesOfType<CsdlEntityContainer>(), childValues.ValuesOfType<CsdlAnnotations>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000127AD File Offset: 0x000109AD
		private CsdlDocumentation OnDocumentationElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return new CsdlDocumentation(childValues["Summary"].TextValue, childValues["LongDescription"].TextValue, element.Location);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000127DC File Offset: 0x000109DC
		private CsdlUsing OnUsingElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Namespace");
			string text2 = base.RequiredAlias("Alias");
			return new CsdlUsing(text, text2, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00012814 File Offset: 0x00010A14
		private CsdlComplexType OnComplexTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalQualifiedName("BaseType");
			bool flag = base.OptionalBoolean("Abstract") ?? false;
			return new CsdlComplexType(text, text2, flag, childValues.ValuesOfType<CsdlProperty>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00012874 File Offset: 0x00010A74
		private CsdlEntityType OnEntityTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalQualifiedName("BaseType");
			bool flag = base.OptionalBoolean("OpenType") ?? false;
			bool flag2 = base.OptionalBoolean("Abstract") ?? false;
			CsdlKey csdlKey = childValues.ValuesOfType<CsdlKey>().FirstOrDefault<CsdlKey>();
			return new CsdlEntityType(text, text2, flag2, flag, csdlKey, childValues.ValuesOfType<CsdlProperty>(), childValues.ValuesOfType<CsdlNavigationProperty>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001290C File Offset: 0x00010B0C
		private CsdlProperty OnPropertyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			string text2 = base.Required("Name");
			string text3 = base.Optional("DefaultValue");
			bool flag = (base.OptionalConcurrencyMode("ConcurrencyMode") ?? EdmConcurrencyMode.None) == EdmConcurrencyMode.Fixed;
			return new CsdlProperty(text2, csdlTypeReference, flag, text3, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001298C File Offset: 0x00010B8C
		private CsdlValueTerm OnValueTermElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			string text2 = base.Required("Name");
			return new CsdlValueTerm(text2, csdlTypeReference, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000129D4 File Offset: 0x00010BD4
		private CsdlAnnotations OnAnnotationsElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Target");
			string text2 = base.Optional("Qualifier");
			IEnumerable<CsdlVocabularyAnnotationBase> enumerable = childValues.ValuesOfType<CsdlVocabularyAnnotationBase>();
			return new CsdlAnnotations(enumerable, text, text2);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00012A08 File Offset: 0x00010C08
		private CsdlValueAnnotation OnValueAnnotationElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredQualifiedName("Term");
			string text2 = base.Optional("Qualifier");
			CsdlExpressionBase csdlExpressionBase = this.ParseAnnotationExpression(element, childValues);
			return new CsdlValueAnnotation(text, text2, csdlExpressionBase, element.Location);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00012A44 File Offset: 0x00010C44
		private CsdlTypeAnnotation OnTypeAnnotationElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredQualifiedName("Term");
			string text2 = base.Optional("Qualifier");
			IEnumerable<CsdlPropertyValue> enumerable = childValues.ValuesOfType<CsdlPropertyValue>();
			return new CsdlTypeAnnotation(text, text2, enumerable, element.Location);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00012A80 File Offset: 0x00010C80
		private CsdlPropertyValue OnPropertyValueElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Property");
			CsdlExpressionBase csdlExpressionBase = this.ParseAnnotationExpression(element, childValues);
			return new CsdlPropertyValue(text, csdlExpressionBase, element.Location);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00012AB0 File Offset: 0x00010CB0
		private CsdlRecordExpression OnRecordElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalQualifiedName("Type");
			IEnumerable<CsdlPropertyValue> enumerable = childValues.ValuesOfType<CsdlPropertyValue>();
			return new CsdlRecordExpression((text != null) ? new CsdlNamedTypeReference(text, false, element.Location) : null, enumerable, element.Location);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00012AF0 File Offset: 0x00010CF0
		private CsdlCollectionExpression OnCollectionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Optional);
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			return new CsdlCollectionExpression(csdlTypeReference, enumerable, element.Location);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00012B30 File Offset: 0x00010D30
		private CsdlLabeledExpression OnLabeledElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			if (enumerable.Count<CsdlExpressionBase>() != 1)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidLabeledElementExpressionIncorrectNumberOfOperands, Strings.CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands);
			}
			return new CsdlLabeledExpression(text, enumerable.ElementAtOrDefault(0), element.Location);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00012B84 File Offset: 0x00010D84
		private CsdlApplyExpression OnApplyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Optional("Function");
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			return new CsdlApplyExpression(text, enumerable, element.Location);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00012BB4 File Offset: 0x00010DB4
		private static CsdlConstantExpression ConstantExpression(EdmValueKind kind, XmlElementValueCollection childValues, CsdlLocation location)
		{
			XmlTextValue firstText = childValues.FirstText;
			return new CsdlConstantExpression(kind, (firstText != null) ? firstText.TextValue : string.Empty, location);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00012BDF File Offset: 0x00010DDF
		private static CsdlConstantExpression OnIntConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Integer, childValues, element.Location);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00012BEF File Offset: 0x00010DEF
		private static CsdlConstantExpression OnStringConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.String, childValues, element.Location);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00012BFF File Offset: 0x00010DFF
		private static CsdlConstantExpression OnBinaryConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Binary, childValues, element.Location);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00012C0E File Offset: 0x00010E0E
		private static CsdlConstantExpression OnFloatConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Floating, childValues, element.Location);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00012C1D File Offset: 0x00010E1D
		private static CsdlConstantExpression OnGuidConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Guid, childValues, element.Location);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00012C2C File Offset: 0x00010E2C
		private static CsdlConstantExpression OnDecimalConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Decimal, childValues, element.Location);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00012C3B File Offset: 0x00010E3B
		private static CsdlConstantExpression OnBoolConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Boolean, childValues, element.Location);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00012C4A File Offset: 0x00010E4A
		private static CsdlConstantExpression OnTimeConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Time, childValues, element.Location);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00012C5A File Offset: 0x00010E5A
		private static CsdlConstantExpression OnDateTimeConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.DateTime, childValues, element.Location);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00012C69 File Offset: 0x00010E69
		private static CsdlConstantExpression OnDateTimeOffsetConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.DateTimeOffset, childValues, element.Location);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00012C78 File Offset: 0x00010E78
		private static CsdlConstantExpression OnNullConstantExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return CsdlDocumentParser.ConstantExpression(EdmValueKind.Null, childValues, element.Location);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00012C88 File Offset: 0x00010E88
		private static CsdlPathExpression OnPathExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			XmlTextValue firstText = childValues.FirstText;
			return new CsdlPathExpression((firstText != null) ? firstText.TextValue : string.Empty, element.Location);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00012CB8 File Offset: 0x00010EB8
		private CsdlLabeledExpressionReferenceExpression OnLabeledElementReferenceExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			return new CsdlLabeledExpressionReferenceExpression(text, element.Location);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00012CE0 File Offset: 0x00010EE0
		private CsdlEntitySetReferenceExpression OnEntitySetReferenceExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredEntitySetPath("Name");
			return new CsdlEntitySetReferenceExpression(text, element.Location);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00012D08 File Offset: 0x00010F08
		private CsdlEnumMemberReferenceExpression OnEnumMemberReferenceExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredEnumMemberPath("Name");
			return new CsdlEnumMemberReferenceExpression(text, element.Location);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00012D30 File Offset: 0x00010F30
		private CsdlPropertyReferenceExpression OnPropertyReferenceExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			return new CsdlPropertyReferenceExpression(text, childValues.ValuesOfType<CsdlExpressionBase>().FirstOrDefault<CsdlExpressionBase>(), element.Location);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00012D60 File Offset: 0x00010F60
		private CsdlFunctionReferenceExpression OnFunctionReferenceExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredQualifiedName("Name");
			return new CsdlFunctionReferenceExpression(text, element.Location);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00012D88 File Offset: 0x00010F88
		private CsdlParameterReferenceExpression OnParameterReferenceExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			return new CsdlParameterReferenceExpression(text, element.Location);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00012DB0 File Offset: 0x00010FB0
		private CsdlExpressionBase OnIfExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			if (enumerable.Count<CsdlExpressionBase>() != 3)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidIfExpressionIncorrectNumberOfOperands, Strings.CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands);
			}
			return new CsdlIfExpression(enumerable.ElementAtOrDefault(0), enumerable.ElementAtOrDefault(1), enumerable.ElementAtOrDefault(2), element.Location);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00012E04 File Offset: 0x00011004
		private CsdlExpressionBase OnAssertTypeExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			if (enumerable.Count<CsdlExpressionBase>() != 1)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidAssertTypeExpressionIncorrectNumberOfOperands, Strings.CsdlParser_InvalidAssertTypeExpressionIncorrectNumberOfOperands);
			}
			return new CsdlAssertTypeExpression(csdlTypeReference, enumerable.ElementAtOrDefault(0), element.Location);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00012E68 File Offset: 0x00011068
		private CsdlExpressionBase OnIsTypeExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			IEnumerable<CsdlExpressionBase> enumerable = childValues.ValuesOfType<CsdlExpressionBase>();
			if (enumerable.Count<CsdlExpressionBase>() != 1)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidIsTypeExpressionIncorrectNumberOfOperands, Strings.CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands);
			}
			return new CsdlIsTypeExpression(csdlTypeReference, enumerable.ElementAtOrDefault(0), element.Location);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00012ECC File Offset: 0x000110CC
		private CsdlExpressionBase ParseAnnotationExpression(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			CsdlExpressionBase csdlExpressionBase = childValues.ValuesOfType<CsdlExpressionBase>().FirstOrDefault<CsdlExpressionBase>();
			if (csdlExpressionBase != null)
			{
				return csdlExpressionBase;
			}
			string text = base.Optional("Path");
			if (text != null)
			{
				return new CsdlPathExpression(text, element.Location);
			}
			string text2 = base.Optional("String");
			EdmValueKind edmValueKind;
			if (text2 != null)
			{
				edmValueKind = EdmValueKind.String;
			}
			else
			{
				text2 = base.Optional("Bool");
				if (text2 != null)
				{
					edmValueKind = EdmValueKind.Boolean;
				}
				else
				{
					text2 = base.Optional("Int");
					if (text2 != null)
					{
						edmValueKind = EdmValueKind.Integer;
					}
					else
					{
						text2 = base.Optional("Float");
						if (text2 != null)
						{
							edmValueKind = EdmValueKind.Floating;
						}
						else
						{
							text2 = base.Optional("DateTime");
							if (text2 != null)
							{
								edmValueKind = EdmValueKind.DateTime;
							}
							else
							{
								text2 = base.Optional("DateTimeOffset");
								if (text2 != null)
								{
									edmValueKind = EdmValueKind.DateTimeOffset;
								}
								else
								{
									text2 = base.Optional("Time");
									if (text2 != null)
									{
										edmValueKind = EdmValueKind.Time;
									}
									else
									{
										text2 = base.Optional("Decimal");
										if (text2 != null)
										{
											edmValueKind = EdmValueKind.Decimal;
										}
										else
										{
											text2 = base.Optional("Binary");
											if (text2 != null)
											{
												edmValueKind = EdmValueKind.Binary;
											}
											else
											{
												text2 = base.Optional("Guid");
												if (text2 == null)
												{
													return null;
												}
												edmValueKind = EdmValueKind.Guid;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return new CsdlConstantExpression(edmValueKind, text2, element.Location);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00012FE0 File Offset: 0x000111E0
		private CsdlNamedTypeReference ParseNamedTypeReference(string typeName, bool isNullable, CsdlLocation parentLocation)
		{
			EdmCoreModel instance = EdmCoreModel.Instance;
			EdmPrimitiveTypeKind primitiveTypeKind = instance.GetPrimitiveTypeKind(typeName);
			switch (primitiveTypeKind)
			{
			case EdmPrimitiveTypeKind.Binary:
			{
				bool flag;
				int? num;
				bool? flag2;
				this.ParseBinaryFacets(out flag, out num, out flag2);
				return new CsdlBinaryTypeReference(flag2, flag, num, typeName, isNullable, parentLocation);
			}
			case EdmPrimitiveTypeKind.Boolean:
			case EdmPrimitiveTypeKind.Byte:
			case EdmPrimitiveTypeKind.Double:
			case EdmPrimitiveTypeKind.Guid:
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
			case EdmPrimitiveTypeKind.Single:
			case EdmPrimitiveTypeKind.Stream:
				return new CsdlPrimitiveTypeReference(primitiveTypeKind, typeName, isNullable, parentLocation);
			case EdmPrimitiveTypeKind.DateTime:
			case EdmPrimitiveTypeKind.DateTimeOffset:
			case EdmPrimitiveTypeKind.Time:
			{
				int? num2;
				this.ParseTemporalFacets(out num2);
				return new CsdlTemporalTypeReference(primitiveTypeKind, num2, typeName, isNullable, parentLocation);
			}
			case EdmPrimitiveTypeKind.Decimal:
			{
				int? num3;
				int? num4;
				this.ParseDecimalFacets(out num3, out num4);
				return new CsdlDecimalTypeReference(num3, num4, typeName, isNullable, parentLocation);
			}
			case EdmPrimitiveTypeKind.String:
			{
				bool flag3;
				int? num5;
				bool? flag4;
				bool? flag5;
				string text;
				this.ParseStringFacets(out flag3, out num5, out flag4, out flag5, out text);
				return new CsdlStringTypeReference(flag4, flag3, num5, flag5, text, typeName, isNullable, parentLocation);
			}
			case EdmPrimitiveTypeKind.Geography:
			case EdmPrimitiveTypeKind.GeographyPoint:
			case EdmPrimitiveTypeKind.GeographyLineString:
			case EdmPrimitiveTypeKind.GeographyPolygon:
			case EdmPrimitiveTypeKind.GeographyCollection:
			case EdmPrimitiveTypeKind.GeographyMultiPolygon:
			case EdmPrimitiveTypeKind.GeographyMultiLineString:
			case EdmPrimitiveTypeKind.GeographyMultiPoint:
			{
				int? num6;
				this.ParseSpatialFacets(out num6, 4326);
				return new CsdlSpatialTypeReference(primitiveTypeKind, num6, typeName, isNullable, parentLocation);
			}
			case EdmPrimitiveTypeKind.Geometry:
			case EdmPrimitiveTypeKind.GeometryPoint:
			case EdmPrimitiveTypeKind.GeometryLineString:
			case EdmPrimitiveTypeKind.GeometryPolygon:
			case EdmPrimitiveTypeKind.GeometryCollection:
			case EdmPrimitiveTypeKind.GeometryMultiPolygon:
			case EdmPrimitiveTypeKind.GeometryMultiLineString:
			case EdmPrimitiveTypeKind.GeometryMultiPoint:
			{
				int? num7;
				this.ParseSpatialFacets(out num7, 0);
				return new CsdlSpatialTypeReference(primitiveTypeKind, num7, typeName, isNullable, parentLocation);
			}
			default:
				return new CsdlNamedTypeReference(typeName, isNullable, parentLocation);
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00013134 File Offset: 0x00011334
		private CsdlTypeReference ParseTypeReference(string typeString, XmlElementValueCollection childValues, CsdlLocation parentLocation, CsdlDocumentParser.Optionality typeInfoOptionality)
		{
			bool flag = base.OptionalBoolean("Nullable") ?? true;
			CsdlTypeReference csdlTypeReference = null;
			if (typeString != null)
			{
				string[] array = typeString.Split(new char[] { '(', ')' });
				string text = array[0];
				string text2;
				if ((text2 = text) != null)
				{
					if (text2 == "Collection")
					{
						string text3 = ((array.Count<string>() > 1) ? array[1] : typeString);
						csdlTypeReference = new CsdlExpressionTypeReference(new CsdlCollectionType(this.ParseNamedTypeReference(text3, flag, parentLocation), parentLocation), false, parentLocation);
						goto IL_DF;
					}
					if (text2 == "Ref")
					{
						string text4 = ((array.Count<string>() > 1) ? array[1] : typeString);
						csdlTypeReference = new CsdlExpressionTypeReference(new CsdlEntityReferenceType(this.ParseNamedTypeReference(text4, flag, parentLocation), parentLocation), true, parentLocation);
						goto IL_DF;
					}
				}
				csdlTypeReference = this.ParseNamedTypeReference(text, flag, parentLocation);
			}
			else if (childValues != null)
			{
				csdlTypeReference = childValues.ValuesOfType<CsdlTypeReference>().FirstOrDefault<CsdlTypeReference>();
			}
			IL_DF:
			if (csdlTypeReference == null && typeInfoOptionality == CsdlDocumentParser.Optionality.Required)
			{
				if (childValues != null)
				{
					base.ReportError(parentLocation, EdmErrorCode.MissingType, Strings.CsdlParser_MissingTypeAttributeOrElement);
				}
				csdlTypeReference = new CsdlNamedTypeReference(string.Empty, flag, parentLocation);
			}
			return csdlTypeReference;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00013248 File Offset: 0x00011448
		private CsdlNavigationProperty OnNavigationPropertyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.Required("Relationship");
			string text3 = base.Required("ToRole");
			string text4 = base.Required("FromRole");
			bool? flag = base.OptionalBoolean("ContainsTarget");
			return new CsdlNavigationProperty(text, text2, text3, text4, flag ?? false, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000132BE File Offset: 0x000114BE
		private static CsdlKey OnEntityKeyElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return new CsdlKey(new List<CsdlPropertyReference>(childValues.ValuesOfType<CsdlPropertyReference>()), element.Location);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000132D6 File Offset: 0x000114D6
		private CsdlPropertyReference OnPropertyRefElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return new CsdlPropertyReference(base.Required("Name"), element.Location);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000132F0 File Offset: 0x000114F0
		private CsdlEnumType OnEnumTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalType("UnderlyingType");
			bool? flag = base.OptionalBoolean("IsFlags");
			return new CsdlEnumType(text, text2, flag ?? false, childValues.ValuesOfType<CsdlEnumMember>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00013350 File Offset: 0x00011550
		private CsdlEnumMember OnEnumMemberElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			long? num = base.OptionalLong("Value");
			return new CsdlEnumMember(text, num, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00013388 File Offset: 0x00011588
		private CsdlAssociation OnAssociationElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			IEnumerable<CsdlAssociationEnd> enumerable = childValues.ValuesOfType<CsdlAssociationEnd>();
			if (enumerable.Count<CsdlAssociationEnd>() != 2)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidAssociation, Strings.CsdlParser_InvalidAssociationIncorrectNumberOfEnds(text));
			}
			IEnumerable<CsdlReferentialConstraint> enumerable2 = childValues.ValuesOfType<CsdlReferentialConstraint>();
			if (enumerable2.Count<CsdlReferentialConstraint>() > 1)
			{
				base.ReportError(childValues.OfResultType<CsdlReferentialConstraint>().ElementAt(1).Location, EdmErrorCode.InvalidAssociation, Strings.CsdlParser_AssociationHasAtMostOneConstraint);
			}
			return new CsdlAssociation(text, enumerable.ElementAtOrDefault(0), enumerable.ElementAtOrDefault(1), enumerable2.FirstOrDefault<CsdlReferentialConstraint>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001341C File Offset: 0x0001161C
		private CsdlAssociationEnd OnAssociationEndElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredType("Type");
			string text2 = base.Optional("Role");
			EdmMultiplicity edmMultiplicity = base.RequiredMultiplicity("Multiplicity");
			CsdlOnDelete csdlOnDelete = childValues.ValuesOfType<CsdlOnDelete>().FirstOrDefault<CsdlOnDelete>();
			bool flag;
			switch (edmMultiplicity)
			{
			case EdmMultiplicity.One:
			case EdmMultiplicity.Many:
				flag = false;
				break;
			default:
				flag = true;
				break;
			}
			CsdlNamedTypeReference csdlNamedTypeReference = new CsdlNamedTypeReference(text, flag, element.Location);
			return new CsdlAssociationEnd(text2, csdlNamedTypeReference, edmMultiplicity, csdlOnDelete, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000134A0 File Offset: 0x000116A0
		private CsdlOnDelete OnDeleteActionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			EdmOnDeleteAction edmOnDeleteAction = base.RequiredOnDeleteAction("Action");
			return new CsdlOnDelete(edmOnDeleteAction, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000134DC File Offset: 0x000116DC
		private CsdlReferentialConstraint OnReferentialConstraintElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			CsdlReferentialConstraintRole constraintRole = this.GetConstraintRole(childValues, "Principal", () => Strings.CsdlParser_ReferentialConstraintRequiresOnePrincipal);
			CsdlReferentialConstraintRole constraintRole2 = this.GetConstraintRole(childValues, "Dependent", () => Strings.CsdlParser_ReferentialConstraintRequiresOneDependent);
			return new CsdlReferentialConstraint(constraintRole, constraintRole2, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00013550 File Offset: 0x00011750
		private CsdlReferentialConstraintRole GetConstraintRole(XmlElementValueCollection childValues, string roleElementName, Func<string> improperNumberMessage)
		{
			IEnumerable<XmlElementValue<CsdlReferentialConstraintRole>> enumerable = childValues.FindByName<CsdlReferentialConstraintRole>(roleElementName).ToArray<XmlElementValue<CsdlReferentialConstraintRole>>();
			if (enumerable.Count<XmlElementValue<CsdlReferentialConstraintRole>>() > 1)
			{
				base.ReportError(enumerable.ElementAt(1).Location, EdmErrorCode.InvalidAssociation, improperNumberMessage());
			}
			XmlElementValue<CsdlReferentialConstraintRole> xmlElementValue = enumerable.FirstOrDefault<XmlElementValue<CsdlReferentialConstraintRole>>();
			if (xmlElementValue == null)
			{
				return null;
			}
			return xmlElementValue.Value;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000135A0 File Offset: 0x000117A0
		private CsdlReferentialConstraintRole OnReferentialConstraintRoleElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Role");
			IEnumerable<CsdlPropertyReference> enumerable = childValues.ValuesOfType<CsdlPropertyReference>();
			return new CsdlReferentialConstraintRole(text, enumerable, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000135D4 File Offset: 0x000117D4
		private CsdlFunction OnFunctionElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalType("ReturnType");
			IEnumerable<CsdlFunctionParameter> enumerable = childValues.ValuesOfType<CsdlFunctionParameter>();
			XmlElementValue xmlElementValue = childValues["DefiningExpression"];
			string text3 = null;
			if (!(xmlElementValue is XmlElementValueCollection.MissingXmlElementValue))
			{
				text3 = xmlElementValue.TextValue ?? string.Empty;
			}
			CsdlTypeReference csdlTypeReference = null;
			if (text2 == null)
			{
				CsdlFunctionReturnType csdlFunctionReturnType = childValues.ValuesOfType<CsdlFunctionReturnType>().FirstOrDefault<CsdlFunctionReturnType>();
				if (csdlFunctionReturnType != null)
				{
					csdlTypeReference = csdlFunctionReturnType.ReturnType;
				}
			}
			else
			{
				csdlTypeReference = this.ParseTypeReference(text2, null, element.Location, CsdlDocumentParser.Optionality.Required);
			}
			return new CsdlFunction(text, enumerable, text3, csdlTypeReference, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00013674 File Offset: 0x00011874
		private CsdlFunctionParameter OnParameterElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text2, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			return new CsdlFunctionParameter(text, csdlTypeReference, EdmFunctionParameterMode.In, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x000136C0 File Offset: 0x000118C0
		private CsdlFunctionImport OnFunctionImportElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.OptionalType("ReturnType");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text2, childValues, element.Location, CsdlDocumentParser.Optionality.Optional);
			bool flag = base.OptionalBoolean("IsComposable") ?? false;
			bool flag2 = base.OptionalBoolean("IsSideEffecting") ?? (!flag);
			bool flag3 = base.OptionalBoolean("IsBindable") ?? false;
			string text3 = base.Optional("EntitySet");
			string text4 = base.Optional("EntitySetPath");
			IEnumerable<CsdlFunctionParameter> enumerable = childValues.ValuesOfType<CsdlFunctionParameter>();
			return new CsdlFunctionImport(text, flag2, flag, flag3, text3, text4, enumerable, csdlTypeReference, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000137A0 File Offset: 0x000119A0
		private CsdlFunctionParameter OnFunctionImportParameterElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredType("Type");
			EdmFunctionParameterMode? edmFunctionParameterMode = base.OptionalFunctionParameterMode("Mode");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text2, null, element.Location, CsdlDocumentParser.Optionality.Required);
			return new CsdlFunctionParameter(text, csdlTypeReference, edmFunctionParameterMode ?? EdmFunctionParameterMode.In, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001380C File Offset: 0x00011A0C
		private CsdlTypeReference OnEntityReferenceTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredType("Type");
			return new CsdlExpressionTypeReference(new CsdlEntityReferenceType(this.ParseTypeReference(text, null, element.Location, CsdlDocumentParser.Optionality.Required), element.Location), true, element.Location);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001384C File Offset: 0x00011A4C
		private CsdlTypeReference OnTypeRefElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.RequiredType("Type");
			return this.ParseTypeReference(text, null, element.Location, CsdlDocumentParser.Optionality.Required);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00013874 File Offset: 0x00011A74
		private CsdlTypeReference OnRowTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			return new CsdlExpressionTypeReference(new CsdlRowType(childValues.ValuesOfType<CsdlProperty>(), element.Location), true, element.Location);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00013894 File Offset: 0x00011A94
		private CsdlTypeReference OnCollectionTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("ElementType");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			return new CsdlExpressionTypeReference(new CsdlCollectionType(csdlTypeReference, element.Location), false, element.Location);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000138D8 File Offset: 0x00011AD8
		private CsdlFunctionReturnType OnReturnTypeElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.OptionalType("Type");
			CsdlTypeReference csdlTypeReference = this.ParseTypeReference(text, childValues, element.Location, CsdlDocumentParser.Optionality.Required);
			return new CsdlFunctionReturnType(csdlTypeReference, element.Location);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00013910 File Offset: 0x00011B10
		private CsdlEntityContainer OnEntityContainerElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.Optional("Extends");
			return new CsdlEntityContainer(text, text2, childValues.ValuesOfType<CsdlEntitySet>(), childValues.ValuesOfType<CsdlAssociationSet>(), childValues.ValuesOfType<CsdlFunctionImport>(), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001395C File Offset: 0x00011B5C
		private CsdlEntitySet OnEntitySetElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredQualifiedName("EntityType");
			return new CsdlEntitySet(text, text2, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00013994 File Offset: 0x00011B94
		private CsdlAssociationSet OnAssociationSetElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Name");
			string text2 = base.RequiredQualifiedName("Association");
			IEnumerable<CsdlAssociationSetEnd> enumerable = childValues.ValuesOfType<CsdlAssociationSetEnd>();
			if (enumerable.Count<CsdlAssociationSetEnd>() > 2)
			{
				base.ReportError(element.Location, EdmErrorCode.InvalidAssociationSet, Strings.CsdlParser_InvalidAssociationSetIncorrectNumberOfEnds(text));
			}
			return new CsdlAssociationSet(text, text2, enumerable.ElementAtOrDefault(0), enumerable.ElementAtOrDefault(1), CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00013A04 File Offset: 0x00011C04
		private CsdlAssociationSetEnd OnAssociationSetEndElement(XmlElementInfo element, XmlElementValueCollection childValues)
		{
			string text = base.Required("Role");
			string text2 = base.Required("EntitySet");
			return new CsdlAssociationSetEnd(text, text2, CsdlDocumentParser.Documentation(childValues), element.Location);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00013A3C File Offset: 0x00011C3C
		private void ParseMaxLength(out bool Unbounded, out int? maxLength)
		{
			string text = base.Optional("MaxLength");
			if (text == null)
			{
				Unbounded = false;
				maxLength = null;
				return;
			}
			if (text.EqualsOrdinalIgnoreCase("Max"))
			{
				Unbounded = true;
				maxLength = null;
				return;
			}
			Unbounded = false;
			maxLength = base.OptionalMaxLength("MaxLength");
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00013A8F File Offset: 0x00011C8F
		private void ParseBinaryFacets(out bool Unbounded, out int? maxLength, out bool? fixedLength)
		{
			this.ParseMaxLength(out Unbounded, out maxLength);
			fixedLength = base.OptionalBoolean("FixedLength");
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00013AAA File Offset: 0x00011CAA
		private void ParseDecimalFacets(out int? precision, out int? scale)
		{
			precision = base.OptionalInteger("Precision");
			scale = base.OptionalInteger("Scale");
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00013ACE File Offset: 0x00011CCE
		private void ParseStringFacets(out bool Unbounded, out int? maxLength, out bool? fixedLength, out bool? unicode, out string collation)
		{
			this.ParseMaxLength(out Unbounded, out maxLength);
			fixedLength = base.OptionalBoolean("FixedLength");
			unicode = base.OptionalBoolean("Unicode");
			collation = base.Optional("Collation");
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00013B09 File Offset: 0x00011D09
		private void ParseTemporalFacets(out int? precision)
		{
			precision = base.OptionalInteger("Precision");
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00013B1C File Offset: 0x00011D1C
		private void ParseSpatialFacets(out int? srid, int defaultSrid)
		{
			srid = base.OptionalSrid("SRID", defaultSrid);
		}

		// Token: 0x04000392 RID: 914
		private Version artifactVersion;

		// Token: 0x02000160 RID: 352
		private enum Optionality
		{
			// Token: 0x0400039A RID: 922
			Optional,
			// Token: 0x0400039B RID: 923
			Required
		}
	}
}
