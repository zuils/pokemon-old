using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class RedTasTest : RedGlitchless {
    void AgathaBug()
    {
        LoadState("basesaves/red/agathabug.gqs");
        ForceTurn(new RbyTurn("EARTHQUAKE"));

        // return;

        string GetLine()
        {
            int pc = PC;
            if(pc > 0x4000) pc |= CpuRead("hLoadedROMBank") << 16;
            string pcs = SYM.Contains(pc) ? SYM[pc] : String.Format("{0:x4}",pc);

            int sp=CpuReadLE<ushort>(SP);
            if(sp > 0x4000) sp |= CpuRead("hLoadedROMBank") << 16;
            string sps = String.Format("{0:x4}",sp) + (SYM.Contains(sp) ? " ("+SYM[sp]+")" : "");

            return "instruction: " + pcs + "  stack: " + sps + "  ly:"+(int)CpuRead(0xff44);
        }


        LoadState("basesaves/red/agathabug_test.gqs");

        for(int k=0; k<30; ++k) {
            const int maxhist=100;
            string[] hist=new string[maxhist];
            int j=0;
            string ins=GetLine();
            while(!ins.Contains("NULL+0040")) {
                RunFor(1);
                ins=GetLine();
                hist[j]=ins;
                j=(j+1)%maxhist;
            }

            for(int i=j; i<maxhist; ++i )
                Console.WriteLine(hist[i]);
            for(int i=0; i<j; ++i )
                Console.WriteLine(hist[i]);

            // RunUntil(SYM["RandomizeDamage.loop"] + 0x0);

            for(int i=0; i<100; ++i) {
                RunFor(1);
                Console.WriteLine(GetLine());
            }
            Console.WriteLine();
        }
    }
    void Bide()
    {
        Record("bide");
        LoadState("basesaves/red/bide.gqs");

        ForceTurn(new RbyTurn("BUBBLE"), new RbyTurn("BIDE", 3*Turns));
        ForceTurn(new RbyTurn("BUBBLE"), new RbyTurn("BIDE"));
        ForceTurn(new RbyTurn("BUBBLE"), new RbyTurn("BIDE"));
        // ForceTurn(new RbyTurn("BUBBLE"));

        AdvanceFrames(60);
        Dispose();
    }
    void Wrap()
    {
        Record("wrap");

        LoadState("basesaves/red/wrap.gqs");

        ForceTurn(new RbyTurn("LEER", Miss), new RbyTurn("WRAP", 20 | ThreeTurn));
        ForceTurn(new RbyTurn(""), new RbyTurn(""));
        ForceTurn(new RbyTurn(""), new RbyTurn(""));
        // ForceTurn(new RbyTurn(""), new RbyTurn(""));
        // ForceTurn(new RbyTurn(""), new RbyTurn(""));
        ForceTurn(new RbyTurn("LEER", Miss), new RbyTurn("LEER", Miss));

        AdvanceFrames(60);
        Dispose();
    }
    void TestRange()
    {
        Record("testrange");

        // LoadState("basesaves/red/psrange2.gqs");
        // ForceTurn(new RbyTurn("MEGA PUNCH", Miss), new RbyTurn("POISON STING", 1));
        // LoadState("basesaves/red/psrange2.gqs");
        // ForceTurn(new RbyTurn("MEGA PUNCH", Miss), new RbyTurn("POISON STING", 39));
        // LoadState("basesaves/red/psrange2.gqs");
        // ForceTurn(new RbyTurn("MEGA PUNCH", Miss), new RbyTurn("POISON STING", 1 | Crit));
        // LoadState("basesaves/red/psrange2.gqs");
        // ForceTurn(new RbyTurn("MEGA PUNCH", Miss), new RbyTurn("POISON STING", 39 | Crit));

        // LoadState("basesaves/red/vileplume.gqs");
        // ForceTurn(new RbyTurn("EARTHQUAKE", Miss), new RbyTurn("PETAL DANCE", 1));
        // LoadState("basesaves/red/vileplume.gqs");
        // ForceTurn(new RbyTurn("EARTHQUAKE", Miss), new RbyTurn("PETAL DANCE", 39));
        // LoadState("basesaves/red/vileplume.gqs");
        // ForceTurn(new RbyTurn("EARTHQUAKE", Miss), new RbyTurn("PETAL DANCE", 1 | Crit));
        // LoadState("basesaves/red/vileplume.gqs");
        // ForceTurn(new RbyTurn("EARTHQUAKE", Miss), new RbyTurn("PETAL DANCE", 39 | Crit));

        // LoadState("basesaves/red/kadabar.gqs");
        // ForceTurn(new RbyTurn("POISON STING"), new RbyTurn("CONFUSION", 1));
        // LoadState("basesaves/red/kadabar.gqs");
        // ForceTurn(new RbyTurn("POISON STING"), new RbyTurn("CONFUSION", 39));
        // LoadState("basesaves/red/kadabar.gqs");
        // ForceTurn(new RbyTurn("POISON STING"), new RbyTurn("CONFUSION", 1 | Crit));
        // LoadState("basesaves/red/kadabar.gqs");
        // ForceTurn(new RbyTurn("POISON STING"), new RbyTurn("CONFUSION", 39 | Crit));


        // LoadState("basesaves/red/champrange.gqs");
        // ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("SKY ATTACK"));
        // ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("SKY ATTACK", 1));
        // LoadState("basesaves/red/champrange.gqs");
        // ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("SKY ATTACK"));
        // ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("SKY ATTACK", 39));
        // LoadState("basesaves/red/champrange.gqs");
        // ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("SKY ATTACK"));
        // ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("SKY ATTACK", 1 | Crit));
        // LoadState("basesaves/red/champrange.gqs");
        // ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("SKY ATTACK"));
        // ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("SKY ATTACK", 39 | Crit));

        // ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP", 1));
        // ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("HYDRO PUMP", 39));
        // ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP", 1 | Crit));
        // ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("HYDRO PUMP", 39 | Crit));

        // ForceTurn(new RbyTurn("LEER"), new RbyTurn("QUICK ATTACK", 39 | Crit));

        // ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("HAZE"));
        // ForceTurn(new RbyTurn("THUNDERBOLT"), new RbyTurn("CONFUSE RAY"));
        // ForceTurn(new RbyTurn("THUNDERBOLT", Hitself), new RbyTurn("CONFUSE RAY"));
        // ForceTurn(new RbyTurn("THUNDERBOLT", Hitself), new RbyTurn("CONFUSE RAY"));
        // ForceTurn(new RbyTurn("THUNDERBOLT", Hitself), new RbyTurn("CONFUSE RAY"));
        // ForceTurn(new RbyTurn("THUNDERBOLT", Crit));
        // ForceTurn(new RbyTurn("EARTHQUAKE"));
        // ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("BITE", 1));
        // ForceTurn(new RbyTurn("EARTHQUAKE", Miss), new RbyTurn("BITE", 39));
        // ForceTurn(new RbyTurn("EARTHQUAKE", Miss), new RbyTurn("BITE", 1 | Crit));
        // ForceTurn(new RbyTurn("EARTHQUAKE", Miss), new RbyTurn("BITE", 39 | Crit));
        AdvanceFrames(300);

        AdvanceFrames(60);
        Dispose();
    }
    void MistyAI()
    {
        Record("mistyai");

        LoadState("basesaves/red/misty2.gqs");

        ForceTurn(new RbyTurn("THRASH",1), new RbyTurn("BUBBLEBEAM", AiItem));
        ForceTurn(new RbyTurn("THRASH",1), new RbyTurn("BUBBLEBEAM"));
        ForceTurn(new RbyTurn("THRASH",1), new RbyTurn("BUBBLEBEAM"));

        AdvanceFrames(60);
        Dispose();
    }
    void Disable()
    {
        Record("disable");
        LoadState("basesaves/red/disable.gqs");

        ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("DISABLE", 1*Turns, "EARTHQUAKE"));
        ForceTurn(new RbyTurn("THUNDERBOLT", Miss), new RbyTurn("POISON GAS"));
        ForceTurn(new RbyTurn("EARTHQUAKE"));

        // RbyTurn.defaultRoll=20;
        // ForceTurn(new RbyTurn("THUNDERBOLT"), new RbyTurn("DISABLE", 8*Turns, "EARTHQUAKE"));
        // ForceTurn(new RbyTurn("THUNDERBOLT"), new RbyTurn("HEADBUTT", Crit));
        // ForceTurn(new RbyTurn("THUNDERBOLT"), new RbyTurn("CONFUSION"));

        AdvanceFrames(60);
        Dispose();
    }
    void Potion()
    {
        Record("potion");
        LoadState("basesaves/red/potion.gqs");

        ForceTurn(new RbyTurn("POTION", "SQUIRTLE"), new RbyTurn("STRING SHOT"));
        ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT", Miss));
        ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT", Miss));

        AdvanceFrames(60);
        Dispose();
    }
    void FuryAttack()
    {
        Record("furyattack");
        LoadState("basesaves/red/furyattack.gqs");

        ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("FURY ATTACK", 30 | Crit | 4*Turns));

        AdvanceFrames(60);
        Dispose();
    }
    void MoveRng()
    {
        // Record("moverng");

        // int i=0;
        // for(int i=0; i<256; ++i) {
            // LoadState("basesaves/red/agatha.gqs");
            // LoadState("basesaves/red/blackbelt.gqs");
            // LoadState("basesaves/red/champ.gqs");
            // LoadState("basesaves/red/lorelei.gqs");
            // AdvanceFrames(7);



            // Hold(Joypad.B, SYM["Random"]);
            // int addr = CpuReadLE<ushort>(SP);
            // if(addr > 0x4000) addr |= CpuRead("hLoadedROMBank") << 16;
            // string address = SYM[addr];
            // Console.WriteLine(address);

            // Hold(Joypad.B, SYM["TrainerAI.getpointer+0006"]);
            // A=i;
            // if(ClearText(Joypad.B, 1, SYM["ExecuteEnemyMove"], SYM["MainInBattleLoop.AIActionUsedPlayerFirst"])==SYM["ExecuteEnemyMove"])
            //     Console.WriteLine(i + " skip");
            // else
            //     Console.WriteLine(i + " AI");

            // 63 64 63 66
            // Hold(Joypad.B, SYM["SelectEnemyMove.chooseRandomMove+0004"]);
            // A=i;
            // int r;
            // while((r=Hold(Joypad.B, SYM["SelectEnemyMove.chooseRandomMove+0004"], SYM["SelectEnemyMove.moveChosen"], SYM["SelectEnemyMove.done"]))!=SYM["SelectEnemyMove.done"])
            // {
            //     if(r==SYM["SelectEnemyMove.chooseRandomMove+0004"]) Console.WriteLine("chooseRandomMove "+A);
            //     if(r==SYM["SelectEnemyMove.moveChosen"]) Console.WriteLine("moveChosen "+B);
            //     RunFor(1);
            // }
            // Console.WriteLine("done " + Moves[A].Name + "\n");

            // Hold(Joypad.B, SYM["SelectEnemyMove.chooseRandomMove+0004"]);
            // A=i;
            // Hold(Joypad.B, SYM["SelectEnemyMove.moveChosen"]);
            // Console.WriteLine(B);
            // Hold(Joypad.B, SYM["SelectEnemyMove.done"]);
            // Console.WriteLine(Moves[A].Name);
        // }

        // var score=new Dictionary<string,int>();
        // LoadState("basesaves/red/lorelei.gqs");
        // byte[] state=SaveState();
        // const int it=1000000;
        // Console.WriteLine("iterations: "+it);
        // for(int i=0; i<it; ++i) {
        //     Hold(Joypad.B, SYM["SelectEnemyMove.done"]);
        //     score[Moves[A].Name] = score.GetValueOrDefault(Moves[A].Name) + 1;
        //     LoadState(state);
        //     AdvanceFrame();
        //     state=SaveState();
        //     if(i%1000==999) Console.WriteLine(".");
        // }
        // foreach(var e in score)
        //     Console.WriteLine(e.Key + ": " + e.Value + " (" + 100.0*e.Value/it + "%)");

        // iterations: 1000000
        // AURORA BEAM: 532773 (53,2773%)
        // REST: 467227 (46,7227%)


        AdvanceFrames(60);
        Dispose();
    }
    void Agatha()
    {
        Record("agatha");

        LoadState("basesaves/red/agatha.gqs");

        ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("DREAM EATER", Switch));
        ForceTurn(new RbyTurn("BLIZZARD", Miss), new RbyTurn("CONFUSE RAY", AiItem));
        ForceTurn(new RbyTurn("EARTHQUAKE"));

        AdvanceFrames(60);
        Dispose();
    }
    void GenSquirtle()
    {
        ushort dvs=0x8777;
        // Record("test");

        new RbyIntroSequence(RbyStrat.NoPal, RbyStrat.GfSkip, RbyStrat.Hop0, RbyStrat.Title0).Execute(this);
        Press(Joypad.Down, Joypad.A, Joypad.Left, Joypad.Down, Joypad.Left, Joypad.B, Joypad.A); // Options

        ClearText(Joypad.A);
        Press(Joypad.A, Joypad.None, Joypad.A, Joypad.Start); // Name self

        ClearText(Joypad.A);
        Press(Joypad.A, Joypad.None, Joypad.A, Joypad.Start); // Name rival
        ClearText(); // Journey begins!

        // PC potion
        TalkTo(0, 1);
        ChooseMenuItem(0);
            ChooseMenuItem(0);
            ClearText();
            MenuPress(Joypad.A);
            ClearText();
        MenuPress(Joypad.B);
        ClearText();
        MenuPress(Joypad.B);

        MoveTo("PalletTown", 10, 1); // Oak cutscene
        ClearText();

        TalkTo(7, 3);
        Yes();
        ClearText();
        Yes();
        Press(Joypad.None, Joypad.A, Joypad.Start); // Name Squirtle
        ForceGiftDVs(dvs);
        ClearText(); // Squirtle received

        MoveTo(5, 6);
        ClearText();

        // RIVAL1
        ForceTurn(new RbyTurn("TAIL WHIP"), new RbyTurn("TACKLE"));
        ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("GROWL", Miss));
        ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("GROWL", Miss));
        ForceTurn(new RbyTurn("TACKLE",1), new RbyTurn("GROWL", Miss));
        ForceTurn(new RbyTurn("TACKLE",1), new RbyTurn("GROWL", Miss));
        ClearText();

        MoveTo("Route1",14,14);
        ForceEncounter(Action.Up, 1, 0x8888);
        ClearText();
        ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("TACKLE"));
        ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("TACKLE"));
        ForceTurn(new RbyTurn("TACKLE"));

        MoveTo("ViridianCity", 29, 19);
        ClearText(); // Receive parcel

        TalkTo("OaksLab", 5, 2, Action.Right); // give parcel

        TalkTo("ViridianMart", 1, 5);
        Buy("POKE BALL", 4);
        MoveTo("ViridianCity", 27, 18);

        MoveTo("ViridianCity", 7, 18, Action.Left);
        Save();
        SaveState($"basesaves/red/manip/nido{dvs:X4}.gqs");
    }

    public RedTasTest() : base() {
        GenSquirtle();
        Environment.Exit(0);
    }
}
