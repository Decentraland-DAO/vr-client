using Cysharp.Threading.Tasks;
using DCL;
using DCL.LoadingScreen;
using DCL.Providers;
using MainScripts.DCL.Controllers.ShaderPrewarm;
using System.Threading;
using UnityEngine;

namespace DCLPlugins.LoadingScreenPlugin
{
    /// <summary>
    /// Plugin controller for the decoupled Loading Screen
    /// </summary>
    public class LoadingScreenPlugin : IPlugin
    {
        #if DCL_VR
        private const string LOADING_SCREEN_ASSET = "_LoadingScreenVR";
        #else
        private const string LOADING_SCREEN_ASSET = "_LoadingScreen";
        #endif

        private readonly DataStoreRef<DataStore_LoadingScreen> dataStoreLoadingScreen;
        private readonly CancellationTokenSource cancellationTokenSource;

        private LoadingScreenController loadingScreenController;

        public LoadingScreenPlugin()
        {
            dataStoreLoadingScreen.Ref.decoupledLoadingHUD.visible.Set(true);

            cancellationTokenSource = new CancellationTokenSource();
            CreateLoadingScreen(cancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid CreateLoadingScreen(CancellationToken cancellationToken = default)
        {
            loadingScreenController = new LoadingScreenController(
                CreateLoadingScreenView(),
                Environment.i.world.sceneController, Environment.i.world.state, NotificationsController.i,
                DataStore.i.player, DataStore.i.common, dataStoreLoadingScreen.Ref, DataStore.i.realm, new ShaderPrewarm(Environment.i.serviceLocator.Get<IAddressableResourceProvider>()));
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();

            loadingScreenController.Dispose();
        }

        public static LoadingScreenView CreateLoadingScreenView() =>
            GameObject.Instantiate(Resources.Load<GameObject>(LOADING_SCREEN_ASSET)).GetComponent<LoadingScreenView>();
    }
}
