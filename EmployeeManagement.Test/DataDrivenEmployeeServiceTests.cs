using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixtures;
using EmployeeManagement.Test.TestData;

namespace EmployeeManagement.Test;

[Collection("EmployeeServiceCollection")]
public class DataDrivenEmployeeServiceTests
{
    private readonly EmployeeServiceFixture _employeeServiceFixture;

    public DataDrivenEmployeeServiceTests(EmployeeServiceFixture employeeServiceFixture)
    {
        _employeeServiceFixture = employeeServiceFixture;
    }

    [Theory]
    [InlineData("37e03ca7-c730-4351-834c-b66f280cdb01")]
    [InlineData("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e")]
    public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveFirstObligatoryCourse(Guid courseId)
    {
        // Arrange


        // Act
        var internalEmployee = _employeeServiceFixture.EmployeeService.CreateInternalEmployee("Tomodo", "Yamato");

        //Assert
        Assert.Contains(internalEmployee.AttendedCourses,
            course => course.Id == courseId);
    }


    [Fact]
    public async Task GiveRaise_MinimumRaiseGiven_EmployeeMinimumRaiseGivenMustBeTrue()
    {
        // Arrange
        var internalEmployee = new InternalEmployee("Ivan", "Panchenko", 5, 3000, false, 1);

        // Act
        await _employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, 100);

        // Assert
        Assert.True(internalEmployee.MinimumRaiseGiven);
    }

    [Fact]
    public async Task GiveRaise_MoreThanMinimumRaiseGiven_EmployeeMinimumRaiseGivenMustBeTrue()
    {
        // Arrange
        var internalEmployee = new InternalEmployee("Ivan", "Panchenko", 5, 3000, false, 1);

        // Act
        await _employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, 200);

        // Assert
        Assert.False(internalEmployee.MinimumRaiseGiven);
    }

    public static IEnumerable<object[]> ExampleTestDataForGiveRaise_WithProperty
    {
        get
        {
            return new List<object[]>
            {
                new object[] {100, true},
                new object[] {200, false}
            };
        }
    }

    public static TheoryData<int, bool> StronglyTypedExampleTestDataForGiveRaise_WithProperty
    {
        get
        {
            return new TheoryData<int, bool>()
            {
                {100, true},
                {200, false}
            };
        }
    }

    public static IEnumerable<object[]> ExampleTestDataForGiveRaise_WithMethod(int testDataInstancedToProvide)
    {
        var testData = new List<object[]>
            {
                new object[] {100, true},
                new object[] {200, false}
            };
        return testData.Take(testDataInstancedToProvide);
    }

    [Theory]
    //[MemberData(nameof(ExampleTestDataForGiveRaise_WithMethod),
    //    1,
    //    MemberType = typeof(DataDrivenEmployeeServiceTests))]

    //[ClassData(typeof(EmployeeServiceTestData))]

    //[ClassData(typeof(StronglyTypedEmployeeServiceTestData))]
    
    //[MemberData(nameof(StronglyTypedExampleTestDataForGiveRaise_WithProperty))]
    [ClassData(typeof(StronglyTypedEmployeeServiceTestDataFromFile))]
    public async Task GiveRaise_RaiseGiven_EmployeeMinimumRaiseGivenMatchesValue(int raiseGiven,
        bool expectedValueForMinimumRaiseGiven)
    {
        // Arrange
        var internalEmployee = new InternalEmployee("Ivan", "Panchenko", 5, 3000, false, 1);

        // Act
        await _employeeServiceFixture.EmployeeService.GiveRaiseAsync(internalEmployee, raiseGiven);

        // Assert
        Assert.Equal(expectedValueForMinimumRaiseGiven, internalEmployee.MinimumRaiseGiven);
    }

}
