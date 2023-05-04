using UnityEngine;

public class ScrollUV : MonoBehaviour
{
    public Material _material;
    public float currentscroll;
    public float speed;
    public Vector2 vector;

    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        currentscroll += speed * Time.deltaTime;
        _material.mainTextureOffset = vector * currentscroll;
    }
}