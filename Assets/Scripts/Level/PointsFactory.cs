using UnityEngine;
using TMPro;

namespace BlackPad.DropCube.Level
{
  public class PointsFactory
  {
    public Points Build(
      Component parent,
      int pointsValue
    )
    {
      var parentTransform = parent.transform;
      var parentTransformPosition = parentTransform.position;
      
      var points = new GameObject
        {
          name = "Points",
          transform =
          {
            parent = parentTransform
          }
        }
        .AddComponent<TextMeshPro>();

      points.text = pointsValue.ToString();
      points.fontSize = 75;
      points.alignment = TextAlignmentOptions.Center;
      
      var roomScoreTextTransform = points.transform;
      roomScoreTextTransform.position = new Vector3(
        parentTransformPosition.x,
        parentTransformPosition.y,
        parentTransformPosition.z + 2
      );

      return points.gameObject.AddComponent<Points>();
    }
  }
}