using System;
using System.Threading;

namespace DiningPhilophers
{
    //This code creates 5 philophers and 5 forks.
    //Philophoers can only eat if there are two forks available
    class Program
    {
        //static bool array holding 5 bools wich starts from 0 and goes to 4
        static bool[] forkIsAvailable = new bool[5];
        static object _lock = new object();

        //instansing all my filosofs
        static Philosoph PhilosophOne = new Philosoph(false, false, "1");
        static Philosoph PhilosophTwo = new Philosoph(false,false, "2");
        static Philosoph PhilosophThree = new Philosoph(false, false, "3");
        static Philosoph PhilosophFour = new Philosoph(false, false, "4");
        static Philosoph PhilosophFive = new Philosoph(false, false, "5");

        static void Main()
        {
            //making a referance to what is outside this scope so that I can use objects that is placed there
            Program pg = new Program();
            
            //creating a threat for every filosof
            Thread threadOne = new Thread(new ThreadStart(pg.PhiloOneEats));
            Thread threadTwo = new Thread(new ThreadStart(pg.PhiloTwoEats));
            Thread threadThree = new Thread(new ThreadStart(pg.PhiloThreeEats));
            Thread threadFour = new Thread(new ThreadStart(pg.PhiloFourEats));
            Thread threadFive = new Thread(new ThreadStart(pg.PhiloFiveEats));
            
            //starting my threads
            threadOne.Start();
            threadTwo.Start();
            threadThree.Start();
            threadFour.Start();
            threadFive.Start();
        }
        
        public void PhiloOneEats()
        {
            while (true)
            {
                Monitor.Enter(_lock);
                try
                {
                    forkIsAvailable[0] = PhilosophOne.PickUpLeftFork(forkIsAvailable[0]);
                    forkIsAvailable[1] = PhilosophOne.PickUpRightFork(forkIsAvailable[1]);
                    PhilosophOne.Eat();
                    forkIsAvailable[0] = PhilosophOne.PutDownLeftFork(forkIsAvailable[0]);
                    forkIsAvailable[1] = PhilosophOne.PutDownRightFork(forkIsAvailable[1]);
                    Console.WriteLine("Philosoph 1 is Thinking");
                    Thread.Sleep(500);
                }
                finally
                {
                    Monitor.Exit(_lock);
                }
            }
        }
        
        public void PhiloTwoEats()
        {
            while (true)
            {
                Monitor.Enter(_lock);
                try
                {
                    forkIsAvailable[2] = PhilosophTwo.PickUpLeftFork(forkIsAvailable[2]);
                    forkIsAvailable[3] = PhilosophTwo.PickUpRightFork(forkIsAvailable[3]);
                    PhilosophTwo.Eat();
                    forkIsAvailable[2] = PhilosophTwo.PutDownLeftFork(forkIsAvailable[2]);
                    forkIsAvailable[3] = PhilosophTwo.PutDownRightFork(forkIsAvailable[3]);
                    Console.WriteLine("Philosoph 2 is Thinking");
                    Thread.Sleep(500);
                }
                finally
                {
                    Monitor.Exit(_lock);
                }
            }
        }
        public void PhiloThreeEats()
        {
            while (true)
            {
                Monitor.Enter(_lock);
                try
                {
                    forkIsAvailable[1] = PhilosophThree.PickUpLeftFork(forkIsAvailable[1]);
                    forkIsAvailable[2] = PhilosophThree.PickUpRightFork(forkIsAvailable[2]);
                    PhilosophThree.Eat();
                    forkIsAvailable[1] = PhilosophThree.PutDownLeftFork(forkIsAvailable[1]);
                    forkIsAvailable[2] = PhilosophThree.PutDownRightFork(forkIsAvailable[2]);
                    Thread.Sleep(500);
                    Console.WriteLine("Philosoph 3 is Thinking");
                }
                finally
                {
                    Monitor.Exit(_lock);
                }
            }
        }
        public void PhiloFourEats()
        {
            while (true)
            {
                Monitor.Enter(_lock);
                try
                {
                    forkIsAvailable[3] = PhilosophFour.PickUpLeftFork(forkIsAvailable[3]);
                    forkIsAvailable[4] = PhilosophFour.PickUpRightFork(forkIsAvailable[4]);
                    PhilosophFour.Eat();
                    forkIsAvailable[3] = PhilosophFour.PutDownLeftFork(forkIsAvailable[3]);
                    forkIsAvailable[4] = PhilosophFour.PutDownRightFork(forkIsAvailable[4]);
                    Thread.Sleep(500);
                    Console.WriteLine("Philosoph 4 is Thinking");
                }
                finally
                {
                    Monitor.Exit(_lock);
                }
            }
        }
        public void PhiloFiveEats()
        {
            while (true)
            {
                Monitor.Enter(_lock);
                try
                {
                    forkIsAvailable[4] = PhilosophFive.PickUpLeftFork(forkIsAvailable[4]);
                    forkIsAvailable[0] = PhilosophFive.PickUpRightFork(forkIsAvailable[0]);
                    PhilosophFive.Eat();
                    forkIsAvailable[4] = PhilosophFive.PutDownLeftFork(forkIsAvailable[4]);
                    forkIsAvailable[0] = PhilosophFive.PutDownRightFork(forkIsAvailable[0]);
                    Thread.Sleep(500);
                    Console.WriteLine("Philosoph 5 is Thinking");
                }
                finally
                {
                    Monitor.Exit(_lock);
                }
            }
        }
    }

    class Philosoph
    {
        //variables
        private bool lf { get; set; }
        private bool rf { get; set; }
        private string name { get; set; }

        //constructor  
        public Philosoph(bool lf, bool rf, string name)
        {
            this.lf = lf;
            this.rf = rf;
            this.name = name;
        }
        
        //checks if the left fork is on the table and make the filosof pick it up if it is
        public bool PickUpLeftFork(bool forkOnTable)
        {
            if (forkOnTable == true)
            {
                forkOnTable = false;
                lf = true;
            }
            
            return forkOnTable;
        }

        //checks if the right fork is on the table and make the filosof pick it up if it is
        public bool PickUpRightFork(bool forkOnTable)
        {
            if (forkOnTable == true)
            {
                forkOnTable = false;
                rf = true;
            }
            return forkOnTable;
        }
        
        //checks if the filosof have a fork in each hand and make the filosof eat if that is the case
        public void Eat()
        {
            if (lf == true && rf == true)
            {
                Console.WriteLine(name + " is eating");
            }
        }

        //checks if for is on table and if its not it makes sure that the filosof puts it on the table
        public bool PutDownLeftFork(bool forkOnTable)
        {
            if (forkOnTable == false)
            {
                forkOnTable = true;
                lf = false;
            }
            return forkOnTable;
        }

        //checks if for is on table and if its not it makes sure that the filosof puts it on the table
        public bool PutDownRightFork(bool forkOnTable)
        {
            if (forkOnTable == false)
            {
                forkOnTable = true;
                rf = false;
            }
            return forkOnTable;
        }
    }
}