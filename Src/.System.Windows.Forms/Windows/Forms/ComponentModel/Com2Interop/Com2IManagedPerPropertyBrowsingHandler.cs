using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x0200049B RID: 1179
	[SuppressUnmanagedCodeSecurity]
	internal class Com2IManagedPerPropertyBrowsingHandler : Com2ExtendedBrowsingHandler
	{
		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x06004E9C RID: 20124 RVA: 0x00143584 File Offset: 0x00141784
		public override Type Interface
		{
			get
			{
				return typeof(NativeMethods.IManagedPerPropertyBrowsing);
			}
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x00143590 File Offset: 0x00141790
		public override void SetupPropertyHandlers(Com2PropertyDescriptor[] propDesc)
		{
			if (propDesc == null)
			{
				return;
			}
			for (int i = 0; i < propDesc.Length; i++)
			{
				propDesc[i].QueryGetDynamicAttributes += this.OnGetAttributes;
			}
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x001435C4 File Offset: 0x001417C4
		private void OnGetAttributes(Com2PropertyDescriptor sender, GetAttributesEvent attrEvent)
		{
			object targetObject = sender.TargetObject;
			if (targetObject is NativeMethods.IManagedPerPropertyBrowsing)
			{
				Attribute[] componentAttributes = Com2IManagedPerPropertyBrowsingHandler.GetComponentAttributes((NativeMethods.IManagedPerPropertyBrowsing)targetObject, sender.DISPID);
				if (componentAttributes != null)
				{
					for (int i = 0; i < componentAttributes.Length; i++)
					{
						attrEvent.Add(componentAttributes[i]);
					}
				}
			}
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x0014360C File Offset: 0x0014180C
		internal static Attribute[] GetComponentAttributes(NativeMethods.IManagedPerPropertyBrowsing target, int dispid)
		{
			int num = 0;
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			if (target.GetPropertyAttributes(dispid, ref num, ref zero, ref zero2) != 0 || num == 0)
			{
				return new Attribute[0];
			}
			ArrayList arrayList = new ArrayList();
			string[] stringsFromPtr = Com2IManagedPerPropertyBrowsingHandler.GetStringsFromPtr(zero, num);
			object[] variantsFromPtr = Com2IManagedPerPropertyBrowsingHandler.GetVariantsFromPtr(zero2, num);
			if (stringsFromPtr.Length != variantsFromPtr.Length)
			{
				return new Attribute[0];
			}
			Type[] array = new Type[stringsFromPtr.Length];
			int i = 0;
			while (i < stringsFromPtr.Length)
			{
				string text = stringsFromPtr[i];
				Type type = Type.GetType(text);
				Assembly assembly = null;
				if (type != null)
				{
					assembly = type.Assembly;
				}
				if (!(type == null))
				{
					goto IL_192;
				}
				string text2 = "";
				int num2 = text.LastIndexOf(',');
				if (num2 != -1)
				{
					text2 = text.Substring(num2);
					text = text.Substring(0, num2);
				}
				int num3 = text.LastIndexOf('.');
				if (num3 != -1)
				{
					string text3 = text.Substring(num3 + 1);
					if (assembly == null)
					{
						type = Type.GetType(text.Substring(0, num3) + text2);
					}
					else
					{
						type = assembly.GetType(text.Substring(0, num3) + text2);
					}
					if (!(type == null) && typeof(Attribute).IsAssignableFrom(type))
					{
						if (!(type != null))
						{
							goto IL_192;
						}
						FieldInfo field = type.GetField(text3);
						if (!(field != null) || !field.IsStatic)
						{
							goto IL_192;
						}
						object value = field.GetValue(null);
						if (!(value is Attribute))
						{
							goto IL_192;
						}
						arrayList.Add(value);
					}
				}
				IL_252:
				i++;
				continue;
				IL_192:
				if (!typeof(Attribute).IsAssignableFrom(type))
				{
					goto IL_252;
				}
				if (!Convert.IsDBNull(variantsFromPtr[i]) && variantsFromPtr[i] != null)
				{
					ConstructorInfo[] constructors = type.GetConstructors();
					for (int j = 0; j < constructors.Length; j++)
					{
						ParameterInfo[] parameters = constructors[j].GetParameters();
						if (parameters.Length == 1 && parameters[0].ParameterType.IsAssignableFrom(variantsFromPtr[i].GetType()))
						{
							try
							{
								Attribute attribute = (Attribute)Activator.CreateInstance(type, new object[] { variantsFromPtr[i] });
								arrayList.Add(attribute);
							}
							catch
							{
							}
						}
					}
					goto IL_252;
				}
				try
				{
					Attribute attribute = (Attribute)Activator.CreateInstance(type);
					arrayList.Add(attribute);
				}
				catch
				{
				}
				goto IL_252;
			}
			Attribute[] array2 = new Attribute[arrayList.Count];
			arrayList.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x001438B4 File Offset: 0x00141AB4
		private static string[] GetStringsFromPtr(IntPtr ptr, int cStrings)
		{
			if (ptr != IntPtr.Zero)
			{
				string[] array = new string[cStrings];
				for (int i = 0; i < cStrings; i++)
				{
					try
					{
						IntPtr intPtr = Marshal.ReadIntPtr(ptr, i * 4);
						if (intPtr != IntPtr.Zero)
						{
							array[i] = Marshal.PtrToStringUni(intPtr);
							SafeNativeMethods.SysFreeString(new HandleRef(null, intPtr));
						}
						else
						{
							array[i] = "";
						}
					}
					catch (Exception ex)
					{
					}
				}
				try
				{
					Marshal.FreeCoTaskMem(ptr);
				}
				catch (Exception ex2)
				{
				}
				return array;
			}
			return new string[0];
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x00143950 File Offset: 0x00141B50
		private static object[] GetVariantsFromPtr(IntPtr ptr, int cVariants)
		{
			if (ptr != IntPtr.Zero)
			{
				object[] array = new object[cVariants];
				for (int i = 0; i < cVariants; i++)
				{
					try
					{
						IntPtr intPtr = (IntPtr)((long)ptr + (long)(i * 16));
						if (intPtr != IntPtr.Zero)
						{
							array[i] = Marshal.GetObjectForNativeVariant(intPtr);
							SafeNativeMethods.VariantClear(new HandleRef(null, intPtr));
						}
						else
						{
							array[i] = Convert.DBNull;
						}
					}
					catch (Exception ex)
					{
					}
				}
				try
				{
					Marshal.FreeCoTaskMem(ptr);
				}
				catch (Exception ex2)
				{
				}
				return array;
			}
			return new object[cVariants];
		}
	}
}
