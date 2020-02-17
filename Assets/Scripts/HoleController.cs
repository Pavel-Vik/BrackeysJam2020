using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public AudioClip holeOut,holeIn;
    private AudioSource audioSource;
    public GameObject ship;
    public GameObject connectedHole;
    private bool lookAtShip =true;
    private bool notStable=false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void EnterHole()
    {
        StartCoroutine(HoleEnterWorker());
    }
    public void ExitHole()
    {
        StartCoroutine(HoleExitWorker());
    }
    IEnumerator HoleEnterWorker()
    {
        lookAtShip = false;
        GetComponent<Collider2D>().enabled = false;
        ship.GetComponent<MoveController>().EnterToHole();

        yield return new WaitForSeconds(1);

        connectedHole.GetComponent<HoleController>().ExitHole();
        yield return new WaitForSeconds(3);
        GetComponent<Collider2D>().enabled = true;
        lookAtShip = true;
    }
    IEnumerator HoleExitWorker()
    {
        GetComponent<Collider2D>().enabled = false;
       
        notStable = true;

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(holeOut);
        
        yield return new WaitForSeconds(1f);
        
        //transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        
        ship.GetComponent<ShipController>().WorkTubines(false);
        ship.transform.position = transform.position;
        ship.GetComponent<ShipController>().WorkTubines(true);

        ship.GetComponent<MoveController>().ExitOutHole();
        Camera.main.SendMessage("activateFollow", true);


        yield return new WaitForSeconds(0.2f);
        notStable = false;
        yield return new WaitForSeconds(1);
        
        GetComponent<Collider2D>().enabled = true;
        
    }
    void Update()
    {
        if (lookAtShip)
        {
            Vector3 diff = ship.transform.position - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        if (notStable)
        {
            transform.localScale=Vector3.Lerp(transform.localScale, Vector3.one * Random.Range(0.3f, 1.7f),7 * Time.deltaTime);
        }
    }
}
