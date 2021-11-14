using Core.Models;
using Core.Testing.Questions;
using System.Collections.Generic;
using System.Linq;

namespace Core.Testing
{
    public class Test
    {
        private readonly IEnumerable<QuestionBase> _questions;
        private readonly IEnumerable<Disease> _diseases;

        public Test(IEnumerable<Disease> deseases, IEnumerable<QuestionBase> questions)
        {
            _diseases = deseases;
            _questions = questions;
        }

        public IEnumerable<Disease> Run()
        {
            foreach (var question in _questions)
            {
                question.Ask();
            }

            return _diseases.Where(x => x.IsPossible());
        }
    }
}
