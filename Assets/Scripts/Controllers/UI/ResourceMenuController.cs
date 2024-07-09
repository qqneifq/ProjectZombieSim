using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ResourceMenuController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] texts = new TextMeshProUGUI[5];


    // Start is called before the first frame update
    void Start()
    {
        ResourceHandler.OnResourceChange += ResourceChangeHandler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResourceChangeHandler(int index, int count, int max)
    {
        if(max > 0)
        {
            texts[index].text = count.ToString() + "/" + max.ToString();
        }
        else if(max == 0)
        {
            texts[index].text = "???";
        }
        else
        {
            texts[index].text = count.ToString();
        }
    }
    void DisplayResource(int i)
    {

    }

    void RefreshResources()
    {

    }
}
