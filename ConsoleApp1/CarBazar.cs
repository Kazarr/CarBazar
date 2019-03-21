using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public static class CarBazar
    {
        public static List<Car> Cars { get; } = new List<Car>();
        public static List<Car> FileteredCars { get; set; } = new List<Car>();
        public static Car CreateCar()
        {
            Car newCar = new Car(GenerateID());

            Console.WriteLine("Type year car made");
            int yearMade = ValidateIntInput();
            newCar.Year = yearMade;

            Console.WriteLine("Type driven KM");
            int drivenKM = ValidateIntInput();
            newCar.DrivedKM = drivenKM;

            Console.WriteLine("Type brand");
            newCar.Brand = ValidateStringInput();

            Console.WriteLine("Type model");
            newCar.Model = ValidateStringInput();

            Console.WriteLine("Choose witch energy type:");
            Console.WriteLine(Energy.disel);
            Console.WriteLine(Energy.gas);
            Console.WriteLine(Energy.elektro);
            Console.WriteLine(Energy.hybrid);
            Energy choiceEnergy = ValidateEnergyInput();
            newCar.Energy = choiceEnergy;
            
            Console.WriteLine("Type price");
            decimal price = ValidateDecimalInput();
            newCar.Price = price;

            Console.WriteLine("Type city");
            newCar.City = ValidateStringInput();

            Console.WriteLine("Type door count");
            int doorCount = ValidateIntInput();
            newCar.DoorCount = doorCount;

            Console.WriteLine("Was a car crashed ?(True or false)");
            bool crashed = ValidateBoolInput();
            newCar.Crashed = crashed;

            Cars.Add(newCar);
            return newCar;
        }
        public static void SaveCars(string path)
        {
            File.Delete(path);
            foreach(Car c in Cars)
            {
                File.AppendAllText(path, c.ToString());
            }
        }
        public static void LoadCars(string path)
        {
            Cars.Clear();
            if (File.Exists(path))
            {
                string[] loadedLines = File.ReadAllLines(path);
                for (int i = 0; i < loadedLines.Length; i++)
                {
                    Car newCar = new Car(int.Parse(loadedLines[i].Split('\t')[0]));
                    string[] loadedCar = loadedLines[i].Split('\t');
                    newCar.Year = int.Parse(loadedCar[1]);
                    newCar.DrivedKM = int.Parse(loadedCar[2]);
                    newCar.Brand = loadedCar[3];
                    newCar.Model = loadedCar[4];
                    newCar.Energy = (Energy)Enum.Parse(typeof(Energy), loadedCar[5]);
                    newCar.Price = decimal.Parse(loadedCar[6]);
                    newCar.City = loadedCar[7];
                    newCar.DoorCount = int.Parse(loadedCar[8]);
                    newCar.Crashed = bool.Parse(loadedCar[9]);
                    Cars.Add(newCar);
                }
                Console.WriteLine("Cars have been loaded");
            }
            else
            {
                Console.WriteLine("Cars have not been loaded. There are no cars on given path");
            }
        }
        public static int GenerateID()
        {
            Random r = new Random();
            int number = r.Next(int.MaxValue);
            if(Cars.Count == 0)
            {
                return number;
            }
            bool end = true;
            while (end)
            {
                for(int i = 0; i <= Cars.Count; i++)
                {
                    if (Cars[i].ID == number)
                    {
                        number = r.Next(int.MaxValue);
                    }
                    else
                    {
                        end = false;
                        break;
                    }
                }
            }
            return number;
        }
        public static void EditCar(Car oldCar)
        {
            Console.WriteLine("This is the car you want to edit. ");
            Console.WriteLine(oldCar.ToString());
            Console.WriteLine("What you want to edit ?");
            Console.Write("year");
            Console.Write(", drived kilometers");
            Console.Write(", brand");
            Console.Write(", model");
            Console.Write(", Energy");
            Console.Write(", price");
            Console.Write(", city");
            Console.Write(", door count");
            Console.WriteLine(", crashed");
            
            bool end = false;
            while (!end)
            {
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "year":
                        {
                            Console.WriteLine("Type new year");
                            int newYear = ValidateIntInput();
                            oldCar.Year = newYear;
                            end = true;
                            break;
                        }
                    case "drived kilometers":
                        {
                            Console.WriteLine("Type new drived kilometers");
                            int newDrivedKm = ValidateIntInput();
                            oldCar.DrivedKM = newDrivedKm;
                            end = true;
                            break;
                        }
                    case "brand":
                        {
                            Console.WriteLine("Type new brand");
                            String newBrand = ValidateStringInput();
                            oldCar.Brand = newBrand;
                            end = true;
                            break;
                        }
                    case "model":
                        {
                            Console.WriteLine("Type new model");
                            String newModel = ValidateStringInput();
                            oldCar.Model = newModel;
                            end = true;
                            break;
                        }
                    case "energy":
                        {
                            Console.WriteLine("Type new energy type");
                            Energy newEnergy = ValidateEnergyInput();
                            oldCar.Energy = newEnergy;
                            end = true;
                            break;
                        }
                    case "price":
                        {
                            Console.WriteLine("Type new price");
                            decimal newPrice = ValidateDecimalInput();
                            oldCar.Price = newPrice;
                            end = true;
                            break;
                        }
                    case "city":
                        {
                            Console.WriteLine("Type new city");
                            String newCity = ValidateStringInput();
                            oldCar.City = newCity;
                            end = true;
                            break;
                        }
                    case "door count":
                        {
                            Console.WriteLine("Type new door count");
                            int newDoorCount = ValidateIntInput();
                            oldCar.DoorCount = newDoorCount;
                            end = true;
                            break;
                        }
                    case "crashed":
                        {
                            Console.WriteLine("Type if it was crashed");
                            bool newCrashed = ValidateBoolInput();
                            oldCar.Crashed = newCrashed;
                            end = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Type correct choice");
                            break;
                        }

                }
            }
        }
        public static void RemoveCar(Car oldCar)
        {
            Console.WriteLine("This is the car you want to delete. ");
            Console.WriteLine(oldCar.ToString());
            Console.WriteLine("Are you sure you want to delete this car ?");
            String choice = Console.ReadLine();
            if (choice.Equals("yes"))
            {
                Cars.Remove(oldCar);
            }
        }
        public static Car FindCar(int Id)
        {
            foreach (Car c in Cars)
            {
                if (c.ID == Id)
                {
                    return c;
                }
            }
            return null;
        }
        public static Car FindCar(Car car)
        {
            foreach(Car c in Cars)
            {
                if (c.Equals(car))
                {
                    return c;
                }
            }
            return null;
        }
        public static void WriteCars()
        {
            foreach (Car c in Cars)
            {
               Console.WriteLine(c.ToString());
            }
        }
        public static void WriteFilteredCars()
        {
            foreach(Car c in FileteredCars)
            {
                Console.WriteLine(c.ToString());
            }
        }
        public static void FilterYearMake(int fromYear, int toYear)
        {
            List<Car> filteredYear = new List<Car>();
            foreach (Car c in FileteredCars)
            {
                if (c.Year >= fromYear && c.Year <= toYear)
                {
                    filteredYear.Add(c);
                    Console.WriteLine(c.ToString());
                }
            }
            FileteredCars = filteredYear;
        }
        public static void FilterDrivenKilometers(int from, int to)
        {
            List<Car> filteredKilometers = new List<Car>();
            foreach (Car c in FileteredCars)
            {
                if (c.DrivedKM >= from && c.DrivedKM <= to)
                {
                    filteredKilometers.Add(c);
                    Console.WriteLine(c.ToString());
                }
            }
            FileteredCars = filteredKilometers;
        } 
        public static void FilterBrand(String brand)
        {
            List<Car> filteredBrand = new List<Car>();
            string[] brands = brand.Split(',');
            foreach (Car c in FileteredCars)
            {
                for(int i = 0; i < brands.Length; i++)
                {
                    if (c.Brand.Equals(brands[i]))
                    {
                        filteredBrand.Add(c);
                        Console.WriteLine(c.ToString());
                    }

                }
                
            }
            FileteredCars = filteredBrand;
        }
        public static void FilterEnergyType(string energy)
        {
            List<Car> filteredEnergy = new List<Car>();
            string[] energys = energy.Split(',');
            foreach (Car c in FileteredCars)
            {
                for(int i = 0; i < energys.Length; i++)
                {
                    if (c.Energy.Equals(Enum.Parse(typeof(Energy), energys[i])))
                    {
                        filteredEnergy.Add(c);
                        Console.WriteLine(c.ToString());
                    }
                }
                
            }
            FileteredCars = filteredEnergy;
        } 
        public static void FileterPrice(int fromPrice, int toPrice)
        {
            List<Car> filteredPrice = new List<Car>();
            foreach (Car c in FileteredCars)
            {
                if (c.Price >= fromPrice && c.Price <= toPrice)
                {
                    filteredPrice.Add(c);
                    Console.WriteLine(c.ToString());
                }
            }
            FileteredCars = filteredPrice;
        }
        public static void FiterCity(String city)
        {
            List<Car> filteredCity = new List<Car>();
            string[] cities = city.Split(',');
            foreach (Car c in FileteredCars)
            {
                for(int i = 0; i < cities.Length; i++)
                {
                    if (c.City.Equals(cities[i]))
                    {
                        filteredCity.Add(c);
                        Console.WriteLine(c.ToString());
                    }
                }
                
            }
            FileteredCars = filteredCity;
        } 
        public static void FilterCrashed(bool crashed)
        {
            List<Car> filteredCrashed = new List<Car>();
            foreach (Car c in FileteredCars)
            {
                if (c.Crashed == crashed)
                {
                    filteredCrashed.Add(c);
                    Console.WriteLine(c.ToString());
                }
            }
            FileteredCars = filteredCrashed;
        } 
        public static void FilterDoorCount(int doorCount)
        {
            List<Car> filteredDoorCount = new List<Car>();
            foreach (Car c in FileteredCars)
            {
                if (c.DoorCount == doorCount)
                {
                    Console.WriteLine(c.ToString());
                }
                else
                {
                    filteredDoorCount.Add(c);
                }
            }
            FileteredCars = filteredDoorCount;
        } 
        public static void CreateFilteredCars()
        {
            if (FileteredCars.Count <= 0)
            {
                foreach (Car c in Cars)
                {
                    FileteredCars.Add(c);
                }
            }
        }
        public static void RemoveFilteredCars()
        {
            if (FileteredCars.Count >= 0)
            {
                foreach(Car c in Cars)
                {
                    FileteredCars.Remove(c);
                }
            }
        }
        public static void RemoveWrongCars(List<Car> wrongCars)
        {
            foreach(Car c in wrongCars)
            {
                if (FileteredCars.Contains(c))
                {
                    FileteredCars.Remove(c);
                }
            }
        }
        public static void RemainRightCars(List<Car> rightCars)
        {
            for(int i = 0; i < FileteredCars.Count; i++)
            {
                if (!rightCars.Contains(FileteredCars[i]))
                {
                    FileteredCars.RemoveAt(i);
                }
            }
        }
        public static int ValidateIntInput()
        {
            bool success = false;
            int result = 0;
            while (!success)
            {
                if (int.TryParse(Console.ReadLine(), out result) && result>0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Type correct number");
                }
            }
            return result;
        }
        public static decimal ValidateDecimalInput()
        {
            bool success = false;
            decimal result = 0;
            while (!success)
            {
                if (decimal.TryParse(Console.ReadLine(), out result) && result >=0)
                {
                    if (result>0)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Type correc number");
                }
            }
            return result;
        }
        public static bool ValidateBoolInput()
        {
            bool success = false;
            bool result = false;
            while (!success)
            {
                success = bool.TryParse(Console.ReadLine(), out result);
                if (success)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Type correct answer. (True or False)");
                }
            }
            return result;
        }
        public static String ValidateStringInput()
        {
            string result = "bad input";
            bool success = false;
            while (!success)
            {
                result = Console.ReadLine();
                if (!result.Equals(""))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Type correct text");
                }
            }
            return result;
        }
        public static Energy ValidateEnergyInput()
        {
            Energy result = Energy.gas;
            bool success = false;
            while(!success){
                switch (Console.ReadLine())
                {
                    case "disel":
                        result = Energy.disel;
                        success = true;
                        break;
                    case "gas":
                        result = Energy.gas;
                        success = true;
                        break;
                    case "elektro":
                        result = Energy.elektro;
                        success = true;
                        break;
                    case "hybrid":
                        result = Energy.hybrid;
                        success = true;
                        break;
                    default:
                        Console.WriteLine("You didnt type a correct value try again.");
                        break;
                }
            }
            return result;
        }
        
    }
}
