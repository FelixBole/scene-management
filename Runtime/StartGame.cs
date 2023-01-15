using UnityEngine;
using Slax.EventSystem;

namespace Slax.SceneManagement
{
    public class StartGame : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private GameSceneSO _locationToLoad;
        [SerializeField] private bool _showLoadScreen;

        [Header("Broadcasting on")]
        [SerializeField] private LoadEventChannelSO _startGameEvent;
        [Tooltip("This should be listened to by systems that want to be notified of a new game. Like the save system to create new game data.")]
        [SerializeField] private VoidEventChannelSO _onNewGameEvent;

        [Header("Listening to")]
        [SerializeField] private VoidEventChannelSO _startNewGameEvent;
        [SerializeField] private VoidEventChannelSO _continueGameEvent;

        private void Start()
        {
            _startNewGameEvent.OnEventRaised += StartNewGame;
        }

        private void OnDestroy()
        {
            _startNewGameEvent.OnEventRaised -= StartNewGame;
        }

        private void StartNewGame()
        {
            if (_onNewGameEvent != null)
                _onNewGameEvent.RaiseEvent();

            _startGameEvent.RaiseEvent(_locationToLoad, _showLoadScreen);
        }
    }
}
