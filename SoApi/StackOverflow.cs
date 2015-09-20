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
        private Config _config;

        public StackOverflow(Config config)
        {
            _config = config;
        }

        private List<Question> questions;
        private List<Question> Questions
        {
            get
            {
                if (questions == null || questions.Count == 0)
                {
                    LoadQuestions();
                }

                return questions;
            }
        }

        public void LoadQuestions()
        {
            var client = new StacManClient();
            var response = client.Questions.GetAll(StackoverflowSite, page: 1, pagesize: 100, sort: AllSort.Creation, order: Order.Desc, tagged: _config.SelectedCategory).Result;

            questions = response.Data.Items.Select(x => new Question {Title = x.Title, Url = x.Link}).ToList();
        }

        public List<Question> GetNext()
        {
            var result = new List<Question>();
            
            for (var i = 0; i < 3; i++)
            {
                var rnd = new Random().Next(Questions.Count);
                var q = Questions[rnd];
                Questions.RemoveAt(rnd);
                result.Add(q);
            }
            
            return result;
        }
    }
}
