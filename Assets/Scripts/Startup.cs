using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Monry.AsyncDisposableSample
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private Dialog dialogPrefab;
        [SerializeField] private Transform parentTransform;

        // Button.onClick に設定する場合、戻り値は void でないとダメ
        public async void OpenDialog()
        {
            Debug.Log("Before OpenDialog");
            await using (var dialog = Instantiate(dialogPrefab, parentTransform))
            {
                var cancellationToken = this.GetCancellationTokenOnDestroy();
                Debug.Log("Initialize");
                await dialog.InitializeAsync(cancellationToken);
                Debug.Log("Wait for click close button in Dialog");
                await dialog.OnClickButtonToCloseAsync(cancellationToken);
                Debug.Log("Dispose");
            }
            Debug.Log("After OpenDialog");
        }
    }
}