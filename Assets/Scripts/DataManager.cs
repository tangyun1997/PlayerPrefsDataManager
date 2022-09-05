using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DataManager
{
    public static DataManager Instance { get; } = new();

    public void Save(string key, object data)
    {
        Type dataType = data.GetType();
        TypeCode dataTypeCode = Type.GetTypeCode(dataType);

        //Debug.Log("data type：" + dataType);
        //Debug.Log("dataTypeCode：" + dataTypeCode);

        //enum
        if (dataType.IsEnum)
        {
            Debug.Log("存入枚举");
            SaveEnum(key, data);
        }
        //Boolean Char SByte Int16 Int32 Byte UInt16
        else if ((int)dataTypeCode >= 3 && (int)dataTypeCode <= 9)
            SaveInt(key, Convert.ToInt32(data));
        //Single
        else if ((int)dataTypeCode == 13)
            SaveSingle(key, Convert.ToSingle(data));
        //UInt32 Int64 UInt64 Double Decimal String
        else if ((int)dataTypeCode >= 10 && (int)dataTypeCode <= 18)
            SaveString(key, Convert.ToString(data));
        //array
        else if (dataType.IsArray)
        {
            Debug.Log("存入数组");
            SaveArray(key, data);
        }
        //ArrayList GenericList
        else if (typeof(IList).IsAssignableFrom(dataType))
        {
            if (dataType.GetGenericArguments().Length == 0)
            {
                Debug.Log("存入ArrayList");
                SaveArrayList(key, data);
            }
            else if (dataType.GetGenericArguments().Length == 1)
            {
                Debug.Log("存入List");
                SaveList(key, data);
            }
        }
        //Dictionary
        else if (typeof(IDictionary).IsAssignableFrom(dataType))
        {
            Debug.Log("存入字典");
            SaveDictionary(key, data);
        }
        //class struct
        else
        {
            Debug.Log("存入class");
            SaveClass(key, data);
        }

        PlayerPrefs.Save();
    }

    public object Load(string key, Type type)
    {
        TypeCode dataTypeCode = Type.GetTypeCode(type);

        //Debug.Log("当前key：" + key);
        //Debug.Log("data type：" + type);

        if (type.IsEnum)
        {
            Debug.Log("读取枚举");
            return LoadEnum(key, type);
        }
        //Boolean Char SByte Int16 Int32 Byte UInt16
        else if ((int)dataTypeCode >= 3 && (int)dataTypeCode <= 9)
            return Convert.ChangeType(LoadInt(key), type);
        //Single
        else if ((int)dataTypeCode == 13)
            return LoadSingle(key);
        //UInt32 Int64 UInt64 Double Decimal String
        else if ((int)dataTypeCode >= 10 && (int)dataTypeCode <= 18)
            return Convert.ChangeType(LoadString(key), type);
        //array
        else if (type.IsArray)
        {
            Debug.Log("读取数组");
            return LoadArray(key, type);
        }
        //ArrayList GenericList
        else if (typeof(IList).IsAssignableFrom(type))
        {
            if (type.GetGenericArguments().Length == 0)
            {
                Debug.Log("读取ArrayList");
                return LoadArrayList(key);
            }
            else if (type.GetGenericArguments().Length == 1)
            {
                Debug.Log("读取List");
                return LoadList(key, type);
            }
            return null;
        }
        //Dictionary
        else if (typeof(IDictionary).IsAssignableFrom(type))
        {
            Debug.Log("读取字典");
            return LoadDictionary(key, type);
        }
        //class struct
        else
        {
            Debug.Log("读取class");
            return LoadClass(key, type);
        }
    }
    
    public T Load<T>(string key)
    {
        Type type = typeof(T);

        TypeCode dataTypeCode = Type.GetTypeCode(type);

        //Debug.Log("当前key：" + key);
        //Debug.Log("data type：" + type);

        if (type.IsEnum)
        {
            Debug.Log("读取枚举");
            return (T)LoadEnum(key, type);
        }
        //Boolean Char SByte Int16 Int32 Byte UInt16
        else if ((int)dataTypeCode >= 3 && (int)dataTypeCode <= 9)
            return (T)Convert.ChangeType(LoadInt(key), type);
        //Single
        else if ((int)dataTypeCode == 13)
            return (T)Convert.ChangeType(LoadSingle(key), type);
        //UInt32 Int64 = 11 UInt64 Double Decimal String
        else if ((int)dataTypeCode >= 10 && (int)dataTypeCode <= 18)
            return (T)Convert.ChangeType(LoadString(key), type);
        //array
        else if (type.IsArray)
        {
            Debug.Log("读取数组");
            return (T)LoadArray(key, type);
        }
        //ArrayList GenericList
        else if (typeof(IList).IsAssignableFrom(type))
        {
            if (type.GetGenericArguments().Length == 0)
            {
                Debug.Log("读取ArrayList");
                return (T)LoadArrayList(key);
            }
            else if (type.GetGenericArguments().Length == 1)
            {
                Debug.Log("读取List");
                return (T)LoadList(key, type);
            }
            return default(T);
        }
        //Dictionary
        else if (typeof(IDictionary).IsAssignableFrom(type))
        {
            Debug.Log("读取字典");
            return (T)LoadDictionary(key, type);
        }
        //class struct
        else
        {
            Debug.Log("读取class");
            return (T)LoadClass(key, type);
        }
    }
    
    private void SaveInt(string key,int value)
    {
        Debug.Log("当前key：" + key);
        Debug.Log("存入变量值为：" + value);
        Debug.Log("==============================");

        PlayerPrefs.SetInt(key, EncryptInt(key, value));
    }

    private void SaveSingle(string key, float value)
    {
        Debug.Log("当前key：" + key);
        Debug.Log("存入变量值为：" + value);
        Debug.Log("==============================");

        PlayerPrefs.SetFloat(key, value);
    }

    private void SaveString(string key, String value)
    {
        Debug.Log("当前key：" + key);
        Debug.Log("存入变量值为：" + value);
        Debug.Log("==============================");

        PlayerPrefs.SetString(key, value);
    }

    private int LoadInt(string key)
    {
        int value = DecryptInt(key, PlayerPrefs.GetInt(key, key.Length));

        Debug.Log("当前key：" + key);
        Debug.Log("读取变量值为：" + value);
        Debug.Log("==============================");

        return value;
    }

    private float LoadSingle(string key)
    {
        float value = PlayerPrefs.GetFloat(key, 0);

        Debug.Log("当前key：" + key);
        Debug.Log("读取变量值为：" + value);
        Debug.Log("==============================");

        return value;
    }

    private string LoadString(string key)
    {
        string value = PlayerPrefs.GetString(key, "1");

        Debug.Log("当前key：" + key);
        Debug.Log("读取变量值为：" + value);
        Debug.Log("==============================");

        return value;
    }

    private void SaveEnum(string key, object data)
    {
        SaveString(key + "_Enum", data.ToString());
    }

    private object LoadEnum(string key, Type type)
    {
        return Enum.Parse(type, LoadString(key + "_Enum"));
    }

    private void SaveArray(string key, object data)
    {
        IList list = data as IList;
        int count = list.Count;

        Debug.Log("存入数组长度为：" + count);

        SaveInt(key + "_Count", count);

        for (int i = 0; i < count; i++)
            Save(key + "_Index" + i, list[i]);
    }

    private object LoadArray(string key, Type type)
    {
        int count = Convert.ToInt32(Load(key + "_Count", typeof(int)));
        Type elementType = type.GetElementType();

        Debug.Log("读取数组长度为：" + count);
        Debug.Log("elementType：" + elementType);

        IList ret = Array.CreateInstance(elementType, count);

        for (int i = 0; i < count; i++)
            ret[i] = Load(key + "_Index" + i, elementType);

        return ret;
    }

    private void SaveClass(string key, object data)
    {
        FieldInfo[] fieldInfos = data.GetType().GetFields();
        FieldInfo fieldInfo;

        for (int i = 0; i < fieldInfos.Length;)
        {
            fieldInfo = fieldInfos[i++];
            Debug.Log("存入字段" + i);

            Debug.Log("存入变量类型为：" + Type.GetTypeCode(fieldInfo.FieldType));
            Debug.Log("存入变量名为：" + fieldInfo.Name);

            Save(key + "_" + fieldInfo.Name, fieldInfo.GetValue(data));
        }
    }

    private object LoadClass(string key, Type type)
    {
        object ret = Activator.CreateInstance(type);

        FieldInfo[] fieldInfos = type.GetFields();
        FieldInfo fieldInfo;

        for (int i = 0; i < fieldInfos.Length;)
        {
            fieldInfo = fieldInfos[i++];
            Debug.Log("读取字段" + i);

            Debug.Log("读取变量类型为：" + Type.GetTypeCode(fieldInfo.FieldType));
            Debug.Log("读取变量名为：" + fieldInfo.Name);

            fieldInfo.SetValue(ret, Load(key + "_" + fieldInfo.Name, fieldInfo.FieldType));
        }

        return ret;
    }

    private void SaveArrayList(string key, object data)
    {
        IList list = data as IList;
        int count = list.Count;

        Debug.Log("存入ArrayList长度为：" + count);

        SaveInt(key + "_Count", count);

        for (int i = 0; i < count; i++)
        {
            SaveString(key + "_Index" + i+"_Type", list[i].GetType().ToString());

            Save(key + "_Index" + i, list[i]);
        }
    }

    private object LoadArrayList(string key)
    {
        int count = Convert.ToInt32(Load(key + "_Count", typeof(int)));

        Debug.Log("读取ArrayList长度为：" + count);

        ArrayList ret = new();

        for (int i = 0; i < count; i++)
        {
            Type elementType = Type.GetType(LoadString(key + "_Index" + i + "_Type"));

            Debug.Log("elementType：" + elementType);

            ret.Add(Load(key + "_Index" + i, elementType));
        }

        return ret;
    }

    private void SaveList(string key, object data)
    {
        IList list = data as IList;
        int count = list.Count;
        SaveInt(key + "_Count", count);

        Debug.Log("存入List长度为：" + count);

        for (int i = 0; i < count; i++)
            Save(key + "_Index" + i, list[i]);
    }

    private object LoadList(string key, Type type)
    {
        int count = Convert.ToInt32(Load(key + "_Count", typeof(int)));
        Type valueType = type.GetGenericArguments()[0];

        Debug.Log("读取List长度为：" + count);
        Debug.Log("valueType：" + valueType);

        IList ret = Activator.CreateInstance(type) as IList;

        for (int i = 0; i < count; i++)
            ret.Add(Load(key + "_Index" + i, valueType));

        return ret;
    }

    private void SaveDictionary(string key, object data)
    {
        IDictionary dic = data as IDictionary;
        int count = dic.Count;
        IDictionaryEnumerator enu = dic.GetEnumerator();

        SaveInt(key + "_Count", count);
        Debug.Log("存入字典对数为：" + count);

        for (int i = 0; i < count; i++)
        {
            enu.MoveNext();

            Debug.Log("键：" + enu.Key);
            Debug.Log("值：" + enu.Value);

            Save(key + "_Index" + i + "_Key", enu.Key);
            Save(key + "_Index" + i + "_Value", enu.Value);
        }
    }

    private object LoadDictionary(string key, Type type)
    {
        int count = Convert.ToInt32(Load(key + "_Count", typeof(int)));
        Type keyType = type.GetGenericArguments()[0];
        Type valueType = type.GetGenericArguments()[1];

        Debug.Log("读取字典对数为：" + count);
        Debug.Log("keyType：" + keyType);
        Debug.Log("valueType：" + valueType);

        IDictionary ret = Activator.CreateInstance(type) as IDictionary;

        for (int i = 0; i < count; i++)
            ret.Add(Load(key + "_Index" + i + "_Key", keyType), Load(key + "_Index" + i + "_Value", valueType));

        return ret;
    }

    private int EncryptInt(string key, int value) => value + key.Length;

    private int DecryptInt(string key, int value) => value - key.Length;
}
