using System;

namespace Reservoom.Models
{
    public class @string
    {
        public int FloorNumber { get; }
        public int RoomNumber { get; }
        public @string(int floorNumber, int roomNumber)
        {
            FloorNumber=floorNumber;
            RoomNumber=roomNumber;
        }

        public override bool Equals(object? obj)
        {
            return obj is @string roomID &&
                FloorNumber == roomID.FloorNumber && RoomNumber == roomID.RoomNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FloorNumber, RoomNumber);
        }

        public override string ToString()
        {
            return $"{FloorNumber}{RoomNumber}";
        }

        public static bool operator ==(@string roomID1, @string roomID2)
        {
            if (roomID1 is null && roomID2 is null)
            {
                return true;
            }

            return !(roomID1 is null) && roomID1.Equals(roomID2);
        }

        public static bool operator !=(@string roomID1, @string roomID2)
        {
            return !(roomID1 == roomID2);
        }
    }
}
