﻿using System.Collections.Generic;
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
        string headshot;
        public static SimpleDeathMessages Instance { get; private set; }
        public HashSet<EDeathCause> CausesSet { get; private set; } = new HashSet<EDeathCause>();
        protected override void Load()
        {
            Rocket.Unturned.Events.UnturnedPlayerEvents.OnPlayerDeath += HandleEvent;
            headshot = Translations.Instance.Translate("headshot");
            CausesSet = new HashSet<EDeathCause>(Configuration.Instance.Causes);
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

        public void HandleEvent(UnturnedPlayer player, EDeathCause cause, ELimb limb, Steamworks.CSteamID murderer)
        {
            UnturnedPlayer killer = UnturnedPlayer.FromCSteamID(murderer);
            Color deathmessageColor = Configuration.Instance.DeathMessagesColor; //ConfigurationInstance.DeathMessagesColor;

            //string headshot = Translations.Instance.Translate("headshot");
            if (CausesSet.Contains(cause) || (Configuration.Instance.ShowSuicideMSG && cause == EDeathCause.SUICIDE))
            {
                switch (cause)
                {
                    case EDeathCause.ROADKILL:
                        UnturnedChat.Say(Translations.Instance.Translate("roadkill", player.DisplayName, killer.DisplayName), deathmessageColor);
                        break;
                    case EDeathCause.MELEE:
                    case EDeathCause.GUN:
                        if (limb == ELimb.SKULL)
                            UnturnedChat.Say(Translations.Instance.Translate(cause.ToString().ToLower() + "_headshot", player.DisplayName, killer.DisplayName, headshot, killer.Player.equipment.asset?.itemName), deathmessageColor);
                        else
                            UnturnedChat.Say(Translations.Instance.Translate(cause.ToString().ToLower(), player.DisplayName, killer.DisplayName, headshot, killer.Player.equipment.asset?.itemName), deathmessageColor);
                        break;
                    case EDeathCause.PUNCH:
                        UnturnedChat.Say(Translations.Instance.Translate(limb == ELimb.SKULL ? "punch_headshot" : "punch", player.DisplayName, killer.DisplayName, headshot), deathmessageColor);
                        break;
                    default:
                        UnturnedChat.Say(Translations.Instance.Translate(cause.ToString().ToLower(), player.DisplayName), deathmessageColor);
                        break;
                }

                return;
            }
            string xy = Translations.Instance.Translate(cause.ToString().ToLower());
            if (!string.IsNullOrEmpty(xy))
            {
                if (xy.Contains("{1}"))
                {
                    UnturnedChat.Say(Translations.Instance.Translate(xy, player.DisplayName, killer.DisplayName, headshot), deathmessageColor);
                }
                else
                {
                    UnturnedChat.Say(Translations.Instance.Translate(xy, player.DisplayName, headshot), deathmessageColor);
                }

                return;
            }

            Rocket.Core.Logging.Logger.LogError("Please add translation for " + cause +
                            " | Parameters for custom translation: {0} = Player , {1} = Killer");
        }
    }
}
