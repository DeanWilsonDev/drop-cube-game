using UnityEngine;
using UnityEngine.Audio;

namespace BlackPad.DropCube.Audio
{
    public class VolumeManager : MonoBehaviour
    {
      [SerializeField] AudioMixer _mixer;
      void Start()
      {
        var musicVolume = PlayerPrefs.GetFloat("MusicVolume"); 
        _mixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);

        var soundEffectsVolume = PlayerPrefs.GetFloat("SoundEffectsVolume");
        _mixer.SetFloat("SoundEffectsVolume", Mathf.Log10(soundEffectsVolume) * 20);
      }

      public void SetMusicLevel(float sliderValue)
      {
        _mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
      }
      
      public void SetSoundEffectsLevel(float sliderValue)
      {
        _mixer.SetFloat("SoundEffectsVolume", Mathf.Log10(sliderValue) * 20);
      }

      public void SaveVolume()
      {
        _mixer.GetFloat("SoundEffectsVolume", out var soundEffectsVolume);
        PlayerPrefs.SetFloat("SoundEffectsVolume", soundEffectsVolume);
        
        _mixer.GetFloat("MusicVolume", out var musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
      }
    }
}
