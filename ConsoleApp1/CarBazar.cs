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
            int yearMade;
            bool sucess = int.TryParse(Console.ReadLine(), out yearMade);
            if (sucess)
            {
                newCar.Year = yearMade;
            }


            Console.WriteLine("Type driven KM");
            int drivenKM;
            sucess = int.TryParse(Console.ReadLine(), out drivenKM);
            if (sucess)
            {
                newCar.DrivedKM = drivenKM;
            }
            Console.WriteLine("Type brand");
            newCar.Brand = Console.ReadLine();
            Console.WriteLine("Type model");
            newCar.Model = Console.ReadLine();
            Console.WriteLine("Choose witch energy type:");
            Console.WriteLine(Energy.disel);
            Console.WriteLine(Energy.gas);
            Console.WriteLine(Energy.elektro);
            Console.WriteLine(Energy.hybrid);
            string choiceEnergy = Console.ReadLine();
            switch (choiceEnergy)
            {
                case "disel":
                    newCar.Energy = Energy.disel;
                    break;
                case "gas":
                    newCar.Energy = Energy.gas;
                    break;
                case "elektro":
                    newCar.Energy = Energy.elektro;
                    break;
                case "hybrid":
                    newCar.Energy = Energy.hybrid;
                    break;
                default:
                    Console.WriteLine("You didnt type a correct value try again.");
                    break;
            }
            Console.WriteLine("Type price");
            decimal price;
            sucess = decimal.TryParse(Console.ReadLine(), out price);
            if (sucess)
            {
                newCar.Price = price;
            }
            Console.WriteLine("Type city");
            newCar.City = Console.ReadLine();

            Console.WriteLine("Type door count");
            int doorCount;
            sucess = int.TryParse(Console.ReadLine(), out doorCount);
            if (sucess)
            {
                newCar.DoorCount = doorCount;
            }
            Console.WriteLine("Was a car crashed ?(yes or no)");
            bool crashed;
            sucess = bool.TryParse(Console.ReadLine(), out crashed);
            if (crashed)
            {
                newCar.Crashed = crashed;
            }
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
                    Console.WriteLine("Cars have been loaded");
                }
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
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "year":
                    {
                        int newYear;
                        bool success = int.TryParse(Console.ReadLine(), out newYear);
                        oldCar.Year = newYear;
                        break;
                    }
                case "drived kilometers":
                    {
                        int newDrivedKm;
                        bool success = int.TryParse(Console.ReadLine(), out newDrivedKm);
                        oldCar.DrivedKM = newDrivedKm;
                        break;
                    }
                case "brand":
                    {
                        String newBrand = Console.ReadLine();
                        oldCar.Brand = newBrand;
                        break;
                    }
                case "model":
                    {
                        String newModel = Console.ReadLine();
                        oldCar.Model = newModel;
                        break;
                    }
                case "energy":
                    {
                        Energy newEnergy;
                        newEnergy = (Energy)Enum.Parse(typeof(Energy), Console.ReadLine());
                        oldCar.Energy = newEnergy;
                        break;
                    }
                case "price":
                    {
                        decimal newPrice;
                        bool success = decimal.TryParse(Console.ReadLine(), out newPrice);
                        oldCar.Price = newPrice;
                        break;
                    }
                case "city":
                    {
                        String newCity = Console.ReadLine();
                        oldCar.City = newCity;
                        break;
                    }
                case "door count":
                    {
                        int newDoorCount;
                        bool success = int.TryParse(Console.ReadLine(), out newDoorCount);
                        oldCar.DoorCount = newDoorCount;
                        break;
                    }
                case "crashed":
                    {
                        bool newCrashed;
                        bool success = bool.TryParse(Console.ReadLine(), out newCrashed);
                        oldCar.Crashed = newCrashed;
                        break;
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
        
    }
}
