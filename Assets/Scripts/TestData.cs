using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PrimitiveTest
{
    public sbyte sbyteTest = Convert.ToSByte(UnityEngine.Random.Range(sbyte.MinValue, sbyte.MaxValue));
    public int intTest = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
    public short shortTest = Convert.ToInt16(UnityEngine.Random.Range(short.MinValue, short.MaxValue));
    public long longTest = Convert.ToInt64(UnityEngine.Random.Range(long.MinValue, long.MaxValue));

    public byte byteTest = Convert.ToByte(UnityEngine.Random.Range(byte.MinValue, byte.MaxValue));
    public uint uintTest = Convert.ToUInt32(UnityEngine.Random.Range(uint.MinValue, uint.MaxValue));
    public ushort ushortTest = Convert.ToUInt16(UnityEngine.Random.Range(ushort.MinValue, ushort.MaxValue));
    public ulong ulongTest = Convert.ToUInt64(UnityEngine.Random.Range(ulong.MinValue, ulong.MaxValue));

    public float floatTest = UnityEngine.Random.Range(float.MinValue, float.MaxValue);
    public double doubleTest = Convert.ToDouble(UnityEngine.Random.Range(float.MinValue, float.MaxValue));
    public decimal decimalTest = decimal.Zero;

    public string stringTest = UnityEngine.Random.Range(int.MinValue, int.MaxValue).ToString();
    public bool boolTest = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));
    public char charTest = (char)UnityEngine.Random.Range(50, 120);
}

class ArrayTest
{
    public int[] intArrayTest = { 94, 5, -8, -12 };
    public double[] doubleArrayTest = { 45.4, -68.99, 798798.6815 };
}

struct StructTest
{
    public int x;
    public float y;
    public double z;

    public StructTest(int x, float y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

enum E_EnumTest
{
    A,
    A2,
    A3,
    A4,
    A5,
    A78,
    M78,
}

class ClassTest1
{
    public int intTest = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
    public bool boolTest = Convert.ToBoolean(UnityEngine.Random.Range(0, 2));
    public char charTest = (char)UnityEngine.Random.Range(50, 120);

    public float floatTest = UnityEngine.Random.Range(float.MinValue, float.MaxValue);

    public double doubleTest = Convert.ToDouble(UnityEngine.Random.Range(float.MinValue, float.MaxValue));
    public string stringTest = UnityEngine.Random.Range(int.MinValue, int.MaxValue).ToString();
        
    public int[] intArrayTest = { 94, 5, -8, -12 };

    public StructTest structTest = new(6, 6, 6);

    public PrimitiveTest classTest = new();
}

class ClassTest2
{
    public ArrayList arrayListTest = new() { 321, true, '/', 12.5f, 34.43, ":::::df", new StructTest(321, 654, 987) };

    public List<int> testGenericList = new() { 94, 5, -8, -12, 321 };

    public List<StructTest> testGenericList2 = new() { new(321, 654, 987), new(3216, 54, 987), new(32165, 49, 87) };

    public Dictionary<int, string> testDictionary = new()
    {
        { 789, "789" },
        { 159, "159" },
        { 345, "345" },
        { 985, "985" },
        { 211, "211" },
        { 2, "sf" }
    };

    public Dictionary<double, bool> testDictionary2 = new()
    {
        { 789.123, true },
        { 159.12, true },
        { 34.5, false },
        { 9.85, true },
        { 21.1, false },
        { 2.0025, true }
    };

    public E_EnumTest et = E_EnumTest.A3;
}

class ClassTest3
{
    public Dictionary<StructTest, E_EnumTest> testDictionary = new()
    {
        { new(321, 65.4f, 9.87), E_EnumTest.A4 },
        { new(321, 6.54f, 98.7), E_EnumTest.A78 },
        { new(321, 0.654f, 987.5), E_EnumTest.M78 },
    };
}
