using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplianceAttribute : MonoBehaviour
{
    public float Radius;
    // Start is called before the first frame update
    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Vector2 sprite_size = sprite.rect.size;
        Vector2 local_sprite_size = sprite_size / sprite.pixelsPerUnit;
        Radius = local_sprite_size.x / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsCollidingWith(float x, float y)
    {
        float distance = Util.FindDistance(transform.position, new Vector2(x, y));
        return distance <= Radius;

    }
}

public class Util
{
    public static float FindDistance(Vector2 p1, Vector2 p2)
    {
        //finds distance between p1 and p2 using vector subtraction
        float a = p1.x - p2.x;
        float b = p1.y - p2.y;
        return Mathf.Sqrt((a * a) + (b * b));
    }
}
