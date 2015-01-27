using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacySolution.Core;

namespace UnitTestProject
{
    [TestClass]
    public class AutoMapperTestClass
    {
        [TestMethod]
        public void TestAotoMapper()
        {
            var pharmacy = new Pharmacy()
            {
                Address = "Test", 
                Number = "Test",
                OpenDate = DateTime.Now, 
                PhoneNumber = "60606060"
            };
            var jhhj = Mapper.Map<Pharmacy, NewPharmacy>(pharmacy);
            Mapper.CreateMap<Pharmacy, NewPharmacy>();
            var ph = Mapper.Map<Pharmacy>(pharmacy);
        }
    }
}
