using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class FMODEvents : MonoBehaviour
{
    [field: Header("Music")]
    [field: SerializeField] public EventReference MusicLevel1 { get; private set; }
    [field: SerializeField] public EventReference MusicHUB { get; private set; }
    [field: SerializeField] public EventReference MusicTutorial { get; private set; }


    [field: Header("SFX")]

    [field: SerializeField] public EventReference CollectCrystal { get; private set; }
    [field: SerializeField] public EventReference CraftPotion { get; private set; }
    [field: SerializeField] public EventReference Healing { get; private set; }
    [field: SerializeField] public EventReference Swoosh { get; private set; }
    [field: SerializeField] public EventReference Punch { get; private set; }
    [field: SerializeField] public EventReference TeleportEntrance { get; private set; }
    [field: SerializeField] public EventReference Shoot { get; private set; }

    public static FMODEvents Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Error, found more than one FMOD Event");
        }
        Instance = this;
    }
}
