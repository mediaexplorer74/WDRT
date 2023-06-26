using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Defines GUID identifiers that correspond to the standard set of tool windows that are available in the design environment.</summary>
	// Token: 0x02000600 RID: 1536
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class StandardToolWindows
	{
		/// <summary>Gets the GUID for the object browser. This field is read-only.</summary>
		// Token: 0x04002B4D RID: 11085
		public static readonly Guid ObjectBrowser = new Guid("{970d9861-ee83-11d0-a778-00a0c91110c3}");

		/// <summary>Gets the GUID for the output window. This field is read-only.</summary>
		// Token: 0x04002B4E RID: 11086
		public static readonly Guid OutputWindow = new Guid("{34e76e81-ee4a-11d0-ae2e-00a0c90fffc3}");

		/// <summary>Gets the GUID for the solution explorer. This field is read-only.</summary>
		// Token: 0x04002B4F RID: 11087
		public static readonly Guid ProjectExplorer = new Guid("{3ae79031-e1bc-11d0-8f78-00a0c9110057}");

		/// <summary>Gets the GUID for the Properties window. This field is read-only.</summary>
		// Token: 0x04002B50 RID: 11088
		public static readonly Guid PropertyBrowser = new Guid("{eefa5220-e298-11d0-8f78-00a0c9110057}");

		/// <summary>Gets the GUID for the related links frame. This field is read-only.</summary>
		// Token: 0x04002B51 RID: 11089
		public static readonly Guid RelatedLinks = new Guid("{66dba47c-61df-11d2-aa79-00c04f990343}");

		/// <summary>Gets the GUID for the server explorer. This field is read-only.</summary>
		// Token: 0x04002B52 RID: 11090
		public static readonly Guid ServerExplorer = new Guid("{74946827-37a0-11d2-a273-00c04f8ef4ff}");

		/// <summary>Gets the GUID for the task list. This field is read-only.</summary>
		// Token: 0x04002B53 RID: 11091
		public static readonly Guid TaskList = new Guid("{4a9b7e51-aa16-11d0-a8c5-00a0c921a4d2}");

		/// <summary>Gets the GUID for the Toolbox. This field is read-only.</summary>
		// Token: 0x04002B54 RID: 11092
		public static readonly Guid Toolbox = new Guid("{b1e99781-ab81-11d0-b683-00aa00a3ee26}");
	}
}
