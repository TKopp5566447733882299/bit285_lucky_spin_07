using System;
using System.Collections.Generic;

namespace LuckySpin.Models
{
    public class Repository
    {
        private List<Spin> spins = new List<Spin>();

        //Properties
        public Player CurrentPlayer { get; set; }

        public IEnumerable<Spin> PlayerSpins { // Read Only Property

            get { return spins; }
        }

        //Interaction method
        public void AddSpin(Spin s)
        {
            spins.Add(s);
        }
        public void ClearSpins()
        {
            spins.Clear();
        }
        
    }
}
