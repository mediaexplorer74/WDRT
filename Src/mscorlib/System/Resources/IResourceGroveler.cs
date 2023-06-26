using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System.Resources
{
	// Token: 0x0200038B RID: 907
	internal interface IResourceGroveler
	{
		// Token: 0x06002D04 RID: 11524
		ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark);

		// Token: 0x06002D05 RID: 11525
		bool HasNeutralResources(CultureInfo culture, string defaultResName);
	}
}
