using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DexterLab
{
   public class Question_Answer
    {
        public List<string> question;
        public List<string> answer;

        public Question_Answer()
        {
            question = new List<string>();
            answer = new List<string>();
        }

        public void ADD(string[] question, string[] answer)
        {
            for (int ques = 0; ques < question.Length; ques++)
            {
                this.question.Add(question[ques]);
            }
            for (int ans = 0; ans < answer.Length; ans++)
            {
                this.answer.Add(answer[ans]);
            }
        }
        public void ADD(string question, string answer)
        {
            this.question.Add(question);
            this.answer.Add(answer);
        }

        public void write_Ques_InFile(StreamWriter sw, Question_Answer QandA)
        {
            for (int writeQues = 0; writeQues < QandA.question.Count; writeQues++)
            {
                if (writeQues == QandA.question.Count - 1)
                {
                    sw.Write(QandA.question[writeQues]);
                }
                else
                {
                    sw.Write(QandA.question[writeQues] + "|");
                }
            }
            sw.WriteLine();
        }
        public void Write_Ans_In_File(StreamWriter sw, Question_Answer QandA)
        {
            for (int writeans = 0; writeans < QandA.answer.Count; writeans++)
            {
                if (writeans == QandA.answer.Count - 1)
                {
                    sw.Write(QandA.answer[writeans]);
                }
                else
                {
                    sw.Write(QandA.answer[writeans] + "|");
                }
            }
            sw.WriteLine();
        }
    }
}
