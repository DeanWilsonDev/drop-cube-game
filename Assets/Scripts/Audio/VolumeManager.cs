using UnityEngine;
using UnityEngine.Audio;

namespace BlackPad.DropCube.Audio
{
    public class VolumeManager : MonoBehaviour
    {
      [SerializeField] AudioMixer _mixer;
      
      public void SetMusicLevel(float sliderValue)
      {
        _mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
      }
      
      public void SetSoundEffectsLevel(float sliderValue)
      {
        _mixer.SetFloat("SoundEffectsVolume", Mathf.Log10(sliderValue) * 20);
      }
    }
}
