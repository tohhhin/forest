using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forest
{
    interface Eatable//съедобное
    {
        double calories { get; set; }
    }
    abstract class Animal : Eatable
    {
        public string name;
        public double calories { get; set; }
        public List<Eatable> stomach = new List<Eatable>();
        public abstract bool eat(Eatable x);
        public abstract void inspection_stomach(List<Eatable> stomach,string name);
       


    }
    class Predator: Animal
    {
      
        public int weight;

        public Predator()
        {
            this.calories = 100;
        }

        public override void inspection_stomach(List<Eatable> stomach, string fname)
        {
            int kol = stomach.Count;//количество объектво в желудке, пмимо тех, которые у травоядных
            Console.WriteLine(" в желудке у " + fname + " " + kol + " объектов :");
            foreach (Eatable aPart in stomach)
            {
                if (aPart is Trash)
                    Console.WriteLine("мусор");
                else
                  if (aPart is Grass)
                {
                    Grass g = aPart as Grass;
                    Console.WriteLine(g.name);
                }
                else
                  if (aPart is Herbivorous)
                {
                    Herbivorous qq = aPart as Herbivorous;
                    Console.WriteLine(qq.name);
                    qq.inspection_stomach(qq.stomach, qq.name);
                }
                else if (aPart is Predator)
                {
                    Predator qq = aPart as Predator;
                    Console.WriteLine(qq.name);
                    qq.inspection_stomach(qq.stomach, qq.name);
                }

            }
        }
        public override bool eat(Eatable x)
        {
            if (x is Predator)
            {
              
                Predator a = x as Predator;
                Console.WriteLine(name + " пытается съесть " + a.name);
                if (weight > a.weight && (a.defend() == false))
                {
                    calories += 0.5*x.calories;
                    stomach.Add(x);
                    Console.WriteLine(name + " (" + calories + ")" + " съел " + a.name);
                    return true;
                }

            }
            else if (x is Herbivorous)
                {
                    Herbivorous b = x as Herbivorous;
                Console.WriteLine(name + " пытается съесть " + b.name);
                if (b.hide() == false)
                    {
                        calories += 0.5*x.calories;
                    stomach.Add(x);
                    Console.WriteLine(name + " (" + calories + ")"+" съел " + b.name);

                    return true;
                }

                }
            else if (x is Trash)
            {
              
                    calories += x.calories;
                    stomach.Add(x);
                Console.WriteLine(name + " (" + calories + ")" + " съел мусор ");
                return true;

            }
            return false;

        }
        public bool defend()//
        {
           Random rnd = new Random();
            if (rnd.Next(0, 2) == 1)
            {
                Console.WriteLine(name + " защищается ");
                return true;
            }
            else return false;

        }


    }
    class Trash :Eatable
    {
       
        public Trash()
        {
            this.calories = -1;
            
        }
        public double calories { get; set; }
    }

    class Grass : Eatable
    {
        public string name;
        public Grass()
        {
            this.calories = 10;
        }
        public double calories { get; set; }
    }

    class Herbivorous : Animal
    {
        public Herbivorous()
        {
            this.calories = 50;
        }

        public override bool eat(Eatable x)
        {
           if (x is Grass )
            {
                calories +=  x.calories;
                Console.WriteLine(name + " (" + calories + ")" + " съел траву ");
                stomach.Add(x);
              
                return true;
            }

        else    if (x is Trash)
            {
                calories += x.calories;

                Console.WriteLine(name + " (" + calories + ")" + " съел мусор ");
                stomach.Add(x);

                return true;
            }
            return false;

        }
        public override void inspection_stomach(List<Eatable> stomach,string fname)
        {
        int kol = stomach.Count;//количество объектво в желудке у травоядного
            Console.WriteLine(" в желудке у "+ fname +" " + kol + " объектов :");
            foreach (Eatable aPart in stomach)
            { if (aPart is Trash)
                    Console.WriteLine("мусор");
                else
                if (aPart is Grass) { Grass g = aPart as Grass;
                    Console.WriteLine(g.name); }
            
            }         
        }

    public bool hide()//  /прятаться
        {
            Random rnd = new Random();
            if (rnd.Next(0, 2) == 1)
            {  Console.WriteLine(name + " прячется ");
            return true; }
              else return false;

            /* return rnd.Next(0, 2) == 1;*/
        }


    } 
     
        
    class Program
    {
        static void Main()
        {
            List<Eatable> parts = new List<Eatable>();
            parts.Add(new Trash() );
            parts.Add(new Trash() );
            parts.Add(new Trash() );
            parts.Add(new Herbivorous() { name = "cow" });
            parts.Add(new Herbivorous() { name = "pig" });
            parts.Add(new Herbivorous() { name = "horse"});
            parts.Add(new Predator() {  name = "krokodile", weight=200 });
            parts.Add(new Predator() { name = "lion", weight = 350 });
            parts.Add(new Predator() { name = "ferret", weight = 20 });//толстый хорек
            parts.Add(new Grass() { name = "green" });
            parts.Add(new Grass() { name = "flowers" });

            Random aq = new Random();
            
            while (parts.Count > 1)
            {
                int number_animal = aq.Next(0, parts.Count);
                while (!(parts[number_animal] is Animal))
                    number_animal = aq.Next(0, parts.Count);

                int number_food = aq.Next(0, parts.Count);
                while (number_food == number_animal)
                    number_food = aq.Next(0, parts.Count);


                Animal hunter = parts[number_animal] as Animal;
                Eatable h = parts[number_food];
             

                

                    
               if (hunter.eat(parts[number_food]))
                {
                  /*  if (h is Animal)
                    {
                        Animal qq = h as Animal;
                        qq.inspection_stomach(qq.stomach,qq.name);
                    }
*/
                    parts.RemoveAt(number_food);
                    
                }

                if(parts.Count ==1)hunter.inspection_stomach(hunter.stomach, hunter.name);
            }
           

            Console.ReadLine();

        }

    }


}
