using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;

public class TestClick : MonoBehaviour
{
    bool selected = false;
    static Vector2 mousePosition;
    
    private void Update()
    {
        if (selected)
        {
            Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(vector.x, vector.y);
        }

    }
    private void OnMouseUp()
    {
        transform.position = new Vector2(mousePosition.x, mousePosition.y);
        selected = false;
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = new Vector2(transform.position.x, transform.position.y);
            selected = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (gameObject.name==collision.gameObject.name)
        {
            mousePosition = collision.gameObject.transform.position;
            gameObject.GetComponent<TestClick>().enabled = false;
            GameObject.FindGameObjectWithTag("GameManage").GetComponent<CreateGame>().TrueAnswer();
        }
        else
        {
            GameObject.FindGameObjectWithTag("GameManage").GetComponent<CreateGame>().FalseAnswer();
        }
        
    }
   
}