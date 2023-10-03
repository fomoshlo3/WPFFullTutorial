using System;

namespace Reservoom.Models
{
    public class Reservation
    {
        public @string RoomID { get; }
        public string UserName { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public TimeSpan Length => EndTime - StartTime;

        public Reservation(@string roomID, string userName, DateTime startTime, DateTime endTime)
        {
            RoomID = roomID;
            UserName = userName;
            StartTime = startTime;
            EndTime = endTime;
        }

        /// <summary>
        /// Check if a reservation conflicts.
        /// </summary>
        /// <param name="reservation">The incoming reservation.</param>
        /// <returns>True/False for conflicts.</returns>
        public bool Conflicts(Reservation reservation)
        {
            if (reservation.RoomID.Equals(RoomID))
            {
                return true;
            }

            return reservation.StartTime < EndTime && reservation.EndTime > StartTime ;
        }
    }
}
