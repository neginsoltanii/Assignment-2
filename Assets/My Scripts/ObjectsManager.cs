using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{

    public AudioClip safeObjectClip;
    public AudioClip unsafeObjectClip;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {


    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Safe"))
        {
            audioSource.PlayOneShot(safeObjectClip);
        }
        else if (other.CompareTag("Unsafe"))
        {
            audioSource.PlayOneShot(unsafeObjectClip);
        }
    }

}
