using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public replaceItemTextByImgs imgs;
    public ArrayList myInventory = new ArrayList();
    public ArrayList myInventoryCount = new ArrayList();
    public Text bagHUD;
    public Text bagMenu;
    public int pointPos;


    public string[] StartItems;

    // Use this for initialization
    void Start ()
    {
        pointPos = 0;


        foreach (string s in StartItems)
            addItem(s);

        //              DEBUG - ADD FEW ITEMS AT START --
        /*
        addItem("Argent");
        addItem("Argent");
        addItem("Argent");

        addItem("Bijoux");
        addItem("Bijoux");
        addItem("Bijoux");

        addItem("Vivres");
        addItem("Vivres");

        addItem("Ourson");
        */

        //myText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        bagHUD.text = "Inventory: \n";
        for (int i = 0; i < myInventory.Count; i++)
        {
            bagHUD.text += myInventory[i] + "*" + myInventoryCount[i] + "\n";
        }

        bagMenu.text = "Bag: \n";
        for (int i = 0; i < myInventory.Count; i++)
        {
            if (i == pointPos)
                bagMenu.text += ">>> " + myInventory[i] + " x" + myInventoryCount[i]+ " <<<\n";
            else
                bagMenu.text += myInventory[i] + " x" + myInventoryCount[i]+ "\n";
        }
        //imgs.Replace(myInventory, myInventoryCount);
    }

    public void addItem(string item)
    {
        if (item != "")
        {
            //print("Additem: " + item + "..........");///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (myInventory.Contains(item)) //We already have this object, we add one to the counter
                myInventoryCount[myInventory.IndexOf(item)] = (int)myInventoryCount[myInventory.IndexOf(item)] + 1;
            else
            {
                myInventory.Add(item);
                myInventoryCount.Add(1);
            }
        }   
    }

    public void openClose(bool open)
    {
        if (open)
        {         
            bagMenu.text = "Bag: \n";
            bagMenu.enabled = true;
            for (int i = 0; i < myInventory.Count; i++)
            {
                if (i == pointPos)
                    bagMenu.text += ">>> " + myInventory[i] + " <<<\n";
                else
                    bagMenu.text += myInventory[i] + "\n";
            }
        }
        else
            bagMenu.enabled = false;

    }

    public string movePointer(bool up)
    {
        if(up)
        {
            if (pointPos <= 0)
                pointPos = myInventory.Count-1;
            else
                pointPos--;
        }
        else //down
        {
            if (pointPos >= myInventory.Count-1)
                pointPos = 0;
            else
                pointPos++;
        }
        return (string)(myInventory[pointPos]);
    }

    public int removeItem()
    {
        if ((int)myInventoryCount[pointPos] > 1)    //More than one item > Remove count
            myInventoryCount[pointPos] = (int)myInventoryCount[pointPos] - 1;
        else                                        //Only one item > Remove count AND remove item
        {            
            myInventory.RemoveAt(pointPos);
            myInventoryCount.RemoveAt(pointPos);
            if (pointPos >= myInventoryCount.Capacity)
                pointPos = 0;
        }
        return 1;
    }

    public int removeItem(string[] t)
    {
        if ((int)myInventoryCount[pointPos] > 1)    //More than one item > Remove count
        {
            for (int i = 0; i < t.Length; i++)
                if ((string)myInventory[pointPos] == t[i])
                {
                    myInventoryCount[pointPos] = (int)myInventoryCount[pointPos] - 1;
                    return 1;
                }
        }
        else                                        //Only one item > Remove count AND remove item
        {
            for(int i =0; i<t.Length;i++)
            if ((string)myInventory[pointPos] == t[i])
            {
                myInventory.RemoveAt(pointPos);
                myInventoryCount.RemoveAt(pointPos);
                    if (pointPos >= myInventoryCount.Capacity)
                        pointPos = 0;
                    return 1;
            }
        }
        return 0;
    }

}
