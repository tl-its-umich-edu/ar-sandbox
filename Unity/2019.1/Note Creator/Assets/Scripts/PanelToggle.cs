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
        mainPanelOrigin = new Vector2(mainPanelRT.localPosition.x, mainPanelRT.localPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (hidePanel)
        {
            mainPanelRT.localPosition = new Vector3(Mathf.Lerp(mainPanelRT.localPosition.x, mainPanelOrigin.x + mainPanelRT.rect.width, transitionSpeed), 0, 0);
            toggleButton.GetComponentInChildren<TMP_Text>().text = "Show";
        }
        else
        {
            mainPanelRT.localPosition = new Vector3(Mathf.Lerp(mainPanelRT.localPosition.x, mainPanelOrigin.x, transitionSpeed), 0, 0);
            toggleButton.GetComponentInChildren<TMP_Text>().text = "Hide";
        }
    }

    private void ToggleMainPanel()
    {
        hidePanel = !hidePanel;
    }
}
