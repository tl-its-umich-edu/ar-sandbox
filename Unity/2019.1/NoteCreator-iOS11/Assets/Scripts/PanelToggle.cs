using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelToggle : MonoBehaviour
{
    public Button toggleButton;
    public GameObject mainPanel;
    public GameObject dockPanel;

    private float transitionSpeed = .2f;

    private bool hidePanel = false;

    private RectTransform mainPanelRT;
    private Vector2 mainPanelOrigin;

    private RectTransform dockPanelRT;
    private Vector2 dockPanelOrigin;

    // Start is called before the first frame update
    void Start()
    {
        toggleButton.onClick.AddListener(ToggleMainPanel);
        mainPanelRT = mainPanel.GetComponent<RectTransform>();
        dockPanelRT = dockPanel.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hidePanel)
        {
            // move side panel

            Vector2 newPanelPos;

            newPanelPos.x = Mathf.Lerp(mainPanelRT.position.x, Screen.width + mainPanelRT.rect.width / 2, transitionSpeed);
            newPanelPos.y = Screen.height / 2;

            mainPanelRT.position = newPanelPos;

            // move dock panel

            Vector2 newDockPos;

            newDockPos.x = Screen.width / 2;
            newDockPos.y = Mathf.Lerp(dockPanelRT.position.y, dockPanelRT.rect.height / 2, transitionSpeed);

            dockPanelRT.position = newDockPos;

            // change button text
            toggleButton.GetComponentInChildren<TMP_Text>().text = "Show";
        }
        else
        {
            // move side panel

            Vector2 newPanelPos;

            newPanelPos.x = Mathf.Lerp(mainPanelRT.position.x, Screen.width - mainPanelRT.rect.width / 2, transitionSpeed);
            newPanelPos.y = Screen.height / 2;

            mainPanelRT.position = newPanelPos;

            // move dock panel

            Vector2 newDockPos;

            newDockPos.x = Screen.width / 2;
            newDockPos.y = Mathf.Lerp(dockPanelRT.position.y, -dockPanelRT.rect.height / 2, transitionSpeed);

            dockPanelRT.position = newDockPos;

            // change button text
            toggleButton.GetComponentInChildren<TMP_Text>().text = "Hide";
        }
    }

    private void ToggleMainPanel()
    {
        hidePanel = !hidePanel;
    }
}
