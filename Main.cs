using System.Collections.Generic;
using System.Drawing;
using Rocket.Core.Logging;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Rocket.Core.Plugins;
using UnityEngine;
using Rocket.API.Collections;
using System;
using Rocket.Unturned.Chat;

namespace coolpuppy24.simpledeathmessages
{
    public class SimpleDeathMessages : RocketPlugin<SimpleDeathMessagesConfiguration>
    {
        public static SimpleDeathMessages Instance { get; private set; }
        protected override void Load()
        {
            Rocket.Unturned.Events.UnturnedPlayerEvents.OnPlayerDeath += HandleEvent;
            Rocket.Core.Logging.Logger.Log("Successfully Loaded!");
        }

        protected override void Unload()
        {
            Rocket.Unturned.Events.UnturnedPlayerEvents.OnPlayerDeath -= HandleEvent;
            Rocket.Core.Logging.Logger.Log("Unloaded!");
        }

        public override TranslationList DefaultTranslations => new TranslationList
        {
            {"gun_headshot","{1} [GUN - {3}] {2} {0}"},
            {"gun","{1} [GUN - {2}] {0}"},
            {"food","[FOOD] {0}"},
            {"arena","[ARENA] {0}"},
            {"shred","[SHRED] {0}"},
            {"punch_headshot","{1} [PUNCH] {2} {0}"},
            {"punch","{0} [PUNCH] {1}"},
            {"bones","[BONES] {0}"},
            {"melee_headshot","{1} [MELEE - {3}] {2} {0}"},
            {"melee","{0} [MELEE- {2}] {1}"},
            {"water","[WATER] {0}"},
            {"breath","[BREATH] {0}"},
            {"zombie","[ZOMBIE] {0}"},
            {"animal","[ANIMAL] {0}"},
            {"grenade","[GRENADE] {0}"},
            {"vehicle","[VEHICLE] {0}"},
            {"suicide","[SUICIDE] {0}"},
            {"burning","[BURNING] {0}"},
            {"headshot","+ [HEADSHOT]" },
            {"landmine","[LANDMINE] {0}"},
            {"roadkill","{1} [ROADKILL] {0}"},
            {"bleeding","[BLEEDING] {0}"},
            {"freezing","[FREEZING] {0}"},
            {"sentry","[SENTRY] {0}"},
            {"charge","[CHARGE] {0}"},
            {"missile","[MISSILE] {0}"},
            {"splash","[SPLASH] {0}"},
            {"acid","[ACID] {0}"},
            {"spark", "[SPARK] {0}"},
            {"infection", "[INFECTION] {0}"},
            {"spit","[SPIT] {0}"},
            {"kill","[ADMIN KILL] {0}"},
            {"boulder","[BOULDER] {0}"},
        };

        public void HandleEvent(UnturnedPlayer player, EDeathCause cause, ELimb limb, global::Steamworks.CSteamID murderer)
        {
            UnturnedPlayer killer = UnturnedPlayer.FromCSteamID(murderer);

            Color deathmessageColor = Configuration.Instance.DeathMessagesColor; //ConfigurationInstance.DeathMessagesColor;


            string headshot = Translations.Instance.Translate("headshot");
            if (Configuration.Instance.Causes.Contains(cause.ToString()) || (Instance.Configuration.Instance.ShowSuicideMSG && cause.ToString() == "SUICIDE"))
            {
                if (cause.ToString() != "ROADKILL" && cause.ToString() != "MELEE" && cause.ToString() != "GUN" &&
                    cause.ToString() != "PUNCH")
                {
                    UnturnedChat.Say(Translations.Instance.Translate(cause.ToString().ToLower(), deathmessageColor, player.DisplayName));
                }
                else if (cause.ToString() == "ROADKILL")
                {
                    UnturnedChat.Say(Translations.Instance.Translate("roadkill", deathmessageColor, player.DisplayName, killer.DisplayName));
                }
                else if (cause.ToString() == "MELEE" || cause.ToString() == "GUN")
                {
                    if (limb == ELimb.SKULL)
                        UnturnedChat.Say(Translations.Instance.Translate(cause.ToString().ToLower() + "_headshot", deathmessageColor, player.DisplayName, killer.DisplayName, headshot, killer.Player.equipment.asset.itemName));
                    else
                        UnturnedChat.Say(Translations.Instance.Translate(cause.ToString().ToLower(), deathmessageColor, player.DisplayName, killer.DisplayName, headshot, killer.Player.equipment.asset.itemName));
                }
                else if (cause.ToString() == "PUNCH")
                {
                    UnturnedChat.Say(Translations.Instance.Translate(limb == ELimb.SKULL ? "punch_headshot" : "punch", deathmessageColor, player.DisplayName, killer.DisplayName, headshot));
                }

                return;
            }
            String xy;
            if ((xy=Translations.Instance.Translate(cause.ToString().ToLower())) != null && xy !="")
            {
                if (xy.Contains("{1}"))
                {
                    UnturnedChat.Say(Translations.Instance.Translate(xy, deathmessageColor, player.DisplayName, killer.DisplayName, headshot));
                }
                else
                {
                    UnturnedChat.Say(Translations.Instance.Translate(xy, deathmessageColor, player.DisplayName, headshot));
                }

                return;
            }

            Rocket.Core.Logging.Logger.LogError("Please add translation for " + cause +
                            " | Parameters for custom translation: {0} = Player , {1} = Killer");
        }
    }
}
