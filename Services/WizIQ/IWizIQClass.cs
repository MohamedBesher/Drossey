namespace Drossey.Services.WizIQ
{
    public interface IWizIQClass
    {
        string AddAttendees(string userName, string id, string classId);
        string Create(string start_time, string presenter_email, string title,
                             string time_zone, string attendee_limit, string duration, string create_recording, string language_culture_name);
        string CreatePerma();
        string Modify(string classId, string start_time, string presenter_email, string title,
                             string time_zone, string attendee_limit, string duration, string create_recording, string language_culture_name);
        string Cancel(string classId);
        string GetData();
        string AddTeacher(string name, string email,
            string password, string phone_number, string mobile_number,
            string time_zone, string about_the_teacher, string can_schedule_class,
            string is_active, string postPath = "");
        string EditTeacher(string teacherId, string name, string email,
            string password, string phone_number, string mobile_number,
            string time_zone, string about_the_teacher, string can_schedule_class,
            string is_active, string postPath = "");
        string GetTeacherDetails(string teacherId);
        string DownloadRecording();
    }
}