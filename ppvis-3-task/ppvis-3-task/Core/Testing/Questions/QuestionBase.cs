namespace Core.Testing.Questions
{
    public abstract class QuestionBase
    {
        public string Description { get; private set; }

        public QuestionBase(string description)
        {
            Description = description;
        }

        abstract public void Ask();
    }
}
