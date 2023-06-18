using UnityEngine;
using UnityEngine.UI;

public class ImageView : MonoBehaviour
{
    private void Start()
    {
        transform.GetComponent<Image>().sprite = ImageInfoSaver.ImageTexture;
    }
}