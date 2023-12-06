﻿using System;

namespace XStart.Bean {
    internal class TableParam : Attribute {
        public readonly bool isKey;
        public readonly string param;
        public readonly string type;

        public TableParam(string param, string type) {
            this.param = param;
            this.type = type;
        }

        public TableParam(bool isKey, string param, string type) {
            this.isKey = isKey;
            this.param = param;
            this.type = type;
        }
    }
}
