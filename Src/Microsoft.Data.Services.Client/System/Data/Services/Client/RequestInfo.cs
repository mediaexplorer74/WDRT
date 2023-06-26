using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using System.Data.Services.Common;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.Data.OData;

namespace System.Data.Services.Client
{
	// Token: 0x02000086 RID: 134
	internal class RequestInfo
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x000138F6 File Offset: 0x00011AF6
		internal RequestInfo(DataServiceContext context, bool isContinuation)
			: this(context)
		{
			this.IsContinuation = isContinuation;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00013908 File Offset: 0x00011B08
		internal RequestInfo(DataServiceContext context)
		{
			this.Context = context;
			this.WriteHelper = new ODataMessageWritingHelper(this);
			this.typeResolver = new TypeResolver(context.Model, new Func<string, Type>(context.ResolveTypeFromName), new Func<Type, string>(context.ResolveNameFromType), context.Format.ServiceModel);
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00013962 File Offset: 0x00011B62
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0001396A File Offset: 0x00011B6A
		internal ODataMessageWritingHelper WriteHelper { get; private set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00013973 File Offset: 0x00011B73
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x0001397B File Offset: 0x00011B7B
		internal DataServiceContext Context { get; private set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00013984 File Offset: 0x00011B84
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0001398C File Offset: 0x00011B8C
		internal bool IsContinuation { get; private set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00013995 File Offset: 0x00011B95
		internal Uri TypeScheme
		{
			get
			{
				return this.Context.TypeScheme;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x000139A2 File Offset: 0x00011BA2
		internal string DataNamespace
		{
			get
			{
				return this.Context.DataNamespace;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x000139AF File Offset: 0x00011BAF
		internal DataServiceClientConfigurations Configurations
		{
			get
			{
				return this.Context.Configurations;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x000139BC File Offset: 0x00011BBC
		internal EntityTracker EntityTracker
		{
			get
			{
				return this.Context.EntityTracker;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x000139C9 File Offset: 0x00011BC9
		internal bool HasWritingEventHandlers
		{
			get
			{
				return this.Context.HasWritingEntityHandlers;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x000139D6 File Offset: 0x00011BD6
		internal bool IgnoreResourceNotFoundException
		{
			get
			{
				return this.Context.IgnoreResourceNotFoundException;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x000139E3 File Offset: 0x00011BE3
		internal bool HasResolveName
		{
			get
			{
				return this.Context.ResolveName != null;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x000139F8 File Offset: 0x00011BF8
		internal bool IsUserSuppliedResolver
		{
			get
			{
				MethodInfo method = this.Context.ResolveName.Method;
				GeneratedCodeAttribute generatedCodeAttribute = method.GetCustomAttributes(false).OfType<GeneratedCodeAttribute>().FirstOrDefault<GeneratedCodeAttribute>();
				return generatedCodeAttribute == null || generatedCodeAttribute.Tool != "System.Data.Services.Design";
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00013A3D File Offset: 0x00011C3D
		internal UriResolver BaseUriResolver
		{
			get
			{
				return this.Context.BaseUriResolver;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00013A4A File Offset: 0x00011C4A
		internal DataServiceResponsePreference AddAndUpdateResponsePreference
		{
			get
			{
				return this.Context.AddAndUpdateResponsePreference;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00013A57 File Offset: 0x00011C57
		internal Version MaxProtocolVersionAsVersion
		{
			get
			{
				return this.Context.MaxProtocolVersionAsVersion;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00013A64 File Offset: 0x00011C64
		internal bool HasSendingRequestEventHandlers
		{
			get
			{
				return this.Context.HasSendingRequestEventHandlers;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00013A71 File Offset: 0x00011C71
		internal bool HasSendingRequest2EventHandlers
		{
			get
			{
				return this.Context.HasSendingRequest2EventHandlers;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00013A7E File Offset: 0x00011C7E
		internal bool UserModifiedRequestInBuildingRequest
		{
			get
			{
				return this.Context.HasBuildingRequestEventHandlers;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00013A8B File Offset: 0x00011C8B
		internal ICredentials Credentials
		{
			get
			{
				return this.Context.Credentials;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00013A98 File Offset: 0x00011C98
		internal int Timeout
		{
			get
			{
				return this.Context.Timeout;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00013AA5 File Offset: 0x00011CA5
		internal bool UsePostTunneling
		{
			get
			{
				return this.Context.UsePostTunneling;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00013AB2 File Offset: 0x00011CB2
		internal ClientEdmModel Model
		{
			get
			{
				return this.Context.Model;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00013ABF File Offset: 0x00011CBF
		internal DataServiceClientFormat Format
		{
			get
			{
				return this.Context.Format;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00013ACC File Offset: 0x00011CCC
		internal TypeResolver TypeResolver
		{
			get
			{
				return this.typeResolver;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00013AD4 File Offset: 0x00011CD4
		internal DataServiceUrlConventions UrlConventions
		{
			get
			{
				return this.Context.UrlConventions;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00013AE1 File Offset: 0x00011CE1
		internal HttpStack HttpStack
		{
			get
			{
				return this.Context.HttpStack;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00013AEE File Offset: 0x00011CEE
		internal bool UseDefaultCredentials
		{
			get
			{
				return this.Context.UseDefaultCredentials;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00013AFB File Offset: 0x00011CFB
		internal IODataResponseMessage GetSyncronousResponse(ODataRequestMessageWrapper request, bool handleWebException)
		{
			return this.Context.GetSyncronousResponse(request, handleWebException);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00013B0A File Offset: 0x00011D0A
		internal IODataResponseMessage EndGetResponse(ODataRequestMessageWrapper request, IAsyncResult asyncResult)
		{
			return this.Context.EndGetResponse(request, asyncResult);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00013B1C File Offset: 0x00011D1C
		internal string GetServerTypeName(EntityDescriptor descriptor)
		{
			string text;
			if (this.HasResolveName)
			{
				Type type = descriptor.Entity.GetType();
				if (this.IsUserSuppliedResolver)
				{
					text = this.ResolveNameFromType(type) ?? descriptor.GetLatestServerTypeName();
				}
				else
				{
					text = descriptor.GetLatestServerTypeName() ?? this.ResolveNameFromType(type);
				}
			}
			else
			{
				text = descriptor.GetLatestServerTypeName();
			}
			return text;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00013B78 File Offset: 0x00011D78
		internal string GetServerTypeName(ClientTypeAnnotation clientTypeAnnotation)
		{
			return this.ResolveNameFromType(clientTypeAnnotation.ElementType);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00013B94 File Offset: 0x00011D94
		internal string InferServerTypeNameFromServerModel(EntityDescriptor descriptor)
		{
			if (descriptor.EntitySetName != null)
			{
				string text;
				if (this.TypeResolver.TryResolveEntitySetBaseTypeName(descriptor.EntitySetName, out text))
				{
					return text;
				}
			}
			else if (descriptor.IsDeepInsert)
			{
				string text2 = this.GetServerTypeName(descriptor.ParentForInsert);
				if (text2 == null)
				{
					text2 = this.InferServerTypeNameFromServerModel(descriptor.ParentForInsert);
				}
				string text3;
				if (this.TypeResolver.TryResolveNavigationTargetTypeName(text2, descriptor.ParentPropertyForInsert, out text3))
				{
					return text3;
				}
			}
			return null;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00013BFE File Offset: 0x00011DFE
		internal void FireWritingEntityEvent(object element, XElement data, Uri baseUri)
		{
			this.Context.FireWritingAtomEntityEvent(element, data, baseUri);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00013C0E File Offset: 0x00011E0E
		internal string ResolveNameFromType(Type type)
		{
			return this.Context.ResolveNameFromType(type);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00013C1C File Offset: 0x00011E1C
		internal ResponseInfo GetDeserializationInfo(MergeOption? mergeOption)
		{
			return new ResponseInfo(this, (mergeOption != null) ? mergeOption.Value : this.Context.MergeOption);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00013C41 File Offset: 0x00011E41
		internal ResponseInfo GetDeserializationInfoForLoadProperty(MergeOption? mergeOption, EntityDescriptor entityDescriptor, ClientPropertyAnnotation property)
		{
			return new LoadPropertyResponseInfo(this, (mergeOption != null) ? mergeOption.Value : this.Context.MergeOption, entityDescriptor, property);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00013C68 File Offset: 0x00011E68
		internal InvalidOperationException ValidateResponseVersion(Version responseVersion)
		{
			if (responseVersion != null && responseVersion > this.Context.MaxProtocolVersionAsVersion)
			{
				string text = Strings.Context_ResponseVersionIsBiggerThanProtocolVersion(responseVersion.ToString(), this.Context.MaxProtocolVersion.ToString());
				return Error.InvalidOperation(text);
			}
			return null;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00013CBA File Offset: 0x00011EBA
		internal void FireSendingRequest(SendingRequestEventArgs eventArgs)
		{
			this.Context.FireSendingRequest(eventArgs);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00013CC8 File Offset: 0x00011EC8
		internal void FireSendingRequest2(SendingRequest2EventArgs eventArgs)
		{
			this.Context.FireSendingRequest2(eventArgs);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00013CD8 File Offset: 0x00011ED8
		internal DataServiceClientRequestMessage CreateRequestMessage(BuildingRequestEventArgs requestMessageArgs)
		{
			IDictionary<string, string> underlyingDictionary = requestMessageArgs.HeaderCollection.UnderlyingDictionary;
			if (this.UsePostTunneling)
			{
				bool flag = false;
				if (string.CompareOrdinal("GET", requestMessageArgs.Method) != 0 && string.CompareOrdinal("POST", requestMessageArgs.Method) != 0)
				{
					flag = true;
				}
				if (flag)
				{
					underlyingDictionary["X-HTTP-Method"] = requestMessageArgs.Method;
				}
				if (string.CompareOrdinal("DELETE", requestMessageArgs.Method) == 0)
				{
					underlyingDictionary["Content-Length"] = "0";
				}
			}
			DataServiceClientRequestMessageArgs dataServiceClientRequestMessageArgs = new DataServiceClientRequestMessageArgs(requestMessageArgs.Method, requestMessageArgs.RequestUri, this.UseDefaultCredentials, this.UsePostTunneling, underlyingDictionary);
			DataServiceClientRequestMessage dataServiceClientRequestMessage;
			if (this.Configurations.RequestPipeline.OnMessageCreating != null)
			{
				dataServiceClientRequestMessage = this.Configurations.RequestPipeline.OnMessageCreating(dataServiceClientRequestMessageArgs);
				if (dataServiceClientRequestMessage == null)
				{
					throw Error.InvalidOperation(Strings.Context_OnMessageCreatingReturningNull);
				}
			}
			else
			{
				dataServiceClientRequestMessage = new HttpWebRequestMessage(dataServiceClientRequestMessageArgs, this);
			}
			return dataServiceClientRequestMessage;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00013DB6 File Offset: 0x00011FB6
		internal BuildingRequestEventArgs CreateRequestArgsAndFireBuildingRequest(string method, Uri requestUri, HeaderCollection headers, HttpStack httpStack, Descriptor descriptor)
		{
			return this.Context.CreateRequestArgsAndFireBuildingRequest(method, requestUri, headers, httpStack, descriptor);
		}

		// Token: 0x040002EB RID: 747
		private readonly TypeResolver typeResolver;
	}
}
