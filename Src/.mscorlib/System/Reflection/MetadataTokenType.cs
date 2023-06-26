using System;

namespace System.Reflection
{
	// Token: 0x020005F8 RID: 1528
	[Serializable]
	internal enum MetadataTokenType
	{
		// Token: 0x04001D3B RID: 7483
		Module,
		// Token: 0x04001D3C RID: 7484
		TypeRef = 16777216,
		// Token: 0x04001D3D RID: 7485
		TypeDef = 33554432,
		// Token: 0x04001D3E RID: 7486
		FieldDef = 67108864,
		// Token: 0x04001D3F RID: 7487
		MethodDef = 100663296,
		// Token: 0x04001D40 RID: 7488
		ParamDef = 134217728,
		// Token: 0x04001D41 RID: 7489
		InterfaceImpl = 150994944,
		// Token: 0x04001D42 RID: 7490
		MemberRef = 167772160,
		// Token: 0x04001D43 RID: 7491
		CustomAttribute = 201326592,
		// Token: 0x04001D44 RID: 7492
		Permission = 234881024,
		// Token: 0x04001D45 RID: 7493
		Signature = 285212672,
		// Token: 0x04001D46 RID: 7494
		Event = 335544320,
		// Token: 0x04001D47 RID: 7495
		Property = 385875968,
		// Token: 0x04001D48 RID: 7496
		ModuleRef = 436207616,
		// Token: 0x04001D49 RID: 7497
		TypeSpec = 452984832,
		// Token: 0x04001D4A RID: 7498
		Assembly = 536870912,
		// Token: 0x04001D4B RID: 7499
		AssemblyRef = 587202560,
		// Token: 0x04001D4C RID: 7500
		File = 637534208,
		// Token: 0x04001D4D RID: 7501
		ExportedType = 654311424,
		// Token: 0x04001D4E RID: 7502
		ManifestResource = 671088640,
		// Token: 0x04001D4F RID: 7503
		GenericPar = 704643072,
		// Token: 0x04001D50 RID: 7504
		MethodSpec = 721420288,
		// Token: 0x04001D51 RID: 7505
		String = 1879048192,
		// Token: 0x04001D52 RID: 7506
		Name = 1895825408,
		// Token: 0x04001D53 RID: 7507
		BaseType = 1912602624,
		// Token: 0x04001D54 RID: 7508
		Invalid = 2147483647
	}
}
