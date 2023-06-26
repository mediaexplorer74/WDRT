using System;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x02000142 RID: 322
	internal class KnownHttpVerb
	{
		// Token: 0x06000B58 RID: 2904 RVA: 0x0003DF1C File Offset: 0x0003C11C
		internal KnownHttpVerb(string name, bool requireContentBody, bool contentBodyNotAllowed, bool connectRequest, bool expectNoContentResponse)
		{
			this.Name = name;
			this.RequireContentBody = requireContentBody;
			this.ContentBodyNotAllowed = contentBodyNotAllowed;
			this.ConnectRequest = connectRequest;
			this.ExpectNoContentResponse = expectNoContentResponse;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0003DF4C File Offset: 0x0003C14C
		static KnownHttpVerb()
		{
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Get.Name] = KnownHttpVerb.Get;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Connect.Name] = KnownHttpVerb.Connect;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Head.Name] = KnownHttpVerb.Head;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Put.Name] = KnownHttpVerb.Put;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Post.Name] = KnownHttpVerb.Post;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.MkCol.Name] = KnownHttpVerb.MkCol;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0003E070 File Offset: 0x0003C270
		public bool Equals(KnownHttpVerb verb)
		{
			return this == verb || string.Compare(this.Name, verb.Name, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0003E090 File Offset: 0x0003C290
		public static KnownHttpVerb Parse(string name)
		{
			KnownHttpVerb knownHttpVerb = KnownHttpVerb.NamedHeaders[name] as KnownHttpVerb;
			if (knownHttpVerb == null)
			{
				knownHttpVerb = new KnownHttpVerb(name, false, false, false, false);
			}
			return knownHttpVerb;
		}

		// Token: 0x040010C2 RID: 4290
		internal string Name;

		// Token: 0x040010C3 RID: 4291
		internal bool RequireContentBody;

		// Token: 0x040010C4 RID: 4292
		internal bool ContentBodyNotAllowed;

		// Token: 0x040010C5 RID: 4293
		internal bool ConnectRequest;

		// Token: 0x040010C6 RID: 4294
		internal bool ExpectNoContentResponse;

		// Token: 0x040010C7 RID: 4295
		private static ListDictionary NamedHeaders = new ListDictionary(CaseInsensitiveAscii.StaticInstance);

		// Token: 0x040010C8 RID: 4296
		internal static KnownHttpVerb Get = new KnownHttpVerb("GET", false, true, false, false);

		// Token: 0x040010C9 RID: 4297
		internal static KnownHttpVerb Connect = new KnownHttpVerb("CONNECT", false, true, true, false);

		// Token: 0x040010CA RID: 4298
		internal static KnownHttpVerb Head = new KnownHttpVerb("HEAD", false, true, false, true);

		// Token: 0x040010CB RID: 4299
		internal static KnownHttpVerb Put = new KnownHttpVerb("PUT", true, false, false, false);

		// Token: 0x040010CC RID: 4300
		internal static KnownHttpVerb Post = new KnownHttpVerb("POST", true, false, false, false);

		// Token: 0x040010CD RID: 4301
		internal static KnownHttpVerb MkCol = new KnownHttpVerb("MKCOL", false, false, false, false);
	}
}
