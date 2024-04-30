using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XStart2._0.Utils {
    class WindUtil {
        public static string Deg2Direc(int deg) {
            string direc = "";
            if (deg > 337 || deg <= 22) {
                direc = "北";
            } else if (deg > 22 && deg <= 67) {
                direc = "东北";
            } else if (deg > 67 && deg <= 112) {
                direc = "东";
            } else if (deg > 112 && deg <= 157) {
                direc = "东南";
            } else if (deg > 157 && deg <= 202) {
                direc = "南";
            } else if (deg > 202 && deg <= 247) {
                direc = "西南";
            } else if (deg > 247 && deg <= 292) {
                direc = "西";
            } else if (deg > 292 && deg <= 337) {
                direc = "西北";
            }
            return direc;
        }

        public static int Speed2Scale(double speed) {
            int scale = 0;
            if (speed < 0.2) {
                scale = 0;
            } else
            if (speed < 1.5) {
                scale = 1;
            } else if (speed < 3.3) {
                scale = 2;
            } else if (speed < 5.4) {
                scale = 3;
            } else if (speed < 7.9) {
                scale = 4;
            } else if (speed < 10.7) {
                scale = 5;
            } else if (speed < 13.8) {
                scale = 6;
            } else if (speed < 17.1) {
                scale = 7;
            } else if (speed < 20.7) {
                scale = 8;
            } else if (speed < 24.4) {
                scale = 9;
            } else if (speed < 28.4) {
                scale = 10;
            } else if (speed < 32.6) {
                scale = 11;
            } else if (speed >= 32.6) {
                scale = 12;
            }
            return scale;
        }
    }
}
