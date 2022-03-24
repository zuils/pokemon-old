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

    Comparison Comparison;
    public BlueCEA() : base("roms/pokeblue.gbc", true)
    {
        Comparison = new Comparison(this);
        Use4Flash4();
    }
}
