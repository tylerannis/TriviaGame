using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame
{
    class Program
    {//The logic for your trivia game happens here
        static List<Trivia> AllQuestions;
        static void Main(string[] args)
        {
            //The logic for your trivia game happens here
            AllQuestions = GetTriviaList();
            //greet the user and ask for their name
            Console.WriteLine("Hello and welcome to the almost decent trivia game! What is your name?");
            string name = Console.ReadLine();
            //print new greeting and rules with name
            Console.WriteLine("Hey there " + name + ". We are going to ask you a few questions and if you get them right you could earn up to 5000 useless points!\nOn the other hand... if you get just five questions wrong, you will lose and your name will not go down in history.");
            Console.WriteLine("Let's play Who's A Big Fat Nerd!\n");
            //set variables for number of wrong guesses
            int lives = 5;
            int score = 0;
           
           
            //set up something to randomly loop through the triva list and give a question while lives is > 0
            while (lives >= 0 || score <= 5000)
            {
                int currentscore = 0;
                //make a random number generator
                Random newQuestion = new Random();
                int number = newQuestion.Next(0, AllQuestions.Count());
                Trivia CurrentQues = AllQuestions[number];
                //return question
                Console.WriteLine(CurrentQues.AskQuestion());
                //tell them to write an answer
                Console.WriteLine("Type your answer");
              string answer = Console.ReadLine().ToLower();
                //if user answer is correct
              if (answer == CurrentQues.GiveAnswer())
              {
                  //add one to their score
                  currentscore++;
                  //display good job and their lives and their score
                  Console.WriteLine("[Your Score: {0}]", currentscore + "\n[Your Lives: {1}]", lives);
                  Console.WriteLine("\n\n\nGreat job! I can't believe you knew that! Your get a point!");
              }
              else
              {
                  //if they get it wrong subtract one from their lives
                  lives--;
                  //display score and lives and the answer and appropritate insult
                  Console.WriteLine("\n\n[YourScore: {0}]", currentscore + "\n[Your Lives: ]", lives);
                  Console.WriteLine("\n\n\nYou lose a life");
                  Console.WriteLine("\nThe answer is {0}", CurrentQues.GiveAnswer());
                  if (lives == 4)
                  {
                      Console.WriteLine("\nYou are so dumb, I can't believe you missed that. You lose a life.\n");
                  }
                  else if (lives == 3)
                  {
                      Console.WriteLine("\nWhat the hell is wrong with you! Didn't you go to grade school! You idiot!\n");
                  }
                  else if (lives == 2)
                  {
                      Console.WriteLine("\nOh my god you are totally off the Jepordy team. I didn't know anyone could be this dumb.\n");
                  }
                  else if (lives == 1)
                  {
                      Console.WriteLine("\nJust give up you suck...\n");
                  }
              }
              
              
            }
            ReturnHighScore(score);
            DisplayScores();
        }



        //This functions gets the full list of trivia questions from the Trivia.txt document
        static List<Trivia> GetTriviaList()
        {
            //Get Contents from the file.  Remove the special char "\r".  Split on each line.  Convert to a list.
            List<string> contents = File.ReadAllText("trivia.txt").Replace("\r", "").Split('\n').ToList();

            //Each item in list "contents" is now one line of the Trivia.txt document.
            
            //make a new list to return all trivia questions
            List<Trivia> returnList = new List<Trivia>();
            foreach (var item in contents)
            {
                returnList.Add(new Trivia(item));
            }
            // TODO: go through each line in contents of the trivia file and make a trivia object.
            //       add it to our return list.
            // Example: Trivia newTrivia = new Trivia("what is my name?*question");
            //Return the full list of trivia questions
            return returnList;
           
            
        }
        static void ReturnHighScore(int playerscore)
        {
            
            Console.WriteLine("Your name: ");
            string playerName = Console.ReadLine();

            //new database
            TylerEntities db = new TylerEntities();

            HighScore newHighScore = new HighScore();

            newHighScore.Date = DateTime.Now;
            newHighScore.Game = "Trivia";
            newHighScore.PlayerName = playerName;
            newHighScore.Score = playerscore;

            db.HighScores.Add(newHighScore);

            db.SaveChanges();
        }
        static void DisplayScores()
        {
            //clear the console
            Console.Clear();
            //Write the High Score Text

            Console.WriteLine("============================= TRIVIA HIGH SCORE =============================");

            //create a new connection to the database
            TylerEntities db = new TylerEntities();

            //get the HS list
            List<HighScore> highScoreList = db.HighScores.Where(x => x.Game == "Trivia").OrderByDescending(x => x.Score).Take(10).ToList();

            foreach (HighScore highScore in highScoreList)
            {
                Console.WriteLine("{0}, {1} - {2}", highScoreList.IndexOf(highScore) + 1, highScore.PlayerName, highScore.Score);
            }

        }
    }
}
