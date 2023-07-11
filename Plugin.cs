using System;
using BepInEx;
using UnityEngine;
using UnityEngine.UI;
using Utilla;

namespace GorillaTagModTemplateProject
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        // public bool antiLoop, enabled, loaded;
        public GameObject forestSign;
        public Text signText;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            forestSign = GameObject.Find("Level/lower level/UI/Tree Room Texts/WallScreenForest");
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }
        void Update()
        {
            if (forestSign.GetComponent<Text>().text != "GO PLAY GORILLA TAG")
            { 
                if (forestSign != null)
                {
                    signText = forestSign.GetComponent<Text>();
                    signText.text = "GO PLAY GORILLA TAG";
                }
                else
                {
                    Debug.Log("forestSign doesn't exist in OnGameInitialized");
                }
            }
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
        }
    }
}