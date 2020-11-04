using SharedTrip.Data;
using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Globalization;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        //dadenata platforma sys vsichki tripove
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var trips = this.tripsService.GetAll();
            return this.View(trips);

        }

        public HttpResponse Details(string tripId)
        {
            // Ako ne se e lognal ne moje da vijda deil, all, add, i go prashtame da se logne
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var trip = this.tripsService.GetDetails(tripId);
            return this.View(trip);
        }


        // Butona Join s tozi method
        // Trips/AddUserToTrip?tripId={tripId} (logged-in user)
        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (!this.tripsService.HasAvailableSeats(tripId))
            {
                return this.Error("No seats available.");
            }

            var userId = this.GetUserId();

            // If a User tries to join a Trip more than once, 
            //they should be redirected to the details page of the given Trip.
            if (!this.tripsService.AddUserTotrip(userId, tripId))
            {
                //Ako ne sym uspql da go dobavq Redirect -> ("/Trips/Details?tripId="+tripId)
                return this.Redirect("/Trips/Details?tripId=" + tripId);
            }
            //Upon successful Adding user to a trip, should be redirected to the /Trips/All.
            return this.Redirect("/Trips/All");
        }

        //platformata za dobavqne
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        //Validate dates recive form of AddTripInputModel
        //validaciite se pravqt , akto se gleda samoto nnachalo na dokumenta - Database Requirements
        public HttpResponse Add(AddTripInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(input.StartPoint))
            {
                return this.Error("Start point is required.");
            }
            if (string.IsNullOrEmpty(input.EndPoint))
            {
                return this.Error("End point is required.");
            }
            if (input.Seats < 2 || input.Seats > 6)
            {
                return this.Error("Seats should be between 2 and 6.");
            }
            if (string.IsNullOrEmpty(input.Description) || input.Description.Length > 80)
            {
                return this.Error("Description is required and has max length of 80.");
            }

            if (!DateTime.TryParseExact(
                input.DepartureTime,
                "dd.MM.yyyy HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _))
            {
                return this.Error("Invalid departure time. Please use dd.MM.yyyy HH:mm format.");
            }

            //We need AddTripsService where we will write some method to return right Trips
            //Create(input)

            //•	Upon successful Creation of a Trip, you should be redirected to the /Trips/All.
            this.tripsService.Create(input);
            //sled kato nqma go pravim malko po gore-> tozi pyt
            return this.Redirect("/Trips/All");
        }
    }
}
