using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    public float width;

    void Start()
    {
        width = this.gameObject.transform.parent.GetComponent<RectTransform>().rect.width;
        Vector2 newSize = new Vector2(width / 4, width / 4);
        this.gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }
}
