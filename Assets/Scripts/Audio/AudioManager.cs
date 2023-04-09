using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
  [SerializeField] AudioSource _gameMusicSource;
  [SerializeField] AudioSource _menuMusicSource;
  public AudioSource GameMusicSource => _gameMusicSource;
  public AudioSource MenuMenuMusicSource => _menuMusicSource;
  [SerializeField] AudioSource _clickSoundSource;
  [SerializeField] AudioMixer _mixer;

  public void PlayClickSound()
  {
    _clickSoundSource.Play();
  }
}