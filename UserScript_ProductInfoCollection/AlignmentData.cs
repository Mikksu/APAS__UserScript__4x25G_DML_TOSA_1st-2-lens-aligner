using System;

namespace UserScript
{
    public class AlignmentData
    {
        public string Sn { get; set; }

        public string Pn { get; set; }

        public string Traveler { get; set; }

        public string WorkOrder { get; set; }

        public string Operator { get; set; }


        #region  #region LD Lens 在 CH3 的耦合参数
        public double LD_Lens_Hight_Ref_CH3 { get; set; }

        public double CollimatorX_After_Align_CH3 { get; set; }

        public double CollimatorY_After_Align_CH3 { get; set; }

        public double LD_Lens_X_After_Align_CH3 { get; set; }

        public double LD_Lens_Y_After_Align_CH3 { get; set; }

        public double LD_Lens_Z_After_Align_CH3 { get; set; }

        public double LD_Lens_Power_After_Align_CH3 { get; set; }

        public double LD_Lens_To_Base_Gap_CH3 { get; set; }

        #endregion


        #region  #region LD Lens 在 CH0 的耦合参数

        public double LD_Lens_Hight_Ref_CH0 { get; set; }

        public double CollimatorX_After_Align_CH0 { get; set; }

        public double CollimatorY_After_Align_CH0 { get; set; }

        public double LD_Lens_X_After_Align_CH0 { get; set; }

        public double LD_Lens_Y_After_Align_CH0 { get; set; }

        public double LD_Lens_Z_After_Align_CH0 { get; set; }

        public double LD_Lens_Power_After_Align_CH0 { get; set; }

        public double LD_Lens_To_Base_Gap_CH0 { get; set; }

        #endregion

        #region 准直器转角度参数

        public double Collimator_X_Diff { get; set; }

        public double Collimator_Y_Diff { get; set; }

        public double Collimator_RX_Before_Tuning { get; set; }

        public double Collimator_RY_Before_Tuning { get; set; }

        public double Collimator_RX { get; set; }

        public double Collimator_RY { get; set; }

        public double Collimator_RX_After_Tuning { get; set; }

        public double Collimator_RY_After_Tuning { get; set; }

        #endregion

        #region LD Lens UV 参数

        public double LD_Lens_X_Before_UV { get; set; }

        public double LD_Lens_Y_Before_UV { get; set; }

        public double LD_Lens_Z_Before_UV { get; set; }

        public double LD_Lens_Power_Before_UV { get; set; }

        public double LD_Lens_Power_After_UV { get; set; }

        public double LD_Lens_Power_Grip_Released { get; set; }

        #endregion

        #region Fiber Lens 耦合参数

        public double Fiber_Lens_Hight_Ref { get; set; }

        public double Fiber_Lens_Power_After_Align { get; set; }

        public double Fiber_Lens_X_After_Align { get; set; }

        public double Fiber_Lens_Y_After_Align { get; set; }

        public double Fiber_Lens_Z_After_Align { get; set; }

        public double Fiber_Lens_X_Before_UV { get; set; }

        public double Fiber_Lens_Y_Before_UV { get; set; }

        public double Fiber_Lens_Z_Before_UV { get; set; }

        public double Fiber_Lens_Power_Before_UV { get; set; }

        public double Fiber_Lens_To_Base_Gap { get; set; }

        public double Fiber_Lens_Power_After_UV { get; set; }

        public double Fiber_Lens_Power_Grip_Released { get; set; }

        #endregion


        public DateTime Time { get; set; }
    }
}
