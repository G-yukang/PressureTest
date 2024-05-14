using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class XmlHandler<T>
{
    private string _filePath;

    public XmlHandler()
    {
        string rootDirectory = AppDomain.CurrentDomain.BaseDirectory;
        _filePath = rootDirectory + "HydropressXML.xml";

        // 检查文件是否存在
        if (!File.Exists(_filePath))
        {
            // 文件不存在，创建新文件
            CreateFile(_filePath);
        }

    }
    static void CreateFile(string filePath)
    {
        // 在指定路径创建新文件
        try
        {
            File.Create(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"文件创建失败：{ex.Message}");
        }
    }
    //写
    public bool WriteToFile(T data)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StreamWriter writer = new StreamWriter(_filePath))
        {
            serializer.Serialize(writer, data);
        }
        return true;
    }
    //读
    public T ReadFromFile()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StreamReader reader = new StreamReader(_filePath))
        {
            return (T)serializer.Deserialize(reader);
        }
    }
    //修
    public bool UpdateFile(T newData)
    {
        return WriteToFile(newData); 
    }
    //删
    public bool DeleteFile()
    {
        try
        {
            File.Delete(_filePath);
            return true; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"删除文件失败：{ex.Message}");
            return false; 
        }
    }
}