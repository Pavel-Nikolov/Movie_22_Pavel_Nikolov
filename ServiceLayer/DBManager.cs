using DomainLayer.DataAccess;
using DomainLayer.Models;
using ServiceLayer.Enums;
using ServiceLayer.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    public static class DBManager
    {
        private static Context context = new Context();
        private static readonly MovieRepository movieRepository;
        private static readonly OrderRepository orderRepository;
        private static readonly UserRepository userRepository;

        static DBManager()
        {
            movieRepository = new MovieRepository(context);
            orderRepository = new OrderRepository(context);
            userRepository = new UserRepository(context);
        }

        public static void RunCommand(EntityType entityType, OperationType operationType, params object[] args)
        {
            context = new Context();
            switch (entityType)
            {
                case EntityType.Movie:
                    ManageMovies(operationType, args);
                    break;
                case EntityType.Order:
                    ManageProducts(operationType, args);
                    break;
                case EntityType.User:
                    ManageUsers(operationType, args);
                    break;
                default:
                    break;
            }
        }

        private static void ManageUsers(OperationType operationType, object[] args)
        {
            switch (operationType)
            {
                case OperationType.Create:
                    List<Order> ordersToBeCreated = new List<Order>();
                    if (args.Length > 2)
                    {
                        int[] orderIdsToBeCreated = args[2] as int[];
                        foreach (var item in orderIdsToBeCreated)
                        {
                            ordersToBeCreated.Add(context.Orders.Find(item));
                        }
                    }
                    User userToBeCreated = new User()
                    {
                        Name = args[0].ToString(),
                        Age = ParseNullable(args[1].ToString(), "?"),
                        Orders = ordersToBeCreated
                    };
                    userRepository.Create(userToBeCreated);
                    break;

                case OperationType.Read:
                    int readKey = int.Parse(args[0].ToString());
                    User readUser = userRepository.Read(readKey);
                    EventManager.OnUserShowing(readUser);
                    break;

                case OperationType.ReadAll:
                    ICollection<User> usersRead = userRepository.ReadAll();
                    foreach (var item in usersRead)
                    {
                        EventManager.OnUserShowing(item);
                    }
                    break;

                case OperationType.Delete:
                    int deleteKey = int.Parse(args[0].ToString());
                    userRepository.Delete(deleteKey);
                    break;

                case OperationType.Update:
                    List<Order> ordersToBeUpdated = new List<Order>();
                    if (args.Length > 3)
                    {
                        int[] productIdsToBeUpdated = args[3] as int[];
                        foreach (var item in productIdsToBeUpdated)
                        {
                            ordersToBeUpdated.Add(context.Orders.Find(item));
                        }
                    }
                    User userToBeUpdated = new User()
                    {
                        ID = int.Parse(args[0].ToString()),
                        Name = args[1].ToString(),
                        Age = ParseNullable(args[2].ToString(), "?"),
                        Orders = ordersToBeUpdated
                    };
                    userRepository.Update(userToBeUpdated);
                    break;
                case OperationType.Find:
                    string index = args[0].ToString();
                    ICollection<User> usersFound = userRepository.Find(index);
                    foreach (var item in usersFound)
                    {
                        EventManager.OnUserShowing(item);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Converts string to int?
        /// </summary>
        /// <param name="input">The string to be converted</param>
        /// <param name="nullSrting">The representation of the null value</param>
        /// <returns>A int? of value of the corresponding number or null if it is the nullString</returns>
        /// <exception cref="FormatException">The number is not a number or the null string</exception>
        
        public static int? ParseNullable(string input, string nullSrting)
        {
            if (input == nullSrting)
            {
                return null;
            }
            return int.Parse(input);
        }

        private static void ManageProducts(OperationType operationType, object[] args)
        {
            switch (operationType)
            {
                case OperationType.Create:
                    int movieToBeCreatedId = int.Parse(args[3].ToString());
                    Movie movieToBeCreated = context.Movies.Find(movieToBeCreatedId);
                    List<User> usersToBeCreated = new List<User>();

                    if (args.Length > 5)
                    {
                        int[] userIdsToBeCreated = args[4] as int[];
                        foreach (var item in userIdsToBeCreated) 
                        {
                            usersToBeCreated.Add(context.Users.Find(item));
                        }
                    }

                    Order productToBeCreated = new Order()
                    {
                        ID = int.Parse(args[0].ToString()),
                        Name = args[1].ToString(),                        
                        Price = decimal.Parse(args[2].ToString()),
                        Movie = movieToBeCreated,
                        Users = usersToBeCreated
                    };

                    orderRepository.Create(productToBeCreated);
                    break;

                case OperationType.Read:
                    int readKey = int.Parse(args[0].ToString());
                    Order readOrder = orderRepository.Read(readKey);
                    EventManager.OnOrderShowing(readOrder);
                    break;

                case OperationType.ReadAll:
                    ICollection<Order> readOrders = orderRepository.ReadAll();
                    foreach (var item in readOrders)
                    {
                        EventManager.OnOrderShowing(item);
                    }
                    break;

                case OperationType.Delete:
                    int deleteKey = int.Parse(args[0].ToString());
                    orderRepository.Delete(deleteKey);
                    break;

                case OperationType.Update:
                    int movieToBeUpdatedId = int.Parse(args[3].ToString());
                    Movie movieToBeUpdated = context.Movies.Find(movieToBeUpdatedId);
                    List<User> usersToBeUpdated = new List<User>();

                    if (args.Length > 5)
                    {
                        int[] userIdsToBeUpdated = args[4] as int[];
                        foreach (var item in userIdsToBeUpdated)
                        {
                            usersToBeUpdated.Add(context.Users.Find(item));
                        }
                    }

                    Order productToBeUpdated = new Order()
                    {
                        ID = int.Parse(args[0].ToString()),
                        Name = args[1].ToString(),                       
                        Price = decimal.Parse(args[2].ToString()),
                        Movie = movieToBeUpdated,
                        Users = usersToBeUpdated
                    };
                    orderRepository.Update(productToBeUpdated);
                    break;

                case OperationType.Find:
                    string index = args[0].ToString();
                    ICollection<Order> ordersFound = orderRepository.Find(index);
                    foreach (var item in ordersFound)
                    {
                        EventManager.OnOrderShowing(item);
                    }
                    break;
                default:
                    break;
            }
        }

        private static void ManageMovies(OperationType operationType, object[] args)
        {
            switch (operationType)
            {
                case OperationType.Create:
                    List<Order> ordersToBeCreated = new List<Order>();

                    if (args.Length > 1)
                    {
                        int[] ordersIdsToBeCreated = args[2] as int[];
                        foreach (var item in ordersIdsToBeCreated)
                        {
                            ordersToBeCreated.Add(context.Orders.Find(item));
                        }
                    }

                    Movie movieToBeCreated = new Movie
                    {
                        Name = args[0].ToString(),
                        Duration = int.Parse(args[1].ToString()),
                        Orders = ordersToBeCreated
                    };

                    movieRepository.Create(movieToBeCreated);
                    break;

                case OperationType.Read:
                    int readKey = int.Parse(args[0].ToString());
                    Movie readMovie = movieRepository.Read(readKey);
                    EventManager.OnMovieShowing(readMovie);
                    break;

                case OperationType.ReadAll:
                    ICollection<Movie> readMovies = movieRepository.ReadAll();
                    foreach (var item in readMovies)
                    {
                        EventManager.OnMovieShowing(item);
                    }
                    break;

                case OperationType.Delete:
                    int deleteKey = int.Parse(args[0].ToString());
                    movieRepository.Delete(deleteKey);
                    break;

                case OperationType.Update:
                    List<Order> ordersToBeUpdated = new List<Order>();
                    if (args.Length > 2)
                    {
                        int[] ordersIdsToBeUpdated = args[3] as int[];
                        foreach (var item in ordersIdsToBeUpdated)
                        {
                            ordersToBeUpdated.Add(context.Orders.Find(item));
                        }
                    }
                    Movie moviesToBeUpdated = new Movie()
                    {
                        ID = int.Parse(args[0].ToString()),
                        Name = args[1].ToString(),
                        Duration = int.Parse(args[2].ToString()),
                        Orders = ordersToBeUpdated,
                    };
                    movieRepository.Update(moviesToBeUpdated);
                    break;

                case OperationType.Find:
                    string index = args[0].ToString();
                    ICollection<Movie> moviesFound = movieRepository.Find(index);
                    foreach (var item in moviesFound)
                    {
                        EventManager.OnMovieShowing(item);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
