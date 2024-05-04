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
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _firstNumberText;
        [SerializeField] private TextMeshProUGUI _secondNumberText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TMP_InputField _answerInputField;
        [SerializeField] private Button _submitButton;
        [SerializeField] private CanvasGroup _mainGroup;

        private GameService _gameService;
        private bool _canSubmit = true;


        [Inject]
        public void Construct(GameService gameService)
        {
            _gameService = gameService;
            _gameService.NumbersGenerated += OnNumbersGenerated;
            _gameService.GameStarted += OnGameStarted;
            _submitButton.onClick.AddListener(OnSubmitClicked);
        }

        private void OnDestroy()
        {
            _gameService.NumbersGenerated -= OnNumbersGenerated;
            _gameService.GameStarted -= OnGameStarted;
            _submitButton.onClick.RemoveListener(OnSubmitClicked);
        }

        private void OnGameStarted(float countdown)
        {
            SetMainGroupVisibility(false);
            DOTween
                .To(() => countdown, (v) => countdown = v, 0, countdown)
                .SetEase(Ease.Linear)
                .OnUpdate(() => _descriptionText.text = countdown.ToString("F1"))
                .OnComplete(() => _descriptionText.text = "Введите ответ");
        }

        private void OnNumbersGenerated(int firstNumber, int secondNumber)
        {
            _firstNumberText.text = firstNumber.ToString();
            _secondNumberText.text = secondNumber.ToString();
            _canSubmit = true;
            SetMainGroupVisibility(true);
        }

        private void OnSubmitClicked()
        {
            if (!_canSubmit || string.IsNullOrWhiteSpace(_answerInputField.text))
                return;
                
            _canSubmit = false;
            _gameService.SubmitAnswer(int.Parse(_answerInputField.text));
            CleanUpView().Forget();


            async UniTask CleanUpView()
            {
                await UniTask.WaitForSeconds(Constants.MenuFadeDuration);
                _firstNumberText.text = string.Empty;
                _secondNumberText.text = string.Empty;
                _answerInputField.text = string.Empty;
            }
        }

        private void SetMainGroupVisibility(bool isVisible)
        {
            _mainGroup.interactable = isVisible;
            _mainGroup.blocksRaycasts = isVisible;
            _mainGroup.alpha = isVisible ? 1 : 0;
        }
    }
}