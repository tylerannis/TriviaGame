using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame
{
    class Trivia
    {
        //TODO: Fill out the Trivia Object
        
        //The Trivia Object will have 2 properties
        // at a minimum, Question and Answer

        //set your properties
        public string Question { get; set; }
        public string Answer { get; set; }
        //The Constructor for the Trivia object will
        // accept only 1 string parameter.  You will
        // split the question from the answer in your
        // constructor and assign them to the Question
        // and Answer properties

        //Set the Class Trivia to a string
        public Trivia(string splitAnswer)
        {
            //You need something to hold both the question and answer once you split them
            List<string> hold = new List<string>();
            //split Trivia string now both question and answer can be seperate strings with seperate indexs
            hold = splitAnswer.Split('*').ToList();


            //setting question to hold the value of the string before the *
            //and setting answer to hold the value of the string after the *
           
            this.Question = hold[0].ToString();
            this.Answer = hold[1].ToString();
        }
        //Methods and Functions
        //this is a function to ask a question
        public string AskQuestion()
        {
            return this.Question;
        }
        public string GiveAnswer()
        {
            return this.Answer.ToLower();
        }
    
    }
}
