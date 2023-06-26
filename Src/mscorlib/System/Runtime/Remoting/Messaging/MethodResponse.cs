using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface to create a message that acts as a method response on a remote object.</summary>
	// Token: 0x02000869 RID: 2153
	[SecurityCritical]
	[CLSCompliant(false)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class MethodResponse : IMethodReturnMessage, IMethodMessage, IMessage, ISerializable, ISerializationRootObject, IInternalMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> class from an array of remoting headers and a request message.</summary>
		/// <param name="h1">An array of remoting headers that contains key/value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> fields for headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		/// <param name="mcm">A request message that acts as a method call on a remote object.</param>
		// Token: 0x06005BB6 RID: 23478 RVA: 0x00142C64 File Offset: 0x00140E64
		[SecurityCritical]
		public MethodResponse(Header[] h1, IMethodCallMessage mcm)
		{
			if (mcm == null)
			{
				throw new ArgumentNullException("mcm");
			}
			Message message = mcm as Message;
			if (message != null)
			{
				this.MI = message.GetMethodBase();
			}
			else
			{
				this.MI = mcm.MethodBase;
			}
			if (this.MI == null)
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), mcm.MethodName, mcm.TypeName));
			}
			this._methodCache = InternalRemotingServices.GetReflectionCachedData(this.MI);
			this.argCount = this._methodCache.Parameters.Length;
			this.fSoap = true;
			this.FillHeaders(h1);
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x00142D10 File Offset: 0x00140F10
		[SecurityCritical]
		internal MethodResponse(IMethodCallMessage msg, SmuggledMethodReturnMessage smuggledMrm, ArrayList deserializedArgs)
		{
			this.MI = msg.MethodBase;
			this._methodCache = InternalRemotingServices.GetReflectionCachedData(this.MI);
			this.methodName = msg.MethodName;
			this.uri = msg.Uri;
			this.typeName = msg.TypeName;
			if (this._methodCache.IsOverloaded())
			{
				this.methodSignature = (Type[])msg.MethodSignature;
			}
			this.retVal = smuggledMrm.GetReturnValue(deserializedArgs);
			this.outArgs = smuggledMrm.GetArgs(deserializedArgs);
			this.fault = smuggledMrm.GetException(deserializedArgs);
			this.callContext = smuggledMrm.GetCallContext(deserializedArgs);
			if (smuggledMrm.MessagePropertyCount > 0)
			{
				smuggledMrm.PopulateMessageProperties(this.Properties, deserializedArgs);
			}
			this.argCount = this._methodCache.Parameters.Length;
			this.fSoap = false;
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x00142DE8 File Offset: 0x00140FE8
		[SecurityCritical]
		internal MethodResponse(IMethodCallMessage msg, object handlerObject, BinaryMethodReturnMessage smuggledMrm)
		{
			if (msg != null)
			{
				this.MI = msg.MethodBase;
				this._methodCache = InternalRemotingServices.GetReflectionCachedData(this.MI);
				this.methodName = msg.MethodName;
				this.uri = msg.Uri;
				this.typeName = msg.TypeName;
				if (this._methodCache.IsOverloaded())
				{
					this.methodSignature = (Type[])msg.MethodSignature;
				}
				this.argCount = this._methodCache.Parameters.Length;
			}
			this.retVal = smuggledMrm.ReturnValue;
			this.outArgs = smuggledMrm.Args;
			this.fault = smuggledMrm.Exception;
			this.callContext = smuggledMrm.LogicalCallContext;
			if (smuggledMrm.HasProperties)
			{
				smuggledMrm.PopulateMessageProperties(this.Properties);
			}
			this.fSoap = false;
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x00142EBB File Offset: 0x001410BB
		[SecurityCritical]
		internal MethodResponse(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.SetObjectData(info, context);
		}

		/// <summary>Initializes an internal serialization handler from an array of remoting headers that are applied to a method.</summary>
		/// <param name="h">An array of remoting headers that contain key/value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> fields for headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		/// <returns>An internal serialization handler.</returns>
		// Token: 0x06005BBA RID: 23482 RVA: 0x00142EDC File Offset: 0x001410DC
		[SecurityCritical]
		public virtual object HeaderHandler(Header[] h)
		{
			SerializationMonkey serializationMonkey = (SerializationMonkey)FormatterServices.GetUninitializedObject(typeof(SerializationMonkey));
			Header[] array;
			if (h != null && h.Length != 0 && h[0].Name == "__methodName")
			{
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
			Type type = null;
			MethodInfo methodInfo = this.MI as MethodInfo;
			if (methodInfo != null)
			{
				type = methodInfo.ReturnType;
			}
			ParameterInfo[] parameters = this._methodCache.Parameters;
			int num = this._methodCache.MarshalResponseArgMap.Length;
			if (!(type == null) && !(type == typeof(void)))
			{
				num++;
			}
			Type[] array2 = new Type[num];
			string[] array3 = new string[num];
			int num2 = 0;
			if (!(type == null) && !(type == typeof(void)))
			{
				array2[num2++] = type;
			}
			foreach (int num3 in this._methodCache.MarshalResponseArgMap)
			{
				array3[num2] = parameters[num3].Name;
				if (parameters[num3].ParameterType.IsByRef)
				{
					array2[num2++] = parameters[num3].ParameterType.GetElementType();
				}
				else
				{
					array2[num2++] = parameters[num3].ParameterType;
				}
			}
			((IFieldInfo)serializationMonkey).FieldTypes = array2;
			((IFieldInfo)serializationMonkey).FieldNames = array3;
			this.FillHeaders(array, true);
			serializationMonkey._obj = this;
			return serializationMonkey;
		}

		/// <summary>Sets method information from serialization settings.</summary>
		/// <param name="info">The data for serializing or deserializing the remote object.</param>
		/// <param name="ctx">The context of a certain serialized stream.</param>
		// Token: 0x06005BBB RID: 23483 RVA: 0x0014306E File Offset: 0x0014126E
		[SecurityCritical]
		public void RootSetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			this.SetObjectData(info, ctx);
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x00143078 File Offset: 0x00141278
		[SecurityCritical]
		internal void SetObjectData(SerializationInfo info, StreamingContext ctx)
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
			bool flag = false;
			bool flag2 = false;
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("__return"))
				{
					flag = true;
					break;
				}
				if (enumerator.Name.Equals("__fault"))
				{
					flag2 = true;
					this.fault = (Exception)enumerator.Value;
					break;
				}
				this.FillHeader(enumerator.Name, enumerator.Value);
			}
			if (flag2 && flag)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
			}
		}

		/// <summary>The <see cref="M:System.Runtime.Remoting.Messaging.MethodResponse.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method is not implemented.</summary>
		/// <param name="info">Data for serializing or deserializing the remote object.</param>
		/// <param name="context">Context of a certain serialized stream.</param>
		// Token: 0x06005BBD RID: 23485 RVA: 0x0014311C File Offset: 0x0014131C
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x00143130 File Offset: 0x00141330
		internal void SetObjectFromSoapData(SerializationInfo info)
		{
			Hashtable hashtable = (Hashtable)info.GetValue("__keyToNamespaceTable", typeof(Hashtable));
			ArrayList arrayList = (ArrayList)info.GetValue("__paramNameList", typeof(ArrayList));
			SoapFault soapFault = (SoapFault)info.GetValue("__fault", typeof(SoapFault));
			if (soapFault != null)
			{
				ServerFault serverFault = soapFault.Detail as ServerFault;
				if (serverFault != null)
				{
					if (serverFault.Exception != null)
					{
						this.fault = serverFault.Exception;
						return;
					}
					Type type = Type.GetType(serverFault.ExceptionType, false, false);
					if (type == null)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append("\nException Type: ");
						stringBuilder.Append(serverFault.ExceptionType);
						stringBuilder.Append("\n");
						stringBuilder.Append("Exception Message: ");
						stringBuilder.Append(serverFault.ExceptionMessage);
						stringBuilder.Append("\n");
						stringBuilder.Append(serverFault.StackTrace);
						this.fault = new ServerException(stringBuilder.ToString());
						return;
					}
					object[] array = new object[] { serverFault.ExceptionMessage };
					this.fault = (Exception)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, array, null, null);
					return;
				}
				else
				{
					if (soapFault.Detail != null && soapFault.Detail.GetType() == typeof(string) && ((string)soapFault.Detail).Length != 0)
					{
						this.fault = new ServerException((string)soapFault.Detail);
						return;
					}
					this.fault = new ServerException(soapFault.FaultString);
					return;
				}
			}
			else
			{
				MethodInfo methodInfo = this.MI as MethodInfo;
				int num = 0;
				if (methodInfo != null)
				{
					Type returnType = methodInfo.ReturnType;
					if (returnType != typeof(void))
					{
						num++;
						object value = info.GetValue((string)arrayList[0], typeof(object));
						if (value is string)
						{
							this.retVal = Message.SoapCoerceArg(value, returnType, hashtable);
						}
						else
						{
							this.retVal = value;
						}
					}
				}
				ParameterInfo[] parameters = this._methodCache.Parameters;
				object obj = ((this.InternalProperties == null) ? null : this.InternalProperties["__UnorderedParams"]);
				if (obj != null && obj is bool && (bool)obj)
				{
					for (int i = num; i < arrayList.Count; i++)
					{
						string text = (string)arrayList[i];
						int num2 = -1;
						for (int j = 0; j < parameters.Length; j++)
						{
							if (text.Equals(parameters[j].Name))
							{
								num2 = parameters[j].Position;
							}
						}
						if (num2 == -1)
						{
							if (!text.StartsWith("__param", StringComparison.Ordinal))
							{
								throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
							}
							num2 = int.Parse(text.Substring(7), CultureInfo.InvariantCulture);
						}
						if (num2 >= this.argCount)
						{
							throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
						}
						if (this.outArgs == null)
						{
							this.outArgs = new object[this.argCount];
						}
						this.outArgs[num2] = Message.SoapCoerceArg(info.GetValue(text, typeof(object)), parameters[num2].ParameterType, hashtable);
					}
					return;
				}
				if (this.argMapper == null)
				{
					this.argMapper = new ArgMapper(this, true);
				}
				for (int k = num; k < arrayList.Count; k++)
				{
					string text2 = (string)arrayList[k];
					if (this.outArgs == null)
					{
						this.outArgs = new object[this.argCount];
					}
					int num3 = this.argMapper.Map[k - num];
					this.outArgs[num3] = Message.SoapCoerceArg(info.GetValue(text2, typeof(object)), parameters[num3].ParameterType, hashtable);
				}
				return;
			}
		}

		// Token: 0x06005BBF RID: 23487 RVA: 0x0014352D File Offset: 0x0014172D
		[SecurityCritical]
		internal LogicalCallContext GetLogicalCallContext()
		{
			if (this.callContext == null)
			{
				this.callContext = new LogicalCallContext();
			}
			return this.callContext;
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x00143548 File Offset: 0x00141748
		internal LogicalCallContext SetLogicalCallContext(LogicalCallContext ctx)
		{
			LogicalCallContext logicalCallContext = this.callContext;
			this.callContext = ctx;
			return logicalCallContext;
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the remote object on which the method call is being made.</summary>
		/// <returns>The URI of a remote object.</returns>
		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06005BC1 RID: 23489 RVA: 0x00143564 File Offset: 0x00141764
		// (set) Token: 0x06005BC2 RID: 23490 RVA: 0x0014356C File Offset: 0x0014176C
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

		/// <summary>Gets the name of the invoked method.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the invoked method.</returns>
		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06005BC3 RID: 23491 RVA: 0x00143575 File Offset: 0x00141775
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
		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06005BC4 RID: 23492 RVA: 0x0014357D File Offset: 0x0014177D
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
		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06005BC5 RID: 23493 RVA: 0x00143585 File Offset: 0x00141785
		public object MethodSignature
		{
			[SecurityCritical]
			get
			{
				return this.methodSignature;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06005BC6 RID: 23494 RVA: 0x0014358D File Offset: 0x0014178D
		public MethodBase MethodBase
		{
			[SecurityCritical]
			get
			{
				return this.MI;
			}
		}

		/// <summary>Gets a value that indicates whether the method can accept a variable number of arguments.</summary>
		/// <returns>
		///   <see langword="true" /> if the method can accept a variable number of arguments; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06005BC7 RID: 23495 RVA: 0x00143595 File Offset: 0x00141795
		public bool HasVarArgs
		{
			[SecurityCritical]
			get
			{
				return false;
			}
		}

		/// <summary>Gets the number of arguments passed to the method.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments passed to a method.</returns>
		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06005BC8 RID: 23496 RVA: 0x00143598 File Offset: 0x00141798
		public int ArgCount
		{
			[SecurityCritical]
			get
			{
				if (this.outArgs == null)
				{
					return 0;
				}
				return this.outArgs.Length;
			}
		}

		/// <summary>Gets a method argument, as an object, at a specified index.</summary>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <returns>The method argument as an object.</returns>
		// Token: 0x06005BC9 RID: 23497 RVA: 0x001435AC File Offset: 0x001417AC
		[SecurityCritical]
		public object GetArg(int argNum)
		{
			return this.outArgs[argNum];
		}

		/// <summary>Gets the name of a method argument at a specified index.</summary>
		/// <param name="index">The index of the requested argument.</param>
		/// <returns>The name of the method argument.</returns>
		// Token: 0x06005BCA RID: 23498 RVA: 0x001435B8 File Offset: 0x001417B8
		[SecurityCritical]
		public string GetArgName(int index)
		{
			if (!(this.MI != null))
			{
				return "__param" + index.ToString();
			}
			RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.MI);
			ParameterInfo[] parameters = reflectionCachedData.Parameters;
			if (index < 0 || index >= parameters.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return reflectionCachedData.Parameters[index].Name;
		}

		/// <summary>Gets an array of arguments passed to the method.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments passed to a method.</returns>
		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06005BCB RID: 23499 RVA: 0x0014361A File Offset: 0x0014181A
		public object[] Args
		{
			[SecurityCritical]
			get
			{
				return this.outArgs;
			}
		}

		/// <summary>Gets the number of arguments in the method call marked as <see langword="ref" /> or <see langword="out" /> parameters.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments in the method call marked as <see langword="ref" /> or <see langword="out" /> parameters.</returns>
		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06005BCC RID: 23500 RVA: 0x00143622 File Offset: 0x00141822
		public int OutArgCount
		{
			[SecurityCritical]
			get
			{
				if (this.argMapper == null)
				{
					this.argMapper = new ArgMapper(this, true);
				}
				return this.argMapper.ArgCount;
			}
		}

		/// <summary>Returns the specified argument marked as a <see langword="ref" /> parameter or an <see langword="out" /> parameter.</summary>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <returns>The specified argument marked as a <see langword="ref" /> parameter or an <see langword="out" /> parameter.</returns>
		// Token: 0x06005BCD RID: 23501 RVA: 0x00143644 File Offset: 0x00141844
		[SecurityCritical]
		public object GetOutArg(int argNum)
		{
			if (this.argMapper == null)
			{
				this.argMapper = new ArgMapper(this, true);
			}
			return this.argMapper.GetArg(argNum);
		}

		/// <summary>Returns the name of the specified argument marked as a <see langword="ref" /> parameter or an <see langword="out" /> parameter.</summary>
		/// <param name="index">The index of the requested argument.</param>
		/// <returns>The argument name, or <see langword="null" /> if the current method is not implemented.</returns>
		// Token: 0x06005BCE RID: 23502 RVA: 0x00143667 File Offset: 0x00141867
		[SecurityCritical]
		public string GetOutArgName(int index)
		{
			if (this.argMapper == null)
			{
				this.argMapper = new ArgMapper(this, true);
			}
			return this.argMapper.GetArgName(index);
		}

		/// <summary>Gets an array of arguments in the method call that are marked as <see langword="ref" /> or <see langword="out" /> parameters.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments in the method call that are marked as <see langword="ref" /> or <see langword="out" /> parameters.</returns>
		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06005BCF RID: 23503 RVA: 0x0014368A File Offset: 0x0014188A
		public object[] OutArgs
		{
			[SecurityCritical]
			get
			{
				if (this.argMapper == null)
				{
					this.argMapper = new ArgMapper(this, true);
				}
				return this.argMapper.Args;
			}
		}

		/// <summary>Gets the exception thrown during the method call, or <see langword="null" /> if the method did not throw an exception.</summary>
		/// <returns>The <see cref="T:System.Exception" /> thrown during the method call, or <see langword="null" /> if the method did not throw an exception.</returns>
		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06005BD0 RID: 23504 RVA: 0x001436AC File Offset: 0x001418AC
		public Exception Exception
		{
			[SecurityCritical]
			get
			{
				return this.fault;
			}
		}

		/// <summary>Gets the return value of the method call.</summary>
		/// <returns>A <see cref="T:System.Object" /> that represents the return value of the method call.</returns>
		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06005BD1 RID: 23505 RVA: 0x001436B4 File Offset: 0x001418B4
		public object ReturnValue
		{
			[SecurityCritical]
			get
			{
				return this.retVal;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06005BD2 RID: 23506 RVA: 0x001436BC File Offset: 0x001418BC
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
						this.ExternalProperties = new MRMDictionary(this, this.InternalProperties);
					}
					externalProperties = this.ExternalProperties;
				}
				return externalProperties;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06005BD3 RID: 23507 RVA: 0x00143728 File Offset: 0x00141928
		public LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				return this.GetLogicalCallContext();
			}
		}

		// Token: 0x06005BD4 RID: 23508 RVA: 0x00143730 File Offset: 0x00141930
		[SecurityCritical]
		internal void FillHeaders(Header[] h)
		{
			this.FillHeaders(h, false);
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x0014373C File Offset: 0x0014193C
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

		// Token: 0x06005BD6 RID: 23510 RVA: 0x001437C4 File Offset: 0x001419C4
		[SecurityCritical]
		internal void FillHeader(string name, object value)
		{
			if (name.Equals("__MethodName"))
			{
				this.methodName = (string)value;
				return;
			}
			if (name.Equals("__Uri"))
			{
				this.uri = (string)value;
				return;
			}
			if (name.Equals("__MethodSignature"))
			{
				this.methodSignature = (Type[])value;
				return;
			}
			if (name.Equals("__TypeName"))
			{
				this.typeName = (string)value;
				return;
			}
			if (name.Equals("__OutArgs"))
			{
				this.outArgs = (object[])value;
				return;
			}
			if (name.Equals("__CallContext"))
			{
				if (value is string)
				{
					this.callContext = new LogicalCallContext();
					this.callContext.RemotingData.LogicalCallID = (string)value;
					return;
				}
				this.callContext = (LogicalCallContext)value;
				return;
			}
			else
			{
				if (name.Equals("__Return"))
				{
					this.retVal = value;
					return;
				}
				if (this.InternalProperties == null)
				{
					this.InternalProperties = new Hashtable();
				}
				this.InternalProperties[name] = value;
				return;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06005BD7 RID: 23511 RVA: 0x001438CC File Offset: 0x00141ACC
		// (set) Token: 0x06005BD8 RID: 23512 RVA: 0x001438CF File Offset: 0x00141ACF
		ServerIdentity IInternalMessage.ServerIdentityObject
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06005BD9 RID: 23513 RVA: 0x001438D1 File Offset: 0x00141AD1
		// (set) Token: 0x06005BDA RID: 23514 RVA: 0x001438D4 File Offset: 0x00141AD4
		Identity IInternalMessage.IdentityObject
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
			}
		}

		// Token: 0x06005BDB RID: 23515 RVA: 0x001438D6 File Offset: 0x00141AD6
		[SecurityCritical]
		void IInternalMessage.SetURI(string val)
		{
			this.uri = val;
		}

		// Token: 0x06005BDC RID: 23516 RVA: 0x001438DF File Offset: 0x00141ADF
		[SecurityCritical]
		void IInternalMessage.SetCallContext(LogicalCallContext newCallContext)
		{
			this.callContext = newCallContext;
		}

		// Token: 0x06005BDD RID: 23517 RVA: 0x001438E8 File Offset: 0x00141AE8
		[SecurityCritical]
		bool IInternalMessage.HasProperties()
		{
			return this.ExternalProperties != null || this.InternalProperties != null;
		}

		// Token: 0x04002978 RID: 10616
		private MethodBase MI;

		// Token: 0x04002979 RID: 10617
		private string methodName;

		// Token: 0x0400297A RID: 10618
		private Type[] methodSignature;

		// Token: 0x0400297B RID: 10619
		private string uri;

		// Token: 0x0400297C RID: 10620
		private string typeName;

		// Token: 0x0400297D RID: 10621
		private object retVal;

		// Token: 0x0400297E RID: 10622
		private Exception fault;

		// Token: 0x0400297F RID: 10623
		private object[] outArgs;

		// Token: 0x04002980 RID: 10624
		private LogicalCallContext callContext;

		/// <summary>Specifies an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		// Token: 0x04002981 RID: 10625
		protected IDictionary InternalProperties;

		/// <summary>Specifies an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</summary>
		// Token: 0x04002982 RID: 10626
		protected IDictionary ExternalProperties;

		// Token: 0x04002983 RID: 10627
		private int argCount;

		// Token: 0x04002984 RID: 10628
		private bool fSoap;

		// Token: 0x04002985 RID: 10629
		private ArgMapper argMapper;

		// Token: 0x04002986 RID: 10630
		private RemotingMethodCachedData _methodCache;
	}
}
