using System;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007B6 RID: 1974
	[Serializable]
	internal class TypeInfo : IRemotingTypeInfo
	{
		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x060055A3 RID: 21923 RVA: 0x00131548 File Offset: 0x0012F748
		// (set) Token: 0x060055A4 RID: 21924 RVA: 0x00131550 File Offset: 0x0012F750
		public virtual string TypeName
		{
			[SecurityCritical]
			get
			{
				return this.serverType;
			}
			[SecurityCritical]
			set
			{
				this.serverType = value;
			}
		}

		// Token: 0x060055A5 RID: 21925 RVA: 0x0013155C File Offset: 0x0012F75C
		[SecurityCritical]
		public virtual bool CanCastTo(Type castType, object o)
		{
			if (null != castType)
			{
				if (castType == typeof(MarshalByRefObject) || castType == typeof(object))
				{
					return true;
				}
				if (castType.IsInterface)
				{
					return this.interfacesImplemented != null && this.CanCastTo(castType, this.InterfacesImplemented);
				}
				if (castType.IsMarshalByRef)
				{
					if (this.CompareTypes(castType, this.serverType))
					{
						return true;
					}
					if (this.serverHierarchy != null && this.CanCastTo(castType, this.ServerHierarchy))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060055A6 RID: 21926 RVA: 0x001315EB File Offset: 0x0012F7EB
		[SecurityCritical]
		internal static string GetQualifiedTypeName(RuntimeType type)
		{
			if (type == null)
			{
				return null;
			}
			return RemotingServices.GetDefaultQualifiedTypeName(type);
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x00131600 File Offset: 0x0012F800
		internal static bool ParseTypeAndAssembly(string typeAndAssembly, out string typeName, out string assemName)
		{
			if (typeAndAssembly == null)
			{
				typeName = null;
				assemName = null;
				return false;
			}
			int num = typeAndAssembly.IndexOf(',');
			if (num == -1)
			{
				typeName = typeAndAssembly;
				assemName = null;
				return true;
			}
			typeName = typeAndAssembly.Substring(0, num);
			assemName = typeAndAssembly.Substring(num + 1).Trim();
			return true;
		}

		// Token: 0x060055A8 RID: 21928 RVA: 0x00131648 File Offset: 0x0012F848
		[SecurityCritical]
		internal TypeInfo(RuntimeType typeOfObj)
		{
			this.ServerType = TypeInfo.GetQualifiedTypeName(typeOfObj);
			RuntimeType runtimeType = (RuntimeType)typeOfObj.BaseType;
			int num = 0;
			while (runtimeType != typeof(MarshalByRefObject) && runtimeType != null)
			{
				runtimeType = (RuntimeType)runtimeType.BaseType;
				num++;
			}
			string[] array = null;
			if (num > 0)
			{
				array = new string[num];
				runtimeType = (RuntimeType)typeOfObj.BaseType;
				for (int i = 0; i < num; i++)
				{
					array[i] = TypeInfo.GetQualifiedTypeName(runtimeType);
					runtimeType = (RuntimeType)runtimeType.BaseType;
				}
			}
			this.ServerHierarchy = array;
			Type[] interfaces = typeOfObj.GetInterfaces();
			string[] array2 = null;
			bool isInterface = typeOfObj.IsInterface;
			if (interfaces.Length != 0 || isInterface)
			{
				array2 = new string[interfaces.Length + (isInterface ? 1 : 0)];
				for (int j = 0; j < interfaces.Length; j++)
				{
					array2[j] = TypeInfo.GetQualifiedTypeName((RuntimeType)interfaces[j]);
				}
				if (isInterface)
				{
					array2[array2.Length - 1] = TypeInfo.GetQualifiedTypeName(typeOfObj);
				}
			}
			this.InterfacesImplemented = array2;
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x060055A9 RID: 21929 RVA: 0x00131757 File Offset: 0x0012F957
		// (set) Token: 0x060055AA RID: 21930 RVA: 0x0013175F File Offset: 0x0012F95F
		internal string ServerType
		{
			get
			{
				return this.serverType;
			}
			set
			{
				this.serverType = value;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x060055AB RID: 21931 RVA: 0x00131768 File Offset: 0x0012F968
		// (set) Token: 0x060055AC RID: 21932 RVA: 0x00131770 File Offset: 0x0012F970
		private string[] ServerHierarchy
		{
			get
			{
				return this.serverHierarchy;
			}
			set
			{
				this.serverHierarchy = value;
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x060055AD RID: 21933 RVA: 0x00131779 File Offset: 0x0012F979
		// (set) Token: 0x060055AE RID: 21934 RVA: 0x00131781 File Offset: 0x0012F981
		private string[] InterfacesImplemented
		{
			get
			{
				return this.interfacesImplemented;
			}
			set
			{
				this.interfacesImplemented = value;
			}
		}

		// Token: 0x060055AF RID: 21935 RVA: 0x0013178C File Offset: 0x0012F98C
		[SecurityCritical]
		private bool CompareTypes(Type type1, string type2)
		{
			Type type3 = RemotingServices.InternalGetTypeFromQualifiedTypeName(type2);
			return type1 == type3;
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x001317A8 File Offset: 0x0012F9A8
		[SecurityCritical]
		private bool CanCastTo(Type castType, string[] types)
		{
			bool flag = false;
			if (null != castType)
			{
				for (int i = 0; i < types.Length; i++)
				{
					if (this.CompareTypes(castType, types[i]))
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x0400276D RID: 10093
		private string serverType;

		// Token: 0x0400276E RID: 10094
		private string[] serverHierarchy;

		// Token: 0x0400276F RID: 10095
		private string[] interfacesImplemented;
	}
}
