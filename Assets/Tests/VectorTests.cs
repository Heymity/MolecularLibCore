using MolecularLib.Helpers;
using NUnit.Framework;
using UnityEngine;

public class VectorTests
{
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
    public void Vector3ToVectorXInt()
    {
        var vec3 = new Vector3(423.53f, -4235.644f, -473.25f);
        var vec2Int = new Vector2Int(424, -4236);
        var vec3Int = new Vector3Int(424, -4236, -473);
        
        Assert.True(vec3.ToVec2Int() == vec2Int);
        Assert.True(vec3.ToVec3Int() == vec3Int);
    }

    [Test]
    public void WithX()
    {
        var xStart = Random.Range(float.MinValue, float.MaxValue);
        var vec2 = new Vector2(xStart, Random.Range(float.MinValue, float.MaxValue));
        var vec3 = new Vector3(xStart, Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue));
        var vec4 = new Vector4(xStart, Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue));

        var vec2Xp1 = vec2.WithX(x => x + 1);
        var vec2X0 = vec2.WithoutX();

        var vec3Xp1 = vec3.WithX(x => x + 1);
        var vec3X0 = vec3.WithoutX();
        
        var vec4Xp1 = vec4.WithX(x => x + 1);
        var vec4X0 = vec4.WithoutX();
        
        Assert.True(vec2Xp1 == new Vector2(xStart + 1, vec2.y));
        Assert.True(vec3Xp1 == new Vector3(xStart + 1, vec3.y, vec3.z));
        Assert.True(vec4Xp1 == new Vector4(xStart + 1, vec4.y, vec4.z, vec4.w));
        
        Assert.True(vec2X0 == new Vector2(0, vec2.y));
        Assert.True(vec3X0 == new Vector3(0, vec3.y, vec3.z));
        Assert.True(vec4X0 == new Vector4(0, vec4.y, vec4.z, vec4.w));
    }
    
    [Test]
    public void WithY()
    {
        var yStart = Random.Range(float.MinValue, float.MaxValue);
        var vec2 = new Vector2(Random.Range(float.MinValue, float.MaxValue), yStart);
        var vec3 = new Vector3(Random.Range(float.MinValue, float.MaxValue), yStart, Random.Range(float.MinValue, float.MaxValue));
        var vec4 = new Vector4(Random.Range(float.MinValue, float.MaxValue), yStart, Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue));

        var vec2Yp1 = vec2.WithY(y => y + 1);
        var vec2Y0 = vec2.WithoutY();

        var vec3Yp1 = vec3.WithY(y => y + 1);
        var vec3Y0 = vec3.WithoutY();
        
        var vec4Yp1 = vec4.WithY(y => y + 1);
        var vec4Y0 = vec4.WithoutY();
        
        Assert.True(vec2Yp1 == new Vector2(vec2.x, yStart + 1));
        Assert.True(vec3Yp1 == new Vector3(vec3.x, yStart + 1, vec3.z));
        Assert.True(vec4Yp1 == new Vector4(vec4.x, yStart + 1, vec4.z, vec4.w));
        
        Assert.True(vec2Y0 == new Vector2(vec2.x, 0));
        Assert.True(vec3Y0 == new Vector3(vec3.x, 0, vec3.z));
        Assert.True(vec4Y0 == new Vector4(vec4.x, 0, vec4.z, vec4.w));
    }
    
    [Test]
    public void WithZ()
    {
        var zStart = Random.Range(float.MinValue, float.MaxValue);
        var vec3 = new Vector3(Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue), zStart);
        var vec4 = new Vector4(Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue), zStart, Random.Range(float.MinValue, float.MaxValue));

        var vec3Zp1 = vec3.WithZ(z => z + 1);
        var vec3Z0 = vec3.WithoutZ();
        
        var vec4Zp1 = vec4.WithZ(z => z + 1);
        var vec4Z0 = vec4.WithoutZ();

        Assert.True(vec3Zp1 == new Vector3(vec3.x, vec3.y, zStart + 1));
        Assert.True(vec4Zp1 == new Vector4(vec4.x, vec4.y, zStart + 1, vec4.w));
      
        Assert.True(vec3Z0 == new Vector3(vec3.x, vec3.y, 0));
        Assert.True(vec4Z0 == new Vector4(vec4.x, vec4.y, 0, vec4.w));
    }
    
    [Test]
    public void WithW()
    {
        var wStart = Random.Range(float.MinValue, float.MaxValue);
        var vec4 = new Vector4(Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue), Random.Range(float.MinValue, float.MaxValue), wStart);

        var vec4Zp1 = vec4.WithW(w => w + 1);
        var vec4Z0 = vec4.WithoutW();
        
        Assert.True(vec4Zp1 == new Vector4(vec4.x, vec4.y, vec4.z, wStart + 1));
   
        Assert.True(vec4Z0 == new Vector4(vec4.x, vec4.y, vec4.z, 0));
    }
    
    [Test]
    public void WithXInt()
    {
        var xStart = Random.Range(int.MinValue, int.MaxValue);
        var vec2 = new Vector2Int(xStart, Random.Range(int.MinValue, int.MaxValue));
        var vec3 = new Vector3Int(xStart, Random.Range(int.MinValue, int.MaxValue), Random.Range(int.MinValue, int.MaxValue));

        var vec2Xp1 = vec2.WithX(x => x + 1);
        var vec2X0 = vec2.WithoutX();

        var vec3Xp1 = vec3.WithX(x => x + 1);
        var vec3X0 = vec3.WithoutX();
        
        Assert.True(vec2Xp1 == new Vector2Int(xStart + 1, vec2.y));
        Assert.True(vec3Xp1 == new Vector3Int(xStart + 1, vec3.y, vec3.z));

        Assert.True(vec2X0 == new Vector2Int(0, vec2.y));
        Assert.True(vec3X0 == new Vector3Int(0, vec3.y, vec3.z));
    }
    
    [Test]
    public void WithYInt()
    {
        var yStart = Random.Range(int.MinValue, int.MaxValue);
        var vec2 = new Vector2Int(Random.Range(int.MinValue, int.MaxValue), yStart);
        var vec3 = new Vector3Int(Random.Range(int.MinValue, int.MaxValue), yStart, Random.Range(int.MinValue, int.MaxValue));

        var vec2Yp1 = vec2.WithY(y => y + 1);
        var vec2Y0 = vec2.WithoutY();

        var vec3Yp1 = vec3.WithY(y => y + 1);
        var vec3Y0 = vec3.WithoutY();

        Assert.True(vec2Yp1 == new Vector2Int(vec2.x, yStart + 1));
        Assert.True(vec3Yp1 == new Vector3Int(vec3.x, yStart + 1, vec3.z));
        
        Assert.True(vec2Y0 == new Vector2Int(vec2.x, 0));
        Assert.True(vec3Y0 == new Vector3Int(vec3.x, 0, vec3.z));
    }
    
    [Test]
    public void WithZInt()
    {
        var zStart = Random.Range(int.MinValue, int.MaxValue);
        var vec3 = new Vector3Int(Random.Range(int.MinValue, int.MaxValue), Random.Range(int.MinValue, int.MaxValue), zStart);

        var vec3Zp1 = vec3.WithZ(z => z + 1);
        var vec3Z0 = vec3.WithoutZ();

        Assert.True(vec3Zp1 == new Vector3Int(vec3.x, vec3.y, zStart + 1));

        Assert.True(vec3Z0 == new Vector3Int(vec3.x, vec3.y, 0));
    }
}
