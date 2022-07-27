using UnityEngine;

public class Constants
{
#if UNITY_EDITOR
    public static string SELFIE_PATH = @"D:\Documents\Unity Projects\Course_Face_Claps\AssetsBundle";
#else
public static string SELFIE_PATH = Application.persistentDataPath;
#endif

    public static string SELFIE_PRE_NAME = "Photo_";
    public static string SELFIE_EXTENSION = ".png";

    public static int PHOTOS_LENGTH_AMOUNT = 5;
}
