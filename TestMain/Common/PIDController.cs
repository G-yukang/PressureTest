using System;

namespace TestMain.Common
{
    public class PIDController
    {
        // PID控制器的参数
        private double Kp; // 比例系数
        private double Ki; // 积分时间
        private double Kd; // 微分时间

        // 上一次的误差和积分值
        private double prevError; // 上一次误差
        private double integral; // 积分值

        private double learningRate;
        // 输出限制参数
        private double outputMin; // 输出最小值
        private double outputMax; // 输出最大值

        // 抗积分饱和参数
        private bool antiWindup; // 是否启用抗积分饱和

        /// <summary>
        /// 构造函数，初始化PID控制器的参数
        /// </summary>
        /// <param name="Kp">比例系数</param>
        /// <param name="Ki">积分时间</param>
        /// <param name="Kd">微分时间</param>
        /// <param name="outputMin">输出最小值</param>
        /// <param name="outputMax">输出最大值</param>
        /// <param name="antiWindup">是否启用抗积分饱和</param>
        public PIDController(double Kp, double Ki, double Kd, double outputMin = double.MinValue, double outputMax = double.MaxValue, bool antiWindup = true, double learningRate = 0)
        {
            this.Kp = Kp;
            this.Ki = Ki;
            this.Kd = Kd;

            // 初始化上一次的误差和积分值
            prevError = 0;
            integral = 0;
            this.learningRate = learningRate;
            // 初始化输出限制参数
            this.outputMin = outputMin;
            this.outputMax = outputMax;

            // 初始化抗积分饱和参数
            this.antiWindup = antiWindup;
        }

        /// <summary>
        /// 计算PID控制器的输出
        /// </summary>
        /// <param name="setpoint">设定值</param>
        /// <param name="processValue">过程值</param>
        /// <returns>PID控制器的输出</returns>
        public double CalculateOutput(double setpoint, double processValue)
        {
            double error = setpoint - processValue;
            integral += error;
            double derivative = error - prevError;

            // 自适应调整PID参数
            Kp += learningRate * error;
            Ki += learningRate * integral;
            Kd += learningRate * derivative;

            double output = Kp * error + Ki * integral + Kd * derivative;

            prevError = error;

            //double error = setpoint - processValue;

            //integral += error;
            //double derivative = error - prevError;

            //double output = Kp * error + Ki * integral + Kd * derivative;

            //// 限制输出
            //output = Math.Max(outputMin, Math.Min(outputMax, output));

            //// 抗积分饱和
            //if (antiWindup)
            //{
            //    if ((output == outputMax && error > 0) || (output == outputMin && error < 0))
            //    {
            //        integral -= error; // 积分抗饱和
            //    }
            //}

            //prevError = error;

            return output;
        }

        /// <summary>
        /// 更新PID控制器的参数
        /// </summary>
        /// <param name="newKp">新的比例系数</param>
        /// <param name="newKi">新的积分时间</param>
        /// <param name="newKd">新的微分时间</param>
        public void UpdateParameters(double newKp, double newKi, double newKd)
        {
            // 更新PID参数
            Kp = newKp;
            Ki = newKi;
            Kd = newKd;
        }

        /// <summary>
        /// 更新输出限制参数
        /// </summary>
        /// <param name="newOutputMin">新的输出最小值</param>
        /// <param name="newOutputMax">新的输出最大值</param>
        public void UpdateOutputLimits(double newOutputMin, double newOutputMax)
        {
            outputMin = newOutputMin;
            outputMax = newOutputMax;
        }
    }
}
