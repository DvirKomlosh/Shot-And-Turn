
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem 
{
    public static void SaveData(int HighScore, int Gems, int BulletNum, int BulletSpeed, int TurnSpeed, int MovementSpeed) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "gameData.kom";
        FileStream stream = new FileStream(path, FileMode.Create);

        Data data = new Data(HighScore, Gems, BulletNum, BulletSpeed, TurnSpeed, MovementSpeed);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    
    public static void SaveData(Data data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "gameData.kom";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();

    }
    
    public static Data LoadData()
    {
        string path = Application.persistentDataPath + "gameData.kom";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();

            return data;
        }
        else 
        {
            return null;
        }
    }


}
