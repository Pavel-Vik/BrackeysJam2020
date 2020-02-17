using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameObject turbines;
    public GameObject nitroTurbines;
    private AudioSource audioSource;
    public AudioClip nitro;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void WorkTubines(bool b)
    {
        turbines.SetActive(b);
    }
    public void WorkNitroTubines(bool b)
    {
       // nitroTurbines.SetActive(b);
        GetComponent<MoveController>().SetSpeed(b?3:1);
        ParticleSystem[] particleSystems = nitroTurbines.GetComponentsInChildren<ParticleSystem>();
        if(b)
        {
            audioSource.PlayOneShot(nitro);
                foreach(ParticleSystem particle in particleSystems)
            {
                particle.Play();
            }
        }
        else
        {
            foreach (ParticleSystem particle in particleSystems)
            {
                particle.Stop();
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            WorkNitroTubines(true);
        }
        if(Input.GetMouseButtonUp(0))
            WorkNitroTubines(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Camera.main.SendMessage("activateFollow", false);
        collision.SendMessage("EnterHole");
    }
}
