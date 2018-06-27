using UnityEngine;
using System.Collections;

public class YCoordinateSpritePositioner : MonoBehaviour 
{
    public Transform baseLocation;
    public SpriteRenderer[] spriteRenderers;
    public int[] relativeLayerChanges;

    private void Update()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sortingOrder = (Mathf.RoundToInt(baseLocation.position.y * 100f) * -1) + relativeLayerChanges[i];
        }

    }

}
