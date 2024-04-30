using XStart2._0.Bean.Weather.Open;

namespace XStart2._0.Bean.Weather {
    class WeatherUtil {
        public static string GetWeatherImg(string weather) {
            string weaImg = "qing";
            switch (weather) {
                case "晴":
                    weaImg = "qing";
                    break;
                case "阴":
                    weaImg = "yin";
                    break;
                case "冰雹":
                    weaImg = "bingbao";
                    break;
                case "风":
                    weaImg = "feng";
                    break;
                case "雷阵雨":
                    weaImg = "lei";
                    break;
                case "霾":
                    weaImg = "mai";
                    break;
                case "雾":
                    weaImg = "wu";
                    break;
                case "雪":
                    weaImg = "xue";
                    break;
                case "阵雨":
                case "雨":
                case "小雨":
                case "中雨":
                case "大雨":
                    weaImg = "yu";
                    break;
                case "多云":
                    weaImg = "yun";
                    break;
            }
            return weaImg;
        }

        public static double GetAirFromOpenAir(OpenAir air) {
            double airAqi = 0;
            double co = air.AirList[0].Components.Co;
            double no = air.AirList[0].Components.No;
            double no2 = air.AirList[0].Components.No2;
            double o3 = air.AirList[0].Components.O3;
            double so2 = air.AirList[0].Components.So2;
            double pm25 = air.AirList[0].Components.Pm25;
            double pm10 = air.AirList[0].Components.Pm10;
            double nh3 = air.AirList[0].Components.Nh3;
            // 一氧化碳
            double coMg = co / 1000;
            if (coMg <= 10) {
                airAqi += coMg * 10;
            } else if (coMg <= 35) {
                airAqi += 100 + (coMg - 10) * 2;
            } else if (co / 1000 <= 60) {
                airAqi += 150 + ((coMg - 35) * 2);
            } else if (co / 1000 <= 90) {
                airAqi += 200 + ((coMg - 60) / 3 * 10);
            } else if (co / 1000 <= 120) {
                airAqi += 300 + ((coMg - 90) / 3 * 10);
            } else if (co / 1000 <= 150) {
                airAqi += 400 + ((coMg - 120) / 3 * 10);
            }
            // 一氧化氮
            if (no <= 200) {
                airAqi += no / 2;
            } else if (no <= 700) {
                airAqi += 100 + (no - 200) / 10;
            } else if (no <= 1200) {
                airAqi += 150 + (no - 700) / 10;
            } else if (no <= 2340) {
                airAqi += 200 + (no - 1200) / 114 * 10;
            } else if (no <= 3090) {
                airAqi += 300 + (no - 2340) / 75 * 10;
            } else if (no <= 3840) {
                airAqi += 400 + (no - 3090) / 75 * 10;
            }
            // 二氧化氮
            if (no2 <= 200) {
                airAqi += no2 / 2;
            } else if (no2 <= 700) {
                airAqi += 100 + (no2 - 200) / 10;
            } else if (no2 <= 1200) {
                airAqi += 150 + (no2 - 700) / 10;
            } else if (no2 <= 2340) {
                airAqi += 200 + (no2 - 1200) / 57 * 5;
            } else if (no2 <= 3090) {
                airAqi += 300 + (no2 - 2340) / 15 * 2;
            } else if (no2 <= 3840) {
                airAqi += 400 + (no2 - 3090) / 15 * 2;
            }
            // 臭氧
            if (o3 <= 160) {
                airAqi += o3 / 16 * 5;
            } else if (o3 <= 200) {
                airAqi += 50 + (o3 - 160) / 4 * 5;
            } else if (o3 <= 300) {
                airAqi += 100 + (o3 - 200) / 2;
            } else if (o3 <= 400) {
                airAqi += 150 + (o3 - 300) / 2;
            } else if (o3 <= 800) {
                airAqi += 200 + (o3 - 400) / 4;
            } else if (o3 <= 1000) {
                airAqi += 300 + (o3 - 800) / 2;
            } else if (o3 <= 1200) {
                airAqi += 300 + (o3 - 1000) / 2;
            }
            // 二氧化硫
            if (so2 <= 150) {
                airAqi += so2 / 3;
            } else if (so2 <= 500) {
                airAqi += 50 + (so2 - 150) / 7;
            } else if (so2 <= 650) {
                airAqi += 100 + (so2 - 500) / 3;
            } else if (so2 <= 800) {
                airAqi += 100 + (so2 - 650) / 3;
            }
            // pm 2.5
            if (pm25 <= 35) {
                airAqi += pm25 / 7 * 10;
            } else if (pm25 <= 75) {
                airAqi += 50 + (pm25 - 35) / 4 * 5;
            } else if (pm25 <= 115) {
                airAqi += 100 + (pm25 - 75) / 4 * 5;
            } else if (pm25 <= 150) {
                airAqi += 150 + (pm25 - 115) / 7 * 10;
            } else if (pm25 <= 250) {
                airAqi += 200 + pm25 - 150;
            } else if (pm25 <= 350) {
                airAqi += 300 + pm25 - 250;
            } else if (pm25 <= 500) {
                airAqi += 400 + (pm25 - 350) / 3 * 2;
            }
            // pm10
            if (pm10 <= 50) {
                airAqi += pm10;
            } else if (pm10 <= 150) {
                airAqi += 50 + (pm10 - 50) / 2;
            } else if (pm10 <= 250) {
                airAqi += 100 + (pm10 - 150) / 2;
            } else if (pm10 <= 350) {
                airAqi += 150 + (pm10 - 250) / 2;
            } else if (pm10 <= 420) {
                airAqi += 200 + (pm10 - 350) / 7 * 10;
            } else if (pm10 <= 500) {
                airAqi += 300 + (pm10 - 420) / 8 * 10;
            } else if (pm10 <= 600) {
                airAqi += 400 + pm10 - 500;
            }
            return airAqi;
        }
    }
}
