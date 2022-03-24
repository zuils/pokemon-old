using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using static Comparison;

public class RedComparisons : RedBlueComparisons
{
    void BlackBeltSave()
    {
        Comparison.Compare("basesaves/red/blackbeltsave.gqs", () => {
            MoveTo(10, 4);
        }, () => {
            MoveTo(10, 5);
            Save();
            MoveTo(10, 4);
        });
    }
    void GioElixer()
    {
        Comparison.Compare("basesaves/red/gioelixer.gqs", () => {
            ClearText();
            MoveTo(1, 32, 8);
            MoveTo(45, 16, 16);
            UseItem("ELIXER", "NIDOKING");
            MoveTo(45, 2, 2);
        }, () => {
            // ClearText();
            // MoveTo(45, 16, 16);
            // UseItem("ELIXER", "NIDOKING");
            // MoveTo(1, 32, 8);
            // MoveTo(45, 2, 2);
            ClearText();
            MoveTo(1, 32, 8);
            UseItem("ELIXER", "NIDOKING");
            MoveTo(45, 2, 2);
        });
    }
    void SilphMaxEther()
    {
        Comparison.Compare("basesaves/red/silphmaxether.gqs", () => {
            // ClearText(Joypad.None,2);
            // AdvanceFrames(wait);
            ClearText();
            MoveTo(3, 7);
            MoveTo(5, 7);
            // MoveTo(235, 3, 3);
            UseItem("MAX ETHER", "NIDOKING", "HORN DRILL");
            MoveTo(235, 3, 5);
            MoveTo(235, 2, 16);
            Press(Joypad.Right);
        }, () => {
            // ClearText(Joypad.None,2);
            // AdvanceFrames(wait);
            ClearText();
            MoveTo(5, 6);
            MoveTo(5, 7);
            // MoveTo(235, 3, 3);
            UseItem("MAX ETHER", "NIDOKING", "HORN DRILL");
            MoveTo(235, 3, 5);
            MoveTo(235, 2, 16);
            Press(Joypad.Right);
        }, true, true, 120);
    }
    void SilphMaxEtherCheckAll()
    {
        void UseAtCoords(int x, int y)
        {
            LoadState("basesaves/red/silphmaxether.gqs");
            Show();
            TimerComponent timer = new TimerComponent(0, 144, 2.0f);
            Scene.AddComponent(timer);

            ClearText();
            MoveTo(x, y);
            UseItem("MAX ETHER", "NIDOKING", "HORN DRILL");
            MoveTo(235, 2, 16);
            Press(Joypad.Right);

            timer.Running = false;
            Console.WriteLine(x + " " + y + "  " + timer.Text);
            AdvanceFrames(50);
            Scene.Dispose();
        }
        int x = 3;
        for(int y = 3; y <= 7; ++y)
            UseAtCoords(x, y);
    }
    void SilphMovement()
    {
        for(int wait = 0; wait < 50; ++wait)
        {
            Console.WriteLine("\nwaiting " + wait);
            Comparison.Compare("basesaves/red/silphelixer.gqs", () => {
                AdvanceFrames(wait);
                ClearText();
                MoveTo(3, 7);
                MoveTo(5, 7);
                MoveTo(3, 3);
            }, () => {
                AdvanceFrames(wait);
                ClearText();
                MoveTo(5, 6);
                MoveTo(5, 7);
                MoveTo(3, 3);
            });
        }
    }
    void LanceStall()
    {
        Scenario LanceXAccStall = () => {
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP", Miss));
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("HYDRO PUMP", 20));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("BLIZZARD"));
        };
        Scenario LanceXSpeedStall = () => {
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP", Miss));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("HYDRO PUMP", 39));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
        };
        Scenario LanceHydroHit = () => {
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP", 20));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
        };
        Scenario LanceNoStall = () => {
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP", Miss));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
        };
        Scenario LanceNoStallBlizzMiss = () => {
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP", Miss));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
        };
        Scenario Champ = () => {
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("WHIRLWIND"));
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("WHIRLWIND"));
            // ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            // ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        };
        Scenario ChampSkyAttack = () => {
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("SKY ATTACK"));
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("SKY ATTACK"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        };
        Comparison.Compare("basesaves/red/lancestall.gqs", () => {
            MoveTo(5, 1);
            ClearText();
            LanceXAccStall();

            Execute("U U");
            ClearText();
            Champ();
        }, () => {
            MoveTo(5, 1);
            ClearText();
            LanceNoStall();

            Execute("U U");
            ClearText();
            Champ();
        });
    }
    void BadgeCheck()
    {
        Comparison.Compare("basesaves/red/badgecheck.gqs", () => {
            Surf();
            MoveTo(10, 96, Action.Up);
            ClearText();
            // MoveTo(8, 86, Action.Up);
            MoveTo(7, 85, Action.Up);
            ClearText();
            MoveTo(8, 71, Action.Up);
            UseItem("SUPER REPEL");
            UseItem("BICYCLE");
            MoveTo(12, 56, Action.Up);
            ClearText();
            MoveTo(5, 35, Action.Up);
            ClearText();
            MoveTo("VictoryRoad1F", 8, 17);
        }, () => {
            Surf();
            MoveTo(10, 96, Action.Up);
            ClearText();

            TalkTo(8, 85, Action.Up); // talk to the girl
            // MoveTo(8, 86, Action.Up);
            // MoveTo(7, 85, Action.Up);
            // ClearText();

            MoveTo(8, 71, Action.Up);
            UseItem("SUPER REPEL");
            UseItem("BICYCLE");

            // TalkTo(10, 56, Action.Up); // talk to the guy
            MoveTo(12, 56, Action.Up);
            ClearText();

            MoveTo(5, 35, Action.Up);
            ClearText();
            MoveTo("VictoryRoad1F", 8, 17);
        });
    }
    void Options()
    {
        Comparison.Compare("basesaves/red/options.gqs", () => {
            ClearText();
            MoveTo(4, 10);
            SetOptions(Fast | Off | Set);
            MoveTo("Route3", 0, 8);
            MoveTo("Route3", 11, 6);
        }, () => {
            ClearText();
            MoveTo(4, 10);
            // MoveTo(2, 39, 16);
            MoveTo("Route3", 0, 8);
            SetOptions(Fast | Off | Set);
            MoveTo("Route3", 11, 6);
        });
    }
    void BirdSwap()
    {
        {
            LoadState("basesaves/red/birdswap.gqs");
            MenuPress(Joypad.B);
            MoveTo("IndigoPlateauLobby", 8, 1);

            Scene s;

            const int sca = 48;
            s = new Scene(this, 10 * sca, 9 * sca);
            s.AddComponent(new VideoBufferComponent(0, 0, 10 * sca, 9 * sca));
            s.AddComponent(new RecordingComponent("right"));

            // MoveTo("IndigoPlateauLobby", 8, 0);
            Execute("U");
            PartySwap("NIDOKING", "PIDGEY");
            Execute("U U");

            AdvanceFrames(300);

            s.Dispose();
        }
        Comparison.Compare("basesaves/red/birdswap.gqs", () => {
            MenuPress(Joypad.B);
            MoveTo("IndigoPlateauLobby", 8, 0);
            MoveTo("LoreleisRoom", 4, 4);
            PartySwap("NIDOKING", "PIDGEY");
            MoveTo("LoreleisRoom", 4, 2);
            Press(Joypad.Right);
        }, () => {
            MenuPress(Joypad.B);
            // MoveTo("IndigoPlateauLobby", 8, 1);
            // PartySwap("NIDOKING", "PIDGEY");
            MoveTo("IndigoPlateauLobby", 8, 0);
            // MoveTo("LoreleisRoom", 4, 5);
            // PartySwap("NIDOKING", "PIDGEY");
            MoveTo("LoreleisRoom", 4, 2);
            Press(Joypad.Right);
        });
    }
    void DrillBruno()
    {
        Comparison.Compare("basesaves/red/drillbruno.gqs", () => {
            TalkTo("BrunosRoom", 5, 2, Action.Right);
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("RAGE", 20));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            // ForceTurn(new RbyTurn("HORN DRILL"));
            // ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        }, () => {
            TalkTo("BrunosRoom", 5, 2, Action.Right);
            // ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn(AiItem)); // X Defend
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("RAGE", 20));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        });
    }
    void RhydonRange()
    {
        Comparison.Compare("basesaves/red/rhydonrange.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("BLIZZARD", 30));
        }, () => {
            ClearText();
            // ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("FISSURE"));
            ForceTurn(new RbyTurn("BLIZZARD", 20), new RbyTurn("FISSURE"));
            // ForceTurn(new RbyTurn("BLIZZARD", 20 + SideEffect));
            ForceTurn(new RbyTurn("BLIZZARD"));
        });
    }
    void PidgeotRange()
    {
        Comparison.Compare("basesaves/red/pidgeotrange.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            // ForceTurn(new RbyTurn("BLIZZARD", 10), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));
        }, () => {
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD", 10), new RbyTurn("AGILITY"));
            // ForceTurn(new RbyTurn("BLIZZARD", 10 + SideEffect), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));
        });
    }
    void BlaineSuper()
    {
        Comparison.Compare("basesaves/red/blainesuper.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn(AiItem));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        }, () => {
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        });
        Comparison.Compare("basesaves/red/blainesuper.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn(AiItem));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        }, () => {
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        });
        Comparison.Compare("basesaves/red/blainesuper.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn(AiItem));
            ForceTurn(new RbyTurn("HORN DRILL"));
        }, () => {
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        });
    }
    void FlyPallet()
    {
        Comparison.Compare("basesaves/red/flypallet.gqs", () => {
            Fly("ViridianCity");
        }, () => {
            Fly("PalletTown");
            Fly("ViridianCity");
        });
    }
    void BikePallet()
    {
        Comparison.Compare("basesaves/red/bikepallet.gqs", () => {
            // MoveTo(4, 8);
            // MoveTo(3, 17);
            // Press(Joypad.Right);
            MoveTo(3, 17, Action.Right);
            UseItem("SUPER REPEL");
            ItemSwap("HELIX FOSSIL", "X SPEED");
            UseItem("HM03", "SQUIRTLE");
            Surf();
        }, () => {
            UseItem("BICYCLE");
            // MoveTo(4, 8);
            // AdvanceFrames(3);
            // MoveTo(3, 17);
            // Press(Joypad.Right);
            MoveTo(3, 17, Action.Right);
            UseItem("SUPER REPEL");
            ItemSwap("HELIX FOSSIL", "X SPEED");
            UseItem("HM03", "SQUIRTLE");
            Surf();
        });
    }
    void Gentleman()
    {
        // Comparison.Compare("basesaves/red/gentlemanit.gqs", () => {
        Comparison.Compare("basesaves/red/gentlemannoit.gqs", () => {
            MoveTo(96, 2, 4);
        }, () => {
            TalkTo(103, 0, 14);
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"));
            PickupItemAt(0, 12);
            MoveTo(96, 2, 4);
        });
    }
    void Deposit()
    {
        Comparison.Compare("basesaves/red/deposit.gqs", () => {
            MoveTo("IndigoPlateauLobby", 8, 1, Action.Up);
        }, () => {
            TalkTo("IndigoPlateauLobby", 15, 7, Action.Up);
            Deposit("SQUIRTLE", "PARAS");
            // Deposit("SQUIRTLE", "PIDGEY", "PARAS");
            MoveTo("IndigoPlateauLobby", 8, 1, Action.Up);
        });
    }
    void Hof()
    {
        byte[][] states = new byte[3][];
        LoadState("basesaves/red/hof1.gqs");
        RunUntil("EnterMap");
        states[0] = SaveState();
        LoadState("basesaves/red/hof2.gqs");
        RunUntil("EnterMap");
        states[1] = SaveState();
        LoadState("basesaves/red/hof4.gqs");
        RunUntil("EnterMap");
        states[2] = SaveState();

        Scenario hof = () => {
            ClearText();
            ClearText(Joypad.None, 26);
            AdvanceFrames(164);
        };

        Comparison.Compare("hof", states, new Scenario[] { hof, hof, hof });
    }
    void SilphBar()
    {
        Scenario StartToArbok = () => {
            ClearText();
            MoveTo("LavenderTown", 7, 10);
            Fly("CeladonCity");
            TalkTo("CeladonPokecenter", 3, 2);
            Yes();
            ClearText(); // healed at center

            MoveTo("CeladonCity", 41, 10);
            UseItem("BICYCLE");

            MoveTo(44, 10);
            Press(Joypad.Right);
            ClearText(); // repel

            MoveTo("Route7Gate", 3, 4);
            ClearText();
            MoveTo("Route7", 18, 10);
            UseItem("BICYCLE");

            PickupItemAt("SilphCo5F", 12, 3);
        };
        Scenario GioToJuggler = () => {
            // SILPH GIOVANNI
            TalkTo(6, 13, Action.Up);
            MoveTo(6, 13);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("HORN ATTACK"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            Execute("D"); // after gio (?)

            TalkTo(236, 3, 0);
            ChooseListItem(9);
            MoveTo(2, 3);
            Execute("D"); // exit elevator

            MoveTo(3, 9);
            MoveTo(2, 9); // thinks trainer is still there (?)
            PickupItemAt(234, 2, 12);
            PickupItemAt(234, 4, 14);
            Dig();

            UseItem("BICYCLE");

            // Snorlax menu
            MoveTo("Route16", 27, 10);
            UseItem("REPEL");
            ItemSwap("HELIX FOSSIL", "X SPECIAL");
            UseItem("POKE FLUTE");
            RunAway();

            MoveTo("Route17", 15, 5);
            PickupItemAt("Route17", 15, 13); // candy

            // MoveTo(18,72);
            MoveTo("Route17", 17, 59);
            PickupItemAt("Route17", 17, 71); // pp up
            Press(Joypad.Down);
            ClearText(); // repel

            // Post cycling
            MoveTo("Route18", 40, 8);
            UseItem("REPEL");
            ItemSwap("PARLYZ HEAL", "TM26");
            UseItem("PP UP", "NIDOKING", "HORN DRILL");
            UseItem("TM26", "NIDOKING", "THRASH");
            UseItem("BICYCLE");

            CutAt("FuchsiaCity", 18, 19);
            CutAt(16, 11);
            MoveTo("SafariZoneGate", 3, 2);
            ClearText();
            Yes();
            ClearText();
            ClearText(); // sneaky joypad call

            UseItem("BICYCLE");

            MoveTo(217, 4, 23);
            UseItem("SUPER REPEL");

            MoveTo(218, 6, 9);
            Press(Joypad.Down);
            ClearText(); // repel

            PickupItemAt("SafariZoneWest", 19, 7, Action.Down); // gold teeth

            TalkTo("SafariZoneSecretHouse", 3, 3);
            MoveTo("SafariZoneWest", 3, 4);
            Dig();

            Fly("FuchsiaCity");
            UseItem("BICYCLE");

            // JUGGLER #1
            TalkTo("FuchsiaGym", 7, 8);
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
        };
        Scenario Koga = () => {
            // KOGA
            TalkTo(4, 10);
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("ELIXER", "NIDOKING"), new RbyTurn("SELFDESTRUCT"), true, false);
            ClearText(7);
            AdvanceFrames(2);
        };
        Scenario Juggler2Lives = () => {
            // JUGGLER #2
            MoveTo(1, 7);
            ClearText();
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("POISON GAS"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
        };
        Scenario Juggler2Dies = () => {
            // JUGGLER #2
            MoveTo(1, 7);
            ClearText();
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("CONFUSION"));
            SendOut("SQUIRTLE");
            ForceTurn(new RbyTurn("REVIVE", "NIDOKING"), new RbyTurn("CONFUSION"));
            SendOut("NIDOKING");
            ForceTurn(new RbyTurn("THUNDERBOLT"));
        };

        Scenario FullSilphBar = () => {
            StartToArbok();

            // ARBOK TRAINER
            TalkTo(8, 16);
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("LEER", Miss));
            ForceTurn(new RbyTurn("HORN DRILL"));

            PickupItemAt(21, 16);
            TalkTo(7, 13);
            TalkTo("SilphCo3F", 17, 9);

            // SILPH RIVAL
            MoveTo("SilphCo7F", 3, 2, Action.Left);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("WHIRLWIND"));
            ItemSwap("POTION", "RARE CANDY");
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("WHIRLWIND"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            MoveTo("SilphCo7F", 5, 7, Action.Right);
            UseItem("MAX ETHER", "NIDOKING", "HORN DRILL");

            // SILPH ROCKET
            TalkTo("SilphCo11F", 3, 16);
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("FOCUS ENERGY"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            GioToJuggler();

            Juggler2Lives();
            // Juggler2Dies();

            Koga();
        };
        Scenario NoSilphBar = () => {
            StartToArbok();

            // ARBOK TRAINER
            TalkTo(8, 16);
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("LEER", Miss));
            ForceTurn(new RbyTurn("THRASH"));

            PickupItemAt(21, 16);
            TalkTo(7, 13);
            TalkTo("SilphCo3F", 17, 9);

            // SILPH RIVAL
            MoveTo("SilphCo7F", 3, 2, Action.Left);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("WHIRLWIND"));
            ItemSwap("POTION", "RARE CANDY");
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("WHIRLWIND"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            MoveTo("SilphCo7F", 5, 7, Action.Right);

            // SILPH ROCKET
            TalkTo("SilphCo11F", 3, 16);
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("FOCUS ENERGY"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("MAX ETHER", "NIDOKING", "HORN DRILL"), new RbyTurn("CONFUSION"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            GioToJuggler();

            Juggler2Lives();

            Koga();
        };
        Scenario DrowzeeBar = () => {
            StartToArbok();

            // ARBOK TRAINER
            TalkTo(8, 16);
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("LEER", Miss));
            ForceTurn(new RbyTurn("THRASH"));

            PickupItemAt(21, 16);
            TalkTo(7, 13);
            TalkTo("SilphCo3F", 17, 9);

            // SILPH RIVAL
            MoveTo("SilphCo7F", 3, 2, Action.Left);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("WING ATTACK"));
            ItemSwap("POTION", "RARE CANDY");
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("WING ATTACK"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            MoveTo("SilphCo7F", 5, 7, Action.Right);

            // SILPH ROCKET
            TalkTo("SilphCo11F", 3, 16);
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("FOCUS ENERGY"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("MAX ETHER", "NIDOKING", "HORN DRILL"), new RbyTurn("PSYCHIC"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            GioToJuggler();

            // Juggler2Lives();
            Juggler2Dies();

            Koga();
        };

        Comparison.Compare("basesaves/red/silphbar.gqs", () => {
            FullSilphBar();
            // DrowzeeBar();
        }, () => {
            NoSilphBar();
        });
    }
    void Bill()
    {
        Comparison.Compare("basesaves/red/bill.gqs", () => {
            ClearText();
            UseItem("POTION", "NIDOKING");
            UseItem("RARE CANDY", "NIDOKING");
            UseItem("ESCAPE ROPE");
        }, () => {
            ClearText();
            UseItem("RARE CANDY", "NIDOKING");
            UseItem("ESCAPE ROPE");
            TalkTo("CeruleanPokecenter", 3, 2);
            Yes();
            ClearText();
            MoveTo(3, 19, 18);
        });
    }
    void BillEther()
    {
        Comparison.Compare("basesaves/red/billether.gqs", () => {
            ClearText();
            // CpuWriteBE<ushort>("wPartyMon1HP", 15 );
            PickupItemAt(38, 3);
            TalkTo("BillsHouse", 6, 5, Action.Right);
            Yes();
            ClearText();
            TalkTo(1, 4);
            TalkTo(4, 4);
            // UseItem("POTION", "NIDOKING");
            // UseItem("POTION", "NIDOKING");
            UseItem("POTION", "NIDOKING");
            UseItem("RARE CANDY", "NIDOKING");
            UseItem("ETHER", "NIDOKING", "MEGA PUNCH");
            UseItem("ESCAPE ROPE");
            MoveTo(3, 18, 18);
        }, () => {
            ClearText();
            TalkTo("BillsHouse", 6, 5, Action.Right);
            Yes();
            ClearText();
            TalkTo(1, 4);
            TalkTo(4, 4);
            UseItem("RARE CANDY", "NIDOKING");
            UseItem("ESCAPE ROPE");
            TalkTo("CeruleanPokecenter", 3, 2);
            Yes();
            ClearText();
            MoveTo(3, 18, 18);
        });
    }
    void AgathaMenu()
    {
        // todo fix this
        Comparison.Compare("basesaves/red/agathamenu.gqs", () => {
            ClearText();
            Execute("U U U");
            UseItem("SUPER POTION", "NIDOKING");
            UseItem("RARE CANDY", "NIDOKING");
            MoveTo("AgathasRoom", 4, 2);
            Press(Joypad.Right);
        }, () => {
            ClearText();
            Execute("U U U");
            MoveTo("AgathasRoom", 4, 3);
            UseItem("SUPER POTION", "NIDOKING");
            UseItem("RARE CANDY", "NIDOKING");
            MoveTo("AgathasRoom", 4, 2);
            Press(Joypad.Right);
        });
    }
    void SabrinaMovement()
    {
        Comparison.Compare("basesaves/red/sabrina.gqs", () => {
            Execute("R");
            UseItem("BICYCLE");
            // MoveTo(178, 10, 8, Action.Left);
            TalkTo("SaffronGym", 9, 8, Action.Left);
        }, () => {
            Execute("R");
            UseItem("BICYCLE");
            MoveTo(10, 1, 6);
            MoveTo(10, 20, 4);
            MoveTo(178, 8, 16);
            MoveTo(178, 16, 15);
            MoveTo(178, 19, 4);
            MoveTo(178, 11, 10);
            // MoveTo(178, 9, 9, Action.Up);
            TalkTo("SaffronGym", 9, 8, Action.Up);
        });
    }
    void ErikaMovement()
    {
        Comparison.Compare("basesaves/red/erika.gqs", () => {
            // AdvanceFrames(10);
            PickupItem();
            Dig();
            UseItem("BICYCLE");
            Execute("L");
            Press(Joypad.Left);
            ClearText(); // repel
            MoveTo(36, 22);
            // MoveTo(35, 31, Action.Down);
            CutAt(35, 32);
            CutAt(134, 2, 4);
            MoveTo(3, 4);
        }, () => {
            // AdvanceFrames(10);
            PickupItem();
            Dig();
            UseItem("BICYCLE");
            Execute("D");
            Press(Joypad.Down);
            ClearText(); // repel
            MoveTo(41, 14);
            MoveTo(37, 23);
            MoveTo(25, 30);
            // MoveTo(35, 31, Action.Down);
            CutAt(35, 32);
            MoveTo(33, 33);
            MoveTo(21, 32);
            MoveTo(5, 29);
            MoveTo(134, 4, 16);
            MoveTo(134, 1, 9);
            CutAt(134, 2, 4);
            MoveTo(3, 4);
        });
    }
    void PostSilphGio()
    {
        Comparison.Compare("basesaves/red/postsilphgio.gqs", () => {
            ClearText();
            Execute("D"); // after gio (?)

            TalkTo(236, 3, 0);
            ChooseListItem(9);
            MoveTo(2, 3);
            Execute("D"); // exit elevator

            MoveTo(3, 9);
        }, () => {
            ClearText();
            Execute("D"); // after gio (?)

            MoveTo(234, 3, 9);
        });
        // return;
        Comparison.Compare("basesaves/red/postsilphgio.gqs", () => {
            ClearText();
            Execute("D"); // after gio (?)

            TalkTo(236, 3, 0);
            ChooseListItem(9);
            MoveTo(2, 3);
            Execute("D"); // exit elevator

            MoveTo(3, 9);
            MoveTo(2, 9); // thinks trainer is still there (?)
            PickupItemAt(234, 2, 12);
            PickupItemAt(234, 4, 14);
            Dig();

            UseItem("BICYCLE");
            MoveTo("Route16", 27, 10);
        }, () => {
            ClearText();
            Execute("D"); // after gio (?)

            MoveTo(212, 5, 6);
            MoveTo(208, 14, 8);
            MoveTo(208, 18, 7);
            MoveTo(208, 18, 1);
            MoveTo(236, 1, 2);

            TalkTo(236, 3, 0);
            ChooseListItem(9);
            MoveTo(3, 2);
            MoveTo(2, 3);
            Execute("D"); // exit elevator

            MoveTo(12, 3);
            MoveTo(6, 7);
            MoveTo(4, 9);

            MoveTo(3, 9);
            MoveTo(2, 9); // thinks trainer is still there (?)
            PickupItemAt(234, 2, 12);
            PickupItemAt(234, 4, 14);
            Dig();

            UseItem("BICYCLE");
            // MoveTo(41, 13);
            MoveTo(31, 13);
            MoveTo(19, 14);
            MoveTo(6, 18);
            MoveTo("Route16", 27, 10);
        });
    }
    void Elevator()
    {
        Comparison.Compare("basesaves/red/elevator.gqs", () => {
            TalkTo(236, 3, 0);
            ChooseListItem(9);

            // MoveTo(236, 3, 1, Action.Up);
            // Execute("A");
            // AdvanceFrames(100, Joypad.Down); // 10f
            // Press(Joypad.A);

            MoveTo(2, 3);
            Execute("D"); // exit elevator
            MoveTo(234, 7, 3);
        }, () => {
            MoveTo(236, 3, 1, Action.Up);
            Execute("A");
            // AdvanceFrames(92, Joypad.Down); // 9f
            // AdvanceFrames(99, Joypad.Down); // 9f
            AdvanceFrames(100, Joypad.Down); // 10f
            // AdvanceFrames(108, Joypad.Down); // 11f
            // AdvanceFrames(114, Joypad.Down); // 11f
            Press(Joypad.A);

            MoveTo(2, 3);
            Execute("D"); // exit elevator
            // MoveTo(234, 8, 2);
            MoveTo(234, 7, 3);
        });
    }
    void CeruleanLedge()
    {
        Comparison.Compare("basesaves/red/ceruleanledge.gqs", () => {
            MoveTo(15, 76, 8);
            MoveTo(15, 76, 10);
            MoveTo(3, 19, 17);
        }, () => {
            MoveTo(15, 77, 8);
            MoveTo(15, 77, 10);
            MoveTo(3, 19, 17);
        });
    }
    void ThrashvsHA()
    {
        LoadState("basesaves/red/oddishthrash.gqs");
        Press(Joypad.A);
        ClearText();
        ForceTurn(new RbyTurn("MEGA PUNCH"));
        ForceTurn(new RbyTurn("HORN ATTACK"));
        byte[] state1 = SaveState();

        Comparison.Compare("oddishthrash", state1, () => {
            TeachLevelUpMove("WATER GUN");
            ForceTurn(new RbyTurn("THRASH"), null, true, false);
        }, () => {
            TeachLevelUpMove("WATER GUN");
            ForceTurn(new RbyTurn("HORN ATTACK"), null, true, false);
        });

        LoadState("basesaves/red/4ttgthrash.gqs");
        Press(Joypad.A);
        ClearText();
        ForceTurn(new RbyTurn("THRASH", ThreeTurn));
        ForceTurn(new RbyTurn("THRASH"));
        byte[] state2 = SaveState();

        Comparison.Compare("4ttgthrash", state2, () => {
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"), null, true, false);
        }, () => {
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("HORN ATTACK"), null, true, false);
        });

        LoadState("basesaves/red/surgethrash.gqs");
        ClearText();
        ForceTurn(new RbyTurn("THRASH", ThreeTurn));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"), new RbyTurn("GROWL", Miss));
        BattleSwitch("PIDGEY", new RbyTurn("THUNDERBOLT"));
        byte[] state3 = SaveState();

        Comparison.Compare("surgethrash", state3, () => {
            SendOut("NIDOKING");
            ForceTurn(new RbyTurn("THRASH"), null, true, false);
        }, () => {
            SendOut("NIDOKING");
            ForceTurn(new RbyTurn("HORN ATTACK"), null, true, false);
        });
    }
    void Boulder()
    {
        Comparison.Compare("basesaves/red/boulder.gqs", () => {
            MoveTo("VictoryRoad3F", 23, 6);
            Strength();
            MoveTo(22, 4);

            for(int i = 0; i < 2; i++) { PushBoulder(Joypad.Up); Execute("U"); }
            Execute("R U");
            for(int i = 0; i < 16; i++) { PushBoulder(Joypad.Left); Execute("L"); }

            Execute("U L");
            PushBoulder(Joypad.Down);
            Execute("R D D");
            for(int i = 0; i < 4; i++) { PushBoulder(Joypad.Left); Execute("L"); }
            Execute("U L");
            for(int i = 0; i < 3; i++) { PushBoulder(Joypad.Down); Execute("D"); }
            Execute("L D");
            PushBoulder(Joypad.Right); Execute("U");
            MoveTo(21, 15, Action.Right);
            PushBoulder(Joypad.Right);
            Execute("R R");
            FallDown();
            AdvanceFrames(300);
        }, () => {
            MoveTo("VictoryRoad3F", 23, 6);
            // return;
            Strength();
            MoveTo(22, 5);

            AdvanceFrames(8, Joypad.Up);
            // AdvanceFrames(140, Joypad.Up);

            AdvanceFrames(165, Joypad.Up);
            AdvanceFrames(10, Joypad.Right);
            AdvanceFrames(10, Joypad.Up);
            AdvanceFrames(1330, Joypad.Left);
            AdvanceFrames(300);
        });
    }
    void MoonXP()
    {
        Comparison.Compare("basesaves/red/moonxp.gqs", () => {
            AfterMoveAndSplit();
            MoveTo(24, 3);
            // ForceEncounter(Action.Right, 9, 0xffff);
            ForceEncounter(Action.Right, 5, 0xffff);
            ClearText();
            RunAway();
            // ForceTurn(new RbyTurn("WATER GUN"));
            MoveTo(26, 3);
            MoveAndSplit(Joypad.Right);
        }, () => {
            AfterMoveAndSplit();
            MoveTo(24, 3);
            // ForceEncounter(Action.Right, 9, 0xffff);
            ForceEncounter(Action.Right, 5, 0xffff);
            ClearText();
            // ForceTurn(new RbyTurn("WATER GUN"));
            ForceTurn(new RbyTurn("POISON STING"));
            MoveTo(26, 3);
            MoveAndSplit(Joypad.Right);
        });
    }
    void EarlyMisty()
    {
        RbyTurn.DefaultRoll = 20;
        Scenario L24Misty = () => {
            // Bill menu
            UseItem("POTION", "NIDOKING");
            UseItem("POTION", "NIDOKING");
            UseItem("POTION", "NIDOKING");
            UseItem("RARE CANDY", "NIDOKING");
            UseItem("ESCAPE ROPE");

            TalkTo("BikeShop", 6, 3);
            No();
            ClearText(); // got instant text

            MoveTo("CeruleanGym", 4, 10);

            // MISTY MINION
            MoveTo(5, 3);
            ClearText();
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("TAIL WHIP"));
            ForceTurn(new RbyTurn("THRASH"));

            // MISTY
            TalkTo(4, 2);
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("WATER GUN"));
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn(AiItem));

            // DIG ROCKET
            MoveTo("CeruleanCity", 30, 9);
            ClearText();
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"));
            MoveTo("CeruleanCity", 36, 19);
        };
        Scenario RedBarMisty = () => {
            // Bill menu
            UseItem("RARE CANDY", "NIDOKING");
            UseItem("ESCAPE ROPE");

            TalkTo("BikeShop", 6, 3);
            No();
            ClearText(); // got instant text

            // DIG ROCKET
            MoveTo("CeruleanCity", 30, 9);
            ClearText();
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("KARATE CHOP", Crit));
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"));

            MoveTo("CeruleanGym", 4, 10);

            // MISTY MINION
            MoveTo(5, 3);
            ClearText();
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("TAIL WHIP"));
            ForceTurn(new RbyTurn("THRASH"));

            // MISTY
            TalkTo(4, 2);
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH", Crit), new RbyTurn(AiItem));
            ForceTurnAndSplit(new RbyTurn("THRASH"));
        };
        Scenario L25Misty = () => {
            // Bill menu
            UseItem("POTION", "NIDOKING");
            UseItem("POTION", "NIDOKING");
            UseItem("RARE CANDY", "NIDOKING");
            UseItem("ESCAPE ROPE");

            TalkTo("BikeShop", 6, 3);
            No();
            ClearText(); // got instant text

            // DIG ROCKET
            MoveTo("CeruleanCity", 30, 9);
            ClearText();
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("KARATE CHOP", Crit));
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"));

            MoveTo("CeruleanGym", 4, 10);

            // MISTY MINION
            MoveTo(5, 3);
            ClearText();
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("TAIL WHIP"));
            ForceTurn(new RbyTurn("THRASH"));

            // MISTY
            TalkTo(4, 2);
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH", Crit), new RbyTurn(AiItem));
            ForceTurn(new RbyTurn("THRASH"));
            MoveTo("CeruleanCity", 36, 19);
            // ForceTurnAndSplit(new RbyTurn("THRASH"));
        };
        Comparison.Compare("basesaves/red/earlymisty.gqs", () => {
            L24Misty();
        }, () => {
            L25Misty();
        });
    }
    void PostMisty()
    {
        Comparison.Compare("basesaves/red/postmisty.gqs", () => {
            ClearText();
            MoveTo(3, 36, 31);
            MoveTo(16, 18, 23);
            MoveTo(16, 17, 27);
        }, () => {
            ClearText();
            MoveTo(3, 22, 18);
            MoveTo(3, 17, 16);
            MoveTo(3, 8, 12);
            MoveTo(3, 32, 11);
            MoveTo(3, 33, 18);

            MoveTo(16, 17, 27);
        });
    }
    void BlaineDig()
    {
        Comparison.Compare("basesaves/red/blainedig.gqs", () => {
            ClearText(6);
            AdvanceFrames(53);
            Press(Joypad.B);
            AdvanceFrames(10);
            OpenStartMenu();
            Dig();
        }, () => {
            ClearText(6);
            AdvanceFrames(53);
            Press(Joypad.B);
            AdvanceFrames(20);
            OpenStartMenu();
            Dig();
        });
    }
    void BlaineFirst()
    {
        Comparison.Compare("basesaves/red/blainefirst.gqs", () => {
            PickupItem();
            Dig();

            UseItem("BICYCLE");
            CutAt(35, 32);
            MoveTo("CeladonGym", 4, 16);//simplified
            {
                CutAt("CeladonGym", 2, 4);
                // BEAUTY
                MoveTo(3, 4);
                ClearText();
                ForceTurn(new RbyTurn("BLIZZARD"));
                // ERIKA
                TalkTo(4, 3);
                ForceTurn(new RbyTurn("EARTHQUAKE"));
                ForceTurn(new RbyTurn("BLIZZARD"));
                ForceTurn(new RbyTurn("EARTHQUAKE"));
                CutAt(5, 7);
            }
            MoveTo("CeladonCity", 12, 28);

            Fly("CinnabarIsland");
            UseItem("BICYCLE");
            MoveTo("CinnabarGym", 16, 16);//simplified
            {
                TalkTo("CinnabarGym", 15, 7, Action.Up);
                BlaineQuiz(Joypad.A);
                TalkTo(10, 1, Action.Up);
                BlaineQuiz(Joypad.B);
                TalkTo(9, 7, Action.Up);
                BlaineQuiz(Joypad.B);
                TalkTo(9, 13, Action.Up);
                BlaineQuiz(Joypad.B);
                TalkTo(1, 13, Action.Up);
                BlaineQuiz(Joypad.A);
                TalkTo(1, 7, Action.Up);
                BlaineQuiz(Joypad.B);
                // BLAINE
                TalkTo(3, 3);
                ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
                ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("AGILITY"));
                ForceTurn(new RbyTurn("HORN DRILL"));
                ForceTurn(new RbyTurn("HORN DRILL"));
                ForceTurn(new RbyTurn("HORN DRILL"));
            }
            Dig();

            UseItem("BICYCLE");
            MoveTo(18, 18, 10);
            UseItem("BICYCLE");
            MoveTo("SaffronGym", 8, 16);
        }, () => {
            PickupItem();
            Dig();

            Fly("CinnabarIsland");
            UseItem("BICYCLE");
            MoveTo("CinnabarGym", 16, 16);//simplified
            {
                TalkTo("CinnabarGym", 15, 7, Action.Up);
                BlaineQuiz(Joypad.A);
                TalkTo(10, 1, Action.Up);
                BlaineQuiz(Joypad.B);
                TalkTo(9, 7, Action.Up);
                BlaineQuiz(Joypad.B);
                TalkTo(9, 13, Action.Up);
                BlaineQuiz(Joypad.B);
                TalkTo(1, 13, Action.Up);
                BlaineQuiz(Joypad.A);
                TalkTo(1, 7, Action.Up);
                BlaineQuiz(Joypad.B);
                // BLAINE
                TalkTo(3, 3);
                ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
                ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("AGILITY"));
                ForceTurn(new RbyTurn("HORN DRILL"));
                ForceTurn(new RbyTurn("HORN DRILL"));
                ForceTurn(new RbyTurn("HORN DRILL"));
            }
            Dig();

            UseItem("BICYCLE");
            CutAt(35, 32);
            MoveTo("CeladonGym", 4, 16);//simplified
            {
                CutAt("CeladonGym", 2, 4);
                // BEAUTY
                MoveTo(3, 4);
                ClearText();
                ForceTurn(new RbyTurn("BLIZZARD"));
                // ERIKA
                TalkTo(4, 3);
                ForceTurn(new RbyTurn("EARTHQUAKE"));
                ForceTurn(new RbyTurn("BLIZZARD"));
                ForceTurn(new RbyTurn("EARTHQUAKE"));
                CutAt(5, 7);
            }
            MoveTo("CeladonCity", 12, 28);

            Fly("SaffronCity");
            UseItem("BICYCLE");
            MoveTo("SaffronGym", 8, 16);
        });
    }
    void FlySaffron()
    {
        Comparison.Compare("basesaves/red/flysaffron.gqs", () => {
            ClearText();
            Dig();
            UseItem("BICYCLE");
            MoveTo(18, 18, 10);
            UseItem("BICYCLE");
            MoveTo("SaffronGym", 8, 17);
        }, () => {
            ClearText();
            Dig();
            Fly("SaffronCity");
            UseItem("BICYCLE");
            MoveTo("SaffronGym", 8, 17);
        });
    }
    void BikevsWalk()
    {
        Comparison.Compare("basesaves/red/biketower.gqs", () => {
            UseItem("BICYCLE");
            MoveTo(14, 5);
        }, () => {
            MoveTo(14, 5);
        });
        Comparison.Compare("basesaves/red/bikekoga.gqs", () => {
            UseItem("BICYCLE");
            // Press(Joypad.Start, Joypad.Down, Joypad.A, Joypad.Up | Joypad.A); // slot2
            // Press(Joypad.Start, Joypad.Down, Joypad.A, Joypad.Up, Joypad.Up | Joypad.A); // slot3
            // ClearText();
            MoveTo(5, 28);
            Inject(Joypad.Up);
            RunUntil("EnterMap");
        }, () => {
            MoveTo(5, 28);
            Inject(Joypad.Up);
            RunUntil("EnterMap");
        });
        Comparison.Compare("basesaves/red/bikeflyhouse.gqs", () => {
            MoveTo(27, 17, 4);
            UseItem("BICYCLE");
            MoveTo(7, 5);
        }, () => {
            MoveTo(27, 7, 5);
        });
    }
    void ChampAnims()
    {
        Scenario Setup = () => {
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("WHIRLWIND"));
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("WHIRLWIND"));
            // ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("WING ATTACK", 20));
        };
        Scenario BlizzPidgeot = () => {
            Setup();
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));
        };
        Scenario EQAlakazam = () => {
            Setup();
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));
        };
        Scenario BlizzRhydon = () => {
            Setup();
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));
        };
        Scenario TBGyarados = () => {
            Setup();
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));
        };
        Scenario DrillAll = () => {
            Setup();
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));
        };

        Comparison.Compare("basesaves/red/champanims_noredbar.gqs", () => {
            CpuWriteBE<ushort>("wPartyMon2HP", 35);
            // CpuWrite("wPartyMon2PP", 6);
            ClearText();
            BlizzRhydon();
        }, () => {
            CpuWriteBE<ushort>("wPartyMon2HP", 35);
            // CpuWrite("wPartyMon2PP", 6);
            ClearText();
            TBGyarados();
        });
    }
    void PalletSurf()
    {
        Comparison.Compare("basesaves/red/palletsurf.gqs", () => {
            MoveTo(4, 8); // no troll

            MoveTo(4, 13, Action.Down);
            UseItem("SUPER REPEL");
            UseItem("HM03", "SQUIRTLE");
            Surf();
            MoveTo(32, 4, 0);
        }, () => {
            MoveTo(4, 8); // no troll
            MoveTo(3, 17);
            Press(Joypad.Right);

            // MoveTo(3, 17, Action.Right); // troll

            UseItem("SUPER REPEL");
            UseItem("HM03", "SQUIRTLE");
            Surf();
            MoveTo(32, 4, 0);
        });
    }
    void EarlyPotions()
    {
        Comparison.Compare("basesaves/red/pcpotion.gqs", () => {
            ClearText();
            TalkTo(0, 1);
            WithdrawItems("POTION", 1);
            MoveTo(7, 1);
        }, () => {
            ClearText();
            MoveTo(7, 1);
        });
        Comparison.Compare("basesaves/red/martguypotion.gqs", () => {
            MoveTo(8, 27);
            TalkTo(5, 24);
            MoveTo(11, 24);
            MoveTo(12, 23);
        }, () => {
            MoveTo(8, 27);
            MoveTo(11, 24);
            MoveTo(12, 23);
        });
        Comparison.Compare("basesaves/red/treepotion.gqs", () => {
            PickupItemAt(14, 4);
            MoveTo(13, 7, 71);
        }, () => {
            MoveTo(13, 7, 71);
        });
        Comparison.Compare("basesaves/red/extrapotion.gqs", () => {
            MoveTo(7, 24);
            PickupItemAt(12, 29);
            MoveTo(1, 21, Action.Up);
        }, () => {
            MoveTo(7, 22);
            MoveTo(1, 21, Action.Up);
        });
        Scenario WeedleFight = () => {
            // ForceTurn(new RbyTurn("TAIL WHIP"), new RbyTurn("STRING SHOT", Miss));
            // ForceTurn(new RbyTurn("TAIL WHIP"), new RbyTurn("STRING SHOT", Miss));
            // ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT", Miss));
            // ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT", Miss));
            // ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT", Miss));
            CpuWriteBE<ushort>("wEnemyMonHP", 0);
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT", Miss));
        };
        Comparison.Compare("basesaves/red/weedlepotion.gqs", () => {
            PickupItemAt(1, 18);
            MoveTo(1, 18);
            ClearText();
            WeedleFight();
            MoveTo(1, 17);
        }, () => {
            TalkTo(2, 18);
            WeedleFight();
            MoveTo(1, 17);
        });
    }
    void BrunoMenu()
    {
        Comparison.Compare("basesaves/red/brunomenu.gqs", () => {
            ClearText();
            Execute("U U U U U");
            UseItem("MAX ETHER", "NIDOKING", "HORN DRILL");
            MoveTo(4, 2);
            Press(Joypad.Right, Joypad.A);
        }, () => {
            ClearText();
            Execute("U");
            UseItem("MAX ETHER", "NIDOKING", "HORN DRILL");
            Execute("U U");
            MoveTo(4, 2);
            Press(Joypad.Right, Joypad.None, Joypad.A);
            // Press(Joypad.Right, Joypad.A);
        });
    }
    void Missvs2Shot()
    {
        Comparison.Compare("basesaves/red/nerd13.gqs", () => {
            ForceTurn(new RbyTurn("WATER GUN", 1), new RbyTurn("POUND", 20));
            ForceTurn(new RbyTurn("POISON STING"), null, true, false);
        }, () => {
            ForceTurn(new RbyTurn("MEGA PUNCH", Miss), new RbyTurn("POUND", 20));
            ForceTurn(new RbyTurn("MEGA PUNCH"), null, true, false);
        });
    }
    void FuchsiaCut()
    {
        Comparison.Compare("basesaves/red/fuchsiacut.gqs", () => {
            // CutAt(18, 19);
            MoveTo(18, 20);
            Press(Joypad.Up);
            Cut();
            CutAt(16, 12);
            MoveTo(18, 4);
            Press(Joypad.Up);
        }, () => {
            // CutAt(18, 19);
            // CutAt(16, 11);
            MoveTo(18, 4);
            Press(Joypad.Up);
        });
    }
    void WildEncounter()
    {
        Comparison.Compare("basesaves/red/wildrt3.gqs", () => {
            MoveTo(27, 9);
            ForceEncounter(Action.Down, 0, 0x8888);
            ClearText();
            RunAway();
            MoveTo(27, 11);
        }, () => {
            MoveTo(27, 11);
        });
        Comparison.Compare("basesaves/red/wildrt1.gqs", () => {
            MoveTo(14, 8);
            ForceEncounter(Action.Up, 1, 0x8888);
            ClearText();
            RunAway();
            MoveTo(14, 5);
        }, () => {
            MoveTo(14, 5);
        });
        Comparison.Compare("basesaves/red/wildmoon.gqs", () => {
            AfterMoveAndSplit();
            MoveTo(24, 3);
            // ForceEncounter(Action.Right, 0, 0x8888); // zubat
            ForceEncounter(Action.Right, 2, 0x8888); // geo
            // ForceEncounter(Action.Right, 5, 0x8888); // paras
            ClearText();
            RunAway();
            MoveTo(26, 3);
            MoveAndSplit(Joypad.Right);
        }, () => {
            AfterMoveAndSplit();
            MoveTo(26, 3);
            MoveAndSplit(Joypad.Right);
        });
        Comparison.Compare("basesaves/red/wildrt6.gqs", () => {
            MoveTo(17, 17);
            ForceEncounter(Action.Down, 1, 0x8888); // pidgey
            // ForceEncounter(Action.Down, 0, 0x8888); // oddish
            // ForceEncounter(Action.Down, 3, 0x8888); // mankey
            ClearText();
            RunAway();
            MoveTo(17, 20);
        }, () => {
            MoveTo(17, 20);
        });
        Comparison.Compare("basesaves/red/wildforest.gqs", () => {
            MoveTo(1, 8);
            // ForceEncounter(Action.Up, 0, 0x8888); // w
            ForceEncounter(Action.Up, 1, 0x8888); // k
            ClearText();
            RunAway();
            MoveTo(1, 5);
        }, () => {
            MoveTo(1, 5);
        });
    }
    void BirdCatch()
    {
        // Comparison.Compare("basesaves/red/birdcatchrb.gqs", ()=>{
        Comparison.Compare("basesaves/red/birdcatch.gqs", () => {
            // CpuWriteBE<ushort>("wPartyMon1HP", 8);
            MoveTo(27, 9);
            ForceEncounter(Action.Down, 0, 0x8888);
            ClearText();
            ForceTurn(new RbyTurn("TACKLE", 20), new RbyTurn("GUST", 20));
            ForceYoloball("POKE BALL");
            ClearText();
            No();
            MoveTo(27, 11);
        }, () => {
            MoveTo(27, 11);
        });
        Comparison.Compare("basesaves/red/birdcatchext.gqs", () => {
            Execute(SpacePath("UAUUAURARRRAU"));
            ForceEncounter(Action.Up, 4, 0x8888);
            ClearText();
            ForceYoloball("POKE BALL");
            ClearText();
            No();
            MoveTo(3, 44);
        }, () => {
            MoveTo(10, 52);
            MoveTo(10, 47);
            MoveTo(3, 44);
        });
    }
    void SaveAndQuitCompare()
    {
        Comparison.Compare("basesaves/red/saveandquit.gqs", () => {
            MoveTo(59, 5, 5);
            Save();
            AdvanceFrames(32);
            AdvanceFrames(105);
            HardReset();
            NoPal.Execute(this);
            Execute("D");
        }, () => {
            MoveTo(60, 5, 6);
        });
    }
    void LanceMenu()
    {
        Comparison.Compare("basesaves/red/lancemenu.gqs", () => {
            MoveTo(6, 8);
            UseItem("ELIXER", "NIDOKING");
            UseItem("SUPER POTION", "NIDOKING");
            MoveTo(6, 7);
        }, () => {
            MoveTo(6, 8);
            UseItem("SUPER POTION", "NIDOKING");
            UseItem("ELIXER", "NIDOKING");
            MoveTo(6, 7);
        });
    }
    void Jingles()
    {
        Comparison.Compare("basesaves/red/itemhidden.gqs", () => {
            PickupItemAt(1, 18);
            MoveTo(1, 18);
        }, () => {
            MoveTo(1, 18);
        });
        Comparison.Compare("basesaves/red/itemhidden2.gqs", () => {
            Execute("L L");
            PickupItem();
            Execute("R R");
        }, () => {
            Execute("L L");
            Execute("R R");
        });
        Comparison.Compare("basesaves/red/itemhiddenzone.gqs", () => {
            Execute("U U");
            PickupItem();
            Execute("D D");
        }, () => {
            LoadState("basesaves/red/itemhidden2.gqs");
            Execute("L L");
            PickupItem();
            Execute("R R");
        });
        Comparison.Compare("basesaves/red/itemball.gqs", () => {
            PickupItemAt(12, 29);
            MoveTo(12, 26);
        }, () => {
            MoveTo(12, 28);
            MoveTo(12, 26);
        });
        Comparison.Compare("basesaves/red/exclamation.gqs", () => {
            MoveTo(1, 18);
            ClearText(2);
        }, () => {
            MoveTo(2, 19, Action.Up);
            Press(Joypad.A);
            ClearText(1);
        });
    }
    void FluteRepel()
    {
        Scenario FluteSplit = () => {
            // RIVAL 4
            MoveTo("PokemonTower2F", 15, 5);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("LEECH SEED"));
            ForceTurn(new RbyTurn("THRASH"));

            // CHANNELER #1
            TalkTo("PokemonTower4F", 15, 7);
            ForceTurn(new RbyTurn("ROCK SLIDE"));
            ForceTurn(new RbyTurn("ROCK SLIDE"));

            PickupItemAt(12, 10); // elixer
            PickupItemAt("PokemonTower5F", 4, 12); // elixer

            MoveTo("PokemonTower5F", 11, 9);
            ClearText(); // heal pad

            // CHANNELER #2
            MoveTo("PokemonTower6F", 15, 5);
            ClearText();
            ForceTurn(new RbyTurn("ROCK SLIDE"));

            // CHANNELER #3
            TalkTo("PokemonTower6F", 9, 5);
            ForceTurn(new RbyTurn("ROCK SLIDE"));

            PickupItemAt(6, 8); // rare candy

            MoveTo(10, 15);
            UseItem("TM07", "NIDOKING", "ROCK SLIDE");

            MoveTo(10, 16);
            ClearText();
            ItemSwap("HM01", "SUPER REPEL");
            UseItem("POKE DOLL"); // escape ghost

            // ROCKET #1
            MoveTo("PokemonTower7F", 10, 11);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));

            // ROCKET #2
            MoveTo(10, 9);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("TACKLE", Miss));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            // ROCKET #3
            MoveTo(10, 7);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"));

            // Get Pokéflute
            TalkTo(10, 3);
            MoveTo(2, 1);
            Press(Joypad.Right);
            ReceiveItemAndSplit();

            ClearText();
            MoveTo("LavenderTown", 7, 10);
            Fly("CeladonCity");
            TalkTo("CeladonPokecenter", 3, 2);
            Yes();
            ClearText(); // healed at center

            MoveTo("CeladonCity", 41, 10);
            UseItem("BICYCLE");

            MoveTo("Route7Gate", 3, 4);
        };
        Comparison.Compare("basesaves/red/fluterepel.gqs", () => {
            AfterMoveAndSplit();
            UseItem("BICYCLE");

            // Fly house
            CutAt("Route16", 34, 9);
            MoveTo("Route16", 17, 4);
            ItemSwap("HELIX FOSSIL", "TM07");
            UseItem("SUPER REPEL");
            UseItem("TM48", "NIDOKING", "MEGA PUNCH");
            UseItem("BICYCLE");

            TalkTo("Route16FlyHouse", 2, 3);

            // Fly menu
            MoveTo("Route16", 7, 6);
            ItemSwap("S.S.TICKET", "X ACCURACY");
            UseItem("HM02", "PIDGEY");
            Fly("LavenderTown");

            FluteSplit();
        }, () => {
            AfterMoveAndSplit();
            UseItem("BICYCLE");

            // Fly house
            CutAt("Route16", 34, 9);
            MoveTo("Route16", 17, 4);
            UseItem("BICYCLE");

            TalkTo("Route16FlyHouse", 2, 3);

            // Fly menu
            MoveTo("Route16", 7, 6);
            ItemSwap("HELIX FOSSIL", "TM07");
            UseItem("SUPER REPEL");
            UseItem("TM48", "NIDOKING", "MEGA PUNCH");
            ItemSwap("S.S.TICKET", "X ACCURACY");
            UseItem("HM02", "PIDGEY");
            Fly("LavenderTown");

            FluteSplit();
        });
        Comparison.Compare("basesaves/red/flymenu.gqs", () => {
            CurrentMenuType = MenuType.None;
            ItemSwap("HELIX FOSSIL", "TM07");
            UseItem("SUPER REPEL");
            UseItem("TM48", "NIDOKING", "MEGA PUNCH");
            UseItem("BICYCLE");

            MoveTo("Route16FlyHouse", 2, 6);
            MoveTo("Route16", 7, 6);

            ItemSwap("S.S.TICKET", "X ACCURACY");
            UseItem("HM02", "PIDGEY");
        }, () => {
            CurrentMenuType = MenuType.None;
            UseItem("BICYCLE");
            MoveTo("Route16FlyHouse", 2, 6);
            MoveTo("Route16", 7, 6);

            ItemSwap("HELIX FOSSIL", "TM07");
            UseItem("SUPER REPEL");
            UseItem("TM48", "NIDOKING", "MEGA PUNCH");
            ItemSwap("S.S.TICKET", "X ACCURACY");
            UseItem("HM02", "PIDGEY");
        });
        Comparison.Compare("basesaves/red/flymenu2.gqs", () => {
            CurrentMenuType = MenuType.None;
            MoveTo("Route16", 17, 4);
            ItemSwap("HELIX FOSSIL", "TM07");
            UseItem("SUPER REPEL");
            UseItem("TM48", "NIDOKING", "MEGA PUNCH");
            UseItem("BICYCLE");

            TalkTo("Route16FlyHouse", 2, 3);

            MoveTo("Route16", 7, 6);
            OpenStartMenu();
            ChooseMenuItem(1 + StartMenuOffset());
            SelectMenuItem(2);
            // ItemSwap("S.S.TICKET", "X ACCURACY");
            // UseItem("HM02", "PIDGEY");
        }, () => {
            CurrentMenuType = MenuType.None;
            MoveTo("Route16", 17, 4);
            UseItem("BICYCLE");

            TalkTo("Route16FlyHouse", 2, 3);

            MoveTo("Route16", 7, 6);
            ItemSwap("HELIX FOSSIL", "TM07");
            UseItem("SUPER REPEL");
            UseItem("TM48", "NIDOKING", "MEGA PUNCH");
            SelectMenuItem(2);
            // ItemSwap("S.S.TICKET", "X ACCURACY");
            // UseItem("HM02", "PIDGEY");
        });
        Comparison.Compare("basesaves/red/flymenu3.gqs", () => {
            // MoveTo("Route16", 17, 4);
            UseItem("BICYCLE");
            MoveTo("Route16FlyHouse", 2, 6);
        }, () => {
            // MoveTo("Route16", 17, 4);
            MoveTo("Route16FlyHouse", 2, 6);
        });
    }
    void RockTunnelRepel()
    {
        Comparison.Compare("basesaves/red/rocktunnelrepel.gqs", () => {
            ClearText();
            MoveTo(34, 19);
            UseItem("REPEL");
            MoveTo(82, 11, 14);
            // MoveTo(82, 13, 14);
            UseItem("REPEL");

            // HIKER
            TalkTo(232, 6, 10);
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));

            // PIDGEY GIRL
            TalkTo("RockTunnel1F", 22, 24);
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"));

            PickupItemAt(21, 16, 53); // max ether

            // GAMBLER
            MoveTo("Route8", 46, 13);
        }, () => {
            ClearText();
            MoveTo(34, 19);
            UseItem("REPEL");
            MoveTo(8, 10);
            UseItem("REPEL");

            // HIKER
            TalkTo(6, 10);
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));

            // PIDGEY GIRL
            TalkTo("RockTunnel1F", 22, 24);
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"));

            PickupItemAt(21, 16, 53); // max ether

            // GAMBLER
            MoveTo("Route8", 46, 13);
        });
    }
    void BikeClick()
    {
        Comparison.Compare("basesaves/red/bikeclick.gqs", () => {
            Execute("U");
            AdvanceFrames(13);
            UseItem("BICYCLE");
            Execute("L");
            // AdvanceFrames(500);
        }, () => {
            AdvanceFrames(1);
            Execute("U");
            AdvanceFrames(12);
            UseItem("BICYCLE");
            Execute("L");
            // AdvanceFrames(500);
        });
    }
    void BlizzRhyhorn()
    {
        Comparison.Compare("basesaves/red/blizzrhyhorn.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        }, () => {
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        });
        Comparison.Compare("basesaves/red/blizzrhyhorn.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD", 1), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("THUNDERBOLT"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        }, () => {
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD", 1), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        });
    }
    void SwagMaxEther()
    {
        Comparison.Compare("basesaves/red/swagmaxether.gqs", () => {
            MoveTo(8, 89);
            Press(Joypad.None, Joypad.Down, Joypad.None);
            PickupItem();
            MoveTo(7, 85);
        }, () => {
            MoveTo(8, 89, Action.Down);
            PickupItem();
            MoveTo(7, 85);
        });
    }
    void RepelWearOff()
    {
        Comparison.Compare("basesaves/red/repelwearoff.gqs", () => {
            MoveTo(21, 15, Action.Right);
            PushBoulder(Joypad.Right);
            // UseItem("MAX ETHER", "NIDOKING", "HORN DRILL");
            UseItem("SUPER REPEL");
            Execute("R R");
            FallDown();

            UseItem("BICYCLE");
            ClearText();
            Strength();
            Execute("D R R U");
            PushBoulder(Joypad.Left);
        }, () => {
            MoveTo(21, 15, Action.Right);
            PushBoulder(Joypad.Right);
            Execute("R R");
            FallDown();

            Strength();
            // UseItem("MAX ETHER", "NIDOKING", "HORN DRILL");
            UseItem("SUPER REPEL");
            UseItem("BICYCLE");
            Execute("D R R U");
            PushBoulder(Joypad.Left);
        });
    }
    void LanceTurnFrame()
    {
        Comparison.Compare("basesaves/red/lanceturnframe.gqs", () => {
            Execute("U");
            MoveTo(6, 3);
            UseItem("ELIXER", "NIDOKING");
            UseItem("SUPER POTION", "NIDOKING");
            Save();
            MoveTo(6, 2);
            ClearText();
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            MoveTo(5, 0);
        }, () => {
            Execute("U");
            MoveTo(5, 2);
            UseItem("ELIXER", "NIDOKING");
            UseItem("SUPER POTION", "NIDOKING");
            Save();
            MoveTo(5, 1);
            ClearText();
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            MoveTo(5, 0);
        });
    }
    void LavenderRival()
    {
        Comparison.Compare("basesaves/red/lavanderrival.gqs", () => {
            MoveTo("PokemonTower2F", 16, 9);
            MoveTo("PokemonTower2F", 16, 5);
            MoveTo("PokemonTower2F", 15, 5);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("LEECH SEED"));
            ForceTurn(new RbyTurn("THRASH"));
            MoveTo("PokemonTower2F", 5, 5);
            MoveTo("PokemonTower2F", 5, 9);
            MoveTo("PokemonTower2F", 3, 9);
        }, () => {
            MoveTo("PokemonTower2F", 18, 7);
            MoveTo("PokemonTower2F", 14, 7);
            MoveTo("PokemonTower2F", 14, 6);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("LEECH SEED"));
            ForceTurn(new RbyTurn("THRASH"));
            MoveTo("PokemonTower2F", 5, 5);
            MoveTo("PokemonTower2F", 5, 9);
            MoveTo("PokemonTower2F", 3, 9);
        });
    }
    void KogaStall()
    {
        Comparison.Compare("basesaves/red/kogastall.gqs", () => {
            ClearText();
            // ForceTurnAndSplit(new RbyTurn("ELIXER", "NIDOKING"), new RbyTurn("SELFDESTRUCT"));
            ForceTurn(new RbyTurn("ELIXER", "NIDOKING"), new RbyTurn(AiItem));
            // ForceTurnAndSplit(new RbyTurn("POKE FLUTE"), new RbyTurn(AiItem));
            // ForceTurnAndSplit(new RbyTurn("POKE FLUTE"), new RbyTurn("SELFDESTRUCT"));
            // ForceTurnAndSplit(new RbyTurn("BUBBLEBEAM"), new RbyTurn(AiItem));
            ForceTurnAndSplit(new RbyTurn("BUBBLEBEAM"), new RbyTurn("SELFDESTRUCT"));
        }, () => {
            ClearText();
            ForceTurn(new RbyTurn("ELIXER", "NIDOKING"), new RbyTurn(AiItem));
            // ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn(AiItem));
            ForceTurnAndSplit(new RbyTurn("X SPECIAL"), new RbyTurn("SELFDESTRUCT"));
        });
    }
    void Yolorelei()
    {
        Comparison.Compare("basesaves/red/yolorelei.gqs", () => {
            TalkTo("IndigoPlateauLobby", 15, 8, Action.Up);
            Deposit("SQUIRTLE", "PIDGEY", "PARAS");

            MoveTo("IndigoPlateauLobby", 8, 0);

            // LORELEI
            TalkTo("LoreleisRoom", 5, 2, Action.Right);
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("REST"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        }, () => {
            TalkTo("IndigoPlateauLobby", 15, 8, Action.Up);
            Deposit("SQUIRTLE", "PARAS");

            MoveTo("IndigoPlateauLobby", 8, 0);
            PartySwap("NIDOKING", "PIDGEY");

            // LORELEI
            TalkTo("LoreleisRoom", 5, 2, Action.Right);
            // BattleSwitch("PIDGEY", new RbyTurn("AURORA BEAM"));
            ForceTurn(new RbyTurn("GUST"), new RbyTurn("AURORA BEAM"));
            SendOut("NIDOKING");
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("REST"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        });
    }
    void ShakeMove()
    {
        Comparison.Compare("basesaves/red/shakemove_it.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("MEGA PUNCH", 10), new RbyTurn("LEER", Miss));
            ForceTurn(new RbyTurn("POISON STING"));
        }, () => {
            ClearText();
            ForceTurn(new RbyTurn("MEGA PUNCH", 10), new RbyTurn("LEER", Miss));
            ForceTurn(new RbyTurn("HORN ATTACK"));
        });
    }
    void NerdVoltorb()
    {
        Comparison.Compare("basesaves/red/nerdvoltorb.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("POISON STING", 1 | SideEffect), new RbyTurn("SCREECH"));
            ForceTurn(new RbyTurn("POISON STING", 1), new RbyTurn("SCREECH"));
            ForceTurn(new RbyTurn("POISON STING", 1), new RbyTurn("SCREECH"));
        }, () => {
            ClearText();
            // ForceTurn(new RbyTurn("WATER GUN", 1 | Crit), new RbyTurn("SCREECH", Miss));
            // ForceTurn(new RbyTurn("WATER GUN", 1 | Crit), new RbyTurn("SCREECH", Miss));
            ForceTurn(new RbyTurn("POISON STING", 1), new RbyTurn("SCREECH"));
            ForceTurn(new RbyTurn("POISON STING", 1), new RbyTurn("SCREECH"));
            ForceTurn(new RbyTurn("POISON STING", 1), new RbyTurn("SCREECH"));
            ForceTurn(new RbyTurn("POISON STING", 1), new RbyTurn("SCREECH"));
        });
    }
    void BC2Caterpie()
    {
        RbyTurn.DefaultRoll = 20;
        Comparison.Compare("basesaves/red/bc2caterpieta.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("LEER"), new RbyTurn("STRING SHOT", Miss));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("STRING SHOT", Miss));
            // ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("HARDEN"));
            BattleMenu(0, 0);
            ChooseMenuItem(2); //ha
            Hold(Joypad.A, SYM["MainInBattleLoop.speedEqual"] + 9);
            A = 0;
        }, () => {
            ClearText();
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("STRING SHOT", Miss));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT", Miss));
            // ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("HARDEN"));
            BattleMenu(0, 0);
            ChooseMenuItem(2); //ha
            Hold(Joypad.A, SYM["MainInBattleLoop.speedEqual"] + 9);
            A = 0;
        });
    }
    void SurfVsWalk()
    {
        Comparison.Compare("surfvswalk", "basesaves/red/surfvswalkw.gqs", "basesaves/red/surfvswalks.gqs", () => {
            MoveTo(4, 17);
            MoveTo(4, 14);
            MoveTo(4, 17);
            MoveTo(4, 14);
        }, () => {
            MoveTo(3, 17);
            MoveTo(3, 14);
            MoveTo(3, 17);
            MoveTo(3, 14);
        });
    }
    void SixtyCans()
    {
        Comparison.Compare("basesaves/red/manip/cans.gqs", () => {
            Execute(SpacePath("DALLLAURUUUUUA"));
            ClearText();
            TalkTo(3, 11, Action.Left);
            MoveTo(4, 9);
        }, () => {
            Execute(SpacePath("SDALLLAURAUUUUUA"));
            ClearText();
            TalkTo(7, 11, Action.Right);
            MoveTo(4, 9);
        });
        Scenario Yolo123 = () => {
            MoveTo(12, 19);
            CpuWrite("wFirstLockTrashCanIndex", 14);
            TalkTo(1, 11, Action.Up);
            TalkTo(5, 11, Action.Right);
            TalkTo(3, 9, Action.Left);
            TalkTo(5, 7, Action.Right);
            TalkTo(1, 7, Action.Down);
            TalkTo(7, 9, Action.Down);
            TalkTo(9, 7, Action.Up);
            TalkTo(9, 11, Action.Down);
            CpuWrite("wSecondLockTrashCanIndex", 13);
            TalkTo(9, 9);
            MoveTo(5, 2, Action.Up);
        };
        Scenario Yolo128 = () => {
            MoveTo(12, 19);
            CpuWrite("wFirstLockTrashCanIndex", 0);
            TalkTo(1, 11, Action.Up);
            TalkTo(5, 11, Action.Up);
            TalkTo(9, 11, Action.Right);
            TalkTo(7, 9, Action.Left);
            TalkTo(9, 7, Action.Up);
            TalkTo(5, 7, Action.Up);
            TalkTo(3, 9, Action.Left);
            TalkTo(1, 7, Action.Down);
            CpuWrite("wSecondLockTrashCanIndex", 0);
            TalkTo(1, 6);
            MoveTo(5, 2, Action.Up);
        };
        Scenario Manip58Cans = () => {
            MoveTo(15, 19);
            SaveAndQuit();
            NoPal.Execute(this, true);
            Execute(SpacePath("DALLLAURUUUUUA"));
            ClearText();
            TalkTo(3, 11, Action.Left);
            MoveTo(5, 2, Action.Up);
        };
        Comparison.Compare("basesaves/red/cans.gqs", () => {
            Yolo128();
        }, () => {
            Yolo123();
        });
    }
    void LateMart()
    {
        Comparison.Compare("basesaves/red/latemart.gqs", () => {
            MoveTo(54, 4, 12);
            TalkTo("PewterMart", 1, 5);
            Buy("POTION", 8);
            MoveTo(2, 34, 17, Action.Right);
        }, () => {
            TalkTo("PewterMart", 1, 5);
            Buy("POTION", 8);
            MoveTo(54, 4, 12);
            MoveTo(2, 34, 17, Action.Right);
        });
    }
    void SideOak()
    {
        Comparison.Compare("basesaves/red/sideoak.gqs", () => {
            MoveTo(40, 5, 3);
            TalkTo(40, 5, 2, Action.Right);
            MoveTo(0, 12, 12);
        }, () => {
            TalkTo(40, 5, 2);
            MoveTo(0, 12, 12);
            // MoveTo(40, 5, 3);
            // TalkTo(40, 5, 2, Action.Down);
            // MoveTo(0, 12, 12);
        });
    }
    void BadLevelUp()
    {
        Comparison.Compare("basesaves/red/badlevelup.gqs", () => {
            CpuWriteBE<ushort>("wPartyMon2HP", 31);
            ClearText();
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("WHIRLWIND"));
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("WHIRLWIND"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));
        }, () => {
            CpuWriteBE<ushort>("wPartyMon2HP", 32);
            ClearText();
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("WHIRLWIND"));
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("WHIRLWIND"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));
        });
    }
    void TeachDrill()
    {
        Comparison.Compare("basesaves/red/teachdrill.gqs", () => {
            ClearText();
            PickupItemAt(6, 8);
            MoveTo(10, 14);
            UseItem("TM07", "NIDOKING", "ROCK SLIDE");
            MoveTo(10, 16);
        }, () => {
            ClearText();
            PickupItemAt(6, 8);
            MoveTo(10, 15);
            UseItem("TM07", "NIDOKING", "ROCK SLIDE");
            MoveTo(10, 16);
        });
    }
    void TeachDrillCheckAll()
    {
        void TeachAtCoords(int x, int y, Action dir = Action.None, bool aftercandy = true)
        {
            if(y < 7) aftercandy = false;
            LoadState("basesaves/red/teachdrill.gqs");
            Show();
            TimerComponent timer = new TimerComponent(0, 144, 2.0f);
            Scene.AddComponent(timer);

            AdvanceFrames(3);
            ClearText();
            if(aftercandy) PickupItemAt(6, 8);
            MoveTo(x, y, dir);
            UseItem("TM07", "NIDOKING", "ROCK SLIDE");
            if(!aftercandy) PickupItemAt(6, 8);
            MoveTo(10, 16);

            timer.Running = false;
            Console.WriteLine(x + " " + y + "  " + timer.Text);
            AdvanceFrames(50);
            Scene.Dispose();
        }
        TeachAtCoords(10, 5);
        for(int x = 10; x >= 6; --x) TeachAtCoords(x, 6);
        TeachAtCoords(6, 7, Action.None, false);
        TeachAtCoords(6, 7, Action.None, true);
        for(int y = 8; y <= 14; ++y) TeachAtCoords(6, y);
        TeachAtCoords(7, 10);
        for(int y = 11; y <= 14; ++y) { TeachAtCoords(7, y, Action.Down); TeachAtCoords(7, y, Action.Right); }
        TeachAtCoords(8, 11);
        TeachAtCoords(9, 11);
        TeachAtCoords(10, 11);
        TeachAtCoords(8, 12, Action.Down); TeachAtCoords(8, 12, Action.Right);
        TeachAtCoords(9, 12, Action.Down); TeachAtCoords(9, 12, Action.Right);
        TeachAtCoords(10, 12, Action.Down); TeachAtCoords(10, 12, Action.Right);
        TeachAtCoords(10, 13);
        TeachAtCoords(8, 14);
        TeachAtCoords(9, 14);
        TeachAtCoords(10, 14, Action.Down); TeachAtCoords(10, 14, Action.Right);
        TeachAtCoords(10, 15);
    }
    void FirstOddishHA()
    {
        RbyTurn.DefaultRoll = 10;
        void NormalExp(ushort hp, bool qa = false)
        {
            Comparison.Compare("basesaves/red/oddishha.gqs", () => {
                CpuWriteBE<ushort>("wPartyMon1HP", hp);
                Press(Joypad.A);
                ClearText();
                ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("ABSORB"));
                ForceTurn(new RbyTurn("POISON STING"));
                ForceTurn(new RbyTurn("HORN ATTACK"), (qa ? new RbyTurn("QUICK ATTACK") : null));
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                TeachLevelUpMove("WATER GUN");
            }, () => {
                CpuWriteBE<ushort>("wPartyMon1HP", hp);
                Press(Joypad.A);
                ClearText();
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                ForceTurn(new RbyTurn("HORN ATTACK"), (qa ? new RbyTurn("QUICK ATTACK") : null));
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                TeachLevelUpMove("WATER GUN");
            });
        }
        void MoonExp(ushort hp, bool qa = false)
        {
            Comparison.Compare("basesaves/red/oddishha.gqs", () => {
                CpuWriteBE<ushort>(SYM["wPartyMon1Exp"] + 1, 8460);
                CpuWriteBE<ushort>("wPartyMon1HP", hp);
                Press(Joypad.A);
                ClearText();
                ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("ABSORB"));
                ForceTurn(new RbyTurn("POISON STING"));
                ForceTurn(new RbyTurn("HORN ATTACK"), (qa ? new RbyTurn("QUICK ATTACK") : null));
                TeachLevelUpMove("WATER GUN");
                ForceTurn(new RbyTurn("HORN ATTACK"));
            }, () => {
                CpuWriteBE<ushort>(SYM["wPartyMon1Exp"] + 1, 8460);
                CpuWriteBE<ushort>("wPartyMon1HP", hp);
                Press(Joypad.A);
                ClearText();
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                ForceTurn(new RbyTurn("HORN ATTACK"), (qa ? new RbyTurn("QUICK ATTACK") : null));
                TeachLevelUpMove("WATER GUN");
                ForceTurn(new RbyTurn("HORN ATTACK"));
            });
        }
        Console.WriteLine("Normal Exp");
        for(ushort hp = 10; hp <= 25; ++hp)
        {
            Console.Write("HP=" + hp + " ");
            NormalExp(hp);
        }
        Console.WriteLine("Moon Exp");
        for(ushort hp = 10; hp <= 25; ++hp)
        {
            Console.Write("HP=" + hp + " ");
            MoonExp(hp);
        }
        MoonExp(16, false);
    }
    void SecondOddishHA()
    {
        RbyTurn.DefaultRoll = 10;
        void Stall(ushort hp)
        {
            Comparison.Compare("basesaves/red/oddishha.gqs", () => {
                CpuWriteBE<ushort>("wPartyMon1HP", (ushort) (hp + 5));
                Press(Joypad.A);
                ClearText();
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("QUICK ATTACK"));
                ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("ABSORB"));
                ForceTurn(new RbyTurn("POISON STING"));
                TeachLevelUpMove("WATER GUN");
            }, () => {
                CpuWriteBE<ushort>("wPartyMon1HP", (ushort) (hp + 5));
                Press(Joypad.A);
                ClearText();
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("QUICK ATTACK"));
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                TeachLevelUpMove("WATER GUN");
            });
        }
        for(ushort hp = 10; hp <= 25; ++hp)
        {
            Console.Write("\nHP=" + hp + " ");
            Stall(hp);
        }
    }
    void BridgeRivalWalk()
    {
        for(int wait = 0; wait < 20; ++wait)
            Comparison.Compare("basesaves/red/bridgerivalwalk.gqs", () => {
                AdvanceFrames(wait);
                MoveTo("CeruleanCity", 20, 6, Action.Up);
                ClearText();
                ForceTurn(new RbyTurn("HORN ATTACK", Crit), new RbyTurn("SAND-ATTACK", Miss));
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                ForceTurn(new RbyTurn("HORN ATTACK"));
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                ForceTurn(new RbyTurn("MEGA PUNCH", Crit));
                TalkTo("Route24", 11, 31);
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                MoveTo(10, 29);
                // MoveTo("Route24", 11, 32);
                // MoveTo(10, 32);
            }, () => {
                AdvanceFrames(wait);
                MoveTo("CeruleanCity", 21, 6, Action.Up);
                ClearText();
                ForceTurn(new RbyTurn("HORN ATTACK", Crit), new RbyTurn("SAND-ATTACK", Miss));
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                ForceTurn(new RbyTurn("HORN ATTACK"));
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                ForceTurn(new RbyTurn("MEGA PUNCH", Crit));
                TalkTo("Route24", 11, 31);
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                ForceTurn(new RbyTurn("MEGA PUNCH"));
                MoveTo(10, 29);
                // MoveTo("Route24", 11, 32);
                // MoveTo(10, 32);
            });
    }
    void LanceMiss()
    {
        // miss drago 1
        Comparison.Compare("basesaves/red/lancemiss.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"), null, true, false);
        }, () => {
            ClearText();
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"), null, true, false);
        });
        // miss dragonite
        Comparison.Compare("basesaves/red/lancemiss.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("BARRIER"));
            ForceTurn(new RbyTurn("BLIZZARD"), null, true, false);
        }, () => {
            ClearText();
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"), null, true, false);
        });
        // x spec vs x speed
        // no extra miss
        Comparison.Compare("basesaves/red/lancemiss.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("AGILITY"));

            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));

            ForceTurn(new RbyTurn("BLIZZARD"), null, true, false);
        }, () => {
            ClearText();
            ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("AGILITY"));

            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));

            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), null, true, false);
        });
        // miss 1
        Comparison.Compare("basesaves/red/lancemiss.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("AGILITY"));

            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));

            ForceTurn(new RbyTurn("BLIZZARD"), null, true, false);
        }, () => {
            ClearText();
            ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("AGILITY"));

            ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("AGILITY"));
            // ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));

            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), null, true, false);
        });
        // miss 2
        Comparison.Compare("basesaves/red/lancemiss.gqs", () => {
            ClearText();
            ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("AGILITY"));

            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));

            ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"), true, false);
        }, () => {
            ClearText();
            ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("AGILITY"));

            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));

            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"), true, false);
        });
    }
    void EarlyElixer()
    {
        bool range = true;
        Comparison.Compare("basesaves/red/earlyelixer.gqs", () => {
            LoadState("basesaves/red/blainesuper.gqs");
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn(AiItem));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));

            LoadState("basesaves/red/earlyelixer.gqs");
            UseItem("ELIXER", "NIDOKING");
            UseItem("BICYCLE");

            // RHYHORN
            MoveTo("ViridianGym", 15, 5);
            ClearText();
            ForceTurn(new RbyTurn("EARTHQUAKE"));

            // BLACKBELT
            MoveTo(10, 4);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("FOCUS ENERGY"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            MoveTo("ViridianCity", 32, 8);
            MoveTo("ViridianGym", 16, 16);

            // GIOVANNI
            TalkTo("ViridianGym", 2, 1);
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            if(range)
            {
                ForceTurn(new RbyTurn("BLIZZARD"));
            }
            else
            {
                ForceTurn(new RbyTurn("BLIZZARD", 1), new RbyTurn("FISSURE"));
                ForceTurn(new RbyTurn("EARTHQUAKE"));
            }

            MoveTo("ViridianCity", 32, 8);
            UseItem("SUPER REPEL");
            UseItem("BICYCLE");

            // VIRIDIAN RIVAL
            MoveTo("Route22", 29, 5);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        }, () => {
            LoadState("basesaves/red/blainesuper.gqs");
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn(AiItem));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));

            LoadState("basesaves/red/earlyelixer.gqs");
            UseItem("BICYCLE");

            // RHYHORN
            MoveTo("ViridianGym", 15, 5);
            ClearText();
            ForceTurn(new RbyTurn("EARTHQUAKE"));

            // BLACKBELT
            MoveTo(10, 4);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("FOCUS ENERGY"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            MoveTo("ViridianCity", 32, 8);
            MoveTo("ViridianGym", 16, 16);
            UseItem("ELIXER", "NIDOKING");

            // GIOVANNI
            TalkTo("ViridianGym", 2, 1);
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            if(range)
            {
                ForceTurn(new RbyTurn("BLIZZARD"));
            }
            else
            {
                ForceTurn(new RbyTurn("BLIZZARD", 1), new RbyTurn("FISSURE"));
                ForceTurn(new RbyTurn("BLIZZARD", 1));
            }

            MoveTo("ViridianCity", 32, 8);
            UseItem("SUPER REPEL");
            UseItem("BICYCLE");

            // VIRIDIAN RIVAL
            MoveTo("Route22", 29, 5);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        });
    }
    void LanceElixer()
    {
        Comparison.Compare("basesaves/red/lanceelixer.gqs", () => {
            Execute("U");
            MoveTo(5, 1);
            ClearText();
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("ELIXER", "NIDOKING"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurnAndSplit(new RbyTurn("BLIZZARD"));
        }, () => {
            Execute("U");
            UseItem("ELIXER", "NIDOKING");
            MoveTo(5, 1);
            ClearText();
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurnAndSplit(new RbyTurn("BLIZZARD"));
        });
        Comparison.Compare("basesaves/red/lanceelixer.gqs", () => {
            CpuWriteBE<ushort>("wPartyMon2HP", 20);
            Execute("U U");
            MoveTo(5, 1);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("ELIXER", "NIDOKING"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurnAndSplit(new RbyTurn("BLIZZARD"));
        }, () => {
            CpuWriteBE<ushort>("wPartyMon2HP", 20);
            Execute("U U");
            UseItem("ELIXER", "NIDOKING");
            MoveTo(5, 1);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurnAndSplit(new RbyTurn("BLIZZARD"));
        });
    }
    void HealPadSkip()
    {
        Scenario Flute = () => {
            MoveTo("PokemonTower6F", 15, 5);
            ClearText();
            ForceTurn(new RbyTurn("ROCK SLIDE"));

            TalkTo("PokemonTower6F", 9, 5);
            ForceTurn(new RbyTurn("ROCK SLIDE"));

            PickupItemAt(6, 8);

            MoveTo(10, 15);
            UseItem("TM07", "NIDOKING", "ROCK SLIDE");

            MoveTo(10, 16);
            ClearText();
            ItemSwap("HM01", "SUPER REPEL");
            UseItem("POKE DOLL");

            MoveTo("PokemonTower7F", 10, 11);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));

            MoveTo(10, 9);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            MoveTo(10, 7);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"));

            // Get Pokéflute
            TalkTo(10, 3);
            MoveTo(2, 1);
            Press(Joypad.Right);
            ReceiveItemAndSplit();
        };
        Scenario Silph = () => {
            ClearText();
            MoveTo("LavenderTown", 7, 10);
            Fly("CeladonCity");
            TalkTo("CeladonPokecenter", 3, 2);
            Yes();
            ClearText(); // healed at center

            MoveTo("CeladonCity", 41, 10);
            UseItem("BICYCLE");

            MoveTo("Route7Gate", 3, 4);
            ClearText();
            MoveTo("Route7", 18, 10);
            UseItem("BICYCLE");

            PickupItemAt("SilphCo5F", 12, 3);

            // ARBOK TRAINER
            TalkTo(8, 16);
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("LEER"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            PickupItemAt(21, 16);
            TalkTo(7, 13);
            TalkTo("SilphCo3F", 17, 9);

            // SILPH RIVAL
            MoveTo("SilphCo7F", 3, 2, Action.Left);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("WHIRLWIND"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("WHIRLWIND"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            MoveTo(5, 7, Action.Right);
            UseItem("ELIXER", "NIDOKING");

            // SILPH ROCKET
            TalkTo("SilphCo11F", 3, 16);
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("FOCUS ENERGY"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            // SILPH GIOVANNI
            TalkTo(6, 13, Action.Up);
            MoveTo(6, 13);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("HORN ATTACK"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));
        };
        Scenario Safari = () => {
            ClearText();
            MoveTo(6, 14); // blocked by door?

            TalkTo(236, 3, 0);
            ChooseListItem(9);
            MoveTo(2, 3);
            Execute("D"); // exit elevator

            // Get candy and EQ
            MoveTo(3, 9);
            MoveTo(2, 9); // thinks trainer is still there
            PickupItemAt(234, 2, 12);
            PickupItemAt(234, 4, 14);
            Dig();

            UseItem("BICYCLE");

            // Snorlax menu
            MoveTo("Route16", 27, 10);
            UseItem("REPEL");
            ItemSwap("PARLYZ HEAL", "RARE CANDY");
            UseItem("POKE FLUTE");
            RunAway();

            PickupItemAt("Route17", 15, 14); // candy

            // Post cycling menu
            MoveTo("Route18", 40, 8);
            UseItem("REPEL");
            ItemSwap("POTION", "X SPECIAL");
            UseItem("TM26", "NIDOKING", "THRASH");
            UseItem("BICYCLE");

            CutAt("FuchsiaCity", 18, 19);
            CutAt(16, 11);
            MoveTo("SafariZoneGate", 3, 2);
            ClearText();
            Yes();
            ClearText();
            ClearText(); // sneaky joypad call

            UseItem("BICYCLE");

            MoveTo(217, 4, 23);
            UseItem("SUPER REPEL");
            CloseMenu(Joypad.Up); // direction close

            MoveTo(218, 7, 13);
            PickupItemAt("SafariZoneWest", 19, 7, Action.Down); // gold teeth

            TalkTo("SafariZoneSecretHouse", 3, 3);
            MoveTo("SafariZoneWest", 3, 4);
            Dig();
        };
        Scenario Koga = () => {
            Fly("FuchsiaCity");
            UseItem("BICYCLE");

            // JUGGLER #1
            TalkTo("FuchsiaGym", 7, 8);
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));

            // JUGGLER #2
            MoveTo(1, 7);
            ClearText();
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("POISON GAS"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));

            // KOGA
            TalkTo(4, 10);
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurnAndSplit(new RbyTurn("ELIXER", "NIDOKING"), new RbyTurn("SELFDESTRUCT"));
        };
        Comparison.Compare("basesaves/red/healpadskip.gqs", () => {
            CpuWriteBE<ushort>("wPartyMon1HP", 10);
            MoveTo("PokemonTower5F", 15, 7);
            ClearText();
            ForceTurn(new RbyTurn("ROCK SLIDE"));

            Flute();

            Silph();
            Safari();
            Koga();
        }, () => {
            MoveTo("PokemonTower5F", 11, 9);
            ClearText(); // heal pad

            Flute();

            Silph();
            Safari();
            Koga();
        }, false);
    }
    void WrapIT()
    {
        Comparison.Compare("basesaves/red/bridge3wrapnoit.gqs", () => {
        // Comparison.Compare("basesaves/red/bridge3wrapit.gqs", () => {
            ForceTurn(new RbyTurn("MEGA PUNCH", Miss), new RbyTurn("WRAP", 20 | 5 * Turns));
            ForceTurn(new RbyTurn());
            ForceTurn(new RbyTurn());
            ForceTurn(new RbyTurn());
            ForceTurn(new RbyTurn());
            ForceTurn(new RbyTurn("MEGA PUNCH"));
        }, () => {
            ForceTurn(new RbyTurn("MEGA PUNCH"));
        });
    }

    Comparison Comparison;
    public RedComparisons() : base()
    {
        Comparison = new Comparison(this);
        WrapIT();
        Environment.Exit(0);
    }
}
