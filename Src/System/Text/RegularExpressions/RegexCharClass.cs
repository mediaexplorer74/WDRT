using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200068E RID: 1678
	internal sealed class RegexCharClass
	{
		// Token: 0x06003DEF RID: 15855 RVA: 0x000FD658 File Offset: 0x000FB858
		static RegexCharClass()
		{
			string[,] array = new string[112, 2];
			array[0, 0] = "IsAlphabeticPresentationForms";
			array[0, 1] = "ﬀﭐ";
			array[1, 0] = "IsArabic";
			array[1, 1] = "\u0600܀";
			array[2, 0] = "IsArabicPresentationForms-A";
			array[2, 1] = "ﭐ\ufe00";
			array[3, 0] = "IsArabicPresentationForms-B";
			array[3, 1] = "ﹰ\uff00";
			array[4, 0] = "IsArmenian";
			array[4, 1] = "\u0530\u0590";
			array[5, 0] = "IsArrows";
			array[5, 1] = "←∀";
			array[6, 0] = "IsBasicLatin";
			array[6, 1] = "\0\u0080";
			array[7, 0] = "IsBengali";
			array[7, 1] = "ঀ\u0a00";
			array[8, 0] = "IsBlockElements";
			array[8, 1] = "▀■";
			array[9, 0] = "IsBopomofo";
			array[9, 1] = "\u3100\u3130";
			array[10, 0] = "IsBopomofoExtended";
			array[10, 1] = "ㆠ㇀";
			array[11, 0] = "IsBoxDrawing";
			array[11, 1] = "─▀";
			array[12, 0] = "IsBraillePatterns";
			array[12, 1] = "⠀⤀";
			array[13, 0] = "IsBuhid";
			array[13, 1] = "ᝀᝠ";
			array[14, 0] = "IsCJKCompatibility";
			array[14, 1] = "㌀㐀";
			array[15, 0] = "IsCJKCompatibilityForms";
			array[15, 1] = "︰﹐";
			array[16, 0] = "IsCJKCompatibilityIdeographs";
			array[16, 1] = "豈ﬀ";
			array[17, 0] = "IsCJKRadicalsSupplement";
			array[17, 1] = "⺀⼀";
			array[18, 0] = "IsCJKSymbolsandPunctuation";
			array[18, 1] = "\u3000\u3040";
			array[19, 0] = "IsCJKUnifiedIdeographs";
			array[19, 1] = "一ꀀ";
			array[20, 0] = "IsCJKUnifiedIdeographsExtensionA";
			array[20, 1] = "㐀䷀";
			array[21, 0] = "IsCherokee";
			array[21, 1] = "Ꭰ᐀";
			array[22, 0] = "IsCombiningDiacriticalMarks";
			array[22, 1] = "\u0300Ͱ";
			array[23, 0] = "IsCombiningDiacriticalMarksforSymbols";
			array[23, 1] = "\u20d0℀";
			array[24, 0] = "IsCombiningHalfMarks";
			array[24, 1] = "\ufe20︰";
			array[25, 0] = "IsCombiningMarksforSymbols";
			array[25, 1] = "\u20d0℀";
			array[26, 0] = "IsControlPictures";
			array[26, 1] = "␀⑀";
			array[27, 0] = "IsCurrencySymbols";
			array[27, 1] = "₠\u20d0";
			array[28, 0] = "IsCyrillic";
			array[28, 1] = "ЀԀ";
			array[29, 0] = "IsCyrillicSupplement";
			array[29, 1] = "Ԁ\u0530";
			array[30, 0] = "IsDevanagari";
			array[30, 1] = "\u0900ঀ";
			array[31, 0] = "IsDingbats";
			array[31, 1] = "✀⟀";
			array[32, 0] = "IsEnclosedAlphanumerics";
			array[32, 1] = "①─";
			array[33, 0] = "IsEnclosedCJKLettersandMonths";
			array[33, 1] = "㈀㌀";
			array[34, 0] = "IsEthiopic";
			array[34, 1] = "ሀᎀ";
			array[35, 0] = "IsGeneralPunctuation";
			array[35, 1] = "\u2000⁰";
			array[36, 0] = "IsGeometricShapes";
			array[36, 1] = "■☀";
			array[37, 0] = "IsGeorgian";
			array[37, 1] = "Ⴀᄀ";
			array[38, 0] = "IsGreek";
			array[38, 1] = "ͰЀ";
			array[39, 0] = "IsGreekExtended";
			array[39, 1] = "ἀ\u2000";
			array[40, 0] = "IsGreekandCoptic";
			array[40, 1] = "ͰЀ";
			array[41, 0] = "IsGujarati";
			array[41, 1] = "\u0a80\u0b00";
			array[42, 0] = "IsGurmukhi";
			array[42, 1] = "\u0a00\u0a80";
			array[43, 0] = "IsHalfwidthandFullwidthForms";
			array[43, 1] = "\uff00\ufff0";
			array[44, 0] = "IsHangulCompatibilityJamo";
			array[44, 1] = "\u3130㆐";
			array[45, 0] = "IsHangulJamo";
			array[45, 1] = "ᄀሀ";
			array[46, 0] = "IsHangulSyllables";
			array[46, 1] = "가ힰ";
			array[47, 0] = "IsHanunoo";
			array[47, 1] = "ᜠᝀ";
			array[48, 0] = "IsHebrew";
			array[48, 1] = "\u0590\u0600";
			array[49, 0] = "IsHighPrivateUseSurrogates";
			array[49, 1] = "\udb80\udc00";
			array[50, 0] = "IsHighSurrogates";
			array[50, 1] = "\ud800\udb80";
			array[51, 0] = "IsHiragana";
			array[51, 1] = "\u3040゠";
			array[52, 0] = "IsIPAExtensions";
			array[52, 1] = "ɐʰ";
			array[53, 0] = "IsIdeographicDescriptionCharacters";
			array[53, 1] = "⿰\u3000";
			array[54, 0] = "IsKanbun";
			array[54, 1] = "㆐ㆠ";
			array[55, 0] = "IsKangxiRadicals";
			array[55, 1] = "⼀\u2fe0";
			array[56, 0] = "IsKannada";
			array[56, 1] = "\u0c80\u0d00";
			array[57, 0] = "IsKatakana";
			array[57, 1] = "゠\u3100";
			array[58, 0] = "IsKatakanaPhoneticExtensions";
			array[58, 1] = "ㇰ㈀";
			array[59, 0] = "IsKhmer";
			array[59, 1] = "ក᠀";
			array[60, 0] = "IsKhmerSymbols";
			array[60, 1] = "᧠ᨀ";
			array[61, 0] = "IsLao";
			array[61, 1] = "\u0e80ༀ";
			array[62, 0] = "IsLatin-1Supplement";
			array[62, 1] = "\u0080Ā";
			array[63, 0] = "IsLatinExtended-A";
			array[63, 1] = "Āƀ";
			array[64, 0] = "IsLatinExtended-B";
			array[64, 1] = "ƀɐ";
			array[65, 0] = "IsLatinExtendedAdditional";
			array[65, 1] = "Ḁἀ";
			array[66, 0] = "IsLetterlikeSymbols";
			array[66, 1] = "℀⅐";
			array[67, 0] = "IsLimbu";
			array[67, 1] = "ᤀᥐ";
			array[68, 0] = "IsLowSurrogates";
			array[68, 1] = "\udc00\ue000";
			array[69, 0] = "IsMalayalam";
			array[69, 1] = "\u0d00\u0d80";
			array[70, 0] = "IsMathematicalOperators";
			array[70, 1] = "∀⌀";
			array[71, 0] = "IsMiscellaneousMathematicalSymbols-A";
			array[71, 1] = "⟀⟰";
			array[72, 0] = "IsMiscellaneousMathematicalSymbols-B";
			array[72, 1] = "⦀⨀";
			array[73, 0] = "IsMiscellaneousSymbols";
			array[73, 1] = "☀✀";
			array[74, 0] = "IsMiscellaneousSymbolsandArrows";
			array[74, 1] = "⬀Ⰰ";
			array[75, 0] = "IsMiscellaneousTechnical";
			array[75, 1] = "⌀␀";
			array[76, 0] = "IsMongolian";
			array[76, 1] = "᠀ᢰ";
			array[77, 0] = "IsMyanmar";
			array[77, 1] = "ကႠ";
			array[78, 0] = "IsNumberForms";
			array[78, 1] = "⅐←";
			array[79, 0] = "IsOgham";
			array[79, 1] = "\u1680ᚠ";
			array[80, 0] = "IsOpticalCharacterRecognition";
			array[80, 1] = "⑀①";
			array[81, 0] = "IsOriya";
			array[81, 1] = "\u0b00\u0b80";
			array[82, 0] = "IsPhoneticExtensions";
			array[82, 1] = "ᴀᶀ";
			array[83, 0] = "IsPrivateUse";
			array[83, 1] = "\ue000豈";
			array[84, 0] = "IsPrivateUseArea";
			array[84, 1] = "\ue000豈";
			array[85, 0] = "IsRunic";
			array[85, 1] = "ᚠᜀ";
			array[86, 0] = "IsSinhala";
			array[86, 1] = "\u0d80\u0e00";
			array[87, 0] = "IsSmallFormVariants";
			array[87, 1] = "﹐ﹰ";
			array[88, 0] = "IsSpacingModifierLetters";
			array[88, 1] = "ʰ\u0300";
			array[89, 0] = "IsSpecials";
			array[89, 1] = "\ufff0";
			array[90, 0] = "IsSuperscriptsandSubscripts";
			array[90, 1] = "⁰₠";
			array[91, 0] = "IsSupplementalArrows-A";
			array[91, 1] = "⟰⠀";
			array[92, 0] = "IsSupplementalArrows-B";
			array[92, 1] = "⤀⦀";
			array[93, 0] = "IsSupplementalMathematicalOperators";
			array[93, 1] = "⨀⬀";
			array[94, 0] = "IsSyriac";
			array[94, 1] = "܀ݐ";
			array[95, 0] = "IsTagalog";
			array[95, 1] = "ᜀᜠ";
			array[96, 0] = "IsTagbanwa";
			array[96, 1] = "ᝠក";
			array[97, 0] = "IsTaiLe";
			array[97, 1] = "ᥐᦀ";
			array[98, 0] = "IsTamil";
			array[98, 1] = "\u0b80\u0c00";
			array[99, 0] = "IsTelugu";
			array[99, 1] = "\u0c00\u0c80";
			array[100, 0] = "IsThaana";
			array[100, 1] = "ހ߀";
			array[101, 0] = "IsThai";
			array[101, 1] = "\u0e00\u0e80";
			array[102, 0] = "IsTibetan";
			array[102, 1] = "ༀက";
			array[103, 0] = "IsUnifiedCanadianAboriginalSyllabics";
			array[103, 1] = "᐀\u1680";
			array[104, 0] = "IsVariationSelectors";
			array[104, 1] = "\ufe00︐";
			array[105, 0] = "IsYiRadicals";
			array[105, 1] = "꒐ꓐ";
			array[106, 0] = "IsYiSyllables";
			array[106, 1] = "ꀀ꒐";
			array[107, 0] = "IsYijingHexagramSymbols";
			array[107, 1] = "䷀一";
			array[108, 0] = "_xmlC";
			array[108, 1] = "-/0;A[_`a{·\u00b8À×Ø÷øĲĴĿŁŉŊſƀǄǍǱǴǶǺȘɐʩʻ\u02c2ː\u02d2\u0300\u0346\u0360\u0362Ά\u038bΌ\u038dΎ\u03a2ΣϏϐϗϚϛϜϝϞϟϠϡϢϴЁЍЎѐёѝў҂\u0483\u0487ҐӅӇӉӋӍӐӬӮӶӸӺԱ\u0557ՙ՚աև\u0591\u05a2\u05a3\u05ba\u05bb־\u05bf׀\u05c1׃\u05c4\u05c5א\u05ebװ׳ءػـ\u0653٠٪\u0670ڸںڿۀۏې۔ە۩\u06eaۮ۰ۺ\u0901ऄअ\u093a\u093c\u094e\u0951\u0955क़।०॰\u0981\u0984অ\u098dএ\u0991ও\u09a9প\u09b1ল\u09b3শ\u09ba\u09bcঽ\u09be\u09c5\u09c7\u09c9\u09cbৎ\u09d7\u09d8ড়\u09deয়\u09e4০৲\u0a02\u0a03ਅ\u0a0bਏ\u0a11ਓ\u0a29ਪ\u0a31ਲ\u0a34ਵ\u0a37ਸ\u0a3a\u0a3c\u0a3d\u0a3e\u0a43\u0a47\u0a49\u0a4b\u0a4eਖ਼\u0a5dਫ਼\u0a5f੦\u0a75\u0a81\u0a84અઌઍ\u0a8eએ\u0a92ઓ\u0aa9પ\u0ab1લ\u0ab4વ\u0aba\u0abc\u0ac6\u0ac7\u0aca\u0acb\u0aceૠૡ૦૰\u0b01\u0b04ଅ\u0b0dଏ\u0b11ଓ\u0b29ପ\u0b31ଲ\u0b34ଶ\u0b3a\u0b3c\u0b44\u0b47\u0b49\u0b4b\u0b4e\u0b56\u0b58ଡ଼\u0b5eୟ\u0b62୦୰\u0b82\u0b84அ\u0b8bஎ\u0b91ஒ\u0b96ங\u0b9bஜ\u0b9dஞ\u0ba0ண\u0ba5ந\u0babமஶஷ\u0bba\u0bbe\u0bc3\u0bc6\u0bc9\u0bca\u0bce\u0bd7\u0bd8௧௰\u0c01\u0c04అ\u0c0dఎ\u0c11ఒ\u0c29పఴవ\u0c3a\u0c3e\u0c45\u0c46\u0c49\u0c4a\u0c4e\u0c55\u0c57ౠ\u0c62౦\u0c70\u0c82\u0c84ಅ\u0c8dಎ\u0c91ಒ\u0ca9ಪ\u0cb4ವ\u0cba\u0cbe\u0cc5\u0cc6\u0cc9\u0cca\u0cce\u0cd5\u0cd7ೞ\u0cdfೠ\u0ce2೦\u0cf0\u0d02\u0d04അ\u0d0dഎ\u0d11ഒഩപഺ\u0d3e\u0d44\u0d46\u0d49\u0d4aൎ\u0d57\u0d58ൠ\u0d62൦൰กฯะ\u0e3bเ๏๐๚ກ\u0e83ຄ\u0e85ງ\u0e89ຊ\u0e8bຍ\u0e8eດ\u0e98ນ\u0ea0ມ\u0ea4ລ\u0ea6ວ\u0ea8ສ\u0eacອຯະ\u0eba\u0ebb\u0ebeເ\u0ec5ໆ\u0ec7\u0ec8\u0ece໐\u0eda\u0f18༚༠༪\u0f35༶\u0f37༸\u0f39༺\u0f3e\u0f48ཉཪ\u0f71྅\u0f86ྌ\u0f90\u0f96\u0f97\u0f98\u0f99\u0fae\u0fb1\u0fb8\u0fb9\u0fbaႠ\u10c6აჷᄀᄁᄂᄄᄅᄈᄉᄊᄋᄍᄎᄓᄼᄽᄾᄿᅀᅁᅌᅍᅎᅏᅐᅑᅔᅖᅙᅚᅟᅢᅣᅤᅥᅦᅧᅨᅩᅪᅭᅯᅲᅴᅵᅶᆞᆟᆨᆩᆫᆬᆮᆰᆷᆹᆺᆻᆼᇃᇫᇬᇰᇱᇹᇺḀẜẠỺἀ\u1f16Ἐ\u1f1eἠ\u1f46Ὀ\u1f4eὐ\u1f58Ὑ\u1f5aὛ\u1f5cὝ\u1f5eὟ\u1f7eᾀ\u1fb5ᾶ\u1fbdι\u1fbfῂ\u1fc5ῆ\u1fcdῐ\u1fd4ῖ\u1fdcῠ\u1fedῲ\u1ff5ῶ\u1ffd\u20d0\u20dd\u20e1\u20e2Ω℧Kℬ℮ℯↀↃ々〆〇〈〡〰〱〶ぁゕ\u3099\u309bゝゟァ・ーヿㄅㄭ一龦가\ud7a4";
			array[109, 0] = "_xmlD";
			array[109, 1] = "0:٠٪۰ۺ०॰০ৰ੦\u0a70૦૰୦୰௧௰౦\u0c70೦\u0cf0൦൰๐๚໐\u0eda༠༪၀၊፩፲០\u17ea᠐\u181a０：";
			array[110, 0] = "_xmlI";
			array[110, 1] = ":;A[_`a{À×Ø÷øĲĴĿŁŉŊſƀǄǍǱǴǶǺȘɐʩʻ\u02c2Ά·Έ\u038bΌ\u038dΎ\u03a2ΣϏϐϗϚϛϜϝϞϟϠϡϢϴЁЍЎѐёѝў҂ҐӅӇӉӋӍӐӬӮӶӸӺԱ\u0557ՙ՚աևא\u05ebװ׳ءػف\u064bٱڸںڿۀۏې۔ە\u06d6ۥ\u06e7अ\u093aऽ\u093eक़\u0962অ\u098dএ\u0991ও\u09a9প\u09b1ল\u09b3শ\u09baড়\u09deয়\u09e2ৰ৲ਅ\u0a0bਏ\u0a11ਓ\u0a29ਪ\u0a31ਲ\u0a34ਵ\u0a37ਸ\u0a3aਖ਼\u0a5dਫ਼\u0a5fੲ\u0a75અઌઍ\u0a8eએ\u0a92ઓ\u0aa9પ\u0ab1લ\u0ab4વ\u0abaઽ\u0abeૠૡଅ\u0b0dଏ\u0b11ଓ\u0b29ପ\u0b31ଲ\u0b34ଶ\u0b3aଽ\u0b3eଡ଼\u0b5eୟ\u0b62அ\u0b8bஎ\u0b91ஒ\u0b96ங\u0b9bஜ\u0b9dஞ\u0ba0ண\u0ba5ந\u0babமஶஷ\u0bbaఅ\u0c0dఎ\u0c11ఒ\u0c29పఴవ\u0c3aౠ\u0c62ಅ\u0c8dಎ\u0c91ಒ\u0ca9ಪ\u0cb4ವ\u0cbaೞ\u0cdfೠ\u0ce2അ\u0d0dഎ\u0d11ഒഩപഺൠ\u0d62กฯะ\u0e31า\u0e34เๆກ\u0e83ຄ\u0e85ງ\u0e89ຊ\u0e8bຍ\u0e8eດ\u0e98ນ\u0ea0ມ\u0ea4ລ\u0ea6ວ\u0ea8ສ\u0eacອຯະ\u0eb1າ\u0eb4ຽ\u0ebeເ\u0ec5ཀ\u0f48ཉཪႠ\u10c6აჷᄀᄁᄂᄄᄅᄈᄉᄊᄋᄍᄎᄓᄼᄽᄾᄿᅀᅁᅌᅍᅎᅏᅐᅑᅔᅖᅙᅚᅟᅢᅣᅤᅥᅦᅧᅨᅩᅪᅭᅯᅲᅴᅵᅶᆞᆟᆨᆩᆫᆬᆮᆰᆷᆹᆺᆻᆼᇃᇫᇬᇰᇱᇹᇺḀẜẠỺἀ\u1f16Ἐ\u1f1eἠ\u1f46Ὀ\u1f4eὐ\u1f58Ὑ\u1f5aὛ\u1f5cὝ\u1f5eὟ\u1f7eᾀ\u1fb5ᾶ\u1fbdι\u1fbfῂ\u1fc5ῆ\u1fcdῐ\u1fd4ῖ\u1fdcῠ\u1fedῲ\u1ff5ῶ\u1ffdΩ℧Kℬ℮ℯↀↃ〇〈〡\u302aぁゕァ・ㄅㄭ一龦가\ud7a4";
			array[111, 0] = "_xmlW";
			array[111, 1] = "$%+,0:<?A[^_`{|}~\u007f¢«¬­®·\u00b8»¼¿ÀȡȢȴɐʮʰ\u02ef\u0300\u0350\u0360ͰʹͶͺͻ\u0384·Έ\u038bΌ\u038dΎ\u03a2ΣϏϐϷЀ\u0487\u0488ӏӐӶӸӺԀԐԱ\u0557ՙ՚ա\u0588\u0591\u05a2\u05a3\u05ba\u05bb־\u05bf׀\u05c1׃\u05c4\u05c5א\u05ebװ׳ءػـ\u0656٠٪ٮ۔ە\u06dd۞ۮ۰ۿܐܭ\u0730\u074bހ\u07b2\u0901ऄअ\u093a\u093c\u094eॐ\u0955क़।०॰\u0981\u0984অ\u098dএ\u0991ও\u09a9প\u09b1ল\u09b3শ\u09ba\u09bcঽ\u09be\u09c5\u09c7\u09c9\u09cbৎ\u09d7\u09d8ড়\u09deয়\u09e4০৻\u0a02\u0a03ਅ\u0a0bਏ\u0a11ਓ\u0a29ਪ\u0a31ਲ\u0a34ਵ\u0a37ਸ\u0a3a\u0a3c\u0a3d\u0a3e\u0a43\u0a47\u0a49\u0a4b\u0a4eਖ਼\u0a5dਫ਼\u0a5f੦\u0a75\u0a81\u0a84અઌઍ\u0a8eએ\u0a92ઓ\u0aa9પ\u0ab1લ\u0ab4વ\u0aba\u0abc\u0ac6\u0ac7\u0aca\u0acb\u0aceૐ\u0ad1ૠૡ૦૰\u0b01\u0b04ଅ\u0b0dଏ\u0b11ଓ\u0b29ପ\u0b31ଲ\u0b34ଶ\u0b3a\u0b3c\u0b44\u0b47\u0b49\u0b4b\u0b4e\u0b56\u0b58ଡ଼\u0b5eୟ\u0b62୦ୱ\u0b82\u0b84அ\u0b8bஎ\u0b91ஒ\u0b96ங\u0b9bஜ\u0b9dஞ\u0ba0ண\u0ba5ந\u0babமஶஷ\u0bba\u0bbe\u0bc3\u0bc6\u0bc9\u0bca\u0bce\u0bd7\u0bd8௧௳\u0c01\u0c04అ\u0c0dఎ\u0c11ఒ\u0c29పఴవ\u0c3a\u0c3e\u0c45\u0c46\u0c49\u0c4a\u0c4e\u0c55\u0c57ౠ\u0c62౦\u0c70\u0c82\u0c84ಅ\u0c8dಎ\u0c91ಒ\u0ca9ಪ\u0cb4ವ\u0cba\u0cbe\u0cc5\u0cc6\u0cc9\u0cca\u0cce\u0cd5\u0cd7ೞ\u0cdfೠ\u0ce2೦\u0cf0\u0d02\u0d04അ\u0d0dഎ\u0d11ഒഩപഺ\u0d3e\u0d44\u0d46\u0d49\u0d4aൎ\u0d57\u0d58ൠ\u0d62൦൰\u0d82\u0d84අ\u0d97ක\u0db2ඳ\u0dbcල\u0dbeව\u0dc7\u0dca\u0dcb\u0dcf\u0dd5\u0dd6\u0dd7\u0dd8\u0de0\u0df2෴ก\u0e3b฿๏๐๚ກ\u0e83ຄ\u0e85ງ\u0e89ຊ\u0e8bຍ\u0e8eດ\u0e98ນ\u0ea0ມ\u0ea4ລ\u0ea6ວ\u0ea8ສ\u0eacອ\u0eba\u0ebb\u0ebeເ\u0ec5ໆ\u0ec7\u0ec8\u0ece໐\u0edaໜໞༀ༄༓༺\u0f3e\u0f48ཉཫ\u0f71྅\u0f86ྌ\u0f90\u0f98\u0f99\u0fbd྾\u0fcd࿏࿐ကဢဣဨဩ\u102b\u102c\u1033\u1036\u103a၀၊ၐၚႠ\u10c6აჹᄀᅚᅟᆣᆨᇺሀሇለቇቈ\u1249ቊ\u124eቐ\u1257ቘ\u1259ቚ\u125eበኇኈ\u1289ኊ\u128eነኯኰ\u12b1ኲ\u12b6ኸ\u12bfዀ\u12c1ዂ\u12c6ወዏዐ\u12d7ዘዯደጏጐ\u1311ጒ\u1316ጘጟጠፇፈ\u135b፩\u137dᎠᏵᐁ᙭ᙯᙷᚁ᚛ᚠ᛫ᛮᛱᜀ\u170dᜎ\u1715ᜠ᜵ᝀ\u1754ᝠ\u176dᝮ\u1771\u1772\u1774ក។ៗ៘៛\u17dd០\u17ea\u180b\u180e᠐\u181aᠠ\u1878ᢀᢪḀẜẠỺἀ\u1f16Ἐ\u1f1eἠ\u1f46Ὀ\u1f4eὐ\u1f58Ὑ\u1f5aὛ\u1f5cὝ\u1f5eὟ\u1f7eᾀ\u1fb5ᾶ\u1fc5ῆ\u1fd4ῖ\u1fdc\u1fdd\u1ff0ῲ\u1ff5ῶ\u1fff⁄⁅⁒⁓⁰\u2072⁴⁽ⁿ₍₠₲\u20d0\u20eb℀℻ℽ⅌⅓ↄ←〈⌫⎴⎷⏏␀\u2427⑀\u244b①⓿─☔☖☘☙♾⚀⚊✁✅✆✊✌✨✩❌❍❎❏❓❖❗❘❟❡❨❶➕➘➰➱➿⟐⟦⟰⦃⦙⧘⧜⧼⧾⬀⺀\u2e9a⺛\u2ef4⼀\u2fd6⿰\u2ffc〄〈〒〔〠〰〱〽〾\u3040ぁ\u3097\u3099゠ァ・ー\u3100ㄅㄭㄱ\u318f㆐ㆸㇰ㈝㈠㉄㉑㉼㉿㋌㋐\u32ff㌀㍷㍻㏞㏠㏿㐀\u4db6一龦ꀀ\ua48d꒐\ua4c7가\ud7a4豈郞侮恵ﬀ\ufb07ﬓ\ufb18יִ\ufb37טּ\ufb3dמּ\ufb3fנּ\ufb42ףּ\ufb45צּ\ufbb2ﯓ﴾ﵐ\ufd90ﶒ\ufdc8ﷰ﷽\ufe00︐\ufe20\ufe24﹢﹣﹤\ufe67﹩﹪ﹰ\ufe75ﹶ\ufefd＄％＋，０：＜？Ａ［\uff3e\uff3f\uff40｛｜｝～｟ｦ\uffbfￂ\uffc8ￊ\uffd0ￒ\uffd8ￚ\uffdd￠\uffe7￨\uffef￼\ufffe";
			RegexCharClass._propTable = array;
			RegexCharClass._lcTable = new RegexCharClass.LowerCaseMapping[]
			{
				new RegexCharClass.LowerCaseMapping('A', 'Z', 1, 32),
				new RegexCharClass.LowerCaseMapping('À', 'Þ', 1, 32),
				new RegexCharClass.LowerCaseMapping('Ā', 'Į', 2, 0),
				new RegexCharClass.LowerCaseMapping('İ', 'İ', 0, 105),
				new RegexCharClass.LowerCaseMapping('Ĳ', 'Ķ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ĺ', 'Ň', 3, 0),
				new RegexCharClass.LowerCaseMapping('Ŋ', 'Ŷ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ÿ', 'Ÿ', 0, 255),
				new RegexCharClass.LowerCaseMapping('Ź', 'Ž', 3, 0),
				new RegexCharClass.LowerCaseMapping('Ɓ', 'Ɓ', 0, 595),
				new RegexCharClass.LowerCaseMapping('Ƃ', 'Ƅ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ɔ', 'Ɔ', 0, 596),
				new RegexCharClass.LowerCaseMapping('Ƈ', 'Ƈ', 0, 392),
				new RegexCharClass.LowerCaseMapping('Ɖ', 'Ɗ', 1, 205),
				new RegexCharClass.LowerCaseMapping('Ƌ', 'Ƌ', 0, 396),
				new RegexCharClass.LowerCaseMapping('Ǝ', 'Ǝ', 0, 477),
				new RegexCharClass.LowerCaseMapping('Ə', 'Ə', 0, 601),
				new RegexCharClass.LowerCaseMapping('Ɛ', 'Ɛ', 0, 603),
				new RegexCharClass.LowerCaseMapping('Ƒ', 'Ƒ', 0, 402),
				new RegexCharClass.LowerCaseMapping('Ɠ', 'Ɠ', 0, 608),
				new RegexCharClass.LowerCaseMapping('Ɣ', 'Ɣ', 0, 611),
				new RegexCharClass.LowerCaseMapping('Ɩ', 'Ɩ', 0, 617),
				new RegexCharClass.LowerCaseMapping('Ɨ', 'Ɨ', 0, 616),
				new RegexCharClass.LowerCaseMapping('Ƙ', 'Ƙ', 0, 409),
				new RegexCharClass.LowerCaseMapping('Ɯ', 'Ɯ', 0, 623),
				new RegexCharClass.LowerCaseMapping('Ɲ', 'Ɲ', 0, 626),
				new RegexCharClass.LowerCaseMapping('Ɵ', 'Ɵ', 0, 629),
				new RegexCharClass.LowerCaseMapping('Ơ', 'Ƥ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ƨ', 'Ƨ', 0, 424),
				new RegexCharClass.LowerCaseMapping('Ʃ', 'Ʃ', 0, 643),
				new RegexCharClass.LowerCaseMapping('Ƭ', 'Ƭ', 0, 429),
				new RegexCharClass.LowerCaseMapping('Ʈ', 'Ʈ', 0, 648),
				new RegexCharClass.LowerCaseMapping('Ư', 'Ư', 0, 432),
				new RegexCharClass.LowerCaseMapping('Ʊ', 'Ʋ', 1, 217),
				new RegexCharClass.LowerCaseMapping('Ƴ', 'Ƶ', 3, 0),
				new RegexCharClass.LowerCaseMapping('Ʒ', 'Ʒ', 0, 658),
				new RegexCharClass.LowerCaseMapping('Ƹ', 'Ƹ', 0, 441),
				new RegexCharClass.LowerCaseMapping('Ƽ', 'Ƽ', 0, 445),
				new RegexCharClass.LowerCaseMapping('Ǆ', 'ǅ', 0, 454),
				new RegexCharClass.LowerCaseMapping('Ǉ', 'ǈ', 0, 457),
				new RegexCharClass.LowerCaseMapping('Ǌ', 'ǋ', 0, 460),
				new RegexCharClass.LowerCaseMapping('Ǎ', 'Ǜ', 3, 0),
				new RegexCharClass.LowerCaseMapping('Ǟ', 'Ǯ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ǳ', 'ǲ', 0, 499),
				new RegexCharClass.LowerCaseMapping('Ǵ', 'Ǵ', 0, 501),
				new RegexCharClass.LowerCaseMapping('Ǻ', 'Ȗ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ά', 'Ά', 0, 940),
				new RegexCharClass.LowerCaseMapping('Έ', 'Ί', 1, 37),
				new RegexCharClass.LowerCaseMapping('Ό', 'Ό', 0, 972),
				new RegexCharClass.LowerCaseMapping('Ύ', 'Ώ', 1, 63),
				new RegexCharClass.LowerCaseMapping('Α', 'Ϋ', 1, 32),
				new RegexCharClass.LowerCaseMapping('Ϣ', 'Ϯ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ё', 'Џ', 1, 80),
				new RegexCharClass.LowerCaseMapping('А', 'Я', 1, 32),
				new RegexCharClass.LowerCaseMapping('Ѡ', 'Ҁ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ґ', 'Ҿ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ӂ', 'Ӄ', 3, 0),
				new RegexCharClass.LowerCaseMapping('Ӈ', 'Ӈ', 0, 1224),
				new RegexCharClass.LowerCaseMapping('Ӌ', 'Ӌ', 0, 1228),
				new RegexCharClass.LowerCaseMapping('Ӑ', 'Ӫ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ӯ', 'Ӵ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ӹ', 'Ӹ', 0, 1273),
				new RegexCharClass.LowerCaseMapping('Ա', 'Ֆ', 1, 48),
				new RegexCharClass.LowerCaseMapping('Ⴀ', 'Ⴥ', 1, 48),
				new RegexCharClass.LowerCaseMapping('Ḁ', 'Ỹ', 2, 0),
				new RegexCharClass.LowerCaseMapping('Ἀ', 'Ἇ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ἐ', '\u1f1f', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ἠ', 'Ἧ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ἰ', 'Ἷ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ὀ', 'Ὅ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ὑ', 'Ὑ', 0, 8017),
				new RegexCharClass.LowerCaseMapping('Ὓ', 'Ὓ', 0, 8019),
				new RegexCharClass.LowerCaseMapping('Ὕ', 'Ὕ', 0, 8021),
				new RegexCharClass.LowerCaseMapping('Ὗ', 'Ὗ', 0, 8023),
				new RegexCharClass.LowerCaseMapping('Ὠ', 'Ὧ', 1, -8),
				new RegexCharClass.LowerCaseMapping('ᾈ', 'ᾏ', 1, -8),
				new RegexCharClass.LowerCaseMapping('ᾘ', 'ᾟ', 1, -8),
				new RegexCharClass.LowerCaseMapping('ᾨ', 'ᾯ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ᾰ', 'Ᾱ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ὰ', 'Ά', 1, -74),
				new RegexCharClass.LowerCaseMapping('ᾼ', 'ᾼ', 0, 8115),
				new RegexCharClass.LowerCaseMapping('Ὲ', 'Ή', 1, -86),
				new RegexCharClass.LowerCaseMapping('ῌ', 'ῌ', 0, 8131),
				new RegexCharClass.LowerCaseMapping('Ῐ', 'Ῑ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ὶ', 'Ί', 1, -100),
				new RegexCharClass.LowerCaseMapping('Ῠ', 'Ῡ', 1, -8),
				new RegexCharClass.LowerCaseMapping('Ὺ', 'Ύ', 1, -112),
				new RegexCharClass.LowerCaseMapping('Ῥ', 'Ῥ', 0, 8165),
				new RegexCharClass.LowerCaseMapping('Ὸ', 'Ό', 1, -128),
				new RegexCharClass.LowerCaseMapping('Ὼ', 'Ώ', 1, -126),
				new RegexCharClass.LowerCaseMapping('ῼ', 'ῼ', 0, 8179),
				new RegexCharClass.LowerCaseMapping('Ⅰ', 'Ⅿ', 1, 16),
				new RegexCharClass.LowerCaseMapping('Ⓐ', 'ⓐ', 1, 26),
				new RegexCharClass.LowerCaseMapping('Ａ', 'Ｚ', 1, 32)
			};
			Dictionary<string, string> dictionary = new Dictionary<string, string>(32);
			char[] array2 = new char[9];
			StringBuilder stringBuilder = new StringBuilder(11);
			stringBuilder.Append('\0');
			array2[0] = '\0';
			array2[1] = '\u000f';
			dictionary["Cc"] = array2[1].ToString();
			array2[2] = '\u0010';
			dictionary["Cf"] = array2[2].ToString();
			array2[3] = '\u001e';
			dictionary["Cn"] = array2[3].ToString();
			array2[4] = '\u0012';
			dictionary["Co"] = array2[4].ToString();
			array2[5] = '\u0011';
			dictionary["Cs"] = array2[5].ToString();
			array2[6] = '\0';
			dictionary["C"] = new string(array2, 0, 7);
			array2[1] = '\u0002';
			dictionary["Ll"] = array2[1].ToString();
			array2[2] = '\u0004';
			dictionary["Lm"] = array2[2].ToString();
			array2[3] = '\u0005';
			dictionary["Lo"] = array2[3].ToString();
			array2[4] = '\u0003';
			dictionary["Lt"] = array2[4].ToString();
			array2[5] = '\u0001';
			dictionary["Lu"] = array2[5].ToString();
			dictionary["L"] = new string(array2, 0, 7);
			stringBuilder.Append(new string(array2, 1, 5));
			dictionary[RegexCharClass.InternalRegexIgnoreCase] = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}", new object[]
			{
				'\0',
				array2[1],
				array2[4],
				array2[5],
				array2[6]
			});
			array2[1] = '\a';
			dictionary["Mc"] = array2[1].ToString();
			array2[2] = '\b';
			dictionary["Me"] = array2[2].ToString();
			array2[3] = '\u0006';
			dictionary["Mn"] = array2[3].ToString();
			array2[4] = '\0';
			dictionary["M"] = new string(array2, 0, 5);
			stringBuilder.Append(array2[3]);
			array2[1] = '\t';
			dictionary["Nd"] = array2[1].ToString();
			array2[2] = '\n';
			dictionary["Nl"] = array2[2].ToString();
			array2[3] = '\v';
			dictionary["No"] = array2[3].ToString();
			dictionary["N"] = new string(array2, 0, 5);
			stringBuilder.Append(array2[1]);
			array2[1] = '\u0013';
			dictionary["Pc"] = array2[1].ToString();
			array2[2] = '\u0014';
			dictionary["Pd"] = array2[2].ToString();
			array2[3] = '\u0016';
			dictionary["Pe"] = array2[3].ToString();
			array2[4] = '\u0019';
			dictionary["Po"] = array2[4].ToString();
			array2[5] = '\u0015';
			dictionary["Ps"] = array2[5].ToString();
			array2[6] = '\u0018';
			dictionary["Pf"] = array2[6].ToString();
			array2[7] = '\u0017';
			dictionary["Pi"] = array2[7].ToString();
			array2[8] = '\0';
			dictionary["P"] = new string(array2, 0, 9);
			stringBuilder.Append(array2[1]);
			array2[1] = '\u001b';
			dictionary["Sc"] = array2[1].ToString();
			array2[2] = '\u001c';
			dictionary["Sk"] = array2[2].ToString();
			array2[3] = '\u001a';
			dictionary["Sm"] = array2[3].ToString();
			array2[4] = '\u001d';
			dictionary["So"] = array2[4].ToString();
			array2[5] = '\0';
			dictionary["S"] = new string(array2, 0, 6);
			array2[1] = '\r';
			dictionary["Zl"] = array2[1].ToString();
			array2[2] = '\u000e';
			dictionary["Zp"] = array2[2].ToString();
			array2[3] = '\f';
			dictionary["Zs"] = array2[3].ToString();
			array2[4] = '\0';
			dictionary["Z"] = new string(array2, 0, 5);
			stringBuilder.Append('\0');
			RegexCharClass.Word = stringBuilder.ToString();
			RegexCharClass.NotWord = RegexCharClass.NegateCategory(RegexCharClass.Word);
			RegexCharClass.SpaceClass = "\0\0\u0001" + RegexCharClass.Space;
			RegexCharClass.NotSpaceClass = "\u0001\0\u0001" + RegexCharClass.Space;
			RegexCharClass.WordClass = "\0\0" + ((char)RegexCharClass.Word.Length).ToString() + RegexCharClass.Word;
			RegexCharClass.NotWordClass = "\u0001\0" + ((char)RegexCharClass.Word.Length).ToString() + RegexCharClass.Word;
			RegexCharClass.DigitClass = "\0\0\u0001\t";
			RegexCharClass.NotDigitClass = "\0\0\u0001\ufff7";
			RegexCharClass._definedCategories = dictionary;
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x000FF1F3 File Offset: 0x000FD3F3
		internal RegexCharClass()
		{
			this._rangelist = new List<RegexCharClass.SingleRange>(6);
			this._canonical = true;
			this._categories = new StringBuilder();
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x000FF219 File Offset: 0x000FD419
		private RegexCharClass(bool negate, List<RegexCharClass.SingleRange> ranges, StringBuilder categories, RegexCharClass subtraction)
		{
			this._rangelist = ranges;
			this._categories = categories;
			this._canonical = true;
			this._negate = negate;
			this._subtractor = subtraction;
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06003DF2 RID: 15858 RVA: 0x000FF245 File Offset: 0x000FD445
		internal bool CanMerge
		{
			get
			{
				return !this._negate && this._subtractor == null;
			}
		}

		// Token: 0x17000EB0 RID: 3760
		// (set) Token: 0x06003DF3 RID: 15859 RVA: 0x000FF25A File Offset: 0x000FD45A
		internal bool Negate
		{
			set
			{
				this._negate = value;
			}
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x000FF263 File Offset: 0x000FD463
		internal void AddChar(char c)
		{
			this.AddRange(c, c);
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x000FF270 File Offset: 0x000FD470
		internal void AddCharClass(RegexCharClass cc)
		{
			if (!cc._canonical)
			{
				this._canonical = false;
			}
			else if (this._canonical && this.RangeCount() > 0 && cc.RangeCount() > 0 && cc.GetRangeAt(0)._first <= this.GetRangeAt(this.RangeCount() - 1)._last)
			{
				this._canonical = false;
			}
			for (int i = 0; i < cc.RangeCount(); i++)
			{
				this._rangelist.Add(cc.GetRangeAt(i));
			}
			this._categories.Append(cc._categories.ToString());
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x000FF30C File Offset: 0x000FD50C
		private void AddSet(string set)
		{
			if (this._canonical && this.RangeCount() > 0 && set.Length > 0 && set[0] <= this.GetRangeAt(this.RangeCount() - 1)._last)
			{
				this._canonical = false;
			}
			int i;
			for (i = 0; i < set.Length - 1; i += 2)
			{
				this._rangelist.Add(new RegexCharClass.SingleRange(set[i], set[i + 1] - '\u0001'));
			}
			if (i < set.Length)
			{
				this._rangelist.Add(new RegexCharClass.SingleRange(set[i], char.MaxValue));
			}
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x000FF3B1 File Offset: 0x000FD5B1
		internal void AddSubtraction(RegexCharClass sub)
		{
			this._subtractor = sub;
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x000FF3BC File Offset: 0x000FD5BC
		internal void AddRange(char first, char last)
		{
			this._rangelist.Add(new RegexCharClass.SingleRange(first, last));
			if (this._canonical && this._rangelist.Count > 0 && first <= this._rangelist[this._rangelist.Count - 1]._last)
			{
				this._canonical = false;
			}
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x000FF418 File Offset: 0x000FD618
		internal void AddCategoryFromName(string categoryName, bool invert, bool caseInsensitive, string pattern)
		{
			string text;
			RegexCharClass._definedCategories.TryGetValue(categoryName, out text);
			if (text != null && !categoryName.Equals(RegexCharClass.InternalRegexIgnoreCase))
			{
				string text2 = text;
				if (caseInsensitive && (categoryName.Equals("Ll") || categoryName.Equals("Lu") || categoryName.Equals("Lt")))
				{
					text2 = RegexCharClass._definedCategories[RegexCharClass.InternalRegexIgnoreCase];
				}
				if (invert)
				{
					text2 = RegexCharClass.NegateCategory(text2);
				}
				this._categories.Append(text2);
				return;
			}
			this.AddSet(RegexCharClass.SetFromProperty(categoryName, invert, pattern));
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x000FF4A6 File Offset: 0x000FD6A6
		private void AddCategory(string category)
		{
			this._categories.Append(category);
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x000FF4B8 File Offset: 0x000FD6B8
		internal void AddLowercase(CultureInfo culture)
		{
			this._canonical = false;
			int i = 0;
			int count = this._rangelist.Count;
			while (i < count)
			{
				RegexCharClass.SingleRange singleRange = this._rangelist[i];
				if (singleRange._first == singleRange._last)
				{
					singleRange._first = (singleRange._last = char.ToLower(singleRange._first, culture));
				}
				else
				{
					this.AddLowercaseRange(singleRange._first, singleRange._last, culture);
				}
				i++;
			}
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x000FF530 File Offset: 0x000FD730
		private void AddLowercaseRange(char chMin, char chMax, CultureInfo culture)
		{
			int i = 0;
			int num = RegexCharClass._lcTable.Length;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				if (RegexCharClass._lcTable[num2]._chMax < chMin)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2;
				}
			}
			if (i >= RegexCharClass._lcTable.Length)
			{
				return;
			}
			RegexCharClass.LowerCaseMapping lowerCaseMapping;
			while (i < RegexCharClass._lcTable.Length && (lowerCaseMapping = RegexCharClass._lcTable[i])._chMin <= chMax)
			{
				char c;
				if ((c = lowerCaseMapping._chMin) < chMin)
				{
					c = chMin;
				}
				char c2;
				if ((c2 = lowerCaseMapping._chMax) > chMax)
				{
					c2 = chMax;
				}
				switch (lowerCaseMapping._lcOp)
				{
				case 0:
					c = (char)lowerCaseMapping._data;
					c2 = (char)lowerCaseMapping._data;
					break;
				case 1:
					c += (char)lowerCaseMapping._data;
					c2 += (char)lowerCaseMapping._data;
					break;
				case 2:
					c |= '\u0001';
					c2 |= '\u0001';
					break;
				case 3:
					c += c & '\u0001';
					c2 += c2 & '\u0001';
					break;
				}
				if (c < chMin || c2 > chMax)
				{
					this.AddRange(c, c2);
				}
				i++;
			}
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x000FF647 File Offset: 0x000FD847
		internal void AddWord(bool ecma, bool negate)
		{
			if (negate)
			{
				if (ecma)
				{
					this.AddSet("\00:A[_`a{İı");
					return;
				}
				this.AddCategory(RegexCharClass.NotWord);
				return;
			}
			else
			{
				if (ecma)
				{
					this.AddSet("0:A[_`a{İı");
					return;
				}
				this.AddCategory(RegexCharClass.Word);
				return;
			}
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x000FF681 File Offset: 0x000FD881
		internal void AddSpace(bool ecma, bool negate)
		{
			if (negate)
			{
				if (ecma)
				{
					this.AddSet("\0\t\u000e !");
					return;
				}
				this.AddCategory(RegexCharClass.NotSpace);
				return;
			}
			else
			{
				if (ecma)
				{
					this.AddSet("\t\u000e !");
					return;
				}
				this.AddCategory(RegexCharClass.Space);
				return;
			}
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x000FF6BB File Offset: 0x000FD8BB
		internal void AddDigit(bool ecma, bool negate, string pattern)
		{
			if (!ecma)
			{
				this.AddCategoryFromName("Nd", negate, false, pattern);
				return;
			}
			if (negate)
			{
				this.AddSet("\00:");
				return;
			}
			this.AddSet("0:");
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x000FF6EC File Offset: 0x000FD8EC
		internal static string ConvertOldStringsToClass(string set, string category)
		{
			StringBuilder stringBuilder = new StringBuilder(set.Length + category.Length + 3);
			if (set.Length >= 2 && set[0] == '\0' && set[1] == '\0')
			{
				stringBuilder.Append('\u0001');
				stringBuilder.Append((char)(set.Length - 2));
				stringBuilder.Append((char)category.Length);
				stringBuilder.Append(set.Substring(2));
			}
			else
			{
				stringBuilder.Append('\0');
				stringBuilder.Append((char)set.Length);
				stringBuilder.Append((char)category.Length);
				stringBuilder.Append(set);
			}
			stringBuilder.Append(category);
			return stringBuilder.ToString();
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x000FF799 File Offset: 0x000FD999
		internal static char SingletonChar(string set)
		{
			return set[3];
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x000FF7A2 File Offset: 0x000FD9A2
		internal static bool IsMergeable(string charClass)
		{
			return !RegexCharClass.IsNegated(charClass) && !RegexCharClass.IsSubtraction(charClass);
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x000FF7B7 File Offset: 0x000FD9B7
		internal static bool IsEmpty(string charClass)
		{
			return charClass[2] == '\0' && charClass[0] == '\0' && charClass[1] == '\0' && !RegexCharClass.IsSubtraction(charClass);
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x000FF7E0 File Offset: 0x000FD9E0
		internal static bool IsSingleton(string set)
		{
			return set[0] == '\0' && set[2] == '\0' && set[1] == '\u0002' && !RegexCharClass.IsSubtraction(set) && (set[3] == char.MaxValue || set[3] + '\u0001' == set[4]);
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x000FF834 File Offset: 0x000FDA34
		internal static bool IsSingletonInverse(string set)
		{
			return set[0] == '\u0001' && set[2] == '\0' && set[1] == '\u0002' && !RegexCharClass.IsSubtraction(set) && (set[3] == char.MaxValue || set[3] + '\u0001' == set[4]);
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x000FF889 File Offset: 0x000FDA89
		private static bool IsSubtraction(string charClass)
		{
			return charClass.Length > (int)('\u0003' + charClass[1] + charClass[2]);
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x000FF8A4 File Offset: 0x000FDAA4
		internal static bool IsNegated(string set)
		{
			return set != null && set[0] == '\u0001';
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x000FF8B5 File Offset: 0x000FDAB5
		internal static bool IsECMAWordChar(char ch)
		{
			return RegexCharClass.CharInClass(ch, "\0\n\00:A[_`a{İı");
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x000FF8C2 File Offset: 0x000FDAC2
		internal static bool IsWordChar(char ch)
		{
			return RegexCharClass.CharInClass(ch, RegexCharClass.WordClass) || ch == '\u200d' || ch == '\u200c';
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x000FF8E3 File Offset: 0x000FDAE3
		internal static bool CharInClass(char ch, string set)
		{
			return RegexCharClass.CharInClassRecursive(ch, set, 0);
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x000FF8F0 File Offset: 0x000FDAF0
		internal static bool CharInClassRecursive(char ch, string set, int start)
		{
			int num = (int)set[start + 1];
			int num2 = (int)set[start + 2];
			int num3 = start + 3 + num + num2;
			bool flag = false;
			if (set.Length > num3)
			{
				flag = RegexCharClass.CharInClassRecursive(ch, set, num3);
			}
			bool flag2 = RegexCharClass.CharInClassInternal(ch, set, start, num, num2);
			if (set[start] == '\u0001')
			{
				flag2 = !flag2;
			}
			return flag2 && !flag;
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x000FF954 File Offset: 0x000FDB54
		private static bool CharInClassInternal(char ch, string set, int start, int mySetLength, int myCategoryLength)
		{
			int num = start + 3;
			int num2 = num + mySetLength;
			while (num != num2)
			{
				int num3 = (num + num2) / 2;
				if (ch < set[num3])
				{
					num2 = num3;
				}
				else
				{
					num = num3 + 1;
				}
			}
			return (num & 1) == (start & 1) || (myCategoryLength != 0 && RegexCharClass.CharInCategory(ch, set, start, mySetLength, myCategoryLength));
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x000FF9A4 File Offset: 0x000FDBA4
		private static bool CharInCategory(char ch, string set, int start, int mySetLength, int myCategoryLength)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(ch);
			int i = start + 3 + mySetLength;
			int num = i + myCategoryLength;
			while (i < num)
			{
				int num2 = (int)((short)set[i]);
				if (num2 == 0)
				{
					if (RegexCharClass.CharInCategoryGroup(ch, unicodeCategory, set, ref i))
					{
						return true;
					}
				}
				else if (num2 > 0)
				{
					if (num2 == 100)
					{
						if (char.IsWhiteSpace(ch))
						{
							return true;
						}
						i++;
						continue;
					}
					else
					{
						num2--;
						if (unicodeCategory == (UnicodeCategory)num2)
						{
							return true;
						}
					}
				}
				else if (num2 == -100)
				{
					if (!char.IsWhiteSpace(ch))
					{
						return true;
					}
					i++;
					continue;
				}
				else
				{
					num2 = -1 - num2;
					if (unicodeCategory != (UnicodeCategory)num2)
					{
						return true;
					}
				}
				i++;
			}
			return false;
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x000FFA2C File Offset: 0x000FDC2C
		private static bool CharInCategoryGroup(char ch, UnicodeCategory chcategory, string category, ref int i)
		{
			i++;
			int num = (int)((short)category[i]);
			if (num > 0)
			{
				bool flag = false;
				while (num != 0)
				{
					if (!flag)
					{
						num--;
						if (chcategory == (UnicodeCategory)num)
						{
							flag = true;
						}
					}
					i++;
					num = (int)((short)category[i]);
				}
				return flag;
			}
			bool flag2 = true;
			while (num != 0)
			{
				if (flag2)
				{
					num = -1 - num;
					if (chcategory == (UnicodeCategory)num)
					{
						flag2 = false;
					}
				}
				i++;
				num = (int)((short)category[i]);
			}
			return flag2;
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x000FFA98 File Offset: 0x000FDC98
		private static string NegateCategory(string category)
		{
			if (category == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(category.Length);
			foreach (short num in category)
			{
				stringBuilder.Append((char)(-(char)num));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x000FFAE0 File Offset: 0x000FDCE0
		internal static RegexCharClass Parse(string charClass)
		{
			return RegexCharClass.ParseRecursive(charClass, 0);
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x000FFAEC File Offset: 0x000FDCEC
		private static RegexCharClass ParseRecursive(string charClass, int start)
		{
			int num = (int)charClass[start + 1];
			int num2 = (int)charClass[start + 2];
			int num3 = start + 3 + num + num2;
			List<RegexCharClass.SingleRange> list = new List<RegexCharClass.SingleRange>(num);
			int i = start + 3;
			int num4 = i + num;
			while (i < num4)
			{
				char c = charClass[i];
				i++;
				char c2;
				if (i < num4)
				{
					c2 = charClass[i] - '\u0001';
				}
				else
				{
					c2 = char.MaxValue;
				}
				i++;
				list.Add(new RegexCharClass.SingleRange(c, c2));
			}
			RegexCharClass regexCharClass = null;
			if (charClass.Length > num3)
			{
				regexCharClass = RegexCharClass.ParseRecursive(charClass, num3);
			}
			return new RegexCharClass(charClass[start] == '\u0001', list, new StringBuilder(charClass.Substring(num4, num2)), regexCharClass);
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x000FFBA5 File Offset: 0x000FDDA5
		private int RangeCount()
		{
			return this._rangelist.Count;
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x000FFBB4 File Offset: 0x000FDDB4
		internal string ToStringClass()
		{
			if (!this._canonical)
			{
				this.Canonicalize();
			}
			int num = this._rangelist.Count * 2;
			StringBuilder stringBuilder = new StringBuilder(num + this._categories.Length + 3);
			int num2;
			if (this._negate)
			{
				num2 = 1;
			}
			else
			{
				num2 = 0;
			}
			stringBuilder.Append((char)num2);
			stringBuilder.Append((char)num);
			stringBuilder.Append((char)this._categories.Length);
			for (int i = 0; i < this._rangelist.Count; i++)
			{
				RegexCharClass.SingleRange singleRange = this._rangelist[i];
				stringBuilder.Append(singleRange._first);
				if (singleRange._last != '\uffff')
				{
					stringBuilder.Append(singleRange._last + '\u0001');
				}
			}
			stringBuilder[1] = (char)(stringBuilder.Length - 3);
			stringBuilder.Append(this._categories);
			if (this._subtractor != null)
			{
				stringBuilder.Append(this._subtractor.ToStringClass());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x000FFCB3 File Offset: 0x000FDEB3
		private RegexCharClass.SingleRange GetRangeAt(int i)
		{
			return this._rangelist[i];
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x000FFCC4 File Offset: 0x000FDEC4
		private void Canonicalize()
		{
			this._canonical = true;
			this._rangelist.Sort(0, this._rangelist.Count, new RegexCharClass.SingleRangeComparer());
			if (this._rangelist.Count > 1)
			{
				bool flag = false;
				int num = 1;
				int num2 = 0;
				for (;;)
				{
					IL_3B:
					char c = this._rangelist[num2]._last;
					while (num != this._rangelist.Count && c != '\uffff')
					{
						RegexCharClass.SingleRange singleRange;
						if ((singleRange = this._rangelist[num])._first <= c + '\u0001')
						{
							if (c < singleRange._last)
							{
								c = singleRange._last;
							}
							num++;
						}
						else
						{
							IL_96:
							this._rangelist[num2]._last = c;
							num2++;
							if (!flag)
							{
								if (num2 < num)
								{
									this._rangelist[num2] = this._rangelist[num];
								}
								num++;
								goto IL_3B;
							}
							goto IL_D5;
						}
					}
					flag = true;
					goto IL_96;
				}
				IL_D5:
				this._rangelist.RemoveRange(num2, this._rangelist.Count - num2);
			}
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x000FFDC0 File Offset: 0x000FDFC0
		private static string SetFromProperty(string capname, bool invert, string pattern)
		{
			int num = 0;
			int num2 = RegexCharClass._propTable.GetLength(0);
			while (num != num2)
			{
				int num3 = (num + num2) / 2;
				int num4 = string.Compare(capname, RegexCharClass._propTable[num3, 0], StringComparison.Ordinal);
				if (num4 < 0)
				{
					num2 = num3;
				}
				else if (num4 > 0)
				{
					num = num3 + 1;
				}
				else
				{
					string text = RegexCharClass._propTable[num3, 1];
					if (!invert)
					{
						return text;
					}
					if (text[0] == '\0')
					{
						return text.Substring(1);
					}
					return "\0" + text;
				}
			}
			throw new ArgumentException(SR.GetString("MakeException", new object[]
			{
				pattern,
				SR.GetString("UnknownProperty", new object[] { capname })
			}));
		}

		// Token: 0x04002CEC RID: 11500
		private List<RegexCharClass.SingleRange> _rangelist;

		// Token: 0x04002CED RID: 11501
		private StringBuilder _categories;

		// Token: 0x04002CEE RID: 11502
		private bool _canonical;

		// Token: 0x04002CEF RID: 11503
		private bool _negate;

		// Token: 0x04002CF0 RID: 11504
		private RegexCharClass _subtractor;

		// Token: 0x04002CF1 RID: 11505
		private const int FLAGS = 0;

		// Token: 0x04002CF2 RID: 11506
		private const int SETLENGTH = 1;

		// Token: 0x04002CF3 RID: 11507
		private const int CATEGORYLENGTH = 2;

		// Token: 0x04002CF4 RID: 11508
		private const int SETSTART = 3;

		// Token: 0x04002CF5 RID: 11509
		private const char Nullchar = '\0';

		// Token: 0x04002CF6 RID: 11510
		private const char Lastchar = '\uffff';

		// Token: 0x04002CF7 RID: 11511
		private const char GroupChar = '\0';

		// Token: 0x04002CF8 RID: 11512
		private const short SpaceConst = 100;

		// Token: 0x04002CF9 RID: 11513
		private const short NotSpaceConst = -100;

		// Token: 0x04002CFA RID: 11514
		private const char ZeroWidthJoiner = '\u200d';

		// Token: 0x04002CFB RID: 11515
		private const char ZeroWidthNonJoiner = '\u200c';

		// Token: 0x04002CFC RID: 11516
		private static readonly string InternalRegexIgnoreCase = "__InternalRegexIgnoreCase__";

		// Token: 0x04002CFD RID: 11517
		private static readonly string Space = "d";

		// Token: 0x04002CFE RID: 11518
		private static readonly string NotSpace = RegexCharClass.NegateCategory(RegexCharClass.Space);

		// Token: 0x04002CFF RID: 11519
		private static readonly string Word;

		// Token: 0x04002D00 RID: 11520
		private static readonly string NotWord;

		// Token: 0x04002D01 RID: 11521
		internal static readonly string SpaceClass;

		// Token: 0x04002D02 RID: 11522
		internal static readonly string NotSpaceClass;

		// Token: 0x04002D03 RID: 11523
		internal static readonly string WordClass;

		// Token: 0x04002D04 RID: 11524
		internal static readonly string NotWordClass;

		// Token: 0x04002D05 RID: 11525
		internal static readonly string DigitClass;

		// Token: 0x04002D06 RID: 11526
		internal static readonly string NotDigitClass;

		// Token: 0x04002D07 RID: 11527
		private const string ECMASpaceSet = "\t\u000e !";

		// Token: 0x04002D08 RID: 11528
		private const string NotECMASpaceSet = "\0\t\u000e !";

		// Token: 0x04002D09 RID: 11529
		private const string ECMAWordSet = "0:A[_`a{İı";

		// Token: 0x04002D0A RID: 11530
		private const string NotECMAWordSet = "\00:A[_`a{İı";

		// Token: 0x04002D0B RID: 11531
		private const string ECMADigitSet = "0:";

		// Token: 0x04002D0C RID: 11532
		private const string NotECMADigitSet = "\00:";

		// Token: 0x04002D0D RID: 11533
		internal const string ECMASpaceClass = "\0\u0004\0\t\u000e !";

		// Token: 0x04002D0E RID: 11534
		internal const string NotECMASpaceClass = "\u0001\u0004\0\t\u000e !";

		// Token: 0x04002D0F RID: 11535
		internal const string ECMAWordClass = "\0\n\00:A[_`a{İı";

		// Token: 0x04002D10 RID: 11536
		internal const string NotECMAWordClass = "\u0001\n\00:A[_`a{İı";

		// Token: 0x04002D11 RID: 11537
		internal const string ECMADigitClass = "\0\u0002\00:";

		// Token: 0x04002D12 RID: 11538
		internal const string NotECMADigitClass = "\u0001\u0002\00:";

		// Token: 0x04002D13 RID: 11539
		internal const string AnyClass = "\0\u0001\0\0";

		// Token: 0x04002D14 RID: 11540
		internal const string EmptyClass = "\0\0\0";

		// Token: 0x04002D15 RID: 11541
		private static Dictionary<string, string> _definedCategories;

		// Token: 0x04002D16 RID: 11542
		private static readonly string[,] _propTable;

		// Token: 0x04002D17 RID: 11543
		private const int LowercaseSet = 0;

		// Token: 0x04002D18 RID: 11544
		private const int LowercaseAdd = 1;

		// Token: 0x04002D19 RID: 11545
		private const int LowercaseBor = 2;

		// Token: 0x04002D1A RID: 11546
		private const int LowercaseBad = 3;

		// Token: 0x04002D1B RID: 11547
		private static readonly RegexCharClass.LowerCaseMapping[] _lcTable;

		// Token: 0x020008B2 RID: 2226
		private struct LowerCaseMapping
		{
			// Token: 0x06004606 RID: 17926 RVA: 0x001243C4 File Offset: 0x001225C4
			internal LowerCaseMapping(char chMin, char chMax, int lcOp, int data)
			{
				this._chMin = chMin;
				this._chMax = chMax;
				this._lcOp = lcOp;
				this._data = data;
			}

			// Token: 0x04003B3F RID: 15167
			internal char _chMin;

			// Token: 0x04003B40 RID: 15168
			internal char _chMax;

			// Token: 0x04003B41 RID: 15169
			internal int _lcOp;

			// Token: 0x04003B42 RID: 15170
			internal int _data;
		}

		// Token: 0x020008B3 RID: 2227
		private sealed class SingleRangeComparer : IComparer<RegexCharClass.SingleRange>
		{
			// Token: 0x06004607 RID: 17927 RVA: 0x001243E3 File Offset: 0x001225E3
			public int Compare(RegexCharClass.SingleRange x, RegexCharClass.SingleRange y)
			{
				if (x._first < y._first)
				{
					return -1;
				}
				if (x._first <= y._first)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x020008B4 RID: 2228
		private sealed class SingleRange
		{
			// Token: 0x06004609 RID: 17929 RVA: 0x0012440E File Offset: 0x0012260E
			internal SingleRange(char first, char last)
			{
				this._first = first;
				this._last = last;
			}

			// Token: 0x04003B43 RID: 15171
			internal char _first;

			// Token: 0x04003B44 RID: 15172
			internal char _last;
		}
	}
}
