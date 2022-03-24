using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class Comparison
{
    GameBoy Gb;

    public Comparison(GameBoy gb)
    {
        Gb = gb;
    }

    public delegate void Scenario();

    public void Compare(string name, byte[][] states, Scenario[] scenarios, bool video = true, bool record = true, int wait = 300, int ratio = 1)
    {
        Gb.SetSpeedupFlags(SpeedupFlags.None);

        TimerComponent[] timers = new TimerComponent[scenarios.Length];
        double[] times = new double[scenarios.Length];
        double longest = 0;

        for(int i = scenarios.Length - 1; i >= 0; --i)
        {
            Gb.LoadState(states[i]);
            timers[i] = new TimerComponent(0, 144 * ratio, 2.0f * ratio);
            if(video)
            {
                new Scene(Gb, 160 * ratio, 160 * ratio);
                Gb.Scene.AddComponent(new VideoBufferComponent(0, 0, 160 * ratio, 144 * ratio));
                if(record) Gb.Scene.AddComponent(new RecordingComponent((i + 1).ToString()));
                Gb.Scene.AddComponent(timers[i]);
            }
            timers[i].OnInit(Gb);

            scenarios[i]();

            timers[i].Running = false;
            times[i] = timers[i].Duration().TotalSeconds;
            Gb.AdvanceFrames(wait);
            if(times[i] > longest)
                longest = times[i];
            else
                Gb.AdvanceFrames((int) ((longest - times[i]) * 59.7));
            if(video) Gb.Scene.Dispose();
        }

        string movies = "";
        for(int i = 0; i < scenarios.Length; ++i)
        {
            Console.Write((i + 1) + ":  " + (video ? timers[i].Text : times[i].ToString("F3")) + "  ");
            for(int j = 0; j < scenarios.Length; ++j)
            {
                if(i != j)
                {
                    double delta = times[i] - times[j];
                    Console.Write(" " + (delta < 0 ? "" : "+") + delta.ToString("F3"));
                }
            }
            movies += "-i movies/" + (i + 1) + ".mp4 ";
            Console.WriteLine();
        }

        if(video && record) FFMPEG.RunFFMPEGCommand("-y " + movies + "-filter_complex hstack=inputs=" + scenarios.Length + " movies/" + name + ".mp4");
    }

    public void Compare(string name, byte[] leftstate, byte[] rightstate, Scenario left, Scenario right, bool video = true, bool record = true, int wait = 300, int ratio = 1)
    {
        Compare(name, new byte[][] { leftstate, rightstate }, new Scenario[] { left, right }, video, record, wait, ratio);
    }

    public void Compare(string name, string leftpath, string rightpath, Scenario left, Scenario right, bool video = true, bool record = true, int wait = 300, int ratio = 1)
    {
        Compare(name, File.ReadAllBytes(leftpath), File.ReadAllBytes(rightpath), left, right, video, record, wait, ratio);
    }

    public void Compare(string name, byte[] state, Scenario left, Scenario right, bool video = true, bool record = true, int wait = 300, int ratio = 1)
    {
        Compare(name, state, state, left, right, video, record, wait, ratio);
    }

    public void Compare(string statepath, Scenario left, Scenario right, bool video = true, bool record = true, int wait = 300, int ratio = 1)
    {
        string name = Regex.Match(statepath, @"([^/\\]+)\.gqs").Groups[1].Value;
        Compare(name, File.ReadAllBytes(statepath), left, right, video, record, wait, ratio);
    }
}

public class RedBlueComparisons : RedBlueForce
{
    public static RbyIntroSequence NoPal = new RbyIntroSequence(RbyStrat.NoPal);
    public static RbyIntroSequence PalHold = new RbyIntroSequence(RbyStrat.PalHold);
    public TimerComponent Timer = new TimerComponent(0, 144, 2.0f);

    public static string SpacePath(string path)
    {
        string output = "";
        string[] validActions = new string[] { "A", "U", "D", "L", "R", "S", "S_B" };
        while(path.Length > 0)
        {
            if(validActions.Any(path.StartsWith))
            {
                if(path.StartsWith("S_B"))
                {
                    output += "S_B";
                    path = path.Remove(0, 3);
                }
                else if(path.StartsWith("S"))
                {
                    output += "S_B";
                    path = path.Remove(0, 1);
                }
                else
                {
                    output += path[0];
                    path = path.Remove(0, 1);
                }

                output += " ";
            }
            else
            {
                throw new Exception(String.Format("Invalid Path Action Recieved: {0}", path));
            }
        }
        return output.Trim();
    }

    public void MoveAndSplit(Joypad j)
    {
        Inject(j);
        AdvanceFrames(16, j);
    }

    public void AfterMoveAndSplit()
    {
        do
        {
            RunFor(1);
            RunUntil(SYM["JoypadOverworld"]);
        } while((CpuRead("wd730") & 0xa0) > 0);
    }

    public void ReceiveItemAndSplit()
    {
        Press(Joypad.A);
        ClearTextUntil(Joypad.None, SYM["GiveItem"]);
        RunUntil("PlaySound");
    }

    public void ForceTurnAndSplit(RbyTurn playerTurn, RbyTurn enemyTurn = null, bool speedTieWin = true)
    {
        ForceTurn(playerTurn, enemyTurn, speedTieWin, false);
        ClearTextUntil(Joypad.None, SYM["EnterMap"]);
    }

    public void SaveAndQuit()
    {
        Save();
        RunUntil(SYM["SaveSAV.save"] + 0x3);
        HardReset(true);
    }

    public void RecordAndTime(string movie, bool start = false)
    {
        SetSpeedupFlags(SpeedupFlags.None);
        Scene s = new Scene(this, 160, 160);
        s.AddComponent(new VideoBufferComponent(0, 0, 160, 144));
        s.AddComponent(new RecordingComponent(movie));
        s.AddComponent(Timer);
        Timer.Running = start;
    }

    public RedBlueComparisons(string rom = "roms/pokered.gbc", bool speedup = true) : base(rom, speedup)
    {
    }

    public RedBlueComparisons(bool speedup) : base("roms/pokered.gbc", speedup)
    {
    }
}
