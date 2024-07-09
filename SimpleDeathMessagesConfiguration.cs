using Rocket.API;
using SDG.Unturned;
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
        public List<EDeathCause> Causes;

        public void LoadDefaults()
        {
            ShowSuicideMSG = true;
            DeathMessagesColor = UnityEngine.Color.red;
            Causes = new List<EDeathCause> { EDeathCause.SHRED, EDeathCause.ZOMBIE, EDeathCause.ANIMAL, EDeathCause.SPARK, EDeathCause.VEHICLE, EDeathCause.FOOD, EDeathCause.WATER, EDeathCause.INFECTION, EDeathCause.BLEEDING, EDeathCause.LANDMINE, EDeathCause.BREATH, EDeathCause.KILL, EDeathCause.FREEZING, EDeathCause.SENTRY, EDeathCause.CHARGE, EDeathCause.MISSILE, EDeathCause.BONES, EDeathCause.SPLASH, EDeathCause.ACID, EDeathCause.SPIT, EDeathCause.BURNING, EDeathCause.BURNER, EDeathCause.BOULDER, EDeathCause.ARENA, EDeathCause.GRENADE, EDeathCause.ROADKILL, EDeathCause.MELEE, EDeathCause.GUN, EDeathCause.PUNCH };
        }
    }
}

