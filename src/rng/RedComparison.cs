using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class RedComparison : RedGlitchless {
    public delegate void Scenario();
    public void Comparison(string name, byte[] state, Scenario left, Scenario right, int wait=300)
    {
        LoadState(state);

        Scene s;

        s = new Scene(this, 160, 160);
        s.AddComponent(new VideoBufferComponent(0, 0, 160, 144));
        s.AddComponent(new RecordingComponent("right"));
        TimerComponent tright=new TimerComponent(0,144,2.0f);
        s.AddComponent(tright);

        right();

        tright.Running = false;
        TimeSpan diff=tright.Duration();
        AdvanceFrames(wait);

        s.Dispose();
        LoadState(state);

        s = new Scene(this, 160, 160);
        s.AddComponent(new VideoBufferComponent(0, 0, 160, 144));
        s.AddComponent(new RecordingComponent("left"));
        TimerComponent tleft=new TimerComponent(0,144,2.0f);
        s.AddComponent(tleft);

        left();

        tleft.Running = false;
        diff=diff-tleft.Duration();
        if(diff.TotalSeconds>0)
            AdvanceFrames((int)(diff.TotalSeconds*59.7));
        AdvanceFrames(wait);

        s.Dispose();

        Console.WriteLine("left:  " + tleft.Text  + "   " + (diff.TotalSeconds<0?"+":"-") + diff.ToString("ss\\.fffffff"));
        Console.WriteLine("right: " + tright.Text + "   " + (diff.TotalSeconds<0?"-":"+") + diff.ToString("ss\\.fffffff"));

        FFMPEG.RunFFMPEGCommand("-y -i movies/left.mp4 -i movies/right.mp4 -filter_complex hstack movies/" + name + ".mp4");
    }
    public void Comparison(string statepath, Scenario left, Scenario right, int wait=300)
    {
        LoadState(statepath);
        byte[] state = SaveState();
        string name = Regex.Match(statepath, @"([^/\\]+)\.gqs").Groups[1].Value;
        Comparison(name, state, left, right, wait);
    }

    void BlackBeltSave()
    {
        Comparison("basesaves/red/blackbeltsave.gqs", ()=>{
            MoveTo(10, 4);
        }, ()=>{
            MoveTo(10, 5);
            Save();
            MoveTo(10, 4);
        });
    }
    void GioElixer()
    {
        Comparison("basesaves/red/gioelixer.gqs", ()=>{
            ClearText();
            MoveTo(1, 32, 8);
            MoveTo(45, 16, 16);
            UseItem("ELIXER", "NIDOKING");
            MoveTo(45, 2, 2);
        }, ()=>{
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
        // const int wait=64;
        Comparison("basesaves/red/silphmaxether.gqs", ()=>{
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
        }, ()=>{
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
        }, 120);
        // return;
        int x=3;
        for(int y=3; y<=7; ++y)
        {
            LoadState("basesaves/red/silphmaxether.gqs");
            Scene s;
            s = new Scene(this, 160, 160);
            s.AddComponent(new VideoBufferComponent(0, 0, 160, 144));
            // s.AddComponent(new RecordingComponent("right"));
            TimerComponent timer=new TimerComponent(0,144,2.0f);
            s.AddComponent(timer);
            ClearText();
            // MoveTo(3, 7);
            MoveTo(x, y);
            UseItem("MAX ETHER", "NIDOKING", "HORN DRILL");
            MoveTo(235, 2, 16);
            Press(Joypad.Right);
            timer.Running = false;
            Console.WriteLine(x+" "+y+"  "+timer.Text);
            AdvanceFrames(50);
            s.Dispose();
        }
    }
    void LanceStall()
    {
        Comparison("basesaves/red/lancestall.gqs", ()=>{
            // stall
            MoveTo(5, 1);
            // x acc stall
            // ClearText();
            // ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP", Miss));
            // ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("HYDRO PUMP", 20));
            // ForceTurn(new RbyTurn("THUNDERBOLT"));
            // ForceTurn(new RbyTurn("BLIZZARD"));
            // ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            // ForceTurn(new RbyTurn("BLIZZARD"));
            // ForceTurn(new RbyTurn("BLIZZARD"));
            // ForceTurn(new RbyTurn("BLIZZARD"));
            // x speed stall
            // ClearText();
            // ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP", Miss));
            // ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("HYDRO PUMP", 39));
            // ForceTurn(new RbyTurn("THUNDERBOLT"));
            // ForceTurn(new RbyTurn("BLIZZARD"));
            // ForceTurn(new RbyTurn("BLIZZARD"));
            // ForceTurn(new RbyTurn("THUNDERBOLT"));
            // ForceTurn(new RbyTurn("BLIZZARD"));
            // optimal scenario
            ClearText();
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP", 20));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            Execute("U U");
            ClearText();
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("WHIRLWIND"));
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("WHIRLWIND"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            // ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        }, ()=>{
            // no stall
            // MoveTo(5, 1);
            // ClearText();
            // ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP", Miss));
            // ForceTurn(new RbyTurn("THUNDERBOLT"));
            // ForceTurn(new RbyTurn("BLIZZARD"));
            // ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            // ForceTurn(new RbyTurn("BLIZZARD"));
            // ForceTurn(new RbyTurn("THUNDERBOLT"));
            // ForceTurn(new RbyTurn("BLIZZARD"));
            // Execute("U U");
            // ClearText();
            // ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("WHIRLWIND"));
            // ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("WHIRLWIND"));
            // // ForceTurn(new RbyTurn("BLIZZARD"));
            // ForceTurn(new RbyTurn("HORN DRILL"));
            // ForceTurn(new RbyTurn("HORN DRILL"));
            // ForceTurn(new RbyTurn("HORN DRILL"));
            // ForceTurn(new RbyTurn("HORN DRILL"));
            // ForceTurn(new RbyTurn("HORN DRILL"));
            // ForceTurn(new RbyTurn("HORN DRILL"));
            // no stall blizz miss sky attack fury attack
            MoveTo(5, 1);
            ClearText();
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP", Miss));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            // ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("AGILITY"));
            // ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            Execute("U U");
            ClearText();
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("SKY ATTACK"));
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("SKY ATTACK"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        });
    }
    void BadgeCheck()
    {
        Comparison("basesaves/red/badgecheck.gqs", ()=>{
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
        }, ()=>{
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
        Comparison("basesaves/red/options.gqs", ()=>{
            ClearText();
            MoveTo(4, 10);
            SetOptions(Fast | Off | Set);
            MoveTo("Route3", 0, 8);
            MoveTo("Route3", 11, 6);
        }, ()=>{
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

            const int sca=48;
            s = new Scene(this, 10*sca, 9*sca);
            s.AddComponent(new VideoBufferComponent(0, 0, 10*sca, 9*sca));
            s.AddComponent(new RecordingComponent("right"));
            // TimerComponent tright=new TimerComponent(0,144*2,2.0f*2);
            // s.AddComponent(tright);

            // MoveTo("IndigoPlateauLobby", 8, 0);
            Execute("U");
            PartySwap("NIDOKING", "PIDGEY");
            Execute("U U");

            // tright.Running = false;
            // TimeSpan diff=tright.Duration(this);
            AdvanceFrames(300);

            s.Dispose();
            // return;
        }
        Comparison("basesaves/red/birdswap.gqs", ()=>{
            MenuPress(Joypad.B);
            MoveTo("IndigoPlateauLobby", 8, 0);
            MoveTo("LoreleisRoom", 4, 4);
            PartySwap("NIDOKING", "PIDGEY");
            MoveTo("LoreleisRoom", 4, 2);
            Press(Joypad.Right);
        }, ()=>{
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
        Comparison("basesaves/red/drillbruno.gqs", ()=>{
            TalkTo("BrunosRoom", 5, 2, Action.Right);
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("ROCK THROW",1)); //RAGE BUG
            ForceTurn(new RbyTurn("BLIZZARD", Crit));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("BLIZZARD", Crit));
            ForceTurn(new RbyTurn("HORN DRILL"));
        }, ()=>{
            TalkTo("BrunosRoom", 5, 2, Action.Right);
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("ROCK THROW",1)); //RAGE BUG
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
        });
    }
    void RhydonRange()
    {
        Comparison("basesaves/red/rhydonrange.gqs", ()=>{
            ClearText();
            ForceTurn(new RbyTurn("BLIZZARD",30));
        }, ()=>{
            ClearText();
            // ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("FISSURE"));
            ForceTurn(new RbyTurn("BLIZZARD", 20), new RbyTurn("FISSURE"));
            // ForceTurn(new RbyTurn("BLIZZARD", 20 + SideEffect));
            ForceTurn(new RbyTurn("BLIZZARD"));
        });
    }
    void PidgeotRange()
    {
        Comparison("basesaves/red/pidgeotrange.gqs", ()=>{
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            // ForceTurn(new RbyTurn("BLIZZARD", 10), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD"), new RbyTurn("AGILITY"));
        }, ()=>{
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
        Comparison("basesaves/red/blainesuper.gqs", ()=>{
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY", AiItem));
            ForceTurn(new RbyTurn("HORN DRILL"), new RbyTurn("AGILITY"));
        }, ()=>{
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("AGILITY"));
        });
    }
    void FlyPallet()
    {
        Comparison("basesaves/red/flypallet.gqs", ()=>{
            Fly("ViridianCity");
        }, ()=>{
            Fly("PalletTown");
            Fly("ViridianCity");
        });
    }
    void BikePallet()
    {
        Comparison("basesaves/red/bikepallet.gqs", ()=>{
            // MoveTo(4, 8);
            // MoveTo(3, 17);
            // Press(Joypad.Right);
            MoveTo(3, 17, Action.Right);
            UseItem("SUPER REPEL");
            ItemSwap("HELIX FOSSIL", "X SPEED");
            UseItem("HM03", "SQUIRTLE");
            Surf();
        }, ()=>{
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
        // Comparison("basesaves/red/gentlemanit.gqs", ()=>{
        Comparison("basesaves/red/gentlemannoit.gqs", ()=>{
            MoveTo(96, 2, 4);
        }, ()=>{
            TalkTo(103, 0, 14);
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"));
            PickupItemAt(0,12);
            MoveTo(96, 2, 4);
        });
    }
    void Deposit()
    {
        Comparison("basesaves/red/deposit.gqs", ()=>{
            // MoveTo("IndigoPlateauLobby", 15, 9);
            MoveTo("IndigoPlateauLobby", 8, 1, Action.Up);
        }, ()=>{
            // MoveTo("IndigoPlateauLobby", 15, 9);
            TalkTo("IndigoPlateauLobby", 15, 7, Action.Up);
            ChooseMenuItem(0);
            ClearText();
                ChooseMenuItem(1);
                ChooseMenuItem(1);
                ChooseMenuItem(0);
                ClearText();
                // ChooseMenuItem(1);
                // ChooseMenuItem(1);
                // ChooseMenuItem(0);
                // ClearText();
                // ChooseMenuItem(1);
                // ChooseMenuItem(1);
                // ChooseMenuItem(0);
                // ClearText();
                ChooseMenuItem(1);
                ChooseMenuItem(2);
                ChooseMenuItem(0);
                ClearText();
            MenuPress(Joypad.B);
            MenuPress(Joypad.B);
            MoveTo("IndigoPlateauLobby", 8, 1, Action.Up);
        });
    }
    void Hof()
    {
        const int hof=164;
        const int wait=300;
        Scene s;

        LoadState("basesaves/red/hof4.gqs");
        s = new Scene(this, 160, 160);
        s.AddComponent(new VideoBufferComponent(0, 0, 160, 144));
        s.AddComponent(new RecordingComponent("right"));
        RunUntil("EnterMap");
        TimerComponent tright=new TimerComponent(0,144,2.0f);
        s.AddComponent(tright);

            ClearText();
            ClearText(Joypad.None, 26);
            AdvanceFrames(hof);

        tright.Running = false;
        TimeSpan diff=tright.Duration();
        AdvanceFrames(wait);
        s.Dispose();

        LoadState("basesaves/red/hof2.gqs");
        s = new Scene(this, 160, 160);
        s.AddComponent(new VideoBufferComponent(0, 0, 160, 144));
        s.AddComponent(new RecordingComponent("left"));
        RunUntil("EnterMap");
        TimerComponent tleft=new TimerComponent(0,144,2.0f);
        s.AddComponent(tleft);

            ClearText();
            ClearText(Joypad.None, 26);
            AdvanceFrames(hof);

        tleft.Running = false;
        TimeSpan diff1=diff-tleft.Duration();
        if(diff1.TotalSeconds>0)
            AdvanceFrames((int)(diff1.TotalSeconds*59.7));
        AdvanceFrames(wait);
        s.Dispose();

        LoadState("basesaves/red/hof1.gqs");
        s = new Scene(this, 160, 160);
        s.AddComponent(new VideoBufferComponent(0, 0, 160, 144));
        s.AddComponent(new RecordingComponent("leftleft"));
        RunUntil("EnterMap");
        TimerComponent t3=new TimerComponent(0,144,2.0f);
        s.AddComponent(t3);

            ClearText();
            ClearText(Joypad.None, 26);
            AdvanceFrames(hof);

        t3.Running = false;
        diff=diff-t3.Duration();
        if(diff.TotalSeconds>0)
            AdvanceFrames((int)(diff.TotalSeconds*59.7));
        AdvanceFrames(wait);
        s.Dispose();

        Console.WriteLine("left: " + t3.Text);
        Console.WriteLine("mid: " + tleft.Text);
        Console.WriteLine("right: " + tright.Text);
        Console.WriteLine("diff1: " + diff1);
        Console.WriteLine("diff2: " + diff);

        FFMPEG.RunFFMPEGCommand("-y -i movies/leftleft.mp4 -i movies/left.mp4 -i movies/right.mp4 -filter_complex hstack=inputs=3 movies/hof.mp4");
    }
    void SilphBar()
    {
        Scenario startToArbok = ()=>{
            ClearText();
            MoveTo("LavenderTown", 7, 10);
            Fly("CeladonCity");
            TalkTo("CeladonPokecenter", 3, 2);
            Yes();
            ClearText(); // healed at center

            MoveTo("CeladonCity", 41, 10);
            UseItem("BICYCLE");

            MoveTo(44,10);
            Press(Joypad.Right);
            ClearText(); // repel

            MoveTo("Route7Gate", 3, 4);
            ClearText();
            MoveTo("Route7", 18, 10);
            UseItem("BICYCLE");

            PickupItemAt("SilphCo5F", 12, 3);
        };
        Scenario gioToJuggler = ()=>{
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
            ChooseMenuItem(9);
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
        Scenario koga = ()=>{
            // KOGA
            TalkTo(4, 10);
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("ELIXER", "NIDOKING"), new RbyTurn("SELFDESTRUCT"), true, false);
            ClearText(7);
            AdvanceFrames(2);
        };
        Scenario fullSilphBar = ()=>{
            startToArbok();

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

            gioToJuggler();

            // JUGGLER #2
            MoveTo(1, 7);
            ClearText();
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("POISON GAS"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));

            koga();
        };
        Scenario noSilphBar = ()=>{
            startToArbok();

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

            gioToJuggler();

            // JUGGLER #2
            MoveTo(1, 7);
            ClearText();
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("POISON GAS"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));

            koga();
        };
        Scenario confusion = ()=>{
            startToArbok();

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

            gioToJuggler();

            // JUGGLER #2
            MoveTo(1, 7);
            ClearText();
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("CONFUSION"));
            SendOut("SQUIRTLE");
            ForceTurn(new RbyTurn("REVIVE", "NIDOKING"), new RbyTurn("CONFUSION"));
            SendOut("NIDOKING");
            ForceTurn(new RbyTurn("THUNDERBOLT"));

            koga();
        };

        Comparison("basesaves/red/silphbar.gqs", ()=>{
            // fullSilphBar();
            confusion();
        }, ()=>{
            // ClearText();
            noSilphBar();
        });
    }
    void Bill()
    {
        Comparison("basesaves/red/bill.gqs", ()=>{
            ClearText();
            UseItem("POTION", "NIDOKING");
            UseItem("RARE CANDY", "NIDOKING");
            UseItem("ESCAPE ROPE");
        }, ()=>{
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
        Comparison("basesaves/red/billether.gqs", ()=>{
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
        }, ()=>{
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
        Comparison("basesaves/red/agathamenu.gqs", ()=>{
            ClearText();
            Execute("U U U");
            UseItem("SUPER POTION", "NIDOKING");
            UseItem("RARE CANDY", "NIDOKING");
            MoveTo("AgathasRoom", 4, 2);
            Press(Joypad.Right);
        }, ()=>{
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
        Comparison("basesaves/red/sabrina.gqs", ()=>{
            Execute("R");
            UseItem("BICYCLE");
            // MoveTo(178, 10, 8, Action.Left);
            TalkTo("SaffronGym", 9, 8, Action.Left);
        }, ()=>{
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
        Comparison("basesaves/red/erika.gqs", ()=>{
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
        }, ()=>{
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
        Comparison("basesaves/red/postsilphgio.gqs", ()=>{
            ClearText();
            Execute("D"); // after gio (?)

            TalkTo(236, 3, 0);
            ChooseMenuItem(9);
            MoveTo(2, 3);
            Execute("D"); // exit elevator

            MoveTo(3, 9);
        }, ()=>{
            ClearText();
            Execute("D"); // after gio (?)

            MoveTo(234, 3, 9);
        });
        // return;
        Comparison("basesaves/red/postsilphgio.gqs", ()=>{
            ClearText();
            Execute("D"); // after gio (?)

            TalkTo(236, 3, 0);
            ChooseMenuItem(9);
            MoveTo(2, 3);
            Execute("D"); // exit elevator

            MoveTo(3, 9);
            MoveTo(2, 9); // thinks trainer is still there (?)
            PickupItemAt(234, 2, 12);
            PickupItemAt(234, 4, 14);
            Dig();

            UseItem("BICYCLE");
            MoveTo("Route16", 27, 10);
        }, ()=>{
            ClearText();
            Execute("D"); // after gio (?)

            MoveTo(212, 5, 6);
            MoveTo(208, 14, 8);
            MoveTo(208, 18, 7);
            MoveTo(208, 18, 1);
            MoveTo(236, 1, 2);

            TalkTo(236, 3, 0);
            ChooseMenuItem(9);
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
        Comparison("basesaves/red/elevator.gqs", ()=>{
            // TalkTo(236, 3, 0);
            // ChooseMenuItem(9);

            MoveTo(236, 3, 1, Action.Up);
            Execute("A");
            AdvanceFrames(100, Joypad.Down); // 10f
            Press(Joypad.A);

            MoveTo(2, 3);
            Execute("D"); // exit elevator
            MoveTo(234, 7, 3);
        }, ()=>{
            MoveTo(236, 3, 1, Action.Up);
            Execute("A");
            AdvanceFrames(92, Joypad.Down); // 9f
            // AdvanceFrames(99, Joypad.Down); // 9f
            // AdvanceFrames(108, Joypad.Down); // 11f
            // AdvanceFrames(114, Joypad.Down); // 11f
            Press(Joypad.A);

            MoveTo(2, 3);
            Execute("D"); // exit elevator
            MoveTo(234, 8, 2);
            MoveTo(234, 7, 3);
        });
    }
    void CeruleanLedge()
    {
        Comparison("basesaves/red/ceruleanledge.gqs", ()=>{
            MoveTo(15, 76, 8);
            MoveTo(15, 76, 10);
            MoveTo(3, 19, 17);
        }, ()=>{
            MoveTo(15, 77, 8);
            MoveTo(15, 77, 10);
            MoveTo(3, 19, 17);
        });
    }
    void ThrashvsHA()
    {
        // LoadState("basesaves/red/oddishthrash.gqs");
        // Press(Joypad.A);
        // ClearText();
        // ForceTurn(new RbyTurn("MEGA PUNCH"));
        // ForceTurn(new RbyTurn("HORN ATTACK"));
        // byte[] state = SaveState();

        // Comparison("oddishthrash", state, ()=>{
        //     TeachLevelUpMove("WATER GUN");
        //     ForceTurn(new RbyTurn("THRASH"), null, true, false);
        // }, ()=>{
        //     TeachLevelUpMove("WATER GUN");
        //     ForceTurn(new RbyTurn("HORN ATTACK"), null, true, false);
        // });

        // LoadState("basesaves/red/4ttgthrash.gqs");
        // Press(Joypad.A);
        // ClearText();
        // ForceTurn(new RbyTurn("THRASH", ThreeTurn));
        // ForceTurn(new RbyTurn("THRASH"));
        // byte[] state = SaveState();

        // Comparison("4ttgthrash", state, ()=>{
        //     ForceTurn(new RbyTurn("THRASH"));
        //     ForceTurn(new RbyTurn("THRASH"), null, true, false);
        // }, ()=>{
        //     ForceTurn(new RbyTurn("THRASH"));
        //     ForceTurn(new RbyTurn("HORN ATTACK"), null, true, false);
        // });

        LoadState("basesaves/red/surgethrash.gqs");
        ClearText();
        ForceTurn(new RbyTurn("THRASH", ThreeTurn));
        ForceTurn(new RbyTurn("THRASH"));
        ForceTurn(new RbyTurn("THRASH"), new RbyTurn("GROWL", Miss));
        BattleSwitch("PIDGEY", new RbyTurn("THUNDERBOLT"));
        byte[] state = SaveState();

        Comparison("surgethrash", state, ()=>{
            SendOut("NIDOKING");
            ForceTurn(new RbyTurn("THRASH"), null, true, false);
        }, ()=>{
            SendOut("NIDOKING");
            ForceTurn(new RbyTurn("HORN ATTACK"), null, true, false);
        });
    }
    void Boulder()
    {
        Comparison("basesaves/red/boulder.gqs", ()=>{
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
        }, ()=>{
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
        Comparison("basesaves/red/moonxp.gqs", ()=>{
            AfterMoveAndSplit();
            MoveTo(24, 3);
            // ForceEncounter(Action.Right, 9, 0xffff);
            ForceEncounter(Action.Right, 5, 0xffff);
            ClearText();
            RunAway();
            // ForceTurn(new RbyTurn("WATER GUN"));
            MoveTo(26, 3);
            MoveAndSplit(Joypad.Right);
        }, ()=>{
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
        Comparison("basesaves/red/earlymisty.gqs", ()=>{
            // // Bill menu
            // UseItem("POTION", "NIDOKING");
            // UseItem("POTION", "NIDOKING");
            // UseItem("POTION", "NIDOKING");
            // UseItem("RARE CANDY", "NIDOKING");
            // UseItem("ESCAPE ROPE");

            // TalkTo("BikeShop", 6, 3);
            // No();
            // ClearText(); // got instant text

            // MoveTo("CeruleanGym", 4, 10);

            // // MISTY MINION
            // MoveTo(5, 3);
            // ClearText();
            // ForceTurn(new RbyTurn("THRASH"), new RbyTurn("TAIL WHIP"));
            // ForceTurn(new RbyTurn("THRASH"));

            // // MISTY
            // TalkTo(4, 2);
            // ForceTurn(new RbyTurn("THRASH"));
            // ForceTurn(new RbyTurn("THRASH"), new RbyTurn("WATER GUN"));
            // ForceTurn(new RbyTurn("THRASH"), new RbyTurn("BUBBLEBEAM"));
            // ForceTurn(new RbyTurn("THRASH"), new RbyTurn("WATER GUN", AiItem));

            // // DIG ROCKET
            // MoveTo("CeruleanCity", 30, 9);
            // ClearText();
            // ForceTurn(new RbyTurn("THRASH"));
            // ForceTurn(new RbyTurn("THRASH"));
            // MoveTo("CeruleanCity", 36, 19);


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
            ForceTurn(new RbyTurn("THRASH", Crit), new RbyTurn("BUBBLEBEAM", AiItem));
            // ForceTurnAndSplit(new RbyTurn("THRASH"));
        }, ()=>{
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
            ForceTurn(new RbyTurn("THRASH", Crit), new RbyTurn("BUBBLEBEAM", AiItem));
            // ForceTurn(new RbyTurn("THRASH"));
            // MoveTo("CeruleanCity", 36, 19);
            // ForceTurnAndSplit(new RbyTurn("THRASH"));
        });
    }
    void PostMisty()
    {
        Comparison("basesaves/red/postmisty.gqs", ()=>{
            ClearText();
            MoveTo(3, 36, 31);
            MoveTo(16, 18, 23);
            MoveTo(16, 17, 27);
        }, ()=>{
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
        Comparison("basesaves/red/blainedig.gqs", ()=>{
            ClearText(6);
            AdvanceFrames(53);
            Press(Joypad.B);
            AdvanceFrames(10);
            OpenStartMenu();
            Dig();
        }, ()=>{
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
        Comparison("basesaves/red/blainefirst.gqs", ()=>{
            PickupItem();
            Dig();

            UseItem("BICYCLE");
            CutAt(35, 32);
            MoveTo("CeladonGym", 4, 16);//simplified
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
            MoveTo("CeladonCity", 12, 28);

            Fly("CinnabarIsland");
            UseItem("BICYCLE");
            MoveTo("CinnabarGym", 16, 16);//simplified
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
            Dig();

            UseItem("BICYCLE");
            MoveTo(18, 18, 10);
            UseItem("BICYCLE");
            MoveTo("SaffronGym", 8, 16);
        }, ()=>{
            PickupItem();
            Dig();

            Fly("CinnabarIsland");
            UseItem("BICYCLE");
            MoveTo("CinnabarGym", 16, 16);//simplified
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
            Dig();

            UseItem("BICYCLE");
            CutAt(35, 32);
            MoveTo("CeladonGym", 4, 16);//simplified
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
            MoveTo("CeladonCity", 12, 28);

            Fly("SaffronCity");
            UseItem("BICYCLE");
            MoveTo("SaffronGym", 8, 16);
        });
    }
    void FlySaffron()
    {
        Comparison("basesaves/red/flysaffron.gqs", ()=>{
            ClearText();
            Dig();
            UseItem("BICYCLE");
            MoveTo(18, 18, 10);
            UseItem("BICYCLE");
            MoveTo("SaffronGym", 8, 17);
        }, ()=>{
            ClearText();
            Dig();
            Fly("SaffronCity");
            UseItem("BICYCLE");
            MoveTo("SaffronGym", 8, 17);
        });
    }
    void BikevsWalk()
    {
        // Comparison("basesaves/red/biketower.gqs", ()=>{
        //     UseItem("BICYCLE");
        //     MoveTo(14, 5);
        // }, ()=>{
        //     MoveTo(14, 5);
        // });
        // Comparison("basesaves/red/bikekoga.gqs", ()=>{
        //     UseItem("BICYCLE");
        //     MoveTo(5, 27);
        // }, ()=>{
        //     MoveTo(5, 27);
        // });
        Comparison("basesaves/red/bikeflyhouse.gqs", ()=>{
            MoveTo(27, 17, 4);
            UseItem("BICYCLE");
            MoveTo(7, 5);
        }, ()=>{
            MoveTo(27, 7, 5);
        });
    }
    void ChampAnims()
    {
        Scenario Setup = ()=>{
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("WHIRLWIND"));
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("WHIRLWIND"));
            // ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("WING ATTACK", 20));
        };
        Scenario BlizzPidgeot = ()=>{
            Setup();
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"), null, true, false);
        };
        Scenario EQAlakazam = ()=>{
            Setup();
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"), null, true, false);
        };
        Scenario BlizzRhydon = ()=>{
            Setup();
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"), null, true, false);
        };
        Scenario TBGyarados = ()=>{
            Setup();
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"), null, true, false);
        };
        Scenario DrillAll = ()=>{
            Setup();
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"), null, true, false);
        };

        Comparison("basesaves/red/champanims_noredbar.gqs", ()=>{
            // CpuWriteBE<ushort>("wPartyMon2HP", 25 );
            // CpuWrite("wPartyMon2PP", 6 );
            ClearText();
            BlizzRhydon();
            ClearTextUntil(Joypad.None, SYM["EnterMap"]);
        }, ()=>{
            // CpuWriteBE<ushort>("wPartyMon2HP", 70 );
            CpuWrite("wPartyMon2PP", 6 );
            ClearText();
            DrillAll();
            ClearTextUntil(Joypad.None, SYM["EnterMap"]);
        });
    }
    void PalletSurf()
    {
        Comparison("basesaves/red/palletsurf.gqs", ()=>{
            MoveTo(4, 8); // no troll

            MoveTo(4, 13, Action.Down);
            UseItem("SUPER REPEL");
            UseItem("HM03", "SQUIRTLE");
            Surf();
            MoveTo(32, 4, 0);
        }, ()=>{
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
        // Comparison("basesaves/red/pcpotion.gqs", ()=>{
        //     ClearText();
        //     TalkTo(0, 1);
        //     ChooseMenuItem(0);
        //         ChooseMenuItem(0);
        //         ClearText();
        //         MenuPress(Joypad.A);
        //         ClearText();
        //     MenuPress(Joypad.B);
        //     ClearText();
        //     MenuPress(Joypad.B);
        //     MoveTo(7, 1);
        // }, ()=>{
        //     ClearText();
        //     MoveTo(7, 1);
        // });
        // Comparison("basesaves/red/martguypotion.gqs", ()=>{
        //     MoveTo(8, 27);
        //     TalkTo(5, 24);
        //     MoveTo(11, 24);
        //     MoveTo(12, 23);
        // }, ()=>{
        //     MoveTo(8, 27);
        //     MoveTo(11, 24);
        //     MoveTo(12, 23);
        // });
        // Comparison("basesaves/red/treepotion.gqs", ()=>{
        //     PickupItemAt(14, 4);
        //     MoveTo(13, 7, 71);
        // }, ()=>{
        //     MoveTo(13, 7, 71);
        // });
        // Comparison("basesaves/red/extrapotion.gqs", ()=>{
        //     MoveTo(7, 24);
        //     PickupItemAt(12, 29);
        //     MoveTo(1, 21, Action.Up);
        // }, ()=>{
        //     MoveTo(7, 22);
        //     MoveTo(1, 21, Action.Up);
        // });
        Comparison("basesaves/red/weedlepotion.gqs", ()=>{
            PickupItemAt(1, 18);
            MoveTo(1, 18);
            ClearText();
        }, ()=>{
            TalkTo(2, 18);
        });
    }
    void BrunoMenu()
    {
        Comparison("basesaves/red/brunomenu.gqs", ()=>{
            ClearText();
            Execute("U U U U U");
            UseItem("MAX ETHER", "NIDOKING", "HORN DRILL");
            MoveTo(4, 2);
            Press(Joypad.Right, Joypad.A);
        }, ()=>{
            ClearText();
            Execute("U");
            UseItem("MAX ETHER", "NIDOKING", "HORN DRILL");
            Execute("U U");
            MoveTo(4, 2);
            Press(Joypad.Right, Joypad.None, Joypad.A);
            // Press(Joypad.Right, Joypad.A);
        });
    }

    public RedComparison() : base()
    {
        BrunoMenu();
        Environment.Exit(0);
    }
}
