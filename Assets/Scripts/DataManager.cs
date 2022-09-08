using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DataManager
{
    public static DataManager Instance { get; } = new();

    public bool debugEnabled = false;

    private string endKey;

    private bool main = true;

    public void Save(string key, object data)
    {
        if (main)
        {
            endKey = key;
            main = false;
        }

        Type dataType = data.GetType();
        TypeCode dataTypeCode = Type.GetTypeCode(dataType);

        debug("data type��" + dataType);
        debug("dataTypeCode��" + dataTypeCode);

        //enum
        if (dataType.IsEnum)
        {
            debug("����ö��");
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
            debug("��������");
            SaveArray(key, data);
        }
        //ArrayList GenericList
        else if (typeof(IList).IsAssignableFrom(dataType))
        {
            if (dataType.GetGenericArguments().Length == 0)
            {
                debug("����ArrayList");
                SaveArrayList(key, data);
            }
            else if (dataType.GetGenericArguments().Length == 1)
            {
                debug("����List");
                SaveList(key, data);
            }
        }
        //Dictionary
        else if (typeof(IDictionary).IsAssignableFrom(dataType))
        {
            debug("�����ֵ�");
            SaveDictionary(key, data);
        }
        //class struct
        else
        {
            debug("����class");
            SaveClass(key, data);
        }

        if (endKey == key)
        {
            PlayerPrefs.Save();
            Debug.Log(key + "�ѱ���");
            main = true;
        }
    }

    public object Load(string key, Type type)
    {
        if (main)
        {
            endKey = key;
            main = false;
        }

        TypeCode dataTypeCode = Type.GetTypeCode(type);

        debug("��ǰkey��" + key);
        debug("data type��" + type);

        object ret = null;

        if (type.IsEnum)
        {
            debug("��ȡö��");
            ret = LoadEnum(key, type);
        }
        //Boolean Char SByte Int16 Int32 Byte UInt16
        else if ((int)dataTypeCode >= 3 && (int)dataTypeCode <= 9)
            ret = Convert.ChangeType(LoadInt(key), type);
        //Single
        else if ((int)dataTypeCode == 13)
            ret = LoadSingle(key);
        //UInt32 Int64 UInt64 Double Decimal String
        else if ((int)dataTypeCode >= 10 && (int)dataTypeCode <= 18)
            ret = Convert.ChangeType(LoadString(key), type);
        //array
        else if (type.IsArray)
        {
            debug("��ȡ����");
            ret = LoadArray(key, type);
        }
        //ArrayList GenericList
        else if (typeof(IList).IsAssignableFrom(type))
        {
            if (type.GetGenericArguments().Length == 0)
            {
                debug("��ȡArrayList");
                ret = LoadArrayList(key);
            }
            else if (type.GetGenericArguments().Length == 1)
            {
                debug("��ȡList");
                ret = LoadList(key, type);
            }
        }
        //Dictionary
        else if (typeof(IDictionary).IsAssignableFrom(type))
        {
            debug("��ȡ�ֵ�");
            ret = LoadDictionary(key, type);
        }
        //class struct
        else
        {
            debug("��ȡclass");
            ret = LoadClass(key, type);
        }

        if (endKey == key)
        {
            main = true;
            Debug.Log(key + "�Ѷ�ȡ");
        }

        return ret;
    }

    public T Load<T>(string key)
    {
        if (main)
        {
            endKey = key;
            main = false;
        }

        Type type = typeof(T);

        TypeCode dataTypeCode = Type.GetTypeCode(type);

        debug("��ǰkey��" + key);
        debug("data type��" + type);

        object ret = null;

        if (type.IsEnum)
        {
            debug("��ȡö��");
            ret = LoadEnum(key, type);
        }
        //Boolean Char SByte Int16 Int32 Byte UInt16
        else if ((int)dataTypeCode >= 3 && (int)dataTypeCode <= 9)
            ret = Convert.ChangeType(LoadInt(key), type);
        //Single
        else if ((int)dataTypeCode == 13)
            ret = Convert.ChangeType(LoadSingle(key), type);
        //UInt32 Int64 = 11 UInt64 Double Decimal String
        else if ((int)dataTypeCode >= 10 && (int)dataTypeCode <= 18)
            ret = Convert.ChangeType(LoadString(key), type);
        //array
        else if (type.IsArray)
        {
            debug("��ȡ����");
            ret = LoadArray(key, type);
        }
        //ArrayList GenericList
        else if (typeof(IList).IsAssignableFrom(type))
        {
            if (type.GetGenericArguments().Length == 0)
            {
                debug("��ȡArrayList");
                ret = (T)LoadArrayList(key);
            }
            else if (type.GetGenericArguments().Length == 1)
            {
                debug("��ȡList");
                ret = LoadList(key, type);
            }
        }
        //Dictionary
        else if (typeof(IDictionary).IsAssignableFrom(type))
        {
            debug("��ȡ�ֵ�");
            ret = LoadDictionary(key, type);
        }
        //class struct
        else
        {
            debug("��ȡclass");
            ret = LoadClass(key, type);
        }

        if (endKey == key)
        {
            main = true;
            Debug.Log(key + "�Ѷ�ȡ");
        }

        return (T)ret;
    }
    
    private void debug(string msg)
    {
        if (debugEnabled)
            Debug.Log(msg);
    }

    private void SaveInt(string key,int value)
    {
        debug("��ǰkey��" + key);
        debug("�������ֵΪ��" + value);
        debug("==============================");

        PlayerPrefs.SetInt(key, EncryptInt(key, value));
    }

    private void SaveSingle(string key, float value)
    {
        debug("��ǰkey��" + key);
        debug("�������ֵΪ��" + value);
        debug("==============================");

        PlayerPrefs.SetFloat(key, value);
    }

    private void SaveString(string key, String value)
    {
        debug("��ǰkey��" + key);
        debug("�������ֵΪ��" + value);
        debug("==============================");

        PlayerPrefs.SetString(key, value);
    }

    private int LoadInt(string key)
    {
        int value = DecryptInt(key, PlayerPrefs.GetInt(key, key.Length));

        debug("��ǰkey��" + key);
        debug("��ȡ����ֵΪ��" + value);
        debug("==============================");

        return value;
    }

    private float LoadSingle(string key)
    {
        float value = PlayerPrefs.GetFloat(key, 0);

        debug("��ǰkey��" + key);
        debug("��ȡ����ֵΪ��" + value);
        debug("==============================");

        return value;
    }

    private string LoadString(string key)
    {
        string value = PlayerPrefs.GetString(key, "1");

        debug("��ǰkey��" + key);
        debug("��ȡ����ֵΪ��" + value);
        debug("==============================");

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

        debug("�������鳤��Ϊ��" + count);

        SaveInt(key + "_Count", count);

        for (int i = 0; i < count; i++)
            Save(key + "_Index" + i, list[i]);
    }

    private object LoadArray(string key, Type type)
    {
        int count = Convert.ToInt32(Load(key + "_Count", typeof(int)));
        Type elementType = type.GetElementType();

        debug("��ȡ���鳤��Ϊ��" + count);
        debug("elementType��" + elementType);

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
            debug("�����ֶ�" + i);

            debug("�����������Ϊ��" + Type.GetTypeCode(fieldInfo.FieldType));
            debug("���������Ϊ��" + fieldInfo.Name);

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
            debug("��ȡ�ֶ�" + i);

            debug("��ȡ��������Ϊ��" + Type.GetTypeCode(fieldInfo.FieldType));
            debug("��ȡ������Ϊ��" + fieldInfo.Name);

            fieldInfo.SetValue(ret, Load(key + "_" + fieldInfo.Name, fieldInfo.FieldType));
        }

        return ret;
    }

    private void SaveArrayList(string key, object data)
    {
        IList list = data as IList;
        int count = list.Count;

        debug("����ArrayList����Ϊ��" + count);

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

        debug("��ȡArrayList����Ϊ��" + count);

        ArrayList ret = new();

        for (int i = 0; i < count; i++)
        {
            Type elementType = Type.GetType(LoadString(key + "_Index" + i + "_Type"));

            debug("elementType��" + elementType);

            ret.Add(Load(key + "_Index" + i, elementType));
        }

        return ret;
    }

    private void SaveList(string key, object data)
    {
        IList list = data as IList;
        int count = list.Count;
        SaveInt(key + "_Count", count);

        debug("����List����Ϊ��" + count);

        for (int i = 0; i < count; i++)
            Save(key + "_Index" + i, list[i]);
    }

    private object LoadList(string key, Type type)
    {
        int count = Convert.ToInt32(Load(key + "_Count", typeof(int)));
        Type valueType = type.GetGenericArguments()[0];

        debug("��ȡList����Ϊ��" + count);
        debug("valueType��" + valueType);

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
        debug("�����ֵ����Ϊ��" + count);

        for (int i = 0; i < count; i++)
        {
            enu.MoveNext();

            debug("����" + enu.Key);
            debug("ֵ��" + enu.Value);

            Save(key + "_Index" + i + "_Key", enu.Key);
            Save(key + "_Index" + i + "_Value", enu.Value);
        }
    }

    private object LoadDictionary(string key, Type type)
    {
        int count = Convert.ToInt32(Load(key + "_Count", typeof(int)));
        Type keyType = type.GetGenericArguments()[0];
        Type valueType = type.GetGenericArguments()[1];

        debug("��ȡ�ֵ����Ϊ��" + count);
        debug("keyType��" + keyType);
        debug("valueType��" + valueType);

        IDictionary ret = Activator.CreateInstance(type) as IDictionary;

        for (int i = 0; i < count; i++)
            ret.Add(Load(key + "_Index" + i + "_Key", keyType), Load(key + "_Index" + i + "_Value", valueType));

        return ret;
    }

    private int EncryptInt(string key, int value) => value + key.Length;

    private int DecryptInt(string key, int value) => value - key.Length;
}
