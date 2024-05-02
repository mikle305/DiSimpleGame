using Cysharp.Threading.Tasks;
using DG.Tweening;
using SimpleGame.Additional;
using SimpleGame.Services;
using TMPro;
using UniDependencyInjection.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleGame.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _matchResultText;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private GameService _gameService;
        private ProgressService _progressService;


        [Inject]
        public void Construct(GameService gameService, ProgressService progressService)
        {
            _progressService = progressService;
            _gameService = gameService;
            _progressService.ProgressChanged += OnProgressChanged;
            _gameService.AnswerChecked += OnAnswerChecked;
            _startButton.onClick.AddListener(OnStartClicked);
        }

        private void OnDestroy()
        {
            _progressService.ProgressChanged -= OnProgressChanged;
            _gameService.AnswerChecked -= OnAnswerChecked;
            _startButton.onClick.RemoveListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            OnStartClickedAsync().Forget();
            return;

            async UniTask OnStartClickedAsync()
            {
                _canvasGroup.interactable = false;
                await _canvasGroup.DOFade(0, Constants.MenuFadeDuration);
                _canvasGroup.blocksRaycasts = false;
                await _gameService.StartGame();
            }
        }

        private void OnAnswerChecked(bool isAnswerCorrect)
        {
            OnAnswerCheckedAsync().Forget();
            return;
            
            async UniTask OnAnswerCheckedAsync()
            {
                SetResultText(isAnswerCorrect);
                _canvasGroup.blocksRaycasts = true;
                await _canvasGroup.DOFade(1, Constants.MenuFadeDuration);
                _canvasGroup.interactable = true;
            }
        }

        private void OnProgressChanged()
        {
            _levelText.text = "Уровень " + _progressService.Progress.CurrentLevel;
        }

        private void SetResultText(bool isAnswerCorrect)
        {
            if (isAnswerCorrect)
            {
                _matchResultText.text = "Вы ответили\nПравильно!";
                
                _matchResultText.color = Color.green;
            }
            else
            {
                _matchResultText.text = "Неверно,\nПопробуйте снова";
                
                _matchResultText.color = Color.red;
            }
        }
    }
}