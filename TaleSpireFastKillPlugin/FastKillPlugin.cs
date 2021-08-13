using System.Collections.Generic;
using System.Linq;

using BepInEx;
using BepInEx.Configuration;
using Bounce.Unmanaged;

using UnityEngine;

namespace LordAshes
{
    [BepInPlugin(Guid, Name, Version)]
    public class FastKillPlugin : BaseUnityPlugin
    {
        // Plugin info
        private const string Name = "Fast Kill Plug-In";
        private const string Guid = "org.lordashes.plugins.fastkill";
        private const string Version = "1.0.0.0";

        // Content directory
        private string dir = UnityEngine.Application.dataPath.Substring(0, UnityEngine.Application.dataPath.LastIndexOf("/")) + "/TaleSpire_CustomData/";

        // Triggering Key
        private ConfigEntry<KeyboardShortcut> trigger;

        /// <summary>
        /// Function for initializing plugin
        /// This function is called once by TaleSpire
        /// </summary>
        void Awake()
        {
            UnityEngine.Debug.Log("Lord Ashes Fast Kill Plugin Active.");

            trigger = Config.Bind("Hotkeys", "Delete Asset", new KeyboardShortcut(KeyCode.Delete, KeyCode.RightControl));
        }

        /// <summary>
        /// Function for determining if view mode has been toggled and, if so, activating or deactivating Character View mode.
        /// This function is called periodically by TaleSpire.
        /// </summary>
        void Update()
        {
            if(trigger.Value.IsUp())
            {
                if(LocalClient.SelectedCreatureId!=null)
                {
                    CreatureManager.DeleteCreature(LocalClient.SelectedCreatureId, UniqueCreatureGuid.Empty);
                }
            }
        }

        /// <summary>
        /// Method to properly evaluate shortcut keys. 
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        public bool StrictKeyCheck(KeyboardShortcut check)
        {
            if (!check.IsUp()) { return false; }
            foreach (KeyCode modifier in new KeyCode[] { KeyCode.LeftAlt, KeyCode.RightAlt, KeyCode.LeftControl, KeyCode.RightControl, KeyCode.RightControl, KeyCode.RightShift })
            {
                if (Input.GetKey(modifier) != check.Modifiers.Contains(modifier)) { return false; }
            }
            return true;
        }
    }
}
