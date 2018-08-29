using Drossey.Services.WizIQ;
using System;
using System.Xml;
using Drossey.Areas.admin.Models;

namespace Drossey.Admin.Services
{
    public class WizIQSender : IWizIQSender
    {
        private readonly IWizIQClass _WizIQClass;

        public WizIQSender(IWizIQClass WizIQClass)
        {
            _WizIQClass = WizIQClass;
        }
        public Tuple<string, string> Add_Attendees(string userName, string id, string classId)
        {
            string attend_url = "";
            string returnXml = _WizIQClass.AddAttendees(userName, id, classId);
            //work with returnXml
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(returnXml);
            XmlNode root = xDoc.SelectSingleNode("rsp");
            string stat = root.Attributes["status"].Value;
            if (stat == "ok")
            {
                string attende = xDoc.SelectNodes("/rsp/add_attendees/attendee_list/attendee")
                    .Item(0)
                    .SelectSingleNode("attendee_url")
                    .InnerXml;
                attend_url = attende.Substring(9, attende.Length - 12);
                //process xml
            }
            else if (stat == "fail")
            {
                attend_url = xDoc.SelectNodes("/rsp/error")
                .Item(0)
                .Attributes["msg"].Value;

            }
            return Tuple.Create(attend_url, stat);

        }

        public Tuple<string,string> AddTeacher(string name, string email,
            string password, string phone_number, string mobile_number,
            string time_zone, string about_the_teacher, string can_schedule_class,
            string is_active, string postPath = "")
        {
            string teacherId = "";
            string returnXml = _WizIQClass.AddTeacher( name,  email,
             password,  phone_number,  mobile_number,
             time_zone,  about_the_teacher,  can_schedule_class,
             is_active,  postPath );
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(returnXml);
            XmlNode root = xDoc.SelectSingleNode("rsp");
            string stat = root.Attributes["status"].Value;
            if (stat == "ok")
            {
                teacherId = xDoc.SelectNodes("/rsp/add_teacher/teacher_id")
                  .Item(0)
                  .InnerXml;

                //process xml
            }
            else if (stat == "fail")
            {
                teacherId = xDoc.SelectNodes("/rsp/error")
                .Item(0)
                .Attributes["msg"].Value;

            }
            return Tuple.Create(teacherId, stat);

            //           < rsp status = 'fail' >
            //< error code = '403' msg = 'Forbidden: The request is refused by the API.' />
            //   </ rsp >
        }


        public Tuple<string, string> EditTeacher (string techerId,string name, string email,
            string password, string phone_number, string mobile_number,
            string time_zone, string about_the_teacher, string can_schedule_class,
            string is_active, string postPath = "")
        {
            string teacherId = "";
            string returnXml = _WizIQClass.EditTeacher(techerId,name,email,
             password,phone_number,mobile_number,
             time_zone,  about_the_teacher,  can_schedule_class,
             is_active,  postPath = "");
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(returnXml);
            XmlNode root = xDoc.SelectSingleNode("rsp");
            string stat = root.Attributes["status"].Value;
            if (stat == "ok")
            {
                teacherId = xDoc.SelectNodes("/rsp/edit_teacher/teacher_id")
                  .Item(0)
                  .InnerXml;

                //process xml
            }
            else if (stat == "fail")
            {
                teacherId = xDoc.SelectNodes("/rsp/error")
                .Item(0)
                .Attributes["msg"].Value;

            }
            return Tuple.Create(teacherId, stat);
        }

        public TeacherViewModel GetTeacherDetails(string teacherId)
        {
            string returnXml = _WizIQClass.GetTeacherDetails(teacherId);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(returnXml);
            XmlNode root = xDoc.SelectSingleNode("rsp");
            string stat = root.Attributes["status"].Value;
            if (stat == "ok")
            {

                //process xml
            }
            else
            {

            }
            return new TeacherViewModel();
        }


        public Tuple<string, string> Create(string start_time, string presenter_email, string title,
                             string time_zone, string attendee_limit, string duration, string create_recording, string language_culture_name)
        {
            string classId = "";
            string returnXml = _WizIQClass.Create( start_time,  presenter_email,  title,
                              time_zone,  attendee_limit,  duration,  create_recording,  language_culture_name);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(returnXml);
            XmlNode root = xDoc.SelectSingleNode("rsp");
            string stat = root.Attributes["status"].Value;
            if (stat == "ok")
            {
                 classId = xDoc.SelectNodes("/rsp/create/class_details/class_id")
                  .Item(0)
                  .InnerXml;
            }
            else if (stat == "fail")
            {
                classId = xDoc.SelectNodes("/rsp/error")
                .Item(0)
                .Attributes["msg"].Value;

            }
            return Tuple.Create(classId, stat);
        }
        public Tuple<string, string> Modify(string classId, string start_time, string presenter_email, string title,
                             string time_zone, string attendee_limit, string duration, string create_recording, string language_culture_name)
        {
            string returnXml = _WizIQClass.Modify(classId,start_time,presenter_email,title,
                              time_zone,attendee_limit,duration,create_recording,language_culture_name);

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(returnXml);
            XmlNode root = xDoc.SelectSingleNode("rsp");
            string stat = root.Attributes["status"].Value;
            if (stat == "ok")
            {
                classId = xDoc.SelectNodes("/rsp/create/class_details/class_id")
                 .Item(0)
                 .InnerXml;
            }
            else if (stat == "fail")
            {
                classId = xDoc.SelectNodes("/rsp/error")
                .Item(0)
                .Attributes["msg"].Value;

            }
            return Tuple.Create(classId, stat);
        }
    }
}
