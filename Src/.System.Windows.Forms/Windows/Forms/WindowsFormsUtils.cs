using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms
{
	// Token: 0x02000444 RID: 1092
	internal sealed class WindowsFormsUtils
	{
		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06004BE6 RID: 19430 RVA: 0x0013B35C File Offset: 0x0013955C
		public static Point LastCursorPoint
		{
			get
			{
				int messagePos = SafeNativeMethods.GetMessagePos();
				return new Point(NativeMethods.Util.SignedLOWORD(messagePos), NativeMethods.Util.SignedHIWORD(messagePos));
			}
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x0013B380 File Offset: 0x00139580
		public static Graphics CreateMeasurementGraphics()
		{
			return Graphics.FromHdcInternal(WindowsGraphicsCacheManager.MeasurementGraphics.DeviceContext.Hdc);
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x0013B398 File Offset: 0x00139598
		public static bool ContainsMnemonic(string text)
		{
			if (text != null)
			{
				int length = text.Length;
				int num = text.IndexOf('&', 0);
				if (num >= 0 && num <= length - 2)
				{
					int num2 = text.IndexOf('&', num + 1);
					if (num2 == -1)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x0013B3D6 File Offset: 0x001395D6
		internal static Rectangle ConstrainToScreenWorkingAreaBounds(Rectangle bounds)
		{
			return WindowsFormsUtils.ConstrainToBounds(Screen.GetWorkingArea(bounds), bounds);
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x0013B3E4 File Offset: 0x001395E4
		internal static Rectangle ConstrainToScreenBounds(Rectangle bounds)
		{
			return WindowsFormsUtils.ConstrainToBounds(Screen.FromRectangle(bounds).Bounds, bounds);
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x0013B3F8 File Offset: 0x001395F8
		internal static Rectangle ConstrainToBounds(Rectangle constrainingBounds, Rectangle bounds)
		{
			if (!constrainingBounds.Contains(bounds))
			{
				bounds.Size = new Size(Math.Min(constrainingBounds.Width - 2, bounds.Width), Math.Min(constrainingBounds.Height - 2, bounds.Height));
				if (bounds.Right > constrainingBounds.Right)
				{
					bounds.X = constrainingBounds.Right - bounds.Width;
				}
				else if (bounds.Left < constrainingBounds.Left)
				{
					bounds.X = constrainingBounds.Left;
				}
				if (bounds.Bottom > constrainingBounds.Bottom)
				{
					bounds.Y = constrainingBounds.Bottom - 1 - bounds.Height;
				}
				else if (bounds.Top < constrainingBounds.Top)
				{
					bounds.Y = constrainingBounds.Top;
				}
			}
			return bounds;
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x0013B4D8 File Offset: 0x001396D8
		internal static string EscapeTextWithAmpersands(string text)
		{
			if (text == null)
			{
				return null;
			}
			int i = text.IndexOf('&');
			if (i == -1)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Substring(0, i));
			while (i < text.Length)
			{
				if (text[i] == '&')
				{
					stringBuilder.Append("&");
				}
				if (i < text.Length)
				{
					stringBuilder.Append(text[i]);
				}
				i++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x0013B54C File Offset: 0x0013974C
		internal static string GetControlInformation(IntPtr hwnd)
		{
			if (hwnd == IntPtr.Zero)
			{
				return "Handle is IntPtr.Zero";
			}
			return "";
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x0013B573 File Offset: 0x00139773
		internal static string AssertControlInformation(bool condition, Control control)
		{
			if (condition)
			{
				return string.Empty;
			}
			return WindowsFormsUtils.GetControlInformation(control.Handle);
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x0013B58C File Offset: 0x0013978C
		internal static int GetCombinedHashCodes(params int[] args)
		{
			int num = -757577119;
			for (int i = 0; i < args.Length; i++)
			{
				num = (args[i] ^ num) * -1640531535;
			}
			return num;
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x0013B5BC File Offset: 0x001397BC
		public static char GetMnemonic(string text, bool bConvertToUpperCase)
		{
			char c = '\0';
			if (text != null)
			{
				int length = text.Length;
				for (int i = 0; i < length - 1; i++)
				{
					if (text[i] == '&')
					{
						if (text[i + 1] == '&')
						{
							i++;
						}
						else
						{
							if (bConvertToUpperCase)
							{
								c = char.ToUpper(text[i + 1], CultureInfo.CurrentCulture);
								break;
							}
							c = char.ToLower(text[i + 1], CultureInfo.CurrentCulture);
							break;
						}
					}
				}
			}
			return c;
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x0013B634 File Offset: 0x00139834
		public static HandleRef GetRootHWnd(HandleRef hwnd)
		{
			IntPtr ancestor = UnsafeNativeMethods.GetAncestor(new HandleRef(hwnd, hwnd.Handle), 2);
			return new HandleRef(hwnd.Wrapper, ancestor);
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x0013B667 File Offset: 0x00139867
		public static HandleRef GetRootHWnd(Control control)
		{
			return WindowsFormsUtils.GetRootHWnd(new HandleRef(control, control.Handle));
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x0013B67C File Offset: 0x0013987C
		public static string TextWithoutMnemonics(string text)
		{
			if (text == null)
			{
				return null;
			}
			int i = text.IndexOf('&');
			if (i == -1)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Substring(0, i));
			while (i < text.Length)
			{
				if (text[i] == '&')
				{
					i++;
				}
				if (i < text.Length)
				{
					stringBuilder.Append(text[i]);
				}
				i++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x0013B6E8 File Offset: 0x001398E8
		public static Point TranslatePoint(Point point, Control fromControl, Control toControl)
		{
			NativeMethods.POINT point2 = new NativeMethods.POINT(point.X, point.Y);
			UnsafeNativeMethods.MapWindowPoints(new HandleRef(fromControl, fromControl.Handle), new HandleRef(toControl, toControl.Handle), point2, 1);
			return new Point(point2.x, point2.y);
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x0013B73A File Offset: 0x0013993A
		public static bool SafeCompareStrings(string string1, string string2, bool ignoreCase)
		{
			return string1 != null && string2 != null && string1.Length == string2.Length && string.Compare(string1, string2, ignoreCase, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x0013B764 File Offset: 0x00139964
		public static int RotateLeft(int value, int nBits)
		{
			nBits %= 32;
			return (value << nBits) | (value >> 32 - nBits);
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x0013B77C File Offset: 0x0013997C
		public static string GetComponentName(IComponent component, string defaultNameValue)
		{
			string text = string.Empty;
			if (string.IsNullOrEmpty(defaultNameValue))
			{
				if (component.Site != null)
				{
					text = component.Site.Name;
				}
				if (text == null)
				{
					text = string.Empty;
				}
			}
			else
			{
				text = defaultNameValue;
			}
			return text;
		}

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06004BF8 RID: 19448 RVA: 0x0013B7B9 File Offset: 0x001399B9
		internal static bool TargetsAtLeast_v4_5
		{
			get
			{
				return WindowsFormsUtils._targetsAtLeast_v4_5;
			}
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x0013B7C0 File Offset: 0x001399C0
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool RunningOnCheck(string propertyName)
		{
			Type type;
			try
			{
				type = typeof(object).GetTypeInfo().Assembly.GetType("System.Runtime.Versioning.BinaryCompatibility", false);
			}
			catch (TypeLoadException)
			{
				return false;
			}
			if (type == null)
			{
				return false;
			}
			PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			return !(property == null) && (bool)property.GetValue(null);
		}

		// Token: 0x0400285A RID: 10330
		public static readonly Size UninitializedSize = new Size(-7199369, -5999471);

		// Token: 0x0400285B RID: 10331
		private static bool _targetsAtLeast_v4_5 = WindowsFormsUtils.RunningOnCheck("TargetsAtLeast_Desktop_V4_5");

		// Token: 0x0400285C RID: 10332
		public static readonly ContentAlignment AnyRightAlign = (ContentAlignment)1092;

		// Token: 0x0400285D RID: 10333
		public static readonly ContentAlignment AnyLeftAlign = (ContentAlignment)273;

		// Token: 0x0400285E RID: 10334
		public static readonly ContentAlignment AnyTopAlign = (ContentAlignment)7;

		// Token: 0x0400285F RID: 10335
		public static readonly ContentAlignment AnyBottomAlign = (ContentAlignment)1792;

		// Token: 0x04002860 RID: 10336
		public static readonly ContentAlignment AnyMiddleAlign = (ContentAlignment)112;

		// Token: 0x04002861 RID: 10337
		public static readonly ContentAlignment AnyCenterAlign = (ContentAlignment)546;

		// Token: 0x0200082B RID: 2091
		public static class EnumValidator
		{
			// Token: 0x06007009 RID: 28681 RVA: 0x0019A178 File Offset: 0x00198378
			public static bool IsValidContentAlignment(ContentAlignment contentAlign)
			{
				if (ClientUtils.GetBitCount((uint)contentAlign) != 1)
				{
					return false;
				}
				int num = 1911;
				return (num & (int)contentAlign) != 0;
			}

			// Token: 0x0600700A RID: 28682 RVA: 0x0019A19C File Offset: 0x0019839C
			public static bool IsEnumWithinShiftedRange(Enum enumValue, int numBitsToShift, int minValAfterShift, int maxValAfterShift)
			{
				int num = Convert.ToInt32(enumValue, CultureInfo.InvariantCulture);
				int num2 = num >> numBitsToShift;
				return num2 << numBitsToShift == num && num2 >= minValAfterShift && num2 <= maxValAfterShift;
			}

			// Token: 0x0600700B RID: 28683 RVA: 0x0019A1D4 File Offset: 0x001983D4
			public static bool IsValidTextImageRelation(TextImageRelation relation)
			{
				return ClientUtils.IsEnumValid(relation, (int)relation, 0, 8, 1);
			}

			// Token: 0x0600700C RID: 28684 RVA: 0x0019A1E5 File Offset: 0x001983E5
			public static bool IsValidArrowDirection(ArrowDirection direction)
			{
				return direction <= ArrowDirection.Up || direction - ArrowDirection.Right <= 1;
			}
		}

		// Token: 0x0200082C RID: 2092
		public class ArraySubsetEnumerator : IEnumerator
		{
			// Token: 0x0600700D RID: 28685 RVA: 0x0019A1F5 File Offset: 0x001983F5
			public ArraySubsetEnumerator(object[] array, int count)
			{
				this.array = array;
				this.total = count;
				this.current = -1;
			}

			// Token: 0x0600700E RID: 28686 RVA: 0x0019A212 File Offset: 0x00198412
			public bool MoveNext()
			{
				if (this.current < this.total - 1)
				{
					this.current++;
					return true;
				}
				return false;
			}

			// Token: 0x0600700F RID: 28687 RVA: 0x0019A235 File Offset: 0x00198435
			public void Reset()
			{
				this.current = -1;
			}

			// Token: 0x1700187C RID: 6268
			// (get) Token: 0x06007010 RID: 28688 RVA: 0x0019A23E File Offset: 0x0019843E
			public object Current
			{
				get
				{
					if (this.current == -1)
					{
						return null;
					}
					return this.array[this.current];
				}
			}

			// Token: 0x0400434B RID: 17227
			private object[] array;

			// Token: 0x0400434C RID: 17228
			private int total;

			// Token: 0x0400434D RID: 17229
			private int current;
		}

		// Token: 0x0200082D RID: 2093
		internal class ReadOnlyControlCollection : Control.ControlCollection
		{
			// Token: 0x06007011 RID: 28689 RVA: 0x0019A258 File Offset: 0x00198458
			public ReadOnlyControlCollection(Control owner, bool isReadOnly)
				: base(owner)
			{
				this._isReadOnly = isReadOnly;
			}

			// Token: 0x06007012 RID: 28690 RVA: 0x0019A268 File Offset: 0x00198468
			public override void Add(Control value)
			{
				if (this.IsReadOnly)
				{
					throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
				}
				this.AddInternal(value);
			}

			// Token: 0x06007013 RID: 28691 RVA: 0x0019A289 File Offset: 0x00198489
			internal virtual void AddInternal(Control value)
			{
				base.Add(value);
			}

			// Token: 0x06007014 RID: 28692 RVA: 0x0019A292 File Offset: 0x00198492
			public override void Clear()
			{
				if (this.IsReadOnly)
				{
					throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
				}
				base.Clear();
			}

			// Token: 0x06007015 RID: 28693 RVA: 0x00178FBA File Offset: 0x001771BA
			internal virtual void RemoveInternal(Control value)
			{
				base.Remove(value);
			}

			// Token: 0x06007016 RID: 28694 RVA: 0x0019A2B2 File Offset: 0x001984B2
			public override void RemoveByKey(string key)
			{
				if (this.IsReadOnly)
				{
					throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
				}
				base.RemoveByKey(key);
			}

			// Token: 0x1700187D RID: 6269
			// (get) Token: 0x06007017 RID: 28695 RVA: 0x0019A2D3 File Offset: 0x001984D3
			public override bool IsReadOnly
			{
				get
				{
					return this._isReadOnly;
				}
			}

			// Token: 0x0400434E RID: 17230
			private readonly bool _isReadOnly;
		}

		// Token: 0x0200082E RID: 2094
		internal class TypedControlCollection : WindowsFormsUtils.ReadOnlyControlCollection
		{
			// Token: 0x06007018 RID: 28696 RVA: 0x0019A2DB File Offset: 0x001984DB
			public TypedControlCollection(Control owner, Type typeOfControl, bool isReadOnly)
				: base(owner, isReadOnly)
			{
				this.typeOfControl = typeOfControl;
				this.ownerControl = owner;
			}

			// Token: 0x06007019 RID: 28697 RVA: 0x0019A2F3 File Offset: 0x001984F3
			public TypedControlCollection(Control owner, Type typeOfControl)
				: base(owner, false)
			{
				this.typeOfControl = typeOfControl;
				this.ownerControl = owner;
			}

			// Token: 0x0600701A RID: 28698 RVA: 0x0019A30C File Offset: 0x0019850C
			public override void Add(Control value)
			{
				Control.CheckParentingCycle(this.ownerControl, value);
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.IsReadOnly)
				{
					throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
				}
				if (!this.typeOfControl.IsAssignableFrom(value.GetType()))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("TypedControlCollectionShouldBeOfType", new object[] { this.typeOfControl.Name }), new object[0]), value.GetType().Name);
				}
				base.Add(value);
			}

			// Token: 0x0400434F RID: 17231
			private Type typeOfControl;

			// Token: 0x04004350 RID: 17232
			private Control ownerControl;
		}

		// Token: 0x0200082F RID: 2095
		internal struct DCMapping : IDisposable
		{
			// Token: 0x0600701B RID: 28699 RVA: 0x0019A3A4 File Offset: 0x001985A4
			public DCMapping(HandleRef hDC, Rectangle bounds)
			{
				if (hDC.Handle == IntPtr.Zero)
				{
					throw new ArgumentNullException("hDC");
				}
				NativeMethods.POINT point = new NativeMethods.POINT();
				HandleRef handleRef = NativeMethods.NullHandleRef;
				this.translatedBounds = bounds;
				this.graphics = null;
				this.dc = DeviceContext.FromHdc(hDC.Handle);
				this.dc.SaveHdc();
				bool flag = SafeNativeMethods.GetViewportOrgEx(hDC, point);
				HandleRef handleRef2 = new HandleRef(null, SafeNativeMethods.CreateRectRgn(point.x + bounds.Left, point.y + bounds.Top, point.x + bounds.Right, point.y + bounds.Bottom));
				try
				{
					handleRef = new HandleRef(this, SafeNativeMethods.CreateRectRgn(0, 0, 0, 0));
					int clipRgn = SafeNativeMethods.GetClipRgn(hDC, handleRef);
					NativeMethods.POINT point2 = new NativeMethods.POINT();
					flag = SafeNativeMethods.SetViewportOrgEx(hDC, point.x + bounds.Left, point.y + bounds.Top, point2);
					if (clipRgn != 0)
					{
						NativeMethods.RECT rect = default(NativeMethods.RECT);
						NativeMethods.RegionFlags rgnBox = (NativeMethods.RegionFlags)SafeNativeMethods.GetRgnBox(handleRef, ref rect);
						if (rgnBox == NativeMethods.RegionFlags.SIMPLEREGION)
						{
							NativeMethods.RegionFlags regionFlags = (NativeMethods.RegionFlags)SafeNativeMethods.CombineRgn(handleRef2, handleRef2, handleRef, 1);
						}
					}
					else
					{
						SafeNativeMethods.DeleteObject(handleRef);
						handleRef = new HandleRef(null, IntPtr.Zero);
					}
					NativeMethods.RegionFlags regionFlags2 = (NativeMethods.RegionFlags)SafeNativeMethods.SelectClipRgn(hDC, handleRef2);
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
					this.dc.RestoreHdc();
					this.dc.Dispose();
				}
				finally
				{
					flag = SafeNativeMethods.DeleteObject(handleRef2);
					if (handleRef.Handle != IntPtr.Zero)
					{
						flag = SafeNativeMethods.DeleteObject(handleRef);
					}
				}
			}

			// Token: 0x0600701C RID: 28700 RVA: 0x0019A554 File Offset: 0x00198754
			public void Dispose()
			{
				if (this.graphics != null)
				{
					this.graphics.Dispose();
					this.graphics = null;
				}
				if (this.dc != null)
				{
					this.dc.RestoreHdc();
					this.dc.Dispose();
					this.dc = null;
				}
			}

			// Token: 0x1700187E RID: 6270
			// (get) Token: 0x0600701D RID: 28701 RVA: 0x0019A5A0 File Offset: 0x001987A0
			public Graphics Graphics
			{
				get
				{
					if (this.graphics == null)
					{
						this.graphics = Graphics.FromHdcInternal(this.dc.Hdc);
						this.graphics.SetClip(new Rectangle(Point.Empty, this.translatedBounds.Size));
					}
					return this.graphics;
				}
			}

			// Token: 0x04004351 RID: 17233
			private DeviceContext dc;

			// Token: 0x04004352 RID: 17234
			private Graphics graphics;

			// Token: 0x04004353 RID: 17235
			private Rectangle translatedBounds;
		}
	}
}
