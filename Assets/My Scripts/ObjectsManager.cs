using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public AudioClip safeObjectClip;
    public AudioClip unsafeObjectClip;
    public GameObject resetButton; // Assign this in the Inspector

    private AudioSource audioSource;
    private Dictionary<GameObject, Vector3> safeObjectsInitialPositions = new Dictionary<GameObject, Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        resetButton.SetActive(false); // Hide button initially

        // Assume the safe objects are all active at the start
        GameObject[] safeObjects = GameObject.FindGameObjectsWithTag("Safe");
        foreach (GameObject obj in safeObjects)
        {
            // Record the initial position of each safe object
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

    // Call this from event trigger on the button
    public void ResetSafeObjects()
    {
        foreach (KeyValuePair<GameObject, Vector3> entry in safeObjectsInitialPositions)
        {
            // Move safe objects back to their initial positions
            entry.Key.transform.position = entry.Value;

            // Consider also resetting physics if needed
            Rigidbody rb = entry.Key.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        // Turn off audio
        audioSource.Stop();

        // Hide the button again
        resetButton.SetActive(false);
    }
}

