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

        //Debug.Log("data type��" + dataType);
        //Debug.Log("dataTypeCode��" + dataTypeCode);

        //enum
        if (dataType.IsEnum)
        {
            Debug.Log("����ö��");
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
            Debug.Log("��������");
            SaveArray(key, data);
        }
        //ArrayList GenericList
        else if (typeof(IList).IsAssignableFrom(dataType))
        {
            if (dataType.GetGenericArguments().Length == 0)
            {
                Debug.Log("����ArrayList");
                SaveArrayList(key, data);
            }
            else if (dataType.GetGenericArguments().Length == 1)
            {
                Debug.Log("����List");
                SaveList(key, data);
            }
        }
        //Dictionary
        else if (typeof(IDictionary).IsAssignableFrom(dataType))
        {
            Debug.Log("�����ֵ�");
            SaveDictionary(key, data);
        }
        //class struct
        else
        {
            Debug.Log("����class");
            SaveClass(key, data);
        }

        PlayerPrefs.Save();
    }

    public object Load(string key, Type type)
    {
        TypeCode dataTypeCode = Type.GetTypeCode(type);

        //Debug.Log("��ǰkey��" + key);
        //Debug.Log("data type��" + type);

        if (type.IsEnum)
        {
            Debug.Log("��ȡö��");
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
            Debug.Log("��ȡ����");
            return LoadArray(key, type);
        }
        //ArrayList GenericList
        else if (typeof(IList).IsAssignableFrom(type))
        {
            if (type.GetGenericArguments().Length == 0)
            {
                Debug.Log("��ȡArrayList");
                return LoadArrayList(key);
            }
            else if (type.GetGenericArguments().Length == 1)
            {
                Debug.Log("��ȡList");
                return LoadList(key, type);
            }
            return null;
        }
        //Dictionary
        else if (typeof(IDictionary).IsAssignableFrom(type))
        {
            Debug.Log("��ȡ�ֵ�");
            return LoadDictionary(key, type);
        }
        //class struct
        else
        {
            Debug.Log("��ȡclass");
            return LoadClass(key, type);
        }
    }
    
    public T Load<T>(string key)
    {
        Type type = typeof(T);

        TypeCode dataTypeCode = Type.GetTypeCode(type);

        //Debug.Log("��ǰkey��" + key);
        //Debug.Log("data type��" + type);

        if (type.IsEnum)
        {
            Debug.Log("��ȡö��");
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
            Debug.Log("��ȡ����");
            return (T)LoadArray(key, type);
        }
        //ArrayList GenericList
        else if (typeof(IList).IsAssignableFrom(type))
        {
            if (type.GetGenericArguments().Length == 0)
            {
                Debug.Log("��ȡArrayList");
                return (T)LoadArrayList(key);
            }
            else if (type.GetGenericArguments().Length == 1)
            {
                Debug.Log("��ȡList");
                return (T)LoadList(key, type);
            }
            return default(T);
        }
        //Dictionary
        else if (typeof(IDictionary).IsAssignableFrom(type))
        {
            Debug.Log("��ȡ�ֵ�");
            return (T)LoadDictionary(key, type);
        }
        //class struct
        else
        {
            Debug.Log("��ȡclass");
            return (T)LoadClass(key, type);
        }
    }
    
    private void SaveInt(string key,int value)
    {
        Debug.Log("��ǰkey��" + key);
        Debug.Log("�������ֵΪ��" + value);
        Debug.Log("==============================");

        PlayerPrefs.SetInt(key, EncryptInt(key, value));
    }

    private void SaveSingle(string key, float value)
    {
        Debug.Log("��ǰkey��" + key);
        Debug.Log("�������ֵΪ��" + value);
        Debug.Log("==============================");

        PlayerPrefs.SetFloat(key, value);
    }

    private void SaveString(string key, String value)
    {
        Debug.Log("��ǰkey��" + key);
        Debug.Log("�������ֵΪ��" + value);
        Debug.Log("==============================");

        PlayerPrefs.SetString(key, value);
    }

    private int LoadInt(string key)
    {
        int value = DecryptInt(key, PlayerPrefs.GetInt(key, key.Length));

        Debug.Log("��ǰkey��" + key);
        Debug.Log("��ȡ����ֵΪ��" + value);
        Debug.Log("==============================");

        return value;
    }

    private float LoadSingle(string key)
    {
        float value = PlayerPrefs.GetFloat(key, 0);

        Debug.Log("��ǰkey��" + key);
        Debug.Log("��ȡ����ֵΪ��" + value);
        Debug.Log("==============================");

        return value;
    }

    private string LoadString(string key)
    {
        string value = PlayerPrefs.GetString(key, "1");

        Debug.Log("��ǰkey��" + key);
        Debug.Log("��ȡ����ֵΪ��" + value);
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

        Debug.Log("�������鳤��Ϊ��" + count);

        SaveInt(key + "_Count", count);

        for (int i = 0; i < count; i++)
            Save(key + "_Index" + i, list[i]);
    }

    private object LoadArray(string key, Type type)
    {
        int count = Convert.ToInt32(Load(key + "_Count", typeof(int)));
        Type elementType = type.GetElementType();

        Debug.Log("��ȡ���鳤��Ϊ��" + count);
        Debug.Log("elementType��" + elementType);

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
            Debug.Log("�����ֶ�" + i);

            Debug.Log("�����������Ϊ��" + Type.GetTypeCode(fieldInfo.FieldType));
            Debug.Log("���������Ϊ��" + fieldInfo.Name);

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
            Debug.Log("��ȡ�ֶ�" + i);

            Debug.Log("��ȡ��������Ϊ��" + Type.GetTypeCode(fieldInfo.FieldType));
            Debug.Log("��ȡ������Ϊ��" + fieldInfo.Name);

            fieldInfo.SetValue(ret, Load(key + "_" + fieldInfo.Name, fieldInfo.FieldType));
        }

        return ret;
    }

    private void SaveArrayList(string key, object data)
    {
        IList list = data as IList;
        int count = list.Count;

        Debug.Log("����ArrayList����Ϊ��" + count);

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

        Debug.Log("��ȡArrayList����Ϊ��" + count);

        ArrayList ret = new();

        for (int i = 0; i < count; i++)
        {
            Type elementType = Type.GetType(LoadString(key + "_Index" + i + "_Type"));

            Debug.Log("elementType��" + elementType);

            ret.Add(Load(key + "_Index" + i, elementType));
        }

        return ret;
    }

    private void SaveList(string key, object data)
    {
        IList list = data as IList;
        int count = list.Count;
        SaveInt(key + "_Count", count);

        Debug.Log("����List����Ϊ��" + count);

        for (int i = 0; i < count; i++)
            Save(key + "_Index" + i, list[i]);
    }

    private object LoadList(string key, Type type)
    {
        int count = Convert.ToInt32(Load(key + "_Count", typeof(int)));
        Type valueType = type.GetGenericArguments()[0];

        Debug.Log("��ȡList����Ϊ��" + count);
        Debug.Log("valueType��" + valueType);

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
        Debug.Log("�����ֵ����Ϊ��" + count);

        for (int i = 0; i < count; i++)
        {
            enu.MoveNext();

            Debug.Log("����" + enu.Key);
            Debug.Log("ֵ��" + enu.Value);

            Save(key + "_Index" + i + "_Key", enu.Key);
            Save(key + "_Index" + i + "_Value", enu.Value);
        }
    }

    private object LoadDictionary(string key, Type type)
    {
        int count = Convert.ToInt32(Load(key + "_Count", typeof(int)));
        Type keyType = type.GetGenericArguments()[0];
        Type valueType = type.GetGenericArguments()[1];

        Debug.Log("��ȡ�ֵ����Ϊ��" + count);
        Debug.Log("keyType��" + keyType);
        Debug.Log("valueType��" + valueType);

        IDictionary ret = Activator.CreateInstance(type) as IDictionary;

        for (int i = 0; i < count; i++)
            ret.Add(Load(key + "_Index" + i + "_Key", keyType), Load(key + "_Index" + i + "_Value", valueType));

        return ret;
    }

    private int EncryptInt(string key, int value) => value + key.Length;

    private int DecryptInt(string key, int value) => value - key.Length;
}
