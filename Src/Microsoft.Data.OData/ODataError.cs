using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.Data.OData
{
	// Token: 0x02000291 RID: 657
	[DebuggerDisplay("{ErrorCode}: {Message}")]
	[Serializable]
	public sealed class ODataError : ODataAnnotatable
	{
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x000513E5 File Offset: 0x0004F5E5
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x000513ED File Offset: 0x0004F5ED
		public string ErrorCode { get; set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x000513F6 File Offset: 0x0004F5F6
		// (set) Token: 0x06001627 RID: 5671 RVA: 0x000513FE File Offset: 0x0004F5FE
		public string Message { get; set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x00051407 File Offset: 0x0004F607
		// (set) Token: 0x06001629 RID: 5673 RVA: 0x0005140F File Offset: 0x0004F60F
		public string MessageLanguage { get; set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x00051418 File Offset: 0x0004F618
		// (set) Token: 0x0600162B RID: 5675 RVA: 0x00051420 File Offset: 0x0004F620
		public ODataInnerError InnerError { get; set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x00051429 File Offset: 0x0004F629
		// (set) Token: 0x0600162D RID: 5677 RVA: 0x00051431 File Offset: 0x0004F631
		public ICollection<ODataInstanceAnnotation> InstanceAnnotations
		{
			get
			{
				return base.GetInstanceAnnotations();
			}
			set
			{
				base.SetInstanceAnnotations(value);
			}
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0005143A File Offset: 0x0004F63A
		internal override void VerifySetAnnotation(object annotation)
		{
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00051454 File Offset: 0x0004F654
		internal IEnumerable<ODataInstanceAnnotation> GetInstanceAnnotationsForWriting()
		{
			if (this.InstanceAnnotations.Count > 0)
			{
				return this.InstanceAnnotations;
			}
			InstanceAnnotationCollection annotation = base.GetAnnotation<InstanceAnnotationCollection>();
			if (annotation != null && annotation.Count > 0)
			{
				return annotation.Select((KeyValuePair<string, ODataValue> ia) => new ODataInstanceAnnotation(ia.Key, ia.Value));
			}
			return Enumerable.Empty<ODataInstanceAnnotation>();
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x000514B4 File Offset: 0x0004F6B4
		internal void AddInstanceAnnotationForReading(string instanceAnnotationName, object instanceAnnotationValue)
		{
			ODataValue odataValue = instanceAnnotationValue.ToODataValue();
			base.GetOrCreateAnnotation<InstanceAnnotationCollection>().Add(instanceAnnotationName, odataValue);
			this.InstanceAnnotations.Add(new ODataInstanceAnnotation(instanceAnnotationName, odataValue));
		}
	}
}
