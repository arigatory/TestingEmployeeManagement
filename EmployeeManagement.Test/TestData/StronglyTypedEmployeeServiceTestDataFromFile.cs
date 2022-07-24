namespace EmployeeManagement.Test.TestData;

public class StronglyTypedEmployeeServiceTestDataFromFile : TheoryData<int, bool>
{
    public StronglyTypedEmployeeServiceTestDataFromFile()
    {
        var testData = File.ReadAllLines("TestData/EmployeeServiceTestData.csv");
        foreach (var line in testData)
        {
            var splitString = line.Split(',');
            if (int.TryParse(splitString[0], out int raise)
                && bool.TryParse(splitString[1], out bool minimumRaiseGiven))
            {
                Add(raise, minimumRaiseGiven);
            }
        }
    }
}
