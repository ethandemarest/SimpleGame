using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRating : MonoBehaviour
{
    public Image image;
    public Sprite[] stars;
    public void UpdateStars(int starCount)
    {
        GetComponent<Image>().sprite = stars[starCount];
    }
}
