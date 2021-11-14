using Core.Authorization;
using Core.Models;
using Core.Storage;
using Core.Tasks;
using Core.Tasks.Contracts;
using Core.Testing;
using Core.Testing.Questions;
using Core.Testing.Questions.Validators;
using Core.Testing.Questions.Validators.Contracts;
using System.Collections.Generic;

namespace Core
{
    class Program
    {
        private static void DummyInjectorInitialization()
        {
            var temperatureQuestion = new NumericQuestion("Your temperature");
            var abcQuestion = new SingleVariantQuestion("Choose one result (1, 2, 3)");

            var temperatureValidator1 = new RangeNumericValidator(36f, 37.5f, temperatureQuestion);
            var temperatureValidator2 = new RangeNumericValidator(37f, 38f, temperatureQuestion);
            var temperatureValidator3 = new RangeNumericValidator(38f, 39f, temperatureQuestion);
            var temperatureValidator4 = new RangeNumericValidator(37.5f, 38.5f, temperatureQuestion);

            var abcValidator1 = new SingleVariantValidator(1, abcQuestion);
            var abcValidator2 = new SingleVariantValidator(2, abcQuestion);
            var abcValidator3 = new SingleVariantValidator(3, abcQuestion);
            var abcValidator4 = new SingleVariantValidator(1, abcQuestion);

            var diseases = new Dictionary<string, Disease>
            {
                {"0", new Disease("0", "Disease A (temperature[36-37.5], result[1])", new Dictionary<string, ISymptomValidator>
                {
                    { "temperature", temperatureValidator1 },
                    { "question", abcValidator1 },
                }) },
                {"1", new Disease("1", "Disease B (temperature[37-38], result[2])", new Dictionary<string, ISymptomValidator>
                {
                    { "temperature", temperatureValidator2 },
                    { "question", abcValidator2 },
                }) },
                {"2", new Disease("2", "Disease C (temperature[38-39], result[3])", new Dictionary<string, ISymptomValidator>
                {
                    { "temperature", temperatureValidator3 },
                    { "question", abcValidator3 },
                }) },
                {"3", new Disease("3", "Disease D (temperature[37.5-38.5], result[1])", new Dictionary<string, ISymptomValidator>
                {
                    { "temperature", temperatureValidator4 },
                    { "question", abcValidator4 },
                }) },
            };

            var questions = new List<QuestionBase>
            {
                temperatureQuestion,
                abcQuestion,
            };

            var test = new Test(diseases.Values, questions);

            var diseasesProvider = new DummyDiseasesStorageProvider(diseases);
            var storageProvider = new DummyStorageProvider(diseasesProvider);
            var authorizationService = new DummyAuthorizationService(storageProvider);
            var medicamentsProvider = new DummyMedicamentsStorageProvider(
                new[] 
                {
                    new Medicament("0", "Medicament A", 3, 7),
                    new Medicament("1", "Medicament B", 1, 14),
                    new Medicament("2", "Medicament C", 2, 4),
                }, 
                new[] 
                {
                    new Medicament("3", "Medicament D", 1, 7),
                    new Medicament("4", "Medicament E", 2, 14),
                    new Medicament("5", "Medicament F", 2, 28),
                });

            var flow = new WorkFlow();

            flow.Append(new AuthorizationTask(authorizationService));
            flow.Append(new ConditionTask(new TestingTask(test), () => User.Instance.CurrentDisease == null));
            flow.Append(new ConditionTask(new ShowMedicamentsTask(medicamentsProvider), () => User.Instance.CurrentDisease != null));

            flow.Append(new MenuTask(
                new Dictionary<string, ITaskHandler>
                    {
                        { "Register", new RegisterMedicamentTask(medicamentsProvider) },
                        { "Show all", new ShowAllMedicamentsTask(medicamentsProvider) },
                        { "Show current", new ShowMedicamentsTask(medicamentsProvider) },
                        { "Exit", new ApplicationQuitTask() },
                    }));

            flow.Run();
        }

        static void Main(string[] args)
        {
            DummyInjectorInitialization();
        }
    }
}
