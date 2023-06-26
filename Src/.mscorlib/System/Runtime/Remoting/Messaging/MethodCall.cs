using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> interface to create a request message that acts as a method call on a remote object.</summary>
	// Token: 0x02000867 RID: 2151
	[SecurityCritical]
	[CLSCompliant(false)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class MethodCall : IMethodCallMessage, IMethodMessage, IMessage, ISerializable, IInternalMessage, ISerializationRootObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> class from an array of remoting headers.</summary>
		/// <param name="h1">An array of remoting headers that contains key/value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> fields for headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		// Token: 0x06005B7A RID: 23418 RVA: 0x00141A8B File Offset: 0x0013FC8B
		[SecurityCritical]
		public MethodCall(Header[] h1)
		{
			this.Init();
			this.fSoap = true;
			this.FillHeaders(h1);
			this.ResolveMethod();
		}

		/// <summary>Initializes  a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> class by copying an existing message.</summary>
		/// <param name="msg">A remoting message.</param>
		// Token: 0x06005B7B RID: 23419 RVA: 0x00141AB0 File Offset: 0x0013FCB0
		[SecurityCritical]
		public MethodCall(IMessage msg)
		{
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
			this.Init();
			IDictionaryEnumerator enumerator = msg.Properties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.FillHeader(enumerator.Key.ToString(), enumerator.Value);
			}
			IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
			if (methodCallMessage != null)
			{
				this.MI = methodCallMessage.MethodBase;
			}
			this.ResolveMethod();
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x00141B20 File Offset: 0x0013FD20
		[SecurityCritical]
		internal MethodCall(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Init();
			this.SetObjectData(info, context);
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x00141B44 File Offset: 0x0013FD44
		[SecurityCritical]
		internal MethodCall(SmuggledMethodCallMessage smuggledMsg, ArrayList deserializedArgs)
		{
			this.uri = smuggledMsg.Uri;
			this.typeName = smuggledMsg.TypeName;
			this.methodName = smuggledMsg.MethodName;
			this.methodSignature = (Type[])smuggledMsg.GetMethodSignature(deserializedArgs);
			this.args = smuggledMsg.GetArgs(deserializedArgs);
			this.instArgs = smuggledMsg.GetInstantiation(deserializedArgs);
			this.callContext = smuggledMsg.GetCallContext(deserializedArgs);
			this.ResolveMethod();
			if (smuggledMsg.MessagePropertyCount > 0)
			{
				smuggledMsg.PopulateMessageProperties(this.Properties, deserializedArgs);
			}
		}

		// Token: 0x06005B7E RID: 23422 RVA: 0x00141BD0 File Offset: 0x0013FDD0
		[SecurityCritical]
		internal MethodCall(object handlerObject, BinaryMethodCallMessage smuggledMsg)
		{
			if (handlerObject != null)
			{
				this.uri = handlerObject as string;
				if (this.uri == null)
				{
					MarshalByRefObject marshalByRefObject = handlerObject as MarshalByRefObject;
					if (marshalByRefObject != null)
					{
						bool flag;
						this.srvID = MarshalByRefObject.GetIdentity(marshalByRefObject, out flag) as ServerIdentity;
						this.uri = this.srvID.URI;
					}
				}
			}
			this.typeName = smuggledMsg.TypeName;
			this.methodName = smuggledMsg.MethodName;
			this.methodSignature = (Type[])smuggledMsg.MethodSignature;
			this.args = smuggledMsg.Args;
			this.instArgs = smuggledMsg.InstantiationArgs;
			this.callContext = smuggledMsg.LogicalCallContext;
			this.ResolveMethod();
			if (smuggledMsg.HasProperties)
			{
				smuggledMsg.PopulateMessageProperties(this.Properties);
			}
		}

		/// <summary>Sets method information from serialization settings.</summary>
		/// <param name="info">The data for serializing or deserializing the remote object.</param>
		/// <param name="ctx">The context of a given serialized stream.</param>
		// Token: 0x06005B7F RID: 23423 RVA: 0x00141C8F File Offset: 0x0013FE8F
		[SecurityCritical]
		public void RootSetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			this.SetObjectData(info, ctx);
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x00141C9C File Offset: 0x0013FE9C
		[SecurityCritical]
		internal void SetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.fSoap)
			{
				this.SetObjectFromSoapData(info);
				return;
			}
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.FillHeader(enumerator.Name, enumerator.Value);
			}
			if (context.State == StreamingContextStates.Remoting && context.Context != null)
			{
				Header[] array = context.Context as Header[];
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						this.FillHeader(array[i].Name, array[i].Value);
					}
				}
			}
		}

		// Token: 0x06005B81 RID: 23425 RVA: 0x00141D34 File Offset: 0x0013FF34
		private static Type ResolveTypeRelativeTo(string typeName, int offset, int count, Type serverType)
		{
			Type type = MethodCall.ResolveTypeRelativeToBaseTypes(typeName, offset, count, serverType);
			if (type == null)
			{
				Type[] interfaces = serverType.GetInterfaces();
				foreach (Type type2 in interfaces)
				{
					string fullName = type2.FullName;
					if (fullName.Length == count && string.CompareOrdinal(typeName, offset, fullName, 0, count) == 0)
					{
						return type2;
					}
				}
			}
			return type;
		}

		// Token: 0x06005B82 RID: 23426 RVA: 0x00141D94 File Offset: 0x0013FF94
		private static Type ResolveTypeRelativeToBaseTypes(string typeName, int offset, int count, Type serverType)
		{
			if (typeName == null || serverType == null)
			{
				return null;
			}
			string fullName = serverType.FullName;
			if (fullName.Length == count && string.CompareOrdinal(typeName, offset, fullName, 0, count) == 0)
			{
				return serverType;
			}
			return MethodCall.ResolveTypeRelativeToBaseTypes(typeName, offset, count, serverType.BaseType);
		}

		// Token: 0x06005B83 RID: 23427 RVA: 0x00141DDC File Offset: 0x0013FFDC
		internal Type ResolveType()
		{
			Type type = null;
			if (this.srvID == null)
			{
				this.srvID = IdentityHolder.CasualResolveIdentity(this.uri) as ServerIdentity;
			}
			if (this.srvID != null)
			{
				Type type2 = this.srvID.GetLastCalledType(this.typeName);
				if (type2 != null)
				{
					return type2;
				}
				int num = 0;
				if (string.CompareOrdinal(this.typeName, 0, "clr:", 0, 4) == 0)
				{
					num = 4;
				}
				int num2 = this.typeName.IndexOf(',', num);
				if (num2 == -1)
				{
					num2 = this.typeName.Length;
				}
				type2 = this.srvID.ServerType;
				type = MethodCall.ResolveTypeRelativeTo(this.typeName, num, num2 - num, type2);
			}
			if (type == null)
			{
				type = RemotingServices.InternalGetTypeFromQualifiedTypeName(this.typeName);
			}
			if (this.srvID != null)
			{
				this.srvID.SetLastCalledType(this.typeName, type);
			}
			return type;
		}

		/// <summary>Sets method information from previously initialized remoting message properties.</summary>
		// Token: 0x06005B84 RID: 23428 RVA: 0x00141EB3 File Offset: 0x001400B3
		[SecurityCritical]
		public void ResolveMethod()
		{
			this.ResolveMethod(true);
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x00141EBC File Offset: 0x001400BC
		[SecurityCritical]
		internal void ResolveMethod(bool bThrowIfNotResolved)
		{
			if (this.MI == null && this.methodName != null)
			{
				RuntimeType runtimeType = this.ResolveType() as RuntimeType;
				if (this.methodName.Equals(".ctor"))
				{
					return;
				}
				if (runtimeType == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), this.typeName));
				}
				if (this.methodSignature != null)
				{
					bool flag = false;
					int num = ((this.instArgs == null) ? 0 : this.instArgs.Length);
					if (num == 0)
					{
						try
						{
							this.MI = runtimeType.GetMethod(this.methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, this.methodSignature, null);
							flag = true;
						}
						catch (AmbiguousMatchException)
						{
						}
					}
					if (!flag)
					{
						MemberInfo[] array = runtimeType.FindMembers(MemberTypes.Method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, Type.FilterName, this.methodName);
						int num2 = 0;
						for (int i = 0; i < array.Length; i++)
						{
							try
							{
								MethodInfo methodInfo = (MethodInfo)array[i];
								int num3 = (methodInfo.IsGenericMethod ? methodInfo.GetGenericArguments().Length : 0);
								if (num3 == num)
								{
									if (num > 0)
									{
										methodInfo = methodInfo.MakeGenericMethod(this.instArgs);
									}
									array[num2] = methodInfo;
									num2++;
								}
							}
							catch (ArgumentException)
							{
							}
							catch (VerificationException)
							{
							}
						}
						MethodInfo[] array2 = new MethodInfo[num2];
						for (int j = 0; j < num2; j++)
						{
							array2[j] = (MethodInfo)array[j];
						}
						Binder defaultBinder = Type.DefaultBinder;
						BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
						MethodBase[] array3 = array2;
						this.MI = defaultBinder.SelectMethod(bindingFlags, array3, this.methodSignature, null);
					}
				}
				else
				{
					RemotingTypeCachedData remotingTypeCachedData = null;
					if (this.instArgs == null)
					{
						remotingTypeCachedData = InternalRemotingServices.GetReflectionCachedData(runtimeType);
						this.MI = remotingTypeCachedData.GetLastCalledMethod(this.methodName);
						if (this.MI != null)
						{
							return;
						}
					}
					bool flag2 = false;
					try
					{
						this.MI = runtimeType.GetMethod(this.methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
						if (this.instArgs != null && this.instArgs.Length != 0)
						{
							this.MI = ((MethodInfo)this.MI).MakeGenericMethod(this.instArgs);
						}
					}
					catch (AmbiguousMatchException)
					{
						flag2 = true;
						this.ResolveOverloadedMethod(runtimeType);
					}
					if (this.MI != null && !flag2 && remotingTypeCachedData != null)
					{
						remotingTypeCachedData.SetLastCalledMethod(this.methodName, this.MI);
					}
				}
				if (this.MI == null && bThrowIfNotResolved)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), this.methodName, this.typeName));
				}
			}
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x0014215C File Offset: 0x0014035C
		private void ResolveOverloadedMethod(RuntimeType t)
		{
			if (this.args == null)
			{
				return;
			}
			MemberInfo[] member = t.GetMember(this.methodName, MemberTypes.Method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			int num = member.Length;
			if (num == 1)
			{
				this.MI = member[0] as MethodBase;
				return;
			}
			if (num == 0)
			{
				return;
			}
			int num2 = this.args.Length;
			MethodBase methodBase = null;
			for (int i = 0; i < num; i++)
			{
				MethodBase methodBase2 = member[i] as MethodBase;
				if (methodBase2.GetParameters().Length == num2)
				{
					if (methodBase != null)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_AmbiguousMethod"));
					}
					methodBase = methodBase2;
				}
			}
			if (methodBase != null)
			{
				this.MI = methodBase;
			}
		}

		// Token: 0x06005B87 RID: 23431 RVA: 0x001421FC File Offset: 0x001403FC
		private void ResolveOverloadedMethod(RuntimeType t, string methodName, ArrayList argNames, ArrayList argValues)
		{
			MemberInfo[] member = t.GetMember(methodName, MemberTypes.Method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			int num = member.Length;
			if (num == 1)
			{
				this.MI = member[0] as MethodBase;
				return;
			}
			if (num == 0)
			{
				return;
			}
			MethodBase methodBase = null;
			for (int i = 0; i < num; i++)
			{
				MethodBase methodBase2 = member[i] as MethodBase;
				ParameterInfo[] parameters = methodBase2.GetParameters();
				if (parameters.Length == argValues.Count)
				{
					bool flag = true;
					for (int j = 0; j < parameters.Length; j++)
					{
						Type type = parameters[j].ParameterType;
						if (type.IsByRef)
						{
							type = type.GetElementType();
						}
						if (type != argValues[j].GetType())
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						methodBase = methodBase2;
						break;
					}
				}
			}
			if (methodBase == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_AmbiguousMethod"));
			}
			this.MI = methodBase;
		}

		/// <summary>The <see cref="M:System.Runtime.Remoting.Messaging.MethodCall.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method is not implemented.</summary>
		/// <param name="info">The data for serializing or deserializing the remote object.</param>
		/// <param name="context">The context of a certain serialized stream.</param>
		// Token: 0x06005B88 RID: 23432 RVA: 0x001422D9 File Offset: 0x001404D9
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x001422EC File Offset: 0x001404EC
		[SecurityCritical]
		internal void SetObjectFromSoapData(SerializationInfo info)
		{
			this.methodName = info.GetString("__methodName");
			ArrayList arrayList = (ArrayList)info.GetValue("__paramNameList", typeof(ArrayList));
			Hashtable hashtable = (Hashtable)info.GetValue("__keyToNamespaceTable", typeof(Hashtable));
			if (this.MI == null)
			{
				ArrayList arrayList2 = new ArrayList();
				ArrayList arrayList3 = arrayList;
				for (int i = 0; i < arrayList3.Count; i++)
				{
					arrayList2.Add(info.GetValue((string)arrayList3[i], typeof(object)));
				}
				RuntimeType runtimeType = this.ResolveType() as RuntimeType;
				if (runtimeType == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), this.typeName));
				}
				this.ResolveOverloadedMethod(runtimeType, this.methodName, arrayList3, arrayList2);
				if (this.MI == null)
				{
					throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), this.methodName, this.typeName));
				}
			}
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.MI);
			ParameterInfo[] parameters = reflectionCachedData.Parameters;
			int[] marshalRequestArgMap = reflectionCachedData.MarshalRequestArgMap;
			object obj = ((this.InternalProperties == null) ? null : this.InternalProperties["__UnorderedParams"]);
			this.args = new object[parameters.Length];
			if (obj != null && obj is bool && (bool)obj)
			{
				for (int j = 0; j < arrayList.Count; j++)
				{
					string text = (string)arrayList[j];
					int num = -1;
					for (int k = 0; k < parameters.Length; k++)
					{
						if (text.Equals(parameters[k].Name))
						{
							num = parameters[k].Position;
							break;
						}
					}
					if (num == -1)
					{
						if (!text.StartsWith("__param", StringComparison.Ordinal))
						{
							throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
						}
						num = int.Parse(text.Substring(7), CultureInfo.InvariantCulture);
					}
					if (num >= this.args.Length)
					{
						throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
					}
					this.args[num] = Message.SoapCoerceArg(info.GetValue(text, typeof(object)), parameters[num].ParameterType, hashtable);
				}
				return;
			}
			for (int l = 0; l < arrayList.Count; l++)
			{
				string text2 = (string)arrayList[l];
				this.args[marshalRequestArgMap[l]] = Message.SoapCoerceArg(info.GetValue(text2, typeof(object)), parameters[marshalRequestArgMap[l]].ParameterType, hashtable);
			}
			this.PopulateOutArguments(reflectionCachedData);
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x001425B4 File Offset: 0x001407B4
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void PopulateOutArguments(RemotingMethodCachedData methodCache)
		{
			ParameterInfo[] parameters = methodCache.Parameters;
			foreach (int num in methodCache.OutOnlyArgMap)
			{
				Type elementType = parameters[num].ParameterType.GetElementType();
				if (elementType.IsValueType)
				{
					this.args[num] = Activator.CreateInstance(elementType, true);
				}
			}
		}

		/// <summary>Initializes a <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" />.</summary>
		// Token: 0x06005B8B RID: 23435 RVA: 0x00142609 File Offset: 0x00140809
		public virtual void Init()
		{
		}

		/// <summary>Gets the number of arguments passed to a method.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments passed to a method.</returns>
		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06005B8C RID: 23436 RVA: 0x0014260B File Offset: 0x0014080B
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				if (this.args != null)
				{
					return this.args.Length;
				}
				return 0;
			}
		}

		/// <summary>Gets a method argument, as an object, at a specified index.</summary>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <returns>The method argument as an object.</returns>
		// Token: 0x06005B8D RID: 23437 RVA: 0x0014261F File Offset: 0x0014081F
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			return this.args[argNum];
		}

		/// <summary>Gets the name of a method argument at a specified index.</summary>
		/// <param name="index">The index of the requested argument.</param>
		/// <returns>The name of the method argument.</returns>
		// Token: 0x06005B8E RID: 23438 RVA: 0x0014262C File Offset: 0x0014082C
		[SecurityCritical]
		public string GetArgName(int index)
		{
			this.ResolveMethod();
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.MI);
			return reflectionCachedData.Parameters[index].Name;
		}

		/// <summary>Gets an array of arguments passed to a method.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments passed to a method.</returns>
		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06005B8F RID: 23439 RVA: 0x00142658 File Offset: 0x00140858
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return this.args;
			}
		}

		/// <summary>Gets the number of arguments in the method call that are not marked as <see langword="out" /> parameters.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments in the method call that are not marked as <see langword="out" /> parameters.</returns>
		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06005B90 RID: 23440 RVA: 0x00142660 File Offset: 0x00140860
		public int InArgCount
		{
			[SecurityCritical]
			get
			{
				if (this.argMapper == null)
				{
					this.argMapper = new ArgMapper(this, false);
				}
				return this.argMapper.ArgCount;
			}
		}

		/// <summary>Gets a method argument at a specified index that is not marked as an <see langword="out" /> parameter.</summary>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <returns>The method argument that is not marked as an <see langword="out" /> parameter.</returns>
		// Token: 0x06005B91 RID: 23441 RVA: 0x00142682 File Offset: 0x00140882
		[SecurityCritical]
		public object GetInArg(int argNum)
		{
			if (this.argMapper == null)
			{
				this.argMapper = new ArgMapper(this, false);
			}
			return this.argMapper.GetArg(argNum);
		}

		/// <summary>Gets the name of a method argument at a specified index that is not marked as an <see langword="out" /> parameter.</summary>
		/// <param name="index">The index of the requested argument.</param>
		/// <returns>The name of the method argument that is not marked as an <see langword="out" /> parameter.</returns>
		// Token: 0x06005B92 RID: 23442 RVA: 0x001426A5 File Offset: 0x001408A5
		[SecurityCritical]
		public string GetInArgName(int index)
		{
			if (this.argMapper == null)
			{
				this.argMapper = new ArgMapper(this, false);
			}
			return this.argMapper.GetArgName(index);
		}

		/// <summary>Gets an array of arguments in the method call that are not marked as <see langword="out" /> parameters.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents arguments in the method call that are not marked as <see langword="out" /> parameters.</returns>
		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06005B93 RID: 23443 RVA: 0x001426C8 File Offset: 0x001408C8
		public object[] InArgs
		{
			[SecurityCritical]
			get
			{
				if (this.argMapper == null)
				{
					this.argMapper = new ArgMapper(this, false);
				}
				return this.argMapper.Args;
			}
		}

		/// <summary>Gets the name of the invoked method.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the invoked method.</returns>
		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06005B94 RID: 23444 RVA: 0x001426EA File Offset: 0x001408EA
		public string MethodName
		{
			[SecurityCritical]
			get
			{
				return this.methodName;
			}
		}

		/// <summary>Gets the full type name of the remote object on which the method call is being made.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the full type name of the remote object on which the method call is being made.</returns>
		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06005B95 RID: 23445 RVA: 0x001426F2 File Offset: 0x001408F2
		public string TypeName
		{
			[SecurityCritical]
			get
			{
				return this.typeName;
			}
		}

		/// <summary>Gets an object that contains the method signature.</summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the method signature.</returns>
		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06005B96 RID: 23446 RVA: 0x001426FA File Offset: 0x001408FA
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				if (this.methodSignature != null)
				{
					return this.methodSignature;
				}
				if (this.MI != null)
				{
					this.methodSignature = Message.GenerateMethodSignature(this.MethodBase);
				}
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06005B97 RID: 23447 RVA: 0x0014272B File Offset: 0x0014092B
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				if (this.MI == null)
				{
					this.MI = RemotingServices.InternalGetMethodBaseFromMethodMessage(this);
				}
				return this.MI;
			}
		}

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) of the remote object on which the method call is being made.</summary>
		/// <returns>The URI of a remote object.</returns>
		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06005B98 RID: 23448 RVA: 0x0014274D File Offset: 0x0014094D
		// (set) Token: 0x06005B99 RID: 23449 RVA: 0x00142755 File Offset: 0x00140955
		public string Uri
		{
			[SecurityCritical]
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		/// <summary>Gets a value that indicates whether the method can accept a variable number of arguments.</summary>
		/// <returns>
		///   <see langword="true" /> if the method can accept a variable number of arguments; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06005B9A RID: 23450 RVA: 0x0014275E File Offset: 0x0014095E
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return this.fVarArgs;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06005B9B RID: 23451 RVA: 0x00142768 File Offset: 0x00140968
		public virtual IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				IDictionary externalProperties;
				lock (this)
				{
					if (this.InternalProperties == null)
					{
						this.InternalProperties = new Hashtable();
					}
					if (this.ExternalProperties == null)
					{
						this.ExternalProperties = new MCMDictionary(this, this.InternalProperties);
					}
					externalProperties = this.ExternalProperties;
				}
				return externalProperties;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06005B9C RID: 23452 RVA: 0x001427D4 File Offset: 0x001409D4
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this.GetLogicalCallContext();
			}
		}

		// Token: 0x06005B9D RID: 23453 RVA: 0x001427DC File Offset: 0x001409DC
		[SecurityCritical]
		internal LogicalCallContext GetLogicalCallContext()
		{
			if (this.callContext == null)
			{
				this.callContext = new LogicalCallContext();
			}
			return this.callContext;
		}

		// Token: 0x06005B9E RID: 23454 RVA: 0x001427F8 File Offset: 0x001409F8
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext ctx)
		{
			LogicalCallContext logicalCallContext = this.callContext;
			this.callContext = ctx;
			return logicalCallContext;
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06005B9F RID: 23455 RVA: 0x00142814 File Offset: 0x00140A14
		// (set) Token: 0x06005BA0 RID: 23456 RVA: 0x0014281C File Offset: 0x00140A1C
		ServerIdentity IInternalMessage.ServerIdentityObject
		{
			[SecurityCritical]
			get
			{
				return this.srvID;
			}
			[SecurityCritical]
			set
			{
				this.srvID = value;
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06005BA1 RID: 23457 RVA: 0x00142825 File Offset: 0x00140A25
		// (set) Token: 0x06005BA2 RID: 23458 RVA: 0x0014282D File Offset: 0x00140A2D
		Identity IInternalMessage.IdentityObject
		{
			[SecurityCritical]
			get
			{
				return this.identity;
			}
			[SecurityCritical]
			set
			{
				this.identity = value;
			}
		}

		// Token: 0x06005BA3 RID: 23459 RVA: 0x00142836 File Offset: 0x00140A36
		[SecurityCritical]
		void IInternalMessage.SetURI(string val)
		{
			this.uri = val;
		}

		// Token: 0x06005BA4 RID: 23460 RVA: 0x0014283F File Offset: 0x00140A3F
		[SecurityCritical]
		void IInternalMessage.SetCallContext(LogicalCallContext newCallContext)
		{
			this.callContext = newCallContext;
		}

		// Token: 0x06005BA5 RID: 23461 RVA: 0x00142848 File Offset: 0x00140A48
		[SecurityCritical]
		bool IInternalMessage.HasProperties()
		{
			return this.ExternalProperties != null || this.InternalProperties != null;
		}

		// Token: 0x06005BA6 RID: 23462 RVA: 0x0014285D File Offset: 0x00140A5D
		[SecurityCritical]
		internal void FillHeaders(Header[] h)
		{
			this.FillHeaders(h, false);
		}

		// Token: 0x06005BA7 RID: 23463 RVA: 0x00142868 File Offset: 0x00140A68
		[SecurityCritical]
		private void FillHeaders(Header[] h, bool bFromHeaderHandler)
		{
			if (h == null)
			{
				return;
			}
			if (bFromHeaderHandler && this.fSoap)
			{
				foreach (Header header in h)
				{
					if (header.HeaderNamespace == "http://schemas.microsoft.com/clr/soap/messageProperties")
					{
						this.FillHeader(header.Name, header.Value);
					}
					else
					{
						string propertyKeyForHeader = LogicalCallContext.GetPropertyKeyForHeader(header);
						this.FillHeader(propertyKeyForHeader, header);
					}
				}
				return;
			}
			for (int j = 0; j < h.Length; j++)
			{
				this.FillHeader(h[j].Name, h[j].Value);
			}
		}

		// Token: 0x06005BA8 RID: 23464 RVA: 0x001428F0 File Offset: 0x00140AF0
		[SecurityCritical]
		internal virtual bool FillSpecialHeader(string key, object value)
		{
			if (key != null)
			{
				if (key.Equals("__Uri"))
				{
					this.uri = (string)value;
				}
				else if (key.Equals("__MethodName"))
				{
					this.methodName = (string)value;
				}
				else if (key.Equals("__MethodSignature"))
				{
					this.methodSignature = (Type[])value;
				}
				else if (key.Equals("__TypeName"))
				{
					this.typeName = (string)value;
				}
				else if (key.Equals("__Args"))
				{
					this.args = (object[])value;
				}
				else
				{
					if (!key.Equals("__CallContext"))
					{
						return false;
					}
					if (value is string)
					{
						this.callContext = new LogicalCallContext();
						this.callContext.RemotingData.LogicalCallID = (string)value;
					}
					else
					{
						this.callContext = (LogicalCallContext)value;
					}
				}
			}
			return true;
		}

		// Token: 0x06005BA9 RID: 23465 RVA: 0x001429D9 File Offset: 0x00140BD9
		[SecurityCritical]
		internal void FillHeader(string key, object value)
		{
			if (!this.FillSpecialHeader(key, value))
			{
				if (this.InternalProperties == null)
				{
					this.InternalProperties = new Hashtable();
				}
				this.InternalProperties[key] = value;
			}
		}

		/// <summary>Initializes an internal serialization handler from an array of remoting headers that are applied to a method.</summary>
		/// <param name="h">An array of remoting headers that contain key/value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> fields for headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		/// <returns>An internal serialization handler.</returns>
		// Token: 0x06005BAA RID: 23466 RVA: 0x00142A08 File Offset: 0x00140C08
		[SecurityCritical]
		public virtual object HeaderHandler(Header[] h)
		{
			SerializationMonkey serializationMonkey = (SerializationMonkey)FormatterServices.GetUninitializedObject(typeof(SerializationMonkey));
			Header[] array;
			if (h != null && h.Length != 0 && h[0].Name == "__methodName")
			{
				this.methodName = (string)h[0].Value;
				if (h.Length > 1)
				{
					array = new Header[h.Length - 1];
					Array.Copy(h, 1, array, 0, h.Length - 1);
				}
				else
				{
					array = null;
				}
			}
			else
			{
				array = h;
			}
			this.FillHeaders(array, true);
			this.ResolveMethod(false);
			serializationMonkey._obj = this;
			if (this.MI != null)
			{
				ArgMapper argMapper = new ArgMapper(this.MI, false);
				serializationMonkey.fieldNames = argMapper.ArgNames;
				serializationMonkey.fieldTypes = argMapper.ArgTypes;
			}
			return serializationMonkey;
		}

		// Token: 0x04002962 RID: 10594
		private const BindingFlags LookupAll = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04002963 RID: 10595
		private const BindingFlags LookupPublic = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		// Token: 0x04002964 RID: 10596
		private string uri;

		// Token: 0x04002965 RID: 10597
		private string methodName;

		// Token: 0x04002966 RID: 10598
		private MethodBase MI;

		// Token: 0x04002967 RID: 10599
		private string typeName;

		// Token: 0x04002968 RID: 10600
		private object[] args;

		// Token: 0x04002969 RID: 10601
		private Type[] instArgs;

		// Token: 0x0400296A RID: 10602
		private LogicalCallContext callContext;

		// Token: 0x0400296B RID: 10603
		private Type[] methodSignature;

		/// <summary>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		// Token: 0x0400296C RID: 10604
		protected IDictionary ExternalProperties;

		/// <summary>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		// Token: 0x0400296D RID: 10605
		protected IDictionary InternalProperties;

		// Token: 0x0400296E RID: 10606
		private ServerIdentity srvID;

		// Token: 0x0400296F RID: 10607
		private Identity identity;

		// Token: 0x04002970 RID: 10608
		private bool fSoap;

		// Token: 0x04002971 RID: 10609
		private bool fVarArgs;

		// Token: 0x04002972 RID: 10610
		private ArgMapper argMapper;
	}
}
