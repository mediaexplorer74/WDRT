using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200053E RID: 1342
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class DelegatingTypeDescriptionProvider : TypeDescriptionProvider
	{
		// Token: 0x06003288 RID: 12936 RVA: 0x000E1DBF File Offset: 0x000DFFBF
		internal DelegatingTypeDescriptionProvider(Type type)
		{
			this._type = type;
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06003289 RID: 12937 RVA: 0x000E1DCE File Offset: 0x000DFFCE
		internal TypeDescriptionProvider Provider
		{
			get
			{
				return TypeDescriptor.GetProviderRecursive(this._type);
			}
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x000E1DDB File Offset: 0x000DFFDB
		public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			return this.Provider.CreateInstance(provider, objectType, argTypes, args);
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000E1DED File Offset: 0x000DFFED
		public override IDictionary GetCache(object instance)
		{
			return this.Provider.GetCache(instance);
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000E1DFB File Offset: 0x000DFFFB
		public override string GetFullComponentName(object component)
		{
			return this.Provider.GetFullComponentName(component);
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000E1E09 File Offset: 0x000E0009
		public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			return this.Provider.GetExtendedTypeDescriptor(instance);
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000E1E17 File Offset: 0x000E0017
		protected internal override IExtenderProvider[] GetExtenderProviders(object instance)
		{
			return this.Provider.GetExtenderProviders(instance);
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000E1E25 File Offset: 0x000E0025
		public override Type GetReflectionType(Type objectType, object instance)
		{
			return this.Provider.GetReflectionType(objectType, instance);
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x000E1E34 File Offset: 0x000E0034
		public override Type GetRuntimeType(Type objectType)
		{
			return this.Provider.GetRuntimeType(objectType);
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000E1E42 File Offset: 0x000E0042
		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			return this.Provider.GetTypeDescriptor(objectType, instance);
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000E1E51 File Offset: 0x000E0051
		public override bool IsSupportedType(Type type)
		{
			return this.Provider.IsSupportedType(type);
		}

		// Token: 0x0400296B RID: 10603
		private Type _type;
	}
}
