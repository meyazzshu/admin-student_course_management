using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseRegistration.Exception
{
    public class AppExceptions
    {

    }

    public class InvalidCreditHoursException : System.Exception
    {
        public InvalidCreditHoursException(int totalCredits)
            : base($"Credit hours must be between 5 and 20. You tried: {totalCredits}") { }
    }

    public class DuplicateEmailException : System.Exception
    {
        public DuplicateEmailException(string email)
            : base($"The email '{email}' is already registered.") { }
    }

    public class DuplicateCourseRegistrationException : System.Exception
    {
        public DuplicateCourseRegistrationException(string courseId)
            : base($"The course '{courseId}' is already registered by this student.") { }
    }

    public class InvalidCredentialsException : System.Exception
    {
        public InvalidCredentialsException()
            : base("Invalid username or password.") { }
    }

    public class ScheduleConflictException : System.Exception
    {
        public ScheduleConflictException(string course1, string course2)
            : base($"Schedule conflict between '{course1}' and '{course2}'. Please resolve the conflict.")
        { }
    }

}