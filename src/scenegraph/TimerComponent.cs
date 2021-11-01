using System;
using System.Numerics;

public class TimerComponent : TextComponent {

    public ulong Start;
    public bool Running = true;

    public Vector4 RunningColor = new Vector4(97.0f / 255.0f, 200.0f / 255.0f, 135.0f / 255.0f, 255.0f);
    public Vector4 FinishedColor = new Vector4(84.0f / 255.0f, 167.0f / 255.0f, 229.0f / 255.0f, 255.0f);

    public TimerComponent(float x, float y, float scale = 1) : base("", x, y, scale) {
    }

    public override void OnInit(GameBoy gb) {
        Start = gb.EmulatedSamples;
    }

    public TimeSpan Duration(GameBoy gb)
    {
        return TimeSpan.FromSeconds((gb.EmulatedSamples - Start) / 2097152.0);
    }

    public override void BeginScene(GameBoy gb) {
        TimeSpan duration = Duration(gb);//TimeSpan.FromSeconds((gb.EmulatedSamples - Start) / 2097152.0);
        // if(Running) Text = string.Format("{0:ss\\.ff}", duration);
        if(Running)
        {
            if(duration.Hours>0)
                Text = string.Format("{0:h\\:mm\\:ss\\.ff}", duration);
            else
                Text = string.Format("{0:mm\\:ss\\.fff}", duration);
        }
        // if(Running) Text = duration.ToString();
    }

    public override void Render(GameBoy gb) {
        Renderer.DrawString(Text, X, Y, RenderLayer, Scale, Running ? RunningColor : FinishedColor);
    }
}