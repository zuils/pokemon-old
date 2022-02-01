using System;

class Simulations
{
    public static void NerdVoltorb()
    {
        var s = new Simulation<Red>("basesaves/red/nerdvoltorb.gqs").Track("Time", "HP", "Success");
        s.Simulate("WG + PS", (gb) =>
        {
            gb.ClearText();
            while(gb.EnemyMon.Species.Name == "VOLTORB" && gb.BattleMon.HP > 0)
            {
                gb.BattleMenu(0, 0);
                // if(gb.EnemyMon.HP>10) gb.ChooseMenuItem(1); else gb.ChooseMenuItem(3); //wg+ps
                if(gb.EnemyMon.HP == 33 || (gb.EnemyMon.HP >= 16 && gb.EnemyMon.HP <= 20)) gb.ChooseMenuItem(1); else gb.ChooseMenuItem(3); //wg+ps
                gb.ClearText();
            }
            return gb.BattleMon.HP > 0;
        });
        s.Simulate("Spam PS", (gb) =>
        {
            gb.ClearText();
            while(gb.EnemyMon.Species.Name == "VOLTORB" && gb.BattleMon.HP > 0)
            {
                gb.BattleMenu(0, 0);
                gb.ChooseMenuItem(3); //ps
                gb.ClearText();
            }
            return gb.BattleMon.HP > 0;
        });
    }

    public static void BC2Caterpie()
    {
        Action<Red> Metapod = (gb) =>
        {
            while(gb.EnemyMon.Species.Name == "METAPOD" && gb.BattleMon.HP > 0 && gb.EnemyMon.HP > 0)
            {
                gb.BattleMenu(0, 0);
                gb.ChooseMenuItem(2); //ha
                gb.ClearText();
            }
        };
        Func<Red, bool> Leer_HA = (gb) =>
        {
            gb.ClearText();
            while(gb.EnemyMon.Species.Name == "CATERPIE" && gb.BattleMon.HP > 0)
            {
                gb.BattleMenu(0, 0);
                if(gb.EnemyMon.DefenseModifider == 7)
                    gb.ChooseMenuItem(0); //leer
                else if(gb.EnemyMon.HP > 17)
                    gb.ChooseMenuItem(2); //ha
                else
                    gb.ChooseMenuItem(1); //tackle
                gb.ClearText();
            }
            Metapod(gb);
            return gb.BattleMon.HP > 0;
        };
        Func<Red, bool> HA_Tackle = (gb) =>
        {
            gb.ClearText();
            while(gb.EnemyMon.Species.Name == "CATERPIE" && gb.BattleMon.HP > 0)
            {
                gb.BattleMenu(0, 0);
                if(gb.EnemyMon.HP > 10)
                    gb.ChooseMenuItem(2); //ha
                else
                    gb.ChooseMenuItem(1); //tackle
                gb.ClearText();
            }
            Metapod(gb);
            return gb.BattleMon.HP > 0;
        };
        Func<Red, bool> Tackle_HA = (gb) =>
        {
            gb.ClearText();
            while(gb.EnemyMon.Species.Name == "CATERPIE" && gb.BattleMon.HP > 0)
            {
                gb.BattleMenu(0, 0);
                if(gb.EnemyMon.HP == 28)
                    gb.ChooseMenuItem(1); //tackle
                else
                    gb.ChooseMenuItem(2); //ha
                gb.ClearText();
            }
            Metapod(gb);
            return gb.BattleMon.HP > 0;
        };

        var s = new Simulation<Red>().Track("Time", "HP", "Success");
        s.Simulate("Leer + HA (cursor on HA)", "basesaves/red/bc2caterpieha30.gqs", Leer_HA);
        s.Simulate("HA + Tackle (cursor on HA)", "basesaves/red/bc2caterpieha30.gqs", HA_Tackle);
        s.Simulate("\n\n\nLeer + HA (cursor on Tackle)", "basesaves/red/bc2caterpieta30.gqs", Leer_HA);
        s.Simulate("HA + Tackle (cursor on Tackle)", "basesaves/red/bc2caterpieta30.gqs", HA_Tackle);
        s.Simulate("Tackle + HA (cursor on Tackle)", "basesaves/red/bc2caterpieta30.gqs", Tackle_HA);
    }
}
