using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000691 RID: 1681
	internal abstract class RegexCompiler
	{
		// Token: 0x06003E2A RID: 15914 RVA: 0x001001B8 File Offset: 0x000FE3B8
		static RegexCompiler()
		{
			new ReflectionPermission(PermissionState.Unrestricted).Assert();
			try
			{
				RegexCompiler._textbegF = RegexCompiler.RegexRunnerField("runtextbeg");
				RegexCompiler._textendF = RegexCompiler.RegexRunnerField("runtextend");
				RegexCompiler._textstartF = RegexCompiler.RegexRunnerField("runtextstart");
				RegexCompiler._textposF = RegexCompiler.RegexRunnerField("runtextpos");
				RegexCompiler._textF = RegexCompiler.RegexRunnerField("runtext");
				RegexCompiler._trackposF = RegexCompiler.RegexRunnerField("runtrackpos");
				RegexCompiler._trackF = RegexCompiler.RegexRunnerField("runtrack");
				RegexCompiler._stackposF = RegexCompiler.RegexRunnerField("runstackpos");
				RegexCompiler._stackF = RegexCompiler.RegexRunnerField("runstack");
				RegexCompiler._trackcountF = RegexCompiler.RegexRunnerField("runtrackcount");
				RegexCompiler._ensurestorageM = RegexCompiler.RegexRunnerMethod("EnsureStorage");
				RegexCompiler._captureM = RegexCompiler.RegexRunnerMethod("Capture");
				RegexCompiler._transferM = RegexCompiler.RegexRunnerMethod("TransferCapture");
				RegexCompiler._uncaptureM = RegexCompiler.RegexRunnerMethod("Uncapture");
				RegexCompiler._ismatchedM = RegexCompiler.RegexRunnerMethod("IsMatched");
				RegexCompiler._matchlengthM = RegexCompiler.RegexRunnerMethod("MatchLength");
				RegexCompiler._matchindexM = RegexCompiler.RegexRunnerMethod("MatchIndex");
				RegexCompiler._isboundaryM = RegexCompiler.RegexRunnerMethod("IsBoundary");
				RegexCompiler._charInSetM = RegexCompiler.RegexRunnerMethod("CharInClass");
				RegexCompiler._isECMABoundaryM = RegexCompiler.RegexRunnerMethod("IsECMABoundary");
				RegexCompiler._crawlposM = RegexCompiler.RegexRunnerMethod("Crawlpos");
				RegexCompiler._checkTimeoutM = RegexCompiler.RegexRunnerMethod("CheckTimeout");
				RegexCompiler._chartolowerM = typeof(char).GetMethod("ToLower", new Type[]
				{
					typeof(char),
					typeof(CultureInfo)
				});
				RegexCompiler._getcharM = typeof(string).GetMethod("get_Chars", new Type[] { typeof(int) });
				RegexCompiler._getCurrentCulture = typeof(CultureInfo).GetMethod("get_CurrentCulture");
				RegexCompiler._getInvariantCulture = typeof(CultureInfo).GetMethod("get_InvariantCulture");
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x001003E0 File Offset: 0x000FE5E0
		private static FieldInfo RegexRunnerField(string fieldname)
		{
			return typeof(RegexRunner).GetField(fieldname, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x001003F4 File Offset: 0x000FE5F4
		private static MethodInfo RegexRunnerMethod(string methname)
		{
			return typeof(RegexRunner).GetMethod(methname, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x00100408 File Offset: 0x000FE608
		internal static RegexRunnerFactory Compile(RegexCode code, RegexOptions options)
		{
			RegexLWCGCompiler regexLWCGCompiler = new RegexLWCGCompiler();
			new ReflectionPermission(PermissionState.Unrestricted).Assert();
			RegexRunnerFactory regexRunnerFactory;
			try
			{
				regexRunnerFactory = regexLWCGCompiler.FactoryInstanceFromCode(code, options);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return regexRunnerFactory;
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x00100448 File Offset: 0x000FE648
		internal static void CompileToAssembly(RegexCompilationInfo[] regexes, AssemblyName an, CustomAttributeBuilder[] attribs, string resourceFile)
		{
			RegexTypeCompiler regexTypeCompiler = new RegexTypeCompiler(an, attribs, resourceFile);
			for (int i = 0; i < regexes.Length; i++)
			{
				if (regexes[i] == null)
				{
					throw new ArgumentNullException("regexes", SR.GetString("ArgumentNull_ArrayWithNullElements"));
				}
				string pattern = regexes[i].Pattern;
				RegexOptions options = regexes[i].Options;
				string text;
				if (regexes[i].Namespace.Length == 0)
				{
					text = regexes[i].Name;
				}
				else
				{
					text = regexes[i].Namespace + "." + regexes[i].Name;
				}
				TimeSpan matchTimeout = regexes[i].MatchTimeout;
				RegexTree regexTree = RegexParser.Parse(pattern, options);
				RegexCode regexCode = RegexWriter.Write(regexTree);
				new ReflectionPermission(PermissionState.Unrestricted).Assert();
				try
				{
					Type type = regexTypeCompiler.FactoryTypeFromCode(regexCode, options, text);
					regexTypeCompiler.GenerateRegexType(pattern, options, text, regexes[i].IsPublic, regexCode, regexTree, type, matchTimeout);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			regexTypeCompiler.Save();
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x00100544 File Offset: 0x000FE744
		internal int AddBacktrackNote(int flags, Label l, int codepos)
		{
			if (this._notes == null || this._notecount >= this._notes.Length)
			{
				RegexCompiler.BacktrackNote[] array = new RegexCompiler.BacktrackNote[(this._notes == null) ? 16 : (this._notes.Length * 2)];
				if (this._notes != null)
				{
					Array.Copy(this._notes, 0, array, 0, this._notecount);
				}
				this._notes = array;
			}
			this._notes[this._notecount] = new RegexCompiler.BacktrackNote(flags, l, codepos);
			int notecount = this._notecount;
			this._notecount = notecount + 1;
			return notecount;
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x001005CE File Offset: 0x000FE7CE
		internal int AddTrack()
		{
			return this.AddTrack(128);
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x001005DB File Offset: 0x000FE7DB
		internal int AddTrack(int flags)
		{
			return this.AddBacktrackNote(flags, this.DefineLabel(), this._codepos);
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x001005F0 File Offset: 0x000FE7F0
		internal int AddGoto(int destpos)
		{
			if (this._goto[destpos] == -1)
			{
				this._goto[destpos] = this.AddBacktrackNote(0, this._labels[destpos], destpos);
			}
			return this._goto[destpos];
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x00100621 File Offset: 0x000FE821
		internal int AddUniqueTrack(int i)
		{
			return this.AddUniqueTrack(i, 128);
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x0010062F File Offset: 0x000FE82F
		internal int AddUniqueTrack(int i, int flags)
		{
			if (this._uniquenote[i] == -1)
			{
				this._uniquenote[i] = this.AddTrack(flags);
			}
			return this._uniquenote[i];
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x00100653 File Offset: 0x000FE853
		internal Label DefineLabel()
		{
			return this._ilg.DefineLabel();
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x00100660 File Offset: 0x000FE860
		internal void MarkLabel(Label l)
		{
			this._ilg.MarkLabel(l);
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x0010066E File Offset: 0x000FE86E
		internal int Operand(int i)
		{
			return this._codes[this._codepos + i + 1];
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x00100681 File Offset: 0x000FE881
		internal bool IsRtl()
		{
			return (this._regexopcode & 64) != 0;
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x0010068F File Offset: 0x000FE88F
		internal bool IsCi()
		{
			return (this._regexopcode & 512) != 0;
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x001006A0 File Offset: 0x000FE8A0
		internal int Code()
		{
			return this._regexopcode & 63;
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x001006AB File Offset: 0x000FE8AB
		internal void Ldstr(string str)
		{
			this._ilg.Emit(OpCodes.Ldstr, str);
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x001006BE File Offset: 0x000FE8BE
		internal void Ldc(int i)
		{
			if (i <= 127 && i >= -128)
			{
				this._ilg.Emit(OpCodes.Ldc_I4_S, (byte)i);
				return;
			}
			this._ilg.Emit(OpCodes.Ldc_I4, i);
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x001006EE File Offset: 0x000FE8EE
		internal void LdcI8(long i)
		{
			if (i <= 2147483647L && i >= -2147483648L)
			{
				this.Ldc((int)i);
				this._ilg.Emit(OpCodes.Conv_I8);
				return;
			}
			this._ilg.Emit(OpCodes.Ldc_I8, i);
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x0010072C File Offset: 0x000FE92C
		internal void Dup()
		{
			this._ilg.Emit(OpCodes.Dup);
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x0010073E File Offset: 0x000FE93E
		internal void Ret()
		{
			this._ilg.Emit(OpCodes.Ret);
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x00100750 File Offset: 0x000FE950
		private void Rem()
		{
			this._ilg.Emit(OpCodes.Rem);
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x00100762 File Offset: 0x000FE962
		private void Ceq()
		{
			this._ilg.Emit(OpCodes.Ceq);
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x00100774 File Offset: 0x000FE974
		internal void Pop()
		{
			this._ilg.Emit(OpCodes.Pop);
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x00100786 File Offset: 0x000FE986
		internal void Add()
		{
			this._ilg.Emit(OpCodes.Add);
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x00100798 File Offset: 0x000FE998
		internal void Add(bool negate)
		{
			if (negate)
			{
				this._ilg.Emit(OpCodes.Sub);
				return;
			}
			this._ilg.Emit(OpCodes.Add);
		}

		// Token: 0x06003E45 RID: 15941 RVA: 0x001007BE File Offset: 0x000FE9BE
		internal void Sub()
		{
			this._ilg.Emit(OpCodes.Sub);
		}

		// Token: 0x06003E46 RID: 15942 RVA: 0x001007D0 File Offset: 0x000FE9D0
		internal void Sub(bool negate)
		{
			if (negate)
			{
				this._ilg.Emit(OpCodes.Add);
				return;
			}
			this._ilg.Emit(OpCodes.Sub);
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x001007F6 File Offset: 0x000FE9F6
		internal void Ldloc(LocalBuilder lt)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, lt);
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x00100809 File Offset: 0x000FEA09
		internal void Stloc(LocalBuilder lt)
		{
			this._ilg.Emit(OpCodes.Stloc_S, lt);
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x0010081C File Offset: 0x000FEA1C
		internal void Ldthis()
		{
			this._ilg.Emit(OpCodes.Ldarg_0);
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x0010082E File Offset: 0x000FEA2E
		internal void Ldthisfld(FieldInfo ft)
		{
			this.Ldthis();
			this._ilg.Emit(OpCodes.Ldfld, ft);
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x00100847 File Offset: 0x000FEA47
		internal void Mvfldloc(FieldInfo ft, LocalBuilder lt)
		{
			this.Ldthisfld(ft);
			this.Stloc(lt);
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x00100857 File Offset: 0x000FEA57
		internal void Mvlocfld(LocalBuilder lt, FieldInfo ft)
		{
			this.Ldthis();
			this.Ldloc(lt);
			this.Stfld(ft);
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x0010086D File Offset: 0x000FEA6D
		internal void Stfld(FieldInfo ft)
		{
			this._ilg.Emit(OpCodes.Stfld, ft);
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x00100880 File Offset: 0x000FEA80
		internal void Callvirt(MethodInfo mt)
		{
			this._ilg.Emit(OpCodes.Callvirt, mt);
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x00100893 File Offset: 0x000FEA93
		internal void Call(MethodInfo mt)
		{
			this._ilg.Emit(OpCodes.Call, mt);
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x001008A6 File Offset: 0x000FEAA6
		internal void Newobj(ConstructorInfo ct)
		{
			this._ilg.Emit(OpCodes.Newobj, ct);
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x001008B9 File Offset: 0x000FEAB9
		internal void BrfalseFar(Label l)
		{
			this._ilg.Emit(OpCodes.Brfalse, l);
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x001008CC File Offset: 0x000FEACC
		internal void BrtrueFar(Label l)
		{
			this._ilg.Emit(OpCodes.Brtrue, l);
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x001008DF File Offset: 0x000FEADF
		internal void BrFar(Label l)
		{
			this._ilg.Emit(OpCodes.Br, l);
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x001008F2 File Offset: 0x000FEAF2
		internal void BleFar(Label l)
		{
			this._ilg.Emit(OpCodes.Ble, l);
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x00100905 File Offset: 0x000FEB05
		internal void BltFar(Label l)
		{
			this._ilg.Emit(OpCodes.Blt, l);
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x00100918 File Offset: 0x000FEB18
		internal void BgeFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bge, l);
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x0010092B File Offset: 0x000FEB2B
		internal void BgtFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt, l);
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x0010093E File Offset: 0x000FEB3E
		internal void BneFar(Label l)
		{
			this._ilg.Emit(OpCodes.Bne_Un, l);
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x00100951 File Offset: 0x000FEB51
		internal void BeqFar(Label l)
		{
			this._ilg.Emit(OpCodes.Beq, l);
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x00100964 File Offset: 0x000FEB64
		internal void Brfalse(Label l)
		{
			this._ilg.Emit(OpCodes.Brfalse_S, l);
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x00100977 File Offset: 0x000FEB77
		internal void Br(Label l)
		{
			this._ilg.Emit(OpCodes.Br_S, l);
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x0010098A File Offset: 0x000FEB8A
		internal void Ble(Label l)
		{
			this._ilg.Emit(OpCodes.Ble_S, l);
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x0010099D File Offset: 0x000FEB9D
		internal void Blt(Label l)
		{
			this._ilg.Emit(OpCodes.Blt_S, l);
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x001009B0 File Offset: 0x000FEBB0
		internal void Bge(Label l)
		{
			this._ilg.Emit(OpCodes.Bge_S, l);
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x001009C3 File Offset: 0x000FEBC3
		internal void Bgt(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt_S, l);
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x001009D6 File Offset: 0x000FEBD6
		internal void Bgtun(Label l)
		{
			this._ilg.Emit(OpCodes.Bgt_Un_S, l);
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x001009E9 File Offset: 0x000FEBE9
		internal void Bne(Label l)
		{
			this._ilg.Emit(OpCodes.Bne_Un_S, l);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x001009FC File Offset: 0x000FEBFC
		internal void Beq(Label l)
		{
			this._ilg.Emit(OpCodes.Beq_S, l);
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x00100A0F File Offset: 0x000FEC0F
		internal void Ldlen()
		{
			this._ilg.Emit(OpCodes.Ldlen);
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x00100A21 File Offset: 0x000FEC21
		internal void Rightchar()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Callvirt(RegexCompiler._getcharM);
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x00100A48 File Offset: 0x000FEC48
		internal void Rightcharnext()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Dup();
			this.Ldc(1);
			this.Add();
			this.Stloc(this._textposV);
			this.Callvirt(RegexCompiler._getcharM);
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x00100A97 File Offset: 0x000FEC97
		internal void Leftchar()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub();
			this.Callvirt(RegexCompiler._getcharM);
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x00100ACC File Offset: 0x000FECCC
		internal void Leftcharnext()
		{
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub();
			this.Dup();
			this.Stloc(this._textposV);
			this.Callvirt(RegexCompiler._getcharM);
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x00100B1B File Offset: 0x000FED1B
		internal void Track()
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddTrack());
			this.DoPush();
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x00100B35 File Offset: 0x000FED35
		internal void Trackagain()
		{
			this.ReadyPushTrack();
			this.Ldc(this._backpos);
			this.DoPush();
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x00100B4F File Offset: 0x000FED4F
		internal void PushTrack(LocalBuilder lt)
		{
			this.ReadyPushTrack();
			this.Ldloc(lt);
			this.DoPush();
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x00100B64 File Offset: 0x000FED64
		internal void TrackUnique(int i)
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddUniqueTrack(i));
			this.DoPush();
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x00100B7F File Offset: 0x000FED7F
		internal void TrackUnique2(int i)
		{
			this.ReadyPushTrack();
			this.Ldc(this.AddUniqueTrack(i, 256));
			this.DoPush();
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x00100BA0 File Offset: 0x000FEDA0
		internal void ReadyPushTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Sub);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Stloc_S, this._trackposV);
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x00100C20 File Offset: 0x000FEE20
		internal void PopTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x00100CAF File Offset: 0x000FEEAF
		internal void TopTrack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._trackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x00100CED File Offset: 0x000FEEED
		internal void PushStack(LocalBuilder lt)
		{
			this.ReadyPushStack();
			this._ilg.Emit(OpCodes.Ldloc_S, lt);
			this.DoPush();
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x00100D0C File Offset: 0x000FEF0C
		internal void ReadyReplaceStack(int i)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			if (i != 0)
			{
				this.Ldc(i);
				this._ilg.Emit(OpCodes.Add);
			}
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x00100D60 File Offset: 0x000FEF60
		internal void ReadyPushStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Sub);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x00100DDF File Offset: 0x000FEFDF
		internal void TopStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x00100E20 File Offset: 0x000FF020
		internal void PopStack()
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackV);
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Dup);
			this._ilg.Emit(OpCodes.Ldc_I4_1);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
			this._ilg.Emit(OpCodes.Ldelem_I4);
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x00100EAF File Offset: 0x000FF0AF
		internal void PopDiscardStack()
		{
			this.PopDiscardStack(1);
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x00100EB8 File Offset: 0x000FF0B8
		internal void PopDiscardStack(int i)
		{
			this._ilg.Emit(OpCodes.Ldloc_S, this._stackposV);
			this.Ldc(i);
			this._ilg.Emit(OpCodes.Add);
			this._ilg.Emit(OpCodes.Stloc_S, this._stackposV);
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x00100F08 File Offset: 0x000FF108
		internal void DoReplace()
		{
			this._ilg.Emit(OpCodes.Stelem_I4);
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x00100F1A File Offset: 0x000FF11A
		internal void DoPush()
		{
			this._ilg.Emit(OpCodes.Stelem_I4);
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x00100F2C File Offset: 0x000FF12C
		internal void Back()
		{
			this._ilg.Emit(OpCodes.Br, this._backtrack);
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x00100F44 File Offset: 0x000FF144
		internal void Goto(int i)
		{
			if (i < this._codepos)
			{
				Label label = this.DefineLabel();
				this.Ldloc(this._trackposV);
				this.Ldc(this._trackcount * 4);
				this.Ble(label);
				this.Ldloc(this._stackposV);
				this.Ldc(this._trackcount * 3);
				this.BgtFar(this._labels[i]);
				this.MarkLabel(label);
				this.ReadyPushTrack();
				this.Ldc(this.AddGoto(i));
				this.DoPush();
				this.BrFar(this._backtrack);
				return;
			}
			this.BrFar(this._labels[i]);
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x00100FF0 File Offset: 0x000FF1F0
		internal int NextCodepos()
		{
			return this._codepos + RegexCode.OpcodeSize(this._codes[this._codepos]);
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x0010100B File Offset: 0x000FF20B
		internal Label AdvanceLabel()
		{
			return this._labels[this.NextCodepos()];
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x0010101E File Offset: 0x000FF21E
		internal void Advance()
		{
			this._ilg.Emit(OpCodes.Br, this.AdvanceLabel());
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x00101036 File Offset: 0x000FF236
		internal void CallToLower()
		{
			if ((this._options & RegexOptions.CultureInvariant) != RegexOptions.None)
			{
				this.Call(RegexCompiler._getInvariantCulture);
			}
			else
			{
				this.Call(RegexCompiler._getCurrentCulture);
			}
			this.Call(RegexCompiler._chartolowerM);
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x0010106C File Offset: 0x000FF26C
		internal void GenerateForwardSection()
		{
			this._labels = new Label[this._codes.Length];
			this._goto = new int[this._codes.Length];
			for (int i = 0; i < this._codes.Length; i += RegexCode.OpcodeSize(this._codes[i]))
			{
				this._goto[i] = -1;
				this._labels[i] = this._ilg.DefineLabel();
			}
			this._uniquenote = new int[10];
			for (int j = 0; j < 10; j++)
			{
				this._uniquenote[j] = -1;
			}
			this.Mvfldloc(RegexCompiler._textF, this._textV);
			this.Mvfldloc(RegexCompiler._textstartF, this._textstartV);
			this.Mvfldloc(RegexCompiler._textbegF, this._textbegV);
			this.Mvfldloc(RegexCompiler._textendF, this._textendV);
			this.Mvfldloc(RegexCompiler._textposF, this._textposV);
			this.Mvfldloc(RegexCompiler._trackF, this._trackV);
			this.Mvfldloc(RegexCompiler._trackposF, this._trackposV);
			this.Mvfldloc(RegexCompiler._stackF, this._stackV);
			this.Mvfldloc(RegexCompiler._stackposF, this._stackposV);
			this._backpos = -1;
			for (int i = 0; i < this._codes.Length; i += RegexCode.OpcodeSize(this._codes[i]))
			{
				this.MarkLabel(this._labels[i]);
				this._codepos = i;
				this._regexopcode = this._codes[i];
				this.GenerateOneCode();
			}
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x001011F0 File Offset: 0x000FF3F0
		internal void GenerateMiddleSection()
		{
			Label label = this.DefineLabel();
			this.MarkLabel(this._backtrack);
			this.Mvlocfld(this._trackposV, RegexCompiler._trackposF);
			this.Mvlocfld(this._stackposV, RegexCompiler._stackposF);
			this.Ldthis();
			this.Callvirt(RegexCompiler._ensurestorageM);
			this.Mvfldloc(RegexCompiler._trackposF, this._trackposV);
			this.Mvfldloc(RegexCompiler._stackposF, this._stackposV);
			this.Mvfldloc(RegexCompiler._trackF, this._trackV);
			this.Mvfldloc(RegexCompiler._stackF, this._stackV);
			this.PopTrack();
			Label[] array = new Label[this._notecount];
			for (int i = 0; i < this._notecount; i++)
			{
				array[i] = this._notes[i]._label;
			}
			this._ilg.Emit(OpCodes.Switch, array);
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x001012D0 File Offset: 0x000FF4D0
		internal void GenerateBacktrackSection()
		{
			for (int i = 0; i < this._notecount; i++)
			{
				RegexCompiler.BacktrackNote backtrackNote = this._notes[i];
				if (backtrackNote._flags != 0)
				{
					this._ilg.MarkLabel(backtrackNote._label);
					this._codepos = backtrackNote._codepos;
					this._backpos = i;
					this._regexopcode = this._codes[backtrackNote._codepos] | backtrackNote._flags;
					this.GenerateOneCode();
				}
			}
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x00101344 File Offset: 0x000FF544
		internal void GenerateFindFirstChar()
		{
			this._textposV = this.DeclareInt();
			this._textV = this.DeclareString();
			this._tempV = this.DeclareInt();
			this._temp2V = this.DeclareInt();
			if ((this._anchors & 53) != 0)
			{
				if (!this._code._rightToLeft)
				{
					if ((this._anchors & 1) != 0)
					{
						Label label = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Ble(label);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textendF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label);
					}
					if ((this._anchors & 4) != 0)
					{
						Label label2 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textstartF);
						this.Ble(label2);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textendF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label2);
					}
					if ((this._anchors & 16) != 0)
					{
						Label label3 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Ldc(1);
						this.Sub();
						this.Bge(label3);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textendF);
						this.Ldc(1);
						this.Sub();
						this.Stfld(RegexCompiler._textposF);
						this.MarkLabel(label3);
					}
					if ((this._anchors & 32) != 0)
					{
						Label label4 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Bge(label4);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textendF);
						this.Stfld(RegexCompiler._textposF);
						this.MarkLabel(label4);
					}
				}
				else
				{
					if ((this._anchors & 32) != 0)
					{
						Label label5 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Bge(label5);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label5);
					}
					if ((this._anchors & 16) != 0)
					{
						Label label6 = this.DefineLabel();
						Label label7 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Ldc(1);
						this.Sub();
						this.Blt(label6);
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textendF);
						this.Beq(label7);
						this.Ldthisfld(RegexCompiler._textF);
						this.Ldthisfld(RegexCompiler._textposF);
						this.Callvirt(RegexCompiler._getcharM);
						this.Ldc(10);
						this.Beq(label7);
						this.MarkLabel(label6);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label7);
					}
					if ((this._anchors & 4) != 0)
					{
						Label label8 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textstartF);
						this.Bge(label8);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Stfld(RegexCompiler._textposF);
						this.Ldc(0);
						this.Ret();
						this.MarkLabel(label8);
					}
					if ((this._anchors & 1) != 0)
					{
						Label label9 = this.DefineLabel();
						this.Ldthisfld(RegexCompiler._textposF);
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Ble(label9);
						this.Ldthis();
						this.Ldthisfld(RegexCompiler._textbegF);
						this.Stfld(RegexCompiler._textposF);
						this.MarkLabel(label9);
					}
				}
				this.Ldc(1);
				this.Ret();
				return;
			}
			if (this._bmPrefix != null && this._bmPrefix._negativeUnicode == null)
			{
				LocalBuilder tempV = this._tempV;
				LocalBuilder tempV2 = this._tempV;
				LocalBuilder temp2V = this._temp2V;
				Label label10 = this.DefineLabel();
				Label label11 = this.DefineLabel();
				Label label12 = this.DefineLabel();
				Label label13 = this.DefineLabel();
				Label label14 = this.DefineLabel();
				Label label15 = this.DefineLabel();
				int num;
				int num2;
				if (!this._code._rightToLeft)
				{
					num = -1;
					num2 = this._bmPrefix._pattern.Length - 1;
				}
				else
				{
					num = this._bmPrefix._pattern.Length;
					num2 = 0;
				}
				int num3 = (int)this._bmPrefix._pattern[num2];
				this.Mvfldloc(RegexCompiler._textF, this._textV);
				if (!this._code._rightToLeft)
				{
					this.Ldthisfld(RegexCompiler._textendF);
				}
				else
				{
					this.Ldthisfld(RegexCompiler._textbegF);
				}
				this.Stloc(temp2V);
				this.Ldthisfld(RegexCompiler._textposF);
				if (!this._code._rightToLeft)
				{
					this.Ldc(this._bmPrefix._pattern.Length - 1);
					this.Add();
				}
				else
				{
					this.Ldc(this._bmPrefix._pattern.Length);
					this.Sub();
				}
				this.Stloc(this._textposV);
				this.Br(label13);
				this.MarkLabel(label10);
				if (!this._code._rightToLeft)
				{
					this.Ldc(this._bmPrefix._pattern.Length);
				}
				else
				{
					this.Ldc(-this._bmPrefix._pattern.Length);
				}
				this.MarkLabel(label11);
				this.Ldloc(this._textposV);
				this.Add();
				this.Stloc(this._textposV);
				this.MarkLabel(label13);
				this.Ldloc(this._textposV);
				this.Ldloc(temp2V);
				if (!this._code._rightToLeft)
				{
					this.BgeFar(label12);
				}
				else
				{
					this.BltFar(label12);
				}
				this.Rightchar();
				if (this._bmPrefix._caseInsensitive)
				{
					this.CallToLower();
				}
				this.Dup();
				this.Stloc(tempV);
				this.Ldc(num3);
				this.BeqFar(label15);
				this.Ldloc(tempV);
				this.Ldc(this._bmPrefix._lowASCII);
				this.Sub();
				this.Dup();
				this.Stloc(tempV);
				this.Ldc(this._bmPrefix._highASCII - this._bmPrefix._lowASCII);
				this.Bgtun(label10);
				Label[] array = new Label[this._bmPrefix._highASCII - this._bmPrefix._lowASCII + 1];
				for (int i = this._bmPrefix._lowASCII; i <= this._bmPrefix._highASCII; i++)
				{
					if (this._bmPrefix._negativeASCII[i] == num)
					{
						array[i - this._bmPrefix._lowASCII] = label10;
					}
					else
					{
						array[i - this._bmPrefix._lowASCII] = this.DefineLabel();
					}
				}
				this.Ldloc(tempV);
				this._ilg.Emit(OpCodes.Switch, array);
				for (int i = this._bmPrefix._lowASCII; i <= this._bmPrefix._highASCII; i++)
				{
					if (this._bmPrefix._negativeASCII[i] != num)
					{
						this.MarkLabel(array[i - this._bmPrefix._lowASCII]);
						this.Ldc(this._bmPrefix._negativeASCII[i]);
						this.BrFar(label11);
					}
				}
				this.MarkLabel(label15);
				this.Ldloc(this._textposV);
				this.Stloc(tempV2);
				for (int i = this._bmPrefix._pattern.Length - 2; i >= 0; i--)
				{
					Label label16 = this.DefineLabel();
					int num4;
					if (!this._code._rightToLeft)
					{
						num4 = i;
					}
					else
					{
						num4 = this._bmPrefix._pattern.Length - 1 - i;
					}
					this.Ldloc(this._textV);
					this.Ldloc(tempV2);
					this.Ldc(1);
					this.Sub(this._code._rightToLeft);
					this.Dup();
					this.Stloc(tempV2);
					this.Callvirt(RegexCompiler._getcharM);
					if (this._bmPrefix._caseInsensitive)
					{
						this.CallToLower();
					}
					this.Ldc((int)this._bmPrefix._pattern[num4]);
					this.Beq(label16);
					this.Ldc(this._bmPrefix._positive[num4]);
					this.BrFar(label11);
					this.MarkLabel(label16);
				}
				this.Ldthis();
				this.Ldloc(tempV2);
				if (this._code._rightToLeft)
				{
					this.Ldc(1);
					this.Add();
				}
				this.Stfld(RegexCompiler._textposF);
				this.Ldc(1);
				this.Ret();
				this.MarkLabel(label12);
				this.Ldthis();
				if (!this._code._rightToLeft)
				{
					this.Ldthisfld(RegexCompiler._textendF);
				}
				else
				{
					this.Ldthisfld(RegexCompiler._textbegF);
				}
				this.Stfld(RegexCompiler._textposF);
				this.Ldc(0);
				this.Ret();
				return;
			}
			if (this._fcPrefix == null)
			{
				this.Ldc(1);
				this.Ret();
				return;
			}
			LocalBuilder temp2V2 = this._temp2V;
			LocalBuilder tempV3 = this._tempV;
			Label label17 = this.DefineLabel();
			Label label18 = this.DefineLabel();
			Label label19 = this.DefineLabel();
			Label label20 = this.DefineLabel();
			Label label21 = this.DefineLabel();
			this.Mvfldloc(RegexCompiler._textposF, this._textposV);
			this.Mvfldloc(RegexCompiler._textF, this._textV);
			if (!this._code._rightToLeft)
			{
				this.Ldthisfld(RegexCompiler._textendF);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldthisfld(RegexCompiler._textbegF);
			}
			this.Sub();
			this.Stloc(temp2V2);
			this.Ldloc(temp2V2);
			this.Ldc(0);
			this.BleFar(label20);
			this.MarkLabel(label17);
			this.Ldloc(temp2V2);
			this.Ldc(1);
			this.Sub();
			this.Stloc(temp2V2);
			if (this._code._rightToLeft)
			{
				this.Leftcharnext();
			}
			else
			{
				this.Rightcharnext();
			}
			if (this._fcPrefix.CaseInsensitive)
			{
				this.CallToLower();
			}
			if (!RegexCharClass.IsSingleton(this._fcPrefix.Prefix))
			{
				this.Ldstr(this._fcPrefix.Prefix);
				this.Call(RegexCompiler._charInSetM);
				this.BrtrueFar(label18);
			}
			else
			{
				this.Ldc((int)RegexCharClass.SingletonChar(this._fcPrefix.Prefix));
				this.Beq(label18);
			}
			this.MarkLabel(label21);
			this.Ldloc(temp2V2);
			this.Ldc(0);
			if (!RegexCharClass.IsSingleton(this._fcPrefix.Prefix))
			{
				this.BgtFar(label17);
			}
			else
			{
				this.Bgt(label17);
			}
			this.Ldc(0);
			this.BrFar(label19);
			this.MarkLabel(label18);
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub(this._code._rightToLeft);
			this.Stloc(this._textposV);
			this.Ldc(1);
			this.MarkLabel(label19);
			this.Mvlocfld(this._textposV, RegexCompiler._textposF);
			this.Ret();
			this.MarkLabel(label20);
			this.Ldc(0);
			this.Ret();
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x00101E83 File Offset: 0x00100083
		internal void GenerateInitTrackCount()
		{
			this.Ldthis();
			this.Ldc(this._trackcount);
			this.Stfld(RegexCompiler._trackcountF);
			this.Ret();
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x00101EA8 File Offset: 0x001000A8
		internal LocalBuilder DeclareInt()
		{
			return this._ilg.DeclareLocal(typeof(int));
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x00101EBF File Offset: 0x001000BF
		internal LocalBuilder DeclareIntArray()
		{
			return this._ilg.DeclareLocal(typeof(int[]));
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x00101ED6 File Offset: 0x001000D6
		internal LocalBuilder DeclareString()
		{
			return this._ilg.DeclareLocal(typeof(string));
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x00101EF0 File Offset: 0x001000F0
		internal void GenerateGo()
		{
			this._textposV = this.DeclareInt();
			this._textV = this.DeclareString();
			this._trackposV = this.DeclareInt();
			this._trackV = this.DeclareIntArray();
			this._stackposV = this.DeclareInt();
			this._stackV = this.DeclareIntArray();
			this._tempV = this.DeclareInt();
			this._temp2V = this.DeclareInt();
			this._temp3V = this.DeclareInt();
			this._textbegV = this.DeclareInt();
			this._textendV = this.DeclareInt();
			this._textstartV = this.DeclareInt();
			if (!RegexCompiler.UseLegacyTimeoutCheck)
			{
				this._loopV = this.DeclareInt();
			}
			this._labels = null;
			this._notes = null;
			this._notecount = 0;
			this._backtrack = this.DefineLabel();
			this.GenerateForwardSection();
			this.GenerateMiddleSection();
			this.GenerateBacktrackSection();
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x00101FD4 File Offset: 0x001001D4
		internal void GenerateOneCode()
		{
			this.Ldthis();
			this.Callvirt(RegexCompiler._checkTimeoutM);
			int regexopcode = this._regexopcode;
			if (regexopcode <= 285)
			{
				if (regexopcode <= 164)
				{
					switch (regexopcode)
					{
					case 0:
					case 1:
					case 2:
					case 64:
					case 65:
					case 66:
						goto IL_143A;
					case 3:
					case 4:
					case 5:
					case 67:
					case 68:
					case 69:
						goto IL_1613;
					case 6:
					case 7:
					case 8:
					case 70:
					case 71:
					case 72:
						goto IL_190B;
					case 9:
					case 10:
					case 11:
					case 73:
					case 74:
					case 75:
						break;
					case 12:
						goto IL_1026;
					case 13:
					case 77:
						goto IL_11F8;
					case 14:
					{
						Label label = this._labels[this.NextCodepos()];
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ble(label);
						this.Leftchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					}
					case 15:
					{
						Label label2 = this._labels[this.NextCodepos()];
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Bge(label2);
						this.Rightchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					}
					case 16:
					case 17:
						this.Ldthis();
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ldloc(this._textendV);
						this.Callvirt(RegexCompiler._isboundaryM);
						if (this.Code() == 16)
						{
							this.BrfalseFar(this._backtrack);
							return;
						}
						this.BrtrueFar(this._backtrack);
						return;
					case 18:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.BgtFar(this._backtrack);
						return;
					case 19:
						this.Ldloc(this._textposV);
						this.Ldthisfld(RegexCompiler._textstartF);
						this.BneFar(this._backtrack);
						return;
					case 20:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Ldc(1);
						this.Sub();
						this.BltFar(this._backtrack);
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.Bge(this._labels[this.NextCodepos()]);
						this.Rightchar();
						this.Ldc(10);
						this.BneFar(this._backtrack);
						return;
					case 21:
						this.Ldloc(this._textposV);
						this.Ldloc(this._textendV);
						this.BltFar(this._backtrack);
						return;
					case 22:
						this.Back();
						return;
					case 23:
						this.PushTrack(this._textposV);
						this.Track();
						return;
					case 24:
					{
						LocalBuilder tempV = this._tempV;
						Label label3 = this.DefineLabel();
						this.PopStack();
						this.Dup();
						this.Stloc(tempV);
						this.PushTrack(tempV);
						this.Ldloc(this._textposV);
						this.Beq(label3);
						this.PushTrack(this._textposV);
						this.PushStack(this._textposV);
						this.Track();
						this.Goto(this.Operand(0));
						this.MarkLabel(label3);
						this.TrackUnique2(5);
						return;
					}
					case 25:
					{
						LocalBuilder tempV2 = this._tempV;
						Label label4 = this.DefineLabel();
						Label label5 = this.DefineLabel();
						Label label6 = this.DefineLabel();
						this.PopStack();
						this.Dup();
						this.Stloc(tempV2);
						this.Ldloc(tempV2);
						this.Ldc(-1);
						this.Beq(label5);
						this.PushTrack(tempV2);
						this.Br(label6);
						this.MarkLabel(label5);
						this.PushTrack(this._textposV);
						this.MarkLabel(label6);
						this.Ldloc(this._textposV);
						this.Beq(label4);
						this.PushTrack(this._textposV);
						this.Track();
						this.Br(this.AdvanceLabel());
						this.MarkLabel(label4);
						this.ReadyPushStack();
						this.Ldloc(tempV2);
						this.DoPush();
						this.TrackUnique2(6);
						return;
					}
					case 26:
						this.ReadyPushStack();
						this.Ldc(-1);
						this.DoPush();
						this.ReadyPushStack();
						this.Ldc(this.Operand(0));
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 27:
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldc(this.Operand(0));
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 28:
					{
						LocalBuilder tempV3 = this._tempV;
						LocalBuilder temp2V = this._temp2V;
						Label label7 = this.DefineLabel();
						Label label8 = this.DefineLabel();
						this.PopStack();
						this.Stloc(tempV3);
						this.PopStack();
						this.Dup();
						this.Stloc(temp2V);
						this.PushTrack(temp2V);
						this.Ldloc(this._textposV);
						this.Bne(label7);
						this.Ldloc(tempV3);
						this.Ldc(0);
						this.Bge(label8);
						this.MarkLabel(label7);
						this.Ldloc(tempV3);
						this.Ldc(this.Operand(1));
						this.Bge(label8);
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldloc(tempV3);
						this.Ldc(1);
						this.Add();
						this.DoPush();
						this.Track();
						this.Goto(this.Operand(0));
						this.MarkLabel(label8);
						this.PushTrack(tempV3);
						this.TrackUnique2(7);
						return;
					}
					case 29:
					{
						LocalBuilder tempV4 = this._tempV;
						LocalBuilder temp2V2 = this._temp2V;
						Label label9 = this.DefineLabel();
						Label label10 = this.DefineLabel();
						Label label11 = this._labels[this.NextCodepos()];
						this.PopStack();
						this.Stloc(tempV4);
						this.PopStack();
						this.Stloc(temp2V2);
						this.Ldloc(tempV4);
						this.Ldc(0);
						this.Bge(label9);
						this.PushTrack(temp2V2);
						this.PushStack(this._textposV);
						this.ReadyPushStack();
						this.Ldloc(tempV4);
						this.Ldc(1);
						this.Add();
						this.DoPush();
						this.TrackUnique2(8);
						this.Goto(this.Operand(0));
						this.MarkLabel(label9);
						this.PushTrack(temp2V2);
						this.PushTrack(tempV4);
						this.PushTrack(this._textposV);
						this.Track();
						return;
					}
					case 30:
						this.ReadyPushStack();
						this.Ldc(-1);
						this.DoPush();
						this.TrackUnique(0);
						return;
					case 31:
						this.PushStack(this._textposV);
						this.TrackUnique(0);
						return;
					case 32:
						if (this.Operand(1) != -1)
						{
							this.Ldthis();
							this.Ldc(this.Operand(1));
							this.Callvirt(RegexCompiler._ismatchedM);
							this.BrfalseFar(this._backtrack);
						}
						this.PopStack();
						this.Stloc(this._tempV);
						if (this.Operand(1) != -1)
						{
							this.Ldthis();
							this.Ldc(this.Operand(0));
							this.Ldc(this.Operand(1));
							this.Ldloc(this._tempV);
							this.Ldloc(this._textposV);
							this.Callvirt(RegexCompiler._transferM);
						}
						else
						{
							this.Ldthis();
							this.Ldc(this.Operand(0));
							this.Ldloc(this._tempV);
							this.Ldloc(this._textposV);
							this.Callvirt(RegexCompiler._captureM);
						}
						this.PushTrack(this._tempV);
						if (this.Operand(0) != -1 && this.Operand(1) != -1)
						{
							this.TrackUnique(4);
							return;
						}
						this.TrackUnique(3);
						return;
					case 33:
						this.ReadyPushTrack();
						this.PopStack();
						this.Dup();
						this.Stloc(this._textposV);
						this.DoPush();
						this.Track();
						return;
					case 34:
						this.ReadyPushStack();
						this.Ldthisfld(RegexCompiler._trackF);
						this.Ldlen();
						this.Ldloc(this._trackposV);
						this.Sub();
						this.DoPush();
						this.ReadyPushStack();
						this.Ldthis();
						this.Callvirt(RegexCompiler._crawlposM);
						this.DoPush();
						this.TrackUnique(1);
						return;
					case 35:
					{
						Label label12 = this.DefineLabel();
						Label label13 = this.DefineLabel();
						this.PopStack();
						this.Ldthisfld(RegexCompiler._trackF);
						this.Ldlen();
						this.PopStack();
						this.Sub();
						this.Stloc(this._trackposV);
						this.Dup();
						this.Ldthis();
						this.Callvirt(RegexCompiler._crawlposM);
						this.Beq(label13);
						this.MarkLabel(label12);
						this.Ldthis();
						this.Callvirt(RegexCompiler._uncaptureM);
						this.Dup();
						this.Ldthis();
						this.Callvirt(RegexCompiler._crawlposM);
						this.Bne(label12);
						this.MarkLabel(label13);
						this.Pop();
						this.Back();
						return;
					}
					case 36:
						this.PopStack();
						this.Stloc(this._tempV);
						this.Ldthisfld(RegexCompiler._trackF);
						this.Ldlen();
						this.PopStack();
						this.Sub();
						this.Stloc(this._trackposV);
						this.PushTrack(this._tempV);
						this.TrackUnique(9);
						return;
					case 37:
						this.Ldthis();
						this.Ldc(this.Operand(0));
						this.Callvirt(RegexCompiler._ismatchedM);
						this.BrfalseFar(this._backtrack);
						return;
					case 38:
						this.Goto(this.Operand(0));
						return;
					case 39:
					case 43:
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 57:
					case 58:
					case 59:
					case 60:
					case 61:
					case 62:
					case 63:
						goto IL_1B00;
					case 40:
						this.Mvlocfld(this._textposV, RegexCompiler._textposF);
						this.Ret();
						return;
					case 41:
					case 42:
						this.Ldthis();
						this.Ldloc(this._textposV);
						this.Ldloc(this._textbegV);
						this.Ldloc(this._textendV);
						this.Callvirt(RegexCompiler._isECMABoundaryM);
						if (this.Code() == 41)
						{
							this.BrfalseFar(this._backtrack);
							return;
						}
						this.BrtrueFar(this._backtrack);
						return;
					case 76:
						goto IL_110D;
					default:
						switch (regexopcode)
						{
						case 131:
						case 132:
						case 133:
							goto IL_186B;
						case 134:
						case 135:
						case 136:
							goto IL_19F5;
						case 137:
						case 138:
						case 139:
						case 140:
						case 141:
						case 142:
						case 143:
						case 144:
						case 145:
						case 146:
						case 147:
						case 148:
						case 149:
						case 150:
						case 163:
							goto IL_1B00;
						case 151:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.Goto(this.Operand(0));
							return;
						case 152:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PopStack();
							this.Pop();
							this.TrackUnique2(5);
							this.Advance();
							return;
						case 153:
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PushStack(this._textposV);
							this.TrackUnique2(6);
							this.Goto(this.Operand(0));
							return;
						case 154:
						case 155:
							this.PopDiscardStack(2);
							this.Back();
							return;
						case 156:
						{
							LocalBuilder tempV5 = this._tempV;
							Label label14 = this.DefineLabel();
							this.PopStack();
							this.Ldc(1);
							this.Sub();
							this.Dup();
							this.Stloc(tempV5);
							this.Ldc(0);
							this.Blt(label14);
							this.PopStack();
							this.Stloc(this._textposV);
							this.PushTrack(tempV5);
							this.TrackUnique2(7);
							this.Advance();
							this.MarkLabel(label14);
							this.ReadyReplaceStack(0);
							this.PopTrack();
							this.DoReplace();
							this.PushStack(tempV5);
							this.Back();
							return;
						}
						case 157:
						{
							Label label15 = this.DefineLabel();
							LocalBuilder tempV6 = this._tempV;
							this.PopTrack();
							this.Stloc(this._textposV);
							this.PopTrack();
							this.Dup();
							this.Stloc(tempV6);
							this.Ldc(this.Operand(1));
							this.Bge(label15);
							this.Ldloc(this._textposV);
							this.TopTrack();
							this.Beq(label15);
							this.PushStack(this._textposV);
							this.ReadyPushStack();
							this.Ldloc(tempV6);
							this.Ldc(1);
							this.Add();
							this.DoPush();
							this.TrackUnique2(8);
							this.Goto(this.Operand(0));
							this.MarkLabel(label15);
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.PushStack(tempV6);
							this.Back();
							return;
						}
						case 158:
						case 159:
							this.PopDiscardStack();
							this.Back();
							return;
						case 160:
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.Ldthis();
							this.Callvirt(RegexCompiler._uncaptureM);
							if (this.Operand(0) != -1 && this.Operand(1) != -1)
							{
								this.Ldthis();
								this.Callvirt(RegexCompiler._uncaptureM);
							}
							this.Back();
							return;
						case 161:
							this.ReadyPushStack();
							this.PopTrack();
							this.DoPush();
							this.Back();
							return;
						case 162:
							this.PopDiscardStack(2);
							this.Back();
							return;
						case 164:
						{
							Label label16 = this.DefineLabel();
							Label label17 = this.DefineLabel();
							this.PopTrack();
							this.Dup();
							this.Ldthis();
							this.Callvirt(RegexCompiler._crawlposM);
							this.Beq(label17);
							this.MarkLabel(label16);
							this.Ldthis();
							this.Callvirt(RegexCompiler._uncaptureM);
							this.Dup();
							this.Ldthis();
							this.Callvirt(RegexCompiler._crawlposM);
							this.Bne(label16);
							this.MarkLabel(label17);
							this.Pop();
							this.Back();
							return;
						}
						default:
							goto IL_1B00;
						}
						break;
					}
				}
				else
				{
					if (regexopcode - 195 <= 2)
					{
						goto IL_186B;
					}
					if (regexopcode - 198 <= 2)
					{
						goto IL_19F5;
					}
					switch (regexopcode)
					{
					case 280:
						this.ReadyPushStack();
						this.PopTrack();
						this.DoPush();
						this.Back();
						return;
					case 281:
						this.ReadyReplaceStack(0);
						this.PopTrack();
						this.DoReplace();
						this.Back();
						return;
					case 282:
					case 283:
						goto IL_1B00;
					case 284:
						this.PopTrack();
						this.Stloc(this._tempV);
						this.ReadyPushStack();
						this.PopTrack();
						this.DoPush();
						this.PushStack(this._tempV);
						this.Back();
						return;
					case 285:
						this.ReadyReplaceStack(1);
						this.PopTrack();
						this.DoReplace();
						this.ReadyReplaceStack(0);
						this.TopStack();
						this.Ldc(1);
						this.Sub();
						this.DoReplace();
						this.Back();
						return;
					default:
						goto IL_1B00;
					}
				}
			}
			else if (regexopcode <= 645)
			{
				switch (regexopcode)
				{
				case 512:
				case 513:
				case 514:
					goto IL_143A;
				case 515:
				case 516:
				case 517:
					goto IL_1613;
				case 518:
				case 519:
				case 520:
					goto IL_190B;
				case 521:
				case 522:
				case 523:
					break;
				case 524:
					goto IL_1026;
				case 525:
					goto IL_11F8;
				default:
					switch (regexopcode)
					{
					case 576:
					case 577:
					case 578:
						goto IL_143A;
					case 579:
					case 580:
					case 581:
						goto IL_1613;
					case 582:
					case 583:
					case 584:
						goto IL_190B;
					case 585:
					case 586:
					case 587:
						break;
					case 588:
						goto IL_110D;
					case 589:
						goto IL_11F8;
					default:
						if (regexopcode - 643 > 2)
						{
							goto IL_1B00;
						}
						goto IL_186B;
					}
					break;
				}
			}
			else
			{
				if (regexopcode - 646 <= 2)
				{
					goto IL_19F5;
				}
				if (regexopcode - 707 <= 2)
				{
					goto IL_186B;
				}
				if (regexopcode - 710 > 2)
				{
					goto IL_1B00;
				}
				goto IL_19F5;
			}
			this.Ldloc(this._textposV);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.BgeFar(this._backtrack);
				this.Rightcharnext();
			}
			else
			{
				this.Ldloc(this._textbegV);
				this.BleFar(this._backtrack);
				this.Leftcharnext();
			}
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 11)
			{
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler._charInSetM);
				this.BrfalseFar(this._backtrack);
				return;
			}
			this.Ldc(this.Operand(0));
			if (this.Code() == 9)
			{
				this.BneFar(this._backtrack);
				return;
			}
			this.BeqFar(this._backtrack);
			return;
			IL_1026:
			string text = this._strings[this.Operand(0)];
			this.Ldc(text.Length);
			this.Ldloc(this._textendV);
			this.Ldloc(this._textposV);
			this.Sub();
			this.BgtFar(this._backtrack);
			for (int i = 0; i < text.Length; i++)
			{
				this.Ldloc(this._textV);
				this.Ldloc(this._textposV);
				if (i != 0)
				{
					this.Ldc(i);
					this.Add();
				}
				this.Callvirt(RegexCompiler._getcharM);
				if (this.IsCi())
				{
					this.CallToLower();
				}
				this.Ldc((int)text[i]);
				this.BneFar(this._backtrack);
			}
			this.Ldloc(this._textposV);
			this.Ldc(text.Length);
			this.Add();
			this.Stloc(this._textposV);
			return;
			IL_110D:
			string text2 = this._strings[this.Operand(0)];
			this.Ldc(text2.Length);
			this.Ldloc(this._textposV);
			this.Ldloc(this._textbegV);
			this.Sub();
			this.BgtFar(this._backtrack);
			int j = text2.Length;
			while (j > 0)
			{
				j--;
				this.Ldloc(this._textV);
				this.Ldloc(this._textposV);
				this.Ldc(text2.Length - j);
				this.Sub();
				this.Callvirt(RegexCompiler._getcharM);
				if (this.IsCi())
				{
					this.CallToLower();
				}
				this.Ldc((int)text2[j]);
				this.BneFar(this._backtrack);
			}
			this.Ldloc(this._textposV);
			this.Ldc(text2.Length);
			this.Sub();
			this.Stloc(this._textposV);
			return;
			IL_11F8:
			LocalBuilder tempV7 = this._tempV;
			LocalBuilder temp2V3 = this._temp2V;
			Label label18 = this.DefineLabel();
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler._ismatchedM);
			if ((this._options & RegexOptions.ECMAScript) != RegexOptions.None)
			{
				this.Brfalse(this.AdvanceLabel());
			}
			else
			{
				this.BrfalseFar(this._backtrack);
			}
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler._matchlengthM);
			this.Dup();
			this.Stloc(tempV7);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldloc(this._textbegV);
			}
			this.Sub();
			this.BgtFar(this._backtrack);
			this.Ldthis();
			this.Ldc(this.Operand(0));
			this.Callvirt(RegexCompiler._matchindexM);
			if (!this.IsRtl())
			{
				this.Ldloc(tempV7);
				this.Add(this.IsRtl());
			}
			this.Stloc(temp2V3);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV7);
			this.Add(this.IsRtl());
			this.Stloc(this._textposV);
			this.MarkLabel(label18);
			this.Ldloc(tempV7);
			this.Ldc(0);
			this.Ble(this.AdvanceLabel());
			this.Ldloc(this._textV);
			this.Ldloc(temp2V3);
			this.Ldloc(tempV7);
			if (this.IsRtl())
			{
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV7);
			}
			this.Sub(this.IsRtl());
			this.Callvirt(RegexCompiler._getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV7);
			if (!this.IsRtl())
			{
				this.Dup();
				this.Ldc(1);
				this.Sub();
				this.Stloc(tempV7);
			}
			this.Sub(this.IsRtl());
			this.Callvirt(RegexCompiler._getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			this.Beq(label18);
			this.Back();
			return;
			IL_143A:
			LocalBuilder tempV8 = this._tempV;
			Label label19 = this.DefineLabel();
			int num = this.Operand(1);
			if (num == 0)
			{
				return;
			}
			this.Ldc(num);
			if (!this.IsRtl())
			{
				this.Ldloc(this._textendV);
				this.Ldloc(this._textposV);
			}
			else
			{
				this.Ldloc(this._textposV);
				this.Ldloc(this._textbegV);
			}
			this.Sub();
			this.BgtFar(this._backtrack);
			this.Ldloc(this._textposV);
			this.Ldc(num);
			this.Add(this.IsRtl());
			this.Stloc(this._textposV);
			this.Ldc(num);
			this.Stloc(tempV8);
			this.MarkLabel(label19);
			this.Ldloc(this._textV);
			this.Ldloc(this._textposV);
			this.Ldloc(tempV8);
			if (this.IsRtl())
			{
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV8);
				this.Add();
			}
			else
			{
				this.Dup();
				this.Ldc(1);
				this.Sub();
				this.Stloc(tempV8);
				this.Sub();
			}
			this.Callvirt(RegexCompiler._getcharM);
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 2)
			{
				if (!RegexCompiler.UseLegacyTimeoutCheck)
				{
					this.EmitTimeoutCheck();
				}
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler._charInSetM);
				this.BrfalseFar(this._backtrack);
			}
			else
			{
				this.Ldc(this.Operand(0));
				if (this.Code() == 0)
				{
					this.BneFar(this._backtrack);
				}
				else
				{
					this.BeqFar(this._backtrack);
				}
			}
			this.Ldloc(tempV8);
			this.Ldc(0);
			if (this.Code() == 2)
			{
				this.BgtFar(label19);
				return;
			}
			this.Bgt(label19);
			return;
			IL_1613:
			LocalBuilder tempV9 = this._tempV;
			LocalBuilder temp2V4 = this._temp2V;
			Label label20 = this.DefineLabel();
			Label label21 = this.DefineLabel();
			int num2 = this.Operand(1);
			if (num2 != 0)
			{
				if (!this.IsRtl())
				{
					this.Ldloc(this._textendV);
					this.Ldloc(this._textposV);
				}
				else
				{
					this.Ldloc(this._textposV);
					this.Ldloc(this._textbegV);
				}
				this.Sub();
				if (num2 != 2147483647)
				{
					Label label22 = this.DefineLabel();
					this.Dup();
					this.Ldc(num2);
					this.Blt(label22);
					this.Pop();
					this.Ldc(num2);
					this.MarkLabel(label22);
				}
				this.Dup();
				this.Stloc(temp2V4);
				this.Ldc(1);
				this.Add();
				this.Stloc(tempV9);
				this.MarkLabel(label20);
				this.Ldloc(tempV9);
				this.Ldc(1);
				this.Sub();
				this.Dup();
				this.Stloc(tempV9);
				this.Ldc(0);
				if (this.Code() == 5)
				{
					this.BleFar(label21);
				}
				else
				{
					this.Ble(label21);
				}
				if (this.IsRtl())
				{
					this.Leftcharnext();
				}
				else
				{
					this.Rightcharnext();
				}
				if (this.IsCi())
				{
					this.CallToLower();
				}
				if (this.Code() == 5)
				{
					if (!RegexCompiler.UseLegacyTimeoutCheck)
					{
						this.EmitTimeoutCheck();
					}
					this.Ldstr(this._strings[this.Operand(0)]);
					this.Call(RegexCompiler._charInSetM);
					this.BrtrueFar(label20);
				}
				else
				{
					this.Ldc(this.Operand(0));
					if (this.Code() == 3)
					{
						this.Beq(label20);
					}
					else
					{
						this.Bne(label20);
					}
				}
				this.Ldloc(this._textposV);
				this.Ldc(1);
				this.Sub(this.IsRtl());
				this.Stloc(this._textposV);
				this.MarkLabel(label21);
				this.Ldloc(temp2V4);
				this.Ldloc(tempV9);
				this.Ble(this.AdvanceLabel());
				this.ReadyPushTrack();
				this.Ldloc(temp2V4);
				this.Ldloc(tempV9);
				this.Sub();
				this.Ldc(1);
				this.Sub();
				this.DoPush();
				this.ReadyPushTrack();
				this.Ldloc(this._textposV);
				this.Ldc(1);
				this.Sub(this.IsRtl());
				this.DoPush();
				this.Track();
				return;
			}
			return;
			IL_186B:
			this.PopTrack();
			this.Stloc(this._textposV);
			this.PopTrack();
			this.Stloc(this._tempV);
			this.Ldloc(this._tempV);
			this.Ldc(0);
			this.BleFar(this.AdvanceLabel());
			this.ReadyPushTrack();
			this.Ldloc(this._tempV);
			this.Ldc(1);
			this.Sub();
			this.DoPush();
			this.ReadyPushTrack();
			this.Ldloc(this._textposV);
			this.Ldc(1);
			this.Sub(this.IsRtl());
			this.DoPush();
			this.Trackagain();
			this.Advance();
			return;
			IL_190B:
			LocalBuilder tempV10 = this._tempV;
			int num3 = this.Operand(1);
			if (num3 != 0)
			{
				if (!this.IsRtl())
				{
					this.Ldloc(this._textendV);
					this.Ldloc(this._textposV);
				}
				else
				{
					this.Ldloc(this._textposV);
					this.Ldloc(this._textbegV);
				}
				this.Sub();
				if (num3 != 2147483647)
				{
					Label label23 = this.DefineLabel();
					this.Dup();
					this.Ldc(num3);
					this.Blt(label23);
					this.Pop();
					this.Ldc(num3);
					this.MarkLabel(label23);
				}
				this.Dup();
				this.Stloc(tempV10);
				this.Ldc(0);
				this.Ble(this.AdvanceLabel());
				this.ReadyPushTrack();
				this.Ldloc(tempV10);
				this.Ldc(1);
				this.Sub();
				this.DoPush();
				this.PushTrack(this._textposV);
				this.Track();
				return;
			}
			return;
			IL_19F5:
			this.PopTrack();
			this.Stloc(this._textposV);
			this.PopTrack();
			this.Stloc(this._temp2V);
			if (!this.IsRtl())
			{
				this.Rightcharnext();
			}
			else
			{
				this.Leftcharnext();
			}
			if (this.IsCi())
			{
				this.CallToLower();
			}
			if (this.Code() == 8)
			{
				this.Ldstr(this._strings[this.Operand(0)]);
				this.Call(RegexCompiler._charInSetM);
				this.BrfalseFar(this._backtrack);
			}
			else
			{
				this.Ldc(this.Operand(0));
				if (this.Code() == 6)
				{
					this.BneFar(this._backtrack);
				}
				else
				{
					this.BeqFar(this._backtrack);
				}
			}
			this.Ldloc(this._temp2V);
			this.Ldc(0);
			this.BleFar(this.AdvanceLabel());
			this.ReadyPushTrack();
			this.Ldloc(this._temp2V);
			this.Ldc(1);
			this.Sub();
			this.DoPush();
			this.PushTrack(this._textposV);
			this.Trackagain();
			this.Advance();
			return;
			IL_1B00:
			throw new NotImplementedException(SR.GetString("UnimplementedState"));
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x00103AF4 File Offset: 0x00101CF4
		private void EmitTimeoutCheck()
		{
			Label label = this.DefineLabel();
			this.Ldloc(this._loopV);
			this.Ldc(1);
			this.Add();
			this.Stloc(this._loopV);
			this.Ldloc(this._loopV);
			this.Ldc(2000);
			this.Rem();
			this.Ldc(0);
			this.Ceq();
			this.Brfalse(label);
			this.Ldthis();
			this.Callvirt(RegexCompiler._checkTimeoutM);
			this.MarkLabel(label);
		}

		// Token: 0x04002D5B RID: 11611
		internal static FieldInfo _textbegF;

		// Token: 0x04002D5C RID: 11612
		internal static FieldInfo _textendF;

		// Token: 0x04002D5D RID: 11613
		internal static FieldInfo _textstartF;

		// Token: 0x04002D5E RID: 11614
		internal static FieldInfo _textposF;

		// Token: 0x04002D5F RID: 11615
		internal static FieldInfo _textF;

		// Token: 0x04002D60 RID: 11616
		internal static FieldInfo _trackposF;

		// Token: 0x04002D61 RID: 11617
		internal static FieldInfo _trackF;

		// Token: 0x04002D62 RID: 11618
		internal static FieldInfo _stackposF;

		// Token: 0x04002D63 RID: 11619
		internal static FieldInfo _stackF;

		// Token: 0x04002D64 RID: 11620
		internal static FieldInfo _trackcountF;

		// Token: 0x04002D65 RID: 11621
		internal static MethodInfo _ensurestorageM;

		// Token: 0x04002D66 RID: 11622
		internal static MethodInfo _captureM;

		// Token: 0x04002D67 RID: 11623
		internal static MethodInfo _transferM;

		// Token: 0x04002D68 RID: 11624
		internal static MethodInfo _uncaptureM;

		// Token: 0x04002D69 RID: 11625
		internal static MethodInfo _ismatchedM;

		// Token: 0x04002D6A RID: 11626
		internal static MethodInfo _matchlengthM;

		// Token: 0x04002D6B RID: 11627
		internal static MethodInfo _matchindexM;

		// Token: 0x04002D6C RID: 11628
		internal static MethodInfo _isboundaryM;

		// Token: 0x04002D6D RID: 11629
		internal static MethodInfo _isECMABoundaryM;

		// Token: 0x04002D6E RID: 11630
		internal static MethodInfo _chartolowerM;

		// Token: 0x04002D6F RID: 11631
		internal static MethodInfo _getcharM;

		// Token: 0x04002D70 RID: 11632
		internal static MethodInfo _crawlposM;

		// Token: 0x04002D71 RID: 11633
		internal static MethodInfo _charInSetM;

		// Token: 0x04002D72 RID: 11634
		internal static MethodInfo _getCurrentCulture;

		// Token: 0x04002D73 RID: 11635
		internal static MethodInfo _getInvariantCulture;

		// Token: 0x04002D74 RID: 11636
		internal static MethodInfo _checkTimeoutM;

		// Token: 0x04002D75 RID: 11637
		internal ILGenerator _ilg;

		// Token: 0x04002D76 RID: 11638
		internal LocalBuilder _textstartV;

		// Token: 0x04002D77 RID: 11639
		internal LocalBuilder _textbegV;

		// Token: 0x04002D78 RID: 11640
		internal LocalBuilder _textendV;

		// Token: 0x04002D79 RID: 11641
		internal LocalBuilder _textposV;

		// Token: 0x04002D7A RID: 11642
		internal LocalBuilder _textV;

		// Token: 0x04002D7B RID: 11643
		internal LocalBuilder _trackposV;

		// Token: 0x04002D7C RID: 11644
		internal LocalBuilder _trackV;

		// Token: 0x04002D7D RID: 11645
		internal LocalBuilder _stackposV;

		// Token: 0x04002D7E RID: 11646
		internal LocalBuilder _stackV;

		// Token: 0x04002D7F RID: 11647
		internal LocalBuilder _tempV;

		// Token: 0x04002D80 RID: 11648
		internal LocalBuilder _temp2V;

		// Token: 0x04002D81 RID: 11649
		internal LocalBuilder _temp3V;

		// Token: 0x04002D82 RID: 11650
		internal LocalBuilder _loopV;

		// Token: 0x04002D83 RID: 11651
		internal RegexCode _code;

		// Token: 0x04002D84 RID: 11652
		internal int[] _codes;

		// Token: 0x04002D85 RID: 11653
		internal string[] _strings;

		// Token: 0x04002D86 RID: 11654
		internal RegexPrefix _fcPrefix;

		// Token: 0x04002D87 RID: 11655
		internal RegexBoyerMoore _bmPrefix;

		// Token: 0x04002D88 RID: 11656
		internal int _anchors;

		// Token: 0x04002D89 RID: 11657
		internal Label[] _labels;

		// Token: 0x04002D8A RID: 11658
		internal RegexCompiler.BacktrackNote[] _notes;

		// Token: 0x04002D8B RID: 11659
		internal int _notecount;

		// Token: 0x04002D8C RID: 11660
		internal int _trackcount;

		// Token: 0x04002D8D RID: 11661
		internal Label _backtrack;

		// Token: 0x04002D8E RID: 11662
		internal int _regexopcode;

		// Token: 0x04002D8F RID: 11663
		internal int _codepos;

		// Token: 0x04002D90 RID: 11664
		internal int _backpos;

		// Token: 0x04002D91 RID: 11665
		internal RegexOptions _options;

		// Token: 0x04002D92 RID: 11666
		internal int[] _uniquenote;

		// Token: 0x04002D93 RID: 11667
		internal int[] _goto;

		// Token: 0x04002D94 RID: 11668
		internal const int stackpop = 0;

		// Token: 0x04002D95 RID: 11669
		internal const int stackpop2 = 1;

		// Token: 0x04002D96 RID: 11670
		internal const int stackpop3 = 2;

		// Token: 0x04002D97 RID: 11671
		internal const int capback = 3;

		// Token: 0x04002D98 RID: 11672
		internal const int capback2 = 4;

		// Token: 0x04002D99 RID: 11673
		internal const int branchmarkback2 = 5;

		// Token: 0x04002D9A RID: 11674
		internal const int lazybranchmarkback2 = 6;

		// Token: 0x04002D9B RID: 11675
		internal const int branchcountback2 = 7;

		// Token: 0x04002D9C RID: 11676
		internal const int lazybranchcountback2 = 8;

		// Token: 0x04002D9D RID: 11677
		internal const int forejumpback = 9;

		// Token: 0x04002D9E RID: 11678
		internal const int uniquecount = 10;

		// Token: 0x04002D9F RID: 11679
		private const int LoopTimeoutCheckCount = 2000;

		// Token: 0x04002DA0 RID: 11680
		private static readonly bool UseLegacyTimeoutCheck = LocalAppContextSwitches.UseLegacyTimeoutCheck;

		// Token: 0x020008B5 RID: 2229
		internal sealed class BacktrackNote
		{
			// Token: 0x0600460A RID: 17930 RVA: 0x00124424 File Offset: 0x00122624
			internal BacktrackNote(int flags, Label label, int codepos)
			{
				this._codepos = codepos;
				this._flags = flags;
				this._label = label;
			}

			// Token: 0x04003B45 RID: 15173
			internal int _codepos;

			// Token: 0x04003B46 RID: 15174
			internal int _flags;

			// Token: 0x04003B47 RID: 15175
			internal Label _label;
		}
	}
}
