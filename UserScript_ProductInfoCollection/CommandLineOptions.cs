using CommandLine;

namespace UserScript
{
    public class Option
    {
        [Option("save-to-path", Required = false, Default = @"100G TOSA 耦合数据",
            HelpText = "耦合数据保存路径文件夹名称。")]
        public string FolderName { get; set; }

        [Option("filename-prefix", Required = false, Default = "耦合数据汇总_100G_TOSA_",
            HelpText = "文件名前缀，完整的文件名包含当天日期信息。")]
        public string FilenamePrefix { get; set; }
    }
}