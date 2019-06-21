﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShopProductScript : MonoBehaviour
{
    public Texture2D cursorTexturePointer;
    public Texture2D cursorTextureHand;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpotPointer = Vector2.zero;
    public Vector2 hotSpotHand = Vector2.zero;
    public int productIndex;
    GameObject Antoni;
    // Start is called before the first frame update
    void Start()
    {
        Antoni = transform.parent.GetComponent<ShopProductCollectionScript>().Antoni;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        if (this.transform.parent.GetComponent<ShopProductCollectionScript>().ChoosenProducts.Count < 3)
        {
            Cursor.SetCursor(cursorTextureHand, hotSpotHand, cursorMode);
        }
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorTexturePointer, hotSpotPointer, cursorMode);
    }

    void OnMouseDown()
    {
        if (this.transform.parent.GetComponent<ShopProductCollectionScript>().ChoosenProducts.Count < 3)
        {
            StartCoroutine(GetTheProduckt());
        }
        
    }

    IEnumerator GetTheProduckt()
    {
        Antoni.SendMessage("MoveToPosition", this.transform.position.x);
        this.transform.parent.GetComponent<ShopProductCollectionScript>().SendMessage("PickProduct", productIndex);
        Cursor.SetCursor(cursorTexturePointer, hotSpotPointer, cursorMode);
        while (Antoni.transform.position.x!= this.transform.position.x)
        {
            yield return null;
        }
        
        this.gameObject.SetActive(false);
    }
}
