using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200079F RID: 1951
	internal sealed class WriteObjectInfo
	{
		// Token: 0x060054D7 RID: 21719 RVA: 0x0012E753 File Offset: 0x0012C953
		internal WriteObjectInfo()
		{
		}

		// Token: 0x060054D8 RID: 21720 RVA: 0x0012E75B File Offset: 0x0012C95B
		internal void ObjectEnd()
		{
			WriteObjectInfo.PutObjectInfo(this.serObjectInfoInit, this);
		}

		// Token: 0x060054D9 RID: 21721 RVA: 0x0012E76C File Offset: 0x0012C96C
		private void InternalInit()
		{
			this.obj = null;
			this.objectType = null;
			this.isSi = false;
			this.isNamed = false;
			this.isTyped = false;
			this.isArray = false;
			this.si = null;
			this.cache = null;
			this.memberData = null;
			this.objectId = 0L;
			this.assemId = 0L;
			this.binderTypeName = null;
			this.binderAssemblyString = null;
		}

		// Token: 0x060054DA RID: 21722 RVA: 0x0012E7D8 File Offset: 0x0012C9D8
		[SecurityCritical]
		internal static WriteObjectInfo Serialize(object obj, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, ObjectWriter objectWriter, SerializationBinder binder)
		{
			WriteObjectInfo objectInfo = WriteObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.InitSerialize(obj, surrogateSelector, context, serObjectInfoInit, converter, objectWriter, binder);
			return objectInfo;
		}

		// Token: 0x060054DB RID: 21723 RVA: 0x0012E800 File Offset: 0x0012CA00
		[SecurityCritical]
		internal void InitSerialize(object obj, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, ObjectWriter objectWriter, SerializationBinder binder)
		{
			this.context = context;
			this.obj = obj;
			this.serObjectInfoInit = serObjectInfoInit;
			if (RemotingServices.IsTransparentProxy(obj))
			{
				this.objectType = Converter.typeofMarshalByRefObject;
			}
			else
			{
				this.objectType = obj.GetType();
			}
			if (this.objectType.IsArray)
			{
				this.isArray = true;
				this.InitNoMembers();
				return;
			}
			this.InvokeSerializationBinder(binder);
			objectWriter.ObjectManager.RegisterObject(obj);
			ISurrogateSelector surrogateSelector2;
			if (surrogateSelector != null && (this.serializationSurrogate = surrogateSelector.GetSurrogate(this.objectType, context, out surrogateSelector2)) != null)
			{
				this.si = new SerializationInfo(this.objectType, converter);
				if (!this.objectType.IsPrimitive)
				{
					this.serializationSurrogate.GetObjectData(obj, this.si, context);
				}
				this.InitSiWrite();
				return;
			}
			if (!(obj is ISerializable))
			{
				this.InitMemberInfo();
				WriteObjectInfo.CheckTypeForwardedFrom(this.cache, this.objectType, this.binderAssemblyString);
				return;
			}
			if (!this.objectType.IsSerializable)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_NonSerType", new object[]
				{
					this.objectType.FullName,
					this.objectType.Assembly.FullName
				}));
			}
			this.si = new SerializationInfo(this.objectType, converter, !FormatterServices.UnsafeTypeForwardersIsEnabled());
			((ISerializable)obj).GetObjectData(this.si, context);
			this.InitSiWrite();
			WriteObjectInfo.CheckTypeForwardedFrom(this.cache, this.objectType, this.binderAssemblyString);
		}

		// Token: 0x060054DC RID: 21724 RVA: 0x0012E984 File Offset: 0x0012CB84
		[Conditional("SER_LOGGING")]
		private void DumpMemberInfo()
		{
			for (int i = 0; i < this.cache.memberInfos.Length; i++)
			{
			}
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x0012E9AC File Offset: 0x0012CBAC
		[SecurityCritical]
		internal static WriteObjectInfo Serialize(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, SerializationBinder binder)
		{
			WriteObjectInfo objectInfo = WriteObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.InitSerialize(objectType, surrogateSelector, context, serObjectInfoInit, converter, binder);
			return objectInfo;
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x0012E9D0 File Offset: 0x0012CBD0
		[SecurityCritical]
		internal void InitSerialize(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, SerializationBinder binder)
		{
			this.objectType = objectType;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			if (objectType.IsArray)
			{
				this.InitNoMembers();
				return;
			}
			this.InvokeSerializationBinder(binder);
			ISurrogateSelector surrogateSelector2 = null;
			if (surrogateSelector != null)
			{
				this.serializationSurrogate = surrogateSelector.GetSurrogate(objectType, context, out surrogateSelector2);
			}
			if (this.serializationSurrogate != null)
			{
				this.si = new SerializationInfo(objectType, converter);
				this.cache = new SerObjectInfoCache(objectType);
				this.isSi = true;
			}
			else if (objectType != Converter.typeofObject && Converter.typeofISerializable.IsAssignableFrom(objectType))
			{
				this.si = new SerializationInfo(objectType, converter, !FormatterServices.UnsafeTypeForwardersIsEnabled());
				this.cache = new SerObjectInfoCache(objectType);
				WriteObjectInfo.CheckTypeForwardedFrom(this.cache, objectType, this.binderAssemblyString);
				this.isSi = true;
			}
			if (!this.isSi)
			{
				this.InitMemberInfo();
				WriteObjectInfo.CheckTypeForwardedFrom(this.cache, objectType, this.binderAssemblyString);
			}
		}

		// Token: 0x060054DF RID: 21727 RVA: 0x0012EABC File Offset: 0x0012CCBC
		private void InitSiWrite()
		{
			this.isSi = true;
			SerializationInfoEnumerator serializationInfoEnumerator = this.si.GetEnumerator();
			int memberCount = this.si.MemberCount;
			int num = memberCount;
			TypeInformation typeInformation = null;
			string text = this.si.FullTypeName;
			string text2 = this.si.AssemblyName;
			bool flag = false;
			if (!this.si.IsFullTypeNameSetExplicit)
			{
				typeInformation = BinaryFormatter.GetTypeInformation(this.si.ObjectType);
				text = typeInformation.FullTypeName;
				flag = typeInformation.HasTypeForwardedFrom;
			}
			if (!this.si.IsAssemblyNameSetExplicit)
			{
				if (typeInformation == null)
				{
					typeInformation = BinaryFormatter.GetTypeInformation(this.si.ObjectType);
				}
				text2 = typeInformation.AssemblyString;
				flag = typeInformation.HasTypeForwardedFrom;
			}
			this.cache = new SerObjectInfoCache(text, text2, flag);
			this.cache.memberNames = new string[num];
			this.cache.memberTypes = new Type[num];
			this.memberData = new object[num];
			serializationInfoEnumerator = this.si.GetEnumerator();
			int num2 = 0;
			while (serializationInfoEnumerator.MoveNext())
			{
				this.cache.memberNames[num2] = serializationInfoEnumerator.Name;
				this.cache.memberTypes[num2] = serializationInfoEnumerator.ObjectType;
				this.memberData[num2] = serializationInfoEnumerator.Value;
				num2++;
			}
			this.isNamed = true;
			this.isTyped = false;
		}

		// Token: 0x060054E0 RID: 21728 RVA: 0x0012EC10 File Offset: 0x0012CE10
		private static void CheckTypeForwardedFrom(SerObjectInfoCache cache, Type objectType, string binderAssemblyString)
		{
			if (cache.hasTypeForwardedFrom && binderAssemblyString == null && !FormatterServices.UnsafeTypeForwardersIsEnabled())
			{
				Assembly assembly = objectType.Assembly;
				if (!SerializationInfo.IsAssemblyNameAssignmentSafe(assembly.FullName, cache.assemblyString) && !assembly.IsFullyTrusted)
				{
					throw new SecurityException(Environment.GetResourceString("Serialization_RequireFullTrust", new object[] { objectType }));
				}
			}
		}

		// Token: 0x060054E1 RID: 21729 RVA: 0x0012EC6C File Offset: 0x0012CE6C
		private void InitNoMembers()
		{
			this.cache = (SerObjectInfoCache)this.serObjectInfoInit.seenBeforeTable[this.objectType];
			if (this.cache == null)
			{
				this.cache = new SerObjectInfoCache(this.objectType);
				this.serObjectInfoInit.seenBeforeTable.Add(this.objectType, this.cache);
			}
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x0012ECD0 File Offset: 0x0012CED0
		[SecurityCritical]
		private void InitMemberInfo()
		{
			this.cache = (SerObjectInfoCache)this.serObjectInfoInit.seenBeforeTable[this.objectType];
			if (this.cache == null)
			{
				this.cache = new SerObjectInfoCache(this.objectType);
				this.cache.memberInfos = FormatterServices.GetSerializableMembers(this.objectType, this.context);
				int num = this.cache.memberInfos.Length;
				this.cache.memberNames = new string[num];
				this.cache.memberTypes = new Type[num];
				for (int i = 0; i < num; i++)
				{
					this.cache.memberNames[i] = this.cache.memberInfos[i].Name;
					this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
				}
				this.serObjectInfoInit.seenBeforeTable.Add(this.objectType, this.cache);
			}
			if (this.obj != null)
			{
				this.memberData = FormatterServices.GetObjectData(this.obj, this.cache.memberInfos);
			}
			this.isTyped = true;
			this.isNamed = true;
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x0012EDFF File Offset: 0x0012CFFF
		internal string GetTypeFullName()
		{
			return this.binderTypeName ?? this.cache.fullTypeName;
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x0012EE16 File Offset: 0x0012D016
		internal string GetAssemblyString()
		{
			return this.binderAssemblyString ?? this.cache.assemblyString;
		}

		// Token: 0x060054E5 RID: 21733 RVA: 0x0012EE2D File Offset: 0x0012D02D
		private void InvokeSerializationBinder(SerializationBinder binder)
		{
			if (binder != null)
			{
				binder.BindToName(this.objectType, out this.binderAssemblyString, out this.binderTypeName);
			}
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x0012EE4C File Offset: 0x0012D04C
		internal Type GetMemberType(MemberInfo objMember)
		{
			Type type;
			if (objMember is FieldInfo)
			{
				type = ((FieldInfo)objMember).FieldType;
			}
			else
			{
				if (!(objMember is PropertyInfo))
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_SerMemberInfo", new object[] { objMember.GetType() }));
				}
				type = ((PropertyInfo)objMember).PropertyType;
			}
			return type;
		}

		// Token: 0x060054E7 RID: 21735 RVA: 0x0012EEA8 File Offset: 0x0012D0A8
		internal void GetMemberInfo(out string[] outMemberNames, out Type[] outMemberTypes, out object[] outMemberData)
		{
			outMemberNames = this.cache.memberNames;
			outMemberTypes = this.cache.memberTypes;
			outMemberData = this.memberData;
			if (this.isSi && !this.isNamed)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_ISerializableMemberInfo"));
			}
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x0012EEF8 File Offset: 0x0012D0F8
		private static WriteObjectInfo GetObjectInfo(SerObjectInfoInit serObjectInfoInit)
		{
			WriteObjectInfo writeObjectInfo;
			if (!serObjectInfoInit.oiPool.IsEmpty())
			{
				writeObjectInfo = (WriteObjectInfo)serObjectInfoInit.oiPool.Pop();
				writeObjectInfo.InternalInit();
			}
			else
			{
				writeObjectInfo = new WriteObjectInfo();
				WriteObjectInfo writeObjectInfo2 = writeObjectInfo;
				int objectInfoIdCount = serObjectInfoInit.objectInfoIdCount;
				serObjectInfoInit.objectInfoIdCount = objectInfoIdCount + 1;
				writeObjectInfo2.objectInfoId = objectInfoIdCount;
			}
			return writeObjectInfo;
		}

		// Token: 0x060054E9 RID: 21737 RVA: 0x0012EF4B File Offset: 0x0012D14B
		private static void PutObjectInfo(SerObjectInfoInit serObjectInfoInit, WriteObjectInfo objectInfo)
		{
			serObjectInfoInit.oiPool.Push(objectInfo);
		}

		// Token: 0x040026E9 RID: 9961
		internal int objectInfoId;

		// Token: 0x040026EA RID: 9962
		internal object obj;

		// Token: 0x040026EB RID: 9963
		internal Type objectType;

		// Token: 0x040026EC RID: 9964
		internal bool isSi;

		// Token: 0x040026ED RID: 9965
		internal bool isNamed;

		// Token: 0x040026EE RID: 9966
		internal bool isTyped;

		// Token: 0x040026EF RID: 9967
		internal bool isArray;

		// Token: 0x040026F0 RID: 9968
		internal SerializationInfo si;

		// Token: 0x040026F1 RID: 9969
		internal SerObjectInfoCache cache;

		// Token: 0x040026F2 RID: 9970
		internal object[] memberData;

		// Token: 0x040026F3 RID: 9971
		internal ISerializationSurrogate serializationSurrogate;

		// Token: 0x040026F4 RID: 9972
		internal StreamingContext context;

		// Token: 0x040026F5 RID: 9973
		internal SerObjectInfoInit serObjectInfoInit;

		// Token: 0x040026F6 RID: 9974
		internal long objectId;

		// Token: 0x040026F7 RID: 9975
		internal long assemId;

		// Token: 0x040026F8 RID: 9976
		private string binderTypeName;

		// Token: 0x040026F9 RID: 9977
		private string binderAssemblyString;
	}
}
