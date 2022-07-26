using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance{ get { return instance; } }
    public AudioSource soundEffect;
    public AudioSource soundMusic;
    public AudioSource environmentSound;
    public SoundType[] Sounds;
    public bool IsMute = false;
    [Range(0f, 1f)]
    public float Volume = 1f;
   
    private void Awake() 
    {
        if(instance == null) {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    private void Start() 
    {
        PlayMusic(global::Sounds.Music);    
    }

    public void Mute(bool status)
    {
        IsMute = status;
    }
    public void SetVolume(float volume)
    {
        Volume = volume;
        soundMusic.volume = Volume;
        soundEffect.volume = Volume;
    }

    public void PlayEnvironmentMusic(Sounds sound)
    {   
        if(IsMute) 
            return;
        AudioClip clip = GetSoundClip(sound);
        if(clip != null) {
            environmentSound.clip = clip;
            environmentSound.Play();
        }   else {
                Debug.LogError("Clip not found for sound type: " + sound );
        }
    }
    public void PlayMusic(Sounds sound)
    {   if(IsMute) 
            return;
        AudioClip clip = GetSoundClip(sound);
        if(clip != null) {
            soundMusic.clip = clip;
            soundMusic.Play();
        }   else {
                Debug.LogError("Clip not found for sound type: " + sound );
        }
    }

    public void Play(Sounds sound)
    {  
         if(IsMute)
            return;
         AudioClip clip = GetSoundClip(sound);
         if(clip != null) {
             soundEffect.PlayOneShot(clip);
         }   else {
             Debug.LogError("Clip not found for sound type: " + sound );
         }
    }

    private AudioClip GetSoundClip(Sounds sound)
    {
        SoundType item = Array.Find(Sounds, i => i.soundType == sound);
        if(item != null) 
           return item.soundClip;
        return null;
        
    }
}
[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}
public enum Sounds
{   StartButtonClick,
    ExitButtonClick,
    RestartButtonClick,
    EnvironmentalAmbian,
    Music,
    ButtonClick,
    PlayerMove,
    PlayerDeath,
    EnemyDeath
}