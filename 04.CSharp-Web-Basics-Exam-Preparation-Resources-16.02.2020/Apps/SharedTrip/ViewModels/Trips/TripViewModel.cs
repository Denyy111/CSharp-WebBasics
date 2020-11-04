using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SharedTrip.ViewModels.Trips
{
    //Neshatat koito iskame da vizualizirame v formata All" -> tripovete drypnati ot bazata.
    public class TripViewModel
    {
        public string Id { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }

        // StartPoint-EndPoint
        //public string Fullname => $"{this.StartPoint} - {this.EndPoint}";
        // i go izpolzwave vyv view-to All

        public DateTime DepartureTime { get; set; }

        //Ako iskat DepartureTime da e osoben vid, Pravim dopylnitelno prop.
        //public string DepartureTimeAsString => this.DepartureTime.ToString(CultureInfo.GetCultureInfo("bg-BG"));
        // i go izpolzwave vyv view-to All
        public int AvailableSeats { get; set; }
    }
}
