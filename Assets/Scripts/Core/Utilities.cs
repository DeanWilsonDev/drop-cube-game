using UnityEngine;

namespace BlackPad.Core.Utilities
{
  public static class Utilities {

    public static bool IsRandomlySelected(int randomValue) => randomValue < 50;

    public static float GetValueIfTrue(bool flag, float value) =>
      flag 
        ? value
        : 0;

    public static bool DetermineIfRandomlySelected(float threshold) => Random.Range(0, 100) < threshold;

    public static float GameObjectWidth(GameObject gObject) => gObject.transform.localScale.x;
    public static Vector3 GameObjectTransformPosition(GameObject gObject) => gObject.transform.position;

    public static float FindOriginPoint(GameObject gObject) =>
      gObject.transform.position.x - (GameObjectWidth(gObject) / 2);

    public static void SetGameObjectGlobalScale(
      GameObject obj,
      Vector3 globalScale)
    {
      obj.transform.localScale = Vector3.one;
      var lossyScale = obj.transform.lossyScale;
      obj.transform.localScale = new Vector3(
        globalScale.x / lossyScale.x,
        globalScale.y / lossyScale.y,
        globalScale.z / lossyScale.z
      );
    }

  }
}