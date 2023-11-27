namespace Mvc_day2.Common
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete"
            };
        }

        public static List<string> GenerateAllPermissions()
        {
            var allPermissions = new List<string>();

            var modules = Enum.GetValues(typeof(Modules));

            foreach (var module in modules)
                allPermissions.AddRange(GeneratePermissionsList(module.ToString()));

            return allPermissions;
        }

        public static class Course
        {
            public const string View = "Permissions.Course.View";
            public const string Create = "Permissions.Course.Create";
            public const string Edit = "Permissions.Course.Edit";
            public const string Delete = "Permissions.Course.Delete";
        }
        public static class Department
        {
            public const string View = "Permissions.Department.View";
            public const string Create = "Permissions.Department.Create";
            public const string Edit = "Permissions.Department.Edit";
            public const string Delete = "Permissions.Department.Delete";
        }
        public static class Student
        {
            public const string View = "Permissions.Student.View";
            public const string Create = "Permissions.Student.Create";
            public const string Edit = "Permissions.Student.Edit";
            public const string Delete = "Permissions.Student.Delete";
        }
        public static class Instructor
        {
            public const string View = "Permissions.Instructor.View";
            public const string Create = "Permissions.Instructor.Create";
            public const string Edit = "Permissions.Instructor.Edit";
            public const string Delete = "Permissions.Instructor.Delete";
        }






    }
}
