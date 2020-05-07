using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLib.Entities {
    public class EResponse {

        public EResponse SetErrorResponse(int errorCode) {
            return this;
        }
    }
}
