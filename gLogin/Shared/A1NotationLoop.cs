using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gLogin.Shared
{
    public static class A1NotationLoop
    {
        public static string GetPrevious(string current)
        {
            var last = current.Reverse().ToArray();
            if(last[0] != 'A')
            {
                last[0]--;
                return new String(last.Reverse().ToArray());
            }
            if(last[0] == 'A')
            {
                if(last.Count() == 1)
                {
                    return "";
                }
                var next = new String(last.Skip(1).ToArray());
                var result =new String( ("Z" + GetPrevious(next)).Reverse().ToArray());
                return result;
            }

            return "";
        }
        
        
    }
}