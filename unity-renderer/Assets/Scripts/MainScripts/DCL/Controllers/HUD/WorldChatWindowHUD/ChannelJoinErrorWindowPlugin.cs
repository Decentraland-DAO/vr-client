using DCL.Social.Chat;
using DCL.Providers;
using System.Threading;

namespace DCL.Social.Chat
{
    public class ChannelJoinErrorWindowPlugin : IPlugin
    {
        private readonly CancellationTokenSource cts = new ();

        private ChannelJoinErrorWindowController channelLimitReachedWindow;

        public ChannelJoinErrorWindowPlugin()
        {
            Initialize(cts.Token);
        }

        private async void Initialize(CancellationToken ct)
        {
            #if DCL_VR
            var view = await Environment.i.serviceLocator.Get<IAddressableResourceProvider>()
                                        .Instantiate<ChannelJoinErrorWindowComponentView>("ChannelJoinErrorModalVR", cancellationToken: ct);
            #else
            var view = await Environment.i.serviceLocator.Get<IAddressableResourceProvider>()
                                        .Instantiate<ChannelJoinErrorWindowComponentView>("ChannelJoinErrorModal", cancellationToken: ct);
            #endif
            channelLimitReachedWindow = new ChannelJoinErrorWindowController(view, Environment.i.serviceLocator.Get<IChatController>(), DataStore.i);
        }

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
            channelLimitReachedWindow.Dispose();
        }
    }
}
