namespace Examples.DiPattern
{
    public class EntryPoint
    {
        public void Main()
        {
            var audioManager = new AudioManager();
            var gamePlay = new GamePlay(audioManager);
            gamePlay.Execute();
        }
    }
}