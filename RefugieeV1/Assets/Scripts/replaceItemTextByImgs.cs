using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class replaceItemTextByImgs : MonoBehaviour
{

    public string[] myList;             //All items names

    public GameObject[] GOList;         //All items
    public GameObject Select;

    public ArrayList mySprites;           

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Replace(ArrayList itemList, ArrayList itemCount)
    {
        //if (this.GetComponent<GUIText>().text.Contains(item));
        for(int j = 0; j<myList.Length; j++)
        for (int i =0; i<itemList.Capacity; i++)
        {
                if (itemList[i] == myList[j])
                    mySprites.Add(Instantiate(GOList[j]));
        }
    }

    public void Selection(string item)
    {

        
    }


}
