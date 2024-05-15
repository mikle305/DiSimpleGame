namespace Examples.DiMethods
{
    public class GamePlay
    {
        private AudioManager _audioManager;
        
        // Property Injection
        public AudioManager AudioManager
        {
            get => _audioManager;
            set => _audioManager = value;
        }
        
        // Method Injection
        public void Construct(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }
        
        // Constructor Injection
        public GamePlay(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }
    }
}