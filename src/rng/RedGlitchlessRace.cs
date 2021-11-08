public partial class RedGlitchless {
    public void Race(int T=1, int R=1, int S=1, int N=1, bool IT=false) {
        System.Console.Write(T+" "+R+" "+S+" "+N+" ");

        RecordAndTime("red-glitchless-race");
        RbyTurn.DefaultRoll = 20;

        // ClearCache();
        CacheState("newgame", () => {
            new RbyIntroSequence(RbyStrat.NoPal, RbyStrat.GfSkip, RbyStrat.Hop0, RbyStrat.Title0).Execute(this);
            Press(Joypad.Down, Joypad.A, Joypad.Left, Joypad.Down, Joypad.Left, Joypad.B, Joypad.A); // Options
        });

        Timer.Start();

        ClearCache();
        CacheState("rival1", () => {
            ClearText(Joypad.A);
            // Press(Joypad.A, Joypad.None, Joypad.A, Joypad.Start); // Name self
            Press(Joypad.A); for(int i=0; i<T; ++i) Press(Joypad.None, Joypad.A); Press(Joypad.Start); // 2-10 char name

            ClearText(Joypad.A);
            // Press(Joypad.A, Joypad.None, Joypad.A, Joypad.Start); // Name rival
            Press(Joypad.A); for(int i=0; i<R; ++i) Press(Joypad.None, Joypad.A); Press(Joypad.Start); // 2-10 char name
            ClearText(Joypad.A); // Journey begins!

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
                // Yes();
                // Press(Joypad.None, Joypad.A, Joypad.Start); // Name Squirtle
                if(S>=0) Yes(); else No(); // skip naming
                if(S==0) Press(Joypad.None); // miss name
                for(int i=0; i<S; ++i) Press(Joypad.None, Joypad.A); if(S>=0) Press(Joypad.Start); // 2-10 char name
            ForceGiftDVs(0xC0a4);
            ClearText(); // Squirtle received

            MoveTo(5, 6);
            ClearText();

            // RIVAL1
            ForceTurn(new RbyTurn("TAIL WHIP"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("GROWL"), false);
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("GROWL"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("GROWL"));
            ForceTurn(new RbyTurn("TACKLE", Crit), new RbyTurn("GROWL"), false);
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("TACKLE"), false);
            ForceTurn(new RbyTurn("TACKLE"));
            ClearText();
            MoveTo(5, 11);
        });

        // ClearCache();
        CacheState("nidoran", () => {
            Execute("D"); // pathfinding doesnt like standing there (todo)

            MoveTo("Route1",8,30);
            ForceEncounter(Action.Up, 8, 0x8888);
            ClearText();
            RunAway();

            MoveTo("Route1",11,24);
            MoveTo("Route1",13,14);

            MoveTo("Route1",14,14);
            ForceEncounter(Action.Up, 1, 0x8888);
            ClearText();
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("TACKLE"));

            MoveTo("ViridianCity", 21, 30);
            MoveTo("ViridianCity", 29, 19);
            ClearText(); // Receive parcel

            MoveTo("Route1",8,6);
            MoveTo("Route1",9,14);
            MoveTo("Route1",16,22);
            MoveTo("Route1",16,24);
            MoveTo("Route1",16,26);
            MoveTo("Route1",10,26);
            MoveTo("Route1",10,28);

            TalkTo("OaksLab", 5, 2, Action.Right); // give parcel

            MoveTo("Route1",11,24);
            MoveTo("Route1",13,14);
            MoveTo("Route1",15,12); // todo simulate npc troll
            MoveTo("ViridianCity", 21, 30);

            TalkTo("ViridianMart", 1, 5);
            Buy("POKE BALL", 4);
            MoveTo("ViridianCity", 27, 18);

            MoveTo("ViridianCity", 7, 18, Action.Left);

            Save();
            AdvanceFrames(29); // saving
            AdvanceFrames(105); // fade out (todo?)
            HardReset();

            NoPal.Execute(this);
            Execute(SpacePath("LLLULLUAULALDLDLLDADDADLALLALUUA"));
            ForceEncounter(Action.Up, 3, 0xffef);
            ForceYoloball("POKE BALL");
            ClearText();
                // Yes();
                // Press(Joypad.None, Joypad.A, Joypad.Start); // nido nickname
                if(N>=0) Yes(); else No(); // skip naming
                if(N==0) Press(Joypad.None); // miss name
                for(int i=0; i<N; ++i) Press(Joypad.None, Joypad.A); if(N>=0) Press(Joypad.Start); // 2-10 char name
            RunUntil("EnterMap");
        });

        // ClearCache();
        CacheState("forest", () => {
            MoveTo(13, 10, 52); // no extended
            MoveTo(10, 46);
            
            MoveTo("ViridianForest", 26, 42); // safe path
            MoveTo(26, 34);
            MoveTo(27, 32);
            MoveTo(27, 20);

            PickupItemAt("ViridianForest", 25, 11);
            MoveTo(17, 16);
            MoveTo(13, 3);
            MoveTo(7, 22);

            // WEEDLE GUY
            TalkTo(2, 18);
            ForceTurn(new RbyTurn("TAIL WHIP"), new RbyTurn("POISON STING"), false);
            ForceTurn(new RbyTurn("TAIL WHIP"), new RbyTurn("POISON STING", SideEffect), false);
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT"), false);
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("POISON STING"));
            ForceTurn(new RbyTurn("POTION", "SQUIRTLE"), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT", Miss));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT", Miss));
        });

        // ClearCache();
        CacheState("brock", () => {
            MoveTo(1, 18);
            PartySwap("SQUIRTLE", "NIDORANM");
            UseItem("ANTIDOTE", "SQUIRTLE");

            // Backup bird
            MoveTo(1, 14);
            Save();
            AdvanceFrames(29); // saving
            AdvanceFrames(105); // fade out (todo?)
            HardReset();

            PalHold.Execute(this);
            Execute(SpacePath("UUUUUUUUUUUUUUU"+"URUAUUAUUU"+"UUAUU"));
            Press(Joypad.Right);
            ForceEncounter(Action.Right, 1, 0x8888); // todo doesnt work with turnframes
            ForceYoloball("POKE BALL");
            ClearText();
            No();

            MoveTo(5, 8);

            TalkTo("PewterMart", 1, 5);
            Buy("POTION", 8);

            // BROCK
            TalkTo("PewterGym", 4, 1);
            BattleSwitch("SQUIRTLE", new RbyTurn("DEFENSE CURL"));
            ForceTurn(new RbyTurn("BUBBLE", SideEffect), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("BUBBLE"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("POTION", "SQUIRTLE"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("BUBBLE"));
            Yes();
            SendOut("NIDORANM");
            BattleSwitch("SQUIRTLE", new RbyTurn("SCREECH", Miss));
            ForceTurn(new RbyTurn("BUBBLE"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("BUBBLE", SideEffect), new RbyTurn("TACKLE", Miss));
            ForceTurnAndSplit(new RbyTurn("BUBBLE"), new RbyTurn("BIDE"));
        });

        // ClearCache();
        CacheState("route3", () => {
            ClearText();
            MoveTo(4, 10);
            SetOptions(Fast | Off | Set);

            // ROUTE 3 TRAINER 1
            MoveTo("Route3", 11, 6);
            ClearText();
            ForceTurn(new RbyTurn("LEER"), new RbyTurn("TACKLE"), false);
            ForceTurn(new RbyTurn("HORN ATTACK", Crit), new RbyTurn("TACKLE", Miss));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("STRING SHOT", Miss));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT"), false);

            ForceTurn(new RbyTurn("LEER"), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("HORN ATTACK", Crit), new RbyTurn("POISON STING"));
            ForceTurn(new RbyTurn("TACKLE", 1), new RbyTurn("POISON STING"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT"));

            ForceTurn(new RbyTurn("LEER"), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("TACKLE"), new RbyTurn("STRING SHOT"));

            // ROUTE 3 TRAINER 2
            TalkTo(14, 4);
            ForceTurn(new RbyTurn("LEER"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("POTION", "NIDORANM"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("QUICK ATTACK"));

            ForceTurn(new RbyTurn("LEER"), new RbyTurn("POISON STING"));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("LEER", Miss));
            ForceTurn(new RbyTurn("HORN ATTACK"));

            // ROUTE 3 TRAINER 3
            TalkTo(19, 5);
            ForceTurn(new RbyTurn("HORN ATTACK", 10 | Crit), new RbyTurn("POISON STING"));
            ForceTurn(new RbyTurn("TACKLE"));

            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("HARDEN"));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("HARDEN"));
            ForceTurn(new RbyTurn("HORN ATTACK"));

            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("HORN ATTACK"));
            
            ForceTurn(new RbyTurn("HORN ATTACK", Crit), new RbyTurn("HARDEN"));
            ForceTurn(new RbyTurn("HORN ATTACK"));

            // ROUTE 3 TRAINER 4
            TalkTo(24, 6);
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("STRING SHOT", Miss));
            ForceTurn(new RbyTurn("HORN ATTACK"));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("HARDEN"));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("HARDEN"));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("HARDEN"));
            ForceTurn(new RbyTurn("TACKLE"));

            MoveTo(27, 11);
            Save();
            AdvanceFrames(32); // saving
            AdvanceFrames(105); // fade out (todo?)
            HardReset();

            PalHold.Execute(this);
            Execute(SpacePath("RRRRRRRRURRUUUUUARRRRRRRRRRRRDDDDDRRRRRRRARUURRUUUUUUUUUURRRRUUUUUUUUUURRRRR"));
            MoveAndSplit(Joypad.Up);
        });

        // ClearCache();
        CacheState("mtmoon", () => {
            AfterMoveAndSplit();
            Execute(SpacePath("UUUUUULLLLLALLLLDD"));
            PickupItem();
            Execute(SpacePath("RRRRUURRRARRUUUUUUURRRRRRRAUUUUUUURRRDRDDDDDDDADDDDDDDDADRRRRRURRRR"));
            PickupItem();
            Execute(SpacePath("UUUUUUUUR"));
            PickupItem();
            Execute(SpacePath("ULUUUUUAUUUUUULLLUUUUUUUULLLLLLDDLALLLLLLLDDDDDD"));
            Execute(SpacePath("LALLALLALLALDD"));
            Execute(SpacePath("RRRUUULAUR"));
            PickupItem();
            Execute(SpacePath("DDADLALLAD"));
            Execute(SpacePath("RARRARRARRARUU"));
            Execute(SpacePath("DDLDDDDLLLLLLLULUUUUULUUUUUUUULLLUL"));
            PickupItem();
            Execute(SpacePath("DADDRAR"));
            Execute(SpacePath("DRRDDDDDDDDDDRRRARRRRRRRRRRDR"));
            Execute(SpacePath("RRUUURARRRDDRRRRRUARURARRDDDDDDDDALLLLDDDDDDDADDLLLALLLLLLLLLLLLALLLLLLUUUUAUUALUUUUUUU"));

            ForceEncounter(Action.Up, 5, 0x8888); // paras
            ClearText();
            ForceYoloball("POKE BALL");
            ClearText();
            No();
            ClearText();

            TossItem("POKE BALL");
            UseItem("POTION", "NIDORANM");
            UseItem("POTION", "NIDORANM");
            Save();

            // MOON ROCKET
            MoveTo("MtMoonB2F", 11, 17);
            ClearText();
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("QUICK ATTACK"));
            ForceTurn(new RbyTurn("POISON STING"), new RbyTurn("QUICK ATTACK"));
            ForceTurn(new RbyTurn("LEER"), new RbyTurn("LEECH LIFE"));
            ForceTurn(new RbyTurn("POTION", "NIDORANM"), new RbyTurn("LEECH LIFE"));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("SUPERSONIC", Miss));
            ForceTurn(new RbyTurn("HORN ATTACK", Crit));

            // Moon menu
            MoveTo(12, 16);
            UseItem("RARE CANDY", "NIDORANM");
            RunUntil("Evolution_PartyMonLoop.done");
            UseItem("TM12", "NIDORINO", "TACKLE");
            UseItem("MOON STONE", "NIDORINO");
            UseItem("TM01", "NIDOKING", "LEER");

            // SUPER NERD
            TalkTo(12, 8);
            ForceTurn(new RbyTurn("MEGA PUNCH"), new RbyTurn("POUND"));
            ForceTurn(new RbyTurn("WATER GUN"));
            ForceTurn(new RbyTurn("MEGA PUNCH"), new RbyTurn("SCREECH"));
            ForceTurn(new RbyTurn("POISON STING"));
            ForceTurn(new RbyTurn("MEGA PUNCH", Miss), new RbyTurn("SMOG", Miss));
            ForceTurn(new RbyTurn("MEGA PUNCH", 30), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("WATER GUN"));

            TalkTo(13, 6);
            Yes();
            ClearText(); // helix fossil picked up

            MoveTo(3,7);
            ForceEncounter(Action.Right, 1, 0x0000);
            ClearText();
            ForceTurn(new RbyTurn("WATER GUN"));

            MoveTo("MtMoonB1F", 26, 3);
            MoveAndSplit(Joypad.Right);
        });

        // ClearCache();
        CacheState("bridge", () => {
            AfterMoveAndSplit();
            TalkTo("CeruleanPokecenter", 3, 2);
            Yes();
            ClearText(); // healed at center

            if(IT) {
                MoveTo("BikeShop", 2, 6);
                TalkTo(6, 3);
                No();
                ClearText(); // got instant text
            }

            PickupItemAt("CeruleanCity", 15, 8);

            MoveTo("CeruleanCity", 21, 6, Action.Up);

            // RIVAL 2
            ClearText();
            ForceTurn(new RbyTurn("HORN ATTACK", Crit), new RbyTurn("SAND-ATTACK", Miss));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("GUST"));
            ForceTurn(new RbyTurn("HORN ATTACK", Crit), new RbyTurn("QUICK ATTACK"));
            ForceTurn(new RbyTurn("HORN ATTACK"));
            ForceTurn(new RbyTurn("MEGA PUNCH", Miss), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("MEGA PUNCH"));
            ForceTurn(new RbyTurn("MEGA PUNCH"), new RbyTurn("LEECH SEED"));
            ForceTurn(new RbyTurn("HORN ATTACK", Crit));

            // NUGGET BRIDGE #1
            TalkTo("Route24", 11, 31);
            ForceTurn(new RbyTurn("MEGA PUNCH", Miss), new RbyTurn("STRING SHOT"));
            ForceTurn(new RbyTurn("MEGA PUNCH"));
            ForceTurn(new RbyTurn("MEGA PUNCH"));

            // NUGGET BRIDGE #2
            TalkTo(10, 28);
            ForceTurn(new RbyTurn("MEGA PUNCH"), new RbyTurn("QUICK ATTACK"));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("HORN ATTACK"));

            // NUGGET BRIDGE #3
            TalkTo(11, 25);
            ForceTurn(new RbyTurn("MEGA PUNCH"));
            ForceTurn(new RbyTurn("MEGA PUNCH", 1), new RbyTurn("WRAP", 3 * Turns));
            ForceTurn(new RbyTurn(""), new RbyTurn("WRAP"));
            ForceTurn(new RbyTurn(""), new RbyTurn("WRAP"));
            ForceTurn(new RbyTurn("POISON STING"));
            ForceTurn(new RbyTurn("HORN ATTACK"));

            // NUGGET BRIDGE #4
            TalkTo(10, 22);
            ForceTurn(new RbyTurn("MEGA PUNCH"));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("TACKLE", 39));
            ForceTurn(new RbyTurn("HORN ATTACK", Crit));

            // NUGGET BRIDGE #5
            TalkTo(11, 19);
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("SCRATCH", 39 | Crit));
            ForceTurn(new RbyTurn("POISON STING"));

            // NUGGET BRIDGE #5
            MoveTo(10, 15);
            ClearText();
            ForceTurn(new RbyTurn("MEGA PUNCH"));
            ForceTurnAndSplit(new RbyTurn("HORN ATTACK"));
        });

        // ClearCache();
        CacheState("bill", () => {
            ClearText();

            // HIKER
            MoveTo("Route25", 14, 7);
            ClearText();
            ForceTurn(new RbyTurn("WATER GUN"));

            // LASS
            TalkTo(18, 8, Action.Down);
            ForceTurn(new RbyTurn("MEGA PUNCH"));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("GROWL"));
            ForceTurn(new RbyTurn("HORN ATTACK"));

            // HIKER
            TalkTo(23, 9);
            ForceTurn(new RbyTurn("WATER GUN"));
            ForceTurn(new RbyTurn("WATER GUN", Crit));
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("KARATE CHOP"));
            ForceTurn(new RbyTurn("POISON STING"));
            ForceTurn(new RbyTurn("WATER GUN", Crit));

            // ODDISH LASS
            TalkTo(37, 4);
            ForceTurn(new RbyTurn("MEGA PUNCH", Crit));
            ForceTurn(new RbyTurn("HORN ATTACK"));
            TeachLevelUpMove("WATER GUN");
            ForceTurn(new RbyTurn("THRASH"));

            TalkTo("BillsHouse", 6, 5, Action.Right);
            Yes();
            ClearText();
            TalkTo(1, 4);
            TalkTo(4, 4);

            // Bill menu
            UseItem("RARE CANDY", "NIDOKING");
            UseItem("ESCAPE ROPE");
        });

        // ClearCache();
        CacheState("misty", () => {
            TalkTo("CeruleanPokecenter", 3, 2);
            Yes();
            ClearText(); // healed at center
            
            if(IT) {
                Save(); // time penalty
                TalkTo("BikeShop", 6, 3);
                No();
                ClearText(); // got instant text
            }

            // DIG ROCKET
            MoveTo("CeruleanCity", 30, 9);
            ClearText();
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("KARATE CHOP", Crit));
            ForceTurn(new RbyTurn("THRASH", Crit));
            ForceTurn(new RbyTurn("THRASH"));

            MoveTo("CeruleanGym", 4, 10);

            // MISTY MINION
            MoveTo(5, 3);
            ClearText();
            ForceTurn(new RbyTurn("THRASH", Crit));

            // MISTY
            MoveTo(5, 2);
            if(!IT) Save();
            TalkTo(4, 2);
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("WATER GUN", 39));
            ForceTurnAndSplit(new RbyTurn("THRASH"));
        });

        // ClearCache();
        CacheState("boat", () => {
            ClearText();
            MoveTo("Route6", 17, 25);
            MoveTo(15, 28);

            // ROUTE 6 #1
            TalkTo(11, 30, Action.Down);
            ForceTurn(new RbyTurn("THRASH", Crit), new RbyTurn("QUICK ATTACK", Crit));
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("QUICK ATTACK"));
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("QUICK ATTACK", Crit));

            // ROUTE 6 #2
            MoveTo(10, 31);
            ClearText();
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("QUICK ATTACK"));

            MoveTo("VermilionCity", 18, 30);
            ClearText();

            // RIVAL 3
            MoveTo("SSAnne2F", 37, 8, Action.Up);
            ClearText();
            ForceTurn(new RbyTurn("HORN ATTACK"), new RbyTurn("QUICK ATTACK"));
            ForceTurn(new RbyTurn("HORN ATTACK", Crit));
            if(!IT) ForceTurn(new RbyTurn("POTION", "NIDOKING"), new RbyTurn("TAIL WHIP"));
            else ForceTurn(new RbyTurn("HORN ATTACK", Miss), new RbyTurn("TAIL WHIP"));
            ForceTurn(new RbyTurn("HORN ATTACK"));
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("LEECH SEED"));
            ForceTurn(new RbyTurn("THRASH"));

            TalkTo("SSAnneCaptainsRoom", 4, 2); // hm01 received

            MoveTo("VermilionDock", 14, 2);
            ClearText();
            ClearText(); // watch cutscene
        });

        // ClearCache();
        CacheState("surge", () => {
            TalkTo("VermilionMart", 1, 5);
            Buy("REPEL",6,"PARLYZ HEAL",4);

            // Cut menu
            MoveTo("VermilionCity", 15, 17, Action.Down);
            if(IT) UseItem("POTION", "NIDOKING");
            UseItem("TM11", "NIDOKING", "POISON STING");
            UseItem("HM01", "PARAS");
            UseItem("TM28", "PARAS");
            Cut();

            // Manip
            MoveTo(15,19);
            Save();
            AdvanceFrames(32); // saving
            AdvanceFrames(105); // fade out (todo?)
            HardReset();

            NoPal.Execute(this);
            Execute(SpacePath("SDLALLAURUAUUUU")); // 60 cans
            ForceCan();
            MoveTo("VermilionGym", 6, 11);
            Press(Joypad.Right);
            ForceCan();

            // SURGE
            TalkTo(5, 1);
            ForceTurn(new RbyTurn("BUBBLEBEAM"), new RbyTurn("SONICBOOM", AiItem));
            ForceTurn(new RbyTurn("THRASH", ThreeTurn), new RbyTurn("SONICBOOM"));
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("QUICK ATTACK"));
            ForceTurn(new RbyTurn("THRASH"), new RbyTurn("GROWL", AiItem));
            ForceTurnAndSplit(new RbyTurn("THRASH"), new RbyTurn("GROWL"));
        });

        // ClearCache();
        CacheState("route9", () => {
            ClearText();
            CutAt("VermilionCity", 15, 18);
            TalkTo("PokemonFanClub", 3, 1);
            Yes();
            ClearText();
            Dig();
            ClearText();

            TalkTo("BikeShop", 6, 3);

            // Bike menu
            MoveTo("CeruleanCity", 13, 26);
            ItemSwap("POTION", "BICYCLE");
            UseItem("TM24", "NIDOKING", "HORN ATTACK");
            UseItem("BICYCLE");

            CutAt(19, 28);
            CutAt("Route9", 5, 8);

            // 4 TURN THRASH
            TalkTo(13, 10);
            ForceTurn(new RbyTurn("THRASH", ThreeTurn));
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"));
            ForceTurn(new RbyTurn("THRASH"));

            // BUG CATCHER
            TalkTo(40, 8);
            ForceTurn(new RbyTurn("BUBBLEBEAM", Crit));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("BUBBLEBEAM", SideEffect), new RbyTurn("TACKLE"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));

            MoveTo(21, 3, 10);
            MoveTo(8, 18);
            MoveAndSplit(Joypad.Up);
        });

        // ClearCache();
        CacheState("rocktunnel", () => {
            AfterMoveAndSplit();
            MoveTo("RockTunnel1F", 15, 4);
            UseItem("REPEL");

            // POKEMANIAC #1
            TalkTo("RockTunnel1F", 23, 8);
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));

            // POKEMANIAC #2
            TalkTo("RockTunnelB1F", 26, 30);
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));

            // ODDISH GIRL
            MoveTo(14, 29, Action.Up);
            Save();
            TalkTo(14, 28);
            ForceTurn(new RbyTurn("THRASH", Crit));
            ForceTurn(new RbyTurn("THRASH"));

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

            MoveTo(15, 32);
            MoveAndSplit(Joypad.Down);
        });

        // ClearCache();
        CacheState("fly", () => {
            AfterMoveAndSplit();
            PickupItemAt(21, 16, 53); // max ether

            // GAMBLER
            TalkTo("Route8", 46, 13);
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("THRASH"));

            MoveTo("UndergroundPathWestEast", 47, 2);

            UseItem("BICYCLE");
            PickupItemAt(21, 5, Action.Left); // elixer
            PickupItemAt(12, 2, Action.Left); // nugget

            MoveTo("Route7", 5, 14);
            UseItem("BICYCLE");

            // Shopping
            TalkTo("CeladonMart2F", 7, 3);
            Sell("TM34",1,"NUGGET",2);
            Buy("TM07", 1);
            TalkTo("CeladonMart2F", 5, 4);
            Buy("SUPER REPEL", 7, "SUPER POTION", 3, "REVIVE", 2);

            TalkTo("CeladonMart4F", 5, 6);
            Buy("POKE DOLL", 1);

            TalkTo("CeladonMartRoof", 12, 2);
            ChooseMenuItem(1); // get soda pop
            ClearText();

            TalkTo(5,5);
            Yes();
            ChooseMenuItem(0); // trade soda pop
            ClearText();

            TalkTo(12, 2, Action.Up);
            ChooseMenuItem(0); // get fresh water
            ClearText();

            TalkTo("CeladonMart5F", 5, 4);
            Buy("X ACCURACY", 13, "X SPECIAL", 6, "X SPEED", 3);

            TalkTo("CeladonMartElevator", 3, 0);
            ChooseMenuItem(0);

            MoveTo("CeladonCity", 8, 14);
            UseItem("BICYCLE");

            // Fly house
            CutAt("Route16", 34, 9);
            // MoveTo("Route16", 17, 4);
            // UseItem("BICYCLE");
            MoveTo("Route16FlyHouse", 2, 4);
            ReceiveItemAndSplit();
        });

        // ClearCache();
        CacheState("flute", () => {
            ClearText();

            // Fly menu
            MoveTo("Route16", 7, 6);
            ItemSwap("HELIX FOSSIL", "TM07");
            UseItem("SUPER REPEL");
            UseItem("TM48", "NIDOKING", "THRASH"); //early drill
            UseItem("TM07", "NIDOKING", "MEGA PUNCH");
            ItemSwap("S.S.TICKET", "X ACCURACY");
            UseItem("HM02", "PIDGEY");
            Fly("LavenderTown");

            // RIVAL 4
            MoveTo("PokemonTower2F", 15, 5);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("SAND-ATTACK"));
            ForceTurn(new RbyTurn("HORN DRILL"), new RbyTurn("QUICK ATTACK", Crit));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));

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
            ForceTurn(new RbyTurn("ROCK SLIDE", Crit));

            PickupItemAt(6, 8); // rare candy

            // MoveTo(10, 15);
            // UseItem("TM07", "NIDOKING", "ROCK SLIDE");

            MoveTo(10, 16);
            ClearText();
            ItemSwap("HM01", "SUPER REPEL");
            UseItem("POKE DOLL"); // escape ghost

            // ROCKET #1
            MoveTo("PokemonTower7F", 10, 11);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));

            // ROCKET #2
            MoveTo(10, 9);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("SMOG"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            // ROCKET #3
            MoveTo(10, 7);
            ClearText();
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("THUNDERBOLT"), new RbyTurn("QUICK ATTACK", Crit));
            ForceTurn(new RbyTurn("THUNDERBOLT", 30));
            ForceTurn(new RbyTurn("THUNDERBOLT"));

            // Get Pokéflute
            TalkTo(10, 3);
            MoveTo(2, 1);
            Press(Joypad.Right);
            ReceiveItemAndSplit();
        });

        // ClearCache();
        CacheState("silph", () => {
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
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("WING ATTACK", Crit));
            ItemSwap("POTION", "RARE CANDY");
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("WING ATTACK"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("ROCK SLIDE"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            MoveTo(5, 7, Action.Right);
            UseItem("MAX ETHER", "NIDOKING", "HORN DRILL");

            // SILPH ROCKET
            TalkTo("SilphCo11F", 3, 16);
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("GROWL"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            // SILPH GIOVANNI
            TalkTo(6, 13, Action.Up);
            MoveTo(6, 13);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("FURY ATTACK", 2 * Turns));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("BUBBLEBEAM"));
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));
        });

        // ClearCache();
        CacheState("safari", () => {
            ClearText();
            MoveTo(6, 14); // blocked by door? (todo)

            TalkTo(236, 3, 0);
            ChooseMenuItem(9);
            MoveTo(2, 3);
            Execute("D"); // exit elevator (todo?)

            // Get candy and EQ
            MoveTo(3, 9);
            MoveTo(2, 9); // thinks trainer is still there (todo)
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

            // PickupItemAt("Route17", 15, 14, Action.Down); // bugged (todo)
            MoveTo("Route17", 15, 5);
            // PickupItemAt("Route17", 15, 14); // candy
            PickupItemAt("Route17", 15, 13); // candy
            MoveTo("Route17", 17, 59);
            PickupItemAt("Route17", 17, 71); // pp up

            // Post cycling menu
            MoveTo("Route18", 40, 8);
            UseItem("REPEL");
            ItemSwap("PARLYZ HEAL", "TM26");
            UseItem("PP UP", "NIDOKING", "HORN DRILL");
            UseItem("TM26", "NIDOKING", "ROCK SLIDE");
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
        });

        // ClearCache();
        CacheState("koga", () => {
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
            ForceTurn(new RbyTurn("EARTHQUAKE", Miss), new RbyTurn("HEADBUTT"));
            ForceTurn(new RbyTurn("EARTHQUAKE"), new RbyTurn("POISON GAS"));
            ForceTurn(new RbyTurn("THUNDERBOLT", Crit));

            // KOGA
            TalkTo(4, 10);
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE", Crit));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurnAndSplit(new RbyTurn("ELIXER", "NIDOKING"), new RbyTurn("SELFDESTRUCT"));
        });

        // ClearCache();
        CacheState("erika", () => {
            ClearText();

            // Candy menu
            MoveTo("FuchsiaCity", 5, 28);
            UseItem("RARE CANDY", "NIDOKING");
            UseItem("RARE CANDY", "NIDOKING");
            UseItem("RARE CANDY", "NIDOKING");
            UseItem("BICYCLE");

            TalkTo("WardensHouse", 2, 3);
            MoveTo("FuchsiaCity", 27, 28);
            Fly("PalletTown");

            // Surf menu
            MoveTo(3, 17, Action.Right); // todo fix npc troll
            UseItem("SUPER REPEL");
            // ItemSwap("HM01", "X SPEED");
            UseItem("HM03", "SQUIRTLE");
            Surf();

            MoveTo("CinnabarIsland", 4, 4);
            TalkTo("PokemonMansion3F", 10, 5, Action.Up);
            ActivateMansionSwitch();
            MoveTo(16, 14);
            FallDown();

            // Blizzard menu
            PickupItemAt("PokemonMansionB1F", 19, 25);
            UseItem("HM04", "SQUIRTLE", "TACKLE");
            UseItem("TM14", "NIDOKING", "BUBBLEBEAM");
            UseItem("REPEL");

            TalkTo("PokemonMansionB1F", 18, 25, Action.Up);
            ActivateMansionSwitch();

            TalkTo(20, 3, Action.Up);
            ActivateMansionSwitch();
            PickupItemAt(10, 2); // candy
            PickupItemAt(5, 13); // secret key
            Dig();

            UseItem("BICYCLE");

            CutAt(35, 32);
            CutAt("CeladonGym", 2, 4);

            // BEAUTY
            MoveTo(3, 4);
            ClearText();
            ForceTurn(new RbyTurn("BLIZZARD"));

            // ERIKA
            TalkTo(4, 3);
            ForceTurn(new RbyTurn("EARTHQUAKE", Crit));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurnAndSplit(new RbyTurn("EARTHQUAKE"));
        });

        // ClearCache();
        CacheState("blaine", () => {
            ClearText();
            CutAt(5, 7);
            MoveTo("CeladonCity", 12, 28);

            Fly("CinnabarIsland");

            UseItem("BICYCLE");
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
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));
        });

        // ClearCache();
        CacheState("sabrina", () => {
            ClearText();
            Dig();
            UseItem("BICYCLE");

            MoveTo(18, 18, 10);
            UseItem("BICYCLE");

            // SABRINA
            TalkTo("SaffronGym", 9, 8);
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurnAndSplit(new RbyTurn("EARTHQUAKE"));
        });

        // ClearCache();
        CacheState("giovanni", () => {
            ClearText();
            MoveTo(1, 5);
            Dig();

            Fly("ViridianCity");
            UseItem("BICYCLE");

            // RHYHORN
            MoveTo("ViridianGym", 15, 5);
            ClearText();
            ForceTurn(new RbyTurn("EARTHQUAKE"));

            // BLACKBELT
            MoveTo(10, 5);
            Save();
            MoveTo(10, 4);
            ClearText();
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("LOW KICK"));
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
            ForceTurnAndSplit(new RbyTurn("BLIZZARD",30));
        });

        // ClearCache();
        CacheState("victoryroad", () => {
            ClearText();
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
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("HORN DRILL"));

            MoveTo("Route22Gate", 4, 2, Action.Up);
            ClearText();
            MoveTo("Route23", 7, 139);
            UseItem("BICYCLE");
            MoveTo(7, 136, Action.Up);
            ClearText();
            MoveTo(9, 119, Action.Up);
            ClearText();
            MoveTo(10, 105, Action.Up);
            ClearText();
            MoveTo(10, 104, Action.Up);
            Surf();
            MoveTo(10, 96, Action.Up);
            ClearText();

            // skip max ether

            MoveTo(7, 85, Action.Up);
            ClearText();
            MoveTo(8, 71, Action.Up);
            UseItem("SUPER REPEL");
            UseItem("BICYCLE");
            MoveTo(12, 56, Action.Up);
            ClearText();
            MoveTo(5, 35, Action.Up);
            ClearText();

            MoveTo("VictoryRoad1F", 8, 16);
            Strength();
            MoveTo(5, 14);
            PushBoulder(Joypad.Down);
            Execute("L D D");
            PushBoulder(Joypad.Right, 4);
            Execute("D R R");
            PushBoulder(Joypad.Up, 2);
            Execute("L U U");
            PushBoulder(Joypad.Right, 7);
            Execute("D R R");
            PushBoulder(Joypad.Up, 2);
            Execute("L L U U R");
            PushBoulder(Joypad.Right);
            Execute("U R R");
            PushBoulder(Joypad.Down);
            MoveTo("VictoryRoad2F", 5, 14);

            UseItem("SUPER REPEL");
            Strength();

            PushBoulder(Joypad.Left);
            Execute("U L L");
            PushBoulder(Joypad.Down, 2);
            Execute("R D D");
            PushBoulder(Joypad.Left, 2);

            MoveTo("VictoryRoad3F", 23, 6);
            Strength();
            MoveTo(22, 4);
            PushBoulder(Joypad.Up, 2);
            Execute("R U U");
            PushBoulder(Joypad.Left, 16);
            Execute("U L L");
            PushBoulder(Joypad.Down);
            Execute("R D D");
            PushBoulder(Joypad.Left, 4);
            Execute("L U L");
            PushBoulder(Joypad.Down, 3);
            Execute("L D D");
            PushBoulder(Joypad.Right);
            Execute("U"); //?? todo

            MoveTo(21, 15, Action.Right);
            PushBoulder(Joypad.Right);
            Execute("R R");
            FallDown();

            // VR menu
            Strength();
            UseItem("SUPER REPEL");
            UseItem("BICYCLE");

            Execute("D R R U");
            PushBoulder(Joypad.Left, 14);

            MoveTo("VictoryRoad2F", 29, 7);
            MoveAndSplit(Joypad.Right);
            AdvanceFrames(20);
        });

        // ClearCache();
        CacheState("lorelei", () => {
            AfterMoveAndSplit();

            // TalkTo("IndigoPlateauLobby", 15, 8, Action.Up);
            // PC
            // ChooseMenuItem(0);
            // ClearText();
            //     ChooseMenuItem(1);
            //     ChooseMenuItem(1);
            //     ChooseMenuItem(0);
            //     ClearText();
            //     ChooseMenuItem(1);
            //     ChooseMenuItem(2);
            //     ChooseMenuItem(0);
            //     ClearText();
            // MenuPress(Joypad.B);
            // MenuPress(Joypad.B);

            MoveTo("IndigoPlateauLobby", 8, 0);

            // LORELEI
            TalkTo("LoreleisRoom", 5, 2, Action.Right);
            BattleSwitch("PIDGEY", new RbyTurn("AURORA BEAM", Crit));
            SendOut("NIDOKING");
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("REST"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));
        });

        // ClearCache();
        CacheState("bruno", () => {
            ClearText();
            Execute("U U U");
            UseItem("ELIXER", "NIDOKING");

            // BRUNO
            TalkTo("BrunosRoom", 5, 2, Action.Right);
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("RAGE", Crit));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));
        });

        // ClearCache();
        CacheState("agatha", () => {
            ClearText();
            Execute("U U U");
            UseItem("SUPER POTION", "NIDOKING");
            UseItem("RARE CANDY", "NIDOKING");

            // AGATHA
            TalkTo("AgathasRoom", 5, 2, Action.Right);
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("DREAM EATER"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("EARTHQUAKE"));
            ForceTurn(new RbyTurn("EARTHQUAKE", Crit));
            ForceTurnAndSplit(new RbyTurn("EARTHQUAKE"));
        });

        // ClearCache();
        CacheState("lance", () => {
            ClearText();
            Execute("U U U");
            MoveTo("LancesRoom", 5, 2, Action.Up);
            UseItem("ELIXER", "NIDOKING");
            UseItem("SUPER POTION", "NIDOKING");
            UseItem("POTION", "NIDOKING");
            Save();

            // LANCE
            MoveTo("LancesRoom", 5, 1);
            ClearText();
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("HYDRO PUMP", 16));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurn(new RbyTurn("BLIZZARD"));
            ForceTurn(new RbyTurn("X SPEED"), new RbyTurn("AGILITY"));
            ForceTurn(new RbyTurn("BLIZZARD", Crit));
            ForceTurn(new RbyTurn("THUNDERBOLT"));
            ForceTurnAndSplit(new RbyTurn("BLIZZARD"));
        });

        // ClearCache();
        CacheState("champion", () => {
            ClearText();
            Execute("U U");

            // CHAMPION
            ClearText();
            ForceTurn(new RbyTurn("X SPECIAL"), new RbyTurn("WHIRLWIND"));
            ForceTurn(new RbyTurn("X ACCURACY"), new RbyTurn("MIRROR MOVE"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurn(new RbyTurn("HORN DRILL"));
            ForceTurnAndSplit(new RbyTurn("HORN DRILL"));
        });

        // ClearCache();
        CacheState("end", () => {
            ClearText();
            ClearText(Joypad.None, 26);
            AdvanceFrames(164);
        });

        Timer.Stop();
        AdvanceFrames(600);
        Dispose();
    }
}
