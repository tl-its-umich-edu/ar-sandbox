using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelToggle : MonoBehaviour
{
    public Button toggleButton;
    public GameObject mainPanel;

    private float transitionSpeed = .2f;

    private bool hidePanel = false;
    private RectTransform mainPanelRT;
    private Vector2 mainPanelOrigin;

    // Start is called before the first frame update
    void Start()
    {
        toggleButton.onClick.AddListener(ToggleMainPanel);
        mainPanelRT = mainPanel.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hidePanel)
        {
            Vector2 newPanelPos;

            newPanelPos.x = Mathf.Lerp(mainPanelRT.position.x, Screen.width + mainPanelRT.rect.width / 2, transitionSpeed);
            newPanelPos.y = Screen.height / 2;

            mainPanelRT.position = newPanelPos;

            toggleButton.GetComponentInChildren<TMP_Text>().text = "Show";
        }
        else
        {
            Vector2 newPanelPos;

            newPanelPos.x = Mathf.Lerp(mainPanelRT.position.x, Screen.width - mainPanelRT.rect.width / 2, transitionSpeed);
            newPanelPos.y = Screen.height / 2;

            mainPanelRT.position = newPanelPos;

            toggleButton.GetComponentInChildren<TMP_Text>().text = "Hide";
        }
    }

    private void ToggleMainPanel()
    {
        hidePanel = !hidePanel;
    }
}
