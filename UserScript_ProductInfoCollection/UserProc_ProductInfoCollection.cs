using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        /// <param name="camera"></param>
        /// <param name="opts"></param>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        private static void UserProc(ISystemService apas, CamRemoteAccessContractClient camera = null,
            Option opts = null)
        {
            if (opts == null) throw new ArgumentException(nameof(opts));

            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var folder = string.IsNullOrEmpty(opts.FolderName) ? "100G TOSA 耦合数据" : opts.FolderName;

            var fullFolderName = Path.Combine(desktopPath, folder);
            if (Directory.Exists(fullFolderName) == false)
                Directory.CreateDirectory(fullFolderName);

            var fullname = Path.Combine(fullFolderName, $"{opts.FilenamePrefix}-{DateTime.Now:yyyy_MM_dd}.csv");

            var records = new List<AlignmentData>();
            var data = new AlignmentData();
            /*            
            data.Sn = ReadVariable<string>(apas.__SSC_ReadVariable, "__SN");
            data.Pn = ReadVariable<string>(apas.__SSC_ReadVariable, "__PN");
            data.Traveler = ReadVariable<string>(apas.__SSC_ReadVariable, "__TC");
            data.WorkOrder = ReadVariable<string>(apas.__SSC_ReadVariable, "__WO");
            data.Operator = ReadVariable<string>(apas.__SSC_ReadVariable, "__OP");
            
            data.LD_Lens_Hight_Ref_CH3 = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_H_ORG_CH3", double.Parse);
            data.CollimatorX_After_Align_CH3 = ReadVariable<double>(apas.__SSC_ReadVariable, "COLL_X_AF_AL_CH3", double.Parse);
            data.CollimatorY_After_Align_CH3 = ReadVariable<double>(apas.__SSC_ReadVariable, "COLL_Y_AF_AL_CH3", double.Parse);
            data.LD_Lens_X_After_Align_CH3 = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_X_AF_AL_CH3", double.Parse);
            data.LD_Lens_Y_After_Align_CH3 = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_Y_AF_AL_CH3", double.Parse);
            data.LD_Lens_Z_After_Align_CH3 = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_Z_AF_AL_CH3", double.Parse);
            data.LD_Lens_Power_After_Align_CH3 = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_P_AF_AL_CH3", double.Parse);
            data.LD_Lens_To_Base_Gap_CH3 = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_H_CH3", double.Parse);

            data.LD_Lens_Hight_Ref_CH0 = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_H_ORG_CH0", double.Parse);
            data.CollimatorX_After_Align_CH0 = ReadVariable<double>(apas.__SSC_ReadVariable, "COLL_X_AF_AL_CH0", double.Parse);
            data.CollimatorY_After_Align_CH0 = ReadVariable<double>(apas.__SSC_ReadVariable, "COLL_Y_AF_AL_CH0", double.Parse);
            data.LD_Lens_X_After_Align_CH0 = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_X_AF_AL_CH0", double.Parse);
            data.LD_Lens_Y_After_Align_CH0 = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_Y_AF_AL_CH0", double.Parse);
            data.LD_Lens_Z_After_Align_CH0 = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_Z_AF_AL_CH0", double.Parse);
            data.LD_Lens_Power_After_Align_CH0 = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_P_AF_AL_CH0", double.Parse);
            data.LD_Lens_To_Base_Gap_CH0 = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_H_CH0", double.Parse);

            data.Collimator_X_Diff = ReadVariable<double>(apas.__SSC_ReadVariable, "COLL_X_DIFF", double.Parse);
            data.Collimator_Y_Diff = ReadVariable<double>(apas.__SSC_ReadVariable, "COLL_Y_DIFF", double.Parse);
            data.Collimator_RX_Before_Tuning = ReadVariable<double>(apas.__SSC_ReadVariable, "COLL_RX_BF_TU", double.Parse);
            data.Collimator_RY_Before_Tuning = ReadVariable<double>(apas.__SSC_ReadVariable, "COLL_RY_BF_TU", double.Parse);
            data.Collimator_RX = ReadVariable<double>(apas.__SSC_ReadVariable, "COLL_RX", double.Parse);
            data.Collimator_RY = ReadVariable<double>(apas.__SSC_ReadVariable, "COLL_RY", double.Parse);
            data.Collimator_RX_After_Tuning = ReadVariable<double>(apas.__SSC_ReadVariable, "COLL_RX_AF_TU", double.Parse);
            data.Collimator_RY_After_Tuning = ReadVariable<double>(apas.__SSC_ReadVariable, "COLL_RY_AF_TU", double.Parse);

            data.LD_Lens_X_Before_UV = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_X_BF_UV", double.Parse);
            data.LD_Lens_Y_Before_UV = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_Y_BF_UV", double.Parse);
            data.LD_Lens_Z_Before_UV = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_Z_BF_UV", double.Parse);
            data.LD_Lens_Power_Before_UV = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_P_BF_UV", double.Parse);
            data.LD_Lens_Power_Before_UV = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_P_AF_UV", double.Parse);
            data.LD_Lens_Power_Grip_Released = ReadVariable<double>(apas.__SSC_ReadVariable, "LD_LENS_P_GRIP_RELEASED", double.Parse);

            data.Fiber_Lens_Hight_Ref = ReadVariable<double>(apas.__SSC_ReadVariable, "FIB_LENS_H_ORG", double.Parse);
            data.Fiber_Lens_Power_After_Align = ReadVariable<double>(apas.__SSC_ReadVariable, "FIB_LENS_P_AF_AL", double.Parse);
            data.Fiber_Lens_X_After_Align = ReadVariable<double>(apas.__SSC_ReadVariable, "FIB_LENS_X_AF_AL", double.Parse);
            data.Fiber_Lens_Y_After_Align = ReadVariable<double>(apas.__SSC_ReadVariable, "FIB_LENS_Y_AF_AL", double.Parse);
            data.Fiber_Lens_Z_After_Align = ReadVariable<double>(apas.__SSC_ReadVariable, "FIB_LENS_Z_AF_AL", double.Parse);

            data.Fiber_Lens_X_Before_UV = ReadVariable<double>(apas.__SSC_ReadVariable, "FIB_LENS_X_BF_UV", double.Parse);
            data.Fiber_Lens_Y_Before_UV = ReadVariable<double>(apas.__SSC_ReadVariable, "FIB_LENS_Y_BF_UV", double.Parse);
            data.Fiber_Lens_Z_Before_UV = ReadVariable<double>(apas.__SSC_ReadVariable, "FIB_LENS_Z_BF_UV", double.Parse);
            data.Fiber_Lens_Power_Before_UV = ReadVariable<double>(apas.__SSC_ReadVariable, "FIB_LENS_P_BF_UV", double.Parse);
            data.Fiber_Lens_To_Base_Gap = ReadVariable<double>(apas.__SSC_ReadVariable, "FIB_LENS_H", double.Parse);

            data.Fiber_Lens_Power_After_UV = ReadVariable<double>(apas.__SSC_ReadVariable, "FIB_LENS_P_AF_UV", double.Parse);
            data.Fiber_Lens_Power_Grip_Released = ReadVariable<double>(apas.__SSC_ReadVariable, "FIB_LENS_P_GRIP_RELEASED", double.Parse);
            */

            data.Sn = ReadVariable<string>("__SN");
            data.Pn = ReadVariable<string>("__PN");
            data.Traveler = ReadVariable<string>("__TC");
            data.WorkOrder = ReadVariable<string>("__WO");
            data.Operator = ReadVariable<string>("__OP");

            data.LD_Lens_Hight_Ref_CH3 = ReadVariable<double>("LD_LENS_H_ORG_CH3", double.Parse);
            data.CollimatorX_After_Align_CH3 = ReadVariable<double>("COLL_X_AF_AL_CH3", double.Parse);
            data.CollimatorY_After_Align_CH3 = ReadVariable<double>("COLL_Y_AF_AL_CH3", double.Parse);
            data.LD_Lens_X_After_Align_CH3 = ReadVariable<double>("LD_LENS_X_AF_AL_CH3", double.Parse);
            data.LD_Lens_Y_After_Align_CH3 = ReadVariable<double>("LD_LENS_Y_AF_AL_CH3", double.Parse);
            data.LD_Lens_Z_After_Align_CH3 = ReadVariable<double>("LD_LENS_Z_AF_AL_CH3", double.Parse);
            data.LD_Lens_Power_After_Align_CH3 = ReadVariable<double>("LD_LENS_P_AF_AL_CH3", double.Parse);
            data.LD_Lens_To_Base_Gap_CH3 = ReadVariable<double>("LD_LENS_H_CH3", double.Parse);

            data.LD_Lens_Hight_Ref_CH0 = ReadVariable<double>("LD_LENS_H_ORG_CH0", double.Parse);
            data.CollimatorX_After_Align_CH0 = ReadVariable<double>("COLL_X_AF_AL_CH0", double.Parse);
            data.CollimatorY_After_Align_CH0 = ReadVariable<double>("COLL_Y_AF_AL_CH0", double.Parse);
            data.LD_Lens_X_After_Align_CH0 = ReadVariable<double>("LD_LENS_X_AF_AL_CH0", double.Parse);
            data.LD_Lens_Y_After_Align_CH0 = ReadVariable<double>("LD_LENS_Y_AF_AL_CH0", double.Parse);
            data.LD_Lens_Z_After_Align_CH0 = ReadVariable<double>("LD_LENS_Z_AF_AL_CH0", double.Parse);
            data.LD_Lens_Power_After_Align_CH0 = ReadVariable<double>("LD_LENS_P_AF_AL_CH0", double.Parse);
            data.LD_Lens_To_Base_Gap_CH0 = ReadVariable<double>("LD_LENS_H_CH0", double.Parse);

            data.Collimator_X_Diff = ReadVariable<double>("COLL_X_DIFF", double.Parse);
            data.Collimator_Y_Diff = ReadVariable<double>("COLL_Y_DIFF", double.Parse);
            data.Collimator_RX_Before_Tuning = ReadVariable<double>("COLL_RX_BF_TU", double.Parse);
            data.Collimator_RY_Before_Tuning = ReadVariable<double>("COLL_RY_BF_TU", double.Parse);
            data.Collimator_RX = ReadVariable<double>("COLL_RX", double.Parse);
            data.Collimator_RY = ReadVariable<double>("COLL_RY", double.Parse);
            data.Collimator_RX_After_Tuning = ReadVariable<double>("COLL_RX_AF_TU", double.Parse);
            data.Collimator_RY_After_Tuning = ReadVariable<double>("COLL_RY_AF_TU", double.Parse);

            data.LD_Lens_X_Before_UV = ReadVariable<double>("LD_LENS_X_BF_UV", double.Parse);
            data.LD_Lens_Y_Before_UV = ReadVariable<double>("LD_LENS_Y_BF_UV", double.Parse);
            data.LD_Lens_Z_Before_UV = ReadVariable<double>("LD_LENS_Z_BF_UV", double.Parse);
            data.LD_Lens_Power_Before_UV = ReadVariable<double>("LD_LENS_P_BF_UV", double.Parse);
            data.LD_Lens_Power_Before_UV = ReadVariable<double>("LD_LENS_P_AF_UV", double.Parse);
            data.LD_Lens_Power_Grip_Released = ReadVariable<double>("LD_LENS_P_GRIP_RELEASED", double.Parse);

            data.Fiber_Lens_Hight_Ref = ReadVariable<double>("FIB_LENS_H_ORG", double.Parse);
            data.Fiber_Lens_Power_After_Align = ReadVariable<double>("FIB_LENS_P_AF_AL", double.Parse);
            data.Fiber_Lens_X_After_Align = ReadVariable<double>("FIB_LENS_X_AF_AL", double.Parse);
            data.Fiber_Lens_Y_After_Align = ReadVariable<double>("FIB_LENS_Y_AF_AL", double.Parse);
            data.Fiber_Lens_Z_After_Align = ReadVariable<double>("FIB_LENS_Z_AF_AL", double.Parse);

            data.Fiber_Lens_X_Before_UV = ReadVariable<double>("FIB_LENS_X_BF_UV", double.Parse);
            data.Fiber_Lens_Y_Before_UV = ReadVariable<double>("FIB_LENS_Y_BF_UV", double.Parse);
            data.Fiber_Lens_Z_Before_UV = ReadVariable<double>("FIB_LENS_Z_BF_UV", double.Parse);
            data.Fiber_Lens_Power_Before_UV = ReadVariable<double>("FIB_LENS_P_BF_UV", double.Parse);
            data.Fiber_Lens_To_Base_Gap = ReadVariable<double>("FIB_LENS_H", double.Parse);

            data.Fiber_Lens_Power_After_UV = ReadVariable<double>("FIB_LENS_P_AF_UV", double.Parse);
            data.Fiber_Lens_Power_Grip_Released = ReadVariable<double>("FIB_LENS_P_GRIP_RELEASED", double.Parse);

            data.Time = DateTime.Now;

            records.Add(data);

            var hasHeader = File.Exists(fullname) == false;

            using (var writer = new StreamWriter(fullname, append: true))
            {
                using (var csv = new CsvWriter(writer,
                    new CsvConfiguration(cultureInfo: CultureInfo.InvariantCulture, hasHeaderRecord: hasHeader)))
                {
                    csv.WriteRecords(records);
                }
            }
        }

        #endregion

        #region Private Methods

        private static T ReadVariable<T>(string varName, Func<string, T> typeParser = null)
        {
            try
            {
                var apas = new SystemServiceClient();
                var ret = apas.__SSC_ReadVariable(varName);

                if (ret == null)
                    return default;

                if (typeParser == null)
                    return (T)ret;
                else
                    return typeParser(ret.ToString());

            }
            catch (Exception ex)
            {
                //throw new InvalidOperationException($"无法读取变量 [{varName}]({ret})，{ex.Message}");
                return default;
            }
        }

        #endregion
    }
}