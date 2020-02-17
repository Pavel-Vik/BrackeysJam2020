using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public bool moveToCursor = true;
    public bool moveForward = false;
    private int speed = 1;

    //Тут нужно забрать у игрока управление и спрятать состав под маску.
    public void EnterToHole()
    {
       // moveToCursor = false;
       // moveForward = true;
    }
    //Тут вывести состав из дыры и отдать игроку управление.
    public void ExitOutHole()
    {
      //  moveForward = false;
       // moveToCursor = true;
        
    }

    public void SetSpeed(int s)
    {
        speed = s;
    }
    void Update()
    {
        if (moveToCursor) { 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        transform.position = Vector3.Lerp(transform.position, mousePos, speed * Time.deltaTime);

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    } 
        //if(moveForward)
          //  transform.position = Vector3.Lerp(transform.position, Vector3.forward, speed * Time.deltaTime);
    }
}
