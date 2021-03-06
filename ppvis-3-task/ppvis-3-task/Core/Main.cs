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
            var diseasesHistoryProvider = new DummyDiseasesHistoryStorageProvider();

            var temperatureQuestion = new NumericQuestion("Your temperature");
            var abcQuestion = new SingleVariantQuestion("Choose one result (1, 2, 3)");
            var abcMultipleQuestion = new MultipleVariantsQuestion("Choose multiple results (1, 2, 3)");

            var temperatureValidator1 = new RangeNumericValidator(36f, 37.5f, temperatureQuestion);
            var temperatureValidator2 = new RangeNumericValidator(37f, 38f, temperatureQuestion);
            var temperatureValidator3 = new SmartRangeNumericValidator(38f, 39f, temperatureQuestion, new[] { "1" }, diseasesHistoryProvider);
            var temperatureValidator4 = new RangeNumericValidator(37.5f, 38.5f, temperatureQuestion);

            var abcValidator1 = new SingleVariantValidator(1, abcQuestion);
            var abcValidator2 = new SingleVariantValidator(2, abcQuestion);
            var abcValidator3 = new SingleVariantValidator(3, abcQuestion);
            var abcValidator4 = new SingleVariantValidator(1, abcQuestion);

            var abcMultipleValidator1 = new MultipleVariantsValidator(new[] { 1, 2 }, abcMultipleQuestion);
            var abcMultipleValidator2 = new MultipleVariantsValidator(new[] { 2, 3 }, abcMultipleQuestion);
            var abcMultipleValidator3 = new MultipleVariantsValidator(new[] { 1, 3 }, abcMultipleQuestion);
            var abcMultipleValidator4 = new MultipleVariantsValidator(new[] { 1, 2, 3 }, abcMultipleQuestion);

            var diseases = new Dictionary<string, Disease>
            {
                {"0", new Disease("0", "Disease A (temperature[36-37.5], result[1], multiple_result[1, 2])", new Dictionary<string, ISymptomValidator>
                {
                    { "temperature", temperatureValidator1 },
                    { "question", abcValidator1 },
                    { "multiple_question", abcMultipleValidator1 },
                }) },
                {"1", new Disease("1", "Disease B (temperature[37-38], result[2], multiple_result[2, 3])", new Dictionary<string, ISymptomValidator>
                {
                    { "temperature", temperatureValidator2 },
                    { "question", abcValidator2 },
                    { "multiple_question", abcMultipleValidator2 },
                }) },
                {"2", new Disease("2", "Disease C (temperature[38-39], result[3], multiple_result[1, 3]) (After A or B)", new Dictionary<string, ISymptomValidator>
                {
                    { "temperature", temperatureValidator3 },
                    { "question", abcValidator3 },
                    { "multiple_question", abcMultipleValidator3 },
                }) },
                {"3", new Disease("3", "Disease D (temperature[37.5-38.5], result[1], multiple_result[1, 2, 3])", new Dictionary<string, ISymptomValidator>
                {
                    { "temperature", temperatureValidator4 },
                    { "question", abcValidator4 },
                    { "multiple_question", abcMultipleValidator4 },
                }) },
            };

            var questions = new List<QuestionBase>
            {
                temperatureQuestion,
                abcQuestion,
                abcMultipleQuestion,
            };

            var test = new Test(diseases.Values, questions);

            var medicamentsHistoryProvider = new DummyMedicamentsHistoryStorageProvider(new Dictionary<string, List<string>>
            {
                { "0", new List<string> { "0", "1" } },
            });
            var medicamentsCompatibilityProvider = new DummyMedicamentsCompatibilityStorageProvider(new Dictionary<string, IEnumerable<string>>
            {
                { "0", new List<string> { "2", "3" } },
            });

            var diseasesProvider = new DummyDiseasesStorageProvider(diseases);
            var storageProvider = new DummyStorageProvider(diseasesProvider);
            var authorizationService = new DummyAuthorizationService(storageProvider);
            var pharmacyProvider = new DummyPharmacyStorageProvider(new Dictionary<string, bool>
            {
                { "4", true },
                { "5", true },
                { "6", false },
            });
            var medicamentsProvider = new SmartestMedicamentsStorageProvider(
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
                },
                new Dictionary<string, IEnumerable<string>>
                {
                    {"0", new[] { "1", "2" } },
                    {"1", new[] { "0", "2" } },
                    {"2", new[] { "4" } },
                    {"4", new[] { "2" } },
                },
                medicamentsHistoryProvider,
                medicamentsCompatibilityProvider,
                pharmacyProvider);

            var flow = new WorkFlow();

            flow.Append(new AuthorizationTask(authorizationService));
            flow.Append(new ConditionTask(new TestingTask(test), () => User.Instance.CurrentDisease == null));
            flow.Append(new ConditionTask(new ShowMedicamentsTask(medicamentsProvider), () => User.Instance.CurrentDisease != null));

            flow.Append(new MenuTask(
                new Dictionary<string, ITaskHandler>
                    {
                        { "Register", new RegisterMedicamentTask(medicamentsProvider) },
                        { "Unregister", new UnregisterMedicamentTask(medicamentsProvider) },
                        { "Show all", new ShowAllMedicamentsTask(medicamentsProvider) },
                        { "Show current", new ShowMedicamentsTask(medicamentsProvider) },
                        { "Disease finished", new DiseaseFinishedTask(medicamentsProvider, medicamentsHistoryProvider) },
                        { "Symptoms changed", new SymptomsChangedTask(new TestingTask(test), new ShowMedicamentsTask(medicamentsProvider), diseasesHistoryProvider) },
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
