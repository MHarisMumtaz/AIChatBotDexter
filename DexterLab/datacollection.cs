using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace DexterLab
{
    public class datacollection
    {
            Dictionary<string, Question_Answer> collection;
            List<double> Answers_index;
            List<string> Answers;

            public datacollection()
            {

                this.collection = new Dictionary<string, Question_Answer>();

            }//end of constructor

            public void Add(string question, string Answer)
            {

                Question_Answer ques_Ans = new Question_Answer();
                ques_Ans.ADD(question, Answer);
                string key = ques_Ans.question[0].ToUpper()[0].ToString();
                key = generatekey(key);
                collection.Add(key, ques_Ans);

            }//end of overload Adding new question and answer in collection method

            private void Add(Question_Answer q)
            {

                try
                {

                    string key = q.question[0].ToUpper()[0].ToString();
                    key = generatekey(key);
                    collection.Add(key, q);

                }
                catch
                {

                    string key = q.question[0].ToUpper().ToString();
                    key = generatekey(key);
                    collection.Add(key, q);

                }

            }//end of add method()

            string generatekey(string key)
            {

                int strnum = 1;
                string originalkey = key;
                string temp = key;

                while (collection.ContainsKey(key) == true)
                {

                    originalkey = originalkey + strnum;
                    strnum++;
                    key = originalkey;
                    originalkey = temp;

                }
                return key;

            }//end of genreating key method()

            public void RetrieveDataFromFile()
            {
                StreamReader reader = new StreamReader(@"Questions.txt", true);
                StreamReader ReadAnswer = new StreamReader(@"Answers.txt", true);

                while (reader.EndOfStream == false)
                {
                    string temp = "";
                    string[] a;
                    string[] b;

                    temp = reader.ReadLine();
                    a = temp.Split('|');
                    temp = ReadAnswer.ReadLine();
                    b = temp.Split('|');

                    Question_Answer q = new Question_Answer();
                    q.ADD(a, b);
                    Add(q);
                }

                reader.Close();
                ReadAnswer.Close();
            }//end of retrive data from file method

            public string Search_Answer(string ques)
            {
                //Random rn = new Random();

                //string key = ques.ToUpper()[0].ToString();
                //string tempkey = key;
                //int num = 0, num2 = 1;

                //while (collection.ContainsKey(key) == true)
                //{

                //    for (int i = 0; i < collection[key].question.Count; i++)
                //    {

                //        string qw = collection[key].question[i].ToUpper();
                //        string tempques = ques.ToUpper();
                //        if (qw == tempques)
                //        {

                //            num = rn.Next(0, collection[key].answer.Count);
                //            string ans = collection[key].answer[num];
                //            return ans;

                //        }
                //    }

                //    key = tempkey;
                //    key = key + num2;
                //    num2++;
                //}

                string result = Search_In_Per(ques);

                return result;
            }//end of search function()

            public string Search_In_Per(string question)
            {

                Initialize_Search_Field();
                int concatnumb = 1;
                string key;

                string[] inputQuestion = question.ToUpper().Split(' ');

                for (int k = 65; k <= 90; k++)
                {
                    key = MakeKeys(k);
                    concatnumb = 1;

                    while (collection.ContainsKey(key) == true)
                    {

                        check_Collection_Question(key, inputQuestion);
                        key = MakeKeys(k);
                        key = key + concatnumb;
                        concatnumb++;

                    }//end of while loop contain key

                }//end of aplhabet key loop A-Z
                if (Answers_index.Count != 0)
                {
                    double mkey = Answers_index.Max();
                    if (mkey >= 90)
                    {
                        int ind = Answers_index.IndexOf(mkey);
                        string answer = Answers[ind];
                        return answer;
                    }

                }

                return question;


            }//end of search method using percentage

            private void Initialize_Search_Field()
            {
                this.Answers_index = new List<double>();
                this.Answers = new List<string>();
            }//end of initiallize lists

            private void check_Collection_Question(string key, string[] inputQuestion)
            {
                int ans_index = 0;
                double sum = 0, per = 0;
                double total_Length = 0;

                for (int Questionsindex = 0; Questionsindex < collection[key].question.Count; Questionsindex++)
                {
                    string[] Original_Saved_Data = collection[key].question[Questionsindex].ToUpper().Split(' ');

                    total_Length = 0;
                    sum = 0;

                    Count_Question_Length(Original_Saved_Data, out  total_Length);
                    Check_QuestionBy_percentage(Original_Saved_Data, inputQuestion, ref sum);

                    per = (sum / total_Length) * 100;
                    per = Math.Floor(per);

                    if (per >= 50)
                    {
                        try
                        {
                            Check_PER__Add_Question_InCollection(inputQuestion, per, key);

                            Answers.Add(collection[key].answer[ans_index]);
                            Answers_index.Add(per);

                            ans_index++;
                        }
                        catch
                        {
                            Check_PER__Add_Question_InCollection(inputQuestion, per, key);

                            Answers.Add(collection[key].answer[ans_index - 1]);
                            Answers_index.Add(per);
                        }
                    }
                }//end of questions loop in one key of dictinoary
            }

            public void Count_Question_Length(string[] question, out double length)
            {
                length = 0.0;

                for (int i = 0; i < question.Length; i++)
                {
                    length = question[i].Length + length;
                }
            }//end of count length

            private void Check_QuestionBy_percentage(string[] Original_Saved_Data, string[] inputQuestion, ref double sum)
            {

                int count = 0;
                sum = 0;

                for (int i = 0; i < Original_Saved_Data.Length; i++)
                {//total saved data array length tk chly ga outer loop
                    count = 0;
                    try
                    {
                        for (int inputloop = 0; inputloop < inputQuestion[i].Length; inputloop++)
                        {
                            for (int checkloop = inputloop; checkloop < Original_Saved_Data[i].Length; checkloop++)
                            {
                                if (Original_Saved_Data[i][checkloop].Equals(inputQuestion[i][inputloop]))
                                {

                                    count++;
                                    break;

                                }//end of comparing each aplhabet of question word
                            }//end of comparing saved data to input data loop
                        }//end of input question one word length loop

                        if (i == 0 && count == 0)
                        {
                            break;
                        }//end of checking first word

                        sum = sum + count;
                    }
                    catch { }
                }//end of outer loop
            }//end of checking question method()

           

            void Check_PER__Add_Question_InCollection(string[] question, double percentage, string key)
            {
                if (percentage >= 80 && percentage <= 97)
                {
                    string newquestion = "";
                    newquestion = question[0].ToLower() + " ";

                    for (int i = 1; i < question.Length - 1; i++)
                    {
                        newquestion = newquestion + question[i].ToLower() + " ";
                    }

                    newquestion = newquestion + question[question.Length - 1].ToLower();
                    collection[key].question.Add(newquestion);
                }
            }//end of checking percentage and adding new question

            private string MakeKeys(int startingAscii)
            {
                char origkey = (char)startingAscii;
                string key = origkey.ToString();
                return key;
            }//end of making keys

            private void File_Copy()
            {

                File.Delete(@"Questions.txt");
                File.Delete(@"Answers.txt");
                File.Create(@"Answers.txt").Close();
                File.Create(@"Questions.txt").Close();

            }//end of method()

            public void SaveAll()
            {

                File_Copy();

                StreamWriter writequestion = new StreamWriter(@"Questions.txt", true);
                StreamWriter writeanswer = new StreamWriter(@"Answers.txt", true);

                int num = 1, ascii = 65;
                string key = MakeKeys(ascii);
                int count = 0;

                while (count != collection.Count)
                {
                    if (collection.ContainsKey(key))
                    {

                        string innerkey = key;

                        while (collection.ContainsKey(innerkey))
                        {
                            Question_Answer QandA = new Question_Answer();
                            QandA = collection[innerkey];

                            QandA.write_Ques_InFile(writequestion, QandA);
                            QandA.Write_Ans_In_File(writeanswer, QandA);

                            innerkey = key;
                            innerkey = innerkey + num;
                            num++;
                            count++;
                        }
                    }

                    num = 1;
                    ascii++;
                    key = MakeKeys(ascii);

                }//end of write all data in file

                writeanswer.Close();
                writequestion.Close();

            }//end of saving all data

            public string Search_In_Answer(string Answer)
            {

                Initialize_Search_Field();
                int concatnumb = 1;
                string key;

                string[] inputAnswer = Answer.ToUpper().Split(' ');

                for (int k = 65; k <= 90; k++)
                {
                    key = MakeKeys(k);
                    concatnumb = 1;

                    while (collection.ContainsKey(key) == true)
                    {

                        check_Collection_Answer(key, inputAnswer);
                        key = MakeKeys(k);
                        key = key + concatnumb;
                        concatnumb++;

                    }//end of while loop contain key

                }//end of aplhabet key loop A-Z
                if (Answers_index.Count != 0)
                {
                    double mkey = Answers_index.Max();

                    if (mkey >= 66)
                    {
                        int ind = Answers_index.IndexOf(mkey);
                        string answer = Answers[ind];
                        return answer;
                    }
                }
                return Answer;

            }//end of search method using percentage

            private void check_Collection_Answer(string key, string[] inputAnswer)
            {
                int ans_index = 0;
                double sum = 0, per = 0;
                double total_Length = 0;

                for (int Answerindex = 0; Answerindex < collection[key].answer.Count; Answerindex++)
                {
                    string[] Original_Saved_Data = collection[key].answer[Answerindex].ToUpper().Split(' ');

                    total_Length = 0;
                    sum = 0;

                    Count_Question_Length(Original_Saved_Data, out  total_Length);
                    sum = Check_QuestionBy_percentage(Original_Saved_Data, inputAnswer, sum);

                    per = (sum / total_Length) * 100;
                    per = Math.Ceiling(per);

                    if (per >= 70)
                    {
                        try
                        {
                            Check_PER__Add_Question_InCollection(inputAnswer, per, key);

                            Answers.Add(collection[key].question[ans_index]);
                            Answers_index.Add(per);

                            ans_index++;
                        }
                        catch
                        {
                            Check_PER__Add_Question_InCollection(inputAnswer, per, key);

                            Answers.Add(collection[key].question[ans_index - 1]);
                            Answers_index.Add(per);
                        }
                    }
                }//end of questions loop in one key of dictinoary
            }//end of collection checking answers

            private double Check_QuestionBy_percentage(string[] Original_Saved_Data, string[] inputAnswer, double sum)
            {

                int count = 0;
                sum = 0;

                for (int i = 0; i < Original_Saved_Data.Length; i++)
                {//total saved data array length tk chly ga outer loop
                    count = 0;
                    try
                    {
                        for (int inputloop = 0; inputloop < inputAnswer[i].Length; inputloop++)
                        {
                            for (int checkloop = inputloop; checkloop < Original_Saved_Data[i].Length; checkloop++)
                            {
                                if (Original_Saved_Data[i][checkloop].Equals(inputAnswer[i][inputloop]))
                                {
                                    count++;
                                    break;
                                }//end of comparing each aplhabet of answers word
                            }//end of comparing saved data to input data loop
                        }//end of input answers one word length loop

                        if (i == 0 && count == 0)
                        {
                            break;
                        }//end of checking first word

                        sum = sum + count;
                    }
                    catch { }
                }//end of outer loop
                return sum;
            }//end of checking answers method()

            void Check_PER__Add_Answer_InCollection(string[] Answer, double percentage, string key)
            {
                if (percentage >= 95 && percentage <= 98)
                {
                    string newAnswer = "";
                    newAnswer = Answer[0].ToLower() + " ";

                    for (int i = 1; i < Answer.Length - 1; i++)
                    {
                        newAnswer = newAnswer + Answer[i].ToLower() + " ";
                    }

                    newAnswer = newAnswer + Answer[Answer.Length - 1].ToLower();
                    collection[key].answer.Add(newAnswer);
                }
            }//end of checking percentage and adding new answers
    }
}
