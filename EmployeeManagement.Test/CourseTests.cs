using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test;

public class CourseTests
{
    [Fact]
    public void CourseConstructor_ConsstructCourse_IsNewMustBeTrue()
    {
        // Arrange
        // nothing

        // Act
        var course = new Course("C# Professional API Testing 101");
        
        // Assert
        Assert.True(course.IsNew);
    }
}
