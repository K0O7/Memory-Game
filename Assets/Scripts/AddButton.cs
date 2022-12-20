using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButton : MonoBehaviour
{
    [SerializeField]
    private Transform cardField;

    [SerializeField]
    private GameObject startCard;

    int rows = 2;
    int columns = 2;
    float x = 160f;
    float y = -130f;

    private void Awake()
    {
        rows = PlayerPrefs.GetInt("rows");
        columns = PlayerPrefs.GetInt("columns");
        int cardCount = rows * columns;
        for(int i = 1; i < cardCount; i++)
        {
            x += 1100f/(rows + columns);
            if (i % columns == 0)
            {
                y -= 850f / (rows + columns);
                x = 160f;
            }
            GameObject card = Instantiate(startCard, new Vector3(x,y,0f),Quaternion.identity);
            card.name = "" + i;
            card.transform.SetParent(cardField, false);

        }
    }

}
