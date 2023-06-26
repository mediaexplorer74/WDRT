using System;
using System.Data.Services.Common;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000087 RID: 135
	internal class ResponseInfo
	{
		// Token: 0x060004CE RID: 1230 RVA: 0x00013DCA File Offset: 0x00011FCA
		internal ResponseInfo(RequestInfo requestInfo, MergeOption mergeOption)
		{
			this.requestInfo = requestInfo;
			this.mergeOption = mergeOption;
			this.ReadHelper = new ODataMessageReadingHelper(this);
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00013DEC File Offset: 0x00011FEC
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00013DF4 File Offset: 0x00011FF4
		public ODataMessageReadingHelper ReadHelper { get; private set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00013DFD File Offset: 0x00011FFD
		internal bool IsContinuation
		{
			get
			{
				return this.requestInfo.IsContinuation;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00013E0A File Offset: 0x0001200A
		internal Uri TypeScheme
		{
			get
			{
				return this.Context.TypeScheme;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00013E17 File Offset: 0x00012017
		internal string DataNamespace
		{
			get
			{
				return this.Context.DataNamespace;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00013E24 File Offset: 0x00012024
		internal MergeOption MergeOption
		{
			get
			{
				return this.mergeOption;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00013E2C File Offset: 0x0001202C
		internal bool IgnoreMissingProperties
		{
			get
			{
				return this.Context.IgnoreMissingProperties;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00013E39 File Offset: 0x00012039
		internal bool AutoNullPropagation
		{
			get
			{
				return this.Context.AutoNullPropagation;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00013E46 File Offset: 0x00012046
		internal ODataUndeclaredPropertyBehaviorKinds UndeclaredPropertyBehaviorKinds
		{
			get
			{
				if (this.Context.UndeclaredPropertyBehavior == UndeclaredPropertyBehavior.None)
				{
					if (!this.Context.IgnoreMissingProperties)
					{
						return ODataUndeclaredPropertyBehaviorKinds.None;
					}
					return ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty;
				}
				else
				{
					if (this.Context.UndeclaredPropertyBehavior == UndeclaredPropertyBehavior.Ignore)
					{
						return ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty;
					}
					return ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty;
				}
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00013E77 File Offset: 0x00012077
		internal EntityTracker EntityTracker
		{
			get
			{
				return this.Context.EntityTracker;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00013E84 File Offset: 0x00012084
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00013E91 File Offset: 0x00012091
		internal bool ApplyingChanges
		{
			get
			{
				return this.Context.ApplyingChanges;
			}
			set
			{
				this.Context.ApplyingChanges = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00013E9F File Offset: 0x0001209F
		internal TypeResolver TypeResolver
		{
			get
			{
				return this.requestInfo.TypeResolver;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00013EAC File Offset: 0x000120AC
		internal UriResolver BaseUriResolver
		{
			get
			{
				return this.requestInfo.BaseUriResolver;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00013EB9 File Offset: 0x000120B9
		internal DataServiceProtocolVersion MaxProtocolVersion
		{
			get
			{
				return this.Context.MaxProtocolVersion;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00013EC6 File Offset: 0x000120C6
		internal ClientEdmModel Model
		{
			get
			{
				return this.requestInfo.Model;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00013ED3 File Offset: 0x000120D3
		internal DataServiceContext Context
		{
			get
			{
				return this.requestInfo.Context;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00013EE0 File Offset: 0x000120E0
		internal DataServiceClientResponsePipelineConfiguration ResponsePipeline
		{
			get
			{
				return this.requestInfo.Configurations.ResponsePipeline;
			}
		}

		// Token: 0x040002EF RID: 751
		private readonly RequestInfo requestInfo;

		// Token: 0x040002F0 RID: 752
		private readonly MergeOption mergeOption;
	}
}
