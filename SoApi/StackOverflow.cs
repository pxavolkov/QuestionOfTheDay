using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.StacMan;
using StackExchange.StacMan.Questions;

namespace SoApi
{
    public class StackOverflow
    {
        private const string StackoverflowSite = "stackoverflow";

        private List<Question> questions;
        private List<Question> Questions
        {
            get
            {
                if (questions == null || questions.Count == 0)
                {
                    questions = LoadQuestions();
                }

                return questions;
            }
        }

        private List<Question> LoadQuestions()
        {
            var client = new StacManClient();
            var response = client.Questions.GetAll(StackoverflowSite, page: 1, pagesize: 100, sort: AllSort.Creation, order: Order.Desc).Result;

            var data = response.Data.Items.Select(x => new Question {Title = x.Title, Url = x.Link}).ToList();
            return data;
        }

        public Question GetNext(string category)
        {
            var rnd = new Random().Next(Questions.Count);
            var q = Questions[rnd];
            Questions.RemoveAt(rnd);
            return q;
        }
    }
}
