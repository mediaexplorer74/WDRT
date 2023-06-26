using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;

namespace System.Resources
{
	/// <summary>Represents all resources in an XML resource (.resx) file.</summary>
	// Token: 0x020000F0 RID: 240
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class ResXResourceSet : ResourceSet
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Resources.ResXResourceSet" /> class using the system default <see cref="T:System.Resources.ResXResourceReader" /> that opens and reads resources from the specified file.</summary>
		/// <param name="fileName">The name of the file to read resources from.</param>
		// Token: 0x06000391 RID: 913 RVA: 0x0000B2B9 File Offset: 0x000094B9
		public ResXResourceSet(string fileName)
		{
			this.Reader = new ResXResourceReader(fileName);
			this.Table = new Hashtable();
			this.ReadResources();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceSet" /> class using the system default <see cref="T:System.Resources.ResXResourceReader" /> to read resources from the specified stream.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> of resources to be read. The stream should refer to an existing resource file.</param>
		// Token: 0x06000392 RID: 914 RVA: 0x0000B2DE File Offset: 0x000094DE
		public ResXResourceSet(Stream stream)
		{
			this.Reader = new ResXResourceReader(stream);
			this.Table = new Hashtable();
			this.ReadResources();
		}

		/// <summary>Returns the preferred resource reader class for this kind of <see cref="T:System.Resources.ResXResourceSet" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the preferred resource reader for this kind of <see cref="T:System.Resources.ResXResourceSet" />.</returns>
		// Token: 0x06000393 RID: 915 RVA: 0x0000B303 File Offset: 0x00009503
		public override Type GetDefaultReader()
		{
			return typeof(ResXResourceReader);
		}

		/// <summary>Returns the preferred resource writer class for this kind of <see cref="T:System.Resources.ResXResourceSet" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the preferred resource writer for this kind of <see cref="T:System.Resources.ResXResourceSet" />.</returns>
		// Token: 0x06000394 RID: 916 RVA: 0x0000B30F File Offset: 0x0000950F
		public override Type GetDefaultWriter()
		{
			return typeof(ResXResourceWriter);
		}
	}
}
