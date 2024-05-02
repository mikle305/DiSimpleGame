using System;
using Cysharp.Threading.Tasks;
using SimpleGame.Additional;
using SimpleGame.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleGame.Services
{
    public class GameService
    {
        private readonly ProgressService _progressService;
        private readonly ConfigsProvider _configsProvider;
        private int _expectedAnswer;

        public event Action<float> GameStarted;
        public event Action<int, int> NumbersGenerated;
        public event Action<bool> AnswerChecked;


        public GameService(ProgressService progressService, ConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
            _progressService = progressService;
        }
        
        public async UniTask StartGame()
        {
            GameStarted?.Invoke(Constants.GameCountdown);
            await UniTask.WaitForSeconds(Constants.GameCountdown);
            GenerateNumbers();
        }

        private void GenerateNumbers()
        {
            var gameConfig = _configsProvider.GetConfig<GameConfig>();
            var currentLevel = _progressService.Progress.CurrentLevel;
            
            var minNumber = Mathf.RoundToInt(gameConfig.MinNumberByLevelCurve.Evaluate(currentLevel));
            var maxNumber = Mathf.RoundToInt(gameConfig.MaxNumberByLevelCurve.Evaluate(currentLevel));

            var firstNumber = Random.Range(minNumber, maxNumber + 1);
            var secondNumber = Random.Range(minNumber, maxNumber + 1);
            
            _expectedAnswer = firstNumber * secondNumber;
            NumbersGenerated?.Invoke(firstNumber, secondNumber);
        }

        public void SubmitAnswer(int answer)
        {
            var isAnswerCorrect = answer == _expectedAnswer;
            if (isAnswerCorrect)
            {
                _progressService.Progress.CurrentLevel++;
                _progressService.Save();
            }

            AnswerChecked?.Invoke(isAnswerCorrect);
        }
    }
}