using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace System.Management.Instrumentation
{
	/// <summary>Specifies that this assembly provides management instrumentation. This attribute should appear one time per assembly.  
	///  Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x020000AC RID: 172
	[AttributeUsage(AttributeTargets.Assembly)]
	public class InstrumentedAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.Instrumentation.InstrumentedAttribute" /> class that is set for the root\default namespace. This is the default constructor.</summary>
		// Token: 0x0600048C RID: 1164 RVA: 0x000222B7 File Offset: 0x000204B7
		public InstrumentedAttribute()
			: this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.Instrumentation.InstrumentedAttribute" /> class that is set to the specified namespace for instrumentation within this assembly.</summary>
		/// <param name="namespaceName">The namespace for instrumentation instances and events.</param>
		// Token: 0x0600048D RID: 1165 RVA: 0x000222C1 File Offset: 0x000204C1
		public InstrumentedAttribute(string namespaceName)
			: this(namespaceName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.Instrumentation.InstrumentedAttribute" /> class that is set to the specified namespace and security settings for instrumentation within this assembly.</summary>
		/// <param name="namespaceName">The namespace for instrumentation instances and events.</param>
		/// <param name="securityDescriptor">A security descriptor that allows only the specified users or groups to run applications that provide the instrumentation supported by this assembly.</param>
		// Token: 0x0600048E RID: 1166 RVA: 0x000222CC File Offset: 0x000204CC
		public InstrumentedAttribute(string namespaceName, string securityDescriptor)
		{
			if (namespaceName != null)
			{
				namespaceName = namespaceName.Replace('/', '\\');
			}
			if (namespaceName == null || namespaceName.Length == 0)
			{
				namespaceName = "root\\default";
			}
			bool flag = true;
			foreach (string text in namespaceName.Split(new char[] { '\\' }))
			{
				if (text.Length == 0 || (flag && string.Compare(text, "root", StringComparison.OrdinalIgnoreCase) != 0) || !Regex.Match(text, "^[a-z,A-Z]").Success || Regex.Match(text, "_$").Success || Regex.Match(text, "[^a-z,A-Z,0-9,_,\\u0080-\\uFFFF]").Success)
				{
					ManagementException.ThrowWithExtendedInfo(ManagementStatus.InvalidNamespace);
				}
				flag = false;
			}
			this.namespaceName = namespaceName;
			this.securityDescriptor = securityDescriptor;
		}

		/// <summary>Gets or sets the namespace for instrumentation instances and events in this assembly.</summary>
		/// <returns>Returns a <see cref="T:System.String" /> value containing the name of the namespace for instrumentation instances and events in this assembly.</returns>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0002238F File Offset: 0x0002058F
		public string NamespaceName
		{
			get
			{
				if (this.namespaceName != null)
				{
					return this.namespaceName;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets or sets a security descriptor that allows only the specified users or groups to run applications that provide the instrumentation supported by this assembly.</summary>
		/// <returns>Returns a <see cref="T:System.String" /> value containing the security descriptor that allows only the specified users or groups to run applications that provide the instrumentation supported by this assembly.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x000223A5 File Offset: 0x000205A5
		public string SecurityDescriptor
		{
			get
			{
				if (this.securityDescriptor == null || this.securityDescriptor.Length == 0)
				{
					return null;
				}
				return this.securityDescriptor;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000223C4 File Offset: 0x000205C4
		internal static InstrumentedAttribute GetAttribute(Assembly assembly)
		{
			object[] customAttributes = assembly.GetCustomAttributes(typeof(InstrumentedAttribute), false);
			if (customAttributes.Length != 0)
			{
				return (InstrumentedAttribute)customAttributes[0];
			}
			return new InstrumentedAttribute();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000223F8 File Offset: 0x000205F8
		internal static Type[] GetInstrumentedTypes(Assembly assembly)
		{
			ArrayList arrayList = new ArrayList();
			foreach (Type type in assembly.GetTypes())
			{
				if (InstrumentedAttribute.IsInstrumentationClass(type))
				{
					InstrumentedAttribute.GetInstrumentedParentTypes(arrayList, type);
				}
			}
			return (Type[])arrayList.ToArray(typeof(Type));
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00022448 File Offset: 0x00020648
		private static void GetInstrumentedParentTypes(ArrayList types, Type childType)
		{
			if (!types.Contains(childType))
			{
				Type baseInstrumentationType = InstrumentationClassAttribute.GetBaseInstrumentationType(childType);
				if (baseInstrumentationType != null)
				{
					InstrumentedAttribute.GetInstrumentedParentTypes(types, baseInstrumentationType);
				}
				types.Add(childType);
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0002247D File Offset: 0x0002067D
		private static bool IsInstrumentationClass(Type type)
		{
			return InstrumentationClassAttribute.GetAttribute(type) != null;
		}

		// Token: 0x040004E1 RID: 1249
		private string namespaceName;

		// Token: 0x040004E2 RID: 1250
		private string securityDescriptor;
	}
}
