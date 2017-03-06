using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerController : MonoBehaviour
{
    
    public int maxValue = 100;
    public int minValue = 1;

    public float minPos = 30;
    public float maxPos = 1;

    public float front = 0.5f;
    public float middle = 1f;
    public float back = 1.5f;

    public bool autoSortB = false;

    public SpriteRenderer[] CharactersSprites;
	// Use this for initialization
	void Start ()
    {
        

        SpriteRenderer[] s = FindObjectsOfType(typeof(SpriteRenderer)) as SpriteRenderer[];
        Debug.Log("Nombre de sprites trouvés ! : " + s.Length);
       
        foreach (SpriteRenderer sprite in s)
            sprite.sortingOrder = (int)((sprite.transform.position.z - 50) * -50 / 10);

        ParticleSystemRenderer[] PS = (FindObjectsOfType(typeof(ParticleSystemRenderer)) as ParticleSystemRenderer[]);
        Debug.Log("Nombre de particles trouvés ! : " + PS.Length);

        foreach (ParticleSystemRenderer p in PS)
            p.sortingOrder = (int)((p.transform.position.z - 50) * -50 / 10);
        /*
        MeshRenderer[] m = FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
        print("Nombre de MeshRenderer trouvés ! : " + m.Length);

        foreach (MeshRenderer mats in m)
            if(mats.material.name != "clay")
            mats.material.renderQueue = (int)((mats.transform.position.z - 50) * -50 )*10;*/
        //mats.GetComponent<Material>()
        //mats.GetComponent<Material>().renderQueue = (int)((mats.transform.position.z - 30) * -30 / 10);


        //mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(this.transform.position.z + "");///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (!autoSortB)
        {
            /*    if (transform.position.z < front)
                    DadSprite.sortingOrder = maxValue;
                else if (transform.position.z < middle)
                    DadSprite.sortingOrder = 55;
                else if (transform.position.z < back)
                    DadSprite.sortingOrder = minValue;*/
        }
        if (autoSortB)
            autoSort();
	}

    void autoSort()
    {
        // Max = position 1                                           --  Valeur Max = 100
        // Min = Position = 30                                         --  Valeur Min = 1

        //PositionZ                                                     --  CALCUL !  
        //SortingLayer order = position-30*-10
        //30/100 -> 1pas
        foreach(SpriteRenderer sr in CharactersSprites)
            sr.sortingOrder = (int)((sr.transform.position.z - 50) * -50 / 10);

        
    }
}
