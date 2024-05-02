using Cysharp.Threading.Tasks;
using SimpleGame.Additional;
using SimpleGame.Data;
using SimpleGame.Services;

namespace SimpleGame.GameFlow
{
    public class EntryPoint
    {
        private readonly ConfigsProvider _configsProvider;
        private readonly ProgressService _progressService;
        private readonly LoadingCurtain _loadingCurtain;


        public EntryPoint(
            ConfigsProvider configsProvider, 
            ProgressService progressService,
            LoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
            _progressService = progressService;
            _configsProvider = configsProvider;
        }

        public async UniTask Execute()
        {
            _loadingCurtain.SetProgress(0);
            _configsProvider.LoadConfig<GameConfig>();
            _progressService.Load();
            _loadingCurtain.SetProgress(1);
            await UniTask.WaitForSeconds(Constants.CurtainHideDelay);
            await _loadingCurtain.HideCurtain();
        }
    }
}