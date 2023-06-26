using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace System.Resources
{
	// Token: 0x020000EC RID: 236
	internal class AssemblyNamesTypeResolutionService : ITypeResolutionService
	{
		// Token: 0x06000353 RID: 851 RVA: 0x00009FF8 File Offset: 0x000081F8
		internal AssemblyNamesTypeResolutionService(AssemblyName[] names)
		{
			this.names = names;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000A007 File Offset: 0x00008207
		public Assembly GetAssembly(AssemblyName name)
		{
			return this.GetAssembly(name, true);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000A014 File Offset: 0x00008214
		public Assembly GetAssembly(AssemblyName name, bool throwOnError)
		{
			Assembly assembly = null;
			if (this.cachedAssemblies == null)
			{
				this.cachedAssemblies = Hashtable.Synchronized(new Hashtable());
			}
			if (this.cachedAssemblies.Contains(name))
			{
				assembly = this.cachedAssemblies[name] as Assembly;
			}
			if (assembly == null)
			{
				assembly = Assembly.LoadWithPartialName(name.FullName);
				if (assembly != null)
				{
					this.cachedAssemblies[name] = assembly;
				}
				else if (this.names != null)
				{
					for (int i = 0; i < this.names.Length; i++)
					{
						if (name.Equals(this.names[i]))
						{
							try
							{
								assembly = Assembly.LoadFrom(this.GetPathOfAssembly(name));
								if (assembly != null)
								{
									this.cachedAssemblies[name] = assembly;
								}
							}
							catch
							{
								if (throwOnError)
								{
									throw;
								}
							}
						}
					}
				}
			}
			return assembly;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000A0F4 File Offset: 0x000082F4
		public string GetPathOfAssembly(AssemblyName name)
		{
			return name.CodeBase;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000A0FC File Offset: 0x000082FC
		public Type GetType(string name)
		{
			return this.GetType(name, true);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000A106 File Offset: 0x00008306
		public Type GetType(string name, bool throwOnError)
		{
			return this.GetType(name, throwOnError, false);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000A114 File Offset: 0x00008314
		public Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			Type type = null;
			if (this.cachedTypes == null)
			{
				this.cachedTypes = Hashtable.Synchronized(new Hashtable(StringComparer.Ordinal));
			}
			if (this.cachedTypes.Contains(name))
			{
				type = this.cachedTypes[name] as Type;
				return type;
			}
			if (name.IndexOf(',') != -1)
			{
				type = Type.GetType(name, false, ignoreCase);
			}
			if (type == null && this.names != null)
			{
				int num = name.IndexOf(',');
				if (num > 0 && num < name.Length - 1)
				{
					string text = name.Substring(num + 1).Trim();
					AssemblyName assemblyName = null;
					try
					{
						assemblyName = new AssemblyName(text);
					}
					catch
					{
					}
					if (assemblyName != null)
					{
						List<AssemblyName> list = new List<AssemblyName>(this.names.Length);
						for (int i = 0; i < this.names.Length; i++)
						{
							if (string.Compare(assemblyName.Name, this.names[i].Name, StringComparison.OrdinalIgnoreCase) == 0)
							{
								list.Insert(0, this.names[i]);
							}
							else
							{
								list.Add(this.names[i]);
							}
						}
						this.names = list.ToArray();
					}
				}
				for (int j = 0; j < this.names.Length; j++)
				{
					Assembly assembly = this.GetAssembly(this.names[j], false);
					if (assembly != null)
					{
						type = assembly.GetType(name, false, ignoreCase);
						if (type == null)
						{
							int num2 = name.IndexOf(",");
							if (num2 != -1)
							{
								string text2 = name.Substring(0, num2);
								type = assembly.GetType(text2, false, ignoreCase);
							}
						}
					}
					if (type != null)
					{
						break;
					}
				}
			}
			if (type == null && throwOnError)
			{
				throw new ArgumentException(SR.GetString("InvalidResXNoType", new object[] { name }));
			}
			if (type != null && (type.Assembly.GlobalAssemblyCache || this.IsNetFrameworkAssembly(type.Assembly.Location)))
			{
				this.cachedTypes[name] = type;
			}
			return type;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000A324 File Offset: 0x00008524
		private bool IsNetFrameworkAssembly(string assemblyPath)
		{
			return assemblyPath != null && assemblyPath.StartsWith(AssemblyNamesTypeResolutionService.NetFrameworkPath, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000A337 File Offset: 0x00008537
		public void ReferenceAssembly(AssemblyName name)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040003C0 RID: 960
		private AssemblyName[] names;

		// Token: 0x040003C1 RID: 961
		private Hashtable cachedAssemblies;

		// Token: 0x040003C2 RID: 962
		private Hashtable cachedTypes;

		// Token: 0x040003C3 RID: 963
		private static string NetFrameworkPath = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "Microsoft.Net\\Framework");
	}
}
