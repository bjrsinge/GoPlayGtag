using System;
using BepInEx;
using UnityEngine;
using UnityEngine.UI;
using Utilla;
using Bepinject;

namespace GoPlayGtag
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin("com.bjrsinge.gorillatag.GoPlayGtag", "GoPlayGtag", "1.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        public GameObject forestSign;
        public Text signText;
        public bool init;

        void Awake()
        {
            // will totally not add ci support here :p
        }

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            init = true;
            forestSign = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/Tree Room Texts/WallScreenForest");
        }

        void OnEnable()
        {
            if (init)
            {
                if (forestSign != null)
                {
                    signText = forestSign.GetComponent<Text>();
                    signText.text = "GO PLAY GORILLA TAG";
                }
                else
                {
                    Debug.Log("forestSign doesn't exist in OnEnable");
                }
            }
        }

        void OnDisable()
        {
            if (init)
            {
                if (forestSign != null)
                {
                    signText = forestSign.GetComponent<Text>();
                    signText.text = "WELCOME TO GORILLA TAG!\r\n\r\nHEAD OUTSIDE TO AUTOMATICALLY JOIN A PUBLIC GAME, OR USE THE TERMINAL TO JOIN A SPECIFIC ROOM OR ADJUST YOUR SETTINGS.";
                }
                else
                {
                    Debug.Log("forestSign doesn't exist in OnDisable");
                }
            }
        }
        void Update()
        {
            if (init)
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