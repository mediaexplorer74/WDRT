using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Text;

namespace System.Runtime.Remoting
{
	/// <summary>Provides several methods for using and publishing remoted objects in SOAP format.</summary>
	// Token: 0x020007CB RID: 1995
	[SecurityCritical]
	[ComVisible(true)]
	public class SoapServices
	{
		// Token: 0x060056BC RID: 22204 RVA: 0x00134DF3 File Offset: 0x00132FF3
		private SoapServices()
		{
		}

		// Token: 0x060056BD RID: 22205 RVA: 0x00134DFB File Offset: 0x00132FFB
		private static string CreateKey(string elementName, string elementNamespace)
		{
			if (elementNamespace == null)
			{
				return elementName;
			}
			return elementName + " " + elementNamespace;
		}

		/// <summary>Associates the given XML element name and namespace with a run-time type that should be used for deserialization.</summary>
		/// <param name="xmlElement">The XML element name to use in deserialization.</param>
		/// <param name="xmlNamespace">The XML namespace to use in deserialization.</param>
		/// <param name="type">The run-time <see cref="T:System.Type" /> to use in deserialization.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056BE RID: 22206 RVA: 0x00134E0E File Offset: 0x0013300E
		[SecurityCritical]
		public static void RegisterInteropXmlElement(string xmlElement, string xmlNamespace, Type type)
		{
			SoapServices._interopXmlElementToType[SoapServices.CreateKey(xmlElement, xmlNamespace)] = type;
			SoapServices._interopTypeToXmlElement[type] = new SoapServices.XmlEntry(xmlElement, xmlNamespace);
		}

		/// <summary>Associates the given XML type name and namespace with the run-time type that should be used for deserialization.</summary>
		/// <param name="xmlType">The XML type to use in deserialization.</param>
		/// <param name="xmlTypeNamespace">The XML namespace to use in deserialization.</param>
		/// <param name="type">The run-time <see cref="T:System.Type" /> to use in deserialization.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056BF RID: 22207 RVA: 0x00134E34 File Offset: 0x00133034
		[SecurityCritical]
		public static void RegisterInteropXmlType(string xmlType, string xmlTypeNamespace, Type type)
		{
			SoapServices._interopXmlTypeToType[SoapServices.CreateKey(xmlType, xmlTypeNamespace)] = type;
			SoapServices._interopTypeToXmlType[type] = new SoapServices.XmlEntry(xmlType, xmlTypeNamespace);
		}

		/// <summary>Preloads the given <see cref="T:System.Type" /> based on values set in a <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> on the type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to preload.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056C0 RID: 22208 RVA: 0x00134E5C File Offset: 0x0013305C
		[SecurityCritical]
		public static void PreLoad(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(type is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			MethodInfo[] methods = type.GetMethods();
			foreach (MethodInfo methodInfo in methods)
			{
				SoapServices.RegisterSoapActionForMethodBase(methodInfo);
			}
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (soapTypeAttribute.IsInteropXmlElement())
			{
				SoapServices.RegisterInteropXmlElement(soapTypeAttribute.XmlElementName, soapTypeAttribute.XmlNamespace, type);
			}
			if (soapTypeAttribute.IsInteropXmlType())
			{
				SoapServices.RegisterInteropXmlType(soapTypeAttribute.XmlTypeName, soapTypeAttribute.XmlTypeNamespace, type);
			}
			int num = 0;
			SoapServices.XmlToFieldTypeMap xmlToFieldTypeMap = new SoapServices.XmlToFieldTypeMap();
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				SoapFieldAttribute soapFieldAttribute = (SoapFieldAttribute)InternalRemotingServices.GetCachedSoapAttribute(fieldInfo);
				if (soapFieldAttribute.IsInteropXmlElement())
				{
					string xmlElementName = soapFieldAttribute.XmlElementName;
					string xmlNamespace = soapFieldAttribute.XmlNamespace;
					if (soapFieldAttribute.UseAttribute)
					{
						xmlToFieldTypeMap.AddXmlAttribute(fieldInfo.FieldType, fieldInfo.Name, xmlElementName, xmlNamespace);
					}
					else
					{
						xmlToFieldTypeMap.AddXmlElement(fieldInfo.FieldType, fieldInfo.Name, xmlElementName, xmlNamespace);
					}
					num++;
				}
			}
			if (num > 0)
			{
				SoapServices._xmlToFieldTypeMap[type] = xmlToFieldTypeMap;
			}
		}

		/// <summary>Preloads every <see cref="T:System.Type" /> found in the specified <see cref="T:System.Reflection.Assembly" /> from the information found in the <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" /> associated with each type.</summary>
		/// <param name="assembly">The <see cref="T:System.Reflection.Assembly" /> for each type of which to call <see cref="M:System.Runtime.Remoting.SoapServices.PreLoad(System.Type)" />.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056C1 RID: 22209 RVA: 0x00134FA4 File Offset: 0x001331A4
		[SecurityCritical]
		public static void PreLoad(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!(assembly is RuntimeAssembly))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), "assembly");
			}
			Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				SoapServices.PreLoad(type);
			}
		}

		/// <summary>Retrieves the <see cref="T:System.Type" /> that should be used during deserialization of an unrecognized object type with the given XML element name and namespace.</summary>
		/// <param name="xmlElement">The XML element name of the unknown object type.</param>
		/// <param name="xmlNamespace">The XML namespace of the unknown object type.</param>
		/// <returns>The <see cref="T:System.Type" /> of object associated with the specified XML element name and namespace.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056C2 RID: 22210 RVA: 0x00135003 File Offset: 0x00133203
		[SecurityCritical]
		public static Type GetInteropTypeFromXmlElement(string xmlElement, string xmlNamespace)
		{
			return (Type)SoapServices._interopXmlElementToType[SoapServices.CreateKey(xmlElement, xmlNamespace)];
		}

		/// <summary>Retrieves the object <see cref="T:System.Type" /> that should be used during deserialization of an unrecognized object type with the given XML type name and namespace.</summary>
		/// <param name="xmlType">The XML type of the unknown object type.</param>
		/// <param name="xmlTypeNamespace">The XML type namespace of the unknown object type.</param>
		/// <returns>The <see cref="T:System.Type" /> of object associated with the specified XML type name and namespace.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056C3 RID: 22211 RVA: 0x0013501B File Offset: 0x0013321B
		[SecurityCritical]
		public static Type GetInteropTypeFromXmlType(string xmlType, string xmlTypeNamespace)
		{
			return (Type)SoapServices._interopXmlTypeToType[SoapServices.CreateKey(xmlType, xmlTypeNamespace)];
		}

		/// <summary>Retrieves the <see cref="T:System.Type" /> and name of a field from the provided XML element name, namespace, and the containing type.</summary>
		/// <param name="containingType">The <see cref="T:System.Type" /> of the object that contains the field.</param>
		/// <param name="xmlElement">The XML element name of field.</param>
		/// <param name="xmlNamespace">The XML namespace of the field type.</param>
		/// <param name="type">When this method returns, contains a <see cref="T:System.Type" /> of the field. This parameter is passed uninitialized.</param>
		/// <param name="name">When this method returns, contains a <see cref="T:System.String" /> that holds the name of the field. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056C4 RID: 22212 RVA: 0x00135034 File Offset: 0x00133234
		public static void GetInteropFieldTypeAndNameFromXmlElement(Type containingType, string xmlElement, string xmlNamespace, out Type type, out string name)
		{
			if (containingType == null)
			{
				type = null;
				name = null;
				return;
			}
			SoapServices.XmlToFieldTypeMap xmlToFieldTypeMap = (SoapServices.XmlToFieldTypeMap)SoapServices._xmlToFieldTypeMap[containingType];
			if (xmlToFieldTypeMap != null)
			{
				xmlToFieldTypeMap.GetFieldTypeAndNameFromXmlElement(xmlElement, xmlNamespace, out type, out name);
				return;
			}
			type = null;
			name = null;
		}

		/// <summary>Retrieves field type from XML attribute name, namespace, and the <see cref="T:System.Type" /> of the containing object.</summary>
		/// <param name="containingType">The <see cref="T:System.Type" /> of the object that contains the field.</param>
		/// <param name="xmlAttribute">The XML attribute name of the field type.</param>
		/// <param name="xmlNamespace">The XML namespace of the field type.</param>
		/// <param name="type">When this method returns, contains a <see cref="T:System.Type" /> of the field. This parameter is passed uninitialized.</param>
		/// <param name="name">When this method returns, contains a <see cref="T:System.String" /> that holds the name of the field. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056C5 RID: 22213 RVA: 0x0013507C File Offset: 0x0013327C
		public static void GetInteropFieldTypeAndNameFromXmlAttribute(Type containingType, string xmlAttribute, string xmlNamespace, out Type type, out string name)
		{
			if (containingType == null)
			{
				type = null;
				name = null;
				return;
			}
			SoapServices.XmlToFieldTypeMap xmlToFieldTypeMap = (SoapServices.XmlToFieldTypeMap)SoapServices._xmlToFieldTypeMap[containingType];
			if (xmlToFieldTypeMap != null)
			{
				xmlToFieldTypeMap.GetFieldTypeAndNameFromXmlAttribute(xmlAttribute, xmlNamespace, out type, out name);
				return;
			}
			type = null;
			name = null;
		}

		/// <summary>Returns XML element information that should be used when serializing the given type.</summary>
		/// <param name="type">The object <see cref="T:System.Type" /> for which the XML element and namespace names were requested.</param>
		/// <param name="xmlElement">When this method returns, contains a <see cref="T:System.String" /> that holds the XML element name of the specified object type. This parameter is passed uninitialized.</param>
		/// <param name="xmlNamespace">When this method returns, contains a <see cref="T:System.String" /> that holds the XML namespace name of the specified object type. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the requested values have been set flagged with <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056C6 RID: 22214 RVA: 0x001350C4 File Offset: 0x001332C4
		[SecurityCritical]
		public static bool GetXmlElementForInteropType(Type type, out string xmlElement, out string xmlNamespace)
		{
			SoapServices.XmlEntry xmlEntry = (SoapServices.XmlEntry)SoapServices._interopTypeToXmlElement[type];
			if (xmlEntry != null)
			{
				xmlElement = xmlEntry.Name;
				xmlNamespace = xmlEntry.Namespace;
				return true;
			}
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (soapTypeAttribute.IsInteropXmlElement())
			{
				xmlElement = soapTypeAttribute.XmlElementName;
				xmlNamespace = soapTypeAttribute.XmlNamespace;
				return true;
			}
			xmlElement = null;
			xmlNamespace = null;
			return false;
		}

		/// <summary>Returns XML type information that should be used when serializing the given <see cref="T:System.Type" />.</summary>
		/// <param name="type">The object <see cref="T:System.Type" /> for which the XML element and namespace names were requested.</param>
		/// <param name="xmlType">The XML type of the specified object <see cref="T:System.Type" />.</param>
		/// <param name="xmlTypeNamespace">The XML type namespace of the specified object <see cref="T:System.Type" />.</param>
		/// <returns>
		///   <see langword="true" /> if the requested values have been set flagged with <see cref="T:System.Runtime.Remoting.Metadata.SoapTypeAttribute" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056C7 RID: 22215 RVA: 0x00135124 File Offset: 0x00133324
		[SecurityCritical]
		public static bool GetXmlTypeForInteropType(Type type, out string xmlType, out string xmlTypeNamespace)
		{
			SoapServices.XmlEntry xmlEntry = (SoapServices.XmlEntry)SoapServices._interopTypeToXmlType[type];
			if (xmlEntry != null)
			{
				xmlType = xmlEntry.Name;
				xmlTypeNamespace = xmlEntry.Namespace;
				return true;
			}
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (soapTypeAttribute.IsInteropXmlType())
			{
				xmlType = soapTypeAttribute.XmlTypeName;
				xmlTypeNamespace = soapTypeAttribute.XmlTypeNamespace;
				return true;
			}
			xmlType = null;
			xmlTypeNamespace = null;
			return false;
		}

		/// <summary>Retrieves the XML namespace used during remote calls of the method specified in the given <see cref="T:System.Reflection.MethodBase" />.</summary>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> of the method for which the XML namespace was requested.</param>
		/// <returns>The XML namespace used during remote calls of the specified method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056C8 RID: 22216 RVA: 0x00135184 File Offset: 0x00133384
		[SecurityCritical]
		public static string GetXmlNamespaceForMethodCall(MethodBase mb)
		{
			SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
			return soapMethodAttribute.XmlNamespace;
		}

		/// <summary>Retrieves the XML namespace used during the generation of responses to the remote call to the method specified in the given <see cref="T:System.Reflection.MethodBase" />.</summary>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> of the method for which the XML namespace was requested.</param>
		/// <returns>The XML namespace used during the generation of responses to a remote method call.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056C9 RID: 22217 RVA: 0x001351A4 File Offset: 0x001333A4
		[SecurityCritical]
		public static string GetXmlNamespaceForMethodResponse(MethodBase mb)
		{
			SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
			return soapMethodAttribute.ResponseXmlNamespace;
		}

		/// <summary>Associates the specified <see cref="T:System.Reflection.MethodBase" /> with the SOAPAction cached with it.</summary>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> of the method to associate with the SOAPAction cached with it.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056CA RID: 22218 RVA: 0x001351C4 File Offset: 0x001333C4
		[SecurityCritical]
		public static void RegisterSoapActionForMethodBase(MethodBase mb)
		{
			SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
			if (soapMethodAttribute.SoapActionExplicitySet)
			{
				SoapServices.RegisterSoapActionForMethodBase(mb, soapMethodAttribute.SoapAction);
			}
		}

		/// <summary>Associates the provided SOAPAction value with the given <see cref="T:System.Reflection.MethodBase" /> for use in channel sinks.</summary>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> to associate with the provided SOAPAction.</param>
		/// <param name="soapAction">The SOAPAction value to associate with the given <see cref="T:System.Reflection.MethodBase" />.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056CB RID: 22219 RVA: 0x001351F4 File Offset: 0x001333F4
		public static void RegisterSoapActionForMethodBase(MethodBase mb, string soapAction)
		{
			if (soapAction != null)
			{
				SoapServices._methodBaseToSoapAction[mb] = soapAction;
				ArrayList arrayList = (ArrayList)SoapServices._soapActionToMethodBase[soapAction];
				if (arrayList == null)
				{
					Hashtable soapActionToMethodBase = SoapServices._soapActionToMethodBase;
					lock (soapActionToMethodBase)
					{
						arrayList = ArrayList.Synchronized(new ArrayList());
						SoapServices._soapActionToMethodBase[soapAction] = arrayList;
					}
				}
				arrayList.Add(mb);
			}
		}

		/// <summary>Returns the SOAPAction value associated with the method specified in the given <see cref="T:System.Reflection.MethodBase" />.</summary>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> that contains the method for which a SOAPAction is requested.</param>
		/// <returns>The SOAPAction value associated with the method specified in the given <see cref="T:System.Reflection.MethodBase" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056CC RID: 22220 RVA: 0x00135270 File Offset: 0x00133470
		[SecurityCritical]
		public static string GetSoapActionFromMethodBase(MethodBase mb)
		{
			string text = (string)SoapServices._methodBaseToSoapAction[mb];
			if (text == null)
			{
				SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
				text = soapMethodAttribute.SoapAction;
			}
			return text;
		}

		/// <summary>Determines if the specified SOAPAction is acceptable for a given <see cref="T:System.Reflection.MethodBase" />.</summary>
		/// <param name="soapAction">The SOAPAction to check against the given <see cref="T:System.Reflection.MethodBase" />.</param>
		/// <param name="mb">The <see cref="T:System.Reflection.MethodBase" /> the specified SOAPAction is checked against.</param>
		/// <returns>
		///   <see langword="true" /> if the specified SOAPAction is acceptable for a given <see cref="T:System.Reflection.MethodBase" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056CD RID: 22221 RVA: 0x001352A8 File Offset: 0x001334A8
		[SecurityCritical]
		public static bool IsSoapActionValidForMethodBase(string soapAction, MethodBase mb)
		{
			if (mb == null)
			{
				throw new ArgumentNullException("mb");
			}
			if (soapAction[0] == '"' && soapAction[soapAction.Length - 1] == '"')
			{
				soapAction = soapAction.Substring(1, soapAction.Length - 2);
			}
			SoapMethodAttribute soapMethodAttribute = (SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb);
			if (string.CompareOrdinal(soapMethodAttribute.SoapAction, soapAction) == 0)
			{
				return true;
			}
			string text = (string)SoapServices._methodBaseToSoapAction[mb];
			if (text != null && string.CompareOrdinal(text, soapAction) == 0)
			{
				return true;
			}
			string[] array = soapAction.Split(new char[] { '#' });
			if (array.Length != 2)
			{
				return false;
			}
			bool flag;
			string typeNameForSoapActionNamespace = XmlNamespaceEncoder.GetTypeNameForSoapActionNamespace(array[0], out flag);
			if (typeNameForSoapActionNamespace == null)
			{
				return false;
			}
			string text2 = array[1];
			RuntimeMethodInfo runtimeMethodInfo = mb as RuntimeMethodInfo;
			RuntimeConstructorInfo runtimeConstructorInfo = mb as RuntimeConstructorInfo;
			RuntimeModule runtimeModule;
			if (runtimeMethodInfo != null)
			{
				runtimeModule = runtimeMethodInfo.GetRuntimeModule();
			}
			else
			{
				if (!(runtimeConstructorInfo != null))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
				}
				runtimeModule = runtimeConstructorInfo.GetRuntimeModule();
			}
			string text3 = mb.DeclaringType.FullName;
			if (flag)
			{
				text3 = text3 + ", " + runtimeModule.GetRuntimeAssembly().GetSimpleName();
			}
			return text3.Equals(typeNameForSoapActionNamespace) && mb.Name.Equals(text2);
		}

		/// <summary>Determines the type and method name of the method associated with the specified SOAPAction value.</summary>
		/// <param name="soapAction">The SOAPAction of the method for which the type and method names were requested.</param>
		/// <param name="typeName">When this method returns, contains a <see cref="T:System.String" /> that holds the type name of the method in question. This parameter is passed uninitialized.</param>
		/// <param name="methodName">When this method returns, contains a <see cref="T:System.String" /> that holds the method name of the method in question. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the type and method name were successfully recovered; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The SOAPAction value does not start and end with quotes.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056CE RID: 22222 RVA: 0x001353F4 File Offset: 0x001335F4
		public static bool GetTypeAndMethodNameFromSoapAction(string soapAction, out string typeName, out string methodName)
		{
			if (soapAction[0] == '"' && soapAction[soapAction.Length - 1] == '"')
			{
				soapAction = soapAction.Substring(1, soapAction.Length - 2);
			}
			ArrayList arrayList = (ArrayList)SoapServices._soapActionToMethodBase[soapAction];
			if (arrayList != null)
			{
				if (arrayList.Count > 1)
				{
					typeName = null;
					methodName = null;
					return false;
				}
				MethodBase methodBase = (MethodBase)arrayList[0];
				if (methodBase != null)
				{
					RuntimeMethodInfo runtimeMethodInfo = methodBase as RuntimeMethodInfo;
					RuntimeConstructorInfo runtimeConstructorInfo = methodBase as RuntimeConstructorInfo;
					RuntimeModule runtimeModule;
					if (runtimeMethodInfo != null)
					{
						runtimeModule = runtimeMethodInfo.GetRuntimeModule();
					}
					else
					{
						if (!(runtimeConstructorInfo != null))
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
						}
						runtimeModule = runtimeConstructorInfo.GetRuntimeModule();
					}
					typeName = methodBase.DeclaringType.FullName + ", " + runtimeModule.GetRuntimeAssembly().GetSimpleName();
					methodName = methodBase.Name;
					return true;
				}
			}
			string[] array = soapAction.Split(new char[] { '#' });
			if (array.Length != 2)
			{
				typeName = null;
				methodName = null;
				return false;
			}
			bool flag;
			typeName = XmlNamespaceEncoder.GetTypeNameForSoapActionNamespace(array[0], out flag);
			if (typeName == null)
			{
				methodName = null;
				return false;
			}
			methodName = array[1];
			return true;
		}

		/// <summary>Gets the XML namespace prefix for common language runtime types.</summary>
		/// <returns>The XML namespace prefix for common language runtime types.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x060056CF RID: 22223 RVA: 0x0013551C File Offset: 0x0013371C
		public static string XmlNsForClrType
		{
			get
			{
				return SoapServices.startNS;
			}
		}

		/// <summary>Gets the default XML namespace prefix that should be used for XML encoding of a common language runtime class that has an assembly, but no native namespace.</summary>
		/// <returns>The default XML namespace prefix that should be used for XML encoding of a common language runtime class that has an assembly, but no native namespace.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x060056D0 RID: 22224 RVA: 0x00135523 File Offset: 0x00133723
		public static string XmlNsForClrTypeWithAssembly
		{
			get
			{
				return SoapServices.assemblyNS;
			}
		}

		/// <summary>Gets the XML namespace prefix that should be used for XML encoding of a common language runtime class that is part of the mscorlib.dll file.</summary>
		/// <returns>The XML namespace prefix that should be used for XML encoding of a common language runtime class that is part of the mscorlib.dll file.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x060056D1 RID: 22225 RVA: 0x0013552A File Offset: 0x0013372A
		public static string XmlNsForClrTypeWithNs
		{
			get
			{
				return SoapServices.namespaceNS;
			}
		}

		/// <summary>Gets the default XML namespace prefix that should be used for XML encoding of a common language runtime class that has both a common language runtime namespace and an assembly.</summary>
		/// <returns>The default XML namespace prefix that should be used for XML encoding of a common language runtime class that has both a common language runtime namespace and an assembly.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x060056D2 RID: 22226 RVA: 0x00135531 File Offset: 0x00133731
		public static string XmlNsForClrTypeWithNsAndAssembly
		{
			get
			{
				return SoapServices.fullNS;
			}
		}

		/// <summary>Returns a Boolean value that indicates whether the specified namespace is native to the common language runtime.</summary>
		/// <param name="namespaceString">The namespace to check in the common language runtime.</param>
		/// <returns>
		///   <see langword="true" /> if the given namespace is native to the common language runtime; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056D3 RID: 22227 RVA: 0x00135538 File Offset: 0x00133738
		public static bool IsClrTypeNamespace(string namespaceString)
		{
			return namespaceString.StartsWith(SoapServices.startNS, StringComparison.Ordinal);
		}

		/// <summary>Returns the common language runtime type namespace name from the provided namespace and assembly names.</summary>
		/// <param name="typeNamespace">The namespace that is to be coded.</param>
		/// <param name="assemblyName">The name of the assembly that is to be coded.</param>
		/// <returns>The common language runtime type namespace name from the provided namespace and assembly names.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="assemblyName" /> and <paramref name="typeNamespace" /> parameters are both either <see langword="null" /> or empty.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056D4 RID: 22228 RVA: 0x0013554C File Offset: 0x0013374C
		[SecurityCritical]
		public static string CodeXmlNamespaceForClrTypeNamespace(string typeNamespace, string assemblyName)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (SoapServices.IsNameNull(typeNamespace))
			{
				if (SoapServices.IsNameNull(assemblyName))
				{
					throw new ArgumentNullException("typeNamespace,assemblyName");
				}
				stringBuilder.Append(SoapServices.assemblyNS);
				SoapServices.UriEncode(assemblyName, stringBuilder);
			}
			else if (SoapServices.IsNameNull(assemblyName))
			{
				stringBuilder.Append(SoapServices.namespaceNS);
				stringBuilder.Append(typeNamespace);
			}
			else
			{
				stringBuilder.Append(SoapServices.fullNS);
				if (typeNamespace[0] == '.')
				{
					stringBuilder.Append(typeNamespace.Substring(1));
				}
				else
				{
					stringBuilder.Append(typeNamespace);
				}
				stringBuilder.Append('/');
				SoapServices.UriEncode(assemblyName, stringBuilder);
			}
			return stringBuilder.ToString();
		}

		/// <summary>Decodes the XML namespace and assembly names from the provided common language runtime namespace.</summary>
		/// <param name="inNamespace">The common language runtime namespace.</param>
		/// <param name="typeNamespace">When this method returns, contains a <see cref="T:System.String" /> that holds the decoded namespace name. This parameter is passed uninitialized.</param>
		/// <param name="assemblyName">When this method returns, contains a <see cref="T:System.String" /> that holds the decoded assembly name. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the namespace and assembly names were successfully decoded; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inNamespace" /> parameter is <see langword="null" /> or empty.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060056D5 RID: 22229 RVA: 0x001355F8 File Offset: 0x001337F8
		[SecurityCritical]
		public static bool DecodeXmlNamespaceForClrTypeNamespace(string inNamespace, out string typeNamespace, out string assemblyName)
		{
			if (SoapServices.IsNameNull(inNamespace))
			{
				throw new ArgumentNullException("inNamespace");
			}
			assemblyName = null;
			typeNamespace = "";
			if (inNamespace.StartsWith(SoapServices.assemblyNS, StringComparison.Ordinal))
			{
				assemblyName = SoapServices.UriDecode(inNamespace.Substring(SoapServices.assemblyNS.Length));
			}
			else if (inNamespace.StartsWith(SoapServices.namespaceNS, StringComparison.Ordinal))
			{
				typeNamespace = inNamespace.Substring(SoapServices.namespaceNS.Length);
			}
			else
			{
				if (!inNamespace.StartsWith(SoapServices.fullNS, StringComparison.Ordinal))
				{
					return false;
				}
				int num = inNamespace.IndexOf("/", SoapServices.fullNS.Length);
				typeNamespace = inNamespace.Substring(SoapServices.fullNS.Length, num - SoapServices.fullNS.Length);
				assemblyName = SoapServices.UriDecode(inNamespace.Substring(num + 1));
			}
			return true;
		}

		// Token: 0x060056D6 RID: 22230 RVA: 0x001356C4 File Offset: 0x001338C4
		internal static void UriEncode(string value, StringBuilder sb)
		{
			if (value == null || value.Length == 0)
			{
				return;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == ' ')
				{
					sb.Append("%20");
				}
				else if (value[i] == '=')
				{
					sb.Append("%3D");
				}
				else if (value[i] == ',')
				{
					sb.Append("%2C");
				}
				else
				{
					sb.Append(value[i]);
				}
			}
		}

		// Token: 0x060056D7 RID: 22231 RVA: 0x00135748 File Offset: 0x00133948
		internal static string UriDecode(string value)
		{
			if (value == null || value.Length == 0)
			{
				return value;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == '%' && value.Length - i >= 3)
				{
					if (value[i + 1] == '2' && value[i + 2] == '0')
					{
						stringBuilder.Append(' ');
						i += 2;
					}
					else if (value[i + 1] == '3' && value[i + 2] == 'D')
					{
						stringBuilder.Append('=');
						i += 2;
					}
					else if (value[i + 1] == '2' && value[i + 2] == 'C')
					{
						stringBuilder.Append(',');
						i += 2;
					}
					else
					{
						stringBuilder.Append(value[i]);
					}
				}
				else
				{
					stringBuilder.Append(value[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060056D8 RID: 22232 RVA: 0x0013583A File Offset: 0x00133A3A
		private static bool IsNameNull(string name)
		{
			return name == null || name.Length == 0;
		}

		// Token: 0x040027B1 RID: 10161
		private static Hashtable _interopXmlElementToType = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040027B2 RID: 10162
		private static Hashtable _interopTypeToXmlElement = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040027B3 RID: 10163
		private static Hashtable _interopXmlTypeToType = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040027B4 RID: 10164
		private static Hashtable _interopTypeToXmlType = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040027B5 RID: 10165
		private static Hashtable _xmlToFieldTypeMap = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040027B6 RID: 10166
		private static Hashtable _methodBaseToSoapAction = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040027B7 RID: 10167
		private static Hashtable _soapActionToMethodBase = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040027B8 RID: 10168
		internal static string startNS = "http://schemas.microsoft.com/clr/";

		// Token: 0x040027B9 RID: 10169
		internal static string assemblyNS = "http://schemas.microsoft.com/clr/assem/";

		// Token: 0x040027BA RID: 10170
		internal static string namespaceNS = "http://schemas.microsoft.com/clr/ns/";

		// Token: 0x040027BB RID: 10171
		internal static string fullNS = "http://schemas.microsoft.com/clr/nsassem/";

		// Token: 0x02000C66 RID: 3174
		private class XmlEntry
		{
			// Token: 0x060070BC RID: 28860 RVA: 0x00185F4A File Offset: 0x0018414A
			public XmlEntry(string name, string xmlNamespace)
			{
				this.Name = name;
				this.Namespace = xmlNamespace;
			}

			// Token: 0x040037DC RID: 14300
			public string Name;

			// Token: 0x040037DD RID: 14301
			public string Namespace;
		}

		// Token: 0x02000C67 RID: 3175
		private class XmlToFieldTypeMap
		{
			// Token: 0x060070BE RID: 28862 RVA: 0x00185F7E File Offset: 0x0018417E
			[SecurityCritical]
			public void AddXmlElement(Type fieldType, string fieldName, string xmlElement, string xmlNamespace)
			{
				this._elements[SoapServices.CreateKey(xmlElement, xmlNamespace)] = new SoapServices.XmlToFieldTypeMap.FieldEntry(fieldType, fieldName);
			}

			// Token: 0x060070BF RID: 28863 RVA: 0x00185F9A File Offset: 0x0018419A
			[SecurityCritical]
			public void AddXmlAttribute(Type fieldType, string fieldName, string xmlAttribute, string xmlNamespace)
			{
				this._attributes[SoapServices.CreateKey(xmlAttribute, xmlNamespace)] = new SoapServices.XmlToFieldTypeMap.FieldEntry(fieldType, fieldName);
			}

			// Token: 0x060070C0 RID: 28864 RVA: 0x00185FB8 File Offset: 0x001841B8
			[SecurityCritical]
			public void GetFieldTypeAndNameFromXmlElement(string xmlElement, string xmlNamespace, out Type type, out string name)
			{
				SoapServices.XmlToFieldTypeMap.FieldEntry fieldEntry = (SoapServices.XmlToFieldTypeMap.FieldEntry)this._elements[SoapServices.CreateKey(xmlElement, xmlNamespace)];
				if (fieldEntry != null)
				{
					type = fieldEntry.Type;
					name = fieldEntry.Name;
					return;
				}
				type = null;
				name = null;
			}

			// Token: 0x060070C1 RID: 28865 RVA: 0x00185FFC File Offset: 0x001841FC
			[SecurityCritical]
			public void GetFieldTypeAndNameFromXmlAttribute(string xmlAttribute, string xmlNamespace, out Type type, out string name)
			{
				SoapServices.XmlToFieldTypeMap.FieldEntry fieldEntry = (SoapServices.XmlToFieldTypeMap.FieldEntry)this._attributes[SoapServices.CreateKey(xmlAttribute, xmlNamespace)];
				if (fieldEntry != null)
				{
					type = fieldEntry.Type;
					name = fieldEntry.Name;
					return;
				}
				type = null;
				name = null;
			}

			// Token: 0x040037DE RID: 14302
			private Hashtable _attributes = new Hashtable();

			// Token: 0x040037DF RID: 14303
			private Hashtable _elements = new Hashtable();

			// Token: 0x02000D0C RID: 3340
			private class FieldEntry
			{
				// Token: 0x0600723C RID: 29244 RVA: 0x0018ADBC File Offset: 0x00188FBC
				public FieldEntry(Type type, string name)
				{
					this.Type = type;
					this.Name = name;
				}

				// Token: 0x04003966 RID: 14694
				public Type Type;

				// Token: 0x04003967 RID: 14695
				public string Name;
			}
		}
	}
}
