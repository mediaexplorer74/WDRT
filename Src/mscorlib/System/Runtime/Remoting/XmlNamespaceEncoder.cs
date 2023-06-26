using System;
using System.Reflection;
using System.Security;
using System.Text;

namespace System.Runtime.Remoting
{
	// Token: 0x020007CC RID: 1996
	internal static class XmlNamespaceEncoder
	{
		// Token: 0x060056DA RID: 22234 RVA: 0x001358EC File Offset: 0x00133AEC
		[SecurityCritical]
		internal static string GetXmlNamespaceForType(RuntimeType type, string dynamicUrl)
		{
			string fullName = type.FullName;
			RuntimeAssembly runtimeAssembly = type.GetRuntimeAssembly();
			StringBuilder stringBuilder = new StringBuilder(256);
			Assembly assembly = typeof(string).Module.Assembly;
			if (runtimeAssembly == assembly)
			{
				stringBuilder.Append(SoapServices.namespaceNS);
				stringBuilder.Append(fullName);
			}
			else
			{
				stringBuilder.Append(SoapServices.fullNS);
				stringBuilder.Append(fullName);
				stringBuilder.Append('/');
				stringBuilder.Append(runtimeAssembly.GetSimpleName());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060056DB RID: 22235 RVA: 0x00135978 File Offset: 0x00133B78
		[SecurityCritical]
		internal static string GetXmlNamespaceForTypeNamespace(RuntimeType type, string dynamicUrl)
		{
			string @namespace = type.Namespace;
			RuntimeAssembly runtimeAssembly = type.GetRuntimeAssembly();
			StringBuilder stringBuilder = StringBuilderCache.Acquire(256);
			Assembly assembly = typeof(string).Module.Assembly;
			if (runtimeAssembly == assembly)
			{
				stringBuilder.Append(SoapServices.namespaceNS);
				stringBuilder.Append(@namespace);
			}
			else
			{
				stringBuilder.Append(SoapServices.fullNS);
				stringBuilder.Append(@namespace);
				stringBuilder.Append('/');
				stringBuilder.Append(runtimeAssembly.GetSimpleName());
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x060056DC RID: 22236 RVA: 0x00135A04 File Offset: 0x00133C04
		[SecurityCritical]
		internal static string GetTypeNameForSoapActionNamespace(string uri, out bool assemblyIncluded)
		{
			assemblyIncluded = false;
			string fullNS = SoapServices.fullNS;
			string namespaceNS = SoapServices.namespaceNS;
			if (uri.StartsWith(fullNS, StringComparison.Ordinal))
			{
				uri = uri.Substring(fullNS.Length);
				char[] array = new char[] { '/' };
				string[] array2 = uri.Split(array);
				if (array2.Length != 2)
				{
					return null;
				}
				assemblyIncluded = true;
				return array2[0] + ", " + array2[1];
			}
			else
			{
				if (uri.StartsWith(namespaceNS, StringComparison.Ordinal))
				{
					string simpleName = ((RuntimeAssembly)typeof(string).Module.Assembly).GetSimpleName();
					assemblyIncluded = true;
					return uri.Substring(namespaceNS.Length) + ", " + simpleName;
				}
				return null;
			}
		}
	}
}
