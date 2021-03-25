using ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Events
{
    public delegate void ShowMovieEventHandler(MovieVM brandVM);

    public delegate void ShowOrderEventHandler(OrderVM productVM);

    public delegate void ShowUserEventHandler(UserVM userVM);
}
