using System;
using SimpleGame.Additional;
using SimpleGame.Data;
using UnityEngine;

namespace SimpleGame.Services
{
    public class ProgressService
    {
        public PlayerProgress Progress { get; private set; }

        public event Action ProgressChanged;
        
        
        public void Save()
        {
            var serializedData = Progress.ToJson();
            PlayerPrefs.SetString(Constants.ProgressKey, serializedData);
            PlayerPrefs.Save();
            ProgressChanged?.Invoke();
        }

        public void Load()
        {
            var serializedData = PlayerPrefs.GetString(Constants.ProgressKey);
            Progress = serializedData.FromJson<PlayerProgress>() ?? new PlayerProgress();
            ProgressChanged?.Invoke();
        }
    }
}