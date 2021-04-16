using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RulesMenu : MonoBehaviour
{
    public GameObject[] items;
    private int index = 0;

    public void getNext()
    {
        items[index].SetActive(false);
        index++;

        if (index > items.Length - 1)
        {
            index = 0;
        }

        // Show next model
        items[index].SetActive(true);

    }

    public void getPrev()
    {
        // Hide current model
        items[index].SetActive(false);

        index--;
        if (index < 0)
        {
            index = items.Length - 1;
        }

        // Show previous model
        items[index].SetActive(true);
    }
}
