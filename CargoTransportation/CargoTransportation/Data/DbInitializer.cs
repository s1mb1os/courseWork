using CargoTransportation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.Data
{
    public class DbInitializer
    {
        private static Random randObj = new Random(1);
        public static void Initialize(CargoTransportationContext db)
        {
            db.Database.EnsureCreated();

            int employeeCount = 50;
            int carBrandCount = 40;
            int carCount = 100;
            int cargoCount = 100;
            int flightCount = 200;

            TypeOfCargo(db);
            EmployeeGenerate(db, employeeCount);
            CarBrandGenerate(db, carBrandCount);
            CarGenerate(db, carCount);
            CargoGenerate(db, cargoCount);
            FlightGenerat(db, flightCount);
        }

        private static void TypeOfCargo(CargoTransportationContext db)
        {
            if (db.TypeOfCargos.Any())
            {
                return;
            }

            db.TypeOfCargos.AddRange(new TypeOfCargo[]
            {
                new TypeOfCargo()
                {
                    Name = "Bulk",
                    Description = "Some description"
                },
                new TypeOfCargo()
                {
                    Name = "Powdery",
                    Description = "Some description"
                },
                new TypeOfCargo()
                {
                    Name = "Oversized",
                    Description = "Some description"
                },
                new TypeOfCargo()
                {
                    Name = "Piece",
                    Description = "Some description"
                }
            });
            db.SaveChanges();
        }

        private static void EmployeeGenerate(CargoTransportationContext db, int count)
        {
            if (db.Employees.Any())
            {
                return;
            }

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";

            string[] fullNamesVoc = { "Zhmailik A.V.", "Setko A.I.", "Semenov D.S.", "Davidchik A.E.", "Piskun E.A.",
                                  "Drakula V.A.", "Yastrebov A.A.", "Steponenko Y.A.", "Basharimov Y.I.", "Karkozov V.V.", "Lipsky D.Y." };

            string[] addressVoc = {"Mozyr, per.Zaslonova, ", "Gomel, st.Gastelo, ", "Minsk, st.Poleskay, ", 
                "Grodno, pr.Rechetski, ", "Vitebsk, st, International, ",
                                    "Brest, pr.October, ", "Minsk, st.Basseinaya, ", "Mozyr, boulevard Youth, " };

            for (int i = 0; i < count; i++)
            {
                var fullName = fullNamesVoc[randObj.Next(fullNamesVoc.GetLength(0))] + randObj.Next(count);
                var age = randObj.Next(18, 65);
                var address = addressVoc[randObj.Next(addressVoc.GetLength(0))] + randObj.Next(count);
                var phoneNumber = "+375 (29) " + randObj.Next(100, 999) + "-" + randObj.Next(10, 99) +
                              "-" + randObj.Next(10, 99);
                var passportData = new string(Enumerable.Repeat(chars, 2)
                                .Select(s => s[randObj.Next(s.Length)]).ToArray()).ToUpper() + randObj.Next(100000, 999999);
                var position = new string(Enumerable.Repeat(chars, 20)
                                .Select(s => s[randObj.Next(s.Length)]).ToArray());

                db.Employees.Add(new Employee()
                {
                    FullName = fullName,
                    Age = age,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    PassportData = passportData,
                    Position = position
                });
            }
            db.SaveChanges();
        }

        private static void CarBrandGenerate(CargoTransportationContext db, int count)
        {
            if (db.CarBrands.Any())
            {
                return;
            }

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
            string[] nameVoc = { "Mercedes-Benz W", "VW Passat B", "VW Golf ", "VW Polo ", "ВАЗ201", "Tesla Model ",
                                "Volvo F", "Seat ", "Toyota Camry 3.", "Hyundai "};

            string[] typeVoc = { "Passenger car", "Truck ", "Сargo-passenger car" };

            for (int i = 0; i < count; i++)
            {
                var name = nameVoc[randObj.Next(nameVoc.GetLength(0))] + randObj.Next(count);
                int maxSpeed = randObj.Next(160, 320);
                var description = new string(Enumerable.Repeat(chars, 20)
                                .Select(s => s[randObj.Next(s.Length)]).ToArray());
                var type = typeVoc[randObj.Next(typeVoc.GetLength(0))] + randObj.Next(count);

                db.CarBrands.Add(new CarBrand()
                {
                    Name = name,
                    MaxSpeed = maxSpeed,
                    Description = description,
                    Type = type
                });
            }
            db.SaveChanges();
        }

        private static void CarGenerate(CargoTransportationContext db, int count)
        {
            if (db.Cars.Any())
            {
                return;
            }

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int carBrandCount = db.CarBrands.Count();
            int employeeCount = db.Employees.Count();

            for (int i = 0; i < count; i++)
            {
                var regNumber = chars[randObj.Next(chars.Length)].ToString() + chars[randObj.Next(chars.Length)].ToString()
                    + " " + randObj.Next(1000, 9999);
                var vinNumber = GetRandStr(chars, 8) + randObj.Next(0, 9) + GetRandStr(chars, 2) + randObj.Next(100000, 999999);
                var engineNumber = GetRandStr(chars, 2) + randObj.Next(1000, 9999) + GetRandStr(chars, 1) + randObj.Next(10, 99);
                var yearOfIssue = randObj.Next(1980, 2020);
                var techInspection = DateTime.Now.AddDays(-randObj.Next(1000));
                var employeeId = randObj.Next(1, employeeCount + 1);
                var carBrandId = randObj.Next(1, carBrandCount + 1);

                db.Cars.Add(new Car()
                {
                    RegNumber = regNumber,
                    VinNumber = vinNumber,
                    EngineNumber = engineNumber,
                    YearOfIssue = yearOfIssue,
                    TechInspection = techInspection,
                    EmployeeId = employeeId,
                    CarBrandId = carBrandId
                });
            }
            db.SaveChanges();
        }

        private static void CargoGenerate(CargoTransportationContext db, int count)
        {
            if (db.Cargos.Any())
            {
                return;
            }

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz  ";
            var typeOfCargoCount = db.TypeOfCargos.Count();

            for (int i = 0; i < count; i++)
            {
                var name = GetRandStr(chars, 20);
                var typeOfCargoId = randObj.Next(1, typeOfCargoCount + 1);
                var shelfLife = DateTime.Now.AddDays(randObj.Next(500));
                var features = GetRandStr(chars, 40);

                db.Cargos.Add(new Cargo()
                {
                    Name = name,
                    TypeOfCargoId = typeOfCargoId,
                    ShelfLife = shelfLife,
                    Features = features
                });
            }
            db.SaveChanges();
        }

        private static void FlightGenerat(CargoTransportationContext db, int count)
        {
            if (db.Flights.Any())
            {
                return;
            }

            string[] customerVoc =
            {
                "Lipsky D.Y.", "Stolny S.D.", "Semenov D.S.", "Deker M.A.",
                "Ropot I.V.", "Butkovski Y.V.",
                "Stepanenko Y.V.", "Moiseikov R.A.", "Rogolevich N.V.", "Gerosimenko M.A.",
                "Galetskiy A.A.", "Zankevich K.A."
            };
            string[] pointVoc =
            {
                "Gomel", "Mozyr", "Minsk", "Grodno", "Brest", "Moscow", "Mogilev"
            };
            int carCount = db.Cargos.Count();
            int cargoCount = db.Cargos.Count();

            for (int i = 0; i < count; i++)
            {
                var customer = customerVoc[randObj.Next(customerVoc.GetLength(0))] + randObj.Next(count);
                var startPoint = pointVoc[randObj.Next(pointVoc.GetLength(0))] + randObj.Next(count);
                var endPoint = pointVoc[randObj.Next(pointVoc.GetLength(0))] + randObj.Next(count);
                var startDate = DateTime.Now.AddDays(-randObj.Next(500));
                var endDate = startDate.AddDays(randObj.Next(500));
                var carId = randObj.Next(1, carCount + 1);
                var cargoId = randObj.Next(1, cargoCount + 1);
                var price = randObj.NextDouble() * 100;
                var isPayment = randObj.Next(0, 1000) > 500 ? true : false;
                var isReturn = randObj.Next(0, 1000) > 500 ? true : false;

                db.Flights.Add(new Flight()
                {
                    Customer = customer,
                    StartPoint = startPoint,
                    EndPoint = endPoint,
                    StartDate = startDate,
                    EndDate = endDate,
                    CarId = carId,
                    CargoId = cargoId,
                    Price = price,
                    IsPayment = isPayment,
                    IsReturn = isReturn
                });
            }
            db.SaveChanges();
        }

        private static string GetRandStr(string chars, int maxChar)
        {
            return new string(Enumerable.Repeat(chars, maxChar)
                                .Select(s => s[randObj.Next(s.Length)]).ToArray());
        }
    }
}
