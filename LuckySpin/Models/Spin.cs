using System;
using System.ComponentModel.DataAnnotations;
namespace LuckySpin.Models
{
    public class Spin
    {
        public long SpinId { get; set; }
        public Boolean IsWinning { get; set; }
    }
}
