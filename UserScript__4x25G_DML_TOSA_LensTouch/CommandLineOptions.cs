using CommandLine;

namespace UserScript
{
    public class Options
    {

        [Option("sensor-name", Required = false, Default = "M12 A0 Attached",
            HelpText = "传感器名称")]
        public string SensorName { get; set; }

        [Option("feed-in-step", Required = false, Default = 5,
            HelpText = "Lens探底进给步进")]
        public double FeedInStep { get; set; }

        [Option("feed-in-limit", Required = false, Default = 200,
           HelpText = "Lens探底进给最大距离")]
        public double FeedInLimit { get; set; }

        [Option("feed-in-speed", Required = false, Default = 5,
           HelpText = "Lens探底进给速度")]
        public int FeedInSpeed { get; set; }

        [Option("volt-diff", Required = false, Default = 200,
          HelpText = "Lens探底时传感器的电压差值")]
        public double SensorVoltageDiff { get; set; }

        [Option("skip-org-return", Required = false, HelpText = "Lens探底后上提，返回接触原点")]
        public bool SkipReturnToOrg { get; set; }


        [Option("lmu", Required = false, Default = "Lens",
           HelpText = "控制Lens探底的耦合单元名称")]
        public string LMU { get; set; }

        [Option("axis", Required = false, Default = "Y",
          HelpText = "控制Lens探底的轴名称")]
        public string Axis { get; set; }

    }
}