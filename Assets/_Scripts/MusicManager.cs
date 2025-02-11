using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject rhythmsound;      // Assign the rhythmsound GameObject in Inspector
    public GameObject successCanvas;    // Assign the Success Canvas
    public AudioSource gameMusic;       // Assign the AudioSource that plays the music

    [Header("Player Components")]
    public HatController hatController; // Assign the HatController directly

    private bool isSuccessTriggered = false;

    void Start()
    {
        // Hide Success Canvas at start
        if (successCanvas != null)
            successCanvas.SetActive(false);

        // **Check if the Audio Source is assigned**
        if (gameMusic != null)
        {
            if (!gameMusic.gameObject.activeSelf)  
            {
                gameMusic.gameObject.SetActive(true);  // Enable the Audio Source GameObject
            }

            gameMusic.Play();
            if (gameMusic.clip != null)  // **Ensure there's a valid audio clip**
            {
                Invoke("CheckForMusicEnd", gameMusic.clip.length); // Schedule success trigger
            }
            else
            {
                Debug.LogWarning("⚠ gameMusic has no audio clip assigned!");
            }
        }
        else
        {
            Debug.LogWarning("⚠ No AudioSource assigned for game music!");
        }
    }

    void CheckForMusicEnd()
    {
        if (!isSuccessTriggered)
        {
            TriggerSuccess();
        }
    }

    public void TriggerSuccess()
    {
        if (isSuccessTriggered) return; // Prevent multiple triggers
        isSuccessTriggered = true;

        Debug.Log("🎉 Music Ended! Triggering Success...");

        // **Disable Player Movement**
        if (hatController != null)
        {
            hatController.enabled = false; // Disable movement
            Debug.Log("🚫 HatController disabled. Player movement stopped.");
        }
        else
        {
            Debug.LogWarning("⚠ No HatController script assigned!");
        }

        // **Show Success UI**
        if (successCanvas != null)
        {
            successCanvas.SetActive(true);
            Debug.Log("✅ Success Canvas shown.");
        }
        else
        {
            Debug.LogError("⚠ Success Canvas is NOT assigned in the Inspector!");
        }
    }
}
