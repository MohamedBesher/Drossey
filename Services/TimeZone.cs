using Drossey.Data.Core.Dto;
using System.Collections.Generic;

namespace Drossey.Admin.Services
{
    public class TimeZone : ITimeZone
    {
        public List<Dto> GetLanguages()
        {
            List<Dto> languags = new List<Dto>()
            {

                 new Dto(){ Value= "en-US",Name="English-United States" },
                 new Dto(){ Value= "es-ES",Name=" Spanish - Spain" },
                 new Dto(){ Value= "pt-PT",Name="Portuguese - Portugal" },
                 new Dto(){ Value= "he-IL",Name=" Hebrew - Israel" },
                 new Dto(){ Value= "th-TH",Name="Thai - Thailand" },
                 new Dto(){ Value= "zh-HK",Name="Chinese - Hong Kong SAR" },
                 new Dto(){ Value= "ru-RU",Name="Russian - Russia" },
                 new Dto(){ Value= "ar-SA",Name="Arabic - Saudi Arabia" },
                 new Dto(){ Value= "fr-FR",Name="French - France" },
                 new Dto(){ Value= "pl-PL",Name="Polish - Poland" },
                 new Dto(){ Value= "cs-CZ",Name="Czech - Czech Republic" },
                 new Dto(){ Value= "fa-IR",Name=" Farsi/Persian-Iran" },
                 new Dto(){ Value= "nl-NL",Name="Dutch" },
                 new Dto(){ Value= "tr-TR",Name="Turkish" },
                 new Dto(){ Value= "it-IT",Name="Italian - Italy" },
                  new Dto(){ Value= "de-DE ",Name="German - Germany" },
                 new Dto(){ Value= "ms-MY ",Name="Malay - Malaysia " },
                  new Dto(){ Value= "el-GR ",Name="Greek - Greece " }


  };
 
 
  
  
 
 
 
  
 

            return languags;
        }

        public List<Dto> GetTimesZones()
        {
            List<Dto> TimeZones = new List<Dto>()
            {
            new Dto(){ Value="Africa/Bangui",Name="W. Central Africa Standard Time" },
            new Dto(){ Value="Africa/Cairo",Name="Egypt Standard Time" },
            new Dto(){ Value="Africa/Casablanca",Name="Morocco Standard Time" },
            new Dto(){ Value="Africa/Harare",Name="South Africa Standard Time" },
            new Dto(){ Value="Africa/Johannesburg",Name="South Africa Standard Time" },
            new Dto(){ Value="Africa/Lagos",Name="W. Central Africa Standard Time" },
            new Dto(){ Value="Africa/Monrovia",Name="Greenwich Standard Time" },
            new Dto(){ Value="Africa/Nairobi",Name="E. Africa Standard Time" },
            new Dto(){ Value="Africa/Windhoek",Name="Namibia Standard Time" },
            new Dto(){ Value="America/Anchorage",Name="Alaskan Standard Time" },
            new Dto(){ Value="America/Argentina/San_Juan",Name="Argentina Standard Time" },
            new Dto(){ Value="America/Asuncion",Name="Paraguay Standard Time" },
            new Dto(){ Value="America/Bahia",Name="Bahia Standard Time" },
            new Dto(){ Value="America/Bogota",Name="SA Pacific Standard Time" },
            new Dto(){ Value="America/Buenos_Aires",Name="Argentina Standard Time" },
            new Dto(){ Value="America/Caracas",Name="Venezuela Standard Time" },
            new Dto(){ Value="America/Cayenne",Name="SA Eastern Standard Time" },
            new Dto(){ Value="America/Chicago",Name="Central Standard Time" },
            new Dto(){ Value="America/Chihuahua",Name="Mountain Standard Time (Mexico)" },
            new Dto(){ Value="America/Cuiaba",Name="Central Brazilian Standard Time" },
            new Dto(){ Value="America/Denver",Name="Mountain Standard Time" },
            new Dto(){ Value="America/Fortaleza",Name="SA Eastern Standard Time" },
            new Dto(){ Value="America/Godthab",Name="Greenland Standard Time" },
            new Dto(){ Value="America/Guatemala",Name="Central America Standard Time" },
            new Dto(){ Value="America/Halifax",Name="Atlantic Standard Time" },
            new Dto(){ Value="America/Indianapolis",Name="US Eastern Standard Time" },
            new Dto(){ Value="America/Indiana/Indianapolis",Name="US Eastern Standard Time" },
            new Dto(){ Value="America/La_Paz",Name="SA Western Standard Time" },
            new Dto(){ Value="America/Los_Angeles",Name="Pacific Standard Time" },
            new Dto(){ Value="America/Mexico_City",Name="Mexico Standard Time" },
            new Dto(){ Value="America/Montevideo",Name="Montevideo Standard Time" },
            new Dto(){ Value="America/New_York",Name="Eastern Standard Time" },
            new Dto(){ Value="America/Noronha",Name="UTC-02" },
            new Dto(){ Value="America/Phoenix",Name="US Mountain Standard Time" },
            new Dto(){ Value="America/Regina",Name="Canada Central Standard Time" },
            new Dto(){ Value="America/Santa_Isabel",Name="Pacific Standard Time (Mexico)" },
            new Dto(){ Value="America/Santiago",Name="Pacific SA Standard Time" },
            new Dto(){ Value="America/Sao_Paulo",Name="E. South America Standard Time" },
            new Dto(){ Value="America/St_Johns",Name="Newfoundland Standard Time" },
            new Dto(){ Value="America/Tijuana",Name="Pacific Standard Time" },
            new Dto(){ Value="Antarctica/McMurdo",Name="New Zealand Standard Time" },
            new Dto(){ Value="Atlantic/South_Georgia",Name="UTC-02" },
            new Dto(){ Value="Asia/Almaty",Name="Central Asia Standard Time" },
            new Dto(){ Value="Asia/Amman",Name="Jordan Standard Time" },
            new Dto(){ Value="Asia/Baghdad",Name="Arabic Standard Time" },
            new Dto(){ Value="Asia/Baku",Name="Azerbaijan Standard Time" },
            new Dto(){ Value="Asia/Bangkok",Name="SE Asia Standard Time" },
            new Dto(){ Value="Asia/Beirut",Name="Middle East Standard Time" },
            new Dto(){ Value="Asia/Calcutta",Name="India Standard Time" },
            new Dto(){ Value="Asia/Colombo",Name="Sri Lanka Standard Time" },
            new Dto(){ Value="Asia/Damascus",Name="Syria Standard Time" },
            new Dto(){ Value="Asia/Dhaka",Name="Bangladesh Standard Time" },
            new Dto(){ Value="Asia/Dubai",Name="Arabian Standard Time" },
            new Dto(){ Value="Asia/Irkutsk",Name="North Asia East Standard Time" },
            new Dto(){ Value="Asia/Jerusalem",Name="Israel Standard Time" },
            new Dto(){ Value="Asia/Kabul",Name="Afghanistan Standard Time" },
            new Dto(){ Value="Asia/Kamchatka",Name="Kamchatka Standard Time" },
            new Dto(){ Value="Asia/Karachi",Name="Pakistan Standard Time" },
            new Dto(){ Value="Asia/Katmandu",Name="Nepal Standard Time" },
            new Dto(){ Value="Asia/Kolkata",Name="India Standard Time" },
            new Dto(){ Value="Asia/Krasnoyarsk",Name="North Asia Standard Time" },
            new Dto(){ Value="Asia/Kuala_Lumpur",Name="Singapore Standard Time" },
            new Dto(){ Value="Asia/Kuwait",Name="Arab Standard Time" },
            new Dto(){ Value="Asia/Magadan",Name="Magadan Standard Time" },
            new Dto(){ Value="Asia/Muscat",Name="Arabian Standard Time" },
            new Dto(){ Value="Asia/Novosibirsk",Name="N. Central Asia Standard Time" },
            new Dto(){ Value="Asia/Oral",Name="West Asia Standard Time" },
            new Dto(){ Value="Asia/Rangoon",Name="Myanmar Standard Time" },
            new Dto(){ Value="Asia/Riyadh",Name="Arab Standard Time" },
            new Dto(){ Value="Asia/Seoul",Name="Korea Standard Time" },
            new Dto(){ Value="Asia/Shanghai",Name="China Standard Time" },
            new Dto(){ Value="Asia/Singapore",Name="Singapore Standard Time" },
            new Dto(){ Value="Asia/Taipei",Name="Taipei Standard Time" },
            new Dto(){ Value="Asia/Tashkent",Name="West Asia Standard Time" },
            new Dto(){ Value="Asia/Tbilisi",Name="Georgian Standard Time" },
            new Dto(){ Value="Asia/Tehran",Name="Iran Standard Time" },
            new Dto(){ Value="Asia/Tokyo",Name="Tokyo Standard Time" },
            new Dto(){ Value="Asia/Ulaanbaatar",Name="Ulaanbaatar Standard Time" },
            new Dto(){ Value="Asia/Vladivostok",Name="Vladivostok Standard Time" },
            new Dto(){ Value="Asia/Yakutsk",Name="Yakutsk Standard Time" },
            new Dto(){ Value="Asia/Yekaterinburg",Name="Ekaterinburg Standard Time" },
            new Dto(){ Value="Asia/Yerevan",Name="Armenian Standard Time" },
            new Dto(){ Value="Atlantic/Azores",Name="Azores Standard Time" },
            new Dto(){ Value="Atlantic/Cape_Verde",Name="Cape Verde Standard Time" },
            new Dto(){ Value="Atlantic/Reykjavik",Name="Greenwich Standard Time" },
            new Dto(){ Value="Australia/Adelaide",Name="Cen. Australia Standard Time" },
            new Dto(){ Value="Australia/Brisbane",Name="E. Australia Standard Time" },
            new Dto(){ Value="Australia/Darwin",Name="AUS Central Standard Time" },
            new Dto(){ Value="Australia/Hobart",Name="Tasmania Standard Time" },
            new Dto(){ Value="Australia/Perth",Name="W. Australia Standard Time" },
            new Dto(){ Value="Australia/Sydney",Name="AUS Eastern Standard Time" },
            new Dto(){ Value="Etc/GMT",Name="UTC" },
            new Dto(){ Value="Etc/GMT+11",Name="UTC-11" },
            new Dto(){ Value="Etc/GMT+12",Name="Dateline Standard Time" },
            new Dto(){ Value="Etc/GMT+2",Name="UTC-02" },
            new Dto(){ Value="Etc/GMT-12",Name="UTC+12" },
            new Dto(){ Value="Europe/Amsterdam",Name="W. Europe Standard Time" },
            new Dto(){ Value="Europe/Athens",Name="GTB Standard Time" },
            new Dto(){ Value="Europe/Belgrade",Name="Central Europe Standard Time" },
            new Dto(){ Value="Europe/Berlin",Name="W. Europe Standard Time" },
            new Dto(){ Value="Europe/Brussels",Name="Romance Standard Time" },
            new Dto(){ Value="Europe/Budapest",Name="Central Europe Standard Time" },
            new Dto(){ Value="Europe/Dublin",Name="GMT Standard Time" },
            new Dto(){ Value="Europe/Helsinki",Name="FLE Standard Time" },
            new Dto(){ Value="Europe/Istanbul",Name="GTB Standard Time" },
            new Dto(){ Value="Europe/Kiev",Name="FLE Standard Time" },
            new Dto(){ Value="Europe/London",Name="GMT Standard Time" },
            new Dto(){ Value="Europe/Minsk",Name="E. Europe Standard Time" },
            new Dto(){ Value="Europe/Moscow",Name="Russian Standard Time" },
            new Dto(){ Value="Europe/Paris",Name="Romance Standard Time" },
            new Dto(){ Value="Europe/Sarajevo",Name="Central European Standard Time" },
            new Dto(){ Value="Europe/Warsaw",Name="Central European Standard Time" },
            new Dto(){ Value="Indian/Mauritius",Name="Mauritius Standard Time" },
            new Dto(){ Value="Pacific/Apia",Name="Samoa Standard Time" },
            new Dto(){ Value="Pacific/Auckland",Name="New Zealand Standard Time" },
            new Dto(){ Value="Pacific/Fiji",Name="Fiji Standard Time" },
            new Dto(){ Value="Pacific/Guadalcanal",Name="Central Pacific Standard Time" },
            new Dto(){ Value="Pacific/Guam",Name="West Pacific Standard Time" },
            new Dto(){ Value="Pacific/Honolulu",Name="Hawaiian Standard Time" },
            new Dto(){ Value="Pacific/Pago_Pago",Name="UTC-11" },
            new Dto(){ Value="Pacific/Port_Moresby",Name="West Pacific Standard Time" },
            new Dto(){ Value="Pacific/Tongatapu",Name="Tonga Standard Time" }
            };
            return TimeZones;
        }



    }


}

