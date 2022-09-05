using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SaveLoadTest : MonoBehaviour
{
    public bool isClearPlayerPrefs;

    public bool savePrimitive;
    public bool loadPrimitive;

    public bool saveArray;
    public bool loadArray;

    public bool saveClass;
    public bool loadClass;

    public bool saveArrayList;
    public bool loadArrayList;

    public bool saveGenericList;
    public bool loadGenericList;

    public bool saveDictionary;
    public bool loadDictionary;

    public bool saveEnum;
    public bool loadEnum;

    void Start()
    {
        if (isClearPlayerPrefs)
            PlayerPrefs.DeleteAll();

        if (savePrimitive)
            SavePrimitive();

        if (loadPrimitive)
            LoadPrimitive();

        if (saveArray)
            SaveArray();

        if (loadArray)
            LoadArray();

        if (saveClass)
            SaveClass();

        if (loadClass)
            LoadClass();

        if (saveArrayList)
            SaveArrayList();

        if (loadArrayList)
            LoadArrayList();

        if (saveGenericList)
            SaveGenericList();

        if (loadGenericList)
            LoadGenericList();

        if (saveDictionary)
            SaveDictionary();

        if (loadDictionary)
            LoadDictionary();

        if (saveEnum)
            SaveEnum();

        if (loadEnum)
            LoadEnum();
    }

    private void SavePrimitive()
    {
        PrimitiveTest primitive = new();

        DataManager.Instance.Save("b32", primitive.boolTest);
        DataManager.Instance.Save("a54", primitive.intTest);
        DataManager.Instance.Save("f123", primitive.floatTest);
        DataManager.Instance.Save("s", primitive.doubleTest);
        DataManager.Instance.Save("l43", primitive.longTest);
    }

    private void LoadPrimitive()
    {
        DataManager.Instance.Load("b32", typeof(bool));
        DataManager.Instance.Load("a54", typeof(int));
        DataManager.Instance.Load("f123", typeof(float));
        DataManager.Instance.Load("s", typeof(double));
        DataManager.Instance.Load("l43", typeof(long));
    }

    private void SaveArray()
    {
        ArrayTest array = new();

        DataManager.Instance.Save("arrTest", array.intArrayTest);
        DataManager.Instance.Save("arrTest2", array.doubleArrayTest);
    }

    private void LoadArray()
    {
        int[] arrTest = DataManager.Instance.Load("arrTest", typeof(int[])) as int[];
        double[] arrTest2 = DataManager.Instance.Load("arrTest2", typeof(double[])) as double[];
    }

    private void SaveClass()
    {
        //DataManager.Instance.Save("DemoClass2345", new AllTest2());

        DataManager.Instance.Save("test2345", new ClassTest3());
    }

    private void LoadClass()
    {
        //ClassTest2 demo1Test = DataManager.Instance.Load("DemoClass2345", typeof(ClassTest2)) as ClassTest2;
        //ClassTest2 demo1Test = DataManager.Instance.Load<ClassTest2>("DemoClass2345");

        ClassTest3 test3 = DataManager.Instance.Load<ClassTest3>("test2345");
    }

    private void SaveArrayList()
    {
        DataManager.Instance.Save("arrayListTest", new ClassTest2().arrayListTest);
    }

    private void LoadArrayList()
    {
        ArrayList arrayList = DataManager.Instance.Load("arrayListTest", typeof(ArrayList)) as ArrayList;
    }

    private void SaveGenericList()
    {
        DataManager.Instance.Save("GenericList", new ClassTest2().testGenericList);
        DataManager.Instance.Save("GenericList2", new ClassTest2().testGenericList2);
    }

    private void LoadGenericList()
    {
        List<int> l = (List<int>)DataManager.Instance.Load("GenericList", typeof(List<int>));
        List<StructTest> l1 = (List<StructTest>)DataManager.Instance.Load("GenericList2", typeof(List<StructTest>));
    }

    private void SaveDictionary()
    {
        //DataManager.Instance.Save("Dictionary", new AllTest2().testDictionary);

        DataManager.Instance.Save("Dictionary2", new ClassTest2().testDictionary2);
    }

    private void LoadDictionary()
    {
        //Dictionary<int, string> dic = (Dictionary<int, string>)DataManager.Instance.Load("Dictionary", typeof(Dictionary<int, string>));

        Dictionary<double, bool> dic2 = (Dictionary<double, bool>)DataManager.Instance.Load("Dictionary2", typeof(Dictionary<double, bool>));
    }

    private void SaveEnum()
    {
        E_EnumTest et = E_EnumTest.M78;
        DataManager.Instance.Save("enumT", et);
    }

    private void LoadEnum()
    {
        E_EnumTest e = (E_EnumTest)DataManager.Instance.Load("enumT", typeof(E_EnumTest));
    }
}
