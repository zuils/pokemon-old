using System;
using System.IO;
using System.Linq;

public partial class RedGlitchless : RedBlueForce {
    public static RbyIntroSequence NoPal = new RbyIntroSequence(RbyStrat.NoPal);
    public static RbyIntroSequence PalHold = new RbyIntroSequence(RbyStrat.PalHold);
    public TimerComponent Timer = new TimerComponent(0,144,2.0f);

    public static string SpacePath(string path)
    {
        string output = "";
        string[] validActions = new string[] { "A", "U", "D", "L", "R", "S", "S_B" };
        while(path.Length > 0) {
            if (validActions.Any(path.StartsWith)) {
                if (path.StartsWith("S_B")) {
                    output += "S_B";
                    path = path.Remove(0, 3);
                } else if(path.StartsWith("S")) {
                    output += "S_B";
                    path = path.Remove(0, 1);
                } else {
                    output += path[0];
                    path = path.Remove(0, 1);
                }

                output += " ";
            } else {
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
        do {
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

    public RedGlitchless(string rom = "roms/pokered.gbc", bool speedup = true) : base(rom, speedup)
    {
    }

    public RedGlitchless(bool speedup) : base("roms/pokered.gbc", speedup)
    {
    }
}
