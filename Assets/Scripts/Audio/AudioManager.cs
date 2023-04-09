using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
  [SerializeField] AudioSource _gameMusicSource;
  [SerializeField] AudioSource _mainMenuMusicSource;
  public AudioSource GameMusicSource => _gameMusicSource;
  public AudioSource MainMenuMusicSource => _mainMenuMusicSource;
  [SerializeField] AudioSource _clickSoundSource;
  [SerializeField] AudioMixer _mixer;

  public void PlayClickSound()
  {
    _clickSoundSource.Play();
  }
}