using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Utils;
using XStart2._0.Bean;

namespace XStart2._0.Utils {
    class GradientColorUtils {

        public static List<GradientColor> GetList(string gradientColor) {
            List<GradientColor> list = new List<GradientColor>();
            if (!string.IsNullOrEmpty(gradientColor)) {
                string[] gradientColorsArray = gradientColor.Split(';');
                foreach (string gradientColorStr in gradientColorsArray) {
                    if (gradientColorStr.Contains(":")) {
                        string[] gradientColorArray = gradientColorStr.Split(':');
                        float point = Convert.ToSingle(gradientColorArray[1]);
                        string colorHtml = gradientColorArray[0];
                        list.Add(new GradientColor(colorHtml, point));
                    }
                }
            }
            return list;
        }

        public static double GetAngle(string gradientColor) {
            double angle = 0D;
            if (!string.IsNullOrEmpty(gradientColor)) {
                string[] gradientColorsArray = gradientColor.Split(';');
                // 取最后一个值
                string angleStr = gradientColorsArray[gradientColorsArray.Length - 1];
                if (!angleStr.Contains(":") && NumberUtils.IsNumeric(angleStr, out angle)) {
                    // 是double类型，直接out赋值了
                }
            }
            return angle;
        }

        public static string GetString(ICollection<GradientColor> gradientColorList, double angle) {
            string gradientColorStr = string.Empty;
            foreach (GradientColor gradientColor in gradientColorList) {
                if (gradientColorStr.Length > 0) {
                    gradientColorStr += ";";
                }
                gradientColorStr += $"{gradientColor.Color}:{gradientColor.Point}";
            }
            return gradientColorStr + ";" + Math.Round(angle, 0);
        }

        public static Brush GetBrush(string gradient) {
            Brush brush;
            if (string.IsNullOrEmpty(gradient)) {
                brush = new SolidColorBrush(Colors.Transparent);
            } else {
                string[] colorArray = gradient.Split(';');
                if (colorArray.Length > 1) {
                    // 渐变色 color1:point1;colo2:point2;angle，如果配置的渐变有问题，则对gradients里的颜色数进行判断
                    GradientStopCollection gradients = new GradientStopCollection();
                    double angle = 0D;
                    for (int i = 0; i < colorArray.Length; i++) {
                        string colorSingle = colorArray[i];
                        // 如果最末尾非旋转角度，则判定为颜色
                        if ((i < colorArray.Length - 1 || !NumberUtils.IsNumeric(colorSingle, out angle)) && colorSingle.Contains(":")) {
                            string[] gradientColor = colorSingle.Split(':');
                            gradients.Add(new GradientStop(ColorUtils.GetColor(gradientColor[0]), Convert.ToDouble(gradientColor[1])));
                        }
                    }
                    switch (gradients.Count) {
                        case 0:
                            // 没有颜色
                            brush = ColorUtils.GetBrush(Colors.Transparent);
                            break;
                        case 1:
                            // 1个颜色
                            brush = new SolidColorBrush(gradients[0].Color);
                            break;
                        default:
                            // 渐变色
                            // 根据角度计算start point和end point
                            Point startPoint, endPoint;
                            if ((angle >= 0 && angle < 45) || (angle >= 315 && angle <= 360)) {
                                double arcAngle = angle * Math.PI / 180;
                                double offset = Math.Tan(arcAngle) * 0.5;
                                startPoint = new Point(0, 0.5 - offset);
                                endPoint = new Point(1, 0.5 + offset);
                            } else if (angle >= 45 && angle < 135) {
                                double arcAngle = (90 - angle) * Math.PI / 180; ;
                                double offset = Math.Tan(arcAngle) * 0.5;
                                startPoint = new Point(0.5 - offset, 0);
                                endPoint = new Point(0.5 + offset, 1);
                            } else if (angle >= 135 && angle < 225) {
                                double arcAngle = (180 - angle) * Math.PI / 180; ;
                                double offset = Math.Tan(arcAngle) * 0.5;
                                startPoint = new Point(1, 0.5 - offset);
                                endPoint = new Point(0, 0.5 + offset);
                            } else {
                                // 225-315
                                double arcAngle = (270 - angle) * Math.PI / 180; ;
                                double offset = Math.Tan(arcAngle) * 0.5;
                                startPoint = new Point(0.5 + offset, 1);
                                endPoint = new Point(0.5 - offset, 0);
                            }
                            brush = new LinearGradientBrush(gradients) { StartPoint = startPoint, EndPoint = endPoint };
                            break;
                    }
                } else {
                    // 单色
                    brush = ColorUtils.GetBrush(colorArray[0]);
                }
            }
            return brush;
        }
    }
}
