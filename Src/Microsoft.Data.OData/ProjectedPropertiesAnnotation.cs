using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x02000242 RID: 578
	public sealed class ProjectedPropertiesAnnotation
	{
		// Token: 0x0600127A RID: 4730 RVA: 0x000459DB File Offset: 0x00043BDB
		public ProjectedPropertiesAnnotation(IEnumerable<string> projectedPropertyNames)
		{
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<string>>(projectedPropertyNames, "projectedPropertyNames");
			this.projectedProperties = new HashSet<string>(projectedPropertyNames, StringComparer.Ordinal);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x000459FF File Offset: 0x00043BFF
		internal ProjectedPropertiesAnnotation()
		{
			this.projectedProperties = new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x00045A17 File Offset: 0x00043C17
		internal static ProjectedPropertiesAnnotation EmptyProjectedPropertiesInstance
		{
			get
			{
				return ProjectedPropertiesAnnotation.emptyProjectedPropertiesMarker;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x00045A1E File Offset: 0x00043C1E
		internal static ProjectedPropertiesAnnotation AllProjectedPropertiesInstance
		{
			get
			{
				return ProjectedPropertiesAnnotation.allProjectedPropertiesMarker;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x00045A25 File Offset: 0x00043C25
		internal IEnumerable<string> ProjectedProperties
		{
			get
			{
				return this.projectedProperties;
			}
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00045A2D File Offset: 0x00043C2D
		internal bool IsPropertyProjected(string propertyName)
		{
			return this.projectedProperties.Contains(propertyName);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00045A3B File Offset: 0x00043C3B
		internal void Add(string propertyName)
		{
			if (object.ReferenceEquals(ProjectedPropertiesAnnotation.AllProjectedPropertiesInstance, this))
			{
				return;
			}
			if (!this.projectedProperties.Contains(propertyName))
			{
				this.projectedProperties.Add(propertyName);
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00045A66 File Offset: 0x00043C66
		internal void Remove(string propertyName)
		{
			this.projectedProperties.Remove(propertyName);
		}

		// Token: 0x040006AF RID: 1711
		internal const string StarSegment = "*";

		// Token: 0x040006B0 RID: 1712
		private static readonly ProjectedPropertiesAnnotation emptyProjectedPropertiesMarker = new ProjectedPropertiesAnnotation(new string[0]);

		// Token: 0x040006B1 RID: 1713
		private static readonly ProjectedPropertiesAnnotation allProjectedPropertiesMarker = new ProjectedPropertiesAnnotation(new string[] { "*" });

		// Token: 0x040006B2 RID: 1714
		private readonly HashSet<string> projectedProperties;
	}
}
