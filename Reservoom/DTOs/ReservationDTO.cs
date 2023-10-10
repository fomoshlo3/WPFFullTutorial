using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Reservoom.DTOs
{
    [Table("Reservations")]
    public class ReservationDTO
    {
        [Key]
        public Guid Id { get; set; }

        public int FloorNumber { get; set; }
        public int RoomNumber { get; set; }
        public string UserName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
