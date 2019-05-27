using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text textComponent;
    private float time = 300;

    private void Start()
    {
        textComponent = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        time -= Time.fixedDeltaTime;

        if (time < 0)
        {
            time = 0;
            FindObjectOfType<GameOver>().GameEnd(0);
        }

        textComponent.text = Mathf.RoundToInt(time).ToString();
    }
}
