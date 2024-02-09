using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public AudioClip safeObjectClip;
    public AudioClip unsafeObjectClip;
    public GameObject resetButton;

    private AudioSource audioSource;
    private Dictionary<GameObject, Vector3> safeObjectsInitialPositions = new Dictionary<GameObject, Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        resetButton.SetActive(false); // Hide button initially

        GameObject[] safeObjects = GameObject.FindGameObjectsWithTag("Safe");
        foreach (GameObject obj in safeObjects)
        {
            safeObjectsInitialPositions[obj] = obj.transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Safe"))
        {
            audioSource.PlayOneShot(safeObjectClip);
            resetButton.SetActive(true); // Show the button
        }
        else if (other.CompareTag("Unsafe"))
        {
            audioSource.PlayOneShot(unsafeObjectClip);
        }
    }

    public void ResetSafeObjects()
    {
        StartCoroutine(ResetSafeObjectsCoroutine());
    }

    IEnumerator ResetSafeObjectsCoroutine()
    {
        foreach (KeyValuePair<GameObject, Vector3> entry in safeObjectsInitialPositions)
        {
            entry.Key.transform.position = entry.Value;

            Rigidbody rb = entry.Key.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        audioSource.Stop();

        // Wait for a second or so to let the glow and sound effect play out
        yield return new WaitForSeconds(0.5f); 

        // Hide the button after waiting
        resetButton.SetActive(false);
    }
}
