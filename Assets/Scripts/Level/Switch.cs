using UnityEngine;
using UnityEngine.Serialization;

namespace BlackPad.DropCube.Level
{
  public class Switch : MonoBehaviour
  {
    [SerializeField] Door _doorComponent;

    [SerializeField] Light _pointLight;
    [SerializeField] Material _switchGreen;
    [SerializeField] Material _switchRed;
    [SerializeField] MeshRenderer _buttonRenderer;
    [SerializeField] Animator _animator;
    [SerializeField] AudioSource _audioSource;
    static readonly int Open = Animator.StringToHash("Open");
    bool _isClicked;
    void Start()
    {
      _isClicked = false;
      var boxCollider = gameObject.AddComponent<BoxCollider>();
      boxCollider.isTrigger = true;
      _doorComponent = GetComponentInParent<Room>()
        .DoorComponent;
      _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
      if (_isClicked) return;
      if (!other.CompareTag("Player")) return;
      _doorComponent.OpenDoor();
      
      _pointLight.color = _switchGreen.color;
      _buttonRenderer.material = _switchGreen;
      _animator.SetTrigger(Open);
      _audioSource.Play();
      _isClicked = true;
    }
  }
}