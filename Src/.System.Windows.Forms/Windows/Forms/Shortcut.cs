using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies shortcut keys that can be used by menu items.</summary>
	// Token: 0x02000368 RID: 872
	[ComVisible(true)]
	public enum Shortcut
	{
		/// <summary>No shortcut key is associated with the menu item.</summary>
		// Token: 0x040021E1 RID: 8673
		None,
		/// <summary>The shortcut keys CTRL+A.</summary>
		// Token: 0x040021E2 RID: 8674
		CtrlA = 131137,
		/// <summary>The shortcut keys CTRL+B.</summary>
		// Token: 0x040021E3 RID: 8675
		CtrlB,
		/// <summary>The shortcut keys CTRL+C.</summary>
		// Token: 0x040021E4 RID: 8676
		CtrlC,
		/// <summary>The shortcut keys CTRL+D.</summary>
		// Token: 0x040021E5 RID: 8677
		CtrlD,
		/// <summary>The shortcut keys CTRL+E.</summary>
		// Token: 0x040021E6 RID: 8678
		CtrlE,
		/// <summary>The shortcut keys CTRL+F.</summary>
		// Token: 0x040021E7 RID: 8679
		CtrlF,
		/// <summary>The shortcut keys CTRL+G.</summary>
		// Token: 0x040021E8 RID: 8680
		CtrlG,
		/// <summary>The shortcut keys CTRL+H.</summary>
		// Token: 0x040021E9 RID: 8681
		CtrlH,
		/// <summary>The shortcut keys CTRL+I.</summary>
		// Token: 0x040021EA RID: 8682
		CtrlI,
		/// <summary>The shortcut keys CTRL+J.</summary>
		// Token: 0x040021EB RID: 8683
		CtrlJ,
		/// <summary>The shortcut keys CTRL+K.</summary>
		// Token: 0x040021EC RID: 8684
		CtrlK,
		/// <summary>The shortcut keys CTRL+L.</summary>
		// Token: 0x040021ED RID: 8685
		CtrlL,
		/// <summary>The shortcut keys CTRL+M.</summary>
		// Token: 0x040021EE RID: 8686
		CtrlM,
		/// <summary>The shortcut keys CTRL+N.</summary>
		// Token: 0x040021EF RID: 8687
		CtrlN,
		/// <summary>The shortcut keys CTRL+O.</summary>
		// Token: 0x040021F0 RID: 8688
		CtrlO,
		/// <summary>The shortcut keys CTRL+P.</summary>
		// Token: 0x040021F1 RID: 8689
		CtrlP,
		/// <summary>The shortcut keys CTRL+Q.</summary>
		// Token: 0x040021F2 RID: 8690
		CtrlQ,
		/// <summary>The shortcut keys CTRL+R.</summary>
		// Token: 0x040021F3 RID: 8691
		CtrlR,
		/// <summary>The shortcut keys CTRL+S.</summary>
		// Token: 0x040021F4 RID: 8692
		CtrlS,
		/// <summary>The shortcut keys CTRL+T.</summary>
		// Token: 0x040021F5 RID: 8693
		CtrlT,
		/// <summary>The shortcut keys CTRL+U.</summary>
		// Token: 0x040021F6 RID: 8694
		CtrlU,
		/// <summary>The shortcut keys CTRL+V.</summary>
		// Token: 0x040021F7 RID: 8695
		CtrlV,
		/// <summary>The shortcut keys CTRL+W.</summary>
		// Token: 0x040021F8 RID: 8696
		CtrlW,
		/// <summary>The shortcut keys CTRL+X.</summary>
		// Token: 0x040021F9 RID: 8697
		CtrlX,
		/// <summary>The shortcut keys CTRL+Y.</summary>
		// Token: 0x040021FA RID: 8698
		CtrlY,
		/// <summary>The shortcut keys CTRL+Z.</summary>
		// Token: 0x040021FB RID: 8699
		CtrlZ,
		/// <summary>The shortcut keys CTRL+SHIFT+A.</summary>
		// Token: 0x040021FC RID: 8700
		CtrlShiftA = 196673,
		/// <summary>The shortcut keys CTRL+SHIFT+B.</summary>
		// Token: 0x040021FD RID: 8701
		CtrlShiftB,
		/// <summary>The shortcut keys CTRL+SHIFT+C.</summary>
		// Token: 0x040021FE RID: 8702
		CtrlShiftC,
		/// <summary>The shortcut keys CTRL+SHIFT+D.</summary>
		// Token: 0x040021FF RID: 8703
		CtrlShiftD,
		/// <summary>The shortcut keys CTRL+SHIFT+E.</summary>
		// Token: 0x04002200 RID: 8704
		CtrlShiftE,
		/// <summary>The shortcut keys CTRL+SHIFT+F.</summary>
		// Token: 0x04002201 RID: 8705
		CtrlShiftF,
		/// <summary>The shortcut keys CTRL+SHIFT+G.</summary>
		// Token: 0x04002202 RID: 8706
		CtrlShiftG,
		/// <summary>The shortcut keys CTRL+SHIFT+H.</summary>
		// Token: 0x04002203 RID: 8707
		CtrlShiftH,
		/// <summary>The shortcut keys CTRL+SHIFT+I.</summary>
		// Token: 0x04002204 RID: 8708
		CtrlShiftI,
		/// <summary>The shortcut keys CTRL+SHIFT+J.</summary>
		// Token: 0x04002205 RID: 8709
		CtrlShiftJ,
		/// <summary>The shortcut keys CTRL+SHIFT+K.</summary>
		// Token: 0x04002206 RID: 8710
		CtrlShiftK,
		/// <summary>The shortcut keys CTRL+SHIFT+L.</summary>
		// Token: 0x04002207 RID: 8711
		CtrlShiftL,
		/// <summary>The shortcut keys CTRL+SHIFT+M.</summary>
		// Token: 0x04002208 RID: 8712
		CtrlShiftM,
		/// <summary>The shortcut keys CTRL+SHIFT+N.</summary>
		// Token: 0x04002209 RID: 8713
		CtrlShiftN,
		/// <summary>The shortcut keys CTRL+SHIFT+O.</summary>
		// Token: 0x0400220A RID: 8714
		CtrlShiftO,
		/// <summary>The shortcut keys CTRL+SHIFT+P.</summary>
		// Token: 0x0400220B RID: 8715
		CtrlShiftP,
		/// <summary>The shortcut keys CTRL+SHIFT+Q.</summary>
		// Token: 0x0400220C RID: 8716
		CtrlShiftQ,
		/// <summary>The shortcut keys CTRL+SHIFT+R.</summary>
		// Token: 0x0400220D RID: 8717
		CtrlShiftR,
		/// <summary>The shortcut keys CTRL+SHIFT+S.</summary>
		// Token: 0x0400220E RID: 8718
		CtrlShiftS,
		/// <summary>The shortcut keys CTRL+SHIFT+T.</summary>
		// Token: 0x0400220F RID: 8719
		CtrlShiftT,
		/// <summary>The shortcut keys CTRL+SHIFT+U.</summary>
		// Token: 0x04002210 RID: 8720
		CtrlShiftU,
		/// <summary>The shortcut keys CTRL+SHIFT+V.</summary>
		// Token: 0x04002211 RID: 8721
		CtrlShiftV,
		/// <summary>The shortcut keys CTRL+SHIFT+W.</summary>
		// Token: 0x04002212 RID: 8722
		CtrlShiftW,
		/// <summary>The shortcut keys CTRL+SHIFT+X.</summary>
		// Token: 0x04002213 RID: 8723
		CtrlShiftX,
		/// <summary>The shortcut keys CTRL+SHIFT+Y.</summary>
		// Token: 0x04002214 RID: 8724
		CtrlShiftY,
		/// <summary>The shortcut keys CTRL+SHIFT+Z.</summary>
		// Token: 0x04002215 RID: 8725
		CtrlShiftZ,
		/// <summary>The shortcut key F1.</summary>
		// Token: 0x04002216 RID: 8726
		F1 = 112,
		/// <summary>The shortcut key F2.</summary>
		// Token: 0x04002217 RID: 8727
		F2,
		/// <summary>The shortcut key F3.</summary>
		// Token: 0x04002218 RID: 8728
		F3,
		/// <summary>The shortcut key F4.</summary>
		// Token: 0x04002219 RID: 8729
		F4,
		/// <summary>The shortcut key F5.</summary>
		// Token: 0x0400221A RID: 8730
		F5,
		/// <summary>The shortcut key F6.</summary>
		// Token: 0x0400221B RID: 8731
		F6,
		/// <summary>The shortcut key F7.</summary>
		// Token: 0x0400221C RID: 8732
		F7,
		/// <summary>The shortcut key F8.</summary>
		// Token: 0x0400221D RID: 8733
		F8,
		/// <summary>The shortcut key F9.</summary>
		// Token: 0x0400221E RID: 8734
		F9,
		/// <summary>The shortcut key F10.</summary>
		// Token: 0x0400221F RID: 8735
		F10,
		/// <summary>The shortcut key F11.</summary>
		// Token: 0x04002220 RID: 8736
		F11,
		/// <summary>The shortcut key F12.</summary>
		// Token: 0x04002221 RID: 8737
		F12,
		/// <summary>The shortcut keys SHIFT+F1.</summary>
		// Token: 0x04002222 RID: 8738
		ShiftF1 = 65648,
		/// <summary>The shortcut keys SHIFT+F2.</summary>
		// Token: 0x04002223 RID: 8739
		ShiftF2,
		/// <summary>The shortcut keys SHIFT+F3.</summary>
		// Token: 0x04002224 RID: 8740
		ShiftF3,
		/// <summary>The shortcut keys SHIFT+F4.</summary>
		// Token: 0x04002225 RID: 8741
		ShiftF4,
		/// <summary>The shortcut keys SHIFT+F5.</summary>
		// Token: 0x04002226 RID: 8742
		ShiftF5,
		/// <summary>The shortcut keys SHIFT+F6.</summary>
		// Token: 0x04002227 RID: 8743
		ShiftF6,
		/// <summary>The shortcut keys SHIFT+F7.</summary>
		// Token: 0x04002228 RID: 8744
		ShiftF7,
		/// <summary>The shortcut keys SHIFT+F8.</summary>
		// Token: 0x04002229 RID: 8745
		ShiftF8,
		/// <summary>The shortcut keys SHIFT+F9.</summary>
		// Token: 0x0400222A RID: 8746
		ShiftF9,
		/// <summary>The shortcut keys SHIFT+F10.</summary>
		// Token: 0x0400222B RID: 8747
		ShiftF10,
		/// <summary>The shortcut keys SHIFT+F11.</summary>
		// Token: 0x0400222C RID: 8748
		ShiftF11,
		/// <summary>The shortcut keys SHIFT+F12.</summary>
		// Token: 0x0400222D RID: 8749
		ShiftF12,
		/// <summary>The shortcut keys CTRL+F1.</summary>
		// Token: 0x0400222E RID: 8750
		CtrlF1 = 131184,
		/// <summary>The shortcut keys CTRL+F2.</summary>
		// Token: 0x0400222F RID: 8751
		CtrlF2,
		/// <summary>The shortcut keys CTRL+F3.</summary>
		// Token: 0x04002230 RID: 8752
		CtrlF3,
		/// <summary>The shortcut keys CTRL+F4.</summary>
		// Token: 0x04002231 RID: 8753
		CtrlF4,
		/// <summary>The shortcut keys CTRL+F5.</summary>
		// Token: 0x04002232 RID: 8754
		CtrlF5,
		/// <summary>The shortcut keys CTRL+F6.</summary>
		// Token: 0x04002233 RID: 8755
		CtrlF6,
		/// <summary>The shortcut keys CTRL+F7.</summary>
		// Token: 0x04002234 RID: 8756
		CtrlF7,
		/// <summary>The shortcut keys CTRL+F8.</summary>
		// Token: 0x04002235 RID: 8757
		CtrlF8,
		/// <summary>The shortcut keys CTRL+F9.</summary>
		// Token: 0x04002236 RID: 8758
		CtrlF9,
		/// <summary>The shortcut keys CTRL+F10.</summary>
		// Token: 0x04002237 RID: 8759
		CtrlF10,
		/// <summary>The shortcut keys CTRL+F11.</summary>
		// Token: 0x04002238 RID: 8760
		CtrlF11,
		/// <summary>The shortcut keys CTRL+F12.</summary>
		// Token: 0x04002239 RID: 8761
		CtrlF12,
		/// <summary>The shortcut keys CTRL+SHIFT+F1.</summary>
		// Token: 0x0400223A RID: 8762
		CtrlShiftF1 = 196720,
		/// <summary>The shortcut keys CTRL+SHIFT+F2.</summary>
		// Token: 0x0400223B RID: 8763
		CtrlShiftF2,
		/// <summary>The shortcut keys CTRL+SHIFT+F3.</summary>
		// Token: 0x0400223C RID: 8764
		CtrlShiftF3,
		/// <summary>The shortcut keys CTRL+SHIFT+F4.</summary>
		// Token: 0x0400223D RID: 8765
		CtrlShiftF4,
		/// <summary>The shortcut keys CTRL+SHIFT+F5.</summary>
		// Token: 0x0400223E RID: 8766
		CtrlShiftF5,
		/// <summary>The shortcut keys CTRL+SHIFT+F6.</summary>
		// Token: 0x0400223F RID: 8767
		CtrlShiftF6,
		/// <summary>The shortcut keys CTRL+SHIFT+F7.</summary>
		// Token: 0x04002240 RID: 8768
		CtrlShiftF7,
		/// <summary>The shortcut keys CTRL+SHIFT+F8.</summary>
		// Token: 0x04002241 RID: 8769
		CtrlShiftF8,
		/// <summary>The shortcut keys CTRL+SHIFT+F9.</summary>
		// Token: 0x04002242 RID: 8770
		CtrlShiftF9,
		/// <summary>The shortcut keys CTRL+SHIFT+F10.</summary>
		// Token: 0x04002243 RID: 8771
		CtrlShiftF10,
		/// <summary>The shortcut keys CTRL+SHIFT+F11.</summary>
		// Token: 0x04002244 RID: 8772
		CtrlShiftF11,
		/// <summary>The shortcut keys CTRL+SHIFT+F12.</summary>
		// Token: 0x04002245 RID: 8773
		CtrlShiftF12,
		/// <summary>The shortcut key INSERT.</summary>
		// Token: 0x04002246 RID: 8774
		Ins = 45,
		/// <summary>The shortcut keys CTRL+INSERT.</summary>
		// Token: 0x04002247 RID: 8775
		CtrlIns = 131117,
		/// <summary>The shortcut keys SHIFT+INSERT.</summary>
		// Token: 0x04002248 RID: 8776
		ShiftIns = 65581,
		/// <summary>The shortcut key DELETE.</summary>
		// Token: 0x04002249 RID: 8777
		Del = 46,
		/// <summary>The shortcut keys CTRL+DELETE.</summary>
		// Token: 0x0400224A RID: 8778
		CtrlDel = 131118,
		/// <summary>The shortcut keys SHIFT+DELETE.</summary>
		// Token: 0x0400224B RID: 8779
		ShiftDel = 65582,
		/// <summary>The shortcut keys ALT+RIGHTARROW.</summary>
		// Token: 0x0400224C RID: 8780
		AltRightArrow = 262183,
		/// <summary>The shortcut keys ALT+LEFTARROW.</summary>
		// Token: 0x0400224D RID: 8781
		AltLeftArrow = 262181,
		/// <summary>The shortcut keys ALT+UPARROW.</summary>
		// Token: 0x0400224E RID: 8782
		AltUpArrow,
		/// <summary>The shortcut keys ALT+DOWNARROW.</summary>
		// Token: 0x0400224F RID: 8783
		AltDownArrow = 262184,
		/// <summary>The shortcut keys ALT+BACKSPACE.</summary>
		// Token: 0x04002250 RID: 8784
		AltBksp = 262152,
		/// <summary>The shortcut keys ALT+F1.</summary>
		// Token: 0x04002251 RID: 8785
		AltF1 = 262256,
		/// <summary>The shortcut keys ALT+F2.</summary>
		// Token: 0x04002252 RID: 8786
		AltF2,
		/// <summary>The shortcut keys ALT+F3.</summary>
		// Token: 0x04002253 RID: 8787
		AltF3,
		/// <summary>The shortcut keys ALT+F4.</summary>
		// Token: 0x04002254 RID: 8788
		AltF4,
		/// <summary>The shortcut keys ALT+F5.</summary>
		// Token: 0x04002255 RID: 8789
		AltF5,
		/// <summary>The shortcut keys ALT+F6.</summary>
		// Token: 0x04002256 RID: 8790
		AltF6,
		/// <summary>The shortcut keys ALT+F7.</summary>
		// Token: 0x04002257 RID: 8791
		AltF7,
		/// <summary>The shortcut keys ALT+F8.</summary>
		// Token: 0x04002258 RID: 8792
		AltF8,
		/// <summary>The shortcut keys ALT+F9.</summary>
		// Token: 0x04002259 RID: 8793
		AltF9,
		/// <summary>The shortcut keys ALT+F10.</summary>
		// Token: 0x0400225A RID: 8794
		AltF10,
		/// <summary>The shortcut keys ALT+F11.</summary>
		// Token: 0x0400225B RID: 8795
		AltF11,
		/// <summary>The shortcut keys ALT+F12.</summary>
		// Token: 0x0400225C RID: 8796
		AltF12,
		/// <summary>The shortcut keys ALT+0.</summary>
		// Token: 0x0400225D RID: 8797
		Alt0 = 262192,
		/// <summary>The shortcut keys ALT+1.</summary>
		// Token: 0x0400225E RID: 8798
		Alt1,
		/// <summary>The shortcut keys ALT+2.</summary>
		// Token: 0x0400225F RID: 8799
		Alt2,
		/// <summary>The shortcut keys ALT+3.</summary>
		// Token: 0x04002260 RID: 8800
		Alt3,
		/// <summary>The shortcut keys ALT+4.</summary>
		// Token: 0x04002261 RID: 8801
		Alt4,
		/// <summary>The shortcut keys ALT+5.</summary>
		// Token: 0x04002262 RID: 8802
		Alt5,
		/// <summary>The shortcut keys ALT+6.</summary>
		// Token: 0x04002263 RID: 8803
		Alt6,
		/// <summary>The shortcut keys ALT+7.</summary>
		// Token: 0x04002264 RID: 8804
		Alt7,
		/// <summary>The shortcut keys ALT+8.</summary>
		// Token: 0x04002265 RID: 8805
		Alt8,
		/// <summary>The shortcut keys ALT+9.</summary>
		// Token: 0x04002266 RID: 8806
		Alt9,
		/// <summary>The shortcut keys CTRL+0.</summary>
		// Token: 0x04002267 RID: 8807
		Ctrl0 = 131120,
		/// <summary>The shortcut keys CTRL+1.</summary>
		// Token: 0x04002268 RID: 8808
		Ctrl1,
		/// <summary>The shortcut keys CTRL+2.</summary>
		// Token: 0x04002269 RID: 8809
		Ctrl2,
		/// <summary>The shortcut keys CTRL+3.</summary>
		// Token: 0x0400226A RID: 8810
		Ctrl3,
		/// <summary>The shortcut keys CTRL+4.</summary>
		// Token: 0x0400226B RID: 8811
		Ctrl4,
		/// <summary>The shortcut keys CTRL+5.</summary>
		// Token: 0x0400226C RID: 8812
		Ctrl5,
		/// <summary>The shortcut keys CTRL+6.</summary>
		// Token: 0x0400226D RID: 8813
		Ctrl6,
		/// <summary>The shortcut keys CTRL+7.</summary>
		// Token: 0x0400226E RID: 8814
		Ctrl7,
		/// <summary>The shortcut keys CTRL+8.</summary>
		// Token: 0x0400226F RID: 8815
		Ctrl8,
		/// <summary>The shortcut keys CTRL+9.</summary>
		// Token: 0x04002270 RID: 8816
		Ctrl9,
		/// <summary>The shortcut keys CTRL+SHIFT+0.</summary>
		// Token: 0x04002271 RID: 8817
		CtrlShift0 = 196656,
		/// <summary>The shortcut keys CTRL+SHIFT+1.</summary>
		// Token: 0x04002272 RID: 8818
		CtrlShift1,
		/// <summary>The shortcut keys CTRL+SHIFT+2.</summary>
		// Token: 0x04002273 RID: 8819
		CtrlShift2,
		/// <summary>The shortcut keys CTRL+SHIFT+3.</summary>
		// Token: 0x04002274 RID: 8820
		CtrlShift3,
		/// <summary>The shortcut keys CTRL+SHIFT+4.</summary>
		// Token: 0x04002275 RID: 8821
		CtrlShift4,
		/// <summary>The shortcut keys CTRL+SHIFT+5.</summary>
		// Token: 0x04002276 RID: 8822
		CtrlShift5,
		/// <summary>The shortcut keys CTRL+SHIFT+6.</summary>
		// Token: 0x04002277 RID: 8823
		CtrlShift6,
		/// <summary>The shortcut keys CTRL+SHIFT+7.</summary>
		// Token: 0x04002278 RID: 8824
		CtrlShift7,
		/// <summary>The shortcut keys CTRL+SHIFT+8.</summary>
		// Token: 0x04002279 RID: 8825
		CtrlShift8,
		/// <summary>The shortcut keys CTRL+SHIFT+9.</summary>
		// Token: 0x0400227A RID: 8826
		CtrlShift9
	}
}
