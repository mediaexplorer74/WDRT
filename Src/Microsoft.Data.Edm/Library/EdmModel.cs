using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Library.Annotations;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001D5 RID: 469
	public class EdmModel : EdmModelBase
	{
		// Token: 0x06000B24 RID: 2852 RVA: 0x000208AA File Offset: 0x0001EAAA
		public EdmModel()
			: base(Enumerable.Empty<IEdmModel>(), new EdmDirectValueAnnotationsManager())
		{
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x000208DD File Offset: 0x0001EADD
		public override IEnumerable<IEdmSchemaElement> SchemaElements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x000208EE File Offset: 0x0001EAEE
		public override IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations
		{
			get
			{
				return this.vocabularyAnnotationsDictionary.SelectMany((KeyValuePair<IEdmVocabularyAnnotatable, List<IEdmVocabularyAnnotation>> kvp) => kvp.Value);
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00020918 File Offset: 0x0001EB18
		public new void AddReferencedModel(IEdmModel model)
		{
			base.AddReferencedModel(model);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00020924 File Offset: 0x0001EB24
		public void AddElement(IEdmSchemaElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmSchemaElement>(element, "element");
			this.elements.Add(element);
			IEdmStructuredType edmStructuredType = element as IEdmStructuredType;
			if (edmStructuredType != null && edmStructuredType.BaseType != null)
			{
				List<IEdmStructuredType> list;
				if (!this.derivedTypeMappings.TryGetValue(edmStructuredType.BaseType, out list))
				{
					list = new List<IEdmStructuredType>();
					this.derivedTypeMappings[edmStructuredType.BaseType] = list;
				}
				list.Add(edmStructuredType);
			}
			base.RegisterElement(element);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00020998 File Offset: 0x0001EB98
		public void AddElements(IEnumerable<IEdmSchemaElement> newElements)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmSchemaElement>>(newElements, "newElements");
			foreach (IEdmSchemaElement edmSchemaElement in newElements)
			{
				this.AddElement(edmSchemaElement);
			}
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000209EC File Offset: 0x0001EBEC
		public void AddVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			if (annotation.Target == null)
			{
				throw new InvalidOperationException(Strings.Constructable_VocabularyAnnotationMustHaveTarget);
			}
			List<IEdmVocabularyAnnotation> list;
			if (!this.vocabularyAnnotationsDictionary.TryGetValue(annotation.Target, out list))
			{
				list = new List<IEdmVocabularyAnnotation>();
				this.vocabularyAnnotationsDictionary.Add(annotation.Target, list);
			}
			list.Add(annotation);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00020A4C File Offset: 0x0001EC4C
		public override IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element)
		{
			List<IEdmVocabularyAnnotation> list;
			if (!this.vocabularyAnnotationsDictionary.TryGetValue(element, out list))
			{
				return Enumerable.Empty<IEdmVocabularyAnnotation>();
			}
			return list;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00020A70 File Offset: 0x0001EC70
		public override IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType)
		{
			List<IEdmStructuredType> list;
			if (this.derivedTypeMappings.TryGetValue(baseType, out list))
			{
				return list;
			}
			return Enumerable.Empty<IEdmStructuredType>();
		}

		// Token: 0x0400053D RID: 1341
		private readonly List<IEdmSchemaElement> elements = new List<IEdmSchemaElement>();

		// Token: 0x0400053E RID: 1342
		private readonly Dictionary<IEdmVocabularyAnnotatable, List<IEdmVocabularyAnnotation>> vocabularyAnnotationsDictionary = new Dictionary<IEdmVocabularyAnnotatable, List<IEdmVocabularyAnnotation>>();

		// Token: 0x0400053F RID: 1343
		private readonly Dictionary<IEdmStructuredType, List<IEdmStructuredType>> derivedTypeMappings = new Dictionary<IEdmStructuredType, List<IEdmStructuredType>>();
	}
}
