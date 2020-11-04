namespace SharedTrip.Data
{
    //Table between User and Trip
    public class UserTrip
    {    
        public string UserId { get; set; }
        //Navigation Prop-Object
        public virtual User User { get; set; }
        public string TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
