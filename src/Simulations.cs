using System;
using System.Collections.Generic;

public static class Simulations
{
    public static void NerdVoltorb()
    {
        var s = new Simulation<Red>("basesaves/red/nerdvoltorb.gqs").Track("Time", "HP", "Success");
        s.Simulate("WG + PS", gb =>
        {
            while(gb.EnemyMon.Species.Name == "VOLTORB" && gb.BattleMon.HP > 0)
            {
                // if(gb.EnemyMon.HP > 10) gb.UseMove("WATER GUN"); else gb.UseMove("POISON STING");
                if(gb.EnemyMon.HP == 33 || (gb.EnemyMon.HP >= 16 && gb.EnemyMon.HP <= 20)) gb.UseMove("WATER GUN"); else gb.UseMove("POISON STING");
            }
            return gb.BattleMon.HP > 0;
        });
        s.Simulate("Spam PS", gb =>
        {
            while(gb.EnemyMon.Species.Name == "VOLTORB" && gb.BattleMon.HP > 0)
                gb.UseMove("POISON STING");
            return gb.BattleMon.HP > 0;
        });
    }

    public static void BC2Caterpie()
    {
        Action<Red> Metapod = gb =>
        {
            while(gb.EnemyMon.Species.Name == "METAPOD" && gb.BattleMon.HP > 0 && gb.EnemyMon.HP > 0)
                gb.UseMove("HORN ATTACK");
        };
        Func<Red, bool> Leer_HA = gb =>
        {
            gb.ClearText();
            while(gb.EnemyMon.Species.Name == "CATERPIE" && gb.BattleMon.HP > 0)
            {
                if(gb.EnemyMon.DefenseModifider == 7)
                    gb.UseMove("LEER");
                else if(gb.EnemyMon.HP > 17)
                    gb.UseMove("HORN ATTACK");
                else
                    gb.UseMove("TACKLE");
            }
            Metapod(gb);
            return gb.BattleMon.HP > 0;
        };
        Func<Red, bool> HA_Tackle = gb =>
        {
            gb.ClearText();
            while(gb.EnemyMon.Species.Name == "CATERPIE" && gb.BattleMon.HP > 0)
            {
                if(gb.EnemyMon.HP > 10)
                    gb.UseMove("HORN ATTACK");
                else
                    gb.UseMove("TACKLE");
            }
            Metapod(gb);
            return gb.BattleMon.HP > 0;
        };
        Func<Red, bool> Tackle_HA = gb =>
        {
            gb.ClearText();
            while(gb.EnemyMon.Species.Name == "CATERPIE" && gb.BattleMon.HP > 0)
            {
                if(gb.EnemyMon.HP == 28)
                    gb.UseMove("TACKLE");
                else
                    gb.UseMove("HORN ATTACK");
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

    public static void ClassicBlackbelt()
    {
        var s = new Simulation<Red>("basesaves/red/classicblackbelt.gqs", 65536).Track("Time", "Success");

        for(int atk = 104; atk <= 121; ++atk)
        {
            s.Simulate("EQ (" + atk + " Atk)", gb =>
            {
                gb.CpuWriteBE<ushort>("wBattleMonAttack", (ushort)(atk * 9 / 8));
                while(gb.BattleMon.HP > 0 && gb.EnemyMon.HP > 0)
                {
                    if(gb.EnemyMon.HP < 50) gb.UseMove("THUNDERBOLT"); else gb.UseMove("EARTHQUAKE");
                }
                return gb.BattleMon.HP > 0;
            });
        }
        s.Simulate("X Acc + HD", gb =>
        {
            gb.UseItem("X ACCURACY");
            // while(gb.BattleMon.HP > 0 && gb.EnemyMon.HP > 0)
            // {
            //     gb.UseMove("HORN DRILL");
            // }
            return gb.BattleMon.HP > 0;
        });
    }

    public static void Route1NPC(bool side)
    {
        const int it = 65536;
        var s = new Simulation<Red>("basesaves/red/route1npc.gqs", it);

        void RunAway(Rby gb) {
            gb.ClearText();
            gb.CpuWriteBE<ushort>("wBattleMonSpeed", 20);
            gb.BattleMenu(1, 1);
            gb.ClearText();
        }

        List<double> listencounters = new List<double>(it);
        List<double> liststeps = new List<double>(it);
        List<double> listtrolled = new List<double>(it);
        List<double> listcollision = new List<double>(it);

        Red r = new Red();
        int collisionaddress = r.SYM["CollisionCheckOnLand.collision"];
        int encounteraddress = r.SYM["CalcStats"];

        string path = side ? "URRRRUUUU" : "URRUUUURR";

        s.Simulate("https://gunnermaniac.com/pokeworld?local=12#9/19/" + path, gb =>
        {
            int encounters = 0;
            int steps = 0;
            int trolled = 0;
            int collision = 0;
            int addr, x;

            void Move(string dir)
            {
                addr = gb.Execute(dir);
                while(addr == collisionaddress)
                {
                    collision = 1;
                    addr = gb.Execute(dir);
                }
                steps++;
                if(addr == encounteraddress)
                {
                    encounters++;
                    RunAway(gb);
                }
            }
            void Decision1414()
            {
                x = gb.CpuRead("wSprite02StateData2MapX") - 4;
                if(x == 14)
                {
                    trolled++;
                    Move("R");
                    Decision1514();
                }
                else
                {
                    Move("U");
                    Move("U");
                    Move("U");
                }
            }
            void Decision1514()
            {
                x = gb.CpuRead("wSprite02StateData2MapX") - 4;
                if(x == 15)
                {
                    trolled++;
                    Move("L");
                    Decision1414();
                }
                else
                {
                    Move("U");
                    Move("U");
                    Move("U");
                    Move("L");
                }
            }

            gb.Execute(RbyForceComparisons.SpacePath(path));
            Move("R");
            Decision1414();

            lock(listencounters) {
                listencounters.Add(encounters);
                liststeps.Add(steps);
                listtrolled.Add(trolled);
                listcollision.Add(collision);
            }

            return true;
        });

        SimulationUtils.PrintResults("Encounters", listencounters);
        SimulationUtils.PrintResults("Steps", liststeps);
        SimulationUtils.PrintResults("Trolled", listtrolled);
        SimulationUtils.PrintResults("Bonks", listcollision);
    }

    public static void YoloLance()
    {
        var s = new Simulation<Red>("basesaves/red/lanceyolo.gqs", 65536).Track("Success", "Time");
        s.Simulate("TB", gb =>
        {
            while(gb.EnemyMon.Species.Name == "GYARADOS" && gb.BattleMon.HP > 0)
            {
                gb.UseMove("THUNDERBOLT");
            }
            return gb.BattleMon.HP > 0;
        });
        s.Simulate("HD", gb =>
        {
            while(gb.EnemyMon.Species.Name == "GYARADOS" && gb.BattleMon.HP > 0)
            {
                gb.UseMove("HORN DRILL");
            }
            return gb.BattleMon.HP > 0;
        });
        s.Simulate("X Spec", "basesaves/red/lance.gqs", gb =>
        {
            gb.UseItem("X SPECIAL");
            while(gb.BattleMon.HP > 100)
                gb.UseMove("EARTHQUAKE");
            while(gb.EnemyMon.Species.Name == "GYARADOS" && gb.BattleMon.HP > 0)
                gb.UseMove("THUNDERBOLT");
            return gb.BattleMon.HP > 0;
        });
    }
}
