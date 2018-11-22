using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Exebite.Common.Reflection
{
    public static class Methods
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }
    }
}
