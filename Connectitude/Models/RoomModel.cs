using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connectitude.Models
{
    public class RoomModel
    {
        public int Id { get; set; } //Id to distinguish the rooms from each other as different facility could have the same type of room
        public string Name { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public int HomeId { get; set; } //Id to the facility that the room is linked to
        public string HomeName { get; set; } //Name of the facility the room can be found in
    }
}