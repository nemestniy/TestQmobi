using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBordersManager : MonoBehaviour
{
    private BoxCollider2D LeftBorder;
    private BoxCollider2D RightBorder;
    private BoxCollider2D TopBorder;
    private BoxCollider2D BottomBorder;

    private string LeftBorderName = "LeftBorder";
    private string RightBorderName = "RightBorder";
    private string TopBorderName = "TopBorder";
    private string BottomBorderName = "BottomBorder";

    public void Initialize ()
    {
        LeftBorder = GetBoxCollider2D(LeftBorderName);
        RightBorder = GetBoxCollider2D(RightBorderName);
        TopBorder = GetBoxCollider2D(TopBorderName);
        BottomBorder = GetBoxCollider2D(BottomBorderName);

        var width = new Vector2(1, ObjectsHolder.FrameSize.y * 2);
        var height = new Vector2(ObjectsHolder.FrameSize.x * 2, 1);

        LeftBorder.size = width;
        RightBorder.size = width;
        TopBorder.size = height;
        BottomBorder.size = height;

        LeftBorder.transform.position = Vector2.left * (height.x + width.x) * 0.5f;
        RightBorder.transform.position = Vector2.right * (height.x + width.x) * 0.5f;
        TopBorder.transform.position = Vector2.up * (width.y + height.y) * 0.5f;
        BottomBorder.transform.position = Vector2.down * (width.y + height.y) * 0.5f;
    }

    private BoxCollider2D GetBoxCollider2D(string name)
    {
        var obj = transform.Find(name);

        if(obj != null)
        {
            var collider = obj.GetComponent<BoxCollider2D>();
            if(collider != null)
            {
                return collider;
            }
            else
            {
                Debug.LogError(name + " object has no BoxCollider2D component.");
            }
        }
        else
        {
            Debug.LogError(name + " object was not found.");
        }

        return null;
    }
}
