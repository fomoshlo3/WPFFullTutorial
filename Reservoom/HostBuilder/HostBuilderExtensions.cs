using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reservoom.Services;
using Reservoom.Stores;
using Reservoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.HostBuilder
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
                  {
                      services.AddTransient((s) => CreateReservationListingViewModel(s))
                              .AddSingleton<Func<ReservationListingViewModel>>((s) => () => s.GetRequiredService<ReservationListingViewModel>())
                              .AddSingleton<NavigationService<ReservationListingViewModel>>()

                              .AddTransient<MakeReservationViewModel>()
                              .AddSingleton<Func<MakeReservationViewModel>>((s) => () => s.GetRequiredService<MakeReservationViewModel>())
                              .AddSingleton<NavigationService<MakeReservationViewModel>>()

                              .AddSingleton<MainViewModel>();
                  });

            return hostBuilder;
        }

        private static ReservationListingViewModel CreateReservationListingViewModel(IServiceProvider services)
        {
            var hotelStore = services.GetRequiredService<HotelStore>();
            var navService = services.GetRequiredService<NavigationService<MakeReservationViewModel>>();

            return ReservationListingViewModel.LoadViewModel(hotelStore, navService);
        }
    }
}
