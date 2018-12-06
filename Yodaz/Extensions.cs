using System;
using System.Threading.Tasks;
namespace Yodaz
{
    public static class Extensions
    {
        public static void IgnoreResult(this Task task)
        {
            // This just silences the warnings when tasks are not awaited.
        }

    }
}