using ServiceLayer;
using ServiceLayer.Enums;
using ServiceLayer.Events;
using System;
using System.Linq;

namespace PresentationLayer
{
    class Program
    {
        static void SetUp()
        {
            EventManager.ShowMovie += PrintManager.PrintMovie;
            EventManager.ShowOrder += PrintManager.PrintProduct;
            EventManager.ShowUser += PrintManager.PrintUsers;
        }
        static void Main()
        {
            SetUp();
            string[] command;
            while (true)
            {
                try
                {
                    command = Console.ReadLine().Split();
                    switch (command[0].ToLower())
                    {
                        case "brand":
                            switch (command[1].ToLower())
                            {
                                case "create":
                                    DBManager.RunCommand(EntityType.Movie, OperationType.Create, command[2], command[3], command.Skip(4).ToArray());
                                    break;
                                case "read":
                                    DBManager.RunCommand(EntityType.Movie, OperationType.Read, command[2]);
                                    break;
                                case "readall":
                                    DBManager.RunCommand(EntityType.Movie, OperationType.ReadAll);
                                    break;
                                case "delete":
                                    DBManager.RunCommand(EntityType.Movie, OperationType.Delete, command[2]);
                                    break;
                                case "update":
                                    DBManager.RunCommand(EntityType.Movie, OperationType.Update, command[2], command[3], command[4], command.Skip(5).ToArray());
                                    break;
                                case "find":
                                    DBManager.RunCommand(EntityType.Movie, OperationType.Find, command[2]);
                                    break;
                                default:
                                    Console.WriteLine("Invalid operation");
                                    break;
                            }
                            break;

                        case "product":
                            switch (command[1].ToLower())
                            {
                                case "create":
                                    DBManager.RunCommand(EntityType.Order, OperationType.Create, command[2], command[3], command[4], command[5], command.Skip(6).Select(int.Parse).ToArray());
                                    break;
                                case "read":
                                    DBManager.RunCommand(EntityType.Order, OperationType.Read, command[2]);
                                    break;
                                case "readall":
                                    DBManager.RunCommand(EntityType.Order, OperationType.ReadAll);
                                    break;
                                case "update":
                                    DBManager.RunCommand(EntityType.Order, OperationType.Update, command[2], command[3], command[4], command[5], command.Skip(6).Select(int.Parse).ToArray());
                                    break;
                                case "delete":
                                    DBManager.RunCommand(EntityType.Order, OperationType.Delete, command[2]);
                                    break;
                                case "find":
                                    DBManager.RunCommand(EntityType.Order, OperationType.Find, command[2]);
                                    break;
                                default:
                                    Console.WriteLine("Invalid operation");
                                    break;
                            }
                            break;

                        case "user":
                            switch (command[1].ToLower())
                            {
                                case "create":
                                    DBManager.RunCommand(EntityType.User, OperationType.Create, command[2], command[3], command.Skip(4).ToArray());
                                    break;
                                case "read":
                                    DBManager.RunCommand(EntityType.User, OperationType.Read, command[2]);
                                    break;
                                case "readall":
                                    DBManager.RunCommand(EntityType.User, OperationType.ReadAll);
                                    break;
                                case "update":
                                    DBManager.RunCommand(EntityType.User, OperationType.Update, command[2], command[3], command[4], command.Skip(5).ToArray());
                                    break;
                                case "delete":
                                    DBManager.RunCommand(EntityType.User, OperationType.Delete, command[2]);
                                    break;
                                case "find":
                                    DBManager.RunCommand(EntityType.User, OperationType.Find, command[2]);
                                    break;
                                default:
                                    Console.WriteLine("Invalid operation");
                                    break;
                            }
                            break;

                        default:
                            Console.WriteLine("Invalid type");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("Next Command");
                }
            }
        }
    }
}
