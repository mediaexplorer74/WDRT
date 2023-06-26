using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006C5 RID: 1733
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("a504e5b0-8ccf-4cb4-9902-c9d1b9abd033")]
	[ComImport]
	internal interface ICMS
	{
		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06005057 RID: 20567
		IDefinitionIdentity Identity
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06005058 RID: 20568
		ISection FileSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06005059 RID: 20569
		ISection CategoryMembershipSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x0600505A RID: 20570
		ISection COMRedirectionSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x0600505B RID: 20571
		ISection ProgIdRedirectionSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x0600505C RID: 20572
		ISection CLRSurrogateSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x0600505D RID: 20573
		ISection AssemblyReferenceSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x0600505E RID: 20574
		ISection WindowClassSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x0600505F RID: 20575
		ISection StringSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06005060 RID: 20576
		ISection EntryPointSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06005061 RID: 20577
		ISection PermissionSetSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06005062 RID: 20578
		ISectionEntry MetadataSectionEntry
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06005063 RID: 20579
		ISection AssemblyRequestSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06005064 RID: 20580
		ISection RegistryKeySection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06005065 RID: 20581
		ISection DirectorySection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06005066 RID: 20582
		ISection FileAssociationSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06005067 RID: 20583
		ISection CompatibleFrameworksSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06005068 RID: 20584
		ISection EventSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06005069 RID: 20585
		ISection EventMapSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x0600506A RID: 20586
		ISection EventTagSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600506B RID: 20587
		ISection CounterSetSection
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600506C RID: 20588
		ISection CounterSection
		{
			[SecurityCritical]
			get;
		}
	}
}
