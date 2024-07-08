using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace coolpuppy24.simpledeathmessages
{
    public class SimpleDeathMessagesConfiguration : IRocketPluginConfiguration
    {
        public bool ShowSuicideMSG;
        public UnityEngine.Color DeathMessagesColor;
        public List<string> Causes;

        public void LoadDefaults()
        {
            ShowSuicideMSG = true;
            DeathMessagesColor = UnityEngine.Color.red;
            Causes = new List<string> { "SHRED", "ZOMBIE", "ANIMAL", "SPARK", "VEHICLE", "FOOD", "WATER", "INFECTION", "BLEEDING", "LANDMINE", "BREATH", "KILL", "FREEZING", "SENTRY", "CHARGE", "MISSILE", "BONES", "SPLASH", "ACID", "SPIT", "BURNING", "BURNER", "BOULDER", "ARENA", "GRENADE", "ROADKILL", "MELEE", "GUN", "PUNCH" };
        }
    }
}

