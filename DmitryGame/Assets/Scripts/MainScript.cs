using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    enum GameStatus { Plaing, Droped};
    GameStatus gameStatus;

    [SerializeField] GameObject nut;
    [SerializeField] GameObject cookie;
    GameObject obj;
    Rigidbody2D rb;
    [SerializeField] int xForce = 1;
    [SerializeField] GameObject Dmitry;
    [SerializeField] GameObject Squirrel;
    Animator animator;

    private Vector2 mousePos;
    private float maxPosX = 1.5f;
    private float progressSpeed = 8f;
    private bool mouseDrag = false;

    private void Start()
    {
        
        Spawn();
    }

    private void Update()
    {
        DeliteGameObject();
    }

    private void OnMouseDrag()
    {
        mouseDrag = true;
        if (gameStatus == GameStatus.Plaing)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.x = mousePos.x > maxPosX ? maxPosX : mousePos.x;
            mousePos.x = mousePos.x < -maxPosX ? -maxPosX : mousePos.x;
            mousePos.y = 0;

            obj.transform.position = Vector2.MoveTowards(obj.transform.position, mousePos, progressSpeed * Time.deltaTime);
        }
    }

    private void OnMouseUp()
    {
        if (mouseDrag)
        {
            gameStatus = GameStatus.Droped;

            if (obj.transform.position.x > 0)
            {
                rb.gravityScale = 1;
                rb.AddForce(new Vector2(xForce, 0), ForceMode2D.Impulse);
            }
            else if ((obj.transform.position.x < 0))
            {
                rb.gravityScale = 1;
                rb.AddForce(new Vector2(-xForce, 0), ForceMode2D.Impulse);
            }
            mouseDrag = false;
        }
    }

    void Spawn()
    {
        gameStatus = GameStatus.Plaing;
        RandomGameObj();
        rb = obj.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void DeliteGameObject()
    {
        if (obj.transform.position.y < -3.2f)
        {
            TargetsAnimation();
            Destroy(obj);
            Spawn();
        }
    }

    void RandomGameObj()
    {
        int i = Random.Range(0, 101);

        if (i <= 50 )
        {
            obj = Instantiate(nut, new Vector2(0, 0), Quaternion.identity);
        }
        else
        {
            obj = Instantiate(cookie, new Vector2(0, 0), Quaternion.identity);
        }
    }

    void TargetsAnimation()
    {
        if (obj.transform.position.x > 0)
        {
            animator = Dmitry.GetComponent<Animator>();
            animator.SetTrigger("DmitryAnim");
        }
        else if (obj.transform.position.x < 0)
        {
            animator = Squirrel.GetComponent<Animator>();
            animator.SetTrigger("SquirrelAnim");
        }
    }

}
