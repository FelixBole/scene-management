using UnityEngine.Events;
using UnityEngine;
using Slax.EventSystem;

namespace Slax.SceneManagement
{
    public class GameExitListener : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO _channel = default;

        public UnityEvent OnEventRaised;

        private void OnEnable()
        {
            if (_channel != null)
                _channel.OnEventRaised += Respond;
        }

        private void OnDisable()
        {
            if (_channel != null)
                _channel.OnEventRaised -= Respond;
        }

        private void Respond()
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke();
        }
    }
}
