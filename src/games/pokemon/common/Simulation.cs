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
    public string StatePath;
    public List<(string, Func<Gb, bool?, double?>)> Variables;

    public Simulation(int iterations = 1000, int numThreads = 20)
    {
        Iterations = iterations;
        NumThreads = numThreads;
        Variables = new List<(string, Func<Gb, bool?, double?>)>();
    }

    public Simulation(string statePath, int iterations = 1000, int numThreads = 20) : this(iterations, numThreads)
    {
        StatePath = statePath;
    }

    public void Simulate(string title, string statePath, Func<Gb, bool> scenario)
    {
        StatePath = statePath;
        Simulate(title, scenario);
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
            data[i] = new List<double>();

        Gb[] gbs = MultiThread.MakeThreads<Gb>(NumThreads);
        var w = Stopwatch.StartNew();
        MultiThread.For(NumThreads, gbs, (gb, thread) =>
        {
            gb.LoadState(StatePath);
            gb.AdvanceFrames((int) thread * Iterations / NumThreads);
            byte[] state = gb.SaveState();

            for(int i = 0; i < Iterations / NumThreads; ++i)
            {
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

                gb.LoadState(state);
                gb.AdvanceFrame();
                state = gb.SaveState();
            }
        });

        Console.WriteLine(w.Elapsed.TotalSeconds + "s");

        for(int i = 0; i < Variables.Count; ++i)
            PrintResults(Variables[i].Item1, data[i]);
        Trace.WriteLine("");
    }
}

public class SimulationUtils
{
    public static void PrintResults(string name, List<double> list)
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

    public static double Stdev(List<double> list)
    {
        double avg = list.Average();
        return Math.Sqrt(list.Average(v => (v - avg) * (v - avg)));
    }
}
