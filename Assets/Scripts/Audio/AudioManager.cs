using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using Unity.VisualScripting;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Volume")]
    [Range(0, 1)]
    public float MusicVolume = 1;
    [Range(0, 1)]
    public float SFXVolume = 1;

    private Bus MusicBus;
    private Bus SFXBus;

    private List<EventInstance> eventInstances;
    private EventInstance musicEventInstance;

    
    

    public TimeLineInfo timelineinfo = null;
    private GCHandle timelineHandle;

    private FMOD.Studio.EVENT_CALLBACK beatCallback;

    public delegate void BeatEventDelegate();
    public static event BeatEventDelegate BeatUpdated;

    public delegate void MarkerListenerDelegate();
    public static event MarkerListenerDelegate MarkerUpdated;

    public delegate void TempoEventDelegate();
    public static event TempoEventDelegate TempoUpdated;

    public static int lastBeat = 0;
    public static string lastMarkerString = null;
    public static float lastTempo = 0;

    [StructLayout(LayoutKind.Sequential)]
    public class TimeLineInfo
    {
        public float tempo = 0;
        public int currentBeat = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Error, found more than one Audio Manager");
        }
        Instance = this;

        eventInstances = new List<EventInstance>();

        MusicBus = RuntimeManager.GetBus("bus:/Music");
        SFXBus = RuntimeManager.GetBus("bus:/SFX");
    }
    
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Level 1")
        {
            InitializeMusic(FMODEvents.Instance.MusicLevel1);
        }
        else if(SceneManager.GetActiveScene().name == "HUB")
        {
            InitializeMusic(FMODEvents.Instance.MusicHUB);
            
        }
        else if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            InitializeMusic(FMODEvents.Instance.MusicTutorial);
        }
        else
        {
            Debug.Log("B³¹d nie wyczytuje Sceny");
        }
        if(FMODEvents.Instance != null)
        {
            timelineinfo = new TimeLineInfo();
            beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
            timelineHandle = GCHandle.Alloc(timelineinfo, GCHandleType.Pinned);
            musicEventInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
            musicEventInstance.setCallback(beatCallback,
    FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        }
    }
    private void Update()
    {
        
        if (lastMarkerString != timelineinfo.lastMarker)
        {
            lastMarkerString = timelineinfo.lastMarker;
            if (MarkerUpdated != null)
            {
                MarkerUpdated();
            }
        }
        if (lastBeat != timelineinfo.currentBeat)
        {
            
            lastBeat = timelineinfo.currentBeat;
            if(BeatUpdated != null)
            {
                BeatUpdated();
            }
        }
        if(lastTempo != timelineinfo.tempo)
        {
            lastTempo = timelineinfo.tempo;
            if(TempoUpdated != null)
            {
                TempoUpdated();
            }
        }

        MusicBus.setVolume(MusicVolume);
        SFXBus.setVolume(SFXVolume);
    }
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }
    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }
    private void CleanUp()
    {
        foreach (var eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }
    private void OnDestroy()
    {

        CleanUp();
        musicEventInstance.setUserData(IntPtr.Zero);
        timelineHandle.Free();
    }
    private void OnGUI()
    {
        GUILayout.Box($"Current Beat = {timelineinfo.currentBeat},LastMarker = {(string)timelineinfo.lastMarker}");
    }
    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if(result != FMOD.RESULT.OK)
        {
            Debug.Log("Timeline Callback error :" + result);
            
        }
        else if(timelineInfoPtr != IntPtr.Zero)
        {
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimeLineInfo timelineInfo = (TimeLineInfo)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter =(FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentBeat = parameter.beat;
                        timelineInfo.tempo = parameter.tempo;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                    }
                    
                    break;
                
                
                    
            }
        }
        return FMOD.RESULT.OK;
    }
    public void Save(ref SaveVolumes data)
    {
        data.MusicVolume = MusicVolume;
        data.SFXVolume = SFXVolume;

    }
    public void Load(SaveVolumes data)
    {

        MusicVolume = data.MusicVolume;
        SFXVolume = data.SFXVolume;
        
    }

}

[System.Serializable]
public struct SaveVolumes
{
    public float MusicVolume;
    public float SFXVolume;

}

