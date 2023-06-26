using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000180 RID: 384
	internal sealed class ODataEdmStructuredValue : EdmValue, IEdmStructuredValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000ACA RID: 2762 RVA: 0x00024100 File Offset: 0x00022300
		internal ODataEdmStructuredValue(ODataEntry entry)
			: base(entry.GetEdmType())
		{
			this.properties = entry.NonComputedProperties;
			this.structuredType = ((base.Type == null) ? null : base.Type.AsStructured());
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00024136 File Offset: 0x00022336
		internal ODataEdmStructuredValue(ODataComplexValue complexValue)
			: base(complexValue.GetEdmType())
		{
			this.properties = complexValue.Properties;
			this.structuredType = ((base.Type == null) ? null : base.Type.AsStructured());
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0002431C File Offset: 0x0002251C
		public IEnumerable<IEdmPropertyValue> PropertyValues
		{
			get
			{
				if (this.properties != null)
				{
					foreach (ODataProperty property in this.properties)
					{
						yield return property.GetEdmPropertyValue(this.structuredType);
					}
				}
				yield break;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00024339 File Offset: 0x00022539
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Structured;
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00024358 File Offset: 0x00022558
		public IEdmPropertyValue FindPropertyValue(string propertyName)
		{
			ODataProperty odataProperty = ((this.properties == null) ? null : this.properties.Where((ODataProperty p) => p.Name == propertyName).FirstOrDefault<ODataProperty>());
			if (odataProperty == null)
			{
				return null;
			}
			return odataProperty.GetEdmPropertyValue(this.structuredType);
		}

		// Token: 0x04000400 RID: 1024
		private readonly IEnumerable<ODataProperty> properties;

		// Token: 0x04000401 RID: 1025
		private readonly IEdmStructuredTypeReference structuredType;
	}
}
