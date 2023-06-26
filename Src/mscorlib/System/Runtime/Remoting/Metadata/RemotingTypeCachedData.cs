using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007D1 RID: 2001
	internal class RemotingTypeCachedData : RemotingCachedData
	{
		// Token: 0x060056E9 RID: 22249 RVA: 0x00135C2F File Offset: 0x00133E2F
		internal RemotingTypeCachedData(RuntimeType ri)
		{
			this.RI = ri;
		}

		// Token: 0x060056EA RID: 22250 RVA: 0x00135C40 File Offset: 0x00133E40
		internal override SoapAttribute GetSoapAttributeNoLock()
		{
			object[] customAttributes = this.RI.GetCustomAttributes(typeof(SoapTypeAttribute), true);
			SoapAttribute soapAttribute;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				soapAttribute = (SoapAttribute)customAttributes[0];
			}
			else
			{
				soapAttribute = new SoapTypeAttribute();
			}
			soapAttribute.SetReflectInfo(this.RI);
			return soapAttribute;
		}

		// Token: 0x060056EB RID: 22251 RVA: 0x00135C8C File Offset: 0x00133E8C
		internal MethodBase GetLastCalledMethod(string newMeth)
		{
			RemotingTypeCachedData.LastCalledMethodClass lastMethodCalled = this._lastMethodCalled;
			if (lastMethodCalled == null)
			{
				return null;
			}
			string methodName = lastMethodCalled.methodName;
			MethodBase mb = lastMethodCalled.MB;
			if (mb == null || methodName == null)
			{
				return null;
			}
			if (methodName.Equals(newMeth))
			{
				return mb;
			}
			return null;
		}

		// Token: 0x060056EC RID: 22252 RVA: 0x00135CD0 File Offset: 0x00133ED0
		internal void SetLastCalledMethod(string newMethName, MethodBase newMB)
		{
			this._lastMethodCalled = new RemotingTypeCachedData.LastCalledMethodClass
			{
				methodName = newMethName,
				MB = newMB
			};
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060056ED RID: 22253 RVA: 0x00135CF8 File Offset: 0x00133EF8
		internal TypeInfo TypeInfo
		{
			[SecurityCritical]
			get
			{
				if (this._typeInfo == null)
				{
					this._typeInfo = new TypeInfo(this.RI);
				}
				return this._typeInfo;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060056EE RID: 22254 RVA: 0x00135D19 File Offset: 0x00133F19
		internal string QualifiedTypeName
		{
			[SecurityCritical]
			get
			{
				if (this._qualifiedTypeName == null)
				{
					this._qualifiedTypeName = RemotingServices.DetermineDefaultQualifiedTypeName(this.RI);
				}
				return this._qualifiedTypeName;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060056EF RID: 22255 RVA: 0x00135D3A File Offset: 0x00133F3A
		internal string AssemblyName
		{
			get
			{
				if (this._assemblyName == null)
				{
					this._assemblyName = this.RI.Module.Assembly.FullName;
				}
				return this._assemblyName;
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x060056F0 RID: 22256 RVA: 0x00135D65 File Offset: 0x00133F65
		internal string SimpleAssemblyName
		{
			[SecurityCritical]
			get
			{
				if (this._simpleAssemblyName == null)
				{
					this._simpleAssemblyName = this.RI.GetRuntimeAssembly().GetSimpleName();
				}
				return this._simpleAssemblyName;
			}
		}

		// Token: 0x040027C0 RID: 10176
		private RuntimeType RI;

		// Token: 0x040027C1 RID: 10177
		private RemotingTypeCachedData.LastCalledMethodClass _lastMethodCalled;

		// Token: 0x040027C2 RID: 10178
		private TypeInfo _typeInfo;

		// Token: 0x040027C3 RID: 10179
		private string _qualifiedTypeName;

		// Token: 0x040027C4 RID: 10180
		private string _assemblyName;

		// Token: 0x040027C5 RID: 10181
		private string _simpleAssemblyName;

		// Token: 0x02000C68 RID: 3176
		private class LastCalledMethodClass
		{
			// Token: 0x040037E0 RID: 14304
			public string methodName;

			// Token: 0x040037E1 RID: 14305
			public MethodBase MB;
		}
	}
}
