using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Client.Metadata;
using System.IO;
using System.Text;

namespace System.Data.Services.Client
{
	// Token: 0x02000069 RID: 105
	internal class LoadPropertyResult : QueryResult
	{
		// Token: 0x0600038F RID: 911 RVA: 0x0000F75C File Offset: 0x0000D95C
		internal LoadPropertyResult(object entity, string propertyName, DataServiceContext context, ODataRequestMessageWrapper request, AsyncCallback callback, object state, DataServiceRequest dataServiceRequest, ProjectionPlan plan, bool isContinuation)
			: base(context, "LoadProperty", dataServiceRequest, request, new RequestInfo(context, isContinuation), callback, state)
		{
			this.entity = entity;
			this.propertyName = propertyName;
			this.plan = plan;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000F790 File Offset: 0x0000D990
		internal QueryOperationResponse LoadProperty()
		{
			MaterializeAtom materializeAtom = null;
			DataServiceContext dataServiceContext = (DataServiceContext)this.Source;
			ClientEdmModel model = dataServiceContext.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(this.entity.GetType()));
			EntityDescriptor entityDescriptor = dataServiceContext.GetEntityDescriptor(this.entity);
			if (EntityStates.Added == entityDescriptor.State)
			{
				throw Error.InvalidOperation(Strings.Context_NoLoadWithInsertEnd);
			}
			ClientPropertyAnnotation property = clientTypeAnnotation.GetProperty(this.propertyName, false);
			Type type = property.EntityCollectionItemType ?? property.NullablePropertyType;
			QueryOperationResponse responseWithType;
			try
			{
				if (clientTypeAnnotation.MediaDataMember == property)
				{
					materializeAtom = this.ReadPropertyFromRawData(property);
				}
				else
				{
					materializeAtom = this.ReadPropertyFromAtom(property);
				}
				responseWithType = base.GetResponseWithType(materializeAtom, type);
			}
			catch (InvalidOperationException ex)
			{
				QueryOperationResponse responseWithType2 = base.GetResponseWithType(materializeAtom, type);
				if (responseWithType2 != null)
				{
					responseWithType2.Error = ex;
					throw new DataServiceQueryException(Strings.DataServiceException_GeneralError, ex, responseWithType2);
				}
				throw;
			}
			return responseWithType;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000F878 File Offset: 0x0000DA78
		protected override ResponseInfo CreateResponseInfo()
		{
			DataServiceContext dataServiceContext = (DataServiceContext)this.Source;
			ClientEdmModel model = dataServiceContext.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(this.entity.GetType()));
			return this.RequestInfo.GetDeserializationInfoForLoadProperty(null, dataServiceContext.GetEntityDescriptor(this.entity), clientTypeAnnotation.GetProperty(this.propertyName, false));
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000F8E0 File Offset: 0x0000DAE0
		private static byte[] ReadByteArrayWithContentLength(Stream responseStream, int totalLength)
		{
			byte[] array = new byte[totalLength];
			int num;
			for (int i = 0; i < totalLength; i += num)
			{
				num = responseStream.Read(array, i, totalLength - i);
				if (num <= 0)
				{
					throw Error.InvalidOperation(Strings.Context_UnexpectedZeroRawRead);
				}
			}
			return array;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000F91C File Offset: 0x0000DB1C
		private static byte[] ReadByteArrayChunked(Stream responseStream)
		{
			byte[] array = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] array2 = new byte[4096];
				int num = 0;
				int num2;
				for (;;)
				{
					num2 = responseStream.Read(array2, 0, array2.Length);
					if (num2 <= 0)
					{
						break;
					}
					memoryStream.Write(array2, 0, num2);
					num += num2;
				}
				array = new byte[num];
				memoryStream.Position = 0L;
				num2 = memoryStream.Read(array, 0, array.Length);
			}
			return array;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000F99C File Offset: 0x0000DB9C
		private MaterializeAtom ReadPropertyFromAtom(ClientPropertyAnnotation property)
		{
			DataServiceContext dataServiceContext = (DataServiceContext)this.Source;
			bool applyingChanges = dataServiceContext.ApplyingChanges;
			MaterializeAtom materializeAtom;
			try
			{
				dataServiceContext.ApplyingChanges = true;
				Type type = (property.IsEntityCollection ? property.EntityCollectionItemType : property.NullablePropertyType);
				IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { type }));
				DataServiceQueryContinuation dataServiceQueryContinuation = null;
				using (MaterializeAtom materializer = base.GetMaterializer(this.plan))
				{
					foreach (object obj in materializer)
					{
						if (property.IsEntityCollection)
						{
							list.Add(obj);
						}
						else if (property.IsPrimitiveOrComplexCollection)
						{
							object obj2 = property.GetValue(this.entity);
							if (obj2 == null)
							{
								obj2 = Activator.CreateInstance(obj.GetType());
								property.SetValue(this.entity, obj2, this.propertyName, false);
							}
							else
							{
								property.ClearBackingICollectionInstance(obj2);
							}
							foreach (object obj3 in ((IEnumerable)obj))
							{
								property.AddValueToBackingICollectionInstance(obj2, obj3);
							}
							list.Add(obj2);
						}
						else
						{
							property.SetValue(this.entity, obj, this.propertyName, false);
							list.Add(obj);
						}
					}
					dataServiceQueryContinuation = materializer.GetContinuation(null);
				}
				materializeAtom = MaterializeAtom.CreateWrapper(dataServiceContext, list, dataServiceQueryContinuation);
			}
			finally
			{
				dataServiceContext.ApplyingChanges = applyingChanges;
			}
			return materializeAtom;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		private MaterializeAtom ReadPropertyFromRawData(ClientPropertyAnnotation property)
		{
			DataServiceContext dataServiceContext = (DataServiceContext)this.Source;
			bool applyingChanges = dataServiceContext.ApplyingChanges;
			MaterializeAtom materializeAtom;
			try
			{
				dataServiceContext.ApplyingChanges = true;
				string text = null;
				Encoding encoding = null;
				Type type = property.EntityCollectionItemType ?? property.NullablePropertyType;
				IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { type }));
				ContentTypeUtil.ReadContentType(base.ContentType, out text, out encoding);
				using (Stream responseStream = base.GetResponseStream())
				{
					if (property.PropertyType == typeof(byte[]))
					{
						int num = checked((int)base.ContentLength);
						byte[] array;
						if (num >= 0)
						{
							array = LoadPropertyResult.ReadByteArrayWithContentLength(responseStream, num);
						}
						else
						{
							array = LoadPropertyResult.ReadByteArrayChunked(responseStream);
						}
						list.Add(array);
						property.SetValue(this.entity, array, this.propertyName, false);
					}
					else
					{
						StreamReader streamReader = new StreamReader(responseStream, encoding);
						object obj = ((property.PropertyType == typeof(string)) ? streamReader.ReadToEnd() : ClientConvert.ChangeType(streamReader.ReadToEnd(), property.PropertyType));
						list.Add(obj);
						property.SetValue(this.entity, obj, this.propertyName, false);
					}
				}
				if (property.MimeTypeProperty != null)
				{
					property.MimeTypeProperty.SetValue(this.entity, text, null, false);
				}
				materializeAtom = MaterializeAtom.CreateWrapper(dataServiceContext, list);
			}
			finally
			{
				dataServiceContext.ApplyingChanges = applyingChanges;
			}
			return materializeAtom;
		}

		// Token: 0x040002A8 RID: 680
		private readonly object entity;

		// Token: 0x040002A9 RID: 681
		private readonly ProjectionPlan plan;

		// Token: 0x040002AA RID: 682
		private readonly string propertyName;
	}
}
