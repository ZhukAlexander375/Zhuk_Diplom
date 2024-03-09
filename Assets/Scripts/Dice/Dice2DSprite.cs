using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dice2DSprite : MonoBehaviour, IDiceTopShow
{
    //[SerializeField] private Sprite[] _diceFaces;
    //private Image diceTopFaceImage;


    private int topFace;

    public static UnityEvent<int> OnDiceTopFace = new();

    public int GetNumberOfTopFace()
    {
        topFace = Random.Range(1, 7);
        OnDiceTopFace.Invoke(topFace);

        /*if (diceTopFaceImage != null && topFace >= 1 && topFace <= _diceFaces.Length)
        {
            diceTopFaceImage.sprite = _diceFaces[topFace - 1];
        } */ 
        
        return topFace;
    }

   /* public Image GetDiceTopFaceImage()
    {
        return diceTopFaceImage;
    }

    public Sprite[] GetDiceFaces()
    {
        return _diceFaces;
    }*/
}
