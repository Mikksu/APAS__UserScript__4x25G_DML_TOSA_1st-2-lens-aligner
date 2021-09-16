using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using CommandLine;
using CommandLine.Text;
using UserScript.SystemService;

namespace UserScript
{
    /// <summary>
    /// =========================== ATTENTION ===========================
    /// ===========================    注意   =========================== 
    /// =                                                               =  
    /// =          Please DO NOT make ANY changes to this file.         =
    /// =                    请勿修改当前文件的任何内容。                   =
    /// =                                                               =
    /// =================================================================
    /// 
    /// </summary>
    /// 
    internal partial class APAS_UserScript
    {
        private static void Main(string[] args)
        {
            var errText = "";
            var isExceptionThrown = false;
            var wcfClient = new SystemServiceClient();

            try
            {
                // args = new string[] { "ry"};
                // connect to the APAS.
                wcfClient.__SSC_Connect();

                var helpText = new StringBuilder();
                var stream = new StringWriter(helpText);
                var parser = new Parser(config =>
                {
                    config.EnableDashDash = true;
                    config.CaseSensitive = false;
                    config.HelpWriter = stream;
                });

                parser.ParseArguments<CalRYOptions, CalRXOptions>(args)
                    .MapResult(
                        (CalRYOptions opts) =>
                        {
                            UserProc(wcfClient, opts: opts);
                            return 0;
                        },
                        (CalRXOptions opts) =>
                        {
                            UserProc(wcfClient, opts: opts);
                            return 0;
                        },
                        errs =>
                        {
                            var errmsg = "";

                            if (errs.IsHelp() || errs.IsVersion())
                                errmsg = "";
                            else
                                errmsg = "脚本启动参数错误。\r\n";

                            wcfClient.__SSC_LogError(errmsg + helpText.ToString());
                            throw new Exception(errmsg + helpText.ToString());
                        });
            }
            catch (AggregateException ae)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Red;

                var ex = ae.Flatten();

                ex.InnerExceptions.ToList().ForEach(e =>
                {
                    Console.WriteLine($"Error occurred, {e.Message}");
                });

                Console.ResetColor();
            }
            catch (TimeoutException timeProblem)
            {
                errText = "The service operation timed out. " + timeProblem.Message;
                Console.Error.WriteLine(errText);
                isExceptionThrown = true;
            }
            // Catch unrecognized faults. This handler receives exceptions thrown by WCF
            // services when ServiceDebugBehavior.IncludeExceptionDetailInFaults
            // is set to true.
            catch (FaultException faultEx)
            {
                errText = "An unknown exception was received. "
                          + faultEx.Message
                          + faultEx.StackTrace;
                Console.Error.WriteLine(errText);
                isExceptionThrown = true;
            }
            // Standard communication fault handler.
            catch (CommunicationException commProblem)
            {
                errText = "There was a communication problem. " + commProblem.Message + commProblem.StackTrace;
                Console.Error.WriteLine(errText);
            }
            catch (Exception ex)
            {
                errText = ex.Message;
                Console.Error.WriteLine(errText);
                isExceptionThrown = true;
            }
            finally
            {
                wcfClient.Abort();
                if (isExceptionThrown)
                {
                    // try to output the error message to the log.
                    try
                    {
                        using (wcfClient = new SystemServiceClient())
                        {
                            wcfClient.__SSC_LogError(errText);
                            wcfClient.Abort();
                        }
                    }
                    catch (Exception)
                    {
                        // ignore
                    }


                    Environment.ExitCode = -1;
                }
            }
        }
    }
}
