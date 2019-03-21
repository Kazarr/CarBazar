using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool changed = false;
            bool end = true;
            while (end)
            {
                var path = "D:\\Transformer\\autobazar.txt";
                Console.WriteLine("Main menu");
                Console.WriteLine("Press 1 to add car");
                Console.WriteLine("Press 2 to delete car");
                Console.WriteLine("Press 3 to edit car");
                Console.WriteLine("Press 4 to see your cars");
                Console.WriteLine("Press 5 to save your cars");
                Console.WriteLine("Press 6 to load your cars");
                Console.WriteLine("Press 7 to filter your cars");
                Console.WriteLine("Press 8 to clear fileters");
                Console.WriteLine("Press 9 to see filtered cars");
                Console.WriteLine("Press x to exit program");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            changed = true;
                            CarBazar.CreateCar();
                            break;
                        }
                    case "2":
                        changed = true;
                        Console.WriteLine("Which car you want to remove. Type ID(the first number)");
                        CarBazar.WriteCars();
                        int removingCar;
                        bool successRemove = int.TryParse(Console.ReadLine(), out removingCar);
                        if(CarBazar.FindCar(removingCar) == null)
                        {
                            Console.WriteLine("Wrong ID");
                            break;
                        }
                        CarBazar.RemoveCar(CarBazar.FindCar(removingCar));
                        break;

                    case "3":
                        changed = true;
                        Console.WriteLine("Which car you want to edit. Type ID(the first number)");
                        CarBazar.WriteCars();
                        int editingCar;
                        bool successEdit = int.TryParse(Console.ReadLine(), out editingCar);
                        if (CarBazar.FindCar(editingCar) == null)
                        {
                            Console.WriteLine("Wrong ID");
                            break;
                        }
                        CarBazar.EditCar(CarBazar.FindCar(editingCar));
                        break;

                    case "4":
                        CarBazar.WriteCars();
                        break;

                    case "5":
                        changed = false;
                        CarBazar.SaveCars(path);
                        break;

                    case "6":
                        if (changed)
                        {
                            Console.WriteLine("You will lose all your un-saved work. Do you want to continue ?");
                            string answer = Console.ReadLine();
                            if (answer.Contains("yes"))
                            {
                                changed = false;
                                CarBazar.LoadCars(path);
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            CarBazar.LoadCars(path);
                        }
                        break;
                    case "7":
                        {
                            Console.WriteLine("Choose what you want to use as a filter");
                            Console.WriteLine("year, driven kilometers, brand, energyType, price, city, doorCount, crashed");
                            string filterChoice = Console.ReadLine();
                            CarBazar.CreateFilteredCars();
                            switch (filterChoice)
                            {
                                case "year":
                                    {
                                        int from;
                                        int to;
                                        Console.WriteLine("Type from what year");
                                        bool successFilterYear = int.TryParse(Console.ReadLine(), out from);
                                        Console.WriteLine("Type to what year");
                                        successFilterYear = int.TryParse(Console.ReadLine(), out to);
                                        CarBazar.FilterYearMake(from, to);
                                        break;
                                    }
                                case "driven kilometers":
                                    {
                                        int from;
                                        int to;
                                        Console.WriteLine("Type min driven kilometers");
                                        bool successFileterDrivenKilometers = int.TryParse(Console.ReadLine(), out from);
                                        Console.WriteLine("Type max driven kilometers");
                                        successFileterDrivenKilometers = int.TryParse(Console.ReadLine(), out to);
                                        CarBazar.FilterDrivenKilometers(from, to);
                                        break;
                                    }
                                case "brand":
                                    {
                                        string brand;
                                        Console.WriteLine("Type brand");
                                        brand = Console.ReadLine();
                                        CarBazar.FilterBrand(brand);
                                        break;
                                    }
                                case "energyType":
                                    {
                                        String energy;
                                        Console.WriteLine("Type energy type");
                                        energy = Console.ReadLine();
                                        CarBazar.FilterEnergyType(energy);
                                        break;
                                        //Energy energy;
                                        //Console.WriteLine("Type energy type");
                                        //bool successFilterEnergy = Enum.TryParse(Console.ReadLine(), out energy);
                                        //CarBazar.FilterEnergyType(energy);
                                        //break;
                                    }
                                case "price":
                                    {
                                        int from;
                                        int to;
                                        Console.WriteLine("Type min price");
                                        bool successFileterPrice = int.TryParse(Console.ReadLine(), out from);
                                        Console.WriteLine("Type max driven kilometers");
                                        successFileterPrice = int.TryParse(Console.ReadLine(), out to);
                                        CarBazar.FileterPrice(from, to);
                                        break;
                                    }
                                case "city":
                                    {
                                        String city;
                                        Console.WriteLine("Type city");
                                        city = Console.ReadLine();
                                        CarBazar.FiterCity(city);
                                        break;
                                    }
                                case "doorCount":
                                    {
                                        int count;
                                        Console.WriteLine("Type door count");
                                        bool successFileterPrice = int.TryParse(Console.ReadLine(), out count);
                                        CarBazar.FilterDoorCount(count);
                                        break;
                                    }
                                case "crashed":
                                    {
                                        bool crash;
                                        Console.WriteLine("Was it crashed?");
                                        bool successFilterCrash = bool.TryParse(Console.ReadLine(), out crash);
                                        CarBazar.FilterCrashed(crash);
                                        break;
                                    }
                            }
                            break;
                        }
                    case "8":
                        {
                            CarBazar.RemoveFilteredCars();
                            Console.WriteLine("Filters have been cleared");
                            break;
                        }
                    case "9":
                        {
                            CarBazar.WriteFilteredCars();
                            break;
                        }

                    case "x":
                        if (changed)
                        {
                            Console.WriteLine("You will lose all your un-saved work. Do you want to continue ?");
                            string answer = Console.ReadLine();
                            if (answer.Contains("yes"))
                            {
                                CarBazar.LoadCars(path);
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            CarBazar.LoadCars(path);
                        }
                        end = false;
                        break;

                    default:
                        Console.WriteLine("Your choice is invalid. Try again");
                        break;
                }
                
            }
        }
    }
}
