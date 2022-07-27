using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource soundEffect;
    public AudioSource soundMusic;
    public AudioSource environmentSound;
    public Sounds[] sound;
    public bool isMute;
    
    [Range(0f, 1f)]
    public float volume = 1f;

    #region SINGLETON
    public static SoundManager Instance { get; private set; }

    private void Awake() 
    {
        if(Instance == null) {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    #endregion
    
    private void Start() 
    {
        PlayMusic(global::SoundTypes.Music);    
    }

    public void Mute(bool status)
    {
        isMute = status;
    }
    public void SetVolume(float volume)
    {
        this.volume = volume;
        soundMusic.volume = this.volume;
        soundEffect.volume = this.volume;
    }

    public void PlayEnvironmentMusic(SoundTypes soundType)
    {   
        if(isMute) 
            return;
        AudioClip clip = GetSoundClip(soundType);
        if(clip != null) {
            environmentSound.clip = clip;
            environmentSound.Play();
        }   else {
                Debug.LogError("Clip not found for sound type: " + soundType );
        }
    }

    private void PlayMusic(SoundTypes soundType)
    {   
        if(isMute) 
            return;
        AudioClip clip = GetSoundClip(soundType);
        if(clip != null) {
            soundMusic.clip = clip;
            soundMusic.Play();
        }   else {
                Debug.LogError("Clip not found for sound type: " + soundType );
        }
    }

    public void Play(SoundTypes soundType)
    {  
         if(isMute)
            return;
         
         AudioClip clip = GetSoundClip(soundType);
         if(clip != null) {
             soundEffect.PlayOneShot(clip);
         }   else {
             Debug.LogError("Clip not found for sound type: " + soundType );
         }
    }

    private AudioClip GetSoundClip(SoundTypes soundType)
    {
        Sounds item = Array.Find(sound, item => item.soundType == soundType);
        if(item != null) 
           return item.soundClip;
        return null;
        
    }
}
[Serializable]
public class Sounds
{
    public SoundTypes soundType;
    public AudioClip soundClip;
}
public enum SoundTypes
{   
    StartButtonClick,
    BackButtonClick,
    RestartButtonClick,
    LevelSelected,
    EnvironmentalAmbiance,
    Music,
    MusicDeathSting,
    ButtonClick,
    ChomperAttack,
    Pickup,
    PlayerJump
    
}