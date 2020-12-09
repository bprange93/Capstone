//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace PlannerProject.Stacker
//{
//    public class Sweepstakes
//    {
//        private Dictionary<int, Contestant> contestants;
//        private string name;
//        public string Name;
//        public Contestant contestWinner;

//        public Sweepstakes(string name)
//        {
//            this.name = name;
//            contestants = new Dictionary<int, Contestant>();
//        }

//        //move to UserInterface
//        public void RegisterContestant(Contestant contestant) //Assingns contestant obj an int Key and adds the contestants registration to Dic
//        {
//            int registerNumber = contestants.Count + 1;
//            contestants.Add(registerNumber, contestant);
//            contestant.RegistrationNumber = registerNumber;
//            Console.WriteLine($"{ contestants.Count} + {contestant.RegistrationNumber} + {contestant.FirstName}");
//        }

//        public Contestant PickWinner()
//        {

//            int winnersNumber = GetRandomNumber(contestants.Count + 1);
//            foreach (KeyValuePair<int, Contestant> winner in contestants)
//            {
//                if (winnersNumber == winner.Key)
//                {
//                    contestWinner = winner.Value;

//                }

//            }
//            return contestWinner;

//        }

//        public void PrintContestantInfo(Contestant contestant)
//        {
//            UserInterface.PrintContestantInfo(contestant, contestants);
//        }

//        public int GetRandomNumber(int max)
//        {
//            Random random = new Random();
//            int randomResult = random.Next(max);
//            return randomResult;
//        }
//    }
//}
//}
