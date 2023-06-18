using UnityEngine;
using UnityEngine.UI;

public class ImageInfoSaver : MonoBehaviour
{
    public static Sprite ImageTexture;

    public void SaveInfo()
    {
        ImageTexture = transform.GetComponent<Image>().sprite;
    }
}
