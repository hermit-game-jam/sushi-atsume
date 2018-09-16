using UnityEngine;

public static class GameObjectExtensions
{
    public static void SetLayerRecursively(this GameObject self, int layer)
    {
        self.layer = layer;
        
        foreach (Transform it in self.transform)
        {
            SetLayerRecursively(it.gameObject, layer);
        }
    }
}
