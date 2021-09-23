using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    public void TogglePause(bool togle)
    {
        PauseMenu.SetActive(togle);
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(!PauseMenu.active);
        }
    }
}
