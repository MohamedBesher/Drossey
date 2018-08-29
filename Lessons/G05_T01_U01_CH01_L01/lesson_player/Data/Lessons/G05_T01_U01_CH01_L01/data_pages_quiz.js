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
            "rightfeedback": "احسنت إجابة صحيحة ",
            "wrongfeedback": "الاجابة غير صحيحة "
        },*/
        //true or false quiz
        /*{
            "id": 2,
            "type": "trueFalse",
            "header": "حدد الجملة صح أم خطأ:",
            "body": "السؤال",
            'co_answer': true, //true or false
            "rightfeedback": "احسنت إجابة صحيحة ",
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
            "rightfeedback":"احسنت إجابة صحيحة ",
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
            "rightfeedback":"احسنت إجابة صحيحة ",
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
            "rightfeedback":"احسنت إجابة صحيحة ",
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
            "rightfeedback":"احسنت إجابة صحيحة ",
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
            "rightfeedback":"احسنت إجابة صحيحة ",
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
            "rightfeedback":"احسنت إجابة صحيحة ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },*/
        
                //fillBlanK quiz
        {
            "id": 7,
            "type": "fillBlanK",
            'example':false, // true mean this question is example else false
            "header": "أكمل ما يأتي: ",
            "body": "حجم الطلائعيات ===== من حجم البكتيريا. ",//question body 
            "blankwords":[
                {'correct':"أكبر"},
//                {'correct':"صحيحة"},
//                {'correct':"صحيحة"}
                
            ],//repalce words in question body by ( =====)example =====
            "partialfeedback":"اجابة غير دقيقة",
            "rightfeedback":"احسنت إجابة صحيحة ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },
        
                  //fillBlanK quiz
        {
            "id": 7,
            "type": "fillBlanK",
            'example':false, // true mean this question is example else false
            "header": "أكمل ما يأتي: ",
            "body": " ===== هو علم تقسيم المخلوقات الحية إلى مجموعات بحسب درجة التشابه في الشكل أو التركيب أو الوظيفة.",//question body 
            "blankwords":[
                {'correct':"التصنيف"},
//                {'correct':"صحيحة"},
//                {'correct':"صحيحة"}
                
            ],//repalce words in question body by ( =====)example =====
            "partialfeedback":"اجابة غير دقيقة",
            "rightfeedback":"احسنت إجابة صحيحة ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },
        
                        //fillBlanK quiz
        {
            "id": 7,
            "type": "fillBlanK",
            'example':false, // true mean this question is example else false
            "header": "أكمل ما يأتي: ",
            "body": "مملكة الطلائعيات تحتوي على أشباه الفطريات وأشباه الحيوانات وأشباه ===== ",//question body 
            "blankwords":[
                {'correct':"النباتات"},
//                {'correct':"صحيحة"},
//                {'correct':"صحيحة"}
                
            ],//repalce words in question body by ( =====)example =====
            "partialfeedback":"اجابة غير دقيقة",
            "rightfeedback":"احسنت إجابة صحيحة ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },
        
         //true or false quiz
        {
            "id": 2,
            "type": "trueFalse",
            "header": "حدد الجملة صح أم خطأ:",
            "body": "لا يمكن تصنيف الفيروسات ضمن أيٍّ من الممالك الستِّ. ",
            'co_answer': true, //true or false
            "rightfeedback": "أحسنت إجابة صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        },
        
         //true or false quiz
        {
            "id": 2,
            "type": "trueFalse",
            "header": "حدد الجملة صح أم خطأ:",
            "body": "توجد البكتيريا البدائية في كلِّ مكان تقريبا. ",
            'co_answer': false, //true or false
            "rightfeedback": "أحسنت إجابة صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        },
        
         //true or false quiz
        {
            "id": 2,
            "type": "trueFalse",
            "header": "حدد الجملة صح أم خطأ:",
            "body": "يعتبر الثعبان من اللافقاريات.  ",
            'co_answer': false, //true or false
            "rightfeedback": "أحسنت إجابة صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        },
       
        
     // single choice text
         {
            "id": 1,
            "type": "singleChoiceTxt",
            "header": "إختر الإجابة الصحيحة ",
            "body": "من أنواع البكتريا الحقيقية: ...........",
            'co_answer': "كل الإجابات صحيحة",
            'wro_answer': [
                { 'wrong': "العصوية " },
                { 'wrong': "الكروية " },
                { 'wrong': "الحلزونية " }
            ],
            "rightfeedback": "أحسنت إجابة صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        },
        
             // single choice text
         {
            "id": 1,
            "type": "singleChoiceTxt",
            "header": "إختر الإجابة الصحيحة ",
            "body": "........... مخلوقات حية وحيدة الخلية تتكون من خلية واحدة لا نواة لها",
            'co_answer': "البكتريا",
            'wro_answer': [
                { 'wrong': "الفيروسات " },
                { 'wrong': "الفطريات  " },
            ],
            "rightfeedback": "أحسنت إجابة صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        },
        
               // single choice text
         {
            "id": 1,
            "type": "singleChoiceTxt",
            "header": "إختر الإجابة الصحيحة ",
            "body": "تعتبر الحزازيات من النباتات: ..........",
            'co_answer': "اللاوعائية",
            'wro_answer': [
                { 'wrong': "الوعائية  " },
            ],
            "rightfeedback": "أحسنت إجابة صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        },
        
        
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