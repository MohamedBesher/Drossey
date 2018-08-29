var quiz = {
    "questions": [
        // single choice text
        /* {
            "id": 1,
            "type": "singleChoiceTxt",
            "header": "إختر الإجابة الصحيحة ",
            "body": "السؤال",
            'co_answer': "صحيحة",
            'wro_answer': [
                { 'wrong': "خطأ" },
                { 'wrong': "خطأ" }
            ],
            "rightfeedback": "احسنت اجابتك صحيحة ",
            "wrongfeedback": "الاجابة غير صحيحة "
        },*/
        //true or false quiz
        /*{
            "id": 2,
            "type": "trueFalse",
            "header": "حدد الجملة صح أم خطأ:",
            "body": "السؤال",
            'co_answer': true, //true or false
            "rightfeedback": "احسنت اجابتك صحيحة ",
            "wrongfeedback": "الاجابة غير صحيحة "
        },*/
        // multiple choice text
        /*{
            "id":3,
            "type":"multiChoiceTxt",
            "header": "إختر الإجابة الصحيحة ",
            "body": "السؤال",
            'co_answer':[
                {'correct':"الجملة"},
                {'correct':"الجملة"},
                {'correct':"الجملة"}
            ],
            'wro_answer':[
                {'wrong':"خطأ"},
                {'wrong':"خطأ"},
                {'wrong':"خطأ"},
                {'wrong':"خطأ"}
            ],
            "rightfeedback":"احسنت اجابتك صحيحة ",
            "partialfeedback":"اجابة غير دقيقة   ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },*/
        // single choice image
        /*{
            "id":4,
            "type":"singleChoiceImg",
            "header": "إختر الإجابة الصحيحة ",
            "body": " السؤال",
            'co_img_src':"../Player/lessons/lesson_01/media/pics/2.jpg",
            'wro_img_src':[
                {'wrong':"../Player/lessons/lesson_01/media/pics/1.jpg"},
                {'wrong':"../Player/lessons/lesson_01/media/pics/1.jpg"},
                {'wrong':"../Player/lessons/lesson_01/media/pics/1.jpg"}
            ],
            "rightfeedback":"احسنت اجابتك صحيحة ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },*/
        // multi choice image
        /*{
            "id":5,
            "type":"multiChoiceImg",
            "header": "إختر الإجابة الصحيحة ",
            "body": "  السؤال",
            'co_img_src':[
                {'correct':"../Player/lessons/lesson_01/media/pics/2.jpg"},
                {'correct':"../Player/lessons/lesson_01/media/pics/2.jpg"},
                {'correct':"../Player/lessons/lesson_01/media/pics/2.jpg"}
            ],
            'wro_img_src':[
                {'wrong':"../Player/lessons/lesson_01/media/pics/1.jpg"},
                {'wrong':"../Player/lessons/lesson_01/media/pics/1.jpg"},
                {'wrong':"../Player/lessons/lesson_01/media/pics/1.jpg"},
                {'wrong':"../Player/lessons/lesson_01/media/pics/1.jpg"},
                {'wrong':"../Player/lessons/lesson_01/media/pics/1.jpg"}
            ],
            "partialfeedback":"اجابة غير دقيقة",
            "rightfeedback":"احسنت اجابتك صحيحة ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },*/
        //essay quiz
        /*{
            "id": 6,
            "type": "essay",
            "header": "اجب على الأسئلة التالية",
            "body": "السؤال ؟؟",
            "feedback_title": "عنوان الاجابة",
            "rightfeedback": "نص الاجابة"
        },*/
        //fillBlanK quiz
        /*{
            "id": 7,
            "type": "fillBlanK",
            'example':true, // true mean this question is example else false
            "header": "اكمل الفراغات ",
            "body": "ان للبيانات انواع ===== منها البيانات الرقمية الصحيحة والبيانات الرقمية غير الصحيحة او ===== و البيانات الحرفية  نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي ===== نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي ",//question body 
            "blankwords":[
                {'correct':"صحيحة"},
                {'correct':"صحيحة"},
                {'correct':"صحيحة"}
                
            ],//repalce words in question body by ( =====)example =====
            "partialfeedback":"اجابة غير دقيقة",
            "rightfeedback":"احسنت اجابتك صحيحة ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },*/
        //fillDropDown quiz
        /*{
            "id": 7,
            "type": "fillDropDown",
            "header": " اختر الكلمة المناسبة ",
            "body": "ان للبيانات انواع ===== منها البيانات الرقمية الصحيحة والبيانات الرقمية غير الصحيحة او ===== و البيانات الحرفية  نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي ===== نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي نص افتراضي ",//question body 
            "blankwords":[
                {'correct':"صحيحة",'wrong':["خطأ","خطأ","خطأ"]},
                {'correct':"صحيحة",'wrong':["خطأخطأ"]},
                {'correct':"صحيحة",'wrong':["خطأ","خطأ"]}
                
            ],//repalce words in question body by ( =====)example =====
            "partialfeedback":"اجابة غير دقيقة",
            "rightfeedback":"احسنت اجابتك صحيحة ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },*/
          //fillByDragDrop quiz
        /*{
            "id": 8,
            "type": "fillByDragDrop",
            "header": " اختر الكلمة المناسبة ",
            "question": [//repalce answer in question body by ( =====)example =====
                {"body":"السؤال الاول ===== و اعتد علي","answer":"صحيحة1"},
                {"body":"ان ===== الاعتماد علي الواقع","answer":"صحيحة2"},
                {"body":"===== حياة الانسان  لا تعتمد ","answer":"صحيحة3"},
                {"body":"المفاعلات النووية خطيرة جدا في التعامل =====","answer":"صحيحة4"}
            ],//question body 
            'wrong':["خطأ","خطأ","خطأ"],
            "partialfeedback":"اجابة غير دقيقة",
            "rightfeedback":"احسنت اجابتك صحيحة ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },*/
        
        //fillBlanK quiz
//       
        
        
//        
        
         {
            "id": 7,
            "type": "fillBlanK",
            'example':true, // true mean this question is example else false
            "header": "أكمل ما يأتي:",
            "body": " عندما يكون ميل نصف الكرة الأرضية الشَّمالي نحو ===== تزداد شدَّة الضَّوء والحرارة السَّاقطة عليه.",//question body 
            "blankwords":[
                {'correct':"الشَّمس"},
                
            ],//repalce words in question body by ( =====)example =====
            "partialfeedback":"اجابة غير دقيقة",
            "rightfeedback":"احسنت إجابة صحيحة ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },
  {
            "id": 7,
            "type": "fillBlanK",
            'example':true, // true mean this question is example else false
            "header": "أكمل ما يأتي:",
            "body": " المدار هو المسار ===== أو شبه الدائريّ الذي يسلكه الجسم المتحرِّك حول جسم آخر.  ",//question body 
            "blankwords":[
                {'correct':"الدائريّ"},
                
            ],//repalce words in question body by ( =====)example =====
            "partialfeedback":"اجابة غير دقيقة",
            "rightfeedback":"احسنت إجابة صحيحة ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },
  {
            "id": 7,
            "type": "fillBlanK",
            'example':true, // true mean this question is example else false
            "header": "أكمل ما يأتي:",
            "body": "  تحدث الفصول الأربعة بسبب ميلان ===== الأرض، وبسبب دورانِها حول الشَّمس. ",//question body 
            "blankwords":[
                {'correct':"محور"},
                
            ],//repalce words in question body by ( =====)example =====
            "partialfeedback":"اجابة غير دقيقة",
            "rightfeedback":"احسنت إجابة صحيحة ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },

        
        
        
             //fillBlanK quiz
//        {
//            "id": 7,
//            "type": "fillBlanK",
//            'example':false, // true mean this question is example else false
//            "header": "أكمل ما يأتي:",
//            "body": "  في المعادلة -3(6+5ص)=2(-5+4ص)، فإن ص =  ===== ",//question body 
//            "blankwords":[
//                {'correct':"<img  src='../Data/lessons/G09_T01_U01_L04/images/G09_T01_U01_L04_P07_quiz_1.png'>"},
////                {'correct':"صحيحة"},
////                {'correct':"صحيحة"}
//                
//            ],//repalce words in question body by ( =====)example =====
//            "partialfeedback":"اجابة غير دقيقة",
//            "rightfeedback":"احسنت اجابتك صحيحة ",
//            "wrongfeedback":"الاجابة غير صحيحة "
//        },
        
                     //fillBlanK quiz
//        {
//            "id": 7,
//            "type": "fillBlanK",
//            'example':false, // true mean this question is example else false
//            "header": "أكمل ما يأتي:",
//            "body": "   في المعادلة 9(4ب-1)=2(9ب+3) ، فإن ب = ===== ",//question body 
//            "blankwords":[
//                {'correct':"<img  src='../Data/lessons/G09_T01_U01_L04/images/G09_T01_U01_L04_P07_quiz_2.png'> "},
////                {'correct':"صحيحة"},
////                {'correct':"صحيحة"}
//                
//            ],//repalce words in question body by ( =====)example =====
//            "partialfeedback":"اجابة غير دقيقة",
//            "rightfeedback":"احسنت اجابتك صحيحة ",
//            "wrongfeedback":"الاجابة غير صحيحة "
//        },
//         {
//            "id": 7,
//            "type": "fillBlanK",
//            'example':false, // true mean this question is example else false
//            "header": "أكمل ما يأتي:",
//            "body": "  <img  src='../Data/lessons/G09_T01_U01_L04/images/G09_T01_U01_L04_P07_quiz_3.png'>   ",//question body 
//            "blankwords":[
//                {'correct':"<img  src='../Data/lessons/G09_T01_U01_L04/images/G09_T01_U01_L04_P07_quiz_4.png'>"},
////                {'correct':"صحيحة"},
////                {'correct':"صحيحة"}
//                
//            ],//repalce words in question body by ( =====)example =====
//            "partialfeedback":"اجابة غير دقيقة",
//            "rightfeedback":"احسنت اجابتك صحيحة ",
//            "wrongfeedback":"الاجابة غير صحيحة "
//        },
     
        
        
        
        //true or false quiz
        {
            "id": 2,
            "type": "trueFalse",
            "header": "حدد الجملة صحيحة أم خاطئة:",
            "body": " فصل الصيف يبدأ من 21 ديسمبر. ",
            'co_answer': false, //true or false
            "rightfeedback": "أحسنت إجابتك صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        }, 
        
               //true or false quiz
        {
            "id": 2,
            "type": "trueFalse",
            "header": "حدد الجملة صحيحة أم خاطئة:",
            "body": "لا تدور الأرض حول محورها فقط، وإنَّما تدور أيضا حول الشَّمس في مدار إهليلجيٍّ. ",
            'co_answer': true, //true or false
            "rightfeedback": "أحسنت إجابتك صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        }, 
        
               //true or false quiz
        {
            "id": 2,
            "type": "trueFalse",
            "header": "حدد الجملة صحيحة أم خاطئة:",
            "body": "   حدث تلوث طبقة الأوزون بفعل استخدام مركبات الفريون. ",
            'co_answer': true, //true or false
            "rightfeedback": "أحسنت إجابتك صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        }, 
       
     
       
        
       
        
        
        
        
        // single choice text
         {
            "id": 1,
            "type": "singleChoiceTxt",
            "header": "إختر الإجابة الصحيحة ",
            "body": "   ....................هي الدورة التي تتم فيها الأرض دورة كاملة حول محورها كل يوم. ",
            'co_answer': "دورة الأرض اليومية ",
            'wro_answer': [
                { 'wrong': "دورة القمر اليومية " },
                { 'wrong': "دورة الأرض السنوية" }
            ],
            "rightfeedback": "أحسنت إجابتك صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        }, 
        
            // single choice text
         {
            "id": 1,
            "type": "singleChoiceTxt",
            "header": "إختر الإجابة الصحيحة ",
            "body": ".....................خط حقيقي أو وهمى يدور حوله الجسم. ",
            'co_answer': "المحور ",
            'wro_answer': [
                { 'wrong': "الطول " },
                { 'wrong': "العرض " }
            ],
            "rightfeedback": "أحسنت إجابتك صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        }, 
        
           // single choice text
         {
            "id": 1,
            "type": "singleChoiceTxt",
            "header": "إختر الإجابة الصحيحة ",
            "body": "فصل يبدأ في 20 مارس حتى 21 يونيو.........................",
            'co_answer': "الربيع",
            'wro_answer': [
                { 'wrong': "الشتاء " },
                { 'wrong': "الخريف" },
//                { 'wrong': "الفاصولياء" },

            ],
            "rightfeedback": "أحسنت إجابتك صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        }, 
        
        {
            "id": 6,
            "type": "essay",
            "header": "اجب على الأسئلة التالية",
            "body": "دورة الأرض السنوية",
            "feedback_title": "الإجابة الصحيحة",
            "rightfeedback": "هي الدورة التي يستغرقُ فيها دورانُ الأرضِ حولَ الشَّمس ٣٦٥٫٢٥ يومًا، أي: سنةً ميلادية واحدة."
        },
        
        {
            "id": 6,
            "type": "essay",
            "header": "اجب على الأسئلة التالية",
            "body": "حدوث الفصول الأربعة.",
            "feedback_title": "الإجابة الصحيحة",
            "rightfeedback": "عندما يكون مَيْلُ نصفِ الكرة الشَّمالي نحو الشَّمس تزدادُ شدَّةُ الضَّوءِ والحرارةُ السَّاقطةِ عليه، فيَحِلَّ فصل الصَّيف، بينما يَحلُّ فصلُ الشِّتاءِ في نصف الكرة الجنوبيِّ.</br>وبعد ستةِ أشهرٍ تقريبًا يحدثُ العكس، فيكون مَيْلُ نصفِ الكرة الجنوبيّ نحو الشمس، ويحلُّ فصلُ الصيف هناك، بينما يحلُّ فصلُ الشتاء في نصف الكرةِ الشماليِّ."
        },
        
       
        
            // single choice text
        
        
       
    ],
    //report
    "report": {
        "enabled": true,// true mean enable false mean no report 
        "header": "تفاصيل التقرير ",// the header of report Modal
        "essay": "ليس له درجات",// this mean q type no degree for it
        "trueFalse": 1,// this mean the degree of this q
        "singleChoiceTxt": 1,
        "multiChoiceTxt": 1,
        "singleChoiceImg": 1,
        "multiChoiceImg": 1,
        "fillBlanK": 1,
        "fillDropDown": 1,
        "fillByDragDrop": 1,
        "math": 1
           
    },
    //feedback
    "feedback": true// enabled feedback or diabled take (true Or false)values
    ,//randomaize
    "randomaize": false// enabled random take (true Or false)values
};