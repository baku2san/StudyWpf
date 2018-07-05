using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyWpf.Models
{
    public class TestRepository
    {
        private TestContext testContext = new TestContext();

        public IEnumerable<TestEntity> Tests()
        {
            return testContext.Items;
        }
        public IEnumerable<Test2Entity> Test2s(TestEntity test)
        {
            return testContext.Items.Where(w=>w.Id == test.Id)
                .Join(testContext.Item2s, item1 => item1.Id, item2=>item2.TestModelId, (item1, item2)=>  item2 );
        }
    }
}
