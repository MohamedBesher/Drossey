using Drossey.Areas.admin.Models;
using System;

namespace Drossey.Admin.Services
{
    public interface IWizIQSender
    {
        Tuple<string, string> Add_Attendees(string userName, string id, string classId);

        Tuple<string,string> AddTeacher(string name, string email,
            string password, string phone_number, string mobile_number,
            string time_zone, string about_the_teacher, string can_schedule_class,
            string is_active, string postPath = "");
       Tuple<string, string> EditTeacher(string techerId, string name, string email,
         string password, string phone_number, string mobile_number,
         string time_zone, string about_the_teacher, string can_schedule_class,
         string is_active, string postPath = "");
        TeacherViewModel GetTeacherDetails(string teacherId);

        Tuple<string, string> Create(string start_time, string presenter_email, string title,
                               string time_zone, string attendee_limit, string duration, string create_recording, string language_culture_name);

        Tuple<string, string> Modify(string classId, string start_time, string presenter_email, string title,
                           string time_zone, string attendee_limit, string duration, string create_recording, string language_culture_name);

    }

   
    }



