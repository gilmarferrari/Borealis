using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSys
{
    private static readonly string _filename = $"/player_data.borealis";

    public static void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = Application.persistentDataPath + _filename;
        FileStream stream = new FileStream(savePath, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadData()
    {
        string savePath = Application.persistentDataPath + _filename;
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static PlayerData DeleteData()
    {
        string savePath = Application.persistentDataPath + _filename;
        File.Delete(savePath);

        return null;
    }
}
