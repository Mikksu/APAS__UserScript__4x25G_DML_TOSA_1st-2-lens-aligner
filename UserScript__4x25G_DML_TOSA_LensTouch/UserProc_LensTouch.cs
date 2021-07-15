using System;
using System.Diagnostics;
using System.Threading;
using UserScript.CamRAC;
using UserScript.SystemService;

namespace UserScript
{
    internal partial class APAS_UserScript
    {
        #region User Process

        /// <summary>
        ///     The section of the user process.
        ///     用户自定义流程函数。
        ///     Please write your process in the following method.
        ///     请在以下函数中定义您的工艺流程。
        /// </summary>
        /// <param name="apas"></param>
        /// <returns></returns>
        private static void UserProc(SystemServiceClient apas, CamRemoteAccessContractClient cam = null,
            Options opts = null)
        {
            try
            {
                double totalMoved = 0;
                var initVolt = apas.__SSC_MeasurableDevice_Read(opts.SensorName);
                
                while(true)
                {
                    apas.__SSC_MoveAxis(opts.LMU, opts.Axis, SSC_MoveMode.REL, opts.FeedInSpeed, opts.FeedInStep);
                    totalMoved += opts.FeedInStep;

                    var volt = apas.__SSC_MeasurableDevice_Read(opts.SensorName);
                    if (Math.Abs(volt - initVolt) >= opts.SensorVoltageDiff)
                        break;

                    if (Math.Abs(totalMoved) > opts.FeedInLimit)
                        throw new Exception($"Lens探底失败，在最大行程[{opts.FeedInLimit}]内没有找到Lens和基板接触位置。");
                }

                totalMoved = 0;
                if(!opts.SkipReturnToOrg)
                {
                    while(true)
                    {
                        apas.__SSC_MoveAxis(opts.LMU, opts.Axis, SSC_MoveMode.REL, opts.FeedInSpeed, -opts.FeedInStep);
                        totalMoved += (-opts.FeedInStep);

                        var volt = apas.__SSC_MeasurableDevice_Read(opts.SensorName);
                        if (Math.Abs(volt - initVolt) <= 5)
                            break;

                        if (Math.Abs(totalMoved) > opts.FeedInLimit)
                            throw new Exception($"在最大行程[{opts.FeedInLimit}]内没有找到压力传感器恢复初始电压[{initVolt:F1}]的位置，返回Lens探底原点失败。");
                    }
                }
            }
            catch (Exception ex)
            {
                //Apas.__SSC_LogError(ex.Message);
                throw new Exception(ex.Message);
            }

            // Thread.Sleep(100);
        }

        #endregion

    }
}