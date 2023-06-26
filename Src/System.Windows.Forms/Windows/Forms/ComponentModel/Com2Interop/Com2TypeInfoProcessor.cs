using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004B0 RID: 1200
	internal class Com2TypeInfoProcessor
	{
		// Token: 0x06004F49 RID: 20297 RVA: 0x00002843 File Offset: 0x00000A43
		private Com2TypeInfoProcessor()
		{
		}

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06004F4A RID: 20298 RVA: 0x001462D4 File Offset: 0x001444D4
		private static ModuleBuilder ModuleBuilder
		{
			get
			{
				if (Com2TypeInfoProcessor.moduleBuilder == null)
				{
					AppDomain domain = Thread.GetDomain();
					AssemblyBuilder assemblyBuilder = domain.DefineDynamicAssembly(new AssemblyName
					{
						Name = "COM2InteropEmit"
					}, AssemblyBuilderAccess.Run);
					Com2TypeInfoProcessor.moduleBuilder = assemblyBuilder.DefineDynamicModule("COM2Interop.Emit");
				}
				return Com2TypeInfoProcessor.moduleBuilder;
			}
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x00146324 File Offset: 0x00144524
		public static UnsafeNativeMethods.ITypeInfo FindTypeInfo(object obj, bool wantCoClass)
		{
			UnsafeNativeMethods.ITypeInfo typeInfo = null;
			int num = 0;
			while (typeInfo == null && num < 2)
			{
				if (wantCoClass != (num == 0))
				{
					goto IL_28;
				}
				if (obj is NativeMethods.IProvideClassInfo)
				{
					NativeMethods.IProvideClassInfo provideClassInfo = (NativeMethods.IProvideClassInfo)obj;
					try
					{
						typeInfo = provideClassInfo.GetClassInfo();
						goto IL_49;
					}
					catch
					{
						goto IL_49;
					}
					goto IL_28;
				}
				IL_49:
				num++;
				continue;
				IL_28:
				if (obj is UnsafeNativeMethods.IDispatch)
				{
					UnsafeNativeMethods.IDispatch dispatch = (UnsafeNativeMethods.IDispatch)obj;
					try
					{
						typeInfo = dispatch.GetTypeInfo(0, SafeNativeMethods.GetThreadLCID());
					}
					catch
					{
					}
					goto IL_49;
				}
				goto IL_49;
			}
			return typeInfo;
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x001463A4 File Offset: 0x001445A4
		public static UnsafeNativeMethods.ITypeInfo[] FindTypeInfos(object obj, bool wantCoClass)
		{
			UnsafeNativeMethods.ITypeInfo[] array = null;
			int num = 0;
			UnsafeNativeMethods.ITypeInfo typeInfo = null;
			if (obj is NativeMethods.IProvideMultipleClassInfo)
			{
				NativeMethods.IProvideMultipleClassInfo provideMultipleClassInfo = (NativeMethods.IProvideMultipleClassInfo)obj;
				if (!NativeMethods.Succeeded(provideMultipleClassInfo.GetMultiTypeInfoCount(ref num)) || num == 0)
				{
					num = 0;
				}
				if (num > 0)
				{
					array = new UnsafeNativeMethods.ITypeInfo[num];
					for (int i = 0; i < num; i++)
					{
						if (!NativeMethods.Failed(provideMultipleClassInfo.GetInfoOfIndex(i, 1, ref typeInfo, 0, 0, IntPtr.Zero, IntPtr.Zero)))
						{
							array[i] = typeInfo;
						}
					}
				}
			}
			if (array == null || array.Length == 0)
			{
				typeInfo = Com2TypeInfoProcessor.FindTypeInfo(obj, wantCoClass);
				if (typeInfo != null)
				{
					array = new UnsafeNativeMethods.ITypeInfo[] { typeInfo };
				}
			}
			return array;
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x00146438 File Offset: 0x00144638
		public static int GetNameDispId(UnsafeNativeMethods.IDispatch obj)
		{
			int num = -1;
			string[] array = null;
			ComNativeDescriptor instance = ComNativeDescriptor.Instance;
			bool flag = false;
			instance.GetPropertyValue(obj, "__id", ref flag);
			if (flag)
			{
				array = new string[] { "__id" };
			}
			else
			{
				instance.GetPropertyValue(obj, -800, ref flag);
				if (flag)
				{
					num = -800;
				}
				else
				{
					instance.GetPropertyValue(obj, "Name", ref flag);
					if (flag)
					{
						array = new string[] { "Name" };
					}
				}
			}
			if (array != null)
			{
				int[] array2 = new int[] { -1 };
				Guid empty = Guid.Empty;
				int idsOfNames = obj.GetIDsOfNames(ref empty, array, 1, SafeNativeMethods.GetThreadLCID(), array2);
				if (NativeMethods.Succeeded(idsOfNames))
				{
					num = array2[0];
				}
			}
			return num;
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x001464E8 File Offset: 0x001446E8
		public static Com2Properties GetProperties(object obj)
		{
			if (obj == null || !Marshal.IsComObject(obj))
			{
				return null;
			}
			UnsafeNativeMethods.ITypeInfo[] array = Com2TypeInfoProcessor.FindTypeInfos(obj, false);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			int num = -1;
			int num2 = -1;
			ArrayList arrayList = new ArrayList();
			Guid[] array2 = new Guid[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				UnsafeNativeMethods.ITypeInfo typeInfo = array[i];
				if (typeInfo != null)
				{
					int[] array3 = new int[2];
					Guid guidForTypeInfo = Com2TypeInfoProcessor.GetGuidForTypeInfo(typeInfo, null, array3);
					PropertyDescriptor[] array4 = null;
					bool flag = guidForTypeInfo != Guid.Empty && Com2TypeInfoProcessor.processedLibraries != null && Com2TypeInfoProcessor.processedLibraries.Contains(guidForTypeInfo);
					if (flag)
					{
						Com2TypeInfoProcessor.CachedProperties cachedProperties = (Com2TypeInfoProcessor.CachedProperties)Com2TypeInfoProcessor.processedLibraries[guidForTypeInfo];
						if (array3[0] == cachedProperties.MajorVersion && array3[1] == cachedProperties.MinorVersion)
						{
							array4 = cachedProperties.Properties;
							if (i == 0 && cachedProperties.DefaultIndex != -1)
							{
								num = cachedProperties.DefaultIndex;
							}
						}
						else
						{
							flag = false;
						}
					}
					if (!flag)
					{
						array4 = Com2TypeInfoProcessor.InternalGetProperties(obj, typeInfo, -1, ref num2);
						if (i == 0 && num2 != -1)
						{
							num = num2;
						}
						if (Com2TypeInfoProcessor.processedLibraries == null)
						{
							Com2TypeInfoProcessor.processedLibraries = new Hashtable();
						}
						if (guidForTypeInfo != Guid.Empty)
						{
							Com2TypeInfoProcessor.processedLibraries[guidForTypeInfo] = new Com2TypeInfoProcessor.CachedProperties(array4, (i == 0) ? num : (-1), array3[0], array3[1]);
						}
					}
					if (array4 != null)
					{
						arrayList.AddRange(array4);
					}
				}
			}
			Com2PropertyDescriptor[] array5 = new Com2PropertyDescriptor[arrayList.Count];
			arrayList.CopyTo(array5, 0);
			return new Com2Properties(obj, array5, num);
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x00146678 File Offset: 0x00144878
		private static Guid GetGuidForTypeInfo(UnsafeNativeMethods.ITypeInfo typeInfo, Com2TypeInfoProcessor.StructCache structCache, int[] versions)
		{
			IntPtr zero = IntPtr.Zero;
			int typeAttr = typeInfo.GetTypeAttr(ref zero);
			if (!NativeMethods.Succeeded(typeAttr))
			{
				throw new ExternalException(SR.GetString("TYPEINFOPROCESSORGetTypeAttrFailed", new object[] { typeAttr }), typeAttr);
			}
			Guid guid = Guid.Empty;
			NativeMethods.tagTYPEATTR tagTYPEATTR = null;
			try
			{
				if (structCache == null)
				{
					tagTYPEATTR = new NativeMethods.tagTYPEATTR();
				}
				else
				{
					tagTYPEATTR = (NativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(NativeMethods.tagTYPEATTR));
				}
				UnsafeNativeMethods.PtrToStructure(zero, tagTYPEATTR);
				guid = tagTYPEATTR.guid;
				if (versions != null)
				{
					versions[0] = (int)tagTYPEATTR.wMajorVerNum;
					versions[1] = (int)tagTYPEATTR.wMinorVerNum;
				}
			}
			finally
			{
				typeInfo.ReleaseTypeAttr(zero);
				if (structCache != null && tagTYPEATTR != null)
				{
					structCache.ReleaseStruct(tagTYPEATTR);
				}
			}
			return guid;
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x00146730 File Offset: 0x00144930
		private static Type GetValueTypeFromTypeDesc(NativeMethods.tagTYPEDESC typeDesc, UnsafeNativeMethods.ITypeInfo typeInfo, object[] typeData, Com2TypeInfoProcessor.StructCache structCache)
		{
			NativeMethods.tagVT vt = (NativeMethods.tagVT)typeDesc.vt;
			if (vt > NativeMethods.tagVT.VT_UNKNOWN)
			{
				IntPtr intPtr;
				if (vt != NativeMethods.tagVT.VT_PTR)
				{
					if (vt != NativeMethods.tagVT.VT_USERDEFINED)
					{
						goto IL_2A;
					}
					intPtr = typeDesc.unionMember;
				}
				else
				{
					NativeMethods.tagTYPEDESC tagTYPEDESC = (NativeMethods.tagTYPEDESC)structCache.GetStruct(typeof(NativeMethods.tagTYPEDESC));
					try
					{
						try
						{
							UnsafeNativeMethods.PtrToStructure(typeDesc.unionMember, tagTYPEDESC);
						}
						catch
						{
							tagTYPEDESC = new NativeMethods.tagTYPEDESC();
							tagTYPEDESC.unionMember = (IntPtr)Marshal.ReadInt32(typeDesc.unionMember);
							tagTYPEDESC.vt = Marshal.ReadInt16(typeDesc.unionMember, 4);
						}
						if (tagTYPEDESC.vt == 12)
						{
							return Com2TypeInfoProcessor.VTToType((NativeMethods.tagVT)tagTYPEDESC.vt);
						}
						intPtr = tagTYPEDESC.unionMember;
					}
					finally
					{
						structCache.ReleaseStruct(tagTYPEDESC);
					}
				}
				UnsafeNativeMethods.ITypeInfo typeInfo2 = null;
				int num = typeInfo.GetRefTypeInfo(intPtr, ref typeInfo2);
				if (!NativeMethods.Succeeded(num))
				{
					throw new ExternalException(SR.GetString("TYPEINFOPROCESSORGetRefTypeInfoFailed", new object[] { num }), num);
				}
				try
				{
					if (typeInfo2 != null)
					{
						IntPtr zero = IntPtr.Zero;
						num = typeInfo2.GetTypeAttr(ref zero);
						if (!NativeMethods.Succeeded(num))
						{
							throw new ExternalException(SR.GetString("TYPEINFOPROCESSORGetTypeAttrFailed", new object[] { num }), num);
						}
						NativeMethods.tagTYPEATTR tagTYPEATTR = (NativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(NativeMethods.tagTYPEATTR));
						UnsafeNativeMethods.PtrToStructure(zero, tagTYPEATTR);
						try
						{
							Guid guid = tagTYPEATTR.guid;
							if (!Guid.Empty.Equals(guid))
							{
								typeData[0] = guid;
							}
							switch (tagTYPEATTR.typekind)
							{
							case 0:
								return Com2TypeInfoProcessor.ProcessTypeInfoEnum(typeInfo2, structCache);
							case 3:
							case 5:
								return Com2TypeInfoProcessor.VTToType(NativeMethods.tagVT.VT_UNKNOWN);
							case 4:
								return Com2TypeInfoProcessor.VTToType(NativeMethods.tagVT.VT_DISPATCH);
							case 6:
								return Com2TypeInfoProcessor.GetValueTypeFromTypeDesc(tagTYPEATTR.Get_tdescAlias(), typeInfo2, typeData, structCache);
							}
							return null;
						}
						finally
						{
							typeInfo2.ReleaseTypeAttr(zero);
							structCache.ReleaseStruct(tagTYPEATTR);
						}
					}
				}
				finally
				{
					typeInfo2 = null;
				}
				return null;
			}
			if (vt == NativeMethods.tagVT.VT_DISPATCH || vt == NativeMethods.tagVT.VT_UNKNOWN)
			{
				typeData[0] = Com2TypeInfoProcessor.GetGuidForTypeInfo(typeInfo, structCache, null);
				return Com2TypeInfoProcessor.VTToType((NativeMethods.tagVT)typeDesc.vt);
			}
			IL_2A:
			return Com2TypeInfoProcessor.VTToType((NativeMethods.tagVT)typeDesc.vt);
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x0014698C File Offset: 0x00144B8C
		private static PropertyDescriptor[] InternalGetProperties(object obj, UnsafeNativeMethods.ITypeInfo typeInfo, int dispidToGet, ref int defaultIndex)
		{
			if (typeInfo == null)
			{
				return null;
			}
			Hashtable hashtable = new Hashtable();
			int nameDispId = Com2TypeInfoProcessor.GetNameDispId((UnsafeNativeMethods.IDispatch)obj);
			bool flag = false;
			Com2TypeInfoProcessor.StructCache structCache = new Com2TypeInfoProcessor.StructCache();
			try
			{
				Com2TypeInfoProcessor.ProcessFunctions(typeInfo, hashtable, dispidToGet, nameDispId, ref flag, structCache);
			}
			catch (ExternalException ex)
			{
			}
			try
			{
				Com2TypeInfoProcessor.ProcessVariables(typeInfo, hashtable, dispidToGet, nameDispId, structCache);
			}
			catch (ExternalException ex2)
			{
			}
			typeInfo = null;
			int num = hashtable.Count;
			if (flag)
			{
				num++;
			}
			PropertyDescriptor[] array = new PropertyDescriptor[num];
			int num2 = 0;
			object[] array2 = new object[1];
			ComNativeDescriptor instance = ComNativeDescriptor.Instance;
			foreach (object obj2 in hashtable.Values)
			{
				Com2TypeInfoProcessor.PropInfo propInfo = (Com2TypeInfoProcessor.PropInfo)obj2;
				if (!propInfo.NonBrowsable)
				{
					try
					{
						num2 = instance.GetPropertyValue(obj, propInfo.DispId, array2);
					}
					catch (ExternalException ex3)
					{
						num2 = ex3.ErrorCode;
					}
					if (!NativeMethods.Succeeded(num2))
					{
						propInfo.Attributes.Add(new BrowsableAttribute(false));
						propInfo.NonBrowsable = true;
					}
				}
				else
				{
					num2 = 0;
				}
				Attribute[] array3 = new Attribute[propInfo.Attributes.Count];
				propInfo.Attributes.CopyTo(array3, 0);
				array[propInfo.Index] = new Com2PropertyDescriptor(propInfo.DispId, propInfo.Name, array3, propInfo.ReadOnly != 2, propInfo.ValueType, propInfo.TypeData, !NativeMethods.Succeeded(num2));
				if (propInfo.IsDefault)
				{
					int index = propInfo.Index;
				}
			}
			if (flag)
			{
				array[array.Length - 1] = new Com2AboutBoxPropertyDescriptor();
			}
			return array;
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x00146B64 File Offset: 0x00144D64
		private static Com2TypeInfoProcessor.PropInfo ProcessDataCore(UnsafeNativeMethods.ITypeInfo typeInfo, IDictionary propInfoList, int dispid, int nameDispID, NativeMethods.tagTYPEDESC typeDesc, int flags, Com2TypeInfoProcessor.StructCache structCache)
		{
			string text = null;
			string text2 = null;
			int documentation = typeInfo.GetDocumentation(dispid, ref text, ref text2, null, null);
			ComNativeDescriptor instance = ComNativeDescriptor.Instance;
			if (!NativeMethods.Succeeded(documentation))
			{
				throw new COMException(SR.GetString("TYPEINFOPROCESSORGetDocumentationFailed", new object[]
				{
					dispid,
					documentation,
					instance.GetClassName(typeInfo)
				}), documentation);
			}
			if (text == null)
			{
				return null;
			}
			Com2TypeInfoProcessor.PropInfo propInfo = (Com2TypeInfoProcessor.PropInfo)propInfoList[text];
			if (propInfo == null)
			{
				propInfo = new Com2TypeInfoProcessor.PropInfo();
				propInfo.Index = propInfoList.Count;
				propInfoList[text] = propInfo;
				propInfo.Name = text;
				propInfo.DispId = dispid;
				propInfo.Attributes.Add(new DispIdAttribute(propInfo.DispId));
			}
			if (text2 != null)
			{
				propInfo.Attributes.Add(new DescriptionAttribute(text2));
			}
			if (propInfo.ValueType == null)
			{
				object[] array = new object[1];
				try
				{
					propInfo.ValueType = Com2TypeInfoProcessor.GetValueTypeFromTypeDesc(typeDesc, typeInfo, array, structCache);
				}
				catch (Exception ex)
				{
				}
				if (propInfo.ValueType == null)
				{
					propInfo.NonBrowsable = true;
				}
				if (propInfo.NonBrowsable)
				{
					flags |= 1024;
				}
				if (array[0] != null)
				{
					propInfo.TypeData = array[0];
				}
			}
			if ((flags & 1) != 0)
			{
				propInfo.ReadOnly = 1;
			}
			if ((flags & 64) != 0 || (flags & 1024) != 0 || propInfo.Name[0] == '_' || dispid == -515)
			{
				propInfo.Attributes.Add(new BrowsableAttribute(false));
				propInfo.NonBrowsable = true;
			}
			if ((flags & 512) != 0)
			{
				propInfo.IsDefault = true;
			}
			if ((flags & 4) != 0 && (flags & 16) != 0)
			{
				propInfo.Attributes.Add(new BindableAttribute(true));
			}
			if (dispid == nameDispID)
			{
				propInfo.Attributes.Add(new ParenthesizePropertyNameAttribute(true));
				propInfo.Attributes.Add(new MergablePropertyAttribute(false));
			}
			return propInfo;
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x00146D64 File Offset: 0x00144F64
		private static void ProcessFunctions(UnsafeNativeMethods.ITypeInfo typeInfo, IDictionary propInfoList, int dispidToGet, int nameDispID, ref bool addAboutBox, Com2TypeInfoProcessor.StructCache structCache)
		{
			IntPtr zero = IntPtr.Zero;
			int num = typeInfo.GetTypeAttr(ref zero);
			if (!NativeMethods.Succeeded(num) || zero == IntPtr.Zero)
			{
				throw new ExternalException(SR.GetString("TYPEINFOPROCESSORGetTypeAttrFailed", new object[] { num }), num);
			}
			NativeMethods.tagTYPEATTR tagTYPEATTR = (NativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(NativeMethods.tagTYPEATTR));
			UnsafeNativeMethods.PtrToStructure(zero, tagTYPEATTR);
			if (tagTYPEATTR == null)
			{
				return;
			}
			NativeMethods.tagFUNCDESC tagFUNCDESC = null;
			NativeMethods.tagELEMDESC tagELEMDESC = null;
			try
			{
				tagFUNCDESC = (NativeMethods.tagFUNCDESC)structCache.GetStruct(typeof(NativeMethods.tagFUNCDESC));
				tagELEMDESC = (NativeMethods.tagELEMDESC)structCache.GetStruct(typeof(NativeMethods.tagELEMDESC));
				for (int i = 0; i < (int)tagTYPEATTR.cFuncs; i++)
				{
					IntPtr zero2 = IntPtr.Zero;
					num = typeInfo.GetFuncDesc(i, ref zero2);
					if (NativeMethods.Succeeded(num) && !(zero2 == IntPtr.Zero))
					{
						UnsafeNativeMethods.PtrToStructure(zero2, tagFUNCDESC);
						try
						{
							if (tagFUNCDESC.invkind == 1 || (dispidToGet != -1 && tagFUNCDESC.memid != dispidToGet))
							{
								if (tagFUNCDESC.memid == -552)
								{
									addAboutBox = true;
								}
							}
							else
							{
								bool flag = tagFUNCDESC.invkind == 2;
								NativeMethods.tagTYPEDESC tagTYPEDESC;
								if (flag)
								{
									if (tagFUNCDESC.cParams != 0)
									{
										goto IL_194;
									}
									tagTYPEDESC = tagFUNCDESC.elemdescFunc.tdesc;
								}
								else
								{
									if (tagFUNCDESC.lprgelemdescParam == IntPtr.Zero || tagFUNCDESC.cParams != 1)
									{
										goto IL_194;
									}
									Marshal.PtrToStructure(tagFUNCDESC.lprgelemdescParam, tagELEMDESC);
									tagTYPEDESC = tagELEMDESC.tdesc;
								}
								Com2TypeInfoProcessor.PropInfo propInfo = Com2TypeInfoProcessor.ProcessDataCore(typeInfo, propInfoList, tagFUNCDESC.memid, nameDispID, tagTYPEDESC, (int)tagFUNCDESC.wFuncFlags, structCache);
								if (propInfo != null && !flag)
								{
									propInfo.ReadOnly = 2;
								}
							}
						}
						finally
						{
							typeInfo.ReleaseFuncDesc(zero2);
						}
					}
					IL_194:;
				}
			}
			finally
			{
				if (tagFUNCDESC != null)
				{
					structCache.ReleaseStruct(tagFUNCDESC);
				}
				if (tagELEMDESC != null)
				{
					structCache.ReleaseStruct(tagELEMDESC);
				}
				typeInfo.ReleaseTypeAttr(zero);
				structCache.ReleaseStruct(tagTYPEATTR);
			}
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x00146F78 File Offset: 0x00145178
		private static Type ProcessTypeInfoEnum(UnsafeNativeMethods.ITypeInfo enumTypeInfo, Com2TypeInfoProcessor.StructCache structCache)
		{
			if (enumTypeInfo == null)
			{
				return null;
			}
			try
			{
				IntPtr zero = IntPtr.Zero;
				int num = enumTypeInfo.GetTypeAttr(ref zero);
				if (!NativeMethods.Succeeded(num) || zero == IntPtr.Zero)
				{
					throw new ExternalException(SR.GetString("TYPEINFOPROCESSORGetTypeAttrFailed", new object[] { num }), num);
				}
				NativeMethods.tagTYPEATTR tagTYPEATTR = (NativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(NativeMethods.tagTYPEATTR));
				UnsafeNativeMethods.PtrToStructure(zero, tagTYPEATTR);
				if (zero == IntPtr.Zero)
				{
					return null;
				}
				try
				{
					int cVars = (int)tagTYPEATTR.cVars;
					ArrayList arrayList = new ArrayList();
					ArrayList arrayList2 = new ArrayList();
					NativeMethods.tagVARDESC tagVARDESC = (NativeMethods.tagVARDESC)structCache.GetStruct(typeof(NativeMethods.tagVARDESC));
					object obj = null;
					string text = null;
					string text2 = null;
					string text3 = null;
					enumTypeInfo.GetDocumentation(-1, ref text, ref text3, null, null);
					for (int i = 0; i < cVars; i++)
					{
						IntPtr zero2 = IntPtr.Zero;
						num = enumTypeInfo.GetVarDesc(i, ref zero2);
						if (NativeMethods.Succeeded(num) && !(zero2 == IntPtr.Zero))
						{
							try
							{
								UnsafeNativeMethods.PtrToStructure(zero2, tagVARDESC);
								if (tagVARDESC != null && tagVARDESC.varkind == 2 && !(tagVARDESC.unionMember == IntPtr.Zero))
								{
									text3 = (text2 = null);
									obj = null;
									num = enumTypeInfo.GetDocumentation(tagVARDESC.memid, ref text2, ref text3, null, null);
									if (NativeMethods.Succeeded(num))
									{
										try
										{
											obj = Marshal.GetObjectForNativeVariant(tagVARDESC.unionMember);
										}
										catch (Exception ex)
										{
										}
										arrayList2.Add(obj);
										string text4;
										if (text3 != null)
										{
											text4 = text3;
										}
										else
										{
											text4 = text2;
										}
										arrayList.Add(text4);
									}
								}
							}
							finally
							{
								if (zero2 != IntPtr.Zero)
								{
									enumTypeInfo.ReleaseVarDesc(zero2);
								}
							}
						}
					}
					structCache.ReleaseStruct(tagVARDESC);
					if (arrayList.Count > 0)
					{
						IntPtr iunknownForObject = Marshal.GetIUnknownForObject(enumTypeInfo);
						try
						{
							text = iunknownForObject.ToString() + "_" + text;
							if (Com2TypeInfoProcessor.builtEnums == null)
							{
								Com2TypeInfoProcessor.builtEnums = new Hashtable();
							}
							else if (Com2TypeInfoProcessor.builtEnums.ContainsKey(text))
							{
								return (Type)Com2TypeInfoProcessor.builtEnums[text];
							}
							Type type = typeof(int);
							if (arrayList2.Count > 0 && arrayList2[0] != null)
							{
								type = arrayList2[0].GetType();
							}
							EnumBuilder enumBuilder = Com2TypeInfoProcessor.ModuleBuilder.DefineEnum(text, TypeAttributes.Public, type);
							for (int j = 0; j < arrayList.Count; j++)
							{
								enumBuilder.DefineLiteral((string)arrayList[j], arrayList2[j]);
							}
							Type type2 = enumBuilder.CreateType();
							Com2TypeInfoProcessor.builtEnums[text] = type2;
							return type2;
						}
						finally
						{
							if (iunknownForObject != IntPtr.Zero)
							{
								Marshal.Release(iunknownForObject);
							}
						}
					}
				}
				finally
				{
					enumTypeInfo.ReleaseTypeAttr(zero);
					structCache.ReleaseStruct(tagTYPEATTR);
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x001472D8 File Offset: 0x001454D8
		private static void ProcessVariables(UnsafeNativeMethods.ITypeInfo typeInfo, IDictionary propInfoList, int dispidToGet, int nameDispID, Com2TypeInfoProcessor.StructCache structCache)
		{
			IntPtr zero = IntPtr.Zero;
			int num = typeInfo.GetTypeAttr(ref zero);
			if (!NativeMethods.Succeeded(num) || zero == IntPtr.Zero)
			{
				throw new ExternalException(SR.GetString("TYPEINFOPROCESSORGetTypeAttrFailed", new object[] { num }), num);
			}
			NativeMethods.tagTYPEATTR tagTYPEATTR = (NativeMethods.tagTYPEATTR)structCache.GetStruct(typeof(NativeMethods.tagTYPEATTR));
			UnsafeNativeMethods.PtrToStructure(zero, tagTYPEATTR);
			try
			{
				if (tagTYPEATTR != null)
				{
					NativeMethods.tagVARDESC tagVARDESC = (NativeMethods.tagVARDESC)structCache.GetStruct(typeof(NativeMethods.tagVARDESC));
					for (int i = 0; i < (int)tagTYPEATTR.cVars; i++)
					{
						IntPtr zero2 = IntPtr.Zero;
						num = typeInfo.GetVarDesc(i, ref zero2);
						if (NativeMethods.Succeeded(num) && !(zero2 == IntPtr.Zero))
						{
							UnsafeNativeMethods.PtrToStructure(zero2, tagVARDESC);
							try
							{
								if (tagVARDESC.varkind != 2 && (dispidToGet == -1 || tagVARDESC.memid == dispidToGet))
								{
									Com2TypeInfoProcessor.PropInfo propInfo = Com2TypeInfoProcessor.ProcessDataCore(typeInfo, propInfoList, tagVARDESC.memid, nameDispID, tagVARDESC.elemdescVar.tdesc, (int)tagVARDESC.wVarFlags, structCache);
									if (propInfo.ReadOnly != 1)
									{
										propInfo.ReadOnly = 2;
									}
								}
							}
							finally
							{
								if (zero2 != IntPtr.Zero)
								{
									typeInfo.ReleaseVarDesc(zero2);
								}
							}
						}
					}
					structCache.ReleaseStruct(tagVARDESC);
				}
			}
			finally
			{
				typeInfo.ReleaseTypeAttr(zero);
				structCache.ReleaseStruct(tagTYPEATTR);
			}
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x00147450 File Offset: 0x00145650
		private static Type VTToType(NativeMethods.tagVT vt)
		{
			if (vt <= NativeMethods.tagVT.VT_VECTOR)
			{
				switch (vt)
				{
				case NativeMethods.tagVT.VT_EMPTY:
				case NativeMethods.tagVT.VT_NULL:
					return null;
				case NativeMethods.tagVT.VT_I2:
					return typeof(short);
				case NativeMethods.tagVT.VT_I4:
				case NativeMethods.tagVT.VT_INT:
					return typeof(int);
				case NativeMethods.tagVT.VT_R4:
					return typeof(float);
				case NativeMethods.tagVT.VT_R8:
					return typeof(double);
				case NativeMethods.tagVT.VT_CY:
					return typeof(decimal);
				case NativeMethods.tagVT.VT_DATE:
					return typeof(DateTime);
				case NativeMethods.tagVT.VT_BSTR:
				case NativeMethods.tagVT.VT_LPSTR:
				case NativeMethods.tagVT.VT_LPWSTR:
					return typeof(string);
				case NativeMethods.tagVT.VT_DISPATCH:
					return typeof(UnsafeNativeMethods.IDispatch);
				case NativeMethods.tagVT.VT_ERROR:
				case NativeMethods.tagVT.VT_HRESULT:
					return typeof(int);
				case NativeMethods.tagVT.VT_BOOL:
					return typeof(bool);
				case NativeMethods.tagVT.VT_VARIANT:
					return typeof(Com2Variant);
				case NativeMethods.tagVT.VT_UNKNOWN:
					return typeof(object);
				case NativeMethods.tagVT.VT_DECIMAL:
				case (NativeMethods.tagVT)15:
				case NativeMethods.tagVT.VT_VOID:
				case NativeMethods.tagVT.VT_PTR:
				case NativeMethods.tagVT.VT_SAFEARRAY:
				case NativeMethods.tagVT.VT_CARRAY:
				case (NativeMethods.tagVT)32:
				case (NativeMethods.tagVT)33:
				case (NativeMethods.tagVT)34:
				case (NativeMethods.tagVT)35:
				case NativeMethods.tagVT.VT_RECORD:
				case (NativeMethods.tagVT)37:
				case (NativeMethods.tagVT)38:
				case (NativeMethods.tagVT)39:
				case (NativeMethods.tagVT)40:
				case (NativeMethods.tagVT)41:
				case (NativeMethods.tagVT)42:
				case (NativeMethods.tagVT)43:
				case (NativeMethods.tagVT)44:
				case (NativeMethods.tagVT)45:
				case (NativeMethods.tagVT)46:
				case (NativeMethods.tagVT)47:
				case (NativeMethods.tagVT)48:
				case (NativeMethods.tagVT)49:
				case (NativeMethods.tagVT)50:
				case (NativeMethods.tagVT)51:
				case (NativeMethods.tagVT)52:
				case (NativeMethods.tagVT)53:
				case (NativeMethods.tagVT)54:
				case (NativeMethods.tagVT)55:
				case (NativeMethods.tagVT)56:
				case (NativeMethods.tagVT)57:
				case (NativeMethods.tagVT)58:
				case (NativeMethods.tagVT)59:
				case (NativeMethods.tagVT)60:
				case (NativeMethods.tagVT)61:
				case (NativeMethods.tagVT)62:
				case (NativeMethods.tagVT)63:
				case NativeMethods.tagVT.VT_BLOB:
				case NativeMethods.tagVT.VT_STREAM:
				case NativeMethods.tagVT.VT_STORAGE:
				case NativeMethods.tagVT.VT_STREAMED_OBJECT:
				case NativeMethods.tagVT.VT_STORED_OBJECT:
				case NativeMethods.tagVT.VT_BLOB_OBJECT:
				case NativeMethods.tagVT.VT_CF:
					break;
				case NativeMethods.tagVT.VT_I1:
					return typeof(sbyte);
				case NativeMethods.tagVT.VT_UI1:
					return typeof(byte);
				case NativeMethods.tagVT.VT_UI2:
					return typeof(ushort);
				case NativeMethods.tagVT.VT_UI4:
				case NativeMethods.tagVT.VT_UINT:
					return typeof(uint);
				case NativeMethods.tagVT.VT_I8:
					return typeof(long);
				case NativeMethods.tagVT.VT_UI8:
					return typeof(ulong);
				case NativeMethods.tagVT.VT_USERDEFINED:
					throw new ArgumentException(SR.GetString("COM2UnhandledVT", new object[] { "VT_USERDEFINED" }));
				case NativeMethods.tagVT.VT_FILETIME:
					return typeof(NativeMethods.FILETIME);
				case NativeMethods.tagVT.VT_CLSID:
					return typeof(Guid);
				default:
					if (vt - NativeMethods.tagVT.VT_BSTR_BLOB > 1)
					{
					}
					break;
				}
			}
			else if (vt != NativeMethods.tagVT.VT_ARRAY && vt != NativeMethods.tagVT.VT_BYREF && vt != NativeMethods.tagVT.VT_RESERVED)
			{
			}
			string text = "COM2UnhandledVT";
			object[] array = new object[1];
			int num = 0;
			int num2 = (int)vt;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			throw new ArgumentException(SR.GetString(text, array));
		}

		// Token: 0x04003454 RID: 13396
		private static TraceSwitch DbgTypeInfoProcessorSwitch;

		// Token: 0x04003455 RID: 13397
		private static ModuleBuilder moduleBuilder;

		// Token: 0x04003456 RID: 13398
		private static Hashtable builtEnums;

		// Token: 0x04003457 RID: 13399
		private static Hashtable processedLibraries;

		// Token: 0x02000853 RID: 2131
		internal class CachedProperties
		{
			// Token: 0x06007068 RID: 28776 RVA: 0x0019B95F File Offset: 0x00199B5F
			internal CachedProperties(PropertyDescriptor[] props, int defIndex, int majVersion, int minVersion)
			{
				this.props = this.ClonePropertyDescriptors(props);
				this.MajorVersion = majVersion;
				this.MinorVersion = minVersion;
				this.defaultIndex = defIndex;
			}

			// Token: 0x17001887 RID: 6279
			// (get) Token: 0x06007069 RID: 28777 RVA: 0x0019B98A File Offset: 0x00199B8A
			public PropertyDescriptor[] Properties
			{
				get
				{
					return this.ClonePropertyDescriptors(this.props);
				}
			}

			// Token: 0x17001888 RID: 6280
			// (get) Token: 0x0600706A RID: 28778 RVA: 0x0019B998 File Offset: 0x00199B98
			public int DefaultIndex
			{
				get
				{
					return this.defaultIndex;
				}
			}

			// Token: 0x0600706B RID: 28779 RVA: 0x0019B9A0 File Offset: 0x00199BA0
			private PropertyDescriptor[] ClonePropertyDescriptors(PropertyDescriptor[] props)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[props.Length];
				for (int i = 0; i < props.Length; i++)
				{
					if (props[i] is ICloneable)
					{
						array[i] = (PropertyDescriptor)((ICloneable)props[i]).Clone();
					}
					else
					{
						array[i] = props[i];
					}
				}
				return array;
			}

			// Token: 0x04004391 RID: 17297
			private PropertyDescriptor[] props;

			// Token: 0x04004392 RID: 17298
			public readonly int MajorVersion;

			// Token: 0x04004393 RID: 17299
			public readonly int MinorVersion;

			// Token: 0x04004394 RID: 17300
			private int defaultIndex;
		}

		// Token: 0x02000854 RID: 2132
		public class StructCache
		{
			// Token: 0x0600706C RID: 28780 RVA: 0x0019B9EC File Offset: 0x00199BEC
			private Queue GetQueue(Type t, bool create)
			{
				object obj = this.queuedTypes[t];
				if (obj == null && create)
				{
					obj = new Queue();
					this.queuedTypes[t] = obj;
				}
				return (Queue)obj;
			}

			// Token: 0x0600706D RID: 28781 RVA: 0x0019BA28 File Offset: 0x00199C28
			public object GetStruct(Type t)
			{
				Queue queue = this.GetQueue(t, true);
				object obj;
				if (queue.Count == 0)
				{
					obj = Activator.CreateInstance(t);
				}
				else
				{
					obj = queue.Dequeue();
				}
				return obj;
			}

			// Token: 0x0600706E RID: 28782 RVA: 0x0019BA5C File Offset: 0x00199C5C
			public void ReleaseStruct(object str)
			{
				Type type = str.GetType();
				Queue queue = this.GetQueue(type, false);
				if (queue != null)
				{
					queue.Enqueue(str);
				}
			}

			// Token: 0x04004395 RID: 17301
			private Hashtable queuedTypes = new Hashtable();
		}

		// Token: 0x02000855 RID: 2133
		private class PropInfo
		{
			// Token: 0x17001889 RID: 6281
			// (get) Token: 0x06007070 RID: 28784 RVA: 0x0019BA96 File Offset: 0x00199C96
			// (set) Token: 0x06007071 RID: 28785 RVA: 0x0019BA9E File Offset: 0x00199C9E
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			// Token: 0x1700188A RID: 6282
			// (get) Token: 0x06007072 RID: 28786 RVA: 0x0019BAA7 File Offset: 0x00199CA7
			// (set) Token: 0x06007073 RID: 28787 RVA: 0x0019BAAF File Offset: 0x00199CAF
			public int DispId
			{
				get
				{
					return this.dispid;
				}
				set
				{
					this.dispid = value;
				}
			}

			// Token: 0x1700188B RID: 6283
			// (get) Token: 0x06007074 RID: 28788 RVA: 0x0019BAB8 File Offset: 0x00199CB8
			// (set) Token: 0x06007075 RID: 28789 RVA: 0x0019BAC0 File Offset: 0x00199CC0
			public Type ValueType
			{
				get
				{
					return this.valueType;
				}
				set
				{
					this.valueType = value;
				}
			}

			// Token: 0x1700188C RID: 6284
			// (get) Token: 0x06007076 RID: 28790 RVA: 0x0019BAC9 File Offset: 0x00199CC9
			public ArrayList Attributes
			{
				get
				{
					return this.attributes;
				}
			}

			// Token: 0x1700188D RID: 6285
			// (get) Token: 0x06007077 RID: 28791 RVA: 0x0019BAD1 File Offset: 0x00199CD1
			// (set) Token: 0x06007078 RID: 28792 RVA: 0x0019BAD9 File Offset: 0x00199CD9
			public int ReadOnly
			{
				get
				{
					return this.readOnly;
				}
				set
				{
					this.readOnly = value;
				}
			}

			// Token: 0x1700188E RID: 6286
			// (get) Token: 0x06007079 RID: 28793 RVA: 0x0019BAE2 File Offset: 0x00199CE2
			// (set) Token: 0x0600707A RID: 28794 RVA: 0x0019BAEA File Offset: 0x00199CEA
			public bool IsDefault
			{
				get
				{
					return this.isDefault;
				}
				set
				{
					this.isDefault = value;
				}
			}

			// Token: 0x1700188F RID: 6287
			// (get) Token: 0x0600707B RID: 28795 RVA: 0x0019BAF3 File Offset: 0x00199CF3
			// (set) Token: 0x0600707C RID: 28796 RVA: 0x0019BAFB File Offset: 0x00199CFB
			public object TypeData
			{
				get
				{
					return this.typeData;
				}
				set
				{
					this.typeData = value;
				}
			}

			// Token: 0x17001890 RID: 6288
			// (get) Token: 0x0600707D RID: 28797 RVA: 0x0019BB04 File Offset: 0x00199D04
			// (set) Token: 0x0600707E RID: 28798 RVA: 0x0019BB0C File Offset: 0x00199D0C
			public bool NonBrowsable
			{
				get
				{
					return this.nonbrowsable;
				}
				set
				{
					this.nonbrowsable = value;
				}
			}

			// Token: 0x17001891 RID: 6289
			// (get) Token: 0x0600707F RID: 28799 RVA: 0x0019BB15 File Offset: 0x00199D15
			// (set) Token: 0x06007080 RID: 28800 RVA: 0x0019BB1D File Offset: 0x00199D1D
			public int Index
			{
				get
				{
					return this.index;
				}
				set
				{
					this.index = value;
				}
			}

			// Token: 0x06007081 RID: 28801 RVA: 0x0019BB26 File Offset: 0x00199D26
			public override int GetHashCode()
			{
				if (this.name != null)
				{
					return this.name.GetHashCode();
				}
				return base.GetHashCode();
			}

			// Token: 0x04004396 RID: 17302
			public const int ReadOnlyUnknown = 0;

			// Token: 0x04004397 RID: 17303
			public const int ReadOnlyTrue = 1;

			// Token: 0x04004398 RID: 17304
			public const int ReadOnlyFalse = 2;

			// Token: 0x04004399 RID: 17305
			private string name;

			// Token: 0x0400439A RID: 17306
			private int dispid = -1;

			// Token: 0x0400439B RID: 17307
			private Type valueType;

			// Token: 0x0400439C RID: 17308
			private readonly ArrayList attributes = new ArrayList();

			// Token: 0x0400439D RID: 17309
			private int readOnly;

			// Token: 0x0400439E RID: 17310
			private bool isDefault;

			// Token: 0x0400439F RID: 17311
			private object typeData;

			// Token: 0x040043A0 RID: 17312
			private bool nonbrowsable;

			// Token: 0x040043A1 RID: 17313
			private int index;
		}
	}
}
