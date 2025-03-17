using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using static WeaponManager;

public class SaveScript : MonoBehaviour
{
    public static SaveValues saveValues = new SaveValues();

    [System.Serializable]
    public struct SaveValues
    {
        public InventorySavedData inventorySavedData;
        public PotionSaveData potionSaveData;
        public PlayerHpData playerHpData;
        public WeaponSaveData weaponSaveData;
        public SaveVolumes saveVolumes;
        public SaveToggles saveToggles;
        
    }


    
    public static void SaveToJson()
    {

        InventoryManager.Instance.Save(ref saveValues.inventorySavedData);
        LayoutPotions.Instance.Save(ref saveValues.potionSaveData);
        Player_Info.Instance.Save(ref saveValues.playerHpData);
        WeaponManager.Instance.Save(ref saveValues.weaponSaveData);
        AudioManager.Instance.Save(ref saveValues.saveVolumes);
        Settings.instance.Save(ref saveValues.saveToggles);
        

        string SaveValuesData = JsonUtility.ToJson(saveValues, true);
        string FilePath = Application.persistentDataPath + "/SaveValuesData.json";
        Debug.Log(FilePath);
        System.IO.File.WriteAllText(FilePath, SaveValuesData);
        Debug.Log("saved");
    }
    
    public static void LoadFromJson()
    {
        string FilePath = Application.persistentDataPath + "/SaveValuesData.json";
        if(System.IO.File.Exists(FilePath))
        {
            string SaveValuesData = System.IO.File.ReadAllText(FilePath);

            saveValues = JsonUtility.FromJson<SaveValues>(SaveValuesData);

            InventoryManager.Instance.Load(saveValues.inventorySavedData);
            LayoutPotions.Instance.Load(saveValues.potionSaveData);
            Player_Info.Instance.Load(saveValues.playerHpData);
            WeaponManager.Instance.Load(saveValues.weaponSaveData);
            AudioManager.Instance.Load(saveValues.saveVolumes);
            Settings.instance.Load(saveValues.saveToggles);
            
            Debug.Log("Loaded");
        }
        
    }
    public static void ClearJsonData()
    {
        string FilePath = Application.persistentDataPath + "/SaveValuesData.json";
        if (System.IO.File.Exists(FilePath))
        {
            
            System.IO.File.Delete(FilePath);
        }
        else
        {
            Debug.Log("nie istnieje");
        }
        
    }
    // ³adowanie ustawieñ z Menu
    //public static void LoadFromJsonMenu()
    //{
    //    string FilePath = Application.persistentDataPath + "/SaveValuesDataMenu.json";
    //    if (System.IO.File.Exists(FilePath))
    //    {

    //        string SaveValuesData = System.IO.File.ReadAllText(FilePath);

    //        saveValues = JsonUtility.FromJson<SaveValues>(SaveValuesData);
    //        ToggleVolume.Instance.Load(saveValues.saveVolumes);
    //    }
    //    else
    //    {
    //        Debug.Log("Wczytane dane z Menu LOad");
    //    }
    //}
    //public static void SaveToJsonFromMenu()
    //{
    //    ToggleVolume.Instance.Save(ref saveValues.saveVolumes);

    //    string SaveValuesData = JsonUtility.ToJson(saveValues, true);
    //    string FilePath = Application.persistentDataPath + "/SaveValuesDataMenu.json";
    //    Debug.Log(FilePath);
    //    System.IO.File.WriteAllText(FilePath, SaveValuesData);
    //    Debug.Log("savedMenu");
    //}
    //public static void ClearJsonDataMenu()
    //{
    //    string FilePath = Application.persistentDataPath + "/SaveValuesDataMenu.json";
    //    if (System.IO.File.Exists(FilePath))
    //    {

    //        System.IO.File.Delete(FilePath);
    //    }
    //    else
    //    {
    //        Debug.Log("nie istniejeMenu");
    //    }

    //}
    // ³adowanie ustawieñ z Menu
}






