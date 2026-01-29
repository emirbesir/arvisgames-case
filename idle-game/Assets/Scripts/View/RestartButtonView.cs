using UnityEngine;
using UnityEngine.UI;
using Game.Controllers;
using Zenject;

namespace Game.Views
{
    [RequireComponent(typeof(Button))]
    public class RestartButtonView : MonoBehaviour
    {
        private GameController _gameController;

        [Inject]
        public void Construct(GameController gameController)
        {
            _gameController = gameController;
        }

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => 
            {
                _gameController.RestartGame();
            });
        }
    }
}