using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject rhythmsound;      // Assign the rhythmsound GameObject in Inspector
    public GameObject successCanvas;    // Assign the Success Canvas
    public AudioSource gameMusic;       // Assign the AudioSource that plays the music
    public UIManager uiManager;         // Reference to UIManager

    [Header("Player Components")]
    public HatController hatController; // Assign the HatController directly

    private bool isSuccessTriggered = false;

    void Start()
    {
        // Hide Success Canvas at start
        if (successCanvas != null) successCanvas.SetActive(false);
    }

    public void StartMusic()
    {
        // Enable Audio Source and Play Music
        if (gameMusic != null)
        {
            gameMusic.Play();

            if (gameMusic.clip != null)
            {
                Invoke(nameof(CheckForMusicEnd), gameMusic.clip.length); // Call when music ends
            }
            else
            {
                Debug.LogWarning("⚠ gameMusic has no audio clip assigned!");
            }
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
        if (isSuccessTriggered) return;
        isSuccessTriggered = true;

        Debug.Log("🎉 Music Ended! Showing Success UI...");

        // Disable Player Movement
        if (hatController != null)
        {
            hatController.enabled = false;
            Debug.Log("🚫 HatController disabled. Player movement stopped.");
        }
        else
        {
            Debug.LogWarning("⚠ No HatController script assigned!");
        }

        // Show Success UI
        if (successCanvas != null)
        {
            successCanvas.SetActive(true);
            Debug.Log("✅ Success Canvas shown.");
        }
        else
        {
            Debug.LogError("⚠ Success Canvas is NOT assigned in the Inspector!");
        }

        // **Trigger GameOver in UIManager**
        if (uiManager != null)
        {
            uiManager.GameOver();
            Debug.Log("🕒 UIManager triggered GameOver Timer.");
        }
        else
        {
            Debug.LogWarning("⚠ UIManager is missing! GameOver UI might not work.");
        }
    }
}
