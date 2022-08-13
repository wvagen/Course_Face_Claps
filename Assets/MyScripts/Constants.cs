using UnityEngine;

public class Constants
{
#if UNITY_EDITOR
    //public static string SELFIE_PATH = @"D:\Documents\Unity Projects\Course_Face_Claps\AssetsBundle"; For PC
    public static string SELFIE_PATH = @"/Users/mouadhmkadmi/Library/Application Support/Mkadmi/BelKaff/"; // For Mac
#else
public static string SELFIE_PATH = Application.persistentDataPath;
#endif

    public static string SELFIE_PRE_NAME = "Photo_";
    public static string SELFIE_EXTENSION = ".png";

    public static int PHOTOS_LENGTH_AMOUNT = 6;
}
