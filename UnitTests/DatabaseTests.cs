using Effort;
using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdministrationPanel.DatabaseEntityFramework;
using System.Data.Common;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class DatabaseTests
    {
        private BusinessCodingModelContex _context;

        [TestInitialize]
        public void Initialize()
        {
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();

            _context = new BusinessCodingModelContex(connection);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }

        [TestMethod]
        public void TestAddTOoUsers()
        {
            using (var context = new BusinessCodingModelContex())
            {
                context.Users.Add(new Users()
                {
                    UserID = 100,
                    Login = "testUnit",
                    Password = "123",
                    Email = "testUnit@com.pl",
                    ActivationDate = DateTime.Parse("2013-11-05 22:12:28.990"),
                    LastLogin = DateTime.Parse("2013-11-05 22:12:28.990")
                });
            }
        }

        [TestMethod]
        public void TestAddToCompanies()
        {
            using (var context = new BusinessCodingModelContex())
            {
                Companies tmp = new Companies();
                tmp.CostOfLiving = 12;
                tmp.Money = 3242;
                tmp.Name = "testUnit";
                tmp.Respect = 12;
                var result1 = from x in context.TypesOfOffices where x.Name == "Duże biuro" select x;
                tmp.TypesOfOfficesTOOfficeID = result1.First().TOOfficeID;

                var result2 = from x in context.Users where x.Login == "test3" select x;
                tmp.UsersUserID = result2.First().UserID;

                context.Companies.Add(tmp);  
            }
        }

        [TestMethod]
        public void TestAddToEmployee()
        {
            using (var context = new BusinessCodingModelContex())
            {
                Empolyees tmp = new Empolyees();
                tmp.DateOfHiring = DateTime.Parse("2013-11-05 22:12:28.990");

                var result = from x in context.TypesOfEmployees where x.Name == "Senior Developer" select x;
                tmp.TypesOfEmployeesTOEmployeeID = result.First().TOEmployeeID;

                context.Empolyees.Add(tmp);
            }
        }
        
        [TestMethod]
        public void TestAddToProject()
        {
            using (var context = new BusinessCodingModelContex())
            {
                Projects tmp = new Projects();

                var result1 = from x in context.Companies where x.Name == "Apple Inc." select x;
                tmp.CompaniesCompanyID = result1.First().CompanyID;
                tmp.Complexity = 10;
                tmp.Gratification = 10000;
                tmp.MinCodity = 10;
                tmp.MinRespect = 10;
                tmp.Name = "testUnit";
                tmp.TimeRounds = 10;
                tmp.TimeToEnd = 10;

                context.Projects.Add(tmp);
            }
        }

        [TestMethod]
        public void AddToTypesOfEmployees()
        {
            using (var context = new BusinessCodingModelContex())
            {
                TypesOfEmployees tmp = new TypesOfEmployees();

                tmp.Codity = 10;
                tmp.Name = "testUnit";
                tmp.Salary = 10;

                context.TypesOfEmployees.Add(tmp);
            }
        }

        [TestMethod]
        public void AddToTypesOfOffices()
        {
            using (var context = new BusinessCodingModelContex())
            {
                TypesOfOffices tmp = new TypesOfOffices();

                tmp.Capacity = 10;
                tmp.CostOfHiring = 10;
                tmp.Name = "testUnit";

                context.TypesOfOffices.Add(tmp);
            }
        }

        [TestMethod]
        public void AddToDescriptions()
        {
            using (var contex = new BusinessCodingModelContex())
            {
                Descriptions tmp = new Descriptions();

                tmp.String = "testUnit";

                var restult = from x in contex.Projects where x.Name == "Nowy iOS" select x;
                tmp.ProjectsProjectID = restult.First().ProjectID;

                contex.Descriptions.Add(tmp);
            }
        }
    }
}
