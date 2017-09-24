using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace blog_webapi.Utility
{
    public class JsonStaticMethod
    {
        public static JsonResult Nothing() {
            return new JsonResult(new { error = "nothing" });
        }
    }
}
