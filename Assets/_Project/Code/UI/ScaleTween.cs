using DG.Tweening;
using UnityEngine;

namespace SimpleGame.UI
{
    public class ScaleTween : MonoBehaviour
    {
        [SerializeField] private float _from = 1;
        [SerializeField] private float _to = 1;
        [SerializeField] private float _interval = 1;


        private void Awake()
        {
            transform.localScale = new Vector3(_from, _from, 1);
            transform.DOScale(new Vector3(_to, _to, 1), _interval).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InCubic);
        }
    }
}