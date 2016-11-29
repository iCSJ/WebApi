using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaseApi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyModel;
using BaseModels;
using System.Transactions;

namespace BaseApi.DAL.Tests
{
    [TestClass()]
    public class GenericDBContextTests
    {
        [TestMethod()]
        public void PostTest()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var db = GenericDBContext.Instance;
                    db.Post(new User() { UserNo = "key0" });
                    db.Post(new User() { UserNo = "key1" });
                    scope.Complete();
                }
                Assert.Fail();
            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}