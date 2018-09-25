using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DexterLab
{
   public class Alternate_Q_A
    {
        List<string> Questions;
        List<string> Answers;
        Random rndm;

        public Alternate_Q_A()
        {
            rndm = new Random();
            Questions = new List<string>();
            Answers = new List<string>();

            StreamReader srA = new StreamReader(@"AlterAns.txt", true);

            while (srA.EndOfStream == false)
            {
                this.Answers.Add(srA.ReadLine());
            }

            srA.Close();

            StreamReader srQ = new StreamReader(@"AlterQues.txt", true);

            while (srQ.EndOfStream == false)
            {
                this.Questions.Add(srQ.ReadLine());
            }

            srQ.Close();
            //END of reading all question and answers from file

        }//end of Construtor

        public string Random_Alt_Question()
        {
            int num = rndm.Next(0, Questions.Count);
            return Questions[num];
        }//end of selecting random question from list method

        public string Random_Alt_Answer()
        {
            int num = rndm.Next(0, Answers.Count);
            return Answers[num];
        }//end of selecting random Answer from list method
    }
}
