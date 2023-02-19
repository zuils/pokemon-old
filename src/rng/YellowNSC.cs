public class YellowNSC : YellowForceComparisons
{
    public YellowNSC()
    {
        // ulong lasttime = 0;
        // CallbackHandler.SetCallback(SYM["VBlank"], gb => {
        //     if(gb.EmulatedSamples - lasttime > 40000) System.Diagnostics.Trace.WriteLine($"{gb.CpuRead("wPlayTimeMinutes"):d2}:{gb.CpuRead("wPlayTimeSeconds"):d2}.{gb.CpuRead("wPlayTimeFrames"):d2} " + (gb.EmulatedSamples - lasttime) + " +" + (float)(gb.EmulatedSamples - lasttime - SamplesPerFrame) / SamplesPerFrame);
        //     lasttime = gb.EmulatedSamples;
        // });
        // RecordAndTime("yellow-nsc");
        RbyTurn.DefaultRoll = 20;

        ClearCache();
        CacheState("newgame", () => {
            new RbyIntroSequence(RbyStrat.NoPal, RbyStrat.GfSkip, RbyStrat.Hop0, RbyStrat.Title0).Execute(this);
            // Press(Joypad.Down, Joypad.A, Joypad.Left, Joypad.Down, Joypad.Left, Joypad.Down, Joypad.Left, Joypad.Start, Joypad.A); // Options
            Press(Joypad.Down, Joypad.A, Joypad.Left, Joypad.Down, Joypad.Left, Joypad.Start, Joypad.A); // Options
        });

        Timer.Start();

        // ClearCache();
        CacheState("rival1", () => {
            ClearText();
            Press(Joypad.A, Joypad.None, Joypad.A, Joypad.Start); // Name self

            ClearText();
            Press(Joypad.A, Joypad.None, Joypad.A, Joypad.Start); // Name rival
            ClearText(); // Journey begins!

            MoveTo("PalletTown", 10, 0); // Oak cutscene
            ClearText();

            TalkTo(7, 3);
            No();
            ForceGiftDVs(0xbaaa);
            ClearText();

            MoveTo(5, 6);
            ClearText();

            // RIVAL1
            ForceTurn(new RbyTurn("THUNDERSHOCK"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("THUNDERSHOCK", Crit), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("THUNDERSHOCK"), new RbyTurn("TAIL WHIP"));
            ForceTurn(new RbyTurn("THUNDERSHOCK", Crit));
            MoveTo(5, 10);
        });

        // ClearCache();
        CacheState("route1", () => {
            MoveTo("Route1", 11, 24);
            MoveTo("Route1", 13, 14);

            MoveTo("ViridianCity", 21, 30);
            MoveTo("ViridianCity", 29, 19);
            ClearText(); // Receive parcel

            MoveTo("Route1", 8, 6);
            MoveTo("Route1", 9, 14);
            MoveTo("Route1", 16, 22);
            MoveTo("Route1", 16, 24);
            MoveTo("Route1", 16, 26);
            MoveTo("Route1", 14, 26);
            MoveTo("Route1", 14, 28);
            MoveTo("Route1", 11, 28);
            MoveTo("Route1", 11, 34);

            TalkTo("OaksLab", 5, 2, Action.Down); // give parcel

            MoveTo("Route1", 8, 29);
            ForceEncounter(Action.Up, 6, 0x8888);
            RunAway();
            MoveTo("Route1", 11, 24);
            MoveTo("Route1", 13, 14);
            MoveTo("ViridianCity", 21, 30);
        });

        // ClearCache();
        CacheState("rat", () => {
            TalkTo("ViridianMart", 1, 5);
            Buy("POKE BALL", 2);

            MoveTo("Route1", 11, 6);
            ForceEncounter(Action.Right, 7, 0xffff);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERSHOCK"), new RbyTurn("TACKLE"));
            ForceYoloball("POKE BALL");
            ClearText();
            Yes();
            Press(Joypad.None, Joypad.A, Joypad.Start); // nickname

            TalkTo(41, 13, 3, Action.Up);
            Deposit("PIKACHU");
            TalkTo(3, 2);
            Yes();
            ClearText();

            MoveTo("ViridianCity", 19, 9);
            ClearText();
            MoveTo("Route2", 7, 52);
            SaveAndQuit();
        });
    }

    void GlitchlessForest()
    {
        // ClearCache();
        CacheState("nidoran", () => {
            TalkTo("ViridianMart", 1, 5);
            Buy("POKE BALL", 2, "POTION", 5);

            MoveTo("ViridianCity", 19, 9);
            ClearText();
            MoveTo("Route2", 7, 51, Action.Up);
            SaveAndQuit();

            NoPal.Execute(this, true);
            Execute(SpacePath("URAR"));
            ForceEncounter(Action.Up, 6, 0xf9ed);
            ForceYoloball("POKE BALL");
            ClearText();
            Yes();
            Press(Joypad.None, Joypad.A, Joypad.Start); // nido nickname
            RunUntil("EnterMap");
        });

        // ClearCache();
        CacheState("forest", () => {
            MoveTo("ViridianForest", 25, 32);
            PartySwap("PIKACHU", "NIDORANM");

            TalkTo(30, 33, Action.Down);
            ForceTurn(new RbyTurn("LEER"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT"), false);
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("TACKLE", 1), new RbyTurn("STRING SHOT", Miss));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("LEER"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("LEER"), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT"));

            TalkTo(30, 19, Action.Down);
            MoveSwap(0, 2);
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("HARDEN"));
            ForceTurn(new RbyTurn("HORN ATTACK", Crit));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("HORN ATTACK"));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("HARDEN"));
            ForceTurn(new RbyTurn("HORN ATTACK", Crit));

            MoveTo(30, 15);
            // UseItem("POTION", "NIDORANM");
            SaveAndQuit();
        });
    }
}
