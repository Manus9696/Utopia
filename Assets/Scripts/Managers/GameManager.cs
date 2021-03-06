﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;        
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform _camera;
    [SerializeField] GameObject _player;
    [SerializeField] MapGeneratorScript _mapGeneratorScript;
    int xSign;
    int ySign;
    private Vector3[] _SpritePositions = new Vector3[] { new Vector3(-5.5f,-3,0), new Vector3(0,-3,0), new Vector3(5.5f,-3,0), new Vector3(-5.5f,0,0),
                                           new Vector3(0,0,0),  new Vector3(5.5f,0,0), new Vector3(-5.5f,3,0), new Vector3(0,3,0), new Vector3(5.5f,3,0)};
    public int actScene;   
    public int sceneOffset;
    public int collumns;
    public int rows;
    static public Vector2 lastPos = new Vector2(0, -0.89f);

    void GenerateRoomSprites()
    {
        for (int i = 0; i < 9; i++)
        {
            if (i == 4) { continue; }
            Debug.Log("RoomSprites/OneRoom/Room" + _mapGeneratorScript.currentRoomGrid[i].roomIndex);
            Instantiate(_mapGeneratorScript.RoomSprites[i], _SpritePositions[i], Quaternion.identity);
        }
    }

    void Start ()
    {
        //GenerateRoomSprites();
        xSign = 1;
        ySign = 1;
        collumns = 3;
        rows = 3;
        actScene = SceneManager.GetActiveScene().buildIndex + 1;
        _player.transform.position = lastPos;
        _mapGeneratorScript = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGeneratorScript>();
        GenerateRoomSprites();
	}   

    public IEnumerator MoveCamera(string axis, float distToMove)
    {   
        float pos = 0; // Camera move increaser  
        SetValues(axis);      
                                           
        while(distToMove > Mathf.Abs(pos))
        {
            Vector3 move = (axis == "x") ? new Vector3(xSign * pos, 0f, -5f) : new Vector3(0f, ySign * pos, -5f);
            _camera.position = move;
            pos += Time.deltaTime;                                                                      
            yield return null;
        }
        ChangeScene(actScene + sceneOffset);
    }

    public void ChangeScene(int index)
    {
        //SCENE NUMBERTION HAS BEEN FIXED
        SceneManager.LoadScene(index);
        _player.SetActive(true);
    }

    public void CheckBoundaries()
    {                                      
        if((actScene % rows == 1) && (xSign == -1))        // Left boundary
            xSign = 1;
        if((actScene % rows == 0) && (xSign == 1))    // Right boundary
            xSign = -1;
        if((actScene <= collumns) && (ySign == -1))        // Bottom boundary
            ySign = 1;
        if((actScene > (collumns * rows - collumns)) && (ySign == 1))         // Top boundary
            ySign = -1;
    }

    public void SetValues(string axis)
    {
        _player.SetActive(false);                                       // Disable player on scene change     
        xSign = (Random.Range(0, 2) % 2 == 0) ? -1 : 1;
        ySign = (Random.Range(0, 2) % 2 == 0) ? -1 : 1;
        CheckBoundaries();                                              // Checking boundaries
        sceneOffset = (axis == "x") ? 1 * xSign : collumns * ySign;    // If left/right then scene by 1 
        // Setting next pos whether I used eleator or corridor
        lastPos = (axis == "x") ? new Vector2(-2.16f, _player.transform.position.y)
                                : new Vector2(-1f, _player.transform.position.y);
    }    
}
                                                                                                        