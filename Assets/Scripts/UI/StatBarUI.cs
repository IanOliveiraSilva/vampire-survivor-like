using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI
{
    public class StatBarUI : MonoBehaviour
    {
        [SerializeField]
        private Core.Events.FloatEventChannelSO channelToListen;
        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            if(channelToListen != null)
            {
                channelToListen.OnEventRaised += UpdateBar;
            }
        }

        private void OnDisable()
        {
            if (channelToListen != null)
            {
                channelToListen.OnEventRaised -= UpdateBar;
            }
        }

        private void UpdateBar(float normalizedValue)
        {
            slider.value = normalizedValue;
        }


    }
}

