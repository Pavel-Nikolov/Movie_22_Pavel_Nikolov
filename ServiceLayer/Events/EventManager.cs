using DomainLayer.Models;
using ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Events
{
    public static class EventManager
    {
        public static event ShowMovieEventHandler ShowMovie;
        public static event ShowOrderEventHandler ShowOrder;
        public static event ShowUserEventHandler ShowUser;

        public static void OnMovieShowing(Movie brand)
        {
            MovieVM brandVM = new MovieVM(brand, true);
            if (ShowMovie != null)
            {
                ShowMovie.Invoke(brandVM);
            }
        }

        public static void OnOrderShowing(Order product)
        {
            OrderVM productVM = new OrderVM(product, true);
            if (ShowOrder != null)
            {
                ShowOrder.Invoke(productVM);
            }
        }

        public static void OnUserShowing(User user)
        {
            UserVM userVM = new UserVM(user, true);
            if (ShowUser != null)
            {
                ShowUser.Invoke(userVM);
            }
        }
    }
}
