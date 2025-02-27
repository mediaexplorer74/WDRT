﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200079C RID: 1948
	internal sealed class ObjectReader
	{
		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x0600547A RID: 21626 RVA: 0x0012AED8 File Offset: 0x001290D8
		private SerStack ValueFixupStack
		{
			get
			{
				if (this.valueFixupStack == null)
				{
					this.valueFixupStack = new SerStack("ValueType Fixup Stack");
				}
				return this.valueFixupStack;
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x0600547B RID: 21627 RVA: 0x0012AEF8 File Offset: 0x001290F8
		// (set) Token: 0x0600547C RID: 21628 RVA: 0x0012AF00 File Offset: 0x00129100
		internal object TopObject
		{
			get
			{
				return this.m_topObject;
			}
			set
			{
				this.m_topObject = value;
				if (this.m_objectManager != null)
				{
					this.m_objectManager.TopObject = value;
				}
			}
		}

		// Token: 0x0600547D RID: 21629 RVA: 0x0012AF1D File Offset: 0x0012911D
		internal void SetMethodCall(BinaryMethodCall binaryMethodCall)
		{
			this.bMethodCall = true;
			this.binaryMethodCall = binaryMethodCall;
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x0012AF2D File Offset: 0x0012912D
		internal void SetMethodReturn(BinaryMethodReturn binaryMethodReturn)
		{
			this.bMethodReturn = true;
			this.binaryMethodReturn = binaryMethodReturn;
		}

		// Token: 0x0600547F RID: 21631 RVA: 0x0012AF40 File Offset: 0x00129140
		internal ObjectReader(Stream stream, ISurrogateSelector selector, StreamingContext context, InternalFE formatterEnums, SerializationBinder binder)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", Environment.GetResourceString("ArgumentNull_Stream"));
			}
			this.m_stream = stream;
			this.m_surrogates = selector;
			this.m_context = context;
			this.m_binder = binder;
			if (this.m_binder != null)
			{
				ResourceReader.TypeLimitingDeserializationBinder typeLimitingDeserializationBinder = this.m_binder as ResourceReader.TypeLimitingDeserializationBinder;
				if (typeLimitingDeserializationBinder != null)
				{
					typeLimitingDeserializationBinder.ObjectReader = this;
				}
			}
			this.formatterEnums = formatterEnums;
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x0012AFBC File Offset: 0x001291BC
		[SecurityCritical]
		internal object Deserialize(HeaderHandler handler, __BinaryParser serParser, bool fCheck, bool isCrossAppDomain, IMethodCallMessage methodCallMessage)
		{
			if (serParser == null)
			{
				throw new ArgumentNullException("serParser", Environment.GetResourceString("ArgumentNull_WithParamName", new object[] { serParser }));
			}
			this.bFullDeserialization = false;
			this.TopObject = null;
			this.topId = 0L;
			this.bMethodCall = false;
			this.bMethodReturn = false;
			this.bIsCrossAppDomain = isCrossAppDomain;
			this.bSimpleAssembly = this.formatterEnums.FEassemblyFormat == FormatterAssemblyStyle.Simple;
			if (fCheck)
			{
				CodeAccessPermission.Demand(PermissionType.SecuritySerialization);
			}
			this.handler = handler;
			serParser.Run();
			if (this.bFullDeserialization)
			{
				this.m_objectManager.DoFixups();
			}
			if (!this.bMethodCall && !this.bMethodReturn)
			{
				if (this.TopObject == null)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_TopObject"));
				}
				if (this.HasSurrogate(this.TopObject.GetType()) && this.topId != 0L)
				{
					this.TopObject = this.m_objectManager.GetObject(this.topId);
				}
				if (this.TopObject is IObjectReference)
				{
					this.TopObject = ((IObjectReference)this.TopObject).GetRealObject(this.m_context);
				}
			}
			if (this.bFullDeserialization)
			{
				this.m_objectManager.RaiseDeserializationEvent();
			}
			if (handler != null)
			{
				this.handlerObject = handler(this.headers);
			}
			if (this.bMethodCall)
			{
				object[] array = this.TopObject as object[];
				this.TopObject = this.binaryMethodCall.ReadArray(array, this.handlerObject);
			}
			else if (this.bMethodReturn)
			{
				object[] array2 = this.TopObject as object[];
				this.TopObject = this.binaryMethodReturn.ReadArray(array2, methodCallMessage, this.handlerObject);
			}
			return this.TopObject;
		}

		// Token: 0x06005481 RID: 21633 RVA: 0x0012B164 File Offset: 0x00129364
		[SecurityCritical]
		private bool HasSurrogate(Type t)
		{
			ISurrogateSelector surrogateSelector;
			return this.m_surrogates != null && this.m_surrogates.GetSurrogate(t, this.m_context, out surrogateSelector) != null;
		}

		// Token: 0x06005482 RID: 21634 RVA: 0x0012B192 File Offset: 0x00129392
		[SecurityCritical]
		private void CheckSerializable(Type t)
		{
			if (!t.IsSerializable && !this.HasSurrogate(t))
			{
				throw new SerializationException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Serialization_NonSerType"), t.FullName, t.Assembly.FullName));
			}
		}

		// Token: 0x06005483 RID: 21635 RVA: 0x0012B1D0 File Offset: 0x001293D0
		[SecurityCritical]
		private void InitFullDeserialization()
		{
			this.bFullDeserialization = true;
			this.stack = new SerStack("ObjectReader Object Stack");
			this.m_objectManager = new ObjectManager(this.m_surrogates, this.m_context, false, this.bIsCrossAppDomain);
			if (this.m_formatterConverter == null)
			{
				this.m_formatterConverter = new FormatterConverter();
			}
		}

		// Token: 0x06005484 RID: 21636 RVA: 0x0012B225 File Offset: 0x00129425
		internal object CrossAppDomainArray(int index)
		{
			return this.crossAppDomainArray[index];
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x0012B22F File Offset: 0x0012942F
		[SecurityCritical]
		internal ReadObjectInfo CreateReadObjectInfo(Type objectType)
		{
			return ReadObjectInfo.Create(objectType, this.m_surrogates, this.m_context, this.m_objectManager, this.serObjectInfoInit, this.m_formatterConverter, this.bSimpleAssembly);
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x0012B25C File Offset: 0x0012945C
		[SecurityCritical]
		internal ReadObjectInfo CreateReadObjectInfo(Type objectType, string[] memberNames, Type[] memberTypes)
		{
			return ReadObjectInfo.Create(objectType, memberNames, memberTypes, this.m_surrogates, this.m_context, this.m_objectManager, this.serObjectInfoInit, this.m_formatterConverter, this.bSimpleAssembly);
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x0012B298 File Offset: 0x00129498
		[SecurityCritical]
		internal void Parse(ParseRecord pr)
		{
			switch (pr.PRparseTypeEnum)
			{
			case InternalParseTypeE.SerializedStreamHeader:
				this.ParseSerializedStreamHeader(pr);
				return;
			case InternalParseTypeE.Object:
				this.ParseObject(pr);
				return;
			case InternalParseTypeE.Member:
				this.ParseMember(pr);
				return;
			case InternalParseTypeE.ObjectEnd:
				this.ParseObjectEnd(pr);
				return;
			case InternalParseTypeE.MemberEnd:
				this.ParseMemberEnd(pr);
				return;
			case InternalParseTypeE.SerializedStreamHeaderEnd:
				this.ParseSerializedStreamHeaderEnd(pr);
				return;
			case InternalParseTypeE.Envelope:
			case InternalParseTypeE.EnvelopeEnd:
			case InternalParseTypeE.Body:
			case InternalParseTypeE.BodyEnd:
				return;
			}
			throw new SerializationException(Environment.GetResourceString("Serialization_XMLElement", new object[] { pr.PRname }));
		}

		// Token: 0x06005488 RID: 21640 RVA: 0x0012B338 File Offset: 0x00129538
		private void ParseError(ParseRecord processing, ParseRecord onStack)
		{
			string text = "Serialization_ParseError";
			object[] array = new object[1];
			int num = 0;
			string[] array2 = new string[7];
			array2[0] = onStack.PRname;
			array2[1] = " ";
			int num2 = 2;
			object obj = onStack.PRparseTypeEnum;
			array2[num2] = ((obj != null) ? obj.ToString() : null);
			array2[3] = " ";
			array2[4] = processing.PRname;
			array2[5] = " ";
			int num3 = 6;
			object obj2 = processing.PRparseTypeEnum;
			array2[num3] = ((obj2 != null) ? obj2.ToString() : null);
			array[num] = string.Concat(array2);
			throw new SerializationException(Environment.GetResourceString(text, array));
		}

		// Token: 0x06005489 RID: 21641 RVA: 0x0012B3C6 File Offset: 0x001295C6
		private void ParseSerializedStreamHeader(ParseRecord pr)
		{
			this.stack.Push(pr);
		}

		// Token: 0x0600548A RID: 21642 RVA: 0x0012B3D4 File Offset: 0x001295D4
		private void ParseSerializedStreamHeaderEnd(ParseRecord pr)
		{
			this.stack.Pop();
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x0600548B RID: 21643 RVA: 0x0012B3E2 File Offset: 0x001295E2
		private bool IsRemoting
		{
			get
			{
				return this.bMethodCall || this.bMethodReturn;
			}
		}

		// Token: 0x0600548C RID: 21644 RVA: 0x0012B3F4 File Offset: 0x001295F4
		[SecurityCritical]
		internal void CheckSecurity(ParseRecord pr)
		{
			Type prdtType = pr.PRdtType;
			if (prdtType != null && this.IsRemoting)
			{
				if (typeof(MarshalByRefObject).IsAssignableFrom(prdtType))
				{
					throw new ArgumentException(Environment.GetResourceString("Serialization_MBRAsMBV", new object[] { prdtType.FullName }));
				}
				FormatterServices.CheckTypeSecurity(prdtType, this.formatterEnums.FEsecurityLevel);
			}
		}

		// Token: 0x0600548D RID: 21645 RVA: 0x0012B458 File Offset: 0x00129658
		[SecurityCritical]
		private void ParseObject(ParseRecord pr)
		{
			if (!this.bFullDeserialization)
			{
				this.InitFullDeserialization();
			}
			if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
			{
				this.topId = pr.PRobjectId;
			}
			if (pr.PRparseTypeEnum == InternalParseTypeE.Object)
			{
				this.stack.Push(pr);
			}
			if (pr.PRobjectTypeEnum == InternalObjectTypeE.Array)
			{
				this.ParseArray(pr);
				return;
			}
			if (pr.PRdtType == null)
			{
				pr.PRnewObj = new TypeLoadExceptionHolder(pr.PRkeyDt);
				return;
			}
			if (pr.PRdtType == Converter.typeofString)
			{
				if (pr.PRvalue == null)
				{
					return;
				}
				pr.PRnewObj = pr.PRvalue;
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = pr.PRnewObj;
					return;
				}
				this.stack.Pop();
				this.RegisterObject(pr.PRnewObj, pr, (ParseRecord)this.stack.Peek());
				return;
			}
			else
			{
				this.CheckSerializable(pr.PRdtType);
				if (this.IsRemoting && this.formatterEnums.FEsecurityLevel != TypeFilterLevel.Full)
				{
					pr.PRnewObj = FormatterServices.GetSafeUninitializedObject(pr.PRdtType);
				}
				else
				{
					pr.PRnewObj = FormatterServices.GetUninitializedObject(pr.PRdtType);
				}
				this.m_objectManager.RaiseOnDeserializingEvent(pr.PRnewObj);
				if (pr.PRnewObj == null)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_TopObjectInstantiate", new object[] { pr.PRdtType }));
				}
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = pr.PRnewObj;
				}
				if (pr.PRobjectInfo == null)
				{
					pr.PRobjectInfo = ReadObjectInfo.Create(pr.PRdtType, this.m_surrogates, this.m_context, this.m_objectManager, this.serObjectInfoInit, this.m_formatterConverter, this.bSimpleAssembly);
				}
				this.CheckSecurity(pr);
				return;
			}
		}

		// Token: 0x0600548E RID: 21646 RVA: 0x0012B604 File Offset: 0x00129804
		[SecurityCritical]
		private void ParseObjectEnd(ParseRecord pr)
		{
			ParseRecord parseRecord = (ParseRecord)this.stack.Peek();
			if (parseRecord == null)
			{
				parseRecord = pr;
			}
			if (parseRecord.PRobjectPositionEnum == InternalObjectPositionE.Top && parseRecord.PRdtType == Converter.typeofString)
			{
				parseRecord.PRnewObj = parseRecord.PRvalue;
				this.TopObject = parseRecord.PRnewObj;
				return;
			}
			this.stack.Pop();
			ParseRecord parseRecord2 = (ParseRecord)this.stack.Peek();
			if (parseRecord.PRnewObj == null)
			{
				return;
			}
			if (parseRecord.PRobjectTypeEnum == InternalObjectTypeE.Array)
			{
				if (parseRecord.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = parseRecord.PRnewObj;
				}
				this.RegisterObject(parseRecord.PRnewObj, parseRecord, parseRecord2);
				return;
			}
			parseRecord.PRobjectInfo.PopulateObjectMembers(parseRecord.PRnewObj, parseRecord.PRmemberData);
			if (!parseRecord.PRisRegistered && parseRecord.PRobjectId > 0L)
			{
				this.RegisterObject(parseRecord.PRnewObj, parseRecord, parseRecord2);
			}
			if (parseRecord.PRisValueTypeFixup)
			{
				ValueFixup valueFixup = (ValueFixup)this.ValueFixupStack.Pop();
				valueFixup.Fixup(parseRecord, parseRecord2);
			}
			if (parseRecord.PRobjectPositionEnum == InternalObjectPositionE.Top)
			{
				this.TopObject = parseRecord.PRnewObj;
			}
			parseRecord.PRobjectInfo.ObjectEnd();
		}

		// Token: 0x0600548F RID: 21647 RVA: 0x0012B724 File Offset: 0x00129924
		[SecurityCritical]
		private void ParseArray(ParseRecord pr)
		{
			long probjectId = pr.PRobjectId;
			if (pr.PRarrayTypeEnum == InternalArrayTypeE.Base64)
			{
				if (pr.PRvalue.Length > 0)
				{
					pr.PRnewObj = Convert.FromBase64String(pr.PRvalue);
				}
				else
				{
					pr.PRnewObj = new byte[0];
				}
				if (this.stack.Peek() == pr)
				{
					this.stack.Pop();
				}
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = pr.PRnewObj;
				}
				ParseRecord parseRecord = (ParseRecord)this.stack.Peek();
				this.RegisterObject(pr.PRnewObj, pr, parseRecord);
			}
			else if (pr.PRnewObj != null && Converter.IsWriteAsByteArray(pr.PRarrayElementTypeCode))
			{
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = pr.PRnewObj;
				}
				ParseRecord parseRecord2 = (ParseRecord)this.stack.Peek();
				this.RegisterObject(pr.PRnewObj, pr, parseRecord2);
			}
			else if (pr.PRarrayTypeEnum == InternalArrayTypeE.Jagged || pr.PRarrayTypeEnum == InternalArrayTypeE.Single)
			{
				bool flag = true;
				if (pr.PRlowerBoundA == null || pr.PRlowerBoundA[0] == 0)
				{
					if (pr.PRarrayElementType == Converter.typeofString)
					{
						object[] array = new string[pr.PRlengthA[0]];
						pr.PRobjectA = array;
						pr.PRnewObj = pr.PRobjectA;
						flag = false;
					}
					else if (pr.PRarrayElementType == Converter.typeofObject)
					{
						pr.PRobjectA = new object[pr.PRlengthA[0]];
						pr.PRnewObj = pr.PRobjectA;
						flag = false;
					}
					else if (pr.PRarrayElementType != null)
					{
						pr.PRnewObj = Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA[0]);
					}
					pr.PRisLowerBound = false;
				}
				else
				{
					if (pr.PRarrayElementType != null)
					{
						pr.PRnewObj = Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA, pr.PRlowerBoundA);
					}
					pr.PRisLowerBound = true;
				}
				if (pr.PRarrayTypeEnum == InternalArrayTypeE.Single)
				{
					if (!pr.PRisLowerBound && Converter.IsWriteAsByteArray(pr.PRarrayElementTypeCode))
					{
						pr.PRprimitiveArray = new PrimitiveArray(pr.PRarrayElementTypeCode, (Array)pr.PRnewObj);
					}
					else if (flag && pr.PRarrayElementType != null && !pr.PRarrayElementType.IsValueType && !pr.PRisLowerBound)
					{
						pr.PRobjectA = (object[])pr.PRnewObj;
					}
				}
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Headers)
				{
					this.headers = (Header[])pr.PRnewObj;
				}
				pr.PRindexMap = new int[1];
			}
			else
			{
				if (pr.PRarrayTypeEnum != InternalArrayTypeE.Rectangular)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_ArrayType", new object[] { pr.PRarrayTypeEnum }));
				}
				pr.PRisLowerBound = false;
				if (pr.PRlowerBoundA != null)
				{
					for (int i = 0; i < pr.PRrank; i++)
					{
						if (pr.PRlowerBoundA[i] != 0)
						{
							pr.PRisLowerBound = true;
						}
					}
				}
				if (pr.PRarrayElementType != null)
				{
					if (!pr.PRisLowerBound)
					{
						pr.PRnewObj = Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA);
					}
					else
					{
						pr.PRnewObj = Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA, pr.PRlowerBoundA);
					}
				}
				int num = 1;
				for (int j = 0; j < pr.PRrank; j++)
				{
					num *= pr.PRlengthA[j];
				}
				pr.PRindexMap = new int[pr.PRrank];
				pr.PRrectangularMap = new int[pr.PRrank];
				pr.PRlinearlength = num;
			}
			this.CheckSecurity(pr);
		}

		// Token: 0x06005490 RID: 21648 RVA: 0x0012BA98 File Offset: 0x00129C98
		private void NextRectangleMap(ParseRecord pr)
		{
			for (int i = pr.PRrank - 1; i > -1; i--)
			{
				if (pr.PRrectangularMap[i] < pr.PRlengthA[i] - 1)
				{
					pr.PRrectangularMap[i]++;
					if (i < pr.PRrank - 1)
					{
						for (int j = i + 1; j < pr.PRrank; j++)
						{
							pr.PRrectangularMap[j] = 0;
						}
					}
					Array.Copy(pr.PRrectangularMap, pr.PRindexMap, pr.PRrank);
					return;
				}
			}
		}

		// Token: 0x06005491 RID: 21649 RVA: 0x0012BB1C File Offset: 0x00129D1C
		[SecurityCritical]
		private void ParseArrayMember(ParseRecord pr)
		{
			ParseRecord parseRecord = (ParseRecord)this.stack.Peek();
			if (parseRecord.PRarrayTypeEnum == InternalArrayTypeE.Rectangular)
			{
				if (parseRecord.PRmemberIndex > 0)
				{
					this.NextRectangleMap(parseRecord);
				}
				if (parseRecord.PRisLowerBound)
				{
					for (int i = 0; i < parseRecord.PRrank; i++)
					{
						parseRecord.PRindexMap[i] = parseRecord.PRrectangularMap[i] + parseRecord.PRlowerBoundA[i];
					}
				}
			}
			else if (!parseRecord.PRisLowerBound)
			{
				parseRecord.PRindexMap[0] = parseRecord.PRmemberIndex;
			}
			else
			{
				parseRecord.PRindexMap[0] = parseRecord.PRlowerBoundA[0] + parseRecord.PRmemberIndex;
			}
			if (pr.PRmemberValueEnum == InternalMemberValueE.Reference)
			{
				object @object = this.m_objectManager.GetObject(pr.PRidRef);
				if (@object == null)
				{
					int[] array = new int[parseRecord.PRrank];
					Array.Copy(parseRecord.PRindexMap, 0, array, 0, parseRecord.PRrank);
					this.m_objectManager.RecordArrayElementFixup(parseRecord.PRobjectId, array, pr.PRidRef);
				}
				else if (parseRecord.PRobjectA != null)
				{
					parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = @object;
				}
				else
				{
					((Array)parseRecord.PRnewObj).SetValue(@object, parseRecord.PRindexMap);
				}
			}
			else if (pr.PRmemberValueEnum == InternalMemberValueE.Nested)
			{
				if (pr.PRdtType == null)
				{
					pr.PRdtType = parseRecord.PRarrayElementType;
				}
				this.ParseObject(pr);
				this.stack.Push(pr);
				if (parseRecord.PRarrayElementType != null)
				{
					if (parseRecord.PRarrayElementType.IsValueType && pr.PRarrayElementTypeCode == InternalPrimitiveTypeE.Invalid)
					{
						pr.PRisValueTypeFixup = true;
						this.ValueFixupStack.Push(new ValueFixup((Array)parseRecord.PRnewObj, parseRecord.PRindexMap));
					}
					else if (parseRecord.PRobjectA != null)
					{
						parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = pr.PRnewObj;
					}
					else
					{
						((Array)parseRecord.PRnewObj).SetValue(pr.PRnewObj, parseRecord.PRindexMap);
					}
				}
			}
			else if (pr.PRmemberValueEnum == InternalMemberValueE.InlineValue)
			{
				if (parseRecord.PRarrayElementType == Converter.typeofString || pr.PRdtType == Converter.typeofString)
				{
					this.ParseString(pr, parseRecord);
					if (parseRecord.PRobjectA != null)
					{
						parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = pr.PRvalue;
					}
					else
					{
						((Array)parseRecord.PRnewObj).SetValue(pr.PRvalue, parseRecord.PRindexMap);
					}
				}
				else if (parseRecord.PRisArrayVariant)
				{
					if (pr.PRkeyDt == null)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_ArrayTypeObject"));
					}
					object obj;
					if (pr.PRdtType == Converter.typeofString)
					{
						this.ParseString(pr, parseRecord);
						obj = pr.PRvalue;
					}
					else if (pr.PRdtTypeCode == InternalPrimitiveTypeE.Invalid)
					{
						this.CheckSerializable(pr.PRdtType);
						if (this.IsRemoting && this.formatterEnums.FEsecurityLevel != TypeFilterLevel.Full)
						{
							obj = FormatterServices.GetSafeUninitializedObject(pr.PRdtType);
						}
						else
						{
							obj = FormatterServices.GetUninitializedObject(pr.PRdtType);
						}
					}
					else if (pr.PRvarValue != null)
					{
						obj = pr.PRvarValue;
					}
					else
					{
						obj = Converter.FromString(pr.PRvalue, pr.PRdtTypeCode);
					}
					if (parseRecord.PRobjectA != null)
					{
						parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = obj;
					}
					else
					{
						((Array)parseRecord.PRnewObj).SetValue(obj, parseRecord.PRindexMap);
					}
				}
				else if (parseRecord.PRprimitiveArray != null)
				{
					parseRecord.PRprimitiveArray.SetValue(pr.PRvalue, parseRecord.PRindexMap[0]);
				}
				else
				{
					object obj2;
					if (pr.PRvarValue != null)
					{
						obj2 = pr.PRvarValue;
					}
					else
					{
						obj2 = Converter.FromString(pr.PRvalue, parseRecord.PRarrayElementTypeCode);
					}
					if (parseRecord.PRobjectA != null)
					{
						parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = obj2;
					}
					else
					{
						((Array)parseRecord.PRnewObj).SetValue(obj2, parseRecord.PRindexMap);
					}
				}
			}
			else if (pr.PRmemberValueEnum == InternalMemberValueE.Null)
			{
				parseRecord.PRmemberIndex += pr.PRnullCount - 1;
			}
			else
			{
				this.ParseError(pr, parseRecord);
			}
			parseRecord.PRmemberIndex++;
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x0012BF32 File Offset: 0x0012A132
		[SecurityCritical]
		private void ParseArrayMemberEnd(ParseRecord pr)
		{
			if (pr.PRmemberValueEnum == InternalMemberValueE.Nested)
			{
				this.ParseObjectEnd(pr);
			}
		}

		// Token: 0x06005493 RID: 21651 RVA: 0x0012BF44 File Offset: 0x0012A144
		[SecurityCritical]
		private void ParseMember(ParseRecord pr)
		{
			ParseRecord parseRecord = (ParseRecord)this.stack.Peek();
			if (parseRecord != null)
			{
				string prname = parseRecord.PRname;
			}
			InternalMemberTypeE prmemberTypeEnum = pr.PRmemberTypeEnum;
			if (prmemberTypeEnum != InternalMemberTypeE.Field && prmemberTypeEnum == InternalMemberTypeE.Item)
			{
				this.ParseArrayMember(pr);
				return;
			}
			if (pr.PRdtType == null && parseRecord.PRobjectInfo.isTyped)
			{
				pr.PRdtType = parseRecord.PRobjectInfo.GetType(pr.PRname);
				if (pr.PRdtType != null)
				{
					pr.PRdtTypeCode = Converter.ToCode(pr.PRdtType);
				}
			}
			if (pr.PRmemberValueEnum == InternalMemberValueE.Null)
			{
				parseRecord.PRobjectInfo.AddValue(pr.PRname, null, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
				return;
			}
			if (pr.PRmemberValueEnum == InternalMemberValueE.Nested)
			{
				this.ParseObject(pr);
				this.stack.Push(pr);
				if (pr.PRobjectInfo != null && pr.PRobjectInfo.objectType != null && pr.PRobjectInfo.objectType.IsValueType)
				{
					pr.PRisValueTypeFixup = true;
					this.ValueFixupStack.Push(new ValueFixup(parseRecord.PRnewObj, pr.PRname, parseRecord.PRobjectInfo));
					return;
				}
				parseRecord.PRobjectInfo.AddValue(pr.PRname, pr.PRnewObj, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
				return;
			}
			else
			{
				if (pr.PRmemberValueEnum != InternalMemberValueE.Reference)
				{
					if (pr.PRmemberValueEnum == InternalMemberValueE.InlineValue)
					{
						if (pr.PRdtType == Converter.typeofString)
						{
							this.ParseString(pr, parseRecord);
							parseRecord.PRobjectInfo.AddValue(pr.PRname, pr.PRvalue, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
						if (pr.PRdtTypeCode != InternalPrimitiveTypeE.Invalid)
						{
							object obj;
							if (pr.PRvarValue != null)
							{
								obj = pr.PRvarValue;
							}
							else
							{
								obj = Converter.FromString(pr.PRvalue, pr.PRdtTypeCode);
							}
							parseRecord.PRobjectInfo.AddValue(pr.PRname, obj, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
						if (pr.PRarrayTypeEnum == InternalArrayTypeE.Base64)
						{
							parseRecord.PRobjectInfo.AddValue(pr.PRname, Convert.FromBase64String(pr.PRvalue), ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
						if (pr.PRdtType == Converter.typeofObject)
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_TypeMissing", new object[] { pr.PRname }));
						}
						this.ParseString(pr, parseRecord);
						if (pr.PRdtType == Converter.typeofSystemVoid)
						{
							parseRecord.PRobjectInfo.AddValue(pr.PRname, pr.PRdtType, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
						if (parseRecord.PRobjectInfo.isSi)
						{
							parseRecord.PRobjectInfo.AddValue(pr.PRname, pr.PRvalue, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
					}
					else
					{
						this.ParseError(pr, parseRecord);
					}
					return;
				}
				object @object = this.m_objectManager.GetObject(pr.PRidRef);
				if (@object == null)
				{
					parseRecord.PRobjectInfo.AddValue(pr.PRname, null, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
					parseRecord.PRobjectInfo.RecordFixup(parseRecord.PRobjectId, pr.PRname, pr.PRidRef);
					return;
				}
				parseRecord.PRobjectInfo.AddValue(pr.PRname, @object, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
				return;
			}
		}

		// Token: 0x06005494 RID: 21652 RVA: 0x0012C268 File Offset: 0x0012A468
		[SecurityCritical]
		private void ParseMemberEnd(ParseRecord pr)
		{
			InternalMemberTypeE prmemberTypeEnum = pr.PRmemberTypeEnum;
			if (prmemberTypeEnum != InternalMemberTypeE.Field)
			{
				if (prmemberTypeEnum == InternalMemberTypeE.Item)
				{
					this.ParseArrayMemberEnd(pr);
					return;
				}
				this.ParseError(pr, (ParseRecord)this.stack.Peek());
			}
			else if (pr.PRmemberValueEnum == InternalMemberValueE.Nested)
			{
				this.ParseObjectEnd(pr);
				return;
			}
		}

		// Token: 0x06005495 RID: 21653 RVA: 0x0012C2B4 File Offset: 0x0012A4B4
		[SecurityCritical]
		private void ParseString(ParseRecord pr, ParseRecord parentPr)
		{
			if (!pr.PRisRegistered && pr.PRobjectId > 0L)
			{
				this.RegisterObject(pr.PRvalue, pr, parentPr, true);
			}
		}

		// Token: 0x06005496 RID: 21654 RVA: 0x0012C2D7 File Offset: 0x0012A4D7
		[SecurityCritical]
		private void RegisterObject(object obj, ParseRecord pr, ParseRecord objectPr)
		{
			this.RegisterObject(obj, pr, objectPr, false);
		}

		// Token: 0x06005497 RID: 21655 RVA: 0x0012C2E4 File Offset: 0x0012A4E4
		[SecurityCritical]
		private void RegisterObject(object obj, ParseRecord pr, ParseRecord objectPr, bool bIsString)
		{
			if (!pr.PRisRegistered)
			{
				pr.PRisRegistered = true;
				long num = 0L;
				MemberInfo memberInfo = null;
				int[] array = null;
				if (objectPr != null)
				{
					array = objectPr.PRindexMap;
					num = objectPr.PRobjectId;
					if (objectPr.PRobjectInfo != null && !objectPr.PRobjectInfo.isSi)
					{
						memberInfo = objectPr.PRobjectInfo.GetMemberInfo(pr.PRname);
					}
				}
				SerializationInfo prsi = pr.PRsi;
				if (bIsString)
				{
					this.m_objectManager.RegisterString((string)obj, pr.PRobjectId, prsi, num, memberInfo);
					return;
				}
				this.m_objectManager.RegisterObject(obj, pr.PRobjectId, prsi, num, memberInfo, array);
			}
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x0012C380 File Offset: 0x0012A580
		[SecurityCritical]
		internal long GetId(long objectId)
		{
			if (!this.bFullDeserialization)
			{
				this.InitFullDeserialization();
			}
			if (objectId > 0L)
			{
				return objectId;
			}
			if (this.bOldFormatDetected || objectId == -1L)
			{
				this.bOldFormatDetected = true;
				if (this.valTypeObjectIdTable == null)
				{
					this.valTypeObjectIdTable = new IntSizedArray();
				}
				long num;
				if ((num = (long)this.valTypeObjectIdTable[(int)objectId]) == 0L)
				{
					num = 2147483647L + objectId;
					this.valTypeObjectIdTable[(int)objectId] = (int)num;
				}
				return num;
			}
			return -1L * objectId;
		}

		// Token: 0x06005499 RID: 21657 RVA: 0x0012C3FC File Offset: 0x0012A5FC
		[Conditional("SER_LOGGING")]
		private void IndexTraceMessage(string message, int[] index)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(10);
			stringBuilder.Append("[");
			for (int i = 0; i < index.Length; i++)
			{
				stringBuilder.Append(index[i]);
				if (i != index.Length - 1)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append("]");
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x0012C458 File Offset: 0x0012A658
		[SecurityCritical]
		internal Type Bind(string assemblyString, string typeString)
		{
			Type type = null;
			if (this.m_binder != null)
			{
				type = this.m_binder.BindToType(assemblyString, typeString);
			}
			if (type == null)
			{
				type = this.FastBindToType(assemblyString, typeString);
			}
			return type;
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x0012C48C File Offset: 0x0012A68C
		[SecurityCritical]
		internal Type FastBindToType(string assemblyName, string typeName)
		{
			Type type = null;
			ObjectReader.TypeNAssembly typeNAssembly = (ObjectReader.TypeNAssembly)this.typeCache.GetCachedValue(typeName);
			if (typeNAssembly == null || typeNAssembly.assemblyName != assemblyName)
			{
				Assembly assembly = null;
				if (this.bSimpleAssembly)
				{
					try
					{
						ObjectReader.sfileIOPermission.Assert();
						try
						{
							assembly = ObjectReader.ResolveSimpleAssemblyName(new AssemblyName(assemblyName));
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					catch (Exception ex)
					{
					}
					if (assembly == null)
					{
						return null;
					}
					ObjectReader.GetSimplyNamedTypeFromAssembly(assembly, typeName, ref type);
				}
				else
				{
					try
					{
						ObjectReader.sfileIOPermission.Assert();
						try
						{
							assembly = Assembly.Load(assemblyName);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					catch (Exception ex2)
					{
					}
					if (assembly == null)
					{
						return null;
					}
					type = FormatterServices.GetTypeFromAssembly(assembly, typeName);
				}
				if (type == null)
				{
					return null;
				}
				ObjectReader.CheckTypeForwardedTo(assembly, type.Assembly, type);
				typeNAssembly = new ObjectReader.TypeNAssembly();
				typeNAssembly.type = type;
				typeNAssembly.assemblyName = assemblyName;
				this.typeCache.SetCachedValue(typeNAssembly);
			}
			return typeNAssembly.type;
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x0012C5A4 File Offset: 0x0012A7A4
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Assembly ResolveSimpleAssemblyName(AssemblyName assemblyName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMe;
			Assembly assembly = RuntimeAssembly.LoadWithPartialNameInternal(assemblyName, null, ref stackCrawlMark);
			if (assembly == null && assemblyName != null)
			{
				assembly = RuntimeAssembly.LoadWithPartialNameInternal(assemblyName.Name, null, ref stackCrawlMark);
			}
			return assembly;
		}

		// Token: 0x0600549D RID: 21661 RVA: 0x0012C5DC File Offset: 0x0012A7DC
		[SecurityCritical]
		private static void GetSimplyNamedTypeFromAssembly(Assembly assm, string typeName, ref Type type)
		{
			try
			{
				type = FormatterServices.GetTypeFromAssembly(assm, typeName);
			}
			catch (TypeLoadException)
			{
			}
			catch (FileNotFoundException)
			{
			}
			catch (FileLoadException)
			{
			}
			catch (BadImageFormatException)
			{
			}
			if (type == null)
			{
				type = Type.GetType(typeName, new Func<AssemblyName, Assembly>(ObjectReader.ResolveSimpleAssemblyName), new Func<Assembly, string, bool, Type>(new ObjectReader.TopLevelAssemblyTypeResolver(assm).ResolveType), false);
			}
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x0012C660 File Offset: 0x0012A860
		[SecurityCritical]
		internal Type GetType(BinaryAssemblyInfo assemblyInfo, string name)
		{
			Type type = null;
			if (this.previousName != null && this.previousName.Length == name.Length && this.previousName.Equals(name) && this.previousAssemblyString != null && this.previousAssemblyString.Length == assemblyInfo.assemblyString.Length && this.previousAssemblyString.Equals(assemblyInfo.assemblyString))
			{
				type = this.previousType;
			}
			else
			{
				type = this.Bind(assemblyInfo.assemblyString, name);
				if (type == null)
				{
					Assembly assembly = assemblyInfo.GetAssembly();
					if (this.bSimpleAssembly)
					{
						ObjectReader.GetSimplyNamedTypeFromAssembly(assembly, name, ref type);
					}
					else
					{
						type = FormatterServices.GetTypeFromAssembly(assembly, name);
					}
					if (type != null)
					{
						ObjectReader.CheckTypeForwardedTo(assembly, type.Assembly, type);
					}
				}
				this.previousAssemblyString = assemblyInfo.assemblyString;
				this.previousName = name;
				this.previousType = type;
			}
			return type;
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x0012C738 File Offset: 0x0012A938
		[SecuritySafeCritical]
		private static void CheckTypeForwardedTo(Assembly sourceAssembly, Assembly destAssembly, Type resolvedType)
		{
			if (!FormatterServices.UnsafeTypeForwardersIsEnabled() && sourceAssembly != destAssembly && !destAssembly.PermissionSet.IsSubsetOf(sourceAssembly.PermissionSet))
			{
				TypeInformation typeInformation = BinaryFormatter.GetTypeInformation(resolvedType);
				if (!typeInformation.HasTypeForwardedFrom)
				{
					throw new SecurityException
					{
						Demanded = sourceAssembly.PermissionSet
					};
				}
				Assembly assembly = null;
				try
				{
					assembly = Assembly.Load(typeInformation.AssemblyString);
				}
				catch
				{
				}
				if (assembly != sourceAssembly)
				{
					throw new SecurityException
					{
						Demanded = sourceAssembly.PermissionSet
					};
				}
			}
		}

		// Token: 0x04002685 RID: 9861
		internal Stream m_stream;

		// Token: 0x04002686 RID: 9862
		internal ISurrogateSelector m_surrogates;

		// Token: 0x04002687 RID: 9863
		internal StreamingContext m_context;

		// Token: 0x04002688 RID: 9864
		internal ObjectManager m_objectManager;

		// Token: 0x04002689 RID: 9865
		internal InternalFE formatterEnums;

		// Token: 0x0400268A RID: 9866
		internal SerializationBinder m_binder;

		// Token: 0x0400268B RID: 9867
		internal long topId;

		// Token: 0x0400268C RID: 9868
		internal bool bSimpleAssembly;

		// Token: 0x0400268D RID: 9869
		internal object handlerObject;

		// Token: 0x0400268E RID: 9870
		internal object m_topObject;

		// Token: 0x0400268F RID: 9871
		internal Header[] headers;

		// Token: 0x04002690 RID: 9872
		internal HeaderHandler handler;

		// Token: 0x04002691 RID: 9873
		internal SerObjectInfoInit serObjectInfoInit;

		// Token: 0x04002692 RID: 9874
		internal IFormatterConverter m_formatterConverter;

		// Token: 0x04002693 RID: 9875
		internal SerStack stack;

		// Token: 0x04002694 RID: 9876
		private SerStack valueFixupStack;

		// Token: 0x04002695 RID: 9877
		internal object[] crossAppDomainArray;

		// Token: 0x04002696 RID: 9878
		private bool bFullDeserialization;

		// Token: 0x04002697 RID: 9879
		private bool bMethodCall;

		// Token: 0x04002698 RID: 9880
		private bool bMethodReturn;

		// Token: 0x04002699 RID: 9881
		private BinaryMethodCall binaryMethodCall;

		// Token: 0x0400269A RID: 9882
		private BinaryMethodReturn binaryMethodReturn;

		// Token: 0x0400269B RID: 9883
		private bool bIsCrossAppDomain;

		// Token: 0x0400269C RID: 9884
		private static FileIOPermission sfileIOPermission = new FileIOPermission(PermissionState.Unrestricted);

		// Token: 0x0400269D RID: 9885
		private const int THRESHOLD_FOR_VALUETYPE_IDS = 2147483647;

		// Token: 0x0400269E RID: 9886
		private bool bOldFormatDetected;

		// Token: 0x0400269F RID: 9887
		private IntSizedArray valTypeObjectIdTable;

		// Token: 0x040026A0 RID: 9888
		private NameCache typeCache = new NameCache();

		// Token: 0x040026A1 RID: 9889
		private string previousAssemblyString;

		// Token: 0x040026A2 RID: 9890
		private string previousName;

		// Token: 0x040026A3 RID: 9891
		private Type previousType;

		// Token: 0x02000C62 RID: 3170
		internal class TypeNAssembly
		{
			// Token: 0x040037CD RID: 14285
			public Type type;

			// Token: 0x040037CE RID: 14286
			public string assemblyName;
		}

		// Token: 0x02000C63 RID: 3171
		internal sealed class TopLevelAssemblyTypeResolver
		{
			// Token: 0x0600709D RID: 28829 RVA: 0x00184E46 File Offset: 0x00183046
			public TopLevelAssemblyTypeResolver(Assembly topLevelAssembly)
			{
				this.m_topLevelAssembly = topLevelAssembly;
			}

			// Token: 0x0600709E RID: 28830 RVA: 0x00184E55 File Offset: 0x00183055
			public Type ResolveType(Assembly assembly, string simpleTypeName, bool ignoreCase)
			{
				if (assembly == null)
				{
					assembly = this.m_topLevelAssembly;
				}
				return assembly.GetType(simpleTypeName, false, ignoreCase);
			}

			// Token: 0x040037CF RID: 14287
			private Assembly m_topLevelAssembly;
		}
	}
}
