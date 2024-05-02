namespace Examples.DiPattern
{
    public class GamePlay
    {
        private readonly AudioManager _audioManager;


        public GamePlay(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public void Execute()
        {
            _audioManager.PlayMusic("SomeMusic");
        }
    }
}