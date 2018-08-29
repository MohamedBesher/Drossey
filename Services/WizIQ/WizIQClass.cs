using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;

namespace Drossey.Services.WizIQ
{
    /// <summary>
    /// Summary description for AddAttendees
    /// </summary>
    public class WizIQClass: IWizIQClass
    {
        public readonly IConfiguration _config;

        public WizIQClass(IConfiguration config)
        {
            _config = config;
        }
        public string AddAttendees(string userName, string id,string classId)
        {
           
            string ServiceRootUrl = _config["WizIQ:ServiceRootUrl"];
            //private secret Key
            var secretAcessKey = _config["WizIQ:secretAcessKey"];
            // Build the Query Parameters to Generate Signature
            var requestParameters = new Dictionary<string, string>();
            requestParameters["access_key"] = _config["WizIQ:access_key"];
            requestParameters["timestamp"] = AuthBase.GenerateTimeStamp();
            requestParameters["method"] = "add_attendees";
            // Step 1 Generate Signature using secretAcessKey
            AuthBase authBase = new AuthBase();
            var signature = authBase.GenerateSignature(secretAcessKey, requestParameters);

            // Step 2 Add additional Query Params for the WiZiQ REST Service
            // Construct requestParameters and call WiZiQ endpoint to add attendees to a class
            // Required parameters
            requestParameters["signature"] = signature;
            
            //Required for Time-based class
            requestParameters["class_id"] = classId;
            //Required for permanent class
            //requestParameters["class_master_id"] = "29783";
            //requestParameters["perma_class"] = "true";
            //requestParameters["attendee_list"] = "<attendee_list><attendee><attendee_id><![CDATA[101]]></attendee_id><screen_name><![CDATA[Attendee1]]></screen_name><language_culture_name><![CDATA[en-us]]></language_culture_name></attendee><attendee><attendee_id><![CDATA[102]]></attendee_id><screen_name><![CDATA[Attendee2]]></screen_name><language_culture_name><![CDATA[en-us]]></language_culture_name></attendee></attendee_list>";

            requestParameters["attendee_list"]="<attendee_list>" +
                "<attendee>" +
                "<attendee_id><![CDATA["+ id +"]]></attendee_id>" +
                "<screen_name><![CDATA["+ userName +"]]></screen_name>" +
                "<language_culture_name><![CDATA[en-us]]>" +
                "</language_culture_name>" +
                "</attendee>" +
                "</attendee_list>";

            HttpRequest oRequest = new HttpRequest();
            return oRequest.WiZiQWebRequest(ServiceRootUrl + "method=add_attendees", requestParameters);

        }

        // create virtual class
        public string Create(string start_time,string presenter_email,string title,
                             string time_zone,string attendee_limit,string duration,string create_recording,string language_culture_name)
        {
            string ServiceRootUrl = _config["WizIQ:ServiceRootUrl"];
            //private secret Key
            var secretAcessKey = _config["WizIQ:secretAcessKey"];
            // Build the Query Parameters to Generate Signature
            var requestParameters = new Dictionary<string, string>();
            requestParameters["access_key"] = _config["WizIQ:access_key"];

            requestParameters["timestamp"] = AuthBase.GenerateTimeStamp();
            requestParameters["method"] = "create";

            // Step 1 Generate Signature using secretAcessKey
            AuthBase authBase = new AuthBase();
            var signature = authBase.GenerateSignature(secretAcessKey, requestParameters);

            // Step 2 Add additional Query Params for the WiZiQ REST Service
            // Construct requestParameters and call WiZiQ endpoint to create a class
            // Required parameters
            requestParameters["signature"] = signature;
            //01 / 25 / 2018 01:12
            requestParameters["start_time"] = start_time;

            //for teacher account pass parameter 'presenter_email'

            //This is the unique email of the presenter that will identify the presenter in WizIQ. Make sure to add 
            //this presenter email to your organization’s teacher account.
            //For more information visit at: (http://developer.wiziq.com/faqs)
            requestParameters["presenter_email"] = presenter_email;

            //presenter_id and presenter_name used for room based account
            //requestParameters["presenter_id"] = "123";
            //requestParameters["presenter_name"] = "veenu george";

            requestParameters["title"] = title;

            // Optional parameters
            requestParameters["time_zone"] = time_zone;
            requestParameters["attendee_limit"] = attendee_limit;
            requestParameters["duration"] = duration;//in mins        
            requestParameters["presenter_default_controls"] = "audio, video";
            requestParameters["attendee_default_controls"] = "audio";
            //true
            requestParameters["create_recording"] = create_recording;
            requestParameters["return_url"] = "";
            requestParameters["status_ping_url"] = "";
            requestParameters["language_culture_name"] = language_culture_name;
            //"en-us"
            HttpRequest oRequest = new HttpRequest();
            return oRequest.WiZiQWebRequest(ServiceRootUrl + "method=create", requestParameters);

        }

        public  string CreatePerma()
        {

            string ServiceRootUrl = _config["WizIQ:ServiceRootUrl"];
            //private secret Key
            var secretAcessKey = _config["WizIQ:secretAcessKey"];
            // Build the Query Parameters to Generate Signature
            var requestParameters = new Dictionary<string, string>();
            requestParameters["access_key"] = _config["WizIQ:access_key"];

            requestParameters["timestamp"] = AuthBase.GenerateTimeStamp();
            requestParameters["method"] = "create_perma_class";

            // Step 1 Generate Signature using secretAcessKey
            AuthBase authBase = new AuthBase();
            var signature = authBase.GenerateSignature(secretAcessKey, requestParameters);

            // Step 2 Add additional Query Params for the WiZiQ REST Service
            // Construct requestParameters and call WiZiQ endpoint to create a class
            // Required parameters
            requestParameters["signature"] = signature;

            //for teacher account pass parameter 'presenter_email'

            //This is the unique email of the presenter that will identify the presenter in WizIQ. Make sure to add 
            //this presenter email to your organization’s teacher account.
            //For more information visit at: (http://developer.wiziq.com/faqs)
            requestParameters["presenter_email"] = "PresenterEmail@gmail.com";

            //presenter_id and presenter_name used for room based account
            //requestParameters["presenter_id"] = "123";
            //requestParameters["presenter_name"] = "veenu george";

            requestParameters["title"] = "Mathematics";

            // Optional parameters
            requestParameters["attendee_limit"] = "10";
            requestParameters["presenter_default_controls"] = "audio, video";
            requestParameters["attendee_default_controls"] = "audio";
            requestParameters["create_recording"] = "true";
            requestParameters["return_url"] = "";
            requestParameters["status_ping_url"] = "";
            requestParameters["language_culture_name"] = "en-us";

            HttpRequest oRequest = new HttpRequest();
            return oRequest.WiZiQWebRequest(ServiceRootUrl + "method=create_perma_class", requestParameters);

        }

        public  string Modify(string classId,string start_time, string presenter_email, string title,
                             string time_zone, string attendee_limit, string duration, string create_recording, string language_culture_name)
        {
            string ServiceRootUrl = _config["WizIQ:ServiceRootUrl"];
            //private secret Key
            var secretAcessKey = _config["WizIQ:secretAcessKey"];
            // Build the Query Parameters to Generate Signature
            var requestParameters = new Dictionary<string, string>();
            requestParameters["access_key"] = _config["WizIQ:access_key"];

            requestParameters["timestamp"] = AuthBase.GenerateTimeStamp();
            requestParameters["method"] = "modify";

            // Step 1 Generate Signature using secretAcessKey
            AuthBase authBase = new AuthBase();
            var signature = authBase.GenerateSignature(secretAcessKey, requestParameters);

            // Step 2 Add additional Query Params for the WiZiQ REST Service
            // Construct requestParameters and call WiZiQ endpoint to modify a class
            // Required parameters
            requestParameters["signature"] = signature;

            //Required for Time-based class
            requestParameters["class_id"] = classId;
            //Required for permanent class
            //requestParameters["class_master_id"] = "29783";
            //requestParameters["perma_class"] = "true";

            // Optional parameters
            requestParameters["start_time"] = start_time;
            //presenter_id and presenter_name used for room based account
            //requestParameters["presenter_id"] = "123";
            //requestParameters["presenter_name"] = "veenu george";
            requestParameters["presenter_email"] = presenter_email;
            requestParameters["title"] = title;
            requestParameters["time_zone"] = time_zone;
            requestParameters["attendee_limit"] = attendee_limit;
            requestParameters["duration"] = duration;//in mins        
            requestParameters["presenter_default_controls"] = "audio, video";
            requestParameters["attendee_default_controls"] = "audio";
           // true
            requestParameters["create_recording"] = create_recording;
            requestParameters["return_url"] = "";
            requestParameters["status_ping_url"] = "";
            requestParameters["language_culture_name"] = language_culture_name;

            HttpRequest oRequest = new HttpRequest();
            return oRequest.WiZiQWebRequest(ServiceRootUrl + "method=modify", requestParameters);

        }

        public string Cancel(string classId)
        {
            string ServiceRootUrl = _config["WizIQ:ServiceRootUrl"];
            //private secret Key
            var secretAcessKey = _config["WizIQ:secretAcessKey"];
            // Build the Query Parameters to Generate Signature
            var requestParameters = new Dictionary<string, string>();
            requestParameters["access_key"] = _config["WizIQ:access_key"];

            requestParameters["timestamp"] = AuthBase.GenerateTimeStamp();
            requestParameters["method"] = "cancel";

            // Step 1 Generate Signature using secretAcessKey
            AuthBase authBase = new AuthBase();
            var signature = authBase.GenerateSignature(secretAcessKey, requestParameters);

            // Step 2 Add additional Query Params for the WiZiQ REST Service
            // Construct requestParameters and call WiZiQ endpoint to cancel a class
            // Required parameters
            requestParameters["signature"] = signature;

            //Required for Time-based class
            requestParameters["class_id"] = classId;
            //Required for permanent class
            //requestParameters["class_master_id"] = "29783";
            //requestParameters["perma_class"] = "true";

            HttpRequest oRequest = new HttpRequest();
            return oRequest.WiZiQWebRequest(ServiceRootUrl + "method=cancel", requestParameters);

        }


        public  string GetData()
        {
            string ServiceRootUrl = _config["WizIQ:ServiceRootUrl"];
            //private secret Key
            var secretAcessKey = _config["WizIQ:secretAcessKey"];
            // Build the Query Parameters to Generate Signature
            var requestParameters = new Dictionary<string, string>();
            requestParameters["access_key"] = _config["WizIQ:access_key"];

            requestParameters["timestamp"] = AuthBase.GenerateTimeStamp();
            requestParameters["method"] = "get_data";

            // Step 1 Generate Signature using secretAcessKey
            AuthBase authBase = new AuthBase();
            var signature = authBase.GenerateSignature(secretAcessKey, requestParameters);

            // Step 2 Add additional Query Params for the WiZiQ REST Service
            // Construct requestParameters and call WiZiQ endpoint to grt class/classes information
            // Required parameters
            requestParameters["signature"] = signature;
            //requestParameters["class_id"] = "24393";          

            requestParameters["multiple_class_id"] = "24392,24393";

            HttpRequest oRequest = new HttpRequest();
            //Once you call method response xml will shows related class/classes information.             
            return oRequest.WiZiQWebRequest(ServiceRootUrl + "method=get_data", requestParameters);

        }

        public  string AddTeacher(string name,string email,
            string password,string phone_number,string mobile_number,
            string time_zone,string about_the_teacher,string can_schedule_class,
            string is_active,string postPath="")
        {
            string ServiceRootUrl = _config["WizIQ:ServiceRootUrl"];
            //private secret Key
            var secretAcessKey = _config["WizIQ:secretAcessKey"];
            // Build the Query Parameters to Generate Signature
            NameValueCollection requestParameters = new NameValueCollection();
            requestParameters["access_key"] = _config["WizIQ:access_key"];

            requestParameters["timestamp"] = AuthBase.GenerateTimeStamp();
            requestParameters.Add("method", "add_teacher");
            // Step 1 Generate Signature using secretAcessKey
            AuthBase authBase = new AuthBase();
            string signature = authBase.GenerateSignature(requestParameters["access_key"], secretAcessKey, requestParameters["timestamp"], requestParameters["method"]);

            // Step 2 Add additional Query Params for the WiZiQ REST Service
            // Construct requestParameters and call WiZiQ endpoint to add a teacher
            // Required parameters                     
            requestParameters["signature"] = signature;
            requestParameters.Add("name", name);
            //This is the unique email of the teacher that will identify the teacher in your WizIQ account.
            //For more information visit at: (http://developer.wiziq.com/faqs)
            requestParameters.Add("email", email);
            requestParameters.Add("password", password);
            //Optional parameters
            //"Asia/Tokyo"
            requestParameters.Add("phone_number", phone_number);
            requestParameters.Add("mobile_number", mobile_number);
            requestParameters.Add("time_zone",time_zone);
            requestParameters.Add("about_the_teacher", about_the_teacher);
            //0-Cannot Schedule(deafult)
            //1-Can Schedule

            requestParameters.Add("can_schedule_class", can_schedule_class);

            //0-Inactive
            //1-Active(default)
            requestParameters.Add("is_active", is_active);
            //image_url can have following extensions
            //{'.gif','.jpeg','.jpg','.png'}
            //if no image file is there pass this as empty string.
            //@"x://folder/img.jpeg"
            string postFilePath = postPath;
           HttpRequest oRequest = new HttpRequest();
            return oRequest.WiZiQWebRequest(ServiceRootUrl + "method=add_teacher", requestParameters, postFilePath);
        }
        public  string EditTeacher(string teacherId,string name, string email,
            string password, string phone_number, string mobile_number,
            string time_zone, string about_the_teacher, string can_schedule_class,
            string is_active, string postPath = "")
        {
            string ServiceRootUrl = _config["WizIQ:ServiceRootUrl"];
            //private secret Key
            var secretAcessKey = _config["WizIQ:secretAcessKey"];
            // Build the Query Parameters to Generate Signature
            NameValueCollection requestParameters = new NameValueCollection();
            requestParameters["access_key"] = _config["WizIQ:access_key"];
            requestParameters["timestamp"] = AuthBase.GenerateTimeStamp();
            requestParameters.Add("method", "edit_teacher");
            // Step 1 Generate Signature using secretAcessKey
            AuthBase authBase = new AuthBase();
            string signature = authBase.GenerateSignature(requestParameters["access_key"], secretAcessKey, requestParameters["timestamp"], requestParameters["method"]);

            // Step 2 Add additional Query Params for the WiZiQ REST Service
            // Construct requestParameters and call WiZiQ endpoint to edit a teacher
            // Required parameters                     
            requestParameters["signature"] = signature;
            requestParameters.Add("name", name);
            //This is the unique email of the teacher that will identify the teacher in your WizIQ account.
            //For more information visit at: (http://developer.wiziq.com/faqs)
            requestParameters.Add("email", email);
            requestParameters.Add("password", password);
            requestParameters.Add("teacher_id", teacherId);
            requestParameters.Add("phone_number", phone_number);
            requestParameters.Add("mobile_number", mobile_number);
            requestParameters.Add("time_zone", time_zone);
            requestParameters.Add("about_the_teacher", about_the_teacher);
            //0-Cannot Schedule Class(deafult)
            //1-Can Schedule Class
            requestParameters.Add("can_schedule_class", can_schedule_class);
            //0-Inactive Teacher
            //1-Active Teacher(default)
            requestParameters.Add("is_active", is_active);
            //image can have following extensions
            //{'.gif','.jpeg','.jpg','.png'}
            //if no image file is there pass this as empty string.
            string postFilePath = postPath;
            HttpRequest oRequest = new HttpRequest();
            return oRequest.WiZiQWebRequest(ServiceRootUrl + "method=edit_teacher", requestParameters, postFilePath);
        }

        public  string GetTeacherDetails(string teacherId)
        {
            string ServiceRootUrl = _config["WizIQ:ServiceRootUrl"];
            //private secret Key
            var secretAcessKey = _config["WizIQ:secretAcessKey"];
            // Build the Query Parameters to Generate Signature
            var requestParameters = new Dictionary<string, string>();
            requestParameters["access_key"] = _config["WizIQ:access_key"];
            requestParameters["timestamp"] = AuthBase.GenerateTimeStamp();
            requestParameters["method"] = "get_teacher_details";

            // Step 1 Generate Signature using secretAcessKey
            AuthBase authBase = new AuthBase();
            var signature = authBase.GenerateSignature(secretAcessKey, requestParameters);
            // Step 2 Add additional Query Params for the WiZiQ REST Service
            // Construct requestParameters and call WiZiQ endpoint to get teacher details
            // Required parameters
            requestParameters["signature"] = signature;
            //Optional Parameter
            requestParameters["teacher_id"] = "123456";
            HttpRequest oRequest = new HttpRequest();
            //Once you call method response xml will shows related teacher details.             
            return oRequest.WiZiQWebRequest(ServiceRootUrl + "method=get_teacher_details", requestParameters);
        }

        public  string DownloadRecording()
        {
            string ServiceRootUrl = _config["WizIQ:ServiceRootUrl"];
            //private secret Key
            var secretAcessKey = _config["WizIQ:secretAcessKey"];
            // Build the Query Parameters to Generate Signature
            var requestParameters = new Dictionary<string, string>();
            requestParameters["access_key"] = _config["WizIQ:access_key"];

            requestParameters["timestamp"] = AuthBase.GenerateTimeStamp();
            requestParameters["method"] = "download_recording";

            // Step 1 Generate Signature using secretAcessKey
            AuthBase authBase = new AuthBase();
            var signature = authBase.GenerateSignature(secretAcessKey, requestParameters);

            // Step 2 Add additional Query Params for the WiZiQ REST Service
            // Construct requestParameters and call WiZiQ endpoint to cancel a class
            // Required parameters
            requestParameters["signature"] = signature;
            requestParameters["class_id"] = "16854";

            //recording_format value can be zip, exe or mp4
            //mp4 recording is available only for classes conducted on WizIQ desktop app
            requestParameters["recording_format"] = "zip";

            HttpRequest oRequest = new HttpRequest();
            //Once you call method, it creates recording package. <message> node in response xml will shows related messages. 
            //< status_xml_path> will contain http xml path which will have all the information of recording package.
            return oRequest.WiZiQWebRequest(ServiceRootUrl + "method=download_recording", requestParameters);



        }
    }
}
