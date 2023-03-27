using UnityEngine;
using UnityEngine.Serialization;

namespace BlackPad.DropCube.Level
{
  public class Switch : MonoBehaviour
  {
    [SerializeField] Door doorComponent;

    [SerializeField] Light _pointLight;
    [SerializeField] Material _switchGreen;
    [SerializeField] Material _switchRed;
    [SerializeField] MeshRenderer _buttonRenderer;
    [SerializeField] Animator _animator;
    static readonly int Open = Animator.StringToHash("Open");
    void Start()
    {
      var boxCollider = gameObject.AddComponent<BoxCollider>();
      boxCollider.isTrigger = true;
      doorComponent = GetComponentInParent<Room>()
        .DoorComponent;

    }

    void OnTriggerEnter(Collider other)
    {
      if (!other.CompareTag("Player")) return;
      doorComponent.OpenDoor();
      _pointLight.color = _switchGreen.color;
      _buttonRenderer.material = _switchGreen;
      _animator.SetTrigger(Open);
    }
  }
}