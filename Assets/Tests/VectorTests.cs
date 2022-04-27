/*  Copyright 2022 Gabriel Pasquale Rodrigues Scavone
*
*  Licensed under the Apache License, Version 2.0 (the "License");
*  you may not use this file except in compliance with the License.
*  You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
*  Unless required by applicable law or agreed to in writing, software
*  distributed under the License is distributed on an "AS IS" BASIS,
*  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*  See the License for the specific language governing permissions and
*  limitations under the License.
*/

using MolecularLib.Helpers;
using NUnit.Framework;
using UnityEngine;

namespace MolecularLibTests
{
    public class VectorTests
    {
        [Test]
        public void RandomVector2ToVectorXInt()
        {
            var vec3 = new Vector2(Random.Range(float.MinValue, float.MaxValue),
                Random.Range(float.MinValue, float.MaxValue));
            var vec2Int = new Vector2Int(Mathf.RoundToInt(vec3.x), Mathf.RoundToInt(vec3.y));
            var vec3Int = new Vector3Int(Mathf.RoundToInt(vec3.x), Mathf.RoundToInt(vec3.y), 0);

            Assert.True(vec3.ToVec2Int() == vec2Int);
            Assert.True(vec3.ToVec3Int() == vec3Int);
        }

        [Test]
        public void RandomVector3ToVectorXInt()
        {
            var vec3 = new Vector3(Random.Range(float.MinValue, float.MaxValue),
                Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue));
            var vec2Int = new Vector2Int(Mathf.RoundToInt(vec3.x), Mathf.RoundToInt(vec3.y));
            var vec3Int = new Vector3Int(Mathf.RoundToInt(vec3.x), Mathf.RoundToInt(vec3.y), Mathf.RoundToInt(vec3.z));

            Assert.True(vec3.ToVec2Int() == vec2Int);
            Assert.True(vec3.ToVec3Int() == vec3Int);
        }

        [Test]
        public void RandomVector4ToVectorXInt()
        {
            var vec3 = new Vector4(Random.Range(float.MinValue, float.MaxValue),
                Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue),
                Random.Range(float.MinValue, float.MaxValue));
            var vec2Int = new Vector2Int(Mathf.RoundToInt(vec3.x), Mathf.RoundToInt(vec3.y));
            var vec3Int = new Vector3Int(Mathf.RoundToInt(vec3.x), Mathf.RoundToInt(vec3.y), Mathf.RoundToInt(vec3.z));

            Assert.True(vec3.ToVec2Int() == vec2Int);
            Assert.True(vec3.ToVec3Int() == vec3Int);
        }

        [Test]
        public void RandomVector2IntToVectorX()
        {
            var vec3Int = new Vector2Int(Random.Range(int.MinValue, int.MaxValue),
                Random.Range(int.MinValue, int.MaxValue));
            var vec2 = new Vector2(vec3Int.x, vec3Int.y);
            var vec3 = new Vector3(vec3Int.x, vec3Int.y, 0);
            var vec4 = new Vector4(vec3.x, vec3.y, 0, 0);

            Assert.True(vec3Int.ToVec2() == vec2);
            Assert.True(vec3Int.ToVec3() == vec3);
            Assert.True(vec3Int.ToVec4() == vec4);
        }

        [Test]
        public void RandomVector3IntToVectorX()
        {
            var vec3Int = new Vector3Int(Random.Range(int.MinValue, int.MaxValue),
                Random.Range(int.MinValue, int.MaxValue), Random.Range(int.MinValue, int.MaxValue));
            var vec2 = new Vector2(vec3Int.x, vec3Int.y);
            var vec3 = new Vector3(vec3Int.x, vec3Int.y, vec3Int.z);
            var vec4 = new Vector4(vec3Int.x, vec3Int.y, vec3Int.z, 0);

            Assert.True(vec3Int.ToVec2() == vec2);
            Assert.True(vec3Int.ToVec3() == vec3);
            Assert.True(vec3Int.ToVec4() == vec4);
        }

        [Test]
        public void WithX()
        {
            var xStart = Random.Range(float.MinValue, float.MaxValue);
            var vec2 = new Vector2(xStart, Random.Range(float.MinValue, float.MaxValue));
            var vec3 = new Vector3(xStart, Random.Range(float.MinValue, float.MaxValue),
                Random.Range(float.MinValue, float.MaxValue));
            var vec4 = new Vector4(xStart, Random.Range(float.MinValue, float.MaxValue),
                Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue));

            var rand = Random.Range(float.MinValue, float.MaxValue);
            
            var vec2Xp1 = vec2.WithX(x => x + 1);
            var vec2XR = vec2.WithX(rand);
            var vec2X0 = vec2.WithoutX();

            var vec3Xp1 = vec3.WithX(x => x + 1);
            var vec3XR = vec3.WithX(rand);
            var vec3X0 = vec3.WithoutX();

            var vec4Xp1 = vec4.WithX(x => x + 1);
            var vec4XR = vec4.WithX(rand);
            var vec4X0 = vec4.WithoutX();

            Assert.True(vec2Xp1 == new Vector2(xStart + 1, vec2.y));
            Assert.True(vec3Xp1 == new Vector3(xStart + 1, vec3.y, vec3.z));
            Assert.True(vec4Xp1 == new Vector4(xStart + 1, vec4.y, vec4.z, vec4.w));
            
            Assert.True(vec2XR == new Vector2(rand, vec2.y));
            Assert.True(vec3XR == new Vector3(rand, vec3.y, vec3.z));
            Assert.True(vec4XR == new Vector4(rand, vec4.y, vec4.z, vec4.w));

            Assert.True(vec2X0 == new Vector2(0, vec2.y));
            Assert.True(vec3X0 == new Vector3(0, vec3.y, vec3.z));
            Assert.True(vec4X0 == new Vector4(0, vec4.y, vec4.z, vec4.w));
        }

        [Test]
        public void WithY()
        {
            var yStart = Random.Range(float.MinValue, float.MaxValue);
            var vec2 = new Vector2(Random.Range(float.MinValue, float.MaxValue), yStart);
            var vec3 = new Vector3(Random.Range(float.MinValue, float.MaxValue), yStart,
                Random.Range(float.MinValue, float.MaxValue));
            var vec4 = new Vector4(Random.Range(float.MinValue, float.MaxValue), yStart,
                Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue));
            
            var rand = Random.Range(float.MinValue, float.MaxValue);
            
            var vec2Yp1 = vec2.WithY(y => y + 1);
            var vec2YR = vec2.WithY(rand);
            var vec2Y0 = vec2.WithoutY();

            var vec3Yp1 = vec3.WithY(y => y + 1);
            var vec3YR = vec3.WithY(rand);
            var vec3Y0 = vec3.WithoutY();

            var vec4Yp1 = vec4.WithY(y => y + 1);
            var vec4YR = vec4.WithY(rand);
            var vec4Y0 = vec4.WithoutY();

            Assert.True(vec2Yp1 == new Vector2(vec2.x, yStart + 1));
            Assert.True(vec3Yp1 == new Vector3(vec3.x, yStart + 1, vec3.z));
            Assert.True(vec4Yp1 == new Vector4(vec4.x, yStart + 1, vec4.z, vec4.w));
            
            Assert.True(vec2YR == new Vector2(vec2.x, rand));
            Assert.True(vec3YR == new Vector3(vec3.x, rand, vec3.z));
            Assert.True(vec4YR == new Vector4(vec4.x, rand, vec4.z, vec4.w));

            Assert.True(vec2Y0 == new Vector2(vec2.x, 0));
            Assert.True(vec3Y0 == new Vector3(vec3.x, 0, vec3.z));
            Assert.True(vec4Y0 == new Vector4(vec4.x, 0, vec4.z, vec4.w));
        }

        [Test]
        public void WithZ()
        {
            var zStart = Random.Range(float.MinValue, float.MaxValue);
            var vec3 = new Vector3(Random.Range(float.MinValue, float.MaxValue),
                Random.Range(float.MinValue, float.MaxValue), zStart);
            var vec4 = new Vector4(Random.Range(float.MinValue, float.MaxValue),
                Random.Range(float.MinValue, float.MaxValue), zStart, Random.Range(float.MinValue, float.MaxValue));
            
            var rand = Random.Range(float.MinValue, float.MaxValue);
            
            var vec3Zp1 = vec3.WithZ(z => z + 1);
            var vec3ZR = vec3.WithZ(rand);
            var vec3Z0 = vec3.WithoutZ();

            var vec4Zp1 = vec4.WithZ(z => z + 1);
            var vec4ZR = vec4.WithZ(rand);
            var vec4Z0 = vec4.WithoutZ();

            Assert.True(vec3Zp1 == new Vector3(vec3.x, vec3.y, zStart + 1));
            Assert.True(vec4Zp1 == new Vector4(vec4.x, vec4.y, zStart + 1, vec4.w));
            
            Assert.True(vec3ZR == new Vector3(vec3.x, vec3.y, rand));
            Assert.True(vec4ZR == new Vector4(vec4.x, vec4.y, rand, vec4.w));
            
            Assert.True(vec3Z0 == new Vector3(vec3.x, vec3.y, 0));
            Assert.True(vec4Z0 == new Vector4(vec4.x, vec4.y, 0, vec4.w));
        }

        [Test]
        public void WithW()
        {
            var wStart = Random.Range(float.MinValue, float.MaxValue);
            var vec4 = new Vector4(Random.Range(float.MinValue, float.MaxValue),
                Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue), wStart);
            
            var rand = Random.Range(float.MinValue, float.MaxValue);
            
            var vec4Zp1 = vec4.WithW(w => w + 1);
            var vec4ZR = vec4.WithW(rand);
            var vec4Z0 = vec4.WithoutW();

            Assert.True(vec4Zp1 == new Vector4(vec4.x, vec4.y, vec4.z, wStart + 1));
            
            Assert.True(vec4ZR == new Vector4(vec4.x, vec4.y, vec4.z, rand));

            Assert.True(vec4Z0 == new Vector4(vec4.x, vec4.y, vec4.z, 0));
        }

        [Test]
        public void WithXInt()
        {
            var xStart = Random.Range(int.MinValue, int.MaxValue);
            var vec2 = new Vector2Int(xStart, Random.Range(int.MinValue, int.MaxValue));
            var vec3 = new Vector3Int(xStart, Random.Range(int.MinValue, int.MaxValue),
                Random.Range(int.MinValue, int.MaxValue));

            var rand = Random.Range(int.MinValue, int.MaxValue);

            var vec2Xp1 = vec2.WithX(x => x + 1);
            var vec2XR = vec2.WithX(rand);
            var vec2X0 = vec2.WithoutX();

            var vec3Xp1 = vec3.WithX(x => x + 1);
            var vec3XR = vec3.WithX(rand);
            var vec3X0 = vec3.WithoutX();

            Assert.True(vec2Xp1 == new Vector2Int(xStart + 1, vec2.y));
            Assert.True(vec3Xp1 == new Vector3Int(xStart + 1, vec3.y, vec3.z));

            Assert.True(vec2XR == new Vector2Int(rand, vec2.y));
            Assert.True(vec3XR == new Vector3Int(rand, vec3.y, vec3.z));
            
            Assert.True(vec2X0 == new Vector2Int(0, vec2.y));
            Assert.True(vec3X0 == new Vector3Int(0, vec3.y, vec3.z));
        }

        [Test]
        public void WithYInt()
        {
            var yStart = Random.Range(int.MinValue, int.MaxValue);
            var vec2 = new Vector2Int(Random.Range(int.MinValue, int.MaxValue), yStart);
            var vec3 = new Vector3Int(Random.Range(int.MinValue, int.MaxValue), yStart,
                Random.Range(int.MinValue, int.MaxValue));

            var rand = Random.Range(int.MinValue, int.MaxValue);

            var vec2Yp1 = vec2.WithY(y => y + 1);
            var vec2YR = vec2.WithY(rand);
            var vec2Y0 = vec2.WithoutY();

            var vec3Yp1 = vec3.WithY(y => y + 1);
            var vec3YR = vec3.WithY(rand);
            var vec3Y0 = vec3.WithoutY();

            Assert.True(vec2Yp1 == new Vector2Int(vec2.x, yStart + 1));
            Assert.True(vec3Yp1 == new Vector3Int(vec3.x, yStart + 1, vec3.z));

            Assert.True(vec2YR == new Vector2Int(vec2.x, rand));
            Assert.True(vec3YR == new Vector3Int(vec3.x, rand, vec3.z));
            
            Assert.True(vec2Y0 == new Vector2Int(vec2.x, 0));
            Assert.True(vec3Y0 == new Vector3Int(vec3.x, 0, vec3.z));
        }

        [Test]
        public void WithZInt()
        {
            var zStart = Random.Range(int.MinValue, int.MaxValue);
            var vec3 = new Vector3Int(Random.Range(int.MinValue, int.MaxValue),
                Random.Range(int.MinValue, int.MaxValue), zStart);

            var rand = Random.Range(int.MinValue, int.MaxValue);

            var vec3Zp1 = vec3.WithZ(z => z + 1);
            var vec3ZR = vec3.WithZ(rand);
            var vec3Z0 = vec3.WithoutZ();

            Assert.True(vec3Zp1 == new Vector3Int(vec3.x, vec3.y, zStart + 1));
            
            Assert.True(vec3ZR == new Vector3Int(vec3.x, vec3.y, rand));

            Assert.True(vec3Z0 == new Vector3Int(vec3.x, vec3.y, 0));
        }

        [Test]
        public void IsBetweenAndIsWithinVec2()
        { 
            // ReSharper disable InconsistentNaming
            var vec2_1 = RandomVec2();
            var vec2_2 = RandomVec2();
            
            var maxVec2 = Vector2.Max(vec2_1, vec2_2);
            var minVec2 = Vector2.Min(vec2_1, vec2_2);

            var betweenVec2 = new Vector2(Random.Range(minVec2.x + float.Epsilon, maxVec2.x - float.Epsilon),
                Random.Range(minVec2.y + float.Epsilon, maxVec2.y - float.Epsilon));

            Assert.True(betweenVec2.IsBetween(minVec2, maxVec2));
            
            var outsideVec2_GG = new Vector2(Random.Range(maxVec2.x + float.Epsilon, float.MaxValue),
                Random.Range(maxVec2.y + float.Epsilon, float.MaxValue));
            var outsideVec2_GL = new Vector2(Random.Range(maxVec2.x + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec2.y - float.Epsilon));
            var outsideVec2_LG = new Vector2(Random.Range(float.MinValue, minVec2.x - float.Epsilon),
                Random.Range(maxVec2.y + float.Epsilon, float.MaxValue));
            var outsideVec2_LL = new Vector2(Random.Range(float.MinValue, minVec2.x - float.Epsilon),
                Random.Range(float.MinValue, minVec2.y - float.Epsilon));
            // ReSharper restore InconsistentNaming
            
            Assert.False(minVec2.IsBetween(minVec2, maxVec2));
            Assert.False(maxVec2.IsBetween(minVec2, maxVec2));
            
            Assert.False(outsideVec2_GG.IsBetween(minVec2, maxVec2));
            Assert.False(outsideVec2_GL.IsBetween(minVec2, maxVec2));
            Assert.False(outsideVec2_LG.IsBetween(minVec2, maxVec2));
            Assert.False(outsideVec2_LL.IsBetween(minVec2, maxVec2));
            
            Assert.True(betweenVec2.IsWithin(minVec2, maxVec2));
            
            Assert.True(minVec2.IsWithin(minVec2, maxVec2));
            Assert.True(maxVec2.IsWithin(minVec2, maxVec2));
            
            Assert.False(outsideVec2_GG.IsWithin(minVec2, maxVec2));
            Assert.False(outsideVec2_GL.IsWithin(minVec2, maxVec2));
            Assert.False(outsideVec2_LG.IsWithin(minVec2, maxVec2));
            Assert.False(outsideVec2_LL.IsWithin(minVec2, maxVec2));

            Vector2 RandomVec2() => new Vector2(Random.Range(float.MinValue, float.MaxValue),
                Random.Range(float.MinValue, float.MaxValue));
        }
        
        [Test]
        public void IsBetweenAndIsWithinVec3()
        {
            // ReSharper disable InconsistentNaming
            var vec3_1 = RandomVec3();
            var vec3_2 = RandomVec3();
            
            var maxVec3 = Vector3.Max(vec3_1, vec3_2);
            var minVec3 = Vector3.Min(vec3_1, vec3_2);
           
            var betweenVec3 = new Vector3(
                Random.Range(minVec3.x + float.Epsilon, maxVec3.x - float.Epsilon),
                Random.Range(minVec3.y + float.Epsilon, maxVec3.y - float.Epsilon),
                Random.Range(minVec3.z + float.Epsilon, maxVec3.z - float.Epsilon));

            Assert.True(betweenVec3.IsBetween(minVec3, maxVec3));
            
           
            var outsideVec3_GGG = new Vector3(
                Random.Range(maxVec3.x + float.Epsilon, float.MaxValue),
                Random.Range(maxVec3.y + float.Epsilon, float.MaxValue),
                Random.Range(maxVec3.z + float.Epsilon, float.MaxValue));
            var outsideVec3_GGL = new Vector3(
                Random.Range(maxVec3.x + float.Epsilon, float.MaxValue),
                Random.Range(maxVec3.y + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec3.z - float.Epsilon));
            var outsideVec3_GLG = new Vector3(
                Random.Range(maxVec3.x + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec3.y - float.Epsilon),
                Random.Range(maxVec3.z + float.Epsilon, float.MaxValue));
            var outsideVec3_GLL = new Vector3(
                Random.Range(maxVec3.x + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec3.y - float.Epsilon),
                Random.Range(float.MinValue, minVec3.z - float.Epsilon));
            var outsideVec3_LGG = new Vector3(
                Random.Range(float.MinValue, minVec3.x - float.Epsilon),
                Random.Range(maxVec3.y + float.Epsilon, float.MaxValue),
                Random.Range(maxVec3.z + float.Epsilon, float.MaxValue));
            var outsideVec3_LGL = new Vector3(
                Random.Range(float.MinValue, minVec3.x - float.Epsilon),
                Random.Range(maxVec3.y + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec3.z - float.Epsilon));
            var outsideVec3_LLG = new Vector3(
                Random.Range(float.MinValue, minVec3.x - float.Epsilon),
                Random.Range(float.MinValue, minVec3.y - float.Epsilon),
                Random.Range(maxVec3.z + float.Epsilon, float.MaxValue));
            var outsideVec3_LLL = new Vector3(
                Random.Range(float.MinValue, minVec3.x - float.Epsilon),
                Random.Range(float.MinValue, minVec3.y - float.Epsilon),
                Random.Range(float.MinValue, minVec3.z - float.Epsilon));
            // ReSharper restore InconsistentNaming

            Assert.False(minVec3.IsBetween(minVec3, maxVec3));
            Assert.False(maxVec3.IsBetween(minVec3, maxVec3));
            
            Assert.False(outsideVec3_GGG.IsBetween(minVec3, maxVec3));
            Assert.False(outsideVec3_GGL.IsBetween(minVec3, maxVec3));
            Assert.False(outsideVec3_GLG.IsBetween(minVec3, maxVec3));
            Assert.False(outsideVec3_GLL.IsBetween(minVec3, maxVec3));
            Assert.False(outsideVec3_LGG.IsBetween(minVec3, maxVec3));
            Assert.False(outsideVec3_LGL.IsBetween(minVec3, maxVec3));
            Assert.False(outsideVec3_LLG.IsBetween(minVec3, maxVec3));
            Assert.False(outsideVec3_LLL.IsBetween(minVec3, maxVec3));
            
            Assert.True(betweenVec3.IsWithin(minVec3, maxVec3));
            
            Assert.True(minVec3.IsWithin(minVec3, maxVec3));
            Assert.True(maxVec3.IsWithin(minVec3, maxVec3));
            
            Assert.False(outsideVec3_GGG.IsWithin(minVec3, maxVec3));
            Assert.False(outsideVec3_GGL.IsWithin(minVec3, maxVec3));
            Assert.False(outsideVec3_GLG.IsWithin(minVec3, maxVec3));
            Assert.False(outsideVec3_GLL.IsWithin(minVec3, maxVec3));
            Assert.False(outsideVec3_LGG.IsWithin(minVec3, maxVec3));
            Assert.False(outsideVec3_LGL.IsWithin(minVec3, maxVec3));
            Assert.False(outsideVec3_LLG.IsWithin(minVec3, maxVec3));
            Assert.False(outsideVec3_LLL.IsWithin(minVec3, maxVec3));
            
            Vector3 RandomVec3() => new Vector3(Random.Range(float.MinValue, float.MaxValue),
                Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue));
        }
        
        [Test]
        public void IsBetweenAndIsWithinVec4()
        {
            // ReSharper disable InconsistentNaming
            // ReSharper disable IdentifierTypo
            var vec4_1 = RandomVec4();
            var vec4_2 = RandomVec4();
            
            var maxVec4 = Vector4.Max(vec4_1, vec4_2);
            var minVec4 = Vector4.Min(vec4_1, vec4_2);

            var betweenVec4 = new Vector4(
                Random.Range(minVec4.x + float.Epsilon, maxVec4.x - float.Epsilon),
                Random.Range(minVec4.y + float.Epsilon, maxVec4.y - float.Epsilon),
                Random.Range(minVec4.z + float.Epsilon, maxVec4.z - float.Epsilon),
                Random.Range(minVec4.w + float.Epsilon, maxVec4.w - float.Epsilon));

            Assert.True(betweenVec4.IsBetween(minVec4, maxVec4));

            var outsideVec4_GGGG = new Vector4(
                Random.Range(maxVec4.x + float.Epsilon, float.MaxValue),
                Random.Range(maxVec4.y + float.Epsilon, float.MaxValue),
                Random.Range(maxVec4.z + float.Epsilon, float.MaxValue),
                Random.Range(maxVec4.w + float.Epsilon, float.MaxValue));
            var outsideVec4_GGGL = new Vector4(
                Random.Range(maxVec4.x + float.Epsilon, float.MaxValue),
                Random.Range(maxVec4.y + float.Epsilon, float.MaxValue),
                Random.Range(maxVec4.z + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec4.w - float.Epsilon));
            var outsideVec4_GGLG = new Vector4(
                Random.Range(maxVec4.x + float.Epsilon, float.MaxValue),
                Random.Range(maxVec4.y + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec4.z - float.Epsilon),
                Random.Range(maxVec4.w + float.Epsilon, float.MaxValue));
            var outsideVec4_GGLL = new Vector4(
                Random.Range(maxVec4.x + float.Epsilon, float.MaxValue),
                Random.Range(maxVec4.y + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec4.z - float.Epsilon),
                Random.Range(float.MinValue, minVec4.w - float.Epsilon));
            var outsideVec4_GLGG = new Vector4(
                Random.Range(maxVec4.x + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec4.y - float.Epsilon),
                Random.Range(maxVec4.z + float.Epsilon, float.MaxValue),
                Random.Range(maxVec4.w + float.Epsilon, float.MaxValue));
            var outsideVec4_GLGL = new Vector4(
                Random.Range(maxVec4.x + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec4.y - float.Epsilon),
                Random.Range(maxVec4.z + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec4.w - float.Epsilon));
            var outsideVec4_GLLG = new Vector4(
                Random.Range(maxVec4.x + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec4.y - float.Epsilon),
                Random.Range(float.MinValue, minVec4.z - float.Epsilon),
                Random.Range(maxVec4.w + float.Epsilon, float.MaxValue));
            var outsideVec4_GLLL = new Vector4(
                Random.Range(maxVec4.x + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec4.y - float.Epsilon),
                Random.Range(float.MinValue, minVec4.z - float.Epsilon),
                Random.Range(float.MinValue, minVec4.w - float.Epsilon));
            var outsideVec4_LGGG = new Vector4(
                Random.Range(float.MinValue, minVec4.x - float.Epsilon),
                Random.Range(maxVec4.y + float.Epsilon, float.MaxValue),
                Random.Range(maxVec4.z + float.Epsilon, float.MaxValue),
                Random.Range(maxVec4.w + float.Epsilon, float.MaxValue));
            var outsideVec4_LGGL = new Vector4(
                Random.Range(float.MinValue, minVec4.x - float.Epsilon),
                Random.Range(maxVec4.y + float.Epsilon, float.MaxValue),
                Random.Range(maxVec4.z + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec4.w - float.Epsilon));
            var outsideVec4_LGLG = new Vector4(
                Random.Range(float.MinValue, minVec4.x - float.Epsilon),
                Random.Range(maxVec4.y + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec4.z - float.Epsilon),
                Random.Range(maxVec4.w + float.Epsilon, float.MaxValue));
            var outsideVec4_LGLL = new Vector4(
                Random.Range(float.MinValue, minVec4.x - float.Epsilon),
                Random.Range(maxVec4.y + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec4.z - float.Epsilon),
                Random.Range(float.MinValue, minVec4.w - float.Epsilon));
            var outsideVec4_LLGG = new Vector4(
                Random.Range(float.MinValue, minVec4.x - float.Epsilon),
                Random.Range(float.MinValue, minVec4.y - float.Epsilon),
                Random.Range(maxVec4.z + float.Epsilon, float.MaxValue),
                Random.Range(maxVec4.w + float.Epsilon, float.MaxValue));
            var outsideVec4_LLGL = new Vector4(
                Random.Range(float.MinValue, minVec4.x - float.Epsilon),
                Random.Range(float.MinValue, minVec4.y - float.Epsilon),
                Random.Range(maxVec4.z + float.Epsilon, float.MaxValue),
                Random.Range(float.MinValue, minVec4.w - float.Epsilon));
            var outsideVec4_LLLG = new Vector4(
                Random.Range(float.MinValue, minVec4.x - float.Epsilon),
                Random.Range(float.MinValue, minVec4.y - float.Epsilon),
                Random.Range(float.MinValue, minVec4.z - float.Epsilon),
                Random.Range(maxVec4.w + float.Epsilon, float.MaxValue));
            var outsideVec4_LLLL = new Vector4(
                Random.Range(float.MinValue, minVec4.x - float.Epsilon),
                Random.Range(float.MinValue, minVec4.y - float.Epsilon),
                Random.Range(float.MinValue, minVec4.z - float.Epsilon),
                Random.Range(float.MinValue, minVec4.w - float.Epsilon));
            // ReSharper restore InconsistentNaming
            // ReSharper restore IdentifierTypo
            
            Assert.False(minVec4.IsBetween(minVec4, maxVec4));
            Assert.False(maxVec4.IsBetween(minVec4, maxVec4));
            
            Assert.False(outsideVec4_LLLG.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_LLGL.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_LLGG.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_LGLL.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_LGLG.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_LGGL.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_LGGG.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_GLLL.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_GLLG.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_GLGL.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_GLGG.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_GGLL.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_GGGG.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_GGGL.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_GGLG.IsBetween(minVec4, maxVec4));
            Assert.False(outsideVec4_LLLL.IsBetween(minVec4, maxVec4));
            
            Assert.True(betweenVec4.IsWithin(minVec4, maxVec4));
            
            Assert.True(minVec4.IsWithin(minVec4, maxVec4));
            Assert.True(maxVec4.IsWithin(minVec4, maxVec4));
            
            Assert.False(outsideVec4_LLLG.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_LLGL.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_LLGG.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_LGLL.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_LGLG.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_LGGL.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_LGGG.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_GLLL.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_GLLG.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_GLGL.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_GLGG.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_GGLL.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_GGGG.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_GGGL.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_GGLG.IsWithin(minVec4, maxVec4));
            Assert.False(outsideVec4_LLLL.IsWithin(minVec4, maxVec4));
            
            Vector4 RandomVec4() => new Vector4(Random.Range(float.MinValue, float.MaxValue),
                Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue));
        }
    }
}