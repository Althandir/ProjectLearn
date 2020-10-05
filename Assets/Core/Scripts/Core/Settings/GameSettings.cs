using System;
using System.IO;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Settings", menuName = "GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        static GameSettings _instance;

        [SerializeField] bool Debug_loadNow;
        [SerializeField] bool Debug_clearSettingsFile;
        [SerializeField] bool Debug_openDirectory;

        [SerializeField] GameSettingsContainer _gameSettings = new GameSettingsContainer();

        [SerializeField] string _fileName = "settings.ini";
        string _pathToSettings;
        public static GameSettings Instance { get => _instance; }

        public float MasterVolume { get => _gameSettings.masterVolume; }
        public float EffectsVolume { get => _gameSettings.effectsVolume; }
        public float MusicVolume { get => _gameSettings.musicVolume; }


        #region Unity Messages
        private void OnEnable()
        {
            CreateSingleton();
            CreatePathToSettingsIni();

            LoadSettingsFromDisk();
        }

        /// <summary>
        /// Debug Methods to be used inside the Unity Inspector.
        /// </summary>
        private void OnValidate()
        {
            if (Debug_loadNow)
            {
                Debug_loadNow = false;

                LoadSettingsFromDisk();
            }
            if (Debug_clearSettingsFile)
            {
                Debug_clearSettingsFile = false;

                if (File.Exists(_pathToSettings))
                {
                    File.Delete(_pathToSettings);
                    Debug.Log("settings.ini deleted.");
                }
                else
                {
                    Debug.Log("No settings.ini found.");
                }
            }
            if (Debug_openDirectory)
            {
                Debug_openDirectory = false;

                Application.OpenURL(Application.persistentDataPath);
            }

        }
        #endregion

        void CreatePathToSettingsIni()
        {
            _pathToSettings = Path.Combine(Application.persistentDataPath, _fileName);
        }

        #region Singleton
        void CreateSingleton()
        {
            if (!_instance)
            {
                _instance = this;
                Debug.Log("Settings initialized.");
            }
            else
            {
                Debug.LogError("Duplicated Settings found!");
            }
        }
        #endregion
        
        void LoadSettingsFromDisk()
        {
            if (File.Exists(_pathToSettings))
            {
                Debug.Log("Settings found.");
                try
                {
                    JsonUtility.FromJsonOverwrite(File.ReadAllText(_pathToSettings), _gameSettings);
                }
                catch (Exception)
                {
                    Debug.LogError("Corrupted settingsfile detected! Recreating.");
                    CreateNewSettingsFile();
                    throw;
                }
            }
            else
            {
                Debug.LogWarning("Settingsfile not found. Creating new File.");
                CreateNewSettingsFile();
            }
        }

        /// <summary>
        /// Creates a new Settingsfile with a default GameSettingsContainer.
        /// </summary>
        void CreateNewSettingsFile()
        {
            try
            {
                SaveSettingsToDisk();
            }
            catch (Exception)
            {
                Debug.LogError("Could not create new Settingsfile!");
                throw;
            }
        }

        /// <summary>
        /// Writes the values in gameSettingsContainer into the settings.ini
        /// </summary>
        public void SaveSettingsToDisk()
        {
            File.WriteAllText(_pathToSettings, JsonUtility.ToJson(_gameSettings, true));
            Debug.Log("Settings saved!");
        }

        #region Setter for GameSettingsContainer Values
        public void SetMasterVolume(Single value)
        {
            _gameSettings.masterVolume = (float)Math.Round(value,2);
        }

        public void SetMusicVolume(Single value)
        {
            _gameSettings.musicVolume = (float)Math.Round(value, 2);
        }

        public void SetEffectsVolume(Single value)
        {
            _gameSettings.effectsVolume = (float)Math.Round(value, 2);
        }
        #endregion

    }



    [Serializable]
    public class GameSettingsContainer
    {
        public float masterVolume;
        public float musicVolume;
        public float effectsVolume;

        /// <summary>
        /// Creates new Settings with each value set to 0.5f
        /// </summary>
        public GameSettingsContainer()
        {
            masterVolume = 0.5f;
            musicVolume = 0.5f;
            effectsVolume = 0.5f;
        }

        /// <summary>
        /// Creates new Settings with the given parameters
        /// </summary>
        /// <param name="masterVolume"></param>
        /// <param name="musicVolume"></param>
        /// <param name="effectsVolume"></param>
        public GameSettingsContainer(float masterVolume, float musicVolume, float effectsVolume)
        {
            this.masterVolume = masterVolume;
            this.musicVolume = musicVolume;
            this.effectsVolume = effectsVolume;
        }
    }
}