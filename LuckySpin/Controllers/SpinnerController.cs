using System;
using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;
using LuckySpin.ViewModels;

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        //TODO: IMPORTANT: Run the application FIRST and play couple games before making any of these changes!
        //      In every case, use the TODO prompt to use the LuckySpin database in place of the Repository
        //      Check that the behavior of the application is the same at the end

        //TODO: Start here by removing the reference to the Singleton Repository
        //      and inject a reference to the LuckySpinContext instead
        private Repository _repository;
        private LuckySpinContext _lsc;
        Random random = new Random();

        /***
         * Controller Constructor
         */
        public SpinnerController(Repository repository) //TODO: use LuckySpinContext instead
        {
            _repository = repository; //TODO: use _lsc instead
        }

        /***
         * Index Action - Gathers Player info
         **/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel info)
        {
            if (!ModelState.IsValid) { return View(); }

            //Create a new Player object
            Player player = new Player
            {
                FirstName = info.FirstName,
                Luck = info.Luck,
                Balance = info.StartingBalance
            };
            //TODO: Use _lsc to add the player to dbContext _lsc and save changes to the database, instead of the repository
            _repository.CurrentPlayer = player;

            
            return RedirectToAction("Spin"); //TODO: Pass the player Id to Spin, using RedirectToAction("Spin", new {id = player.PlayerId})
        }

        /***
         * Spin Action - Plays one Spin
         **/  
         [HttpGet]      
         public IActionResult Spin() //TODO: receive an id of type long
        {
            //TODO: Get a player object from the database using the _lsc Players' Find method
            

            //TODO: Intialize the ViewModel with the player object you just got from the database, instead of the repository object
            SpinViewModel spinVM = new SpinViewModel() {
                PlayerName = _repository.CurrentPlayer.FirstName,
                Luck = _repository.CurrentPlayer.Luck,
                CurrentBalance = _repository.CurrentPlayer.Balance
            };

            if (!spinVM.ChargeSpin())
            {
                return RedirectToAction("LuckList");
            }
 
            if (spinVM.Winner) { spinVM.CollectWinnings(); }
            
            // TODO: Update the dbContext player's Balance using value from the ViewModel
            _repository.CurrentPlayer.Balance = spinVM.CurrentBalance;

            //Creates a Spin using the logic from the SpinViewModel
            Spin spin = new Spin() {
                IsWinning = spinVM.Winner
            };

            //TODO: Use _lsc to add the spin to dbContext _lsc and save changes to the database, instead of the repository
            _repository.AddSpin(spin);

            return View("Spin", spinVM); //Sends the updated spin info to the Spin View
        }

        /***
         * ListSpins Action - Displays Spin data
         **/
         [HttpGet]
         public IActionResult LuckList()
        {
            //TODO: Pass the DbSet of Spins from the _lsc to the LuckyList View, instead of the repository spins
            return View(_repository.PlayerSpins);
        }

    }
}

