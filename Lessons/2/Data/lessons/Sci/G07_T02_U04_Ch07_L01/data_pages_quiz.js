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
        {
            "id": 7,
            "type": "fillBlanK",
            'example':true, // true mean this question is example else false
            "header": "أكمل ما يأتي:",
            "body": " يتكوّن الغلاف الجوي من عدة غازات، أهمها ===== والأكسجين. ",//question body 
            "blankwords":[
                {'correct':"النيتروجين"},
//                {'correct':"صحيحة"},
//                {'correct':"صحيحة"}
                
            ],//repalce words in question body by ( =====)example =====
            "partialfeedback":"اجابة غير دقيقة",
            "rightfeedback":"احسنت اجابتك صحيحة ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },
        
         {
            "id": 7,
            "type": "fillBlanK",
            'example':true, // true mean this question is example else false
            "header": "أكمل ما يأتي:",
            "body": " يتكوّن ===== من مواد صلبة، مثل الغبار والأملاح وحبوب اللقاح. ",//question body 
            "blankwords":[
                {'correct':"الهباء الجوي"},
//                {'correct':"صحيحة"},
//                {'correct':"صحيحة"}
                
            ],//repalce words in question body by ( =====)example =====
            "partialfeedback":"اجابة غير دقيقة",
            "rightfeedback":"احسنت اجابتك صحيحة ",
            "wrongfeedback":"الاجابة غير صحيحة "
        },
        
         {
            "id": 7,
            "type": "fillBlanK",
            'example':true, // true mean this question is example else false
            "header": "أكمل ما يأتي:",
            "body": "تُعرّف ===== بأنها مقدار بخار الماء في الغلاف الجوي.",//question body 
            "blankwords":[
                {'correct':"الرطوبة"},
//                {'correct':"صحيحة"},
//                {'correct':"صحيحة"}
                
            ],//repalce words in question body by ( =====)example =====
            "partialfeedback":"اجابة غير دقيقة",
            "rightfeedback":"احسنت اجابتك صحيحة ",
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
            "body": " يشكّل النيتروجين  <img width='20px' src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABkAAAAdCAYAAABfeMd1AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyJpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6RUUyNDQ5MkU1OEM5MTFFN0I3QTNBOTg0MUFCNjc4QTEiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6RUUyNDQ5MkY1OEM5MTFFN0I3QTNBOTg0MUFCNjc4QTEiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDpFRTI0NDkyQzU4QzkxMUU3QjdBM0E5ODQxQUI2NzhBMSIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDpFRTI0NDkyRDU4QzkxMUU3QjdBM0E5ODQxQUI2NzhBMSIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PhDBADQAAAFFSURBVHjatJWBjYMgFIaRuEBvBG8EHMGOYEdoR7iOUEdoR6gj4AjeCL0RzhHa95LfhBArUB8v+SMR5PMHHq8wxjRKqT8oOcZxDI7RJEt6QG1g/A/pSbqn/Ih22hU+PkZ8134K+cXzSjJKMFxI54AuuSAT6Yx2A4lDOAZo3uQskHnZRN0sQcTd6DfvXTdVLoioG73SN7s5bnWzBhFzowP9Im5CEBE3OmKM66bJBRm8I50cZeS4DoC3t3Nd163nli/bjopaz076CAhfnnvApgVAg1o00aQFC+Pu3FdQ+d2UaFx+aSKurhW1vz34PzvSSiaMU4v8FdiVEgRy8LWwhBVyq5dyshRXPG9ZIOTiglN2JpdDmQFgAeDj26XkSczkOxxjBpwIcEtNxhiAxUYfOAE/yfhQWBzjPe+B37k5GbE8du22eAkwACDuYRZPwQKJAAAAAElFTkSuQmCC'> نحو 8 %، ويشكل الأكسجين <img width='20px' src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABsAAAAdCAYAAABbjRdIAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyJpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6REY5RDNDQzU1OEM5MTFFNzlGN0FFN0IwRjQwODNCNDgiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6REY5RDNDQzY1OEM5MTFFNzlGN0FFN0IwRjQwODNCNDgiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDpERjlEM0NDMzU4QzkxMUU3OUY3QUU3QjBGNDA4M0I0OCIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDpERjlEM0NDNDU4QzkxMUU3OUY3QUU3QjBGNDA4M0I0OCIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/Pn37wucAAAGqSURBVHjazJaNjYMwDIVDxQJ0BG6EMgKMACPQEWCEMsIxQhmhjEBXYITLCNSWXiRfBIHAId2TrPCbL3YcJ8E0TYqVJImydCOL0L7JNNpZDcOg1hTOPKvISrJ45t1I1pC1aockjL14wRMjDYB5xgP4xv3dF3YR1wbEgJrsCuP4Bmg7fFsC6g+j+aoEKEOotPUtz1eBgRhgvsezCm3jSgLxTW/9tw1GXuWYL+Ux8Z3I2NjHMzP5/UzoltSL63QPzEejlcXe2dgfAHrDYs9/o38PG3fAYmv9bYa9RQdbkyXfC+tEylcbw1eKdak3w2hr0KgKZsTlyj8PEcbGO0EIKMsUF9jnTN3j+0EMpvZNf7nFZADlwpZUL3mF8leKysJONORQd7H2rgL71NtREzMHKEVUNHUesKHfJ78LHMeC2EpxZ4XhYwH1wXtiTNdf1iB+2IFwpRz5lqTbwqDYuyhUfyjy6DoTWhOhX3N2lszxoT0VRl49kJU1ed2HJ4JeADVYxyo8ARIh/Rl0J1DrOqQeBb2QEAUv5LUT8RGZs2fGc2S/dC1qX6WALUl/BBgA/iyIai18500AAAAASUVORK5CYII='>  نحو 71 %، أما النسبة الضئيلة جدًّا المتبقية فتتكوّن من غازات مختلفة .",
            'co_answer': false, //true or false
            "rightfeedback": "أحسنت إجابتك صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        }, 
        
               //true or false quiz
        {
            "id": 2,
            "type": "trueFalse",
            "header": "حدد الجملة صحيحة أم خاطئة:",
            "body": "  يقوم الغلاف الجوي - وهو طبقة الغازات المحيطة بالأرض - بتزويد الأرض بجميع الغازات اللازمة للحياة. ",
            'co_answer': true, //true or false
            "rightfeedback": "أحسنت إجابتك صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        }, 
        
               //true or false quiz
       

      
        

        
        
        
        
        
        // single choice text
         {
            "id": 1,
            "type": "singleChoiceTxt",
            "header": "إختر الإجابة الصحيحة ",
            "body": "      أما حبوب اللقاح فتدخل الغلاف الجوي مباشرة من ...................... ",
            'co_answer': "النباتات ",
            'wro_answer': [
                { 'wrong': "الحيوانات  " },
                { 'wrong': "الإنسان" }
            ],
            "rightfeedback": "أحسنت إجابتك صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        }, 
        
            // single choice text
         {
            "id": 1,
            "type": "singleChoiceTxt",
            "header": "إختر الإجابة الصحيحة ",
            "body": " يصف .................. الحالة السائدة في الغلاف الجوي.  ",
            'co_answer': "الطقس  ",
            'wro_answer': [
                { 'wrong': "الضغط  " },
                { 'wrong': "الغلاف الجوي " }
            ],
            "rightfeedback": "أحسنت إجابتك صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        }, 
        
           // single choice text
         {
            "id": 1,
            "type": "singleChoiceTxt",
            "header": "إختر الإجابة الصحيحة ",
            "body": "  تقوم جزيئات الهواء المتحركة بسرعة عالية بنقل الطاقة إلى الجزيئات البطيئة الحركة عندما تصطدم بها. وتسمى عملية .........................",
            'co_answer': "نقل الطاقة",
            'wro_answer': [
                { 'wrong': "انعدام الطاقة " },
                { 'wrong': "التسارع" },
//                { 'wrong': "الفاصولياء" },

            ],
            "rightfeedback": "أحسنت إجابتك صحيحة ",
            "wrongfeedback": "الإجابة غير صحيحة "
        }, 
        
            // single choice text
        
        {
            "id": 6,
            "type": "essay",
            "header": "اذكر الجملة الدالة على المصطلحات الآتية:",
            "body": "الطقس",
            "feedback_title": "الإجابة الصحيحة",
            "rightfeedback": "                               ( هو الحالة السائدة في الغلاف الجوي، وتتضمن عوامل الطقس كلا من درجة الحرارة والغيوم وسرعة الرياح.)"
        },
        
        {
            "id": 6,
            "type": "essay",
            "header": "اذكر الجملة الدالة على المصطلحات الآتية:",
            "body": " الغيوم المتوسطة ",
            "feedback_title": "الإجابة الصحيحة",
            "rightfeedback": "                                            ( تكون على ارتفاعات تتراوح بين 2000 م - 8000 م، وتتكون من خليط من ماء سائل وبلورات جليدية، وقد تسبب أمطارًا خفيفة. ومن أمثلتها: الغيوم الركامية المتوسطة، والغيوم الطبقية المتوسطة.)"
        },
        
        {
            "id": 6,
            "type": "essay",
            "header": "اذكر الجملة الدالة على المصطلحات الآتية:",
            "body": "الغلاف الجوي",
            "feedback_title": "الإجابة الصحيحة",
            "rightfeedback": "( هو طبة الغازات المحيطة بالأرض، وتزود الأرض بجميع الغازات اللازمة للحياة.)"
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