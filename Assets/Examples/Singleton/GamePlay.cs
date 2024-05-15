namespace Examples.Singleton
{
    public class GamePlay
    {
        private static GamePlay _instance;
        private AudioManager _audioManager;

        private GamePlay()
        {
            _audioManager = AudioManager.Instance;
        }

        public static GamePlay Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GamePlay();
                
                return _instance;
            }
        }

        public void Execute()
        {
            _audioManager.PlayMusic("SomeMusic");
        }
    }
}