using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Monry.AsyncDisposableSample
{
    public class Dialog : MonoBehaviour, IAsyncDisposable
    {
        private const double AnimationDurationOpen = 1.0;
        private const double AnimationDurationClose = 0.5;

        private static readonly int AnimatorTriggerOpen = Animator.StringToHash("Open");
        private static readonly int AnimatorTriggerClose = Animator.StringToHash("Close");

        [SerializeField] private Animator animator;
        [SerializeField] private Button buttonToClose;

        public async UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            animator.SetTrigger(AnimatorTriggerOpen);
            await UniTask.Delay(TimeSpan.FromSeconds(AnimationDurationOpen), cancellationToken: cancellationToken);
        }

        public async ValueTask DisposeAsync()
        {
            animator.SetTrigger(AnimatorTriggerClose);
            await UniTask.Delay(TimeSpan.FromSeconds(AnimationDurationClose));
            Destroy(gameObject);
        }

        public UniTask OnClickButtonToCloseAsync(CancellationToken cancellationToken = default) =>
            buttonToClose.OnClickAsync(cancellationToken);
    }
}
