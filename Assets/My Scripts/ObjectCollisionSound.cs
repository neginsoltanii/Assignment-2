using UnityEngine;

public class ObjectCollisionSound : MonoBehaviour
{
    public AudioClip collisionClip; // Assign different clips for Safe and Unsafe objects in the inspector
    private AudioSource audioSource;
    private bool preventSound = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if collided with the table, if so, do not play the sound
        if (collision.gameObject.tag == "Table")
        {
            return; // Exit the method without playing the sound
        }

        // Check if we should prevent sound (e.g., collided with the trash bin)
        if (!preventSound)
        {
            audioSource.PlayOneShot(collisionClip);
        }
        // Reset the flag in case it was set for a trash collision
        preventSound = false;
    }


    public void PreventSoundNextCollision()
    {
        preventSound = true;
    }
}
