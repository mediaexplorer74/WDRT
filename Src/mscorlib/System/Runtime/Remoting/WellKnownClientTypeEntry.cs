using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.Remoting
{
	/// <summary>Holds values for an object type registered on the client as a server-activated type (single call or singleton).</summary>
	// Token: 0x020007C1 RID: 1985
	[ComVisible(true)]
	public class WellKnownClientTypeEntry : TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> class with the given type, assembly name, and URL.</summary>
		/// <param name="typeName">The type name of the server-activated type.</param>
		/// <param name="assemblyName">The assembly name of the server-activated type.</param>
		/// <param name="objectUrl">The URL of the server-activated type.</param>
		// Token: 0x06005619 RID: 22041 RVA: 0x001329B4 File Offset: 0x00130BB4
		public WellKnownClientTypeEntry(string typeName, string assemblyName, string objectUrl)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (objectUrl == null)
			{
				throw new ArgumentNullException("objectUrl");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
			this._objectUrl = objectUrl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> class with the given type and URL.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the server-activated type.</param>
		/// <param name="objectUrl">The URL of the server-activated type.</param>
		// Token: 0x0600561A RID: 22042 RVA: 0x00132A08 File Offset: 0x00130C08
		public WellKnownClientTypeEntry(Type type, string objectUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (objectUrl == null)
			{
				throw new ArgumentNullException("objectUrl");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			base.TypeName = type.FullName;
			base.AssemblyName = runtimeType.GetRuntimeAssembly().GetSimpleName();
			this._objectUrl = objectUrl;
		}

		/// <summary>Gets the URL of the server-activated client object.</summary>
		/// <returns>The URL of the server-activated client object.</returns>
		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x0600561B RID: 22043 RVA: 0x00132A81 File Offset: 0x00130C81
		public string ObjectUrl
		{
			get
			{
				return this._objectUrl;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the server-activated client type.</summary>
		/// <returns>Gets the <see cref="T:System.Type" /> of the server-activated client type.</returns>
		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x0600561C RID: 22044 RVA: 0x00132A8C File Offset: 0x00130C8C
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeTypeHandle.GetTypeByName(base.TypeName + ", " + base.AssemblyName, ref stackCrawlMark);
			}
		}

		/// <summary>Gets or sets the URL of the application to activate the type in.</summary>
		/// <returns>The URL of the application to activate the type in.</returns>
		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x0600561D RID: 22045 RVA: 0x00132AB8 File Offset: 0x00130CB8
		// (set) Token: 0x0600561E RID: 22046 RVA: 0x00132AC0 File Offset: 0x00130CC0
		public string ApplicationUrl
		{
			get
			{
				return this._appUrl;
			}
			set
			{
				this._appUrl = value;
			}
		}

		/// <summary>Returns the full type name, assembly name, and object URL of the server-activated client type as a <see cref="T:System.String" />.</summary>
		/// <returns>The full type name, assembly name, and object URL of the server-activated client type as a <see cref="T:System.String" />.</returns>
		// Token: 0x0600561F RID: 22047 RVA: 0x00132ACC File Offset: 0x00130CCC
		public override string ToString()
		{
			string text = string.Concat(new string[] { "type='", base.TypeName, ", ", base.AssemblyName, "'; url=", this._objectUrl });
			if (this._appUrl != null)
			{
				text = text + "; appUrl=" + this._appUrl;
			}
			return text;
		}

		// Token: 0x0400278B RID: 10123
		private string _objectUrl;

		// Token: 0x0400278C RID: 10124
		private string _appUrl;
	}
}
