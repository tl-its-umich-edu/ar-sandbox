using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawnBehavior : MonoBehaviour
{
    public float pauseBeforeWobble = 0;

    private Vector3 initScale;

    // Start is called before the first frame update
    void Start()
    {
        initScale = transform.localScale;
        Debug.Log(initScale);

        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // wobble notes on spawn

        if (pauseBeforeWobble <= 0)
        {
            float wobble = Mathf.Pow(1.1f, -40f * Mathf.Abs(pauseBeforeWobble)) * -Mathf.Cos(Mathf.Abs(pauseBeforeWobble * 10)) + 1;
            transform.localScale = initScale * wobble;

            if (pauseBeforeWobble <= -1)
            {
                transform.localScale = initScale;
                enabled = false;
            }
        }

        pauseBeforeWobble -= Time.deltaTime;
    }
}
