using System.Threading;
using MolecularLib.Helpers;
using NUnit.Framework;

namespace MolecularLibTests
{
    public class UtilsTests
    {
        [Test]
        public void TimedScopeTest()
        {
            Thread.Sleep(100);
            
            using (var _ = new TimedScope("Test completed"))
            {
                Thread.Sleep(500);
            }
        }
    }
}