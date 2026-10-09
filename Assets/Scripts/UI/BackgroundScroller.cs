using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed = new Vector2(0f, 0.2f);
    Vector2 offset;
    Material material;

    void Start()
    {
        material = new Material(GetComponent<Image>().material);
        GetComponent<Image>().material = material;
    }


    void Update()
    {
        offset += moveSpeed * Time.deltaTime;
        material.mainTextureOffset = offset;
    }

}
