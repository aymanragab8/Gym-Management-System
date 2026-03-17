namespace GymSystem.Domain.AppMeteData
{
    public static class Router
    {
        public const string ConstPart = "/Api/";

        public static class Auth
        {
            public const string Prefix = "Auth/";
            public const string Register = ConstPart + Prefix + "Register";
            public const string Login = ConstPart + Prefix + "Login";
            public const string RefreshToken = ConstPart + Prefix + "RefreshToken";
            public const string RevokeToken = ConstPart + Prefix + "RevokeToken";
        }
        public static class Member
        {
            public const string Prefix = "Member/";
            public const string GetAllMembers = ConstPart + Prefix + "List";
            public const string GetById = ConstPart + Prefix + "{id}";
            public const string CreateMember = ConstPart + Prefix + "Create";
            public const string UpdateMember = ConstPart + Prefix + "{id}";
            public const string DeleteMember = ConstPart + Prefix + "{id}";
            public const string ViewSubscriptions = ConstPart + Prefix + "{memberId}/Subscriptions";
        }
        public static class Subscription
        {
            public const string Prefix = "Subscription/";
            public const string GetAllSubscriptions = ConstPart + Prefix + "List";
            public const string GetById = ConstPart + Prefix + "{id}";
            public const string CreateSubscription = ConstPart + Prefix + "{memberId}/Create";
            public const string UpdateSubscription = ConstPart + Prefix + "{subId}";
            public const string DeleteSubscription = ConstPart + Prefix + "{subId}";
        }
        public static class Exercise
        {
            public const string Prefix = "Exercise/";
            public const string GetAllExercises = ConstPart + Prefix + "List";
            public const string CreateExercise = ConstPart + Prefix + "Create";
            public const string DeleteExercise = ConstPart + Prefix + "{exId}";
        }
        public static class WorkoutPlan
        {
            public const string Prefix = "WorkoutPlan/";
            public const string GetAllWorkoutPlans = ConstPart + Prefix + "List";
            public const string GetAllWorkoutPlansforMemeber = ConstPart + Prefix + "{memberId}/List";
            public const string CreateWorkoutPlan = ConstPart + Prefix + "{memberId}/Create";
            public const string UpdateWorkoutPlan = ConstPart + Prefix + "{workplanId}";
            public const string DeleteWorkoutPlan = ConstPart + Prefix + "{workplanId}";

            public const string GetExercisesByWorkoutPlanId = ConstPart + Prefix + "{workplanId}/exercises";
            public const string CreateExerciseToWorkoutPlan = ConstPart + Prefix + "{workplanId}/exercise/{exId}";
            public const string UpdateWorkoutPlanExercise = ConstPart + Prefix + "{workplanId}/exercise/{exId}";
            public const string DeleteExerciseFromWorkoutPlan = ConstPart + Prefix + "{workplanId}/exercise/{exId}";
        }
        public static class Attendance
        {
            public const string Prefix = "Attendance/";
            public const string CheckIn = ConstPart + Prefix + "CheckIn";
            public const string GetMemberAttendance = ConstPart + Prefix + "Member/{memberId}";
            public const string GetMemberAttendanceByDate = ConstPart + Prefix + "Date/" + "{date}";
            public const string DeleteMemberAttendance = ConstPart + Prefix + "Member/{memberId}";

        }
        public static class Payment
        {
            public const string Prefix = "Payment/";
            public const string GetAllPayments = ConstPart + Prefix + "List";
            public const string GetAllMemberPayments = ConstPart + Prefix + "Member/{memberId}";
            public const string GetPaymentById = ConstPart + Prefix + "{paymentId}";
            public const string ProcessPayment = ConstPart + Prefix + "Process";

        }


    }
}
