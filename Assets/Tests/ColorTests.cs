using System;
using MolecularLib.Helpers;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MolecularLibTests
{
    public class ColorTests
    {
        private readonly Color32 aStringColor = new Color32(254, 254, 189, 255);
        private readonly Color32 abStringColor = new Color32(254, 254, 71, 255);
        private readonly Color32 abcStringColor = new Color32(189, 192, 191, 255);
        private readonly Color32 abcdStringColor = new Color32(71, 235, 254, 255);
        
        [Test]
        public void ColorFromString()
        {
            var c0Char = "".ToColor();
            var c1Char = "a".ToColor();
            var c2Char = "ab".ToColor();
            var c3Char = "abc".ToColor();
            var c4Char = "abcd".ToColor();
            
            Assert.AreEqual(Color.white, c0Char);
            Assert.True(c1Char != c2Char && c1Char != c3Char && c1Char != c4Char);
            Assert.True(c2Char != c3Char && c2Char != c4Char);
            Assert.True(c3Char != c4Char);
            
            Assert.AreEqual(aStringColor, c1Char.ToColor32());
            Assert.AreEqual(abStringColor, c2Char.ToColor32());
            Assert.AreEqual(abcStringColor, c3Char.ToColor32());
            Assert.AreEqual(abcdStringColor, c4Char.ToColor32());
        }
        
        [Test]
        public void RandomColor()
        {
            var c1 = ColorHelper.Random();
            var c2 = ColorHelper.Random();
            var c3 = ColorHelper.Random();
            var c4 = ColorHelper.Random();
            var c5 = ColorHelper.Random();
            var c6 = ColorHelper.Random();
            var c7 = ColorHelper.Random();
            var c8 = ColorHelper.Random();
            
            Assert.True(c1 != c2 || c1 != c3 || c1 != c4 || c1 != c5 || c1 != c6 || c1 != c7 || c1 != c8);
        }
        
        [Test]
        public void RandomLimitedColor()
        {
            var v1 = Random.Range(0, 256);
            var v2 = Random.Range(0, 256);

            var min = (byte)Math.Min(v1, v2);
            var max = (byte)Math.Max(v1, v2);
            
            var c1 = ColorHelper.Random(min, max).ToColor32();

            Assert.True(c1.r >= min && c1.r <= max);
            Assert.True(c1.g >= min && c1.g <= max);
            Assert.True(c1.b >= min && c1.b <= max);
        }

        [Test]
        public void GetTextColorFromBackground()
        {
            Assert.True(Color.white.TextForegroundColorShouldBeDark());
            Assert.False(Color.black.TextForegroundColorShouldBeDark());
            
            Assert.AreEqual(ColorHelper.DarkTextColor, Color.white.TextForegroundColor());
            Assert.AreEqual(ColorHelper.LightTextColor, Color.black.TextForegroundColor());
        }

        [Test]
        public void NormalizeToColor()
        {
            var color = ColorHelper.Random().ToColor32();
            var c = ColorHelper.NormalizeToColor(color.r, color.g, color.b);
            
            Assert.AreEqual(color, c.ToColor32());
        }
        
        [Test]
        public void Color32Conversion()
        {
            var color = ColorHelper.Random();
            var color32 = color.ToColor32();
            
            Assert.AreEqual(color, (Color)color32);
        }
        
        [Test]
        public void SimpleHexColorConversion()
        {
            var color = ColorHelper.Random();
            var colorHex = color.ToHexString();
            
            Assert.AreEqual(color, ColorHelper.FromHex(colorHex));

            var colorHex2 = ColorUtility.ToHtmlStringRGBA(color);
            var colorHex3 =  color.ToHexString(false);

            Assert.AreEqual(colorHex, "#" + colorHex2);
            Assert.AreEqual(colorHex3, colorHex2);
        }
        
        [Test]
        public void ComplexHexColorConversion()
        {
            var color1 = ColorHelper.FromHex("#CCC");
            var color2 = ColorHelper.FromHex("#CCCCCC");
            var color3 = ColorHelper.FromHex("CCC");
            var color4 = ColorHelper.FromHex("CCCCCC");
            
            var color5 = ColorHelper.FromHex("#CCCA");
            var color6 = ColorHelper.FromHex("#CCCCCCAA");
            var color7 = ColorHelper.FromHex("CCCA");
            var color8 = ColorHelper.FromHex("CCCCCCAA");
            
            Assert.AreEqual(color1, color2);
            Assert.AreEqual(color1, color3);
            Assert.AreEqual(color1, color4);
            
            Assert.AreEqual(color5, color6);
            Assert.AreEqual(color5, color7);
            Assert.AreEqual(color5, color8);
        }

        
        [Test]
        public void HexColorConversionNoAlpha()
        {
            var color = ColorHelper.Random();
            var colorHex = color.ToHexStringNoAlpha();
            var colorHexNo00 = color.ToHexStringNoAlpha(false, false);
            
            Assert.AreEqual(color.WithA(0), ColorHelper.FromHex(colorHex));
            Assert.AreEqual(color, ColorHelper.FromHex(colorHexNo00));
        }

        [Test]
        public void WithR()
        {
            var color = ColorHelper.Random();
            var colorR0 = color;
            colorR0.r = 0;
            
            var color0R1 = color.WithR(0f);
            var color0R2 = color.WithR((byte)0);
            
            Assert.AreEqual(color0R1, color0R2);
            Assert.AreEqual(colorR0, color0R2);
            
            var colorR51 = color;
            colorR51.r = 0.2f;
            
            var colorDiv5R1 = color.WithR(0.2f);
            var colorDiv5R2 = color.WithR((byte)51);
            
            Assert.AreEqual(colorDiv5R1, colorDiv5R2);
            Assert.AreEqual(colorR51, colorDiv5R2);
        }
        
        [Test]
        public void WithG()
        {
            var color = ColorHelper.Random();
            var colorG0 = color;
            colorG0.g = 0;
            
            var color0G1 = color.WithG(0f);
            var color0G2 = color.WithG((byte)0);
            
            Assert.AreEqual(color0G1, color0G2);
            Assert.AreEqual(colorG0, color0G2);
            
            var colorG51 = color;
            colorG51.g = 0.2f;
            
            var colorDiv5G1 = color.WithG(0.2f);
            var colorDiv5G2 = color.WithG((byte)51);
            
            Assert.AreEqual(colorDiv5G1, colorDiv5G2);
            Assert.AreEqual(colorG51, colorDiv5G2);
        }
        
        [Test]
        public void WithB()
        {
            var color = ColorHelper.Random();
            var colorB0 = color;
            colorB0.b = 0;
            
            var color0B1 = color.WithB(0f);
            var color0B2 = color.WithB((byte)0);
            
            Assert.AreEqual(color0B1, color0B2);
            Assert.AreEqual(colorB0, color0B2);
            
            var colorB51 = color;
            colorB51.b = 0.2f;
            
            var colorDiv5B1 = color.WithB(0.2f);
            var colorDiv5B2 = color.WithB((byte)51);
            
            Assert.AreEqual(colorDiv5B1, colorDiv5B2);
            Assert.AreEqual(colorB51, colorDiv5B2);
        }
        
        [Test]
        public void WithA()
        {
            var color = ColorHelper.Random();
            var colorA0 = color;
            colorA0.a = 0;
            
            var color0A1 = color.WithA(0f);
            var color0A2 = color.WithA((byte)0);
            
            Assert.AreEqual(color0A1, color0A2);
            Assert.AreEqual(colorA0, color0A2);
            
            var colorA51 = color;
            colorA51.a = 0.2f;
            
            var colorDiv5A1 = color.WithA(0.2f);
            var colorDiv5A2 = color.WithA((byte)51);
            
            Assert.AreEqual(colorDiv5A1, colorDiv5A2);
            Assert.AreEqual(colorA51, colorDiv5A2);
        }
    }
}