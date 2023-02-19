class BlueNSC : RedBlueForceComparisons
{
    public BlueNSC() : base("roms/pokeblue.gbc", true)
    {
        // ulong lasttime = 0;
        // CallbackHandler.SetCallback(SYM["VBlank"], gb => {
        //     if(gb.EmulatedSamples - lasttime > 40000) System.Diagnostics.Trace.WriteLine($"{gb.CpuRead("wPlayTimeMinutes"):d2}:{gb.CpuRead("wPlayTimeSeconds"):d2}.{gb.CpuRead("wPlayTimeFrames"):d2} " + (gb.EmulatedSamples - lasttime) + " +" + (float)(gb.EmulatedSamples - lasttime - SamplesPerFrame) / SamplesPerFrame);
        //     lasttime = gb.EmulatedSamples;
        // });
        // RecordAndTime("blue-nsc");
        RbyTurn.DefaultRoll = 20;

        ClearCache();
        CacheState("newgame", () => {
            new RbyIntroSequence(RbyStrat.PalAB, RbyStrat.GfSkip, RbyStrat.Hop2, RbyStrat.Title0Scroll, RbyStrat.Options, RbyStrat.NewGame).Execute(this);
        });

        Timer.Start();

        // ClearCache();
        CacheState("rival1", () => {
            ClearText(Joypad.A);
            Press(Joypad.A);
            MenuPress(Joypad.Select, Joypad.Down, Joypad.Right, Joypad.Right, Joypad.Right, Joypad.A, Joypad.Up, Joypad.Up, Joypad.Up, Joypad.Up, Joypad.Left, Joypad.A, Joypad.Down, Joypad.Down, Joypad.Down, Joypad.A, Joypad.Up, Joypad.Up, Joypad.Left, Joypad.Left, Joypad.Left, Joypad.A, Joypad.Left, Joypad.Left, Joypad.A, Joypad.Left, Joypad.Left, Joypad.Left, Joypad.Up, Joypad.Up, Joypad.A, Joypad.Select, Joypad.Right, Joypad.Right, Joypad.Right, Joypad.Right, Joypad.Up, Joypad.Up, Joypad.A, Joypad.A); // Name self
            ClearText(Joypad.A);
            Press(Joypad.Down | Joypad.A); // Name rival
            ClearText(Joypad.A); // Journey begins!

            SetOptions(Fast | Off | Shift);
            MoveTo("PalletTown", 10, 1); // Oak cutscene
            ClearText();

            TalkTo(6, 3);
            Yes();
            ClearText();
            No();
            ForceGiftDVs(0x0000);
            ClearText(); // Char received

            MoveTo(5, 6);
            ClearText();

            // RIVAL1
            // ForceTurn(new RbyTurn("GROWL"), new RbyTurn("TACKLE", 39 | Crit));
            // ForceTurn(new RbyTurn("GROWL"), new RbyTurn("TACKLE", 39 | Crit));
            // ForceTurn(new RbyTurn("GROWL"), new RbyTurn("TACKLE", 39 | Crit));
            ForceTurn(new RbyTurn("SCRATCH"), new RbyTurn("TAIL WHIP"));
            ForceTurn(new RbyTurn("SCRATCH"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("SCRATCH"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("SCRATCH"), new RbyTurn("TACKLE"));
            ClearText();
            MoveTo(5, 10); // pathfinding doesnt like standing there (todo)
        });

        // ClearCache();
        CacheState("spearow", () => {
            MoveTo("Route1", 11, 24);
            MoveTo("Route1", 13, 14);
            MoveTo("Route1", 14, 9);
            ForceEncounter(Action.Up, 0, 0x0000);
            RunAway();

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
            MoveTo("Route1", 10, 28);

            TalkTo("OaksLab", 5, 2, Action.Right); // give parcel

            MoveTo("Route1", 11, 24);
            MoveTo("Route1", 13, 14);
            MoveTo("ViridianCity", 21, 30);

            TalkTo("ViridianMart", 1, 5);
            Buy("POKE BALL", 1);
            MoveTo("ViridianCity", 27, 18);

            MoveTo("ViridianCity", 6, 18, Action.Left);
            SaveAndQuit();

            new RbyIntroSequence(RbyStrat.Pal).Execute(this, true);
            Execute(SpacePath("DUULLALLLLALLLDDADLLLLA"));
            ForceEncounter(Action.Up, 7, 0xfef1);
            ForceYoloball("POKE BALL");
            ClearText();
            Yes();
            Press(Joypad.None, Joypad.A, Joypad.None, Joypad.A, Joypad.Start); // nido nickname
            RunUntil("EnterMap");
        });

        // ClearCache();
        CacheState("end", () => {
            TalkTo(41, 13, 3, Action.Up);
            Deposit("CHARMANDER");
            TalkTo(3, 2);
            Yes();
            ClearText();

            MoveTo(13, 4, 53);
            SaveAndQuit();
            PalHold.Execute(this, true);
            Execute(SpacePath("UUUURRRRUUUULLULLLU" + "URUUUUUU" + "UUUURRRRRRRRUUUUUUUUUUUUUUUUUAUUUUUUUUUUURUUUUUULLLLLLALLLDDDDDDDLLLLUUUUUUUUUUUUULLLLLLDDDDDDDDDDDDDDDDLDDDLLLLUUUL"));
            ForceEncounter(Action.Up, 9, 0xffff);
            ClearText();
            ForceTurn(new RbyTurn("GROWL"), new RbyTurn("THUNDERSHOCK", Crit), false, false);
        });
    }
}
