﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRoomChalkScript : MonoBehaviour
{
    public Texture2D cursorTexturePointer;
    public Texture2D cursorTextureHand;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpotPointer = Vector2.zero;
    public Vector2 hotSpotHand = Vector2.zero;
    public GameObject Antoni;
    public GameObject Elevator;
    private TextMesh BoardWritingField;
    bool isInteractable = false;
    bool boardInteraction = false;
    // Start is called before the first frame update

    void Start()
    {
        BoardWritingField = transform.GetChild(1).GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        if (isInteractable)
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
        boardInteraction = true;
        Debug.Log("Chalk got clicked on! :D");
        if(isInteractable)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(MoveWriteOnBoard(this.transform.position.x));
        }                  
    }

    IEnumerator MoveWriteOnBoard(float x)
    {

        Antoni.SendMessage("MoveToPosition", x);
        while (Antoni.transform.position.x != x)
        {
            yield return null;
        }
        Debug.Log("Antoni arrived at Chalk");
        Antoni.SendMessage("AllowPlayerToClick", false);
        Antoni.transform.GetComponent<Animator>().SetBool("IsWritingOnBoard", true);
        yield return new WaitForSeconds(2.0f);
        BoardWritingField.text = "Zawsze będę posłuszny...";
        //When player finished writing
        Antoni.SendMessage("AllowPlayerToClick", true); // <--- Add this line when player fininished writing on board and is free to walk and interact with other objects
        Antoni.transform.GetComponent<Animator>().SetBool("IsWritingOnBoard", false);
        Antoni.SendMessage("AllowPlayerToMove", true);
        Elevator.SendMessage("OpenElevator");
        isInteractable = false;
        //

    }

    void OnGUI()
    {
        Event e = Event.current;
        if(e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1 && char.IsLetter(e.keyCode.ToString()[0]) && boardInteraction)
        {
            Debug.Log("Detected key code: " + e.keyCode);
            BoardWritingField.text += e.keyCode;
            if(Input.GetKeyDown(KeyCode.Return))
                boardInteraction = false;
        }
    }
    
    public void ActivateChalk()
    {
        isInteractable = true;
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
