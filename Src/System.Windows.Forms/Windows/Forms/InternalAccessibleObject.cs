using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Accessibility;

namespace System.Windows.Forms
{
	// Token: 0x02000118 RID: 280
	internal sealed class InternalAccessibleObject : StandardOleMarshalObject, UnsafeNativeMethods.IAccessibleInternal, IReflect, UnsafeNativeMethods.IServiceProvider, UnsafeNativeMethods.IAccessibleEx, UnsafeNativeMethods.IRawElementProviderSimple, UnsafeNativeMethods.IRawElementProviderFragment, UnsafeNativeMethods.IRawElementProviderFragmentRoot, UnsafeNativeMethods.IInvokeProvider, UnsafeNativeMethods.IValueProvider, UnsafeNativeMethods.IRangeValueProvider, UnsafeNativeMethods.IExpandCollapseProvider, UnsafeNativeMethods.IToggleProvider, UnsafeNativeMethods.ITableProvider, UnsafeNativeMethods.ITableItemProvider, UnsafeNativeMethods.IGridProvider, UnsafeNativeMethods.IGridItemProvider, UnsafeNativeMethods.IEnumVariant, UnsafeNativeMethods.IOleWindow, UnsafeNativeMethods.ILegacyIAccessibleProvider, UnsafeNativeMethods.ISelectionProvider, UnsafeNativeMethods.ISelectionItemProvider, UnsafeNativeMethods.IScrollItemProvider, UnsafeNativeMethods.IRawElementProviderHwndOverride, UnsafeNativeMethods.UiaCore.ITextProvider, UnsafeNativeMethods.UiaCore.ITextProvider2
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x000173D4 File Offset: 0x000155D4
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal InternalAccessibleObject(AccessibleObject accessibleImplemention)
		{
			this.publicIAccessible = accessibleImplemention;
			this.publicIEnumVariant = accessibleImplemention;
			this.publicIOleWindow = accessibleImplemention;
			this.publicIReflect = accessibleImplemention;
			this.publicIServiceProvider = accessibleImplemention;
			this.publicIAccessibleEx = accessibleImplemention;
			this.publicIRawElementProviderSimple = accessibleImplemention;
			this.publicIRawElementProviderFragment = accessibleImplemention;
			this.publicIRawElementProviderFragmentRoot = accessibleImplemention;
			this.publicIInvokeProvider = accessibleImplemention;
			this.publicIValueProvider = accessibleImplemention;
			this.publicIRangeValueProvider = accessibleImplemention;
			this.publicIExpandCollapseProvider = accessibleImplemention;
			this.publicIToggleProvider = accessibleImplemention;
			this.publicITableProvider = accessibleImplemention;
			this.publicITableItemProvider = accessibleImplemention;
			this.publicIGridProvider = accessibleImplemention;
			this.publicIGridItemProvider = accessibleImplemention;
			this.publicILegacyIAccessibleProvider = accessibleImplemention;
			this.publicISelectionProvider = accessibleImplemention;
			this.publicISelectionItemProvider = accessibleImplemention;
			this.publicIScrollItemProvider = accessibleImplemention;
			this.publicIRawElementProviderHwndOverride = accessibleImplemention;
			this.publicITextProvider2 = accessibleImplemention;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00017490 File Offset: 0x00015690
		internal void ClearAccessibleObject()
		{
			this.publicIAccessible = null;
			this.publicIEnumVariant = null;
			this.publicIOleWindow = null;
			this.publicIReflect = null;
			this.publicIServiceProvider = null;
			this.publicIAccessibleEx = null;
			this.publicIRawElementProviderSimple = null;
			this.publicIRawElementProviderFragment = null;
			this.publicIRawElementProviderFragmentRoot = null;
			this.publicIInvokeProvider = null;
			this.publicIValueProvider = null;
			this.publicIRangeValueProvider = null;
			this.publicIExpandCollapseProvider = null;
			this.publicIToggleProvider = null;
			this.publicITableProvider = null;
			this.publicITableItemProvider = null;
			this.publicIGridProvider = null;
			this.publicIGridItemProvider = null;
			this.publicILegacyIAccessibleProvider = null;
			this.publicISelectionProvider = null;
			this.publicISelectionItemProvider = null;
			this.publicIScrollItemProvider = null;
			this.publicIRawElementProviderHwndOverride = null;
			this.publicITextProvider2 = null;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00017545 File Offset: 0x00015745
		private object AsNativeAccessible(object accObject)
		{
			if (accObject is AccessibleObject)
			{
				return new InternalAccessibleObject(accObject as AccessibleObject);
			}
			return accObject;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001755C File Offset: 0x0001575C
		private object[] AsArrayOfNativeAccessibles(object[] accObjectArray)
		{
			if (accObjectArray != null && accObjectArray.Length != 0)
			{
				for (int i = 0; i < accObjectArray.Length; i++)
				{
					accObjectArray[i] = this.AsNativeAccessible(accObjectArray[i]);
				}
			}
			return accObjectArray;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001758B File Offset: 0x0001578B
		void UnsafeNativeMethods.IAccessibleInternal.accDoDefaultAction(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIAccessible.accDoDefaultAction(childID);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000175A3 File Offset: 0x000157A3
		object UnsafeNativeMethods.IAccessibleInternal.accHitTest(int xLeft, int yTop)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIAccessible.accHitTest(xLeft, yTop));
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000175C2 File Offset: 0x000157C2
		void UnsafeNativeMethods.IAccessibleInternal.accLocation(out int l, out int t, out int w, out int h, object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIAccessible.accLocation(out l, out t, out w, out h, childID);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000175E0 File Offset: 0x000157E0
		object UnsafeNativeMethods.IAccessibleInternal.accNavigate(int navDir, object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIAccessible.accNavigate(navDir, childID));
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000175FF File Offset: 0x000157FF
		void UnsafeNativeMethods.IAccessibleInternal.accSelect(int flagsSelect, object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIAccessible.accSelect(flagsSelect, childID);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00017618 File Offset: 0x00015818
		object UnsafeNativeMethods.IAccessibleInternal.get_accChild(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIAccessible.get_accChild(childID));
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00017636 File Offset: 0x00015836
		int UnsafeNativeMethods.IAccessibleInternal.get_accChildCount()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.accChildCount;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001764D File Offset: 0x0001584D
		string UnsafeNativeMethods.IAccessibleInternal.get_accDefaultAction(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accDefaultAction(childID);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00017665 File Offset: 0x00015865
		string UnsafeNativeMethods.IAccessibleInternal.get_accDescription(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accDescription(childID);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001767D File Offset: 0x0001587D
		object UnsafeNativeMethods.IAccessibleInternal.get_accFocus()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIAccessible.accFocus);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001769A File Offset: 0x0001589A
		string UnsafeNativeMethods.IAccessibleInternal.get_accHelp(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accHelp(childID);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x000176B2 File Offset: 0x000158B2
		int UnsafeNativeMethods.IAccessibleInternal.get_accHelpTopic(out string pszHelpFile, object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accHelpTopic(out pszHelpFile, childID);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000176CB File Offset: 0x000158CB
		string UnsafeNativeMethods.IAccessibleInternal.get_accKeyboardShortcut(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accKeyboardShortcut(childID);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000176E3 File Offset: 0x000158E3
		string UnsafeNativeMethods.IAccessibleInternal.get_accName(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accName(childID);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000176FB File Offset: 0x000158FB
		object UnsafeNativeMethods.IAccessibleInternal.get_accParent()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIAccessible.accParent);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00017718 File Offset: 0x00015918
		object UnsafeNativeMethods.IAccessibleInternal.get_accRole(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accRole(childID);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00017730 File Offset: 0x00015930
		object UnsafeNativeMethods.IAccessibleInternal.get_accSelection()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIAccessible.accSelection);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001774D File Offset: 0x0001594D
		object UnsafeNativeMethods.IAccessibleInternal.get_accState(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accState(childID);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00017765 File Offset: 0x00015965
		string UnsafeNativeMethods.IAccessibleInternal.get_accValue(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accValue(childID);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001777D File Offset: 0x0001597D
		void UnsafeNativeMethods.IAccessibleInternal.set_accName(object childID, string newName)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIAccessible.set_accName(childID, newName);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00017796 File Offset: 0x00015996
		void UnsafeNativeMethods.IAccessibleInternal.set_accValue(object childID, string newValue)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIAccessible.set_accValue(childID, newValue);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000177AF File Offset: 0x000159AF
		void UnsafeNativeMethods.IEnumVariant.Clone(UnsafeNativeMethods.IEnumVariant[] v)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIEnumVariant.Clone(v);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x000177C7 File Offset: 0x000159C7
		int UnsafeNativeMethods.IEnumVariant.Next(int n, IntPtr rgvar, int[] ns)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIEnumVariant.Next(n, rgvar, ns);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000177E1 File Offset: 0x000159E1
		void UnsafeNativeMethods.IEnumVariant.Reset()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIEnumVariant.Reset();
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000177F8 File Offset: 0x000159F8
		void UnsafeNativeMethods.IEnumVariant.Skip(int n)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIEnumVariant.Skip(n);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00017810 File Offset: 0x00015A10
		int UnsafeNativeMethods.IOleWindow.GetWindow(out IntPtr hwnd)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIOleWindow.GetWindow(out hwnd);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00017828 File Offset: 0x00015A28
		void UnsafeNativeMethods.IOleWindow.ContextSensitiveHelp(int fEnterMode)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIOleWindow.ContextSensitiveHelp(fEnterMode);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00017840 File Offset: 0x00015A40
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.publicIReflect.GetMethod(name, bindingAttr, binder, types, modifiers);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00017854 File Offset: 0x00015A54
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetMethod(name, bindingAttr);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00017863 File Offset: 0x00015A63
		MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetMethods(bindingAttr);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00017871 File Offset: 0x00015A71
		FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetField(name, bindingAttr);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00017880 File Offset: 0x00015A80
		FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetFields(bindingAttr);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001788E File Offset: 0x00015A8E
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetProperty(name, bindingAttr);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001789D File Offset: 0x00015A9D
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return this.publicIReflect.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000178B3 File Offset: 0x00015AB3
		PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetProperties(bindingAttr);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000178C1 File Offset: 0x00015AC1
		MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetMember(name, bindingAttr);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000178D0 File Offset: 0x00015AD0
		MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetMembers(bindingAttr);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000178E0 File Offset: 0x00015AE0
		object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			IntSecurity.UnmanagedCode.Demand();
			return this.publicIReflect.InvokeMember(name, invokeAttr, binder, this.publicIAccessible, args, modifiers, culture, namedParameters);
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00017914 File Offset: 0x00015B14
		Type IReflect.UnderlyingSystemType
		{
			get
			{
				IReflect reflect = this.publicIReflect;
				return this.publicIReflect.UnderlyingSystemType;
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00017934 File Offset: 0x00015B34
		int UnsafeNativeMethods.IServiceProvider.QueryService(ref Guid service, ref Guid riid, out IntPtr ppvObject)
		{
			IntSecurity.UnmanagedCode.Assert();
			ppvObject = IntPtr.Zero;
			int num = this.publicIServiceProvider.QueryService(ref service, ref riid, out ppvObject);
			if (num >= 0)
			{
				ppvObject = Marshal.GetComInterfaceForObject(this, typeof(UnsafeNativeMethods.IAccessibleEx));
			}
			return num;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00017978 File Offset: 0x00015B78
		object UnsafeNativeMethods.IAccessibleEx.GetObjectForChild(int idChild)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessibleEx.GetObjectForChild(idChild);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00017990 File Offset: 0x00015B90
		int UnsafeNativeMethods.IAccessibleEx.GetIAccessiblePair(out object ppAcc, out int pidChild)
		{
			IntSecurity.UnmanagedCode.Assert();
			ppAcc = this;
			pidChild = 0;
			return 0;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000179A3 File Offset: 0x00015BA3
		int[] UnsafeNativeMethods.IAccessibleEx.GetRuntimeId()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessibleEx.GetRuntimeId();
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000179BA File Offset: 0x00015BBA
		int UnsafeNativeMethods.IAccessibleEx.ConvertReturnedElement(object pIn, out object ppRetValOut)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessibleEx.ConvertReturnedElement(pIn, out ppRetValOut);
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x000179D3 File Offset: 0x00015BD3
		UnsafeNativeMethods.ProviderOptions UnsafeNativeMethods.IRawElementProviderSimple.ProviderOptions
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRawElementProviderSimple.ProviderOptions;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x000179EA File Offset: 0x00015BEA
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.IRawElementProviderSimple.HostRawElementProvider
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRawElementProviderSimple.HostRawElementProvider;
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00017A04 File Offset: 0x00015C04
		object UnsafeNativeMethods.IRawElementProviderSimple.GetPatternProvider(int patternId)
		{
			IntSecurity.UnmanagedCode.Assert();
			object patternProvider = this.publicIRawElementProviderSimple.GetPatternProvider(patternId);
			if (patternProvider == null)
			{
				return null;
			}
			if (patternId == 10005)
			{
				return this;
			}
			if (patternId == 10002)
			{
				return this;
			}
			if (AccessibilityImprovements.Level3 && patternId == 10003)
			{
				return this;
			}
			if (patternId == 10015)
			{
				return this;
			}
			if (patternId == 10012)
			{
				return this;
			}
			if (patternId == 10013)
			{
				return this;
			}
			if (patternId == 10006)
			{
				return this;
			}
			if (patternId == 10007)
			{
				return this;
			}
			if (AccessibilityImprovements.Level3 && patternId == 10000)
			{
				return this;
			}
			if (AccessibilityImprovements.Level3 && patternId == 10018)
			{
				return this;
			}
			if (AccessibilityImprovements.Level3 && patternId == 10001)
			{
				return this;
			}
			if (AccessibilityImprovements.Level3 && patternId == 10010)
			{
				return this;
			}
			if (AccessibilityImprovements.Level3 && patternId == 10017)
			{
				return this;
			}
			if (AccessibilityImprovements.Level5 && patternId == 10014)
			{
				return this;
			}
			if (AccessibilityImprovements.Level5 && patternId == 10024)
			{
				return this;
			}
			return null;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00017AFF File Offset: 0x00015CFF
		object UnsafeNativeMethods.IRawElementProviderSimple.GetPropertyValue(int propertyID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIRawElementProviderSimple.GetPropertyValue(propertyID);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00017B17 File Offset: 0x00015D17
		object UnsafeNativeMethods.IRawElementProviderFragment.Navigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIRawElementProviderFragment.Navigate(direction));
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00017B35 File Offset: 0x00015D35
		int[] UnsafeNativeMethods.IRawElementProviderFragment.GetRuntimeId()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIRawElementProviderFragment.GetRuntimeId();
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00017B4C File Offset: 0x00015D4C
		object[] UnsafeNativeMethods.IRawElementProviderFragment.GetEmbeddedFragmentRoots()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsArrayOfNativeAccessibles(this.publicIRawElementProviderFragment.GetEmbeddedFragmentRoots());
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00017B69 File Offset: 0x00015D69
		void UnsafeNativeMethods.IRawElementProviderFragment.SetFocus()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIRawElementProviderFragment.SetFocus();
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00017B80 File Offset: 0x00015D80
		NativeMethods.UiaRect UnsafeNativeMethods.IRawElementProviderFragment.BoundingRectangle
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRawElementProviderFragment.BoundingRectangle;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00017B97 File Offset: 0x00015D97
		UnsafeNativeMethods.IRawElementProviderFragmentRoot UnsafeNativeMethods.IRawElementProviderFragment.FragmentRoot
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				if (AccessibilityImprovements.Level3)
				{
					return this.publicIRawElementProviderFragment.FragmentRoot;
				}
				return this.AsNativeAccessible(this.publicIRawElementProviderFragment.FragmentRoot) as UnsafeNativeMethods.IRawElementProviderFragmentRoot;
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00017BCC File Offset: 0x00015DCC
		object UnsafeNativeMethods.IRawElementProviderFragmentRoot.ElementProviderFromPoint(double x, double y)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIRawElementProviderFragmentRoot.ElementProviderFromPoint(x, y));
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00017BEB File Offset: 0x00015DEB
		object UnsafeNativeMethods.IRawElementProviderFragmentRoot.GetFocus()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIRawElementProviderFragmentRoot.GetFocus());
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00017C08 File Offset: 0x00015E08
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.DefaultAction
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.DefaultAction;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00017C1F File Offset: 0x00015E1F
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Description
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.Description;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00017C36 File Offset: 0x00015E36
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Help
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.Help;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00017C4D File Offset: 0x00015E4D
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.KeyboardShortcut
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.KeyboardShortcut;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00017C64 File Offset: 0x00015E64
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Name
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.Name;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00017C7B File Offset: 0x00015E7B
		uint UnsafeNativeMethods.ILegacyIAccessibleProvider.Role
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.Role;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00017C92 File Offset: 0x00015E92
		uint UnsafeNativeMethods.ILegacyIAccessibleProvider.State
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.State;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00017CA9 File Offset: 0x00015EA9
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Value
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.Value;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00017CC0 File Offset: 0x00015EC0
		int UnsafeNativeMethods.ILegacyIAccessibleProvider.ChildId
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.ChildId;
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00017CD7 File Offset: 0x00015ED7
		void UnsafeNativeMethods.ILegacyIAccessibleProvider.DoDefaultAction()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicILegacyIAccessibleProvider.DoDefaultAction();
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00017CEE File Offset: 0x00015EEE
		IAccessible UnsafeNativeMethods.ILegacyIAccessibleProvider.GetIAccessible()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicILegacyIAccessibleProvider.GetIAccessible();
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00017D05 File Offset: 0x00015F05
		object[] UnsafeNativeMethods.ILegacyIAccessibleProvider.GetSelection()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsArrayOfNativeAccessibles(this.publicILegacyIAccessibleProvider.GetSelection());
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00017D22 File Offset: 0x00015F22
		void UnsafeNativeMethods.ILegacyIAccessibleProvider.Select(int flagsSelect)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicILegacyIAccessibleProvider.Select(flagsSelect);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00017D3A File Offset: 0x00015F3A
		void UnsafeNativeMethods.ILegacyIAccessibleProvider.SetValue(string szValue)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicILegacyIAccessibleProvider.SetValue(szValue);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00017D52 File Offset: 0x00015F52
		void UnsafeNativeMethods.IInvokeProvider.Invoke()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIInvokeProvider.Invoke();
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00017D69 File Offset: 0x00015F69
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider.GetSelection()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.GetSelection();
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00017D80 File Offset: 0x00015F80
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider.GetVisibleRanges()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.GetVisibleRanges();
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00017D97 File Offset: 0x00015F97
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider.RangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.RangeFromChild(childElement);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00017DAF File Offset: 0x00015FAF
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider.RangeFromPoint(Point screenLocation)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.RangeFromPoint(screenLocation);
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00017DC7 File Offset: 0x00015FC7
		UnsafeNativeMethods.UiaCore.SupportedTextSelection UnsafeNativeMethods.UiaCore.ITextProvider.SupportedTextSelection
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicITextProvider2.SupportedTextSelection;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00017DDE File Offset: 0x00015FDE
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider.DocumentRange
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicITextProvider2.DocumentRange;
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00017D69 File Offset: 0x00015F69
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider2.GetSelection()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.GetSelection();
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00017D80 File Offset: 0x00015F80
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider2.GetVisibleRanges()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.GetVisibleRanges();
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00017D97 File Offset: 0x00015F97
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.RangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.RangeFromChild(childElement);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00017DAF File Offset: 0x00015FAF
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.RangeFromPoint(Point screenLocation)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.RangeFromPoint(screenLocation);
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00017DC7 File Offset: 0x00015FC7
		UnsafeNativeMethods.UiaCore.SupportedTextSelection UnsafeNativeMethods.UiaCore.ITextProvider2.SupportedTextSelection
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicITextProvider2.SupportedTextSelection;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00017DDE File Offset: 0x00015FDE
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.DocumentRange
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicITextProvider2.DocumentRange;
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00017DF5 File Offset: 0x00015FF5
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.GetCaretRange(out UnsafeNativeMethods.BOOL isActive)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.GetCaretRange(out isActive);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00017E0D File Offset: 0x0001600D
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.RangeFromAnnotation(UnsafeNativeMethods.IRawElementProviderSimple annotationElement)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.RangeFromAnnotation(annotationElement);
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00017E25 File Offset: 0x00016025
		bool UnsafeNativeMethods.IValueProvider.IsReadOnly
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIValueProvider.IsReadOnly;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00017E3C File Offset: 0x0001603C
		string UnsafeNativeMethods.IValueProvider.Value
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIValueProvider.Value;
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00017E53 File Offset: 0x00016053
		void UnsafeNativeMethods.IValueProvider.SetValue(string newValue)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIValueProvider.SetValue(newValue);
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00017E25 File Offset: 0x00016025
		bool UnsafeNativeMethods.IRangeValueProvider.IsReadOnly
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIValueProvider.IsReadOnly;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00017E6B File Offset: 0x0001606B
		double UnsafeNativeMethods.IRangeValueProvider.LargeChange
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRangeValueProvider.LargeChange;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00017E82 File Offset: 0x00016082
		double UnsafeNativeMethods.IRangeValueProvider.Maximum
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRangeValueProvider.Maximum;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00017E99 File Offset: 0x00016099
		double UnsafeNativeMethods.IRangeValueProvider.Minimum
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRangeValueProvider.Minimum;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00017EB0 File Offset: 0x000160B0
		double UnsafeNativeMethods.IRangeValueProvider.SmallChange
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRangeValueProvider.SmallChange;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00017EC7 File Offset: 0x000160C7
		double UnsafeNativeMethods.IRangeValueProvider.Value
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRangeValueProvider.Value;
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00017EDE File Offset: 0x000160DE
		void UnsafeNativeMethods.IRangeValueProvider.SetValue(double newValue)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIRangeValueProvider.SetValue(newValue);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00017EF6 File Offset: 0x000160F6
		void UnsafeNativeMethods.IExpandCollapseProvider.Expand()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIExpandCollapseProvider.Expand();
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00017F0D File Offset: 0x0001610D
		void UnsafeNativeMethods.IExpandCollapseProvider.Collapse()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIExpandCollapseProvider.Collapse();
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00017F24 File Offset: 0x00016124
		UnsafeNativeMethods.ExpandCollapseState UnsafeNativeMethods.IExpandCollapseProvider.ExpandCollapseState
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIExpandCollapseProvider.ExpandCollapseState;
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00017F3B File Offset: 0x0001613B
		void UnsafeNativeMethods.IToggleProvider.Toggle()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIToggleProvider.Toggle();
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00017F52 File Offset: 0x00016152
		UnsafeNativeMethods.ToggleState UnsafeNativeMethods.IToggleProvider.ToggleState
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIToggleProvider.ToggleState;
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00017F69 File Offset: 0x00016169
		object[] UnsafeNativeMethods.ITableProvider.GetRowHeaders()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsArrayOfNativeAccessibles(this.publicITableProvider.GetRowHeaders());
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00017F86 File Offset: 0x00016186
		object[] UnsafeNativeMethods.ITableProvider.GetColumnHeaders()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsArrayOfNativeAccessibles(this.publicITableProvider.GetColumnHeaders());
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00017FA3 File Offset: 0x000161A3
		UnsafeNativeMethods.RowOrColumnMajor UnsafeNativeMethods.ITableProvider.RowOrColumnMajor
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicITableProvider.RowOrColumnMajor;
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00017FBA File Offset: 0x000161BA
		object[] UnsafeNativeMethods.ITableItemProvider.GetRowHeaderItems()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsArrayOfNativeAccessibles(this.publicITableItemProvider.GetRowHeaderItems());
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00017FD7 File Offset: 0x000161D7
		object[] UnsafeNativeMethods.ITableItemProvider.GetColumnHeaderItems()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsArrayOfNativeAccessibles(this.publicITableItemProvider.GetColumnHeaderItems());
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00017FF4 File Offset: 0x000161F4
		object UnsafeNativeMethods.IGridProvider.GetItem(int row, int column)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIGridProvider.GetItem(row, column));
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00018013 File Offset: 0x00016213
		int UnsafeNativeMethods.IGridProvider.RowCount
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIGridProvider.RowCount;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0001802A File Offset: 0x0001622A
		int UnsafeNativeMethods.IGridProvider.ColumnCount
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIGridProvider.ColumnCount;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00018041 File Offset: 0x00016241
		int UnsafeNativeMethods.IGridItemProvider.Row
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIGridItemProvider.Row;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00018058 File Offset: 0x00016258
		int UnsafeNativeMethods.IGridItemProvider.Column
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIGridItemProvider.Column;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0001806F File Offset: 0x0001626F
		int UnsafeNativeMethods.IGridItemProvider.RowSpan
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIGridItemProvider.RowSpan;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00018086 File Offset: 0x00016286
		int UnsafeNativeMethods.IGridItemProvider.ColumnSpan
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIGridItemProvider.ColumnSpan;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0001809D File Offset: 0x0001629D
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.IGridItemProvider.ContainingGrid
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				if (AccessibilityImprovements.Level3)
				{
					return this.publicIGridItemProvider.ContainingGrid;
				}
				return this.AsNativeAccessible(this.publicIGridItemProvider.ContainingGrid) as UnsafeNativeMethods.IRawElementProviderSimple;
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000180D2 File Offset: 0x000162D2
		object[] UnsafeNativeMethods.ISelectionProvider.GetSelection()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicISelectionProvider.GetSelection();
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x000180E9 File Offset: 0x000162E9
		bool UnsafeNativeMethods.ISelectionProvider.CanSelectMultiple
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicISelectionProvider.CanSelectMultiple;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00018100 File Offset: 0x00016300
		bool UnsafeNativeMethods.ISelectionProvider.IsSelectionRequired
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicISelectionProvider.IsSelectionRequired;
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00018117 File Offset: 0x00016317
		void UnsafeNativeMethods.ISelectionItemProvider.Select()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicISelectionItemProvider.Select();
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001812E File Offset: 0x0001632E
		void UnsafeNativeMethods.ISelectionItemProvider.AddToSelection()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicISelectionItemProvider.AddToSelection();
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00018145 File Offset: 0x00016345
		void UnsafeNativeMethods.ISelectionItemProvider.RemoveFromSelection()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicISelectionItemProvider.RemoveFromSelection();
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0001815C File Offset: 0x0001635C
		bool UnsafeNativeMethods.ISelectionItemProvider.IsSelected
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicISelectionItemProvider.IsSelected;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00018173 File Offset: 0x00016373
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.ISelectionItemProvider.SelectionContainer
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicISelectionItemProvider.SelectionContainer;
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001818A File Offset: 0x0001638A
		void UnsafeNativeMethods.IScrollItemProvider.ScrollIntoView()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIScrollItemProvider.ScrollIntoView();
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x000181A1 File Offset: 0x000163A1
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.IRawElementProviderHwndOverride.GetOverrideProviderForHwnd(IntPtr hwnd)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIRawElementProviderHwndOverride.GetOverrideProviderForHwnd(hwnd);
		}

		// Token: 0x04000538 RID: 1336
		private IAccessible publicIAccessible;

		// Token: 0x04000539 RID: 1337
		private UnsafeNativeMethods.IEnumVariant publicIEnumVariant;

		// Token: 0x0400053A RID: 1338
		private UnsafeNativeMethods.IOleWindow publicIOleWindow;

		// Token: 0x0400053B RID: 1339
		private IReflect publicIReflect;

		// Token: 0x0400053C RID: 1340
		private UnsafeNativeMethods.IServiceProvider publicIServiceProvider;

		// Token: 0x0400053D RID: 1341
		private UnsafeNativeMethods.IAccessibleEx publicIAccessibleEx;

		// Token: 0x0400053E RID: 1342
		private UnsafeNativeMethods.IRawElementProviderSimple publicIRawElementProviderSimple;

		// Token: 0x0400053F RID: 1343
		private UnsafeNativeMethods.IRawElementProviderFragment publicIRawElementProviderFragment;

		// Token: 0x04000540 RID: 1344
		private UnsafeNativeMethods.IRawElementProviderFragmentRoot publicIRawElementProviderFragmentRoot;

		// Token: 0x04000541 RID: 1345
		private UnsafeNativeMethods.IInvokeProvider publicIInvokeProvider;

		// Token: 0x04000542 RID: 1346
		private UnsafeNativeMethods.IValueProvider publicIValueProvider;

		// Token: 0x04000543 RID: 1347
		private UnsafeNativeMethods.IRangeValueProvider publicIRangeValueProvider;

		// Token: 0x04000544 RID: 1348
		private UnsafeNativeMethods.IExpandCollapseProvider publicIExpandCollapseProvider;

		// Token: 0x04000545 RID: 1349
		private UnsafeNativeMethods.IToggleProvider publicIToggleProvider;

		// Token: 0x04000546 RID: 1350
		private UnsafeNativeMethods.ITableProvider publicITableProvider;

		// Token: 0x04000547 RID: 1351
		private UnsafeNativeMethods.ITableItemProvider publicITableItemProvider;

		// Token: 0x04000548 RID: 1352
		private UnsafeNativeMethods.IGridProvider publicIGridProvider;

		// Token: 0x04000549 RID: 1353
		private UnsafeNativeMethods.IGridItemProvider publicIGridItemProvider;

		// Token: 0x0400054A RID: 1354
		private UnsafeNativeMethods.ILegacyIAccessibleProvider publicILegacyIAccessibleProvider;

		// Token: 0x0400054B RID: 1355
		private UnsafeNativeMethods.ISelectionProvider publicISelectionProvider;

		// Token: 0x0400054C RID: 1356
		private UnsafeNativeMethods.ISelectionItemProvider publicISelectionItemProvider;

		// Token: 0x0400054D RID: 1357
		private UnsafeNativeMethods.IScrollItemProvider publicIScrollItemProvider;

		// Token: 0x0400054E RID: 1358
		private UnsafeNativeMethods.IRawElementProviderHwndOverride publicIRawElementProviderHwndOverride;

		// Token: 0x0400054F RID: 1359
		private UnsafeNativeMethods.UiaCore.ITextProvider2 publicITextProvider2;
	}
}
