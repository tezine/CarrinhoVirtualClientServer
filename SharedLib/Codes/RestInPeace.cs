using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLib.Codes {
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class RestInPeaceEntity : System.Attribute {

        public RestInPeaceEntity() {
        }
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class RestInPeaceGet : Attribute {
        public RestInPeaceGet(int version, string signature) {
        }
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class RestInPeacePut : Attribute {
        public RestInPeacePut(int version, string signature) {
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class RestInPeacePost : Attribute {
        public RestInPeacePost(int version, string signature) {
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class RestInPeaceDelete : Attribute {
        public RestInPeaceDelete(int version, string signature) {
        }
    }
}
