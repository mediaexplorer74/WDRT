using System;
using System.ComponentModel.Design;
using System.Runtime.Serialization;

namespace System.Resources
{
	// Token: 0x020000EB RID: 235
	internal class ResXSerializationBinder : SerializationBinder
	{
		// Token: 0x0600034F RID: 847 RVA: 0x00009E89 File Offset: 0x00008089
		internal ResXSerializationBinder(ITypeResolutionService typeResolver)
		{
			this.typeResolver = typeResolver;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00009E98 File Offset: 0x00008098
		internal ResXSerializationBinder(Func<Type, string> typeNameConverter)
		{
			this.typeNameConverter = typeNameConverter;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00009EA8 File Offset: 0x000080A8
		public override Type BindToType(string assemblyName, string typeName)
		{
			if (this.typeResolver == null)
			{
				return null;
			}
			typeName = typeName + ", " + assemblyName;
			Type type = this.typeResolver.GetType(typeName);
			if (type == null)
			{
				string[] array = typeName.Split(new char[] { ',' });
				if (array != null && array.Length > 2)
				{
					string text = array[0].Trim();
					for (int i = 1; i < array.Length; i++)
					{
						string text2 = array[i].Trim();
						if (!text2.StartsWith("Version=") && !text2.StartsWith("version="))
						{
							text = text + ", " + text2;
						}
					}
					type = this.typeResolver.GetType(text);
					if (type == null)
					{
						type = this.typeResolver.GetType(array[0].Trim());
					}
				}
			}
			return type;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00009F78 File Offset: 0x00008178
		public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			typeName = null;
			if (this.typeNameConverter != null)
			{
				string assemblyQualifiedName = MultitargetUtil.GetAssemblyQualifiedName(serializedType, this.typeNameConverter);
				if (!string.IsNullOrEmpty(assemblyQualifiedName))
				{
					int num = assemblyQualifiedName.IndexOf(',');
					if (num > 0 && num < assemblyQualifiedName.Length - 1)
					{
						assemblyName = assemblyQualifiedName.Substring(num + 1).TrimStart(new char[0]);
						string text = assemblyQualifiedName.Substring(0, num);
						if (!string.Equals(text, serializedType.FullName, StringComparison.InvariantCulture))
						{
							typeName = text;
						}
						return;
					}
				}
			}
			base.BindToName(serializedType, out assemblyName, out typeName);
		}

		// Token: 0x040003BE RID: 958
		private ITypeResolutionService typeResolver;

		// Token: 0x040003BF RID: 959
		private Func<Type, string> typeNameConverter;
	}
}
