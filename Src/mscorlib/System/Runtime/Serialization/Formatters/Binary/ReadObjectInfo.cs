using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security;
using System.Threading;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007A0 RID: 1952
	internal sealed class ReadObjectInfo
	{
		// Token: 0x060054EA RID: 21738 RVA: 0x0012EF59 File Offset: 0x0012D159
		internal ReadObjectInfo()
		{
		}

		// Token: 0x060054EB RID: 21739 RVA: 0x0012EF61 File Offset: 0x0012D161
		internal void ObjectEnd()
		{
		}

		// Token: 0x060054EC RID: 21740 RVA: 0x0012EF63 File Offset: 0x0012D163
		internal void PrepareForReuse()
		{
			this.lastPosition = 0;
		}

		// Token: 0x060054ED RID: 21741 RVA: 0x0012EF6C File Offset: 0x0012D16C
		[SecurityCritical]
		internal static ReadObjectInfo Create(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			ReadObjectInfo objectInfo = ReadObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.Init(objectType, surrogateSelector, context, objectManager, serObjectInfoInit, converter, bSimpleAssembly);
			return objectInfo;
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x0012EF92 File Offset: 0x0012D192
		[SecurityCritical]
		internal void Init(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			this.objectType = objectType;
			this.objectManager = objectManager;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			this.formatterConverter = converter;
			this.bSimpleAssembly = bSimpleAssembly;
			this.InitReadConstructor(objectType, surrogateSelector, context);
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x0012EFCC File Offset: 0x0012D1CC
		[SecurityCritical]
		internal static ReadObjectInfo Create(Type objectType, string[] memberNames, Type[] memberTypes, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			ReadObjectInfo objectInfo = ReadObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.Init(objectType, memberNames, memberTypes, surrogateSelector, context, objectManager, serObjectInfoInit, converter, bSimpleAssembly);
			return objectInfo;
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x0012EFF8 File Offset: 0x0012D1F8
		[SecurityCritical]
		internal void Init(Type objectType, string[] memberNames, Type[] memberTypes, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
		{
			this.objectType = objectType;
			this.objectManager = objectManager;
			this.wireMemberNames = memberNames;
			this.wireMemberTypes = memberTypes;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			this.formatterConverter = converter;
			this.bSimpleAssembly = bSimpleAssembly;
			if (memberNames != null)
			{
				this.isNamed = true;
			}
			if (memberTypes != null)
			{
				this.isTyped = true;
			}
			if (objectType != null)
			{
				this.InitReadConstructor(objectType, surrogateSelector, context);
			}
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x0012F064 File Offset: 0x0012D264
		[SecurityCritical]
		private void InitReadConstructor(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context)
		{
			if (objectType.IsArray)
			{
				this.InitNoMembers();
				return;
			}
			ISurrogateSelector surrogateSelector2 = null;
			if (surrogateSelector != null)
			{
				this.serializationSurrogate = surrogateSelector.GetSurrogate(objectType, context, out surrogateSelector2);
			}
			if (this.serializationSurrogate != null)
			{
				this.isSi = true;
			}
			else if (objectType != Converter.typeofObject && Converter.typeofISerializable.IsAssignableFrom(objectType))
			{
				this.isSi = true;
			}
			if (this.isSi)
			{
				this.InitSiRead();
				return;
			}
			this.InitMemberInfo();
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x0012F0D7 File Offset: 0x0012D2D7
		private void InitSiRead()
		{
			if (this.memberTypesList != null)
			{
				this.memberTypesList = new List<Type>(20);
			}
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x0012F0EE File Offset: 0x0012D2EE
		private void InitNoMembers()
		{
			this.cache = new SerObjectInfoCache(this.objectType);
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x0012F104 File Offset: 0x0012D304
		[SecurityCritical]
		private void InitMemberInfo()
		{
			this.cache = new SerObjectInfoCache(this.objectType);
			this.cache.memberInfos = FormatterServices.GetSerializableMembers(this.objectType, this.context);
			this.count = this.cache.memberInfos.Length;
			this.cache.memberNames = new string[this.count];
			this.cache.memberTypes = new Type[this.count];
			for (int i = 0; i < this.count; i++)
			{
				this.cache.memberNames[i] = this.cache.memberInfos[i].Name;
				this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
			}
			this.isTyped = true;
			this.isNamed = true;
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x0012F1DC File Offset: 0x0012D3DC
		internal MemberInfo GetMemberInfo(string name)
		{
			if (this.cache == null)
			{
				return null;
			}
			if (this.isSi)
			{
				string text = "Serialization_MemberInfo";
				object[] array = new object[1];
				int num = 0;
				Type type = this.objectType;
				array[num] = ((type != null) ? type.ToString() : null) + " " + name;
				throw new SerializationException(Environment.GetResourceString(text, array));
			}
			if (this.cache.memberInfos == null)
			{
				string text2 = "Serialization_NoMemberInfo";
				object[] array2 = new object[1];
				int num2 = 0;
				Type type2 = this.objectType;
				array2[num2] = ((type2 != null) ? type2.ToString() : null) + " " + name;
				throw new SerializationException(Environment.GetResourceString(text2, array2));
			}
			int num3 = this.Position(name);
			if (num3 != -1)
			{
				return this.cache.memberInfos[this.Position(name)];
			}
			return null;
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x0012F298 File Offset: 0x0012D498
		internal Type GetType(string name)
		{
			int num = this.Position(name);
			if (num == -1)
			{
				return null;
			}
			Type type;
			if (this.isTyped)
			{
				type = this.cache.memberTypes[num];
			}
			else
			{
				type = this.memberTypesList[num];
			}
			if (type == null)
			{
				string text = "Serialization_ISerializableTypes";
				object[] array = new object[1];
				int num2 = 0;
				Type type2 = this.objectType;
				array[num2] = ((type2 != null) ? type2.ToString() : null) + " " + name;
				throw new SerializationException(Environment.GetResourceString(text, array));
			}
			return type;
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x0012F314 File Offset: 0x0012D514
		internal void AddValue(string name, object value, ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isSi)
			{
				si.AddValue(name, value);
				return;
			}
			int num = this.Position(name);
			if (num != -1)
			{
				memberData[num] = value;
			}
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x0012F348 File Offset: 0x0012D548
		internal void InitDataStore(ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isSi)
			{
				if (si == null)
				{
					si = new SerializationInfo(this.objectType, this.formatterConverter);
					return;
				}
			}
			else if (memberData == null && this.cache != null)
			{
				memberData = new object[this.cache.memberNames.Length];
			}
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x0012F398 File Offset: 0x0012D598
		internal void RecordFixup(long objectId, string name, long idRef)
		{
			if (this.isSi)
			{
				this.objectManager.RecordDelayedFixup(objectId, name, idRef);
				return;
			}
			int num = this.Position(name);
			if (num != -1)
			{
				this.objectManager.RecordFixup(objectId, this.cache.memberInfos[num], idRef);
			}
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x0012F3E2 File Offset: 0x0012D5E2
		[SecurityCritical]
		internal void PopulateObjectMembers(object obj, object[] memberData)
		{
			if (!this.isSi && memberData != null)
			{
				FormatterServices.PopulateObjectMembers(obj, this.cache.memberInfos, memberData);
			}
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x0012F404 File Offset: 0x0012D604
		[Conditional("SER_LOGGING")]
		private void DumpPopulate(MemberInfo[] memberInfos, object[] memberData)
		{
			for (int i = 0; i < memberInfos.Length; i++)
			{
			}
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x0012F41F File Offset: 0x0012D61F
		[Conditional("SER_LOGGING")]
		private void DumpPopulateSi()
		{
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x0012F424 File Offset: 0x0012D624
		private int Position(string name)
		{
			if (this.cache == null)
			{
				return -1;
			}
			if (this.cache.memberNames.Length != 0 && this.cache.memberNames[this.lastPosition].Equals(name))
			{
				return this.lastPosition;
			}
			int num = this.lastPosition + 1;
			this.lastPosition = num;
			if (num < this.cache.memberNames.Length && this.cache.memberNames[this.lastPosition].Equals(name))
			{
				return this.lastPosition;
			}
			for (int i = 0; i < this.cache.memberNames.Length; i++)
			{
				if (this.cache.memberNames[i].Equals(name))
				{
					this.lastPosition = i;
					return this.lastPosition;
				}
			}
			this.lastPosition = 0;
			return -1;
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x0012F4F0 File Offset: 0x0012D6F0
		internal Type[] GetMemberTypes(string[] inMemberNames, Type objectType)
		{
			if (this.isSi)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_ISerializableTypes", new object[] { objectType }));
			}
			if (this.cache == null)
			{
				return null;
			}
			if (this.cache.memberTypes == null)
			{
				this.cache.memberTypes = new Type[this.count];
				for (int i = 0; i < this.count; i++)
				{
					this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
				}
			}
			bool flag = false;
			if (inMemberNames.Length < this.cache.memberInfos.Length)
			{
				flag = true;
			}
			Type[] array = new Type[this.cache.memberInfos.Length];
			for (int j = 0; j < this.cache.memberInfos.Length; j++)
			{
				if (!flag && inMemberNames[j].Equals(this.cache.memberInfos[j].Name))
				{
					array[j] = this.cache.memberTypes[j];
				}
				else
				{
					bool flag2 = false;
					for (int k = 0; k < inMemberNames.Length; k++)
					{
						if (this.cache.memberInfos[j].Name.Equals(inMemberNames[k]))
						{
							array[j] = this.cache.memberTypes[j];
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						object[] customAttributes = this.cache.memberInfos[j].GetCustomAttributes(typeof(OptionalFieldAttribute), false);
						if ((customAttributes == null || customAttributes.Length == 0) && !this.bSimpleAssembly)
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_MissingMember", new object[]
							{
								this.cache.memberNames[j],
								objectType,
								typeof(OptionalFieldAttribute).FullName
							}));
						}
					}
				}
			}
			return array;
		}

		// Token: 0x060054FF RID: 21759 RVA: 0x0012F6BC File Offset: 0x0012D8BC
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

		// Token: 0x06005500 RID: 21760 RVA: 0x0012F718 File Offset: 0x0012D918
		private static ReadObjectInfo GetObjectInfo(SerObjectInfoInit serObjectInfoInit)
		{
			return new ReadObjectInfo
			{
				objectInfoId = Interlocked.Increment(ref ReadObjectInfo.readObjectInfoCounter)
			};
		}

		// Token: 0x040026FA RID: 9978
		internal int objectInfoId;

		// Token: 0x040026FB RID: 9979
		internal static int readObjectInfoCounter;

		// Token: 0x040026FC RID: 9980
		internal Type objectType;

		// Token: 0x040026FD RID: 9981
		internal ObjectManager objectManager;

		// Token: 0x040026FE RID: 9982
		internal int count;

		// Token: 0x040026FF RID: 9983
		internal bool isSi;

		// Token: 0x04002700 RID: 9984
		internal bool isNamed;

		// Token: 0x04002701 RID: 9985
		internal bool isTyped;

		// Token: 0x04002702 RID: 9986
		internal bool bSimpleAssembly;

		// Token: 0x04002703 RID: 9987
		internal SerObjectInfoCache cache;

		// Token: 0x04002704 RID: 9988
		internal string[] wireMemberNames;

		// Token: 0x04002705 RID: 9989
		internal Type[] wireMemberTypes;

		// Token: 0x04002706 RID: 9990
		private int lastPosition;

		// Token: 0x04002707 RID: 9991
		internal ISurrogateSelector surrogateSelector;

		// Token: 0x04002708 RID: 9992
		internal ISerializationSurrogate serializationSurrogate;

		// Token: 0x04002709 RID: 9993
		internal StreamingContext context;

		// Token: 0x0400270A RID: 9994
		internal List<Type> memberTypesList;

		// Token: 0x0400270B RID: 9995
		internal SerObjectInfoInit serObjectInfoInit;

		// Token: 0x0400270C RID: 9996
		internal IFormatterConverter formatterConverter;
	}
}
