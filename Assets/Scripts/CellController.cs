using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    public float width;
    public int cellCount = 4;
    public GameObject target;

    void Start()
    {
        //print(transform.parent.GetComponent<RectTransform>().rect);
        width = target.GetComponent<RectTransform>().rect.width;
        Vector2 newSize = new Vector2(width / cellCount, width / cellCount);
        GetComponent<GridLayoutGroup>().cellSize = newSize;
    }
}
