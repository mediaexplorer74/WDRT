using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000652 RID: 1618
	internal enum OpCodeValues
	{
		// Token: 0x04001F74 RID: 8052
		Nop,
		// Token: 0x04001F75 RID: 8053
		Break,
		// Token: 0x04001F76 RID: 8054
		Ldarg_0,
		// Token: 0x04001F77 RID: 8055
		Ldarg_1,
		// Token: 0x04001F78 RID: 8056
		Ldarg_2,
		// Token: 0x04001F79 RID: 8057
		Ldarg_3,
		// Token: 0x04001F7A RID: 8058
		Ldloc_0,
		// Token: 0x04001F7B RID: 8059
		Ldloc_1,
		// Token: 0x04001F7C RID: 8060
		Ldloc_2,
		// Token: 0x04001F7D RID: 8061
		Ldloc_3,
		// Token: 0x04001F7E RID: 8062
		Stloc_0,
		// Token: 0x04001F7F RID: 8063
		Stloc_1,
		// Token: 0x04001F80 RID: 8064
		Stloc_2,
		// Token: 0x04001F81 RID: 8065
		Stloc_3,
		// Token: 0x04001F82 RID: 8066
		Ldarg_S,
		// Token: 0x04001F83 RID: 8067
		Ldarga_S,
		// Token: 0x04001F84 RID: 8068
		Starg_S,
		// Token: 0x04001F85 RID: 8069
		Ldloc_S,
		// Token: 0x04001F86 RID: 8070
		Ldloca_S,
		// Token: 0x04001F87 RID: 8071
		Stloc_S,
		// Token: 0x04001F88 RID: 8072
		Ldnull,
		// Token: 0x04001F89 RID: 8073
		Ldc_I4_M1,
		// Token: 0x04001F8A RID: 8074
		Ldc_I4_0,
		// Token: 0x04001F8B RID: 8075
		Ldc_I4_1,
		// Token: 0x04001F8C RID: 8076
		Ldc_I4_2,
		// Token: 0x04001F8D RID: 8077
		Ldc_I4_3,
		// Token: 0x04001F8E RID: 8078
		Ldc_I4_4,
		// Token: 0x04001F8F RID: 8079
		Ldc_I4_5,
		// Token: 0x04001F90 RID: 8080
		Ldc_I4_6,
		// Token: 0x04001F91 RID: 8081
		Ldc_I4_7,
		// Token: 0x04001F92 RID: 8082
		Ldc_I4_8,
		// Token: 0x04001F93 RID: 8083
		Ldc_I4_S,
		// Token: 0x04001F94 RID: 8084
		Ldc_I4,
		// Token: 0x04001F95 RID: 8085
		Ldc_I8,
		// Token: 0x04001F96 RID: 8086
		Ldc_R4,
		// Token: 0x04001F97 RID: 8087
		Ldc_R8,
		// Token: 0x04001F98 RID: 8088
		Dup = 37,
		// Token: 0x04001F99 RID: 8089
		Pop,
		// Token: 0x04001F9A RID: 8090
		Jmp,
		// Token: 0x04001F9B RID: 8091
		Call,
		// Token: 0x04001F9C RID: 8092
		Calli,
		// Token: 0x04001F9D RID: 8093
		Ret,
		// Token: 0x04001F9E RID: 8094
		Br_S,
		// Token: 0x04001F9F RID: 8095
		Brfalse_S,
		// Token: 0x04001FA0 RID: 8096
		Brtrue_S,
		// Token: 0x04001FA1 RID: 8097
		Beq_S,
		// Token: 0x04001FA2 RID: 8098
		Bge_S,
		// Token: 0x04001FA3 RID: 8099
		Bgt_S,
		// Token: 0x04001FA4 RID: 8100
		Ble_S,
		// Token: 0x04001FA5 RID: 8101
		Blt_S,
		// Token: 0x04001FA6 RID: 8102
		Bne_Un_S,
		// Token: 0x04001FA7 RID: 8103
		Bge_Un_S,
		// Token: 0x04001FA8 RID: 8104
		Bgt_Un_S,
		// Token: 0x04001FA9 RID: 8105
		Ble_Un_S,
		// Token: 0x04001FAA RID: 8106
		Blt_Un_S,
		// Token: 0x04001FAB RID: 8107
		Br,
		// Token: 0x04001FAC RID: 8108
		Brfalse,
		// Token: 0x04001FAD RID: 8109
		Brtrue,
		// Token: 0x04001FAE RID: 8110
		Beq,
		// Token: 0x04001FAF RID: 8111
		Bge,
		// Token: 0x04001FB0 RID: 8112
		Bgt,
		// Token: 0x04001FB1 RID: 8113
		Ble,
		// Token: 0x04001FB2 RID: 8114
		Blt,
		// Token: 0x04001FB3 RID: 8115
		Bne_Un,
		// Token: 0x04001FB4 RID: 8116
		Bge_Un,
		// Token: 0x04001FB5 RID: 8117
		Bgt_Un,
		// Token: 0x04001FB6 RID: 8118
		Ble_Un,
		// Token: 0x04001FB7 RID: 8119
		Blt_Un,
		// Token: 0x04001FB8 RID: 8120
		Switch,
		// Token: 0x04001FB9 RID: 8121
		Ldind_I1,
		// Token: 0x04001FBA RID: 8122
		Ldind_U1,
		// Token: 0x04001FBB RID: 8123
		Ldind_I2,
		// Token: 0x04001FBC RID: 8124
		Ldind_U2,
		// Token: 0x04001FBD RID: 8125
		Ldind_I4,
		// Token: 0x04001FBE RID: 8126
		Ldind_U4,
		// Token: 0x04001FBF RID: 8127
		Ldind_I8,
		// Token: 0x04001FC0 RID: 8128
		Ldind_I,
		// Token: 0x04001FC1 RID: 8129
		Ldind_R4,
		// Token: 0x04001FC2 RID: 8130
		Ldind_R8,
		// Token: 0x04001FC3 RID: 8131
		Ldind_Ref,
		// Token: 0x04001FC4 RID: 8132
		Stind_Ref,
		// Token: 0x04001FC5 RID: 8133
		Stind_I1,
		// Token: 0x04001FC6 RID: 8134
		Stind_I2,
		// Token: 0x04001FC7 RID: 8135
		Stind_I4,
		// Token: 0x04001FC8 RID: 8136
		Stind_I8,
		// Token: 0x04001FC9 RID: 8137
		Stind_R4,
		// Token: 0x04001FCA RID: 8138
		Stind_R8,
		// Token: 0x04001FCB RID: 8139
		Add,
		// Token: 0x04001FCC RID: 8140
		Sub,
		// Token: 0x04001FCD RID: 8141
		Mul,
		// Token: 0x04001FCE RID: 8142
		Div,
		// Token: 0x04001FCF RID: 8143
		Div_Un,
		// Token: 0x04001FD0 RID: 8144
		Rem,
		// Token: 0x04001FD1 RID: 8145
		Rem_Un,
		// Token: 0x04001FD2 RID: 8146
		And,
		// Token: 0x04001FD3 RID: 8147
		Or,
		// Token: 0x04001FD4 RID: 8148
		Xor,
		// Token: 0x04001FD5 RID: 8149
		Shl,
		// Token: 0x04001FD6 RID: 8150
		Shr,
		// Token: 0x04001FD7 RID: 8151
		Shr_Un,
		// Token: 0x04001FD8 RID: 8152
		Neg,
		// Token: 0x04001FD9 RID: 8153
		Not,
		// Token: 0x04001FDA RID: 8154
		Conv_I1,
		// Token: 0x04001FDB RID: 8155
		Conv_I2,
		// Token: 0x04001FDC RID: 8156
		Conv_I4,
		// Token: 0x04001FDD RID: 8157
		Conv_I8,
		// Token: 0x04001FDE RID: 8158
		Conv_R4,
		// Token: 0x04001FDF RID: 8159
		Conv_R8,
		// Token: 0x04001FE0 RID: 8160
		Conv_U4,
		// Token: 0x04001FE1 RID: 8161
		Conv_U8,
		// Token: 0x04001FE2 RID: 8162
		Callvirt,
		// Token: 0x04001FE3 RID: 8163
		Cpobj,
		// Token: 0x04001FE4 RID: 8164
		Ldobj,
		// Token: 0x04001FE5 RID: 8165
		Ldstr,
		// Token: 0x04001FE6 RID: 8166
		Newobj,
		// Token: 0x04001FE7 RID: 8167
		Castclass,
		// Token: 0x04001FE8 RID: 8168
		Isinst,
		// Token: 0x04001FE9 RID: 8169
		Conv_R_Un,
		// Token: 0x04001FEA RID: 8170
		Unbox = 121,
		// Token: 0x04001FEB RID: 8171
		Throw,
		// Token: 0x04001FEC RID: 8172
		Ldfld,
		// Token: 0x04001FED RID: 8173
		Ldflda,
		// Token: 0x04001FEE RID: 8174
		Stfld,
		// Token: 0x04001FEF RID: 8175
		Ldsfld,
		// Token: 0x04001FF0 RID: 8176
		Ldsflda,
		// Token: 0x04001FF1 RID: 8177
		Stsfld,
		// Token: 0x04001FF2 RID: 8178
		Stobj,
		// Token: 0x04001FF3 RID: 8179
		Conv_Ovf_I1_Un,
		// Token: 0x04001FF4 RID: 8180
		Conv_Ovf_I2_Un,
		// Token: 0x04001FF5 RID: 8181
		Conv_Ovf_I4_Un,
		// Token: 0x04001FF6 RID: 8182
		Conv_Ovf_I8_Un,
		// Token: 0x04001FF7 RID: 8183
		Conv_Ovf_U1_Un,
		// Token: 0x04001FF8 RID: 8184
		Conv_Ovf_U2_Un,
		// Token: 0x04001FF9 RID: 8185
		Conv_Ovf_U4_Un,
		// Token: 0x04001FFA RID: 8186
		Conv_Ovf_U8_Un,
		// Token: 0x04001FFB RID: 8187
		Conv_Ovf_I_Un,
		// Token: 0x04001FFC RID: 8188
		Conv_Ovf_U_Un,
		// Token: 0x04001FFD RID: 8189
		Box,
		// Token: 0x04001FFE RID: 8190
		Newarr,
		// Token: 0x04001FFF RID: 8191
		Ldlen,
		// Token: 0x04002000 RID: 8192
		Ldelema,
		// Token: 0x04002001 RID: 8193
		Ldelem_I1,
		// Token: 0x04002002 RID: 8194
		Ldelem_U1,
		// Token: 0x04002003 RID: 8195
		Ldelem_I2,
		// Token: 0x04002004 RID: 8196
		Ldelem_U2,
		// Token: 0x04002005 RID: 8197
		Ldelem_I4,
		// Token: 0x04002006 RID: 8198
		Ldelem_U4,
		// Token: 0x04002007 RID: 8199
		Ldelem_I8,
		// Token: 0x04002008 RID: 8200
		Ldelem_I,
		// Token: 0x04002009 RID: 8201
		Ldelem_R4,
		// Token: 0x0400200A RID: 8202
		Ldelem_R8,
		// Token: 0x0400200B RID: 8203
		Ldelem_Ref,
		// Token: 0x0400200C RID: 8204
		Stelem_I,
		// Token: 0x0400200D RID: 8205
		Stelem_I1,
		// Token: 0x0400200E RID: 8206
		Stelem_I2,
		// Token: 0x0400200F RID: 8207
		Stelem_I4,
		// Token: 0x04002010 RID: 8208
		Stelem_I8,
		// Token: 0x04002011 RID: 8209
		Stelem_R4,
		// Token: 0x04002012 RID: 8210
		Stelem_R8,
		// Token: 0x04002013 RID: 8211
		Stelem_Ref,
		// Token: 0x04002014 RID: 8212
		Ldelem,
		// Token: 0x04002015 RID: 8213
		Stelem,
		// Token: 0x04002016 RID: 8214
		Unbox_Any,
		// Token: 0x04002017 RID: 8215
		Conv_Ovf_I1 = 179,
		// Token: 0x04002018 RID: 8216
		Conv_Ovf_U1,
		// Token: 0x04002019 RID: 8217
		Conv_Ovf_I2,
		// Token: 0x0400201A RID: 8218
		Conv_Ovf_U2,
		// Token: 0x0400201B RID: 8219
		Conv_Ovf_I4,
		// Token: 0x0400201C RID: 8220
		Conv_Ovf_U4,
		// Token: 0x0400201D RID: 8221
		Conv_Ovf_I8,
		// Token: 0x0400201E RID: 8222
		Conv_Ovf_U8,
		// Token: 0x0400201F RID: 8223
		Refanyval = 194,
		// Token: 0x04002020 RID: 8224
		Ckfinite,
		// Token: 0x04002021 RID: 8225
		Mkrefany = 198,
		// Token: 0x04002022 RID: 8226
		Ldtoken = 208,
		// Token: 0x04002023 RID: 8227
		Conv_U2,
		// Token: 0x04002024 RID: 8228
		Conv_U1,
		// Token: 0x04002025 RID: 8229
		Conv_I,
		// Token: 0x04002026 RID: 8230
		Conv_Ovf_I,
		// Token: 0x04002027 RID: 8231
		Conv_Ovf_U,
		// Token: 0x04002028 RID: 8232
		Add_Ovf,
		// Token: 0x04002029 RID: 8233
		Add_Ovf_Un,
		// Token: 0x0400202A RID: 8234
		Mul_Ovf,
		// Token: 0x0400202B RID: 8235
		Mul_Ovf_Un,
		// Token: 0x0400202C RID: 8236
		Sub_Ovf,
		// Token: 0x0400202D RID: 8237
		Sub_Ovf_Un,
		// Token: 0x0400202E RID: 8238
		Endfinally,
		// Token: 0x0400202F RID: 8239
		Leave,
		// Token: 0x04002030 RID: 8240
		Leave_S,
		// Token: 0x04002031 RID: 8241
		Stind_I,
		// Token: 0x04002032 RID: 8242
		Conv_U,
		// Token: 0x04002033 RID: 8243
		Prefix7 = 248,
		// Token: 0x04002034 RID: 8244
		Prefix6,
		// Token: 0x04002035 RID: 8245
		Prefix5,
		// Token: 0x04002036 RID: 8246
		Prefix4,
		// Token: 0x04002037 RID: 8247
		Prefix3,
		// Token: 0x04002038 RID: 8248
		Prefix2,
		// Token: 0x04002039 RID: 8249
		Prefix1,
		// Token: 0x0400203A RID: 8250
		Prefixref,
		// Token: 0x0400203B RID: 8251
		Arglist = 65024,
		// Token: 0x0400203C RID: 8252
		Ceq,
		// Token: 0x0400203D RID: 8253
		Cgt,
		// Token: 0x0400203E RID: 8254
		Cgt_Un,
		// Token: 0x0400203F RID: 8255
		Clt,
		// Token: 0x04002040 RID: 8256
		Clt_Un,
		// Token: 0x04002041 RID: 8257
		Ldftn,
		// Token: 0x04002042 RID: 8258
		Ldvirtftn,
		// Token: 0x04002043 RID: 8259
		Ldarg = 65033,
		// Token: 0x04002044 RID: 8260
		Ldarga,
		// Token: 0x04002045 RID: 8261
		Starg,
		// Token: 0x04002046 RID: 8262
		Ldloc,
		// Token: 0x04002047 RID: 8263
		Ldloca,
		// Token: 0x04002048 RID: 8264
		Stloc,
		// Token: 0x04002049 RID: 8265
		Localloc,
		// Token: 0x0400204A RID: 8266
		Endfilter = 65041,
		// Token: 0x0400204B RID: 8267
		Unaligned_,
		// Token: 0x0400204C RID: 8268
		Volatile_,
		// Token: 0x0400204D RID: 8269
		Tail_,
		// Token: 0x0400204E RID: 8270
		Initobj,
		// Token: 0x0400204F RID: 8271
		Constrained_,
		// Token: 0x04002050 RID: 8272
		Cpblk,
		// Token: 0x04002051 RID: 8273
		Initblk,
		// Token: 0x04002052 RID: 8274
		Rethrow = 65050,
		// Token: 0x04002053 RID: 8275
		Sizeof = 65052,
		// Token: 0x04002054 RID: 8276
		Refanytype,
		// Token: 0x04002055 RID: 8277
		Readonly_
	}
}
