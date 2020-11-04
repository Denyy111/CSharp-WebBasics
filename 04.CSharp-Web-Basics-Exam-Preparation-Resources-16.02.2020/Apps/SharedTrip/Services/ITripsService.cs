using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void Create(AddTripInputModel trip);

        IEnumerable<TripViewModel> GetAll();

        TripDetailsViewModel GetDetails(string id);

        //Метод за междинната таблица 
        /* 
         Trips/AddUserToTrip? tripId = { tripId } (logged-in user) 
         Adds the current user to the given trip
        */
        bool AddUserTotrip(string userId, string tripId);
        bool HasAvailableSeats(string tripId);
    }
}
