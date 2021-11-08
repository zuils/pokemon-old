using System;
using System.Numerics;

public class TimerComponent : TextComponent {

    public ulong StartTime;
    public bool Running = true;
    GameBoy Gb;

    public Vector4 RunningColor = new Vector4(97.0f / 255.0f, 200.0f / 255.0f, 135.0f / 255.0f, 255.0f);
    public Vector4 FinishedColor = new Vector4(84.0f / 255.0f, 167.0f / 255.0f, 229.0f / 255.0f, 255.0f);

    public TimerComponent(float x, float y, float scale = 1) : base("", x, y, scale) {
    }

    public override void OnInit(GameBoy gb) {
        StartTime = gb.EmulatedSamples;
        Gb = gb;
    }

    public void Start() {
        if(Gb != null) OnInit(Gb);
        Running = true;
    }

    public void Stop() {
        Running = false;
    }

    public TimeSpan Duration() {
        return TimeSpan.FromSeconds((Gb.EmulatedSamples - StartTime) / 2097152.0);
    }

    public override void BeginScene(GameBoy gb) {
        TimeSpan duration = Duration();
        if(Running) {
            if(duration.Hours>0)
                Text = string.Format("{0:h\\:mm\\:ss\\.ff}", duration);
            else
                Text = string.Format("{0:mm\\:ss\\.fff}", duration);
        }
    }

    public override void Render(GameBoy gb) {
        Renderer.DrawString(Text, X, Y, RenderLayer, Scale, Running ? RunningColor : FinishedColor);
    }
}