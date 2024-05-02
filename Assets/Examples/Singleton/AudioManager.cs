namespace Examples.Singleton
{
public class AudioManager
{
    private static AudioManager _instance;

    private AudioManager() {}

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new AudioManager();
            
            return _instance;
        }
    }

    public void PlayMusic(string musicName)
    {
        // Проигрывает музыку под названием musicName
    }
}
}