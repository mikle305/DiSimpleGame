using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SimpleGame.Additional;
using TMPro;
using UnityEngine;

namespace SimpleGame.Services
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private CancellationTokenSource _fadeTokenSource;
        
        
        public void SetProgress(float progressCoefficient) 
            => _progressText.text = $"{Mathf.FloorToInt(progressCoefficient * 100).ToString()}%";

        public async UniTask ShowCurtain()
        {
            _fadeTokenSource?.Cancel();
            _fadeTokenSource = new CancellationTokenSource();
            _canvasGroup.blocksRaycasts = true;
            
            await _canvasGroup
                .DOFade(1, CalcShowDuration())
                .ToUniTask(cancellationToken: _fadeTokenSource.Token);
        }

        public async UniTask HideCurtain()
        {
            _fadeTokenSource?.Cancel();
            _fadeTokenSource = new CancellationTokenSource();
            
            await _canvasGroup
                .DOFade(0, CalcHideDuration())
                .ToUniTask(cancellationToken: _fadeTokenSource.Token);

            _canvasGroup.blocksRaycasts = false;
        }

        private float CalcShowDuration()
            => (1f - _canvasGroup.alpha) * Constants.CurtainFadeDuration;
        
        private float CalcHideDuration()
            => _canvasGroup.alpha * Constants.CurtainFadeDuration;
    }
}