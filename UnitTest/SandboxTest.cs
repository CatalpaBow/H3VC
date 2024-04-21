using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace UnitTest
{
    public class SandboxTest{
        private readonly ITestOutputHelper _output;

        public SandboxTest(ITestOutputHelper output) {
            _output = output;
        }
        [Fact]
        public void LoopTest() {
            int i = 0;
            for(i = 0; i < 10; i++) {

            }
            _output.WriteLine(i.ToString());

        }
    }
}
