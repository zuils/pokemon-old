using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using static SimulationUtils;

public class Simulation<Gb> where Gb : GameBoy
{
    public int Iterations;
    public int NumThreads;
    public byte[] State;
    public List<(string, Func<Gb, bool?, double?>)> Variables;

    public Simulation(int iterations = 1000, int numThreads = 20)
    {
        Iterations = iterations;
        NumThreads = numThreads;
        Variables = new List<(string, Func<Gb, bool?, double?>)>();
    }

    public Simulation(byte[] state, int iterations = 1000, int numThreads = 20) : this(iterations, numThreads)
    {
        State = state;
    }

    public Simulation(string statePath, int iterations = 1000, int numThreads = 20) : this(File.ReadAllBytes(statePath), iterations, numThreads)
    {
    }

    public void Simulate(string title, byte[] state, Func<Gb, bool> scenario)
    {
        State = state;
        Simulate(title, scenario);
    }

    public void Simulate(string title, string statePath, Func<Gb, bool> scenario)
    {
        Simulate(title, File.ReadAllBytes(statePath), scenario);
    }

    public Simulation<Gb> Track(params string[] variables)
    {
        foreach(string name in variables)
        {
            if(name == "Time")
                Track(name, (gb, s) => { return s != false ? (double?) gb.EmulatedSamples / 2097152.0 : null; });
            else if(name == "HP" && typeof(Gb).IsSubclassOf(typeof(Rby)))
                Track(name, (gb, s) => { return ((Rby) (object) gb).BattleMon.HP - ((Rby) (object) gb).BattleMon.MaxHP; });
            else if(name == "Success")
                Track(name, (gb, s) => { return s == true ? 1 : 0; });
        }
        return this;
    }

    public Simulation<Gb> Track(string name, Func<Gb, bool?, double?> get)
    {
        Variables.Add((name, get));
        return this;
    }

    public void Simulate(string title, Func<Gb, bool> scenario)
    {
        Trace.WriteLine(title);
        List<double>[] data = new List<double>[Variables.Count];
        for(int i = 0; i < data.Length; ++i)
            data[i] = new List<double>(Iterations);

        Gb[] gbs = MultiThread.MakeThreads<Gb>(NumThreads);
        if(NumThreads == 1) gbs[0].Record("test");

        byte[] rngvalues = new byte[Iterations * 3];
        if(Iterations % 65536 == 0)
        {
            for(int i = 0; i < Iterations; ++i)
            {
                rngvalues[3 * i] = (byte) i;
                rngvalues[3 * i + 1] = (byte) (i / 256);
                rngvalues[3 * i + 2] = (byte) (i / 65536);
            }
        }
        else
        {
            new Random(123456789).NextBytes(rngvalues);
        }

        var w = Stopwatch.StartNew();
        MultiThread.For(NumThreads, gbs, (gb, thread) =>
        {
            gb.LoadState(State);
            byte[] state = gb.SaveState();

            for(int i = thread * Iterations / NumThreads; i < (thread + 1) * Iterations / NumThreads; ++i)
            {
                // sf: 0x282 + 0x23c ; gsr: 0x1902 + 0x23c ; cpp: 0x3dd + 0x24b
                state[0x3dd + 0x24b] = rngvalues[3 * i]; // rDIV
                state[0x3dd + 0x1d3] = rngvalues[3 * i + 1]; // HRA
                state[0x3dd + 0x1d4] = rngvalues[3 * i + 2]; // HRS
                gb.LoadState(state);

                double[] start = new double[Variables.Count];
                for(int v = 0; v < Variables.Count; ++v)
                    start[v] = (double) Variables[v].Item2(gb, null);

                bool success = scenario(gb);

                lock(data)
                {
                    for(int v = 0; v < Variables.Count; ++v)
                    {
                        double? end = Variables[v].Item2(gb, success);
                        if(end != null)
                            data[v].Add((double) end - start[v]);
                    }
                }
            }
        });

        Console.WriteLine(w.Elapsed.TotalSeconds + "s");

        for(int i = 0; i < Variables.Count; ++i)
            PrintResults(Variables[i].Item1, data[i]);
        Trace.WriteLine("");
    }
}

public static class SimulationUtils
{
    public static void PrintResults(string name, List<double> list)
    {
        if(list.Count > 0)
        {
            list.Sort();
            Trace.WriteLine(name +
                $"\n\tAverage: {list.Average()      :G5}" +
                $"\n\tMedian:  {list[list.Count / 2]:G5}" +
                $"\n\tStdev:   {Stdev(list)         :G5}" +
                $"\n\tMin:     {list.Min()          :G5}" +
                $"\n\tMax:     {list.Max()          :G5}"
            );
        }
    }

    public static double Stdev(List<double> list)
    {
        double avg = list.Average();
        return Math.Sqrt(list.Average(v => (v - avg) * (v - avg)));
    }

    public static void UseMove(this Rby gb, string move)
    {
        if(gb.PC != 0x019C) gb.ClearText();
        gb.BattleMenu(0, 0);
        gb.ChooseMenuItem(Array.IndexOf(gb.BattleMon.Moves, gb.Moves[move]));
        gb.ClearText(Joypad.None, int.MaxValue, 0x0f4696, 0x0f4700);
    }

    public static void UseItem(this Rby gb, string item)
    {
        if(gb.PC != 0x019C) gb.ClearText();
        gb.BattleMenu(0, 1);
        gb.ChooseListItem(gb.Bag.IndexOf(item));
        gb.ClearText(Joypad.None, int.MaxValue, 0x0f4696, 0x0f4700);
    }
}
