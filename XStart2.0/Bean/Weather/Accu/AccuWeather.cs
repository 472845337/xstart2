
namespace XStart2._0.Bean.Weather.Accu {
    public class AccuWeather {
        public class ValueBean {
            public double Value { get; set; }
            public string Unit { get; set; }
            public int UnitType { get; set; }
            public string Phrase { get; set; }
        }

        public class WindBean {
            public UnitBean Speed { get; set; }
            public DirectionBean Direction { get; set; }
        }

        public class WindBeanNoUnit {
            // 风速，单位是km/h
            public ValueBean Speed { get; set; }
            public DirectionBean Direction { get; set; }
        }

        public class UnitBean {
            public ValueBean Metric { get; set; }
            public ValueBean Imperial { get; set; }
        }

        public class DirectionBean {
            public int Degrees { get; set; }
            public string Localized { get; set; }
            public string English { get; set; }
        }

        public class TemperatureRangeBean {
            public UnitBean Minimum { get; set; }
            public UnitBean Maximum { get; set; }
        }

        public class TemperatureRangeBeanNoUnit {
            public ValueBean Minimum { get; set; }
            public ValueBean Maximum { get; set; }
        }
    }
}
