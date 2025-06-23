using TMPro;
using UnityEngine;

namespace Survivor.UI
{
    public class TimerUI : MonoBehaviour
    {
        private TextMeshProUGUI timerText;
        private float elapsedTime;

        private void Awake()
        {
            timerText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;

            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}

