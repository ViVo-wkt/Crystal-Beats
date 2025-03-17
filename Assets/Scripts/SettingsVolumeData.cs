using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "Game/Settings")]
public class SettingsVolumeData : ScriptableObject
{
    public float musicVolume;
    public float SFXVolume;
}
