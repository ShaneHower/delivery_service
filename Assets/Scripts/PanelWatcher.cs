using UnityEngine;

public class PanelWatcher : MonoBehaviour
{
    // This class watches for input to turn panels on and off
    public Transform inventory_canvas;
    public Transform pauseCanvas;

    private void Start()
    {
        inventory_canvas = gameObject.transform.Find("inventory_canvas");
        pauseCanvas = gameObject.transform.Find("pause_canvas");
    }

    // Update is called once per frame
    void Update()
    {
        checkInventory();
        checkPause();
    }

    private void checkInventory()
    {
        // Turns inventory panel on and off using Tab or i as an input

        bool invInput = Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I);
        GameObject invObject = inventory_canvas.gameObject;
        bool isInv = invObject.activeSelf;

        switchActiveState(invInput, invObject, isInv);
    }

    private void checkPause()
    {
        // Turns inventory panel on and off using Tab or i as an input
        bool pauseInput = Input.GetKeyDown(KeyCode.Escape);
        GameObject pauseObject = pauseCanvas.gameObject;
        bool isPaused = pauseObject.activeSelf;

        switchActiveState(pauseInput, pauseObject, isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void switchActiveState(bool input, GameObject canvasObject, bool isActive)
    {
        if (input && !isActive)
        {
            canvasObject.SetActive(true);
        }
        else if (input && isActive)
        {
            canvasObject.SetActive(false);
        }
    }

    public void resumeButton()
    {
        GameObject pauseObject = pauseCanvas.gameObject;
        bool isPaused = pauseObject.activeSelf;

        switchActiveState(true, pauseObject, isPaused);
    }
}
