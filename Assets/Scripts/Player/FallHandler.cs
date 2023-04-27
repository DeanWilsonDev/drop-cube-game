using UnityEngine;

namespace BlackPad.DropCube.Player
{
  public class FallHandler : MonoBehaviour
  {
    public bool IsGrounded { get; set; }
    const string GroundLayer = "Ground";
    LayerMask _groundLayerMask;
    RaycastHit _hit;
    
    // Start is called before the first frame update
    void Start()
    {
      _groundLayerMask = LayerMask.NameToLayer(GroundLayer);
    }

    // Update is called once per frame
    void Update()
    
    {
      CheckIsGrounded();
    }

    void CheckIsGrounded()
    {
      IsGrounded = Physics.Raycast(
        transform.position, 
        Vector3.down, 
        out _hit, 
        1f,
        _groundLayerMask
      );
    }
  }
}