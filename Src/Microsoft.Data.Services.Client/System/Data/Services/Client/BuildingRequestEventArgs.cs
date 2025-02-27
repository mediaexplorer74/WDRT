﻿using System;
using System.Collections.Generic;

namespace System.Data.Services.Client
{
	// Token: 0x0200002D RID: 45
	public class BuildingRequestEventArgs : EventArgs
	{
		// Token: 0x0600014E RID: 334 RVA: 0x0000804D File Offset: 0x0000624D
		internal BuildingRequestEventArgs(string method, Uri requestUri, HeaderCollection headers, Descriptor descriptor, HttpStack httpStack)
		{
			this.Method = method;
			this.RequestUri = requestUri;
			this.HeaderCollection = headers ?? new HeaderCollection();
			this.ClientHttpStack = httpStack;
			this.Descriptor = descriptor;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00008083 File Offset: 0x00006283
		// (set) Token: 0x06000150 RID: 336 RVA: 0x0000808B File Offset: 0x0000628B
		public string Method { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00008094 File Offset: 0x00006294
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000809C File Offset: 0x0000629C
		public Uri RequestUri
		{
			get
			{
				return this.requestUri;
			}
			set
			{
				this.requestUri = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000080A5 File Offset: 0x000062A5
		public IDictionary<string, string> Headers
		{
			get
			{
				return this.HeaderCollection.UnderlyingDictionary;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000080B2 File Offset: 0x000062B2
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000080BA File Offset: 0x000062BA
		public Descriptor Descriptor { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000080C3 File Offset: 0x000062C3
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000080CB File Offset: 0x000062CB
		internal HttpStack ClientHttpStack { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000080D4 File Offset: 0x000062D4
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000080DC File Offset: 0x000062DC
		internal HeaderCollection HeaderCollection { get; private set; }

		// Token: 0x0600015A RID: 346 RVA: 0x000080E5 File Offset: 0x000062E5
		internal BuildingRequestEventArgs Clone()
		{
			return new BuildingRequestEventArgs(this.Method, this.RequestUri, this.HeaderCollection, this.Descriptor, this.ClientHttpStack);
		}

		// Token: 0x040001E0 RID: 480
		private Uri requestUri;
	}
}
