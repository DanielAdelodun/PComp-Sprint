using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerManager : MonoBehaviour
{
    private PlayerMovement movement;
    [Header("Playmode options")]
    public bool thirdPerson;
    private bool launcherEnabled;

    private InputManager input;
    private Launcher launcher;
    void Awake()
    {
        UpdateCameraPosition();
        movement = GetComponent<PlayerMovement>();
        launcher = GetComponent<Launcher>();
        launcherEnabled = launcher ? true : false;
        input = GetComponent<InputManager>();
    }

    void Update()
    {
        movement.HandleAllMovement();
    }

    public void OnLaunch()
    {
        if (launcherEnabled) {
            launcher.Fire(input.mousePosition);
        }
    }

    private void UpdateCameraPosition()
    {
        if (thirdPerson) {
            Camera.main.transform.localPosition = new Vector3(0, 2.25f, -7.0f);
        } else {
            Camera.main.transform.localPosition = new Vector3(0, 0.71f, 0.179f);
        }
    }
}
