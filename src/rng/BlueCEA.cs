class BlueCEA : RedBlueComparisons
{
    void Afo7()
    {
        void Intro()
        {
            CurrentMenuType = MenuType.None;
            TalkTo("VermilionPokecenter", 13, 4);
            Deposit("GRAVELER", "MACHOKE", "DUGTRIO", "HYPNO", "SANDSLASH");
            Withdraw("ZUBAT");
            ChooseMenuItem(3);
            ClearText();
            Yes();
            Press(Joypad.None, Joypad.Up, Joypad.A);
            MenuScroll(0, Joypad.None, true);
            ClearText();
        }

        Comparison.Compare("bluecea", "basesaves/blue/afo7.gqs", "basesaves/blue/afo7.gqs", () => {
            Intro();
            Withdraw("STARYU", "PSYDUCK", "SEEL", "KRABBY");
            Deposit("ARCANINE");
            Withdraw("SHELLDER");
            UseItem("RARE CANDY", "ZUBAT");
            RunUntil("Evolution_PartyMonLoop.done");
            UseItem("hex77");
            Press(Joypad.B);
            TalkTo("VermilionPokecenter", 13, 4);
            Deposit("GOLBAT", "STARYU", "PSYDUCK", "SEEL", "KRABBY");
            Withdraw("MAGIKARP");
            Deposit("SHELLDER");
            UseItem("RARE CANDY", "MAGIKARP");
            RunUntil("Evolution_PartyMonLoop.done");
            ChooseListItem(23);
        }, () => {
            Intro();
            Withdraw("STARYU", "PSYDUCK", "SEEL", "SHELLDER");
            UseItem("RARE CANDY", "ZUBAT");
            RunUntil("Evolution_PartyMonLoop.done");
            UseItem("hex77");
            Press(Joypad.B);
            TalkTo("VermilionPokecenter", 13, 4);
            Deposit("ARCANINE", "GOLBAT", "STARYU", "PSYDUCK", "SEEL");
            Withdraw("KRABBY", "MAGIKARP");
            UseItem("RARE CANDY", "MAGIKARP");
            RunUntil("Evolution_PartyMonLoop.done");
            UseItem("hex77");
            Press(Joypad.B);
            TalkTo("VermilionPokecenter", 13, 4);
            Deposit("SHELLDER", "KRABBY");
            OpenStartMenu();
            Press(Joypad.A);
            ChooseListItem(23);
        });
        // 56.349 - 1:00.137
    }

    void Afo6()
    {
        void Intro()
        {
            CurrentMenuType = MenuType.None;
            TalkTo("VermilionPokecenter", 13, 4);
            Deposit("GRAVELER", "MACHOKE", "DUGTRIO", "HYPNO", "SANDSLASH");
            ChooseMenuItem(3);
            ClearText();
            Yes();
            Press(Joypad.None, Joypad.Up, Joypad.A);
            MenuScroll(0, Joypad.None, true);
            ClearText();
            Withdraw("PSYDUCK", "STARYU", "SEEL", "KRABBY", "SHELLDER");
        }

        Comparison.Compare("bluecea", "basesaves/blue/afo7.gqs", "basesaves/blue/afo7.gqs", () => {
            Intro();
            Deposit("ARCANINE");
            Withdraw("MAGIKARP");
            UseItem("RARE CANDY", "PSYDUCK");
            UseItem("RARE CANDY", "PSYDUCK");
            RunUntil("Evolution_PartyMonLoop.done");
            // UseItem("hex77");
            // Press(Joypad.B);
            // UseItem("RARE CANDY", "MAGIKARP");
            // RunUntil("Evolution_PartyMonLoop.done");
            UseItem("hex77");
            Press(Joypad.B);
            TalkTo("VermilionPokecenter", 13, 4);
            Deposit("GOLDUCK", "STARYU", "SEEL", "KRABBY", "SHELLDER");
            UseItem("RARE CANDY", "MAGIKARP");
            RunUntil("Evolution_PartyMonLoop.done");
            // OpenStartMenu();
            // Press(Joypad.A);
            ChooseListItem(23);
        }, () => {
            Intro();
            UseItem("RARE CANDY", "PSYDUCK");
            UseItem("RARE CANDY", "PSYDUCK");
            RunUntil("Evolution_PartyMonLoop.done");
            UseItem("hex77");
            Press(Joypad.B);
            TalkTo("VermilionPokecenter", 13, 4);
            Deposit("ARCANINE", "GOLDUCK", "STARYU", "SEEL", "KRABBY");
            Withdraw("MAGIKARP");
            Deposit("SHELLDER");
            UseItem("RARE CANDY", "MAGIKARP");
            RunUntil("Evolution_PartyMonLoop.done");
            ChooseListItem(23);
        });
    }

    void Afo5()
    {
        Comparison.Compare("bluecea", "basesaves/blue/afo5.gqs", "basesaves/blue/afo5.gqs", () => {
            CurrentMenuType = MenuType.None;
            TalkTo("VermilionPokecenter", 13, 4);
            Deposit("GRAVELER", "MACHOKE", "DUGTRIO", "HYPNO", "SANDSLASH");
            Withdraw("PSYDUCK", "SEEL", "KRABBY", "SHELLDER", "MAGIKARP");
            UseItem("RARE CANDY", "PSYDUCK");
            // RecordAndTime("1", true);
            UseItem("RARE CANDY", "PSYDUCK");
            RunUntil("Evolution_PartyMonLoop.done");
            UseItem("hex77");
            Press(Joypad.B);
            // UseItem("hex77");
            // Press(Joypad.B);
            TalkTo("VermilionPokecenter", 13, 4);
            Deposit("STARMIE", "GOLDUCK", "SEEL", "KRABBY", "SHELLDER");
            UseItem("RARE CANDY", "MAGIKARP");
            RunUntil("Evolution_PartyMonLoop.done");
            // OpenStartMenu();
            // Press(Joypad.A);
            ChooseListItem(23);
        }, () => {
            CurrentMenuType = MenuType.None;
            TalkTo("VermilionPokecenter", 13, 4);
            Deposit("GRAVELER", "MACHOKE", "DUGTRIO", "HYPNO", "SANDSLASH");
            Withdraw("PSYDUCK", "SEEL", "KRABBY", "SHELLDER");
            UseItem("RARE CANDY", "PSYDUCK");
            // RecordAndTime("2", true);
            UseItem("RARE CANDY", "PSYDUCK");
            RunUntil("Evolution_PartyMonLoop.done");
            UseItem("hex77");
            Press(Joypad.B);
            TalkTo("VermilionPokecenter", 13, 4);
            Withdraw("MAGIKARP");
            Deposit("STARMIE", "GOLDUCK", "SEEL", "KRABBY", "SHELLDER");
            UseItem("RARE CANDY", "MAGIKARP");
            RunUntil("Evolution_PartyMonLoop.done");
            ChooseListItem(23);
        });
    }

    void Farfetchd()
    {
        void Use9F()
        {
            OpenBag();
            RunUntil("_Joypad", "HandleMenuInput_.getJoypadState");
            ListScroll(FindItem("9F"), Joypad.A, false);
            Press(Joypad.None, Joypad.Start);
            CurrentMenuType = MenuType.StartMenu;
        }
        void FlashTrainer()
        {
            OpenStartMenu();
            ChooseMenuItem(3);
            Press(Joypad.B);
        }

        Comparison.Compare("basesaves/blue/farfetchd.gqs", () => {
            ClearText();
            Use9F();
            FlashTrainer();
            ChooseMenuItem(2);
            CurrentMenuType = MenuType.Bag;
            UseItem("hex77");
            Press(Joypad.B);
            CurrentMenuType = MenuType.Bag;
            UseItem("MASTER BALL");
            MenuPress(Joypad.A);
        }, () => {
            CurrentMenuType = MenuType.None;
            ClearText();
            Use9F();
            Press(Joypad.B, Joypad.Start);
            ChooseMenuItem(2);
            CurrentMenuType = MenuType.Bag;
            UseItem("hex77");
            Press(Joypad.B);
            CurrentMenuType = MenuType.Bag;
            UseItem("MASTER BALL");
            MenuPress(Joypad.A);
        });
    }

    void Use4Flash4()
    {
        Comparison.Compare("bluecea", "basesaves/blue/afo7.gqs", "basesaves/blue/afo7.gqs", () => {
            UseItem("hex77");
            Press(Joypad.B);
            TalkTo("VermilionPokecenter", 13, 4);
        }, () => {
            CurrentMenuType = MenuType.None;
            UseItem("hex77");
            MenuPress(Joypad.A);
            CurrentMenuType = MenuType.None;
            Press(Joypad.None);
            TalkTo("VermilionPokecenter", 13, 4);
        });
    }

    void BulbaName()
    {
        bool name = true;

        RecordAndTime("bluecea");
        RbyTurn.DefaultRoll = 20;

        ClearCache();
        CacheState("newgame", () => {
            new RbyIntroSequence(RbyStrat.NoPalAB, RbyStrat.GfSkip, RbyStrat.Hop3, RbyStrat.Title0, RbyStrat.Backout, RbyStrat.Title0, RbyStrat.Options, RbyStrat.Backout, RbyStrat.Title0, RbyStrat.Options, RbyStrat.NewGame).Execute(this);
        });

        Timer.Start();

        // ClearCache();
        CacheState("rival1", () => {
            ClearText(Joypad.A);
            Press(Joypad.A, Joypad.None, Joypad.A, Joypad.Start); // Name self

            ClearText(Joypad.A);
            Press(Joypad.A, Joypad.None);
            Press(Joypad.A, Joypad.Select, Joypad.A, Joypad.Select, Joypad.Left, Joypad.Down, Joypad.A, Joypad.None, Joypad.A, Joypad.Right, Joypad.Down, Joypad.A, Joypad.Up, Joypad.None, Joypad.Up, Joypad.A, Joypad.Down, Joypad.None, Joypad.Down, Joypad.None, Joypad.Down, Joypad.A, Joypad.Start); // Name rival
            ClearText(Joypad.A); // Journey begins!

            SetOptions(Fast | Off);

            // PC potion
            TalkTo(0, 1);
            WithdrawItems("POTION", 1);

            MoveTo("PalletTown", 10, 1); // Oak cutscene
            ClearText();

            TalkTo(8, 3);
            Yes();
            ClearText();
            if(name)
            {
                Yes();
                Press(Joypad.None, Joypad.A, Joypad.Start);
            }
            else
            {
                No();
            }
            ForceGiftDVs(0xC33E);
            ClearText();

            MoveTo(5, 6);
            ClearText();

            // RIVAL1
            BattleMenu(1, 0);
            MenuPress(Joypad.A);
            ChooseMenuItem(1);
            Press(Joypad.B, Joypad.None, Joypad.B, Joypad.None, Joypad.B);
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("SCRATCH"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("SCRATCH"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("SCRATCH"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("GROWL"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("GROWL", Miss));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("GROWL"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("GROWL"));
            ClearText();
            MoveTo(5, 11);
        });

        // ClearCache();
        CacheState("bird", () => {
            Execute("D"); // pathfinding doesnt like standing there (todo)

            MoveTo("Route1", 11, 24);
            MoveNpc(15, 13, Action.Left); // npc troll
            MoveTo("Route1", 13, 14);
            MoveTo("Route1", 15, 8);
            ForceEncounter(Action.Up, 1, 0x8888);
            ClearText();
            RunAway();
            MoveTo("Route1", 15, 4);

            MoveTo("ViridianCity", 21, 30);
            MoveTo("ViridianCity", 29, 19);
            ClearText(); // Receive parcel

            MoveTo("Route1", 8, 6);
            MoveTo("Route1", 9, 14);
            MoveTo("Route1", 16, 22);
            MoveTo("Route1", 16, 24);
            MoveTo("Route1", 16, 26);
            MoveTo("Route1", 10, 26);
            MoveTo("Route1", 10, 28);

            TalkTo("OaksLab", 5, 2, Action.Right); // give parcel

            MoveTo("Route1", 11, 24);
            MoveTo("Route1", 13, 14);
            MoveTo("ViridianCity", 21, 30);

            TalkTo("ViridianMart", 1, 5);
            Buy("POKE BALL", 4, "PARLYZ HEAL", 1);

            MoveTo("Route2", 4, 56, Action.Up);
            SaveAndQuit();

            NoPal.Execute(this, true);
            Execute(SpacePath("UUAURAURRURA"));
            ForceEncounter(Action.Up, 1, 0x2060);
            ForceYoloball("POKE BALL");
            ClearText();
            No();
            RunUntil("EnterMap");
        });

        // ClearCache();
        CacheState("forest", () => {
            TalkTo("ViridianForest", 25, 11);
            MoveTo("ViridianForest", 1, 20);
            ForceEncounter(Action.Up, 0, 0x8888);
            ForceYoloball("POKE BALL");
            ClearText();
            No();
            Execute("U");
            ClearText();

            // WEEDLE GUY
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT", Miss));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("POISON STING"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("POISON STING"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT"));

            MoveTo("PewterMart", 3, 5);
            Press(Joypad.Start, Joypad.Up, Joypad.None, Joypad.Up, Joypad.None, Joypad.Up, Joypad.Start);
            TalkTo("PewterMart", 1, 5);
            Buy("BURN HEAL", 1, "ESCAPE ROPE", 1);

            MoveTo("PewterCity", 37, 18);
            ClearText();
            Press(Joypad.Start, Joypad.A);
            ClearText();
            Yes();
            // new RbyIntroSequence(RbyStrat.GfReset, RbyStrat.GfSkip, RbyStrat.Hop0, RbyStrat.Title0, RbyStrat.Continue, RbyStrat.Continue).Execute(this);
            RbyStrat.GfReset.Execute(this);
            RbyStrat.GfSkip.Execute(this);
            RbyStrat.Hop0.Execute(this);
            RbyStrat.Title0.Execute(this);
            RbyStrat.Continue.Execute(this);
            RbyStrat.Continue.Execute(this);
        });

        // ClearCache();
        CacheState("vermilion", () => {
            ClearText();
            Execute("R R R U U L L L L A");
            ClearText(4);
            while(Tile.X != 22) AdvanceFrame(Joypad.B | Joypad.Left);
            while(Map.Id != 46) AdvanceFrame(Joypad.B | Joypad.Down);
            AfterMoveAndSplit();
            MoveTo(4, 4);
            SaveAndQuit();
            PalHold.Execute(this, true);
            Execute(SpacePath("DDDDDDDDDADDDARRRADDDADRRRRDDDDADDDDRRRRRRARRRRRRDRRRARRRS_BRRRRDDDRRUU"));
            ForceEncounter(Action.Up, 8, 0x8888);
            ForceYoloball("POKE BALL");
            ClearText();
            No();

            TalkTo(89, 13, 3, Action.Up);
            Deposit("BULBASAUR", "CATERPIE", "DUGTRIO");
            MoveTo(12, 4);
        });
        Timer.Stop();
        AdvanceFrames(600);
        Dispose();
    }

    Comparison Comparison;
    public BlueCEA() : base("roms/pokeblue.gbc", true)
    {
        Comparison = new Comparison(this);
        BulbaName();
    }
}
