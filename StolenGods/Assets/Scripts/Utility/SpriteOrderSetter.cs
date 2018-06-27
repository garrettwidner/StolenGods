using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SpriteOrderSetter : MonoBehaviour
{
    public bool moves;
    public Transform baseLocation;
    public SpriteRenderer[] spriteRenderers;
    public int[] relativeLayerChanges;

    private void Start()
    {
        SetSpriteOrder();
    }

    private void Update()
    {
        if(moves)
        {
            SetSpriteOrder();
        }

        if(!Application.isPlaying)
        {
            SetSpriteOrder();
        }
    }

    private void SetSpriteOrder()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sortingOrder = (Mathf.RoundToInt(baseLocation.position.y * 100f) * -1) + relativeLayerChanges[i];
        }
    }

}